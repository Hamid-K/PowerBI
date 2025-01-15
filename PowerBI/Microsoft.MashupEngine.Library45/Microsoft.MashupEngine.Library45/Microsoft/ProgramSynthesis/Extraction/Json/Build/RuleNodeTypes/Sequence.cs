using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes
{
	// Token: 0x02000B5F RID: 2911
	public struct Sequence : IProgramNodeBuilder, IEquatable<Sequence>
	{
		// Token: 0x17000D3B RID: 3387
		// (get) Token: 0x0600497A RID: 18810 RVA: 0x000E7EDE File Offset: 0x000E60DE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600497B RID: 18811 RVA: 0x000E7EE6 File Offset: 0x000E60E6
		private Sequence(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600497C RID: 18812 RVA: 0x000E7EEF File Offset: 0x000E60EF
		public static Sequence CreateUnsafe(ProgramNode node)
		{
			return new Sequence(node);
		}

		// Token: 0x0600497D RID: 18813 RVA: 0x000E7EF8 File Offset: 0x000E60F8
		public static Sequence? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Sequence)
			{
				return null;
			}
			return new Sequence?(Sequence.CreateUnsafe(node));
		}

		// Token: 0x0600497E RID: 18814 RVA: 0x000E7F2D File Offset: 0x000E612D
		public Sequence(GrammarBuilders g, id value0, selectSequence value1)
		{
			this._node = g.Rule.Sequence.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600497F RID: 18815 RVA: 0x000E7F53 File Offset: 0x000E6153
		public static implicit operator sequence(Sequence arg)
		{
			return sequence.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000D3C RID: 3388
		// (get) Token: 0x06004980 RID: 18816 RVA: 0x000E7F61 File Offset: 0x000E6161
		public id id
		{
			get
			{
				return id.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17000D3D RID: 3389
		// (get) Token: 0x06004981 RID: 18817 RVA: 0x000E7F75 File Offset: 0x000E6175
		public selectSequence selectSequence
		{
			get
			{
				return selectSequence.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06004982 RID: 18818 RVA: 0x000E7F89 File Offset: 0x000E6189
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06004983 RID: 18819 RVA: 0x000E7F9C File Offset: 0x000E619C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06004984 RID: 18820 RVA: 0x000E7FC6 File Offset: 0x000E61C6
		public bool Equals(Sequence other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400215A RID: 8538
		private ProgramNode _node;
	}
}
