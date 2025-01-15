using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.MachineLearning.Data.IO.Zlib
{
	// Token: 0x020000E4 RID: 228
	internal static class Zlib
	{
		// Token: 0x060004BA RID: 1210
		[SuppressUnmanagedCodeSecurity]
		[DllImport("zlib.dll")]
		private unsafe static extern Constants.RetCode deflateInit2_(ZStream* strm, int level, int method, int windowBits, int memLevel, Constants.Strategy strategy, byte* version, int stream_size);

		// Token: 0x060004BB RID: 1211
		[SuppressUnmanagedCodeSecurity]
		[DllImport("zlib.dll")]
		private unsafe static extern Constants.RetCode inflateInit2_(ZStream* strm, int windowBits, byte* version, int stream_size);

		// Token: 0x060004BC RID: 1212
		[SuppressUnmanagedCodeSecurity]
		[DllImport("zlib.dll")]
		private unsafe static extern byte* zlibVersion();

		// Token: 0x060004BD RID: 1213
		[SuppressUnmanagedCodeSecurity]
		[DllImport("zlib.dll")]
		public unsafe static extern Constants.RetCode deflateEnd(ZStream* strm);

		// Token: 0x060004BE RID: 1214
		[SuppressUnmanagedCodeSecurity]
		[DllImport("zlib.dll")]
		public unsafe static extern Constants.RetCode deflate(ZStream* strm, Constants.Flush flush);

		// Token: 0x060004BF RID: 1215 RVA: 0x0001A1D1 File Offset: 0x000183D1
		public unsafe static Constants.RetCode deflateInit2(ZStream* strm, int level, int method, int windowBits, int memLevel, Constants.Strategy strategy)
		{
			return Zlib.deflateInit2_(strm, level, method, windowBits, memLevel, strategy, Zlib.zlibVersion(), sizeof(ZStream));
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0001A1EB File Offset: 0x000183EB
		public unsafe static Constants.RetCode inflateInit2(ZStream* strm, int windowBits)
		{
			return Zlib.inflateInit2_(strm, windowBits, Zlib.zlibVersion(), sizeof(ZStream));
		}

		// Token: 0x060004C1 RID: 1217
		[SuppressUnmanagedCodeSecurity]
		[DllImport("zlib.dll")]
		public unsafe static extern Constants.RetCode inflate(ZStream* strm, Constants.Flush flush);

		// Token: 0x060004C2 RID: 1218
		[SuppressUnmanagedCodeSecurity]
		[DllImport("zlib.dll")]
		public unsafe static extern Constants.RetCode inflateEnd(ZStream* strm);

		// Token: 0x04000234 RID: 564
		public const string DLLPATH = "zlib.dll";
	}
}
