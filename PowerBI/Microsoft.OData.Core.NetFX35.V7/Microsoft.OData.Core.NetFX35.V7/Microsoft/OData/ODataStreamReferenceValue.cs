using System;
using Microsoft.OData.Evaluation;

namespace Microsoft.OData
{
	// Token: 0x0200009B RID: 155
	public sealed class ODataStreamReferenceValue : ODataValue
	{
		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060005EC RID: 1516 RVA: 0x0000FF84 File Offset: 0x0000E184
		// (set) Token: 0x060005ED RID: 1517 RVA: 0x0000FFCE File Offset: 0x0000E1CE
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

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060005EE RID: 1518 RVA: 0x0000FFE0 File Offset: 0x0000E1E0
		// (set) Token: 0x060005EF RID: 1519 RVA: 0x0001002A File Offset: 0x0000E22A
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

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x0001003A File Offset: 0x0000E23A
		// (set) Token: 0x060005F1 RID: 1521 RVA: 0x00010042 File Offset: 0x0000E242
		public string ContentType { get; set; }

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x0001004B File Offset: 0x0000E24B
		// (set) Token: 0x060005F3 RID: 1523 RVA: 0x00010053 File Offset: 0x0000E253
		public string ETag { get; set; }

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060005F4 RID: 1524 RVA: 0x0001005C File Offset: 0x0000E25C
		// (set) Token: 0x060005F5 RID: 1525 RVA: 0x00010064 File Offset: 0x0000E264
		internal bool HasNonComputedEditLink { get; private set; }

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060005F6 RID: 1526 RVA: 0x0001006D File Offset: 0x0000E26D
		// (set) Token: 0x060005F7 RID: 1527 RVA: 0x00010075 File Offset: 0x0000E275
		internal bool HasNonComputedReadLink { get; private set; }

		// Token: 0x060005F8 RID: 1528 RVA: 0x0001007E File Offset: 0x0000E27E
		internal void SetMetadataBuilder(ODataResourceMetadataBuilder builder, string propertyName)
		{
			this.metadataBuilder = builder;
			this.edmPropertyName = propertyName;
			this.computedEditLink = null;
			this.computedReadLink = null;
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x0001009C File Offset: 0x0000E29C
		internal ODataResourceMetadataBuilder GetMetadataBuilder()
		{
			return this.metadataBuilder;
		}

		// Token: 0x040002C3 RID: 707
		private ODataResourceMetadataBuilder metadataBuilder;

		// Token: 0x040002C4 RID: 708
		private string edmPropertyName;

		// Token: 0x040002C5 RID: 709
		private Uri editLink;

		// Token: 0x040002C6 RID: 710
		private Uri computedEditLink;

		// Token: 0x040002C7 RID: 711
		private Uri readLink;

		// Token: 0x040002C8 RID: 712
		private Uri computedReadLink;
	}
}
