using System;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.Constraints
{
	// Token: 0x0200022F RID: 559
	public class Example<TInput, TOutput> : ValueToValueConstraint<TInput, TOutput>
	{
		// Token: 0x06000BFE RID: 3070 RVA: 0x00023E3F File Offset: 0x0002203F
		public Example(TInput input, TOutput output, bool isSoft = false)
			: base(input, output, isSoft)
		{
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x00024618 File Offset: 0x00022818
		public override bool Valid(Program<TInput, TOutput> program)
		{
			TOutput toutput = program.Run(base.Input);
			bool flag = base.Output != null && toutput != null;
			if (flag)
			{
				TOutput toutput2 = base.Output;
				bool flag2 = toutput2 is double || toutput2 is decimal || toutput2 is float;
				flag = flag2;
			}
			bool flag3 = flag;
			if (flag3)
			{
				bool flag2 = toutput is double || toutput is decimal || toutput is float;
				flag3 = flag2;
			}
			if (flag3)
			{
				TOutput toutput2 = base.Output;
				if (toutput2.GetType() == toutput.GetType())
				{
					decimal num = Convert.ToDecimal(toutput);
					int num2 = num.Scale();
					decimal num3 = Convert.ToDecimal(base.Output);
					int num4 = num3.Scale();
					if (num2 > 10 && num4 < num2)
					{
						return num.EqualsWithTruncate(num3);
					}
					return Math.Abs(num - num3) < 0.00001m;
				}
			}
			return ValueEquality.Comparer.Equals(toutput, base.Output);
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x00024770 File Offset: 0x00022970
		public override bool ConflictsWith(Constraint<TInput, TOutput> other)
		{
			Example<TInput, TOutput> example = other as Example<TInput, TOutput>;
			if (example != null)
			{
				return ValueEquality.Comparer.Equals(example.Input, base.Input) && !ValueEquality.Comparer.Equals(example.Output, base.Output);
			}
			DoesNotEqual<TInput, TOutput> doesNotEqual = other as DoesNotEqual<TInput, TOutput>;
			return doesNotEqual != null && ValueEquality.Comparer.Equals(doesNotEqual.Input, base.Input) && ValueEquality.Comparer.Equals(doesNotEqual.Output, base.Output);
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x0002482B File Offset: 0x00022A2B
		public override string ToString()
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("{0} -> {1}", new object[]
			{
				base.Input.ToLiteral(null),
				base.Output.ToLiteral(null)
			}));
		}
	}
}
