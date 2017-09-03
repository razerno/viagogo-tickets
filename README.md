# Viagogo Tickets
An ASP.NET MVC demo project to demonstrate access to the viagogo API with an example unit test. The application lists all events for a fixed category ID. The user can then select to view all the tickets available for a particular event.

The event with the lowest price for each country has the price highlighted in bold. As a simplification, the assumption has been made that all events within a particular country have their prices listed in the same currency.

Before running the application, the client variables need to be added to the project's user secrets as below:
```
{
  "CLIENT_ID": "<client_id>",
  "CLIENT_SECRET": "<client_secret>",
  "CATEGORY_ID": <category_id>
}
```
