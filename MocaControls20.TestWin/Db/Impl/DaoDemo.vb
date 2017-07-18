
Imports Moca.Db

Namespace Db.Impl

	''' <summary>
	''' tbMocaDemo データアクセス
	''' </summary>
	''' <remarks></remarks>
	Public Class DaoDemo
		Inherits AbstractDao
		Implements IDaoDemo

		Public Function [Get]() As System.Collections.Generic.IList(Of DemoRow) Implements IDaoDemo.Get
			Const C_SQL As String = "SELECT [ID],[Code],[Name],[Note] FROM [tbDemo]"

			Using cmd As IDbCommandSelect = CreateCommandSelect(C_SQL)
				Return cmd.Execute(Of DemoRow)()
			End Using
		End Function

		Public Function [Get](id As String) As System.Collections.Generic.IList(Of DemoRow) Implements IDaoDemo.Get
			Const C_SQL As String = "SELECT [ID],[Code],[Name],[Note] FROM [tbDemo] WHERE @ID IS NULL OR (@ID IS NOT NULL AND [ID] LIKE '%' + @ID + '%')"

			Using cmd As IDbCommandSelect = CreateCommandSelect(C_SQL)
				cmd.SetParameter("ID", CNothing(id))

				Return cmd.Execute(Of DemoRow)()
			End Using
		End Function

		Public Function [Get](id As String, code As Integer) As System.Collections.Generic.IList(Of DemoRow) Implements IDaoDemo.Get
			Const C_SQL As String = "IDaoDemo_Get"

			Using cmd As IDbCommandStoredProcedure = CreateCommandStoredProcedure(C_SQL)
				cmd.AddParameterValue(id)
				cmd.AddParameterValue(code)

				Return cmd.Execute(Of DemoRow)()
			End Using
		End Function

		Public Function Ins(value As DemoRow) As Integer Implements IDaoDemo.Ins
			Const C_SQL As String = "IDaoDemo_Ins"

			Using cmd As IDbCommandStoredProcedure = CreateCommandStoredProcedure(C_SQL)
				cmd.AddParameterValue(value.ID)
				cmd.AddParameterValue(value.Name)
				cmd.AddParameterValue(value.Note)

				cmd.ExecuteNonQuery()

				Return cmd.ResultOutParameter("MaxCode")
			End Using
		End Function

		Public Sub [Mod](value As DemoRow) Implements IDaoDemo.Mod
			Const C_SQL As String = "UPDATE [tbDemo] SET [Name] = @Name ,[Note] = @Note WHERE [ID] = @ID AND [Code] = @Code"

			Using cmd As IDbCommandUpdate = CreateCommandUpdate(C_SQL)
				cmd.SetParameter("Name", value.Name)
				cmd.SetParameter("Note", value.Note)
				cmd.SetParameter("ID", value.ID)
				cmd.SetParameter("Code", value.Code)

				Dim rc As Integer
				rc = cmd.Execute()
			End Using
		End Sub

	End Class

End Namespace
