using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Semantics;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes
{
	// Token: 0x02000E71 RID: 3697
	public struct splitMode : IProgramNodeBuilder, IEquatable<splitMode>
	{
		// Token: 0x1700120C RID: 4620
		// (get) Token: 0x060064CF RID: 25807 RVA: 0x00147082 File Offset: 0x00145282
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060064D0 RID: 25808 RVA: 0x0014708A File Offset: 0x0014528A
		private splitMode(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060064D1 RID: 25809 RVA: 0x00147093 File Offset: 0x00145293
		public static splitMode CreateUnsafe(ProgramNode node)
		{
			return new splitMode(node);
		}

		// Token: 0x060064D2 RID: 25810 RVA: 0x0014709C File Offset: 0x0014529C
		public static splitMode? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.splitMode)
			{
				return null;
			}
			return new splitMode?(splitMode.CreateUnsafe(node));
		}

		// Token: 0x060064D3 RID: 25811 RVA: 0x001470D6 File Offset: 0x001452D6
		public static splitMode CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new splitMode(new Hole(g.Symbol.splitMode, holeId));
		}

		// Token: 0x060064D4 RID: 25812 RVA: 0x001470EE File Offset: 0x001452EE
		public splitMode(GrammarBuilders g, SplitMode value)
		{
			this = new splitMode(new LiteralNode(g.Symbol.splitMode, value));
		}

		// Token: 0x1700120D RID: 4621
		// (get) Token: 0x060064D5 RID: 25813 RVA: 0x0014710C File Offset: 0x0014530C
		public SplitMode Value
		{
			get
			{
				return (SplitMode)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x060064D6 RID: 25814 RVA: 0x00147123 File Offset: 0x00145323
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060064D7 RID: 25815 RVA: 0x00147138 File Offset: 0x00145338
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060064D8 RID: 25816 RVA: 0x00147162 File Offset: 0x00145362
		public bool Equals(splitMode other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002C1B RID: 11291
		private ProgramNode _node;
	}
}
