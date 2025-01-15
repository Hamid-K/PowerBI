using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes
{
	// Token: 0x020011E6 RID: 4582
	public struct MatchColumns : IProgramNodeBuilder, IEquatable<MatchColumns>
	{
		// Token: 0x170017A1 RID: 6049
		// (get) Token: 0x060089A9 RID: 35241 RVA: 0x001CF78A File Offset: 0x001CD98A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060089AA RID: 35242 RVA: 0x001CF792 File Offset: 0x001CD992
		private MatchColumns(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060089AB RID: 35243 RVA: 0x001CF79B File Offset: 0x001CD99B
		public static MatchColumns CreateUnsafe(ProgramNode node)
		{
			return new MatchColumns(node);
		}

		// Token: 0x060089AC RID: 35244 RVA: 0x001CF7A4 File Offset: 0x001CD9A4
		public static MatchColumns? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.MatchColumns)
			{
				return null;
			}
			return new MatchColumns?(MatchColumns.CreateUnsafe(node));
		}

		// Token: 0x060089AD RID: 35245 RVA: 0x001CF7D9 File Offset: 0x001CD9D9
		public MatchColumns(GrammarBuilders g, disjunctive_match value0, multi_result_matches value1)
		{
			this._node = g.Rule.MatchColumns.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x060089AE RID: 35246 RVA: 0x001CF7FF File Offset: 0x001CD9FF
		public static implicit operator _LetB2(MatchColumns arg)
		{
			return _LetB2.CreateUnsafe(arg.Node);
		}

		// Token: 0x170017A2 RID: 6050
		// (get) Token: 0x060089AF RID: 35247 RVA: 0x001CF80D File Offset: 0x001CDA0D
		public disjunctive_match disjunctive_match
		{
			get
			{
				return disjunctive_match.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170017A3 RID: 6051
		// (get) Token: 0x060089B0 RID: 35248 RVA: 0x001CF821 File Offset: 0x001CDA21
		public multi_result_matches multi_result_matches
		{
			get
			{
				return multi_result_matches.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x060089B1 RID: 35249 RVA: 0x001CF835 File Offset: 0x001CDA35
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060089B2 RID: 35250 RVA: 0x001CF848 File Offset: 0x001CDA48
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060089B3 RID: 35251 RVA: 0x001CF872 File Offset: 0x001CDA72
		public bool Equals(MatchColumns other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400389A RID: 14490
		private ProgramNode _node;
	}
}
