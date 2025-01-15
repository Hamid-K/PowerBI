using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Controllers;
using Microsoft.AspNet.OData.Adapters;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Interfaces;

namespace Microsoft.AspNet.OData.Routing.Conventions
{
	// Token: 0x02000096 RID: 150
	public class MetadataRoutingConvention : IODataRoutingConvention
	{
		// Token: 0x06000534 RID: 1332 RVA: 0x000114A4 File Offset: 0x0000F6A4
		public string SelectController(ODataPath odataPath, HttpRequestMessage request)
		{
			SelectControllerResult selectControllerResult = MetadataRoutingConvention.SelectControllerImpl(odataPath, new WebApiRequestMessage(request));
			if (selectControllerResult == null)
			{
				return null;
			}
			return selectControllerResult.ControllerName;
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x000114CC File Offset: 0x0000F6CC
		public string SelectAction(ODataPath odataPath, HttpControllerContext controllerContext, ILookup<string, HttpActionDescriptor> actionMap)
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
			SelectControllerResult selectControllerResult = new SelectControllerResult(controllerContext.ControllerDescriptor.ControllerName, null);
			return MetadataRoutingConvention.SelectActionImpl(odataPath, new WebApiControllerContext(controllerContext, selectControllerResult), new WebApiActionMap(actionMap));
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00011528 File Offset: 0x0000F728
		internal static SelectControllerResult SelectControllerImpl(ODataPath odataPath, IWebApiRequestMessage request)
		{
			if (odataPath == null)
			{
				throw Error.ArgumentNull("odataPath");
			}
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			if (odataPath.PathTemplate == "~" || odataPath.PathTemplate == "~/$metadata")
			{
				return new SelectControllerResult("Metadata", null);
			}
			return null;
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x00011584 File Offset: 0x0000F784
		internal static string SelectActionImpl(ODataPath odataPath, IWebApiControllerContext controllerContext, IWebApiActionMap actionMap)
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
			if (odataPath.PathTemplate == "~")
			{
				return "GetServiceDocument";
			}
			if (odataPath.PathTemplate == "~/$metadata")
			{
				return "GetMetadata";
			}
			return null;
		}
	}
}
