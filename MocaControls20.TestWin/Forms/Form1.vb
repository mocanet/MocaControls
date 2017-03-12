
Imports Moca.Win

Public Class Form1

    Private dat As New Hoge2Row

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim lst As IList(Of HogeRow)
        lst = New List(Of HogeRow)
        Dim ii As Integer
        Dim row As HogeRow
        row = New HogeRow With {
                .ID = ii,
                .Name = "テスト たろう" & ii,
                .Note = "onaji",
                .Merge1 = "aaa",
                .Merge2 = "111",
                .Hoge = "test",
                .Cbo = "003",
                .LinkText = "http://www.hoge.com/"
        }
        lst.Add(row)
        ii += 1
        row = New HogeRow With {
                .ID = ii,
                .Name = "テスト たろう" & ii,
                .Note = "onaji",
                .Merge1 = "aaa",
                .Merge2 = "222",
                .Hoge = "test",
                .Cbo = "001"
        }
        lst.Add(row)
        ii += 1
        row = New HogeRow With {
                .ID = ii,
                .Name = "テスト たろう" & ii,
                .Note = "onaji",
                .Merge1 = "aaa",
                .Merge2 = "222",
                .Hoge = "test",
                .Cbo = "002"
        }
        lst.Add(row)
        ii += 1
        row = New HogeRow With {
                .ID = ii,
                .Name = "テスト たろう" & ii,
                .Note = "onaji",
                .Merge1 = "bbb",
                .Merge2 = "222",
                .Hoge = "test",
                .Cbo = "002"
        }
        lst.Add(row)
        ii += 1
        row = New HogeRow With {
                .ID = ii,
                .Name = "テスト たろう" & ii,
                .Note = "onaji",
                .Merge1 = "bbb",
                .Merge2 = "222",
                .Hoge = "test",
                .Cbo = "001"
        }
        lst.Add(row)
        ii += 1
        row = New HogeRow With {
                .ID = ii,
                .Name = "テスト たろう" & ii,
                .Note = "onaji",
                .Merge1 = "ccc",
                .Merge2 = "333",
                .Hoge = "test",
                .Cbo = "003"
        }
        lst.Add(row)

        ModelGridView1.AllowUserToAddRows = False
        ModelGridView1.DataSource = lst
        ModelGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        ModelGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect
        ModelGridView1.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect
        'ModelGridView1.SelectionMode = DataGridViewSelectionMode.ColumnHeaderSelect
        ModelGridView1.TransparentRowSelection = True
        ModelGridView1.DefaultCellStyle.Padding = New Padding(5)
        ModelGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None
        ModelGridView1.RowTemplate.Height = 60
        ModelGridView1.MultiSelect = False

        'TextBoxEx2.InputFormat = TextBoxEx.InputFormatType.Number Or TextBoxEx.InputFormatType.Custom
        TextBoxEx2.InputFormat = TextBoxEx.InputFormatType.Custom
        TextBoxEx2.CustomChars = "*-"

        Me.DoubleBuffered = True

        Dim dt As New DataTable
        dt.Columns.Add("ID", GetType(Integer))
        dt.Columns.Add("Name", GetType(String))
        dt.Rows.Add(1, "hoge1")
        dt.Rows.Add(10, "hoge10")

        ComboBoxEx1.DropDownStyle = ComboBoxStyle.DropDown
        ComboBoxEx1.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBoxEx1.DataSource = dt
        ComboBoxEx1.DisplayMember = "Name"
        ComboBoxEx1.ValueMember = "ID"

        Dim lst2 As New List(Of Hoge2Row)
        lst2.Add(dat)
        DataBinder1.DataSource = lst2
        DataBinder1.DataBinding(ComboBoxEx1, "Dat")

        Dim dt2 As New DataTable
        dt2.Columns.Add("ID", GetType(Integer))
        dt2.Columns.Add("Name", GetType(String))
        dt2.Columns.Add("Day", GetType(DateTime))
        dt2.Rows.Add(1, "hoge1", DBNull.Value)
        dt2.Rows.Add(2, "hoge2", DBNull.Value)
        dt2.Rows.Add(3, "hoge3", DBNull.Value)
        dt2.AcceptChanges()

        DataGridViewEx1.AutoGenerateColumns = False
        DataGridViewEx1.DataSource = dt2
        DataGridViewEx1.AllowUserToResizeColumns = True
        DataGridViewEx1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
    End Sub

    Private Sub ModelGridView1_DataError(sender As Object, e As DataGridViewDataErrorEventArgs)
        Debug.Print(e.Exception.ToString)
    End Sub

    Private Sub ModelGridView1_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles ModelGridView1.DataBindingComplete
        For Each row As DataGridViewRow In ModelGridView1.Rows
            If row.DataBoundItem Is Nothing Then
                Continue For
            End If
            Dim item As HogeRow = row.DataBoundItem
            Select Case item.ID
                Case "2", "3", "5"
                    CType(row.Cells(2), DataGridViewDisableButtonCell).Enabled = False
            End Select
        Next
    End Sub

    Private Sub ModelGridView1_GridColmnSetting(sender As Object, e As GridColmnSettingEventArgs) Handles ModelGridView1.GridColmnSetting
        Select Case e.Index
            Case 4
                Dim cbo As DataGridViewComboBoxColumn = e.Column
                Dim dt As Moca.ConstantDataSet.ConstantDataTable
                dt = New Moca.ConstantDataSet("Hoge", True).Constant
                dt.AddRow("Hoge 1", "001")
                dt.AddRow("Hoge 2", "002")
                dt.AddRow("Hoge 3", "003")
                cbo.DataSource = dt
                ModelGridView1.SetComboBoxItems(e.Column, dt)
            Case Else
        End Select
        ModelGridView1.ReadOnly = True
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Debug.Print(dat.Dat)
    End Sub

    Private Sub DataGridViewEx1_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles DataGridViewEx1.DataError
        Debug.Print(e.Exception.ToString)
    End Sub

    Private Sub ModelGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles ModelGridView1.CellContentClick
        Debug.Print("ModelGridView1_CellContentClick")
    End Sub

End Class
