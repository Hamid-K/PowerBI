using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Wrangling.Constraints
{
	// Token: 0x0200021A RID: 538
	public abstract class ConstraintOnInput<TInput, TOutput> : Constraint<TInput, TOutput>, IEquatable<ConstraintOnInput<TInput, TOutput>>
	{
		// Token: 0x06000B8B RID: 2955 RVA: 0x0002307A File Offset: 0x0002127A
		protected ConstraintOnInput(TInput input, bool isSoft)
		{
			this.Input = input;
			this.IsSoft = isSoft;
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000B8C RID: 2956 RVA: 0x00023090 File Offset: 0x00021290
		public TInput Input { get; }

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000B8D RID: 2957 RVA: 0x00023098 File Offset: 0x00021298
		public override bool IsSoft { get; }

		// Token: 0x06000B8E RID: 2958 RVA: 0x000230A0 File Offset: 0x000212A0
		public bool Equals(ConstraintOnInput<TInput, TOutput> other)
		{
			return other != null && (this == other || (EqualityComparer<TInput>.Default.Equals(this.Input, other.Input) && this.IsSoft == other.IsSoft));
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x000230D5 File Offset: 0x000212D5
		public override bool Equals(object other)
		{
			return other != null && (this == other || (!(other.GetType() != base.GetType()) && this.Equals((ConstraintOnInput<TInput, TOutput>)other)));
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x00023104 File Offset: 0x00021304
		public override int GetHashCode()
		{
			return (EqualityComparer<TInput>.Default.GetHashCode(this.Input) * 10070587) ^ this.IsSoft.GetHashCode();
		}
	}
}
