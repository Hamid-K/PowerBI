using System;
using System.Net;
using System.Net.Http;
using Microsoft.BIServer.Configuration;
using Owin;

namespace Microsoft.BIServer.Owin.Common.Middleware
{
	// Token: 0x02000023 RID: 35
	public static class AuthSchemeConfig
	{
		// Token: 0x060000A8 RID: 168 RVA: 0x000040CC File Offset: 0x000022CC
		public static void Configure(IAppBuilder app, ServerConfiguration serverConfiguration)
		{
			HttpListener httpListener = (HttpListener)app.Properties["System.Net.HttpListener"];
			httpListener.AuthenticationSchemeSelectorDelegate = delegate(HttpListenerRequest request)
			{
				if (string.Compare(request.HttpMethod, HttpMethod.Options.ToString(), true) == 0)
				{
					return AuthenticationSchemes.Anonymous;
				}
				return serverConfiguration.AuthenticationSchemes;
			};
			httpListener.UnsafeConnectionNtlmAuthentication = serverConfiguration.AuthPersistence;
			httpListener.Realm = serverConfiguration.BasicAuthenticationRealm;
		}
	}
}
