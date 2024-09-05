namespace epicmorg.jira.issue.web.reporter.Services.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using epicmorg.jira.issue.web.reporter.Models;
    using epicmorg.jira.issue.web.reporter.Models.Check.Response;
    using epicmorg.jira.issue.web.reporter.Models.Configuration;
    using epicmorg.jira.issue.web.reporter.Models.Create.Request;
    using epicmorg.jira.issue.web.reporter.Models.Create.Response;
    using epicmorg.jira.issue.web.reporter.Services.Interfaces;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    public class JiraService : IJiraService
    {
        private readonly HashSet<string> allowedProjects;
        private readonly IJiraClient client;
        private readonly ILogger<JiraService> logger;
        private readonly IJiraCache cache;

        public JiraService(
            ILogger<JiraService> logger,
            IJiraClient client,
            IOptions<JiraConfig> options,
            IJiraCache cache)
        {
            this.allowedProjects = new HashSet<string>(options.Value.AllowedProjects, StringComparer.OrdinalIgnoreCase);
            this.client = client;
            this.logger = logger;
            this.cache = cache;
        }

        public async Task<CreateResponse> CreateIssue(CreateJiraRequest request, CancellationToken cancellationToken)
        {
            if (!this.IsAllowedProject(request.Project))
            {
                this.logger.LogWarning("Tried to access disabled project {project} when creating issue {issue}", request.Project, request);
                return new CreateResponse
                {
                    IssueKey = null,
                    Message = "Invalid project",
                    Success = false,
                };
            }

            try
            {
                var result = await this.client.CreateIssue(request, cancellationToken).ConfigureAwait(false);
                return new CreateResponse
                {
                    Message = "Issue created!",
                    IssueKey = result,
                    Success = true,
                };
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Failed to create jira issue: {request}", request);
                return new CreateResponse
                {
                    IssueKey = null,
                    Message = "Failed to create an issue",
                    Success = false,
                };
            }
        }

        public async Task<IReadOnlyList<ProjectDto>> GetProjects(CancellationToken cancellationToken)
        {
            try
            {
                var cached = await this.cache.GetProjects().ConfigureAwait(false);
                if (cached != null)
                {
                    return cached;
                }

                var result = await this.client.GetProjects(cancellationToken).ConfigureAwait(false);
                result = result.Where(a => this.IsAllowedProject(a.Key)).ToList();

                await this.cache.SetProjects(result).ConfigureAwait(false);
                return result;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Failed to get projects for the desk form");
                throw;
            }
        }

        public async Task<CheckResponse> GetIssueStatus(string issue, CancellationToken cancellationToken)
        {
            var project = issue.Split('-').First();
            if (!this.IsAllowedProject(project))
            {
                this.logger.LogWarning("Tried to access disabled project {project} for issue {issue}", project, issue);
                return new CheckResponse
                {
                    Resolution = "No such issue",
                    Status = "Invalid project",
                    Success = false,
                    Issue = issue,
                };
            }

            CheckResponse result;
            try
            {
                var cached = await this.cache.GetIssueStatus(issue).ConfigureAwait(false);

                var status = await this.client.GetIssueStatus(issue, cancellationToken).ConfigureAwait(false);
                result = new CheckResponse
                {
                    Resolution = status.Resolution,
                    Status = status.Status,
                    Success = true,
                };
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Failed to check status for issue {issue}", issue);
                result = new CheckResponse
                {
                    Resolution = null,
                    Status = null,
                    Success = false,
                };
            }

            await this.cache.SetIssue(issue, result).ConfigureAwait(false);
            return result;
        }

        private bool IsAllowedProject(string key) => this.allowedProjects.Contains(key);
    }
}
