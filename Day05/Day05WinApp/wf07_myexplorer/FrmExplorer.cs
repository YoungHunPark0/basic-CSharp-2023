﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wf07_myexplorer
{
    public partial class FrmExplorer : Form
    {
        public FrmExplorer()
        {
            InitializeComponent();
        }

        private void FrmExplorer_Load(object sender, EventArgs e)
        {
            // 현재사용자 정보 출력
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            LblPath.Text = identity.Name;

            // 현재 컴퓨터에 존재하는 드라이브 정보검색해서 트리뷰에 추가
            DriveInfo[] drives = DriveInfo.GetDrives();

            // 트리뷰에 전부다 추가
            foreach (DriveInfo drive in drives)
            {
                // 실제 존재하는 하드드라이브만 
                if (drive.DriveType == DriveType.Fixed)
                {
                    TreeNode rootNode = new TreeNode(drive.Name);
                    rootNode.ImageIndex = 0; // 이미지 넣었던거 순서
                    rootNode.SelectedImageIndex = 0;
                    TrvDrive.Nodes.Add(rootNode);
                    FillNodes(rootNode);

                }
            }

            TrvDrive.Nodes[0].Expand(); // [0] 확장

            // 리스트뷰 설정
            LsvFolder.View = View.Details;

            // details 속성넣기
            LsvFolder.Columns.Add("이름", LsvFolder.Width / 2, HorizontalAlignment.Left); // 왼쪽정렬  LsvFolder.Width / 2 => 전체사이즈/2
            LsvFolder.Columns.Add("날짜", LsvFolder.Width / 4, HorizontalAlignment.Left);
            LsvFolder.Columns.Add("유형", LsvFolder.Width / 5, HorizontalAlignment.Left);
            LsvFolder.Columns.Add("크기", LsvFolder.Width / 5, HorizontalAlignment.Right); // 오른쪽정렬

            LsvFolder.FullRowSelect = true; // 한행을 전부 선택
        }

        private void FillNodes(TreeNode curNode)
        {
            try
            {// 예외값 발생할 수 있는 내용
                DirectoryInfo dir = new DirectoryInfo(curNode.FullPath);
                // 드라이브 하위폴더
                foreach (DirectoryInfo item in dir.GetDirectories())
                {
                    TreeNode newNode = new TreeNode(item.Name);
                    newNode.ImageIndex = 1;
                    newNode.SelectedImageIndex = 2; // 눌렀을때
                    curNode.Nodes.Add(newNode);
                    newNode.Nodes.Add("*");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("트리 오류발생!", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // sender 자기자신 객체 내용, e 이벤트와 관련된 속성포함
        /// <summary>
        /// 트리뷰 노드 확장하기 전 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       
        private void TrvDrive_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Nodes[0].Text == "*")
            {
                e.Node.Nodes.Clear();
                e.Node.ImageIndex = 1; // folder - nomal
                e.Node.SelectedImageIndex = 2; // folder - open
                FillNodes(e.Node); // 하위폴더 만들어줌
            }
        }
        /// <summary>
        /// 트리뷰 접기전 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TrvDrive_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            
           e.Node.ImageIndex = 1; 
           e.Node.SelectedImageIndex = 1;
            
        }

        /// <summary>
        /// 트리노드를 마우스 클릭이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TrvDrive_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            SetLsvFolder(e.Node.FullPath);
        }

        private void SetLsvFolder(string fullPath)
        {
            try
            {
                LsvFolder.Items.Clear(); // 기존 리스트 삭제
                DirectoryInfo dir = new DirectoryInfo(fullPath); // 현재선택한 경로의 폴더는 닫아줘야함
                int dirCount = 0;

                foreach (DirectoryInfo item in dir.GetDirectories()) 
                {
                    ListViewItem lvi = new ListViewItem();
                    
                    lvi.ImageIndex = 1;
                    lvi.Text = item.Name;   // 0번인덱스- 이름

                    LsvFolder.Items.Add(lvi);

                    LsvFolder.Items[dirCount].SubItems.Add(item.CreationTime.ToString());
                    LsvFolder.Items[dirCount].SubItems.Add("폴더");
                    LsvFolder.Items[dirCount].SubItems.Add( string.Format( "{0} files", 
                                                           item.GetFiles().Length.ToString() ) );
                    dirCount++;
                } // 폴더내 디렉토리 리스트뷰에 리스트업

                // 파일목록 리스트업
                FileInfo[] files = dir.GetFiles();
                int fileCount = dirCount; // 이전 카운트가 승계

                foreach (FileInfo file in files)
                {
                    LsvFolder.Items.Add(file.Name);
                    if ( file.LastWriteTime != null )
                    {
                        LsvFolder.Items[fileCount].SubItems.Add(file.LastWriteTime.ToString());
                    }
                    else
                    {
                        LsvFolder.Items[fileCount].SubItems.Add(file.CreationTime.ToString());
                    }
                    LsvFolder.Items[fileCount].SubItems.Add(file.Attributes.ToString());
                    LsvFolder.Items[fileCount].SubItems.Add(file.Length.ToString());

                    fileCount++;
                }

            }
            catch (Exception)
            {
                MessageBox.Show("리스트뷰 오류발생!", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
