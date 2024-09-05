namespace epicmorg.jira.issue.web.reporter.Models.Configuration
{
    using System.ComponentModel.DataAnnotations;

    public class JiraConfig
    {
        [Url]
        public string Domain { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string[] AllowedProjects { get; set; }


        [Required]
        public string[] AllowedIssueTypes { get; set; }
    }
}
