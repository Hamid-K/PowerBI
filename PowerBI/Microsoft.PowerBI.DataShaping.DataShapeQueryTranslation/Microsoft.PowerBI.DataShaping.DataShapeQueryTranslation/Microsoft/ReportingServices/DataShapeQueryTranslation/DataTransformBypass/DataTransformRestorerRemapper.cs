using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataTransformBypass
{
	// Token: 0x020000C2 RID: 194
	internal sealed class DataTransformRestorerRemapper : DataShapeVisitor
	{
		// Token: 0x06000843 RID: 2115 RVA: 0x0001FBEB File Offset: 0x0001DDEB
		private DataTransformRestorerRemapper(ExpressionTable inputExpressionTable, IReadOnlyDictionary<DataTransformTableColumn, DataTransformColumnInliningInfo> sourceColumns, IExpressionTableLookup referrerExprTableLookup)
		{
			this.m_inputExpressionTable = inputExpressionTable;
			this.m_outputExpressionTable = new WritableExpressionTable();
			this.m_sourceColumns = sourceColumns;
			this.m_referrerExprTableLookup = referrerExprTableLookup;
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x0001FC13 File Offset: 0x0001DE13
		internal static WritableExpressionTable RemapTransforms(DataShape dataShape, ExpressionTable inputExpressionTable, IReadOnlyDictionary<DataTransformTableColumn, DataTransformColumnInliningInfo> sourceColumns, IExpressionTableLookup referrerExprTableLookup)
		{
			DataTransformRestorerRemapper dataTransformRestorerRemapper = new DataTransformRestorerRemapper(inputExpressionTable, sourceColumns, referrerExprTableLookup);
			dataTransformRestorerRemapper.Visit(dataShape);
			return dataTransformRestorerRemapper.m_outputExpressionTable;
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x0001FC29 File Offset: 0x0001DE29
		protected override void Visit(DataShape dataShape)
		{
			base.Visit<DataTransform>(dataShape.Transforms, new Action<DataTransform>(this.Visit));
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x0001FC44 File Offset: 0x0001DE44
		protected override void Visit(DataTransformParameter param)
		{
			this.CopyExpressionToOutput(param.Value);
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x0001FC54 File Offset: 0x0001DE54
		protected override void Visit(DataTransformTableColumn column)
		{
			DataTransformColumnInliningInfo dataTransformColumnInliningInfo;
			if (!this.m_sourceColumns.TryGetValue(column, out dataTransformColumnInliningInfo))
			{
				this.CopyExpressionToOutput(column.Value);
				return;
			}
			ExpressionReference expressionReference = dataTransformColumnInliningInfo.Referrers.First<ExpressionReference>();
			ExpressionNode expressionNode = this.m_referrerExprTableLookup.GetExpressionTable(expressionReference.Owner).GetNodeOrDefault(expressionReference.ExpressionId);
			if (expressionNode == null)
			{
				expressionNode = this.m_referrerExprTableLookup.GetFallbackExpressionTable().GetNode(expressionReference.ExpressionId);
			}
			this.m_outputExpressionTable.SetNode(column.Value, expressionNode);
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x0001FCD4 File Offset: 0x0001DED4
		private void CopyExpressionToOutput(Expression expression)
		{
			ExpressionNode node = this.m_inputExpressionTable.GetNode(expression);
			this.m_outputExpressionTable.SetNode(expression, node);
		}

		// Token: 0x04000411 RID: 1041
		private readonly ExpressionTable m_inputExpressionTable;

		// Token: 0x04000412 RID: 1042
		private readonly WritableExpressionTable m_outputExpressionTable;

		// Token: 0x04000413 RID: 1043
		private readonly IReadOnlyDictionary<DataTransformTableColumn, DataTransformColumnInliningInfo> m_sourceColumns;

		// Token: 0x04000414 RID: 1044
		private readonly IExpressionTableLookup m_referrerExprTableLookup;
	}
}
