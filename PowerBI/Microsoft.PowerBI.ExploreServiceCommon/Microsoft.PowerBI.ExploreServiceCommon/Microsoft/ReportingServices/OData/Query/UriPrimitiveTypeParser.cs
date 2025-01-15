using System;
using System.Globalization;
using System.Text;
using System.Xml;
using Microsoft.ReportingServices.OData.Json;

namespace Microsoft.ReportingServices.OData.Query
{
	// Token: 0x02000011 RID: 17
	internal static class UriPrimitiveTypeParser
	{
		// Token: 0x06000053 RID: 83 RVA: 0x0000292B File Offset: 0x00000B2B
		internal static bool IsCharHexDigit(char c)
		{
			return (c >= '0' && c <= '9') || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F');
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002954 File Offset: 0x00000B54
		internal static bool TryUriStringToPrimitive(string text, PrimitiveTypeKind primitiveTypeKind, out object targetValue)
		{
			byte[] array;
			bool flag = UriPrimitiveTypeParser.TryUriStringToByteArray(text, out array);
			if (primitiveTypeKind == PrimitiveTypeKind.Binary)
			{
				targetValue = array;
				return flag;
			}
			if (flag)
			{
				return UriPrimitiveTypeParser.TryUriStringToPrimitive(Encoding.UTF8.GetString(array, 0, array.Length), primitiveTypeKind, out targetValue);
			}
			if (primitiveTypeKind == PrimitiveTypeKind.Guid)
			{
				Guid guid;
				bool flag2 = UriPrimitiveTypeParser.TryUriStringToGuid(text, out guid);
				targetValue = guid;
				return flag2;
			}
			if (primitiveTypeKind == PrimitiveTypeKind.DateTime)
			{
				DateTime dateTime;
				bool flag3 = UriPrimitiveTypeParser.TryUriStringToDateTime(text, out dateTime);
				targetValue = dateTime;
				return flag3;
			}
			if (primitiveTypeKind == PrimitiveTypeKind.DateTimeOffset)
			{
				DateTimeOffset dateTimeOffset;
				bool flag4 = UriPrimitiveTypeParser.TryUriStringToDateTimeOffset(text, out dateTimeOffset);
				targetValue = dateTimeOffset;
				return flag4;
			}
			if (primitiveTypeKind == PrimitiveTypeKind.Time)
			{
				TimeSpan timeSpan;
				bool flag5 = UriPrimitiveTypeParser.TryUriStringToTime(text, out timeSpan);
				targetValue = timeSpan;
				return flag5;
			}
			bool flag6 = primitiveTypeKind == PrimitiveTypeKind.String;
			if (flag6 != UriPrimitiveTypeParser.IsUriValueQuoted(text))
			{
				targetValue = null;
				return false;
			}
			if (flag6)
			{
				text = UriPrimitiveTypeParser.RemoveQuotes(text);
			}
			bool flag7;
			try
			{
				switch (primitiveTypeKind)
				{
				case PrimitiveTypeKind.Boolean:
					targetValue = XmlConvert.ToBoolean(text.ToLowerInvariant());
					goto IL_0236;
				case PrimitiveTypeKind.Byte:
					targetValue = XmlConvert.ToByte(text);
					goto IL_0236;
				case PrimitiveTypeKind.Decimal:
					if (UriPrimitiveTypeParser.TryRemoveLiteralSuffix("M", ref text))
					{
						try
						{
							targetValue = XmlConvert.ToDecimal(text);
							goto IL_0236;
						}
						catch (FormatException)
						{
							decimal num;
							if (decimal.TryParse(text, NumberStyles.Float, NumberFormatInfo.InvariantInfo, out num))
							{
								targetValue = num;
								goto IL_0236;
							}
							targetValue = 0m;
							return false;
						}
					}
					targetValue = 0m;
					return false;
				case PrimitiveTypeKind.Double:
					UriPrimitiveTypeParser.TryRemoveLiteralSuffix("D", ref text);
					targetValue = XmlConvert.ToDouble(text);
					goto IL_0236;
				case PrimitiveTypeKind.Single:
					if (UriPrimitiveTypeParser.TryRemoveLiteralSuffix("f", ref text))
					{
						targetValue = XmlConvert.ToSingle(text);
						goto IL_0236;
					}
					targetValue = 0f;
					return false;
				case PrimitiveTypeKind.SByte:
					targetValue = XmlConvert.ToSByte(text);
					goto IL_0236;
				case PrimitiveTypeKind.Int16:
					targetValue = XmlConvert.ToInt16(text);
					goto IL_0236;
				case PrimitiveTypeKind.Int32:
					targetValue = XmlConvert.ToInt32(text);
					goto IL_0236;
				case PrimitiveTypeKind.Int64:
					if (UriPrimitiveTypeParser.TryRemoveLiteralSuffix("L", ref text))
					{
						targetValue = XmlConvert.ToInt64(text);
						goto IL_0236;
					}
					targetValue = 0L;
					return false;
				case PrimitiveTypeKind.String:
					targetValue = text;
					goto IL_0236;
				}
				throw new InvalidOperationException(Errors.UnsupportedPrimitiveType(primitiveTypeKind));
				IL_0236:
				flag7 = true;
			}
			catch (FormatException)
			{
				targetValue = null;
				flag7 = false;
			}
			catch (OverflowException)
			{
				targetValue = null;
				flag7 = false;
			}
			return flag7;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002BFC File Offset: 0x00000DFC
		internal static bool TryUriStringToNonNegativeInteger(string text, out int nonNegativeInteger)
		{
			object obj;
			if (!UriPrimitiveTypeParser.TryUriStringToPrimitive(text, PrimitiveTypeKind.Int32, out obj))
			{
				nonNegativeInteger = -1;
				return false;
			}
			nonNegativeInteger = (int)obj;
			return nonNegativeInteger >= 0;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002C2C File Offset: 0x00000E2C
		private static bool TryUriStringToByteArray(string text, out byte[] targetValue)
		{
			if (!UriPrimitiveTypeParser.TryRemoveLiteralPrefix("binary", ref text) && !UriPrimitiveTypeParser.TryRemoveLiteralPrefix("X", ref text))
			{
				targetValue = null;
				return false;
			}
			if (!UriPrimitiveTypeParser.TryRemoveQuotes(ref text))
			{
				targetValue = null;
				return false;
			}
			if (text.Length % 2 != 0)
			{
				targetValue = null;
				return false;
			}
			byte[] array = new byte[text.Length / 2];
			int i = 0;
			int num = 0;
			while (i < array.Length)
			{
				char c = text[num];
				char c2 = text[num + 1];
				if (!UriPrimitiveTypeParser.IsCharHexDigit(c) || !UriPrimitiveTypeParser.IsCharHexDigit(c2))
				{
					targetValue = null;
					return false;
				}
				array[i] = (byte)(UriPrimitiveTypeParser.HexCharToNibble(c) << 4) + UriPrimitiveTypeParser.HexCharToNibble(c2);
				num += 2;
				i++;
			}
			targetValue = array;
			return true;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002CDC File Offset: 0x00000EDC
		private static bool TryUriStringToGuid(string text, out Guid targetValue)
		{
			if (!UriPrimitiveTypeParser.TryRemoveLiteralPrefix("guid", ref text))
			{
				targetValue = default(Guid);
				return false;
			}
			if (!UriPrimitiveTypeParser.TryRemoveQuotes(ref text))
			{
				targetValue = default(Guid);
				return false;
			}
			bool flag;
			try
			{
				targetValue = XmlConvert.ToGuid(text);
				flag = true;
			}
			catch (FormatException)
			{
				targetValue = default(Guid);
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002D40 File Offset: 0x00000F40
		private static bool TryUriStringToDateTime(string text, out DateTime targetValue)
		{
			targetValue = default(DateTime);
			if (!UriPrimitiveTypeParser.TryRemoveLiteralPrefix("datetime", ref text))
			{
				return false;
			}
			if (!UriPrimitiveTypeParser.TryRemoveQuotes(ref text))
			{
				return false;
			}
			bool flag;
			try
			{
				targetValue = XmlConvert.ToDateTime(text, XmlDateTimeSerializationMode.Unspecified);
				flag = true;
			}
			catch (FormatException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002D98 File Offset: 0x00000F98
		private static bool TryUriStringToDateTimeOffset(string text, out DateTimeOffset targetValue)
		{
			targetValue = default(DateTimeOffset);
			if (!UriPrimitiveTypeParser.TryRemoveLiteralPrefix("datetimeoffset", ref text))
			{
				return false;
			}
			if (!UriPrimitiveTypeParser.TryRemoveQuotes(ref text))
			{
				return false;
			}
			bool flag;
			try
			{
				targetValue = XmlConvert.ToDateTimeOffset(text);
				flag = true;
			}
			catch (FormatException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002DF0 File Offset: 0x00000FF0
		private static bool TryUriStringToTime(string text, out TimeSpan targetValue)
		{
			if (!UriPrimitiveTypeParser.TryRemoveLiteralPrefix("time", ref text))
			{
				targetValue = default(TimeSpan);
				return false;
			}
			if (!UriPrimitiveTypeParser.TryRemoveQuotes(ref text))
			{
				targetValue = default(TimeSpan);
				return false;
			}
			bool flag;
			try
			{
				targetValue = XmlConvert.ToTimeSpan(text);
				flag = true;
			}
			catch (FormatException)
			{
				targetValue = default(TimeSpan);
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002E54 File Offset: 0x00001054
		private static bool TryRemoveQuotes(ref string text)
		{
			if (text.Length < 2)
			{
				return false;
			}
			char c = text[0];
			if (c != '\'' || text[text.Length - 1] != c)
			{
				return false;
			}
			string text2 = text.Substring(1, text.Length - 2);
			int num = 0;
			for (;;)
			{
				int num2 = text2.IndexOf(c, num);
				if (num2 < 0)
				{
					goto IL_0076;
				}
				text2 = text2.Remove(num2, 1);
				if (text2.Length < num2 + 1 || text2[num2] != c)
				{
					break;
				}
				num = num2 + 1;
			}
			return false;
			IL_0076:
			text = text2;
			return true;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002EDC File Offset: 0x000010DC
		private static bool TryRemoveLiteralSuffix(string suffix, ref string text)
		{
			text = text.Trim(UriPrimitiveTypeParser.WhitespaceChars);
			if (text.Length <= suffix.Length || !text.EndsWith(suffix, StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			text = text.Substring(0, text.Length - suffix.Length);
			return true;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002F2C File Offset: 0x0000112C
		private static bool TryRemoveLiteralPrefix(string prefix, ref string text)
		{
			if (text.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
			{
				text = text.Remove(0, prefix.Length);
				return true;
			}
			return false;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002F4C File Offset: 0x0000114C
		private static bool IsUriValueQuoted(string text)
		{
			if (text.Length < 2 || text[0] != '\'' || text[text.Length - 1] != '\'')
			{
				return false;
			}
			int num;
			for (int i = 1; i < text.Length - 1; i = num + 2)
			{
				num = text.IndexOf('\'', i, text.Length - i - 1);
				if (num == -1)
				{
					break;
				}
				if (num == text.Length - 2 || text[num + 1] != '\'')
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002FC8 File Offset: 0x000011C8
		private static string RemoveQuotes(string text)
		{
			char c = text[0];
			string text2 = text.Substring(1, text.Length - 2);
			int num = 0;
			for (;;)
			{
				int num2 = text2.IndexOf(c, num);
				if (num2 < 0)
				{
					break;
				}
				text2 = text2.Remove(num2, 1);
				num = num2 + 1;
			}
			return text2;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000300C File Offset: 0x0000120C
		private static byte HexCharToNibble(char c)
		{
			switch (c)
			{
			case '0':
				return 0;
			case '1':
				return 1;
			case '2':
				return 2;
			case '3':
				return 3;
			case '4':
				return 4;
			case '5':
				return 5;
			case '6':
				return 6;
			case '7':
				return 7;
			case '8':
				return 8;
			case '9':
				return 9;
			case ':':
			case ';':
			case '<':
			case '=':
			case '>':
			case '?':
			case '@':
				goto IL_00AF;
			case 'A':
				break;
			case 'B':
				return 11;
			case 'C':
				return 12;
			case 'D':
				return 13;
			case 'E':
				return 14;
			case 'F':
				return 15;
			default:
				switch (c)
				{
				case 'a':
					break;
				case 'b':
					return 11;
				case 'c':
					return 12;
				case 'd':
					return 13;
				case 'e':
					return 14;
				case 'f':
					return 15;
				default:
					goto IL_00AF;
				}
				break;
			}
			return 10;
			IL_00AF:
			throw new InvalidOperationException(Errors.InvalidHexChar(new string(c, 1)));
		}

		// Token: 0x04000063 RID: 99
		private static char[] WhitespaceChars = new char[] { ' ', '\t', '\n', '\r' };
	}
}
