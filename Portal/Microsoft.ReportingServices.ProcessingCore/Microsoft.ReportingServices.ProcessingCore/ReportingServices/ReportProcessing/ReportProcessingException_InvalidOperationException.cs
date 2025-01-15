using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020005E4 RID: 1508
	[Serializable]
	internal sealed class ReportProcessingException_InvalidOperationException : Exception
	{
		// Token: 0x0600540B RID: 21515 RVA: 0x0016197E File Offset: 0x0015FB7E
		internal ReportProcessingException_InvalidOperationException()
		{
		}

		// Token: 0x0600540C RID: 21516 RVA: 0x00161986 File Offset: 0x0015FB86
		private ReportProcessingException_InvalidOperationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
