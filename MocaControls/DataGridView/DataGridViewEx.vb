

Imports System.ComponentModel

Namespace Win

    Public Class DataGridViewEx
        Inherits DataGridView

#Region " Declare "

        'コンポーネント デザイナーで必要です。
        Private components As System.ComponentModel.IContainer

        ''' <summary>データバインダー</summary>
        Private _dataBinder As New DataBinder

        Private CAPTIONHEIGHT As Integer = 20
        Private BORDERWIDTH As Integer = 0

#Region " 列名編集用コード "

        ''' <summary>列の改行コード</summary>
        Public Const C_COLTITLE_CR As String = "\n"

#End Region
#Region " スタイル定義 "

        ''' <summary>グリッドのスタイル定義</summary>
        Private _designSettings As System.Configuration.ApplicationSettingsBase

#End Region
#Region " Events "

        ''' <summary>
        ''' グリッドの設定変更イベント
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks>
        ''' このイベントでグリッドの固定列、固定行を設定しても反映されません。<br/>
        ''' これは.NETの初期化タイミングの問題なので回避できません。<br/>
        ''' デザイン時に設定するか、フォームロード時に設定してください。
        ''' </remarks>
        Public Event GridSetting(ByVal sender As Object, ByVal e As DataGridSettingEventArgs)

        ''' <summary>
        ''' グリッドの列情報設定イベント
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Public Event GridColmnSetting(ByVal sender As Object, ByVal e As DataGridColmnSettingEventArgs)

#End Region
#End Region

#Region " コンストラクタ "

        ''' <summary>
        ''' デフォルトコンストラクタ
        ''' </summary>
        <System.Diagnostics.DebuggerNonUserCode()>
        Public Sub New()
            MyBase.New()

            'この呼び出しは、コンポーネント デザイナーで必要です。
            InitializeComponent()

            If DesignMode Then
                Return
            End If
            If Moca.Win.WinUtil.UserControlDesignMode Then
                Return
            End If

            ' セットアップ
            _setupGrid()
        End Sub

        'メモ: 以下のプロシージャはコンポーネント デザイナーで必要です。
        'コンポーネント デザイナーを使って変更できます。
        'コード エディターを使って変更しないでください。
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            components = New System.ComponentModel.Container()
        End Sub

#End Region

#Region " Overrides "

        'Component は、コンポーネント一覧に後処理を実行するために dispose をオーバーライドします。
        <System.Diagnostics.DebuggerNonUserCode()>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        Protected Overrides Sub OnCellEndEdit(e As DataGridViewCellEventArgs)
            MyBase.OnCellEndEdit(e)

            Dim cur As DataRowView = CurrentView()

            Dim value As Object = cur.Row.Item(e.ColumnIndex, DataRowVersion.Original)
            Dim newValue As Object = cur.Row.Item(e.ColumnIndex, DataRowVersion.Default)

            If IsDBNull(value) Then
                If Not IsDBNull(newValue) Then
                    If Styles.ContainsKey(Moca.StyleNames.Modify.ToString) Then
                        CurrentCell.Style.Font = New Font(Styles(Moca.StyleNames.Modify.ToString).Font.FontFamily,
                                                      CurrentCell.OwningColumn.InheritedStyle.Font.Size,
                                                      Styles(Moca.StyleNames.Modify.ToString).Font.Style)
                    End If
                    Return
                End If
            Else
                If value <> newValue Then
                    If Styles.ContainsKey(Moca.StyleNames.Modify.ToString) Then
                        CurrentCell.Style.Font = New Font(Styles(Moca.StyleNames.Modify.ToString).Font.FontFamily,
                                                      CurrentCell.OwningColumn.InheritedStyle.Font.Size,
                                                      Styles(Moca.StyleNames.Modify.ToString).Font.Style)
                    End If
                    Return
                End If
            End If

            If Styles.ContainsKey(Moca.StyleNames.Normal.ToString) Then
                Dim cellStyle As DataGridViewCellStyle
                cellStyle = Styles(Moca.StyleNames.Normal.ToString)
                If cellStyle IsNot Nothing Then
                    CurrentCell.Style.Font = New Font(cellStyle.Font.FontFamily,
                                                              cellStyle.Font.Size,
                                                              cellStyle.Font.Style)
                End If
            End If

            cur.CancelEdit()
        End Sub

        Protected Overrides Sub OnCellPainting(e As DataGridViewCellPaintingEventArgs)
            MyBase.OnCellPainting(e)

            If NewRowIndex = e.RowIndex Then
                Return
            End If

            Select Case e.ColumnIndex
                Case < 0
                    ' 行ヘッダー部
                    Select Case e.RowIndex
                        Case < 0
                            ' 行ヘッダーで列ヘッダー
                        Case Else
                            GetRow(e.RowIndex)

                            If GetRow(e.RowIndex).RowState = DataRowState.Unchanged Then
                                Return
                            End If

                            ' 行ヘッダーでデータ部
                            If (e.PaintParts And DataGridViewPaintParts.Background) = DataGridViewPaintParts.Background Then
                                '背景だけを描画する
                                Dim backParts As DataGridViewPaintParts = e.PaintParts And (DataGridViewPaintParts.Background Or DataGridViewPaintParts.SelectionBackground)
                                e.Paint(e.ClipBounds, backParts)

                                If RowEditImage IsNot Nothing Then
                                    Const paddingRight As Integer = 7
                                    Dim srcRect As Rectangle = New Rectangle(0, 0, RowEditImage.Width, RowEditImage.Height)
                                    Dim destPoints As Rectangle = New Rectangle(e.CellBounds.Left + e.CellBounds.Width - srcRect.Width - paddingRight, e.CellBounds.Height / 2 - srcRect.Height / 2 + e.CellBounds.Top - 1, srcRect.Width, srcRect.Height)
                                    e.Graphics.DrawImage(RowEditImage, destPoints, srcRect, GraphicsUnit.Pixel)
                                End If

                                '背景以外が描画されるようにする
                                Dim paintParts As DataGridViewPaintParts = e.PaintParts And Not backParts
                                'セルを描画する
                                e.Paint(e.ClipBounds, paintParts)

                                '描画が完了したことを知らせる
                                e.Handled = True
                            End If
                    End Select

                Case Else
                    ' データ部
                    Select Case e.RowIndex
                        Case < 0
                            ' 行ヘッダーで列データ
                        Case Else
                            'If Not rowStatus = Attr.Status Then
                            '    Dim style As DataGridViewCellStyle
                            '    style = Me.Styles(Moca.StyleNames.ReadOnly.ToString())
                            '    Me(e.ColumnIndex, e.RowIndex).Style.BackColor = style.BackColor
                            '    Me(e.ColumnIndex, e.RowIndex).Style.Font = style.Font
                            '    Me(e.ColumnIndex, e.RowIndex).Style.ForeColor = style.ForeColor
                            '    Me(e.ColumnIndex, e.RowIndex).Style.SelectionBackColor = style.SelectionBackColor
                            '    Me(e.ColumnIndex, e.RowIndex).Style.SelectionForeColor = style.SelectionForeColor
                            'End If
                    End Select
            End Select
        End Sub

        Protected Overrides Sub OnCellEnter(e As DataGridViewCellEventArgs)
            MyBase.OnCellEnter(e)

            ImeMode = ImeMode.NoControl

            Dim column As DataGridViewColumn = Columns(e.ColumnIndex)
            Dim dbCol As DataColumn
            dbCol = CType(column.Tag, DataColumn)
            If dbCol Is Nothing Then
                Return
            End If

            If DBInfoColumns Is Nothing Then
                Return
            End If
            If Not DBInfoColumns.ContainsKey(dbCol.ColumnName) Then
                Return
            End If

            Dim info As Moca.Db.DbInfoColumn
            info = DBInfoColumns.Item(dbCol.ColumnName)
            If Not info.Typ.Equals("nvarchar") AndAlso Not info.Typ.Equals("nchar") Then
                Return
            End If

            ImeMode = ImeMode.Hiragana
        End Sub

#End Region

#Region " Property "

        ''' <summary>
        ''' データバインダー
        ''' </summary>
        ''' <returns></returns>
        <Browsable(False)>
        Public ReadOnly Property DataBinder As DataBinder
            Get
                Return _dataBinder
            End Get
        End Property

        ''' <summary>
        ''' エンティティコレクション
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Browsable(False)>
        Public Shadows Property DataSource() As Object
            Get
                Return _dataBinder.DataSource
            End Get
            Set(value As Object)
                _dataBinder.DataSource = value

                ' データからグリッドの設定
                _setData(value)
                ' セル位置設定
                _dataBinder.Position = 0
            End Set
        End Property

        ''' <summary>
        ''' グリッドデザイン設定
        ''' </summary>
        ''' <returns></returns>
        <Browsable(False)>
        Public Property DesignSettings As System.Configuration.ApplicationSettingsBase
            Get
                Return _designSettings
            End Get
            Set(value As System.Configuration.ApplicationSettingsBase)
                If value Is Nothing Then
                    _designSettings = Moca.GridDesignSettings.Default
                Else
                    _designSettings = value
                End If
                _setStyleNames()
            End Set
        End Property

        ''' <summary>
        ''' 各種セルスタイル
        ''' </summary>
        ''' <returns></returns>
        Public Property Styles As IDictionary(Of String, DataGridViewCellStyle)
            Get
                If _styles Is Nothing Then
                    _styles = New Dictionary(Of String, DataGridViewCellStyle)
                End If
                Return _styles
            End Get
            Set(value As IDictionary(Of String, DataGridViewCellStyle))
                _styles = value
            End Set
        End Property
        Private _styles As IDictionary(Of String, DataGridViewCellStyle)

        ''' <summary>
        ''' データ変更があるかどうか
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Browsable(False)>
        Public ReadOnly Property HasChanges As Boolean
            Get
                Return (Not GetChanges().Count.Equals(0))
            End Get
        End Property

#Region " Current "

        ''' <summary>
        ''' 現在行のDataSource項目を返す
        ''' </summary>
        ''' <returns></returns>
        Public Function Current() As DataRow
            If _dataBinder.BindSrc.DataSource Is Nothing Then
                Return Nothing
            End If
            Return DirectCast(_dataBinder.BindSrc.Current(), DataRowView).Row
        End Function

        Public Function CurrentView() As DataRowView
            If _dataBinder.BindSrc.DataSource Is Nothing Then
                Return Nothing
            End If
            Return _dataBinder.BindSrc.Current()
        End Function

#End Region
#Region " GetRow "

        Public Function GetRowView(ByVal index As Integer) As DataRowView
            Return Rows(index).DataBoundItem
        End Function

        Public Function GetRow(ByVal index As Integer) As DataRow
            Return GetRowView(index).Row
        End Function

#End Region
#Region " GetChanges "

        ''' <summary>
        ''' 変更行のみ返す
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overloads Function GetChanges() As IList(Of DataRow)
            Dim lst As IList(Of DataRow) = New List(Of DataRow)

            If _dataBinder.BindSrc.DataSource IsNot Nothing Then
                'lst = (From item As DataRow In CType(_dataBinder.BindSrc.DataSource, DataTable).Rows
                '       Where item.RowState <> DataRowState.Unchanged
                '       Select item).ToList
                For Each row As DataRow In CType(_dataBinder.BindSrc.DataSource, DataTable).Rows
                    If row.RowState <> DataRowState.Unchanged Then
                        lst.Add(row)
                    End If
                Next
            End If

            Return lst
        End Function

#End Region

        ''' <summary>
        ''' 垂直スクロールバーを常に表示するかどうか
        ''' </summary>
        ''' <returns></returns>
        Public Property VerticalScrollBarAlwaysShow As Boolean
            Get
                Return _verticalScrollBarAlwaysShow
            End Get
            Set(value As Boolean)
                _verticalScrollBarAlwaysShow = value
                Me.VerticalScrollBar.Visible = _verticalScrollBarAlwaysShow
            End Set
        End Property
        Private _verticalScrollBarAlwaysShow As Boolean

        ''' <summary>
        ''' 水平スクロールバーを常に表示するかどうか
        ''' </summary>
        ''' <returns></returns>
        Public Property HorizontalScrollBarAlwaysShow As Boolean
            Get
                Return _horizontalScrollBarAlwaysShow
            End Get
            Set(value As Boolean)
                _horizontalScrollBarAlwaysShow = value
                Me.HorizontalScrollBar.Visible = _horizontalScrollBarAlwaysShow
            End Set
        End Property
        Private _horizontalScrollBarAlwaysShow As Boolean

        ''' <summary>
        ''' 行が編集されたときに、行ヘッダー列へ表示するアイコン
        ''' </summary>
        ''' <returns></returns>
        Public Property RowEditImage As Image = My.Resources.RowEdit

        Public Property DBInfoColumns As Moca.Db.DbInfoColumnCollection

#End Region
#Region " Method "

        Protected Overridable Sub OnGridSetting(ByVal args As DataGridSettingEventArgs)
            RaiseEvent GridSetting(Me, args)
        End Sub

        ''' <summary>
        ''' グリッドの列情報設定イベント
        ''' </summary>
        ''' <param name="args"></param>
        Protected Overridable Sub OnGridColmnSetting(ByVal args As DataGridColmnSettingEventArgs)
            RaiseEvent GridColmnSetting(Me, args)
        End Sub

        ''' <summary>
        ''' グリッドのセットアップ
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub _setupGrid()
            ' デザイン時はここまで
            If Me.DesignMode Then
                Exit Sub
            End If

            Me.DoubleBuffered = True
            Me.Columns.Clear()

            AddHandler VerticalScrollBar.VisibleChanged, AddressOf _showScrollBars

            ' グリッドの設定変更イベント
            Dim args As New DataGridSettingEventArgs(Me)
            OnGridSetting(args)
        End Sub

        ''' <summary>
        ''' グリッドにデータソースを設定する
        ''' </summary>
        ''' <param name="value"></param>
        ''' <remarks></remarks>
        Private Sub _setData(ByVal value As DataTable)
            MyBase.DataSource = Nothing
            Columns.Clear()

            If value Is Nothing Then
                Return
            End If

            If DesignSettings Is Nothing Then
                DesignSettings = Moca.GridDesignSettings.Default
            End If

            ' 列設定（非表示、編集不可）
            _setCols(value)

            MyBase.DataSource = _dataBinder.BindSrc
        End Sub

        ''' <summary>
        ''' 列定義設定
        ''' </summary>
        Private Sub _setCols(ByVal value As DataTable)
            If AutoGenerateColumns Then
                Return
            End If
            If value.Columns.Count.Equals(0) Then
                Return
            End If

            For Each dbCol As DataColumn In value.Columns
                Dim col As DataGridViewColumn

                ' スタイル
                col = _setColStyle(dbCol)

                ' 読取専用
                _setColStyleReadOnly(dbCol, col)

                Dim args As New DataGridColmnSettingEventArgs(col, col.Index, dbCol)
                OnGridColmnSetting(args)
            Next
        End Sub

        ''' <summary>
        ''' 列のスタイル設定
        ''' </summary>
        ''' <param name="dbCol"></param>
        Private Function _setColStyle(ByVal dbCol As DataColumn) As DataGridViewColumn
            Dim col As DataGridViewColumn

            Dim caption As String

            caption = dbCol.ColumnName

            Dim colIndex As Integer

            col = _makeColumn(dbCol)
            col.HeaderText = _cnvColCaption(caption)
            col.DataPropertyName = dbCol.ColumnName
            col.Name = dbCol.ColumnName
            'col.DefaultCellStyle.Alignment = attr.Align
            col.SortMode = DataGridViewColumnSortMode.NotSortable
            'col.DefaultCellStyle.NullValue = attr.NullValue

            colIndex = Me.Columns.Add(col)

            ' とりあえず列のサイズは自動にする
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            col.Tag = dbCol

            Return col
        End Function

        ''' <summary>
        ''' 指定されたセル種別でDataGridViewColumnを作成する。
        ''' </summary>
        ''' <param name="dbCol"></param>
        ''' <returns></returns>
        Private Function _makeColumn(ByVal dbCol As DataColumn) As DataGridViewColumn
            Dim type As CellType = CellType.TextBox
            Dim col As DataGridViewColumn

            If dbCol.DataType.Equals(GetType(Image)) Then
                type = CellType.Image
            End If
            If dbCol.DataType.IsSubclassOf(GetType(Image)) Then
                type = CellType.Image
            End If
            If dbCol.DataType.Equals(GetType(Date)) Then
                type = CellType.Calendar
            End If
            If dbCol.DataType.Equals(GetType(Boolean)) Then
                type = CellType.CheckBox
            End If

            Select Case type
                Case CellType.Button
                    col = New DataGridViewButtonColumn()
                Case CellType.DisableButton
                    col = New DataGridViewDisableButtonColumn()
                Case CellType.CheckBox
                    col = New DataGridViewCheckBoxColumn()
            'Case CellType.CheckBoxImage
            '    col = New DataGridViewCheckBoxImageColumn()
            '    Dim img As DataGridViewCheckBoxImageColumn = col
            '    img.CheckedImage = My.Resources.Checked
            '    img.UnCheckedImage = My.Resources.UnChecked
                Case CellType.ComboBox
                    col = New DataGridViewComboBoxColumn()
                    Dim cbo As DataGridViewComboBoxColumn = DirectCast(col, DataGridViewComboBoxColumn)
                    ' 現在のセルしかコンボボックスが表示されないようにする
                    cbo.DisplayStyleForCurrentCellOnly = True
                    ' 編集モードの時だけコンボボックスを表示する
                    cbo.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing
                Case CellType.Image
                    col = New DataGridViewImageColumn()
                    Dim img As DataGridViewImageColumn = col
                    img.ImageLayout = DataGridViewImageCellLayout.Zoom
                Case CellType.Link
                    col = New DataGridViewLinkColumn()
                Case CellType.Calendar
                    col = New DataGridViewCalendarColumn()
                    Dim cal As DataGridViewCalendarColumn = DirectCast(col, DataGridViewCalendarColumn)
                    If DBInfoColumns IsNot Nothing Then
                        If DBInfoColumns.ContainsKey(dbCol.ColumnName) Then
                            Dim info As Moca.Db.DbInfoColumn
                            info = DBInfoColumns.Item(dbCol.ColumnName)
                            If info.Typ.Equals("date") Then
                                cal.PickerCustomFormat = "yyyy/MM/dd"
                            End If
                        End If
                    End If
                    'If String.IsNullOrEmpty(Attr.InputFormat) Then
                    '    If Not String.IsNullOrEmpty(Attr.Format) Then
                    '        cal.PickerCustomFormat = Attr.Format
                    '    End If
                    'Else
                    '    cal.PickerCustomFormat = Attr.InputFormat
                    'End If
                    col.DefaultCellStyle.DataSourceNullValue = DBNull.Value
                    col.DefaultCellStyle.NullValue = Nothing
                Case CellType.MaskedTextBox
                    col = New DataGridViewMaskedTextBoxColumn()
                    Dim mask As DataGridViewMaskedTextBoxColumn = DirectCast(col, DataGridViewMaskedTextBoxColumn)
                    'mask.Mask = Attr.InputFormat
                Case Else
                    col = New DataGridViewTextBoxColumn()
                    'If Attr.InputControl = TextBoxEx.InputFormatType.None Then
                    '    col = New DataGridViewTextBoxColumn()
                    'Else
                    '    col = New DataGridViewTextBoxExColumn()
                    '    Dim txt As DataGridViewTextBoxExColumn = DirectCast(col, DataGridViewTextBoxExColumn)
                    '    txt.InputFormat = Attr.InputControl
                    'End If
            End Select

            Return col
        End Function

        ''' <summary>
        ''' 各種コードを削除して正式な列名を返す。
        ''' </summary>
        ''' <param name="val"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function _cnvColCaption(ByVal val As String) As String
            Dim caption As String = val

            caption = caption.Replace(C_COLTITLE_CR, Environment.NewLine)

            Return caption
        End Function

        ''' <summary>
        ''' 読取専用列設定
        ''' </summary>
        ''' <param name="dbCol"></param>
        ''' <param name="col"></param>
        Private Sub _setColStyleReadOnly(ByVal dbCol As DataColumn, ByVal col As DataGridViewColumn)
            If Not dbCol.ReadOnly AndAlso Not col.ReadOnly Then
                Return
            End If

            SetColStyleReadOnly(col)
        End Sub

        ''' <summary>
        ''' 読取専用列設定
        ''' </summary>
        ''' <param name="col"></param>
        Public Sub SetColStyleReadOnly(ByVal col As DataGridViewColumn)
            col.ReadOnly = True

            Dim style As DataGridViewCellStyle
            style = Styles(StyleNames.ReadOnly.ToString)
            col.DefaultCellStyle.BackColor = style.BackColor
            col.DefaultCellStyle.Font = style.Font
            col.DefaultCellStyle.ForeColor = style.ForeColor
            col.DefaultCellStyle.SelectionBackColor = style.SelectionBackColor
            col.DefaultCellStyle.SelectionForeColor = style.SelectionForeColor
            col.DefaultCellStyle.Tag = style.Tag
        End Sub

        ''' <summary>
        ''' グリッドデザイン設定の設定
        ''' </summary>
        ''' <param name="key"></param>
        ''' <returns></returns>
        Private Function _getGridDesignSetting(ByVal key As String) As Object
            If _designSettings Is Nothing Then
                Return Nothing
            End If
            If _designSettings.Properties(key) Is Nothing Then
                Return Nothing
            End If
            Return _designSettings(key)
        End Function

        ''' <summary>
        ''' デザイン情報を構築する
        ''' </summary>
        Private Sub _setStyleNames()
            ' カスタムスタイルを作成します。
            Dim cs As DataGridViewCellStyle
            Dim spv As String
            spv = _getGridDesignSetting("StyleNames")
            If spv Is Nothing Then
                Return
            End If

            Dim names() As String = Nothing
            names = spv.Split(",")

            For Each name As String In names
                name = name.Trim

                If Me.Styles.Keys.Contains(name.Trim) Then
                    cs = Me.Styles(name)
                Else
                    cs = DefaultCellStyle.Clone()
                    Me.Styles.Add(name, cs)
                End If

                Dim val As Object
                val = _getGridDesignSetting(name & "BackColor")
                If val IsNot Nothing Then
                    cs.BackColor = val
                End If
                val = _getGridDesignSetting(name & "ForeColor")
                If val IsNot Nothing Then
                    cs.ForeColor = val
                End If
                val = _getGridDesignSetting(name & "Font")
                If val IsNot Nothing Then
                    cs.Font = val
                End If
                'val = _getGridDesignSetting(name & "BorderWidth")
                'If val IsNot Nothing Then
                '    cs.Border.Width = val
                'End If
                'val = _getGridDesignSetting(name & "BorderColor")
                'If val IsNot Nothing Then
                '    cs.Border.Color = val
                'End If
                'val = _getGridDesignSetting(name & "BorderStyle")
                'If val IsNot Nothing Then
                '    cs.Border.Style = val
                'End If
                'val = _getGridDesignSetting(name & "BorderDirection")
                'If val IsNot Nothing Then
                '    cs.Border.Direction = val
                'End If
                cs.Tag = name
            Next

            _setDefaultStyle()
        End Sub

        ''' <summary>
        ''' セルのデフォルトスタイルを設定する
        ''' </summary>
        Private Sub _setDefaultStyle()
            DefaultCellStyle = Styles(Moca.StyleNames.Normal.ToString)
        End Sub

        Private Sub _showScrollBars(sender As Object, e As EventArgs)
            If Not VerticalScrollBar.Visible Then
                Dim width As Integer = VerticalScrollBar.Width
                VerticalScrollBar.Location = New Point(Me.ClientRectangle.Width - width - BORDERWIDTH, 0)
                VerticalScrollBar.Size = New Size(width, Me.ClientRectangle.Height - CAPTIONHEIGHT - BORDERWIDTH + 4)
                HorizontalScrollBar.Size = New Size(HorizontalScrollBar.Size.Width - VerticalScrollBar.Size.Width, HorizontalScrollBar.Size.Height)
                VerticalScrollBar.Show()
            End If
        End Sub

#End Region

    End Class

    '
    Public Class DataGridSettingEventArgs
        Inherits EventArgs

        Private _TargetGrid As DataGridViewEx

        Public Sub New(ByVal grd As DataGridViewEx)
            _TargetGrid = grd
        End Sub

        '
        Public ReadOnly Property TargetGrid As DataGridViewEx
            Get
                Return _TargetGrid
            End Get
        End Property

    End Class

    '
    ' 概要:
    '     DataGridView の列情報設定イベント引数
    Public Class DataGridColmnSettingEventArgs
        Inherits EventArgs

        Public Sub New(ByVal column As DataGridViewColumn, ByVal index As Integer, ByVal dbColumn As DataColumn)
            _Column = column
            _Index = index
            _DBColumn = dbColumn
        End Sub

        Public ReadOnly Property Column As DataGridViewColumn

        Public ReadOnly Property Index As Integer

        Public ReadOnly Property DBColumn As DataColumn

    End Class

End Namespace
