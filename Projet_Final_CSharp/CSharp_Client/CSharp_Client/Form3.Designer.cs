namespace CSharp_Client
{
    partial class Form3
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.output_text = new System.Windows.Forms.TextBox();
            this.input_text = new System.Windows.Forms.TextBox();
            this.Send = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // output_text
            // 
            this.output_text.Enabled = false;
            this.output_text.Location = new System.Drawing.Point(5, 13);
            this.output_text.Multiline = true;
            this.output_text.Name = "output_text";
            this.output_text.Size = new System.Drawing.Size(726, 222);
            this.output_text.TabIndex = 0;
            // 
            // input_text
            // 
            this.input_text.Location = new System.Drawing.Point(5, 259);
            this.input_text.Multiline = true;
            this.input_text.Name = "input_text";
            this.input_text.Size = new System.Drawing.Size(581, 77);
            this.input_text.TabIndex = 1;
            // 
            // Send
            // 
            this.Send.Location = new System.Drawing.Point(609, 259);
            this.Send.Name = "Send";
            this.Send.Size = new System.Drawing.Size(122, 77);
            this.Send.TabIndex = 2;
            this.Send.Text = "Send";
            this.Send.UseVisualStyleBackColor = true;
            this.Send.Click += new System.EventHandler(this.Send_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(880, 348);
            this.Controls.Add(this.Send);
            this.Controls.Add(this.input_text);
            this.Controls.Add(this.output_text);
            this.Name = "Form3";
            this.Text = "Form3";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form3_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox output_text;
        private System.Windows.Forms.TextBox input_text;
        private System.Windows.Forms.Button Send;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}