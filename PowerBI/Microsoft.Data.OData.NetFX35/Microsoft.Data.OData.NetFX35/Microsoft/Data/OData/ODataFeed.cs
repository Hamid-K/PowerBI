using System;
using System.Collections.Generic;

namespace Microsoft.Data.OData
{
	// Token: 0x020002A8 RID: 680
	public sealed class ODataFeed : ODataItem
	{
		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x060015AE RID: 5550 RVA: 0x0004ECC4 File Offset: 0x0004CEC4
		// (set) Token: 0x060015AF RID: 5551 RVA: 0x0004ECCC File Offset: 0x0004CECC
		public long? Count { get; set; }

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x060015B0 RID: 5552 RVA: 0x0004ECD5 File Offset: 0x0004CED5
		// (set) Token: 0x060015B1 RID: 5553 RVA: 0x0004ECDD File Offset: 0x0004CEDD
		public string Id { get; set; }

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x060015B2 RID: 5554 RVA: 0x0004ECE6 File Offset: 0x0004CEE6
		// (set) Token: 0x060015B3 RID: 5555 RVA: 0x0004ECEE File Offset: 0x0004CEEE
		public Uri NextPageLink
		{
			get
			{
				return this.nextPageLink;
			}
			set
			{
				if (this.DeltaLink != null)
				{
					throw new ODataException(Strings.ODataFeed_MustNotContainBothNextPageLinkAndDeltaLink);
				}
				this.nextPageLink = value;
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x060015B4 RID: 5556 RVA: 0x0004ED10 File Offset: 0x0004CF10
		// (set) Token: 0x060015B5 RID: 5557 RVA: 0x0004ED18 File Offset: 0x0004CF18
		public Uri DeltaLink
		{
			get
			{
				return this.deltaLink;
			}
			set
			{
				if (this.NextPageLink != null)
				{
					throw new ODataException(Strings.ODataFeed_MustNotContainBothNextPageLinkAndDeltaLink);
				}
				this.deltaLink = value;
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x060015B6 RID: 5558 RVA: 0x0004ED3A File Offset: 0x0004CF3A
		// (set) Token: 0x060015B7 RID: 5559 RVA: 0x0004ED42 File Offset: 0x0004CF42
		public ICollection<ODataInstanceAnnotation> InstanceAnnotations
		{
			get
			{
				return base.GetInstanceAnnotations();
			}
			set
			{
				base.SetInstanceAnnotations(value);
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x060015B8 RID: 5560 RVA: 0x0004ED4B File Offset: 0x0004CF4B
		// (set) Token: 0x060015B9 RID: 5561 RVA: 0x0004ED53 File Offset: 0x0004CF53
		internal ODataFeedAndEntrySerializationInfo SerializationInfo
		{
			get
			{
				return this.serializationInfo;
			}
			set
			{
				this.serializationInfo = ODataFeedAndEntrySerializationInfo.Validate(value);
			}
		}

		// Token: 0x04000973 RID: 2419
		private Uri nextPageLink;

		// Token: 0x04000974 RID: 2420
		private Uri deltaLink;

		// Token: 0x04000975 RID: 2421
		private ODataFeedAndEntrySerializationInfo serializationInfo;
	}
}
