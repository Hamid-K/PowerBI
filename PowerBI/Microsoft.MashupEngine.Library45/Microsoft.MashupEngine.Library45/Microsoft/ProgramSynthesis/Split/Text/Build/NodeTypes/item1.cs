using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes
{
	// Token: 0x02001371 RID: 4977
	public struct item1 : IProgramNodeBuilder, IEquatable<item1>
	{
		// Token: 0x17001A7A RID: 6778
		// (get) Token: 0x06009A53 RID: 39507 RVA: 0x0020AD02 File Offset: 0x00208F02
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06009A54 RID: 39508 RVA: 0x0020AD0A File Offset: 0x00208F0A
		private item1(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06009A55 RID: 39509 RVA: 0x0020AD13 File Offset: 0x00208F13
		public static item1 CreateUnsafe(ProgramNode node)
		{
			return new item1(node);
		}

		// Token: 0x06009A56 RID: 39510 RVA: 0x0020AD1C File Offset: 0x00208F1C
		public static item1? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.item1)
			{
				return null;
			}
			return new item1?(item1.CreateUnsafe(node));
		}

		// Token: 0x06009A57 RID: 39511 RVA: 0x0020AD56 File Offset: 0x00208F56
		public static item1 CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new item1(new Hole(g.Symbol.item1, holeId));
		}

		// Token: 0x06009A58 RID: 39512 RVA: 0x0020AD6E File Offset: 0x00208F6E
		public Item1 Cast_Item1()
		{
			return Item1.CreateUnsafe(this.Node);
		}

		// Token: 0x06009A59 RID: 39513 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_Item1(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06009A5A RID: 39514 RVA: 0x0020AD7B File Offset: 0x00208F7B
		public bool Is_Item1(GrammarBuilders g, out Item1 value)
		{
			value = Item1.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06009A5B RID: 39515 RVA: 0x0020AD8F File Offset: 0x00208F8F
		public Item1? As_Item1(GrammarBuilders g)
		{
			return new Item1?(Item1.CreateUnsafe(this.Node));
		}

		// Token: 0x06009A5C RID: 39516 RVA: 0x0020ADA1 File Offset: 0x00208FA1
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06009A5D RID: 39517 RVA: 0x0020ADB4 File Offset: 0x00208FB4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06009A5E RID: 39518 RVA: 0x0020ADDE File Offset: 0x00208FDE
		public bool Equals(item1 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DE8 RID: 15848
		private ProgramNode _node;
	}
}
