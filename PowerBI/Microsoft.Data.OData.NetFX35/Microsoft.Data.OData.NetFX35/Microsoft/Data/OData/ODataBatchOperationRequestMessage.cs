using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Data.OData
{
	// Token: 0x02000262 RID: 610
	public sealed class ODataBatchOperationRequestMessage : IODataRequestMessage, IODataUrlResolver
	{
		// Token: 0x0600130B RID: 4875 RVA: 0x0004781B File Offset: 0x00045A1B
		private ODataBatchOperationRequestMessage(Func<Stream> contentStreamCreatorFunc, string method, Uri requestUrl, ODataBatchOperationHeaders headers, IODataBatchOperationListener operationListener, IODataUrlResolver urlResolver, bool writing)
		{
			this.Method = method;
			this.Url = requestUrl;
			this.message = new ODataBatchOperationMessage(contentStreamCreatorFunc, headers, operationListener, urlResolver, writing);
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x0600130C RID: 4876 RVA: 0x00047845 File Offset: 0x00045A45
		public IEnumerable<KeyValuePair<string, string>> Headers
		{
			get
			{
				return this.message.Headers;
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x0600130D RID: 4877 RVA: 0x00047852 File Offset: 0x00045A52
		// (set) Token: 0x0600130E RID: 4878 RVA: 0x0004785A File Offset: 0x00045A5A
		public Uri Url { get; set; }

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x0600130F RID: 4879 RVA: 0x00047863 File Offset: 0x00045A63
		// (set) Token: 0x06001310 RID: 4880 RVA: 0x0004786B File Offset: 0x00045A6B
		public string Method { get; set; }

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06001311 RID: 4881 RVA: 0x00047874 File Offset: 0x00045A74
		internal ODataBatchOperationMessage OperationMessage
		{
			get
			{
				return this.message;
			}
		}

		// Token: 0x06001312 RID: 4882 RVA: 0x0004787C File Offset: 0x00045A7C
		public string GetHeader(string headerName)
		{
			return this.message.GetHeader(headerName);
		}

		// Token: 0x06001313 RID: 4883 RVA: 0x0004788A File Offset: 0x00045A8A
		public void SetHeader(string headerName, string headerValue)
		{
			this.message.SetHeader(headerName, headerValue);
		}

		// Token: 0x06001314 RID: 4884 RVA: 0x00047899 File Offset: 0x00045A99
		public Stream GetStream()
		{
			return this.message.GetStream();
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x000478A6 File Offset: 0x00045AA6
		Uri IODataUrlResolver.ResolveUrl(Uri baseUri, Uri payloadUri)
		{
			return this.message.ResolveUrl(baseUri, payloadUri);
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x000478D0 File Offset: 0x00045AD0
		internal static ODataBatchOperationRequestMessage CreateWriteMessage(Stream outputStream, string method, Uri requestUrl, IODataBatchOperationListener operationListener, IODataUrlResolver urlResolver)
		{
			Func<Stream> func = () => ODataBatchUtils.CreateBatchOperationWriteStream(outputStream, operationListener);
			return new ODataBatchOperationRequestMessage(func, method, requestUrl, null, operationListener, urlResolver, true);
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x00047934 File Offset: 0x00045B34
		internal static ODataBatchOperationRequestMessage CreateReadMessage(ODataBatchReaderStream batchReaderStream, string method, Uri requestUrl, ODataBatchOperationHeaders headers, IODataBatchOperationListener operationListener, IODataUrlResolver urlResolver)
		{
			Func<Stream> func = () => ODataBatchUtils.CreateBatchOperationReadStream(batchReaderStream, headers, operationListener);
			return new ODataBatchOperationRequestMessage(func, method, requestUrl, headers, operationListener, urlResolver, false);
		}

		// Token: 0x0400071E RID: 1822
		private readonly ODataBatchOperationMessage message;
	}
}
