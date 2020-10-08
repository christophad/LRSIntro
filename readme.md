Demo Project that uses .Net Core as backend API and Angular 10 on the front end.
In order to run the project follow the steps
1)Run createDB.sql on a local db
2)Modify the appsettings.json file on LRSIntro project in order the connection string to point at your db
3)Start the API. The swagger will start at https://localhost:44308/swagger/index.html
4) Optional. If your API starts at a different port, modify the apiURL on the environment.ts file on the Front end project accordingly
5) On the front end project npm install and ng serve and the app will be up and running
