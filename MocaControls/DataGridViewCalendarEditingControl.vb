
Imports Moca.Win

Namespace Win

    ''' <summary>
    ''' <see cref="NullableDateTimePicker"/> の編集用コントロール
    ''' </summary>
    Public Class DataGridViewCalendarEditingControl
        Inherits NullableDateTimePicker
        Implements IDataGridViewEditingControl

#Region " Declare "

        Private _dataGridViewControl As DataGridView
        Private _valueIsChanged As Boolean = False
        Private _rowIndexNum As Integer

#End Region

#Region " コンストラクタ "

        ''' <summary>
        ''' デフォルトコンストラクタ
        ''' </summary>
        Public Sub New()
            ImeModeBase = ImeMode.Disable
            MinDate = DateTime.MinValue
            MaxDate = DateTime.MaxValue
            BorderStyle = ButtonBorderStyle.None
            BorderColor = Color.Transparent
        End Sub

#End Region

#Region " Implements IDataGridViewEditingControl "

#Region " Property "

        '''<summary>セルを格納する <see cref="T:System.Windows.Forms.DataGridView" /> を取得または設定します。</summary>
        '''<returns>編集対象の <see cref="T:System.Windows.Forms.DataGridViewCell" /> を格納する <see cref="T:System.Windows.Forms.DataGridView" />。関連付けられた <see cref="T:System.Windows.Forms.DataGridView" /> がない場合は null。</returns>
        Public Property EditingControlDataGridView As DataGridView Implements IDataGridViewEditingControl.EditingControlDataGridView
            Get
                Return _dataGridViewControl
            End Get
            Set(value As DataGridView)
                _dataGridViewControl = value
            End Set
        End Property

        '''<summary>エディタによって変更されるセルの書式設定された値を取得または設定します。</summary>
        '''<returns>セルの書式設定された値を表す <see cref="T:System.Object" />。</returns>
        Public Property EditingControlFormattedValue As Object Implements IDataGridViewEditingControl.EditingControlFormattedValue
            Get
                If Value Is Nothing Then
                    Return String.Empty
                End If

                Return GetEditingControlFormattedValue(DataGridViewDataErrorContexts.Formatting)
            End Get
            Set(value As Object)
                Try
                    Me.Value = DateTime.Parse(CStr(value))
                Catch
                    Me.Value = Nothing
                End Try
            End Set
        End Property

        '''<summary>ホストしているセルの親行のインデックスを取得または設定します。</summary>
        '''<returns>セルを含む行のインデックス。親行がない場合は -1。</returns>
        Public Property EditingControlRowIndex As Integer Implements IDataGridViewEditingControl.EditingControlRowIndex
            Get
                Return _rowIndexNum
            End Get
            Set(value As Integer)
                _rowIndexNum = value
            End Set
        End Property

        '''<summary>編集コントロールの値と、そのコントロールをホストしているセルの値とが異なるかどうかを示す値を取得または設定します。</summary>
        '''<returns>
        '''コントロールの値と、セルの値が異なる場合は true。それ以外の場合は false。</returns>
        Public Property EditingControlValueChanged As Boolean Implements IDataGridViewEditingControl.EditingControlValueChanged
            Get
                Return _valueIsChanged
            End Get
            Set(value As Boolean)
                _valueIsChanged = value
            End Set
        End Property

        '''<summary>マウス ポインタが編集コントロールの上ではなく、<see cref="P:System.Windows.Forms.DataGridView.EditingPanel" /> の上にあるときに使用されるカーソルを取得します。</summary>
        '''<returns>編集パネルに使用されるマウス ポインタを表す <see cref="T:System.Windows.Forms.Cursor" />。 </returns>
        Public ReadOnly Property EditingPanelCursor As Cursor Implements IDataGridViewEditingControl.EditingPanelCursor
            Get
                Return MyBase.Cursor
            End Get
        End Property

        '''<summary>値が変更されるたびに、セルの内容の位置を変更する必要があるかどうかを示す値を取得または設定します。</summary>
        '''<returns>
        '''内容の位置を変更する必要がある場合は true。それ以外の場合は false。</returns>
        Public ReadOnly Property RepositionEditingControlOnValueChange As Boolean Implements IDataGridViewEditingControl.RepositionEditingControlOnValueChange
            Get
                Return False
            End Get
        End Property

#End Region
#Region " Method "

        '''<summary>指定されたセル スタイルと矛盾しないように、コントロールのユーザー インターフェイス (UI) を変更します。</summary>
        '''<param name="dataGridViewCellStyle">UI のモデルとして使用する <see cref="T:System.Windows.Forms.DataGridViewCellStyle" />。</param>
        Public Sub ApplyCellStyleToEditingControl(dataGridViewCellStyle As DataGridViewCellStyle) Implements IDataGridViewEditingControl.ApplyCellStyleToEditingControl
            Me.Font = dataGridViewCellStyle.Font
            Me.CalendarForeColor = dataGridViewCellStyle.ForeColor
            Me.CalendarMonthBackground = dataGridViewCellStyle.BackColor
        End Sub

        '''<summary>現在選択されているセルの編集を準備します。</summary>
        '''<param name="selectAll">
        '''セルの内容をすべて選択する場合は true。それ以外の場合は false。</param>
        Public Sub PrepareEditingControlForEdit(selectAll As Boolean) Implements IDataGridViewEditingControl.PrepareEditingControlForEdit
            ' 特に処理無し
        End Sub

        '''<summary>指定されたキーが、編集コントロールによって処理される通常の入力キーか、<see cref="T:System.Windows.Forms.DataGridView" /> によって処理される特殊なキーであるかを確認します。</summary>
        '''<returns>
        '''指定されたキーが編集コントロールによって処理される通常の入力キーの場合は true。それ以外の場合は false。</returns>
        '''<param name="keyData">押されたキーを表す <see cref="T:System.Windows.Forms.Keys" />。</param>
        '''<param name="dataGridViewWantsInputKey">
        '''<paramref name="keyData" /> に格納された <see cref="T:System.Windows.Forms.Keys" /> を、<see cref="T:System.Windows.Forms.DataGridView" /> に処理させる場合は true。それ以外の場合は false。</param>
        Public Function EditingControlWantsInputKey(keyData As Keys, dataGridViewWantsInputKey As Boolean) As Boolean Implements IDataGridViewEditingControl.EditingControlWantsInputKey
            Select Case keyData And Keys.KeyCode
                Case Keys.Left, Keys.Up, Keys.Down, Keys.Right,
                Keys.Home, Keys.End, Keys.PageDown, Keys.PageUp, Keys.Delete
                    Return True

                Case Else
                    Return Not dataGridViewWantsInputKey
            End Select
        End Function

        '''<summary>セルの書式設定された値を取得します。</summary>
        '''<returns>セルの内容の書式設定されたバージョンを表す <see cref="T:System.Object" />。</returns>
        '''<param name="context">データが必要とされるコンテキストを指定する <see cref="T:System.Windows.Forms.DataGridViewDataErrorContexts" /> 値のビットごとの組み合わせ。</param>
        Public Function GetEditingControlFormattedValue(context As DataGridViewDataErrorContexts) As Object Implements IDataGridViewEditingControl.GetEditingControlFormattedValue
            If Value Is Nothing Then
                Return String.Empty
            End If

            Return CType(Value, DateTime).ToString(CustomFormat)
        End Function

#End Region

#End Region

#Region " Overrides "

        ''' <summary>
        ''' セルの内容が変更されているのを DataGridView に通知します。
        ''' </summary>
        ''' <param name="e"></param>
        Protected Overrides Sub OnEnter(e As EventArgs)
            _valueIsChanged = True
            Me.EditingControlDataGridView.NotifyCurrentCellDirty(True)
            MyBase.OnValueChanged(e)
        End Sub

#End Region

    End Class

End Namespace
