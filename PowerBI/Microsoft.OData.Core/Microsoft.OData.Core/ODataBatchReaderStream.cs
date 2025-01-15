using System;

namespace Microsoft.OData
{
	// Token: 0x0200005B RID: 91
	internal abstract class ODataBatchReaderStream
	{
		// Token: 0x06000307 RID: 775 RVA: 0x00008FC2 File Offset: 0x000071C2
		internal ODataBatchReaderStream()
		{
			this.BatchBuffer = new ODataBatchReaderStreamBuffer();
		}

		// Token: 0x06000308 RID: 776
		internal abstract int ReadWithDelimiter(byte[] userBuffer, int userBufferOffset, int count);

		// Token: 0x06000309 RID: 777
		internal abstract int ReadWithLength(byte[] userBuffer, int userBufferOffset, int count);

		// Token: 0x0400015C RID: 348
		protected readonly ODataBatchReaderStreamBuffer BatchBuffer;

		// Token: 0x0400015D RID: 349
		protected bool underlyingStreamExhausted;
	}
}
