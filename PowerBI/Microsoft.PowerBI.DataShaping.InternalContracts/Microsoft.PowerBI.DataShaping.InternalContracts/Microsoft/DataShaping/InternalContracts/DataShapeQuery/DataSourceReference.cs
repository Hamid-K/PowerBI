using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x0200007A RID: 122
	internal sealed class DataSourceReference
	{
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x00006968 File Offset: 0x00004B68
		// (set) Token: 0x060002F3 RID: 755 RVA: 0x00006970 File Offset: 0x00004B70
		public string DataSourceName { get; set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x00006979 File Offset: 0x00004B79
		// (set) Token: 0x060002F5 RID: 757 RVA: 0x00006981 File Offset: 0x00004B81
		public string ItemPath { get; set; }
	}
}
