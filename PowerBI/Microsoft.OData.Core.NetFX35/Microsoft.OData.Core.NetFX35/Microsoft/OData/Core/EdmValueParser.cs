using System;
using System.Text.RegularExpressions;
using System.Xml;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Core
{
	// Token: 0x020002A2 RID: 674
	internal static class EdmValueParser
	{
		// Token: 0x06001733 RID: 5939 RVA: 0x0004F9BA File Offset: 0x0004DBBA
		internal static TimeSpan ParseDuration(string value)
		{
			if (value == null || !EdmValueParser.DayTimeDurationValidator.IsMatch(value))
			{
				throw new FormatException(Strings.ValueParser_InvalidDuration(value));
			}
			return XmlConvert.ToTimeSpan(value);
		}

		// Token: 0x06001734 RID: 5940 RVA: 0x0004F9E0 File Offset: 0x0004DBE0
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

		// Token: 0x06001735 RID: 5941 RVA: 0x0004FA58 File Offset: 0x0004DC58
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

		// Token: 0x06001736 RID: 5942 RVA: 0x0004FBC0 File Offset: 0x0004DDC0
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

		// Token: 0x06001737 RID: 5943 RVA: 0x0004FC18 File Offset: 0x0004DE18
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

		// Token: 0x06001738 RID: 5944 RVA: 0x0004FC70 File Offset: 0x0004DE70
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

		// Token: 0x06001739 RID: 5945 RVA: 0x0004FCC8 File Offset: 0x0004DEC8
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

		// Token: 0x0600173A RID: 5946 RVA: 0x0004FD20 File Offset: 0x0004DF20
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

		// Token: 0x0600173B RID: 5947 RVA: 0x0004FD78 File Offset: 0x0004DF78
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

		// Token: 0x0600173C RID: 5948 RVA: 0x0004FDD0 File Offset: 0x0004DFD0
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

		// Token: 0x0600173D RID: 5949 RVA: 0x0004FE10 File Offset: 0x0004E010
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

		// Token: 0x0600173E RID: 5950 RVA: 0x0004FE50 File Offset: 0x0004E050
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

		// Token: 0x0600173F RID: 5951 RVA: 0x0004FE90 File Offset: 0x0004E090
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

		// Token: 0x04000A0B RID: 2571
		internal static readonly Regex DayTimeDurationValidator = PlatformHelper.CreateCompiled("^[^YM]*[DT].*$", 16);
	}
}
