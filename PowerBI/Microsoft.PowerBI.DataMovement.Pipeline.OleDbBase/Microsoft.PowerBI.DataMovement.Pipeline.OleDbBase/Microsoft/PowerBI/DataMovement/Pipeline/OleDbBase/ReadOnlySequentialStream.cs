using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x020000C7 RID: 199
	public class ReadOnlySequentialStream : ISequentialStream
	{
		// Token: 0x06000372 RID: 882 RVA: 0x0000A4EE File Offset: 0x000086EE
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public ReadOnlySequentialStream(byte[] bytes)
		{
			this.bytes = bytes;
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000A4FD File Offset: 0x000086FD
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public byte[] GetBytes()
		{
			return this.bytes;
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0000A505 File Offset: 0x00008705
		private int GetLength()
		{
			return this.bytes.Length - this.index;
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0000A518 File Offset: 0x00008718
		public unsafe int Read(void* readIntoBuffer, int sizeToRead, out uint actualRead)
		{
			if (readIntoBuffer == null)
			{
				actualRead = 0U;
				return -2147024809;
			}
			if (sizeToRead == 0)
			{
				actualRead = 0U;
				return 0;
			}
			int num = Math.Min(sizeToRead, this.GetLength());
			Marshal.Copy(this.bytes, 0, (IntPtr)readIntoBuffer, num);
			actualRead = (uint)num;
			return 0;
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000A55F File Offset: 0x0000875F
		public unsafe int Write(void* writeFromBuffer, int sizeToWrite, uint* actualWrite)
		{
			return -2147467262;
		}

		// Token: 0x04000381 RID: 897
		[global::System.Runtime.CompilerServices.Nullable(1)]
		private byte[] bytes;

		// Token: 0x04000382 RID: 898
		private int index;
	}
}
