using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes
{
	// Token: 0x02001E77 RID: 7799
	public struct TmpFilter : IProgramNodeBuilder, IEquatable<TmpFilter>
	{
		// Token: 0x17002BCA RID: 11210
		// (get) Token: 0x0601070F RID: 67343 RVA: 0x0038A9AA File Offset: 0x00388BAA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06010710 RID: 67344 RVA: 0x0038A9B2 File Offset: 0x00388BB2
		private TmpFilter(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06010711 RID: 67345 RVA: 0x0038A9BB File Offset: 0x00388BBB
		public static TmpFilter CreateUnsafe(ProgramNode node)
		{
			return new TmpFilter(node);
		}

		// Token: 0x06010712 RID: 67346 RVA: 0x0038A9C4 File Offset: 0x00388BC4
		public static TmpFilter? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.TmpFilter)
			{
				return null;
			}
			return new TmpFilter?(TmpFilter.CreateUnsafe(node));
		}

		// Token: 0x06010713 RID: 67347 RVA: 0x0038A9F9 File Offset: 0x00388BF9
		public TmpFilter(GrammarBuilders g, match value0, inorderAllNodes value1)
		{
			this._node = g.Rule.TmpFilter.BuildConceptASTFromDslAST(new ProgramNode[] { value0.Node, value1.Node });
		}

		// Token: 0x06010714 RID: 67348 RVA: 0x0038AA2B File Offset: 0x00388C2B
		public static implicit operator tmpFilter(TmpFilter arg)
		{
			return tmpFilter.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002BCB RID: 11211
		// (get) Token: 0x06010715 RID: 67349 RVA: 0x0038AA39 File Offset: 0x00388C39
		public match match
		{
			get
			{
				return match.CreateUnsafe(this.Node.Children[0].Children[0]);
			}
		}

		// Token: 0x17002BCC RID: 11212
		// (get) Token: 0x06010716 RID: 67350 RVA: 0x0038AA54 File Offset: 0x00388C54
		public inorderAllNodes inorderAllNodes
		{
			get
			{
				return inorderAllNodes.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06010717 RID: 67351 RVA: 0x0038AA68 File Offset: 0x00388C68
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06010718 RID: 67352 RVA: 0x0038AA7C File Offset: 0x00388C7C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06010719 RID: 67353 RVA: 0x0038AAA6 File Offset: 0x00388CA6
		public bool Equals(TmpFilter other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040062B6 RID: 25270
		private ProgramNode _node;
	}
}
