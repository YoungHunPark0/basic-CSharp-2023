using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using wf13_bookrentalshop.Helpers;

namespace wf13_bookrentalshop
{
    public partial class FrmLogin : Form
    {
        private bool isLogined = false; // 로그인이 성공했는지 물어보는 변수
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            isLogined = LoginProcess(); // 로그인을 성공해야만 true가 됨

            if (isLogined) this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            // Application.Exit(); 창만꺼지고 책대여점 창은 켜짐
            Environment.Exit(0); // 가장 완벽하게 프로그램 종료 메서드
        }

        // 이게 없으면 X버튼이나 Alt+F4로 했을때 메인폼이 나타남
        private void FrmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (isLogined != true)
            {   // 로그인 안되었을때 창을 닫으면 프로그램 모두 종료
                Environment.Exit(0);
            }
        }

        private void TxtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) // 엔터키를 누르면
            {
                BtnLogin_Click(sender, e); // 버튼클릭 이벤트핸들러 호출
            }
        }

        // DB userTbl에서 정보확인 로그인처리
        private bool LoginProcess()
        {
            //★ Validation check 입력(데이터) 검증 ★
            if (string.IsNullOrEmpty(TxtUserId.Text))
            {
                MessageBox.Show("유저아이디를 입력하세요", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrEmpty(TxtPassword.Text))
            {
                MessageBox.Show("패스워드를 입력하세요", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            string strUserId = ""; // 받아온 결과를 받아올거라서
            string strPassword = "";

            try
            {//< DB처리 >
             //예전방식 잘안씀
             //MySqlConnection conn = new MySqlConnection("");
             //conn.Open();
             //conn.Close(); // 여기선 close써야함


                //string connectionString = "Server=localhost;Port=3306;Database=bookrentalshop;Uid=root;Pwd=12345";
                // -> helpers 폴더 네임스페이스에 connstring 메소드에 서버주소 등록해서 필요없어짐
                //< DB처리 >
                // ★최신방식★
                using (MySqlConnection conn = new MySqlConnection(Helpers.Commons.ConnString))
                //using쓰면 자동으로 close써줌
                {
                    conn.Open();

                    #region << ★DB쿼리를 위한 구성★ >>
                    string selQuery = @"SELECT userID
	                                         , password
                                          FROM usertbl
                                         WHERE userID = @userID
                                           AND password = @password";
                    MySqlCommand selCmd = new MySqlCommand(selQuery, conn);
                    // @userID, @password 파라미터를 할당
                    MySqlParameter prmUserID = new MySqlParameter("@userID", TxtUserId.Text);
                    MySqlParameter prmPassword = new MySqlParameter("@password", TxtPassword.Text);
                    selCmd.Parameters.Add(prmUserID);
                    selCmd.Parameters.Add(prmPassword);
                    #endregion

                    MySqlDataReader reader = selCmd.ExecuteReader(); // 실행한 다음에 userID, password 받아옴. 받아온걸 C#에서 쓸 수 있음
                    if (reader.Read())
                    {
                        strUserId = reader["userID"] != null ? reader["userID"].ToString() : "-";
                        strPassword = reader["password"] != null ? reader["password"].ToString() : "--";
                        Commons.LoginID = strUserId; // 로그인아이디에 struserid 들어가서 프로그램 전체에서 사용
                        return true;
                    }
                    else
                    {
                        MessageBox.Show($"로그인 정보가 없습니다!", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Commons.LoginID = strUserId;    // 실패할때 비사용
                        return false;
                    }
                }   // conn.Close();

                
                // MessageBox.Show($"{strUserId} / {strPassword}"); - 테스트디버깅용 // 실제로 안씀
            }
            catch (Exception ex)
            {
                MessageBox.Show($"비정상적 오류 {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // 유저아이디 텍스트박스에서 엔터를 치면 패스워드 텍스트박스로 포커스 이동
        private void TxtUserId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) // 엔터치면
            {
                TxtPassword.Focus(); // 자동으로 다음박스로 넘어감 편함 
            }
        }
    }
}
