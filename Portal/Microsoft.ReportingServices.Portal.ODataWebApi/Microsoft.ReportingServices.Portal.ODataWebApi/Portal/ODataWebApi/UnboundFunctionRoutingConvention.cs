using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Controllers;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNet.OData.Routing.Conventions;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.ReportingServices.Portal.ODataWebApi
{
	// Token: 0x02000008 RID: 8
	public class UnboundFunctionRoutingConvention : IODataRoutingConvention
	{
		// Token: 0x0600000A RID: 10 RVA: 0x00002445 File Offset: 0x00000645
		public UnboundFunctionRoutingConvention(ILogger logger)
		{
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}
			this._logger = logger;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002464 File Offset: 0x00000664
		public string SelectController(Microsoft.AspNet.OData.Routing.ODataPath odataPath, HttpRequestMessage request)
		{
			if (odataPath.Segments.FirstOrDefault<ODataPathSegment>() is OperationImportSegment)
			{
				this._logger.Trace(TraceLevel.Verbose, () => string.Format("Mapping {0} -> {1} to {2}", odataPath.PathTemplate, request.RequestUri, "UnboundFunction"));
				return "UnboundFunction";
			}
			return null;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000024BC File Offset: 0x000006BC
		public string SelectAction(Microsoft.AspNet.OData.Routing.ODataPath odataPath, HttpControllerContext controllerContext, ILookup<string, HttpActionDescriptor> actionMap)
		{
			OperationImportSegment operationImportSegment = odataPath.Segments.FirstOrDefault<ODataPathSegment>() as OperationImportSegment;
			if (operationImportSegment != null)
			{
				HttpActionDescriptor action = actionMap[operationImportSegment.OperationImports.First<IEdmOperationImport>().Name].FirstOrDefault<HttpActionDescriptor>();
				if (action != null)
				{
					using (IEnumerator<HttpParameterDescriptor> enumerator = action.GetParameters().GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							HttpParameterDescriptor parameter = enumerator.Current;
							ConstantNode constantNode = operationImportSegment.Parameters.FirstOrDefault((OperationSegmentParameter p) => p.Name == parameter.ParameterName).Value as ConstantNode;
							if (constantNode.Value != null)
							{
								controllerContext.RouteData.Values[parameter.ParameterName] = constantNode.Value;
							}
						}
					}
					this._logger.Trace(TraceLevel.Verbose, () => string.Format("Mapping {0} -> {1} to {2}({3})", new object[]
					{
						odataPath.PathTemplate,
						controllerContext.Request.RequestUri,
						"UnboundFunction",
						action.ActionName
					}));
					return action.ActionName;
				}
			}
			return null;
		}

		// Token: 0x0400003A RID: 58
		private readonly ILogger _logger;

		// Token: 0x0400003B RID: 59
		private const string UnboundFunctionController = "UnboundFunction";
	}
}
