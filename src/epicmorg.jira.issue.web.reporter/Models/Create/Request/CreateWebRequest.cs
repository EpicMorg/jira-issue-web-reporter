namespace epicmorg.jira.issue.web.reporter.Models.Create.Request
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using epicmorg.jira.issue.web.reporter.Models.Captcha;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class CreateWebRequest : CreateRequestBase, ICaptchaRequest
    {
        public CreateWebRequest() { }

        public CreateWebRequest(CreateWebRequest source)
            : base(source)
        {
            this.Files = source.Files;
            this.Captcha = source.Captcha;
        }

        [Required]
        [BindProperty(Name = "g-recaptcha-response")]
        public string Captcha { get; set; }

        public IEnumerable<IFormFile> Files { get; set; }
    }
}
