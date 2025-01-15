using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes
{
	// Token: 0x02000E5F RID: 3679
	public struct verticalSheetSplits : IProgramNodeBuilder, IEquatable<verticalSheetSplits>
	{
		// Token: 0x170011F7 RID: 4599
		// (get) Token: 0x06006391 RID: 25489 RVA: 0x00143C06 File Offset: 0x00141E06
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006392 RID: 25490 RVA: 0x00143C0E File Offset: 0x00141E0E
		private verticalSheetSplits(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006393 RID: 25491 RVA: 0x00143C17 File Offset: 0x00141E17
		public static verticalSheetSplits CreateUnsafe(ProgramNode node)
		{
			return new verticalSheetSplits(node);
		}

		// Token: 0x06006394 RID: 25492 RVA: 0x00143C20 File Offset: 0x00141E20
		public static verticalSheetSplits? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.verticalSheetSplits)
			{
				return null;
			}
			return new verticalSheetSplits?(verticalSheetSplits.CreateUnsafe(node));
		}

		// Token: 0x06006395 RID: 25493 RVA: 0x00143C5A File Offset: 0x00141E5A
		public static verticalSheetSplits CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new verticalSheetSplits(new Hole(g.Symbol.verticalSheetSplits, holeId));
		}

		// Token: 0x06006396 RID: 25494 RVA: 0x00143C72 File Offset: 0x00141E72
		public SplitOnEmptyColumns Cast_SplitOnEmptyColumns()
		{
			return SplitOnEmptyColumns.CreateUnsafe(this.Node);
		}

		// Token: 0x06006397 RID: 25495 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_SplitOnEmptyColumns(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06006398 RID: 25496 RVA: 0x00143C7F File Offset: 0x00141E7F
		public bool Is_SplitOnEmptyColumns(GrammarBuilders g, out SplitOnEmptyColumns value)
		{
			value = SplitOnEmptyColumns.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06006399 RID: 25497 RVA: 0x00143C93 File Offset: 0x00141E93
		public SplitOnEmptyColumns? As_SplitOnEmptyColumns(GrammarBuilders g)
		{
			return new SplitOnEmptyColumns?(SplitOnEmptyColumns.CreateUnsafe(this.Node));
		}

		// Token: 0x0600639A RID: 25498 RVA: 0x00143CA5 File Offset: 0x00141EA5
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600639B RID: 25499 RVA: 0x00143CB8 File Offset: 0x00141EB8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600639C RID: 25500 RVA: 0x00143CE2 File Offset: 0x00141EE2
		public bool Equals(verticalSheetSplits other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002C09 RID: 11273
		private ProgramNode _node;
	}
}
