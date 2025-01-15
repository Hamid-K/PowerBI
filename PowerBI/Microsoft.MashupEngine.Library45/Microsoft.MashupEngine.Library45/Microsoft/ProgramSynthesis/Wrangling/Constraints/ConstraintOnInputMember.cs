using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.Constraints
{
	// Token: 0x0200021B RID: 539
	public abstract class ConstraintOnInputMember<TInputMember, TOutput> : Constraint<IEnumerable<TInputMember>, TOutput>, IEquatable<ConstraintOnInputMember<TInputMember, TOutput>>
	{
		// Token: 0x06000B91 RID: 2961 RVA: 0x00023136 File Offset: 0x00021336
		protected ConstraintOnInputMember(TInputMember inputMember, bool isSoft)
		{
			this.InputMember = inputMember;
			this.IsSoft = isSoft;
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000B92 RID: 2962 RVA: 0x0002314C File Offset: 0x0002134C
		public TInputMember InputMember { get; }

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000B93 RID: 2963 RVA: 0x00023154 File Offset: 0x00021354
		internal override Optional<IEnumerable<TInputMember>> PossibleInput
		{
			get
			{
				return Seq.Of<TInputMember>(new TInputMember[] { this.InputMember }).Some<IEnumerable<TInputMember>>();
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000B94 RID: 2964 RVA: 0x00023173 File Offset: 0x00021373
		public override bool IsSoft { get; }

		// Token: 0x06000B95 RID: 2965 RVA: 0x0002317B File Offset: 0x0002137B
		public bool Equals(ConstraintOnInputMember<TInputMember, TOutput> other)
		{
			return other != null && (this == other || (EqualityComparer<TInputMember>.Default.Equals(this.InputMember, other.InputMember) && this.IsSoft == other.IsSoft));
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x000231B0 File Offset: 0x000213B0
		public sealed override bool Valid(Program<IEnumerable<TInputMember>, TOutput> program)
		{
			throw new InvalidOperationException("Cannot validate an InputMemberConstraint without an input.");
		}

		// Token: 0x06000B97 RID: 2967
		protected abstract bool Valid(Program<IEnumerable<TInputMember>, TOutput> program, IReadOnlyList<TInputMember> input, TOutput output);

		// Token: 0x06000B98 RID: 2968 RVA: 0x000231BC File Offset: 0x000213BC
		public override bool Valid(Program<IEnumerable<TInputMember>, TOutput> program, IEnumerable<IEnumerable<TInputMember>> inputs)
		{
			bool flag = true;
			bool flag2 = false;
			foreach (IEnumerable<TInputMember> enumerable in inputs)
			{
				IReadOnlyList<TInputMember> readOnlyList = (enumerable as IReadOnlyList<TInputMember>) ?? enumerable.ToList<TInputMember>();
				if (readOnlyList.Contains(this.InputMember) && (!flag2 || readOnlyList.Count != 1))
				{
					flag2 = true;
					flag &= this.Valid(program, readOnlyList, program.Run(readOnlyList));
				}
			}
			if (!flag2)
			{
				throw new ArgumentException("No valid inputs found.", "inputs");
			}
			return flag;
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x0002325C File Offset: 0x0002145C
		public override bool Equals(object other)
		{
			return other != null && (this == other || (!(other.GetType() != base.GetType()) && this.Equals((ConstraintOnInputMember<TInputMember, TOutput>)other)));
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x0002328C File Offset: 0x0002148C
		public override int GetHashCode()
		{
			return (EqualityComparer<TInputMember>.Default.GetHashCode(this.InputMember) * 5585989) ^ this.IsSoft.GetHashCode();
		}
	}
}
