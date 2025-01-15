using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000AA RID: 170
	[Serializable]
	internal sealed class RdceMismatchException : RSException
	{
		// Token: 0x0600029C RID: 668 RVA: 0x000054BB File Offset: 0x000036BB
		public RdceMismatchException(string rdceSet, string rdceConfigured)
			: base(ErrorCode.rsRdceMismatchError, ErrorStringsWrapper.rsRdceMismatchError(rdceSet, rdceConfigured), null, RSTrace.IsTraceInitialized ? RSTrace.CatalogTrace : null, null, Array.Empty<object>())
		{
		}

		// Token: 0x0600029D RID: 669 RVA: 0x000054E5 File Offset: 0x000036E5
		private RdceMismatchException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
