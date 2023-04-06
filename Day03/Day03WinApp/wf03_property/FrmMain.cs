﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wf03_property
{
    public partial class FrmMain : Form
    {
        Random rnd = new Random();
        public FrmMain()
        {
            InitializeComponent();
           // 생성자에는 되도록 설정부분을 넣지마세요.
           // Form_Load() 이벤트 핸들러에 작성할 것 
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            GbxMain.Text = "컨트롤 학습";
            var fonts = FontFamily.Families.ToList(); // 내 OS 폰트이름 가져오기
            foreach (var font in fonts)
            {
                CboFontFamily.Items.Add(font.Name); 
            }
            // 글자크기 최소값, 최대값 지정
            NudFontSize.Minimum = 5;
            NudFontSize.Maximum = 40;
            // 텍스트박스 초기화
            TxtResult.Text = "Hello, WinForms!!";

            NudFontSize.Value = 9; // 최초 글자체 크기를 9로 지정
        }

        /// <summary>
        /// 글자 스타일, 크기, 글자체를 변경해주는 메서드 
        /// </summary>
        private void ChangeFontStyle()
        {
            if (CboFontFamily.SelectedIndex < 0)
            {
                CboFontFamily.SelectedIndex = 257; // 디폴트는 나눔고딕으로 지정.
            }

            FontStyle style = FontStyle.Regular; // Regular == 일반, 기본
            if(ChkBold.Checked==true)
            {
                style |= FontStyle.Bold; // 비트연산 | == or
            }
            if(ChkItalic.Checked==true) 
            {
                style |= FontStyle.Italic;
            }

            decimal fontSize = NudFontSize.Value;

            TxtResult.Font = new Font((string)CboFontFamily.SelectedItem, (float)fontSize, style);
        }

        void ChangIndent()
        {
            if (RdoNormal.Checked) // 라디오버튼 추가 이벤트
            {
                TxtResult.Text = TxtResult.Text.Trim();
            }
            else if (RboIndent.Checked)
            {
                TxtResult.Text = "    " + TxtResult.Text;
            }
        }

        private void CboFontFamily_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeFontStyle();
        }

        private void ChkBold_CheckedChanged(object sender, EventArgs e)
        {
            ChangeFontStyle();
        }

        private void ChkItalic_CheckedChanged(object sender, EventArgs e)
        {
            ChangeFontStyle();
        }

        private void NudFontSize_ValueChanged(object sender, EventArgs e)
        {
            ChangeFontStyle();
        }

        private void TrbDummy_Scroll(object sender, EventArgs e)
        {
            PgbDummy.Value = TrbDummy.Value;
        }

        private void BtnModal_Click(object sender, EventArgs e)
        {
            Form frm = new Form()
            {
                Text = "Modal Form",
                Width = 300,
                Height = 200,
                Left = 10,
                Top = 20,
                BackColor= Color.Crimson,
            };
            frm.ShowDialog(); // 모달방식으로 자식창 띄우기
        }

        private void BtnModaless_Click(object sender, EventArgs e)
        {
            Form frm = new Form()
            {
                Text = "Modaless Form",
                Width = 300,
                Height = 200,
                StartPosition = FormStartPosition.CenterParent, // Modaless는 CenterParent가 안먹힘
                BackColor = Color.GreenYellow,
            };
            frm.Show(); // 모달리스방식으로 자식창 띄우기
        }

        // 기본적으로는 MessageBox는 모달(창뜨고 다른거 누르면 안됨).
        private void BtnMsgBox_Click(object sender, EventArgs e)
        {
            // 1. MessageBox.Show(TxtResult.Text); // 가장 기본
            // MessageBox.Show(TxtResult.Text, caption: "메시지창"); // 2. 캡션
            //MessageBox.Show(TxtResult.Text, "메세지창", MessageBoxButtons.AbortRetryIgnore); // 3. 버튼추가
            // MessageBox.Show(TxtResult.Text, "메세지창",
            //     MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error); // 4. 아이콘추가
            MessageBox.Show(TxtResult.Text, "메세지창", MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2); // 5. 기본포커스 버튼 설정
            //MessageBox.Show(TxtResult.Text, "메세지창", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, 
            //    MessageBoxDefaultButton.Button2, MessageBoxOptions.RightAlign); // 6. 글자 오른쪽 정렬
        }

        private void BtnAddRoot_Click(object sender, EventArgs e)
        {//부모
            //Add(rnd.Next()) 그냥안됨! add(string) 들어가야하는데 숫자를 문자로! Tostring() 써야함
            TrvDummy.Nodes.Add(rnd.Next(50).ToString());
            TreeToList();
        }

        private void BtnAddChild_Click(object sender, EventArgs e)
        {// 자식
            // 루트 추가 클릭해서 나온 값을 클릭해서 자식추가 할 것
            if (TrvDummy.SelectedNode != null) // TrvDummy에 선택된 값이 null이 아니면
            {
                TrvDummy.SelectedNode.Nodes.Add(rnd.Next(50, 100).ToString()); // 랜덤50~100사이 (부모값보다 높음)
                TrvDummy.SelectedNode.Expand(); //expand==펼치기// 트리노드 하위것 펼쳐주기 반대=> .Collapse()
                TreeToList();
            }
        }

        void TreeToList()
        {
            LsvDummy.Items.Clear(); // 리스트뷰, 트리뷰 모든 아이템을 제거 초기화 메서드
            foreach (TreeNode item in TrvDummy.Nodes)
            {
                TreeToList(item); // void treetolist 가 아닌 파라미터 받는 메서드 만든것
            }
        }

        private void TreeToList(TreeNode item)
        {
            LsvDummy.Items.Add(new ListViewItem(new[] { item.Text, item.FullPath.ToString() }));

            foreach (TreeNode node in item.Nodes)
            {
                TreeToList(node); // 재귀호출
            }
        }

        private void RdoNormal_CheckedChanged(object sender, EventArgs e)
        {
            ChangIndent();
        }

        private void RboIndent_CheckedChanged(object sender, EventArgs e)
        {
            ChangIndent();
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            PcbDummy.Image = Bitmap.FromFile("cat.png");
        }

        private void PcbDummy_Click(object sender, EventArgs e)
        {
            if (PcbDummy.SizeMode == PictureBoxSizeMode.Zoom) 
            {
                PcbDummy.SizeMode= PictureBoxSizeMode.StretchImage;
            }
            else
            {
                PcbDummy.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }
    }
}