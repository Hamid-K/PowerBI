using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes
{
	// Token: 0x02000972 RID: 2418
	public struct columnList : IProgramNodeBuilder, IEquatable<columnList>
	{
		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x0600399A RID: 14746 RVA: 0x000B2486 File Offset: 0x000B0686
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600399B RID: 14747 RVA: 0x000B248E File Offset: 0x000B068E
		private columnList(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600399C RID: 14748 RVA: 0x000B2497 File Offset: 0x000B0697
		public static columnList CreateUnsafe(ProgramNode node)
		{
			return new columnList(node);
		}

		// Token: 0x0600399D RID: 14749 RVA: 0x000B24A0 File Offset: 0x000B06A0
		public static columnList? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.columnList)
			{
				return null;
			}
			return new columnList?(columnList.CreateUnsafe(node));
		}

		// Token: 0x0600399E RID: 14750 RVA: 0x000B24DA File Offset: 0x000B06DA
		public static columnList CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new columnList(new Hole(g.Symbol.columnList, holeId));
		}

		// Token: 0x0600399F RID: 14751 RVA: 0x000B24F2 File Offset: 0x000B06F2
		public columnList(GrammarBuilders g, int[] value)
		{
			this = new columnList(new LiteralNode(g.Symbol.columnList, value));
		}

		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x060039A0 RID: 14752 RVA: 0x000B250B File Offset: 0x000B070B
		public int[] Value
		{
			get
			{
				return (int[])((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x060039A1 RID: 14753 RVA: 0x000B2522 File Offset: 0x000B0722
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060039A2 RID: 14754 RVA: 0x000B2538 File Offset: 0x000B0738
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060039A3 RID: 14755 RVA: 0x000B2562 File Offset: 0x000B0762
		public bool Equals(columnList other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A92 RID: 6802
		private ProgramNode _node;
	}
}
