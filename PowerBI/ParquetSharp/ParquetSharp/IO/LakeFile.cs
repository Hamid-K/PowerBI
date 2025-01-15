using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ParquetSharp.IO
{
	// Token: 0x020000A7 RID: 167
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class LakeFile : RandomAccessFile
	{
		// Token: 0x0600050D RID: 1293 RVA: 0x00011540 File Offset: 0x0000F740
		public LakeFile(string filePath)
		{
			this.Handle = LakeFile.Create(filePath, this);
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x00011558 File Offset: 0x0000F758
		public LakeFile(string filePath, AuthenticationContext authenticationContext)
		{
			this.Handle = LakeFile.Create(filePath, authenticationContext, this);
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x00011570 File Offset: 0x0000F770
		private static ParquetHandle Create(string filePath, LakeFile lakeFile)
		{
			LakeFile.<>c__DisplayClass2_0 CS$<>8__locals1 = new LakeFile.<>c__DisplayClass2_0();
			CS$<>8__locals1.lakeFile = lakeFile;
			IntPtr intPtr;
			ExceptionInfo.Check(LakeFile.LakeFile_Create(filePath, out intPtr));
			return new ParquetHandle(intPtr, new Action<IntPtr>(CS$<>8__locals1.<Create>g__Free|0));
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x000115B0 File Offset: 0x0000F7B0
		private static ParquetHandle Create(string filePath, AuthenticationContext authenticationContext, LakeFile lakeFile)
		{
			LakeFile.<>c__DisplayClass3_0 CS$<>8__locals1 = new LakeFile.<>c__DisplayClass3_0();
			CS$<>8__locals1.lakeFile = lakeFile;
			IntPtr intPtr;
			ExceptionInfo.Check(LakeFile.LakeFileWithToken_Create(filePath, authenticationContext, out intPtr));
			return new ParquetHandle(intPtr, new Action<IntPtr>(CS$<>8__locals1.<Create>g__Free|0));
		}

		// Token: 0x06000511 RID: 1297
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr LakeFile_Create(string filePath, out IntPtr randomAccessFile);

		// Token: 0x06000512 RID: 1298
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr LakeFileWithToken_Create(string filePath, AuthenticationContext authenticationContext, out IntPtr randomAccessFile);
	}
}
