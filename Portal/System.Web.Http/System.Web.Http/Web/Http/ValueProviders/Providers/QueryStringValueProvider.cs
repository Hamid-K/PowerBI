using System;
using System.Globalization;
using System.Net.Http;
using System.Web.Http.Controllers;

namespace System.Web.Http.ValueProviders.Providers
{
	// Token: 0x02000045 RID: 69
	public class QueryStringValueProvider : NameValuePairsValueProvider
	{
		// Token: 0x060001D7 RID: 471 RVA: 0x00005FDF File Offset: 0x000041DF
		public QueryStringValueProvider(HttpActionContext actionContext, CultureInfo culture)
			: base(actionContext.ControllerContext.Request.GetQueryNameValuePairs(), culture)
		{
		}
	}
}
