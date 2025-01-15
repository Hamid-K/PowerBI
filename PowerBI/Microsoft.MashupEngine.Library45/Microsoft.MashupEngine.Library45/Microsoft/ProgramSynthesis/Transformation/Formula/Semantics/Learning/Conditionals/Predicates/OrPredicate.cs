using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Wrangling;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Conditionals.Predicates
{
	// Token: 0x02001736 RID: 5942
	public class OrPredicate : Predicate
	{
		// Token: 0x0600C57A RID: 50554 RVA: 0x002A7A83 File Offset: 0x002A5C83
		public OrPredicate(IEnumerable<Predicate> children)
		{
			this.Children = (children as IReadOnlyList<Predicate>) ?? children.ToList<Predicate>();
		}

		// Token: 0x17002198 RID: 8600
		// (get) Token: 0x0600C57B RID: 50555 RVA: 0x002A7AA1 File Offset: 0x002A5CA1
		public IReadOnlyList<Predicate> Children { get; }

		// Token: 0x0600C57C RID: 50556 RVA: 0x002A7AAC File Offset: 0x002A5CAC
		public override bool Evaluate(IRow subject)
		{
			IReadOnlyList<Predicate> children = this.Children;
			return ((children != null) ? children.Aggregate(new bool?(false), (bool? current, Predicate next) => new bool?(Operators.Or(current, new bool?(next.Evaluate(subject))))) : null).GetValueOrDefault();
		}

		// Token: 0x0600C57D RID: 50557 RVA: 0x002A7AFA File Offset: 0x002A5CFA
		public override string ToEqualString()
		{
			return this.Children.Select((Predicate i) => i.ToString()).ToJoinString(" or ");
		}
	}
}
