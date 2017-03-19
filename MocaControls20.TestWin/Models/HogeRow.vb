
Imports System.ComponentModel
Imports Moca.Win

Public Class HogeRow
    Inherits RowModelBase

    <ColumnStyle(20)>
    <EditCondition(DataRowState.Added)>
    Public Property ID As String

    <ColumnStyle(80, CellType:=Moca.CellType.DisableButton)>
    <PropertyOrder(1)>
    Public ReadOnly Property Btn As String
        Get
            Return "Test"
        End Get
    End Property

    '<[ReadOnly](True)>
    <[ReadOnly](False)>
    <ColumnStyle(40, DataGridViewContentAlignment.MiddleCenter, CellType:=Moca.CellType.CheckBoxImage)>
    <PropertyOrder(0)>
    Public Property Selected As Boolean

    <ColumnStyle(100, RightBorderNone:=True,
                 InputControl:=TextBoxEx.InputFormatType.Number Or TextBoxEx.InputFormatType.Custom,
                 InputControlCustomChars:="-")>
    Public Property UserCode As String

    <ColumnStyle(50, CellType:=Moca.CellType.ComboBox)>
    Public Property Cbo As String

    <DisplayName("名前")>
    <ColumnStyle(100, ImeMode:=ImeMode.Hiragana)>
    <ValidateTypes(Moca.Util.ValidateTypes.LenghtMax)>
    <Frozen()>
    Public Property Name As String

    <ColumnStyle(100, CellType:=Moca.CellType.Link)>
    Public Property LinkText As String

    <ColumnStyle(50)>
    <AllowMerging()>
    Public Property Merge1 As String
    <ColumnStyle(50, DataGridViewContentAlignment.MiddleRight)>
    <AllowMerging()>
    Public Property Merge2 As String

    <DisplayName("メールアドレス")>
    <ColumnStyle(200)>
    Public Property Mail As String

    <ColumnStyle(100, Format:="yyyy/MM/dd")>
    Public Property Day As Date

    <DisplayName("画像")>
    <ColumnStyle(50, DataGridViewContentAlignment.MiddleCenter)>
    Public ReadOnly Property Img As Bitmap
        Get
            If Selected Then
                Return Moca.My.Resources.Checked
            Else
                Return Moca.My.Resources.UnChecked
            End If
        End Get
    End Property

    <DisplayName("備考")>
    <ColumnStyle(200, ImeMode:=ImeMode.Hiragana, WordWrap:=True)>
    Public Property Note As String

    <DisplayName("Hoge")>
    <ColumnStyle(100, DataGridViewContentAlignment.MiddleCenter)>
    <[ReadOnly](True)>
    Public Property Hoge As String

End Class
