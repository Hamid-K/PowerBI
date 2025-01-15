using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000AB RID: 171
	[Serializable]
	internal sealed class RdceInvalidRdlException : RSException
	{
		// Token: 0x0600029E RID: 670 RVA: 0x000054EF File Offset: 0x000036EF
		public RdceInvalidRdlException(Exception innerException)
			: base(ErrorCode.rsRdceInvalidRdlError, ErrorStringsWrapper.rsRdceInvalidRdlError, innerException, RSTrace.IsTraceInitialized ? RSTrace.CatalogTrace : null, null, Array.Empty<object>())
		{
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00005517 File Offset: 0x00003717
		private RdceInvalidRdlException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
