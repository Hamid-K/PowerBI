using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.OData.Core
{
	// Token: 0x02000143 RID: 323
	public sealed class ODataBatchOperationRequestMessage : IODataRequestMessage, IODataUrlResolver
	{
		// Token: 0x06000C33 RID: 3123 RVA: 0x0002D94D File Offset: 0x0002BB4D
		private ODataBatchOperationRequestMessage(Func<Stream> contentStreamCreatorFunc, string method, Uri requestUrl, ODataBatchOperationHeaders headers, IODataBatchOperationListener operationListener, string contentId, IODataUrlResolver urlResolver, bool writing)
		{
			this.Method = method;
			this.Url = requestUrl;
			this.ContentId = contentId;
			this.message = new ODataBatchOperationMessage(contentStreamCreatorFunc, headers, operationListener, urlResolver, writing);
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000C34 RID: 3124 RVA: 0x0002D97F File Offset: 0x0002BB7F
		public IEnumerable<KeyValuePair<string, string>> Headers
		{
			get
			{
				return this.message.Headers;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000C35 RID: 3125 RVA: 0x0002D98C File Offset: 0x0002BB8C
		// (set) Token: 0x06000C36 RID: 3126 RVA: 0x0002D994 File Offset: 0x0002BB94
		public Uri Url { get; set; }

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000C37 RID: 3127 RVA: 0x0002D99D File Offset: 0x0002BB9D
		// (set) Token: 0x06000C38 RID: 3128 RVA: 0x0002D9A5 File Offset: 0x0002BBA5
		public string Method { get; set; }

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000C39 RID: 3129 RVA: 0x0002D9AE File Offset: 0x0002BBAE
		internal ODataBatchOperationMessage OperationMessage
		{
			get
			{
				return this.message;
			}
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x0002D9B6 File Offset: 0x0002BBB6
		public string GetHeader(string headerName)
		{
			return this.message.GetHeader(headerName);
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x0002D9C4 File Offset: 0x0002BBC4
		public void SetHeader(string headerName, string headerValue)
		{
			this.message.SetHeader(headerName, headerValue);
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x0002D9D3 File Offset: 0x0002BBD3
		public Stream GetStream()
		{
			return this.message.GetStream();
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x0002D9E0 File Offset: 0x0002BBE0
		Uri IODataUrlResolver.ResolveUrl(Uri baseUri, Uri payloadUri)
		{
			return this.message.ResolveUrl(baseUri, payloadUri);
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x0002DA0C File Offset: 0x0002BC0C
		internal static ODataBatchOperationRequestMessage CreateWriteMessage(Stream outputStream, string method, Uri requestUrl, IODataBatchOperationListener operationListener, IODataUrlResolver urlResolver)
		{
			Func<Stream> func = () => ODataBatchUtils.CreateBatchOperationWriteStream(outputStream, operationListener);
			return new ODataBatchOperationRequestMessage(func, method, requestUrl, null, operationListener, null, urlResolver, true);
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x0002DA70 File Offset: 0x0002BC70
		internal static ODataBatchOperationRequestMessage CreateReadMessage(ODataBatchReaderStream batchReaderStream, string method, Uri requestUrl, ODataBatchOperationHeaders headers, IODataBatchOperationListener operationListener, string contentId, IODataUrlResolver urlResolver)
		{
			Func<Stream> func = () => ODataBatchUtils.CreateBatchOperationReadStream(batchReaderStream, headers, operationListener);
			return new ODataBatchOperationRequestMessage(func, method, requestUrl, headers, operationListener, contentId, urlResolver, false);
		}

		// Token: 0x0400050D RID: 1293
		public readonly string ContentId;

		// Token: 0x0400050E RID: 1294
		private readonly ODataBatchOperationMessage message;
	}
}
