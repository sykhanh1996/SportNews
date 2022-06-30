# SportNews
Sport Information

Add-Migration InitialCreate -Context ApplicationDbContext

## Application URLs:
- Identity STS: https://localhost:5001
- News API: https://localhost:5002
- Identity API: https://localhost:5003
- News Admin: https://localhost:6001
- News Portal: https://localhost:6002
- Identity Admin: https://localhost:6003


## Docker Command Examples
Web blazor and MongoDb for Sport
- docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Admin@123" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2017-latest

- docker ps or docker container ls
- docker run -d  --name mongodb -e MONGO_INITDB_ROOT_USERNAME=mongoadmin -e MONGO_INITDB_ROOT_PASSWORD=Admin@123 -p 127.0.0.1:27017:27017 mongo


terminal: 
- docker-compose -f docker.yml up
- docker-compose up

## Application URLs:
- Identity: https://localhost:5001
- SportInfor: https://localhost:5002
- SportInfor Admin: https://localhost:6001
- SportInfor Portal: https://localhost:6002

Install:
# In Identity.API
1, IdentityServer4
2, IdentityServer4.AspNetIdentity
3, IdentityServer4.EntityFramwork
4, IdentityServer4.Storage
5, IdentityServer4.EntityFramwork.Storage
6, Microsoft.EntityFrameworkCore
7, Microsoft.EntityFrameworkCore.SqlServer
8, Microsoft.EntityFrameworkCore.Identity.EntityFrameworkCore
9, Microsoft.EntityFrameworkCore.Tool
10, Polly
11, Serilog
12, Serilog.AspNetCore
13, Serilog.Sink.Http

# In SportInfor.Domain
14, MediatR 
15, MongoDB.Bson

# In SportInfor.Infrastructure
16, mongoDb.Driver
17, Polly
18, Microsoft.Extensions.Logging.Abstrctions
19, Microsoft.Extensions.Options

# In SportInfor.Application
20, MediatR 
21, mongoDb.Driver
22, AutoMapper
23, Automapper.Extensions.Microsoft.DependencyInjection

# In SportInfor.API
24, AutoMapper
25, Automapper.Extensions.Microsoft.DependencyInjection
26, MediatR
27, MediatR.Extensions.Microsoft.DependencyInjection
27, mongoDb.Driver
28, MudBlazor

Ep 25: samwalpole.com/using-scoped-services-inside-singletons