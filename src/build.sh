#!/usr/bin/env bash

export DOTNET_CLI_TELEMETRY_OPTOUT=true
export DOTNET_SKIP_FIRST_TIME_EXPERIENCE=true

dotnet publish -p:PublishProfile=win-x64 --force
dotnet publish -p:PublishProfile=win-x86 --force
dotnet publish -p:PublishProfile=win-arm64 --force
dotnet publish -p:PublishProfile=osx-x64 --force
dotnet publish -p:PublishProfile=osx-arm64 --force
dotnet publish -p:PublishProfile=linux-x64 --force
dotnet publish -p:PublishProfile=linux-musl-x64 --force
dotnet publish -p:PublishProfile=linux-arm --force
dotnet publish -p:PublishProfile=linux-arm64 --force

rm -rfv ./bin/Release/net8.0/win-x64/publish/epicmorg.jira.issue.web.reporter.pdb
rm -rfv ./bin/Release/net8.0/win-x86/publish/epicmorg.jira.issue.web.reporter.pdb
rm -rfv ./bin/Release/net8.0/win-arm64/publish/epicmorg.jira.issue.web.reporter.pdb
rm -rfv ./bin/Release/net8.0/osx-x64/publish/epicmorg.jira.issue.web.reporter.pdb
rm -rfv ./bin/Release/net8.0/osx-arm64/publish/epicmorg.jira.issue.web.reporter.pdb
rm -rfv ./bin/Release/net8.0/linux-x64/publish/epicmorg.jira.issue.web.reporter.pdb
rm -rfv ./bin/Release/net8.0/linux-musl-x64/publish/epicmorg.jira.issue.web.reporter.pdb
rm -rfv ./bin/Release/net8.0/linux-arm/publish/epicmorg.jira.issue.web.reporter.pdb
rm -rfv ./bin/Release/net8.0/linux-arm64/publish/epicmorg.jira.issue.web.reporter.pdb

rm -rfv ./bin/Release/net8.0/win-x64/publish/appsettings.Development.json
rm -rfv ./bin/Release/net8.0/win-x86/publish/appsettings.Development.json
rm -rfv ./bin/Release/net8.0/win-arm64/publish/appsettings.Development.json
rm -rfv ./bin/Release/net8.0/osx-x64/publish/appsettings.Development.json
rm -rfv ./bin/Release/net8.0/osx-arm64/publish/appsettings.Development.json
rm -rfv ./bin/Release/net8.0/linux-x64/publish/appsettings.Development.json
rm -rfv ./bin/Release/net8.0/linux-musl-x64/publish/appsettings.Development.json
rm -rfv ./bin/Release/net8.0/linux-arm/publish/appsettings.Development.json
rm -rfv ./bin/Release/net8.0/linux-arm64/publish/appsettings.Development.json

touch ./bin/Release/net8.0/win-x64/publish/createdump.ignore
touch ./bin/Release/net8.0/win-x86/publish/createdump.ignore
touch ./bin/Release/net8.0/win-arm64/publish/createdump.ignore
touch ./bin/Release/net8.0/osx-x64/publish/createdump.ignore
touch ./bin/Release/net8.0/osx-arm64/publish/createdump.ignore
touch ./bin/Release/net8.0/linux-x64/publish/createdump.ignore
touch ./bin/Release/net8.0/linux-musl-x64/publish/createdump.ignore
touch ./bin/Release/net8.0/linux-arm/publish/createdump.ignore
touch ./bin/Release/net8.0/linux-arm64/publish/createdump.ignore

7z a -tzip -mx5 -r0 ./bin/epicmorg.jira.issue.web.reporter-net8.0-win-x64.zip ././bin/Release/net8.0/win-x64/publish/*
7z a -tzip -mx5 -r0 ./bin/epicmorg.jira.issue.web.reporter-net8.0-win-x86.zip ././bin/Release/net8.0/win-x86/publish/*
7z a -tzip -mx5 -r0 ./bin/epicmorg.jira.issue.web.reporter-net8.0-win-arm64.zip ././bin/Release/net8.0/win-arm64/publish/*
7z a -tzip -mx5 -r0 ./bin/epicmorg.jira.issue.web.reporter-net8.0-osx-x64.zip ././bin/Release/net8.0/osx-x64/publish/*
7z a -tzip -mx5 -r0 ./bin/epicmorg.jira.issue.web.reporter-net8.0-osx-arm64.zip ././bin/Release/net8.0/osx-arm64/publish/*
7z a -tzip -mx5 -r0 ./bin/epicmorg.jira.issue.web.reporter-net8.0-linux-x64.zip ././bin/Release/net8.0/linux-x64/publish/*
7z a -tzip -mx5 -r0 ./bin/epicmorg.jira.issue.web.reporter-net8.0-linux-musl-x64.zip ././bin/Release/net8.0/linu-musl-x64/publish/*
7z a -tzip -mx5 -r0 ./bin/epicmorg.jira.issue.web.reporter-net8.0-linux-arm.zip ././bin/Release/net8.0/linux-arm/publish/*
7z a -tzip -mx5 -r0 ./bin/epicmorg.jira.issue.web.reporter-net8.0-linux-arm64.zip ././bin/Release/net8.0/linux-arm64/publish/*

