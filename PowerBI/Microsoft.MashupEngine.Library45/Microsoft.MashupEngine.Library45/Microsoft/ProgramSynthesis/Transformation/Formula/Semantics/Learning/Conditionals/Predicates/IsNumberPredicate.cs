using System;
using Microsoft.ProgramSynthesis.Wrangling;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Conditionals.Predicates
{
	// Token: 0x02001732 RID: 5938
	public class IsNumberPredicate : ColumnPredicate
	{
		// Token: 0x0600C56C RID: 50540 RVA: 0x002A77E1 File Offset: 0x002A59E1
		public IsNumberPredicate(string columnName)
		{
			base.ColumnName = columnName;
		}

		// Token: 0x0600C56D RID: 50541 RVA: 0x002A795D File Offset: 0x002A5B5D
		public override bool Evaluate(IRow row)
		{
			return Operators.IsNumber(row, base.ColumnName);
		}

		// Token: 0x0600C56E RID: 50542 RVA: 0x002A796B File Offset: 0x002A5B6B
		public override string ToEqualString()
		{
			return "IsNumber(" + base.ColumnName + ")";
		}
	}
}
