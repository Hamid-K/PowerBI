using System;
using System.Collections.Generic;
using System.ServiceModel;
using Microsoft.AnalysisServices.Azure.Common.DataContracts;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x0200005F RID: 95
	[MessageContract]
	public class PowerBIModelPopulateInfo
	{
		// Token: 0x0400015E RID: 350
		[MessageHeader]
		public DatabaseMoniker DatabaseMoniker;

		// Token: 0x0400015F RID: 351
		[MessageHeader]
		public IEnumerable<ASConnectionInfo> ConnectionInfos;

		// Token: 0x04000160 RID: 352
		[MessageHeader]
		public BindDatabaseResult BindDatabaseResult;

		// Token: 0x04000161 RID: 353
		[MessageHeader]
		public AdvancedModelFeatures AdvancedModelFeatures;
	}
}
