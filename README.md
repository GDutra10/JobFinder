# The Goal

The goal here is to create a job opportunity board, where the company register new opportunities and the worker can submit in these new opportunities.
The project was developed in .Net Core 3.1 on the backend and React JS for the front end. For the database was used MSSQLLocalDB. The authentication was used JWT.

# Technologies

Back-end - .Net Core 3.1<br>
Front-end - React JS<br>
Authentication - JWT<br>
ORM - Entity Framework Core<br>

# To run this project

1 - Adjust ConnectionStrings>JobFinderDB with the database connection.<br>
2 - Go to Package Manager Console window in VS, type "update-database" and press "Enter".<br>
3 - Run the project.<br>

# A little bit about the API
Each API consumption returns an object called APIResponse that contains:<br>
- Status (HttpStatus)<br>
- Title<br>
- Errors <br>
- Data (Object)<br><br>

Exp 1.: <br>
```json
{
  "status": 200,
  "title": "OK",
  "errors": null,
  "data": [
    {
      "idRole": 1,
      "nmRole": "Software Developer",
      "isActive": true
    },
    {
      "idRole": 2,
      "nmRole": "Front-End Developer",
      "isActive": true
    }]
}
```

Exp 2.: <br>
```json
{
  "status": 400,
  "title": "One or more validation errors occurred.",
  "errors": {
    "nmUser": [
      "Field is required"
    ],
    "deEmail": [
      "Field is required"
    ],
    "dePassword": [
      "Field is required"
    ],
    "dePasswordConfirm": [
      "Field is required"
    ]
  }
}
```
