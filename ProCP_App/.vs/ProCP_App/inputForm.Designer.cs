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
            this.numEmp = new System.Windows.Forms.NumericUpDown();
            this.numCarts = new System.Windows.Forms.NumericUpDown();
            this.lblLuggages = new System.Windows.Forms.Label();
            this.lblEmployees = new System.Windows.Forms.Label();
            this.lblCarts = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numLuggages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEmp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCarts)).BeginInit();
            this.SuspendLayout();
            // 
            // numLuggages
            // 
            this.numLuggages.Location = new System.Drawing.Point(38, 49);
            this.numLuggages.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
            // numEmp
            // 
            this.numEmp.Location = new System.Drawing.Point(38, 87);
            this.numEmp.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.numEmp.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numEmp.Name = "numEmp";
            this.numEmp.Size = new System.Drawing.Size(87, 22);
            this.numEmp.TabIndex = 3;
            this.numEmp.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numCarts
            // 
            this.numCarts.Location = new System.Drawing.Point(38, 129);
            this.numCarts.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.numCarts.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numCarts.Name = "numCarts";
            this.numCarts.Size = new System.Drawing.Size(87, 22);
            this.numCarts.TabIndex = 4;
            this.numCarts.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
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
            // lblEmployees
            // 
            this.lblEmployees.AutoSize = true;
            this.lblEmployees.Location = new System.Drawing.Point(151, 88);
            this.lblEmployees.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblEmployees.Name = "lblEmployees";
            this.lblEmployees.Size = new System.Drawing.Size(144, 17);
            this.lblEmployees.TabIndex = 7;
            this.lblEmployees.Text = "Amount of employees";
            // 
            // lblCarts
            // 
            this.lblCarts.AutoSize = true;
            this.lblCarts.Location = new System.Drawing.Point(151, 131);
            this.lblCarts.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCarts.Name = "lblCarts";
            this.lblCarts.Size = new System.Drawing.Size(107, 17);
            this.lblCarts.TabIndex = 8;
            this.lblCarts.Text = "Amount of carts";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(38, 166);
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(264, 34);
            this.btnSubmit.TabIndex = 9;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // inputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 228);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.lblCarts);
            this.Controls.Add(this.lblEmployees);
            this.Controls.Add(this.lblLuggages);
            this.Controls.Add(this.numCarts);
            this.Controls.Add(this.numEmp);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.numLuggages);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "inputForm";
            this.Text = "inputForm";
            ((System.ComponentModel.ISupportInitialize)(this.numLuggages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEmp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCarts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numLuggages;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.NumericUpDown numEmp;
        private System.Windows.Forms.NumericUpDown numCarts;
        private System.Windows.Forms.Label lblLuggages;
        private System.Windows.Forms.Label lblEmployees;
        private System.Windows.Forms.Label lblCarts;
        private System.Windows.Forms.Button btnSubmit;
    }
}
