# MyProject

## Overview
MyProject is a full-stack web application with a backend built using ASP.NET Web API and a frontend built using Angular.

## Prerequisites
- [.NET SDK](https://dotnet.microsoft.com/download) (version 5.0 or later)
- [Node.js](https://nodejs.org/) (version 12 or later)
- [Angular CLI](https://angular.io/cli) (installed globally via npm)

## Getting Started

### Backend Setup (ASP.NET Web API)
1. **Navigate to the project directory**:
    ```bash
    cd MyProject
    ```
2. **Restore dependencies and build the project**:
    ```bash
    dotnet restore
    dotnet build
    ```
3. **Run the backend**:
    ```bash
    dotnet run
    ```

### Frontend Setup (Angular)
1. **Navigate to the Angular project directory**:
    ```bash
    cd ClientApp
    ```
2. **Install Angular dependencies**:
    ```bash
    npm install
    ```
3. **Serve the Angular application**:
    ```bash
    ng serve
    ```

### Running the Application


To run the application in development mode:
1. **Start the backend**:
    ```bash
    dotnet run
    ```
2. **Start the frontend**:
    ```bash
    cd ClientApp
    ng serve
    ```

### Deployment
To deploy the application:
1. **Build the Angular application**:
    ```bash
    cd ClientApp
    ng build --prod
    ```
2. **Publish the ASP.NET Core application**:
    ```bash
    dotnet publish -c Release
    ```


## Contributing
Contributions are welcome! Please submit a pull request or open an issue to discuss any changes.

## License
This project is licensed under the MIT License.
