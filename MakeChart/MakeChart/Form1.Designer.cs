namespace MakeChart
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.Add_button = new System.Windows.Forms.Button();
            this.fileListBox = new System.Windows.Forms.ListBox();
            this.Draw_Chart_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Add_button
            // 
            this.Add_button.Location = new System.Drawing.Point(338, 12);
            this.Add_button.Name = "Add_button";
            this.Add_button.Size = new System.Drawing.Size(97, 34);
            this.Add_button.TabIndex = 0;
            this.Add_button.Text = "Add";
            this.Add_button.UseVisualStyleBackColor = true;
            this.Add_button.Click += new System.EventHandler(this.Add_button_Click);
            // 
            // fileListBox
            // 
            this.fileListBox.FormattingEnabled = true;
            this.fileListBox.ItemHeight = 15;
            this.fileListBox.Location = new System.Drawing.Point(71, 12);
            this.fileListBox.Name = "fileListBox";
            this.fileListBox.Size = new System.Drawing.Size(218, 154);
            this.fileListBox.TabIndex = 1;
            // 
            // Draw_Chart_button
            // 
            this.Draw_Chart_button.Location = new System.Drawing.Point(338, 52);
            this.Draw_Chart_button.Name = "Draw_Chart_button";
            this.Draw_Chart_button.Size = new System.Drawing.Size(97, 34);
            this.Draw_Chart_button.TabIndex = 2;
            this.Draw_Chart_button.Text = "DrawChart";
            this.Draw_Chart_button.UseVisualStyleBackColor = true;
            this.Draw_Chart_button.Click += new System.EventHandler(this.Draw_Chart_button_click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(781, 550);
            this.Controls.Add(this.Draw_Chart_button);
            this.Controls.Add(this.fileListBox);
            this.Controls.Add(this.Add_button);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Add_button;
        private System.Windows.Forms.ListBox fileListBox;
        private System.Windows.Forms.Button Draw_Chart_button;
    }
}

