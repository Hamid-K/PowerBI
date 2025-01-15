using System;
using System.Linq;
using System.Web.Http.Controllers;
using Microsoft.AspNet.OData.Adapters;
using Microsoft.AspNet.OData.Interfaces;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Routing.Conventions
{
	// Token: 0x0200009A RID: 154
	public class PropertyRoutingConvention : NavigationSourceRoutingConvention
	{
		// Token: 0x06000545 RID: 1349 RVA: 0x000119F3 File Offset: 0x0000FBF3
		public override string SelectAction(ODataPath odataPath, HttpControllerContext controllerContext, ILookup<string, HttpActionDescriptor> actionMap)
		{
			NavigationSourceRoutingConvention.ValidateSelectActionParameters(odataPath, controllerContext, actionMap);
			return PropertyRoutingConvention.SelectActionImpl(odataPath, new WebApiControllerContext(controllerContext, NavigationSourceRoutingConvention.GetControllerResult(controllerContext)), new WebApiActionMap(actionMap));
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00011A18 File Offset: 0x0000FC18
		internal static string SelectActionImpl(ODataPath odataPath, IWebApiControllerContext controllerContext, IWebApiActionMap actionMap)
		{
			string text;
			TypeSegment typeSegment;
			IEdmProperty property = PropertyRoutingConvention.GetProperty(odataPath, controllerContext.Request.Method, out text, out typeSegment);
			IEdmEntityType edmEntityType = ((property == null) ? null : (property.DeclaringType as IEdmEntityType));
			if (edmEntityType != null)
			{
				string text2;
				if (typeSegment == null)
				{
					text2 = actionMap.FindMatchingAction(new string[]
					{
						text + property.Name + "From" + edmEntityType.Name,
						text + property.Name
					});
				}
				else
				{
					IEdmComplexType edmComplexType;
					if (typeSegment.EdmType.TypeKind == EdmTypeKind.Collection)
					{
						edmComplexType = ((IEdmCollectionType)typeSegment.EdmType).ElementType.AsComplex().ComplexDefinition();
					}
					else
					{
						edmComplexType = (IEdmComplexType)typeSegment.EdmType;
					}
					text2 = actionMap.FindMatchingAction(new string[]
					{
						string.Concat(new string[] { text, property.Name, "Of", edmComplexType.Name, "From", edmEntityType.Name }),
						text + property.Name + "Of" + edmComplexType.Name
					});
				}
				if (text2 != null)
				{
					if (odataPath.PathTemplate.StartsWith("~/entityset/key", StringComparison.Ordinal))
					{
						KeySegment keySegment = (KeySegment)odataPath.Segments[1];
						controllerContext.AddKeyValueToRouteData(keySegment, "key");
					}
					return text2;
				}
			}
			return null;
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00011B70 File Offset: 0x0000FD70
		private static IEdmProperty GetProperty(ODataPath odataPath, ODataRequestMethod method, out string prefix, out TypeSegment cast)
		{
			prefix = string.Empty;
			cast = null;
			PropertySegment propertySegment = null;
			if (odataPath.PathTemplate == "~/entityset/key/property" || odataPath.PathTemplate == "~/entityset/key/cast/property" || odataPath.PathTemplate == "~/singleton/property" || odataPath.PathTemplate == "~/singleton/cast/property")
			{
				PropertySegment propertySegment2 = (PropertySegment)odataPath.Segments[odataPath.Segments.Count - 1];
				switch (method)
				{
				case ODataRequestMethod.Get:
					prefix = "Get";
					propertySegment = propertySegment2;
					break;
				case ODataRequestMethod.Delete:
					if (propertySegment2.Property.Type.IsNullable)
					{
						prefix = "DeleteTo";
						propertySegment = propertySegment2;
					}
					break;
				case ODataRequestMethod.Patch:
					if (!propertySegment2.Property.Type.IsCollection())
					{
						prefix = "PatchTo";
						propertySegment = propertySegment2;
					}
					break;
				case ODataRequestMethod.Post:
					if (propertySegment2.Property.Type.IsCollection())
					{
						prefix = "PostTo";
						propertySegment = propertySegment2;
					}
					break;
				case ODataRequestMethod.Put:
					prefix = "PutTo";
					propertySegment = propertySegment2;
					break;
				}
			}
			else if (odataPath.PathTemplate == "~/entityset/key/property/cast" || odataPath.PathTemplate == "~/entityset/key/cast/property/cast" || odataPath.PathTemplate == "~/singleton/property/cast" || odataPath.PathTemplate == "~/singleton/cast/property/cast")
			{
				PropertySegment propertySegment3 = (PropertySegment)odataPath.Segments[odataPath.Segments.Count - 2];
				TypeSegment typeSegment = (TypeSegment)odataPath.Segments.Last<ODataPathSegment>();
				switch (method)
				{
				case ODataRequestMethod.Get:
					prefix = "Get";
					propertySegment = propertySegment3;
					cast = typeSegment;
					break;
				case ODataRequestMethod.Patch:
					if (!propertySegment3.Property.Type.IsCollection())
					{
						prefix = "PatchTo";
						propertySegment = propertySegment3;
						cast = typeSegment;
					}
					break;
				case ODataRequestMethod.Post:
					if (propertySegment3.Property.Type.IsCollection())
					{
						prefix = "PostTo";
						propertySegment = propertySegment3;
						cast = typeSegment;
					}
					break;
				case ODataRequestMethod.Put:
					prefix = "PutTo";
					propertySegment = propertySegment3;
					cast = typeSegment;
					break;
				}
			}
			else if (odataPath.PathTemplate == "~/entityset/key/property/$value" || odataPath.PathTemplate == "~/entityset/key/cast/property/$value" || odataPath.PathTemplate == "~/singleton/property/$value" || odataPath.PathTemplate == "~/singleton/cast/property/$value" || odataPath.PathTemplate == "~/entityset/key/property/$count" || odataPath.PathTemplate == "~/entityset/key/cast/property/$count" || odataPath.PathTemplate == "~/singleton/property/$count" || odataPath.PathTemplate == "~/singleton/cast/property/$count")
			{
				PropertySegment propertySegment4 = (PropertySegment)odataPath.Segments[odataPath.Segments.Count - 2];
				if (method == ODataRequestMethod.Get)
				{
					prefix = "Get";
					propertySegment = propertySegment4;
				}
			}
			if (propertySegment != null)
			{
				return propertySegment.Property;
			}
			return null;
		}
	}
}
