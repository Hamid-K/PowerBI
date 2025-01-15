using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x02001015 RID: 4117
	public struct CSSSelection : IProgramNodeBuilder, IEquatable<CSSSelection>
	{
		// Token: 0x17001583 RID: 5507
		// (get) Token: 0x06007964 RID: 31076 RVA: 0x001A056E File Offset: 0x0019E76E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007965 RID: 31077 RVA: 0x001A0576 File Offset: 0x0019E776
		private CSSSelection(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007966 RID: 31078 RVA: 0x001A057F File Offset: 0x0019E77F
		public static CSSSelection CreateUnsafe(ProgramNode node)
		{
			return new CSSSelection(node);
		}

		// Token: 0x06007967 RID: 31079 RVA: 0x001A0588 File Offset: 0x0019E788
		public static CSSSelection? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.CSSSelection)
			{
				return null;
			}
			return new CSSSelection?(CSSSelection.CreateUnsafe(node));
		}

		// Token: 0x06007968 RID: 31080 RVA: 0x001A05BD File Offset: 0x0019E7BD
		public CSSSelection(GrammarBuilders g, cssSelector value0, allNodes value1)
		{
			this._node = g.Rule.CSSSelection.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x06007969 RID: 31081 RVA: 0x001A05E3 File Offset: 0x0019E7E3
		public static implicit operator selection(CSSSelection arg)
		{
			return selection.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001584 RID: 5508
		// (get) Token: 0x0600796A RID: 31082 RVA: 0x001A05F1 File Offset: 0x0019E7F1
		public cssSelector cssSelector
		{
			get
			{
				return cssSelector.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001585 RID: 5509
		// (get) Token: 0x0600796B RID: 31083 RVA: 0x001A0605 File Offset: 0x0019E805
		public allNodes allNodes
		{
			get
			{
				return allNodes.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600796C RID: 31084 RVA: 0x001A0619 File Offset: 0x0019E819
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600796D RID: 31085 RVA: 0x001A062C File Offset: 0x0019E82C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600796E RID: 31086 RVA: 0x001A0656 File Offset: 0x0019E856
		public bool Equals(CSSSelection other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400332E RID: 13102
		private ProgramNode _node;
	}
}
