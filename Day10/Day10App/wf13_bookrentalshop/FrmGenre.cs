using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using wf13_bookrentalshop.Helpers;

namespace wf13_bookrentalshop
{
    public partial class FrmGenre : Form
    {
        bool isNew = false; // 신규가 false(update) / true(insert)

        #region < 생성자 >

        public FrmGenre() // 생성자는 리턴없음
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

                string strCheckQuery = "SELECT COUNT(*) FROM bookstbl WHERE Division = @Division";
                MySqlCommand chkCmd = new MySqlCommand(strCheckQuery, conn);
                MySqlParameter prmDivision = new MySqlParameter("@Division", TxtDivision.Text); // 파라미터랑 커맨드랑 연결
                chkCmd.Parameters.Add(prmDivision);

                var result = chkCmd.ExecuteScalar(); // 쿼리실행하면 오브젝트를 리턴받음
                
                if (result.ToString() != "0")
                {
                    MessageBox.Show("이미 사용중인 코드입니다.", "삭제", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                TxtDivision.Text = selData.Cells[0].Value.ToString(); // Text 데이터속성은 string selData.Cells[0].Value;는 오브젝트.
                                                                      // 오브젝트의 조상인 .ToString 해줘야함
                TxtNames.Text = selData.Cells[1].Value.ToString();
                TxtDivision.ReadOnly = true; // PK(장르코드)는 수정하면 안됨

                isNew = false; // 수정
            }
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
                    var selQuery = @"SELECT Division
	                                      , Names
                                       FROM divtbl";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(selQuery, conn);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds, "divtbl"); // divtbl으로 DataSet 접근가능

                    DgvResult.DataSource = ds.Tables[0];
                    // DgvResult.DataSource = ds;
                    // DgvResult.DataMember = "divtbl"; 위랑 같음

                    DgvResult.Columns[0].HeaderText = "장르코드";
                    DgvResult.Columns[1].HeaderText = "장르명";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"비정상적 오류 {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInputs()
        {
            TxtDivision.Text = TxtNames.Text = string.Empty;
            TxtDivision.ReadOnly = false; // 신규일땐 입력 가능
            TxtDivision.Focus();
            isNew = true; // 신규
        }

        // 입력검증 - 실무에서 많은 일을 함
        private bool CheckValidation()
        {
            var result = true;
            var errorMsg = string.Empty; // errormessage
            if (string.IsNullOrEmpty(TxtDivision.Text))
            {
                // 입력 검증 (Validation check)
                result = false;
                errorMsg = "● 장르코드를 입력하세요.\r\n";
                //MessageBox.Show("장르코드를 입력하세요", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //return false; // 메서드 탈출
            }

            if (string.IsNullOrEmpty(TxtNames.Text))
            {
                // 입력 검증 (Validation check)
                result = false;
                errorMsg += "● 장르명을 입력하세요.\r\n";
                //MessageBox.Show("장르명을 입력하세요", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        query = @"INSERT INTO divtbl
	                                   VALUES (@Division, @Names)";
                    }
                    else
                    {
                        query = @"UPDATE divtbl
                                     SET Names = @Names 
                                   WHERE Division = @Division";
                    }

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlParameter prmDivision = new MySqlParameter("@Division", TxtDivision.Text);
                    MySqlParameter prmNames = new MySqlParameter("@Names", TxtNames.Text);
                    cmd.Parameters.Add(prmDivision);
                    cmd.Parameters.Add(prmNames);

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

                    var query = @"DELETE FROM divtbl
	                              WHERE Division = @Division";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlParameter prmDivision = new MySqlParameter("@Division", TxtDivision.Text);
                    cmd.Parameters.Add(prmDivision);


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
