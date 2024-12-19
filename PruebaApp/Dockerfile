#Imagen para archivo docker
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /SRC


#Copiar los archivos del proyecto
COPY *.csproj ./
RUN dotnet restore


#Copiar el resto de archivos
COPY . ./
RUN dotnet publish -c Release -o /app

#Imagen base para cuando se ejecute
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app .

#Exponer el puerto donde se ejecuta la aplicacion
EXPOSE 80


ENTRYPOINT ["dotnet", "PruebaApp.dll"]
