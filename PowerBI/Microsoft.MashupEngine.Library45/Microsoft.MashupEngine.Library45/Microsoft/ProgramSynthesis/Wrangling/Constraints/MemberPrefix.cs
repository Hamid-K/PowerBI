using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.Constraints
{
	// Token: 0x02000234 RID: 564
	public class MemberPrefix<TInputMember, TOutputSubmember> : MemberToMemberConstraint<TInputMember, IEnumerable<TOutputSubmember>>
	{
		// Token: 0x06000C13 RID: 3091 RVA: 0x00024912 File Offset: 0x00022B12
		public MemberPrefix(TInputMember inputMember, IEnumerable<TOutputSubmember> outputMember, bool isSoft = false)
			: base(inputMember, (outputMember as IReadOnlyList<TOutputSubmember>) ?? outputMember.ToList<TOutputSubmember>(), isSoft)
		{
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x0002492C File Offset: 0x00022B2C
		protected override bool Valid(Program<IEnumerable<TInputMember>, IEnumerable<IEnumerable<TOutputSubmember>>> program, IEnumerable<TOutputSubmember> programOutputMember)
		{
			return programOutputMember != null && ValueEquality.Comparer.Equals(programOutputMember.Take(base.OutputMember.Count<TOutputSubmember>()), base.OutputMember);
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x00024954 File Offset: 0x00022B54
		public override bool ConflictsWith(Constraint<IEnumerable<TInputMember>, IEnumerable<IEnumerable<TOutputSubmember>>> other)
		{
			Example<IEnumerable<TInputMember>, IEnumerable<IEnumerable<TOutputSubmember>>> example = other as Example<IEnumerable<TInputMember>, IEnumerable<IEnumerable<TOutputSubmember>>>;
			if (example != null)
			{
				return example.Input.Zip(example.Output, (TInputMember inputMember, IEnumerable<TOutputSubmember> outputMember) => ValueEquality.Comparer.Equals(inputMember, base.InputMember) && DiscrepancyUtil.FirstDiscrepancy<TOutputSubmember>(outputMember, base.OutputMember, ValueEquality<TOutputSubmember>.Instance).HasValue).Any((bool b) => b);
			}
			CorrespondingMemberEquals<TInputMember, IEnumerable<TOutputSubmember>> correspondingMemberEquals = other as CorrespondingMemberEquals<TInputMember, IEnumerable<TOutputSubmember>>;
			if (correspondingMemberEquals != null)
			{
				return ValueEquality.Comparer.Equals(correspondingMemberEquals.InputMember, base.InputMember) && DiscrepancyUtil.FirstDiscrepancy<TOutputSubmember>(correspondingMemberEquals.OutputMember, base.OutputMember, ValueEquality<TOutputSubmember>.Instance).HasValue;
			}
			NegativeMemberSubset<TInputMember, TOutputSubmember> negativeMemberSubset = other as NegativeMemberSubset<TInputMember, TOutputSubmember>;
			return negativeMemberSubset != null && ValueEquality.Comparer.Equals(negativeMemberSubset.InputMember, base.InputMember) && negativeMemberSubset.OutputMember.Intersect(base.OutputMember, ValueEquality<TOutputSubmember>.Instance).Any<TOutputSubmember>();
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x00024A57 File Offset: 0x00022C57
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
