using System;
using System.Globalization;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000030 RID: 48
	public struct TimeOfDay : IComparable, IComparable<TimeOfDay>, IEquatable<TimeOfDay>
	{
		// Token: 0x060000C1 RID: 193 RVA: 0x000031FC File Offset: 0x000013FC
		public TimeOfDay(int hour, int minute, int second, int millisecond)
		{
			this = default(TimeOfDay);
			if (hour < 0 || hour > 23 || minute < 0 || minute > 59 || second < 0 || second > 59 || millisecond < 0 || millisecond > 999)
			{
				throw new FormatException(Strings.TimeOfDay_InvalidTimeOfDayParameters(hour, minute, second, millisecond));
			}
			this.timeSpan = new TimeSpan(0, hour, minute, second, millisecond);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0000326E File Offset: 0x0000146E
		public TimeOfDay(long ticks)
		{
			this = default(TimeOfDay);
			if (ticks < 0L || ticks > 863999999999L)
			{
				throw new FormatException(Strings.TimeOfDay_TicksOutOfRange(ticks));
			}
			this.timeSpan = new TimeSpan(ticks);
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x000032A8 File Offset: 0x000014A8
		public static TimeOfDay Now
		{
			get
			{
				return DateTime.Now.TimeOfDay;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x000032C7 File Offset: 0x000014C7
		public int Hours
		{
			get
			{
				return this.timeSpan.Hours;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x000032D4 File Offset: 0x000014D4
		public int Minutes
		{
			get
			{
				return this.timeSpan.Minutes;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x000032E1 File Offset: 0x000014E1
		public int Seconds
		{
			get
			{
				return this.timeSpan.Seconds;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x000032EE File Offset: 0x000014EE
		public long Milliseconds
		{
			get
			{
				return (long)this.timeSpan.Milliseconds;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x000032FC File Offset: 0x000014FC
		public long Ticks
		{
			get
			{
				return this.timeSpan.Ticks;
			}
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00003309 File Offset: 0x00001509
		public static bool operator !=(TimeOfDay firstOperand, TimeOfDay secondOperand)
		{
			return firstOperand.timeSpan != secondOperand.timeSpan;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0000331C File Offset: 0x0000151C
		public static bool operator ==(TimeOfDay firstOperand, TimeOfDay secondOperand)
		{
			return firstOperand.timeSpan == secondOperand.timeSpan;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000332F File Offset: 0x0000152F
		public static bool operator >=(TimeOfDay firstOperand, TimeOfDay secondOperand)
		{
			return firstOperand.timeSpan >= secondOperand.timeSpan;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00003342 File Offset: 0x00001542
		public static bool operator >(TimeOfDay firstOperand, TimeOfDay secondOperand)
		{
			return firstOperand.timeSpan > secondOperand.timeSpan;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00003355 File Offset: 0x00001555
		public static bool operator <=(TimeOfDay firstOperand, TimeOfDay secondOperand)
		{
			return firstOperand.timeSpan <= secondOperand.timeSpan;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00003368 File Offset: 0x00001568
		public static bool operator <(TimeOfDay firstOperand, TimeOfDay secondOperand)
		{
			return firstOperand.timeSpan < secondOperand.timeSpan;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0000337B File Offset: 0x0000157B
		public static implicit operator TimeSpan(TimeOfDay time)
		{
			return time.timeSpan;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00003384 File Offset: 0x00001584
		public static implicit operator TimeOfDay(TimeSpan timeSpan)
		{
			if (timeSpan.Days != 0 || timeSpan.Hours < 0 || timeSpan.Minutes < 0 || timeSpan.Milliseconds < 0 || timeSpan.Ticks < 0L || timeSpan.Ticks > 863999999999L)
			{
				throw new FormatException(Strings.TimeOfDay_ConvertErrorFromTimeSpan(timeSpan));
			}
			return new TimeOfDay(timeSpan.Ticks);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000033F2 File Offset: 0x000015F2
		public bool Equals(TimeOfDay other)
		{
			return this.timeSpan.Equals(other.timeSpan);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00003405 File Offset: 0x00001605
		public override bool Equals(object obj)
		{
			return obj != null && obj is TimeOfDay && this.timeSpan.Equals(((TimeOfDay)obj).timeSpan);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000342A File Offset: 0x0000162A
		public override int GetHashCode()
		{
			return this.timeSpan.GetHashCode();
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000343D File Offset: 0x0000163D
		public override string ToString()
		{
			return this.timeSpan.ToString("hh\\:mm\\:ss\\.fffffff", CultureInfo.InvariantCulture);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00003454 File Offset: 0x00001654
		public int CompareTo(object obj)
		{
			if (obj is TimeOfDay)
			{
				TimeOfDay timeOfDay = (TimeOfDay)obj;
				return this.CompareTo(timeOfDay);
			}
			throw new ArgumentException(Strings.TimeOfDay_InvalidCompareToTarget(obj));
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00003483 File Offset: 0x00001683
		public int CompareTo(TimeOfDay other)
		{
			return this.timeSpan.CompareTo(other.timeSpan);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00003496 File Offset: 0x00001696
		public static TimeOfDay Parse(string text)
		{
			return TimeOfDay.Parse(text, CultureInfo.CurrentCulture);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000034A4 File Offset: 0x000016A4
		public static TimeOfDay Parse(string text, IFormatProvider provider)
		{
			TimeOfDay timeOfDay;
			if (TimeOfDay.TryParse(text, provider, out timeOfDay))
			{
				return timeOfDay;
			}
			throw new FormatException(Strings.TimeOfDay_InvalidParsingString(text));
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000034C9 File Offset: 0x000016C9
		public static bool TryParse(string text, out TimeOfDay result)
		{
			return TimeOfDay.TryParse(text, CultureInfo.CurrentCulture, out result);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000034D8 File Offset: 0x000016D8
		public static bool TryParse(string text, IFormatProvider provider, out TimeOfDay result)
		{
			TimeSpan timeSpan;
			bool flag = TimeSpan.TryParse(text, provider, out timeSpan);
			if (flag && timeSpan.Ticks >= 0L && timeSpan.Ticks <= 863999999999L)
			{
				result = new TimeOfDay(timeSpan.Ticks);
				return true;
			}
			result = TimeOfDay.MinValue;
			return false;
		}

		// Token: 0x04000047 RID: 71
		public const long MaxTickValue = 863999999999L;

		// Token: 0x04000048 RID: 72
		public const long MinTickValue = 0L;

		// Token: 0x04000049 RID: 73
		public const long TicksPerHour = 36000000000L;

		// Token: 0x0400004A RID: 74
		public const long TicksPerMinute = 600000000L;

		// Token: 0x0400004B RID: 75
		public const long TicksPerSecond = 10000000L;

		// Token: 0x0400004C RID: 76
		public static readonly TimeOfDay MinValue = new TimeOfDay(0L);

		// Token: 0x0400004D RID: 77
		public static readonly TimeOfDay MaxValue = new TimeOfDay(863999999999L);

		// Token: 0x0400004E RID: 78
		private TimeSpan timeSpan;
	}
}
