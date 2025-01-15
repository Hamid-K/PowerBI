using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x0200100D RID: 4109
	public struct Union : IProgramNodeBuilder, IEquatable<Union>
	{
		// Token: 0x17001570 RID: 5488
		// (get) Token: 0x06007911 RID: 30993 RVA: 0x0019FE06 File Offset: 0x0019E006
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007912 RID: 30994 RVA: 0x0019FE0E File Offset: 0x0019E00E
		private Union(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007913 RID: 30995 RVA: 0x0019FE17 File Offset: 0x0019E017
		public static Union CreateUnsafe(ProgramNode node)
		{
			return new Union(node);
		}

		// Token: 0x06007914 RID: 30996 RVA: 0x0019FE20 File Offset: 0x0019E020
		public static Union? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Union)
			{
				return null;
			}
			return new Union?(Union.CreateUnsafe(node));
		}

		// Token: 0x06007915 RID: 30997 RVA: 0x0019FE55 File Offset: 0x0019E055
		public Union(GrammarBuilders g, resultSequence value0, resultSequence value1)
		{
			this._node = g.Rule.Union.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x06007916 RID: 30998 RVA: 0x0019FE7B File Offset: 0x0019E07B
		public static implicit operator resultSequence(Union arg)
		{
			return resultSequence.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001571 RID: 5489
		// (get) Token: 0x06007917 RID: 30999 RVA: 0x0019FE89 File Offset: 0x0019E089
		public resultSequence resultSequence1
		{
			get
			{
				return resultSequence.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001572 RID: 5490
		// (get) Token: 0x06007918 RID: 31000 RVA: 0x0019FE9D File Offset: 0x0019E09D
		public resultSequence resultSequence2
		{
			get
			{
				return resultSequence.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06007919 RID: 31001 RVA: 0x0019FEB1 File Offset: 0x0019E0B1
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600791A RID: 31002 RVA: 0x0019FEC4 File Offset: 0x0019E0C4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600791B RID: 31003 RVA: 0x0019FEEE File Offset: 0x0019E0EE
		public bool Equals(Union other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003326 RID: 13094
		private ProgramNode _node;
	}
}
