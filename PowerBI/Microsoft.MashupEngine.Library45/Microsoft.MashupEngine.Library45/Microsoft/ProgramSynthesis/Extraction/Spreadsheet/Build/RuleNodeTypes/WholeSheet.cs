using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes
{
	// Token: 0x02000E2B RID: 3627
	public struct WholeSheet : IProgramNodeBuilder, IEquatable<WholeSheet>
	{
		// Token: 0x17001191 RID: 4497
		// (get) Token: 0x060060F3 RID: 24819 RVA: 0x0013E6E2 File Offset: 0x0013C8E2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060060F4 RID: 24820 RVA: 0x0013E6EA File Offset: 0x0013C8EA
		private WholeSheet(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060060F5 RID: 24821 RVA: 0x0013E6F3 File Offset: 0x0013C8F3
		public static WholeSheet CreateUnsafe(ProgramNode node)
		{
			return new WholeSheet(node);
		}

		// Token: 0x060060F6 RID: 24822 RVA: 0x0013E6FC File Offset: 0x0013C8FC
		public static WholeSheet? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.WholeSheet)
			{
				return null;
			}
			return new WholeSheet?(WholeSheet.CreateUnsafe(node));
		}

		// Token: 0x060060F7 RID: 24823 RVA: 0x0013E731 File Offset: 0x0013C931
		public WholeSheet(GrammarBuilders g, sheet value0)
		{
			this._node = g.Rule.WholeSheet.BuildASTNode(value0.Node);
		}

		// Token: 0x060060F8 RID: 24824 RVA: 0x0013E750 File Offset: 0x0013C950
		public static implicit operator wholeSheetFull(WholeSheet arg)
		{
			return wholeSheetFull.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001192 RID: 4498
		// (get) Token: 0x060060F9 RID: 24825 RVA: 0x0013E75E File Offset: 0x0013C95E
		public sheet sheet
		{
			get
			{
				return sheet.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x060060FA RID: 24826 RVA: 0x0013E772 File Offset: 0x0013C972
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060060FB RID: 24827 RVA: 0x0013E788 File Offset: 0x0013C988
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060060FC RID: 24828 RVA: 0x0013E7B2 File Offset: 0x0013C9B2
		public bool Equals(WholeSheet other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BD5 RID: 11221
		private ProgramNode _node;
	}
}
