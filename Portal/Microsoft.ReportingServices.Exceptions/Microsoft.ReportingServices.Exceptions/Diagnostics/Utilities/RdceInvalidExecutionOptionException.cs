using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000AE RID: 174
	[Serializable]
	internal sealed class RdceInvalidExecutionOptionException : RSException
	{
		// Token: 0x060002A4 RID: 676 RVA: 0x00005586 File Offset: 0x00003786
		public RdceInvalidExecutionOptionException()
			: base(ErrorCode.rsRdceInvalidExecutionOptionError, ErrorStringsWrapper.rsRdceInvalidExecutionOptionError, null, RSTrace.IsTraceInitialized ? RSTrace.CatalogTrace : null, null, Array.Empty<object>())
		{
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x000055AE File Offset: 0x000037AE
		private RdceInvalidExecutionOptionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
