using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x02001864 RID: 6244
	internal class PythonDivideFloor : FormulaDivide
	{
		// Token: 0x0600CBF6 RID: 52214 RVA: 0x002B48B3 File Offset: 0x002B2AB3
		public PythonDivideFloor(FormulaExpression left, FormulaExpression right)
			: base(left, right, 6)
		{
		}

		// Token: 0x17002267 RID: 8807
		// (get) Token: 0x0600CBF7 RID: 52215 RVA: 0x002B8C52 File Offset: 0x002B6E52
		public override string Symbol
		{
			get
			{
				return "//";
			}
		}

		// Token: 0x0600CBF8 RID: 52216 RVA: 0x002B8C59 File Offset: 0x002B6E59
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PythonDivideFloor(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
