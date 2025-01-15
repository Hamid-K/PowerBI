using System;
using System.Runtime.CompilerServices;

namespace System.Text.Unicode
{
	// Token: 0x0200001F RID: 31
	internal static class UnicodeHelpers
	{
		// Token: 0x0600008E RID: 142 RVA: 0x000034D9 File Offset: 0x000016D9
		internal static ReadOnlySpan<byte> GetDefinedBmpCodePointsBitmapLittleEndian()
		{
			return UnicodeHelpers.DefinedCharsBitmapSpan;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000034E0 File Offset: 0x000016E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void GetUtf16SurrogatePairFromAstralScalarValue(uint scalar, out char highSurrogate, out char lowSurrogate)
		{
			highSurrogate = (char)(scalar + 56557568U >> 10);
			lowSurrogate = (char)((scalar & 1023U) + 56320U);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003500 File Offset: 0x00001700
		internal static int GetUtf8RepresentationForScalarValue(uint scalar)
		{
			if (scalar <= 127U)
			{
				return (int)((byte)scalar);
			}
			if (scalar <= 2047U)
			{
				byte b = (byte)(192U | (scalar >> 6));
				byte b2 = (byte)(128U | (scalar & 63U));
				return ((int)b2 << 8) | (int)b;
			}
			if (scalar <= 65535U)
			{
				byte b3 = (byte)(224U | (scalar >> 12));
				byte b4 = (byte)(128U | ((scalar >> 6) & 63U));
				byte b5 = (byte)(128U | (scalar & 63U));
				return ((((int)b5 << 8) | (int)b4) << 8) | (int)b3;
			}
			byte b6 = (byte)(240U | (scalar >> 18));
			byte b7 = (byte)(128U | ((scalar >> 12) & 63U));
			byte b8 = (byte)(128U | ((scalar >> 6) & 63U));
			byte b9 = (byte)(128U | (scalar & 63U));
			return ((((((int)b9 << 8) | (int)b8) << 8) | (int)b7) << 8) | (int)b6;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000035C2 File Offset: 0x000017C2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool IsSupplementaryCodePoint(int scalar)
		{
			return (scalar & -65536) != 0;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000092 RID: 146 RVA: 0x000035CE File Offset: 0x000017CE
		private unsafe static ReadOnlySpan<byte> DefinedCharsBitmapSpan
		{
			get
			{
				return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.B3AC89EE9FC6DD6CE1B8349B23557D92E963EFB91DFB57E356869086CF3AB527), 8192);
			}
		}

		// Token: 0x04000021 RID: 33
		internal const int UNICODE_LAST_CODEPOINT = 1114111;
	}
}
