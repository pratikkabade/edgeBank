# backend

### To run, 

1. make changes in *DefaultConnection* of `appsettings.json` 
    ```json
      "ConnectionStrings": {
        "DefaultConnection": "server=WINDOWS11\\SQLEXPRESS;database=Backend;trusted_connection=true;"
      },
    ```

2. then run following commands in terminal

    ```
    dotnet build
    ```

    ```
    dotnet ef migrations add InitialCreate --context DataContext --output-dir Migrations
    ```

    ```
    dotnet run
    ```

### To start, 

1. go to `postman` or `thunderclient` 

  1. **POST** - http://localhost:4000/users/register

    ```json
    {
        "firstName": "john",
        "lastName": "doe",
        "MobileNumber": "100",
        "AadharCard": "10002",
        "PAN_Card": "PAN95",
        "Address": "MH, India",
        "Nominee": "NA",
        "username": "john",
        "password": "111111"
    }
    ```

  2. **POST** - http://localhost:4000/users/authenticate

    ```json
    {
      "username":"john",
      "password":"111111"
    }
    ```

  3. **GET** - http://localhost:4000/users/

    Use the token generated in authenticate operation

