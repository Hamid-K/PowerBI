using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNet.OData.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData;

namespace Microsoft.AspNet.OData.Batch
{
	// Token: 0x020001CB RID: 459
	public class ODataBatchContent : HttpContent
	{
		// Token: 0x06000F26 RID: 3878 RVA: 0x0003E5EC File Offset: 0x0003C7EC
		public ODataBatchContent(IEnumerable<ODataBatchResponseItem> responses, IServiceProvider requestContainer)
			: this(responses, requestContainer, null)
		{
		}

		// Token: 0x06000F27 RID: 3879 RVA: 0x0003E5F8 File Offset: 0x0003C7F8
		public ODataBatchContent(IEnumerable<ODataBatchResponseItem> responses, IServiceProvider requestContainer, MediaTypeHeaderValue contentType)
		{
			this.Initialize(responses, requestContainer);
			if (contentType == null)
			{
				contentType = MediaTypeHeaderValue.Parse(string.Format(CultureInfo.InvariantCulture, "multipart/mixed;boundary=batchresponse_{0}", new object[] { Guid.NewGuid() }));
			}
			base.Headers.ContentType = contentType;
			ODataVersion valueOrDefault = this._writerSettings.Version.GetValueOrDefault();
			base.Headers.TryAddWithoutValidation("OData-Version", ODataUtils.ODataVersionToString(valueOrDefault));
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x0003E678 File Offset: 0x0003C878
		protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
		{
			IODataResponseMessage iodataResponseMessage = ODataMessageWrapperHelper.Create(stream, base.Headers, this._requestContainer);
			return this.WriteToResponseMessageAsync(iodataResponseMessage);
		}

		// Token: 0x06000F29 RID: 3881 RVA: 0x0003E5E5 File Offset: 0x0003C7E5
		protected override bool TryComputeLength(out long length)
		{
			length = -1L;
			return false;
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x0003E6A0 File Offset: 0x0003C8A0
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				foreach (ODataBatchResponseItem odataBatchResponseItem in this.Responses)
				{
					if (odataBatchResponseItem != null)
					{
						odataBatchResponseItem.Dispose();
					}
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x0003E6FC File Offset: 0x0003C8FC
		private void Initialize(IEnumerable<ODataBatchResponseItem> responses, IServiceProvider requestContainer)
		{
			if (responses == null)
			{
				throw Error.ArgumentNull("responses");
			}
			this.Responses = responses;
			this._requestContainer = requestContainer;
			this._writerSettings = ServiceProviderServiceExtensions.GetRequiredService<ODataMessageWriterSettings>(requestContainer);
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000F2C RID: 3884 RVA: 0x0003E726 File Offset: 0x0003C926
		// (set) Token: 0x06000F2D RID: 3885 RVA: 0x0003E72E File Offset: 0x0003C92E
		public IEnumerable<ODataBatchResponseItem> Responses { get; private set; }

		// Token: 0x06000F2E RID: 3886 RVA: 0x0003E738 File Offset: 0x0003C938
		private async Task WriteToResponseMessageAsync(IODataResponseMessage responseMessage)
		{
			ODataMessageWriter odataMessageWriter = new ODataMessageWriter(responseMessage, this._writerSettings);
			ODataBatchWriter writer = odataMessageWriter.CreateODataBatchWriter();
			writer.WriteStartBatch();
			foreach (ODataBatchResponseItem odataBatchResponseItem in this.Responses)
			{
				await odataBatchResponseItem.WriteResponseAsync(writer);
			}
			IEnumerator<ODataBatchResponseItem> enumerator = null;
			writer.WriteEndBatch();
		}

		// Token: 0x04000434 RID: 1076
		private IServiceProvider _requestContainer;

		// Token: 0x04000435 RID: 1077
		private ODataMessageWriterSettings _writerSettings;
	}
}
