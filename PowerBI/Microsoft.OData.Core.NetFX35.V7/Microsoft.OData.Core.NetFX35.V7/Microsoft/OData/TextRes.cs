using System;
using System.Globalization;
using System.Resources;
using System.Threading;

namespace Microsoft.OData
{
	// Token: 0x020000DC RID: 220
	internal sealed class TextRes
	{
		// Token: 0x06000862 RID: 2146 RVA: 0x00018067 File Offset: 0x00016267
		internal TextRes()
		{
			this.resources = new ResourceManager("Microsoft.OData.Core", base.GetType().Assembly);
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x0001808C File Offset: 0x0001628C
		private static TextRes GetLoader()
		{
			if (TextRes.loader == null)
			{
				TextRes textRes = new TextRes();
				Interlocked.CompareExchange<TextRes>(ref TextRes.loader, textRes, null);
			}
			return TextRes.loader;
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000864 RID: 2148 RVA: 0x0000B41B File Offset: 0x0000961B
		private static CultureInfo Culture
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000865 RID: 2149 RVA: 0x000180B8 File Offset: 0x000162B8
		public static ResourceManager Resources
		{
			get
			{
				return TextRes.GetLoader().resources;
			}
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x000180C4 File Offset: 0x000162C4
		public static string GetString(string name, params object[] args)
		{
			TextRes textRes = TextRes.GetLoader();
			if (textRes == null)
			{
				return null;
			}
			string @string = textRes.resources.GetString(name, TextRes.Culture);
			if (args != null && args.Length != 0)
			{
				for (int i = 0; i < args.Length; i++)
				{
					string text = args[i] as string;
					if (text != null && text.Length > 1024)
					{
						args[i] = text.Substring(0, 1021) + "...";
					}
				}
				return string.Format(CultureInfo.CurrentCulture, @string, args);
			}
			return @string;
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x00018144 File Offset: 0x00016344
		public static string GetString(string name)
		{
			TextRes textRes = TextRes.GetLoader();
			if (textRes == null)
			{
				return null;
			}
			return textRes.resources.GetString(name, TextRes.Culture);
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x0001816D File Offset: 0x0001636D
		public static string GetString(string name, out bool usedFallback)
		{
			usedFallback = false;
			return TextRes.GetString(name);
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x00018178 File Offset: 0x00016378
		public static object GetObject(string name)
		{
			TextRes textRes = TextRes.GetLoader();
			if (textRes == null)
			{
				return null;
			}
			return textRes.resources.GetObject(name, TextRes.Culture);
		}

		// Token: 0x0400039C RID: 924
		internal const string ExceptionUtils_ArgumentStringEmpty = "ExceptionUtils_ArgumentStringEmpty";

		// Token: 0x0400039D RID: 925
		internal const string ODataRequestMessage_AsyncNotAvailable = "ODataRequestMessage_AsyncNotAvailable";

		// Token: 0x0400039E RID: 926
		internal const string ODataRequestMessage_StreamTaskIsNull = "ODataRequestMessage_StreamTaskIsNull";

		// Token: 0x0400039F RID: 927
		internal const string ODataRequestMessage_MessageStreamIsNull = "ODataRequestMessage_MessageStreamIsNull";

		// Token: 0x040003A0 RID: 928
		internal const string ODataResponseMessage_AsyncNotAvailable = "ODataResponseMessage_AsyncNotAvailable";

		// Token: 0x040003A1 RID: 929
		internal const string ODataResponseMessage_StreamTaskIsNull = "ODataResponseMessage_StreamTaskIsNull";

		// Token: 0x040003A2 RID: 930
		internal const string ODataResponseMessage_MessageStreamIsNull = "ODataResponseMessage_MessageStreamIsNull";

		// Token: 0x040003A3 RID: 931
		internal const string AsyncBufferedStream_WriterDisposedWithoutFlush = "AsyncBufferedStream_WriterDisposedWithoutFlush";

		// Token: 0x040003A4 RID: 932
		internal const string ODataFormat_AtomFormatObsoleted = "ODataFormat_AtomFormatObsoleted";

		// Token: 0x040003A5 RID: 933
		internal const string ODataOutputContext_UnsupportedPayloadKindForFormat = "ODataOutputContext_UnsupportedPayloadKindForFormat";

		// Token: 0x040003A6 RID: 934
		internal const string ODataInputContext_UnsupportedPayloadKindForFormat = "ODataInputContext_UnsupportedPayloadKindForFormat";

		// Token: 0x040003A7 RID: 935
		internal const string ODataOutputContext_MetadataDocumentUriMissing = "ODataOutputContext_MetadataDocumentUriMissing";

		// Token: 0x040003A8 RID: 936
		internal const string ODataJsonLightSerializer_RelativeUriUsedWithoutMetadataDocumentUriOrMetadata = "ODataJsonLightSerializer_RelativeUriUsedWithoutMetadataDocumentUriOrMetadata";

		// Token: 0x040003A9 RID: 937
		internal const string ODataWriter_RelativeUriUsedWithoutBaseUriSpecified = "ODataWriter_RelativeUriUsedWithoutBaseUriSpecified";

		// Token: 0x040003AA RID: 938
		internal const string ODataWriter_StreamPropertiesMustBePropertiesOfODataResource = "ODataWriter_StreamPropertiesMustBePropertiesOfODataResource";

		// Token: 0x040003AB RID: 939
		internal const string ODataWriterCore_InvalidStateTransition = "ODataWriterCore_InvalidStateTransition";

		// Token: 0x040003AC RID: 940
		internal const string ODataWriterCore_InvalidTransitionFromStart = "ODataWriterCore_InvalidTransitionFromStart";

		// Token: 0x040003AD RID: 941
		internal const string ODataWriterCore_InvalidTransitionFromResource = "ODataWriterCore_InvalidTransitionFromResource";

		// Token: 0x040003AE RID: 942
		internal const string ODataWriterCore_InvalidTransitionFromNullResource = "ODataWriterCore_InvalidTransitionFromNullResource";

		// Token: 0x040003AF RID: 943
		internal const string ODataWriterCore_InvalidTransitionFromResourceSet = "ODataWriterCore_InvalidTransitionFromResourceSet";

		// Token: 0x040003B0 RID: 944
		internal const string ODataWriterCore_InvalidTransitionFromExpandedLink = "ODataWriterCore_InvalidTransitionFromExpandedLink";

		// Token: 0x040003B1 RID: 945
		internal const string ODataWriterCore_InvalidTransitionFromCompleted = "ODataWriterCore_InvalidTransitionFromCompleted";

		// Token: 0x040003B2 RID: 946
		internal const string ODataWriterCore_InvalidTransitionFromError = "ODataWriterCore_InvalidTransitionFromError";

		// Token: 0x040003B3 RID: 947
		internal const string ODataJsonLightDeltaWriter_InvalidTransitionFromNestedResource = "ODataJsonLightDeltaWriter_InvalidTransitionFromNestedResource";

		// Token: 0x040003B4 RID: 948
		internal const string ODataJsonLightDeltaWriter_InvalidTransitionToNestedResource = "ODataJsonLightDeltaWriter_InvalidTransitionToNestedResource";

		// Token: 0x040003B5 RID: 949
		internal const string ODataJsonLightDeltaWriter_WriteStartExpandedResourceSetCalledInInvalidState = "ODataJsonLightDeltaWriter_WriteStartExpandedResourceSetCalledInInvalidState";

		// Token: 0x040003B6 RID: 950
		internal const string ODataWriterCore_WriteEndCalledInInvalidState = "ODataWriterCore_WriteEndCalledInInvalidState";

		// Token: 0x040003B7 RID: 951
		internal const string ODataWriterCore_QueryCountInRequest = "ODataWriterCore_QueryCountInRequest";

		// Token: 0x040003B8 RID: 952
		internal const string ODataWriterCore_CannotWriteTopLevelResourceSetWithResourceWriter = "ODataWriterCore_CannotWriteTopLevelResourceSetWithResourceWriter";

		// Token: 0x040003B9 RID: 953
		internal const string ODataWriterCore_CannotWriteTopLevelResourceWithResourceSetWriter = "ODataWriterCore_CannotWriteTopLevelResourceWithResourceSetWriter";

		// Token: 0x040003BA RID: 954
		internal const string ODataWriterCore_SyncCallOnAsyncWriter = "ODataWriterCore_SyncCallOnAsyncWriter";

		// Token: 0x040003BB RID: 955
		internal const string ODataWriterCore_AsyncCallOnSyncWriter = "ODataWriterCore_AsyncCallOnSyncWriter";

		// Token: 0x040003BC RID: 956
		internal const string ODataWriterCore_EntityReferenceLinkWithoutNavigationLink = "ODataWriterCore_EntityReferenceLinkWithoutNavigationLink";

		// Token: 0x040003BD RID: 957
		internal const string ODataWriterCore_EntityReferenceLinkInResponse = "ODataWriterCore_EntityReferenceLinkInResponse";

		// Token: 0x040003BE RID: 958
		internal const string ODataWriterCore_DeferredLinkInRequest = "ODataWriterCore_DeferredLinkInRequest";

		// Token: 0x040003BF RID: 959
		internal const string ODataWriterCore_MultipleItemsInNestedResourceInfoWithContent = "ODataWriterCore_MultipleItemsInNestedResourceInfoWithContent";

		// Token: 0x040003C0 RID: 960
		internal const string ODataWriterCore_DeltaLinkNotSupportedOnExpandedResourceSet = "ODataWriterCore_DeltaLinkNotSupportedOnExpandedResourceSet";

		// Token: 0x040003C1 RID: 961
		internal const string ODataWriterCore_PathInODataUriMustBeSetWhenWritingContainedElement = "ODataWriterCore_PathInODataUriMustBeSetWhenWritingContainedElement";

		// Token: 0x040003C2 RID: 962
		internal const string DuplicatePropertyNamesNotAllowed = "DuplicatePropertyNamesNotAllowed";

		// Token: 0x040003C3 RID: 963
		internal const string DuplicateAnnotationNotAllowed = "DuplicateAnnotationNotAllowed";

		// Token: 0x040003C4 RID: 964
		internal const string DuplicateAnnotationForPropertyNotAllowed = "DuplicateAnnotationForPropertyNotAllowed";

		// Token: 0x040003C5 RID: 965
		internal const string DuplicateAnnotationForInstanceAnnotationNotAllowed = "DuplicateAnnotationForInstanceAnnotationNotAllowed";

		// Token: 0x040003C6 RID: 966
		internal const string PropertyAnnotationAfterTheProperty = "PropertyAnnotationAfterTheProperty";

		// Token: 0x040003C7 RID: 967
		internal const string AtomValueUtils_CannotConvertValueToAtomPrimitive = "AtomValueUtils_CannotConvertValueToAtomPrimitive";

		// Token: 0x040003C8 RID: 968
		internal const string ODataJsonWriter_UnsupportedValueType = "ODataJsonWriter_UnsupportedValueType";

		// Token: 0x040003C9 RID: 969
		internal const string ODataException_GeneralError = "ODataException_GeneralError";

		// Token: 0x040003CA RID: 970
		internal const string ODataErrorException_GeneralError = "ODataErrorException_GeneralError";

		// Token: 0x040003CB RID: 971
		internal const string ODataUriParserException_GeneralError = "ODataUriParserException_GeneralError";

		// Token: 0x040003CC RID: 972
		internal const string ODataMessageWriter_WriterAlreadyUsed = "ODataMessageWriter_WriterAlreadyUsed";

		// Token: 0x040003CD RID: 973
		internal const string ODataMessageWriter_EntityReferenceLinksInRequestNotAllowed = "ODataMessageWriter_EntityReferenceLinksInRequestNotAllowed";

		// Token: 0x040003CE RID: 974
		internal const string ODataMessageWriter_ErrorPayloadInRequest = "ODataMessageWriter_ErrorPayloadInRequest";

		// Token: 0x040003CF RID: 975
		internal const string ODataMessageWriter_ServiceDocumentInRequest = "ODataMessageWriter_ServiceDocumentInRequest";

		// Token: 0x040003D0 RID: 976
		internal const string ODataMessageWriter_MetadataDocumentInRequest = "ODataMessageWriter_MetadataDocumentInRequest";

		// Token: 0x040003D1 RID: 977
		internal const string ODataMessageWriter_DeltaInRequest = "ODataMessageWriter_DeltaInRequest";

		// Token: 0x040003D2 RID: 978
		internal const string ODataMessageWriter_AsyncInRequest = "ODataMessageWriter_AsyncInRequest";

		// Token: 0x040003D3 RID: 979
		internal const string ODataMessageWriter_CannotWriteNullInRawFormat = "ODataMessageWriter_CannotWriteNullInRawFormat";

		// Token: 0x040003D4 RID: 980
		internal const string ODataMessageWriter_CannotSetHeadersWithInvalidPayloadKind = "ODataMessageWriter_CannotSetHeadersWithInvalidPayloadKind";

		// Token: 0x040003D5 RID: 981
		internal const string ODataMessageWriter_IncompatiblePayloadKinds = "ODataMessageWriter_IncompatiblePayloadKinds";

		// Token: 0x040003D6 RID: 982
		internal const string ODataMessageWriter_CannotWriteStreamPropertyAsTopLevelProperty = "ODataMessageWriter_CannotWriteStreamPropertyAsTopLevelProperty";

		// Token: 0x040003D7 RID: 983
		internal const string ODataMessageWriter_WriteErrorAlreadyCalled = "ODataMessageWriter_WriteErrorAlreadyCalled";

		// Token: 0x040003D8 RID: 984
		internal const string ODataMessageWriter_CannotWriteInStreamErrorForRawValues = "ODataMessageWriter_CannotWriteInStreamErrorForRawValues";

		// Token: 0x040003D9 RID: 985
		internal const string ODataMessageWriter_CannotWriteMetadataWithoutModel = "ODataMessageWriter_CannotWriteMetadataWithoutModel";

		// Token: 0x040003DA RID: 986
		internal const string ODataMessageWriter_CannotSpecifyOperationWithoutModel = "ODataMessageWriter_CannotSpecifyOperationWithoutModel";

		// Token: 0x040003DB RID: 987
		internal const string ODataMessageWriter_JsonPaddingOnInvalidContentType = "ODataMessageWriter_JsonPaddingOnInvalidContentType";

		// Token: 0x040003DC RID: 988
		internal const string ODataMessageWriter_NonCollectionType = "ODataMessageWriter_NonCollectionType";

		// Token: 0x040003DD RID: 989
		internal const string ODataMessageWriterSettings_MessageWriterSettingsXmlCustomizationCallbacksMustBeSpecifiedBoth = "ODataMessageWriterSettings_MessageWriterSettingsXmlCustomizationCallbacksMustBeSpecifiedBoth";

		// Token: 0x040003DE RID: 990
		internal const string ODataCollectionWriterCore_InvalidTransitionFromStart = "ODataCollectionWriterCore_InvalidTransitionFromStart";

		// Token: 0x040003DF RID: 991
		internal const string ODataCollectionWriterCore_InvalidTransitionFromCollection = "ODataCollectionWriterCore_InvalidTransitionFromCollection";

		// Token: 0x040003E0 RID: 992
		internal const string ODataCollectionWriterCore_InvalidTransitionFromItem = "ODataCollectionWriterCore_InvalidTransitionFromItem";

		// Token: 0x040003E1 RID: 993
		internal const string ODataCollectionWriterCore_WriteEndCalledInInvalidState = "ODataCollectionWriterCore_WriteEndCalledInInvalidState";

		// Token: 0x040003E2 RID: 994
		internal const string ODataCollectionWriterCore_SyncCallOnAsyncWriter = "ODataCollectionWriterCore_SyncCallOnAsyncWriter";

		// Token: 0x040003E3 RID: 995
		internal const string ODataCollectionWriterCore_AsyncCallOnSyncWriter = "ODataCollectionWriterCore_AsyncCallOnSyncWriter";

		// Token: 0x040003E4 RID: 996
		internal const string ODataBatch_InvalidHttpMethodForChangeSetRequest = "ODataBatch_InvalidHttpMethodForChangeSetRequest";

		// Token: 0x040003E5 RID: 997
		internal const string ODataBatchOperationHeaderDictionary_KeyNotFound = "ODataBatchOperationHeaderDictionary_KeyNotFound";

		// Token: 0x040003E6 RID: 998
		internal const string ODataBatchOperationHeaderDictionary_DuplicateCaseInsensitiveKeys = "ODataBatchOperationHeaderDictionary_DuplicateCaseInsensitiveKeys";

		// Token: 0x040003E7 RID: 999
		internal const string ODataParameterWriter_InStreamErrorNotSupported = "ODataParameterWriter_InStreamErrorNotSupported";

		// Token: 0x040003E8 RID: 1000
		internal const string ODataParameterWriter_CannotCreateParameterWriterOnResponseMessage = "ODataParameterWriter_CannotCreateParameterWriterOnResponseMessage";

		// Token: 0x040003E9 RID: 1001
		internal const string ODataParameterWriterCore_SyncCallOnAsyncWriter = "ODataParameterWriterCore_SyncCallOnAsyncWriter";

		// Token: 0x040003EA RID: 1002
		internal const string ODataParameterWriterCore_AsyncCallOnSyncWriter = "ODataParameterWriterCore_AsyncCallOnSyncWriter";

		// Token: 0x040003EB RID: 1003
		internal const string ODataParameterWriterCore_CannotWriteStart = "ODataParameterWriterCore_CannotWriteStart";

		// Token: 0x040003EC RID: 1004
		internal const string ODataParameterWriterCore_CannotWriteParameter = "ODataParameterWriterCore_CannotWriteParameter";

		// Token: 0x040003ED RID: 1005
		internal const string ODataParameterWriterCore_CannotWriteEnd = "ODataParameterWriterCore_CannotWriteEnd";

		// Token: 0x040003EE RID: 1006
		internal const string ODataParameterWriterCore_CannotWriteInErrorOrCompletedState = "ODataParameterWriterCore_CannotWriteInErrorOrCompletedState";

		// Token: 0x040003EF RID: 1007
		internal const string ODataParameterWriterCore_DuplicatedParameterNameNotAllowed = "ODataParameterWriterCore_DuplicatedParameterNameNotAllowed";

		// Token: 0x040003F0 RID: 1008
		internal const string ODataParameterWriterCore_CannotWriteValueOnNonValueTypeKind = "ODataParameterWriterCore_CannotWriteValueOnNonValueTypeKind";

		// Token: 0x040003F1 RID: 1009
		internal const string ODataParameterWriterCore_CannotWriteValueOnNonSupportedValueType = "ODataParameterWriterCore_CannotWriteValueOnNonSupportedValueType";

		// Token: 0x040003F2 RID: 1010
		internal const string ODataParameterWriterCore_CannotCreateCollectionWriterOnNonCollectionTypeKind = "ODataParameterWriterCore_CannotCreateCollectionWriterOnNonCollectionTypeKind";

		// Token: 0x040003F3 RID: 1011
		internal const string ODataParameterWriterCore_CannotCreateResourceWriterOnNonEntityOrComplexTypeKind = "ODataParameterWriterCore_CannotCreateResourceWriterOnNonEntityOrComplexTypeKind";

		// Token: 0x040003F4 RID: 1012
		internal const string ODataParameterWriterCore_CannotCreateResourceSetWriterOnNonStructuredCollectionTypeKind = "ODataParameterWriterCore_CannotCreateResourceSetWriterOnNonStructuredCollectionTypeKind";

		// Token: 0x040003F5 RID: 1013
		internal const string ODataParameterWriterCore_ParameterNameNotFoundInOperation = "ODataParameterWriterCore_ParameterNameNotFoundInOperation";

		// Token: 0x040003F6 RID: 1014
		internal const string ODataParameterWriterCore_MissingParameterInParameterPayload = "ODataParameterWriterCore_MissingParameterInParameterPayload";

		// Token: 0x040003F7 RID: 1015
		internal const string ODataBatchWriter_FlushOrFlushAsyncCalledInStreamRequestedState = "ODataBatchWriter_FlushOrFlushAsyncCalledInStreamRequestedState";

		// Token: 0x040003F8 RID: 1016
		internal const string ODataBatchWriter_CannotCompleteBatchWithActiveChangeSet = "ODataBatchWriter_CannotCompleteBatchWithActiveChangeSet";

		// Token: 0x040003F9 RID: 1017
		internal const string ODataBatchWriter_CannotStartChangeSetWithActiveChangeSet = "ODataBatchWriter_CannotStartChangeSetWithActiveChangeSet";

		// Token: 0x040003FA RID: 1018
		internal const string ODataBatchWriter_CannotCompleteChangeSetWithoutActiveChangeSet = "ODataBatchWriter_CannotCompleteChangeSetWithoutActiveChangeSet";

		// Token: 0x040003FB RID: 1019
		internal const string ODataBatchWriter_InvalidTransitionFromStart = "ODataBatchWriter_InvalidTransitionFromStart";

		// Token: 0x040003FC RID: 1020
		internal const string ODataBatchWriter_InvalidTransitionFromBatchStarted = "ODataBatchWriter_InvalidTransitionFromBatchStarted";

		// Token: 0x040003FD RID: 1021
		internal const string ODataBatchWriter_InvalidTransitionFromChangeSetStarted = "ODataBatchWriter_InvalidTransitionFromChangeSetStarted";

		// Token: 0x040003FE RID: 1022
		internal const string ODataBatchWriter_InvalidTransitionFromOperationCreated = "ODataBatchWriter_InvalidTransitionFromOperationCreated";

		// Token: 0x040003FF RID: 1023
		internal const string ODataBatchWriter_InvalidTransitionFromOperationContentStreamRequested = "ODataBatchWriter_InvalidTransitionFromOperationContentStreamRequested";

		// Token: 0x04000400 RID: 1024
		internal const string ODataBatchWriter_InvalidTransitionFromOperationContentStreamDisposed = "ODataBatchWriter_InvalidTransitionFromOperationContentStreamDisposed";

		// Token: 0x04000401 RID: 1025
		internal const string ODataBatchWriter_InvalidTransitionFromChangeSetCompleted = "ODataBatchWriter_InvalidTransitionFromChangeSetCompleted";

		// Token: 0x04000402 RID: 1026
		internal const string ODataBatchWriter_InvalidTransitionFromBatchCompleted = "ODataBatchWriter_InvalidTransitionFromBatchCompleted";

		// Token: 0x04000403 RID: 1027
		internal const string ODataBatchWriter_CannotCreateRequestOperationWhenWritingResponse = "ODataBatchWriter_CannotCreateRequestOperationWhenWritingResponse";

		// Token: 0x04000404 RID: 1028
		internal const string ODataBatchWriter_CannotCreateResponseOperationWhenWritingRequest = "ODataBatchWriter_CannotCreateResponseOperationWhenWritingRequest";

		// Token: 0x04000405 RID: 1029
		internal const string ODataBatchWriter_MaxBatchSizeExceeded = "ODataBatchWriter_MaxBatchSizeExceeded";

		// Token: 0x04000406 RID: 1030
		internal const string ODataBatchWriter_MaxChangeSetSizeExceeded = "ODataBatchWriter_MaxChangeSetSizeExceeded";

		// Token: 0x04000407 RID: 1031
		internal const string ODataBatchWriter_SyncCallOnAsyncWriter = "ODataBatchWriter_SyncCallOnAsyncWriter";

		// Token: 0x04000408 RID: 1032
		internal const string ODataBatchWriter_AsyncCallOnSyncWriter = "ODataBatchWriter_AsyncCallOnSyncWriter";

		// Token: 0x04000409 RID: 1033
		internal const string ODataBatchWriter_DuplicateContentIDsNotAllowed = "ODataBatchWriter_DuplicateContentIDsNotAllowed";

		// Token: 0x0400040A RID: 1034
		internal const string ODataBatchWriter_CannotWriteInStreamErrorForBatch = "ODataBatchWriter_CannotWriteInStreamErrorForBatch";

		// Token: 0x0400040B RID: 1035
		internal const string ODataBatchUtils_RelativeUriUsedWithoutBaseUriSpecified = "ODataBatchUtils_RelativeUriUsedWithoutBaseUriSpecified";

		// Token: 0x0400040C RID: 1036
		internal const string ODataBatchUtils_RelativeUriStartingWithDollarUsedWithoutBaseUriSpecified = "ODataBatchUtils_RelativeUriStartingWithDollarUsedWithoutBaseUriSpecified";

		// Token: 0x0400040D RID: 1037
		internal const string ODataBatchOperationMessage_VerifyNotCompleted = "ODataBatchOperationMessage_VerifyNotCompleted";

		// Token: 0x0400040E RID: 1038
		internal const string ODataBatchOperationStream_Disposed = "ODataBatchOperationStream_Disposed";

		// Token: 0x0400040F RID: 1039
		internal const string ODataBatchReader_CannotCreateRequestOperationWhenReadingResponse = "ODataBatchReader_CannotCreateRequestOperationWhenReadingResponse";

		// Token: 0x04000410 RID: 1040
		internal const string ODataBatchReader_CannotCreateResponseOperationWhenReadingRequest = "ODataBatchReader_CannotCreateResponseOperationWhenReadingRequest";

		// Token: 0x04000411 RID: 1041
		internal const string ODataBatchReader_InvalidStateForCreateOperationRequestMessage = "ODataBatchReader_InvalidStateForCreateOperationRequestMessage";

		// Token: 0x04000412 RID: 1042
		internal const string ODataBatchReader_OperationRequestMessageAlreadyCreated = "ODataBatchReader_OperationRequestMessageAlreadyCreated";

		// Token: 0x04000413 RID: 1043
		internal const string ODataBatchReader_OperationResponseMessageAlreadyCreated = "ODataBatchReader_OperationResponseMessageAlreadyCreated";

		// Token: 0x04000414 RID: 1044
		internal const string ODataBatchReader_InvalidStateForCreateOperationResponseMessage = "ODataBatchReader_InvalidStateForCreateOperationResponseMessage";

		// Token: 0x04000415 RID: 1045
		internal const string ODataBatchReader_CannotUseReaderWhileOperationStreamActive = "ODataBatchReader_CannotUseReaderWhileOperationStreamActive";

		// Token: 0x04000416 RID: 1046
		internal const string ODataBatchReader_SyncCallOnAsyncReader = "ODataBatchReader_SyncCallOnAsyncReader";

		// Token: 0x04000417 RID: 1047
		internal const string ODataBatchReader_AsyncCallOnSyncReader = "ODataBatchReader_AsyncCallOnSyncReader";

		// Token: 0x04000418 RID: 1048
		internal const string ODataBatchReader_ReadOrReadAsyncCalledInInvalidState = "ODataBatchReader_ReadOrReadAsyncCalledInInvalidState";

		// Token: 0x04000419 RID: 1049
		internal const string ODataBatchReader_MaxBatchSizeExceeded = "ODataBatchReader_MaxBatchSizeExceeded";

		// Token: 0x0400041A RID: 1050
		internal const string ODataBatchReader_MaxChangeSetSizeExceeded = "ODataBatchReader_MaxChangeSetSizeExceeded";

		// Token: 0x0400041B RID: 1051
		internal const string ODataBatchReader_NoMessageWasCreatedForOperation = "ODataBatchReader_NoMessageWasCreatedForOperation";

		// Token: 0x0400041C RID: 1052
		internal const string ODataBatchReader_DuplicateContentIDsNotAllowed = "ODataBatchReader_DuplicateContentIDsNotAllowed";

		// Token: 0x0400041D RID: 1053
		internal const string ODataBatchReaderStream_InvalidHeaderSpecified = "ODataBatchReaderStream_InvalidHeaderSpecified";

		// Token: 0x0400041E RID: 1054
		internal const string ODataBatchReaderStream_InvalidRequestLine = "ODataBatchReaderStream_InvalidRequestLine";

		// Token: 0x0400041F RID: 1055
		internal const string ODataBatchReaderStream_InvalidResponseLine = "ODataBatchReaderStream_InvalidResponseLine";

		// Token: 0x04000420 RID: 1056
		internal const string ODataBatchReaderStream_InvalidHttpVersionSpecified = "ODataBatchReaderStream_InvalidHttpVersionSpecified";

		// Token: 0x04000421 RID: 1057
		internal const string ODataBatchReaderStream_NonIntegerHttpStatusCode = "ODataBatchReaderStream_NonIntegerHttpStatusCode";

		// Token: 0x04000422 RID: 1058
		internal const string ODataBatchReaderStream_MissingContentTypeHeader = "ODataBatchReaderStream_MissingContentTypeHeader";

		// Token: 0x04000423 RID: 1059
		internal const string ODataBatchReaderStream_MissingOrInvalidContentEncodingHeader = "ODataBatchReaderStream_MissingOrInvalidContentEncodingHeader";

		// Token: 0x04000424 RID: 1060
		internal const string ODataBatchReaderStream_InvalidContentTypeSpecified = "ODataBatchReaderStream_InvalidContentTypeSpecified";

		// Token: 0x04000425 RID: 1061
		internal const string ODataBatchReaderStream_InvalidContentLengthSpecified = "ODataBatchReaderStream_InvalidContentLengthSpecified";

		// Token: 0x04000426 RID: 1062
		internal const string ODataBatchReaderStream_DuplicateHeaderFound = "ODataBatchReaderStream_DuplicateHeaderFound";

		// Token: 0x04000427 RID: 1063
		internal const string ODataBatchReaderStream_NestedChangesetsAreNotSupported = "ODataBatchReaderStream_NestedChangesetsAreNotSupported";

		// Token: 0x04000428 RID: 1064
		internal const string ODataBatchReaderStream_MultiByteEncodingsNotSupported = "ODataBatchReaderStream_MultiByteEncodingsNotSupported";

		// Token: 0x04000429 RID: 1065
		internal const string ODataBatchReaderStream_UnexpectedEndOfInput = "ODataBatchReaderStream_UnexpectedEndOfInput";

		// Token: 0x0400042A RID: 1066
		internal const string ODataBatchReaderStreamBuffer_BoundaryLineSecurityLimitReached = "ODataBatchReaderStreamBuffer_BoundaryLineSecurityLimitReached";

		// Token: 0x0400042B RID: 1067
		internal const string ODataAsyncWriter_CannotCreateResponseWhenNotWritingResponse = "ODataAsyncWriter_CannotCreateResponseWhenNotWritingResponse";

		// Token: 0x0400042C RID: 1068
		internal const string ODataAsyncWriter_CannotCreateResponseMoreThanOnce = "ODataAsyncWriter_CannotCreateResponseMoreThanOnce";

		// Token: 0x0400042D RID: 1069
		internal const string ODataAsyncWriter_SyncCallOnAsyncWriter = "ODataAsyncWriter_SyncCallOnAsyncWriter";

		// Token: 0x0400042E RID: 1070
		internal const string ODataAsyncWriter_AsyncCallOnSyncWriter = "ODataAsyncWriter_AsyncCallOnSyncWriter";

		// Token: 0x0400042F RID: 1071
		internal const string ODataAsyncWriter_CannotWriteInStreamErrorForAsync = "ODataAsyncWriter_CannotWriteInStreamErrorForAsync";

		// Token: 0x04000430 RID: 1072
		internal const string ODataAsyncReader_InvalidHeaderSpecified = "ODataAsyncReader_InvalidHeaderSpecified";

		// Token: 0x04000431 RID: 1073
		internal const string ODataAsyncReader_CannotCreateResponseWhenNotReadingResponse = "ODataAsyncReader_CannotCreateResponseWhenNotReadingResponse";

		// Token: 0x04000432 RID: 1074
		internal const string ODataAsyncReader_InvalidResponseLine = "ODataAsyncReader_InvalidResponseLine";

		// Token: 0x04000433 RID: 1075
		internal const string ODataAsyncReader_InvalidHttpVersionSpecified = "ODataAsyncReader_InvalidHttpVersionSpecified";

		// Token: 0x04000434 RID: 1076
		internal const string ODataAsyncReader_NonIntegerHttpStatusCode = "ODataAsyncReader_NonIntegerHttpStatusCode";

		// Token: 0x04000435 RID: 1077
		internal const string ODataAsyncReader_DuplicateHeaderFound = "ODataAsyncReader_DuplicateHeaderFound";

		// Token: 0x04000436 RID: 1078
		internal const string ODataAsyncReader_MultiByteEncodingsNotSupported = "ODataAsyncReader_MultiByteEncodingsNotSupported";

		// Token: 0x04000437 RID: 1079
		internal const string ODataAsyncReader_InvalidNewLineEncountered = "ODataAsyncReader_InvalidNewLineEncountered";

		// Token: 0x04000438 RID: 1080
		internal const string ODataAsyncReader_UnexpectedEndOfInput = "ODataAsyncReader_UnexpectedEndOfInput";

		// Token: 0x04000439 RID: 1081
		internal const string ODataAsyncReader_SyncCallOnAsyncReader = "ODataAsyncReader_SyncCallOnAsyncReader";

		// Token: 0x0400043A RID: 1082
		internal const string ODataAsyncReader_AsyncCallOnSyncReader = "ODataAsyncReader_AsyncCallOnSyncReader";

		// Token: 0x0400043B RID: 1083
		internal const string HttpUtils_MediaTypeUnspecified = "HttpUtils_MediaTypeUnspecified";

		// Token: 0x0400043C RID: 1084
		internal const string HttpUtils_MediaTypeRequiresSlash = "HttpUtils_MediaTypeRequiresSlash";

		// Token: 0x0400043D RID: 1085
		internal const string HttpUtils_MediaTypeRequiresSubType = "HttpUtils_MediaTypeRequiresSubType";

		// Token: 0x0400043E RID: 1086
		internal const string HttpUtils_MediaTypeMissingParameterValue = "HttpUtils_MediaTypeMissingParameterValue";

		// Token: 0x0400043F RID: 1087
		internal const string HttpUtils_MediaTypeMissingParameterName = "HttpUtils_MediaTypeMissingParameterName";

		// Token: 0x04000440 RID: 1088
		internal const string HttpUtils_EscapeCharWithoutQuotes = "HttpUtils_EscapeCharWithoutQuotes";

		// Token: 0x04000441 RID: 1089
		internal const string HttpUtils_EscapeCharAtEnd = "HttpUtils_EscapeCharAtEnd";

		// Token: 0x04000442 RID: 1090
		internal const string HttpUtils_ClosingQuoteNotFound = "HttpUtils_ClosingQuoteNotFound";

		// Token: 0x04000443 RID: 1091
		internal const string HttpUtils_InvalidCharacterInQuotedParameterValue = "HttpUtils_InvalidCharacterInQuotedParameterValue";

		// Token: 0x04000444 RID: 1092
		internal const string HttpUtils_ContentTypeMissing = "HttpUtils_ContentTypeMissing";

		// Token: 0x04000445 RID: 1093
		internal const string HttpUtils_MediaTypeRequiresSemicolonBeforeParameter = "HttpUtils_MediaTypeRequiresSemicolonBeforeParameter";

		// Token: 0x04000446 RID: 1094
		internal const string HttpUtils_InvalidQualityValueStartChar = "HttpUtils_InvalidQualityValueStartChar";

		// Token: 0x04000447 RID: 1095
		internal const string HttpUtils_InvalidQualityValue = "HttpUtils_InvalidQualityValue";

		// Token: 0x04000448 RID: 1096
		internal const string HttpUtils_CannotConvertCharToInt = "HttpUtils_CannotConvertCharToInt";

		// Token: 0x04000449 RID: 1097
		internal const string HttpUtils_MissingSeparatorBetweenCharsets = "HttpUtils_MissingSeparatorBetweenCharsets";

		// Token: 0x0400044A RID: 1098
		internal const string HttpUtils_InvalidSeparatorBetweenCharsets = "HttpUtils_InvalidSeparatorBetweenCharsets";

		// Token: 0x0400044B RID: 1099
		internal const string HttpUtils_InvalidCharsetName = "HttpUtils_InvalidCharsetName";

		// Token: 0x0400044C RID: 1100
		internal const string HttpUtils_UnexpectedEndOfQValue = "HttpUtils_UnexpectedEndOfQValue";

		// Token: 0x0400044D RID: 1101
		internal const string HttpUtils_ExpectedLiteralNotFoundInString = "HttpUtils_ExpectedLiteralNotFoundInString";

		// Token: 0x0400044E RID: 1102
		internal const string HttpUtils_InvalidHttpMethodString = "HttpUtils_InvalidHttpMethodString";

		// Token: 0x0400044F RID: 1103
		internal const string HttpUtils_NoOrMoreThanOneContentTypeSpecified = "HttpUtils_NoOrMoreThanOneContentTypeSpecified";

		// Token: 0x04000450 RID: 1104
		internal const string HttpHeaderValueLexer_UnrecognizedSeparator = "HttpHeaderValueLexer_UnrecognizedSeparator";

		// Token: 0x04000451 RID: 1105
		internal const string HttpHeaderValueLexer_TokenExpectedButFoundQuotedString = "HttpHeaderValueLexer_TokenExpectedButFoundQuotedString";

		// Token: 0x04000452 RID: 1106
		internal const string HttpHeaderValueLexer_FailedToReadTokenOrQuotedString = "HttpHeaderValueLexer_FailedToReadTokenOrQuotedString";

		// Token: 0x04000453 RID: 1107
		internal const string HttpHeaderValueLexer_InvalidSeparatorAfterQuotedString = "HttpHeaderValueLexer_InvalidSeparatorAfterQuotedString";

		// Token: 0x04000454 RID: 1108
		internal const string HttpHeaderValueLexer_EndOfFileAfterSeparator = "HttpHeaderValueLexer_EndOfFileAfterSeparator";

		// Token: 0x04000455 RID: 1109
		internal const string MediaType_EncodingNotSupported = "MediaType_EncodingNotSupported";

		// Token: 0x04000456 RID: 1110
		internal const string MediaTypeUtils_DidNotFindMatchingMediaType = "MediaTypeUtils_DidNotFindMatchingMediaType";

		// Token: 0x04000457 RID: 1111
		internal const string MediaTypeUtils_CannotDetermineFormatFromContentType = "MediaTypeUtils_CannotDetermineFormatFromContentType";

		// Token: 0x04000458 RID: 1112
		internal const string MediaTypeUtils_NoOrMoreThanOneContentTypeSpecified = "MediaTypeUtils_NoOrMoreThanOneContentTypeSpecified";

		// Token: 0x04000459 RID: 1113
		internal const string MediaTypeUtils_BoundaryMustBeSpecifiedForBatchPayloads = "MediaTypeUtils_BoundaryMustBeSpecifiedForBatchPayloads";

		// Token: 0x0400045A RID: 1114
		internal const string ExpressionLexer_ExpectedLiteralToken = "ExpressionLexer_ExpectedLiteralToken";

		// Token: 0x0400045B RID: 1115
		internal const string ODataUriUtils_ConvertToUriLiteralUnsupportedType = "ODataUriUtils_ConvertToUriLiteralUnsupportedType";

		// Token: 0x0400045C RID: 1116
		internal const string ODataUriUtils_ConvertFromUriLiteralTypeRefWithoutModel = "ODataUriUtils_ConvertFromUriLiteralTypeRefWithoutModel";

		// Token: 0x0400045D RID: 1117
		internal const string ODataUriUtils_ConvertFromUriLiteralTypeVerificationFailure = "ODataUriUtils_ConvertFromUriLiteralTypeVerificationFailure";

		// Token: 0x0400045E RID: 1118
		internal const string ODataUriUtils_ConvertFromUriLiteralNullTypeVerificationFailure = "ODataUriUtils_ConvertFromUriLiteralNullTypeVerificationFailure";

		// Token: 0x0400045F RID: 1119
		internal const string ODataUriUtils_ConvertFromUriLiteralNullOnNonNullableType = "ODataUriUtils_ConvertFromUriLiteralNullOnNonNullableType";

		// Token: 0x04000460 RID: 1120
		internal const string ODataUtils_CannotConvertValueToRawString = "ODataUtils_CannotConvertValueToRawString";

		// Token: 0x04000461 RID: 1121
		internal const string ODataUtils_DidNotFindDefaultMediaType = "ODataUtils_DidNotFindDefaultMediaType";

		// Token: 0x04000462 RID: 1122
		internal const string ODataUtils_UnsupportedVersionHeader = "ODataUtils_UnsupportedVersionHeader";

		// Token: 0x04000463 RID: 1123
		internal const string ODataUtils_UnsupportedVersionNumber = "ODataUtils_UnsupportedVersionNumber";

		// Token: 0x04000464 RID: 1124
		internal const string ODataUtils_ModelDoesNotHaveContainer = "ODataUtils_ModelDoesNotHaveContainer";

		// Token: 0x04000465 RID: 1125
		internal const string ReaderUtils_EnumerableModified = "ReaderUtils_EnumerableModified";

		// Token: 0x04000466 RID: 1126
		internal const string ReaderValidationUtils_NullValueForNonNullableType = "ReaderValidationUtils_NullValueForNonNullableType";

		// Token: 0x04000467 RID: 1127
		internal const string ReaderValidationUtils_NullNamedValueForNonNullableType = "ReaderValidationUtils_NullNamedValueForNonNullableType";

		// Token: 0x04000468 RID: 1128
		internal const string ReaderValidationUtils_EntityReferenceLinkMissingUri = "ReaderValidationUtils_EntityReferenceLinkMissingUri";

		// Token: 0x04000469 RID: 1129
		internal const string ReaderValidationUtils_ValueWithoutType = "ReaderValidationUtils_ValueWithoutType";

		// Token: 0x0400046A RID: 1130
		internal const string ReaderValidationUtils_ResourceWithoutType = "ReaderValidationUtils_ResourceWithoutType";

		// Token: 0x0400046B RID: 1131
		internal const string ReaderValidationUtils_CannotConvertPrimitiveValue = "ReaderValidationUtils_CannotConvertPrimitiveValue";

		// Token: 0x0400046C RID: 1132
		internal const string ReaderValidationUtils_MessageReaderSettingsBaseUriMustBeNullOrAbsolute = "ReaderValidationUtils_MessageReaderSettingsBaseUriMustBeNullOrAbsolute";

		// Token: 0x0400046D RID: 1133
		internal const string ReaderValidationUtils_UndeclaredPropertyBehaviorKindSpecifiedOnRequest = "ReaderValidationUtils_UndeclaredPropertyBehaviorKindSpecifiedOnRequest";

		// Token: 0x0400046E RID: 1134
		internal const string ReaderValidationUtils_ContextUriValidationInvalidExpectedEntitySet = "ReaderValidationUtils_ContextUriValidationInvalidExpectedEntitySet";

		// Token: 0x0400046F RID: 1135
		internal const string ReaderValidationUtils_ContextUriValidationInvalidExpectedEntityType = "ReaderValidationUtils_ContextUriValidationInvalidExpectedEntityType";

		// Token: 0x04000470 RID: 1136
		internal const string ReaderValidationUtils_ContextUriValidationNonMatchingPropertyNames = "ReaderValidationUtils_ContextUriValidationNonMatchingPropertyNames";

		// Token: 0x04000471 RID: 1137
		internal const string ReaderValidationUtils_ContextUriValidationNonMatchingDeclaringTypes = "ReaderValidationUtils_ContextUriValidationNonMatchingDeclaringTypes";

		// Token: 0x04000472 RID: 1138
		internal const string ReaderValidationUtils_NonMatchingPropertyNames = "ReaderValidationUtils_NonMatchingPropertyNames";

		// Token: 0x04000473 RID: 1139
		internal const string ReaderValidationUtils_TypeInContextUriDoesNotMatchExpectedType = "ReaderValidationUtils_TypeInContextUriDoesNotMatchExpectedType";

		// Token: 0x04000474 RID: 1140
		internal const string ReaderValidationUtils_ContextUriDoesNotReferTypeAssignableToExpectedType = "ReaderValidationUtils_ContextUriDoesNotReferTypeAssignableToExpectedType";

		// Token: 0x04000475 RID: 1141
		internal const string ODataMessageReader_ReaderAlreadyUsed = "ODataMessageReader_ReaderAlreadyUsed";

		// Token: 0x04000476 RID: 1142
		internal const string ODataMessageReader_ErrorPayloadInRequest = "ODataMessageReader_ErrorPayloadInRequest";

		// Token: 0x04000477 RID: 1143
		internal const string ODataMessageReader_ServiceDocumentInRequest = "ODataMessageReader_ServiceDocumentInRequest";

		// Token: 0x04000478 RID: 1144
		internal const string ODataMessageReader_MetadataDocumentInRequest = "ODataMessageReader_MetadataDocumentInRequest";

		// Token: 0x04000479 RID: 1145
		internal const string ODataMessageReader_DeltaInRequest = "ODataMessageReader_DeltaInRequest";

		// Token: 0x0400047A RID: 1146
		internal const string ODataMessageReader_ExpectedTypeSpecifiedWithoutMetadata = "ODataMessageReader_ExpectedTypeSpecifiedWithoutMetadata";

		// Token: 0x0400047B RID: 1147
		internal const string ODataMessageReader_EntitySetSpecifiedWithoutMetadata = "ODataMessageReader_EntitySetSpecifiedWithoutMetadata";

		// Token: 0x0400047C RID: 1148
		internal const string ODataMessageReader_OperationImportSpecifiedWithoutMetadata = "ODataMessageReader_OperationImportSpecifiedWithoutMetadata";

		// Token: 0x0400047D RID: 1149
		internal const string ODataMessageReader_OperationSpecifiedWithoutMetadata = "ODataMessageReader_OperationSpecifiedWithoutMetadata";

		// Token: 0x0400047E RID: 1150
		internal const string ODataMessageReader_ExpectedCollectionTypeWrongKind = "ODataMessageReader_ExpectedCollectionTypeWrongKind";

		// Token: 0x0400047F RID: 1151
		internal const string ODataMessageReader_ExpectedPropertyTypeEntityCollectionKind = "ODataMessageReader_ExpectedPropertyTypeEntityCollectionKind";

		// Token: 0x04000480 RID: 1152
		internal const string ODataMessageReader_ExpectedPropertyTypeEntityKind = "ODataMessageReader_ExpectedPropertyTypeEntityKind";

		// Token: 0x04000481 RID: 1153
		internal const string ODataMessageReader_ExpectedPropertyTypeStream = "ODataMessageReader_ExpectedPropertyTypeStream";

		// Token: 0x04000482 RID: 1154
		internal const string ODataMessageReader_ExpectedValueTypeWrongKind = "ODataMessageReader_ExpectedValueTypeWrongKind";

		// Token: 0x04000483 RID: 1155
		internal const string ODataMessageReader_NoneOrEmptyContentTypeHeader = "ODataMessageReader_NoneOrEmptyContentTypeHeader";

		// Token: 0x04000484 RID: 1156
		internal const string ODataMessageReader_WildcardInContentType = "ODataMessageReader_WildcardInContentType";

		// Token: 0x04000485 RID: 1157
		internal const string ODataMessageReader_GetFormatCalledBeforeReadingStarted = "ODataMessageReader_GetFormatCalledBeforeReadingStarted";

		// Token: 0x04000486 RID: 1158
		internal const string ODataMessageReader_DetectPayloadKindMultipleTimes = "ODataMessageReader_DetectPayloadKindMultipleTimes";

		// Token: 0x04000487 RID: 1159
		internal const string ODataMessageReader_PayloadKindDetectionRunning = "ODataMessageReader_PayloadKindDetectionRunning";

		// Token: 0x04000488 RID: 1160
		internal const string ODataMessageReader_PayloadKindDetectionInServerMode = "ODataMessageReader_PayloadKindDetectionInServerMode";

		// Token: 0x04000489 RID: 1161
		internal const string ODataMessageReader_ParameterPayloadInResponse = "ODataMessageReader_ParameterPayloadInResponse";

		// Token: 0x0400048A RID: 1162
		internal const string ODataMessageReader_SingletonNavigationPropertyForEntityReferenceLinks = "ODataMessageReader_SingletonNavigationPropertyForEntityReferenceLinks";

		// Token: 0x0400048B RID: 1163
		internal const string ODataAsyncResponseMessage_MustNotModifyMessage = "ODataAsyncResponseMessage_MustNotModifyMessage";

		// Token: 0x0400048C RID: 1164
		internal const string ODataMessage_MustNotModifyMessage = "ODataMessage_MustNotModifyMessage";

		// Token: 0x0400048D RID: 1165
		internal const string ODataReaderCore_SyncCallOnAsyncReader = "ODataReaderCore_SyncCallOnAsyncReader";

		// Token: 0x0400048E RID: 1166
		internal const string ODataReaderCore_AsyncCallOnSyncReader = "ODataReaderCore_AsyncCallOnSyncReader";

		// Token: 0x0400048F RID: 1167
		internal const string ODataReaderCore_ReadOrReadAsyncCalledInInvalidState = "ODataReaderCore_ReadOrReadAsyncCalledInInvalidState";

		// Token: 0x04000490 RID: 1168
		internal const string ODataReaderCore_NoReadCallsAllowed = "ODataReaderCore_NoReadCallsAllowed";

		// Token: 0x04000491 RID: 1169
		internal const string ODataJsonReader_CannotReadResourcesOfResourceSet = "ODataJsonReader_CannotReadResourcesOfResourceSet";

		// Token: 0x04000492 RID: 1170
		internal const string ODataJsonReaderUtils_CannotConvertInt32 = "ODataJsonReaderUtils_CannotConvertInt32";

		// Token: 0x04000493 RID: 1171
		internal const string ODataJsonReaderUtils_CannotConvertDouble = "ODataJsonReaderUtils_CannotConvertDouble";

		// Token: 0x04000494 RID: 1172
		internal const string ODataJsonReaderUtils_CannotConvertBoolean = "ODataJsonReaderUtils_CannotConvertBoolean";

		// Token: 0x04000495 RID: 1173
		internal const string ODataJsonReaderUtils_CannotConvertDecimal = "ODataJsonReaderUtils_CannotConvertDecimal";

		// Token: 0x04000496 RID: 1174
		internal const string ODataJsonReaderUtils_CannotConvertDateTime = "ODataJsonReaderUtils_CannotConvertDateTime";

		// Token: 0x04000497 RID: 1175
		internal const string ODataJsonReaderUtils_CannotConvertDateTimeOffset = "ODataJsonReaderUtils_CannotConvertDateTimeOffset";

		// Token: 0x04000498 RID: 1176
		internal const string ODataJsonReaderUtils_ConflictBetweenInputFormatAndParameter = "ODataJsonReaderUtils_ConflictBetweenInputFormatAndParameter";

		// Token: 0x04000499 RID: 1177
		internal const string ODataJsonReaderUtils_MultipleErrorPropertiesWithSameName = "ODataJsonReaderUtils_MultipleErrorPropertiesWithSameName";

		// Token: 0x0400049A RID: 1178
		internal const string ODataJsonLightResourceSerializer_ActionsAndFunctionsGroupMustSpecifyTarget = "ODataJsonLightResourceSerializer_ActionsAndFunctionsGroupMustSpecifyTarget";

		// Token: 0x0400049B RID: 1179
		internal const string ODataJsonLightResourceSerializer_ActionsAndFunctionsGroupMustNotHaveDuplicateTarget = "ODataJsonLightResourceSerializer_ActionsAndFunctionsGroupMustNotHaveDuplicateTarget";

		// Token: 0x0400049C RID: 1180
		internal const string ODataJsonErrorDeserializer_TopLevelErrorWithInvalidProperty = "ODataJsonErrorDeserializer_TopLevelErrorWithInvalidProperty";

		// Token: 0x0400049D RID: 1181
		internal const string ODataJsonErrorDeserializer_TopLevelErrorMessageValueWithInvalidProperty = "ODataJsonErrorDeserializer_TopLevelErrorMessageValueWithInvalidProperty";

		// Token: 0x0400049E RID: 1182
		internal const string ODataCollectionReaderCore_ReadOrReadAsyncCalledInInvalidState = "ODataCollectionReaderCore_ReadOrReadAsyncCalledInInvalidState";

		// Token: 0x0400049F RID: 1183
		internal const string ODataCollectionReaderCore_SyncCallOnAsyncReader = "ODataCollectionReaderCore_SyncCallOnAsyncReader";

		// Token: 0x040004A0 RID: 1184
		internal const string ODataCollectionReaderCore_AsyncCallOnSyncReader = "ODataCollectionReaderCore_AsyncCallOnSyncReader";

		// Token: 0x040004A1 RID: 1185
		internal const string ODataCollectionReaderCore_ExpectedItemTypeSetInInvalidState = "ODataCollectionReaderCore_ExpectedItemTypeSetInInvalidState";

		// Token: 0x040004A2 RID: 1186
		internal const string ODataParameterReaderCore_ReadOrReadAsyncCalledInInvalidState = "ODataParameterReaderCore_ReadOrReadAsyncCalledInInvalidState";

		// Token: 0x040004A3 RID: 1187
		internal const string ODataParameterReaderCore_SyncCallOnAsyncReader = "ODataParameterReaderCore_SyncCallOnAsyncReader";

		// Token: 0x040004A4 RID: 1188
		internal const string ODataParameterReaderCore_AsyncCallOnSyncReader = "ODataParameterReaderCore_AsyncCallOnSyncReader";

		// Token: 0x040004A5 RID: 1189
		internal const string ODataParameterReaderCore_SubReaderMustBeCreatedAndReadToCompletionBeforeTheNextReadOrReadAsyncCall = "ODataParameterReaderCore_SubReaderMustBeCreatedAndReadToCompletionBeforeTheNextReadOrReadAsyncCall";

		// Token: 0x040004A6 RID: 1190
		internal const string ODataParameterReaderCore_SubReaderMustBeInCompletedStateBeforeTheNextReadOrReadAsyncCall = "ODataParameterReaderCore_SubReaderMustBeInCompletedStateBeforeTheNextReadOrReadAsyncCall";

		// Token: 0x040004A7 RID: 1191
		internal const string ODataParameterReaderCore_InvalidCreateReaderMethodCalledForState = "ODataParameterReaderCore_InvalidCreateReaderMethodCalledForState";

		// Token: 0x040004A8 RID: 1192
		internal const string ODataParameterReaderCore_CreateReaderAlreadyCalled = "ODataParameterReaderCore_CreateReaderAlreadyCalled";

		// Token: 0x040004A9 RID: 1193
		internal const string ODataParameterReaderCore_ParameterNameNotInMetadata = "ODataParameterReaderCore_ParameterNameNotInMetadata";

		// Token: 0x040004AA RID: 1194
		internal const string ODataParameterReaderCore_DuplicateParametersInPayload = "ODataParameterReaderCore_DuplicateParametersInPayload";

		// Token: 0x040004AB RID: 1195
		internal const string ODataParameterReaderCore_ParametersMissingInPayload = "ODataParameterReaderCore_ParametersMissingInPayload";

		// Token: 0x040004AC RID: 1196
		internal const string ValidationUtils_ActionsAndFunctionsMustSpecifyMetadata = "ValidationUtils_ActionsAndFunctionsMustSpecifyMetadata";

		// Token: 0x040004AD RID: 1197
		internal const string ValidationUtils_ActionsAndFunctionsMustSpecifyTarget = "ValidationUtils_ActionsAndFunctionsMustSpecifyTarget";

		// Token: 0x040004AE RID: 1198
		internal const string ValidationUtils_EnumerableContainsANullItem = "ValidationUtils_EnumerableContainsANullItem";

		// Token: 0x040004AF RID: 1199
		internal const string ValidationUtils_AssociationLinkMustSpecifyName = "ValidationUtils_AssociationLinkMustSpecifyName";

		// Token: 0x040004B0 RID: 1200
		internal const string ValidationUtils_AssociationLinkMustSpecifyUrl = "ValidationUtils_AssociationLinkMustSpecifyUrl";

		// Token: 0x040004B1 RID: 1201
		internal const string ValidationUtils_TypeNameMustNotBeEmpty = "ValidationUtils_TypeNameMustNotBeEmpty";

		// Token: 0x040004B2 RID: 1202
		internal const string ValidationUtils_PropertyDoesNotExistOnType = "ValidationUtils_PropertyDoesNotExistOnType";

		// Token: 0x040004B3 RID: 1203
		internal const string ValidationUtils_ResourceMustSpecifyUrl = "ValidationUtils_ResourceMustSpecifyUrl";

		// Token: 0x040004B4 RID: 1204
		internal const string ValidationUtils_ResourceMustSpecifyName = "ValidationUtils_ResourceMustSpecifyName";

		// Token: 0x040004B5 RID: 1205
		internal const string ValidationUtils_ServiceDocumentElementUrlMustNotBeNull = "ValidationUtils_ServiceDocumentElementUrlMustNotBeNull";

		// Token: 0x040004B6 RID: 1206
		internal const string ValidationUtils_NonPrimitiveTypeForPrimitiveValue = "ValidationUtils_NonPrimitiveTypeForPrimitiveValue";

		// Token: 0x040004B7 RID: 1207
		internal const string ValidationUtils_UnsupportedPrimitiveType = "ValidationUtils_UnsupportedPrimitiveType";

		// Token: 0x040004B8 RID: 1208
		internal const string ValidationUtils_IncompatiblePrimitiveItemType = "ValidationUtils_IncompatiblePrimitiveItemType";

		// Token: 0x040004B9 RID: 1209
		internal const string ValidationUtils_NonNullableCollectionElementsMustNotBeNull = "ValidationUtils_NonNullableCollectionElementsMustNotBeNull";

		// Token: 0x040004BA RID: 1210
		internal const string ValidationUtils_InvalidCollectionTypeName = "ValidationUtils_InvalidCollectionTypeName";

		// Token: 0x040004BB RID: 1211
		internal const string ValidationUtils_UnrecognizedTypeName = "ValidationUtils_UnrecognizedTypeName";

		// Token: 0x040004BC RID: 1212
		internal const string ValidationUtils_IncorrectTypeKind = "ValidationUtils_IncorrectTypeKind";

		// Token: 0x040004BD RID: 1213
		internal const string ValidationUtils_IncorrectTypeKindNoTypeName = "ValidationUtils_IncorrectTypeKindNoTypeName";

		// Token: 0x040004BE RID: 1214
		internal const string ValidationUtils_IncorrectValueTypeKind = "ValidationUtils_IncorrectValueTypeKind";

		// Token: 0x040004BF RID: 1215
		internal const string ValidationUtils_LinkMustSpecifyName = "ValidationUtils_LinkMustSpecifyName";

		// Token: 0x040004C0 RID: 1216
		internal const string ValidationUtils_MismatchPropertyKindForStreamProperty = "ValidationUtils_MismatchPropertyKindForStreamProperty";

		// Token: 0x040004C1 RID: 1217
		internal const string ValidationUtils_NestedCollectionsAreNotSupported = "ValidationUtils_NestedCollectionsAreNotSupported";

		// Token: 0x040004C2 RID: 1218
		internal const string ValidationUtils_StreamReferenceValuesNotSupportedInCollections = "ValidationUtils_StreamReferenceValuesNotSupportedInCollections";

		// Token: 0x040004C3 RID: 1219
		internal const string ValidationUtils_IncompatibleType = "ValidationUtils_IncompatibleType";

		// Token: 0x040004C4 RID: 1220
		internal const string ValidationUtils_OpenCollectionProperty = "ValidationUtils_OpenCollectionProperty";

		// Token: 0x040004C5 RID: 1221
		internal const string ValidationUtils_OpenStreamProperty = "ValidationUtils_OpenStreamProperty";

		// Token: 0x040004C6 RID: 1222
		internal const string ValidationUtils_InvalidCollectionTypeReference = "ValidationUtils_InvalidCollectionTypeReference";

		// Token: 0x040004C7 RID: 1223
		internal const string ValidationUtils_ResourceWithMediaResourceAndNonMLEType = "ValidationUtils_ResourceWithMediaResourceAndNonMLEType";

		// Token: 0x040004C8 RID: 1224
		internal const string ValidationUtils_ResourceWithoutMediaResourceAndMLEType = "ValidationUtils_ResourceWithoutMediaResourceAndMLEType";

		// Token: 0x040004C9 RID: 1225
		internal const string ValidationUtils_ResourceTypeNotAssignableToExpectedType = "ValidationUtils_ResourceTypeNotAssignableToExpectedType";

		// Token: 0x040004CA RID: 1226
		internal const string ValidationUtils_NavigationPropertyExpected = "ValidationUtils_NavigationPropertyExpected";

		// Token: 0x040004CB RID: 1227
		internal const string ValidationUtils_InvalidBatchBoundaryDelimiterLength = "ValidationUtils_InvalidBatchBoundaryDelimiterLength";

		// Token: 0x040004CC RID: 1228
		internal const string ValidationUtils_RecursionDepthLimitReached = "ValidationUtils_RecursionDepthLimitReached";

		// Token: 0x040004CD RID: 1229
		internal const string ValidationUtils_MaxDepthOfNestedEntriesExceeded = "ValidationUtils_MaxDepthOfNestedEntriesExceeded";

		// Token: 0x040004CE RID: 1230
		internal const string ValidationUtils_NullCollectionItemForNonNullableType = "ValidationUtils_NullCollectionItemForNonNullableType";

		// Token: 0x040004CF RID: 1231
		internal const string ValidationUtils_PropertiesMustNotContainReservedChars = "ValidationUtils_PropertiesMustNotContainReservedChars";

		// Token: 0x040004D0 RID: 1232
		internal const string ValidationUtils_WorkspaceResourceMustNotContainNullItem = "ValidationUtils_WorkspaceResourceMustNotContainNullItem";

		// Token: 0x040004D1 RID: 1233
		internal const string ValidationUtils_InvalidMetadataReferenceProperty = "ValidationUtils_InvalidMetadataReferenceProperty";

		// Token: 0x040004D2 RID: 1234
		internal const string WriterValidationUtils_PropertyMustNotBeNull = "WriterValidationUtils_PropertyMustNotBeNull";

		// Token: 0x040004D3 RID: 1235
		internal const string WriterValidationUtils_PropertiesMustHaveNonEmptyName = "WriterValidationUtils_PropertiesMustHaveNonEmptyName";

		// Token: 0x040004D4 RID: 1236
		internal const string WriterValidationUtils_MissingTypeNameWithMetadata = "WriterValidationUtils_MissingTypeNameWithMetadata";

		// Token: 0x040004D5 RID: 1237
		internal const string WriterValidationUtils_NextPageLinkInRequest = "WriterValidationUtils_NextPageLinkInRequest";

		// Token: 0x040004D6 RID: 1238
		internal const string WriterValidationUtils_DefaultStreamWithContentTypeWithoutReadLink = "WriterValidationUtils_DefaultStreamWithContentTypeWithoutReadLink";

		// Token: 0x040004D7 RID: 1239
		internal const string WriterValidationUtils_DefaultStreamWithReadLinkWithoutContentType = "WriterValidationUtils_DefaultStreamWithReadLinkWithoutContentType";

		// Token: 0x040004D8 RID: 1240
		internal const string WriterValidationUtils_StreamReferenceValueMustHaveEditLinkOrReadLink = "WriterValidationUtils_StreamReferenceValueMustHaveEditLinkOrReadLink";

		// Token: 0x040004D9 RID: 1241
		internal const string WriterValidationUtils_StreamReferenceValueMustHaveEditLinkToHaveETag = "WriterValidationUtils_StreamReferenceValueMustHaveEditLinkToHaveETag";

		// Token: 0x040004DA RID: 1242
		internal const string WriterValidationUtils_StreamReferenceValueEmptyContentType = "WriterValidationUtils_StreamReferenceValueEmptyContentType";

		// Token: 0x040004DB RID: 1243
		internal const string WriterValidationUtils_EntriesMustHaveNonEmptyId = "WriterValidationUtils_EntriesMustHaveNonEmptyId";

		// Token: 0x040004DC RID: 1244
		internal const string WriterValidationUtils_MessageWriterSettingsBaseUriMustBeNullOrAbsolute = "WriterValidationUtils_MessageWriterSettingsBaseUriMustBeNullOrAbsolute";

		// Token: 0x040004DD RID: 1245
		internal const string WriterValidationUtils_EntityReferenceLinkUrlMustNotBeNull = "WriterValidationUtils_EntityReferenceLinkUrlMustNotBeNull";

		// Token: 0x040004DE RID: 1246
		internal const string WriterValidationUtils_EntityReferenceLinksLinkMustNotBeNull = "WriterValidationUtils_EntityReferenceLinksLinkMustNotBeNull";

		// Token: 0x040004DF RID: 1247
		internal const string WriterValidationUtils_NestedResourceTypeNotCompatibleWithParentPropertyType = "WriterValidationUtils_NestedResourceTypeNotCompatibleWithParentPropertyType";

		// Token: 0x040004E0 RID: 1248
		internal const string WriterValidationUtils_ExpandedLinkIsCollectionTrueWithResourceContent = "WriterValidationUtils_ExpandedLinkIsCollectionTrueWithResourceContent";

		// Token: 0x040004E1 RID: 1249
		internal const string WriterValidationUtils_ExpandedLinkIsCollectionFalseWithResourceSetContent = "WriterValidationUtils_ExpandedLinkIsCollectionFalseWithResourceSetContent";

		// Token: 0x040004E2 RID: 1250
		internal const string WriterValidationUtils_ExpandedLinkIsCollectionTrueWithResourceMetadata = "WriterValidationUtils_ExpandedLinkIsCollectionTrueWithResourceMetadata";

		// Token: 0x040004E3 RID: 1251
		internal const string WriterValidationUtils_ExpandedLinkIsCollectionFalseWithResourceSetMetadata = "WriterValidationUtils_ExpandedLinkIsCollectionFalseWithResourceSetMetadata";

		// Token: 0x040004E4 RID: 1252
		internal const string WriterValidationUtils_ExpandedLinkWithResourceSetPayloadAndResourceMetadata = "WriterValidationUtils_ExpandedLinkWithResourceSetPayloadAndResourceMetadata";

		// Token: 0x040004E5 RID: 1253
		internal const string WriterValidationUtils_ExpandedLinkWithResourcePayloadAndResourceSetMetadata = "WriterValidationUtils_ExpandedLinkWithResourcePayloadAndResourceSetMetadata";

		// Token: 0x040004E6 RID: 1254
		internal const string WriterValidationUtils_CollectionPropertiesMustNotHaveNullValue = "WriterValidationUtils_CollectionPropertiesMustNotHaveNullValue";

		// Token: 0x040004E7 RID: 1255
		internal const string WriterValidationUtils_NonNullablePropertiesMustNotHaveNullValue = "WriterValidationUtils_NonNullablePropertiesMustNotHaveNullValue";

		// Token: 0x040004E8 RID: 1256
		internal const string WriterValidationUtils_StreamPropertiesMustNotHaveNullValue = "WriterValidationUtils_StreamPropertiesMustNotHaveNullValue";

		// Token: 0x040004E9 RID: 1257
		internal const string WriterValidationUtils_OperationInRequest = "WriterValidationUtils_OperationInRequest";

		// Token: 0x040004EA RID: 1258
		internal const string WriterValidationUtils_AssociationLinkInRequest = "WriterValidationUtils_AssociationLinkInRequest";

		// Token: 0x040004EB RID: 1259
		internal const string WriterValidationUtils_StreamPropertyInRequest = "WriterValidationUtils_StreamPropertyInRequest";

		// Token: 0x040004EC RID: 1260
		internal const string WriterValidationUtils_MessageWriterSettingsServiceDocumentUriMustBeNullOrAbsolute = "WriterValidationUtils_MessageWriterSettingsServiceDocumentUriMustBeNullOrAbsolute";

		// Token: 0x040004ED RID: 1261
		internal const string WriterValidationUtils_NavigationLinkMustSpecifyUrl = "WriterValidationUtils_NavigationLinkMustSpecifyUrl";

		// Token: 0x040004EE RID: 1262
		internal const string WriterValidationUtils_NestedResourceInfoMustSpecifyIsCollection = "WriterValidationUtils_NestedResourceInfoMustSpecifyIsCollection";

		// Token: 0x040004EF RID: 1263
		internal const string WriterValidationUtils_MessageWriterSettingsJsonPaddingOnRequestMessage = "WriterValidationUtils_MessageWriterSettingsJsonPaddingOnRequestMessage";

		// Token: 0x040004F0 RID: 1264
		internal const string XmlReaderExtension_InvalidNodeInStringValue = "XmlReaderExtension_InvalidNodeInStringValue";

		// Token: 0x040004F1 RID: 1265
		internal const string XmlReaderExtension_InvalidRootNode = "XmlReaderExtension_InvalidRootNode";

		// Token: 0x040004F2 RID: 1266
		internal const string ODataMetadataInputContext_ErrorReadingMetadata = "ODataMetadataInputContext_ErrorReadingMetadata";

		// Token: 0x040004F3 RID: 1267
		internal const string ODataMetadataOutputContext_ErrorWritingMetadata = "ODataMetadataOutputContext_ErrorWritingMetadata";

		// Token: 0x040004F4 RID: 1268
		internal const string ODataAtomDeserializer_RelativeUriUsedWithoutBaseUriSpecified = "ODataAtomDeserializer_RelativeUriUsedWithoutBaseUriSpecified";

		// Token: 0x040004F5 RID: 1269
		internal const string ODataAtomPropertyAndValueDeserializer_InvalidCollectionElement = "ODataAtomPropertyAndValueDeserializer_InvalidCollectionElement";

		// Token: 0x040004F6 RID: 1270
		internal const string ODataAtomPropertyAndValueDeserializer_NavigationPropertyInProperties = "ODataAtomPropertyAndValueDeserializer_NavigationPropertyInProperties";

		// Token: 0x040004F7 RID: 1271
		internal const string JsonLightInstanceAnnotationWriter_NullValueNotAllowedForInstanceAnnotation = "JsonLightInstanceAnnotationWriter_NullValueNotAllowedForInstanceAnnotation";

		// Token: 0x040004F8 RID: 1272
		internal const string EdmLibraryExtensions_OperationGroupReturningActionsAndFunctionsModelInvalid = "EdmLibraryExtensions_OperationGroupReturningActionsAndFunctionsModelInvalid";

		// Token: 0x040004F9 RID: 1273
		internal const string EdmLibraryExtensions_UnBoundOperationsFoundFromIEdmModelFindMethodIsInvalid = "EdmLibraryExtensions_UnBoundOperationsFoundFromIEdmModelFindMethodIsInvalid";

		// Token: 0x040004FA RID: 1274
		internal const string EdmLibraryExtensions_NoParameterBoundOperationsFoundFromIEdmModelFindMethodIsInvalid = "EdmLibraryExtensions_NoParameterBoundOperationsFoundFromIEdmModelFindMethodIsInvalid";

		// Token: 0x040004FB RID: 1275
		internal const string EdmLibraryExtensions_ValueOverflowForUnderlyingType = "EdmLibraryExtensions_ValueOverflowForUnderlyingType";

		// Token: 0x040004FC RID: 1276
		internal const string ODataAtomResourceDeserializer_ContentWithWrongType = "ODataAtomResourceDeserializer_ContentWithWrongType";

		// Token: 0x040004FD RID: 1277
		internal const string ODataAtomErrorDeserializer_MultipleErrorElementsWithSameName = "ODataAtomErrorDeserializer_MultipleErrorElementsWithSameName";

		// Token: 0x040004FE RID: 1278
		internal const string ODataAtomErrorDeserializer_MultipleInnerErrorElementsWithSameName = "ODataAtomErrorDeserializer_MultipleInnerErrorElementsWithSameName";

		// Token: 0x040004FF RID: 1279
		internal const string CollectionWithoutExpectedTypeValidator_InvalidItemTypeKind = "CollectionWithoutExpectedTypeValidator_InvalidItemTypeKind";

		// Token: 0x04000500 RID: 1280
		internal const string CollectionWithoutExpectedTypeValidator_IncompatibleItemTypeKind = "CollectionWithoutExpectedTypeValidator_IncompatibleItemTypeKind";

		// Token: 0x04000501 RID: 1281
		internal const string CollectionWithoutExpectedTypeValidator_IncompatibleItemTypeName = "CollectionWithoutExpectedTypeValidator_IncompatibleItemTypeName";

		// Token: 0x04000502 RID: 1282
		internal const string ResourceSetWithoutExpectedTypeValidator_IncompatibleTypes = "ResourceSetWithoutExpectedTypeValidator_IncompatibleTypes";

		// Token: 0x04000503 RID: 1283
		internal const string MessageStreamWrappingStream_ByteLimitExceeded = "MessageStreamWrappingStream_ByteLimitExceeded";

		// Token: 0x04000504 RID: 1284
		internal const string MetadataUtils_ResolveTypeName = "MetadataUtils_ResolveTypeName";

		// Token: 0x04000505 RID: 1285
		internal const string MetadataUtils_CalculateBindableOperationsForType = "MetadataUtils_CalculateBindableOperationsForType";

		// Token: 0x04000506 RID: 1286
		internal const string EdmValueUtils_UnsupportedPrimitiveType = "EdmValueUtils_UnsupportedPrimitiveType";

		// Token: 0x04000507 RID: 1287
		internal const string EdmValueUtils_IncorrectPrimitiveTypeKind = "EdmValueUtils_IncorrectPrimitiveTypeKind";

		// Token: 0x04000508 RID: 1288
		internal const string EdmValueUtils_IncorrectPrimitiveTypeKindNoTypeName = "EdmValueUtils_IncorrectPrimitiveTypeKindNoTypeName";

		// Token: 0x04000509 RID: 1289
		internal const string EdmValueUtils_CannotConvertTypeToClrValue = "EdmValueUtils_CannotConvertTypeToClrValue";

		// Token: 0x0400050A RID: 1290
		internal const string ODataEdmStructuredValue_UndeclaredProperty = "ODataEdmStructuredValue_UndeclaredProperty";

		// Token: 0x0400050B RID: 1291
		internal const string ODataMetadataBuilder_MissingEntitySetUri = "ODataMetadataBuilder_MissingEntitySetUri";

		// Token: 0x0400050C RID: 1292
		internal const string ODataMetadataBuilder_MissingSegmentForEntitySetUriSuffix = "ODataMetadataBuilder_MissingSegmentForEntitySetUriSuffix";

		// Token: 0x0400050D RID: 1293
		internal const string ODataMetadataBuilder_MissingEntityInstanceUri = "ODataMetadataBuilder_MissingEntityInstanceUri";

		// Token: 0x0400050E RID: 1294
		internal const string ODataMetadataBuilder_MissingParentIdOrContextUrl = "ODataMetadataBuilder_MissingParentIdOrContextUrl";

		// Token: 0x0400050F RID: 1295
		internal const string ODataMetadataBuilder_UnknownEntitySet = "ODataMetadataBuilder_UnknownEntitySet";

		// Token: 0x04000510 RID: 1296
		internal const string ODataJsonLightInputContext_EntityTypeMustBeCompatibleWithEntitySetBaseType = "ODataJsonLightInputContext_EntityTypeMustBeCompatibleWithEntitySetBaseType";

		// Token: 0x04000511 RID: 1297
		internal const string ODataJsonLightInputContext_PayloadKindDetectionForRequest = "ODataJsonLightInputContext_PayloadKindDetectionForRequest";

		// Token: 0x04000512 RID: 1298
		internal const string ODataJsonLightInputContext_OperationCannotBeNullForCreateParameterReader = "ODataJsonLightInputContext_OperationCannotBeNullForCreateParameterReader";

		// Token: 0x04000513 RID: 1299
		internal const string ODataJsonLightInputContext_NoEntitySetForRequest = "ODataJsonLightInputContext_NoEntitySetForRequest";

		// Token: 0x04000514 RID: 1300
		internal const string ODataJsonLightInputContext_ModelRequiredForReading = "ODataJsonLightInputContext_ModelRequiredForReading";

		// Token: 0x04000515 RID: 1301
		internal const string ODataJsonLightInputContext_ItemTypeRequiredForCollectionReaderInRequests = "ODataJsonLightInputContext_ItemTypeRequiredForCollectionReaderInRequests";

		// Token: 0x04000516 RID: 1302
		internal const string ODataJsonLightDeserializer_ContextLinkNotFoundAsFirstProperty = "ODataJsonLightDeserializer_ContextLinkNotFoundAsFirstProperty";

		// Token: 0x04000517 RID: 1303
		internal const string ODataJsonLightDeserializer_OnlyODataTypeAnnotationCanTargetInstanceAnnotation = "ODataJsonLightDeserializer_OnlyODataTypeAnnotationCanTargetInstanceAnnotation";

		// Token: 0x04000518 RID: 1304
		internal const string ODataJsonLightDeserializer_AnnotationTargetingInstanceAnnotationWithoutValue = "ODataJsonLightDeserializer_AnnotationTargetingInstanceAnnotationWithoutValue";

		// Token: 0x04000519 RID: 1305
		internal const string ODataJsonLightWriter_EntityReferenceLinkAfterResourceSetInRequest = "ODataJsonLightWriter_EntityReferenceLinkAfterResourceSetInRequest";

		// Token: 0x0400051A RID: 1306
		internal const string ODataJsonLightWriter_InstanceAnnotationNotSupportedOnExpandedResourceSet = "ODataJsonLightWriter_InstanceAnnotationNotSupportedOnExpandedResourceSet";

		// Token: 0x0400051B RID: 1307
		internal const string ODataJsonLightPropertyAndValueSerializer_NoExpectedTypeOrTypeNameSpecifiedForComplexValueRequest = "ODataJsonLightPropertyAndValueSerializer_NoExpectedTypeOrTypeNameSpecifiedForComplexValueRequest";

		// Token: 0x0400051C RID: 1308
		internal const string ODataJsonLightPropertyAndValueSerializer_NoExpectedTypeOrTypeNameSpecifiedForCollectionValueInRequest = "ODataJsonLightPropertyAndValueSerializer_NoExpectedTypeOrTypeNameSpecifiedForCollectionValueInRequest";

		// Token: 0x0400051D RID: 1309
		internal const string ODataResourceTypeContext_MetadataOrSerializationInfoMissing = "ODataResourceTypeContext_MetadataOrSerializationInfoMissing";

		// Token: 0x0400051E RID: 1310
		internal const string ODataResourceTypeContext_ODataResourceTypeNameMissing = "ODataResourceTypeContext_ODataResourceTypeNameMissing";

		// Token: 0x0400051F RID: 1311
		internal const string ODataContextUriBuilder_ValidateDerivedType = "ODataContextUriBuilder_ValidateDerivedType";

		// Token: 0x04000520 RID: 1312
		internal const string ODataContextUriBuilder_TypeNameMissingForTopLevelCollection = "ODataContextUriBuilder_TypeNameMissingForTopLevelCollection";

		// Token: 0x04000521 RID: 1313
		internal const string ODataContextUriBuilder_UnsupportedPayloadKind = "ODataContextUriBuilder_UnsupportedPayloadKind";

		// Token: 0x04000522 RID: 1314
		internal const string ODataContextUriBuilder_StreamValueMustBePropertiesOfODataResource = "ODataContextUriBuilder_StreamValueMustBePropertiesOfODataResource";

		// Token: 0x04000523 RID: 1315
		internal const string ODataContextUriBuilder_NavigationSourceOrTypeNameMissingForResourceOrResourceSet = "ODataContextUriBuilder_NavigationSourceOrTypeNameMissingForResourceOrResourceSet";

		// Token: 0x04000524 RID: 1316
		internal const string ODataContextUriBuilder_ODataUriMissingForIndividualProperty = "ODataContextUriBuilder_ODataUriMissingForIndividualProperty";

		// Token: 0x04000525 RID: 1317
		internal const string ODataContextUriBuilder_TypeNameMissingForProperty = "ODataContextUriBuilder_TypeNameMissingForProperty";

		// Token: 0x04000526 RID: 1318
		internal const string ODataContextUriBuilder_ODataPathInvalidForContainedElement = "ODataContextUriBuilder_ODataPathInvalidForContainedElement";

		// Token: 0x04000527 RID: 1319
		internal const string ODataJsonLightPropertyAndValueDeserializer_UnexpectedAnnotationProperties = "ODataJsonLightPropertyAndValueDeserializer_UnexpectedAnnotationProperties";

		// Token: 0x04000528 RID: 1320
		internal const string ODataJsonLightPropertyAndValueDeserializer_UnexpectedPropertyAnnotation = "ODataJsonLightPropertyAndValueDeserializer_UnexpectedPropertyAnnotation";

		// Token: 0x04000529 RID: 1321
		internal const string ODataJsonLightPropertyAndValueDeserializer_UnexpectedODataPropertyAnnotation = "ODataJsonLightPropertyAndValueDeserializer_UnexpectedODataPropertyAnnotation";

		// Token: 0x0400052A RID: 1322
		internal const string ODataJsonLightPropertyAndValueDeserializer_UnexpectedProperty = "ODataJsonLightPropertyAndValueDeserializer_UnexpectedProperty";

		// Token: 0x0400052B RID: 1323
		internal const string ODataJsonLightPropertyAndValueDeserializer_InvalidTopLevelPropertyPayload = "ODataJsonLightPropertyAndValueDeserializer_InvalidTopLevelPropertyPayload";

		// Token: 0x0400052C RID: 1324
		internal const string ODataJsonLightPropertyAndValueDeserializer_InvalidTopLevelPropertyName = "ODataJsonLightPropertyAndValueDeserializer_InvalidTopLevelPropertyName";

		// Token: 0x0400052D RID: 1325
		internal const string ODataJsonLightPropertyAndValueDeserializer_InvalidTypeName = "ODataJsonLightPropertyAndValueDeserializer_InvalidTypeName";

		// Token: 0x0400052E RID: 1326
		internal const string ODataJsonLightPropertyAndValueDeserializer_TopLevelPropertyAnnotationWithoutProperty = "ODataJsonLightPropertyAndValueDeserializer_TopLevelPropertyAnnotationWithoutProperty";

		// Token: 0x0400052F RID: 1327
		internal const string ODataJsonLightPropertyAndValueDeserializer_ComplexValuePropertyAnnotationWithoutProperty = "ODataJsonLightPropertyAndValueDeserializer_ComplexValuePropertyAnnotationWithoutProperty";

		// Token: 0x04000530 RID: 1328
		internal const string ODataJsonLightPropertyAndValueDeserializer_ComplexValueWithPropertyTypeAnnotation = "ODataJsonLightPropertyAndValueDeserializer_ComplexValueWithPropertyTypeAnnotation";

		// Token: 0x04000531 RID: 1329
		internal const string ODataJsonLightPropertyAndValueDeserializer_ComplexTypeAnnotationNotFirst = "ODataJsonLightPropertyAndValueDeserializer_ComplexTypeAnnotationNotFirst";

		// Token: 0x04000532 RID: 1330
		internal const string ODataJsonLightPropertyAndValueDeserializer_UnexpectedDataPropertyAnnotation = "ODataJsonLightPropertyAndValueDeserializer_UnexpectedDataPropertyAnnotation";

		// Token: 0x04000533 RID: 1331
		internal const string ODataJsonLightPropertyAndValueDeserializer_TypePropertyAfterValueProperty = "ODataJsonLightPropertyAndValueDeserializer_TypePropertyAfterValueProperty";

		// Token: 0x04000534 RID: 1332
		internal const string ODataJsonLightPropertyAndValueDeserializer_ODataTypeAnnotationInPrimitiveValue = "ODataJsonLightPropertyAndValueDeserializer_ODataTypeAnnotationInPrimitiveValue";

		// Token: 0x04000535 RID: 1333
		internal const string ODataJsonLightPropertyAndValueDeserializer_TopLevelPropertyWithPrimitiveNullValue = "ODataJsonLightPropertyAndValueDeserializer_TopLevelPropertyWithPrimitiveNullValue";

		// Token: 0x04000536 RID: 1334
		internal const string ODataJsonLightPropertyAndValueDeserializer_UnexpectedMetadataReferenceProperty = "ODataJsonLightPropertyAndValueDeserializer_UnexpectedMetadataReferenceProperty";

		// Token: 0x04000537 RID: 1335
		internal const string ODataJsonLightPropertyAndValueDeserializer_NoPropertyAndAnnotationAllowedInNullPayload = "ODataJsonLightPropertyAndValueDeserializer_NoPropertyAndAnnotationAllowedInNullPayload";

		// Token: 0x04000538 RID: 1336
		internal const string ODataJsonLightPropertyAndValueDeserializer_CollectionTypeNotExpected = "ODataJsonLightPropertyAndValueDeserializer_CollectionTypeNotExpected";

		// Token: 0x04000539 RID: 1337
		internal const string ODataJsonLightPropertyAndValueDeserializer_CollectionTypeExpected = "ODataJsonLightPropertyAndValueDeserializer_CollectionTypeExpected";

		// Token: 0x0400053A RID: 1338
		internal const string ODataJsonReaderCoreUtils_CannotReadSpatialPropertyValue = "ODataJsonReaderCoreUtils_CannotReadSpatialPropertyValue";

		// Token: 0x0400053B RID: 1339
		internal const string ODataJsonLightReader_UnexpectedPrimitiveValueForODataResource = "ODataJsonLightReader_UnexpectedPrimitiveValueForODataResource";

		// Token: 0x0400053C RID: 1340
		internal const string ODataJsonLightReaderUtils_AnnotationWithNullValue = "ODataJsonLightReaderUtils_AnnotationWithNullValue";

		// Token: 0x0400053D RID: 1341
		internal const string ODataJsonLightReaderUtils_InvalidValueForODataNullAnnotation = "ODataJsonLightReaderUtils_InvalidValueForODataNullAnnotation";

		// Token: 0x0400053E RID: 1342
		internal const string JsonLightInstanceAnnotationWriter_DuplicateAnnotationNameInCollection = "JsonLightInstanceAnnotationWriter_DuplicateAnnotationNameInCollection";

		// Token: 0x0400053F RID: 1343
		internal const string ODataJsonLightContextUriParser_NullMetadataDocumentUri = "ODataJsonLightContextUriParser_NullMetadataDocumentUri";

		// Token: 0x04000540 RID: 1344
		internal const string ODataJsonLightContextUriParser_ContextUriDoesNotMatchExpectedPayloadKind = "ODataJsonLightContextUriParser_ContextUriDoesNotMatchExpectedPayloadKind";

		// Token: 0x04000541 RID: 1345
		internal const string ODataJsonLightContextUriParser_InvalidEntitySetNameOrTypeName = "ODataJsonLightContextUriParser_InvalidEntitySetNameOrTypeName";

		// Token: 0x04000542 RID: 1346
		internal const string ODataJsonLightContextUriParser_InvalidPayloadKindWithSelectQueryOption = "ODataJsonLightContextUriParser_InvalidPayloadKindWithSelectQueryOption";

		// Token: 0x04000543 RID: 1347
		internal const string ODataJsonLightContextUriParser_NoModel = "ODataJsonLightContextUriParser_NoModel";

		// Token: 0x04000544 RID: 1348
		internal const string ODataJsonLightContextUriParser_InvalidContextUrl = "ODataJsonLightContextUriParser_InvalidContextUrl";

		// Token: 0x04000545 RID: 1349
		internal const string ODataJsonLightContextUriParser_LastSegmentIsKeySegment = "ODataJsonLightContextUriParser_LastSegmentIsKeySegment";

		// Token: 0x04000546 RID: 1350
		internal const string ODataJsonLightContextUriParser_TopLevelContextUrlShouldBeAbsolute = "ODataJsonLightContextUriParser_TopLevelContextUrlShouldBeAbsolute";

		// Token: 0x04000547 RID: 1351
		internal const string ODataJsonLightResourceDeserializer_ResourceTypeAnnotationNotFirst = "ODataJsonLightResourceDeserializer_ResourceTypeAnnotationNotFirst";

		// Token: 0x04000548 RID: 1352
		internal const string ODataJsonLightResourceDeserializer_ResourceInstanceAnnotationPrecededByProperty = "ODataJsonLightResourceDeserializer_ResourceInstanceAnnotationPrecededByProperty";

		// Token: 0x04000549 RID: 1353
		internal const string ODataJsonLightResourceDeserializer_CannotReadResourceSetContentStart = "ODataJsonLightResourceDeserializer_CannotReadResourceSetContentStart";

		// Token: 0x0400054A RID: 1354
		internal const string ODataJsonLightResourceDeserializer_ExpectedResourceSetPropertyNotFound = "ODataJsonLightResourceDeserializer_ExpectedResourceSetPropertyNotFound";

		// Token: 0x0400054B RID: 1355
		internal const string ODataJsonLightResourceDeserializer_InvalidNodeTypeForItemsInResourceSet = "ODataJsonLightResourceDeserializer_InvalidNodeTypeForItemsInResourceSet";

		// Token: 0x0400054C RID: 1356
		internal const string ODataJsonLightResourceDeserializer_InvalidPropertyAnnotationInTopLevelResourceSet = "ODataJsonLightResourceDeserializer_InvalidPropertyAnnotationInTopLevelResourceSet";

		// Token: 0x0400054D RID: 1357
		internal const string ODataJsonLightResourceDeserializer_InvalidPropertyInTopLevelResourceSet = "ODataJsonLightResourceDeserializer_InvalidPropertyInTopLevelResourceSet";

		// Token: 0x0400054E RID: 1358
		internal const string ODataJsonLightResourceDeserializer_PropertyWithoutValueWithWrongType = "ODataJsonLightResourceDeserializer_PropertyWithoutValueWithWrongType";

		// Token: 0x0400054F RID: 1359
		internal const string ODataJsonLightResourceDeserializer_OpenPropertyWithoutValue = "ODataJsonLightResourceDeserializer_OpenPropertyWithoutValue";

		// Token: 0x04000550 RID: 1360
		internal const string ODataJsonLightResourceDeserializer_StreamPropertyInRequest = "ODataJsonLightResourceDeserializer_StreamPropertyInRequest";

		// Token: 0x04000551 RID: 1361
		internal const string ODataJsonLightResourceDeserializer_UnexpectedStreamPropertyAnnotation = "ODataJsonLightResourceDeserializer_UnexpectedStreamPropertyAnnotation";

		// Token: 0x04000552 RID: 1362
		internal const string ODataJsonLightResourceDeserializer_StreamPropertyWithValue = "ODataJsonLightResourceDeserializer_StreamPropertyWithValue";

		// Token: 0x04000553 RID: 1363
		internal const string ODataJsonLightResourceDeserializer_UnexpectedDeferredLinkPropertyAnnotation = "ODataJsonLightResourceDeserializer_UnexpectedDeferredLinkPropertyAnnotation";

		// Token: 0x04000554 RID: 1364
		internal const string ODataJsonLightResourceDeserializer_CannotReadSingletonNestedResource = "ODataJsonLightResourceDeserializer_CannotReadSingletonNestedResource";

		// Token: 0x04000555 RID: 1365
		internal const string ODataJsonLightResourceDeserializer_CannotReadCollectionNestedResource = "ODataJsonLightResourceDeserializer_CannotReadCollectionNestedResource";

		// Token: 0x04000556 RID: 1366
		internal const string ODataJsonLightResourceDeserializer_CannotReadNestedResource = "ODataJsonLightResourceDeserializer_CannotReadNestedResource";

		// Token: 0x04000557 RID: 1367
		internal const string ODataJsonLightResourceDeserializer_UnexpectedExpandedSingletonNavigationLinkPropertyAnnotation = "ODataJsonLightResourceDeserializer_UnexpectedExpandedSingletonNavigationLinkPropertyAnnotation";

		// Token: 0x04000558 RID: 1368
		internal const string ODataJsonLightResourceDeserializer_UnexpectedExpandedCollectionNavigationLinkPropertyAnnotation = "ODataJsonLightResourceDeserializer_UnexpectedExpandedCollectionNavigationLinkPropertyAnnotation";

		// Token: 0x04000559 RID: 1369
		internal const string ODataJsonLightResourceDeserializer_UnexpectedComplexCollectionPropertyAnnotation = "ODataJsonLightResourceDeserializer_UnexpectedComplexCollectionPropertyAnnotation";

		// Token: 0x0400055A RID: 1370
		internal const string ODataJsonLightResourceDeserializer_DuplicateNestedResourceSetAnnotation = "ODataJsonLightResourceDeserializer_DuplicateNestedResourceSetAnnotation";

		// Token: 0x0400055B RID: 1371
		internal const string ODataJsonLightResourceDeserializer_UnexpectedPropertyAnnotationAfterExpandedResourceSet = "ODataJsonLightResourceDeserializer_UnexpectedPropertyAnnotationAfterExpandedResourceSet";

		// Token: 0x0400055C RID: 1372
		internal const string ODataJsonLightResourceDeserializer_UnexpectedNavigationLinkInRequestPropertyAnnotation = "ODataJsonLightResourceDeserializer_UnexpectedNavigationLinkInRequestPropertyAnnotation";

		// Token: 0x0400055D RID: 1373
		internal const string ODataJsonLightResourceDeserializer_ArrayValueForSingletonBindPropertyAnnotation = "ODataJsonLightResourceDeserializer_ArrayValueForSingletonBindPropertyAnnotation";

		// Token: 0x0400055E RID: 1374
		internal const string ODataJsonLightResourceDeserializer_StringValueForCollectionBindPropertyAnnotation = "ODataJsonLightResourceDeserializer_StringValueForCollectionBindPropertyAnnotation";

		// Token: 0x0400055F RID: 1375
		internal const string ODataJsonLightResourceDeserializer_EmptyBindArray = "ODataJsonLightResourceDeserializer_EmptyBindArray";

		// Token: 0x04000560 RID: 1376
		internal const string ODataJsonLightResourceDeserializer_NavigationPropertyWithoutValueAndEntityReferenceLink = "ODataJsonLightResourceDeserializer_NavigationPropertyWithoutValueAndEntityReferenceLink";

		// Token: 0x04000561 RID: 1377
		internal const string ODataJsonLightResourceDeserializer_SingletonNavigationPropertyWithBindingAndValue = "ODataJsonLightResourceDeserializer_SingletonNavigationPropertyWithBindingAndValue";

		// Token: 0x04000562 RID: 1378
		internal const string ODataJsonLightResourceDeserializer_PropertyWithoutValueWithUnknownType = "ODataJsonLightResourceDeserializer_PropertyWithoutValueWithUnknownType";

		// Token: 0x04000563 RID: 1379
		internal const string ODataJsonLightResourceDeserializer_OperationIsNotActionOrFunction = "ODataJsonLightResourceDeserializer_OperationIsNotActionOrFunction";

		// Token: 0x04000564 RID: 1380
		internal const string ODataJsonLightResourceDeserializer_MultipleOptionalPropertiesInOperation = "ODataJsonLightResourceDeserializer_MultipleOptionalPropertiesInOperation";

		// Token: 0x04000565 RID: 1381
		internal const string ODataJsonLightResourceDeserializer_OperationMissingTargetProperty = "ODataJsonLightResourceDeserializer_OperationMissingTargetProperty";

		// Token: 0x04000566 RID: 1382
		internal const string ODataJsonLightResourceDeserializer_MetadataReferencePropertyInRequest = "ODataJsonLightResourceDeserializer_MetadataReferencePropertyInRequest";

		// Token: 0x04000567 RID: 1383
		internal const string ODataJsonLightValidationUtils_OperationPropertyCannotBeNull = "ODataJsonLightValidationUtils_OperationPropertyCannotBeNull";

		// Token: 0x04000568 RID: 1384
		internal const string ODataJsonLightValidationUtils_OpenMetadataReferencePropertyNotSupported = "ODataJsonLightValidationUtils_OpenMetadataReferencePropertyNotSupported";

		// Token: 0x04000569 RID: 1385
		internal const string ODataJsonLightDeserializer_RelativeUriUsedWithouODataMetadataAnnotation = "ODataJsonLightDeserializer_RelativeUriUsedWithouODataMetadataAnnotation";

		// Token: 0x0400056A RID: 1386
		internal const string ODataJsonLightResourceMetadataContext_MetadataAnnotationMustBeInPayload = "ODataJsonLightResourceMetadataContext_MetadataAnnotationMustBeInPayload";

		// Token: 0x0400056B RID: 1387
		internal const string ODataJsonLightCollectionDeserializer_ExpectedCollectionPropertyNotFound = "ODataJsonLightCollectionDeserializer_ExpectedCollectionPropertyNotFound";

		// Token: 0x0400056C RID: 1388
		internal const string ODataJsonLightCollectionDeserializer_CannotReadCollectionContentStart = "ODataJsonLightCollectionDeserializer_CannotReadCollectionContentStart";

		// Token: 0x0400056D RID: 1389
		internal const string ODataJsonLightCollectionDeserializer_CannotReadCollectionEnd = "ODataJsonLightCollectionDeserializer_CannotReadCollectionEnd";

		// Token: 0x0400056E RID: 1390
		internal const string ODataJsonLightCollectionDeserializer_InvalidCollectionTypeName = "ODataJsonLightCollectionDeserializer_InvalidCollectionTypeName";

		// Token: 0x0400056F RID: 1391
		internal const string ODataJsonLightEntityReferenceLinkDeserializer_EntityReferenceLinkMustBeObjectValue = "ODataJsonLightEntityReferenceLinkDeserializer_EntityReferenceLinkMustBeObjectValue";

		// Token: 0x04000570 RID: 1392
		internal const string ODataJsonLightEntityReferenceLinkDeserializer_PropertyAnnotationForEntityReferenceLink = "ODataJsonLightEntityReferenceLinkDeserializer_PropertyAnnotationForEntityReferenceLink";

		// Token: 0x04000571 RID: 1393
		internal const string ODataJsonLightEntityReferenceLinkDeserializer_InvalidAnnotationInEntityReferenceLink = "ODataJsonLightEntityReferenceLinkDeserializer_InvalidAnnotationInEntityReferenceLink";

		// Token: 0x04000572 RID: 1394
		internal const string ODataJsonLightEntityReferenceLinkDeserializer_InvalidPropertyInEntityReferenceLink = "ODataJsonLightEntityReferenceLinkDeserializer_InvalidPropertyInEntityReferenceLink";

		// Token: 0x04000573 RID: 1395
		internal const string ODataJsonLightEntityReferenceLinkDeserializer_MissingEntityReferenceLinkProperty = "ODataJsonLightEntityReferenceLinkDeserializer_MissingEntityReferenceLinkProperty";

		// Token: 0x04000574 RID: 1396
		internal const string ODataJsonLightEntityReferenceLinkDeserializer_MultipleUriPropertiesInEntityReferenceLink = "ODataJsonLightEntityReferenceLinkDeserializer_MultipleUriPropertiesInEntityReferenceLink";

		// Token: 0x04000575 RID: 1397
		internal const string ODataJsonLightEntityReferenceLinkDeserializer_EntityReferenceLinkUrlCannotBeNull = "ODataJsonLightEntityReferenceLinkDeserializer_EntityReferenceLinkUrlCannotBeNull";

		// Token: 0x04000576 RID: 1398
		internal const string ODataJsonLightEntityReferenceLinkDeserializer_PropertyAnnotationForEntityReferenceLinks = "ODataJsonLightEntityReferenceLinkDeserializer_PropertyAnnotationForEntityReferenceLinks";

		// Token: 0x04000577 RID: 1399
		internal const string ODataJsonLightEntityReferenceLinkDeserializer_InvalidEntityReferenceLinksPropertyFound = "ODataJsonLightEntityReferenceLinkDeserializer_InvalidEntityReferenceLinksPropertyFound";

		// Token: 0x04000578 RID: 1400
		internal const string ODataJsonLightEntityReferenceLinkDeserializer_InvalidPropertyAnnotationInEntityReferenceLinks = "ODataJsonLightEntityReferenceLinkDeserializer_InvalidPropertyAnnotationInEntityReferenceLinks";

		// Token: 0x04000579 RID: 1401
		internal const string ODataJsonLightEntityReferenceLinkDeserializer_ExpectedEntityReferenceLinksPropertyNotFound = "ODataJsonLightEntityReferenceLinkDeserializer_ExpectedEntityReferenceLinksPropertyNotFound";

		// Token: 0x0400057A RID: 1402
		internal const string ODataJsonOperationsDeserializerUtils_OperationPropertyCannotBeNull = "ODataJsonOperationsDeserializerUtils_OperationPropertyCannotBeNull";

		// Token: 0x0400057B RID: 1403
		internal const string ODataJsonOperationsDeserializerUtils_OperationsPropertyMustHaveObjectValue = "ODataJsonOperationsDeserializerUtils_OperationsPropertyMustHaveObjectValue";

		// Token: 0x0400057C RID: 1404
		internal const string ODataJsonLightServiceDocumentDeserializer_DuplicatePropertiesInServiceDocument = "ODataJsonLightServiceDocumentDeserializer_DuplicatePropertiesInServiceDocument";

		// Token: 0x0400057D RID: 1405
		internal const string ODataJsonLightServiceDocumentDeserializer_DuplicatePropertiesInServiceDocumentElement = "ODataJsonLightServiceDocumentDeserializer_DuplicatePropertiesInServiceDocumentElement";

		// Token: 0x0400057E RID: 1406
		internal const string ODataJsonLightServiceDocumentDeserializer_MissingValuePropertyInServiceDocument = "ODataJsonLightServiceDocumentDeserializer_MissingValuePropertyInServiceDocument";

		// Token: 0x0400057F RID: 1407
		internal const string ODataJsonLightServiceDocumentDeserializer_MissingRequiredPropertyInServiceDocumentElement = "ODataJsonLightServiceDocumentDeserializer_MissingRequiredPropertyInServiceDocumentElement";

		// Token: 0x04000580 RID: 1408
		internal const string ODataJsonLightServiceDocumentDeserializer_PropertyAnnotationInServiceDocument = "ODataJsonLightServiceDocumentDeserializer_PropertyAnnotationInServiceDocument";

		// Token: 0x04000581 RID: 1409
		internal const string ODataJsonLightServiceDocumentDeserializer_InstanceAnnotationInServiceDocument = "ODataJsonLightServiceDocumentDeserializer_InstanceAnnotationInServiceDocument";

		// Token: 0x04000582 RID: 1410
		internal const string ODataJsonLightServiceDocumentDeserializer_PropertyAnnotationInServiceDocumentElement = "ODataJsonLightServiceDocumentDeserializer_PropertyAnnotationInServiceDocumentElement";

		// Token: 0x04000583 RID: 1411
		internal const string ODataJsonLightServiceDocumentDeserializer_InstanceAnnotationInServiceDocumentElement = "ODataJsonLightServiceDocumentDeserializer_InstanceAnnotationInServiceDocumentElement";

		// Token: 0x04000584 RID: 1412
		internal const string ODataJsonLightServiceDocumentDeserializer_UnexpectedPropertyInServiceDocumentElement = "ODataJsonLightServiceDocumentDeserializer_UnexpectedPropertyInServiceDocumentElement";

		// Token: 0x04000585 RID: 1413
		internal const string ODataJsonLightServiceDocumentDeserializer_UnexpectedPropertyInServiceDocument = "ODataJsonLightServiceDocumentDeserializer_UnexpectedPropertyInServiceDocument";

		// Token: 0x04000586 RID: 1414
		internal const string ODataJsonLightServiceDocumentDeserializer_PropertyAnnotationWithoutProperty = "ODataJsonLightServiceDocumentDeserializer_PropertyAnnotationWithoutProperty";

		// Token: 0x04000587 RID: 1415
		internal const string ODataJsonLightParameterDeserializer_PropertyAnnotationForParameters = "ODataJsonLightParameterDeserializer_PropertyAnnotationForParameters";

		// Token: 0x04000588 RID: 1416
		internal const string ODataJsonLightParameterDeserializer_PropertyAnnotationWithoutPropertyForParameters = "ODataJsonLightParameterDeserializer_PropertyAnnotationWithoutPropertyForParameters";

		// Token: 0x04000589 RID: 1417
		internal const string ODataJsonLightParameterDeserializer_UnsupportedPrimitiveParameterType = "ODataJsonLightParameterDeserializer_UnsupportedPrimitiveParameterType";

		// Token: 0x0400058A RID: 1418
		internal const string ODataJsonLightParameterDeserializer_NullCollectionExpected = "ODataJsonLightParameterDeserializer_NullCollectionExpected";

		// Token: 0x0400058B RID: 1419
		internal const string ODataJsonLightParameterDeserializer_UnsupportedParameterTypeKind = "ODataJsonLightParameterDeserializer_UnsupportedParameterTypeKind";

		// Token: 0x0400058C RID: 1420
		internal const string SelectedPropertiesNode_StarSegmentNotLastSegment = "SelectedPropertiesNode_StarSegmentNotLastSegment";

		// Token: 0x0400058D RID: 1421
		internal const string SelectedPropertiesNode_StarSegmentAfterTypeSegment = "SelectedPropertiesNode_StarSegmentAfterTypeSegment";

		// Token: 0x0400058E RID: 1422
		internal const string ODataJsonLightErrorDeserializer_PropertyAnnotationNotAllowedInErrorPayload = "ODataJsonLightErrorDeserializer_PropertyAnnotationNotAllowedInErrorPayload";

		// Token: 0x0400058F RID: 1423
		internal const string ODataJsonLightErrorDeserializer_InstanceAnnotationNotAllowedInErrorPayload = "ODataJsonLightErrorDeserializer_InstanceAnnotationNotAllowedInErrorPayload";

		// Token: 0x04000590 RID: 1424
		internal const string ODataJsonLightErrorDeserializer_PropertyAnnotationWithoutPropertyForError = "ODataJsonLightErrorDeserializer_PropertyAnnotationWithoutPropertyForError";

		// Token: 0x04000591 RID: 1425
		internal const string ODataJsonLightErrorDeserializer_TopLevelErrorValueWithInvalidProperty = "ODataJsonLightErrorDeserializer_TopLevelErrorValueWithInvalidProperty";

		// Token: 0x04000592 RID: 1426
		internal const string ODataConventionalUriBuilder_EntityTypeWithNoKeyProperties = "ODataConventionalUriBuilder_EntityTypeWithNoKeyProperties";

		// Token: 0x04000593 RID: 1427
		internal const string ODataConventionalUriBuilder_NullKeyValue = "ODataConventionalUriBuilder_NullKeyValue";

		// Token: 0x04000594 RID: 1428
		internal const string ODataResourceMetadataContext_EntityTypeWithNoKeyProperties = "ODataResourceMetadataContext_EntityTypeWithNoKeyProperties";

		// Token: 0x04000595 RID: 1429
		internal const string ODataResourceMetadataContext_NullKeyValue = "ODataResourceMetadataContext_NullKeyValue";

		// Token: 0x04000596 RID: 1430
		internal const string ODataResourceMetadataContext_KeyOrETagValuesMustBePrimitiveValues = "ODataResourceMetadataContext_KeyOrETagValuesMustBePrimitiveValues";

		// Token: 0x04000597 RID: 1431
		internal const string EdmValueUtils_NonPrimitiveValue = "EdmValueUtils_NonPrimitiveValue";

		// Token: 0x04000598 RID: 1432
		internal const string EdmValueUtils_PropertyDoesntExist = "EdmValueUtils_PropertyDoesntExist";

		// Token: 0x04000599 RID: 1433
		internal const string ODataPrimitiveValue_CannotCreateODataPrimitiveValueFromNull = "ODataPrimitiveValue_CannotCreateODataPrimitiveValueFromNull";

		// Token: 0x0400059A RID: 1434
		internal const string ODataPrimitiveValue_CannotCreateODataPrimitiveValueFromUnsupportedValueType = "ODataPrimitiveValue_CannotCreateODataPrimitiveValueFromUnsupportedValueType";

		// Token: 0x0400059B RID: 1435
		internal const string ODataInstanceAnnotation_NeedPeriodInName = "ODataInstanceAnnotation_NeedPeriodInName";

		// Token: 0x0400059C RID: 1436
		internal const string ODataInstanceAnnotation_ReservedNamesNotAllowed = "ODataInstanceAnnotation_ReservedNamesNotAllowed";

		// Token: 0x0400059D RID: 1437
		internal const string ODataInstanceAnnotation_BadTermName = "ODataInstanceAnnotation_BadTermName";

		// Token: 0x0400059E RID: 1438
		internal const string ODataInstanceAnnotation_ValueCannotBeODataStreamReferenceValue = "ODataInstanceAnnotation_ValueCannotBeODataStreamReferenceValue";

		// Token: 0x0400059F RID: 1439
		internal const string ODataJsonLightValueSerializer_MissingTypeNameOnCollection = "ODataJsonLightValueSerializer_MissingTypeNameOnCollection";

		// Token: 0x040005A0 RID: 1440
		internal const string ODataJsonLightValueSerializer_MissingRawValueOnUntyped = "ODataJsonLightValueSerializer_MissingRawValueOnUntyped";

		// Token: 0x040005A1 RID: 1441
		internal const string AtomInstanceAnnotation_MissingTermAttributeOnAnnotationElement = "AtomInstanceAnnotation_MissingTermAttributeOnAnnotationElement";

		// Token: 0x040005A2 RID: 1442
		internal const string AtomInstanceAnnotation_AttributeValueNotationUsedWithIncompatibleType = "AtomInstanceAnnotation_AttributeValueNotationUsedWithIncompatibleType";

		// Token: 0x040005A3 RID: 1443
		internal const string AtomInstanceAnnotation_AttributeValueNotationUsedOnNonEmptyElement = "AtomInstanceAnnotation_AttributeValueNotationUsedOnNonEmptyElement";

		// Token: 0x040005A4 RID: 1444
		internal const string AtomInstanceAnnotation_MultipleAttributeValueNotationAttributes = "AtomInstanceAnnotation_MultipleAttributeValueNotationAttributes";

		// Token: 0x040005A5 RID: 1445
		internal const string AnnotationFilterPattern_InvalidPatternMissingDot = "AnnotationFilterPattern_InvalidPatternMissingDot";

		// Token: 0x040005A6 RID: 1446
		internal const string AnnotationFilterPattern_InvalidPatternEmptySegment = "AnnotationFilterPattern_InvalidPatternEmptySegment";

		// Token: 0x040005A7 RID: 1447
		internal const string AnnotationFilterPattern_InvalidPatternWildCardInSegment = "AnnotationFilterPattern_InvalidPatternWildCardInSegment";

		// Token: 0x040005A8 RID: 1448
		internal const string AnnotationFilterPattern_InvalidPatternWildCardMustBeInLastSegment = "AnnotationFilterPattern_InvalidPatternWildCardMustBeInLastSegment";

		// Token: 0x040005A9 RID: 1449
		internal const string SyntacticTree_UriMustBeAbsolute = "SyntacticTree_UriMustBeAbsolute";

		// Token: 0x040005AA RID: 1450
		internal const string SyntacticTree_MaxDepthInvalid = "SyntacticTree_MaxDepthInvalid";

		// Token: 0x040005AB RID: 1451
		internal const string SyntacticTree_InvalidSkipQueryOptionValue = "SyntacticTree_InvalidSkipQueryOptionValue";

		// Token: 0x040005AC RID: 1452
		internal const string SyntacticTree_InvalidTopQueryOptionValue = "SyntacticTree_InvalidTopQueryOptionValue";

		// Token: 0x040005AD RID: 1453
		internal const string SyntacticTree_InvalidCountQueryOptionValue = "SyntacticTree_InvalidCountQueryOptionValue";

		// Token: 0x040005AE RID: 1454
		internal const string QueryOptionUtils_QueryParameterMustBeSpecifiedOnce = "QueryOptionUtils_QueryParameterMustBeSpecifiedOnce";

		// Token: 0x040005AF RID: 1455
		internal const string UriBuilder_NotSupportedClrLiteral = "UriBuilder_NotSupportedClrLiteral";

		// Token: 0x040005B0 RID: 1456
		internal const string UriBuilder_NotSupportedQueryToken = "UriBuilder_NotSupportedQueryToken";

		// Token: 0x040005B1 RID: 1457
		internal const string UriQueryExpressionParser_TooDeep = "UriQueryExpressionParser_TooDeep";

		// Token: 0x040005B2 RID: 1458
		internal const string UriQueryExpressionParser_ExpressionExpected = "UriQueryExpressionParser_ExpressionExpected";

		// Token: 0x040005B3 RID: 1459
		internal const string UriQueryExpressionParser_OpenParenExpected = "UriQueryExpressionParser_OpenParenExpected";

		// Token: 0x040005B4 RID: 1460
		internal const string UriQueryExpressionParser_CloseParenOrCommaExpected = "UriQueryExpressionParser_CloseParenOrCommaExpected";

		// Token: 0x040005B5 RID: 1461
		internal const string UriQueryExpressionParser_CloseParenOrOperatorExpected = "UriQueryExpressionParser_CloseParenOrOperatorExpected";

		// Token: 0x040005B6 RID: 1462
		internal const string UriQueryExpressionParser_CannotCreateStarTokenFromNonStar = "UriQueryExpressionParser_CannotCreateStarTokenFromNonStar";

		// Token: 0x040005B7 RID: 1463
		internal const string UriQueryExpressionParser_RangeVariableAlreadyDeclared = "UriQueryExpressionParser_RangeVariableAlreadyDeclared";

		// Token: 0x040005B8 RID: 1464
		internal const string UriQueryExpressionParser_AsExpected = "UriQueryExpressionParser_AsExpected";

		// Token: 0x040005B9 RID: 1465
		internal const string UriQueryExpressionParser_WithExpected = "UriQueryExpressionParser_WithExpected";

		// Token: 0x040005BA RID: 1466
		internal const string UriQueryExpressionParser_UnrecognizedWithMethod = "UriQueryExpressionParser_UnrecognizedWithMethod";

		// Token: 0x040005BB RID: 1467
		internal const string UriQueryExpressionParser_PropertyPathExpected = "UriQueryExpressionParser_PropertyPathExpected";

		// Token: 0x040005BC RID: 1468
		internal const string UriQueryExpressionParser_KeywordOrIdentifierExpected = "UriQueryExpressionParser_KeywordOrIdentifierExpected";

		// Token: 0x040005BD RID: 1469
		internal const string UriQueryPathParser_RequestUriDoesNotHaveTheCorrectBaseUri = "UriQueryPathParser_RequestUriDoesNotHaveTheCorrectBaseUri";

		// Token: 0x040005BE RID: 1470
		internal const string UriQueryPathParser_SyntaxError = "UriQueryPathParser_SyntaxError";

		// Token: 0x040005BF RID: 1471
		internal const string UriQueryPathParser_TooManySegments = "UriQueryPathParser_TooManySegments";

		// Token: 0x040005C0 RID: 1472
		internal const string UriUtils_DateTimeOffsetInvalidFormat = "UriUtils_DateTimeOffsetInvalidFormat";

		// Token: 0x040005C1 RID: 1473
		internal const string SelectionItemBinder_NonNavigationPathToken = "SelectionItemBinder_NonNavigationPathToken";

		// Token: 0x040005C2 RID: 1474
		internal const string MetadataBinder_UnsupportedQueryTokenKind = "MetadataBinder_UnsupportedQueryTokenKind";

		// Token: 0x040005C3 RID: 1475
		internal const string MetadataBinder_PropertyNotDeclared = "MetadataBinder_PropertyNotDeclared";

		// Token: 0x040005C4 RID: 1476
		internal const string MetadataBinder_PropertyNotDeclaredOrNotKeyInKeyValue = "MetadataBinder_PropertyNotDeclaredOrNotKeyInKeyValue";

		// Token: 0x040005C5 RID: 1477
		internal const string MetadataBinder_QualifiedFunctionNameWithParametersNotDeclared = "MetadataBinder_QualifiedFunctionNameWithParametersNotDeclared";

		// Token: 0x040005C6 RID: 1478
		internal const string MetadataBinder_UnnamedKeyValueOnTypeWithMultipleKeyProperties = "MetadataBinder_UnnamedKeyValueOnTypeWithMultipleKeyProperties";

		// Token: 0x040005C7 RID: 1479
		internal const string MetadataBinder_DuplicitKeyPropertyInKeyValues = "MetadataBinder_DuplicitKeyPropertyInKeyValues";

		// Token: 0x040005C8 RID: 1480
		internal const string MetadataBinder_NotAllKeyPropertiesSpecifiedInKeyValues = "MetadataBinder_NotAllKeyPropertiesSpecifiedInKeyValues";

		// Token: 0x040005C9 RID: 1481
		internal const string MetadataBinder_CannotConvertToType = "MetadataBinder_CannotConvertToType";

		// Token: 0x040005CA RID: 1482
		internal const string MetadataBinder_FilterExpressionNotSingleValue = "MetadataBinder_FilterExpressionNotSingleValue";

		// Token: 0x040005CB RID: 1483
		internal const string MetadataBinder_OrderByExpressionNotSingleValue = "MetadataBinder_OrderByExpressionNotSingleValue";

		// Token: 0x040005CC RID: 1484
		internal const string MetadataBinder_PropertyAccessWithoutParentParameter = "MetadataBinder_PropertyAccessWithoutParentParameter";

		// Token: 0x040005CD RID: 1485
		internal const string MetadataBinder_BinaryOperatorOperandNotSingleValue = "MetadataBinder_BinaryOperatorOperandNotSingleValue";

		// Token: 0x040005CE RID: 1486
		internal const string MetadataBinder_UnaryOperatorOperandNotSingleValue = "MetadataBinder_UnaryOperatorOperandNotSingleValue";

		// Token: 0x040005CF RID: 1487
		internal const string MetadataBinder_PropertyAccessSourceNotSingleValue = "MetadataBinder_PropertyAccessSourceNotSingleValue";

		// Token: 0x040005D0 RID: 1488
		internal const string MetadataBinder_IncompatibleOperandsError = "MetadataBinder_IncompatibleOperandsError";

		// Token: 0x040005D1 RID: 1489
		internal const string MetadataBinder_IncompatibleOperandError = "MetadataBinder_IncompatibleOperandError";

		// Token: 0x040005D2 RID: 1490
		internal const string MetadataBinder_UnknownFunction = "MetadataBinder_UnknownFunction";

		// Token: 0x040005D3 RID: 1491
		internal const string MetadataBinder_FunctionArgumentNotSingleValue = "MetadataBinder_FunctionArgumentNotSingleValue";

		// Token: 0x040005D4 RID: 1492
		internal const string MetadataBinder_NoApplicableFunctionFound = "MetadataBinder_NoApplicableFunctionFound";

		// Token: 0x040005D5 RID: 1493
		internal const string MetadataBinder_BoundNodeCannotBeNull = "MetadataBinder_BoundNodeCannotBeNull";

		// Token: 0x040005D6 RID: 1494
		internal const string MetadataBinder_TopRequiresNonNegativeInteger = "MetadataBinder_TopRequiresNonNegativeInteger";

		// Token: 0x040005D7 RID: 1495
		internal const string MetadataBinder_SkipRequiresNonNegativeInteger = "MetadataBinder_SkipRequiresNonNegativeInteger";

		// Token: 0x040005D8 RID: 1496
		internal const string MetadataBinder_QueryOptionsBindStateCannotBeNull = "MetadataBinder_QueryOptionsBindStateCannotBeNull";

		// Token: 0x040005D9 RID: 1497
		internal const string MetadataBinder_QueryOptionsBindMethodCannotBeNull = "MetadataBinder_QueryOptionsBindMethodCannotBeNull";

		// Token: 0x040005DA RID: 1498
		internal const string MetadataBinder_HierarchyNotFollowed = "MetadataBinder_HierarchyNotFollowed";

		// Token: 0x040005DB RID: 1499
		internal const string MetadataBinder_LambdaParentMustBeCollection = "MetadataBinder_LambdaParentMustBeCollection";

		// Token: 0x040005DC RID: 1500
		internal const string MetadataBinder_ParameterNotInScope = "MetadataBinder_ParameterNotInScope";

		// Token: 0x040005DD RID: 1501
		internal const string MetadataBinder_NavigationPropertyNotFollowingSingleEntityType = "MetadataBinder_NavigationPropertyNotFollowingSingleEntityType";

		// Token: 0x040005DE RID: 1502
		internal const string MetadataBinder_AnyAllExpressionNotSingleValue = "MetadataBinder_AnyAllExpressionNotSingleValue";

		// Token: 0x040005DF RID: 1503
		internal const string MetadataBinder_CastOrIsOfExpressionWithWrongNumberOfOperands = "MetadataBinder_CastOrIsOfExpressionWithWrongNumberOfOperands";

		// Token: 0x040005E0 RID: 1504
		internal const string MetadataBinder_CastOrIsOfFunctionWithoutATypeArgument = "MetadataBinder_CastOrIsOfFunctionWithoutATypeArgument";

		// Token: 0x040005E1 RID: 1505
		internal const string MetadataBinder_CastOrIsOfCollectionsNotSupported = "MetadataBinder_CastOrIsOfCollectionsNotSupported";

		// Token: 0x040005E2 RID: 1506
		internal const string MetadataBinder_CollectionOpenPropertiesNotSupportedInThisRelease = "MetadataBinder_CollectionOpenPropertiesNotSupportedInThisRelease";

		// Token: 0x040005E3 RID: 1507
		internal const string MetadataBinder_IllegalSegmentType = "MetadataBinder_IllegalSegmentType";

		// Token: 0x040005E4 RID: 1508
		internal const string MetadataBinder_QueryOptionNotApplicable = "MetadataBinder_QueryOptionNotApplicable";

		// Token: 0x040005E5 RID: 1509
		internal const string ApplyBinder_AggregateExpressionIncompatibleTypeForMethod = "ApplyBinder_AggregateExpressionIncompatibleTypeForMethod";

		// Token: 0x040005E6 RID: 1510
		internal const string ApplyBinder_UnsupportedAggregateMethod = "ApplyBinder_UnsupportedAggregateMethod";

		// Token: 0x040005E7 RID: 1511
		internal const string ApplyBinder_AggregateExpressionNotSingleValue = "ApplyBinder_AggregateExpressionNotSingleValue";

		// Token: 0x040005E8 RID: 1512
		internal const string ApplyBinder_GroupByPropertyNotPropertyAccessValue = "ApplyBinder_GroupByPropertyNotPropertyAccessValue";

		// Token: 0x040005E9 RID: 1513
		internal const string ApplyBinder_UnsupportedType = "ApplyBinder_UnsupportedType";

		// Token: 0x040005EA RID: 1514
		internal const string ApplyBinder_UnsupportedGroupByChild = "ApplyBinder_UnsupportedGroupByChild";

		// Token: 0x040005EB RID: 1515
		internal const string FunctionCallBinder_CannotFindASuitableOverload = "FunctionCallBinder_CannotFindASuitableOverload";

		// Token: 0x040005EC RID: 1516
		internal const string FunctionCallBinder_UriFunctionMustHaveHaveNullParent = "FunctionCallBinder_UriFunctionMustHaveHaveNullParent";

		// Token: 0x040005ED RID: 1517
		internal const string FunctionCallBinder_CallingFunctionOnOpenProperty = "FunctionCallBinder_CallingFunctionOnOpenProperty";

		// Token: 0x040005EE RID: 1518
		internal const string FunctionCallParser_DuplicateParameterOrEntityKeyName = "FunctionCallParser_DuplicateParameterOrEntityKeyName";

		// Token: 0x040005EF RID: 1519
		internal const string ODataUriParser_InvalidCount = "ODataUriParser_InvalidCount";

		// Token: 0x040005F0 RID: 1520
		internal const string CastBinder_ChildTypeIsNotEntity = "CastBinder_ChildTypeIsNotEntity";

		// Token: 0x040005F1 RID: 1521
		internal const string CastBinder_EnumOnlyCastToOrFromString = "CastBinder_EnumOnlyCastToOrFromString";

		// Token: 0x040005F2 RID: 1522
		internal const string Binder_IsNotValidEnumConstant = "Binder_IsNotValidEnumConstant";

		// Token: 0x040005F3 RID: 1523
		internal const string BatchReferenceSegment_InvalidContentID = "BatchReferenceSegment_InvalidContentID";

		// Token: 0x040005F4 RID: 1524
		internal const string SelectExpandBinder_UnknownPropertyType = "SelectExpandBinder_UnknownPropertyType";

		// Token: 0x040005F5 RID: 1525
		internal const string SelectionItemBinder_NoExpandForSelectedProperty = "SelectionItemBinder_NoExpandForSelectedProperty";

		// Token: 0x040005F6 RID: 1526
		internal const string SelectExpandPathBinder_FollowNonTypeSegment = "SelectExpandPathBinder_FollowNonTypeSegment";

		// Token: 0x040005F7 RID: 1527
		internal const string SelectPropertyVisitor_SystemTokenInSelect = "SelectPropertyVisitor_SystemTokenInSelect";

		// Token: 0x040005F8 RID: 1528
		internal const string SelectPropertyVisitor_DisparateTypeSegmentsInSelectExpand = "SelectPropertyVisitor_DisparateTypeSegmentsInSelectExpand";

		// Token: 0x040005F9 RID: 1529
		internal const string SelectBinder_MultiLevelPathInSelect = "SelectBinder_MultiLevelPathInSelect";

		// Token: 0x040005FA RID: 1530
		internal const string ExpandItemBinder_TraversingANonNormalizedTree = "ExpandItemBinder_TraversingANonNormalizedTree";

		// Token: 0x040005FB RID: 1531
		internal const string ExpandItemBinder_CannotFindType = "ExpandItemBinder_CannotFindType";

		// Token: 0x040005FC RID: 1532
		internal const string ExpandItemBinder_PropertyIsNotANavigationPropertyOrComplexProperty = "ExpandItemBinder_PropertyIsNotANavigationPropertyOrComplexProperty";

		// Token: 0x040005FD RID: 1533
		internal const string ExpandItemBinder_TypeSegmentNotFollowedByPath = "ExpandItemBinder_TypeSegmentNotFollowedByPath";

		// Token: 0x040005FE RID: 1534
		internal const string ExpandItemBinder_PathTooDeep = "ExpandItemBinder_PathTooDeep";

		// Token: 0x040005FF RID: 1535
		internal const string ExpandItemBinder_TraversingMultipleNavPropsInTheSamePath = "ExpandItemBinder_TraversingMultipleNavPropsInTheSamePath";

		// Token: 0x04000600 RID: 1536
		internal const string ExpandItemBinder_LevelsNotAllowedOnIncompatibleRelatedType = "ExpandItemBinder_LevelsNotAllowedOnIncompatibleRelatedType";

		// Token: 0x04000601 RID: 1537
		internal const string ExpandItemBinder_InvaidSegmentInExpand = "ExpandItemBinder_InvaidSegmentInExpand";

		// Token: 0x04000602 RID: 1538
		internal const string Nodes_CollectionNavigationNode_MustHaveSingleMultiplicity = "Nodes_CollectionNavigationNode_MustHaveSingleMultiplicity";

		// Token: 0x04000603 RID: 1539
		internal const string Nodes_NonentityParameterQueryNodeWithEntityType = "Nodes_NonentityParameterQueryNodeWithEntityType";

		// Token: 0x04000604 RID: 1540
		internal const string Nodes_CollectionNavigationNode_MustHaveManyMultiplicity = "Nodes_CollectionNavigationNode_MustHaveManyMultiplicity";

		// Token: 0x04000605 RID: 1541
		internal const string Nodes_PropertyAccessShouldBeNonEntityProperty = "Nodes_PropertyAccessShouldBeNonEntityProperty";

		// Token: 0x04000606 RID: 1542
		internal const string Nodes_PropertyAccessTypeShouldNotBeCollection = "Nodes_PropertyAccessTypeShouldNotBeCollection";

		// Token: 0x04000607 RID: 1543
		internal const string Nodes_PropertyAccessTypeMustBeCollection = "Nodes_PropertyAccessTypeMustBeCollection";

		// Token: 0x04000608 RID: 1544
		internal const string Nodes_NonStaticEntitySetExpressionsAreNotSupportedInThisRelease = "Nodes_NonStaticEntitySetExpressionsAreNotSupportedInThisRelease";

		// Token: 0x04000609 RID: 1545
		internal const string Nodes_CollectionFunctionCallNode_ItemTypeMustBePrimitiveOrComplexOrEnum = "Nodes_CollectionFunctionCallNode_ItemTypeMustBePrimitiveOrComplexOrEnum";

		// Token: 0x0400060A RID: 1546
		internal const string Nodes_EntityCollectionFunctionCallNode_ItemTypeMustBeAnEntity = "Nodes_EntityCollectionFunctionCallNode_ItemTypeMustBeAnEntity";

		// Token: 0x0400060B RID: 1547
		internal const string Nodes_SingleValueFunctionCallNode_ItemTypeMustBePrimitiveOrComplexOrEnum = "Nodes_SingleValueFunctionCallNode_ItemTypeMustBePrimitiveOrComplexOrEnum";

		// Token: 0x0400060C RID: 1548
		internal const string ExpandTreeNormalizer_NonPathInPropertyChain = "ExpandTreeNormalizer_NonPathInPropertyChain";

		// Token: 0x0400060D RID: 1549
		internal const string UriExpandParser_TermIsNotValidForStar = "UriExpandParser_TermIsNotValidForStar";

		// Token: 0x0400060E RID: 1550
		internal const string UriExpandParser_TermIsNotValidForStarRef = "UriExpandParser_TermIsNotValidForStarRef";

		// Token: 0x0400060F RID: 1551
		internal const string UriExpandParser_ParentEntityIsNull = "UriExpandParser_ParentEntityIsNull";

		// Token: 0x04000610 RID: 1552
		internal const string UriExpandParser_TermWithMultipleStarNotAllowed = "UriExpandParser_TermWithMultipleStarNotAllowed";

		// Token: 0x04000611 RID: 1553
		internal const string UriSelectParser_TermIsNotValid = "UriSelectParser_TermIsNotValid";

		// Token: 0x04000612 RID: 1554
		internal const string UriSelectParser_InvalidTopOption = "UriSelectParser_InvalidTopOption";

		// Token: 0x04000613 RID: 1555
		internal const string UriSelectParser_InvalidSkipOption = "UriSelectParser_InvalidSkipOption";

		// Token: 0x04000614 RID: 1556
		internal const string UriSelectParser_InvalidCountOption = "UriSelectParser_InvalidCountOption";

		// Token: 0x04000615 RID: 1557
		internal const string UriSelectParser_InvalidLevelsOption = "UriSelectParser_InvalidLevelsOption";

		// Token: 0x04000616 RID: 1558
		internal const string UriSelectParser_SystemTokenInSelectExpand = "UriSelectParser_SystemTokenInSelectExpand";

		// Token: 0x04000617 RID: 1559
		internal const string UriParser_MissingExpandOption = "UriParser_MissingExpandOption";

		// Token: 0x04000618 RID: 1560
		internal const string UriParser_RelativeUriMustBeRelative = "UriParser_RelativeUriMustBeRelative";

		// Token: 0x04000619 RID: 1561
		internal const string UriParser_NeedServiceRootForThisOverload = "UriParser_NeedServiceRootForThisOverload";

		// Token: 0x0400061A RID: 1562
		internal const string UriParser_UriMustBeAbsolute = "UriParser_UriMustBeAbsolute";

		// Token: 0x0400061B RID: 1563
		internal const string UriParser_NegativeLimit = "UriParser_NegativeLimit";

		// Token: 0x0400061C RID: 1564
		internal const string UriParser_ExpandCountExceeded = "UriParser_ExpandCountExceeded";

		// Token: 0x0400061D RID: 1565
		internal const string UriParser_ExpandDepthExceeded = "UriParser_ExpandDepthExceeded";

		// Token: 0x0400061E RID: 1566
		internal const string UriParser_TypeInvalidForSelectExpand = "UriParser_TypeInvalidForSelectExpand";

		// Token: 0x0400061F RID: 1567
		internal const string UriParser_ContextHandlerCanNotBeNull = "UriParser_ContextHandlerCanNotBeNull";

		// Token: 0x04000620 RID: 1568
		internal const string UriParserMetadata_MultipleMatchingPropertiesFound = "UriParserMetadata_MultipleMatchingPropertiesFound";

		// Token: 0x04000621 RID: 1569
		internal const string UriParserMetadata_MultipleMatchingNavigationSourcesFound = "UriParserMetadata_MultipleMatchingNavigationSourcesFound";

		// Token: 0x04000622 RID: 1570
		internal const string UriParserMetadata_MultipleMatchingTypesFound = "UriParserMetadata_MultipleMatchingTypesFound";

		// Token: 0x04000623 RID: 1571
		internal const string UriParserMetadata_MultipleMatchingKeysFound = "UriParserMetadata_MultipleMatchingKeysFound";

		// Token: 0x04000624 RID: 1572
		internal const string UriParserMetadata_MultipleMatchingParametersFound = "UriParserMetadata_MultipleMatchingParametersFound";

		// Token: 0x04000625 RID: 1573
		internal const string PathParser_EntityReferenceNotSupported = "PathParser_EntityReferenceNotSupported";

		// Token: 0x04000626 RID: 1574
		internal const string PathParser_CannotUseValueOnCollection = "PathParser_CannotUseValueOnCollection";

		// Token: 0x04000627 RID: 1575
		internal const string PathParser_TypeMustBeRelatedToSet = "PathParser_TypeMustBeRelatedToSet";

		// Token: 0x04000628 RID: 1576
		internal const string PathParser_TypeCastOnlyAllowedAfterStructuralCollection = "PathParser_TypeCastOnlyAllowedAfterStructuralCollection";

		// Token: 0x04000629 RID: 1577
		internal const string ODataResourceSet_MustNotContainBothNextPageLinkAndDeltaLink = "ODataResourceSet_MustNotContainBothNextPageLinkAndDeltaLink";

		// Token: 0x0400062A RID: 1578
		internal const string ODataExpandPath_OnlyLastSegmentMustBeNavigationProperty = "ODataExpandPath_OnlyLastSegmentMustBeNavigationProperty";

		// Token: 0x0400062B RID: 1579
		internal const string ODataExpandPath_InvalidExpandPathSegment = "ODataExpandPath_InvalidExpandPathSegment";

		// Token: 0x0400062C RID: 1580
		internal const string ODataSelectPath_CannotOnlyHaveTypeSegment = "ODataSelectPath_CannotOnlyHaveTypeSegment";

		// Token: 0x0400062D RID: 1581
		internal const string ODataSelectPath_InvalidSelectPathSegmentType = "ODataSelectPath_InvalidSelectPathSegmentType";

		// Token: 0x0400062E RID: 1582
		internal const string ODataSelectPath_OperationSegmentCanOnlyBeLastSegment = "ODataSelectPath_OperationSegmentCanOnlyBeLastSegment";

		// Token: 0x0400062F RID: 1583
		internal const string ODataSelectPath_NavPropSegmentCanOnlyBeLastSegment = "ODataSelectPath_NavPropSegmentCanOnlyBeLastSegment";

		// Token: 0x04000630 RID: 1584
		internal const string RequestUriProcessor_TargetEntitySetNotFound = "RequestUriProcessor_TargetEntitySetNotFound";

		// Token: 0x04000631 RID: 1585
		internal const string RequestUriProcessor_FoundInvalidFunctionImport = "RequestUriProcessor_FoundInvalidFunctionImport";

		// Token: 0x04000632 RID: 1586
		internal const string OperationSegment_ReturnTypeForMultipleOverloads = "OperationSegment_ReturnTypeForMultipleOverloads";

		// Token: 0x04000633 RID: 1587
		internal const string OperationSegment_CannotReturnNull = "OperationSegment_CannotReturnNull";

		// Token: 0x04000634 RID: 1588
		internal const string FunctionOverloadResolver_NoSingleMatchFound = "FunctionOverloadResolver_NoSingleMatchFound";

		// Token: 0x04000635 RID: 1589
		internal const string FunctionOverloadResolver_MultipleActionOverloads = "FunctionOverloadResolver_MultipleActionOverloads";

		// Token: 0x04000636 RID: 1590
		internal const string FunctionOverloadResolver_MultipleActionImportOverloads = "FunctionOverloadResolver_MultipleActionImportOverloads";

		// Token: 0x04000637 RID: 1591
		internal const string FunctionOverloadResolver_MultipleOperationImportOverloads = "FunctionOverloadResolver_MultipleOperationImportOverloads";

		// Token: 0x04000638 RID: 1592
		internal const string FunctionOverloadResolver_MultipleOperationOverloads = "FunctionOverloadResolver_MultipleOperationOverloads";

		// Token: 0x04000639 RID: 1593
		internal const string FunctionOverloadResolver_FoundInvalidOperation = "FunctionOverloadResolver_FoundInvalidOperation";

		// Token: 0x0400063A RID: 1594
		internal const string FunctionOverloadResolver_FoundInvalidOperationImport = "FunctionOverloadResolver_FoundInvalidOperationImport";

		// Token: 0x0400063B RID: 1595
		internal const string CustomUriFunctions_AddCustomUriFunction_BuiltInExistsNotAddingAsOverload = "CustomUriFunctions_AddCustomUriFunction_BuiltInExistsNotAddingAsOverload";

		// Token: 0x0400063C RID: 1596
		internal const string CustomUriFunctions_AddCustomUriFunction_BuiltInExistsFullSignature = "CustomUriFunctions_AddCustomUriFunction_BuiltInExistsFullSignature";

		// Token: 0x0400063D RID: 1597
		internal const string CustomUriFunctions_AddCustomUriFunction_CustomFunctionOverloadExists = "CustomUriFunctions_AddCustomUriFunction_CustomFunctionOverloadExists";

		// Token: 0x0400063E RID: 1598
		internal const string RequestUriProcessor_InvalidValueForEntitySegment = "RequestUriProcessor_InvalidValueForEntitySegment";

		// Token: 0x0400063F RID: 1599
		internal const string RequestUriProcessor_InvalidValueForKeySegment = "RequestUriProcessor_InvalidValueForKeySegment";

		// Token: 0x04000640 RID: 1600
		internal const string RequestUriProcessor_EmptySegmentInRequestUrl = "RequestUriProcessor_EmptySegmentInRequestUrl";

		// Token: 0x04000641 RID: 1601
		internal const string RequestUriProcessor_SyntaxError = "RequestUriProcessor_SyntaxError";

		// Token: 0x04000642 RID: 1602
		internal const string RequestUriProcessor_CountOnRoot = "RequestUriProcessor_CountOnRoot";

		// Token: 0x04000643 RID: 1603
		internal const string RequestUriProcessor_MustBeLeafSegment = "RequestUriProcessor_MustBeLeafSegment";

		// Token: 0x04000644 RID: 1604
		internal const string RequestUriProcessor_LinkSegmentMustBeFollowedByEntitySegment = "RequestUriProcessor_LinkSegmentMustBeFollowedByEntitySegment";

		// Token: 0x04000645 RID: 1605
		internal const string RequestUriProcessor_MissingSegmentAfterLink = "RequestUriProcessor_MissingSegmentAfterLink";

		// Token: 0x04000646 RID: 1606
		internal const string RequestUriProcessor_CountNotSupported = "RequestUriProcessor_CountNotSupported";

		// Token: 0x04000647 RID: 1607
		internal const string RequestUriProcessor_CannotQueryCollections = "RequestUriProcessor_CannotQueryCollections";

		// Token: 0x04000648 RID: 1608
		internal const string RequestUriProcessor_SegmentDoesNotSupportKeyPredicates = "RequestUriProcessor_SegmentDoesNotSupportKeyPredicates";

		// Token: 0x04000649 RID: 1609
		internal const string RequestUriProcessor_ValueSegmentAfterScalarPropertySegment = "RequestUriProcessor_ValueSegmentAfterScalarPropertySegment";

		// Token: 0x0400064A RID: 1610
		internal const string RequestUriProcessor_InvalidTypeIdentifier_UnrelatedType = "RequestUriProcessor_InvalidTypeIdentifier_UnrelatedType";

		// Token: 0x0400064B RID: 1611
		internal const string OpenNavigationPropertiesNotSupportedOnOpenTypes = "OpenNavigationPropertiesNotSupportedOnOpenTypes";

		// Token: 0x0400064C RID: 1612
		internal const string BadRequest_ResourceCanBeCrossReferencedOnlyForBindOperation = "BadRequest_ResourceCanBeCrossReferencedOnlyForBindOperation";

		// Token: 0x0400064D RID: 1613
		internal const string DataServiceConfiguration_ResponseVersionIsBiggerThanProtocolVersion = "DataServiceConfiguration_ResponseVersionIsBiggerThanProtocolVersion";

		// Token: 0x0400064E RID: 1614
		internal const string BadRequest_KeyCountMismatch = "BadRequest_KeyCountMismatch";

		// Token: 0x0400064F RID: 1615
		internal const string RequestUriProcessor_KeysMustBeNamed = "RequestUriProcessor_KeysMustBeNamed";

		// Token: 0x04000650 RID: 1616
		internal const string RequestUriProcessor_ResourceNotFound = "RequestUriProcessor_ResourceNotFound";

		// Token: 0x04000651 RID: 1617
		internal const string RequestUriProcessor_BatchedActionOnEntityCreatedInSameChangeset = "RequestUriProcessor_BatchedActionOnEntityCreatedInSameChangeset";

		// Token: 0x04000652 RID: 1618
		internal const string RequestUriProcessor_Forbidden = "RequestUriProcessor_Forbidden";

		// Token: 0x04000653 RID: 1619
		internal const string RequestUriProcessor_OperationSegmentBoundToANonEntityType = "RequestUriProcessor_OperationSegmentBoundToANonEntityType";

		// Token: 0x04000654 RID: 1620
		internal const string General_InternalError = "General_InternalError";

		// Token: 0x04000655 RID: 1621
		internal const string ExceptionUtils_CheckIntegerNotNegative = "ExceptionUtils_CheckIntegerNotNegative";

		// Token: 0x04000656 RID: 1622
		internal const string ExceptionUtils_CheckIntegerPositive = "ExceptionUtils_CheckIntegerPositive";

		// Token: 0x04000657 RID: 1623
		internal const string ExceptionUtils_CheckLongPositive = "ExceptionUtils_CheckLongPositive";

		// Token: 0x04000658 RID: 1624
		internal const string ExceptionUtils_ArgumentStringNullOrEmpty = "ExceptionUtils_ArgumentStringNullOrEmpty";

		// Token: 0x04000659 RID: 1625
		internal const string ExpressionToken_OnlyRefAllowWithStarInExpand = "ExpressionToken_OnlyRefAllowWithStarInExpand";

		// Token: 0x0400065A RID: 1626
		internal const string ExpressionToken_NoPropAllowedAfterRef = "ExpressionToken_NoPropAllowedAfterRef";

		// Token: 0x0400065B RID: 1627
		internal const string ExpressionToken_NoSegmentAllowedBeforeStarInExpand = "ExpressionToken_NoSegmentAllowedBeforeStarInExpand";

		// Token: 0x0400065C RID: 1628
		internal const string ExpressionToken_IdentifierExpected = "ExpressionToken_IdentifierExpected";

		// Token: 0x0400065D RID: 1629
		internal const string ExpressionLexer_UnterminatedStringLiteral = "ExpressionLexer_UnterminatedStringLiteral";

		// Token: 0x0400065E RID: 1630
		internal const string ExpressionLexer_InvalidCharacter = "ExpressionLexer_InvalidCharacter";

		// Token: 0x0400065F RID: 1631
		internal const string ExpressionLexer_SyntaxError = "ExpressionLexer_SyntaxError";

		// Token: 0x04000660 RID: 1632
		internal const string ExpressionLexer_UnterminatedLiteral = "ExpressionLexer_UnterminatedLiteral";

		// Token: 0x04000661 RID: 1633
		internal const string ExpressionLexer_DigitExpected = "ExpressionLexer_DigitExpected";

		// Token: 0x04000662 RID: 1634
		internal const string ExpressionLexer_UnbalancedBracketExpression = "ExpressionLexer_UnbalancedBracketExpression";

		// Token: 0x04000663 RID: 1635
		internal const string ExpressionLexer_InvalidNumericString = "ExpressionLexer_InvalidNumericString";

		// Token: 0x04000664 RID: 1636
		internal const string ExpressionLexer_InvalidEscapeSequence = "ExpressionLexer_InvalidEscapeSequence";

		// Token: 0x04000665 RID: 1637
		internal const string UriQueryExpressionParser_UnrecognizedLiteral = "UriQueryExpressionParser_UnrecognizedLiteral";

		// Token: 0x04000666 RID: 1638
		internal const string UriQueryExpressionParser_UnrecognizedLiteralWithReason = "UriQueryExpressionParser_UnrecognizedLiteralWithReason";

		// Token: 0x04000667 RID: 1639
		internal const string UriPrimitiveTypeParsers_FailedToParseTextToPrimitiveValue = "UriPrimitiveTypeParsers_FailedToParseTextToPrimitiveValue";

		// Token: 0x04000668 RID: 1640
		internal const string UriPrimitiveTypeParsers_FailedToParseStringToGeography = "UriPrimitiveTypeParsers_FailedToParseStringToGeography";

		// Token: 0x04000669 RID: 1641
		internal const string UriCustomTypeParsers_AddCustomUriTypeParserAlreadyExists = "UriCustomTypeParsers_AddCustomUriTypeParserAlreadyExists";

		// Token: 0x0400066A RID: 1642
		internal const string UriCustomTypeParsers_AddCustomUriTypeParserEdmTypeExists = "UriCustomTypeParsers_AddCustomUriTypeParserEdmTypeExists";

		// Token: 0x0400066B RID: 1643
		internal const string UriParserHelper_InvalidPrefixLiteral = "UriParserHelper_InvalidPrefixLiteral";

		// Token: 0x0400066C RID: 1644
		internal const string CustomUriTypePrefixLiterals_AddCustomUriTypePrefixLiteralAlreadyExists = "CustomUriTypePrefixLiterals_AddCustomUriTypePrefixLiteralAlreadyExists";

		// Token: 0x0400066D RID: 1645
		internal const string ValueParser_InvalidDuration = "ValueParser_InvalidDuration";

		// Token: 0x0400066E RID: 1646
		internal const string PlatformHelper_DateTimeOffsetMustContainTimeZone = "PlatformHelper_DateTimeOffsetMustContainTimeZone";

		// Token: 0x0400066F RID: 1647
		internal const string JsonReader_UnexpectedComma = "JsonReader_UnexpectedComma";

		// Token: 0x04000670 RID: 1648
		internal const string JsonReader_MultipleTopLevelValues = "JsonReader_MultipleTopLevelValues";

		// Token: 0x04000671 RID: 1649
		internal const string JsonReader_EndOfInputWithOpenScope = "JsonReader_EndOfInputWithOpenScope";

		// Token: 0x04000672 RID: 1650
		internal const string JsonReader_UnexpectedToken = "JsonReader_UnexpectedToken";

		// Token: 0x04000673 RID: 1651
		internal const string JsonReader_UnrecognizedToken = "JsonReader_UnrecognizedToken";

		// Token: 0x04000674 RID: 1652
		internal const string JsonReader_MissingColon = "JsonReader_MissingColon";

		// Token: 0x04000675 RID: 1653
		internal const string JsonReader_UnrecognizedEscapeSequence = "JsonReader_UnrecognizedEscapeSequence";

		// Token: 0x04000676 RID: 1654
		internal const string JsonReader_UnexpectedEndOfString = "JsonReader_UnexpectedEndOfString";

		// Token: 0x04000677 RID: 1655
		internal const string JsonReader_InvalidNumberFormat = "JsonReader_InvalidNumberFormat";

		// Token: 0x04000678 RID: 1656
		internal const string JsonReader_MissingComma = "JsonReader_MissingComma";

		// Token: 0x04000679 RID: 1657
		internal const string JsonReader_InvalidPropertyNameOrUnexpectedComma = "JsonReader_InvalidPropertyNameOrUnexpectedComma";

		// Token: 0x0400067A RID: 1658
		internal const string JsonReaderExtensions_UnexpectedNodeDetected = "JsonReaderExtensions_UnexpectedNodeDetected";

		// Token: 0x0400067B RID: 1659
		internal const string JsonReaderExtensions_UnexpectedNodeDetectedWithPropertyName = "JsonReaderExtensions_UnexpectedNodeDetectedWithPropertyName";

		// Token: 0x0400067C RID: 1660
		internal const string JsonReaderExtensions_CannotReadPropertyValueAsString = "JsonReaderExtensions_CannotReadPropertyValueAsString";

		// Token: 0x0400067D RID: 1661
		internal const string JsonReaderExtensions_CannotReadValueAsString = "JsonReaderExtensions_CannotReadValueAsString";

		// Token: 0x0400067E RID: 1662
		internal const string JsonReaderExtensions_CannotReadValueAsDouble = "JsonReaderExtensions_CannotReadValueAsDouble";

		// Token: 0x0400067F RID: 1663
		internal const string JsonReaderExtensions_UnexpectedInstanceAnnotationName = "JsonReaderExtensions_UnexpectedInstanceAnnotationName";

		// Token: 0x04000680 RID: 1664
		internal const string ServiceProviderExtensions_NoServiceRegistered = "ServiceProviderExtensions_NoServiceRegistered";

		// Token: 0x04000681 RID: 1665
		private static TextRes loader;

		// Token: 0x04000682 RID: 1666
		private ResourceManager resources;
	}
}
