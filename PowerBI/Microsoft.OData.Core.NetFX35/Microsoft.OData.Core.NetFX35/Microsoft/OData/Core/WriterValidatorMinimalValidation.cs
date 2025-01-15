using System;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core
{
	// Token: 0x020002B9 RID: 697
	internal sealed class WriterValidatorMinimalValidation : IWriterValidator
	{
		// Token: 0x0600180A RID: 6154 RVA: 0x0005224C File Offset: 0x0005044C
		public void ValidateMessageWriterSettings(ODataMessageWriterSettings messageWriterSettings, bool writingResponse)
		{
		}

		// Token: 0x0600180B RID: 6155 RVA: 0x0005224E File Offset: 0x0005044E
		public void ValidatePropertyNotNull(ODataProperty property)
		{
		}

		// Token: 0x0600180C RID: 6156 RVA: 0x00052250 File Offset: 0x00050450
		public void ValidatePropertyName(string propertyName)
		{
		}

		// Token: 0x0600180D RID: 6157 RVA: 0x00052252 File Offset: 0x00050452
		public IEdmProperty ValidatePropertyDefined(string propertyName, IEdmStructuredType owningStructuredType, bool throwOnMissingProperty = true)
		{
			if (owningStructuredType == null)
			{
				return null;
			}
			return owningStructuredType.FindProperty(propertyName);
		}

		// Token: 0x0600180E RID: 6158 RVA: 0x00052260 File Offset: 0x00050460
		public IEdmNavigationProperty ValidateNavigationPropertyDefined(string propertyName, IEdmEntityType owningEntityType)
		{
			return (IEdmNavigationProperty)this.ValidatePropertyDefined(propertyName, owningEntityType, true);
		}

		// Token: 0x0600180F RID: 6159 RVA: 0x00052270 File Offset: 0x00050470
		public void ValidateEntryInExpandedLink(IEdmEntityType entryEntityType, IEdmEntityType parentNavigationPropertyType)
		{
		}

		// Token: 0x06001810 RID: 6160 RVA: 0x00052272 File Offset: 0x00050472
		public void ValidateCanWriteOperation(ODataOperation operation, bool writingResponse)
		{
		}

		// Token: 0x06001811 RID: 6161 RVA: 0x00052274 File Offset: 0x00050474
		public void ValidateFeedAtEnd(ODataFeed feed, bool writingRequest)
		{
		}

		// Token: 0x06001812 RID: 6162 RVA: 0x00052276 File Offset: 0x00050476
		public void ValidateEntryAtStart(ODataEntry entry)
		{
		}

		// Token: 0x06001813 RID: 6163 RVA: 0x00052278 File Offset: 0x00050478
		public void ValidateEntryAtEnd(ODataEntry entry)
		{
		}

		// Token: 0x06001814 RID: 6164 RVA: 0x0005227A File Offset: 0x0005047A
		public void ValidateStreamReferenceValue(ODataStreamReferenceValue streamReference, bool isDefaultStream)
		{
		}

		// Token: 0x06001815 RID: 6165 RVA: 0x0005227C File Offset: 0x0005047C
		public void ValidateStreamReferenceProperty(ODataProperty streamProperty, IEdmProperty edmProperty, bool writingResponse)
		{
		}

		// Token: 0x06001816 RID: 6166 RVA: 0x0005227E File Offset: 0x0005047E
		public void ValidateEntityReferenceLinkNotNull(ODataEntityReferenceLink entityReferenceLink)
		{
		}

		// Token: 0x06001817 RID: 6167 RVA: 0x00052280 File Offset: 0x00050480
		public void ValidateEntityReferenceLink(ODataEntityReferenceLink entityReferenceLink)
		{
		}

		// Token: 0x06001818 RID: 6168 RVA: 0x00052282 File Offset: 0x00050482
		public IEdmNavigationProperty ValidateNavigationLink(ODataNavigationLink navigationLink, IEdmEntityType declaringEntityType, ODataPayloadKind? expandedPayloadKind)
		{
			if (declaringEntityType != null)
			{
				return declaringEntityType.FindProperty(navigationLink.Name) as IEdmNavigationProperty;
			}
			return null;
		}

		// Token: 0x06001819 RID: 6169 RVA: 0x0005229A File Offset: 0x0005049A
		public void ValidateNavigationLinkUrlPresent(ODataNavigationLink navigationLink)
		{
		}

		// Token: 0x0600181A RID: 6170 RVA: 0x0005229C File Offset: 0x0005049C
		public void ValidateNavigationLinkHasCardinality(ODataNavigationLink navigationLink)
		{
		}

		// Token: 0x0600181B RID: 6171 RVA: 0x0005229E File Offset: 0x0005049E
		public void ValidateNullPropertyValue(IEdmTypeReference expectedPropertyTypeReference, string propertyName, ODataWriterBehavior writerBehavior, IEdmModel model)
		{
		}

		// Token: 0x0600181C RID: 6172 RVA: 0x000522A0 File Offset: 0x000504A0
		public void ValidateOpenPropertyValue(string propertyName, object value)
		{
		}

		// Token: 0x0600181D RID: 6173 RVA: 0x000522A2 File Offset: 0x000504A2
		public void ValidateServiceDocumentElement(ODataServiceDocumentElement serviceDocumentElement, ODataFormat format)
		{
		}

		// Token: 0x0600181E RID: 6174 RVA: 0x000522A4 File Offset: 0x000504A4
		public void ValidateCollectionItem(object item, bool isNullable)
		{
		}

		// Token: 0x0600181F RID: 6175 RVA: 0x000522A6 File Offset: 0x000504A6
		public void ValidateIsExpectedPrimitiveType(object value, IEdmPrimitiveTypeReference valuePrimitiveTypeReference, IEdmTypeReference expectedTypeReference)
		{
		}

		// Token: 0x06001820 RID: 6176 RVA: 0x000522A8 File Offset: 0x000504A8
		public void ValidateTypeReference(IEdmTypeReference typeReferenceFromMetadata, IEdmTypeReference typeReferenceFromValue)
		{
		}

		// Token: 0x06001821 RID: 6177 RVA: 0x000522AA File Offset: 0x000504AA
		public void ValidateTypeKind(EdmTypeKind actualTypeKind, EdmTypeKind expectedTypeKind, IEdmType edmType)
		{
		}

		// Token: 0x06001822 RID: 6178 RVA: 0x000522AC File Offset: 0x000504AC
		public IEdmCollectionTypeReference ValidateCollectionType(IEdmTypeReference typeReference)
		{
			return typeReference.AsCollectionOrNull();
		}
	}
}
