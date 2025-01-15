using System;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQuery.Expressions
{
	// Token: 0x0200002D RID: 45
	internal sealed class ResolvedDataTransformTableColumnReferenceExpressionNode : ResolvedStructureReferenceExpressionNode
	{
		// Token: 0x06000238 RID: 568 RVA: 0x00007177 File Offset: 0x00005377
		internal ResolvedDataTransformTableColumnReferenceExpressionNode(DataTransformTable table, DataTransformTableColumn column)
		{
			this.m_table = table;
			this.m_column = column;
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000239 RID: 569 RVA: 0x0000718D File Offset: 0x0000538D
		public override IIdentifiable Target
		{
			get
			{
				return this.m_column;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600023A RID: 570 RVA: 0x00007195 File Offset: 0x00005395
		public DataTransformTable Table
		{
			get
			{
				return this.m_table;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600023B RID: 571 RVA: 0x0000719D File Offset: 0x0000539D
		public DataTransformTableColumn Column
		{
			get
			{
				return this.m_column;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600023C RID: 572 RVA: 0x000071A5 File Offset: 0x000053A5
		public override ExpressionNodeKind Kind
		{
			get
			{
				return ExpressionNodeKind.ResolvedDataTransformTableColumnReference;
			}
		}

		// Token: 0x0600023D RID: 573 RVA: 0x000071AC File Offset: 0x000053AC
		public override bool Equals(ExpressionNode other)
		{
			bool flag;
			ResolvedDataTransformTableColumnReferenceExpressionNode resolvedDataTransformTableColumnReferenceExpressionNode;
			if (ExpressionNode.CheckReferenceAndTypeEquality<ResolvedDataTransformTableColumnReferenceExpressionNode>(this, other, out flag, out resolvedDataTransformTableColumnReferenceExpressionNode))
			{
				return flag;
			}
			return this.Table == resolvedDataTransformTableColumnReferenceExpressionNode.Table && this.Column == resolvedDataTransformTableColumnReferenceExpressionNode.Column;
		}

		// Token: 0x0600023E RID: 574 RVA: 0x000071E6 File Offset: 0x000053E6
		protected override int GetHashCodeImpl()
		{
			return Hashing.CombineHash(this.Table.GetHashCode(), this.Column.GetHashCode());
		}

		// Token: 0x0400009F RID: 159
		private readonly DataTransformTable m_table;

		// Token: 0x040000A0 RID: 160
		private readonly DataTransformTableColumn m_column;
	}
}
