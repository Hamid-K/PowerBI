using System;
using System.IO;
using System.ServiceModel;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x020000A6 RID: 166
	[MessageContract]
	public class PushDataAppendRowsBatch
	{
		// Token: 0x0400020C RID: 524
		[MessageHeader]
		public string vs;

		// Token: 0x0400020D RID: 525
		[MessageHeader]
		public string tableName;

		// Token: 0x0400020E RID: 526
		[MessageHeader]
		public string datasetName;

		// Token: 0x0400020F RID: 527
		[MessageHeader]
		public long modelId;

		// Token: 0x04000210 RID: 528
		[MessageBodyMember]
		public Stream dataStream;
	}
}
