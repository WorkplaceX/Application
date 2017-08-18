# Application Start

Use this repo to build a new database application with WorkplaceX framework. This repo contains an empty application to get started with. Before installing see also [Prerequisite](https://github.com/WorkplaceX/Framework/wiki/Prerequisite).

## Clone

Clone git repository like this.

```cmd
git clone https://github.com/WorkplaceX/ApplicationStart.git --recursive
cd ApplicationStart
git submodule foreach git checkout master
```

After git command "git submodule foreach git checkout master" submodule is no more in detached head mode.

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
dotnet run -- serve
```	
	
Internet browser will first show a page not found error. Wait about 30 seconds! It will refresh automatically once ready.

## Next steps

Now let's set up a database connection like this: [Database Connection](https://github.com/WorkplaceX/Framework/wiki/Database-Connection).

Next we need to create a simple SQL table:

```sql
CREATE TABLE HelloWorld
(
	Id INT PRIMARY KEY IDENTITY,
  	Text NVARCHAR(256),
	Number FLOAT
)
```	

Run the BuildTool command generate

```cmd
dotnet run -- generate
```	


