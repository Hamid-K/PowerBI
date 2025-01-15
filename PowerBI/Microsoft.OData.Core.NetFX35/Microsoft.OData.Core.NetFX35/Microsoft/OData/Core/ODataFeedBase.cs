using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.OData.Core
{
	// Token: 0x02000163 RID: 355
	public abstract class ODataFeedBase : ODataItem
	{
		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000D2B RID: 3371 RVA: 0x00030FEE File Offset: 0x0002F1EE
		// (set) Token: 0x06000D2C RID: 3372 RVA: 0x00030FF6 File Offset: 0x0002F1F6
		public long? Count { get; set; }

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000D2D RID: 3373 RVA: 0x00030FFF File Offset: 0x0002F1FF
		// (set) Token: 0x06000D2E RID: 3374 RVA: 0x00031007 File Offset: 0x0002F207
		public Uri Id { get; set; }

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000D2F RID: 3375 RVA: 0x00031010 File Offset: 0x0002F210
		// (set) Token: 0x06000D30 RID: 3376 RVA: 0x00031018 File Offset: 0x0002F218
		public Uri NextPageLink
		{
			get
			{
				return this.nextPageLink;
			}
			set
			{
				if (this.DeltaLink != null && value != null)
				{
					throw new ODataException(Strings.ODataFeed_MustNotContainBothNextPageLinkAndDeltaLink);
				}
				this.nextPageLink = value;
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000D31 RID: 3377 RVA: 0x00031043 File Offset: 0x0002F243
		// (set) Token: 0x06000D32 RID: 3378 RVA: 0x0003104B File Offset: 0x0002F24B
		public Uri DeltaLink
		{
			get
			{
				return this.deltaLink;
			}
			set
			{
				if (this.NextPageLink != null && value != null)
				{
					throw new ODataException(Strings.ODataFeed_MustNotContainBothNextPageLinkAndDeltaLink);
				}
				this.deltaLink = value;
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000D33 RID: 3379 RVA: 0x00031076 File Offset: 0x0002F276
		// (set) Token: 0x06000D34 RID: 3380 RVA: 0x0003107E File Offset: 0x0002F27E
		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "We want to allow the same instance annotation collection instance to be shared across ODataLib OM instances.")]
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

		// Token: 0x040005B2 RID: 1458
		private Uri nextPageLink;

		// Token: 0x040005B3 RID: 1459
		private Uri deltaLink;
	}
}
