IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = 'ListCategory'))
BEGIN
    CREATE TABLE [dbo].ListCategory (
    [Id]							Int IDENTITY(1,1) NOT NULL,
	[Name]							NVarChar(500) NOT NULL,	
	CreatedOn						DateTime2(7) NOT NULL,
	CreatedUserId					Int NULL,
	UpdatedOn						DateTime2(7) NOT NULL,
	UpdatedUserId					Int NULL,
    CONSTRAINT [PK_ListCategory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
Go

IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = 'ListValues'))
BEGIN
    CREATE TABLE [dbo].ListValues (
    [Id]							Int IDENTITY(1,1) NOT NULL,
	[Name]							NVarChar(500) NOT NULL,
	IsActive						Bit Not Null,
	ListCategoryID					Int Not Null,
	CreatedOn						DateTime2(7) NOT NULL,
	CreatedUserId					Int NULL,
	UpdatedOn						DateTime2(7) NOT NULL,
	UpdatedUserId					Int NULL,
    CONSTRAINT [PK_ListValues] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
Go

IF (OBJECT_ID('dbo.FK_ListValue_ListCategory', 'F') IS NULL)
BEGIN
	ALTER TABLE [ListValues] ADD CONSTRAINT FK_ListValue_ListCategory
	FOREIGN KEY (Id)   REFERENCES ListValues (Id)
END
Go

IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = 'Movie'))
BEGIN
    CREATE TABLE [dbo].Movie (
    [Id]							Int IDENTITY(1,1) NOT NULL,
	[Name]							NVarChar(500) NOT NULL,
	[Cost]							Numeric(10, 4) Not null,
	[SalePrice]						Numeric(10, 4) Not null,
	GenreId							Int Not Null,
	CreatedOn						DateTime2(7) NOT NULL,
	CreatedUserId					Int NULL,
	UpdatedOn						DateTime2(7) NOT NULL,
	UpdatedUserId					Int NULL,
    CONSTRAINT [PK_Movie] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
Go

IF (OBJECT_ID('dbo.FK_Movie_ListValues', 'F') IS NULL)
BEGIN
	ALTER TABLE Movie ADD CONSTRAINT FK_Movie_ListValues
	FOREIGN KEY (GenreId)   REFERENCES ListValues (Id)
END
Go

If Not Exists (Select Top 1 1 From ListCategory Where Id = 1)
Begin
	Set Identity_Insert ListCategory On
	Insert Into ListCategory
			(Id, [Name], [CreatedUserId], [CreatedOn], [UpdatedUserId], [UpdatedOn])
	Values	(1, 'Movie Genre', 1, GETUTCDATE(), 1, GETUTCDATE())
	Set Identity_Insert ListCategory Off
End

If Not Exists (Select Top 1 1 From ListValues Where Id = 1)
Begin
	Set Identity_Insert ListValues On
	Insert Into ListValues
			(Id, [Name], [ListCategoryId], [IsActive], [CreatedUserId], [CreatedOn], [UpdatedUserId], [UpdatedOn])
	Values	(1, 'Action', 1, 1, 1, GETUTCDATE(), 1, GETUTCDATE())
	Set Identity_Insert ListValues Off
End