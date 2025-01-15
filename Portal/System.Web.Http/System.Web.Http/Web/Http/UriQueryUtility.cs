using System;
using System.Text;

namespace System.Web.Http
{
	// Token: 0x02000011 RID: 17
	internal static class UriQueryUtility
	{
		// Token: 0x0600007D RID: 125 RVA: 0x00003720 File Offset: 0x00001920
		public static string UrlEncode(string str)
		{
			if (str == null)
			{
				return null;
			}
			byte[] bytes = Encoding.UTF8.GetBytes(str);
			return Encoding.ASCII.GetString(UriQueryUtility.UrlEncode(bytes, 0, bytes.Length, false));
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003753 File Offset: 0x00001953
		public static string UrlDecode(string str)
		{
			if (str == null)
			{
				return null;
			}
			return UriQueryUtility.UrlDecodeInternal(str, Encoding.UTF8);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003768 File Offset: 0x00001968
		private static byte[] UrlEncode(byte[] bytes, int offset, int count, bool alwaysCreateNewReturnValue)
		{
			byte[] array = UriQueryUtility.UrlEncode(bytes, offset, count);
			if (!alwaysCreateNewReturnValue || array == null || array != bytes)
			{
				return array;
			}
			return (byte[])array.Clone();
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003798 File Offset: 0x00001998
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

		// Token: 0x06000081 RID: 129 RVA: 0x0000386C File Offset: 0x00001A6C
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

		// Token: 0x06000082 RID: 130 RVA: 0x0000391B File Offset: 0x00001B1B
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

		// Token: 0x06000083 RID: 131 RVA: 0x00003951 File Offset: 0x00001B51
		private static char IntToHex(int n)
		{
			if (n <= 9)
			{
				return (char)(n + 48);
			}
			return (char)(n - 10 + 97);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003968 File Offset: 0x00001B68
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

		// Token: 0x06000085 RID: 133 RVA: 0x000039C8 File Offset: 0x00001BC8
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

		// Token: 0x02000194 RID: 404
		private class UrlDecoder
		{
			// Token: 0x06000A39 RID: 2617 RVA: 0x0001A798 File Offset: 0x00018998
			private void FlushBytes()
			{
				if (this._numBytes > 0)
				{
					this._numChars += this._encoding.GetChars(this._byteBuffer, 0, this._numBytes, this._charBuffer, this._numChars);
					this._numBytes = 0;
				}
			}

			// Token: 0x06000A3A RID: 2618 RVA: 0x0001A7E6 File Offset: 0x000189E6
			internal UrlDecoder(int bufferSize, Encoding encoding)
			{
				this._bufferSize = bufferSize;
				this._encoding = encoding;
				this._charBuffer = new char[bufferSize];
			}

			// Token: 0x06000A3B RID: 2619 RVA: 0x0001A808 File Offset: 0x00018A08
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

			// Token: 0x06000A3C RID: 2620 RVA: 0x0001A840 File Offset: 0x00018A40
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

			// Token: 0x06000A3D RID: 2621 RVA: 0x0001A87F File Offset: 0x00018A7F
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

			// Token: 0x040002C4 RID: 708
			private int _bufferSize;

			// Token: 0x040002C5 RID: 709
			private int _numChars;

			// Token: 0x040002C6 RID: 710
			private char[] _charBuffer;

			// Token: 0x040002C7 RID: 711
			private int _numBytes;

			// Token: 0x040002C8 RID: 712
			private byte[] _byteBuffer;

			// Token: 0x040002C9 RID: 713
			private Encoding _encoding;
		}
	}
}
