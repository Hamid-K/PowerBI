using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData
{
	// Token: 0x020000C0 RID: 192
	internal interface IReaderValidator
	{
		// Token: 0x0600076E RID: 1902
		void ValidateMediaResource(ODataResource resource, IEdmEntityType resourceType);

		// Token: 0x0600076F RID: 1903
		PropertyAndAnnotationCollector CreatePropertyAndAnnotationCollector();

		// Token: 0x06000770 RID: 1904
		void ValidateNullValue(IEdmTypeReference expectedTypeReference, bool validateNullValue, string propertyName, bool? isDynamicProperty);

		// Token: 0x06000771 RID: 1905
		IEdmTypeReference ResolvePayloadTypeNameAndComputeTargetType(EdmTypeKind expectedTypeKind, bool? expectStructuredType, IEdmType defaultPrimitivePayloadType, IEdmTypeReference expectedTypeReference, string payloadTypeName, IEdmModel model, Func<EdmTypeKind> typeKindFromPayloadFunc, out EdmTypeKind targetTypeKind, out ODataTypeAnnotation typeAnnotation);

		// Token: 0x06000772 RID: 1906
		IEdmProperty ValidatePropertyDefined(string propertyName, IEdmStructuredType owningStructuredType);

		// Token: 0x06000773 RID: 1907
		void ValidateStreamReferenceProperty(ODataProperty streamProperty, IEdmStructuredType structuredType, IEdmProperty streamEdmProperty);
	}
}
