﻿namespace ProCP_App
{
    partial class inputForm
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
            this.numLuggages = new System.Windows.Forms.NumericUpDown();
            this.lblInfo = new System.Windows.Forms.Label();
            this.lblLuggages = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.numUpDownCapacity = new System.Windows.Forms.NumericUpDown();
            this.lblCapacity = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numLuggages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownCapacity)).BeginInit();
            this.SuspendLayout();
            // 
            // numLuggages
            // 
            this.numLuggages.Location = new System.Drawing.Point(38, 49);
            this.numLuggages.Margin = new System.Windows.Forms.Padding(2);
            this.numLuggages.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numLuggages.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numLuggages.Name = "numLuggages";
            this.numLuggages.Size = new System.Drawing.Size(87, 22);
            this.numLuggages.TabIndex = 0;
            this.numLuggages.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(34, 17);
            this.lblInfo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(182, 17);
            this.lblInfo.TabIndex = 1;
            this.lblInfo.Text = "Limitations of the simulation";
            // 
            // lblLuggages
            // 
            this.lblLuggages.AutoSize = true;
            this.lblLuggages.Location = new System.Drawing.Point(157, 49);
            this.lblLuggages.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLuggages.Name = "lblLuggages";
            this.lblLuggages.Size = new System.Drawing.Size(134, 17);
            this.lblLuggages.TabIndex = 5;
            this.lblLuggages.Text = "Amount of luggages";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(37, 212);
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(2);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(264, 34);
            this.btnSubmit.TabIndex = 9;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // numUpDownCapacity
            // 
            this.numUpDownCapacity.Location = new System.Drawing.Point(38, 99);
            this.numUpDownCapacity.Margin = new System.Windows.Forms.Padding(2);
            this.numUpDownCapacity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUpDownCapacity.Name = "numUpDownCapacity";
            this.numUpDownCapacity.Size = new System.Drawing.Size(87, 22);
            this.numUpDownCapacity.TabIndex = 10;
            this.numUpDownCapacity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblCapacity
            // 
            this.lblCapacity.AutoSize = true;
            this.lblCapacity.Location = new System.Drawing.Point(157, 101);
            this.lblCapacity.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCapacity.Name = "lblCapacity";
            this.lblCapacity.Size = new System.Drawing.Size(106, 17);
            this.lblCapacity.TabIndex = 11;
            this.lblCapacity.Text = "Capacity of cart";
            // 
            // inputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 257);
            this.Controls.Add(this.lblCapacity);
            this.Controls.Add(this.numUpDownCapacity);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.lblLuggages);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.numLuggages);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "inputForm";
            this.Text = "inputForm";
            ((System.ComponentModel.ISupportInitialize)(this.numLuggages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownCapacity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numLuggages;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label lblLuggages;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.NumericUpDown numUpDownCapacity;
        private System.Windows.Forms.Label lblCapacity;
    }
}
