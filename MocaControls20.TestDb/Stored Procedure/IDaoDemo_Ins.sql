CREATE PROCEDURE dbo.IDaoDemo_Ins
	(
	@ID	nvarchar(5),
	@Name	nvarchar(256),
	@Note	nvarchar(max),
	@MaxCode	int OUTPUT
	)
AS

SELECT
	@MaxCode = MAX([Code]) + 1
FROM	[tbDemo]
WHERE	ID=@ID
GROUP BY	ID

SELECT @MaxCode = CASE WHEN @MaxCode IS NULL THEN 1 ELSE @MaxCode END

INSERT INTO [tbDemo]
           ([ID]
           ,[Code]
           ,[Name]
           ,[Note])
     VALUES
           (@ID
           ,@MaxCode
           ,@Name
           ,@Note)


RETURN	@MaxCode