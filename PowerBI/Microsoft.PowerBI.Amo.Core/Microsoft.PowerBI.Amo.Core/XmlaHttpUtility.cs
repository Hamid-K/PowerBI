using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000060 RID: 96
	internal static class XmlaHttpUtility
	{
		// Token: 0x060004CB RID: 1227 RVA: 0x0001F2AC File Offset: 0x0001D4AC
		public static string AddSessionToUrl(string url, string session)
		{
			UriBuilder uriBuilder = new UriBuilder(url);
			NameValueCollection nameValueCollection = XmlaHttpUtility.ParseQueryString(uriBuilder.Query);
			nameValueCollection["SessionId"] = session;
			uriBuilder.Query = nameValueCollection.ToString();
			return uriBuilder.ToString();
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x0001F2E8 File Offset: 0x0001D4E8
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

		// Token: 0x060004CD RID: 1229 RVA: 0x0001F338 File Offset: 0x0001D538
		public static NameValueCollection ParseQueryString(string query)
		{
			return XmlaHttpUtility.ParseQueryString(query, Encoding.UTF8);
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x0001F345 File Offset: 0x0001D545
		public static NameValueCollection ParseQueryString(string query, Encoding encoding)
		{
			return XmlaHttpUtility.ParseQueryString(query, encoding, true);
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x0001F350 File Offset: 0x0001D550
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

		// Token: 0x060004D0 RID: 1232 RVA: 0x0001F39F File Offset: 0x0001D59F
		public static string UrlEncode(string str)
		{
			if (str == null)
			{
				return null;
			}
			return XmlaHttpUtility.UrlEncode(str, Encoding.UTF8);
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x0001F3B1 File Offset: 0x0001D5B1
		public static string UrlEncode(string str, Encoding e)
		{
			if (str == null)
			{
				return null;
			}
			return Encoding.ASCII.GetString(XmlaHttpUtility.UrlEncodeToBytes(str, e));
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x0001F3C9 File Offset: 0x0001D5C9
		public static string UrlEncode(byte[] bytes)
		{
			if (bytes == null)
			{
				return null;
			}
			return Encoding.ASCII.GetString(XmlaHttpUtility.UrlEncodeToBytes(bytes));
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x0001F3E0 File Offset: 0x0001D5E0
		public static byte[] UrlEncodeToBytes(string str)
		{
			if (str == null)
			{
				return null;
			}
			return XmlaHttpUtility.UrlEncodeToBytes(str, Encoding.UTF8);
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x0001F3F2 File Offset: 0x0001D5F2
		public static byte[] UrlEncodeToBytes(byte[] bytes)
		{
			if (bytes == null)
			{
				return null;
			}
			return XmlaHttpUtility.UrlEncodeToBytes(bytes, 0, bytes.Length);
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x0001F404 File Offset: 0x0001D604
		public static byte[] UrlEncodeToBytes(string str, Encoding e)
		{
			if (str == null)
			{
				return null;
			}
			byte[] bytes = e.GetBytes(str);
			return XmlaHttpUtility.UrlEncode(bytes, 0, bytes.Length, false);
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0001F429 File Offset: 0x0001D629
		public static byte[] UrlEncodeToBytes(byte[] bytes, int offset, int count)
		{
			return XmlaHttpUtility.UrlEncode(bytes, offset, count, true);
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0001F434 File Offset: 0x0001D634
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

		// Token: 0x060004D8 RID: 1240 RVA: 0x0001F484 File Offset: 0x0001D684
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

		// Token: 0x060004D9 RID: 1241 RVA: 0x0001F575 File Offset: 0x0001D775
		public static string UrlDecode(string str)
		{
			if (str == null)
			{
				return null;
			}
			return XmlaHttpUtility.UrlDecode(str, Encoding.UTF8);
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0001F587 File Offset: 0x0001D787
		public static string UrlDecode(string str, Encoding e)
		{
			return XmlaHttpUtility.UrlDecodeInternal(str, e);
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0001F590 File Offset: 0x0001D790
		public static string UrlDecode(byte[] bytes, Encoding e)
		{
			if (bytes == null)
			{
				return null;
			}
			return XmlaHttpUtility.UrlDecode(bytes, 0, bytes.Length, e);
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0001F5A2 File Offset: 0x0001D7A2
		public static string UrlDecode(byte[] bytes, int offset, int count, Encoding e)
		{
			return XmlaHttpUtility.UrlDecodeInternal(bytes, offset, count, e);
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0001F5AD File Offset: 0x0001D7AD
		public static byte[] UrlDecodeToBytes(string str)
		{
			if (str == null)
			{
				return null;
			}
			return XmlaHttpUtility.UrlDecodeToBytes(str, Encoding.UTF8);
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0001F5BF File Offset: 0x0001D7BF
		public static byte[] UrlDecodeToBytes(string str, Encoding e)
		{
			if (str == null)
			{
				return null;
			}
			return XmlaHttpUtility.UrlDecodeToBytes(e.GetBytes(str));
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0001F5D2 File Offset: 0x0001D7D2
		public static byte[] UrlDecodeToBytes(byte[] bytes)
		{
			if (bytes == null)
			{
				return null;
			}
			return XmlaHttpUtility.UrlDecodeToBytes(bytes, 0, (bytes != null) ? bytes.Length : 0);
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0001F5E9 File Offset: 0x0001D7E9
		public static byte[] UrlDecodeToBytes(byte[] bytes, int offset, int count)
		{
			return XmlaHttpUtility.UrlDecodeInternal(bytes, offset, count);
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0001F5F4 File Offset: 0x0001D7F4
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

		// Token: 0x060004E2 RID: 1250 RVA: 0x0001F630 File Offset: 0x0001D830
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

		// Token: 0x060004E3 RID: 1251 RVA: 0x0001F75F File Offset: 0x0001D95F
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

		// Token: 0x060004E4 RID: 1252 RVA: 0x0001F795 File Offset: 0x0001D995
		public static char IntToHex(int n)
		{
			if (n <= 9)
			{
				return (char)(n + 48);
			}
			return (char)(n - 10 + 97);
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x0001F7AC File Offset: 0x0001D9AC
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

		// Token: 0x060004E6 RID: 1254 RVA: 0x0001F80B File Offset: 0x0001DA0B
		private static string UrlEncodeSpaces(string str)
		{
			if (str != null && str.IndexOf(' ') >= 0)
			{
				str = str.Replace(" ", "%20");
			}
			return str;
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0001F830 File Offset: 0x0001DA30
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

		// Token: 0x060004E8 RID: 1256 RVA: 0x0001F880 File Offset: 0x0001DA80
		private static bool IsNonAsciiByte(byte b)
		{
			return b >= 127 || b < 32;
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x0001F890 File Offset: 0x0001DA90
		private static byte[] UrlEncode(byte[] bytes, int offset, int count, bool alwaysCreateNewReturnValue)
		{
			byte[] array = XmlaHttpUtility.UrlEncode(bytes, offset, count);
			if (!alwaysCreateNewReturnValue || array == null || array != bytes)
			{
				return array;
			}
			return (byte[])array.Clone();
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0001F8C0 File Offset: 0x0001DAC0
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

		// Token: 0x060004EB RID: 1259 RVA: 0x0001F994 File Offset: 0x0001DB94
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

		// Token: 0x060004EC RID: 1260 RVA: 0x0001F9D4 File Offset: 0x0001DBD4
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

		// Token: 0x060004ED RID: 1261 RVA: 0x0001FA7C File Offset: 0x0001DC7C
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

		// Token: 0x060004EE RID: 1262 RVA: 0x0001FBC0 File Offset: 0x0001DDC0
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

		// Token: 0x060004EF RID: 1263 RVA: 0x0001FC64 File Offset: 0x0001DE64
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

		// Token: 0x040003D1 RID: 977
		private const string SharePointSessionId = "SessionId";

		// Token: 0x040003D2 RID: 978
		private static readonly char[] _htmlEntityEndingChars = new char[] { ';', '&' };

		// Token: 0x02000192 RID: 402
		private class UrlDecoder
		{
			// Token: 0x060012E3 RID: 4835 RVA: 0x0004208B File Offset: 0x0004028B
			internal UrlDecoder(int bufferSize, Encoding encoding)
			{
				this._bufferSize = bufferSize;
				this._encoding = encoding;
				this._charBuffer = new char[bufferSize];
			}

			// Token: 0x060012E4 RID: 4836 RVA: 0x000420B0 File Offset: 0x000402B0
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

			// Token: 0x060012E5 RID: 4837 RVA: 0x000420E8 File Offset: 0x000402E8
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

			// Token: 0x060012E6 RID: 4838 RVA: 0x00042127 File Offset: 0x00040327
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

			// Token: 0x060012E7 RID: 4839 RVA: 0x0004215C File Offset: 0x0004035C
			private void FlushBytes()
			{
				if (this._numBytes > 0)
				{
					this._numChars += this._encoding.GetChars(this._byteBuffer, 0, this._numBytes, this._charBuffer, this._numChars);
					this._numBytes = 0;
				}
			}

			// Token: 0x04000C37 RID: 3127
			private int _bufferSize;

			// Token: 0x04000C38 RID: 3128
			private int _numChars;

			// Token: 0x04000C39 RID: 3129
			private char[] _charBuffer;

			// Token: 0x04000C3A RID: 3130
			private int _numBytes;

			// Token: 0x04000C3B RID: 3131
			private byte[] _byteBuffer;

			// Token: 0x04000C3C RID: 3132
			private Encoding _encoding;
		}

		// Token: 0x02000193 RID: 403
		[Serializable]
		private class HttpValueCollection : NameValueCollection
		{
			// Token: 0x060012E8 RID: 4840 RVA: 0x000421AA File Offset: 0x000403AA
			internal HttpValueCollection()
				: base(StringComparer.OrdinalIgnoreCase)
			{
			}

			// Token: 0x060012E9 RID: 4841 RVA: 0x000421B7 File Offset: 0x000403B7
			internal HttpValueCollection(string str, bool readOnly, bool urlencoded, Encoding encoding)
				: base(StringComparer.OrdinalIgnoreCase)
			{
				if (!string.IsNullOrEmpty(str))
				{
					this.FillFromString(str, urlencoded, encoding);
				}
				base.IsReadOnly = readOnly;
			}

			// Token: 0x060012EA RID: 4842 RVA: 0x000421DD File Offset: 0x000403DD
			internal HttpValueCollection(int capacity)
				: base(capacity, StringComparer.OrdinalIgnoreCase)
			{
			}

			// Token: 0x060012EB RID: 4843 RVA: 0x000421EB File Offset: 0x000403EB
			protected HttpValueCollection(SerializationInfo info, StreamingContext context)
				: base(info, context)
			{
			}

			// Token: 0x060012EC RID: 4844 RVA: 0x000421F5 File Offset: 0x000403F5
			public override string ToString()
			{
				return this.ToString(true);
			}

			// Token: 0x060012ED RID: 4845 RVA: 0x000421FE File Offset: 0x000403FE
			internal void FillFromString(string s)
			{
				this.FillFromString(s, false, null);
			}

			// Token: 0x060012EE RID: 4846 RVA: 0x0004220C File Offset: 0x0004040C
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

			// Token: 0x060012EF RID: 4847 RVA: 0x000422DC File Offset: 0x000404DC
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

			// Token: 0x060012F0 RID: 4848 RVA: 0x00042383 File Offset: 0x00040583
			internal void MakeReadOnly()
			{
				base.IsReadOnly = true;
			}

			// Token: 0x060012F1 RID: 4849 RVA: 0x0004238C File Offset: 0x0004058C
			internal void MakeReadWrite()
			{
				base.IsReadOnly = false;
			}

			// Token: 0x060012F2 RID: 4850 RVA: 0x00042395 File Offset: 0x00040595
			internal void Reset()
			{
				base.Clear();
			}

			// Token: 0x060012F3 RID: 4851 RVA: 0x0004239D File Offset: 0x0004059D
			internal virtual string ToString(bool urlencoded)
			{
				return this.ToString(urlencoded, null);
			}

			// Token: 0x060012F4 RID: 4852 RVA: 0x000423A8 File Offset: 0x000405A8
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

		// Token: 0x02000194 RID: 404
		private static class HtmlEntities
		{
			// Token: 0x060012F5 RID: 4853 RVA: 0x000424E0 File Offset: 0x000406E0
			public static char Lookup(string entity)
			{
				char c;
				XmlaHttpUtility.HtmlEntities._lookupTable.TryGetValue(entity, out c);
				return c;
			}

			// Token: 0x060012F6 RID: 4854 RVA: 0x000424FC File Offset: 0x000406FC
			private static Dictionary<string, char> GenerateLookupTable()
			{
				Dictionary<string, char> dictionary = new Dictionary<string, char>(StringComparer.Ordinal);
				foreach (string text in XmlaHttpUtility.HtmlEntities._entitiesList)
				{
					dictionary.Add(text.Substring(2), text[0]);
				}
				return dictionary;
			}

			// Token: 0x04000C3D RID: 3133
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

			// Token: 0x04000C3E RID: 3134
			private static Dictionary<string, char> _lookupTable = XmlaHttpUtility.HtmlEntities.GenerateLookupTable();
		}
	}
}
