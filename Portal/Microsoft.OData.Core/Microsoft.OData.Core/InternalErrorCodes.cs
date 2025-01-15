using System;

namespace Microsoft.OData
{
	// Token: 0x02000045 RID: 69
	internal enum InternalErrorCodes
	{
		// Token: 0x040000A2 RID: 162
		ODataWriterCore_WriteEnd_UnreachableCodePath,
		// Token: 0x040000A3 RID: 163
		ODataWriterCore_ValidateTransition_UnreachableCodePath,
		// Token: 0x040000A4 RID: 164
		ODataWriterCore_Scope_Create_UnreachableCodePath,
		// Token: 0x040000A5 RID: 165
		ODataWriterCore_PropertyAndAnnotationCollector,
		// Token: 0x040000A6 RID: 166
		ODataWriterCore_ParentNestedResourceInfoScope,
		// Token: 0x040000A7 RID: 167
		ODataUtils_VersionString_UnreachableCodePath,
		// Token: 0x040000A8 RID: 168
		ODataUtilsInternal_IsPayloadKindSupported_UnreachableCodePath,
		// Token: 0x040000A9 RID: 169
		ODataUtils_GetDefaultEncoding_UnreachableCodePath,
		// Token: 0x040000AA RID: 170
		ODataMessageWriter_WriteProperty,
		// Token: 0x040000AB RID: 171
		ODataMessageWriter_WriteEntityReferenceLink,
		// Token: 0x040000AC RID: 172
		ODataMessageWriter_WriteEntityReferenceLinks,
		// Token: 0x040000AD RID: 173
		ODataMessageWriter_WriteError,
		// Token: 0x040000AE RID: 174
		ODataMessageWriter_WriteServiceDocument,
		// Token: 0x040000AF RID: 175
		ODataMessageWriter_WriteMetadataDocument,
		// Token: 0x040000B0 RID: 176
		ODataCollectionWriter_CreateCollectionWriter_UnreachableCodePath,
		// Token: 0x040000B1 RID: 177
		ODataCollectionWriterCore_ValidateTransition_UnreachableCodePath,
		// Token: 0x040000B2 RID: 178
		ODataCollectionWriterCore_WriteEnd_UnreachableCodePath,
		// Token: 0x040000B3 RID: 179
		ODataParameterWriter_CannotCreateParameterWriterForFormat,
		// Token: 0x040000B4 RID: 180
		ODataParameterWriterCore_ValidateTransition_InvalidTransitionFromStart,
		// Token: 0x040000B5 RID: 181
		ODataParameterWriterCore_ValidateTransition_InvalidTransitionFromCanWriteParameter,
		// Token: 0x040000B6 RID: 182
		ODataParameterWriterCore_ValidateTransition_InvalidTransitionFromActiveSubWriter,
		// Token: 0x040000B7 RID: 183
		ODataParameterWriterCore_ValidateTransition_InvalidTransitionFromCompleted,
		// Token: 0x040000B8 RID: 184
		ODataParameterWriterCore_ValidateTransition_InvalidTransitionFromError,
		// Token: 0x040000B9 RID: 185
		ODataParameterWriterCore_ValidateTransition_UnreachableCodePath,
		// Token: 0x040000BA RID: 186
		ODataParameterWriterCore_WriteEndImplementation_UnreachableCodePath,
		// Token: 0x040000BB RID: 187
		QueryPathValidator_ValidateSegment_Root,
		// Token: 0x040000BC RID: 188
		QueryPathValidator_ValidateSegment_NonRoot,
		// Token: 0x040000BD RID: 189
		ODataBatchWriter_ValidateTransition_UnreachableCodePath,
		// Token: 0x040000BE RID: 190
		ODataBatchWriterUtils_HttpMethod_ToText_UnreachableCodePath,
		// Token: 0x040000BF RID: 191
		ODataBatchReader_ReadImplementation,
		// Token: 0x040000C0 RID: 192
		ODataBatchReader_GetEndBoundary_Completed,
		// Token: 0x040000C1 RID: 193
		ODataBatchReader_GetEndBoundary_Exception,
		// Token: 0x040000C2 RID: 194
		ODataBatchReader_GetEndBoundary_UnknownValue,
		// Token: 0x040000C3 RID: 195
		ODataBatchReaderStream_SkipToBoundary,
		// Token: 0x040000C4 RID: 196
		ODataBatchReaderStream_ReadLine,
		// Token: 0x040000C5 RID: 197
		ODataBatchReaderStream_ReadWithDelimiter,
		// Token: 0x040000C6 RID: 198
		ODataBatchReaderStreamBuffer_ScanForBoundary,
		// Token: 0x040000C7 RID: 199
		ODataBatchReaderStreamBuffer_ReadWithLength,
		// Token: 0x040000C8 RID: 200
		JsonReader_Read,
		// Token: 0x040000C9 RID: 201
		ODataReader_CreateReader_UnreachableCodePath,
		// Token: 0x040000CA RID: 202
		ODataReaderCore_ReadImplementation,
		// Token: 0x040000CB RID: 203
		ODataReaderCoreAsync_ReadAsynchronously,
		// Token: 0x040000CC RID: 204
		ODataCollectionReader_CreateReader_UnreachableCodePath,
		// Token: 0x040000CD RID: 205
		ODataCollectionReaderCore_ReadImplementation,
		// Token: 0x040000CE RID: 206
		ODataCollectionReaderCoreAsync_ReadAsynchronously,
		// Token: 0x040000CF RID: 207
		ODataParameterReaderCore_ReadImplementation,
		// Token: 0x040000D0 RID: 208
		ODataParameterReaderCoreAsync_ReadAsynchronously,
		// Token: 0x040000D1 RID: 209
		ODataParameterReaderCore_ValueMustBePrimitiveOrNull,
		// Token: 0x040000D2 RID: 210
		ODataRawValueUtils_ConvertStringToPrimitive,
		// Token: 0x040000D3 RID: 211
		EdmCoreModel_PrimitiveType,
		// Token: 0x040000D4 RID: 212
		ReaderValidationUtils_ResolveAndValidateTypeName_Strict_TypeKind,
		// Token: 0x040000D5 RID: 213
		ReaderValidationUtils_ResolveAndValidateTypeName_Lax_TypeKind,
		// Token: 0x040000D6 RID: 214
		ODataMetadataFormat_CreateOutputContextAsync,
		// Token: 0x040000D7 RID: 215
		ODataMetadataFormat_CreateInputContextAsync,
		// Token: 0x040000D8 RID: 216
		ODataModelFunctions_UnsupportedMethodOrProperty,
		// Token: 0x040000D9 RID: 217
		ODataJsonLightPropertyAndValueDeserializer_ReadPropertyValue,
		// Token: 0x040000DA RID: 218
		ODataJsonLightPropertyAndValueDeserializer_GetNonEntityValueKind,
		// Token: 0x040000DB RID: 219
		ODataJsonLightReader_ReadResourceStart,
		// Token: 0x040000DC RID: 220
		ODataJsonLightResourceDeserializer_ReadTopLevelResourceSetAnnotations,
		// Token: 0x040000DD RID: 221
		ODataJsonLightCollectionDeserializer_ReadCollectionStart,
		// Token: 0x040000DE RID: 222
		ODataJsonLightCollectionDeserializer_ReadCollectionStart_TypeKindFromPayloadFunc,
		// Token: 0x040000DF RID: 223
		ODataJsonLightCollectionDeserializer_ReadCollectionEnd,
		// Token: 0x040000E0 RID: 224
		ODataJsonLightEntityReferenceLinkDeserializer_ReadSingleEntityReferenceLink,
		// Token: 0x040000E1 RID: 225
		ODataJsonLightEntityReferenceLinkDeserializer_ReadEntityReferenceLinksAnnotations,
		// Token: 0x040000E2 RID: 226
		ODataJsonLightParameterDeserializer_ReadNextParameter,
		// Token: 0x040000E3 RID: 227
		EdmTypeWriterResolver_GetReturnTypeForOperationImportGroup,
		// Token: 0x040000E4 RID: 228
		ODataVersionCache_UnknownVersion
	}
}
