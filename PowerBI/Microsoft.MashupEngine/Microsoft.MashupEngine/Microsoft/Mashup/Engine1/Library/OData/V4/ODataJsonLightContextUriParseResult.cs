using System;
using Microsoft.OData.Core;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Edm;

namespace Microsoft.Mashup.Engine1.Library.OData.V4
{
	// Token: 0x02000869 RID: 2153
	internal sealed class ODataJsonLightContextUriParseResult
	{
		// Token: 0x1700145C RID: 5212
		// (get) Token: 0x06003DF0 RID: 15856 RVA: 0x000CA91E File Offset: 0x000C8B1E
		public Uri ContextUri
		{
			get
			{
				return this.contextUriFromPayload;
			}
		}

		// Token: 0x1700145D RID: 5213
		// (get) Token: 0x06003DF1 RID: 15857 RVA: 0x000CA926 File Offset: 0x000C8B26
		// (set) Token: 0x06003DF2 RID: 15858 RVA: 0x000CA92E File Offset: 0x000C8B2E
		public Uri MetadataDocumentUri { get; set; }

		// Token: 0x1700145E RID: 5214
		// (get) Token: 0x06003DF3 RID: 15859 RVA: 0x000CA937 File Offset: 0x000C8B37
		// (set) Token: 0x06003DF4 RID: 15860 RVA: 0x000CA93F File Offset: 0x000C8B3F
		public string Fragment { get; set; }

		// Token: 0x1700145F RID: 5215
		// (get) Token: 0x06003DF5 RID: 15861 RVA: 0x000CA948 File Offset: 0x000C8B48
		// (set) Token: 0x06003DF6 RID: 15862 RVA: 0x000CA950 File Offset: 0x000C8B50
		public string SelectQueryOption { get; set; }

		// Token: 0x17001460 RID: 5216
		// (get) Token: 0x06003DF7 RID: 15863 RVA: 0x000CA959 File Offset: 0x000C8B59
		// (set) Token: 0x06003DF8 RID: 15864 RVA: 0x000CA961 File Offset: 0x000C8B61
		public Microsoft.OData.Edm.IEdmType EdmType { get; set; }

		// Token: 0x17001461 RID: 5217
		// (get) Token: 0x06003DF9 RID: 15865 RVA: 0x000CA96A File Offset: 0x000C8B6A
		// (set) Token: 0x06003DFA RID: 15866 RVA: 0x000CA972 File Offset: 0x000C8B72
		public ODataPayloadKind[] DetectedPayloadKinds { get; set; }

		// Token: 0x17001462 RID: 5218
		// (get) Token: 0x06003DFB RID: 15867 RVA: 0x000CA97B File Offset: 0x000C8B7B
		// (set) Token: 0x06003DFC RID: 15868 RVA: 0x000CA983 File Offset: 0x000C8B83
		public bool IsNullProperty { get; set; }

		// Token: 0x17001463 RID: 5219
		// (get) Token: 0x06003DFD RID: 15869 RVA: 0x000CA98C File Offset: 0x000C8B8C
		// (set) Token: 0x06003DFE RID: 15870 RVA: 0x000CA994 File Offset: 0x000C8B94
		public ODataPath Path { get; set; }

		// Token: 0x17001464 RID: 5220
		// (get) Token: 0x06003DFF RID: 15871 RVA: 0x000CA99D File Offset: 0x000C8B9D
		// (set) Token: 0x06003E00 RID: 15872 RVA: 0x000CA9A5 File Offset: 0x000C8BA5
		public ODataDeltaKind DeltaKind { get; set; }

		// Token: 0x17001465 RID: 5221
		// (get) Token: 0x06003E01 RID: 15873 RVA: 0x000CA9AE File Offset: 0x000C8BAE
		// (set) Token: 0x06003E02 RID: 15874 RVA: 0x000CA9B6 File Offset: 0x000C8BB6
		public ODataPayloadKind PayloadKind { get; set; }

		// Token: 0x06003E03 RID: 15875 RVA: 0x000CA9BF File Offset: 0x000C8BBF
		public ODataJsonLightContextUriParseResult(Uri contextUriFromPayload)
		{
			this.contextUriFromPayload = contextUriFromPayload;
		}

		// Token: 0x0400209C RID: 8348
		private readonly Uri contextUriFromPayload;
	}
}
