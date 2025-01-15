using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Buffers.Text
{
	// Token: 0x020000EA RID: 234
	internal static class FormattingHelpers
	{
		// Token: 0x06000834 RID: 2100 RVA: 0x0002390C File Offset: 0x00021B0C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static char GetSymbolOrDefault([System.Memory.IsReadOnly] [In] ref StandardFormat format, char defaultSymbol)
		{
			char c = format.Symbol;
			if (c == '\0' && format.Precision == 0)
			{
				c = defaultSymbol;
			}
			return c;
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x00023938 File Offset: 0x00021B38
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void FillWithAsciiZeros(Span<byte> buffer)
		{
			for (int i = 0; i < buffer.Length; i++)
			{
				*buffer[i] = 48;
			}
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x0002396C File Offset: 0x00021B6C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void WriteHexByte(byte value, Span<byte> buffer, int startingIndex = 0, FormattingHelpers.HexCasing casing = FormattingHelpers.HexCasing.Uppercase)
		{
			uint num = (uint)(((int)(value & 240) << 4) + (int)(value & 15) - 35209);
			uint num2 = (uint)((((-num & 28784U) >> 4) + num + (FormattingHelpers.HexCasing)47545U) | casing);
			*buffer[startingIndex + 1] = (byte)num2;
			*buffer[startingIndex] = (byte)(num2 >> 8);
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x000239C4 File Offset: 0x00021BC4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void WriteDigits(ulong value, Span<byte> buffer)
		{
			for (int i = buffer.Length - 1; i >= 1; i--)
			{
				ulong num = 48UL + value;
				value /= 10UL;
				*buffer[i] = (byte)(num - value * 10UL);
			}
			*buffer[0] = (byte)(48UL + value);
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x00023A1C File Offset: 0x00021C1C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void WriteDigitsWithGroupSeparator(ulong value, Span<byte> buffer)
		{
			int num = 0;
			for (int i = buffer.Length - 1; i >= 1; i--)
			{
				ulong num2 = 48UL + value;
				value /= 10UL;
				*buffer[i] = (byte)(num2 - value * 10UL);
				if (num == 2)
				{
					*buffer[--i] = 44;
					num = 0;
				}
				else
				{
					num++;
				}
			}
			*buffer[0] = (byte)(48UL + value);
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x00023A94 File Offset: 0x00021C94
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void WriteDigits(uint value, Span<byte> buffer)
		{
			for (int i = buffer.Length - 1; i >= 1; i--)
			{
				uint num = 48U + value;
				value /= 10U;
				*buffer[i] = (byte)(num - value * 10U);
			}
			*buffer[0] = (byte)(48U + value);
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x00023AE8 File Offset: 0x00021CE8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void WriteFourDecimalDigits(uint value, Span<byte> buffer, int startingIndex = 0)
		{
			uint num = 48U + value;
			value /= 10U;
			*buffer[startingIndex + 3] = (byte)(num - value * 10U);
			num = 48U + value;
			value /= 10U;
			*buffer[startingIndex + 2] = (byte)(num - value * 10U);
			num = 48U + value;
			value /= 10U;
			*buffer[startingIndex + 1] = (byte)(num - value * 10U);
			*buffer[startingIndex] = (byte)(48U + value);
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x00023B60 File Offset: 0x00021D60
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void WriteTwoDecimalDigits(uint value, Span<byte> buffer, int startingIndex = 0)
		{
			uint num = 48U + value;
			value /= 10U;
			*buffer[startingIndex + 1] = (byte)(num - value * 10U);
			*buffer[startingIndex] = (byte)(48U + value);
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x00023B9C File Offset: 0x00021D9C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong DivMod(ulong numerator, ulong denominator, out ulong modulo)
		{
			ulong num = numerator / denominator;
			modulo = numerator - num * denominator;
			return num;
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x00023BBC File Offset: 0x00021DBC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint DivMod(uint numerator, uint denominator, out uint modulo)
		{
			uint num = numerator / denominator;
			modulo = numerator - num * denominator;
			return num;
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x00023BDC File Offset: 0x00021DDC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int CountDecimalTrailingZeros(uint value, out uint valueWithoutTrailingZeros)
		{
			int num = 0;
			if (value != 0U)
			{
				for (;;)
				{
					uint num3;
					uint num2 = FormattingHelpers.DivMod(value, 10U, out num3);
					if (num3 != 0U)
					{
						break;
					}
					value = num2;
					num++;
				}
			}
			valueWithoutTrailingZeros = value;
			return num;
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x00023C14 File Offset: 0x00021E14
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int CountDigits(ulong value)
		{
			int num = 1;
			uint num2;
			if (value >= 10000000UL)
			{
				if (value >= 100000000000000UL)
				{
					num2 = (uint)(value / 100000000000000UL);
					num += 14;
				}
				else
				{
					num2 = (uint)(value / 10000000UL);
					num += 7;
				}
			}
			else
			{
				num2 = (uint)value;
			}
			if (num2 >= 10U)
			{
				if (num2 < 100U)
				{
					num++;
				}
				else if (num2 < 1000U)
				{
					num += 2;
				}
				else if (num2 < 10000U)
				{
					num += 3;
				}
				else if (num2 < 100000U)
				{
					num += 4;
				}
				else if (num2 < 1000000U)
				{
					num += 5;
				}
				else
				{
					num += 6;
				}
			}
			return num;
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x00023CE0 File Offset: 0x00021EE0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int CountDigits(uint value)
		{
			int num = 1;
			if (value >= 100000U)
			{
				value /= 100000U;
				num += 5;
			}
			if (value >= 10U)
			{
				if (value < 100U)
				{
					num++;
				}
				else if (value < 1000U)
				{
					num += 2;
				}
				else if (value < 10000U)
				{
					num += 3;
				}
				else
				{
					num += 4;
				}
			}
			return num;
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x00023D54 File Offset: 0x00021F54
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int CountHexDigits(ulong value)
		{
			int num = 1;
			if (value > (ulong)(-1))
			{
				num += 8;
				value >>= 32;
			}
			if (value > 65535UL)
			{
				num += 4;
				value >>= 16;
			}
			if (value > 255UL)
			{
				num += 2;
				value >>= 8;
			}
			if (value > 15UL)
			{
				num++;
			}
			return num;
		}

		// Token: 0x04000279 RID: 633
		internal const string HexTableLower = "0123456789abcdef";

		// Token: 0x0400027A RID: 634
		internal const string HexTableUpper = "0123456789ABCDEF";

		// Token: 0x02000150 RID: 336
		public enum HexCasing : uint
		{
			// Token: 0x0400033C RID: 828
			Uppercase,
			// Token: 0x0400033D RID: 829
			Lowercase = 8224U
		}
	}
}
