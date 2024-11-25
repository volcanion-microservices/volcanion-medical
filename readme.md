# WARNING
## Nuget package server:
Change `NUGET_SOURCE` in `Dockerfile` to `ip of nuget package management server`

## Entity Framework
### 1. Add migration:
```
dotnet ef migrations add InitialCreate
```

### 2. Update database:
```
dotnet ef database update
```

### 3. Update dotnet-ef:
```
dotnet tool update --global dotnet-ef
```
