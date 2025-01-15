using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.Constraints
{
	// Token: 0x0200021F RID: 543
	public class CorrespondingMemberDoesNotEqual<TInputMember, TOutputMember> : MemberToMemberConstraint<TInputMember, TOutputMember>
	{
		// Token: 0x06000BB4 RID: 2996 RVA: 0x0002399F File Offset: 0x00021B9F
		public CorrespondingMemberDoesNotEqual(TInputMember inputMember, TOutputMember outputMember, bool isSoft = false)
			: base(inputMember, outputMember, isSoft)
		{
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x000239AA File Offset: 0x00021BAA
		protected override bool Valid(Program<IEnumerable<TInputMember>, IEnumerable<TOutputMember>> program, TOutputMember programOutputMember)
		{
			return !ValueEquality.Comparer.Equals(programOutputMember, base.OutputMember);
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x000239CC File Offset: 0x00021BCC
		public override bool ConflictsWith(Constraint<IEnumerable<TInputMember>, IEnumerable<TOutputMember>> other)
		{
			Example<IEnumerable<TInputMember>, IEnumerable<TOutputMember>> example = other as Example<IEnumerable<TInputMember>, IEnumerable<TOutputMember>>;
			if (example != null)
			{
				return example.Input.Zip(example.Output, (TInputMember inputMember, TOutputMember outputMember) => ValueEquality.Comparer.Equals(inputMember, base.InputMember) && ValueEquality.Comparer.Equals(outputMember, base.OutputMember)).Any((bool b) => b);
			}
			CorrespondingMemberEquals<TInputMember, TOutputMember> correspondingMemberEquals = other as CorrespondingMemberEquals<TInputMember, TOutputMember>;
			return correspondingMemberEquals != null && ValueEquality.Comparer.Equals(correspondingMemberEquals.InputMember, base.InputMember) && ValueEquality.Comparer.Equals(correspondingMemberEquals.OutputMember, base.OutputMember);
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x00023A81 File Offset: 0x00021C81
		public override string ToString()
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("'{0}' !-> '{1}'", new object[] { base.InputMember, base.OutputMember }));
		}
	}
}
