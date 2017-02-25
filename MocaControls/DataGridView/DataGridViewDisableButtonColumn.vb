
''' <summary>
''' グリッドのボタン制御処理
''' </summary>
''' <remarks></remarks>
Public Class DataGridViewDisableButtonColumn
    Inherits DataGridViewButtonColumn

    Public Sub New()
        Me.CellTemplate = New DataGridViewDisableButtonCell()
    End Sub
End Class
