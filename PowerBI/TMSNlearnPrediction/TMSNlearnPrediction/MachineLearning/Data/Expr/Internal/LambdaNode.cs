using System;
using Microsoft.MachineLearning.Internal.Lexer;

namespace Microsoft.MachineLearning.Data.Expr.Internal
{
	// Token: 0x020001A7 RID: 423
	internal sealed class LambdaNode : Node
	{
		// Token: 0x0600094A RID: 2378 RVA: 0x000328FC File Offset: 0x00030AFC
		public LambdaNode(Token tok, ParamNode[] vars, ExprNode expr)
			: base(tok)
		{
			this.Vars = vars;
			this.Expr = expr;
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600094B RID: 2379 RVA: 0x00032913 File Offset: 0x00030B13
		public override NodeKind Kind
		{
			get
			{
				return NodeKind.Lambda;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600094C RID: 2380 RVA: 0x00032916 File Offset: 0x00030B16
		public override LambdaNode AsPredicate
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600094D RID: 2381 RVA: 0x00032919 File Offset: 0x00030B19
		public override LambdaNode TestPredicate
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x0003291C File Offset: 0x00030B1C
		public override void Accept(NodeVisitor visitor)
		{
			if (visitor.PreVisit(this))
			{
				foreach (ParamNode paramNode in this.Vars)
				{
					paramNode.Accept(visitor);
				}
				this.Expr.Accept(visitor);
				visitor.PostVisit(this);
			}
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x00032968 File Offset: 0x00030B68
		public ParamNode FindParam(string name)
		{
			foreach (ParamNode paramNode in this.Vars)
			{
				if (paramNode.Name == name)
				{
					return paramNode;
				}
			}
			return null;
		}

		// Token: 0x040004DA RID: 1242
		public readonly ParamNode[] Vars;

		// Token: 0x040004DB RID: 1243
		public readonly ExprNode Expr;

		// Token: 0x040004DC RID: 1244
		public ColumnType ResultType;
	}
}
