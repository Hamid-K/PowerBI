using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions;

namespace Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition
{
	// Token: 0x0200004E RID: 78
	internal sealed class ResultTableMetadata
	{
		// Token: 0x06000208 RID: 520 RVA: 0x000062E8 File Offset: 0x000044E8
		internal ResultTableMetadata(IList<FieldValueExpressionNode> rowIndexExpressions)
		{
			this._rowIndexExpressions = rowIndexExpressions;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x000062F7 File Offset: 0x000044F7
		internal FieldValueExpressionNode GetRowIndexExpression(int resultTableIndex)
		{
			return this._rowIndexExpressions[resultTableIndex];
		}

		// Token: 0x0400013C RID: 316
		private readonly IList<FieldValueExpressionNode> _rowIndexExpressions;
	}
}
