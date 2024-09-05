namespace epicmorg.jira.issue.web.reporter.Models.Check.Response
{
    public class CheckResponse
    {
        public string Issue { get; set; }

        public bool Success { get; set; }

        public string Resolution { get; set; }

        public string Status { get; set; }
        
        public string Message { get; set; }
    }
}
