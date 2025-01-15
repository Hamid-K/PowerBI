using System;
using System.Security.Principal;
using System.Web.Security;
using Microsoft.BIServer.Configuration;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.BIServer.Owin.Common.Exceptions;
using Microsoft.BIServer.Owin.Common.Middleware;

namespace Microsoft.BIServer.Owin.Common.Services
{
	// Token: 0x02000013 RID: 19
	public sealed class AuthenticationService : IAuthenticationService
	{
		// Token: 0x06000049 RID: 73 RVA: 0x00002B13 File Offset: 0x00000D13
		public AuthenticationService(IAuthExtensionProvider authExtensionProvider, ServerConfiguration serverConfiguration)
		{
			this._authExtensionProvider = authExtensionProvider;
			this._serverConfiguration = serverConfiguration;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002B2C File Offset: 0x00000D2C
		public IWebHostUserContext GetUserInfo(RsRequestContext requestContext)
		{
			AuthenticationType authType = this._serverConfiguration.AuthenticationMode;
			IIdentity userIdentity = null;
			IntPtr userIntPtr = IntPtr.Zero;
			return this.WrapExtensionCall<RsUserContext>("GetUserInfo", delegate
			{
				this._authExtensionProvider.GetAuthenticationExtension(authType).GetUserInfo(requestContext, out userIdentity, out userIntPtr);
				if (userIdentity != null)
				{
					return new RsUserContext(userIdentity, userIntPtr, authType);
				}
				return null;
			});
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002B88 File Offset: 0x00000D88
		public bool TryLogonUser(string user, string password, string domain, out FormsAuthenticationTicket cookieValue)
		{
			cookieValue = null;
			AuthenticationType authType = this._serverConfiguration.AuthenticationMode;
			if (this.WrapExtensionCall<bool>("LogonUser", () => this._authExtensionProvider.GetAuthenticationExtension(authType).LogonUser(user, password, domain)))
			{
				int formsCookieTimeoutInMinutes = this._serverConfiguration.FormsCookieTimeoutInMinutes;
				cookieValue = new FormsAuthenticationTicket(2, user, DateTime.Now, DateTime.Now.AddMinutes((double)formsCookieTimeoutInMinutes), true, string.Empty, this._serverConfiguration.FormsCookiePath);
				return true;
			}
			return false;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002C28 File Offset: 0x00000E28
		public bool IsAuthExtensionInBackCompatMode()
		{
			AuthenticationType authenticationMode = this._serverConfiguration.AuthenticationMode;
			return this._authExtensionProvider.IsAuthExtensionInBackCompatMode(authenticationMode);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002C50 File Offset: 0x00000E50
		private T WrapExtensionCall<T>(string methodName, Func<T> func)
		{
			T t;
			try
			{
				t = func();
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Error when calling {0} in the Custom Authentication Extension", new object[] { methodName });
				throw new AuthenticationExtensionException(methodName, ex);
			}
			return t;
		}

		// Token: 0x0400003C RID: 60
		private readonly IAuthExtensionProvider _authExtensionProvider;

		// Token: 0x0400003D RID: 61
		private readonly ServerConfiguration _serverConfiguration;
	}
}
