using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes
{
	// Token: 0x02000E60 RID: 3680
	public struct sheetSplits : IProgramNodeBuilder, IEquatable<sheetSplits>
	{
		// Token: 0x170011F8 RID: 4600
		// (get) Token: 0x0600639D RID: 25501 RVA: 0x00143CF6 File Offset: 0x00141EF6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600639E RID: 25502 RVA: 0x00143CFE File Offset: 0x00141EFE
		private sheetSplits(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600639F RID: 25503 RVA: 0x00143D07 File Offset: 0x00141F07
		public static sheetSplits CreateUnsafe(ProgramNode node)
		{
			return new sheetSplits(node);
		}

		// Token: 0x060063A0 RID: 25504 RVA: 0x00143D10 File Offset: 0x00141F10
		public static sheetSplits? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.sheetSplits)
			{
				return null;
			}
			return new sheetSplits?(sheetSplits.CreateUnsafe(node));
		}

		// Token: 0x060063A1 RID: 25505 RVA: 0x00143D4A File Offset: 0x00141F4A
		public static sheetSplits CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new sheetSplits(new Hole(g.Symbol.sheetSplits, holeId));
		}

		// Token: 0x060063A2 RID: 25506 RVA: 0x00143D62 File Offset: 0x00141F62
		public BorderedAreas Cast_BorderedAreas()
		{
			return BorderedAreas.CreateUnsafe(this.Node);
		}

		// Token: 0x060063A3 RID: 25507 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_BorderedAreas(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x060063A4 RID: 25508 RVA: 0x00143D6F File Offset: 0x00141F6F
		public bool Is_BorderedAreas(GrammarBuilders g, out BorderedAreas value)
		{
			value = BorderedAreas.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x060063A5 RID: 25509 RVA: 0x00143D83 File Offset: 0x00141F83
		public BorderedAreas? As_BorderedAreas(GrammarBuilders g)
		{
			return new BorderedAreas?(BorderedAreas.CreateUnsafe(this.Node));
		}

		// Token: 0x060063A6 RID: 25510 RVA: 0x00143D95 File Offset: 0x00141F95
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060063A7 RID: 25511 RVA: 0x00143DA8 File Offset: 0x00141FA8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060063A8 RID: 25512 RVA: 0x00143DD2 File Offset: 0x00141FD2
		public bool Equals(sheetSplits other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002C0A RID: 11274
		private ProgramNode _node;
	}
}
