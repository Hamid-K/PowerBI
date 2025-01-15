using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.InfoNav;

namespace Microsoft.ReportingServices.DataShapeQuery.Expressions
{
	// Token: 0x0200002E RID: 46
	internal sealed class ResolvedEntitySetExpressionNode : ExpressionNode
	{
		// Token: 0x0600023F RID: 575 RVA: 0x00007203 File Offset: 0x00005403
		internal ResolvedEntitySetExpressionNode(IConceptualEntity entity)
		{
			this.Entity = entity;
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000240 RID: 576 RVA: 0x00007212 File Offset: 0x00005412
		public override ExpressionNodeKind Kind
		{
			get
			{
				return ExpressionNodeKind.ResolvedEntitySet;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000241 RID: 577 RVA: 0x00007216 File Offset: 0x00005416
		public IConceptualEntity Entity { get; }

		// Token: 0x06000242 RID: 578 RVA: 0x00007220 File Offset: 0x00005420
		public override bool Equals(ExpressionNode other)
		{
			bool flag;
			ResolvedEntitySetExpressionNode resolvedEntitySetExpressionNode;
			if (ExpressionNode.CheckReferenceAndTypeEquality<ResolvedEntitySetExpressionNode>(this, other, out flag, out resolvedEntitySetExpressionNode))
			{
				return flag;
			}
			return object.Equals(this.Entity, resolvedEntitySetExpressionNode.Entity);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000724D File Offset: 0x0000544D
		protected override int GetHashCodeImpl()
		{
			return Hashing.GetHashCode<IConceptualEntity>(this.Entity, null);
		}
	}
}
