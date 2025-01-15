using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData
{
	// Token: 0x020000CC RID: 204
	internal class ReaderValidator : IReaderValidator
	{
		// Token: 0x060007CB RID: 1995 RVA: 0x00015C26 File Offset: 0x00013E26
		internal ReaderValidator(ODataMessageReaderSettings settings)
		{
			this.settings = settings;
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x00015C35 File Offset: 0x00013E35
		public virtual void ValidateMediaResource(ODataResource resource, IEdmEntityType resourceType)
		{
			ValidationUtils.ValidateMediaResource(resource, resourceType);
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x00015C3E File Offset: 0x00013E3E
		public PropertyAndAnnotationCollector CreatePropertyAndAnnotationCollector()
		{
			return new PropertyAndAnnotationCollector(this.settings.ThrowOnDuplicatePropertyNames);
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x00015C50 File Offset: 0x00013E50
		public void ValidateNullValue(IEdmTypeReference expectedTypeReference, bool validateNullValue, string propertyName, bool? isDynamicProperty)
		{
			if (this.settings.ThrowIfTypeConflictsWithMetadata)
			{
				ReaderValidationUtils.ValidateNullValue(expectedTypeReference, this.settings.EnablePrimitiveTypeConversion, validateNullValue, propertyName, isDynamicProperty);
			}
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x00015C74 File Offset: 0x00013E74
		public IEdmTypeReference ResolvePayloadTypeNameAndComputeTargetType(EdmTypeKind expectedTypeKind, bool? expectStructuredType, IEdmType defaultPrimitivePayloadType, IEdmTypeReference expectedTypeReference, string payloadTypeName, IEdmModel model, Func<EdmTypeKind> typeKindFromPayloadFunc, out EdmTypeKind targetTypeKind, out ODataTypeAnnotation typeAnnotation)
		{
			return ReaderValidationUtils.ResolvePayloadTypeNameAndComputeTargetType(expectedTypeKind, expectStructuredType, defaultPrimitivePayloadType, expectedTypeReference, payloadTypeName, model, this.settings.ClientCustomTypeResolver, this.settings.ThrowIfTypeConflictsWithMetadata, this.settings.EnablePrimitiveTypeConversion, typeKindFromPayloadFunc, out targetTypeKind, out typeAnnotation);
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x00015CB6 File Offset: 0x00013EB6
		public IEdmProperty ValidatePropertyDefined(string propertyName, IEdmStructuredType owningStructuredType)
		{
			return ReaderValidationUtils.ValidatePropertyDefined(propertyName, owningStructuredType, this.settings.ThrowOnUndeclaredPropertyForNonOpenType);
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x00015CCA File Offset: 0x00013ECA
		public void ValidateStreamReferenceProperty(ODataProperty streamProperty, IEdmStructuredType structuredType, IEdmProperty streamEdmProperty)
		{
			ReaderValidationUtils.ValidateStreamReferenceProperty(streamProperty, structuredType, streamEdmProperty, this.settings.ThrowOnUndeclaredPropertyForNonOpenType);
		}

		// Token: 0x0400032E RID: 814
		private readonly ODataMessageReaderSettings settings;
	}
}
