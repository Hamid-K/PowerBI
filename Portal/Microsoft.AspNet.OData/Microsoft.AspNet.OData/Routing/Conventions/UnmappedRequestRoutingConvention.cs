using System;
using System.Linq;
using System.Web.Http.Controllers;
using Microsoft.AspNet.OData.Adapters;
using Microsoft.AspNet.OData.Interfaces;

namespace Microsoft.AspNet.OData.Routing.Conventions
{
	// Token: 0x0200009D RID: 157
	public class UnmappedRequestRoutingConvention : NavigationSourceRoutingConvention
	{
		// Token: 0x06000552 RID: 1362 RVA: 0x00012294 File Offset: 0x00010494
		public override string SelectAction(ODataPath odataPath, HttpControllerContext controllerContext, ILookup<string, HttpActionDescriptor> actionMap)
		{
			NavigationSourceRoutingConvention.ValidateSelectActionParameters(odataPath, controllerContext, actionMap);
			return UnmappedRequestRoutingConvention.SelectActionImpl(new WebApiActionMap(actionMap));
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x000122A9 File Offset: 0x000104A9
		internal static string SelectActionImpl(IWebApiActionMap actionMap)
		{
			if (actionMap.Contains("HandleUnmappedRequest"))
			{
				return "HandleUnmappedRequest";
			}
			return null;
		}

		// Token: 0x0400012F RID: 303
		private const string UnmappedRequestActionName = "HandleUnmappedRequest";
	}
}
