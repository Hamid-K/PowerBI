using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.OData
{
	// Token: 0x02000031 RID: 49
	public sealed class ODataBatchOperationRequestMessage : IODataRequestMessage, IODataPayloadUriConverter, IContainerProvider
	{
		// Token: 0x06000153 RID: 339 RVA: 0x00005BD0 File Offset: 0x00003DD0
		private ODataBatchOperationRequestMessage(Func<Stream> contentStreamCreatorFunc, string method, Uri requestUrl, ODataBatchOperationHeaders headers, IODataBatchOperationListener operationListener, string contentId, IODataPayloadUriConverter payloadUriConverter, bool writing, IServiceProvider container)
		{
			this.Method = method;
			this.Url = requestUrl;
			this.ContentId = contentId;
			this.message = new ODataBatchOperationMessage(contentStreamCreatorFunc, headers, operationListener, payloadUriConverter, writing);
			this.Container = container;
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00005C0A File Offset: 0x00003E0A
		public IEnumerable<KeyValuePair<string, string>> Headers
		{
			get
			{
				return this.message.Headers;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00005C17 File Offset: 0x00003E17
		// (set) Token: 0x06000156 RID: 342 RVA: 0x00005C1F File Offset: 0x00003E1F
		public Uri Url { get; set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00005C28 File Offset: 0x00003E28
		// (set) Token: 0x06000158 RID: 344 RVA: 0x00005C30 File Offset: 0x00003E30
		public string Method { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00005C39 File Offset: 0x00003E39
		// (set) Token: 0x0600015A RID: 346 RVA: 0x00005C41 File Offset: 0x00003E41
		public IServiceProvider Container { get; private set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00005C4A File Offset: 0x00003E4A
		internal ODataBatchOperationMessage OperationMessage
		{
			get
			{
				return this.message;
			}
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00005C52 File Offset: 0x00003E52
		public string GetHeader(string headerName)
		{
			return this.message.GetHeader(headerName);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00005C60 File Offset: 0x00003E60
		public void SetHeader(string headerName, string headerValue)
		{
			this.message.SetHeader(headerName, headerValue);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00005C6F File Offset: 0x00003E6F
		public Stream GetStream()
		{
			return this.message.GetStream();
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00005C7C File Offset: 0x00003E7C
		Uri IODataPayloadUriConverter.ConvertPayloadUri(Uri baseUri, Uri payloadUri)
		{
			return this.message.ResolveUrl(baseUri, payloadUri);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00005C8C File Offset: 0x00003E8C
		internal static ODataBatchOperationRequestMessage CreateWriteMessage(Stream outputStream, string method, Uri requestUrl, IODataBatchOperationListener operationListener, IODataPayloadUriConverter payloadUriConverter, IServiceProvider container)
		{
			Func<Stream> func = () => ODataBatchUtils.CreateBatchOperationWriteStream(outputStream, operationListener);
			return new ODataBatchOperationRequestMessage(func, method, requestUrl, null, operationListener, null, payloadUriConverter, true, container);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00005CD0 File Offset: 0x00003ED0
		internal static ODataBatchOperationRequestMessage CreateReadMessage(ODataBatchReaderStream batchReaderStream, string method, Uri requestUrl, ODataBatchOperationHeaders headers, IODataBatchOperationListener operationListener, string contentId, IODataPayloadUriConverter payloadUriConverter, IServiceProvider container)
		{
			Func<Stream> func = () => ODataBatchUtils.CreateBatchOperationReadStream(batchReaderStream, headers, operationListener);
			return new ODataBatchOperationRequestMessage(func, method, requestUrl, headers, operationListener, contentId, payloadUriConverter, false, container);
		}

		// Token: 0x040000D6 RID: 214
		public readonly string ContentId;

		// Token: 0x040000D7 RID: 215
		private readonly ODataBatchOperationMessage message;
	}
}
