
Imports Moca.Win

Public Class Form1

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
                .Cbo = "003"
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
                .Merge1 = "bbb",
                .Merge2 = "333",
                .Hoge = "test",
                .Cbo = "003"
        }
        lst.Add(row)

        'ModelGridView1.AllowUserToAddRows = False
        ModelGridView1.DataSource = lst

        'TextBoxEx2.InputFormat = TextBoxEx.InputFormatType.Number Or TextBoxEx.InputFormatType.Custom
        TextBoxEx2.InputFormat = TextBoxEx.InputFormatType.Custom
        TextBoxEx2.CustomChars = "*-"
    End Sub

    Private Sub ModelGridView1_DataError(sender As Object, e As DataGridViewDataErrorEventArgs)
        Debug.Print(e.Exception.ToString)
    End Sub

    Private Sub ModelGridView1_GridColmnSetting(sender As Object, e As GridColmnSettingEventArgs) Handles ModelGridView1.GridColmnSetting
        If Not e.Index.Equals(3) Then
            Return
        End If
        Dim cbo As DataGridViewComboBoxColumn = e.Column
        Dim dt As Moca.ConstantDataSet.ConstantDataTable
        dt = New Moca.ConstantDataSet("Hoge", True).Constant
        dt.AddRow("Hoge 1", "001")
        dt.AddRow("Hoge 2", "002")
        dt.AddRow("Hoge 3", "003")
        cbo.DataSource = dt
        ModelGridView1.SetComboBoxItems(e.Column, dt)
    End Sub

End Class
