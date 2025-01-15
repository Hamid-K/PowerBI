using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Microsoft.OData
{
	// Token: 0x02000057 RID: 87
	public sealed class ODataBatchOperationRequestMessage : IODataRequestMessageAsync, IODataRequestMessage, IODataPayloadUriConverter, IContainerProvider
	{
		// Token: 0x060002BF RID: 703 RVA: 0x00008728 File Offset: 0x00006928
		internal ODataBatchOperationRequestMessage(Func<Stream> contentStreamCreatorFunc, string method, Uri requestUrl, ODataBatchOperationHeaders headers, IODataStreamListener operationListener, string contentId, IODataPayloadUriConverter payloadUriConverter, bool writing, IServiceProvider container, IEnumerable<string> dependsOnIds, string groupId)
		{
			this.Method = method;
			this.Url = requestUrl;
			this.ContentId = contentId;
			this.groupId = groupId;
			this.message = new ODataBatchOperationMessage(contentStreamCreatorFunc, headers, operationListener, payloadUriConverter, writing);
			this.Container = container;
			this.dependsOnIds = ((dependsOnIds == null) ? new List<string>() : new List<string>(dependsOnIds));
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x0000878D File Offset: 0x0000698D
		public IEnumerable<KeyValuePair<string, string>> Headers
		{
			get
			{
				return this.message.Headers;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x0000879A File Offset: 0x0000699A
		// (set) Token: 0x060002C2 RID: 706 RVA: 0x000087A2 File Offset: 0x000069A2
		public Uri Url { get; set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x000087AB File Offset: 0x000069AB
		// (set) Token: 0x060002C4 RID: 708 RVA: 0x000087B3 File Offset: 0x000069B3
		public string Method { get; set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x000087BC File Offset: 0x000069BC
		// (set) Token: 0x060002C6 RID: 710 RVA: 0x000087C4 File Offset: 0x000069C4
		public IServiceProvider Container { get; private set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x000087CD File Offset: 0x000069CD
		public string GroupId
		{
			get
			{
				return this.groupId;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x000087D5 File Offset: 0x000069D5
		public IEnumerable<string> DependsOnIds
		{
			get
			{
				return this.dependsOnIds;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x000087DD File Offset: 0x000069DD
		internal ODataBatchOperationMessage OperationMessage
		{
			get
			{
				return this.message;
			}
		}

		// Token: 0x060002CA RID: 714 RVA: 0x000087E5 File Offset: 0x000069E5
		public string GetHeader(string headerName)
		{
			return this.message.GetHeader(headerName);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x000087F3 File Offset: 0x000069F3
		public void SetHeader(string headerName, string headerValue)
		{
			this.message.SetHeader(headerName, headerValue);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00008802 File Offset: 0x00006A02
		public Stream GetStream()
		{
			return this.message.GetStream();
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000880F File Offset: 0x00006A0F
		public Task<Stream> GetStreamAsync()
		{
			return this.message.GetStreamAsync();
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000881C File Offset: 0x00006A1C
		Uri IODataPayloadUriConverter.ConvertPayloadUri(Uri baseUri, Uri payloadUri)
		{
			return this.message.ResolveUrl(baseUri, payloadUri);
		}

		// Token: 0x0400013F RID: 319
		public readonly string ContentId;

		// Token: 0x04000140 RID: 320
		private readonly string groupId;

		// Token: 0x04000141 RID: 321
		private readonly ODataBatchOperationMessage message;

		// Token: 0x04000142 RID: 322
		private readonly List<string> dependsOnIds;
	}
}
