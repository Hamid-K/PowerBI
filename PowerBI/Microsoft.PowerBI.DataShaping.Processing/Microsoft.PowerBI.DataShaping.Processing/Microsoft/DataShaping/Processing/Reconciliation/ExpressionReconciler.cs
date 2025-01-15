using System;
using System.Collections.ObjectModel;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions;

namespace Microsoft.DataShaping.Processing.Reconciliation
{
	// Token: 0x0200001E RID: 30
	internal sealed class ExpressionReconciler : Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.IExpressionNodeVisitor<Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions.ExpressionNode>
	{
		// Token: 0x060000FA RID: 250 RVA: 0x00004868 File Offset: 0x00002A68
		internal ExpressionReconciler(ResultTableLookup dataSetTable)
		{
			this._dataSetTable = dataSetTable;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00004877 File Offset: 0x00002A77
		public Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions.ExpressionNode Visit(Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.ExpressionNode node)
		{
			return node.Accept<Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions.ExpressionNode>(this);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00004880 File Offset: 0x00002A80
		public Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions.ExpressionNode Visit(Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.FieldValueExpressionNode node)
		{
			int resultTableIndex = this._dataSetTable.GetResultTableIndex(node.TableId);
			int fieldIndex = this._dataSetTable.GetFieldIndex(node.FieldId, resultTableIndex);
			return new Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions.FieldValueExpressionNode(node.FieldId, fieldIndex, node.TableId, resultTableIndex);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000048C8 File Offset: 0x00002AC8
		public Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions.ExpressionNode Visit(Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.FunctionCallExpressionNode node)
		{
			Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions.FunctionKind kind = (Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions.FunctionKind)node.Kind;
			ReadOnlyCollection<Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions.ExpressionNode> readOnlyCollection = node.Arguments.Reconcile(new Func<Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.ExpressionNode, Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions.ExpressionNode>(this.Visit));
			return new Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions.FunctionCallExpressionNode(kind, readOnlyCollection);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x000048F9 File Offset: 0x00002AF9
		public Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions.ExpressionNode Visit(Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.LiteralExpressionNode node)
		{
			return new Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions.LiteralExpressionNode(new ScalarValue(node.Value));
		}

		// Token: 0x04000099 RID: 153
		private readonly ResultTableLookup _dataSetTable;
	}
}
