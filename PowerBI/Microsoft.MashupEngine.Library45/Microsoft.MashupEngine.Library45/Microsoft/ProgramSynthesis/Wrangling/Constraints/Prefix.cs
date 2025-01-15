using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.Constraints
{
	// Token: 0x0200023F RID: 575
	public class Prefix<TInput, TOutputMember> : ValueToValueConstraint<TInput, IEnumerable<TOutputMember>>
	{
		// Token: 0x06000C4A RID: 3146 RVA: 0x00024F03 File Offset: 0x00023103
		public Prefix(TInput input, IEnumerable<TOutputMember> output, bool isSoft = false)
			: base(input, (output as IReadOnlyList<TOutputMember>) ?? output.ToList<TOutputMember>(), isSoft)
		{
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x0002516C File Offset: 0x0002336C
		public override bool Valid(Program<TInput, IEnumerable<TOutputMember>> program)
		{
			IEnumerable<TOutputMember> enumerable = program.Run(base.Input);
			return ValueEquality.Comparer.Equals(enumerable.Take(base.Output.Count<TOutputMember>()), base.Output);
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x000251A8 File Offset: 0x000233A8
		public override bool ConflictsWith(Constraint<TInput, IEnumerable<TOutputMember>> other)
		{
			Example<TInput, IEnumerable<TOutputMember>> example = other as Example<TInput, IEnumerable<TOutputMember>>;
			if (example != null)
			{
				return ValueEquality.Comparer.Equals(example.Input, base.Input) && DiscrepancyUtil.FirstDiscrepancy<TOutputMember>(example.Output, base.Output, ValueEquality<TOutputMember>.Instance).HasValue;
			}
			NegativeSubset<TInput, TOutputMember> negativeSubset = other as NegativeSubset<TInput, TOutputMember>;
			return negativeSubset != null && ValueEquality.Comparer.Equals(negativeSubset.Input, base.Input) && negativeSubset.Output.Intersect(base.Output, ValueEquality<TOutputMember>.Instance).Any<TOutputMember>();
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x00024FCA File Offset: 0x000231CA
		public override string ToString()
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("'{0}' -> [{1}, ...]", new object[]
			{
				base.Input,
				string.Join<TOutputMember>(", ", base.Output)
			}));
		}
	}
}
