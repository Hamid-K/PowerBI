using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes
{
	// Token: 0x02001358 RID: 4952
	public struct Item2 : IProgramNodeBuilder, IEquatable<Item2>
	{
		// Token: 0x17001A56 RID: 6742
		// (get) Token: 0x060098D9 RID: 39129 RVA: 0x002075C6 File Offset: 0x002057C6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060098DA RID: 39130 RVA: 0x002075CE File Offset: 0x002057CE
		private Item2(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060098DB RID: 39131 RVA: 0x002075D7 File Offset: 0x002057D7
		public static Item2 CreateUnsafe(ProgramNode node)
		{
			return new Item2(node);
		}

		// Token: 0x060098DC RID: 39132 RVA: 0x002075E0 File Offset: 0x002057E0
		public static Item2? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Item2)
			{
				return null;
			}
			return new Item2?(Item2.CreateUnsafe(node));
		}

		// Token: 0x060098DD RID: 39133 RVA: 0x00207615 File Offset: 0x00205815
		public Item2(GrammarBuilders g, pair value0)
		{
			this._node = g.Rule.Item2.BuildASTNode(value0.Node);
		}

		// Token: 0x060098DE RID: 39134 RVA: 0x00207634 File Offset: 0x00205834
		public static implicit operator _LetB0(Item2 arg)
		{
			return _LetB0.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001A57 RID: 6743
		// (get) Token: 0x060098DF RID: 39135 RVA: 0x00207642 File Offset: 0x00205842
		public pair pair
		{
			get
			{
				return pair.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x060098E0 RID: 39136 RVA: 0x00207656 File Offset: 0x00205856
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060098E1 RID: 39137 RVA: 0x0020766C File Offset: 0x0020586C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060098E2 RID: 39138 RVA: 0x00207696 File Offset: 0x00205896
		public bool Equals(Item2 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DCF RID: 15823
		private ProgramNode _node;
	}
}
