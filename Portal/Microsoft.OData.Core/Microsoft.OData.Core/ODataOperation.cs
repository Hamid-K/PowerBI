using System;
using Microsoft.OData.Evaluation;
using Microsoft.OData.JsonLight;

namespace Microsoft.OData
{
	// Token: 0x020000A0 RID: 160
	public abstract class ODataOperation : ODataAnnotatable
	{
		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060006AD RID: 1709 RVA: 0x0001073C File Offset: 0x0000E93C
		// (set) Token: 0x060006AE RID: 1710 RVA: 0x00010744 File Offset: 0x0000E944
		public Uri Metadata { get; set; }

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060006AF RID: 1711 RVA: 0x00010750 File Offset: 0x0000E950
		// (set) Token: 0x060006B0 RID: 1712 RVA: 0x0001079A File Offset: 0x0000E99A
		public string Title
		{
			get
			{
				string text;
				if (!this.hasNonComputedTitle)
				{
					if ((text = this.computedTitle) == null)
					{
						if (this.metadataBuilder != null)
						{
							return this.computedTitle = this.metadataBuilder.GetOperationTitle(this.operationFullName);
						}
						return null;
					}
				}
				else
				{
					text = this.title;
				}
				return text;
			}
			set
			{
				this.title = value;
				this.hasNonComputedTitle = true;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060006B1 RID: 1713 RVA: 0x000107AC File Offset: 0x0000E9AC
		// (set) Token: 0x060006B2 RID: 1714 RVA: 0x00010802 File Offset: 0x0000EA02
		public Uri Target
		{
			get
			{
				Uri uri;
				if (!this.hasNonComputedTarget)
				{
					if ((uri = this.computedTarget) == null)
					{
						if (this.metadataBuilder != null)
						{
							return this.computedTarget = this.metadataBuilder.GetOperationTargetUri(this.operationFullName, this.BindingParameterTypeName, this.parameterNames);
						}
						return null;
					}
				}
				else
				{
					uri = this.target;
				}
				return uri;
			}
			set
			{
				this.target = value;
				this.hasNonComputedTarget = true;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060006B3 RID: 1715 RVA: 0x00010812 File Offset: 0x0000EA12
		// (set) Token: 0x060006B4 RID: 1716 RVA: 0x0001081A File Offset: 0x0000EA1A
		internal string BindingParameterTypeName { get; set; }

		// Token: 0x060006B5 RID: 1717 RVA: 0x00010823 File Offset: 0x0000EA23
		internal void SetMetadataBuilder(ODataResourceMetadataBuilder builder, Uri metadataDocumentUri)
		{
			ODataJsonLightValidationUtils.ValidateOperation(metadataDocumentUri, this);
			this.metadataBuilder = builder;
			this.operationFullName = ODataJsonLightUtils.GetFullyQualifiedOperationName(metadataDocumentUri, UriUtils.UriToString(this.Metadata), out this.parameterNames);
			this.computedTitle = null;
			this.computedTarget = null;
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x0001085E File Offset: 0x0000EA5E
		internal ODataResourceMetadataBuilder GetMetadataBuilder()
		{
			return this.metadataBuilder;
		}

		// Token: 0x0400029F RID: 671
		private ODataResourceMetadataBuilder metadataBuilder;

		// Token: 0x040002A0 RID: 672
		private string title;

		// Token: 0x040002A1 RID: 673
		private bool hasNonComputedTitle;

		// Token: 0x040002A2 RID: 674
		private string computedTitle;

		// Token: 0x040002A3 RID: 675
		private Uri target;

		// Token: 0x040002A4 RID: 676
		private bool hasNonComputedTarget;

		// Token: 0x040002A5 RID: 677
		private Uri computedTarget;

		// Token: 0x040002A6 RID: 678
		private string operationFullName;

		// Token: 0x040002A7 RID: 679
		private string parameterNames;
	}
}
