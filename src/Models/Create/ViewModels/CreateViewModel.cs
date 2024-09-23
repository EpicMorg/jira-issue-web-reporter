namespace epicmorg.jira.issue.web.reporter.Models.Create.ViewModels
{
    using epicmorg.jira.issue.web.reporter.Models.Create.Request;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CreateViewModel : CreateWebRequest
    {
        public CreateViewModel() { }

        public CreateViewModel(CreateWebRequest source)
            : base(source) { }

        public SelectList Projects { get; set; }

        public SelectList IssueTypes { get; set; }
    }
}
