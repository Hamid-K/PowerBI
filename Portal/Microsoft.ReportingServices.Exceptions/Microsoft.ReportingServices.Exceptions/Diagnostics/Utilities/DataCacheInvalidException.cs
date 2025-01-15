using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000B3 RID: 179
	internal sealed class DataCacheInvalidException : RSException
	{
		// Token: 0x060002B0 RID: 688 RVA: 0x000056B1 File Offset: 0x000038B1
		public DataCacheInvalidException(Exception innerException)
			: base(ErrorCode.rsDataCacheMismatch, ErrorStringsWrapper.rsDataCacheMismatch, innerException, RSTrace.IsTraceInitialized ? RSTrace.CatalogTrace : null, null, Array.Empty<object>())
		{
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x000056D9 File Offset: 0x000038D9
		private DataCacheInvalidException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
