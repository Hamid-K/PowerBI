using System;
using Microsoft.OData.Json;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x02000229 RID: 553
	internal sealed class ODataJsonLightBatchReaderStream : ODataBatchReaderStream
	{
		// Token: 0x06001825 RID: 6181 RVA: 0x00045783 File Offset: 0x00043983
		internal ODataJsonLightBatchReaderStream(ODataJsonLightInputContext inputContext)
		{
			this.inputContext = inputContext;
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06001826 RID: 6182 RVA: 0x00045792 File Offset: 0x00043992
		internal BufferingJsonReader JsonReader
		{
			get
			{
				return this.inputContext.JsonReader;
			}
		}

		// Token: 0x06001827 RID: 6183 RVA: 0x000032BD File Offset: 0x000014BD
		internal override int ReadWithDelimiter(byte[] userBuffer, int userBufferOffset, int count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001828 RID: 6184 RVA: 0x000457A0 File Offset: 0x000439A0
		internal override int ReadWithLength(byte[] userBuffer, int userBufferOffset, int count)
		{
			int i = count;
			while (i > 0)
			{
				if (this.BatchBuffer.NumberOfBytesInBuffer >= i)
				{
					Buffer.BlockCopy(this.BatchBuffer.Bytes, this.BatchBuffer.CurrentReadPosition, userBuffer, userBufferOffset, i);
					this.BatchBuffer.SkipTo(this.BatchBuffer.CurrentReadPosition + i);
					i = 0;
				}
				else
				{
					int numberOfBytesInBuffer = this.BatchBuffer.NumberOfBytesInBuffer;
					Buffer.BlockCopy(this.BatchBuffer.Bytes, this.BatchBuffer.CurrentReadPosition, userBuffer, userBufferOffset, numberOfBytesInBuffer);
					i -= numberOfBytesInBuffer;
					userBufferOffset += numberOfBytesInBuffer;
					if (this.underlyingStreamExhausted)
					{
						throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataBatchReaderStreamBuffer_ReadWithLength));
					}
					this.underlyingStreamExhausted = this.BatchBuffer.RefillFrom(this.inputContext.Stream, 8000);
				}
			}
			return count - i;
		}

		// Token: 0x04000ACF RID: 2767
		private readonly ODataJsonLightInputContext inputContext;
	}
}
