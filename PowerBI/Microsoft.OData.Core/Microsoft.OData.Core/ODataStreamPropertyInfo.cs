using System;
using Microsoft.OData.Edm;
using Microsoft.OData.Evaluation;

namespace Microsoft.OData
{
	// Token: 0x02000012 RID: 18
	public sealed class ODataStreamPropertyInfo : ODataPropertyInfo, IODataStreamReferenceInfo
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00003448 File Offset: 0x00001648
		// (set) Token: 0x060000DB RID: 219 RVA: 0x00003492 File Offset: 0x00001692
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

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000DC RID: 220 RVA: 0x000034A4 File Offset: 0x000016A4
		// (set) Token: 0x060000DD RID: 221 RVA: 0x000034EE File Offset: 0x000016EE
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

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000DE RID: 222 RVA: 0x000034FE File Offset: 0x000016FE
		// (set) Token: 0x060000DF RID: 223 RVA: 0x00003506 File Offset: 0x00001706
		public string ContentType { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x0000350F File Offset: 0x0000170F
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x00003517 File Offset: 0x00001717
		public string ETag { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00003520 File Offset: 0x00001720
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x00003528 File Offset: 0x00001728
		public override EdmPrimitiveTypeKind PrimitiveTypeKind
		{
			get
			{
				return this.primitiveTypeKind;
			}
			set
			{
				if (value != EdmPrimitiveTypeKind.Binary && value != EdmPrimitiveTypeKind.String && value != EdmPrimitiveTypeKind.None)
				{
					throw new ODataException(Strings.StreamItemInvalidPrimitiveKind(value));
				}
				this.primitiveTypeKind = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x0000354E File Offset: 0x0000174E
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x00003556 File Offset: 0x00001756
		internal bool HasNonComputedEditLink { get; private set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x0000355F File Offset: 0x0000175F
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x00003567 File Offset: 0x00001767
		internal bool HasNonComputedReadLink { get; private set; }

		// Token: 0x060000E8 RID: 232 RVA: 0x00003570 File Offset: 0x00001770
		internal void SetMetadataBuilder(ODataResourceMetadataBuilder builder, string propertyName)
		{
			this.metadataBuilder = builder;
			this.edmPropertyName = propertyName;
			this.computedEditLink = null;
			this.computedReadLink = null;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000358E File Offset: 0x0000178E
		internal ODataResourceMetadataBuilder GetMetadataBuilder()
		{
			return this.metadataBuilder;
		}

		// Token: 0x0400002C RID: 44
		private string edmPropertyName;

		// Token: 0x0400002D RID: 45
		private ODataResourceMetadataBuilder metadataBuilder;

		// Token: 0x0400002E RID: 46
		private Uri editLink;

		// Token: 0x0400002F RID: 47
		private Uri computedEditLink;

		// Token: 0x04000030 RID: 48
		private Uri readLink;

		// Token: 0x04000031 RID: 49
		private Uri computedReadLink;

		// Token: 0x04000032 RID: 50
		private EdmPrimitiveTypeKind primitiveTypeKind;
	}
}
