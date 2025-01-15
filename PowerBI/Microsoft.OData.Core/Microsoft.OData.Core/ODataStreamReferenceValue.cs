using System;
using Microsoft.OData.Evaluation;

namespace Microsoft.OData
{
	// Token: 0x020000E8 RID: 232
	public sealed class ODataStreamReferenceValue : ODataValue, IODataStreamReferenceInfo
	{
		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000AAC RID: 2732 RVA: 0x0001CBA4 File Offset: 0x0001ADA4
		// (set) Token: 0x06000AAD RID: 2733 RVA: 0x0001CBEE File Offset: 0x0001ADEE
		public Uri EditLink
		{
			get
			{
				Uri uri;
				if (!this.HasNonComputedEditLink)
				{
					if ((uri = this.computedEditLink) == null)
					{
						if (this.metadataBuilder != null)
						{
							return this.computedEditLink = this.metadataBuilder.GetStreamEditLink(this.edmPropertyName);
						}
						return null;
					}
				}
				else
				{
					uri = this.editLink;
				}
				return uri;
			}
			set
			{
				this.editLink = value;
				this.HasNonComputedEditLink = true;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000AAE RID: 2734 RVA: 0x0001CC00 File Offset: 0x0001AE00
		// (set) Token: 0x06000AAF RID: 2735 RVA: 0x0001CC4A File Offset: 0x0001AE4A
		public Uri ReadLink
		{
			get
			{
				Uri uri;
				if (!this.HasNonComputedReadLink)
				{
					if ((uri = this.computedReadLink) == null)
					{
						if (this.metadataBuilder != null)
						{
							return this.computedReadLink = this.metadataBuilder.GetStreamReadLink(this.edmPropertyName);
						}
						return null;
					}
				}
				else
				{
					uri = this.readLink;
				}
				return uri;
			}
			set
			{
				this.readLink = value;
				this.HasNonComputedReadLink = true;
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000AB0 RID: 2736 RVA: 0x0001CC5A File Offset: 0x0001AE5A
		// (set) Token: 0x06000AB1 RID: 2737 RVA: 0x0001CC62 File Offset: 0x0001AE62
		public string ContentType { get; set; }

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000AB2 RID: 2738 RVA: 0x0001CC6B File Offset: 0x0001AE6B
		// (set) Token: 0x06000AB3 RID: 2739 RVA: 0x0001CC73 File Offset: 0x0001AE73
		public string ETag { get; set; }

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000AB4 RID: 2740 RVA: 0x0001CC7C File Offset: 0x0001AE7C
		// (set) Token: 0x06000AB5 RID: 2741 RVA: 0x0001CC84 File Offset: 0x0001AE84
		internal bool HasNonComputedEditLink { get; private set; }

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000AB6 RID: 2742 RVA: 0x0001CC8D File Offset: 0x0001AE8D
		// (set) Token: 0x06000AB7 RID: 2743 RVA: 0x0001CC95 File Offset: 0x0001AE95
		internal bool HasNonComputedReadLink { get; private set; }

		// Token: 0x06000AB8 RID: 2744 RVA: 0x0001CC9E File Offset: 0x0001AE9E
		internal void SetMetadataBuilder(ODataResourceMetadataBuilder builder, string propertyName)
		{
			this.metadataBuilder = builder;
			this.edmPropertyName = propertyName;
			this.computedEditLink = null;
			this.computedReadLink = null;
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x0001CCBC File Offset: 0x0001AEBC
		internal ODataResourceMetadataBuilder GetMetadataBuilder()
		{
			return this.metadataBuilder;
		}

		// Token: 0x040003D1 RID: 977
		private string edmPropertyName;

		// Token: 0x040003D2 RID: 978
		private ODataResourceMetadataBuilder metadataBuilder;

		// Token: 0x040003D3 RID: 979
		private Uri editLink;

		// Token: 0x040003D4 RID: 980
		private Uri computedEditLink;

		// Token: 0x040003D5 RID: 981
		private Uri readLink;

		// Token: 0x040003D6 RID: 982
		private Uri computedReadLink;
	}
}
