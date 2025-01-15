using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerFx
{
	// Token: 0x020018E9 RID: 6377
	internal class PowerFxStringLiteral : FormulaStringLiteral
	{
		// Token: 0x0600CFB7 RID: 53175 RVA: 0x002B2B68 File Offset: 0x002B0D68
		public PowerFxStringLiteral(string value)
			: base(value)
		{
		}

		// Token: 0x0600CFB8 RID: 53176 RVA: 0x002C44FF File Offset: 0x002C26FF
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PowerFxStringLiteral(base.Value);
		}

		// Token: 0x0600CFB9 RID: 53177 RVA: 0x002C450C File Offset: 0x002C270C
		protected override string ToCodeString()
		{
			string text = "\"";
			string value = base.Value;
			return text + ((value != null) ? value.Replace("\"", "\"\"") : null) + "\"";
		}
	}
}
