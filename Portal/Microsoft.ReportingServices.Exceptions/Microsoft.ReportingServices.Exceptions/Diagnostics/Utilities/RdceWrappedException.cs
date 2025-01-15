using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000B0 RID: 176
	[Serializable]
	internal sealed class RdceWrappedException : RSException
	{
		// Token: 0x060002A8 RID: 680 RVA: 0x000055EA File Offset: 0x000037EA
		public RdceWrappedException(Exception innerException)
			: this(innerException, null)
		{
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x000055F4 File Offset: 0x000037F4
		public RdceWrappedException(Exception innerException, string additionalTraceMessage)
			: base(ErrorCode.rsRdceInvalidCacheOptionError, ErrorStringsWrapper.rsRdceWrappedException, innerException, RSTrace.IsTraceInitialized ? RSTrace.CatalogTrace : null, additionalTraceMessage, Array.Empty<object>())
		{
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000561C File Offset: 0x0000381C
		private RdceWrappedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
