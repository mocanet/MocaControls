
Imports Moca.Attr
Imports Moca.Db.Attr

Namespace Db

    ''' <summary>
    ''' tbMocaDemo データアクセスインタフェース
    ''' </summary>
    ''' <remarks></remarks>
    <Dao(Sys.C_CONNECTION_STRING, GetType(Impl.DaoDemo))>
    Public Interface IDaoDemo

		Function [Get]() As IList(Of DemoRow)

		Function [Get](ByVal id As String) As IList(Of DemoRow)

		Function [Get](ByVal id As String, ByVal code As Integer) As IList(Of DemoRow)

		<Transaction()>
		<Aspect(GetType(SQLStatementlogger))>
		Function Ins(ByVal value As DemoRow) As Integer

		<Transaction()>
		Sub [Mod](ByVal value As DemoRow)

    End Interface

End Namespace
