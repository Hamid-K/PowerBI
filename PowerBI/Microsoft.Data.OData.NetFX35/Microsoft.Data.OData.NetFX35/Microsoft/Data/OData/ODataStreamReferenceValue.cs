using System;
using Microsoft.Data.OData.Evaluation;

namespace Microsoft.Data.OData
{
	// Token: 0x020002AA RID: 682
	public sealed class ODataStreamReferenceValue : ODataValue
	{
		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x060015C5 RID: 5573 RVA: 0x0004EE30 File Offset: 0x0004D030
		// (set) Token: 0x060015C6 RID: 5574 RVA: 0x0004EE7A File Offset: 0x0004D07A
		public Uri EditLink
		{
			get
			{
				Uri uri;
				if (!this.hasNonComputedEditLink)
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
				this.hasNonComputedEditLink = true;
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x060015C7 RID: 5575 RVA: 0x0004EE8C File Offset: 0x0004D08C
		// (set) Token: 0x060015C8 RID: 5576 RVA: 0x0004EED6 File Offset: 0x0004D0D6
		public Uri ReadLink
		{
			get
			{
				Uri uri;
				if (!this.hasNonComputedReadLink)
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
				this.hasNonComputedReadLink = true;
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x060015C9 RID: 5577 RVA: 0x0004EEE6 File Offset: 0x0004D0E6
		// (set) Token: 0x060015CA RID: 5578 RVA: 0x0004EEEE File Offset: 0x0004D0EE
		public string ContentType { get; set; }

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x060015CB RID: 5579 RVA: 0x0004EEF7 File Offset: 0x0004D0F7
		// (set) Token: 0x060015CC RID: 5580 RVA: 0x0004EEFF File Offset: 0x0004D0FF
		public string ETag { get; set; }

		// Token: 0x060015CD RID: 5581 RVA: 0x0004EF08 File Offset: 0x0004D108
		internal void SetMetadataBuilder(ODataEntityMetadataBuilder builder, string propertyName)
		{
			this.metadataBuilder = builder;
			this.edmPropertyName = propertyName;
			this.computedEditLink = null;
			this.computedReadLink = null;
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x0004EF26 File Offset: 0x0004D126
		internal ODataEntityMetadataBuilder GetMetadataBuilder()
		{
			return this.metadataBuilder;
		}

		// Token: 0x0400097F RID: 2431
		private ODataEntityMetadataBuilder metadataBuilder;

		// Token: 0x04000980 RID: 2432
		private string edmPropertyName;

		// Token: 0x04000981 RID: 2433
		private Uri editLink;

		// Token: 0x04000982 RID: 2434
		private Uri computedEditLink;

		// Token: 0x04000983 RID: 2435
		private bool hasNonComputedEditLink;

		// Token: 0x04000984 RID: 2436
		private Uri readLink;

		// Token: 0x04000985 RID: 2437
		private Uri computedReadLink;

		// Token: 0x04000986 RID: 2438
		private bool hasNonComputedReadLink;
	}
}
