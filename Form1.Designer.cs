namespace Lab_31_Danylko 
{ 
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dataGridViewProcesses;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.Button buttonKillProcess;
        private System.Windows.Forms.Button buttonLoadProcessInfo;
        private System.Windows.Forms.Button buttonShowThreadsModules;
        private System.Windows.Forms.RichTextBox richTextBoxProcessInfo;
        private System.Windows.Forms.SplitContainer splitContainer;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.dataGridViewProcesses = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.buttonExport = new System.Windows.Forms.Button();
            this.buttonKillProcess = new System.Windows.Forms.Button();
            this.buttonLoadProcessInfo = new System.Windows.Forms.Button();
            this.buttonShowThreadsModules = new System.Windows.Forms.Button();
            this.richTextBoxProcessInfo = new System.Windows.Forms.RichTextBox();

            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProcesses)).BeginInit();
            this.SuspendLayout();

            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.dataGridViewProcesses);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.richTextBoxProcessInfo);
            this.splitContainer.Panel2.Controls.Add(this.buttonShowThreadsModules);
            this.splitContainer.Panel2.Controls.Add(this.buttonLoadProcessInfo);
            this.splitContainer.Panel2.Controls.Add(this.buttonKillProcess);
            this.splitContainer.Panel2.Controls.Add(this.buttonExport);
            this.splitContainer.Panel2.Controls.Add(this.buttonRefresh);
            this.splitContainer.Panel2MinSize = 300;
            this.splitContainer.Size = new System.Drawing.Size(784, 461);
            this.splitContainer.SplitterDistance = 480;
            this.splitContainer.TabIndex = 0;

            // 
            // dataGridViewProcesses
            // 
            this.dataGridViewProcesses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewProcesses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewProcesses.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewProcesses.Name = "dataGridViewProcesses";
            this.dataGridViewProcesses.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewProcesses.Size = new System.Drawing.Size(480, 461);
            this.dataGridViewProcesses.TabIndex = 0;
            this.dataGridViewProcesses.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewProcesses_CellClick);

            // 
            // richTextBoxProcessInfo
            // 
            this.richTextBoxProcessInfo.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxProcessInfo.Name = "richTextBoxProcessInfo";
            this.richTextBoxProcessInfo.Size = new System.Drawing.Size(294, 200);
            this.richTextBoxProcessInfo.TabIndex = 1;
            this.richTextBoxProcessInfo.ReadOnly = true;
            this.richTextBoxProcessInfo.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;

            // 
            // buttonLoadProcessInfo
            // 
            this.buttonLoadProcessInfo.Location = new System.Drawing.Point(3, 209);
            this.buttonLoadProcessInfo.Name = "buttonLoadProcessInfo";
            this.buttonLoadProcessInfo.Size = new System.Drawing.Size(294, 23);
            this.buttonLoadProcessInfo.TabIndex = 2;
            this.buttonLoadProcessInfo.Text = "Load Process Info";
            this.buttonLoadProcessInfo.UseVisualStyleBackColor = true;
            this.buttonLoadProcessInfo.Click += new System.EventHandler(this.buttonLoadProcessInfo_Click);

            // 
            // buttonKillProcess
            // 
            this.buttonKillProcess.Location = new System.Drawing.Point(3, 238);
            this.buttonKillProcess.Name = "buttonKillProcess";
            this.buttonKillProcess.Size = new System.Drawing.Size(294, 23);
            this.buttonKillProcess.TabIndex = 3;
            this.buttonKillProcess.Text = "Kill Process";
            this.buttonKillProcess.UseVisualStyleBackColor = true;
            this.buttonKillProcess.Click += new System.EventHandler(this.buttonKillProcess_Click);

            // 
            // buttonShowThreadsModules
            // 
            this.buttonShowThreadsModules.Location = new System.Drawing.Point(3, 267);
            this.buttonShowThreadsModules.Name = "buttonShowThreadsModules";
            this.buttonShowThreadsModules.Size = new System.Drawing.Size(294, 23);
            this.buttonShowThreadsModules.TabIndex = 6;
            this.buttonShowThreadsModules.Text = "Show Threads and Modules";
            this.buttonShowThreadsModules.UseVisualStyleBackColor = true;
            this.buttonShowThreadsModules.Click += new System.EventHandler(this.buttonShowThreadsModules_Click);

            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Location = new System.Drawing.Point(3, 296);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(294, 23);
            this.buttonRefresh.TabIndex = 4;
            this.buttonRefresh.Text = "Refresh Processes";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);

            // 
            // buttonExport
            // 
            this.buttonExport.Location = new System.Drawing.Point(3, 325);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(294, 23);
            this.buttonExport.TabIndex = 5;
            this.buttonExport.Text = "Export Processes";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);

            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.splitContainer);
            this.Name = "Form1";
            this.Text = "Process Manager";

            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProcesses)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
