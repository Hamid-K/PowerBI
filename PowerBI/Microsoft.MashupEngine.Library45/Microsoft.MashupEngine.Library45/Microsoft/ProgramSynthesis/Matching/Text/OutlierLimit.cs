using System;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Matching.Text.Learning;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Matching.Text
{
	// Token: 0x020011AB RID: 4523
	public class OutlierLimit<TInput, TOutput> : Constraint<TInput, TOutput>, IOptionConstraint<Witnesses.Options>
	{
		// Token: 0x17001712 RID: 5906
		// (get) Token: 0x060086AB RID: 34475 RVA: 0x001C451B File Offset: 0x001C271B
		public double MaxOutlierRate { get; }

		// Token: 0x060086AC RID: 34476 RVA: 0x001C4524 File Offset: 0x001C2724
		public OutlierLimit(double maxOutlierRate)
		{
			if (maxOutlierRate > 1.0 || maxOutlierRate < 0.0)
			{
				throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("Invalid range for outlier rate: 0.0 <= {0} <= 1.0", new object[] { maxOutlierRate })));
			}
			this.MaxOutlierRate = maxOutlierRate;
		}

		// Token: 0x060086AD RID: 34477 RVA: 0x001C457A File Offset: 0x001C277A
		public override bool ConflictsWith(Constraint<TInput, TOutput> other)
		{
			return other is OutlierLimit<TInput, TOutput> && !this.Equals(other);
		}

		// Token: 0x060086AE RID: 34478 RVA: 0x001C4590 File Offset: 0x001C2790
		public override bool Equals(Constraint<TInput, TOutput> other)
		{
			return other != null && (this == other || (!(other.GetType() != base.GetType()) && this.Equals((OutlierLimit<TInput, TOutput>)other)));
		}

		// Token: 0x060086AF RID: 34479 RVA: 0x001C45BE File Offset: 0x001C27BE
		public bool Equals(OutlierLimit<TInput, TOutput> other)
		{
			return other != null && (this == other || this.MaxOutlierRate == other.MaxOutlierRate);
		}

		// Token: 0x060086B0 RID: 34480 RVA: 0x001C45DC File Offset: 0x001C27DC
		public override int GetHashCode()
		{
			return this.MaxOutlierRate.GetHashCode();
		}

		// Token: 0x060086B1 RID: 34481 RVA: 0x001C45F7 File Offset: 0x001C27F7
		public void SetOptions(Witnesses.Options options)
		{
			options.MaxOutlierRate = this.MaxOutlierRate;
		}

		// Token: 0x060086B2 RID: 34482 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool Valid(Program<TInput, TOutput> program)
		{
			return true;
		}
	}
}
