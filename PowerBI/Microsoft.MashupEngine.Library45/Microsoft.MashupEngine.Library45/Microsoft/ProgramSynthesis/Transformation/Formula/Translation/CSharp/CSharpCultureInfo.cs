using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp
{
	// Token: 0x02001945 RID: 6469
	internal class CSharpCultureInfo : FormulaExpression
	{
		// Token: 0x0600D386 RID: 54150 RVA: 0x002D1904 File Offset: 0x002CFB04
		public CSharpCultureInfo(FormulaExpression locale)
		{
			this.Locale = locale;
			base.Children = new FormulaExpression[] { this.Locale };
		}

		// Token: 0x17002325 RID: 8997
		// (get) Token: 0x0600D387 RID: 54151 RVA: 0x002D1928 File Offset: 0x002CFB28
		public FormulaExpression Locale { get; }

		// Token: 0x0600D388 RID: 54152 RVA: 0x002D1930 File Offset: 0x002CFB30
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new CSharpCultureInfo(this.Locale.Accept<FormulaExpression>(visitor));
		}

		// Token: 0x0600D389 RID: 54153 RVA: 0x002D1943 File Offset: 0x002CFB43
		protected override string ToCodeString()
		{
			return string.Format("new CultureInfo({0})", this.Locale);
		}
	}
}
