using System;

namespace Microsoft.OData
{
	// Token: 0x020000F1 RID: 241
	internal static class Strings
	{
		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000B32 RID: 2866 RVA: 0x0001DEAC File Offset: 0x0001C0AC
		internal static string ExceptionUtils_ArgumentStringEmpty
		{
			get
			{
				return TextRes.GetString("ExceptionUtils_ArgumentStringEmpty");
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000B33 RID: 2867 RVA: 0x0001DEB8 File Offset: 0x0001C0B8
		internal static string ODataRequestMessage_AsyncNotAvailable
		{
			get
			{
				return TextRes.GetString("ODataRequestMessage_AsyncNotAvailable");
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000B34 RID: 2868 RVA: 0x0001DEC4 File Offset: 0x0001C0C4
		internal static string ODataRequestMessage_StreamTaskIsNull
		{
			get
			{
				return TextRes.GetString("ODataRequestMessage_StreamTaskIsNull");
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000B35 RID: 2869 RVA: 0x0001DED0 File Offset: 0x0001C0D0
		internal static string ODataRequestMessage_MessageStreamIsNull
		{
			get
			{
				return TextRes.GetString("ODataRequestMessage_MessageStreamIsNull");
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000B36 RID: 2870 RVA: 0x0001DEDC File Offset: 0x0001C0DC
		internal static string ODataResponseMessage_AsyncNotAvailable
		{
			get
			{
				return TextRes.GetString("ODataResponseMessage_AsyncNotAvailable");
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000B37 RID: 2871 RVA: 0x0001DEE8 File Offset: 0x0001C0E8
		internal static string ODataResponseMessage_StreamTaskIsNull
		{
			get
			{
				return TextRes.GetString("ODataResponseMessage_StreamTaskIsNull");
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000B38 RID: 2872 RVA: 0x0001DEF4 File Offset: 0x0001C0F4
		internal static string ODataResponseMessage_MessageStreamIsNull
		{
			get
			{
				return TextRes.GetString("ODataResponseMessage_MessageStreamIsNull");
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000B39 RID: 2873 RVA: 0x0001DF00 File Offset: 0x0001C100
		internal static string AsyncBufferedStream_WriterDisposedWithoutFlush
		{
			get
			{
				return TextRes.GetString("AsyncBufferedStream_WriterDisposedWithoutFlush");
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000B3A RID: 2874 RVA: 0x0001DF0C File Offset: 0x0001C10C
		internal static string ODataFormat_AtomFormatObsoleted
		{
			get
			{
				return TextRes.GetString("ODataFormat_AtomFormatObsoleted");
			}
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x0001DF18 File Offset: 0x0001C118
		internal static string ODataOutputContext_UnsupportedPayloadKindForFormat(object p0, object p1)
		{
			return TextRes.GetString("ODataOutputContext_UnsupportedPayloadKindForFormat", new object[] { p0, p1 });
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x0001DF32 File Offset: 0x0001C132
		internal static string ODataInputContext_UnsupportedPayloadKindForFormat(object p0, object p1)
		{
			return TextRes.GetString("ODataInputContext_UnsupportedPayloadKindForFormat", new object[] { p0, p1 });
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000B3D RID: 2877 RVA: 0x0001DF4C File Offset: 0x0001C14C
		internal static string ODataOutputContext_MetadataDocumentUriMissing
		{
			get
			{
				return TextRes.GetString("ODataOutputContext_MetadataDocumentUriMissing");
			}
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x0001DF58 File Offset: 0x0001C158
		internal static string ODataJsonLightSerializer_RelativeUriUsedWithoutMetadataDocumentUriOrMetadata(object p0)
		{
			return TextRes.GetString("ODataJsonLightSerializer_RelativeUriUsedWithoutMetadataDocumentUriOrMetadata", new object[] { p0 });
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x0001DF6E File Offset: 0x0001C16E
		internal static string ODataWriter_RelativeUriUsedWithoutBaseUriSpecified(object p0)
		{
			return TextRes.GetString("ODataWriter_RelativeUriUsedWithoutBaseUriSpecified", new object[] { p0 });
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x0001DF84 File Offset: 0x0001C184
		internal static string ODataWriter_StreamPropertiesMustBePropertiesOfODataResource(object p0)
		{
			return TextRes.GetString("ODataWriter_StreamPropertiesMustBePropertiesOfODataResource", new object[] { p0 });
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x0001DF9A File Offset: 0x0001C19A
		internal static string ODataWriterCore_InvalidStateTransition(object p0, object p1)
		{
			return TextRes.GetString("ODataWriterCore_InvalidStateTransition", new object[] { p0, p1 });
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x0001DFB4 File Offset: 0x0001C1B4
		internal static string ODataWriterCore_InvalidTransitionFromStart(object p0, object p1)
		{
			return TextRes.GetString("ODataWriterCore_InvalidTransitionFromStart", new object[] { p0, p1 });
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x0001DFCE File Offset: 0x0001C1CE
		internal static string ODataWriterCore_InvalidTransitionFromResource(object p0, object p1)
		{
			return TextRes.GetString("ODataWriterCore_InvalidTransitionFromResource", new object[] { p0, p1 });
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x0001DFE8 File Offset: 0x0001C1E8
		internal static string ODataWriterCore_InvalidTransitionFrom40DeletedResource(object p0, object p1)
		{
			return TextRes.GetString("ODataWriterCore_InvalidTransitionFrom40DeletedResource", new object[] { p0, p1 });
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x0001E002 File Offset: 0x0001C202
		internal static string ODataWriterCore_InvalidTransitionFromNullResource(object p0, object p1)
		{
			return TextRes.GetString("ODataWriterCore_InvalidTransitionFromNullResource", new object[] { p0, p1 });
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x0001E01C File Offset: 0x0001C21C
		internal static string ODataWriterCore_InvalidTransitionFromResourceSet(object p0, object p1)
		{
			return TextRes.GetString("ODataWriterCore_InvalidTransitionFromResourceSet", new object[] { p0, p1 });
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x0001E036 File Offset: 0x0001C236
		internal static string ODataWriterCore_InvalidTransitionFromExpandedLink(object p0, object p1)
		{
			return TextRes.GetString("ODataWriterCore_InvalidTransitionFromExpandedLink", new object[] { p0, p1 });
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x0001E050 File Offset: 0x0001C250
		internal static string ODataWriterCore_InvalidTransitionFromCompleted(object p0, object p1)
		{
			return TextRes.GetString("ODataWriterCore_InvalidTransitionFromCompleted", new object[] { p0, p1 });
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x0001E06A File Offset: 0x0001C26A
		internal static string ODataWriterCore_InvalidTransitionFromError(object p0, object p1)
		{
			return TextRes.GetString("ODataWriterCore_InvalidTransitionFromError", new object[] { p0, p1 });
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x0001E084 File Offset: 0x0001C284
		internal static string ODataJsonLightDeltaWriter_InvalidTransitionFromNestedResource(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightDeltaWriter_InvalidTransitionFromNestedResource", new object[] { p0, p1 });
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x0001E09E File Offset: 0x0001C29E
		internal static string ODataJsonLightDeltaWriter_InvalidTransitionToNestedResource(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightDeltaWriter_InvalidTransitionToNestedResource", new object[] { p0, p1 });
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x0001E0B8 File Offset: 0x0001C2B8
		internal static string ODataJsonLightDeltaWriter_WriteStartExpandedResourceSetCalledInInvalidState(object p0)
		{
			return TextRes.GetString("ODataJsonLightDeltaWriter_WriteStartExpandedResourceSetCalledInInvalidState", new object[] { p0 });
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x0001E0CE File Offset: 0x0001C2CE
		internal static string ODataWriterCore_WriteEndCalledInInvalidState(object p0)
		{
			return TextRes.GetString("ODataWriterCore_WriteEndCalledInInvalidState", new object[] { p0 });
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000B4E RID: 2894 RVA: 0x0001E0E4 File Offset: 0x0001C2E4
		internal static string ODataWriterCore_StreamNotDisposed
		{
			get
			{
				return TextRes.GetString("ODataWriterCore_StreamNotDisposed");
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000B4F RID: 2895 RVA: 0x0001E0F0 File Offset: 0x0001C2F0
		internal static string ODataWriterCore_DeltaResourceWithoutIdOrKeyProperties
		{
			get
			{
				return TextRes.GetString("ODataWriterCore_DeltaResourceWithoutIdOrKeyProperties");
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000B50 RID: 2896 RVA: 0x0001E0FC File Offset: 0x0001C2FC
		internal static string ODataWriterCore_QueryCountInRequest
		{
			get
			{
				return TextRes.GetString("ODataWriterCore_QueryCountInRequest");
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000B51 RID: 2897 RVA: 0x0001E108 File Offset: 0x0001C308
		internal static string ODataWriterCore_QueryNextLinkInRequest
		{
			get
			{
				return TextRes.GetString("ODataWriterCore_QueryNextLinkInRequest");
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000B52 RID: 2898 RVA: 0x0001E114 File Offset: 0x0001C314
		internal static string ODataWriterCore_QueryDeltaLinkInRequest
		{
			get
			{
				return TextRes.GetString("ODataWriterCore_QueryDeltaLinkInRequest");
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000B53 RID: 2899 RVA: 0x0001E120 File Offset: 0x0001C320
		internal static string ODataWriterCore_CannotWriteDeltaWithResourceSetWriter
		{
			get
			{
				return TextRes.GetString("ODataWriterCore_CannotWriteDeltaWithResourceSetWriter");
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000B54 RID: 2900 RVA: 0x0001E12C File Offset: 0x0001C32C
		internal static string ODataWriterCore_NestedContentNotAllowedIn40DeletedEntry
		{
			get
			{
				return TextRes.GetString("ODataWriterCore_NestedContentNotAllowedIn40DeletedEntry");
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000B55 RID: 2901 RVA: 0x0001E138 File Offset: 0x0001C338
		internal static string ODataWriterCore_CannotWriteTopLevelResourceSetWithResourceWriter
		{
			get
			{
				return TextRes.GetString("ODataWriterCore_CannotWriteTopLevelResourceSetWithResourceWriter");
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000B56 RID: 2902 RVA: 0x0001E144 File Offset: 0x0001C344
		internal static string ODataWriterCore_CannotWriteTopLevelResourceWithResourceSetWriter
		{
			get
			{
				return TextRes.GetString("ODataWriterCore_CannotWriteTopLevelResourceWithResourceSetWriter");
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000B57 RID: 2903 RVA: 0x0001E150 File Offset: 0x0001C350
		internal static string ODataWriterCore_SyncCallOnAsyncWriter
		{
			get
			{
				return TextRes.GetString("ODataWriterCore_SyncCallOnAsyncWriter");
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000B58 RID: 2904 RVA: 0x0001E15C File Offset: 0x0001C35C
		internal static string ODataWriterCore_AsyncCallOnSyncWriter
		{
			get
			{
				return TextRes.GetString("ODataWriterCore_AsyncCallOnSyncWriter");
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000B59 RID: 2905 RVA: 0x0001E168 File Offset: 0x0001C368
		internal static string ODataWriterCore_EntityReferenceLinkWithoutNavigationLink
		{
			get
			{
				return TextRes.GetString("ODataWriterCore_EntityReferenceLinkWithoutNavigationLink");
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000B5A RID: 2906 RVA: 0x0001E174 File Offset: 0x0001C374
		internal static string ODataWriterCore_DeferredLinkInRequest
		{
			get
			{
				return TextRes.GetString("ODataWriterCore_DeferredLinkInRequest");
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000B5B RID: 2907 RVA: 0x0001E180 File Offset: 0x0001C380
		internal static string ODataWriterCore_MultipleItemsInNestedResourceInfoWithContent
		{
			get
			{
				return TextRes.GetString("ODataWriterCore_MultipleItemsInNestedResourceInfoWithContent");
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000B5C RID: 2908 RVA: 0x0001E18C File Offset: 0x0001C38C
		internal static string ODataWriterCore_DeltaLinkNotSupportedOnExpandedResourceSet
		{
			get
			{
				return TextRes.GetString("ODataWriterCore_DeltaLinkNotSupportedOnExpandedResourceSet");
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000B5D RID: 2909 RVA: 0x0001E198 File Offset: 0x0001C398
		internal static string ODataWriterCore_PathInODataUriMustBeSetWhenWritingContainedElement
		{
			get
			{
				return TextRes.GetString("ODataWriterCore_PathInODataUriMustBeSetWhenWritingContainedElement");
			}
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x0001E1A4 File Offset: 0x0001C3A4
		internal static string DuplicatePropertyNamesNotAllowed(object p0)
		{
			return TextRes.GetString("DuplicatePropertyNamesNotAllowed", new object[] { p0 });
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x0001E1BA File Offset: 0x0001C3BA
		internal static string DuplicateAnnotationNotAllowed(object p0)
		{
			return TextRes.GetString("DuplicateAnnotationNotAllowed", new object[] { p0 });
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x0001E1D0 File Offset: 0x0001C3D0
		internal static string DuplicateAnnotationForPropertyNotAllowed(object p0, object p1)
		{
			return TextRes.GetString("DuplicateAnnotationForPropertyNotAllowed", new object[] { p0, p1 });
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x0001E1EA File Offset: 0x0001C3EA
		internal static string DuplicateAnnotationForInstanceAnnotationNotAllowed(object p0, object p1)
		{
			return TextRes.GetString("DuplicateAnnotationForInstanceAnnotationNotAllowed", new object[] { p0, p1 });
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x0001E204 File Offset: 0x0001C404
		internal static string PropertyAnnotationAfterTheProperty(object p0, object p1)
		{
			return TextRes.GetString("PropertyAnnotationAfterTheProperty", new object[] { p0, p1 });
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x0001E21E File Offset: 0x0001C41E
		internal static string AtomValueUtils_CannotConvertValueToAtomPrimitive(object p0)
		{
			return TextRes.GetString("AtomValueUtils_CannotConvertValueToAtomPrimitive", new object[] { p0 });
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x0001E234 File Offset: 0x0001C434
		internal static string ODataJsonWriter_UnsupportedValueType(object p0)
		{
			return TextRes.GetString("ODataJsonWriter_UnsupportedValueType", new object[] { p0 });
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000B65 RID: 2917 RVA: 0x0001E24A File Offset: 0x0001C44A
		internal static string ODataJsonWriter_UnsupportedValueInCollection
		{
			get
			{
				return TextRes.GetString("ODataJsonWriter_UnsupportedValueInCollection");
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000B66 RID: 2918 RVA: 0x0001E256 File Offset: 0x0001C456
		internal static string ODataException_GeneralError
		{
			get
			{
				return TextRes.GetString("ODataException_GeneralError");
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000B67 RID: 2919 RVA: 0x0001E262 File Offset: 0x0001C462
		internal static string ODataErrorException_GeneralError
		{
			get
			{
				return TextRes.GetString("ODataErrorException_GeneralError");
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000B68 RID: 2920 RVA: 0x0001E26E File Offset: 0x0001C46E
		internal static string ODataUriParserException_GeneralError
		{
			get
			{
				return TextRes.GetString("ODataUriParserException_GeneralError");
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000B69 RID: 2921 RVA: 0x0001E27A File Offset: 0x0001C47A
		internal static string ODataMessageWriter_WriterAlreadyUsed
		{
			get
			{
				return TextRes.GetString("ODataMessageWriter_WriterAlreadyUsed");
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000B6A RID: 2922 RVA: 0x0001E286 File Offset: 0x0001C486
		internal static string ODataMessageWriter_EntityReferenceLinksInRequestNotAllowed
		{
			get
			{
				return TextRes.GetString("ODataMessageWriter_EntityReferenceLinksInRequestNotAllowed");
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000B6B RID: 2923 RVA: 0x0001E292 File Offset: 0x0001C492
		internal static string ODataMessageWriter_ErrorPayloadInRequest
		{
			get
			{
				return TextRes.GetString("ODataMessageWriter_ErrorPayloadInRequest");
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000B6C RID: 2924 RVA: 0x0001E29E File Offset: 0x0001C49E
		internal static string ODataMessageWriter_ServiceDocumentInRequest
		{
			get
			{
				return TextRes.GetString("ODataMessageWriter_ServiceDocumentInRequest");
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000B6D RID: 2925 RVA: 0x0001E2AA File Offset: 0x0001C4AA
		internal static string ODataMessageWriter_MetadataDocumentInRequest
		{
			get
			{
				return TextRes.GetString("ODataMessageWriter_MetadataDocumentInRequest");
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000B6E RID: 2926 RVA: 0x0001E2B6 File Offset: 0x0001C4B6
		internal static string ODataMessageWriter_DeltaInRequest
		{
			get
			{
				return TextRes.GetString("ODataMessageWriter_DeltaInRequest");
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000B6F RID: 2927 RVA: 0x0001E2C2 File Offset: 0x0001C4C2
		internal static string ODataMessageWriter_AsyncInRequest
		{
			get
			{
				return TextRes.GetString("ODataMessageWriter_AsyncInRequest");
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000B70 RID: 2928 RVA: 0x0001E2CE File Offset: 0x0001C4CE
		internal static string ODataMessageWriter_CannotWriteTopLevelNull
		{
			get
			{
				return TextRes.GetString("ODataMessageWriter_CannotWriteTopLevelNull");
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000B71 RID: 2929 RVA: 0x0001E2DA File Offset: 0x0001C4DA
		internal static string ODataMessageWriter_CannotWriteNullInRawFormat
		{
			get
			{
				return TextRes.GetString("ODataMessageWriter_CannotWriteNullInRawFormat");
			}
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x0001E2E6 File Offset: 0x0001C4E6
		internal static string ODataMessageWriter_CannotSetHeadersWithInvalidPayloadKind(object p0)
		{
			return TextRes.GetString("ODataMessageWriter_CannotSetHeadersWithInvalidPayloadKind", new object[] { p0 });
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x0001E2FC File Offset: 0x0001C4FC
		internal static string ODataMessageWriter_IncompatiblePayloadKinds(object p0, object p1)
		{
			return TextRes.GetString("ODataMessageWriter_IncompatiblePayloadKinds", new object[] { p0, p1 });
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x0001E316 File Offset: 0x0001C516
		internal static string ODataMessageWriter_CannotWriteStreamPropertyAsTopLevelProperty(object p0)
		{
			return TextRes.GetString("ODataMessageWriter_CannotWriteStreamPropertyAsTopLevelProperty", new object[] { p0 });
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000B75 RID: 2933 RVA: 0x0001E32C File Offset: 0x0001C52C
		internal static string ODataMessageWriter_WriteErrorAlreadyCalled
		{
			get
			{
				return TextRes.GetString("ODataMessageWriter_WriteErrorAlreadyCalled");
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000B76 RID: 2934 RVA: 0x0001E338 File Offset: 0x0001C538
		internal static string ODataMessageWriter_CannotWriteInStreamErrorForRawValues
		{
			get
			{
				return TextRes.GetString("ODataMessageWriter_CannotWriteInStreamErrorForRawValues");
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000B77 RID: 2935 RVA: 0x0001E344 File Offset: 0x0001C544
		internal static string ODataMessageWriter_CannotWriteMetadataWithoutModel
		{
			get
			{
				return TextRes.GetString("ODataMessageWriter_CannotWriteMetadataWithoutModel");
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000B78 RID: 2936 RVA: 0x0001E350 File Offset: 0x0001C550
		internal static string ODataMessageWriter_CannotSpecifyOperationWithoutModel
		{
			get
			{
				return TextRes.GetString("ODataMessageWriter_CannotSpecifyOperationWithoutModel");
			}
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x0001E35C File Offset: 0x0001C55C
		internal static string ODataMessageWriter_JsonPaddingOnInvalidContentType(object p0)
		{
			return TextRes.GetString("ODataMessageWriter_JsonPaddingOnInvalidContentType", new object[] { p0 });
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x0001E372 File Offset: 0x0001C572
		internal static string ODataMessageWriter_NonCollectionType(object p0)
		{
			return TextRes.GetString("ODataMessageWriter_NonCollectionType", new object[] { p0 });
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x0001E388 File Offset: 0x0001C588
		internal static string ODataMessageWriter_NotAllowedWriteTopLevelPropertyWithResourceValue(object p0)
		{
			return TextRes.GetString("ODataMessageWriter_NotAllowedWriteTopLevelPropertyWithResourceValue", new object[] { p0 });
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000B7C RID: 2940 RVA: 0x0001E39E File Offset: 0x0001C59E
		internal static string ODataMessageWriterSettings_MessageWriterSettingsXmlCustomizationCallbacksMustBeSpecifiedBoth
		{
			get
			{
				return TextRes.GetString("ODataMessageWriterSettings_MessageWriterSettingsXmlCustomizationCallbacksMustBeSpecifiedBoth");
			}
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x0001E3AA File Offset: 0x0001C5AA
		internal static string ODataCollectionWriterCore_InvalidTransitionFromStart(object p0, object p1)
		{
			return TextRes.GetString("ODataCollectionWriterCore_InvalidTransitionFromStart", new object[] { p0, p1 });
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x0001E3C4 File Offset: 0x0001C5C4
		internal static string ODataCollectionWriterCore_InvalidTransitionFromCollection(object p0, object p1)
		{
			return TextRes.GetString("ODataCollectionWriterCore_InvalidTransitionFromCollection", new object[] { p0, p1 });
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x0001E3DE File Offset: 0x0001C5DE
		internal static string ODataCollectionWriterCore_InvalidTransitionFromItem(object p0, object p1)
		{
			return TextRes.GetString("ODataCollectionWriterCore_InvalidTransitionFromItem", new object[] { p0, p1 });
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x0001E3F8 File Offset: 0x0001C5F8
		internal static string ODataCollectionWriterCore_WriteEndCalledInInvalidState(object p0)
		{
			return TextRes.GetString("ODataCollectionWriterCore_WriteEndCalledInInvalidState", new object[] { p0 });
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000B81 RID: 2945 RVA: 0x0001E40E File Offset: 0x0001C60E
		internal static string ODataCollectionWriterCore_SyncCallOnAsyncWriter
		{
			get
			{
				return TextRes.GetString("ODataCollectionWriterCore_SyncCallOnAsyncWriter");
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000B82 RID: 2946 RVA: 0x0001E41A File Offset: 0x0001C61A
		internal static string ODataCollectionWriterCore_AsyncCallOnSyncWriter
		{
			get
			{
				return TextRes.GetString("ODataCollectionWriterCore_AsyncCallOnSyncWriter");
			}
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x0001E426 File Offset: 0x0001C626
		internal static string ODataBatch_InvalidHttpMethodForChangeSetRequest(object p0)
		{
			return TextRes.GetString("ODataBatch_InvalidHttpMethodForChangeSetRequest", new object[] { p0 });
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x0001E43C File Offset: 0x0001C63C
		internal static string ODataBatchOperationHeaderDictionary_KeyNotFound(object p0)
		{
			return TextRes.GetString("ODataBatchOperationHeaderDictionary_KeyNotFound", new object[] { p0 });
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x0001E452 File Offset: 0x0001C652
		internal static string ODataBatchOperationHeaderDictionary_DuplicateCaseInsensitiveKeys(object p0)
		{
			return TextRes.GetString("ODataBatchOperationHeaderDictionary_DuplicateCaseInsensitiveKeys", new object[] { p0 });
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000B86 RID: 2950 RVA: 0x0001E468 File Offset: 0x0001C668
		internal static string ODataParameterWriter_InStreamErrorNotSupported
		{
			get
			{
				return TextRes.GetString("ODataParameterWriter_InStreamErrorNotSupported");
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000B87 RID: 2951 RVA: 0x0001E474 File Offset: 0x0001C674
		internal static string ODataParameterWriter_CannotCreateParameterWriterOnResponseMessage
		{
			get
			{
				return TextRes.GetString("ODataParameterWriter_CannotCreateParameterWriterOnResponseMessage");
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000B88 RID: 2952 RVA: 0x0001E480 File Offset: 0x0001C680
		internal static string ODataParameterWriterCore_SyncCallOnAsyncWriter
		{
			get
			{
				return TextRes.GetString("ODataParameterWriterCore_SyncCallOnAsyncWriter");
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000B89 RID: 2953 RVA: 0x0001E48C File Offset: 0x0001C68C
		internal static string ODataParameterWriterCore_AsyncCallOnSyncWriter
		{
			get
			{
				return TextRes.GetString("ODataParameterWriterCore_AsyncCallOnSyncWriter");
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000B8A RID: 2954 RVA: 0x0001E498 File Offset: 0x0001C698
		internal static string ODataParameterWriterCore_CannotWriteStart
		{
			get
			{
				return TextRes.GetString("ODataParameterWriterCore_CannotWriteStart");
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000B8B RID: 2955 RVA: 0x0001E4A4 File Offset: 0x0001C6A4
		internal static string ODataParameterWriterCore_CannotWriteParameter
		{
			get
			{
				return TextRes.GetString("ODataParameterWriterCore_CannotWriteParameter");
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000B8C RID: 2956 RVA: 0x0001E4B0 File Offset: 0x0001C6B0
		internal static string ODataParameterWriterCore_CannotWriteEnd
		{
			get
			{
				return TextRes.GetString("ODataParameterWriterCore_CannotWriteEnd");
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000B8D RID: 2957 RVA: 0x0001E4BC File Offset: 0x0001C6BC
		internal static string ODataParameterWriterCore_CannotWriteInErrorOrCompletedState
		{
			get
			{
				return TextRes.GetString("ODataParameterWriterCore_CannotWriteInErrorOrCompletedState");
			}
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x0001E4C8 File Offset: 0x0001C6C8
		internal static string ODataParameterWriterCore_DuplicatedParameterNameNotAllowed(object p0)
		{
			return TextRes.GetString("ODataParameterWriterCore_DuplicatedParameterNameNotAllowed", new object[] { p0 });
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x0001E4DE File Offset: 0x0001C6DE
		internal static string ODataParameterWriterCore_CannotWriteValueOnNonValueTypeKind(object p0, object p1)
		{
			return TextRes.GetString("ODataParameterWriterCore_CannotWriteValueOnNonValueTypeKind", new object[] { p0, p1 });
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x0001E4F8 File Offset: 0x0001C6F8
		internal static string ODataParameterWriterCore_CannotWriteValueOnNonSupportedValueType(object p0, object p1)
		{
			return TextRes.GetString("ODataParameterWriterCore_CannotWriteValueOnNonSupportedValueType", new object[] { p0, p1 });
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x0001E512 File Offset: 0x0001C712
		internal static string ODataParameterWriterCore_CannotCreateCollectionWriterOnNonCollectionTypeKind(object p0, object p1)
		{
			return TextRes.GetString("ODataParameterWriterCore_CannotCreateCollectionWriterOnNonCollectionTypeKind", new object[] { p0, p1 });
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x0001E52C File Offset: 0x0001C72C
		internal static string ODataParameterWriterCore_CannotCreateResourceWriterOnNonEntityOrComplexTypeKind(object p0, object p1)
		{
			return TextRes.GetString("ODataParameterWriterCore_CannotCreateResourceWriterOnNonEntityOrComplexTypeKind", new object[] { p0, p1 });
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x0001E546 File Offset: 0x0001C746
		internal static string ODataParameterWriterCore_CannotCreateResourceSetWriterOnNonStructuredCollectionTypeKind(object p0, object p1)
		{
			return TextRes.GetString("ODataParameterWriterCore_CannotCreateResourceSetWriterOnNonStructuredCollectionTypeKind", new object[] { p0, p1 });
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x0001E560 File Offset: 0x0001C760
		internal static string ODataParameterWriterCore_ParameterNameNotFoundInOperation(object p0, object p1)
		{
			return TextRes.GetString("ODataParameterWriterCore_ParameterNameNotFoundInOperation", new object[] { p0, p1 });
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x0001E57A File Offset: 0x0001C77A
		internal static string ODataParameterWriterCore_MissingParameterInParameterPayload(object p0, object p1)
		{
			return TextRes.GetString("ODataParameterWriterCore_MissingParameterInParameterPayload", new object[] { p0, p1 });
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000B96 RID: 2966 RVA: 0x0001E594 File Offset: 0x0001C794
		internal static string ODataBatchWriter_FlushOrFlushAsyncCalledInStreamRequestedState
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_FlushOrFlushAsyncCalledInStreamRequestedState");
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000B97 RID: 2967 RVA: 0x0001E5A0 File Offset: 0x0001C7A0
		internal static string ODataBatchWriter_CannotCompleteBatchWithActiveChangeSet
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_CannotCompleteBatchWithActiveChangeSet");
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000B98 RID: 2968 RVA: 0x0001E5AC File Offset: 0x0001C7AC
		internal static string ODataBatchWriter_CannotStartChangeSetWithActiveChangeSet
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_CannotStartChangeSetWithActiveChangeSet");
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000B99 RID: 2969 RVA: 0x0001E5B8 File Offset: 0x0001C7B8
		internal static string ODataBatchWriter_CannotCompleteChangeSetWithoutActiveChangeSet
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_CannotCompleteChangeSetWithoutActiveChangeSet");
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000B9A RID: 2970 RVA: 0x0001E5C4 File Offset: 0x0001C7C4
		internal static string ODataBatchWriter_InvalidTransitionFromStart
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_InvalidTransitionFromStart");
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000B9B RID: 2971 RVA: 0x0001E5D0 File Offset: 0x0001C7D0
		internal static string ODataBatchWriter_InvalidTransitionFromBatchStarted
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_InvalidTransitionFromBatchStarted");
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000B9C RID: 2972 RVA: 0x0001E5DC File Offset: 0x0001C7DC
		internal static string ODataBatchWriter_InvalidTransitionFromChangeSetStarted
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_InvalidTransitionFromChangeSetStarted");
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000B9D RID: 2973 RVA: 0x0001E5E8 File Offset: 0x0001C7E8
		internal static string ODataBatchWriter_InvalidTransitionFromOperationCreated
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_InvalidTransitionFromOperationCreated");
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000B9E RID: 2974 RVA: 0x0001E5F4 File Offset: 0x0001C7F4
		internal static string ODataBatchWriter_InvalidTransitionFromOperationContentStreamRequested
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_InvalidTransitionFromOperationContentStreamRequested");
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000B9F RID: 2975 RVA: 0x0001E600 File Offset: 0x0001C800
		internal static string ODataBatchWriter_InvalidTransitionFromOperationContentStreamDisposed
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_InvalidTransitionFromOperationContentStreamDisposed");
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000BA0 RID: 2976 RVA: 0x0001E60C File Offset: 0x0001C80C
		internal static string ODataBatchWriter_InvalidTransitionFromChangeSetCompleted
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_InvalidTransitionFromChangeSetCompleted");
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000BA1 RID: 2977 RVA: 0x0001E618 File Offset: 0x0001C818
		internal static string ODataBatchWriter_InvalidTransitionFromBatchCompleted
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_InvalidTransitionFromBatchCompleted");
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000BA2 RID: 2978 RVA: 0x0001E624 File Offset: 0x0001C824
		internal static string ODataBatchWriter_CannotCreateRequestOperationWhenWritingResponse
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_CannotCreateRequestOperationWhenWritingResponse");
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000BA3 RID: 2979 RVA: 0x0001E630 File Offset: 0x0001C830
		internal static string ODataBatchWriter_CannotCreateResponseOperationWhenWritingRequest
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_CannotCreateResponseOperationWhenWritingRequest");
			}
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x0001E63C File Offset: 0x0001C83C
		internal static string ODataBatchWriter_MaxBatchSizeExceeded(object p0)
		{
			return TextRes.GetString("ODataBatchWriter_MaxBatchSizeExceeded", new object[] { p0 });
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x0001E652 File Offset: 0x0001C852
		internal static string ODataBatchWriter_MaxChangeSetSizeExceeded(object p0)
		{
			return TextRes.GetString("ODataBatchWriter_MaxChangeSetSizeExceeded", new object[] { p0 });
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000BA6 RID: 2982 RVA: 0x0001E668 File Offset: 0x0001C868
		internal static string ODataBatchWriter_SyncCallOnAsyncWriter
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_SyncCallOnAsyncWriter");
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000BA7 RID: 2983 RVA: 0x0001E674 File Offset: 0x0001C874
		internal static string ODataBatchWriter_AsyncCallOnSyncWriter
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_AsyncCallOnSyncWriter");
			}
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x0001E680 File Offset: 0x0001C880
		internal static string ODataBatchWriter_DuplicateContentIDsNotAllowed(object p0)
		{
			return TextRes.GetString("ODataBatchWriter_DuplicateContentIDsNotAllowed", new object[] { p0 });
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000BA9 RID: 2985 RVA: 0x0001E696 File Offset: 0x0001C896
		internal static string ODataBatchWriter_CannotWriteInStreamErrorForBatch
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_CannotWriteInStreamErrorForBatch");
			}
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x0001E6A2 File Offset: 0x0001C8A2
		internal static string ODataBatchUtils_RelativeUriUsedWithoutBaseUriSpecified(object p0)
		{
			return TextRes.GetString("ODataBatchUtils_RelativeUriUsedWithoutBaseUriSpecified", new object[] { p0 });
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x0001E6B8 File Offset: 0x0001C8B8
		internal static string ODataBatchUtils_RelativeUriStartingWithDollarUsedWithoutBaseUriSpecified(object p0)
		{
			return TextRes.GetString("ODataBatchUtils_RelativeUriStartingWithDollarUsedWithoutBaseUriSpecified", new object[] { p0 });
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000BAC RID: 2988 RVA: 0x0001E6CE File Offset: 0x0001C8CE
		internal static string ODataBatchOperationMessage_VerifyNotCompleted
		{
			get
			{
				return TextRes.GetString("ODataBatchOperationMessage_VerifyNotCompleted");
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000BAD RID: 2989 RVA: 0x0001E6DA File Offset: 0x0001C8DA
		internal static string ODataBatchOperationStream_Disposed
		{
			get
			{
				return TextRes.GetString("ODataBatchOperationStream_Disposed");
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000BAE RID: 2990 RVA: 0x0001E6E6 File Offset: 0x0001C8E6
		internal static string ODataBatchReader_CannotCreateRequestOperationWhenReadingResponse
		{
			get
			{
				return TextRes.GetString("ODataBatchReader_CannotCreateRequestOperationWhenReadingResponse");
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000BAF RID: 2991 RVA: 0x0001E6F2 File Offset: 0x0001C8F2
		internal static string ODataBatchReader_CannotCreateResponseOperationWhenReadingRequest
		{
			get
			{
				return TextRes.GetString("ODataBatchReader_CannotCreateResponseOperationWhenReadingRequest");
			}
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x0001E6FE File Offset: 0x0001C8FE
		internal static string ODataBatchReader_InvalidStateForCreateOperationRequestMessage(object p0)
		{
			return TextRes.GetString("ODataBatchReader_InvalidStateForCreateOperationRequestMessage", new object[] { p0 });
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000BB1 RID: 2993 RVA: 0x0001E714 File Offset: 0x0001C914
		internal static string ODataBatchReader_OperationRequestMessageAlreadyCreated
		{
			get
			{
				return TextRes.GetString("ODataBatchReader_OperationRequestMessageAlreadyCreated");
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000BB2 RID: 2994 RVA: 0x0001E720 File Offset: 0x0001C920
		internal static string ODataBatchReader_OperationResponseMessageAlreadyCreated
		{
			get
			{
				return TextRes.GetString("ODataBatchReader_OperationResponseMessageAlreadyCreated");
			}
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x0001E72C File Offset: 0x0001C92C
		internal static string ODataBatchReader_InvalidStateForCreateOperationResponseMessage(object p0)
		{
			return TextRes.GetString("ODataBatchReader_InvalidStateForCreateOperationResponseMessage", new object[] { p0 });
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000BB4 RID: 2996 RVA: 0x0001E742 File Offset: 0x0001C942
		internal static string ODataBatchReader_CannotUseReaderWhileOperationStreamActive
		{
			get
			{
				return TextRes.GetString("ODataBatchReader_CannotUseReaderWhileOperationStreamActive");
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000BB5 RID: 2997 RVA: 0x0001E74E File Offset: 0x0001C94E
		internal static string ODataBatchReader_SyncCallOnAsyncReader
		{
			get
			{
				return TextRes.GetString("ODataBatchReader_SyncCallOnAsyncReader");
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000BB6 RID: 2998 RVA: 0x0001E75A File Offset: 0x0001C95A
		internal static string ODataBatchReader_AsyncCallOnSyncReader
		{
			get
			{
				return TextRes.GetString("ODataBatchReader_AsyncCallOnSyncReader");
			}
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x0001E766 File Offset: 0x0001C966
		internal static string ODataBatchReader_ReadOrReadAsyncCalledInInvalidState(object p0)
		{
			return TextRes.GetString("ODataBatchReader_ReadOrReadAsyncCalledInInvalidState", new object[] { p0 });
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x0001E77C File Offset: 0x0001C97C
		internal static string ODataBatchReader_MaxBatchSizeExceeded(object p0)
		{
			return TextRes.GetString("ODataBatchReader_MaxBatchSizeExceeded", new object[] { p0 });
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x0001E792 File Offset: 0x0001C992
		internal static string ODataBatchReader_MaxChangeSetSizeExceeded(object p0)
		{
			return TextRes.GetString("ODataBatchReader_MaxChangeSetSizeExceeded", new object[] { p0 });
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000BBA RID: 3002 RVA: 0x0001E7A8 File Offset: 0x0001C9A8
		internal static string ODataBatchReader_NoMessageWasCreatedForOperation
		{
			get
			{
				return TextRes.GetString("ODataBatchReader_NoMessageWasCreatedForOperation");
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000BBB RID: 3003 RVA: 0x0001E7B4 File Offset: 0x0001C9B4
		internal static string ODataBatchReader_ReaderModeNotInitilized
		{
			get
			{
				return TextRes.GetString("ODataBatchReader_ReaderModeNotInitilized");
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000BBC RID: 3004 RVA: 0x0001E7C0 File Offset: 0x0001C9C0
		internal static string ODataBatchReader_JsonBatchTopLevelPropertyMissing
		{
			get
			{
				return TextRes.GetString("ODataBatchReader_JsonBatchTopLevelPropertyMissing");
			}
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x0001E7CC File Offset: 0x0001C9CC
		internal static string ODataBatchReader_DuplicateContentIDsNotAllowed(object p0)
		{
			return TextRes.GetString("ODataBatchReader_DuplicateContentIDsNotAllowed", new object[] { p0 });
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x0001E7E2 File Offset: 0x0001C9E2
		internal static string ODataBatchReader_DuplicateAtomicityGroupIDsNotAllowed(object p0)
		{
			return TextRes.GetString("ODataBatchReader_DuplicateAtomicityGroupIDsNotAllowed", new object[] { p0 });
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x0001E7F8 File Offset: 0x0001C9F8
		internal static string ODataBatchReader_RequestPropertyMissing(object p0)
		{
			return TextRes.GetString("ODataBatchReader_RequestPropertyMissing", new object[] { p0 });
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x0001E80E File Offset: 0x0001CA0E
		internal static string ODataBatchReader_SameRequestIdAsAtomicityGroupIdNotAllowed(object p0, object p1)
		{
			return TextRes.GetString("ODataBatchReader_SameRequestIdAsAtomicityGroupIdNotAllowed", new object[] { p0, p1 });
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x0001E828 File Offset: 0x0001CA28
		internal static string ODataBatchReader_SelfReferenceDependsOnRequestIdNotAllowed(object p0, object p1)
		{
			return TextRes.GetString("ODataBatchReader_SelfReferenceDependsOnRequestIdNotAllowed", new object[] { p0, p1 });
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x0001E842 File Offset: 0x0001CA42
		internal static string ODataBatchReader_DependsOnRequestIdIsPartOfAtomicityGroupNotAllowed(object p0, object p1)
		{
			return TextRes.GetString("ODataBatchReader_DependsOnRequestIdIsPartOfAtomicityGroupNotAllowed", new object[] { p0, p1 });
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x0001E85C File Offset: 0x0001CA5C
		internal static string ODataBatchReader_DependsOnIdNotFound(object p0, object p1)
		{
			return TextRes.GetString("ODataBatchReader_DependsOnIdNotFound", new object[] { p0, p1 });
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x0001E876 File Offset: 0x0001CA76
		internal static string ODataBatchReader_AbsoluteURINotMatchingBaseUri(object p0, object p1)
		{
			return TextRes.GetString("ODataBatchReader_AbsoluteURINotMatchingBaseUri", new object[] { p0, p1 });
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x0001E890 File Offset: 0x0001CA90
		internal static string ODataBatchReader_ReferenceIdNotIncludedInDependsOn(object p0, object p1, object p2)
		{
			return TextRes.GetString("ODataBatchReader_ReferenceIdNotIncludedInDependsOn", new object[] { p0, p1, p2 });
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000BC6 RID: 3014 RVA: 0x0001E8AE File Offset: 0x0001CAAE
		internal static string ODataBatch_GroupIdOrChangeSetIdCannotBeNull
		{
			get
			{
				return TextRes.GetString("ODataBatch_GroupIdOrChangeSetIdCannotBeNull");
			}
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x0001E8BA File Offset: 0x0001CABA
		internal static string ODataBatchReader_MessageIdPositionedIncorrectly(object p0, object p1)
		{
			return TextRes.GetString("ODataBatchReader_MessageIdPositionedIncorrectly", new object[] { p0, p1 });
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000BC8 RID: 3016 RVA: 0x0001E8D4 File Offset: 0x0001CAD4
		internal static string ODataBatchReader_ReaderStreamChangesetBoundaryCannotBeNull
		{
			get
			{
				return TextRes.GetString("ODataBatchReader_ReaderStreamChangesetBoundaryCannotBeNull");
			}
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x0001E8E0 File Offset: 0x0001CAE0
		internal static string ODataBatchReaderStream_InvalidHeaderSpecified(object p0)
		{
			return TextRes.GetString("ODataBatchReaderStream_InvalidHeaderSpecified", new object[] { p0 });
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x0001E8F6 File Offset: 0x0001CAF6
		internal static string ODataBatchReaderStream_InvalidRequestLine(object p0)
		{
			return TextRes.GetString("ODataBatchReaderStream_InvalidRequestLine", new object[] { p0 });
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x0001E90C File Offset: 0x0001CB0C
		internal static string ODataBatchReaderStream_InvalidResponseLine(object p0)
		{
			return TextRes.GetString("ODataBatchReaderStream_InvalidResponseLine", new object[] { p0 });
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x0001E922 File Offset: 0x0001CB22
		internal static string ODataBatchReaderStream_InvalidHttpVersionSpecified(object p0, object p1)
		{
			return TextRes.GetString("ODataBatchReaderStream_InvalidHttpVersionSpecified", new object[] { p0, p1 });
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x0001E93C File Offset: 0x0001CB3C
		internal static string ODataBatchReaderStream_NonIntegerHttpStatusCode(object p0)
		{
			return TextRes.GetString("ODataBatchReaderStream_NonIntegerHttpStatusCode", new object[] { p0 });
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000BCE RID: 3022 RVA: 0x0001E952 File Offset: 0x0001CB52
		internal static string ODataBatchReaderStream_MissingContentTypeHeader
		{
			get
			{
				return TextRes.GetString("ODataBatchReaderStream_MissingContentTypeHeader");
			}
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x0001E95E File Offset: 0x0001CB5E
		internal static string ODataBatchReaderStream_MissingOrInvalidContentEncodingHeader(object p0, object p1)
		{
			return TextRes.GetString("ODataBatchReaderStream_MissingOrInvalidContentEncodingHeader", new object[] { p0, p1 });
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x0001E978 File Offset: 0x0001CB78
		internal static string ODataBatchReaderStream_InvalidContentTypeSpecified(object p0, object p1, object p2, object p3)
		{
			return TextRes.GetString("ODataBatchReaderStream_InvalidContentTypeSpecified", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x0001E99A File Offset: 0x0001CB9A
		internal static string ODataBatchReaderStream_InvalidContentLengthSpecified(object p0)
		{
			return TextRes.GetString("ODataBatchReaderStream_InvalidContentLengthSpecified", new object[] { p0 });
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x0001E9B0 File Offset: 0x0001CBB0
		internal static string ODataBatchReaderStream_DuplicateHeaderFound(object p0)
		{
			return TextRes.GetString("ODataBatchReaderStream_DuplicateHeaderFound", new object[] { p0 });
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000BD3 RID: 3027 RVA: 0x0001E9C6 File Offset: 0x0001CBC6
		internal static string ODataBatchReaderStream_NestedChangesetsAreNotSupported
		{
			get
			{
				return TextRes.GetString("ODataBatchReaderStream_NestedChangesetsAreNotSupported");
			}
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x0001E9D2 File Offset: 0x0001CBD2
		internal static string ODataBatchReaderStream_MultiByteEncodingsNotSupported(object p0)
		{
			return TextRes.GetString("ODataBatchReaderStream_MultiByteEncodingsNotSupported", new object[] { p0 });
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000BD5 RID: 3029 RVA: 0x0001E9E8 File Offset: 0x0001CBE8
		internal static string ODataBatchReaderStream_UnexpectedEndOfInput
		{
			get
			{
				return TextRes.GetString("ODataBatchReaderStream_UnexpectedEndOfInput");
			}
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x0001E9F4 File Offset: 0x0001CBF4
		internal static string ODataBatchReaderStreamBuffer_BoundaryLineSecurityLimitReached(object p0)
		{
			return TextRes.GetString("ODataBatchReaderStreamBuffer_BoundaryLineSecurityLimitReached", new object[] { p0 });
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000BD7 RID: 3031 RVA: 0x0001EA0A File Offset: 0x0001CC0A
		internal static string ODataAsyncWriter_CannotCreateResponseWhenNotWritingResponse
		{
			get
			{
				return TextRes.GetString("ODataAsyncWriter_CannotCreateResponseWhenNotWritingResponse");
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000BD8 RID: 3032 RVA: 0x0001EA16 File Offset: 0x0001CC16
		internal static string ODataAsyncWriter_CannotCreateResponseMoreThanOnce
		{
			get
			{
				return TextRes.GetString("ODataAsyncWriter_CannotCreateResponseMoreThanOnce");
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000BD9 RID: 3033 RVA: 0x0001EA22 File Offset: 0x0001CC22
		internal static string ODataAsyncWriter_SyncCallOnAsyncWriter
		{
			get
			{
				return TextRes.GetString("ODataAsyncWriter_SyncCallOnAsyncWriter");
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000BDA RID: 3034 RVA: 0x0001EA2E File Offset: 0x0001CC2E
		internal static string ODataAsyncWriter_AsyncCallOnSyncWriter
		{
			get
			{
				return TextRes.GetString("ODataAsyncWriter_AsyncCallOnSyncWriter");
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000BDB RID: 3035 RVA: 0x0001EA3A File Offset: 0x0001CC3A
		internal static string ODataAsyncWriter_CannotWriteInStreamErrorForAsync
		{
			get
			{
				return TextRes.GetString("ODataAsyncWriter_CannotWriteInStreamErrorForAsync");
			}
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x0001EA46 File Offset: 0x0001CC46
		internal static string ODataAsyncReader_InvalidHeaderSpecified(object p0)
		{
			return TextRes.GetString("ODataAsyncReader_InvalidHeaderSpecified", new object[] { p0 });
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000BDD RID: 3037 RVA: 0x0001EA5C File Offset: 0x0001CC5C
		internal static string ODataAsyncReader_CannotCreateResponseWhenNotReadingResponse
		{
			get
			{
				return TextRes.GetString("ODataAsyncReader_CannotCreateResponseWhenNotReadingResponse");
			}
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x0001EA68 File Offset: 0x0001CC68
		internal static string ODataAsyncReader_InvalidResponseLine(object p0)
		{
			return TextRes.GetString("ODataAsyncReader_InvalidResponseLine", new object[] { p0 });
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x0001EA7E File Offset: 0x0001CC7E
		internal static string ODataAsyncReader_InvalidHttpVersionSpecified(object p0, object p1)
		{
			return TextRes.GetString("ODataAsyncReader_InvalidHttpVersionSpecified", new object[] { p0, p1 });
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x0001EA98 File Offset: 0x0001CC98
		internal static string ODataAsyncReader_NonIntegerHttpStatusCode(object p0)
		{
			return TextRes.GetString("ODataAsyncReader_NonIntegerHttpStatusCode", new object[] { p0 });
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x0001EAAE File Offset: 0x0001CCAE
		internal static string ODataAsyncReader_DuplicateHeaderFound(object p0)
		{
			return TextRes.GetString("ODataAsyncReader_DuplicateHeaderFound", new object[] { p0 });
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x0001EAC4 File Offset: 0x0001CCC4
		internal static string ODataAsyncReader_MultiByteEncodingsNotSupported(object p0)
		{
			return TextRes.GetString("ODataAsyncReader_MultiByteEncodingsNotSupported", new object[] { p0 });
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x0001EADA File Offset: 0x0001CCDA
		internal static string ODataAsyncReader_InvalidNewLineEncountered(object p0)
		{
			return TextRes.GetString("ODataAsyncReader_InvalidNewLineEncountered", new object[] { p0 });
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000BE4 RID: 3044 RVA: 0x0001EAF0 File Offset: 0x0001CCF0
		internal static string ODataAsyncReader_UnexpectedEndOfInput
		{
			get
			{
				return TextRes.GetString("ODataAsyncReader_UnexpectedEndOfInput");
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000BE5 RID: 3045 RVA: 0x0001EAFC File Offset: 0x0001CCFC
		internal static string ODataAsyncReader_SyncCallOnAsyncReader
		{
			get
			{
				return TextRes.GetString("ODataAsyncReader_SyncCallOnAsyncReader");
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000BE6 RID: 3046 RVA: 0x0001EB08 File Offset: 0x0001CD08
		internal static string ODataAsyncReader_AsyncCallOnSyncReader
		{
			get
			{
				return TextRes.GetString("ODataAsyncReader_AsyncCallOnSyncReader");
			}
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x0001EB14 File Offset: 0x0001CD14
		internal static string HttpUtils_MediaTypeUnspecified(object p0)
		{
			return TextRes.GetString("HttpUtils_MediaTypeUnspecified", new object[] { p0 });
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x0001EB2A File Offset: 0x0001CD2A
		internal static string HttpUtils_MediaTypeRequiresSlash(object p0)
		{
			return TextRes.GetString("HttpUtils_MediaTypeRequiresSlash", new object[] { p0 });
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x0001EB40 File Offset: 0x0001CD40
		internal static string HttpUtils_MediaTypeRequiresSubType(object p0)
		{
			return TextRes.GetString("HttpUtils_MediaTypeRequiresSubType", new object[] { p0 });
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x0001EB56 File Offset: 0x0001CD56
		internal static string HttpUtils_MediaTypeMissingParameterValue(object p0)
		{
			return TextRes.GetString("HttpUtils_MediaTypeMissingParameterValue", new object[] { p0 });
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000BEB RID: 3051 RVA: 0x0001EB6C File Offset: 0x0001CD6C
		internal static string HttpUtils_MediaTypeMissingParameterName
		{
			get
			{
				return TextRes.GetString("HttpUtils_MediaTypeMissingParameterName");
			}
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x0001EB78 File Offset: 0x0001CD78
		internal static string HttpUtils_EscapeCharWithoutQuotes(object p0, object p1, object p2, object p3)
		{
			return TextRes.GetString("HttpUtils_EscapeCharWithoutQuotes", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x0001EB9A File Offset: 0x0001CD9A
		internal static string HttpUtils_EscapeCharAtEnd(object p0, object p1, object p2, object p3)
		{
			return TextRes.GetString("HttpUtils_EscapeCharAtEnd", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x0001EBBC File Offset: 0x0001CDBC
		internal static string HttpUtils_ClosingQuoteNotFound(object p0, object p1, object p2)
		{
			return TextRes.GetString("HttpUtils_ClosingQuoteNotFound", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x0001EBDA File Offset: 0x0001CDDA
		internal static string HttpUtils_InvalidCharacterInQuotedParameterValue(object p0, object p1, object p2, object p3)
		{
			return TextRes.GetString("HttpUtils_InvalidCharacterInQuotedParameterValue", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000BF0 RID: 3056 RVA: 0x0001EBFC File Offset: 0x0001CDFC
		internal static string HttpUtils_ContentTypeMissing
		{
			get
			{
				return TextRes.GetString("HttpUtils_ContentTypeMissing");
			}
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x0001EC08 File Offset: 0x0001CE08
		internal static string HttpUtils_MediaTypeRequiresSemicolonBeforeParameter(object p0)
		{
			return TextRes.GetString("HttpUtils_MediaTypeRequiresSemicolonBeforeParameter", new object[] { p0 });
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x0001EC1E File Offset: 0x0001CE1E
		internal static string HttpUtils_InvalidQualityValueStartChar(object p0, object p1)
		{
			return TextRes.GetString("HttpUtils_InvalidQualityValueStartChar", new object[] { p0, p1 });
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x0001EC38 File Offset: 0x0001CE38
		internal static string HttpUtils_InvalidQualityValue(object p0, object p1)
		{
			return TextRes.GetString("HttpUtils_InvalidQualityValue", new object[] { p0, p1 });
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x0001EC52 File Offset: 0x0001CE52
		internal static string HttpUtils_CannotConvertCharToInt(object p0)
		{
			return TextRes.GetString("HttpUtils_CannotConvertCharToInt", new object[] { p0 });
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x0001EC68 File Offset: 0x0001CE68
		internal static string HttpUtils_MissingSeparatorBetweenCharsets(object p0)
		{
			return TextRes.GetString("HttpUtils_MissingSeparatorBetweenCharsets", new object[] { p0 });
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x0001EC7E File Offset: 0x0001CE7E
		internal static string HttpUtils_InvalidSeparatorBetweenCharsets(object p0)
		{
			return TextRes.GetString("HttpUtils_InvalidSeparatorBetweenCharsets", new object[] { p0 });
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x0001EC94 File Offset: 0x0001CE94
		internal static string HttpUtils_InvalidCharsetName(object p0)
		{
			return TextRes.GetString("HttpUtils_InvalidCharsetName", new object[] { p0 });
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x0001ECAA File Offset: 0x0001CEAA
		internal static string HttpUtils_UnexpectedEndOfQValue(object p0)
		{
			return TextRes.GetString("HttpUtils_UnexpectedEndOfQValue", new object[] { p0 });
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x0001ECC0 File Offset: 0x0001CEC0
		internal static string HttpUtils_ExpectedLiteralNotFoundInString(object p0, object p1, object p2)
		{
			return TextRes.GetString("HttpUtils_ExpectedLiteralNotFoundInString", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x0001ECDE File Offset: 0x0001CEDE
		internal static string HttpUtils_InvalidHttpMethodString(object p0)
		{
			return TextRes.GetString("HttpUtils_InvalidHttpMethodString", new object[] { p0 });
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x0001ECF4 File Offset: 0x0001CEF4
		internal static string HttpUtils_NoOrMoreThanOneContentTypeSpecified(object p0)
		{
			return TextRes.GetString("HttpUtils_NoOrMoreThanOneContentTypeSpecified", new object[] { p0 });
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x0001ED0A File Offset: 0x0001CF0A
		internal static string HttpHeaderValueLexer_UnrecognizedSeparator(object p0, object p1, object p2, object p3)
		{
			return TextRes.GetString("HttpHeaderValueLexer_UnrecognizedSeparator", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x0001ED2C File Offset: 0x0001CF2C
		internal static string HttpHeaderValueLexer_TokenExpectedButFoundQuotedString(object p0, object p1, object p2)
		{
			return TextRes.GetString("HttpHeaderValueLexer_TokenExpectedButFoundQuotedString", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x0001ED4A File Offset: 0x0001CF4A
		internal static string HttpHeaderValueLexer_FailedToReadTokenOrQuotedString(object p0, object p1, object p2)
		{
			return TextRes.GetString("HttpHeaderValueLexer_FailedToReadTokenOrQuotedString", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x0001ED68 File Offset: 0x0001CF68
		internal static string HttpHeaderValueLexer_InvalidSeparatorAfterQuotedString(object p0, object p1, object p2, object p3)
		{
			return TextRes.GetString("HttpHeaderValueLexer_InvalidSeparatorAfterQuotedString", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x0001ED8A File Offset: 0x0001CF8A
		internal static string HttpHeaderValueLexer_EndOfFileAfterSeparator(object p0, object p1, object p2, object p3)
		{
			return TextRes.GetString("HttpHeaderValueLexer_EndOfFileAfterSeparator", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x0001EDAC File Offset: 0x0001CFAC
		internal static string MediaType_EncodingNotSupported(object p0)
		{
			return TextRes.GetString("MediaType_EncodingNotSupported", new object[] { p0 });
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x0001EDC2 File Offset: 0x0001CFC2
		internal static string MediaTypeUtils_DidNotFindMatchingMediaType(object p0, object p1)
		{
			return TextRes.GetString("MediaTypeUtils_DidNotFindMatchingMediaType", new object[] { p0, p1 });
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x0001EDDC File Offset: 0x0001CFDC
		internal static string MediaTypeUtils_CannotDetermineFormatFromContentType(object p0, object p1)
		{
			return TextRes.GetString("MediaTypeUtils_CannotDetermineFormatFromContentType", new object[] { p0, p1 });
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x0001EDF6 File Offset: 0x0001CFF6
		internal static string MediaTypeUtils_NoOrMoreThanOneContentTypeSpecified(object p0)
		{
			return TextRes.GetString("MediaTypeUtils_NoOrMoreThanOneContentTypeSpecified", new object[] { p0 });
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x0001EE0C File Offset: 0x0001D00C
		internal static string MediaTypeUtils_BoundaryMustBeSpecifiedForBatchPayloads(object p0, object p1)
		{
			return TextRes.GetString("MediaTypeUtils_BoundaryMustBeSpecifiedForBatchPayloads", new object[] { p0, p1 });
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x0001EE26 File Offset: 0x0001D026
		internal static string ExpressionLexer_ExpectedLiteralToken(object p0)
		{
			return TextRes.GetString("ExpressionLexer_ExpectedLiteralToken", new object[] { p0 });
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x0001EE3C File Offset: 0x0001D03C
		internal static string ODataUriUtils_ConvertToUriLiteralUnsupportedType(object p0)
		{
			return TextRes.GetString("ODataUriUtils_ConvertToUriLiteralUnsupportedType", new object[] { p0 });
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000C08 RID: 3080 RVA: 0x0001EE52 File Offset: 0x0001D052
		internal static string ODataUriUtils_ConvertFromUriLiteralTypeRefWithoutModel
		{
			get
			{
				return TextRes.GetString("ODataUriUtils_ConvertFromUriLiteralTypeRefWithoutModel");
			}
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x0001EE5E File Offset: 0x0001D05E
		internal static string ODataUriUtils_ConvertFromUriLiteralTypeVerificationFailure(object p0, object p1)
		{
			return TextRes.GetString("ODataUriUtils_ConvertFromUriLiteralTypeVerificationFailure", new object[] { p0, p1 });
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x0001EE78 File Offset: 0x0001D078
		internal static string ODataUriUtils_ConvertFromUriLiteralNullTypeVerificationFailure(object p0, object p1)
		{
			return TextRes.GetString("ODataUriUtils_ConvertFromUriLiteralNullTypeVerificationFailure", new object[] { p0, p1 });
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x0001EE92 File Offset: 0x0001D092
		internal static string ODataUriUtils_ConvertFromUriLiteralNullOnNonNullableType(object p0)
		{
			return TextRes.GetString("ODataUriUtils_ConvertFromUriLiteralNullOnNonNullableType", new object[] { p0 });
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x0001EEA8 File Offset: 0x0001D0A8
		internal static string ODataUtils_CannotConvertValueToRawString(object p0)
		{
			return TextRes.GetString("ODataUtils_CannotConvertValueToRawString", new object[] { p0 });
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x0001EEBE File Offset: 0x0001D0BE
		internal static string ODataUtils_DidNotFindDefaultMediaType(object p0)
		{
			return TextRes.GetString("ODataUtils_DidNotFindDefaultMediaType", new object[] { p0 });
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x0001EED4 File Offset: 0x0001D0D4
		internal static string ODataUtils_UnsupportedVersionHeader(object p0)
		{
			return TextRes.GetString("ODataUtils_UnsupportedVersionHeader", new object[] { p0 });
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x0001EEEA File Offset: 0x0001D0EA
		internal static string ODataUtils_MaxProtocolVersionExceeded(object p0, object p1)
		{
			return TextRes.GetString("ODataUtils_MaxProtocolVersionExceeded", new object[] { p0, p1 });
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000C10 RID: 3088 RVA: 0x0001EF04 File Offset: 0x0001D104
		internal static string ODataUtils_UnsupportedVersionNumber
		{
			get
			{
				return TextRes.GetString("ODataUtils_UnsupportedVersionNumber");
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000C11 RID: 3089 RVA: 0x0001EF10 File Offset: 0x0001D110
		internal static string ODataUtils_ModelDoesNotHaveContainer
		{
			get
			{
				return TextRes.GetString("ODataUtils_ModelDoesNotHaveContainer");
			}
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x0001EF1C File Offset: 0x0001D11C
		internal static string ReaderUtils_EnumerableModified(object p0)
		{
			return TextRes.GetString("ReaderUtils_EnumerableModified", new object[] { p0 });
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x0001EF32 File Offset: 0x0001D132
		internal static string ReaderValidationUtils_NullValueForNonNullableType(object p0)
		{
			return TextRes.GetString("ReaderValidationUtils_NullValueForNonNullableType", new object[] { p0 });
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x0001EF48 File Offset: 0x0001D148
		internal static string ReaderValidationUtils_NullNamedValueForNonNullableType(object p0, object p1)
		{
			return TextRes.GetString("ReaderValidationUtils_NullNamedValueForNonNullableType", new object[] { p0, p1 });
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000C15 RID: 3093 RVA: 0x0001EF62 File Offset: 0x0001D162
		internal static string ReaderValidationUtils_EntityReferenceLinkMissingUri
		{
			get
			{
				return TextRes.GetString("ReaderValidationUtils_EntityReferenceLinkMissingUri");
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000C16 RID: 3094 RVA: 0x0001EF6E File Offset: 0x0001D16E
		internal static string ReaderValidationUtils_ValueWithoutType
		{
			get
			{
				return TextRes.GetString("ReaderValidationUtils_ValueWithoutType");
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000C17 RID: 3095 RVA: 0x0001EF7A File Offset: 0x0001D17A
		internal static string ReaderValidationUtils_ResourceWithoutType
		{
			get
			{
				return TextRes.GetString("ReaderValidationUtils_ResourceWithoutType");
			}
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x0001EF86 File Offset: 0x0001D186
		internal static string ReaderValidationUtils_CannotConvertPrimitiveValue(object p0, object p1)
		{
			return TextRes.GetString("ReaderValidationUtils_CannotConvertPrimitiveValue", new object[] { p0, p1 });
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x0001EFA0 File Offset: 0x0001D1A0
		internal static string ReaderValidationUtils_MessageReaderSettingsBaseUriMustBeNullOrAbsolute(object p0)
		{
			return TextRes.GetString("ReaderValidationUtils_MessageReaderSettingsBaseUriMustBeNullOrAbsolute", new object[] { p0 });
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000C1A RID: 3098 RVA: 0x0001EFB6 File Offset: 0x0001D1B6
		internal static string ReaderValidationUtils_UndeclaredPropertyBehaviorKindSpecifiedOnRequest
		{
			get
			{
				return TextRes.GetString("ReaderValidationUtils_UndeclaredPropertyBehaviorKindSpecifiedOnRequest");
			}
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x0001EFC2 File Offset: 0x0001D1C2
		internal static string ReaderValidationUtils_ContextUriValidationInvalidExpectedEntitySet(object p0, object p1, object p2)
		{
			return TextRes.GetString("ReaderValidationUtils_ContextUriValidationInvalidExpectedEntitySet", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x0001EFE0 File Offset: 0x0001D1E0
		internal static string ReaderValidationUtils_ContextUriValidationInvalidExpectedEntityType(object p0, object p1, object p2)
		{
			return TextRes.GetString("ReaderValidationUtils_ContextUriValidationInvalidExpectedEntityType", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x0001EFFE File Offset: 0x0001D1FE
		internal static string ReaderValidationUtils_ContextUriValidationNonMatchingPropertyNames(object p0, object p1, object p2, object p3)
		{
			return TextRes.GetString("ReaderValidationUtils_ContextUriValidationNonMatchingPropertyNames", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x0001F020 File Offset: 0x0001D220
		internal static string ReaderValidationUtils_ContextUriValidationNonMatchingDeclaringTypes(object p0, object p1, object p2, object p3)
		{
			return TextRes.GetString("ReaderValidationUtils_ContextUriValidationNonMatchingDeclaringTypes", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x0001F042 File Offset: 0x0001D242
		internal static string ReaderValidationUtils_NonMatchingPropertyNames(object p0, object p1)
		{
			return TextRes.GetString("ReaderValidationUtils_NonMatchingPropertyNames", new object[] { p0, p1 });
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x0001F05C File Offset: 0x0001D25C
		internal static string ReaderValidationUtils_TypeInContextUriDoesNotMatchExpectedType(object p0, object p1, object p2)
		{
			return TextRes.GetString("ReaderValidationUtils_TypeInContextUriDoesNotMatchExpectedType", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x0001F07A File Offset: 0x0001D27A
		internal static string ReaderValidationUtils_ContextUriDoesNotReferTypeAssignableToExpectedType(object p0, object p1, object p2)
		{
			return TextRes.GetString("ReaderValidationUtils_ContextUriDoesNotReferTypeAssignableToExpectedType", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x0001F098 File Offset: 0x0001D298
		internal static string ReaderValidationUtils_ValueTypeNotAllowedInDerivedTypeConstraint(object p0, object p1, object p2)
		{
			return TextRes.GetString("ReaderValidationUtils_ValueTypeNotAllowedInDerivedTypeConstraint", new object[] { p0, p1, p2 });
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000C23 RID: 3107 RVA: 0x0001F0B6 File Offset: 0x0001D2B6
		internal static string ODataMessageReader_ReaderAlreadyUsed
		{
			get
			{
				return TextRes.GetString("ODataMessageReader_ReaderAlreadyUsed");
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000C24 RID: 3108 RVA: 0x0001F0C2 File Offset: 0x0001D2C2
		internal static string ODataMessageReader_ErrorPayloadInRequest
		{
			get
			{
				return TextRes.GetString("ODataMessageReader_ErrorPayloadInRequest");
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000C25 RID: 3109 RVA: 0x0001F0CE File Offset: 0x0001D2CE
		internal static string ODataMessageReader_ServiceDocumentInRequest
		{
			get
			{
				return TextRes.GetString("ODataMessageReader_ServiceDocumentInRequest");
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000C26 RID: 3110 RVA: 0x0001F0DA File Offset: 0x0001D2DA
		internal static string ODataMessageReader_MetadataDocumentInRequest
		{
			get
			{
				return TextRes.GetString("ODataMessageReader_MetadataDocumentInRequest");
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000C27 RID: 3111 RVA: 0x0001F0E6 File Offset: 0x0001D2E6
		internal static string ODataMessageReader_DeltaInRequest
		{
			get
			{
				return TextRes.GetString("ODataMessageReader_DeltaInRequest");
			}
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x0001F0F2 File Offset: 0x0001D2F2
		internal static string ODataMessageReader_ExpectedTypeSpecifiedWithoutMetadata(object p0)
		{
			return TextRes.GetString("ODataMessageReader_ExpectedTypeSpecifiedWithoutMetadata", new object[] { p0 });
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x0001F108 File Offset: 0x0001D308
		internal static string ODataMessageReader_EntitySetSpecifiedWithoutMetadata(object p0)
		{
			return TextRes.GetString("ODataMessageReader_EntitySetSpecifiedWithoutMetadata", new object[] { p0 });
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x0001F11E File Offset: 0x0001D31E
		internal static string ODataMessageReader_OperationImportSpecifiedWithoutMetadata(object p0)
		{
			return TextRes.GetString("ODataMessageReader_OperationImportSpecifiedWithoutMetadata", new object[] { p0 });
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x0001F134 File Offset: 0x0001D334
		internal static string ODataMessageReader_OperationSpecifiedWithoutMetadata(object p0)
		{
			return TextRes.GetString("ODataMessageReader_OperationSpecifiedWithoutMetadata", new object[] { p0 });
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x0001F14A File Offset: 0x0001D34A
		internal static string ODataMessageReader_ExpectedCollectionTypeWrongKind(object p0)
		{
			return TextRes.GetString("ODataMessageReader_ExpectedCollectionTypeWrongKind", new object[] { p0 });
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000C2D RID: 3117 RVA: 0x0001F160 File Offset: 0x0001D360
		internal static string ODataMessageReader_ExpectedPropertyTypeEntityCollectionKind
		{
			get
			{
				return TextRes.GetString("ODataMessageReader_ExpectedPropertyTypeEntityCollectionKind");
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000C2E RID: 3118 RVA: 0x0001F16C File Offset: 0x0001D36C
		internal static string ODataMessageReader_ExpectedPropertyTypeEntityKind
		{
			get
			{
				return TextRes.GetString("ODataMessageReader_ExpectedPropertyTypeEntityKind");
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000C2F RID: 3119 RVA: 0x0001F178 File Offset: 0x0001D378
		internal static string ODataMessageReader_ExpectedPropertyTypeStream
		{
			get
			{
				return TextRes.GetString("ODataMessageReader_ExpectedPropertyTypeStream");
			}
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x0001F184 File Offset: 0x0001D384
		internal static string ODataMessageReader_ExpectedValueTypeWrongKind(object p0)
		{
			return TextRes.GetString("ODataMessageReader_ExpectedValueTypeWrongKind", new object[] { p0 });
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000C31 RID: 3121 RVA: 0x0001F19A File Offset: 0x0001D39A
		internal static string ODataMessageReader_NoneOrEmptyContentTypeHeader
		{
			get
			{
				return TextRes.GetString("ODataMessageReader_NoneOrEmptyContentTypeHeader");
			}
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x0001F1A6 File Offset: 0x0001D3A6
		internal static string ODataMessageReader_WildcardInContentType(object p0)
		{
			return TextRes.GetString("ODataMessageReader_WildcardInContentType", new object[] { p0 });
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000C33 RID: 3123 RVA: 0x0001F1BC File Offset: 0x0001D3BC
		internal static string ODataMessageReader_GetFormatCalledBeforeReadingStarted
		{
			get
			{
				return TextRes.GetString("ODataMessageReader_GetFormatCalledBeforeReadingStarted");
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000C34 RID: 3124 RVA: 0x0001F1C8 File Offset: 0x0001D3C8
		internal static string ODataMessageReader_DetectPayloadKindMultipleTimes
		{
			get
			{
				return TextRes.GetString("ODataMessageReader_DetectPayloadKindMultipleTimes");
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000C35 RID: 3125 RVA: 0x0001F1D4 File Offset: 0x0001D3D4
		internal static string ODataMessageReader_PayloadKindDetectionRunning
		{
			get
			{
				return TextRes.GetString("ODataMessageReader_PayloadKindDetectionRunning");
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000C36 RID: 3126 RVA: 0x0001F1E0 File Offset: 0x0001D3E0
		internal static string ODataMessageReader_PayloadKindDetectionInServerMode
		{
			get
			{
				return TextRes.GetString("ODataMessageReader_PayloadKindDetectionInServerMode");
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000C37 RID: 3127 RVA: 0x0001F1EC File Offset: 0x0001D3EC
		internal static string ODataMessageReader_ParameterPayloadInResponse
		{
			get
			{
				return TextRes.GetString("ODataMessageReader_ParameterPayloadInResponse");
			}
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x0001F1F8 File Offset: 0x0001D3F8
		internal static string ODataMessageReader_SingletonNavigationPropertyForEntityReferenceLinks(object p0, object p1)
		{
			return TextRes.GetString("ODataMessageReader_SingletonNavigationPropertyForEntityReferenceLinks", new object[] { p0, p1 });
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000C39 RID: 3129 RVA: 0x0001F212 File Offset: 0x0001D412
		internal static string ODataAsyncResponseMessage_MustNotModifyMessage
		{
			get
			{
				return TextRes.GetString("ODataAsyncResponseMessage_MustNotModifyMessage");
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000C3A RID: 3130 RVA: 0x0001F21E File Offset: 0x0001D41E
		internal static string ODataMessage_MustNotModifyMessage
		{
			get
			{
				return TextRes.GetString("ODataMessage_MustNotModifyMessage");
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000C3B RID: 3131 RVA: 0x0001F22A File Offset: 0x0001D42A
		internal static string ODataReaderCore_SyncCallOnAsyncReader
		{
			get
			{
				return TextRes.GetString("ODataReaderCore_SyncCallOnAsyncReader");
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000C3C RID: 3132 RVA: 0x0001F236 File Offset: 0x0001D436
		internal static string ODataReaderCore_AsyncCallOnSyncReader
		{
			get
			{
				return TextRes.GetString("ODataReaderCore_AsyncCallOnSyncReader");
			}
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x0001F242 File Offset: 0x0001D442
		internal static string ODataReaderCore_ReadOrReadAsyncCalledInInvalidState(object p0)
		{
			return TextRes.GetString("ODataReaderCore_ReadOrReadAsyncCalledInInvalidState", new object[] { p0 });
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000C3E RID: 3134 RVA: 0x0001F258 File Offset: 0x0001D458
		internal static string ODataReaderCore_CreateReadStreamCalledInInvalidState
		{
			get
			{
				return TextRes.GetString("ODataReaderCore_CreateReadStreamCalledInInvalidState");
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000C3F RID: 3135 RVA: 0x0001F264 File Offset: 0x0001D464
		internal static string ODataReaderCore_CreateTextReaderCalledInInvalidState
		{
			get
			{
				return TextRes.GetString("ODataReaderCore_CreateTextReaderCalledInInvalidState");
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000C40 RID: 3136 RVA: 0x0001F270 File Offset: 0x0001D470
		internal static string ODataReaderCore_ReadCalledWithOpenStream
		{
			get
			{
				return TextRes.GetString("ODataReaderCore_ReadCalledWithOpenStream");
			}
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x0001F27C File Offset: 0x0001D47C
		internal static string ODataReaderCore_NoReadCallsAllowed(object p0)
		{
			return TextRes.GetString("ODataReaderCore_NoReadCallsAllowed", new object[] { p0 });
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x0001F292 File Offset: 0x0001D492
		internal static string ODataWriterCore_PropertyValueAlreadyWritten(object p0)
		{
			return TextRes.GetString("ODataWriterCore_PropertyValueAlreadyWritten", new object[] { p0 });
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x0001F2A8 File Offset: 0x0001D4A8
		internal static string ODataJsonReader_CannotReadResourcesOfResourceSet(object p0)
		{
			return TextRes.GetString("ODataJsonReader_CannotReadResourcesOfResourceSet", new object[] { p0 });
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x0001F2BE File Offset: 0x0001D4BE
		internal static string ODataJsonReaderUtils_CannotConvertInt32(object p0)
		{
			return TextRes.GetString("ODataJsonReaderUtils_CannotConvertInt32", new object[] { p0 });
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x0001F2D4 File Offset: 0x0001D4D4
		internal static string ODataJsonReaderUtils_CannotConvertDouble(object p0)
		{
			return TextRes.GetString("ODataJsonReaderUtils_CannotConvertDouble", new object[] { p0 });
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x0001F2EA File Offset: 0x0001D4EA
		internal static string ODataJsonReaderUtils_CannotConvertBoolean(object p0)
		{
			return TextRes.GetString("ODataJsonReaderUtils_CannotConvertBoolean", new object[] { p0 });
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x0001F300 File Offset: 0x0001D500
		internal static string ODataJsonReaderUtils_CannotConvertDecimal(object p0)
		{
			return TextRes.GetString("ODataJsonReaderUtils_CannotConvertDecimal", new object[] { p0 });
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x0001F316 File Offset: 0x0001D516
		internal static string ODataJsonReaderUtils_CannotConvertDateTime(object p0)
		{
			return TextRes.GetString("ODataJsonReaderUtils_CannotConvertDateTime", new object[] { p0 });
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x0001F32C File Offset: 0x0001D52C
		internal static string ODataJsonReaderUtils_CannotConvertDateTimeOffset(object p0)
		{
			return TextRes.GetString("ODataJsonReaderUtils_CannotConvertDateTimeOffset", new object[] { p0 });
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x0001F342 File Offset: 0x0001D542
		internal static string ODataJsonReaderUtils_ConflictBetweenInputFormatAndParameter(object p0)
		{
			return TextRes.GetString("ODataJsonReaderUtils_ConflictBetweenInputFormatAndParameter", new object[] { p0 });
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x0001F358 File Offset: 0x0001D558
		internal static string ODataJsonReaderUtils_MultipleErrorPropertiesWithSameName(object p0)
		{
			return TextRes.GetString("ODataJsonReaderUtils_MultipleErrorPropertiesWithSameName", new object[] { p0 });
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x0001F36E File Offset: 0x0001D56E
		internal static string ODataJsonLightResourceSerializer_ActionsAndFunctionsGroupMustSpecifyTarget(object p0)
		{
			return TextRes.GetString("ODataJsonLightResourceSerializer_ActionsAndFunctionsGroupMustSpecifyTarget", new object[] { p0 });
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x0001F384 File Offset: 0x0001D584
		internal static string ODataJsonLightResourceSerializer_ActionsAndFunctionsGroupMustNotHaveDuplicateTarget(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightResourceSerializer_ActionsAndFunctionsGroupMustNotHaveDuplicateTarget", new object[] { p0, p1 });
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x0001F39E File Offset: 0x0001D59E
		internal static string ODataJsonErrorDeserializer_TopLevelErrorWithInvalidProperty(object p0)
		{
			return TextRes.GetString("ODataJsonErrorDeserializer_TopLevelErrorWithInvalidProperty", new object[] { p0 });
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x0001F3B4 File Offset: 0x0001D5B4
		internal static string ODataJsonErrorDeserializer_TopLevelErrorMessageValueWithInvalidProperty(object p0)
		{
			return TextRes.GetString("ODataJsonErrorDeserializer_TopLevelErrorMessageValueWithInvalidProperty", new object[] { p0 });
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x0001F3CA File Offset: 0x0001D5CA
		internal static string ODataCollectionReaderCore_ReadOrReadAsyncCalledInInvalidState(object p0)
		{
			return TextRes.GetString("ODataCollectionReaderCore_ReadOrReadAsyncCalledInInvalidState", new object[] { p0 });
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000C51 RID: 3153 RVA: 0x0001F3E0 File Offset: 0x0001D5E0
		internal static string ODataCollectionReaderCore_SyncCallOnAsyncReader
		{
			get
			{
				return TextRes.GetString("ODataCollectionReaderCore_SyncCallOnAsyncReader");
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000C52 RID: 3154 RVA: 0x0001F3EC File Offset: 0x0001D5EC
		internal static string ODataCollectionReaderCore_AsyncCallOnSyncReader
		{
			get
			{
				return TextRes.GetString("ODataCollectionReaderCore_AsyncCallOnSyncReader");
			}
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x0001F3F8 File Offset: 0x0001D5F8
		internal static string ODataCollectionReaderCore_ExpectedItemTypeSetInInvalidState(object p0, object p1)
		{
			return TextRes.GetString("ODataCollectionReaderCore_ExpectedItemTypeSetInInvalidState", new object[] { p0, p1 });
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x0001F412 File Offset: 0x0001D612
		internal static string ODataParameterReaderCore_ReadOrReadAsyncCalledInInvalidState(object p0)
		{
			return TextRes.GetString("ODataParameterReaderCore_ReadOrReadAsyncCalledInInvalidState", new object[] { p0 });
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000C55 RID: 3157 RVA: 0x0001F428 File Offset: 0x0001D628
		internal static string ODataParameterReaderCore_SyncCallOnAsyncReader
		{
			get
			{
				return TextRes.GetString("ODataParameterReaderCore_SyncCallOnAsyncReader");
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000C56 RID: 3158 RVA: 0x0001F434 File Offset: 0x0001D634
		internal static string ODataParameterReaderCore_AsyncCallOnSyncReader
		{
			get
			{
				return TextRes.GetString("ODataParameterReaderCore_AsyncCallOnSyncReader");
			}
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x0001F440 File Offset: 0x0001D640
		internal static string ODataParameterReaderCore_SubReaderMustBeCreatedAndReadToCompletionBeforeTheNextReadOrReadAsyncCall(object p0, object p1)
		{
			return TextRes.GetString("ODataParameterReaderCore_SubReaderMustBeCreatedAndReadToCompletionBeforeTheNextReadOrReadAsyncCall", new object[] { p0, p1 });
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x0001F45A File Offset: 0x0001D65A
		internal static string ODataParameterReaderCore_SubReaderMustBeInCompletedStateBeforeTheNextReadOrReadAsyncCall(object p0, object p1)
		{
			return TextRes.GetString("ODataParameterReaderCore_SubReaderMustBeInCompletedStateBeforeTheNextReadOrReadAsyncCall", new object[] { p0, p1 });
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x0001F474 File Offset: 0x0001D674
		internal static string ODataParameterReaderCore_InvalidCreateReaderMethodCalledForState(object p0, object p1)
		{
			return TextRes.GetString("ODataParameterReaderCore_InvalidCreateReaderMethodCalledForState", new object[] { p0, p1 });
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x0001F48E File Offset: 0x0001D68E
		internal static string ODataParameterReaderCore_CreateReaderAlreadyCalled(object p0, object p1)
		{
			return TextRes.GetString("ODataParameterReaderCore_CreateReaderAlreadyCalled", new object[] { p0, p1 });
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x0001F4A8 File Offset: 0x0001D6A8
		internal static string ODataParameterReaderCore_ParameterNameNotInMetadata(object p0, object p1)
		{
			return TextRes.GetString("ODataParameterReaderCore_ParameterNameNotInMetadata", new object[] { p0, p1 });
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x0001F4C2 File Offset: 0x0001D6C2
		internal static string ODataParameterReaderCore_DuplicateParametersInPayload(object p0)
		{
			return TextRes.GetString("ODataParameterReaderCore_DuplicateParametersInPayload", new object[] { p0 });
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x0001F4D8 File Offset: 0x0001D6D8
		internal static string ODataParameterReaderCore_ParametersMissingInPayload(object p0, object p1)
		{
			return TextRes.GetString("ODataParameterReaderCore_ParametersMissingInPayload", new object[] { p0, p1 });
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x0001F4F2 File Offset: 0x0001D6F2
		internal static string ValidationUtils_ActionsAndFunctionsMustSpecifyMetadata(object p0)
		{
			return TextRes.GetString("ValidationUtils_ActionsAndFunctionsMustSpecifyMetadata", new object[] { p0 });
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x0001F508 File Offset: 0x0001D708
		internal static string ValidationUtils_ActionsAndFunctionsMustSpecifyTarget(object p0)
		{
			return TextRes.GetString("ValidationUtils_ActionsAndFunctionsMustSpecifyTarget", new object[] { p0 });
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x0001F51E File Offset: 0x0001D71E
		internal static string ValidationUtils_EnumerableContainsANullItem(object p0)
		{
			return TextRes.GetString("ValidationUtils_EnumerableContainsANullItem", new object[] { p0 });
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000C61 RID: 3169 RVA: 0x0001F534 File Offset: 0x0001D734
		internal static string ValidationUtils_AssociationLinkMustSpecifyName
		{
			get
			{
				return TextRes.GetString("ValidationUtils_AssociationLinkMustSpecifyName");
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000C62 RID: 3170 RVA: 0x0001F540 File Offset: 0x0001D740
		internal static string ValidationUtils_AssociationLinkMustSpecifyUrl
		{
			get
			{
				return TextRes.GetString("ValidationUtils_AssociationLinkMustSpecifyUrl");
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000C63 RID: 3171 RVA: 0x0001F54C File Offset: 0x0001D74C
		internal static string ValidationUtils_TypeNameMustNotBeEmpty
		{
			get
			{
				return TextRes.GetString("ValidationUtils_TypeNameMustNotBeEmpty");
			}
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x0001F558 File Offset: 0x0001D758
		internal static string ValidationUtils_PropertyDoesNotExistOnType(object p0, object p1)
		{
			return TextRes.GetString("ValidationUtils_PropertyDoesNotExistOnType", new object[] { p0, p1 });
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000C65 RID: 3173 RVA: 0x0001F572 File Offset: 0x0001D772
		internal static string ValidationUtils_ResourceMustSpecifyUrl
		{
			get
			{
				return TextRes.GetString("ValidationUtils_ResourceMustSpecifyUrl");
			}
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x0001F57E File Offset: 0x0001D77E
		internal static string ValidationUtils_ResourceMustSpecifyName(object p0)
		{
			return TextRes.GetString("ValidationUtils_ResourceMustSpecifyName", new object[] { p0 });
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000C67 RID: 3175 RVA: 0x0001F594 File Offset: 0x0001D794
		internal static string ValidationUtils_ServiceDocumentElementUrlMustNotBeNull
		{
			get
			{
				return TextRes.GetString("ValidationUtils_ServiceDocumentElementUrlMustNotBeNull");
			}
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x0001F5A0 File Offset: 0x0001D7A0
		internal static string ValidationUtils_NonPrimitiveTypeForPrimitiveValue(object p0)
		{
			return TextRes.GetString("ValidationUtils_NonPrimitiveTypeForPrimitiveValue", new object[] { p0 });
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x0001F5B6 File Offset: 0x0001D7B6
		internal static string ValidationUtils_UnsupportedPrimitiveType(object p0)
		{
			return TextRes.GetString("ValidationUtils_UnsupportedPrimitiveType", new object[] { p0 });
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x0001F5CC File Offset: 0x0001D7CC
		internal static string ValidationUtils_IncompatiblePrimitiveItemType(object p0, object p1, object p2, object p3)
		{
			return TextRes.GetString("ValidationUtils_IncompatiblePrimitiveItemType", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000C6B RID: 3179 RVA: 0x0001F5EE File Offset: 0x0001D7EE
		internal static string ValidationUtils_NonNullableCollectionElementsMustNotBeNull
		{
			get
			{
				return TextRes.GetString("ValidationUtils_NonNullableCollectionElementsMustNotBeNull");
			}
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x0001F5FA File Offset: 0x0001D7FA
		internal static string ValidationUtils_InvalidCollectionTypeName(object p0)
		{
			return TextRes.GetString("ValidationUtils_InvalidCollectionTypeName", new object[] { p0 });
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x0001F610 File Offset: 0x0001D810
		internal static string ValidationUtils_UnrecognizedTypeName(object p0)
		{
			return TextRes.GetString("ValidationUtils_UnrecognizedTypeName", new object[] { p0 });
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x0001F626 File Offset: 0x0001D826
		internal static string ValidationUtils_IncorrectTypeKind(object p0, object p1, object p2)
		{
			return TextRes.GetString("ValidationUtils_IncorrectTypeKind", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x0001F644 File Offset: 0x0001D844
		internal static string ValidationUtils_IncorrectTypeKindNoTypeName(object p0, object p1)
		{
			return TextRes.GetString("ValidationUtils_IncorrectTypeKindNoTypeName", new object[] { p0, p1 });
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x0001F65E File Offset: 0x0001D85E
		internal static string ValidationUtils_IncorrectValueTypeKind(object p0, object p1)
		{
			return TextRes.GetString("ValidationUtils_IncorrectValueTypeKind", new object[] { p0, p1 });
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000C71 RID: 3185 RVA: 0x0001F678 File Offset: 0x0001D878
		internal static string ValidationUtils_LinkMustSpecifyName
		{
			get
			{
				return TextRes.GetString("ValidationUtils_LinkMustSpecifyName");
			}
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x0001F684 File Offset: 0x0001D884
		internal static string ValidationUtils_MismatchPropertyKindForStreamProperty(object p0)
		{
			return TextRes.GetString("ValidationUtils_MismatchPropertyKindForStreamProperty", new object[] { p0 });
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000C73 RID: 3187 RVA: 0x0001F69A File Offset: 0x0001D89A
		internal static string ValidationUtils_NestedCollectionsAreNotSupported
		{
			get
			{
				return TextRes.GetString("ValidationUtils_NestedCollectionsAreNotSupported");
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000C74 RID: 3188 RVA: 0x0001F6A6 File Offset: 0x0001D8A6
		internal static string ValidationUtils_StreamReferenceValuesNotSupportedInCollections
		{
			get
			{
				return TextRes.GetString("ValidationUtils_StreamReferenceValuesNotSupportedInCollections");
			}
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x0001F6B2 File Offset: 0x0001D8B2
		internal static string ValidationUtils_IncompatibleType(object p0, object p1)
		{
			return TextRes.GetString("ValidationUtils_IncompatibleType", new object[] { p0, p1 });
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x0001F6CC File Offset: 0x0001D8CC
		internal static string ValidationUtils_OpenCollectionProperty(object p0)
		{
			return TextRes.GetString("ValidationUtils_OpenCollectionProperty", new object[] { p0 });
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x0001F6E2 File Offset: 0x0001D8E2
		internal static string ValidationUtils_OpenStreamProperty(object p0)
		{
			return TextRes.GetString("ValidationUtils_OpenStreamProperty", new object[] { p0 });
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x0001F6F8 File Offset: 0x0001D8F8
		internal static string ValidationUtils_InvalidCollectionTypeReference(object p0)
		{
			return TextRes.GetString("ValidationUtils_InvalidCollectionTypeReference", new object[] { p0 });
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x0001F70E File Offset: 0x0001D90E
		internal static string ValidationUtils_ResourceWithMediaResourceAndNonMLEType(object p0)
		{
			return TextRes.GetString("ValidationUtils_ResourceWithMediaResourceAndNonMLEType", new object[] { p0 });
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x0001F724 File Offset: 0x0001D924
		internal static string ValidationUtils_ResourceWithoutMediaResourceAndMLEType(object p0)
		{
			return TextRes.GetString("ValidationUtils_ResourceWithoutMediaResourceAndMLEType", new object[] { p0 });
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x0001F73A File Offset: 0x0001D93A
		internal static string ValidationUtils_ResourceTypeNotAssignableToExpectedType(object p0, object p1)
		{
			return TextRes.GetString("ValidationUtils_ResourceTypeNotAssignableToExpectedType", new object[] { p0, p1 });
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x0001F754 File Offset: 0x0001D954
		internal static string ValidationUtils_NavigationPropertyExpected(object p0, object p1, object p2)
		{
			return TextRes.GetString("ValidationUtils_NavigationPropertyExpected", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x0001F772 File Offset: 0x0001D972
		internal static string ValidationUtils_InvalidBatchBoundaryDelimiterLength(object p0, object p1)
		{
			return TextRes.GetString("ValidationUtils_InvalidBatchBoundaryDelimiterLength", new object[] { p0, p1 });
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x0001F78C File Offset: 0x0001D98C
		internal static string ValidationUtils_RecursionDepthLimitReached(object p0)
		{
			return TextRes.GetString("ValidationUtils_RecursionDepthLimitReached", new object[] { p0 });
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x0001F7A2 File Offset: 0x0001D9A2
		internal static string ValidationUtils_MaxDepthOfNestedEntriesExceeded(object p0)
		{
			return TextRes.GetString("ValidationUtils_MaxDepthOfNestedEntriesExceeded", new object[] { p0 });
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x0001F7B8 File Offset: 0x0001D9B8
		internal static string ValidationUtils_NullCollectionItemForNonNullableType(object p0)
		{
			return TextRes.GetString("ValidationUtils_NullCollectionItemForNonNullableType", new object[] { p0 });
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x0001F7CE File Offset: 0x0001D9CE
		internal static string ValidationUtils_PropertiesMustNotContainReservedChars(object p0, object p1)
		{
			return TextRes.GetString("ValidationUtils_PropertiesMustNotContainReservedChars", new object[] { p0, p1 });
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000C82 RID: 3202 RVA: 0x0001F7E8 File Offset: 0x0001D9E8
		internal static string ValidationUtils_WorkspaceResourceMustNotContainNullItem
		{
			get
			{
				return TextRes.GetString("ValidationUtils_WorkspaceResourceMustNotContainNullItem");
			}
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x0001F7F4 File Offset: 0x0001D9F4
		internal static string ValidationUtils_InvalidMetadataReferenceProperty(object p0)
		{
			return TextRes.GetString("ValidationUtils_InvalidMetadataReferenceProperty", new object[] { p0 });
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000C84 RID: 3204 RVA: 0x0001F80A File Offset: 0x0001DA0A
		internal static string WriterValidationUtils_PropertyMustNotBeNull
		{
			get
			{
				return TextRes.GetString("WriterValidationUtils_PropertyMustNotBeNull");
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000C85 RID: 3205 RVA: 0x0001F816 File Offset: 0x0001DA16
		internal static string WriterValidationUtils_PropertiesMustHaveNonEmptyName
		{
			get
			{
				return TextRes.GetString("WriterValidationUtils_PropertiesMustHaveNonEmptyName");
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000C86 RID: 3206 RVA: 0x0001F822 File Offset: 0x0001DA22
		internal static string WriterValidationUtils_MissingTypeNameWithMetadata
		{
			get
			{
				return TextRes.GetString("WriterValidationUtils_MissingTypeNameWithMetadata");
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000C87 RID: 3207 RVA: 0x0001F82E File Offset: 0x0001DA2E
		internal static string WriterValidationUtils_NextPageLinkInRequest
		{
			get
			{
				return TextRes.GetString("WriterValidationUtils_NextPageLinkInRequest");
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000C88 RID: 3208 RVA: 0x0001F83A File Offset: 0x0001DA3A
		internal static string WriterValidationUtils_DefaultStreamWithContentTypeWithoutReadLink
		{
			get
			{
				return TextRes.GetString("WriterValidationUtils_DefaultStreamWithContentTypeWithoutReadLink");
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000C89 RID: 3209 RVA: 0x0001F846 File Offset: 0x0001DA46
		internal static string WriterValidationUtils_DefaultStreamWithReadLinkWithoutContentType
		{
			get
			{
				return TextRes.GetString("WriterValidationUtils_DefaultStreamWithReadLinkWithoutContentType");
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000C8A RID: 3210 RVA: 0x0001F852 File Offset: 0x0001DA52
		internal static string WriterValidationUtils_StreamReferenceValueMustHaveEditLinkOrReadLink
		{
			get
			{
				return TextRes.GetString("WriterValidationUtils_StreamReferenceValueMustHaveEditLinkOrReadLink");
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000C8B RID: 3211 RVA: 0x0001F85E File Offset: 0x0001DA5E
		internal static string WriterValidationUtils_StreamReferenceValueMustHaveEditLinkToHaveETag
		{
			get
			{
				return TextRes.GetString("WriterValidationUtils_StreamReferenceValueMustHaveEditLinkToHaveETag");
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000C8C RID: 3212 RVA: 0x0001F86A File Offset: 0x0001DA6A
		internal static string WriterValidationUtils_StreamReferenceValueEmptyContentType
		{
			get
			{
				return TextRes.GetString("WriterValidationUtils_StreamReferenceValueEmptyContentType");
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000C8D RID: 3213 RVA: 0x0001F876 File Offset: 0x0001DA76
		internal static string WriterValidationUtils_EntriesMustHaveNonEmptyId
		{
			get
			{
				return TextRes.GetString("WriterValidationUtils_EntriesMustHaveNonEmptyId");
			}
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x0001F882 File Offset: 0x0001DA82
		internal static string WriterValidationUtils_MessageWriterSettingsBaseUriMustBeNullOrAbsolute(object p0)
		{
			return TextRes.GetString("WriterValidationUtils_MessageWriterSettingsBaseUriMustBeNullOrAbsolute", new object[] { p0 });
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000C8F RID: 3215 RVA: 0x0001F898 File Offset: 0x0001DA98
		internal static string WriterValidationUtils_EntityReferenceLinkUrlMustNotBeNull
		{
			get
			{
				return TextRes.GetString("WriterValidationUtils_EntityReferenceLinkUrlMustNotBeNull");
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000C90 RID: 3216 RVA: 0x0001F8A4 File Offset: 0x0001DAA4
		internal static string WriterValidationUtils_EntityReferenceLinksLinkMustNotBeNull
		{
			get
			{
				return TextRes.GetString("WriterValidationUtils_EntityReferenceLinksLinkMustNotBeNull");
			}
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x0001F8B0 File Offset: 0x0001DAB0
		internal static string WriterValidationUtils_NestedResourceTypeNotCompatibleWithParentPropertyType(object p0, object p1)
		{
			return TextRes.GetString("WriterValidationUtils_NestedResourceTypeNotCompatibleWithParentPropertyType", new object[] { p0, p1 });
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x0001F8CA File Offset: 0x0001DACA
		internal static string WriterValidationUtils_ExpandedLinkIsCollectionTrueWithResourceContent(object p0)
		{
			return TextRes.GetString("WriterValidationUtils_ExpandedLinkIsCollectionTrueWithResourceContent", new object[] { p0 });
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x0001F8E0 File Offset: 0x0001DAE0
		internal static string WriterValidationUtils_ExpandedLinkIsCollectionFalseWithResourceSetContent(object p0)
		{
			return TextRes.GetString("WriterValidationUtils_ExpandedLinkIsCollectionFalseWithResourceSetContent", new object[] { p0 });
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x0001F8F6 File Offset: 0x0001DAF6
		internal static string WriterValidationUtils_ExpandedLinkIsCollectionTrueWithResourceMetadata(object p0)
		{
			return TextRes.GetString("WriterValidationUtils_ExpandedLinkIsCollectionTrueWithResourceMetadata", new object[] { p0 });
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x0001F90C File Offset: 0x0001DB0C
		internal static string WriterValidationUtils_ExpandedLinkIsCollectionFalseWithResourceSetMetadata(object p0)
		{
			return TextRes.GetString("WriterValidationUtils_ExpandedLinkIsCollectionFalseWithResourceSetMetadata", new object[] { p0 });
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x0001F922 File Offset: 0x0001DB22
		internal static string WriterValidationUtils_ExpandedLinkWithResourceSetPayloadAndResourceMetadata(object p0)
		{
			return TextRes.GetString("WriterValidationUtils_ExpandedLinkWithResourceSetPayloadAndResourceMetadata", new object[] { p0 });
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x0001F938 File Offset: 0x0001DB38
		internal static string WriterValidationUtils_ExpandedLinkWithResourcePayloadAndResourceSetMetadata(object p0)
		{
			return TextRes.GetString("WriterValidationUtils_ExpandedLinkWithResourcePayloadAndResourceSetMetadata", new object[] { p0 });
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x0001F94E File Offset: 0x0001DB4E
		internal static string WriterValidationUtils_CollectionPropertiesMustNotHaveNullValue(object p0)
		{
			return TextRes.GetString("WriterValidationUtils_CollectionPropertiesMustNotHaveNullValue", new object[] { p0 });
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x0001F964 File Offset: 0x0001DB64
		internal static string WriterValidationUtils_NonNullablePropertiesMustNotHaveNullValue(object p0, object p1)
		{
			return TextRes.GetString("WriterValidationUtils_NonNullablePropertiesMustNotHaveNullValue", new object[] { p0, p1 });
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x0001F97E File Offset: 0x0001DB7E
		internal static string WriterValidationUtils_StreamPropertiesMustNotHaveNullValue(object p0)
		{
			return TextRes.GetString("WriterValidationUtils_StreamPropertiesMustNotHaveNullValue", new object[] { p0 });
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x0001F994 File Offset: 0x0001DB94
		internal static string WriterValidationUtils_OperationInRequest(object p0)
		{
			return TextRes.GetString("WriterValidationUtils_OperationInRequest", new object[] { p0 });
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x0001F9AA File Offset: 0x0001DBAA
		internal static string WriterValidationUtils_AssociationLinkInRequest(object p0)
		{
			return TextRes.GetString("WriterValidationUtils_AssociationLinkInRequest", new object[] { p0 });
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x0001F9C0 File Offset: 0x0001DBC0
		internal static string WriterValidationUtils_StreamPropertyInRequest(object p0)
		{
			return TextRes.GetString("WriterValidationUtils_StreamPropertyInRequest", new object[] { p0 });
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x0001F9D6 File Offset: 0x0001DBD6
		internal static string WriterValidationUtils_MessageWriterSettingsServiceDocumentUriMustBeNullOrAbsolute(object p0)
		{
			return TextRes.GetString("WriterValidationUtils_MessageWriterSettingsServiceDocumentUriMustBeNullOrAbsolute", new object[] { p0 });
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x0001F9EC File Offset: 0x0001DBEC
		internal static string WriterValidationUtils_NavigationLinkMustSpecifyUrl(object p0)
		{
			return TextRes.GetString("WriterValidationUtils_NavigationLinkMustSpecifyUrl", new object[] { p0 });
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x0001FA02 File Offset: 0x0001DC02
		internal static string WriterValidationUtils_NestedResourceInfoMustSpecifyIsCollection(object p0)
		{
			return TextRes.GetString("WriterValidationUtils_NestedResourceInfoMustSpecifyIsCollection", new object[] { p0 });
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000CA1 RID: 3233 RVA: 0x0001FA18 File Offset: 0x0001DC18
		internal static string WriterValidationUtils_MessageWriterSettingsJsonPaddingOnRequestMessage
		{
			get
			{
				return TextRes.GetString("WriterValidationUtils_MessageWriterSettingsJsonPaddingOnRequestMessage");
			}
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x0001FA24 File Offset: 0x0001DC24
		internal static string WriterValidationUtils_ValueTypeNotAllowedInDerivedTypeConstraint(object p0, object p1, object p2)
		{
			return TextRes.GetString("WriterValidationUtils_ValueTypeNotAllowedInDerivedTypeConstraint", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x0001FA42 File Offset: 0x0001DC42
		internal static string XmlReaderExtension_InvalidNodeInStringValue(object p0)
		{
			return TextRes.GetString("XmlReaderExtension_InvalidNodeInStringValue", new object[] { p0 });
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x0001FA58 File Offset: 0x0001DC58
		internal static string XmlReaderExtension_InvalidRootNode(object p0)
		{
			return TextRes.GetString("XmlReaderExtension_InvalidRootNode", new object[] { p0 });
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x0001FA6E File Offset: 0x0001DC6E
		internal static string ODataMetadataInputContext_ErrorReadingMetadata(object p0)
		{
			return TextRes.GetString("ODataMetadataInputContext_ErrorReadingMetadata", new object[] { p0 });
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x0001FA84 File Offset: 0x0001DC84
		internal static string ODataMetadataOutputContext_ErrorWritingMetadata(object p0)
		{
			return TextRes.GetString("ODataMetadataOutputContext_ErrorWritingMetadata", new object[] { p0 });
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x0001FA9A File Offset: 0x0001DC9A
		internal static string ODataAtomDeserializer_RelativeUriUsedWithoutBaseUriSpecified(object p0)
		{
			return TextRes.GetString("ODataAtomDeserializer_RelativeUriUsedWithoutBaseUriSpecified", new object[] { p0 });
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x0001FAB0 File Offset: 0x0001DCB0
		internal static string ODataAtomPropertyAndValueDeserializer_InvalidCollectionElement(object p0, object p1)
		{
			return TextRes.GetString("ODataAtomPropertyAndValueDeserializer_InvalidCollectionElement", new object[] { p0, p1 });
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x0001FACA File Offset: 0x0001DCCA
		internal static string ODataAtomPropertyAndValueDeserializer_NavigationPropertyInProperties(object p0, object p1)
		{
			return TextRes.GetString("ODataAtomPropertyAndValueDeserializer_NavigationPropertyInProperties", new object[] { p0, p1 });
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x0001FAE4 File Offset: 0x0001DCE4
		internal static string JsonLightInstanceAnnotationWriter_NullValueNotAllowedForInstanceAnnotation(object p0, object p1)
		{
			return TextRes.GetString("JsonLightInstanceAnnotationWriter_NullValueNotAllowedForInstanceAnnotation", new object[] { p0, p1 });
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x0001FAFE File Offset: 0x0001DCFE
		internal static string EdmLibraryExtensions_OperationGroupReturningActionsAndFunctionsModelInvalid(object p0)
		{
			return TextRes.GetString("EdmLibraryExtensions_OperationGroupReturningActionsAndFunctionsModelInvalid", new object[] { p0 });
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x0001FB14 File Offset: 0x0001DD14
		internal static string EdmLibraryExtensions_UnBoundOperationsFoundFromIEdmModelFindMethodIsInvalid(object p0)
		{
			return TextRes.GetString("EdmLibraryExtensions_UnBoundOperationsFoundFromIEdmModelFindMethodIsInvalid", new object[] { p0 });
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x0001FB2A File Offset: 0x0001DD2A
		internal static string EdmLibraryExtensions_NoParameterBoundOperationsFoundFromIEdmModelFindMethodIsInvalid(object p0)
		{
			return TextRes.GetString("EdmLibraryExtensions_NoParameterBoundOperationsFoundFromIEdmModelFindMethodIsInvalid", new object[] { p0 });
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x0001FB40 File Offset: 0x0001DD40
		internal static string EdmLibraryExtensions_ValueOverflowForUnderlyingType(object p0, object p1)
		{
			return TextRes.GetString("EdmLibraryExtensions_ValueOverflowForUnderlyingType", new object[] { p0, p1 });
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x0001FB5A File Offset: 0x0001DD5A
		internal static string ODataAtomResourceDeserializer_ContentWithWrongType(object p0)
		{
			return TextRes.GetString("ODataAtomResourceDeserializer_ContentWithWrongType", new object[] { p0 });
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x0001FB70 File Offset: 0x0001DD70
		internal static string ODataAtomErrorDeserializer_MultipleErrorElementsWithSameName(object p0)
		{
			return TextRes.GetString("ODataAtomErrorDeserializer_MultipleErrorElementsWithSameName", new object[] { p0 });
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x0001FB86 File Offset: 0x0001DD86
		internal static string ODataAtomErrorDeserializer_MultipleInnerErrorElementsWithSameName(object p0)
		{
			return TextRes.GetString("ODataAtomErrorDeserializer_MultipleInnerErrorElementsWithSameName", new object[] { p0 });
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x0001FB9C File Offset: 0x0001DD9C
		internal static string CollectionWithoutExpectedTypeValidator_InvalidItemTypeKind(object p0)
		{
			return TextRes.GetString("CollectionWithoutExpectedTypeValidator_InvalidItemTypeKind", new object[] { p0 });
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x0001FBB2 File Offset: 0x0001DDB2
		internal static string CollectionWithoutExpectedTypeValidator_IncompatibleItemTypeKind(object p0, object p1)
		{
			return TextRes.GetString("CollectionWithoutExpectedTypeValidator_IncompatibleItemTypeKind", new object[] { p0, p1 });
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x0001FBCC File Offset: 0x0001DDCC
		internal static string CollectionWithoutExpectedTypeValidator_IncompatibleItemTypeName(object p0, object p1)
		{
			return TextRes.GetString("CollectionWithoutExpectedTypeValidator_IncompatibleItemTypeName", new object[] { p0, p1 });
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x0001FBE6 File Offset: 0x0001DDE6
		internal static string ResourceSetWithoutExpectedTypeValidator_IncompatibleTypes(object p0, object p1)
		{
			return TextRes.GetString("ResourceSetWithoutExpectedTypeValidator_IncompatibleTypes", new object[] { p0, p1 });
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x0001FC00 File Offset: 0x0001DE00
		internal static string MessageStreamWrappingStream_ByteLimitExceeded(object p0, object p1)
		{
			return TextRes.GetString("MessageStreamWrappingStream_ByteLimitExceeded", new object[] { p0, p1 });
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x0001FC1A File Offset: 0x0001DE1A
		internal static string MetadataUtils_ResolveTypeName(object p0)
		{
			return TextRes.GetString("MetadataUtils_ResolveTypeName", new object[] { p0 });
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x0001FC30 File Offset: 0x0001DE30
		internal static string MetadataUtils_CalculateBindableOperationsForType(object p0)
		{
			return TextRes.GetString("MetadataUtils_CalculateBindableOperationsForType", new object[] { p0 });
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x0001FC46 File Offset: 0x0001DE46
		internal static string EdmValueUtils_UnsupportedPrimitiveType(object p0)
		{
			return TextRes.GetString("EdmValueUtils_UnsupportedPrimitiveType", new object[] { p0 });
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x0001FC5C File Offset: 0x0001DE5C
		internal static string EdmValueUtils_IncorrectPrimitiveTypeKind(object p0, object p1, object p2)
		{
			return TextRes.GetString("EdmValueUtils_IncorrectPrimitiveTypeKind", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x0001FC7A File Offset: 0x0001DE7A
		internal static string EdmValueUtils_IncorrectPrimitiveTypeKindNoTypeName(object p0, object p1)
		{
			return TextRes.GetString("EdmValueUtils_IncorrectPrimitiveTypeKindNoTypeName", new object[] { p0, p1 });
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x0001FC94 File Offset: 0x0001DE94
		internal static string EdmValueUtils_CannotConvertTypeToClrValue(object p0)
		{
			return TextRes.GetString("EdmValueUtils_CannotConvertTypeToClrValue", new object[] { p0 });
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x0001FCAA File Offset: 0x0001DEAA
		internal static string ODataEdmStructuredValue_UndeclaredProperty(object p0, object p1)
		{
			return TextRes.GetString("ODataEdmStructuredValue_UndeclaredProperty", new object[] { p0, p1 });
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x0001FCC4 File Offset: 0x0001DEC4
		internal static string ODataMetadataBuilder_MissingEntitySetUri(object p0)
		{
			return TextRes.GetString("ODataMetadataBuilder_MissingEntitySetUri", new object[] { p0 });
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x0001FCDA File Offset: 0x0001DEDA
		internal static string ODataMetadataBuilder_MissingSegmentForEntitySetUriSuffix(object p0, object p1)
		{
			return TextRes.GetString("ODataMetadataBuilder_MissingSegmentForEntitySetUriSuffix", new object[] { p0, p1 });
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x0001FCF4 File Offset: 0x0001DEF4
		internal static string ODataMetadataBuilder_MissingEntityInstanceUri(object p0)
		{
			return TextRes.GetString("ODataMetadataBuilder_MissingEntityInstanceUri", new object[] { p0 });
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000CC1 RID: 3265 RVA: 0x0001FD0A File Offset: 0x0001DF0A
		internal static string ODataMetadataBuilder_MissingParentIdOrContextUrl
		{
			get
			{
				return TextRes.GetString("ODataMetadataBuilder_MissingParentIdOrContextUrl");
			}
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x0001FD16 File Offset: 0x0001DF16
		internal static string ODataMetadataBuilder_UnknownEntitySet(object p0)
		{
			return TextRes.GetString("ODataMetadataBuilder_UnknownEntitySet", new object[] { p0 });
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x0001FD2C File Offset: 0x0001DF2C
		internal static string ODataJsonLightInputContext_EntityTypeMustBeCompatibleWithEntitySetBaseType(object p0, object p1, object p2)
		{
			return TextRes.GetString("ODataJsonLightInputContext_EntityTypeMustBeCompatibleWithEntitySetBaseType", new object[] { p0, p1, p2 });
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000CC4 RID: 3268 RVA: 0x0001FD4A File Offset: 0x0001DF4A
		internal static string ODataJsonLightInputContext_PayloadKindDetectionForRequest
		{
			get
			{
				return TextRes.GetString("ODataJsonLightInputContext_PayloadKindDetectionForRequest");
			}
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x0001FD56 File Offset: 0x0001DF56
		internal static string ODataJsonLightInputContext_OperationCannotBeNullForCreateParameterReader(object p0)
		{
			return TextRes.GetString("ODataJsonLightInputContext_OperationCannotBeNullForCreateParameterReader", new object[] { p0 });
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000CC6 RID: 3270 RVA: 0x0001FD6C File Offset: 0x0001DF6C
		internal static string ODataJsonLightInputContext_NoEntitySetForRequest
		{
			get
			{
				return TextRes.GetString("ODataJsonLightInputContext_NoEntitySetForRequest");
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000CC7 RID: 3271 RVA: 0x0001FD78 File Offset: 0x0001DF78
		internal static string ODataJsonLightInputContext_ModelRequiredForReading
		{
			get
			{
				return TextRes.GetString("ODataJsonLightInputContext_ModelRequiredForReading");
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000CC8 RID: 3272 RVA: 0x0001FD84 File Offset: 0x0001DF84
		internal static string ODataJsonLightInputContext_ItemTypeRequiredForCollectionReaderInRequests
		{
			get
			{
				return TextRes.GetString("ODataJsonLightInputContext_ItemTypeRequiredForCollectionReaderInRequests");
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000CC9 RID: 3273 RVA: 0x0001FD90 File Offset: 0x0001DF90
		internal static string ODataJsonLightDeserializer_ContextLinkNotFoundAsFirstProperty
		{
			get
			{
				return TextRes.GetString("ODataJsonLightDeserializer_ContextLinkNotFoundAsFirstProperty");
			}
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x0001FD9C File Offset: 0x0001DF9C
		internal static string ODataJsonLightDeserializer_OnlyODataTypeAnnotationCanTargetInstanceAnnotation(object p0, object p1, object p2)
		{
			return TextRes.GetString("ODataJsonLightDeserializer_OnlyODataTypeAnnotationCanTargetInstanceAnnotation", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x0001FDBA File Offset: 0x0001DFBA
		internal static string ODataJsonLightDeserializer_AnnotationTargetingInstanceAnnotationWithoutValue(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightDeserializer_AnnotationTargetingInstanceAnnotationWithoutValue", new object[] { p0, p1 });
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000CCC RID: 3276 RVA: 0x0001FDD4 File Offset: 0x0001DFD4
		internal static string ODataJsonLightWriter_EntityReferenceLinkAfterResourceSetInRequest
		{
			get
			{
				return TextRes.GetString("ODataJsonLightWriter_EntityReferenceLinkAfterResourceSetInRequest");
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000CCD RID: 3277 RVA: 0x0001FDE0 File Offset: 0x0001DFE0
		internal static string ODataJsonLightWriter_InstanceAnnotationNotSupportedOnExpandedResourceSet
		{
			get
			{
				return TextRes.GetString("ODataJsonLightWriter_InstanceAnnotationNotSupportedOnExpandedResourceSet");
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000CCE RID: 3278 RVA: 0x0001FDEC File Offset: 0x0001DFEC
		internal static string ODataJsonLightPropertyAndValueSerializer_NoExpectedTypeOrTypeNameSpecifiedForResourceValueRequest
		{
			get
			{
				return TextRes.GetString("ODataJsonLightPropertyAndValueSerializer_NoExpectedTypeOrTypeNameSpecifiedForResourceValueRequest");
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000CCF RID: 3279 RVA: 0x0001FDF8 File Offset: 0x0001DFF8
		internal static string ODataJsonLightPropertyAndValueSerializer_NoExpectedTypeOrTypeNameSpecifiedForCollectionValueInRequest
		{
			get
			{
				return TextRes.GetString("ODataJsonLightPropertyAndValueSerializer_NoExpectedTypeOrTypeNameSpecifiedForCollectionValueInRequest");
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000CD0 RID: 3280 RVA: 0x0001FE04 File Offset: 0x0001E004
		internal static string ODataResourceTypeContext_MetadataOrSerializationInfoMissing
		{
			get
			{
				return TextRes.GetString("ODataResourceTypeContext_MetadataOrSerializationInfoMissing");
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000CD1 RID: 3281 RVA: 0x0001FE10 File Offset: 0x0001E010
		internal static string ODataResourceTypeContext_ODataResourceTypeNameMissing
		{
			get
			{
				return TextRes.GetString("ODataResourceTypeContext_ODataResourceTypeNameMissing");
			}
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x0001FE1C File Offset: 0x0001E01C
		internal static string ODataContextUriBuilder_ValidateDerivedType(object p0, object p1)
		{
			return TextRes.GetString("ODataContextUriBuilder_ValidateDerivedType", new object[] { p0, p1 });
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000CD3 RID: 3283 RVA: 0x0001FE36 File Offset: 0x0001E036
		internal static string ODataContextUriBuilder_TypeNameMissingForTopLevelCollection
		{
			get
			{
				return TextRes.GetString("ODataContextUriBuilder_TypeNameMissingForTopLevelCollection");
			}
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x0001FE42 File Offset: 0x0001E042
		internal static string ODataContextUriBuilder_UnsupportedPayloadKind(object p0)
		{
			return TextRes.GetString("ODataContextUriBuilder_UnsupportedPayloadKind", new object[] { p0 });
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000CD5 RID: 3285 RVA: 0x0001FE58 File Offset: 0x0001E058
		internal static string ODataContextUriBuilder_StreamValueMustBePropertiesOfODataResource
		{
			get
			{
				return TextRes.GetString("ODataContextUriBuilder_StreamValueMustBePropertiesOfODataResource");
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000CD6 RID: 3286 RVA: 0x0001FE64 File Offset: 0x0001E064
		internal static string ODataContextUriBuilder_NavigationSourceOrTypeNameMissingForResourceOrResourceSet
		{
			get
			{
				return TextRes.GetString("ODataContextUriBuilder_NavigationSourceOrTypeNameMissingForResourceOrResourceSet");
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000CD7 RID: 3287 RVA: 0x0001FE70 File Offset: 0x0001E070
		internal static string ODataContextUriBuilder_ODataUriMissingForIndividualProperty
		{
			get
			{
				return TextRes.GetString("ODataContextUriBuilder_ODataUriMissingForIndividualProperty");
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000CD8 RID: 3288 RVA: 0x0001FE7C File Offset: 0x0001E07C
		internal static string ODataContextUriBuilder_TypeNameMissingForProperty
		{
			get
			{
				return TextRes.GetString("ODataContextUriBuilder_TypeNameMissingForProperty");
			}
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x0001FE88 File Offset: 0x0001E088
		internal static string ODataContextUriBuilder_ODataPathInvalidForContainedElement(object p0)
		{
			return TextRes.GetString("ODataContextUriBuilder_ODataPathInvalidForContainedElement", new object[] { p0 });
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x0001FE9E File Offset: 0x0001E09E
		internal static string ODataJsonLightPropertyAndValueDeserializer_UnexpectedAnnotationProperties(object p0)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_UnexpectedAnnotationProperties", new object[] { p0 });
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x0001FEB4 File Offset: 0x0001E0B4
		internal static string ODataJsonLightPropertyAndValueDeserializer_UnexpectedPropertyAnnotation(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_UnexpectedPropertyAnnotation", new object[] { p0, p1 });
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x0001FECE File Offset: 0x0001E0CE
		internal static string ODataJsonLightPropertyAndValueDeserializer_UnexpectedODataPropertyAnnotation(object p0)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_UnexpectedODataPropertyAnnotation", new object[] { p0 });
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x0001FEE4 File Offset: 0x0001E0E4
		internal static string ODataJsonLightPropertyAndValueDeserializer_UnexpectedProperty(object p0)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_UnexpectedProperty", new object[] { p0 });
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000CDE RID: 3294 RVA: 0x0001FEFA File Offset: 0x0001E0FA
		internal static string ODataJsonLightPropertyAndValueDeserializer_InvalidTopLevelPropertyPayload
		{
			get
			{
				return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_InvalidTopLevelPropertyPayload");
			}
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x0001FF06 File Offset: 0x0001E106
		internal static string ODataJsonLightPropertyAndValueDeserializer_InvalidTopLevelPropertyName(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_InvalidTopLevelPropertyName", new object[] { p0, p1 });
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x0001FF20 File Offset: 0x0001E120
		internal static string ODataJsonLightPropertyAndValueDeserializer_InvalidTypeName(object p0)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_InvalidTypeName", new object[] { p0 });
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x0001FF36 File Offset: 0x0001E136
		internal static string ODataJsonLightPropertyAndValueDeserializer_TopLevelPropertyAnnotationWithoutProperty(object p0)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_TopLevelPropertyAnnotationWithoutProperty", new object[] { p0 });
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x0001FF4C File Offset: 0x0001E14C
		internal static string ODataJsonLightPropertyAndValueDeserializer_ResourceValuePropertyAnnotationWithoutProperty(object p0)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_ResourceValuePropertyAnnotationWithoutProperty", new object[] { p0 });
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x0001FF62 File Offset: 0x0001E162
		internal static string ODataJsonLightPropertyAndValueDeserializer_ComplexValueWithPropertyTypeAnnotation(object p0)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_ComplexValueWithPropertyTypeAnnotation", new object[] { p0 });
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000CE4 RID: 3300 RVA: 0x0001FF78 File Offset: 0x0001E178
		internal static string ODataJsonLightPropertyAndValueDeserializer_ResourceTypeAnnotationNotFirst
		{
			get
			{
				return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_ResourceTypeAnnotationNotFirst");
			}
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x0001FF84 File Offset: 0x0001E184
		internal static string ODataJsonLightPropertyAndValueDeserializer_UnexpectedDataPropertyAnnotation(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_UnexpectedDataPropertyAnnotation", new object[] { p0, p1 });
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x0001FF9E File Offset: 0x0001E19E
		internal static string ODataJsonLightPropertyAndValueDeserializer_TypePropertyAfterValueProperty(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_TypePropertyAfterValueProperty", new object[] { p0, p1 });
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x0001FFB8 File Offset: 0x0001E1B8
		internal static string ODataJsonLightPropertyAndValueDeserializer_ODataTypeAnnotationInPrimitiveValue(object p0)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_ODataTypeAnnotationInPrimitiveValue", new object[] { p0 });
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x0001FFCE File Offset: 0x0001E1CE
		internal static string ODataJsonLightPropertyAndValueDeserializer_TopLevelPropertyWithPrimitiveNullValue(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_TopLevelPropertyWithPrimitiveNullValue", new object[] { p0, p1 });
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x0001FFE8 File Offset: 0x0001E1E8
		internal static string ODataJsonLightPropertyAndValueDeserializer_UnexpectedMetadataReferenceProperty(object p0)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_UnexpectedMetadataReferenceProperty", new object[] { p0 });
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x0001FFFE File Offset: 0x0001E1FE
		internal static string ODataJsonLightPropertyAndValueDeserializer_NoPropertyAndAnnotationAllowedInNullPayload(object p0)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_NoPropertyAndAnnotationAllowedInNullPayload", new object[] { p0 });
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x00020014 File Offset: 0x0001E214
		internal static string ODataJsonLightPropertyAndValueDeserializer_CollectionTypeNotExpected(object p0)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_CollectionTypeNotExpected", new object[] { p0 });
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x0002002A File Offset: 0x0001E22A
		internal static string ODataJsonLightPropertyAndValueDeserializer_CollectionTypeExpected(object p0)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_CollectionTypeExpected", new object[] { p0 });
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000CED RID: 3309 RVA: 0x00020040 File Offset: 0x0001E240
		internal static string ODataJsonReaderCoreUtils_CannotReadSpatialPropertyValue
		{
			get
			{
				return TextRes.GetString("ODataJsonReaderCoreUtils_CannotReadSpatialPropertyValue");
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000CEE RID: 3310 RVA: 0x0002004C File Offset: 0x0001E24C
		internal static string ODataJsonLightReader_UnexpectedPrimitiveValueForODataResource
		{
			get
			{
				return TextRes.GetString("ODataJsonLightReader_UnexpectedPrimitiveValueForODataResource");
			}
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x00020058 File Offset: 0x0001E258
		internal static string ODataJsonLightReaderUtils_AnnotationWithNullValue(object p0)
		{
			return TextRes.GetString("ODataJsonLightReaderUtils_AnnotationWithNullValue", new object[] { p0 });
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x0002006E File Offset: 0x0001E26E
		internal static string ODataJsonLightReaderUtils_InvalidValueForODataNullAnnotation(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightReaderUtils_InvalidValueForODataNullAnnotation", new object[] { p0, p1 });
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x00020088 File Offset: 0x0001E288
		internal static string JsonLightInstanceAnnotationWriter_DuplicateAnnotationNameInCollection(object p0)
		{
			return TextRes.GetString("JsonLightInstanceAnnotationWriter_DuplicateAnnotationNameInCollection", new object[] { p0 });
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000CF2 RID: 3314 RVA: 0x0002009E File Offset: 0x0001E29E
		internal static string ODataJsonLightContextUriParser_NullMetadataDocumentUri
		{
			get
			{
				return TextRes.GetString("ODataJsonLightContextUriParser_NullMetadataDocumentUri");
			}
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x000200AA File Offset: 0x0001E2AA
		internal static string ODataJsonLightContextUriParser_ContextUriDoesNotMatchExpectedPayloadKind(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightContextUriParser_ContextUriDoesNotMatchExpectedPayloadKind", new object[] { p0, p1 });
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x000200C4 File Offset: 0x0001E2C4
		internal static string ODataJsonLightContextUriParser_InvalidEntitySetNameOrTypeName(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightContextUriParser_InvalidEntitySetNameOrTypeName", new object[] { p0, p1 });
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x000200DE File Offset: 0x0001E2DE
		internal static string ODataJsonLightContextUriParser_InvalidPayloadKindWithSelectQueryOption(object p0)
		{
			return TextRes.GetString("ODataJsonLightContextUriParser_InvalidPayloadKindWithSelectQueryOption", new object[] { p0 });
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000CF6 RID: 3318 RVA: 0x000200F4 File Offset: 0x0001E2F4
		internal static string ODataJsonLightContextUriParser_NoModel
		{
			get
			{
				return TextRes.GetString("ODataJsonLightContextUriParser_NoModel");
			}
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x00020100 File Offset: 0x0001E300
		internal static string ODataJsonLightContextUriParser_InvalidContextUrl(object p0)
		{
			return TextRes.GetString("ODataJsonLightContextUriParser_InvalidContextUrl", new object[] { p0 });
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x00020116 File Offset: 0x0001E316
		internal static string ODataJsonLightContextUriParser_LastSegmentIsKeySegment(object p0)
		{
			return TextRes.GetString("ODataJsonLightContextUriParser_LastSegmentIsKeySegment", new object[] { p0 });
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x0002012C File Offset: 0x0001E32C
		internal static string ODataJsonLightContextUriParser_TopLevelContextUrlShouldBeAbsolute(object p0)
		{
			return TextRes.GetString("ODataJsonLightContextUriParser_TopLevelContextUrlShouldBeAbsolute", new object[] { p0 });
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x00020142 File Offset: 0x0001E342
		internal static string ODataJsonLightResourceDeserializer_DeltaRemovedAnnotationMustBeObject(object p0)
		{
			return TextRes.GetString("ODataJsonLightResourceDeserializer_DeltaRemovedAnnotationMustBeObject", new object[] { p0 });
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000CFB RID: 3323 RVA: 0x00020158 File Offset: 0x0001E358
		internal static string ODataJsonLightResourceDeserializer_ResourceTypeAnnotationNotFirst
		{
			get
			{
				return TextRes.GetString("ODataJsonLightResourceDeserializer_ResourceTypeAnnotationNotFirst");
			}
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x00020164 File Offset: 0x0001E364
		internal static string ODataJsonLightResourceDeserializer_ResourceInstanceAnnotationPrecededByProperty(object p0)
		{
			return TextRes.GetString("ODataJsonLightResourceDeserializer_ResourceInstanceAnnotationPrecededByProperty", new object[] { p0 });
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x0002017A File Offset: 0x0001E37A
		internal static string ODataJsonLightResourceDeserializer_CannotReadResourceSetContentStart(object p0)
		{
			return TextRes.GetString("ODataJsonLightResourceDeserializer_CannotReadResourceSetContentStart", new object[] { p0 });
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x00020190 File Offset: 0x0001E390
		internal static string ODataJsonLightResourceDeserializer_ExpectedResourceSetPropertyNotFound(object p0)
		{
			return TextRes.GetString("ODataJsonLightResourceDeserializer_ExpectedResourceSetPropertyNotFound", new object[] { p0 });
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x000201A6 File Offset: 0x0001E3A6
		internal static string ODataJsonLightResourceDeserializer_InvalidNodeTypeForItemsInResourceSet(object p0)
		{
			return TextRes.GetString("ODataJsonLightResourceDeserializer_InvalidNodeTypeForItemsInResourceSet", new object[] { p0 });
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x000201BC File Offset: 0x0001E3BC
		internal static string ODataJsonLightResourceDeserializer_InvalidPropertyAnnotationInTopLevelResourceSet(object p0)
		{
			return TextRes.GetString("ODataJsonLightResourceDeserializer_InvalidPropertyAnnotationInTopLevelResourceSet", new object[] { p0 });
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x000201D2 File Offset: 0x0001E3D2
		internal static string ODataJsonLightResourceDeserializer_InvalidPropertyInTopLevelResourceSet(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightResourceDeserializer_InvalidPropertyInTopLevelResourceSet", new object[] { p0, p1 });
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x000201EC File Offset: 0x0001E3EC
		internal static string ODataJsonLightResourceDeserializer_PropertyWithoutValueWithWrongType(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightResourceDeserializer_PropertyWithoutValueWithWrongType", new object[] { p0, p1 });
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x00020206 File Offset: 0x0001E406
		internal static string ODataJsonLightResourceDeserializer_OpenPropertyWithoutValue(object p0)
		{
			return TextRes.GetString("ODataJsonLightResourceDeserializer_OpenPropertyWithoutValue", new object[] { p0 });
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x0002021C File Offset: 0x0001E41C
		internal static string ODataJsonLightResourceDeserializer_StreamPropertyInRequest(object p0)
		{
			return TextRes.GetString("ODataJsonLightResourceDeserializer_StreamPropertyInRequest", new object[] { p0 });
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x00020232 File Offset: 0x0001E432
		internal static string ODataJsonLightResourceDeserializer_UnexpectedStreamPropertyAnnotation(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightResourceDeserializer_UnexpectedStreamPropertyAnnotation", new object[] { p0, p1 });
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x0002024C File Offset: 0x0001E44C
		internal static string ODataJsonLightResourceDeserializer_StreamPropertyWithValue(object p0)
		{
			return TextRes.GetString("ODataJsonLightResourceDeserializer_StreamPropertyWithValue", new object[] { p0 });
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x00020262 File Offset: 0x0001E462
		internal static string ODataJsonLightResourceDeserializer_UnexpectedDeferredLinkPropertyAnnotation(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightResourceDeserializer_UnexpectedDeferredLinkPropertyAnnotation", new object[] { p0, p1 });
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x0002027C File Offset: 0x0001E47C
		internal static string ODataJsonLightResourceDeserializer_CannotReadSingletonNestedResource(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightResourceDeserializer_CannotReadSingletonNestedResource", new object[] { p0, p1 });
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x00020296 File Offset: 0x0001E496
		internal static string ODataJsonLightResourceDeserializer_CannotReadCollectionNestedResource(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightResourceDeserializer_CannotReadCollectionNestedResource", new object[] { p0, p1 });
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x000202B0 File Offset: 0x0001E4B0
		internal static string ODataJsonLightResourceDeserializer_CannotReadNestedResource(object p0)
		{
			return TextRes.GetString("ODataJsonLightResourceDeserializer_CannotReadNestedResource", new object[] { p0 });
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x000202C6 File Offset: 0x0001E4C6
		internal static string ODataJsonLightResourceDeserializer_UnexpectedExpandedSingletonNavigationLinkPropertyAnnotation(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightResourceDeserializer_UnexpectedExpandedSingletonNavigationLinkPropertyAnnotation", new object[] { p0, p1 });
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x000202E0 File Offset: 0x0001E4E0
		internal static string ODataJsonLightResourceDeserializer_UnexpectedExpandedCollectionNavigationLinkPropertyAnnotation(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightResourceDeserializer_UnexpectedExpandedCollectionNavigationLinkPropertyAnnotation", new object[] { p0, p1 });
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x000202FA File Offset: 0x0001E4FA
		internal static string ODataJsonLightResourceDeserializer_UnexpectedComplexCollectionPropertyAnnotation(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightResourceDeserializer_UnexpectedComplexCollectionPropertyAnnotation", new object[] { p0, p1 });
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x00020314 File Offset: 0x0001E514
		internal static string ODataJsonLightResourceDeserializer_DuplicateNestedResourceSetAnnotation(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightResourceDeserializer_DuplicateNestedResourceSetAnnotation", new object[] { p0, p1 });
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x0002032E File Offset: 0x0001E52E
		internal static string ODataJsonLightResourceDeserializer_UnexpectedPropertyAnnotationAfterExpandedResourceSet(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightResourceDeserializer_UnexpectedPropertyAnnotationAfterExpandedResourceSet", new object[] { p0, p1 });
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x00020348 File Offset: 0x0001E548
		internal static string ODataJsonLightResourceDeserializer_UnexpectedNavigationLinkInRequestPropertyAnnotation(object p0, object p1, object p2)
		{
			return TextRes.GetString("ODataJsonLightResourceDeserializer_UnexpectedNavigationLinkInRequestPropertyAnnotation", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x00020366 File Offset: 0x0001E566
		internal static string ODataJsonLightResourceDeserializer_ArrayValueForSingletonBindPropertyAnnotation(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightResourceDeserializer_ArrayValueForSingletonBindPropertyAnnotation", new object[] { p0, p1 });
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x00020380 File Offset: 0x0001E580
		internal static string ODataJsonLightResourceDeserializer_StringValueForCollectionBindPropertyAnnotation(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightResourceDeserializer_StringValueForCollectionBindPropertyAnnotation", new object[] { p0, p1 });
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x0002039A File Offset: 0x0001E59A
		internal static string ODataJsonLightResourceDeserializer_EmptyBindArray(object p0)
		{
			return TextRes.GetString("ODataJsonLightResourceDeserializer_EmptyBindArray", new object[] { p0 });
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x000203B0 File Offset: 0x0001E5B0
		internal static string ODataJsonLightResourceDeserializer_NavigationPropertyWithoutValueAndEntityReferenceLink(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightResourceDeserializer_NavigationPropertyWithoutValueAndEntityReferenceLink", new object[] { p0, p1 });
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x000203CA File Offset: 0x0001E5CA
		internal static string ODataJsonLightResourceDeserializer_SingletonNavigationPropertyWithBindingAndValue(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightResourceDeserializer_SingletonNavigationPropertyWithBindingAndValue", new object[] { p0, p1 });
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x000203E4 File Offset: 0x0001E5E4
		internal static string ODataJsonLightResourceDeserializer_PropertyWithoutValueWithUnknownType(object p0)
		{
			return TextRes.GetString("ODataJsonLightResourceDeserializer_PropertyWithoutValueWithUnknownType", new object[] { p0 });
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x000203FA File Offset: 0x0001E5FA
		internal static string ODataJsonLightResourceDeserializer_OperationIsNotActionOrFunction(object p0)
		{
			return TextRes.GetString("ODataJsonLightResourceDeserializer_OperationIsNotActionOrFunction", new object[] { p0 });
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x00020410 File Offset: 0x0001E610
		internal static string ODataJsonLightResourceDeserializer_MultipleOptionalPropertiesInOperation(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightResourceDeserializer_MultipleOptionalPropertiesInOperation", new object[] { p0, p1 });
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x0002042A File Offset: 0x0001E62A
		internal static string ODataJsonLightResourceDeserializer_OperationMissingTargetProperty(object p0)
		{
			return TextRes.GetString("ODataJsonLightResourceDeserializer_OperationMissingTargetProperty", new object[] { p0 });
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000D1A RID: 3354 RVA: 0x00020440 File Offset: 0x0001E640
		internal static string ODataJsonLightResourceDeserializer_MetadataReferencePropertyInRequest
		{
			get
			{
				return TextRes.GetString("ODataJsonLightResourceDeserializer_MetadataReferencePropertyInRequest");
			}
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x0002044C File Offset: 0x0001E64C
		internal static string ODataJsonLightValidationUtils_OperationPropertyCannotBeNull(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightValidationUtils_OperationPropertyCannotBeNull", new object[] { p0, p1 });
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x00020466 File Offset: 0x0001E666
		internal static string ODataJsonLightValidationUtils_OpenMetadataReferencePropertyNotSupported(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightValidationUtils_OpenMetadataReferencePropertyNotSupported", new object[] { p0, p1 });
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x00020480 File Offset: 0x0001E680
		internal static string ODataJsonLightDeserializer_RelativeUriUsedWithouODataMetadataAnnotation(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightDeserializer_RelativeUriUsedWithouODataMetadataAnnotation", new object[] { p0, p1 });
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x0002049A File Offset: 0x0001E69A
		internal static string ODataJsonLightResourceMetadataContext_MetadataAnnotationMustBeInPayload(object p0)
		{
			return TextRes.GetString("ODataJsonLightResourceMetadataContext_MetadataAnnotationMustBeInPayload", new object[] { p0 });
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x000204B0 File Offset: 0x0001E6B0
		internal static string ODataJsonLightCollectionDeserializer_ExpectedCollectionPropertyNotFound(object p0)
		{
			return TextRes.GetString("ODataJsonLightCollectionDeserializer_ExpectedCollectionPropertyNotFound", new object[] { p0 });
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x000204C6 File Offset: 0x0001E6C6
		internal static string ODataJsonLightCollectionDeserializer_CannotReadCollectionContentStart(object p0)
		{
			return TextRes.GetString("ODataJsonLightCollectionDeserializer_CannotReadCollectionContentStart", new object[] { p0 });
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x000204DC File Offset: 0x0001E6DC
		internal static string ODataJsonLightCollectionDeserializer_CannotReadCollectionEnd(object p0)
		{
			return TextRes.GetString("ODataJsonLightCollectionDeserializer_CannotReadCollectionEnd", new object[] { p0 });
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x000204F2 File Offset: 0x0001E6F2
		internal static string ODataJsonLightCollectionDeserializer_InvalidCollectionTypeName(object p0)
		{
			return TextRes.GetString("ODataJsonLightCollectionDeserializer_InvalidCollectionTypeName", new object[] { p0 });
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x00020508 File Offset: 0x0001E708
		internal static string ODataJsonLightEntityReferenceLinkDeserializer_EntityReferenceLinkMustBeObjectValue(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntityReferenceLinkDeserializer_EntityReferenceLinkMustBeObjectValue", new object[] { p0 });
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x0002051E File Offset: 0x0001E71E
		internal static string ODataJsonLightEntityReferenceLinkDeserializer_PropertyAnnotationForEntityReferenceLink(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntityReferenceLinkDeserializer_PropertyAnnotationForEntityReferenceLink", new object[] { p0 });
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x00020534 File Offset: 0x0001E734
		internal static string ODataJsonLightEntityReferenceLinkDeserializer_InvalidAnnotationInEntityReferenceLink(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntityReferenceLinkDeserializer_InvalidAnnotationInEntityReferenceLink", new object[] { p0 });
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x0002054A File Offset: 0x0001E74A
		internal static string ODataJsonLightEntityReferenceLinkDeserializer_InvalidPropertyInEntityReferenceLink(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightEntityReferenceLinkDeserializer_InvalidPropertyInEntityReferenceLink", new object[] { p0, p1 });
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x00020564 File Offset: 0x0001E764
		internal static string ODataJsonLightEntityReferenceLinkDeserializer_MissingEntityReferenceLinkProperty(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntityReferenceLinkDeserializer_MissingEntityReferenceLinkProperty", new object[] { p0 });
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x0002057A File Offset: 0x0001E77A
		internal static string ODataJsonLightEntityReferenceLinkDeserializer_MultipleUriPropertiesInEntityReferenceLink(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntityReferenceLinkDeserializer_MultipleUriPropertiesInEntityReferenceLink", new object[] { p0 });
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x00020590 File Offset: 0x0001E790
		internal static string ODataJsonLightEntityReferenceLinkDeserializer_EntityReferenceLinkUrlCannotBeNull(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntityReferenceLinkDeserializer_EntityReferenceLinkUrlCannotBeNull", new object[] { p0 });
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000D2A RID: 3370 RVA: 0x000205A6 File Offset: 0x0001E7A6
		internal static string ODataJsonLightEntityReferenceLinkDeserializer_PropertyAnnotationForEntityReferenceLinks
		{
			get
			{
				return TextRes.GetString("ODataJsonLightEntityReferenceLinkDeserializer_PropertyAnnotationForEntityReferenceLinks");
			}
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x000205B2 File Offset: 0x0001E7B2
		internal static string ODataJsonLightEntityReferenceLinkDeserializer_InvalidEntityReferenceLinksPropertyFound(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightEntityReferenceLinkDeserializer_InvalidEntityReferenceLinksPropertyFound", new object[] { p0, p1 });
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x000205CC File Offset: 0x0001E7CC
		internal static string ODataJsonLightEntityReferenceLinkDeserializer_InvalidPropertyAnnotationInEntityReferenceLinks(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntityReferenceLinkDeserializer_InvalidPropertyAnnotationInEntityReferenceLinks", new object[] { p0 });
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x000205E2 File Offset: 0x0001E7E2
		internal static string ODataJsonLightEntityReferenceLinkDeserializer_ExpectedEntityReferenceLinksPropertyNotFound(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntityReferenceLinkDeserializer_ExpectedEntityReferenceLinksPropertyNotFound", new object[] { p0 });
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x000205F8 File Offset: 0x0001E7F8
		internal static string ODataJsonOperationsDeserializerUtils_OperationPropertyCannotBeNull(object p0, object p1, object p2)
		{
			return TextRes.GetString("ODataJsonOperationsDeserializerUtils_OperationPropertyCannotBeNull", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x00020616 File Offset: 0x0001E816
		internal static string ODataJsonOperationsDeserializerUtils_OperationsPropertyMustHaveObjectValue(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonOperationsDeserializerUtils_OperationsPropertyMustHaveObjectValue", new object[] { p0, p1 });
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x00020630 File Offset: 0x0001E830
		internal static string ODataJsonLightServiceDocumentDeserializer_DuplicatePropertiesInServiceDocument(object p0)
		{
			return TextRes.GetString("ODataJsonLightServiceDocumentDeserializer_DuplicatePropertiesInServiceDocument", new object[] { p0 });
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x00020646 File Offset: 0x0001E846
		internal static string ODataJsonLightServiceDocumentDeserializer_DuplicatePropertiesInServiceDocumentElement(object p0)
		{
			return TextRes.GetString("ODataJsonLightServiceDocumentDeserializer_DuplicatePropertiesInServiceDocumentElement", new object[] { p0 });
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x0002065C File Offset: 0x0001E85C
		internal static string ODataJsonLightServiceDocumentDeserializer_MissingValuePropertyInServiceDocument(object p0)
		{
			return TextRes.GetString("ODataJsonLightServiceDocumentDeserializer_MissingValuePropertyInServiceDocument", new object[] { p0 });
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x00020672 File Offset: 0x0001E872
		internal static string ODataJsonLightServiceDocumentDeserializer_MissingRequiredPropertyInServiceDocumentElement(object p0)
		{
			return TextRes.GetString("ODataJsonLightServiceDocumentDeserializer_MissingRequiredPropertyInServiceDocumentElement", new object[] { p0 });
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x00020688 File Offset: 0x0001E888
		internal static string ODataJsonLightServiceDocumentDeserializer_PropertyAnnotationInServiceDocument(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightServiceDocumentDeserializer_PropertyAnnotationInServiceDocument", new object[] { p0, p1 });
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x000206A2 File Offset: 0x0001E8A2
		internal static string ODataJsonLightServiceDocumentDeserializer_InstanceAnnotationInServiceDocument(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightServiceDocumentDeserializer_InstanceAnnotationInServiceDocument", new object[] { p0, p1 });
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x000206BC File Offset: 0x0001E8BC
		internal static string ODataJsonLightServiceDocumentDeserializer_PropertyAnnotationInServiceDocumentElement(object p0)
		{
			return TextRes.GetString("ODataJsonLightServiceDocumentDeserializer_PropertyAnnotationInServiceDocumentElement", new object[] { p0 });
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x000206D2 File Offset: 0x0001E8D2
		internal static string ODataJsonLightServiceDocumentDeserializer_InstanceAnnotationInServiceDocumentElement(object p0)
		{
			return TextRes.GetString("ODataJsonLightServiceDocumentDeserializer_InstanceAnnotationInServiceDocumentElement", new object[] { p0 });
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x000206E8 File Offset: 0x0001E8E8
		internal static string ODataJsonLightServiceDocumentDeserializer_UnexpectedPropertyInServiceDocumentElement(object p0, object p1, object p2)
		{
			return TextRes.GetString("ODataJsonLightServiceDocumentDeserializer_UnexpectedPropertyInServiceDocumentElement", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x00020706 File Offset: 0x0001E906
		internal static string ODataJsonLightServiceDocumentDeserializer_UnexpectedPropertyInServiceDocument(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightServiceDocumentDeserializer_UnexpectedPropertyInServiceDocument", new object[] { p0, p1 });
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x00020720 File Offset: 0x0001E920
		internal static string ODataJsonLightServiceDocumentDeserializer_PropertyAnnotationWithoutProperty(object p0)
		{
			return TextRes.GetString("ODataJsonLightServiceDocumentDeserializer_PropertyAnnotationWithoutProperty", new object[] { p0 });
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000D3B RID: 3387 RVA: 0x00020736 File Offset: 0x0001E936
		internal static string ODataJsonLightParameterDeserializer_PropertyAnnotationForParameters
		{
			get
			{
				return TextRes.GetString("ODataJsonLightParameterDeserializer_PropertyAnnotationForParameters");
			}
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x00020742 File Offset: 0x0001E942
		internal static string ODataJsonLightParameterDeserializer_PropertyAnnotationWithoutPropertyForParameters(object p0)
		{
			return TextRes.GetString("ODataJsonLightParameterDeserializer_PropertyAnnotationWithoutPropertyForParameters", new object[] { p0 });
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x00020758 File Offset: 0x0001E958
		internal static string ODataJsonLightParameterDeserializer_UnsupportedPrimitiveParameterType(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightParameterDeserializer_UnsupportedPrimitiveParameterType", new object[] { p0, p1 });
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x00020772 File Offset: 0x0001E972
		internal static string ODataJsonLightParameterDeserializer_NullCollectionExpected(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightParameterDeserializer_NullCollectionExpected", new object[] { p0, p1 });
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x0002078C File Offset: 0x0001E98C
		internal static string ODataJsonLightParameterDeserializer_UnsupportedParameterTypeKind(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightParameterDeserializer_UnsupportedParameterTypeKind", new object[] { p0, p1 });
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000D40 RID: 3392 RVA: 0x000207A6 File Offset: 0x0001E9A6
		internal static string SelectedPropertiesNode_StarSegmentNotLastSegment
		{
			get
			{
				return TextRes.GetString("SelectedPropertiesNode_StarSegmentNotLastSegment");
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000D41 RID: 3393 RVA: 0x000207B2 File Offset: 0x0001E9B2
		internal static string SelectedPropertiesNode_StarSegmentAfterTypeSegment
		{
			get
			{
				return TextRes.GetString("SelectedPropertiesNode_StarSegmentAfterTypeSegment");
			}
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x000207BE File Offset: 0x0001E9BE
		internal static string ODataJsonLightErrorDeserializer_PropertyAnnotationNotAllowedInErrorPayload(object p0)
		{
			return TextRes.GetString("ODataJsonLightErrorDeserializer_PropertyAnnotationNotAllowedInErrorPayload", new object[] { p0 });
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x000207D4 File Offset: 0x0001E9D4
		internal static string ODataJsonLightErrorDeserializer_InstanceAnnotationNotAllowedInErrorPayload(object p0)
		{
			return TextRes.GetString("ODataJsonLightErrorDeserializer_InstanceAnnotationNotAllowedInErrorPayload", new object[] { p0 });
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x000207EA File Offset: 0x0001E9EA
		internal static string ODataJsonLightErrorDeserializer_PropertyAnnotationWithoutPropertyForError(object p0)
		{
			return TextRes.GetString("ODataJsonLightErrorDeserializer_PropertyAnnotationWithoutPropertyForError", new object[] { p0 });
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x00020800 File Offset: 0x0001EA00
		internal static string ODataJsonLightErrorDeserializer_TopLevelErrorValueWithInvalidProperty(object p0)
		{
			return TextRes.GetString("ODataJsonLightErrorDeserializer_TopLevelErrorValueWithInvalidProperty", new object[] { p0 });
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x00020816 File Offset: 0x0001EA16
		internal static string ODataConventionalUriBuilder_EntityTypeWithNoKeyProperties(object p0)
		{
			return TextRes.GetString("ODataConventionalUriBuilder_EntityTypeWithNoKeyProperties", new object[] { p0 });
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x0002082C File Offset: 0x0001EA2C
		internal static string ODataConventionalUriBuilder_NullKeyValue(object p0, object p1)
		{
			return TextRes.GetString("ODataConventionalUriBuilder_NullKeyValue", new object[] { p0, p1 });
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x00020846 File Offset: 0x0001EA46
		internal static string ODataResourceMetadataContext_EntityTypeWithNoKeyProperties(object p0)
		{
			return TextRes.GetString("ODataResourceMetadataContext_EntityTypeWithNoKeyProperties", new object[] { p0 });
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x0002085C File Offset: 0x0001EA5C
		internal static string ODataResourceMetadataContext_NullKeyValue(object p0, object p1)
		{
			return TextRes.GetString("ODataResourceMetadataContext_NullKeyValue", new object[] { p0, p1 });
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x00020876 File Offset: 0x0001EA76
		internal static string ODataResourceMetadataContext_KeyOrETagValuesMustBePrimitiveValues(object p0, object p1)
		{
			return TextRes.GetString("ODataResourceMetadataContext_KeyOrETagValuesMustBePrimitiveValues", new object[] { p0, p1 });
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x00020890 File Offset: 0x0001EA90
		internal static string ODataResource_PropertyValueCannotBeODataResourceValue(object p0)
		{
			return TextRes.GetString("ODataResource_PropertyValueCannotBeODataResourceValue", new object[] { p0 });
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x000208A6 File Offset: 0x0001EAA6
		internal static string EdmValueUtils_NonPrimitiveValue(object p0, object p1)
		{
			return TextRes.GetString("EdmValueUtils_NonPrimitiveValue", new object[] { p0, p1 });
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x000208C0 File Offset: 0x0001EAC0
		internal static string EdmValueUtils_PropertyDoesntExist(object p0, object p1)
		{
			return TextRes.GetString("EdmValueUtils_PropertyDoesntExist", new object[] { p0, p1 });
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000D4E RID: 3406 RVA: 0x000208DA File Offset: 0x0001EADA
		internal static string ODataPrimitiveValue_CannotCreateODataPrimitiveValueFromNull
		{
			get
			{
				return TextRes.GetString("ODataPrimitiveValue_CannotCreateODataPrimitiveValueFromNull");
			}
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x000208E6 File Offset: 0x0001EAE6
		internal static string ODataPrimitiveValue_CannotCreateODataPrimitiveValueFromUnsupportedValueType(object p0)
		{
			return TextRes.GetString("ODataPrimitiveValue_CannotCreateODataPrimitiveValueFromUnsupportedValueType", new object[] { p0 });
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x000208FC File Offset: 0x0001EAFC
		internal static string ODataInstanceAnnotation_NeedPeriodInName(object p0)
		{
			return TextRes.GetString("ODataInstanceAnnotation_NeedPeriodInName", new object[] { p0 });
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x00020912 File Offset: 0x0001EB12
		internal static string ODataInstanceAnnotation_ReservedNamesNotAllowed(object p0, object p1)
		{
			return TextRes.GetString("ODataInstanceAnnotation_ReservedNamesNotAllowed", new object[] { p0, p1 });
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x0002092C File Offset: 0x0001EB2C
		internal static string ODataInstanceAnnotation_BadTermName(object p0)
		{
			return TextRes.GetString("ODataInstanceAnnotation_BadTermName", new object[] { p0 });
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000D53 RID: 3411 RVA: 0x00020942 File Offset: 0x0001EB42
		internal static string ODataInstanceAnnotation_ValueCannotBeODataStreamReferenceValue
		{
			get
			{
				return TextRes.GetString("ODataInstanceAnnotation_ValueCannotBeODataStreamReferenceValue");
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000D54 RID: 3412 RVA: 0x0002094E File Offset: 0x0001EB4E
		internal static string ODataJsonLightValueSerializer_MissingTypeNameOnCollection
		{
			get
			{
				return TextRes.GetString("ODataJsonLightValueSerializer_MissingTypeNameOnCollection");
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000D55 RID: 3413 RVA: 0x0002095A File Offset: 0x0001EB5A
		internal static string ODataJsonLightValueSerializer_MissingRawValueOnUntyped
		{
			get
			{
				return TextRes.GetString("ODataJsonLightValueSerializer_MissingRawValueOnUntyped");
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000D56 RID: 3414 RVA: 0x00020966 File Offset: 0x0001EB66
		internal static string AtomInstanceAnnotation_MissingTermAttributeOnAnnotationElement
		{
			get
			{
				return TextRes.GetString("AtomInstanceAnnotation_MissingTermAttributeOnAnnotationElement");
			}
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x00020972 File Offset: 0x0001EB72
		internal static string AtomInstanceAnnotation_AttributeValueNotationUsedWithIncompatibleType(object p0, object p1)
		{
			return TextRes.GetString("AtomInstanceAnnotation_AttributeValueNotationUsedWithIncompatibleType", new object[] { p0, p1 });
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x0002098C File Offset: 0x0001EB8C
		internal static string AtomInstanceAnnotation_AttributeValueNotationUsedOnNonEmptyElement(object p0)
		{
			return TextRes.GetString("AtomInstanceAnnotation_AttributeValueNotationUsedOnNonEmptyElement", new object[] { p0 });
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000D59 RID: 3417 RVA: 0x000209A2 File Offset: 0x0001EBA2
		internal static string AtomInstanceAnnotation_MultipleAttributeValueNotationAttributes
		{
			get
			{
				return TextRes.GetString("AtomInstanceAnnotation_MultipleAttributeValueNotationAttributes");
			}
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x000209AE File Offset: 0x0001EBAE
		internal static string AnnotationFilterPattern_InvalidPatternMissingDot(object p0)
		{
			return TextRes.GetString("AnnotationFilterPattern_InvalidPatternMissingDot", new object[] { p0 });
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x000209C4 File Offset: 0x0001EBC4
		internal static string AnnotationFilterPattern_InvalidPatternEmptySegment(object p0)
		{
			return TextRes.GetString("AnnotationFilterPattern_InvalidPatternEmptySegment", new object[] { p0 });
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x000209DA File Offset: 0x0001EBDA
		internal static string AnnotationFilterPattern_InvalidPatternWildCardInSegment(object p0)
		{
			return TextRes.GetString("AnnotationFilterPattern_InvalidPatternWildCardInSegment", new object[] { p0 });
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x000209F0 File Offset: 0x0001EBF0
		internal static string AnnotationFilterPattern_InvalidPatternWildCardMustBeInLastSegment(object p0)
		{
			return TextRes.GetString("AnnotationFilterPattern_InvalidPatternWildCardMustBeInLastSegment", new object[] { p0 });
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x00020A06 File Offset: 0x0001EC06
		internal static string SyntacticTree_UriMustBeAbsolute(object p0)
		{
			return TextRes.GetString("SyntacticTree_UriMustBeAbsolute", new object[] { p0 });
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000D5F RID: 3423 RVA: 0x00020A1C File Offset: 0x0001EC1C
		internal static string SyntacticTree_MaxDepthInvalid
		{
			get
			{
				return TextRes.GetString("SyntacticTree_MaxDepthInvalid");
			}
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x00020A28 File Offset: 0x0001EC28
		internal static string SyntacticTree_InvalidSkipQueryOptionValue(object p0)
		{
			return TextRes.GetString("SyntacticTree_InvalidSkipQueryOptionValue", new object[] { p0 });
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x00020A3E File Offset: 0x0001EC3E
		internal static string SyntacticTree_InvalidTopQueryOptionValue(object p0)
		{
			return TextRes.GetString("SyntacticTree_InvalidTopQueryOptionValue", new object[] { p0 });
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x00020A54 File Offset: 0x0001EC54
		internal static string SyntacticTree_InvalidCountQueryOptionValue(object p0, object p1)
		{
			return TextRes.GetString("SyntacticTree_InvalidCountQueryOptionValue", new object[] { p0, p1 });
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x00020A6E File Offset: 0x0001EC6E
		internal static string SyntacticTree_InvalidIndexQueryOptionValue(object p0)
		{
			return TextRes.GetString("SyntacticTree_InvalidIndexQueryOptionValue", new object[] { p0 });
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x00020A84 File Offset: 0x0001EC84
		internal static string QueryOptionUtils_QueryParameterMustBeSpecifiedOnce(object p0)
		{
			return TextRes.GetString("QueryOptionUtils_QueryParameterMustBeSpecifiedOnce", new object[] { p0 });
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x00020A9A File Offset: 0x0001EC9A
		internal static string UriBuilder_NotSupportedClrLiteral(object p0)
		{
			return TextRes.GetString("UriBuilder_NotSupportedClrLiteral", new object[] { p0 });
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x00020AB0 File Offset: 0x0001ECB0
		internal static string UriBuilder_NotSupportedQueryToken(object p0)
		{
			return TextRes.GetString("UriBuilder_NotSupportedQueryToken", new object[] { p0 });
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000D67 RID: 3431 RVA: 0x00020AC6 File Offset: 0x0001ECC6
		internal static string UriQueryExpressionParser_TooDeep
		{
			get
			{
				return TextRes.GetString("UriQueryExpressionParser_TooDeep");
			}
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x00020AD2 File Offset: 0x0001ECD2
		internal static string UriQueryExpressionParser_ExpressionExpected(object p0, object p1)
		{
			return TextRes.GetString("UriQueryExpressionParser_ExpressionExpected", new object[] { p0, p1 });
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x00020AEC File Offset: 0x0001ECEC
		internal static string UriQueryExpressionParser_OpenParenExpected(object p0, object p1)
		{
			return TextRes.GetString("UriQueryExpressionParser_OpenParenExpected", new object[] { p0, p1 });
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x00020B06 File Offset: 0x0001ED06
		internal static string UriQueryExpressionParser_CloseParenOrCommaExpected(object p0, object p1)
		{
			return TextRes.GetString("UriQueryExpressionParser_CloseParenOrCommaExpected", new object[] { p0, p1 });
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x00020B20 File Offset: 0x0001ED20
		internal static string UriQueryExpressionParser_CloseParenOrOperatorExpected(object p0, object p1)
		{
			return TextRes.GetString("UriQueryExpressionParser_CloseParenOrOperatorExpected", new object[] { p0, p1 });
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x00020B3A File Offset: 0x0001ED3A
		internal static string UriQueryExpressionParser_CannotCreateStarTokenFromNonStar(object p0)
		{
			return TextRes.GetString("UriQueryExpressionParser_CannotCreateStarTokenFromNonStar", new object[] { p0 });
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x00020B50 File Offset: 0x0001ED50
		internal static string UriQueryExpressionParser_RangeVariableAlreadyDeclared(object p0)
		{
			return TextRes.GetString("UriQueryExpressionParser_RangeVariableAlreadyDeclared", new object[] { p0 });
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x00020B66 File Offset: 0x0001ED66
		internal static string UriQueryExpressionParser_AsExpected(object p0, object p1)
		{
			return TextRes.GetString("UriQueryExpressionParser_AsExpected", new object[] { p0, p1 });
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x00020B80 File Offset: 0x0001ED80
		internal static string UriQueryExpressionParser_WithExpected(object p0, object p1)
		{
			return TextRes.GetString("UriQueryExpressionParser_WithExpected", new object[] { p0, p1 });
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x00020B9A File Offset: 0x0001ED9A
		internal static string UriQueryExpressionParser_UnrecognizedWithMethod(object p0, object p1, object p2)
		{
			return TextRes.GetString("UriQueryExpressionParser_UnrecognizedWithMethod", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x00020BB8 File Offset: 0x0001EDB8
		internal static string UriQueryExpressionParser_PropertyPathExpected(object p0, object p1)
		{
			return TextRes.GetString("UriQueryExpressionParser_PropertyPathExpected", new object[] { p0, p1 });
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x00020BD2 File Offset: 0x0001EDD2
		internal static string UriQueryExpressionParser_KeywordOrIdentifierExpected(object p0, object p1, object p2)
		{
			return TextRes.GetString("UriQueryExpressionParser_KeywordOrIdentifierExpected", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x00020BF0 File Offset: 0x0001EDF0
		internal static string UriQueryExpressionParser_InnerMostExpandRequireFilter(object p0, object p1)
		{
			return TextRes.GetString("UriQueryExpressionParser_InnerMostExpandRequireFilter", new object[] { p0, p1 });
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x00020C0A File Offset: 0x0001EE0A
		internal static string UriQueryPathParser_RequestUriDoesNotHaveTheCorrectBaseUri(object p0, object p1)
		{
			return TextRes.GetString("UriQueryPathParser_RequestUriDoesNotHaveTheCorrectBaseUri", new object[] { p0, p1 });
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000D75 RID: 3445 RVA: 0x00020C24 File Offset: 0x0001EE24
		internal static string UriQueryPathParser_SyntaxError
		{
			get
			{
				return TextRes.GetString("UriQueryPathParser_SyntaxError");
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000D76 RID: 3446 RVA: 0x00020C30 File Offset: 0x0001EE30
		internal static string UriQueryPathParser_TooManySegments
		{
			get
			{
				return TextRes.GetString("UriQueryPathParser_TooManySegments");
			}
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x00020C3C File Offset: 0x0001EE3C
		internal static string UriQueryPathParser_InvalidEscapeUri(object p0)
		{
			return TextRes.GetString("UriQueryPathParser_InvalidEscapeUri", new object[] { p0 });
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x00020C52 File Offset: 0x0001EE52
		internal static string UriUtils_DateTimeOffsetInvalidFormat(object p0)
		{
			return TextRes.GetString("UriUtils_DateTimeOffsetInvalidFormat", new object[] { p0 });
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000D79 RID: 3449 RVA: 0x00020C68 File Offset: 0x0001EE68
		internal static string SelectionItemBinder_NonNavigationPathToken
		{
			get
			{
				return TextRes.GetString("SelectionItemBinder_NonNavigationPathToken");
			}
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x00020C74 File Offset: 0x0001EE74
		internal static string MetadataBinder_UnsupportedQueryTokenKind(object p0)
		{
			return TextRes.GetString("MetadataBinder_UnsupportedQueryTokenKind", new object[] { p0 });
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x00020C8A File Offset: 0x0001EE8A
		internal static string MetadataBinder_PropertyNotDeclared(object p0, object p1)
		{
			return TextRes.GetString("MetadataBinder_PropertyNotDeclared", new object[] { p0, p1 });
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x00020CA4 File Offset: 0x0001EEA4
		internal static string MetadataBinder_InvalidIdentifierInQueryOption(object p0)
		{
			return TextRes.GetString("MetadataBinder_InvalidIdentifierInQueryOption", new object[] { p0 });
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x00020CBA File Offset: 0x0001EEBA
		internal static string MetadataBinder_PropertyNotDeclaredOrNotKeyInKeyValue(object p0, object p1)
		{
			return TextRes.GetString("MetadataBinder_PropertyNotDeclaredOrNotKeyInKeyValue", new object[] { p0, p1 });
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x00020CD4 File Offset: 0x0001EED4
		internal static string MetadataBinder_QualifiedFunctionNameWithParametersNotDeclared(object p0, object p1)
		{
			return TextRes.GetString("MetadataBinder_QualifiedFunctionNameWithParametersNotDeclared", new object[] { p0, p1 });
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x00020CEE File Offset: 0x0001EEEE
		internal static string MetadataBinder_UnnamedKeyValueOnTypeWithMultipleKeyProperties(object p0)
		{
			return TextRes.GetString("MetadataBinder_UnnamedKeyValueOnTypeWithMultipleKeyProperties", new object[] { p0 });
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x00020D04 File Offset: 0x0001EF04
		internal static string MetadataBinder_DuplicitKeyPropertyInKeyValues(object p0)
		{
			return TextRes.GetString("MetadataBinder_DuplicitKeyPropertyInKeyValues", new object[] { p0 });
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x00020D1A File Offset: 0x0001EF1A
		internal static string MetadataBinder_NotAllKeyPropertiesSpecifiedInKeyValues(object p0)
		{
			return TextRes.GetString("MetadataBinder_NotAllKeyPropertiesSpecifiedInKeyValues", new object[] { p0 });
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x00020D30 File Offset: 0x0001EF30
		internal static string MetadataBinder_CannotConvertToType(object p0, object p1)
		{
			return TextRes.GetString("MetadataBinder_CannotConvertToType", new object[] { p0, p1 });
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000D83 RID: 3459 RVA: 0x00020D4A File Offset: 0x0001EF4A
		internal static string MetadataBinder_FilterExpressionNotSingleValue
		{
			get
			{
				return TextRes.GetString("MetadataBinder_FilterExpressionNotSingleValue");
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000D84 RID: 3460 RVA: 0x00020D56 File Offset: 0x0001EF56
		internal static string MetadataBinder_OrderByExpressionNotSingleValue
		{
			get
			{
				return TextRes.GetString("MetadataBinder_OrderByExpressionNotSingleValue");
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000D85 RID: 3461 RVA: 0x00020D62 File Offset: 0x0001EF62
		internal static string MetadataBinder_PropertyAccessWithoutParentParameter
		{
			get
			{
				return TextRes.GetString("MetadataBinder_PropertyAccessWithoutParentParameter");
			}
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x00020D6E File Offset: 0x0001EF6E
		internal static string MetadataBinder_BinaryOperatorOperandNotSingleValue(object p0)
		{
			return TextRes.GetString("MetadataBinder_BinaryOperatorOperandNotSingleValue", new object[] { p0 });
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x00020D84 File Offset: 0x0001EF84
		internal static string MetadataBinder_UnaryOperatorOperandNotSingleValue(object p0)
		{
			return TextRes.GetString("MetadataBinder_UnaryOperatorOperandNotSingleValue", new object[] { p0 });
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000D88 RID: 3464 RVA: 0x00020D9A File Offset: 0x0001EF9A
		internal static string MetadataBinder_LeftOperandNotSingleValue
		{
			get
			{
				return TextRes.GetString("MetadataBinder_LeftOperandNotSingleValue");
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000D89 RID: 3465 RVA: 0x00020DA6 File Offset: 0x0001EFA6
		internal static string MetadataBinder_RightOperandNotCollectionValue
		{
			get
			{
				return TextRes.GetString("MetadataBinder_RightOperandNotCollectionValue");
			}
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x00020DB2 File Offset: 0x0001EFB2
		internal static string MetadataBinder_PropertyAccessSourceNotSingleValue(object p0)
		{
			return TextRes.GetString("MetadataBinder_PropertyAccessSourceNotSingleValue", new object[] { p0 });
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x00020DC8 File Offset: 0x0001EFC8
		internal static string MetadataBinder_IncompatibleOperandsError(object p0, object p1, object p2)
		{
			return TextRes.GetString("MetadataBinder_IncompatibleOperandsError", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x00020DE6 File Offset: 0x0001EFE6
		internal static string MetadataBinder_IncompatibleOperandError(object p0, object p1)
		{
			return TextRes.GetString("MetadataBinder_IncompatibleOperandError", new object[] { p0, p1 });
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x00020E00 File Offset: 0x0001F000
		internal static string MetadataBinder_UnknownFunction(object p0)
		{
			return TextRes.GetString("MetadataBinder_UnknownFunction", new object[] { p0 });
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x00020E16 File Offset: 0x0001F016
		internal static string MetadataBinder_FunctionArgumentNotSingleValue(object p0)
		{
			return TextRes.GetString("MetadataBinder_FunctionArgumentNotSingleValue", new object[] { p0 });
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x00020E2C File Offset: 0x0001F02C
		internal static string MetadataBinder_NoApplicableFunctionFound(object p0, object p1)
		{
			return TextRes.GetString("MetadataBinder_NoApplicableFunctionFound", new object[] { p0, p1 });
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x00020E46 File Offset: 0x0001F046
		internal static string MetadataBinder_BoundNodeCannotBeNull(object p0)
		{
			return TextRes.GetString("MetadataBinder_BoundNodeCannotBeNull", new object[] { p0 });
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x00020E5C File Offset: 0x0001F05C
		internal static string MetadataBinder_TopRequiresNonNegativeInteger(object p0)
		{
			return TextRes.GetString("MetadataBinder_TopRequiresNonNegativeInteger", new object[] { p0 });
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x00020E72 File Offset: 0x0001F072
		internal static string MetadataBinder_SkipRequiresNonNegativeInteger(object p0)
		{
			return TextRes.GetString("MetadataBinder_SkipRequiresNonNegativeInteger", new object[] { p0 });
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000D93 RID: 3475 RVA: 0x00020E88 File Offset: 0x0001F088
		internal static string MetadataBinder_QueryOptionsBindStateCannotBeNull
		{
			get
			{
				return TextRes.GetString("MetadataBinder_QueryOptionsBindStateCannotBeNull");
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000D94 RID: 3476 RVA: 0x00020E94 File Offset: 0x0001F094
		internal static string MetadataBinder_QueryOptionsBindMethodCannotBeNull
		{
			get
			{
				return TextRes.GetString("MetadataBinder_QueryOptionsBindMethodCannotBeNull");
			}
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x00020EA0 File Offset: 0x0001F0A0
		internal static string MetadataBinder_HierarchyNotFollowed(object p0, object p1)
		{
			return TextRes.GetString("MetadataBinder_HierarchyNotFollowed", new object[] { p0, p1 });
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000D96 RID: 3478 RVA: 0x00020EBA File Offset: 0x0001F0BA
		internal static string MetadataBinder_LambdaParentMustBeCollection
		{
			get
			{
				return TextRes.GetString("MetadataBinder_LambdaParentMustBeCollection");
			}
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x00020EC6 File Offset: 0x0001F0C6
		internal static string MetadataBinder_ParameterNotInScope(object p0)
		{
			return TextRes.GetString("MetadataBinder_ParameterNotInScope", new object[] { p0 });
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000D98 RID: 3480 RVA: 0x00020EDC File Offset: 0x0001F0DC
		internal static string MetadataBinder_NavigationPropertyNotFollowingSingleEntityType
		{
			get
			{
				return TextRes.GetString("MetadataBinder_NavigationPropertyNotFollowingSingleEntityType");
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000D99 RID: 3481 RVA: 0x00020EE8 File Offset: 0x0001F0E8
		internal static string MetadataBinder_AnyAllExpressionNotSingleValue
		{
			get
			{
				return TextRes.GetString("MetadataBinder_AnyAllExpressionNotSingleValue");
			}
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x00020EF4 File Offset: 0x0001F0F4
		internal static string MetadataBinder_CastOrIsOfExpressionWithWrongNumberOfOperands(object p0)
		{
			return TextRes.GetString("MetadataBinder_CastOrIsOfExpressionWithWrongNumberOfOperands", new object[] { p0 });
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000D9B RID: 3483 RVA: 0x00020F0A File Offset: 0x0001F10A
		internal static string MetadataBinder_CastOrIsOfFunctionWithoutATypeArgument
		{
			get
			{
				return TextRes.GetString("MetadataBinder_CastOrIsOfFunctionWithoutATypeArgument");
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000D9C RID: 3484 RVA: 0x00020F16 File Offset: 0x0001F116
		internal static string MetadataBinder_CastOrIsOfCollectionsNotSupported
		{
			get
			{
				return TextRes.GetString("MetadataBinder_CastOrIsOfCollectionsNotSupported");
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000D9D RID: 3485 RVA: 0x00020F22 File Offset: 0x0001F122
		internal static string MetadataBinder_CollectionOpenPropertiesNotSupportedInThisRelease
		{
			get
			{
				return TextRes.GetString("MetadataBinder_CollectionOpenPropertiesNotSupportedInThisRelease");
			}
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x00020F2E File Offset: 0x0001F12E
		internal static string MetadataBinder_IllegalSegmentType(object p0)
		{
			return TextRes.GetString("MetadataBinder_IllegalSegmentType", new object[] { p0 });
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x00020F44 File Offset: 0x0001F144
		internal static string MetadataBinder_QueryOptionNotApplicable(object p0)
		{
			return TextRes.GetString("MetadataBinder_QueryOptionNotApplicable", new object[] { p0 });
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x00020F5A File Offset: 0x0001F15A
		internal static string StringItemShouldBeQuoted(object p0)
		{
			return TextRes.GetString("StringItemShouldBeQuoted", new object[] { p0 });
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x00020F70 File Offset: 0x0001F170
		internal static string StreamItemInvalidPrimitiveKind(object p0)
		{
			return TextRes.GetString("StreamItemInvalidPrimitiveKind", new object[] { p0 });
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x00020F86 File Offset: 0x0001F186
		internal static string ApplyBinder_AggregateExpressionIncompatibleTypeForMethod(object p0, object p1)
		{
			return TextRes.GetString("ApplyBinder_AggregateExpressionIncompatibleTypeForMethod", new object[] { p0, p1 });
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x00020FA0 File Offset: 0x0001F1A0
		internal static string ApplyBinder_UnsupportedAggregateMethod(object p0)
		{
			return TextRes.GetString("ApplyBinder_UnsupportedAggregateMethod", new object[] { p0 });
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x00020FB6 File Offset: 0x0001F1B6
		internal static string ApplyBinder_UnsupportedAggregateKind(object p0)
		{
			return TextRes.GetString("ApplyBinder_UnsupportedAggregateKind", new object[] { p0 });
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x00020FCC File Offset: 0x0001F1CC
		internal static string ApplyBinder_AggregateExpressionNotSingleValue(object p0)
		{
			return TextRes.GetString("ApplyBinder_AggregateExpressionNotSingleValue", new object[] { p0 });
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x00020FE2 File Offset: 0x0001F1E2
		internal static string ApplyBinder_GroupByPropertyNotPropertyAccessValue(object p0)
		{
			return TextRes.GetString("ApplyBinder_GroupByPropertyNotPropertyAccessValue", new object[] { p0 });
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x00020FF8 File Offset: 0x0001F1F8
		internal static string ApplyBinder_UnsupportedType(object p0)
		{
			return TextRes.GetString("ApplyBinder_UnsupportedType", new object[] { p0 });
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x0002100E File Offset: 0x0001F20E
		internal static string ApplyBinder_UnsupportedGroupByChild(object p0)
		{
			return TextRes.GetString("ApplyBinder_UnsupportedGroupByChild", new object[] { p0 });
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000DA9 RID: 3497 RVA: 0x00021024 File Offset: 0x0001F224
		internal static string AggregateTransformationNode_UnsupportedAggregateExpressions
		{
			get
			{
				return TextRes.GetString("AggregateTransformationNode_UnsupportedAggregateExpressions");
			}
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x00021030 File Offset: 0x0001F230
		internal static string FunctionCallBinder_CannotFindASuitableOverload(object p0, object p1)
		{
			return TextRes.GetString("FunctionCallBinder_CannotFindASuitableOverload", new object[] { p0, p1 });
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x0002104A File Offset: 0x0001F24A
		internal static string FunctionCallBinder_UriFunctionMustHaveHaveNullParent(object p0)
		{
			return TextRes.GetString("FunctionCallBinder_UriFunctionMustHaveHaveNullParent", new object[] { p0 });
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x00021060 File Offset: 0x0001F260
		internal static string FunctionCallBinder_CallingFunctionOnOpenProperty(object p0)
		{
			return TextRes.GetString("FunctionCallBinder_CallingFunctionOnOpenProperty", new object[] { p0 });
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000DAD RID: 3501 RVA: 0x00021076 File Offset: 0x0001F276
		internal static string FunctionCallParser_DuplicateParameterOrEntityKeyName
		{
			get
			{
				return TextRes.GetString("FunctionCallParser_DuplicateParameterOrEntityKeyName");
			}
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x00021082 File Offset: 0x0001F282
		internal static string ODataUriParser_InvalidCount(object p0)
		{
			return TextRes.GetString("ODataUriParser_InvalidCount", new object[] { p0 });
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x00021098 File Offset: 0x0001F298
		internal static string CastBinder_ChildTypeIsNotEntity(object p0)
		{
			return TextRes.GetString("CastBinder_ChildTypeIsNotEntity", new object[] { p0 });
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000DB0 RID: 3504 RVA: 0x000210AE File Offset: 0x0001F2AE
		internal static string CastBinder_EnumOnlyCastToOrFromString
		{
			get
			{
				return TextRes.GetString("CastBinder_EnumOnlyCastToOrFromString");
			}
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x000210BA File Offset: 0x0001F2BA
		internal static string Binder_IsNotValidEnumConstant(object p0)
		{
			return TextRes.GetString("Binder_IsNotValidEnumConstant", new object[] { p0 });
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x000210D0 File Offset: 0x0001F2D0
		internal static string BatchReferenceSegment_InvalidContentID(object p0)
		{
			return TextRes.GetString("BatchReferenceSegment_InvalidContentID", new object[] { p0 });
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x000210E6 File Offset: 0x0001F2E6
		internal static string SelectExpandBinder_UnknownPropertyType(object p0)
		{
			return TextRes.GetString("SelectExpandBinder_UnknownPropertyType", new object[] { p0 });
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x000210FC File Offset: 0x0001F2FC
		internal static string SelectExpandBinder_InvalidIdentifierAfterWildcard(object p0)
		{
			return TextRes.GetString("SelectExpandBinder_InvalidIdentifierAfterWildcard", new object[] { p0 });
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x00021112 File Offset: 0x0001F312
		internal static string SelectExpandBinder_InvalidQueryOptionNestedSelection(object p0)
		{
			return TextRes.GetString("SelectExpandBinder_InvalidQueryOptionNestedSelection", new object[] { p0 });
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x00021128 File Offset: 0x0001F328
		internal static string SelectExpandBinder_SystemTokenInSelect(object p0)
		{
			return TextRes.GetString("SelectExpandBinder_SystemTokenInSelect", new object[] { p0 });
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x0002113E File Offset: 0x0001F33E
		internal static string SelectionItemBinder_NoExpandForSelectedProperty(object p0)
		{
			return TextRes.GetString("SelectionItemBinder_NoExpandForSelectedProperty", new object[] { p0 });
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x00021154 File Offset: 0x0001F354
		internal static string SelectExpandPathBinder_FollowNonTypeSegment(object p0)
		{
			return TextRes.GetString("SelectExpandPathBinder_FollowNonTypeSegment", new object[] { p0 });
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000DB9 RID: 3513 RVA: 0x0002116A File Offset: 0x0001F36A
		internal static string SelectBinder_MultiLevelPathInSelect
		{
			get
			{
				return TextRes.GetString("SelectBinder_MultiLevelPathInSelect");
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000DBA RID: 3514 RVA: 0x00021176 File Offset: 0x0001F376
		internal static string ExpandItemBinder_TraversingANonNormalizedTree
		{
			get
			{
				return TextRes.GetString("ExpandItemBinder_TraversingANonNormalizedTree");
			}
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x00021182 File Offset: 0x0001F382
		internal static string ExpandItemBinder_CannotFindType(object p0)
		{
			return TextRes.GetString("ExpandItemBinder_CannotFindType", new object[] { p0 });
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x00021198 File Offset: 0x0001F398
		internal static string ExpandItemBinder_PropertyIsNotANavigationPropertyOrComplexProperty(object p0, object p1)
		{
			return TextRes.GetString("ExpandItemBinder_PropertyIsNotANavigationPropertyOrComplexProperty", new object[] { p0, p1 });
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000DBD RID: 3517 RVA: 0x000211B2 File Offset: 0x0001F3B2
		internal static string ExpandItemBinder_TypeSegmentNotFollowedByPath
		{
			get
			{
				return TextRes.GetString("ExpandItemBinder_TypeSegmentNotFollowedByPath");
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000DBE RID: 3518 RVA: 0x000211BE File Offset: 0x0001F3BE
		internal static string ExpandItemBinder_PathTooDeep
		{
			get
			{
				return TextRes.GetString("ExpandItemBinder_PathTooDeep");
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000DBF RID: 3519 RVA: 0x000211CA File Offset: 0x0001F3CA
		internal static string ExpandItemBinder_TraversingMultipleNavPropsInTheSamePath
		{
			get
			{
				return TextRes.GetString("ExpandItemBinder_TraversingMultipleNavPropsInTheSamePath");
			}
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x000211D6 File Offset: 0x0001F3D6
		internal static string ExpandItemBinder_LevelsNotAllowedOnIncompatibleRelatedType(object p0, object p1, object p2)
		{
			return TextRes.GetString("ExpandItemBinder_LevelsNotAllowedOnIncompatibleRelatedType", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x000211F4 File Offset: 0x0001F3F4
		internal static string ExpandItemBinder_InvaidSegmentInExpand(object p0)
		{
			return TextRes.GetString("ExpandItemBinder_InvaidSegmentInExpand", new object[] { p0 });
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000DC2 RID: 3522 RVA: 0x0002120A File Offset: 0x0001F40A
		internal static string Nodes_CollectionNavigationNode_MustHaveSingleMultiplicity
		{
			get
			{
				return TextRes.GetString("Nodes_CollectionNavigationNode_MustHaveSingleMultiplicity");
			}
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x00021216 File Offset: 0x0001F416
		internal static string Nodes_NonentityParameterQueryNodeWithEntityType(object p0)
		{
			return TextRes.GetString("Nodes_NonentityParameterQueryNodeWithEntityType", new object[] { p0 });
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000DC4 RID: 3524 RVA: 0x0002122C File Offset: 0x0001F42C
		internal static string Nodes_CollectionNavigationNode_MustHaveManyMultiplicity
		{
			get
			{
				return TextRes.GetString("Nodes_CollectionNavigationNode_MustHaveManyMultiplicity");
			}
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x00021238 File Offset: 0x0001F438
		internal static string Nodes_PropertyAccessShouldBeNonEntityProperty(object p0)
		{
			return TextRes.GetString("Nodes_PropertyAccessShouldBeNonEntityProperty", new object[] { p0 });
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x0002124E File Offset: 0x0001F44E
		internal static string Nodes_PropertyAccessTypeShouldNotBeCollection(object p0)
		{
			return TextRes.GetString("Nodes_PropertyAccessTypeShouldNotBeCollection", new object[] { p0 });
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x00021264 File Offset: 0x0001F464
		internal static string Nodes_PropertyAccessTypeMustBeCollection(object p0)
		{
			return TextRes.GetString("Nodes_PropertyAccessTypeMustBeCollection", new object[] { p0 });
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000DC8 RID: 3528 RVA: 0x0002127A File Offset: 0x0001F47A
		internal static string Nodes_NonStaticEntitySetExpressionsAreNotSupportedInThisRelease
		{
			get
			{
				return TextRes.GetString("Nodes_NonStaticEntitySetExpressionsAreNotSupportedInThisRelease");
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000DC9 RID: 3529 RVA: 0x00021286 File Offset: 0x0001F486
		internal static string Nodes_CollectionFunctionCallNode_ItemTypeMustBePrimitiveOrComplexOrEnum
		{
			get
			{
				return TextRes.GetString("Nodes_CollectionFunctionCallNode_ItemTypeMustBePrimitiveOrComplexOrEnum");
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000DCA RID: 3530 RVA: 0x00021292 File Offset: 0x0001F492
		internal static string Nodes_EntityCollectionFunctionCallNode_ItemTypeMustBeAnEntity
		{
			get
			{
				return TextRes.GetString("Nodes_EntityCollectionFunctionCallNode_ItemTypeMustBeAnEntity");
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000DCB RID: 3531 RVA: 0x0002129E File Offset: 0x0001F49E
		internal static string Nodes_SingleValueFunctionCallNode_ItemTypeMustBePrimitiveOrComplexOrEnum
		{
			get
			{
				return TextRes.GetString("Nodes_SingleValueFunctionCallNode_ItemTypeMustBePrimitiveOrComplexOrEnum");
			}
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x000212AA File Offset: 0x0001F4AA
		internal static string Nodes_InNode_CollectionItemTypeMustBeSameAsSingleItemType(object p0, object p1)
		{
			return TextRes.GetString("Nodes_InNode_CollectionItemTypeMustBeSameAsSingleItemType", new object[] { p0, p1 });
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000DCD RID: 3533 RVA: 0x000212C4 File Offset: 0x0001F4C4
		internal static string ExpandTreeNormalizer_NonPathInPropertyChain
		{
			get
			{
				return TextRes.GetString("ExpandTreeNormalizer_NonPathInPropertyChain");
			}
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x000212D0 File Offset: 0x0001F4D0
		internal static string SelectTreeNormalizer_MultipleSelecTermWithSamePathFound(object p0)
		{
			return TextRes.GetString("SelectTreeNormalizer_MultipleSelecTermWithSamePathFound", new object[] { p0 });
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x000212E6 File Offset: 0x0001F4E6
		internal static string UriExpandParser_TermIsNotValidForStar(object p0)
		{
			return TextRes.GetString("UriExpandParser_TermIsNotValidForStar", new object[] { p0 });
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x000212FC File Offset: 0x0001F4FC
		internal static string UriExpandParser_TermIsNotValidForStarRef(object p0)
		{
			return TextRes.GetString("UriExpandParser_TermIsNotValidForStarRef", new object[] { p0 });
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x00021312 File Offset: 0x0001F512
		internal static string UriExpandParser_ParentStructuredTypeIsNull(object p0)
		{
			return TextRes.GetString("UriExpandParser_ParentStructuredTypeIsNull", new object[] { p0 });
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x00021328 File Offset: 0x0001F528
		internal static string UriExpandParser_TermWithMultipleStarNotAllowed(object p0)
		{
			return TextRes.GetString("UriExpandParser_TermWithMultipleStarNotAllowed", new object[] { p0 });
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x0002133E File Offset: 0x0001F53E
		internal static string UriSelectParser_TermIsNotValid(object p0)
		{
			return TextRes.GetString("UriSelectParser_TermIsNotValid", new object[] { p0 });
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x00021354 File Offset: 0x0001F554
		internal static string UriSelectParser_InvalidTopOption(object p0)
		{
			return TextRes.GetString("UriSelectParser_InvalidTopOption", new object[] { p0 });
		}

		// Token: 0x06000DD5 RID: 3541 RVA: 0x0002136A File Offset: 0x0001F56A
		internal static string UriSelectParser_InvalidSkipOption(object p0)
		{
			return TextRes.GetString("UriSelectParser_InvalidSkipOption", new object[] { p0 });
		}

		// Token: 0x06000DD6 RID: 3542 RVA: 0x00021380 File Offset: 0x0001F580
		internal static string UriSelectParser_InvalidCountOption(object p0)
		{
			return TextRes.GetString("UriSelectParser_InvalidCountOption", new object[] { p0 });
		}

		// Token: 0x06000DD7 RID: 3543 RVA: 0x00021396 File Offset: 0x0001F596
		internal static string UriSelectParser_InvalidLevelsOption(object p0)
		{
			return TextRes.GetString("UriSelectParser_InvalidLevelsOption", new object[] { p0 });
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x000213AC File Offset: 0x0001F5AC
		internal static string UriSelectParser_SystemTokenInSelectExpand(object p0, object p1)
		{
			return TextRes.GetString("UriSelectParser_SystemTokenInSelectExpand", new object[] { p0, p1 });
		}

		// Token: 0x06000DD9 RID: 3545 RVA: 0x000213C6 File Offset: 0x0001F5C6
		internal static string UriParser_MissingExpandOption(object p0)
		{
			return TextRes.GetString("UriParser_MissingExpandOption", new object[] { p0 });
		}

		// Token: 0x06000DDA RID: 3546 RVA: 0x000213DC File Offset: 0x0001F5DC
		internal static string UriParser_MissingSelectOption(object p0)
		{
			return TextRes.GetString("UriParser_MissingSelectOption", new object[] { p0 });
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000DDB RID: 3547 RVA: 0x000213F2 File Offset: 0x0001F5F2
		internal static string UriParser_RelativeUriMustBeRelative
		{
			get
			{
				return TextRes.GetString("UriParser_RelativeUriMustBeRelative");
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000DDC RID: 3548 RVA: 0x000213FE File Offset: 0x0001F5FE
		internal static string UriParser_NeedServiceRootForThisOverload
		{
			get
			{
				return TextRes.GetString("UriParser_NeedServiceRootForThisOverload");
			}
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x0002140A File Offset: 0x0001F60A
		internal static string UriParser_UriMustBeAbsolute(object p0)
		{
			return TextRes.GetString("UriParser_UriMustBeAbsolute", new object[] { p0 });
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000DDE RID: 3550 RVA: 0x00021420 File Offset: 0x0001F620
		internal static string UriParser_NegativeLimit
		{
			get
			{
				return TextRes.GetString("UriParser_NegativeLimit");
			}
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x0002142C File Offset: 0x0001F62C
		internal static string UriParser_ExpandCountExceeded(object p0, object p1)
		{
			return TextRes.GetString("UriParser_ExpandCountExceeded", new object[] { p0, p1 });
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x00021446 File Offset: 0x0001F646
		internal static string UriParser_ExpandDepthExceeded(object p0, object p1)
		{
			return TextRes.GetString("UriParser_ExpandDepthExceeded", new object[] { p0, p1 });
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x00021460 File Offset: 0x0001F660
		internal static string UriParser_TypeInvalidForSelectExpand(object p0)
		{
			return TextRes.GetString("UriParser_TypeInvalidForSelectExpand", new object[] { p0 });
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x00021476 File Offset: 0x0001F676
		internal static string UriParser_ContextHandlerCanNotBeNull(object p0)
		{
			return TextRes.GetString("UriParser_ContextHandlerCanNotBeNull", new object[] { p0 });
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x0002148C File Offset: 0x0001F68C
		internal static string UriParserMetadata_MultipleMatchingPropertiesFound(object p0, object p1)
		{
			return TextRes.GetString("UriParserMetadata_MultipleMatchingPropertiesFound", new object[] { p0, p1 });
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x000214A6 File Offset: 0x0001F6A6
		internal static string UriParserMetadata_MultipleMatchingNavigationSourcesFound(object p0)
		{
			return TextRes.GetString("UriParserMetadata_MultipleMatchingNavigationSourcesFound", new object[] { p0 });
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x000214BC File Offset: 0x0001F6BC
		internal static string UriParserMetadata_MultipleMatchingTypesFound(object p0)
		{
			return TextRes.GetString("UriParserMetadata_MultipleMatchingTypesFound", new object[] { p0 });
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x000214D2 File Offset: 0x0001F6D2
		internal static string UriParserMetadata_MultipleMatchingKeysFound(object p0)
		{
			return TextRes.GetString("UriParserMetadata_MultipleMatchingKeysFound", new object[] { p0 });
		}

		// Token: 0x06000DE7 RID: 3559 RVA: 0x000214E8 File Offset: 0x0001F6E8
		internal static string UriParserMetadata_MultipleMatchingParametersFound(object p0)
		{
			return TextRes.GetString("UriParserMetadata_MultipleMatchingParametersFound", new object[] { p0 });
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x000214FE File Offset: 0x0001F6FE
		internal static string PathParser_EntityReferenceNotSupported(object p0)
		{
			return TextRes.GetString("PathParser_EntityReferenceNotSupported", new object[] { p0 });
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000DE9 RID: 3561 RVA: 0x00021514 File Offset: 0x0001F714
		internal static string PathParser_CannotUseValueOnCollection
		{
			get
			{
				return TextRes.GetString("PathParser_CannotUseValueOnCollection");
			}
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x00021520 File Offset: 0x0001F720
		internal static string PathParser_TypeMustBeRelatedToSet(object p0, object p1, object p2)
		{
			return TextRes.GetString("PathParser_TypeMustBeRelatedToSet", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x0002153E File Offset: 0x0001F73E
		internal static string PathParser_TypeCastOnlyAllowedAfterStructuralCollection(object p0)
		{
			return TextRes.GetString("PathParser_TypeCastOnlyAllowedAfterStructuralCollection", new object[] { p0 });
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x00021554 File Offset: 0x0001F754
		internal static string PathParser_TypeCastOnlyAllowedInDerivedTypeConstraint(object p0, object p1, object p2)
		{
			return TextRes.GetString("PathParser_TypeCastOnlyAllowedInDerivedTypeConstraint", new object[] { p0, p1, p2 });
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000DED RID: 3565 RVA: 0x00021572 File Offset: 0x0001F772
		internal static string ODataResourceSet_MustNotContainBothNextPageLinkAndDeltaLink
		{
			get
			{
				return TextRes.GetString("ODataResourceSet_MustNotContainBothNextPageLinkAndDeltaLink");
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000DEE RID: 3566 RVA: 0x0002157E File Offset: 0x0001F77E
		internal static string ODataExpandPath_OnlyLastSegmentMustBeNavigationProperty
		{
			get
			{
				return TextRes.GetString("ODataExpandPath_OnlyLastSegmentMustBeNavigationProperty");
			}
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x0002158A File Offset: 0x0001F78A
		internal static string ODataExpandPath_InvalidExpandPathSegment(object p0)
		{
			return TextRes.GetString("ODataExpandPath_InvalidExpandPathSegment", new object[] { p0 });
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000DF0 RID: 3568 RVA: 0x000215A0 File Offset: 0x0001F7A0
		internal static string ODataSelectPath_CannotOnlyHaveTypeSegment
		{
			get
			{
				return TextRes.GetString("ODataSelectPath_CannotOnlyHaveTypeSegment");
			}
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x000215AC File Offset: 0x0001F7AC
		internal static string ODataSelectPath_InvalidSelectPathSegmentType(object p0)
		{
			return TextRes.GetString("ODataSelectPath_InvalidSelectPathSegmentType", new object[] { p0 });
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000DF2 RID: 3570 RVA: 0x000215C2 File Offset: 0x0001F7C2
		internal static string ODataSelectPath_OperationSegmentCanOnlyBeLastSegment
		{
			get
			{
				return TextRes.GetString("ODataSelectPath_OperationSegmentCanOnlyBeLastSegment");
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000DF3 RID: 3571 RVA: 0x000215CE File Offset: 0x0001F7CE
		internal static string ODataSelectPath_NavPropSegmentCanOnlyBeLastSegment
		{
			get
			{
				return TextRes.GetString("ODataSelectPath_NavPropSegmentCanOnlyBeLastSegment");
			}
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x000215DA File Offset: 0x0001F7DA
		internal static string RequestUriProcessor_TargetEntitySetNotFound(object p0)
		{
			return TextRes.GetString("RequestUriProcessor_TargetEntitySetNotFound", new object[] { p0 });
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x000215F0 File Offset: 0x0001F7F0
		internal static string RequestUriProcessor_FoundInvalidFunctionImport(object p0)
		{
			return TextRes.GetString("RequestUriProcessor_FoundInvalidFunctionImport", new object[] { p0 });
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000DF6 RID: 3574 RVA: 0x00021606 File Offset: 0x0001F806
		internal static string OperationSegment_ReturnTypeForMultipleOverloads
		{
			get
			{
				return TextRes.GetString("OperationSegment_ReturnTypeForMultipleOverloads");
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000DF7 RID: 3575 RVA: 0x00021612 File Offset: 0x0001F812
		internal static string OperationSegment_CannotReturnNull
		{
			get
			{
				return TextRes.GetString("OperationSegment_CannotReturnNull");
			}
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x0002161E File Offset: 0x0001F81E
		internal static string FunctionOverloadResolver_NoSingleMatchFound(object p0, object p1)
		{
			return TextRes.GetString("FunctionOverloadResolver_NoSingleMatchFound", new object[] { p0, p1 });
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x00021638 File Offset: 0x0001F838
		internal static string FunctionOverloadResolver_MultipleActionOverloads(object p0)
		{
			return TextRes.GetString("FunctionOverloadResolver_MultipleActionOverloads", new object[] { p0 });
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x0002164E File Offset: 0x0001F84E
		internal static string FunctionOverloadResolver_MultipleActionImportOverloads(object p0)
		{
			return TextRes.GetString("FunctionOverloadResolver_MultipleActionImportOverloads", new object[] { p0 });
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x00021664 File Offset: 0x0001F864
		internal static string FunctionOverloadResolver_MultipleOperationImportOverloads(object p0)
		{
			return TextRes.GetString("FunctionOverloadResolver_MultipleOperationImportOverloads", new object[] { p0 });
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x0002167A File Offset: 0x0001F87A
		internal static string FunctionOverloadResolver_MultipleOperationOverloads(object p0)
		{
			return TextRes.GetString("FunctionOverloadResolver_MultipleOperationOverloads", new object[] { p0 });
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x00021690 File Offset: 0x0001F890
		internal static string FunctionOverloadResolver_FoundInvalidOperation(object p0)
		{
			return TextRes.GetString("FunctionOverloadResolver_FoundInvalidOperation", new object[] { p0 });
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x000216A6 File Offset: 0x0001F8A6
		internal static string FunctionOverloadResolver_FoundInvalidOperationImport(object p0)
		{
			return TextRes.GetString("FunctionOverloadResolver_FoundInvalidOperationImport", new object[] { p0 });
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x000216BC File Offset: 0x0001F8BC
		internal static string CustomUriFunctions_AddCustomUriFunction_BuiltInExistsNotAddingAsOverload(object p0)
		{
			return TextRes.GetString("CustomUriFunctions_AddCustomUriFunction_BuiltInExistsNotAddingAsOverload", new object[] { p0 });
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x000216D2 File Offset: 0x0001F8D2
		internal static string CustomUriFunctions_AddCustomUriFunction_BuiltInExistsFullSignature(object p0)
		{
			return TextRes.GetString("CustomUriFunctions_AddCustomUriFunction_BuiltInExistsFullSignature", new object[] { p0 });
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x000216E8 File Offset: 0x0001F8E8
		internal static string CustomUriFunctions_AddCustomUriFunction_CustomFunctionOverloadExists(object p0)
		{
			return TextRes.GetString("CustomUriFunctions_AddCustomUriFunction_CustomFunctionOverloadExists", new object[] { p0 });
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x000216FE File Offset: 0x0001F8FE
		internal static string RequestUriProcessor_InvalidValueForEntitySegment(object p0)
		{
			return TextRes.GetString("RequestUriProcessor_InvalidValueForEntitySegment", new object[] { p0 });
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x00021714 File Offset: 0x0001F914
		internal static string RequestUriProcessor_InvalidValueForKeySegment(object p0)
		{
			return TextRes.GetString("RequestUriProcessor_InvalidValueForKeySegment", new object[] { p0 });
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x0002172A File Offset: 0x0001F92A
		internal static string RequestUriProcessor_CannotApplyFilterOnSingleEntities(object p0)
		{
			return TextRes.GetString("RequestUriProcessor_CannotApplyFilterOnSingleEntities", new object[] { p0 });
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x00021740 File Offset: 0x0001F940
		internal static string RequestUriProcessor_CannotApplyEachOnSingleEntities(object p0)
		{
			return TextRes.GetString("RequestUriProcessor_CannotApplyEachOnSingleEntities", new object[] { p0 });
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000E06 RID: 3590 RVA: 0x00021756 File Offset: 0x0001F956
		internal static string RequestUriProcessor_FilterPathSegmentSyntaxError
		{
			get
			{
				return TextRes.GetString("RequestUriProcessor_FilterPathSegmentSyntaxError");
			}
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x00021762 File Offset: 0x0001F962
		internal static string RequestUriProcessor_NoNavigationSourceFound(object p0)
		{
			return TextRes.GetString("RequestUriProcessor_NoNavigationSourceFound", new object[] { p0 });
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000E08 RID: 3592 RVA: 0x00021778 File Offset: 0x0001F978
		internal static string RequestUriProcessor_OnlySingleOperationCanFollowEachPathSegment
		{
			get
			{
				return TextRes.GetString("RequestUriProcessor_OnlySingleOperationCanFollowEachPathSegment");
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000E09 RID: 3593 RVA: 0x00021784 File Offset: 0x0001F984
		internal static string RequestUriProcessor_EmptySegmentInRequestUrl
		{
			get
			{
				return TextRes.GetString("RequestUriProcessor_EmptySegmentInRequestUrl");
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000E0A RID: 3594 RVA: 0x00021790 File Offset: 0x0001F990
		internal static string RequestUriProcessor_SyntaxError
		{
			get
			{
				return TextRes.GetString("RequestUriProcessor_SyntaxError");
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000E0B RID: 3595 RVA: 0x0002179C File Offset: 0x0001F99C
		internal static string RequestUriProcessor_CountOnRoot
		{
			get
			{
				return TextRes.GetString("RequestUriProcessor_CountOnRoot");
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000E0C RID: 3596 RVA: 0x000217A8 File Offset: 0x0001F9A8
		internal static string RequestUriProcessor_FilterOnRoot
		{
			get
			{
				return TextRes.GetString("RequestUriProcessor_FilterOnRoot");
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000E0D RID: 3597 RVA: 0x000217B4 File Offset: 0x0001F9B4
		internal static string RequestUriProcessor_EachOnRoot
		{
			get
			{
				return TextRes.GetString("RequestUriProcessor_EachOnRoot");
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000E0E RID: 3598 RVA: 0x000217C0 File Offset: 0x0001F9C0
		internal static string RequestUriProcessor_RefOnRoot
		{
			get
			{
				return TextRes.GetString("RequestUriProcessor_RefOnRoot");
			}
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x000217CC File Offset: 0x0001F9CC
		internal static string RequestUriProcessor_MustBeLeafSegment(object p0)
		{
			return TextRes.GetString("RequestUriProcessor_MustBeLeafSegment", new object[] { p0 });
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x000217E2 File Offset: 0x0001F9E2
		internal static string RequestUriProcessor_LinkSegmentMustBeFollowedByEntitySegment(object p0, object p1)
		{
			return TextRes.GetString("RequestUriProcessor_LinkSegmentMustBeFollowedByEntitySegment", new object[] { p0, p1 });
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x000217FC File Offset: 0x0001F9FC
		internal static string RequestUriProcessor_MissingSegmentAfterLink(object p0)
		{
			return TextRes.GetString("RequestUriProcessor_MissingSegmentAfterLink", new object[] { p0 });
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x00021812 File Offset: 0x0001FA12
		internal static string RequestUriProcessor_CountNotSupported(object p0)
		{
			return TextRes.GetString("RequestUriProcessor_CountNotSupported", new object[] { p0 });
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x00021828 File Offset: 0x0001FA28
		internal static string RequestUriProcessor_CannotQueryCollections(object p0)
		{
			return TextRes.GetString("RequestUriProcessor_CannotQueryCollections", new object[] { p0 });
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x0002183E File Offset: 0x0001FA3E
		internal static string RequestUriProcessor_SegmentDoesNotSupportKeyPredicates(object p0)
		{
			return TextRes.GetString("RequestUriProcessor_SegmentDoesNotSupportKeyPredicates", new object[] { p0 });
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x00021854 File Offset: 0x0001FA54
		internal static string RequestUriProcessor_ValueSegmentAfterScalarPropertySegment(object p0, object p1)
		{
			return TextRes.GetString("RequestUriProcessor_ValueSegmentAfterScalarPropertySegment", new object[] { p0, p1 });
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x0002186E File Offset: 0x0001FA6E
		internal static string RequestUriProcessor_InvalidTypeIdentifier_UnrelatedType(object p0, object p1)
		{
			return TextRes.GetString("RequestUriProcessor_InvalidTypeIdentifier_UnrelatedType", new object[] { p0, p1 });
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x00021888 File Offset: 0x0001FA88
		internal static string OpenNavigationPropertiesNotSupportedOnOpenTypes(object p0)
		{
			return TextRes.GetString("OpenNavigationPropertiesNotSupportedOnOpenTypes", new object[] { p0 });
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000E18 RID: 3608 RVA: 0x0002189E File Offset: 0x0001FA9E
		internal static string BadRequest_ResourceCanBeCrossReferencedOnlyForBindOperation
		{
			get
			{
				return TextRes.GetString("BadRequest_ResourceCanBeCrossReferencedOnlyForBindOperation");
			}
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x000218AA File Offset: 0x0001FAAA
		internal static string DataServiceConfiguration_ResponseVersionIsBiggerThanProtocolVersion(object p0, object p1)
		{
			return TextRes.GetString("DataServiceConfiguration_ResponseVersionIsBiggerThanProtocolVersion", new object[] { p0, p1 });
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x000218C4 File Offset: 0x0001FAC4
		internal static string BadRequest_KeyCountMismatch(object p0)
		{
			return TextRes.GetString("BadRequest_KeyCountMismatch", new object[] { p0 });
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000E1B RID: 3611 RVA: 0x000218DA File Offset: 0x0001FADA
		internal static string RequestUriProcessor_KeysMustBeNamed
		{
			get
			{
				return TextRes.GetString("RequestUriProcessor_KeysMustBeNamed");
			}
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x000218E6 File Offset: 0x0001FAE6
		internal static string RequestUriProcessor_ResourceNotFound(object p0)
		{
			return TextRes.GetString("RequestUriProcessor_ResourceNotFound", new object[] { p0 });
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x000218FC File Offset: 0x0001FAFC
		internal static string RequestUriProcessor_BatchedActionOnEntityCreatedInSameChangeset(object p0)
		{
			return TextRes.GetString("RequestUriProcessor_BatchedActionOnEntityCreatedInSameChangeset", new object[] { p0 });
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000E1E RID: 3614 RVA: 0x00021912 File Offset: 0x0001FB12
		internal static string RequestUriProcessor_Forbidden
		{
			get
			{
				return TextRes.GetString("RequestUriProcessor_Forbidden");
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000E1F RID: 3615 RVA: 0x0002191E File Offset: 0x0001FB1E
		internal static string RequestUriProcessor_OperationSegmentBoundToANonEntityType
		{
			get
			{
				return TextRes.GetString("RequestUriProcessor_OperationSegmentBoundToANonEntityType");
			}
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x0002192A File Offset: 0x0001FB2A
		internal static string RequestUriProcessor_NoBoundEscapeFunctionSupported(object p0)
		{
			return TextRes.GetString("RequestUriProcessor_NoBoundEscapeFunctionSupported", new object[] { p0 });
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x00021940 File Offset: 0x0001FB40
		internal static string RequestUriProcessor_EscapeFunctionMustHaveOneStringParameter(object p0)
		{
			return TextRes.GetString("RequestUriProcessor_EscapeFunctionMustHaveOneStringParameter", new object[] { p0 });
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x00021956 File Offset: 0x0001FB56
		internal static string General_InternalError(object p0)
		{
			return TextRes.GetString("General_InternalError", new object[] { p0 });
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x0002196C File Offset: 0x0001FB6C
		internal static string ExceptionUtils_CheckIntegerNotNegative(object p0)
		{
			return TextRes.GetString("ExceptionUtils_CheckIntegerNotNegative", new object[] { p0 });
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x00021982 File Offset: 0x0001FB82
		internal static string ExceptionUtils_CheckIntegerPositive(object p0)
		{
			return TextRes.GetString("ExceptionUtils_CheckIntegerPositive", new object[] { p0 });
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x00021998 File Offset: 0x0001FB98
		internal static string ExceptionUtils_CheckLongPositive(object p0)
		{
			return TextRes.GetString("ExceptionUtils_CheckLongPositive", new object[] { p0 });
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000E26 RID: 3622 RVA: 0x000219AE File Offset: 0x0001FBAE
		internal static string ExceptionUtils_ArgumentStringNullOrEmpty
		{
			get
			{
				return TextRes.GetString("ExceptionUtils_ArgumentStringNullOrEmpty");
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000E27 RID: 3623 RVA: 0x000219BA File Offset: 0x0001FBBA
		internal static string ExpressionToken_OnlyRefAllowWithStarInExpand
		{
			get
			{
				return TextRes.GetString("ExpressionToken_OnlyRefAllowWithStarInExpand");
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000E28 RID: 3624 RVA: 0x000219C6 File Offset: 0x0001FBC6
		internal static string ExpressionToken_NoPropAllowedAfterRef
		{
			get
			{
				return TextRes.GetString("ExpressionToken_NoPropAllowedAfterRef");
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000E29 RID: 3625 RVA: 0x000219D2 File Offset: 0x0001FBD2
		internal static string ExpressionToken_NoSegmentAllowedBeforeStarInExpand
		{
			get
			{
				return TextRes.GetString("ExpressionToken_NoSegmentAllowedBeforeStarInExpand");
			}
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x000219DE File Offset: 0x0001FBDE
		internal static string ExpressionToken_IdentifierExpected(object p0)
		{
			return TextRes.GetString("ExpressionToken_IdentifierExpected", new object[] { p0 });
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x000219F4 File Offset: 0x0001FBF4
		internal static string ExpressionLexer_UnterminatedStringLiteral(object p0, object p1)
		{
			return TextRes.GetString("ExpressionLexer_UnterminatedStringLiteral", new object[] { p0, p1 });
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x00021A0E File Offset: 0x0001FC0E
		internal static string ExpressionLexer_InvalidCharacter(object p0, object p1, object p2)
		{
			return TextRes.GetString("ExpressionLexer_InvalidCharacter", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x00021A2C File Offset: 0x0001FC2C
		internal static string ExpressionLexer_SyntaxError(object p0, object p1)
		{
			return TextRes.GetString("ExpressionLexer_SyntaxError", new object[] { p0, p1 });
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x00021A46 File Offset: 0x0001FC46
		internal static string ExpressionLexer_UnterminatedLiteral(object p0, object p1)
		{
			return TextRes.GetString("ExpressionLexer_UnterminatedLiteral", new object[] { p0, p1 });
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x00021A60 File Offset: 0x0001FC60
		internal static string ExpressionLexer_DigitExpected(object p0, object p1)
		{
			return TextRes.GetString("ExpressionLexer_DigitExpected", new object[] { p0, p1 });
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000E30 RID: 3632 RVA: 0x00021A7A File Offset: 0x0001FC7A
		internal static string ExpressionLexer_UnbalancedBracketExpression
		{
			get
			{
				return TextRes.GetString("ExpressionLexer_UnbalancedBracketExpression");
			}
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x00021A86 File Offset: 0x0001FC86
		internal static string ExpressionLexer_InvalidNumericString(object p0)
		{
			return TextRes.GetString("ExpressionLexer_InvalidNumericString", new object[] { p0 });
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x00021A9C File Offset: 0x0001FC9C
		internal static string ExpressionLexer_InvalidEscapeSequence(object p0, object p1, object p2)
		{
			return TextRes.GetString("ExpressionLexer_InvalidEscapeSequence", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x00021ABA File Offset: 0x0001FCBA
		internal static string UriQueryExpressionParser_UnrecognizedLiteral(object p0, object p1, object p2, object p3)
		{
			return TextRes.GetString("UriQueryExpressionParser_UnrecognizedLiteral", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x00021ADC File Offset: 0x0001FCDC
		internal static string UriQueryExpressionParser_UnrecognizedLiteralWithReason(object p0, object p1, object p2, object p3, object p4)
		{
			return TextRes.GetString("UriQueryExpressionParser_UnrecognizedLiteralWithReason", new object[] { p0, p1, p2, p3, p4 });
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x00021B03 File Offset: 0x0001FD03
		internal static string UriPrimitiveTypeParsers_FailedToParseTextToPrimitiveValue(object p0, object p1)
		{
			return TextRes.GetString("UriPrimitiveTypeParsers_FailedToParseTextToPrimitiveValue", new object[] { p0, p1 });
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000E36 RID: 3638 RVA: 0x00021B1D File Offset: 0x0001FD1D
		internal static string UriPrimitiveTypeParsers_FailedToParseStringToGeography
		{
			get
			{
				return TextRes.GetString("UriPrimitiveTypeParsers_FailedToParseStringToGeography");
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000E37 RID: 3639 RVA: 0x00021B29 File Offset: 0x0001FD29
		internal static string UriCustomTypeParsers_AddCustomUriTypeParserAlreadyExists
		{
			get
			{
				return TextRes.GetString("UriCustomTypeParsers_AddCustomUriTypeParserAlreadyExists");
			}
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x00021B35 File Offset: 0x0001FD35
		internal static string UriCustomTypeParsers_AddCustomUriTypeParserEdmTypeExists(object p0)
		{
			return TextRes.GetString("UriCustomTypeParsers_AddCustomUriTypeParserEdmTypeExists", new object[] { p0 });
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x00021B4B File Offset: 0x0001FD4B
		internal static string UriParserHelper_InvalidPrefixLiteral(object p0)
		{
			return TextRes.GetString("UriParserHelper_InvalidPrefixLiteral", new object[] { p0 });
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x00021B61 File Offset: 0x0001FD61
		internal static string CustomUriTypePrefixLiterals_AddCustomUriTypePrefixLiteralAlreadyExists(object p0)
		{
			return TextRes.GetString("CustomUriTypePrefixLiterals_AddCustomUriTypePrefixLiteralAlreadyExists", new object[] { p0 });
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x00021B77 File Offset: 0x0001FD77
		internal static string ValueParser_InvalidDuration(object p0)
		{
			return TextRes.GetString("ValueParser_InvalidDuration", new object[] { p0 });
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x00021B8D File Offset: 0x0001FD8D
		internal static string PlatformHelper_DateTimeOffsetMustContainTimeZone(object p0)
		{
			return TextRes.GetString("PlatformHelper_DateTimeOffsetMustContainTimeZone", new object[] { p0 });
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x00021BA3 File Offset: 0x0001FDA3
		internal static string JsonReader_UnexpectedComma(object p0)
		{
			return TextRes.GetString("JsonReader_UnexpectedComma", new object[] { p0 });
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x00021BB9 File Offset: 0x0001FDB9
		internal static string JsonReader_ArrayClosureMismatch(object p0, object p1, object p2)
		{
			return TextRes.GetString("JsonReader_ArrayClosureMismatch", new object[] { p0, p1, p2 });
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000E3F RID: 3647 RVA: 0x00021BD7 File Offset: 0x0001FDD7
		internal static string JsonReader_MultipleTopLevelValues
		{
			get
			{
				return TextRes.GetString("JsonReader_MultipleTopLevelValues");
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000E40 RID: 3648 RVA: 0x00021BE3 File Offset: 0x0001FDE3
		internal static string JsonReader_EndOfInputWithOpenScope
		{
			get
			{
				return TextRes.GetString("JsonReader_EndOfInputWithOpenScope");
			}
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x00021BEF File Offset: 0x0001FDEF
		internal static string JsonReader_UnexpectedToken(object p0)
		{
			return TextRes.GetString("JsonReader_UnexpectedToken", new object[] { p0 });
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000E42 RID: 3650 RVA: 0x00021C05 File Offset: 0x0001FE05
		internal static string JsonReader_UnrecognizedToken
		{
			get
			{
				return TextRes.GetString("JsonReader_UnrecognizedToken");
			}
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x00021C11 File Offset: 0x0001FE11
		internal static string JsonReader_MissingColon(object p0)
		{
			return TextRes.GetString("JsonReader_MissingColon", new object[] { p0 });
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x00021C27 File Offset: 0x0001FE27
		internal static string JsonReader_UnrecognizedEscapeSequence(object p0)
		{
			return TextRes.GetString("JsonReader_UnrecognizedEscapeSequence", new object[] { p0 });
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000E45 RID: 3653 RVA: 0x00021C3D File Offset: 0x0001FE3D
		internal static string JsonReader_UnexpectedEndOfString
		{
			get
			{
				return TextRes.GetString("JsonReader_UnexpectedEndOfString");
			}
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x00021C49 File Offset: 0x0001FE49
		internal static string JsonReader_InvalidNumberFormat(object p0)
		{
			return TextRes.GetString("JsonReader_InvalidNumberFormat", new object[] { p0 });
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x00021C5F File Offset: 0x0001FE5F
		internal static string JsonReader_InvalidBinaryFormat(object p0)
		{
			return TextRes.GetString("JsonReader_InvalidBinaryFormat", new object[] { p0 });
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x00021C75 File Offset: 0x0001FE75
		internal static string JsonReader_MissingComma(object p0)
		{
			return TextRes.GetString("JsonReader_MissingComma", new object[] { p0 });
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x00021C8B File Offset: 0x0001FE8B
		internal static string JsonReader_InvalidPropertyNameOrUnexpectedComma(object p0)
		{
			return TextRes.GetString("JsonReader_InvalidPropertyNameOrUnexpectedComma", new object[] { p0 });
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000E4A RID: 3658 RVA: 0x00021CA1 File Offset: 0x0001FEA1
		internal static string JsonReader_MaxBufferReached
		{
			get
			{
				return TextRes.GetString("JsonReader_MaxBufferReached");
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000E4B RID: 3659 RVA: 0x00021CAD File Offset: 0x0001FEAD
		internal static string JsonReader_CannotAccessValueInStreamState
		{
			get
			{
				return TextRes.GetString("JsonReader_CannotAccessValueInStreamState");
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000E4C RID: 3660 RVA: 0x00021CB9 File Offset: 0x0001FEB9
		internal static string JsonReader_CannotCallReadInStreamState
		{
			get
			{
				return TextRes.GetString("JsonReader_CannotCallReadInStreamState");
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000E4D RID: 3661 RVA: 0x00021CC5 File Offset: 0x0001FEC5
		internal static string JsonReader_CannotCreateReadStream
		{
			get
			{
				return TextRes.GetString("JsonReader_CannotCreateReadStream");
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000E4E RID: 3662 RVA: 0x00021CD1 File Offset: 0x0001FED1
		internal static string JsonReader_CannotCreateTextReader
		{
			get
			{
				return TextRes.GetString("JsonReader_CannotCreateTextReader");
			}
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x00021CDD File Offset: 0x0001FEDD
		internal static string JsonReaderExtensions_UnexpectedNodeDetected(object p0, object p1)
		{
			return TextRes.GetString("JsonReaderExtensions_UnexpectedNodeDetected", new object[] { p0, p1 });
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x00021CF7 File Offset: 0x0001FEF7
		internal static string JsonReaderExtensions_UnexpectedNodeDetectedWithPropertyName(object p0, object p1, object p2)
		{
			return TextRes.GetString("JsonReaderExtensions_UnexpectedNodeDetectedWithPropertyName", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x00021D15 File Offset: 0x0001FF15
		internal static string JsonReaderExtensions_CannotReadPropertyValueAsString(object p0, object p1)
		{
			return TextRes.GetString("JsonReaderExtensions_CannotReadPropertyValueAsString", new object[] { p0, p1 });
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x00021D2F File Offset: 0x0001FF2F
		internal static string JsonReaderExtensions_CannotReadValueAsString(object p0)
		{
			return TextRes.GetString("JsonReaderExtensions_CannotReadValueAsString", new object[] { p0 });
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x00021D45 File Offset: 0x0001FF45
		internal static string JsonReaderExtensions_CannotReadValueAsDouble(object p0)
		{
			return TextRes.GetString("JsonReaderExtensions_CannotReadValueAsDouble", new object[] { p0 });
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x00021D5B File Offset: 0x0001FF5B
		internal static string JsonReaderExtensions_UnexpectedInstanceAnnotationName(object p0)
		{
			return TextRes.GetString("JsonReaderExtensions_UnexpectedInstanceAnnotationName", new object[] { p0 });
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x00021D71 File Offset: 0x0001FF71
		internal static string BufferUtils_InvalidBufferOrSize(object p0)
		{
			return TextRes.GetString("BufferUtils_InvalidBufferOrSize", new object[] { p0 });
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x00021D87 File Offset: 0x0001FF87
		internal static string ServiceProviderExtensions_NoServiceRegistered(object p0)
		{
			return TextRes.GetString("ServiceProviderExtensions_NoServiceRegistered", new object[] { p0 });
		}
	}
}
