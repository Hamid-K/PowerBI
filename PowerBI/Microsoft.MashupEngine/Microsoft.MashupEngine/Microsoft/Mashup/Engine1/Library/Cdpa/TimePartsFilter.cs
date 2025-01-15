using System;
using System.Runtime.Serialization;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000E57 RID: 3671
	[DataContract]
	internal class TimePartsFilter : IIntersectable<TimePartsFilter>, IUnionable<TimePartsFilter>
	{
		// Token: 0x17001CD8 RID: 7384
		// (get) Token: 0x060062BC RID: 25276 RVA: 0x00152FA3 File Offset: 0x001511A3
		// (set) Token: 0x060062BD RID: 25277 RVA: 0x00152FAB File Offset: 0x001511AB
		[DataMember(Name = "utcStart", IsRequired = false)]
		public TimeParts<Value> UtcStart { get; set; }

		// Token: 0x17001CD9 RID: 7385
		// (get) Token: 0x060062BE RID: 25278 RVA: 0x00152FB4 File Offset: 0x001511B4
		// (set) Token: 0x060062BF RID: 25279 RVA: 0x00152FBC File Offset: 0x001511BC
		[DataMember(Name = "utcEnd", IsRequired = false)]
		public TimeParts<Value> UtcEnd { get; set; }

		// Token: 0x060062C0 RID: 25280 RVA: 0x00152FC8 File Offset: 0x001511C8
		public TimeIntervalCdpaTimeRange ApplyTo(TimeIntervalCdpaTimeRange interval)
		{
			TimePartsFilter timePartsFilter = new TimePartsFilter
			{
				UtcStart = TimePartsFilter.ToTimeParts((interval != null) ? interval.UtcStart : null),
				UtcEnd = TimePartsFilter.ToTimeParts((interval != null) ? interval.UtcEnd : null)
			};
			TimePartsFilter timePartsFilter2 = this.NullableIntersect(timePartsFilter);
			return new TimeIntervalCdpaTimeRange
			{
				UtcStart = TimePartsFilter.ToDateTime(timePartsFilter2.UtcStart, false),
				UtcEnd = TimePartsFilter.ToDateTime(timePartsFilter2.UtcEnd, true)
			};
		}

		// Token: 0x060062C1 RID: 25281 RVA: 0x0015304C File Offset: 0x0015124C
		public TimePartsFilter Intersect(TimePartsFilter other)
		{
			return new TimePartsFilter
			{
				UtcStart = TimePartsFilter.Combine(new Func<Value, Value, Value>(TimePartsFilter.Max), this.UtcStart, other.UtcStart),
				UtcEnd = TimePartsFilter.Combine(new Func<Value, Value, Value>(TimePartsFilter.Min), this.UtcEnd, other.UtcEnd)
			};
		}

		// Token: 0x060062C2 RID: 25282 RVA: 0x001530A4 File Offset: 0x001512A4
		public TimePartsFilter Union(TimePartsFilter other)
		{
			return new TimePartsFilter
			{
				UtcStart = TimePartsFilter.Combine(new Func<Value, Value, Value>(TimePartsFilter.Min), this.UtcStart, other.UtcStart),
				UtcEnd = TimePartsFilter.Combine(new Func<Value, Value, Value>(TimePartsFilter.Max), this.UtcEnd, other.UtcEnd)
			};
		}

		// Token: 0x060062C3 RID: 25283 RVA: 0x00152F9B File Offset: 0x0015119B
		public override string ToString()
		{
			return this.ToJson();
		}

		// Token: 0x060062C4 RID: 25284 RVA: 0x001530FC File Offset: 0x001512FC
		public static TimeParts<Value> ToTimeParts(DateTime? dateTime)
		{
			if (dateTime == null)
			{
				return null;
			}
			return new TimeParts<Value>
			{
				Years = NumberValue.New(dateTime.Value.Year),
				Months = NumberValue.New(dateTime.Value.Month),
				Days = NumberValue.New(dateTime.Value.Day),
				Hours = NumberValue.New(dateTime.Value.Hour),
				Minutes = NumberValue.New(dateTime.Value.Minute),
				Seconds = NumberValue.New(dateTime.Value.Second)
			};
		}

		// Token: 0x060062C5 RID: 25285 RVA: 0x001531B8 File Offset: 0x001513B8
		public static DateTime? ToDateTime(TimeParts<Value> timeParts, bool end)
		{
			if (timeParts == null)
			{
				return null;
			}
			bool flag = true;
			short part = TimePartsFilter.GetPart(timeParts.Years, -1, ref flag);
			flag = false;
			short part2 = TimePartsFilter.GetPart(timeParts.Months, end ? 12 : 1, ref flag);
			short num = (short)DateTime.DaysInMonth((int)part, (int)part2);
			flag = false;
			short part3 = TimePartsFilter.GetPart(timeParts.Seconds, end ? 59 : 0, ref flag);
			short part4 = TimePartsFilter.GetPart(timeParts.Minutes, end ? 59 : 0, ref flag);
			short part5 = TimePartsFilter.GetPart(timeParts.Hours, end ? 23 : 0, ref flag);
			short part6 = TimePartsFilter.GetPart(timeParts.Days, end ? num : 1, ref flag);
			short part7 = TimePartsFilter.GetPart(timeParts.Months, end ? 12 : 1, ref flag);
			DateTime dateTime = new DateTime((int)part, (int)part7, (int)part6, (int)part5, (int)part4, (int)part3, DateTimeKind.Utc);
			if (end)
			{
				dateTime += CdpaSetContextProvider.ResolutionAsTimeSpan;
			}
			return new DateTime?(dateTime);
		}

		// Token: 0x060062C6 RID: 25286 RVA: 0x001532AA File Offset: 0x001514AA
		private static short GetPart(Value part, short defaultValue, ref bool haveFinerPart)
		{
			if (haveFinerPart && part == null)
			{
				throw new NotSupportedException();
			}
			if (part != null)
			{
				haveFinerPart = true;
				return (short)part.AsNumber.AsInteger64;
			}
			return defaultValue;
		}

		// Token: 0x060062C7 RID: 25287 RVA: 0x001532D0 File Offset: 0x001514D0
		private static TimeParts<Value> Combine(Func<Value, Value, Value> combiner, TimeParts<Value> thisParts, TimeParts<Value> otherParts)
		{
			if (thisParts == null)
			{
				return otherParts;
			}
			if (otherParts == null)
			{
				return thisParts;
			}
			return new TimeParts<Value>
			{
				Years = combiner(thisParts.Years, otherParts.Years),
				Months = combiner(thisParts.Months, otherParts.Months),
				Days = combiner(thisParts.Days, otherParts.Days),
				Hours = combiner(thisParts.Hours, otherParts.Hours),
				Minutes = combiner(thisParts.Minutes, otherParts.Minutes),
				Seconds = combiner(thisParts.Seconds, otherParts.Seconds)
			};
		}

		// Token: 0x060062C8 RID: 25288 RVA: 0x0015337C File Offset: 0x0015157C
		private static Value Max(Value v1, Value v2)
		{
			if (v1 == null)
			{
				return v2;
			}
			if (v2 == null)
			{
				return v1;
			}
			if (v2.GreaterThan(v1))
			{
				return v2;
			}
			return v1;
		}

		// Token: 0x060062C9 RID: 25289 RVA: 0x00153394 File Offset: 0x00151594
		private static Value Min(Value v1, Value v2)
		{
			if (v1 == null)
			{
				return v2;
			}
			if (v2 == null)
			{
				return v1;
			}
			if (v2.LessThan(v1))
			{
				return v2;
			}
			return v1;
		}
	}
}
