using System;
using Microsoft.ProgramSynthesis.Wrangling;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Conditionals.Predicates
{
	// Token: 0x0200173B RID: 5947
	public class StartsWithDigitPredicate : ColumnPredicate
	{
		// Token: 0x0600C58F RID: 50575 RVA: 0x002A77E1 File Offset: 0x002A59E1
		public StartsWithDigitPredicate(string columnName)
		{
			base.ColumnName = columnName;
		}

		// Token: 0x0600C590 RID: 50576 RVA: 0x002A7BEE File Offset: 0x002A5DEE
		public override bool Evaluate(IRow row)
		{
			return Operators.StartsWithDigit(row, base.ColumnName);
		}

		// Token: 0x0600C591 RID: 50577 RVA: 0x002A7BFC File Offset: 0x002A5DFC
		public override string ToEqualString()
		{
			return "StartsWithDigit(" + base.ColumnName + ")";
		}
	}
}
