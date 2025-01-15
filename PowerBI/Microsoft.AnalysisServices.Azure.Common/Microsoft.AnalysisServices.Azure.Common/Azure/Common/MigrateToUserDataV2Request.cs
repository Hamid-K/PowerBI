using System;
using System.Runtime.Serialization;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x0200005C RID: 92
	[DataContract]
	public class MigrateToUserDataV2Request : IFabricIntegratorProvisioningServiceRandomRoutable
	{
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x000100F9 File Offset: 0x0000E2F9
		// (set) Token: 0x06000483 RID: 1155 RVA: 0x00010101 File Offset: 0x0000E301
		[DataMember]
		public DatabaseEntity DatabaseEntity { get; set; }
	}
}
