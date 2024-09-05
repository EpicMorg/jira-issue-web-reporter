namespace epicmorg.jira.issue.web.reporter.Services.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Atlassian.Jira;
    using epicmorg.jira.issue.web.reporter.Models;
    using epicmorg.jira.issue.web.reporter.Models.Check.Response;
    using epicmorg.jira.issue.web.reporter.Models.Create.Request;
    using epicmorg.jira.issue.web.reporter.Services.Interfaces;
    using Microsoft.Extensions.Logging;

    public class JiraClient : IJiraClient
    {
        private readonly ILogger<JiraClient> logger;
        private readonly Jira jira;

        public JiraClient(
            ILogger<JiraClient> logger,
            Jira jira)
        {
            this.logger = logger;
            this.jira = jira;
        }

        public async Task<CheckResponse> GetIssueStatus(string issueId, CancellationToken cancellationToken)
        {
            var issue = await this.jira.Issues.GetIssueAsync(issueId, cancellationToken).ConfigureAwait(false);
            var result = new CheckResponse
            {
                Resolution = issue.Resolution?.Name,
                Status = issue.Status.Name,
                Success = true,
            };
            return result;
        }

        public async Task<string> CreateIssue(CreateJiraRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var issue = this.jira.CreateIssue(request.Project);
                issue.Summary = $"[Bot] {request.Description[0..(Math.Min(50, request.Description.Length) - 1)]}...";
                issue.Description = request.Description;
                issue.Type = request.IssueType;
                issue["Reporter name"] = request.Name;
                issue["Reporter email"] = request.Email;

                var result = await issue.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                if (request.Files?.Any() ?? false)
                {
                    await issue.AddAttachmentAsync(
                        request.Files.Select(file => new UploadAttachmentInfo(file.Name, file.Content)).ToArray(),
                        cancellationToken)
                        .ConfigureAwait(false);
                }

                this.logger.LogInformation("Created issue {issue} for request {request}", issue, request);

                return result.Key.Value;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Failed to create issue for requst {request}", request);
                throw;
            }
        }

        public async Task<IReadOnlyList<ProjectDto>> GetProjects(CancellationToken cancellationToken)
        {
            var projects = await this.jira.Projects.GetProjectsAsync(cancellationToken).ConfigureAwait(false);
            var result = projects.Select(a => new ProjectDto
            {
                Key = a.Key,
                Name = a.Name,
            }).ToArray();
            return result;
        }
    }
}