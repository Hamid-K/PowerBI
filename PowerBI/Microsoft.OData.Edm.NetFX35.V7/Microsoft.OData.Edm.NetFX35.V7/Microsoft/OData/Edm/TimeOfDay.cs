using System;
using System.Globalization;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000CA RID: 202
	public struct TimeOfDay : IComparable, IComparable<TimeOfDay>, IEquatable<TimeOfDay>
	{
		// Token: 0x060004C9 RID: 1225 RVA: 0x0000C8D4 File Offset: 0x0000AAD4
		public TimeOfDay(int hour, int minute, int second, int millisecond)
		{
			this = default(TimeOfDay);
			if (hour < 0 || hour > 23 || minute < 0 || minute > 59 || second < 0 || second > 59 || millisecond < 0 || millisecond > 999)
			{
				throw new FormatException(Strings.TimeOfDay_InvalidTimeOfDayParameters(hour, minute, second, millisecond));
			}
			this.timeSpan = new TimeSpan(0, hour, minute, second, millisecond);
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0000C946 File Offset: 0x0000AB46
		public TimeOfDay(long ticks)
		{
			this = default(TimeOfDay);
			if (ticks < 0L || ticks > 863999999999L)
			{
				throw new FormatException(Strings.TimeOfDay_TicksOutOfRange(ticks));
			}
			this.timeSpan = new TimeSpan(ticks);
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x0000C980 File Offset: 0x0000AB80
		public static TimeOfDay Now
		{
			get
			{
				return DateTime.Now.TimeOfDay;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x0000C99F File Offset: 0x0000AB9F
		public int Hours
		{
			get
			{
				return this.timeSpan.Hours;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x0000C9AC File Offset: 0x0000ABAC
		public int Minutes
		{
			get
			{
				return this.timeSpan.Minutes;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x0000C9B9 File Offset: 0x0000ABB9
		public int Seconds
		{
			get
			{
				return this.timeSpan.Seconds;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x0000C9C6 File Offset: 0x0000ABC6
		public long Milliseconds
		{
			get
			{
				return (long)this.timeSpan.Milliseconds;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x0000C9D4 File Offset: 0x0000ABD4
		public long Ticks
		{
			get
			{
				return this.timeSpan.Ticks;
			}
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x0000C9E1 File Offset: 0x0000ABE1
		public static bool operator !=(TimeOfDay firstOperand, TimeOfDay secondOperand)
		{
			return firstOperand.timeSpan != secondOperand.timeSpan;
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x0000C9F4 File Offset: 0x0000ABF4
		public static bool operator ==(TimeOfDay firstOperand, TimeOfDay secondOperand)
		{
			return firstOperand.timeSpan == secondOperand.timeSpan;
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x0000CA07 File Offset: 0x0000AC07
		public static bool operator >=(TimeOfDay firstOperand, TimeOfDay secondOperand)
		{
			return firstOperand.timeSpan >= secondOperand.timeSpan;
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x0000CA1A File Offset: 0x0000AC1A
		public static bool operator >(TimeOfDay firstOperand, TimeOfDay secondOperand)
		{
			return firstOperand.timeSpan > secondOperand.timeSpan;
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x0000CA2D File Offset: 0x0000AC2D
		public static bool operator <=(TimeOfDay firstOperand, TimeOfDay secondOperand)
		{
			return firstOperand.timeSpan <= secondOperand.timeSpan;
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0000CA40 File Offset: 0x0000AC40
		public static bool operator <(TimeOfDay firstOperand, TimeOfDay secondOperand)
		{
			return firstOperand.timeSpan < secondOperand.timeSpan;
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0000CA53 File Offset: 0x0000AC53
		public static implicit operator TimeSpan(TimeOfDay time)
		{
			return time.timeSpan;
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0000CA5C File Offset: 0x0000AC5C
		public static implicit operator TimeOfDay(TimeSpan timeSpan)
		{
			if (timeSpan.Days != 0 || timeSpan.Hours < 0 || timeSpan.Minutes < 0 || timeSpan.Milliseconds < 0 || timeSpan.Ticks < 0L || timeSpan.Ticks > 863999999999L)
			{
				throw new FormatException(Strings.TimeOfDay_ConvertErrorFromTimeSpan(timeSpan));
			}
			return new TimeOfDay(timeSpan.Ticks);
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0000CACA File Offset: 0x0000ACCA
		public bool Equals(TimeOfDay other)
		{
			return this.timeSpan.Equals(other.timeSpan);
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0000CADD File Offset: 0x0000ACDD
		public override bool Equals(object obj)
		{
			return obj != null && obj is TimeOfDay && this.timeSpan.Equals(((TimeOfDay)obj).timeSpan);
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0000CB02 File Offset: 0x0000AD02
		public override int GetHashCode()
		{
			return this.timeSpan.GetHashCode();
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0000CB15 File Offset: 0x0000AD15
		public override string ToString()
		{
			return this.timeSpan.ToString();
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0000CB28 File Offset: 0x0000AD28
		public int CompareTo(object obj)
		{
			if (obj is TimeOfDay)
			{
				TimeOfDay timeOfDay = (TimeOfDay)obj;
				return this.CompareTo(timeOfDay);
			}
			throw new ArgumentException(Strings.TimeOfDay_InvalidCompareToTarget(obj));
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0000CB57 File Offset: 0x0000AD57
		public int CompareTo(TimeOfDay other)
		{
			return this.timeSpan.CompareTo(other.timeSpan);
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0000CB6A File Offset: 0x0000AD6A
		public static TimeOfDay Parse(string text)
		{
			return TimeOfDay.Parse(text, CultureInfo.CurrentCulture);
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0000CB78 File Offset: 0x0000AD78
		public static TimeOfDay Parse(string text, IFormatProvider provider)
		{
			TimeOfDay timeOfDay;
			if (TimeOfDay.TryParse(text, provider, out timeOfDay))
			{
				return timeOfDay;
			}
			throw new FormatException(Strings.TimeOfDay_InvalidParsingString(text));
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0000CB9D File Offset: 0x0000AD9D
		public static bool TryParse(string text, out TimeOfDay result)
		{
			return TimeOfDay.TryParse(text, CultureInfo.CurrentCulture, out result);
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0000CBAC File Offset: 0x0000ADAC
		public static bool TryParse(string text, IFormatProvider provider, out TimeOfDay result)
		{
			TimeSpan timeSpan;
			bool flag = TimeSpan.TryParse(text, ref timeSpan);
			if (flag && timeSpan.Ticks >= 0L && timeSpan.Ticks <= 863999999999L)
			{
				result = new TimeOfDay(timeSpan.Ticks);
				return true;
			}
			result = TimeOfDay.MinValue;
			return false;
		}

		// Token: 0x04000185 RID: 389
		public const long MaxTickValue = 863999999999L;

		// Token: 0x04000186 RID: 390
		public const long MinTickValue = 0L;

		// Token: 0x04000187 RID: 391
		public const long TicksPerHour = 36000000000L;

		// Token: 0x04000188 RID: 392
		public const long TicksPerMinute = 600000000L;

		// Token: 0x04000189 RID: 393
		public const long TicksPerSecond = 10000000L;

		// Token: 0x0400018A RID: 394
		public static readonly TimeOfDay MinValue = new TimeOfDay(0L);

		// Token: 0x0400018B RID: 395
		public static readonly TimeOfDay MaxValue = new TimeOfDay(863999999999L);

		// Token: 0x0400018C RID: 396
		private TimeSpan timeSpan;
	}
}
