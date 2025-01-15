using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x0200185E RID: 6238
	internal class PythonMonthDays : FormulaExpression
	{
		// Token: 0x17002265 RID: 8805
		// (get) Token: 0x0600CBE6 RID: 52198 RVA: 0x002B8B19 File Offset: 0x002B6D19
		public FormulaExpression Subject { get; }

		// Token: 0x0600CBE7 RID: 52199 RVA: 0x002B8B24 File Offset: 0x002B6D24
		public PythonMonthDays(FormulaExpression subject)
		{
			this.Subject = subject;
			base.Children = new FormulaExpression[] { PythonExpressionHelper.Index<int>(PythonExpressionHelper.Func<object>("calendar.monthrange", new FormulaExpression[]
			{
				PythonExpressionHelper.Year(subject),
				PythonExpressionHelper.Month(subject)
			}), 1) };
		}

		// Token: 0x0600CBE8 RID: 52200 RVA: 0x002B8B75 File Offset: 0x002B6D75
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PythonMonthDays(this.Subject.Accept<FormulaExpression>(visitor));
		}

		// Token: 0x0600CBE9 RID: 52201 RVA: 0x002B8AAE File Offset: 0x002B6CAE
		protected override string ToCodeString()
		{
			return base.Children[0].ToString();
		}
	}
}
