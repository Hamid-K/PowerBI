using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes
{
	// Token: 0x02000981 RID: 2433
	public struct rowRecords : IProgramNodeBuilder, IEquatable<rowRecords>
	{
		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x06003A30 RID: 14896 RVA: 0x000B329E File Offset: 0x000B149E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06003A31 RID: 14897 RVA: 0x000B32A6 File Offset: 0x000B14A6
		private rowRecords(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06003A32 RID: 14898 RVA: 0x000B32AF File Offset: 0x000B14AF
		public static rowRecords CreateUnsafe(ProgramNode node)
		{
			return new rowRecords(node);
		}

		// Token: 0x06003A33 RID: 14899 RVA: 0x000B32B8 File Offset: 0x000B14B8
		public static rowRecords? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.rowRecords)
			{
				return null;
			}
			return new rowRecords?(rowRecords.CreateUnsafe(node));
		}

		// Token: 0x06003A34 RID: 14900 RVA: 0x000B32F2 File Offset: 0x000B14F2
		public static rowRecords CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new rowRecords(new Hole(g.Symbol.rowRecords, holeId));
		}

		// Token: 0x06003A35 RID: 14901 RVA: 0x000B330A File Offset: 0x000B150A
		public rowRecords(GrammarBuilders g)
		{
			this = new rowRecords(new VariableNode(g.Symbol.rowRecords));
		}

		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x06003A36 RID: 14902 RVA: 0x000B3322 File Offset: 0x000B1522
		public VariableNode Variable
		{
			get
			{
				return (VariableNode)this.Node;
			}
		}

		// Token: 0x06003A37 RID: 14903 RVA: 0x000B332F File Offset: 0x000B152F
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06003A38 RID: 14904 RVA: 0x000B3344 File Offset: 0x000B1544
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06003A39 RID: 14905 RVA: 0x000B336E File Offset: 0x000B156E
		public bool Equals(rowRecords other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001AA1 RID: 6817
		private ProgramNode _node;
	}
}
