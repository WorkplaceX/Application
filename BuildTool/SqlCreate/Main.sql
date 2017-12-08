IF EXISTS(SELECT * FROM sys.tables WHERE name = 'HelloWorld') RETURN

CREATE TABLE [dbo].[HelloWorld](
	[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Text] [nvarchar](256) NULL,
	[Number] [float] NULL
) 

CREATE TABLE [dbo].[Attribute](
	[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[HelloWorldId] [int] NOT NULL,
	[Name] [nvarchar](256) NULL,
	[Text] [nvarchar](256) NULL,
)

ALTER TABLE [dbo].[Attribute]  
ADD CONSTRAINT FK_X
FOREIGN KEY ([HelloWorldId]) REFERENCES [dbo].[HelloWorld] ([Id])

CREATE TABLE [dbo].[AttributeNote](
	[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[AttributeId] [int] NOT NULL,
	[Text] [nvarchar](256) NULL,
)

ALTER TABLE [dbo].[AttributeNote]
ADD CONSTRAINT FX_Y 
FOREIGN KEY([AttributeId]) REFERENCES [dbo].[Attribute] ([Id])