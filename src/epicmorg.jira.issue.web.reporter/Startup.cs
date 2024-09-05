namespace epicmorg.jira.issue.web.reporter
{
    using epicmorg.jira.issue.web.reporter.Models.Configuration;
    using epicmorg.jira.issue.web.reporter.Services.DI;
    using epicmorg.jira.issue.web.reporter.Services.Impl;
    using epicmorg.jira.issue.web.reporter.Services.Interfaces;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllersWithViews();
            services
                .AddMvc()
                .AddMvcLocalization()
                .AddDataAnnotationsLocalization();

            services
                .AddOptions()
                .Configure<JiraConfig>(this.Configuration.GetSection("Jira"))
                .Configure<UiConfig>(this.Configuration.GetSection("UI"))
                .Configure<CaptchaConfig>(this.Configuration.GetSection("Captcha"));

            services
                  .AddAntiforgery(o =>
                {
                    o.SuppressXFrameOptionsHeader = true;
                    o.Cookie.SameSite = SameSiteMode.None;
                })
                .AddHttpClient()
                .AddHttpClient<ICaptchaValidator, GoogleRecaptchaValidator>();

            services
                .AddJira()
                //.AddScoped<IDistributedCache, MemoryDistributedCache>()
                .AddScoped<IJiraCache, Services.Impl.JiraCache>()
                .AddScoped<IJiraClient, JiraClient>()
                .AddScoped<IJiraService, JiraService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
