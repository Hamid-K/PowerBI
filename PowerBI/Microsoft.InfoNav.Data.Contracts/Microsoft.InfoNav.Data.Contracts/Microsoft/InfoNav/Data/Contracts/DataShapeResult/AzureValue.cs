using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.DataShapeResult
{
	// Token: 0x02000102 RID: 258
	[DataContract]
	public sealed class AzureValue
	{
		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x0000EB63 File Offset: 0x0000CD63
		// (set) Token: 0x060006D3 RID: 1747 RVA: 0x0000EB6B File Offset: 0x0000CD6B
		[DataMember(Name = "timestamp", IsRequired = false, EmitDefaultValue = false, Order = 0)]
		public string Timestamp { get; set; }

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060006D4 RID: 1748 RVA: 0x0000EB74 File Offset: 0x0000CD74
		// (set) Token: 0x060006D5 RID: 1749 RVA: 0x0000EB7C File Offset: 0x0000CD7C
		[DataMember(Name = "source", IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public string Source { get; set; }

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060006D6 RID: 1750 RVA: 0x0000EB85 File Offset: 0x0000CD85
		// (set) Token: 0x060006D7 RID: 1751 RVA: 0x0000EB8D File Offset: 0x0000CD8D
		[DataMember(Name = "details", IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public string Details { get; set; }

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060006D8 RID: 1752 RVA: 0x0000EB96 File Offset: 0x0000CD96
		// (set) Token: 0x060006D9 RID: 1753 RVA: 0x0000EB9E File Offset: 0x0000CD9E
		[DataMember(Name = "helpLink", IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public string HelpLink { get; set; }

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x0000EBA7 File Offset: 0x0000CDA7
		// (set) Token: 0x060006DB RID: 1755 RVA: 0x0000EBAF File Offset: 0x0000CDAF
		[DataMember(Name = "productInfo", IsRequired = false, EmitDefaultValue = false, Order = 40)]
		public ProductInfo ProductInfo { get; set; }

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060006DC RID: 1756 RVA: 0x0000EBB8 File Offset: 0x0000CDB8
		// (set) Token: 0x060006DD RID: 1757 RVA: 0x0000EBC0 File Offset: 0x0000CDC0
		[DataMember(Name = "additionalMessages", IsRequired = false, EmitDefaultValue = false, Order = 50)]
		public AdditionalMessage[] AdditionalMessages { get; set; }

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060006DE RID: 1758 RVA: 0x0000EBC9 File Offset: 0x0000CDC9
		// (set) Token: 0x060006DF RID: 1759 RVA: 0x0000EBD1 File Offset: 0x0000CDD1
		[DataMember(Name = "moreInformation", IsRequired = false, EmitDefaultValue = false, Order = 60)]
		public MoreInformation MoreInformation { get; set; }

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060006E0 RID: 1760 RVA: 0x0000EBDA File Offset: 0x0000CDDA
		// (set) Token: 0x060006E1 RID: 1761 RVA: 0x0000EBE2 File Offset: 0x0000CDE2
		[DataMember(Name = "powerBiErrorDetails", IsRequired = false, EmitDefaultValue = false, Order = 70)]
		public string PowerBiErrorDetails { get; set; }
	}
}
