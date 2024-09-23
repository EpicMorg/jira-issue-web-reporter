namespace epicmorg.jira.issue.web.reporter.Models.Configuration
{
    using System.ComponentModel.DataAnnotations;

    public class JiraConfig
    {
        [Url]
        public required string Domain { get; set; }

        [Required]
        public required string[] AllowedProjects { get; set; }

        [Required]
        public required string[] AllowedIssueTypes { get; set; }

        [Required]
        public required JiraAuthTypes AuthType { get; set; }

        public JiraAuthTypeBasic? JiraAuthTypeBasic { get; set; }
        public JiraAuthTypeOAuth? JiraAuthTypeOAuth { get; set; }
    }

    public class JiraAuthTypeBasic
    {
        [Required]
        public required string Login { get; set; }

        [Required]
        public required string Password { get; set; }

    }

    public class JiraAuthTypeOAuth
    {
        [Required]
        public required string ConsumerKey { get; set; }

        [Required]
        public required string ConsumerSecret { get; set; }

        [Required]
        public required string AccessToken { get; set; }

        [Required]
        public required string TokenSecret { get; set; }

    }

    public enum JiraAuthTypes
    {
        OAuth,
        Basic,
    }
}
