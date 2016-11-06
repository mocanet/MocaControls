
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
                .Hoge = "test"
        }
        lst.Add(row)
        ii += 1
        row = New HogeRow With {
                .ID = ii,
                .Name = "テスト たろう" & ii,
                .Note = "onaji",
                .Merge1 = "aaa",
                .Merge2 = "222",
                .Hoge = "test"
        }
        lst.Add(row)
        ii += 1
        row = New HogeRow With {
                .ID = ii,
                .Name = "テスト たろう" & ii,
                .Note = "onaji",
                .Merge1 = "aaa",
                .Merge2 = "222",
                .Hoge = "test"
        }
        lst.Add(row)
        ii += 1
        row = New HogeRow With {
                .ID = ii,
                .Name = "テスト たろう" & ii,
                .Note = "onaji",
                .Merge1 = "bbb",
                .Merge2 = "222",
                .Hoge = "test"
        }
        lst.Add(row)
        ii += 1
        row = New HogeRow With {
                .ID = ii,
                .Name = "テスト たろう" & ii,
                .Note = "onaji",
                .Merge1 = "bbb",
                .Merge2 = "222",
                .Hoge = "test"
        }
        lst.Add(row)
        ii += 1
        row = New HogeRow With {
                .ID = ii,
                .Name = "テスト たろう" & ii,
                .Note = "onaji",
                .Merge1 = "bbb",
                .Merge2 = "333",
                .Hoge = "test"
        }
        lst.Add(row)

        ModelGridView1.DataSource = lst
    End Sub

End Class
