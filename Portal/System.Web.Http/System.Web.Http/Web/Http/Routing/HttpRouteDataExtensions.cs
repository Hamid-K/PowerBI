using System;
using System.Collections.Generic;

namespace System.Web.Http.Routing
{
	// Token: 0x02000150 RID: 336
	public static class HttpRouteDataExtensions
	{
		// Token: 0x06000926 RID: 2342 RVA: 0x00017058 File Offset: 0x00015258
		public static void RemoveOptionalRoutingParameters(this IHttpRouteData routeData)
		{
			HttpRouteDataExtensions.RemoveOptionalRoutingParameters(routeData.Values);
			IEnumerable<IHttpRouteData> subRoutes = routeData.GetSubRoutes();
			if (subRoutes != null)
			{
				foreach (IHttpRouteData httpRouteData in subRoutes)
				{
					httpRouteData.RemoveOptionalRoutingParameters();
				}
			}
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x000170B4 File Offset: 0x000152B4
		private static void RemoveOptionalRoutingParameters(IDictionary<string, object> routeValueDictionary)
		{
			int count = routeValueDictionary.Count;
			int num = 0;
			string[] array = new string[count];
			foreach (KeyValuePair<string, object> keyValuePair in routeValueDictionary)
			{
				if (keyValuePair.Value == RouteParameter.Optional)
				{
					array[num] = keyValuePair.Key;
					num++;
				}
			}
			for (int i = 0; i < num; i++)
			{
				string text = array[i];
				routeValueDictionary.Remove(text);
			}
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x00017140 File Offset: 0x00015340
		public static IEnumerable<IHttpRouteData> GetSubRoutes(this IHttpRouteData routeData)
		{
			IHttpRouteData[] array = null;
			if (routeData.Values.TryGetValue("MS_SubRoutes", out array))
			{
				return array;
			}
			return null;
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x00017168 File Offset: 0x00015368
		internal static CandidateAction[] GetDirectRouteCandidates(this IHttpRouteData routeData)
		{
			IEnumerable<IHttpRouteData> subRoutes = routeData.GetSubRoutes();
			if (subRoutes != null)
			{
				List<CandidateAction> list = new List<CandidateAction>();
				foreach (IHttpRouteData httpRouteData in subRoutes)
				{
					CandidateAction[] directRouteCandidates = httpRouteData.Route.GetDirectRouteCandidates();
					if (directRouteCandidates != null)
					{
						list.AddRange(directRouteCandidates);
					}
				}
				return list.ToArray();
			}
			if (routeData.Route == null)
			{
				return null;
			}
			return routeData.Route.GetDirectRouteCandidates();
		}
	}
}
