using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes
{
	// Token: 0x02000E2F RID: 3631
	public struct SplitOnEmptyColumns : IProgramNodeBuilder, IEquatable<SplitOnEmptyColumns>
	{
		// Token: 0x1700119B RID: 4507
		// (get) Token: 0x0600611D RID: 24861 RVA: 0x0013EAA6 File Offset: 0x0013CCA6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600611E RID: 24862 RVA: 0x0013EAAE File Offset: 0x0013CCAE
		private SplitOnEmptyColumns(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600611F RID: 24863 RVA: 0x0013EAB7 File Offset: 0x0013CCB7
		public static SplitOnEmptyColumns CreateUnsafe(ProgramNode node)
		{
			return new SplitOnEmptyColumns(node);
		}

		// Token: 0x06006120 RID: 24864 RVA: 0x0013EAC0 File Offset: 0x0013CCC0
		public static SplitOnEmptyColumns? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.SplitOnEmptyColumns)
			{
				return null;
			}
			return new SplitOnEmptyColumns?(SplitOnEmptyColumns.CreateUnsafe(node));
		}

		// Token: 0x06006121 RID: 24865 RVA: 0x0013EAF5 File Offset: 0x0013CCF5
		public SplitOnEmptyColumns(GrammarBuilders g, uncleanedSheetSection value0)
		{
			this._node = g.Rule.SplitOnEmptyColumns.BuildASTNode(value0.Node);
		}

		// Token: 0x06006122 RID: 24866 RVA: 0x0013EB14 File Offset: 0x0013CD14
		public static implicit operator verticalSheetSplits(SplitOnEmptyColumns arg)
		{
			return verticalSheetSplits.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700119C RID: 4508
		// (get) Token: 0x06006123 RID: 24867 RVA: 0x0013EB22 File Offset: 0x0013CD22
		public uncleanedSheetSection uncleanedSheetSection
		{
			get
			{
				return uncleanedSheetSection.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06006124 RID: 24868 RVA: 0x0013EB36 File Offset: 0x0013CD36
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006125 RID: 24869 RVA: 0x0013EB4C File Offset: 0x0013CD4C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006126 RID: 24870 RVA: 0x0013EB76 File Offset: 0x0013CD76
		public bool Equals(SplitOnEmptyColumns other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BD9 RID: 11225
		private ProgramNode _node;
	}
}
