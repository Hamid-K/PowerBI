using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200007B RID: 123
	[Serializable]
	internal sealed class CacheRefreshPlanNotFoundException : ReportCatalogException
	{
		// Token: 0x0600022D RID: 557 RVA: 0x00004B7F File Offset: 0x00002D7F
		public CacheRefreshPlanNotFoundException(string idOrData)
			: base(ErrorCode.rsCacheRefreshPlanNotFound, ErrorStringsWrapper.rsCacheRefreshPlanNotFound(idOrData), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00004B99 File Offset: 0x00002D99
		private CacheRefreshPlanNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
