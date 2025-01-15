using System;
using Microsoft.DataShaping.Processing.Pipeline;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.DataShaping.Processing.DataShapeResultGeneration
{
	// Token: 0x0200007E RID: 126
	internal sealed class ResultTableMetadataManager : IDataPipelineRowMetadata
	{
		// Token: 0x06000334 RID: 820 RVA: 0x0000A67A File Offset: 0x0000887A
		internal ResultTableMetadataManager(ResultTableMetadata resultTableMetadata)
		{
			this._tableMetadata = resultTableMetadata;
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000A68C File Offset: 0x0000888C
		internal long GetRowIndex(IDataRow row, int resultSetIndex)
		{
			FieldValueExpressionNode rowIndexExpression = this._tableMetadata.GetRowIndexExpression(resultSetIndex);
			if (row == null || rowIndexExpression == null)
			{
				return -1L;
			}
			object @object = row.GetObject(rowIndexExpression.FieldIndex);
			if (@object == null)
			{
				return -1L;
			}
			return (long)@object;
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000A6C8 File Offset: 0x000088C8
		public bool CountsForLimiting(IDataRow row, int resultSetIndex)
		{
			return this.GetRowIndex(row, resultSetIndex) != -1L;
		}

		// Token: 0x040001D6 RID: 470
		private readonly ResultTableMetadata _tableMetadata;
	}
}
