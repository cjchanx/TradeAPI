# TradeAPI
Trading Data API Project.

To run the project open TradingDB.sln inside Visual Studio 2022 (for compatability support with ASP.NET CORE 6). Press the continue buttons from the top to compile and run the program. A webpage will be available under https://localhost:7182 which links to the project. For API calls, direct them at the same URL. For instance https://localhost:7182/api/orders/GetOrders.

In order to ensure a connection to a properly configured database, setup a database using the TradeDB.sql file. Once the database is setup, point to it inside Webservice/appsettings.json inside the connection string in the following format
```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "Database": {
        "ConnectionString": "Server = {hostname}; Port = 3306; Database = TradeDB; Uid = {username}; Pwd={password};"
  }
}
```
The included appsettings.json file includes a connection to a temporary database which was used during the development process, and is configured with the current version of the SQL schema.

# Dependencies
Uses Libraries available from NuGet.
- **Newtonsoft.Json**   *TradingDB and Webservice*
- **MySQL.Data**    *DatabaseLibrary*
- **Microsoft.AspNetCore.Mvc.NewtonsoftJson** *Webservice*
- Nest

For internal dependencies, right click Dependencies for a particular project and add the required dependencies.
- DatabaseLibrary references TradingLibrary
- WebService references DatabaseLibrary and TradingLibrary

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
4. Setup [Entity]Helper.cs inside Webservice.ControllerHelpers
5. Inside Webservice.Controllers setup [Entity[s]]Controller.cs with all API endpoints.

# Frontend Information
The following section includes important information and general guidelines for contributing to this project's frontend.

# Providers Information
Currently implemented for a MySQL database with extensible support for other databases possible.

# API Information
Following is sample API calls and basic documentation
## ORDERS API
```
DELETE
https://localhost:7182/api/orders/DeleteOrder/{Id}
GET
https://localhost:7182/api/orders/GetOrders
https://localhost:7182/api/orders/GetOrdersByAccount/{AccountId}
POST
https://localhost:7182/api/orders/UpdateOrders
https://localhost:7182/api/orders/AddOrders
>> Use "Body->Raw->JSON" and following JSON Object
{
    "accountref": 1,
    "action": 3,
    "targetprice": 2.0,
    "datecreated": "2021-12-09T17:38:26",
    "quantity": 0,
    "status": 1,
    "symbol": "FB",
    "broker": "IBKR"
}

PUT
https://localhost:7182/api/orders/EditOrder/{Id}
{
    "accountref": 3,
    "action": 4,
    "targetprice": 2.0,
    "datecreated": "2021-12-09T17:38:26",
    "quantity": 0,
    "status": 1,
    "symbol": "FB",
    "broker": "IBKR"
}
```
## ACCOUNT SUMMARY API
```
GET
https://localhost:7182/api/account_summary/GetAccount_Summary
https://localhost:7182/api/account_summary/GetAccount_Summary/{Id}
DELETE
https://localhost:7182/api/account_summary/DeleteAccount_Summary/{Id}
POST
https://localhost:7182/api/account_summary/AddAccount_Summary
{
    "accountref": 3,
    "availablefunds": 431.0,
    "grosspositionvalue": 1.0,
}
PUT
https://localhost:7182/api/account_summary/EditAccountSummary/{Id}
{
    "availablefunds": 10000000000,
    "grosspositionvalue": 1.0,
}
```
## BROKER API
```
GET
https://localhost:7182/api/brokers/GetBrokers
DELETE
https://localhost:7182/api/brokers/DeleteBroker
{
    "name": "For Deletion"
}
POST
https://localhost:7182/api/brokers/AddBrokers
{
    "name": "Test Broker",
    "website": "https://www.testbrokerpostman.com/"
}
```
## COMMISSIONS API
```
GET
https://localhost:7182/api/commissions/GetCommissions
DELETE
https://localhost:7182/api/commissions/DeleteCommission
{
    "broker": "ASPX",
    "type": 1
}
https://localhost:7182/api/commissions/DeleteByBroker
{
    "broker": "ASPX"
}
POST
https://localhost:7182/api/commissions/AddCommission
{
    "broker": "ASPX",
    "type": 0,
    "rate": 1.5
}
```
