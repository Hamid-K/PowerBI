using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation
{
	// Token: 0x020017F0 RID: 6128
	internal abstract class FormulaDateTimeLiteral : FormulaExpression, IFormulaLiteral<DateTime>, IFormulaTyped
	{
		// Token: 0x0600C9C4 RID: 51652 RVA: 0x002B2CB2 File Offset: 0x002B0EB2
		protected FormulaDateTimeLiteral(DateTime value)
		{
			this.Value = value;
			base.Children = new FormulaExpression[0];
		}

		// Token: 0x170021FA RID: 8698
		// (get) Token: 0x0600C9C5 RID: 51653 RVA: 0x002B2CCD File Offset: 0x002B0ECD
		public Type Type
		{
			get
			{
				return typeof(DateTime);
			}
		}

		// Token: 0x170021FB RID: 8699
		// (get) Token: 0x0600C9C6 RID: 51654 RVA: 0x002B2CD9 File Offset: 0x002B0ED9
		public DateTime Value { get; }
	}
}
