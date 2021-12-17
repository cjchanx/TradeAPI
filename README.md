# TradeAPI
Trading Data API Project.

# Dependencies
Uses Libraries available from NuGet.
- **Newtonsoft.Json**   *TradingDB*
- **MySQL.Data**    *DatabaseLibrary*
- Nest

For internal dependencies, right click Dependencies for a particular project and add the required dependencies.
- DatabaseLibrary references TradingLibrary
- WebService references DatbaseLibrary and TradingLibrary

# Backend Information
The following section includes important information and general guidelines for contributing to this project's backend.
## General Workflow For Implementing Entities
1. Inside TradingLibrary.Models create a new model named [Entity (eg. Account)].cs

    This model should include regions:
    ```
        Constructors : Default, Parameterized
        Properties : All Attributes. Use JsonProperty as follows.
                    [JsonProperty(PropertyName = "active")]
                    public bool Active { get; set; }
        Methods : Relevant Methods.
   ```
2. Inside DatabaseLibrary.Models create a new [Entity (eg. Account)]_db.cs

    This model should include regions:
    ```
        Constructors : Default, Parameterized
        Properties: All database attributes.
    ```
3. Inside DatabaseLibrary.Helpers create a new [Entity (eg. Account)]Helper_db.cs

    This Helper should include relevant database entries and requirements for the class, and include the following:
    ```
        Add() method : returns a [Entity]_db object and takes in parameters for the object, to add a new instance.
                     should do the following
                        - Validate data (make sure values are defined)
                        - Generate a new [Entity]_db instance
                        - Add the row to the database
                        - Return the instance upon success, null otherwise
        GetCollection() method : can be different filters based on specific needs of the entity, in general
                                 returns List<[Entity]_db> of queried objects from the database.
    ```

# Frontend Information
The following section includes important information and general guidelines for contributing to this project's frontend.

# Providers Information
Currently implemented for a MySQL database with extensible support for other databases possible.
