using System;
using System.Text.RegularExpressions;
using System.Xml;

namespace Microsoft.OData.Edm.Csdl
{
	// Token: 0x02000142 RID: 322
	internal static class EdmValueParser
	{
		// Token: 0x060007C2 RID: 1986 RVA: 0x0001461C File Offset: 0x0001281C
		internal static TimeSpan ParseDuration(string value)
		{
			if (value == null || !EdmValueParser.DayTimeDurationValidator.IsMatch(value))
			{
				throw new FormatException(Strings.ValueParser_InvalidDuration(value));
			}
			return XmlConvert.ToTimeSpan(value);
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x00014640 File Offset: 0x00012840
		internal static bool TryParseBinary(string value, out byte[] result)
		{
			if (value.Length % 2 != 0)
			{
				result = null;
				return false;
			}
			result = new byte[value.Length >> 1];
			for (int i = 0; i < value.Length; i++)
			{
				byte b;
				if (!EdmValueParser.TryParseCharAsBinary(value.get_Chars(i), out b))
				{
					result = null;
					return false;
				}
				byte b2;
				if (!EdmValueParser.TryParseCharAsBinary(value.get_Chars(++i), out b2))
				{
					result = null;
					return false;
				}
				result[i >> 1] = (byte)(((int)b << 4) | (int)b2);
			}
			return true;
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x000146B8 File Offset: 0x000128B8
		internal static bool TryParseBool(string value, out bool? result)
		{
			switch (value.Length)
			{
			case 1:
			{
				char c = value.get_Chars(0);
				if (c == '0')
				{
					result = new bool?(false);
					return true;
				}
				if (c == '1')
				{
					result = new bool?(true);
					return true;
				}
				break;
			}
			case 4:
				if ((value.get_Chars(0) == 't' || value.get_Chars(0) == 'T') && (value.get_Chars(1) == 'r' || value.get_Chars(1) == 'R') && (value.get_Chars(2) == 'u' || value.get_Chars(2) == 'U') && (value.get_Chars(3) == 'e' || value.get_Chars(3) == 'E'))
				{
					result = new bool?(true);
					return true;
				}
				break;
			case 5:
				if ((value.get_Chars(0) == 'f' || value.get_Chars(0) == 'F') && (value.get_Chars(1) == 'a' || value.get_Chars(1) == 'A') && (value.get_Chars(2) == 'l' || value.get_Chars(2) == 'L') && (value.get_Chars(3) == 's' || value.get_Chars(3) == 'S') && (value.get_Chars(4) == 'e' || value.get_Chars(4) == 'E'))
				{
					result = new bool?(false);
					return true;
				}
				break;
			}
			result = default(bool?);
			return false;
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x00014814 File Offset: 0x00012A14
		internal static bool TryParseDuration(string value, out TimeSpan? result)
		{
			bool flag;
			try
			{
				result = new TimeSpan?(EdmValueParser.ParseDuration(value));
				flag = true;
			}
			catch (FormatException)
			{
				result = default(TimeSpan?);
				flag = false;
			}
			catch (OverflowException)
			{
				result = default(TimeSpan?);
				flag = false;
			}
			return flag;
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x0001486C File Offset: 0x00012A6C
		internal static bool TryParseDateTimeOffset(string value, out DateTimeOffset? result)
		{
			bool flag;
			try
			{
				result = new DateTimeOffset?(PlatformHelper.ConvertStringToDateTimeOffset(value));
				flag = true;
			}
			catch (FormatException)
			{
				result = default(DateTimeOffset?);
				flag = false;
			}
			catch (ArgumentOutOfRangeException)
			{
				result = default(DateTimeOffset?);
				flag = false;
			}
			return flag;
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x000148C4 File Offset: 0x00012AC4
		internal static bool TryParseInt(string value, out int? result)
		{
			bool flag;
			try
			{
				result = new int?(XmlConvert.ToInt32(value));
				flag = true;
			}
			catch (FormatException)
			{
				result = default(int?);
				flag = false;
			}
			catch (OverflowException)
			{
				result = default(int?);
				flag = false;
			}
			return flag;
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x0001491C File Offset: 0x00012B1C
		internal static bool TryParseLong(string value, out long? result)
		{
			bool flag;
			try
			{
				result = new long?(XmlConvert.ToInt64(value));
				flag = true;
			}
			catch (FormatException)
			{
				result = default(long?);
				flag = false;
			}
			catch (OverflowException)
			{
				result = default(long?);
				flag = false;
			}
			return flag;
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x00014974 File Offset: 0x00012B74
		internal static bool TryParseDecimal(string value, out decimal? result)
		{
			bool flag;
			try
			{
				result = new decimal?(XmlConvert.ToDecimal(value));
				flag = true;
			}
			catch (FormatException)
			{
				result = default(decimal?);
				flag = false;
			}
			catch (OverflowException)
			{
				result = default(decimal?);
				flag = false;
			}
			return flag;
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x000149CC File Offset: 0x00012BCC
		internal static bool TryParseFloat(string value, out double? result)
		{
			bool flag;
			try
			{
				result = new double?(XmlConvert.ToDouble(value));
				flag = true;
			}
			catch (FormatException)
			{
				result = default(double?);
				flag = false;
			}
			catch (OverflowException)
			{
				result = default(double?);
				flag = false;
			}
			return flag;
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x00014A24 File Offset: 0x00012C24
		internal static bool TryParseGuid(string value, out Guid? result)
		{
			bool flag;
			try
			{
				result = new Guid?(XmlConvert.ToGuid(value));
				flag = true;
			}
			catch (FormatException)
			{
				result = default(Guid?);
				flag = false;
			}
			return flag;
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x00014A64 File Offset: 0x00012C64
		internal static bool TryParseDate(string value, out Date? result)
		{
			bool flag;
			try
			{
				result = new Date?(PlatformHelper.ConvertStringToDate(value));
				flag = true;
			}
			catch (FormatException)
			{
				result = default(Date?);
				flag = false;
			}
			return flag;
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x00014AA4 File Offset: 0x00012CA4
		internal static bool TryParseTimeOfDay(string value, out TimeOfDay? result)
		{
			bool flag;
			try
			{
				result = new TimeOfDay?(PlatformHelper.ConvertStringToTimeOfDay(value));
				flag = true;
			}
			catch (FormatException)
			{
				result = default(TimeOfDay?);
				flag = false;
			}
			return flag;
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x00014AE4 File Offset: 0x00012CE4
		private static bool TryParseCharAsBinary(char c, out byte b)
		{
			uint num = (uint)(c - '0');
			if (num >= 0U && num <= 9U)
			{
				b = (byte)num;
				return true;
			}
			num = (uint)(c - 'A');
			if (num < 0U || num > 5U)
			{
				num = (uint)(c - 'a');
			}
			if (num >= 0U && num <= 5U)
			{
				b = (byte)(num + 10U);
				return true;
			}
			b = 0;
			return false;
		}

		// Token: 0x04000480 RID: 1152
		internal static readonly Regex DayTimeDurationValidator = PlatformHelper.CreateCompiled("^[^YM]*[DT].*$", 16);
	}
}
