namespace epicmorg.jira.issue.web.reporter.Services.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using epicmorg.jira.issue.web.reporter.Models.Configuration;
    using epicmorg.jira.issue.web.reporter.Services.Interfaces;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json.Linq;

    public class GoogleRecaptchaValidator : ICaptchaValidator
    {
        private const string ApiRoot = "https://www.google.com/recaptcha/api/siteverify";

        private readonly HttpClient httpClient;
        private readonly string secret;
        private readonly ILogger<GoogleRecaptchaValidator> logger;

        public GoogleRecaptchaValidator(
            HttpClient httpClient,
            IOptions<CaptchaConfig> captchaOptions,
            ILogger<GoogleRecaptchaValidator> logger)
        {
            this.httpClient = httpClient;
            this.secret = captchaOptions.Value.Secret;
            this.logger = logger;
        }

        public async Task<bool> Validate(string response, CancellationToken cancellationToken)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("secret", this.secret),
                new KeyValuePair<string, string>("response", response),
            });
            try
            {
                var res = await this.httpClient.PostAsync(ApiRoot, content, cancellationToken).ConfigureAwait(false);
                if (res.StatusCode != HttpStatusCode.OK)
                {
                    return false;
                }

                string jsonResult = await res.Content.ReadAsStringAsync().ConfigureAwait(false);
                dynamic jsonData = JObject.Parse(jsonResult);
                var success = jsonData.success == "true";

                if (!success)
                {
                    this.logger.LogInformation("Failed to pass captcha");
                }

                return success;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Failed validate captcha for respose {response}", response);
                throw;
            }
        }
    }
}
