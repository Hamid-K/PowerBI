using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace System.Net.Http
{
	// Token: 0x02000003 RID: 3
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class OwinHttpRequestMessageExtensions
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000021CC File Offset: 0x000003CC
		public static IOwinContext GetOwinContext(this HttpRequestMessage request)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			IOwinContext owinContext;
			IDictionary<string, object> dictionary;
			if (!request.Properties.TryGetValue("MS_OwinContext", out owinContext) && request.Properties.TryGetValue("MS_OwinEnvironment", out dictionary))
			{
				owinContext = new OwinContext(dictionary);
				request.SetOwinContext(owinContext);
				request.Properties.Remove("MS_OwinEnvironment");
			}
			return owinContext;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000222F File Offset: 0x0000042F
		public static void SetOwinContext(this HttpRequestMessage request, IOwinContext context)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			request.Properties["MS_OwinContext"] = context;
			request.Properties.Remove("MS_OwinEnvironment");
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002270 File Offset: 0x00000470
		public static IDictionary<string, object> GetOwinEnvironment(this HttpRequestMessage request)
		{
			IOwinContext owinContext = request.GetOwinContext();
			if (owinContext == null)
			{
				return null;
			}
			return owinContext.Environment;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000228F File Offset: 0x0000048F
		public static void SetOwinEnvironment(this HttpRequestMessage request, IDictionary<string, object> environment)
		{
			request.SetOwinContext(new OwinContext(environment));
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000022A0 File Offset: 0x000004A0
		internal static IAuthenticationManager GetAuthenticationManager(this HttpRequestMessage request)
		{
			IOwinContext owinContext = request.GetOwinContext();
			if (owinContext == null)
			{
				return null;
			}
			return owinContext.Authentication;
		}

		// Token: 0x04000002 RID: 2
		private const string OwinEnvironmentKey = "MS_OwinEnvironment";

		// Token: 0x04000003 RID: 3
		private const string OwinContextKey = "MS_OwinContext";
	}
}
