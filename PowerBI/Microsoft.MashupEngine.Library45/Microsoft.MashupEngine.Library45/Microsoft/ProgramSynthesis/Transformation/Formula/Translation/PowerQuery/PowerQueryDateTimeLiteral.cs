using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerQuery
{
	// Token: 0x0200189E RID: 6302
	internal class PowerQueryDateTimeLiteral : FormulaDateTimeLiteral
	{
		// Token: 0x0600CDF0 RID: 52720 RVA: 0x002B8A01 File Offset: 0x002B6C01
		public PowerQueryDateTimeLiteral(DateTime value)
			: base(value)
		{
		}

		// Token: 0x0600CDF1 RID: 52721 RVA: 0x00004FAE File Offset: 0x000031AE
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return this;
		}

		// Token: 0x0600CDF2 RID: 52722 RVA: 0x002BF7A0 File Offset: 0x002BD9A0
		protected override string ToCodeString()
		{
			if (!(base.Value == base.Value.Date))
			{
				return string.Format("#datetime({0:yyyy, M, d, H, m, s})", base.Value);
			}
			return string.Format("#date({0:yyyy, M, d})", base.Value);
		}
	}
}
