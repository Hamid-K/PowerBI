using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Wrangling.Session
{
	// Token: 0x0200010F RID: 271
	public class LearnProgramRequest<TProgram, TInput, TOutput> : Tuple<IImmutableList<TInput>, IImmutableList<Constraint<TInput, TOutput>>>, IEquatable<LearnProgramRequest<TProgram, TInput, TOutput>> where TProgram : Program<TInput, TOutput>
	{
		// Token: 0x06000625 RID: 1573 RVA: 0x0001409C File Offset: 0x0001229C
		public LearnProgramRequest(IImmutableList<TInput> inputs, IImmutableList<Constraint<TInput, TOutput>> constraints)
			: base(inputs, constraints)
		{
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000626 RID: 1574 RVA: 0x000140A6 File Offset: 0x000122A6
		public IImmutableList<TInput> Inputs
		{
			get
			{
				return base.Item1;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000627 RID: 1575 RVA: 0x000140AE File Offset: 0x000122AE
		public IImmutableList<Constraint<TInput, TOutput>> Constraints
		{
			get
			{
				return base.Item2;
			}
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x000140B8 File Offset: 0x000122B8
		public bool Equals(LearnProgramRequest<TProgram, TInput, TOutput> other)
		{
			return !(other == null) && this.Inputs.Count == other.Inputs.Count && this.Constraints.Count == other.Constraints.Count && (this.Constraints == other.Constraints || this.Constraints.SequenceEqual(other.Constraints)) && (this.Inputs == other.Inputs || this.Inputs.SequenceEqual(other.Inputs, ValueEquality<TInput>.Instance));
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x0001414D File Offset: 0x0001234D
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((LearnProgramRequest<TProgram, TInput, TOutput>)obj)));
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x0001417C File Offset: 0x0001237C
		public override int GetHashCode()
		{
			if (this._hashCode == null)
			{
				this._hashCode = new int?((10810627 * this.Inputs.OrderDependentHashCode(ValueEquality<TInput>.Instance)) ^ this.Constraints.OrderDependentHashCode<Constraint<TInput, TOutput>>());
			}
			return this._hashCode.Value;
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x0000BE9E File Offset: 0x0000A09E
		public static bool operator ==(LearnProgramRequest<TProgram, TInput, TOutput> left, LearnProgramRequest<TProgram, TInput, TOutput> right)
		{
			return object.Equals(left, right);
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x0000BEA7 File Offset: 0x0000A0A7
		public static bool operator !=(LearnProgramRequest<TProgram, TInput, TOutput> left, LearnProgramRequest<TProgram, TInput, TOutput> right)
		{
			return !object.Equals(left, right);
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x000141CE File Offset: 0x000123CE
		internal virtual LearnProgramRequest<TProgram, TInput, TOutput> WithoutInputs()
		{
			return new LearnProgramRequest<TProgram, TInput, TOutput>(ImmutableList<TInput>.Empty, this.Constraints);
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x000141E0 File Offset: 0x000123E0
		public bool IsConstrained(TInput input)
		{
			return this.Constraints.OfType<ConstraintOnInput<TInput, TOutput>>().Any((ConstraintOnInput<TInput, TOutput> c) => ValueEquality<TInput>.Instance.Equals(c.Input, input));
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x00014216 File Offset: 0x00012416
		internal HashSet<TInput> ConstrainedInputs()
		{
			return (from c in this.Constraints.OfType<ConstraintOnInput<TInput, TOutput>>()
				select c.Input).ConvertToHashSet(ValueEquality<TInput>.Instance);
		}

		// Token: 0x0400029A RID: 666
		private int? _hashCode;
	}
}
