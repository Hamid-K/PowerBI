using System;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.Wrangling;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Conditionals.Predicates
{
	// Token: 0x02001730 RID: 5936
	public class IsMatchPredicate : ColumnPredicate
	{
		// Token: 0x0600C565 RID: 50533 RVA: 0x002A78C9 File Offset: 0x002A5AC9
		public IsMatchPredicate(string columnName, Regex pattern)
		{
			base.ColumnName = columnName;
			this.Pattern = pattern;
		}

		// Token: 0x17002195 RID: 8597
		// (get) Token: 0x0600C566 RID: 50534 RVA: 0x002A78DF File Offset: 0x002A5ADF
		public Regex Pattern { get; }

		// Token: 0x0600C567 RID: 50535 RVA: 0x002A78E7 File Offset: 0x002A5AE7
		public override bool Evaluate(IRow row)
		{
			return Operators.IsMatch(row, base.ColumnName, this.Pattern);
		}

		// Token: 0x0600C568 RID: 50536 RVA: 0x002A78FB File Offset: 0x002A5AFB
		public override string ToEqualString()
		{
			return string.Format("{0}({1}, /{2}/)", "IsMatch", base.ColumnName, this.Pattern);
		}
	}
}
