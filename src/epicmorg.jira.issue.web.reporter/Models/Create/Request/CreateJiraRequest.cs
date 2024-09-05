namespace epicmorg.jira.issue.web.reporter.Models.Create.Request
{
    using System.Collections.Generic;

    public class CreateJiraRequest : CreateRequestBase
    {
        public CreateJiraRequest() { }

        public CreateJiraRequest(CreateRequestBase source)
            : base(source) { }

        public IEnumerable<JiraFile> Files { get; set; }
    }
}
