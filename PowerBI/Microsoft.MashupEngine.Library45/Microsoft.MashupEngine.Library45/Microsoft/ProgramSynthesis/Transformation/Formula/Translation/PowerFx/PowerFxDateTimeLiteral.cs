using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerFx
{
	// Token: 0x020018D7 RID: 6359
	internal class PowerFxDateTimeLiteral : FormulaDateTimeLiteral
	{
		// Token: 0x0600CF7F RID: 53119 RVA: 0x002B8A01 File Offset: 0x002B6C01
		public PowerFxDateTimeLiteral(DateTime value)
			: base(value)
		{
		}

		// Token: 0x0600CF80 RID: 53120 RVA: 0x00004FAE File Offset: 0x000031AE
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return this;
		}

		// Token: 0x0600CF81 RID: 53121 RVA: 0x002C421C File Offset: 0x002C241C
		protected override string ToCodeString()
		{
			if (!(base.Value == base.Value.Date))
			{
				return string.Format("DateTimeValue(\"{0:s}\")", base.Value);
			}
			return string.Format("DateValue(\"{0:yyyy-MM-dd}\")", base.Value);
		}
	}
}
