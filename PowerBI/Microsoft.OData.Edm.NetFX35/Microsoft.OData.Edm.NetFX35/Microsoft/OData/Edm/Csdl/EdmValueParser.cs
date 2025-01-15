using System;
using System.Text.RegularExpressions;
using System.Xml;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Edm.Csdl
{
	// Token: 0x02000018 RID: 24
	internal static class EdmValueParser
	{
		// Token: 0x0600006D RID: 109 RVA: 0x00002C63 File Offset: 0x00000E63
		internal static TimeSpan ParseDuration(string value)
		{
			if (value == null || !EdmValueParser.DayTimeDurationValidator.IsMatch(value))
			{
				throw new FormatException(Strings.ValueParser_InvalidDuration(value));
			}
			return XmlConvert.ToTimeSpan(value);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002C88 File Offset: 0x00000E88
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

		// Token: 0x0600006F RID: 111 RVA: 0x00002D00 File Offset: 0x00000F00
		internal static bool TryParseBool(string value, out bool? result)
		{
			switch (value.Length)
			{
			case 1:
				switch (value.get_Chars(0))
				{
				case '0':
					result = new bool?(false);
					return true;
				case '1':
					result = new bool?(true);
					return true;
				}
				break;
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

		// Token: 0x06000070 RID: 112 RVA: 0x00002E68 File Offset: 0x00001068
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

		// Token: 0x06000071 RID: 113 RVA: 0x00002EC0 File Offset: 0x000010C0
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

		// Token: 0x06000072 RID: 114 RVA: 0x00002F18 File Offset: 0x00001118
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

		// Token: 0x06000073 RID: 115 RVA: 0x00002F70 File Offset: 0x00001170
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

		// Token: 0x06000074 RID: 116 RVA: 0x00002FC8 File Offset: 0x000011C8
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

		// Token: 0x06000075 RID: 117 RVA: 0x00003020 File Offset: 0x00001220
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

		// Token: 0x06000076 RID: 118 RVA: 0x00003078 File Offset: 0x00001278
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

		// Token: 0x06000077 RID: 119 RVA: 0x000030B8 File Offset: 0x000012B8
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

		// Token: 0x06000078 RID: 120 RVA: 0x000030F8 File Offset: 0x000012F8
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

		// Token: 0x06000079 RID: 121 RVA: 0x00003138 File Offset: 0x00001338
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

		// Token: 0x04000028 RID: 40
		internal static readonly Regex DayTimeDurationValidator = PlatformHelper.CreateCompiled("^[^YM]*[DT].*$", 16);
	}
}
