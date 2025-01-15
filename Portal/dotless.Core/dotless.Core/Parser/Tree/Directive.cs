using System;
using System.Linq;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Plugins;

namespace dotless.Core.Parser.Tree
{
	// Token: 0x02000034 RID: 52
	public class Directive : Ruleset
	{
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x0000A0BB File Offset: 0x000082BB
		// (set) Token: 0x060001F1 RID: 497 RVA: 0x0000A0C3 File Offset: 0x000082C3
		public string Name { get; set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x0000A0CC File Offset: 0x000082CC
		// (set) Token: 0x060001F3 RID: 499 RVA: 0x0000A0D4 File Offset: 0x000082D4
		public string Identifier { get; set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x0000A0DD File Offset: 0x000082DD
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x0000A0E5 File Offset: 0x000082E5
		public Node Value { get; set; }

		// Token: 0x060001F6 RID: 502 RVA: 0x0000A0EE File Offset: 0x000082EE
		public Directive(string name, string identifier, NodeList rules)
		{
			this.Name = name;
			base.Rules = rules;
			this.Identifier = identifier;
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000A10B File Offset: 0x0000830B
		public Directive(string name, Node value)
		{
			this.Name = name;
			this.Value = value;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000A121 File Offset: 0x00008321
		protected Directive()
		{
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000A12C File Offset: 0x0000832C
		protected override Node CloneCore()
		{
			if (base.Rules != null)
			{
				return new Directive(this.Name, this.Identifier, (NodeList)base.Rules.Clone());
			}
			return new Directive(this.Name, this.Value.Clone());
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000A179 File Offset: 0x00008379
		public override void Accept(IVisitor visitor)
		{
			base.Rules = base.VisitAndReplace<NodeList>(base.Rules, visitor);
			this.Value = base.VisitAndReplace<Node>(this.Value, visitor);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000A1A4 File Offset: 0x000083A4
		public override Node Evaluate(Env env)
		{
			env.Frames.Push(this);
			Directive directive;
			if (base.Rules != null)
			{
				directive = new Directive(this.Name, this.Identifier, new NodeList(base.Rules.Select((Node r) => r.Evaluate(env))).ReducedFrom<NodeList>(new Node[] { base.Rules }));
			}
			else
			{
				directive = new Directive(this.Name, this.Value.Evaluate(env));
			}
			directive.IsReference = base.IsReference;
			env.Frames.Pop();
			return directive;
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000A258 File Offset: 0x00008458
		public override void AppendCSS(Env env, Context context)
		{
			if (base.IsReference)
			{
				return;
			}
			if (env.Compress && base.Rules != null && !base.Rules.Any<Node>())
			{
				return;
			}
			env.Output.Append(this.Name);
			if (!string.IsNullOrEmpty(this.Identifier))
			{
				env.Output.Append(" ");
				env.Output.Append(this.Identifier);
			}
			if (base.Rules != null)
			{
				if (base.Rules.PreComments)
				{
					env.Output.Append(base.Rules.PreComments);
				}
				base.AppendRules(env);
				env.Output.Append("\n");
				return;
			}
			env.Output.Append(" ").Append(this.Value).Append(";\n");
		}
	}
}
