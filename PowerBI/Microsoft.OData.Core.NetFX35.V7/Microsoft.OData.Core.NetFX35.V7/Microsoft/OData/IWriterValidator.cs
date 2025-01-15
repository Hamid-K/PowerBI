using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData
{
	// Token: 0x020000C1 RID: 193
	internal interface IWriterValidator
	{
		// Token: 0x06000774 RID: 1908
		IDuplicatePropertyNameChecker CreateDuplicatePropertyNameChecker();

		// Token: 0x06000775 RID: 1909
		void ValidateResourceInNestedResourceInfo(IEdmStructuredType resourceType, IEdmStructuredType parentNavigationPropertyType);

		// Token: 0x06000776 RID: 1910
		void ValidateNestedResourceInfoHasCardinality(ODataNestedResourceInfo nestedResourceInfo);

		// Token: 0x06000777 RID: 1911
		void ValidateOpenPropertyValue(string propertyName, object value);

		// Token: 0x06000778 RID: 1912
		void ValidateIsExpectedPrimitiveType(object value, IEdmPrimitiveTypeReference valuePrimitiveTypeReference, IEdmTypeReference expectedTypeReference);

		// Token: 0x06000779 RID: 1913
		void ValidateTypeReference(IEdmTypeReference typeReferenceFromMetadata, IEdmTypeReference typeReferenceFromValue);

		// Token: 0x0600077A RID: 1914
		void ValidateTypeKind(EdmTypeKind actualTypeKind, EdmTypeKind expectedTypeKind, bool? expectStructuredType, IEdmType edmType);

		// Token: 0x0600077B RID: 1915
		void ValidateMetadataResource(ODataResource resource, IEdmEntityType resourceType);

		// Token: 0x0600077C RID: 1916
		void ValidateNullPropertyValue(IEdmTypeReference expectedPropertyTypeReference, string propertyName, IEdmModel model);

		// Token: 0x0600077D RID: 1917
		void ValidateNullCollectionItem(IEdmTypeReference expectedItemType);

		// Token: 0x0600077E RID: 1918
		IEdmProperty ValidatePropertyDefined(string propertyName, IEdmStructuredType owningStructuredType);

		// Token: 0x0600077F RID: 1919
		IEdmNavigationProperty ValidateNestedResourceInfo(ODataNestedResourceInfo nestedResourceInfo, IEdmStructuredType declaringStructuredType, ODataPayloadKind? expandedPayloadKind);
	}
}
