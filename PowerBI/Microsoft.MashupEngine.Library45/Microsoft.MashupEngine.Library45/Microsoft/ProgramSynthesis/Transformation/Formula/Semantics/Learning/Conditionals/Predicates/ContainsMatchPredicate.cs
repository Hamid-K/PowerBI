using System;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.Wrangling;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Conditionals.Predicates
{
	// Token: 0x0200172B RID: 5931
	public class ContainsMatchPredicate : ColumnPredicate
	{
		// Token: 0x0600C550 RID: 50512 RVA: 0x002A76D1 File Offset: 0x002A58D1
		public ContainsMatchPredicate(string columnName, Regex pattern, int count)
		{
			base.ColumnName = columnName;
			this.Pattern = pattern;
			this.Count = count;
		}

		// Token: 0x17002190 RID: 8592
		// (get) Token: 0x0600C551 RID: 50513 RVA: 0x002A76EE File Offset: 0x002A58EE
		// (set) Token: 0x0600C552 RID: 50514 RVA: 0x002A76F6 File Offset: 0x002A58F6
		public int Count { get; set; }

		// Token: 0x17002191 RID: 8593
		// (get) Token: 0x0600C553 RID: 50515 RVA: 0x002A76FF File Offset: 0x002A58FF
		public Regex Pattern { get; }

		// Token: 0x0600C554 RID: 50516 RVA: 0x002A7707 File Offset: 0x002A5907
		public override bool Evaluate(IRow row)
		{
			return Operators.ContainsMatch(row, base.ColumnName, this.Pattern, this.Count);
		}

		// Token: 0x0600C555 RID: 50517 RVA: 0x002A7721 File Offset: 0x002A5921
		public override string ToEqualString()
		{
			return string.Format("{0}({1}, /{2}/, {3})", new object[] { "ContainsMatch", base.ColumnName, this.Pattern, this.Count });
		}
	}
}
