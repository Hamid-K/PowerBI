using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Matching.Text.Learning;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Matching.Text
{
	// Token: 0x020011A2 RID: 4514
	public abstract class ClusterConstraint<TImpl, TInput, TOutput> : Constraint<TInput, TOutput>, IOptionConstraint<Witnesses.Options> where TImpl : ClusterConstraint<TImpl, TInput, TOutput> where TInput : IEquatable<TInput>
	{
		// Token: 0x17001707 RID: 5895
		// (get) Token: 0x0600866E RID: 34414 RVA: 0x001C3B83 File Offset: 0x001C1D83
		internal IReadOnlyCollection<TInput> Inputs
		{
			get
			{
				return this.InputSet;
			}
		}

		// Token: 0x0600866F RID: 34415 RVA: 0x001C3B8B File Offset: 0x001C1D8B
		protected ClusterConstraint(IEnumerable<TInput> inputs)
		{
			this.InputSet = inputs.ConvertToHashSet<TInput>();
		}

		// Token: 0x06008670 RID: 34416 RVA: 0x001C3B9F File Offset: 0x001C1D9F
		public override bool Equals(Constraint<TInput, TOutput> other)
		{
			return other != null && (this == other || (!(other.GetType() != base.GetType()) && this.Equals((TImpl)((object)other))));
		}

		// Token: 0x06008671 RID: 34417 RVA: 0x001C3BCD File Offset: 0x001C1DCD
		public bool Equals(TImpl other)
		{
			return other != null && (this == other || this.InputSet.SetEquals(other.InputSet));
		}

		// Token: 0x06008672 RID: 34418
		public abstract override bool ConflictsWith(Constraint<TInput, TOutput> other);

		// Token: 0x06008673 RID: 34419
		public abstract override bool Valid(Program<TInput, TOutput> program);

		// Token: 0x06008674 RID: 34420
		public abstract void SetOptions(Witnesses.Options options);

		// Token: 0x06008675 RID: 34421 RVA: 0x001C3BFC File Offset: 0x001C1DFC
		public override int GetHashCode()
		{
			int? hashCode = this._hashCode;
			return ((hashCode != null) ? hashCode : (this._hashCode = new int?(this.Inputs.OrderIndependentHashCode<TInput>()))).Value;
		}

		// Token: 0x0400378C RID: 14220
		protected readonly HashSet<TInput> InputSet;

		// Token: 0x0400378D RID: 14221
		private int? _hashCode;
	}
}
