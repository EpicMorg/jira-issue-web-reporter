namespace epicmorg.jira.issue.web.reporter.Services.Interfaces
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface ICaptchaValidator
    {
        Task<bool> Validate(string response, CancellationToken cancellationToken);
    }
}