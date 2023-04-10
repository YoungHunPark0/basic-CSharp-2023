using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wf06_listview
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            string currFolder = Environment.CurrentDirectory; // 현재프로그램 실행경로 .\bin\debug\
            DirectoryInfo di = new DirectoryInfo(currFolder);
            FileInfo[] files = di.GetFiles(); // 현재 디렉토리내 파일배열 가저옴

            LsvFiles.BeginUpdate(); // 업데이트 완료전까지는 UI갱신을 중지
            LsvFiles.View = View.Details; // 리스트뷰 화면은 자세히보기로
            CboView.SelectedIndex = 0; // 뷰보기 콤보박스의 첫번째값으로 설정

            // ListView에 사용할 아이콘 지정 // 코딩으로 넣어줘도 되고,
            // 디자인에서 largeimagelist, smallimagelist에서 지정해도됨
            //LsvFiles.LargeImageList = ImgLargeIcon;
            //LsvFiles.SmallImageList = ImgSmallIcon;

            foreach (FileInfo file in files) 
            {
                // 각 파일별로 ListViewItem 객체를 만들어 하나씩 지정
                ListViewItem lvi = new ListViewItem(file.Name); // 리스트뷰 첫번째값. 이름
                lvi.SubItems.Add(file.LastWriteTime.ToString()); // 수정한 날짜
                
                var ext = Path.GetExtension(file.Name);
                var extName = "";
                switch (ext)
                {
                    case ".exe":
                        extName = "응용프로그램";
                        break;
                    case ".config":
                        extName = "Configuration 원본파일";
                        break;
                    case ".pdb":
                        extName = "Program Debug Database";
                        break;
                    default:
                        extName = "기타";
                        break;
                } 
                // 아이콘. 실행파일만 exe아이콘, 나머진 파일아이콘
                if (ext == ".exe")
                {
                    lvi.ImageIndex = 0; 
                }
                else
                {
                    lvi.ImageIndex = 1;
                }

                lvi.SubItems.Add(extName); // 유형
                var fileSize = file.Length / 1024;
                lvi.SubItems.Add(string.Format("{0} KB", fileSize)); // 크기
                // 아이콘은 나중에..

                LsvFiles.Items.Add(lvi);
            }

            LsvFiles.EndUpdate(); // 업데이트 끝났으니 UI갱신
        }
    }
}
