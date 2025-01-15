using System;

namespace Microsoft.Owin.StaticFiles.ContentTypes
{
	// Token: 0x0200001C RID: 28
	public interface IContentTypeProvider
	{
		// Token: 0x06000089 RID: 137
		bool TryGetContentType(string subpath, out string contentType);
	}
}
