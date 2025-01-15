using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes
{
	// Token: 0x02000E2A RID: 3626
	public struct TrimHiddenWholeSheet : IProgramNodeBuilder, IEquatable<TrimHiddenWholeSheet>
	{
		// Token: 0x1700118F RID: 4495
		// (get) Token: 0x060060E9 RID: 24809 RVA: 0x0013E5FE File Offset: 0x0013C7FE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060060EA RID: 24810 RVA: 0x0013E606 File Offset: 0x0013C806
		private TrimHiddenWholeSheet(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060060EB RID: 24811 RVA: 0x0013E60F File Offset: 0x0013C80F
		public static TrimHiddenWholeSheet CreateUnsafe(ProgramNode node)
		{
			return new TrimHiddenWholeSheet(node);
		}

		// Token: 0x060060EC RID: 24812 RVA: 0x0013E618 File Offset: 0x0013C818
		public static TrimHiddenWholeSheet? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.TrimHiddenWholeSheet)
			{
				return null;
			}
			return new TrimHiddenWholeSheet?(TrimHiddenWholeSheet.CreateUnsafe(node));
		}

		// Token: 0x060060ED RID: 24813 RVA: 0x0013E64D File Offset: 0x0013C84D
		public TrimHiddenWholeSheet(GrammarBuilders g, wholeSheetFull value0)
		{
			this._node = g.Rule.TrimHiddenWholeSheet.BuildASTNode(value0.Node);
		}

		// Token: 0x060060EE RID: 24814 RVA: 0x0013E66C File Offset: 0x0013C86C
		public static implicit operator wholeSheet(TrimHiddenWholeSheet arg)
		{
			return wholeSheet.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001190 RID: 4496
		// (get) Token: 0x060060EF RID: 24815 RVA: 0x0013E67A File Offset: 0x0013C87A
		public wholeSheetFull wholeSheetFull
		{
			get
			{
				return wholeSheetFull.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x060060F0 RID: 24816 RVA: 0x0013E68E File Offset: 0x0013C88E
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060060F1 RID: 24817 RVA: 0x0013E6A4 File Offset: 0x0013C8A4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060060F2 RID: 24818 RVA: 0x0013E6CE File Offset: 0x0013C8CE
		public bool Equals(TrimHiddenWholeSheet other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BD4 RID: 11220
		private ProgramNode _node;
	}
}
