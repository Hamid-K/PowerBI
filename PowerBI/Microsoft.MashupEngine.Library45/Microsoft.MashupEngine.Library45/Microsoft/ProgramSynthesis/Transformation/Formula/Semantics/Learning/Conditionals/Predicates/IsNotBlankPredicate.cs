using System;
using Microsoft.ProgramSynthesis.Wrangling;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Conditionals.Predicates
{
	// Token: 0x02001731 RID: 5937
	public class IsNotBlankPredicate : ColumnPredicate
	{
		// Token: 0x0600C569 RID: 50537 RVA: 0x002A77E1 File Offset: 0x002A59E1
		public IsNotBlankPredicate(string columnName)
		{
			base.ColumnName = columnName;
		}

		// Token: 0x0600C56A RID: 50538 RVA: 0x002A7918 File Offset: 0x002A5B18
		public override bool Evaluate(IRow row)
		{
			object obj;
			return row != null && row.TryGetValue(base.ColumnName, out obj) && Operators.IsNotBlank(row, base.ColumnName);
		}

		// Token: 0x0600C56B RID: 50539 RVA: 0x002A7946 File Offset: 0x002A5B46
		public override string ToEqualString()
		{
			return "IsNotBlank(" + base.ColumnName + ")";
		}
	}
}
