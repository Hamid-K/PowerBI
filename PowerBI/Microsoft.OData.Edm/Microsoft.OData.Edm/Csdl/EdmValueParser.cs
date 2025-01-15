using System;
using System.Text.RegularExpressions;
using System.Xml;

namespace Microsoft.OData.Edm.Csdl
{
	// Token: 0x02000150 RID: 336
	internal static class EdmValueParser
	{
		// Token: 0x06000865 RID: 2149 RVA: 0x00016263 File Offset: 0x00014463
		internal static TimeSpan ParseDuration(string value)
		{
			if (value == null || !EdmValueParser.DayTimeDurationValidator.IsMatch(value))
			{
				throw new FormatException(Strings.ValueParser_InvalidDuration(value));
			}
			return XmlConvert.ToTimeSpan(value);
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x00016288 File Offset: 0x00014488
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

		// Token: 0x06000867 RID: 2151 RVA: 0x00016300 File Offset: 0x00014500
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

		// Token: 0x06000868 RID: 2152 RVA: 0x0001645C File Offset: 0x0001465C
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

		// Token: 0x06000869 RID: 2153 RVA: 0x000164B4 File Offset: 0x000146B4
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

		// Token: 0x0600086A RID: 2154 RVA: 0x0001650C File Offset: 0x0001470C
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

		// Token: 0x0600086B RID: 2155 RVA: 0x00016564 File Offset: 0x00014764
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

		// Token: 0x0600086C RID: 2156 RVA: 0x000165BC File Offset: 0x000147BC
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

		// Token: 0x0600086D RID: 2157 RVA: 0x00016614 File Offset: 0x00014814
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

		// Token: 0x0600086E RID: 2158 RVA: 0x0001666C File Offset: 0x0001486C
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

		// Token: 0x0600086F RID: 2159 RVA: 0x000166AC File Offset: 0x000148AC
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

		// Token: 0x06000870 RID: 2160 RVA: 0x000166DC File Offset: 0x000148DC
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

		// Token: 0x06000871 RID: 2161 RVA: 0x0001671C File Offset: 0x0001491C
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

		// Token: 0x040004E9 RID: 1257
		internal static readonly Regex DayTimeDurationValidator = PlatformHelper.CreateCompiled("^[^YM]*[DT].*$", RegexOptions.Singleline);
	}
}
