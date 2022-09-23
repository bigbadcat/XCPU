
namespace XKit
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.miFile = new System.Windows.Forms.ToolStripMenuItem();
            this.miFileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.miFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.miFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.miFileSplit1 = new System.Windows.Forms.ToolStripSeparator();
            this.miFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.miEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.miBuild = new System.Windows.Forms.ToolStripMenuItem();
            this.miEditCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.miEditCut = new System.Windows.Forms.ToolStripMenuItem();
            this.miEditPast = new System.Windows.Forms.ToolStripMenuItem();
            this.miBuildMicro = new System.Windows.Forms.ToolStripMenuItem();
            this.miBuildComplie = new System.Windows.Forms.ToolStripMenuItem();
            this.msMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // msMain
            // 
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miFile,
            this.miEdit,
            this.miBuild});
            this.msMain.Location = new System.Drawing.Point(0, 0);
            this.msMain.Name = "msMain";
            this.msMain.Size = new System.Drawing.Size(800, 25);
            this.msMain.TabIndex = 0;
            this.msMain.Text = "menuStrip1";
            // 
            // miFile
            // 
            this.miFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miFileNew,
            this.miFileOpen,
            this.miFileSave,
            this.miFileSplit1,
            this.miFileExit});
            this.miFile.Name = "miFile";
            this.miFile.Size = new System.Drawing.Size(39, 21);
            this.miFile.Text = "File";
            // 
            // miFileNew
            // 
            this.miFileNew.Name = "miFileNew";
            this.miFileNew.Size = new System.Drawing.Size(180, 22);
            this.miFileNew.Text = "New";
            // 
            // miFileOpen
            // 
            this.miFileOpen.Name = "miFileOpen";
            this.miFileOpen.Size = new System.Drawing.Size(180, 22);
            this.miFileOpen.Text = "Open";
            // 
            // miFileSave
            // 
            this.miFileSave.Name = "miFileSave";
            this.miFileSave.Size = new System.Drawing.Size(180, 22);
            this.miFileSave.Text = "Save";
            // 
            // miFileSplit1
            // 
            this.miFileSplit1.Name = "miFileSplit1";
            this.miFileSplit1.Size = new System.Drawing.Size(177, 6);
            // 
            // miFileExit
            // 
            this.miFileExit.Name = "miFileExit";
            this.miFileExit.Size = new System.Drawing.Size(180, 22);
            this.miFileExit.Text = "Exit";
            this.miFileExit.Click += new System.EventHandler(this.miFileExit_Click);
            // 
            // miEdit
            // 
            this.miEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miEditCopy,
            this.miEditCut,
            this.miEditPast});
            this.miEdit.Name = "miEdit";
            this.miEdit.Size = new System.Drawing.Size(42, 21);
            this.miEdit.Text = "Edit";
            // 
            // miBuild
            // 
            this.miBuild.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miBuildMicro,
            this.miBuildComplie});
            this.miBuild.Name = "miBuild";
            this.miBuild.Size = new System.Drawing.Size(49, 21);
            this.miBuild.Text = "Build";
            // 
            // miEditCopy
            // 
            this.miEditCopy.Name = "miEditCopy";
            this.miEditCopy.Size = new System.Drawing.Size(180, 22);
            this.miEditCopy.Text = "Copy";
            // 
            // miEditCut
            // 
            this.miEditCut.Name = "miEditCut";
            this.miEditCut.Size = new System.Drawing.Size(180, 22);
            this.miEditCut.Text = "Cut";
            // 
            // miEditPast
            // 
            this.miEditPast.Name = "miEditPast";
            this.miEditPast.Size = new System.Drawing.Size(180, 22);
            this.miEditPast.Text = "Past";
            // 
            // miBuildMicro
            // 
            this.miBuildMicro.Name = "miBuildMicro";
            this.miBuildMicro.Size = new System.Drawing.Size(180, 22);
            this.miBuildMicro.Text = "Build Micro";
            this.miBuildMicro.Click += new System.EventHandler(this.miBuildMicro_Click);
            // 
            // miBuildComplie
            // 
            this.miBuildComplie.Name = "miBuildComplie";
            this.miBuildComplie.Size = new System.Drawing.Size(180, 22);
            this.miBuildComplie.Text = "Compile";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.msMain);
            this.MainMenuStrip = this.msMain;
            this.Name = "MainForm";
            this.Text = "XCPU";
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip msMain;
        private System.Windows.Forms.ToolStripMenuItem miFile;
        private System.Windows.Forms.ToolStripMenuItem miFileNew;
        private System.Windows.Forms.ToolStripMenuItem miFileOpen;
        private System.Windows.Forms.ToolStripMenuItem miFileSave;
        private System.Windows.Forms.ToolStripSeparator miFileSplit1;
        private System.Windows.Forms.ToolStripMenuItem miFileExit;
        private System.Windows.Forms.ToolStripMenuItem miEdit;
        private System.Windows.Forms.ToolStripMenuItem miEditCopy;
        private System.Windows.Forms.ToolStripMenuItem miEditCut;
        private System.Windows.Forms.ToolStripMenuItem miEditPast;
        private System.Windows.Forms.ToolStripMenuItem miBuild;
        private System.Windows.Forms.ToolStripMenuItem miBuildMicro;
        private System.Windows.Forms.ToolStripMenuItem miBuildComplie;
    }
}

