using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Data.OData
{
	// Token: 0x02000261 RID: 609
	public sealed class ODataBatchOperationResponseMessage : IODataResponseMessage, IODataUrlResolver
	{
		// Token: 0x06001300 RID: 4864 RVA: 0x000476C8 File Offset: 0x000458C8
		private ODataBatchOperationResponseMessage(Func<Stream> contentStreamCreatorFunc, ODataBatchOperationHeaders headers, IODataBatchOperationListener operationListener, IODataUrlResolver urlResolver, bool writing)
		{
			this.message = new ODataBatchOperationMessage(contentStreamCreatorFunc, headers, operationListener, urlResolver, writing);
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06001301 RID: 4865 RVA: 0x000476E2 File Offset: 0x000458E2
		// (set) Token: 0x06001302 RID: 4866 RVA: 0x000476EA File Offset: 0x000458EA
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

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06001303 RID: 4867 RVA: 0x000476FE File Offset: 0x000458FE
		public IEnumerable<KeyValuePair<string, string>> Headers
		{
			get
			{
				return this.message.Headers;
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06001304 RID: 4868 RVA: 0x0004770B File Offset: 0x0004590B
		internal ODataBatchOperationMessage OperationMessage
		{
			get
			{
				return this.message;
			}
		}

		// Token: 0x06001305 RID: 4869 RVA: 0x00047713 File Offset: 0x00045913
		public string GetHeader(string headerName)
		{
			return this.message.GetHeader(headerName);
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x00047721 File Offset: 0x00045921
		public void SetHeader(string headerName, string headerValue)
		{
			this.message.SetHeader(headerName, headerValue);
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x00047730 File Offset: 0x00045930
		public Stream GetStream()
		{
			return this.message.GetStream();
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x0004773D File Offset: 0x0004593D
		Uri IODataUrlResolver.ResolveUrl(Uri baseUri, Uri payloadUri)
		{
			return this.message.ResolveUrl(baseUri, payloadUri);
		}

		// Token: 0x06001309 RID: 4873 RVA: 0x00047768 File Offset: 0x00045968
		internal static ODataBatchOperationResponseMessage CreateWriteMessage(Stream outputStream, IODataBatchOperationListener operationListener, IODataUrlResolver urlResolver)
		{
			Func<Stream> func = () => ODataBatchUtils.CreateBatchOperationWriteStream(outputStream, operationListener);
			return new ODataBatchOperationResponseMessage(func, null, operationListener, urlResolver, true);
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x000477C8 File Offset: 0x000459C8
		internal static ODataBatchOperationResponseMessage CreateReadMessage(ODataBatchReaderStream batchReaderStream, int statusCode, ODataBatchOperationHeaders headers, IODataBatchOperationListener operationListener, IODataUrlResolver urlResolver)
		{
			Func<Stream> func = () => ODataBatchUtils.CreateBatchOperationReadStream(batchReaderStream, headers, operationListener);
			return new ODataBatchOperationResponseMessage(func, headers, operationListener, urlResolver, false)
			{
				statusCode = statusCode
			};
		}

		// Token: 0x0400071C RID: 1820
		private readonly ODataBatchOperationMessage message;

		// Token: 0x0400071D RID: 1821
		private int statusCode;
	}
}
