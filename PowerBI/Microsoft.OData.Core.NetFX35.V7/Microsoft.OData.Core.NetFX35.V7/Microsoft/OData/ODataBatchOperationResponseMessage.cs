using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.OData
{
	// Token: 0x02000032 RID: 50
	public sealed class ODataBatchOperationResponseMessage : IODataResponseMessage, IODataPayloadUriConverter, IContainerProvider
	{
		// Token: 0x06000162 RID: 354 RVA: 0x00005D21 File Offset: 0x00003F21
		private ODataBatchOperationResponseMessage(Func<Stream> contentStreamCreatorFunc, ODataBatchOperationHeaders headers, IODataBatchOperationListener operationListener, string contentId, IODataPayloadUriConverter payloadUriConverter, bool writing, IServiceProvider container)
		{
			this.message = new ODataBatchOperationMessage(contentStreamCreatorFunc, headers, operationListener, payloadUriConverter, writing);
			this.ContentId = contentId;
			this.Container = container;
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00005D4B File Offset: 0x00003F4B
		// (set) Token: 0x06000164 RID: 356 RVA: 0x00005D53 File Offset: 0x00003F53
		public int StatusCode
		{
			get
			{
				return this.statusCode;
			}
			set
			{
				this.message.VerifyNotCompleted();
				this.statusCode = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00005D67 File Offset: 0x00003F67
		public IEnumerable<KeyValuePair<string, string>> Headers
		{
			get
			{
				return this.message.Headers;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00005D74 File Offset: 0x00003F74
		// (set) Token: 0x06000167 RID: 359 RVA: 0x00005D7C File Offset: 0x00003F7C
		public IServiceProvider Container { get; private set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00005D85 File Offset: 0x00003F85
		internal ODataBatchOperationMessage OperationMessage
		{
			get
			{
				return this.message;
			}
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00005D8D File Offset: 0x00003F8D
		public string GetHeader(string headerName)
		{
			return this.message.GetHeader(headerName);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00005D9B File Offset: 0x00003F9B
		public void SetHeader(string headerName, string headerValue)
		{
			this.message.SetHeader(headerName, headerValue);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00005DAA File Offset: 0x00003FAA
		public Stream GetStream()
		{
			return this.message.GetStream();
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00005DB7 File Offset: 0x00003FB7
		Uri IODataPayloadUriConverter.ConvertPayloadUri(Uri baseUri, Uri payloadUri)
		{
			return this.message.ResolveUrl(baseUri, payloadUri);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00005DC8 File Offset: 0x00003FC8
		internal static ODataBatchOperationResponseMessage CreateWriteMessage(Stream outputStream, IODataBatchOperationListener operationListener, IODataPayloadUriConverter payloadUriConverter, IServiceProvider container)
		{
			Func<Stream> func = () => ODataBatchUtils.CreateBatchOperationWriteStream(outputStream, operationListener);
			return new ODataBatchOperationResponseMessage(func, null, operationListener, null, payloadUriConverter, true, container);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00005E08 File Offset: 0x00004008
		internal static ODataBatchOperationResponseMessage CreateReadMessage(ODataBatchReaderStream batchReaderStream, int statusCode, ODataBatchOperationHeaders headers, string contentId, IODataBatchOperationListener operationListener, IODataPayloadUriConverter payloadUriConverter, IServiceProvider container)
		{
			Func<Stream> func = () => ODataBatchUtils.CreateBatchOperationReadStream(batchReaderStream, headers, operationListener);
			return new ODataBatchOperationResponseMessage(func, headers, operationListener, contentId, payloadUriConverter, false, container)
			{
				statusCode = statusCode
			};
		}

		// Token: 0x040000DB RID: 219
		public readonly string ContentId;

		// Token: 0x040000DC RID: 220
		private readonly ODataBatchOperationMessage message;

		// Token: 0x040000DD RID: 221
		private int statusCode;
	}
}
