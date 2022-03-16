Don't need to perform these steps. Follow below steps to generate EntityFramework classes
- Set LMS.API as startup project
- Open paackage Manager console and se default project to LMS.DAL
- run this command "Scaffold-DbContext -Connection name=LMSSaaS -OutputDir Models Microsoft.EntityFrameworkCore.SqlServer -Context "LMSSaaSContext" -Force"


API
- Using Visual Studio to run API project
- Right click on API project and click on "Manage User Secrets"
- copy content from "secrets.json" file to new file which is opened in previous step
- Debug the code
- copy url i.e. localhost/<<Portnumber>>.. we need this for angular project

Angular project
- go to powershell/command prompt and go to project directory i.e. ..\Movies\Web\ClientApp\ and run 
commands
- restore all the packages installed in the project i.e. run "npm install" command
- run npm i - install npm module
- npm run ng serve - to run the project
- use api url and update ..\Movies\Web\ClientApp\src\environments\environment.ts
- Don't need token at this time

Database
- restore Database backup provided MovieDB.baks. rename the file to MovieDB.bak
- create user movieuser with password movieuser to access the database from API
- You can add more Genre to ListValues table with CategoryId = 1

How to Use Front end
- Click on "Movies" link, it will load all the movies in the databse to the page in the grid
- Click on "Add" button to add new movie(s)
- Click on any row in the grid to update that movie
- Click on "Add" button in "Add to Cart" column to add item to the cart
- Click on "My cart" on top right to see the items in the cart, where user can edit/delete those items. If there is 
no items in the cart then user will navigate back to Movie list page