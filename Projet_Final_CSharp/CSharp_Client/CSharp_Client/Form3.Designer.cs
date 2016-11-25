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
            this.input_text = new System.Windows.Forms.TextBox();
            this.Send = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.output_text = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // input_text
            // 
            this.input_text.Location = new System.Drawing.Point(12, 259);
            this.input_text.Multiline = true;
            this.input_text.Name = "input_text";
            this.input_text.Size = new System.Drawing.Size(574, 77);
            this.input_text.TabIndex = 1;
            // 
            // Send
            // 
            this.Send.Location = new System.Drawing.Point(608, 259);
            this.Send.Name = "Send";
            this.Send.Size = new System.Drawing.Size(122, 77);
            this.Send.TabIndex = 2;
            this.Send.Text = "Send";
            this.Send.UseVisualStyleBackColor = true;
            this.Send.Click += new System.EventHandler(this.Send_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(608, 343);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(117, 17);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "Press enter to send";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // output_text
            // 
            this.output_text.Location = new System.Drawing.Point(12, 32);
            this.output_text.Multiline = true;
            this.output_text.Name = "output_text";
            this.output_text.ReadOnly = true;
            this.output_text.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.output_text.Size = new System.Drawing.Size(719, 205);
            this.output_text.TabIndex = 4;
            this.output_text.WordWrap = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(749, 32);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(125, 304);
            this.textBox1.TabIndex = 6;
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(886, 376);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.output_text);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.Send);
            this.Controls.Add(this.input_text);
            this.Name = "Form3";
            this.Text = "Form3";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form3_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form3_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox input_text;
        private System.Windows.Forms.Button Send;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox output_text;
        private System.Windows.Forms.TextBox textBox1;
    }
}