using System;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.Data.Contracts.ResolvedDataShapeBindings
{
	// Token: 0x020000A3 RID: 163
	public sealed class ResolvedDataReductionPlotAxisBinding : ResolvedDataReductionLimit
	{
		// Token: 0x0600047D RID: 1149 RVA: 0x0000B5E8 File Offset: 0x000097E8
		public ResolvedDataReductionPlotAxisBinding(int index, DataReductionPlotAxisTransform transform)
		{
			this.Index = index;
			this.Transform = transform;
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x0000B5FE File Offset: 0x000097FE
		public int Index { get; }

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x0000B606 File Offset: 0x00009806
		public DataReductionPlotAxisTransform Transform { get; }
	}
}
