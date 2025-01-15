using System;
using System.Diagnostics;
using System.Linq;
using System.Web.Http.Controllers;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNet.OData.Routing.Conventions;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.OData.UriParser;

namespace Microsoft.ReportingServices.Portal.ODataWebApi
{
	// Token: 0x02000007 RID: 7
	public class MethodBasedRoutingConvention : NavigationSourceRoutingConvention
	{
		// Token: 0x06000008 RID: 8 RVA: 0x0000233E File Offset: 0x0000053E
		public MethodBasedRoutingConvention(ILogger logger)
		{
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}
			this._logger = logger;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000235C File Offset: 0x0000055C
		public override string SelectAction(Microsoft.AspNet.OData.Routing.ODataPath odataPath, HttpControllerContext controllerContext, ILookup<string, HttpActionDescriptor> actionMap)
		{
			string name = null;
			EntitySetSegment entitySetSegment = odataPath.Segments.FirstOrDefault<ODataPathSegment>() as EntitySetSegment;
			if (entitySetSegment != null)
			{
				name = entitySetSegment.EntitySet.Name;
			}
			SingletonSegment singletonSegment = odataPath.Segments.FirstOrDefault<ODataPathSegment>() as SingletonSegment;
			if (singletonSegment != null)
			{
				name = singletonSegment.Singleton.Name;
			}
			if (name != null)
			{
				HttpActionDescriptor action = actionMap[controllerContext.Request.Method.Method].FirstOrDefault<HttpActionDescriptor>();
				if (action != null)
				{
					this._logger.Trace(TraceLevel.Verbose, () => string.Format("Mapping {0} -> {1} to {2}({3})", new object[]
					{
						odataPath.PathTemplate,
						controllerContext.Request.RequestUri,
						name,
						action.ActionName
					}));
					return action.ActionName;
				}
			}
			return null;
		}

		// Token: 0x04000039 RID: 57
		private readonly ILogger _logger;
	}
}
