using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.DataShapeResult
{
	// Token: 0x02000107 RID: 263
	[DataContract]
	public sealed class DataMemberInstance
	{
		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060006FF RID: 1791 RVA: 0x0000EDBD File Offset: 0x0000CFBD
		// (set) Token: 0x06000700 RID: 1792 RVA: 0x0000EDC5 File Offset: 0x0000CFC5
		[DataMember(Name = "RestartFlag", IsRequired = false, EmitDefaultValue = false, Order = 0)]
		public RestartFlag RestartFlag { get; set; }

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000701 RID: 1793 RVA: 0x0000EDCE File Offset: 0x0000CFCE
		// (set) Token: 0x06000702 RID: 1794 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
		[DataMember(Name = "RestartKind", IsRequired = false, EmitDefaultValue = false, Order = 5)]
		public RestartKind RestartKind { get; set; }

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000703 RID: 1795 RVA: 0x0000EDDF File Offset: 0x0000CFDF
		// (set) Token: 0x06000704 RID: 1796 RVA: 0x0000EDE7 File Offset: 0x0000CFE7
		[DataMember(Name = "Calculations", IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public IList<Calculation> Calculations { get; set; }

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000705 RID: 1797 RVA: 0x0000EDF0 File Offset: 0x0000CFF0
		// (set) Token: 0x06000706 RID: 1798 RVA: 0x0000EDF8 File Offset: 0x0000CFF8
		[DataMember(Name = "DataShapes", IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public IList<DataShape> DataShapes { get; set; }

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000707 RID: 1799 RVA: 0x0000EE01 File Offset: 0x0000D001
		// (set) Token: 0x06000708 RID: 1800 RVA: 0x0000EE09 File Offset: 0x0000D009
		[DataMember(Name = "Members", IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public IList<DataMember> Members { get; set; }

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000709 RID: 1801 RVA: 0x0000EE12 File Offset: 0x0000D012
		// (set) Token: 0x0600070A RID: 1802 RVA: 0x0000EE1A File Offset: 0x0000D01A
		[DataMember(Name = "Intersections", IsRequired = false, EmitDefaultValue = false, Order = 40)]
		public IList<DataIntersection> Intersections { get; set; }

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x0600070B RID: 1803 RVA: 0x0000EE23 File Offset: 0x0000D023
		// (set) Token: 0x0600070C RID: 1804 RVA: 0x0000EE2B File Offset: 0x0000D02B
		[DataMember(Name = "Group", IsRequired = false, EmitDefaultValue = false, Order = 50)]
		public Group Group { get; set; }
	}
}
