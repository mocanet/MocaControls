
Imports Moca.Util

Namespace Win

    ''' <summary>
    ''' 検証列属性
    ''' </summary>
    ''' <remarks></remarks>
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field, AllowMultiple:=False)>
    Public Class ValidateTypesAttribute
        Inherits Attribute

#Region " コンストラクタ "

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="validateTypes ">検証。省略時は ValidateTypes.None</param>
        ''' <param name="min">最小値検証するときの値</param>
        ''' <param name="max">最大値検証するときの値</param>
        ''' <param name="errorDispControlName">エラー表示するコントロールが別の時のコントロール名</param>
        ''' <param name="tableColumnName">テーブル列名</param>
        ''' <param name="tableDefinitionFieldName">関連するテーブルの列名を取得するテーブル定義フィールド名</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal validateTypes As ValidateTypes, Optional ByVal min As Object = Nothing, Optional ByVal max As Object = Nothing, Optional ByVal errorDispControlName As String = Nothing, Optional ByVal tableColumnName As String = Nothing, Optional ByVal tableDefinitionFieldName As String = Nothing)
            _ValidateTypes = validateTypes
            _Min = min
            _Max = max
            _ErrorDispControlName = errorDispControlName
            _TableColumnName = tableColumnName
            _TableDefinitionFieldName = tableDefinitionFieldName
        End Sub

#End Region

#Region " Property "

        ''' <summary>検証</summary>
        Public Property ValidateTypes() As ValidateTypes


        ''' <summary> 
        ''' 最小値検証するときの値 
        ''' </summary> 
        Public Property Min() As Object

        ''' <summary> 
        ''' 最大値検証するときの値
        ''' </summary> 
        Public Property Max() As Object

        ''' <summary>
        ''' エラー表示するコントロールが別の時のコントロール名
        ''' </summary>
        ''' <returns></returns>
        Public Property ErrorDispControlName As String

        ''' <summary>
        ''' 関連するテーブルの列名
        ''' </summary>
        ''' <returns></returns>
        Public Property TableColumnName As String

        ''' <summary>
        ''' 関連するテーブルの列名を取得するテーブル定義フィールド名
        ''' </summary>
        ''' <returns></returns>
        Public Property TableDefinitionFieldName As String

#End Region

    End Class

End Namespace
