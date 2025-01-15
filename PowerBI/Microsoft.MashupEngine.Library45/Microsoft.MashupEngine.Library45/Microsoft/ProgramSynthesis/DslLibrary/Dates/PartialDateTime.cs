using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.DslLibrary.Dates
{
	// Token: 0x02000874 RID: 2164
	[Parseable("TryParseXML", ParseHumanReadableString = "TryParseHumanReadable")]
	public class PartialDateTime : IEquatable<PartialDateTime>, IRenderableLiteral
	{
		// Token: 0x06002F30 RID: 12080 RVA: 0x0008A0F0 File Offset: 0x000882F0
		private PartialDateTime(PartialDateTimeData data, bool hourValueWas24 = false)
		{
			this._data = data;
			this._hourValueWas24 = hourValueWas24;
		}

		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x06002F31 RID: 12081 RVA: 0x0008A106 File Offset: 0x00088306
		public static PartialDateTime Empty { get; } = new PartialDateTime(PartialDateTimeData.Empty, false);

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x06002F32 RID: 12082 RVA: 0x0008A10D File Offset: 0x0008830D
		public DateTimePartSet Parts
		{
			get
			{
				return this._data.SetParts;
			}
		}

		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x06002F33 RID: 12083 RVA: 0x0008A11A File Offset: 0x0008831A
		public Optional<int> Year
		{
			get
			{
				return from x in this.Get(DateTimePart.Year)
					select (x);
			}
		}

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x06002F34 RID: 12084 RVA: 0x0008A147 File Offset: 0x00088347
		public Optional<int> Month
		{
			get
			{
				return from x in this.Get(DateTimePart.Month)
					select (x);
			}
		}

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x06002F35 RID: 12085 RVA: 0x0008A174 File Offset: 0x00088374
		public Optional<int> Day
		{
			get
			{
				return from x in this.Get(DateTimePart.Day)
					select (x);
			}
		}

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x06002F36 RID: 12086 RVA: 0x0008A1A1 File Offset: 0x000883A1
		public Optional<DayOfWeek> DayOfWeek
		{
			get
			{
				return from x in this.Get(DateTimePart.DayOfWeek)
					select (DayOfWeek)x;
			}
		}

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x06002F37 RID: 12087 RVA: 0x0008A1CF File Offset: 0x000883CF
		public Optional<int> DayOfYear
		{
			get
			{
				return from x in this.Get(DateTimePart.DayOfYear)
					select (x);
			}
		}

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x06002F38 RID: 12088 RVA: 0x0008A1FD File Offset: 0x000883FD
		public Optional<int> DayOfWeekInMonth
		{
			get
			{
				return from x in this.Get(DateTimePart.DayOfWeekInMonth)
					select (x);
			}
		}

		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x06002F39 RID: 12089 RVA: 0x0008A22B File Offset: 0x0008842B
		public Optional<int> WeekYear
		{
			get
			{
				return this.Get(DateTimePart.WeekYear);
			}
		}

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x06002F3A RID: 12090 RVA: 0x0008A235 File Offset: 0x00088435
		public Optional<int> WeekOfYear
		{
			get
			{
				return this.Get(DateTimePart.WeekOfYear);
			}
		}

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x06002F3B RID: 12091 RVA: 0x0008A23F File Offset: 0x0008843F
		public Optional<int> Hour
		{
			get
			{
				return from x in this.Get(DateTimePart.Hour)
					select (x);
			}
		}

		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x06002F3C RID: 12092 RVA: 0x0008A26C File Offset: 0x0008846C
		public Optional<int> HourInPeriod
		{
			get
			{
				return from x in this.Get(DateTimePart.HourInPeriod)
					select (x);
			}
		}

		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x06002F3D RID: 12093 RVA: 0x0008A299 File Offset: 0x00088499
		public Optional<Period> Period
		{
			get
			{
				return from x in this.Get(DateTimePart.Period)
					select (Period)x;
			}
		}

		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x06002F3E RID: 12094 RVA: 0x0008A2C6 File Offset: 0x000884C6
		public Optional<int> Minute
		{
			get
			{
				return from x in this.Get(DateTimePart.Minute)
					select (x);
			}
		}

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x06002F3F RID: 12095 RVA: 0x0008A2F3 File Offset: 0x000884F3
		public Optional<int> Second
		{
			get
			{
				return from x in this.Get(DateTimePart.Second)
					select (x);
			}
		}

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x06002F40 RID: 12096 RVA: 0x0008A320 File Offset: 0x00088520
		public Optional<int> Millisecond
		{
			get
			{
				return from x in this.Get(DateTimePart.Millisecond)
					select (x);
			}
		}

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x06002F41 RID: 12097 RVA: 0x0008A34D File Offset: 0x0008854D
		public Optional<int> Quarter
		{
			get
			{
				return from x in this.Get(DateTimePart.Quarter)
					select (x);
			}
		}

		// Token: 0x06002F42 RID: 12098 RVA: 0x0008A37C File Offset: 0x0008857C
		public bool Equals(PartialDateTime other)
		{
			return other != null && (this == other || this._data.Equals(other._data));
		}

		// Token: 0x06002F43 RID: 12099 RVA: 0x0008A3A8 File Offset: 0x000885A8
		public bool Contains(DateTimePart part)
		{
			return this._data.Contains(part);
		}

		// Token: 0x06002F44 RID: 12100 RVA: 0x0008A3C4 File Offset: 0x000885C4
		public Optional<int> Get(DateTimePart part)
		{
			return this._data.Get(part).SomeIfNotNull<int>();
		}

		// Token: 0x06002F45 RID: 12101 RVA: 0x0008A3E8 File Offset: 0x000885E8
		private Optional<string> GetAsString(DateTimePart part)
		{
			if (part == DateTimePart.Period)
			{
				return this.Period.Select((Period v) => v.ToString());
			}
			if (part == DateTimePart.DayOfWeek)
			{
				return this.DayOfWeek.Select((DayOfWeek v) => v.ToString());
			}
			int? num;
			return ((this._data.Get(part) != null) ? num.GetValueOrDefault().ToString(CultureInfo.InvariantCulture) : null).SomeIfNotNull<string>();
		}

		// Token: 0x06002F46 RID: 12102 RVA: 0x0008A48C File Offset: 0x0008868C
		public static PartialDateTime Create(DateTime dt)
		{
			int[] array = new int[DateTimePartUtil.PartKindCount];
			for (int i = 0; i < DateTimePartUtil.PartKindCount; i++)
			{
				array[i] = dt.GetValue((DateTimePart)i);
			}
			return new PartialDateTime(new PartialDateTimeData(DateTimePartSet.All.Clear(DateTimePart.TimeZoneOffset), array), false);
		}

		// Token: 0x06002F47 RID: 12103 RVA: 0x0008A4DC File Offset: 0x000886DC
		public static PartialDateTime Create(DateTimeOffset dto)
		{
			int[] array = new int[DateTimePartUtil.PartKindCount];
			for (int i = 0; i < DateTimePartUtil.PartKindCount; i++)
			{
				array[i] = dto.GetValue((DateTimePart)i);
			}
			return new PartialDateTime(new PartialDateTimeData(DateTimePartSet.All, array), false);
		}

		// Token: 0x06002F48 RID: 12104 RVA: 0x0008A51F File Offset: 0x0008871F
		public static Optional<PartialDateTime> Create(PartialDateTimeData data)
		{
			return PartialDateTime.Create(data, false);
		}

		// Token: 0x06002F49 RID: 12105 RVA: 0x0008A528 File Offset: 0x00088728
		private static Optional<PartialDateTime> Create(PartialDateTimeData data, bool hourValueWas24 = false)
		{
			for (int i = 0; i < DateTimePartUtil.PartKindCount; i++)
			{
				DateTimePart dateTimePart = (DateTimePart)i;
				if (data.Contains(dateTimePart))
				{
					int num = data.Values[i];
					if ((num != 24 || dateTimePart != DateTimePart.Hour) && (num < DateTimePartUtil.MinValues[i] || num > DateTimePartUtil.MaxValues[i]))
					{
						return Optional<PartialDateTime>.Nothing;
					}
				}
			}
			int? num2 = data.Get(DateTimePart.Hour);
			int? num3 = data.Get(DateTimePart.HourInPeriod);
			int? num4 = data.Get(DateTimePart.Period);
			if (num2 != null)
			{
				int value = num2.Value;
				int num5;
				Period period;
				if (value < 12)
				{
					num5 = ((value == 0) ? 12 : value);
					period = Microsoft.ProgramSynthesis.DslLibrary.Dates.Period.AM;
				}
				else if (value < 24)
				{
					num5 = ((value == 12) ? 12 : (value - 12));
					period = Microsoft.ProgramSynthesis.DslLibrary.Dates.Period.PM;
				}
				else
				{
					num5 = 12;
					period = Microsoft.ProgramSynthesis.DslLibrary.Dates.Period.AM;
					hourValueWas24 = true;
				}
				if (!data.TryAdd(DateTimePart.HourInPeriod, num5) || !data.TryAdd(DateTimePart.Period, (int)period))
				{
					return Optional<PartialDateTime>.Nothing;
				}
			}
			else if (num3 != null && num4 != null)
			{
				int num6 = DateTimePartUtil.HourInPeriodToHour(num3.Value, (Period)num4.Value);
				data.TryAdd(DateTimePart.Hour, num6);
			}
			int? year = data.Get(DateTimePart.Year);
			int? month = data.Get(DateTimePart.Month);
			int? day = data.Get(DateTimePart.Day);
			int? num7 = data.Get(DateTimePart.DayOfYear);
			int? num8 = data.Get(DateTimePart.DayOfWeek);
			DayOfWeek? dayOfWeek = ((num8 != null) ? new DayOfWeek?((DayOfWeek)num8.GetValueOrDefault()) : null);
			int? num9 = data.Get(DateTimePart.DayOfWeekInMonth);
			int? num10 = data.Get(DateTimePart.WeekYear);
			int? num11 = data.Get(DateTimePart.WeekOfYear);
			num8 = num11;
			int num12 = 53;
			if (((num8.GetValueOrDefault() == num12) & (num8 != null)) && num10 != null && !DateTimePartUtil.IsLongIsoWeekYear(num10.Value))
			{
				return Optional<PartialDateTime>.Nothing;
			}
			if (year == null && month != null && day != null)
			{
				num8 = day;
				num12 = DateTimePartUtil.GetDaysInMonth(2004, month.Value);
				if ((num8.GetValueOrDefault() > num12) & (num8 != null))
				{
					return Optional<PartialDateTime>.Nothing;
				}
			}
			else if (year != null && num7 != null)
			{
				int num13;
				int num14;
				if (!DateTimePartUtil.DayOfYearToDate(year.Value, num7.Value, out num13, out num14))
				{
					return Optional<PartialDateTime>.Nothing;
				}
				if (!data.TryAdd(DateTimePart.Month, num13))
				{
					return Optional<PartialDateTime>.Nothing;
				}
				if (!data.TryAdd(DateTimePart.Day, num14))
				{
					return Optional<PartialDateTime>.Nothing;
				}
				month = new int?(num13);
				day = new int?(num14);
			}
			else if (year != null && month != null && dayOfWeek != null && num9 != null)
			{
				int num15 = DateTimePartUtil.DayOfWeekInMonthToDayOfMonth(year.Value, month.Value, dayOfWeek.Value, num9.Value);
				if (!data.TryAdd(DateTimePart.Day, num15))
				{
					return Optional<PartialDateTime>.Nothing;
				}
				day = new int?(num15);
			}
			else if (num10 != null && num11 != null && dayOfWeek != null)
			{
				int num16;
				int num17;
				int num18;
				DateTimePartUtil.WeekOfYearToDate(num10.Value, num11.Value, dayOfWeek.Value, out num16, out num17, out num18);
				if (!data.TryAdd(DateTimePart.Year, num16))
				{
					return Optional<PartialDateTime>.Nothing;
				}
				if (!data.TryAdd(DateTimePart.Month, num17))
				{
					return Optional<PartialDateTime>.Nothing;
				}
				if (!data.TryAdd(DateTimePart.Day, num18))
				{
					return Optional<PartialDateTime>.Nothing;
				}
				year = new int?(num16);
				month = new int?(num17);
				day = new int?(num18);
			}
			Func<Optional<PartialDateTime>> func = null;
			if (year != null && month != null && day != null)
			{
				num8 = day;
				num12 = DateTimePartUtil.GetDaysInMonth(year.Value, month.Value);
				if ((num8.GetValueOrDefault() > num12) & (num8 != null))
				{
					return Optional<PartialDateTime>.Nothing;
				}
				if (hourValueWas24)
				{
					func = delegate
					{
						DateTime dateTime = new DateTime(year.Value, month.Value, day.Value).AddDays(1.0);
						PartialDateTimeData partialDateTimeData2 = data.Without(DateTimePartSet.DateParts.Set(DateTimePart.Hour));
						partialDateTimeData2.TryAdd(DateTimePart.Hour, 0);
						partialDateTimeData2.TryAdd(DateTimePart.Year, dateTime.Year);
						partialDateTimeData2.TryAdd(DateTimePart.Month, dateTime.Month);
						partialDateTimeData2.TryAdd(DateTimePart.Day, dateTime.Day);
						return PartialDateTime.Create(partialDateTimeData2, false);
					};
				}
				int num19;
				int num20;
				DayOfWeek dayOfWeek2;
				DateTimePartUtil.DateToWeekOfYear(year.Value, month.Value, day.Value, out num19, out num20, out dayOfWeek2);
				if (!data.TryAdd(DateTimePart.DayOfWeek, (int)dayOfWeek2))
				{
					return Optional<PartialDateTime>.Nothing;
				}
				if (!data.TryAdd(DateTimePart.WeekYear, num19))
				{
					return Optional<PartialDateTime>.Nothing;
				}
				if (!data.TryAdd(DateTimePart.WeekOfYear, num20))
				{
					return Optional<PartialDateTime>.Nothing;
				}
				if (!data.TryAdd(DateTimePart.DayOfYear, DateTimePartUtil.DateToDayOfYear(year.Value, month.Value, day.Value)))
				{
					return Optional<PartialDateTime>.Nothing;
				}
			}
			if (month != null)
			{
				int num21 = DateTimePartUtil.MonthToQuarter(month.Value);
				if (!data.TryAdd(DateTimePart.Quarter, num21))
				{
					return Optional<PartialDateTime>.Nothing;
				}
			}
			if (day != null && !data.TryAdd(DateTimePart.DayOfWeekInMonth, DateTimePartUtil.DayOfMonthToDayOfWeekInMonth(day.Value)))
			{
				return Optional<PartialDateTime>.Nothing;
			}
			if (func != null)
			{
				return func();
			}
			if (hourValueWas24)
			{
				num8 = num2;
				num12 = 24;
				if ((num8.GetValueOrDefault() == num12) & (num8 != null))
				{
					PartialDateTimeData partialDateTimeData = data.Without(DateTimePart.Hour);
					partialDateTimeData.TryAdd(DateTimePart.Hour, 0);
					return new PartialDateTime(partialDateTimeData, true).Some<PartialDateTime>();
				}
			}
			return new PartialDateTime(data, hourValueWas24).Some<PartialDateTime>();
		}

		// Token: 0x06002F4A RID: 12106 RVA: 0x0008AB60 File Offset: 0x00088D60
		public Optional<DateTime> ToDateTime()
		{
			Optional<DateTime> optional;
			try
			{
				if (this.Second.HasValue && this.Second.Value > 59)
				{
					optional = Optional<DateTime>.Nothing;
				}
				else if (this.Year.HasValue && (this.Year.Value < 1 || this.Year.Value > 9999))
				{
					optional = Optional<DateTime>.Nothing;
				}
				else
				{
					int num = (this.Hour.HasValue ? this.Hour.Value : DateTimePartUtil.HourInPeriodToHour(this.HourInPeriod.OrElse(0), this.Period.OrElse(Microsoft.ProgramSynthesis.DslLibrary.Dates.Period.AM)));
					int num2 = 2000;
					int num3 = 1;
					int num4 = 1;
					DateTime res;
					do
					{
						res = new DateTime(this.Year.OrElse(num2), this.Month.OrElse(num3), this.Day.OrElse(num4), num, this.Minute.OrElse(0), this.Second.OrElse(0), this.Millisecond.OrElse(0));
						num2++;
						num3++;
						num4++;
					}
					while (this.DayOfWeek.HasValue && this.DayOfWeek.Value != res.DayOfWeek && (!this.Year.HasValue || !this.Month.HasValue || !this.Day.HasValue));
					if (this.DayOfWeek.HasValue && this.DayOfWeek.Value != res.DayOfWeek)
					{
						optional = Optional<DateTime>.Nothing;
					}
					else if (this.Parts.Except(new DateTimePart[] { DateTimePart.Quarter }).AsEnumerable().Any((DateTimePart part) => res.GetValue(part) != this.Get(part).Value))
					{
						optional = Optional<DateTime>.Nothing;
					}
					else
					{
						optional = res.Some<DateTime>();
					}
				}
			}
			catch (ArgumentOutOfRangeException)
			{
				optional = Optional<DateTime>.Nothing;
			}
			return optional;
		}

		// Token: 0x06002F4B RID: 12107 RVA: 0x0008ADB4 File Offset: 0x00088FB4
		public bool Explains(PartialDateTime toExplain)
		{
			return this.Parts.CanExplain(toExplain.Parts) && toExplain.Parts.AsEnumerable().All((DateTimePart partToExplain) => toExplain.Get(partToExplain) == this.Get(partToExplain));
		}

		// Token: 0x06002F4C RID: 12108 RVA: 0x0008AE14 File Offset: 0x00089014
		public Optional<PartialDateTime> CombineWith(PartialDateTime other)
		{
			return from d in this._data.CombineWith(other._data).SomeIfNotNull<PartialDateTimeData>()
				select PartialDateTime.Create(d, this._hourValueWas24 || other._hourValueWas24);
		}

		// Token: 0x06002F4D RID: 12109 RVA: 0x0008AE6C File Offset: 0x0008906C
		public Optional<PartialDateTime> With(DateTimePart part, int value)
		{
			return from d in this._data.With(part, value).SomeIfNotNull<PartialDateTimeData>()
				select PartialDateTime.Create(d, this._hourValueWas24);
		}

		// Token: 0x06002F4E RID: 12110 RVA: 0x0008AEA4 File Offset: 0x000890A4
		public PartialDateTime Without(DateTimePart part)
		{
			if (!this._data.Contains(part))
			{
				return this;
			}
			return PartialDateTime.Create(this._data.Without(part), this._hourValueWas24 && part != DateTimePart.Hour).Value;
		}

		// Token: 0x06002F4F RID: 12111 RVA: 0x0008AEF2 File Offset: 0x000890F2
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((PartialDateTime)obj)));
		}

		// Token: 0x06002F50 RID: 12112 RVA: 0x0008AF20 File Offset: 0x00089120
		public override int GetHashCode()
		{
			return this._data.GetHashCode();
		}

		// Token: 0x06002F51 RID: 12113 RVA: 0x0000BE9E File Offset: 0x0000A09E
		public static bool operator ==(PartialDateTime left, PartialDateTime right)
		{
			return object.Equals(left, right);
		}

		// Token: 0x06002F52 RID: 12114 RVA: 0x0000BEA7 File Offset: 0x0000A0A7
		public static bool operator !=(PartialDateTime left, PartialDateTime right)
		{
			return !object.Equals(left, right);
		}

		// Token: 0x06002F53 RID: 12115 RVA: 0x0008AF41 File Offset: 0x00089141
		public override string ToString()
		{
			return string.Join(", ", DateTimePartList.AllDateTime.SelectValues((DateTimePart part) => from value in this.GetAsString(part)
				select FormattableString.Invariant(FormattableStringFactory.Create("{0}: {1}", new object[] { part, value }))));
		}

		// Token: 0x06002F54 RID: 12116 RVA: 0x0008AF64 File Offset: 0x00089164
		public string RenderHumanReadable()
		{
			return "{" + string.Join(", ", from part in this.Parts.AsEnumerable()
				select part.ToString() + "=" + this.Get(part).ToString()) + "}";
		}

		// Token: 0x06002F55 RID: 12117 RVA: 0x0008AFAC File Offset: 0x000891AC
		public XElement RenderXML()
		{
			return new XElement("PartialDateTime", from part in this.Parts.AsEnumerable()
				select new XAttribute(part.ToString(), this.Get(part)));
		}

		// Token: 0x06002F56 RID: 12118 RVA: 0x0008AFE8 File Offset: 0x000891E8
		public static Optional<PartialDateTime> TryParseXML(XElement literal)
		{
			Optional<PartialDateTime> optional;
			try
			{
				if (literal.Name != "PartialDateTime")
				{
					optional = Optional<PartialDateTime>.Nothing;
				}
				else
				{
					if (literal.HasElements)
					{
						throw new NotImplementedException("Unsupported PartialDateTime XML: " + ((literal != null) ? literal.ToString() : null));
					}
					PartialDateTimeData empty = PartialDateTimeData.Empty;
					foreach (DateTimePart dateTimePart in DateTimePartList.AllDateTime)
					{
						XAttribute xattribute = literal.Attribute(dateTimePart.ToString());
						if (xattribute != null)
						{
							empty.TryAdd(dateTimePart, int.Parse(xattribute.Value, CultureInfo.InvariantCulture));
						}
					}
					optional = PartialDateTime.Create(empty).Some<Optional<PartialDateTime>>();
				}
			}
			catch
			{
				optional = Optional<PartialDateTime>.Nothing;
			}
			return optional;
		}

		// Token: 0x06002F57 RID: 12119 RVA: 0x0008B0DC File Offset: 0x000892DC
		internal static Optional<PartialDateTime> TryParseHumanReadable(string literal)
		{
			if (!literal.StartsWith("{", StringComparison.Ordinal) || !literal.EndsWith("", StringComparison.Ordinal))
			{
				return Optional<PartialDateTime>.Nothing;
			}
			literal = literal.Substring(1, literal.Length - 2);
			PartialDateTimeData empty = PartialDateTimeData.Empty;
			Dictionary<string, int> dictionary = (from kv in literal.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries)
				select kv.Split(new char[] { '=' })).ToDictionary((string[] kv) => kv[0], (string[] kv) => int.Parse(kv[1], CultureInfo.InvariantCulture));
			foreach (DateTimePart dateTimePart in DateTimePartList.AllDateTime)
			{
				int num;
				if (dictionary.TryGetValue(dateTimePart.ToString(), out num))
				{
					empty.TryAdd(dateTimePart, num);
				}
			}
			return PartialDateTime.Create(empty).Some<Optional<PartialDateTime>>();
		}

		// Token: 0x04001712 RID: 5906
		private readonly PartialDateTimeData _data;

		// Token: 0x04001713 RID: 5907
		private readonly bool _hourValueWas24;
	}
}
