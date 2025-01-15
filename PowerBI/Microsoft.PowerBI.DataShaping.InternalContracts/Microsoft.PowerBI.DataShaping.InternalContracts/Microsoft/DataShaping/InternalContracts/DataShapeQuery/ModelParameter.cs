using System;
using System.Collections.Generic;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x02000075 RID: 117
	internal sealed class ModelParameter
	{
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x00006322 File Offset: 0x00004522
		// (set) Token: 0x060002B1 RID: 689 RVA: 0x0000632A File Offset: 0x0000452A
		public string Name { get; set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x00006333 File Offset: 0x00004533
		// (set) Token: 0x060002B3 RID: 691 RVA: 0x0000633B File Offset: 0x0000453B
		public List<Expression> Values { get; set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x00006344 File Offset: 0x00004544
		// (set) Token: 0x060002B5 RID: 693 RVA: 0x0000634C File Offset: 0x0000454C
		public bool IsListType { get; set; }
	}
}
