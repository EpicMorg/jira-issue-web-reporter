namespace epicmorg.jira.issue.web.reporter.Models.Create.Request
{
    using System.ComponentModel.DataAnnotations;

    public class CreateRequestBase
    {
        public CreateRequestBase() { }

        public CreateRequestBase(CreateRequestBase source)
        {
            this.Description = source.Description;
            this.Email = source.Email;
            this.IssueType = source.IssueType;
            this.Name = source.Name;
            this.Project = source.Project;
        }

        [Required]
        public string Project { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string IssueType { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
