using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ParquetSharp.IO
{
	// Token: 0x020000A6 RID: 166
	public sealed class BufferReader : RandomAccessFile
	{
		// Token: 0x0600050B RID: 1291 RVA: 0x00011520 File Offset: 0x0000F720
		[NullableContext(1)]
		public BufferReader(Buffer buffer)
			: base(ExceptionInfo.Return<IntPtr>(buffer.Handle, new ExceptionInfo.GetFunction<IntPtr>(BufferReader.BufferReader_Create)))
		{
		}

		// Token: 0x0600050C RID: 1292
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr BufferReader_Create(IntPtr buffer, out IntPtr bufferReader);
	}
}
