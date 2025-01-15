using System;

namespace Microsoft.Mashup.Engine1.Library.OData.V3
{
	// Token: 0x020008AE RID: 2222
	internal class DetectedPayloadInformationResults
	{
		// Token: 0x170014A3 RID: 5283
		// (get) Token: 0x06003F84 RID: 16260 RVA: 0x000D1BBE File Offset: 0x000CFDBE
		// (set) Token: 0x06003F85 RID: 16261 RVA: 0x000D1BC6 File Offset: 0x000CFDC6
		public bool IsMetadata { get; set; }

		// Token: 0x170014A4 RID: 5284
		// (get) Token: 0x06003F86 RID: 16262 RVA: 0x000D1BCF File Offset: 0x000CFDCF
		// (set) Token: 0x06003F87 RID: 16263 RVA: 0x000D1BD7 File Offset: 0x000CFDD7
		public bool IsBatchPayload { get; set; }

		// Token: 0x170014A5 RID: 5285
		// (get) Token: 0x06003F88 RID: 16264 RVA: 0x000D1BE0 File Offset: 0x000CFDE0
		// (set) Token: 0x06003F89 RID: 16265 RVA: 0x000D1BE8 File Offset: 0x000CFDE8
		public Uri MetadataUri { get; set; }

		// Token: 0x170014A6 RID: 5286
		// (get) Token: 0x06003F8A RID: 16266 RVA: 0x000D1BF1 File Offset: 0x000CFDF1
		// (set) Token: 0x06003F8B RID: 16267 RVA: 0x000D1BF9 File Offset: 0x000CFDF9
		public Uri IdUri { get; set; }

		// Token: 0x170014A7 RID: 5287
		// (get) Token: 0x06003F8C RID: 16268 RVA: 0x000D1C02 File Offset: 0x000CFE02
		// (set) Token: 0x06003F8D RID: 16269 RVA: 0x000D1C0A File Offset: 0x000CFE0A
		public Uri BaseUri { get; set; }

		// Token: 0x170014A8 RID: 5288
		// (get) Token: 0x06003F8E RID: 16270 RVA: 0x000D1C13 File Offset: 0x000CFE13
		// (set) Token: 0x06003F8F RID: 16271 RVA: 0x000D1C1B File Offset: 0x000CFE1B
		public bool IsJson { get; set; }
	}
}
