using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000045 RID: 69
	[DataContract]
	public sealed class DiscoverClusterDatasourceStatusResponse
	{
		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000200 RID: 512 RVA: 0x00003BA2 File Offset: 0x00001DA2
		// (set) Token: 0x06000201 RID: 513 RVA: 0x00003BAA File Offset: 0x00001DAA
		[DataMember(Name = "status", Order = 10)]
		public ClusterDatasourceStatus? Status { get; set; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000202 RID: 514 RVA: 0x00003BB3 File Offset: 0x00001DB3
		// (set) Token: 0x06000203 RID: 515 RVA: 0x00003BBB File Offset: 0x00001DBB
		[DataMember(Name = "errors", Order = 20)]
		public IEnumerable<DiscoverClusterDatasourceError> Errors { get; set; }
	}
}
