using System;
using Microsoft.OData.Core.Evaluation;
using Microsoft.OData.Core.JsonLight;

namespace Microsoft.OData.Core
{
	// Token: 0x02000136 RID: 310
	public abstract class ODataOperation : ODataAnnotatable
	{
		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000BCB RID: 3019 RVA: 0x0002CB12 File Offset: 0x0002AD12
		// (set) Token: 0x06000BCC RID: 3020 RVA: 0x0002CB1A File Offset: 0x0002AD1A
		public Uri Metadata { get; set; }

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000BCD RID: 3021 RVA: 0x0002CB24 File Offset: 0x0002AD24
		// (set) Token: 0x06000BCE RID: 3022 RVA: 0x0002CB6E File Offset: 0x0002AD6E
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

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000BCF RID: 3023 RVA: 0x0002CB80 File Offset: 0x0002AD80
		// (set) Token: 0x06000BD0 RID: 3024 RVA: 0x0002CBD6 File Offset: 0x0002ADD6
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

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000BD1 RID: 3025 RVA: 0x0002CBE6 File Offset: 0x0002ADE6
		// (set) Token: 0x06000BD2 RID: 3026 RVA: 0x0002CBEE File Offset: 0x0002ADEE
		internal string BindingParameterTypeName { get; set; }

		// Token: 0x06000BD3 RID: 3027 RVA: 0x0002CBF7 File Offset: 0x0002ADF7
		internal void SetMetadataBuilder(ODataEntityMetadataBuilder builder, Uri metadataDocumentUri)
		{
			ODataJsonLightValidationUtils.ValidateOperation(metadataDocumentUri, this);
			this.metadataBuilder = builder;
			this.operationFullName = ODataJsonLightUtils.GetFullyQualifiedOperationName(metadataDocumentUri, UriUtils.UriToString(this.Metadata), out this.parameterNames);
			this.computedTitle = null;
			this.computedTarget = null;
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x0002CC32 File Offset: 0x0002AE32
		internal ODataEntityMetadataBuilder GetMetadataBuilder()
		{
			return this.metadataBuilder;
		}

		// Token: 0x040004EA RID: 1258
		private ODataEntityMetadataBuilder metadataBuilder;

		// Token: 0x040004EB RID: 1259
		private string title;

		// Token: 0x040004EC RID: 1260
		private bool hasNonComputedTitle;

		// Token: 0x040004ED RID: 1261
		private string computedTitle;

		// Token: 0x040004EE RID: 1262
		private Uri target;

		// Token: 0x040004EF RID: 1263
		private bool hasNonComputedTarget;

		// Token: 0x040004F0 RID: 1264
		private Uri computedTarget;

		// Token: 0x040004F1 RID: 1265
		private string operationFullName;

		// Token: 0x040004F2 RID: 1266
		private string parameterNames;
	}
}
