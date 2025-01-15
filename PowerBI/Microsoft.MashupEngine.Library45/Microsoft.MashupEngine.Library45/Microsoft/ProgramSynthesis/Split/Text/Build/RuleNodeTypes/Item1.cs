using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes
{
	// Token: 0x0200135C RID: 4956
	public struct Item1 : IProgramNodeBuilder, IEquatable<Item1>
	{
		// Token: 0x17001A60 RID: 6752
		// (get) Token: 0x06009903 RID: 39171 RVA: 0x00207986 File Offset: 0x00205B86
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06009904 RID: 39172 RVA: 0x0020798E File Offset: 0x00205B8E
		private Item1(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06009905 RID: 39173 RVA: 0x00207997 File Offset: 0x00205B97
		public static Item1 CreateUnsafe(ProgramNode node)
		{
			return new Item1(node);
		}

		// Token: 0x06009906 RID: 39174 RVA: 0x002079A0 File Offset: 0x00205BA0
		public static Item1? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Item1)
			{
				return null;
			}
			return new Item1?(Item1.CreateUnsafe(node));
		}

		// Token: 0x06009907 RID: 39175 RVA: 0x002079D5 File Offset: 0x00205BD5
		public Item1(GrammarBuilders g, pair value0)
		{
			this._node = g.Rule.Item1.BuildASTNode(value0.Node);
		}

		// Token: 0x06009908 RID: 39176 RVA: 0x002079F4 File Offset: 0x00205BF4
		public static implicit operator item1(Item1 arg)
		{
			return item1.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001A61 RID: 6753
		// (get) Token: 0x06009909 RID: 39177 RVA: 0x00207A02 File Offset: 0x00205C02
		public pair pair
		{
			get
			{
				return pair.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600990A RID: 39178 RVA: 0x00207A16 File Offset: 0x00205C16
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600990B RID: 39179 RVA: 0x00207A2C File Offset: 0x00205C2C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600990C RID: 39180 RVA: 0x00207A56 File Offset: 0x00205C56
		public bool Equals(Item1 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DD3 RID: 15827
		private ProgramNode _node;
	}
}
