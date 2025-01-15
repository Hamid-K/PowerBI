using System;
using System.Text.RegularExpressions;
using System.Xml;
using Microsoft.OData.Edm;

namespace Microsoft.OData
{
	// Token: 0x020000EC RID: 236
	internal static class EdmValueParser
	{
		// Token: 0x06000AC2 RID: 2754 RVA: 0x0001CD51 File Offset: 0x0001AF51
		internal static TimeSpan ParseDuration(string value)
		{
			if (value == null || !EdmValueParser.DayTimeDurationValidator.IsMatch(value))
			{
				throw new FormatException(Strings.ValueParser_InvalidDuration(value));
			}
			return XmlConvert.ToTimeSpan(value);
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x0001CD78 File Offset: 0x0001AF78
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
				if (!EdmValueParser.TryParseCharAsBinary(value[i], out b))
				{
					result = null;
					return false;
				}
				byte b2;
				if (!EdmValueParser.TryParseCharAsBinary(value[++i], out b2))
				{
					result = null;
					return false;
				}
				result[i >> 1] = (byte)(((int)b << 4) | (int)b2);
			}
			return true;
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x0001CDF0 File Offset: 0x0001AFF0
		internal static bool TryParseBool(string value, out bool? result)
		{
			switch (value.Length)
			{
			case 1:
			{
				char c = value[0];
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
				if ((value[0] == 't' || value[0] == 'T') && (value[1] == 'r' || value[1] == 'R') && (value[2] == 'u' || value[2] == 'U') && (value[3] == 'e' || value[3] == 'E'))
				{
					result = new bool?(true);
					return true;
				}
				break;
			case 5:
				if ((value[0] == 'f' || value[0] == 'F') && (value[1] == 'a' || value[1] == 'A') && (value[2] == 'l' || value[2] == 'L') && (value[3] == 's' || value[3] == 'S') && (value[4] == 'e' || value[4] == 'E'))
				{
					result = new bool?(false);
					return true;
				}
				break;
			}
			result = null;
			return false;
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x0001CF4C File Offset: 0x0001B14C
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
				result = null;
				flag = false;
			}
			catch (OverflowException)
			{
				result = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x0001CFA4 File Offset: 0x0001B1A4
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
				result = null;
				flag = false;
			}
			catch (ArgumentOutOfRangeException)
			{
				result = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x0001CFFC File Offset: 0x0001B1FC
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
				result = null;
				flag = false;
			}
			catch (OverflowException)
			{
				result = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x0001D054 File Offset: 0x0001B254
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
				result = null;
				flag = false;
			}
			catch (OverflowException)
			{
				result = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x0001D0AC File Offset: 0x0001B2AC
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
				result = null;
				flag = false;
			}
			catch (OverflowException)
			{
				result = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x0001D104 File Offset: 0x0001B304
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
				result = null;
				flag = false;
			}
			catch (OverflowException)
			{
				result = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x0001D15C File Offset: 0x0001B35C
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
				result = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x0001D19C File Offset: 0x0001B39C
		internal static bool TryParseDate(string value, out Date? result)
		{
			result = null;
			Date date;
			if (PlatformHelper.TryConvertStringToDate(value, out date))
			{
				result = new Date?(date);
				return true;
			}
			return false;
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x0001D1CC File Offset: 0x0001B3CC
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
				result = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x0001D20C File Offset: 0x0001B40C
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

		// Token: 0x040003DC RID: 988
		internal static readonly Regex DayTimeDurationValidator = PlatformHelper.CreateCompiled("^[^YM]*[DT].*$", RegexOptions.Singleline);
	}
}
