using System;
using System.Diagnostics;
using System.Web;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Authentication
{
	// Token: 0x0200000B RID: 11
	internal sealed class HttpContextResponseHandler : IResponseHandler
	{
		// Token: 0x06000016 RID: 22 RVA: 0x00002154 File Offset: 0x00000354
		public bool TrySetCookie(string cookieName, string value, DateTime expiration)
		{
			if (HttpContext.Current != null)
			{
				try
				{
					HttpCookie httpCookie = new HttpCookie(cookieName, value)
					{
						Expires = expiration,
						Path = "/",
						HttpOnly = true
					};
					HttpContext.Current.Response.SetCookie(httpCookie);
				}
				catch (HttpException ex)
				{
					RSTrace.SecurityTracer.Trace(TraceLevel.Warning, "Error setting cookie: {0}", new object[] { ex.Message });
					return false;
				}
				return true;
			}
			return true;
		}
	}
}
