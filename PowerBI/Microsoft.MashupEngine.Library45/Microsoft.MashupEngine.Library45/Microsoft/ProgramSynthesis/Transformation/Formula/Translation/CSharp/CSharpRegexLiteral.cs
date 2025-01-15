using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp
{
	// Token: 0x02001947 RID: 6471
	internal class CSharpRegexLiteral : FormulaRegexLiteral
	{
		// Token: 0x0600D38C RID: 54156 RVA: 0x002B88CB File Offset: 0x002B6ACB
		public CSharpRegexLiteral(string value)
			: base(value)
		{
		}

		// Token: 0x0600D38D RID: 54157 RVA: 0x00004FAE File Offset: 0x000031AE
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return this;
		}

		// Token: 0x0600D38E RID: 54158 RVA: 0x002D1968 File Offset: 0x002CFB68
		protected override string ToCodeString()
		{
			return "@\"" + base.Value.Replace("\"", "\"\"") + "\"";
		}
	}
}
