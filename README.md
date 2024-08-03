# IN4315 (Software Architecture) Team 14: DUO

## About The Project

DUO is a government body which is responsible for, among other tasks, providing student financing. DUO stated that their system for student financing is deprecated and needs to be modernised. This causes problems such as a lack of knowledge to maintain the system, and the inability to adapt to the (political) environment.
DUO is improving their systems via the IT-project ‘Doorontwikkelen Applicatielandschap Bekostiging’ (DAB). Bureau ICT-Toetsing (BIT) indicated drawbacks in their current approach, which utilises the software ‘Blueriq’, namely dependence on an external party and uncertainty of the suitability and future-proofness of the platform. Therefore, they were suggested to review their solution.
We want to look into what the software architecture of an in-house solution can be like for DUO.

### Key Features

- **Student finance** applications in various forms: basic grants, supplementary grants
- **Student travel product** arrangements
- **Student debt** tracking
- **Student loan** requests in various forms: tuition fees loans and interest bearing loans


### Built With

- *.NET 8.0*
    - Testing frameworks: *Moq 4.20.70, Coverlet*
- *React.js*
- *Coverlet*
- *SwaggerUI*
- *MySQL*

## Getting Started

### Prerequisites

- **.NET SDK**: The .NET 8.0 SDK is required to build and run the microservices. Download it from the [official .NET download page](https://dotnet.microsoft.com/download).
- **Node.js**: Latest version (as of now 20.12.0) required for the frontend application, can be downloaded on [nodejs.org](https://nodejs.org/en/download). This also comes with **npm**.


To run the system locally you will need to have MySQL installed on your machine. You can download the distribution [here](https://dev.mysql.com/downloads/installer/)
You will also need the following NuGet packages: 

- MySQL.Data
- MySQL.EntityFrameworkCore
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Tools
- Pomelo.EntityFrameworkCore.MySQL

### Installation

To get a local copy up and running, follow these steps:

1. **Clone the repository**:
   
   ```bash
   git clone https://gitlab.ewi.tudelft.nl/in4315/2023-2024/groups/team-14.git
   ```

2. **Install Dependencies**

    Each microservice may have its dependencies. Ensure to navigate into each microservice's directory and restore the dependencies:

    ```bash
    cd [path/to/microservice]
    dotnet restore
    ```
    
    Repeat for each microservice directory.

## Usage

To run a microservice, navigate to its respective directory and use the `dotnet run` command. Here is how:

1. Open a terminal or command prompt.

2. Navigate to the microservice directory:

    ```bash
    cd [path/to/microservice]
    ```
3. Run the microservice:

    ```bash
    dotnet build
    dotnet run
    ```
The microservice should now be running and accessible on its designated ports or endpoints.

4. Run the frontend application:

```bash
npm start
```

The above command runs the app in development mode. Open [http://localhost:3000](http://localhost:3000) to view it in your browser. The page will reload when you make changes. You may also see any lint errors in the console.

```bash
npm run build
```

The above command builds the app for production to the `build` folder. It correctly bundles React in production mode and optimizes the build for the best performance.
The build is minified and the filenames include the hashes.



### Testing

For running the backend tests, navigate to the test folder of the respective microservice e.g. `"Services/StudentProductMicroservice.Tests"`.
From here, run `dotnet test`.

For testing the frontend React application, run `npm test`. 
This launches the test runner in the interactive watch mode. See this section about [running tests](https://facebook.github.io/create-react-app/docs/running-tests) for more information.

### Test Coverage

To run the code coverage tool Coverlet, you can either run view a quick overview in the terminal, or generate a more extensive html report.

To view a quick overview in the terminal, run the command `dotnet test /p:CollectCoverage=true` in the folder where the respective tests are located.

To generate a html report:

1. In the folder where the tests are located run the command `dotnet test --collect:"XPlat Code Coverage;Format=json,lcov,cobertura"`. This command will generate a file containing the code coverage details.
2. To generate a html file of the report, navigate to the folder where the file resulting from the previous command is created. Then run the command: `dotnet reportgenerator "-reports:coverage.cobertura.xml" "-targetdir:coveragereport" -reporttypes:Html`.   A folder called 'coveragereport' will be created. In there, you can open the index.html file to view the report.

### Database Setup

To connect to the database, each microservice contains a connection string in the appsettings.json file. It specifies the server location, the database name, the user name and the password. Make you locally create the database with the correct name. For example, for the StudentProduct microservice, create a database in your MySQL server called 'student_product'. Make sure you change the value for the user and password as well, according to your own MySQL settings.

Furthermore, we make use of database migrations provided by the installed NuGet packages. There are existing migrations available under the 'Migrations' folder in each microservice.
You can use them to generate the required tables in the database by using the `Update-Database` command in the NuGet Package Manager console in Visual Studio. Alternatively you can use `dotnet ef database update` command in the .NET core command line interface.

If you wish to generate your own migrations, say for example because you have changed a model, you can use the command `Add-Migration YourMigrationName` or `dotnet ef migrations add YourMigrationName`.

### API Endpoints Documentation with SwaggerUI

We used the OpenAPI documentation format and the SwaggerUI tool to document the API endpoints of the microservices.
To be able to view the documentation, run a microservice and from the base path, navigate to `/swagger`.


## Contributing

### Bug Reports & Feature Recommendations

Please open an issue to report any bugs or feature requests.


### Development

MRs are welcome! To start developing, perform these steps:

- Clone the repository using the instructions above
- Start the microservice you wish to work on in localhost




<!-- ## License

... -->

## Contributors

- Suzanne Backer
- Gerben Bultema
- Nadine Kuo
- Erwin Li
