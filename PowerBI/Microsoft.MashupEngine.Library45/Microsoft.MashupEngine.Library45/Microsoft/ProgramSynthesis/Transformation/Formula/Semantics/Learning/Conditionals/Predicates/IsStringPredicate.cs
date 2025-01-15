using System;
using Microsoft.ProgramSynthesis.Wrangling;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Conditionals.Predicates
{
	// Token: 0x02001733 RID: 5939
	public class IsStringPredicate : ColumnPredicate
	{
		// Token: 0x0600C56F RID: 50543 RVA: 0x002A77E1 File Offset: 0x002A59E1
		public IsStringPredicate(string columnName)
		{
			base.ColumnName = columnName;
		}

		// Token: 0x0600C570 RID: 50544 RVA: 0x002A7982 File Offset: 0x002A5B82
		public override bool Evaluate(IRow row)
		{
			return Operators.IsString(row, base.ColumnName);
		}

		// Token: 0x0600C571 RID: 50545 RVA: 0x002A7990 File Offset: 0x002A5B90
		public override string ToEqualString()
		{
			return "IsString(" + base.ColumnName + ")";
		}
	}
}
