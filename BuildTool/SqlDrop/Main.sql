IF NOT EXISTS(SELECT * FROM sys.tables WHERE name = 'HelloWorld') RETURN

ALTER TABLE [dbo].[Attribute] 
DROP CONSTRAINT FK_X

ALTER TABLE [dbo].[AttributeNote]
DROP CONSTRAINT FX_Y

DROP TABLE [dbo].[HelloWorld]
DROP TABLE [dbo].[Attribute]
DROP TABLE [dbo].[AttributeNote]