using System;
using System.Text.RegularExpressions;
using System.Xml;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Client
{
	// Token: 0x02000007 RID: 7
	internal static class EdmValueParser
	{
		// Token: 0x0600000F RID: 15 RVA: 0x000028DE File Offset: 0x00000ADE
		internal static TimeSpan ParseDuration(string value)
		{
			if (value == null || !EdmValueParser.DayTimeDurationValidator.IsMatch(value))
			{
				throw new FormatException(Strings.ValueParser_InvalidDuration(value));
			}
			return XmlConvert.ToTimeSpan(value);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002904 File Offset: 0x00000B04
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

		// Token: 0x06000011 RID: 17 RVA: 0x0000297C File Offset: 0x00000B7C
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

		// Token: 0x06000012 RID: 18 RVA: 0x00002AD8 File Offset: 0x00000CD8
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

		// Token: 0x06000013 RID: 19 RVA: 0x00002B30 File Offset: 0x00000D30
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

		// Token: 0x06000014 RID: 20 RVA: 0x00002B88 File Offset: 0x00000D88
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

		// Token: 0x06000015 RID: 21 RVA: 0x00002BE0 File Offset: 0x00000DE0
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

		// Token: 0x06000016 RID: 22 RVA: 0x00002C38 File Offset: 0x00000E38
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

		// Token: 0x06000017 RID: 23 RVA: 0x00002C90 File Offset: 0x00000E90
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

		// Token: 0x06000018 RID: 24 RVA: 0x00002CE8 File Offset: 0x00000EE8
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

		// Token: 0x06000019 RID: 25 RVA: 0x00002D28 File Offset: 0x00000F28
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

		// Token: 0x0600001A RID: 26 RVA: 0x00002D58 File Offset: 0x00000F58
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

		// Token: 0x0600001B RID: 27 RVA: 0x00002D98 File Offset: 0x00000F98
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

		// Token: 0x0400001C RID: 28
		internal static readonly Regex DayTimeDurationValidator = PlatformHelper.CreateCompiled("^[^YM]*[DT].*$", RegexOptions.Singleline);
	}
}
