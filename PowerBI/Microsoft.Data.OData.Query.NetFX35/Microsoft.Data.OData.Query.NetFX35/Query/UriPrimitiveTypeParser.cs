using System;
using System.Globalization;
using System.Spatial;
using System.Text;
using System.Xml;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;
using Microsoft.Data.Experimental.OData.Metadata;
using Microsoft.Data.OData;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x02000053 RID: 83
	internal static class UriPrimitiveTypeParser
	{
		// Token: 0x06000202 RID: 514 RVA: 0x0000AD14 File Offset: 0x00008F14
		internal static bool IsCharHexDigit(char c)
		{
			return (c >= '0' && c <= '9') || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F');
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000AD3C File Offset: 0x00008F3C
		internal static bool TryUriStringToPrimitive(string text, IEdmTypeReference targetType, out object targetValue)
		{
			if (targetType.IsNullable && text == "null")
			{
				targetValue = null;
				return true;
			}
			IEdmPrimitiveTypeReference edmPrimitiveTypeReference = targetType.AsPrimitiveOrNull();
			if (edmPrimitiveTypeReference == null)
			{
				targetValue = null;
				return false;
			}
			EdmPrimitiveTypeKind edmPrimitiveTypeKind = edmPrimitiveTypeReference.PrimitiveKind();
			byte[] array;
			bool flag = UriPrimitiveTypeParser.TryUriStringToByteArray(text, out array);
			if (edmPrimitiveTypeKind == EdmPrimitiveTypeKind.Binary)
			{
				targetValue = array;
				return flag;
			}
			if (flag)
			{
				string @string = Encoding.UTF8.GetString(array, 0, array.Length);
				return UriPrimitiveTypeParser.TryUriStringToPrimitive(@string, targetType, out targetValue);
			}
			if (edmPrimitiveTypeKind == EdmPrimitiveTypeKind.Guid)
			{
				Guid guid;
				bool flag2 = UriPrimitiveTypeParser.TryUriStringToGuid(text, out guid);
				targetValue = guid;
				return flag2;
			}
			if (edmPrimitiveTypeKind == EdmPrimitiveTypeKind.DateTime)
			{
				DateTime dateTime;
				bool flag3 = UriPrimitiveTypeParser.TryUriStringToDateTime(text, out dateTime);
				targetValue = dateTime;
				return flag3;
			}
			if (edmPrimitiveTypeKind == EdmPrimitiveTypeKind.DateTimeOffset)
			{
				DateTimeOffset dateTimeOffset;
				bool flag4 = UriPrimitiveTypeParser.TryUriStringToDateTimeOffset(text, out dateTimeOffset);
				targetValue = dateTimeOffset;
				return flag4;
			}
			if (edmPrimitiveTypeKind == EdmPrimitiveTypeKind.Time)
			{
				TimeSpan timeSpan;
				bool flag5 = UriPrimitiveTypeParser.TryUriStringToTime(text, out timeSpan);
				targetValue = timeSpan;
				return flag5;
			}
			if (edmPrimitiveTypeKind == EdmPrimitiveTypeKind.Geography)
			{
				Geography geography;
				bool flag6 = UriPrimitiveTypeParser.TryUriStringToGeography(text, out geography);
				targetValue = geography;
				return flag6;
			}
			if (edmPrimitiveTypeKind == EdmPrimitiveTypeKind.Geometry)
			{
				Geometry geometry;
				bool flag7 = UriPrimitiveTypeParser.TryUriStringToGeometry(text, out geometry);
				targetValue = geometry;
				return flag7;
			}
			bool flag8 = edmPrimitiveTypeKind == EdmPrimitiveTypeKind.String;
			if (flag8 != UriPrimitiveTypeParser.IsUriValueQuoted(text))
			{
				targetValue = null;
				return false;
			}
			if (flag8)
			{
				text = UriPrimitiveTypeParser.RemoveQuotes(text);
			}
			bool flag9;
			try
			{
				switch (edmPrimitiveTypeKind)
				{
				case EdmPrimitiveTypeKind.Boolean:
					targetValue = XmlConvert.ToBoolean(text);
					goto IL_02B3;
				case EdmPrimitiveTypeKind.Byte:
					targetValue = XmlConvert.ToByte(text);
					goto IL_02B3;
				case EdmPrimitiveTypeKind.Decimal:
					if (UriPrimitiveTypeParser.TryRemoveLiteralSuffix("M", ref text))
					{
						try
						{
							targetValue = XmlConvert.ToDecimal(text);
							goto IL_02B3;
						}
						catch (FormatException)
						{
							decimal num;
							if (decimal.TryParse(text, 167, NumberFormatInfo.InvariantInfo, ref num))
							{
								targetValue = num;
								goto IL_02B3;
							}
							targetValue = 0m;
							return false;
						}
					}
					targetValue = 0m;
					return false;
				case EdmPrimitiveTypeKind.Double:
					UriPrimitiveTypeParser.TryRemoveLiteralSuffix("D", ref text);
					targetValue = XmlConvert.ToDouble(text);
					goto IL_02B3;
				case EdmPrimitiveTypeKind.Int16:
					targetValue = XmlConvert.ToInt16(text);
					goto IL_02B3;
				case EdmPrimitiveTypeKind.Int32:
					targetValue = XmlConvert.ToInt32(text);
					goto IL_02B3;
				case EdmPrimitiveTypeKind.Int64:
					if (UriPrimitiveTypeParser.TryRemoveLiteralSuffix("L", ref text))
					{
						targetValue = XmlConvert.ToInt64(text);
						goto IL_02B3;
					}
					targetValue = 0L;
					return false;
				case EdmPrimitiveTypeKind.SByte:
					targetValue = XmlConvert.ToSByte(text);
					goto IL_02B3;
				case EdmPrimitiveTypeKind.Single:
					if (UriPrimitiveTypeParser.TryRemoveLiteralSuffix("f", ref text))
					{
						targetValue = XmlConvert.ToSingle(text);
						goto IL_02B3;
					}
					targetValue = 0f;
					return false;
				case EdmPrimitiveTypeKind.String:
					targetValue = text;
					goto IL_02B3;
				}
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.UriPrimitiveTypeParser_TryUriStringToPrimitive));
				IL_02B3:
				flag9 = true;
			}
			catch (FormatException)
			{
				targetValue = null;
				flag9 = false;
			}
			catch (OverflowException)
			{
				targetValue = null;
				flag9 = false;
			}
			return flag9;
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000B064 File Offset: 0x00009264
		internal static bool TryUriStringToNonNegativeInteger(string text, out int nonNegativeInteger)
		{
			object obj;
			if (!UriPrimitiveTypeParser.TryUriStringToPrimitive(text, EdmCoreModel.Instance.GetInt32(false), out obj))
			{
				nonNegativeInteger = -1;
				return false;
			}
			nonNegativeInteger = (int)obj;
			return nonNegativeInteger >= 0;
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000B09C File Offset: 0x0000929C
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
				char c = text.get_Chars(num);
				char c2 = text.get_Chars(num + 1);
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

		// Token: 0x06000206 RID: 518 RVA: 0x0000B14C File Offset: 0x0000934C
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

		// Token: 0x06000207 RID: 519 RVA: 0x0000B1B0 File Offset: 0x000093B0
		private static bool TryUriStringToDateTime(string text, out DateTime targetValue)
		{
			if (!UriPrimitiveTypeParser.TryRemoveLiteralPrefix("datetime", ref text))
			{
				targetValue = default(DateTime);
				return false;
			}
			if (!UriPrimitiveTypeParser.TryRemoveQuotes(ref text))
			{
				targetValue = default(DateTime);
				return false;
			}
			bool flag;
			try
			{
				targetValue = PlatformHelper.ConvertStringToDateTime(text);
				flag = true;
			}
			catch (FormatException)
			{
				targetValue = default(DateTime);
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000B214 File Offset: 0x00009414
		private static bool TryUriStringToDateTimeOffset(string text, out DateTimeOffset targetValue)
		{
			if (!UriPrimitiveTypeParser.TryRemoveLiteralPrefix("datetimeoffset", ref text))
			{
				targetValue = default(DateTimeOffset);
				return false;
			}
			if (!UriPrimitiveTypeParser.TryRemoveQuotes(ref text))
			{
				targetValue = default(DateTimeOffset);
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
				targetValue = default(DateTimeOffset);
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000B278 File Offset: 0x00009478
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

		// Token: 0x0600020A RID: 522 RVA: 0x0000B2DC File Offset: 0x000094DC
		private static bool TryUriStringToGeography(string text, out Geography targetValue)
		{
			if (!UriPrimitiveTypeParser.TryRemoveLiteralPrefix("geography", ref text))
			{
				targetValue = null;
				return false;
			}
			if (!UriPrimitiveTypeParser.TryRemoveQuotes(ref text))
			{
				targetValue = null;
				return false;
			}
			bool flag;
			try
			{
				targetValue = LiteralUtils.ParseGeography(text);
				flag = true;
			}
			catch (ParseErrorException)
			{
				targetValue = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000B330 File Offset: 0x00009530
		private static bool TryUriStringToGeometry(string text, out Geometry targetValue)
		{
			if (!UriPrimitiveTypeParser.TryRemoveLiteralPrefix("geometry", ref text))
			{
				targetValue = null;
				return false;
			}
			if (!UriPrimitiveTypeParser.TryRemoveQuotes(ref text))
			{
				targetValue = null;
				return false;
			}
			bool flag;
			try
			{
				targetValue = LiteralUtils.ParseGeometry(text);
				flag = true;
			}
			catch (ParseErrorException)
			{
				targetValue = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000B384 File Offset: 0x00009584
		private static bool TryRemoveQuotes(ref string text)
		{
			if (text.Length < 2)
			{
				return false;
			}
			char c = text.get_Chars(0);
			if (c != '\'' || text.get_Chars(text.Length - 1) != c)
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
				if (text2.Length < num2 + 1 || text2.get_Chars(num2) != c)
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

		// Token: 0x0600020D RID: 525 RVA: 0x0000B40C File Offset: 0x0000960C
		private static bool TryRemoveLiteralSuffix(string suffix, ref string text)
		{
			text = text.Trim(UriPrimitiveTypeParser.WhitespaceChars);
			if (text.Length <= suffix.Length || !text.EndsWith(suffix, 5))
			{
				return false;
			}
			text = text.Substring(0, text.Length - suffix.Length);
			return true;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000B45C File Offset: 0x0000965C
		private static bool TryRemoveLiteralPrefix(string prefix, ref string text)
		{
			if (text.StartsWith(prefix, 5))
			{
				text = text.Remove(0, prefix.Length);
				return true;
			}
			return false;
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000B47C File Offset: 0x0000967C
		private static bool IsUriValueQuoted(string text)
		{
			if (text.Length < 2 || text.get_Chars(0) != '\'' || text.get_Chars(text.Length - 1) != '\'')
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
				if (num == text.Length - 2 || text.get_Chars(num + 1) != '\'')
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000B4F8 File Offset: 0x000096F8
		private static string RemoveQuotes(string text)
		{
			char c = text.get_Chars(0);
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

		// Token: 0x06000211 RID: 529 RVA: 0x0000B53C File Offset: 0x0000973C
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
				goto IL_00B1;
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
					goto IL_00B1;
				}
				break;
			}
			return 10;
			IL_00B1:
			throw new ODataException(Strings.General_InternalError(InternalErrorCodes.UriPrimitiveTypeParser_HexCharToNibble));
		}

		// Token: 0x040001EC RID: 492
		private static char[] WhitespaceChars = new char[] { ' ', '\t', '\n', '\r' };
	}
}
