namespace epicmorg.jira.issue.web.reporter.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using epicmorg.jira.issue.web.reporter.Models;
    using epicmorg.jira.issue.web.reporter.Models.Check.Response;
    using epicmorg.jira.issue.web.reporter.Models.Create.Request;

    public interface IJiraClient
    {
        Task<CheckResponse> GetIssueStatus(string issueId, CancellationToken cancellationToken);

        Task<string> CreateIssue(CreateJiraRequest request, CancellationToken cancellationToken);

        Task<IReadOnlyList<ProjectDto>> GetProjects(CancellationToken cancellationToken);
    }
}