using System;
using System.Collections.Generic;

namespace Microsoft.OData.Core
{
	// Token: 0x0200014C RID: 332
	internal sealed class ODataBatchUrlResolver : IODataUrlResolver
	{
		// Token: 0x06000C94 RID: 3220 RVA: 0x0002F657 File Offset: 0x0002D857
		internal ODataBatchUrlResolver(IODataUrlResolver batchMessageUrlResolver)
		{
			this.batchMessageUrlResolver = batchMessageUrlResolver;
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000C95 RID: 3221 RVA: 0x0002F666 File Offset: 0x0002D866
		internal IODataUrlResolver BatchMessageUrlResolver
		{
			get
			{
				return this.batchMessageUrlResolver;
			}
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x0002F670 File Offset: 0x0002D870
		Uri IODataUrlResolver.ResolveUrl(Uri baseUri, Uri payloadUri)
		{
			ExceptionUtils.CheckArgumentNotNull<Uri>(payloadUri, "payloadUri");
			if (this.contentIdCache != null && !payloadUri.IsAbsoluteUri)
			{
				string text = UriUtils.UriToString(payloadUri);
				if (text.Length > 0 && text.get_Chars(0) == '$')
				{
					int num = text.IndexOf('/', 1);
					string text2;
					if (num > 0)
					{
						text2 = text.Substring(1, num - 1);
					}
					else
					{
						text2 = text.Substring(1);
					}
					if (this.contentIdCache.Contains(text2))
					{
						return payloadUri;
					}
				}
			}
			if (this.batchMessageUrlResolver != null)
			{
				return this.batchMessageUrlResolver.ResolveUrl(baseUri, payloadUri);
			}
			return null;
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x0002F6FD File Offset: 0x0002D8FD
		internal void AddContentId(string contentId)
		{
			if (this.contentIdCache == null)
			{
				this.contentIdCache = new HashSet<string>(StringComparer.Ordinal);
			}
			this.contentIdCache.Add(contentId);
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x0002F724 File Offset: 0x0002D924
		internal bool ContainsContentId(string contentId)
		{
			return this.contentIdCache != null && this.contentIdCache.Contains(contentId);
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x0002F73C File Offset: 0x0002D93C
		internal void Reset()
		{
			if (this.contentIdCache != null)
			{
				this.contentIdCache.Clear();
			}
		}

		// Token: 0x0400053E RID: 1342
		private readonly IODataUrlResolver batchMessageUrlResolver;

		// Token: 0x0400053F RID: 1343
		private HashSet<string> contentIdCache;
	}
}
