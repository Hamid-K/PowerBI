using System;
using System.Globalization;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x0200000F RID: 15
	public struct TimeOfDay : IComparable, IComparable<TimeOfDay>, IEquatable<TimeOfDay>
	{
		// Token: 0x06000034 RID: 52 RVA: 0x0000259C File Offset: 0x0000079C
		public TimeOfDay(int hour, int minute, int second, int millisecond)
		{
			this = default(TimeOfDay);
			if (hour < 0 || hour > 23 || minute < 0 || minute > 59 || second < 0 || second > 59 || millisecond < 0 || millisecond > 999)
			{
				throw new FormatException(Strings.TimeOfDay_InvalidTimeOfDayParameters(hour, minute, second, millisecond));
			}
			this.timeSpan = new TimeSpan(0, hour, minute, second, millisecond);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000260E File Offset: 0x0000080E
		public TimeOfDay(long ticks)
		{
			this = default(TimeOfDay);
			if (ticks < 0L || ticks > 863999999999L)
			{
				throw new FormatException(Strings.TimeOfDay_TicksOutOfRange(ticks));
			}
			this.timeSpan = new TimeSpan(ticks);
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002648 File Offset: 0x00000848
		public static TimeOfDay Now
		{
			get
			{
				return DateTime.Now.TimeOfDay;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002667 File Offset: 0x00000867
		public int Hours
		{
			get
			{
				return this.timeSpan.Hours;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002674 File Offset: 0x00000874
		public int Minutes
		{
			get
			{
				return this.timeSpan.Minutes;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002681 File Offset: 0x00000881
		public int Seconds
		{
			get
			{
				return this.timeSpan.Seconds;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600003A RID: 58 RVA: 0x0000268E File Offset: 0x0000088E
		public long Milliseconds
		{
			get
			{
				return (long)this.timeSpan.Milliseconds;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600003B RID: 59 RVA: 0x0000269C File Offset: 0x0000089C
		public long Ticks
		{
			get
			{
				return this.timeSpan.Ticks;
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000026A9 File Offset: 0x000008A9
		public static bool operator !=(TimeOfDay firstOperand, TimeOfDay secondOperand)
		{
			return firstOperand.timeSpan != secondOperand.timeSpan;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000026BE File Offset: 0x000008BE
		public static bool operator ==(TimeOfDay firstOperand, TimeOfDay secondOperand)
		{
			return firstOperand.timeSpan == secondOperand.timeSpan;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000026D3 File Offset: 0x000008D3
		public static bool operator >=(TimeOfDay firstOperand, TimeOfDay secondOperand)
		{
			return firstOperand.timeSpan >= secondOperand.timeSpan;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000026E8 File Offset: 0x000008E8
		public static bool operator >(TimeOfDay firstOperand, TimeOfDay secondOperand)
		{
			return firstOperand.timeSpan > secondOperand.timeSpan;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000026FD File Offset: 0x000008FD
		public static bool operator <=(TimeOfDay firstOperand, TimeOfDay secondOperand)
		{
			return firstOperand.timeSpan <= secondOperand.timeSpan;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002712 File Offset: 0x00000912
		public static bool operator <(TimeOfDay firstOperand, TimeOfDay secondOperand)
		{
			return firstOperand.timeSpan < secondOperand.timeSpan;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002727 File Offset: 0x00000927
		public static implicit operator TimeSpan(TimeOfDay time)
		{
			return time.timeSpan;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002730 File Offset: 0x00000930
		public static implicit operator TimeOfDay(TimeSpan timeSpan)
		{
			if (timeSpan.Days != 0 || timeSpan.Hours < 0 || timeSpan.Minutes < 0 || timeSpan.Milliseconds < 0 || timeSpan.Ticks < 0L || timeSpan.Ticks > 863999999999L)
			{
				throw new FormatException(Strings.TimeOfDay_ConvertErrorFromTimeSpan(timeSpan));
			}
			return new TimeOfDay(timeSpan.Ticks);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x0000279E File Offset: 0x0000099E
		public bool Equals(TimeOfDay other)
		{
			return this.timeSpan.Equals(other.timeSpan);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000027B2 File Offset: 0x000009B2
		public override bool Equals(object obj)
		{
			return obj != null && obj is TimeOfDay && this.timeSpan.Equals(((TimeOfDay)obj).timeSpan);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000027D7 File Offset: 0x000009D7
		public override int GetHashCode()
		{
			return this.timeSpan.GetHashCode();
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000027EA File Offset: 0x000009EA
		public override string ToString()
		{
			return this.timeSpan.ToString();
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002800 File Offset: 0x00000A00
		public int CompareTo(object obj)
		{
			if (obj is TimeOfDay)
			{
				TimeOfDay timeOfDay = (TimeOfDay)obj;
				return this.CompareTo(timeOfDay);
			}
			throw new ArgumentException(Strings.TimeOfDay_InvalidCompareToTarget(obj));
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000282F File Offset: 0x00000A2F
		public int CompareTo(TimeOfDay other)
		{
			return this.timeSpan.CompareTo(other.timeSpan);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002843 File Offset: 0x00000A43
		public static TimeOfDay Parse(string text)
		{
			return TimeOfDay.Parse(text, CultureInfo.CurrentCulture);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002850 File Offset: 0x00000A50
		public static TimeOfDay Parse(string text, IFormatProvider provider)
		{
			TimeOfDay timeOfDay;
			if (TimeOfDay.TryParse(text, provider, out timeOfDay))
			{
				return timeOfDay;
			}
			throw new FormatException(Strings.TimeOfDay_InvalidParsingString(text));
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002875 File Offset: 0x00000A75
		public static bool TryParse(string text, out TimeOfDay result)
		{
			return TimeOfDay.TryParse(text, CultureInfo.CurrentCulture, out result);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002884 File Offset: 0x00000A84
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

		// Token: 0x04000015 RID: 21
		public const long MaxTickValue = 863999999999L;

		// Token: 0x04000016 RID: 22
		public const long MinTickValue = 0L;

		// Token: 0x04000017 RID: 23
		public const long TicksPerHour = 36000000000L;

		// Token: 0x04000018 RID: 24
		public const long TicksPerMinute = 600000000L;

		// Token: 0x04000019 RID: 25
		public const long TicksPerSecond = 10000000L;

		// Token: 0x0400001A RID: 26
		public static readonly TimeOfDay MinValue = new TimeOfDay(0L);

		// Token: 0x0400001B RID: 27
		public static readonly TimeOfDay MaxValue = new TimeOfDay(863999999999L);

		// Token: 0x0400001C RID: 28
		private TimeSpan timeSpan;
	}
}
