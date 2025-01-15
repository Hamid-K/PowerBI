using System;
using Microsoft.ProgramSynthesis.Wrangling;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Conditionals.Predicates
{
	// Token: 0x0200172D RID: 5933
	public class EndsWithDigitPredicate : ColumnPredicate
	{
		// Token: 0x0600C55B RID: 50523 RVA: 0x002A77E1 File Offset: 0x002A59E1
		public EndsWithDigitPredicate(string columnName)
		{
			base.ColumnName = columnName;
		}

		// Token: 0x0600C55C RID: 50524 RVA: 0x002A77F0 File Offset: 0x002A59F0
		public override bool Evaluate(IRow row)
		{
			return Operators.EndsWithDigit(row, base.ColumnName);
		}

		// Token: 0x0600C55D RID: 50525 RVA: 0x002A77FE File Offset: 0x002A59FE
		public override string ToEqualString()
		{
			return "EndsWithDigit(" + base.ColumnName + ")";
		}
	}
}
