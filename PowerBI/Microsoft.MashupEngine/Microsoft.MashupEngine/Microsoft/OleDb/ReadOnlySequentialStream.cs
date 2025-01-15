using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001F2A RID: 7978
	public class ReadOnlySequentialStream : ISequentialStream
	{
		// Token: 0x0600C381 RID: 50049 RVA: 0x00272DF3 File Offset: 0x00270FF3
		public ReadOnlySequentialStream(byte[] bytes)
		{
			this.bytes = bytes;
		}

		// Token: 0x0600C382 RID: 50050 RVA: 0x00272E02 File Offset: 0x00271002
		public byte[] GetBytes()
		{
			return this.bytes;
		}

		// Token: 0x0600C383 RID: 50051 RVA: 0x00272E0A File Offset: 0x0027100A
		private int GetLength()
		{
			return this.bytes.Length - this.index;
		}

		// Token: 0x0600C384 RID: 50052 RVA: 0x00272E1C File Offset: 0x0027101C
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

		// Token: 0x0600C385 RID: 50053 RVA: 0x00272E63 File Offset: 0x00271063
		public unsafe int Write(void* writeFromBuffer, int sizeToWrite, uint* actualWrite)
		{
			return -2147467262;
		}

		// Token: 0x04006495 RID: 25749
		private byte[] bytes;

		// Token: 0x04006496 RID: 25750
		private int index;
	}
}
