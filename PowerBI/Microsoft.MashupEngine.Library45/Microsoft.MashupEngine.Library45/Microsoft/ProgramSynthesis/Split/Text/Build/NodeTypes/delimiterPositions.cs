using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes
{
	// Token: 0x0200137F RID: 4991
	public struct delimiterPositions : IProgramNodeBuilder, IEquatable<delimiterPositions>
	{
		// Token: 0x17001A91 RID: 6801
		// (get) Token: 0x06009AE9 RID: 39657 RVA: 0x0020BA3A File Offset: 0x00209C3A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06009AEA RID: 39658 RVA: 0x0020BA42 File Offset: 0x00209C42
		private delimiterPositions(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06009AEB RID: 39659 RVA: 0x0020BA4B File Offset: 0x00209C4B
		public static delimiterPositions CreateUnsafe(ProgramNode node)
		{
			return new delimiterPositions(node);
		}

		// Token: 0x06009AEC RID: 39660 RVA: 0x0020BA54 File Offset: 0x00209C54
		public static delimiterPositions? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.delimiterPositions)
			{
				return null;
			}
			return new delimiterPositions?(delimiterPositions.CreateUnsafe(node));
		}

		// Token: 0x06009AED RID: 39661 RVA: 0x0020BA8E File Offset: 0x00209C8E
		public static delimiterPositions CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new delimiterPositions(new Hole(g.Symbol.delimiterPositions, holeId));
		}

		// Token: 0x06009AEE RID: 39662 RVA: 0x0020BAA6 File Offset: 0x00209CA6
		public delimiterPositions(GrammarBuilders g, Record<int, int>[] value)
		{
			this = new delimiterPositions(new LiteralNode(g.Symbol.delimiterPositions, value));
		}

		// Token: 0x17001A92 RID: 6802
		// (get) Token: 0x06009AEF RID: 39663 RVA: 0x0020BABF File Offset: 0x00209CBF
		public Record<int, int>[] Value
		{
			get
			{
				return (Record<int, int>[])((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06009AF0 RID: 39664 RVA: 0x0020BAD6 File Offset: 0x00209CD6
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06009AF1 RID: 39665 RVA: 0x0020BAEC File Offset: 0x00209CEC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06009AF2 RID: 39666 RVA: 0x0020BB16 File Offset: 0x00209D16
		public bool Equals(delimiterPositions other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DF6 RID: 15862
		private ProgramNode _node;
	}
}
