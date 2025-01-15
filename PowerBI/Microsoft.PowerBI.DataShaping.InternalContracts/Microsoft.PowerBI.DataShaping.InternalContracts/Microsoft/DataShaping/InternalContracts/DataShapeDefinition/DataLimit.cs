using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.DataShaping.InternalContracts.DataShapeDefinition
{
	// Token: 0x0200010D RID: 269
	[DataContract]
	internal sealed class DataLimit
	{
		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000736 RID: 1846 RVA: 0x0000F583 File Offset: 0x0000D783
		// (set) Token: 0x06000737 RID: 1847 RVA: 0x0000F58B File Offset: 0x0000D78B
		[DataMember(EmitDefaultValue = false, Order = 1)]
		internal string Id { get; set; }

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000738 RID: 1848 RVA: 0x0000F594 File Offset: 0x0000D794
		// (set) Token: 0x06000739 RID: 1849 RVA: 0x0000F59C File Offset: 0x0000D79C
		[DataMember(EmitDefaultValue = false, Order = 2)]
		internal DataLimitOperator Operator { get; set; }

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x0600073A RID: 1850 RVA: 0x0000F5A5 File Offset: 0x0000D7A5
		// (set) Token: 0x0600073B RID: 1851 RVA: 0x0000F5AD File Offset: 0x0000D7AD
		[DataMember(EmitDefaultValue = false, Order = 3)]
		internal IList<string> Targets { get; set; }

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x0600073C RID: 1852 RVA: 0x0000F5B6 File Offset: 0x0000D7B6
		// (set) Token: 0x0600073D RID: 1853 RVA: 0x0000F5BE File Offset: 0x0000D7BE
		[DataMember(EmitDefaultValue = false, Order = 4)]
		internal string Within { get; set; }

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x0600073E RID: 1854 RVA: 0x0000F5C7 File Offset: 0x0000D7C7
		// (set) Token: 0x0600073F RID: 1855 RVA: 0x0000F5CF File Offset: 0x0000D7CF
		[DataMember(EmitDefaultValue = false, Order = 5)]
		internal IList<string> AppliesTo { get; set; }

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000740 RID: 1856 RVA: 0x0000F5D8 File Offset: 0x0000D7D8
		// (set) Token: 0x06000741 RID: 1857 RVA: 0x0000F5E0 File Offset: 0x0000D7E0
		[DataMember(EmitDefaultValue = false, Order = 6)]
		internal int? TelemetryId { get; set; }
	}
}
