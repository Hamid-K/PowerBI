using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ParquetSharp.IO
{
	// Token: 0x020000AB RID: 171
	public abstract class OutputStream : IDisposable
	{
		// Token: 0x0600052D RID: 1325 RVA: 0x00011E4C File Offset: 0x0001004C
		protected OutputStream()
		{
			this.Handle = null;
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x00011E5C File Offset: 0x0001005C
		protected OutputStream(IntPtr handle)
		{
			this.Handle = new ParquetHandle(handle, new Action<IntPtr>(OutputStream.OutputStream_Free));
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x00011E7C File Offset: 0x0001007C
		public void Dispose()
		{
			ParquetHandle handle = this.Handle;
			if (handle != null)
			{
				handle.Dispose();
			}
			this.Handle = null;
		}

		// Token: 0x06000530 RID: 1328
		[DllImport("ParquetSharpNative")]
		internal static extern void OutputStream_Free(IntPtr outputStream);

		// Token: 0x0400018B RID: 395
		[Nullable(2)]
		internal ParquetHandle Handle;
	}
}
