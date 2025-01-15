using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x0200023C RID: 572
	internal sealed class ODataJsonLightContextUriParseResult
	{
		// Token: 0x060018B7 RID: 6327 RVA: 0x00046DEF File Offset: 0x00044FEF
		internal ODataJsonLightContextUriParseResult(Uri contextUriFromPayload)
		{
			this.contextUriFromPayload = contextUriFromPayload;
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x060018B8 RID: 6328 RVA: 0x00046DFE File Offset: 0x00044FFE
		internal Uri ContextUri
		{
			get
			{
				return this.contextUriFromPayload;
			}
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x060018B9 RID: 6329 RVA: 0x00046E06 File Offset: 0x00045006
		// (set) Token: 0x060018BA RID: 6330 RVA: 0x00046E0E File Offset: 0x0004500E
		internal Uri MetadataDocumentUri { get; set; }

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x060018BB RID: 6331 RVA: 0x00046E17 File Offset: 0x00045017
		// (set) Token: 0x060018BC RID: 6332 RVA: 0x00046E1F File Offset: 0x0004501F
		internal string Fragment { get; set; }

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x060018BD RID: 6333 RVA: 0x00046E28 File Offset: 0x00045028
		// (set) Token: 0x060018BE RID: 6334 RVA: 0x00046E30 File Offset: 0x00045030
		internal string SelectQueryOption { get; set; }

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x060018BF RID: 6335 RVA: 0x00046E39 File Offset: 0x00045039
		// (set) Token: 0x060018C0 RID: 6336 RVA: 0x00046E41 File Offset: 0x00045041
		internal IEdmNavigationSource NavigationSource { get; set; }

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x060018C1 RID: 6337 RVA: 0x00046E4A File Offset: 0x0004504A
		// (set) Token: 0x060018C2 RID: 6338 RVA: 0x00046E52 File Offset: 0x00045052
		internal IEdmType EdmType { get; set; }

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x060018C3 RID: 6339 RVA: 0x00046E5B File Offset: 0x0004505B
		// (set) Token: 0x060018C4 RID: 6340 RVA: 0x00046E63 File Offset: 0x00045063
		internal IEnumerable<ODataPayloadKind> DetectedPayloadKinds { get; set; }

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x060018C5 RID: 6341 RVA: 0x00046E6C File Offset: 0x0004506C
		// (set) Token: 0x060018C6 RID: 6342 RVA: 0x00046E74 File Offset: 0x00045074
		internal ODataPath Path { get; set; }

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x060018C7 RID: 6343 RVA: 0x00046E7D File Offset: 0x0004507D
		// (set) Token: 0x060018C8 RID: 6344 RVA: 0x00046E85 File Offset: 0x00045085
		internal ODataDeltaKind DeltaKind { get; set; }

		// Token: 0x04000B1F RID: 2847
		private readonly Uri contextUriFromPayload;
	}
}
