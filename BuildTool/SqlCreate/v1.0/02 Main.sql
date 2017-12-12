IF NOT EXISTS(SELECT * FROM FrameworkVersion WHERE Name = 'Application' AND Version = 'v1.0') BEGIN SELECT 'RETURN' RETURN END -- Version Check

CREATE TABLE HelloWorld
(
	Id INT PRIMARY KEY IDENTITY,
  	Text NVARCHAR(256),
	Number FLOAT
)
