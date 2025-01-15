using System;
using System.Runtime.InteropServices;

namespace ParquetSharp.IO
{
	// Token: 0x020000AD RID: 173
	public sealed class ResizableBuffer : Buffer
	{
		// Token: 0x06000535 RID: 1333 RVA: 0x00011EEC File Offset: 0x000100EC
		public ResizableBuffer(long initialSize = 128L)
			: base(ExceptionInfo.Return<long, IntPtr>(initialSize, new ExceptionInfo.GetAction<long, IntPtr>(ResizableBuffer.ResizableBuffer_Create)))
		{
		}

		// Token: 0x06000536 RID: 1334
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ResizableBuffer_Create(long initialSize, out IntPtr resizableBuffer);
	}
}
