using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Controllers;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Routing.Conventions
{
	// Token: 0x02000098 RID: 152
	public abstract class NavigationSourceRoutingConvention : IODataRoutingConvention
	{
		// Token: 0x0600053D RID: 1341 RVA: 0x000117F8 File Offset: 0x0000F9F8
		public virtual string SelectController(ODataPath odataPath, HttpRequestMessage request)
		{
			if (odataPath == null)
			{
				throw Error.ArgumentNull("odataPath");
			}
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			SelectControllerResult selectControllerResult = NavigationSourceRoutingConvention.SelectControllerImpl(odataPath);
			if (selectControllerResult != null)
			{
				request.Properties["AttributeRouteData"] = selectControllerResult.Values;
			}
			if (selectControllerResult == null)
			{
				return null;
			}
			return selectControllerResult.ControllerName;
		}

		// Token: 0x0600053E RID: 1342
		public abstract string SelectAction(ODataPath odataPath, HttpControllerContext controllerContext, ILookup<string, HttpActionDescriptor> actionMap);

		// Token: 0x0600053F RID: 1343 RVA: 0x0001184C File Offset: 0x0000FA4C
		internal static void ValidateSelectActionParameters(ODataPath odataPath, HttpControllerContext controllerContext, ILookup<string, HttpActionDescriptor> actionMap)
		{
			if (odataPath == null)
			{
				throw Error.ArgumentNull("odataPath");
			}
			if (controllerContext == null)
			{
				throw Error.ArgumentNull("controllerContext");
			}
			if (actionMap == null)
			{
				throw Error.ArgumentNull("actionMap");
			}
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x00011878 File Offset: 0x0000FA78
		internal static SelectControllerResult GetControllerResult(HttpControllerContext controllerContext)
		{
			string text = null;
			object obj = null;
			if (controllerContext != null)
			{
				if (controllerContext.Request != null)
				{
					controllerContext.Request.Properties.TryGetValue("AttributeRouteData", out obj);
				}
				if (controllerContext.ControllerDescriptor != null)
				{
					text = controllerContext.ControllerDescriptor.ControllerName;
				}
			}
			return new SelectControllerResult(text, obj as IDictionary<string, object>);
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x000118CC File Offset: 0x0000FACC
		internal static SelectControllerResult SelectControllerImpl(ODataPath odataPath)
		{
			EntitySetSegment entitySetSegment = odataPath.Segments.FirstOrDefault<ODataPathSegment>() as EntitySetSegment;
			if (entitySetSegment != null)
			{
				return new SelectControllerResult(entitySetSegment.EntitySet.Name, null);
			}
			SingletonSegment singletonSegment = odataPath.Segments.FirstOrDefault<ODataPathSegment>() as SingletonSegment;
			if (singletonSegment != null)
			{
				return new SelectControllerResult(singletonSegment.Singleton.Name, null);
			}
			return null;
		}
	}
}
