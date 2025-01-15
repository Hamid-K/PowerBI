using System;
using System.Collections.Generic;
using System.Web.Http.Controllers;

namespace System.Web.Http.Routing
{
	// Token: 0x02000151 RID: 337
	internal static class HttpRouteExtensions
	{
		// Token: 0x0600092A RID: 2346 RVA: 0x000171EC File Offset: 0x000153EC
		public static CandidateAction[] GetDirectRouteCandidates(this IHttpRoute route)
		{
			IDictionary<string, object> dataTokens = route.DataTokens;
			if (dataTokens == null)
			{
				return null;
			}
			List<CandidateAction> list = new List<CandidateAction>();
			HttpActionDescriptor[] array = null;
			HttpActionDescriptor[] array2;
			if (dataTokens.TryGetValue("actions", out array2) && array2 != null && array2.Length != 0)
			{
				array = array2;
			}
			if (array == null)
			{
				return null;
			}
			int num = 0;
			int num2;
			if (dataTokens.TryGetValue("order", out num2))
			{
				num = num2;
			}
			decimal num3 = 0m;
			decimal num4;
			if (dataTokens.TryGetValue("precedence", out num4))
			{
				num3 = num4;
			}
			foreach (HttpActionDescriptor httpActionDescriptor in array)
			{
				list.Add(new CandidateAction
				{
					ActionDescriptor = httpActionDescriptor,
					Order = num,
					Precedence = num3
				});
			}
			return list.ToArray();
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x000172A4 File Offset: 0x000154A4
		public static HttpActionDescriptor[] GetTargetActionDescriptors(this IHttpRoute route)
		{
			IDictionary<string, object> dataTokens = route.DataTokens;
			if (dataTokens == null)
			{
				return null;
			}
			HttpActionDescriptor[] array;
			if (!dataTokens.TryGetValue("actions", out array))
			{
				return null;
			}
			return array;
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x000172D0 File Offset: 0x000154D0
		public static HttpControllerDescriptor GetTargetControllerDescriptor(this IHttpRoute route)
		{
			IDictionary<string, object> dataTokens = route.DataTokens;
			if (dataTokens == null)
			{
				return null;
			}
			HttpControllerDescriptor httpControllerDescriptor;
			if (!dataTokens.TryGetValue("controller", out httpControllerDescriptor))
			{
				return null;
			}
			return httpControllerDescriptor;
		}
	}
}
