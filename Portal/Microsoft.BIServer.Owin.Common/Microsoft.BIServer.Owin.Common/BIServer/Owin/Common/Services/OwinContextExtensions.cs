using System;
using Microsoft.Owin;

namespace Microsoft.BIServer.Owin.Common.Services
{
	// Token: 0x0200000C RID: 12
	public static class OwinContextExtensions
	{
		// Token: 0x06000029 RID: 41 RVA: 0x000026C0 File Offset: 0x000008C0
		public static bool IsApiRequest(this IOwinContext context)
		{
			PathString path = context.Request.Path;
			return context.Request.Path.Value.StartsWith("/api/", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000026F8 File Offset: 0x000008F8
		public static bool IsCallToLogon(this IOwinContext context)
		{
			return context.Request.Path.Value.EndsWith("/api/v2.0/session", StringComparison.OrdinalIgnoreCase) && context.Request.Method.Equals("POST");
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000273C File Offset: 0x0000093C
		public static bool HasBasicAuthHeader(this IOwinContext context)
		{
			if (context.Request.Headers != null)
			{
				string text = context.Request.Headers["Authorization"];
				return text != null && text.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase);
			}
			return false;
		}
	}
}
