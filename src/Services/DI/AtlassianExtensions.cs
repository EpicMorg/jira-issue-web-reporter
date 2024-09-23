namespace epicmorg.jira.issue.web.reporter.Services.DI
{
    using Atlassian.Jira;
    using epicmorg.jira.issue.web.reporter.Models.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using System;

    public static class AtlassianExtensions
    {
        public static IServiceCollection AddJira(this IServiceCollection services)
        {
            services.AddScoped<Jira>(context =>
            {
                var jiraOptions = context.GetService<IOptions<JiraConfig>>().Value;


                var client = jiraOptions.AuthType switch
                {
                    JiraAuthTypes.Basic => Jira.CreateRestClient(
                         url: jiraOptions.Domain,
                         username: jiraOptions.JiraAuthTypeBasic.Login,
                         password: jiraOptions.JiraAuthTypeBasic.Password,
                        settings: new JiraRestClientSettings
                        {
                            Cache = new Atlassian.Jira.JiraCache(),
                        }),
                    JiraAuthTypes.OAuth => Jira.CreateOAuthRestClient(
                         url: jiraOptions.Domain,
                         consumerKey: jiraOptions.JiraAuthTypeOAuth.ConsumerKey,
                         consumerSecret: jiraOptions.JiraAuthTypeOAuth.ConsumerSecret,
                         oAuthAccessToken: jiraOptions.JiraAuthTypeOAuth.AccessToken,
                         oAuthTokenSecret: jiraOptions.JiraAuthTypeOAuth.TokenSecret),
                    _ => throw new Exception($"Invalid auth type!: {jiraOptions.AuthType}")
                };

                return client;
            });
            return services;
        }
    }
}
