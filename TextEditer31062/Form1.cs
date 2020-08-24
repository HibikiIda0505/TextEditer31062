using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextEditer31062
{
    public partial class Form1 : Form
    {
        //現在編集中のファイル名
        private string fileName = "";   //Camel形式（⇔Pascal形式）

        public Form1()
        {
            InitializeComponent();
        }

        //終了
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //アプリケーション終了
            Application.Exit();
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //［開く］ダイアログを表示
            if (ofdFileOpen.ShowDialog() == DialogResult.OK)
            {
                //StreamReaderクラスを使用してファイル読込み
                using (StreamReader sr = new StreamReader(ofdFileOpen.FileName, Encoding.GetEncoding("utf-8"), false))
                {
                    rtTextArea.Text = sr.ReadToEnd();
                    this.fileName = ofdFileOpen.FileName;  //現在開いているファイル名を設定
                }
            }
        }

        //上書き保存
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.fileName != "")
            {
                FileSave(fileName);
            }
            else
            {
                SaveNameToolStripMenuItem_Click(sender, e);
            }
        }
        
        //名前を付けて保存
        private void SaveNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //［名前を付けて保存］ダイアログを表示
            if (sfdFileSave.ShowDialog() == DialogResult.OK)
            {
                FileSave(sfdFileSave.FileName);
            }
        }

        //ファイル名を指定し、データを保存
        private void FileSave(string fileName)
        {
            using (StreamWriter sw = new StreamWriter(fileName, false, Encoding.GetEncoding("utf-8")))
            {
                sw.WriteLine(rtTextArea.Text);
            }
        }

        //元に戻す
        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rtTextArea.CanUndo)
            {
                //アンドゥする
                rtTextArea.Undo();
                //アンドゥバッファを削除する
                rtTextArea.ClearUndo();
            }
        }
    }
}
