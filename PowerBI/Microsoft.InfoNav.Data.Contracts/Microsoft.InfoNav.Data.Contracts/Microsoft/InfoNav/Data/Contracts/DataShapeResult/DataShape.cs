using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.DataShapeResult
{
	// Token: 0x0200010A RID: 266
	[DataContract]
	public sealed class DataShape
	{
		// Token: 0x17000229 RID: 553
		// (get) Token: 0x0600070E RID: 1806 RVA: 0x0000EE3C File Offset: 0x0000D03C
		// (set) Token: 0x0600070F RID: 1807 RVA: 0x0000EE44 File Offset: 0x0000D044
		[DataMember(Name = "Id", IsRequired = false, EmitDefaultValue = false, Order = 0)]
		public string Id { get; set; }

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000710 RID: 1808 RVA: 0x0000EE4D File Offset: 0x0000D04D
		// (set) Token: 0x06000711 RID: 1809 RVA: 0x0000EE55 File Offset: 0x0000D055
		[DataMember(Name = "DataShapes", IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public IList<DataShape> DataShapes { get; set; }

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000712 RID: 1810 RVA: 0x0000EE5E File Offset: 0x0000D05E
		// (set) Token: 0x06000713 RID: 1811 RVA: 0x0000EE66 File Offset: 0x0000D066
		[DataMember(Name = "Calculations", IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public IList<Calculation> Calculations { get; set; }

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000714 RID: 1812 RVA: 0x0000EE6F File Offset: 0x0000D06F
		// (set) Token: 0x06000715 RID: 1813 RVA: 0x0000EE77 File Offset: 0x0000D077
		[DataMember(Name = "SecondaryHierarchy", IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public IList<DataMember> SecondaryHierarchy { get; set; }

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000716 RID: 1814 RVA: 0x0000EE80 File Offset: 0x0000D080
		// (set) Token: 0x06000717 RID: 1815 RVA: 0x0000EE88 File Offset: 0x0000D088
		[DataMember(Name = "PrimaryHierarchy", IsRequired = false, EmitDefaultValue = false, Order = 40)]
		public IList<DataMember> PrimaryHierarchy { get; set; }

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000718 RID: 1816 RVA: 0x0000EE91 File Offset: 0x0000D091
		// (set) Token: 0x06000719 RID: 1817 RVA: 0x0000EE99 File Offset: 0x0000D099
		[DataMember(Name = "DataLimitsExceeded", IsRequired = false, EmitDefaultValue = false, Order = 50)]
		public IList<Limit> DataLimitsExceeded { get; set; }

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x0600071A RID: 1818 RVA: 0x0000EEA2 File Offset: 0x0000D0A2
		// (set) Token: 0x0600071B RID: 1819 RVA: 0x0000EEAA File Offset: 0x0000D0AA
		[DataMember(Name = "IsComplete", IsRequired = false, EmitDefaultValue = false, Order = 60)]
		public bool? IsComplete { get; set; }

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x0600071C RID: 1820 RVA: 0x0000EEB3 File Offset: 0x0000D0B3
		// (set) Token: 0x0600071D RID: 1821 RVA: 0x0000EEBB File Offset: 0x0000D0BB
		[DataMember(Name = "RestartTokens", IsRequired = false, EmitDefaultValue = false, Order = 70)]
		public IList<RestartToken> RestartTokens { get; set; }

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x0600071E RID: 1822 RVA: 0x0000EEC4 File Offset: 0x0000D0C4
		// (set) Token: 0x0600071F RID: 1823 RVA: 0x0000EECC File Offset: 0x0000D0CC
		[DataMember(Name = "HasAllData", IsRequired = false, EmitDefaultValue = false, Order = 75)]
		public bool? HasAllData { get; set; }

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000720 RID: 1824 RVA: 0x0000EED5 File Offset: 0x0000D0D5
		// (set) Token: 0x06000721 RID: 1825 RVA: 0x0000EEDD File Offset: 0x0000D0DD
		[DataMember(Name = "DataShapeMessages", IsRequired = false, EmitDefaultValue = false, Order = 80)]
		public IList<DataShapeMessage> DataShapeMessages { get; set; }

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000722 RID: 1826 RVA: 0x0000EEE6 File Offset: 0x0000D0E6
		// (set) Token: 0x06000723 RID: 1827 RVA: 0x0000EEEE File Offset: 0x0000D0EE
		[DataMember(Name = "odata.error", IsRequired = false, EmitDefaultValue = false, Order = 90)]
		public ODataError Error { get; set; }

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000724 RID: 1828 RVA: 0x0000EEF7 File Offset: 0x0000D0F7
		// (set) Token: 0x06000725 RID: 1829 RVA: 0x0000EEFF File Offset: 0x0000D0FF
		[DataMember(Name = "DataWindows", IsRequired = false, EmitDefaultValue = false, Order = 100)]
		public IList<DataWindow> DataWindows { get; set; }
	}
}
