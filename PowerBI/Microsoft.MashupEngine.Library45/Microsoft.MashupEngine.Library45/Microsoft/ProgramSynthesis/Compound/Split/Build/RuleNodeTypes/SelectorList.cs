using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes
{
	// Token: 0x02000941 RID: 2369
	public struct SelectorList : IProgramNodeBuilder, IEquatable<SelectorList>
	{
		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x06003705 RID: 14085 RVA: 0x000AD3B2 File Offset: 0x000AB5B2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06003706 RID: 14086 RVA: 0x000AD3BA File Offset: 0x000AB5BA
		private SelectorList(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06003707 RID: 14087 RVA: 0x000AD3C3 File Offset: 0x000AB5C3
		public static SelectorList CreateUnsafe(ProgramNode node)
		{
			return new SelectorList(node);
		}

		// Token: 0x06003708 RID: 14088 RVA: 0x000AD3CC File Offset: 0x000AB5CC
		public static SelectorList? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.SelectorList)
			{
				return null;
			}
			return new SelectorList?(SelectorList.CreateUnsafe(node));
		}

		// Token: 0x06003709 RID: 14089 RVA: 0x000AD401 File Offset: 0x000AB601
		public SelectorList(GrammarBuilders g, columnSelector value0, columnSelectorList value1)
		{
			this._node = g.Rule.SelectorList.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600370A RID: 14090 RVA: 0x000AD427 File Offset: 0x000AB627
		public static implicit operator columnSelectorList(SelectorList arg)
		{
			return columnSelectorList.CreateUnsafe(arg.Node);
		}

		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x0600370B RID: 14091 RVA: 0x000AD435 File Offset: 0x000AB635
		public columnSelector columnSelector
		{
			get
			{
				return columnSelector.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x0600370C RID: 14092 RVA: 0x000AD449 File Offset: 0x000AB649
		public columnSelectorList columnSelectorList
		{
			get
			{
				return columnSelectorList.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600370D RID: 14093 RVA: 0x000AD45D File Offset: 0x000AB65D
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600370E RID: 14094 RVA: 0x000AD470 File Offset: 0x000AB670
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600370F RID: 14095 RVA: 0x000AD49A File Offset: 0x000AB69A
		public bool Equals(SelectorList other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A61 RID: 6753
		private ProgramNode _node;
	}
}
