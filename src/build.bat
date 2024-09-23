SET DOTNET_CLI_TELEMETRY_OPTOUT=true
SET DOTNET_SKIP_FIRST_TIME_EXPERIENCE=true

dotnet.exe publish -p:PublishProfile=win-x64 --force
dotnet.exe publish -p:PublishProfile=win-x86 --force
dotnet.exe publish -p:PublishProfile=win-arm64 --force
dotnet.exe publish -p:PublishProfile=osx-x64 --force
dotnet.exe publish -p:PublishProfile=osx-arm64 --force
dotnet.exe publish -p:PublishProfile=linux-x64 --force
dotnet.exe publish -p:PublishProfile=linux-musl-x64 --force
dotnet.exe publish -p:PublishProfile=linux-arm --force
dotnet.exe publish -p:PublishProfile=linux-arm64 --force
dotnet.exe publish -p:PublishProfile=linux-bionic-x64 --force

del /F bin\\Release\\net8.0\\win-x64\\publish\\epicmorg.jira.issue.web.reporter.pdb
del /F bin\\Release\\net8.0\\win-x86\\publish\\epicmorg.jira.issue.web.reporter.pdb
del /F bin\\Release\\net8.0\\win-arm64\\publish\\epicmorg.jira.issue.web.reporter.pdb
del /F bin\\Release\\net8.0\\osx-x64\\publish\\epicmorg.jira.issue.web.reporter.pdb
del /F bin\\Release\\net8.0\\osx-arm64\\publish\\epicmorg.jira.issue.web.reporter.pdb
del /F bin\\Release\\net8.0\\linux-x64\\publish\\epicmorg.jira.issue.web.reporter.pdb
del /F bin\\Release\\net8.0\\linux-musl-x64\\publish\\epicmorg.jira.issue.web.reporter.pdb
del /F bin\\Release\\net8.0\\linux-arm\\publish\\epicmorg.jira.issue.web.reporter.pdb
del /F bin\\Release\\net8.0\\linux-arm64\\publish\\epicmorg.jira.issue.web.reporter.pdb
del /F bin\\Release\\net8.0\\linux-bionic-x64\\publish\\epicmorg.jira.issue.web.reporter.pdb

del /F bin\\Release\\net8.0\\win-x64\\publish\\appsettings.Development.json
del /F bin\\Release\\net8.0\\win-x86\\publish\\appsettings.Development.json
del /F bin\\Release\\net8.0\\win-arm64\\publish\\appsettings.Development.json
del /F bin\\Release\\net8.0\\osx-x64\\publish\\appsettings.Development.json
del /F bin\\Release\\net8.0\\osx-arm64\\publish\\appsettings.Development.json
del /F bin\\Release\\net8.0\\linux-x64\\publish\\appsettings.Development.json
del /F bin\\Release\\net8.0\\linux-musl-x64\\publish\\appsettings.Development.json
del /F bin\\Release\\net8.0\\linux-arm\\publish\\appsettings.Development.json
del /F bin\\Release\\net8.0\\linux-arm64\\publish\\appsettings.Development.json
del /F bin\\Release\\net8.0\\linux-bionic-x64\\publish\\appsettings.Development.json

type nul > bin/Release/net8.0/win-x64/publish/createdump.exe.ignore
type nul > bin/Release/net8.0/win-x86/publish/createdump.exe.ignore
type nul > bin/Release/net8.0/win-arm64/publish/createdump.exe.ignore
type nul > bin/Release/net8.0/osx-x64/publish/createdump.exe.ignore
type nul > bin/Release/net8.0/osx-arm64/publish/createdump.exe.ignore
type nul > bin/Release/net8.0/linux-x64/publish/createdump.exe.ignore
type nul > bin/Release/net8.0/linux-musl-x64/publish/createdump.exe.ignore
type nul > bin/Release/net8.0/linux-arm/publish/createdump.exe.ignore
type nul > bin/Release/net8.0/linux-arm64/publish/createdump.exe.ignore
type nul > bin/Release/net8.0/linux-bionic-x64/publish/createdump.exe.ignore

7z a -tzip -mx5 -r0 ./bin/epicmorg.jira.issue.web.reporter-net8.0-win-x64.zip ./bin/Release/net8.0/win-x64/publish/*
7z a -tzip -mx5 -r0 ./bin/epicmorg.jira.issue.web.reporter-net8.0-win-x86.zip ./bin/Release/net8.0/win-x86/publish/*
7z a -tzip -mx5 -r0 ./bin/epicmorg.jira.issue.web.reporter-net8.0-win-arm64.zip ./bin/Release/net8.0/win-arm64/publish/*
7z a -tzip -mx5 -r0 ./bin/epicmorg.jira.issue.web.reporter-net8.0-osx-x64.zip ./bin/Release/net8.0/osx-x64/publish/*
7z a -tzip -mx5 -r0 ./bin/epicmorg.jira.issue.web.reporter-net8.0-osx-arm64.zip ./bin/Release/net8.0/osx-arm64/publish/*
7z a -tzip -mx5 -r0 ./bin/epicmorg.jira.issue.web.reporter-net8.0-linux-x64.zip ./bin/Release/net8.0/linux-x64/publish/*
7z a -tzip -mx5 -r0 ./bin/epicmorg.jira.issue.web.reporter-net8.0-linux-musl-x64.zip ./bin/Release/net8.0/linux-musl-x64/publish/*
7z a -tzip -mx5 -r0 ./bin/epicmorg.jira.issue.web.reporter-net8.0-linux-arm.zip ./bin/Release/net8.0/linux-arm/publish/*
7z a -tzip -mx5 -r0 ./bin/epicmorg.jira.issue.web.reporter-net8.0-linux-arm64.zip ./bin/Release/net8.0/linux-arm64/publish/*
7z a -tzip -mx5 -r0 ./bin/epicmorg.jira.issue.web.reporter-net8.0-linux-bionic-x64.zip ./bin/Release/net8.0/linux-bionic-x64/publish/*

