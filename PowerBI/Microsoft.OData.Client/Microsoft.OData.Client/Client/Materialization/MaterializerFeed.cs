using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.Client.Materialization
{
	// Token: 0x02000110 RID: 272
	internal struct MaterializerFeed
	{
		// Token: 0x06000B9A RID: 2970 RVA: 0x0002BBB4 File Offset: 0x00029DB4
		private MaterializerFeed(ODataResourceSet feed, IEnumerable<ODataResource> entries)
		{
			this.feed = feed;
			this.entries = entries;
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000B9B RID: 2971 RVA: 0x0002BBC4 File Offset: 0x00029DC4
		public ODataResourceSet Feed
		{
			get
			{
				return this.feed;
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000B9C RID: 2972 RVA: 0x0002BBCC File Offset: 0x00029DCC
		public IEnumerable<ODataResource> Entries
		{
			get
			{
				return this.entries;
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000B9D RID: 2973 RVA: 0x0002BBD4 File Offset: 0x00029DD4
		public Uri NextPageLink
		{
			get
			{
				return this.feed.NextPageLink;
			}
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x0002BBE1 File Offset: 0x00029DE1
		public static MaterializerFeed CreateFeed(ODataResourceSet feed, IEnumerable<ODataResource> entries)
		{
			if (entries == null)
			{
				entries = Enumerable.Empty<ODataResource>();
			}
			else
			{
				feed.SetAnnotation(entries);
			}
			return new MaterializerFeed(feed, entries);
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x0002BC00 File Offset: 0x00029E00
		public static MaterializerFeed GetFeed(ODataResourceSet feed)
		{
			IEnumerable<ODataResource> annotation = feed.GetAnnotation<IEnumerable<ODataResource>>();
			return new MaterializerFeed(feed, annotation);
		}

		// Token: 0x0400064A RID: 1610
		private readonly ODataResourceSet feed;

		// Token: 0x0400064B RID: 1611
		private readonly IEnumerable<ODataResource> entries;
	}
}
