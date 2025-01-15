using System;
using System.Globalization;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x020001F8 RID: 504
	public struct Date : IComparable, IComparable<Date>, IEquatable<Date>
	{
		// Token: 0x06000BAB RID: 2987 RVA: 0x00021554 File Offset: 0x0001F754
		public Date(int year, int month, int day)
		{
			this = default(Date);
			try
			{
				this.dateTime = new DateTime(year, month, day);
			}
			catch (ArgumentOutOfRangeException)
			{
				throw new FormatException(Strings.Date_InvalidDateParameters(year, month, day));
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06000BAC RID: 2988 RVA: 0x000215A8 File Offset: 0x0001F7A8
		public static Date Now
		{
			get
			{
				return DateTime.Now;
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06000BAD RID: 2989 RVA: 0x000215B4 File Offset: 0x0001F7B4
		public int Year
		{
			get
			{
				return this.dateTime.Year;
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06000BAE RID: 2990 RVA: 0x000215C1 File Offset: 0x0001F7C1
		public int Month
		{
			get
			{
				return this.dateTime.Month;
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06000BAF RID: 2991 RVA: 0x000215CE File Offset: 0x0001F7CE
		public int Day
		{
			get
			{
				return this.dateTime.Day;
			}
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x000215DB File Offset: 0x0001F7DB
		public static bool operator ==(Date firstOperand, Date secondOperand)
		{
			return firstOperand.dateTime == secondOperand.dateTime;
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x000215F0 File Offset: 0x0001F7F0
		public static bool operator !=(Date firstOperand, Date secondOperand)
		{
			return firstOperand.dateTime != secondOperand.dateTime;
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x00021605 File Offset: 0x0001F805
		public static bool operator <(Date firstOperand, Date secondOperand)
		{
			return firstOperand.dateTime < secondOperand.dateTime;
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x0002161A File Offset: 0x0001F81A
		public static bool operator <=(Date firstOperand, Date secondOperand)
		{
			return firstOperand.dateTime <= secondOperand.dateTime;
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x0002162F File Offset: 0x0001F82F
		public static bool operator >(Date firstOperand, Date secondOperand)
		{
			return firstOperand.dateTime > secondOperand.dateTime;
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x00021644 File Offset: 0x0001F844
		public static bool operator >=(Date firstOperand, Date secondOperand)
		{
			return firstOperand.dateTime >= secondOperand.dateTime;
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x0002165C File Offset: 0x0001F85C
		public Date AddYears(int value)
		{
			Date date;
			try
			{
				date = this.dateTime.AddYears(value);
			}
			catch (ArgumentOutOfRangeException)
			{
				throw new ArgumentOutOfRangeException("value", Strings.Date_InvalidAddedOrSubtractedResults);
			}
			return date;
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x000216A0 File Offset: 0x0001F8A0
		public Date AddMonths(int value)
		{
			Date date;
			try
			{
				date = this.dateTime.AddMonths(value);
			}
			catch (ArgumentOutOfRangeException)
			{
				throw new ArgumentOutOfRangeException("value", Strings.Date_InvalidAddedOrSubtractedResults);
			}
			return date;
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x000216E4 File Offset: 0x0001F8E4
		public Date AddDays(int value)
		{
			Date date;
			try
			{
				date = this.dateTime.AddDays((double)value);
			}
			catch (ArgumentOutOfRangeException)
			{
				throw new ArgumentOutOfRangeException("value", Strings.Date_InvalidAddedOrSubtractedResults);
			}
			return date;
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x00021728 File Offset: 0x0001F928
		public static implicit operator DateTime(Date operand)
		{
			return operand.dateTime;
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x00021731 File Offset: 0x0001F931
		public static implicit operator Date(DateTime operand)
		{
			return new Date(operand.Year, operand.Month, operand.Day);
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x0002174D File Offset: 0x0001F94D
		public override string ToString()
		{
			return this.dateTime.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x00021764 File Offset: 0x0001F964
		public int CompareTo(object obj)
		{
			if (obj is Date)
			{
				Date date = (Date)obj;
				return this.CompareTo(date);
			}
			throw new ArgumentException(Strings.Date_InvalidCompareToTarget(obj));
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x00021793 File Offset: 0x0001F993
		public int CompareTo(Date other)
		{
			return this.dateTime.CompareTo(other.dateTime);
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x000217A7 File Offset: 0x0001F9A7
		public bool Equals(Date other)
		{
			return this.dateTime.Equals(other.dateTime);
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x000217BB File Offset: 0x0001F9BB
		public override bool Equals(object obj)
		{
			return obj != null && obj is Date && this.dateTime.Equals(((Date)obj).dateTime);
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x000217E0 File Offset: 0x0001F9E0
		public override int GetHashCode()
		{
			return this.dateTime.GetHashCode();
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x000217F3 File Offset: 0x0001F9F3
		public static Date Parse(string text)
		{
			return Date.Parse(text, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x00021800 File Offset: 0x0001FA00
		public static Date Parse(string text, IFormatProvider provider)
		{
			Date date;
			if (Date.TryParse(text, provider, out date))
			{
				return date;
			}
			throw new FormatException(Strings.Date_InvalidParsingString(text));
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x00021825 File Offset: 0x0001FA25
		public static bool TryParse(string text, out Date result)
		{
			return Date.TryParse(text, CultureInfo.CurrentCulture, out result);
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x00021834 File Offset: 0x0001FA34
		public static bool TryParse(string text, IFormatProvider provider, out Date result)
		{
			DateTime dateTime;
			bool flag = DateTime.TryParse(text, provider, 0, ref dateTime);
			result = dateTime;
			return flag;
		}

		// Token: 0x04000565 RID: 1381
		public static readonly Date MinValue = new Date(1, 1, 1);

		// Token: 0x04000566 RID: 1382
		public static readonly Date MaxValue = new Date(9999, 12, 31);

		// Token: 0x04000567 RID: 1383
		private DateTime dateTime;
	}
}
