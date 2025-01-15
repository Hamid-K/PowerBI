using System;

namespace AngleSharp.Network
{
	// Token: 0x02000095 RID: 149
	public interface IResourceLoader : ILoader
	{
		// Token: 0x06000475 RID: 1141
		IDownload DownloadAsync(ResourceRequest request);
	}
}
