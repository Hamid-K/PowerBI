using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ParquetSharp.IO
{
	// Token: 0x020000AC RID: 172
	public abstract class RandomAccessFile : IDisposable
	{
		// Token: 0x06000531 RID: 1329 RVA: 0x00011E9C File Offset: 0x0001009C
		protected RandomAccessFile()
		{
			this.Handle = null;
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x00011EAC File Offset: 0x000100AC
		protected RandomAccessFile(IntPtr handle)
		{
			this.Handle = new ParquetHandle(handle, new Action<IntPtr>(RandomAccessFile.RandomAccessFile_Free));
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x00011ECC File Offset: 0x000100CC
		public void Dispose()
		{
			ParquetHandle handle = this.Handle;
			if (handle != null)
			{
				handle.Dispose();
			}
			this.Handle = null;
		}

		// Token: 0x06000534 RID: 1332
		[DllImport("ParquetSharpNative")]
		internal static extern void RandomAccessFile_Free(IntPtr randomAccessFile);

		// Token: 0x0400018C RID: 396
		[Nullable(2)]
		internal ParquetHandle Handle;
	}
}
