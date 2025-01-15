using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Microsoft.OData
{
	// Token: 0x02000058 RID: 88
	public sealed class ODataBatchOperationResponseMessage : IODataResponseMessageAsync, IODataResponseMessage, IODataPayloadUriConverter, IContainerProvider
	{
		// Token: 0x060002CF RID: 719 RVA: 0x0000882B File Offset: 0x00006A2B
		internal ODataBatchOperationResponseMessage(Func<Stream> contentStreamCreatorFunc, ODataBatchOperationHeaders headers, IODataStreamListener operationListener, string contentId, IODataPayloadUriConverter payloadUriConverter, bool writing, IServiceProvider container, string groupId)
		{
			this.message = new ODataBatchOperationMessage(contentStreamCreatorFunc, headers, operationListener, payloadUriConverter, writing);
			this.ContentId = contentId;
			this.Container = container;
			this.GroupId = groupId;
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x0000885D File Offset: 0x00006A5D
		// (set) Token: 0x060002D1 RID: 721 RVA: 0x00008865 File Offset: 0x00006A65
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

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x00008879 File Offset: 0x00006A79
		public IEnumerable<KeyValuePair<string, string>> Headers
		{
			get
			{
				return this.message.Headers;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x00008886 File Offset: 0x00006A86
		// (set) Token: 0x060002D4 RID: 724 RVA: 0x0000888E File Offset: 0x00006A8E
		public IServiceProvider Container { get; private set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x00008897 File Offset: 0x00006A97
		public string GroupId { get; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x0000889F File Offset: 0x00006A9F
		internal ODataBatchOperationMessage OperationMessage
		{
			get
			{
				return this.message;
			}
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x000088A7 File Offset: 0x00006AA7
		public string GetHeader(string headerName)
		{
			return this.message.GetHeader(headerName);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x000088B5 File Offset: 0x00006AB5
		public void SetHeader(string headerName, string headerValue)
		{
			this.message.SetHeader(headerName, headerValue);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x000088C4 File Offset: 0x00006AC4
		public Stream GetStream()
		{
			return this.message.GetStream();
		}

		// Token: 0x060002DA RID: 730 RVA: 0x000088D1 File Offset: 0x00006AD1
		public Task<Stream> GetStreamAsync()
		{
			return this.message.GetStreamAsync();
		}

		// Token: 0x060002DB RID: 731 RVA: 0x000088DE File Offset: 0x00006ADE
		Uri IODataPayloadUriConverter.ConvertPayloadUri(Uri baseUri, Uri payloadUri)
		{
			return this.message.ResolveUrl(baseUri, payloadUri);
		}

		// Token: 0x04000146 RID: 326
		public readonly string ContentId;

		// Token: 0x04000147 RID: 327
		private readonly ODataBatchOperationMessage message;

		// Token: 0x04000148 RID: 328
		private int statusCode;
	}
}
