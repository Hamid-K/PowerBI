using System;
using Microsoft.OData.Core.Json;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.JsonLight
{
	// Token: 0x020000AA RID: 170
	internal interface IODataJsonLightValueSerializer
	{
		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000633 RID: 1587
		IJsonWriter JsonWriter { get; }

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000634 RID: 1588
		IEdmModel Model { get; }

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000635 RID: 1589
		ODataMessageWriterSettings Settings { get; }

		// Token: 0x06000636 RID: 1590
		void WriteNullValue();

		// Token: 0x06000637 RID: 1591
		void WriteComplexValue(ODataComplexValue complexValue, IEdmTypeReference metadataTypeReference, bool isTopLevel, bool isOpenPropertyType, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker);

		// Token: 0x06000638 RID: 1592
		void WriteEnumValue(ODataEnumValue value, IEdmTypeReference expectedTypeReference);

		// Token: 0x06000639 RID: 1593
		void WriteCollectionValue(ODataCollectionValue collectionValue, IEdmTypeReference metadataTypeReference, IEdmTypeReference valueTypeReference, bool isTopLevelProperty, bool isInUri, bool isOpenPropertyType);

		// Token: 0x0600063A RID: 1594
		void WritePrimitiveValue(object value, IEdmTypeReference expectedTypeReference);

		// Token: 0x0600063B RID: 1595
		void WriteUntypedValue(ODataUntypedValue value);

		// Token: 0x0600063C RID: 1596
		DuplicatePropertyNamesChecker CreateDuplicatePropertyNamesChecker();
	}
}
