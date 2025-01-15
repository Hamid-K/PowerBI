using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x0200185D RID: 6237
	internal class PythonWeekDay : FormulaExpression
	{
		// Token: 0x17002264 RID: 8804
		// (get) Token: 0x0600CBE2 RID: 52194 RVA: 0x002B8AC1 File Offset: 0x002B6CC1
		public FormulaExpression Subject { get; }

		// Token: 0x0600CBE3 RID: 52195 RVA: 0x002B8AC9 File Offset: 0x002B6CC9
		public PythonWeekDay(FormulaExpression subject)
		{
			this.Subject = subject;
			base.Children = new FormulaExpression[] { PythonExpressionHelper.Plus(PythonExpressionHelper.Modulo(PythonExpressionHelper.DotFunc<int>(subject, "isoweekday"), 7), 1.0) };
		}

		// Token: 0x0600CBE4 RID: 52196 RVA: 0x002B8B06 File Offset: 0x002B6D06
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PythonWeekDay(this.Subject.Accept<FormulaExpression>(visitor));
		}

		// Token: 0x0600CBE5 RID: 52197 RVA: 0x002B8AAE File Offset: 0x002B6CAE
		protected override string ToCodeString()
		{
			return base.Children[0].ToString();
		}
	}
}
