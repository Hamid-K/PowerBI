using System;
using System.Linq;
using System.Web.Http.Controllers;
using Microsoft.AspNet.OData.Adapters;
using Microsoft.AspNet.OData.Interfaces;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Routing.Conventions
{
	// Token: 0x02000092 RID: 146
	public class EntityRoutingConvention : NavigationSourceRoutingConvention
	{
		// Token: 0x06000528 RID: 1320 RVA: 0x00010DA6 File Offset: 0x0000EFA6
		public override string SelectAction(ODataPath odataPath, HttpControllerContext controllerContext, ILookup<string, HttpActionDescriptor> actionMap)
		{
			NavigationSourceRoutingConvention.ValidateSelectActionParameters(odataPath, controllerContext, actionMap);
			return EntityRoutingConvention.SelectActionImpl(odataPath, new WebApiControllerContext(controllerContext, NavigationSourceRoutingConvention.GetControllerResult(controllerContext)), new WebApiActionMap(actionMap));
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00010DC8 File Offset: 0x0000EFC8
		internal static string SelectActionImpl(ODataPath odataPath, IWebApiControllerContext controllerContext, IWebApiActionMap actionMap)
		{
			if (odataPath.PathTemplate == "~/entityset/key" || odataPath.PathTemplate == "~/entityset/key/cast")
			{
				string text;
				switch (controllerContext.Request.Method)
				{
				case ODataRequestMethod.Get:
					text = "Get";
					goto IL_0075;
				case ODataRequestMethod.Delete:
					text = "Delete";
					goto IL_0075;
				case ODataRequestMethod.Merge:
				case ODataRequestMethod.Patch:
					text = "Patch";
					goto IL_0075;
				case ODataRequestMethod.Put:
					text = "Put";
					goto IL_0075;
				}
				return null;
				IL_0075:
				IEdmEntityType edmEntityType = (IEdmEntityType)odataPath.EdmType;
				string text2 = actionMap.FindMatchingAction(new string[]
				{
					text + edmEntityType.Name,
					text
				});
				if (text2 != null)
				{
					KeySegment keySegment = (KeySegment)odataPath.Segments[1];
					controllerContext.AddKeyValueToRouteData(keySegment, "key");
					return text2;
				}
			}
			return null;
		}
	}
}
