using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02000E0D RID: 3597
	public struct horizontalSheetSection_verticalSheetSection : IProgramNodeBuilder, IEquatable<horizontalSheetSection_verticalSheetSection>
	{
		// Token: 0x17001150 RID: 4432
		// (get) Token: 0x06005FC2 RID: 24514 RVA: 0x0013CB86 File Offset: 0x0013AD86
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06005FC3 RID: 24515 RVA: 0x0013CB8E File Offset: 0x0013AD8E
		private horizontalSheetSection_verticalSheetSection(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06005FC4 RID: 24516 RVA: 0x0013CB97 File Offset: 0x0013AD97
		public static horizontalSheetSection_verticalSheetSection CreateUnsafe(ProgramNode node)
		{
			return new horizontalSheetSection_verticalSheetSection(node);
		}

		// Token: 0x06005FC5 RID: 24517 RVA: 0x0013CBA0 File Offset: 0x0013ADA0
		public static horizontalSheetSection_verticalSheetSection? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.horizontalSheetSection_verticalSheetSection)
			{
				return null;
			}
			return new horizontalSheetSection_verticalSheetSection?(horizontalSheetSection_verticalSheetSection.CreateUnsafe(node));
		}

		// Token: 0x06005FC6 RID: 24518 RVA: 0x0013CBD5 File Offset: 0x0013ADD5
		public horizontalSheetSection_verticalSheetSection(GrammarBuilders g, verticalSheetSection value0)
		{
			this._node = g.UnnamedConversion.horizontalSheetSection_verticalSheetSection.BuildASTNode(value0.Node);
		}

		// Token: 0x06005FC7 RID: 24519 RVA: 0x0013CBF4 File Offset: 0x0013ADF4
		public static implicit operator horizontalSheetSection(horizontalSheetSection_verticalSheetSection arg)
		{
			return horizontalSheetSection.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001151 RID: 4433
		// (get) Token: 0x06005FC8 RID: 24520 RVA: 0x0013CC02 File Offset: 0x0013AE02
		public verticalSheetSection verticalSheetSection
		{
			get
			{
				return verticalSheetSection.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06005FC9 RID: 24521 RVA: 0x0013CC16 File Offset: 0x0013AE16
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06005FCA RID: 24522 RVA: 0x0013CC2C File Offset: 0x0013AE2C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06005FCB RID: 24523 RVA: 0x0013CC56 File Offset: 0x0013AE56
		public bool Equals(horizontalSheetSection_verticalSheetSection other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BB7 RID: 11191
		private ProgramNode _node;
	}
}
