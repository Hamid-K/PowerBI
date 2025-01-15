using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes
{
	// Token: 0x020011E0 RID: 4576
	public struct NoMatch : IProgramNodeBuilder, IEquatable<NoMatch>
	{
		// Token: 0x17001794 RID: 6036
		// (get) Token: 0x0600896C RID: 35180 RVA: 0x001CF21A File Offset: 0x001CD41A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600896D RID: 35181 RVA: 0x001CF222 File Offset: 0x001CD422
		private NoMatch(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600896E RID: 35182 RVA: 0x001CF22B File Offset: 0x001CD42B
		public static NoMatch CreateUnsafe(ProgramNode node)
		{
			return new NoMatch(node);
		}

		// Token: 0x0600896F RID: 35183 RVA: 0x001CF234 File Offset: 0x001CD434
		public static NoMatch? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.NoMatch)
			{
				return null;
			}
			return new NoMatch?(NoMatch.CreateUnsafe(node));
		}

		// Token: 0x06008970 RID: 35184 RVA: 0x001CF269 File Offset: 0x001CD469
		public NoMatch(GrammarBuilders g)
		{
			this._node = g.Rule.NoMatch.BuildASTNode(Array.Empty<ProgramNode>());
		}

		// Token: 0x06008971 RID: 35185 RVA: 0x001CF286 File Offset: 0x001CD486
		public static implicit operator disjunctive_match(NoMatch arg)
		{
			return disjunctive_match.CreateUnsafe(arg.Node);
		}

		// Token: 0x06008972 RID: 35186 RVA: 0x001CF294 File Offset: 0x001CD494
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06008973 RID: 35187 RVA: 0x001CF2A8 File Offset: 0x001CD4A8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06008974 RID: 35188 RVA: 0x001CF2D2 File Offset: 0x001CD4D2
		public bool Equals(NoMatch other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003894 RID: 14484
		private ProgramNode _node;
	}
}
