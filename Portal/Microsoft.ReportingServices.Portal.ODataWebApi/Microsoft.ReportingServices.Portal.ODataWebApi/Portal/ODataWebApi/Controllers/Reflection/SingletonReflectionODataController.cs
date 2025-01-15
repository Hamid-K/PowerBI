using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNet.OData.Routing;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.OData.UriParser;
using Microsoft.ReportingServices.Portal.ODataWebApi.Controllers.SegmentHandlers;

namespace Microsoft.ReportingServices.Portal.ODataWebApi.Controllers.Reflection
{
	// Token: 0x02000043 RID: 67
	public abstract class SingletonReflectionODataController<T> : ReflectionODataController<T>
	{
		// Token: 0x0600031E RID: 798
		protected abstract T GetSingleton();

		// Token: 0x0600031F RID: 799 RVA: 0x0000D4BC File Offset: 0x0000B6BC
		protected SingletonReflectionODataController(ILogger logger)
			: base(logger)
		{
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000D4C5 File Offset: 0x0000B6C5
		internal SingletonReflectionODataController(ILogger logger, Dictionary<string, ISegmentHandler> handlers)
			: base(logger, handlers)
		{
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000D4D0 File Offset: 0x0000B6D0
		protected override object GetRoot(Microsoft.AspNet.OData.Routing.ODataPath oDataPath, out int index)
		{
			if (oDataPath.PathTemplate.StartsWith("~/singleton"))
			{
				string singletonName = ((SingletonSegment)oDataPath.Segments[0]).Singleton.Name;
				base.Logger.Trace(TraceLevel.Verbose, () => string.Format("Getting '{0}'", singletonName));
				index = 1;
				return this.GetSingleton();
			}
			throw new InvalidOperationException(string.Format("No singleton handler for path '{0}'", oDataPath.PathTemplate));
		}
	}
}
