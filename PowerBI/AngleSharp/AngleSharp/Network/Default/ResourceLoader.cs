using System;
using System.Collections.Generic;
using System.IO;

namespace AngleSharp.Network.Default
{
	// Token: 0x020000AE RID: 174
	public class ResourceLoader : BaseLoader, IResourceLoader, ILoader
	{
		// Token: 0x06000521 RID: 1313 RVA: 0x0001F854 File Offset: 0x0001DA54
		public ResourceLoader(IBrowsingContext context, Predicate<IRequest> filter = null)
			: base(context, filter)
		{
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x0001FACC File Offset: 0x0001DCCC
		public virtual IDownload DownloadAsync(ResourceRequest request)
		{
			Request request2 = new Request
			{
				Address = request.Target,
				Content = Stream.Null,
				Method = HttpMethod.Get,
				Headers = new Dictionary<string, string> { 
				{
					HeaderNames.Referer,
					request.Source.Owner.DocumentUri
				} }
			};
			string cookie = this.GetCookie(request.Target);
			if (cookie != null)
			{
				request2.Headers[HeaderNames.Cookie] = cookie;
			}
			return this.DownloadAsync(request2, request.Source);
		}
	}
}
