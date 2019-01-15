FROM microsoft/aspnetcore-build:2.2 AS build
WORKDIR /app
 
COPY *.csproj ./
RUN /usr/share/dotnet/dotnet restore
 
COPY . ./
RUN /usr/share/dotnet/dotnet publish -c Release -o output
 
 
FROM microsoft/aspnetcore:2.2
WORKDIR /app
COPY --from=build /app/output .
 
EXPOSE 5000
ENV ASPNETCORE_URLS http://*:5000
ENV ASPNETCORE_ENVIRONMENT docker
 
ENTRYPOINT ["/usr/share/dotnet/dotnet", "TIGSajt.dll"]