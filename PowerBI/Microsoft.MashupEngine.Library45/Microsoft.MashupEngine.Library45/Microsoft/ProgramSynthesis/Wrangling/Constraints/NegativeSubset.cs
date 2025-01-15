using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.Constraints
{
	// Token: 0x0200023C RID: 572
	public class NegativeSubset<TInput, TOutputMember> : ValueToValueConstraint<TInput, IEnumerable<TOutputMember>>
	{
		// Token: 0x06000C3B RID: 3131 RVA: 0x00024F03 File Offset: 0x00023103
		public NegativeSubset(TInput input, IEnumerable<TOutputMember> output, bool isSoft = false)
			: base(input, (output as IReadOnlyList<TOutputMember>) ?? output.ToList<TOutputMember>(), isSoft)
		{
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x00024F20 File Offset: 0x00023120
		public override bool Valid(Program<TInput, IEnumerable<TOutputMember>> program)
		{
			IEnumerable<TOutputMember> enumerable = program.Run(base.Input);
			return !base.Output.Intersect(enumerable, ValueEquality<TOutputMember>.Instance).Any<TOutputMember>();
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x00024F54 File Offset: 0x00023154
		public override bool ConflictsWith(Constraint<TInput, IEnumerable<TOutputMember>> other)
		{
			ValueToValueConstraint<TInput, IEnumerable<TOutputMember>> valueToValueConstraint = other as ValueToValueConstraint<TInput, IEnumerable<TOutputMember>>;
			return valueToValueConstraint != null && (valueToValueConstraint is Example<TInput, IEnumerable<TOutputMember>> || valueToValueConstraint is Prefix<TInput, TOutputMember> || valueToValueConstraint is Subset<TInput, TOutputMember>) && ValueEquality.Comparer.Equals(valueToValueConstraint.Input, base.Input) && valueToValueConstraint.Output.Intersect(base.Output, ValueEquality<TOutputMember>.Instance).Any<TOutputMember>();
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x00024FCA File Offset: 0x000231CA
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
