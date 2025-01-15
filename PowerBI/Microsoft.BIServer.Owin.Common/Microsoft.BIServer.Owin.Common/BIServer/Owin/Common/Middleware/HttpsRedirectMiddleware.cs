using System;
using System.Threading.Tasks;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.BIServer.HostingEnvironment.HostingInfo;
using Microsoft.Owin;
using Owin;

namespace Microsoft.BIServer.Owin.Common.Middleware
{
	// Token: 0x02000017 RID: 23
	public sealed class HttpsRedirectMiddleware : OwinMiddleware
	{
		// Token: 0x06000065 RID: 101 RVA: 0x00002E31 File Offset: 0x00001031
		public HttpsRedirectMiddleware(OwinMiddleware next)
			: base(next)
		{
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002E3C File Offset: 0x0000103C
		public override Task Invoke(IOwinContext context)
		{
			if (!(context.Request.Uri.Scheme == Uri.UriSchemeHttp))
			{
				return base.Next.Invoke(context);
			}
			if (context.Request.Uri.Port == -1 || context.Request.Uri.Port == 80)
			{
				Logger.Info("Redirecting request to HTTPS: {0}", new object[] { context.Request.Uri.AbsolutePath });
				UriBuilder uriBuilder = new UriBuilder(context.Request.Uri)
				{
					Scheme = Uri.UriSchemeHttps,
					Port = -1
				};
				context.Response.StatusCode = 307;
				context.Response.Headers.Set("Location", uriBuilder.Uri.AbsoluteUri);
				return Task.FromResult<int>(0);
			}
			context.Response.StatusCode = 403;
			Logger.Error("Rejecting HTTP request - HTTPs required. Connections are not redirected if non-standard ports are configured.: {0}", new object[] { context.Request.Uri.AbsolutePath });
			return Task.FromResult<int>(0);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002F54 File Offset: 0x00001154
		public static void Register(IAppBuilder app)
		{
			int intOrException = StaticConfig.Current.GetIntOrException(ConfigSettings.SecureConnectionLevel.ToString());
			if (intOrException >= 1)
			{
				Logger.Info("SecureConnectionLevel: {0}. Redirecting HTTP connections to HTTPS.", new object[] { intOrException });
				app.Use(Array.Empty<object>());
			}
		}

		// Token: 0x04000042 RID: 66
		private const int RequiresSecureConnection = 1;

		// Token: 0x04000043 RID: 67
		private const int DefaultPortForScheme = -1;

		// Token: 0x04000044 RID: 68
		private const int DefaultHttpPort = 80;
	}
}
