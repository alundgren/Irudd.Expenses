# Irudd.Expenses

## Overview
- A basic expense tracker using a sql server datastore + exposing a dotnet core api.
- Authentication is handled using [ASP.NET Core Identity](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-8.0)

## Run the app (api/Irudd.Expenses.Api)

Run with:
> dotnet run

Note:
- You need sql server installed and exposed as localhost. Running the app will automatically create the database AAA_ExpensesDb.
- To use a different test database change the ExpensesDb connection string in appsettings.Development.json.

## Basic usage

For exact api details see the swagger documenation at:
https://localhost:7241/swagger/index.html

### Example workflow

Register a user:
> POST v1/identity/register

Aquire an access token:
> POST v1/identity/login

Get a list of all categories:
> GET v1/categories/all

Add expenses:
> POST v1/expenses/add

Get a list of the latest expenses:
> GET v1/expenses/latest

## Run the tests (api/Irudd.Expenses.Api.Test)

The tests uses the InMemory ef provider instead of sql server.

Run with:
> dotnet test

## Deployment

- Requires a connection string ExpensesDb to an sql server database

## Manage database with ef migrations:

Install tool:

> dotnet tool install --global dotnet-ef

Add migration:
> dotnet ef migrations add <name>

Undo last:
> dotnet ef migrations remove

Revert to specific when applied to db:
> dotnet ef database update <target after>
