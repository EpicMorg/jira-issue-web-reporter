namespace epicmorg.jira.issue.web.reporter.Services.DI
{
    using Atlassian.Jira;
    using epicmorg.jira.issue.web.reporter.Models.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;

    public static class AtlassianExtensions
    {
        public static IServiceCollection AddJira(this IServiceCollection services)
        {
            services.AddScoped<Jira>(context =>
            {
                var jiraOptions = context.GetService<IOptions<JiraConfig>>().Value;
                var client = Jira.CreateRestClient(
                    jiraOptions.Domain,
                    jiraOptions.Login,
                    jiraOptions.Password,
                    new JiraRestClientSettings
                    {
                        Cache = new Atlassian.Jira.JiraCache(),
                    });
                return client;
            });
            return services;
        }
    }
}
