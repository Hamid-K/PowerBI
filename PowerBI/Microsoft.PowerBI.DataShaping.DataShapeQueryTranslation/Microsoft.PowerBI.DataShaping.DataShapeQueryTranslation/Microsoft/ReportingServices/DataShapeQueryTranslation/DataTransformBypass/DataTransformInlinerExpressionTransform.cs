using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataTransformBypass
{
	// Token: 0x020000BE RID: 190
	internal sealed class DataTransformInlinerExpressionTransform
	{
		// Token: 0x0600082A RID: 2090 RVA: 0x0001F7CF File Offset: 0x0001D9CF
		internal DataTransformInlinerExpressionTransform(ExpressionTable expressionTable)
		{
			this.m_expressionTable = expressionTable;
			this.m_columnTraversalCache = new Dictionary<DataTransformTableColumn, DataTransformInlinerExpressionTransform.ColumnTraversalCacheEntry>();
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x0001F7EC File Offset: 0x0001D9EC
		internal ExpressionNode TraverseToSource(ExpressionNode inputNode, out DataTransformTableColumn sourceColumn)
		{
			ResolvedDataTransformTableColumnReferenceExpressionNode resolvedDataTransformTableColumnReferenceExpressionNode = inputNode as ResolvedDataTransformTableColumnReferenceExpressionNode;
			if (resolvedDataTransformTableColumnReferenceExpressionNode != null)
			{
				return this.TraverseToSource(resolvedDataTransformTableColumnReferenceExpressionNode.Column, out sourceColumn);
			}
			sourceColumn = null;
			return inputNode;
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x0001F818 File Offset: 0x0001DA18
		private ExpressionNode TraverseToSource(DataTransformTableColumn column, out DataTransformTableColumn sourceColumn)
		{
			DataTransformInlinerExpressionTransform.ColumnTraversalCacheEntry columnTraversalCacheEntry;
			if (!this.m_columnTraversalCache.TryGetValue(column, out columnTraversalCacheEntry))
			{
				ExpressionNode node = this.m_expressionTable.GetNode(column.Value);
				ExpressionNode expressionNode = this.TraverseToSource(node, out sourceColumn);
				if (node == expressionNode)
				{
					columnTraversalCacheEntry = new DataTransformInlinerExpressionTransform.ColumnTraversalCacheEntry(column, node);
				}
				else
				{
					columnTraversalCacheEntry = new DataTransformInlinerExpressionTransform.ColumnTraversalCacheEntry(sourceColumn, expressionNode);
				}
				this.m_columnTraversalCache.Add(column, columnTraversalCacheEntry);
			}
			sourceColumn = columnTraversalCacheEntry.SourceColumn;
			return columnTraversalCacheEntry.ResultNode;
		}

		// Token: 0x04000409 RID: 1033
		private readonly ExpressionTable m_expressionTable;

		// Token: 0x0400040A RID: 1034
		private readonly Dictionary<DataTransformTableColumn, DataTransformInlinerExpressionTransform.ColumnTraversalCacheEntry> m_columnTraversalCache;

		// Token: 0x020002AB RID: 683
		private struct ColumnTraversalCacheEntry
		{
			// Token: 0x060015E3 RID: 5603 RVA: 0x00050AAC File Offset: 0x0004ECAC
			public ColumnTraversalCacheEntry(DataTransformTableColumn sourceColumn, ExpressionNode resultNode)
			{
				this.SourceColumn = sourceColumn;
				this.ResultNode = resultNode;
			}

			// Token: 0x04000A40 RID: 2624
			public DataTransformTableColumn SourceColumn;

			// Token: 0x04000A41 RID: 2625
			public ExpressionNode ResultNode;
		}
	}
}
