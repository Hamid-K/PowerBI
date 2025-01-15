using System;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeDefinition;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition;
using Microsoft.DataShaping.Processing.Utils;

namespace Microsoft.DataShaping.Processing.Correlation
{
	// Token: 0x020000A5 RID: 165
	internal sealed class CorrelationGovernorFactory : ICorrelationGovernorFactory
	{
		// Token: 0x0600044E RID: 1102 RVA: 0x0000D784 File Offset: 0x0000B984
		internal CorrelationGovernorFactory(IDataComparer comparer, IKeyGenerator keyGenerator)
		{
			this._comparer = comparer;
			this._keyGenerator = keyGenerator;
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x0000D79A File Offset: 0x0000B99A
		public CorrelationGovernor CreateCorrelationGovernor(Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataShape dataShape, CellScopeToIntersectionRangeMapping cellScopeRangeMapping)
		{
			if (dataShape.CorrelationMode == CorrelationMode.ValueBased)
			{
				return new ValueBasedCorrelationGovernor(cellScopeRangeMapping, this._comparer, this._keyGenerator);
			}
			return new WritableIndexBasedCorrelationGovernor(cellScopeRangeMapping);
		}

		// Token: 0x04000236 RID: 566
		private readonly IDataComparer _comparer;

		// Token: 0x04000237 RID: 567
		private readonly IKeyGenerator _keyGenerator;
	}
}
