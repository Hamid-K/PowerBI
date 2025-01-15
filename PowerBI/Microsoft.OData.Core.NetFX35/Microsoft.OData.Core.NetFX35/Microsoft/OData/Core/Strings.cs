using System;

namespace Microsoft.OData.Core
{
	// Token: 0x020002C7 RID: 711
	internal static class Strings
	{
		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06001862 RID: 6242 RVA: 0x000533CF File Offset: 0x000515CF
		internal static string ExceptionUtils_ArgumentStringEmpty
		{
			get
			{
				return TextRes.GetString("ExceptionUtils_ArgumentStringEmpty");
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06001863 RID: 6243 RVA: 0x000533DB File Offset: 0x000515DB
		internal static string ODataRequestMessage_AsyncNotAvailable
		{
			get
			{
				return TextRes.GetString("ODataRequestMessage_AsyncNotAvailable");
			}
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06001864 RID: 6244 RVA: 0x000533E7 File Offset: 0x000515E7
		internal static string ODataRequestMessage_StreamTaskIsNull
		{
			get
			{
				return TextRes.GetString("ODataRequestMessage_StreamTaskIsNull");
			}
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06001865 RID: 6245 RVA: 0x000533F3 File Offset: 0x000515F3
		internal static string ODataRequestMessage_MessageStreamIsNull
		{
			get
			{
				return TextRes.GetString("ODataRequestMessage_MessageStreamIsNull");
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06001866 RID: 6246 RVA: 0x000533FF File Offset: 0x000515FF
		internal static string ODataResponseMessage_AsyncNotAvailable
		{
			get
			{
				return TextRes.GetString("ODataResponseMessage_AsyncNotAvailable");
			}
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06001867 RID: 6247 RVA: 0x0005340B File Offset: 0x0005160B
		internal static string ODataResponseMessage_StreamTaskIsNull
		{
			get
			{
				return TextRes.GetString("ODataResponseMessage_StreamTaskIsNull");
			}
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06001868 RID: 6248 RVA: 0x00053417 File Offset: 0x00051617
		internal static string ODataResponseMessage_MessageStreamIsNull
		{
			get
			{
				return TextRes.GetString("ODataResponseMessage_MessageStreamIsNull");
			}
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06001869 RID: 6249 RVA: 0x00053423 File Offset: 0x00051623
		internal static string AsyncBufferedStream_WriterDisposedWithoutFlush
		{
			get
			{
				return TextRes.GetString("AsyncBufferedStream_WriterDisposedWithoutFlush");
			}
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x0600186A RID: 6250 RVA: 0x0005342F File Offset: 0x0005162F
		internal static string ODataFormat_AtomFormatObsoleted
		{
			get
			{
				return TextRes.GetString("ODataFormat_AtomFormatObsoleted");
			}
		}

		// Token: 0x0600186B RID: 6251 RVA: 0x0005343C File Offset: 0x0005163C
		internal static string ODataOutputContext_UnsupportedPayloadKindForFormat(object p0, object p1)
		{
			return TextRes.GetString("ODataOutputContext_UnsupportedPayloadKindForFormat", new object[] { p0, p1 });
		}

		// Token: 0x0600186C RID: 6252 RVA: 0x00053464 File Offset: 0x00051664
		internal static string ODataInputContext_UnsupportedPayloadKindForFormat(object p0, object p1)
		{
			return TextRes.GetString("ODataInputContext_UnsupportedPayloadKindForFormat", new object[] { p0, p1 });
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x0600186D RID: 6253 RVA: 0x0005348B File Offset: 0x0005168B
		internal static string ODataOutputContext_MetadataDocumentUriMissing
		{
			get
			{
				return TextRes.GetString("ODataOutputContext_MetadataDocumentUriMissing");
			}
		}

		// Token: 0x0600186E RID: 6254 RVA: 0x00053498 File Offset: 0x00051698
		internal static string ODataJsonLightSerializer_RelativeUriUsedWithoutMetadataDocumentUriOrMetadata(object p0)
		{
			return TextRes.GetString("ODataJsonLightSerializer_RelativeUriUsedWithoutMetadataDocumentUriOrMetadata", new object[] { p0 });
		}

		// Token: 0x0600186F RID: 6255 RVA: 0x000534BC File Offset: 0x000516BC
		internal static string ODataWriter_RelativeUriUsedWithoutBaseUriSpecified(object p0)
		{
			return TextRes.GetString("ODataWriter_RelativeUriUsedWithoutBaseUriSpecified", new object[] { p0 });
		}

		// Token: 0x06001870 RID: 6256 RVA: 0x000534E0 File Offset: 0x000516E0
		internal static string ODataWriter_StreamPropertiesMustBePropertiesOfODataEntry(object p0)
		{
			return TextRes.GetString("ODataWriter_StreamPropertiesMustBePropertiesOfODataEntry", new object[] { p0 });
		}

		// Token: 0x06001871 RID: 6257 RVA: 0x00053504 File Offset: 0x00051704
		internal static string ODataWriterCore_InvalidStateTransition(object p0, object p1)
		{
			return TextRes.GetString("ODataWriterCore_InvalidStateTransition", new object[] { p0, p1 });
		}

		// Token: 0x06001872 RID: 6258 RVA: 0x0005352C File Offset: 0x0005172C
		internal static string ODataWriterCore_InvalidTransitionFromStart(object p0, object p1)
		{
			return TextRes.GetString("ODataWriterCore_InvalidTransitionFromStart", new object[] { p0, p1 });
		}

		// Token: 0x06001873 RID: 6259 RVA: 0x00053554 File Offset: 0x00051754
		internal static string ODataWriterCore_InvalidTransitionFromEntry(object p0, object p1)
		{
			return TextRes.GetString("ODataWriterCore_InvalidTransitionFromEntry", new object[] { p0, p1 });
		}

		// Token: 0x06001874 RID: 6260 RVA: 0x0005357C File Offset: 0x0005177C
		internal static string ODataWriterCore_InvalidTransitionFromNullEntry(object p0, object p1)
		{
			return TextRes.GetString("ODataWriterCore_InvalidTransitionFromNullEntry", new object[] { p0, p1 });
		}

		// Token: 0x06001875 RID: 6261 RVA: 0x000535A4 File Offset: 0x000517A4
		internal static string ODataWriterCore_InvalidTransitionFromFeed(object p0, object p1)
		{
			return TextRes.GetString("ODataWriterCore_InvalidTransitionFromFeed", new object[] { p0, p1 });
		}

		// Token: 0x06001876 RID: 6262 RVA: 0x000535CC File Offset: 0x000517CC
		internal static string ODataWriterCore_InvalidTransitionFromExpandedLink(object p0, object p1)
		{
			return TextRes.GetString("ODataWriterCore_InvalidTransitionFromExpandedLink", new object[] { p0, p1 });
		}

		// Token: 0x06001877 RID: 6263 RVA: 0x000535F4 File Offset: 0x000517F4
		internal static string ODataWriterCore_InvalidTransitionFromCompleted(object p0, object p1)
		{
			return TextRes.GetString("ODataWriterCore_InvalidTransitionFromCompleted", new object[] { p0, p1 });
		}

		// Token: 0x06001878 RID: 6264 RVA: 0x0005361C File Offset: 0x0005181C
		internal static string ODataWriterCore_InvalidTransitionFromError(object p0, object p1)
		{
			return TextRes.GetString("ODataWriterCore_InvalidTransitionFromError", new object[] { p0, p1 });
		}

		// Token: 0x06001879 RID: 6265 RVA: 0x00053644 File Offset: 0x00051844
		internal static string ODataJsonLightDeltaWriter_InvalidTransitionFromExpandedNavigationProperty(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightDeltaWriter_InvalidTransitionFromExpandedNavigationProperty", new object[] { p0, p1 });
		}

		// Token: 0x0600187A RID: 6266 RVA: 0x0005366C File Offset: 0x0005186C
		internal static string ODataJsonLightDeltaWriter_InvalidTransitionToExpandedNavigationProperty(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightDeltaWriter_InvalidTransitionToExpandedNavigationProperty", new object[] { p0, p1 });
		}

		// Token: 0x0600187B RID: 6267 RVA: 0x00053694 File Offset: 0x00051894
		internal static string ODataJsonLightDeltaWriter_WriteStartExpandedFeedCalledInInvalidState(object p0)
		{
			return TextRes.GetString("ODataJsonLightDeltaWriter_WriteStartExpandedFeedCalledInInvalidState", new object[] { p0 });
		}

		// Token: 0x0600187C RID: 6268 RVA: 0x000536B8 File Offset: 0x000518B8
		internal static string ODataWriterCore_WriteEndCalledInInvalidState(object p0)
		{
			return TextRes.GetString("ODataWriterCore_WriteEndCalledInInvalidState", new object[] { p0 });
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x0600187D RID: 6269 RVA: 0x000536DB File Offset: 0x000518DB
		internal static string ODataWriterCore_OnlyTopLevelFeedsSupportCount
		{
			get
			{
				return TextRes.GetString("ODataWriterCore_OnlyTopLevelFeedsSupportCount");
			}
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x0600187E RID: 6270 RVA: 0x000536E7 File Offset: 0x000518E7
		internal static string ODataWriterCore_QueryCountInRequest
		{
			get
			{
				return TextRes.GetString("ODataWriterCore_QueryCountInRequest");
			}
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x0600187F RID: 6271 RVA: 0x000536F3 File Offset: 0x000518F3
		internal static string ODataWriterCore_CannotWriteTopLevelFeedWithEntryWriter
		{
			get
			{
				return TextRes.GetString("ODataWriterCore_CannotWriteTopLevelFeedWithEntryWriter");
			}
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06001880 RID: 6272 RVA: 0x000536FF File Offset: 0x000518FF
		internal static string ODataWriterCore_CannotWriteTopLevelEntryWithFeedWriter
		{
			get
			{
				return TextRes.GetString("ODataWriterCore_CannotWriteTopLevelEntryWithFeedWriter");
			}
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06001881 RID: 6273 RVA: 0x0005370B File Offset: 0x0005190B
		internal static string ODataWriterCore_SyncCallOnAsyncWriter
		{
			get
			{
				return TextRes.GetString("ODataWriterCore_SyncCallOnAsyncWriter");
			}
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06001882 RID: 6274 RVA: 0x00053717 File Offset: 0x00051917
		internal static string ODataWriterCore_AsyncCallOnSyncWriter
		{
			get
			{
				return TextRes.GetString("ODataWriterCore_AsyncCallOnSyncWriter");
			}
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06001883 RID: 6275 RVA: 0x00053723 File Offset: 0x00051923
		internal static string ODataWriterCore_EntityReferenceLinkWithoutNavigationLink
		{
			get
			{
				return TextRes.GetString("ODataWriterCore_EntityReferenceLinkWithoutNavigationLink");
			}
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06001884 RID: 6276 RVA: 0x0005372F File Offset: 0x0005192F
		internal static string ODataWriterCore_EntityReferenceLinkInResponse
		{
			get
			{
				return TextRes.GetString("ODataWriterCore_EntityReferenceLinkInResponse");
			}
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06001885 RID: 6277 RVA: 0x0005373B File Offset: 0x0005193B
		internal static string ODataWriterCore_DeferredLinkInRequest
		{
			get
			{
				return TextRes.GetString("ODataWriterCore_DeferredLinkInRequest");
			}
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06001886 RID: 6278 RVA: 0x00053747 File Offset: 0x00051947
		internal static string ODataWriterCore_MultipleItemsInNavigationLinkContent
		{
			get
			{
				return TextRes.GetString("ODataWriterCore_MultipleItemsInNavigationLinkContent");
			}
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06001887 RID: 6279 RVA: 0x00053753 File Offset: 0x00051953
		internal static string ODataWriterCore_DeltaLinkNotSupportedOnExpandedFeed
		{
			get
			{
				return TextRes.GetString("ODataWriterCore_DeltaLinkNotSupportedOnExpandedFeed");
			}
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06001888 RID: 6280 RVA: 0x0005375F File Offset: 0x0005195F
		internal static string ODataWriterCore_PathInODataUriMustBeSetWhenWritingContainedElement
		{
			get
			{
				return TextRes.GetString("ODataWriterCore_PathInODataUriMustBeSetWhenWritingContainedElement");
			}
		}

		// Token: 0x06001889 RID: 6281 RVA: 0x0005376C File Offset: 0x0005196C
		internal static string DuplicatePropertyNamesChecker_DuplicatePropertyNamesNotAllowed(object p0)
		{
			return TextRes.GetString("DuplicatePropertyNamesChecker_DuplicatePropertyNamesNotAllowed", new object[] { p0 });
		}

		// Token: 0x0600188A RID: 6282 RVA: 0x00053790 File Offset: 0x00051990
		internal static string DuplicatePropertyNamesChecker_MultipleLinksForSingleton(object p0)
		{
			return TextRes.GetString("DuplicatePropertyNamesChecker_MultipleLinksForSingleton", new object[] { p0 });
		}

		// Token: 0x0600188B RID: 6283 RVA: 0x000537B4 File Offset: 0x000519B4
		internal static string DuplicatePropertyNamesChecker_DuplicateAnnotationNotAllowed(object p0)
		{
			return TextRes.GetString("DuplicatePropertyNamesChecker_DuplicateAnnotationNotAllowed", new object[] { p0 });
		}

		// Token: 0x0600188C RID: 6284 RVA: 0x000537D8 File Offset: 0x000519D8
		internal static string DuplicatePropertyNamesChecker_DuplicateAnnotationForPropertyNotAllowed(object p0, object p1)
		{
			return TextRes.GetString("DuplicatePropertyNamesChecker_DuplicateAnnotationForPropertyNotAllowed", new object[] { p0, p1 });
		}

		// Token: 0x0600188D RID: 6285 RVA: 0x00053800 File Offset: 0x00051A00
		internal static string DuplicatePropertyNamesChecker_DuplicateAnnotationForInstanceAnnotationNotAllowed(object p0, object p1)
		{
			return TextRes.GetString("DuplicatePropertyNamesChecker_DuplicateAnnotationForInstanceAnnotationNotAllowed", new object[] { p0, p1 });
		}

		// Token: 0x0600188E RID: 6286 RVA: 0x00053828 File Offset: 0x00051A28
		internal static string DuplicatePropertyNamesChecker_PropertyAnnotationAfterTheProperty(object p0, object p1)
		{
			return TextRes.GetString("DuplicatePropertyNamesChecker_PropertyAnnotationAfterTheProperty", new object[] { p0, p1 });
		}

		// Token: 0x0600188F RID: 6287 RVA: 0x00053850 File Offset: 0x00051A50
		internal static string AtomValueUtils_CannotConvertValueToAtomPrimitive(object p0)
		{
			return TextRes.GetString("AtomValueUtils_CannotConvertValueToAtomPrimitive", new object[] { p0 });
		}

		// Token: 0x06001890 RID: 6288 RVA: 0x00053874 File Offset: 0x00051A74
		internal static string ODataJsonWriter_UnsupportedValueType(object p0)
		{
			return TextRes.GetString("ODataJsonWriter_UnsupportedValueType", new object[] { p0 });
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06001891 RID: 6289 RVA: 0x00053897 File Offset: 0x00051A97
		internal static string ODataException_GeneralError
		{
			get
			{
				return TextRes.GetString("ODataException_GeneralError");
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06001892 RID: 6290 RVA: 0x000538A3 File Offset: 0x00051AA3
		internal static string ODataErrorException_GeneralError
		{
			get
			{
				return TextRes.GetString("ODataErrorException_GeneralError");
			}
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06001893 RID: 6291 RVA: 0x000538AF File Offset: 0x00051AAF
		internal static string ODataUriParserException_GeneralError
		{
			get
			{
				return TextRes.GetString("ODataUriParserException_GeneralError");
			}
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06001894 RID: 6292 RVA: 0x000538BB File Offset: 0x00051ABB
		internal static string ODataAtomCollectionWriter_CollectionNameMustNotBeNull
		{
			get
			{
				return TextRes.GetString("ODataAtomCollectionWriter_CollectionNameMustNotBeNull");
			}
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06001895 RID: 6293 RVA: 0x000538C7 File Offset: 0x00051AC7
		internal static string ODataAtomWriterMetadataUtils_AuthorMetadataMustNotContainNull
		{
			get
			{
				return TextRes.GetString("ODataAtomWriterMetadataUtils_AuthorMetadataMustNotContainNull");
			}
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06001896 RID: 6294 RVA: 0x000538D3 File Offset: 0x00051AD3
		internal static string ODataAtomWriterMetadataUtils_CategoryMetadataMustNotContainNull
		{
			get
			{
				return TextRes.GetString("ODataAtomWriterMetadataUtils_CategoryMetadataMustNotContainNull");
			}
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06001897 RID: 6295 RVA: 0x000538DF File Offset: 0x00051ADF
		internal static string ODataAtomWriterMetadataUtils_ContributorMetadataMustNotContainNull
		{
			get
			{
				return TextRes.GetString("ODataAtomWriterMetadataUtils_ContributorMetadataMustNotContainNull");
			}
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06001898 RID: 6296 RVA: 0x000538EB File Offset: 0x00051AEB
		internal static string ODataAtomWriterMetadataUtils_LinkMetadataMustNotContainNull
		{
			get
			{
				return TextRes.GetString("ODataAtomWriterMetadataUtils_LinkMetadataMustNotContainNull");
			}
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06001899 RID: 6297 RVA: 0x000538F7 File Offset: 0x00051AF7
		internal static string ODataAtomWriterMetadataUtils_LinkMustSpecifyHref
		{
			get
			{
				return TextRes.GetString("ODataAtomWriterMetadataUtils_LinkMustSpecifyHref");
			}
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x0600189A RID: 6298 RVA: 0x00053903 File Offset: 0x00051B03
		internal static string ODataAtomWriterMetadataUtils_CategoryMustSpecifyTerm
		{
			get
			{
				return TextRes.GetString("ODataAtomWriterMetadataUtils_CategoryMustSpecifyTerm");
			}
		}

		// Token: 0x0600189B RID: 6299 RVA: 0x00053910 File Offset: 0x00051B10
		internal static string ODataAtomWriterMetadataUtils_LinkHrefsMustMatch(object p0, object p1)
		{
			return TextRes.GetString("ODataAtomWriterMetadataUtils_LinkHrefsMustMatch", new object[] { p0, p1 });
		}

		// Token: 0x0600189C RID: 6300 RVA: 0x00053938 File Offset: 0x00051B38
		internal static string ODataAtomWriterMetadataUtils_LinkTitlesMustMatch(object p0, object p1)
		{
			return TextRes.GetString("ODataAtomWriterMetadataUtils_LinkTitlesMustMatch", new object[] { p0, p1 });
		}

		// Token: 0x0600189D RID: 6301 RVA: 0x00053960 File Offset: 0x00051B60
		internal static string ODataAtomWriterMetadataUtils_LinkRelationsMustMatch(object p0, object p1)
		{
			return TextRes.GetString("ODataAtomWriterMetadataUtils_LinkRelationsMustMatch", new object[] { p0, p1 });
		}

		// Token: 0x0600189E RID: 6302 RVA: 0x00053988 File Offset: 0x00051B88
		internal static string ODataAtomWriterMetadataUtils_LinkMediaTypesMustMatch(object p0, object p1)
		{
			return TextRes.GetString("ODataAtomWriterMetadataUtils_LinkMediaTypesMustMatch", new object[] { p0, p1 });
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x0600189F RID: 6303 RVA: 0x000539AF File Offset: 0x00051BAF
		internal static string ODataAtomWriterMetadataUtils_CategoriesHrefWithOtherValues
		{
			get
			{
				return TextRes.GetString("ODataAtomWriterMetadataUtils_CategoriesHrefWithOtherValues");
			}
		}

		// Token: 0x060018A0 RID: 6304 RVA: 0x000539BC File Offset: 0x00051BBC
		internal static string ODataAtomWriterMetadataUtils_CategoryTermsMustMatch(object p0, object p1)
		{
			return TextRes.GetString("ODataAtomWriterMetadataUtils_CategoryTermsMustMatch", new object[] { p0, p1 });
		}

		// Token: 0x060018A1 RID: 6305 RVA: 0x000539E4 File Offset: 0x00051BE4
		internal static string ODataAtomWriterMetadataUtils_CategorySchemesMustMatch(object p0, object p1)
		{
			return TextRes.GetString("ODataAtomWriterMetadataUtils_CategorySchemesMustMatch", new object[] { p0, p1 });
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x060018A2 RID: 6306 RVA: 0x00053A0B File Offset: 0x00051C0B
		internal static string ODataMessageWriter_WriterAlreadyUsed
		{
			get
			{
				return TextRes.GetString("ODataMessageWriter_WriterAlreadyUsed");
			}
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x060018A3 RID: 6307 RVA: 0x00053A17 File Offset: 0x00051C17
		internal static string ODataMessageWriter_EntityReferenceLinksInRequestNotAllowed
		{
			get
			{
				return TextRes.GetString("ODataMessageWriter_EntityReferenceLinksInRequestNotAllowed");
			}
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x060018A4 RID: 6308 RVA: 0x00053A23 File Offset: 0x00051C23
		internal static string ODataMessageWriter_ErrorPayloadInRequest
		{
			get
			{
				return TextRes.GetString("ODataMessageWriter_ErrorPayloadInRequest");
			}
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x060018A5 RID: 6309 RVA: 0x00053A2F File Offset: 0x00051C2F
		internal static string ODataMessageWriter_ServiceDocumentInRequest
		{
			get
			{
				return TextRes.GetString("ODataMessageWriter_ServiceDocumentInRequest");
			}
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x060018A6 RID: 6310 RVA: 0x00053A3B File Offset: 0x00051C3B
		internal static string ODataMessageWriter_MetadataDocumentInRequest
		{
			get
			{
				return TextRes.GetString("ODataMessageWriter_MetadataDocumentInRequest");
			}
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x060018A7 RID: 6311 RVA: 0x00053A47 File Offset: 0x00051C47
		internal static string ODataMessageWriter_DeltaInRequest
		{
			get
			{
				return TextRes.GetString("ODataMessageWriter_DeltaInRequest");
			}
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x060018A8 RID: 6312 RVA: 0x00053A53 File Offset: 0x00051C53
		internal static string ODataMessageWriter_AsyncInRequest
		{
			get
			{
				return TextRes.GetString("ODataMessageWriter_AsyncInRequest");
			}
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x060018A9 RID: 6313 RVA: 0x00053A5F File Offset: 0x00051C5F
		internal static string ODataMessageWriter_CannotWriteNullInRawFormat
		{
			get
			{
				return TextRes.GetString("ODataMessageWriter_CannotWriteNullInRawFormat");
			}
		}

		// Token: 0x060018AA RID: 6314 RVA: 0x00053A6C File Offset: 0x00051C6C
		internal static string ODataMessageWriter_CannotSetHeadersWithInvalidPayloadKind(object p0)
		{
			return TextRes.GetString("ODataMessageWriter_CannotSetHeadersWithInvalidPayloadKind", new object[] { p0 });
		}

		// Token: 0x060018AB RID: 6315 RVA: 0x00053A90 File Offset: 0x00051C90
		internal static string ODataMessageWriter_IncompatiblePayloadKinds(object p0, object p1)
		{
			return TextRes.GetString("ODataMessageWriter_IncompatiblePayloadKinds", new object[] { p0, p1 });
		}

		// Token: 0x060018AC RID: 6316 RVA: 0x00053AB8 File Offset: 0x00051CB8
		internal static string ODataMessageWriter_CannotWriteStreamPropertyAsTopLevelProperty(object p0)
		{
			return TextRes.GetString("ODataMessageWriter_CannotWriteStreamPropertyAsTopLevelProperty", new object[] { p0 });
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x060018AD RID: 6317 RVA: 0x00053ADB File Offset: 0x00051CDB
		internal static string ODataMessageWriter_WriteErrorAlreadyCalled
		{
			get
			{
				return TextRes.GetString("ODataMessageWriter_WriteErrorAlreadyCalled");
			}
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x060018AE RID: 6318 RVA: 0x00053AE7 File Offset: 0x00051CE7
		internal static string ODataMessageWriter_CannotWriteInStreamErrorForRawValues
		{
			get
			{
				return TextRes.GetString("ODataMessageWriter_CannotWriteInStreamErrorForRawValues");
			}
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x060018AF RID: 6319 RVA: 0x00053AF3 File Offset: 0x00051CF3
		internal static string ODataMessageWriter_CannotWriteMetadataWithoutModel
		{
			get
			{
				return TextRes.GetString("ODataMessageWriter_CannotWriteMetadataWithoutModel");
			}
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x060018B0 RID: 6320 RVA: 0x00053AFF File Offset: 0x00051CFF
		internal static string ODataMessageWriter_CannotSpecifyOperationWithoutModel
		{
			get
			{
				return TextRes.GetString("ODataMessageWriter_CannotSpecifyOperationWithoutModel");
			}
		}

		// Token: 0x060018B1 RID: 6321 RVA: 0x00053B0C File Offset: 0x00051D0C
		internal static string ODataMessageWriter_JsonPaddingOnInvalidContentType(object p0)
		{
			return TextRes.GetString("ODataMessageWriter_JsonPaddingOnInvalidContentType", new object[] { p0 });
		}

		// Token: 0x060018B2 RID: 6322 RVA: 0x00053B30 File Offset: 0x00051D30
		internal static string ODataMessageWriter_NonCollectionType(object p0)
		{
			return TextRes.GetString("ODataMessageWriter_NonCollectionType", new object[] { p0 });
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x060018B3 RID: 6323 RVA: 0x00053B53 File Offset: 0x00051D53
		internal static string ODataMessageWriterSettings_MessageWriterSettingsXmlCustomizationCallbacksMustBeSpecifiedBoth
		{
			get
			{
				return TextRes.GetString("ODataMessageWriterSettings_MessageWriterSettingsXmlCustomizationCallbacksMustBeSpecifiedBoth");
			}
		}

		// Token: 0x060018B4 RID: 6324 RVA: 0x00053B60 File Offset: 0x00051D60
		internal static string ODataCollectionWriterCore_InvalidTransitionFromStart(object p0, object p1)
		{
			return TextRes.GetString("ODataCollectionWriterCore_InvalidTransitionFromStart", new object[] { p0, p1 });
		}

		// Token: 0x060018B5 RID: 6325 RVA: 0x00053B88 File Offset: 0x00051D88
		internal static string ODataCollectionWriterCore_InvalidTransitionFromCollection(object p0, object p1)
		{
			return TextRes.GetString("ODataCollectionWriterCore_InvalidTransitionFromCollection", new object[] { p0, p1 });
		}

		// Token: 0x060018B6 RID: 6326 RVA: 0x00053BB0 File Offset: 0x00051DB0
		internal static string ODataCollectionWriterCore_InvalidTransitionFromItem(object p0, object p1)
		{
			return TextRes.GetString("ODataCollectionWriterCore_InvalidTransitionFromItem", new object[] { p0, p1 });
		}

		// Token: 0x060018B7 RID: 6327 RVA: 0x00053BD8 File Offset: 0x00051DD8
		internal static string ODataCollectionWriterCore_WriteEndCalledInInvalidState(object p0)
		{
			return TextRes.GetString("ODataCollectionWriterCore_WriteEndCalledInInvalidState", new object[] { p0 });
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x060018B8 RID: 6328 RVA: 0x00053BFB File Offset: 0x00051DFB
		internal static string ODataCollectionWriterCore_SyncCallOnAsyncWriter
		{
			get
			{
				return TextRes.GetString("ODataCollectionWriterCore_SyncCallOnAsyncWriter");
			}
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x060018B9 RID: 6329 RVA: 0x00053C07 File Offset: 0x00051E07
		internal static string ODataCollectionWriterCore_AsyncCallOnSyncWriter
		{
			get
			{
				return TextRes.GetString("ODataCollectionWriterCore_AsyncCallOnSyncWriter");
			}
		}

		// Token: 0x060018BA RID: 6330 RVA: 0x00053C14 File Offset: 0x00051E14
		internal static string ODataBatch_InvalidHttpMethodForChangeSetRequest(object p0)
		{
			return TextRes.GetString("ODataBatch_InvalidHttpMethodForChangeSetRequest", new object[] { p0 });
		}

		// Token: 0x060018BB RID: 6331 RVA: 0x00053C38 File Offset: 0x00051E38
		internal static string ODataBatchOperationHeaderDictionary_KeyNotFound(object p0)
		{
			return TextRes.GetString("ODataBatchOperationHeaderDictionary_KeyNotFound", new object[] { p0 });
		}

		// Token: 0x060018BC RID: 6332 RVA: 0x00053C5C File Offset: 0x00051E5C
		internal static string ODataBatchOperationHeaderDictionary_DuplicateCaseInsensitiveKeys(object p0)
		{
			return TextRes.GetString("ODataBatchOperationHeaderDictionary_DuplicateCaseInsensitiveKeys", new object[] { p0 });
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x060018BD RID: 6333 RVA: 0x00053C7F File Offset: 0x00051E7F
		internal static string ODataParameterWriter_InStreamErrorNotSupported
		{
			get
			{
				return TextRes.GetString("ODataParameterWriter_InStreamErrorNotSupported");
			}
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x060018BE RID: 6334 RVA: 0x00053C8B File Offset: 0x00051E8B
		internal static string ODataParameterWriter_CannotCreateParameterWriterOnResponseMessage
		{
			get
			{
				return TextRes.GetString("ODataParameterWriter_CannotCreateParameterWriterOnResponseMessage");
			}
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x060018BF RID: 6335 RVA: 0x00053C97 File Offset: 0x00051E97
		internal static string ODataParameterWriterCore_SyncCallOnAsyncWriter
		{
			get
			{
				return TextRes.GetString("ODataParameterWriterCore_SyncCallOnAsyncWriter");
			}
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x060018C0 RID: 6336 RVA: 0x00053CA3 File Offset: 0x00051EA3
		internal static string ODataParameterWriterCore_AsyncCallOnSyncWriter
		{
			get
			{
				return TextRes.GetString("ODataParameterWriterCore_AsyncCallOnSyncWriter");
			}
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x060018C1 RID: 6337 RVA: 0x00053CAF File Offset: 0x00051EAF
		internal static string ODataParameterWriterCore_CannotWriteStart
		{
			get
			{
				return TextRes.GetString("ODataParameterWriterCore_CannotWriteStart");
			}
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x060018C2 RID: 6338 RVA: 0x00053CBB File Offset: 0x00051EBB
		internal static string ODataParameterWriterCore_CannotWriteParameter
		{
			get
			{
				return TextRes.GetString("ODataParameterWriterCore_CannotWriteParameter");
			}
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x060018C3 RID: 6339 RVA: 0x00053CC7 File Offset: 0x00051EC7
		internal static string ODataParameterWriterCore_CannotWriteEnd
		{
			get
			{
				return TextRes.GetString("ODataParameterWriterCore_CannotWriteEnd");
			}
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x060018C4 RID: 6340 RVA: 0x00053CD3 File Offset: 0x00051ED3
		internal static string ODataParameterWriterCore_CannotWriteInErrorOrCompletedState
		{
			get
			{
				return TextRes.GetString("ODataParameterWriterCore_CannotWriteInErrorOrCompletedState");
			}
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x00053CE0 File Offset: 0x00051EE0
		internal static string ODataParameterWriterCore_DuplicatedParameterNameNotAllowed(object p0)
		{
			return TextRes.GetString("ODataParameterWriterCore_DuplicatedParameterNameNotAllowed", new object[] { p0 });
		}

		// Token: 0x060018C6 RID: 6342 RVA: 0x00053D04 File Offset: 0x00051F04
		internal static string ODataParameterWriterCore_CannotWriteValueOnNonValueTypeKind(object p0, object p1)
		{
			return TextRes.GetString("ODataParameterWriterCore_CannotWriteValueOnNonValueTypeKind", new object[] { p0, p1 });
		}

		// Token: 0x060018C7 RID: 6343 RVA: 0x00053D2C File Offset: 0x00051F2C
		internal static string ODataParameterWriterCore_CannotWriteValueOnNonSupportedValueType(object p0, object p1)
		{
			return TextRes.GetString("ODataParameterWriterCore_CannotWriteValueOnNonSupportedValueType", new object[] { p0, p1 });
		}

		// Token: 0x060018C8 RID: 6344 RVA: 0x00053D54 File Offset: 0x00051F54
		internal static string ODataParameterWriterCore_CannotCreateCollectionWriterOnNonCollectionTypeKind(object p0, object p1)
		{
			return TextRes.GetString("ODataParameterWriterCore_CannotCreateCollectionWriterOnNonCollectionTypeKind", new object[] { p0, p1 });
		}

		// Token: 0x060018C9 RID: 6345 RVA: 0x00053D7C File Offset: 0x00051F7C
		internal static string ODataParameterWriterCore_ParameterNameNotFoundInOperation(object p0, object p1)
		{
			return TextRes.GetString("ODataParameterWriterCore_ParameterNameNotFoundInOperation", new object[] { p0, p1 });
		}

		// Token: 0x060018CA RID: 6346 RVA: 0x00053DA4 File Offset: 0x00051FA4
		internal static string ODataParameterWriterCore_MissingParameterInParameterPayload(object p0, object p1)
		{
			return TextRes.GetString("ODataParameterWriterCore_MissingParameterInParameterPayload", new object[] { p0, p1 });
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x060018CB RID: 6347 RVA: 0x00053DCB File Offset: 0x00051FCB
		internal static string ODataBatchWriter_FlushOrFlushAsyncCalledInStreamRequestedState
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_FlushOrFlushAsyncCalledInStreamRequestedState");
			}
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x060018CC RID: 6348 RVA: 0x00053DD7 File Offset: 0x00051FD7
		internal static string ODataBatchWriter_CannotCompleteBatchWithActiveChangeSet
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_CannotCompleteBatchWithActiveChangeSet");
			}
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x060018CD RID: 6349 RVA: 0x00053DE3 File Offset: 0x00051FE3
		internal static string ODataBatchWriter_CannotStartChangeSetWithActiveChangeSet
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_CannotStartChangeSetWithActiveChangeSet");
			}
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x060018CE RID: 6350 RVA: 0x00053DEF File Offset: 0x00051FEF
		internal static string ODataBatchWriter_CannotCompleteChangeSetWithoutActiveChangeSet
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_CannotCompleteChangeSetWithoutActiveChangeSet");
			}
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x060018CF RID: 6351 RVA: 0x00053DFB File Offset: 0x00051FFB
		internal static string ODataBatchWriter_InvalidTransitionFromStart
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_InvalidTransitionFromStart");
			}
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x060018D0 RID: 6352 RVA: 0x00053E07 File Offset: 0x00052007
		internal static string ODataBatchWriter_InvalidTransitionFromBatchStarted
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_InvalidTransitionFromBatchStarted");
			}
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x060018D1 RID: 6353 RVA: 0x00053E13 File Offset: 0x00052013
		internal static string ODataBatchWriter_InvalidTransitionFromChangeSetStarted
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_InvalidTransitionFromChangeSetStarted");
			}
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x060018D2 RID: 6354 RVA: 0x00053E1F File Offset: 0x0005201F
		internal static string ODataBatchWriter_InvalidTransitionFromOperationCreated
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_InvalidTransitionFromOperationCreated");
			}
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x060018D3 RID: 6355 RVA: 0x00053E2B File Offset: 0x0005202B
		internal static string ODataBatchWriter_InvalidTransitionFromOperationContentStreamRequested
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_InvalidTransitionFromOperationContentStreamRequested");
			}
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x060018D4 RID: 6356 RVA: 0x00053E37 File Offset: 0x00052037
		internal static string ODataBatchWriter_InvalidTransitionFromOperationContentStreamDisposed
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_InvalidTransitionFromOperationContentStreamDisposed");
			}
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x060018D5 RID: 6357 RVA: 0x00053E43 File Offset: 0x00052043
		internal static string ODataBatchWriter_InvalidTransitionFromChangeSetCompleted
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_InvalidTransitionFromChangeSetCompleted");
			}
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x060018D6 RID: 6358 RVA: 0x00053E4F File Offset: 0x0005204F
		internal static string ODataBatchWriter_InvalidTransitionFromBatchCompleted
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_InvalidTransitionFromBatchCompleted");
			}
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x060018D7 RID: 6359 RVA: 0x00053E5B File Offset: 0x0005205B
		internal static string ODataBatchWriter_CannotCreateRequestOperationWhenWritingResponse
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_CannotCreateRequestOperationWhenWritingResponse");
			}
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x060018D8 RID: 6360 RVA: 0x00053E67 File Offset: 0x00052067
		internal static string ODataBatchWriter_CannotCreateResponseOperationWhenWritingRequest
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_CannotCreateResponseOperationWhenWritingRequest");
			}
		}

		// Token: 0x060018D9 RID: 6361 RVA: 0x00053E74 File Offset: 0x00052074
		internal static string ODataBatchWriter_MaxBatchSizeExceeded(object p0)
		{
			return TextRes.GetString("ODataBatchWriter_MaxBatchSizeExceeded", new object[] { p0 });
		}

		// Token: 0x060018DA RID: 6362 RVA: 0x00053E98 File Offset: 0x00052098
		internal static string ODataBatchWriter_MaxChangeSetSizeExceeded(object p0)
		{
			return TextRes.GetString("ODataBatchWriter_MaxChangeSetSizeExceeded", new object[] { p0 });
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x060018DB RID: 6363 RVA: 0x00053EBB File Offset: 0x000520BB
		internal static string ODataBatchWriter_SyncCallOnAsyncWriter
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_SyncCallOnAsyncWriter");
			}
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x060018DC RID: 6364 RVA: 0x00053EC7 File Offset: 0x000520C7
		internal static string ODataBatchWriter_AsyncCallOnSyncWriter
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_AsyncCallOnSyncWriter");
			}
		}

		// Token: 0x060018DD RID: 6365 RVA: 0x00053ED4 File Offset: 0x000520D4
		internal static string ODataBatchWriter_DuplicateContentIDsNotAllowed(object p0)
		{
			return TextRes.GetString("ODataBatchWriter_DuplicateContentIDsNotAllowed", new object[] { p0 });
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x060018DE RID: 6366 RVA: 0x00053EF7 File Offset: 0x000520F7
		internal static string ODataBatchWriter_CannotWriteInStreamErrorForBatch
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_CannotWriteInStreamErrorForBatch");
			}
		}

		// Token: 0x060018DF RID: 6367 RVA: 0x00053F04 File Offset: 0x00052104
		internal static string ODataBatchUtils_RelativeUriUsedWithoutBaseUriSpecified(object p0)
		{
			return TextRes.GetString("ODataBatchUtils_RelativeUriUsedWithoutBaseUriSpecified", new object[] { p0 });
		}

		// Token: 0x060018E0 RID: 6368 RVA: 0x00053F28 File Offset: 0x00052128
		internal static string ODataBatchUtils_RelativeUriStartingWithDollarUsedWithoutBaseUriSpecified(object p0)
		{
			return TextRes.GetString("ODataBatchUtils_RelativeUriStartingWithDollarUsedWithoutBaseUriSpecified", new object[] { p0 });
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x060018E1 RID: 6369 RVA: 0x00053F4B File Offset: 0x0005214B
		internal static string ODataBatchOperationMessage_VerifyNotCompleted
		{
			get
			{
				return TextRes.GetString("ODataBatchOperationMessage_VerifyNotCompleted");
			}
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x060018E2 RID: 6370 RVA: 0x00053F57 File Offset: 0x00052157
		internal static string ODataBatchOperationStream_Disposed
		{
			get
			{
				return TextRes.GetString("ODataBatchOperationStream_Disposed");
			}
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x060018E3 RID: 6371 RVA: 0x00053F63 File Offset: 0x00052163
		internal static string ODataBatchReader_CannotCreateRequestOperationWhenReadingResponse
		{
			get
			{
				return TextRes.GetString("ODataBatchReader_CannotCreateRequestOperationWhenReadingResponse");
			}
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x060018E4 RID: 6372 RVA: 0x00053F6F File Offset: 0x0005216F
		internal static string ODataBatchReader_CannotCreateResponseOperationWhenReadingRequest
		{
			get
			{
				return TextRes.GetString("ODataBatchReader_CannotCreateResponseOperationWhenReadingRequest");
			}
		}

		// Token: 0x060018E5 RID: 6373 RVA: 0x00053F7C File Offset: 0x0005217C
		internal static string ODataBatchReader_InvalidStateForCreateOperationRequestMessage(object p0)
		{
			return TextRes.GetString("ODataBatchReader_InvalidStateForCreateOperationRequestMessage", new object[] { p0 });
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x060018E6 RID: 6374 RVA: 0x00053F9F File Offset: 0x0005219F
		internal static string ODataBatchReader_OperationRequestMessageAlreadyCreated
		{
			get
			{
				return TextRes.GetString("ODataBatchReader_OperationRequestMessageAlreadyCreated");
			}
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x060018E7 RID: 6375 RVA: 0x00053FAB File Offset: 0x000521AB
		internal static string ODataBatchReader_OperationResponseMessageAlreadyCreated
		{
			get
			{
				return TextRes.GetString("ODataBatchReader_OperationResponseMessageAlreadyCreated");
			}
		}

		// Token: 0x060018E8 RID: 6376 RVA: 0x00053FB8 File Offset: 0x000521B8
		internal static string ODataBatchReader_InvalidStateForCreateOperationResponseMessage(object p0)
		{
			return TextRes.GetString("ODataBatchReader_InvalidStateForCreateOperationResponseMessage", new object[] { p0 });
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x060018E9 RID: 6377 RVA: 0x00053FDB File Offset: 0x000521DB
		internal static string ODataBatchReader_CannotUseReaderWhileOperationStreamActive
		{
			get
			{
				return TextRes.GetString("ODataBatchReader_CannotUseReaderWhileOperationStreamActive");
			}
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x060018EA RID: 6378 RVA: 0x00053FE7 File Offset: 0x000521E7
		internal static string ODataBatchReader_SyncCallOnAsyncReader
		{
			get
			{
				return TextRes.GetString("ODataBatchReader_SyncCallOnAsyncReader");
			}
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x060018EB RID: 6379 RVA: 0x00053FF3 File Offset: 0x000521F3
		internal static string ODataBatchReader_AsyncCallOnSyncReader
		{
			get
			{
				return TextRes.GetString("ODataBatchReader_AsyncCallOnSyncReader");
			}
		}

		// Token: 0x060018EC RID: 6380 RVA: 0x00054000 File Offset: 0x00052200
		internal static string ODataBatchReader_ReadOrReadAsyncCalledInInvalidState(object p0)
		{
			return TextRes.GetString("ODataBatchReader_ReadOrReadAsyncCalledInInvalidState", new object[] { p0 });
		}

		// Token: 0x060018ED RID: 6381 RVA: 0x00054024 File Offset: 0x00052224
		internal static string ODataBatchReader_MaxBatchSizeExceeded(object p0)
		{
			return TextRes.GetString("ODataBatchReader_MaxBatchSizeExceeded", new object[] { p0 });
		}

		// Token: 0x060018EE RID: 6382 RVA: 0x00054048 File Offset: 0x00052248
		internal static string ODataBatchReader_MaxChangeSetSizeExceeded(object p0)
		{
			return TextRes.GetString("ODataBatchReader_MaxChangeSetSizeExceeded", new object[] { p0 });
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x060018EF RID: 6383 RVA: 0x0005406B File Offset: 0x0005226B
		internal static string ODataBatchReader_NoMessageWasCreatedForOperation
		{
			get
			{
				return TextRes.GetString("ODataBatchReader_NoMessageWasCreatedForOperation");
			}
		}

		// Token: 0x060018F0 RID: 6384 RVA: 0x00054078 File Offset: 0x00052278
		internal static string ODataBatchReader_DuplicateContentIDsNotAllowed(object p0)
		{
			return TextRes.GetString("ODataBatchReader_DuplicateContentIDsNotAllowed", new object[] { p0 });
		}

		// Token: 0x060018F1 RID: 6385 RVA: 0x0005409C File Offset: 0x0005229C
		internal static string ODataBatchReaderStream_InvalidHeaderSpecified(object p0)
		{
			return TextRes.GetString("ODataBatchReaderStream_InvalidHeaderSpecified", new object[] { p0 });
		}

		// Token: 0x060018F2 RID: 6386 RVA: 0x000540C0 File Offset: 0x000522C0
		internal static string ODataBatchReaderStream_InvalidRequestLine(object p0)
		{
			return TextRes.GetString("ODataBatchReaderStream_InvalidRequestLine", new object[] { p0 });
		}

		// Token: 0x060018F3 RID: 6387 RVA: 0x000540E4 File Offset: 0x000522E4
		internal static string ODataBatchReaderStream_InvalidResponseLine(object p0)
		{
			return TextRes.GetString("ODataBatchReaderStream_InvalidResponseLine", new object[] { p0 });
		}

		// Token: 0x060018F4 RID: 6388 RVA: 0x00054108 File Offset: 0x00052308
		internal static string ODataBatchReaderStream_InvalidHttpVersionSpecified(object p0, object p1)
		{
			return TextRes.GetString("ODataBatchReaderStream_InvalidHttpVersionSpecified", new object[] { p0, p1 });
		}

		// Token: 0x060018F5 RID: 6389 RVA: 0x00054130 File Offset: 0x00052330
		internal static string ODataBatchReaderStream_NonIntegerHttpStatusCode(object p0)
		{
			return TextRes.GetString("ODataBatchReaderStream_NonIntegerHttpStatusCode", new object[] { p0 });
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x060018F6 RID: 6390 RVA: 0x00054153 File Offset: 0x00052353
		internal static string ODataBatchReaderStream_MissingContentTypeHeader
		{
			get
			{
				return TextRes.GetString("ODataBatchReaderStream_MissingContentTypeHeader");
			}
		}

		// Token: 0x060018F7 RID: 6391 RVA: 0x00054160 File Offset: 0x00052360
		internal static string ODataBatchReaderStream_MissingOrInvalidContentEncodingHeader(object p0, object p1)
		{
			return TextRes.GetString("ODataBatchReaderStream_MissingOrInvalidContentEncodingHeader", new object[] { p0, p1 });
		}

		// Token: 0x060018F8 RID: 6392 RVA: 0x00054188 File Offset: 0x00052388
		internal static string ODataBatchReaderStream_InvalidContentTypeSpecified(object p0, object p1, object p2, object p3)
		{
			return TextRes.GetString("ODataBatchReaderStream_InvalidContentTypeSpecified", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x060018F9 RID: 6393 RVA: 0x000541B8 File Offset: 0x000523B8
		internal static string ODataBatchReaderStream_InvalidContentLengthSpecified(object p0)
		{
			return TextRes.GetString("ODataBatchReaderStream_InvalidContentLengthSpecified", new object[] { p0 });
		}

		// Token: 0x060018FA RID: 6394 RVA: 0x000541DC File Offset: 0x000523DC
		internal static string ODataBatchReaderStream_DuplicateHeaderFound(object p0)
		{
			return TextRes.GetString("ODataBatchReaderStream_DuplicateHeaderFound", new object[] { p0 });
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x060018FB RID: 6395 RVA: 0x000541FF File Offset: 0x000523FF
		internal static string ODataBatchReaderStream_NestedChangesetsAreNotSupported
		{
			get
			{
				return TextRes.GetString("ODataBatchReaderStream_NestedChangesetsAreNotSupported");
			}
		}

		// Token: 0x060018FC RID: 6396 RVA: 0x0005420C File Offset: 0x0005240C
		internal static string ODataBatchReaderStream_MultiByteEncodingsNotSupported(object p0)
		{
			return TextRes.GetString("ODataBatchReaderStream_MultiByteEncodingsNotSupported", new object[] { p0 });
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x060018FD RID: 6397 RVA: 0x0005422F File Offset: 0x0005242F
		internal static string ODataBatchReaderStream_UnexpectedEndOfInput
		{
			get
			{
				return TextRes.GetString("ODataBatchReaderStream_UnexpectedEndOfInput");
			}
		}

		// Token: 0x060018FE RID: 6398 RVA: 0x0005423C File Offset: 0x0005243C
		internal static string ODataBatchReaderStreamBuffer_BoundaryLineSecurityLimitReached(object p0)
		{
			return TextRes.GetString("ODataBatchReaderStreamBuffer_BoundaryLineSecurityLimitReached", new object[] { p0 });
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x060018FF RID: 6399 RVA: 0x0005425F File Offset: 0x0005245F
		internal static string ODataAsyncWriter_CannotCreateResponseWhenNotWritingResponse
		{
			get
			{
				return TextRes.GetString("ODataAsyncWriter_CannotCreateResponseWhenNotWritingResponse");
			}
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06001900 RID: 6400 RVA: 0x0005426B File Offset: 0x0005246B
		internal static string ODataAsyncWriter_CannotCreateResponseMoreThanOnce
		{
			get
			{
				return TextRes.GetString("ODataAsyncWriter_CannotCreateResponseMoreThanOnce");
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06001901 RID: 6401 RVA: 0x00054277 File Offset: 0x00052477
		internal static string ODataAsyncWriter_SyncCallOnAsyncWriter
		{
			get
			{
				return TextRes.GetString("ODataAsyncWriter_SyncCallOnAsyncWriter");
			}
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06001902 RID: 6402 RVA: 0x00054283 File Offset: 0x00052483
		internal static string ODataAsyncWriter_AsyncCallOnSyncWriter
		{
			get
			{
				return TextRes.GetString("ODataAsyncWriter_AsyncCallOnSyncWriter");
			}
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06001903 RID: 6403 RVA: 0x0005428F File Offset: 0x0005248F
		internal static string ODataAsyncWriter_CannotWriteInStreamErrorForAsync
		{
			get
			{
				return TextRes.GetString("ODataAsyncWriter_CannotWriteInStreamErrorForAsync");
			}
		}

		// Token: 0x06001904 RID: 6404 RVA: 0x0005429C File Offset: 0x0005249C
		internal static string ODataAsyncReader_InvalidHeaderSpecified(object p0)
		{
			return TextRes.GetString("ODataAsyncReader_InvalidHeaderSpecified", new object[] { p0 });
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06001905 RID: 6405 RVA: 0x000542BF File Offset: 0x000524BF
		internal static string ODataAsyncReader_CannotCreateResponseWhenNotReadingResponse
		{
			get
			{
				return TextRes.GetString("ODataAsyncReader_CannotCreateResponseWhenNotReadingResponse");
			}
		}

		// Token: 0x06001906 RID: 6406 RVA: 0x000542CC File Offset: 0x000524CC
		internal static string ODataAsyncReader_InvalidResponseLine(object p0)
		{
			return TextRes.GetString("ODataAsyncReader_InvalidResponseLine", new object[] { p0 });
		}

		// Token: 0x06001907 RID: 6407 RVA: 0x000542F0 File Offset: 0x000524F0
		internal static string ODataAsyncReader_InvalidHttpVersionSpecified(object p0, object p1)
		{
			return TextRes.GetString("ODataAsyncReader_InvalidHttpVersionSpecified", new object[] { p0, p1 });
		}

		// Token: 0x06001908 RID: 6408 RVA: 0x00054318 File Offset: 0x00052518
		internal static string ODataAsyncReader_NonIntegerHttpStatusCode(object p0)
		{
			return TextRes.GetString("ODataAsyncReader_NonIntegerHttpStatusCode", new object[] { p0 });
		}

		// Token: 0x06001909 RID: 6409 RVA: 0x0005433C File Offset: 0x0005253C
		internal static string ODataAsyncReader_DuplicateHeaderFound(object p0)
		{
			return TextRes.GetString("ODataAsyncReader_DuplicateHeaderFound", new object[] { p0 });
		}

		// Token: 0x0600190A RID: 6410 RVA: 0x00054360 File Offset: 0x00052560
		internal static string ODataAsyncReader_MultiByteEncodingsNotSupported(object p0)
		{
			return TextRes.GetString("ODataAsyncReader_MultiByteEncodingsNotSupported", new object[] { p0 });
		}

		// Token: 0x0600190B RID: 6411 RVA: 0x00054384 File Offset: 0x00052584
		internal static string ODataAsyncReader_InvalidNewLineEncountered(object p0)
		{
			return TextRes.GetString("ODataAsyncReader_InvalidNewLineEncountered", new object[] { p0 });
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x0600190C RID: 6412 RVA: 0x000543A7 File Offset: 0x000525A7
		internal static string ODataAsyncReader_UnexpectedEndOfInput
		{
			get
			{
				return TextRes.GetString("ODataAsyncReader_UnexpectedEndOfInput");
			}
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x0600190D RID: 6413 RVA: 0x000543B3 File Offset: 0x000525B3
		internal static string ODataAsyncReader_SyncCallOnAsyncReader
		{
			get
			{
				return TextRes.GetString("ODataAsyncReader_SyncCallOnAsyncReader");
			}
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x0600190E RID: 6414 RVA: 0x000543BF File Offset: 0x000525BF
		internal static string ODataAsyncReader_AsyncCallOnSyncReader
		{
			get
			{
				return TextRes.GetString("ODataAsyncReader_AsyncCallOnSyncReader");
			}
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x000543CC File Offset: 0x000525CC
		internal static string HttpUtils_MediaTypeUnspecified(object p0)
		{
			return TextRes.GetString("HttpUtils_MediaTypeUnspecified", new object[] { p0 });
		}

		// Token: 0x06001910 RID: 6416 RVA: 0x000543F0 File Offset: 0x000525F0
		internal static string HttpUtils_MediaTypeRequiresSlash(object p0)
		{
			return TextRes.GetString("HttpUtils_MediaTypeRequiresSlash", new object[] { p0 });
		}

		// Token: 0x06001911 RID: 6417 RVA: 0x00054414 File Offset: 0x00052614
		internal static string HttpUtils_MediaTypeRequiresSubType(object p0)
		{
			return TextRes.GetString("HttpUtils_MediaTypeRequiresSubType", new object[] { p0 });
		}

		// Token: 0x06001912 RID: 6418 RVA: 0x00054438 File Offset: 0x00052638
		internal static string HttpUtils_MediaTypeMissingParameterValue(object p0)
		{
			return TextRes.GetString("HttpUtils_MediaTypeMissingParameterValue", new object[] { p0 });
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x06001913 RID: 6419 RVA: 0x0005445B File Offset: 0x0005265B
		internal static string HttpUtils_MediaTypeMissingParameterName
		{
			get
			{
				return TextRes.GetString("HttpUtils_MediaTypeMissingParameterName");
			}
		}

		// Token: 0x06001914 RID: 6420 RVA: 0x00054468 File Offset: 0x00052668
		internal static string HttpUtils_EscapeCharWithoutQuotes(object p0, object p1, object p2, object p3)
		{
			return TextRes.GetString("HttpUtils_EscapeCharWithoutQuotes", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06001915 RID: 6421 RVA: 0x00054498 File Offset: 0x00052698
		internal static string HttpUtils_EscapeCharAtEnd(object p0, object p1, object p2, object p3)
		{
			return TextRes.GetString("HttpUtils_EscapeCharAtEnd", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06001916 RID: 6422 RVA: 0x000544C8 File Offset: 0x000526C8
		internal static string HttpUtils_ClosingQuoteNotFound(object p0, object p1, object p2)
		{
			return TextRes.GetString("HttpUtils_ClosingQuoteNotFound", new object[] { p0, p1, p2 });
		}

		// Token: 0x06001917 RID: 6423 RVA: 0x000544F4 File Offset: 0x000526F4
		internal static string HttpUtils_InvalidCharacterInQuotedParameterValue(object p0, object p1, object p2, object p3)
		{
			return TextRes.GetString("HttpUtils_InvalidCharacterInQuotedParameterValue", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06001918 RID: 6424 RVA: 0x00054523 File Offset: 0x00052723
		internal static string HttpUtils_ContentTypeMissing
		{
			get
			{
				return TextRes.GetString("HttpUtils_ContentTypeMissing");
			}
		}

		// Token: 0x06001919 RID: 6425 RVA: 0x00054530 File Offset: 0x00052730
		internal static string HttpUtils_MediaTypeRequiresSemicolonBeforeParameter(object p0)
		{
			return TextRes.GetString("HttpUtils_MediaTypeRequiresSemicolonBeforeParameter", new object[] { p0 });
		}

		// Token: 0x0600191A RID: 6426 RVA: 0x00054554 File Offset: 0x00052754
		internal static string HttpUtils_InvalidQualityValueStartChar(object p0, object p1)
		{
			return TextRes.GetString("HttpUtils_InvalidQualityValueStartChar", new object[] { p0, p1 });
		}

		// Token: 0x0600191B RID: 6427 RVA: 0x0005457C File Offset: 0x0005277C
		internal static string HttpUtils_InvalidQualityValue(object p0, object p1)
		{
			return TextRes.GetString("HttpUtils_InvalidQualityValue", new object[] { p0, p1 });
		}

		// Token: 0x0600191C RID: 6428 RVA: 0x000545A4 File Offset: 0x000527A4
		internal static string HttpUtils_CannotConvertCharToInt(object p0)
		{
			return TextRes.GetString("HttpUtils_CannotConvertCharToInt", new object[] { p0 });
		}

		// Token: 0x0600191D RID: 6429 RVA: 0x000545C8 File Offset: 0x000527C8
		internal static string HttpUtils_MissingSeparatorBetweenCharsets(object p0)
		{
			return TextRes.GetString("HttpUtils_MissingSeparatorBetweenCharsets", new object[] { p0 });
		}

		// Token: 0x0600191E RID: 6430 RVA: 0x000545EC File Offset: 0x000527EC
		internal static string HttpUtils_InvalidSeparatorBetweenCharsets(object p0)
		{
			return TextRes.GetString("HttpUtils_InvalidSeparatorBetweenCharsets", new object[] { p0 });
		}

		// Token: 0x0600191F RID: 6431 RVA: 0x00054610 File Offset: 0x00052810
		internal static string HttpUtils_InvalidCharsetName(object p0)
		{
			return TextRes.GetString("HttpUtils_InvalidCharsetName", new object[] { p0 });
		}

		// Token: 0x06001920 RID: 6432 RVA: 0x00054634 File Offset: 0x00052834
		internal static string HttpUtils_UnexpectedEndOfQValue(object p0)
		{
			return TextRes.GetString("HttpUtils_UnexpectedEndOfQValue", new object[] { p0 });
		}

		// Token: 0x06001921 RID: 6433 RVA: 0x00054658 File Offset: 0x00052858
		internal static string HttpUtils_ExpectedLiteralNotFoundInString(object p0, object p1, object p2)
		{
			return TextRes.GetString("HttpUtils_ExpectedLiteralNotFoundInString", new object[] { p0, p1, p2 });
		}

		// Token: 0x06001922 RID: 6434 RVA: 0x00054684 File Offset: 0x00052884
		internal static string HttpUtils_InvalidHttpMethodString(object p0)
		{
			return TextRes.GetString("HttpUtils_InvalidHttpMethodString", new object[] { p0 });
		}

		// Token: 0x06001923 RID: 6435 RVA: 0x000546A8 File Offset: 0x000528A8
		internal static string HttpUtils_NoOrMoreThanOneContentTypeSpecified(object p0)
		{
			return TextRes.GetString("HttpUtils_NoOrMoreThanOneContentTypeSpecified", new object[] { p0 });
		}

		// Token: 0x06001924 RID: 6436 RVA: 0x000546CC File Offset: 0x000528CC
		internal static string HttpHeaderValueLexer_UnrecognizedSeparator(object p0, object p1, object p2, object p3)
		{
			return TextRes.GetString("HttpHeaderValueLexer_UnrecognizedSeparator", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06001925 RID: 6437 RVA: 0x000546FC File Offset: 0x000528FC
		internal static string HttpHeaderValueLexer_TokenExpectedButFoundQuotedString(object p0, object p1, object p2)
		{
			return TextRes.GetString("HttpHeaderValueLexer_TokenExpectedButFoundQuotedString", new object[] { p0, p1, p2 });
		}

		// Token: 0x06001926 RID: 6438 RVA: 0x00054728 File Offset: 0x00052928
		internal static string HttpHeaderValueLexer_FailedToReadTokenOrQuotedString(object p0, object p1, object p2)
		{
			return TextRes.GetString("HttpHeaderValueLexer_FailedToReadTokenOrQuotedString", new object[] { p0, p1, p2 });
		}

		// Token: 0x06001927 RID: 6439 RVA: 0x00054754 File Offset: 0x00052954
		internal static string HttpHeaderValueLexer_InvalidSeparatorAfterQuotedString(object p0, object p1, object p2, object p3)
		{
			return TextRes.GetString("HttpHeaderValueLexer_InvalidSeparatorAfterQuotedString", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06001928 RID: 6440 RVA: 0x00054784 File Offset: 0x00052984
		internal static string HttpHeaderValueLexer_EndOfFileAfterSeparator(object p0, object p1, object p2, object p3)
		{
			return TextRes.GetString("HttpHeaderValueLexer_EndOfFileAfterSeparator", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06001929 RID: 6441 RVA: 0x000547B4 File Offset: 0x000529B4
		internal static string MediaType_EncodingNotSupported(object p0)
		{
			return TextRes.GetString("MediaType_EncodingNotSupported", new object[] { p0 });
		}

		// Token: 0x0600192A RID: 6442 RVA: 0x000547D8 File Offset: 0x000529D8
		internal static string MediaTypeUtils_DidNotFindMatchingMediaType(object p0, object p1)
		{
			return TextRes.GetString("MediaTypeUtils_DidNotFindMatchingMediaType", new object[] { p0, p1 });
		}

		// Token: 0x0600192B RID: 6443 RVA: 0x00054800 File Offset: 0x00052A00
		internal static string MediaTypeUtils_CannotDetermineFormatFromContentType(object p0, object p1)
		{
			return TextRes.GetString("MediaTypeUtils_CannotDetermineFormatFromContentType", new object[] { p0, p1 });
		}

		// Token: 0x0600192C RID: 6444 RVA: 0x00054828 File Offset: 0x00052A28
		internal static string MediaTypeUtils_NoOrMoreThanOneContentTypeSpecified(object p0)
		{
			return TextRes.GetString("MediaTypeUtils_NoOrMoreThanOneContentTypeSpecified", new object[] { p0 });
		}

		// Token: 0x0600192D RID: 6445 RVA: 0x0005484C File Offset: 0x00052A4C
		internal static string MediaTypeUtils_BoundaryMustBeSpecifiedForBatchPayloads(object p0, object p1)
		{
			return TextRes.GetString("MediaTypeUtils_BoundaryMustBeSpecifiedForBatchPayloads", new object[] { p0, p1 });
		}

		// Token: 0x0600192E RID: 6446 RVA: 0x00054874 File Offset: 0x00052A74
		internal static string ExpressionLexer_ExpectedLiteralToken(object p0)
		{
			return TextRes.GetString("ExpressionLexer_ExpectedLiteralToken", new object[] { p0 });
		}

		// Token: 0x0600192F RID: 6447 RVA: 0x00054898 File Offset: 0x00052A98
		internal static string ODataUriUtils_ConvertToUriLiteralUnsupportedType(object p0)
		{
			return TextRes.GetString("ODataUriUtils_ConvertToUriLiteralUnsupportedType", new object[] { p0 });
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06001930 RID: 6448 RVA: 0x000548BB File Offset: 0x00052ABB
		internal static string ODataUriUtils_ConvertFromUriLiteralTypeRefWithoutModel
		{
			get
			{
				return TextRes.GetString("ODataUriUtils_ConvertFromUriLiteralTypeRefWithoutModel");
			}
		}

		// Token: 0x06001931 RID: 6449 RVA: 0x000548C8 File Offset: 0x00052AC8
		internal static string ODataUriUtils_ConvertFromUriLiteralTypeVerificationFailure(object p0, object p1)
		{
			return TextRes.GetString("ODataUriUtils_ConvertFromUriLiteralTypeVerificationFailure", new object[] { p0, p1 });
		}

		// Token: 0x06001932 RID: 6450 RVA: 0x000548F0 File Offset: 0x00052AF0
		internal static string ODataUriUtils_ConvertFromUriLiteralNullTypeVerificationFailure(object p0, object p1)
		{
			return TextRes.GetString("ODataUriUtils_ConvertFromUriLiteralNullTypeVerificationFailure", new object[] { p0, p1 });
		}

		// Token: 0x06001933 RID: 6451 RVA: 0x00054918 File Offset: 0x00052B18
		internal static string ODataUriUtils_ConvertFromUriLiteralNullOnNonNullableType(object p0)
		{
			return TextRes.GetString("ODataUriUtils_ConvertFromUriLiteralNullOnNonNullableType", new object[] { p0 });
		}

		// Token: 0x06001934 RID: 6452 RVA: 0x0005493C File Offset: 0x00052B3C
		internal static string ODataUriUtils_InvalidUriFormatForEntryIdOrFeedId(object p0)
		{
			return TextRes.GetString("ODataUriUtils_InvalidUriFormatForEntryIdOrFeedId", new object[] { p0 });
		}

		// Token: 0x06001935 RID: 6453 RVA: 0x00054960 File Offset: 0x00052B60
		internal static string ODataUtils_CannotConvertValueToRawString(object p0)
		{
			return TextRes.GetString("ODataUtils_CannotConvertValueToRawString", new object[] { p0 });
		}

		// Token: 0x06001936 RID: 6454 RVA: 0x00054984 File Offset: 0x00052B84
		internal static string ODataUtils_DidNotFindDefaultMediaType(object p0)
		{
			return TextRes.GetString("ODataUtils_DidNotFindDefaultMediaType", new object[] { p0 });
		}

		// Token: 0x06001937 RID: 6455 RVA: 0x000549A8 File Offset: 0x00052BA8
		internal static string ODataUtils_UnsupportedVersionHeader(object p0)
		{
			return TextRes.GetString("ODataUtils_UnsupportedVersionHeader", new object[] { p0 });
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06001938 RID: 6456 RVA: 0x000549CB File Offset: 0x00052BCB
		internal static string ODataUtils_UnsupportedVersionNumber
		{
			get
			{
				return TextRes.GetString("ODataUtils_UnsupportedVersionNumber");
			}
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06001939 RID: 6457 RVA: 0x000549D7 File Offset: 0x00052BD7
		internal static string ODataUtils_ModelDoesNotHaveContainer
		{
			get
			{
				return TextRes.GetString("ODataUtils_ModelDoesNotHaveContainer");
			}
		}

		// Token: 0x0600193A RID: 6458 RVA: 0x000549E4 File Offset: 0x00052BE4
		internal static string ReaderUtils_EnumerableModified(object p0)
		{
			return TextRes.GetString("ReaderUtils_EnumerableModified", new object[] { p0 });
		}

		// Token: 0x0600193B RID: 6459 RVA: 0x00054A08 File Offset: 0x00052C08
		internal static string ReaderValidationUtils_NullValueForNonNullableType(object p0)
		{
			return TextRes.GetString("ReaderValidationUtils_NullValueForNonNullableType", new object[] { p0 });
		}

		// Token: 0x0600193C RID: 6460 RVA: 0x00054A2C File Offset: 0x00052C2C
		internal static string ReaderValidationUtils_NullNamedValueForNonNullableType(object p0, object p1)
		{
			return TextRes.GetString("ReaderValidationUtils_NullNamedValueForNonNullableType", new object[] { p0, p1 });
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x0600193D RID: 6461 RVA: 0x00054A53 File Offset: 0x00052C53
		internal static string ReaderValidationUtils_EntityReferenceLinkMissingUri
		{
			get
			{
				return TextRes.GetString("ReaderValidationUtils_EntityReferenceLinkMissingUri");
			}
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x0600193E RID: 6462 RVA: 0x00054A5F File Offset: 0x00052C5F
		internal static string ReaderValidationUtils_ValueWithoutType
		{
			get
			{
				return TextRes.GetString("ReaderValidationUtils_ValueWithoutType");
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x0600193F RID: 6463 RVA: 0x00054A6B File Offset: 0x00052C6B
		internal static string ReaderValidationUtils_EntryWithoutType
		{
			get
			{
				return TextRes.GetString("ReaderValidationUtils_EntryWithoutType");
			}
		}

		// Token: 0x06001940 RID: 6464 RVA: 0x00054A78 File Offset: 0x00052C78
		internal static string ReaderValidationUtils_CannotConvertPrimitiveValue(object p0, object p1)
		{
			return TextRes.GetString("ReaderValidationUtils_CannotConvertPrimitiveValue", new object[] { p0, p1 });
		}

		// Token: 0x06001941 RID: 6465 RVA: 0x00054AA0 File Offset: 0x00052CA0
		internal static string ReaderValidationUtils_MessageReaderSettingsBaseUriMustBeNullOrAbsolute(object p0)
		{
			return TextRes.GetString("ReaderValidationUtils_MessageReaderSettingsBaseUriMustBeNullOrAbsolute", new object[] { p0 });
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06001942 RID: 6466 RVA: 0x00054AC3 File Offset: 0x00052CC3
		internal static string ReaderValidationUtils_UndeclaredPropertyBehaviorKindSpecifiedOnRequest
		{
			get
			{
				return TextRes.GetString("ReaderValidationUtils_UndeclaredPropertyBehaviorKindSpecifiedOnRequest");
			}
		}

		// Token: 0x06001943 RID: 6467 RVA: 0x00054AD0 File Offset: 0x00052CD0
		internal static string ReaderValidationUtils_ContextUriValidationInvalidExpectedEntitySet(object p0, object p1, object p2)
		{
			return TextRes.GetString("ReaderValidationUtils_ContextUriValidationInvalidExpectedEntitySet", new object[] { p0, p1, p2 });
		}

		// Token: 0x06001944 RID: 6468 RVA: 0x00054AFC File Offset: 0x00052CFC
		internal static string ReaderValidationUtils_ContextUriValidationInvalidExpectedEntityType(object p0, object p1, object p2)
		{
			return TextRes.GetString("ReaderValidationUtils_ContextUriValidationInvalidExpectedEntityType", new object[] { p0, p1, p2 });
		}

		// Token: 0x06001945 RID: 6469 RVA: 0x00054B28 File Offset: 0x00052D28
		internal static string ReaderValidationUtils_ContextUriValidationNonMatchingPropertyNames(object p0, object p1, object p2, object p3)
		{
			return TextRes.GetString("ReaderValidationUtils_ContextUriValidationNonMatchingPropertyNames", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06001946 RID: 6470 RVA: 0x00054B58 File Offset: 0x00052D58
		internal static string ReaderValidationUtils_ContextUriValidationNonMatchingDeclaringTypes(object p0, object p1, object p2, object p3)
		{
			return TextRes.GetString("ReaderValidationUtils_ContextUriValidationNonMatchingDeclaringTypes", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06001947 RID: 6471 RVA: 0x00054B88 File Offset: 0x00052D88
		internal static string ReaderValidationUtils_NonMatchingPropertyNames(object p0, object p1)
		{
			return TextRes.GetString("ReaderValidationUtils_NonMatchingPropertyNames", new object[] { p0, p1 });
		}

		// Token: 0x06001948 RID: 6472 RVA: 0x00054BB0 File Offset: 0x00052DB0
		internal static string ReaderValidationUtils_TypeInContextUriDoesNotMatchExpectedType(object p0, object p1, object p2)
		{
			return TextRes.GetString("ReaderValidationUtils_TypeInContextUriDoesNotMatchExpectedType", new object[] { p0, p1, p2 });
		}

		// Token: 0x06001949 RID: 6473 RVA: 0x00054BDC File Offset: 0x00052DDC
		internal static string ReaderValidationUtils_ContextUriDoesNotReferTypeAssignableToExpectedType(object p0, object p1, object p2)
		{
			return TextRes.GetString("ReaderValidationUtils_ContextUriDoesNotReferTypeAssignableToExpectedType", new object[] { p0, p1, p2 });
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x0600194A RID: 6474 RVA: 0x00054C07 File Offset: 0x00052E07
		internal static string ODataMessageReader_ReaderAlreadyUsed
		{
			get
			{
				return TextRes.GetString("ODataMessageReader_ReaderAlreadyUsed");
			}
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x0600194B RID: 6475 RVA: 0x00054C13 File Offset: 0x00052E13
		internal static string ODataMessageReader_ErrorPayloadInRequest
		{
			get
			{
				return TextRes.GetString("ODataMessageReader_ErrorPayloadInRequest");
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x0600194C RID: 6476 RVA: 0x00054C1F File Offset: 0x00052E1F
		internal static string ODataMessageReader_ServiceDocumentInRequest
		{
			get
			{
				return TextRes.GetString("ODataMessageReader_ServiceDocumentInRequest");
			}
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x0600194D RID: 6477 RVA: 0x00054C2B File Offset: 0x00052E2B
		internal static string ODataMessageReader_MetadataDocumentInRequest
		{
			get
			{
				return TextRes.GetString("ODataMessageReader_MetadataDocumentInRequest");
			}
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x0600194E RID: 6478 RVA: 0x00054C37 File Offset: 0x00052E37
		internal static string ODataMessageReader_DeltaInRequest
		{
			get
			{
				return TextRes.GetString("ODataMessageReader_DeltaInRequest");
			}
		}

		// Token: 0x0600194F RID: 6479 RVA: 0x00054C44 File Offset: 0x00052E44
		internal static string ODataMessageReader_ExpectedTypeSpecifiedWithoutMetadata(object p0)
		{
			return TextRes.GetString("ODataMessageReader_ExpectedTypeSpecifiedWithoutMetadata", new object[] { p0 });
		}

		// Token: 0x06001950 RID: 6480 RVA: 0x00054C68 File Offset: 0x00052E68
		internal static string ODataMessageReader_EntitySetSpecifiedWithoutMetadata(object p0)
		{
			return TextRes.GetString("ODataMessageReader_EntitySetSpecifiedWithoutMetadata", new object[] { p0 });
		}

		// Token: 0x06001951 RID: 6481 RVA: 0x00054C8C File Offset: 0x00052E8C
		internal static string ODataMessageReader_OperationImportSpecifiedWithoutMetadata(object p0)
		{
			return TextRes.GetString("ODataMessageReader_OperationImportSpecifiedWithoutMetadata", new object[] { p0 });
		}

		// Token: 0x06001952 RID: 6482 RVA: 0x00054CB0 File Offset: 0x00052EB0
		internal static string ODataMessageReader_OperationSpecifiedWithoutMetadata(object p0)
		{
			return TextRes.GetString("ODataMessageReader_OperationSpecifiedWithoutMetadata", new object[] { p0 });
		}

		// Token: 0x06001953 RID: 6483 RVA: 0x00054CD4 File Offset: 0x00052ED4
		internal static string ODataMessageReader_ExpectedCollectionTypeWrongKind(object p0)
		{
			return TextRes.GetString("ODataMessageReader_ExpectedCollectionTypeWrongKind", new object[] { p0 });
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06001954 RID: 6484 RVA: 0x00054CF7 File Offset: 0x00052EF7
		internal static string ODataMessageReader_ExpectedPropertyTypeEntityCollectionKind
		{
			get
			{
				return TextRes.GetString("ODataMessageReader_ExpectedPropertyTypeEntityCollectionKind");
			}
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06001955 RID: 6485 RVA: 0x00054D03 File Offset: 0x00052F03
		internal static string ODataMessageReader_ExpectedPropertyTypeEntityKind
		{
			get
			{
				return TextRes.GetString("ODataMessageReader_ExpectedPropertyTypeEntityKind");
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06001956 RID: 6486 RVA: 0x00054D0F File Offset: 0x00052F0F
		internal static string ODataMessageReader_ExpectedPropertyTypeStream
		{
			get
			{
				return TextRes.GetString("ODataMessageReader_ExpectedPropertyTypeStream");
			}
		}

		// Token: 0x06001957 RID: 6487 RVA: 0x00054D1C File Offset: 0x00052F1C
		internal static string ODataMessageReader_ExpectedValueTypeWrongKind(object p0)
		{
			return TextRes.GetString("ODataMessageReader_ExpectedValueTypeWrongKind", new object[] { p0 });
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06001958 RID: 6488 RVA: 0x00054D3F File Offset: 0x00052F3F
		internal static string ODataMessageReader_NoneOrEmptyContentTypeHeader
		{
			get
			{
				return TextRes.GetString("ODataMessageReader_NoneOrEmptyContentTypeHeader");
			}
		}

		// Token: 0x06001959 RID: 6489 RVA: 0x00054D4C File Offset: 0x00052F4C
		internal static string ODataMessageReader_WildcardInContentType(object p0)
		{
			return TextRes.GetString("ODataMessageReader_WildcardInContentType", new object[] { p0 });
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x0600195A RID: 6490 RVA: 0x00054D6F File Offset: 0x00052F6F
		internal static string ODataMessageReader_GetFormatCalledBeforeReadingStarted
		{
			get
			{
				return TextRes.GetString("ODataMessageReader_GetFormatCalledBeforeReadingStarted");
			}
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x0600195B RID: 6491 RVA: 0x00054D7B File Offset: 0x00052F7B
		internal static string ODataMessageReader_DetectPayloadKindMultipleTimes
		{
			get
			{
				return TextRes.GetString("ODataMessageReader_DetectPayloadKindMultipleTimes");
			}
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x0600195C RID: 6492 RVA: 0x00054D87 File Offset: 0x00052F87
		internal static string ODataMessageReader_PayloadKindDetectionRunning
		{
			get
			{
				return TextRes.GetString("ODataMessageReader_PayloadKindDetectionRunning");
			}
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x0600195D RID: 6493 RVA: 0x00054D93 File Offset: 0x00052F93
		internal static string ODataMessageReader_PayloadKindDetectionInServerMode
		{
			get
			{
				return TextRes.GetString("ODataMessageReader_PayloadKindDetectionInServerMode");
			}
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x0600195E RID: 6494 RVA: 0x00054D9F File Offset: 0x00052F9F
		internal static string ODataMessageReader_ParameterPayloadInResponse
		{
			get
			{
				return TextRes.GetString("ODataMessageReader_ParameterPayloadInResponse");
			}
		}

		// Token: 0x0600195F RID: 6495 RVA: 0x00054DAC File Offset: 0x00052FAC
		internal static string ODataMessageReader_SingletonNavigationPropertyForEntityReferenceLinks(object p0, object p1)
		{
			return TextRes.GetString("ODataMessageReader_SingletonNavigationPropertyForEntityReferenceLinks", new object[] { p0, p1 });
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x06001960 RID: 6496 RVA: 0x00054DD3 File Offset: 0x00052FD3
		internal static string ODataAsyncResponseMessage_MustNotModifyMessage
		{
			get
			{
				return TextRes.GetString("ODataAsyncResponseMessage_MustNotModifyMessage");
			}
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06001961 RID: 6497 RVA: 0x00054DDF File Offset: 0x00052FDF
		internal static string ODataMessage_MustNotModifyMessage
		{
			get
			{
				return TextRes.GetString("ODataMessage_MustNotModifyMessage");
			}
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06001962 RID: 6498 RVA: 0x00054DEB File Offset: 0x00052FEB
		internal static string ODataReaderCore_SyncCallOnAsyncReader
		{
			get
			{
				return TextRes.GetString("ODataReaderCore_SyncCallOnAsyncReader");
			}
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06001963 RID: 6499 RVA: 0x00054DF7 File Offset: 0x00052FF7
		internal static string ODataReaderCore_AsyncCallOnSyncReader
		{
			get
			{
				return TextRes.GetString("ODataReaderCore_AsyncCallOnSyncReader");
			}
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x00054E04 File Offset: 0x00053004
		internal static string ODataReaderCore_ReadOrReadAsyncCalledInInvalidState(object p0)
		{
			return TextRes.GetString("ODataReaderCore_ReadOrReadAsyncCalledInInvalidState", new object[] { p0 });
		}

		// Token: 0x06001965 RID: 6501 RVA: 0x00054E28 File Offset: 0x00053028
		internal static string ODataReaderCore_NoReadCallsAllowed(object p0)
		{
			return TextRes.GetString("ODataReaderCore_NoReadCallsAllowed", new object[] { p0 });
		}

		// Token: 0x06001966 RID: 6502 RVA: 0x00054E4C File Offset: 0x0005304C
		internal static string ODataJsonReader_CannotReadEntriesOfFeed(object p0)
		{
			return TextRes.GetString("ODataJsonReader_CannotReadEntriesOfFeed", new object[] { p0 });
		}

		// Token: 0x06001967 RID: 6503 RVA: 0x00054E70 File Offset: 0x00053070
		internal static string ODataJsonReaderUtils_CannotConvertInt32(object p0)
		{
			return TextRes.GetString("ODataJsonReaderUtils_CannotConvertInt32", new object[] { p0 });
		}

		// Token: 0x06001968 RID: 6504 RVA: 0x00054E94 File Offset: 0x00053094
		internal static string ODataJsonReaderUtils_CannotConvertDouble(object p0)
		{
			return TextRes.GetString("ODataJsonReaderUtils_CannotConvertDouble", new object[] { p0 });
		}

		// Token: 0x06001969 RID: 6505 RVA: 0x00054EB8 File Offset: 0x000530B8
		internal static string ODataJsonReaderUtils_CannotConvertBoolean(object p0)
		{
			return TextRes.GetString("ODataJsonReaderUtils_CannotConvertBoolean", new object[] { p0 });
		}

		// Token: 0x0600196A RID: 6506 RVA: 0x00054EDC File Offset: 0x000530DC
		internal static string ODataJsonReaderUtils_CannotConvertDecimal(object p0)
		{
			return TextRes.GetString("ODataJsonReaderUtils_CannotConvertDecimal", new object[] { p0 });
		}

		// Token: 0x0600196B RID: 6507 RVA: 0x00054F00 File Offset: 0x00053100
		internal static string ODataJsonReaderUtils_CannotConvertDateTime(object p0)
		{
			return TextRes.GetString("ODataJsonReaderUtils_CannotConvertDateTime", new object[] { p0 });
		}

		// Token: 0x0600196C RID: 6508 RVA: 0x00054F24 File Offset: 0x00053124
		internal static string ODataJsonReaderUtils_CannotConvertDateTimeOffset(object p0)
		{
			return TextRes.GetString("ODataJsonReaderUtils_CannotConvertDateTimeOffset", new object[] { p0 });
		}

		// Token: 0x0600196D RID: 6509 RVA: 0x00054F48 File Offset: 0x00053148
		internal static string ODataJsonReaderUtils_ConflictBetweenInputFormatAndParameter(object p0)
		{
			return TextRes.GetString("ODataJsonReaderUtils_ConflictBetweenInputFormatAndParameter", new object[] { p0 });
		}

		// Token: 0x0600196E RID: 6510 RVA: 0x00054F6C File Offset: 0x0005316C
		internal static string ODataJsonReaderUtils_MultipleErrorPropertiesWithSameName(object p0)
		{
			return TextRes.GetString("ODataJsonReaderUtils_MultipleErrorPropertiesWithSameName", new object[] { p0 });
		}

		// Token: 0x0600196F RID: 6511 RVA: 0x00054F90 File Offset: 0x00053190
		internal static string ODataJsonLightEntryAndFeedSerializer_ActionsAndFunctionsGroupMustSpecifyTarget(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedSerializer_ActionsAndFunctionsGroupMustSpecifyTarget", new object[] { p0 });
		}

		// Token: 0x06001970 RID: 6512 RVA: 0x00054FB4 File Offset: 0x000531B4
		internal static string ODataJsonLightEntryAndFeedSerializer_ActionsAndFunctionsGroupMustNotHaveDuplicateTarget(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedSerializer_ActionsAndFunctionsGroupMustNotHaveDuplicateTarget", new object[] { p0, p1 });
		}

		// Token: 0x06001971 RID: 6513 RVA: 0x00054FDC File Offset: 0x000531DC
		internal static string ODataJsonErrorDeserializer_TopLevelErrorWithInvalidProperty(object p0)
		{
			return TextRes.GetString("ODataJsonErrorDeserializer_TopLevelErrorWithInvalidProperty", new object[] { p0 });
		}

		// Token: 0x06001972 RID: 6514 RVA: 0x00055000 File Offset: 0x00053200
		internal static string ODataJsonErrorDeserializer_TopLevelErrorMessageValueWithInvalidProperty(object p0)
		{
			return TextRes.GetString("ODataJsonErrorDeserializer_TopLevelErrorMessageValueWithInvalidProperty", new object[] { p0 });
		}

		// Token: 0x06001973 RID: 6515 RVA: 0x00055024 File Offset: 0x00053224
		internal static string ODataCollectionReaderCore_ReadOrReadAsyncCalledInInvalidState(object p0)
		{
			return TextRes.GetString("ODataCollectionReaderCore_ReadOrReadAsyncCalledInInvalidState", new object[] { p0 });
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06001974 RID: 6516 RVA: 0x00055047 File Offset: 0x00053247
		internal static string ODataCollectionReaderCore_SyncCallOnAsyncReader
		{
			get
			{
				return TextRes.GetString("ODataCollectionReaderCore_SyncCallOnAsyncReader");
			}
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06001975 RID: 6517 RVA: 0x00055053 File Offset: 0x00053253
		internal static string ODataCollectionReaderCore_AsyncCallOnSyncReader
		{
			get
			{
				return TextRes.GetString("ODataCollectionReaderCore_AsyncCallOnSyncReader");
			}
		}

		// Token: 0x06001976 RID: 6518 RVA: 0x00055060 File Offset: 0x00053260
		internal static string ODataCollectionReaderCore_ExpectedItemTypeSetInInvalidState(object p0, object p1)
		{
			return TextRes.GetString("ODataCollectionReaderCore_ExpectedItemTypeSetInInvalidState", new object[] { p0, p1 });
		}

		// Token: 0x06001977 RID: 6519 RVA: 0x00055088 File Offset: 0x00053288
		internal static string ODataParameterReaderCore_ReadOrReadAsyncCalledInInvalidState(object p0)
		{
			return TextRes.GetString("ODataParameterReaderCore_ReadOrReadAsyncCalledInInvalidState", new object[] { p0 });
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06001978 RID: 6520 RVA: 0x000550AB File Offset: 0x000532AB
		internal static string ODataParameterReaderCore_SyncCallOnAsyncReader
		{
			get
			{
				return TextRes.GetString("ODataParameterReaderCore_SyncCallOnAsyncReader");
			}
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06001979 RID: 6521 RVA: 0x000550B7 File Offset: 0x000532B7
		internal static string ODataParameterReaderCore_AsyncCallOnSyncReader
		{
			get
			{
				return TextRes.GetString("ODataParameterReaderCore_AsyncCallOnSyncReader");
			}
		}

		// Token: 0x0600197A RID: 6522 RVA: 0x000550C4 File Offset: 0x000532C4
		internal static string ODataParameterReaderCore_SubReaderMustBeCreatedAndReadToCompletionBeforeTheNextReadOrReadAsyncCall(object p0, object p1)
		{
			return TextRes.GetString("ODataParameterReaderCore_SubReaderMustBeCreatedAndReadToCompletionBeforeTheNextReadOrReadAsyncCall", new object[] { p0, p1 });
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x000550EC File Offset: 0x000532EC
		internal static string ODataParameterReaderCore_SubReaderMustBeInCompletedStateBeforeTheNextReadOrReadAsyncCall(object p0, object p1)
		{
			return TextRes.GetString("ODataParameterReaderCore_SubReaderMustBeInCompletedStateBeforeTheNextReadOrReadAsyncCall", new object[] { p0, p1 });
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x00055114 File Offset: 0x00053314
		internal static string ODataParameterReaderCore_InvalidCreateReaderMethodCalledForState(object p0, object p1)
		{
			return TextRes.GetString("ODataParameterReaderCore_InvalidCreateReaderMethodCalledForState", new object[] { p0, p1 });
		}

		// Token: 0x0600197D RID: 6525 RVA: 0x0005513C File Offset: 0x0005333C
		internal static string ODataParameterReaderCore_CreateReaderAlreadyCalled(object p0, object p1)
		{
			return TextRes.GetString("ODataParameterReaderCore_CreateReaderAlreadyCalled", new object[] { p0, p1 });
		}

		// Token: 0x0600197E RID: 6526 RVA: 0x00055164 File Offset: 0x00053364
		internal static string ODataParameterReaderCore_ParameterNameNotInMetadata(object p0, object p1)
		{
			return TextRes.GetString("ODataParameterReaderCore_ParameterNameNotInMetadata", new object[] { p0, p1 });
		}

		// Token: 0x0600197F RID: 6527 RVA: 0x0005518C File Offset: 0x0005338C
		internal static string ODataParameterReaderCore_DuplicateParametersInPayload(object p0)
		{
			return TextRes.GetString("ODataParameterReaderCore_DuplicateParametersInPayload", new object[] { p0 });
		}

		// Token: 0x06001980 RID: 6528 RVA: 0x000551B0 File Offset: 0x000533B0
		internal static string ODataParameterReaderCore_ParametersMissingInPayload(object p0, object p1)
		{
			return TextRes.GetString("ODataParameterReaderCore_ParametersMissingInPayload", new object[] { p0, p1 });
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x000551D8 File Offset: 0x000533D8
		internal static string ValidationUtils_ActionsAndFunctionsMustSpecifyMetadata(object p0)
		{
			return TextRes.GetString("ValidationUtils_ActionsAndFunctionsMustSpecifyMetadata", new object[] { p0 });
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x000551FC File Offset: 0x000533FC
		internal static string ValidationUtils_ActionsAndFunctionsMustSpecifyTarget(object p0)
		{
			return TextRes.GetString("ValidationUtils_ActionsAndFunctionsMustSpecifyTarget", new object[] { p0 });
		}

		// Token: 0x06001983 RID: 6531 RVA: 0x00055220 File Offset: 0x00053420
		internal static string ValidationUtils_EnumerableContainsANullItem(object p0)
		{
			return TextRes.GetString("ValidationUtils_EnumerableContainsANullItem", new object[] { p0 });
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06001984 RID: 6532 RVA: 0x00055243 File Offset: 0x00053443
		internal static string ValidationUtils_AssociationLinkMustSpecifyName
		{
			get
			{
				return TextRes.GetString("ValidationUtils_AssociationLinkMustSpecifyName");
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06001985 RID: 6533 RVA: 0x0005524F File Offset: 0x0005344F
		internal static string ValidationUtils_AssociationLinkMustSpecifyUrl
		{
			get
			{
				return TextRes.GetString("ValidationUtils_AssociationLinkMustSpecifyUrl");
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06001986 RID: 6534 RVA: 0x0005525B File Offset: 0x0005345B
		internal static string ValidationUtils_TypeNameMustNotBeEmpty
		{
			get
			{
				return TextRes.GetString("ValidationUtils_TypeNameMustNotBeEmpty");
			}
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x00055268 File Offset: 0x00053468
		internal static string ValidationUtils_PropertyDoesNotExistOnType(object p0, object p1)
		{
			return TextRes.GetString("ValidationUtils_PropertyDoesNotExistOnType", new object[] { p0, p1 });
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06001988 RID: 6536 RVA: 0x0005528F File Offset: 0x0005348F
		internal static string ValidationUtils_ResourceMustSpecifyUrl
		{
			get
			{
				return TextRes.GetString("ValidationUtils_ResourceMustSpecifyUrl");
			}
		}

		// Token: 0x06001989 RID: 6537 RVA: 0x0005529C File Offset: 0x0005349C
		internal static string ValidationUtils_ResourceMustSpecifyName(object p0)
		{
			return TextRes.GetString("ValidationUtils_ResourceMustSpecifyName", new object[] { p0 });
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x0600198A RID: 6538 RVA: 0x000552BF File Offset: 0x000534BF
		internal static string ValidationUtils_ServiceDocumentElementUrlMustNotBeNull
		{
			get
			{
				return TextRes.GetString("ValidationUtils_ServiceDocumentElementUrlMustNotBeNull");
			}
		}

		// Token: 0x0600198B RID: 6539 RVA: 0x000552CC File Offset: 0x000534CC
		internal static string ValidationUtils_NonPrimitiveTypeForPrimitiveValue(object p0)
		{
			return TextRes.GetString("ValidationUtils_NonPrimitiveTypeForPrimitiveValue", new object[] { p0 });
		}

		// Token: 0x0600198C RID: 6540 RVA: 0x000552F0 File Offset: 0x000534F0
		internal static string ValidationUtils_UnsupportedPrimitiveType(object p0)
		{
			return TextRes.GetString("ValidationUtils_UnsupportedPrimitiveType", new object[] { p0 });
		}

		// Token: 0x0600198D RID: 6541 RVA: 0x00055314 File Offset: 0x00053514
		internal static string ValidationUtils_IncompatiblePrimitiveItemType(object p0, object p1, object p2, object p3)
		{
			return TextRes.GetString("ValidationUtils_IncompatiblePrimitiveItemType", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x0600198E RID: 6542 RVA: 0x00055343 File Offset: 0x00053543
		internal static string ValidationUtils_NonNullableCollectionElementsMustNotBeNull
		{
			get
			{
				return TextRes.GetString("ValidationUtils_NonNullableCollectionElementsMustNotBeNull");
			}
		}

		// Token: 0x0600198F RID: 6543 RVA: 0x00055350 File Offset: 0x00053550
		internal static string ValidationUtils_InvalidCollectionTypeName(object p0)
		{
			return TextRes.GetString("ValidationUtils_InvalidCollectionTypeName", new object[] { p0 });
		}

		// Token: 0x06001990 RID: 6544 RVA: 0x00055374 File Offset: 0x00053574
		internal static string ValidationUtils_UnrecognizedTypeName(object p0)
		{
			return TextRes.GetString("ValidationUtils_UnrecognizedTypeName", new object[] { p0 });
		}

		// Token: 0x06001991 RID: 6545 RVA: 0x00055398 File Offset: 0x00053598
		internal static string ValidationUtils_IncorrectTypeKind(object p0, object p1, object p2)
		{
			return TextRes.GetString("ValidationUtils_IncorrectTypeKind", new object[] { p0, p1, p2 });
		}

		// Token: 0x06001992 RID: 6546 RVA: 0x000553C4 File Offset: 0x000535C4
		internal static string ValidationUtils_IncorrectTypeKindNoTypeName(object p0, object p1)
		{
			return TextRes.GetString("ValidationUtils_IncorrectTypeKindNoTypeName", new object[] { p0, p1 });
		}

		// Token: 0x06001993 RID: 6547 RVA: 0x000553EC File Offset: 0x000535EC
		internal static string ValidationUtils_IncorrectValueTypeKind(object p0, object p1)
		{
			return TextRes.GetString("ValidationUtils_IncorrectValueTypeKind", new object[] { p0, p1 });
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x06001994 RID: 6548 RVA: 0x00055413 File Offset: 0x00053613
		internal static string ValidationUtils_LinkMustSpecifyName
		{
			get
			{
				return TextRes.GetString("ValidationUtils_LinkMustSpecifyName");
			}
		}

		// Token: 0x06001995 RID: 6549 RVA: 0x00055420 File Offset: 0x00053620
		internal static string ValidationUtils_MismatchPropertyKindForStreamProperty(object p0)
		{
			return TextRes.GetString("ValidationUtils_MismatchPropertyKindForStreamProperty", new object[] { p0 });
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06001996 RID: 6550 RVA: 0x00055443 File Offset: 0x00053643
		internal static string ValidationUtils_NestedCollectionsAreNotSupported
		{
			get
			{
				return TextRes.GetString("ValidationUtils_NestedCollectionsAreNotSupported");
			}
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06001997 RID: 6551 RVA: 0x0005544F File Offset: 0x0005364F
		internal static string ValidationUtils_StreamReferenceValuesNotSupportedInCollections
		{
			get
			{
				return TextRes.GetString("ValidationUtils_StreamReferenceValuesNotSupportedInCollections");
			}
		}

		// Token: 0x06001998 RID: 6552 RVA: 0x0005545C File Offset: 0x0005365C
		internal static string ValidationUtils_IncompatibleType(object p0, object p1)
		{
			return TextRes.GetString("ValidationUtils_IncompatibleType", new object[] { p0, p1 });
		}

		// Token: 0x06001999 RID: 6553 RVA: 0x00055484 File Offset: 0x00053684
		internal static string ValidationUtils_OpenCollectionProperty(object p0)
		{
			return TextRes.GetString("ValidationUtils_OpenCollectionProperty", new object[] { p0 });
		}

		// Token: 0x0600199A RID: 6554 RVA: 0x000554A8 File Offset: 0x000536A8
		internal static string ValidationUtils_OpenStreamProperty(object p0)
		{
			return TextRes.GetString("ValidationUtils_OpenStreamProperty", new object[] { p0 });
		}

		// Token: 0x0600199B RID: 6555 RVA: 0x000554CC File Offset: 0x000536CC
		internal static string ValidationUtils_InvalidCollectionTypeReference(object p0)
		{
			return TextRes.GetString("ValidationUtils_InvalidCollectionTypeReference", new object[] { p0 });
		}

		// Token: 0x0600199C RID: 6556 RVA: 0x000554F0 File Offset: 0x000536F0
		internal static string ValidationUtils_EntryWithMediaResourceAndNonMLEType(object p0)
		{
			return TextRes.GetString("ValidationUtils_EntryWithMediaResourceAndNonMLEType", new object[] { p0 });
		}

		// Token: 0x0600199D RID: 6557 RVA: 0x00055514 File Offset: 0x00053714
		internal static string ValidationUtils_EntryWithoutMediaResourceAndMLEType(object p0)
		{
			return TextRes.GetString("ValidationUtils_EntryWithoutMediaResourceAndMLEType", new object[] { p0 });
		}

		// Token: 0x0600199E RID: 6558 RVA: 0x00055538 File Offset: 0x00053738
		internal static string ValidationUtils_EntryTypeNotAssignableToExpectedType(object p0, object p1)
		{
			return TextRes.GetString("ValidationUtils_EntryTypeNotAssignableToExpectedType", new object[] { p0, p1 });
		}

		// Token: 0x0600199F RID: 6559 RVA: 0x00055560 File Offset: 0x00053760
		internal static string ValidationUtils_OpenNavigationProperty(object p0, object p1)
		{
			return TextRes.GetString("ValidationUtils_OpenNavigationProperty", new object[] { p0, p1 });
		}

		// Token: 0x060019A0 RID: 6560 RVA: 0x00055588 File Offset: 0x00053788
		internal static string ValidationUtils_NavigationPropertyExpected(object p0, object p1, object p2)
		{
			return TextRes.GetString("ValidationUtils_NavigationPropertyExpected", new object[] { p0, p1, p2 });
		}

		// Token: 0x060019A1 RID: 6561 RVA: 0x000555B4 File Offset: 0x000537B4
		internal static string ValidationUtils_InvalidBatchBoundaryDelimiterLength(object p0, object p1)
		{
			return TextRes.GetString("ValidationUtils_InvalidBatchBoundaryDelimiterLength", new object[] { p0, p1 });
		}

		// Token: 0x060019A2 RID: 6562 RVA: 0x000555DC File Offset: 0x000537DC
		internal static string ValidationUtils_RecursionDepthLimitReached(object p0)
		{
			return TextRes.GetString("ValidationUtils_RecursionDepthLimitReached", new object[] { p0 });
		}

		// Token: 0x060019A3 RID: 6563 RVA: 0x00055600 File Offset: 0x00053800
		internal static string ValidationUtils_MaxDepthOfNestedEntriesExceeded(object p0)
		{
			return TextRes.GetString("ValidationUtils_MaxDepthOfNestedEntriesExceeded", new object[] { p0 });
		}

		// Token: 0x060019A4 RID: 6564 RVA: 0x00055624 File Offset: 0x00053824
		internal static string ValidationUtils_NullCollectionItemForNonNullableType(object p0)
		{
			return TextRes.GetString("ValidationUtils_NullCollectionItemForNonNullableType", new object[] { p0 });
		}

		// Token: 0x060019A5 RID: 6565 RVA: 0x00055648 File Offset: 0x00053848
		internal static string ValidationUtils_PropertiesMustNotContainReservedChars(object p0, object p1)
		{
			return TextRes.GetString("ValidationUtils_PropertiesMustNotContainReservedChars", new object[] { p0, p1 });
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x060019A6 RID: 6566 RVA: 0x0005566F File Offset: 0x0005386F
		internal static string ValidationUtils_WorkspaceResourceMustNotContainNullItem
		{
			get
			{
				return TextRes.GetString("ValidationUtils_WorkspaceResourceMustNotContainNullItem");
			}
		}

		// Token: 0x060019A7 RID: 6567 RVA: 0x0005567C File Offset: 0x0005387C
		internal static string ValidationUtils_InvalidMetadataReferenceProperty(object p0)
		{
			return TextRes.GetString("ValidationUtils_InvalidMetadataReferenceProperty", new object[] { p0 });
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x060019A8 RID: 6568 RVA: 0x0005569F File Offset: 0x0005389F
		internal static string ODataAtomWriter_FeedsMustHaveNonEmptyId
		{
			get
			{
				return TextRes.GetString("ODataAtomWriter_FeedsMustHaveNonEmptyId");
			}
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x060019A9 RID: 6569 RVA: 0x000556AB File Offset: 0x000538AB
		internal static string WriterValidationUtils_PropertyMustNotBeNull
		{
			get
			{
				return TextRes.GetString("WriterValidationUtils_PropertyMustNotBeNull");
			}
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x060019AA RID: 6570 RVA: 0x000556B7 File Offset: 0x000538B7
		internal static string WriterValidationUtils_PropertiesMustHaveNonEmptyName
		{
			get
			{
				return TextRes.GetString("WriterValidationUtils_PropertiesMustHaveNonEmptyName");
			}
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x060019AB RID: 6571 RVA: 0x000556C3 File Offset: 0x000538C3
		internal static string WriterValidationUtils_MissingTypeNameWithMetadata
		{
			get
			{
				return TextRes.GetString("WriterValidationUtils_MissingTypeNameWithMetadata");
			}
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x060019AC RID: 6572 RVA: 0x000556CF File Offset: 0x000538CF
		internal static string WriterValidationUtils_NextPageLinkInRequest
		{
			get
			{
				return TextRes.GetString("WriterValidationUtils_NextPageLinkInRequest");
			}
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x060019AD RID: 6573 RVA: 0x000556DB File Offset: 0x000538DB
		internal static string WriterValidationUtils_DefaultStreamWithContentTypeWithoutReadLink
		{
			get
			{
				return TextRes.GetString("WriterValidationUtils_DefaultStreamWithContentTypeWithoutReadLink");
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x060019AE RID: 6574 RVA: 0x000556E7 File Offset: 0x000538E7
		internal static string WriterValidationUtils_DefaultStreamWithReadLinkWithoutContentType
		{
			get
			{
				return TextRes.GetString("WriterValidationUtils_DefaultStreamWithReadLinkWithoutContentType");
			}
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x060019AF RID: 6575 RVA: 0x000556F3 File Offset: 0x000538F3
		internal static string WriterValidationUtils_StreamReferenceValueMustHaveEditLinkOrReadLink
		{
			get
			{
				return TextRes.GetString("WriterValidationUtils_StreamReferenceValueMustHaveEditLinkOrReadLink");
			}
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x060019B0 RID: 6576 RVA: 0x000556FF File Offset: 0x000538FF
		internal static string WriterValidationUtils_StreamReferenceValueMustHaveEditLinkToHaveETag
		{
			get
			{
				return TextRes.GetString("WriterValidationUtils_StreamReferenceValueMustHaveEditLinkToHaveETag");
			}
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x060019B1 RID: 6577 RVA: 0x0005570B File Offset: 0x0005390B
		internal static string WriterValidationUtils_StreamReferenceValueEmptyContentType
		{
			get
			{
				return TextRes.GetString("WriterValidationUtils_StreamReferenceValueEmptyContentType");
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x060019B2 RID: 6578 RVA: 0x00055717 File Offset: 0x00053917
		internal static string WriterValidationUtils_EntriesMustHaveNonEmptyId
		{
			get
			{
				return TextRes.GetString("WriterValidationUtils_EntriesMustHaveNonEmptyId");
			}
		}

		// Token: 0x060019B3 RID: 6579 RVA: 0x00055724 File Offset: 0x00053924
		internal static string WriterValidationUtils_MessageWriterSettingsBaseUriMustBeNullOrAbsolute(object p0)
		{
			return TextRes.GetString("WriterValidationUtils_MessageWriterSettingsBaseUriMustBeNullOrAbsolute", new object[] { p0 });
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x060019B4 RID: 6580 RVA: 0x00055747 File Offset: 0x00053947
		internal static string WriterValidationUtils_EntityReferenceLinkUrlMustNotBeNull
		{
			get
			{
				return TextRes.GetString("WriterValidationUtils_EntityReferenceLinkUrlMustNotBeNull");
			}
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x060019B5 RID: 6581 RVA: 0x00055753 File Offset: 0x00053953
		internal static string WriterValidationUtils_EntityReferenceLinksLinkMustNotBeNull
		{
			get
			{
				return TextRes.GetString("WriterValidationUtils_EntityReferenceLinksLinkMustNotBeNull");
			}
		}

		// Token: 0x060019B6 RID: 6582 RVA: 0x00055760 File Offset: 0x00053960
		internal static string WriterValidationUtils_EntryTypeInExpandedLinkNotCompatibleWithNavigationPropertyType(object p0, object p1)
		{
			return TextRes.GetString("WriterValidationUtils_EntryTypeInExpandedLinkNotCompatibleWithNavigationPropertyType", new object[] { p0, p1 });
		}

		// Token: 0x060019B7 RID: 6583 RVA: 0x00055788 File Offset: 0x00053988
		internal static string WriterValidationUtils_ExpandedLinkIsCollectionTrueWithEntryContent(object p0)
		{
			return TextRes.GetString("WriterValidationUtils_ExpandedLinkIsCollectionTrueWithEntryContent", new object[] { p0 });
		}

		// Token: 0x060019B8 RID: 6584 RVA: 0x000557AC File Offset: 0x000539AC
		internal static string WriterValidationUtils_ExpandedLinkIsCollectionFalseWithFeedContent(object p0)
		{
			return TextRes.GetString("WriterValidationUtils_ExpandedLinkIsCollectionFalseWithFeedContent", new object[] { p0 });
		}

		// Token: 0x060019B9 RID: 6585 RVA: 0x000557D0 File Offset: 0x000539D0
		internal static string WriterValidationUtils_ExpandedLinkIsCollectionTrueWithEntryMetadata(object p0)
		{
			return TextRes.GetString("WriterValidationUtils_ExpandedLinkIsCollectionTrueWithEntryMetadata", new object[] { p0 });
		}

		// Token: 0x060019BA RID: 6586 RVA: 0x000557F4 File Offset: 0x000539F4
		internal static string WriterValidationUtils_ExpandedLinkIsCollectionFalseWithFeedMetadata(object p0)
		{
			return TextRes.GetString("WriterValidationUtils_ExpandedLinkIsCollectionFalseWithFeedMetadata", new object[] { p0 });
		}

		// Token: 0x060019BB RID: 6587 RVA: 0x00055818 File Offset: 0x00053A18
		internal static string WriterValidationUtils_ExpandedLinkWithFeedPayloadAndEntryMetadata(object p0)
		{
			return TextRes.GetString("WriterValidationUtils_ExpandedLinkWithFeedPayloadAndEntryMetadata", new object[] { p0 });
		}

		// Token: 0x060019BC RID: 6588 RVA: 0x0005583C File Offset: 0x00053A3C
		internal static string WriterValidationUtils_ExpandedLinkWithEntryPayloadAndFeedMetadata(object p0)
		{
			return TextRes.GetString("WriterValidationUtils_ExpandedLinkWithEntryPayloadAndFeedMetadata", new object[] { p0 });
		}

		// Token: 0x060019BD RID: 6589 RVA: 0x00055860 File Offset: 0x00053A60
		internal static string WriterValidationUtils_CollectionPropertiesMustNotHaveNullValue(object p0)
		{
			return TextRes.GetString("WriterValidationUtils_CollectionPropertiesMustNotHaveNullValue", new object[] { p0 });
		}

		// Token: 0x060019BE RID: 6590 RVA: 0x00055884 File Offset: 0x00053A84
		internal static string WriterValidationUtils_NonNullablePropertiesMustNotHaveNullValue(object p0, object p1)
		{
			return TextRes.GetString("WriterValidationUtils_NonNullablePropertiesMustNotHaveNullValue", new object[] { p0, p1 });
		}

		// Token: 0x060019BF RID: 6591 RVA: 0x000558AC File Offset: 0x00053AAC
		internal static string WriterValidationUtils_StreamPropertiesMustNotHaveNullValue(object p0)
		{
			return TextRes.GetString("WriterValidationUtils_StreamPropertiesMustNotHaveNullValue", new object[] { p0 });
		}

		// Token: 0x060019C0 RID: 6592 RVA: 0x000558D0 File Offset: 0x00053AD0
		internal static string WriterValidationUtils_OperationInRequest(object p0)
		{
			return TextRes.GetString("WriterValidationUtils_OperationInRequest", new object[] { p0 });
		}

		// Token: 0x060019C1 RID: 6593 RVA: 0x000558F4 File Offset: 0x00053AF4
		internal static string WriterValidationUtils_AssociationLinkInRequest(object p0)
		{
			return TextRes.GetString("WriterValidationUtils_AssociationLinkInRequest", new object[] { p0 });
		}

		// Token: 0x060019C2 RID: 6594 RVA: 0x00055918 File Offset: 0x00053B18
		internal static string WriterValidationUtils_StreamPropertyInRequest(object p0)
		{
			return TextRes.GetString("WriterValidationUtils_StreamPropertyInRequest", new object[] { p0 });
		}

		// Token: 0x060019C3 RID: 6595 RVA: 0x0005593C File Offset: 0x00053B3C
		internal static string WriterValidationUtils_MessageWriterSettingsServiceDocumentUriMustBeNullOrAbsolute(object p0)
		{
			return TextRes.GetString("WriterValidationUtils_MessageWriterSettingsServiceDocumentUriMustBeNullOrAbsolute", new object[] { p0 });
		}

		// Token: 0x060019C4 RID: 6596 RVA: 0x00055960 File Offset: 0x00053B60
		internal static string WriterValidationUtils_NavigationLinkMustSpecifyUrl(object p0)
		{
			return TextRes.GetString("WriterValidationUtils_NavigationLinkMustSpecifyUrl", new object[] { p0 });
		}

		// Token: 0x060019C5 RID: 6597 RVA: 0x00055984 File Offset: 0x00053B84
		internal static string WriterValidationUtils_NavigationLinkMustSpecifyIsCollection(object p0)
		{
			return TextRes.GetString("WriterValidationUtils_NavigationLinkMustSpecifyIsCollection", new object[] { p0 });
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x060019C6 RID: 6598 RVA: 0x000559A7 File Offset: 0x00053BA7
		internal static string WriterValidationUtils_MessageWriterSettingsJsonPaddingOnRequestMessage
		{
			get
			{
				return TextRes.GetString("WriterValidationUtils_MessageWriterSettingsJsonPaddingOnRequestMessage");
			}
		}

		// Token: 0x060019C7 RID: 6599 RVA: 0x000559B4 File Offset: 0x00053BB4
		internal static string XmlReaderExtension_InvalidNodeInStringValue(object p0)
		{
			return TextRes.GetString("XmlReaderExtension_InvalidNodeInStringValue", new object[] { p0 });
		}

		// Token: 0x060019C8 RID: 6600 RVA: 0x000559D8 File Offset: 0x00053BD8
		internal static string XmlReaderExtension_InvalidRootNode(object p0)
		{
			return TextRes.GetString("XmlReaderExtension_InvalidRootNode", new object[] { p0 });
		}

		// Token: 0x060019C9 RID: 6601 RVA: 0x000559FC File Offset: 0x00053BFC
		internal static string ODataMetadataInputContext_ErrorReadingMetadata(object p0)
		{
			return TextRes.GetString("ODataMetadataInputContext_ErrorReadingMetadata", new object[] { p0 });
		}

		// Token: 0x060019CA RID: 6602 RVA: 0x00055A20 File Offset: 0x00053C20
		internal static string ODataMetadataOutputContext_ErrorWritingMetadata(object p0)
		{
			return TextRes.GetString("ODataMetadataOutputContext_ErrorWritingMetadata", new object[] { p0 });
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x060019CB RID: 6603 RVA: 0x00055A43 File Offset: 0x00053C43
		internal static string ODataAtomReader_MediaLinkEntryMismatch
		{
			get
			{
				return TextRes.GetString("ODataAtomReader_MediaLinkEntryMismatch");
			}
		}

		// Token: 0x060019CC RID: 6604 RVA: 0x00055A50 File Offset: 0x00053C50
		internal static string ODataAtomReader_FeedNavigationLinkForResourceReferenceProperty(object p0)
		{
			return TextRes.GetString("ODataAtomReader_FeedNavigationLinkForResourceReferenceProperty", new object[] { p0 });
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x060019CD RID: 6605 RVA: 0x00055A73 File Offset: 0x00053C73
		internal static string ODataAtomReader_ExpandedFeedInEntryNavigationLink
		{
			get
			{
				return TextRes.GetString("ODataAtomReader_ExpandedFeedInEntryNavigationLink");
			}
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x060019CE RID: 6606 RVA: 0x00055A7F File Offset: 0x00053C7F
		internal static string ODataAtomReader_ExpandedEntryInFeedNavigationLink
		{
			get
			{
				return TextRes.GetString("ODataAtomReader_ExpandedEntryInFeedNavigationLink");
			}
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x060019CF RID: 6607 RVA: 0x00055A8B File Offset: 0x00053C8B
		internal static string ODataAtomReader_DeferredEntryInFeedNavigationLink
		{
			get
			{
				return TextRes.GetString("ODataAtomReader_DeferredEntryInFeedNavigationLink");
			}
		}

		// Token: 0x060019D0 RID: 6608 RVA: 0x00055A98 File Offset: 0x00053C98
		internal static string ODataAtomDeserializer_RelativeUriUsedWithoutBaseUriSpecified(object p0)
		{
			return TextRes.GetString("ODataAtomDeserializer_RelativeUriUsedWithoutBaseUriSpecified", new object[] { p0 });
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x060019D1 RID: 6609 RVA: 0x00055ABB File Offset: 0x00053CBB
		internal static string ODataAtomCollectionDeserializer_TypeOrNullAttributeNotAllowed
		{
			get
			{
				return TextRes.GetString("ODataAtomCollectionDeserializer_TypeOrNullAttributeNotAllowed");
			}
		}

		// Token: 0x060019D2 RID: 6610 RVA: 0x00055AC8 File Offset: 0x00053CC8
		internal static string ODataAtomCollectionDeserializer_WrongCollectionItemElementName(object p0, object p1)
		{
			return TextRes.GetString("ODataAtomCollectionDeserializer_WrongCollectionItemElementName", new object[] { p0, p1 });
		}

		// Token: 0x060019D3 RID: 6611 RVA: 0x00055AF0 File Offset: 0x00053CF0
		internal static string ODataAtomCollectionDeserializer_TopLevelCollectionElementWrongNamespace(object p0, object p1)
		{
			return TextRes.GetString("ODataAtomCollectionDeserializer_TopLevelCollectionElementWrongNamespace", new object[] { p0, p1 });
		}

		// Token: 0x060019D4 RID: 6612 RVA: 0x00055B18 File Offset: 0x00053D18
		internal static string ODataAtomPropertyAndValueDeserializer_TopLevelPropertyElementWrongNamespace(object p0, object p1)
		{
			return TextRes.GetString("ODataAtomPropertyAndValueDeserializer_TopLevelPropertyElementWrongNamespace", new object[] { p0, p1 });
		}

		// Token: 0x060019D5 RID: 6613 RVA: 0x00055B40 File Offset: 0x00053D40
		internal static string ODataAtomPropertyAndValueDeserializer_InvalidCollectionElement(object p0, object p1)
		{
			return TextRes.GetString("ODataAtomPropertyAndValueDeserializer_InvalidCollectionElement", new object[] { p0, p1 });
		}

		// Token: 0x060019D6 RID: 6614 RVA: 0x00055B68 File Offset: 0x00053D68
		internal static string ODataAtomPropertyAndValueDeserializer_NavigationPropertyInProperties(object p0, object p1)
		{
			return TextRes.GetString("ODataAtomPropertyAndValueDeserializer_NavigationPropertyInProperties", new object[] { p0, p1 });
		}

		// Token: 0x060019D7 RID: 6615 RVA: 0x00055B90 File Offset: 0x00053D90
		internal static string ODataAtomPropertyAndValueSerializer_NullValueNotAllowedForInstanceAnnotation(object p0, object p1)
		{
			return TextRes.GetString("ODataAtomPropertyAndValueSerializer_NullValueNotAllowedForInstanceAnnotation", new object[] { p0, p1 });
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x060019D8 RID: 6616 RVA: 0x00055BB7 File Offset: 0x00053DB7
		internal static string EdmLibraryExtensions_CollectionItemCanBeOnlyPrimitiveEnumComplex
		{
			get
			{
				return TextRes.GetString("EdmLibraryExtensions_CollectionItemCanBeOnlyPrimitiveEnumComplex");
			}
		}

		// Token: 0x060019D9 RID: 6617 RVA: 0x00055BC4 File Offset: 0x00053DC4
		internal static string EdmLibraryExtensions_OperationGroupReturningActionsAndFunctionsModelInvalid(object p0)
		{
			return TextRes.GetString("EdmLibraryExtensions_OperationGroupReturningActionsAndFunctionsModelInvalid", new object[] { p0 });
		}

		// Token: 0x060019DA RID: 6618 RVA: 0x00055BE8 File Offset: 0x00053DE8
		internal static string EdmLibraryExtensions_UnBoundOperationsFoundFromIEdmModelFindMethodIsInvalid(object p0)
		{
			return TextRes.GetString("EdmLibraryExtensions_UnBoundOperationsFoundFromIEdmModelFindMethodIsInvalid", new object[] { p0 });
		}

		// Token: 0x060019DB RID: 6619 RVA: 0x00055C0C File Offset: 0x00053E0C
		internal static string EdmLibraryExtensions_NoParameterBoundOperationsFoundFromIEdmModelFindMethodIsInvalid(object p0)
		{
			return TextRes.GetString("EdmLibraryExtensions_NoParameterBoundOperationsFoundFromIEdmModelFindMethodIsInvalid", new object[] { p0 });
		}

		// Token: 0x060019DC RID: 6620 RVA: 0x00055C30 File Offset: 0x00053E30
		internal static string EdmLibraryExtensions_ValueOverflowForUnderlyingType(object p0, object p1)
		{
			return TextRes.GetString("EdmLibraryExtensions_ValueOverflowForUnderlyingType", new object[] { p0, p1 });
		}

		// Token: 0x060019DD RID: 6621 RVA: 0x00055C58 File Offset: 0x00053E58
		internal static string ODataAtomEntryAndFeedDeserializer_ElementExpected(object p0)
		{
			return TextRes.GetString("ODataAtomEntryAndFeedDeserializer_ElementExpected", new object[] { p0 });
		}

		// Token: 0x060019DE RID: 6622 RVA: 0x00055C7C File Offset: 0x00053E7C
		internal static string ODataAtomEntryAndFeedDeserializer_EntryElementWrongName(object p0, object p1)
		{
			return TextRes.GetString("ODataAtomEntryAndFeedDeserializer_EntryElementWrongName", new object[] { p0, p1 });
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x060019DF RID: 6623 RVA: 0x00055CA3 File Offset: 0x00053EA3
		internal static string ODataAtomEntryAndFeedDeserializer_ContentWithSourceLinkIsNotEmpty
		{
			get
			{
				return TextRes.GetString("ODataAtomEntryAndFeedDeserializer_ContentWithSourceLinkIsNotEmpty");
			}
		}

		// Token: 0x060019E0 RID: 6624 RVA: 0x00055CB0 File Offset: 0x00053EB0
		internal static string ODataAtomEntryAndFeedDeserializer_ContentWithWrongType(object p0)
		{
			return TextRes.GetString("ODataAtomEntryAndFeedDeserializer_ContentWithWrongType", new object[] { p0 });
		}

		// Token: 0x060019E1 RID: 6625 RVA: 0x00055CD4 File Offset: 0x00053ED4
		internal static string ODataAtomEntryAndFeedDeserializer_ContentWithInvalidNode(object p0)
		{
			return TextRes.GetString("ODataAtomEntryAndFeedDeserializer_ContentWithInvalidNode", new object[] { p0 });
		}

		// Token: 0x060019E2 RID: 6626 RVA: 0x00055CF8 File Offset: 0x00053EF8
		internal static string ODataAtomEntryAndFeedDeserializer_FeedElementWrongName(object p0, object p1)
		{
			return TextRes.GetString("ODataAtomEntryAndFeedDeserializer_FeedElementWrongName", new object[] { p0, p1 });
		}

		// Token: 0x060019E3 RID: 6627 RVA: 0x00055D20 File Offset: 0x00053F20
		internal static string ODataAtomEntryAndFeedDeserializer_UnknownElementInInline(object p0)
		{
			return TextRes.GetString("ODataAtomEntryAndFeedDeserializer_UnknownElementInInline", new object[] { p0 });
		}

		// Token: 0x060019E4 RID: 6628 RVA: 0x00055D44 File Offset: 0x00053F44
		internal static string ODataAtomEntryAndFeedDeserializer_MultipleExpansionsInInline(object p0)
		{
			return TextRes.GetString("ODataAtomEntryAndFeedDeserializer_MultipleExpansionsInInline", new object[] { p0 });
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x060019E5 RID: 6629 RVA: 0x00055D67 File Offset: 0x00053F67
		internal static string ODataAtomEntryAndFeedDeserializer_MultipleInlineElementsInLink
		{
			get
			{
				return TextRes.GetString("ODataAtomEntryAndFeedDeserializer_MultipleInlineElementsInLink");
			}
		}

		// Token: 0x060019E6 RID: 6630 RVA: 0x00055D74 File Offset: 0x00053F74
		internal static string ODataAtomEntryAndFeedDeserializer_StreamPropertyWithMultipleEditLinks(object p0)
		{
			return TextRes.GetString("ODataAtomEntryAndFeedDeserializer_StreamPropertyWithMultipleEditLinks", new object[] { p0 });
		}

		// Token: 0x060019E7 RID: 6631 RVA: 0x00055D98 File Offset: 0x00053F98
		internal static string ODataAtomEntryAndFeedDeserializer_StreamPropertyWithMultipleReadLinks(object p0)
		{
			return TextRes.GetString("ODataAtomEntryAndFeedDeserializer_StreamPropertyWithMultipleReadLinks", new object[] { p0 });
		}

		// Token: 0x060019E8 RID: 6632 RVA: 0x00055DBC File Offset: 0x00053FBC
		internal static string ODataAtomEntryAndFeedDeserializer_StreamPropertyWithMultipleContentTypes(object p0)
		{
			return TextRes.GetString("ODataAtomEntryAndFeedDeserializer_StreamPropertyWithMultipleContentTypes", new object[] { p0 });
		}

		// Token: 0x060019E9 RID: 6633 RVA: 0x00055DE0 File Offset: 0x00053FE0
		internal static string ODataAtomEntryAndFeedDeserializer_StreamPropertyDuplicatePropertyName(object p0)
		{
			return TextRes.GetString("ODataAtomEntryAndFeedDeserializer_StreamPropertyDuplicatePropertyName", new object[] { p0 });
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x060019EA RID: 6634 RVA: 0x00055E03 File Offset: 0x00054003
		internal static string ODataAtomEntryAndFeedDeserializer_StreamPropertyWithEmptyName
		{
			get
			{
				return TextRes.GetString("ODataAtomEntryAndFeedDeserializer_StreamPropertyWithEmptyName");
			}
		}

		// Token: 0x060019EB RID: 6635 RVA: 0x00055E10 File Offset: 0x00054010
		internal static string ODataAtomEntryAndFeedDeserializer_OperationMissingMetadataAttribute(object p0)
		{
			return TextRes.GetString("ODataAtomEntryAndFeedDeserializer_OperationMissingMetadataAttribute", new object[] { p0 });
		}

		// Token: 0x060019EC RID: 6636 RVA: 0x00055E34 File Offset: 0x00054034
		internal static string ODataAtomEntryAndFeedDeserializer_OperationMissingTargetAttribute(object p0)
		{
			return TextRes.GetString("ODataAtomEntryAndFeedDeserializer_OperationMissingTargetAttribute", new object[] { p0 });
		}

		// Token: 0x060019ED RID: 6637 RVA: 0x00055E58 File Offset: 0x00054058
		internal static string ODataAtomEntryAndFeedDeserializer_MultipleLinksInEntry(object p0)
		{
			return TextRes.GetString("ODataAtomEntryAndFeedDeserializer_MultipleLinksInEntry", new object[] { p0 });
		}

		// Token: 0x060019EE RID: 6638 RVA: 0x00055E7C File Offset: 0x0005407C
		internal static string ODataAtomEntryAndFeedDeserializer_MultipleLinksInFeed(object p0)
		{
			return TextRes.GetString("ODataAtomEntryAndFeedDeserializer_MultipleLinksInFeed", new object[] { p0 });
		}

		// Token: 0x060019EF RID: 6639 RVA: 0x00055EA0 File Offset: 0x000540A0
		internal static string ODataAtomEntryAndFeedDeserializer_DuplicateElements(object p0, object p1)
		{
			return TextRes.GetString("ODataAtomEntryAndFeedDeserializer_DuplicateElements", new object[] { p0, p1 });
		}

		// Token: 0x060019F0 RID: 6640 RVA: 0x00055EC8 File Offset: 0x000540C8
		internal static string ODataAtomEntryAndFeedDeserializer_InvalidTypeAttributeOnAssociationLink(object p0)
		{
			return TextRes.GetString("ODataAtomEntryAndFeedDeserializer_InvalidTypeAttributeOnAssociationLink", new object[] { p0 });
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x060019F1 RID: 6641 RVA: 0x00055EEB File Offset: 0x000540EB
		internal static string ODataAtomEntryAndFeedDeserializer_EncounteredAnnotationInNestedFeed
		{
			get
			{
				return TextRes.GetString("ODataAtomEntryAndFeedDeserializer_EncounteredAnnotationInNestedFeed");
			}
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x060019F2 RID: 6642 RVA: 0x00055EF7 File Offset: 0x000540F7
		internal static string ODataAtomEntryAndFeedDeserializer_EncounteredDeltaLinkInNestedFeed
		{
			get
			{
				return TextRes.GetString("ODataAtomEntryAndFeedDeserializer_EncounteredDeltaLinkInNestedFeed");
			}
		}

		// Token: 0x060019F3 RID: 6643 RVA: 0x00055F04 File Offset: 0x00054104
		internal static string ODataAtomEntryAndFeedDeserializer_AnnotationWithNonDotTarget(object p0, object p1)
		{
			return TextRes.GetString("ODataAtomEntryAndFeedDeserializer_AnnotationWithNonDotTarget", new object[] { p0, p1 });
		}

		// Token: 0x060019F4 RID: 6644 RVA: 0x00055F2C File Offset: 0x0005412C
		internal static string ODataAtomServiceDocumentDeserializer_ServiceDocumentRootElementWrongNameOrNamespace(object p0, object p1)
		{
			return TextRes.GetString("ODataAtomServiceDocumentDeserializer_ServiceDocumentRootElementWrongNameOrNamespace", new object[] { p0, p1 });
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x060019F5 RID: 6645 RVA: 0x00055F53 File Offset: 0x00054153
		internal static string ODataAtomServiceDocumentDeserializer_MissingWorkspaceElement
		{
			get
			{
				return TextRes.GetString("ODataAtomServiceDocumentDeserializer_MissingWorkspaceElement");
			}
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x060019F6 RID: 6646 RVA: 0x00055F5F File Offset: 0x0005415F
		internal static string ODataAtomServiceDocumentDeserializer_MultipleWorkspaceElementsFound
		{
			get
			{
				return TextRes.GetString("ODataAtomServiceDocumentDeserializer_MultipleWorkspaceElementsFound");
			}
		}

		// Token: 0x060019F7 RID: 6647 RVA: 0x00055F6C File Offset: 0x0005416C
		internal static string ODataAtomServiceDocumentDeserializer_UnexpectedElementInServiceDocument(object p0)
		{
			return TextRes.GetString("ODataAtomServiceDocumentDeserializer_UnexpectedElementInServiceDocument", new object[] { p0 });
		}

		// Token: 0x060019F8 RID: 6648 RVA: 0x00055F90 File Offset: 0x00054190
		internal static string ODataAtomServiceDocumentDeserializer_UnexpectedElementInWorkspace(object p0)
		{
			return TextRes.GetString("ODataAtomServiceDocumentDeserializer_UnexpectedElementInWorkspace", new object[] { p0 });
		}

		// Token: 0x060019F9 RID: 6649 RVA: 0x00055FB4 File Offset: 0x000541B4
		internal static string ODataAtomServiceDocumentDeserializer_UnexpectedODataElementInWorkspace(object p0)
		{
			return TextRes.GetString("ODataAtomServiceDocumentDeserializer_UnexpectedODataElementInWorkspace", new object[] { p0 });
		}

		// Token: 0x060019FA RID: 6650 RVA: 0x00055FD8 File Offset: 0x000541D8
		internal static string ODataAtomServiceDocumentDeserializer_UnexpectedElementInResourceCollection(object p0)
		{
			return TextRes.GetString("ODataAtomServiceDocumentDeserializer_UnexpectedElementInResourceCollection", new object[] { p0 });
		}

		// Token: 0x060019FB RID: 6651 RVA: 0x00055FFC File Offset: 0x000541FC
		internal static string ODataAtomEntryMetadataDeserializer_InvalidTextConstructKind(object p0, object p1)
		{
			return TextRes.GetString("ODataAtomEntryMetadataDeserializer_InvalidTextConstructKind", new object[] { p0, p1 });
		}

		// Token: 0x060019FC RID: 6652 RVA: 0x00056024 File Offset: 0x00054224
		internal static string ODataAtomEntryMetadataDeserializer_InvalidLinkLengthValue(object p0)
		{
			return TextRes.GetString("ODataAtomEntryMetadataDeserializer_InvalidLinkLengthValue", new object[] { p0 });
		}

		// Token: 0x060019FD RID: 6653 RVA: 0x00056048 File Offset: 0x00054248
		internal static string ODataAtomMetadataDeserializer_MultipleSingletonMetadataElements(object p0, object p1)
		{
			return TextRes.GetString("ODataAtomMetadataDeserializer_MultipleSingletonMetadataElements", new object[] { p0, p1 });
		}

		// Token: 0x060019FE RID: 6654 RVA: 0x00056070 File Offset: 0x00054270
		internal static string ODataAtomErrorDeserializer_InvalidRootElement(object p0, object p1)
		{
			return TextRes.GetString("ODataAtomErrorDeserializer_InvalidRootElement", new object[] { p0, p1 });
		}

		// Token: 0x060019FF RID: 6655 RVA: 0x00056098 File Offset: 0x00054298
		internal static string ODataAtomErrorDeserializer_MultipleErrorElementsWithSameName(object p0)
		{
			return TextRes.GetString("ODataAtomErrorDeserializer_MultipleErrorElementsWithSameName", new object[] { p0 });
		}

		// Token: 0x06001A00 RID: 6656 RVA: 0x000560BC File Offset: 0x000542BC
		internal static string ODataAtomErrorDeserializer_MultipleInnerErrorElementsWithSameName(object p0)
		{
			return TextRes.GetString("ODataAtomErrorDeserializer_MultipleInnerErrorElementsWithSameName", new object[] { p0 });
		}

		// Token: 0x06001A01 RID: 6657 RVA: 0x000560E0 File Offset: 0x000542E0
		internal static string ODataAtomEntityReferenceLinkDeserializer_InvalidEntityReferenceLinkStartElement(object p0, object p1)
		{
			return TextRes.GetString("ODataAtomEntityReferenceLinkDeserializer_InvalidEntityReferenceLinkStartElement", new object[] { p0, p1 });
		}

		// Token: 0x06001A02 RID: 6658 RVA: 0x00056108 File Offset: 0x00054308
		internal static string ODataAtomEntityReferenceLinkDeserializer_InvalidEntityReferenceLinksStartElement(object p0, object p1)
		{
			return TextRes.GetString("ODataAtomEntityReferenceLinkDeserializer_InvalidEntityReferenceLinksStartElement", new object[] { p0, p1 });
		}

		// Token: 0x06001A03 RID: 6659 RVA: 0x00056130 File Offset: 0x00054330
		internal static string ODataAtomEntityReferenceLinkDeserializer_MultipleEntityReferenceLinksElementsWithSameName(object p0, object p1)
		{
			return TextRes.GetString("ODataAtomEntityReferenceLinkDeserializer_MultipleEntityReferenceLinksElementsWithSameName", new object[] { p0, p1 });
		}

		// Token: 0x06001A04 RID: 6660 RVA: 0x00056158 File Offset: 0x00054358
		internal static string ODataAtomServiceDocumentMetadataDeserializer_InvalidFixedAttributeValue(object p0)
		{
			return TextRes.GetString("ODataAtomServiceDocumentMetadataDeserializer_InvalidFixedAttributeValue", new object[] { p0 });
		}

		// Token: 0x06001A05 RID: 6661 RVA: 0x0005617C File Offset: 0x0005437C
		internal static string ODataAtomServiceDocumentMetadataDeserializer_MultipleTitleElementsFound(object p0)
		{
			return TextRes.GetString("ODataAtomServiceDocumentMetadataDeserializer_MultipleTitleElementsFound", new object[] { p0 });
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06001A06 RID: 6662 RVA: 0x0005619F File Offset: 0x0005439F
		internal static string ODataAtomServiceDocumentMetadataDeserializer_MultipleAcceptElementsFoundInCollection
		{
			get
			{
				return TextRes.GetString("ODataAtomServiceDocumentMetadataDeserializer_MultipleAcceptElementsFoundInCollection");
			}
		}

		// Token: 0x06001A07 RID: 6663 RVA: 0x000561AC File Offset: 0x000543AC
		internal static string ODataAtomServiceDocumentMetadataSerializer_ResourceCollectionNameAndTitleMismatch(object p0, object p1)
		{
			return TextRes.GetString("ODataAtomServiceDocumentMetadataSerializer_ResourceCollectionNameAndTitleMismatch", new object[] { p0, p1 });
		}

		// Token: 0x06001A08 RID: 6664 RVA: 0x000561D4 File Offset: 0x000543D4
		internal static string CollectionWithoutExpectedTypeValidator_InvalidItemTypeKind(object p0)
		{
			return TextRes.GetString("CollectionWithoutExpectedTypeValidator_InvalidItemTypeKind", new object[] { p0 });
		}

		// Token: 0x06001A09 RID: 6665 RVA: 0x000561F8 File Offset: 0x000543F8
		internal static string CollectionWithoutExpectedTypeValidator_IncompatibleItemTypeKind(object p0, object p1)
		{
			return TextRes.GetString("CollectionWithoutExpectedTypeValidator_IncompatibleItemTypeKind", new object[] { p0, p1 });
		}

		// Token: 0x06001A0A RID: 6666 RVA: 0x00056220 File Offset: 0x00054420
		internal static string CollectionWithoutExpectedTypeValidator_IncompatibleItemTypeName(object p0, object p1)
		{
			return TextRes.GetString("CollectionWithoutExpectedTypeValidator_IncompatibleItemTypeName", new object[] { p0, p1 });
		}

		// Token: 0x06001A0B RID: 6667 RVA: 0x00056248 File Offset: 0x00054448
		internal static string FeedWithoutExpectedTypeValidator_IncompatibleTypes(object p0, object p1)
		{
			return TextRes.GetString("FeedWithoutExpectedTypeValidator_IncompatibleTypes", new object[] { p0, p1 });
		}

		// Token: 0x06001A0C RID: 6668 RVA: 0x00056270 File Offset: 0x00054470
		internal static string MessageStreamWrappingStream_ByteLimitExceeded(object p0, object p1)
		{
			return TextRes.GetString("MessageStreamWrappingStream_ByteLimitExceeded", new object[] { p0, p1 });
		}

		// Token: 0x06001A0D RID: 6669 RVA: 0x00056298 File Offset: 0x00054498
		internal static string MetadataUtils_ResolveTypeName(object p0)
		{
			return TextRes.GetString("MetadataUtils_ResolveTypeName", new object[] { p0 });
		}

		// Token: 0x06001A0E RID: 6670 RVA: 0x000562BC File Offset: 0x000544BC
		internal static string MetadataUtils_CalculateBindableOperationsForType(object p0)
		{
			return TextRes.GetString("MetadataUtils_CalculateBindableOperationsForType", new object[] { p0 });
		}

		// Token: 0x06001A0F RID: 6671 RVA: 0x000562E0 File Offset: 0x000544E0
		internal static string EdmValueUtils_UnsupportedPrimitiveType(object p0)
		{
			return TextRes.GetString("EdmValueUtils_UnsupportedPrimitiveType", new object[] { p0 });
		}

		// Token: 0x06001A10 RID: 6672 RVA: 0x00056304 File Offset: 0x00054504
		internal static string EdmValueUtils_IncorrectPrimitiveTypeKind(object p0, object p1, object p2)
		{
			return TextRes.GetString("EdmValueUtils_IncorrectPrimitiveTypeKind", new object[] { p0, p1, p2 });
		}

		// Token: 0x06001A11 RID: 6673 RVA: 0x00056330 File Offset: 0x00054530
		internal static string EdmValueUtils_IncorrectPrimitiveTypeKindNoTypeName(object p0, object p1)
		{
			return TextRes.GetString("EdmValueUtils_IncorrectPrimitiveTypeKindNoTypeName", new object[] { p0, p1 });
		}

		// Token: 0x06001A12 RID: 6674 RVA: 0x00056358 File Offset: 0x00054558
		internal static string EdmValueUtils_CannotConvertTypeToClrValue(object p0)
		{
			return TextRes.GetString("EdmValueUtils_CannotConvertTypeToClrValue", new object[] { p0 });
		}

		// Token: 0x06001A13 RID: 6675 RVA: 0x0005637C File Offset: 0x0005457C
		internal static string ODataEdmStructuredValue_UndeclaredProperty(object p0, object p1)
		{
			return TextRes.GetString("ODataEdmStructuredValue_UndeclaredProperty", new object[] { p0, p1 });
		}

		// Token: 0x06001A14 RID: 6676 RVA: 0x000563A4 File Offset: 0x000545A4
		internal static string ODataMetadataBuilder_MissingEntitySetUri(object p0)
		{
			return TextRes.GetString("ODataMetadataBuilder_MissingEntitySetUri", new object[] { p0 });
		}

		// Token: 0x06001A15 RID: 6677 RVA: 0x000563C8 File Offset: 0x000545C8
		internal static string ODataMetadataBuilder_MissingSegmentForEntitySetUriSuffix(object p0, object p1)
		{
			return TextRes.GetString("ODataMetadataBuilder_MissingSegmentForEntitySetUriSuffix", new object[] { p0, p1 });
		}

		// Token: 0x06001A16 RID: 6678 RVA: 0x000563F0 File Offset: 0x000545F0
		internal static string ODataMetadataBuilder_MissingEntityInstanceUri(object p0)
		{
			return TextRes.GetString("ODataMetadataBuilder_MissingEntityInstanceUri", new object[] { p0 });
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06001A17 RID: 6679 RVA: 0x00056413 File Offset: 0x00054613
		internal static string ODataMetadataBuilder_MissingODataUri
		{
			get
			{
				return TextRes.GetString("ODataMetadataBuilder_MissingODataUri");
			}
		}

		// Token: 0x06001A18 RID: 6680 RVA: 0x00056420 File Offset: 0x00054620
		internal static string ODataJsonLightInputContext_EntityTypeMustBeCompatibleWithEntitySetBaseType(object p0, object p1, object p2)
		{
			return TextRes.GetString("ODataJsonLightInputContext_EntityTypeMustBeCompatibleWithEntitySetBaseType", new object[] { p0, p1, p2 });
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06001A19 RID: 6681 RVA: 0x0005644B File Offset: 0x0005464B
		internal static string ODataJsonLightInputContext_PayloadKindDetectionForRequest
		{
			get
			{
				return TextRes.GetString("ODataJsonLightInputContext_PayloadKindDetectionForRequest");
			}
		}

		// Token: 0x06001A1A RID: 6682 RVA: 0x00056458 File Offset: 0x00054658
		internal static string ODataJsonLightInputContext_OperationCannotBeNullForCreateParameterReader(object p0)
		{
			return TextRes.GetString("ODataJsonLightInputContext_OperationCannotBeNullForCreateParameterReader", new object[] { p0 });
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06001A1B RID: 6683 RVA: 0x0005647B File Offset: 0x0005467B
		internal static string ODataJsonLightInputContext_NoEntitySetForRequest
		{
			get
			{
				return TextRes.GetString("ODataJsonLightInputContext_NoEntitySetForRequest");
			}
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06001A1C RID: 6684 RVA: 0x00056487 File Offset: 0x00054687
		internal static string ODataJsonLightInputContext_ModelRequiredForReading
		{
			get
			{
				return TextRes.GetString("ODataJsonLightInputContext_ModelRequiredForReading");
			}
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06001A1D RID: 6685 RVA: 0x00056493 File Offset: 0x00054693
		internal static string ODataJsonLightInputContext_ItemTypeRequiredForCollectionReaderInRequests
		{
			get
			{
				return TextRes.GetString("ODataJsonLightInputContext_ItemTypeRequiredForCollectionReaderInRequests");
			}
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x06001A1E RID: 6686 RVA: 0x0005649F File Offset: 0x0005469F
		internal static string ODataJsonLightDeserializer_ContextLinkNotFoundAsFirstProperty
		{
			get
			{
				return TextRes.GetString("ODataJsonLightDeserializer_ContextLinkNotFoundAsFirstProperty");
			}
		}

		// Token: 0x06001A1F RID: 6687 RVA: 0x000564AC File Offset: 0x000546AC
		internal static string ODataJsonLightDeserializer_OnlyODataTypeAnnotationCanTargetInstanceAnnotation(object p0, object p1, object p2)
		{
			return TextRes.GetString("ODataJsonLightDeserializer_OnlyODataTypeAnnotationCanTargetInstanceAnnotation", new object[] { p0, p1, p2 });
		}

		// Token: 0x06001A20 RID: 6688 RVA: 0x000564D8 File Offset: 0x000546D8
		internal static string ODataJsonLightDeserializer_AnnotationTargetingInstanceAnnotationWithoutValue(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightDeserializer_AnnotationTargetingInstanceAnnotationWithoutValue", new object[] { p0, p1 });
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06001A21 RID: 6689 RVA: 0x000564FF File Offset: 0x000546FF
		internal static string ODataJsonLightWriter_EntityReferenceLinkAfterFeedInRequest
		{
			get
			{
				return TextRes.GetString("ODataJsonLightWriter_EntityReferenceLinkAfterFeedInRequest");
			}
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06001A22 RID: 6690 RVA: 0x0005650B File Offset: 0x0005470B
		internal static string ODataJsonLightWriter_InstanceAnnotationNotSupportedOnExpandedFeed
		{
			get
			{
				return TextRes.GetString("ODataJsonLightWriter_InstanceAnnotationNotSupportedOnExpandedFeed");
			}
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06001A23 RID: 6691 RVA: 0x00056517 File Offset: 0x00054717
		internal static string ODataJsonLightPropertyAndValueSerializer_NoExpectedTypeOrTypeNameSpecifiedForComplexValueRequest
		{
			get
			{
				return TextRes.GetString("ODataJsonLightPropertyAndValueSerializer_NoExpectedTypeOrTypeNameSpecifiedForComplexValueRequest");
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06001A24 RID: 6692 RVA: 0x00056523 File Offset: 0x00054723
		internal static string ODataJsonLightPropertyAndValueSerializer_NoExpectedTypeOrTypeNameSpecifiedForCollectionValueInRequest
		{
			get
			{
				return TextRes.GetString("ODataJsonLightPropertyAndValueSerializer_NoExpectedTypeOrTypeNameSpecifiedForCollectionValueInRequest");
			}
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06001A25 RID: 6693 RVA: 0x0005652F File Offset: 0x0005472F
		internal static string ODataFeedAndEntryTypeContext_MetadataOrSerializationInfoMissing
		{
			get
			{
				return TextRes.GetString("ODataFeedAndEntryTypeContext_MetadataOrSerializationInfoMissing");
			}
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06001A26 RID: 6694 RVA: 0x0005653B File Offset: 0x0005473B
		internal static string ODataFeedAndEntryTypeContext_ODataEntryTypeNameMissing
		{
			get
			{
				return TextRes.GetString("ODataFeedAndEntryTypeContext_ODataEntryTypeNameMissing");
			}
		}

		// Token: 0x06001A27 RID: 6695 RVA: 0x00056548 File Offset: 0x00054748
		internal static string ODataContextUriBuilder_ValidateDerivedType(object p0, object p1)
		{
			return TextRes.GetString("ODataContextUriBuilder_ValidateDerivedType", new object[] { p0, p1 });
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06001A28 RID: 6696 RVA: 0x0005656F File Offset: 0x0005476F
		internal static string ODataContextUriBuilder_TypeNameMissingForTopLevelCollection
		{
			get
			{
				return TextRes.GetString("ODataContextUriBuilder_TypeNameMissingForTopLevelCollection");
			}
		}

		// Token: 0x06001A29 RID: 6697 RVA: 0x0005657C File Offset: 0x0005477C
		internal static string ODataContextUriBuilder_UnsupportedPayloadKind(object p0)
		{
			return TextRes.GetString("ODataContextUriBuilder_UnsupportedPayloadKind", new object[] { p0 });
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06001A2A RID: 6698 RVA: 0x0005659F File Offset: 0x0005479F
		internal static string ODataContextUriBuilder_StreamValueMustBePropertiesOfODataEntry
		{
			get
			{
				return TextRes.GetString("ODataContextUriBuilder_StreamValueMustBePropertiesOfODataEntry");
			}
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06001A2B RID: 6699 RVA: 0x000565AB File Offset: 0x000547AB
		internal static string ODataContextUriBuilder_NavigationSourceMissingForEntryAndFeed
		{
			get
			{
				return TextRes.GetString("ODataContextUriBuilder_NavigationSourceMissingForEntryAndFeed");
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06001A2C RID: 6700 RVA: 0x000565B7 File Offset: 0x000547B7
		internal static string ODataContextUriBuilder_ODataUriMissingForIndividualProperty
		{
			get
			{
				return TextRes.GetString("ODataContextUriBuilder_ODataUriMissingForIndividualProperty");
			}
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06001A2D RID: 6701 RVA: 0x000565C3 File Offset: 0x000547C3
		internal static string ODataContextUriBuilder_TypeNameMissingForProperty
		{
			get
			{
				return TextRes.GetString("ODataContextUriBuilder_TypeNameMissingForProperty");
			}
		}

		// Token: 0x06001A2E RID: 6702 RVA: 0x000565D0 File Offset: 0x000547D0
		internal static string ODataContextUriBuilder_ODataPathInvalidForContainedElement(object p0)
		{
			return TextRes.GetString("ODataContextUriBuilder_ODataPathInvalidForContainedElement", new object[] { p0 });
		}

		// Token: 0x06001A2F RID: 6703 RVA: 0x000565F4 File Offset: 0x000547F4
		internal static string ODataJsonLightPropertyAndValueDeserializer_UnexpectedAnnotationProperties(object p0)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_UnexpectedAnnotationProperties", new object[] { p0 });
		}

		// Token: 0x06001A30 RID: 6704 RVA: 0x00056618 File Offset: 0x00054818
		internal static string ODataJsonLightPropertyAndValueDeserializer_UnexpectedPropertyAnnotation(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_UnexpectedPropertyAnnotation", new object[] { p0, p1 });
		}

		// Token: 0x06001A31 RID: 6705 RVA: 0x00056640 File Offset: 0x00054840
		internal static string ODataJsonLightPropertyAndValueDeserializer_UnexpectedODataPropertyAnnotation(object p0)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_UnexpectedODataPropertyAnnotation", new object[] { p0 });
		}

		// Token: 0x06001A32 RID: 6706 RVA: 0x00056664 File Offset: 0x00054864
		internal static string ODataJsonLightPropertyAndValueDeserializer_UnexpectedProperty(object p0)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_UnexpectedProperty", new object[] { p0 });
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06001A33 RID: 6707 RVA: 0x00056687 File Offset: 0x00054887
		internal static string ODataJsonLightPropertyAndValueDeserializer_InvalidTopLevelPropertyPayload
		{
			get
			{
				return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_InvalidTopLevelPropertyPayload");
			}
		}

		// Token: 0x06001A34 RID: 6708 RVA: 0x00056694 File Offset: 0x00054894
		internal static string ODataJsonLightPropertyAndValueDeserializer_InvalidTopLevelPropertyName(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_InvalidTopLevelPropertyName", new object[] { p0, p1 });
		}

		// Token: 0x06001A35 RID: 6709 RVA: 0x000566BC File Offset: 0x000548BC
		internal static string ODataJsonLightPropertyAndValueDeserializer_InvalidTypeName(object p0)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_InvalidTypeName", new object[] { p0 });
		}

		// Token: 0x06001A36 RID: 6710 RVA: 0x000566E0 File Offset: 0x000548E0
		internal static string ODataJsonLightPropertyAndValueDeserializer_TopLevelPropertyAnnotationWithoutProperty(object p0)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_TopLevelPropertyAnnotationWithoutProperty", new object[] { p0 });
		}

		// Token: 0x06001A37 RID: 6711 RVA: 0x00056704 File Offset: 0x00054904
		internal static string ODataJsonLightPropertyAndValueDeserializer_ComplexValuePropertyAnnotationWithoutProperty(object p0)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_ComplexValuePropertyAnnotationWithoutProperty", new object[] { p0 });
		}

		// Token: 0x06001A38 RID: 6712 RVA: 0x00056728 File Offset: 0x00054928
		internal static string ODataJsonLightPropertyAndValueDeserializer_ComplexValueWithPropertyTypeAnnotation(object p0)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_ComplexValueWithPropertyTypeAnnotation", new object[] { p0 });
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06001A39 RID: 6713 RVA: 0x0005674B File Offset: 0x0005494B
		internal static string ODataJsonLightPropertyAndValueDeserializer_ComplexTypeAnnotationNotFirst
		{
			get
			{
				return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_ComplexTypeAnnotationNotFirst");
			}
		}

		// Token: 0x06001A3A RID: 6714 RVA: 0x00056758 File Offset: 0x00054958
		internal static string ODataJsonLightPropertyAndValueDeserializer_UnexpectedDataPropertyAnnotation(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_UnexpectedDataPropertyAnnotation", new object[] { p0, p1 });
		}

		// Token: 0x06001A3B RID: 6715 RVA: 0x00056780 File Offset: 0x00054980
		internal static string ODataJsonLightPropertyAndValueDeserializer_TypePropertyAfterValueProperty(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_TypePropertyAfterValueProperty", new object[] { p0, p1 });
		}

		// Token: 0x06001A3C RID: 6716 RVA: 0x000567A8 File Offset: 0x000549A8
		internal static string ODataJsonLightPropertyAndValueDeserializer_ODataTypeAnnotationInPrimitiveValue(object p0)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_ODataTypeAnnotationInPrimitiveValue", new object[] { p0 });
		}

		// Token: 0x06001A3D RID: 6717 RVA: 0x000567CC File Offset: 0x000549CC
		internal static string ODataJsonLightPropertyAndValueDeserializer_TopLevelPropertyWithPrimitiveNullValue(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_TopLevelPropertyWithPrimitiveNullValue", new object[] { p0, p1 });
		}

		// Token: 0x06001A3E RID: 6718 RVA: 0x000567F4 File Offset: 0x000549F4
		internal static string ODataJsonLightPropertyAndValueDeserializer_UnexpectedMetadataReferenceProperty(object p0)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_UnexpectedMetadataReferenceProperty", new object[] { p0 });
		}

		// Token: 0x06001A3F RID: 6719 RVA: 0x00056818 File Offset: 0x00054A18
		internal static string ODataJsonLightPropertyAndValueDeserializer_NoPropertyAndAnnotationAllowedInNullPayload(object p0)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_NoPropertyAndAnnotationAllowedInNullPayload", new object[] { p0 });
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x06001A40 RID: 6720 RVA: 0x0005683B File Offset: 0x00054A3B
		internal static string ODataJsonReaderCoreUtils_CannotReadSpatialPropertyValue
		{
			get
			{
				return TextRes.GetString("ODataJsonReaderCoreUtils_CannotReadSpatialPropertyValue");
			}
		}

		// Token: 0x06001A41 RID: 6721 RVA: 0x00056848 File Offset: 0x00054A48
		internal static string ODataJsonLightReaderUtils_AnnotationWithNullValue(object p0)
		{
			return TextRes.GetString("ODataJsonLightReaderUtils_AnnotationWithNullValue", new object[] { p0 });
		}

		// Token: 0x06001A42 RID: 6722 RVA: 0x0005686C File Offset: 0x00054A6C
		internal static string ODataJsonLightReaderUtils_InvalidValueForODataNullAnnotation(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightReaderUtils_InvalidValueForODataNullAnnotation", new object[] { p0, p1 });
		}

		// Token: 0x06001A43 RID: 6723 RVA: 0x00056894 File Offset: 0x00054A94
		internal static string JsonLightInstanceAnnotationWriter_DuplicateAnnotationNameInCollection(object p0)
		{
			return TextRes.GetString("JsonLightInstanceAnnotationWriter_DuplicateAnnotationNameInCollection", new object[] { p0 });
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x06001A44 RID: 6724 RVA: 0x000568B7 File Offset: 0x00054AB7
		internal static string ODataJsonLightContextUriParser_NullMetadataDocumentUri
		{
			get
			{
				return TextRes.GetString("ODataJsonLightContextUriParser_NullMetadataDocumentUri");
			}
		}

		// Token: 0x06001A45 RID: 6725 RVA: 0x000568C4 File Offset: 0x00054AC4
		internal static string ODataJsonLightContextUriParser_ContextUriDoesNotMatchExpectedPayloadKind(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightContextUriParser_ContextUriDoesNotMatchExpectedPayloadKind", new object[] { p0, p1 });
		}

		// Token: 0x06001A46 RID: 6726 RVA: 0x000568EC File Offset: 0x00054AEC
		internal static string ODataJsonLightContextUriParser_InvalidEntitySetNameOrTypeName(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightContextUriParser_InvalidEntitySetNameOrTypeName", new object[] { p0, p1 });
		}

		// Token: 0x06001A47 RID: 6727 RVA: 0x00056914 File Offset: 0x00054B14
		internal static string ODataJsonLightContextUriParser_InvalidPayloadKindWithSelectQueryOption(object p0)
		{
			return TextRes.GetString("ODataJsonLightContextUriParser_InvalidPayloadKindWithSelectQueryOption", new object[] { p0 });
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06001A48 RID: 6728 RVA: 0x00056937 File Offset: 0x00054B37
		internal static string ODataJsonLightContextUriParser_NoModel
		{
			get
			{
				return TextRes.GetString("ODataJsonLightContextUriParser_NoModel");
			}
		}

		// Token: 0x06001A49 RID: 6729 RVA: 0x00056944 File Offset: 0x00054B44
		internal static string ODataJsonLightContextUriParser_InvalidContextUrl(object p0)
		{
			return TextRes.GetString("ODataJsonLightContextUriParser_InvalidContextUrl", new object[] { p0 });
		}

		// Token: 0x06001A4A RID: 6730 RVA: 0x00056968 File Offset: 0x00054B68
		internal static string ODataJsonLightContextUriParser_LastSegmentIsKeySegment(object p0)
		{
			return TextRes.GetString("ODataJsonLightContextUriParser_LastSegmentIsKeySegment", new object[] { p0 });
		}

		// Token: 0x06001A4B RID: 6731 RVA: 0x0005698C File Offset: 0x00054B8C
		internal static string ODataJsonLightContextUriParser_TopLevelContextUrlShouldBeAbsolute(object p0)
		{
			return TextRes.GetString("ODataJsonLightContextUriParser_TopLevelContextUrlShouldBeAbsolute", new object[] { p0 });
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06001A4C RID: 6732 RVA: 0x000569AF File Offset: 0x00054BAF
		internal static string ODataJsonLightEntryAndFeedDeserializer_EntryTypeAnnotationNotFirst
		{
			get
			{
				return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_EntryTypeAnnotationNotFirst");
			}
		}

		// Token: 0x06001A4D RID: 6733 RVA: 0x000569BC File Offset: 0x00054BBC
		internal static string ODataJsonLightEntryAndFeedDeserializer_EntryInstanceAnnotationPrecededByProperty(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_EntryInstanceAnnotationPrecededByProperty", new object[] { p0 });
		}

		// Token: 0x06001A4E RID: 6734 RVA: 0x000569E0 File Offset: 0x00054BE0
		internal static string ODataJsonLightEntryAndFeedDeserializer_CannotReadFeedContentStart(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_CannotReadFeedContentStart", new object[] { p0 });
		}

		// Token: 0x06001A4F RID: 6735 RVA: 0x00056A04 File Offset: 0x00054C04
		internal static string ODataJsonLightEntryAndFeedDeserializer_ExpectedFeedPropertyNotFound(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_ExpectedFeedPropertyNotFound", new object[] { p0 });
		}

		// Token: 0x06001A50 RID: 6736 RVA: 0x00056A28 File Offset: 0x00054C28
		internal static string ODataJsonLightEntryAndFeedDeserializer_InvalidNodeTypeForItemsInFeed(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_InvalidNodeTypeForItemsInFeed", new object[] { p0 });
		}

		// Token: 0x06001A51 RID: 6737 RVA: 0x00056A4C File Offset: 0x00054C4C
		internal static string ODataJsonLightEntryAndFeedDeserializer_InvalidPropertyAnnotationInTopLevelFeed(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_InvalidPropertyAnnotationInTopLevelFeed", new object[] { p0 });
		}

		// Token: 0x06001A52 RID: 6738 RVA: 0x00056A70 File Offset: 0x00054C70
		internal static string ODataJsonLightEntryAndFeedDeserializer_InvalidPropertyInTopLevelFeed(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_InvalidPropertyInTopLevelFeed", new object[] { p0, p1 });
		}

		// Token: 0x06001A53 RID: 6739 RVA: 0x00056A98 File Offset: 0x00054C98
		internal static string ODataJsonLightEntryAndFeedDeserializer_PropertyWithoutValueWithWrongType(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_PropertyWithoutValueWithWrongType", new object[] { p0, p1 });
		}

		// Token: 0x06001A54 RID: 6740 RVA: 0x00056AC0 File Offset: 0x00054CC0
		internal static string ODataJsonLightEntryAndFeedDeserializer_OpenPropertyWithoutValue(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_OpenPropertyWithoutValue", new object[] { p0 });
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06001A55 RID: 6741 RVA: 0x00056AE3 File Offset: 0x00054CE3
		internal static string ODataJsonLightEntryAndFeedDeserializer_StreamPropertyInRequest
		{
			get
			{
				return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_StreamPropertyInRequest");
			}
		}

		// Token: 0x06001A56 RID: 6742 RVA: 0x00056AF0 File Offset: 0x00054CF0
		internal static string ODataJsonLightEntryAndFeedDeserializer_UnexpectedStreamPropertyAnnotation(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_UnexpectedStreamPropertyAnnotation", new object[] { p0, p1 });
		}

		// Token: 0x06001A57 RID: 6743 RVA: 0x00056B18 File Offset: 0x00054D18
		internal static string ODataJsonLightEntryAndFeedDeserializer_StreamPropertyWithValue(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_StreamPropertyWithValue", new object[] { p0 });
		}

		// Token: 0x06001A58 RID: 6744 RVA: 0x00056B3C File Offset: 0x00054D3C
		internal static string ODataJsonLightEntryAndFeedDeserializer_UnexpectedDeferredLinkPropertyAnnotation(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_UnexpectedDeferredLinkPropertyAnnotation", new object[] { p0, p1 });
		}

		// Token: 0x06001A59 RID: 6745 RVA: 0x00056B64 File Offset: 0x00054D64
		internal static string ODataJsonLightEntryAndFeedDeserializer_CannotReadSingletonNavigationPropertyValue(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_CannotReadSingletonNavigationPropertyValue", new object[] { p0, p1 });
		}

		// Token: 0x06001A5A RID: 6746 RVA: 0x00056B8C File Offset: 0x00054D8C
		internal static string ODataJsonLightEntryAndFeedDeserializer_CannotReadCollectionNavigationPropertyValue(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_CannotReadCollectionNavigationPropertyValue", new object[] { p0, p1 });
		}

		// Token: 0x06001A5B RID: 6747 RVA: 0x00056BB4 File Offset: 0x00054DB4
		internal static string ODataJsonLightEntryAndFeedDeserializer_CannotReadNavigationPropertyValue(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_CannotReadNavigationPropertyValue", new object[] { p0 });
		}

		// Token: 0x06001A5C RID: 6748 RVA: 0x00056BD8 File Offset: 0x00054DD8
		internal static string ODataJsonLightEntryAndFeedDeserializer_UnexpectedExpandedSingletonNavigationLinkPropertyAnnotation(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_UnexpectedExpandedSingletonNavigationLinkPropertyAnnotation", new object[] { p0, p1 });
		}

		// Token: 0x06001A5D RID: 6749 RVA: 0x00056C00 File Offset: 0x00054E00
		internal static string ODataJsonLightEntryAndFeedDeserializer_UnexpectedExpandedCollectionNavigationLinkPropertyAnnotation(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_UnexpectedExpandedCollectionNavigationLinkPropertyAnnotation", new object[] { p0, p1 });
		}

		// Token: 0x06001A5E RID: 6750 RVA: 0x00056C28 File Offset: 0x00054E28
		internal static string ODataJsonLightEntryAndFeedDeserializer_DuplicateExpandedFeedAnnotation(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_DuplicateExpandedFeedAnnotation", new object[] { p0, p1 });
		}

		// Token: 0x06001A5F RID: 6751 RVA: 0x00056C50 File Offset: 0x00054E50
		internal static string ODataJsonLightEntryAndFeedDeserializer_UnexpectedPropertyAnnotationAfterExpandedFeed(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_UnexpectedPropertyAnnotationAfterExpandedFeed", new object[] { p0, p1 });
		}

		// Token: 0x06001A60 RID: 6752 RVA: 0x00056C78 File Offset: 0x00054E78
		internal static string ODataJsonLightEntryAndFeedDeserializer_UnexpectedNavigationLinkInRequestPropertyAnnotation(object p0, object p1, object p2)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_UnexpectedNavigationLinkInRequestPropertyAnnotation", new object[] { p0, p1, p2 });
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x00056CA4 File Offset: 0x00054EA4
		internal static string ODataJsonLightEntryAndFeedDeserializer_ArrayValueForSingletonBindPropertyAnnotation(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_ArrayValueForSingletonBindPropertyAnnotation", new object[] { p0, p1 });
		}

		// Token: 0x06001A62 RID: 6754 RVA: 0x00056CCC File Offset: 0x00054ECC
		internal static string ODataJsonLightEntryAndFeedDeserializer_StringValueForCollectionBindPropertyAnnotation(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_StringValueForCollectionBindPropertyAnnotation", new object[] { p0, p1 });
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x00056CF4 File Offset: 0x00054EF4
		internal static string ODataJsonLightEntryAndFeedDeserializer_EmptyBindArray(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_EmptyBindArray", new object[] { p0 });
		}

		// Token: 0x06001A64 RID: 6756 RVA: 0x00056D18 File Offset: 0x00054F18
		internal static string ODataJsonLightEntryAndFeedDeserializer_NavigationPropertyWithoutValueAndEntityReferenceLink(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_NavigationPropertyWithoutValueAndEntityReferenceLink", new object[] { p0, p1 });
		}

		// Token: 0x06001A65 RID: 6757 RVA: 0x00056D40 File Offset: 0x00054F40
		internal static string ODataJsonLightEntryAndFeedDeserializer_SingletonNavigationPropertyWithBindingAndValue(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_SingletonNavigationPropertyWithBindingAndValue", new object[] { p0, p1 });
		}

		// Token: 0x06001A66 RID: 6758 RVA: 0x00056D68 File Offset: 0x00054F68
		internal static string ODataJsonLightEntryAndFeedDeserializer_PropertyWithoutValueWithUnknownType(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_PropertyWithoutValueWithUnknownType", new object[] { p0 });
		}

		// Token: 0x06001A67 RID: 6759 RVA: 0x00056D8C File Offset: 0x00054F8C
		internal static string ODataJsonLightEntryAndFeedDeserializer_OperationIsNotActionOrFunction(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_OperationIsNotActionOrFunction", new object[] { p0 });
		}

		// Token: 0x06001A68 RID: 6760 RVA: 0x00056DB0 File Offset: 0x00054FB0
		internal static string ODataJsonLightEntryAndFeedDeserializer_MultipleOptionalPropertiesInOperation(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_MultipleOptionalPropertiesInOperation", new object[] { p0, p1 });
		}

		// Token: 0x06001A69 RID: 6761 RVA: 0x00056DD8 File Offset: 0x00054FD8
		internal static string ODataJsonLightEntryAndFeedDeserializer_OperationMissingTargetProperty(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_OperationMissingTargetProperty", new object[] { p0 });
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06001A6A RID: 6762 RVA: 0x00056DFB File Offset: 0x00054FFB
		internal static string ODataJsonLightEntryAndFeedDeserializer_MetadataReferencePropertyInRequest
		{
			get
			{
				return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_MetadataReferencePropertyInRequest");
			}
		}

		// Token: 0x06001A6B RID: 6763 RVA: 0x00056E08 File Offset: 0x00055008
		internal static string ODataJsonLightValidationUtils_OperationPropertyCannotBeNull(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightValidationUtils_OperationPropertyCannotBeNull", new object[] { p0, p1 });
		}

		// Token: 0x06001A6C RID: 6764 RVA: 0x00056E30 File Offset: 0x00055030
		internal static string ODataJsonLightValidationUtils_OpenMetadataReferencePropertyNotSupported(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightValidationUtils_OpenMetadataReferencePropertyNotSupported", new object[] { p0, p1 });
		}

		// Token: 0x06001A6D RID: 6765 RVA: 0x00056E58 File Offset: 0x00055058
		internal static string ODataJsonLightDeserializer_RelativeUriUsedWithouODataMetadataAnnotation(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightDeserializer_RelativeUriUsedWithouODataMetadataAnnotation", new object[] { p0, p1 });
		}

		// Token: 0x06001A6E RID: 6766 RVA: 0x00056E80 File Offset: 0x00055080
		internal static string ODataJsonLightEntryMetadataContext_MetadataAnnotationMustBeInPayload(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntryMetadataContext_MetadataAnnotationMustBeInPayload", new object[] { p0 });
		}

		// Token: 0x06001A6F RID: 6767 RVA: 0x00056EA4 File Offset: 0x000550A4
		internal static string ODataJsonLightCollectionDeserializer_ExpectedCollectionPropertyNotFound(object p0)
		{
			return TextRes.GetString("ODataJsonLightCollectionDeserializer_ExpectedCollectionPropertyNotFound", new object[] { p0 });
		}

		// Token: 0x06001A70 RID: 6768 RVA: 0x00056EC8 File Offset: 0x000550C8
		internal static string ODataJsonLightCollectionDeserializer_CannotReadCollectionContentStart(object p0)
		{
			return TextRes.GetString("ODataJsonLightCollectionDeserializer_CannotReadCollectionContentStart", new object[] { p0 });
		}

		// Token: 0x06001A71 RID: 6769 RVA: 0x00056EEC File Offset: 0x000550EC
		internal static string ODataJsonLightCollectionDeserializer_CannotReadCollectionEnd(object p0)
		{
			return TextRes.GetString("ODataJsonLightCollectionDeserializer_CannotReadCollectionEnd", new object[] { p0 });
		}

		// Token: 0x06001A72 RID: 6770 RVA: 0x00056F10 File Offset: 0x00055110
		internal static string ODataJsonLightCollectionDeserializer_InvalidCollectionTypeName(object p0)
		{
			return TextRes.GetString("ODataJsonLightCollectionDeserializer_InvalidCollectionTypeName", new object[] { p0 });
		}

		// Token: 0x06001A73 RID: 6771 RVA: 0x00056F34 File Offset: 0x00055134
		internal static string ODataJsonLightEntityReferenceLinkDeserializer_EntityReferenceLinkMustBeObjectValue(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntityReferenceLinkDeserializer_EntityReferenceLinkMustBeObjectValue", new object[] { p0 });
		}

		// Token: 0x06001A74 RID: 6772 RVA: 0x00056F58 File Offset: 0x00055158
		internal static string ODataJsonLightEntityReferenceLinkDeserializer_PropertyAnnotationForEntityReferenceLink(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntityReferenceLinkDeserializer_PropertyAnnotationForEntityReferenceLink", new object[] { p0 });
		}

		// Token: 0x06001A75 RID: 6773 RVA: 0x00056F7C File Offset: 0x0005517C
		internal static string ODataJsonLightEntityReferenceLinkDeserializer_InvalidAnnotationInEntityReferenceLink(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntityReferenceLinkDeserializer_InvalidAnnotationInEntityReferenceLink", new object[] { p0 });
		}

		// Token: 0x06001A76 RID: 6774 RVA: 0x00056FA0 File Offset: 0x000551A0
		internal static string ODataJsonLightEntityReferenceLinkDeserializer_InvalidPropertyInEntityReferenceLink(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightEntityReferenceLinkDeserializer_InvalidPropertyInEntityReferenceLink", new object[] { p0, p1 });
		}

		// Token: 0x06001A77 RID: 6775 RVA: 0x00056FC8 File Offset: 0x000551C8
		internal static string ODataJsonLightEntityReferenceLinkDeserializer_MissingEntityReferenceLinkProperty(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntityReferenceLinkDeserializer_MissingEntityReferenceLinkProperty", new object[] { p0 });
		}

		// Token: 0x06001A78 RID: 6776 RVA: 0x00056FEC File Offset: 0x000551EC
		internal static string ODataJsonLightEntityReferenceLinkDeserializer_MultipleUriPropertiesInEntityReferenceLink(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntityReferenceLinkDeserializer_MultipleUriPropertiesInEntityReferenceLink", new object[] { p0 });
		}

		// Token: 0x06001A79 RID: 6777 RVA: 0x00057010 File Offset: 0x00055210
		internal static string ODataJsonLightEntityReferenceLinkDeserializer_EntityReferenceLinkUrlCannotBeNull(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntityReferenceLinkDeserializer_EntityReferenceLinkUrlCannotBeNull", new object[] { p0 });
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06001A7A RID: 6778 RVA: 0x00057033 File Offset: 0x00055233
		internal static string ODataJsonLightEntityReferenceLinkDeserializer_PropertyAnnotationForEntityReferenceLinks
		{
			get
			{
				return TextRes.GetString("ODataJsonLightEntityReferenceLinkDeserializer_PropertyAnnotationForEntityReferenceLinks");
			}
		}

		// Token: 0x06001A7B RID: 6779 RVA: 0x00057040 File Offset: 0x00055240
		internal static string ODataJsonLightEntityReferenceLinkDeserializer_InvalidEntityReferenceLinksPropertyFound(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightEntityReferenceLinkDeserializer_InvalidEntityReferenceLinksPropertyFound", new object[] { p0, p1 });
		}

		// Token: 0x06001A7C RID: 6780 RVA: 0x00057068 File Offset: 0x00055268
		internal static string ODataJsonLightEntityReferenceLinkDeserializer_InvalidPropertyAnnotationInEntityReferenceLinks(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntityReferenceLinkDeserializer_InvalidPropertyAnnotationInEntityReferenceLinks", new object[] { p0 });
		}

		// Token: 0x06001A7D RID: 6781 RVA: 0x0005708C File Offset: 0x0005528C
		internal static string ODataJsonLightEntityReferenceLinkDeserializer_ExpectedEntityReferenceLinksPropertyNotFound(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntityReferenceLinkDeserializer_ExpectedEntityReferenceLinksPropertyNotFound", new object[] { p0 });
		}

		// Token: 0x06001A7E RID: 6782 RVA: 0x000570B0 File Offset: 0x000552B0
		internal static string ODataJsonOperationsDeserializerUtils_OperationPropertyCannotBeNull(object p0, object p1, object p2)
		{
			return TextRes.GetString("ODataJsonOperationsDeserializerUtils_OperationPropertyCannotBeNull", new object[] { p0, p1, p2 });
		}

		// Token: 0x06001A7F RID: 6783 RVA: 0x000570DC File Offset: 0x000552DC
		internal static string ODataJsonOperationsDeserializerUtils_OperationsPropertyMustHaveObjectValue(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonOperationsDeserializerUtils_OperationsPropertyMustHaveObjectValue", new object[] { p0, p1 });
		}

		// Token: 0x06001A80 RID: 6784 RVA: 0x00057104 File Offset: 0x00055304
		internal static string ODataJsonLightServiceDocumentDeserializer_DuplicatePropertiesInServiceDocument(object p0)
		{
			return TextRes.GetString("ODataJsonLightServiceDocumentDeserializer_DuplicatePropertiesInServiceDocument", new object[] { p0 });
		}

		// Token: 0x06001A81 RID: 6785 RVA: 0x00057128 File Offset: 0x00055328
		internal static string ODataJsonLightServiceDocumentDeserializer_DuplicatePropertiesInServiceDocumentElement(object p0)
		{
			return TextRes.GetString("ODataJsonLightServiceDocumentDeserializer_DuplicatePropertiesInServiceDocumentElement", new object[] { p0 });
		}

		// Token: 0x06001A82 RID: 6786 RVA: 0x0005714C File Offset: 0x0005534C
		internal static string ODataJsonLightServiceDocumentDeserializer_MissingValuePropertyInServiceDocument(object p0)
		{
			return TextRes.GetString("ODataJsonLightServiceDocumentDeserializer_MissingValuePropertyInServiceDocument", new object[] { p0 });
		}

		// Token: 0x06001A83 RID: 6787 RVA: 0x00057170 File Offset: 0x00055370
		internal static string ODataJsonLightServiceDocumentDeserializer_MissingRequiredPropertyInServiceDocumentElement(object p0)
		{
			return TextRes.GetString("ODataJsonLightServiceDocumentDeserializer_MissingRequiredPropertyInServiceDocumentElement", new object[] { p0 });
		}

		// Token: 0x06001A84 RID: 6788 RVA: 0x00057194 File Offset: 0x00055394
		internal static string ODataJsonLightServiceDocumentDeserializer_PropertyAnnotationInServiceDocument(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightServiceDocumentDeserializer_PropertyAnnotationInServiceDocument", new object[] { p0, p1 });
		}

		// Token: 0x06001A85 RID: 6789 RVA: 0x000571BC File Offset: 0x000553BC
		internal static string ODataJsonLightServiceDocumentDeserializer_InstanceAnnotationInServiceDocument(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightServiceDocumentDeserializer_InstanceAnnotationInServiceDocument", new object[] { p0, p1 });
		}

		// Token: 0x06001A86 RID: 6790 RVA: 0x000571E4 File Offset: 0x000553E4
		internal static string ODataJsonLightServiceDocumentDeserializer_PropertyAnnotationInServiceDocumentElement(object p0)
		{
			return TextRes.GetString("ODataJsonLightServiceDocumentDeserializer_PropertyAnnotationInServiceDocumentElement", new object[] { p0 });
		}

		// Token: 0x06001A87 RID: 6791 RVA: 0x00057208 File Offset: 0x00055408
		internal static string ODataJsonLightServiceDocumentDeserializer_InstanceAnnotationInServiceDocumentElement(object p0)
		{
			return TextRes.GetString("ODataJsonLightServiceDocumentDeserializer_InstanceAnnotationInServiceDocumentElement", new object[] { p0 });
		}

		// Token: 0x06001A88 RID: 6792 RVA: 0x0005722C File Offset: 0x0005542C
		internal static string ODataJsonLightServiceDocumentDeserializer_UnexpectedPropertyInServiceDocumentElement(object p0, object p1, object p2)
		{
			return TextRes.GetString("ODataJsonLightServiceDocumentDeserializer_UnexpectedPropertyInServiceDocumentElement", new object[] { p0, p1, p2 });
		}

		// Token: 0x06001A89 RID: 6793 RVA: 0x00057258 File Offset: 0x00055458
		internal static string ODataJsonLightServiceDocumentDeserializer_UnexpectedPropertyInServiceDocument(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightServiceDocumentDeserializer_UnexpectedPropertyInServiceDocument", new object[] { p0, p1 });
		}

		// Token: 0x06001A8A RID: 6794 RVA: 0x00057280 File Offset: 0x00055480
		internal static string ODataJsonLightServiceDocumentDeserializer_PropertyAnnotationWithoutProperty(object p0)
		{
			return TextRes.GetString("ODataJsonLightServiceDocumentDeserializer_PropertyAnnotationWithoutProperty", new object[] { p0 });
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06001A8B RID: 6795 RVA: 0x000572A3 File Offset: 0x000554A3
		internal static string ODataJsonLightParameterDeserializer_PropertyAnnotationForParameters
		{
			get
			{
				return TextRes.GetString("ODataJsonLightParameterDeserializer_PropertyAnnotationForParameters");
			}
		}

		// Token: 0x06001A8C RID: 6796 RVA: 0x000572B0 File Offset: 0x000554B0
		internal static string ODataJsonLightParameterDeserializer_PropertyAnnotationWithoutPropertyForParameters(object p0)
		{
			return TextRes.GetString("ODataJsonLightParameterDeserializer_PropertyAnnotationWithoutPropertyForParameters", new object[] { p0 });
		}

		// Token: 0x06001A8D RID: 6797 RVA: 0x000572D4 File Offset: 0x000554D4
		internal static string ODataJsonLightParameterDeserializer_UnsupportedPrimitiveParameterType(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightParameterDeserializer_UnsupportedPrimitiveParameterType", new object[] { p0, p1 });
		}

		// Token: 0x06001A8E RID: 6798 RVA: 0x000572FC File Offset: 0x000554FC
		internal static string ODataJsonLightParameterDeserializer_NullCollectionExpected(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightParameterDeserializer_NullCollectionExpected", new object[] { p0, p1 });
		}

		// Token: 0x06001A8F RID: 6799 RVA: 0x00057324 File Offset: 0x00055524
		internal static string ODataJsonLightParameterDeserializer_UnsupportedParameterTypeKind(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightParameterDeserializer_UnsupportedParameterTypeKind", new object[] { p0, p1 });
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06001A90 RID: 6800 RVA: 0x0005734B File Offset: 0x0005554B
		internal static string SelectedPropertiesNode_StarSegmentNotLastSegment
		{
			get
			{
				return TextRes.GetString("SelectedPropertiesNode_StarSegmentNotLastSegment");
			}
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06001A91 RID: 6801 RVA: 0x00057357 File Offset: 0x00055557
		internal static string SelectedPropertiesNode_StarSegmentAfterTypeSegment
		{
			get
			{
				return TextRes.GetString("SelectedPropertiesNode_StarSegmentAfterTypeSegment");
			}
		}

		// Token: 0x06001A92 RID: 6802 RVA: 0x00057364 File Offset: 0x00055564
		internal static string ODataJsonLightErrorDeserializer_PropertyAnnotationNotAllowedInErrorPayload(object p0)
		{
			return TextRes.GetString("ODataJsonLightErrorDeserializer_PropertyAnnotationNotAllowedInErrorPayload", new object[] { p0 });
		}

		// Token: 0x06001A93 RID: 6803 RVA: 0x00057388 File Offset: 0x00055588
		internal static string ODataJsonLightErrorDeserializer_InstanceAnnotationNotAllowedInErrorPayload(object p0)
		{
			return TextRes.GetString("ODataJsonLightErrorDeserializer_InstanceAnnotationNotAllowedInErrorPayload", new object[] { p0 });
		}

		// Token: 0x06001A94 RID: 6804 RVA: 0x000573AC File Offset: 0x000555AC
		internal static string ODataJsonLightErrorDeserializer_PropertyAnnotationWithoutPropertyForError(object p0)
		{
			return TextRes.GetString("ODataJsonLightErrorDeserializer_PropertyAnnotationWithoutPropertyForError", new object[] { p0 });
		}

		// Token: 0x06001A95 RID: 6805 RVA: 0x000573D0 File Offset: 0x000555D0
		internal static string ODataJsonLightErrorDeserializer_TopLevelErrorValueWithInvalidProperty(object p0)
		{
			return TextRes.GetString("ODataJsonLightErrorDeserializer_TopLevelErrorValueWithInvalidProperty", new object[] { p0 });
		}

		// Token: 0x06001A96 RID: 6806 RVA: 0x000573F4 File Offset: 0x000555F4
		internal static string ODataConventionalUriBuilder_EntityTypeWithNoKeyProperties(object p0)
		{
			return TextRes.GetString("ODataConventionalUriBuilder_EntityTypeWithNoKeyProperties", new object[] { p0 });
		}

		// Token: 0x06001A97 RID: 6807 RVA: 0x00057418 File Offset: 0x00055618
		internal static string ODataConventionalUriBuilder_NullKeyValue(object p0, object p1)
		{
			return TextRes.GetString("ODataConventionalUriBuilder_NullKeyValue", new object[] { p0, p1 });
		}

		// Token: 0x06001A98 RID: 6808 RVA: 0x00057440 File Offset: 0x00055640
		internal static string ODataEntryMetadataContext_EntityTypeWithNoKeyProperties(object p0)
		{
			return TextRes.GetString("ODataEntryMetadataContext_EntityTypeWithNoKeyProperties", new object[] { p0 });
		}

		// Token: 0x06001A99 RID: 6809 RVA: 0x00057464 File Offset: 0x00055664
		internal static string ODataEntryMetadataContext_NullKeyValue(object p0, object p1)
		{
			return TextRes.GetString("ODataEntryMetadataContext_NullKeyValue", new object[] { p0, p1 });
		}

		// Token: 0x06001A9A RID: 6810 RVA: 0x0005748C File Offset: 0x0005568C
		internal static string ODataEntryMetadataContext_KeyOrETagValuesMustBePrimitiveValues(object p0, object p1)
		{
			return TextRes.GetString("ODataEntryMetadataContext_KeyOrETagValuesMustBePrimitiveValues", new object[] { p0, p1 });
		}

		// Token: 0x06001A9B RID: 6811 RVA: 0x000574B4 File Offset: 0x000556B4
		internal static string EdmValueUtils_NonPrimitiveValue(object p0, object p1)
		{
			return TextRes.GetString("EdmValueUtils_NonPrimitiveValue", new object[] { p0, p1 });
		}

		// Token: 0x06001A9C RID: 6812 RVA: 0x000574DC File Offset: 0x000556DC
		internal static string EdmValueUtils_PropertyDoesntExist(object p0, object p1)
		{
			return TextRes.GetString("EdmValueUtils_PropertyDoesntExist", new object[] { p0, p1 });
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06001A9D RID: 6813 RVA: 0x00057503 File Offset: 0x00055703
		internal static string ODataPrimitiveValue_CannotCreateODataPrimitiveValueFromNull
		{
			get
			{
				return TextRes.GetString("ODataPrimitiveValue_CannotCreateODataPrimitiveValueFromNull");
			}
		}

		// Token: 0x06001A9E RID: 6814 RVA: 0x00057510 File Offset: 0x00055710
		internal static string ODataPrimitiveValue_CannotCreateODataPrimitiveValueFromUnsupportedValueType(object p0)
		{
			return TextRes.GetString("ODataPrimitiveValue_CannotCreateODataPrimitiveValueFromUnsupportedValueType", new object[] { p0 });
		}

		// Token: 0x06001A9F RID: 6815 RVA: 0x00057534 File Offset: 0x00055734
		internal static string ODataInstanceAnnotation_NeedPeriodInName(object p0)
		{
			return TextRes.GetString("ODataInstanceAnnotation_NeedPeriodInName", new object[] { p0 });
		}

		// Token: 0x06001AA0 RID: 6816 RVA: 0x00057558 File Offset: 0x00055758
		internal static string ODataInstanceAnnotation_ReservedNamesNotAllowed(object p0, object p1)
		{
			return TextRes.GetString("ODataInstanceAnnotation_ReservedNamesNotAllowed", new object[] { p0, p1 });
		}

		// Token: 0x06001AA1 RID: 6817 RVA: 0x00057580 File Offset: 0x00055780
		internal static string ODataInstanceAnnotation_BadTermName(object p0)
		{
			return TextRes.GetString("ODataInstanceAnnotation_BadTermName", new object[] { p0 });
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06001AA2 RID: 6818 RVA: 0x000575A3 File Offset: 0x000557A3
		internal static string ODataInstanceAnnotation_ValueCannotBeODataStreamReferenceValue
		{
			get
			{
				return TextRes.GetString("ODataInstanceAnnotation_ValueCannotBeODataStreamReferenceValue");
			}
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06001AA3 RID: 6819 RVA: 0x000575AF File Offset: 0x000557AF
		internal static string ODataJsonLightValueSerializer_MissingTypeNameOnComplex
		{
			get
			{
				return TextRes.GetString("ODataJsonLightValueSerializer_MissingTypeNameOnComplex");
			}
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06001AA4 RID: 6820 RVA: 0x000575BB File Offset: 0x000557BB
		internal static string ODataJsonLightValueSerializer_MissingTypeNameOnCollection
		{
			get
			{
				return TextRes.GetString("ODataJsonLightValueSerializer_MissingTypeNameOnCollection");
			}
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06001AA5 RID: 6821 RVA: 0x000575C7 File Offset: 0x000557C7
		internal static string ODataJsonLightValueSerializer_MissingRawValueOnUntyped
		{
			get
			{
				return TextRes.GetString("ODataJsonLightValueSerializer_MissingRawValueOnUntyped");
			}
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06001AA6 RID: 6822 RVA: 0x000575D3 File Offset: 0x000557D3
		internal static string AtomInstanceAnnotation_MissingTermAttributeOnAnnotationElement
		{
			get
			{
				return TextRes.GetString("AtomInstanceAnnotation_MissingTermAttributeOnAnnotationElement");
			}
		}

		// Token: 0x06001AA7 RID: 6823 RVA: 0x000575E0 File Offset: 0x000557E0
		internal static string AtomInstanceAnnotation_AttributeValueNotationUsedWithIncompatibleType(object p0, object p1)
		{
			return TextRes.GetString("AtomInstanceAnnotation_AttributeValueNotationUsedWithIncompatibleType", new object[] { p0, p1 });
		}

		// Token: 0x06001AA8 RID: 6824 RVA: 0x00057608 File Offset: 0x00055808
		internal static string AtomInstanceAnnotation_AttributeValueNotationUsedOnNonEmptyElement(object p0)
		{
			return TextRes.GetString("AtomInstanceAnnotation_AttributeValueNotationUsedOnNonEmptyElement", new object[] { p0 });
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06001AA9 RID: 6825 RVA: 0x0005762B File Offset: 0x0005582B
		internal static string AtomInstanceAnnotation_MultipleAttributeValueNotationAttributes
		{
			get
			{
				return TextRes.GetString("AtomInstanceAnnotation_MultipleAttributeValueNotationAttributes");
			}
		}

		// Token: 0x06001AAA RID: 6826 RVA: 0x00057638 File Offset: 0x00055838
		internal static string AnnotationFilterPattern_InvalidPatternMissingDot(object p0)
		{
			return TextRes.GetString("AnnotationFilterPattern_InvalidPatternMissingDot", new object[] { p0 });
		}

		// Token: 0x06001AAB RID: 6827 RVA: 0x0005765C File Offset: 0x0005585C
		internal static string AnnotationFilterPattern_InvalidPatternEmptySegment(object p0)
		{
			return TextRes.GetString("AnnotationFilterPattern_InvalidPatternEmptySegment", new object[] { p0 });
		}

		// Token: 0x06001AAC RID: 6828 RVA: 0x00057680 File Offset: 0x00055880
		internal static string AnnotationFilterPattern_InvalidPatternWildCardInSegment(object p0)
		{
			return TextRes.GetString("AnnotationFilterPattern_InvalidPatternWildCardInSegment", new object[] { p0 });
		}

		// Token: 0x06001AAD RID: 6829 RVA: 0x000576A4 File Offset: 0x000558A4
		internal static string AnnotationFilterPattern_InvalidPatternWildCardMustBeInLastSegment(object p0)
		{
			return TextRes.GetString("AnnotationFilterPattern_InvalidPatternWildCardMustBeInLastSegment", new object[] { p0 });
		}

		// Token: 0x06001AAE RID: 6830 RVA: 0x000576C8 File Offset: 0x000558C8
		internal static string SyntacticTree_UriMustBeAbsolute(object p0)
		{
			return TextRes.GetString("SyntacticTree_UriMustBeAbsolute", new object[] { p0 });
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06001AAF RID: 6831 RVA: 0x000576EB File Offset: 0x000558EB
		internal static string SyntacticTree_MaxDepthInvalid
		{
			get
			{
				return TextRes.GetString("SyntacticTree_MaxDepthInvalid");
			}
		}

		// Token: 0x06001AB0 RID: 6832 RVA: 0x000576F8 File Offset: 0x000558F8
		internal static string SyntacticTree_InvalidSkipQueryOptionValue(object p0)
		{
			return TextRes.GetString("SyntacticTree_InvalidSkipQueryOptionValue", new object[] { p0 });
		}

		// Token: 0x06001AB1 RID: 6833 RVA: 0x0005771C File Offset: 0x0005591C
		internal static string SyntacticTree_InvalidTopQueryOptionValue(object p0)
		{
			return TextRes.GetString("SyntacticTree_InvalidTopQueryOptionValue", new object[] { p0 });
		}

		// Token: 0x06001AB2 RID: 6834 RVA: 0x00057740 File Offset: 0x00055940
		internal static string SyntacticTree_InvalidCountQueryOptionValue(object p0, object p1)
		{
			return TextRes.GetString("SyntacticTree_InvalidCountQueryOptionValue", new object[] { p0, p1 });
		}

		// Token: 0x06001AB3 RID: 6835 RVA: 0x00057768 File Offset: 0x00055968
		internal static string QueryOptionUtils_QueryParameterMustBeSpecifiedOnce(object p0)
		{
			return TextRes.GetString("QueryOptionUtils_QueryParameterMustBeSpecifiedOnce", new object[] { p0 });
		}

		// Token: 0x06001AB4 RID: 6836 RVA: 0x0005778C File Offset: 0x0005598C
		internal static string UriBuilder_NotSupportedClrLiteral(object p0)
		{
			return TextRes.GetString("UriBuilder_NotSupportedClrLiteral", new object[] { p0 });
		}

		// Token: 0x06001AB5 RID: 6837 RVA: 0x000577B0 File Offset: 0x000559B0
		internal static string UriBuilder_NotSupportedQueryToken(object p0)
		{
			return TextRes.GetString("UriBuilder_NotSupportedQueryToken", new object[] { p0 });
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06001AB6 RID: 6838 RVA: 0x000577D3 File Offset: 0x000559D3
		internal static string UriQueryExpressionParser_TooDeep
		{
			get
			{
				return TextRes.GetString("UriQueryExpressionParser_TooDeep");
			}
		}

		// Token: 0x06001AB7 RID: 6839 RVA: 0x000577E0 File Offset: 0x000559E0
		internal static string UriQueryExpressionParser_ExpressionExpected(object p0, object p1)
		{
			return TextRes.GetString("UriQueryExpressionParser_ExpressionExpected", new object[] { p0, p1 });
		}

		// Token: 0x06001AB8 RID: 6840 RVA: 0x00057808 File Offset: 0x00055A08
		internal static string UriQueryExpressionParser_OpenParenExpected(object p0, object p1)
		{
			return TextRes.GetString("UriQueryExpressionParser_OpenParenExpected", new object[] { p0, p1 });
		}

		// Token: 0x06001AB9 RID: 6841 RVA: 0x00057830 File Offset: 0x00055A30
		internal static string UriQueryExpressionParser_CloseParenOrCommaExpected(object p0, object p1)
		{
			return TextRes.GetString("UriQueryExpressionParser_CloseParenOrCommaExpected", new object[] { p0, p1 });
		}

		// Token: 0x06001ABA RID: 6842 RVA: 0x00057858 File Offset: 0x00055A58
		internal static string UriQueryExpressionParser_CloseParenOrOperatorExpected(object p0, object p1)
		{
			return TextRes.GetString("UriQueryExpressionParser_CloseParenOrOperatorExpected", new object[] { p0, p1 });
		}

		// Token: 0x06001ABB RID: 6843 RVA: 0x00057880 File Offset: 0x00055A80
		internal static string UriQueryExpressionParser_CannotCreateStarTokenFromNonStar(object p0)
		{
			return TextRes.GetString("UriQueryExpressionParser_CannotCreateStarTokenFromNonStar", new object[] { p0 });
		}

		// Token: 0x06001ABC RID: 6844 RVA: 0x000578A4 File Offset: 0x00055AA4
		internal static string UriQueryExpressionParser_RangeVariableAlreadyDeclared(object p0)
		{
			return TextRes.GetString("UriQueryExpressionParser_RangeVariableAlreadyDeclared", new object[] { p0 });
		}

		// Token: 0x06001ABD RID: 6845 RVA: 0x000578C8 File Offset: 0x00055AC8
		internal static string UriQueryExpressionParser_AsExpected(object p0, object p1)
		{
			return TextRes.GetString("UriQueryExpressionParser_AsExpected", new object[] { p0, p1 });
		}

		// Token: 0x06001ABE RID: 6846 RVA: 0x000578F0 File Offset: 0x00055AF0
		internal static string UriQueryExpressionParser_WithExpected(object p0, object p1)
		{
			return TextRes.GetString("UriQueryExpressionParser_WithExpected", new object[] { p0, p1 });
		}

		// Token: 0x06001ABF RID: 6847 RVA: 0x00057918 File Offset: 0x00055B18
		internal static string UriQueryExpressionParser_UnrecognizedWithVerb(object p0, object p1, object p2)
		{
			return TextRes.GetString("UriQueryExpressionParser_UnrecognizedWithVerb", new object[] { p0, p1, p2 });
		}

		// Token: 0x06001AC0 RID: 6848 RVA: 0x00057944 File Offset: 0x00055B44
		internal static string UriQueryExpressionParser_PropertyPathExpected(object p0, object p1)
		{
			return TextRes.GetString("UriQueryExpressionParser_PropertyPathExpected", new object[] { p0, p1 });
		}

		// Token: 0x06001AC1 RID: 6849 RVA: 0x0005796C File Offset: 0x00055B6C
		internal static string UriQueryExpressionParser_KeywordOrIdentifierExpected(object p0, object p1, object p2)
		{
			return TextRes.GetString("UriQueryExpressionParser_KeywordOrIdentifierExpected", new object[] { p0, p1, p2 });
		}

		// Token: 0x06001AC2 RID: 6850 RVA: 0x00057998 File Offset: 0x00055B98
		internal static string UriQueryPathParser_RequestUriDoesNotHaveTheCorrectBaseUri(object p0, object p1)
		{
			return TextRes.GetString("UriQueryPathParser_RequestUriDoesNotHaveTheCorrectBaseUri", new object[] { p0, p1 });
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06001AC3 RID: 6851 RVA: 0x000579BF File Offset: 0x00055BBF
		internal static string UriQueryPathParser_SyntaxError
		{
			get
			{
				return TextRes.GetString("UriQueryPathParser_SyntaxError");
			}
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06001AC4 RID: 6852 RVA: 0x000579CB File Offset: 0x00055BCB
		internal static string UriQueryPathParser_TooManySegments
		{
			get
			{
				return TextRes.GetString("UriQueryPathParser_TooManySegments");
			}
		}

		// Token: 0x06001AC5 RID: 6853 RVA: 0x000579D8 File Offset: 0x00055BD8
		internal static string UriUtils_DateTimeOffsetInvalidFormat(object p0)
		{
			return TextRes.GetString("UriUtils_DateTimeOffsetInvalidFormat", new object[] { p0 });
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06001AC6 RID: 6854 RVA: 0x000579FB File Offset: 0x00055BFB
		internal static string SelectionItemBinder_NonNavigationPathToken
		{
			get
			{
				return TextRes.GetString("SelectionItemBinder_NonNavigationPathToken");
			}
		}

		// Token: 0x06001AC7 RID: 6855 RVA: 0x00057A08 File Offset: 0x00055C08
		internal static string MetadataBinder_UnsupportedQueryTokenKind(object p0)
		{
			return TextRes.GetString("MetadataBinder_UnsupportedQueryTokenKind", new object[] { p0 });
		}

		// Token: 0x06001AC8 RID: 6856 RVA: 0x00057A2C File Offset: 0x00055C2C
		internal static string MetadataBinder_PropertyNotDeclared(object p0, object p1)
		{
			return TextRes.GetString("MetadataBinder_PropertyNotDeclared", new object[] { p0, p1 });
		}

		// Token: 0x06001AC9 RID: 6857 RVA: 0x00057A54 File Offset: 0x00055C54
		internal static string MetadataBinder_PropertyNotDeclaredOrNotKeyInKeyValue(object p0, object p1)
		{
			return TextRes.GetString("MetadataBinder_PropertyNotDeclaredOrNotKeyInKeyValue", new object[] { p0, p1 });
		}

		// Token: 0x06001ACA RID: 6858 RVA: 0x00057A7C File Offset: 0x00055C7C
		internal static string MetadataBinder_QualifiedFunctionNameWithParametersNotDeclared(object p0, object p1)
		{
			return TextRes.GetString("MetadataBinder_QualifiedFunctionNameWithParametersNotDeclared", new object[] { p0, p1 });
		}

		// Token: 0x06001ACB RID: 6859 RVA: 0x00057AA4 File Offset: 0x00055CA4
		internal static string MetadataBinder_UnnamedKeyValueOnTypeWithMultipleKeyProperties(object p0)
		{
			return TextRes.GetString("MetadataBinder_UnnamedKeyValueOnTypeWithMultipleKeyProperties", new object[] { p0 });
		}

		// Token: 0x06001ACC RID: 6860 RVA: 0x00057AC8 File Offset: 0x00055CC8
		internal static string MetadataBinder_DuplicitKeyPropertyInKeyValues(object p0)
		{
			return TextRes.GetString("MetadataBinder_DuplicitKeyPropertyInKeyValues", new object[] { p0 });
		}

		// Token: 0x06001ACD RID: 6861 RVA: 0x00057AEC File Offset: 0x00055CEC
		internal static string MetadataBinder_NotAllKeyPropertiesSpecifiedInKeyValues(object p0)
		{
			return TextRes.GetString("MetadataBinder_NotAllKeyPropertiesSpecifiedInKeyValues", new object[] { p0 });
		}

		// Token: 0x06001ACE RID: 6862 RVA: 0x00057B10 File Offset: 0x00055D10
		internal static string MetadataBinder_CannotConvertToType(object p0, object p1)
		{
			return TextRes.GetString("MetadataBinder_CannotConvertToType", new object[] { p0, p1 });
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06001ACF RID: 6863 RVA: 0x00057B37 File Offset: 0x00055D37
		internal static string MetadataBinder_FilterExpressionNotSingleValue
		{
			get
			{
				return TextRes.GetString("MetadataBinder_FilterExpressionNotSingleValue");
			}
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06001AD0 RID: 6864 RVA: 0x00057B43 File Offset: 0x00055D43
		internal static string MetadataBinder_OrderByExpressionNotSingleValue
		{
			get
			{
				return TextRes.GetString("MetadataBinder_OrderByExpressionNotSingleValue");
			}
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06001AD1 RID: 6865 RVA: 0x00057B4F File Offset: 0x00055D4F
		internal static string MetadataBinder_PropertyAccessWithoutParentParameter
		{
			get
			{
				return TextRes.GetString("MetadataBinder_PropertyAccessWithoutParentParameter");
			}
		}

		// Token: 0x06001AD2 RID: 6866 RVA: 0x00057B5C File Offset: 0x00055D5C
		internal static string MetadataBinder_BinaryOperatorOperandNotSingleValue(object p0)
		{
			return TextRes.GetString("MetadataBinder_BinaryOperatorOperandNotSingleValue", new object[] { p0 });
		}

		// Token: 0x06001AD3 RID: 6867 RVA: 0x00057B80 File Offset: 0x00055D80
		internal static string MetadataBinder_UnaryOperatorOperandNotSingleValue(object p0)
		{
			return TextRes.GetString("MetadataBinder_UnaryOperatorOperandNotSingleValue", new object[] { p0 });
		}

		// Token: 0x06001AD4 RID: 6868 RVA: 0x00057BA4 File Offset: 0x00055DA4
		internal static string MetadataBinder_PropertyAccessSourceNotSingleValue(object p0)
		{
			return TextRes.GetString("MetadataBinder_PropertyAccessSourceNotSingleValue", new object[] { p0 });
		}

		// Token: 0x06001AD5 RID: 6869 RVA: 0x00057BC8 File Offset: 0x00055DC8
		internal static string MetadataBinder_IncompatibleOperandsError(object p0, object p1, object p2)
		{
			return TextRes.GetString("MetadataBinder_IncompatibleOperandsError", new object[] { p0, p1, p2 });
		}

		// Token: 0x06001AD6 RID: 6870 RVA: 0x00057BF4 File Offset: 0x00055DF4
		internal static string MetadataBinder_IncompatibleOperandError(object p0, object p1)
		{
			return TextRes.GetString("MetadataBinder_IncompatibleOperandError", new object[] { p0, p1 });
		}

		// Token: 0x06001AD7 RID: 6871 RVA: 0x00057C1C File Offset: 0x00055E1C
		internal static string MetadataBinder_UnknownFunction(object p0)
		{
			return TextRes.GetString("MetadataBinder_UnknownFunction", new object[] { p0 });
		}

		// Token: 0x06001AD8 RID: 6872 RVA: 0x00057C40 File Offset: 0x00055E40
		internal static string MetadataBinder_FunctionArgumentNotSingleValue(object p0)
		{
			return TextRes.GetString("MetadataBinder_FunctionArgumentNotSingleValue", new object[] { p0 });
		}

		// Token: 0x06001AD9 RID: 6873 RVA: 0x00057C64 File Offset: 0x00055E64
		internal static string MetadataBinder_NoApplicableFunctionFound(object p0, object p1)
		{
			return TextRes.GetString("MetadataBinder_NoApplicableFunctionFound", new object[] { p0, p1 });
		}

		// Token: 0x06001ADA RID: 6874 RVA: 0x00057C8C File Offset: 0x00055E8C
		internal static string MetadataBinder_BoundNodeCannotBeNull(object p0)
		{
			return TextRes.GetString("MetadataBinder_BoundNodeCannotBeNull", new object[] { p0 });
		}

		// Token: 0x06001ADB RID: 6875 RVA: 0x00057CB0 File Offset: 0x00055EB0
		internal static string MetadataBinder_TopRequiresNonNegativeInteger(object p0)
		{
			return TextRes.GetString("MetadataBinder_TopRequiresNonNegativeInteger", new object[] { p0 });
		}

		// Token: 0x06001ADC RID: 6876 RVA: 0x00057CD4 File Offset: 0x00055ED4
		internal static string MetadataBinder_SkipRequiresNonNegativeInteger(object p0)
		{
			return TextRes.GetString("MetadataBinder_SkipRequiresNonNegativeInteger", new object[] { p0 });
		}

		// Token: 0x06001ADD RID: 6877 RVA: 0x00057CF8 File Offset: 0x00055EF8
		internal static string MetadataBinder_HierarchyNotFollowed(object p0, object p1)
		{
			return TextRes.GetString("MetadataBinder_HierarchyNotFollowed", new object[] { p0, p1 });
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06001ADE RID: 6878 RVA: 0x00057D1F File Offset: 0x00055F1F
		internal static string MetadataBinder_LambdaParentMustBeCollection
		{
			get
			{
				return TextRes.GetString("MetadataBinder_LambdaParentMustBeCollection");
			}
		}

		// Token: 0x06001ADF RID: 6879 RVA: 0x00057D2C File Offset: 0x00055F2C
		internal static string MetadataBinder_ParameterNotInScope(object p0)
		{
			return TextRes.GetString("MetadataBinder_ParameterNotInScope", new object[] { p0 });
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06001AE0 RID: 6880 RVA: 0x00057D4F File Offset: 0x00055F4F
		internal static string MetadataBinder_NavigationPropertyNotFollowingSingleEntityType
		{
			get
			{
				return TextRes.GetString("MetadataBinder_NavigationPropertyNotFollowingSingleEntityType");
			}
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06001AE1 RID: 6881 RVA: 0x00057D5B File Offset: 0x00055F5B
		internal static string MetadataBinder_AnyAllExpressionNotSingleValue
		{
			get
			{
				return TextRes.GetString("MetadataBinder_AnyAllExpressionNotSingleValue");
			}
		}

		// Token: 0x06001AE2 RID: 6882 RVA: 0x00057D68 File Offset: 0x00055F68
		internal static string MetadataBinder_CastOrIsOfExpressionWithWrongNumberOfOperands(object p0)
		{
			return TextRes.GetString("MetadataBinder_CastOrIsOfExpressionWithWrongNumberOfOperands", new object[] { p0 });
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06001AE3 RID: 6883 RVA: 0x00057D8B File Offset: 0x00055F8B
		internal static string MetadataBinder_CastOrIsOfFunctionWithoutATypeArgument
		{
			get
			{
				return TextRes.GetString("MetadataBinder_CastOrIsOfFunctionWithoutATypeArgument");
			}
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06001AE4 RID: 6884 RVA: 0x00057D97 File Offset: 0x00055F97
		internal static string MetadataBinder_CastOrIsOfCollectionsNotSupported
		{
			get
			{
				return TextRes.GetString("MetadataBinder_CastOrIsOfCollectionsNotSupported");
			}
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06001AE5 RID: 6885 RVA: 0x00057DA3 File Offset: 0x00055FA3
		internal static string MetadataBinder_CollectionOpenPropertiesNotSupportedInThisRelease
		{
			get
			{
				return TextRes.GetString("MetadataBinder_CollectionOpenPropertiesNotSupportedInThisRelease");
			}
		}

		// Token: 0x06001AE6 RID: 6886 RVA: 0x00057DB0 File Offset: 0x00055FB0
		internal static string MetadataBinder_IllegalSegmentType(object p0)
		{
			return TextRes.GetString("MetadataBinder_IllegalSegmentType", new object[] { p0 });
		}

		// Token: 0x06001AE7 RID: 6887 RVA: 0x00057DD4 File Offset: 0x00055FD4
		internal static string MetadataBinder_QueryOptionNotApplicable(object p0)
		{
			return TextRes.GetString("MetadataBinder_QueryOptionNotApplicable", new object[] { p0 });
		}

		// Token: 0x06001AE8 RID: 6888 RVA: 0x00057DF8 File Offset: 0x00055FF8
		internal static string ApplyBinder_AggregateExpressionIncompatibleTypeForMethod(object p0, object p1)
		{
			return TextRes.GetString("ApplyBinder_AggregateExpressionIncompatibleTypeForMethod", new object[] { p0, p1 });
		}

		// Token: 0x06001AE9 RID: 6889 RVA: 0x00057E20 File Offset: 0x00056020
		internal static string ApplyBinder_UnsupportedAggregateMethod(object p0)
		{
			return TextRes.GetString("ApplyBinder_UnsupportedAggregateMethod", new object[] { p0 });
		}

		// Token: 0x06001AEA RID: 6890 RVA: 0x00057E44 File Offset: 0x00056044
		internal static string ApplyBinder_AggregateExpressionNotSingleValue(object p0)
		{
			return TextRes.GetString("ApplyBinder_AggregateExpressionNotSingleValue", new object[] { p0 });
		}

		// Token: 0x06001AEB RID: 6891 RVA: 0x00057E68 File Offset: 0x00056068
		internal static string ApplyBinder_GroupByPropertyNotPropertyAccessValue(object p0)
		{
			return TextRes.GetString("ApplyBinder_GroupByPropertyNotPropertyAccessValue", new object[] { p0 });
		}

		// Token: 0x06001AEC RID: 6892 RVA: 0x00057E8C File Offset: 0x0005608C
		internal static string ApplyBinder_UnsupportedType(object p0)
		{
			return TextRes.GetString("ApplyBinder_UnsupportedType", new object[] { p0 });
		}

		// Token: 0x06001AED RID: 6893 RVA: 0x00057EB0 File Offset: 0x000560B0
		internal static string ApplyBinder_UnsupportedGroupByChild(object p0)
		{
			return TextRes.GetString("ApplyBinder_UnsupportedGroupByChild", new object[] { p0 });
		}

		// Token: 0x06001AEE RID: 6894 RVA: 0x00057ED4 File Offset: 0x000560D4
		internal static string FunctionCallBinder_CannotFindASuitableOverload(object p0, object p1)
		{
			return TextRes.GetString("FunctionCallBinder_CannotFindASuitableOverload", new object[] { p0, p1 });
		}

		// Token: 0x06001AEF RID: 6895 RVA: 0x00057EFC File Offset: 0x000560FC
		internal static string FunctionCallBinder_UriFunctionMustHaveHaveNullParent(object p0)
		{
			return TextRes.GetString("FunctionCallBinder_UriFunctionMustHaveHaveNullParent", new object[] { p0 });
		}

		// Token: 0x06001AF0 RID: 6896 RVA: 0x00057F20 File Offset: 0x00056120
		internal static string FunctionCallBinder_CallingFunctionOnOpenProperty(object p0)
		{
			return TextRes.GetString("FunctionCallBinder_CallingFunctionOnOpenProperty", new object[] { p0 });
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06001AF1 RID: 6897 RVA: 0x00057F43 File Offset: 0x00056143
		internal static string FunctionCallParser_DuplicateParameterOrEntityKeyName
		{
			get
			{
				return TextRes.GetString("FunctionCallParser_DuplicateParameterOrEntityKeyName");
			}
		}

		// Token: 0x06001AF2 RID: 6898 RVA: 0x00057F50 File Offset: 0x00056150
		internal static string ODataUriParser_InvalidCount(object p0)
		{
			return TextRes.GetString("ODataUriParser_InvalidCount", new object[] { p0 });
		}

		// Token: 0x06001AF3 RID: 6899 RVA: 0x00057F74 File Offset: 0x00056174
		internal static string CastBinder_ChildTypeIsNotEntity(object p0)
		{
			return TextRes.GetString("CastBinder_ChildTypeIsNotEntity", new object[] { p0 });
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06001AF4 RID: 6900 RVA: 0x00057F97 File Offset: 0x00056197
		internal static string CastBinder_EnumOnlyCastToOrFromString
		{
			get
			{
				return TextRes.GetString("CastBinder_EnumOnlyCastToOrFromString");
			}
		}

		// Token: 0x06001AF5 RID: 6901 RVA: 0x00057FA4 File Offset: 0x000561A4
		internal static string Binder_IsNotValidEnumConstant(object p0)
		{
			return TextRes.GetString("Binder_IsNotValidEnumConstant", new object[] { p0 });
		}

		// Token: 0x06001AF6 RID: 6902 RVA: 0x00057FC8 File Offset: 0x000561C8
		internal static string BatchReferenceSegment_InvalidContentID(object p0)
		{
			return TextRes.GetString("BatchReferenceSegment_InvalidContentID", new object[] { p0 });
		}

		// Token: 0x06001AF7 RID: 6903 RVA: 0x00057FEC File Offset: 0x000561EC
		internal static string SelectExpandBinder_UnknownPropertyType(object p0)
		{
			return TextRes.GetString("SelectExpandBinder_UnknownPropertyType", new object[] { p0 });
		}

		// Token: 0x06001AF8 RID: 6904 RVA: 0x00058010 File Offset: 0x00056210
		internal static string SelectionItemBinder_NoExpandForSelectedProperty(object p0)
		{
			return TextRes.GetString("SelectionItemBinder_NoExpandForSelectedProperty", new object[] { p0 });
		}

		// Token: 0x06001AF9 RID: 6905 RVA: 0x00058034 File Offset: 0x00056234
		internal static string SelectExpandPathBinder_FollowNonTypeSegment(object p0)
		{
			return TextRes.GetString("SelectExpandPathBinder_FollowNonTypeSegment", new object[] { p0 });
		}

		// Token: 0x06001AFA RID: 6906 RVA: 0x00058058 File Offset: 0x00056258
		internal static string SelectPropertyVisitor_SystemTokenInSelect(object p0)
		{
			return TextRes.GetString("SelectPropertyVisitor_SystemTokenInSelect", new object[] { p0 });
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06001AFB RID: 6907 RVA: 0x0005807B File Offset: 0x0005627B
		internal static string SelectPropertyVisitor_DisparateTypeSegmentsInSelectExpand
		{
			get
			{
				return TextRes.GetString("SelectPropertyVisitor_DisparateTypeSegmentsInSelectExpand");
			}
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06001AFC RID: 6908 RVA: 0x00058087 File Offset: 0x00056287
		internal static string SelectBinder_MultiLevelPathInSelect
		{
			get
			{
				return TextRes.GetString("SelectBinder_MultiLevelPathInSelect");
			}
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06001AFD RID: 6909 RVA: 0x00058093 File Offset: 0x00056293
		internal static string ExpandItemBinder_TraversingANonNormalizedTree
		{
			get
			{
				return TextRes.GetString("ExpandItemBinder_TraversingANonNormalizedTree");
			}
		}

		// Token: 0x06001AFE RID: 6910 RVA: 0x000580A0 File Offset: 0x000562A0
		internal static string ExpandItemBinder_CannotFindType(object p0)
		{
			return TextRes.GetString("ExpandItemBinder_CannotFindType", new object[] { p0 });
		}

		// Token: 0x06001AFF RID: 6911 RVA: 0x000580C4 File Offset: 0x000562C4
		internal static string ExpandItemBinder_PropertyIsNotANavigationProperty(object p0, object p1)
		{
			return TextRes.GetString("ExpandItemBinder_PropertyIsNotANavigationProperty", new object[] { p0, p1 });
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06001B00 RID: 6912 RVA: 0x000580EB File Offset: 0x000562EB
		internal static string ExpandItemBinder_TypeSegmentNotFollowedByPath
		{
			get
			{
				return TextRes.GetString("ExpandItemBinder_TypeSegmentNotFollowedByPath");
			}
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06001B01 RID: 6913 RVA: 0x000580F7 File Offset: 0x000562F7
		internal static string ExpandItemBinder_PathTooDeep
		{
			get
			{
				return TextRes.GetString("ExpandItemBinder_PathTooDeep");
			}
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06001B02 RID: 6914 RVA: 0x00058103 File Offset: 0x00056303
		internal static string ExpandItemBinder_TraversingMultipleNavPropsInTheSamePath
		{
			get
			{
				return TextRes.GetString("ExpandItemBinder_TraversingMultipleNavPropsInTheSamePath");
			}
		}

		// Token: 0x06001B03 RID: 6915 RVA: 0x00058110 File Offset: 0x00056310
		internal static string ExpandItemBinder_LevelsNotAllowedOnIncompatibleRelatedType(object p0, object p1, object p2)
		{
			return TextRes.GetString("ExpandItemBinder_LevelsNotAllowedOnIncompatibleRelatedType", new object[] { p0, p1, p2 });
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06001B04 RID: 6916 RVA: 0x0005813B File Offset: 0x0005633B
		internal static string Nodes_CollectionNavigationNode_MustHaveSingleMultiplicity
		{
			get
			{
				return TextRes.GetString("Nodes_CollectionNavigationNode_MustHaveSingleMultiplicity");
			}
		}

		// Token: 0x06001B05 RID: 6917 RVA: 0x00058148 File Offset: 0x00056348
		internal static string Nodes_NonentityParameterQueryNodeWithEntityType(object p0)
		{
			return TextRes.GetString("Nodes_NonentityParameterQueryNodeWithEntityType", new object[] { p0 });
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x06001B06 RID: 6918 RVA: 0x0005816B File Offset: 0x0005636B
		internal static string Nodes_CollectionNavigationNode_MustHaveManyMultiplicity
		{
			get
			{
				return TextRes.GetString("Nodes_CollectionNavigationNode_MustHaveManyMultiplicity");
			}
		}

		// Token: 0x06001B07 RID: 6919 RVA: 0x00058178 File Offset: 0x00056378
		internal static string Nodes_PropertyAccessShouldBeNonEntityProperty(object p0)
		{
			return TextRes.GetString("Nodes_PropertyAccessShouldBeNonEntityProperty", new object[] { p0 });
		}

		// Token: 0x06001B08 RID: 6920 RVA: 0x0005819C File Offset: 0x0005639C
		internal static string Nodes_PropertyAccessTypeShouldNotBeCollection(object p0)
		{
			return TextRes.GetString("Nodes_PropertyAccessTypeShouldNotBeCollection", new object[] { p0 });
		}

		// Token: 0x06001B09 RID: 6921 RVA: 0x000581C0 File Offset: 0x000563C0
		internal static string Nodes_PropertyAccessTypeMustBeCollection(object p0)
		{
			return TextRes.GetString("Nodes_PropertyAccessTypeMustBeCollection", new object[] { p0 });
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x06001B0A RID: 6922 RVA: 0x000581E3 File Offset: 0x000563E3
		internal static string Nodes_NonStaticEntitySetExpressionsAreNotSupportedInThisRelease
		{
			get
			{
				return TextRes.GetString("Nodes_NonStaticEntitySetExpressionsAreNotSupportedInThisRelease");
			}
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x06001B0B RID: 6923 RVA: 0x000581EF File Offset: 0x000563EF
		internal static string Nodes_CollectionFunctionCallNode_ItemTypeMustBePrimitiveOrComplexOrEnum
		{
			get
			{
				return TextRes.GetString("Nodes_CollectionFunctionCallNode_ItemTypeMustBePrimitiveOrComplexOrEnum");
			}
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06001B0C RID: 6924 RVA: 0x000581FB File Offset: 0x000563FB
		internal static string Nodes_EntityCollectionFunctionCallNode_ItemTypeMustBeAnEntity
		{
			get
			{
				return TextRes.GetString("Nodes_EntityCollectionFunctionCallNode_ItemTypeMustBeAnEntity");
			}
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06001B0D RID: 6925 RVA: 0x00058207 File Offset: 0x00056407
		internal static string Nodes_SingleValueFunctionCallNode_ItemTypeMustBePrimitiveOrComplexOrEnum
		{
			get
			{
				return TextRes.GetString("Nodes_SingleValueFunctionCallNode_ItemTypeMustBePrimitiveOrComplexOrEnum");
			}
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06001B0E RID: 6926 RVA: 0x00058213 File Offset: 0x00056413
		internal static string ExpandTreeNormalizer_NonPathInPropertyChain
		{
			get
			{
				return TextRes.GetString("ExpandTreeNormalizer_NonPathInPropertyChain");
			}
		}

		// Token: 0x06001B0F RID: 6927 RVA: 0x00058220 File Offset: 0x00056420
		internal static string UriExpandParser_TermIsNotValidForStar(object p0)
		{
			return TextRes.GetString("UriExpandParser_TermIsNotValidForStar", new object[] { p0 });
		}

		// Token: 0x06001B10 RID: 6928 RVA: 0x00058244 File Offset: 0x00056444
		internal static string UriExpandParser_TermIsNotValidForStarRef(object p0)
		{
			return TextRes.GetString("UriExpandParser_TermIsNotValidForStarRef", new object[] { p0 });
		}

		// Token: 0x06001B11 RID: 6929 RVA: 0x00058268 File Offset: 0x00056468
		internal static string UriExpandParser_ParentEntityIsNull(object p0)
		{
			return TextRes.GetString("UriExpandParser_ParentEntityIsNull", new object[] { p0 });
		}

		// Token: 0x06001B12 RID: 6930 RVA: 0x0005828C File Offset: 0x0005648C
		internal static string UriExpandParser_TermWithMultipleStarNotAllowed(object p0)
		{
			return TextRes.GetString("UriExpandParser_TermWithMultipleStarNotAllowed", new object[] { p0 });
		}

		// Token: 0x06001B13 RID: 6931 RVA: 0x000582B0 File Offset: 0x000564B0
		internal static string UriSelectParser_TermIsNotValid(object p0)
		{
			return TextRes.GetString("UriSelectParser_TermIsNotValid", new object[] { p0 });
		}

		// Token: 0x06001B14 RID: 6932 RVA: 0x000582D4 File Offset: 0x000564D4
		internal static string UriSelectParser_InvalidTopOption(object p0)
		{
			return TextRes.GetString("UriSelectParser_InvalidTopOption", new object[] { p0 });
		}

		// Token: 0x06001B15 RID: 6933 RVA: 0x000582F8 File Offset: 0x000564F8
		internal static string UriSelectParser_InvalidSkipOption(object p0)
		{
			return TextRes.GetString("UriSelectParser_InvalidSkipOption", new object[] { p0 });
		}

		// Token: 0x06001B16 RID: 6934 RVA: 0x0005831C File Offset: 0x0005651C
		internal static string UriSelectParser_InvalidCountOption(object p0)
		{
			return TextRes.GetString("UriSelectParser_InvalidCountOption", new object[] { p0 });
		}

		// Token: 0x06001B17 RID: 6935 RVA: 0x00058340 File Offset: 0x00056540
		internal static string UriSelectParser_InvalidLevelsOption(object p0)
		{
			return TextRes.GetString("UriSelectParser_InvalidLevelsOption", new object[] { p0 });
		}

		// Token: 0x06001B18 RID: 6936 RVA: 0x00058364 File Offset: 0x00056564
		internal static string UriSelectParser_SystemTokenInSelectExpand(object p0, object p1)
		{
			return TextRes.GetString("UriSelectParser_SystemTokenInSelectExpand", new object[] { p0, p1 });
		}

		// Token: 0x06001B19 RID: 6937 RVA: 0x0005838C File Offset: 0x0005658C
		internal static string UriParser_MissingExpandOption(object p0)
		{
			return TextRes.GetString("UriParser_MissingExpandOption", new object[] { p0 });
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x06001B1A RID: 6938 RVA: 0x000583AF File Offset: 0x000565AF
		internal static string UriParser_FullUriMustBeRelative
		{
			get
			{
				return TextRes.GetString("UriParser_FullUriMustBeRelative");
			}
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06001B1B RID: 6939 RVA: 0x000583BB File Offset: 0x000565BB
		internal static string UriParser_NeedServiceRootForThisOverload
		{
			get
			{
				return TextRes.GetString("UriParser_NeedServiceRootForThisOverload");
			}
		}

		// Token: 0x06001B1C RID: 6940 RVA: 0x000583C8 File Offset: 0x000565C8
		internal static string UriParser_UriMustBeAbsolute(object p0)
		{
			return TextRes.GetString("UriParser_UriMustBeAbsolute", new object[] { p0 });
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06001B1D RID: 6941 RVA: 0x000583EB File Offset: 0x000565EB
		internal static string UriParser_NegativeLimit
		{
			get
			{
				return TextRes.GetString("UriParser_NegativeLimit");
			}
		}

		// Token: 0x06001B1E RID: 6942 RVA: 0x000583F8 File Offset: 0x000565F8
		internal static string UriParser_ExpandCountExceeded(object p0, object p1)
		{
			return TextRes.GetString("UriParser_ExpandCountExceeded", new object[] { p0, p1 });
		}

		// Token: 0x06001B1F RID: 6943 RVA: 0x00058420 File Offset: 0x00056620
		internal static string UriParser_ExpandDepthExceeded(object p0, object p1)
		{
			return TextRes.GetString("UriParser_ExpandDepthExceeded", new object[] { p0, p1 });
		}

		// Token: 0x06001B20 RID: 6944 RVA: 0x00058448 File Offset: 0x00056648
		internal static string UriParser_TypeInvalidForSelectExpand(object p0)
		{
			return TextRes.GetString("UriParser_TypeInvalidForSelectExpand", new object[] { p0 });
		}

		// Token: 0x06001B21 RID: 6945 RVA: 0x0005846C File Offset: 0x0005666C
		internal static string UriParser_ContextHandlerCanNotBeNull(object p0)
		{
			return TextRes.GetString("UriParser_ContextHandlerCanNotBeNull", new object[] { p0 });
		}

		// Token: 0x06001B22 RID: 6946 RVA: 0x00058490 File Offset: 0x00056690
		internal static string UriParserMetadata_MultipleMatchingPropertiesFound(object p0, object p1)
		{
			return TextRes.GetString("UriParserMetadata_MultipleMatchingPropertiesFound", new object[] { p0, p1 });
		}

		// Token: 0x06001B23 RID: 6947 RVA: 0x000584B8 File Offset: 0x000566B8
		internal static string UriParserMetadata_MultipleMatchingNavigationSourcesFound(object p0)
		{
			return TextRes.GetString("UriParserMetadata_MultipleMatchingNavigationSourcesFound", new object[] { p0 });
		}

		// Token: 0x06001B24 RID: 6948 RVA: 0x000584DC File Offset: 0x000566DC
		internal static string UriParserMetadata_MultipleMatchingTypesFound(object p0)
		{
			return TextRes.GetString("UriParserMetadata_MultipleMatchingTypesFound", new object[] { p0 });
		}

		// Token: 0x06001B25 RID: 6949 RVA: 0x00058500 File Offset: 0x00056700
		internal static string UriParserMetadata_MultipleMatchingKeysFound(object p0)
		{
			return TextRes.GetString("UriParserMetadata_MultipleMatchingKeysFound", new object[] { p0 });
		}

		// Token: 0x06001B26 RID: 6950 RVA: 0x00058524 File Offset: 0x00056724
		internal static string UriParserMetadata_MultipleMatchingParametersFound(object p0)
		{
			return TextRes.GetString("UriParserMetadata_MultipleMatchingParametersFound", new object[] { p0 });
		}

		// Token: 0x06001B27 RID: 6951 RVA: 0x00058548 File Offset: 0x00056748
		internal static string PathParser_EntityReferenceNotSupported(object p0)
		{
			return TextRes.GetString("PathParser_EntityReferenceNotSupported", new object[] { p0 });
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06001B28 RID: 6952 RVA: 0x0005856B File Offset: 0x0005676B
		internal static string PathParser_CannotUseValueOnCollection
		{
			get
			{
				return TextRes.GetString("PathParser_CannotUseValueOnCollection");
			}
		}

		// Token: 0x06001B29 RID: 6953 RVA: 0x00058578 File Offset: 0x00056778
		internal static string PathParser_TypeMustBeRelatedToSet(object p0, object p1, object p2)
		{
			return TextRes.GetString("PathParser_TypeMustBeRelatedToSet", new object[] { p0, p1, p2 });
		}

		// Token: 0x06001B2A RID: 6954 RVA: 0x000585A4 File Offset: 0x000567A4
		internal static string PathParser_TypeCastOnlyAllowedAfterStructuralCollection(object p0)
		{
			return TextRes.GetString("PathParser_TypeCastOnlyAllowedAfterStructuralCollection", new object[] { p0 });
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06001B2B RID: 6955 RVA: 0x000585C7 File Offset: 0x000567C7
		internal static string ODataFeed_MustNotContainBothNextPageLinkAndDeltaLink
		{
			get
			{
				return TextRes.GetString("ODataFeed_MustNotContainBothNextPageLinkAndDeltaLink");
			}
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x06001B2C RID: 6956 RVA: 0x000585D3 File Offset: 0x000567D3
		internal static string ODataExpandPath_OnlyLastSegmentMustBeNavigationProperty
		{
			get
			{
				return TextRes.GetString("ODataExpandPath_OnlyLastSegmentMustBeNavigationProperty");
			}
		}

		// Token: 0x06001B2D RID: 6957 RVA: 0x000585E0 File Offset: 0x000567E0
		internal static string ODataExpandPath_InvalidExpandPathSegment(object p0)
		{
			return TextRes.GetString("ODataExpandPath_InvalidExpandPathSegment", new object[] { p0 });
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x06001B2E RID: 6958 RVA: 0x00058603 File Offset: 0x00056803
		internal static string ODataSelectPath_CannotEndInTypeSegment
		{
			get
			{
				return TextRes.GetString("ODataSelectPath_CannotEndInTypeSegment");
			}
		}

		// Token: 0x06001B2F RID: 6959 RVA: 0x00058610 File Offset: 0x00056810
		internal static string ODataSelectPath_InvalidSelectPathSegmentType(object p0)
		{
			return TextRes.GetString("ODataSelectPath_InvalidSelectPathSegmentType", new object[] { p0 });
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06001B30 RID: 6960 RVA: 0x00058633 File Offset: 0x00056833
		internal static string ODataSelectPath_OperationSegmentCanOnlyBeLastSegment
		{
			get
			{
				return TextRes.GetString("ODataSelectPath_OperationSegmentCanOnlyBeLastSegment");
			}
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06001B31 RID: 6961 RVA: 0x0005863F File Offset: 0x0005683F
		internal static string ODataSelectPath_NavPropSegmentCanOnlyBeLastSegment
		{
			get
			{
				return TextRes.GetString("ODataSelectPath_NavPropSegmentCanOnlyBeLastSegment");
			}
		}

		// Token: 0x06001B32 RID: 6962 RVA: 0x0005864C File Offset: 0x0005684C
		internal static string RequestUriProcessor_TargetEntitySetNotFound(object p0)
		{
			return TextRes.GetString("RequestUriProcessor_TargetEntitySetNotFound", new object[] { p0 });
		}

		// Token: 0x06001B33 RID: 6963 RVA: 0x00058670 File Offset: 0x00056870
		internal static string RequestUriProcessor_FoundInvalidFunctionImport(object p0)
		{
			return TextRes.GetString("RequestUriProcessor_FoundInvalidFunctionImport", new object[] { p0 });
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06001B34 RID: 6964 RVA: 0x00058693 File Offset: 0x00056893
		internal static string OperationSegment_ReturnTypeForMultipleOverloads
		{
			get
			{
				return TextRes.GetString("OperationSegment_ReturnTypeForMultipleOverloads");
			}
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06001B35 RID: 6965 RVA: 0x0005869F File Offset: 0x0005689F
		internal static string OperationSegment_CannotReturnNull
		{
			get
			{
				return TextRes.GetString("OperationSegment_CannotReturnNull");
			}
		}

		// Token: 0x06001B36 RID: 6966 RVA: 0x000586AC File Offset: 0x000568AC
		internal static string FunctionOverloadResolver_NoSingleMatchFound(object p0, object p1)
		{
			return TextRes.GetString("FunctionOverloadResolver_NoSingleMatchFound", new object[] { p0, p1 });
		}

		// Token: 0x06001B37 RID: 6967 RVA: 0x000586D4 File Offset: 0x000568D4
		internal static string FunctionOverloadResolver_MultipleActionOverloads(object p0)
		{
			return TextRes.GetString("FunctionOverloadResolver_MultipleActionOverloads", new object[] { p0 });
		}

		// Token: 0x06001B38 RID: 6968 RVA: 0x000586F8 File Offset: 0x000568F8
		internal static string FunctionOverloadResolver_MultipleActionImportOverloads(object p0)
		{
			return TextRes.GetString("FunctionOverloadResolver_MultipleActionImportOverloads", new object[] { p0 });
		}

		// Token: 0x06001B39 RID: 6969 RVA: 0x0005871C File Offset: 0x0005691C
		internal static string FunctionOverloadResolver_MultipleOperationImportOverloads(object p0)
		{
			return TextRes.GetString("FunctionOverloadResolver_MultipleOperationImportOverloads", new object[] { p0 });
		}

		// Token: 0x06001B3A RID: 6970 RVA: 0x00058740 File Offset: 0x00056940
		internal static string FunctionOverloadResolver_MultipleOperationOverloads(object p0)
		{
			return TextRes.GetString("FunctionOverloadResolver_MultipleOperationOverloads", new object[] { p0 });
		}

		// Token: 0x06001B3B RID: 6971 RVA: 0x00058764 File Offset: 0x00056964
		internal static string FunctionOverloadResolver_FoundInvalidOperation(object p0)
		{
			return TextRes.GetString("FunctionOverloadResolver_FoundInvalidOperation", new object[] { p0 });
		}

		// Token: 0x06001B3C RID: 6972 RVA: 0x00058788 File Offset: 0x00056988
		internal static string FunctionOverloadResolver_FoundInvalidOperationImport(object p0)
		{
			return TextRes.GetString("FunctionOverloadResolver_FoundInvalidOperationImport", new object[] { p0 });
		}

		// Token: 0x06001B3D RID: 6973 RVA: 0x000587AC File Offset: 0x000569AC
		internal static string CustomUriFunctions_AddCustomUriFunction_BuiltInExistsNotAddingAsOverload(object p0)
		{
			return TextRes.GetString("CustomUriFunctions_AddCustomUriFunction_BuiltInExistsNotAddingAsOverload", new object[] { p0 });
		}

		// Token: 0x06001B3E RID: 6974 RVA: 0x000587D0 File Offset: 0x000569D0
		internal static string CustomUriFunctions_AddCustomUriFunction_BuiltInExistsFullSignature(object p0)
		{
			return TextRes.GetString("CustomUriFunctions_AddCustomUriFunction_BuiltInExistsFullSignature", new object[] { p0 });
		}

		// Token: 0x06001B3F RID: 6975 RVA: 0x000587F4 File Offset: 0x000569F4
		internal static string CustomUriFunctions_AddCustomUriFunction_CustomFunctionOverloadExists(object p0)
		{
			return TextRes.GetString("CustomUriFunctions_AddCustomUriFunction_CustomFunctionOverloadExists", new object[] { p0 });
		}

		// Token: 0x06001B40 RID: 6976 RVA: 0x00058818 File Offset: 0x00056A18
		internal static string RequestUriProcessor_InvalidValueForEntitySegment(object p0)
		{
			return TextRes.GetString("RequestUriProcessor_InvalidValueForEntitySegment", new object[] { p0 });
		}

		// Token: 0x06001B41 RID: 6977 RVA: 0x0005883C File Offset: 0x00056A3C
		internal static string RequestUriProcessor_InvalidValueForKeySegment(object p0)
		{
			return TextRes.GetString("RequestUriProcessor_InvalidValueForKeySegment", new object[] { p0 });
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06001B42 RID: 6978 RVA: 0x0005885F File Offset: 0x00056A5F
		internal static string RequestUriProcessor_EmptySegmentInRequestUrl
		{
			get
			{
				return TextRes.GetString("RequestUriProcessor_EmptySegmentInRequestUrl");
			}
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06001B43 RID: 6979 RVA: 0x0005886B File Offset: 0x00056A6B
		internal static string RequestUriProcessor_SyntaxError
		{
			get
			{
				return TextRes.GetString("RequestUriProcessor_SyntaxError");
			}
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06001B44 RID: 6980 RVA: 0x00058877 File Offset: 0x00056A77
		internal static string RequestUriProcessor_CountOnRoot
		{
			get
			{
				return TextRes.GetString("RequestUriProcessor_CountOnRoot");
			}
		}

		// Token: 0x06001B45 RID: 6981 RVA: 0x00058884 File Offset: 0x00056A84
		internal static string RequestUriProcessor_MustBeLeafSegment(object p0)
		{
			return TextRes.GetString("RequestUriProcessor_MustBeLeafSegment", new object[] { p0 });
		}

		// Token: 0x06001B46 RID: 6982 RVA: 0x000588A8 File Offset: 0x00056AA8
		internal static string RequestUriProcessor_LinkSegmentMustBeFollowedByEntitySegment(object p0, object p1)
		{
			return TextRes.GetString("RequestUriProcessor_LinkSegmentMustBeFollowedByEntitySegment", new object[] { p0, p1 });
		}

		// Token: 0x06001B47 RID: 6983 RVA: 0x000588D0 File Offset: 0x00056AD0
		internal static string RequestUriProcessor_MissingSegmentAfterLink(object p0)
		{
			return TextRes.GetString("RequestUriProcessor_MissingSegmentAfterLink", new object[] { p0 });
		}

		// Token: 0x06001B48 RID: 6984 RVA: 0x000588F4 File Offset: 0x00056AF4
		internal static string RequestUriProcessor_CountNotSupported(object p0)
		{
			return TextRes.GetString("RequestUriProcessor_CountNotSupported", new object[] { p0 });
		}

		// Token: 0x06001B49 RID: 6985 RVA: 0x00058918 File Offset: 0x00056B18
		internal static string RequestUriProcessor_CannotQueryCollections(object p0)
		{
			return TextRes.GetString("RequestUriProcessor_CannotQueryCollections", new object[] { p0 });
		}

		// Token: 0x06001B4A RID: 6986 RVA: 0x0005893C File Offset: 0x00056B3C
		internal static string RequestUriProcessor_SegmentDoesNotSupportKeyPredicates(object p0)
		{
			return TextRes.GetString("RequestUriProcessor_SegmentDoesNotSupportKeyPredicates", new object[] { p0 });
		}

		// Token: 0x06001B4B RID: 6987 RVA: 0x00058960 File Offset: 0x00056B60
		internal static string RequestUriProcessor_ValueSegmentAfterScalarPropertySegment(object p0, object p1)
		{
			return TextRes.GetString("RequestUriProcessor_ValueSegmentAfterScalarPropertySegment", new object[] { p0, p1 });
		}

		// Token: 0x06001B4C RID: 6988 RVA: 0x00058988 File Offset: 0x00056B88
		internal static string RequestUriProcessor_InvalidTypeIdentifier_UnrelatedType(object p0, object p1)
		{
			return TextRes.GetString("RequestUriProcessor_InvalidTypeIdentifier_UnrelatedType", new object[] { p0, p1 });
		}

		// Token: 0x06001B4D RID: 6989 RVA: 0x000589B0 File Offset: 0x00056BB0
		internal static string OpenNavigationPropertiesNotSupportedOnOpenTypes(object p0)
		{
			return TextRes.GetString("OpenNavigationPropertiesNotSupportedOnOpenTypes", new object[] { p0 });
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x06001B4E RID: 6990 RVA: 0x000589D3 File Offset: 0x00056BD3
		internal static string BadRequest_ResourceCanBeCrossReferencedOnlyForBindOperation
		{
			get
			{
				return TextRes.GetString("BadRequest_ResourceCanBeCrossReferencedOnlyForBindOperation");
			}
		}

		// Token: 0x06001B4F RID: 6991 RVA: 0x000589E0 File Offset: 0x00056BE0
		internal static string DataServiceConfiguration_ResponseVersionIsBiggerThanProtocolVersion(object p0, object p1)
		{
			return TextRes.GetString("DataServiceConfiguration_ResponseVersionIsBiggerThanProtocolVersion", new object[] { p0, p1 });
		}

		// Token: 0x06001B50 RID: 6992 RVA: 0x00058A08 File Offset: 0x00056C08
		internal static string BadRequest_KeyCountMismatch(object p0)
		{
			return TextRes.GetString("BadRequest_KeyCountMismatch", new object[] { p0 });
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06001B51 RID: 6993 RVA: 0x00058A2B File Offset: 0x00056C2B
		internal static string RequestUriProcessor_KeysMustBeNamed
		{
			get
			{
				return TextRes.GetString("RequestUriProcessor_KeysMustBeNamed");
			}
		}

		// Token: 0x06001B52 RID: 6994 RVA: 0x00058A38 File Offset: 0x00056C38
		internal static string RequestUriProcessor_ResourceNotFound(object p0)
		{
			return TextRes.GetString("RequestUriProcessor_ResourceNotFound", new object[] { p0 });
		}

		// Token: 0x06001B53 RID: 6995 RVA: 0x00058A5C File Offset: 0x00056C5C
		internal static string RequestUriProcessor_BatchedActionOnEntityCreatedInSameChangeset(object p0)
		{
			return TextRes.GetString("RequestUriProcessor_BatchedActionOnEntityCreatedInSameChangeset", new object[] { p0 });
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06001B54 RID: 6996 RVA: 0x00058A7F File Offset: 0x00056C7F
		internal static string RequestUriProcessor_Forbidden
		{
			get
			{
				return TextRes.GetString("RequestUriProcessor_Forbidden");
			}
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06001B55 RID: 6997 RVA: 0x00058A8B File Offset: 0x00056C8B
		internal static string RequestUriProcessor_OperationSegmentBoundToANonEntityType
		{
			get
			{
				return TextRes.GetString("RequestUriProcessor_OperationSegmentBoundToANonEntityType");
			}
		}

		// Token: 0x06001B56 RID: 6998 RVA: 0x00058A98 File Offset: 0x00056C98
		internal static string General_InternalError(object p0)
		{
			return TextRes.GetString("General_InternalError", new object[] { p0 });
		}

		// Token: 0x06001B57 RID: 6999 RVA: 0x00058ABC File Offset: 0x00056CBC
		internal static string ExceptionUtils_CheckIntegerNotNegative(object p0)
		{
			return TextRes.GetString("ExceptionUtils_CheckIntegerNotNegative", new object[] { p0 });
		}

		// Token: 0x06001B58 RID: 7000 RVA: 0x00058AE0 File Offset: 0x00056CE0
		internal static string ExceptionUtils_CheckIntegerPositive(object p0)
		{
			return TextRes.GetString("ExceptionUtils_CheckIntegerPositive", new object[] { p0 });
		}

		// Token: 0x06001B59 RID: 7001 RVA: 0x00058B04 File Offset: 0x00056D04
		internal static string ExceptionUtils_CheckLongPositive(object p0)
		{
			return TextRes.GetString("ExceptionUtils_CheckLongPositive", new object[] { p0 });
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06001B5A RID: 7002 RVA: 0x00058B27 File Offset: 0x00056D27
		internal static string ExceptionUtils_ArgumentStringNullOrEmpty
		{
			get
			{
				return TextRes.GetString("ExceptionUtils_ArgumentStringNullOrEmpty");
			}
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06001B5B RID: 7003 RVA: 0x00058B33 File Offset: 0x00056D33
		internal static string ExpressionToken_OnlyRefAllowWithStarInExpand
		{
			get
			{
				return TextRes.GetString("ExpressionToken_OnlyRefAllowWithStarInExpand");
			}
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06001B5C RID: 7004 RVA: 0x00058B3F File Offset: 0x00056D3F
		internal static string ExpressionToken_NoPropAllowedAfterRef
		{
			get
			{
				return TextRes.GetString("ExpressionToken_NoPropAllowedAfterRef");
			}
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06001B5D RID: 7005 RVA: 0x00058B4B File Offset: 0x00056D4B
		internal static string ExpressionToken_NoSegmentAllowedBeforeStarInExpand
		{
			get
			{
				return TextRes.GetString("ExpressionToken_NoSegmentAllowedBeforeStarInExpand");
			}
		}

		// Token: 0x06001B5E RID: 7006 RVA: 0x00058B58 File Offset: 0x00056D58
		internal static string ExpressionToken_IdentifierExpected(object p0)
		{
			return TextRes.GetString("ExpressionToken_IdentifierExpected", new object[] { p0 });
		}

		// Token: 0x06001B5F RID: 7007 RVA: 0x00058B7C File Offset: 0x00056D7C
		internal static string ExpressionLexer_UnterminatedStringLiteral(object p0, object p1)
		{
			return TextRes.GetString("ExpressionLexer_UnterminatedStringLiteral", new object[] { p0, p1 });
		}

		// Token: 0x06001B60 RID: 7008 RVA: 0x00058BA4 File Offset: 0x00056DA4
		internal static string ExpressionLexer_InvalidCharacter(object p0, object p1, object p2)
		{
			return TextRes.GetString("ExpressionLexer_InvalidCharacter", new object[] { p0, p1, p2 });
		}

		// Token: 0x06001B61 RID: 7009 RVA: 0x00058BD0 File Offset: 0x00056DD0
		internal static string ExpressionLexer_SyntaxError(object p0, object p1)
		{
			return TextRes.GetString("ExpressionLexer_SyntaxError", new object[] { p0, p1 });
		}

		// Token: 0x06001B62 RID: 7010 RVA: 0x00058BF8 File Offset: 0x00056DF8
		internal static string ExpressionLexer_UnterminatedLiteral(object p0, object p1)
		{
			return TextRes.GetString("ExpressionLexer_UnterminatedLiteral", new object[] { p0, p1 });
		}

		// Token: 0x06001B63 RID: 7011 RVA: 0x00058C20 File Offset: 0x00056E20
		internal static string ExpressionLexer_DigitExpected(object p0, object p1)
		{
			return TextRes.GetString("ExpressionLexer_DigitExpected", new object[] { p0, p1 });
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06001B64 RID: 7012 RVA: 0x00058C47 File Offset: 0x00056E47
		internal static string ExpressionLexer_UnbalancedBracketExpression
		{
			get
			{
				return TextRes.GetString("ExpressionLexer_UnbalancedBracketExpression");
			}
		}

		// Token: 0x06001B65 RID: 7013 RVA: 0x00058C54 File Offset: 0x00056E54
		internal static string ExpressionLexer_InvalidNumericString(object p0)
		{
			return TextRes.GetString("ExpressionLexer_InvalidNumericString", new object[] { p0 });
		}

		// Token: 0x06001B66 RID: 7014 RVA: 0x00058C78 File Offset: 0x00056E78
		internal static string ExpressionLexer_InvalidEscapeSequence(object p0, object p1, object p2)
		{
			return TextRes.GetString("ExpressionLexer_InvalidEscapeSequence", new object[] { p0, p1, p2 });
		}

		// Token: 0x06001B67 RID: 7015 RVA: 0x00058CA4 File Offset: 0x00056EA4
		internal static string UriQueryExpressionParser_UnrecognizedLiteral(object p0, object p1, object p2, object p3)
		{
			return TextRes.GetString("UriQueryExpressionParser_UnrecognizedLiteral", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06001B68 RID: 7016 RVA: 0x00058CD4 File Offset: 0x00056ED4
		internal static string UriQueryExpressionParser_UnrecognizedLiteralWithReason(object p0, object p1, object p2, object p3, object p4)
		{
			return TextRes.GetString("UriQueryExpressionParser_UnrecognizedLiteralWithReason", new object[] { p0, p1, p2, p3, p4 });
		}

		// Token: 0x06001B69 RID: 7017 RVA: 0x00058D08 File Offset: 0x00056F08
		internal static string UriPrimitiveTypeParsers_FailedToParseTextToPrimitiveValue(object p0, object p1)
		{
			return TextRes.GetString("UriPrimitiveTypeParsers_FailedToParseTextToPrimitiveValue", new object[] { p0, p1 });
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06001B6A RID: 7018 RVA: 0x00058D2F File Offset: 0x00056F2F
		internal static string UriPrimitiveTypeParsers_FailedToParseStringToGeography
		{
			get
			{
				return TextRes.GetString("UriPrimitiveTypeParsers_FailedToParseStringToGeography");
			}
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06001B6B RID: 7019 RVA: 0x00058D3B File Offset: 0x00056F3B
		internal static string UriCustomTypeParsers_AddCustomUriTypeParserAlreadyExists
		{
			get
			{
				return TextRes.GetString("UriCustomTypeParsers_AddCustomUriTypeParserAlreadyExists");
			}
		}

		// Token: 0x06001B6C RID: 7020 RVA: 0x00058D48 File Offset: 0x00056F48
		internal static string UriCustomTypeParsers_AddCustomUriTypeParserEdmTypeExists(object p0)
		{
			return TextRes.GetString("UriCustomTypeParsers_AddCustomUriTypeParserEdmTypeExists", new object[] { p0 });
		}

		// Token: 0x06001B6D RID: 7021 RVA: 0x00058D6C File Offset: 0x00056F6C
		internal static string UriParserHelper_InvalidPrefixLiteral(object p0)
		{
			return TextRes.GetString("UriParserHelper_InvalidPrefixLiteral", new object[] { p0 });
		}

		// Token: 0x06001B6E RID: 7022 RVA: 0x00058D90 File Offset: 0x00056F90
		internal static string CustomUriTypePrefixLiterals_AddCustomUriTypePrefixLiteralAlreadyExists(object p0)
		{
			return TextRes.GetString("CustomUriTypePrefixLiterals_AddCustomUriTypePrefixLiteralAlreadyExists", new object[] { p0 });
		}

		// Token: 0x06001B6F RID: 7023 RVA: 0x00058DB4 File Offset: 0x00056FB4
		internal static string ValueParser_InvalidDuration(object p0)
		{
			return TextRes.GetString("ValueParser_InvalidDuration", new object[] { p0 });
		}

		// Token: 0x06001B70 RID: 7024 RVA: 0x00058DD8 File Offset: 0x00056FD8
		internal static string PlatformHelper_DateTimeOffsetMustContainTimeZone(object p0)
		{
			return TextRes.GetString("PlatformHelper_DateTimeOffsetMustContainTimeZone", new object[] { p0 });
		}

		// Token: 0x06001B71 RID: 7025 RVA: 0x00058DFC File Offset: 0x00056FFC
		internal static string JsonReader_UnexpectedComma(object p0)
		{
			return TextRes.GetString("JsonReader_UnexpectedComma", new object[] { p0 });
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06001B72 RID: 7026 RVA: 0x00058E1F File Offset: 0x0005701F
		internal static string JsonReader_MultipleTopLevelValues
		{
			get
			{
				return TextRes.GetString("JsonReader_MultipleTopLevelValues");
			}
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06001B73 RID: 7027 RVA: 0x00058E2B File Offset: 0x0005702B
		internal static string JsonReader_EndOfInputWithOpenScope
		{
			get
			{
				return TextRes.GetString("JsonReader_EndOfInputWithOpenScope");
			}
		}

		// Token: 0x06001B74 RID: 7028 RVA: 0x00058E38 File Offset: 0x00057038
		internal static string JsonReader_UnexpectedToken(object p0)
		{
			return TextRes.GetString("JsonReader_UnexpectedToken", new object[] { p0 });
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06001B75 RID: 7029 RVA: 0x00058E5B File Offset: 0x0005705B
		internal static string JsonReader_UnrecognizedToken
		{
			get
			{
				return TextRes.GetString("JsonReader_UnrecognizedToken");
			}
		}

		// Token: 0x06001B76 RID: 7030 RVA: 0x00058E68 File Offset: 0x00057068
		internal static string JsonReader_MissingColon(object p0)
		{
			return TextRes.GetString("JsonReader_MissingColon", new object[] { p0 });
		}

		// Token: 0x06001B77 RID: 7031 RVA: 0x00058E8C File Offset: 0x0005708C
		internal static string JsonReader_UnrecognizedEscapeSequence(object p0)
		{
			return TextRes.GetString("JsonReader_UnrecognizedEscapeSequence", new object[] { p0 });
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06001B78 RID: 7032 RVA: 0x00058EAF File Offset: 0x000570AF
		internal static string JsonReader_UnexpectedEndOfString
		{
			get
			{
				return TextRes.GetString("JsonReader_UnexpectedEndOfString");
			}
		}

		// Token: 0x06001B79 RID: 7033 RVA: 0x00058EBC File Offset: 0x000570BC
		internal static string JsonReader_InvalidNumberFormat(object p0)
		{
			return TextRes.GetString("JsonReader_InvalidNumberFormat", new object[] { p0 });
		}

		// Token: 0x06001B7A RID: 7034 RVA: 0x00058EE0 File Offset: 0x000570E0
		internal static string JsonReader_MissingComma(object p0)
		{
			return TextRes.GetString("JsonReader_MissingComma", new object[] { p0 });
		}

		// Token: 0x06001B7B RID: 7035 RVA: 0x00058F04 File Offset: 0x00057104
		internal static string JsonReader_InvalidPropertyNameOrUnexpectedComma(object p0)
		{
			return TextRes.GetString("JsonReader_InvalidPropertyNameOrUnexpectedComma", new object[] { p0 });
		}

		// Token: 0x06001B7C RID: 7036 RVA: 0x00058F28 File Offset: 0x00057128
		internal static string JsonReaderExtensions_UnexpectedNodeDetected(object p0, object p1)
		{
			return TextRes.GetString("JsonReaderExtensions_UnexpectedNodeDetected", new object[] { p0, p1 });
		}

		// Token: 0x06001B7D RID: 7037 RVA: 0x00058F50 File Offset: 0x00057150
		internal static string JsonReaderExtensions_UnexpectedNodeDetectedWithPropertyName(object p0, object p1, object p2)
		{
			return TextRes.GetString("JsonReaderExtensions_UnexpectedNodeDetectedWithPropertyName", new object[] { p0, p1, p2 });
		}

		// Token: 0x06001B7E RID: 7038 RVA: 0x00058F7C File Offset: 0x0005717C
		internal static string JsonReaderExtensions_CannotReadPropertyValueAsString(object p0, object p1)
		{
			return TextRes.GetString("JsonReaderExtensions_CannotReadPropertyValueAsString", new object[] { p0, p1 });
		}

		// Token: 0x06001B7F RID: 7039 RVA: 0x00058FA4 File Offset: 0x000571A4
		internal static string JsonReaderExtensions_CannotReadValueAsString(object p0)
		{
			return TextRes.GetString("JsonReaderExtensions_CannotReadValueAsString", new object[] { p0 });
		}

		// Token: 0x06001B80 RID: 7040 RVA: 0x00058FC8 File Offset: 0x000571C8
		internal static string JsonReaderExtensions_CannotReadValueAsDouble(object p0)
		{
			return TextRes.GetString("JsonReaderExtensions_CannotReadValueAsDouble", new object[] { p0 });
		}

		// Token: 0x06001B81 RID: 7041 RVA: 0x00058FEC File Offset: 0x000571EC
		internal static string JsonReaderExtensions_UnexpectedInstanceAnnotationName(object p0)
		{
			return TextRes.GetString("JsonReaderExtensions_UnexpectedInstanceAnnotationName", new object[] { p0 });
		}
	}
}
