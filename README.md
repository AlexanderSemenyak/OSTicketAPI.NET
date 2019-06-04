# OSTIcketAPI.NET
This project has been created to add more support for interfacing with OSTicket. The current web API provided my OSTicket is sufficient for submitting new Tickets. However, we found that we wanted to be able to do more with the data inside of OSTicket.

# Getting Started

Your project must support at least `.Net Standard 2.0`

## Getting started

This .NET Library tries to make setup as painless as possible.

First, you want to update your `appsettings.json` to include the appropriate configuration.

~~~
{
    "OSTicket": {
        "BaseUrl": "https://YourOSTicketInstance.net/",
        "ApiKey": "__YourOSTicketApiKey__",
        "DatabaseConnectionString": "server=SERVERNAME;uid=USERID;pwd=PASSWORD;database=osticket;Convert Zero Datetime=True"
    }
}
~~~

> Note: It is recommended to leave the `Convert Zero Date` as true.

Add the following to your .Net Core `startup.cs` file in the `ConfigureServices`

~~~
services.AddOSTicketServices();
~~~

Its __Important__ to remember that you need to have the OSTicket configuration in the root off your `appsettings.json` because that is where this helper expects it to be.

However, if you want to embed your setting in a deeper part of your appsettings or get your settings from somewhere else entirely, then you will have to initialize the [`OSTicketInstance`](OSTicketAPI.NET/OSTicketInstance.cs) class yourself.


## Q/A

##### Why do I need an API key if we are accessing the database directly?

When you are creating a new ticket using the official API, all the approrpriate triggers for emails and notifications will be sent as well. If we just manually inject those records into the database, then these triggers would never occur.
