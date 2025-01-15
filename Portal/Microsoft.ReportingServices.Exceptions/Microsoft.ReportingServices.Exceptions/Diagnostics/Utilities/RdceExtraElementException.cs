using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000A9 RID: 169
	[Serializable]
	internal sealed class RdceExtraElementException : RSException
	{
		// Token: 0x0600029A RID: 666 RVA: 0x00005488 File Offset: 0x00003688
		public RdceExtraElementException(string nodeName)
			: base(ErrorCode.rsRdceExtraElementError, ErrorStringsWrapper.rsRdceExtraElementError(nodeName), null, RSTrace.IsTraceInitialized ? RSTrace.CatalogTrace : null, null, Array.Empty<object>())
		{
		}

		// Token: 0x0600029B RID: 667 RVA: 0x000054B1 File Offset: 0x000036B1
		private RdceExtraElementException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
