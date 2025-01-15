using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes
{
	// Token: 0x02000E74 RID: 3700
	public struct sheetPair : IProgramNodeBuilder, IEquatable<sheetPair>
	{
		// Token: 0x17001212 RID: 4626
		// (get) Token: 0x060064ED RID: 25837 RVA: 0x0014735A File Offset: 0x0014555A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060064EE RID: 25838 RVA: 0x00147362 File Offset: 0x00145562
		private sheetPair(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060064EF RID: 25839 RVA: 0x0014736B File Offset: 0x0014556B
		public static sheetPair CreateUnsafe(ProgramNode node)
		{
			return new sheetPair(node);
		}

		// Token: 0x060064F0 RID: 25840 RVA: 0x00147374 File Offset: 0x00145574
		public static sheetPair? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.sheetPair)
			{
				return null;
			}
			return new sheetPair?(sheetPair.CreateUnsafe(node));
		}

		// Token: 0x060064F1 RID: 25841 RVA: 0x001473AE File Offset: 0x001455AE
		public static sheetPair CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new sheetPair(new Hole(g.Symbol.sheetPair, holeId));
		}

		// Token: 0x060064F2 RID: 25842 RVA: 0x001473C6 File Offset: 0x001455C6
		public sheetPair(GrammarBuilders g)
		{
			this = new sheetPair(new VariableNode(g.Symbol.sheetPair));
		}

		// Token: 0x17001213 RID: 4627
		// (get) Token: 0x060064F3 RID: 25843 RVA: 0x001473DE File Offset: 0x001455DE
		public VariableNode Variable
		{
			get
			{
				return (VariableNode)this.Node;
			}
		}

		// Token: 0x060064F4 RID: 25844 RVA: 0x001473EB File Offset: 0x001455EB
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060064F5 RID: 25845 RVA: 0x00147400 File Offset: 0x00145600
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060064F6 RID: 25846 RVA: 0x0014742A File Offset: 0x0014562A
		public bool Equals(sheetPair other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002C1E RID: 11294
		private ProgramNode _node;
	}
}
