using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wf04_filecopy
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void BtnFindSource_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            var result = dialog.ShowDialog(); // 모달창
            if (result == DialogResult.OK)
            {
                TxtSource.Text = dialog.FileName;
            }
        }

        private void BtnFindTarget_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            var result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                TxtTarget.Text = dialog.FileName;
            }
        }

        // 일반적인 동기식 파일복사

        private long BtnCopySync(string fromFile, string toFile)
        {
            BtnAsyncCopy.Enabled = false; // 비동기버튼을 일시 비활성화
            long totalCopied = 0;

            using (FileStream fromStream = new FileStream(fromFile, FileMode.Open)) // 원본파일은 열고
            {
                using (FileStream toStream = new FileStream(toFile, FileMode.Create)) // 타겟파일은 새로 생성
                {
                    byte[] buffer = new byte[1024 * 1024]; // buffer의 정해진 형식 1MByte 버퍼
                    int nRead = 0;
                    while ((nRead = fromStream.Read(buffer, 0, buffer.Length)) != 0)  // 0이면 읽을게 없음
                    {
                        toStream.Write(buffer, 0, nRead);
                        totalCopied += nRead;

                        // 프로그래스바에 표시
                        PgbCopy.Value = (int)((double)totalCopied / (double)fromStream.Length) * PgbCopy.Maximum;
                    }
                }
            }
            BtnAsyncCopy.Enabled = true;
            return totalCopied;
        }
        // 비동기 호출할때는 await사용, 구현할때는 async사용

        private async Task<long> BtnCopyAsync(string fromFile, string toFile)
        {
            BtnAsyncCopy.Enabled = false; // 비동기버튼을 일시 비활성화
            long totalCopied = 0;

            using (FileStream fromStream = new FileStream(fromFile, FileMode.Open)) // 원본파일은 열고
            {
                using (FileStream toStream = new FileStream(toFile, FileMode.Create)) // 타겟파일은 새로 생성
                {
                    byte[] buffer = new byte[1024 * 1024]; // buffer의 정해진 형식 1MByte 버퍼
                    int nRead = 0;
                    while ((nRead = await fromStream.ReadAsync(buffer, 0, buffer.Length)) != 0)  // 0이면 읽을게 없음
                    {
                        await toStream.WriteAsync(buffer, 0, nRead);
                        totalCopied += nRead;

                        // 프로그래스바에 표시
                        PgbCopy.Value = (int)((double)totalCopied / (double)fromStream.Length) * PgbCopy.Maximum;
                    }
                }
            }
            BtnAsyncCopy.Enabled = true;
            return totalCopied;
        }
        
        private async void BtnAsyncCopy_Click_1(object sender, EventArgs e)
        {
            long totalCopied = await BtnCopyAsync(TxtSource.Text, TxtTarget.Text);
        }

        private void BtnSyncCopy_Click(object sender, EventArgs e)
        {
            long totalCopied = BtnCopySync(TxtSource.Text, TxtTarget.Text);
        }
    }
}
