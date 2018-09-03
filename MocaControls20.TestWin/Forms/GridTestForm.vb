Imports Moca.Win

Public Class GridTestForm

    Private Sub GridTestForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim lst As IList(Of GridRow)
        lst = New List(Of GridRow)
        Dim ii As Integer
        Dim row As GridRow
        row = New GridRow()
        lst.Add(row)
        ii += 1

        ModelGridView1.EnableHeadersVisualStyles = False
        ModelGridView1.AllowUserToAddRows = False
        Dim lstSort As New Moca.SortableBindingList(Of GridRow)(lst)
        ModelGridView1.DataSource = lstSort
        ModelGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        'ModelGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect
        'ModelGridView1.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect
        'ModelGridView1.SelectionMode = DataGridViewSelectionMode.ColumnHeaderSelect
        ModelGridView1.TransparentRowSelection = True
        ModelGridView1.DefaultCellStyle.Padding = New Padding(5)
        ModelGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None
        ModelGridView1.RowTemplate.Height = 60
        ModelGridView1.MultiSelect = False


        Dim dt2 As New DataTable
        'Dim dc As DataColumn
        dt2.Columns.Add("ID", GetType(Integer))
        dt2.Columns.Add("Name", GetType(String))
        dt2.Columns.Add("Day", GetType(DateTime))
        dt2.Rows.Add(1, "hoge1", DBNull.Value)
        dt2.Rows.Add(2, "hoge2", Now)
        dt2.Rows.Add(3, "hoge3", DBNull.Value)
        dt2.AcceptChanges()

        DataGridViewEx1.AutoGenerateColumns = False
        DataGridViewEx1.EnableHeadersVisualStyles = False
        DataGridViewEx1.AllowUserToAddRows = False
        DataGridViewEx1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataGridViewEx1.AllowUserToResizeColumns = True
        DataGridViewEx1.TransparentRowSelection = True
        DataGridViewEx1.DefaultCellStyle.Padding = New Padding(5)
        DataGridViewEx1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None
        DataGridViewEx1.DataSource = dt2
        DataGridViewEx1.Columns(2).Width = 100

        'Dim cp As ColumnProperties
    End Sub

    Public Class ColumnProperties

        Public Property DisplayName As System.ComponentModel.DisplayNameAttribute

        Public Property Caption As CaptionAttribute
        Public Property PropertyOrder As PropertyOrderAttribute
        Public Property ColumnStyle As ColumnStyleAttribute
        Public Property [ReadOnly] As System.ComponentModel.ReadOnlyAttribute
        Public Property Frozen As FrozenAttribute
        Public Property ValidateTypes As ValidateTypesAttribute
        Public Property AllowMerging As AllowMergingAttribute
        Public Property EditCondition As EditConditionAttribute
        Public Property HiddenColumn As HiddenColumnAttribute
        Public Property RequiredColumn As RequiredColumnAttribute

    End Class

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        DataGridViewEx1.AddNewCurrent()
    End Sub

    Private Sub btnAdd2_Click(sender As Object, e As EventArgs) Handles btnAdd2.Click
        DataGridViewEx1.AddNew()
    End Sub

    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        DataGridViewEx1.RemoveCurrent()
    End Sub

End Class

Public Class GridRow
    Inherits Moca.Win.RowModelBase


    <ColumnStyle(100, Format:="yyyy/MM/dd")>
    Public Property Day As DateTime?

    Public Property Note As String

End Class
