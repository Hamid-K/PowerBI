using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions
{
	// Token: 0x020000DC RID: 220
	internal sealed class StructureReferenceExpressionNode : ExpressionNode
	{
		// Token: 0x0600061D RID: 1565 RVA: 0x0000D19C File Offset: 0x0000B39C
		internal StructureReferenceExpressionNode(Identifier targetId)
		{
			this.TargetId = targetId;
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600061E RID: 1566 RVA: 0x0000D1AB File Offset: 0x0000B3AB
		public Identifier TargetId { get; }

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x0600061F RID: 1567 RVA: 0x0000D1B3 File Offset: 0x0000B3B3
		public override ExpressionNodeKind Kind
		{
			get
			{
				return ExpressionNodeKind.StructureReference;
			}
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x0000D1B8 File Offset: 0x0000B3B8
		public override bool Equals(ExpressionNode other)
		{
			bool flag;
			StructureReferenceExpressionNode structureReferenceExpressionNode;
			if (ExpressionNode.CheckReferenceAndTypeEquality<StructureReferenceExpressionNode>(this, other, out flag, out structureReferenceExpressionNode))
			{
				return flag;
			}
			return this.TargetId == structureReferenceExpressionNode.TargetId;
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x0000D1E5 File Offset: 0x0000B3E5
		protected override int GetHashCodeImpl()
		{
			return this.TargetId.GetHashCode();
		}
	}
}
