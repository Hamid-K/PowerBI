using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation
{
	// Token: 0x020017EA RID: 6122
	internal abstract class FormulaStringLiteral : FormulaExpression, IFormulaLiteral<string>, IFormulaTyped
	{
		// Token: 0x0600C9AF RID: 51631 RVA: 0x002B2B28 File Offset: 0x002B0D28
		protected FormulaStringLiteral()
		{
		}

		// Token: 0x0600C9B0 RID: 51632 RVA: 0x002B2B30 File Offset: 0x002B0D30
		protected FormulaStringLiteral(string value)
		{
			this.Value = value;
			base.Children = new FormulaExpression[0];
		}

		// Token: 0x170021F1 RID: 8689
		// (get) Token: 0x0600C9B1 RID: 51633 RVA: 0x002B2B4B File Offset: 0x002B0D4B
		public Type Type
		{
			get
			{
				return typeof(string);
			}
		}

		// Token: 0x170021F2 RID: 8690
		// (get) Token: 0x0600C9B2 RID: 51634 RVA: 0x002B2B57 File Offset: 0x002B0D57
		// (set) Token: 0x0600C9B3 RID: 51635 RVA: 0x002B2B5F File Offset: 0x002B0D5F
		public string Value { get; protected set; }
	}
}
