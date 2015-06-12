Imports System.Data.SqlClient

Public Class Quanlykhachhang
    Dim database As New DataTable ' Tạo đối tượng database để lưu trữ dữ liệu từ Database Online
    'Tạo chuỗi kết nối để kết nối tới Database Online
    Dim chuoiconnect As String = "workstation id=Linhdmps02739.mssql.somee.com;packet size=4096;user id=Linhdmps02739_SQLLogin_1;pwd=9emh2jofux;data source=Linhdmps02739.mssql.somee.com;persist security info=False;initial catalog=Linhdmps02739"
    
    Private Sub QLKH_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim connect As SqlConnection = New SqlConnection(chuoiconnect) ' Tạo đối tượng kết nối tới DB  Online
        ' Câu truy vấn để get dữ liệu
        Dim Query1 As SqlDataAdapter = New SqlDataAdapter("select * from Khachhang", connect)
        'Kết nối mở ra
        connect.Open()
        'Đổ dữ liệu vào đối tượng database
        Query1.Fill(database)
        'Hiển thị dữ liệu ra Datagridview
        dtgKhachhang.DataSource = database.DefaultView
    End Sub

    Private Sub dtgkhachhang_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dtgKhachhang.CellContentClick
        Dim index As Integer = dtgKhachhang.CurrentCell.RowIndex
        txtMakhachhang.Text = dtgKhachhang.Item(0, index).Value
        txtTenkhachhang.Text = dtgKhachhang.Item(1, index).Value
        txtSodienthoai.Text = dtgKhachhang.Item(2, index).Value
        txtDiachi.Text = dtgKhachhang.Item(3, index).Value
    End Sub

    Private Sub btnthem_Click(sender As Object, e As EventArgs) Handles btnThem.Click
        ' Tạo đối tượng kết nối
        Dim connect As SqlConnection = New SqlConnection(chuoiconnect)
        'Tạo query câu truy vấn Insert into
        Dim Query2 As String = "insert into KhachHang values (@MaKhachHang,@TenKhachHang,@SoDienThoai,@DiaChi)"
        'Tạo đối tượng để thực thi câu truy vấn với DB ONline
        Dim ExecuteQuery1 As New SqlCommand(Query2, connect)
        'Kết nối mở ra
        connect.Open()

        Try
            'Truyền giá trị trong các ô textbox cho các biến tương ứng
            ExecuteQuery1.Parameters.AddWithValue("@MaKhachHang", txtMakhachhang.Text)
            ExecuteQuery1.Parameters.AddWithValue("@TenKhachHang", txtTenkhachhang.Text)
            ExecuteQuery1.Parameters.AddWithValue("@SoDienThoai", txtSodienthoai.Text)
            ExecuteQuery1.Parameters.AddWithValue("@DiaChi", txtDiachi.Text)

            'Exucute là ghi dữ liệu vào Database
            MessageBox.Show("Thêm thành công")
            ExecuteQuery1.ExecuteNonQuery()
        Catch ex As Exception
            'Nếu thêm không được thì hiển thị thông báo
            MessageBox.Show("Không thêm được!")

        End Try
        'Refresh bang
        Dim Query3 As SqlDataAdapter = New SqlDataAdapter("select * from KhachHang", connect)
        database.Clear()

        Query3.Fill(database)
        dtgKhachhang.DataSource = database.DefaultView

    End Sub
    Private Sub Loaddata()
        Dim connect As SqlConnection = New SqlConnection(chuoiconnect)
        Dim Query1 As SqlDataAdapter = New SqlDataAdapter("select * from KhachHang", connect)

        connect.Open()
        Query1.Fill(database)
        dtgKhachhang.DataSource = database.DefaultView
    End Sub

    Private Sub btnsua_Click(sender As Object, e As EventArgs) Handles btnSua.Click
        Dim Ketnoi1 As New SqlConnection(chuoiconnect)
        Ketnoi1.Open()
        Dim Stradd1 As String = "Update Khachhang set MaKhachHang=@MaKhachHang,SoDienThoai=@SoDienThoai,Diachi=@Diachi where MaKhachHang=@MaKhachHang"
        Dim com As New SqlCommand(Stradd1, Ketnoi1)
        Try
            com.Parameters.AddWithValue("@MaKhachHang", txtMakhachhang.Text)
            com.Parameters.AddWithValue("@TenKhachHang", txtTenkhachhang.Text)
            com.Parameters.AddWithValue("@SoDienThoai", txtSodienthoai.Text)
            com.Parameters.AddWithValue("@DiaChi", txtDiachi.Text)
            com.ExecuteNonQuery()
            Ketnoi1.Close()
            MessageBox.Show("Sửa thành công")
        Catch ex As Exception
            MessageBox.Show("Sửa không thành công")
        End Try
        database.Clear()
        dtgKhachhang.DataSource = database
        dtgKhachhang.DataSource = Nothing
        Loaddata()
    End Sub

    Private Sub btnxoa_Click(sender As Object, e As EventArgs) Handles btnXoa.Click
        Dim ketnoi As New SqlConnection(chuoiconnect)
        ketnoi.Open()
        Dim xoadl As String = "Delete from KhachHang where MaKhachHang=@MaKhachHang"
        Dim lenh As New SqlCommand(xoadl, ketnoi)
        Try
            lenh.Parameters.AddWithValue("@MaKhachHang", txtMakhachhang.Text)
            lenh.ExecuteNonQuery()
            ketnoi.Close()
        Catch ex As Exception
            MessageBox.Show("Xoá không thành công")
        End Try
        database.Clear()
        dtgKhachhang.DataSource = database
        dtgKhachhang.DataSource = Nothing
        Loaddata()
    End Sub

    Private Sub btnthoat_Click(sender As Object, e As EventArgs) Handles btnThoat.Click
        Me.Close()
    End Sub
End Class