using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes
{
	// Token: 0x02000E2D RID: 3629
	public struct SplitOnEmptyRows : IProgramNodeBuilder, IEquatable<SplitOnEmptyRows>
	{
		// Token: 0x17001195 RID: 4501
		// (get) Token: 0x06006107 RID: 24839 RVA: 0x0013E8AA File Offset: 0x0013CAAA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006108 RID: 24840 RVA: 0x0013E8B2 File Offset: 0x0013CAB2
		private SplitOnEmptyRows(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006109 RID: 24841 RVA: 0x0013E8BB File Offset: 0x0013CABB
		public static SplitOnEmptyRows CreateUnsafe(ProgramNode node)
		{
			return new SplitOnEmptyRows(node);
		}

		// Token: 0x0600610A RID: 24842 RVA: 0x0013E8C4 File Offset: 0x0013CAC4
		public static SplitOnEmptyRows? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.SplitOnEmptyRows)
			{
				return null;
			}
			return new SplitOnEmptyRows?(SplitOnEmptyRows.CreateUnsafe(node));
		}

		// Token: 0x0600610B RID: 24843 RVA: 0x0013E8F9 File Offset: 0x0013CAF9
		public SplitOnEmptyRows(GrammarBuilders g, verticalSheetSection value0)
		{
			this._node = g.Rule.SplitOnEmptyRows.BuildASTNode(value0.Node);
		}

		// Token: 0x0600610C RID: 24844 RVA: 0x0013E918 File Offset: 0x0013CB18
		public static implicit operator horizontalSheetSplits(SplitOnEmptyRows arg)
		{
			return horizontalSheetSplits.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001196 RID: 4502
		// (get) Token: 0x0600610D RID: 24845 RVA: 0x0013E926 File Offset: 0x0013CB26
		public verticalSheetSection verticalSheetSection
		{
			get
			{
				return verticalSheetSection.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600610E RID: 24846 RVA: 0x0013E93A File Offset: 0x0013CB3A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600610F RID: 24847 RVA: 0x0013E950 File Offset: 0x0013CB50
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006110 RID: 24848 RVA: 0x0013E97A File Offset: 0x0013CB7A
		public bool Equals(SplitOnEmptyRows other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BD7 RID: 11223
		private ProgramNode _node;
	}
}
