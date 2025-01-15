using System;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Json;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x0200013E RID: 318
	internal interface IODataJsonLightValueSerializer
	{
		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000856 RID: 2134
		IJsonWriter JsonWriter { get; }

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000857 RID: 2135
		ODataVersion Version { get; }

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000858 RID: 2136
		IEdmModel Model { get; }

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000859 RID: 2137
		ODataMessageWriterSettings Settings { get; }

		// Token: 0x0600085A RID: 2138
		void WriteNullValue();

		// Token: 0x0600085B RID: 2139
		void WriteComplexValue(ODataComplexValue complexValue, IEdmTypeReference metadataTypeReference, bool isTopLevel, bool isOpenPropertyType, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker);

		// Token: 0x0600085C RID: 2140
		void WriteCollectionValue(ODataCollectionValue collectionValue, IEdmTypeReference metadataTypeReference, bool isTopLevelProperty, bool isInUri, bool isOpenPropertyType);

		// Token: 0x0600085D RID: 2141
		void WritePrimitiveValue(object value, IEdmTypeReference expectedTypeReference);

		// Token: 0x0600085E RID: 2142
		DuplicatePropertyNamesChecker CreateDuplicatePropertyNamesChecker();
	}
}
