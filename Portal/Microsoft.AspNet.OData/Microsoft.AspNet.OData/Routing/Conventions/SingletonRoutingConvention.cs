using System;
using System.Linq;
using System.Web.Http.Controllers;
using Microsoft.AspNet.OData.Adapters;
using Microsoft.AspNet.OData.Interfaces;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Routing.Conventions
{
	// Token: 0x0200009C RID: 156
	public class SingletonRoutingConvention : NavigationSourceRoutingConvention
	{
		// Token: 0x0600054E RID: 1358 RVA: 0x00012132 File Offset: 0x00010332
		public override string SelectAction(ODataPath odataPath, HttpControllerContext controllerContext, ILookup<string, HttpActionDescriptor> actionMap)
		{
			NavigationSourceRoutingConvention.ValidateSelectActionParameters(odataPath, controllerContext, actionMap);
			return SingletonRoutingConvention.SelectActionImpl(odataPath, new WebApiControllerContext(controllerContext, NavigationSourceRoutingConvention.GetControllerResult(controllerContext)), new WebApiActionMap(actionMap));
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x00012154 File Offset: 0x00010354
		internal static string SelectActionImpl(ODataPath odataPath, IWebApiControllerContext controllerContext, IWebApiActionMap actionMap)
		{
			if (odataPath.PathTemplate == "~/singleton")
			{
				SingletonSegment singletonSegment = (SingletonSegment)odataPath.Segments[0];
				string actionNamePrefix = SingletonRoutingConvention.GetActionNamePrefix(controllerContext.Request.Method);
				if (actionNamePrefix != null)
				{
					return actionMap.FindMatchingAction(new string[]
					{
						actionNamePrefix + singletonSegment.Singleton.Name,
						actionNamePrefix
					});
				}
			}
			else if (odataPath.PathTemplate == "~/singleton/cast")
			{
				SingletonSegment singletonSegment2 = (SingletonSegment)odataPath.Segments[0];
				IEdmEntityType edmEntityType = (IEdmEntityType)odataPath.EdmType;
				string actionNamePrefix2 = SingletonRoutingConvention.GetActionNamePrefix(controllerContext.Request.Method);
				if (actionNamePrefix2 != null)
				{
					return actionMap.FindMatchingAction(new string[]
					{
						actionNamePrefix2 + singletonSegment2.Singleton.Name + "From" + edmEntityType.Name,
						actionNamePrefix2 + "From" + edmEntityType.Name
					});
				}
			}
			return null;
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x0001224C File Offset: 0x0001044C
		private static string GetActionNamePrefix(ODataRequestMethod method)
		{
			switch (method)
			{
			case ODataRequestMethod.Get:
				return "Get";
			case ODataRequestMethod.Merge:
			case ODataRequestMethod.Patch:
				return "Patch";
			case ODataRequestMethod.Put:
				return "Put";
			}
			return null;
		}
	}
}
