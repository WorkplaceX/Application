IF NOT EXISTS (SELECT * FROM FrameworkVersion WHERE Name = 'Application')
INSERT INTO FrameworkVersion (Name, Version)
SELECT 'Application', 'v0.0'
