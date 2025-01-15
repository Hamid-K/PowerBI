using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes
{
	// Token: 0x02000E23 RID: 3619
	public struct TrimTopFullWidthMergedCellRows : IProgramNodeBuilder, IEquatable<TrimTopFullWidthMergedCellRows>
	{
		// Token: 0x1700117D RID: 4477
		// (get) Token: 0x0600609F RID: 24735 RVA: 0x0013DF36 File Offset: 0x0013C136
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060060A0 RID: 24736 RVA: 0x0013DF3E File Offset: 0x0013C13E
		private TrimTopFullWidthMergedCellRows(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060060A1 RID: 24737 RVA: 0x0013DF47 File Offset: 0x0013C147
		public static TrimTopFullWidthMergedCellRows CreateUnsafe(ProgramNode node)
		{
			return new TrimTopFullWidthMergedCellRows(node);
		}

		// Token: 0x060060A2 RID: 24738 RVA: 0x0013DF50 File Offset: 0x0013C150
		public static TrimTopFullWidthMergedCellRows? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.TrimTopFullWidthMergedCellRows)
			{
				return null;
			}
			return new TrimTopFullWidthMergedCellRows?(TrimTopFullWidthMergedCellRows.CreateUnsafe(node));
		}

		// Token: 0x060060A3 RID: 24739 RVA: 0x0013DF85 File Offset: 0x0013C185
		public TrimTopFullWidthMergedCellRows(GrammarBuilders g, sheetSection value0)
		{
			this._node = g.Rule.TrimTopFullWidthMergedCellRows.BuildASTNode(value0.Node);
		}

		// Token: 0x060060A4 RID: 24740 RVA: 0x0013DFA4 File Offset: 0x0013C1A4
		public static implicit operator trimTop(TrimTopFullWidthMergedCellRows arg)
		{
			return trimTop.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700117E RID: 4478
		// (get) Token: 0x060060A5 RID: 24741 RVA: 0x0013DFB2 File Offset: 0x0013C1B2
		public sheetSection sheetSection
		{
			get
			{
				return sheetSection.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x060060A6 RID: 24742 RVA: 0x0013DFC6 File Offset: 0x0013C1C6
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060060A7 RID: 24743 RVA: 0x0013DFDC File Offset: 0x0013C1DC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060060A8 RID: 24744 RVA: 0x0013E006 File Offset: 0x0013C206
		public bool Equals(TrimTopFullWidthMergedCellRows other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BCD RID: 11213
		private ProgramNode _node;
	}
}
