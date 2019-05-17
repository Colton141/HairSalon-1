# _Hair Salon_

#### _This program will create a list of hair stylists that have their own list of clients. 5/17/2019_

#### By _**Mathew Akre**_

## Description

_This program is in the eyes of an employee. The employee will be able to hire a hair stylist. They will also be able to give the hair stylists clients. The information for the hair stylists and the clients will be stored and retrieved from a SQL database._

## Recreate SQL Database
```sql
CREATE DATABASE mathew_akre;
USE mathew_akre;
CREATE TABLE stylist (id serial PRIMARY KEY, name VARCHAR(255));
CREATE TABLE clients (id serial PRIMARY KEY, name VARCHAR(255), stylist_id INT(11));
```

## Specs
|Behavior|Input|Output|
|-|-|-|
|Enter a name for stylist should return same name|"Liv"|"Liv"|
|Enter a name for client should return same name|"Jesse"|"Jesse"|
|Click View Stylists anchor tag will redirect user to view all stylists. If no stylists have been added, list will be blank. |Click View Stylist|Redirects to Stylists index page|
|Click the Hire anchor tag to redirect to a stylist hire form page|Click Hire Stylist|Redirects to stylists New.cshtml page|
|Click name of Stylist to redirect to client list page|Click "Lilly"|Redirects to "Lilly's" client list page|
|Click Add new Client to redirect to new client form page|Click Add Client|Redirect to Client New.cshtml page|
|Click Add client button to redirect back to Client list page|Click Add Client|Redirect to Client list page|



## Setup/Installation Requirements

* _Clone Github repository_
* _Install .Net Core SDK 2.2.203_
* _Install MAMP_
* _Install Mono_
* _Open the command line_
* _Change the directory to the Desktop_
* _Change the directory to the file HairSalon.Solution_
* _Open MAMP and start server_
* _Open WebSart page if it didn't open already_
* _Click on tools drop down menu bar and click on phpMyAdmin_
* _Click on the Import tab to import the database from this project_
* _Or redirect to the Recreate SQL Database section to make the Database_
* _Change the directory to HairSalon_
* _Use the Command dotnet run and copy the localhost5000 link_
* _Paste the link into the url to open the Webpage_

## Known Bugs
* _Adding multiple clients to the same stylist without returning back to the stylist page first can give clients a id of 0 which will not give them a stylist_

## Support and contact details

_mwakre29@gmail.com_

## Technologies Used

_C#_
_SQL Database_
_.NetCore SDK_
_MAMP_
_Mono_

### License

*This project is under the MIT License*

Copyright (c) 2019 **_Mathew Akre_**
