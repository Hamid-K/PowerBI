using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor
{
	// Token: 0x020000C9 RID: 201
	[DataContract]
	public sealed class BinnedLineSampleLimitDescriptor
	{
		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000529 RID: 1321 RVA: 0x0000BFD2 File Offset: 0x0000A1D2
		// (set) Token: 0x0600052A RID: 1322 RVA: 0x0000BFDA File Offset: 0x0000A1DA
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 1)]
		public int MaxTargetPointCount { get; set; }

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x0000BFE3 File Offset: 0x0000A1E3
		// (set) Token: 0x0600052C RID: 1324 RVA: 0x0000BFEB File Offset: 0x0000A1EB
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 2)]
		public int MinPointsPerSeriesCount { get; set; }

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x0000BFF4 File Offset: 0x0000A1F4
		// (set) Token: 0x0600052E RID: 1326 RVA: 0x0000BFFC File Offset: 0x0000A1FC
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 3)]
		public string IntersectionDbCountCalc { get; set; }

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x0000C005 File Offset: 0x0000A205
		// (set) Token: 0x06000530 RID: 1328 RVA: 0x0000C00D File Offset: 0x0000A20D
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 4)]
		public string PrimaryDbCountCalc { get; set; }

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x0000C016 File Offset: 0x0000A216
		// (set) Token: 0x06000532 RID: 1330 RVA: 0x0000C01E File Offset: 0x0000A21E
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 5)]
		public string SecondaryDbCountCalc { get; set; }

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x0000C027 File Offset: 0x0000A227
		// (set) Token: 0x06000534 RID: 1332 RVA: 0x0000C02F File Offset: 0x0000A22F
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 6)]
		public int? WarningCount { get; set; }
	}
}
