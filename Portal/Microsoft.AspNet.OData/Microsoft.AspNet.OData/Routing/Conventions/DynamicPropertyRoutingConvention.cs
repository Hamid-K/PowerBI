using System;
using System.Globalization;
using System.Linq;
using System.Web.Http.Controllers;
using Microsoft.AspNet.OData.Adapters;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNet.OData.Interfaces;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Routing.Conventions
{
	// Token: 0x02000091 RID: 145
	public class DynamicPropertyRoutingConvention : NavigationSourceRoutingConvention
	{
		// Token: 0x06000525 RID: 1317 RVA: 0x00010A98 File Offset: 0x0000EC98
		public override string SelectAction(ODataPath odataPath, HttpControllerContext controllerContext, ILookup<string, HttpActionDescriptor> actionMap)
		{
			NavigationSourceRoutingConvention.ValidateSelectActionParameters(odataPath, controllerContext, actionMap);
			return DynamicPropertyRoutingConvention.SelectActionImpl(odataPath, new WebApiControllerContext(controllerContext, NavigationSourceRoutingConvention.GetControllerResult(controllerContext)), new WebApiActionMap(actionMap));
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x00010ABC File Offset: 0x0000ECBC
		internal static string SelectActionImpl(ODataPath odataPath, IWebApiControllerContext controllerContext, IWebApiActionMap actionMap)
		{
			string text = null;
			DynamicPathSegment dynamicPathSegment = null;
			string pathTemplate = odataPath.PathTemplate;
			if (pathTemplate != null)
			{
				uint num = <PrivateImplementationDetails>.ComputeStringHash(pathTemplate);
				if (num <= 2423214442U)
				{
					if (num <= 2238205006U)
					{
						if (num != 1806816046U)
						{
							if (num != 2238205006U)
							{
								goto IL_0236;
							}
							if (!(pathTemplate == "~/entityset/key/property/dynamicproperty"))
							{
								goto IL_0236;
							}
							goto IL_0191;
						}
						else
						{
							if (!(pathTemplate == "~/entityset/key/cast/property/dynamicproperty"))
							{
								goto IL_0236;
							}
							goto IL_0191;
						}
					}
					else if (num != 2360972896U)
					{
						if (num != 2423214442U)
						{
							goto IL_0236;
						}
						if (!(pathTemplate == "~/singleton/property/dynamicproperty"))
						{
							goto IL_0236;
						}
						goto IL_0191;
					}
					else if (!(pathTemplate == "~/singleton/dynamicproperty"))
					{
						goto IL_0236;
					}
				}
				else if (num <= 3364216812U)
				{
					if (num != 2640684946U)
					{
						if (num != 3364216812U)
						{
							goto IL_0236;
						}
						if (!(pathTemplate == "~/entityset/key/dynamicproperty"))
						{
							goto IL_0236;
						}
					}
					else
					{
						if (!(pathTemplate == "~/singleton/cast/property/dynamicproperty"))
						{
							goto IL_0236;
						}
						goto IL_0191;
					}
				}
				else if (num != 3825521612U)
				{
					if (num != 3934195464U)
					{
						goto IL_0236;
					}
					if (!(pathTemplate == "~/singleton/cast/dynamicproperty"))
					{
						goto IL_0236;
					}
				}
				else if (!(pathTemplate == "~/entityset/key/cast/dynamicproperty"))
				{
					goto IL_0236;
				}
				dynamicPathSegment = odataPath.Segments.Last<ODataPathSegment>() as DynamicPathSegment;
				if (dynamicPathSegment == null)
				{
					return null;
				}
				if (controllerContext.Request.Method == ODataRequestMethod.Get)
				{
					string text2 = string.Format(CultureInfo.InvariantCulture, "Get{0}", new object[] { "DynamicProperty" });
					text = actionMap.FindMatchingAction(new string[] { text2 });
					goto IL_0236;
				}
				goto IL_0236;
				IL_0191:
				dynamicPathSegment = odataPath.Segments.Last<ODataPathSegment>() as DynamicPathSegment;
				if (dynamicPathSegment == null)
				{
					return null;
				}
				PropertySegment propertySegment = odataPath.Segments[odataPath.Segments.Count - 2] as PropertySegment;
				if (propertySegment == null)
				{
					return null;
				}
				if (!(propertySegment.Property.Type.Definition is EdmComplexType))
				{
					return null;
				}
				if (controllerContext.Request.Method == ODataRequestMethod.Get)
				{
					string text3 = string.Format(CultureInfo.InvariantCulture, "Get{0}", new object[] { "DynamicProperty" });
					text = actionMap.FindMatchingAction(new string[] { text3 + "From" + propertySegment.Property.Name });
				}
			}
			IL_0236:
			if (text != null)
			{
				if (odataPath.PathTemplate.StartsWith("~/entityset/key", StringComparison.Ordinal))
				{
					KeySegment keySegment = (KeySegment)odataPath.Segments[1];
					controllerContext.AddKeyValueToRouteData(keySegment, "key");
				}
				controllerContext.RouteData.Add(ODataRouteConstants.DynamicProperty, dynamicPathSegment.Identifier);
				string text4 = "DF908045-6922-46A0-82F2-2F6E7F43D1B1_" + ODataRouteConstants.DynamicProperty;
				ODataParameterValue odataParameterValue = new ODataParameterValue(dynamicPathSegment.Identifier, EdmLibHelpers.GetEdmPrimitiveTypeReferenceOrNull(typeof(string)));
				controllerContext.RouteData.Add(text4, odataParameterValue);
				controllerContext.Request.Context.RoutingConventionsStore.Add(text4, odataParameterValue);
				return text;
			}
			return null;
		}

		// Token: 0x0400012B RID: 299
		private const string ActionName = "DynamicProperty";
	}
}
