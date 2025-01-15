using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace Microsoft.Identity.Client.Utils
{
	// Token: 0x020001C0 RID: 448
	internal static class Base64UrlHelpers
	{
		// Token: 0x060013ED RID: 5101 RVA: 0x0004396B File Offset: 0x00041B6B
		public static string Encode(string arg)
		{
			if (arg == null)
			{
				return null;
			}
			return Base64UrlHelpers.Encode(Encoding.UTF8.GetBytes(arg));
		}

		// Token: 0x060013EE RID: 5102 RVA: 0x00043984 File Offset: 0x00041B84
		private static string Encode(byte[] inArray, int offset, int length)
		{
			if (inArray == null)
			{
				throw new ArgumentNullException("inArray");
			}
			if (length == 0)
			{
				return string.Empty;
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			if (offset < 0 || inArray.Length < offset)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (inArray.Length < offset + length)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			int num = length % 3;
			int num2 = offset + (length - num);
			char[] array = new char[(length + 2) / 3 * 4];
			char[] array2 = Base64UrlHelpers.s_base64Table;
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

		// Token: 0x060013EF RID: 5103 RVA: 0x00043AE3 File Offset: 0x00041CE3
		public static string Encode(byte[] inArray)
		{
			if (inArray == null)
			{
				return null;
			}
			return Base64UrlHelpers.Encode(inArray, 0, inArray.Length);
		}

		// Token: 0x060013F0 RID: 5104 RVA: 0x00043AF4 File Offset: 0x00041CF4
		internal static string EncodeString(string str)
		{
			if (str == null)
			{
				return null;
			}
			return Base64UrlHelpers.Encode(Encoding.UTF8.GetBytes(str));
		}

		// Token: 0x060013F1 RID: 5105 RVA: 0x00043B0B File Offset: 0x00041D0B
		public static byte[] DecodeBytes(string str)
		{
			if (str == null)
			{
				return null;
			}
			return Base64UrlHelpers.UnsafeDecode(str);
		}

		// Token: 0x060013F2 RID: 5106 RVA: 0x00043B18 File Offset: 0x00041D18
		private unsafe static byte[] UnsafeDecode(string str)
		{
			int num = str.Length % 4;
			if (num == 1)
			{
				throw new FormatException("Unable to decode: " + str + " as Base64url encoded string.");
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

		// Token: 0x060013F3 RID: 5107 RVA: 0x00043CB7 File Offset: 0x00041EB7
		public static string Decode(string arg)
		{
			return Encoding.UTF8.GetString(Base64UrlHelpers.DecodeBytes(arg));
		}

		// Token: 0x04000835 RID: 2101
		private const char base64PadCharacter = '=';

		// Token: 0x04000836 RID: 2102
		private const char base64Character62 = '+';

		// Token: 0x04000837 RID: 2103
		private const char base64Character63 = '/';

		// Token: 0x04000838 RID: 2104
		private const char base64UrlCharacter62 = '-';

		// Token: 0x04000839 RID: 2105
		private const char base64UrlCharacter63 = '_';

		// Token: 0x0400083A RID: 2106
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
