using System;
using System.Collections.Generic;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x0200016D RID: 365
	internal sealed class ODataJsonLightMetadataUriParseResult
	{
		// Token: 0x060009F1 RID: 2545 RVA: 0x000209D7 File Offset: 0x0001EBD7
		internal ODataJsonLightMetadataUriParseResult(Uri metadataUriFromPayload)
		{
			this.metadataUriFromPayload = metadataUriFromPayload;
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x060009F2 RID: 2546 RVA: 0x000209E6 File Offset: 0x0001EBE6
		internal Uri MetadataUri
		{
			get
			{
				return this.metadataUriFromPayload;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x060009F3 RID: 2547 RVA: 0x000209EE File Offset: 0x0001EBEE
		// (set) Token: 0x060009F4 RID: 2548 RVA: 0x000209F6 File Offset: 0x0001EBF6
		internal Uri MetadataDocumentUri
		{
			get
			{
				return this.metadataDocumentUri;
			}
			set
			{
				this.metadataDocumentUri = value;
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x060009F5 RID: 2549 RVA: 0x000209FF File Offset: 0x0001EBFF
		// (set) Token: 0x060009F6 RID: 2550 RVA: 0x00020A07 File Offset: 0x0001EC07
		internal string Fragment
		{
			get
			{
				return this.fragment;
			}
			set
			{
				this.fragment = value;
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x060009F7 RID: 2551 RVA: 0x00020A10 File Offset: 0x0001EC10
		// (set) Token: 0x060009F8 RID: 2552 RVA: 0x00020A18 File Offset: 0x0001EC18
		internal string SelectQueryOption
		{
			get
			{
				return this.selectQueryOption;
			}
			set
			{
				this.selectQueryOption = value;
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x060009F9 RID: 2553 RVA: 0x00020A21 File Offset: 0x0001EC21
		// (set) Token: 0x060009FA RID: 2554 RVA: 0x00020A29 File Offset: 0x0001EC29
		internal IEdmEntitySet EntitySet
		{
			get
			{
				return this.entitySet;
			}
			set
			{
				this.entitySet = value;
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x060009FB RID: 2555 RVA: 0x00020A32 File Offset: 0x0001EC32
		// (set) Token: 0x060009FC RID: 2556 RVA: 0x00020A3A File Offset: 0x0001EC3A
		internal IEdmType EdmType
		{
			get
			{
				return this.edmType;
			}
			set
			{
				this.edmType = value;
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x060009FD RID: 2557 RVA: 0x00020A43 File Offset: 0x0001EC43
		// (set) Token: 0x060009FE RID: 2558 RVA: 0x00020A4B File Offset: 0x0001EC4B
		internal IEdmNavigationProperty NavigationProperty
		{
			get
			{
				return this.navigationProperty;
			}
			set
			{
				this.navigationProperty = value;
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x060009FF RID: 2559 RVA: 0x00020A54 File Offset: 0x0001EC54
		// (set) Token: 0x06000A00 RID: 2560 RVA: 0x00020A5C File Offset: 0x0001EC5C
		internal IEnumerable<ODataPayloadKind> DetectedPayloadKinds
		{
			get
			{
				return this.detectedPayloadKinds;
			}
			set
			{
				this.detectedPayloadKinds = value;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000A01 RID: 2561 RVA: 0x00020A65 File Offset: 0x0001EC65
		// (set) Token: 0x06000A02 RID: 2562 RVA: 0x00020A6D File Offset: 0x0001EC6D
		internal bool IsNullProperty
		{
			get
			{
				return this.isNullProperty;
			}
			set
			{
				this.isNullProperty = value;
			}
		}

		// Token: 0x040003BE RID: 958
		private readonly Uri metadataUriFromPayload;

		// Token: 0x040003BF RID: 959
		private Uri metadataDocumentUri;

		// Token: 0x040003C0 RID: 960
		private string fragment;

		// Token: 0x040003C1 RID: 961
		private string selectQueryOption;

		// Token: 0x040003C2 RID: 962
		private IEdmEntitySet entitySet;

		// Token: 0x040003C3 RID: 963
		private IEdmType edmType;

		// Token: 0x040003C4 RID: 964
		private IEdmNavigationProperty navigationProperty;

		// Token: 0x040003C5 RID: 965
		private IEnumerable<ODataPayloadKind> detectedPayloadKinds;

		// Token: 0x040003C6 RID: 966
		private bool isNullProperty;
	}
}
