using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.Constraints
{
	// Token: 0x02000239 RID: 569
	public abstract class MemberToSubmemberConstraint<TInputMember, TOutputSubmember> : ConstraintOnInputMember<TInputMember, IEnumerable<IEnumerable<TOutputSubmember>>>, IEquatable<MemberToSubmemberConstraint<TInputMember, TOutputSubmember>>
	{
		// Token: 0x06000C2B RID: 3115 RVA: 0x00024D14 File Offset: 0x00022F14
		protected MemberToSubmemberConstraint(TInputMember inputMember, TOutputSubmember outputSubmember, bool isSoft)
			: base(inputMember, isSoft)
		{
			this.OutputSubmember = outputSubmember;
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000C2C RID: 3116 RVA: 0x00024D25 File Offset: 0x00022F25
		public TOutputSubmember OutputSubmember { get; }

		// Token: 0x06000C2D RID: 3117 RVA: 0x00024D2D File Offset: 0x00022F2D
		public bool Equals(MemberToSubmemberConstraint<TInputMember, TOutputSubmember> other)
		{
			return other != null && (this == other || (base.Equals(other) && EqualityComparer<TOutputSubmember>.Default.Equals(this.OutputSubmember, other.OutputSubmember)));
		}

		// Token: 0x06000C2E RID: 3118
		protected abstract bool Valid(Program<IEnumerable<TInputMember>, IEnumerable<IEnumerable<TOutputSubmember>>> program, IEnumerable<TOutputSubmember> outputMember);

		// Token: 0x06000C2F RID: 3119 RVA: 0x00024D5C File Offset: 0x00022F5C
		protected override bool Valid(Program<IEnumerable<TInputMember>, IEnumerable<IEnumerable<TOutputSubmember>>> program, IReadOnlyList<TInputMember> input, IEnumerable<IEnumerable<TOutputSubmember>> output)
		{
			return this.Valid(program, output.ElementAt(input.IndexOf(base.InputMember).Value));
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x00024D8A File Offset: 0x00022F8A
		public override bool Equals(object other)
		{
			return other != null && (this == other || (!(other.GetType() != base.GetType()) && this.Equals((MemberToSubmemberConstraint<TInputMember, TOutputSubmember>)other)));
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x00024CEC File Offset: 0x00022EEC
		public override bool Equals(Constraint<IEnumerable<TInputMember>, IEnumerable<IEnumerable<TOutputSubmember>>> other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x00024DB8 File Offset: 0x00022FB8
		public override int GetHashCode()
		{
			return (base.GetHashCode() * 8358397) ^ EqualityComparer<TOutputSubmember>.Default.GetHashCode(this.OutputSubmember);
		}
	}
}
