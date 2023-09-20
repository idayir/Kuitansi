<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form2
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim ReportDataSource1 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource()
        Me.ReportViewer1 = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.kuitansiDataSet = New Kuitansi.kuitansiDataSet()
        Me.kuitansitbBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.kuitansitbTableAdapter = New Kuitansi.kuitansiDataSetTableAdapters.kuitansitbTableAdapter()
        CType(Me.kuitansiDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.kuitansitbBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ReportViewer1
        '
        ReportDataSource1.Name = "DataSet1"
        ReportDataSource1.Value = Me.kuitansitbBindingSource
        Me.ReportViewer1.LocalReport.DataSources.Add(ReportDataSource1)
        Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "Kuitansi.Report1.rdlc"
        Me.ReportViewer1.Location = New System.Drawing.Point(12, 12)
        Me.ReportViewer1.Name = "ReportViewer1"
        Me.ReportViewer1.ServerReport.BearerToken = Nothing
        Me.ReportViewer1.Size = New System.Drawing.Size(766, 426)
        Me.ReportViewer1.TabIndex = 0
        '
        'kuitansiDataSet
        '
        Me.kuitansiDataSet.DataSetName = "kuitansiDataSet"
        Me.kuitansiDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'kuitansitbBindingSource
        '
        Me.kuitansitbBindingSource.DataMember = "kuitansitb"
        Me.kuitansitbBindingSource.DataSource = Me.kuitansiDataSet
        '
        'kuitansitbTableAdapter
        '
        Me.kuitansitbTableAdapter.ClearBeforeFill = True
        '
        'Form2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.ReportViewer1)
        Me.Name = "Form2"
        Me.Text = "Form2"
        CType(Me.kuitansiDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.kuitansitbBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ReportViewer1 As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents kuitansitbBindingSource As BindingSource
    Friend WithEvents kuitansiDataSet As kuitansiDataSet
    Friend WithEvents kuitansitbTableAdapter As kuitansiDataSetTableAdapters.kuitansitbTableAdapter
End Class
