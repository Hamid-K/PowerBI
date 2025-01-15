using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Excel
{
	// Token: 0x02001928 RID: 6440
	internal class ExcelRange : FormulaExpression
	{
		// Token: 0x0600D208 RID: 53768 RVA: 0x002CC5C2 File Offset: 0x002CA7C2
		public ExcelRange(FormulaExpression start, FormulaExpression end)
		{
			this.Start = start;
			this.End = end;
			base.Children = new FormulaExpression[] { this.Start, this.End };
		}

		// Token: 0x170022FC RID: 8956
		// (get) Token: 0x0600D209 RID: 53769 RVA: 0x002CC5F6 File Offset: 0x002CA7F6
		public FormulaExpression End { get; }

		// Token: 0x170022FD RID: 8957
		// (get) Token: 0x0600D20A RID: 53770 RVA: 0x002CC5FE File Offset: 0x002CA7FE
		// (set) Token: 0x0600D20B RID: 53771 RVA: 0x002CC606 File Offset: 0x002CA806
		public IReadOnlyList<FormulaExpression> Items { get; protected set; }

		// Token: 0x170022FE RID: 8958
		// (get) Token: 0x0600D20C RID: 53772 RVA: 0x002CC60F File Offset: 0x002CA80F
		public FormulaExpression Start { get; }

		// Token: 0x0600D20D RID: 53773 RVA: 0x002CC617 File Offset: 0x002CA817
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new ExcelRange(this.Start.Accept<FormulaExpression>(visitor), this.End.Accept<FormulaExpression>(visitor));
		}

		// Token: 0x0600D20E RID: 53774 RVA: 0x002CC636 File Offset: 0x002CA836
		protected override string ToCodeString()
		{
			return string.Format("{0}:{1}", this.Start, this.End);
		}
	}
}
