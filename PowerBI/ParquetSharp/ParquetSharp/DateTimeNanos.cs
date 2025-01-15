using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x02000032 RID: 50
	public readonly struct DateTimeNanos : IEquatable<DateTimeNanos>, IComparable, IComparable<DateTimeNanos>
	{
		// Token: 0x0600013A RID: 314 RVA: 0x000052A4 File Offset: 0x000034A4
		public DateTimeNanos(long ticks)
		{
			this.Ticks = ticks;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x000052B0 File Offset: 0x000034B0
		public DateTimeNanos(DateTime dateTime)
		{
			this.Ticks = (dateTime.Ticks - 621355968000000000L) * 100L;
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600013C RID: 316 RVA: 0x000052D0 File Offset: 0x000034D0
		public DateTime DateTime
		{
			get
			{
				return new DateTime(621355968000000000L + this.Ticks / 100L);
			}
		}

		// Token: 0x0600013D RID: 317 RVA: 0x000052EC File Offset: 0x000034EC
		public bool Equals(DateTimeNanos other)
		{
			return this.Ticks == other.Ticks;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x000052FC File Offset: 0x000034FC
		[NullableContext(1)]
		public override bool Equals(object obj)
		{
			if (obj is DateTimeNanos)
			{
				DateTimeNanos dateTimeNanos = (DateTimeNanos)obj;
				return this.Equals(dateTimeNanos);
			}
			return false;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00005328 File Offset: 0x00003528
		public override int GetHashCode()
		{
			return this.Ticks.GetHashCode();
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00005348 File Offset: 0x00003548
		[NullableContext(1)]
		public int CompareTo(object obj)
		{
			int num;
			if (obj != null)
			{
				if (!(obj is DateTimeNanos))
				{
					throw new ArgumentException(string.Format("{0} is not a {1}, cannot compare.", obj, "DateTimeNanos"));
				}
				DateTimeNanos dateTimeNanos = (DateTimeNanos)obj;
				num = this.CompareTo(dateTimeNanos);
			}
			else
			{
				num = 1;
			}
			return num;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x000053A4 File Offset: 0x000035A4
		public int CompareTo(DateTimeNanos other)
		{
			return this.Ticks.CompareTo(other.Ticks);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x000053CC File Offset: 0x000035CC
		[NullableContext(1)]
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x000053D8 File Offset: 0x000035D8
		[NullableContext(2)]
		[return: Nullable(1)]
		public string ToString(string format, IFormatProvider formatProvider = null)
		{
			if (format == null)
			{
				format = "yyyy-MM-dd HH:mm:ss.fffffffff";
			}
			string text = (this.Ticks % 1000000000L).ToString("D9", CultureInfo.InvariantCulture);
			string text2 = format.Replace("fffffffff", "\"" + text + "\"");
			return this.DateTime.ToString(text2, formatProvider);
		}

		// Token: 0x0400004A RID: 74
		public readonly long Ticks;

		// Token: 0x0400004B RID: 75
		public static readonly DateTime MinDateTimeValue = new DateTimeNanos(long.MinValue).DateTime;

		// Token: 0x0400004C RID: 76
		public static readonly DateTime MaxDateTimeValue = new DateTimeNanos(long.MaxValue).DateTime;

		// Token: 0x0400004D RID: 77
		private const long DateTimeOffset = 621355968000000000L;

		// Token: 0x0400004E RID: 78
		[Nullable(1)]
		private const string DefaultFormat = "yyyy-MM-dd HH:mm:ss.fffffffff";
	}
}
