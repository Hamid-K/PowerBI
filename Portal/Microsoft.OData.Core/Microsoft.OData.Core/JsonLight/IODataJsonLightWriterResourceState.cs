using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x0200022C RID: 556
	internal interface IODataJsonLightWriterResourceState
	{
		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06001854 RID: 6228
		ODataResourceBase Resource { get; }

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06001855 RID: 6229
		IEdmStructuredType ResourceType { get; }

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06001856 RID: 6230
		IEdmStructuredType ResourceTypeFromMetadata { get; }

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06001857 RID: 6231
		ODataResourceSerializationInfo SerializationInfo { get; }

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06001858 RID: 6232
		bool IsUndeclared { get; }

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06001859 RID: 6233
		// (set) Token: 0x0600185A RID: 6234
		bool EditLinkWritten { get; set; }

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x0600185B RID: 6235
		// (set) Token: 0x0600185C RID: 6236
		bool ReadLinkWritten { get; set; }

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x0600185D RID: 6237
		// (set) Token: 0x0600185E RID: 6238
		bool MediaEditLinkWritten { get; set; }

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x0600185F RID: 6239
		// (set) Token: 0x06001860 RID: 6240
		bool MediaReadLinkWritten { get; set; }

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06001861 RID: 6241
		// (set) Token: 0x06001862 RID: 6242
		bool MediaContentTypeWritten { get; set; }

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06001863 RID: 6243
		// (set) Token: 0x06001864 RID: 6244
		bool MediaETagWritten { get; set; }

		// Token: 0x06001865 RID: 6245
		ODataResourceTypeContext GetOrCreateTypeContext(bool writingResponse);
	}
}
