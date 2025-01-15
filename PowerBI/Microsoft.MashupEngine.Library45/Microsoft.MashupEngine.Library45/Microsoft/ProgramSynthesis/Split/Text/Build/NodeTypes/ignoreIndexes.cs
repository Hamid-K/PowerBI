using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes
{
	// Token: 0x0200137D RID: 4989
	public struct ignoreIndexes : IProgramNodeBuilder, IEquatable<ignoreIndexes>
	{
		// Token: 0x17001A8D RID: 6797
		// (get) Token: 0x06009AD5 RID: 39637 RVA: 0x0020B85A File Offset: 0x00209A5A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06009AD6 RID: 39638 RVA: 0x0020B862 File Offset: 0x00209A62
		private ignoreIndexes(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06009AD7 RID: 39639 RVA: 0x0020B86B File Offset: 0x00209A6B
		public static ignoreIndexes CreateUnsafe(ProgramNode node)
		{
			return new ignoreIndexes(node);
		}

		// Token: 0x06009AD8 RID: 39640 RVA: 0x0020B874 File Offset: 0x00209A74
		public static ignoreIndexes? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.ignoreIndexes)
			{
				return null;
			}
			return new ignoreIndexes?(ignoreIndexes.CreateUnsafe(node));
		}

		// Token: 0x06009AD9 RID: 39641 RVA: 0x0020B8AE File Offset: 0x00209AAE
		public static ignoreIndexes CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new ignoreIndexes(new Hole(g.Symbol.ignoreIndexes, holeId));
		}

		// Token: 0x06009ADA RID: 39642 RVA: 0x0020B8C6 File Offset: 0x00209AC6
		public ignoreIndexes(GrammarBuilders g, int[] value)
		{
			this = new ignoreIndexes(new LiteralNode(g.Symbol.ignoreIndexes, value));
		}

		// Token: 0x17001A8E RID: 6798
		// (get) Token: 0x06009ADB RID: 39643 RVA: 0x0020B8DF File Offset: 0x00209ADF
		public int[] Value
		{
			get
			{
				return (int[])((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06009ADC RID: 39644 RVA: 0x0020B8F6 File Offset: 0x00209AF6
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06009ADD RID: 39645 RVA: 0x0020B90C File Offset: 0x00209B0C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06009ADE RID: 39646 RVA: 0x0020B936 File Offset: 0x00209B36
		public bool Equals(ignoreIndexes other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DF4 RID: 15860
		private ProgramNode _node;
	}
}
