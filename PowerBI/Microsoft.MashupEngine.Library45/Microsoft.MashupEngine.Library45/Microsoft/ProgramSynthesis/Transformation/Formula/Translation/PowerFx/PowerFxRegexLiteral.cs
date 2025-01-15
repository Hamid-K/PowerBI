using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerFx
{
	// Token: 0x020018EC RID: 6380
	internal class PowerFxRegexLiteral : FormulaRegexLiteral
	{
		// Token: 0x0600CFC0 RID: 53184 RVA: 0x002B88CB File Offset: 0x002B6ACB
		public PowerFxRegexLiteral(string value)
			: base(value)
		{
		}

		// Token: 0x0600CFC1 RID: 53185 RVA: 0x00004FAE File Offset: 0x000031AE
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return this;
		}

		// Token: 0x0600CFC2 RID: 53186 RVA: 0x002C454F File Offset: 0x002C274F
		protected override string ToCodeString()
		{
			return "\"" + base.Value.Replace("\"", "\"\"") + "\"";
		}
	}
}
