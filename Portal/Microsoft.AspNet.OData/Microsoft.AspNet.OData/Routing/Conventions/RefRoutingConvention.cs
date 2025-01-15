using System;
using System.Linq;
using System.Web.Http.Controllers;
using Microsoft.AspNet.OData.Adapters;
using Microsoft.AspNet.OData.Interfaces;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Routing.Conventions
{
	// Token: 0x0200009B RID: 155
	public class RefRoutingConvention : NavigationSourceRoutingConvention
	{
		// Token: 0x06000549 RID: 1353 RVA: 0x00011E6C File Offset: 0x0001006C
		public override string SelectAction(ODataPath odataPath, HttpControllerContext controllerContext, ILookup<string, HttpActionDescriptor> actionMap)
		{
			NavigationSourceRoutingConvention.ValidateSelectActionParameters(odataPath, controllerContext, actionMap);
			return RefRoutingConvention.SelectActionImpl(odataPath, new WebApiControllerContext(controllerContext, NavigationSourceRoutingConvention.GetControllerResult(controllerContext)), new WebApiActionMap(actionMap));
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x00011E90 File Offset: 0x00010090
		internal static string SelectActionImpl(ODataPath odataPath, IWebApiControllerContext controllerContext, IWebApiActionMap actionMap)
		{
			ODataRequestMethod method = controllerContext.Request.Method;
			if (!RefRoutingConvention.IsSupportedRequestMethod(method))
			{
				return null;
			}
			if (odataPath.PathTemplate == "~/entityset/key/navigation/$ref" || odataPath.PathTemplate == "~/entityset/key/cast/navigation/$ref" || odataPath.PathTemplate == "~/singleton/navigation/$ref" || odataPath.PathTemplate == "~/singleton/cast/navigation/$ref")
			{
				NavigationPropertyLinkSegment navigationPropertyLinkSegment = (NavigationPropertyLinkSegment)odataPath.Segments.Last<ODataPathSegment>();
				IEdmNavigationProperty navigationProperty = navigationPropertyLinkSegment.NavigationProperty;
				IEdmEntityType edmEntityType = navigationProperty.DeclaringEntityType();
				string text = RefRoutingConvention.FindRefActionName(actionMap, navigationProperty, edmEntityType, method);
				if (text != null)
				{
					if (odataPath.PathTemplate.StartsWith("~/entityset/key", StringComparison.Ordinal))
					{
						controllerContext.AddKeyValueToRouteData((KeySegment)odataPath.Segments[1], "key");
					}
					controllerContext.RouteData.Add(ODataRouteConstants.NavigationProperty, navigationPropertyLinkSegment.NavigationProperty.Name);
					return text;
				}
			}
			else if (ODataRequestMethod.Delete == method && (odataPath.PathTemplate == "~/entityset/key/navigation/key/$ref" || odataPath.PathTemplate == "~/entityset/key/cast/navigation/key/$ref" || odataPath.PathTemplate == "~/singleton/navigation/key/$ref" || odataPath.PathTemplate == "~/singleton/cast/navigation/key/$ref"))
			{
				NavigationPropertyLinkSegment navigationPropertyLinkSegment2 = (NavigationPropertyLinkSegment)odataPath.Segments[odataPath.Segments.Count - 2];
				IEdmNavigationProperty navigationProperty2 = navigationPropertyLinkSegment2.NavigationProperty;
				IEdmEntityType edmEntityType2 = navigationProperty2.DeclaringEntityType();
				string text2 = RefRoutingConvention.FindRefActionName(actionMap, navigationProperty2, edmEntityType2, method);
				if (text2 != null)
				{
					if (odataPath.PathTemplate.StartsWith("~/entityset/key", StringComparison.Ordinal))
					{
						controllerContext.AddKeyValueToRouteData((KeySegment)odataPath.Segments[1], "key");
					}
					controllerContext.RouteData.Add(ODataRouteConstants.NavigationProperty, navigationPropertyLinkSegment2.NavigationProperty.Name);
					controllerContext.AddKeyValueToRouteData((KeySegment)odataPath.Segments.Last((ODataPathSegment e) => e is KeySegment), ODataRouteConstants.RelatedKey);
					return text2;
				}
			}
			return null;
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x0001209C File Offset: 0x0001029C
		private static string FindRefActionName(IWebApiActionMap actionMap, IEdmNavigationProperty navigationProperty, IEdmEntityType declaringType, ODataRequestMethod method)
		{
			string text;
			if (method != ODataRequestMethod.Get)
			{
				if (method == ODataRequestMethod.Delete)
				{
					text = "DeleteRef";
				}
				else
				{
					text = "CreateRef";
				}
			}
			else
			{
				text = "GetRef";
			}
			return actionMap.FindMatchingAction(new string[]
			{
				string.Concat(new string[] { text, "To", navigationProperty.Name, "From", declaringType.Name }),
				text + "To" + navigationProperty.Name,
				text
			});
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x0001211E File Offset: 0x0001031E
		private static bool IsSupportedRequestMethod(ODataRequestMethod method)
		{
			return ODataRequestMethod.Delete == method || ODataRequestMethod.Put == method || ODataRequestMethod.Post == method || method == ODataRequestMethod.Get;
		}

		// Token: 0x0400012C RID: 300
		private const string DeleteRefActionNamePrefix = "DeleteRef";

		// Token: 0x0400012D RID: 301
		private const string CreateRefActionNamePrefix = "CreateRef";

		// Token: 0x0400012E RID: 302
		private const string GetRefActionNamePrefix = "GetRef";
	}
}
