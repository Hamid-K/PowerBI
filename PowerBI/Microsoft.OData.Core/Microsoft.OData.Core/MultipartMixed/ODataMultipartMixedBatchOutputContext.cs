using System;
using System.Threading.Tasks;

namespace Microsoft.OData.MultipartMixed
{
	// Token: 0x02000204 RID: 516
	internal sealed class ODataMultipartMixedBatchOutputContext : ODataRawOutputContext
	{
		// Token: 0x060016B7 RID: 5815 RVA: 0x0003F786 File Offset: 0x0003D986
		internal ODataMultipartMixedBatchOutputContext(ODataFormat format, ODataMessageInfo messageInfo, ODataMessageWriterSettings messageWriterSettings)
			: base(format, messageInfo, messageWriterSettings)
		{
			this.batchBoundary = ODataMultipartMixedBatchWriterUtils.GetBatchBoundaryFromMediaType(messageInfo.MediaType);
		}

		// Token: 0x060016B8 RID: 5816 RVA: 0x0003F7A2 File Offset: 0x0003D9A2
		internal override ODataBatchWriter CreateODataBatchWriter()
		{
			return this.CreateODataBatchWriterImplementation();
		}

		// Token: 0x060016B9 RID: 5817 RVA: 0x0003F7AA File Offset: 0x0003D9AA
		internal override Task<ODataBatchWriter> CreateODataBatchWriterAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperation<ODataBatchWriter>(() => this.CreateODataBatchWriterImplementation());
		}

		// Token: 0x060016BA RID: 5818 RVA: 0x0003F7C0 File Offset: 0x0003D9C0
		private ODataBatchWriter CreateODataBatchWriterImplementation()
		{
			this.encoding = this.encoding ?? MediaTypeUtils.EncodingUtf8NoPreamble;
			ODataBatchWriter odataBatchWriter = new ODataMultipartMixedBatchWriter(this, this.batchBoundary);
			this.outputInStreamErrorListener = odataBatchWriter;
			return odataBatchWriter;
		}

		// Token: 0x04000A4C RID: 2636
		private readonly string batchBoundary;
	}
}
