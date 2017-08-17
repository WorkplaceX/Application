# Application Start

Use this repo to build a new database application with WorkplaceX framework. This repo contains an empty application to get started with.

## Clone

Clone the git repository like this.

```	
git clone https://github.com/WorkplaceX/ApplicationStart.git
cd ApplicationDemo
git submodule init
git submodule update
```

The command "git submodule init" and "update" downloads the source code of the WorkplaceX framework component.

## Install
Run the BuildTool "installAll" command. This downloads, installs Angular 4 and runs additional scripts.

```	
cd BuildTool
dotnet restore
dotnet run -- installAll
```	

## Test
As a first test serve the hello world application. You don't need a database connection for this.
```	
dotnet run -- serve
```	
	
Internet browser will first show a page not found error. Wait about 30 seconds! It will refresh automatically once ready.


