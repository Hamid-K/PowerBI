using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Wrangling.Constraints
{
	// Token: 0x02000241 RID: 577
	public abstract class ValueToMemberConstraint<TInput, TOutputMember> : ConstraintOnInput<TInput, IEnumerable<TOutputMember>>, IEquatable<ValueToMemberConstraint<TInput, TOutputMember>>
	{
		// Token: 0x06000C52 RID: 3154 RVA: 0x0002533E File Offset: 0x0002353E
		protected ValueToMemberConstraint(TInput input, TOutputMember outputMember, bool isSoft)
			: base(input, isSoft)
		{
			this.OutputMember = outputMember;
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000C53 RID: 3155 RVA: 0x0002534F File Offset: 0x0002354F
		public TOutputMember OutputMember { get; }

		// Token: 0x06000C54 RID: 3156 RVA: 0x00025357 File Offset: 0x00023557
		public bool Equals(ValueToMemberConstraint<TInput, TOutputMember> other)
		{
			return other != null && (this == other || (base.Equals(other) && EqualityComparer<TOutputMember>.Default.Equals(this.OutputMember, other.OutputMember)));
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x00025385 File Offset: 0x00023585
		public override bool Equals(object other)
		{
			return other != null && (this == other || (!(other.GetType() != base.GetType()) && this.Equals((ValueToMemberConstraint<TInput, TOutputMember>)other)));
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x00024CEC File Offset: 0x00022EEC
		public override bool Equals(Constraint<TInput, IEnumerable<TOutputMember>> other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x000253B3 File Offset: 0x000235B3
		public override int GetHashCode()
		{
			return (base.GetHashCode() * 3548023) ^ EqualityComparer<TOutputMember>.Default.GetHashCode(this.OutputMember);
		}
	}
}
