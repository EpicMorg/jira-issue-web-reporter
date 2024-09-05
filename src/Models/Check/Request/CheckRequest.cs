namespace epicmorg.jira.issue.web.reporter.Models.Check.Request
{
    using System.ComponentModel.DataAnnotations;
    using epicmorg.jira.issue.web.reporter.Models.Captcha;
    using Microsoft.AspNetCore.Mvc;

    public class CheckRequest : ICaptchaRequest
    {
        [Required]
        public string Issue { get; set; }

        [Required]
        [BindProperty(Name = "g-recaptcha-response")]
        public string Captcha { get; set; }
    }
}
