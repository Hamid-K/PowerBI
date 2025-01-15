using System;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Wrangling;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Conditionals.Predicates
{
	// Token: 0x0200172C RID: 5932
	public class ContainsPredicate : ColumnPredicate
	{
		// Token: 0x0600C556 RID: 50518 RVA: 0x002A775B File Offset: 0x002A595B
		public ContainsPredicate(string columnName, string value, int count)
		{
			base.ColumnName = columnName;
			this.FindText = value;
			this.Count = count;
		}

		// Token: 0x17002192 RID: 8594
		// (get) Token: 0x0600C557 RID: 50519 RVA: 0x002A7778 File Offset: 0x002A5978
		public int Count { get; }

		// Token: 0x17002193 RID: 8595
		// (get) Token: 0x0600C558 RID: 50520 RVA: 0x002A7780 File Offset: 0x002A5980
		public string FindText { get; }

		// Token: 0x0600C559 RID: 50521 RVA: 0x002A7788 File Offset: 0x002A5988
		public override bool Evaluate(IRow row)
		{
			return Operators.Contains(row, base.ColumnName, this.FindText, this.Count);
		}

		// Token: 0x0600C55A RID: 50522 RVA: 0x002A77A2 File Offset: 0x002A59A2
		public override string ToEqualString()
		{
			return string.Format("{0}({1}, {2}, {3})", new object[]
			{
				"Contains",
				base.ColumnName,
				this.FindText.ToCSharpLiteral(),
				this.Count
			});
		}
	}
}
