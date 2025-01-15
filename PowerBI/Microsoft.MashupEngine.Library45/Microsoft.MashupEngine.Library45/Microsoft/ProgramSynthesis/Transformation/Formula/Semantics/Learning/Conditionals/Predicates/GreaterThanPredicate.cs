using System;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Wrangling;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Conditionals.Predicates
{
	// Token: 0x0200172E RID: 5934
	public class GreaterThanPredicate : ColumnPredicate
	{
		// Token: 0x0600C55E RID: 50526 RVA: 0x002A7815 File Offset: 0x002A5A15
		public GreaterThanPredicate(string columnName, decimal value)
		{
			base.ColumnName = columnName;
			this.Value = value;
		}

		// Token: 0x17002194 RID: 8596
		// (get) Token: 0x0600C55F RID: 50527 RVA: 0x002A782B File Offset: 0x002A5A2B
		public decimal Value { get; }

		// Token: 0x0600C560 RID: 50528 RVA: 0x002A7833 File Offset: 0x002A5A33
		public override bool Evaluate(IRow row)
		{
			return Operators.NumberGreaterThan(row, base.ColumnName, this.Value);
		}

		// Token: 0x0600C561 RID: 50529 RVA: 0x002A7847 File Offset: 0x002A5A47
		public override string ToEqualString()
		{
			return string.Concat(new string[]
			{
				"NumberGreaterThan(",
				base.ColumnName,
				", ",
				this.Value.ToCSharpLiteral(),
				")"
			});
		}
	}
}
