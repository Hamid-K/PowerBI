using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ParquetSharp.IO
{
	// Token: 0x020000A5 RID: 165
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BufferOutputStream : OutputStream
	{
		// Token: 0x06000505 RID: 1285 RVA: 0x000114C4 File Offset: 0x0000F6C4
		public BufferOutputStream()
			: base(ExceptionInfo.Return<IntPtr>(new ExceptionInfo.GetAction<IntPtr>(BufferOutputStream.BufferOutputStream_Create)))
		{
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x000114E0 File Offset: 0x0000F6E0
		public BufferOutputStream(ResizableBuffer resizableBuffer)
			: base(ExceptionInfo.Return<IntPtr>(resizableBuffer.Handle, new ExceptionInfo.GetFunction<IntPtr>(BufferOutputStream.BufferOutputStream_Create_From_ResizableBuffer)))
		{
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00011500 File Offset: 0x0000F700
		public Buffer Finish()
		{
			return new Buffer(ExceptionInfo.Return<IntPtr>(this.Handle, new ExceptionInfo.GetFunction<IntPtr>(BufferOutputStream.BufferOutputStream_Finish)));
		}

		// Token: 0x06000508 RID: 1288
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr BufferOutputStream_Create(out IntPtr outputStream);

		// Token: 0x06000509 RID: 1289
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr BufferOutputStream_Create_From_ResizableBuffer(IntPtr resizableBuffer, out IntPtr outputStream);

		// Token: 0x0600050A RID: 1290
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr BufferOutputStream_Finish(IntPtr outputStream, out IntPtr buffer);
	}
}
