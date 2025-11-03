# Use the official .NET 8.0 SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY Opal-tool.csproj ./
RUN dotnet restore Opal-tool.csproj

# Copy the rest of the source code
COPY . .

# Build the application
RUN dotnet publish Opal-tool.csproj -c Release -o /app/publish --no-restore

# Use the official .NET 8.0 ASP.NET runtime image for the final stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Set the entry point
ENTRYPOINT ["dotnet", "Opal-tool.dll"]
