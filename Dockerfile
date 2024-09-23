##################################################################
##################################################################
##################################################################
#                   Temp Build Layer
##################################################################
##################################################################
##################################################################
FROM mcr.microsoft.com/dotnet/sdk:8.0-bookworm-slim AS build

ENV DOTNET_CLI_TELEMETRY_OPTOUT=true
ENV DOTNET_SKIP_FIRST_TIME_EXPERIENCE=true
ARG DOTNET_VER=net8.0

COPY ["./src", "/tmp/src"]

WORKDIR /tmp/src

RUN cd /tmp/src && \
    mkdir -p /app/publish && \
    dotnet publish -p:PublishProfile=linux-x64 --force && \
    rm -rfv /tmp/src/bin/Release/${DOTNET_VER}/linux-x64/publish/epicmorg.jira.issue.web.reporter.pdb && \
    rm -rfv /tmp/src/bin/Release/${DOTNET_VER}/linux-x64/publish/appsettings.Development.json && \
	cp -rfv /tmp/src/bin/Release/${DOTNET_VER}/linux-x64/publish/* /app/publish

##################################################################
##################################################################
##################################################################
#                   Main Final Layer
##################################################################
##################################################################
##################################################################
FROM mcr.microsoft.com/dotnet/aspnet:8.0-bookworm-slim

LABEL maintainer="EpicMorg DevTeam, developer@epicm.org"
LABEL org.opencontainers.image.vendor="EpicMorg DevTeam, developer@epicm.org"
LABEL org.opencontainers.image.authors="STAM, kasthack, Aleks-Z"
LABEL org.opencontainers.image.source="https://github.com/EpicMorg/docker"
LABEL org.opencontainers.image.url="https://github.com/EpicMorg/docker"
LABEL donate.crypto.TON="EQDvHXRK-K1ZieJhgTD9JZQk7xCnWzRbctYnUkWq1lZq1bUg"
LABEL donate.crypto.ETH="0x26a8443a694f08cdfec966aa6fd72c45068753ec"
LABEL donate.crypto.BTC="bc1querz8ug9asjmsuy6yn4a94a2athgprnu7e5zq2"
LABEL donate.crypto.LTC="ltc1qtwwacq8f0n76fer2y83wxu540hddnmf8cdrlvg"
LABEL donate.crypto.NVC="4SbMynYETyhmKdggu8f38ULU6yQKiJPuo6"
LABEL donate.crypto.DOGE="DHyfE1CZzWtyaQiaMmv6g4KvXVQRUgrYE6"
LABEL donate.crypto.PPC="pQWArPzYoLppNe7ew3QPfto1k1eq66BYUB"
LABEL donate.crypto.RVN="R9t2LKeLhDSZBKNgUzSDZAossA3UqNvbV3"
LABEL donate.crypto.ZEC="t1KRMMmwMSZth8vJcd2ZHtPEFKTQ74yVixE"
LABEL donate.crypto.XMR="884PqZ1gDjWW7fKxtbaeRoBeSh9EGZbkqUyLriWmuKbwLZrAJdYUs4wQxoVfEJoW7LBhdQMP9cFhZQpJr6xvg7esHLdCbb1"
ARG DEBIAN_FRONTEND=noninteractive

##################################################################
#        Copy compilled app from dev stage and prepare
##################################################################
ENV DOTNET_CLI_TELEMETRY_OPTOUT=true
ENV DOTNET_SKIP_FIRST_TIME_EXPERIENCE=true

WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 5000
EXPOSE 443
EXPOSE 80

##################################################################
#                   Run app in foreground
##################################################################
ENTRYPOINT ["dotnet", "epicmorg.jira.issue.web.reporter.dll"]
