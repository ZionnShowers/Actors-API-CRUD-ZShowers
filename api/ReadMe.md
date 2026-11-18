## 1. The request journey

```
Client (Postman, browser)
    | JSON: a DTO
    V
Controller (speaks HTTP. reads the request, picks the status code.)
    | DTO
    V
Services (The Rules / Logic. Converts DTO's to entities and back.)
    | Entity
    V
Repository (The only layer that talks to the database.)
    | Entity
    V
DbContext (Ef Core)
    | SQL
    V
Database
```

## 2. Recap of Day One

app.db we then created or models for our Database
AppDbContext (what allows our API to talk to our Data)
We ran our migrations (Mapped our Models / Entities to our DB table)
We then made our Repository Layer followed by our DTO.