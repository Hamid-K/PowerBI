using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;

namespace Microsoft.OData
{
	// Token: 0x02000018 RID: 24
	internal interface IWriterValidator
	{
		// Token: 0x06000108 RID: 264
		IDuplicatePropertyNameChecker CreateDuplicatePropertyNameChecker();

		// Token: 0x06000109 RID: 265
		void ValidateResourceInNestedResourceInfo(IEdmStructuredType resourceType, IEdmStructuredType parentNavigationPropertyType);

		// Token: 0x0600010A RID: 266
		void ValidateNestedResourceInfoHasCardinality(ODataNestedResourceInfo nestedResourceInfo);

		// Token: 0x0600010B RID: 267
		void ValidateIsExpectedPrimitiveType(object value, IEdmPrimitiveTypeReference valuePrimitiveTypeReference, IEdmTypeReference expectedTypeReference);

		// Token: 0x0600010C RID: 268
		void ValidateTypeReference(IEdmTypeReference typeReferenceFromMetadata, IEdmTypeReference typeReferenceFromValue);

		// Token: 0x0600010D RID: 269
		void ValidateTypeKind(EdmTypeKind actualTypeKind, EdmTypeKind expectedTypeKind, bool? expectStructuredType, IEdmType edmType);

		// Token: 0x0600010E RID: 270
		void ValidateMetadataResource(ODataResourceBase resource, IEdmEntityType resourceType);

		// Token: 0x0600010F RID: 271
		void ValidateNullPropertyValue(IEdmTypeReference expectedPropertyTypeReference, string propertyName, bool isTopLevel, IEdmModel model);

		// Token: 0x06000110 RID: 272
		void ValidateNullCollectionItem(IEdmTypeReference expectedItemType);

		// Token: 0x06000111 RID: 273
		IEdmProperty ValidatePropertyDefined(string propertyName, IEdmStructuredType owningStructuredType);

		// Token: 0x06000112 RID: 274
		IEdmNavigationProperty ValidateNestedResourceInfo(ODataNestedResourceInfo nestedResourceInfo, IEdmStructuredType declaringStructuredType, ODataPayloadKind? expandedPayloadKind);

		// Token: 0x06000113 RID: 275
		void ValidateDerivedTypeConstraint(IEdmStructuredType resourceType, IEdmStructuredType metadataType, IEnumerable<string> derivedTypeConstraints, string itemKind, string itemName);
	}
}
