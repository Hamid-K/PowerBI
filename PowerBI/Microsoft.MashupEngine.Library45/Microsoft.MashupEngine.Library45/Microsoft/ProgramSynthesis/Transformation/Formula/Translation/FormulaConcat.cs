using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation
{
	// Token: 0x020017F6 RID: 6134
	internal abstract class FormulaConcat : FormulaBinaryOperator
	{
		// Token: 0x0600C9DA RID: 51674 RVA: 0x002B3136 File Offset: 0x002B1336
		protected FormulaConcat(FormulaExpression left, FormulaExpression right, int precedence = 6)
			: base(left, right, precedence, false, true)
		{
		}

		// Token: 0x17002208 RID: 8712
		// (get) Token: 0x0600C9DB RID: 51675 RVA: 0x002B317B File Offset: 0x002B137B
		public FormulaExpression Prefix
		{
			get
			{
				return base.Left;
			}
		}

		// Token: 0x17002209 RID: 8713
		// (get) Token: 0x0600C9DC RID: 51676 RVA: 0x002B3183 File Offset: 0x002B1383
		public FormulaExpression Suffix
		{
			get
			{
				return base.Right;
			}
		}
	}
}
