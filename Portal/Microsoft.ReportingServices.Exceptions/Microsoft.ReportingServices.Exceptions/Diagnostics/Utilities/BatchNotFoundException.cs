using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000079 RID: 121
	[Serializable]
	internal sealed class BatchNotFoundException : ReportCatalogException
	{
		// Token: 0x06000229 RID: 553 RVA: 0x00004B3C File Offset: 0x00002D3C
		public BatchNotFoundException(string batchId)
			: base(ErrorCode.rsBatchNotFound, ErrorStringsWrapper.rsBatchNotFound(batchId), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00004B53 File Offset: 0x00002D53
		private BatchNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
