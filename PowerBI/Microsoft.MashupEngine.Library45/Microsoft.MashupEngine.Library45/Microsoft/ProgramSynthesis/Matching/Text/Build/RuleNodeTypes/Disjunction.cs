using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes
{
	// Token: 0x020011E1 RID: 4577
	public struct Disjunction : IProgramNodeBuilder, IEquatable<Disjunction>
	{
		// Token: 0x17001795 RID: 6037
		// (get) Token: 0x06008975 RID: 35189 RVA: 0x001CF2E6 File Offset: 0x001CD4E6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06008976 RID: 35190 RVA: 0x001CF2EE File Offset: 0x001CD4EE
		private Disjunction(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06008977 RID: 35191 RVA: 0x001CF2F7 File Offset: 0x001CD4F7
		public static Disjunction CreateUnsafe(ProgramNode node)
		{
			return new Disjunction(node);
		}

		// Token: 0x06008978 RID: 35192 RVA: 0x001CF300 File Offset: 0x001CD500
		public static Disjunction? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Disjunction)
			{
				return null;
			}
			return new Disjunction?(Disjunction.CreateUnsafe(node));
		}

		// Token: 0x06008979 RID: 35193 RVA: 0x001CF335 File Offset: 0x001CD535
		public Disjunction(GrammarBuilders g, match value0, disjunctive_match value1)
		{
			this._node = g.Rule.Disjunction.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600897A RID: 35194 RVA: 0x001CF35B File Offset: 0x001CD55B
		public static implicit operator disjunctive_match(Disjunction arg)
		{
			return disjunctive_match.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001796 RID: 6038
		// (get) Token: 0x0600897B RID: 35195 RVA: 0x001CF369 File Offset: 0x001CD569
		public match match
		{
			get
			{
				return match.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001797 RID: 6039
		// (get) Token: 0x0600897C RID: 35196 RVA: 0x001CF37D File Offset: 0x001CD57D
		public disjunctive_match disjunctive_match
		{
			get
			{
				return disjunctive_match.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600897D RID: 35197 RVA: 0x001CF391 File Offset: 0x001CD591
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600897E RID: 35198 RVA: 0x001CF3A4 File Offset: 0x001CD5A4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600897F RID: 35199 RVA: 0x001CF3CE File Offset: 0x001CD5CE
		public bool Equals(Disjunction other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003895 RID: 14485
		private ProgramNode _node;
	}
}
