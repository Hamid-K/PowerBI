using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000043 RID: 67
	internal static class XmlaHttpUtility
	{
		// Token: 0x06000413 RID: 1043 RVA: 0x0001B8F4 File Offset: 0x00019AF4
		public static string AddSessionToUrl(string url, string session)
		{
			UriBuilder uriBuilder = new UriBuilder(url);
			NameValueCollection nameValueCollection = XmlaHttpUtility.ParseQueryString(uriBuilder.Query);
			nameValueCollection["SessionId"] = session;
			uriBuilder.Query = nameValueCollection.ToString();
			return uriBuilder.ToString();
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0001B930 File Offset: 0x00019B30
		public static string ExtractSessionFromUrl(string url, out string session)
		{
			UriBuilder uriBuilder = new UriBuilder(url);
			NameValueCollection nameValueCollection = XmlaHttpUtility.ParseQueryString(uriBuilder.Query);
			session = nameValueCollection.Get("SessionId");
			if (session != null)
			{
				nameValueCollection.Remove("SessionId");
				uriBuilder.Query = nameValueCollection.ToString();
				return uriBuilder.ToString();
			}
			return url;
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0001B980 File Offset: 0x00019B80
		public static NameValueCollection ParseQueryString(string query)
		{
			return XmlaHttpUtility.ParseQueryString(query, Encoding.UTF8);
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0001B98D File Offset: 0x00019B8D
		public static NameValueCollection ParseQueryString(string query, Encoding encoding)
		{
			return XmlaHttpUtility.ParseQueryString(query, encoding, true);
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0001B998 File Offset: 0x00019B98
		public static NameValueCollection ParseQueryString(string query, Encoding encoding, bool urlEncoded)
		{
			if (query == null)
			{
				throw new ArgumentNullException("query");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (query.Length > 0 && query[0] == '?')
			{
				query = query.Substring(1);
			}
			return new XmlaHttpUtility.HttpValueCollection(query, false, urlEncoded, encoding);
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0001B9E7 File Offset: 0x00019BE7
		public static string UrlEncode(string str)
		{
			if (str == null)
			{
				return null;
			}
			return XmlaHttpUtility.UrlEncode(str, Encoding.UTF8);
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0001B9F9 File Offset: 0x00019BF9
		public static string UrlEncode(string str, Encoding e)
		{
			if (str == null)
			{
				return null;
			}
			return Encoding.ASCII.GetString(XmlaHttpUtility.UrlEncodeToBytes(str, e));
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0001BA11 File Offset: 0x00019C11
		public static string UrlEncode(byte[] bytes)
		{
			if (bytes == null)
			{
				return null;
			}
			return Encoding.ASCII.GetString(XmlaHttpUtility.UrlEncodeToBytes(bytes));
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0001BA28 File Offset: 0x00019C28
		public static byte[] UrlEncodeToBytes(string str)
		{
			if (str == null)
			{
				return null;
			}
			return XmlaHttpUtility.UrlEncodeToBytes(str, Encoding.UTF8);
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0001BA3A File Offset: 0x00019C3A
		public static byte[] UrlEncodeToBytes(byte[] bytes)
		{
			if (bytes == null)
			{
				return null;
			}
			return XmlaHttpUtility.UrlEncodeToBytes(bytes, 0, bytes.Length);
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0001BA4C File Offset: 0x00019C4C
		public static byte[] UrlEncodeToBytes(string str, Encoding e)
		{
			if (str == null)
			{
				return null;
			}
			byte[] bytes = e.GetBytes(str);
			return XmlaHttpUtility.UrlEncode(bytes, 0, bytes.Length, false);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0001BA71 File Offset: 0x00019C71
		public static byte[] UrlEncodeToBytes(byte[] bytes, int offset, int count)
		{
			return XmlaHttpUtility.UrlEncode(bytes, offset, count, true);
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0001BA7C File Offset: 0x00019C7C
		public static string UrlPathEncode(string str)
		{
			if (string.IsNullOrEmpty(str))
			{
				return str;
			}
			int num = str.IndexOf('?');
			if (num >= 0)
			{
				return XmlaHttpUtility.UrlPathEncode(str.Substring(0, num)) + str.Substring(num);
			}
			return XmlaHttpUtility.UrlEncodeSpaces(XmlaHttpUtility.UrlEncodeNonAscii(str, Encoding.UTF8));
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0001BACC File Offset: 0x00019CCC
		public static string UrlEncodeUnicode(string value)
		{
			if (value == null)
			{
				return null;
			}
			int length = value.Length;
			StringBuilder stringBuilder = new StringBuilder(length);
			for (int i = 0; i < length; i++)
			{
				char c = value[i];
				if ((c & 'ﾀ') == '\0')
				{
					if (XmlaHttpUtility.IsUrlSafeChar(c))
					{
						stringBuilder.Append(c);
					}
					else if (c == ' ')
					{
						stringBuilder.Append('+');
					}
					else
					{
						stringBuilder.Append('%');
						stringBuilder.Append(XmlaHttpUtility.IntToHex((int)((c >> 4) & '\u000f')));
						stringBuilder.Append(XmlaHttpUtility.IntToHex((int)(c & '\u000f')));
					}
				}
				else
				{
					stringBuilder.Append("%u");
					stringBuilder.Append(XmlaHttpUtility.IntToHex((int)((c >> 12) & '\u000f')));
					stringBuilder.Append(XmlaHttpUtility.IntToHex((int)((c >> 8) & '\u000f')));
					stringBuilder.Append(XmlaHttpUtility.IntToHex((int)((c >> 4) & '\u000f')));
					stringBuilder.Append(XmlaHttpUtility.IntToHex((int)(c & '\u000f')));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0001BBBD File Offset: 0x00019DBD
		public static string UrlDecode(string str)
		{
			if (str == null)
			{
				return null;
			}
			return XmlaHttpUtility.UrlDecode(str, Encoding.UTF8);
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0001BBCF File Offset: 0x00019DCF
		public static string UrlDecode(string str, Encoding e)
		{
			return XmlaHttpUtility.UrlDecodeInternal(str, e);
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0001BBD8 File Offset: 0x00019DD8
		public static string UrlDecode(byte[] bytes, Encoding e)
		{
			if (bytes == null)
			{
				return null;
			}
			return XmlaHttpUtility.UrlDecode(bytes, 0, bytes.Length, e);
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0001BBEA File Offset: 0x00019DEA
		public static string UrlDecode(byte[] bytes, int offset, int count, Encoding e)
		{
			return XmlaHttpUtility.UrlDecodeInternal(bytes, offset, count, e);
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0001BBF5 File Offset: 0x00019DF5
		public static byte[] UrlDecodeToBytes(string str)
		{
			if (str == null)
			{
				return null;
			}
			return XmlaHttpUtility.UrlDecodeToBytes(str, Encoding.UTF8);
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0001BC07 File Offset: 0x00019E07
		public static byte[] UrlDecodeToBytes(string str, Encoding e)
		{
			if (str == null)
			{
				return null;
			}
			return XmlaHttpUtility.UrlDecodeToBytes(e.GetBytes(str));
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0001BC1A File Offset: 0x00019E1A
		public static byte[] UrlDecodeToBytes(byte[] bytes)
		{
			if (bytes == null)
			{
				return null;
			}
			return XmlaHttpUtility.UrlDecodeToBytes(bytes, 0, (bytes != null) ? bytes.Length : 0);
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0001BC31 File Offset: 0x00019E31
		public static byte[] UrlDecodeToBytes(byte[] bytes, int offset, int count)
		{
			return XmlaHttpUtility.UrlDecodeInternal(bytes, offset, count);
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0001BC3C File Offset: 0x00019E3C
		public static string HtmlDecode(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return value;
			}
			if (value.IndexOf('&') < 0)
			{
				return value;
			}
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			XmlaHttpUtility.HtmlDecode(value, stringWriter);
			return stringWriter.ToString();
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0001BC78 File Offset: 0x00019E78
		public static void HtmlDecode(string value, TextWriter output)
		{
			if (value == null)
			{
				return;
			}
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			if (value.IndexOf('&') < 0)
			{
				output.Write(value);
				return;
			}
			int length = value.Length;
			int i = 0;
			while (i < length)
			{
				char c = value[i];
				if (c != '&')
				{
					goto IL_0110;
				}
				int num = value.IndexOfAny(XmlaHttpUtility._htmlEntityEndingChars, i + 1);
				if (num <= 0 || value[num] != ';')
				{
					goto IL_0110;
				}
				string text = value.Substring(i + 1, num - i - 1);
				if (text.Length > 1 && text[0] == '#')
				{
					ushort num2;
					if (text[1] == 'x' || text[1] == 'X')
					{
						ushort.TryParse(text.Substring(2), NumberStyles.AllowHexSpecifier, NumberFormatInfo.InvariantInfo, out num2);
					}
					else
					{
						ushort.TryParse(text.Substring(1), NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out num2);
					}
					if (num2 != 0)
					{
						c = (char)num2;
						i = num;
						goto IL_0110;
					}
					goto IL_0110;
				}
				else
				{
					i = num;
					char c2 = XmlaHttpUtility.HtmlEntities.Lookup(text);
					if (c2 != '\0')
					{
						c = c2;
						goto IL_0110;
					}
					output.Write('&');
					output.Write(text);
					output.Write(';');
				}
				IL_0117:
				i++;
				continue;
				IL_0110:
				output.Write(c);
				goto IL_0117;
			}
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0001BDA7 File Offset: 0x00019FA7
		public static int HexToInt(char h)
		{
			if (h >= '0' && h <= '9')
			{
				return (int)(h - '0');
			}
			if (h >= 'a' && h <= 'f')
			{
				return (int)(h - 'a' + '\n');
			}
			if (h < 'A' || h > 'F')
			{
				return -1;
			}
			return (int)(h - 'A' + '\n');
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0001BDDD File Offset: 0x00019FDD
		public static char IntToHex(int n)
		{
			if (n <= 9)
			{
				return (char)(n + 48);
			}
			return (char)(n - 10 + 97);
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0001BDF4 File Offset: 0x00019FF4
		public static bool IsUrlSafeChar(char ch)
		{
			if ((ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z') || (ch >= '0' && ch <= '9'))
			{
				return true;
			}
			if (ch != '!')
			{
				switch (ch)
				{
				case '(':
				case ')':
				case '*':
				case '-':
				case '.':
					return true;
				case '+':
				case ',':
					break;
				default:
					if (ch == '_')
					{
						return true;
					}
					break;
				}
				return false;
			}
			return true;
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0001BE53 File Offset: 0x0001A053
		private static string UrlEncodeSpaces(string str)
		{
			if (str != null && str.IndexOf(' ') >= 0)
			{
				str = str.Replace(" ", "%20");
			}
			return str;
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0001BE78 File Offset: 0x0001A078
		private static bool ValidateUrlEncodingParameters(byte[] bytes, int offset, int count)
		{
			if (bytes == null && count == 0)
			{
				return false;
			}
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			if (offset < 0 || offset > bytes.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || offset + count > bytes.Length)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			return true;
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0001BEC8 File Offset: 0x0001A0C8
		private static bool IsNonAsciiByte(byte b)
		{
			return b >= 127 || b < 32;
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0001BED8 File Offset: 0x0001A0D8
		private static byte[] UrlEncode(byte[] bytes, int offset, int count, bool alwaysCreateNewReturnValue)
		{
			byte[] array = XmlaHttpUtility.UrlEncode(bytes, offset, count);
			if (!alwaysCreateNewReturnValue || array == null || array != bytes)
			{
				return array;
			}
			return (byte[])array.Clone();
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0001BF08 File Offset: 0x0001A108
		private static byte[] UrlEncode(byte[] bytes, int offset, int count)
		{
			if (!XmlaHttpUtility.ValidateUrlEncodingParameters(bytes, offset, count))
			{
				return null;
			}
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < count; i++)
			{
				char c = (char)bytes[offset + i];
				if (c == ' ')
				{
					num++;
				}
				else if (!XmlaHttpUtility.IsUrlSafeChar(c))
				{
					num2++;
				}
			}
			if (num == 0 && num2 == 0)
			{
				return bytes;
			}
			byte[] array = new byte[count + num2 * 2];
			int num3 = 0;
			for (int j = 0; j < count; j++)
			{
				byte b = bytes[offset + j];
				char c2 = (char)b;
				if (XmlaHttpUtility.IsUrlSafeChar(c2))
				{
					array[num3++] = b;
				}
				else if (c2 == ' ')
				{
					array[num3++] = 43;
				}
				else
				{
					array[num3++] = 37;
					array[num3++] = (byte)XmlaHttpUtility.IntToHex((b >> 4) & 15);
					array[num3++] = (byte)XmlaHttpUtility.IntToHex((int)(b & 15));
				}
			}
			return array;
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0001BFDC File Offset: 0x0001A1DC
		private static string UrlEncodeNonAscii(string str, Encoding e)
		{
			if (string.IsNullOrEmpty(str))
			{
				return str;
			}
			if (e == null)
			{
				e = Encoding.UTF8;
			}
			byte[] bytes = e.GetBytes(str);
			byte[] array = XmlaHttpUtility.UrlEncodeNonAscii(bytes, 0, bytes.Length, false);
			return Encoding.ASCII.GetString(array);
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0001C01C File Offset: 0x0001A21C
		private static byte[] UrlEncodeNonAscii(byte[] bytes, int offset, int count, bool alwaysCreateNewReturnValue)
		{
			if (!XmlaHttpUtility.ValidateUrlEncodingParameters(bytes, offset, count))
			{
				return null;
			}
			int num = 0;
			for (int i = 0; i < count; i++)
			{
				if (XmlaHttpUtility.IsNonAsciiByte(bytes[offset + i]))
				{
					num++;
				}
			}
			if (!alwaysCreateNewReturnValue && num == 0)
			{
				return bytes;
			}
			byte[] array = new byte[count + num * 2];
			int num2 = 0;
			for (int j = 0; j < count; j++)
			{
				byte b = bytes[offset + j];
				if (XmlaHttpUtility.IsNonAsciiByte(b))
				{
					array[num2++] = 37;
					array[num2++] = (byte)XmlaHttpUtility.IntToHex((b >> 4) & 15);
					array[num2++] = (byte)XmlaHttpUtility.IntToHex((int)(b & 15));
				}
				else
				{
					array[num2++] = b;
				}
			}
			return array;
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0001C0C4 File Offset: 0x0001A2C4
		private static string UrlDecodeInternal(string value, Encoding encoding)
		{
			if (value == null)
			{
				return null;
			}
			int length = value.Length;
			XmlaHttpUtility.UrlDecoder urlDecoder = new XmlaHttpUtility.UrlDecoder(length, encoding);
			int i = 0;
			while (i < length)
			{
				char c = value[i];
				if (c == '+')
				{
					c = ' ';
					goto IL_010B;
				}
				if (c != '%' || i >= length - 2)
				{
					goto IL_010B;
				}
				if (value[i + 1] == 'u' && i < length - 5)
				{
					int num = XmlaHttpUtility.HexToInt(value[i + 2]);
					int num2 = XmlaHttpUtility.HexToInt(value[i + 3]);
					int num3 = XmlaHttpUtility.HexToInt(value[i + 4]);
					int num4 = XmlaHttpUtility.HexToInt(value[i + 5]);
					if (num < 0 || num2 < 0 || num3 < 0 || num4 < 0)
					{
						goto IL_010B;
					}
					c = (char)((num << 12) | (num2 << 8) | (num3 << 4) | num4);
					i += 5;
					urlDecoder.AddChar(c);
				}
				else
				{
					int num5 = XmlaHttpUtility.HexToInt(value[i + 1]);
					int num6 = XmlaHttpUtility.HexToInt(value[i + 2]);
					if (num5 < 0 || num6 < 0)
					{
						goto IL_010B;
					}
					byte b = (byte)((num5 << 4) | num6);
					i += 2;
					urlDecoder.AddByte(b);
				}
				IL_0125:
				i++;
				continue;
				IL_010B:
				if ((c & 'ﾀ') == '\0')
				{
					urlDecoder.AddByte((byte)c);
					goto IL_0125;
				}
				urlDecoder.AddChar(c);
				goto IL_0125;
			}
			return urlDecoder.GetString();
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0001C208 File Offset: 0x0001A408
		private static byte[] UrlDecodeInternal(byte[] bytes, int offset, int count)
		{
			if (!XmlaHttpUtility.ValidateUrlEncodingParameters(bytes, offset, count))
			{
				return null;
			}
			int num = 0;
			byte[] array = new byte[count];
			for (int i = 0; i < count; i++)
			{
				int num2 = offset + i;
				byte b = bytes[num2];
				if (b == 43)
				{
					b = 32;
				}
				else if (b == 37 && i < count - 2)
				{
					int num3 = XmlaHttpUtility.HexToInt((char)bytes[num2 + 1]);
					int num4 = XmlaHttpUtility.HexToInt((char)bytes[num2 + 2]);
					if (num3 >= 0 && num4 >= 0)
					{
						b = (byte)((num3 << 4) | num4);
						i += 2;
					}
				}
				array[num++] = b;
			}
			if (num < array.Length)
			{
				byte[] array2 = new byte[num];
				Array.Copy(array, array2, num);
				array = array2;
			}
			return array;
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0001C2AC File Offset: 0x0001A4AC
		private static string UrlDecodeInternal(byte[] bytes, int offset, int count, Encoding encoding)
		{
			if (!XmlaHttpUtility.ValidateUrlEncodingParameters(bytes, offset, count))
			{
				return null;
			}
			XmlaHttpUtility.UrlDecoder urlDecoder = new XmlaHttpUtility.UrlDecoder(count, encoding);
			int i = 0;
			while (i < count)
			{
				int num = offset + i;
				byte b = bytes[num];
				if (b == 43)
				{
					b = 32;
					goto IL_00E6;
				}
				if (b != 37 || i >= count - 2)
				{
					goto IL_00E6;
				}
				if (bytes[num + 1] == 117 && i < count - 5)
				{
					int num2 = XmlaHttpUtility.HexToInt((char)bytes[num + 2]);
					int num3 = XmlaHttpUtility.HexToInt((char)bytes[num + 3]);
					int num4 = XmlaHttpUtility.HexToInt((char)bytes[num + 4]);
					int num5 = XmlaHttpUtility.HexToInt((char)bytes[num + 5]);
					if (num2 < 0 || num3 < 0 || num4 < 0 || num5 < 0)
					{
						goto IL_00E6;
					}
					char c = (char)((num2 << 12) | (num3 << 8) | (num4 << 4) | num5);
					i += 5;
					urlDecoder.AddChar(c);
				}
				else
				{
					int num6 = XmlaHttpUtility.HexToInt((char)bytes[num + 1]);
					int num7 = XmlaHttpUtility.HexToInt((char)bytes[num + 2]);
					if (num6 >= 0 && num7 >= 0)
					{
						b = (byte)((num6 << 4) | num7);
						i += 2;
						goto IL_00E6;
					}
					goto IL_00E6;
				}
				IL_00ED:
				i++;
				continue;
				IL_00E6:
				urlDecoder.AddByte(b);
				goto IL_00ED;
			}
			return urlDecoder.GetString();
		}

		// Token: 0x040003A2 RID: 930
		private const string SharePointSessionId = "SessionId";

		// Token: 0x040003A3 RID: 931
		private static readonly char[] _htmlEntityEndingChars = new char[] { ';', '&' };

		// Token: 0x02000196 RID: 406
		private class UrlDecoder
		{
			// Token: 0x06001247 RID: 4679 RVA: 0x0003FB1F File Offset: 0x0003DD1F
			internal UrlDecoder(int bufferSize, Encoding encoding)
			{
				this._bufferSize = bufferSize;
				this._encoding = encoding;
				this._charBuffer = new char[bufferSize];
			}

			// Token: 0x06001248 RID: 4680 RVA: 0x0003FB44 File Offset: 0x0003DD44
			internal void AddChar(char ch)
			{
				if (this._numBytes > 0)
				{
					this.FlushBytes();
				}
				char[] charBuffer = this._charBuffer;
				int numChars = this._numChars;
				this._numChars = numChars + 1;
				charBuffer[numChars] = ch;
			}

			// Token: 0x06001249 RID: 4681 RVA: 0x0003FB7C File Offset: 0x0003DD7C
			internal void AddByte(byte b)
			{
				if (this._byteBuffer == null)
				{
					this._byteBuffer = new byte[this._bufferSize];
				}
				byte[] byteBuffer = this._byteBuffer;
				int numBytes = this._numBytes;
				this._numBytes = numBytes + 1;
				byteBuffer[numBytes] = b;
			}

			// Token: 0x0600124A RID: 4682 RVA: 0x0003FBBB File Offset: 0x0003DDBB
			internal string GetString()
			{
				if (this._numBytes > 0)
				{
					this.FlushBytes();
				}
				if (this._numChars > 0)
				{
					return new string(this._charBuffer, 0, this._numChars);
				}
				return string.Empty;
			}

			// Token: 0x0600124B RID: 4683 RVA: 0x0003FBF0 File Offset: 0x0003DDF0
			private void FlushBytes()
			{
				if (this._numBytes > 0)
				{
					this._numChars += this._encoding.GetChars(this._byteBuffer, 0, this._numBytes, this._charBuffer, this._numChars);
					this._numBytes = 0;
				}
			}

			// Token: 0x04000C7A RID: 3194
			private int _bufferSize;

			// Token: 0x04000C7B RID: 3195
			private int _numChars;

			// Token: 0x04000C7C RID: 3196
			private char[] _charBuffer;

			// Token: 0x04000C7D RID: 3197
			private int _numBytes;

			// Token: 0x04000C7E RID: 3198
			private byte[] _byteBuffer;

			// Token: 0x04000C7F RID: 3199
			private Encoding _encoding;
		}

		// Token: 0x02000197 RID: 407
		[Serializable]
		private class HttpValueCollection : NameValueCollection
		{
			// Token: 0x0600124C RID: 4684 RVA: 0x0003FC3E File Offset: 0x0003DE3E
			internal HttpValueCollection()
				: base(StringComparer.OrdinalIgnoreCase)
			{
			}

			// Token: 0x0600124D RID: 4685 RVA: 0x0003FC4B File Offset: 0x0003DE4B
			internal HttpValueCollection(string str, bool readOnly, bool urlencoded, Encoding encoding)
				: base(StringComparer.OrdinalIgnoreCase)
			{
				if (!string.IsNullOrEmpty(str))
				{
					this.FillFromString(str, urlencoded, encoding);
				}
				base.IsReadOnly = readOnly;
			}

			// Token: 0x0600124E RID: 4686 RVA: 0x0003FC71 File Offset: 0x0003DE71
			internal HttpValueCollection(int capacity)
				: base(capacity, StringComparer.OrdinalIgnoreCase)
			{
			}

			// Token: 0x0600124F RID: 4687 RVA: 0x0003FC7F File Offset: 0x0003DE7F
			protected HttpValueCollection(SerializationInfo info, StreamingContext context)
				: base(info, context)
			{
			}

			// Token: 0x06001250 RID: 4688 RVA: 0x0003FC89 File Offset: 0x0003DE89
			public override string ToString()
			{
				return this.ToString(true);
			}

			// Token: 0x06001251 RID: 4689 RVA: 0x0003FC92 File Offset: 0x0003DE92
			internal void FillFromString(string s)
			{
				this.FillFromString(s, false, null);
			}

			// Token: 0x06001252 RID: 4690 RVA: 0x0003FCA0 File Offset: 0x0003DEA0
			internal void FillFromString(string s, bool urlencoded, Encoding encoding)
			{
				int num = ((s != null) ? s.Length : 0);
				for (int i = 0; i < num; i++)
				{
					int num2 = i;
					int num3 = -1;
					while (i < num)
					{
						char c = s[i];
						if (c == '=')
						{
							if (num3 < 0)
							{
								num3 = i;
							}
						}
						else if (c == '&')
						{
							break;
						}
						i++;
					}
					string text = null;
					string text2;
					if (num3 >= 0)
					{
						text = s.Substring(num2, num3 - num2);
						text2 = s.Substring(num3 + 1, i - num3 - 1);
					}
					else
					{
						text2 = s.Substring(num2, i - num2);
					}
					if (urlencoded)
					{
						base.Add(XmlaHttpUtility.UrlDecode(text, encoding), XmlaHttpUtility.UrlDecode(text2, encoding));
					}
					else
					{
						base.Add(text, text2);
					}
					if (i == num - 1 && s[i] == '&')
					{
						base.Add(null, string.Empty);
					}
				}
			}

			// Token: 0x06001253 RID: 4691 RVA: 0x0003FD70 File Offset: 0x0003DF70
			internal void FillFromEncodedBytes(byte[] bytes, Encoding encoding)
			{
				int num = ((bytes != null) ? bytes.Length : 0);
				for (int i = 0; i < num; i++)
				{
					int num2 = i;
					int num3 = -1;
					while (i < num)
					{
						byte b = bytes[i];
						if (b == 61)
						{
							if (num3 < 0)
							{
								num3 = i;
							}
						}
						else if (b == 38)
						{
							break;
						}
						i++;
					}
					string text;
					string text2;
					if (num3 >= 0)
					{
						text = XmlaHttpUtility.UrlDecode(bytes, num2, num3 - num2, encoding);
						text2 = XmlaHttpUtility.UrlDecode(bytes, num3 + 1, i - num3 - 1, encoding);
					}
					else
					{
						text = null;
						text2 = XmlaHttpUtility.UrlDecode(bytes, num2, i - num2, encoding);
					}
					base.Add(text, text2);
					if (i == num - 1 && bytes[i] == 38)
					{
						base.Add(null, string.Empty);
					}
				}
			}

			// Token: 0x06001254 RID: 4692 RVA: 0x0003FE17 File Offset: 0x0003E017
			internal void MakeReadOnly()
			{
				base.IsReadOnly = true;
			}

			// Token: 0x06001255 RID: 4693 RVA: 0x0003FE20 File Offset: 0x0003E020
			internal void MakeReadWrite()
			{
				base.IsReadOnly = false;
			}

			// Token: 0x06001256 RID: 4694 RVA: 0x0003FE29 File Offset: 0x0003E029
			internal void Reset()
			{
				base.Clear();
			}

			// Token: 0x06001257 RID: 4695 RVA: 0x0003FE31 File Offset: 0x0003E031
			internal virtual string ToString(bool urlencoded)
			{
				return this.ToString(urlencoded, null);
			}

			// Token: 0x06001258 RID: 4696 RVA: 0x0003FE3C File Offset: 0x0003E03C
			internal virtual string ToString(bool urlencoded, IDictionary excludeKeys)
			{
				int count = this.Count;
				if (count == 0)
				{
					return string.Empty;
				}
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < count; i++)
				{
					string text = this.GetKey(i);
					if (excludeKeys == null || text == null || excludeKeys[text] == null)
					{
						if (urlencoded)
						{
							text = XmlaHttpUtility.UrlEncodeUnicode(text);
						}
						string text2 = ((text != null) ? (text + "=") : string.Empty);
						ArrayList arrayList = (ArrayList)base.BaseGet(i);
						int num = ((arrayList != null) ? arrayList.Count : 0);
						if (stringBuilder.Length > 0)
						{
							stringBuilder.Append('&');
						}
						if (num == 1)
						{
							stringBuilder.Append(text2);
							string text3 = (string)arrayList[0];
							if (urlencoded)
							{
								text3 = XmlaHttpUtility.UrlEncodeUnicode(text3);
							}
							stringBuilder.Append(text3);
						}
						else if (num == 0)
						{
							stringBuilder.Append(text2);
						}
						else
						{
							for (int j = 0; j < num; j++)
							{
								if (j > 0)
								{
									stringBuilder.Append('&');
								}
								stringBuilder.Append(text2);
								string text3 = (string)arrayList[j];
								if (urlencoded)
								{
									text3 = XmlaHttpUtility.UrlEncodeUnicode(text3);
								}
								stringBuilder.Append(text3);
							}
						}
					}
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x02000198 RID: 408
		private static class HtmlEntities
		{
			// Token: 0x06001259 RID: 4697 RVA: 0x0003FF74 File Offset: 0x0003E174
			public static char Lookup(string entity)
			{
				char c;
				XmlaHttpUtility.HtmlEntities._lookupTable.TryGetValue(entity, out c);
				return c;
			}

			// Token: 0x0600125A RID: 4698 RVA: 0x0003FF90 File Offset: 0x0003E190
			private static Dictionary<string, char> GenerateLookupTable()
			{
				Dictionary<string, char> dictionary = new Dictionary<string, char>(StringComparer.Ordinal);
				foreach (string text in XmlaHttpUtility.HtmlEntities._entitiesList)
				{
					dictionary.Add(text.Substring(2), text[0]);
				}
				return dictionary;
			}

			// Token: 0x04000C80 RID: 3200
			private static string[] _entitiesList = new string[]
			{
				"\"-quot", "&-amp", "'-apos", "<-lt", ">-gt", "\u00a0-nbsp", "¡-iexcl", "¢-cent", "£-pound", "¤-curren",
				"¥-yen", "¦-brvbar", "§-sect", "\u00a8-uml", "©-copy", "ª-ordf", "«-laquo", "¬-not", "\u00ad-shy", "®-reg",
				"\u00af-macr", "°-deg", "±-plusmn", "²-sup2", "³-sup3", "\u00b4-acute", "µ-micro", "¶-para", "·-middot", "\u00b8-cedil",
				"¹-sup1", "º-ordm", "»-raquo", "¼-frac14", "½-frac12", "¾-frac34", "¿-iquest", "À-Agrave", "Á-Aacute", "Â-Acirc",
				"Ã-Atilde", "Ä-Auml", "Å-Aring", "Æ-AElig", "Ç-Ccedil", "È-Egrave", "É-Eacute", "Ê-Ecirc", "Ë-Euml", "Ì-Igrave",
				"Í-Iacute", "Î-Icirc", "Ï-Iuml", "Ð-ETH", "Ñ-Ntilde", "Ò-Ograve", "Ó-Oacute", "Ô-Ocirc", "Õ-Otilde", "Ö-Ouml",
				"×-times", "Ø-Oslash", "Ù-Ugrave", "Ú-Uacute", "Û-Ucirc", "Ü-Uuml", "Ý-Yacute", "Þ-THORN", "ß-szlig", "à-agrave",
				"á-aacute", "â-acirc", "ã-atilde", "ä-auml", "å-aring", "æ-aelig", "ç-ccedil", "è-egrave", "é-eacute", "ê-ecirc",
				"ë-euml", "ì-igrave", "í-iacute", "î-icirc", "ï-iuml", "ð-eth", "ñ-ntilde", "ò-ograve", "ó-oacute", "ô-ocirc",
				"õ-otilde", "ö-ouml", "÷-divide", "ø-oslash", "ù-ugrave", "ú-uacute", "û-ucirc", "ü-uuml", "ý-yacute", "þ-thorn",
				"ÿ-yuml", "Œ-OElig", "œ-oelig", "Š-Scaron", "š-scaron", "Ÿ-Yuml", "ƒ-fnof", "ˆ-circ", "\u02dc-tilde", "Α-Alpha",
				"Β-Beta", "Γ-Gamma", "Δ-Delta", "Ε-Epsilon", "Ζ-Zeta", "Η-Eta", "Θ-Theta", "Ι-Iota", "Κ-Kappa", "Λ-Lambda",
				"Μ-Mu", "Ν-Nu", "Ξ-Xi", "Ο-Omicron", "Π-Pi", "Ρ-Rho", "Σ-Sigma", "Τ-Tau", "Υ-Upsilon", "Φ-Phi",
				"Χ-Chi", "Ψ-Psi", "Ω-Omega", "α-alpha", "β-beta", "γ-gamma", "δ-delta", "ε-epsilon", "ζ-zeta", "η-eta",
				"θ-theta", "ι-iota", "κ-kappa", "λ-lambda", "μ-mu", "ν-nu", "ξ-xi", "ο-omicron", "π-pi", "ρ-rho",
				"ς-sigmaf", "σ-sigma", "τ-tau", "υ-upsilon", "φ-phi", "χ-chi", "ψ-psi", "ω-omega", "ϑ-thetasym", "ϒ-upsih",
				"ϖ-piv", "\u2002-ensp", "\u2003-emsp", "\u2009-thinsp", "\u200c-zwnj", "\u200d-zwj", "\u200e-lrm", "\u200f-rlm", "–-ndash", "—-mdash",
				"‘-lsquo", "’-rsquo", "‚-sbquo", "“-ldquo", "”-rdquo", "„-bdquo", "†-dagger", "‡-Dagger", "•-bull", "…-hellip",
				"‰-permil", "′-prime", "″-Prime", "‹-lsaquo", "›-rsaquo", "‾-oline", "⁄-frasl", "€-euro", "ℑ-image", "℘-weierp",
				"ℜ-real", "™-trade", "ℵ-alefsym", "←-larr", "↑-uarr", "→-rarr", "↓-darr", "↔-harr", "↵-crarr", "⇐-lArr",
				"⇑-uArr", "⇒-rArr", "⇓-dArr", "⇔-hArr", "∀-forall", "∂-part", "∃-exist", "∅-empty", "∇-nabla", "∈-isin",
				"∉-notin", "∋-ni", "∏-prod", "∑-sum", "−-minus", "∗-lowast", "√-radic", "∝-prop", "∞-infin", "∠-ang",
				"∧-and", "∨-or", "∩-cap", "∪-cup", "∫-int", "∴-there4", "∼-sim", "≅-cong", "≈-asymp", "≠-ne",
				"≡-equiv", "≤-le", "≥-ge", "⊂-sub", "⊃-sup", "⊄-nsub", "⊆-sube", "⊇-supe", "⊕-oplus", "⊗-otimes",
				"⊥-perp", "⋅-sdot", "⌈-lceil", "⌉-rceil", "⌊-lfloor", "⌋-rfloor", "〈-lang", "〉-rang", "◊-loz", "♠-spades",
				"♣-clubs", "♥-hearts", "♦-diams"
			};

			// Token: 0x04000C81 RID: 3201
			private static Dictionary<string, char> _lookupTable = XmlaHttpUtility.HtmlEntities.GenerateLookupTable();
		}
	}
}
