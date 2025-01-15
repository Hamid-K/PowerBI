using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x0200185C RID: 6236
	internal class PythonDateQuarter : FormulaExpression
	{
		// Token: 0x17002263 RID: 8803
		// (get) Token: 0x0600CBDE RID: 52190 RVA: 0x002B8A5F File Offset: 0x002B6C5F
		public FormulaExpression Subject { get; }

		// Token: 0x0600CBDF RID: 52191 RVA: 0x002B8A67 File Offset: 0x002B6C67
		public PythonDateQuarter(FormulaExpression subject)
		{
			this.Subject = subject;
			base.Children = new FormulaExpression[] { PythonExpressionHelper.Plus1(PythonExpressionHelper.DivideFloor(PythonExpressionHelper.Minus1(PythonExpressionHelper.Month(subject)), 3)) };
		}

		// Token: 0x0600CBE0 RID: 52192 RVA: 0x002B8A9B File Offset: 0x002B6C9B
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PythonDateQuarter(this.Subject.Accept<FormulaExpression>(visitor));
		}

		// Token: 0x0600CBE1 RID: 52193 RVA: 0x002B8AAE File Offset: 0x002B6CAE
		protected override string ToCodeString()
		{
			return base.Children[0].ToString();
		}
	}
}
