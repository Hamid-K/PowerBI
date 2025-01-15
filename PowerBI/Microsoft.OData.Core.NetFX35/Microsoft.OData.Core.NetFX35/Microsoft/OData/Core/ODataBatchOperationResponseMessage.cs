using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.OData.Core
{
	// Token: 0x02000144 RID: 324
	public sealed class ODataBatchOperationResponseMessage : IODataResponseMessage, IODataUrlResolver
	{
		// Token: 0x06000C40 RID: 3136 RVA: 0x0002DABF File Offset: 0x0002BCBF
		private ODataBatchOperationResponseMessage(Func<Stream> contentStreamCreatorFunc, ODataBatchOperationHeaders headers, IODataBatchOperationListener operationListener, string contentId, IODataUrlResolver urlResolver, bool writing)
		{
			this.message = new ODataBatchOperationMessage(contentStreamCreatorFunc, headers, operationListener, urlResolver, writing);
			this.ContentId = contentId;
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000C41 RID: 3137 RVA: 0x0002DAE1 File Offset: 0x0002BCE1
		// (set) Token: 0x06000C42 RID: 3138 RVA: 0x0002DAE9 File Offset: 0x0002BCE9
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

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000C43 RID: 3139 RVA: 0x0002DAFD File Offset: 0x0002BCFD
		public IEnumerable<KeyValuePair<string, string>> Headers
		{
			get
			{
				return this.message.Headers;
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000C44 RID: 3140 RVA: 0x0002DB0A File Offset: 0x0002BD0A
		internal ODataBatchOperationMessage OperationMessage
		{
			get
			{
				return this.message;
			}
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x0002DB12 File Offset: 0x0002BD12
		public string GetHeader(string headerName)
		{
			return this.message.GetHeader(headerName);
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x0002DB20 File Offset: 0x0002BD20
		public void SetHeader(string headerName, string headerValue)
		{
			this.message.SetHeader(headerName, headerValue);
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x0002DB2F File Offset: 0x0002BD2F
		public Stream GetStream()
		{
			return this.message.GetStream();
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x0002DB3C File Offset: 0x0002BD3C
		Uri IODataUrlResolver.ResolveUrl(Uri baseUri, Uri payloadUri)
		{
			return this.message.ResolveUrl(baseUri, payloadUri);
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x0002DB68 File Offset: 0x0002BD68
		internal static ODataBatchOperationResponseMessage CreateWriteMessage(Stream outputStream, IODataBatchOperationListener operationListener, IODataUrlResolver urlResolver)
		{
			Func<Stream> func = () => ODataBatchUtils.CreateBatchOperationWriteStream(outputStream, operationListener);
			return new ODataBatchOperationResponseMessage(func, null, operationListener, null, urlResolver, true);
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x0002DBC8 File Offset: 0x0002BDC8
		internal static ODataBatchOperationResponseMessage CreateReadMessage(ODataBatchReaderStream batchReaderStream, int statusCode, ODataBatchOperationHeaders headers, string contentId, IODataBatchOperationListener operationListener, IODataUrlResolver urlResolver)
		{
			Func<Stream> func = () => ODataBatchUtils.CreateBatchOperationReadStream(batchReaderStream, headers, operationListener);
			return new ODataBatchOperationResponseMessage(func, headers, operationListener, contentId, urlResolver, false)
			{
				statusCode = statusCode
			};
		}

		// Token: 0x04000511 RID: 1297
		public readonly string ContentId;

		// Token: 0x04000512 RID: 1298
		private readonly ODataBatchOperationMessage message;

		// Token: 0x04000513 RID: 1299
		private int statusCode;
	}
}
