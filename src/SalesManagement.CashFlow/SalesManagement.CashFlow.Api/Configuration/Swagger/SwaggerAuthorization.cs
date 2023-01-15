using Microsoft.Extensions.Options;
using System.Net;
using System.Text;

namespace SalesManagement.CashFlow.Api.Configuration.Swagger
{
    public class SwaggerAuthorization
    {
        private readonly RequestDelegate next;
        private readonly SwaggerAuthorizedConfig swaggerAuthorizedConfig;

        public SwaggerAuthorization(RequestDelegate next, IOptions<SwaggerAuthorizedConfig> swaggerAuthorizedConfig)
        {
            this.next = next;
            this.swaggerAuthorizedConfig = swaggerAuthorizedConfig.Value ?? throw new ArgumentNullException(nameof(swaggerAuthorizedConfig)); ;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/swagger"))
            {
                string authHeader = context.Request.Headers["Authorization"];
                if (authHeader != null && authHeader.StartsWith("Basic "))
                {
                    var encodedUsernamePassword = authHeader.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries)[1]?.Trim();
                    var decodedUsernamePassword = Encoding.UTF8.GetString(Convert.FromBase64String(encodedUsernamePassword));

                    if (IsAuthorized(decodedUsernamePassword.Split(':', 2)[0], decodedUsernamePassword.Split(':', 2)[1]))
                    {
                        await next.Invoke(context);
                        return;
                    }
                }
                context.Response.Headers.Add("WWW-Authenticate", "Basic");
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
            else
                await next.Invoke(context);
        }

        public bool IsAuthorized(string username, string password)
            => username.Equals(swaggerAuthorizedConfig.Identity, StringComparison.InvariantCultureIgnoreCase) && password.Equals(swaggerAuthorizedConfig.Secret);
    }
}
