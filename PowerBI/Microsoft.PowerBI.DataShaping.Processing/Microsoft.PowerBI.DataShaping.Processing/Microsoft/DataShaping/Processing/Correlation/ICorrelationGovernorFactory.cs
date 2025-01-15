using System;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition;

namespace Microsoft.DataShaping.Processing.Correlation
{
	// Token: 0x020000A8 RID: 168
	internal interface ICorrelationGovernorFactory
	{
		// Token: 0x0600045B RID: 1115
		CorrelationGovernor CreateCorrelationGovernor(DataShape dataShape, CellScopeToIntersectionRangeMapping cellScopeRangeMapping);
	}
}
