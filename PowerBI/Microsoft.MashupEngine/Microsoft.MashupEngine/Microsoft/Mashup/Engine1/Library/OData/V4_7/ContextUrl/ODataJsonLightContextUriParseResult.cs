using System;
using System.Collections.Generic;
using Microsoft.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7.ContextUrl
{
	// Token: 0x02000831 RID: 2097
	internal sealed class ODataJsonLightContextUriParseResult
	{
		// Token: 0x170013FC RID: 5116
		// (get) Token: 0x06003C42 RID: 15426 RVA: 0x000C3963 File Offset: 0x000C1B63
		public Uri ContextUri
		{
			get
			{
				return this.contextUriFromPayload;
			}
		}

		// Token: 0x170013FD RID: 5117
		// (get) Token: 0x06003C43 RID: 15427 RVA: 0x000C396B File Offset: 0x000C1B6B
		// (set) Token: 0x06003C44 RID: 15428 RVA: 0x000C3973 File Offset: 0x000C1B73
		public Uri MetadataDocumentUri { get; set; }

		// Token: 0x170013FE RID: 5118
		// (get) Token: 0x06003C45 RID: 15429 RVA: 0x000C397C File Offset: 0x000C1B7C
		// (set) Token: 0x06003C46 RID: 15430 RVA: 0x000C3984 File Offset: 0x000C1B84
		public string Fragment { get; set; }

		// Token: 0x170013FF RID: 5119
		// (get) Token: 0x06003C47 RID: 15431 RVA: 0x000C398D File Offset: 0x000C1B8D
		// (set) Token: 0x06003C48 RID: 15432 RVA: 0x000C3995 File Offset: 0x000C1B95
		public string SelectQueryOption { get; set; }

		// Token: 0x17001400 RID: 5120
		// (get) Token: 0x06003C49 RID: 15433 RVA: 0x000C399E File Offset: 0x000C1B9E
		// (set) Token: 0x06003C4A RID: 15434 RVA: 0x000C39A6 File Offset: 0x000C1BA6
		public IEnumerable<ContextUrlSelectListItem> SelectItems { get; set; }

		// Token: 0x17001401 RID: 5121
		// (get) Token: 0x06003C4B RID: 15435 RVA: 0x000C39AF File Offset: 0x000C1BAF
		// (set) Token: 0x06003C4C RID: 15436 RVA: 0x000C39B7 File Offset: 0x000C1BB7
		public Microsoft.OData.Edm.IEdmType EdmType { get; set; }

		// Token: 0x17001402 RID: 5122
		// (get) Token: 0x06003C4D RID: 15437 RVA: 0x000C39C0 File Offset: 0x000C1BC0
		// (set) Token: 0x06003C4E RID: 15438 RVA: 0x000C39C8 File Offset: 0x000C1BC8
		public Microsoft.OData.Edm.IEdmNavigationSource NavigationSource { get; set; }

		// Token: 0x17001403 RID: 5123
		// (get) Token: 0x06003C4F RID: 15439 RVA: 0x000C39D1 File Offset: 0x000C1BD1
		// (set) Token: 0x06003C50 RID: 15440 RVA: 0x000C39D9 File Offset: 0x000C1BD9
		public ODataPayloadKind[] DetectedPayloadKinds { get; set; }

		// Token: 0x17001404 RID: 5124
		// (get) Token: 0x06003C51 RID: 15441 RVA: 0x000C39E2 File Offset: 0x000C1BE2
		// (set) Token: 0x06003C52 RID: 15442 RVA: 0x000C39EA File Offset: 0x000C1BEA
		public ODataPath Path { get; set; }

		// Token: 0x17001405 RID: 5125
		// (get) Token: 0x06003C53 RID: 15443 RVA: 0x000C39F3 File Offset: 0x000C1BF3
		// (set) Token: 0x06003C54 RID: 15444 RVA: 0x000C39FB File Offset: 0x000C1BFB
		public ODataDeltaKind DeltaKind { get; set; }

		// Token: 0x17001406 RID: 5126
		// (get) Token: 0x06003C55 RID: 15445 RVA: 0x000C3A04 File Offset: 0x000C1C04
		// (set) Token: 0x06003C56 RID: 15446 RVA: 0x000C3A0C File Offset: 0x000C1C0C
		public ODataPayloadKind PayloadKind { get; set; }

		// Token: 0x06003C57 RID: 15447 RVA: 0x000C3A15 File Offset: 0x000C1C15
		public ODataJsonLightContextUriParseResult(Uri contextUriFromPayload)
		{
			this.contextUriFromPayload = contextUriFromPayload;
		}

		// Token: 0x04001F85 RID: 8069
		private readonly Uri contextUriFromPayload;
	}
}
