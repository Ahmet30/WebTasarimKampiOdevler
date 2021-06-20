CREATE PROCEDURE Notes_GetAllNote
AS
	BEGIN
		SELECT * FROM
		Notes
	END

GO

CREATE PROCEDURE Notes_DeleteById
(
	@Id INT
)
AS
	BEGIN
		DELETE FROM
		Notes
		WHERE ID = @Id
	END

GO

CREATE PROCEDURE Notes_SaveOrUpdate
(
	@Id INT
	,@NoteHeader NVARCHAR(200)
	,@NoteContent NVARCHAR(1100)
	,@DateTime DATETIME
	,@IsStar SMALLINT
)
AS
	BEGIN

		IF @Id > 0
			BEGIN
				--Update Statement
				UPDATE Notes SET
					NoteHeader = @NoteHeader
					,NoteContent = @NoteContent
					,[DateTime] = @DateTime
					,IsStar = @IsStar
				WHERE ID = @Id
			END
		ELSE
			BEGIN
				--INSERT STATEMENT
				INSERT INTO Notes
				(
					NoteHeader
					,NoteContent
					,DateTime
					,IsStar
				)
				VALUES
				(
					@NoteHeader 
					,@NoteContent
					,@DateTime 
					,@IsStar 
				)
			END
	END

GO

CREATE PROCEDURE Notes_GetById
(
	@Id INT
)
AS
	BEGIN
		SELECT * FROM Notes
		WHERE ID = @Id
	END

GO

CREATE PROCEDURE Notes_SearchNotes
(
	@Filter NVARCHAR(300)
)
AS
	BEGIN
		SELECT * FROM Notes
		WHERE NoteHeader LIKE '%' + @Filter + '%' 
			OR NoteContent LIKE '%' + @Filter + '%'
	END

GO

CREATE PROCEDURE Notes_GetByStar
(
	@star SMALLINT
)
AS
	BEGIN
		SELECT * FROM Notes
		WHERE IsStar = @star
	END

GO



