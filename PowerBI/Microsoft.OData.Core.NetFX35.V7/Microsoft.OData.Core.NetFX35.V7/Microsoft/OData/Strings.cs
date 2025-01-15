using System;

namespace Microsoft.OData
{
	// Token: 0x020000DD RID: 221
	internal static class Strings
	{
		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x0600086B RID: 2155 RVA: 0x000181A1 File Offset: 0x000163A1
		internal static string ExceptionUtils_ArgumentStringEmpty
		{
			get
			{
				return TextRes.GetString("ExceptionUtils_ArgumentStringEmpty");
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x0600086C RID: 2156 RVA: 0x000181AD File Offset: 0x000163AD
		internal static string ODataRequestMessage_AsyncNotAvailable
		{
			get
			{
				return TextRes.GetString("ODataRequestMessage_AsyncNotAvailable");
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x0600086D RID: 2157 RVA: 0x000181B9 File Offset: 0x000163B9
		internal static string ODataRequestMessage_StreamTaskIsNull
		{
			get
			{
				return TextRes.GetString("ODataRequestMessage_StreamTaskIsNull");
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x0600086E RID: 2158 RVA: 0x000181C5 File Offset: 0x000163C5
		internal static string ODataRequestMessage_MessageStreamIsNull
		{
			get
			{
				return TextRes.GetString("ODataRequestMessage_MessageStreamIsNull");
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x0600086F RID: 2159 RVA: 0x000181D1 File Offset: 0x000163D1
		internal static string ODataResponseMessage_AsyncNotAvailable
		{
			get
			{
				return TextRes.GetString("ODataResponseMessage_AsyncNotAvailable");
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000870 RID: 2160 RVA: 0x000181DD File Offset: 0x000163DD
		internal static string ODataResponseMessage_StreamTaskIsNull
		{
			get
			{
				return TextRes.GetString("ODataResponseMessage_StreamTaskIsNull");
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000871 RID: 2161 RVA: 0x000181E9 File Offset: 0x000163E9
		internal static string ODataResponseMessage_MessageStreamIsNull
		{
			get
			{
				return TextRes.GetString("ODataResponseMessage_MessageStreamIsNull");
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000872 RID: 2162 RVA: 0x000181F5 File Offset: 0x000163F5
		internal static string AsyncBufferedStream_WriterDisposedWithoutFlush
		{
			get
			{
				return TextRes.GetString("AsyncBufferedStream_WriterDisposedWithoutFlush");
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000873 RID: 2163 RVA: 0x00018201 File Offset: 0x00016401
		internal static string ODataFormat_AtomFormatObsoleted
		{
			get
			{
				return TextRes.GetString("ODataFormat_AtomFormatObsoleted");
			}
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x0001820D File Offset: 0x0001640D
		internal static string ODataOutputContext_UnsupportedPayloadKindForFormat(object p0, object p1)
		{
			return TextRes.GetString("ODataOutputContext_UnsupportedPayloadKindForFormat", new object[] { p0, p1 });
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x00018227 File Offset: 0x00016427
		internal static string ODataInputContext_UnsupportedPayloadKindForFormat(object p0, object p1)
		{
			return TextRes.GetString("ODataInputContext_UnsupportedPayloadKindForFormat", new object[] { p0, p1 });
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000876 RID: 2166 RVA: 0x00018241 File Offset: 0x00016441
		internal static string ODataOutputContext_MetadataDocumentUriMissing
		{
			get
			{
				return TextRes.GetString("ODataOutputContext_MetadataDocumentUriMissing");
			}
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x0001824D File Offset: 0x0001644D
		internal static string ODataJsonLightSerializer_RelativeUriUsedWithoutMetadataDocumentUriOrMetadata(object p0)
		{
			return TextRes.GetString("ODataJsonLightSerializer_RelativeUriUsedWithoutMetadataDocumentUriOrMetadata", new object[] { p0 });
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x00018263 File Offset: 0x00016463
		internal static string ODataWriter_RelativeUriUsedWithoutBaseUriSpecified(object p0)
		{
			return TextRes.GetString("ODataWriter_RelativeUriUsedWithoutBaseUriSpecified", new object[] { p0 });
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x00018279 File Offset: 0x00016479
		internal static string ODataWriter_StreamPropertiesMustBePropertiesOfODataResource(object p0)
		{
			return TextRes.GetString("ODataWriter_StreamPropertiesMustBePropertiesOfODataResource", new object[] { p0 });
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x0001828F File Offset: 0x0001648F
		internal static string ODataWriterCore_InvalidStateTransition(object p0, object p1)
		{
			return TextRes.GetString("ODataWriterCore_InvalidStateTransition", new object[] { p0, p1 });
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x000182A9 File Offset: 0x000164A9
		internal static string ODataWriterCore_InvalidTransitionFromStart(object p0, object p1)
		{
			return TextRes.GetString("ODataWriterCore_InvalidTransitionFromStart", new object[] { p0, p1 });
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x000182C3 File Offset: 0x000164C3
		internal static string ODataWriterCore_InvalidTransitionFromResource(object p0, object p1)
		{
			return TextRes.GetString("ODataWriterCore_InvalidTransitionFromResource", new object[] { p0, p1 });
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x000182DD File Offset: 0x000164DD
		internal static string ODataWriterCore_InvalidTransitionFromNullResource(object p0, object p1)
		{
			return TextRes.GetString("ODataWriterCore_InvalidTransitionFromNullResource", new object[] { p0, p1 });
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x000182F7 File Offset: 0x000164F7
		internal static string ODataWriterCore_InvalidTransitionFromResourceSet(object p0, object p1)
		{
			return TextRes.GetString("ODataWriterCore_InvalidTransitionFromResourceSet", new object[] { p0, p1 });
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x00018311 File Offset: 0x00016511
		internal static string ODataWriterCore_InvalidTransitionFromExpandedLink(object p0, object p1)
		{
			return TextRes.GetString("ODataWriterCore_InvalidTransitionFromExpandedLink", new object[] { p0, p1 });
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x0001832B File Offset: 0x0001652B
		internal static string ODataWriterCore_InvalidTransitionFromCompleted(object p0, object p1)
		{
			return TextRes.GetString("ODataWriterCore_InvalidTransitionFromCompleted", new object[] { p0, p1 });
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x00018345 File Offset: 0x00016545
		internal static string ODataWriterCore_InvalidTransitionFromError(object p0, object p1)
		{
			return TextRes.GetString("ODataWriterCore_InvalidTransitionFromError", new object[] { p0, p1 });
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x0001835F File Offset: 0x0001655F
		internal static string ODataJsonLightDeltaWriter_InvalidTransitionFromNestedResource(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightDeltaWriter_InvalidTransitionFromNestedResource", new object[] { p0, p1 });
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x00018379 File Offset: 0x00016579
		internal static string ODataJsonLightDeltaWriter_InvalidTransitionToNestedResource(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightDeltaWriter_InvalidTransitionToNestedResource", new object[] { p0, p1 });
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x00018393 File Offset: 0x00016593
		internal static string ODataJsonLightDeltaWriter_WriteStartExpandedResourceSetCalledInInvalidState(object p0)
		{
			return TextRes.GetString("ODataJsonLightDeltaWriter_WriteStartExpandedResourceSetCalledInInvalidState", new object[] { p0 });
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x000183A9 File Offset: 0x000165A9
		internal static string ODataWriterCore_WriteEndCalledInInvalidState(object p0)
		{
			return TextRes.GetString("ODataWriterCore_WriteEndCalledInInvalidState", new object[] { p0 });
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000886 RID: 2182 RVA: 0x000183BF File Offset: 0x000165BF
		internal static string ODataWriterCore_QueryCountInRequest
		{
			get
			{
				return TextRes.GetString("ODataWriterCore_QueryCountInRequest");
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000887 RID: 2183 RVA: 0x000183CB File Offset: 0x000165CB
		internal static string ODataWriterCore_CannotWriteTopLevelResourceSetWithResourceWriter
		{
			get
			{
				return TextRes.GetString("ODataWriterCore_CannotWriteTopLevelResourceSetWithResourceWriter");
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000888 RID: 2184 RVA: 0x000183D7 File Offset: 0x000165D7
		internal static string ODataWriterCore_CannotWriteTopLevelResourceWithResourceSetWriter
		{
			get
			{
				return TextRes.GetString("ODataWriterCore_CannotWriteTopLevelResourceWithResourceSetWriter");
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000889 RID: 2185 RVA: 0x000183E3 File Offset: 0x000165E3
		internal static string ODataWriterCore_SyncCallOnAsyncWriter
		{
			get
			{
				return TextRes.GetString("ODataWriterCore_SyncCallOnAsyncWriter");
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x0600088A RID: 2186 RVA: 0x000183EF File Offset: 0x000165EF
		internal static string ODataWriterCore_AsyncCallOnSyncWriter
		{
			get
			{
				return TextRes.GetString("ODataWriterCore_AsyncCallOnSyncWriter");
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x0600088B RID: 2187 RVA: 0x000183FB File Offset: 0x000165FB
		internal static string ODataWriterCore_EntityReferenceLinkWithoutNavigationLink
		{
			get
			{
				return TextRes.GetString("ODataWriterCore_EntityReferenceLinkWithoutNavigationLink");
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x0600088C RID: 2188 RVA: 0x00018407 File Offset: 0x00016607
		internal static string ODataWriterCore_EntityReferenceLinkInResponse
		{
			get
			{
				return TextRes.GetString("ODataWriterCore_EntityReferenceLinkInResponse");
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x0600088D RID: 2189 RVA: 0x00018413 File Offset: 0x00016613
		internal static string ODataWriterCore_DeferredLinkInRequest
		{
			get
			{
				return TextRes.GetString("ODataWriterCore_DeferredLinkInRequest");
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x0600088E RID: 2190 RVA: 0x0001841F File Offset: 0x0001661F
		internal static string ODataWriterCore_MultipleItemsInNestedResourceInfoWithContent
		{
			get
			{
				return TextRes.GetString("ODataWriterCore_MultipleItemsInNestedResourceInfoWithContent");
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x0600088F RID: 2191 RVA: 0x0001842B File Offset: 0x0001662B
		internal static string ODataWriterCore_DeltaLinkNotSupportedOnExpandedResourceSet
		{
			get
			{
				return TextRes.GetString("ODataWriterCore_DeltaLinkNotSupportedOnExpandedResourceSet");
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000890 RID: 2192 RVA: 0x00018437 File Offset: 0x00016637
		internal static string ODataWriterCore_PathInODataUriMustBeSetWhenWritingContainedElement
		{
			get
			{
				return TextRes.GetString("ODataWriterCore_PathInODataUriMustBeSetWhenWritingContainedElement");
			}
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x00018443 File Offset: 0x00016643
		internal static string DuplicatePropertyNamesNotAllowed(object p0)
		{
			return TextRes.GetString("DuplicatePropertyNamesNotAllowed", new object[] { p0 });
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x00018459 File Offset: 0x00016659
		internal static string DuplicateAnnotationNotAllowed(object p0)
		{
			return TextRes.GetString("DuplicateAnnotationNotAllowed", new object[] { p0 });
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x0001846F File Offset: 0x0001666F
		internal static string DuplicateAnnotationForPropertyNotAllowed(object p0, object p1)
		{
			return TextRes.GetString("DuplicateAnnotationForPropertyNotAllowed", new object[] { p0, p1 });
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x00018489 File Offset: 0x00016689
		internal static string DuplicateAnnotationForInstanceAnnotationNotAllowed(object p0, object p1)
		{
			return TextRes.GetString("DuplicateAnnotationForInstanceAnnotationNotAllowed", new object[] { p0, p1 });
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x000184A3 File Offset: 0x000166A3
		internal static string PropertyAnnotationAfterTheProperty(object p0, object p1)
		{
			return TextRes.GetString("PropertyAnnotationAfterTheProperty", new object[] { p0, p1 });
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x000184BD File Offset: 0x000166BD
		internal static string AtomValueUtils_CannotConvertValueToAtomPrimitive(object p0)
		{
			return TextRes.GetString("AtomValueUtils_CannotConvertValueToAtomPrimitive", new object[] { p0 });
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x000184D3 File Offset: 0x000166D3
		internal static string ODataJsonWriter_UnsupportedValueType(object p0)
		{
			return TextRes.GetString("ODataJsonWriter_UnsupportedValueType", new object[] { p0 });
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000898 RID: 2200 RVA: 0x000184E9 File Offset: 0x000166E9
		internal static string ODataException_GeneralError
		{
			get
			{
				return TextRes.GetString("ODataException_GeneralError");
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000899 RID: 2201 RVA: 0x000184F5 File Offset: 0x000166F5
		internal static string ODataErrorException_GeneralError
		{
			get
			{
				return TextRes.GetString("ODataErrorException_GeneralError");
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x0600089A RID: 2202 RVA: 0x00018501 File Offset: 0x00016701
		internal static string ODataUriParserException_GeneralError
		{
			get
			{
				return TextRes.GetString("ODataUriParserException_GeneralError");
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x0600089B RID: 2203 RVA: 0x0001850D File Offset: 0x0001670D
		internal static string ODataMessageWriter_WriterAlreadyUsed
		{
			get
			{
				return TextRes.GetString("ODataMessageWriter_WriterAlreadyUsed");
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x0600089C RID: 2204 RVA: 0x00018519 File Offset: 0x00016719
		internal static string ODataMessageWriter_EntityReferenceLinksInRequestNotAllowed
		{
			get
			{
				return TextRes.GetString("ODataMessageWriter_EntityReferenceLinksInRequestNotAllowed");
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x0600089D RID: 2205 RVA: 0x00018525 File Offset: 0x00016725
		internal static string ODataMessageWriter_ErrorPayloadInRequest
		{
			get
			{
				return TextRes.GetString("ODataMessageWriter_ErrorPayloadInRequest");
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x0600089E RID: 2206 RVA: 0x00018531 File Offset: 0x00016731
		internal static string ODataMessageWriter_ServiceDocumentInRequest
		{
			get
			{
				return TextRes.GetString("ODataMessageWriter_ServiceDocumentInRequest");
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x0600089F RID: 2207 RVA: 0x0001853D File Offset: 0x0001673D
		internal static string ODataMessageWriter_MetadataDocumentInRequest
		{
			get
			{
				return TextRes.GetString("ODataMessageWriter_MetadataDocumentInRequest");
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060008A0 RID: 2208 RVA: 0x00018549 File Offset: 0x00016749
		internal static string ODataMessageWriter_DeltaInRequest
		{
			get
			{
				return TextRes.GetString("ODataMessageWriter_DeltaInRequest");
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060008A1 RID: 2209 RVA: 0x00018555 File Offset: 0x00016755
		internal static string ODataMessageWriter_AsyncInRequest
		{
			get
			{
				return TextRes.GetString("ODataMessageWriter_AsyncInRequest");
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060008A2 RID: 2210 RVA: 0x00018561 File Offset: 0x00016761
		internal static string ODataMessageWriter_CannotWriteNullInRawFormat
		{
			get
			{
				return TextRes.GetString("ODataMessageWriter_CannotWriteNullInRawFormat");
			}
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x0001856D File Offset: 0x0001676D
		internal static string ODataMessageWriter_CannotSetHeadersWithInvalidPayloadKind(object p0)
		{
			return TextRes.GetString("ODataMessageWriter_CannotSetHeadersWithInvalidPayloadKind", new object[] { p0 });
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x00018583 File Offset: 0x00016783
		internal static string ODataMessageWriter_IncompatiblePayloadKinds(object p0, object p1)
		{
			return TextRes.GetString("ODataMessageWriter_IncompatiblePayloadKinds", new object[] { p0, p1 });
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x0001859D File Offset: 0x0001679D
		internal static string ODataMessageWriter_CannotWriteStreamPropertyAsTopLevelProperty(object p0)
		{
			return TextRes.GetString("ODataMessageWriter_CannotWriteStreamPropertyAsTopLevelProperty", new object[] { p0 });
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x060008A6 RID: 2214 RVA: 0x000185B3 File Offset: 0x000167B3
		internal static string ODataMessageWriter_WriteErrorAlreadyCalled
		{
			get
			{
				return TextRes.GetString("ODataMessageWriter_WriteErrorAlreadyCalled");
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060008A7 RID: 2215 RVA: 0x000185BF File Offset: 0x000167BF
		internal static string ODataMessageWriter_CannotWriteInStreamErrorForRawValues
		{
			get
			{
				return TextRes.GetString("ODataMessageWriter_CannotWriteInStreamErrorForRawValues");
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x060008A8 RID: 2216 RVA: 0x000185CB File Offset: 0x000167CB
		internal static string ODataMessageWriter_CannotWriteMetadataWithoutModel
		{
			get
			{
				return TextRes.GetString("ODataMessageWriter_CannotWriteMetadataWithoutModel");
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060008A9 RID: 2217 RVA: 0x000185D7 File Offset: 0x000167D7
		internal static string ODataMessageWriter_CannotSpecifyOperationWithoutModel
		{
			get
			{
				return TextRes.GetString("ODataMessageWriter_CannotSpecifyOperationWithoutModel");
			}
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x000185E3 File Offset: 0x000167E3
		internal static string ODataMessageWriter_JsonPaddingOnInvalidContentType(object p0)
		{
			return TextRes.GetString("ODataMessageWriter_JsonPaddingOnInvalidContentType", new object[] { p0 });
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x000185F9 File Offset: 0x000167F9
		internal static string ODataMessageWriter_NonCollectionType(object p0)
		{
			return TextRes.GetString("ODataMessageWriter_NonCollectionType", new object[] { p0 });
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060008AC RID: 2220 RVA: 0x0001860F File Offset: 0x0001680F
		internal static string ODataMessageWriterSettings_MessageWriterSettingsXmlCustomizationCallbacksMustBeSpecifiedBoth
		{
			get
			{
				return TextRes.GetString("ODataMessageWriterSettings_MessageWriterSettingsXmlCustomizationCallbacksMustBeSpecifiedBoth");
			}
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x0001861B File Offset: 0x0001681B
		internal static string ODataCollectionWriterCore_InvalidTransitionFromStart(object p0, object p1)
		{
			return TextRes.GetString("ODataCollectionWriterCore_InvalidTransitionFromStart", new object[] { p0, p1 });
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x00018635 File Offset: 0x00016835
		internal static string ODataCollectionWriterCore_InvalidTransitionFromCollection(object p0, object p1)
		{
			return TextRes.GetString("ODataCollectionWriterCore_InvalidTransitionFromCollection", new object[] { p0, p1 });
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x0001864F File Offset: 0x0001684F
		internal static string ODataCollectionWriterCore_InvalidTransitionFromItem(object p0, object p1)
		{
			return TextRes.GetString("ODataCollectionWriterCore_InvalidTransitionFromItem", new object[] { p0, p1 });
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x00018669 File Offset: 0x00016869
		internal static string ODataCollectionWriterCore_WriteEndCalledInInvalidState(object p0)
		{
			return TextRes.GetString("ODataCollectionWriterCore_WriteEndCalledInInvalidState", new object[] { p0 });
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060008B1 RID: 2225 RVA: 0x0001867F File Offset: 0x0001687F
		internal static string ODataCollectionWriterCore_SyncCallOnAsyncWriter
		{
			get
			{
				return TextRes.GetString("ODataCollectionWriterCore_SyncCallOnAsyncWriter");
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060008B2 RID: 2226 RVA: 0x0001868B File Offset: 0x0001688B
		internal static string ODataCollectionWriterCore_AsyncCallOnSyncWriter
		{
			get
			{
				return TextRes.GetString("ODataCollectionWriterCore_AsyncCallOnSyncWriter");
			}
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x00018697 File Offset: 0x00016897
		internal static string ODataBatch_InvalidHttpMethodForChangeSetRequest(object p0)
		{
			return TextRes.GetString("ODataBatch_InvalidHttpMethodForChangeSetRequest", new object[] { p0 });
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x000186AD File Offset: 0x000168AD
		internal static string ODataBatchOperationHeaderDictionary_KeyNotFound(object p0)
		{
			return TextRes.GetString("ODataBatchOperationHeaderDictionary_KeyNotFound", new object[] { p0 });
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x000186C3 File Offset: 0x000168C3
		internal static string ODataBatchOperationHeaderDictionary_DuplicateCaseInsensitiveKeys(object p0)
		{
			return TextRes.GetString("ODataBatchOperationHeaderDictionary_DuplicateCaseInsensitiveKeys", new object[] { p0 });
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060008B6 RID: 2230 RVA: 0x000186D9 File Offset: 0x000168D9
		internal static string ODataParameterWriter_InStreamErrorNotSupported
		{
			get
			{
				return TextRes.GetString("ODataParameterWriter_InStreamErrorNotSupported");
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060008B7 RID: 2231 RVA: 0x000186E5 File Offset: 0x000168E5
		internal static string ODataParameterWriter_CannotCreateParameterWriterOnResponseMessage
		{
			get
			{
				return TextRes.GetString("ODataParameterWriter_CannotCreateParameterWriterOnResponseMessage");
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060008B8 RID: 2232 RVA: 0x000186F1 File Offset: 0x000168F1
		internal static string ODataParameterWriterCore_SyncCallOnAsyncWriter
		{
			get
			{
				return TextRes.GetString("ODataParameterWriterCore_SyncCallOnAsyncWriter");
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060008B9 RID: 2233 RVA: 0x000186FD File Offset: 0x000168FD
		internal static string ODataParameterWriterCore_AsyncCallOnSyncWriter
		{
			get
			{
				return TextRes.GetString("ODataParameterWriterCore_AsyncCallOnSyncWriter");
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060008BA RID: 2234 RVA: 0x00018709 File Offset: 0x00016909
		internal static string ODataParameterWriterCore_CannotWriteStart
		{
			get
			{
				return TextRes.GetString("ODataParameterWriterCore_CannotWriteStart");
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060008BB RID: 2235 RVA: 0x00018715 File Offset: 0x00016915
		internal static string ODataParameterWriterCore_CannotWriteParameter
		{
			get
			{
				return TextRes.GetString("ODataParameterWriterCore_CannotWriteParameter");
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060008BC RID: 2236 RVA: 0x00018721 File Offset: 0x00016921
		internal static string ODataParameterWriterCore_CannotWriteEnd
		{
			get
			{
				return TextRes.GetString("ODataParameterWriterCore_CannotWriteEnd");
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060008BD RID: 2237 RVA: 0x0001872D File Offset: 0x0001692D
		internal static string ODataParameterWriterCore_CannotWriteInErrorOrCompletedState
		{
			get
			{
				return TextRes.GetString("ODataParameterWriterCore_CannotWriteInErrorOrCompletedState");
			}
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x00018739 File Offset: 0x00016939
		internal static string ODataParameterWriterCore_DuplicatedParameterNameNotAllowed(object p0)
		{
			return TextRes.GetString("ODataParameterWriterCore_DuplicatedParameterNameNotAllowed", new object[] { p0 });
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x0001874F File Offset: 0x0001694F
		internal static string ODataParameterWriterCore_CannotWriteValueOnNonValueTypeKind(object p0, object p1)
		{
			return TextRes.GetString("ODataParameterWriterCore_CannotWriteValueOnNonValueTypeKind", new object[] { p0, p1 });
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x00018769 File Offset: 0x00016969
		internal static string ODataParameterWriterCore_CannotWriteValueOnNonSupportedValueType(object p0, object p1)
		{
			return TextRes.GetString("ODataParameterWriterCore_CannotWriteValueOnNonSupportedValueType", new object[] { p0, p1 });
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x00018783 File Offset: 0x00016983
		internal static string ODataParameterWriterCore_CannotCreateCollectionWriterOnNonCollectionTypeKind(object p0, object p1)
		{
			return TextRes.GetString("ODataParameterWriterCore_CannotCreateCollectionWriterOnNonCollectionTypeKind", new object[] { p0, p1 });
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x0001879D File Offset: 0x0001699D
		internal static string ODataParameterWriterCore_CannotCreateResourceWriterOnNonEntityOrComplexTypeKind(object p0, object p1)
		{
			return TextRes.GetString("ODataParameterWriterCore_CannotCreateResourceWriterOnNonEntityOrComplexTypeKind", new object[] { p0, p1 });
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x000187B7 File Offset: 0x000169B7
		internal static string ODataParameterWriterCore_CannotCreateResourceSetWriterOnNonStructuredCollectionTypeKind(object p0, object p1)
		{
			return TextRes.GetString("ODataParameterWriterCore_CannotCreateResourceSetWriterOnNonStructuredCollectionTypeKind", new object[] { p0, p1 });
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x000187D1 File Offset: 0x000169D1
		internal static string ODataParameterWriterCore_ParameterNameNotFoundInOperation(object p0, object p1)
		{
			return TextRes.GetString("ODataParameterWriterCore_ParameterNameNotFoundInOperation", new object[] { p0, p1 });
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x000187EB File Offset: 0x000169EB
		internal static string ODataParameterWriterCore_MissingParameterInParameterPayload(object p0, object p1)
		{
			return TextRes.GetString("ODataParameterWriterCore_MissingParameterInParameterPayload", new object[] { p0, p1 });
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060008C6 RID: 2246 RVA: 0x00018805 File Offset: 0x00016A05
		internal static string ODataBatchWriter_FlushOrFlushAsyncCalledInStreamRequestedState
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_FlushOrFlushAsyncCalledInStreamRequestedState");
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060008C7 RID: 2247 RVA: 0x00018811 File Offset: 0x00016A11
		internal static string ODataBatchWriter_CannotCompleteBatchWithActiveChangeSet
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_CannotCompleteBatchWithActiveChangeSet");
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060008C8 RID: 2248 RVA: 0x0001881D File Offset: 0x00016A1D
		internal static string ODataBatchWriter_CannotStartChangeSetWithActiveChangeSet
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_CannotStartChangeSetWithActiveChangeSet");
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060008C9 RID: 2249 RVA: 0x00018829 File Offset: 0x00016A29
		internal static string ODataBatchWriter_CannotCompleteChangeSetWithoutActiveChangeSet
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_CannotCompleteChangeSetWithoutActiveChangeSet");
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060008CA RID: 2250 RVA: 0x00018835 File Offset: 0x00016A35
		internal static string ODataBatchWriter_InvalidTransitionFromStart
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_InvalidTransitionFromStart");
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060008CB RID: 2251 RVA: 0x00018841 File Offset: 0x00016A41
		internal static string ODataBatchWriter_InvalidTransitionFromBatchStarted
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_InvalidTransitionFromBatchStarted");
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060008CC RID: 2252 RVA: 0x0001884D File Offset: 0x00016A4D
		internal static string ODataBatchWriter_InvalidTransitionFromChangeSetStarted
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_InvalidTransitionFromChangeSetStarted");
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060008CD RID: 2253 RVA: 0x00018859 File Offset: 0x00016A59
		internal static string ODataBatchWriter_InvalidTransitionFromOperationCreated
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_InvalidTransitionFromOperationCreated");
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060008CE RID: 2254 RVA: 0x00018865 File Offset: 0x00016A65
		internal static string ODataBatchWriter_InvalidTransitionFromOperationContentStreamRequested
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_InvalidTransitionFromOperationContentStreamRequested");
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060008CF RID: 2255 RVA: 0x00018871 File Offset: 0x00016A71
		internal static string ODataBatchWriter_InvalidTransitionFromOperationContentStreamDisposed
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_InvalidTransitionFromOperationContentStreamDisposed");
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060008D0 RID: 2256 RVA: 0x0001887D File Offset: 0x00016A7D
		internal static string ODataBatchWriter_InvalidTransitionFromChangeSetCompleted
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_InvalidTransitionFromChangeSetCompleted");
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060008D1 RID: 2257 RVA: 0x00018889 File Offset: 0x00016A89
		internal static string ODataBatchWriter_InvalidTransitionFromBatchCompleted
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_InvalidTransitionFromBatchCompleted");
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060008D2 RID: 2258 RVA: 0x00018895 File Offset: 0x00016A95
		internal static string ODataBatchWriter_CannotCreateRequestOperationWhenWritingResponse
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_CannotCreateRequestOperationWhenWritingResponse");
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060008D3 RID: 2259 RVA: 0x000188A1 File Offset: 0x00016AA1
		internal static string ODataBatchWriter_CannotCreateResponseOperationWhenWritingRequest
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_CannotCreateResponseOperationWhenWritingRequest");
			}
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x000188AD File Offset: 0x00016AAD
		internal static string ODataBatchWriter_MaxBatchSizeExceeded(object p0)
		{
			return TextRes.GetString("ODataBatchWriter_MaxBatchSizeExceeded", new object[] { p0 });
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x000188C3 File Offset: 0x00016AC3
		internal static string ODataBatchWriter_MaxChangeSetSizeExceeded(object p0)
		{
			return TextRes.GetString("ODataBatchWriter_MaxChangeSetSizeExceeded", new object[] { p0 });
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060008D6 RID: 2262 RVA: 0x000188D9 File Offset: 0x00016AD9
		internal static string ODataBatchWriter_SyncCallOnAsyncWriter
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_SyncCallOnAsyncWriter");
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060008D7 RID: 2263 RVA: 0x000188E5 File Offset: 0x00016AE5
		internal static string ODataBatchWriter_AsyncCallOnSyncWriter
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_AsyncCallOnSyncWriter");
			}
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x000188F1 File Offset: 0x00016AF1
		internal static string ODataBatchWriter_DuplicateContentIDsNotAllowed(object p0)
		{
			return TextRes.GetString("ODataBatchWriter_DuplicateContentIDsNotAllowed", new object[] { p0 });
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060008D9 RID: 2265 RVA: 0x00018907 File Offset: 0x00016B07
		internal static string ODataBatchWriter_CannotWriteInStreamErrorForBatch
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_CannotWriteInStreamErrorForBatch");
			}
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x00018913 File Offset: 0x00016B13
		internal static string ODataBatchUtils_RelativeUriUsedWithoutBaseUriSpecified(object p0)
		{
			return TextRes.GetString("ODataBatchUtils_RelativeUriUsedWithoutBaseUriSpecified", new object[] { p0 });
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x00018929 File Offset: 0x00016B29
		internal static string ODataBatchUtils_RelativeUriStartingWithDollarUsedWithoutBaseUriSpecified(object p0)
		{
			return TextRes.GetString("ODataBatchUtils_RelativeUriStartingWithDollarUsedWithoutBaseUriSpecified", new object[] { p0 });
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060008DC RID: 2268 RVA: 0x0001893F File Offset: 0x00016B3F
		internal static string ODataBatchOperationMessage_VerifyNotCompleted
		{
			get
			{
				return TextRes.GetString("ODataBatchOperationMessage_VerifyNotCompleted");
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060008DD RID: 2269 RVA: 0x0001894B File Offset: 0x00016B4B
		internal static string ODataBatchOperationStream_Disposed
		{
			get
			{
				return TextRes.GetString("ODataBatchOperationStream_Disposed");
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060008DE RID: 2270 RVA: 0x00018957 File Offset: 0x00016B57
		internal static string ODataBatchReader_CannotCreateRequestOperationWhenReadingResponse
		{
			get
			{
				return TextRes.GetString("ODataBatchReader_CannotCreateRequestOperationWhenReadingResponse");
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060008DF RID: 2271 RVA: 0x00018963 File Offset: 0x00016B63
		internal static string ODataBatchReader_CannotCreateResponseOperationWhenReadingRequest
		{
			get
			{
				return TextRes.GetString("ODataBatchReader_CannotCreateResponseOperationWhenReadingRequest");
			}
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x0001896F File Offset: 0x00016B6F
		internal static string ODataBatchReader_InvalidStateForCreateOperationRequestMessage(object p0)
		{
			return TextRes.GetString("ODataBatchReader_InvalidStateForCreateOperationRequestMessage", new object[] { p0 });
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060008E1 RID: 2273 RVA: 0x00018985 File Offset: 0x00016B85
		internal static string ODataBatchReader_OperationRequestMessageAlreadyCreated
		{
			get
			{
				return TextRes.GetString("ODataBatchReader_OperationRequestMessageAlreadyCreated");
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x060008E2 RID: 2274 RVA: 0x00018991 File Offset: 0x00016B91
		internal static string ODataBatchReader_OperationResponseMessageAlreadyCreated
		{
			get
			{
				return TextRes.GetString("ODataBatchReader_OperationResponseMessageAlreadyCreated");
			}
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x0001899D File Offset: 0x00016B9D
		internal static string ODataBatchReader_InvalidStateForCreateOperationResponseMessage(object p0)
		{
			return TextRes.GetString("ODataBatchReader_InvalidStateForCreateOperationResponseMessage", new object[] { p0 });
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x060008E4 RID: 2276 RVA: 0x000189B3 File Offset: 0x00016BB3
		internal static string ODataBatchReader_CannotUseReaderWhileOperationStreamActive
		{
			get
			{
				return TextRes.GetString("ODataBatchReader_CannotUseReaderWhileOperationStreamActive");
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x060008E5 RID: 2277 RVA: 0x000189BF File Offset: 0x00016BBF
		internal static string ODataBatchReader_SyncCallOnAsyncReader
		{
			get
			{
				return TextRes.GetString("ODataBatchReader_SyncCallOnAsyncReader");
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x060008E6 RID: 2278 RVA: 0x000189CB File Offset: 0x00016BCB
		internal static string ODataBatchReader_AsyncCallOnSyncReader
		{
			get
			{
				return TextRes.GetString("ODataBatchReader_AsyncCallOnSyncReader");
			}
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x000189D7 File Offset: 0x00016BD7
		internal static string ODataBatchReader_ReadOrReadAsyncCalledInInvalidState(object p0)
		{
			return TextRes.GetString("ODataBatchReader_ReadOrReadAsyncCalledInInvalidState", new object[] { p0 });
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x000189ED File Offset: 0x00016BED
		internal static string ODataBatchReader_MaxBatchSizeExceeded(object p0)
		{
			return TextRes.GetString("ODataBatchReader_MaxBatchSizeExceeded", new object[] { p0 });
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x00018A03 File Offset: 0x00016C03
		internal static string ODataBatchReader_MaxChangeSetSizeExceeded(object p0)
		{
			return TextRes.GetString("ODataBatchReader_MaxChangeSetSizeExceeded", new object[] { p0 });
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060008EA RID: 2282 RVA: 0x00018A19 File Offset: 0x00016C19
		internal static string ODataBatchReader_NoMessageWasCreatedForOperation
		{
			get
			{
				return TextRes.GetString("ODataBatchReader_NoMessageWasCreatedForOperation");
			}
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x00018A25 File Offset: 0x00016C25
		internal static string ODataBatchReader_DuplicateContentIDsNotAllowed(object p0)
		{
			return TextRes.GetString("ODataBatchReader_DuplicateContentIDsNotAllowed", new object[] { p0 });
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x00018A3B File Offset: 0x00016C3B
		internal static string ODataBatchReaderStream_InvalidHeaderSpecified(object p0)
		{
			return TextRes.GetString("ODataBatchReaderStream_InvalidHeaderSpecified", new object[] { p0 });
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x00018A51 File Offset: 0x00016C51
		internal static string ODataBatchReaderStream_InvalidRequestLine(object p0)
		{
			return TextRes.GetString("ODataBatchReaderStream_InvalidRequestLine", new object[] { p0 });
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x00018A67 File Offset: 0x00016C67
		internal static string ODataBatchReaderStream_InvalidResponseLine(object p0)
		{
			return TextRes.GetString("ODataBatchReaderStream_InvalidResponseLine", new object[] { p0 });
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x00018A7D File Offset: 0x00016C7D
		internal static string ODataBatchReaderStream_InvalidHttpVersionSpecified(object p0, object p1)
		{
			return TextRes.GetString("ODataBatchReaderStream_InvalidHttpVersionSpecified", new object[] { p0, p1 });
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x00018A97 File Offset: 0x00016C97
		internal static string ODataBatchReaderStream_NonIntegerHttpStatusCode(object p0)
		{
			return TextRes.GetString("ODataBatchReaderStream_NonIntegerHttpStatusCode", new object[] { p0 });
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060008F1 RID: 2289 RVA: 0x00018AAD File Offset: 0x00016CAD
		internal static string ODataBatchReaderStream_MissingContentTypeHeader
		{
			get
			{
				return TextRes.GetString("ODataBatchReaderStream_MissingContentTypeHeader");
			}
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x00018AB9 File Offset: 0x00016CB9
		internal static string ODataBatchReaderStream_MissingOrInvalidContentEncodingHeader(object p0, object p1)
		{
			return TextRes.GetString("ODataBatchReaderStream_MissingOrInvalidContentEncodingHeader", new object[] { p0, p1 });
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x00018AD3 File Offset: 0x00016CD3
		internal static string ODataBatchReaderStream_InvalidContentTypeSpecified(object p0, object p1, object p2, object p3)
		{
			return TextRes.GetString("ODataBatchReaderStream_InvalidContentTypeSpecified", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x00018AF5 File Offset: 0x00016CF5
		internal static string ODataBatchReaderStream_InvalidContentLengthSpecified(object p0)
		{
			return TextRes.GetString("ODataBatchReaderStream_InvalidContentLengthSpecified", new object[] { p0 });
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x00018B0B File Offset: 0x00016D0B
		internal static string ODataBatchReaderStream_DuplicateHeaderFound(object p0)
		{
			return TextRes.GetString("ODataBatchReaderStream_DuplicateHeaderFound", new object[] { p0 });
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060008F6 RID: 2294 RVA: 0x00018B21 File Offset: 0x00016D21
		internal static string ODataBatchReaderStream_NestedChangesetsAreNotSupported
		{
			get
			{
				return TextRes.GetString("ODataBatchReaderStream_NestedChangesetsAreNotSupported");
			}
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x00018B2D File Offset: 0x00016D2D
		internal static string ODataBatchReaderStream_MultiByteEncodingsNotSupported(object p0)
		{
			return TextRes.GetString("ODataBatchReaderStream_MultiByteEncodingsNotSupported", new object[] { p0 });
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060008F8 RID: 2296 RVA: 0x00018B43 File Offset: 0x00016D43
		internal static string ODataBatchReaderStream_UnexpectedEndOfInput
		{
			get
			{
				return TextRes.GetString("ODataBatchReaderStream_UnexpectedEndOfInput");
			}
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x00018B4F File Offset: 0x00016D4F
		internal static string ODataBatchReaderStreamBuffer_BoundaryLineSecurityLimitReached(object p0)
		{
			return TextRes.GetString("ODataBatchReaderStreamBuffer_BoundaryLineSecurityLimitReached", new object[] { p0 });
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060008FA RID: 2298 RVA: 0x00018B65 File Offset: 0x00016D65
		internal static string ODataAsyncWriter_CannotCreateResponseWhenNotWritingResponse
		{
			get
			{
				return TextRes.GetString("ODataAsyncWriter_CannotCreateResponseWhenNotWritingResponse");
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060008FB RID: 2299 RVA: 0x00018B71 File Offset: 0x00016D71
		internal static string ODataAsyncWriter_CannotCreateResponseMoreThanOnce
		{
			get
			{
				return TextRes.GetString("ODataAsyncWriter_CannotCreateResponseMoreThanOnce");
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060008FC RID: 2300 RVA: 0x00018B7D File Offset: 0x00016D7D
		internal static string ODataAsyncWriter_SyncCallOnAsyncWriter
		{
			get
			{
				return TextRes.GetString("ODataAsyncWriter_SyncCallOnAsyncWriter");
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060008FD RID: 2301 RVA: 0x00018B89 File Offset: 0x00016D89
		internal static string ODataAsyncWriter_AsyncCallOnSyncWriter
		{
			get
			{
				return TextRes.GetString("ODataAsyncWriter_AsyncCallOnSyncWriter");
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060008FE RID: 2302 RVA: 0x00018B95 File Offset: 0x00016D95
		internal static string ODataAsyncWriter_CannotWriteInStreamErrorForAsync
		{
			get
			{
				return TextRes.GetString("ODataAsyncWriter_CannotWriteInStreamErrorForAsync");
			}
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x00018BA1 File Offset: 0x00016DA1
		internal static string ODataAsyncReader_InvalidHeaderSpecified(object p0)
		{
			return TextRes.GetString("ODataAsyncReader_InvalidHeaderSpecified", new object[] { p0 });
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000900 RID: 2304 RVA: 0x00018BB7 File Offset: 0x00016DB7
		internal static string ODataAsyncReader_CannotCreateResponseWhenNotReadingResponse
		{
			get
			{
				return TextRes.GetString("ODataAsyncReader_CannotCreateResponseWhenNotReadingResponse");
			}
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x00018BC3 File Offset: 0x00016DC3
		internal static string ODataAsyncReader_InvalidResponseLine(object p0)
		{
			return TextRes.GetString("ODataAsyncReader_InvalidResponseLine", new object[] { p0 });
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x00018BD9 File Offset: 0x00016DD9
		internal static string ODataAsyncReader_InvalidHttpVersionSpecified(object p0, object p1)
		{
			return TextRes.GetString("ODataAsyncReader_InvalidHttpVersionSpecified", new object[] { p0, p1 });
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x00018BF3 File Offset: 0x00016DF3
		internal static string ODataAsyncReader_NonIntegerHttpStatusCode(object p0)
		{
			return TextRes.GetString("ODataAsyncReader_NonIntegerHttpStatusCode", new object[] { p0 });
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x00018C09 File Offset: 0x00016E09
		internal static string ODataAsyncReader_DuplicateHeaderFound(object p0)
		{
			return TextRes.GetString("ODataAsyncReader_DuplicateHeaderFound", new object[] { p0 });
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x00018C1F File Offset: 0x00016E1F
		internal static string ODataAsyncReader_MultiByteEncodingsNotSupported(object p0)
		{
			return TextRes.GetString("ODataAsyncReader_MultiByteEncodingsNotSupported", new object[] { p0 });
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x00018C35 File Offset: 0x00016E35
		internal static string ODataAsyncReader_InvalidNewLineEncountered(object p0)
		{
			return TextRes.GetString("ODataAsyncReader_InvalidNewLineEncountered", new object[] { p0 });
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000907 RID: 2311 RVA: 0x00018C4B File Offset: 0x00016E4B
		internal static string ODataAsyncReader_UnexpectedEndOfInput
		{
			get
			{
				return TextRes.GetString("ODataAsyncReader_UnexpectedEndOfInput");
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000908 RID: 2312 RVA: 0x00018C57 File Offset: 0x00016E57
		internal static string ODataAsyncReader_SyncCallOnAsyncReader
		{
			get
			{
				return TextRes.GetString("ODataAsyncReader_SyncCallOnAsyncReader");
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000909 RID: 2313 RVA: 0x00018C63 File Offset: 0x00016E63
		internal static string ODataAsyncReader_AsyncCallOnSyncReader
		{
			get
			{
				return TextRes.GetString("ODataAsyncReader_AsyncCallOnSyncReader");
			}
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x00018C6F File Offset: 0x00016E6F
		internal static string HttpUtils_MediaTypeUnspecified(object p0)
		{
			return TextRes.GetString("HttpUtils_MediaTypeUnspecified", new object[] { p0 });
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x00018C85 File Offset: 0x00016E85
		internal static string HttpUtils_MediaTypeRequiresSlash(object p0)
		{
			return TextRes.GetString("HttpUtils_MediaTypeRequiresSlash", new object[] { p0 });
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x00018C9B File Offset: 0x00016E9B
		internal static string HttpUtils_MediaTypeRequiresSubType(object p0)
		{
			return TextRes.GetString("HttpUtils_MediaTypeRequiresSubType", new object[] { p0 });
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x00018CB1 File Offset: 0x00016EB1
		internal static string HttpUtils_MediaTypeMissingParameterValue(object p0)
		{
			return TextRes.GetString("HttpUtils_MediaTypeMissingParameterValue", new object[] { p0 });
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x0600090E RID: 2318 RVA: 0x00018CC7 File Offset: 0x00016EC7
		internal static string HttpUtils_MediaTypeMissingParameterName
		{
			get
			{
				return TextRes.GetString("HttpUtils_MediaTypeMissingParameterName");
			}
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x00018CD3 File Offset: 0x00016ED3
		internal static string HttpUtils_EscapeCharWithoutQuotes(object p0, object p1, object p2, object p3)
		{
			return TextRes.GetString("HttpUtils_EscapeCharWithoutQuotes", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x00018CF5 File Offset: 0x00016EF5
		internal static string HttpUtils_EscapeCharAtEnd(object p0, object p1, object p2, object p3)
		{
			return TextRes.GetString("HttpUtils_EscapeCharAtEnd", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x00018D17 File Offset: 0x00016F17
		internal static string HttpUtils_ClosingQuoteNotFound(object p0, object p1, object p2)
		{
			return TextRes.GetString("HttpUtils_ClosingQuoteNotFound", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x00018D35 File Offset: 0x00016F35
		internal static string HttpUtils_InvalidCharacterInQuotedParameterValue(object p0, object p1, object p2, object p3)
		{
			return TextRes.GetString("HttpUtils_InvalidCharacterInQuotedParameterValue", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000913 RID: 2323 RVA: 0x00018D57 File Offset: 0x00016F57
		internal static string HttpUtils_ContentTypeMissing
		{
			get
			{
				return TextRes.GetString("HttpUtils_ContentTypeMissing");
			}
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x00018D63 File Offset: 0x00016F63
		internal static string HttpUtils_MediaTypeRequiresSemicolonBeforeParameter(object p0)
		{
			return TextRes.GetString("HttpUtils_MediaTypeRequiresSemicolonBeforeParameter", new object[] { p0 });
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x00018D79 File Offset: 0x00016F79
		internal static string HttpUtils_InvalidQualityValueStartChar(object p0, object p1)
		{
			return TextRes.GetString("HttpUtils_InvalidQualityValueStartChar", new object[] { p0, p1 });
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x00018D93 File Offset: 0x00016F93
		internal static string HttpUtils_InvalidQualityValue(object p0, object p1)
		{
			return TextRes.GetString("HttpUtils_InvalidQualityValue", new object[] { p0, p1 });
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x00018DAD File Offset: 0x00016FAD
		internal static string HttpUtils_CannotConvertCharToInt(object p0)
		{
			return TextRes.GetString("HttpUtils_CannotConvertCharToInt", new object[] { p0 });
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x00018DC3 File Offset: 0x00016FC3
		internal static string HttpUtils_MissingSeparatorBetweenCharsets(object p0)
		{
			return TextRes.GetString("HttpUtils_MissingSeparatorBetweenCharsets", new object[] { p0 });
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x00018DD9 File Offset: 0x00016FD9
		internal static string HttpUtils_InvalidSeparatorBetweenCharsets(object p0)
		{
			return TextRes.GetString("HttpUtils_InvalidSeparatorBetweenCharsets", new object[] { p0 });
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x00018DEF File Offset: 0x00016FEF
		internal static string HttpUtils_InvalidCharsetName(object p0)
		{
			return TextRes.GetString("HttpUtils_InvalidCharsetName", new object[] { p0 });
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x00018E05 File Offset: 0x00017005
		internal static string HttpUtils_UnexpectedEndOfQValue(object p0)
		{
			return TextRes.GetString("HttpUtils_UnexpectedEndOfQValue", new object[] { p0 });
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x00018E1B File Offset: 0x0001701B
		internal static string HttpUtils_ExpectedLiteralNotFoundInString(object p0, object p1, object p2)
		{
			return TextRes.GetString("HttpUtils_ExpectedLiteralNotFoundInString", new object[] { p0, p1, p2 });
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x00018E39 File Offset: 0x00017039
		internal static string HttpUtils_InvalidHttpMethodString(object p0)
		{
			return TextRes.GetString("HttpUtils_InvalidHttpMethodString", new object[] { p0 });
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x00018E4F File Offset: 0x0001704F
		internal static string HttpUtils_NoOrMoreThanOneContentTypeSpecified(object p0)
		{
			return TextRes.GetString("HttpUtils_NoOrMoreThanOneContentTypeSpecified", new object[] { p0 });
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x00018E65 File Offset: 0x00017065
		internal static string HttpHeaderValueLexer_UnrecognizedSeparator(object p0, object p1, object p2, object p3)
		{
			return TextRes.GetString("HttpHeaderValueLexer_UnrecognizedSeparator", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x00018E87 File Offset: 0x00017087
		internal static string HttpHeaderValueLexer_TokenExpectedButFoundQuotedString(object p0, object p1, object p2)
		{
			return TextRes.GetString("HttpHeaderValueLexer_TokenExpectedButFoundQuotedString", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x00018EA5 File Offset: 0x000170A5
		internal static string HttpHeaderValueLexer_FailedToReadTokenOrQuotedString(object p0, object p1, object p2)
		{
			return TextRes.GetString("HttpHeaderValueLexer_FailedToReadTokenOrQuotedString", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x00018EC3 File Offset: 0x000170C3
		internal static string HttpHeaderValueLexer_InvalidSeparatorAfterQuotedString(object p0, object p1, object p2, object p3)
		{
			return TextRes.GetString("HttpHeaderValueLexer_InvalidSeparatorAfterQuotedString", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x00018EE5 File Offset: 0x000170E5
		internal static string HttpHeaderValueLexer_EndOfFileAfterSeparator(object p0, object p1, object p2, object p3)
		{
			return TextRes.GetString("HttpHeaderValueLexer_EndOfFileAfterSeparator", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x00018F07 File Offset: 0x00017107
		internal static string MediaType_EncodingNotSupported(object p0)
		{
			return TextRes.GetString("MediaType_EncodingNotSupported", new object[] { p0 });
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x00018F1D File Offset: 0x0001711D
		internal static string MediaTypeUtils_DidNotFindMatchingMediaType(object p0, object p1)
		{
			return TextRes.GetString("MediaTypeUtils_DidNotFindMatchingMediaType", new object[] { p0, p1 });
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x00018F37 File Offset: 0x00017137
		internal static string MediaTypeUtils_CannotDetermineFormatFromContentType(object p0, object p1)
		{
			return TextRes.GetString("MediaTypeUtils_CannotDetermineFormatFromContentType", new object[] { p0, p1 });
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x00018F51 File Offset: 0x00017151
		internal static string MediaTypeUtils_NoOrMoreThanOneContentTypeSpecified(object p0)
		{
			return TextRes.GetString("MediaTypeUtils_NoOrMoreThanOneContentTypeSpecified", new object[] { p0 });
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x00018F67 File Offset: 0x00017167
		internal static string MediaTypeUtils_BoundaryMustBeSpecifiedForBatchPayloads(object p0, object p1)
		{
			return TextRes.GetString("MediaTypeUtils_BoundaryMustBeSpecifiedForBatchPayloads", new object[] { p0, p1 });
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x00018F81 File Offset: 0x00017181
		internal static string ExpressionLexer_ExpectedLiteralToken(object p0)
		{
			return TextRes.GetString("ExpressionLexer_ExpectedLiteralToken", new object[] { p0 });
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x00018F97 File Offset: 0x00017197
		internal static string ODataUriUtils_ConvertToUriLiteralUnsupportedType(object p0)
		{
			return TextRes.GetString("ODataUriUtils_ConvertToUriLiteralUnsupportedType", new object[] { p0 });
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x0600092B RID: 2347 RVA: 0x00018FAD File Offset: 0x000171AD
		internal static string ODataUriUtils_ConvertFromUriLiteralTypeRefWithoutModel
		{
			get
			{
				return TextRes.GetString("ODataUriUtils_ConvertFromUriLiteralTypeRefWithoutModel");
			}
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x00018FB9 File Offset: 0x000171B9
		internal static string ODataUriUtils_ConvertFromUriLiteralTypeVerificationFailure(object p0, object p1)
		{
			return TextRes.GetString("ODataUriUtils_ConvertFromUriLiteralTypeVerificationFailure", new object[] { p0, p1 });
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x00018FD3 File Offset: 0x000171D3
		internal static string ODataUriUtils_ConvertFromUriLiteralNullTypeVerificationFailure(object p0, object p1)
		{
			return TextRes.GetString("ODataUriUtils_ConvertFromUriLiteralNullTypeVerificationFailure", new object[] { p0, p1 });
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x00018FED File Offset: 0x000171ED
		internal static string ODataUriUtils_ConvertFromUriLiteralNullOnNonNullableType(object p0)
		{
			return TextRes.GetString("ODataUriUtils_ConvertFromUriLiteralNullOnNonNullableType", new object[] { p0 });
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x00019003 File Offset: 0x00017203
		internal static string ODataUtils_CannotConvertValueToRawString(object p0)
		{
			return TextRes.GetString("ODataUtils_CannotConvertValueToRawString", new object[] { p0 });
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x00019019 File Offset: 0x00017219
		internal static string ODataUtils_DidNotFindDefaultMediaType(object p0)
		{
			return TextRes.GetString("ODataUtils_DidNotFindDefaultMediaType", new object[] { p0 });
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x0001902F File Offset: 0x0001722F
		internal static string ODataUtils_UnsupportedVersionHeader(object p0)
		{
			return TextRes.GetString("ODataUtils_UnsupportedVersionHeader", new object[] { p0 });
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000932 RID: 2354 RVA: 0x00019045 File Offset: 0x00017245
		internal static string ODataUtils_UnsupportedVersionNumber
		{
			get
			{
				return TextRes.GetString("ODataUtils_UnsupportedVersionNumber");
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000933 RID: 2355 RVA: 0x00019051 File Offset: 0x00017251
		internal static string ODataUtils_ModelDoesNotHaveContainer
		{
			get
			{
				return TextRes.GetString("ODataUtils_ModelDoesNotHaveContainer");
			}
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x0001905D File Offset: 0x0001725D
		internal static string ReaderUtils_EnumerableModified(object p0)
		{
			return TextRes.GetString("ReaderUtils_EnumerableModified", new object[] { p0 });
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x00019073 File Offset: 0x00017273
		internal static string ReaderValidationUtils_NullValueForNonNullableType(object p0)
		{
			return TextRes.GetString("ReaderValidationUtils_NullValueForNonNullableType", new object[] { p0 });
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x00019089 File Offset: 0x00017289
		internal static string ReaderValidationUtils_NullNamedValueForNonNullableType(object p0, object p1)
		{
			return TextRes.GetString("ReaderValidationUtils_NullNamedValueForNonNullableType", new object[] { p0, p1 });
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000937 RID: 2359 RVA: 0x000190A3 File Offset: 0x000172A3
		internal static string ReaderValidationUtils_EntityReferenceLinkMissingUri
		{
			get
			{
				return TextRes.GetString("ReaderValidationUtils_EntityReferenceLinkMissingUri");
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000938 RID: 2360 RVA: 0x000190AF File Offset: 0x000172AF
		internal static string ReaderValidationUtils_ValueWithoutType
		{
			get
			{
				return TextRes.GetString("ReaderValidationUtils_ValueWithoutType");
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000939 RID: 2361 RVA: 0x000190BB File Offset: 0x000172BB
		internal static string ReaderValidationUtils_ResourceWithoutType
		{
			get
			{
				return TextRes.GetString("ReaderValidationUtils_ResourceWithoutType");
			}
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x000190C7 File Offset: 0x000172C7
		internal static string ReaderValidationUtils_CannotConvertPrimitiveValue(object p0, object p1)
		{
			return TextRes.GetString("ReaderValidationUtils_CannotConvertPrimitiveValue", new object[] { p0, p1 });
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x000190E1 File Offset: 0x000172E1
		internal static string ReaderValidationUtils_MessageReaderSettingsBaseUriMustBeNullOrAbsolute(object p0)
		{
			return TextRes.GetString("ReaderValidationUtils_MessageReaderSettingsBaseUriMustBeNullOrAbsolute", new object[] { p0 });
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x0600093C RID: 2364 RVA: 0x000190F7 File Offset: 0x000172F7
		internal static string ReaderValidationUtils_UndeclaredPropertyBehaviorKindSpecifiedOnRequest
		{
			get
			{
				return TextRes.GetString("ReaderValidationUtils_UndeclaredPropertyBehaviorKindSpecifiedOnRequest");
			}
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x00019103 File Offset: 0x00017303
		internal static string ReaderValidationUtils_ContextUriValidationInvalidExpectedEntitySet(object p0, object p1, object p2)
		{
			return TextRes.GetString("ReaderValidationUtils_ContextUriValidationInvalidExpectedEntitySet", new object[] { p0, p1, p2 });
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x00019121 File Offset: 0x00017321
		internal static string ReaderValidationUtils_ContextUriValidationInvalidExpectedEntityType(object p0, object p1, object p2)
		{
			return TextRes.GetString("ReaderValidationUtils_ContextUriValidationInvalidExpectedEntityType", new object[] { p0, p1, p2 });
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x0001913F File Offset: 0x0001733F
		internal static string ReaderValidationUtils_ContextUriValidationNonMatchingPropertyNames(object p0, object p1, object p2, object p3)
		{
			return TextRes.GetString("ReaderValidationUtils_ContextUriValidationNonMatchingPropertyNames", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x00019161 File Offset: 0x00017361
		internal static string ReaderValidationUtils_ContextUriValidationNonMatchingDeclaringTypes(object p0, object p1, object p2, object p3)
		{
			return TextRes.GetString("ReaderValidationUtils_ContextUriValidationNonMatchingDeclaringTypes", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x00019183 File Offset: 0x00017383
		internal static string ReaderValidationUtils_NonMatchingPropertyNames(object p0, object p1)
		{
			return TextRes.GetString("ReaderValidationUtils_NonMatchingPropertyNames", new object[] { p0, p1 });
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x0001919D File Offset: 0x0001739D
		internal static string ReaderValidationUtils_TypeInContextUriDoesNotMatchExpectedType(object p0, object p1, object p2)
		{
			return TextRes.GetString("ReaderValidationUtils_TypeInContextUriDoesNotMatchExpectedType", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x000191BB File Offset: 0x000173BB
		internal static string ReaderValidationUtils_ContextUriDoesNotReferTypeAssignableToExpectedType(object p0, object p1, object p2)
		{
			return TextRes.GetString("ReaderValidationUtils_ContextUriDoesNotReferTypeAssignableToExpectedType", new object[] { p0, p1, p2 });
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000944 RID: 2372 RVA: 0x000191D9 File Offset: 0x000173D9
		internal static string ODataMessageReader_ReaderAlreadyUsed
		{
			get
			{
				return TextRes.GetString("ODataMessageReader_ReaderAlreadyUsed");
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000945 RID: 2373 RVA: 0x000191E5 File Offset: 0x000173E5
		internal static string ODataMessageReader_ErrorPayloadInRequest
		{
			get
			{
				return TextRes.GetString("ODataMessageReader_ErrorPayloadInRequest");
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000946 RID: 2374 RVA: 0x000191F1 File Offset: 0x000173F1
		internal static string ODataMessageReader_ServiceDocumentInRequest
		{
			get
			{
				return TextRes.GetString("ODataMessageReader_ServiceDocumentInRequest");
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000947 RID: 2375 RVA: 0x000191FD File Offset: 0x000173FD
		internal static string ODataMessageReader_MetadataDocumentInRequest
		{
			get
			{
				return TextRes.GetString("ODataMessageReader_MetadataDocumentInRequest");
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000948 RID: 2376 RVA: 0x00019209 File Offset: 0x00017409
		internal static string ODataMessageReader_DeltaInRequest
		{
			get
			{
				return TextRes.GetString("ODataMessageReader_DeltaInRequest");
			}
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x00019215 File Offset: 0x00017415
		internal static string ODataMessageReader_ExpectedTypeSpecifiedWithoutMetadata(object p0)
		{
			return TextRes.GetString("ODataMessageReader_ExpectedTypeSpecifiedWithoutMetadata", new object[] { p0 });
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x0001922B File Offset: 0x0001742B
		internal static string ODataMessageReader_EntitySetSpecifiedWithoutMetadata(object p0)
		{
			return TextRes.GetString("ODataMessageReader_EntitySetSpecifiedWithoutMetadata", new object[] { p0 });
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x00019241 File Offset: 0x00017441
		internal static string ODataMessageReader_OperationImportSpecifiedWithoutMetadata(object p0)
		{
			return TextRes.GetString("ODataMessageReader_OperationImportSpecifiedWithoutMetadata", new object[] { p0 });
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x00019257 File Offset: 0x00017457
		internal static string ODataMessageReader_OperationSpecifiedWithoutMetadata(object p0)
		{
			return TextRes.GetString("ODataMessageReader_OperationSpecifiedWithoutMetadata", new object[] { p0 });
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x0001926D File Offset: 0x0001746D
		internal static string ODataMessageReader_ExpectedCollectionTypeWrongKind(object p0)
		{
			return TextRes.GetString("ODataMessageReader_ExpectedCollectionTypeWrongKind", new object[] { p0 });
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x0600094E RID: 2382 RVA: 0x00019283 File Offset: 0x00017483
		internal static string ODataMessageReader_ExpectedPropertyTypeEntityCollectionKind
		{
			get
			{
				return TextRes.GetString("ODataMessageReader_ExpectedPropertyTypeEntityCollectionKind");
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x0600094F RID: 2383 RVA: 0x0001928F File Offset: 0x0001748F
		internal static string ODataMessageReader_ExpectedPropertyTypeEntityKind
		{
			get
			{
				return TextRes.GetString("ODataMessageReader_ExpectedPropertyTypeEntityKind");
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000950 RID: 2384 RVA: 0x0001929B File Offset: 0x0001749B
		internal static string ODataMessageReader_ExpectedPropertyTypeStream
		{
			get
			{
				return TextRes.GetString("ODataMessageReader_ExpectedPropertyTypeStream");
			}
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x000192A7 File Offset: 0x000174A7
		internal static string ODataMessageReader_ExpectedValueTypeWrongKind(object p0)
		{
			return TextRes.GetString("ODataMessageReader_ExpectedValueTypeWrongKind", new object[] { p0 });
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000952 RID: 2386 RVA: 0x000192BD File Offset: 0x000174BD
		internal static string ODataMessageReader_NoneOrEmptyContentTypeHeader
		{
			get
			{
				return TextRes.GetString("ODataMessageReader_NoneOrEmptyContentTypeHeader");
			}
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x000192C9 File Offset: 0x000174C9
		internal static string ODataMessageReader_WildcardInContentType(object p0)
		{
			return TextRes.GetString("ODataMessageReader_WildcardInContentType", new object[] { p0 });
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000954 RID: 2388 RVA: 0x000192DF File Offset: 0x000174DF
		internal static string ODataMessageReader_GetFormatCalledBeforeReadingStarted
		{
			get
			{
				return TextRes.GetString("ODataMessageReader_GetFormatCalledBeforeReadingStarted");
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000955 RID: 2389 RVA: 0x000192EB File Offset: 0x000174EB
		internal static string ODataMessageReader_DetectPayloadKindMultipleTimes
		{
			get
			{
				return TextRes.GetString("ODataMessageReader_DetectPayloadKindMultipleTimes");
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000956 RID: 2390 RVA: 0x000192F7 File Offset: 0x000174F7
		internal static string ODataMessageReader_PayloadKindDetectionRunning
		{
			get
			{
				return TextRes.GetString("ODataMessageReader_PayloadKindDetectionRunning");
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000957 RID: 2391 RVA: 0x00019303 File Offset: 0x00017503
		internal static string ODataMessageReader_PayloadKindDetectionInServerMode
		{
			get
			{
				return TextRes.GetString("ODataMessageReader_PayloadKindDetectionInServerMode");
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000958 RID: 2392 RVA: 0x0001930F File Offset: 0x0001750F
		internal static string ODataMessageReader_ParameterPayloadInResponse
		{
			get
			{
				return TextRes.GetString("ODataMessageReader_ParameterPayloadInResponse");
			}
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x0001931B File Offset: 0x0001751B
		internal static string ODataMessageReader_SingletonNavigationPropertyForEntityReferenceLinks(object p0, object p1)
		{
			return TextRes.GetString("ODataMessageReader_SingletonNavigationPropertyForEntityReferenceLinks", new object[] { p0, p1 });
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x0600095A RID: 2394 RVA: 0x00019335 File Offset: 0x00017535
		internal static string ODataAsyncResponseMessage_MustNotModifyMessage
		{
			get
			{
				return TextRes.GetString("ODataAsyncResponseMessage_MustNotModifyMessage");
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x0600095B RID: 2395 RVA: 0x00019341 File Offset: 0x00017541
		internal static string ODataMessage_MustNotModifyMessage
		{
			get
			{
				return TextRes.GetString("ODataMessage_MustNotModifyMessage");
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x0600095C RID: 2396 RVA: 0x0001934D File Offset: 0x0001754D
		internal static string ODataReaderCore_SyncCallOnAsyncReader
		{
			get
			{
				return TextRes.GetString("ODataReaderCore_SyncCallOnAsyncReader");
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x0600095D RID: 2397 RVA: 0x00019359 File Offset: 0x00017559
		internal static string ODataReaderCore_AsyncCallOnSyncReader
		{
			get
			{
				return TextRes.GetString("ODataReaderCore_AsyncCallOnSyncReader");
			}
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x00019365 File Offset: 0x00017565
		internal static string ODataReaderCore_ReadOrReadAsyncCalledInInvalidState(object p0)
		{
			return TextRes.GetString("ODataReaderCore_ReadOrReadAsyncCalledInInvalidState", new object[] { p0 });
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x0001937B File Offset: 0x0001757B
		internal static string ODataReaderCore_NoReadCallsAllowed(object p0)
		{
			return TextRes.GetString("ODataReaderCore_NoReadCallsAllowed", new object[] { p0 });
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x00019391 File Offset: 0x00017591
		internal static string ODataJsonReader_CannotReadResourcesOfResourceSet(object p0)
		{
			return TextRes.GetString("ODataJsonReader_CannotReadResourcesOfResourceSet", new object[] { p0 });
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x000193A7 File Offset: 0x000175A7
		internal static string ODataJsonReaderUtils_CannotConvertInt32(object p0)
		{
			return TextRes.GetString("ODataJsonReaderUtils_CannotConvertInt32", new object[] { p0 });
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x000193BD File Offset: 0x000175BD
		internal static string ODataJsonReaderUtils_CannotConvertDouble(object p0)
		{
			return TextRes.GetString("ODataJsonReaderUtils_CannotConvertDouble", new object[] { p0 });
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x000193D3 File Offset: 0x000175D3
		internal static string ODataJsonReaderUtils_CannotConvertBoolean(object p0)
		{
			return TextRes.GetString("ODataJsonReaderUtils_CannotConvertBoolean", new object[] { p0 });
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x000193E9 File Offset: 0x000175E9
		internal static string ODataJsonReaderUtils_CannotConvertDecimal(object p0)
		{
			return TextRes.GetString("ODataJsonReaderUtils_CannotConvertDecimal", new object[] { p0 });
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x000193FF File Offset: 0x000175FF
		internal static string ODataJsonReaderUtils_CannotConvertDateTime(object p0)
		{
			return TextRes.GetString("ODataJsonReaderUtils_CannotConvertDateTime", new object[] { p0 });
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x00019415 File Offset: 0x00017615
		internal static string ODataJsonReaderUtils_CannotConvertDateTimeOffset(object p0)
		{
			return TextRes.GetString("ODataJsonReaderUtils_CannotConvertDateTimeOffset", new object[] { p0 });
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x0001942B File Offset: 0x0001762B
		internal static string ODataJsonReaderUtils_ConflictBetweenInputFormatAndParameter(object p0)
		{
			return TextRes.GetString("ODataJsonReaderUtils_ConflictBetweenInputFormatAndParameter", new object[] { p0 });
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x00019441 File Offset: 0x00017641
		internal static string ODataJsonReaderUtils_MultipleErrorPropertiesWithSameName(object p0)
		{
			return TextRes.GetString("ODataJsonReaderUtils_MultipleErrorPropertiesWithSameName", new object[] { p0 });
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x00019457 File Offset: 0x00017657
		internal static string ODataJsonLightResourceSerializer_ActionsAndFunctionsGroupMustSpecifyTarget(object p0)
		{
			return TextRes.GetString("ODataJsonLightResourceSerializer_ActionsAndFunctionsGroupMustSpecifyTarget", new object[] { p0 });
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x0001946D File Offset: 0x0001766D
		internal static string ODataJsonLightResourceSerializer_ActionsAndFunctionsGroupMustNotHaveDuplicateTarget(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightResourceSerializer_ActionsAndFunctionsGroupMustNotHaveDuplicateTarget", new object[] { p0, p1 });
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x00019487 File Offset: 0x00017687
		internal static string ODataJsonErrorDeserializer_TopLevelErrorWithInvalidProperty(object p0)
		{
			return TextRes.GetString("ODataJsonErrorDeserializer_TopLevelErrorWithInvalidProperty", new object[] { p0 });
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x0001949D File Offset: 0x0001769D
		internal static string ODataJsonErrorDeserializer_TopLevelErrorMessageValueWithInvalidProperty(object p0)
		{
			return TextRes.GetString("ODataJsonErrorDeserializer_TopLevelErrorMessageValueWithInvalidProperty", new object[] { p0 });
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x000194B3 File Offset: 0x000176B3
		internal static string ODataCollectionReaderCore_ReadOrReadAsyncCalledInInvalidState(object p0)
		{
			return TextRes.GetString("ODataCollectionReaderCore_ReadOrReadAsyncCalledInInvalidState", new object[] { p0 });
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x0600096E RID: 2414 RVA: 0x000194C9 File Offset: 0x000176C9
		internal static string ODataCollectionReaderCore_SyncCallOnAsyncReader
		{
			get
			{
				return TextRes.GetString("ODataCollectionReaderCore_SyncCallOnAsyncReader");
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x0600096F RID: 2415 RVA: 0x000194D5 File Offset: 0x000176D5
		internal static string ODataCollectionReaderCore_AsyncCallOnSyncReader
		{
			get
			{
				return TextRes.GetString("ODataCollectionReaderCore_AsyncCallOnSyncReader");
			}
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x000194E1 File Offset: 0x000176E1
		internal static string ODataCollectionReaderCore_ExpectedItemTypeSetInInvalidState(object p0, object p1)
		{
			return TextRes.GetString("ODataCollectionReaderCore_ExpectedItemTypeSetInInvalidState", new object[] { p0, p1 });
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x000194FB File Offset: 0x000176FB
		internal static string ODataParameterReaderCore_ReadOrReadAsyncCalledInInvalidState(object p0)
		{
			return TextRes.GetString("ODataParameterReaderCore_ReadOrReadAsyncCalledInInvalidState", new object[] { p0 });
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000972 RID: 2418 RVA: 0x00019511 File Offset: 0x00017711
		internal static string ODataParameterReaderCore_SyncCallOnAsyncReader
		{
			get
			{
				return TextRes.GetString("ODataParameterReaderCore_SyncCallOnAsyncReader");
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000973 RID: 2419 RVA: 0x0001951D File Offset: 0x0001771D
		internal static string ODataParameterReaderCore_AsyncCallOnSyncReader
		{
			get
			{
				return TextRes.GetString("ODataParameterReaderCore_AsyncCallOnSyncReader");
			}
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x00019529 File Offset: 0x00017729
		internal static string ODataParameterReaderCore_SubReaderMustBeCreatedAndReadToCompletionBeforeTheNextReadOrReadAsyncCall(object p0, object p1)
		{
			return TextRes.GetString("ODataParameterReaderCore_SubReaderMustBeCreatedAndReadToCompletionBeforeTheNextReadOrReadAsyncCall", new object[] { p0, p1 });
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x00019543 File Offset: 0x00017743
		internal static string ODataParameterReaderCore_SubReaderMustBeInCompletedStateBeforeTheNextReadOrReadAsyncCall(object p0, object p1)
		{
			return TextRes.GetString("ODataParameterReaderCore_SubReaderMustBeInCompletedStateBeforeTheNextReadOrReadAsyncCall", new object[] { p0, p1 });
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x0001955D File Offset: 0x0001775D
		internal static string ODataParameterReaderCore_InvalidCreateReaderMethodCalledForState(object p0, object p1)
		{
			return TextRes.GetString("ODataParameterReaderCore_InvalidCreateReaderMethodCalledForState", new object[] { p0, p1 });
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x00019577 File Offset: 0x00017777
		internal static string ODataParameterReaderCore_CreateReaderAlreadyCalled(object p0, object p1)
		{
			return TextRes.GetString("ODataParameterReaderCore_CreateReaderAlreadyCalled", new object[] { p0, p1 });
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x00019591 File Offset: 0x00017791
		internal static string ODataParameterReaderCore_ParameterNameNotInMetadata(object p0, object p1)
		{
			return TextRes.GetString("ODataParameterReaderCore_ParameterNameNotInMetadata", new object[] { p0, p1 });
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x000195AB File Offset: 0x000177AB
		internal static string ODataParameterReaderCore_DuplicateParametersInPayload(object p0)
		{
			return TextRes.GetString("ODataParameterReaderCore_DuplicateParametersInPayload", new object[] { p0 });
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x000195C1 File Offset: 0x000177C1
		internal static string ODataParameterReaderCore_ParametersMissingInPayload(object p0, object p1)
		{
			return TextRes.GetString("ODataParameterReaderCore_ParametersMissingInPayload", new object[] { p0, p1 });
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x000195DB File Offset: 0x000177DB
		internal static string ValidationUtils_ActionsAndFunctionsMustSpecifyMetadata(object p0)
		{
			return TextRes.GetString("ValidationUtils_ActionsAndFunctionsMustSpecifyMetadata", new object[] { p0 });
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x000195F1 File Offset: 0x000177F1
		internal static string ValidationUtils_ActionsAndFunctionsMustSpecifyTarget(object p0)
		{
			return TextRes.GetString("ValidationUtils_ActionsAndFunctionsMustSpecifyTarget", new object[] { p0 });
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x00019607 File Offset: 0x00017807
		internal static string ValidationUtils_EnumerableContainsANullItem(object p0)
		{
			return TextRes.GetString("ValidationUtils_EnumerableContainsANullItem", new object[] { p0 });
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x0600097E RID: 2430 RVA: 0x0001961D File Offset: 0x0001781D
		internal static string ValidationUtils_AssociationLinkMustSpecifyName
		{
			get
			{
				return TextRes.GetString("ValidationUtils_AssociationLinkMustSpecifyName");
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x0600097F RID: 2431 RVA: 0x00019629 File Offset: 0x00017829
		internal static string ValidationUtils_AssociationLinkMustSpecifyUrl
		{
			get
			{
				return TextRes.GetString("ValidationUtils_AssociationLinkMustSpecifyUrl");
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000980 RID: 2432 RVA: 0x00019635 File Offset: 0x00017835
		internal static string ValidationUtils_TypeNameMustNotBeEmpty
		{
			get
			{
				return TextRes.GetString("ValidationUtils_TypeNameMustNotBeEmpty");
			}
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x00019641 File Offset: 0x00017841
		internal static string ValidationUtils_PropertyDoesNotExistOnType(object p0, object p1)
		{
			return TextRes.GetString("ValidationUtils_PropertyDoesNotExistOnType", new object[] { p0, p1 });
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000982 RID: 2434 RVA: 0x0001965B File Offset: 0x0001785B
		internal static string ValidationUtils_ResourceMustSpecifyUrl
		{
			get
			{
				return TextRes.GetString("ValidationUtils_ResourceMustSpecifyUrl");
			}
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x00019667 File Offset: 0x00017867
		internal static string ValidationUtils_ResourceMustSpecifyName(object p0)
		{
			return TextRes.GetString("ValidationUtils_ResourceMustSpecifyName", new object[] { p0 });
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000984 RID: 2436 RVA: 0x0001967D File Offset: 0x0001787D
		internal static string ValidationUtils_ServiceDocumentElementUrlMustNotBeNull
		{
			get
			{
				return TextRes.GetString("ValidationUtils_ServiceDocumentElementUrlMustNotBeNull");
			}
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x00019689 File Offset: 0x00017889
		internal static string ValidationUtils_NonPrimitiveTypeForPrimitiveValue(object p0)
		{
			return TextRes.GetString("ValidationUtils_NonPrimitiveTypeForPrimitiveValue", new object[] { p0 });
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x0001969F File Offset: 0x0001789F
		internal static string ValidationUtils_UnsupportedPrimitiveType(object p0)
		{
			return TextRes.GetString("ValidationUtils_UnsupportedPrimitiveType", new object[] { p0 });
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x000196B5 File Offset: 0x000178B5
		internal static string ValidationUtils_IncompatiblePrimitiveItemType(object p0, object p1, object p2, object p3)
		{
			return TextRes.GetString("ValidationUtils_IncompatiblePrimitiveItemType", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000988 RID: 2440 RVA: 0x000196D7 File Offset: 0x000178D7
		internal static string ValidationUtils_NonNullableCollectionElementsMustNotBeNull
		{
			get
			{
				return TextRes.GetString("ValidationUtils_NonNullableCollectionElementsMustNotBeNull");
			}
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x000196E3 File Offset: 0x000178E3
		internal static string ValidationUtils_InvalidCollectionTypeName(object p0)
		{
			return TextRes.GetString("ValidationUtils_InvalidCollectionTypeName", new object[] { p0 });
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x000196F9 File Offset: 0x000178F9
		internal static string ValidationUtils_UnrecognizedTypeName(object p0)
		{
			return TextRes.GetString("ValidationUtils_UnrecognizedTypeName", new object[] { p0 });
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x0001970F File Offset: 0x0001790F
		internal static string ValidationUtils_IncorrectTypeKind(object p0, object p1, object p2)
		{
			return TextRes.GetString("ValidationUtils_IncorrectTypeKind", new object[] { p0, p1, p2 });
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x0001972D File Offset: 0x0001792D
		internal static string ValidationUtils_IncorrectTypeKindNoTypeName(object p0, object p1)
		{
			return TextRes.GetString("ValidationUtils_IncorrectTypeKindNoTypeName", new object[] { p0, p1 });
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x00019747 File Offset: 0x00017947
		internal static string ValidationUtils_IncorrectValueTypeKind(object p0, object p1)
		{
			return TextRes.GetString("ValidationUtils_IncorrectValueTypeKind", new object[] { p0, p1 });
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x0600098E RID: 2446 RVA: 0x00019761 File Offset: 0x00017961
		internal static string ValidationUtils_LinkMustSpecifyName
		{
			get
			{
				return TextRes.GetString("ValidationUtils_LinkMustSpecifyName");
			}
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x0001976D File Offset: 0x0001796D
		internal static string ValidationUtils_MismatchPropertyKindForStreamProperty(object p0)
		{
			return TextRes.GetString("ValidationUtils_MismatchPropertyKindForStreamProperty", new object[] { p0 });
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000990 RID: 2448 RVA: 0x00019783 File Offset: 0x00017983
		internal static string ValidationUtils_NestedCollectionsAreNotSupported
		{
			get
			{
				return TextRes.GetString("ValidationUtils_NestedCollectionsAreNotSupported");
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000991 RID: 2449 RVA: 0x0001978F File Offset: 0x0001798F
		internal static string ValidationUtils_StreamReferenceValuesNotSupportedInCollections
		{
			get
			{
				return TextRes.GetString("ValidationUtils_StreamReferenceValuesNotSupportedInCollections");
			}
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x0001979B File Offset: 0x0001799B
		internal static string ValidationUtils_IncompatibleType(object p0, object p1)
		{
			return TextRes.GetString("ValidationUtils_IncompatibleType", new object[] { p0, p1 });
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x000197B5 File Offset: 0x000179B5
		internal static string ValidationUtils_OpenCollectionProperty(object p0)
		{
			return TextRes.GetString("ValidationUtils_OpenCollectionProperty", new object[] { p0 });
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x000197CB File Offset: 0x000179CB
		internal static string ValidationUtils_OpenStreamProperty(object p0)
		{
			return TextRes.GetString("ValidationUtils_OpenStreamProperty", new object[] { p0 });
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x000197E1 File Offset: 0x000179E1
		internal static string ValidationUtils_InvalidCollectionTypeReference(object p0)
		{
			return TextRes.GetString("ValidationUtils_InvalidCollectionTypeReference", new object[] { p0 });
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x000197F7 File Offset: 0x000179F7
		internal static string ValidationUtils_ResourceWithMediaResourceAndNonMLEType(object p0)
		{
			return TextRes.GetString("ValidationUtils_ResourceWithMediaResourceAndNonMLEType", new object[] { p0 });
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x0001980D File Offset: 0x00017A0D
		internal static string ValidationUtils_ResourceWithoutMediaResourceAndMLEType(object p0)
		{
			return TextRes.GetString("ValidationUtils_ResourceWithoutMediaResourceAndMLEType", new object[] { p0 });
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x00019823 File Offset: 0x00017A23
		internal static string ValidationUtils_ResourceTypeNotAssignableToExpectedType(object p0, object p1)
		{
			return TextRes.GetString("ValidationUtils_ResourceTypeNotAssignableToExpectedType", new object[] { p0, p1 });
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x0001983D File Offset: 0x00017A3D
		internal static string ValidationUtils_NavigationPropertyExpected(object p0, object p1, object p2)
		{
			return TextRes.GetString("ValidationUtils_NavigationPropertyExpected", new object[] { p0, p1, p2 });
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x0001985B File Offset: 0x00017A5B
		internal static string ValidationUtils_InvalidBatchBoundaryDelimiterLength(object p0, object p1)
		{
			return TextRes.GetString("ValidationUtils_InvalidBatchBoundaryDelimiterLength", new object[] { p0, p1 });
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x00019875 File Offset: 0x00017A75
		internal static string ValidationUtils_RecursionDepthLimitReached(object p0)
		{
			return TextRes.GetString("ValidationUtils_RecursionDepthLimitReached", new object[] { p0 });
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x0001988B File Offset: 0x00017A8B
		internal static string ValidationUtils_MaxDepthOfNestedEntriesExceeded(object p0)
		{
			return TextRes.GetString("ValidationUtils_MaxDepthOfNestedEntriesExceeded", new object[] { p0 });
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x000198A1 File Offset: 0x00017AA1
		internal static string ValidationUtils_NullCollectionItemForNonNullableType(object p0)
		{
			return TextRes.GetString("ValidationUtils_NullCollectionItemForNonNullableType", new object[] { p0 });
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x000198B7 File Offset: 0x00017AB7
		internal static string ValidationUtils_PropertiesMustNotContainReservedChars(object p0, object p1)
		{
			return TextRes.GetString("ValidationUtils_PropertiesMustNotContainReservedChars", new object[] { p0, p1 });
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x0600099F RID: 2463 RVA: 0x000198D1 File Offset: 0x00017AD1
		internal static string ValidationUtils_WorkspaceResourceMustNotContainNullItem
		{
			get
			{
				return TextRes.GetString("ValidationUtils_WorkspaceResourceMustNotContainNullItem");
			}
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x000198DD File Offset: 0x00017ADD
		internal static string ValidationUtils_InvalidMetadataReferenceProperty(object p0)
		{
			return TextRes.GetString("ValidationUtils_InvalidMetadataReferenceProperty", new object[] { p0 });
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x060009A1 RID: 2465 RVA: 0x000198F3 File Offset: 0x00017AF3
		internal static string WriterValidationUtils_PropertyMustNotBeNull
		{
			get
			{
				return TextRes.GetString("WriterValidationUtils_PropertyMustNotBeNull");
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x060009A2 RID: 2466 RVA: 0x000198FF File Offset: 0x00017AFF
		internal static string WriterValidationUtils_PropertiesMustHaveNonEmptyName
		{
			get
			{
				return TextRes.GetString("WriterValidationUtils_PropertiesMustHaveNonEmptyName");
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x060009A3 RID: 2467 RVA: 0x0001990B File Offset: 0x00017B0B
		internal static string WriterValidationUtils_MissingTypeNameWithMetadata
		{
			get
			{
				return TextRes.GetString("WriterValidationUtils_MissingTypeNameWithMetadata");
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x060009A4 RID: 2468 RVA: 0x00019917 File Offset: 0x00017B17
		internal static string WriterValidationUtils_NextPageLinkInRequest
		{
			get
			{
				return TextRes.GetString("WriterValidationUtils_NextPageLinkInRequest");
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x060009A5 RID: 2469 RVA: 0x00019923 File Offset: 0x00017B23
		internal static string WriterValidationUtils_DefaultStreamWithContentTypeWithoutReadLink
		{
			get
			{
				return TextRes.GetString("WriterValidationUtils_DefaultStreamWithContentTypeWithoutReadLink");
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x060009A6 RID: 2470 RVA: 0x0001992F File Offset: 0x00017B2F
		internal static string WriterValidationUtils_DefaultStreamWithReadLinkWithoutContentType
		{
			get
			{
				return TextRes.GetString("WriterValidationUtils_DefaultStreamWithReadLinkWithoutContentType");
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x060009A7 RID: 2471 RVA: 0x0001993B File Offset: 0x00017B3B
		internal static string WriterValidationUtils_StreamReferenceValueMustHaveEditLinkOrReadLink
		{
			get
			{
				return TextRes.GetString("WriterValidationUtils_StreamReferenceValueMustHaveEditLinkOrReadLink");
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x060009A8 RID: 2472 RVA: 0x00019947 File Offset: 0x00017B47
		internal static string WriterValidationUtils_StreamReferenceValueMustHaveEditLinkToHaveETag
		{
			get
			{
				return TextRes.GetString("WriterValidationUtils_StreamReferenceValueMustHaveEditLinkToHaveETag");
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x060009A9 RID: 2473 RVA: 0x00019953 File Offset: 0x00017B53
		internal static string WriterValidationUtils_StreamReferenceValueEmptyContentType
		{
			get
			{
				return TextRes.GetString("WriterValidationUtils_StreamReferenceValueEmptyContentType");
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x060009AA RID: 2474 RVA: 0x0001995F File Offset: 0x00017B5F
		internal static string WriterValidationUtils_EntriesMustHaveNonEmptyId
		{
			get
			{
				return TextRes.GetString("WriterValidationUtils_EntriesMustHaveNonEmptyId");
			}
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x0001996B File Offset: 0x00017B6B
		internal static string WriterValidationUtils_MessageWriterSettingsBaseUriMustBeNullOrAbsolute(object p0)
		{
			return TextRes.GetString("WriterValidationUtils_MessageWriterSettingsBaseUriMustBeNullOrAbsolute", new object[] { p0 });
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x060009AC RID: 2476 RVA: 0x00019981 File Offset: 0x00017B81
		internal static string WriterValidationUtils_EntityReferenceLinkUrlMustNotBeNull
		{
			get
			{
				return TextRes.GetString("WriterValidationUtils_EntityReferenceLinkUrlMustNotBeNull");
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x060009AD RID: 2477 RVA: 0x0001998D File Offset: 0x00017B8D
		internal static string WriterValidationUtils_EntityReferenceLinksLinkMustNotBeNull
		{
			get
			{
				return TextRes.GetString("WriterValidationUtils_EntityReferenceLinksLinkMustNotBeNull");
			}
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x00019999 File Offset: 0x00017B99
		internal static string WriterValidationUtils_NestedResourceTypeNotCompatibleWithParentPropertyType(object p0, object p1)
		{
			return TextRes.GetString("WriterValidationUtils_NestedResourceTypeNotCompatibleWithParentPropertyType", new object[] { p0, p1 });
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x000199B3 File Offset: 0x00017BB3
		internal static string WriterValidationUtils_ExpandedLinkIsCollectionTrueWithResourceContent(object p0)
		{
			return TextRes.GetString("WriterValidationUtils_ExpandedLinkIsCollectionTrueWithResourceContent", new object[] { p0 });
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x000199C9 File Offset: 0x00017BC9
		internal static string WriterValidationUtils_ExpandedLinkIsCollectionFalseWithResourceSetContent(object p0)
		{
			return TextRes.GetString("WriterValidationUtils_ExpandedLinkIsCollectionFalseWithResourceSetContent", new object[] { p0 });
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x000199DF File Offset: 0x00017BDF
		internal static string WriterValidationUtils_ExpandedLinkIsCollectionTrueWithResourceMetadata(object p0)
		{
			return TextRes.GetString("WriterValidationUtils_ExpandedLinkIsCollectionTrueWithResourceMetadata", new object[] { p0 });
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x000199F5 File Offset: 0x00017BF5
		internal static string WriterValidationUtils_ExpandedLinkIsCollectionFalseWithResourceSetMetadata(object p0)
		{
			return TextRes.GetString("WriterValidationUtils_ExpandedLinkIsCollectionFalseWithResourceSetMetadata", new object[] { p0 });
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x00019A0B File Offset: 0x00017C0B
		internal static string WriterValidationUtils_ExpandedLinkWithResourceSetPayloadAndResourceMetadata(object p0)
		{
			return TextRes.GetString("WriterValidationUtils_ExpandedLinkWithResourceSetPayloadAndResourceMetadata", new object[] { p0 });
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x00019A21 File Offset: 0x00017C21
		internal static string WriterValidationUtils_ExpandedLinkWithResourcePayloadAndResourceSetMetadata(object p0)
		{
			return TextRes.GetString("WriterValidationUtils_ExpandedLinkWithResourcePayloadAndResourceSetMetadata", new object[] { p0 });
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x00019A37 File Offset: 0x00017C37
		internal static string WriterValidationUtils_CollectionPropertiesMustNotHaveNullValue(object p0)
		{
			return TextRes.GetString("WriterValidationUtils_CollectionPropertiesMustNotHaveNullValue", new object[] { p0 });
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x00019A4D File Offset: 0x00017C4D
		internal static string WriterValidationUtils_NonNullablePropertiesMustNotHaveNullValue(object p0, object p1)
		{
			return TextRes.GetString("WriterValidationUtils_NonNullablePropertiesMustNotHaveNullValue", new object[] { p0, p1 });
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x00019A67 File Offset: 0x00017C67
		internal static string WriterValidationUtils_StreamPropertiesMustNotHaveNullValue(object p0)
		{
			return TextRes.GetString("WriterValidationUtils_StreamPropertiesMustNotHaveNullValue", new object[] { p0 });
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x00019A7D File Offset: 0x00017C7D
		internal static string WriterValidationUtils_OperationInRequest(object p0)
		{
			return TextRes.GetString("WriterValidationUtils_OperationInRequest", new object[] { p0 });
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x00019A93 File Offset: 0x00017C93
		internal static string WriterValidationUtils_AssociationLinkInRequest(object p0)
		{
			return TextRes.GetString("WriterValidationUtils_AssociationLinkInRequest", new object[] { p0 });
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x00019AA9 File Offset: 0x00017CA9
		internal static string WriterValidationUtils_StreamPropertyInRequest(object p0)
		{
			return TextRes.GetString("WriterValidationUtils_StreamPropertyInRequest", new object[] { p0 });
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x00019ABF File Offset: 0x00017CBF
		internal static string WriterValidationUtils_MessageWriterSettingsServiceDocumentUriMustBeNullOrAbsolute(object p0)
		{
			return TextRes.GetString("WriterValidationUtils_MessageWriterSettingsServiceDocumentUriMustBeNullOrAbsolute", new object[] { p0 });
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x00019AD5 File Offset: 0x00017CD5
		internal static string WriterValidationUtils_NavigationLinkMustSpecifyUrl(object p0)
		{
			return TextRes.GetString("WriterValidationUtils_NavigationLinkMustSpecifyUrl", new object[] { p0 });
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x00019AEB File Offset: 0x00017CEB
		internal static string WriterValidationUtils_NestedResourceInfoMustSpecifyIsCollection(object p0)
		{
			return TextRes.GetString("WriterValidationUtils_NestedResourceInfoMustSpecifyIsCollection", new object[] { p0 });
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x060009BE RID: 2494 RVA: 0x00019B01 File Offset: 0x00017D01
		internal static string WriterValidationUtils_MessageWriterSettingsJsonPaddingOnRequestMessage
		{
			get
			{
				return TextRes.GetString("WriterValidationUtils_MessageWriterSettingsJsonPaddingOnRequestMessage");
			}
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x00019B0D File Offset: 0x00017D0D
		internal static string XmlReaderExtension_InvalidNodeInStringValue(object p0)
		{
			return TextRes.GetString("XmlReaderExtension_InvalidNodeInStringValue", new object[] { p0 });
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x00019B23 File Offset: 0x00017D23
		internal static string XmlReaderExtension_InvalidRootNode(object p0)
		{
			return TextRes.GetString("XmlReaderExtension_InvalidRootNode", new object[] { p0 });
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x00019B39 File Offset: 0x00017D39
		internal static string ODataMetadataInputContext_ErrorReadingMetadata(object p0)
		{
			return TextRes.GetString("ODataMetadataInputContext_ErrorReadingMetadata", new object[] { p0 });
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x00019B4F File Offset: 0x00017D4F
		internal static string ODataMetadataOutputContext_ErrorWritingMetadata(object p0)
		{
			return TextRes.GetString("ODataMetadataOutputContext_ErrorWritingMetadata", new object[] { p0 });
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x00019B65 File Offset: 0x00017D65
		internal static string ODataAtomDeserializer_RelativeUriUsedWithoutBaseUriSpecified(object p0)
		{
			return TextRes.GetString("ODataAtomDeserializer_RelativeUriUsedWithoutBaseUriSpecified", new object[] { p0 });
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x00019B7B File Offset: 0x00017D7B
		internal static string ODataAtomPropertyAndValueDeserializer_InvalidCollectionElement(object p0, object p1)
		{
			return TextRes.GetString("ODataAtomPropertyAndValueDeserializer_InvalidCollectionElement", new object[] { p0, p1 });
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x00019B95 File Offset: 0x00017D95
		internal static string ODataAtomPropertyAndValueDeserializer_NavigationPropertyInProperties(object p0, object p1)
		{
			return TextRes.GetString("ODataAtomPropertyAndValueDeserializer_NavigationPropertyInProperties", new object[] { p0, p1 });
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x00019BAF File Offset: 0x00017DAF
		internal static string JsonLightInstanceAnnotationWriter_NullValueNotAllowedForInstanceAnnotation(object p0, object p1)
		{
			return TextRes.GetString("JsonLightInstanceAnnotationWriter_NullValueNotAllowedForInstanceAnnotation", new object[] { p0, p1 });
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x00019BC9 File Offset: 0x00017DC9
		internal static string EdmLibraryExtensions_OperationGroupReturningActionsAndFunctionsModelInvalid(object p0)
		{
			return TextRes.GetString("EdmLibraryExtensions_OperationGroupReturningActionsAndFunctionsModelInvalid", new object[] { p0 });
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x00019BDF File Offset: 0x00017DDF
		internal static string EdmLibraryExtensions_UnBoundOperationsFoundFromIEdmModelFindMethodIsInvalid(object p0)
		{
			return TextRes.GetString("EdmLibraryExtensions_UnBoundOperationsFoundFromIEdmModelFindMethodIsInvalid", new object[] { p0 });
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x00019BF5 File Offset: 0x00017DF5
		internal static string EdmLibraryExtensions_NoParameterBoundOperationsFoundFromIEdmModelFindMethodIsInvalid(object p0)
		{
			return TextRes.GetString("EdmLibraryExtensions_NoParameterBoundOperationsFoundFromIEdmModelFindMethodIsInvalid", new object[] { p0 });
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x00019C0B File Offset: 0x00017E0B
		internal static string EdmLibraryExtensions_ValueOverflowForUnderlyingType(object p0, object p1)
		{
			return TextRes.GetString("EdmLibraryExtensions_ValueOverflowForUnderlyingType", new object[] { p0, p1 });
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x00019C25 File Offset: 0x00017E25
		internal static string ODataAtomResourceDeserializer_ContentWithWrongType(object p0)
		{
			return TextRes.GetString("ODataAtomResourceDeserializer_ContentWithWrongType", new object[] { p0 });
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x00019C3B File Offset: 0x00017E3B
		internal static string ODataAtomErrorDeserializer_MultipleErrorElementsWithSameName(object p0)
		{
			return TextRes.GetString("ODataAtomErrorDeserializer_MultipleErrorElementsWithSameName", new object[] { p0 });
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x00019C51 File Offset: 0x00017E51
		internal static string ODataAtomErrorDeserializer_MultipleInnerErrorElementsWithSameName(object p0)
		{
			return TextRes.GetString("ODataAtomErrorDeserializer_MultipleInnerErrorElementsWithSameName", new object[] { p0 });
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x00019C67 File Offset: 0x00017E67
		internal static string CollectionWithoutExpectedTypeValidator_InvalidItemTypeKind(object p0)
		{
			return TextRes.GetString("CollectionWithoutExpectedTypeValidator_InvalidItemTypeKind", new object[] { p0 });
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x00019C7D File Offset: 0x00017E7D
		internal static string CollectionWithoutExpectedTypeValidator_IncompatibleItemTypeKind(object p0, object p1)
		{
			return TextRes.GetString("CollectionWithoutExpectedTypeValidator_IncompatibleItemTypeKind", new object[] { p0, p1 });
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x00019C97 File Offset: 0x00017E97
		internal static string CollectionWithoutExpectedTypeValidator_IncompatibleItemTypeName(object p0, object p1)
		{
			return TextRes.GetString("CollectionWithoutExpectedTypeValidator_IncompatibleItemTypeName", new object[] { p0, p1 });
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x00019CB1 File Offset: 0x00017EB1
		internal static string ResourceSetWithoutExpectedTypeValidator_IncompatibleTypes(object p0, object p1)
		{
			return TextRes.GetString("ResourceSetWithoutExpectedTypeValidator_IncompatibleTypes", new object[] { p0, p1 });
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x00019CCB File Offset: 0x00017ECB
		internal static string MessageStreamWrappingStream_ByteLimitExceeded(object p0, object p1)
		{
			return TextRes.GetString("MessageStreamWrappingStream_ByteLimitExceeded", new object[] { p0, p1 });
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x00019CE5 File Offset: 0x00017EE5
		internal static string MetadataUtils_ResolveTypeName(object p0)
		{
			return TextRes.GetString("MetadataUtils_ResolveTypeName", new object[] { p0 });
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x00019CFB File Offset: 0x00017EFB
		internal static string MetadataUtils_CalculateBindableOperationsForType(object p0)
		{
			return TextRes.GetString("MetadataUtils_CalculateBindableOperationsForType", new object[] { p0 });
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x00019D11 File Offset: 0x00017F11
		internal static string EdmValueUtils_UnsupportedPrimitiveType(object p0)
		{
			return TextRes.GetString("EdmValueUtils_UnsupportedPrimitiveType", new object[] { p0 });
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x00019D27 File Offset: 0x00017F27
		internal static string EdmValueUtils_IncorrectPrimitiveTypeKind(object p0, object p1, object p2)
		{
			return TextRes.GetString("EdmValueUtils_IncorrectPrimitiveTypeKind", new object[] { p0, p1, p2 });
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x00019D45 File Offset: 0x00017F45
		internal static string EdmValueUtils_IncorrectPrimitiveTypeKindNoTypeName(object p0, object p1)
		{
			return TextRes.GetString("EdmValueUtils_IncorrectPrimitiveTypeKindNoTypeName", new object[] { p0, p1 });
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x00019D5F File Offset: 0x00017F5F
		internal static string EdmValueUtils_CannotConvertTypeToClrValue(object p0)
		{
			return TextRes.GetString("EdmValueUtils_CannotConvertTypeToClrValue", new object[] { p0 });
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x00019D75 File Offset: 0x00017F75
		internal static string ODataEdmStructuredValue_UndeclaredProperty(object p0, object p1)
		{
			return TextRes.GetString("ODataEdmStructuredValue_UndeclaredProperty", new object[] { p0, p1 });
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x00019D8F File Offset: 0x00017F8F
		internal static string ODataMetadataBuilder_MissingEntitySetUri(object p0)
		{
			return TextRes.GetString("ODataMetadataBuilder_MissingEntitySetUri", new object[] { p0 });
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x00019DA5 File Offset: 0x00017FA5
		internal static string ODataMetadataBuilder_MissingSegmentForEntitySetUriSuffix(object p0, object p1)
		{
			return TextRes.GetString("ODataMetadataBuilder_MissingSegmentForEntitySetUriSuffix", new object[] { p0, p1 });
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x00019DBF File Offset: 0x00017FBF
		internal static string ODataMetadataBuilder_MissingEntityInstanceUri(object p0)
		{
			return TextRes.GetString("ODataMetadataBuilder_MissingEntityInstanceUri", new object[] { p0 });
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x060009DD RID: 2525 RVA: 0x00019DD5 File Offset: 0x00017FD5
		internal static string ODataMetadataBuilder_MissingParentIdOrContextUrl
		{
			get
			{
				return TextRes.GetString("ODataMetadataBuilder_MissingParentIdOrContextUrl");
			}
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x00019DE1 File Offset: 0x00017FE1
		internal static string ODataMetadataBuilder_UnknownEntitySet(object p0)
		{
			return TextRes.GetString("ODataMetadataBuilder_UnknownEntitySet", new object[] { p0 });
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x00019DF7 File Offset: 0x00017FF7
		internal static string ODataJsonLightInputContext_EntityTypeMustBeCompatibleWithEntitySetBaseType(object p0, object p1, object p2)
		{
			return TextRes.GetString("ODataJsonLightInputContext_EntityTypeMustBeCompatibleWithEntitySetBaseType", new object[] { p0, p1, p2 });
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x060009E0 RID: 2528 RVA: 0x00019E15 File Offset: 0x00018015
		internal static string ODataJsonLightInputContext_PayloadKindDetectionForRequest
		{
			get
			{
				return TextRes.GetString("ODataJsonLightInputContext_PayloadKindDetectionForRequest");
			}
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x00019E21 File Offset: 0x00018021
		internal static string ODataJsonLightInputContext_OperationCannotBeNullForCreateParameterReader(object p0)
		{
			return TextRes.GetString("ODataJsonLightInputContext_OperationCannotBeNullForCreateParameterReader", new object[] { p0 });
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x060009E2 RID: 2530 RVA: 0x00019E37 File Offset: 0x00018037
		internal static string ODataJsonLightInputContext_NoEntitySetForRequest
		{
			get
			{
				return TextRes.GetString("ODataJsonLightInputContext_NoEntitySetForRequest");
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x060009E3 RID: 2531 RVA: 0x00019E43 File Offset: 0x00018043
		internal static string ODataJsonLightInputContext_ModelRequiredForReading
		{
			get
			{
				return TextRes.GetString("ODataJsonLightInputContext_ModelRequiredForReading");
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x060009E4 RID: 2532 RVA: 0x00019E4F File Offset: 0x0001804F
		internal static string ODataJsonLightInputContext_ItemTypeRequiredForCollectionReaderInRequests
		{
			get
			{
				return TextRes.GetString("ODataJsonLightInputContext_ItemTypeRequiredForCollectionReaderInRequests");
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x060009E5 RID: 2533 RVA: 0x00019E5B File Offset: 0x0001805B
		internal static string ODataJsonLightDeserializer_ContextLinkNotFoundAsFirstProperty
		{
			get
			{
				return TextRes.GetString("ODataJsonLightDeserializer_ContextLinkNotFoundAsFirstProperty");
			}
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x00019E67 File Offset: 0x00018067
		internal static string ODataJsonLightDeserializer_OnlyODataTypeAnnotationCanTargetInstanceAnnotation(object p0, object p1, object p2)
		{
			return TextRes.GetString("ODataJsonLightDeserializer_OnlyODataTypeAnnotationCanTargetInstanceAnnotation", new object[] { p0, p1, p2 });
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x00019E85 File Offset: 0x00018085
		internal static string ODataJsonLightDeserializer_AnnotationTargetingInstanceAnnotationWithoutValue(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightDeserializer_AnnotationTargetingInstanceAnnotationWithoutValue", new object[] { p0, p1 });
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x060009E8 RID: 2536 RVA: 0x00019E9F File Offset: 0x0001809F
		internal static string ODataJsonLightWriter_EntityReferenceLinkAfterResourceSetInRequest
		{
			get
			{
				return TextRes.GetString("ODataJsonLightWriter_EntityReferenceLinkAfterResourceSetInRequest");
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x060009E9 RID: 2537 RVA: 0x00019EAB File Offset: 0x000180AB
		internal static string ODataJsonLightWriter_InstanceAnnotationNotSupportedOnExpandedResourceSet
		{
			get
			{
				return TextRes.GetString("ODataJsonLightWriter_InstanceAnnotationNotSupportedOnExpandedResourceSet");
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x060009EA RID: 2538 RVA: 0x00019EB7 File Offset: 0x000180B7
		internal static string ODataJsonLightPropertyAndValueSerializer_NoExpectedTypeOrTypeNameSpecifiedForComplexValueRequest
		{
			get
			{
				return TextRes.GetString("ODataJsonLightPropertyAndValueSerializer_NoExpectedTypeOrTypeNameSpecifiedForComplexValueRequest");
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x060009EB RID: 2539 RVA: 0x00019EC3 File Offset: 0x000180C3
		internal static string ODataJsonLightPropertyAndValueSerializer_NoExpectedTypeOrTypeNameSpecifiedForCollectionValueInRequest
		{
			get
			{
				return TextRes.GetString("ODataJsonLightPropertyAndValueSerializer_NoExpectedTypeOrTypeNameSpecifiedForCollectionValueInRequest");
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x060009EC RID: 2540 RVA: 0x00019ECF File Offset: 0x000180CF
		internal static string ODataResourceTypeContext_MetadataOrSerializationInfoMissing
		{
			get
			{
				return TextRes.GetString("ODataResourceTypeContext_MetadataOrSerializationInfoMissing");
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x060009ED RID: 2541 RVA: 0x00019EDB File Offset: 0x000180DB
		internal static string ODataResourceTypeContext_ODataResourceTypeNameMissing
		{
			get
			{
				return TextRes.GetString("ODataResourceTypeContext_ODataResourceTypeNameMissing");
			}
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x00019EE7 File Offset: 0x000180E7
		internal static string ODataContextUriBuilder_ValidateDerivedType(object p0, object p1)
		{
			return TextRes.GetString("ODataContextUriBuilder_ValidateDerivedType", new object[] { p0, p1 });
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x060009EF RID: 2543 RVA: 0x00019F01 File Offset: 0x00018101
		internal static string ODataContextUriBuilder_TypeNameMissingForTopLevelCollection
		{
			get
			{
				return TextRes.GetString("ODataContextUriBuilder_TypeNameMissingForTopLevelCollection");
			}
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x00019F0D File Offset: 0x0001810D
		internal static string ODataContextUriBuilder_UnsupportedPayloadKind(object p0)
		{
			return TextRes.GetString("ODataContextUriBuilder_UnsupportedPayloadKind", new object[] { p0 });
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x060009F1 RID: 2545 RVA: 0x00019F23 File Offset: 0x00018123
		internal static string ODataContextUriBuilder_StreamValueMustBePropertiesOfODataResource
		{
			get
			{
				return TextRes.GetString("ODataContextUriBuilder_StreamValueMustBePropertiesOfODataResource");
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x060009F2 RID: 2546 RVA: 0x00019F2F File Offset: 0x0001812F
		internal static string ODataContextUriBuilder_NavigationSourceOrTypeNameMissingForResourceOrResourceSet
		{
			get
			{
				return TextRes.GetString("ODataContextUriBuilder_NavigationSourceOrTypeNameMissingForResourceOrResourceSet");
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x060009F3 RID: 2547 RVA: 0x00019F3B File Offset: 0x0001813B
		internal static string ODataContextUriBuilder_ODataUriMissingForIndividualProperty
		{
			get
			{
				return TextRes.GetString("ODataContextUriBuilder_ODataUriMissingForIndividualProperty");
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x060009F4 RID: 2548 RVA: 0x00019F47 File Offset: 0x00018147
		internal static string ODataContextUriBuilder_TypeNameMissingForProperty
		{
			get
			{
				return TextRes.GetString("ODataContextUriBuilder_TypeNameMissingForProperty");
			}
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x00019F53 File Offset: 0x00018153
		internal static string ODataContextUriBuilder_ODataPathInvalidForContainedElement(object p0)
		{
			return TextRes.GetString("ODataContextUriBuilder_ODataPathInvalidForContainedElement", new object[] { p0 });
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x00019F69 File Offset: 0x00018169
		internal static string ODataJsonLightPropertyAndValueDeserializer_UnexpectedAnnotationProperties(object p0)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_UnexpectedAnnotationProperties", new object[] { p0 });
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x00019F7F File Offset: 0x0001817F
		internal static string ODataJsonLightPropertyAndValueDeserializer_UnexpectedPropertyAnnotation(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_UnexpectedPropertyAnnotation", new object[] { p0, p1 });
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x00019F99 File Offset: 0x00018199
		internal static string ODataJsonLightPropertyAndValueDeserializer_UnexpectedODataPropertyAnnotation(object p0)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_UnexpectedODataPropertyAnnotation", new object[] { p0 });
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x00019FAF File Offset: 0x000181AF
		internal static string ODataJsonLightPropertyAndValueDeserializer_UnexpectedProperty(object p0)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_UnexpectedProperty", new object[] { p0 });
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x060009FA RID: 2554 RVA: 0x00019FC5 File Offset: 0x000181C5
		internal static string ODataJsonLightPropertyAndValueDeserializer_InvalidTopLevelPropertyPayload
		{
			get
			{
				return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_InvalidTopLevelPropertyPayload");
			}
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x00019FD1 File Offset: 0x000181D1
		internal static string ODataJsonLightPropertyAndValueDeserializer_InvalidTopLevelPropertyName(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_InvalidTopLevelPropertyName", new object[] { p0, p1 });
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x00019FEB File Offset: 0x000181EB
		internal static string ODataJsonLightPropertyAndValueDeserializer_InvalidTypeName(object p0)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_InvalidTypeName", new object[] { p0 });
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x0001A001 File Offset: 0x00018201
		internal static string ODataJsonLightPropertyAndValueDeserializer_TopLevelPropertyAnnotationWithoutProperty(object p0)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_TopLevelPropertyAnnotationWithoutProperty", new object[] { p0 });
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x0001A017 File Offset: 0x00018217
		internal static string ODataJsonLightPropertyAndValueDeserializer_ComplexValuePropertyAnnotationWithoutProperty(object p0)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_ComplexValuePropertyAnnotationWithoutProperty", new object[] { p0 });
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x0001A02D File Offset: 0x0001822D
		internal static string ODataJsonLightPropertyAndValueDeserializer_ComplexValueWithPropertyTypeAnnotation(object p0)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_ComplexValueWithPropertyTypeAnnotation", new object[] { p0 });
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000A00 RID: 2560 RVA: 0x0001A043 File Offset: 0x00018243
		internal static string ODataJsonLightPropertyAndValueDeserializer_ComplexTypeAnnotationNotFirst
		{
			get
			{
				return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_ComplexTypeAnnotationNotFirst");
			}
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x0001A04F File Offset: 0x0001824F
		internal static string ODataJsonLightPropertyAndValueDeserializer_UnexpectedDataPropertyAnnotation(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_UnexpectedDataPropertyAnnotation", new object[] { p0, p1 });
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x0001A069 File Offset: 0x00018269
		internal static string ODataJsonLightPropertyAndValueDeserializer_TypePropertyAfterValueProperty(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_TypePropertyAfterValueProperty", new object[] { p0, p1 });
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x0001A083 File Offset: 0x00018283
		internal static string ODataJsonLightPropertyAndValueDeserializer_ODataTypeAnnotationInPrimitiveValue(object p0)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_ODataTypeAnnotationInPrimitiveValue", new object[] { p0 });
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x0001A099 File Offset: 0x00018299
		internal static string ODataJsonLightPropertyAndValueDeserializer_TopLevelPropertyWithPrimitiveNullValue(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_TopLevelPropertyWithPrimitiveNullValue", new object[] { p0, p1 });
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x0001A0B3 File Offset: 0x000182B3
		internal static string ODataJsonLightPropertyAndValueDeserializer_UnexpectedMetadataReferenceProperty(object p0)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_UnexpectedMetadataReferenceProperty", new object[] { p0 });
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x0001A0C9 File Offset: 0x000182C9
		internal static string ODataJsonLightPropertyAndValueDeserializer_NoPropertyAndAnnotationAllowedInNullPayload(object p0)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_NoPropertyAndAnnotationAllowedInNullPayload", new object[] { p0 });
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x0001A0DF File Offset: 0x000182DF
		internal static string ODataJsonLightPropertyAndValueDeserializer_CollectionTypeNotExpected(object p0)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_CollectionTypeNotExpected", new object[] { p0 });
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x0001A0F5 File Offset: 0x000182F5
		internal static string ODataJsonLightPropertyAndValueDeserializer_CollectionTypeExpected(object p0)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_CollectionTypeExpected", new object[] { p0 });
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000A09 RID: 2569 RVA: 0x0001A10B File Offset: 0x0001830B
		internal static string ODataJsonReaderCoreUtils_CannotReadSpatialPropertyValue
		{
			get
			{
				return TextRes.GetString("ODataJsonReaderCoreUtils_CannotReadSpatialPropertyValue");
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000A0A RID: 2570 RVA: 0x0001A117 File Offset: 0x00018317
		internal static string ODataJsonLightReader_UnexpectedPrimitiveValueForODataResource
		{
			get
			{
				return TextRes.GetString("ODataJsonLightReader_UnexpectedPrimitiveValueForODataResource");
			}
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x0001A123 File Offset: 0x00018323
		internal static string ODataJsonLightReaderUtils_AnnotationWithNullValue(object p0)
		{
			return TextRes.GetString("ODataJsonLightReaderUtils_AnnotationWithNullValue", new object[] { p0 });
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x0001A139 File Offset: 0x00018339
		internal static string ODataJsonLightReaderUtils_InvalidValueForODataNullAnnotation(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightReaderUtils_InvalidValueForODataNullAnnotation", new object[] { p0, p1 });
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x0001A153 File Offset: 0x00018353
		internal static string JsonLightInstanceAnnotationWriter_DuplicateAnnotationNameInCollection(object p0)
		{
			return TextRes.GetString("JsonLightInstanceAnnotationWriter_DuplicateAnnotationNameInCollection", new object[] { p0 });
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000A0E RID: 2574 RVA: 0x0001A169 File Offset: 0x00018369
		internal static string ODataJsonLightContextUriParser_NullMetadataDocumentUri
		{
			get
			{
				return TextRes.GetString("ODataJsonLightContextUriParser_NullMetadataDocumentUri");
			}
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x0001A175 File Offset: 0x00018375
		internal static string ODataJsonLightContextUriParser_ContextUriDoesNotMatchExpectedPayloadKind(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightContextUriParser_ContextUriDoesNotMatchExpectedPayloadKind", new object[] { p0, p1 });
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x0001A18F File Offset: 0x0001838F
		internal static string ODataJsonLightContextUriParser_InvalidEntitySetNameOrTypeName(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightContextUriParser_InvalidEntitySetNameOrTypeName", new object[] { p0, p1 });
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x0001A1A9 File Offset: 0x000183A9
		internal static string ODataJsonLightContextUriParser_InvalidPayloadKindWithSelectQueryOption(object p0)
		{
			return TextRes.GetString("ODataJsonLightContextUriParser_InvalidPayloadKindWithSelectQueryOption", new object[] { p0 });
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000A12 RID: 2578 RVA: 0x0001A1BF File Offset: 0x000183BF
		internal static string ODataJsonLightContextUriParser_NoModel
		{
			get
			{
				return TextRes.GetString("ODataJsonLightContextUriParser_NoModel");
			}
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x0001A1CB File Offset: 0x000183CB
		internal static string ODataJsonLightContextUriParser_InvalidContextUrl(object p0)
		{
			return TextRes.GetString("ODataJsonLightContextUriParser_InvalidContextUrl", new object[] { p0 });
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x0001A1E1 File Offset: 0x000183E1
		internal static string ODataJsonLightContextUriParser_LastSegmentIsKeySegment(object p0)
		{
			return TextRes.GetString("ODataJsonLightContextUriParser_LastSegmentIsKeySegment", new object[] { p0 });
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x0001A1F7 File Offset: 0x000183F7
		internal static string ODataJsonLightContextUriParser_TopLevelContextUrlShouldBeAbsolute(object p0)
		{
			return TextRes.GetString("ODataJsonLightContextUriParser_TopLevelContextUrlShouldBeAbsolute", new object[] { p0 });
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000A16 RID: 2582 RVA: 0x0001A20D File Offset: 0x0001840D
		internal static string ODataJsonLightResourceDeserializer_ResourceTypeAnnotationNotFirst
		{
			get
			{
				return TextRes.GetString("ODataJsonLightResourceDeserializer_ResourceTypeAnnotationNotFirst");
			}
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x0001A219 File Offset: 0x00018419
		internal static string ODataJsonLightResourceDeserializer_ResourceInstanceAnnotationPrecededByProperty(object p0)
		{
			return TextRes.GetString("ODataJsonLightResourceDeserializer_ResourceInstanceAnnotationPrecededByProperty", new object[] { p0 });
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x0001A22F File Offset: 0x0001842F
		internal static string ODataJsonLightResourceDeserializer_CannotReadResourceSetContentStart(object p0)
		{
			return TextRes.GetString("ODataJsonLightResourceDeserializer_CannotReadResourceSetContentStart", new object[] { p0 });
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x0001A245 File Offset: 0x00018445
		internal static string ODataJsonLightResourceDeserializer_ExpectedResourceSetPropertyNotFound(object p0)
		{
			return TextRes.GetString("ODataJsonLightResourceDeserializer_ExpectedResourceSetPropertyNotFound", new object[] { p0 });
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x0001A25B File Offset: 0x0001845B
		internal static string ODataJsonLightResourceDeserializer_InvalidNodeTypeForItemsInResourceSet(object p0)
		{
			return TextRes.GetString("ODataJsonLightResourceDeserializer_InvalidNodeTypeForItemsInResourceSet", new object[] { p0 });
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x0001A271 File Offset: 0x00018471
		internal static string ODataJsonLightResourceDeserializer_InvalidPropertyAnnotationInTopLevelResourceSet(object p0)
		{
			return TextRes.GetString("ODataJsonLightResourceDeserializer_InvalidPropertyAnnotationInTopLevelResourceSet", new object[] { p0 });
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x0001A287 File Offset: 0x00018487
		internal static string ODataJsonLightResourceDeserializer_InvalidPropertyInTopLevelResourceSet(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightResourceDeserializer_InvalidPropertyInTopLevelResourceSet", new object[] { p0, p1 });
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x0001A2A1 File Offset: 0x000184A1
		internal static string ODataJsonLightResourceDeserializer_PropertyWithoutValueWithWrongType(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightResourceDeserializer_PropertyWithoutValueWithWrongType", new object[] { p0, p1 });
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x0001A2BB File Offset: 0x000184BB
		internal static string ODataJsonLightResourceDeserializer_OpenPropertyWithoutValue(object p0)
		{
			return TextRes.GetString("ODataJsonLightResourceDeserializer_OpenPropertyWithoutValue", new object[] { p0 });
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000A1F RID: 2591 RVA: 0x0001A2D1 File Offset: 0x000184D1
		internal static string ODataJsonLightResourceDeserializer_StreamPropertyInRequest
		{
			get
			{
				return TextRes.GetString("ODataJsonLightResourceDeserializer_StreamPropertyInRequest");
			}
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x0001A2DD File Offset: 0x000184DD
		internal static string ODataJsonLightResourceDeserializer_UnexpectedStreamPropertyAnnotation(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightResourceDeserializer_UnexpectedStreamPropertyAnnotation", new object[] { p0, p1 });
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x0001A2F7 File Offset: 0x000184F7
		internal static string ODataJsonLightResourceDeserializer_StreamPropertyWithValue(object p0)
		{
			return TextRes.GetString("ODataJsonLightResourceDeserializer_StreamPropertyWithValue", new object[] { p0 });
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x0001A30D File Offset: 0x0001850D
		internal static string ODataJsonLightResourceDeserializer_UnexpectedDeferredLinkPropertyAnnotation(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightResourceDeserializer_UnexpectedDeferredLinkPropertyAnnotation", new object[] { p0, p1 });
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x0001A327 File Offset: 0x00018527
		internal static string ODataJsonLightResourceDeserializer_CannotReadSingletonNestedResource(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightResourceDeserializer_CannotReadSingletonNestedResource", new object[] { p0, p1 });
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x0001A341 File Offset: 0x00018541
		internal static string ODataJsonLightResourceDeserializer_CannotReadCollectionNestedResource(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightResourceDeserializer_CannotReadCollectionNestedResource", new object[] { p0, p1 });
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x0001A35B File Offset: 0x0001855B
		internal static string ODataJsonLightResourceDeserializer_CannotReadNestedResource(object p0)
		{
			return TextRes.GetString("ODataJsonLightResourceDeserializer_CannotReadNestedResource", new object[] { p0 });
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x0001A371 File Offset: 0x00018571
		internal static string ODataJsonLightResourceDeserializer_UnexpectedExpandedSingletonNavigationLinkPropertyAnnotation(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightResourceDeserializer_UnexpectedExpandedSingletonNavigationLinkPropertyAnnotation", new object[] { p0, p1 });
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x0001A38B File Offset: 0x0001858B
		internal static string ODataJsonLightResourceDeserializer_UnexpectedExpandedCollectionNavigationLinkPropertyAnnotation(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightResourceDeserializer_UnexpectedExpandedCollectionNavigationLinkPropertyAnnotation", new object[] { p0, p1 });
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x0001A3A5 File Offset: 0x000185A5
		internal static string ODataJsonLightResourceDeserializer_UnexpectedComplexCollectionPropertyAnnotation(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightResourceDeserializer_UnexpectedComplexCollectionPropertyAnnotation", new object[] { p0, p1 });
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x0001A3BF File Offset: 0x000185BF
		internal static string ODataJsonLightResourceDeserializer_DuplicateNestedResourceSetAnnotation(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightResourceDeserializer_DuplicateNestedResourceSetAnnotation", new object[] { p0, p1 });
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x0001A3D9 File Offset: 0x000185D9
		internal static string ODataJsonLightResourceDeserializer_UnexpectedPropertyAnnotationAfterExpandedResourceSet(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightResourceDeserializer_UnexpectedPropertyAnnotationAfterExpandedResourceSet", new object[] { p0, p1 });
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x0001A3F3 File Offset: 0x000185F3
		internal static string ODataJsonLightResourceDeserializer_UnexpectedNavigationLinkInRequestPropertyAnnotation(object p0, object p1, object p2)
		{
			return TextRes.GetString("ODataJsonLightResourceDeserializer_UnexpectedNavigationLinkInRequestPropertyAnnotation", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x0001A411 File Offset: 0x00018611
		internal static string ODataJsonLightResourceDeserializer_ArrayValueForSingletonBindPropertyAnnotation(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightResourceDeserializer_ArrayValueForSingletonBindPropertyAnnotation", new object[] { p0, p1 });
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x0001A42B File Offset: 0x0001862B
		internal static string ODataJsonLightResourceDeserializer_StringValueForCollectionBindPropertyAnnotation(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightResourceDeserializer_StringValueForCollectionBindPropertyAnnotation", new object[] { p0, p1 });
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x0001A445 File Offset: 0x00018645
		internal static string ODataJsonLightResourceDeserializer_EmptyBindArray(object p0)
		{
			return TextRes.GetString("ODataJsonLightResourceDeserializer_EmptyBindArray", new object[] { p0 });
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x0001A45B File Offset: 0x0001865B
		internal static string ODataJsonLightResourceDeserializer_NavigationPropertyWithoutValueAndEntityReferenceLink(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightResourceDeserializer_NavigationPropertyWithoutValueAndEntityReferenceLink", new object[] { p0, p1 });
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x0001A475 File Offset: 0x00018675
		internal static string ODataJsonLightResourceDeserializer_SingletonNavigationPropertyWithBindingAndValue(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightResourceDeserializer_SingletonNavigationPropertyWithBindingAndValue", new object[] { p0, p1 });
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x0001A48F File Offset: 0x0001868F
		internal static string ODataJsonLightResourceDeserializer_PropertyWithoutValueWithUnknownType(object p0)
		{
			return TextRes.GetString("ODataJsonLightResourceDeserializer_PropertyWithoutValueWithUnknownType", new object[] { p0 });
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x0001A4A5 File Offset: 0x000186A5
		internal static string ODataJsonLightResourceDeserializer_OperationIsNotActionOrFunction(object p0)
		{
			return TextRes.GetString("ODataJsonLightResourceDeserializer_OperationIsNotActionOrFunction", new object[] { p0 });
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x0001A4BB File Offset: 0x000186BB
		internal static string ODataJsonLightResourceDeserializer_MultipleOptionalPropertiesInOperation(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightResourceDeserializer_MultipleOptionalPropertiesInOperation", new object[] { p0, p1 });
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x0001A4D5 File Offset: 0x000186D5
		internal static string ODataJsonLightResourceDeserializer_OperationMissingTargetProperty(object p0)
		{
			return TextRes.GetString("ODataJsonLightResourceDeserializer_OperationMissingTargetProperty", new object[] { p0 });
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000A35 RID: 2613 RVA: 0x0001A4EB File Offset: 0x000186EB
		internal static string ODataJsonLightResourceDeserializer_MetadataReferencePropertyInRequest
		{
			get
			{
				return TextRes.GetString("ODataJsonLightResourceDeserializer_MetadataReferencePropertyInRequest");
			}
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x0001A4F7 File Offset: 0x000186F7
		internal static string ODataJsonLightValidationUtils_OperationPropertyCannotBeNull(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightValidationUtils_OperationPropertyCannotBeNull", new object[] { p0, p1 });
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x0001A511 File Offset: 0x00018711
		internal static string ODataJsonLightValidationUtils_OpenMetadataReferencePropertyNotSupported(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightValidationUtils_OpenMetadataReferencePropertyNotSupported", new object[] { p0, p1 });
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x0001A52B File Offset: 0x0001872B
		internal static string ODataJsonLightDeserializer_RelativeUriUsedWithouODataMetadataAnnotation(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightDeserializer_RelativeUriUsedWithouODataMetadataAnnotation", new object[] { p0, p1 });
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x0001A545 File Offset: 0x00018745
		internal static string ODataJsonLightResourceMetadataContext_MetadataAnnotationMustBeInPayload(object p0)
		{
			return TextRes.GetString("ODataJsonLightResourceMetadataContext_MetadataAnnotationMustBeInPayload", new object[] { p0 });
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x0001A55B File Offset: 0x0001875B
		internal static string ODataJsonLightCollectionDeserializer_ExpectedCollectionPropertyNotFound(object p0)
		{
			return TextRes.GetString("ODataJsonLightCollectionDeserializer_ExpectedCollectionPropertyNotFound", new object[] { p0 });
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x0001A571 File Offset: 0x00018771
		internal static string ODataJsonLightCollectionDeserializer_CannotReadCollectionContentStart(object p0)
		{
			return TextRes.GetString("ODataJsonLightCollectionDeserializer_CannotReadCollectionContentStart", new object[] { p0 });
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x0001A587 File Offset: 0x00018787
		internal static string ODataJsonLightCollectionDeserializer_CannotReadCollectionEnd(object p0)
		{
			return TextRes.GetString("ODataJsonLightCollectionDeserializer_CannotReadCollectionEnd", new object[] { p0 });
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x0001A59D File Offset: 0x0001879D
		internal static string ODataJsonLightCollectionDeserializer_InvalidCollectionTypeName(object p0)
		{
			return TextRes.GetString("ODataJsonLightCollectionDeserializer_InvalidCollectionTypeName", new object[] { p0 });
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x0001A5B3 File Offset: 0x000187B3
		internal static string ODataJsonLightEntityReferenceLinkDeserializer_EntityReferenceLinkMustBeObjectValue(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntityReferenceLinkDeserializer_EntityReferenceLinkMustBeObjectValue", new object[] { p0 });
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x0001A5C9 File Offset: 0x000187C9
		internal static string ODataJsonLightEntityReferenceLinkDeserializer_PropertyAnnotationForEntityReferenceLink(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntityReferenceLinkDeserializer_PropertyAnnotationForEntityReferenceLink", new object[] { p0 });
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x0001A5DF File Offset: 0x000187DF
		internal static string ODataJsonLightEntityReferenceLinkDeserializer_InvalidAnnotationInEntityReferenceLink(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntityReferenceLinkDeserializer_InvalidAnnotationInEntityReferenceLink", new object[] { p0 });
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x0001A5F5 File Offset: 0x000187F5
		internal static string ODataJsonLightEntityReferenceLinkDeserializer_InvalidPropertyInEntityReferenceLink(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightEntityReferenceLinkDeserializer_InvalidPropertyInEntityReferenceLink", new object[] { p0, p1 });
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x0001A60F File Offset: 0x0001880F
		internal static string ODataJsonLightEntityReferenceLinkDeserializer_MissingEntityReferenceLinkProperty(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntityReferenceLinkDeserializer_MissingEntityReferenceLinkProperty", new object[] { p0 });
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x0001A625 File Offset: 0x00018825
		internal static string ODataJsonLightEntityReferenceLinkDeserializer_MultipleUriPropertiesInEntityReferenceLink(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntityReferenceLinkDeserializer_MultipleUriPropertiesInEntityReferenceLink", new object[] { p0 });
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x0001A63B File Offset: 0x0001883B
		internal static string ODataJsonLightEntityReferenceLinkDeserializer_EntityReferenceLinkUrlCannotBeNull(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntityReferenceLinkDeserializer_EntityReferenceLinkUrlCannotBeNull", new object[] { p0 });
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000A45 RID: 2629 RVA: 0x0001A651 File Offset: 0x00018851
		internal static string ODataJsonLightEntityReferenceLinkDeserializer_PropertyAnnotationForEntityReferenceLinks
		{
			get
			{
				return TextRes.GetString("ODataJsonLightEntityReferenceLinkDeserializer_PropertyAnnotationForEntityReferenceLinks");
			}
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x0001A65D File Offset: 0x0001885D
		internal static string ODataJsonLightEntityReferenceLinkDeserializer_InvalidEntityReferenceLinksPropertyFound(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightEntityReferenceLinkDeserializer_InvalidEntityReferenceLinksPropertyFound", new object[] { p0, p1 });
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x0001A677 File Offset: 0x00018877
		internal static string ODataJsonLightEntityReferenceLinkDeserializer_InvalidPropertyAnnotationInEntityReferenceLinks(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntityReferenceLinkDeserializer_InvalidPropertyAnnotationInEntityReferenceLinks", new object[] { p0 });
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x0001A68D File Offset: 0x0001888D
		internal static string ODataJsonLightEntityReferenceLinkDeserializer_ExpectedEntityReferenceLinksPropertyNotFound(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntityReferenceLinkDeserializer_ExpectedEntityReferenceLinksPropertyNotFound", new object[] { p0 });
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x0001A6A3 File Offset: 0x000188A3
		internal static string ODataJsonOperationsDeserializerUtils_OperationPropertyCannotBeNull(object p0, object p1, object p2)
		{
			return TextRes.GetString("ODataJsonOperationsDeserializerUtils_OperationPropertyCannotBeNull", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x0001A6C1 File Offset: 0x000188C1
		internal static string ODataJsonOperationsDeserializerUtils_OperationsPropertyMustHaveObjectValue(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonOperationsDeserializerUtils_OperationsPropertyMustHaveObjectValue", new object[] { p0, p1 });
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x0001A6DB File Offset: 0x000188DB
		internal static string ODataJsonLightServiceDocumentDeserializer_DuplicatePropertiesInServiceDocument(object p0)
		{
			return TextRes.GetString("ODataJsonLightServiceDocumentDeserializer_DuplicatePropertiesInServiceDocument", new object[] { p0 });
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x0001A6F1 File Offset: 0x000188F1
		internal static string ODataJsonLightServiceDocumentDeserializer_DuplicatePropertiesInServiceDocumentElement(object p0)
		{
			return TextRes.GetString("ODataJsonLightServiceDocumentDeserializer_DuplicatePropertiesInServiceDocumentElement", new object[] { p0 });
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x0001A707 File Offset: 0x00018907
		internal static string ODataJsonLightServiceDocumentDeserializer_MissingValuePropertyInServiceDocument(object p0)
		{
			return TextRes.GetString("ODataJsonLightServiceDocumentDeserializer_MissingValuePropertyInServiceDocument", new object[] { p0 });
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x0001A71D File Offset: 0x0001891D
		internal static string ODataJsonLightServiceDocumentDeserializer_MissingRequiredPropertyInServiceDocumentElement(object p0)
		{
			return TextRes.GetString("ODataJsonLightServiceDocumentDeserializer_MissingRequiredPropertyInServiceDocumentElement", new object[] { p0 });
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x0001A733 File Offset: 0x00018933
		internal static string ODataJsonLightServiceDocumentDeserializer_PropertyAnnotationInServiceDocument(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightServiceDocumentDeserializer_PropertyAnnotationInServiceDocument", new object[] { p0, p1 });
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x0001A74D File Offset: 0x0001894D
		internal static string ODataJsonLightServiceDocumentDeserializer_InstanceAnnotationInServiceDocument(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightServiceDocumentDeserializer_InstanceAnnotationInServiceDocument", new object[] { p0, p1 });
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x0001A767 File Offset: 0x00018967
		internal static string ODataJsonLightServiceDocumentDeserializer_PropertyAnnotationInServiceDocumentElement(object p0)
		{
			return TextRes.GetString("ODataJsonLightServiceDocumentDeserializer_PropertyAnnotationInServiceDocumentElement", new object[] { p0 });
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x0001A77D File Offset: 0x0001897D
		internal static string ODataJsonLightServiceDocumentDeserializer_InstanceAnnotationInServiceDocumentElement(object p0)
		{
			return TextRes.GetString("ODataJsonLightServiceDocumentDeserializer_InstanceAnnotationInServiceDocumentElement", new object[] { p0 });
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x0001A793 File Offset: 0x00018993
		internal static string ODataJsonLightServiceDocumentDeserializer_UnexpectedPropertyInServiceDocumentElement(object p0, object p1, object p2)
		{
			return TextRes.GetString("ODataJsonLightServiceDocumentDeserializer_UnexpectedPropertyInServiceDocumentElement", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x0001A7B1 File Offset: 0x000189B1
		internal static string ODataJsonLightServiceDocumentDeserializer_UnexpectedPropertyInServiceDocument(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightServiceDocumentDeserializer_UnexpectedPropertyInServiceDocument", new object[] { p0, p1 });
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x0001A7CB File Offset: 0x000189CB
		internal static string ODataJsonLightServiceDocumentDeserializer_PropertyAnnotationWithoutProperty(object p0)
		{
			return TextRes.GetString("ODataJsonLightServiceDocumentDeserializer_PropertyAnnotationWithoutProperty", new object[] { p0 });
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000A56 RID: 2646 RVA: 0x0001A7E1 File Offset: 0x000189E1
		internal static string ODataJsonLightParameterDeserializer_PropertyAnnotationForParameters
		{
			get
			{
				return TextRes.GetString("ODataJsonLightParameterDeserializer_PropertyAnnotationForParameters");
			}
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x0001A7ED File Offset: 0x000189ED
		internal static string ODataJsonLightParameterDeserializer_PropertyAnnotationWithoutPropertyForParameters(object p0)
		{
			return TextRes.GetString("ODataJsonLightParameterDeserializer_PropertyAnnotationWithoutPropertyForParameters", new object[] { p0 });
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x0001A803 File Offset: 0x00018A03
		internal static string ODataJsonLightParameterDeserializer_UnsupportedPrimitiveParameterType(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightParameterDeserializer_UnsupportedPrimitiveParameterType", new object[] { p0, p1 });
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x0001A81D File Offset: 0x00018A1D
		internal static string ODataJsonLightParameterDeserializer_NullCollectionExpected(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightParameterDeserializer_NullCollectionExpected", new object[] { p0, p1 });
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x0001A837 File Offset: 0x00018A37
		internal static string ODataJsonLightParameterDeserializer_UnsupportedParameterTypeKind(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightParameterDeserializer_UnsupportedParameterTypeKind", new object[] { p0, p1 });
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000A5B RID: 2651 RVA: 0x0001A851 File Offset: 0x00018A51
		internal static string SelectedPropertiesNode_StarSegmentNotLastSegment
		{
			get
			{
				return TextRes.GetString("SelectedPropertiesNode_StarSegmentNotLastSegment");
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000A5C RID: 2652 RVA: 0x0001A85D File Offset: 0x00018A5D
		internal static string SelectedPropertiesNode_StarSegmentAfterTypeSegment
		{
			get
			{
				return TextRes.GetString("SelectedPropertiesNode_StarSegmentAfterTypeSegment");
			}
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x0001A869 File Offset: 0x00018A69
		internal static string ODataJsonLightErrorDeserializer_PropertyAnnotationNotAllowedInErrorPayload(object p0)
		{
			return TextRes.GetString("ODataJsonLightErrorDeserializer_PropertyAnnotationNotAllowedInErrorPayload", new object[] { p0 });
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x0001A87F File Offset: 0x00018A7F
		internal static string ODataJsonLightErrorDeserializer_InstanceAnnotationNotAllowedInErrorPayload(object p0)
		{
			return TextRes.GetString("ODataJsonLightErrorDeserializer_InstanceAnnotationNotAllowedInErrorPayload", new object[] { p0 });
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x0001A895 File Offset: 0x00018A95
		internal static string ODataJsonLightErrorDeserializer_PropertyAnnotationWithoutPropertyForError(object p0)
		{
			return TextRes.GetString("ODataJsonLightErrorDeserializer_PropertyAnnotationWithoutPropertyForError", new object[] { p0 });
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x0001A8AB File Offset: 0x00018AAB
		internal static string ODataJsonLightErrorDeserializer_TopLevelErrorValueWithInvalidProperty(object p0)
		{
			return TextRes.GetString("ODataJsonLightErrorDeserializer_TopLevelErrorValueWithInvalidProperty", new object[] { p0 });
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x0001A8C1 File Offset: 0x00018AC1
		internal static string ODataConventionalUriBuilder_EntityTypeWithNoKeyProperties(object p0)
		{
			return TextRes.GetString("ODataConventionalUriBuilder_EntityTypeWithNoKeyProperties", new object[] { p0 });
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x0001A8D7 File Offset: 0x00018AD7
		internal static string ODataConventionalUriBuilder_NullKeyValue(object p0, object p1)
		{
			return TextRes.GetString("ODataConventionalUriBuilder_NullKeyValue", new object[] { p0, p1 });
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x0001A8F1 File Offset: 0x00018AF1
		internal static string ODataResourceMetadataContext_EntityTypeWithNoKeyProperties(object p0)
		{
			return TextRes.GetString("ODataResourceMetadataContext_EntityTypeWithNoKeyProperties", new object[] { p0 });
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x0001A907 File Offset: 0x00018B07
		internal static string ODataResourceMetadataContext_NullKeyValue(object p0, object p1)
		{
			return TextRes.GetString("ODataResourceMetadataContext_NullKeyValue", new object[] { p0, p1 });
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x0001A921 File Offset: 0x00018B21
		internal static string ODataResourceMetadataContext_KeyOrETagValuesMustBePrimitiveValues(object p0, object p1)
		{
			return TextRes.GetString("ODataResourceMetadataContext_KeyOrETagValuesMustBePrimitiveValues", new object[] { p0, p1 });
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x0001A93B File Offset: 0x00018B3B
		internal static string EdmValueUtils_NonPrimitiveValue(object p0, object p1)
		{
			return TextRes.GetString("EdmValueUtils_NonPrimitiveValue", new object[] { p0, p1 });
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x0001A955 File Offset: 0x00018B55
		internal static string EdmValueUtils_PropertyDoesntExist(object p0, object p1)
		{
			return TextRes.GetString("EdmValueUtils_PropertyDoesntExist", new object[] { p0, p1 });
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000A68 RID: 2664 RVA: 0x0001A96F File Offset: 0x00018B6F
		internal static string ODataPrimitiveValue_CannotCreateODataPrimitiveValueFromNull
		{
			get
			{
				return TextRes.GetString("ODataPrimitiveValue_CannotCreateODataPrimitiveValueFromNull");
			}
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x0001A97B File Offset: 0x00018B7B
		internal static string ODataPrimitiveValue_CannotCreateODataPrimitiveValueFromUnsupportedValueType(object p0)
		{
			return TextRes.GetString("ODataPrimitiveValue_CannotCreateODataPrimitiveValueFromUnsupportedValueType", new object[] { p0 });
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x0001A991 File Offset: 0x00018B91
		internal static string ODataInstanceAnnotation_NeedPeriodInName(object p0)
		{
			return TextRes.GetString("ODataInstanceAnnotation_NeedPeriodInName", new object[] { p0 });
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x0001A9A7 File Offset: 0x00018BA7
		internal static string ODataInstanceAnnotation_ReservedNamesNotAllowed(object p0, object p1)
		{
			return TextRes.GetString("ODataInstanceAnnotation_ReservedNamesNotAllowed", new object[] { p0, p1 });
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x0001A9C1 File Offset: 0x00018BC1
		internal static string ODataInstanceAnnotation_BadTermName(object p0)
		{
			return TextRes.GetString("ODataInstanceAnnotation_BadTermName", new object[] { p0 });
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000A6D RID: 2669 RVA: 0x0001A9D7 File Offset: 0x00018BD7
		internal static string ODataInstanceAnnotation_ValueCannotBeODataStreamReferenceValue
		{
			get
			{
				return TextRes.GetString("ODataInstanceAnnotation_ValueCannotBeODataStreamReferenceValue");
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000A6E RID: 2670 RVA: 0x0001A9E3 File Offset: 0x00018BE3
		internal static string ODataJsonLightValueSerializer_MissingTypeNameOnCollection
		{
			get
			{
				return TextRes.GetString("ODataJsonLightValueSerializer_MissingTypeNameOnCollection");
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000A6F RID: 2671 RVA: 0x0001A9EF File Offset: 0x00018BEF
		internal static string ODataJsonLightValueSerializer_MissingRawValueOnUntyped
		{
			get
			{
				return TextRes.GetString("ODataJsonLightValueSerializer_MissingRawValueOnUntyped");
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000A70 RID: 2672 RVA: 0x0001A9FB File Offset: 0x00018BFB
		internal static string AtomInstanceAnnotation_MissingTermAttributeOnAnnotationElement
		{
			get
			{
				return TextRes.GetString("AtomInstanceAnnotation_MissingTermAttributeOnAnnotationElement");
			}
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x0001AA07 File Offset: 0x00018C07
		internal static string AtomInstanceAnnotation_AttributeValueNotationUsedWithIncompatibleType(object p0, object p1)
		{
			return TextRes.GetString("AtomInstanceAnnotation_AttributeValueNotationUsedWithIncompatibleType", new object[] { p0, p1 });
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x0001AA21 File Offset: 0x00018C21
		internal static string AtomInstanceAnnotation_AttributeValueNotationUsedOnNonEmptyElement(object p0)
		{
			return TextRes.GetString("AtomInstanceAnnotation_AttributeValueNotationUsedOnNonEmptyElement", new object[] { p0 });
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000A73 RID: 2675 RVA: 0x0001AA37 File Offset: 0x00018C37
		internal static string AtomInstanceAnnotation_MultipleAttributeValueNotationAttributes
		{
			get
			{
				return TextRes.GetString("AtomInstanceAnnotation_MultipleAttributeValueNotationAttributes");
			}
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x0001AA43 File Offset: 0x00018C43
		internal static string AnnotationFilterPattern_InvalidPatternMissingDot(object p0)
		{
			return TextRes.GetString("AnnotationFilterPattern_InvalidPatternMissingDot", new object[] { p0 });
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x0001AA59 File Offset: 0x00018C59
		internal static string AnnotationFilterPattern_InvalidPatternEmptySegment(object p0)
		{
			return TextRes.GetString("AnnotationFilterPattern_InvalidPatternEmptySegment", new object[] { p0 });
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x0001AA6F File Offset: 0x00018C6F
		internal static string AnnotationFilterPattern_InvalidPatternWildCardInSegment(object p0)
		{
			return TextRes.GetString("AnnotationFilterPattern_InvalidPatternWildCardInSegment", new object[] { p0 });
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x0001AA85 File Offset: 0x00018C85
		internal static string AnnotationFilterPattern_InvalidPatternWildCardMustBeInLastSegment(object p0)
		{
			return TextRes.GetString("AnnotationFilterPattern_InvalidPatternWildCardMustBeInLastSegment", new object[] { p0 });
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x0001AA9B File Offset: 0x00018C9B
		internal static string SyntacticTree_UriMustBeAbsolute(object p0)
		{
			return TextRes.GetString("SyntacticTree_UriMustBeAbsolute", new object[] { p0 });
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000A79 RID: 2681 RVA: 0x0001AAB1 File Offset: 0x00018CB1
		internal static string SyntacticTree_MaxDepthInvalid
		{
			get
			{
				return TextRes.GetString("SyntacticTree_MaxDepthInvalid");
			}
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x0001AABD File Offset: 0x00018CBD
		internal static string SyntacticTree_InvalidSkipQueryOptionValue(object p0)
		{
			return TextRes.GetString("SyntacticTree_InvalidSkipQueryOptionValue", new object[] { p0 });
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x0001AAD3 File Offset: 0x00018CD3
		internal static string SyntacticTree_InvalidTopQueryOptionValue(object p0)
		{
			return TextRes.GetString("SyntacticTree_InvalidTopQueryOptionValue", new object[] { p0 });
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x0001AAE9 File Offset: 0x00018CE9
		internal static string SyntacticTree_InvalidCountQueryOptionValue(object p0, object p1)
		{
			return TextRes.GetString("SyntacticTree_InvalidCountQueryOptionValue", new object[] { p0, p1 });
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x0001AB03 File Offset: 0x00018D03
		internal static string QueryOptionUtils_QueryParameterMustBeSpecifiedOnce(object p0)
		{
			return TextRes.GetString("QueryOptionUtils_QueryParameterMustBeSpecifiedOnce", new object[] { p0 });
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x0001AB19 File Offset: 0x00018D19
		internal static string UriBuilder_NotSupportedClrLiteral(object p0)
		{
			return TextRes.GetString("UriBuilder_NotSupportedClrLiteral", new object[] { p0 });
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x0001AB2F File Offset: 0x00018D2F
		internal static string UriBuilder_NotSupportedQueryToken(object p0)
		{
			return TextRes.GetString("UriBuilder_NotSupportedQueryToken", new object[] { p0 });
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000A80 RID: 2688 RVA: 0x0001AB45 File Offset: 0x00018D45
		internal static string UriQueryExpressionParser_TooDeep
		{
			get
			{
				return TextRes.GetString("UriQueryExpressionParser_TooDeep");
			}
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x0001AB51 File Offset: 0x00018D51
		internal static string UriQueryExpressionParser_ExpressionExpected(object p0, object p1)
		{
			return TextRes.GetString("UriQueryExpressionParser_ExpressionExpected", new object[] { p0, p1 });
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x0001AB6B File Offset: 0x00018D6B
		internal static string UriQueryExpressionParser_OpenParenExpected(object p0, object p1)
		{
			return TextRes.GetString("UriQueryExpressionParser_OpenParenExpected", new object[] { p0, p1 });
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x0001AB85 File Offset: 0x00018D85
		internal static string UriQueryExpressionParser_CloseParenOrCommaExpected(object p0, object p1)
		{
			return TextRes.GetString("UriQueryExpressionParser_CloseParenOrCommaExpected", new object[] { p0, p1 });
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x0001AB9F File Offset: 0x00018D9F
		internal static string UriQueryExpressionParser_CloseParenOrOperatorExpected(object p0, object p1)
		{
			return TextRes.GetString("UriQueryExpressionParser_CloseParenOrOperatorExpected", new object[] { p0, p1 });
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x0001ABB9 File Offset: 0x00018DB9
		internal static string UriQueryExpressionParser_CannotCreateStarTokenFromNonStar(object p0)
		{
			return TextRes.GetString("UriQueryExpressionParser_CannotCreateStarTokenFromNonStar", new object[] { p0 });
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x0001ABCF File Offset: 0x00018DCF
		internal static string UriQueryExpressionParser_RangeVariableAlreadyDeclared(object p0)
		{
			return TextRes.GetString("UriQueryExpressionParser_RangeVariableAlreadyDeclared", new object[] { p0 });
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x0001ABE5 File Offset: 0x00018DE5
		internal static string UriQueryExpressionParser_AsExpected(object p0, object p1)
		{
			return TextRes.GetString("UriQueryExpressionParser_AsExpected", new object[] { p0, p1 });
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x0001ABFF File Offset: 0x00018DFF
		internal static string UriQueryExpressionParser_WithExpected(object p0, object p1)
		{
			return TextRes.GetString("UriQueryExpressionParser_WithExpected", new object[] { p0, p1 });
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x0001AC19 File Offset: 0x00018E19
		internal static string UriQueryExpressionParser_UnrecognizedWithMethod(object p0, object p1, object p2)
		{
			return TextRes.GetString("UriQueryExpressionParser_UnrecognizedWithMethod", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x0001AC37 File Offset: 0x00018E37
		internal static string UriQueryExpressionParser_PropertyPathExpected(object p0, object p1)
		{
			return TextRes.GetString("UriQueryExpressionParser_PropertyPathExpected", new object[] { p0, p1 });
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x0001AC51 File Offset: 0x00018E51
		internal static string UriQueryExpressionParser_KeywordOrIdentifierExpected(object p0, object p1, object p2)
		{
			return TextRes.GetString("UriQueryExpressionParser_KeywordOrIdentifierExpected", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x0001AC6F File Offset: 0x00018E6F
		internal static string UriQueryPathParser_RequestUriDoesNotHaveTheCorrectBaseUri(object p0, object p1)
		{
			return TextRes.GetString("UriQueryPathParser_RequestUriDoesNotHaveTheCorrectBaseUri", new object[] { p0, p1 });
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000A8D RID: 2701 RVA: 0x0001AC89 File Offset: 0x00018E89
		internal static string UriQueryPathParser_SyntaxError
		{
			get
			{
				return TextRes.GetString("UriQueryPathParser_SyntaxError");
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000A8E RID: 2702 RVA: 0x0001AC95 File Offset: 0x00018E95
		internal static string UriQueryPathParser_TooManySegments
		{
			get
			{
				return TextRes.GetString("UriQueryPathParser_TooManySegments");
			}
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x0001ACA1 File Offset: 0x00018EA1
		internal static string UriUtils_DateTimeOffsetInvalidFormat(object p0)
		{
			return TextRes.GetString("UriUtils_DateTimeOffsetInvalidFormat", new object[] { p0 });
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000A90 RID: 2704 RVA: 0x0001ACB7 File Offset: 0x00018EB7
		internal static string SelectionItemBinder_NonNavigationPathToken
		{
			get
			{
				return TextRes.GetString("SelectionItemBinder_NonNavigationPathToken");
			}
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x0001ACC3 File Offset: 0x00018EC3
		internal static string MetadataBinder_UnsupportedQueryTokenKind(object p0)
		{
			return TextRes.GetString("MetadataBinder_UnsupportedQueryTokenKind", new object[] { p0 });
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x0001ACD9 File Offset: 0x00018ED9
		internal static string MetadataBinder_PropertyNotDeclared(object p0, object p1)
		{
			return TextRes.GetString("MetadataBinder_PropertyNotDeclared", new object[] { p0, p1 });
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x0001ACF3 File Offset: 0x00018EF3
		internal static string MetadataBinder_PropertyNotDeclaredOrNotKeyInKeyValue(object p0, object p1)
		{
			return TextRes.GetString("MetadataBinder_PropertyNotDeclaredOrNotKeyInKeyValue", new object[] { p0, p1 });
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x0001AD0D File Offset: 0x00018F0D
		internal static string MetadataBinder_QualifiedFunctionNameWithParametersNotDeclared(object p0, object p1)
		{
			return TextRes.GetString("MetadataBinder_QualifiedFunctionNameWithParametersNotDeclared", new object[] { p0, p1 });
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x0001AD27 File Offset: 0x00018F27
		internal static string MetadataBinder_UnnamedKeyValueOnTypeWithMultipleKeyProperties(object p0)
		{
			return TextRes.GetString("MetadataBinder_UnnamedKeyValueOnTypeWithMultipleKeyProperties", new object[] { p0 });
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x0001AD3D File Offset: 0x00018F3D
		internal static string MetadataBinder_DuplicitKeyPropertyInKeyValues(object p0)
		{
			return TextRes.GetString("MetadataBinder_DuplicitKeyPropertyInKeyValues", new object[] { p0 });
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x0001AD53 File Offset: 0x00018F53
		internal static string MetadataBinder_NotAllKeyPropertiesSpecifiedInKeyValues(object p0)
		{
			return TextRes.GetString("MetadataBinder_NotAllKeyPropertiesSpecifiedInKeyValues", new object[] { p0 });
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x0001AD69 File Offset: 0x00018F69
		internal static string MetadataBinder_CannotConvertToType(object p0, object p1)
		{
			return TextRes.GetString("MetadataBinder_CannotConvertToType", new object[] { p0, p1 });
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000A99 RID: 2713 RVA: 0x0001AD83 File Offset: 0x00018F83
		internal static string MetadataBinder_FilterExpressionNotSingleValue
		{
			get
			{
				return TextRes.GetString("MetadataBinder_FilterExpressionNotSingleValue");
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000A9A RID: 2714 RVA: 0x0001AD8F File Offset: 0x00018F8F
		internal static string MetadataBinder_OrderByExpressionNotSingleValue
		{
			get
			{
				return TextRes.GetString("MetadataBinder_OrderByExpressionNotSingleValue");
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000A9B RID: 2715 RVA: 0x0001AD9B File Offset: 0x00018F9B
		internal static string MetadataBinder_PropertyAccessWithoutParentParameter
		{
			get
			{
				return TextRes.GetString("MetadataBinder_PropertyAccessWithoutParentParameter");
			}
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x0001ADA7 File Offset: 0x00018FA7
		internal static string MetadataBinder_BinaryOperatorOperandNotSingleValue(object p0)
		{
			return TextRes.GetString("MetadataBinder_BinaryOperatorOperandNotSingleValue", new object[] { p0 });
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x0001ADBD File Offset: 0x00018FBD
		internal static string MetadataBinder_UnaryOperatorOperandNotSingleValue(object p0)
		{
			return TextRes.GetString("MetadataBinder_UnaryOperatorOperandNotSingleValue", new object[] { p0 });
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x0001ADD3 File Offset: 0x00018FD3
		internal static string MetadataBinder_PropertyAccessSourceNotSingleValue(object p0)
		{
			return TextRes.GetString("MetadataBinder_PropertyAccessSourceNotSingleValue", new object[] { p0 });
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x0001ADE9 File Offset: 0x00018FE9
		internal static string MetadataBinder_IncompatibleOperandsError(object p0, object p1, object p2)
		{
			return TextRes.GetString("MetadataBinder_IncompatibleOperandsError", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x0001AE07 File Offset: 0x00019007
		internal static string MetadataBinder_IncompatibleOperandError(object p0, object p1)
		{
			return TextRes.GetString("MetadataBinder_IncompatibleOperandError", new object[] { p0, p1 });
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x0001AE21 File Offset: 0x00019021
		internal static string MetadataBinder_UnknownFunction(object p0)
		{
			return TextRes.GetString("MetadataBinder_UnknownFunction", new object[] { p0 });
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x0001AE37 File Offset: 0x00019037
		internal static string MetadataBinder_FunctionArgumentNotSingleValue(object p0)
		{
			return TextRes.GetString("MetadataBinder_FunctionArgumentNotSingleValue", new object[] { p0 });
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x0001AE4D File Offset: 0x0001904D
		internal static string MetadataBinder_NoApplicableFunctionFound(object p0, object p1)
		{
			return TextRes.GetString("MetadataBinder_NoApplicableFunctionFound", new object[] { p0, p1 });
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x0001AE67 File Offset: 0x00019067
		internal static string MetadataBinder_BoundNodeCannotBeNull(object p0)
		{
			return TextRes.GetString("MetadataBinder_BoundNodeCannotBeNull", new object[] { p0 });
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x0001AE7D File Offset: 0x0001907D
		internal static string MetadataBinder_TopRequiresNonNegativeInteger(object p0)
		{
			return TextRes.GetString("MetadataBinder_TopRequiresNonNegativeInteger", new object[] { p0 });
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x0001AE93 File Offset: 0x00019093
		internal static string MetadataBinder_SkipRequiresNonNegativeInteger(object p0)
		{
			return TextRes.GetString("MetadataBinder_SkipRequiresNonNegativeInteger", new object[] { p0 });
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000AA7 RID: 2727 RVA: 0x0001AEA9 File Offset: 0x000190A9
		internal static string MetadataBinder_QueryOptionsBindStateCannotBeNull
		{
			get
			{
				return TextRes.GetString("MetadataBinder_QueryOptionsBindStateCannotBeNull");
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000AA8 RID: 2728 RVA: 0x0001AEB5 File Offset: 0x000190B5
		internal static string MetadataBinder_QueryOptionsBindMethodCannotBeNull
		{
			get
			{
				return TextRes.GetString("MetadataBinder_QueryOptionsBindMethodCannotBeNull");
			}
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x0001AEC1 File Offset: 0x000190C1
		internal static string MetadataBinder_HierarchyNotFollowed(object p0, object p1)
		{
			return TextRes.GetString("MetadataBinder_HierarchyNotFollowed", new object[] { p0, p1 });
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000AAA RID: 2730 RVA: 0x0001AEDB File Offset: 0x000190DB
		internal static string MetadataBinder_LambdaParentMustBeCollection
		{
			get
			{
				return TextRes.GetString("MetadataBinder_LambdaParentMustBeCollection");
			}
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x0001AEE7 File Offset: 0x000190E7
		internal static string MetadataBinder_ParameterNotInScope(object p0)
		{
			return TextRes.GetString("MetadataBinder_ParameterNotInScope", new object[] { p0 });
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000AAC RID: 2732 RVA: 0x0001AEFD File Offset: 0x000190FD
		internal static string MetadataBinder_NavigationPropertyNotFollowingSingleEntityType
		{
			get
			{
				return TextRes.GetString("MetadataBinder_NavigationPropertyNotFollowingSingleEntityType");
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000AAD RID: 2733 RVA: 0x0001AF09 File Offset: 0x00019109
		internal static string MetadataBinder_AnyAllExpressionNotSingleValue
		{
			get
			{
				return TextRes.GetString("MetadataBinder_AnyAllExpressionNotSingleValue");
			}
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x0001AF15 File Offset: 0x00019115
		internal static string MetadataBinder_CastOrIsOfExpressionWithWrongNumberOfOperands(object p0)
		{
			return TextRes.GetString("MetadataBinder_CastOrIsOfExpressionWithWrongNumberOfOperands", new object[] { p0 });
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000AAF RID: 2735 RVA: 0x0001AF2B File Offset: 0x0001912B
		internal static string MetadataBinder_CastOrIsOfFunctionWithoutATypeArgument
		{
			get
			{
				return TextRes.GetString("MetadataBinder_CastOrIsOfFunctionWithoutATypeArgument");
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000AB0 RID: 2736 RVA: 0x0001AF37 File Offset: 0x00019137
		internal static string MetadataBinder_CastOrIsOfCollectionsNotSupported
		{
			get
			{
				return TextRes.GetString("MetadataBinder_CastOrIsOfCollectionsNotSupported");
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000AB1 RID: 2737 RVA: 0x0001AF43 File Offset: 0x00019143
		internal static string MetadataBinder_CollectionOpenPropertiesNotSupportedInThisRelease
		{
			get
			{
				return TextRes.GetString("MetadataBinder_CollectionOpenPropertiesNotSupportedInThisRelease");
			}
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x0001AF4F File Offset: 0x0001914F
		internal static string MetadataBinder_IllegalSegmentType(object p0)
		{
			return TextRes.GetString("MetadataBinder_IllegalSegmentType", new object[] { p0 });
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x0001AF65 File Offset: 0x00019165
		internal static string MetadataBinder_QueryOptionNotApplicable(object p0)
		{
			return TextRes.GetString("MetadataBinder_QueryOptionNotApplicable", new object[] { p0 });
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x0001AF7B File Offset: 0x0001917B
		internal static string ApplyBinder_AggregateExpressionIncompatibleTypeForMethod(object p0, object p1)
		{
			return TextRes.GetString("ApplyBinder_AggregateExpressionIncompatibleTypeForMethod", new object[] { p0, p1 });
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x0001AF95 File Offset: 0x00019195
		internal static string ApplyBinder_UnsupportedAggregateMethod(object p0)
		{
			return TextRes.GetString("ApplyBinder_UnsupportedAggregateMethod", new object[] { p0 });
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x0001AFAB File Offset: 0x000191AB
		internal static string ApplyBinder_AggregateExpressionNotSingleValue(object p0)
		{
			return TextRes.GetString("ApplyBinder_AggregateExpressionNotSingleValue", new object[] { p0 });
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x0001AFC1 File Offset: 0x000191C1
		internal static string ApplyBinder_GroupByPropertyNotPropertyAccessValue(object p0)
		{
			return TextRes.GetString("ApplyBinder_GroupByPropertyNotPropertyAccessValue", new object[] { p0 });
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x0001AFD7 File Offset: 0x000191D7
		internal static string ApplyBinder_UnsupportedType(object p0)
		{
			return TextRes.GetString("ApplyBinder_UnsupportedType", new object[] { p0 });
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x0001AFED File Offset: 0x000191ED
		internal static string ApplyBinder_UnsupportedGroupByChild(object p0)
		{
			return TextRes.GetString("ApplyBinder_UnsupportedGroupByChild", new object[] { p0 });
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x0001B003 File Offset: 0x00019203
		internal static string FunctionCallBinder_CannotFindASuitableOverload(object p0, object p1)
		{
			return TextRes.GetString("FunctionCallBinder_CannotFindASuitableOverload", new object[] { p0, p1 });
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x0001B01D File Offset: 0x0001921D
		internal static string FunctionCallBinder_UriFunctionMustHaveHaveNullParent(object p0)
		{
			return TextRes.GetString("FunctionCallBinder_UriFunctionMustHaveHaveNullParent", new object[] { p0 });
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x0001B033 File Offset: 0x00019233
		internal static string FunctionCallBinder_CallingFunctionOnOpenProperty(object p0)
		{
			return TextRes.GetString("FunctionCallBinder_CallingFunctionOnOpenProperty", new object[] { p0 });
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000ABD RID: 2749 RVA: 0x0001B049 File Offset: 0x00019249
		internal static string FunctionCallParser_DuplicateParameterOrEntityKeyName
		{
			get
			{
				return TextRes.GetString("FunctionCallParser_DuplicateParameterOrEntityKeyName");
			}
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x0001B055 File Offset: 0x00019255
		internal static string ODataUriParser_InvalidCount(object p0)
		{
			return TextRes.GetString("ODataUriParser_InvalidCount", new object[] { p0 });
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x0001B06B File Offset: 0x0001926B
		internal static string CastBinder_ChildTypeIsNotEntity(object p0)
		{
			return TextRes.GetString("CastBinder_ChildTypeIsNotEntity", new object[] { p0 });
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000AC0 RID: 2752 RVA: 0x0001B081 File Offset: 0x00019281
		internal static string CastBinder_EnumOnlyCastToOrFromString
		{
			get
			{
				return TextRes.GetString("CastBinder_EnumOnlyCastToOrFromString");
			}
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x0001B08D File Offset: 0x0001928D
		internal static string Binder_IsNotValidEnumConstant(object p0)
		{
			return TextRes.GetString("Binder_IsNotValidEnumConstant", new object[] { p0 });
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x0001B0A3 File Offset: 0x000192A3
		internal static string BatchReferenceSegment_InvalidContentID(object p0)
		{
			return TextRes.GetString("BatchReferenceSegment_InvalidContentID", new object[] { p0 });
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x0001B0B9 File Offset: 0x000192B9
		internal static string SelectExpandBinder_UnknownPropertyType(object p0)
		{
			return TextRes.GetString("SelectExpandBinder_UnknownPropertyType", new object[] { p0 });
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x0001B0CF File Offset: 0x000192CF
		internal static string SelectionItemBinder_NoExpandForSelectedProperty(object p0)
		{
			return TextRes.GetString("SelectionItemBinder_NoExpandForSelectedProperty", new object[] { p0 });
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x0001B0E5 File Offset: 0x000192E5
		internal static string SelectExpandPathBinder_FollowNonTypeSegment(object p0)
		{
			return TextRes.GetString("SelectExpandPathBinder_FollowNonTypeSegment", new object[] { p0 });
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x0001B0FB File Offset: 0x000192FB
		internal static string SelectPropertyVisitor_SystemTokenInSelect(object p0)
		{
			return TextRes.GetString("SelectPropertyVisitor_SystemTokenInSelect", new object[] { p0 });
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x0001B111 File Offset: 0x00019311
		internal static string SelectPropertyVisitor_DisparateTypeSegmentsInSelectExpand
		{
			get
			{
				return TextRes.GetString("SelectPropertyVisitor_DisparateTypeSegmentsInSelectExpand");
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000AC8 RID: 2760 RVA: 0x0001B11D File Offset: 0x0001931D
		internal static string SelectBinder_MultiLevelPathInSelect
		{
			get
			{
				return TextRes.GetString("SelectBinder_MultiLevelPathInSelect");
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000AC9 RID: 2761 RVA: 0x0001B129 File Offset: 0x00019329
		internal static string ExpandItemBinder_TraversingANonNormalizedTree
		{
			get
			{
				return TextRes.GetString("ExpandItemBinder_TraversingANonNormalizedTree");
			}
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x0001B135 File Offset: 0x00019335
		internal static string ExpandItemBinder_CannotFindType(object p0)
		{
			return TextRes.GetString("ExpandItemBinder_CannotFindType", new object[] { p0 });
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x0001B14B File Offset: 0x0001934B
		internal static string ExpandItemBinder_PropertyIsNotANavigationPropertyOrComplexProperty(object p0, object p1)
		{
			return TextRes.GetString("ExpandItemBinder_PropertyIsNotANavigationPropertyOrComplexProperty", new object[] { p0, p1 });
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000ACC RID: 2764 RVA: 0x0001B165 File Offset: 0x00019365
		internal static string ExpandItemBinder_TypeSegmentNotFollowedByPath
		{
			get
			{
				return TextRes.GetString("ExpandItemBinder_TypeSegmentNotFollowedByPath");
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000ACD RID: 2765 RVA: 0x0001B171 File Offset: 0x00019371
		internal static string ExpandItemBinder_PathTooDeep
		{
			get
			{
				return TextRes.GetString("ExpandItemBinder_PathTooDeep");
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000ACE RID: 2766 RVA: 0x0001B17D File Offset: 0x0001937D
		internal static string ExpandItemBinder_TraversingMultipleNavPropsInTheSamePath
		{
			get
			{
				return TextRes.GetString("ExpandItemBinder_TraversingMultipleNavPropsInTheSamePath");
			}
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x0001B189 File Offset: 0x00019389
		internal static string ExpandItemBinder_LevelsNotAllowedOnIncompatibleRelatedType(object p0, object p1, object p2)
		{
			return TextRes.GetString("ExpandItemBinder_LevelsNotAllowedOnIncompatibleRelatedType", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x0001B1A7 File Offset: 0x000193A7
		internal static string ExpandItemBinder_InvaidSegmentInExpand(object p0)
		{
			return TextRes.GetString("ExpandItemBinder_InvaidSegmentInExpand", new object[] { p0 });
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000AD1 RID: 2769 RVA: 0x0001B1BD File Offset: 0x000193BD
		internal static string Nodes_CollectionNavigationNode_MustHaveSingleMultiplicity
		{
			get
			{
				return TextRes.GetString("Nodes_CollectionNavigationNode_MustHaveSingleMultiplicity");
			}
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x0001B1C9 File Offset: 0x000193C9
		internal static string Nodes_NonentityParameterQueryNodeWithEntityType(object p0)
		{
			return TextRes.GetString("Nodes_NonentityParameterQueryNodeWithEntityType", new object[] { p0 });
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000AD3 RID: 2771 RVA: 0x0001B1DF File Offset: 0x000193DF
		internal static string Nodes_CollectionNavigationNode_MustHaveManyMultiplicity
		{
			get
			{
				return TextRes.GetString("Nodes_CollectionNavigationNode_MustHaveManyMultiplicity");
			}
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x0001B1EB File Offset: 0x000193EB
		internal static string Nodes_PropertyAccessShouldBeNonEntityProperty(object p0)
		{
			return TextRes.GetString("Nodes_PropertyAccessShouldBeNonEntityProperty", new object[] { p0 });
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x0001B201 File Offset: 0x00019401
		internal static string Nodes_PropertyAccessTypeShouldNotBeCollection(object p0)
		{
			return TextRes.GetString("Nodes_PropertyAccessTypeShouldNotBeCollection", new object[] { p0 });
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x0001B217 File Offset: 0x00019417
		internal static string Nodes_PropertyAccessTypeMustBeCollection(object p0)
		{
			return TextRes.GetString("Nodes_PropertyAccessTypeMustBeCollection", new object[] { p0 });
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000AD7 RID: 2775 RVA: 0x0001B22D File Offset: 0x0001942D
		internal static string Nodes_NonStaticEntitySetExpressionsAreNotSupportedInThisRelease
		{
			get
			{
				return TextRes.GetString("Nodes_NonStaticEntitySetExpressionsAreNotSupportedInThisRelease");
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000AD8 RID: 2776 RVA: 0x0001B239 File Offset: 0x00019439
		internal static string Nodes_CollectionFunctionCallNode_ItemTypeMustBePrimitiveOrComplexOrEnum
		{
			get
			{
				return TextRes.GetString("Nodes_CollectionFunctionCallNode_ItemTypeMustBePrimitiveOrComplexOrEnum");
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000AD9 RID: 2777 RVA: 0x0001B245 File Offset: 0x00019445
		internal static string Nodes_EntityCollectionFunctionCallNode_ItemTypeMustBeAnEntity
		{
			get
			{
				return TextRes.GetString("Nodes_EntityCollectionFunctionCallNode_ItemTypeMustBeAnEntity");
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000ADA RID: 2778 RVA: 0x0001B251 File Offset: 0x00019451
		internal static string Nodes_SingleValueFunctionCallNode_ItemTypeMustBePrimitiveOrComplexOrEnum
		{
			get
			{
				return TextRes.GetString("Nodes_SingleValueFunctionCallNode_ItemTypeMustBePrimitiveOrComplexOrEnum");
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000ADB RID: 2779 RVA: 0x0001B25D File Offset: 0x0001945D
		internal static string ExpandTreeNormalizer_NonPathInPropertyChain
		{
			get
			{
				return TextRes.GetString("ExpandTreeNormalizer_NonPathInPropertyChain");
			}
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x0001B269 File Offset: 0x00019469
		internal static string UriExpandParser_TermIsNotValidForStar(object p0)
		{
			return TextRes.GetString("UriExpandParser_TermIsNotValidForStar", new object[] { p0 });
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x0001B27F File Offset: 0x0001947F
		internal static string UriExpandParser_TermIsNotValidForStarRef(object p0)
		{
			return TextRes.GetString("UriExpandParser_TermIsNotValidForStarRef", new object[] { p0 });
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x0001B295 File Offset: 0x00019495
		internal static string UriExpandParser_ParentEntityIsNull(object p0)
		{
			return TextRes.GetString("UriExpandParser_ParentEntityIsNull", new object[] { p0 });
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x0001B2AB File Offset: 0x000194AB
		internal static string UriExpandParser_TermWithMultipleStarNotAllowed(object p0)
		{
			return TextRes.GetString("UriExpandParser_TermWithMultipleStarNotAllowed", new object[] { p0 });
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x0001B2C1 File Offset: 0x000194C1
		internal static string UriSelectParser_TermIsNotValid(object p0)
		{
			return TextRes.GetString("UriSelectParser_TermIsNotValid", new object[] { p0 });
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x0001B2D7 File Offset: 0x000194D7
		internal static string UriSelectParser_InvalidTopOption(object p0)
		{
			return TextRes.GetString("UriSelectParser_InvalidTopOption", new object[] { p0 });
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x0001B2ED File Offset: 0x000194ED
		internal static string UriSelectParser_InvalidSkipOption(object p0)
		{
			return TextRes.GetString("UriSelectParser_InvalidSkipOption", new object[] { p0 });
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x0001B303 File Offset: 0x00019503
		internal static string UriSelectParser_InvalidCountOption(object p0)
		{
			return TextRes.GetString("UriSelectParser_InvalidCountOption", new object[] { p0 });
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x0001B319 File Offset: 0x00019519
		internal static string UriSelectParser_InvalidLevelsOption(object p0)
		{
			return TextRes.GetString("UriSelectParser_InvalidLevelsOption", new object[] { p0 });
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x0001B32F File Offset: 0x0001952F
		internal static string UriSelectParser_SystemTokenInSelectExpand(object p0, object p1)
		{
			return TextRes.GetString("UriSelectParser_SystemTokenInSelectExpand", new object[] { p0, p1 });
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x0001B349 File Offset: 0x00019549
		internal static string UriParser_MissingExpandOption(object p0)
		{
			return TextRes.GetString("UriParser_MissingExpandOption", new object[] { p0 });
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000AE7 RID: 2791 RVA: 0x0001B35F File Offset: 0x0001955F
		internal static string UriParser_RelativeUriMustBeRelative
		{
			get
			{
				return TextRes.GetString("UriParser_RelativeUriMustBeRelative");
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000AE8 RID: 2792 RVA: 0x0001B36B File Offset: 0x0001956B
		internal static string UriParser_NeedServiceRootForThisOverload
		{
			get
			{
				return TextRes.GetString("UriParser_NeedServiceRootForThisOverload");
			}
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x0001B377 File Offset: 0x00019577
		internal static string UriParser_UriMustBeAbsolute(object p0)
		{
			return TextRes.GetString("UriParser_UriMustBeAbsolute", new object[] { p0 });
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000AEA RID: 2794 RVA: 0x0001B38D File Offset: 0x0001958D
		internal static string UriParser_NegativeLimit
		{
			get
			{
				return TextRes.GetString("UriParser_NegativeLimit");
			}
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x0001B399 File Offset: 0x00019599
		internal static string UriParser_ExpandCountExceeded(object p0, object p1)
		{
			return TextRes.GetString("UriParser_ExpandCountExceeded", new object[] { p0, p1 });
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x0001B3B3 File Offset: 0x000195B3
		internal static string UriParser_ExpandDepthExceeded(object p0, object p1)
		{
			return TextRes.GetString("UriParser_ExpandDepthExceeded", new object[] { p0, p1 });
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x0001B3CD File Offset: 0x000195CD
		internal static string UriParser_TypeInvalidForSelectExpand(object p0)
		{
			return TextRes.GetString("UriParser_TypeInvalidForSelectExpand", new object[] { p0 });
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x0001B3E3 File Offset: 0x000195E3
		internal static string UriParser_ContextHandlerCanNotBeNull(object p0)
		{
			return TextRes.GetString("UriParser_ContextHandlerCanNotBeNull", new object[] { p0 });
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x0001B3F9 File Offset: 0x000195F9
		internal static string UriParserMetadata_MultipleMatchingPropertiesFound(object p0, object p1)
		{
			return TextRes.GetString("UriParserMetadata_MultipleMatchingPropertiesFound", new object[] { p0, p1 });
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x0001B413 File Offset: 0x00019613
		internal static string UriParserMetadata_MultipleMatchingNavigationSourcesFound(object p0)
		{
			return TextRes.GetString("UriParserMetadata_MultipleMatchingNavigationSourcesFound", new object[] { p0 });
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x0001B429 File Offset: 0x00019629
		internal static string UriParserMetadata_MultipleMatchingTypesFound(object p0)
		{
			return TextRes.GetString("UriParserMetadata_MultipleMatchingTypesFound", new object[] { p0 });
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x0001B43F File Offset: 0x0001963F
		internal static string UriParserMetadata_MultipleMatchingKeysFound(object p0)
		{
			return TextRes.GetString("UriParserMetadata_MultipleMatchingKeysFound", new object[] { p0 });
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x0001B455 File Offset: 0x00019655
		internal static string UriParserMetadata_MultipleMatchingParametersFound(object p0)
		{
			return TextRes.GetString("UriParserMetadata_MultipleMatchingParametersFound", new object[] { p0 });
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x0001B46B File Offset: 0x0001966B
		internal static string PathParser_EntityReferenceNotSupported(object p0)
		{
			return TextRes.GetString("PathParser_EntityReferenceNotSupported", new object[] { p0 });
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000AF5 RID: 2805 RVA: 0x0001B481 File Offset: 0x00019681
		internal static string PathParser_CannotUseValueOnCollection
		{
			get
			{
				return TextRes.GetString("PathParser_CannotUseValueOnCollection");
			}
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x0001B48D File Offset: 0x0001968D
		internal static string PathParser_TypeMustBeRelatedToSet(object p0, object p1, object p2)
		{
			return TextRes.GetString("PathParser_TypeMustBeRelatedToSet", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x0001B4AB File Offset: 0x000196AB
		internal static string PathParser_TypeCastOnlyAllowedAfterStructuralCollection(object p0)
		{
			return TextRes.GetString("PathParser_TypeCastOnlyAllowedAfterStructuralCollection", new object[] { p0 });
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000AF8 RID: 2808 RVA: 0x0001B4C1 File Offset: 0x000196C1
		internal static string ODataResourceSet_MustNotContainBothNextPageLinkAndDeltaLink
		{
			get
			{
				return TextRes.GetString("ODataResourceSet_MustNotContainBothNextPageLinkAndDeltaLink");
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000AF9 RID: 2809 RVA: 0x0001B4CD File Offset: 0x000196CD
		internal static string ODataExpandPath_OnlyLastSegmentMustBeNavigationProperty
		{
			get
			{
				return TextRes.GetString("ODataExpandPath_OnlyLastSegmentMustBeNavigationProperty");
			}
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x0001B4D9 File Offset: 0x000196D9
		internal static string ODataExpandPath_InvalidExpandPathSegment(object p0)
		{
			return TextRes.GetString("ODataExpandPath_InvalidExpandPathSegment", new object[] { p0 });
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000AFB RID: 2811 RVA: 0x0001B4EF File Offset: 0x000196EF
		internal static string ODataSelectPath_CannotOnlyHaveTypeSegment
		{
			get
			{
				return TextRes.GetString("ODataSelectPath_CannotOnlyHaveTypeSegment");
			}
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x0001B4FB File Offset: 0x000196FB
		internal static string ODataSelectPath_InvalidSelectPathSegmentType(object p0)
		{
			return TextRes.GetString("ODataSelectPath_InvalidSelectPathSegmentType", new object[] { p0 });
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000AFD RID: 2813 RVA: 0x0001B511 File Offset: 0x00019711
		internal static string ODataSelectPath_OperationSegmentCanOnlyBeLastSegment
		{
			get
			{
				return TextRes.GetString("ODataSelectPath_OperationSegmentCanOnlyBeLastSegment");
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000AFE RID: 2814 RVA: 0x0001B51D File Offset: 0x0001971D
		internal static string ODataSelectPath_NavPropSegmentCanOnlyBeLastSegment
		{
			get
			{
				return TextRes.GetString("ODataSelectPath_NavPropSegmentCanOnlyBeLastSegment");
			}
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x0001B529 File Offset: 0x00019729
		internal static string RequestUriProcessor_TargetEntitySetNotFound(object p0)
		{
			return TextRes.GetString("RequestUriProcessor_TargetEntitySetNotFound", new object[] { p0 });
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x0001B53F File Offset: 0x0001973F
		internal static string RequestUriProcessor_FoundInvalidFunctionImport(object p0)
		{
			return TextRes.GetString("RequestUriProcessor_FoundInvalidFunctionImport", new object[] { p0 });
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000B01 RID: 2817 RVA: 0x0001B555 File Offset: 0x00019755
		internal static string OperationSegment_ReturnTypeForMultipleOverloads
		{
			get
			{
				return TextRes.GetString("OperationSegment_ReturnTypeForMultipleOverloads");
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000B02 RID: 2818 RVA: 0x0001B561 File Offset: 0x00019761
		internal static string OperationSegment_CannotReturnNull
		{
			get
			{
				return TextRes.GetString("OperationSegment_CannotReturnNull");
			}
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x0001B56D File Offset: 0x0001976D
		internal static string FunctionOverloadResolver_NoSingleMatchFound(object p0, object p1)
		{
			return TextRes.GetString("FunctionOverloadResolver_NoSingleMatchFound", new object[] { p0, p1 });
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x0001B587 File Offset: 0x00019787
		internal static string FunctionOverloadResolver_MultipleActionOverloads(object p0)
		{
			return TextRes.GetString("FunctionOverloadResolver_MultipleActionOverloads", new object[] { p0 });
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x0001B59D File Offset: 0x0001979D
		internal static string FunctionOverloadResolver_MultipleActionImportOverloads(object p0)
		{
			return TextRes.GetString("FunctionOverloadResolver_MultipleActionImportOverloads", new object[] { p0 });
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x0001B5B3 File Offset: 0x000197B3
		internal static string FunctionOverloadResolver_MultipleOperationImportOverloads(object p0)
		{
			return TextRes.GetString("FunctionOverloadResolver_MultipleOperationImportOverloads", new object[] { p0 });
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x0001B5C9 File Offset: 0x000197C9
		internal static string FunctionOverloadResolver_MultipleOperationOverloads(object p0)
		{
			return TextRes.GetString("FunctionOverloadResolver_MultipleOperationOverloads", new object[] { p0 });
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x0001B5DF File Offset: 0x000197DF
		internal static string FunctionOverloadResolver_FoundInvalidOperation(object p0)
		{
			return TextRes.GetString("FunctionOverloadResolver_FoundInvalidOperation", new object[] { p0 });
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x0001B5F5 File Offset: 0x000197F5
		internal static string FunctionOverloadResolver_FoundInvalidOperationImport(object p0)
		{
			return TextRes.GetString("FunctionOverloadResolver_FoundInvalidOperationImport", new object[] { p0 });
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x0001B60B File Offset: 0x0001980B
		internal static string CustomUriFunctions_AddCustomUriFunction_BuiltInExistsNotAddingAsOverload(object p0)
		{
			return TextRes.GetString("CustomUriFunctions_AddCustomUriFunction_BuiltInExistsNotAddingAsOverload", new object[] { p0 });
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x0001B621 File Offset: 0x00019821
		internal static string CustomUriFunctions_AddCustomUriFunction_BuiltInExistsFullSignature(object p0)
		{
			return TextRes.GetString("CustomUriFunctions_AddCustomUriFunction_BuiltInExistsFullSignature", new object[] { p0 });
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x0001B637 File Offset: 0x00019837
		internal static string CustomUriFunctions_AddCustomUriFunction_CustomFunctionOverloadExists(object p0)
		{
			return TextRes.GetString("CustomUriFunctions_AddCustomUriFunction_CustomFunctionOverloadExists", new object[] { p0 });
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x0001B64D File Offset: 0x0001984D
		internal static string RequestUriProcessor_InvalidValueForEntitySegment(object p0)
		{
			return TextRes.GetString("RequestUriProcessor_InvalidValueForEntitySegment", new object[] { p0 });
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x0001B663 File Offset: 0x00019863
		internal static string RequestUriProcessor_InvalidValueForKeySegment(object p0)
		{
			return TextRes.GetString("RequestUriProcessor_InvalidValueForKeySegment", new object[] { p0 });
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000B0F RID: 2831 RVA: 0x0001B679 File Offset: 0x00019879
		internal static string RequestUriProcessor_EmptySegmentInRequestUrl
		{
			get
			{
				return TextRes.GetString("RequestUriProcessor_EmptySegmentInRequestUrl");
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000B10 RID: 2832 RVA: 0x0001B685 File Offset: 0x00019885
		internal static string RequestUriProcessor_SyntaxError
		{
			get
			{
				return TextRes.GetString("RequestUriProcessor_SyntaxError");
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000B11 RID: 2833 RVA: 0x0001B691 File Offset: 0x00019891
		internal static string RequestUriProcessor_CountOnRoot
		{
			get
			{
				return TextRes.GetString("RequestUriProcessor_CountOnRoot");
			}
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x0001B69D File Offset: 0x0001989D
		internal static string RequestUriProcessor_MustBeLeafSegment(object p0)
		{
			return TextRes.GetString("RequestUriProcessor_MustBeLeafSegment", new object[] { p0 });
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x0001B6B3 File Offset: 0x000198B3
		internal static string RequestUriProcessor_LinkSegmentMustBeFollowedByEntitySegment(object p0, object p1)
		{
			return TextRes.GetString("RequestUriProcessor_LinkSegmentMustBeFollowedByEntitySegment", new object[] { p0, p1 });
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x0001B6CD File Offset: 0x000198CD
		internal static string RequestUriProcessor_MissingSegmentAfterLink(object p0)
		{
			return TextRes.GetString("RequestUriProcessor_MissingSegmentAfterLink", new object[] { p0 });
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x0001B6E3 File Offset: 0x000198E3
		internal static string RequestUriProcessor_CountNotSupported(object p0)
		{
			return TextRes.GetString("RequestUriProcessor_CountNotSupported", new object[] { p0 });
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x0001B6F9 File Offset: 0x000198F9
		internal static string RequestUriProcessor_CannotQueryCollections(object p0)
		{
			return TextRes.GetString("RequestUriProcessor_CannotQueryCollections", new object[] { p0 });
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x0001B70F File Offset: 0x0001990F
		internal static string RequestUriProcessor_SegmentDoesNotSupportKeyPredicates(object p0)
		{
			return TextRes.GetString("RequestUriProcessor_SegmentDoesNotSupportKeyPredicates", new object[] { p0 });
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x0001B725 File Offset: 0x00019925
		internal static string RequestUriProcessor_ValueSegmentAfterScalarPropertySegment(object p0, object p1)
		{
			return TextRes.GetString("RequestUriProcessor_ValueSegmentAfterScalarPropertySegment", new object[] { p0, p1 });
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x0001B73F File Offset: 0x0001993F
		internal static string RequestUriProcessor_InvalidTypeIdentifier_UnrelatedType(object p0, object p1)
		{
			return TextRes.GetString("RequestUriProcessor_InvalidTypeIdentifier_UnrelatedType", new object[] { p0, p1 });
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x0001B759 File Offset: 0x00019959
		internal static string OpenNavigationPropertiesNotSupportedOnOpenTypes(object p0)
		{
			return TextRes.GetString("OpenNavigationPropertiesNotSupportedOnOpenTypes", new object[] { p0 });
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000B1B RID: 2843 RVA: 0x0001B76F File Offset: 0x0001996F
		internal static string BadRequest_ResourceCanBeCrossReferencedOnlyForBindOperation
		{
			get
			{
				return TextRes.GetString("BadRequest_ResourceCanBeCrossReferencedOnlyForBindOperation");
			}
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x0001B77B File Offset: 0x0001997B
		internal static string DataServiceConfiguration_ResponseVersionIsBiggerThanProtocolVersion(object p0, object p1)
		{
			return TextRes.GetString("DataServiceConfiguration_ResponseVersionIsBiggerThanProtocolVersion", new object[] { p0, p1 });
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x0001B795 File Offset: 0x00019995
		internal static string BadRequest_KeyCountMismatch(object p0)
		{
			return TextRes.GetString("BadRequest_KeyCountMismatch", new object[] { p0 });
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000B1E RID: 2846 RVA: 0x0001B7AB File Offset: 0x000199AB
		internal static string RequestUriProcessor_KeysMustBeNamed
		{
			get
			{
				return TextRes.GetString("RequestUriProcessor_KeysMustBeNamed");
			}
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x0001B7B7 File Offset: 0x000199B7
		internal static string RequestUriProcessor_ResourceNotFound(object p0)
		{
			return TextRes.GetString("RequestUriProcessor_ResourceNotFound", new object[] { p0 });
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x0001B7CD File Offset: 0x000199CD
		internal static string RequestUriProcessor_BatchedActionOnEntityCreatedInSameChangeset(object p0)
		{
			return TextRes.GetString("RequestUriProcessor_BatchedActionOnEntityCreatedInSameChangeset", new object[] { p0 });
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000B21 RID: 2849 RVA: 0x0001B7E3 File Offset: 0x000199E3
		internal static string RequestUriProcessor_Forbidden
		{
			get
			{
				return TextRes.GetString("RequestUriProcessor_Forbidden");
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000B22 RID: 2850 RVA: 0x0001B7EF File Offset: 0x000199EF
		internal static string RequestUriProcessor_OperationSegmentBoundToANonEntityType
		{
			get
			{
				return TextRes.GetString("RequestUriProcessor_OperationSegmentBoundToANonEntityType");
			}
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x0001B7FB File Offset: 0x000199FB
		internal static string General_InternalError(object p0)
		{
			return TextRes.GetString("General_InternalError", new object[] { p0 });
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x0001B811 File Offset: 0x00019A11
		internal static string ExceptionUtils_CheckIntegerNotNegative(object p0)
		{
			return TextRes.GetString("ExceptionUtils_CheckIntegerNotNegative", new object[] { p0 });
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x0001B827 File Offset: 0x00019A27
		internal static string ExceptionUtils_CheckIntegerPositive(object p0)
		{
			return TextRes.GetString("ExceptionUtils_CheckIntegerPositive", new object[] { p0 });
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x0001B83D File Offset: 0x00019A3D
		internal static string ExceptionUtils_CheckLongPositive(object p0)
		{
			return TextRes.GetString("ExceptionUtils_CheckLongPositive", new object[] { p0 });
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000B27 RID: 2855 RVA: 0x0001B853 File Offset: 0x00019A53
		internal static string ExceptionUtils_ArgumentStringNullOrEmpty
		{
			get
			{
				return TextRes.GetString("ExceptionUtils_ArgumentStringNullOrEmpty");
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000B28 RID: 2856 RVA: 0x0001B85F File Offset: 0x00019A5F
		internal static string ExpressionToken_OnlyRefAllowWithStarInExpand
		{
			get
			{
				return TextRes.GetString("ExpressionToken_OnlyRefAllowWithStarInExpand");
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000B29 RID: 2857 RVA: 0x0001B86B File Offset: 0x00019A6B
		internal static string ExpressionToken_NoPropAllowedAfterRef
		{
			get
			{
				return TextRes.GetString("ExpressionToken_NoPropAllowedAfterRef");
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000B2A RID: 2858 RVA: 0x0001B877 File Offset: 0x00019A77
		internal static string ExpressionToken_NoSegmentAllowedBeforeStarInExpand
		{
			get
			{
				return TextRes.GetString("ExpressionToken_NoSegmentAllowedBeforeStarInExpand");
			}
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x0001B883 File Offset: 0x00019A83
		internal static string ExpressionToken_IdentifierExpected(object p0)
		{
			return TextRes.GetString("ExpressionToken_IdentifierExpected", new object[] { p0 });
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x0001B899 File Offset: 0x00019A99
		internal static string ExpressionLexer_UnterminatedStringLiteral(object p0, object p1)
		{
			return TextRes.GetString("ExpressionLexer_UnterminatedStringLiteral", new object[] { p0, p1 });
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x0001B8B3 File Offset: 0x00019AB3
		internal static string ExpressionLexer_InvalidCharacter(object p0, object p1, object p2)
		{
			return TextRes.GetString("ExpressionLexer_InvalidCharacter", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x0001B8D1 File Offset: 0x00019AD1
		internal static string ExpressionLexer_SyntaxError(object p0, object p1)
		{
			return TextRes.GetString("ExpressionLexer_SyntaxError", new object[] { p0, p1 });
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x0001B8EB File Offset: 0x00019AEB
		internal static string ExpressionLexer_UnterminatedLiteral(object p0, object p1)
		{
			return TextRes.GetString("ExpressionLexer_UnterminatedLiteral", new object[] { p0, p1 });
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x0001B905 File Offset: 0x00019B05
		internal static string ExpressionLexer_DigitExpected(object p0, object p1)
		{
			return TextRes.GetString("ExpressionLexer_DigitExpected", new object[] { p0, p1 });
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000B31 RID: 2865 RVA: 0x0001B91F File Offset: 0x00019B1F
		internal static string ExpressionLexer_UnbalancedBracketExpression
		{
			get
			{
				return TextRes.GetString("ExpressionLexer_UnbalancedBracketExpression");
			}
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x0001B92B File Offset: 0x00019B2B
		internal static string ExpressionLexer_InvalidNumericString(object p0)
		{
			return TextRes.GetString("ExpressionLexer_InvalidNumericString", new object[] { p0 });
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x0001B941 File Offset: 0x00019B41
		internal static string ExpressionLexer_InvalidEscapeSequence(object p0, object p1, object p2)
		{
			return TextRes.GetString("ExpressionLexer_InvalidEscapeSequence", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x0001B95F File Offset: 0x00019B5F
		internal static string UriQueryExpressionParser_UnrecognizedLiteral(object p0, object p1, object p2, object p3)
		{
			return TextRes.GetString("UriQueryExpressionParser_UnrecognizedLiteral", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x0001B981 File Offset: 0x00019B81
		internal static string UriQueryExpressionParser_UnrecognizedLiteralWithReason(object p0, object p1, object p2, object p3, object p4)
		{
			return TextRes.GetString("UriQueryExpressionParser_UnrecognizedLiteralWithReason", new object[] { p0, p1, p2, p3, p4 });
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x0001B9A8 File Offset: 0x00019BA8
		internal static string UriPrimitiveTypeParsers_FailedToParseTextToPrimitiveValue(object p0, object p1)
		{
			return TextRes.GetString("UriPrimitiveTypeParsers_FailedToParseTextToPrimitiveValue", new object[] { p0, p1 });
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000B37 RID: 2871 RVA: 0x0001B9C2 File Offset: 0x00019BC2
		internal static string UriPrimitiveTypeParsers_FailedToParseStringToGeography
		{
			get
			{
				return TextRes.GetString("UriPrimitiveTypeParsers_FailedToParseStringToGeography");
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000B38 RID: 2872 RVA: 0x0001B9CE File Offset: 0x00019BCE
		internal static string UriCustomTypeParsers_AddCustomUriTypeParserAlreadyExists
		{
			get
			{
				return TextRes.GetString("UriCustomTypeParsers_AddCustomUriTypeParserAlreadyExists");
			}
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x0001B9DA File Offset: 0x00019BDA
		internal static string UriCustomTypeParsers_AddCustomUriTypeParserEdmTypeExists(object p0)
		{
			return TextRes.GetString("UriCustomTypeParsers_AddCustomUriTypeParserEdmTypeExists", new object[] { p0 });
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x0001B9F0 File Offset: 0x00019BF0
		internal static string UriParserHelper_InvalidPrefixLiteral(object p0)
		{
			return TextRes.GetString("UriParserHelper_InvalidPrefixLiteral", new object[] { p0 });
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x0001BA06 File Offset: 0x00019C06
		internal static string CustomUriTypePrefixLiterals_AddCustomUriTypePrefixLiteralAlreadyExists(object p0)
		{
			return TextRes.GetString("CustomUriTypePrefixLiterals_AddCustomUriTypePrefixLiteralAlreadyExists", new object[] { p0 });
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x0001BA1C File Offset: 0x00019C1C
		internal static string ValueParser_InvalidDuration(object p0)
		{
			return TextRes.GetString("ValueParser_InvalidDuration", new object[] { p0 });
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x0001BA32 File Offset: 0x00019C32
		internal static string PlatformHelper_DateTimeOffsetMustContainTimeZone(object p0)
		{
			return TextRes.GetString("PlatformHelper_DateTimeOffsetMustContainTimeZone", new object[] { p0 });
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x0001BA48 File Offset: 0x00019C48
		internal static string JsonReader_UnexpectedComma(object p0)
		{
			return TextRes.GetString("JsonReader_UnexpectedComma", new object[] { p0 });
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000B3F RID: 2879 RVA: 0x0001BA5E File Offset: 0x00019C5E
		internal static string JsonReader_MultipleTopLevelValues
		{
			get
			{
				return TextRes.GetString("JsonReader_MultipleTopLevelValues");
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000B40 RID: 2880 RVA: 0x0001BA6A File Offset: 0x00019C6A
		internal static string JsonReader_EndOfInputWithOpenScope
		{
			get
			{
				return TextRes.GetString("JsonReader_EndOfInputWithOpenScope");
			}
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x0001BA76 File Offset: 0x00019C76
		internal static string JsonReader_UnexpectedToken(object p0)
		{
			return TextRes.GetString("JsonReader_UnexpectedToken", new object[] { p0 });
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000B42 RID: 2882 RVA: 0x0001BA8C File Offset: 0x00019C8C
		internal static string JsonReader_UnrecognizedToken
		{
			get
			{
				return TextRes.GetString("JsonReader_UnrecognizedToken");
			}
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x0001BA98 File Offset: 0x00019C98
		internal static string JsonReader_MissingColon(object p0)
		{
			return TextRes.GetString("JsonReader_MissingColon", new object[] { p0 });
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x0001BAAE File Offset: 0x00019CAE
		internal static string JsonReader_UnrecognizedEscapeSequence(object p0)
		{
			return TextRes.GetString("JsonReader_UnrecognizedEscapeSequence", new object[] { p0 });
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000B45 RID: 2885 RVA: 0x0001BAC4 File Offset: 0x00019CC4
		internal static string JsonReader_UnexpectedEndOfString
		{
			get
			{
				return TextRes.GetString("JsonReader_UnexpectedEndOfString");
			}
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x0001BAD0 File Offset: 0x00019CD0
		internal static string JsonReader_InvalidNumberFormat(object p0)
		{
			return TextRes.GetString("JsonReader_InvalidNumberFormat", new object[] { p0 });
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x0001BAE6 File Offset: 0x00019CE6
		internal static string JsonReader_MissingComma(object p0)
		{
			return TextRes.GetString("JsonReader_MissingComma", new object[] { p0 });
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x0001BAFC File Offset: 0x00019CFC
		internal static string JsonReader_InvalidPropertyNameOrUnexpectedComma(object p0)
		{
			return TextRes.GetString("JsonReader_InvalidPropertyNameOrUnexpectedComma", new object[] { p0 });
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x0001BB12 File Offset: 0x00019D12
		internal static string JsonReaderExtensions_UnexpectedNodeDetected(object p0, object p1)
		{
			return TextRes.GetString("JsonReaderExtensions_UnexpectedNodeDetected", new object[] { p0, p1 });
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x0001BB2C File Offset: 0x00019D2C
		internal static string JsonReaderExtensions_UnexpectedNodeDetectedWithPropertyName(object p0, object p1, object p2)
		{
			return TextRes.GetString("JsonReaderExtensions_UnexpectedNodeDetectedWithPropertyName", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x0001BB4A File Offset: 0x00019D4A
		internal static string JsonReaderExtensions_CannotReadPropertyValueAsString(object p0, object p1)
		{
			return TextRes.GetString("JsonReaderExtensions_CannotReadPropertyValueAsString", new object[] { p0, p1 });
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x0001BB64 File Offset: 0x00019D64
		internal static string JsonReaderExtensions_CannotReadValueAsString(object p0)
		{
			return TextRes.GetString("JsonReaderExtensions_CannotReadValueAsString", new object[] { p0 });
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x0001BB7A File Offset: 0x00019D7A
		internal static string JsonReaderExtensions_CannotReadValueAsDouble(object p0)
		{
			return TextRes.GetString("JsonReaderExtensions_CannotReadValueAsDouble", new object[] { p0 });
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x0001BB90 File Offset: 0x00019D90
		internal static string JsonReaderExtensions_UnexpectedInstanceAnnotationName(object p0)
		{
			return TextRes.GetString("JsonReaderExtensions_UnexpectedInstanceAnnotationName", new object[] { p0 });
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x0001BBA6 File Offset: 0x00019DA6
		internal static string ServiceProviderExtensions_NoServiceRegistered(object p0)
		{
			return TextRes.GetString("ServiceProviderExtensions_NoServiceRegistered", new object[] { p0 });
		}
	}
}
