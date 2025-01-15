using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Conditionals.Predicates
{
	// Token: 0x0200172A RID: 5930
	public abstract class ColumnPredicate : Predicate
	{
		// Token: 0x1700218F RID: 8591
		// (get) Token: 0x0600C54D RID: 50509 RVA: 0x002A76B8 File Offset: 0x002A58B8
		// (set) Token: 0x0600C54E RID: 50510 RVA: 0x002A76C0 File Offset: 0x002A58C0
		public string ColumnName { get; protected set; }
	}
}
