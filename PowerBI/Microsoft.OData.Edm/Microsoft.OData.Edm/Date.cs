using System;
using System.Globalization;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000B1 RID: 177
	public struct Date : IComparable, IComparable<Date>, IEquatable<Date>
	{
		// Token: 0x060003F7 RID: 1015 RVA: 0x0000A800 File Offset: 0x00008A00
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

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x0000A854 File Offset: 0x00008A54
		public static Date Now
		{
			get
			{
				return DateTime.Now;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x0000A860 File Offset: 0x00008A60
		public int Year
		{
			get
			{
				return this.dateTime.Year;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x0000A86D File Offset: 0x00008A6D
		public int Month
		{
			get
			{
				return this.dateTime.Month;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x0000A87A File Offset: 0x00008A7A
		public int Day
		{
			get
			{
				return this.dateTime.Day;
			}
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0000A887 File Offset: 0x00008A87
		public static bool operator ==(Date firstOperand, Date secondOperand)
		{
			return firstOperand.dateTime == secondOperand.dateTime;
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000A89A File Offset: 0x00008A9A
		public static bool operator !=(Date firstOperand, Date secondOperand)
		{
			return firstOperand.dateTime != secondOperand.dateTime;
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0000A8AD File Offset: 0x00008AAD
		public static bool operator <(Date firstOperand, Date secondOperand)
		{
			return firstOperand.dateTime < secondOperand.dateTime;
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000A8C0 File Offset: 0x00008AC0
		public static bool operator <=(Date firstOperand, Date secondOperand)
		{
			return firstOperand.dateTime <= secondOperand.dateTime;
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0000A8D3 File Offset: 0x00008AD3
		public static bool operator >(Date firstOperand, Date secondOperand)
		{
			return firstOperand.dateTime > secondOperand.dateTime;
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0000A8E6 File Offset: 0x00008AE6
		public static bool operator >=(Date firstOperand, Date secondOperand)
		{
			return firstOperand.dateTime >= secondOperand.dateTime;
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000A8FC File Offset: 0x00008AFC
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

		// Token: 0x06000403 RID: 1027 RVA: 0x0000A940 File Offset: 0x00008B40
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

		// Token: 0x06000404 RID: 1028 RVA: 0x0000A984 File Offset: 0x00008B84
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

		// Token: 0x06000405 RID: 1029 RVA: 0x0000A9C8 File Offset: 0x00008BC8
		public static implicit operator DateTime(Date operand)
		{
			return operand.dateTime;
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0000A9D0 File Offset: 0x00008BD0
		public static implicit operator Date(DateTime operand)
		{
			return new Date(operand.Year, operand.Month, operand.Day);
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0000A9EC File Offset: 0x00008BEC
		public override string ToString()
		{
			return this.dateTime.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000AA04 File Offset: 0x00008C04
		public int CompareTo(object obj)
		{
			if (obj is Date)
			{
				Date date = (Date)obj;
				return this.CompareTo(date);
			}
			throw new ArgumentException(Strings.Date_InvalidCompareToTarget(obj));
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0000AA33 File Offset: 0x00008C33
		public int CompareTo(Date other)
		{
			return this.dateTime.CompareTo(other.dateTime);
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0000AA46 File Offset: 0x00008C46
		public bool Equals(Date other)
		{
			return this.dateTime.Equals(other.dateTime);
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000AA59 File Offset: 0x00008C59
		public override bool Equals(object obj)
		{
			return obj != null && obj is Date && this.dateTime.Equals(((Date)obj).dateTime);
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0000AA7E File Offset: 0x00008C7E
		public override int GetHashCode()
		{
			return this.dateTime.GetHashCode();
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0000AA8B File Offset: 0x00008C8B
		public static Date Parse(string text)
		{
			return Date.Parse(text, CultureInfo.CurrentCulture);
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0000AA98 File Offset: 0x00008C98
		public static Date Parse(string text, IFormatProvider provider)
		{
			Date date;
			if (Date.TryParse(text, provider, out date))
			{
				return date;
			}
			throw new FormatException(Strings.Date_InvalidParsingString(text));
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000AABD File Offset: 0x00008CBD
		public static bool TryParse(string text, out Date result)
		{
			return Date.TryParse(text, CultureInfo.CurrentCulture, out result);
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000AACC File Offset: 0x00008CCC
		public static bool TryParse(string text, IFormatProvider provider, out Date result)
		{
			DateTime dateTime;
			bool flag = DateTime.TryParse(text, provider, DateTimeStyles.None, out dateTime);
			result = dateTime;
			return flag;
		}

		// Token: 0x0400013B RID: 315
		public static readonly Date MinValue = new Date(1, 1, 1);

		// Token: 0x0400013C RID: 316
		public static readonly Date MaxValue = new Date(9999, 12, 31);

		// Token: 0x0400013D RID: 317
		private DateTime dateTime;
	}
}
