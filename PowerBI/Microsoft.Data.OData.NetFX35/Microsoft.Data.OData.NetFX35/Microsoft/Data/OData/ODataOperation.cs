using System;
using Microsoft.Data.OData.Evaluation;
using Microsoft.Data.OData.JsonLight;

namespace Microsoft.Data.OData
{
	// Token: 0x020001FE RID: 510
	public abstract class ODataOperation : ODataAnnotatable
	{
		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000EAE RID: 3758 RVA: 0x00035174 File Offset: 0x00033374
		// (set) Token: 0x06000EAF RID: 3759 RVA: 0x0003517C File Offset: 0x0003337C
		public Uri Metadata { get; set; }

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000EB0 RID: 3760 RVA: 0x00035188 File Offset: 0x00033388
		// (set) Token: 0x06000EB1 RID: 3761 RVA: 0x000351D2 File Offset: 0x000333D2
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

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000EB2 RID: 3762 RVA: 0x000351E4 File Offset: 0x000333E4
		// (set) Token: 0x06000EB3 RID: 3763 RVA: 0x00035234 File Offset: 0x00033434
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
							return this.computedTarget = this.metadataBuilder.GetOperationTargetUri(this.operationFullName, this.bindingParameterTypeName);
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

		// Token: 0x06000EB4 RID: 3764 RVA: 0x00035244 File Offset: 0x00033444
		internal void SetMetadataBuilder(ODataEntityMetadataBuilder builder, Uri metadataDocumentUri)
		{
			ODataJsonLightValidationUtils.ValidateOperation(metadataDocumentUri, this);
			this.metadataBuilder = builder;
			this.operationFullName = ODataJsonLightUtils.GetFullyQualifiedFunctionImportName(metadataDocumentUri, UriUtilsCommon.UriToString(this.Metadata), out this.bindingParameterTypeName);
			this.computedTitle = null;
			this.computedTarget = null;
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x0003527F File Offset: 0x0003347F
		internal ODataEntityMetadataBuilder GetMetadataBuilder()
		{
			return this.metadataBuilder;
		}

		// Token: 0x0400057C RID: 1404
		private ODataEntityMetadataBuilder metadataBuilder;

		// Token: 0x0400057D RID: 1405
		private string title;

		// Token: 0x0400057E RID: 1406
		private bool hasNonComputedTitle;

		// Token: 0x0400057F RID: 1407
		private string computedTitle;

		// Token: 0x04000580 RID: 1408
		private Uri target;

		// Token: 0x04000581 RID: 1409
		private bool hasNonComputedTarget;

		// Token: 0x04000582 RID: 1410
		private Uri computedTarget;

		// Token: 0x04000583 RID: 1411
		private string operationFullName;

		// Token: 0x04000584 RID: 1412
		private string bindingParameterTypeName;
	}
}
