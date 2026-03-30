FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy only csproj first
COPY EmployeeManagementAPI/*.csproj ./EmployeeManagementAPI/

# Restore
RUN dotnet restore ./EmployeeManagementAPI/EmployeeManagementAPI.csproj

# Copy everything
COPY . ./

# Build
RUN dotnet publish ./EmployeeManagementAPI/EmployeeManagementAPI.csproj -c Release -o out

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "EmployeeManagementAPI.dll"]