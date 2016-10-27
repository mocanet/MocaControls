
Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim lst As IList(Of HogeRow)
        lst = New List(Of HogeRow)
        For ii As Integer = 0 To 20
            Dim row As New HogeRow With {
                .ID = ii,
                .Name = "テスト たろう" & ii,
                .Note = "",
                .Hoge = "test"}
            lst.Add(row)
        Next

        ModelGridView1.DataSource = lst
    End Sub

End Class
