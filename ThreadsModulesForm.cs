using System.Windows.Forms;

namespace Lab_31_Danylko
{
    public partial class ThreadsModulesForm : Form
    {
        private System.Windows.Forms.RichTextBox richTextBoxInfo;

        public ThreadsModulesForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.richTextBoxInfo = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // richTextBoxInfo
            // 
            this.richTextBoxInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxInfo.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxInfo.Name = "richTextBoxInfo";
            this.richTextBoxInfo.ReadOnly = true;
            this.richTextBoxInfo.Size = new System.Drawing.Size(800, 450);
            this.richTextBoxInfo.TabIndex = 0;
            this.richTextBoxInfo.Text = "";
            // 
            // ThreadsModulesForm
            // 
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.richTextBoxInfo);
            this.Name = "ThreadsModulesForm";
            this.Text = "Threads and Modules Information";
            this.ResumeLayout(false);
        }

        public void SetInfo(string info)
        {
            richTextBoxInfo.Text = info;
        }
    }
}