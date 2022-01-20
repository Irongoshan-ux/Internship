## CarMarket

CarMarket is a project based on Blazor framework. It includes such important topics as Clean Architecture Layers, authorization and JWT authentication using IdentityServer4, Claims-based authentication, Unit tests (xUnit). It also has some sample pages showing how you can link Database, API and UI in the solution.

## Installation

### Prerequisites

- IDE - Visual Studio or VS Code
- Framework - .NET 5
- Database - MS SQL Server

### Clone

- Clone this repo to your local machine using `https://github.com/Irongoshan-ux/CarMarket.git`

### Setup

> CarMarket Database
- Create database `CarMarketDb` in SQL Server
- Copy the `Connection String` property from `CarMarketDb`
- Paste this value into the `CarMarketDb` connection string in the `appsettings.json` file in `CarMarket.Server`
- Add `MultipleActiveResultSets=true` to the end of this connection string

> CarMarket Web API
- Open Solution `CarMarket/CarMarket.sln`
- Set startup project: `CarMarket.Server`
- Run Project through Visual Studio/Terminal
- Go to `https://localhost:10000/swagger` or `https://localhost:10001/swagger`

> Blazor WebAssembly App
- Open Solution `CarMarket/CarMarket.sln`
- Set startup projects: `CarMarket.Server` & `CarMarket.UI`
- Run Project through Visual Studio/Terminal
- Go to `https://localhost:5001/`
- Click on login. You should get logged in as `admin@gmail.com`. Password: `qwe`
