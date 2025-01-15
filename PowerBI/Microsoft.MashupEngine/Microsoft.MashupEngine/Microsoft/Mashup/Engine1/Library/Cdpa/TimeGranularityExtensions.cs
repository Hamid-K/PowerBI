using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000E55 RID: 3669
	internal static class TimeGranularityExtensions
	{
		// Token: 0x060062A1 RID: 25249 RVA: 0x001526A8 File Offset: 0x001508A8
		public static bool TryGetMetricGranularity(this ITimeGranularity granularity, out MetricGranularityTimeGranularity metricGranularity)
		{
			metricGranularity = granularity as MetricGranularityTimeGranularity;
			if (metricGranularity != null)
			{
				return true;
			}
			TimePartsTimeGranularity timePartsTimeGranularity = granularity as TimePartsTimeGranularity;
			if (timePartsTimeGranularity != null)
			{
				return TimeGranularityExtensions.timeGranularityToMetricGranularityMap.TryGetValue(timePartsTimeGranularity, out metricGranularity);
			}
			metricGranularity = null;
			return false;
		}

		// Token: 0x060062A2 RID: 25250 RVA: 0x001526E0 File Offset: 0x001508E0
		public static bool TryGetTimePartsGranularity(this ITimeGranularity granularity, out TimePartsTimeGranularity timePartsGranularity)
		{
			timePartsGranularity = granularity as TimePartsTimeGranularity;
			if (timePartsGranularity != null)
			{
				return true;
			}
			MetricGranularityTimeGranularity metricGranularityTimeGranularity = granularity as MetricGranularityTimeGranularity;
			if (metricGranularityTimeGranularity != null)
			{
				return TimeGranularityExtensions.metricGranularityToTimeGranularityMap.TryGetValue(metricGranularityTimeGranularity, out timePartsGranularity);
			}
			timePartsGranularity = null;
			return false;
		}

		// Token: 0x060062A3 RID: 25251 RVA: 0x00152718 File Offset: 0x00150918
		public static TimePartsTimeGranularity ToUnits(this TimePartsTimeGranularity granularity)
		{
			return new TimePartsTimeGranularity
			{
				Years = TimeGranularityExtensions.GetUnit(granularity.Years),
				Months = TimeGranularityExtensions.GetUnit(granularity.Months),
				Days = TimeGranularityExtensions.GetUnit(granularity.Days),
				Hours = TimeGranularityExtensions.GetUnit(granularity.Hours),
				Minutes = TimeGranularityExtensions.GetUnit(granularity.Minutes),
				Seconds = TimeGranularityExtensions.GetUnit(granularity.Seconds)
			};
		}

		// Token: 0x060062A4 RID: 25252 RVA: 0x00152790 File Offset: 0x00150990
		private static short GetUnit(short part)
		{
			if (part != TimePartsTimeGranularity.CoarsestPart && part != TimePartsTimeGranularity.FinestPart)
			{
				return 1;
			}
			return part;
		}

		// Token: 0x060062A5 RID: 25253 RVA: 0x001527A8 File Offset: 0x001509A8
		public static DateTime GetNearestAnchor(this TimePartsTimeGranularity granularity, DateTime anchor, DateTime dateTime)
		{
			DateTime dateTime2 = dateTime;
			if (anchor <= dateTime)
			{
				if (granularity.AddFinestPartTo(anchor) != anchor)
				{
					dateTime2 = anchor;
					for (;;)
					{
						DateTime dateTime3 = granularity.AddFinestPartTo(dateTime2);
						if (dateTime3 > dateTime)
						{
							break;
						}
						dateTime2 = dateTime3;
					}
				}
			}
			else if (granularity.SubtractFinestPartFrom(anchor) != anchor)
			{
				dateTime2 = anchor;
				while (dateTime2 > dateTime)
				{
					dateTime2 = granularity.SubtractFinestPartFrom(dateTime2);
				}
			}
			return dateTime2;
		}

		// Token: 0x060062A6 RID: 25254 RVA: 0x0015280C File Offset: 0x00150A0C
		public static ITimeGranularity CrossJoin(this ITimeGranularity granularity1, ITimeGranularity granularity2)
		{
			if (granularity1 == null)
			{
				return granularity2;
			}
			if (granularity2 == null)
			{
				return granularity1;
			}
			AnchoredTimeGranularity anchoredTimeGranularity = granularity1 as AnchoredTimeGranularity;
			AnchoredTimeGranularity anchoredTimeGranularity2 = granularity2 as AnchoredTimeGranularity;
			if (anchoredTimeGranularity != null)
			{
				granularity1 = anchoredTimeGranularity.Granularity;
			}
			if (anchoredTimeGranularity2 != null)
			{
				granularity2 = anchoredTimeGranularity2.Granularity;
			}
			TimePartsTimeGranularity timePartsTimeGranularity;
			TimePartsTimeGranularity timePartsTimeGranularity2;
			if (granularity1.TryGetTimePartsGranularity(out timePartsTimeGranularity) && granularity2.TryGetTimePartsGranularity(out timePartsTimeGranularity2))
			{
				TimePartsTimeGranularity timePartsTimeGranularity3 = new TimePartsTimeGranularity
				{
					Years = Math.Min(timePartsTimeGranularity.Years, timePartsTimeGranularity2.Years),
					Months = Math.Min(timePartsTimeGranularity.Months, timePartsTimeGranularity2.Months),
					Days = Math.Min(timePartsTimeGranularity.Days, timePartsTimeGranularity2.Days),
					Hours = Math.Min(timePartsTimeGranularity.Hours, timePartsTimeGranularity2.Hours),
					Minutes = Math.Min(timePartsTimeGranularity.Minutes, timePartsTimeGranularity2.Minutes),
					Seconds = Math.Min(timePartsTimeGranularity.Seconds, timePartsTimeGranularity2.Seconds)
				};
				if (anchoredTimeGranularity == null && anchoredTimeGranularity2 == null)
				{
					return timePartsTimeGranularity3;
				}
				DateTime? dateTime = null;
				if (anchoredTimeGranularity == null)
				{
					dateTime = new DateTime?(anchoredTimeGranularity2.Anchor);
				}
				else if (anchoredTimeGranularity2 == null)
				{
					dateTime = new DateTime?(anchoredTimeGranularity.Anchor);
				}
				else if (timePartsTimeGranularity3.GetNearestAnchor(anchoredTimeGranularity.Anchor, anchoredTimeGranularity2.Anchor) == anchoredTimeGranularity2.Anchor && timePartsTimeGranularity3.GetNearestAnchor(anchoredTimeGranularity2.Anchor, anchoredTimeGranularity.Anchor) == anchoredTimeGranularity.Anchor)
				{
					dateTime = new DateTime?(TemporalExtensions.Min(new DateTime?(anchoredTimeGranularity.Anchor), new DateTime?(anchoredTimeGranularity2.Anchor)).Value);
				}
				if (dateTime != null)
				{
					return new AnchoredTimeGranularity
					{
						Anchor = dateTime.Value,
						Granularity = timePartsTimeGranularity3
					};
				}
			}
			throw new NotSupportedException();
		}

		// Token: 0x060062A7 RID: 25255 RVA: 0x001529BE File Offset: 0x00150BBE
		public static ITimeGranularity NullableMerge(this ITimeGranularity granularity1, ITimeGranularity granularity2)
		{
			if (granularity1 == null)
			{
				return granularity2;
			}
			if (granularity2 == null)
			{
				return granularity1;
			}
			if (granularity1.Equals(granularity2))
			{
				return granularity1;
			}
			throw new NotSupportedException();
		}

		// Token: 0x060062A8 RID: 25256 RVA: 0x001529DC File Offset: 0x00150BDC
		public static DateTime RoundTo(this DateTime dateTime, ITimeGranularity granularity, bool roundUp)
		{
			TimePartsTimeGranularity timePartsTimeGranularity;
			if (granularity.TryGetTimePartsGranularity(out timePartsTimeGranularity))
			{
				if (roundUp)
				{
					DateTime dateTime2 = timePartsTimeGranularity.AddFinestPartTo(dateTime);
					if (dateTime2 > dateTime)
					{
						dateTime = dateTime2 - TimeSpan.FromTicks(1L);
					}
				}
				dateTime = new DateTime(TimeGranularityExtensions.RoundPart(dateTime.Year, (int)timePartsTimeGranularity.Years, 1), TimeGranularityExtensions.RoundPart(dateTime.Month, (int)timePartsTimeGranularity.Months, 1), TimeGranularityExtensions.RoundPart(dateTime.Day, (int)timePartsTimeGranularity.Days, 1), TimeGranularityExtensions.RoundPart(dateTime.Hour, (int)timePartsTimeGranularity.Hours, 0), TimeGranularityExtensions.RoundPart(dateTime.Minute, (int)timePartsTimeGranularity.Minutes, 0), TimeGranularityExtensions.RoundPart(dateTime.Second, (int)timePartsTimeGranularity.Seconds, 0), DateTimeKind.Utc);
			}
			return dateTime;
		}

		// Token: 0x060062A9 RID: 25257 RVA: 0x00152A94 File Offset: 0x00150C94
		private static int RoundPart(int part, int partGranularity, int minValue)
		{
			if (partGranularity == 0)
			{
				partGranularity = 1;
			}
			return Math.Max(part / partGranularity * partGranularity, minValue);
		}

		// Token: 0x040035AE RID: 13742
		private static readonly Dictionary<TimePartsTimeGranularity, MetricGranularityTimeGranularity> timeGranularityToMetricGranularityMap = new Dictionary<TimePartsTimeGranularity, MetricGranularityTimeGranularity>
		{
			{
				TimePartsTimeGranularity.Coarsest,
				MetricGranularityTimeGranularity.All
			},
			{
				new TimePartsTimeGranularity
				{
					Years = 1
				},
				MetricGranularityTimeGranularity.P1Y
			},
			{
				new TimePartsTimeGranularity
				{
					Years = 1,
					Months = 1
				},
				MetricGranularityTimeGranularity.P1M
			},
			{
				new TimePartsTimeGranularity
				{
					Years = 1,
					Months = 1,
					Days = 1
				},
				MetricGranularityTimeGranularity.P1D
			},
			{
				new TimePartsTimeGranularity
				{
					Years = 1,
					Months = 1,
					Days = 1,
					Hours = 1
				},
				MetricGranularityTimeGranularity.PT1H
			},
			{
				new TimePartsTimeGranularity
				{
					Years = 1,
					Months = 1,
					Days = 1,
					Hours = 1,
					Minutes = 5
				},
				MetricGranularityTimeGranularity.PT5M
			},
			{
				new TimePartsTimeGranularity
				{
					Years = 1,
					Months = 1,
					Days = 1,
					Hours = 1,
					Minutes = 1
				},
				MetricGranularityTimeGranularity.PT1M
			},
			{
				new TimePartsTimeGranularity
				{
					Years = 1,
					Months = 1,
					Days = 1,
					Hours = 1,
					Minutes = 1,
					Seconds = 1
				},
				MetricGranularityTimeGranularity.PT1S
			},
			{
				new TimePartsTimeGranularity
				{
					Years = TimePartsTimeGranularity.CoarsestPart,
					Months = TimePartsTimeGranularity.CoarsestPart,
					Days = 7,
					Hours = TimePartsTimeGranularity.CoarsestPart,
					Minutes = TimePartsTimeGranularity.CoarsestPart
				},
				MetricGranularityTimeGranularity.P7D
			}
		};

		// Token: 0x040035AF RID: 13743
		private static readonly Dictionary<MetricGranularityTimeGranularity, TimePartsTimeGranularity> metricGranularityToTimeGranularityMap = new Dictionary<MetricGranularityTimeGranularity, TimePartsTimeGranularity>
		{
			{
				MetricGranularityTimeGranularity.All,
				TimePartsTimeGranularity.Coarsest
			},
			{
				MetricGranularityTimeGranularity.P1Y,
				new TimePartsTimeGranularity
				{
					Years = 1
				}
			},
			{
				MetricGranularityTimeGranularity.P1M,
				new TimePartsTimeGranularity
				{
					Years = 1,
					Months = 1
				}
			},
			{
				MetricGranularityTimeGranularity.P1D,
				new TimePartsTimeGranularity
				{
					Years = 1,
					Months = 1,
					Days = 1
				}
			},
			{
				MetricGranularityTimeGranularity.PT1H,
				new TimePartsTimeGranularity
				{
					Years = 1,
					Months = 1,
					Days = 1,
					Hours = 1
				}
			},
			{
				MetricGranularityTimeGranularity.PT5M,
				new TimePartsTimeGranularity
				{
					Years = 1,
					Months = 1,
					Days = 1,
					Hours = 1,
					Minutes = 5
				}
			},
			{
				MetricGranularityTimeGranularity.PT1M,
				new TimePartsTimeGranularity
				{
					Years = 1,
					Months = 1,
					Days = 1,
					Hours = 1,
					Minutes = 1
				}
			},
			{
				MetricGranularityTimeGranularity.PT1S,
				new TimePartsTimeGranularity
				{
					Years = 1,
					Months = 1,
					Days = 1,
					Hours = 1,
					Minutes = 1,
					Seconds = 1
				}
			},
			{
				MetricGranularityTimeGranularity.P7D,
				new TimePartsTimeGranularity
				{
					Years = TimePartsTimeGranularity.CoarsestPart,
					Months = TimePartsTimeGranularity.CoarsestPart,
					Days = 7,
					Hours = TimePartsTimeGranularity.CoarsestPart,
					Minutes = TimePartsTimeGranularity.CoarsestPart
				}
			}
		};
	}
}
