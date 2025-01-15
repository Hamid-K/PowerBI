using System;
using Microsoft.DataShaping.Processing.Pipeline;

namespace Microsoft.DataShaping.Processing.Correlation
{
	// Token: 0x020000AE RID: 174
	internal sealed class WritableIndexBasedCorrelationGovernor : IndexBasedCorrelationGovernor
	{
		// Token: 0x0600047E RID: 1150 RVA: 0x0000DD76 File Offset: 0x0000BF76
		internal WritableIndexBasedCorrelationGovernor(CellScopeToIntersectionRangeMapping cellScopeToIntersectionRangeMapping)
			: base(cellScopeToIntersectionRangeMapping)
		{
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0000DD7F File Offset: 0x0000BF7F
		internal override void SetCorrelationInfo(int correlationIndex, IReadOnlyRowCache rowCache)
		{
			this._correlationFieldIndex = correlationIndex;
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x0000DD88 File Offset: 0x0000BF88
		internal override CorrelationGovernor ToReadOnly()
		{
			return new ReadOnlyIndexBasedCorrelationGovernor(this);
		}
	}
}
