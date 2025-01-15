using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.Constraints
{
	// Token: 0x02000238 RID: 568
	public abstract class MemberToMemberConstraint<TInputMember, TOutputMember> : ConstraintOnInputMember<TInputMember, IEnumerable<TOutputMember>>, IEquatable<MemberToMemberConstraint<TInputMember, TOutputMember>>
	{
		// Token: 0x06000C23 RID: 3107 RVA: 0x00024C47 File Offset: 0x00022E47
		protected MemberToMemberConstraint(TInputMember inputMember, TOutputMember outputMember, bool isSoft)
			: base(inputMember, isSoft)
		{
			this.OutputMember = outputMember;
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000C24 RID: 3108 RVA: 0x00024C58 File Offset: 0x00022E58
		public TOutputMember OutputMember { get; }

		// Token: 0x06000C25 RID: 3109 RVA: 0x00024C60 File Offset: 0x00022E60
		public bool Equals(MemberToMemberConstraint<TInputMember, TOutputMember> other)
		{
			return other != null && (this == other || (base.Equals(other) && EqualityComparer<TOutputMember>.Default.Equals(this.OutputMember, other.OutputMember)));
		}

		// Token: 0x06000C26 RID: 3110
		protected abstract bool Valid(Program<IEnumerable<TInputMember>, IEnumerable<TOutputMember>> program, TOutputMember programOutputMember);

		// Token: 0x06000C27 RID: 3111 RVA: 0x00024C90 File Offset: 0x00022E90
		protected override bool Valid(Program<IEnumerable<TInputMember>, IEnumerable<TOutputMember>> program, IReadOnlyList<TInputMember> input, IEnumerable<TOutputMember> output)
		{
			return this.Valid(program, output.ElementAt(input.IndexOf(base.InputMember).Value));
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x00024CBE File Offset: 0x00022EBE
		public override bool Equals(object other)
		{
			return other != null && (this == other || (!(other.GetType() != base.GetType()) && this.Equals((MemberToMemberConstraint<TInputMember, TOutputMember>)other)));
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x00024CEC File Offset: 0x00022EEC
		public override bool Equals(Constraint<IEnumerable<TInputMember>, IEnumerable<TOutputMember>> other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x00024CF5 File Offset: 0x00022EF5
		public override int GetHashCode()
		{
			return (base.GetHashCode() * 7456997) ^ EqualityComparer<TOutputMember>.Default.GetHashCode(this.OutputMember);
		}
	}
}
