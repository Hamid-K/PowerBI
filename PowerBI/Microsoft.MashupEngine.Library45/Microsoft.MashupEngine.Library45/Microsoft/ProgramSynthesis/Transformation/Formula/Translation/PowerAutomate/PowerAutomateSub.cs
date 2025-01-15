using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerAutomate
{
	// Token: 0x02001907 RID: 6407
	internal class PowerAutomateSub : PowerAutomateFunc
	{
		// Token: 0x0600D0EE RID: 53486 RVA: 0x002C8E3F File Offset: 0x002C703F
		public PowerAutomateSub(FormulaExpression left, FormulaExpression right)
			: base("Sub", new FormulaExpression[] { left, right })
		{
		}

		// Token: 0x170022EA RID: 8938
		// (get) Token: 0x0600D0EF RID: 53487 RVA: 0x002C8E5A File Offset: 0x002C705A
		public FormulaExpression Left
		{
			get
			{
				return base.Children[0];
			}
		}

		// Token: 0x170022EB RID: 8939
		// (get) Token: 0x0600D0F0 RID: 53488 RVA: 0x002C8E68 File Offset: 0x002C7068
		public FormulaExpression Right
		{
			get
			{
				return base.Children[1];
			}
		}

		// Token: 0x0600D0F1 RID: 53489 RVA: 0x002C8E76 File Offset: 0x002C7076
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PowerAutomateSub(this.Left.Accept<FormulaExpression>(visitor), this.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
