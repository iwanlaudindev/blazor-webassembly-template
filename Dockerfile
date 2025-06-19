FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY ["src/Water.Management.Client/Water.Management.Client.csproj", "Water.Management.Client/"]
COPY ["src/Water.Management.Application/Water.Management.Application.csproj", "Water.Management.Application/"]
COPY ["src/Water.Management.Shared/Water.Management.Shared.csproj", "Water.Management.Shared/"]

RUN dotnet restore "Water.Management.Client/Water.Management.Client.csproj"
COPY . .
WORKDIR /src
RUN dotnet build "src/Water.Management.Client/Water.Management.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "src/Water.Management.Client/Water.Management.Client.csproj" -c Release -o /app/publish

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY nginx.conf /etc/nginx/nginx.conf