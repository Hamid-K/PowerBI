using System;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.Constraints
{
	// Token: 0x02000225 RID: 549
	public class DoesNotEqual<TInput, TOutput> : ValueToValueConstraint<TInput, TOutput>
	{
		// Token: 0x06000BCC RID: 3020 RVA: 0x00023E3F File Offset: 0x0002203F
		public DoesNotEqual(TInput input, TOutput output, bool isSoft = false)
			: base(input, output, isSoft)
		{
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x00023E4C File Offset: 0x0002204C
		public override bool Valid(Program<TInput, TOutput> program)
		{
			TOutput toutput = program.Run(base.Input);
			return !ValueEquality.Comparer.Equals(toutput, base.Output);
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x00023E84 File Offset: 0x00022084
		public override bool ConflictsWith(Constraint<TInput, TOutput> other)
		{
			Example<TInput, TOutput> example = other as Example<TInput, TOutput>;
			return example != null && ValueEquality.Comparer.Equals(example.Input, base.Input) && ValueEquality.Comparer.Equals(example.Output, base.Output);
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x00023EE7 File Offset: 0x000220E7
		public override string ToString()
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("'{0}' -!> '{1}'", new object[] { base.Input, base.Output }));
		}
	}
}
