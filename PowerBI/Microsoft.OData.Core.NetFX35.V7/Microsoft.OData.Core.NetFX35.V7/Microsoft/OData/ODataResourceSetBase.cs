using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.OData
{
	// Token: 0x02000064 RID: 100
	public abstract class ODataResourceSetBase : ODataItem
	{
		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000333 RID: 819 RVA: 0x0000A2DF File Offset: 0x000084DF
		// (set) Token: 0x06000334 RID: 820 RVA: 0x0000A2E7 File Offset: 0x000084E7
		public long? Count { get; set; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000335 RID: 821 RVA: 0x0000A2F0 File Offset: 0x000084F0
		// (set) Token: 0x06000336 RID: 822 RVA: 0x0000A2F8 File Offset: 0x000084F8
		public Uri Id { get; set; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000337 RID: 823 RVA: 0x0000A301 File Offset: 0x00008501
		// (set) Token: 0x06000338 RID: 824 RVA: 0x0000A309 File Offset: 0x00008509
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
					throw new ODataException(Strings.ODataResourceSet_MustNotContainBothNextPageLinkAndDeltaLink);
				}
				this.nextPageLink = value;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000339 RID: 825 RVA: 0x0000A334 File Offset: 0x00008534
		// (set) Token: 0x0600033A RID: 826 RVA: 0x0000A33C File Offset: 0x0000853C
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
					throw new ODataException(Strings.ODataResourceSet_MustNotContainBothNextPageLinkAndDeltaLink);
				}
				this.deltaLink = value;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600033B RID: 827 RVA: 0x00009CAD File Offset: 0x00007EAD
		// (set) Token: 0x0600033C RID: 828 RVA: 0x00009CB5 File Offset: 0x00007EB5
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

		// Token: 0x040001BB RID: 443
		private Uri nextPageLink;

		// Token: 0x040001BC RID: 444
		private Uri deltaLink;
	}
}
