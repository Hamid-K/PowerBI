using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System
{
	// Token: 0x0200000C RID: 12
	internal static class HexConverter
	{
		// Token: 0x0600001E RID: 30 RVA: 0x000024B0 File Offset: 0x000006B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void ToBytesBuffer(byte value, Span<byte> buffer, int startingIndex = 0, HexConverter.Casing casing = HexConverter.Casing.Upper)
		{
			uint num = (uint)(((int)(value & 240) << 4) + (int)(value & 15) - 35209);
			uint num2 = (uint)((((-num & 28784U) >> 4) + num + (HexConverter.Casing)47545U) | casing);
			*buffer[startingIndex + 1] = (byte)num2;
			*buffer[startingIndex] = (byte)(num2 >> 8);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002504 File Offset: 0x00000704
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void ToCharsBuffer(byte value, Span<char> buffer, int startingIndex = 0, HexConverter.Casing casing = HexConverter.Casing.Upper)
		{
			uint num = (uint)(((int)(value & 240) << 4) + (int)(value & 15) - 35209);
			uint num2 = (uint)((((-num & 28784U) >> 4) + num + (HexConverter.Casing)47545U) | casing);
			*buffer[startingIndex + 1] = (char)(num2 & 255U);
			*buffer[startingIndex] = (char)(num2 >> 8);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000255C File Offset: 0x0000075C
		public unsafe static void EncodeToUtf16(ReadOnlySpan<byte> bytes, Span<char> chars, HexConverter.Casing casing = HexConverter.Casing.Upper)
		{
			for (int i = 0; i < bytes.Length; i++)
			{
				HexConverter.ToCharsBuffer(*bytes[i], chars, i * 2, casing);
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002590 File Offset: 0x00000790
		[SecuritySafeCritical]
		public unsafe static string ToString(ReadOnlySpan<byte> bytes, HexConverter.Casing casing = HexConverter.Casing.Upper)
		{
			Span<char> span = default(Span<char>);
			if (bytes.Length > 16)
			{
				char[] array = new char[bytes.Length * 2];
				span = MemoryExtensions.AsSpan<char>(array);
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
			int num2 = 0;
			ReadOnlySpan<byte> readOnlySpan = bytes;
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				byte b = *readOnlySpan[i];
				HexConverter.ToCharsBuffer(b, span, num2, casing);
				num2 += 2;
			}
			return span.ToString();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002625 File Offset: 0x00000825
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

		// Token: 0x06000023 RID: 35 RVA: 0x0000263F File Offset: 0x0000083F
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

		// Token: 0x06000024 RID: 36 RVA: 0x0000265C File Offset: 0x0000085C
		public static bool TryDecodeFromUtf16(ReadOnlySpan<char> chars, Span<byte> bytes)
		{
			int num;
			return HexConverter.TryDecodeFromUtf16(chars, bytes, out num);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002674 File Offset: 0x00000874
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

		// Token: 0x06000026 RID: 38 RVA: 0x000026F4 File Offset: 0x000008F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int FromChar(int c)
		{
			if (c < HexConverter.CharToHexLookup.Length)
			{
				return (int)(*HexConverter.CharToHexLookup[c]);
			}
			return 255;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002728 File Offset: 0x00000928
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int FromUpperChar(int c)
		{
			if (c <= 71)
			{
				return (int)(*HexConverter.CharToHexLookup[c]);
			}
			return 255;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000274F File Offset: 0x0000094F
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

		// Token: 0x06000029 RID: 41 RVA: 0x00002774 File Offset: 0x00000974
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

		// Token: 0x0600002A RID: 42 RVA: 0x000027C0 File Offset: 0x000009C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsHexUpperChar(int c)
		{
			return c - 48 <= 9 || c - 65 <= 5;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000027D6 File Offset: 0x000009D6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsHexLowerChar(int c)
		{
			return c - 48 <= 9 || c - 97 <= 5;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002C RID: 44 RVA: 0x000027EC File Offset: 0x000009EC
		public unsafe static ReadOnlySpan<byte> CharToHexLookup
		{
			get
			{
				return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.21244F82B210125632917591768F6BF22EB6861F80C6C25A25BD26DFB580EA7B), 256);
			}
		}

		// Token: 0x0200006F RID: 111
		public enum Casing : uint
		{
			// Token: 0x04000154 RID: 340
			Upper,
			// Token: 0x04000155 RID: 341
			Lower = 8224U
		}
	}
}
