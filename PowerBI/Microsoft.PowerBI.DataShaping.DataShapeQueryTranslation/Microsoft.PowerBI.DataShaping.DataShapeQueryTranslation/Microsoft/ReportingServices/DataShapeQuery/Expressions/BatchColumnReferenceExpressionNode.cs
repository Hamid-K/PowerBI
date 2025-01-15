using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQuery.Expressions
{
	// Token: 0x02000014 RID: 20
	internal sealed class BatchColumnReferenceExpressionNode : ExpressionNode
	{
		// Token: 0x060000D5 RID: 213 RVA: 0x000043A2 File Offset: 0x000025A2
		internal BatchColumnReferenceExpressionNode(string columnName)
		{
			this.m_columnName = columnName;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x000043B1 File Offset: 0x000025B1
		public string ColumnName
		{
			get
			{
				return this.m_columnName;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x000043B9 File Offset: 0x000025B9
		public override ExpressionNodeKind Kind
		{
			get
			{
				return ExpressionNodeKind.BatchColumnReference;
			}
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000043BC File Offset: 0x000025BC
		public override bool Equals(ExpressionNode other)
		{
			bool flag;
			BatchColumnReferenceExpressionNode batchColumnReferenceExpressionNode;
			if (ExpressionNode.CheckReferenceAndTypeEquality<BatchColumnReferenceExpressionNode>(this, other, out flag, out batchColumnReferenceExpressionNode))
			{
				return flag;
			}
			return this.m_columnName.Equals(batchColumnReferenceExpressionNode.m_columnName);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000043E9 File Offset: 0x000025E9
		protected override int GetHashCodeImpl()
		{
			return this.m_columnName.GetHashCode();
		}

		// Token: 0x04000042 RID: 66
		private readonly string m_columnName;
	}
}
