FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS base
EXPOSE 8080

WORKDIR /app
COPY . .

ENTRYPOINT [ "dotnet" , "ProductService.dll"]