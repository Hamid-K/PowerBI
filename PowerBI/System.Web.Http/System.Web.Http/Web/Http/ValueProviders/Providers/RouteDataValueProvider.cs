using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;

namespace System.Web.Http.ValueProviders.Providers
{
	// Token: 0x02000043 RID: 67
	public class RouteDataValueProvider : NameValuePairsValueProvider
	{
		// Token: 0x060001D3 RID: 467 RVA: 0x00005F5C File Offset: 0x0000415C
		public RouteDataValueProvider(HttpActionContext actionContext, CultureInfo culture)
			: base(RouteDataValueProvider.GetRouteValues(actionContext.ControllerContext.RouteData), culture)
		{
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00005F75 File Offset: 0x00004175
		internal static IEnumerable<KeyValuePair<string, string>> GetRouteValues(IHttpRouteData routeData)
		{
			foreach (KeyValuePair<string, object> keyValuePair in routeData.Values)
			{
				string text = ((keyValuePair.Value == null) ? null : keyValuePair.Value.ToString());
				yield return new KeyValuePair<string, string>(keyValuePair.Key, text);
			}
			IEnumerator<KeyValuePair<string, object>> enumerator = null;
			yield break;
			yield break;
		}
	}
}
