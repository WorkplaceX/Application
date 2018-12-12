CREATE TABLE HelloWorld
(
	Id INT PRIMARY KEY IDENTITY,
	Text NVARCHAR(256),
	Number FLOAT,
)

CREATE TABLE LoginUser
(
	Id INT PRIMARY KEY IDENTITY,
	UserName NVARCHAR(256),
	Password NVARCHAR(256),
	Email NVARCHAR(256),
	Value FLOAT,
	ValueUOM NVARCHAR(256),
	IsActive BIT NOT NULL
	INDEX IX_LoginUser UNIQUE (Id, UserName)
)

CREATE TABLE LoginUserRole
(
	Id INT PRIMARY KEY IDENTITY,
	RoleName NVARCHAR(256),
	IsActive BIT NOT NULL
	INDEX IX_LoginUser UNIQUE (RoleName)
)

CREATE TABLE LoginUserUserRole
(
	Id INT PRIMARY KEY IDENTITY,
	UserId INT FOREIGN KEY REFERENCES LoginUser(Id) NOT NULL,
	UserRoleId INT FOREIGN KEY REFERENCES LoginUserRole(Id) NOT NULL,
	IsActive BIT NOT NULL
	INDEX IX_LoginUserUserRole UNIQUE (UserId, UserRoleId)
)

GO
CREATE VIEW LoginUserRoleDisplay AS
SELECT
	[User].Id AS UserId,
	UserRole.Id AS UserRoleId,
	UserRole.RoleName AS UserRoleName,
	UserUserRole.Id AS UserUserRoleId,
	UserUserRole.IsActive AS IsActive
FROM
	LoginUser [User]
CROSS JOIN
	LoginUserRole UserRole
LEFT JOIN
	LoginUserUserRole UserUserRole ON (UserUserRole.UserId = [User].Id AND UserUserRole.UserRoleId = UserRole.Id)