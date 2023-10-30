namespace Otopark_Otomasyonu
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.otoparkDBDataSet = new Otopark_Otomasyonu.otoparkDBDataSet();
            this.OtoparkGirisCikis_TBLBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.OtoparkGirisCikis_TBLTableAdapter = new Otopark_Otomasyonu.otoparkDBDataSetTableAdapters.OtoparkGirisCikis_TBLTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.otoparkDBDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OtoparkGirisCikis_TBLBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.OtoparkGirisCikis_TBLBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Otopark_Otomasyonu.Report1.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(800, 450);
            this.reportViewer1.TabIndex = 0;
            this.reportViewer1.Load += new System.EventHandler(this.reportViewer1_Load);
            // 
            // otoparkDBDataSet
            // 
            this.otoparkDBDataSet.DataSetName = "otoparkDBDataSet";
            this.otoparkDBDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // OtoparkGirisCikis_TBLBindingSource
            // 
            this.OtoparkGirisCikis_TBLBindingSource.DataMember = "OtoparkGirisCikis_TBL";
            this.OtoparkGirisCikis_TBLBindingSource.DataSource = this.otoparkDBDataSet;
            // 
            // OtoparkGirisCikis_TBLTableAdapter
            // 
            this.OtoparkGirisCikis_TBLTableAdapter.ClearBeforeFill = true;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.reportViewer1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.otoparkDBDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OtoparkGirisCikis_TBLBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource OtoparkGirisCikis_TBLBindingSource;
        private otoparkDBDataSet otoparkDBDataSet;
        private otoparkDBDataSetTableAdapters.OtoparkGirisCikis_TBLTableAdapter OtoparkGirisCikis_TBLTableAdapter;
    }
}