using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XKit
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        #region 对外操作----------------------------------------------------------------

        #endregion

        #region 对外属性----------------------------------------------------------------

        #endregion

        #region 内部操作----------------------------------------------------------------

        #endregion

        #region 数据成员----------------------------------------------------------------

        #endregion

        #region 控件事件----------------------------------------------------------------

        private void miFileExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void miBuildMicro_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Bin|*.bin|Bytes|*.bytes|All|*.*";
            var res = dlg.ShowDialog();
            if (res == DialogResult.OK)
            {
                MicroProgram.Build(dlg.FileName);
            }
        }

        private void miBuildComplie_Click(object sender, EventArgs e)
        {
            string src = @"C:\Users\XuXiang\Desktop\p.asm";
            string dst = @"C:\Users\XuXiang\Desktop\a.bin";
            string msg;
            int err = Compiler.Complie(src, dst, out msg);
            if (err == 0)
            {
                MessageBox.Show("Complie finished.");
            }
            else
            {
                MessageBox.Show(string.Format("error {0}\n{1}", err, msg));
            }
        }

        #endregion
    }
}
