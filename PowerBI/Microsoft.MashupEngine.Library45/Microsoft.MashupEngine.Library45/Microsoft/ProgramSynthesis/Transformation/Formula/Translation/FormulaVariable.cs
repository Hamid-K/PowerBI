using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation
{
	// Token: 0x020017F8 RID: 6136
	internal abstract class FormulaVariable : FormulaExpression
	{
		// Token: 0x0600C9DE RID: 51678 RVA: 0x002B318B File Offset: 0x002B138B
		protected FormulaVariable(string name)
		{
			this.Name = name;
			base.Children = new FormulaExpression[0];
		}

		// Token: 0x1700220A RID: 8714
		// (get) Token: 0x0600C9DF RID: 51679 RVA: 0x002B31A6 File Offset: 0x002B13A6
		public string Name { get; }

		// Token: 0x0600C9E0 RID: 51680
		public abstract FormulaExpression CloneWith(string name);

		// Token: 0x0600C9E1 RID: 51681 RVA: 0x002B31AE File Offset: 0x002B13AE
		protected override string ToCodeString()
		{
			return this.Name;
		}
	}
}
