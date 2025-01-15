using System;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Wrangling;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Conditionals.Predicates
{
	// Token: 0x02001735 RID: 5941
	public class NumberEqualsPredicate : ColumnPredicate
	{
		// Token: 0x0600C576 RID: 50550 RVA: 0x002A7A15 File Offset: 0x002A5C15
		public NumberEqualsPredicate(string columnName, decimal value)
		{
			base.ColumnName = columnName;
			this.Value = value;
		}

		// Token: 0x17002197 RID: 8599
		// (get) Token: 0x0600C577 RID: 50551 RVA: 0x002A7A2B File Offset: 0x002A5C2B
		public decimal Value { get; }

		// Token: 0x0600C578 RID: 50552 RVA: 0x002A7A33 File Offset: 0x002A5C33
		public override bool Evaluate(IRow row)
		{
			return Operators.NumberEquals(row, base.ColumnName, this.Value);
		}

		// Token: 0x0600C579 RID: 50553 RVA: 0x002A7A47 File Offset: 0x002A5C47
		public override string ToEqualString()
		{
			return string.Concat(new string[]
			{
				"NumberEquals(",
				base.ColumnName,
				", ",
				this.Value.ToCSharpLiteral(),
				")"
			});
		}
	}
}
