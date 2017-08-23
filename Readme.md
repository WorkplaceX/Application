# Application Start

Use this repo to build a new database application with WorkplaceX framework. This repo contains an empty hello world application to get started with. Before installing see also [Prerequisite](https://github.com/WorkplaceX/Framework/wiki/Prerequisite).

## Clone

Clone git repository like this.

```cmd
git clone https://github.com/WorkplaceX/ApplicationStart.git --recursive
cd ApplicationStart
git submodule foreach git checkout master
```

Git command "git submodule foreach git checkout master" prevents detached head mode.

## Install
Run BuildTool "installAll" command. This downloads, installs Angular 4 and runs additional scripts.

```cmd
cd BuildTool
dotnet restore
dotnet run -- installAll
```	

## Start
As a first test serve the hello world application. You don't need a database connection for this.
```cmd
cd BuildTool
dotnet run -- serve
```	
	
Internet browser will first show a page not found error. Wait about 30 seconds! It will refresh automatically once ready.

## Next steps

Set up a database connection string like this: [BuildTool connection](https://github.com/WorkplaceX/Framework/wiki/BuildTool-connection).

* Create a simple SQL table:

```sql
CREATE TABLE HelloWorld
(
	Id INT PRIMARY KEY IDENTITY,
  	Text NVARCHAR(256),
	Number FLOAT
)
```	

* Run the BuildTool generate command (See also [BuildTool generate](https://github.com/WorkplaceX/Framework/wiki/BuildTool-generate)). This generates a C# class for every table and every column.

```cmd
cd BuildTool
dotnet run -- generate
```	

* Run the BuildTool runSql command (See [BuildTool runSql](https://github.com/WorkplaceX/Framework/wiki/BuildTool-runSql)). It adds "Framework" prefixed tables to the database. These tables are used internally by the framework.

```cmd
cd BuildTool
dotnet run -- runSql
```	

* Run BuildTool runSqlTable command (See [BuildTool runSqlTable](https://github.com/WorkplaceX/Framework/wiki/BuildTool-runSqlTable)). In C# declared classes are written for example the table "FrameworkColumn". It serves as meta data for configuration.
