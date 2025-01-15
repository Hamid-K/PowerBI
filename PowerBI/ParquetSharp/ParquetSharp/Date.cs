using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x02000031 RID: 49
	public readonly struct Date : IEquatable<Date>, IComparable, IComparable<Date>
	{
		// Token: 0x0600012F RID: 303 RVA: 0x0000513C File Offset: 0x0000333C
		public Date(int year, int month, int day)
		{
			this = new Date(new DateTime(year, month, day));
		}

		// Token: 0x06000130 RID: 304 RVA: 0x0000514C File Offset: 0x0000334C
		public Date(DateTime dateTime)
		{
			this = new Date((int)((dateTime.Ticks - 621355968000000000L) / 864000000000L));
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00005170 File Offset: 0x00003370
		public Date(int days)
		{
			this.Days = days;
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000132 RID: 306 RVA: 0x0000517C File Offset: 0x0000337C
		public DateTime DateTime
		{
			get
			{
				return new DateTime(621355968000000000L + (long)this.Days * 864000000000L);
			}
		}

		// Token: 0x06000133 RID: 307 RVA: 0x000051A0 File Offset: 0x000033A0
		public Date AddDays(int days)
		{
			return new Date(this.Days + days);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x000051B0 File Offset: 0x000033B0
		public bool Equals(Date other)
		{
			return this.Days == other.Days;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x000051C0 File Offset: 0x000033C0
		[NullableContext(1)]
		public override bool Equals(object obj)
		{
			if (obj is Date)
			{
				Date date = (Date)obj;
				return this.Equals(date);
			}
			return false;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000051EC File Offset: 0x000033EC
		public override int GetHashCode()
		{
			return this.Days;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x000051F4 File Offset: 0x000033F4
		[NullableContext(1)]
		public int CompareTo(object obj)
		{
			int num;
			if (obj != null)
			{
				if (!(obj is Date))
				{
					throw new ArgumentException(string.Format("{0} is not a {1}, cannot compare.", obj, "Date"));
				}
				Date date = (Date)obj;
				num = this.CompareTo(date);
			}
			else
			{
				num = 1;
			}
			return num;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00005250 File Offset: 0x00003450
		public int CompareTo(Date other)
		{
			return this.Days.CompareTo(other.Days);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00005278 File Offset: 0x00003478
		[NullableContext(1)]
		public override string ToString()
		{
			return this.DateTime.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
		}

		// Token: 0x04000048 RID: 72
		public readonly int Days;

		// Token: 0x04000049 RID: 73
		private const long BaseDateTimeTicks = 621355968000000000L;
	}
}
