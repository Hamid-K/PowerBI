using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Logging;
using Microsoft.Owin.Security.Infrastructure;

namespace Microsoft.Owin.Security.Cookies
{
	// Token: 0x02000004 RID: 4
	internal class CookieAuthenticationHandler : AuthenticationHandler<CookieAuthenticationOptions>
	{
		// Token: 0x06000004 RID: 4 RVA: 0x000020B2 File Offset: 0x000002B2
		public CookieAuthenticationHandler(ILogger logger)
		{
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}
			this._logger = logger;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020D0 File Offset: 0x000002D0
		protected override async Task<AuthenticationTicket> AuthenticateCoreAsync()
		{
			AuthenticationTicket ticket = null;
			AuthenticationTicket authenticationTicket;
			try
			{
				string cookie = base.Options.CookieManager.GetRequestCookie(base.Context, base.Options.CookieName);
				if (string.IsNullOrWhiteSpace(cookie))
				{
					authenticationTicket = null;
				}
				else
				{
					ticket = base.Options.TicketDataFormat.Unprotect(cookie);
					if (ticket == null)
					{
						this._logger.WriteWarning("Unprotect ticket failed", new string[0]);
						authenticationTicket = null;
					}
					else
					{
						if (base.Options.SessionStore != null)
						{
							Claim claim = ticket.Identity.Claims.FirstOrDefault((Claim c) => c.Type.Equals("Microsoft.Owin.Security.Cookies-SessionId"));
							if (claim == null)
							{
								this._logger.WriteWarning("SessionId missing", new string[0]);
								return null;
							}
							this._sessionKey = claim.Value;
							AuthenticationTicket authenticationTicket2 = await base.Options.SessionStore.RetrieveAsync(this._sessionKey);
							ticket = authenticationTicket2;
							if (ticket == null)
							{
								this._logger.WriteWarning("Identity missing in session store", new string[0]);
								return null;
							}
						}
						DateTimeOffset currentUtc = base.Options.SystemClock.UtcNow;
						DateTimeOffset? issuedUtc = ticket.Properties.IssuedUtc;
						DateTimeOffset? expiresUtc = ticket.Properties.ExpiresUtc;
						if (expiresUtc != null && expiresUtc.Value < currentUtc)
						{
							if (base.Options.SessionStore != null)
							{
								await base.Options.SessionStore.RemoveAsync(this._sessionKey);
							}
							authenticationTicket = null;
						}
						else
						{
							bool? allowRefresh = ticket.Properties.AllowRefresh;
							if (issuedUtc != null && expiresUtc != null && base.Options.SlidingExpiration && (allowRefresh == null || allowRefresh.Value))
							{
								TimeSpan timeElapsed = currentUtc.Subtract(issuedUtc.Value);
								if (expiresUtc.Value.Subtract(currentUtc) < timeElapsed)
								{
									this._shouldRenew = true;
									this._renewIssuedUtc = currentUtc;
									this._renewExpiresUtc = currentUtc.Add(expiresUtc.Value.Subtract(issuedUtc.Value));
								}
							}
							CookieValidateIdentityContext context = new CookieValidateIdentityContext(base.Context, ticket, base.Options);
							await base.Options.Provider.ValidateIdentity(context);
							if (context.Identity == null)
							{
								this._shouldRenew = false;
								authenticationTicket = null;
							}
							else
							{
								authenticationTicket = new AuthenticationTicket(context.Identity, context.Properties);
							}
						}
					}
				}
			}
			catch (Exception exception)
			{
				CookieExceptionContext exceptionContext = new CookieExceptionContext(base.Context, base.Options, CookieExceptionContext.ExceptionLocation.AuthenticateAsync, exception, ticket);
				base.Options.Provider.Exception(exceptionContext);
				if (exceptionContext.Rethrow)
				{
					throw;
				}
				authenticationTicket = exceptionContext.Ticket;
			}
			return authenticationTicket;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002114 File Offset: 0x00000314
		protected override async Task ApplyResponseGrantAsync()
		{
			AuthenticationResponseGrant signin = base.Helper.LookupSignIn(base.Options.AuthenticationType);
			bool shouldSignin = signin != null;
			AuthenticationResponseRevoke signout = base.Helper.LookupSignOut(base.Options.AuthenticationType, base.Options.AuthenticationMode);
			bool shouldSignout = signout != null;
			if (shouldSignin || shouldSignout || this._shouldRenew)
			{
				AuthenticationTicket authenticationTicket = await base.AuthenticateAsync();
				AuthenticationTicket model = authenticationTicket;
				try
				{
					CookieOptions cookieOptions = new CookieOptions
					{
						Domain = base.Options.CookieDomain,
						HttpOnly = base.Options.CookieHttpOnly,
						SameSite = base.Options.CookieSameSite,
						Path = (base.Options.CookiePath ?? "/")
					};
					if (base.Options.CookieSecure == CookieSecureOption.SameAsRequest)
					{
						cookieOptions.Secure = base.Request.IsSecure;
					}
					else
					{
						cookieOptions.Secure = base.Options.CookieSecure == CookieSecureOption.Always;
					}
					if (shouldSignin)
					{
						CookieResponseSignInContext signInContext = new CookieResponseSignInContext(base.Context, base.Options, base.Options.AuthenticationType, signin.Identity, signin.Properties, cookieOptions);
						DateTimeOffset issuedUtc;
						if (signInContext.Properties.IssuedUtc != null)
						{
							issuedUtc = signInContext.Properties.IssuedUtc.Value;
						}
						else
						{
							issuedUtc = base.Options.SystemClock.UtcNow;
							signInContext.Properties.IssuedUtc = new DateTimeOffset?(issuedUtc);
						}
						if (signInContext.Properties.ExpiresUtc == null)
						{
							signInContext.Properties.ExpiresUtc = new DateTimeOffset?(issuedUtc.Add(base.Options.ExpireTimeSpan));
						}
						base.Options.Provider.ResponseSignIn(signInContext);
						if (signInContext.Properties.IsPersistent)
						{
							DateTimeOffset expiresUtc = signInContext.Properties.ExpiresUtc ?? issuedUtc.Add(base.Options.ExpireTimeSpan);
							signInContext.CookieOptions.Expires = new DateTime?(expiresUtc.UtcDateTime);
						}
						model = new AuthenticationTicket(signInContext.Identity, signInContext.Properties);
						if (base.Options.SessionStore != null)
						{
							if (this._sessionKey != null)
							{
								await base.Options.SessionStore.RemoveAsync(this._sessionKey);
							}
							this._sessionKey = await base.Options.SessionStore.StoreAsync(model);
							model = new AuthenticationTicket(new ClaimsIdentity(new Claim[]
							{
								new Claim("Microsoft.Owin.Security.Cookies-SessionId", this._sessionKey)
							}, base.Options.AuthenticationType), null);
						}
						string cookieValue = base.Options.TicketDataFormat.Protect(model);
						base.Options.CookieManager.AppendResponseCookie(base.Context, base.Options.CookieName, cookieValue, signInContext.CookieOptions);
						CookieResponseSignedInContext signedInContext = new CookieResponseSignedInContext(base.Context, base.Options, base.Options.AuthenticationType, signInContext.Identity, signInContext.Properties);
						base.Options.Provider.ResponseSignedIn(signedInContext);
						signInContext = null;
					}
					else if (shouldSignout)
					{
						if (base.Options.SessionStore != null && this._sessionKey != null)
						{
							await base.Options.SessionStore.RemoveAsync(this._sessionKey);
						}
						CookieResponseSignOutContext context = new CookieResponseSignOutContext(base.Context, base.Options, cookieOptions);
						base.Options.Provider.ResponseSignOut(context);
						base.Options.CookieManager.DeleteCookie(base.Context, base.Options.CookieName, context.CookieOptions);
					}
					else if (this._shouldRenew)
					{
						AuthenticationProperties properties = model.Properties;
						properties.IssuedUtc = new DateTimeOffset?(this._renewIssuedUtc);
						properties.ExpiresUtc = new DateTimeOffset?(this._renewExpiresUtc);
						if (base.Options.SessionStore != null && this._sessionKey != null)
						{
							await base.Options.SessionStore.RenewAsync(this._sessionKey, model);
							model = new AuthenticationTicket(new ClaimsIdentity(new Claim[]
							{
								new Claim("Microsoft.Owin.Security.Cookies-SessionId", this._sessionKey)
							}, base.Options.AuthenticationType), null);
						}
						string cookieValue2 = base.Options.TicketDataFormat.Protect(model);
						if (properties.IsPersistent)
						{
							cookieOptions.Expires = new DateTime?(this._renewExpiresUtc.UtcDateTime);
						}
						base.Options.CookieManager.AppendResponseCookie(base.Context, base.Options.CookieName, cookieValue2, cookieOptions);
						properties = null;
					}
					base.Response.Headers.Set("Cache-Control", "no-cache");
					base.Response.Headers.Set("Pragma", "no-cache");
					base.Response.Headers.Set("Expires", "-1");
					if (((shouldSignin && base.Options.LoginPath.HasValue && base.Request.Path == base.Options.LoginPath) | (shouldSignout && base.Options.LogoutPath.HasValue && base.Request.Path == base.Options.LogoutPath)) && base.Response.StatusCode == 200)
					{
						string redirectUri = base.Request.Query.Get(base.Options.ReturnUrlParameter);
						if (!string.IsNullOrWhiteSpace(redirectUri) && CookieAuthenticationHandler.IsHostRelative(redirectUri))
						{
							CookieApplyRedirectContext redirectContext = new CookieApplyRedirectContext(base.Context, base.Options, redirectUri);
							base.Options.Provider.ApplyRedirect(redirectContext);
						}
					}
					cookieOptions = null;
				}
				catch (Exception exception)
				{
					CookieExceptionContext exceptionContext = new CookieExceptionContext(base.Context, base.Options, CookieExceptionContext.ExceptionLocation.ApplyResponseGrant, exception, model);
					base.Options.Provider.Exception(exceptionContext);
					if (exceptionContext.Rethrow)
					{
						throw;
					}
				}
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002158 File Offset: 0x00000358
		private static bool IsHostRelative(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				return false;
			}
			if (path.Length == 1)
			{
				return path[0] == '/';
			}
			return path[0] == '/' && path[1] != '/' && path[1] != '\\';
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021AC File Offset: 0x000003AC
		protected override Task ApplyResponseChallengeAsync()
		{
			if (base.Response.StatusCode != 401 || !base.Options.LoginPath.HasValue)
			{
				return Task.FromResult<int>(0);
			}
			AuthenticationResponseChallenge challenge = base.Helper.LookupChallenge(base.Options.AuthenticationType, base.Options.AuthenticationMode);
			try
			{
				if (challenge != null)
				{
					string loginUri = challenge.Properties.RedirectUri;
					if (string.IsNullOrWhiteSpace(loginUri))
					{
						string currentUri = base.Request.PathBase + base.Request.Path + base.Request.QueryString;
						loginUri = string.Concat(new string[]
						{
							base.Request.Scheme,
							Uri.SchemeDelimiter,
							base.Request.Host.ToString(),
							base.Request.PathBase.ToString(),
							base.Options.LoginPath.ToString(),
							new QueryString(base.Options.ReturnUrlParameter, currentUri).ToString()
						});
					}
					CookieApplyRedirectContext redirectContext = new CookieApplyRedirectContext(base.Context, base.Options, loginUri);
					base.Options.Provider.ApplyRedirect(redirectContext);
				}
			}
			catch (Exception exception)
			{
				CookieExceptionContext exceptionContext = new CookieExceptionContext(base.Context, base.Options, CookieExceptionContext.ExceptionLocation.ApplyResponseChallenge, exception, null);
				base.Options.Provider.Exception(exceptionContext);
				if (exceptionContext.Rethrow)
				{
					throw;
				}
			}
			return Task.FromResult<object>(null);
		}

		// Token: 0x04000006 RID: 6
		private const string HeaderNameCacheControl = "Cache-Control";

		// Token: 0x04000007 RID: 7
		private const string HeaderNamePragma = "Pragma";

		// Token: 0x04000008 RID: 8
		private const string HeaderNameExpires = "Expires";

		// Token: 0x04000009 RID: 9
		private const string HeaderValueNoCache = "no-cache";

		// Token: 0x0400000A RID: 10
		private const string HeaderValueMinusOne = "-1";

		// Token: 0x0400000B RID: 11
		private const string SessionIdClaim = "Microsoft.Owin.Security.Cookies-SessionId";

		// Token: 0x0400000C RID: 12
		private readonly ILogger _logger;

		// Token: 0x0400000D RID: 13
		private bool _shouldRenew;

		// Token: 0x0400000E RID: 14
		private DateTimeOffset _renewIssuedUtc;

		// Token: 0x0400000F RID: 15
		private DateTimeOffset _renewExpiresUtc;

		// Token: 0x04000010 RID: 16
		private string _sessionKey;
	}
}
