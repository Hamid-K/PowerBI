using System;
using System.Runtime.InteropServices;

namespace Microsoft.HostIntegration.Nls
{
	// Token: 0x02000640 RID: 1600
	internal class NativeInterface
	{
		// Token: 0x060035CB RID: 13771
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int GetCharacterPlacementW(IntPtr dev, [MarshalAs(UnmanagedType.BStr)] string str, int count, int max, [In] [Out] ref NativeInterface.GpcResults results, NativeInterface.GcpFlags flags);

		// Token: 0x060035CC RID: 13772
		[DllImport("gdi32.dll", ExactSpelling = true)]
		public static extern NativeInterface.GcpFlags GetFontLanguageInfo(IntPtr dev);

		// Token: 0x060035CD RID: 13773
		[DllImport("user32.dll")]
		public static extern IntPtr GetDC(IntPtr ptr);

		// Token: 0x060035CE RID: 13774
		[DllImport("user32.dll")]
		public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

		// Token: 0x060035CF RID: 13775
		[DllImport("gdi32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr CreateFontIndirect([MarshalAs(UnmanagedType.LPStruct)] [In] NativeInterface.LOGFONT lplf);

		// Token: 0x060035D0 RID: 13776
		[DllImport("gdi32.dll")]
		public static extern IntPtr SelectObject(IntPtr handle, IntPtr hgdiObject);

		// Token: 0x060035D1 RID: 13777
		[DllImport("gdi32.dll")]
		public static extern bool DeleteObject(IntPtr handle);

		// Token: 0x060035D2 RID: 13778
		[DllImport("gdi32.dll")]
		public static extern uint SetTextAlign(IntPtr handle, uint mode);

		// Token: 0x060035D3 RID: 13779
		[DllImport("gdi32.dll")]
		public static extern uint GetTextAlign(IntPtr handle);

		// Token: 0x060035D4 RID: 13780
		[DllImport("Usp10.dll", CallingConvention = CallingConvention.StdCall)]
		public static extern int ScriptGetCMap(IntPtr hdc, IntPtr sc, [MarshalAs(UnmanagedType.BStr)] string str, int cChars, uint flags, IntPtr ptr);

		// Token: 0x060035D5 RID: 13781
		[DllImport("usp10.dll")]
		public static extern int ScriptFreeCache(IntPtr psc);

		// Token: 0x060035D6 RID: 13782
		[DllImport("kernel32.dll")]
		public static extern int MultiByteToWideChar(int CodePage, int dwFlags, [MarshalAs(UnmanagedType.LPArray)] byte[] lpMultiByteStr, int cbMultiByte, IntPtr outBytes, int cchWideChar);

		// Token: 0x060035D7 RID: 13783
		[DllImport("kernel32.dll")]
		public static extern int WideCharToMultiByte(int CodePage, uint dwFlags, IntPtr charPtr, int cchWideChar, [MarshalAs(UnmanagedType.LPArray)] byte[] lpMultiByteStr, int cbMultiByte, IntPtr lpDefaultChar, out bool lpUsedDefaultChar);

		// Token: 0x04001EE5 RID: 7909
		private const int MB_PRECOMPOSED = 1;

		// Token: 0x02000641 RID: 1601
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public class LOGFONT
		{
			// Token: 0x04001EE6 RID: 7910
			public const int LF_FACESIZE = 32;

			// Token: 0x04001EE7 RID: 7911
			public int lfHeight;

			// Token: 0x04001EE8 RID: 7912
			public int lfWidth;

			// Token: 0x04001EE9 RID: 7913
			public int lfEscapement;

			// Token: 0x04001EEA RID: 7914
			public int lfOrientation;

			// Token: 0x04001EEB RID: 7915
			public int lfWeight;

			// Token: 0x04001EEC RID: 7916
			public byte lfItalic;

			// Token: 0x04001EED RID: 7917
			public byte lfUnderline;

			// Token: 0x04001EEE RID: 7918
			public byte lfStrikeOut;

			// Token: 0x04001EEF RID: 7919
			public byte lfCharSet;

			// Token: 0x04001EF0 RID: 7920
			public byte lfOutPrecision;

			// Token: 0x04001EF1 RID: 7921
			public byte lfClipPrecision;

			// Token: 0x04001EF2 RID: 7922
			public byte lfQuality;

			// Token: 0x04001EF3 RID: 7923
			public byte lfPitchAndFamily;

			// Token: 0x04001EF4 RID: 7924
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string lfFaceName;
		}

		// Token: 0x02000642 RID: 1602
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public struct SCRIPT_CACHE
		{
			// Token: 0x04001EF5 RID: 7925
			private IntPtr ptr;
		}

		// Token: 0x02000643 RID: 1603
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public struct GpcResults
		{
			// Token: 0x04001EF6 RID: 7926
			public int StructSize;

			// Token: 0x04001EF7 RID: 7927
			[MarshalAs(UnmanagedType.BStr)]
			public string OutString;

			// Token: 0x04001EF8 RID: 7928
			public IntPtr Order;

			// Token: 0x04001EF9 RID: 7929
			public IntPtr Dx;

			// Token: 0x04001EFA RID: 7930
			public IntPtr CaretPos;

			// Token: 0x04001EFB RID: 7931
			public IntPtr Class;

			// Token: 0x04001EFC RID: 7932
			public IntPtr Glyphs;

			// Token: 0x04001EFD RID: 7933
			public int GlyphCount;

			// Token: 0x04001EFE RID: 7934
			public int MaxFit;
		}

		// Token: 0x02000644 RID: 1604
		[Flags]
		public enum GcpFlags
		{
			// Token: 0x04001F00 RID: 7936
			DBCS = 1,
			// Token: 0x04001F01 RID: 7937
			ReOrder = 2,
			// Token: 0x04001F02 RID: 7938
			UseKerning = 8,
			// Token: 0x04001F03 RID: 7939
			GlyphShape = 16,
			// Token: 0x04001F04 RID: 7940
			Ligate = 32,
			// Token: 0x04001F05 RID: 7941
			Diacritic = 256,
			// Token: 0x04001F06 RID: 7942
			Kashida = 1024,
			// Token: 0x04001F07 RID: 7943
			Error = 32768,
			// Token: 0x04001F08 RID: 7944
			Justify = 65536,
			// Token: 0x04001F09 RID: 7945
			FliGlyphs = 262144,
			// Token: 0x04001F0A RID: 7946
			ClassIn = 524288,
			// Token: 0x04001F0B RID: 7947
			MaxExtent = 1048576,
			// Token: 0x04001F0C RID: 7948
			JustifyIn = 2097152,
			// Token: 0x04001F0D RID: 7949
			DisplayZWG = 4194304,
			// Token: 0x04001F0E RID: 7950
			SymSwapOff = 8388608,
			// Token: 0x04001F0F RID: 7951
			NumericOverride = 16777216,
			// Token: 0x04001F10 RID: 7952
			NeutralOverride = 33554432,
			// Token: 0x04001F11 RID: 7953
			NumericsLatin = 67108864,
			// Token: 0x04001F12 RID: 7954
			NumericsLocal = 134217728,
			// Token: 0x04001F13 RID: 7955
			FliMask = 4155
		}
	}
}
