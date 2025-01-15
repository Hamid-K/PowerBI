using System;
using System.Linq;
using System.Web.Http.Controllers;
using Microsoft.AspNet.OData.Adapters;
using Microsoft.AspNet.OData.Interfaces;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Routing.Conventions
{
	// Token: 0x02000097 RID: 151
	public class NavigationRoutingConvention : NavigationSourceRoutingConvention
	{
		// Token: 0x06000539 RID: 1337 RVA: 0x000115EC File Offset: 0x0000F7EC
		public override string SelectAction(ODataPath odataPath, HttpControllerContext controllerContext, ILookup<string, HttpActionDescriptor> actionMap)
		{
			NavigationSourceRoutingConvention.ValidateSelectActionParameters(odataPath, controllerContext, actionMap);
			return NavigationRoutingConvention.SelectActionImpl(odataPath, new WebApiControllerContext(controllerContext, NavigationSourceRoutingConvention.GetControllerResult(controllerContext)), new WebApiActionMap(actionMap));
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00011610 File Offset: 0x0000F810
		internal static string SelectActionImpl(ODataPath odataPath, IWebApiControllerContext controllerContext, IWebApiActionMap actionMap)
		{
			ODataRequestMethod method = controllerContext.Request.Method;
			string actionMethodPrefix = NavigationRoutingConvention.GetActionMethodPrefix(method);
			if (actionMethodPrefix == null)
			{
				return null;
			}
			if (odataPath.PathTemplate == "~/entityset/key/navigation" || odataPath.PathTemplate == "~/entityset/key/navigation/$count" || odataPath.PathTemplate == "~/entityset/key/cast/navigation" || odataPath.PathTemplate == "~/entityset/key/cast/navigation/$count" || odataPath.PathTemplate == "~/singleton/navigation" || odataPath.PathTemplate == "~/singleton/navigation/$count" || odataPath.PathTemplate == "~/singleton/cast/navigation" || odataPath.PathTemplate == "~/singleton/cast/navigation/$count")
			{
				IEdmNavigationProperty navigationProperty = ((odataPath.Segments.Last<ODataPathSegment>() as NavigationPropertySegment) ?? (odataPath.Segments[odataPath.Segments.Count - 2] as NavigationPropertySegment)).NavigationProperty;
				IEdmEntityType edmEntityType = navigationProperty.DeclaringType as IEdmEntityType;
				if (navigationProperty.TargetMultiplicity() != EdmMultiplicity.Many && ODataRequestMethod.Post == method)
				{
					return null;
				}
				if (navigationProperty.TargetMultiplicity() == EdmMultiplicity.Many && (ODataRequestMethod.Put == method || ODataRequestMethod.Patch == method))
				{
					return null;
				}
				if (odataPath.Segments.Last<ODataPathSegment>() is CountSegment && method != ODataRequestMethod.Get)
				{
					return null;
				}
				if (edmEntityType != null)
				{
					string text = actionMap.FindMatchingAction(new string[]
					{
						actionMethodPrefix + navigationProperty.Name + "From" + edmEntityType.Name,
						actionMethodPrefix + navigationProperty.Name
					});
					if (text != null)
					{
						if (odataPath.PathTemplate.StartsWith("~/entityset/key", StringComparison.Ordinal))
						{
							KeySegment keySegment = (KeySegment)odataPath.Segments[1];
							controllerContext.AddKeyValueToRouteData(keySegment, "key");
						}
						return text;
					}
				}
			}
			return null;
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x000117BC File Offset: 0x0000F9BC
		private static string GetActionMethodPrefix(ODataRequestMethod method)
		{
			switch (method)
			{
			case ODataRequestMethod.Get:
				return "Get";
			case ODataRequestMethod.Patch:
				return "PatchTo";
			case ODataRequestMethod.Post:
				return "PostTo";
			case ODataRequestMethod.Put:
				return "PutTo";
			}
			return null;
		}
	}
}
