using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.OData
{
	// Token: 0x02000056 RID: 86
	internal sealed class ODataBatchOperationMessage : ODataMessage
	{
		// Token: 0x060002B5 RID: 693 RVA: 0x00008580 File Offset: 0x00006780
		internal ODataBatchOperationMessage(Func<Stream> contentStreamCreatorFunc, ODataBatchOperationHeaders headers, IODataStreamListener operationListener, IODataPayloadUriConverter payloadUriConverter, bool writing)
			: base(writing, false, -1L)
		{
			this.contentStreamCreatorFunc = contentStreamCreatorFunc;
			this.operationListener = operationListener;
			this.headers = headers;
			this.payloadUriConverter = payloadUriConverter;
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x000085AC File Offset: 0x000067AC
		public override IEnumerable<KeyValuePair<string, string>> Headers
		{
			get
			{
				IEnumerable<KeyValuePair<string, string>> enumerable = this.headers;
				return enumerable ?? Enumerable.Empty<KeyValuePair<string, string>>();
			}
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x000085CC File Offset: 0x000067CC
		public override string GetHeader(string headerName)
		{
			string text;
			if (this.headers != null && this.headers.TryGetValue(headerName, out text))
			{
				return text;
			}
			return null;
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x000085F4 File Offset: 0x000067F4
		public override void SetHeader(string headerName, string headerValue)
		{
			this.VerifyNotCompleted();
			base.VerifyCanSetHeader();
			if (headerValue == null)
			{
				if (this.headers != null)
				{
					this.headers.Remove(headerName);
					return;
				}
			}
			else
			{
				if (this.headers == null)
				{
					this.headers = new ODataBatchOperationHeaders();
				}
				this.headers[headerName] = headerValue;
			}
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00008648 File Offset: 0x00006848
		public override Stream GetStream()
		{
			this.VerifyNotCompleted();
			this.operationListener.StreamRequested();
			Stream stream = this.contentStreamCreatorFunc();
			this.PartHeaderProcessingCompleted();
			return stream;
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000867C File Offset: 0x0000687C
		public override Task<Stream> GetStreamAsync()
		{
			this.VerifyNotCompleted();
			Task task2 = this.operationListener.StreamRequestedAsync();
			Stream contentStream = this.contentStreamCreatorFunc();
			this.PartHeaderProcessingCompleted();
			return task2.FollowOnSuccessWith((Task task) => contentStream);
		}

		// Token: 0x060002BB RID: 699 RVA: 0x000086CC File Offset: 0x000068CC
		internal override TInterface QueryInterface<TInterface>()
		{
			return default(TInterface);
		}

		// Token: 0x060002BC RID: 700 RVA: 0x000086E2 File Offset: 0x000068E2
		internal Uri ResolveUrl(Uri baseUri, Uri payloadUri)
		{
			ExceptionUtils.CheckArgumentNotNull<Uri>(payloadUri, "payloadUri");
			if (this.payloadUriConverter != null)
			{
				return this.payloadUriConverter.ConvertPayloadUri(baseUri, payloadUri);
			}
			return null;
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00008707 File Offset: 0x00006907
		internal void PartHeaderProcessingCompleted()
		{
			this.contentStreamCreatorFunc = null;
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00008710 File Offset: 0x00006910
		internal void VerifyNotCompleted()
		{
			if (this.contentStreamCreatorFunc == null)
			{
				throw new ODataException(Strings.ODataBatchOperationMessage_VerifyNotCompleted);
			}
		}

		// Token: 0x0400013B RID: 315
		private readonly IODataStreamListener operationListener;

		// Token: 0x0400013C RID: 316
		private readonly IODataPayloadUriConverter payloadUriConverter;

		// Token: 0x0400013D RID: 317
		private Func<Stream> contentStreamCreatorFunc;

		// Token: 0x0400013E RID: 318
		private ODataBatchOperationHeaders headers;
	}
}
