using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQuery.Expressions
{
	// Token: 0x02000029 RID: 41
	internal sealed class PlaceholderExpressionNode : ExpressionNode
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000222 RID: 546 RVA: 0x00006FCA File Offset: 0x000051CA
		public override ExpressionNodeKind Kind
		{
			get
			{
				return ExpressionNodeKind.Placeholder;
			}
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00006FD0 File Offset: 0x000051D0
		public override bool Equals(ExpressionNode other)
		{
			bool flag;
			PlaceholderExpressionNode placeholderExpressionNode;
			return !ExpressionNode.CheckReferenceAndTypeEquality<PlaceholderExpressionNode>(this, other, out flag, out placeholderExpressionNode) || flag;
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00006FED File Offset: 0x000051ED
		protected override int GetHashCodeImpl()
		{
			return (int)this.Kind;
		}
	}
}
