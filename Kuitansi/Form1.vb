Imports System.Data.SQLite
Imports System.Data.OleDb

Public Class Form1
    Dim conn As SQLiteConnection
    Dim da As SQLiteDataAdapter
    Dim rd As SQLiteDataReader
    Dim cmd As SQLiteCommand

    Dim connA As OleDbConnection
    Dim daA As OleDbDataAdapter
    Dim rdA As OleDbDataReader
    Dim cmdA As OleDbCommand

    Dim ds As New DataSet
    Dim dt As New DataTable
    Dim str As String

    Dim appStartup As String = Application.StartupPath + "\kuitansi.db"
    Dim strconn As String = "Data Source = " & appStartup & ";Version = 3;"
    Dim startUpAccess = Application.StartupPath + "\kuitansi.accdb"
    Dim strConnAccess = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source = " & startUpAccess & ";Persist Security Info=False;"

    Sub TampilData()
        Try
            DataGridViewX1.DataSource = Nothing
            DataGridViewX1.Rows.Clear()
            Using conn As New SQLiteConnection(strconn)
                conn.Open()
                str = " select * from kuitansitb"
                da = New SQLiteDataAdapter(str, conn)
                ds = New DataSet
                da.Fill(ds)
                DataGridViewX1.DataSource = ds.Tables(0)
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Tampil Data")
        End Try
    End Sub

    Sub TampilDataAccess()
        Try
            DataGridViewX1.DataSource = Nothing
            DataGridViewX1.Rows.Clear()
            Using conna As New OleDbConnection(strConnAccess)
                conna.Open()
                str = " select * from kuitansitb"
                'daA = New OleDbDataAdapter(str, conna)
                'ds = New DataSet
                'daA.Fill(ds)
                'DataGridViewX1.DataSource = ds.Tables(0)

                cmdA = New OleDbCommand(str, conna)
                rdA = cmdA.ExecuteReader()
                While rdA.Read()
                    'MsgBox(rdA.Item(0).ToString)
                    DataGridViewX1.Rows.Add(rdA(0).ToString,
                                            rdA(1).ToString,
                                            rdA(2).ToString,
                                            rdA(3).ToString,
                                            rdA(4).ToString,
                                            rdA(5).ToString,
                                            rdA(6).ToString,
                                            rdA(7).ToString)
                End While
                rdA.Close()
                conna.Close()
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Tampil Data")
        End Try
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TampilDataAccess()

        HapusSemua()
    End Sub

    Private Sub ButtonX1_Click(sender As Object, e As EventArgs) Handles ButtonX1.Click
        KodeOtomatis()
        Aktif()
        ButtonX1.Enabled = False
        'TampilData()
    End Sub

    Public Function Terbilang(ByVal nilai As Long) As String
        Dim bilangan As String() = {"", "Satu", "Dua", "Tiga", "Empat", "Lima", "Enam", "Tujuh", "Delapan", "Sembilan", "Sepuluh", "Sebelas"}
        If nilai < 12 Then
            Return " " & bilangan(nilai)
        ElseIf nilai < 20 Then
            Return Terbilang(nilai - 10) & " Belas"
        ElseIf nilai < 100 Then
            Return (Terbilang(CInt((nilai \ 10))) & " Puluh") + Terbilang(nilai Mod 10)
        ElseIf nilai < 200 Then
            Return " Seratus" & Terbilang(nilai - 100)
        ElseIf nilai < 1000 Then
            Return (Terbilang(CInt((nilai \ 100))) & " Ratus") + Terbilang(nilai Mod 100)
        ElseIf nilai < 2000 Then
            Return " Seribu" & Terbilang(nilai - 1000)
        ElseIf nilai < 1000000 Then
            Return (Terbilang(CInt((nilai \ 1000))) & " Ribu") + Terbilang(nilai Mod 1000)
        ElseIf nilai < 1000000000 Then
            Return (Terbilang(CInt((nilai \ 1000000))) & " Juta") + Terbilang(nilai Mod 1000000)
        ElseIf nilai < 1000000000000 Then
            Return (Terbilang(CInt((nilai \ 1000000000))) & " Milyar") + Terbilang(nilai Mod 1000000000)
        ElseIf nilai < 1000000000000000 Then
            Return (Terbilang(CInt((nilai \ 1000000000000))) & " Trilyun") + Terbilang(nilai Mod 1000000000000)
        Else
            Return ""
        End If
    End Function

    Private Sub TextBoxX2_TextChanged(sender As Object, e As EventArgs) Handles TxtJumlah.TextChanged
        If TxtJumlah.Text = "" Then Exit Sub
        TxtTerbilang.Text = Terbilang(TxtJumlah.Text)
    End Sub

    Sub KodeOtomatis()
        Try
            Dim nomorakhir As Integer = 0

            Using conna As New OleDbConnection(strConnAccess)
                conna.Open()
                str = "select max(no_kuitansi) As no_akhir from kuitansitb"
                cmdA = New OleDbCommand(str, conna)
                rdA = cmdA.ExecuteReader()

                If rdA.Read Then

                    If IsDBNull(rdA("no_akhir")) Then
                        nomorakhir = 0
                    Else
                        nomorakhir = rdA("no_akhir")
                    End If
                End If
                rdA.Close()
                cmdA.Dispose()
            End Using

            '/---Ambil Nilai Bulan dan Tahun
            Dim bulan As String = Format(Now, "MM")
            Dim tahun As String = Format(Now, "yy")
            Dim nomer As String = "0"

            '/--- Nilai data ditambah 1
            nomorakhir = nomorakhir + 1
            ' MsgBox(nomorakhir)
            '/--- Membuat nilai Nol (0000)
            nomer = Microsoft.VisualBasic.Right(("0000" & nomorakhir), 4)

            '/--- Menggabungkan Nilai Bulan, Tahun dan Nomer
            LabelNoKuitansi.Text = bulan & tahun & nomer
            TextBoxX6.Text = LabelNoKuitansi.Text
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Kode Otomatis")
        End Try
    End Sub

    Private Sub TextBoxX1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtPemberi.KeyPress
        Dim i As Integer = TxtPemberi.SelectionStart
        TxtPemberi.Text = StrConv(TxtPemberi.Text, VbStrConv.ProperCase)
        TxtPemberi.SelectionStart = i
    End Sub

    Private Sub TextBoxX5_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtPenerima.KeyPress
        Dim i As Integer = TxtPenerima.SelectionStart
        TxtPenerima.Text = StrConv(TxtPenerima.Text, VbStrConv.ProperCase)
        TxtPenerima.SelectionStart = i
    End Sub

    Private Sub TextBoxX4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtNoteTambahan.KeyPress
        Dim i As Integer = TxtNoteTambahan.SelectionStart
        TxtNoteTambahan.Text = StrConv(TxtNoteTambahan.Text, VbStrConv.ProperCase)
        TxtNoteTambahan.SelectionStart = i
    End Sub

    Private Sub TextBoxX2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtJumlah.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub ButtonX2_Click(sender As Object, e As EventArgs) Handles ButtonX2.Click
        If TxtPemberi.Text = String.Empty Or
                TxtJumlah.Text = String.Empty Or
                TxtPenerima.Text = String.Empty Or
                TxtNote.Text = String.Empty Then
            MsgBox("Data Tidak Lengkap")
            Return
        End If
        Try
            Using conna As New OleDbConnection(strConnAccess)
                conna.Open()

                str = "Insert Into kuitansitb(no_kuitansi,nmpemberi, nmpenerima, jumlah, total, alasan, tambahan) values (
'" & LabelNoKuitansi.Text & "', '" & TxtPemberi.Text & "', '" & TxtPenerima.Text & "', '" & TxtTerbilang.Text & "','" & TxtJumlah.Text & "','" & TxtNote.Text & "', '" & TxtNoteTambahan.Text & "'  )"

                cmdA = New OleDbCommand(str, conna)
                cmdA.ExecuteNonQuery()
                MsgBox("Done")
                HapusSemua()
                TampilDataAccess()
                ButtonX1.Enabled = True
                conna.Close()
            End Using
        Catch ex As Exception
            MsgBox(ex.Message,, "Error Save")
        End Try
    End Sub

    Private Sub TextBoxX7_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtNote.KeyPress
        Dim i As Integer = TxtNote.SelectionStart
        TxtNote.Text = StrConv(TxtNote.Text, VbStrConv.ProperCase)
        TxtNote.SelectionStart = i
    End Sub

    Sub HapusSemua()
        LabelNoKuitansi.Text = String.Empty
        TxtPemberi.Clear()
        TxtJumlah.Clear()
        TxtTerbilang.Clear()
        TxtNoteTambahan.Clear()
        TxtPenerima.Clear()
        TextBoxX6.Clear()
        TxtNote.Clear()
        ButtonX2.Enabled = False
        ButtonX3.Enabled = False
        TxtPemberi.Enabled = False
        TxtJumlah.Enabled = False
        TxtTerbilang.Enabled = False
        TxtNoteTambahan.Enabled = False
        TxtPenerima.Enabled = False
        TextBoxX6.Enabled = False
        TxtNote.Enabled = False
    End Sub

    Private Sub ButtonX3_Click(sender As Object, e As EventArgs) Handles ButtonX3.Click
        HapusSemua()
        ButtonX1.Enabled = True
    End Sub
    Sub Aktif()
        ButtonX2.Enabled = True
        ButtonX3.Enabled = True
        TxtPemberi.Enabled = True
        TxtJumlah.Enabled = True
        TxtTerbilang.Enabled = True
        TxtNoteTambahan.Enabled = True
        TxtPenerima.Enabled = True
        TextBoxX6.Enabled = True
        TxtNote.Enabled = True
    End Sub

    Private Sub ButtonX4_Click(sender As Object, e As EventArgs) Handles ButtonX4.Click
        Form2.Show()
    End Sub

    Private Sub DataGridViewX1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridViewX1.CellClick
        Dim i, j As Integer
        i = DataGridViewX1.CurrentRow.Index
        LabelNoKuitansi.Text = DataGridViewX1.Item(1, i).Value
        TxtJumlah.Text = DataGridViewX1.Item(5, i).Value
        TxtPenerima.Text = DataGridViewX1.Item(3, i).Value
        TxtPemberi.Text = DataGridViewX1.Item(2, i).Value
        TxtTerbilang.Text = DataGridViewX1.Item(4, i).Value
        TxtNote.Text = DataGridViewX1.Item(6, i).Value
        TxtNoteTambahan.Text = DataGridViewX1.Item(7, i).Value
    End Sub
End Class
