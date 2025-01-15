using System;
using Microsoft.OData.Evaluation;
using Microsoft.OData.JsonLight;

namespace Microsoft.OData
{
	// Token: 0x0200007B RID: 123
	public abstract class ODataOperation : ODataAnnotatable
	{
		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600049D RID: 1181 RVA: 0x0000D2FD File Offset: 0x0000B4FD
		// (set) Token: 0x0600049E RID: 1182 RVA: 0x0000D305 File Offset: 0x0000B505
		public Uri Metadata { get; set; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600049F RID: 1183 RVA: 0x0000D310 File Offset: 0x0000B510
		// (set) Token: 0x060004A0 RID: 1184 RVA: 0x0000D35A File Offset: 0x0000B55A
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

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x0000D36C File Offset: 0x0000B56C
		// (set) Token: 0x060004A2 RID: 1186 RVA: 0x0000D3C2 File Offset: 0x0000B5C2
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

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x0000D3D2 File Offset: 0x0000B5D2
		// (set) Token: 0x060004A4 RID: 1188 RVA: 0x0000D3DA File Offset: 0x0000B5DA
		internal string BindingParameterTypeName { get; set; }

		// Token: 0x060004A5 RID: 1189 RVA: 0x0000D3E3 File Offset: 0x0000B5E3
		internal void SetMetadataBuilder(ODataResourceMetadataBuilder builder, Uri metadataDocumentUri)
		{
			ODataJsonLightValidationUtils.ValidateOperation(metadataDocumentUri, this);
			this.metadataBuilder = builder;
			this.operationFullName = ODataJsonLightUtils.GetFullyQualifiedOperationName(metadataDocumentUri, UriUtils.UriToString(this.Metadata), out this.parameterNames);
			this.computedTitle = null;
			this.computedTarget = null;
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x0000D41E File Offset: 0x0000B61E
		internal ODataResourceMetadataBuilder GetMetadataBuilder()
		{
			return this.metadataBuilder;
		}

		// Token: 0x04000239 RID: 569
		private ODataResourceMetadataBuilder metadataBuilder;

		// Token: 0x0400023A RID: 570
		private string title;

		// Token: 0x0400023B RID: 571
		private bool hasNonComputedTitle;

		// Token: 0x0400023C RID: 572
		private string computedTitle;

		// Token: 0x0400023D RID: 573
		private Uri target;

		// Token: 0x0400023E RID: 574
		private bool hasNonComputedTarget;

		// Token: 0x0400023F RID: 575
		private Uri computedTarget;

		// Token: 0x04000240 RID: 576
		private string operationFullName;

		// Token: 0x04000241 RID: 577
		private string parameterNames;
	}
}
