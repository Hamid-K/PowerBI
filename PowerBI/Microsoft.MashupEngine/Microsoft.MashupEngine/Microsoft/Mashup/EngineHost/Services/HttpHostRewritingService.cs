using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x020019EA RID: 6634
	public class HttpHostRewritingService : IHttpUriRewritingService
	{
		// Token: 0x0600A7E5 RID: 42981 RVA: 0x0022B752 File Offset: 0x00229952
		public HttpHostRewritingService(Uri[,] map)
		{
			this.map = map;
		}

		// Token: 0x17002ABD RID: 10941
		// (get) Token: 0x0600A7E6 RID: 42982 RVA: 0x0022B761 File Offset: 0x00229961
		public Uri[,] Map
		{
			get
			{
				return this.map;
			}
		}

		// Token: 0x0600A7E7 RID: 42983 RVA: 0x0022B769 File Offset: 0x00229969
		public bool TryRewriteRequestUri(Uri requestUri, out Uri rewrittenUri)
		{
			return this.TryRewrite(requestUri, 0, 1, out rewrittenUri);
		}

		// Token: 0x0600A7E8 RID: 42984 RVA: 0x0022B775 File Offset: 0x00229975
		public bool TryRewriteResponseUri(Uri responseUri, out Uri rewrittenUri)
		{
			return this.TryRewrite(responseUri, 1, 0, out rewrittenUri);
		}

		// Token: 0x0600A7E9 RID: 42985 RVA: 0x0022B784 File Offset: 0x00229984
		private bool TryRewrite(Uri originalUri, int idx0, int idx1, out Uri rewrittenUri)
		{
			if (this.map != null)
			{
				for (int i = 0; i < this.map.GetLength(0); i++)
				{
					Uri uri = this.map[i, idx0];
					if (string.Equals(originalUri.Scheme, uri.Scheme, StringComparison.OrdinalIgnoreCase) && string.Equals(originalUri.Host, uri.Host, StringComparison.OrdinalIgnoreCase) && originalUri.Port == uri.Port)
					{
						UriBuilder uriBuilder = new UriBuilder(originalUri);
						Uri uri2 = this.map[i, idx1];
						uriBuilder.Scheme = uri2.Scheme;
						uriBuilder.Host = uri2.Host;
						uriBuilder.Port = uri2.Port;
						rewrittenUri = uriBuilder.Uri;
						return true;
					}
				}
			}
			rewrittenUri = null;
			return false;
		}

		// Token: 0x0400576D RID: 22381
		private readonly Uri[,] map;
	}
}
