namespace Newspaper_Selenium_Web_Driver_Automation
{
    partial class SauceDemoAutomation
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
            this.loginPage = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // loginPage
            // 
            this.loginPage.Location = new System.Drawing.Point(76, 51);
            this.loginPage.Name = "loginPage";
            this.loginPage.Size = new System.Drawing.Size(647, 43);
            this.loginPage.TabIndex = 1;
            this.loginPage.Text = "Login Page";
            this.loginPage.UseVisualStyleBackColor = true;
            this.loginPage.Click += new System.EventHandler(this.loginPage_Click);
            // 
            // SauceDemoAutomation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 139);
            this.Controls.Add(this.loginPage);
            this.Name = "SauceDemoAutomation";
            this.Text = "SauceDemo Automation";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button loginPage;
    }
}

