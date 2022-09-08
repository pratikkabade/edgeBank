# backend

### Prerequisites

1. [.NET 5.0](https://dotnet.microsoft.com/en-us/download/dotnet/5.0#:~:text=May%2010%2C%202022-,Build%20apps%20%2D%20SDK,-Tooltip%3A%20Do%20you)

2. [SQL-Server](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16#:~:text=Free%20Download%20for%20SQL%20Server%20Management%20Studio%20(SSMS)%2018.12.1)

3. [SSMS](https://www.microsoft.com/en-in/sql-server/sql-server-downloads#:~:text=Download%20now-,Express,-SQL%20Server%202019)

### To Start 

1. make changes in *DefaultConnection* of `appsettings.json` 
    ```json
      "ConnectionStrings": {
        "DefaultConnection": "server=WINDOWS11\\SQLEXPRESS;database=Backend;trusted_connection=true;"
      },
    ```

2. For Database connection 

    Build the solution

    ```powershell
    dotnet ef migrations add InitialCreate --context DataContext --output-dir Migrations
    ```

    ```powershell
    dotnet ef database update
    ```

    Expected output

    ```output
    Build started...
    Build succeeded.
    Done.
    ```

    ```             
    dotnet run
    ```

3. Use `postman` or `thunderclient` further
    
    1. **POST** method
    
    ```
    https://localhost:5001/api/authenticate
    ```

    There in *basic auth* use these credentials

    ```http
    UserName: admin@gmail.com
    Password: Passcode1
    ```

    ##### `Token` would be generated

    <details>
    <summary>Screenshots</summary>
    <img  src="https://user-images.githubusercontent.com/76637730/185432139-1499ed0d-742e-49b5-871c-08b974b9127e.png"> <br>       
    Response <br> 
    <img  src="https://user-images.githubusercontent.com/76637730/185439279-51db7471-c966-4dcb-bfb0-5f64e4cb1eac.png"> 
    </details>

### thunderclient Log

  #### 1. `POST` login
```
https://localhost:5001/api/authenticate
```

  #### 2. `GET` all_users
```
https://localhost:5001/api/authenticate
```

  #### 3. `GET` first_users
```
https://localhost:5001/api/administration/1
```

  #### 4. `POST` create_user
```
https://localhost:5001/api/authenticate
```

  #### 5. `DELETE` first_users
```
https://localhost:5001/api/authenticate/1
```

---

  #### 1. `GET` all_cashflow
```
https://localhost:5001/api/CashFlow/
```

  #### 2. `GET` first_cashflow
```
https://localhost:5001/api/CashFlow/1
```

  #### 3. `POST` create_cashflow
```
https://localhost:5001/api/authenticate
```

---

  #### 1. `GET` all_users
```
https://localhost:5001/api/products
```

  #### 1. `POST` first_users
```
https://localhost:5001/api/products/1
```

  #### 3. `DELETE` first_users
```
https://localhost:5001/api/products/1
```

---

  #### 1. `GET` all_messages
```
https://localhost:5001/api/contact/
```

  #### 1. `GET` first_messages
```
https://localhost:5001/api/contact/1
```