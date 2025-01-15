using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x02001017 RID: 4119
	public struct LeafChildrenOf1 : IProgramNodeBuilder, IEquatable<LeafChildrenOf1>
	{
		// Token: 0x17001588 RID: 5512
		// (get) Token: 0x06007979 RID: 31097 RVA: 0x001A074E File Offset: 0x0019E94E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600797A RID: 31098 RVA: 0x001A0756 File Offset: 0x0019E956
		private LeafChildrenOf1(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600797B RID: 31099 RVA: 0x001A075F File Offset: 0x0019E95F
		public static LeafChildrenOf1 CreateUnsafe(ProgramNode node)
		{
			return new LeafChildrenOf1(node);
		}

		// Token: 0x0600797C RID: 31100 RVA: 0x001A0768 File Offset: 0x0019E968
		public static LeafChildrenOf1? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.LeafChildrenOf1)
			{
				return null;
			}
			return new LeafChildrenOf1?(LeafChildrenOf1.CreateUnsafe(node));
		}

		// Token: 0x0600797D RID: 31101 RVA: 0x001A079D File Offset: 0x0019E99D
		public LeafChildrenOf1(GrammarBuilders g, selection3 value0)
		{
			this._node = g.Rule.LeafChildrenOf1.BuildASTNode(value0.Node);
		}

		// Token: 0x0600797E RID: 31102 RVA: 0x001A07BC File Offset: 0x0019E9BC
		public static implicit operator selection2(LeafChildrenOf1 arg)
		{
			return selection2.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001589 RID: 5513
		// (get) Token: 0x0600797F RID: 31103 RVA: 0x001A07CA File Offset: 0x0019E9CA
		public selection3 selection3
		{
			get
			{
				return selection3.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06007980 RID: 31104 RVA: 0x001A07DE File Offset: 0x0019E9DE
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007981 RID: 31105 RVA: 0x001A07F4 File Offset: 0x0019E9F4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007982 RID: 31106 RVA: 0x001A081E File Offset: 0x0019EA1E
		public bool Equals(LeafChildrenOf1 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003330 RID: 13104
		private ProgramNode _node;
	}
}
