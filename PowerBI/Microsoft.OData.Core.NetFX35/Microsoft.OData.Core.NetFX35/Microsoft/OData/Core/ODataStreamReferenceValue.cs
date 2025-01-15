using System;
using Microsoft.OData.Core.Evaluation;

namespace Microsoft.OData.Core
{
	// Token: 0x0200019F RID: 415
	public sealed class ODataStreamReferenceValue : ODataValue
	{
		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000F7F RID: 3967 RVA: 0x000359D0 File Offset: 0x00033BD0
		// (set) Token: 0x06000F80 RID: 3968 RVA: 0x00035A1A File Offset: 0x00033C1A
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

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000F81 RID: 3969 RVA: 0x00035A2C File Offset: 0x00033C2C
		// (set) Token: 0x06000F82 RID: 3970 RVA: 0x00035A76 File Offset: 0x00033C76
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

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000F83 RID: 3971 RVA: 0x00035A86 File Offset: 0x00033C86
		// (set) Token: 0x06000F84 RID: 3972 RVA: 0x00035A8E File Offset: 0x00033C8E
		public string ContentType { get; set; }

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000F85 RID: 3973 RVA: 0x00035A97 File Offset: 0x00033C97
		// (set) Token: 0x06000F86 RID: 3974 RVA: 0x00035A9F File Offset: 0x00033C9F
		public string ETag { get; set; }

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000F87 RID: 3975 RVA: 0x00035AA8 File Offset: 0x00033CA8
		// (set) Token: 0x06000F88 RID: 3976 RVA: 0x00035AB0 File Offset: 0x00033CB0
		internal bool HasNonComputedEditLink { get; private set; }

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000F89 RID: 3977 RVA: 0x00035AB9 File Offset: 0x00033CB9
		// (set) Token: 0x06000F8A RID: 3978 RVA: 0x00035AC1 File Offset: 0x00033CC1
		internal bool HasNonComputedReadLink { get; private set; }

		// Token: 0x06000F8B RID: 3979 RVA: 0x00035ACA File Offset: 0x00033CCA
		internal void SetMetadataBuilder(ODataEntityMetadataBuilder builder, string propertyName)
		{
			this.metadataBuilder = builder;
			this.edmPropertyName = propertyName;
			this.computedEditLink = null;
			this.computedReadLink = null;
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x00035AE8 File Offset: 0x00033CE8
		internal ODataEntityMetadataBuilder GetMetadataBuilder()
		{
			return this.metadataBuilder;
		}

		// Token: 0x040006CA RID: 1738
		private ODataEntityMetadataBuilder metadataBuilder;

		// Token: 0x040006CB RID: 1739
		private string edmPropertyName;

		// Token: 0x040006CC RID: 1740
		private Uri editLink;

		// Token: 0x040006CD RID: 1741
		private Uri computedEditLink;

		// Token: 0x040006CE RID: 1742
		private Uri readLink;

		// Token: 0x040006CF RID: 1743
		private Uri computedReadLink;
	}
}
