using System;
using System.Globalization;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000045 RID: 69
	public struct Date : IComparable, IComparable<Date>, IEquatable<Date>
	{
		// Token: 0x060002B4 RID: 692 RVA: 0x0000988C File Offset: 0x00007A8C
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

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x000098E0 File Offset: 0x00007AE0
		public static Date Now
		{
			get
			{
				return DateTime.Now;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x000098EC File Offset: 0x00007AEC
		public int Year
		{
			get
			{
				return this.dateTime.Year;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x000098F9 File Offset: 0x00007AF9
		public int Month
		{
			get
			{
				return this.dateTime.Month;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x00009906 File Offset: 0x00007B06
		public int Day
		{
			get
			{
				return this.dateTime.Day;
			}
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00009913 File Offset: 0x00007B13
		public static bool operator ==(Date firstOperand, Date secondOperand)
		{
			return firstOperand.dateTime == secondOperand.dateTime;
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00009926 File Offset: 0x00007B26
		public static bool operator !=(Date firstOperand, Date secondOperand)
		{
			return firstOperand.dateTime != secondOperand.dateTime;
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00009939 File Offset: 0x00007B39
		public static bool operator <(Date firstOperand, Date secondOperand)
		{
			return firstOperand.dateTime < secondOperand.dateTime;
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000994C File Offset: 0x00007B4C
		public static bool operator <=(Date firstOperand, Date secondOperand)
		{
			return firstOperand.dateTime <= secondOperand.dateTime;
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000995F File Offset: 0x00007B5F
		public static bool operator >(Date firstOperand, Date secondOperand)
		{
			return firstOperand.dateTime > secondOperand.dateTime;
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00009972 File Offset: 0x00007B72
		public static bool operator >=(Date firstOperand, Date secondOperand)
		{
			return firstOperand.dateTime >= secondOperand.dateTime;
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00009988 File Offset: 0x00007B88
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

		// Token: 0x060002C0 RID: 704 RVA: 0x000099CC File Offset: 0x00007BCC
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

		// Token: 0x060002C1 RID: 705 RVA: 0x00009A10 File Offset: 0x00007C10
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

		// Token: 0x060002C2 RID: 706 RVA: 0x00009A54 File Offset: 0x00007C54
		public static implicit operator DateTime(Date operand)
		{
			return operand.dateTime;
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00009A5C File Offset: 0x00007C5C
		public static implicit operator Date(DateTime operand)
		{
			return new Date(operand.Year, operand.Month, operand.Day);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00009A78 File Offset: 0x00007C78
		public override string ToString()
		{
			return this.dateTime.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00009A90 File Offset: 0x00007C90
		public int CompareTo(object obj)
		{
			if (obj is Date)
			{
				Date date = (Date)obj;
				return this.CompareTo(date);
			}
			throw new ArgumentException(Strings.Date_InvalidCompareToTarget(obj));
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00009ABF File Offset: 0x00007CBF
		public int CompareTo(Date other)
		{
			return this.dateTime.CompareTo(other.dateTime);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00009AD2 File Offset: 0x00007CD2
		public bool Equals(Date other)
		{
			return this.dateTime.Equals(other.dateTime);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00009AE5 File Offset: 0x00007CE5
		public override bool Equals(object obj)
		{
			return obj != null && obj is Date && this.dateTime.Equals(((Date)obj).dateTime);
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00009B0A File Offset: 0x00007D0A
		public override int GetHashCode()
		{
			return this.dateTime.GetHashCode();
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00009B17 File Offset: 0x00007D17
		public static Date Parse(string text)
		{
			return Date.Parse(text, CultureInfo.CurrentCulture);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00009B24 File Offset: 0x00007D24
		public static Date Parse(string text, IFormatProvider provider)
		{
			Date date;
			if (Date.TryParse(text, provider, out date))
			{
				return date;
			}
			throw new FormatException(Strings.Date_InvalidParsingString(text));
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00009B49 File Offset: 0x00007D49
		public static bool TryParse(string text, out Date result)
		{
			return Date.TryParse(text, CultureInfo.CurrentCulture, out result);
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00009B58 File Offset: 0x00007D58
		public static bool TryParse(string text, IFormatProvider provider, out Date result)
		{
			DateTime dateTime;
			bool flag = DateTime.TryParse(text, provider, 0, ref dateTime);
			result = dateTime;
			return flag;
		}

		// Token: 0x0400006A RID: 106
		public static readonly Date MinValue = new Date(1, 1, 1);

		// Token: 0x0400006B RID: 107
		public static readonly Date MaxValue = new Date(9999, 12, 31);

		// Token: 0x0400006C RID: 108
		private DateTime dateTime;
	}
}
