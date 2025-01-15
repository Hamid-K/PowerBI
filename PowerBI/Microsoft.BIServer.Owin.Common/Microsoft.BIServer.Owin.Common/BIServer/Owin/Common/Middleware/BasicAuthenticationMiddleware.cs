using System;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.BIServer.Configuration;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.BIServer.Owin.Common.Services;
using Microsoft.Owin;
using Owin;

namespace Microsoft.BIServer.Owin.Common.Middleware
{
	// Token: 0x0200001A RID: 26
	public sealed class BasicAuthenticationMiddleware : OwinMiddleware
	{
		// Token: 0x06000077 RID: 119 RVA: 0x00003110 File Offset: 0x00001310
		public BasicAuthenticationMiddleware(OwinMiddleware next, BasicAuthenticationMiddleware.IUserUtils userUtils, ServerConfiguration serverConfiguration)
			: base(next)
		{
			if (userUtils == null)
			{
				throw new ArgumentNullException("userUtils");
			}
			this._userUtils = userUtils;
			this._serverConfiguration = serverConfiguration;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003138 File Offset: 0x00001338
		public override Task Invoke(IOwinContext context)
		{
			IPrincipal user = context.Request.User;
			if (user == null)
			{
				return this.InvokeNext(context);
			}
			HttpListenerBasicIdentity httpListenerBasicIdentity = user.Identity as HttpListenerBasicIdentity;
			if (httpListenerBasicIdentity == null)
			{
				return this.InvokeNext(context);
			}
			using (ScopeMeter.Use(new string[]
			{
				"owin",
				base.GetType().Name
			}))
			{
				ServerConfiguration serverConfiguration = this._serverConfiguration;
				LogonUserParametersProvider logonUserParametersProvider = new LogonUserParametersProvider(httpListenerBasicIdentity.Name, serverConfiguration.BasicAuthenticationDomain);
				SafeTokenHandle safeTokenHandle;
				if (!this._userUtils.LogonUser(logonUserParametersProvider.Username, logonUserParametersProvider.Domain, httpListenerBasicIdentity.Password, (int)serverConfiguration.BasicAuthenticationLogonType, 0, out safeTokenHandle))
				{
					context.Response.StatusCode = 401;
					return Task.FromResult<int>(2);
				}
				this._userUtils.ResolveRequestUserName(context.Request, safeTokenHandle);
				OwinSynchronizationContext.SetAuthenticationHeader(context.Request.Headers["Authorization"]);
			}
			return this.InvokeNext(context);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003248 File Offset: 0x00001448
		private Task InvokeNext(IOwinContext context)
		{
			if (base.Next == null)
			{
				return Task.FromResult<int>(4);
			}
			return base.Next.Invoke(context);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003268 File Offset: 0x00001468
		public static void Register(IAppBuilder app, ServerConfiguration serverConfig)
		{
			AuthenticationSchemes authenticationSchemes = serverConfig.AuthenticationSchemes;
			if (authenticationSchemes.HasFlag(AuthenticationSchemes.Basic))
			{
				app.Use(new object[]
				{
					new BasicAuthenticationMiddleware.UserUtils(),
					serverConfig
				});
			}
		}

		// Token: 0x0400004C RID: 76
		private readonly BasicAuthenticationMiddleware.IUserUtils _userUtils;

		// Token: 0x0400004D RID: 77
		private ServerConfiguration _serverConfiguration;

		// Token: 0x02000033 RID: 51
		public interface IUserUtils
		{
			// Token: 0x060000D3 RID: 211
			bool LogonUser(string username, string domain, string password, int logonType, int logonProvider, out SafeTokenHandle userToken);

			// Token: 0x060000D4 RID: 212
			void ResolveRequestUserName(IOwinRequest request, SafeTokenHandle userToken);
		}

		// Token: 0x02000034 RID: 52
		private class UserUtils : BasicAuthenticationMiddleware.IUserUtils
		{
			// Token: 0x060000D5 RID: 213 RVA: 0x000049F4 File Offset: 0x00002BF4
			public bool LogonUser(string username, string domain, string password, int logonType, int logonProvider, out SafeTokenHandle userToken)
			{
				return BasicAuthenticationMiddleware.advapi32.LogonUser(username, domain, password, logonType, logonProvider, out userToken);
			}

			// Token: 0x060000D6 RID: 214 RVA: 0x00004A04 File Offset: 0x00002C04
			public void ResolveRequestUserName(IOwinRequest request, SafeTokenHandle userToken)
			{
				HttpListenerBasicIdentity httpListenerBasicIdentity = request.User.Identity as HttpListenerBasicIdentity;
				request.User = BasicAuthenticationMiddleware.UserUtils.CreateResolvedWindowsPrincipal(userToken, httpListenerBasicIdentity.Password);
			}

			// Token: 0x060000D7 RID: 215 RVA: 0x00004A34 File Offset: 0x00002C34
			private static IPrincipal CreateResolvedWindowsPrincipal(SafeTokenHandle userToken, string password)
			{
				return new GenericPrincipal(new HttpListenerBasicIdentity(new WindowsIdentity(userToken.DangerousGetHandle()).Name, password), new string[0]);
			}
		}

		// Token: 0x02000035 RID: 53
		private static class advapi32
		{
			// Token: 0x060000D9 RID: 217
			[DllImport("advapi32.dll", SetLastError = true)]
			public static extern bool LogonUser(string username, string domain, string password, int logonType, int logonProvider, out SafeTokenHandle userToken);
		}
	}
}
