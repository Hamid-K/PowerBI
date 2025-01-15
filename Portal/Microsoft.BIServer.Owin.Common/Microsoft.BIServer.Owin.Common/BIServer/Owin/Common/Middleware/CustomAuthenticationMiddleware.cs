using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using Microsoft.BIServer.Configuration;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.BIServer.Owin.Common.Services;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Newtonsoft.Json;
using Owin;

namespace Microsoft.BIServer.Owin.Common.Middleware
{
	// Token: 0x02000021 RID: 33
	public sealed class CustomAuthenticationMiddleware : OwinMiddleware
	{
		// Token: 0x06000094 RID: 148 RVA: 0x00003896 File Offset: 0x00001A96
		public CustomAuthenticationMiddleware(OwinMiddleware next, IAuthenticationService authenticationService, ServerConfiguration serverConfiguration)
			: base(next)
		{
			this._authenticationService = authenticationService;
			this._serverConfiguration = serverConfiguration;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000038AD File Offset: 0x00001AAD
		public override Task Invoke(IOwinContext context)
		{
			if (context.IsCallToLogon())
			{
				this.LogonWithExtension(context);
				return Task.FromResult<int>(0);
			}
			return this.CreatePortalIdentity(context);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000038CC File Offset: 0x00001ACC
		internal void LogonWithExtension(IOwinContext context)
		{
			UserCredentialsContract userCredentialsContract = null;
			try
			{
				if (context.Request.Body != null)
				{
					using (StreamReader streamReader = new StreamReader(context.Request.Body))
					{
						userCredentialsContract = JsonConvert.DeserializeObject<UserCredentialsContract>(streamReader.ReadToEnd(), new JsonSerializerSettings
						{
							MaxDepth = new int?(1)
						});
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Error while parsing Logon User Request.", Array.Empty<object>());
				context.Response.StatusCode = 400;
				return;
			}
			FormsAuthenticationTicket formsAuthenticationTicket;
			if (userCredentialsContract != null && this._authenticationService.TryLogonUser(userCredentialsContract.UserName, userCredentialsContract.Password, userCredentialsContract.Domain, out formsAuthenticationTicket))
			{
				context.Response.Cookies.Append(this._serverConfiguration.FormsCookieName, FormsAuthentication.Encrypt(formsAuthenticationTicket), new CookieOptions
				{
					HttpOnly = true,
					Path = this._serverConfiguration.FormsCookiePath,
					Expires = new DateTime?(DateTime.UtcNow.AddMinutes((double)this._serverConfiguration.FormsCookieTimeoutInMinutes))
				});
				context.Response.StatusCode = 201;
				return;
			}
			CustomAuthenticationMiddleware.SetUnauthorized(context.Response, null, false);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003A0C File Offset: 0x00001C0C
		internal Task CreatePortalIdentity(IOwinContext context)
		{
			using (ScopeMeter.Use(new string[]
			{
				"owin",
				base.GetType().Name
			}))
			{
				List<KeyValuePair<string, string>> list = context.Request.Cookies.Where((KeyValuePair<string, string> c) => this._serverConfiguration.PassthroughCookies != null && this._serverConfiguration.PassthroughCookies.Contains(c.Key)).ToList<KeyValuePair<string, string>>();
				RsRequestContext rsRequestContext = (context.HasBasicAuthHeader() ? this.CreateRequestContextFromBasicHeader(context, list) : this.CreateRequestContextFromCookie(context));
				IWebHostUserContext userInfo = this._authenticationService.GetUserInfo(rsRequestContext);
				if (userInfo == null)
				{
					context.Authentication.Challenge(new string[] { "Cookies" });
					return Task.FromResult<int>(0);
				}
				if (userInfo.Identity is WindowsIdentity)
				{
					context.Request.User = new WindowsPrincipal(userInfo.Identity as WindowsIdentity);
				}
				else
				{
					PortalIdentity portalIdentity = new PortalIdentity(userInfo, "Cookies", list);
					context.Request.User = new ClaimsPrincipal(portalIdentity);
				}
			}
			return base.Next.Invoke(context);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003B20 File Offset: 0x00001D20
		internal RsRequestContext CreateRequestContextFromBasicHeader(IOwinContext context, List<KeyValuePair<string, string>> passThroughCookies)
		{
			string text = context.Request.Headers["Authorization"].Substring("Basic ".Length).Trim();
			UserCredentialsContract userCredentialsContract = this.TryDecodeBasicHeader(text);
			FormsAuthenticationTicket formsAuthenticationTicket = null;
			if (userCredentialsContract != null && this._authenticationService.TryLogonUser(userCredentialsContract.UserName, userCredentialsContract.Password, userCredentialsContract.Domain, out formsAuthenticationTicket))
			{
				passThroughCookies.Add(new KeyValuePair<string, string>(this._serverConfiguration.FormsCookieName, FormsAuthentication.Encrypt(formsAuthenticationTicket)));
				Logger.Debug("User {0}, domain {1}, succefully logged in through BASIC headers.", new object[] { userCredentialsContract.UserName, userCredentialsContract.Domain });
				return new RsRequestContext(context, new FormsIdentity(formsAuthenticationTicket));
			}
			Logger.Warning("Invalid credentials used on Basic Auth header.", Array.Empty<object>());
			return null;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003BE0 File Offset: 0x00001DE0
		private UserCredentialsContract TryDecodeBasicHeader(string encodedUsernamePassword)
		{
			try
			{
				string[] array = Encoding.UTF8.GetString(Convert.FromBase64String(encodedUsernamePassword)).Split(new char[] { ':' });
				if (array.Length != 2)
				{
					throw new ArgumentOutOfRangeException("Basic Header in unexpected format");
				}
				string text = array[0];
				string text2 = array[1];
				string[] array2 = text.Split(new char[] { '\\' });
				string text3 = text;
				string text4 = string.Empty;
				if (array2.Length > 1)
				{
					text3 = array2[1];
					text4 = array2[0];
				}
				return new UserCredentialsContract
				{
					Domain = text4,
					Password = text2,
					UserName = text3
				};
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Error decoding the basic auth header", Array.Empty<object>());
			}
			return null;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003C94 File Offset: 0x00001E94
		internal RsRequestContext CreateRequestContextFromCookie(IOwinContext context)
		{
			string formsCookieName = this._serverConfiguration.FormsCookieName;
			IIdentity identity = null;
			if (context.Request != null && context.Request.User != null)
			{
				identity = context.Request.User.Identity;
			}
			else if (CustomAuthenticationMiddleware.IsAttemptWithCookie(context, formsCookieName))
			{
				string text = context.Request.Cookies[formsCookieName];
				try
				{
					FormsAuthenticationTicket formsAuthenticationTicket = FormsAuthentication.Decrypt(text);
					if (!formsAuthenticationTicket.Expired)
					{
						identity = new FormsIdentity(formsAuthenticationTicket);
						this.RenewCookieIfNeeed(context, formsAuthenticationTicket);
					}
				}
				catch (ArgumentException)
				{
					Logger.Error("Could not decrypt forms authentication cookie, make sure you have the same machine key set in the config", Array.Empty<object>());
				}
			}
			return new RsRequestContext(context, identity);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003D3C File Offset: 0x00001F3C
		internal void RenewCookieIfNeeed(IOwinContext context, FormsAuthenticationTicket ticket)
		{
			if (this._serverConfiguration.FormsCookieSlidingExpiration)
			{
				FormsAuthenticationTicket formsAuthenticationTicket = FormsAuthentication.RenewTicketIfOld(ticket);
				if (formsAuthenticationTicket.IssueDate != ticket.IssueDate)
				{
					context.Response.Cookies.Append(this._serverConfiguration.FormsCookieName, FormsAuthentication.Encrypt(formsAuthenticationTicket), new CookieOptions
					{
						HttpOnly = true,
						Path = this._serverConfiguration.FormsCookiePath
					});
				}
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003DAE File Offset: 0x00001FAE
		internal static bool IsAttemptWithCookie(IOwinContext context, string formsAuthCookieName)
		{
			return !string.IsNullOrEmpty(formsAuthCookieName) && context.Request.Cookies != null && !string.IsNullOrEmpty(context.Request.Cookies[formsAuthCookieName]);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003DE0 File Offset: 0x00001FE0
		public static void Register(IAppBuilder app, IAuthenticationService authenticationService, ServerConfiguration serverConfiguration, bool useCookieRedirect)
		{
			if (CustomAuthenticationMiddleware.IsUsingCustomAuth(serverConfiguration.AuthenticationTypes) && useCookieRedirect)
			{
				CookieAuthenticationOptions cookieAuthenticationOptions = new CookieAuthenticationOptions
				{
					CookieSecure = CookieSecureOption.SameAsRequest,
					LoginPath = new PathString("/../reportserver/"),
					ReturnUrlParameter = "ReturnUrl",
					Provider = new CookieAuthenticationProvider
					{
						OnApplyRedirect = delegate(CookieApplyRedirectContext context)
						{
							CustomAuthenticationMiddleware.ApplyRedirectContext(context, serverConfiguration);
						}
					}
				};
				app.UseCookieAuthentication(cookieAuthenticationOptions);
			}
			if (serverConfiguration.AuthenticationExtensions != null)
			{
				Extension extension = serverConfiguration.AuthenticationExtensions.FirstOrDefault((Extension p) => p.Name.Equals(serverConfiguration.AuthenticationMode.ToString()));
				if (extension != null && !extension.Class.Equals("Microsoft.ReportingServices.Authentication.WindowsAuthentication", StringComparison.Ordinal))
				{
					if (!authenticationService.IsAuthExtensionInBackCompatMode())
					{
						app.Use(new object[] { authenticationService, serverConfiguration });
						return;
					}
					Logger.Error("The configuration for this server is using Windows authentication  with a custom extension running in backward compatibility mode, which is unsupported. please update this extension to implement IAuthenticationExtension2 interface.", Array.Empty<object>());
				}
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003ECE File Offset: 0x000020CE
		private static bool IsUsingCustomAuth(AuthenticationTypes configAuthenticationTypes)
		{
			return configAuthenticationTypes == AuthenticationTypes.Custom || configAuthenticationTypes == AuthenticationTypes.None || configAuthenticationTypes == AuthenticationTypes.RSForms;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003EE0 File Offset: 0x000020E0
		public static void ApplyRedirectContext(CookieApplyRedirectContext context, ServerConfiguration configuration)
		{
			UriBuilder uriBuilder = new UriBuilder("foo");
			uriBuilder.Path = configuration.ReportServerVirtualDirectory;
			uriBuilder.Path = uriBuilder.Path.TrimEnd(new char[] { '/' }) + "/" + configuration.LoginUrl;
			if (context.OwinContext.IsApiRequest())
			{
				uriBuilder.Query = "ReturnUrl=" + configuration.ReportServerWebAppVirtualDirectory;
				CustomAuthenticationMiddleware.SetUnauthorized(context.Response, uriBuilder.Uri.PathAndQuery, !CustomAuthenticationMiddleware.IsAttemptWithCookie(context.OwinContext, configuration.FormsCookieName));
				return;
			}
			uriBuilder.Query = "ReturnUrl=" + configuration.ReportServerVirtualDirectory + "/localredirect?url=" + HttpUtility.UrlEncode(HttpUtility.UrlEncode(context.Request.Uri.LocalPath + context.Request.QueryString));
			context.RedirectUri = uriBuilder.Uri.PathAndQuery;
			context.Response.Redirect(context.RedirectUri);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003FEA File Offset: 0x000021EA
		private static void SetUnauthorized(IOwinResponse response, string redirectLocation, bool includeBasicHeader)
		{
			if (includeBasicHeader)
			{
				response.Headers.Append("WWW-Authenticate", "Basic Realm=\"\"");
			}
			if (!string.IsNullOrEmpty(redirectLocation))
			{
				response.Headers.Append("RSLocation", redirectLocation);
			}
			response.StatusCode = 401;
		}

		// Token: 0x0400005D RID: 93
		private const string RSLocationHeaderName = "RSLocation";

		// Token: 0x0400005E RID: 94
		private const string DefaultAuthenticationExtension = "Microsoft.ReportingServices.Authentication.WindowsAuthentication";

		// Token: 0x0400005F RID: 95
		private readonly IAuthenticationService _authenticationService;

		// Token: 0x04000060 RID: 96
		private readonly ServerConfiguration _serverConfiguration;
	}
}
