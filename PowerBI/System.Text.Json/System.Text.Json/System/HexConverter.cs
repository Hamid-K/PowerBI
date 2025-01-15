using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x0200000C RID: 12
	internal static class HexConverter
	{
		// Token: 0x0600000B RID: 11 RVA: 0x000020CC File Offset: 0x000002CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void ToBytesBuffer(byte value, Span<byte> buffer, int startingIndex = 0, HexConverter.Casing casing = HexConverter.Casing.Upper)
		{
			uint num = (uint)(((int)(value & 240) << 4) + (int)(value & 15) - 35209);
			uint num2 = (uint)((((-num & 28784U) >> 4) + num + (HexConverter.Casing)47545U) | casing);
			*buffer[startingIndex + 1] = (byte)num2;
			*buffer[startingIndex] = (byte)(num2 >> 8);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002120 File Offset: 0x00000320
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void ToCharsBuffer(byte value, Span<char> buffer, int startingIndex = 0, HexConverter.Casing casing = HexConverter.Casing.Upper)
		{
			uint num = (uint)(((int)(value & 240) << 4) + (int)(value & 15) - 35209);
			uint num2 = (uint)((((-num & 28784U) >> 4) + num + (HexConverter.Casing)47545U) | casing);
			*buffer[startingIndex + 1] = (char)(num2 & 255U);
			*buffer[startingIndex] = (char)(num2 >> 8);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002178 File Offset: 0x00000378
		public unsafe static void EncodeToUtf16(ReadOnlySpan<byte> bytes, Span<char> chars, HexConverter.Casing casing = HexConverter.Casing.Upper)
		{
			for (int i = 0; i < bytes.Length; i++)
			{
				HexConverter.ToCharsBuffer(*bytes[i], chars, i * 2, casing);
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021AC File Offset: 0x000003AC
		public unsafe static string ToString(ReadOnlySpan<byte> bytes, HexConverter.Casing casing = HexConverter.Casing.Upper)
		{
			Span<char> span;
			if (bytes.Length > 16)
			{
				span = new char[bytes.Length * 2].AsSpan<char>();
			}
			else
			{
				int num = bytes.Length * 2;
				checked
				{
					Span<char> span2 = new Span<char>(stackalloc byte[unchecked((UIntPtr)num) * 2], num);
					span = span2;
				}
			}
			Span<char> span3 = span;
			int num2 = 0;
			ReadOnlySpan<byte> readOnlySpan = bytes;
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				byte b = *readOnlySpan[i];
				HexConverter.ToCharsBuffer(b, span3, num2, casing);
				num2 += 2;
			}
			return span3.ToString();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002239 File Offset: 0x00000439
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static char ToCharUpper(int value)
		{
			value &= 15;
			value += 48;
			if (value > 57)
			{
				value += 7;
			}
			return (char)value;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002253 File Offset: 0x00000453
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static char ToCharLower(int value)
		{
			value &= 15;
			value += 48;
			if (value > 57)
			{
				value += 39;
			}
			return (char)value;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002270 File Offset: 0x00000470
		public static bool TryDecodeFromUtf16(ReadOnlySpan<char> chars, Span<byte> bytes)
		{
			int num;
			return HexConverter.TryDecodeFromUtf16(chars, bytes, out num);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002288 File Offset: 0x00000488
		public unsafe static bool TryDecodeFromUtf16(ReadOnlySpan<char> chars, Span<byte> bytes, out int charsProcessed)
		{
			int num = 0;
			int i = 0;
			int num2 = 0;
			int num3 = 0;
			while (i < bytes.Length)
			{
				num2 = HexConverter.FromChar((int)(*chars[num + 1]));
				num3 = HexConverter.FromChar((int)(*chars[num]));
				if ((num2 | num3) == 255)
				{
					break;
				}
				*bytes[i++] = (byte)((num3 << 4) | num2);
				num += 2;
			}
			if (num2 == 255)
			{
				num++;
			}
			charsProcessed = num;
			return (num2 | num3) != 255;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002308 File Offset: 0x00000508
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int FromChar(int c)
		{
			if (c < HexConverter.CharToHexLookup.Length)
			{
				return (int)(*HexConverter.CharToHexLookup[c]);
			}
			return 255;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000233C File Offset: 0x0000053C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int FromUpperChar(int c)
		{
			if (c <= 71)
			{
				return (int)(*HexConverter.CharToHexLookup[c]);
			}
			return 255;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002363 File Offset: 0x00000563
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int FromLowerChar(int c)
		{
			if (c - 48 <= 9)
			{
				return c - 48;
			}
			if (c - 97 <= 5)
			{
				return c - 97 + 10;
			}
			return 255;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002388 File Offset: 0x00000588
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsHexChar(int c)
		{
			if (IntPtr.Size == 8)
			{
				ulong num = (ulong)(c - 48);
				ulong num2 = 18428868213665201664UL << (int)num;
				ulong num3 = num - 64UL;
				return (num2 & num3) < 0UL;
			}
			return HexConverter.FromChar(c) != 255;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000023D1 File Offset: 0x000005D1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsHexUpperChar(int c)
		{
			return c - 48 <= 9 || c - 65 <= 5;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000023E7 File Offset: 0x000005E7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsHexLowerChar(int c)
		{
			return c - 48 <= 9 || c - 97 <= 5;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000023FD File Offset: 0x000005FD
		public unsafe static ReadOnlySpan<byte> CharToHexLookup
		{
			get
			{
				return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.21244F82B210125632917591768F6BF22EB6861F80C6C25A25BD26DFB580EA7B), 256);
			}
		}

		// Token: 0x0200010D RID: 269
		public enum Casing : uint
		{
			// Token: 0x04000430 RID: 1072
			Upper,
			// Token: 0x04000431 RID: 1073
			Lower = 8224U
		}
	}
}
