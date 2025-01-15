using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Wrangling.Constraints
{
	// Token: 0x02000242 RID: 578
	public abstract class ValueToValueConstraint<TInput, TOutput> : ConstraintOnInput<TInput, TOutput>, IEquatable<ValueToValueConstraint<TInput, TOutput>>
	{
		// Token: 0x06000C58 RID: 3160 RVA: 0x000253D2 File Offset: 0x000235D2
		protected ValueToValueConstraint(TInput input, TOutput output, bool isSoft)
			: base(input, isSoft)
		{
			this.Output = output;
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000C59 RID: 3161 RVA: 0x000253E3 File Offset: 0x000235E3
		public TOutput Output { get; }

		// Token: 0x06000C5A RID: 3162 RVA: 0x000253EB File Offset: 0x000235EB
		public bool Equals(ValueToValueConstraint<TInput, TOutput> other)
		{
			return other != null && (this == other || (base.Equals(other) && EqualityComparer<TOutput>.Default.Equals(this.Output, other.Output)));
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x00024CEC File Offset: 0x00022EEC
		public override bool Equals(Constraint<TInput, TOutput> other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x00025419 File Offset: 0x00023619
		public override bool Equals(object other)
		{
			return other != null && (this == other || (!(other.GetType() != base.GetType()) && this.Equals((ValueToValueConstraint<TInput, TOutput>)other)));
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x00025447 File Offset: 0x00023647
		public override int GetHashCode()
		{
			return (base.GetHashCode() * 3148511) ^ EqualityComparer<TOutput>.Default.GetHashCode(this.Output);
		}
	}
}
