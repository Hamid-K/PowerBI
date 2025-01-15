using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000AC RID: 172
	internal static class ContentCacheManagerFactory
	{
		// Token: 0x060007EB RID: 2027 RVA: 0x00020818 File Offset: 0x0001EA18
		internal static IContentCacheManager CreateJsonContentCache(Guid dataSetId, string dataSetPath, ParameterInfoCollection queryParameters, IRSRequestParameters requestParameters, IRSService rsService)
		{
			return new JsonContentCacheManager(dataSetId, dataSetPath, queryParameters, requestParameters, rsService);
		}
	}
}
