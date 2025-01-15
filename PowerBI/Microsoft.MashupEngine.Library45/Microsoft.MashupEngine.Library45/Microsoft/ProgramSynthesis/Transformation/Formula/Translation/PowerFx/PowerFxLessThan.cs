using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerFx
{
	// Token: 0x020018E5 RID: 6373
	internal class PowerFxLessThan : FormulaBinaryOperator
	{
		// Token: 0x0600CFA6 RID: 53158 RVA: 0x002B8C78 File Offset: 0x002B6E78
		public PowerFxLessThan(FormulaExpression left, FormulaExpression right)
			: base(left, right, 0, false, false)
		{
		}

		// Token: 0x170022D3 RID: 8915
		// (get) Token: 0x0600CFA7 RID: 53159 RVA: 0x002B8F5E File Offset: 0x002B715E
		public override string Symbol
		{
			get
			{
				return "<";
			}
		}

		// Token: 0x0600CFA8 RID: 53160 RVA: 0x002C4419 File Offset: 0x002C2619
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PowerFxLessThan(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
