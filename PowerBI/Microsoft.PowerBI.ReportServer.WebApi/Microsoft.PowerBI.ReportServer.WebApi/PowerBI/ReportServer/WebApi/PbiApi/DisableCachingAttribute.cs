using System;
using System.Net.Http.Headers;
using System.Web.Http.Filters;

namespace Microsoft.PowerBI.ReportServer.WebApi.PbiApi
{
	// Token: 0x02000021 RID: 33
	internal sealed class DisableCachingAttribute : ActionFilterAttribute
	{
		// Token: 0x0600008C RID: 140 RVA: 0x0000347E File Offset: 0x0000167E
		public override void OnActionExecuted(HttpActionExecutedContext context)
		{
			if (context.Response != null)
			{
				context.Response.Headers.CacheControl = new CacheControlHeaderValue
				{
					NoCache = true,
					MustRevalidate = true,
					NoStore = true
				};
			}
			base.OnActionExecuted(context);
		}
	}
}
