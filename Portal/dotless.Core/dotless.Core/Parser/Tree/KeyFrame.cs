using System;
using System.Linq;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Plugins;

namespace dotless.Core.Parser.Tree
{
	// Token: 0x0200003C RID: 60
	public class KeyFrame : Ruleset
	{
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600023B RID: 571 RVA: 0x0000AD4E File Offset: 0x00008F4E
		// (set) Token: 0x0600023C RID: 572 RVA: 0x0000AD56 File Offset: 0x00008F56
		public NodeList Identifiers { get; set; }

		// Token: 0x0600023D RID: 573 RVA: 0x0000AD5F File Offset: 0x00008F5F
		public KeyFrame(NodeList identifiers, NodeList rules)
		{
			this.Identifiers = identifiers;
			base.Rules = rules;
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000AD78 File Offset: 0x00008F78
		public override Node Evaluate(Env env)
		{
			env.Frames.Push(this);
			base.Rules = new NodeList(base.Rules.Select((Node r) => r.Evaluate(env))).ReducedFrom<NodeList>(new Node[] { base.Rules });
			env.Frames.Pop();
			return this;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000ADEB File Offset: 0x00008FEB
		public override void Accept(IVisitor visitor)
		{
			base.Rules = base.VisitAndReplace<NodeList>(base.Rules, visitor);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000AE00 File Offset: 0x00009000
		public override void AppendCSS(Env env, Context context)
		{
			env.Output.AppendMany<Node>(this.Identifiers, env.Compress ? "," : ", ");
			if (base.Rules.PreComments)
			{
				env.Output.Append(base.Rules.PreComments);
			}
			base.AppendRules(env);
		}
	}
}
