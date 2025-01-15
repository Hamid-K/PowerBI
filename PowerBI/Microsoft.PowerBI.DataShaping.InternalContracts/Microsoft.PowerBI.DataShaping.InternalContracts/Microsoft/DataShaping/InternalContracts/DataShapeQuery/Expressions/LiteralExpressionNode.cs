using System;
using Microsoft.DataShaping.Common;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions
{
	// Token: 0x020000D9 RID: 217
	internal sealed class LiteralExpressionNode : ExpressionNode
	{
		// Token: 0x06000609 RID: 1545 RVA: 0x0000CF1C File Offset: 0x0000B11C
		internal LiteralExpressionNode(ScalarValue value)
		{
			this.Value = ((value == null) ? ScalarValue.Null : value);
			this.ClrType = this.Value.Type;
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x0000CF5F File Offset: 0x0000B15F
		private LiteralExpressionNode(Type type)
		{
			this.Value = ScalarValue.Null;
			this.ClrType = type;
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x0600060B RID: 1547 RVA: 0x0000CF79 File Offset: 0x0000B179
		public override ExpressionNodeKind Kind
		{
			get
			{
				return ExpressionNodeKind.Literal;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x0600060C RID: 1548 RVA: 0x0000CF7D File Offset: 0x0000B17D
		public ScalarValue Value { get; }

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600060D RID: 1549 RVA: 0x0000CF85 File Offset: 0x0000B185
		internal Type ClrType { get; }

		// Token: 0x0600060E RID: 1550 RVA: 0x0000CF90 File Offset: 0x0000B190
		public override bool Equals(ExpressionNode other)
		{
			bool flag;
			LiteralExpressionNode literalExpressionNode;
			if (ExpressionNode.CheckReferenceAndTypeEquality<LiteralExpressionNode>(this, other, out flag, out literalExpressionNode))
			{
				return flag;
			}
			return this.Value == literalExpressionNode.Value && this.ClrType == literalExpressionNode.ClrType;
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x0000CFD2 File Offset: 0x0000B1D2
		protected override int GetHashCodeImpl()
		{
			return Hashing.CombineHash(Hashing.GetHashCode<ScalarValue>(this.Value, null), Hashing.GetHashCode<Type>(this.ClrType, null));
		}

		// Token: 0x040002AE RID: 686
		internal static readonly LiteralExpressionNode Zero = new LiteralExpressionNode(0);

		// Token: 0x040002AF RID: 687
		internal static readonly LiteralExpressionNode ZeroInt64 = new LiteralExpressionNode(0L);

		// Token: 0x040002B0 RID: 688
		internal static readonly LiteralExpressionNode One = new LiteralExpressionNode(1);

		// Token: 0x040002B1 RID: 689
		internal static readonly LiteralExpressionNode OneInt64 = new LiteralExpressionNode(1L);

		// Token: 0x040002B2 RID: 690
		internal static readonly LiteralExpressionNode NullUntyped = new LiteralExpressionNode(null);

		// Token: 0x040002B3 RID: 691
		internal static readonly LiteralExpressionNode NullInt64 = new LiteralExpressionNode(typeof(long));

		// Token: 0x040002B4 RID: 692
		internal static readonly LiteralExpressionNode False = new LiteralExpressionNode(false);

		// Token: 0x040002B5 RID: 693
		internal static readonly LiteralExpressionNode True = new LiteralExpressionNode(true);
	}
}
