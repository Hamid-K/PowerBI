using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions
{
	// Token: 0x020000C6 RID: 198
	internal sealed class DaxTextExpressionNode : ExpressionNode
	{
		// Token: 0x0600051E RID: 1310 RVA: 0x0000AB4F File Offset: 0x00008D4F
		internal DaxTextExpressionNode(string daxText)
		{
			this.Text = daxText;
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x0000AB5E File Offset: 0x00008D5E
		public override ExpressionNodeKind Kind
		{
			get
			{
				return ExpressionNodeKind.DaxText;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x0000AB62 File Offset: 0x00008D62
		public string Text { get; }

		// Token: 0x06000521 RID: 1313 RVA: 0x0000AB6C File Offset: 0x00008D6C
		public override bool Equals(ExpressionNode other)
		{
			bool flag;
			DaxTextExpressionNode daxTextExpressionNode;
			if (ExpressionNode.CheckReferenceAndTypeEquality<DaxTextExpressionNode>(this, other, out flag, out daxTextExpressionNode))
			{
				return flag;
			}
			return string.Equals(this.Text, daxTextExpressionNode.Text, StringComparison.Ordinal);
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x0000AB9A File Offset: 0x00008D9A
		protected override int GetHashCodeImpl()
		{
			return StringComparer.Ordinal.GetHashCode(this.Text);
		}

		// Token: 0x04000233 RID: 563
		internal const string ExpressionNodeIdentifier = "Dax";
	}
}
