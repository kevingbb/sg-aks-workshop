FROM mcr.microsoft.com/dotnet/sdk:3.1.405-alpine3.12 AS build-env
# Setup libman and PATH
RUN dotnet tool install -g Microsoft.Web.LibraryManager.Cli
ENV PATH="$PATH:/root/.dotnet/tools"
WORKDIR /app
# copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore -r linux-musl-x64
# copy client-side library dependencies and build
COPY ./libman.json ./
RUN libman restore
# copy remaining files
COPY ./Controllers/* ./Controllers/
COPY ./Hubs/* ./Hubs/
COPY ./Pages/* ./Pages/
COPY ./wwwroot/css/* ./wwwroot/css/
COPY ./wwwroot/images/* ./wwwroot/images/
COPY ./wwwroot/js/* ./wwwroot/js/
COPY ./bundleconfig.json ./
COPY ./Program.cs ./
COPY ./Startup.cs ./
COPY ./ImageClassifierTelemetryInitializer.cs ./
RUN dotnet publish -c Release -r linux-musl-x64 -o out --self-contained true /p:PublishTrimmed=true

# build runtime image
FROM mcr.microsoft.com/dotnet/runtime-deps:3.1.11-alpine3.12
# Add non-root user
RUN addgroup -g 99 appuser && adduser -S -u 99 appuser appuser
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:5000
EXPOSE 5000
COPY --chown=appuser:appuser --from=build-env /app/out ./
USER appuser
ENTRYPOINT ["./Classifier.Web"]
