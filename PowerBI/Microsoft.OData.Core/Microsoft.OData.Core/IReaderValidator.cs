using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData
{
	// Token: 0x02000017 RID: 23
	internal interface IReaderValidator
	{
		// Token: 0x06000102 RID: 258
		void ValidateMediaResource(ODataResourceBase resource, IEdmEntityType resourceType);

		// Token: 0x06000103 RID: 259
		PropertyAndAnnotationCollector CreatePropertyAndAnnotationCollector();

		// Token: 0x06000104 RID: 260
		void ValidateNullValue(IEdmTypeReference expectedTypeReference, bool validateNullValue, string propertyName, bool? isDynamicProperty);

		// Token: 0x06000105 RID: 261
		IEdmTypeReference ResolvePayloadTypeNameAndComputeTargetType(EdmTypeKind expectedTypeKind, bool? expectStructuredType, IEdmType defaultPrimitivePayloadType, IEdmTypeReference expectedTypeReference, string payloadTypeName, IEdmModel model, Func<EdmTypeKind> typeKindFromPayloadFunc, out EdmTypeKind targetTypeKind, out ODataTypeAnnotation typeAnnotation);

		// Token: 0x06000106 RID: 262
		IEdmProperty ValidatePropertyDefined(string propertyName, IEdmStructuredType owningStructuredType);

		// Token: 0x06000107 RID: 263
		void ValidateStreamReferenceProperty(IODataStreamReferenceInfo streamInfo, string propertyName, IEdmStructuredType structuredType, IEdmProperty streamEdmProperty);
	}
}
