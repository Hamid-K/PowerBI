using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core
{
	// Token: 0x020002B3 RID: 691
	internal interface IWriterValidator
	{
		// Token: 0x060017B9 RID: 6073
		void ValidateMessageWriterSettings(ODataMessageWriterSettings messageWriterSettings, bool writingResponse);

		// Token: 0x060017BA RID: 6074
		void ValidatePropertyNotNull(ODataProperty property);

		// Token: 0x060017BB RID: 6075
		void ValidatePropertyName(string propertyName);

		// Token: 0x060017BC RID: 6076
		IEdmProperty ValidatePropertyDefined(string propertyName, IEdmStructuredType owningStructuredType, bool throwOnMissingProperty = true);

		// Token: 0x060017BD RID: 6077
		IEdmNavigationProperty ValidateNavigationPropertyDefined(string propertyName, IEdmEntityType owningEntityType);

		// Token: 0x060017BE RID: 6078
		void ValidateEntryInExpandedLink(IEdmEntityType entryEntityType, IEdmEntityType parentNavigationPropertyType);

		// Token: 0x060017BF RID: 6079
		void ValidateCanWriteOperation(ODataOperation operation, bool writingResponse);

		// Token: 0x060017C0 RID: 6080
		void ValidateFeedAtEnd(ODataFeed feed, bool writingRequest);

		// Token: 0x060017C1 RID: 6081
		void ValidateEntryAtStart(ODataEntry entry);

		// Token: 0x060017C2 RID: 6082
		void ValidateEntryAtEnd(ODataEntry entry);

		// Token: 0x060017C3 RID: 6083
		void ValidateStreamReferenceValue(ODataStreamReferenceValue streamReference, bool isDefaultStream);

		// Token: 0x060017C4 RID: 6084
		void ValidateStreamReferenceProperty(ODataProperty streamProperty, IEdmProperty edmProperty, bool writingResponse);

		// Token: 0x060017C5 RID: 6085
		void ValidateEntityReferenceLinkNotNull(ODataEntityReferenceLink entityReferenceLink);

		// Token: 0x060017C6 RID: 6086
		void ValidateEntityReferenceLink(ODataEntityReferenceLink entityReferenceLink);

		// Token: 0x060017C7 RID: 6087
		IEdmNavigationProperty ValidateNavigationLink(ODataNavigationLink navigationLink, IEdmEntityType declaringEntityType, ODataPayloadKind? expandedPayloadKind);

		// Token: 0x060017C8 RID: 6088
		void ValidateNavigationLinkUrlPresent(ODataNavigationLink navigationLink);

		// Token: 0x060017C9 RID: 6089
		void ValidateNavigationLinkHasCardinality(ODataNavigationLink navigationLink);

		// Token: 0x060017CA RID: 6090
		void ValidateNullPropertyValue(IEdmTypeReference expectedPropertyTypeReference, string propertyName, ODataWriterBehavior writerBehavior, IEdmModel model);

		// Token: 0x060017CB RID: 6091
		void ValidateOpenPropertyValue(string propertyName, object value);

		// Token: 0x060017CC RID: 6092
		void ValidateServiceDocumentElement(ODataServiceDocumentElement serviceDocumentElement, ODataFormat format);

		// Token: 0x060017CD RID: 6093
		void ValidateCollectionItem(object item, bool isNullable);

		// Token: 0x060017CE RID: 6094
		void ValidateIsExpectedPrimitiveType(object value, IEdmPrimitiveTypeReference valuePrimitiveTypeReference, IEdmTypeReference expectedTypeReference);

		// Token: 0x060017CF RID: 6095
		void ValidateTypeReference(IEdmTypeReference typeReferenceFromMetadata, IEdmTypeReference typeReferenceFromValue);

		// Token: 0x060017D0 RID: 6096
		void ValidateTypeKind(EdmTypeKind actualTypeKind, EdmTypeKind expectedTypeKind, IEdmType edmType);

		// Token: 0x060017D1 RID: 6097
		IEdmCollectionTypeReference ValidateCollectionType(IEdmTypeReference typeReference);
	}
}
