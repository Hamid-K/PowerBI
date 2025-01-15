using System;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Plugins;

namespace dotless.Core.Parser.Tree
{
	// Token: 0x0200002A RID: 42
	public class Alpha : Call
	{
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600017D RID: 381 RVA: 0x0000856F File Offset: 0x0000676F
		// (set) Token: 0x0600017E RID: 382 RVA: 0x00008577 File Offset: 0x00006777
		public Node Value { get; set; }

		// Token: 0x0600017F RID: 383 RVA: 0x00008580 File Offset: 0x00006780
		public Alpha(Node value)
		{
			this.Value = value;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000858F File Offset: 0x0000678F
		public override Node Evaluate(Env env)
		{
			this.Value = this.Value.Evaluate(env);
			return this;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x000085A4 File Offset: 0x000067A4
		public override void AppendCSS(Env env)
		{
			env.Output.Append("alpha(opacity=").Append(this.Value).Append(")");
		}

		// Token: 0x06000182 RID: 386 RVA: 0x000085CC File Offset: 0x000067CC
		public override void Accept(IVisitor visitor)
		{
			this.Value = base.VisitAndReplace<Node>(this.Value, visitor);
		}
	}
}
