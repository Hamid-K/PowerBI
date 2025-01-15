using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x02000076 RID: 118
	[DebuggerDisplay("[DataShapeQuery]")]
	internal sealed class DataShapeQuery
	{
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x0000635D File Offset: 0x0000455D
		// (set) Token: 0x060002B8 RID: 696 RVA: 0x00006365 File Offset: 0x00004565
		public List<DataSource> DataSources { get; set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x0000636E File Offset: 0x0000456E
		// (set) Token: 0x060002BA RID: 698 RVA: 0x00006376 File Offset: 0x00004576
		public List<DataShape> DataShapes { get; set; }
	}
}
