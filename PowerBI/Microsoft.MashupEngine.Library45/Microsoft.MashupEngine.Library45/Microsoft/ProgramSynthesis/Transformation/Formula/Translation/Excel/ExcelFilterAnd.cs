using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Excel
{
	// Token: 0x0200192E RID: 6446
	internal class ExcelFilterAnd : FormulaExpression
	{
		// Token: 0x0600D21F RID: 53791 RVA: 0x002CC790 File Offset: 0x002CA990
		public ExcelFilterAnd(FormulaExpression left, FormulaExpression right)
		{
			this.Left = left;
			this.Right = right;
			base.Children = new FormulaExpression[] { this.Left, this.Right };
		}

		// Token: 0x17002301 RID: 8961
		// (get) Token: 0x0600D220 RID: 53792 RVA: 0x002CC7C4 File Offset: 0x002CA9C4
		public FormulaExpression Left { get; }

		// Token: 0x17002302 RID: 8962
		// (get) Token: 0x0600D221 RID: 53793 RVA: 0x002CC7CC File Offset: 0x002CA9CC
		public FormulaExpression Right { get; }

		// Token: 0x0600D222 RID: 53794 RVA: 0x002CC7D4 File Offset: 0x002CA9D4
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new ExcelFilterAnd(this.Left.Accept<FormulaExpression>(visitor), this.Right.Accept<FormulaExpression>(visitor));
		}

		// Token: 0x0600D223 RID: 53795 RVA: 0x002CC7F3 File Offset: 0x002CA9F3
		protected override string ToCodeString()
		{
			return string.Format("({0}) * ({1})", this.Left, this.Right);
		}
	}
}
