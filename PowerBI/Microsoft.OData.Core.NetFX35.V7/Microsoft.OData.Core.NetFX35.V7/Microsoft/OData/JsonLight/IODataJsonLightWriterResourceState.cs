using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x020001F3 RID: 499
	internal interface IODataJsonLightWriterResourceState
	{
		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06001381 RID: 4993
		ODataResource Resource { get; }

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06001382 RID: 4994
		IEdmStructuredType ResourceType { get; }

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06001383 RID: 4995
		IEdmStructuredType ResourceTypeFromMetadata { get; }

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06001384 RID: 4996
		ODataResourceSerializationInfo SerializationInfo { get; }

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06001385 RID: 4997
		bool IsUndeclared { get; }

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06001386 RID: 4998
		// (set) Token: 0x06001387 RID: 4999
		bool EditLinkWritten { get; set; }

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06001388 RID: 5000
		// (set) Token: 0x06001389 RID: 5001
		bool ReadLinkWritten { get; set; }

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x0600138A RID: 5002
		// (set) Token: 0x0600138B RID: 5003
		bool MediaEditLinkWritten { get; set; }

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x0600138C RID: 5004
		// (set) Token: 0x0600138D RID: 5005
		bool MediaReadLinkWritten { get; set; }

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x0600138E RID: 5006
		// (set) Token: 0x0600138F RID: 5007
		bool MediaContentTypeWritten { get; set; }

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06001390 RID: 5008
		// (set) Token: 0x06001391 RID: 5009
		bool MediaETagWritten { get; set; }

		// Token: 0x06001392 RID: 5010
		ODataResourceTypeContext GetOrCreateTypeContext(bool writingResponse);
	}
}
