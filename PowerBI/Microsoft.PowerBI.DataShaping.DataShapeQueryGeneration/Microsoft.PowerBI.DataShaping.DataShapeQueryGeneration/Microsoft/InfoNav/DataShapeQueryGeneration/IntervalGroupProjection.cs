using System;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000097 RID: 151
	internal sealed class IntervalGroupProjection : ICalculatedGroupProjection
	{
		// Token: 0x060005AB RID: 1451 RVA: 0x0001510F File Offset: 0x0001330F
		internal IntervalGroupProjection(IConceptualColumn minColumn, IConceptualColumn maxColumn)
		{
			this.MinColumn = minColumn;
			this.MaxColumn = maxColumn;
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x00015125 File Offset: 0x00013325
		internal IConceptualColumn MinColumn { get; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x0001512D File Offset: 0x0001332D
		internal IConceptualColumn MaxColumn { get; }
	}
}
