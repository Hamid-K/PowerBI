using System;
using System.Collections.Generic;

namespace AngleSharp.Network.Default
{
	// Token: 0x020000AB RID: 171
	public class DocumentLoader : BaseLoader, IDocumentLoader, ILoader
	{
		// Token: 0x0600050D RID: 1293 RVA: 0x0001F854 File Offset: 0x0001DA54
		public DocumentLoader(IBrowsingContext context, Predicate<IRequest> filter = null)
			: base(context, filter)
		{
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0001F860 File Offset: 0x0001DA60
		public virtual IDownload DownloadAsync(DocumentRequest request)
		{
			Request request2 = new Request
			{
				Address = request.Target,
				Content = request.Body,
				Method = request.Method
			};
			foreach (KeyValuePair<string, string> keyValuePair in request.Headers)
			{
				request2.Headers[keyValuePair.Key] = keyValuePair.Value;
			}
			return this.DownloadAsync(request2, request.Source);
		}
	}
}
