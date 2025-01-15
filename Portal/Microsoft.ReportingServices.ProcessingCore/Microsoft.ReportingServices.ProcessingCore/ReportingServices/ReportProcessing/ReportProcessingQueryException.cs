using System;
using System.Runtime.Serialization;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020005F4 RID: 1524
	[Serializable]
	internal sealed class ReportProcessingQueryException : ReportProcessingException
	{
		// Token: 0x0600543F RID: 21567 RVA: 0x0016210E File Offset: 0x0016030E
		public ReportProcessingQueryException(ErrorCode errorCode, Exception innerException, params object[] arguments)
			: base(errorCode, innerException, arguments)
		{
		}

		// Token: 0x06005440 RID: 21568 RVA: 0x00162119 File Offset: 0x00160319
		private ReportProcessingQueryException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
