using System;
using System.Collections.Generic;

namespace Model
{
	// Token: 0x0200004B RID: 75
	public sealed class DataSetSchema
	{
		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060001DB RID: 475 RVA: 0x0000308D File Offset: 0x0000128D
		// (set) Token: 0x060001DC RID: 476 RVA: 0x00003095 File Offset: 0x00001295
		public string Name { get; set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060001DD RID: 477 RVA: 0x0000309E File Offset: 0x0000129E
		// (set) Token: 0x060001DE RID: 478 RVA: 0x000030A6 File Offset: 0x000012A6
		public IEnumerable<DataSetField> Fields { get; set; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060001DF RID: 479 RVA: 0x000030AF File Offset: 0x000012AF
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x000030B7 File Offset: 0x000012B7
		public IEnumerable<DataSetParameterInfo> Parameters { get; set; }
	}
}
