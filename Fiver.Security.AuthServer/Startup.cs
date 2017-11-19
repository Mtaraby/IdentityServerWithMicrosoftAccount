using IdentityServer4;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Fiver.Security.AuthServer
{
    public class Startup
    {
        public void ConfigureServices(
            IServiceCollection services)
        {
            services.AddMvc();
            services.AddIdentityServer()
                        .AddDeveloperSigningCredential(filename: "tempkey.rsa")
                        .AddInMemoryApiResources(Config.GetApiResources())
                        .AddInMemoryIdentityResources(Config.GetIdentityResources())
                        .AddInMemoryClients(Config.GetClients())
                        .AddTestUsers(Config.GetUsers())
                        .AddProfileService<IdentityWithAdditionalClaimsProfileService>();

        services.AddAuthentication()
        .AddMicrosoftAccount("MicrosoftAccount", options =>
        {
            options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

            options.ClientId = "ClientId";
            options.ClientSecret = "ClientSecret";
            options.Scope.Add("openid");
            options.Scope.Add("email");

        });
        }

        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment envloggerFactory)
        {
            app.UseDeveloperExceptionPage();
            app.UseIdentityServer();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
