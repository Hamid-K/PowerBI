using System;
using System.Collections.Generic;

namespace Microsoft.Data.OData
{
	// Token: 0x020001E7 RID: 487
	internal sealed class ODataBatchUrlResolver : IODataUrlResolver
	{
		// Token: 0x06000E35 RID: 3637 RVA: 0x00033114 File Offset: 0x00031314
		internal ODataBatchUrlResolver(IODataUrlResolver batchMessageUrlResolver)
		{
			this.batchMessageUrlResolver = batchMessageUrlResolver;
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000E36 RID: 3638 RVA: 0x00033123 File Offset: 0x00031323
		internal IODataUrlResolver BatchMessageUrlResolver
		{
			get
			{
				return this.batchMessageUrlResolver;
			}
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x0003312C File Offset: 0x0003132C
		Uri IODataUrlResolver.ResolveUrl(Uri baseUri, Uri payloadUri)
		{
			ExceptionUtils.CheckArgumentNotNull<Uri>(payloadUri, "payloadUri");
			if (this.contentIdCache != null && !payloadUri.IsAbsoluteUri)
			{
				string text = UriUtilsCommon.UriToString(payloadUri);
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

		// Token: 0x06000E38 RID: 3640 RVA: 0x000331B9 File Offset: 0x000313B9
		internal void AddContentId(string contentId)
		{
			if (this.contentIdCache == null)
			{
				this.contentIdCache = new HashSet<string>(StringComparer.Ordinal);
			}
			this.contentIdCache.Add(contentId);
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x000331E0 File Offset: 0x000313E0
		internal bool ContainsContentId(string contentId)
		{
			return this.contentIdCache != null && this.contentIdCache.Contains(contentId);
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x000331F8 File Offset: 0x000313F8
		internal void Reset()
		{
			if (this.contentIdCache != null)
			{
				this.contentIdCache.Clear();
			}
		}

		// Token: 0x04000534 RID: 1332
		private readonly IODataUrlResolver batchMessageUrlResolver;

		// Token: 0x04000535 RID: 1333
		private HashSet<string> contentIdCache;
	}
}
