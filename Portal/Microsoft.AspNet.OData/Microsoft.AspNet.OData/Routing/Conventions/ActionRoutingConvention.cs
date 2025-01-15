using System;
using System.Linq;
using System.Web.Http.Controllers;
using Microsoft.AspNet.OData.Adapters;
using Microsoft.AspNet.OData.Interfaces;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Routing.Conventions
{
	// Token: 0x0200008F RID: 143
	public class ActionRoutingConvention : NavigationSourceRoutingConvention
	{
		// Token: 0x0600050D RID: 1293 RVA: 0x00010348 File Offset: 0x0000E548
		public override string SelectAction(ODataPath odataPath, HttpControllerContext controllerContext, ILookup<string, HttpActionDescriptor> actionMap)
		{
			NavigationSourceRoutingConvention.ValidateSelectActionParameters(odataPath, controllerContext, actionMap);
			return ActionRoutingConvention.SelectActionImpl(odataPath, new WebApiControllerContext(controllerContext, NavigationSourceRoutingConvention.GetControllerResult(controllerContext)), new WebApiActionMap(actionMap));
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0001036C File Offset: 0x0000E56C
		internal static string SelectActionImpl(ODataPath odataPath, IWebApiControllerContext controllerContext, IWebApiActionMap actionMap)
		{
			if (ODataRequestMethod.Post == controllerContext.Request.Method)
			{
				string pathTemplate = odataPath.PathTemplate;
				if (pathTemplate != null)
				{
					if (pathTemplate == "~/entityset/key/cast/action" || pathTemplate == "~/entityset/key/action")
					{
						string text = ActionRoutingConvention.GetAction(odataPath).SelectAction(actionMap, false);
						if (text != null)
						{
							KeySegment keySegment = (KeySegment)odataPath.Segments[1];
							controllerContext.AddKeyValueToRouteData(keySegment, "key");
						}
						return text;
					}
					if (pathTemplate == "~/entityset/cast/action" || pathTemplate == "~/entityset/action")
					{
						return ActionRoutingConvention.GetAction(odataPath).SelectAction(actionMap, true);
					}
					if (pathTemplate == "~/singleton/action" || pathTemplate == "~/singleton/cast/action")
					{
						return ActionRoutingConvention.GetAction(odataPath).SelectAction(actionMap, false);
					}
				}
			}
			return null;
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x00010434 File Offset: 0x0000E634
		private static IEdmAction GetAction(ODataPath odataPath)
		{
			ODataPathSegment odataPathSegment = odataPath.Segments.Last<ODataPathSegment>();
			IEdmAction edmAction = null;
			OperationSegment operationSegment = odataPathSegment as OperationSegment;
			if (operationSegment != null)
			{
				edmAction = operationSegment.Operations.First<IEdmOperation>() as IEdmAction;
			}
			return edmAction;
		}
	}
}
