using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation
{
	// Token: 0x020017F9 RID: 6137
	internal class FormulaRaw : FormulaExpression
	{
		// Token: 0x0600C9E2 RID: 51682 RVA: 0x002B31B6 File Offset: 0x002B13B6
		public FormulaRaw(string content)
		{
			this._content = content;
		}

		// Token: 0x0600C9E3 RID: 51683 RVA: 0x00004FAE File Offset: 0x000031AE
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return this;
		}

		// Token: 0x0600C9E4 RID: 51684 RVA: 0x002B31C5 File Offset: 0x002B13C5
		protected override string ToCodeString()
		{
			return this._content;
		}

		// Token: 0x04004F40 RID: 20288
		private readonly string _content;
	}
}
