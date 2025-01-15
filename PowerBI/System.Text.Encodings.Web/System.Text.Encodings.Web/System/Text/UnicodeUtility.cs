using System;
using System.Runtime.CompilerServices;

namespace System.Text
{
	// Token: 0x0200001C RID: 28
	internal static class UnicodeUtility
	{
		// Token: 0x06000050 RID: 80 RVA: 0x0000286A File Offset: 0x00000A6A
		public static int GetPlane(uint codePoint)
		{
			return (int)(codePoint >> 16);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002870 File Offset: 0x00000A70
		public static uint GetScalarFromUtf16SurrogatePair(uint highSurrogateCodePoint, uint lowSurrogateCodePoint)
		{
			return (highSurrogateCodePoint << 10) + lowSurrogateCodePoint - 56613888U;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x0000287E File Offset: 0x00000A7E
		public static int GetUtf16SequenceLength(uint value)
		{
			value -= 65536U;
			value += 33554432U;
			value >>= 24;
			return (int)value;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002899 File Offset: 0x00000A99
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void GetUtf16SurrogatesFromSupplementaryPlaneScalar(uint value, out char highSurrogateCodePoint, out char lowSurrogateCodePoint)
		{
			highSurrogateCodePoint = (char)(value + 56557568U >> 10);
			lowSurrogateCodePoint = (char)((value & 1023U) + 56320U);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000028B8 File Offset: 0x00000AB8
		public static int GetUtf8SequenceLength(uint value)
		{
			int num = (int)(value - 2048U) >> 31;
			value ^= 63488U;
			value -= 63616U;
			value += 67108864U;
			value >>= 24;
			return (int)(value + (uint)(num * 2));
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000028F6 File Offset: 0x00000AF6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsAsciiCodePoint(uint value)
		{
			return value <= 127U;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002900 File Offset: 0x00000B00
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsBmpCodePoint(uint value)
		{
			return value <= 65535U;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x0000290D File Offset: 0x00000B0D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsHighSurrogateCodePoint(uint value)
		{
			return UnicodeUtility.IsInRangeInclusive(value, 55296U, 56319U);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x0000291F File Offset: 0x00000B1F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsInRangeInclusive(uint value, uint lowerBound, uint upperBound)
		{
			return value - lowerBound <= upperBound - lowerBound;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x0000292C File Offset: 0x00000B2C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsLowSurrogateCodePoint(uint value)
		{
			return UnicodeUtility.IsInRangeInclusive(value, 56320U, 57343U);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x0000293E File Offset: 0x00000B3E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsSurrogateCodePoint(uint value)
		{
			return UnicodeUtility.IsInRangeInclusive(value, 55296U, 57343U);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002950 File Offset: 0x00000B50
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsValidCodePoint(uint codePoint)
		{
			return codePoint <= 1114111U;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x0000295D File Offset: 0x00000B5D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsValidUnicodeScalar(uint value)
		{
			return ((value - 1114112U) ^ 55296U) >= 4293855232U;
		}

		// Token: 0x04000018 RID: 24
		public const uint ReplacementChar = 65533U;
	}
}
