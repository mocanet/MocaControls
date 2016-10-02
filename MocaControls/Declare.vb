
#Region "セルの種類 "

''' <summary>
''' セルのコントロール種別
''' </summary>
''' <remarks></remarks>
Public Enum CellType As Integer
    ''' <summary>ボタン</summary>
    Button = 0
    ''' <summary>チェックボックス</summary>
    CheckBox
    ''' <summary>コンボボックス</summary>
    ComboBox
    ''' <summary>イメージ</summary>
    Image
    ''' <summary>リンク</summary>
    Link
    ''' <summary>テキストボックス</summary>
    TextBox
    ''' <summary>マスクテキストボックス</summary>
    MaskedTextBox
    ''' <summary>ボタン（無効化可能）</summary>
    DisableButton
    ''' <summary>カレンダー</summary>
    Calendar

    '    CalendarUpDown
End Enum

#End Region

#Region "グリッドスタイル名 "

''' <summary>
''' グリッドスタイル名
''' </summary>
Public Enum StyleNames As Integer
    Normal
    Alternate
    Fixed
    Highlight
    Focus
    Editor
    Frozen
    NewRow
    SelectedColumnHeader
    ''' <summary>列の SelectedRowHeader 専用スタイル名</summary>
    SelectedRowHeader
    FilterEditor
    ''' <summary>列のエラー専用スタイル名</summary>
    [Error]
    ''' <summary>列の編集不可スタイル名</summary>
    Disable
    ''' <summary>列の読み取り専用スタイル名</summary>
    [ReadOnly]
    ''' <summary>列の入力必須スタイル名</summary>
    Required
    NoEdit
    NegativeValue
    Closed
    Modify
End Enum

#End Region
