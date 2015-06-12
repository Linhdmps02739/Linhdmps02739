Imports System.Data.SqlClient
Imports System.Data.DataTable

Public Class Quanlynhansu
    Dim database As New DataTable ' Tạo đối tượng database để lưu trữ dữ liệu từ Database Online
    'Tạo chuỗi kết nối để kết nối tới Database Online
    Dim chuoiconnect As String = "workstation id=Linhdmps02739.mssql.somee.com;packet size=4096;user id=Linhdmps02739_SQLLogin_1;pwd=9emh2jofux;data source=Linhdmps02739.mssql.somee.com;persist security info=False;initial catalog=Linhdmps02739"

    Private Sub Quanlynhansu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim connect As SqlConnection = New SqlConnection(chuoiconnect) ' Tạo đối tượng kết nối tới DB  Online
        ' Câu truy vấn để get dữ liệu
        Dim Query1 As SqlDataAdapter = New SqlDataAdapter("select * from NhanVien", connect)
        'Kết nối mở ra
        connect.Open()
        'Đổ dữ liệu vào đối tượng database
        Query1.Fill(database)
        'Hiển thị dữ liệu ra Datagridview
        dtgNhanvien.DataSource = database.DefaultView


    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles txtTennhanvien.TextChanged, txtMatkhau.TextChanged, txtManhanvien.TextChanged

    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub dtgnhanvien_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dtgNhanvien.CellContentClick
        Dim index As Integer = dtgnhanvien.CurrentCell.RowIndex
        txttennhanvien.Text = dtgnhanvien.Item(0, index).Value
        txtmanhanvien.Text = dtgnhanvien.Item(1, index).Value
        txtMatkhau.Text = dtgNhanvien.Item(2, index).Value


    End Sub

    Private Sub btnthem_Click(sender As Object, e As EventArgs) Handles btnThem.Click
        ' Tạo đối tượng kết nối
        Dim connect As SqlConnection = New SqlConnection(chuoiconnect)
        'Tạo query câu truy vấn Insert into
        Dim Query2 As String = "insert into NhanVien values (@MaNhanVien,@TenNhanVien,@Password)"
        'Tạo đối tượng để thực thi câu truy vấn với DB ONline
        Dim ExecuteQuery1 As New SqlCommand(Query2, connect)
        'Kết nối mở ra
        connect.Open()

        Try
            'Truyền giá trị trong các ô textbox cho các biến tương ứng
            ExecuteQuery1.Parameters.AddWithValue("@MaNhanVien", txtmanhanvien.Text)
            ExecuteQuery1.Parameters.AddWithValue("@TenNhanVien", txttennhanvien.Text)
            ExecuteQuery1.Parameters.AddWithValue("@Password", txtMatkhau.Text)


            'Exucute là ghi dữ liệu vào Database
            MessageBox.Show("Thêm thành công")
            ExecuteQuery1.ExecuteNonQuery()
        Catch ex As Exception
            'Nếu thêm không được thì hiển thị thông báo
            MessageBox.Show("Không thêm được!")

        End Try
        'Refresh bang
        Dim Query3 As SqlDataAdapter = New SqlDataAdapter("select * from NhanVien", connect)
        database.Clear()

        Query3.Fill(database)
        dtgnhanvien.DataSource = database.DefaultView


    End Sub
    Private Sub Loaddata()
        Dim connect As SqlConnection = New SqlConnection(chuoiconnect)
        Dim Query1 As SqlDataAdapter = New SqlDataAdapter("select * from NhanVien", connect)

        connect.Open()
        Query1.Fill(database)
        dtgnhanvien.DataSource = database.DefaultView

    End Sub

    Private Sub btnsua_Click(sender As Object, e As EventArgs) Handles btnSua.Click
        Dim Ketnoi1 As New SqlConnection(chuoiconnect)
        Ketnoi1.Open()
        Dim Stradd1 As String = "Update NhanVien set MaNhanVien=@MaNhanVien,TenNhanVien=@TenNhanVien,Password=@Password"
        Dim com As New SqlCommand(Stradd1, Ketnoi1)
        Try
            com.Parameters.AddWithValue("@MaNhanVien", txtmanhanvien.Text)
            com.Parameters.AddWithValue("@TenNhanVien", txttennhanvien.Text)
            com.Parameters.AddWithValue("@Password", txtMatkhau.Text)
            com.ExecuteNonQuery()
            Ketnoi1.Close()
            MessageBox.Show("Sửa thành công")
        Catch ex As Exception
            MessageBox.Show("Sửa không thành công")
        End Try
        database.Clear()
        dtgnhanvien.DataSource = database
        dtgnhanvien.DataSource = Nothing
        Loaddata()

    End Sub

    Private Sub btnxoa_Click(sender As Object, e As EventArgs) Handles btnXoa.Click
        Dim lienket As New SqlConnection(chuoiconnect)
        lienket.Open()
        Dim xoadl As String = "Delete from NhanVien where MaNhanVien=@MaNhanVien"
        Dim lenh As New SqlCommand(xoadl, lienket)
        Try
            lenh.Parameters.AddWithValue("@MaNhanVien", txtmanhanvien.Text)
            lenh.ExecuteNonQuery()
            lienket.Close()
        Catch ex As Exception
            MessageBox.Show("Xoá không thành công")
        End Try
        database.Clear()
        dtgnhanvien.DataSource = database
        dtgnhanvien.DataSource = Nothing
        Loaddata()


    End Sub

    Private Sub btnthoat_Click(sender As Object, e As EventArgs) Handles btnThoat.Click
        Me.Close()
    End Sub
End Class