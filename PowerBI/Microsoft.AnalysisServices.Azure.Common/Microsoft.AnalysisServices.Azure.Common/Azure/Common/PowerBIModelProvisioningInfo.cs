using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using Microsoft.PowerBI.ContentProviders;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000060 RID: 96
	[MessageContract]
	public class PowerBIModelProvisioningInfo
	{
		// Token: 0x04000162 RID: 354
		[MessageHeader]
		public DatabaseMoniker DatabaseMoniker;

		// Token: 0x04000163 RID: 355
		[MessageHeader]
		public string CreatedBy;

		// Token: 0x04000164 RID: 356
		[MessageHeader]
		public int InitialLoadInMB;

		// Token: 0x04000165 RID: 357
		[MessageHeader]
		public DatabaseSource DatabaseSource;

		// Token: 0x04000166 RID: 358
		[MessageHeader]
		public string XmlaSchema;

		// Token: 0x04000167 RID: 359
		[MessageHeader]
		public string TabularJsonSchema;

		// Token: 0x04000168 RID: 360
		[MessageBodyMember]
		public Stream AbfStream;

		// Token: 0x04000169 RID: 361
		[MessageHeader]
		public bool ShouldProcess;

		// Token: 0x0400016A RID: 362
		[MessageHeader]
		public string UserPuid;

		// Token: 0x0400016B RID: 363
		[MessageHeader]
		public string TenantId;

		// Token: 0x0400016C RID: 364
		[MessageHeader]
		public IEnumerable<DataSourceMapping> DataSources;

		// Token: 0x0400016D RID: 365
		[MessageHeader]
		public DatabaseType DatabaseType;

		// Token: 0x0400016E RID: 366
		[MessageHeader]
		public PublishBehavior PublishBehavior;

		// Token: 0x0400016F RID: 367
		[MessageHeader]
		public bool IsTabularModel;

		// Token: 0x04000170 RID: 368
		[MessageHeader]
		public string ASAzureObjectId;

		// Token: 0x04000171 RID: 369
		[MessageHeader]
		public bool SkipDatabaseBackupUpload;

		// Token: 0x04000172 RID: 370
		[MessageHeader]
		public Guid? TenantKeyObjectId;

		// Token: 0x04000173 RID: 371
		[MessageHeader]
		public Uri DatabaseTempPremiumFilesAbfUri;
	}
}
