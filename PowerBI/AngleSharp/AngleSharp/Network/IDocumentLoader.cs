using System;

namespace AngleSharp.Network
{
	// Token: 0x02000090 RID: 144
	public interface IDocumentLoader : ILoader
	{
		// Token: 0x0600046B RID: 1131
		IDownload DownloadAsync(DocumentRequest request);
	}
}
