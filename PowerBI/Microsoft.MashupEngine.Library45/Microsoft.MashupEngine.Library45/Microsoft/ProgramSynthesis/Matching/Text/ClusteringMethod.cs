using System;
using Microsoft.ProgramSynthesis.Matching.Text.Learning;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Matching.Text
{
	// Token: 0x020011A7 RID: 4519
	public sealed class ClusteringMethod<TInput, TOutput> : Constraint<TInput, TOutput>, IOptionConstraint<Witnesses.Options>
	{
		// Token: 0x06008684 RID: 34436 RVA: 0x001C3DD8 File Offset: 0x001C1FD8
		public ClusteringMethod(ClusteringAlgorithm? algorithm)
		{
			this.Algorithm = algorithm;
		}

		// Token: 0x17001708 RID: 5896
		// (get) Token: 0x06008685 RID: 34437 RVA: 0x001C3DE7 File Offset: 0x001C1FE7
		public ClusteringAlgorithm? Algorithm { get; }

		// Token: 0x06008686 RID: 34438 RVA: 0x001C3DEF File Offset: 0x001C1FEF
		public override bool Equals(Constraint<TInput, TOutput> other)
		{
			return other != null && (this == other || (!(other.GetType() != base.GetType()) && this.Equals((ClusteringMethod<TInput, TOutput>)other)));
		}

		// Token: 0x06008687 RID: 34439 RVA: 0x001C3E20 File Offset: 0x001C2020
		public bool Equals(ClusteringMethod<TInput, TOutput> other)
		{
			if (other == null)
			{
				return false;
			}
			if (this == other)
			{
				return true;
			}
			ClusteringAlgorithm? algorithm = this.Algorithm;
			ClusteringAlgorithm? algorithm2 = other.Algorithm;
			return (algorithm.GetValueOrDefault() == algorithm2.GetValueOrDefault()) & (algorithm != null == (algorithm2 != null));
		}

		// Token: 0x06008688 RID: 34440 RVA: 0x001C3E67 File Offset: 0x001C2067
		public override bool ConflictsWith(Constraint<TInput, TOutput> other)
		{
			return !this.Equals(other as ClusteringMethod<TInput, TOutput>);
		}

		// Token: 0x06008689 RID: 34441 RVA: 0x001C3E78 File Offset: 0x001C2078
		public override bool Valid(Program<TInput, TOutput> program)
		{
			return this.Algorithm != null && (this.Algorithm.Value == ClusteringAlgorithm.None || this.Algorithm.Value == ClusteringAlgorithm.AHC || this.Algorithm.Value == ClusteringAlgorithm.Sampling);
		}

		// Token: 0x0600868A RID: 34442 RVA: 0x001C3ECC File Offset: 0x001C20CC
		public void SetOptions(Witnesses.Options options)
		{
			options.ClusteringMethod = this.Algorithm;
		}

		// Token: 0x0600868B RID: 34443 RVA: 0x001C3EDC File Offset: 0x001C20DC
		public override int GetHashCode()
		{
			return this.Algorithm.GetHashCode();
		}
	}
}
