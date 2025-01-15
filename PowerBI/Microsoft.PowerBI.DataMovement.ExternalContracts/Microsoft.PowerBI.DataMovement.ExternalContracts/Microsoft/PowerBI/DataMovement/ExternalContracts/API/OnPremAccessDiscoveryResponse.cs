using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000089 RID: 137
	[DataContract]
	public class OnPremAccessDiscoveryResponse
	{
		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x00004FDC File Offset: 0x000031DC
		// (set) Token: 0x06000412 RID: 1042 RVA: 0x00004FE4 File Offset: 0x000031E4
		[DataMember(Name = "dmtsDetails", Order = 0)]
		public DmtsDetails DmtsDetails { get; set; }

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x00004FED File Offset: 0x000031ED
		// (set) Token: 0x06000414 RID: 1044 RVA: 0x00004FF5 File Offset: 0x000031F5
		[DataMember(Name = "dataSourceConnectionDetails", Order = 10)]
		public DmtsDataSourceConnectionDetails DataSourceConnectionDetails { get; set; }

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000415 RID: 1045 RVA: 0x00004FFE File Offset: 0x000031FE
		// (set) Token: 0x06000416 RID: 1046 RVA: 0x00005006 File Offset: 0x00003206
		[DataMember(Name = "signatureAlgorithm", Order = 20)]
		public string SignatureAlgorithm { get; set; }

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x0000500F File Offset: 0x0000320F
		// (set) Token: 0x06000418 RID: 1048 RVA: 0x00005017 File Offset: 0x00003217
		[DataMember(Name = "signature", Order = 30)]
		public string Signature { get; set; }
	}
}
