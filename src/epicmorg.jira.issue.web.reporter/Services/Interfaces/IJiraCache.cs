namespace epicmorg.jira.issue.web.reporter.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using epicmorg.jira.issue.web.reporter.Models;
    using epicmorg.jira.issue.web.reporter.Models.Check.Response;

    public interface IJiraCache
    {
        Task<IReadOnlyList<ProjectDto>> GetProjects();

        Task SetProjects(IReadOnlyList<ProjectDto> value);

        Task<CheckResponse> GetIssueStatus(string issue);

        Task SetIssue(string issue, CheckResponse value);
    }
}