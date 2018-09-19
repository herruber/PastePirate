namespace PastePirate
{
    partial class Form1
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
            this.original = new System.Windows.Forms.TextBox();
            this.definitions = new System.Windows.Forms.TextBox();
            this.newCss = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // original
            // 
            this.original.Location = new System.Drawing.Point(12, 12);
            this.original.Multiline = true;
            this.original.Name = "original";
            this.original.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.original.Size = new System.Drawing.Size(296, 495);
            this.original.TabIndex = 0;
            this.original.TextChanged += new System.EventHandler(this.original_TextChanged);
            // 
            // definitions
            // 
            this.definitions.Location = new System.Drawing.Point(644, 301);
            this.definitions.Multiline = true;
            this.definitions.Name = "definitions";
            this.definitions.Size = new System.Drawing.Size(154, 206);
            this.definitions.TabIndex = 1;
            this.definitions.TextChanged += new System.EventHandler(this.definitions_TextChanged);
            // 
            // newCss
            // 
            this.newCss.Location = new System.Drawing.Point(314, 118);
            this.newCss.Multiline = true;
            this.newCss.Name = "newCss";
            this.newCss.Size = new System.Drawing.Size(226, 389);
            this.newCss.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(908, 519);
            this.Controls.Add(this.newCss);
            this.Controls.Add(this.definitions);
            this.Controls.Add(this.original);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox original;
        private System.Windows.Forms.TextBox definitions;
        private System.Windows.Forms.TextBox newCss;
    }
}

