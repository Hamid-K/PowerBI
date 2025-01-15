using System;
using System.Runtime.Serialization;

namespace Microsoft.AnalysisServices.Azure.Common.DataContracts
{
	// Token: 0x02000158 RID: 344
	[DataContract]
	public sealed class ModelInfo
	{
		// Token: 0x060011FE RID: 4606 RVA: 0x000495D2 File Offset: 0x000477D2
		public ModelInfo(DatabaseMoniker databaseMoniker, long modelSizeInBytes)
		{
			this.DatabaseMoniker = databaseMoniker;
			this.ModelSizeInBytes = modelSizeInBytes;
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060011FF RID: 4607 RVA: 0x000495E8 File Offset: 0x000477E8
		// (set) Token: 0x06001200 RID: 4608 RVA: 0x000495F0 File Offset: 0x000477F0
		[DataMember]
		public DatabaseMoniker DatabaseMoniker { get; private set; }

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06001201 RID: 4609 RVA: 0x000495F9 File Offset: 0x000477F9
		// (set) Token: 0x06001202 RID: 4610 RVA: 0x00049601 File Offset: 0x00047801
		[DataMember]
		public long ModelSizeInBytes { get; private set; }
	}
}
