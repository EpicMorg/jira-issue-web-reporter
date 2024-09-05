namespace epicmorg.jira.issue.web.reporter.Services.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using epicmorg.jira.issue.web.reporter.Models;
    using epicmorg.jira.issue.web.reporter.Models.Check.Response;
    using epicmorg.jira.issue.web.reporter.Services.Interfaces;
    using Microsoft.Extensions.Caching.Distributed;

    public class JiraCache : JsonCache, IJiraCache
    {
        private const string projectsKey = "JiraProjectsKey";

        public JiraCache(IDistributedCache cache)
            : base(cache) { }

        public Task<IReadOnlyList<ProjectDto>> GetProjects() => this.GetCachedValue<IReadOnlyList<ProjectDto>>(projectsKey);

        public Task SetProjects(IReadOnlyList<ProjectDto> value) => this.SetCachedValue(projectsKey, value, TimeSpan.FromMinutes(1));

        public Task<CheckResponse> GetIssueStatus(string issue) => this.GetCachedValue<CheckResponse>(this.GetIssueKey(issue));

        public Task SetIssue(string issue, CheckResponse value) => this.SetCachedValue(this.GetIssueKey(issue), value, TimeSpan.FromMinutes(1));

        private string GetIssueKey(string issue) => $"JiraIssue_{issue}";
    }
}
