#  [![Activity](https://img.shields.io/github/commit-activity/m/EpicMorg/jira-issue-web-reporter?label=commits&style=flat-square)](https://github.com/EpicMorg/jira-issue-web-reporter/commits) [![GitHub issues](https://img.shields.io/github/issues/EpicMorg/jira-issue-web-reporter.svg?style=popout-square)](https://github.com/EpicMorg/jira-issue-web-reporter/issues) [![GitHub forks](https://img.shields.io/github/forks/EpicMorg/jira-issue-web-reporter.svg?style=popout-square)](https://github.com/EpicMorg/jira-issue-web-reporter/network) [![GitHub stars](https://img.shields.io/github/stars/EpicMorg/jira-issue-web-reporter.svg?style=popout-square)](https://github.com/EpicMorg/jira-issue-web-reporter/stargazers)  [![Size](https://img.shields.io/github/repo-size/EpicMorg/jira-issue-web-reporter?label=size&style=flat-square)](https://github.com/EpicMorg/jira-issue-web-reporter/archive/master.zip) [![Release](https://img.shields.io/github/v/release/EpicMorg/jira-issue-web-reporter?style=flat-square)](https://github.com/EpicMorg/jira-issue-web-reporter/releases) [![GitHub license](https://img.shields.io/github/license/EpicMorg/jira-issue-web-reporter.svg?style=popout-square)](LICENSE.md) [![Changelog](https://img.shields.io/badge/Changelog-yellow.svg?style=popout-square)](CHANGELOG.md)

## CI Status
<...>

## Description [![ko-fi](https://www.ko-fi.com/img/githubbutton_sm.svg)](https://ko-fi.com/B0B81CUI4)

Jira Quick Issue Creator - webtool for quick creation and checking issues from Jira instance by customers.

## Supported Platforms:
* Checked with `Jira Server and DataCenter` editions and versions from `7.x` to `10.x` with `JiraAuthTypeBasic`.
* Cloud versions `technically` supported via `JiraAuthTypeOAuth` but *NOT* tested.

# Full `appsettings.json` example:

```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*",
  "Jira": {
    "Domain": "",
    "AuthType": "Basic", //Oauth
    "JiraAuthTypeBasic": {
      "Login": "",
      "Password": ""
    },
    "JiraAuthTypeOAuth": {
      "ConsumerKey": "",
      "ConsumerSecret": "",
      "AccessToken": "",
      "TokenSecret": ""
    },
    "AllowedProjects": [
      "",
      ""
    ],
    "AllowedIssueTypes": [
      "",
      ""
    ]
  },
  "UI": {
    "Theme": "",
    "LogoUrl": "",
    "HeaderText": "",
    "DescriptionText": "",
    "LicensedTo": ""
  },
  "Captcha": {
    "key": "",
    "secret": ""
  }
}

```

### Descriptions of some options

* `AuthType` - kind of auth type. `Basic` or `OAuth`. How to setup `OAuth` - described [here](https://developer.atlassian.com/server/jira/platform/oauth/).
* `Captcha` - is optionan section. Official google Captcha docs [here](https://www.google.com/recaptcha/about/).
* `AllowedProjects` - list of allowed projects to connect. Use `Jira's Project Key`.
* `AllowedIssueTypes` - list of allowed project types to connect. Kind of `Bug`, `Task`, etc. Get names from your Jira Admin section of instance.

### Logging
File `appSettings.json` contains additional settings, like [loglevel](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.logging.loglevel?view=dotnet-plat-ext-5.0#fields) and [console output theme](https://github.com/serilog/serilog-sinks-console). You can set it up via editing this file.

#### Supported log levels
| Level | Enum | Description
|-------------|:-------------:|-------------|
| `Critical` | `5` | Logs that describe an unrecoverable application or system crash, or a catastrophic failure that requires immediate attention.
| `Debug`	| `1` | Logs that are used for interactive investigation during development. These logs should primarily contain information useful for debugging and have no long-term value.
| `Error` | `4` | Logs that highlight when the current flow of execution is stopped due to a failure. These should indicate a failure in the current activity, not an application-wide failure.
| `Information` | `2` | Logs that track the general flow of the application. These logs should have long-term value.
| `None` | `6` | Not used for writing log messages. Specifies that a logging category should not write any messages.
| `Trace`	| `0` | Logs that contain the most detailed messages. These messages may contain sensitive application data. These messages are disabled by default and should never be enabled in a production environment.
| `Warning` | `3` | Logs that highlight an abnormal or unexpected event in the application flow, but do not otherwise cause the application execution to stop.


### Themes:

Set theme in UI section of appsettings.json:

```
"UI": {
    "Theme": "default",
```
 of via enviroment-file vars or compose-file:
```
    - UI__Theme: default
```

Themes:
* `cerulean`
* `cosmo`
* `cyborg`
* `darkly`
* `default`
* `flatly`
* `journal`
* `litera`
* `lumen`
* `lux`
* `materia`
* `minty`
* `morph`
* `pulse`
* `quartz`
* `sandstone`
* `simplex`
* `sketchy`
* `slate`
* `solar`
* `spacelab`
* `superhero`
* `united`
* `vapor`
* `yeti`
* `zephyr`

You cah check live demos at official https://bootswatch.com/ site.

## Setup envs

1. Read officia Microsoft docs [here](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-8.0) 
2. Setup.

### ENV example (docker-compose.yml):

```
	- AllowedHosts: "*"
	<...>
	- UI__LicensedTo: "Me"
	- UI__Theme: "darkly"
	- UI__HeaderText: "Header"
	- UI__DescriptionText: "Description"
	<...>
	- Jira__Domain: "https://my-selfhosted-jira.local"
	- Jira__AuthType: "Basic"
	- Jira__AuthType__JiraAuthTypeBasic__Login: "my-user"
	- Jira__AuthType__JiraAuthTypeBasic__Password: my-user-password"
	- Jira__AllowedProjects__0: "KEY0"
	- Jira__AllowedProject__1: "KEY1"
	- Jira__AllowedIssueTypes__0: "Bug"
	- Jira__AllowedIssueTypes__1: "Support"
	- Jira__AllowedIssueTypes__2: "Feedback"
	- Jira__AllowedIssueTypes__3: "Story"
	<...>
	- Captcha__key: "key"
	- Captcha__secret: "secret"
	<...>
```

####  example

```


```

# Used componets:
| Compoment                       | Link                                               | Version    |
|---------------------------------|-----------------------------------------------------|-----------|
| .NET 8 (ASP.NET Core)           | [dotnet.microsoft.com](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) | 8.0       |
| jQuery                          | [jquery.com](https://jquery.com/)                   | 3.7.1     |
| jQuery Localization Plugin      | [github.com/coderifous/jquery-localize](https://github.com/coderifous/jquery-localize) | 0.2.0     |
| jQuery Validation Plugin        | [github.com/jquery-validation/jquery-validation](https://github.com/jquery-validation/jquery-validation) | 1.19.3    |
| jQuery Validation Unobtrusive   | [github.com/aspnet/jquery-validation-unobtrusive](https://github.com/aspnet/jquery-validation-unobtrusive) | 3.2.12    |
| Bootstrap                       | [getbootstrap.com](https://getbootstrap.com/)       | 5.3.3     |
| Bootswatch                      | [bootswatch.com](https://bootswatch.com/)           | 5.3       |
| FontAwesome                     | [fontawesome.com](https://fontawesome.com/)         | 6.6.0     |
| Flaticon (Freepik)              | [flaticon.com/authors/freepik](https://www.flaticon.com/authors/freepik) | -         |

Если нужно внести изменения или дополнить информацию, дайте знать!

# [Stargazers](https://github.com/EpicMorg/jira-issue-web-reporter/stargazers)

# [Forkers](https://github.com/EpicMorg/jira-issue-web-reporter/network/members)

# &#8627; Special Thanks:

* [@kasthack](https://github.com/kasthack)

# :money_with_wings: Donate

You could support us if you want.

| Adress   | Name | Coin 
| ------  | ------ | ------ 
| `EQDvHXRK-K1ZieJhgTD9JZQk7xCnWzRbctYnUkWq1lZq1bUg` | Toncoin | TON
| `0x26a8443a694f08cdfec966aa6fd72c45068753ec` | Ethereum | ETH
| `bc1querz8ug9asjmsuy6yn4a94a2athgprnu7e5zq2` | Bitcoin | BTC	
| `ltc1qtwwacq8f0n76fer2y83wxu540hddnmf8cdrlvg` | Litecoin | LTC	
| `4SbMynYETyhmKdggu8f38ULU6yQKiJPuo6` | Novacoin | NVC 
| `DHyfE1CZzWtyaQiaMmv6g4KvXVQRUgrYE6` | Dogecoin | DOGE	
| `pQWArPzYoLppNe7ew3QPfto1k1eq66BYUB` | Peercoin | PPC	
| `R9t2LKeLhDSZBKNgUzSDZAossA3UqNvbV3` | Ravencoin | RVN	
| `t1KRMMmwMSZth8vJcd2ZHtPEFKTQ74yVixE` | ZCash | ZEC	
| `884PqZ1gDjWW7fKxtbaeRoBeSh9EGZbkqUyLriWmuKbwLZrAJdYUs4wQxoVfEJoW7LBhdQMP9cFhZQpJr6xvg7esHLdCbb1` | Monero | XMR	
