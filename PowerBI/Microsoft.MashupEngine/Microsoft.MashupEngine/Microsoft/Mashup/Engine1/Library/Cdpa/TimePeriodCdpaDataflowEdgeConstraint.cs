using System;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DA5 RID: 3493
	internal class TimePeriodCdpaDataflowEdgeConstraint : CdpaDataflowEdgeConstraint
	{
		// Token: 0x06005F1C RID: 24348 RVA: 0x001480AD File Offset: 0x001462AD
		public TimePeriodCdpaDataflowEdgeConstraint(TimeSpan duration)
		{
			this.duration = duration;
		}

		// Token: 0x17001C1B RID: 7195
		// (get) Token: 0x06005F1D RID: 24349 RVA: 0x001480BC File Offset: 0x001462BC
		public TimeSpan Duration
		{
			get
			{
				return this.duration;
			}
		}

		// Token: 0x17001C1C RID: 7196
		// (get) Token: 0x06005F1E RID: 24350 RVA: 0x001480C4 File Offset: 0x001462C4
		public bool DurationIsValid
		{
			get
			{
				return this.duration != TimeSpan.MaxValue;
			}
		}

		// Token: 0x06005F1F RID: 24351 RVA: 0x001480D6 File Offset: 0x001462D6
		public override bool Equals(object other)
		{
			return this.Equals(other as TimePeriodCdpaDataflowEdgeConstraint);
		}

		// Token: 0x06005F20 RID: 24352 RVA: 0x001480D6 File Offset: 0x001462D6
		public override bool Equals(CdpaDataflowEdgeConstraint other)
		{
			return this.Equals(other as TimePeriodCdpaDataflowEdgeConstraint);
		}

		// Token: 0x06005F21 RID: 24353 RVA: 0x001480E4 File Offset: 0x001462E4
		public bool Equals(TimePeriodCdpaDataflowEdgeConstraint other)
		{
			return other != null && this.duration == other.duration;
		}

		// Token: 0x06005F22 RID: 24354 RVA: 0x001480FC File Offset: 0x001462FC
		public override int GetHashCode()
		{
			return this.duration.GetHashCode();
		}

		// Token: 0x0400342C RID: 13356
		public static readonly TimePeriodCdpaDataflowEdgeConstraint Unknown = new TimePeriodCdpaDataflowEdgeConstraint(TimeSpan.MaxValue);

		// Token: 0x0400342D RID: 13357
		private readonly TimeSpan duration;
	}
}
