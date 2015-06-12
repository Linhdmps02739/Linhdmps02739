Imports System.Data.SqlClient
Public Class frmDangnhap

    Private Sub frmDangnhap_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnhuy_Click(sender As Object, e As EventArgs) Handles btnThoat.Click
        Application.Exit()
    End Sub

    Private Sub btndangnhap_Click(sender As Object, e As EventArgs) Handles btnDangnhap.Click
        Dim chuoiketnoi As String = "workstation id=Linhdmps02739.mssql.somee.com;packet size=4096;user id=Linhdmps02739_SQLLogin_1;pwd=9emh2jofux;data source=Linhdmps02739.mssql.somee.com;persist security info=False;initial catalog=Linhdmps02739"
        Dim KetNoi As SqlConnection = New SqlConnection(chuoiketnoi)
        Dim sqlAdapter As New SqlDataAdapter("select * from NhanVien where MaNhanVien='" & TextBox1.Text & "' and Password='" & TextBox2.Text & "' ", KetNoi)
        Dim tb As New DataTable

        Try
            KetNoi.Open()
            sqlAdapter.Fill(tb)
            If tb.Rows.Count > 0 Then
                MessageBox.Show("Kết nối thành công")
                frmMain.Show()
                Me.Hide()
            Else
                MessageBox.Show("Sai tài khoản hoặc mật khẩu")
            End If

        Catch ex As Exception

        End Try
    End Sub


End Class
