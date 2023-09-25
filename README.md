# Blazor Wall Area Calculator
A blazor server application to calculate the area of walls in a room, demonstrating the use of cascading grids.  Syncfusion controls are used for components such as grids.  The database used is SQLite and Dapper the library used to communicate with the database.

To protect my Syncfusion licence key appsettings.json has been EXCLUDED from the files on Githum and must be added to any downloaded solution.  An example appsettings.json is shown below:

{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "SyncfusionLicenceKey": "Your Actual Syncfusion Licence Key",
  "ConnectionStrings": {
    "Default": "Data Source=.\\Data\\WallAreaCalc.db;Version=3;"
  }
}
