namespace epicmorg.jira.issue.web.reporter.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using epicmorg.jira.issue.web.reporter.Models;
    using epicmorg.jira.issue.web.reporter.Models.Captcha;
    using epicmorg.jira.issue.web.reporter.Models.Check.Request;
    using epicmorg.jira.issue.web.reporter.Models.Configuration;
    using epicmorg.jira.issue.web.reporter.Models.Create.Request;
    using epicmorg.jira.issue.web.reporter.Models.Create.ViewModels;
    using epicmorg.jira.issue.web.reporter.Services.Interfaces;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly UiConfig uiOptions;

        private readonly ICaptchaValidator captchaValidator;
        private readonly IJiraService jiraService;
        private readonly JiraConfig jiraConfig;

        public HomeController(
            ILogger<HomeController> logger,
            IOptions<UiConfig> uiOptions,
            IOptions<JiraConfig> jiraConfig,
            ICaptchaValidator captchaValidator,
            IJiraService jiraService)
        {
            this.logger = logger;
            this.uiOptions = uiOptions.Value;
            this.jiraConfig = jiraConfig.Value;

            this.captchaValidator = captchaValidator;
            this.jiraService = jiraService;
        }

        public async Task<IActionResult> Index() => this.View();

        public IActionResult Privacy() => this.View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });

        [HttpGet]
        public async Task<IActionResult> Create(CancellationToken cancellationToken) => this.View(await this.BuildCreateModel(cancellationToken).ConfigureAwait(false));

        [HttpGet]
        [Route("Check/{id?}")]
        public async Task<IActionResult> Check(string id = null) => this.View(new CheckRequest { Issue = id});

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Check")]
        public async Task<IActionResult> Check(CheckRequest model, CancellationToken cancellationToken)
        {
            var valid = await this.IsModelAndCaptchaValid(model, cancellationToken).ConfigureAwait(false);
            if (!valid)
            {
                return this.View(model);
            }

            var result = await this.jiraService.GetIssueStatus(model.Issue, cancellationToken).ConfigureAwait(false);
            return this.View("CheckResult", result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateWebRequest model, CancellationToken cancellationToken)
        {
            var valid = await this.IsModelAndCaptchaValid(model, cancellationToken).ConfigureAwait(false);

            if (!valid)
            {
                var viewModel = await this.BuildCreateModel(cancellationToken, new CreateViewModel(model)).ConfigureAwait(false);
                return this.View(nameof(this.Create), viewModel);
            }

            var request = await this.BuildJiraRequest(model).ConfigureAwait(false);
            var result = await this.jiraService.CreateIssue(request, cancellationToken).ConfigureAwait(false);

            return this.View("Created", result);
        }

        private async Task<bool> IsModelAndCaptchaValid(ICaptchaRequest model, CancellationToken cancellationToken)
        {
            var valid = this.ModelState.IsValid;
            if (valid)
            {
                if (!await this.captchaValidator.Validate(model.Captcha, cancellationToken).ConfigureAwait(false))
                {
                    this.logger.LogInformation($"User has failed captcha");
                    this.ModelState.AddModelError(null, "You failed the CAPTCHA");
                    valid = false;
                }
            }

            return valid;
        }

        private async Task<CreateViewModel> BuildCreateModel(CancellationToken cancellationToken, CreateViewModel model = null)
        {
            model ??= new CreateViewModel();
            model.IssueTypes = new SelectList(
                                items: this.jiraConfig.AllowedIssueTypes,
                                dataValueField: null,
                                dataTextField: null,
                                selectedValue: null);
            model.Projects = new SelectList(
                                items: await this.jiraService.GetProjects(cancellationToken).ConfigureAwait(false),
                                dataValueField: nameof(ProjectDto.Key),
                                dataTextField: nameof(ProjectDto.Name),
                                selectedValue: null);
            return model;
        }

        private async Task<CreateJiraRequest> BuildJiraRequest(CreateWebRequest model)
        {
            var request = new CreateJiraRequest(model);
            var lst = new List<JiraFile>();
            foreach (var file in model.Files ?? Enumerable.Empty<IFormFile>())
            {
                using var stream = file.OpenReadStream();
                using var ms = new MemoryStream();
                await stream.CopyToAsync(ms).ConfigureAwait(false);
                lst.Add(new JiraFile
                {
                    Name = file.FileName,
                    Content = ms.ToArray(),
                });
            }

            request.Files = lst;

            return request;
        }
    }
}
