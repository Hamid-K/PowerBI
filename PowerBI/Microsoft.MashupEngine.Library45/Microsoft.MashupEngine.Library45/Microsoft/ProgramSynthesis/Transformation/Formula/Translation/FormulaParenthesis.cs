using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation
{
	// Token: 0x020017ED RID: 6125
	internal class FormulaParenthesis : FormulaExpression, IFormulaTyped
	{
		// Token: 0x0600C9B7 RID: 51639 RVA: 0x002B2B94 File Offset: 0x002B0D94
		public FormulaParenthesis(FormulaExpression body)
		{
			this.Body = body;
			IFormulaTyped formulaTyped = body as IFormulaTyped;
			this.Type = ((formulaTyped != null) ? formulaTyped.Type : typeof(object));
			base.Children = new FormulaExpression[] { body };
		}

		// Token: 0x170021F4 RID: 8692
		// (get) Token: 0x0600C9B8 RID: 51640 RVA: 0x002B2BE0 File Offset: 0x002B0DE0
		public FormulaExpression Body { get; }

		// Token: 0x170021F5 RID: 8693
		// (get) Token: 0x0600C9B9 RID: 51641 RVA: 0x002B2BE8 File Offset: 0x002B0DE8
		public Type Type { get; }

		// Token: 0x0600C9BA RID: 51642 RVA: 0x002B2BF0 File Offset: 0x002B0DF0
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new FormulaParenthesis(this.Body.Accept<FormulaExpression>(visitor));
		}

		// Token: 0x0600C9BB RID: 51643 RVA: 0x002B2C03 File Offset: 0x002B0E03
		protected override string ToCodeString()
		{
			return string.Format("({0})", this.Body);
		}
	}
}
