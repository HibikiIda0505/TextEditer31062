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

        //新規作成メニュー
        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fileName = "";
            rtTextArea.Text = "";
        }

        //終了
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //アプリケーション終了
            Application.Exit();   
        }

        //開く
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

        //編集メニュー項目内のマスク処理
        private void EditMenuMaskCheck()
        {
            UndoToolStripMenuItem.Enabled = rtTextArea.CanUndo;
            RedoToolStripMenuItem.Enabled = rtTextArea.CanRedo;
            CutToolStripMenuItem.Enabled = rtTextArea.SelectionLength > 0;
            CopyToolStripMenuItem.Enabled = rtTextArea.SelectionLength > 0;
            PasteToolStripMenuItem.Enabled = Clipboard.GetDataObject().GetDataPresent(DataFormats.Rtf);
            DeleteToolStripMenuItem.Enabled = rtTextArea.SelectionLength > 0;
        }

        //元に戻す
        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.Undo();
        }

        //やり直し
        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.Redo();
        }

        //切り取り
        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.Cut();
        }

        //コピー
        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.Copy();
        }

        //貼り付け
        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.Paste();
        }

        //削除
        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.SelectedText = "";
        }

        //色
        private void ColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cdColor.ShowDialog() == DialogResult.OK)
            {
                rtTextArea.SelectionColor = cdColor.Color;
            }
        }

        //フォント
        private void FontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fdFont.ShowDialog() == DialogResult.OK)
            {
                rtTextArea.SelectionFont = fdFont.Font;
                rtTextArea.ForeColor = fdFont.Color;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            EditMenuMaskCheck();
        }

        //マスク呼び出し
        private void EditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditMenuMaskCheck();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
    }
}
