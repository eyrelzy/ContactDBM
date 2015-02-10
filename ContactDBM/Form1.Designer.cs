namespace ContactDBM
{
    partial class Form1
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.菜单ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.预处理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.printForm1 = new Microsoft.VisualBasic.PowerPacks.Printing.PrintForm(this.components);
            this.可视化关系图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.时间预处理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.可视化关系图ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.菜单ToolStripMenuItem,
            this.时间预处理ToolStripMenuItem,
            this.可视化关系图ToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(910, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 菜单ToolStripMenuItem
            // 
            this.菜单ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开ToolStripMenuItem,
            this.预处理ToolStripMenuItem,
            this.可视化关系图ToolStripMenuItem});
            this.菜单ToolStripMenuItem.Name = "菜单ToolStripMenuItem";
            this.菜单ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.菜单ToolStripMenuItem.Text = "菜单";
            // 
            // 打开ToolStripMenuItem
            // 
            this.打开ToolStripMenuItem.Name = "打开ToolStripMenuItem";
            this.打开ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.打开ToolStripMenuItem.Text = "打开";
            // 
            // 预处理ToolStripMenuItem
            // 
            this.预处理ToolStripMenuItem.Name = "预处理ToolStripMenuItem";
            this.预处理ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.预处理ToolStripMenuItem.Text = "预处理";
            this.预处理ToolStripMenuItem.Click += new System.EventHandler(this.预处理ToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // printForm1
            // 
            this.printForm1.DocumentName = "document";
            this.printForm1.Form = this;
            this.printForm1.PrintAction = System.Drawing.Printing.PrintAction.PrintToPrinter;
            this.printForm1.PrinterSettings = ((System.Drawing.Printing.PrinterSettings)(resources.GetObject("printForm1.PrinterSettings")));
            this.printForm1.PrintFileName = null;
            // 
            // 可视化关系图ToolStripMenuItem
            // 
            this.可视化关系图ToolStripMenuItem.Name = "可视化关系图ToolStripMenuItem";
            this.可视化关系图ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.可视化关系图ToolStripMenuItem.Text = "可视化关系图";
            this.可视化关系图ToolStripMenuItem.Click += new System.EventHandler(this.可视化关系图ToolStripMenuItem_Click);
            // 
            // 时间预处理ToolStripMenuItem
            // 
            this.时间预处理ToolStripMenuItem.Name = "时间预处理ToolStripMenuItem";
            this.时间预处理ToolStripMenuItem.Size = new System.Drawing.Size(80, 21);
            this.时间预处理ToolStripMenuItem.Text = "时间预处理";
            this.时间预处理ToolStripMenuItem.Click += new System.EventHandler(this.时间预处理ToolStripMenuItem_Click);
            // 
            // 可视化关系图ToolStripMenuItem1
            // 
            this.可视化关系图ToolStripMenuItem1.Name = "可视化关系图ToolStripMenuItem1";
            this.可视化关系图ToolStripMenuItem1.Size = new System.Drawing.Size(92, 21);
            this.可视化关系图ToolStripMenuItem1.Text = "可视化关系图";
            this.可视化关系图ToolStripMenuItem1.Click += new System.EventHandler(this.可视化关系图ToolStripMenuItem1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 490);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 菜单ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 预处理ToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private Microsoft.VisualBasic.PowerPacks.Printing.PrintForm printForm1;
        private System.Windows.Forms.ToolStripMenuItem 可视化关系图ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 时间预处理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 可视化关系图ToolStripMenuItem1;
    }
}

