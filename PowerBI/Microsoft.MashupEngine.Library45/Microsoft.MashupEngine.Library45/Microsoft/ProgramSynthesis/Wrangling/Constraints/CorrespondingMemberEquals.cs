using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.Constraints
{
	// Token: 0x02000223 RID: 547
	public class CorrespondingMemberEquals<TInputMember, TOutputMember> : MemberToMemberConstraint<TInputMember, TOutputMember>
	{
		// Token: 0x06000BC4 RID: 3012 RVA: 0x0002399F File Offset: 0x00021B9F
		public CorrespondingMemberEquals(TInputMember inputMember, TOutputMember outputMember, bool isSoft = false)
			: base(inputMember, outputMember, isSoft)
		{
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x00023C97 File Offset: 0x00021E97
		protected override bool Valid(Program<IEnumerable<TInputMember>, IEnumerable<TOutputMember>> program, TOutputMember programOutputMember)
		{
			return ValueEquality.Comparer.Equals(programOutputMember, base.OutputMember);
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x00023CB4 File Offset: 0x00021EB4
		public override bool ConflictsWith(Constraint<IEnumerable<TInputMember>, IEnumerable<TOutputMember>> other)
		{
			Example<IEnumerable<TInputMember>, IEnumerable<TOutputMember>> example = other as Example<IEnumerable<TInputMember>, IEnumerable<TOutputMember>>;
			if (example != null)
			{
				return example.Input.Zip(example.Output, (TInputMember inputMember, TOutputMember outputMember) => ValueEquality.Comparer.Equals(inputMember, base.InputMember) && !ValueEquality.Comparer.Equals(outputMember, base.OutputMember)).Any((bool b) => b);
			}
			CorrespondingMemberEquals<TInputMember, TOutputMember> correspondingMemberEquals = other as CorrespondingMemberEquals<TInputMember, TOutputMember>;
			if (correspondingMemberEquals != null)
			{
				return ValueEquality.Comparer.Equals(correspondingMemberEquals.InputMember, base.InputMember) && !ValueEquality.Comparer.Equals(correspondingMemberEquals.OutputMember, base.OutputMember);
			}
			CorrespondingMemberDoesNotEqual<TInputMember, TOutputMember> correspondingMemberDoesNotEqual = other as CorrespondingMemberDoesNotEqual<TInputMember, TOutputMember>;
			return correspondingMemberDoesNotEqual != null && ValueEquality.Comparer.Equals(correspondingMemberDoesNotEqual.InputMember, base.InputMember) && ValueEquality.Comparer.Equals(correspondingMemberDoesNotEqual.OutputMember, base.OutputMember);
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x00023DC1 File Offset: 0x00021FC1
		public override string ToString()
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("'{0}' -> '{1}'", new object[] { base.InputMember, base.OutputMember }));
		}
	}
}
