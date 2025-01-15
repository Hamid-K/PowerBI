using System;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Wrangling;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Conditionals.Predicates
{
	// Token: 0x0200173C RID: 5948
	public class StartsWithPredicate : ColumnPredicate
	{
		// Token: 0x0600C592 RID: 50578 RVA: 0x002A7C13 File Offset: 0x002A5E13
		public StartsWithPredicate(string columnName, string findText)
		{
			base.ColumnName = columnName;
			this.FindText = findText;
		}

		// Token: 0x1700219A RID: 8602
		// (get) Token: 0x0600C593 RID: 50579 RVA: 0x002A7C29 File Offset: 0x002A5E29
		public string FindText { get; }

		// Token: 0x0600C594 RID: 50580 RVA: 0x002A7C31 File Offset: 0x002A5E31
		public override bool Evaluate(IRow row)
		{
			return Operators.StartsWith(row, base.ColumnName, this.FindText);
		}

		// Token: 0x0600C595 RID: 50581 RVA: 0x002A7C45 File Offset: 0x002A5E45
		public override string ToEqualString()
		{
			return string.Concat(new string[]
			{
				"StartsWith(",
				base.ColumnName,
				", ",
				this.FindText.ToCSharpLiteral(),
				")"
			});
		}
	}
}
