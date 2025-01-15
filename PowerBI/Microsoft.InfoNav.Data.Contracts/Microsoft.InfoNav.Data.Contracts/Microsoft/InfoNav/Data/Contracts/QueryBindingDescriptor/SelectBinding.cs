using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor
{
	// Token: 0x020000E0 RID: 224
	[DataContract]
	public sealed class SelectBinding : IProjectionBinding
	{
		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060005DE RID: 1502 RVA: 0x0000C62C File Offset: 0x0000A82C
		// (set) Token: 0x060005DF RID: 1503 RVA: 0x0000C634 File Offset: 0x0000A834
		[DataMember(IsRequired = true, EmitDefaultValue = true, Order = 10)]
		public SelectKind Kind { get; set; }

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x0000C63D File Offset: 0x0000A83D
		// (set) Token: 0x060005E1 RID: 1505 RVA: 0x0000C645 File Offset: 0x0000A845
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public int? Depth { get; set; }

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060005E2 RID: 1506 RVA: 0x0000C64E File Offset: 0x0000A84E
		// (set) Token: 0x060005E3 RID: 1507 RVA: 0x0000C656 File Offset: 0x0000A856
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 25)]
		public int? SecondaryDepth { get; set; }

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060005E4 RID: 1508 RVA: 0x0000C65F File Offset: 0x0000A85F
		// (set) Token: 0x060005E5 RID: 1509 RVA: 0x0000C667 File Offset: 0x0000A867
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public string Value { get; set; }

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x0000C670 File Offset: 0x0000A870
		// (set) Token: 0x060005E7 RID: 1511 RVA: 0x0000C678 File Offset: 0x0000A878
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 40)]
		public string Format { get; set; }

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x0000C681 File Offset: 0x0000A881
		// (set) Token: 0x060005E9 RID: 1513 RVA: 0x0000C689 File Offset: 0x0000A889
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 50)]
		public List<string> Subtotal { get; set; }

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060005EA RID: 1514 RVA: 0x0000C692 File Offset: 0x0000A892
		// (set) Token: 0x060005EB RID: 1515 RVA: 0x0000C69A File Offset: 0x0000A89A
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 60)]
		public List<string> Min { get; set; }

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060005EC RID: 1516 RVA: 0x0000C6A3 File Offset: 0x0000A8A3
		// (set) Token: 0x060005ED RID: 1517 RVA: 0x0000C6AB File Offset: 0x0000A8AB
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 70)]
		public List<string> Max { get; set; }

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060005EE RID: 1518 RVA: 0x0000C6B4 File Offset: 0x0000A8B4
		// (set) Token: 0x060005EF RID: 1519 RVA: 0x0000C6BC File Offset: 0x0000A8BC
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 80)]
		public List<string> Count { get; set; }

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x0000C6C5 File Offset: 0x0000A8C5
		// (set) Token: 0x060005F1 RID: 1521 RVA: 0x0000C6CD File Offset: 0x0000A8CD
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 90)]
		public AuxiliarySelectBinding Highlight { get; set; }

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x0000C6D6 File Offset: 0x0000A8D6
		// (set) Token: 0x060005F3 RID: 1523 RVA: 0x0000C6DE File Offset: 0x0000A8DE
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 100)]
		public DynamicFormatBinding DynamicFormat { get; set; }

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060005F4 RID: 1524 RVA: 0x0000C6E7 File Offset: 0x0000A8E7
		// (set) Token: 0x060005F5 RID: 1525 RVA: 0x0000C6EF File Offset: 0x0000A8EF
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 110)]
		public List<DynamicFormatBinding> SubtotalDynamicFormat { get; set; }

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060005F6 RID: 1526 RVA: 0x0000C6F8 File Offset: 0x0000A8F8
		// (set) Token: 0x060005F7 RID: 1527 RVA: 0x0000C700 File Offset: 0x0000A900
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 120)]
		public List<AggregateDescriptor> Aggregates { get; set; }

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060005F8 RID: 1528 RVA: 0x0000C709 File Offset: 0x0000A909
		// (set) Token: 0x060005F9 RID: 1529 RVA: 0x0000C711 File Offset: 0x0000A911
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 130)]
		public List<SelectIdentityKey> GroupKeys { get; set; }

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060005FA RID: 1530 RVA: 0x0000C71A File Offset: 0x0000A91A
		// (set) Token: 0x060005FB RID: 1531 RVA: 0x0000C722 File Offset: 0x0000A922
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 150)]
		public string Name { get; set; }

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060005FC RID: 1532 RVA: 0x0000C72B File Offset: 0x0000A92B
		// (set) Token: 0x060005FD RID: 1533 RVA: 0x0000C733 File Offset: 0x0000A933
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 160)]
		public IntervalValue Interval { get; set; }

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x060005FE RID: 1534 RVA: 0x0000C73C File Offset: 0x0000A93C
		// (set) Token: 0x060005FF RID: 1535 RVA: 0x0000C744 File Offset: 0x0000A944
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 170)]
		public AuxiliarySelectBinding Synchronized { get; set; }
	}
}
