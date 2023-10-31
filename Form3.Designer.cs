namespace Otopark_Otomasyonu
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
            this.components = new System.ComponentModel.Container();
            this.otoparkDBDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.otoparkDBDataSet = new Otopark_Otomasyonu.otoparkDBDataSet();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.txtPlaka = new System.Windows.Forms.TextBox();
            this.lblPlaka = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.otoparkDBDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.otoparkDBDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // otoparkDBDataSetBindingSource
            // 
            this.otoparkDBDataSetBindingSource.DataSource = this.otoparkDBDataSet;
            this.otoparkDBDataSetBindingSource.Position = 0;
            // 
            // otoparkDBDataSet
            // 
            this.otoparkDBDataSet.DataSetName = "otoparkDBDataSet";
            this.otoparkDBDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Otopark_Otomasyonu.Report2.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(800, 450);
            this.reportViewer1.TabIndex = 0;
            // 
            // txtPlaka
            // 
            this.txtPlaka.Location = new System.Drawing.Point(336, 62);
            this.txtPlaka.Name = "txtPlaka";
            this.txtPlaka.Size = new System.Drawing.Size(100, 20);
            this.txtPlaka.TabIndex = 1;
            // 
            // lblPlaka
            // 
            this.lblPlaka.AutoSize = true;
            this.lblPlaka.Location = new System.Drawing.Point(276, 65);
            this.lblPlaka.Name = "lblPlaka";
            this.lblPlaka.Size = new System.Drawing.Size(54, 13);
            this.lblPlaka.TabIndex = 2;
            this.lblPlaka.Text = "Plaka No:";
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblPlaka);
            this.Controls.Add(this.txtPlaka);
            this.Controls.Add(this.reportViewer1);
            this.Name = "Form3";
            this.Text = "Form3";
            this.Load += new System.EventHandler(this.Form3_Load);
            ((System.ComponentModel.ISupportInitialize)(this.otoparkDBDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.otoparkDBDataSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource otoparkDBDataSetBindingSource;
        private otoparkDBDataSet otoparkDBDataSet;
        private System.Windows.Forms.TextBox txtPlaka;
        private System.Windows.Forms.Label lblPlaka;
    }
}