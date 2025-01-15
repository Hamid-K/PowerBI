using System;
using System.Diagnostics;
using Microsoft.Data.OData.Evaluation;

namespace Microsoft.Data.OData
{
	// Token: 0x020002A9 RID: 681
	[DebuggerDisplay("{Name}")]
	public sealed class ODataNavigationLink : ODataItem
	{
		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x060015BB RID: 5563 RVA: 0x0004ED69 File Offset: 0x0004CF69
		// (set) Token: 0x060015BC RID: 5564 RVA: 0x0004ED71 File Offset: 0x0004CF71
		public bool? IsCollection { get; set; }

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x060015BD RID: 5565 RVA: 0x0004ED7A File Offset: 0x0004CF7A
		// (set) Token: 0x060015BE RID: 5566 RVA: 0x0004ED82 File Offset: 0x0004CF82
		public string Name { get; set; }

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x060015BF RID: 5567 RVA: 0x0004ED8B File Offset: 0x0004CF8B
		// (set) Token: 0x060015C0 RID: 5568 RVA: 0x0004EDC5 File Offset: 0x0004CFC5
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

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x060015C1 RID: 5569 RVA: 0x0004EDD5 File Offset: 0x0004CFD5
		// (set) Token: 0x060015C2 RID: 5570 RVA: 0x0004EE0F File Offset: 0x0004D00F
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

		// Token: 0x060015C3 RID: 5571 RVA: 0x0004EE1F File Offset: 0x0004D01F
		internal void SetMetadataBuilder(ODataEntityMetadataBuilder builder)
		{
			this.metadataBuilder = builder;
		}

		// Token: 0x04000978 RID: 2424
		private ODataEntityMetadataBuilder metadataBuilder;

		// Token: 0x04000979 RID: 2425
		private Uri url;

		// Token: 0x0400097A RID: 2426
		private bool hasNavigationLink;

		// Token: 0x0400097B RID: 2427
		private Uri associationLinkUrl;

		// Token: 0x0400097C RID: 2428
		private bool hasAssociationUrl;
	}
}
