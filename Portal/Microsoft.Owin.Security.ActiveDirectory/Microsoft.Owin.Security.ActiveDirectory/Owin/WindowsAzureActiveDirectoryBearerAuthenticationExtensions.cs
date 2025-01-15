using System;
using System.Globalization;
using System.Linq;
using Microsoft.Owin.Security.ActiveDirectory;
using Microsoft.Owin.Security.ActiveDirectory.Properties;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;

namespace Owin
{
	// Token: 0x02000009 RID: 9
	public static class WindowsAzureActiveDirectoryBearerAuthenticationExtensions
	{
		// Token: 0x0600003E RID: 62 RVA: 0x00002638 File Offset: 0x00000838
		public static IAppBuilder UseWindowsAzureActiveDirectoryBearerAuthentication(this IAppBuilder app, WindowsAzureActiveDirectoryBearerAuthenticationOptions options)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			if (string.IsNullOrWhiteSpace(options.MetadataAddress))
			{
				if (string.IsNullOrWhiteSpace(options.Tenant))
				{
					throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Resources.Exception_OptionMustBeProvided, new object[] { "Tenant" }));
				}
				options.MetadataAddress = string.Format(CultureInfo.InvariantCulture, "https://login.windows.net/{0}/federationmetadata/2007-06/federationmetadata.xml", new object[] { options.Tenant });
			}
			WsFedCachingSecurityKeyProvider cachingSecurityTokenProvider = new WsFedCachingSecurityKeyProvider(options.MetadataAddress, options.BackchannelCertificateValidator, options.BackchannelTimeout, options.BackchannelHttpHandler);
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

		// Token: 0x04000020 RID: 32
		private const string SecurityTokenServiceAddressFormat = "https://login.windows.net/{0}/federationmetadata/2007-06/federationmetadata.xml";
	}
}
