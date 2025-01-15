using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes
{
	// Token: 0x02000E6E RID: 3694
	public struct index : IProgramNodeBuilder, IEquatable<index>
	{
		// Token: 0x17001206 RID: 4614
		// (get) Token: 0x060064B1 RID: 25777 RVA: 0x00146DAA File Offset: 0x00144FAA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060064B2 RID: 25778 RVA: 0x00146DB2 File Offset: 0x00144FB2
		private index(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060064B3 RID: 25779 RVA: 0x00146DBB File Offset: 0x00144FBB
		public static index CreateUnsafe(ProgramNode node)
		{
			return new index(node);
		}

		// Token: 0x060064B4 RID: 25780 RVA: 0x00146DC4 File Offset: 0x00144FC4
		public static index? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.index)
			{
				return null;
			}
			return new index?(index.CreateUnsafe(node));
		}

		// Token: 0x060064B5 RID: 25781 RVA: 0x00146DFE File Offset: 0x00144FFE
		public static index CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new index(new Hole(g.Symbol.index, holeId));
		}

		// Token: 0x060064B6 RID: 25782 RVA: 0x00146E16 File Offset: 0x00145016
		public index(GrammarBuilders g, int value)
		{
			this = new index(new LiteralNode(g.Symbol.index, value));
		}

		// Token: 0x17001207 RID: 4615
		// (get) Token: 0x060064B7 RID: 25783 RVA: 0x00146E34 File Offset: 0x00145034
		public int Value
		{
			get
			{
				return (int)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x060064B8 RID: 25784 RVA: 0x00146E4B File Offset: 0x0014504B
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060064B9 RID: 25785 RVA: 0x00146E60 File Offset: 0x00145060
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060064BA RID: 25786 RVA: 0x00146E8A File Offset: 0x0014508A
		public bool Equals(index other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002C18 RID: 11288
		private ProgramNode _node;
	}
}
