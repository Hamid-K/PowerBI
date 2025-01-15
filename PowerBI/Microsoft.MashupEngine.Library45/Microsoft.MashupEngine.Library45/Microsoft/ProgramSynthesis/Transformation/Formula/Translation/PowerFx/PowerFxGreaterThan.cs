using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerFx
{
	// Token: 0x020018E3 RID: 6371
	internal class PowerFxGreaterThan : FormulaBinaryOperator
	{
		// Token: 0x0600CFA0 RID: 53152 RVA: 0x002B8C78 File Offset: 0x002B6E78
		public PowerFxGreaterThan(FormulaExpression left, FormulaExpression right)
			: base(left, right, 0, false, false)
		{
		}

		// Token: 0x170022D1 RID: 8913
		// (get) Token: 0x0600CFA1 RID: 53153 RVA: 0x002B8F12 File Offset: 0x002B7112
		public override string Symbol
		{
			get
			{
				return ">";
			}
		}

		// Token: 0x0600CFA2 RID: 53154 RVA: 0x002C43DB File Offset: 0x002C25DB
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PowerFxGreaterThan(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
