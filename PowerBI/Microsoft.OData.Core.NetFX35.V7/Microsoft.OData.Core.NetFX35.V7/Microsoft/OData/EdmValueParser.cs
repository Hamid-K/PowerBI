using System;
using System.Text.RegularExpressions;
using System.Xml;
using Microsoft.OData.Edm;

namespace Microsoft.OData
{
	// Token: 0x020000B7 RID: 183
	internal static class EdmValueParser
	{
		// Token: 0x06000719 RID: 1817 RVA: 0x00014687 File Offset: 0x00012887
		internal static TimeSpan ParseDuration(string value)
		{
			if (value == null || !EdmValueParser.DayTimeDurationValidator.IsMatch(value))
			{
				throw new FormatException(Strings.ValueParser_InvalidDuration(value));
			}
			return XmlConvert.ToTimeSpan(value);
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x000146AC File Offset: 0x000128AC
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

		// Token: 0x0600071B RID: 1819 RVA: 0x00014724 File Offset: 0x00012924
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

		// Token: 0x0600071C RID: 1820 RVA: 0x00014880 File Offset: 0x00012A80
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

		// Token: 0x0600071D RID: 1821 RVA: 0x000148D8 File Offset: 0x00012AD8
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

		// Token: 0x0600071E RID: 1822 RVA: 0x00014930 File Offset: 0x00012B30
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

		// Token: 0x0600071F RID: 1823 RVA: 0x00014988 File Offset: 0x00012B88
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

		// Token: 0x06000720 RID: 1824 RVA: 0x000149E0 File Offset: 0x00012BE0
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

		// Token: 0x06000721 RID: 1825 RVA: 0x00014A38 File Offset: 0x00012C38
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

		// Token: 0x06000722 RID: 1826 RVA: 0x00014A90 File Offset: 0x00012C90
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

		// Token: 0x06000723 RID: 1827 RVA: 0x00014AD0 File Offset: 0x00012CD0
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

		// Token: 0x06000724 RID: 1828 RVA: 0x00014B10 File Offset: 0x00012D10
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

		// Token: 0x06000725 RID: 1829 RVA: 0x00014B50 File Offset: 0x00012D50
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

		// Token: 0x040002FB RID: 763
		internal static readonly Regex DayTimeDurationValidator = PlatformHelper.CreateCompiled("^[^YM]*[DT].*$", 16);
	}
}
