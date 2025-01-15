using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.DataShapeResult
{
	// Token: 0x02000105 RID: 261
	[DataContract]
	public sealed class DataIntersection
	{
		// Token: 0x1700021C RID: 540
		// (get) Token: 0x060006F1 RID: 1777 RVA: 0x0000ED47 File Offset: 0x0000CF47
		// (set) Token: 0x060006F2 RID: 1778 RVA: 0x0000ED4F File Offset: 0x0000CF4F
		[DataMember(Name = "Id", IsRequired = true, Order = 0)]
		public string Id { get; set; }

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x060006F3 RID: 1779 RVA: 0x0000ED58 File Offset: 0x0000CF58
		// (set) Token: 0x060006F4 RID: 1780 RVA: 0x0000ED60 File Offset: 0x0000CF60
		[DataMember(Name = "Index", IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public int? Index { get; set; }

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x060006F5 RID: 1781 RVA: 0x0000ED69 File Offset: 0x0000CF69
		// (set) Token: 0x060006F6 RID: 1782 RVA: 0x0000ED71 File Offset: 0x0000CF71
		[DataMember(Name = "Calculations", IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public IList<Calculation> Calculations { get; set; }

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x060006F7 RID: 1783 RVA: 0x0000ED7A File Offset: 0x0000CF7A
		// (set) Token: 0x060006F8 RID: 1784 RVA: 0x0000ED82 File Offset: 0x0000CF82
		[DataMember(Name = "DataShapes", IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public IList<DataShape> DataShapes { get; set; }
	}
}
