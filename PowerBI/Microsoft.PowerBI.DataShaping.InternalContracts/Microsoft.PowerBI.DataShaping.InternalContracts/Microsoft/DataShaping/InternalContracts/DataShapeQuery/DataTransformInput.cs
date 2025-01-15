using System;
using System.Collections.Generic;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x0200007C RID: 124
	internal sealed class DataTransformInput
	{
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000301 RID: 769 RVA: 0x000069E2 File Offset: 0x00004BE2
		// (set) Token: 0x06000302 RID: 770 RVA: 0x000069EA File Offset: 0x00004BEA
		public List<DataTransformParameter> Parameters { get; set; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000303 RID: 771 RVA: 0x000069F3 File Offset: 0x00004BF3
		// (set) Token: 0x06000304 RID: 772 RVA: 0x000069FB File Offset: 0x00004BFB
		public DataTransformTable Table { get; set; }
	}
}
