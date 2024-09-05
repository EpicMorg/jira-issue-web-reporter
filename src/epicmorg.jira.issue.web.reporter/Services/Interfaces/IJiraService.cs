namespace epicmorg.jira.issue.web.reporter.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using epicmorg.jira.issue.web.reporter.Models;
    using epicmorg.jira.issue.web.reporter.Models.Check.Response;
    using epicmorg.jira.issue.web.reporter.Models.Create.Request;
    using epicmorg.jira.issue.web.reporter.Models.Create.Response;

    public interface IJiraService
    {
        Task<CheckResponse> GetIssueStatus(string issue, CancellationToken cancellationToken);

        Task<IReadOnlyList<ProjectDto>> GetProjects(CancellationToken cancellationToken);

        Task<CreateResponse> CreateIssue(CreateJiraRequest request, CancellationToken cancellationToken);
    }
}