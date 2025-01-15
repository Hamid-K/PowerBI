using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Microsoft.OData
{
	// Token: 0x0200002F RID: 47
	internal sealed class ODataBatchOperationMessage : ODataMessage
	{
		// Token: 0x0600013E RID: 318 RVA: 0x00005A58 File Offset: 0x00003C58
		internal ODataBatchOperationMessage(Func<Stream> contentStreamCreatorFunc, ODataBatchOperationHeaders headers, IODataBatchOperationListener operationListener, IODataPayloadUriConverter payloadUriConverter, bool writing)
			: base(writing, false, -1L)
		{
			this.contentStreamCreatorFunc = contentStreamCreatorFunc;
			this.operationListener = operationListener;
			this.headers = headers;
			this.payloadUriConverter = payloadUriConverter;
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00005A84 File Offset: 0x00003C84
		public override IEnumerable<KeyValuePair<string, string>> Headers
		{
			get
			{
				IEnumerable<KeyValuePair<string, string>> enumerable = this.headers;
				return enumerable ?? Enumerable.Empty<KeyValuePair<string, string>>();
			}
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00005AA4 File Offset: 0x00003CA4
		public override string GetHeader(string headerName)
		{
			string text;
			if (this.headers != null && this.headers.TryGetValue(headerName, out text))
			{
				return text;
			}
			return null;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00005ACC File Offset: 0x00003CCC
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

		// Token: 0x06000142 RID: 322 RVA: 0x00005B20 File Offset: 0x00003D20
		public override Stream GetStream()
		{
			this.VerifyNotCompleted();
			this.operationListener.BatchOperationContentStreamRequested();
			Stream stream = this.contentStreamCreatorFunc.Invoke();
			this.PartHeaderProcessingCompleted();
			return stream;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00005B54 File Offset: 0x00003D54
		internal override TInterface QueryInterface<TInterface>()
		{
			return default(TInterface);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00005B6A File Offset: 0x00003D6A
		internal Uri ResolveUrl(Uri baseUri, Uri payloadUri)
		{
			ExceptionUtils.CheckArgumentNotNull<Uri>(payloadUri, "payloadUri");
			if (this.payloadUriConverter != null)
			{
				return this.payloadUriConverter.ConvertPayloadUri(baseUri, payloadUri);
			}
			return null;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00005B8F File Offset: 0x00003D8F
		internal void PartHeaderProcessingCompleted()
		{
			this.contentStreamCreatorFunc = null;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00005B98 File Offset: 0x00003D98
		internal void VerifyNotCompleted()
		{
			if (this.contentStreamCreatorFunc == null)
			{
				throw new ODataException(Strings.ODataBatchOperationMessage_VerifyNotCompleted);
			}
		}

		// Token: 0x040000D1 RID: 209
		private readonly IODataBatchOperationListener operationListener;

		// Token: 0x040000D2 RID: 210
		private readonly IODataPayloadUriConverter payloadUriConverter;

		// Token: 0x040000D3 RID: 211
		private Func<Stream> contentStreamCreatorFunc;

		// Token: 0x040000D4 RID: 212
		private ODataBatchOperationHeaders headers;
	}
}
