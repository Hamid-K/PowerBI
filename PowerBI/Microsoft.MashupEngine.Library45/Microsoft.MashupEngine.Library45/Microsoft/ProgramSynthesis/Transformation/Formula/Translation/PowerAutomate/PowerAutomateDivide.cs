using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerAutomate
{
	// Token: 0x0200190B RID: 6411
	internal class PowerAutomateDivide : PowerAutomateFunc
	{
		// Token: 0x0600D100 RID: 53504 RVA: 0x002C8F84 File Offset: 0x002C7184
		public PowerAutomateDivide(FormulaExpression left, FormulaExpression right)
			: base("Div", new FormulaExpression[] { left, right })
		{
		}

		// Token: 0x170022F3 RID: 8947
		// (get) Token: 0x0600D101 RID: 53505 RVA: 0x002C8E5A File Offset: 0x002C705A
		public FormulaExpression Left
		{
			get
			{
				return base.Children[0];
			}
		}

		// Token: 0x170022F4 RID: 8948
		// (get) Token: 0x0600D102 RID: 53506 RVA: 0x002C8E68 File Offset: 0x002C7068
		public FormulaExpression Right
		{
			get
			{
				return base.Children[1];
			}
		}

		// Token: 0x0600D103 RID: 53507 RVA: 0x002C8F9F File Offset: 0x002C719F
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PowerAutomateDivide(this.Left.Accept<FormulaExpression>(visitor), this.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
