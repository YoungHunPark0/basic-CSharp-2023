using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wf05_login
{
    public partial class Frmain : Form
    {
        public Frmain()
        {
            InitializeComponent();
        }


        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (TbId.Text == "abcd" && TbPassword.Text == "1234")
            {
                LblSucess.Text = "로그인 성공";
                MessageBox.Show("로그인 성공!!", "로그인", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else LblSucess.Text= "로그인 실패"; 
            //MessageBox.Show("로그인 실패!!", "로그인", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

        }
    }
}
