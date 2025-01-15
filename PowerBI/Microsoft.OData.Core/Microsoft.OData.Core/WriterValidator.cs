using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData
{
	// Token: 0x020000EF RID: 239
	internal class WriterValidator : IWriterValidator
	{
		// Token: 0x06000B1D RID: 2845 RVA: 0x0001DB87 File Offset: 0x0001BD87
		internal WriterValidator(ODataMessageWriterSettings settings)
		{
			this.settings = settings;
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x0001DB98 File Offset: 0x0001BD98
		public IDuplicatePropertyNameChecker CreateDuplicatePropertyNameChecker()
		{
			if (!this.settings.ThrowOnDuplicatePropertyNames)
			{
				return new NullDuplicatePropertyNameChecker();
			}
			return new DuplicatePropertyNameChecker();
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x0001DBC1 File Offset: 0x0001BDC1
		public virtual void ValidateResourceInNestedResourceInfo(IEdmStructuredType resourceType, IEdmStructuredType parentNavigationPropertyType)
		{
			if (this.settings.ThrowIfTypeConflictsWithMetadata)
			{
				WriterValidationUtils.ValidateNestedResource(resourceType, parentNavigationPropertyType);
			}
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x0001DBD7 File Offset: 0x0001BDD7
		public virtual void ValidateNestedResourceInfoHasCardinality(ODataNestedResourceInfo nestedResourceInfo)
		{
			WriterValidationUtils.ValidateNestedResourceInfoHasCardinality(nestedResourceInfo);
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x0001DBDF File Offset: 0x0001BDDF
		public virtual void ValidateIsExpectedPrimitiveType(object value, IEdmPrimitiveTypeReference valuePrimitiveTypeReference, IEdmTypeReference expectedTypeReference)
		{
			if (this.settings.ThrowIfTypeConflictsWithMetadata)
			{
				ValidationUtils.ValidateIsExpectedPrimitiveType(value, valuePrimitiveTypeReference, expectedTypeReference);
			}
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x0001DBF8 File Offset: 0x0001BDF8
		public virtual void ValidateTypeReference(IEdmTypeReference typeReferenceFromMetadata, IEdmTypeReference typeReferenceFromValue)
		{
			if (this.settings.ThrowIfTypeConflictsWithMetadata)
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
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x0001DCBF File Offset: 0x0001BEBF
		public virtual void ValidateTypeKind(EdmTypeKind actualTypeKind, EdmTypeKind expectedTypeKind, bool? expectStructuredType, IEdmType edmType)
		{
			if (this.settings.ThrowIfTypeConflictsWithMetadata)
			{
				ValidationUtils.ValidateTypeKind(actualTypeKind, expectedTypeKind, expectStructuredType, (edmType == null) ? null : edmType.FullTypeName());
			}
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x00003FBE File Offset: 0x000021BE
		public virtual void ValidateMetadataResource(ODataResourceBase resource, IEdmEntityType resourceType)
		{
			ValidationUtils.ValidateMediaResource(resource, resourceType);
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x0001DCE4 File Offset: 0x0001BEE4
		public void ValidateNullPropertyValue(IEdmTypeReference expectedPropertyTypeReference, string propertyName, bool isTopLevel, IEdmModel model)
		{
			if (this.settings.ThrowIfTypeConflictsWithMetadata)
			{
				WriterValidationUtils.ValidateNullPropertyValue(expectedPropertyTypeReference, propertyName, model);
			}
			if (isTopLevel && (this.settings.LibraryCompatibility >= ODataLibraryCompatibility.Version7 || this.settings.Version >= ODataVersion.V401))
			{
				throw new ODataException(Strings.ODataMessageWriter_CannotWriteTopLevelNull);
			}
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x0001DD4B File Offset: 0x0001BF4B
		public void ValidateNullCollectionItem(IEdmTypeReference expectedItemType)
		{
			if (this.settings.ThrowIfTypeConflictsWithMetadata)
			{
				ValidationUtils.ValidateNullCollectionItem(expectedItemType);
			}
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x0001DD60 File Offset: 0x0001BF60
		public IEdmProperty ValidatePropertyDefined(string propertyName, IEdmStructuredType owningStructuredType)
		{
			return WriterValidationUtils.ValidatePropertyDefined(propertyName, owningStructuredType, this.settings.ThrowOnUndeclaredPropertyForNonOpenType);
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x0001DD74 File Offset: 0x0001BF74
		public IEdmNavigationProperty ValidateNestedResourceInfo(ODataNestedResourceInfo nestedResourceInfo, IEdmStructuredType declaringStructuredType, ODataPayloadKind? expandedPayloadKind)
		{
			return WriterValidationUtils.ValidateNestedResourceInfo(nestedResourceInfo, declaringStructuredType, expandedPayloadKind, this.settings.ThrowOnUndeclaredPropertyForNonOpenType);
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x0001DD89 File Offset: 0x0001BF89
		public void ValidateDerivedTypeConstraint(IEdmStructuredType resourceType, IEdmStructuredType metadataType, IEnumerable<string> derivedTypeConstraints, string itemKind, string itemName)
		{
			WriterValidationUtils.ValidateDerivedTypeConstraint(resourceType, metadataType, derivedTypeConstraints, itemKind, itemName);
		}

		// Token: 0x040003E4 RID: 996
		private readonly ODataMessageWriterSettings settings;
	}
}
