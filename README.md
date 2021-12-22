# TradeAPI
Trading Data API Project.

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
( TO TEST )
POST
( TO TEST )
```
