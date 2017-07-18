CREATE PROCEDURE dbo.IDaoDemo_Get
	(
	@ID nvarchar(5)
	)
AS

SELECT
	[ID]
	,[Code]
	,[Name]
	,[Note]
FROM	[tbDemo]
WHERE	[ID] = @ID
	AND	(@ID IS NULL	OR	[ID] LIKE '%' + @ID + '%')


RETURN