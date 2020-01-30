FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /build
ENV MAIN_APP WebApp

# Copy csproj and restore
COPY ./*.sln ./
# src projects
COPY src/*/*.csproj ./
RUN sh -c 'for file in *.csproj; do mkdir -p src/${file%.*} && mv $file src/${file%.*}; done'
# test projects, don't want to publish with failing tests
COPY test/*/*.csproj ./
RUN sh -c 'for file in *.csproj; do mkdir -p test/${file%.*} && mv $file test/${file%.*}; done'
RUN dotnet restore

COPY ./src ./src
COPY ./test ./test

RUN dotnet test
RUN dotnet publish ./src/$MAIN_APP/$MAIN_APP.csproj -c Release -o /out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
EXPOSE 80
WORKDIR /app
COPY --from=build-env /out ./

ENTRYPOINT ["dotnet", "WebApp.dll"]
