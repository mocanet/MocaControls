
Namespace Win

    ''' <summary>
    ''' DataGridViewTextBoxExCell オブジェクトの列を表します。
    ''' </summary>
    Public Class DataGridViewTextBoxExColumn
        Inherits DataGridViewColumn

#Region " コンストラクタ "

        ''' <summary>
        ''' CellTemplate とする DataGridViewTextBoxExCell オブジェクトを指定して
        ''' 基本クラスのコンストラクタを呼び出す
        ''' </summary>
        Public Sub New()
            MyBase.New(New DataGridViewTextBoxExCell())
        End Sub

#End Region

#Region " Property "

        Private _inputFormat As TextBoxEx.InputFormatType = TextBoxEx.InputFormatType.None
        ''' <summary>
        ''' TextBoxEx の InputFormat プロパティに適用する値
        ''' </summary>
        Public Property InputFormat() As TextBoxEx.InputFormatType
            Get
                Return Me._inputFormat
            End Get
            Set(ByVal value As TextBoxEx.InputFormatType)
                Me._inputFormat = value
            End Set
        End Property

        Private _InputControlCustomChars As String = String.Empty
        Public Property InputControlCustomChars As String
            Get
                Return _InputControlCustomChars
            End Get
            Set(value As String)
                _InputControlCustomChars = value
            End Set
        End Property

		''' <summary>小数点以下の桁数</summary>
		Private _precision As Integer = 0
		Public Property Precision() As System.Int32
			Get
				Return Me._precision
			End Get
			Set(ByVal value As System.Int32)
				Me._precision = value
			End Set
		End Property

#End Region

#Region " Overrides "

		''' <summary>
		''' 新しいプロパティを追加しているため、
		''' Cloneメソッドをオーバーライドする必要がある
		''' </summary>
		''' <returns></returns>
		Public Overrides Function Clone() As Object
			Dim col As DataGridViewTextBoxExColumn = DirectCast(MyBase.Clone(), DataGridViewTextBoxExColumn)
			col.InputFormat = Me.InputFormat
			col.InputControlCustomChars = Me.InputControlCustomChars
			col.Precision = Me.Precision
			Return col
		End Function

		''' <summary>
		''' CellTemplate の取得と設定
		''' </summary>
		''' <returns></returns>
		Public Overrides Property CellTemplate() As DataGridViewCell
            Get
                Return MyBase.CellTemplate
            End Get
            Set(ByVal value As DataGridViewCell)
                ' DataGridViewTextBoxExCell しか CellTemplate に設定できないようにする
                If Not (TypeOf value Is DataGridViewTextBoxExCell) Then
                    Throw New InvalidCastException("DataGridViewTextBoxExCell オブジェクトを指定してください。")
                End If
                MyBase.CellTemplate = value
            End Set
        End Property

#End Region

    End Class

End Namespace
