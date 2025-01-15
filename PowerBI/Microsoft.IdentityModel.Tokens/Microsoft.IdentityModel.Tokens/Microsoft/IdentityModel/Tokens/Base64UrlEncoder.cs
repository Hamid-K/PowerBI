using System;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.IdentityModel.Logging;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x0200011E RID: 286
	public static class Base64UrlEncoder
	{
		// Token: 0x06000E3F RID: 3647 RVA: 0x00038721 File Offset: 0x00036921
		public static string Encode(string arg)
		{
			if (arg == null)
			{
				throw LogHelper.LogArgumentNullException("arg");
			}
			return Base64UrlEncoder.Encode(Encoding.UTF8.GetBytes(arg));
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x00038744 File Offset: 0x00036944
		public static string Encode(byte[] inArray, int offset, int length)
		{
			if (inArray == null)
			{
				throw LogHelper.LogArgumentNullException("inArray");
			}
			if (offset < 0)
			{
				throw LogHelper.LogExceptionMessage(new ArgumentOutOfRangeException(LogHelper.FormatInvariant("IDX10716: '{0}' must be greater than 0, was: '{1}'", new object[]
				{
					LogHelper.MarkAsNonPII("offset"),
					LogHelper.MarkAsNonPII(offset)
				})));
			}
			if (length == 0)
			{
				return string.Empty;
			}
			if (length < 0)
			{
				throw LogHelper.LogExceptionMessage(new ArgumentOutOfRangeException(LogHelper.FormatInvariant("IDX10716: '{0}' must be greater than 0, was: '{1}'", new object[]
				{
					LogHelper.MarkAsNonPII("length"),
					LogHelper.MarkAsNonPII(length)
				})));
			}
			if (inArray.Length < offset + length)
			{
				throw LogHelper.LogExceptionMessage(new ArgumentOutOfRangeException(LogHelper.FormatInvariant("IDX10717: '{0} + {1}' must not be greater than {2}, '{3} + {4} > {5}'.", new object[]
				{
					LogHelper.MarkAsNonPII("offset"),
					LogHelper.MarkAsNonPII("length"),
					LogHelper.MarkAsNonPII("inArray"),
					LogHelper.MarkAsNonPII(offset),
					LogHelper.MarkAsNonPII(length),
					LogHelper.MarkAsNonPII(inArray.Length)
				})));
			}
			int num = length % 3;
			int num2 = offset + (length - num);
			char[] array = new char[(length + 2) / 3 * 4];
			char[] array2 = Base64UrlEncoder.s_base64Table;
			int num3 = 0;
			int i;
			for (i = offset; i < num2; i += 3)
			{
				byte b = inArray[i];
				byte b2 = inArray[i + 1];
				byte b3 = inArray[i + 2];
				array[num3] = array2[b >> 2];
				array[num3 + 1] = array2[((int)(b & 3) << 4) | (b2 >> 4)];
				array[num3 + 2] = array2[((int)(b2 & 15) << 2) | (b3 >> 6)];
				array[num3 + 3] = array2[(int)(b3 & 63)];
				num3 += 4;
			}
			i = num2;
			if (num != 1)
			{
				if (num == 2)
				{
					byte b4 = inArray[i];
					byte b5 = inArray[i + 1];
					array[num3] = array2[b4 >> 2];
					array[num3 + 1] = array2[((int)(b4 & 3) << 4) | (b5 >> 4)];
					array[num3 + 2] = array2[(int)(b5 & 15) << 2];
					num3 += 3;
				}
			}
			else
			{
				byte b6 = inArray[i];
				array[num3] = array2[b6 >> 2];
				array[num3 + 1] = array2[(int)(b6 & 3) << 4];
				num3 += 2;
			}
			return new string(array, 0, num3);
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x00038956 File Offset: 0x00036B56
		public static string Encode(byte[] inArray)
		{
			if (inArray == null)
			{
				throw LogHelper.LogArgumentNullException("inArray");
			}
			return Base64UrlEncoder.Encode(inArray, 0, inArray.Length);
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x00038970 File Offset: 0x00036B70
		internal static string EncodeString(string str)
		{
			if (str == null)
			{
				throw LogHelper.LogArgumentNullException("str");
			}
			return Base64UrlEncoder.Encode(Encoding.UTF8.GetBytes(str));
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x00038990 File Offset: 0x00036B90
		public static byte[] DecodeBytes(string str)
		{
			if (str == null)
			{
				throw LogHelper.LogExceptionMessage(new ArgumentNullException("str"));
			}
			return Base64UrlEncoder.UnsafeDecode(str);
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x000389AC File Offset: 0x00036BAC
		internal unsafe static byte[] UnsafeDecode(string str)
		{
			int num = str.Length % 4;
			if (num == 1)
			{
				throw LogHelper.LogExceptionMessage(new FormatException(LogHelper.FormatInvariant("IDX10400: Unable to decode: '{0}' as Base64url encoded string.", new object[] { str })));
			}
			bool flag = false;
			int num2 = str.Length + (4 - num) % 4;
			for (int i = 0; i < str.Length; i++)
			{
				if (str[i] == '-' || str[i] == '_')
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				string text = new string('\0', num2);
				fixed (string text2 = text)
				{
					char* ptr = text2;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					int j;
					for (j = 0; j < str.Length; j++)
					{
						if (str[j] == '-')
						{
							ptr[j] = '+';
						}
						else if (str[j] == '_')
						{
							ptr[j] = '/';
						}
						else
						{
							ptr[j] = str[j];
						}
					}
					while (j < num2)
					{
						ptr[j] = '=';
						j++;
					}
				}
				return Convert.FromBase64String(text);
			}
			if (num2 == str.Length)
			{
				return Convert.FromBase64String(str);
			}
			string text3 = new string('\0', num2);
			fixed (string text2 = str)
			{
				char* ptr2 = text2;
				if (ptr2 != null)
				{
					ptr2 += RuntimeHelpers.OffsetToStringData / 2;
				}
				fixed (string text4 = text3)
				{
					char* ptr3 = text4;
					if (ptr3 != null)
					{
						ptr3 += RuntimeHelpers.OffsetToStringData / 2;
					}
					Buffer.MemoryCopy((void*)ptr2, (void*)ptr3, (long)(str.Length * 2), (long)(str.Length * 2));
					ptr3[str.Length] = '=';
					if (str.Length + 2 == num2)
					{
						ptr3[str.Length + 1] = '=';
					}
				}
			}
			return Convert.FromBase64String(text3);
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x00038B54 File Offset: 0x00036D54
		internal unsafe static byte[] UnsafeDecode(char[] str)
		{
			int num = str.Length % 4;
			if (num == 1)
			{
				throw LogHelper.LogExceptionMessage(new FormatException(LogHelper.FormatInvariant("IDX10400: Unable to decode: '{0}' as Base64url encoded string.", new object[] { str })));
			}
			bool flag = false;
			int num2 = str.Length + (4 - num) % 4;
			for (int i = 0; i < str.Length; i++)
			{
				if (str[i] == '-' || str[i] == '_')
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				string text = new string('\0', num2);
				fixed (string text2 = text)
				{
					char* ptr = text2;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					int j;
					for (j = 0; j < str.Length; j++)
					{
						if (str[j] == '-')
						{
							ptr[j] = '+';
						}
						else if (str[j] == '_')
						{
							ptr[j] = '/';
						}
						else
						{
							ptr[j] = str[j];
						}
					}
					while (j < num2)
					{
						ptr[j] = '=';
						j++;
					}
				}
				return Convert.FromBase64String(text);
			}
			if (num2 == str.Length)
			{
				return Convert.FromBase64CharArray(str, 0, str.Length);
			}
			string text3 = new string('\0', num2);
			fixed (char[] array = str)
			{
				char* ptr2;
				if (str == null || array.Length == 0)
				{
					ptr2 = null;
				}
				else
				{
					ptr2 = &array[0];
				}
				fixed (string text2 = text3)
				{
					char* ptr3 = text2;
					if (ptr3 != null)
					{
						ptr3 += RuntimeHelpers.OffsetToStringData / 2;
					}
					Buffer.MemoryCopy((void*)ptr2, (void*)ptr3, (long)(str.Length * 2), (long)(str.Length * 2));
					ptr3[str.Length] = '=';
					if (str.Length + 2 == num2)
					{
						ptr3[str.Length + 1] = '=';
					}
				}
			}
			return Convert.FromBase64String(text3);
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x00038CD5 File Offset: 0x00036ED5
		public static string Decode(string arg)
		{
			return Encoding.UTF8.GetString(Base64UrlEncoder.DecodeBytes(arg));
		}

		// Token: 0x0400047F RID: 1151
		private const char base64PadCharacter = '=';

		// Token: 0x04000480 RID: 1152
		private const char base64Character62 = '+';

		// Token: 0x04000481 RID: 1153
		private const char base64Character63 = '/';

		// Token: 0x04000482 RID: 1154
		private const char base64UrlCharacter62 = '-';

		// Token: 0x04000483 RID: 1155
		private const char base64UrlCharacter63 = '_';

		// Token: 0x04000484 RID: 1156
		internal static readonly char[] s_base64Table = new char[]
		{
			'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
			'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T',
			'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd',
			'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n',
			'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x',
			'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7',
			'8', '9', '-', '_'
		};
	}
}
