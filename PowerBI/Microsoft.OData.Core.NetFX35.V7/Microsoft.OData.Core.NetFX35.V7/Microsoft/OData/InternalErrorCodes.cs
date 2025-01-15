using System;

namespace Microsoft.OData
{
	// Token: 0x0200001C RID: 28
	internal enum InternalErrorCodes
	{
		// Token: 0x04000032 RID: 50
		ODataWriterCore_WriteEnd_UnreachableCodePath,
		// Token: 0x04000033 RID: 51
		ODataWriterCore_ValidateTransition_UnreachableCodePath,
		// Token: 0x04000034 RID: 52
		ODataWriterCore_Scope_Create_UnreachableCodePath,
		// Token: 0x04000035 RID: 53
		ODataWriterCore_PropertyAndAnnotationCollector,
		// Token: 0x04000036 RID: 54
		ODataWriterCore_ParentNestedResourceInfoScope,
		// Token: 0x04000037 RID: 55
		ODataUtils_VersionString_UnreachableCodePath,
		// Token: 0x04000038 RID: 56
		ODataUtilsInternal_IsPayloadKindSupported_UnreachableCodePath,
		// Token: 0x04000039 RID: 57
		ODataUtils_GetDefaultEncoding_UnreachableCodePath,
		// Token: 0x0400003A RID: 58
		ODataMessageWriter_WriteProperty,
		// Token: 0x0400003B RID: 59
		ODataMessageWriter_WriteEntityReferenceLink,
		// Token: 0x0400003C RID: 60
		ODataMessageWriter_WriteEntityReferenceLinks,
		// Token: 0x0400003D RID: 61
		ODataMessageWriter_WriteError,
		// Token: 0x0400003E RID: 62
		ODataMessageWriter_WriteServiceDocument,
		// Token: 0x0400003F RID: 63
		ODataMessageWriter_WriteMetadataDocument,
		// Token: 0x04000040 RID: 64
		ODataCollectionWriter_CreateCollectionWriter_UnreachableCodePath,
		// Token: 0x04000041 RID: 65
		ODataCollectionWriterCore_ValidateTransition_UnreachableCodePath,
		// Token: 0x04000042 RID: 66
		ODataCollectionWriterCore_WriteEnd_UnreachableCodePath,
		// Token: 0x04000043 RID: 67
		ODataParameterWriter_CannotCreateParameterWriterForFormat,
		// Token: 0x04000044 RID: 68
		ODataParameterWriterCore_ValidateTransition_InvalidTransitionFromStart,
		// Token: 0x04000045 RID: 69
		ODataParameterWriterCore_ValidateTransition_InvalidTransitionFromCanWriteParameter,
		// Token: 0x04000046 RID: 70
		ODataParameterWriterCore_ValidateTransition_InvalidTransitionFromActiveSubWriter,
		// Token: 0x04000047 RID: 71
		ODataParameterWriterCore_ValidateTransition_InvalidTransitionFromCompleted,
		// Token: 0x04000048 RID: 72
		ODataParameterWriterCore_ValidateTransition_InvalidTransitionFromError,
		// Token: 0x04000049 RID: 73
		ODataParameterWriterCore_ValidateTransition_UnreachableCodePath,
		// Token: 0x0400004A RID: 74
		ODataParameterWriterCore_WriteEndImplementation_UnreachableCodePath,
		// Token: 0x0400004B RID: 75
		QueryPathValidator_ValidateSegment_Root,
		// Token: 0x0400004C RID: 76
		QueryPathValidator_ValidateSegment_NonRoot,
		// Token: 0x0400004D RID: 77
		ODataBatchWriter_ValidateTransition_UnreachableCodePath,
		// Token: 0x0400004E RID: 78
		ODataBatchWriterUtils_HttpMethod_ToText_UnreachableCodePath,
		// Token: 0x0400004F RID: 79
		ODataBatchReader_ReadImplementation,
		// Token: 0x04000050 RID: 80
		ODataBatchReader_GetEndBoundary_Completed,
		// Token: 0x04000051 RID: 81
		ODataBatchReader_GetEndBoundary_Exception,
		// Token: 0x04000052 RID: 82
		ODataBatchReader_GetEndBoundary_UnknownValue,
		// Token: 0x04000053 RID: 83
		ODataBatchReaderStream_SkipToBoundary,
		// Token: 0x04000054 RID: 84
		ODataBatchReaderStream_ReadLine,
		// Token: 0x04000055 RID: 85
		ODataBatchReaderStream_ReadWithDelimiter,
		// Token: 0x04000056 RID: 86
		ODataBatchReaderStreamBuffer_ScanForBoundary,
		// Token: 0x04000057 RID: 87
		ODataBatchReaderStreamBuffer_ReadWithLength,
		// Token: 0x04000058 RID: 88
		JsonReader_Read,
		// Token: 0x04000059 RID: 89
		ODataReader_CreateReader_UnreachableCodePath,
		// Token: 0x0400005A RID: 90
		ODataReaderCore_ReadImplementation,
		// Token: 0x0400005B RID: 91
		ODataReaderCoreAsync_ReadAsynchronously,
		// Token: 0x0400005C RID: 92
		ODataCollectionReader_CreateReader_UnreachableCodePath,
		// Token: 0x0400005D RID: 93
		ODataCollectionReaderCore_ReadImplementation,
		// Token: 0x0400005E RID: 94
		ODataCollectionReaderCoreAsync_ReadAsynchronously,
		// Token: 0x0400005F RID: 95
		ODataParameterReaderCore_ReadImplementation,
		// Token: 0x04000060 RID: 96
		ODataParameterReaderCoreAsync_ReadAsynchronously,
		// Token: 0x04000061 RID: 97
		ODataParameterReaderCore_ValueMustBePrimitiveOrNull,
		// Token: 0x04000062 RID: 98
		ODataRawValueUtils_ConvertStringToPrimitive,
		// Token: 0x04000063 RID: 99
		EdmCoreModel_PrimitiveType,
		// Token: 0x04000064 RID: 100
		ReaderValidationUtils_ResolveAndValidateTypeName_Strict_TypeKind,
		// Token: 0x04000065 RID: 101
		ReaderValidationUtils_ResolveAndValidateTypeName_Lax_TypeKind,
		// Token: 0x04000066 RID: 102
		ODataMetadataFormat_CreateOutputContextAsync,
		// Token: 0x04000067 RID: 103
		ODataMetadataFormat_CreateInputContextAsync,
		// Token: 0x04000068 RID: 104
		ODataModelFunctions_UnsupportedMethodOrProperty,
		// Token: 0x04000069 RID: 105
		ODataJsonLightPropertyAndValueDeserializer_ReadPropertyValue,
		// Token: 0x0400006A RID: 106
		ODataJsonLightPropertyAndValueDeserializer_GetNonEntityValueKind,
		// Token: 0x0400006B RID: 107
		ODataJsonLightReader_ReadResourceStart,
		// Token: 0x0400006C RID: 108
		ODataJsonLightResourceDeserializer_ReadTopLevelResourceSetAnnotations,
		// Token: 0x0400006D RID: 109
		ODataJsonLightCollectionDeserializer_ReadCollectionStart,
		// Token: 0x0400006E RID: 110
		ODataJsonLightCollectionDeserializer_ReadCollectionStart_TypeKindFromPayloadFunc,
		// Token: 0x0400006F RID: 111
		ODataJsonLightCollectionDeserializer_ReadCollectionEnd,
		// Token: 0x04000070 RID: 112
		ODataJsonLightEntityReferenceLinkDeserializer_ReadSingleEntityReferenceLink,
		// Token: 0x04000071 RID: 113
		ODataJsonLightEntityReferenceLinkDeserializer_ReadEntityReferenceLinksAnnotations,
		// Token: 0x04000072 RID: 114
		ODataJsonLightParameterDeserializer_ReadNextParameter,
		// Token: 0x04000073 RID: 115
		EdmTypeWriterResolver_GetReturnTypeForOperationImportGroup,
		// Token: 0x04000074 RID: 116
		ODataVersionCache_UnknownVersion
	}
}
