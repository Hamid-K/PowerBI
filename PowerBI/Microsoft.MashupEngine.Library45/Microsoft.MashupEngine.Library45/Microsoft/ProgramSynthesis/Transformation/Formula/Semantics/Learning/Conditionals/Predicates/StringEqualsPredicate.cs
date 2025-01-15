using System;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Wrangling;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Conditionals.Predicates
{
	// Token: 0x0200173D RID: 5949
	public class StringEqualsPredicate : ColumnPredicate
	{
		// Token: 0x0600C596 RID: 50582 RVA: 0x002A7C81 File Offset: 0x002A5E81
		public StringEqualsPredicate(string columnName, string value)
		{
			base.ColumnName = columnName;
			this.Value = value;
		}

		// Token: 0x1700219B RID: 8603
		// (get) Token: 0x0600C597 RID: 50583 RVA: 0x002A7C97 File Offset: 0x002A5E97
		public string Value { get; }

		// Token: 0x0600C598 RID: 50584 RVA: 0x002A7C9F File Offset: 0x002A5E9F
		public override bool Evaluate(IRow row)
		{
			return Operators.StringEquals(row, base.ColumnName, this.Value);
		}

		// Token: 0x0600C599 RID: 50585 RVA: 0x002A7CB3 File Offset: 0x002A5EB3
		public override string ToEqualString()
		{
			return string.Concat(new string[]
			{
				"StringEquals(",
				base.ColumnName,
				", ",
				this.Value.ToCSharpLiteral(),
				")"
			});
		}
	}
}
