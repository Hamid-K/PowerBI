using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerAutomate
{
	// Token: 0x02001908 RID: 6408
	internal class PowerAutomateAdd : PowerAutomateFunc
	{
		// Token: 0x0600D0F2 RID: 53490 RVA: 0x002C8E95 File Offset: 0x002C7095
		public PowerAutomateAdd(FormulaExpression left, FormulaExpression right)
			: base("Add", new FormulaExpression[] { left, right })
		{
		}

		// Token: 0x170022EC RID: 8940
		// (get) Token: 0x0600D0F3 RID: 53491 RVA: 0x002C8E5A File Offset: 0x002C705A
		public FormulaExpression Left
		{
			get
			{
				return base.Children[0];
			}
		}

		// Token: 0x170022ED RID: 8941
		// (get) Token: 0x0600D0F4 RID: 53492 RVA: 0x002C8E68 File Offset: 0x002C7068
		public FormulaExpression Right
		{
			get
			{
				return base.Children[1];
			}
		}

		// Token: 0x0600D0F5 RID: 53493 RVA: 0x002C8EB0 File Offset: 0x002C70B0
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PowerAutomateAdd(this.Left.Accept<FormulaExpression>(visitor), this.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
