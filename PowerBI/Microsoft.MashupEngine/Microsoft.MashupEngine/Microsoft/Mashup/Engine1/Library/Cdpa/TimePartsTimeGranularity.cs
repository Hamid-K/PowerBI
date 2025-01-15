using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000E58 RID: 3672
	[DataContract]
	internal class TimePartsTimeGranularity : TimeParts<short>, ITimeGranularity, IEquatable<ITimeGranularity>
	{
		// Token: 0x060062CB RID: 25291 RVA: 0x001533AC File Offset: 0x001515AC
		public TimePartsTimeGranularity()
		{
			base.Years = TimePartsTimeGranularity.CoarsestPart;
			base.Months = TimePartsTimeGranularity.CoarsestPart;
			base.Days = TimePartsTimeGranularity.CoarsestPart;
			base.Hours = TimePartsTimeGranularity.CoarsestPart;
			base.Minutes = TimePartsTimeGranularity.CoarsestPart;
			base.Seconds = TimePartsTimeGranularity.CoarsestPart;
		}

		// Token: 0x060062CC RID: 25292 RVA: 0x00153401 File Offset: 0x00151601
		public bool Equals(ITimeGranularity other)
		{
			return this.Equals(other as TimeParts<short>);
		}

		// Token: 0x060062CD RID: 25293 RVA: 0x0015340F File Offset: 0x0015160F
		public override bool Equals(TimeParts<short> other)
		{
			return other is TimePartsTimeGranularity && base.Equals(other);
		}

		// Token: 0x060062CE RID: 25294 RVA: 0x00153424 File Offset: 0x00151624
		public DateTime AddFinestPartTo(DateTime dateTime)
		{
			if (base.Seconds != TimePartsTimeGranularity.CoarsestPart)
			{
				return dateTime.AddSeconds((double)base.Seconds);
			}
			if (base.Minutes != TimePartsTimeGranularity.CoarsestPart)
			{
				return dateTime.AddMinutes((double)base.Minutes);
			}
			if (base.Hours != TimePartsTimeGranularity.CoarsestPart)
			{
				return dateTime.AddHours((double)base.Hours);
			}
			if (base.Days != TimePartsTimeGranularity.CoarsestPart)
			{
				return dateTime.AddDays((double)base.Days);
			}
			if (base.Months != TimePartsTimeGranularity.CoarsestPart)
			{
				return dateTime.AddMonths((int)base.Months);
			}
			if (base.Years != TimePartsTimeGranularity.CoarsestPart)
			{
				return dateTime.AddYears((int)base.Years);
			}
			return DateTime.MaxValue;
		}

		// Token: 0x060062CF RID: 25295 RVA: 0x001534DC File Offset: 0x001516DC
		public DateTime SubtractFinestPartFrom(DateTime dateTime)
		{
			if (base.Seconds != TimePartsTimeGranularity.CoarsestPart)
			{
				return dateTime.AddSeconds((double)(-(double)base.Seconds));
			}
			if (base.Minutes != TimePartsTimeGranularity.CoarsestPart)
			{
				return dateTime.AddMinutes((double)(-(double)base.Minutes));
			}
			if (base.Hours != TimePartsTimeGranularity.CoarsestPart)
			{
				return dateTime.AddHours((double)(-(double)base.Hours));
			}
			if (base.Days != TimePartsTimeGranularity.CoarsestPart)
			{
				return dateTime.AddDays((double)(-(double)base.Days));
			}
			if (base.Months != TimePartsTimeGranularity.CoarsestPart)
			{
				return dateTime.AddMonths((int)(-(int)base.Months));
			}
			if (base.Years != TimePartsTimeGranularity.CoarsestPart)
			{
				return dateTime.AddYears((int)(-(int)base.Years));
			}
			return DateTime.MinValue;
		}

		// Token: 0x040035B8 RID: 13752
		public static readonly short CoarsestPart = short.MaxValue;

		// Token: 0x040035B9 RID: 13753
		public static readonly short FinestPart = 0;

		// Token: 0x040035BA RID: 13754
		public static readonly TimePartsTimeGranularity Coarsest = new TimePartsTimeGranularity();

		// Token: 0x040035BB RID: 13755
		public static readonly TimePartsTimeGranularity Finest = new TimePartsTimeGranularity
		{
			Years = 0,
			Months = 0,
			Days = 0,
			Hours = 0,
			Minutes = 0,
			Seconds = 0
		};
	}
}
