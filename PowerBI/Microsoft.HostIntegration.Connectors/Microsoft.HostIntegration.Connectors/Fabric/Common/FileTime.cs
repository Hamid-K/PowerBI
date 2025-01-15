using System;
using System.Globalization;

namespace Microsoft.Fabric.Common
{
	// Token: 0x020003DF RID: 991
	[Serializable]
	internal struct FileTime : IComparable, IComparable<FileTime>, IEquatable<FileTime>
	{
		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x060022A0 RID: 8864 RVA: 0x0006AE64 File Offset: 0x00069064
		public static FileTime Now
		{
			get
			{
				ulong num;
				NativeMethods.GetSystemTimeAsFileTime(out num);
				return new FileTime(num);
			}
		}

		// Token: 0x060022A1 RID: 8865 RVA: 0x0006AE7E File Offset: 0x0006907E
		public static long FromMilliseconds(long milliseconds)
		{
			return milliseconds * 10000L;
		}

		// Token: 0x060022A2 RID: 8866 RVA: 0x0006AE88 File Offset: 0x00069088
		public static long ToMilliseconds(long fileTimeTicks)
		{
			return fileTimeTicks / 10000L;
		}

		// Token: 0x060022A3 RID: 8867 RVA: 0x0006AE94 File Offset: 0x00069094
		public static FileTime FromTimeSpan(TimeSpan timespan)
		{
			if (!(timespan == TimeSpan.MaxValue))
			{
				return FileTime.Now.Add(timespan);
			}
			return FileTime.MaxValue;
		}

		// Token: 0x060022A4 RID: 8868 RVA: 0x0006AEC2 File Offset: 0x000690C2
		public FileTime(long fileTimeTicks)
		{
			this = new FileTime((ulong)fileTimeTicks);
		}

		// Token: 0x060022A5 RID: 8869 RVA: 0x0006AECB File Offset: 0x000690CB
		public FileTime(ulong fileTimeTicks)
		{
			this.m_fileTimeTicks = fileTimeTicks;
		}

		// Token: 0x060022A6 RID: 8870 RVA: 0x0006AED4 File Offset: 0x000690D4
		public FileTime(DateTime dateTime)
		{
			this = new FileTime(dateTime.ToFileTime());
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x060022A7 RID: 8871 RVA: 0x0006AEE3 File Offset: 0x000690E3
		public long Time
		{
			get
			{
				return (long)this.m_fileTimeTicks;
			}
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x060022A8 RID: 8872 RVA: 0x0006AEE3 File Offset: 0x000690E3
		public ulong Ticks
		{
			get
			{
				return this.m_fileTimeTicks;
			}
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x060022A9 RID: 8873 RVA: 0x0006AEEB File Offset: 0x000690EB
		public TimeSpan RemainingDuration
		{
			get
			{
				return this.Subtract(FileTime.Now);
			}
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x060022AA RID: 8874 RVA: 0x0006AEF8 File Offset: 0x000690F8
		public TimeSpan SafeRemainingDuration
		{
			get
			{
				TimeSpan remainingDuration = this.RemainingDuration;
				if (remainingDuration < TimeSpan.Zero)
				{
					return TimeSpan.Zero;
				}
				return remainingDuration;
			}
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x060022AB RID: 8875 RVA: 0x0006AF20 File Offset: 0x00069120
		public TimeSpan DurationSince
		{
			get
			{
				return FileTime.Now.Subtract(this);
			}
		}

		// Token: 0x060022AC RID: 8876 RVA: 0x0006AF40 File Offset: 0x00069140
		public FileTime Add(TimeSpan duration)
		{
			return new FileTime(this.Time + duration.Ticks);
		}

		// Token: 0x060022AD RID: 8877 RVA: 0x0006AF55 File Offset: 0x00069155
		public FileTime Subtract(TimeSpan duration)
		{
			return new FileTime(this.Time - duration.Ticks);
		}

		// Token: 0x060022AE RID: 8878 RVA: 0x0006AF6A File Offset: 0x0006916A
		public TimeSpan Subtract(FileTime fileTime)
		{
			if (this == FileTime.MaxValue)
			{
				return TimeSpan.MaxValue;
			}
			return new TimeSpan(this.ToFileTimeTicks(fileTime));
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x060022AF RID: 8879 RVA: 0x0006AF90 File Offset: 0x00069190
		public long RemainingMilliseconds
		{
			get
			{
				return this.ToMilliseconds(FileTime.Now);
			}
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x060022B0 RID: 8880 RVA: 0x0006AFA0 File Offset: 0x000691A0
		public long MillisecondsSince
		{
			get
			{
				return FileTime.Now.ToMilliseconds(this);
			}
		}

		// Token: 0x060022B1 RID: 8881 RVA: 0x0006AFC0 File Offset: 0x000691C0
		public long ToMilliseconds(FileTime fileTime)
		{
			return FileTime.ToMilliseconds(this.ToFileTimeTicks(fileTime));
		}

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x060022B2 RID: 8882 RVA: 0x0006AFCE File Offset: 0x000691CE
		public long RemainingFileTimeTicks
		{
			get
			{
				return this.ToFileTimeTicks(FileTime.Now);
			}
		}

		// Token: 0x060022B3 RID: 8883 RVA: 0x0006AFDC File Offset: 0x000691DC
		private long ToFileTimeTicks(FileTime fileTime)
		{
			long num;
			if (this == FileTime.MaxValue)
			{
				num = FileTime.InfiniteTicks;
			}
			else
			{
				num = (long)(this.m_fileTimeTicks - fileTime.m_fileTimeTicks);
			}
			return num;
		}

		// Token: 0x060022B4 RID: 8884 RVA: 0x0006B013 File Offset: 0x00069213
		public static explicit operator DateTime(FileTime value)
		{
			if (value == FileTime.MaxValue)
			{
				return DateTime.MaxValue;
			}
			return DateTime.FromFileTime(value.Time);
		}

		// Token: 0x060022B5 RID: 8885 RVA: 0x0006B034 File Offset: 0x00069234
		public static bool operator ==(FileTime value1, FileTime value2)
		{
			return value1.m_fileTimeTicks == value2.m_fileTimeTicks;
		}

		// Token: 0x060022B6 RID: 8886 RVA: 0x0006B046 File Offset: 0x00069246
		public static bool operator !=(FileTime value1, FileTime value2)
		{
			return !(value1 == value2);
		}

		// Token: 0x060022B7 RID: 8887 RVA: 0x0006B052 File Offset: 0x00069252
		public override int GetHashCode()
		{
			return this.m_fileTimeTicks.GetHashCode();
		}

		// Token: 0x060022B8 RID: 8888 RVA: 0x0006B05F File Offset: 0x0006925F
		public override bool Equals(object obj)
		{
			return obj is FileTime && this == (FileTime)obj;
		}

		// Token: 0x060022B9 RID: 8889 RVA: 0x0006B07C File Offset: 0x0006927C
		public bool Equals(FileTime other)
		{
			return this == other;
		}

		// Token: 0x060022BA RID: 8890 RVA: 0x0006B08A File Offset: 0x0006928A
		public static FileTime operator +(FileTime value1, FileTime value2)
		{
			return FileTime.Add(value1, value2);
		}

		// Token: 0x060022BB RID: 8891 RVA: 0x0006B093 File Offset: 0x00069293
		public static FileTime Add(FileTime value1, FileTime value2)
		{
			return new FileTime(value1.m_fileTimeTicks + value2.m_fileTimeTicks);
		}

		// Token: 0x060022BC RID: 8892 RVA: 0x0006B0A9 File Offset: 0x000692A9
		public static FileTime operator +(FileTime time, long value)
		{
			return FileTime.Add(time, value);
		}

		// Token: 0x060022BD RID: 8893 RVA: 0x0006B0B2 File Offset: 0x000692B2
		public static FileTime operator +(long v, FileTime f)
		{
			return FileTime.Add(f, v);
		}

		// Token: 0x060022BE RID: 8894 RVA: 0x0006B0BB File Offset: 0x000692BB
		public static FileTime Add(FileTime f, long v)
		{
			return new FileTime(f.m_fileTimeTicks + (ulong)v);
		}

		// Token: 0x060022BF RID: 8895 RVA: 0x0006B0CB File Offset: 0x000692CB
		public static FileTime operator -(FileTime value1, FileTime value2)
		{
			return FileTime.Subtract(value1, value2);
		}

		// Token: 0x060022C0 RID: 8896 RVA: 0x0006B0D4 File Offset: 0x000692D4
		public static FileTime Subtract(FileTime value1, FileTime value2)
		{
			return new FileTime(value1.m_fileTimeTicks - value2.m_fileTimeTicks);
		}

		// Token: 0x060022C1 RID: 8897 RVA: 0x0006B0EA File Offset: 0x000692EA
		public static FileTime operator -(FileTime time, long value)
		{
			return FileTime.Subtract(time, value);
		}

		// Token: 0x060022C2 RID: 8898 RVA: 0x0006B0F3 File Offset: 0x000692F3
		public static FileTime Subtract(FileTime time, long value)
		{
			return new FileTime(time.m_fileTimeTicks - (ulong)value);
		}

		// Token: 0x060022C3 RID: 8899 RVA: 0x0006B103 File Offset: 0x00069303
		public static FileTime operator -(long value, FileTime time)
		{
			return FileTime.Subtract(value, time);
		}

		// Token: 0x060022C4 RID: 8900 RVA: 0x0006B10C File Offset: 0x0006930C
		public static FileTime Subtract(long value, FileTime time)
		{
			return new FileTime((ulong)(value - (long)time.m_fileTimeTicks));
		}

		// Token: 0x060022C5 RID: 8901 RVA: 0x0006B11C File Offset: 0x0006931C
		public static bool operator >(FileTime value1, FileTime value2)
		{
			return value1.m_fileTimeTicks > value2.m_fileTimeTicks;
		}

		// Token: 0x060022C6 RID: 8902 RVA: 0x0006B12E File Offset: 0x0006932E
		public static bool operator >=(FileTime value1, FileTime value2)
		{
			return value1.m_fileTimeTicks >= value2.m_fileTimeTicks;
		}

		// Token: 0x060022C7 RID: 8903 RVA: 0x0006B143 File Offset: 0x00069343
		public static bool operator <(FileTime value1, FileTime value2)
		{
			return value1.m_fileTimeTicks < value2.m_fileTimeTicks;
		}

		// Token: 0x060022C8 RID: 8904 RVA: 0x0006B155 File Offset: 0x00069355
		public static bool operator <=(FileTime value1, FileTime value2)
		{
			return value1.m_fileTimeTicks <= value2.m_fileTimeTicks;
		}

		// Token: 0x060022C9 RID: 8905 RVA: 0x0006B16A File Offset: 0x0006936A
		public static FileTime operator /(FileTime time, ulong value)
		{
			return new FileTime(time.m_fileTimeTicks / value);
		}

		// Token: 0x060022CA RID: 8906 RVA: 0x0006B17A File Offset: 0x0006937A
		public static FileTime operator *(FileTime time, ulong value)
		{
			return new FileTime(time.m_fileTimeTicks * value);
		}

		// Token: 0x060022CB RID: 8907 RVA: 0x0006B18A File Offset: 0x0006938A
		public static FileTime operator <<(FileTime time, int value)
		{
			return new FileTime(time.m_fileTimeTicks << value);
		}

		// Token: 0x060022CC RID: 8908 RVA: 0x0006B19D File Offset: 0x0006939D
		public static FileTime operator >>(FileTime time, int value)
		{
			return new FileTime(time.m_fileTimeTicks >> value);
		}

		// Token: 0x060022CD RID: 8909 RVA: 0x0006B1B0 File Offset: 0x000693B0
		public override string ToString()
		{
			return DateTime.FromFileTime(this.Time).ToString("yyyy-M-d HH:mm:ss.fff", CultureInfo.InvariantCulture);
		}

		// Token: 0x060022CE RID: 8910 RVA: 0x0006B1DA File Offset: 0x000693DA
		public int CompareTo(FileTime other)
		{
			if (this == other)
			{
				return 0;
			}
			if (this > other)
			{
				return 1;
			}
			return -1;
		}

		// Token: 0x060022CF RID: 8911 RVA: 0x0006B1FD File Offset: 0x000693FD
		int IComparable.CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			if (obj is FileTime)
			{
				return this.CompareTo((FileTime)obj);
			}
			throw new ArgumentException("Arg must be type FileTime");
		}

		// Token: 0x040015C2 RID: 5570
		public const long TicksPerMillisecond = 10000L;

		// Token: 0x040015C3 RID: 5571
		private const ulong MSB = 9223372036854775808UL;

		// Token: 0x040015C4 RID: 5572
		private const int DaysPerYear = 365;

		// Token: 0x040015C5 RID: 5573
		private const int DaysPer4Years = 1461;

		// Token: 0x040015C6 RID: 5574
		private const int DaysPer100Years = 36524;

		// Token: 0x040015C7 RID: 5575
		private const int DaysPer400Years = 146097;

		// Token: 0x040015C8 RID: 5576
		private const ulong DateTimeOffset = 504911232000000000UL;

		// Token: 0x040015C9 RID: 5577
		private const long MilliSecondsIn400Years = 12622780800000L;

		// Token: 0x040015CA RID: 5578
		private static readonly long InfiniteTicks = DateTime.MaxValue.ToFileTime();

		// Token: 0x040015CB RID: 5579
		private static readonly FileTime s_FileTimeAfter400Years = FileTime.Now + FileTime.FromMilliseconds(12622780800000L);

		// Token: 0x040015CC RID: 5580
		public static readonly FileTime Zero = new FileTime(0L);

		// Token: 0x040015CD RID: 5581
		public static readonly FileTime MaxValue = new FileTime(FileTime.InfiniteTicks);

		// Token: 0x040015CE RID: 5582
		private ulong m_fileTimeTicks;
	}
}
