using System;
using System.Linq;
using Microsoft.ProgramSynthesis.Transformation.Formula.Constraints;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Transformation.Formula
{
	// Token: 0x020014F5 RID: 5365
	public static class ConstraintCollectionExtensions
	{
		// Token: 0x0600A476 RID: 42102 RVA: 0x0022E601 File Offset: 0x0022C801
		public static void AddOrReplace<TConstraint>(this NotifyingCollection<Constraint<IRow, object>> constraints, TConstraint constraint) where TConstraint : Constraint<IRow, object>, IUniqueConstraint<TConstraint>
		{
			constraints.Remove(constraints.OfType<TConstraint>());
			constraints.Add(constraint);
		}
	}
}
