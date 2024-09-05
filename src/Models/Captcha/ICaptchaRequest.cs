namespace epicmorg.jira.issue.web.reporter.Models.Captcha
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc;

    public interface ICaptchaRequest
    {
        [Required]
        [BindProperty(Name = "g-recaptcha-response")]
        public string Captcha { get; set; }
    }
}
