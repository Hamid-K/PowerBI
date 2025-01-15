using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020005F6 RID: 1526
	[Serializable]
	internal sealed class DataSetPublishingException : ReportProcessingException
	{
		// Token: 0x06005444 RID: 21572 RVA: 0x00162171 File Offset: 0x00160371
		public DataSetPublishingException(ProcessingMessageList messages)
			: base(messages)
		{
		}
	}
}
