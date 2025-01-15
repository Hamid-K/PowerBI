using System;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions;
using Microsoft.DataShaping.Processing.Utils;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.DataShaping.Processing.DataShapeResultGeneration
{
	// Token: 0x02000078 RID: 120
	internal sealed class ExpressionEvaluatorSingleRow : ExpressionEvaluatorBase
	{
		// Token: 0x06000315 RID: 789 RVA: 0x0000A255 File Offset: 0x00008455
		internal ExpressionEvaluatorSingleRow(IDataComparer comparer, IKeyGenerator keyGenerator)
			: base(comparer, keyGenerator)
		{
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000A25F File Offset: 0x0000845F
		internal void SetActiveRow(IDataRow row)
		{
			this._activeRow = row;
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000A268 File Offset: 0x00008468
		public override object Accept(FieldValueExpressionNode node)
		{
			if (this._activeRow != null)
			{
				return this._activeRow.GetObject(node.FieldIndex);
			}
			return null;
		}

		// Token: 0x040001C9 RID: 457
		private IDataRow _activeRow;
	}
}
