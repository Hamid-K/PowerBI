using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.Constraints
{
	// Token: 0x0200023A RID: 570
	public class NegativeMemberSubset<TInputMember, TOutputSubmember> : MemberToMemberConstraint<TInputMember, IEnumerable<TOutputSubmember>>
	{
		// Token: 0x06000C33 RID: 3123 RVA: 0x00024912 File Offset: 0x00022B12
		public NegativeMemberSubset(TInputMember inputMember, IEnumerable<TOutputSubmember> outputMember, bool isSoft = false)
			: base(inputMember, (outputMember as IReadOnlyList<TOutputSubmember>) ?? outputMember.ToList<TOutputSubmember>(), isSoft)
		{
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x00024DD7 File Offset: 0x00022FD7
		protected override bool Valid(Program<IEnumerable<TInputMember>, IEnumerable<IEnumerable<TOutputSubmember>>> program, IEnumerable<TOutputSubmember> programOutputMember)
		{
			return programOutputMember == null || !base.OutputMember.Intersect(programOutputMember, ValueEquality<TOutputSubmember>.Instance).Any<TOutputSubmember>();
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x00024DF8 File Offset: 0x00022FF8
		public override bool ConflictsWith(Constraint<IEnumerable<TInputMember>, IEnumerable<IEnumerable<TOutputSubmember>>> other)
		{
			Example<IEnumerable<TInputMember>, IEnumerable<IEnumerable<TOutputSubmember>>> example = other as Example<IEnumerable<TInputMember>, IEnumerable<IEnumerable<TOutputSubmember>>>;
			if (example != null)
			{
				return example.Input.Zip(example.Output, (TInputMember input, IEnumerable<TOutputSubmember> output) => ValueEquality.Comparer.Equals(input, base.InputMember) && output.Intersect(base.OutputMember, ValueEquality<TOutputSubmember>.Instance).Any<TOutputSubmember>()).Any((bool b) => b);
			}
			MemberToMemberConstraint<TInputMember, IEnumerable<TOutputSubmember>> memberToMemberConstraint = other as MemberToMemberConstraint<TInputMember, IEnumerable<TOutputSubmember>>;
			return memberToMemberConstraint != null && (memberToMemberConstraint is CorrespondingMemberEquals<TInputMember, IEnumerable<TOutputSubmember>> || memberToMemberConstraint is MemberPrefix<TInputMember, TOutputSubmember> || memberToMemberConstraint is MemberSubset<TInputMember, TOutputSubmember>) && ValueEquality.Comparer.Equals(memberToMemberConstraint.InputMember, base.InputMember) && memberToMemberConstraint.OutputMember.Intersect(base.OutputMember, ValueEquality<TOutputSubmember>.Instance).Any<TOutputSubmember>();
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x00024A57 File Offset: 0x00022C57
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
