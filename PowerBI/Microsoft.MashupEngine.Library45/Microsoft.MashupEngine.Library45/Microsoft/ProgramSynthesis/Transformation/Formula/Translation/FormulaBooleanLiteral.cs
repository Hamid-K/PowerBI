using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation
{
	// Token: 0x020017EF RID: 6127
	internal abstract class FormulaBooleanLiteral : FormulaExpression, IFormulaLiteral<bool>, IFormulaTyped
	{
		// Token: 0x0600C9C1 RID: 51649 RVA: 0x002B2C83 File Offset: 0x002B0E83
		protected FormulaBooleanLiteral(bool value)
		{
			this.Value = value;
			base.Children = new FormulaExpression[0];
		}

		// Token: 0x170021F8 RID: 8696
		// (get) Token: 0x0600C9C2 RID: 51650 RVA: 0x002B2C9E File Offset: 0x002B0E9E
		public Type Type
		{
			get
			{
				return typeof(bool);
			}
		}

		// Token: 0x170021F9 RID: 8697
		// (get) Token: 0x0600C9C3 RID: 51651 RVA: 0x002B2CAA File Offset: 0x002B0EAA
		public bool Value { get; }
	}
}
