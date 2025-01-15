using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes
{
	// Token: 0x02000E5C RID: 3676
	public struct wholeSheetFull : IProgramNodeBuilder, IEquatable<wholeSheetFull>
	{
		// Token: 0x170011F4 RID: 4596
		// (get) Token: 0x06006367 RID: 25447 RVA: 0x001436EA File Offset: 0x001418EA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006368 RID: 25448 RVA: 0x001436F2 File Offset: 0x001418F2
		private wholeSheetFull(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006369 RID: 25449 RVA: 0x001436FB File Offset: 0x001418FB
		public static wholeSheetFull CreateUnsafe(ProgramNode node)
		{
			return new wholeSheetFull(node);
		}

		// Token: 0x0600636A RID: 25450 RVA: 0x00143704 File Offset: 0x00141904
		public static wholeSheetFull? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.wholeSheetFull)
			{
				return null;
			}
			return new wholeSheetFull?(wholeSheetFull.CreateUnsafe(node));
		}

		// Token: 0x0600636B RID: 25451 RVA: 0x0014373E File Offset: 0x0014193E
		public static wholeSheetFull CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new wholeSheetFull(new Hole(g.Symbol.wholeSheetFull, holeId));
		}

		// Token: 0x0600636C RID: 25452 RVA: 0x00143756 File Offset: 0x00141956
		public WholeSheet Cast_WholeSheet()
		{
			return WholeSheet.CreateUnsafe(this.Node);
		}

		// Token: 0x0600636D RID: 25453 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_WholeSheet(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x0600636E RID: 25454 RVA: 0x00143763 File Offset: 0x00141963
		public bool Is_WholeSheet(GrammarBuilders g, out WholeSheet value)
		{
			value = WholeSheet.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x0600636F RID: 25455 RVA: 0x00143777 File Offset: 0x00141977
		public WholeSheet? As_WholeSheet(GrammarBuilders g)
		{
			return new WholeSheet?(WholeSheet.CreateUnsafe(this.Node));
		}

		// Token: 0x06006370 RID: 25456 RVA: 0x00143789 File Offset: 0x00141989
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006371 RID: 25457 RVA: 0x0014379C File Offset: 0x0014199C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006372 RID: 25458 RVA: 0x001437C6 File Offset: 0x001419C6
		public bool Equals(wholeSheetFull other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002C06 RID: 11270
		private ProgramNode _node;
	}
}
