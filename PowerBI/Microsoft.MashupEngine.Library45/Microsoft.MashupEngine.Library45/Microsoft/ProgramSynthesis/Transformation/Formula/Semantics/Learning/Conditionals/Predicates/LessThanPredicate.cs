using System;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Wrangling;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Conditionals.Predicates
{
	// Token: 0x02001734 RID: 5940
	public class LessThanPredicate : ColumnPredicate
	{
		// Token: 0x0600C572 RID: 50546 RVA: 0x002A79A7 File Offset: 0x002A5BA7
		public LessThanPredicate(string columnName, decimal value)
		{
			base.ColumnName = columnName;
			this.Value = value;
		}

		// Token: 0x17002196 RID: 8598
		// (get) Token: 0x0600C573 RID: 50547 RVA: 0x002A79BD File Offset: 0x002A5BBD
		public decimal Value { get; }

		// Token: 0x0600C574 RID: 50548 RVA: 0x002A79C5 File Offset: 0x002A5BC5
		public override bool Evaluate(IRow row)
		{
			return Operators.NumberLessThan(row, base.ColumnName, this.Value);
		}

		// Token: 0x0600C575 RID: 50549 RVA: 0x002A79D9 File Offset: 0x002A5BD9
		public override string ToEqualString()
		{
			return string.Concat(new string[]
			{
				"NumberLessThan(",
				base.ColumnName,
				", ",
				this.Value.ToCSharpLiteral(),
				")"
			});
		}
	}
}
