using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes
{
	// Token: 0x02001AC4 RID: 6852
	public struct inputTable : IProgramNodeBuilder, IEquatable<inputTable>
	{
		// Token: 0x170025ED RID: 9709
		// (get) Token: 0x0600E2AF RID: 58031 RVA: 0x0030209E File Offset: 0x0030029E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600E2B0 RID: 58032 RVA: 0x003020A6 File Offset: 0x003002A6
		private inputTable(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600E2B1 RID: 58033 RVA: 0x003020AF File Offset: 0x003002AF
		public static inputTable CreateUnsafe(ProgramNode node)
		{
			return new inputTable(node);
		}

		// Token: 0x0600E2B2 RID: 58034 RVA: 0x003020B8 File Offset: 0x003002B8
		public static inputTable? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.inputTable)
			{
				return null;
			}
			return new inputTable?(inputTable.CreateUnsafe(node));
		}

		// Token: 0x0600E2B3 RID: 58035 RVA: 0x003020F2 File Offset: 0x003002F2
		public static inputTable CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new inputTable(new Hole(g.Symbol.inputTable, holeId));
		}

		// Token: 0x0600E2B4 RID: 58036 RVA: 0x0030210A File Offset: 0x0030030A
		public inputTable(GrammarBuilders g)
		{
			this = new inputTable(new VariableNode(g.Symbol.inputTable));
		}

		// Token: 0x170025EE RID: 9710
		// (get) Token: 0x0600E2B5 RID: 58037 RVA: 0x00302122 File Offset: 0x00300322
		public VariableNode Variable
		{
			get
			{
				return (VariableNode)this.Node;
			}
		}

		// Token: 0x0600E2B6 RID: 58038 RVA: 0x0030212F File Offset: 0x0030032F
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600E2B7 RID: 58039 RVA: 0x00302144 File Offset: 0x00300344
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600E2B8 RID: 58040 RVA: 0x0030216E File Offset: 0x0030036E
		public bool Equals(inputTable other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005583 RID: 21891
		private ProgramNode _node;
	}
}
