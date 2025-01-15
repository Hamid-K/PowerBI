using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DB4 RID: 3508
	internal class TimePartsFilteredTimeIntervalCdpaTimeRange : CdpaTimeRange, IIntersectable<TimePartsFilteredTimeIntervalCdpaTimeRange>, IUnionable<TimePartsFilteredTimeIntervalCdpaTimeRange>
	{
		// Token: 0x17001C2A RID: 7210
		// (get) Token: 0x06005F5C RID: 24412 RVA: 0x0014864F File Offset: 0x0014684F
		[DataMember(Name = "timeSpan", IsRequired = true)]
		public override string TimeSpanKind
		{
			get
			{
				return "TimePartsFilteredInterval";
			}
		}

		// Token: 0x17001C2B RID: 7211
		// (get) Token: 0x06005F5D RID: 24413 RVA: 0x00148656 File Offset: 0x00146856
		// (set) Token: 0x06005F5E RID: 24414 RVA: 0x0014865E File Offset: 0x0014685E
		public TimeIntervalCdpaTimeRange Interval { get; set; }

		// Token: 0x17001C2C RID: 7212
		// (get) Token: 0x06005F5F RID: 24415 RVA: 0x00148667 File Offset: 0x00146867
		// (set) Token: 0x06005F60 RID: 24416 RVA: 0x0014866F File Offset: 0x0014686F
		public TimePartsFilter PartsFilter { get; set; }

		// Token: 0x06005F61 RID: 24417 RVA: 0x00148678 File Offset: 0x00146878
		public TimePartsFilteredTimeIntervalCdpaTimeRange Intersect(TimePartsFilteredTimeIntervalCdpaTimeRange other)
		{
			return new TimePartsFilteredTimeIntervalCdpaTimeRange
			{
				Interval = this.Interval.NullableIntersect(other.Interval),
				PartsFilter = this.PartsFilter.NullableIntersect(other.PartsFilter)
			};
		}

		// Token: 0x06005F62 RID: 24418 RVA: 0x001486AD File Offset: 0x001468AD
		public TimePartsFilteredTimeIntervalCdpaTimeRange Union(TimePartsFilteredTimeIntervalCdpaTimeRange other)
		{
			return new TimePartsFilteredTimeIntervalCdpaTimeRange
			{
				Interval = this.Interval.NullableUnion(other.Interval),
				PartsFilter = this.PartsFilter.NullableUnion(other.PartsFilter)
			};
		}

		// Token: 0x06005F63 RID: 24419 RVA: 0x001486E2 File Offset: 0x001468E2
		public TimeIntervalCdpaTimeRange ToTimeInterval()
		{
			if (this.PartsFilter == null)
			{
				return this.Interval;
			}
			return this.PartsFilter.ApplyTo(this.Interval);
		}

		// Token: 0x06005F64 RID: 24420 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public override TimePartsFilteredTimeIntervalCdpaTimeRange ToTimePartsFilteredTimeInterval()
		{
			return this;
		}
	}
}
