using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes
{
	// Token: 0x02000E22 RID: 3618
	public struct TrimTopMergedCellRows : IProgramNodeBuilder, IEquatable<TrimTopMergedCellRows>
	{
		// Token: 0x1700117B RID: 4475
		// (get) Token: 0x06006095 RID: 24725 RVA: 0x0013DE52 File Offset: 0x0013C052
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006096 RID: 24726 RVA: 0x0013DE5A File Offset: 0x0013C05A
		private TrimTopMergedCellRows(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006097 RID: 24727 RVA: 0x0013DE63 File Offset: 0x0013C063
		public static TrimTopMergedCellRows CreateUnsafe(ProgramNode node)
		{
			return new TrimTopMergedCellRows(node);
		}

		// Token: 0x06006098 RID: 24728 RVA: 0x0013DE6C File Offset: 0x0013C06C
		public static TrimTopMergedCellRows? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.TrimTopMergedCellRows)
			{
				return null;
			}
			return new TrimTopMergedCellRows?(TrimTopMergedCellRows.CreateUnsafe(node));
		}

		// Token: 0x06006099 RID: 24729 RVA: 0x0013DEA1 File Offset: 0x0013C0A1
		public TrimTopMergedCellRows(GrammarBuilders g, sheetSection value0)
		{
			this._node = g.Rule.TrimTopMergedCellRows.BuildASTNode(value0.Node);
		}

		// Token: 0x0600609A RID: 24730 RVA: 0x0013DEC0 File Offset: 0x0013C0C0
		public static implicit operator trimTop(TrimTopMergedCellRows arg)
		{
			return trimTop.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700117C RID: 4476
		// (get) Token: 0x0600609B RID: 24731 RVA: 0x0013DECE File Offset: 0x0013C0CE
		public sheetSection sheetSection
		{
			get
			{
				return sheetSection.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600609C RID: 24732 RVA: 0x0013DEE2 File Offset: 0x0013C0E2
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600609D RID: 24733 RVA: 0x0013DEF8 File Offset: 0x0013C0F8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600609E RID: 24734 RVA: 0x0013DF22 File Offset: 0x0013C122
		public bool Equals(TrimTopMergedCellRows other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BCC RID: 11212
		private ProgramNode _node;
	}
}
