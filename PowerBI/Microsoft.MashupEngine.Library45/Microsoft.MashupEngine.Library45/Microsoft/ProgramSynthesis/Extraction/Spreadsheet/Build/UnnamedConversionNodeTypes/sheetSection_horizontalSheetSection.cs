using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02000E0C RID: 3596
	public struct sheetSection_horizontalSheetSection : IProgramNodeBuilder, IEquatable<sheetSection_horizontalSheetSection>
	{
		// Token: 0x1700114E RID: 4430
		// (get) Token: 0x06005FB8 RID: 24504 RVA: 0x0013CAA2 File Offset: 0x0013ACA2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06005FB9 RID: 24505 RVA: 0x0013CAAA File Offset: 0x0013ACAA
		private sheetSection_horizontalSheetSection(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06005FBA RID: 24506 RVA: 0x0013CAB3 File Offset: 0x0013ACB3
		public static sheetSection_horizontalSheetSection CreateUnsafe(ProgramNode node)
		{
			return new sheetSection_horizontalSheetSection(node);
		}

		// Token: 0x06005FBB RID: 24507 RVA: 0x0013CABC File Offset: 0x0013ACBC
		public static sheetSection_horizontalSheetSection? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.sheetSection_horizontalSheetSection)
			{
				return null;
			}
			return new sheetSection_horizontalSheetSection?(sheetSection_horizontalSheetSection.CreateUnsafe(node));
		}

		// Token: 0x06005FBC RID: 24508 RVA: 0x0013CAF1 File Offset: 0x0013ACF1
		public sheetSection_horizontalSheetSection(GrammarBuilders g, horizontalSheetSection value0)
		{
			this._node = g.UnnamedConversion.sheetSection_horizontalSheetSection.BuildASTNode(value0.Node);
		}

		// Token: 0x06005FBD RID: 24509 RVA: 0x0013CB10 File Offset: 0x0013AD10
		public static implicit operator sheetSection(sheetSection_horizontalSheetSection arg)
		{
			return sheetSection.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700114F RID: 4431
		// (get) Token: 0x06005FBE RID: 24510 RVA: 0x0013CB1E File Offset: 0x0013AD1E
		public horizontalSheetSection horizontalSheetSection
		{
			get
			{
				return horizontalSheetSection.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06005FBF RID: 24511 RVA: 0x0013CB32 File Offset: 0x0013AD32
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06005FC0 RID: 24512 RVA: 0x0013CB48 File Offset: 0x0013AD48
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06005FC1 RID: 24513 RVA: 0x0013CB72 File Offset: 0x0013AD72
		public bool Equals(sheetSection_horizontalSheetSection other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BB6 RID: 11190
		private ProgramNode _node;
	}
}
