FROM mcr.microsoft.com/dotnet/core/aspnet:3.1  AS runtime
COPY publish/ productCategories/
WORKDIR /productCategories
EXPOSE 8005
ENTRYPOINT ["dotnet", "DiabetesControl.dll"]