using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000B1 RID: 177
	[Serializable]
	internal sealed class RdceMismatchRdlVersion : RSException
	{
		// Token: 0x060002AB RID: 683 RVA: 0x00005628 File Offset: 0x00003828
		public RdceMismatchRdlVersion(string originalNamespace, string processedNamespace)
			: base(ErrorCode.rsRdceMismatchRdlVersion, ErrorStringsWrapper.rsRdceMismatchRdlVersion, null, RSTrace.IsTraceInitialized ? RSTrace.CatalogTrace : null, string.Format(CultureInfo.CurrentCulture, "Original namespace = '{0}', new namesapce = '{1}'", originalNamespace, processedNamespace), Array.Empty<object>())
		{
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000566B File Offset: 0x0000386B
		private RdceMismatchRdlVersion(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
