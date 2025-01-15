using System;
using System.Linq;
using System.Web.Http.Controllers;
using Microsoft.AspNet.OData.Adapters;
using Microsoft.AspNet.OData.Interfaces;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Routing.Conventions
{
	// Token: 0x02000093 RID: 147
	public class EntitySetRoutingConvention : NavigationSourceRoutingConvention
	{
		// Token: 0x0600052B RID: 1323 RVA: 0x00010E9C File Offset: 0x0000F09C
		public override string SelectAction(ODataPath odataPath, HttpControllerContext controllerContext, ILookup<string, HttpActionDescriptor> actionMap)
		{
			NavigationSourceRoutingConvention.ValidateSelectActionParameters(odataPath, controllerContext, actionMap);
			return EntitySetRoutingConvention.SelectActionImpl(odataPath, new WebApiControllerContext(controllerContext, NavigationSourceRoutingConvention.GetControllerResult(controllerContext)), new WebApiActionMap(actionMap));
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x00010EC0 File Offset: 0x0000F0C0
		internal static string SelectActionImpl(ODataPath odataPath, IWebApiControllerContext controllerContext, IWebApiActionMap actionMap)
		{
			if (odataPath.PathTemplate == "~/entityset")
			{
				IEdmEntitySetBase entitySet = ((EntitySetSegment)odataPath.Segments[0]).EntitySet;
				if (controllerContext.Request.Method == ODataRequestMethod.Get)
				{
					return actionMap.FindMatchingAction(new string[]
					{
						"Get" + entitySet.Name,
						"Get"
					});
				}
				if (ODataRequestMethod.Post == controllerContext.Request.Method)
				{
					return actionMap.FindMatchingAction(new string[]
					{
						"Post" + entitySet.EntityType().Name,
						"Post"
					});
				}
			}
			else
			{
				if (odataPath.PathTemplate == "~/entityset/$count" && controllerContext.Request.Method == ODataRequestMethod.Get)
				{
					IEdmEntitySetBase entitySet2 = ((EntitySetSegment)odataPath.Segments[0]).EntitySet;
					return actionMap.FindMatchingAction(new string[]
					{
						"Get" + entitySet2.Name,
						"Get"
					});
				}
				if (odataPath.PathTemplate == "~/entityset/cast")
				{
					IEdmEntitySetBase entitySet3 = ((EntitySetSegment)odataPath.Segments[0]).EntitySet;
					IEdmEntityType edmEntityType = (IEdmEntityType)((IEdmCollectionType)odataPath.EdmType).ElementType.Definition;
					if (controllerContext.Request.Method == ODataRequestMethod.Get)
					{
						return actionMap.FindMatchingAction(new string[]
						{
							"Get" + entitySet3.Name + "From" + edmEntityType.Name,
							"GetFrom" + edmEntityType.Name
						});
					}
					if (ODataRequestMethod.Post == controllerContext.Request.Method)
					{
						return actionMap.FindMatchingAction(new string[]
						{
							"Post" + entitySet3.EntityType().Name + "From" + edmEntityType.Name,
							"PostFrom" + edmEntityType.Name
						});
					}
				}
				else if (odataPath.PathTemplate == "~/entityset/cast/$count" && controllerContext.Request.Method == ODataRequestMethod.Get)
				{
					IEdmEntitySetBase entitySet4 = ((EntitySetSegment)odataPath.Segments[0]).EntitySet;
					IEdmEntityType edmEntityType2 = (IEdmEntityType)((IEdmCollectionType)odataPath.Segments[1].EdmType).ElementType.Definition;
					return actionMap.FindMatchingAction(new string[]
					{
						"Get" + entitySet4.Name + "From" + edmEntityType2.Name,
						"GetFrom" + edmEntityType2.Name
					});
				}
			}
			return null;
		}
	}
}
