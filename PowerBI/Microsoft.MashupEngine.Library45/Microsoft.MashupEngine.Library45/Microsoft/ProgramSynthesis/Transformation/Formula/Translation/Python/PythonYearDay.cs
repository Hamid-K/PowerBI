using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x0200185F RID: 6239
	internal class PythonYearDay : FormulaExpression
	{
		// Token: 0x17002266 RID: 8806
		// (get) Token: 0x0600CBEA RID: 52202 RVA: 0x002B8B88 File Offset: 0x002B6D88
		public FormulaExpression Subject { get; }

		// Token: 0x0600CBEB RID: 52203 RVA: 0x002B8B90 File Offset: 0x002B6D90
		public PythonYearDay(FormulaExpression subject)
		{
			this.Subject = subject;
			base.Children = new FormulaExpression[] { PythonExpressionHelper.Dot<int>(PythonExpressionHelper.DotFunc<object>(subject, "timetuple"), "tm_yday") };
		}

		// Token: 0x0600CBEC RID: 52204 RVA: 0x002B8BC3 File Offset: 0x002B6DC3
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PythonYearDay(this.Subject.Accept<FormulaExpression>(visitor));
		}

		// Token: 0x0600CBED RID: 52205 RVA: 0x002B8AAE File Offset: 0x002B6CAE
		protected override string ToCodeString()
		{
			return base.Children[0].ToString();
		}
	}
}
