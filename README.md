# Build an app using .NET Core Console App and Azure Cosmos DB's API for MongoDB

This example shows you how to build a **Console App using .NET Core** and **Azure Cosmos DB's API for MongoDB**.

To use this example, you must:

- [Create](https://docs.microsoft.com/en-us/azure/cosmos-db/create-mongodb-dotnet#create-account) a Cosmos account configured to use Azure Cosmos DB's API for MongoDB. 
- Retrieve your [connection string](https://docs.microsoft.com/en-us/azure/cosmos-db/connect-mongodb-account) information.

OR

- Use Azure Cosmos DB Emulator (More info [here](https://docs.microsoft.com/en-us/azure/cosmos-db/local-emulator?tabs=cli%2Cssl-netstd21#azure-cosmos-dbs-api-for-mongodb)).
- Use connection string `mongodb://localhost:C2y6yDjf5%2FR%2Bob0N8A7Cgv30VRDJIWEHLM%2B4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw%2FJw%3D%3D@localhost:10255/admin?ssl=true`

# Usage

- Clone the code
- Update the connection string in appsettings.json
- Run the app

The application showcases how to connect to Azure Cosmos DB's API for MongoDB using .NET Core. It will create a FamiliesDB and Families collection. Insert, search, update and delete 1 record.

