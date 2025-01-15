using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.Constraints
{
	// Token: 0x02000240 RID: 576
	public class Subset<TInput, TOutputMember> : ValueToValueConstraint<TInput, IEnumerable<TOutputMember>>
	{
		// Token: 0x06000C4E RID: 3150 RVA: 0x00024F03 File Offset: 0x00023103
		public Subset(TInput input, IEnumerable<TOutputMember> output, bool isSoft = false)
			: base(input, (output as IReadOnlyList<TOutputMember>) ?? output.ToList<TOutputMember>(), isSoft)
		{
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x0002525C File Offset: 0x0002345C
		public override bool Valid(Program<TInput, IEnumerable<TOutputMember>> program)
		{
			IEnumerable<TOutputMember> enumerable = program.Run(base.Input);
			return !base.Output.Except(enumerable, ValueEquality<TOutputMember>.Instance).Any<TOutputMember>();
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x00025290 File Offset: 0x00023490
		public override bool ConflictsWith(Constraint<TInput, IEnumerable<TOutputMember>> other)
		{
			Example<TInput, IEnumerable<TOutputMember>> example = other as Example<TInput, IEnumerable<TOutputMember>>;
			if (example != null)
			{
				return ValueEquality.Comparer.Equals(example.Input, base.Input) && example.Output.Except(base.Output, ValueEquality<TOutputMember>.Instance).Any<TOutputMember>();
			}
			NegativeSubset<TInput, TOutputMember> negativeSubset = other as NegativeSubset<TInput, TOutputMember>;
			return negativeSubset != null && ValueEquality.Comparer.Equals(negativeSubset.Input, base.Input) && negativeSubset.Output.Intersect(base.Output, ValueEquality<TOutputMember>.Instance).Any<TOutputMember>();
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x00024FCA File Offset: 0x000231CA
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
