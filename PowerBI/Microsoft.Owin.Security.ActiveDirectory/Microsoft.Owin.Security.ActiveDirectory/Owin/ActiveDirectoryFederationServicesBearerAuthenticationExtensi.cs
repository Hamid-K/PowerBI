using System;
using System.Linq;
using Microsoft.Owin.Security.ActiveDirectory;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;

namespace Owin
{
	// Token: 0x02000008 RID: 8
	public static class ActiveDirectoryFederationServicesBearerAuthenticationExtensions
	{
		// Token: 0x0600003D RID: 61 RVA: 0x000024E8 File Offset: 0x000006E8
		public static IAppBuilder UseActiveDirectoryFederationServicesBearerAuthentication(this IAppBuilder app, ActiveDirectoryFederationServicesBearerAuthenticationOptions options)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			WsFedCachingSecurityKeyProvider cachingSecurityTokenProvider = new WsFedCachingSecurityKeyProvider(options.MetadataEndpoint, options.BackchannelCertificateValidator, options.BackchannelTimeout, options.BackchannelHttpHandler);
			JwtFormat jwtFormat;
			if (options.TokenValidationParameters != null)
			{
				if (!string.IsNullOrWhiteSpace(options.Audience))
				{
					if (string.IsNullOrWhiteSpace(options.TokenValidationParameters.ValidAudience))
					{
						options.TokenValidationParameters.ValidAudience = options.Audience;
					}
					else if (options.TokenValidationParameters.ValidAudiences == null)
					{
						options.TokenValidationParameters.ValidAudiences = new string[] { options.Audience };
					}
					else
					{
						options.TokenValidationParameters.ValidAudiences = options.TokenValidationParameters.ValidAudiences.Concat(new string[] { options.Audience });
					}
				}
				jwtFormat = new JwtFormat(options.TokenValidationParameters, cachingSecurityTokenProvider);
			}
			else
			{
				jwtFormat = new JwtFormat(options.Audience, cachingSecurityTokenProvider);
			}
			if (options.TokenHandler != null)
			{
				jwtFormat.TokenHandler = options.TokenHandler;
			}
			OAuthBearerAuthenticationOptions bearerOptions = new OAuthBearerAuthenticationOptions
			{
				Realm = options.Realm,
				Provider = options.Provider,
				AccessTokenFormat = jwtFormat,
				AuthenticationMode = options.AuthenticationMode,
				AuthenticationType = options.AuthenticationType,
				Description = options.Description
			};
			OAuthBearerAuthenticationExtensions.UseOAuthBearerAuthentication(app, bearerOptions);
			return app;
		}
	}
}
