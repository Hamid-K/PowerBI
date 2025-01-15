using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes
{
	// Token: 0x02000A37 RID: 2615
	public struct Conjunction : IProgramNodeBuilder, IEquatable<Conjunction>
	{
		// Token: 0x17000B2E RID: 2862
		// (get) Token: 0x06004010 RID: 16400 RVA: 0x000CA35A File Offset: 0x000C855A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06004011 RID: 16401 RVA: 0x000CA362 File Offset: 0x000C8562
		private Conjunction(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06004012 RID: 16402 RVA: 0x000CA36B File Offset: 0x000C856B
		public static Conjunction CreateUnsafe(ProgramNode node)
		{
			return new Conjunction(node);
		}

		// Token: 0x06004013 RID: 16403 RVA: 0x000CA374 File Offset: 0x000C8574
		public static Conjunction? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Conjunction)
			{
				return null;
			}
			return new Conjunction?(Conjunction.CreateUnsafe(node));
		}

		// Token: 0x06004014 RID: 16404 RVA: 0x000CA3A9 File Offset: 0x000C85A9
		public Conjunction(GrammarBuilders g, pred value0, baseConjunct value1)
		{
			this._node = g.Rule.Conjunction.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x06004015 RID: 16405 RVA: 0x000CA3CF File Offset: 0x000C85CF
		public static implicit operator baseConjunct(Conjunction arg)
		{
			return baseConjunct.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000B2F RID: 2863
		// (get) Token: 0x06004016 RID: 16406 RVA: 0x000CA3DD File Offset: 0x000C85DD
		public pred pred
		{
			get
			{
				return pred.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17000B30 RID: 2864
		// (get) Token: 0x06004017 RID: 16407 RVA: 0x000CA3F1 File Offset: 0x000C85F1
		public baseConjunct baseConjunct
		{
			get
			{
				return baseConjunct.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06004018 RID: 16408 RVA: 0x000CA405 File Offset: 0x000C8605
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06004019 RID: 16409 RVA: 0x000CA418 File Offset: 0x000C8618
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600401A RID: 16410 RVA: 0x000CA442 File Offset: 0x000C8642
		public bool Equals(Conjunction other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001D72 RID: 7538
		private ProgramNode _node;
	}
}
