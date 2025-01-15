using System;
using System.Reflection;
using Microsoft.MachineLearning.Internal.Lexer;

namespace Microsoft.MachineLearning.Data.Expr.Internal
{
	// Token: 0x020001B2 RID: 434
	internal sealed class CallNode : ExprNode
	{
		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000985 RID: 2437 RVA: 0x00032DCD File Offset: 0x00030FCD
		public MethodInfo Method
		{
			get
			{
				return this._meth;
			}
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x00032DD5 File Offset: 0x00030FD5
		public CallNode(Token tok, NameNode head, ListNode args, Token tokClose)
			: base(tok)
		{
			this.Head = head;
			this.Args = args;
			this.CloseToken = tokClose;
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x00032DF4 File Offset: 0x00030FF4
		public CallNode(Token tok, NameNode ns, Token dot, NameNode head, ListNode args, Token tokClose)
			: base(tok)
		{
			this.NameSpace = ns;
			this.Dot = dot;
			this.Head = head;
			this.Args = args;
			this.CloseToken = tokClose;
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000988 RID: 2440 RVA: 0x00032E23 File Offset: 0x00031023
		public override NodeKind Kind
		{
			get
			{
				return NodeKind.Call;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000989 RID: 2441 RVA: 0x00032E26 File Offset: 0x00031026
		public override CallNode AsCall
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600098A RID: 2442 RVA: 0x00032E29 File Offset: 0x00031029
		public override CallNode TestCall
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x00032E2C File Offset: 0x0003102C
		public override void Accept(NodeVisitor visitor)
		{
			if (visitor.PreVisit(this))
			{
				if (this.NameSpace != null)
				{
					this.NameSpace.Accept(visitor);
				}
				this.Head.Accept(visitor);
				this.Args.Accept(visitor);
				visitor.PostVisit(this);
			}
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x00032E6A File Offset: 0x0003106A
		public void SetMethod(MethodInfo meth)
		{
			this._meth = meth;
		}

		// Token: 0x040004F3 RID: 1267
		public readonly NameNode NameSpace;

		// Token: 0x040004F4 RID: 1268
		public readonly Token Dot;

		// Token: 0x040004F5 RID: 1269
		public readonly NameNode Head;

		// Token: 0x040004F6 RID: 1270
		public readonly ListNode Args;

		// Token: 0x040004F7 RID: 1271
		public readonly Token CloseToken;

		// Token: 0x040004F8 RID: 1272
		private MethodInfo _meth;
	}
}
