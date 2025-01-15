using System;
using System.ServiceModel;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000062 RID: 98
	[MessageContract]
	public class ProvisionANDatabaseResult
	{
		// Token: 0x04000180 RID: 384
		[MessageHeader]
		public DatabaseMoniker DatabaseMoniker;

		// Token: 0x04000181 RID: 385
		[MessageHeader]
		public DatabaseType DatabaseType;

		// Token: 0x04000182 RID: 386
		[MessageHeader]
		public string ASAzureObjectId;

		// Token: 0x04000183 RID: 387
		[MessageHeader]
		public string StorageAccountName;

		// Token: 0x04000184 RID: 388
		[MessageHeader]
		public string ContainerId;
	}
}
