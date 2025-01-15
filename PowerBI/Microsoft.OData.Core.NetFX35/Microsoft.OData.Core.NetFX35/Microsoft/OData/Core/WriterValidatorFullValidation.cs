using System;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core
{
	// Token: 0x020002B8 RID: 696
	internal sealed class WriterValidatorFullValidation : IWriterValidator
	{
		// Token: 0x060017F0 RID: 6128 RVA: 0x000520A8 File Offset: 0x000502A8
		public void ValidateMessageWriterSettings(ODataMessageWriterSettings messageWriterSettings, bool writingResponse)
		{
			WriterValidationUtils.ValidateMessageWriterSettings(messageWriterSettings, writingResponse);
		}

		// Token: 0x060017F1 RID: 6129 RVA: 0x000520B1 File Offset: 0x000502B1
		public void ValidatePropertyNotNull(ODataProperty property)
		{
			WriterValidationUtils.ValidatePropertyNotNull(property);
		}

		// Token: 0x060017F2 RID: 6130 RVA: 0x000520B9 File Offset: 0x000502B9
		public void ValidatePropertyName(string propertyName)
		{
			WriterValidationUtils.ValidatePropertyName(propertyName);
		}

		// Token: 0x060017F3 RID: 6131 RVA: 0x000520C1 File Offset: 0x000502C1
		public IEdmProperty ValidatePropertyDefined(string propertyName, IEdmStructuredType owningStructuredType, bool throwOnMissingProperty = true)
		{
			return WriterValidationUtils.ValidatePropertyDefined(propertyName, owningStructuredType, throwOnMissingProperty);
		}

		// Token: 0x060017F4 RID: 6132 RVA: 0x000520CB File Offset: 0x000502CB
		public IEdmNavigationProperty ValidateNavigationPropertyDefined(string propertyName, IEdmEntityType owningEntityType)
		{
			return WriterValidationUtils.ValidateNavigationPropertyDefined(propertyName, owningEntityType);
		}

		// Token: 0x060017F5 RID: 6133 RVA: 0x000520D4 File Offset: 0x000502D4
		public void ValidateEntryInExpandedLink(IEdmEntityType entryEntityType, IEdmEntityType parentNavigationPropertyType)
		{
			WriterValidationUtils.ValidateEntryInExpandedLink(entryEntityType, parentNavigationPropertyType);
		}

		// Token: 0x060017F6 RID: 6134 RVA: 0x000520DD File Offset: 0x000502DD
		public void ValidateCanWriteOperation(ODataOperation operation, bool writingResponse)
		{
			WriterValidationUtils.ValidateCanWriteOperation(operation, writingResponse);
		}

		// Token: 0x060017F7 RID: 6135 RVA: 0x000520E6 File Offset: 0x000502E6
		public void ValidateFeedAtEnd(ODataFeed feed, bool writingRequest)
		{
			WriterValidationUtils.ValidateFeedAtEnd(feed, writingRequest);
		}

		// Token: 0x060017F8 RID: 6136 RVA: 0x000520EF File Offset: 0x000502EF
		public void ValidateEntryAtStart(ODataEntry entry)
		{
			WriterValidationUtils.ValidateEntryAtStart(entry);
		}

		// Token: 0x060017F9 RID: 6137 RVA: 0x000520F7 File Offset: 0x000502F7
		public void ValidateEntryAtEnd(ODataEntry entry)
		{
			WriterValidationUtils.ValidateEntryAtEnd(entry);
		}

		// Token: 0x060017FA RID: 6138 RVA: 0x000520FF File Offset: 0x000502FF
		public void ValidateStreamReferenceValue(ODataStreamReferenceValue streamReference, bool isDefaultStream)
		{
			WriterValidationUtils.ValidateStreamReferenceValue(streamReference, isDefaultStream);
		}

		// Token: 0x060017FB RID: 6139 RVA: 0x00052108 File Offset: 0x00050308
		public void ValidateStreamReferenceProperty(ODataProperty streamProperty, IEdmProperty edmProperty, bool writingResponse)
		{
			WriterValidationUtils.ValidateStreamReferenceProperty(streamProperty, edmProperty, writingResponse);
		}

		// Token: 0x060017FC RID: 6140 RVA: 0x00052112 File Offset: 0x00050312
		public void ValidateEntityReferenceLinkNotNull(ODataEntityReferenceLink entityReferenceLink)
		{
			WriterValidationUtils.ValidateEntityReferenceLinkNotNull(entityReferenceLink);
		}

		// Token: 0x060017FD RID: 6141 RVA: 0x0005211A File Offset: 0x0005031A
		public void ValidateEntityReferenceLink(ODataEntityReferenceLink entityReferenceLink)
		{
			WriterValidationUtils.ValidateEntityReferenceLink(entityReferenceLink);
		}

		// Token: 0x060017FE RID: 6142 RVA: 0x00052122 File Offset: 0x00050322
		public IEdmNavigationProperty ValidateNavigationLink(ODataNavigationLink navigationLink, IEdmEntityType declaringEntityType, ODataPayloadKind? expandedPayloadKind)
		{
			return WriterValidationUtils.ValidateNavigationLink(navigationLink, declaringEntityType, expandedPayloadKind);
		}

		// Token: 0x060017FF RID: 6143 RVA: 0x0005212C File Offset: 0x0005032C
		public void ValidateNavigationLinkUrlPresent(ODataNavigationLink navigationLink)
		{
			WriterValidationUtils.ValidateNavigationLinkUrlPresent(navigationLink);
		}

		// Token: 0x06001800 RID: 6144 RVA: 0x00052134 File Offset: 0x00050334
		public void ValidateNavigationLinkHasCardinality(ODataNavigationLink navigationLink)
		{
			WriterValidationUtils.ValidateNavigationLinkHasCardinality(navigationLink);
		}

		// Token: 0x06001801 RID: 6145 RVA: 0x0005213C File Offset: 0x0005033C
		public void ValidateNullPropertyValue(IEdmTypeReference expectedPropertyTypeReference, string propertyName, ODataWriterBehavior writerBehavior, IEdmModel model)
		{
			WriterValidationUtils.ValidateNullPropertyValue(expectedPropertyTypeReference, propertyName, writerBehavior, model);
		}

		// Token: 0x06001802 RID: 6146 RVA: 0x00052148 File Offset: 0x00050348
		public void ValidateOpenPropertyValue(string propertyName, object value)
		{
			ValidationUtils.ValidateOpenPropertyValue(propertyName, value);
		}

		// Token: 0x06001803 RID: 6147 RVA: 0x00052151 File Offset: 0x00050351
		public void ValidateServiceDocumentElement(ODataServiceDocumentElement serviceDocumentElement, ODataFormat format)
		{
			ValidationUtils.ValidateServiceDocumentElement(serviceDocumentElement, format);
		}

		// Token: 0x06001804 RID: 6148 RVA: 0x0005215A File Offset: 0x0005035A
		public void ValidateCollectionItem(object item, bool isNullable)
		{
			ValidationUtils.ValidateCollectionItem(item, isNullable);
		}

		// Token: 0x06001805 RID: 6149 RVA: 0x00052163 File Offset: 0x00050363
		public void ValidateIsExpectedPrimitiveType(object value, IEdmPrimitiveTypeReference valuePrimitiveTypeReference, IEdmTypeReference expectedTypeReference)
		{
			ValidationUtils.ValidateIsExpectedPrimitiveType(value, valuePrimitiveTypeReference, expectedTypeReference);
		}

		// Token: 0x06001806 RID: 6150 RVA: 0x00052170 File Offset: 0x00050370
		public void ValidateTypeReference(IEdmTypeReference typeReferenceFromMetadata, IEdmTypeReference typeReferenceFromValue)
		{
			if (typeReferenceFromValue.IsODataPrimitiveTypeKind())
			{
				ValidationUtils.ValidateMetadataPrimitiveType(typeReferenceFromMetadata, typeReferenceFromValue);
				return;
			}
			if (typeReferenceFromMetadata.IsEntity())
			{
				ValidationUtils.ValidateEntityTypeIsAssignable((IEdmEntityTypeReference)typeReferenceFromMetadata, (IEdmEntityTypeReference)typeReferenceFromValue);
				return;
			}
			if (typeReferenceFromMetadata.IsComplex())
			{
				ValidationUtils.ValidateComplexTypeIsAssignable(typeReferenceFromMetadata.Definition as IEdmComplexType, typeReferenceFromValue.Definition as IEdmComplexType);
				return;
			}
			if (typeReferenceFromMetadata.IsCollection())
			{
				if (!typeReferenceFromMetadata.Definition.IsElementTypeEquivalentTo(typeReferenceFromValue.Definition))
				{
					throw new ODataException(Strings.ValidationUtils_IncompatibleType(typeReferenceFromValue.FullName(), typeReferenceFromMetadata.FullName()));
				}
			}
			else if (typeReferenceFromMetadata.FullName() != typeReferenceFromValue.FullName())
			{
				throw new ODataException(Strings.ValidationUtils_IncompatibleType(typeReferenceFromValue.FullName(), typeReferenceFromMetadata.FullName()));
			}
		}

		// Token: 0x06001807 RID: 6151 RVA: 0x00052227 File Offset: 0x00050427
		public void ValidateTypeKind(EdmTypeKind actualTypeKind, EdmTypeKind expectedTypeKind, IEdmType edmType)
		{
			ValidationUtils.ValidateTypeKind(actualTypeKind, expectedTypeKind, (edmType == null) ? null : edmType.FullTypeName());
		}

		// Token: 0x06001808 RID: 6152 RVA: 0x0005223C File Offset: 0x0005043C
		public IEdmCollectionTypeReference ValidateCollectionType(IEdmTypeReference typeReference)
		{
			return ValidationUtils.ValidateCollectionType(typeReference);
		}
	}
}
