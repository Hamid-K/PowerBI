using System;
using System.Diagnostics;
using Microsoft.OData.Evaluation;

namespace Microsoft.OData
{
	// Token: 0x0200009E RID: 158
	[DebuggerDisplay("{Name}")]
	public sealed class ODataNestedResourceInfo : ODataItem
	{
		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000691 RID: 1681 RVA: 0x000104D4 File Offset: 0x0000E6D4
		// (set) Token: 0x06000692 RID: 1682 RVA: 0x000104DC File Offset: 0x0000E6DC
		public bool? IsCollection { get; set; }

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000693 RID: 1683 RVA: 0x000104E5 File Offset: 0x0000E6E5
		// (set) Token: 0x06000694 RID: 1684 RVA: 0x000104ED File Offset: 0x0000E6ED
		public string Name { get; set; }

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x000104F8 File Offset: 0x0000E6F8
		// (set) Token: 0x06000696 RID: 1686 RVA: 0x00010545 File Offset: 0x0000E745
		public Uri Url
		{
			get
			{
				if (this.metadataBuilder != null && !this.IsComplex)
				{
					this.url = this.metadataBuilder.GetNavigationLinkUri(this.Name, this.url, this.hasNavigationLink);
					this.hasNavigationLink = true;
				}
				return this.url;
			}
			set
			{
				this.url = value;
				this.hasNavigationLink = true;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x00010558 File Offset: 0x0000E758
		// (set) Token: 0x06000698 RID: 1688 RVA: 0x000105A5 File Offset: 0x0000E7A5
		public Uri AssociationLinkUrl
		{
			get
			{
				if (this.metadataBuilder != null && !this.IsComplex)
				{
					this.associationLinkUrl = this.metadataBuilder.GetAssociationLinkUri(this.Name, this.associationLinkUrl, this.hasAssociationUrl);
					this.hasAssociationUrl = true;
				}
				return this.associationLinkUrl;
			}
			set
			{
				this.associationLinkUrl = value;
				this.hasAssociationUrl = true;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000699 RID: 1689 RVA: 0x000105B5 File Offset: 0x0000E7B5
		// (set) Token: 0x0600069A RID: 1690 RVA: 0x000105BD File Offset: 0x0000E7BD
		internal Uri ContextUrl { get; set; }

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600069B RID: 1691 RVA: 0x000105C6 File Offset: 0x0000E7C6
		// (set) Token: 0x0600069C RID: 1692 RVA: 0x000105CE File Offset: 0x0000E7CE
		internal ODataResourceMetadataBuilder MetadataBuilder
		{
			get
			{
				return this.metadataBuilder;
			}
			set
			{
				this.metadataBuilder = value;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x0600069D RID: 1693 RVA: 0x000105D7 File Offset: 0x0000E7D7
		// (set) Token: 0x0600069E RID: 1694 RVA: 0x000105DF File Offset: 0x0000E7DF
		internal ODataNestedResourceInfoSerializationInfo SerializationInfo { get; set; }

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600069F RID: 1695 RVA: 0x000105E8 File Offset: 0x0000E7E8
		// (set) Token: 0x060006A0 RID: 1696 RVA: 0x000105F0 File Offset: 0x0000E7F0
		internal bool IsComplex { get; set; }

		// Token: 0x04000295 RID: 661
		private ODataResourceMetadataBuilder metadataBuilder;

		// Token: 0x04000296 RID: 662
		private Uri url;

		// Token: 0x04000297 RID: 663
		private bool hasNavigationLink;

		// Token: 0x04000298 RID: 664
		private Uri associationLinkUrl;

		// Token: 0x04000299 RID: 665
		private bool hasAssociationUrl;
	}
}
