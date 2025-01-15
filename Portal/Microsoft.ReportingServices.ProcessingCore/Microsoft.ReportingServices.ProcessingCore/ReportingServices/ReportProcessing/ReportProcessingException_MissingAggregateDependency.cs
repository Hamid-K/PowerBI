using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020005E5 RID: 1509
	[Serializable]
	internal sealed class ReportProcessingException_MissingAggregateDependency : Exception
	{
		// Token: 0x0600540D RID: 21517 RVA: 0x00161990 File Offset: 0x0015FB90
		internal ReportProcessingException_MissingAggregateDependency()
		{
		}

		// Token: 0x0600540E RID: 21518 RVA: 0x00161998 File Offset: 0x0015FB98
		private ReportProcessingException_MissingAggregateDependency(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
