using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001265 RID: 4709
	public class BinaryFormatBufferPool
	{
		// Token: 0x06007C2B RID: 31787 RVA: 0x001AB6A8 File Offset: 0x001A98A8
		public byte[] TakeBuffer(int minimumSize)
		{
			byte[] array = this.buffer;
			if (array != null && minimumSize <= 1024)
			{
				byte[] array2 = array;
				this.buffer = null;
				return array2;
			}
			if (minimumSize < 1024)
			{
				minimumSize = 1024;
			}
			return new byte[minimumSize];
		}

		// Token: 0x06007C2C RID: 31788 RVA: 0x001AB6E5 File Offset: 0x001A98E5
		public void ReturnBuffer(byte[] buffer)
		{
			if (this.buffer == null && buffer.Length == 1024)
			{
				this.buffer = buffer;
			}
		}

		// Token: 0x040044A5 RID: 17573
		private byte[] buffer;

		// Token: 0x040044A6 RID: 17574
		private const int bufferSize = 1024;
	}
}
