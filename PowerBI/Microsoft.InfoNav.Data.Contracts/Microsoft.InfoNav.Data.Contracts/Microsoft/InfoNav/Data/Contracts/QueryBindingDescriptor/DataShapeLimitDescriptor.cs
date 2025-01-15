using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor
{
	// Token: 0x020000D2 RID: 210
	[DataContract]
	public sealed class DataShapeLimitDescriptor
	{
		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x0000C313 File Offset: 0x0000A513
		// (set) Token: 0x06000571 RID: 1393 RVA: 0x0000C31B File Offset: 0x0000A51B
		[DataMember(IsRequired = true, EmitDefaultValue = false, Order = 1)]
		public string Id { get; set; }

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x0000C324 File Offset: 0x0000A524
		// (set) Token: 0x06000573 RID: 1395 RVA: 0x0000C32C File Offset: 0x0000A52C
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 2)]
		public TopLimitDescriptor Top { get; set; }

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x0000C335 File Offset: 0x0000A535
		// (set) Token: 0x06000575 RID: 1397 RVA: 0x0000C33D File Offset: 0x0000A53D
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 3)]
		public BottomLimitDescriptor Bottom { get; set; }

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x0000C346 File Offset: 0x0000A546
		// (set) Token: 0x06000577 RID: 1399 RVA: 0x0000C34E File Offset: 0x0000A54E
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 4)]
		public SampleLimitDescriptor Sample { get; set; }

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x0000C357 File Offset: 0x0000A557
		// (set) Token: 0x06000579 RID: 1401 RVA: 0x0000C35F File Offset: 0x0000A55F
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 5)]
		public BinnedLineSampleLimitDescriptor BinnedLineSample { get; set; }

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x0000C368 File Offset: 0x0000A568
		// (set) Token: 0x0600057B RID: 1403 RVA: 0x0000C370 File Offset: 0x0000A570
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 6)]
		public OverlappingPointsSampleLimitDescriptor OverlappingPointsSample { get; set; }

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x0000C379 File Offset: 0x0000A579
		// (set) Token: 0x0600057D RID: 1405 RVA: 0x0000C381 File Offset: 0x0000A581
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 7)]
		public TopNPerLevelDescriptor TopNPerLevel { get; set; }

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x0600057E RID: 1406 RVA: 0x0000C38A File Offset: 0x0000A58A
		// (set) Token: 0x0600057F RID: 1407 RVA: 0x0000C392 File Offset: 0x0000A592
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 8)]
		public WindowLimitDescriptor Window { get; set; }

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000580 RID: 1408 RVA: 0x0000C39B File Offset: 0x0000A59B
		// (set) Token: 0x06000581 RID: 1409 RVA: 0x0000C3A3 File Offset: 0x0000A5A3
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public DataReductionScope Scope { get; set; }
	}
}
