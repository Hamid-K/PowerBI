using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQuery.Expressions
{
	// Token: 0x02000019 RID: 25
	internal sealed class ExpressionComparerByNode : IEqualityComparer<Expression>
	{
		// Token: 0x060000F1 RID: 241 RVA: 0x000045D6 File Offset: 0x000027D6
		internal ExpressionComparerByNode(ExpressionTable exprTable, IEqualityComparer<ExpressionNode> exprNodeComparer)
		{
			this.m_exprTable = exprTable;
			this.m_exprNodeComparer = exprNodeComparer;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000045EC File Offset: 0x000027EC
		public bool Equals(Expression x, Expression y)
		{
			bool? flag = CompareUtil.AreEqual<Expression, Expression>(x, y);
			if (flag != null)
			{
				return flag.Value;
			}
			ExpressionNode node = this.m_exprTable.GetNode(x);
			ExpressionNode node2 = this.m_exprTable.GetNode(y);
			return this.m_exprNodeComparer.Equals(node, node2);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x0000463C File Offset: 0x0000283C
		public int GetHashCode(Expression obj)
		{
			if (obj == null || obj.ExpressionId == null)
			{
				return -48879;
			}
			return this.m_exprTable.GetNode(obj).GetHashCode();
		}

		// Token: 0x0400004A RID: 74
		private readonly ExpressionTable m_exprTable;

		// Token: 0x0400004B RID: 75
		private readonly IEqualityComparer<ExpressionNode> m_exprNodeComparer;
	}
}
