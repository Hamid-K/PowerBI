using System;
using Microsoft.ProgramSynthesis.Wrangling;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Conditionals.Predicates
{
	// Token: 0x0200172F RID: 5935
	public class IsBlankPredicate : ColumnPredicate
	{
		// Token: 0x0600C562 RID: 50530 RVA: 0x002A77E1 File Offset: 0x002A59E1
		public IsBlankPredicate(string columnName)
		{
			base.ColumnName = columnName;
		}

		// Token: 0x0600C563 RID: 50531 RVA: 0x002A7884 File Offset: 0x002A5A84
		public override bool Evaluate(IRow row)
		{
			object obj;
			return row != null && row.TryGetValue(base.ColumnName, out obj) && Operators.IsBlank(row, base.ColumnName);
		}

		// Token: 0x0600C564 RID: 50532 RVA: 0x002A78B2 File Offset: 0x002A5AB2
		public override string ToEqualString()
		{
			return "IsBlank(" + base.ColumnName + ")";
		}
	}
}
