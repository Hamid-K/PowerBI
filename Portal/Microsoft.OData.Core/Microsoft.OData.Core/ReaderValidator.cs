using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData
{
	// Token: 0x02000025 RID: 37
	internal class ReaderValidator : IReaderValidator
	{
		// Token: 0x0600016C RID: 364 RVA: 0x00003FAF File Offset: 0x000021AF
		internal ReaderValidator(ODataMessageReaderSettings settings)
		{
			this.settings = settings;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00003FBE File Offset: 0x000021BE
		public virtual void ValidateMediaResource(ODataResourceBase resource, IEdmEntityType resourceType)
		{
			ValidationUtils.ValidateMediaResource(resource, resourceType);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00003FC7 File Offset: 0x000021C7
		public PropertyAndAnnotationCollector CreatePropertyAndAnnotationCollector()
		{
			return new PropertyAndAnnotationCollector(this.settings.ThrowOnDuplicatePropertyNames);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00003FD9 File Offset: 0x000021D9
		public void ValidateNullValue(IEdmTypeReference expectedTypeReference, bool validateNullValue, string propertyName, bool? isDynamicProperty)
		{
			if (this.settings.ThrowIfTypeConflictsWithMetadata)
			{
				ReaderValidationUtils.ValidateNullValue(expectedTypeReference, this.settings.EnablePrimitiveTypeConversion, validateNullValue, propertyName, isDynamicProperty);
			}
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00004000 File Offset: 0x00002200
		public IEdmTypeReference ResolvePayloadTypeNameAndComputeTargetType(EdmTypeKind expectedTypeKind, bool? expectStructuredType, IEdmType defaultPrimitivePayloadType, IEdmTypeReference expectedTypeReference, string payloadTypeName, IEdmModel model, Func<EdmTypeKind> typeKindFromPayloadFunc, out EdmTypeKind targetTypeKind, out ODataTypeAnnotation typeAnnotation)
		{
			return ReaderValidationUtils.ResolvePayloadTypeNameAndComputeTargetType(expectedTypeKind, expectStructuredType, defaultPrimitivePayloadType, expectedTypeReference, payloadTypeName, model, this.settings.ClientCustomTypeResolver, this.settings.ThrowIfTypeConflictsWithMetadata, this.settings.EnablePrimitiveTypeConversion, typeKindFromPayloadFunc, out targetTypeKind, out typeAnnotation);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00004042 File Offset: 0x00002242
		public IEdmProperty ValidatePropertyDefined(string propertyName, IEdmStructuredType owningStructuredType)
		{
			return ReaderValidationUtils.ValidatePropertyDefined(propertyName, owningStructuredType, this.settings.ThrowOnUndeclaredPropertyForNonOpenType);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00004056 File Offset: 0x00002256
		public void ValidateStreamReferenceProperty(IODataStreamReferenceInfo streamInfo, string propertyName, IEdmStructuredType structuredType, IEdmProperty streamEdmProperty)
		{
			ReaderValidationUtils.ValidateStreamReferenceProperty(streamInfo, propertyName, structuredType, streamEdmProperty, this.settings.ThrowOnUndeclaredPropertyForNonOpenType);
		}

		// Token: 0x0400006F RID: 111
		private readonly ODataMessageReaderSettings settings;
	}
}
