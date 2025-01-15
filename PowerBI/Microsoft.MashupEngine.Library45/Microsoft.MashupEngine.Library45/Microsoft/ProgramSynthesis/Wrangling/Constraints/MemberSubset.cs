using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.Constraints
{
	// Token: 0x02000236 RID: 566
	public class MemberSubset<TInputMember, TOutputSubmember> : MemberToMemberConstraint<TInputMember, IEnumerable<TOutputSubmember>>
	{
		// Token: 0x06000C1B RID: 3099 RVA: 0x00024912 File Offset: 0x00022B12
		public MemberSubset(TInputMember inputMember, IEnumerable<TOutputSubmember> outputMember, bool isSoft = false)
			: base(inputMember, (outputMember as IReadOnlyList<TOutputSubmember>) ?? outputMember.ToList<TOutputSubmember>(), isSoft)
		{
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x00024AE1 File Offset: 0x00022CE1
		protected override bool Valid(Program<IEnumerable<TInputMember>, IEnumerable<IEnumerable<TOutputSubmember>>> program, IEnumerable<TOutputSubmember> programOutputMember)
		{
			return programOutputMember != null && !base.OutputMember.Except(programOutputMember, ValueEquality<TOutputSubmember>.Instance).Any<TOutputSubmember>();
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x00024B04 File Offset: 0x00022D04
		public override bool ConflictsWith(Constraint<IEnumerable<TInputMember>, IEnumerable<IEnumerable<TOutputSubmember>>> other)
		{
			Example<IEnumerable<TInputMember>, IEnumerable<IEnumerable<TOutputSubmember>>> example = other as Example<IEnumerable<TInputMember>, IEnumerable<IEnumerable<TOutputSubmember>>>;
			if (example != null)
			{
				return example.Input.Zip(example.Output, (TInputMember inputMember, IEnumerable<TOutputSubmember> outputMember) => ValueEquality.Comparer.Equals(inputMember, base.InputMember) && base.OutputMember.Except(outputMember, ValueEquality<TOutputSubmember>.Instance).Any<TOutputSubmember>()).Any((bool b) => b);
			}
			CorrespondingMemberEquals<TInputMember, IEnumerable<TOutputSubmember>> correspondingMemberEquals = other as CorrespondingMemberEquals<TInputMember, IEnumerable<TOutputSubmember>>;
			if (correspondingMemberEquals != null)
			{
				return ValueEquality.Comparer.Equals(correspondingMemberEquals.InputMember, base.InputMember) && base.OutputMember.Except(correspondingMemberEquals.OutputMember, ValueEquality<TOutputSubmember>.Instance).Any<TOutputSubmember>();
			}
			NegativeMemberSubset<TInputMember, TOutputSubmember> negativeMemberSubset = other as NegativeMemberSubset<TInputMember, TOutputSubmember>;
			return negativeMemberSubset != null && ValueEquality.Comparer.Equals(negativeMemberSubset.InputMember, base.InputMember) && negativeMemberSubset.OutputMember.Intersect(base.OutputMember, ValueEquality<TOutputSubmember>.Instance).Any<TOutputSubmember>();
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x00024A57 File Offset: 0x00022C57
		public override string ToString()
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("'{0}' -> [{1}, ...]", new object[]
			{
				base.InputMember,
				string.Join<TOutputSubmember>(", ", base.OutputMember)
			}));
		}
	}
}
