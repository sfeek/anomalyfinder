﻿namespace AnomalieFinder
{
    partial class AF
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AF));
            this.txtInput = new System.Windows.Forms.TextBox();
            this.txtResults = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSensitivity = new System.Windows.Forms.TextBox();
            this.lblSensitivity = new System.Windows.Forms.Label();
            this.lblInput = new System.Windows.Forms.Label();
            this.lblResults = new System.Windows.Forms.Label();
            this.chkMovingZ = new System.Windows.Forms.CheckBox();
            this.txtWindowSize = new System.Windows.Forms.TextBox();
            this.lblZSize = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(16, 36);
            this.txtInput.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtInput.MaxLength = 0;
            this.txtInput.Multiline = true;
            this.txtInput.Name = "txtInput";
            this.txtInput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtInput.Size = new System.Drawing.Size(265, 491);
            this.txtInput.TabIndex = 0;
            // 
            // txtResults
            // 
            this.txtResults.Location = new System.Drawing.Point(563, 36);
            this.txtResults.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtResults.MaxLength = 0;
            this.txtResults.Multiline = true;
            this.txtResults.Name = "txtResults";
            this.txtResults.ReadOnly = true;
            this.txtResults.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResults.Size = new System.Drawing.Size(265, 491);
            this.txtResults.TabIndex = 1;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(324, 36);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(200, 49);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSensitivity
            // 
            this.txtSensitivity.Location = new System.Drawing.Point(404, 110);
            this.txtSensitivity.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSensitivity.Name = "txtSensitivity";
            this.txtSensitivity.Size = new System.Drawing.Size(119, 22);
            this.txtSensitivity.TabIndex = 3;
            this.txtSensitivity.Text = "3.0";
            // 
            // lblSensitivity
            // 
            this.lblSensitivity.AutoSize = true;
            this.lblSensitivity.Location = new System.Drawing.Point(311, 113);
            this.lblSensitivity.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSensitivity.Name = "lblSensitivity";
            this.lblSensitivity.Size = new System.Drawing.Size(85, 17);
            this.lblSensitivity.TabIndex = 4;
            this.lblSensitivity.Text = "Z Threshold";
            // 
            // lblInput
            // 
            this.lblInput.AutoSize = true;
            this.lblInput.Location = new System.Drawing.Point(119, 11);
            this.lblInput.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInput.Name = "lblInput";
            this.lblInput.Size = new System.Drawing.Size(39, 17);
            this.lblInput.TabIndex = 5;
            this.lblInput.Text = "Input";
            // 
            // lblResults
            // 
            this.lblResults.AutoSize = true;
            this.lblResults.Location = new System.Drawing.Point(673, 11);
            this.lblResults.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblResults.Name = "lblResults";
            this.lblResults.Size = new System.Drawing.Size(55, 17);
            this.lblResults.TabIndex = 6;
            this.lblResults.Text = "Results";
            // 
            // chkMovingZ
            // 
            this.chkMovingZ.AutoSize = true;
            this.chkMovingZ.Location = new System.Drawing.Point(404, 217);
            this.chkMovingZ.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkMovingZ.Name = "chkMovingZ";
            this.chkMovingZ.Size = new System.Drawing.Size(88, 21);
            this.chkMovingZ.TabIndex = 7;
            this.chkMovingZ.Text = "Moving Z";
            this.chkMovingZ.UseVisualStyleBackColor = true;
            // 
            // txtWindowSize
            // 
            this.txtWindowSize.Location = new System.Drawing.Point(404, 245);
            this.txtWindowSize.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtWindowSize.Name = "txtWindowSize";
            this.txtWindowSize.Size = new System.Drawing.Size(119, 22);
            this.txtWindowSize.TabIndex = 8;
            this.txtWindowSize.Text = "5";
            // 
            // lblZSize
            // 
            this.lblZSize.AutoSize = true;
            this.lblZSize.Location = new System.Drawing.Point(304, 249);
            this.lblZSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblZSize.Name = "lblZSize";
            this.lblZSize.Size = new System.Drawing.Size(88, 17);
            this.lblZSize.TabIndex = 9;
            this.lblZSize.Text = "Window Size";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(351, 354);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(150, 49);
            this.btnClear.TabIndex = 10;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // AF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(849, 554);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.lblZSize);
            this.Controls.Add(this.txtWindowSize);
            this.Controls.Add(this.chkMovingZ);
            this.Controls.Add(this.lblResults);
            this.Controls.Add(this.lblInput);
            this.Controls.Add(this.lblSensitivity);
            this.Controls.Add(this.txtSensitivity);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtResults);
            this.Controls.Add(this.txtInput);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "AF";
            this.Text = "Anomaly Finder v1.52";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.TextBox txtResults;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSensitivity;
        private System.Windows.Forms.Label lblSensitivity;
        private System.Windows.Forms.Label lblInput;
        private System.Windows.Forms.Label lblResults;
        private System.Windows.Forms.CheckBox chkMovingZ;
        private System.Windows.Forms.TextBox txtWindowSize;
        private System.Windows.Forms.Label lblZSize;
        private System.Windows.Forms.Button btnClear;
    }
}

