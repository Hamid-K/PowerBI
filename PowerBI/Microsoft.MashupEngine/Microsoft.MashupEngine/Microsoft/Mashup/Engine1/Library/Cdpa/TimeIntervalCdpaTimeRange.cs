using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DB3 RID: 3507
	[DataContract]
	internal class TimeIntervalCdpaTimeRange : CdpaTimeRange, IIntersectable<TimeIntervalCdpaTimeRange>, IUnionable<TimeIntervalCdpaTimeRange>
	{
		// Token: 0x17001C26 RID: 7206
		// (get) Token: 0x06005F51 RID: 24401 RVA: 0x0014852D File Offset: 0x0014672D
		[DataMember(Name = "timeSpan", IsRequired = true)]
		public override string TimeSpanKind
		{
			get
			{
				return "Custom";
			}
		}

		// Token: 0x17001C27 RID: 7207
		// (get) Token: 0x06005F52 RID: 24402 RVA: 0x00148534 File Offset: 0x00146734
		[DataMember(Name = "timeInterval", IsRequired = true)]
		public string TimeInterval
		{
			get
			{
				DateTime? dateTime = this.UtcStart;
				string text = DateTimeOffsetSerializer.ToString((dateTime != null) ? new DateTimeOffset?(dateTime.GetValueOrDefault()) : null);
				string text2 = "/";
				dateTime = this.UtcEnd;
				return text + text2 + DateTimeOffsetSerializer.ToString((dateTime != null) ? new DateTimeOffset?(dateTime.GetValueOrDefault()) : null);
			}
		}

		// Token: 0x17001C28 RID: 7208
		// (get) Token: 0x06005F53 RID: 24403 RVA: 0x001485AD File Offset: 0x001467AD
		// (set) Token: 0x06005F54 RID: 24404 RVA: 0x001485B5 File Offset: 0x001467B5
		public DateTime? UtcStart { get; set; }

		// Token: 0x17001C29 RID: 7209
		// (get) Token: 0x06005F55 RID: 24405 RVA: 0x001485BE File Offset: 0x001467BE
		// (set) Token: 0x06005F56 RID: 24406 RVA: 0x001485C6 File Offset: 0x001467C6
		public DateTime? UtcEnd { get; set; }

		// Token: 0x06005F57 RID: 24407 RVA: 0x001485CF File Offset: 0x001467CF
		public TimeIntervalCdpaTimeRange Intersect(TimeIntervalCdpaTimeRange other)
		{
			return new TimeIntervalCdpaTimeRange
			{
				UtcStart = TemporalExtensions.Max(this.UtcStart, other.UtcStart),
				UtcEnd = TemporalExtensions.Min(this.UtcEnd, other.UtcEnd)
			};
		}

		// Token: 0x06005F58 RID: 24408 RVA: 0x00148604 File Offset: 0x00146804
		public TimeIntervalCdpaTimeRange Union(TimeIntervalCdpaTimeRange other)
		{
			return new TimeIntervalCdpaTimeRange
			{
				UtcStart = TemporalExtensions.Min(this.UtcStart, other.UtcStart),
				UtcEnd = TemporalExtensions.Max(this.UtcEnd, other.UtcEnd)
			};
		}

		// Token: 0x06005F59 RID: 24409 RVA: 0x00148639 File Offset: 0x00146839
		public override TimePartsFilteredTimeIntervalCdpaTimeRange ToTimePartsFilteredTimeInterval()
		{
			return new TimePartsFilteredTimeIntervalCdpaTimeRange
			{
				Interval = this
			};
		}

		// Token: 0x06005F5A RID: 24410 RVA: 0x00148647 File Offset: 0x00146847
		public override string ToString()
		{
			return this.TimeInterval;
		}
	}
}
