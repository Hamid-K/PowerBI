using System;
using System.Collections.Generic;

namespace Microsoft.OData
{
	// Token: 0x0200005E RID: 94
	internal sealed class ODataBatchPayloadUriConverter : IODataPayloadUriConverter
	{
		// Token: 0x06000318 RID: 792 RVA: 0x00009503 File Offset: 0x00007703
		internal ODataBatchPayloadUriConverter(IODataPayloadUriConverter batchMessagePayloadUriConverter)
		{
			this.batchMessagePayloadUriConverter = batchMessagePayloadUriConverter;
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000319 RID: 793 RVA: 0x00009512 File Offset: 0x00007712
		internal IODataPayloadUriConverter BatchMessagePayloadUriConverter
		{
			get
			{
				return this.batchMessagePayloadUriConverter;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600031A RID: 794 RVA: 0x0000951A File Offset: 0x0000771A
		internal IEnumerable<string> ContentIdCache
		{
			get
			{
				return this.contentIdCache;
			}
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00009524 File Offset: 0x00007724
		Uri IODataPayloadUriConverter.ConvertPayloadUri(Uri baseUri, Uri payloadUri)
		{
			ExceptionUtils.CheckArgumentNotNull<Uri>(payloadUri, "payloadUri");
			if (this.contentIdCache != null && !payloadUri.IsAbsoluteUri)
			{
				string text = UriUtils.UriToString(payloadUri);
				if (text.Length > 0 && text[0] == '$')
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
			if (this.batchMessagePayloadUriConverter != null)
			{
				return this.batchMessagePayloadUriConverter.ConvertPayloadUri(baseUri, payloadUri);
			}
			return null;
		}

		// Token: 0x0600031C RID: 796 RVA: 0x000095B2 File Offset: 0x000077B2
		internal void AddContentId(string contentId)
		{
			if (this.contentIdCache == null)
			{
				this.contentIdCache = new HashSet<string>(StringComparer.Ordinal);
			}
			this.contentIdCache.Add(contentId);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x000095D9 File Offset: 0x000077D9
		internal bool ContainsContentId(string contentId)
		{
			return this.contentIdCache != null && this.contentIdCache.Contains(contentId);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x000095F1 File Offset: 0x000077F1
		internal void Reset()
		{
			if (this.contentIdCache != null)
			{
				this.contentIdCache.Clear();
			}
		}

		// Token: 0x04000168 RID: 360
		private readonly IODataPayloadUriConverter batchMessagePayloadUriConverter;

		// Token: 0x04000169 RID: 361
		private HashSet<string> contentIdCache;
	}
}
