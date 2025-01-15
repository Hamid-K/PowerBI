using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x02000203 RID: 515
	internal sealed class ODataJsonLightContextUriParseResult
	{
		// Token: 0x060013E4 RID: 5092 RVA: 0x00038F2F File Offset: 0x0003712F
		internal ODataJsonLightContextUriParseResult(Uri contextUriFromPayload)
		{
			this.contextUriFromPayload = contextUriFromPayload;
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x060013E5 RID: 5093 RVA: 0x00038F3E File Offset: 0x0003713E
		internal Uri ContextUri
		{
			get
			{
				return this.contextUriFromPayload;
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x060013E6 RID: 5094 RVA: 0x00038F46 File Offset: 0x00037146
		// (set) Token: 0x060013E7 RID: 5095 RVA: 0x00038F4E File Offset: 0x0003714E
		internal Uri MetadataDocumentUri { get; set; }

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x060013E8 RID: 5096 RVA: 0x00038F57 File Offset: 0x00037157
		// (set) Token: 0x060013E9 RID: 5097 RVA: 0x00038F5F File Offset: 0x0003715F
		internal string Fragment { get; set; }

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x060013EA RID: 5098 RVA: 0x00038F68 File Offset: 0x00037168
		// (set) Token: 0x060013EB RID: 5099 RVA: 0x00038F70 File Offset: 0x00037170
		internal string SelectQueryOption { get; set; }

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x060013EC RID: 5100 RVA: 0x00038F79 File Offset: 0x00037179
		// (set) Token: 0x060013ED RID: 5101 RVA: 0x00038F81 File Offset: 0x00037181
		internal IEdmNavigationSource NavigationSource { get; set; }

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x060013EE RID: 5102 RVA: 0x00038F8A File Offset: 0x0003718A
		// (set) Token: 0x060013EF RID: 5103 RVA: 0x00038F92 File Offset: 0x00037192
		internal IEdmType EdmType { get; set; }

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x060013F0 RID: 5104 RVA: 0x00038F9B File Offset: 0x0003719B
		// (set) Token: 0x060013F1 RID: 5105 RVA: 0x00038FA3 File Offset: 0x000371A3
		internal IEnumerable<ODataPayloadKind> DetectedPayloadKinds { get; set; }

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x060013F2 RID: 5106 RVA: 0x00038FAC File Offset: 0x000371AC
		// (set) Token: 0x060013F3 RID: 5107 RVA: 0x00038FB4 File Offset: 0x000371B4
		internal ODataPath Path { get; set; }

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x060013F4 RID: 5108 RVA: 0x00038FBD File Offset: 0x000371BD
		// (set) Token: 0x060013F5 RID: 5109 RVA: 0x00038FC5 File Offset: 0x000371C5
		internal ODataDeltaKind DeltaKind { get; set; }

		// Token: 0x04000A01 RID: 2561
		private readonly Uri contextUriFromPayload;
	}
}
