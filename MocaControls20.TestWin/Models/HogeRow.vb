
Imports System.ComponentModel
Imports Moca.Win

Public Class HogeRow
    Inherits RowModelBase

    Public Property ID As String

    Public Property UserCode As String

    <DisplayName("名前")>
    <ColumnStyle(100, ImeMode:=ImeMode.Hiragana)>
    <Frozen()>
    Public Property Name As String

    <DisplayName("メールアドレス")>
    <ColumnStyle(200)>
    Public Property Mail As String

    <DisplayName("備考")>
    <ColumnStyle(200, ImeMode:=ImeMode.Hiragana)>
    Public Property Note As String

End Class
