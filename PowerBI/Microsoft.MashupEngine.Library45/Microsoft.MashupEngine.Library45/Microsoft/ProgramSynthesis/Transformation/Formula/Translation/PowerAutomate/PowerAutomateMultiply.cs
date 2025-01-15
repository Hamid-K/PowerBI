using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerAutomate
{
	// Token: 0x0200190A RID: 6410
	internal class PowerAutomateMultiply : PowerAutomateFunc
	{
		// Token: 0x0600D0FC RID: 53500 RVA: 0x002C8F4A File Offset: 0x002C714A
		public PowerAutomateMultiply(FormulaExpression left, FormulaExpression right)
			: base("Mul", new FormulaExpression[] { left, right })
		{
		}

		// Token: 0x170022F1 RID: 8945
		// (get) Token: 0x0600D0FD RID: 53501 RVA: 0x002C8E5A File Offset: 0x002C705A
		public FormulaExpression Left
		{
			get
			{
				return base.Children[0];
			}
		}

		// Token: 0x170022F2 RID: 8946
		// (get) Token: 0x0600D0FE RID: 53502 RVA: 0x002C8E68 File Offset: 0x002C7068
		public FormulaExpression Right
		{
			get
			{
				return base.Children[1];
			}
		}

		// Token: 0x0600D0FF RID: 53503 RVA: 0x002C8F65 File Offset: 0x002C7165
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PowerAutomateMultiply(this.Left.Accept<FormulaExpression>(visitor), this.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
