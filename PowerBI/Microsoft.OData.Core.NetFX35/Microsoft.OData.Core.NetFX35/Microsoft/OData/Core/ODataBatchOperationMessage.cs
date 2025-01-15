using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Microsoft.OData.Core
{
	// Token: 0x0200013E RID: 318
	internal sealed class ODataBatchOperationMessage : ODataMessage
	{
		// Token: 0x06000C16 RID: 3094 RVA: 0x0002D678 File Offset: 0x0002B878
		internal ODataBatchOperationMessage(Func<Stream> contentStreamCreatorFunc, ODataBatchOperationHeaders headers, IODataBatchOperationListener operationListener, IODataUrlResolver urlResolver, bool writing)
			: base(writing, false, -1L)
		{
			this.contentStreamCreatorFunc = contentStreamCreatorFunc;
			this.operationListener = operationListener;
			this.headers = headers;
			this.urlResolver = urlResolver;
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000C17 RID: 3095 RVA: 0x0002D6A2 File Offset: 0x0002B8A2
		public override IEnumerable<KeyValuePair<string, string>> Headers
		{
			get
			{
				return this.headers ?? Enumerable.Empty<KeyValuePair<string, string>>();
			}
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x0002D6B4 File Offset: 0x0002B8B4
		public override string GetHeader(string headerName)
		{
			string text;
			if (this.headers != null && this.headers.TryGetValue(headerName, out text))
			{
				return text;
			}
			return null;
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x0002D6DC File Offset: 0x0002B8DC
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

		// Token: 0x06000C1A RID: 3098 RVA: 0x0002D730 File Offset: 0x0002B930
		public override Stream GetStream()
		{
			this.VerifyNotCompleted();
			this.operationListener.BatchOperationContentStreamRequested();
			Stream stream = this.contentStreamCreatorFunc.Invoke();
			this.PartHeaderProcessingCompleted();
			return stream;
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x0002D764 File Offset: 0x0002B964
		internal override TInterface QueryInterface<TInterface>()
		{
			return default(TInterface);
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x0002D77A File Offset: 0x0002B97A
		internal Uri ResolveUrl(Uri baseUri, Uri payloadUri)
		{
			ExceptionUtils.CheckArgumentNotNull<Uri>(payloadUri, "payloadUri");
			if (this.urlResolver != null)
			{
				return this.urlResolver.ResolveUrl(baseUri, payloadUri);
			}
			return null;
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x0002D79E File Offset: 0x0002B99E
		internal void PartHeaderProcessingCompleted()
		{
			this.contentStreamCreatorFunc = null;
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x0002D7A7 File Offset: 0x0002B9A7
		internal void VerifyNotCompleted()
		{
			if (this.contentStreamCreatorFunc == null)
			{
				throw new ODataException(Strings.ODataBatchOperationMessage_VerifyNotCompleted);
			}
		}

		// Token: 0x04000505 RID: 1285
		private readonly IODataBatchOperationListener operationListener;

		// Token: 0x04000506 RID: 1286
		private readonly IODataUrlResolver urlResolver;

		// Token: 0x04000507 RID: 1287
		private Func<Stream> contentStreamCreatorFunc;

		// Token: 0x04000508 RID: 1288
		private ODataBatchOperationHeaders headers;
	}
}
