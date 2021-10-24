FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
COPY --from=mcr.microsoft.com/dotnet/sdk:6.0 /usr/share/dotnet /usr/share/dotnet
WORKDIR /src
COPY ["./MP.Devember.RLaunch.csproj", "./"]
RUN dotnet tool install --global LibSassBuilder-Tool
RUN dotnet restore "./MP.Devember.RLaunch.csproj"
COPY . .
RUN dotnet build "MP.Devember.RLaunch.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MP.Devember.RLaunch.csproj" -c Release -o /app/publish

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY ./nginx.conf /etc/nginx/nginx.conf
