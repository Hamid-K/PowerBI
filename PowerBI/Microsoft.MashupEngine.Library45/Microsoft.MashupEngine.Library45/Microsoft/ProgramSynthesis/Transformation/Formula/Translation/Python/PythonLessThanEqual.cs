using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x02001874 RID: 6260
	internal class PythonLessThanEqual : FormulaBinaryOperator
	{
		// Token: 0x0600CC2A RID: 52266 RVA: 0x002B8C78 File Offset: 0x002B6E78
		public PythonLessThanEqual(FormulaExpression left, FormulaExpression right)
			: base(left, right, 0, false, false)
		{
		}

		// Token: 0x17002278 RID: 8824
		// (get) Token: 0x0600CC2B RID: 52267 RVA: 0x002B8F84 File Offset: 0x002B7184
		public override string Symbol
		{
			get
			{
				return "<=";
			}
		}

		// Token: 0x0600CC2C RID: 52268 RVA: 0x002B8F8B File Offset: 0x002B718B
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PythonLessThanEqual(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
