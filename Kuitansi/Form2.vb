Public Class Form2
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'kuitansiDataSet.kuitansitb' table. You can move, or remove it, as needed.
        Me.kuitansitbTableAdapter.Fill(Me.kuitansiDataSet.kuitansitb)

        Me.ReportViewer1.RefreshReport()
    End Sub
End Class