using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02000E0F RID: 3599
	public struct uncleanedSheetSection_wholeSheet : IProgramNodeBuilder, IEquatable<uncleanedSheetSection_wholeSheet>
	{
		// Token: 0x17001154 RID: 4436
		// (get) Token: 0x06005FD6 RID: 24534 RVA: 0x0013CD4E File Offset: 0x0013AF4E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06005FD7 RID: 24535 RVA: 0x0013CD56 File Offset: 0x0013AF56
		private uncleanedSheetSection_wholeSheet(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06005FD8 RID: 24536 RVA: 0x0013CD5F File Offset: 0x0013AF5F
		public static uncleanedSheetSection_wholeSheet CreateUnsafe(ProgramNode node)
		{
			return new uncleanedSheetSection_wholeSheet(node);
		}

		// Token: 0x06005FD9 RID: 24537 RVA: 0x0013CD68 File Offset: 0x0013AF68
		public static uncleanedSheetSection_wholeSheet? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.uncleanedSheetSection_wholeSheet)
			{
				return null;
			}
			return new uncleanedSheetSection_wholeSheet?(uncleanedSheetSection_wholeSheet.CreateUnsafe(node));
		}

		// Token: 0x06005FDA RID: 24538 RVA: 0x0013CD9D File Offset: 0x0013AF9D
		public uncleanedSheetSection_wholeSheet(GrammarBuilders g, wholeSheet value0)
		{
			this._node = g.UnnamedConversion.uncleanedSheetSection_wholeSheet.BuildASTNode(value0.Node);
		}

		// Token: 0x06005FDB RID: 24539 RVA: 0x0013CDBC File Offset: 0x0013AFBC
		public static implicit operator uncleanedSheetSection(uncleanedSheetSection_wholeSheet arg)
		{
			return uncleanedSheetSection.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001155 RID: 4437
		// (get) Token: 0x06005FDC RID: 24540 RVA: 0x0013CDCA File Offset: 0x0013AFCA
		public wholeSheet wholeSheet
		{
			get
			{
				return wholeSheet.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06005FDD RID: 24541 RVA: 0x0013CDDE File Offset: 0x0013AFDE
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06005FDE RID: 24542 RVA: 0x0013CDF4 File Offset: 0x0013AFF4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06005FDF RID: 24543 RVA: 0x0013CE1E File Offset: 0x0013B01E
		public bool Equals(uncleanedSheetSection_wholeSheet other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BB9 RID: 11193
		private ProgramNode _node;
	}
}
