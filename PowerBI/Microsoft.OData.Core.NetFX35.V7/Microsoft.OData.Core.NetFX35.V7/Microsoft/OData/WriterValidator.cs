using System;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData
{
	// Token: 0x020000D9 RID: 217
	internal class WriterValidator : IWriterValidator
	{
		// Token: 0x06000851 RID: 2129 RVA: 0x00017E6C File Offset: 0x0001606C
		internal WriterValidator(ODataMessageWriterSettings settings)
		{
			this.settings = settings;
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x00017E7C File Offset: 0x0001607C
		public IDuplicatePropertyNameChecker CreateDuplicatePropertyNameChecker()
		{
			if (!this.settings.ThrowOnDuplicatePropertyNames)
			{
				return new NullDuplicatePropertyNameChecker();
			}
			return new DuplicatePropertyNameChecker();
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x00017EA5 File Offset: 0x000160A5
		public virtual void ValidateResourceInNestedResourceInfo(IEdmStructuredType resourceType, IEdmStructuredType parentNavigationPropertyType)
		{
			if (this.settings.ThrowIfTypeConflictsWithMetadata)
			{
				WriterValidationUtils.ValidateNestedResource(resourceType, parentNavigationPropertyType);
			}
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x00017EBB File Offset: 0x000160BB
		public virtual void ValidateNestedResourceInfoHasCardinality(ODataNestedResourceInfo nestedResourceInfo)
		{
			WriterValidationUtils.ValidateNestedResourceInfoHasCardinality(nestedResourceInfo);
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x00017EC3 File Offset: 0x000160C3
		public virtual void ValidateOpenPropertyValue(string propertyName, object value)
		{
			ValidationUtils.ValidateOpenPropertyValue(propertyName, value);
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x00017ECC File Offset: 0x000160CC
		public virtual void ValidateIsExpectedPrimitiveType(object value, IEdmPrimitiveTypeReference valuePrimitiveTypeReference, IEdmTypeReference expectedTypeReference)
		{
			if (this.settings.ThrowIfTypeConflictsWithMetadata)
			{
				ValidationUtils.ValidateIsExpectedPrimitiveType(value, valuePrimitiveTypeReference, expectedTypeReference);
			}
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x00017EE4 File Offset: 0x000160E4
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

		// Token: 0x06000858 RID: 2136 RVA: 0x00017FAB File Offset: 0x000161AB
		public virtual void ValidateTypeKind(EdmTypeKind actualTypeKind, EdmTypeKind expectedTypeKind, bool? expectStructuredType, IEdmType edmType)
		{
			if (this.settings.ThrowIfTypeConflictsWithMetadata)
			{
				ValidationUtils.ValidateTypeKind(actualTypeKind, expectedTypeKind, expectStructuredType, (edmType == null) ? null : edmType.FullTypeName());
			}
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x00015C35 File Offset: 0x00013E35
		public virtual void ValidateMetadataResource(ODataResource resource, IEdmEntityType resourceType)
		{
			ValidationUtils.ValidateMediaResource(resource, resourceType);
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x00017FD0 File Offset: 0x000161D0
		public void ValidateNullPropertyValue(IEdmTypeReference expectedPropertyTypeReference, string propertyName, IEdmModel model)
		{
			if (this.settings.ThrowIfTypeConflictsWithMetadata)
			{
				WriterValidationUtils.ValidateNullPropertyValue(expectedPropertyTypeReference, propertyName, model);
			}
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x00017FE7 File Offset: 0x000161E7
		public void ValidateNullCollectionItem(IEdmTypeReference expectedItemType)
		{
			if (this.settings.ThrowIfTypeConflictsWithMetadata)
			{
				ValidationUtils.ValidateNullCollectionItem(expectedItemType);
			}
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x00017FFC File Offset: 0x000161FC
		public IEdmProperty ValidatePropertyDefined(string propertyName, IEdmStructuredType owningStructuredType)
		{
			return WriterValidationUtils.ValidatePropertyDefined(propertyName, owningStructuredType, this.settings.ThrowOnUndeclaredPropertyForNonOpenType);
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x00018010 File Offset: 0x00016210
		public IEdmNavigationProperty ValidateNestedResourceInfo(ODataNestedResourceInfo nestedResourceInfo, IEdmStructuredType declaringStructuredType, ODataPayloadKind? expandedPayloadKind)
		{
			return WriterValidationUtils.ValidateNestedResourceInfo(nestedResourceInfo, declaringStructuredType, expandedPayloadKind, this.settings.ThrowOnUndeclaredPropertyForNonOpenType);
		}

		// Token: 0x0400039A RID: 922
		private readonly ODataMessageWriterSettings settings;
	}
}
