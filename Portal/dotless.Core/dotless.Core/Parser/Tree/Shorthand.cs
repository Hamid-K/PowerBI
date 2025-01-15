using System;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Plugins;

namespace dotless.Core.Parser.Tree
{
	// Token: 0x0200004A RID: 74
	public class Shorthand : Node
	{
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060002FC RID: 764 RVA: 0x0000DFB3 File Offset: 0x0000C1B3
		// (set) Token: 0x060002FD RID: 765 RVA: 0x0000DFBB File Offset: 0x0000C1BB
		public Node First { get; set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060002FE RID: 766 RVA: 0x0000DFC4 File Offset: 0x0000C1C4
		// (set) Token: 0x060002FF RID: 767 RVA: 0x0000DFCC File Offset: 0x0000C1CC
		public Node Second { get; set; }

		// Token: 0x06000300 RID: 768 RVA: 0x0000DFD5 File Offset: 0x0000C1D5
		public Shorthand(Node first, Node second)
		{
			this.First = first;
			this.Second = second;
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000DFEB File Offset: 0x0000C1EB
		public override Node Evaluate(Env env)
		{
			return new Shorthand(this.First.Evaluate(env), this.Second.Evaluate(env));
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000E00A File Offset: 0x0000C20A
		protected override Node CloneCore()
		{
			return new Shorthand(this.First.Clone(), this.Second.Clone());
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000E027 File Offset: 0x0000C227
		public override void AppendCSS(Env env)
		{
			env.Output.Append(this.First).Append("/").Append(this.Second);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000E050 File Offset: 0x0000C250
		public override void Accept(IVisitor visitor)
		{
			this.First = base.VisitAndReplace<Node>(this.First, visitor);
			this.Second = base.VisitAndReplace<Node>(this.Second, visitor);
		}
	}
}
