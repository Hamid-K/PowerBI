using System;
using System.Collections.Generic;

namespace Microsoft.OData
{
	// Token: 0x0200003A RID: 58
	internal sealed class ODataBatchPayloadUriConverter : IODataPayloadUriConverter
	{
		// Token: 0x060001BC RID: 444 RVA: 0x000077EF File Offset: 0x000059EF
		internal ODataBatchPayloadUriConverter(IODataPayloadUriConverter batchMessagePayloadUriConverter)
		{
			this.batchMessagePayloadUriConverter = batchMessagePayloadUriConverter;
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001BD RID: 445 RVA: 0x000077FE File Offset: 0x000059FE
		internal IODataPayloadUriConverter BatchMessagePayloadUriConverter
		{
			get
			{
				return this.batchMessagePayloadUriConverter;
			}
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00007808 File Offset: 0x00005A08
		Uri IODataPayloadUriConverter.ConvertPayloadUri(Uri baseUri, Uri payloadUri)
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
			if (this.batchMessagePayloadUriConverter != null)
			{
				return this.batchMessagePayloadUriConverter.ConvertPayloadUri(baseUri, payloadUri);
			}
			return null;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00007896 File Offset: 0x00005A96
		internal void AddContentId(string contentId)
		{
			if (this.contentIdCache == null)
			{
				this.contentIdCache = new HashSet<string>(StringComparer.Ordinal);
			}
			this.contentIdCache.Add(contentId);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x000078BD File Offset: 0x00005ABD
		internal bool ContainsContentId(string contentId)
		{
			return this.contentIdCache != null && this.contentIdCache.Contains(contentId);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x000078D5 File Offset: 0x00005AD5
		internal void Reset()
		{
			if (this.contentIdCache != null)
			{
				this.contentIdCache.Clear();
			}
		}

		// Token: 0x04000107 RID: 263
		private readonly IODataPayloadUriConverter batchMessagePayloadUriConverter;

		// Token: 0x04000108 RID: 264
		private HashSet<string> contentIdCache;
	}
}
