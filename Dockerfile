FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src-temp
COPY ["./src/epicmorg.jira.issue.web.reporter", "jwr/"]
RUN dotnet restore "jwr/epicmorg.jira.issue.web.reporter.csproj"

COPY . .
WORKDIR "/src-temp/jwr"
RUN cat ./epicmorg.jira.issue.web.reporter.csproj  && \
    dotnet build "./epicmorg.jira.issue.web.reporter.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./epicmorg.jira.issue.web.reporter.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "epicmorg.jira.issue.web.reporter.dll"]
EXPOSE 5000
EXPOSE 80