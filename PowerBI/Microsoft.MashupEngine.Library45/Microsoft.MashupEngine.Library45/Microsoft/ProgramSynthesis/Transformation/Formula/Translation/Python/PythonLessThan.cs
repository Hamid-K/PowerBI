using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x02001873 RID: 6259
	internal class PythonLessThan : FormulaBinaryOperator
	{
		// Token: 0x0600CC27 RID: 52263 RVA: 0x002B8C78 File Offset: 0x002B6E78
		public PythonLessThan(FormulaExpression left, FormulaExpression right)
			: base(left, right, 0, false, false)
		{
		}

		// Token: 0x17002277 RID: 8823
		// (get) Token: 0x0600CC28 RID: 52264 RVA: 0x002B8F5E File Offset: 0x002B715E
		public override string Symbol
		{
			get
			{
				return "<";
			}
		}

		// Token: 0x0600CC29 RID: 52265 RVA: 0x002B8F65 File Offset: 0x002B7165
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PythonLessThan(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
