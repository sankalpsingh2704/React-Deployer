using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

namespace ReactDeployer
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        Package package;

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
            this.path = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.load = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // path
            // 
            this.path.Location = new System.Drawing.Point(100, 35);
            this.path.Name = "path";
            this.path.Size = new System.Drawing.Size(355, 20);
            this.path.TabIndex = 0;
            this.path.Text = "D:\\GIT\\assessment-tool-new\\assessment-tool\\frontend\\";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Enter Path:";
            // 
            // load
            // 
            this.load.Location = new System.Drawing.Point(485, 33);
            this.load.Name = "load";
            this.load.Size = new System.Drawing.Size(75, 23);
            this.load.TabIndex = 2;
            this.load.Text = "Load";
            this.load.UseVisualStyleBackColor = true;
            this.load.Click += Load_Click;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.load);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.path);
            this.Name = "Form1";
            this.Text = "React Deployer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void Load_Click(object sender, EventArgs e)
        {
            try
            {
                using(var reader =  new StreamReader(this.path.Text+"package.json"))
                {
                    var json = reader.ReadToEnd();
                    package = JsonConvert.DeserializeObject<Package>(json);
                    Console.WriteLine(package.scripts["start"]);
                    this.createButtons();
                }
            }
            catch(Exception ex)
            {

            }
        }

        private void createButtons()
        {
            var btnList = package.scripts.Keys.ToList();
            foreach (var item in btnList)
            {
                var idx = btnList.IndexOf(item);
                Button dynamicButton = new Button();
                dynamicButton.Click += DynamicButton_Click;
                dynamicButton.Location = new Point(20, 150 + idx * 50);
                dynamicButton.Height = 30;
                dynamicButton.Width = 80;
                dynamicButton.Text = item.ToUpperInvariant();
                Controls.Add(dynamicButton);
            }
            
        }

        private void DynamicButton_Click(object sender, EventArgs e)
        {
            Process process;
            try
            {
                var button = sender as Button;
                string statement = "/c cd "+this.path.Text+" & npm run "+button.Text.ToLower();
                process = Process.Start("CMD.exe", statement);
            }
            catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
           
        }

        #endregion

        private TextBox path;
        private Label label1;
        private Button load;

        public class Package
        {
            public Dictionary<string,string> scripts;
        }
        
    }
}

