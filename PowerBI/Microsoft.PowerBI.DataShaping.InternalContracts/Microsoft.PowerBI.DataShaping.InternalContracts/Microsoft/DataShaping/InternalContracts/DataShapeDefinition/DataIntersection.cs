using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.DataShaping.InternalContracts.DataShapeDefinition
{
	// Token: 0x0200010C RID: 268
	[DataContract]
	internal sealed class DataIntersection
	{
		// Token: 0x17000169 RID: 361
		// (get) Token: 0x0600072D RID: 1837 RVA: 0x0000F537 File Offset: 0x0000D737
		// (set) Token: 0x0600072E RID: 1838 RVA: 0x0000F53F File Offset: 0x0000D73F
		[DataMember(EmitDefaultValue = false, Order = 1)]
		public string Id { get; set; }

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600072F RID: 1839 RVA: 0x0000F548 File Offset: 0x0000D748
		// (set) Token: 0x06000730 RID: 1840 RVA: 0x0000F550 File Offset: 0x0000D750
		[DataMember(EmitDefaultValue = false, Order = 2)]
		internal IList<DataShape> DataShapes { get; set; }

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000731 RID: 1841 RVA: 0x0000F559 File Offset: 0x0000D759
		// (set) Token: 0x06000732 RID: 1842 RVA: 0x0000F561 File Offset: 0x0000D761
		[DataMember(EmitDefaultValue = false, Order = 3)]
		internal IList<Calculation> Calculations { get; set; }

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000733 RID: 1843 RVA: 0x0000F56A File Offset: 0x0000D76A
		// (set) Token: 0x06000734 RID: 1844 RVA: 0x0000F572 File Offset: 0x0000D772
		[DataMember(EmitDefaultValue = false, Order = 4)]
		internal DataBinding DataBinding { get; set; }
	}
}
