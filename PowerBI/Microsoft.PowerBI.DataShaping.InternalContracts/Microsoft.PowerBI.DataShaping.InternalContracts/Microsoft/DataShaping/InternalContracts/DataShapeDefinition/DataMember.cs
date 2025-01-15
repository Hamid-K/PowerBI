using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.DataShaping.InternalContracts.DataShapeDefinition
{
	// Token: 0x02000117 RID: 279
	[DataContract]
	internal sealed class DataMember
	{
		// Token: 0x1700017B RID: 379
		// (get) Token: 0x0600075B RID: 1883 RVA: 0x0000F6B9 File Offset: 0x0000D8B9
		// (set) Token: 0x0600075C RID: 1884 RVA: 0x0000F6C1 File Offset: 0x0000D8C1
		[DataMember(EmitDefaultValue = false, Order = 1)]
		internal string Id { get; set; }

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x0600075D RID: 1885 RVA: 0x0000F6CA File Offset: 0x0000D8CA
		// (set) Token: 0x0600075E RID: 1886 RVA: 0x0000F6D2 File Offset: 0x0000D8D2
		[DataMember(EmitDefaultValue = false, Order = 2)]
		internal IList<DataMember> DataMembers { get; set; }

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600075F RID: 1887 RVA: 0x0000F6DB File Offset: 0x0000D8DB
		// (set) Token: 0x06000760 RID: 1888 RVA: 0x0000F6E3 File Offset: 0x0000D8E3
		[DataMember(EmitDefaultValue = false, Order = 3)]
		internal IList<Calculation> Calculations { get; set; }

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000761 RID: 1889 RVA: 0x0000F6EC File Offset: 0x0000D8EC
		// (set) Token: 0x06000762 RID: 1890 RVA: 0x0000F6F4 File Offset: 0x0000D8F4
		[DataMember(EmitDefaultValue = false, Order = 4)]
		internal Group Group { get; set; }

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000763 RID: 1891 RVA: 0x0000F6FD File Offset: 0x0000D8FD
		// (set) Token: 0x06000764 RID: 1892 RVA: 0x0000F705 File Offset: 0x0000D905
		[DataMember(EmitDefaultValue = false, Order = 5)]
		internal IList<DataIntersection> Intersections { get; set; }

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000765 RID: 1893 RVA: 0x0000F70E File Offset: 0x0000D90E
		// (set) Token: 0x06000766 RID: 1894 RVA: 0x0000F716 File Offset: 0x0000D916
		[DataMember(EmitDefaultValue = false, Order = 6)]
		internal DataBinding DataBinding { get; set; }

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000767 RID: 1895 RVA: 0x0000F71F File Offset: 0x0000D91F
		// (set) Token: 0x06000768 RID: 1896 RVA: 0x0000F727 File Offset: 0x0000D927
		[DataMember(EmitDefaultValue = false, Order = 7)]
		internal MatchCondition MatchCondition { get; set; }

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000769 RID: 1897 RVA: 0x0000F730 File Offset: 0x0000D930
		// (set) Token: 0x0600076A RID: 1898 RVA: 0x0000F738 File Offset: 0x0000D938
		[DataMember(EmitDefaultValue = false, Order = 8)]
		internal StartPosition StartPosition { get; set; }

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x0600076B RID: 1899 RVA: 0x0000F741 File Offset: 0x0000D941
		// (set) Token: 0x0600076C RID: 1900 RVA: 0x0000F749 File Offset: 0x0000D949
		[DataMember(EmitDefaultValue = false, Order = 9)]
		internal RestartKindDefinition RestartKindDefinition { get; set; }

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x0600076D RID: 1901 RVA: 0x0000F752 File Offset: 0x0000D952
		// (set) Token: 0x0600076E RID: 1902 RVA: 0x0000F75A File Offset: 0x0000D95A
		[DataMember(EmitDefaultValue = false, Order = 10)]
		internal DiscardCondition DiscardCondition { get; set; }
	}
}
