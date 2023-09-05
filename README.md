# cqrs_example
CQRS (Command and Query Responsibility Segregation) example written in .Net 7 illustrating how to use CQRS for reading and writing data.

CONTENTS OF THIS FILE
---------------------
   
 * Project Structure
 * Running
 * Tests
 * Next steps

  PROJECT STRUCTURE
--------------------

* Controllers - Web API around core business logic
* Data - Data layer for Database (Currently SQLite)
* Migrations - Files for managing the database schema
* Models - Database entities managed by this API


RUNNING
--------------------
1. Clone repo
2. Run `dotnet build`
3. Run `dotnet run`

TESTS
--------------------
This project contains both unit and integration tests

NEXT STEPS
--------------------
Replace SQLite with a production ready database (MS SQL, mySQL, Postgres)
Add Repository layer abstracting DbContext
Extend PUT endpoint to support more than just adding birth (i.e. Gender, GivenName, SurName, DeathDate, DeathLocation)
- Rename `RecordBirthCommand` to `UpdatePersonCommand`
