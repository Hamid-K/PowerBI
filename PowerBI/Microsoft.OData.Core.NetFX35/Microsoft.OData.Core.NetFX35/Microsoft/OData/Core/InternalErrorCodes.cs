using System;

namespace Microsoft.OData.Core
{
	// Token: 0x020000A7 RID: 167
	internal enum InternalErrorCodes
	{
		// Token: 0x04000289 RID: 649
		ODataWriterCore_WriteEnd_UnreachableCodePath,
		// Token: 0x0400028A RID: 650
		ODataWriterCore_ValidateTransition_UnreachableCodePath,
		// Token: 0x0400028B RID: 651
		ODataWriterCore_Scope_Create_UnreachableCodePath,
		// Token: 0x0400028C RID: 652
		ODataWriterCore_DuplicatePropertyNamesChecker,
		// Token: 0x0400028D RID: 653
		ODataWriterCore_ParentNavigationLinkScope,
		// Token: 0x0400028E RID: 654
		ODataUtils_VersionString_UnreachableCodePath,
		// Token: 0x0400028F RID: 655
		ODataUtilsInternal_IsPayloadKindSupported_UnreachableCodePath,
		// Token: 0x04000290 RID: 656
		ODataUtils_GetDefaultEncoding_UnreachableCodePath,
		// Token: 0x04000291 RID: 657
		ODataMessageWriter_WriteProperty,
		// Token: 0x04000292 RID: 658
		ODataMessageWriter_WriteEntityReferenceLink,
		// Token: 0x04000293 RID: 659
		ODataMessageWriter_WriteEntityReferenceLinks,
		// Token: 0x04000294 RID: 660
		ODataMessageWriter_WriteError,
		// Token: 0x04000295 RID: 661
		ODataMessageWriter_WriteServiceDocument,
		// Token: 0x04000296 RID: 662
		ODataMessageWriter_WriteMetadataDocument,
		// Token: 0x04000297 RID: 663
		ODataAtomConvert_ToString,
		// Token: 0x04000298 RID: 664
		ODataCollectionWriter_CreateCollectionWriter_UnreachableCodePath,
		// Token: 0x04000299 RID: 665
		ODataCollectionWriterCore_ValidateTransition_UnreachableCodePath,
		// Token: 0x0400029A RID: 666
		ODataCollectionWriterCore_WriteEnd_UnreachableCodePath,
		// Token: 0x0400029B RID: 667
		ODataParameterWriter_CannotCreateParameterWriterForFormat,
		// Token: 0x0400029C RID: 668
		ODataParameterWriterCore_ValidateTransition_InvalidTransitionFromStart,
		// Token: 0x0400029D RID: 669
		ODataParameterWriterCore_ValidateTransition_InvalidTransitionFromCanWriteParameter,
		// Token: 0x0400029E RID: 670
		ODataParameterWriterCore_ValidateTransition_InvalidTransitionFromActiveSubWriter,
		// Token: 0x0400029F RID: 671
		ODataParameterWriterCore_ValidateTransition_InvalidTransitionFromCompleted,
		// Token: 0x040002A0 RID: 672
		ODataParameterWriterCore_ValidateTransition_InvalidTransitionFromError,
		// Token: 0x040002A1 RID: 673
		ODataParameterWriterCore_ValidateTransition_UnreachableCodePath,
		// Token: 0x040002A2 RID: 674
		ODataParameterWriterCore_WriteEndImplementation_UnreachableCodePath,
		// Token: 0x040002A3 RID: 675
		QueryPathValidator_ValidateSegment_Root,
		// Token: 0x040002A4 RID: 676
		QueryPathValidator_ValidateSegment_NonRoot,
		// Token: 0x040002A5 RID: 677
		ODataBatchWriter_ValidateTransition_UnreachableCodePath,
		// Token: 0x040002A6 RID: 678
		ODataBatchWriterUtils_HttpMethod_ToText_UnreachableCodePath,
		// Token: 0x040002A7 RID: 679
		ODataBatchReader_ReadImplementation,
		// Token: 0x040002A8 RID: 680
		ODataBatchReader_GetEndBoundary_Completed,
		// Token: 0x040002A9 RID: 681
		ODataBatchReader_GetEndBoundary_Exception,
		// Token: 0x040002AA RID: 682
		ODataBatchReader_GetEndBoundary_UnknownValue,
		// Token: 0x040002AB RID: 683
		ODataBatchReaderStream_SkipToBoundary,
		// Token: 0x040002AC RID: 684
		ODataBatchReaderStream_ReadLine,
		// Token: 0x040002AD RID: 685
		ODataBatchReaderStream_ReadWithDelimiter,
		// Token: 0x040002AE RID: 686
		ODataBatchReaderStreamBuffer_ScanForBoundary,
		// Token: 0x040002AF RID: 687
		ODataBatchReaderStreamBuffer_ReadWithLength,
		// Token: 0x040002B0 RID: 688
		JsonReader_Read,
		// Token: 0x040002B1 RID: 689
		ODataReader_CreateReader_UnreachableCodePath,
		// Token: 0x040002B2 RID: 690
		ODataReaderCore_ReadImplementation,
		// Token: 0x040002B3 RID: 691
		ODataReaderCoreAsync_ReadAsynchronously,
		// Token: 0x040002B4 RID: 692
		ODataVerboseJsonEntryAndFeedDeserializer_ReadFeedProperty,
		// Token: 0x040002B5 RID: 693
		ODataVerboseJsonReader_ReadEntryStart,
		// Token: 0x040002B6 RID: 694
		ODataVerboseJsonPropertyAndValueDeserializer_ReadPropertyValue,
		// Token: 0x040002B7 RID: 695
		ODataCollectionReader_CreateReader_UnreachableCodePath,
		// Token: 0x040002B8 RID: 696
		ODataCollectionReaderCore_ReadImplementation,
		// Token: 0x040002B9 RID: 697
		ODataCollectionReaderCoreAsync_ReadAsynchronously,
		// Token: 0x040002BA RID: 698
		ODataParameterReaderCore_ReadImplementation,
		// Token: 0x040002BB RID: 699
		ODataParameterReaderCoreAsync_ReadAsynchronously,
		// Token: 0x040002BC RID: 700
		ODataParameterReaderCore_ValueMustBePrimitiveOrComplexOrNull,
		// Token: 0x040002BD RID: 701
		ODataAtomReader_ReadAtNavigationLinkStartImplementation,
		// Token: 0x040002BE RID: 702
		ODataAtomPropertyAndValueDeserializer_ReadNonEntityValue,
		// Token: 0x040002BF RID: 703
		AtomValueUtils_ConvertStringToPrimitive,
		// Token: 0x040002C0 RID: 704
		EdmCoreModel_PrimitiveType,
		// Token: 0x040002C1 RID: 705
		ReaderValidationUtils_ResolveAndValidateTypeName_Strict_TypeKind,
		// Token: 0x040002C2 RID: 706
		ReaderValidationUtils_ResolveAndValidateTypeName_Lax_TypeKind,
		// Token: 0x040002C3 RID: 707
		ODataMetadataFormat_CreateOutputContextAsync,
		// Token: 0x040002C4 RID: 708
		ODataMetadataFormat_CreateInputContextAsync,
		// Token: 0x040002C5 RID: 709
		ODataModelFunctions_UnsupportedMethodOrProperty,
		// Token: 0x040002C6 RID: 710
		ODataJsonLightPropertyAndValueDeserializer_ReadPropertyValue,
		// Token: 0x040002C7 RID: 711
		ODataJsonLightPropertyAndValueDeserializer_GetNonEntityValueKind,
		// Token: 0x040002C8 RID: 712
		ODataJsonLightEntryAndFeedDeserializer_ReadFeedProperty,
		// Token: 0x040002C9 RID: 713
		ODataJsonLightReader_ReadEntryStart,
		// Token: 0x040002CA RID: 714
		ODataJsonLightEntryAndFeedDeserializer_ReadTopLevelFeedAnnotations,
		// Token: 0x040002CB RID: 715
		ODataJsonLightReader_ReadFeedEnd,
		// Token: 0x040002CC RID: 716
		ODataJsonLightCollectionDeserializer_ReadCollectionStart,
		// Token: 0x040002CD RID: 717
		ODataJsonLightCollectionDeserializer_ReadCollectionStart_TypeKindFromPayloadFunc,
		// Token: 0x040002CE RID: 718
		ODataJsonLightCollectionDeserializer_ReadCollectionEnd,
		// Token: 0x040002CF RID: 719
		ODataJsonLightEntityReferenceLinkDeserializer_ReadSingleEntityReferenceLink,
		// Token: 0x040002D0 RID: 720
		ODataJsonLightEntityReferenceLinkDeserializer_ReadEntityReferenceLinksAnnotations,
		// Token: 0x040002D1 RID: 721
		ODataJsonLightParameterDeserializer_ReadNextParameter,
		// Token: 0x040002D2 RID: 722
		EdmTypeWriterResolver_GetReturnTypeForOperationImportGroup,
		// Token: 0x040002D3 RID: 723
		ODataVersionCache_UnknownVersion
	}
}
