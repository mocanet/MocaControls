
Imports System.ComponentModel
Imports System.Drawing
Imports System.Reflection
Imports Moca.Db
Imports Moca.Util
Imports Moca.Win

Namespace Win

    ''' <summary>
    ''' DataGridView の操作を補助する拡張コントロール
    ''' </summary>
    <Description("DataGridView の操作を補助する拡張コントロール"),
     ToolboxItem(True),
     DesignTimeVisible(True)>
    Public Class ModelGridView
        Inherits DataGridView

#Region " Declare "

#Region " 列名編集用コード "

        ''' <summary>列の改行コード</summary>
        Public Const C_COLTITLE_CR As String = "\n"

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
        Public Event GridSetting(ByVal sender As Object, ByVal e As GridSettingEventArgs)

        ''' <summary>
        ''' グリッドの列情報設定イベント
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Public Event GridColmnSetting(ByVal sender As Object, ByVal e As GridColmnSettingEventArgs)

#End Region

#Region " スタイル定義 "

        ''' <summary>グリッドのスタイル定義</summary>
        Private _designSettings As System.Configuration.ApplicationSettingsBase

#End Region

        ''' <summary>エンティティタイプ</summary>
        Private _rowEntityType As Type

        ''' <summary>エンティティのプロパティたち</summary>
        Private _modelProps() As PropertyInfo
        Private _modelPropDic As IDictionary(Of String, PropertyInfo)

        ''' <summary>データバインダー</summary>
        Private _dataBinder As New DataBinder

        ''' <summary>削除対象データ</summary>
        Private _deletedRows As IList

        ''' <summary>スクロール固定列</summary>
        Private _frozen As Integer

        ''' <summary>テーブル定義</summary>
        Private _tblDef As New TableDefinition

        Private _editConditions As IDictionary(Of DataGridViewColumn, EditConditionAttribute)

#End Region

#Region " コンストラクタ "

        ''' <summary>
        ''' デフォルトコンストラクタ
        ''' </summary>
        Public Sub New()
            MyBase.New()

            If DesignMode Then
                Return
            End If

            ' セットアップ
            _setupGrid()
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
                    _designSettings = GridDesignSettings.Default
                Else
                    _designSettings = value
                End If
                _setStyleNames()
            End Set
        End Property

        ''' <summary>
        ''' グリッドの列定義列挙
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Browsable(False)>
        Public Property RowEntityType() As Type
            Get
                Return _rowEntityType
            End Get
            Set(value As Type)
                _rowEntityType = value
            End Set
        End Property

        ''' <summary>
        ''' データ変更があるかどうか
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Browsable(False)>
        Public ReadOnly Property HasChanges() As Boolean
            Get
                Return (Not GetChanges().Count.Equals(0))
            End Get
        End Property

        ''' <summary>
        ''' スクロール固定列位置
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property Frozen As Integer
            Get
                Return _frozen
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
                _deletedRows.Clear()

                _dataBinder.DataSource = value

                If Styles.Count.Equals(0) Then
                    _setStyleNames()
                End If

                ' データからグリッドの設定
                _setData(_dataBinder.BindSrc)
                ' セル位置設定
                _dataBinder.Position = 0
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

#Region " GetEntity "

        ''' <summary>
        ''' 指定された行番号の行データを指定されたエンティティへ格納して返す
        ''' </summary>
        ''' <typeparam name="T">格納するエンティティ</typeparam>
        ''' <param name="index">行番号</param>
        ''' <returns>エンティティ</returns>
        ''' <remarks></remarks>
        Public Overloads Function GetEntity(Of T)(ByVal index As Integer) As T
            Dim nowRow As T
            If index < 0 Then
                Return Nothing
            End If
            If NewRowIndex.Equals(index) Then
                Return Nothing
            End If
            If index >= RowCount Then
                Return Nothing
            End If
            nowRow = CType(Me.Rows(index).DataBoundItem, T)
            Return nowRow
        End Function

#End Region

        ''' <summary>
        ''' 行が編集されたときに、行ヘッダー列へ表示するアイコン
        ''' </summary>
        ''' <returns></returns>
        Public Property RowEditImage As Image = My.Resources.RowEdit

        <Category("行選択のカスタマイズ")>
        <Description("選択行に対して枠線を表示するように設定します")>
        Public Property TransparentRowSelection As Boolean
            Get
                Return _transparentRowSelection
            End Get
            Set(value As Boolean)
                _transparentRowSelection = value
            End Set
        End Property
        Private _transparentRowSelection As Boolean

        Public ReadOnly Property LastColumnDisplay As DataGridViewColumn
            Get
                Return Columns.GetLastColumn(DataGridViewElementStates.Displayed, DataGridViewElementStates.None)
            End Get
        End Property

#End Region

#Region " Overrides "

        '''<summary>
        '''Tab キー、Esc キー、Enter キー、方向キーなど、ダイアログ ボックスの制御に使用されるキーを処理します。
        '''</summary>
        '''<returns>
        '''キーが処理された場合は true。それ以外の場合は false。
        '''</returns>
        '''<param name="keyData">
        '''処理するキーを表す <see cref="T:System.Windows.Forms.Keys" /> 値のビットごとの組み合わせ。
        '''</param>
        '''<exception cref="T:System.InvalidCastException">
        '''コントロールを編集モードに移行させるキーが押されましたが、現在のセルの <see cref="P:System.Windows.Forms.DataGridViewCell.EditType" /> プロパティには、<see cref="T:System.Windows.Forms.IDataGridViewEditingControl" /> を実装する、<see cref="T:System.Windows.Forms.Control" /> の派生クラスが指定されていません。
        '''</exception>
        '''<exception cref="T:System.Exception">
        '''この操作を実行すると、セルの値がコミットされるか、編集モードに移行しますが、データ ソース内にエラーが存在するため、この操作を正常に実行できません。<see cref="E:System.Windows.Forms.DataGridView.DataError" /> イベントにハンドラが存在しないか、ハンドラによって <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException" /> プロパティが true に設定されています。
        '''</exception>
        <System.Security.Permissions.UIPermission(
        System.Security.Permissions.SecurityAction.Demand,
        Window:=System.Security.Permissions.UIPermissionWindow.AllWindows)>
        Protected Overrides Function ProcessDialogKey(ByVal keyData As Keys) As Boolean
            'Enterキーが押された時は、Tabキーが押されたようにする
            If (keyData And Keys.KeyCode) = Keys.Enter Then
                Return Me.ProcessTabKey(keyData)
            End If
            Return MyBase.ProcessDialogKey(keyData)
        End Function

        '''<summary>
        '''<see cref="T:System.Windows.Forms.DataGridView" /> での移動に使用されるキーを処理します。
        '''</summary>
        '''<returns>
        '''キーが処理された場合は true。それ以外の場合は false。
        '''</returns>
        '''<param name="e">
        '''押されたキーに関する情報を格納します。
        '''</param>
        '''<exception cref="T:System.InvalidCastException">
        '''コントロールを編集モードに移行させるキーが押されましたが、現在のセルの <see cref="P:System.Windows.Forms.DataGridViewCell.EditType" /> プロパティには、<see cref="T:System.Windows.Forms.IDataGridViewEditingControl" /> を実装する、<see cref="T:System.Windows.Forms.Control" /> の派生クラスが指定されていません。
        '''</exception>
        '''<exception cref="T:System.Exception">
        '''この操作を実行すると、セルの値がコミットされるか、編集モードに移行しますが、データ ソース内にエラーが存在するため、この操作を正常に実行できません。<see cref="E:System.Windows.Forms.DataGridView.DataError" /> イベントにハンドラが存在しないか、ハンドラによって <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException" /> プロパティが true に設定されています。
        '''
        '''または
        '''
        '''Del キーが押されると、1 つまたは複数の行が削除されますが、データ ソース内にエラーが存在するため、削除操作を正常に実行できません。<see cref="E:System.Windows.Forms.DataGridView.DataError" /> イベントにハンドラが存在しないか、ハンドラによって <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException" /> プロパティが true に設定されています。
        '''</exception>
        <System.Security.Permissions.SecurityPermission(
        System.Security.Permissions.SecurityAction.Demand,
        Flags:=System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode)>
        Protected Overrides Function ProcessDataGridViewKey(ByVal e As KeyEventArgs) As Boolean
            'Enterキーが押された時は、Tabキーが押されたようにする
            If e.KeyCode = Keys.Enter Then
                Return Me.ProcessTabKey(e.KeyCode)
            End If
            Return MyBase.ProcessDataGridViewKey(e)
        End Function

        Protected Overrides Function ProcessKeyPreview(ByRef m As Message) As Boolean
            Const WM_KEYDOWN As Integer = &H100
            Const WM_CHAR As Integer = &H102

            ' セル内でEnterキーを改行にする
            If Me.EditingControl IsNot Nothing _
                AndAlso Me.EditingControl.Visible _
                AndAlso TypeOf Me.EditingControl Is TextBox Then

                Dim txb As TextBox = CType(Me.EditingControl, TextBox)
                If (Not txb.ReadOnly) Then
                    Select Case m.Msg
                        Case WM_KEYDOWN
                            If (CType(m.WParam, Keys) = Keys.Return _
                                AndAlso Control.ModifierKeys = Keys.None) _
                                AndAlso txb.WordWrap Then
                                txb.SelectedText = System.Environment.NewLine
                                Return True
                            End If
                        Case WM_CHAR
                            If (CType(m.WParam, Keys) = Keys.Return _
                                AndAlso Control.ModifierKeys = Keys.Shift) Then
                                Return True
                            End If
                    End Select
                End If
            End If
            Return MyBase.ProcessKeyPreview(m)
        End Function

        '''<summary>
        '''<see cref="E:System.Windows.Forms.DataGridView.CellEndEdit" /> イベントを発生させます。
        '''</summary>
        '''<param name="e">
        '''イベント データを格納している <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" />。
        '''</param>
        '''<exception cref="T:System.ArgumentOutOfRangeException">
        '''<paramref name="e" /> の <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.ColumnIndex" /> プロパティの値が、コントロール内の列数 - 1 を超えています。
        '''
        '''または
        '''<paramref name="e" /> の <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.RowIndex" /> プロパティの値が、コントロール内の行数 - 1 を超えています。
        '''</exception>
        Protected Overrides Sub OnCellEndEdit(e As DataGridViewCellEventArgs)
            MyBase.OnCellEndEdit(e)

            Invalidate()

            Dim prop As PropertyInfo
            prop = CType(Columns(e.ColumnIndex).Tag, PropertyInfo)
            If prop Is Nothing Then
                Return
            End If

            Dim org As RowModelBase = Current(Of RowModelBase).GetOriginal()
            Dim cur As RowModelBase = Current()
            Dim value As Object = org.GetType().GetProperty(prop.Name).GetValue(org, Nothing)
            Dim newValue As Object = cur.GetType.GetProperty(prop.Name).GetValue(cur, Nothing)
            If value = newValue Then
                Return
            End If
            cur.Modify()
            If Styles.ContainsKey(StyleNames.Modify.ToString) Then
                CurrentCell.Style.Font = New Font(Styles(StyleNames.Modify.ToString).Font.FontFamily,
                                                  CurrentCell.OwningColumn.InheritedStyle.Font.Size,
                                                  Styles(StyleNames.Modify.ToString).Font.Style)
            End If
            CurrentRow.HeaderCell.ToolTipText = _getGridDesignSetting("ModifyToolTipText")

            If NewRowIndex < 0 Then
                Return
            End If
            If Not NewRowIndex.Equals(e.RowIndex + 1) Then
                Return
            End If

            cur.Add()
        End Sub

        Protected Overrides Sub OnCellLeave(e As DataGridViewCellEventArgs)
            MyBase.OnCellLeave(e)

            Invalidate()
        End Sub

        ''' <summary>
        ''' <see cref="DataGridView"/> コントロールで現在のセルが変更されたとき、またはこのコントロールが入力フォーカスを受け取ったとき
        ''' </summary>
        ''' <param name="e"></param>
        Protected Overrides Sub OnCellEnter(e As DataGridViewCellEventArgs)
            MyBase.OnCellEnter(e)

            Dim column As DataGridViewColumn = Columns(e.ColumnIndex)
            Dim prop As PropertyInfo
            prop = CType(column.Tag, PropertyInfo)
            If prop Is Nothing Then
                Return
            End If

            Dim attr As ColumnStyleAttribute
            attr = ClassUtil.GetCustomAttribute(Of ColumnStyleAttribute)(prop)
            If attr Is Nothing Then
                Return
            End If

            ImeMode = attr.ImeMode
        End Sub

        Protected Overrides Sub OnCellFormatting(e As DataGridViewCellFormattingEventArgs)
            If _transparentRowSelection Then
                e.CellStyle.SelectionBackColor = e.CellStyle.BackColor
                e.CellStyle.SelectionForeColor = e.CellStyle.ForeColor
            End If

            MyBase.OnCellFormatting(e)

            If e.Value IsNot Nothing Then
                Return
            End If
            If e.CellStyle.NullValue Is Nothing Then
                Return
            End If
            If Not e.CellStyle.NullValue.Equals("N/A") Then
                Return
            End If

            e.CellStyle.ForeColor = Color.DimGray
        End Sub

        ''' <summary>
        ''' マウス ポインタがセルに入る
        ''' </summary>
        ''' <param name="e"></param>
        Protected Overrides Sub OnCellMouseEnter(e As DataGridViewCellEventArgs)
            MyBase.OnCellMouseEnter(e)

            If TypeOf Cols(e.ColumnIndex) Is DataGridViewButtonColumn Then
                Dim btn As DataGridViewButtonColumn = Cols(e.ColumnIndex)
                If btn.FlatStyle = FlatStyle.Flat Then
                    Cursor = Cursors.Hand
                    ShowCellToolTips = False
                End If
            Else
                Cursor = Cursors.Default
                ShowCellToolTips = True
            End If
        End Sub

        ''' <summary>
        ''' マウス ポインタがセルを離れる
        ''' </summary>
        ''' <param name="e"></param>
        Protected Overrides Sub OnCellMouseLeave(e As DataGridViewCellEventArgs)
            MyBase.OnCellMouseLeave(e)

            If TypeOf Cols(e.ColumnIndex) Is DataGridViewButtonColumn Then
                Cursor = Cursors.Default
                ShowCellToolTips = True
            End If
        End Sub

        '''<summary>
        '''<see cref="E:System.Windows.Forms.DataGridView.CellPainting" /> イベントを発生させます。
        '''</summary>
        '''<param name="e">
        '''イベント データを格納している <see cref="T:System.Windows.Forms.DataGridViewCellPaintingEventArgs" />。
        '''</param>
        '''<exception cref="T:System.ArgumentOutOfRangeException">
        '''<paramref name="e" /> の <see cref="P:System.Windows.Forms.DataGridViewCellPaintingEventArgs.ColumnIndex" /> プロパティの値が、コントロール内の列数 - 1 を超えています。
        '''
        '''または
        '''<paramref name="e" /> の <see cref="P:System.Windows.Forms.DataGridViewCellPaintingEventArgs.RowIndex" /> プロパティの値が、コントロール内の行数 - 1 を超えています。
        '''</exception>
        Protected Overrides Sub OnCellPainting(e As DataGridViewCellPaintingEventArgs)
            MyBase.OnCellPainting(e)

            If NewRowIndex = e.RowIndex Then
                Return
            End If

            If TransparentRowSelection Then
                e.CellStyle.SelectionBackColor = e.CellStyle.BackColor
                e.CellStyle.SelectionForeColor = e.CellStyle.ForeColor
            End If

            Select Case e.ColumnIndex
                Case < 0
                    ' 行ヘッダー部
                    Select Case e.RowIndex
                        Case < 0
                            ' 行ヘッダーで列ヘッダー
                        Case Else
                            If DirectCast(Rows(e.RowIndex).DataBoundItem, RowModelBase).Status = DataRowState.Unchanged Then
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
                            e.PaintBackground(e.CellBounds, False)

                            Dim r2 As Rectangle = e.CellBounds
                            r2.Y += e.CellBounds.Height / 2
                            r2.Height = e.CellBounds.Height / 2
                            e.PaintContent(r2)
                            e.Handled = True
                        Case Else
                            If Not _editConditions.Count.Equals(0) Then
                                Dim col As DataGridViewColumn = Me.Cols(e.ColumnIndex)
                                If _editConditions.ContainsKey(col) Then
                                    Dim attr As EditConditionAttribute = _editConditions(col)
                                    Dim rowStatus = Me.GetEntity(Of RowModelBase)(e.RowIndex).Status
                                    If Not rowStatus = attr.Status AndAlso String.IsNullOrEmpty(e.ErrorText) Then
                                        Dim style As DataGridViewCellStyle
                                        style = Me.Styles(Moca.StyleNames.ReadOnly.ToString())
                                        Me(e.ColumnIndex, e.RowIndex).Style.BackColor = style.BackColor
                                        Me(e.ColumnIndex, e.RowIndex).Style.Font = style.Font
                                        Me(e.ColumnIndex, e.RowIndex).Style.ForeColor = style.ForeColor
                                        Me(e.ColumnIndex, e.RowIndex).Style.SelectionBackColor = style.SelectionBackColor
                                        Me(e.ColumnIndex, e.RowIndex).Style.SelectionForeColor = style.SelectionForeColor
                                    End If
                                End If
                            End If
                            If (SelectionMode = DataGridViewSelectionMode.CellSelect OrElse
                                SelectionMode = DataGridViewSelectionMode.RowHeaderSelect OrElse
                                SelectionMode = DataGridViewSelectionMode.ColumnHeaderSelect) AndAlso
                                TransparentRowSelection Then
                                If Not Rows(e.RowIndex).Selected AndAlso
                                        Not Cols(e.ColumnIndex).Selected AndAlso
                                        e.ColumnIndex = CurrentCell.ColumnIndex AndAlso e.RowIndex = CurrentCell.RowIndex Then
                                    e.Paint(e.CellBounds, DataGridViewPaintParts.All And Not DataGridViewPaintParts.Border)
                                    Dim borderWidth As Integer = 2
                                    Dim r As Rectangle = Me.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, False)
                                    Dim r2 As Rectangle = Me.GetColumnDisplayRectangle(e.ColumnIndex, False)
                                    Dim r2width As Integer = r2.X + r2.Width
                                    Dim rect = New Rectangle(r.X, r.Y, IIf(r.Width > r2width, r2width, r.Width), r.Height)
                                    ControlPaint.DrawBorder(e.Graphics, rect,
                                                                SystemColors.Highlight, borderWidth, ButtonBorderStyle.Solid,
                                                                SystemColors.Highlight, borderWidth, ButtonBorderStyle.Solid,
                                                                SystemColors.Highlight, borderWidth + 1, ButtonBorderStyle.Solid,
                                                                SystemColors.Highlight, borderWidth + 1, ButtonBorderStyle.Solid)

                                    e.Handled = True
                                End If
                            End If
                    End Select

                    Dim column As DataGridViewColumn = Columns(e.ColumnIndex)
                    Dim prop As PropertyInfo
                    prop = CType(column.Tag, PropertyInfo)
                    If prop IsNot Nothing Then
                        Dim attr As ColumnStyleAttribute
                        attr = ClassUtil.GetCustomAttribute(Of ColumnStyleAttribute)(prop)
                        If attr IsNot Nothing Then
                            If attr.RightBorderNone Then
                                e.AdvancedBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.None
                            End If
                        End If
                    End If

            End Select

            If (SelectionMode = DataGridViewSelectionMode.ColumnHeaderSelect) AndAlso
                TransparentRowSelection Then
                If Cols(e.ColumnIndex).Selected Then
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All And Not DataGridViewPaintParts.Border)
                    Dim borderWidth As Integer = 2
                    Dim r As Rectangle = Me.GetColumnDisplayRectangle(e.ColumnIndex, False)
                    Dim r2 As Rectangle = Me.DisplayRectangle()
                    Dim r3 As Rectangle = Me.GetRowDisplayRectangle(Rows.Count - 1, False)
                    Dim r2Height As Integer = IIf(r3.Bottom = 0, r.Height, r3.Bottom - r.Top)
                    Dim rect = New Rectangle(r.X, r.Y, r.Width, IIf(r2.Height > r2Height, r2Height, r2.Height))
                    ControlPaint.DrawBorder(e.Graphics, rect,
                                                SystemColors.Highlight, borderWidth, ButtonBorderStyle.Solid,
                                                SystemColors.Highlight, borderWidth, ButtonBorderStyle.Solid,
                                                SystemColors.Highlight, borderWidth + 1, ButtonBorderStyle.Solid,
                                                SystemColors.Highlight, borderWidth + 1, ButtonBorderStyle.Solid)

                    e.Handled = True
                End If
            End If
        End Sub

        '''<summary>
        '''<see cref="E:System.Windows.Forms.Control.Paint" /> イベントを発生させます。
        '''</summary>
        '''<param name="e">
        '''イベント データを格納している <see cref="T:System.Windows.Forms.PaintEventArgs" />。
        '''</param>
        '''<exception cref="T:System.Exception">
        '''次の例外を除き、このメソッドの実行中に発生した例外はすべて無視されます。
        '''<see cref="T:System.NullReferenceException" /><see cref="T:System.StackOverflowException" /><see cref="T:System.OutOfMemoryException" /><see cref="T:System.Threading.ThreadAbortException" /><see cref="T:System.ExecutionEngineException" /><see cref="T:System.IndexOutOfRangeException" /><see cref="T:System.AccessViolationException" /></exception>
        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            MyBase.OnPaint(e)

            ' 列マージ
            _mergeCellsInColumn(e.Graphics)
        End Sub

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="e"></param>
        Protected Overrides Sub OnCellBeginEdit(e As DataGridViewCellCancelEventArgs)
            MyBase.OnCellBeginEdit(e)

            If _editConditions.Count.Equals(0) Then
                Return
            End If

            Dim column As DataGridViewColumn = Me.Cols(e.ColumnIndex)
            If Not _editConditions.ContainsKey(column) Then
                Return
            End If

            Dim attr As EditConditionAttribute = _editConditions(column)
            Dim row As RowModelBase = Me.GetEntity(Of RowModelBase)(e.RowIndex)
            If row Is Nothing Then
                Return
            End If
            Dim rowStatus = row.Status
            If Not rowStatus = attr.Status Then
                e.Cancel = True
            End If
        End Sub

        ''' <summary>
        ''' スクロールが実行されたとき
        ''' </summary>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Protected Overrides Sub OnScroll(ByVal e As System.Windows.Forms.ScrollEventArgs)
            MyBase.OnScroll(e)

            Invalidate()
        End Sub

        ''' <summary>
        ''' サイズが変更されたとき
        ''' </summary>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Protected Overrides Sub OnSizeChanged(ByVal e As System.EventArgs)
            MyBase.OnSizeChanged(e)

            Invalidate()
        End Sub

        ''' <summary>
        ''' 列の幅が変更されたとき
        ''' </summary>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Protected Overrides Sub OnColumnWidthChanged(ByVal e As System.Windows.Forms.DataGridViewColumnEventArgs)
            MyBase.OnColumnWidthChanged(e)

            Invalidate()
        End Sub

        ''' <summary>
        ''' 行の境界線がダブルクリックされた時
        ''' </summary>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Protected Overrides Sub OnRowDividerDoubleClick(ByVal e As System.Windows.Forms.DataGridViewRowDividerDoubleClickEventArgs)
            MyBase.OnRowDividerDoubleClick(e)

            Invalidate()
        End Sub

        Protected Overrides Sub SetSelectedCellCore(columnIndex As Integer, rowIndex As Integer, selected As Boolean)
            MyBase.SetSelectedCellCore(columnIndex, rowIndex, selected)
            If selected Then
                _changeLinkSelectedStyle(Columns(columnIndex), rowIndex)
            Else
                _changeLinkUnSelectedStyle(Columns(columnIndex), rowIndex)
            End If
        End Sub

        Protected Overrides Sub SetSelectedRowCore(rowIndex As Integer, selected As Boolean)
            MyBase.SetSelectedRowCore(rowIndex, selected)

            Select Case SelectionMode
                Case DataGridViewSelectionMode.FullRowSelect
                    Return
                Case DataGridViewSelectionMode.RowHeaderSelect
            End Select

            For Each col As DataGridViewColumn In Columns
                If selected Then
                    _changeLinkSelectedStyle(col, rowIndex)
                Else
                    _changeLinkUnSelectedStyle(col, rowIndex)
                End If
            Next
        End Sub

        Protected Overrides Sub OnRowEnter(e As DataGridViewCellEventArgs)
            MyBase.OnRowEnter(e)

            Select Case SelectionMode
                Case DataGridViewSelectionMode.FullRowSelect
                    If SelectedRows.Count.Equals(0) Then
                        Return
                    End If
                    For Each col As DataGridViewColumn In Columns
                        _changeLinkSelectedStyle(col, e.RowIndex)
                    Next
            End Select
        End Sub

        Protected Overrides Sub OnRowLeave(e As DataGridViewCellEventArgs)
            MyBase.OnRowLeave(e)

            Select Case SelectionMode
                Case DataGridViewSelectionMode.FullRowSelect
                    If SelectedRows.Count.Equals(0) Then
                        Return
                    End If
                    For Each col As DataGridViewColumn In Columns
                        _changeLinkUnSelectedStyle(col, e.RowIndex)
                    Next
            End Select
        End Sub

        Protected Overrides Sub OnCellToolTipTextNeeded(e As DataGridViewCellToolTipTextNeededEventArgs)
            MyBase.OnCellToolTipTextNeeded(e)

            If e.ColumnIndex < 0 Then
                Return
            End If
            If e.RowIndex < 0 Then
                Return
            End If

            Dim errorText As String
            errorText = Me(e.ColumnIndex, e.RowIndex).ErrorText
            e.ToolTipText = String.Empty
            If String.IsNullOrEmpty(errorText) Then
                Return
            End If
            e.ToolTipText = errorText
        End Sub

        Protected Overrides Sub OnRowPostPaint(e As DataGridViewRowPostPaintEventArgs)
            MyBase.OnRowPostPaint(e)

            If e.RowIndex < 0 Then
                Return
            End If

            If Not _transparentRowSelection Then
                Return
            End If
            If Not Me.Rows(e.RowIndex).Selected Then
                Return
            End If

            e.PaintCells(e.RowBounds, DataGridViewPaintParts.All And Not DataGridViewPaintParts.Border)

            Dim borderWidth As Integer = 2
            Dim r As Rectangle = Me.GetRowDisplayRectangle(e.RowIndex, False)
            Dim r2 As Rectangle = Me.GetColumnDisplayRectangle(LastColumnDisplay.Index, False)
            Dim r2width As Integer = r2.X + r2.Width
            Dim rect = New Rectangle(r.X, r.Y, IIf(r.Width > r2width, r2width, r.Width), r.Height - 1)
            ControlPaint.DrawBorder(e.Graphics, rect,
                                        SystemColors.Highlight, borderWidth, ButtonBorderStyle.Solid,
                                        SystemColors.Highlight, borderWidth, ButtonBorderStyle.Solid,
                                        SystemColors.Highlight, borderWidth, ButtonBorderStyle.Solid,
                                        SystemColors.Highlight, borderWidth, ButtonBorderStyle.Solid)
        End Sub

        Protected Overrides Sub OnCellContentClick(e As DataGridViewCellEventArgs)
            Dim cell As DataGridViewCell

            If e.RowIndex > 0 AndAlso e.ColumnIndex > 0 Then
                cell = Me(e.ColumnIndex, e.RowIndex)
                If TypeOf cell Is DataGridViewDisableButtonCell Then
                    Dim disableButtonCell As DataGridViewDisableButtonCell = cell
                    If Not disableButtonCell.Enabled Then
                        Return
                    End If
                End If
            End If

            MyBase.OnCellContentClick(e)
        End Sub

#End Region

#Region " Method "

#Region " SuspendLayout/ResumeLayout "

        Public Sub BeginUpdate()
            SuspendLayout()
        End Sub

        Public Sub EndUpdate()
            ResumeLayout()
        End Sub

#End Region
#Region " SetComboBoxItems "

        ''' <summary>
        ''' コンボボックスへ表示するデータをバインドする
        ''' </summary>
        ''' <param name="propertyName">バインドしたい列名称</param>
        ''' <param name="dataSource">コンボボックスへバインドするデータソース</param>
        ''' <param name="displayMember">コンボ ボックスに表示する文字列の取得先となるプロパティまたは列を指定する文字列を取得または設定します。 </param>
        ''' <param name="valueMember">ドロップダウン リストの選択項目に対応する値の取得先となる、プロパティまたは列を指定する文字列を取得または設定します。 </param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function SetComboBoxItems(ByVal propertyName As String, ByVal dataSource As DataTable, ByVal displayMember As String, ByVal valueMember As String) As DataGridViewComboBoxColumn
            Dim col As DataGridViewColumn
            col = Me.Columns.Item(propertyName)
            Return _setComboBoxItems(col, dataSource, displayMember, valueMember)
        End Function

        ''' <summary>
        ''' コンボボックスへ表示するデータをバインドする
        ''' </summary>
        ''' <param name="index">列位置</param>
        ''' <param name="dataSource">コンボボックスへバインドするデータソース</param>
        ''' <param name="displayMember">コンボ ボックスに表示する文字列の取得先となるプロパティまたは列を指定する文字列を取得または設定します。 </param>
        ''' <param name="valueMember">ドロップダウン リストの選択項目に対応する値の取得先となる、プロパティまたは列を指定する文字列を取得または設定します。 </param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function SetComboBoxItems(ByVal index As Integer, ByVal dataSource As DataTable, ByVal displayMember As String, ByVal valueMember As String) As DataGridViewComboBoxColumn
            Dim col As DataGridViewColumn
            col = Me.Columns.Item(index)
            Return _setComboBoxItems(col, dataSource, displayMember, valueMember)
        End Function

        ''' <summary>
        ''' コンボボックスへ表示するデータをバインドする
        ''' </summary>
        ''' <param name="index">列位置</param>
        ''' <param name="dataSource">コンボボックスへバインドするデータソース</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function SetComboBoxItems(ByVal index As Integer, ByVal dataSource As Moca.ConstantDataSet.ConstantDataTable) As DataGridViewComboBoxColumn
            Dim col As DataGridViewColumn
            col = Me.Columns.Item(index)
            Return SetComboBoxItems(col, dataSource)
        End Function

        ''' <summary>
        ''' コンボボックスへ表示するデータをバインドする
        ''' </summary>
        ''' <param name="col">列</param>
        ''' <param name="dataSource">コンボボックスへバインドするデータソース</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function SetComboBoxItems(ByVal col As DataGridViewColumn, ByVal dataSource As Moca.ConstantDataSet.ConstantDataTable) As DataGridViewComboBoxColumn
            Return _setComboBoxItems(col, dataSource, dataSource.TextColumn.ColumnName, dataSource.ValueTextColumn.ColumnName)
        End Function

        ''' <summary>
        ''' コンボボックスへ表示するデータをバインドする
        ''' </summary>
        ''' <param name="col">列</param>
        ''' <param name="dataSource">コンボボックスへバインドするデータソース</param>
        ''' <param name="displayMember">コンボ ボックスに表示する文字列の取得先となるプロパティまたは列を指定する文字列を取得または設定します。 </param>
        ''' <param name="valueMember">ドロップダウン リストの選択項目に対応する値の取得先となる、プロパティまたは列を指定する文字列を取得または設定します。 </param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function SetComboBoxItems(ByVal col As DataGridViewColumn, ByVal dataSource As Object, ByVal displayMember As String, ByVal valueMember As String) As DataGridViewComboBoxColumn
            Return _setComboBoxItems(col, dataSource, displayMember, valueMember)
        End Function

#End Region
#Region " Cols "

        ''' <summary>
        ''' Columns.Item(index) のラッパー
        ''' </summary>
        ''' <param name="index"></param>
        ''' <returns></returns>
        Public Overloads Function Cols(ByVal index As Integer) As DataGridViewColumn
            If index < 0 Then
                Return Nothing
            End If
            Return Me.Columns.Item(index)
        End Function

        ''' <summary>
        ''' Columns.Item(index) のラッパー
        ''' </summary>
        ''' <param name="columnName"></param>
        ''' <returns></returns>
        Public Overloads Function Cols(ByVal columnName As String) As DataGridViewColumn
            Return Me.Columns.Item(columnName)
        End Function

#End Region
#Region " GetChanges "

        ''' <summary>
        ''' 変更行のみ返す
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overloads Function GetChanges(Of T)() As IList(Of T)
            Dim rc As New List(Of T)(1000)

            For Each row As T In GetChanges()
                rc.Add(row)
            Next

            Return rc
        End Function

        ''' <summary>
        ''' 変更行のみ返す
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overloads Function GetChanges() As IList(Of Object)
            Dim lst As New List(Of Object)(1000)
            If _dataBinder.BindSrc.DataSource IsNot Nothing Then
                'lst = (From row As Object In CType(_dataBinder.BindSrc.DataSource, IList)
                '       Where CType(row, RowModelBase).Status <> DataRowState.Unchanged
                '       Select row).ToList
                For Each row As Object In CType(_dataBinder.BindSrc.DataSource, IList)
                    If CType(row, RowModelBase).Status <> DataRowState.Unchanged Then
                        lst.Add(row)
                    End If
                Next
            End If

            lst.AddRange(_deletedRows)

            Return lst
        End Function

        ''' <summary>
        ''' 削除データを返す
        ''' </summary>
        ''' <returns></returns>
        Public Function GetDelete() As IList(Of Object)
            Return _deletedRows
        End Function

#End Region
#Region " Current "

        ''' <summary>
        ''' 現在行のDataSource項目を返す
        ''' </summary>
        ''' <returns></returns>
        Public Function Current() As Object
            If _dataBinder Is Nothing Then
                Return Nothing
            End If
            Return Me._dataBinder.BindSrc.Current()
        End Function

        ''' <summary>
        ''' カレント行データ
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <returns></returns>
        Public Function Current(Of T)() As T
            Dim row As T
            If Me._dataBinder.BindSrc Is Nothing Then
                Return Nothing
            End If
            row = Me._dataBinder.BindSrc.Current()
            Return row
        End Function

        ''' <summary>
        ''' カレント行データ
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="includeNode"></param>
        ''' <returns></returns>
        Public Function Current(Of T)(ByVal includeNode As Boolean) As T
            If Not includeNode Then
                Return Current(Of T)()
            End If
            Dim row As DataGridViewRow = CurrentRow()
            Return CType(row.DataBoundItem, T)
        End Function

#End Region
#Region " AddNew "

        ''' <summary>
        ''' 最終行へ新規追加
        ''' </summary>
        ''' <returns></returns>
        Public Function AddNew(Of T)() As T
            Dim add As Object
            add = Me._dataBinder.BindSrc.AddNew()
            If TypeOf add Is RowModelBase Then
                DirectCast(add, RowModelBase).Add()
            End If

            Me.Focus()
            Return add
        End Function

        ''' <summary>
        ''' 選択行の下へ新規追加
        ''' </summary>
        ''' <param name="copy">カレント行のデータをコピーするか</param>
        ''' <returns></returns>
        Public Function AddNewCurrent(Of T)(Optional ByVal copy As Boolean = False) As T
            Dim add As Object

            add = ClassUtil.NewInstance(Me.RowEntityType)
            Me._dataBinder.BindSrc.Insert(Me.SelectedCells(0).RowIndex + 1, add)

            If TypeOf add Is RowModelBase Then
                Dim value As RowModelBase = DirectCast(add, RowModelBase)
                value.Add()
                If copy Then
                    value.Copy(Current)
                End If
            End If

            Me.Focus()
            Return add
        End Function

#End Region
#Region " Remove "

        ''' <summary>
        ''' グリッドから現在の行を削除する
        ''' </summary>
        Public Function RemoveCurrent(Of T)() As T
            Dim row As T
            row = Current()
            If row Is Nothing Then
                Return Nothing
            End If

            Dim toprow As Integer
            toprow = Me.FirstDisplayedScrollingRowIndex

            _dataBinder.BindSrc.RemoveCurrent()

            Dim obj As Object = row
            Dim mdl As RowModelBase = DirectCast(obj, RowModelBase)
            mdl.Delete()
            If mdl.Status = DataRowState.Deleted Then
                _deletedRows.Add(row)
            End If

            If Not Me.Rows.Count.Equals(0) Then
                If Rows.Count <= toprow Then
                    toprow -= 1
                End If
                Me.FirstDisplayedScrollingRowIndex = toprow
            End If

            Me.Focus()
            Return row
        End Function

#End Region
#Region " Range "

        ''' <summary>
        ''' セルの書式設定と操作に使用できる <see cref="DataGridViewCell"/> オブジェクトを取得
        ''' </summary>
        ''' <param name="row"></param>
        ''' <param name="col"></param>
        ''' <returns></returns>
        Public Function GetCellRange(row As Integer, col As Integer) As DataGridViewCell
            Return Item(col, row)
        End Function

#End Region
#Region " 列マージ "

        ''' <summary>
        ''' 列マージ処理
        ''' </summary>
        ''' <param name="g"></param>
        Private Sub _mergeCellsInColumn(ByVal g As Graphics)
            If RowCount.Equals(0) Then
                Return
            End If

            Dim colMergeOn As Boolean = False
            For colIndex = 0 To ColumnCount - 1
                Dim prop As PropertyInfo = Columns(colIndex).Tag
                Dim attr As AllowMergingAttribute
                attr = ClassUtil.GetCustomAttribute(Of AllowMergingAttribute)(prop)
                If attr Is Nothing Then
                    colMergeOn = False
                    Continue For
                End If

                Dim mergeRow As Integer = 0
                For rowIndex = 1 To RowCount - 1
                    If Me(colIndex, rowIndex - 1).Value = Me(colIndex, rowIndex).Value Then
                        If (colIndex - 1) < 0 Then
                            Continue For
                        Else
                            If Not colMergeOn Then
                                Continue For
                            End If
                            If Not String.IsNullOrEmpty(Me(colIndex - 1, rowIndex).Tag) Then
                                Continue For
                            End If
                        End If
                    End If
                    If rowIndex = mergeRow Then
                        Continue For
                    End If

                    _mergeCellsInColumn(colIndex, mergeRow, rowIndex - 1, g)
                    mergeRow = rowIndex
                Next
                If (RowCount - 1) <> mergeRow Then
                    If Me(colIndex, RowCount - 1).Value Is Nothing Then
                        Continue For
                    End If
                    _mergeCellsInColumn(colIndex, mergeRow, RowCount - 1, g)
                End If
                colMergeOn = True
            Next
        End Sub

        ''' <summary>
        ''' セルマージ
        ''' </summary>
        ''' <param name="col"></param>
        ''' <param name="row1"></param>
        ''' <param name="row2"></param>
        ''' <param name="g"></param>
        Private Sub _mergeCellsInColumn(col As Integer, row1 As Integer, row2 As Integer, g As Graphics)
            If String.IsNullOrEmpty(Me(col, row1).Value) Then
                Return
            End If

            Dim column As DataGridViewColumn = Me.Columns.Item(col)
            Using grdPen As New Pen(GridColor)
                Dim startRectangle As Rectangle = GetCellDisplayRectangle(col, row1, True)
                Dim backColor As Color = IIf(column.DefaultCellStyle.BackColor = Color.Empty, DefaultCellStyle.BackColor, column.DefaultCellStyle.BackColor)
                Dim foreColor As Color = IIf(column.DefaultCellStyle.ForeColor = Color.Empty, DefaultCellStyle.ForeColor, column.DefaultCellStyle.ForeColor)
                Dim currentRectangle As Object = Nothing
                Dim recX As Integer = startRectangle.X
                Dim recY As Integer = startRectangle.Y
                Dim recWidth As Integer = startRectangle.Width
                Dim recHeight As Integer = 0
                Dim cutOverflow As Boolean = startRectangle.Height.Equals(0)
                Dim recValue As String = Me(col, row1).Value.ToString()

                For ii = row1 To row2
                    Dim rect As Rectangle = GetCellDisplayRectangle(col, ii, cutOverflow)
                    recHeight += rect.Height
                    If CurrentCell.ColumnIndex.Equals(col) Then
                        If CurrentCell.RowIndex.Equals(ii) Then
                            currentRectangle = rect
                        End If
                    End If
                    If recX.Equals(0) Then
                        recX = rect.X
                    End If
                    If recY.Equals(0) Then
                        recY = rect.Y
                    End If
                    If recWidth.Equals(0) Then
                        recWidth = rect.Width
                    End If
                    cutOverflow = rect.Height.Equals(0)
                    If Not ii.Equals(row1) Then
                        Me(col, ii).Tag = "MERGE"
                    End If
                Next

                ' マージ状態のセルを描画
                Dim newCell As Rectangle = New Rectangle(recX - 1, recY - 1, recWidth, recHeight)
                Dim currentPadding As Padding = CurrentCell.InheritedStyle.Padding
                Dim txtRect As Rectangle = New Rectangle(newCell.Left + currentPadding.Left,
                                                         newCell.Top + currentPadding.Top,
                                                         newCell.Width - currentPadding.Left - currentPadding.Right,
                                                         newCell.Height - currentPadding.Top - currentPadding.Bottom)
                Dim formatFlg As TextFormatFlags = _getTextFormatFlags(Columns(col).DefaultCellStyle.Alignment, Columns(col).DefaultCellStyle.WrapMode)
                Using backBrash As New SolidBrush(backColor)
                    g.FillRectangle(backBrash, newCell)
                    g.DrawRectangle(grdPen, newCell)
                    TextRenderer.DrawText(g, recValue, DefaultCellStyle.Font, txtRect, foreColor, formatFlg)
                End Using

                If currentRectangle Is Nothing Then
                    ' カレントセルでないときはここまで
                    Return
                End If
                If SelectionMode = DataGridViewSelectionMode.FullRowSelect Then
                    Return
                End If

                ' 選択セルを描画
                newCell = New Rectangle(currentRectangle.X - 1,
                                        currentRectangle.Y,
                                        currentRectangle.Width,
                                        currentRectangle.Height - 1)
                txtRect = New Rectangle(newCell.Left + currentPadding.Left,
                                        newCell.Top + currentPadding.Top,
                                        newCell.Width - currentPadding.Left - currentPadding.Right,
                                        newCell.Height - currentPadding.Top - currentPadding.Bottom)
                backColor = DefaultCellStyle.SelectionBackColor
                foreColor = DefaultCellStyle.SelectionForeColor
                Using backBrash As New SolidBrush(backColor)
                    g.FillRectangle(backBrash, newCell)
                    g.DrawRectangle(grdPen, newCell)
                    TextRenderer.DrawText(g, recValue, DefaultCellStyle.Font, txtRect, foreColor, formatFlg)
                End Using
            End Using
        End Sub

        ''' <summary>
        ''' 指定のスタイルから描写するテキストのスタイルを取得する
        ''' </summary>
        ''' <param name="alignment">テキストのスタイル</param>
        ''' <param name="wrapMode">折り返</param>
        ''' <remarks>描写するテキストのスタイル</remarks>
        Private Function _getTextFormatFlags(ByVal alignment As DataGridViewContentAlignment, ByVal wrapMode As DataGridViewTriState) As TextFormatFlags
            Try
                '文字の描画
                Dim formatFlg As TextFormatFlags = TextFormatFlags.Right Or TextFormatFlags.VerticalCenter Or TextFormatFlags.EndEllipsis

                '表示位置
                Select Case alignment
                    Case DataGridViewContentAlignment.BottomCenter
                        formatFlg = TextFormatFlags.Bottom Or TextFormatFlags.HorizontalCenter Or TextFormatFlags.EndEllipsis
                    Case DataGridViewContentAlignment.BottomLeft
                        formatFlg = TextFormatFlags.Bottom Or TextFormatFlags.Left Or TextFormatFlags.EndEllipsis
                    Case DataGridViewContentAlignment.BottomRight
                        formatFlg = TextFormatFlags.Bottom Or TextFormatFlags.Right Or TextFormatFlags.EndEllipsis
                    Case DataGridViewContentAlignment.MiddleCenter
                        formatFlg = TextFormatFlags.VerticalCenter Or TextFormatFlags.HorizontalCenter Or TextFormatFlags.EndEllipsis
                    Case DataGridViewContentAlignment.MiddleLeft
                        formatFlg = TextFormatFlags.VerticalCenter Or TextFormatFlags.Left Or TextFormatFlags.EndEllipsis
                    Case DataGridViewContentAlignment.MiddleRight
                        formatFlg = TextFormatFlags.VerticalCenter Or TextFormatFlags.Right Or TextFormatFlags.EndEllipsis
                    Case DataGridViewContentAlignment.TopCenter
                        formatFlg = TextFormatFlags.Top Or TextFormatFlags.HorizontalCenter Or TextFormatFlags.EndEllipsis
                    Case DataGridViewContentAlignment.TopLeft
                        formatFlg = TextFormatFlags.Top Or TextFormatFlags.Left Or TextFormatFlags.EndEllipsis
                    Case DataGridViewContentAlignment.TopRight
                        formatFlg = TextFormatFlags.Top Or TextFormatFlags.Right Or TextFormatFlags.EndEllipsis
                End Select

                '折り返し
                Select Case wrapMode
                    Case DataGridViewTriState.False
                    Case DataGridViewTriState.NotSet
                    Case DataGridViewTriState.True
                        formatFlg = formatFlg Or TextFormatFlags.WordBreak
                End Select

                Return formatFlg

            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

        ''' <summary>
        ''' データ部かどうか
        ''' </summary>
        ''' <param name="rowIndex"></param>
        ''' <param name="columnIndex"></param>
        ''' <returns></returns>
        Public Function IsDataPart(ByVal columnIndex As Integer, ByVal rowIndex As Integer) As Boolean
            If rowIndex < 0 Then
                Return False
            End If
            If columnIndex < 0 Then
                Return False
            End If
            Return True
        End Function

        Private Sub _changeLinkSelectedStyle(ByVal col As DataGridViewColumn, ByVal rowIndex As Integer)
            If Not TypeOf col Is DataGridViewLinkColumn Then
                Return
            End If

            Dim cell As DataGridViewLinkCell = CType(Me(col.Index, rowIndex), DataGridViewLinkCell)
            If TransparentRowSelection Then
                cell.LinkColor = col.DefaultCellStyle.ForeColor
                cell.VisitedLinkColor = cell.LinkColor
            Else
                cell.LinkColor = DefaultCellStyle.SelectionForeColor
                cell.VisitedLinkColor = DefaultCellStyle.SelectionForeColor
            End If
        End Sub

        Private Sub _changeLinkUnSelectedStyle(ByVal col As DataGridViewColumn, ByVal rowIndex As Integer)
            If Not TypeOf col Is DataGridViewLinkColumn Then
                Return
            End If

            Dim lnk As DataGridViewLinkColumn = col
            Dim cell As DataGridViewLinkCell = CType(Me(col.Index, rowIndex), DataGridViewLinkCell)
            cell.LinkColor = lnk.DefaultCellStyle.ForeColor
            cell.VisitedLinkColor = lnk.DefaultCellStyle.ForeColor
        End Sub

        ''' <summary>
        ''' セルを結合する対象の列の描画領域の無効化
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub InvalidateUnitColumns()
            Try
                Dim hRect As Rectangle = MyBase.DisplayRectangle
                MyBase.Invalidate(hRect)
                Invalidate()
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
            End Try
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
            Me.AutoGenerateColumns = False   ' DataSource設定時の自動列作成をOFF

            _deletedRows = New List(Of Object)

            ' グリッドの設定変更イベント
            Dim args As GridSettingEventArgs
            args = New GridSettingEventArgs
            args.TargetGrid = Me

            OnGridSetting(args)
        End Sub

        Protected Overridable Sub OnGridSetting(ByVal args As GridSettingEventArgs)
            RaiseEvent GridSetting(Me, args)
        End Sub

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
            DefaultCellStyle = Styles(StyleNames.Normal.ToString)
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
        ''' グリッドにデータソースを設定する
        ''' </summary>
        ''' <param name="obj"></param>
        ''' <remarks></remarks>
        Private Sub _setData(ByVal obj As BindingSource)
            MyBase.DataSource = Nothing
            Columns.Clear()

            If obj.DataSource Is Nothing Then
                RowEntityType = Nothing
                _deletedRows.Clear()
                Return
            End If

            If RowEntityType Is Nothing Then
                RowEntityType = obj.List.GetType.GetGenericArguments(0)
                'RowEntityType = obj.List.GetType.GetGenericArguments.First
            End If

            If DesignSettings Is Nothing Then
                DesignSettings = GridDesignSettings.Default
            End If

            ' 列設定（非表示、編集不可）
            _setCols()

            MyBase.DataSource = obj
        End Sub

        ''' <summary>
        ''' 列定義設定
        ''' </summary>
        Private Sub _setCols()
            If Me.RowEntityType Is Nothing Then
                Return
            End If

            Dim args As GridColmnSettingEventArgs

            args = New GridColmnSettingEventArgs
            _modelProps = ClassUtil.GetProperties(Me.RowEntityType)
            _modelPropDic = New Dictionary(Of String, PropertyInfo)
            For Each prop As PropertyInfo In _modelProps
                _modelPropDic.Add(prop.Name, prop)
            Next

            If _dataBinder.DataSource Is Nothing Then
                Return
            End If

            Dim attrColspans As IList(Of CaptionAttribute)
            attrColspans = New List(Of CaptionAttribute)

            For Each prop As PropertyInfo In _modelProps
                Dim col As DataGridViewColumn

                ' スタイル
                col = _setColStyle(prop)

                ' 読取専用
                _setColStyleReadOnly(prop, col)

                ' 非表示
                _setColStyleHidden(prop, col)

                ' スクロール固定列
                _setColStyleFrozen(prop, col)

                ' 必須
                _setColStyleRequired(prop, col)

                ' 編集条件
                _setColStyleEditCondition(prop, col)

                col.Tag = prop

                args.Index = col.Index
                args.Column = col
                args.ModelProperty = prop
                OnGridColmnSetting(args)
            Next
        End Sub

        ''' <summary>
        ''' グリッドの列情報設定イベント
        ''' </summary>
        ''' <param name="args"></param>
        Protected Overridable Sub OnGridColmnSetting(ByVal args As GridColmnSettingEventArgs)
            RaiseEvent GridColmnSetting(Me, args)
        End Sub

        ''' <summary>
        ''' 列のスタイル設定
        ''' </summary>
        ''' <param name="prop"></param>
        Private Function _setColStyle(ByVal prop As PropertyInfo) As DataGridViewColumn
            Dim col As DataGridViewColumn
            Dim attrCaption As DisplayNameAttribute
            Dim attr As ColumnStyleAttribute

            Dim caption As String

            attrCaption = ClassUtil.GetCustomAttribute(Of DisplayNameAttribute)(prop)
            If attrCaption Is Nothing Then
                caption = prop.Name
            Else
                caption = attrCaption.DisplayName
            End If

            attr = ClassUtil.GetCustomAttribute(Of ColumnStyleAttribute)(prop)
            If attr Is Nothing Then
                attr = New ColumnStyleAttribute(cellType:=CellType.TextBox)
                col = _makeColumn(attr, prop)
                col.HeaderText = _cnvColCaption(caption)
                col.DataPropertyName = prop.Name
                col.Name = prop.Name
                col.SortMode = DataGridViewColumnSortMode.NotSortable
                Me.Columns.Add(col)
                Return col
            End If

            Dim colIndex As Integer

            col = _makeColumn(attr, prop)
            col.HeaderText = _cnvColCaption(caption)
            col.DataPropertyName = prop.Name
            col.Name = prop.Name
            col.DefaultCellStyle.Alignment = attr.Align
            col.SortMode = DataGridViewColumnSortMode.NotSortable
            col.DefaultCellStyle.NullValue = attr.NullValue

            colIndex = Me.Columns.Add(col)

            If attr.Width >= 0 Then
                col.Width = attr.Width
            Else
                ' とりあえず列のサイズは自動にする
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader
            End If

            Dim negativeValueFormat As String = _getGridDesignSetting("NegativeValueFormat")
            Dim format As String = attr.Format
            If Not String.IsNullOrEmpty(negativeValueFormat) Then
                format = format.Replace(negativeValueFormat, String.Empty)
            End If
            If Not String.IsNullOrEmpty(attr.Format) Then
                col.DefaultCellStyle.Format = format
            End If
            If attr.WordWrap Then
                col.DefaultCellStyle.WrapMode = DataGridViewTriState.True
            End If

            Return col
        End Function

        ''' <summary>
        ''' 指定されたセル種別でDataGridViewColumnを作成する。
        ''' </summary>
        ''' <param name="attr"></param>
        ''' <param name="prop"></param>
        ''' <returns></returns>
        Private Function _makeColumn(ByVal attr As ColumnStyleAttribute, ByVal prop As PropertyInfo) As DataGridViewColumn
            Dim type As CellType = attr.CellType
            Dim col As DataGridViewColumn

            If prop.PropertyType.Equals(GetType(Image)) Then
                type = CellType.Image
            End If
            If prop.PropertyType.IsSubclassOf(GetType(Image)) Then
                type = CellType.Image
            End If
            If prop.PropertyType.Equals(GetType(Date)) Then
                type = CellType.Calendar
            End If
            If prop.PropertyType.Equals(GetType(Boolean)) AndAlso Not type.Equals(CellType.CheckBoxImage) Then
                type = CellType.CheckBox
            End If

            Select Case type
                Case CellType.Button
                    col = New DataGridViewButtonColumn()
                    attr.Align = DataGridViewContentAlignment.MiddleCenter
                Case CellType.DisableButton
                    col = New DataGridViewDisableButtonColumn()
                    attr.Align = DataGridViewContentAlignment.MiddleCenter
                Case CellType.CheckBox
                    col = New DataGridViewCheckBoxColumn()
                Case CellType.CheckBoxImage
                    col = New DataGridViewCheckBoxImageColumn()
                    Dim img As DataGridViewCheckBoxImageColumn = col
                    img.CheckedImage = My.Resources.Checked
                    img.UnCheckedImage = My.Resources.UnChecked
                    attr.NullValue = False
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
                    attr.NullValue = Nothing
                Case CellType.Link
                    col = New DataGridViewLinkColumn()
                Case CellType.Calendar
                    col = New DataGridViewCalendarColumn()
                    Dim cal As DataGridViewCalendarColumn = DirectCast(col, DataGridViewCalendarColumn)
                    If String.IsNullOrEmpty(attr.InputFormat) Then
                        If Not String.IsNullOrEmpty(attr.Format) Then
                            cal.PickerCustomFormat = attr.Format
                        End If
                    Else
                        cal.PickerCustomFormat = attr.InputFormat
                    End If
                    col.DefaultCellStyle.NullValue = Nothing
                Case CellType.MaskedTextBox
                    col = New DataGridViewMaskedTextBoxColumn()
                    Dim mask As DataGridViewMaskedTextBoxColumn = DirectCast(col, DataGridViewMaskedTextBoxColumn)
                    mask.Mask = attr.InputFormat
                Case Else
                    If attr.InputControl = TextBoxEx.InputFormatType.None Then
                        col = New DataGridViewTextBoxColumn()
                    Else
                        col = New DataGridViewTextBoxExColumn()
                        Dim txt As DataGridViewTextBoxExColumn = DirectCast(col, DataGridViewTextBoxExColumn)
                        txt.InputFormat = attr.InputControl

                    End If
            End Select

            Return col
        End Function

        Private Sub _setColStyleEditCondition(ByVal prop As PropertyInfo, ByVal col As DataGridViewColumn)
            If _editConditions Is Nothing Then
                _editConditions = New Dictionary(Of DataGridViewColumn, EditConditionAttribute)
            End If
            Dim attr As EditConditionAttribute
            attr = ClassUtil.GetCustomAttribute(Of EditConditionAttribute)(prop)
            If attr Is Nothing Then
                Return
            End If

            _editConditions.Add(col, attr)
        End Sub

        ''' <summary>
        ''' 必須列設定
        ''' </summary>
        ''' <param name="prop"></param>
        ''' <param name="col"></param>
        Private Sub _setColStyleRequired(ByVal prop As PropertyInfo, ByVal col As DataGridViewColumn)
            Dim attr As RequiredColumnAttribute
            attr = ClassUtil.GetCustomAttribute(Of RequiredColumnAttribute)(prop)
            If attr Is Nothing Then
                Return
            End If
            If Not attr.IsRequired Then
                Return
            End If

            Dim style As DataGridViewCellStyle
            style = Styles(StyleNames.Required.ToString)
            col.DefaultCellStyle.BackColor = style.BackColor
            col.DefaultCellStyle.Font = style.Font
            col.DefaultCellStyle.ForeColor = style.ForeColor
            col.DefaultCellStyle.SelectionBackColor = style.SelectionBackColor
            col.DefaultCellStyle.SelectionForeColor = style.SelectionForeColor
            col.DefaultCellStyle.Tag = style.Tag
        End Sub

        ''' <summary>
        ''' 読取専用列設定
        ''' </summary>
        ''' <param name="prop"></param>
        ''' <param name="col"></param>
        Private Sub _setColStyleReadOnly(ByVal prop As PropertyInfo, ByVal col As DataGridViewColumn)
            Dim attr As ReadOnlyAttribute
            attr = ClassUtil.GetCustomAttribute(Of ReadOnlyAttribute)(prop)
            If attr Is Nothing Then
                Return
            End If
            If Not attr.IsReadOnly Then
                Return
            End If

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
        ''' 非表示列設定
        ''' </summary>
        ''' <param name="prop"></param>
        ''' <param name="col"></param>
        Private Sub _setColStyleHidden(ByVal prop As PropertyInfo, ByVal col As DataGridViewColumn)
            Dim attr As HiddenColumnAttribute
            attr = ClassUtil.GetCustomAttribute(Of HiddenColumnAttribute)(prop)
            If attr Is Nothing Then
                Return
            End If

            col.Visible = Not attr.IsHidden
        End Sub

        ''' <summary>
        ''' スクロール固定列設定
        ''' </summary>
        ''' <param name="prop"></param>
        ''' <param name="col"></param>
        Private Sub _setColStyleFrozen(ByVal prop As PropertyInfo, ByVal col As DataGridViewColumn)
            Dim attr As FrozenAttribute
            attr = ClassUtil.GetCustomAttribute(Of FrozenAttribute)(prop)
            If attr Is Nothing Then
                Return
            End If

            If attr.Column < 0 Then
                _frozen = col.Index
            Else
                _frozen = attr.Column
            End If

            For ii As Integer = 0 To _frozen
                Columns.Item(ii).Frozen = True
                If Columns.Item(ii).DefaultCellStyle IsNot Nothing AndAlso
                Columns.Item(ii).DefaultCellStyle.Tag = StyleNames.Required.ToString Then
                    Continue For
                End If
                If Styles.ContainsKey(StyleNames.Frozen.ToString) Then
                    Columns.Item(ii).DefaultCellStyle.BackColor = Styles(StyleNames.Frozen.ToString).BackColor
                End If
            Next
        End Sub

        ''' <summary>
        ''' コンボボックスへ表示するデータをバインドする
        ''' </summary>
        ''' <param name="col">列</param>
        ''' <param name="dataSource">コンボボックスへバインドするデータソース</param>
        ''' <param name="displayMember">コンボ ボックスに表示する文字列の取得先となるプロパティまたは列を指定する文字列を取得または設定します。 </param>
        ''' <param name="valueMember">ドロップダウン リストの選択項目に対応する値の取得先となる、プロパティまたは列を指定する文字列を取得または設定します。 </param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function _setComboBoxItems(ByVal col As DataGridViewColumn, ByVal dataSource As DataTable, ByVal displayMember As String, ByVal valueMember As String) As DataGridViewComboBoxColumn
            If Not TypeOf col Is DataGridViewComboBoxColumn Then
                Throw New ArgumentException("指定された列はコンボボックススタイルになっていません。")
            End If

            Dim cbo As DataGridViewComboBoxColumn = DirectCast(col, DataGridViewComboBoxColumn)

            cbo.DataSource = dataSource
            cbo.DisplayMember = displayMember
            cbo.ValueMember = valueMember

            Return cbo
        End Function

#Region " Validate "

        ''' <summary>
        ''' 列のチェック内容を返す
        ''' </summary>
        ''' <param name="col"></param>
        ''' <returns></returns>
        Public Function GetColValidateTypes(ByVal col As Integer, ByVal val As Object) As ValidateTypesAttribute
            Dim prop As PropertyInfo
            prop = CType(Me.Columns(col).Tag, PropertyInfo)

            Dim attr As ValidateTypesAttribute
            attr = ClassUtil.GetCustomAttribute(Of ValidateTypesAttribute)(prop)
            If attr Is Nothing Then
                Return Nothing
            End If

            If String.IsNullOrEmpty(attr.TableDefinitionFieldName) Then
                _tblDef.GetTableDefinition(val)
            Else
                _tblDef.GetTableDefinition(val, attr.TableDefinitionFieldName)
            End If

            Dim columnName As String
            Dim dbInfoCol As Moca.Db.DbInfoColumn

            columnName = prop.Name
            If Not String.IsNullOrEmpty(attr.TableColumnName) Then
                columnName = attr.TableColumnName
            End If
            If String.IsNullOrEmpty(attr.TableDefinitionFieldName) Then
                dbInfoCol = _tblDef.GetTableDefinitionColumn(columnName)
            Else
                dbInfoCol = _tblDef.GetTableDefinitionColumn(columnName, attr.TableDefinitionFieldName)
            End If

            Return _getValidateAttribute(dbInfoCol, attr)
        End Function

        ''' <summary>
        ''' 列のチェック内容を作成
        ''' </summary>
        ''' <param name="dbInfoCol"></param>
        ''' <param name="attr"></param>
        ''' <returns></returns>
        Private Function _getValidateAttribute(ByVal dbInfoCol As Moca.Db.DbInfoColumn, ByVal attr As ValidateTypesAttribute) As ValidateTypesAttribute
            Dim validator As New Validator
            Dim attrNew As ValidateTypesAttribute
            Dim columnDefinition As Moca.Db.DbInfoColumn = dbInfoCol
            Dim max As Object = Nothing
            If columnDefinition IsNot Nothing Then
                If attr.Max Is Nothing Then
                    If validator.IsValidLenghtMaxB(attr.ValidateTypes) Then
                        max = columnDefinition.MaxLength
                    Else
                        max = columnDefinition.MaxLengthB
                    End If
                Else
                    max = attr.Max
                End If
                attrNew = New ValidateTypesAttribute(attr.ValidateTypes, attr.Min, max, attr.ErrorDispControlName)
            Else
                attrNew = New ValidateTypesAttribute(attr.ValidateTypes, attr.Min, attr.Max, attr.ErrorDispControlName)
            End If

            Return attrNew
        End Function

#End Region

#End Region

#Region " TableDefinition Class "

        ''' <summary>
        ''' テーブル定義
        ''' </summary>
        Public Class TableDefinition

            ''' <summary>
            ''' 列名
            ''' </summary>
            Private _columnNames As IDictionary(Of String, IDictionary(Of String, String))
            ''' <summary>
            ''' フィールド情報
            ''' </summary>
            Private _infos As IDictionary(Of String, FieldInfo)
            ''' <summary>
            ''' テーブル定義オブジェクト
            ''' </summary>
            Private _tblDefs As IDictionary(Of String, Object)

            ''' <summary>
            ''' デフォルトコンストラクタ
            ''' </summary>
            Public Sub New()
                _tblDefs = New Dictionary(Of String, Object)
                _infos = New Dictionary(Of String, FieldInfo)
                _columnNames = New Dictionary(Of String, IDictionary(Of String, String))
            End Sub

            ''' <summary>
            ''' テーブル定義情報取得
            ''' </summary>
            ''' <param name="val"></param>
            Public Sub GetTableDefinition(ByVal val As Object, Optional ByVal tableDefinitionFieldName As String = "tableDefinition")
                If _tblDefs.ContainsKey(tableDefinitionFieldName) Then
                    Return
                End If

                If Not _infos.ContainsKey(tableDefinitionFieldName) Then
                    Dim fields() As FieldInfo = val.GetType.GetFields(BindingFlags.Instance Or BindingFlags.Public Or BindingFlags.NonPublic)
                    For Each field As FieldInfo In fields
                        Dim attr As Moca.Db.Attr.TableAttribute = ClassUtil.GetCustomAttribute(Of Moca.Db.Attr.TableAttribute)(field.FieldType)
                        If attr Is Nothing Then
                            Continue For
                        End If
                        If _infos.ContainsKey(field.Name) Then
                            Continue For
                        End If
                        _infos.Add(field.Name, field)
                        If Not _infos.ContainsKey(tableDefinitionFieldName) Then
                            _infos.Add(tableDefinitionFieldName, field)
                        End If
                    Next

                    If Not _infos.ContainsKey(tableDefinitionFieldName) Then
                        Return
                    End If
                End If

                Dim tblDef As Object
                Dim info As FieldInfo
                info = _infos(tableDefinitionFieldName)
                Dim builder As EntityBuilder = New EntityBuilder
                builder.SetColumnInfo(val)

                tblDef = val.GetType.InvokeMember(info.Name, BindingFlags.Instance Or BindingFlags.NonPublic Or BindingFlags.Public Or BindingFlags.GetField, Nothing, val, New Object() {})
                _tblDefs.Add(tableDefinitionFieldName, tblDef)
            End Sub

            ''' <summary>
            ''' テーブル定義列情報取得
            ''' </summary>
            ''' <param name="name"></param>
            ''' <returns></returns>
            Public Function GetTableDefinitionColumn(ByVal name As String, Optional ByVal tableDefinitionFieldName As String = "tableDefinition") As Moca.Db.DbInfoColumn
                If Not _tblDefs.ContainsKey(tableDefinitionFieldName) Then
                    Return Nothing
                End If

                Dim tblDef As Object
                Dim info As FieldInfo
                Dim columnNames As IDictionary(Of String, String)

                tblDef = _tblDefs(tableDefinitionFieldName)
                info = _infos(tableDefinitionFieldName)

                If _columnNames.ContainsKey(tableDefinitionFieldName) Then
                    columnNames = _columnNames(tableDefinitionFieldName)
                Else
                    columnNames = _getColumnNames(tableDefinitionFieldName, info)
                End If

                Dim columnName As String = String.Empty
                If Not columnNames.TryGetValue(name, columnName) Then
                    Return Nothing
                End If

                Dim column As Moca.Db.DbInfoColumn = Nothing
                column = TryCast(info.FieldType.InvokeMember(columnName, BindingFlags.Instance Or BindingFlags.Public Or BindingFlags.GetProperty, Nothing, tblDef, New Object() {}), Moca.Db.DbInfoColumn)
                Return column
            End Function

            ''' <summary>
            ''' 列名取得
            ''' </summary>
            ''' <param name="TableDefinitionFieldInfo"></param>
            Private Function _getColumnNames(ByVal tableDefinitionFieldName As String, ByVal TableDefinitionFieldInfo As FieldInfo) As IDictionary(Of String, String)
                Dim columnNames As IDictionary(Of String, String) = New Dictionary(Of String, String)

                Dim props() As PropertyInfo = TableDefinitionFieldInfo.FieldType.GetProperties()
                For Each prop As PropertyInfo In props
                    Dim attrColumn As Moca.Db.Attr.ColumnAttribute = ClassUtil.GetCustomAttribute(Of Moca.Db.Attr.ColumnAttribute)(prop)
                    Dim columnName As String = prop.Name
                    If attrColumn IsNot Nothing Then
                        columnName = attrColumn.ColumnName
                    End If
                    columnNames.Add(columnName, prop.Name)
                Next

                _columnNames.Add(tableDefinitionFieldName, columnNames)

                Return columnNames
            End Function

        End Class

#End Region

    End Class

End Namespace
