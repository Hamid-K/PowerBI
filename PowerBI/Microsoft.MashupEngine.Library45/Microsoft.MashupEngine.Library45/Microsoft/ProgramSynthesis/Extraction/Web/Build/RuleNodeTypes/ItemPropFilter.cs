using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x02001042 RID: 4162
	public struct ItemPropFilter : IProgramNodeBuilder, IEquatable<ItemPropFilter>
	{
		// Token: 0x170015FA RID: 5626
		// (get) Token: 0x06007B43 RID: 31555 RVA: 0x001A304A File Offset: 0x001A124A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007B44 RID: 31556 RVA: 0x001A3052 File Offset: 0x001A1252
		private ItemPropFilter(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007B45 RID: 31557 RVA: 0x001A305B File Offset: 0x001A125B
		public static ItemPropFilter CreateUnsafe(ProgramNode node)
		{
			return new ItemPropFilter(node);
		}

		// Token: 0x06007B46 RID: 31558 RVA: 0x001A3064 File Offset: 0x001A1264
		public static ItemPropFilter? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.ItemPropFilter)
			{
				return null;
			}
			return new ItemPropFilter?(ItemPropFilter.CreateUnsafe(node));
		}

		// Token: 0x06007B47 RID: 31559 RVA: 0x001A3099 File Offset: 0x001A1299
		public ItemPropFilter(GrammarBuilders g, propName value0, nodeCollection value1)
		{
			this._node = g.Rule.ItemPropFilter.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x06007B48 RID: 31560 RVA: 0x001A30BF File Offset: 0x001A12BF
		public static implicit operator nodeCollection(ItemPropFilter arg)
		{
			return nodeCollection.CreateUnsafe(arg.Node);
		}

		// Token: 0x170015FB RID: 5627
		// (get) Token: 0x06007B49 RID: 31561 RVA: 0x001A30CD File Offset: 0x001A12CD
		public propName propName
		{
			get
			{
				return propName.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170015FC RID: 5628
		// (get) Token: 0x06007B4A RID: 31562 RVA: 0x001A30E1 File Offset: 0x001A12E1
		public nodeCollection nodeCollection
		{
			get
			{
				return nodeCollection.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06007B4B RID: 31563 RVA: 0x001A30F5 File Offset: 0x001A12F5
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007B4C RID: 31564 RVA: 0x001A3108 File Offset: 0x001A1308
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007B4D RID: 31565 RVA: 0x001A3132 File Offset: 0x001A1332
		public bool Equals(ItemPropFilter other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400335B RID: 13147
		private ProgramNode _node;
	}
}
