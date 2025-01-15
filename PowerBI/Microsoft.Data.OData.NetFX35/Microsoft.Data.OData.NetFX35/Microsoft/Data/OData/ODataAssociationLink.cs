using System;
using System.Diagnostics;
using Microsoft.Data.OData.Evaluation;

namespace Microsoft.Data.OData
{
	// Token: 0x0200028C RID: 652
	[DebuggerDisplay("{Name}")]
	public sealed class ODataAssociationLink : ODataAnnotatable
	{
		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x060014BC RID: 5308 RVA: 0x0004C40F File Offset: 0x0004A60F
		// (set) Token: 0x060014BD RID: 5309 RVA: 0x0004C417 File Offset: 0x0004A617
		public string Name { get; set; }

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x060014BE RID: 5310 RVA: 0x0004C420 File Offset: 0x0004A620
		// (set) Token: 0x060014BF RID: 5311 RVA: 0x0004C45A File Offset: 0x0004A65A
		public Uri Url
		{
			get
			{
				if (this.metadataBuilder != null)
				{
					this.url = this.metadataBuilder.GetAssociationLinkUri(this.Name, this.url, this.hasAssociationLinkUrl);
					this.hasAssociationLinkUrl = true;
				}
				return this.url;
			}
			set
			{
				this.url = value;
				this.hasAssociationLinkUrl = true;
			}
		}

		// Token: 0x060014C0 RID: 5312 RVA: 0x0004C46A File Offset: 0x0004A66A
		internal void SetMetadataBuilder(ODataEntityMetadataBuilder builder)
		{
			this.metadataBuilder = builder;
		}

		// Token: 0x04000859 RID: 2137
		private ODataEntityMetadataBuilder metadataBuilder;

		// Token: 0x0400085A RID: 2138
		private Uri url;

		// Token: 0x0400085B RID: 2139
		private bool hasAssociationLinkUrl;
	}
}
