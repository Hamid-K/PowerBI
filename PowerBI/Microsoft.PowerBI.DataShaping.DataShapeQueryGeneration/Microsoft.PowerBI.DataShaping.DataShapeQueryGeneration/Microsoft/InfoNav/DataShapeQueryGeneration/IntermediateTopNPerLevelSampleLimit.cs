using System;
using Microsoft.InfoNav.Data.Contracts.ResolvedDataShapeBindings;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000033 RID: 51
	internal sealed class IntermediateTopNPerLevelSampleLimit : IntermediateReductionAlgorithm
	{
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001ED RID: 493 RVA: 0x00009943 File Offset: 0x00007B43
		// (set) Token: 0x060001EE RID: 494 RVA: 0x0000994B File Offset: 0x00007B4B
		internal ResolvedDataReductionWindowExpansionState WindowExpansionState { get; set; }

		// Token: 0x060001EF RID: 495 RVA: 0x00009954 File Offset: 0x00007B54
		internal override bool IsFullySpecified()
		{
			return base.Count != null;
		}
	}
}
