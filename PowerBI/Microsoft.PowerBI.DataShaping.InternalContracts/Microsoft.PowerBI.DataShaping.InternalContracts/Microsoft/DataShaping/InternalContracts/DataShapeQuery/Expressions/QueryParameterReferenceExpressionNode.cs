using System;
using Microsoft.InfoNav;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions
{
	// Token: 0x020000DB RID: 219
	internal sealed class QueryParameterReferenceExpressionNode : ExpressionNode
	{
		// Token: 0x06000618 RID: 1560 RVA: 0x0000D13A File Offset: 0x0000B33A
		internal QueryParameterReferenceExpressionNode(string name)
		{
			this.Name = name;
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000619 RID: 1561 RVA: 0x0000D149 File Offset: 0x0000B349
		public string Name { get; }

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600061A RID: 1562 RVA: 0x0000D151 File Offset: 0x0000B351
		public override ExpressionNodeKind Kind
		{
			get
			{
				return ExpressionNodeKind.QueryParameterReference;
			}
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0000D158 File Offset: 0x0000B358
		public override bool Equals(ExpressionNode other)
		{
			bool flag;
			QueryParameterReferenceExpressionNode queryParameterReferenceExpressionNode;
			if (ExpressionNode.CheckReferenceAndTypeEquality<QueryParameterReferenceExpressionNode>(this, other, out flag, out queryParameterReferenceExpressionNode))
			{
				return flag;
			}
			return ConceptualNameComparer.Instance.Equals(this.Name, queryParameterReferenceExpressionNode.Name);
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x0000D18A File Offset: 0x0000B38A
		protected override int GetHashCodeImpl()
		{
			return ConceptualNameComparer.Instance.GetHashCode(this.Name);
		}
	}
}
