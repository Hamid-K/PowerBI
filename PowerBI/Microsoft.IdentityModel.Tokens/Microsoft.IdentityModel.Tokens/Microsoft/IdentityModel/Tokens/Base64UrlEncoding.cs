using System;
using System.Buffers;
using Microsoft.IdentityModel.Logging;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x0200011F RID: 287
	internal static class Base64UrlEncoding
	{
		// Token: 0x06000E48 RID: 3656 RVA: 0x00038D00 File Offset: 0x00036F00
		public static byte[] Decode(string inputString)
		{
			if (inputString == null)
			{
				throw LogHelper.LogArgumentNullException("inputString");
			}
			return Base64UrlEncoding.Decode(inputString, 0, inputString.Length);
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x00038D20 File Offset: 0x00036F20
		public static byte[] Decode(string input, int offset, int length)
		{
			if (input == null)
			{
				throw LogHelper.LogArgumentNullException("input");
			}
			byte[] array = new byte[Base64UrlEncoding.ValidateAndGetOutputSize(input, offset, length)];
			Base64UrlEncoding.Decode(input, offset, length, array);
			return array;
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x00038D54 File Offset: 0x00036F54
		public static T Decode<T, TX>(string input, int offset, int length, TX argx, Func<byte[], int, TX, T> action)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			int num = Base64UrlEncoding.ValidateAndGetOutputSize(input, offset, length);
			byte[] array = ArrayPool<byte>.Shared.Rent(num);
			T t;
			try
			{
				Base64UrlEncoding.Decode(input, offset, length, array);
				t = action(array, num, argx);
			}
			finally
			{
				ArrayPool<byte>.Shared.Return(array, false);
			}
			return t;
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x00038DBC File Offset: 0x00036FBC
		public static T Decode<T>(string input, int offset, int length, Func<byte[], int, T> action)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			int num = Base64UrlEncoding.ValidateAndGetOutputSize(input, offset, length);
			byte[] array = ArrayPool<byte>.Shared.Rent(num);
			T t;
			try
			{
				Base64UrlEncoding.Decode(input, offset, length, array);
				t = action(array, num);
			}
			finally
			{
				ArrayPool<byte>.Shared.Return(array, false);
			}
			return t;
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x00038E20 File Offset: 0x00037020
		public static T Decode<T, TX, TY, TZ>(string input, int offset, int length, TX argx, TY argy, TZ argz, Func<byte[], int, TX, TY, TZ, T> action)
		{
			if (action == null)
			{
				throw LogHelper.LogArgumentNullException("action");
			}
			int num = Base64UrlEncoding.ValidateAndGetOutputSize(input, offset, length);
			byte[] array = ArrayPool<byte>.Shared.Rent(num);
			T t;
			try
			{
				Base64UrlEncoding.Decode(input, offset, length, array);
				t = action(array, num, argx, argy, argz);
			}
			finally
			{
				ArrayPool<byte>.Shared.Return(array, false);
			}
			return t;
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x00038E8C File Offset: 0x0003708C
		private static void Decode(string input, int offset, int length, byte[] output)
		{
			int num = 0;
			uint num2 = 255U;
			int i = offset;
			while (i < offset + length)
			{
				uint num3 = (uint)input[i];
				if (num3 >= 65U && num3 <= 90U)
				{
					num3 -= 65U;
					goto IL_00A0;
				}
				if (num3 >= 97U && num3 <= 122U)
				{
					num3 = num3 - 97U + 26U;
					goto IL_00A0;
				}
				if (num3 >= 48U && num3 <= 57U)
				{
					num3 = num3 - 48U + 52U;
					goto IL_00A0;
				}
				if (num3 == 43U || num3 == 45U)
				{
					num3 = 62U;
					goto IL_00A0;
				}
				if (num3 == 47U || num3 == 95U)
				{
					num3 = 63U;
					goto IL_00A0;
				}
				if (num3 != 61U)
				{
					throw LogHelper.LogExceptionMessage(new ArgumentOutOfRangeException(LogHelper.FormatInvariant("IDX10820: Invalid character found in Base64UrlEncoding. Character: '{0}', Encoding: '{1}'.", new object[]
					{
						LogHelper.MarkAsNonPII(num3),
						input
					})));
				}
				IL_00DA:
				i++;
				continue;
				IL_00A0:
				num2 = (num2 << 6) | num3;
				if ((4278190080U & num2) == 4278190080U)
				{
					output[num++] = (byte)(num2 >> 16);
					output[num++] = (byte)(num2 >> 8);
					output[num++] = (byte)num2;
					num2 = 255U;
					goto IL_00DA;
				}
				goto IL_00DA;
			}
			if (num2 == 255U)
			{
				return;
			}
			if ((66846720U & num2) == 66846720U)
			{
				num2 <<= 6;
				output[num++] = (byte)(num2 >> 16);
				output[num++] = (byte)(num2 >> 8);
				return;
			}
			if ((1044480U & num2) == 1044480U)
			{
				num2 <<= 12;
				output[num++] = (byte)(num2 >> 16);
				return;
			}
			throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX10821: Incorrect padding detected in Base64UrlEncoding. Encoding: '{0}'.", new object[] { input })));
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x00038FF1 File Offset: 0x000371F1
		public static string Encode(byte[] bytes)
		{
			if (bytes == null)
			{
				throw LogHelper.LogArgumentNullException("bytes");
			}
			return Base64UrlEncoding.Encode(bytes, 0, bytes.Length);
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x0003900C File Offset: 0x0003720C
		public static string Encode(byte[] input, int offset, int length)
		{
			if (input == null)
			{
				throw LogHelper.LogArgumentNullException("input");
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
			if (offset < 0)
			{
				throw LogHelper.LogExceptionMessage(new ArgumentOutOfRangeException(LogHelper.FormatInvariant("IDX10716: '{0}' must be greater than 0, was: '{1}'", new object[]
				{
					LogHelper.MarkAsNonPII("offset"),
					LogHelper.MarkAsNonPII(offset)
				})));
			}
			if (input.Length < offset + length)
			{
				throw LogHelper.LogExceptionMessage(new ArgumentOutOfRangeException(LogHelper.FormatInvariant("IDX10717: '{0} + {1}' must not be greater than {2}, '{3} + {4} > {5}'.", new object[]
				{
					LogHelper.MarkAsNonPII("offset"),
					LogHelper.MarkAsNonPII("length"),
					LogHelper.MarkAsNonPII("input"),
					LogHelper.MarkAsNonPII(offset),
					LogHelper.MarkAsNonPII(length),
					LogHelper.MarkAsNonPII(input.Length)
				})));
			}
			int num = length % 3;
			if (num > 0)
			{
				num++;
			}
			num += length / 3 * 4;
			char[] array = new char[num];
			Base64UrlEncoding.WriteEncodedOutput(input, offset, length, array);
			return new string(array);
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x0003914C File Offset: 0x0003734C
		private static int ValidateAndGetOutputSize(string inputString, int offset, int length)
		{
			if (inputString == null)
			{
				throw LogHelper.LogArgumentNullException("inputString");
			}
			if (inputString.Length == 0)
			{
				return 0;
			}
			if (length == 0)
			{
				return 0;
			}
			if (offset < 0)
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX10716: '{0}' must be greater than 0, was: '{1}'", new object[]
				{
					LogHelper.MarkAsNonPII("offset"),
					LogHelper.MarkAsNonPII(offset)
				})));
			}
			if (length < 0)
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX10716: '{0}' must be greater than 0, was: '{1}'", new object[]
				{
					LogHelper.MarkAsNonPII("length"),
					LogHelper.MarkAsNonPII(length)
				})));
			}
			if (length + offset > inputString.Length)
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX10717: '{0} + {1}' must not be greater than {2}, '{3} + {4} > {5}'.", new object[]
				{
					LogHelper.MarkAsNonPII("length"),
					LogHelper.MarkAsNonPII("offset"),
					LogHelper.MarkAsNonPII("inputString"),
					LogHelper.MarkAsNonPII(length),
					LogHelper.MarkAsNonPII(offset),
					LogHelper.MarkAsNonPII(inputString.Length)
				})));
			}
			if (length % 4 == 1)
			{
				throw LogHelper.LogExceptionMessage(new FormatException(LogHelper.FormatInvariant("IDX10400: Unable to decode: '{0}' as Base64url encoded string.", new object[] { inputString })));
			}
			int num = offset + length - 1;
			if (inputString[num] == '=')
			{
				num--;
				if (inputString[num] == '=')
				{
					num--;
				}
			}
			int num2 = 1 + (num - offset);
			int num3 = num2 % 4;
			if (num3 > 0)
			{
				num3--;
			}
			return num3 + num2 / 4 * 3;
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x000392CC File Offset: 0x000374CC
		private unsafe static void WriteEncodedOutput(byte[] inputBytes, int offset, int length, Span<char> output)
		{
			uint num = 255U;
			int num2 = 0;
			for (int i = offset; i < offset + length; i++)
			{
				num = (num << 8) | (uint)inputBytes[i];
				if ((num & 4278190080U) == 4278190080U)
				{
					*output[num2++] = Base64UrlEncoding.Base64Table[(int)((num & 16515072U) >> 18)];
					*output[num2++] = Base64UrlEncoding.Base64Table[(int)(((num & 196608U) | (num & 61440U)) >> 12)];
					*output[num2++] = Base64UrlEncoding.Base64Table[(int)(((num & 3840U) | (num & 192U)) >> 6)];
					*output[num2++] = Base64UrlEncoding.Base64Table[(int)(num & 63U)];
					num = 255U;
				}
			}
			if ((num & 16711680U) == 16711680U)
			{
				*output[num2++] = Base64UrlEncoding.Base64Table[(int)((num & 64512U) >> 10)];
				*output[num2++] = Base64UrlEncoding.Base64Table[(int)((num & 1008U) >> 4)];
				*output[num2++] = Base64UrlEncoding.Base64Table[(int)((int)(num & 15U) << 2)];
				return;
			}
			if ((num & 65280U) == 65280U)
			{
				*output[num2++] = Base64UrlEncoding.Base64Table[(int)((num & 252U) >> 2)];
				*output[num2++] = Base64UrlEncoding.Base64Table[(int)((int)(num & 3U) << 4)];
			}
		}

		// Token: 0x04000485 RID: 1157
		private const uint IntA = 65U;

		// Token: 0x04000486 RID: 1158
		private const uint IntZ = 90U;

		// Token: 0x04000487 RID: 1159
		private const uint Inta = 97U;

		// Token: 0x04000488 RID: 1160
		private const uint Intz = 122U;

		// Token: 0x04000489 RID: 1161
		private const uint Int0 = 48U;

		// Token: 0x0400048A RID: 1162
		private const uint Int9 = 57U;

		// Token: 0x0400048B RID: 1163
		private const uint IntEq = 61U;

		// Token: 0x0400048C RID: 1164
		private const uint IntPlus = 43U;

		// Token: 0x0400048D RID: 1165
		private const uint IntMinus = 45U;

		// Token: 0x0400048E RID: 1166
		private const uint IntSlash = 47U;

		// Token: 0x0400048F RID: 1167
		private const uint IntUnderscore = 95U;

		// Token: 0x04000490 RID: 1168
		private static readonly char[] Base64Table = new char[]
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
