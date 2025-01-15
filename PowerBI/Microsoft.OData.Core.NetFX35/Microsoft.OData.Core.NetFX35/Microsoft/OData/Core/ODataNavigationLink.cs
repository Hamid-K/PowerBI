using System;
using System.Diagnostics;
using Microsoft.OData.Core.Evaluation;

namespace Microsoft.OData.Core
{
	// Token: 0x02000189 RID: 393
	[DebuggerDisplay("{Name}")]
	public sealed class ODataNavigationLink : ODataItem
	{
		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000EEA RID: 3818 RVA: 0x000345B4 File Offset: 0x000327B4
		// (set) Token: 0x06000EEB RID: 3819 RVA: 0x000345BC File Offset: 0x000327BC
		public bool? IsCollection { get; set; }

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000EEC RID: 3820 RVA: 0x000345C5 File Offset: 0x000327C5
		// (set) Token: 0x06000EED RID: 3821 RVA: 0x000345CD File Offset: 0x000327CD
		public string Name { get; set; }

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000EEE RID: 3822 RVA: 0x000345D6 File Offset: 0x000327D6
		// (set) Token: 0x06000EEF RID: 3823 RVA: 0x00034610 File Offset: 0x00032810
		public Uri Url
		{
			get
			{
				if (this.metadataBuilder != null)
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

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000EF0 RID: 3824 RVA: 0x00034620 File Offset: 0x00032820
		// (set) Token: 0x06000EF1 RID: 3825 RVA: 0x0003465A File Offset: 0x0003285A
		public Uri AssociationLinkUrl
		{
			get
			{
				if (this.metadataBuilder != null)
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

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000EF2 RID: 3826 RVA: 0x0003466A File Offset: 0x0003286A
		// (set) Token: 0x06000EF3 RID: 3827 RVA: 0x00034672 File Offset: 0x00032872
		internal Uri ContextUrl { get; set; }

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000EF4 RID: 3828 RVA: 0x0003467B File Offset: 0x0003287B
		// (set) Token: 0x06000EF5 RID: 3829 RVA: 0x00034683 File Offset: 0x00032883
		internal ODataEntityMetadataBuilder MetadataBuilder
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

		// Token: 0x04000662 RID: 1634
		private ODataEntityMetadataBuilder metadataBuilder;

		// Token: 0x04000663 RID: 1635
		private Uri url;

		// Token: 0x04000664 RID: 1636
		private bool hasNavigationLink;

		// Token: 0x04000665 RID: 1637
		private Uri associationLinkUrl;

		// Token: 0x04000666 RID: 1638
		private bool hasAssociationUrl;
	}
}
