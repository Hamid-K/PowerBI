using System;
using Microsoft.ProgramSynthesis.Matching.Text.Learning;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Matching.Text
{
	// Token: 0x020011A8 RID: 4520
	public class ClusteringParameters<TInput, TOutput> : Constraint<TInput, TOutput>, IOptionConstraint<Witnesses.Options>
	{
		// Token: 0x0600868C RID: 34444 RVA: 0x001C3EFD File Offset: 0x001C20FD
		public ClusteringParameters(double? sampleSizeFactor, double? thetaFactor)
		{
			this.SampleSizeFactor = sampleSizeFactor;
			this.ThetaFactor = thetaFactor;
			this._hashCode = null;
		}

		// Token: 0x17001709 RID: 5897
		// (get) Token: 0x0600868D RID: 34445 RVA: 0x001C3F1F File Offset: 0x001C211F
		public double? SampleSizeFactor { get; }

		// Token: 0x1700170A RID: 5898
		// (get) Token: 0x0600868E RID: 34446 RVA: 0x001C3F27 File Offset: 0x001C2127
		public double? ThetaFactor { get; }

		// Token: 0x0600868F RID: 34447 RVA: 0x001C3F30 File Offset: 0x001C2130
		public void SetOptions(Witnesses.Options options)
		{
			options.SampleSizeFactor = this.SampleSizeFactor ?? options.SampleSizeFactor;
			double? thetaFactor = this.ThetaFactor;
			options.ThetaFactor = ((thetaFactor != null) ? thetaFactor : options.ThetaFactor);
		}

		// Token: 0x06008690 RID: 34448 RVA: 0x001C3F81 File Offset: 0x001C2181
		public override bool Equals(Constraint<TInput, TOutput> other)
		{
			return other != null && (this == other || (!(other.GetType() != base.GetType()) && this.Equals((ClusteringParameters<TInput, TOutput>)other)));
		}

		// Token: 0x06008691 RID: 34449 RVA: 0x001C3FAF File Offset: 0x001C21AF
		public override bool ConflictsWith(Constraint<TInput, TOutput> other)
		{
			return other is ClusteringParameters<TInput, TOutput> && !this.Equals(other);
		}

		// Token: 0x06008692 RID: 34450 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool Valid(Program<TInput, TOutput> program)
		{
			return true;
		}

		// Token: 0x06008693 RID: 34451 RVA: 0x001C3FC8 File Offset: 0x001C21C8
		public bool Equals(ClusteringParameters<TInput, TOutput> other)
		{
			if (other == null)
			{
				return false;
			}
			if (this == other)
			{
				return true;
			}
			double? num = this.SampleSizeFactor;
			double? num2 = other.SampleSizeFactor;
			if ((num.GetValueOrDefault() == num2.GetValueOrDefault()) & (num != null == (num2 != null)))
			{
				num2 = this.ThetaFactor;
				num = other.ThetaFactor;
				return (num2.GetValueOrDefault() == num.GetValueOrDefault()) & (num2 != null == (num != null));
			}
			return false;
		}

		// Token: 0x06008694 RID: 34452 RVA: 0x001C4044 File Offset: 0x001C2244
		public override int GetHashCode()
		{
			if (this._hashCode != null)
			{
				return this._hashCode.Value;
			}
			int? num = (this._hashCode = new int?((45269999 * this.SampleSizeFactor.GetHashCode()) ^ this.ThetaFactor.GetHashCode()));
			return num.Value;
		}

		// Token: 0x04003791 RID: 14225
		private int? _hashCode;
	}
}
