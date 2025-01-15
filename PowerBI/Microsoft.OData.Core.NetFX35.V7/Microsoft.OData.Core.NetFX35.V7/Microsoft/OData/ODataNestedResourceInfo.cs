using System;
using System.Diagnostics;
using Microsoft.OData.Evaluation;

namespace Microsoft.OData
{
	// Token: 0x02000078 RID: 120
	[DebuggerDisplay("{Name}")]
	public sealed class ODataNestedResourceInfo : ODataItem
	{
		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x0000D130 File Offset: 0x0000B330
		// (set) Token: 0x06000483 RID: 1155 RVA: 0x0000D138 File Offset: 0x0000B338
		public bool? IsCollection { get; set; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x0000D141 File Offset: 0x0000B341
		// (set) Token: 0x06000485 RID: 1157 RVA: 0x0000D149 File Offset: 0x0000B349
		public string Name { get; set; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x0000D154 File Offset: 0x0000B354
		// (set) Token: 0x06000487 RID: 1159 RVA: 0x0000D1A1 File Offset: 0x0000B3A1
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

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x0000D1B4 File Offset: 0x0000B3B4
		// (set) Token: 0x06000489 RID: 1161 RVA: 0x0000D201 File Offset: 0x0000B401
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

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x0000D211 File Offset: 0x0000B411
		// (set) Token: 0x0600048B RID: 1163 RVA: 0x0000D219 File Offset: 0x0000B419
		internal Uri ContextUrl { get; set; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x0000D222 File Offset: 0x0000B422
		// (set) Token: 0x0600048D RID: 1165 RVA: 0x0000D22A File Offset: 0x0000B42A
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

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x0000D233 File Offset: 0x0000B433
		// (set) Token: 0x0600048F RID: 1167 RVA: 0x0000D23B File Offset: 0x0000B43B
		internal ODataNestedResourceInfoSerializationInfo SerializationInfo { get; set; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x0000D244 File Offset: 0x0000B444
		// (set) Token: 0x06000491 RID: 1169 RVA: 0x0000D24C File Offset: 0x0000B44C
		internal bool IsComplex { get; set; }

		// Token: 0x0400022F RID: 559
		private ODataResourceMetadataBuilder metadataBuilder;

		// Token: 0x04000230 RID: 560
		private Uri url;

		// Token: 0x04000231 RID: 561
		private bool hasNavigationLink;

		// Token: 0x04000232 RID: 562
		private Uri associationLinkUrl;

		// Token: 0x04000233 RID: 563
		private bool hasAssociationUrl;
	}
}
