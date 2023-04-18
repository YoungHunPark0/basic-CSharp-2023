using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using wf13_bookrentalshop.Helpers;

namespace wf13_bookrentalshop
{
    public partial class FrmBooks : Form
    {
        bool isNew = false; // 신규가 false(update) / true(insert)

        #region < 생성자 >

        public FrmBooks() // 생성자는 리턴없음
        {
            InitializeComponent();
        }

        #endregion

        #region < 이벤트 핸들러 > 
        // 이벤트 핸들러는 파라미터가 (object sender, EventArgs e) (샌들러, 아규먼트)

        private void FrmGenre_Load(object sender, EventArgs e)
        {
            isNew = true; // 신규부터 시작
            RefreshData();
            LoadCboData(); // 콤보박스에 들어갈 데이터 로드

            // 출판일자 데이터형식 변경
            DtpReleaseData.Format = DateTimePickerFormat.Custom;
            DtpReleaseData.CustomFormat = "yyyy-MM-dd"; // 요일 안나옴
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (CheckValidation() != true) return;

            SaveData(); // 데이터 저장/수정
            RefreshData(); // 데이터 재조회
            ClearInputs(); // 입력창 클리어
        }

        private void BtnDel_Click(object sender, EventArgs e)
        {
            if (isNew == true) // 신규
            {
                MessageBox.Show("삭제할 데이터를 입력하세요", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // FK제약조건으로 지울 수 없는 데이터인지 먼저 확인
            using (MySqlConnection conn = new MySqlConnection(Commons.ConnString)) // mysql 커넥션 만들고
            {
                if (conn.State == ConnectionState.Closed) conn.Open();

                string strCheckQuery = "SELECT COUNT(*) FROM rentaltbl WHERE bookIdx = @bookIdx";
                MySqlCommand chkCmd = new MySqlCommand(strCheckQuery, conn);
                MySqlParameter prmBookIdx = new MySqlParameter("@bookIdx", TxtBookIdx.Text); // 파라미터랑 커맨드랑 연결
                chkCmd.Parameters.Add(prmBookIdx);

                var result = chkCmd.ExecuteScalar(); // 쿼리실행하면 오브젝트를 리턴받음
                
                if (result.ToString() != "0")
                {
                    MessageBox.Show("이미 대여중인 책입니다.", "삭제", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

                // 삭제여부를 물을때 아니오를 누르면 삭제진행 취소
                if (MessageBox.Show(this, "삭제하시겠습니까?", "삭제", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

            // Yes를 누르면 계속 진행 // SaveData() 에 있는 로직 복사->수정
            DeleteData(); // 데이터 삭제처리
            RefreshData(); // 지우고나서 재조회
            ClearInputs(); // 입력창 데이터 지우기
        }

        // 그리드뷰 클릭하면 발생하는 이벤트
        private void DgvResult_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1) // 아무것도 선택안했을때 == -1, 뭐라도 선택하면 == 0 부터 시작
            {
                var selData = DgvResult.Rows[e.RowIndex]; // dgvresult.rows 데이터를 seldata로 보냄
                //MessageBox.Show(selData.ToString());
                //Debug.WriteLine(selData.ToString());
                Debug.WriteLine(selData.Cells[0].Value);
                Debug.WriteLine(selData.Cells[1].Value);
                TxtBookIdx.Text = selData.Cells[0].Value.ToString(); // Text 데이터속성은 string selData.Cells[0].Value;는 오브젝트.
                                                                      // 오브젝트의 조상인 .ToString 해줘야함
                TxtAuthor.Text = selData.Cells[1].Value.ToString();
                CboDivision.SelectedValue = selData.Cells[2].Value; // B001 == B001
                                                                    // selData.Cells[3] 사용안함
                TxtNames.Text = selData.Cells[4].Value.ToString();
                DtpReleaseData.Value = (DateTime)selData.Cells[5].Value;
                TxtISBN.Text = selData.Cells[6].Value.ToString();
                NudPrice.Text = selData.Cells[7].Value.ToString(); // decimal 써야하지만 작은값이기에 int

                isNew = false; // 수정
            }
        }

        private void DgvResult_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DgvResult.ClearSelection(); // 최초에 첫번째열 첫번째셀 선택되어있는것을 해제
        }

        #endregion

        #region < 커스텀 메서드 >
        // 커스텀메서드는 파라미터가 매개변수가 있을 수도 있고 없을 수도 있음. int x, y 등등
        private void RefreshData()
        {
            // DB divtbl 데이터 조회 DgvResult 뿌림
            try
            {
                using (MySqlConnection conn = new MySqlConnection(Helpers.Commons.ConnString))
                {
                    if (conn.State == ConnectionState.Closed) conn.Open(); // 연결된게 아니면 오픈

                    // 쿼리 작성
                    var selQuery = @"SELECT b.bookIdx,
	                                        b.Author,
                                            b.Division,
                                            d.Names AS DivNames,
	                                        b.Names,
                                            b.ReleaseDate,
                                            b.ISBN,
                                            b.Price
                                       FROM bookstbl AS b
                                      INNER JOIN divtbl AS d
		                                 ON b.Division = d.Division
                                      ORDER BY b.bookIdx ASC"; // 정렬때문에 추가
                    MySqlDataAdapter adapter = new MySqlDataAdapter(selQuery, conn);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds, "bookstbl"); // divtbl으로 DataSet 접근가능

                    DgvResult.DataSource = ds.Tables[0];
                    // DgvResult.DataSource = ds;
                    // DgvResult.DataMember = "divtbl"; 위랑 같음

                    // 데이터그리드뷰 컬럼 헤더 제목
                    DgvResult.Columns[0].HeaderText = "번호";
                    DgvResult.Columns[1].HeaderText = "저자명";
                    DgvResult.Columns[2].HeaderText = "책장르";
                    DgvResult.Columns[3].HeaderText = "책장르";
                    DgvResult.Columns[4].HeaderText = "책제목";
                    DgvResult.Columns[5].HeaderText = "출판일자";
                    DgvResult.Columns[6].HeaderText = "ISBN";
                    DgvResult.Columns[7].HeaderText = "책가격";

                    // 컬럼 넓이 또는 보이기
                    DgvResult.Columns[0].Width = 55;
                    DgvResult.Columns[2].Visible = false; // B001같은 코드영역은 보일필요 없음
                    DgvResult.Columns[5].Width = 78;
                    DgvResult.Columns[7].Width = 80;

                    // 컬럼 정렬
                    DgvResult.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; // 번호
                    DgvResult.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // 출판일자
                    DgvResult.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; // 책가격                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"RefreshData() 비정상적 오류 {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCboData()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(Commons.ConnString))
                {
                    if (conn.State == ConnectionState.Closed) { conn.Open(); }
                    var qurey = "SELECT Division, Names FROM divtbl";
                    MySqlCommand cmd = new MySqlCommand(qurey, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    var temp = new Dictionary<string, string>();
                    while (reader.Read()) //read에 값이 있을때 동안
                    {
                        temp.Add(reader[0].ToString(), reader[1].ToString()); // (key)B001, (value)공포/스릴러
                    }

                    // 콤보박스에 할당
                    CboDivision.DataSource = new BindingSource(temp, null); // source를 집어넣을 수 있는 객체를 만듬. 
                    CboDivision.DisplayMember = "Value";
                    CboDivision.ValueMember = "Key";
                    CboDivision.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"LoadData() 비정상적 오류 {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInputs()
        {
            TxtBookIdx.Text = TxtAuthor.Text = string.Empty;
            TxtNames.Text = TxtISBN.Text = string.Empty;
            CboDivision.SelectedIndex = -1;
            DtpReleaseData.Value = DateTime.Now; // 오늘날짜로 초기화
            NudPrice.Value = 0;
            //TxtBookIdx.ReadOnly = false; // 신규일땐 입력 가능
            TxtAuthor.Focus(); // 번호는 입력안함
            isNew = true; // 신규
        }

        // 입력검증 - 실무에서 많은 일을 함
        private bool CheckValidation()
        {
            var result = true;
            var errorMsg = string.Empty; // errormessage

            if (string.IsNullOrEmpty(TxtAuthor.Text))
            {
                // 입력 검증 (Validation check)
                result = false;
                errorMsg += "● 저자명을 입력하세요.\r\n";
                //MessageBox.Show("장르명을 입력하세요", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //return false; // 메서드 탈출
            }

            if (CboDivision.SelectedIndex < 0)
            {
                // 입력 검증 (Validation check)
                result = false;
                errorMsg += "● 장르를 입력하세요.\r\n";
                //MessageBox.Show("장르명을 입력하세요", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //return false; // 메서드 탈출
            }

            if (string.IsNullOrEmpty(TxtNames.Text))
            {
                // 입력 검증 (Validation check)
                result = false;
                errorMsg += "● 책제목을 입력하세요.\r\n";
                //MessageBox.Show("장르명을 입력하세요", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //return false; // 메서드 탈출
            }

            if (DtpReleaseData.Value == null)
            {
                // 입력 검증 (Validation check)
                result = false;
                errorMsg = "● 출판일자를 선택하세요.\r\n";
                //MessageBox.Show("장르코드를 입력하세요", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //return false; // 메서드 탈출
            }

            if (result == false)
            {
                MessageBox.Show(errorMsg, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // 메서드 탈출
            }
            else
            {
                return result;
            }
        }

        // isNew = true INSERT / false UPDATE
        private void SaveData()
        {
            // INSERT부터 시작
            try
            {
                using (MySqlConnection conn = new MySqlConnection(Helpers.Commons.ConnString))
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();

                    var query = "";

                    if (isNew) // isNew가 true면
                    {
                        query = @"INSERT INTO bookstbl
                                             (Author,
                                              Division,
                                              Names,
                                              ReleaseDate,
                                              ISBN,
                                              Price)
                                       VALUES
                                             (@Author,
                                              @Division,
                                              @Names,
                                              @ReleaseDate,
                                              @ISBN,
                                              @Price);";
                    }
                    else
                    {
                        query = @"UPDATE bookstbl
                                     SET Author = @Author,
	                                     Division = @Division,
	                                     Names = @Names,
	                                     ReleaseDate = @ReleaseDate,
	                                     ISBN = @ISBN,
	                                     Price = @Price 
                                   WHERE bookIdx = @bookIdx;"; // ★마지막 @Price 있는부분 , 안지우면 에러남!★
                    }

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlParameter prmAuthor = new MySqlParameter("@Author", TxtAuthor.Text);
                    MySqlParameter prmDivision = new MySqlParameter("@Division", CboDivision.SelectedValue.ToString());
                    MySqlParameter prmNames = new MySqlParameter("@Names", TxtNames.Text);
                    MySqlParameter prmReleaseDate = new MySqlParameter("@ReleaseDate", DtpReleaseData.Value);
                    MySqlParameter prmISBN = new MySqlParameter("@ISBN", TxtISBN.Text);
                    MySqlParameter prmPrice = new MySqlParameter("@Price", NudPrice.Value);

                    cmd.Parameters.Add(prmAuthor);
                    cmd.Parameters.Add(prmDivision);
                    cmd.Parameters.Add(prmNames);
                    cmd.Parameters.Add(prmReleaseDate);
                    cmd.Parameters.Add(prmISBN);
                    cmd.Parameters.Add(prmPrice);

                    if (isNew == false) // update할 때는 bookIdx 파라미터를 추가!!
                    {
                        MySqlParameter prmBookIdx = new MySqlParameter("@bookIdx", TxtBookIdx.Text);
                        cmd.Parameters.Add(prmBookIdx);
                    }

                    var result = cmd.ExecuteNonQuery(); // INSERT, UPDATE, DELETE 일때 사용. 반환값을 result에 저장

                    if (result != 0)
                    {
                        // 저장성공
                        MessageBox.Show("저장성공!!", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // 저장실패
                        MessageBox.Show("저장실패!!", "저장", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"비정상적 오류 {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void DeleteData()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(Helpers.Commons.ConnString))
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();

                    var query = @"DELETE FROM bookstbl
	                                    WHERE bookIdx = @bookIdx";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlParameter prmBookIdx = new MySqlParameter("@bookIdx", TxtBookIdx.Text);
                    cmd.Parameters.Add(prmBookIdx);


                    var result = cmd.ExecuteNonQuery(); // INSERT, UPDATE, DELETE 일때 사용. 반환값을 result에 저장

                    if (result != 0)
                    {
                        // 저장성공
                        MessageBox.Show("삭제성공!!", "삭제", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // 저장실패
                        MessageBox.Show("삭제실패!!", "삭제", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"비정상적 오류 {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        #endregion
    }
}
