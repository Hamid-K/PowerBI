using System;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x020000AE RID: 174
	internal sealed class LimitPlotAxis
	{
		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x00007735 File Offset: 0x00005935
		// (set) Token: 0x0600041A RID: 1050 RVA: 0x0000773D File Offset: 0x0000593D
		public Expression Key { get; set; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x00007746 File Offset: 0x00005946
		// (set) Token: 0x0600041C RID: 1052 RVA: 0x0000774E File Offset: 0x0000594E
		public DataReductionPlotAxisTransform Transform { get; set; }
	}
}
