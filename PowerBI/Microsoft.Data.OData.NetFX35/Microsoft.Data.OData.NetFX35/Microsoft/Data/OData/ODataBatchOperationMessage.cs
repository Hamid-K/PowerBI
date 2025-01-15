using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Microsoft.Data.OData
{
	// Token: 0x02000260 RID: 608
	internal sealed class ODataBatchOperationMessage : ODataMessage
	{
		// Token: 0x060012F7 RID: 4855 RVA: 0x00047584 File Offset: 0x00045784
		internal ODataBatchOperationMessage(Func<Stream> contentStreamCreatorFunc, ODataBatchOperationHeaders headers, IODataBatchOperationListener operationListener, IODataUrlResolver urlResolver, bool writing)
			: base(writing, false, -1L)
		{
			this.contentStreamCreatorFunc = contentStreamCreatorFunc;
			this.operationListener = operationListener;
			this.headers = headers;
			this.urlResolver = urlResolver;
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x060012F8 RID: 4856 RVA: 0x000475AE File Offset: 0x000457AE
		public override IEnumerable<KeyValuePair<string, string>> Headers
		{
			get
			{
				return this.headers ?? Enumerable.Empty<KeyValuePair<string, string>>();
			}
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x000475C0 File Offset: 0x000457C0
		public override string GetHeader(string headerName)
		{
			string text;
			if (this.headers != null && this.headers.TryGetValue(headerName, out text))
			{
				return text;
			}
			return null;
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x000475E8 File Offset: 0x000457E8
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

		// Token: 0x060012FB RID: 4859 RVA: 0x0004763C File Offset: 0x0004583C
		public override Stream GetStream()
		{
			this.VerifyNotCompleted();
			this.operationListener.BatchOperationContentStreamRequested();
			Stream stream = this.contentStreamCreatorFunc.Invoke();
			this.PartHeaderProcessingCompleted();
			return stream;
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x00047670 File Offset: 0x00045870
		internal override TInterface QueryInterface<TInterface>()
		{
			return default(TInterface);
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x00047686 File Offset: 0x00045886
		internal Uri ResolveUrl(Uri baseUri, Uri payloadUri)
		{
			ExceptionUtils.CheckArgumentNotNull<Uri>(payloadUri, "payloadUri");
			if (this.urlResolver != null)
			{
				return this.urlResolver.ResolveUrl(baseUri, payloadUri);
			}
			return null;
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x000476AA File Offset: 0x000458AA
		internal void PartHeaderProcessingCompleted()
		{
			this.contentStreamCreatorFunc = null;
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x000476B3 File Offset: 0x000458B3
		internal void VerifyNotCompleted()
		{
			if (this.contentStreamCreatorFunc == null)
			{
				throw new ODataException(Strings.ODataBatchOperationMessage_VerifyNotCompleted);
			}
		}

		// Token: 0x04000718 RID: 1816
		private readonly IODataBatchOperationListener operationListener;

		// Token: 0x04000719 RID: 1817
		private readonly IODataUrlResolver urlResolver;

		// Token: 0x0400071A RID: 1818
		private Func<Stream> contentStreamCreatorFunc;

		// Token: 0x0400071B RID: 1819
		private ODataBatchOperationHeaders headers;
	}
}
