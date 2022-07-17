# ASP.NET Core Web API + Reactjs app

## Running instructions

You will have to install the [.NET 6.0 SDK](https://dotnet.microsoft.com/download) and [Node.js](https://nodejs.org/en/).

## Configuring the WebApi to use SQL Server
1. Ensure your connection strings in `appsettings.json` point to a local SQL Server instance.
1. Ensure the tool EF was already installed. You can find some help [here](https://docs.microsoft.com/ef/core/miscellaneous/cli/dotnet)
    ```
    dotnet tool update --global dotnet-ef
    ```

1. Open a command prompt in the Web folder and execute the following commands:
    ```
    dotnet restore
    dotnet tool restore
    dotnet ef database update -c applicationdbcontext -p ../Infrastructure/Infrastructure.csproj -s WebApi.csproj
    ```

1. Run the application.
    The first time you run the application, it will seed the databases with data such that you should see something in the store.
    Note: If you need to create migrations, you can use these commands:

    ```
    -- create migration (from WebApi folder CLI)
    dotnet ef migrations add InitialModel --context applicationdbcontext -p ../Infrastructure/Infrastructure.csproj -s WebApi.csproj -o Data/Migrations
    ```
## Running the sample

- To start the back end you just go to the `WebApi` folder in a terminal window and run `dotnet run` from there.
- Now you should be able to browse to Web API at `https://localhost:5099/swagger` or `https://localhost:5106/swagger`.
- From here you can create a new background job.
- You can manage background jobs at `https://localhost:5099/hangfire` or `https://localhost:5106/hangfire`.
- To start the front end, you can go to the `client` directory and run `npm install` to install the dependencies, and `npm run dev` for local run.
- Now you should be able to browse to React app at `http://localhost:3000`.
- This command might take a while on the first run, since it also installs all the dependencies required by ASP.NET and React.

[<img src="https://user-images.githubusercontent.com/20091577/179419863-2d8b4c82-ded3-4ebb-85da-5e0b41b70c3d.PNG" width="100%" height="100%" />]
[<img src="https://user-images.githubusercontent.com/20091577/179419864-eca4775a-80a8-49d1-beaa-a6193bbdbc65.PNG" width="100%" height="100%" />]
[<img src="https://user-images.githubusercontent.com/20091577/179419858-47e02c69-6584-41eb-8cdb-cde710529b2a.PNG" width="100%" height="100%" />]
### Testing

- To run the back end unit tests, navigate to `UnitTests` and run `dotnet test`.
- To run the front end unit tests, navigate to `client` and run `npm test`.

## Running the WebApi using Docker

First, go to the `backend` directory and build the release version of the app:

```sh
dotnet publish -c Release
```

You can now build a Docker image using the [Dockerfile] in the `backend` directory.

If you want to test the build production image locally, you can also use [docker-compose](https://docs.docker.com/compose/):

```sh
docker-compose up
```

