using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.Constraints
{
	// Token: 0x02000219 RID: 537
	public abstract class Constraint<TInput, TOutput> : IConstraint, IEquatable<Constraint<TInput, TOutput>>
	{
		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000B7F RID: 2943 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public virtual bool IsSoft
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000B80 RID: 2944
		public abstract bool Equals(Constraint<TInput, TOutput> other);

		// Token: 0x06000B81 RID: 2945
		public abstract bool ConflictsWith(Constraint<TInput, TOutput> other);

		// Token: 0x06000B82 RID: 2946
		public abstract bool Valid(Program<TInput, TOutput> program);

		// Token: 0x06000B83 RID: 2947 RVA: 0x00023032 File Offset: 0x00021232
		public virtual bool Valid(Program<TInput, TOutput> program, IEnumerable<TInput> inputs)
		{
			return this.Valid(program);
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x0002303B File Offset: 0x0002123B
		public virtual bool Valid(Program<TInput, TOutput> program, IEnumerable<TInput> inputs, IReadOnlyCollection<Constraint<TInput, TOutput>> allConstraints)
		{
			return this.Valid(program, inputs);
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x0000BE9E File Offset: 0x0000A09E
		public static bool operator ==(Constraint<TInput, TOutput> left, Constraint<TInput, TOutput> right)
		{
			return object.Equals(left, right);
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x0000BEA7 File Offset: 0x0000A0A7
		public static bool operator !=(Constraint<TInput, TOutput> left, Constraint<TInput, TOutput> right)
		{
			return !object.Equals(left, right);
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x00023045 File Offset: 0x00021245
		public override bool Equals(object other)
		{
			return other != null && (this == other || (!(other.GetType() != base.GetType()) && this.Equals((Constraint<TInput, TOutput>)other)));
		}

		// Token: 0x06000B88 RID: 2952
		public abstract override int GetHashCode();

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000B89 RID: 2953 RVA: 0x00023073 File Offset: 0x00021273
		internal virtual Optional<TInput> PossibleInput
		{
			get
			{
				return Optional<TInput>.Nothing;
			}
		}
	}
}
