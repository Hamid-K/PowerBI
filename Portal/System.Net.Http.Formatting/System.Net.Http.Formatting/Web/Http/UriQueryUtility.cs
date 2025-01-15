using System;
using System.Text;

namespace System.Web.Http
{
	// Token: 0x0200005E RID: 94
	internal static class UriQueryUtility
	{
		// Token: 0x06000372 RID: 882 RVA: 0x0000C6F8 File Offset: 0x0000A8F8
		public static string UrlEncode(string str)
		{
			if (str == null)
			{
				return null;
			}
			byte[] bytes = Encoding.UTF8.GetBytes(str);
			return Encoding.ASCII.GetString(UriQueryUtility.UrlEncode(bytes, 0, bytes.Length, false));
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000C72B File Offset: 0x0000A92B
		public static string UrlDecode(string str)
		{
			if (str == null)
			{
				return null;
			}
			return UriQueryUtility.UrlDecodeInternal(str, Encoding.UTF8);
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0000C740 File Offset: 0x0000A940
		private static byte[] UrlEncode(byte[] bytes, int offset, int count, bool alwaysCreateNewReturnValue)
		{
			byte[] array = UriQueryUtility.UrlEncode(bytes, offset, count);
			if (!alwaysCreateNewReturnValue || array == null || array != bytes)
			{
				return array;
			}
			return (byte[])array.Clone();
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0000C770 File Offset: 0x0000A970
		private static byte[] UrlEncode(byte[] bytes, int offset, int count)
		{
			if (!UriQueryUtility.ValidateUrlEncodingParameters(bytes, offset, count))
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
				else if (!UriQueryUtility.IsUrlSafeChar(c))
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
				if (UriQueryUtility.IsUrlSafeChar(c2))
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
					array[num3++] = (byte)UriQueryUtility.IntToHex((b >> 4) & 15);
					array[num3++] = (byte)UriQueryUtility.IntToHex((int)(b & 15));
				}
			}
			return array;
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000C844 File Offset: 0x0000AA44
		private static string UrlDecodeInternal(string value, Encoding encoding)
		{
			if (value == null)
			{
				return null;
			}
			int length = value.Length;
			UriQueryUtility.UrlDecoder urlDecoder = new UriQueryUtility.UrlDecoder(length, encoding);
			int i = 0;
			while (i < length)
			{
				char c = value[i];
				if (c == '+')
				{
					c = ' ';
					goto IL_0077;
				}
				if (c != '%' || i >= length - 2)
				{
					goto IL_0077;
				}
				int num = UriQueryUtility.HexToInt(value[i + 1]);
				int num2 = UriQueryUtility.HexToInt(value[i + 2]);
				if (num < 0 || num2 < 0)
				{
					goto IL_0077;
				}
				byte b = (byte)((num << 4) | num2);
				i += 2;
				urlDecoder.AddByte(b);
				IL_0091:
				i++;
				continue;
				IL_0077:
				if ((c & 'ﾀ') == '\0')
				{
					urlDecoder.AddByte((byte)c);
					goto IL_0091;
				}
				urlDecoder.AddChar(c);
				goto IL_0091;
			}
			return urlDecoder.GetString();
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000C8F3 File Offset: 0x0000AAF3
		private static int HexToInt(char h)
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

		// Token: 0x06000378 RID: 888 RVA: 0x0000C929 File Offset: 0x0000AB29
		private static char IntToHex(int n)
		{
			if (n <= 9)
			{
				return (char)(n + 48);
			}
			return (char)(n - 10 + 97);
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000C940 File Offset: 0x0000AB40
		private static bool IsUrlSafeChar(char ch)
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

		// Token: 0x0600037A RID: 890 RVA: 0x0000C9A0 File Offset: 0x0000ABA0
		private static bool ValidateUrlEncodingParameters(byte[] bytes, int offset, int count)
		{
			if (bytes == null && count == 0)
			{
				return false;
			}
			if (bytes == null)
			{
				throw Error.ArgumentNull("bytes");
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

		// Token: 0x02000092 RID: 146
		private class UrlDecoder
		{
			// Token: 0x06000413 RID: 1043 RVA: 0x0000F310 File Offset: 0x0000D510
			private void FlushBytes()
			{
				if (this._numBytes > 0)
				{
					this._numChars += this._encoding.GetChars(this._byteBuffer, 0, this._numBytes, this._charBuffer, this._numChars);
					this._numBytes = 0;
				}
			}

			// Token: 0x06000414 RID: 1044 RVA: 0x0000F35E File Offset: 0x0000D55E
			internal UrlDecoder(int bufferSize, Encoding encoding)
			{
				this._bufferSize = bufferSize;
				this._encoding = encoding;
				this._charBuffer = new char[bufferSize];
			}

			// Token: 0x06000415 RID: 1045 RVA: 0x0000F380 File Offset: 0x0000D580
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

			// Token: 0x06000416 RID: 1046 RVA: 0x0000F3B8 File Offset: 0x0000D5B8
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

			// Token: 0x06000417 RID: 1047 RVA: 0x0000F3F7 File Offset: 0x0000D5F7
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

			// Token: 0x04000220 RID: 544
			private int _bufferSize;

			// Token: 0x04000221 RID: 545
			private int _numChars;

			// Token: 0x04000222 RID: 546
			private char[] _charBuffer;

			// Token: 0x04000223 RID: 547
			private int _numBytes;

			// Token: 0x04000224 RID: 548
			private byte[] _byteBuffer;

			// Token: 0x04000225 RID: 549
			private Encoding _encoding;
		}
	}
}
