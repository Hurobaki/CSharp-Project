namespace CSharp_Client
{
    partial class Form2
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
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.createTopic_textbox = new System.Windows.Forms.TextBox();
            this.createTopic_button = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.CreateTopicWorker = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(167, 71);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Connect";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(67, 35);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(38, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(77, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(165, 23);
            this.label2.TabIndex = 3;
            this.label2.Text = "Select your chatroom :";
            // 
            // createTopic_textbox
            // 
            this.createTopic_textbox.Location = new System.Drawing.Point(41, 171);
            this.createTopic_textbox.Name = "createTopic_textbox";
            this.createTopic_textbox.Size = new System.Drawing.Size(100, 20);
            this.createTopic_textbox.TabIndex = 4;
            // 
            // createTopic_button
            // 
            this.createTopic_button.Location = new System.Drawing.Point(167, 171);
            this.createTopic_button.Name = "createTopic_button";
            this.createTopic_button.Size = new System.Drawing.Size(75, 23);
            this.createTopic_button.TabIndex = 5;
            this.createTopic_button.Text = "Create";
            this.createTopic_button.UseVisualStyleBackColor = true;
            this.createTopic_button.Click += new System.EventHandler(this.createTopic_button_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(64, 139);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(137, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Enter your chatroom name :";
            // 
            // CreateTopicWorker
            // 
            this.CreateTopicWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.CreateTopicWorker_DoWork);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 217);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.createTopic_button);
            this.Controls.Add(this.createTopic_textbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox createTopic_textbox;
        private System.Windows.Forms.Button createTopic_button;
        private System.Windows.Forms.Label label3;
        private System.ComponentModel.BackgroundWorker CreateTopicWorker;
    }
}