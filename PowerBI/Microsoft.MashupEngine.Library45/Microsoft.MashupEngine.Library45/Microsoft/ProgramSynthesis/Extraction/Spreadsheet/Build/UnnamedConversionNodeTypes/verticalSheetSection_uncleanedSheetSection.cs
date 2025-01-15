using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02000E0E RID: 3598
	public struct verticalSheetSection_uncleanedSheetSection : IProgramNodeBuilder, IEquatable<verticalSheetSection_uncleanedSheetSection>
	{
		// Token: 0x17001152 RID: 4434
		// (get) Token: 0x06005FCC RID: 24524 RVA: 0x0013CC6A File Offset: 0x0013AE6A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06005FCD RID: 24525 RVA: 0x0013CC72 File Offset: 0x0013AE72
		private verticalSheetSection_uncleanedSheetSection(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06005FCE RID: 24526 RVA: 0x0013CC7B File Offset: 0x0013AE7B
		public static verticalSheetSection_uncleanedSheetSection CreateUnsafe(ProgramNode node)
		{
			return new verticalSheetSection_uncleanedSheetSection(node);
		}

		// Token: 0x06005FCF RID: 24527 RVA: 0x0013CC84 File Offset: 0x0013AE84
		public static verticalSheetSection_uncleanedSheetSection? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.verticalSheetSection_uncleanedSheetSection)
			{
				return null;
			}
			return new verticalSheetSection_uncleanedSheetSection?(verticalSheetSection_uncleanedSheetSection.CreateUnsafe(node));
		}

		// Token: 0x06005FD0 RID: 24528 RVA: 0x0013CCB9 File Offset: 0x0013AEB9
		public verticalSheetSection_uncleanedSheetSection(GrammarBuilders g, uncleanedSheetSection value0)
		{
			this._node = g.UnnamedConversion.verticalSheetSection_uncleanedSheetSection.BuildASTNode(value0.Node);
		}

		// Token: 0x06005FD1 RID: 24529 RVA: 0x0013CCD8 File Offset: 0x0013AED8
		public static implicit operator verticalSheetSection(verticalSheetSection_uncleanedSheetSection arg)
		{
			return verticalSheetSection.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001153 RID: 4435
		// (get) Token: 0x06005FD2 RID: 24530 RVA: 0x0013CCE6 File Offset: 0x0013AEE6
		public uncleanedSheetSection uncleanedSheetSection
		{
			get
			{
				return uncleanedSheetSection.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06005FD3 RID: 24531 RVA: 0x0013CCFA File Offset: 0x0013AEFA
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06005FD4 RID: 24532 RVA: 0x0013CD10 File Offset: 0x0013AF10
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06005FD5 RID: 24533 RVA: 0x0013CD3A File Offset: 0x0013AF3A
		public bool Equals(verticalSheetSection_uncleanedSheetSection other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BB8 RID: 11192
		private ProgramNode _node;
	}
}
