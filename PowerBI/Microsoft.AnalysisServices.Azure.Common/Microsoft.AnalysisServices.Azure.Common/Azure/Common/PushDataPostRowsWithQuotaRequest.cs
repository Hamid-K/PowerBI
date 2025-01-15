using System;
using System.IO;
using System.ServiceModel;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x020000A7 RID: 167
	[MessageContract]
	public class PushDataPostRowsWithQuotaRequest
	{
		// Token: 0x04000211 RID: 529
		[MessageHeader]
		public string virtualServer;

		// Token: 0x04000212 RID: 530
		[MessageHeader]
		public string tableName;

		// Token: 0x04000213 RID: 531
		[MessageHeader]
		public string datasetName;

		// Token: 0x04000214 RID: 532
		[MessageHeader]
		public string modelCreatorServicePlanId;

		// Token: 0x04000215 RID: 533
		[MessageHeader]
		public long modelSizeInMbs;

		// Token: 0x04000216 RID: 534
		[MessageHeader]
		public long modelId;

		// Token: 0x04000217 RID: 535
		[MessageBodyMember]
		public Stream dataStream;
	}
}
