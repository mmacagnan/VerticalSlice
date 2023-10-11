# Vertical Slice
## _A useful library to simplify .net web apis creation_

This library implements the following packages
- Mediatr
- FluentValidation

## Features

- Api errors handling
- Validators automaticaly injected and called
- Automatic response status code management

## How to use it
- Add the following code to the services
```cs
services.AddInfraStructure(Configuration, typeof(Startup).Assembly);
```

- Add the following code to the application builder
```cs
app.AddInfrastructureMiddlewares();
```
