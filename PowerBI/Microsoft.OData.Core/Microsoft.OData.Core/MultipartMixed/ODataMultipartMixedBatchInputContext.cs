using System;
using System.Threading.Tasks;

namespace Microsoft.OData.MultipartMixed
{
	// Token: 0x02000203 RID: 515
	internal sealed class ODataMultipartMixedBatchInputContext : ODataRawInputContext
	{
		// Token: 0x060016B2 RID: 5810 RVA: 0x0003F6FC File Offset: 0x0003D8FC
		public ODataMultipartMixedBatchInputContext(ODataFormat format, ODataMessageInfo messageInfo, ODataMessageReaderSettings messageReaderSettings)
			: base(format, messageInfo, messageReaderSettings)
		{
			try
			{
				this.batchBoundary = ODataMultipartMixedBatchWriterUtils.GetBatchBoundaryFromMediaType(messageInfo.MediaType);
			}
			catch (Exception ex)
			{
				if (ExceptionUtils.IsCatchableExceptionType(ex))
				{
					messageInfo.MessageStream.Dispose();
				}
				throw;
			}
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x0003F74C File Offset: 0x0003D94C
		internal override ODataBatchReader CreateBatchReader()
		{
			return this.CreateBatchReaderImplementation(true);
		}

		// Token: 0x060016B4 RID: 5812 RVA: 0x0003F755 File Offset: 0x0003D955
		internal override Task<ODataBatchReader> CreateBatchReaderAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperation<ODataBatchReader>(() => this.CreateBatchReaderImplementation(false));
		}

		// Token: 0x060016B5 RID: 5813 RVA: 0x0003F768 File Offset: 0x0003D968
		private ODataBatchReader CreateBatchReaderImplementation(bool synchronous)
		{
			return new ODataMultipartMixedBatchReader(this, this.batchBoundary, this.Encoding, synchronous);
		}

		// Token: 0x04000A4B RID: 2635
		private string batchBoundary;
	}
}
