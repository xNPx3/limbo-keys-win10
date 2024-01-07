namespace WinFormsApp1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            keyPic = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)keyPic).BeginInit();
            SuspendLayout();
            // 
            // keyPic
            // 
            keyPic.BackColor = Color.Red;
            keyPic.Enabled = false;
            keyPic.Image = Properties.Resources.empty;
            keyPic.Location = new Point(0, 0);
            keyPic.Margin = new Padding(0);
            keyPic.Name = "keyPic";
            keyPic.Size = new Size(232, 232);
            keyPic.SizeMode = PictureBoxSizeMode.StretchImage;
            keyPic.TabIndex = 0;
            keyPic.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoValidate = AutoValidate.Disable;
            BackColor = SystemColors.Control;
            CausesValidation = false;
            ClientSize = new Size(230, 230);
            ControlBox = false;
            Controls.Add(keyPic);
            Enabled = false;
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form1";
            ShowIcon = false;
            ShowInTaskbar = false;
            SizeGripStyle = SizeGripStyle.Hide;
            Text = "Form1";
            TopMost = true;
            TransparencyKey = SystemColors.Control;
            ((System.ComponentModel.ISupportInitialize)keyPic).EndInit();
            ResumeLayout(false);
        }

        #endregion

        public PictureBox keyPic;
    }
}