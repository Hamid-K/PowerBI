using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes
{
	// Token: 0x02000E41 RID: 3649
	public struct BottomLeftSameFontCells : IProgramNodeBuilder, IEquatable<BottomLeftSameFontCells>
	{
		// Token: 0x170011C0 RID: 4544
		// (get) Token: 0x060061D2 RID: 25042 RVA: 0x0013FAC6 File Offset: 0x0013DCC6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060061D3 RID: 25043 RVA: 0x0013FACE File Offset: 0x0013DCCE
		private BottomLeftSameFontCells(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060061D4 RID: 25044 RVA: 0x0013FAD7 File Offset: 0x0013DCD7
		public static BottomLeftSameFontCells CreateUnsafe(ProgramNode node)
		{
			return new BottomLeftSameFontCells(node);
		}

		// Token: 0x060061D5 RID: 25045 RVA: 0x0013FAE0 File Offset: 0x0013DCE0
		public static BottomLeftSameFontCells? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.BottomLeftSameFontCells)
			{
				return null;
			}
			return new BottomLeftSameFontCells?(BottomLeftSameFontCells.CreateUnsafe(node));
		}

		// Token: 0x060061D6 RID: 25046 RVA: 0x0013FB15 File Offset: 0x0013DD15
		public BottomLeftSameFontCells(GrammarBuilders g, aboveOrHeader value0)
		{
			this._node = g.Rule.BottomLeftSameFontCells.BuildASTNode(value0.Node);
		}

		// Token: 0x060061D7 RID: 25047 RVA: 0x0013FB34 File Offset: 0x0013DD34
		public static implicit operator title(BottomLeftSameFontCells arg)
		{
			return title.CreateUnsafe(arg.Node);
		}

		// Token: 0x170011C1 RID: 4545
		// (get) Token: 0x060061D8 RID: 25048 RVA: 0x0013FB42 File Offset: 0x0013DD42
		public aboveOrHeader aboveOrHeader
		{
			get
			{
				return aboveOrHeader.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x060061D9 RID: 25049 RVA: 0x0013FB56 File Offset: 0x0013DD56
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060061DA RID: 25050 RVA: 0x0013FB6C File Offset: 0x0013DD6C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060061DB RID: 25051 RVA: 0x0013FB96 File Offset: 0x0013DD96
		public bool Equals(BottomLeftSameFontCells other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BEB RID: 11243
		private ProgramNode _node;
	}
}
