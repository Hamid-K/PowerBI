using System;
using System.Collections.Generic;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.JsonLight
{
	// Token: 0x020000C1 RID: 193
	internal sealed class ODataJsonLightContextUriParseResult
	{
		// Token: 0x060006EE RID: 1774 RVA: 0x00018D79 File Offset: 0x00016F79
		internal ODataJsonLightContextUriParseResult(Uri contextUriFromPayload)
		{
			this.contextUriFromPayload = contextUriFromPayload;
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060006EF RID: 1775 RVA: 0x00018D88 File Offset: 0x00016F88
		internal Uri ContextUri
		{
			get
			{
				return this.contextUriFromPayload;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060006F0 RID: 1776 RVA: 0x00018D90 File Offset: 0x00016F90
		// (set) Token: 0x060006F1 RID: 1777 RVA: 0x00018D98 File Offset: 0x00016F98
		internal Uri MetadataDocumentUri { get; set; }

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060006F2 RID: 1778 RVA: 0x00018DA1 File Offset: 0x00016FA1
		// (set) Token: 0x060006F3 RID: 1779 RVA: 0x00018DA9 File Offset: 0x00016FA9
		internal string Fragment { get; set; }

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060006F4 RID: 1780 RVA: 0x00018DB2 File Offset: 0x00016FB2
		// (set) Token: 0x060006F5 RID: 1781 RVA: 0x00018DBA File Offset: 0x00016FBA
		internal string SelectQueryOption { get; set; }

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060006F6 RID: 1782 RVA: 0x00018DC3 File Offset: 0x00016FC3
		// (set) Token: 0x060006F7 RID: 1783 RVA: 0x00018DCB File Offset: 0x00016FCB
		internal IEdmNavigationSource NavigationSource { get; set; }

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060006F8 RID: 1784 RVA: 0x00018DD4 File Offset: 0x00016FD4
		// (set) Token: 0x060006F9 RID: 1785 RVA: 0x00018DDC File Offset: 0x00016FDC
		internal IEdmType EdmType { get; set; }

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060006FA RID: 1786 RVA: 0x00018DE5 File Offset: 0x00016FE5
		// (set) Token: 0x060006FB RID: 1787 RVA: 0x00018DED File Offset: 0x00016FED
		internal IEnumerable<ODataPayloadKind> DetectedPayloadKinds { get; set; }

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060006FC RID: 1788 RVA: 0x00018DF6 File Offset: 0x00016FF6
		// (set) Token: 0x060006FD RID: 1789 RVA: 0x00018DFE File Offset: 0x00016FFE
		internal bool IsNullProperty { get; set; }

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060006FE RID: 1790 RVA: 0x00018E07 File Offset: 0x00017007
		// (set) Token: 0x060006FF RID: 1791 RVA: 0x00018E0F File Offset: 0x0001700F
		internal ODataPath Path { get; set; }

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000700 RID: 1792 RVA: 0x00018E18 File Offset: 0x00017018
		// (set) Token: 0x06000701 RID: 1793 RVA: 0x00018E20 File Offset: 0x00017020
		internal ODataDeltaKind DeltaKind { get; set; }

		// Token: 0x0400032F RID: 815
		private readonly Uri contextUriFromPayload;
	}
}
