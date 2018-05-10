
Imports System.ComponentModel

Namespace Win

    ''' <summary>
    ''' 選択状態を画像 <see cref="DataGridView"/> コントロールに表示します。
    ''' </summary>
    Public Class DataGridViewCheckBoxImageCell
        Inherits DataGridViewImageCell

        Public Sub New()
            Me.ImageLayout = DataGridViewImageCellLayout.Zoom
        End Sub

#Region " Overrides "

        Protected Overrides Function GetFormattedValue(value As Object, rowIndex As Integer, ByRef cellStyle As DataGridViewCellStyle, valueTypeConverter As TypeConverter, formattedValueTypeConverter As TypeConverter, context As DataGridViewDataErrorContexts) As Object
            Try
                Dim col As DataGridViewCheckBoxImageColumn = CType(OwningColumn, DataGridViewCheckBoxImageColumn)
                Me.ImageLayout = col.ImageLayout
                Dim val As Boolean = CBool(value)
                If val Then
                    Return col.CheckedImage
                Else
                    Return col.UnCheckedImage
                End If
            Catch ex As Exception
                Return MyBase.GetFormattedValue(value, rowIndex, cellStyle, valueTypeConverter, formattedValueTypeConverter, context)
            End Try
        End Function

        Protected Overrides Sub OnClick(e As DataGridViewCellEventArgs)
            MyBase.OnClick(e)

			If [ReadOnly] Then
				Return
			End If

			Dim val As Boolean = CBool(Value)
			val = Not val
            SetValue(e.RowIndex, val)
        End Sub

        Protected Overrides Sub OnMouseMove(e As DataGridViewCellMouseEventArgs)
            MyBase.OnMouseMove(e)

			If [ReadOnly] Then
				Return
			End If

			Me.DataGridView.Cursor = Cursors.Hand
		End Sub

        Protected Overrides Sub OnMouseLeave(rowIndex As Integer)
            MyBase.OnMouseLeave(rowIndex)

            Me.DataGridView.Cursor = Cursors.Default
        End Sub

#Region " Property "

        Public Overrides ReadOnly Property DefaultNewRowValue As Object
            Get
                Return Nothing
            End Get
        End Property

#End Region

#End Region

    End Class

End Namespace
