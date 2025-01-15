using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes
{
	// Token: 0x02000E28 RID: 3624
	public struct TrimRightSingleCellColumns : IProgramNodeBuilder, IEquatable<TrimRightSingleCellColumns>
	{
		// Token: 0x17001187 RID: 4487
		// (get) Token: 0x060060D1 RID: 24785 RVA: 0x0013E3AA File Offset: 0x0013C5AA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060060D2 RID: 24786 RVA: 0x0013E3B2 File Offset: 0x0013C5B2
		private TrimRightSingleCellColumns(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060060D3 RID: 24787 RVA: 0x0013E3BB File Offset: 0x0013C5BB
		public static TrimRightSingleCellColumns CreateUnsafe(ProgramNode node)
		{
			return new TrimRightSingleCellColumns(node);
		}

		// Token: 0x060060D4 RID: 24788 RVA: 0x0013E3C4 File Offset: 0x0013C5C4
		public static TrimRightSingleCellColumns? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.TrimRightSingleCellColumns)
			{
				return null;
			}
			return new TrimRightSingleCellColumns?(TrimRightSingleCellColumns.CreateUnsafe(node));
		}

		// Token: 0x060060D5 RID: 24789 RVA: 0x0013E3F9 File Offset: 0x0013C5F9
		public TrimRightSingleCellColumns(GrammarBuilders g, horizontalSheetSection value0)
		{
			this._node = g.Rule.TrimRightSingleCellColumns.BuildASTNode(value0.Node);
		}

		// Token: 0x060060D6 RID: 24790 RVA: 0x0013E418 File Offset: 0x0013C618
		public static implicit operator sheetSection(TrimRightSingleCellColumns arg)
		{
			return sheetSection.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001188 RID: 4488
		// (get) Token: 0x060060D7 RID: 24791 RVA: 0x0013E426 File Offset: 0x0013C626
		public horizontalSheetSection horizontalSheetSection
		{
			get
			{
				return horizontalSheetSection.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x060060D8 RID: 24792 RVA: 0x0013E43A File Offset: 0x0013C63A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060060D9 RID: 24793 RVA: 0x0013E450 File Offset: 0x0013C650
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060060DA RID: 24794 RVA: 0x0013E47A File Offset: 0x0013C67A
		public bool Equals(TrimRightSingleCellColumns other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BD2 RID: 11218
		private ProgramNode _node;
	}
}
