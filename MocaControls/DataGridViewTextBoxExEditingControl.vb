
Imports System.ComponentModel

Namespace Win

    ''' <summary>
    ''' DataGridViewTextBoxExCell でホストされる
    ''' TextBoxEx コントロールを表します。
    ''' </summary>
    <ToolboxItem(False)>
    Public Class DataGridViewTextBoxExEditingControl
        Inherits TextBoxEx
        Implements IDataGridViewEditingControl

#Region " Declare "

        '編集コントロールが表示されているDataGridView
        Private _dataGridView As DataGridView
        '編集コントロールが表示されている行
        Private _rowIndex As Integer
        '編集コントロールの値とセルの値が違うかどうか
        Private _valueChanged As Boolean

#End Region

#Region " コンストラクタ "

        ''' <summary>
        ''' デフォルトコンストラクタ
        ''' </summary>
        Public Sub New()
            Me.BorderStyle = BorderStyle.None
        End Sub

#End Region

#Region " Implements "

#Region " Property "

        ''' <summary>
        ''' 編集するセルがあるDataGridView
        ''' </summary>
        ''' <returns></returns>
        Public Property EditingControlDataGridView As DataGridView Implements IDataGridViewEditingControl.EditingControlDataGridView
            Get
                Return Me._dataGridView
            End Get
            Set(value As DataGridView)
                Me._dataGridView = value
            End Set
        End Property

        ''' <summary>
        ''' 編集コントロールで変更されたセルの値
        ''' </summary>
        ''' <returns></returns>
        Public Property EditingControlFormattedValue As Object Implements IDataGridViewEditingControl.EditingControlFormattedValue
            Get
                Return Me.GetEditingControlFormattedValue(DataGridViewDataErrorContexts.Formatting)
            End Get
            Set(value As Object)
                Me.Text = DirectCast(value, String)
            End Set
        End Property

        ''' <summary>
        ''' 編集している行のインデックス
        ''' </summary>
        ''' <returns></returns>
        Public Property EditingControlRowIndex As Integer Implements IDataGridViewEditingControl.EditingControlRowIndex
            Get
                Return Me._rowIndex
            End Get
            Set(value As Integer)
                Me._rowIndex = value
            End Set
        End Property

        ''' <summary>
        ''' 値が変更されたかどうか
        ''' 編集コントロールの値とセルの値が違うかどうか
        ''' </summary>
        ''' <returns></returns>
        Public Property EditingControlValueChanged As Boolean Implements IDataGridViewEditingControl.EditingControlValueChanged
            Get
                Return Me._valueChanged
            End Get
            Set(value As Boolean)
                Me._valueChanged = value
            End Set
        End Property

        ''' <summary>
        ''' マウスカーソルがEditingPanel上にあるときのカーソルを指定する
        ''' EditingPanelは編集コントロールをホストするパネルで、
        ''' 編集コントロールがセルより小さいとコントロール以外の部分がパネルとなる
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property EditingPanelCursor As Cursor Implements IDataGridViewEditingControl.EditingPanelCursor
            Get
                Return MyBase.Cursor
            End Get
        End Property

        ''' <summary>
        ''' 値が変更した時に、セルの位置を変更するかどうか
        ''' 値が変更された時に編集コントロールの大きさが変更される時はTrue
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property RepositionEditingControlOnValueChange As Boolean Implements IDataGridViewEditingControl.RepositionEditingControlOnValueChange
            Get
                Return False
            End Get
        End Property

#End Region
#Region " Method "

        ''' <summary>
        ''' セルスタイルを編集コントロールに適用する
        ''' 編集コントロールの前景色、背景色、フォントなどをセルスタイルに合わせる
        ''' </summary>
        ''' <param name="dataGridViewCellStyle"></param>
        Public Sub ApplyCellStyleToEditingControl(dataGridViewCellStyle As DataGridViewCellStyle) Implements IDataGridViewEditingControl.ApplyCellStyleToEditingControl
            Me.Font = dataGridViewCellStyle.Font
            Me.ForeColor = dataGridViewCellStyle.ForeColor
            Me.BackColor = dataGridViewCellStyle.BackColor
            Select Case dataGridViewCellStyle.Alignment
                Case DataGridViewContentAlignment.BottomCenter,
                DataGridViewContentAlignment.MiddleCenter,
                DataGridViewContentAlignment.TopCenter

                    Me.TextAlign = HorizontalAlignment.Center
                    Exit Select
                Case DataGridViewContentAlignment.BottomRight,
                DataGridViewContentAlignment.MiddleRight,
                DataGridViewContentAlignment.TopRight

                    Me.TextAlign = HorizontalAlignment.Right
                    Exit Select
                Case Else
                    Me.TextAlign = HorizontalAlignment.Left
                    Exit Select
            End Select
        End Sub

        ''' <summary>
        ''' コントロールで編集する準備をする
        ''' テキストを選択状態にしたり、挿入ポインタを末尾にしたりする
        ''' </summary>
        ''' <param name="selectAll"></param>
        Public Sub PrepareEditingControlForEdit(selectAll As Boolean) Implements IDataGridViewEditingControl.PrepareEditingControlForEdit
            If selectAll Then
                '選択状態にする
                Me.SelectAll()
            Else
                '挿入ポインタを末尾にする
                Me.SelectionStart = Me.TextLength
            End If
        End Sub

        ''' <summary>
        ''' 指定されたキーをDataGridViewが処理するか、編集コントロールが処理するかTrueを返すと、編集コントロールが処理する
        ''' dataGridViewWantsInputKeyがTrueの時は、DataGridViewが処理できる
        ''' </summary>
        ''' <param name="keyData"></param>
        ''' <param name="dataGridViewWantsInputKey"></param>
        ''' <returns></returns>
        Public Function EditingControlWantsInputKey(keyData As Keys, dataGridViewWantsInputKey As Boolean) As Boolean Implements IDataGridViewEditingControl.EditingControlWantsInputKey
            'Keys.Left、Right、Home、Endの時は、Trueを返す
            'このようにしないと、これらのキーで別のセルにフォーカスが移ってしまう
            Select Case keyData And Keys.KeyCode
                Case Keys.Right, Keys.[End], Keys.Left, Keys.Home
                    Return True
                Case Else
                    Return Not dataGridViewWantsInputKey
            End Select
        End Function

        ''' <summary>
        ''' 編集コントロールで変更されたセルの値
        ''' </summary>
        ''' <param name="context"></param>
        ''' <returns></returns>
        Public Function GetEditingControlFormattedValue(context As DataGridViewDataErrorContexts) As Object Implements IDataGridViewEditingControl.GetEditingControlFormattedValue
            Return Me.Text
        End Function

#End Region

#End Region

#Region " Overrides "

        ''' <summary>
        ''' セルの内容が変更されているのを DataGridView に通知します。
        ''' </summary>
        ''' <param name="e"></param>
        Protected Overrides Sub OnEnter(e As EventArgs)
            MyBase.OnTextChanged(e)
            _valueChanged = True
            Me.EditingControlDataGridView.NotifyCurrentCellDirty(True)
        End Sub

#End Region

    End Class

End Namespace
