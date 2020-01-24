using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Resources.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Permissions;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Resources.API.AuthenticationHandlers
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IPersonRepository personRepository;

        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger, 
            UrlEncoder encoder, 
            ISystemClock clock,
            IPersonRepository personRepository
            ) : base(options, logger, encoder, clock)
        {
            this.personRepository = personRepository;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Missing authorization header");

            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);

            if (authHeader.Scheme != "Basic")
                return AuthenticateResult.Fail("Invalid scheme");

            var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':', 2);

            string username = credentials[0];
            string hashPassword = credentials[1];

            if (!personRepository
                .TryAuthenticate(username, hashPassword, out Domain.Models.Person person))
            {
                return AuthenticateResult.Fail("Invalid username or password");
            }

            ClaimsIdentity identity = new ClaimsIdentity(Scheme.Name);
            identity.AddClaim(new Claim(ClaimTypes.Role, "Major"));
            identity.AddClaim(new Claim(ClaimTypes.Role, "Driver"));
            identity.AddClaim(new Claim(ClaimTypes.Gender, person.Gender.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.MobilePhone, "555-444-333"));

            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}
