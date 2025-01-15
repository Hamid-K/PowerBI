using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerFx
{
	// Token: 0x020018E1 RID: 6369
	internal class PowerFxAnd : FormulaBinaryOperator
	{
		// Token: 0x0600CF9A RID: 53146 RVA: 0x002B8C78 File Offset: 0x002B6E78
		public PowerFxAnd(FormulaExpression left, FormulaExpression right)
			: base(left, right, 0, false, false)
		{
		}

		// Token: 0x170022CF RID: 8911
		// (get) Token: 0x0600CF9B RID: 53147 RVA: 0x002C43AE File Offset: 0x002C25AE
		public override string Symbol
		{
			get
			{
				return "&&";
			}
		}

		// Token: 0x0600CF9C RID: 53148 RVA: 0x002C43B5 File Offset: 0x002C25B5
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PowerFxAnd(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
