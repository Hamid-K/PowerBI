using System;

namespace Microsoft.Data.OData
{
	// Token: 0x020002B1 RID: 689
	internal static class Strings
	{
		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06001632 RID: 5682 RVA: 0x0004F5D7 File Offset: 0x0004D7D7
		internal static string ExceptionUtils_ArgumentStringEmpty
		{
			get
			{
				return TextRes.GetString("ExceptionUtils_ArgumentStringEmpty");
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06001633 RID: 5683 RVA: 0x0004F5E3 File Offset: 0x0004D7E3
		internal static string ODataRequestMessage_AsyncNotAvailable
		{
			get
			{
				return TextRes.GetString("ODataRequestMessage_AsyncNotAvailable");
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06001634 RID: 5684 RVA: 0x0004F5EF File Offset: 0x0004D7EF
		internal static string ODataRequestMessage_StreamTaskIsNull
		{
			get
			{
				return TextRes.GetString("ODataRequestMessage_StreamTaskIsNull");
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06001635 RID: 5685 RVA: 0x0004F5FB File Offset: 0x0004D7FB
		internal static string ODataRequestMessage_MessageStreamIsNull
		{
			get
			{
				return TextRes.GetString("ODataRequestMessage_MessageStreamIsNull");
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06001636 RID: 5686 RVA: 0x0004F607 File Offset: 0x0004D807
		internal static string ODataResponseMessage_AsyncNotAvailable
		{
			get
			{
				return TextRes.GetString("ODataResponseMessage_AsyncNotAvailable");
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06001637 RID: 5687 RVA: 0x0004F613 File Offset: 0x0004D813
		internal static string ODataResponseMessage_StreamTaskIsNull
		{
			get
			{
				return TextRes.GetString("ODataResponseMessage_StreamTaskIsNull");
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06001638 RID: 5688 RVA: 0x0004F61F File Offset: 0x0004D81F
		internal static string ODataResponseMessage_MessageStreamIsNull
		{
			get
			{
				return TextRes.GetString("ODataResponseMessage_MessageStreamIsNull");
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06001639 RID: 5689 RVA: 0x0004F62B File Offset: 0x0004D82B
		internal static string AsyncBufferedStream_WriterDisposedWithoutFlush
		{
			get
			{
				return TextRes.GetString("AsyncBufferedStream_WriterDisposedWithoutFlush");
			}
		}

		// Token: 0x0600163A RID: 5690 RVA: 0x0004F638 File Offset: 0x0004D838
		internal static string ODataOutputContext_UnsupportedPayloadKindForFormat(object p0, object p1)
		{
			return TextRes.GetString("ODataOutputContext_UnsupportedPayloadKindForFormat", new object[] { p0, p1 });
		}

		// Token: 0x0600163B RID: 5691 RVA: 0x0004F660 File Offset: 0x0004D860
		internal static string ODataOutputContext_CustomInstanceAnnotationsNotSupportedForFormat(object p0)
		{
			return TextRes.GetString("ODataOutputContext_CustomInstanceAnnotationsNotSupportedForFormat", new object[] { p0 });
		}

		// Token: 0x0600163C RID: 5692 RVA: 0x0004F684 File Offset: 0x0004D884
		internal static string ODataInputContext_UnsupportedPayloadKindForFormat(object p0, object p1)
		{
			return TextRes.GetString("ODataInputContext_UnsupportedPayloadKindForFormat", new object[] { p0, p1 });
		}

		// Token: 0x0600163D RID: 5693 RVA: 0x0004F6AC File Offset: 0x0004D8AC
		internal static string ODataJsonLightSerializer_RelativeUriUsedWithoutMetadataDocumentUriOrMetadata(object p0)
		{
			return TextRes.GetString("ODataJsonLightSerializer_RelativeUriUsedWithoutMetadataDocumentUriOrMetadata", new object[] { p0 });
		}

		// Token: 0x0600163E RID: 5694 RVA: 0x0004F6D0 File Offset: 0x0004D8D0
		internal static string ODataWriter_RelativeUriUsedWithoutBaseUriSpecified(object p0)
		{
			return TextRes.GetString("ODataWriter_RelativeUriUsedWithoutBaseUriSpecified", new object[] { p0 });
		}

		// Token: 0x0600163F RID: 5695 RVA: 0x0004F6F4 File Offset: 0x0004D8F4
		internal static string ODataWriter_StreamPropertiesMustBePropertiesOfODataEntry(object p0)
		{
			return TextRes.GetString("ODataWriter_StreamPropertiesMustBePropertiesOfODataEntry", new object[] { p0 });
		}

		// Token: 0x06001640 RID: 5696 RVA: 0x0004F718 File Offset: 0x0004D918
		internal static string ODataWriterCore_InvalidStateTransition(object p0, object p1)
		{
			return TextRes.GetString("ODataWriterCore_InvalidStateTransition", new object[] { p0, p1 });
		}

		// Token: 0x06001641 RID: 5697 RVA: 0x0004F740 File Offset: 0x0004D940
		internal static string ODataWriterCore_InvalidTransitionFromStart(object p0, object p1)
		{
			return TextRes.GetString("ODataWriterCore_InvalidTransitionFromStart", new object[] { p0, p1 });
		}

		// Token: 0x06001642 RID: 5698 RVA: 0x0004F768 File Offset: 0x0004D968
		internal static string ODataWriterCore_InvalidTransitionFromEntry(object p0, object p1)
		{
			return TextRes.GetString("ODataWriterCore_InvalidTransitionFromEntry", new object[] { p0, p1 });
		}

		// Token: 0x06001643 RID: 5699 RVA: 0x0004F790 File Offset: 0x0004D990
		internal static string ODataWriterCore_InvalidTransitionFromNullEntry(object p0, object p1)
		{
			return TextRes.GetString("ODataWriterCore_InvalidTransitionFromNullEntry", new object[] { p0, p1 });
		}

		// Token: 0x06001644 RID: 5700 RVA: 0x0004F7B8 File Offset: 0x0004D9B8
		internal static string ODataWriterCore_InvalidTransitionFromFeed(object p0, object p1)
		{
			return TextRes.GetString("ODataWriterCore_InvalidTransitionFromFeed", new object[] { p0, p1 });
		}

		// Token: 0x06001645 RID: 5701 RVA: 0x0004F7E0 File Offset: 0x0004D9E0
		internal static string ODataWriterCore_InvalidTransitionFromExpandedLink(object p0, object p1)
		{
			return TextRes.GetString("ODataWriterCore_InvalidTransitionFromExpandedLink", new object[] { p0, p1 });
		}

		// Token: 0x06001646 RID: 5702 RVA: 0x0004F808 File Offset: 0x0004DA08
		internal static string ODataWriterCore_InvalidTransitionFromCompleted(object p0, object p1)
		{
			return TextRes.GetString("ODataWriterCore_InvalidTransitionFromCompleted", new object[] { p0, p1 });
		}

		// Token: 0x06001647 RID: 5703 RVA: 0x0004F830 File Offset: 0x0004DA30
		internal static string ODataWriterCore_InvalidTransitionFromError(object p0, object p1)
		{
			return TextRes.GetString("ODataWriterCore_InvalidTransitionFromError", new object[] { p0, p1 });
		}

		// Token: 0x06001648 RID: 5704 RVA: 0x0004F858 File Offset: 0x0004DA58
		internal static string ODataWriterCore_WriteEndCalledInInvalidState(object p0)
		{
			return TextRes.GetString("ODataWriterCore_WriteEndCalledInInvalidState", new object[] { p0 });
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06001649 RID: 5705 RVA: 0x0004F87B File Offset: 0x0004DA7B
		internal static string ODataWriterCore_OnlyTopLevelFeedsSupportInlineCount
		{
			get
			{
				return TextRes.GetString("ODataWriterCore_OnlyTopLevelFeedsSupportInlineCount");
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x0600164A RID: 5706 RVA: 0x0004F887 File Offset: 0x0004DA87
		internal static string ODataWriterCore_InlineCountInRequest
		{
			get
			{
				return TextRes.GetString("ODataWriterCore_InlineCountInRequest");
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x0600164B RID: 5707 RVA: 0x0004F893 File Offset: 0x0004DA93
		internal static string ODataWriterCore_CannotWriteTopLevelFeedWithEntryWriter
		{
			get
			{
				return TextRes.GetString("ODataWriterCore_CannotWriteTopLevelFeedWithEntryWriter");
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x0600164C RID: 5708 RVA: 0x0004F89F File Offset: 0x0004DA9F
		internal static string ODataWriterCore_CannotWriteTopLevelEntryWithFeedWriter
		{
			get
			{
				return TextRes.GetString("ODataWriterCore_CannotWriteTopLevelEntryWithFeedWriter");
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x0600164D RID: 5709 RVA: 0x0004F8AB File Offset: 0x0004DAAB
		internal static string ODataWriterCore_SyncCallOnAsyncWriter
		{
			get
			{
				return TextRes.GetString("ODataWriterCore_SyncCallOnAsyncWriter");
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x0600164E RID: 5710 RVA: 0x0004F8B7 File Offset: 0x0004DAB7
		internal static string ODataWriterCore_AsyncCallOnSyncWriter
		{
			get
			{
				return TextRes.GetString("ODataWriterCore_AsyncCallOnSyncWriter");
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x0600164F RID: 5711 RVA: 0x0004F8C3 File Offset: 0x0004DAC3
		internal static string ODataWriterCore_EntityReferenceLinkWithoutNavigationLink
		{
			get
			{
				return TextRes.GetString("ODataWriterCore_EntityReferenceLinkWithoutNavigationLink");
			}
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06001650 RID: 5712 RVA: 0x0004F8CF File Offset: 0x0004DACF
		internal static string ODataWriterCore_EntityReferenceLinkInResponse
		{
			get
			{
				return TextRes.GetString("ODataWriterCore_EntityReferenceLinkInResponse");
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06001651 RID: 5713 RVA: 0x0004F8DB File Offset: 0x0004DADB
		internal static string ODataWriterCore_DeferredLinkInRequest
		{
			get
			{
				return TextRes.GetString("ODataWriterCore_DeferredLinkInRequest");
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06001652 RID: 5714 RVA: 0x0004F8E7 File Offset: 0x0004DAE7
		internal static string ODataWriterCore_MultipleItemsInNavigationLinkContent
		{
			get
			{
				return TextRes.GetString("ODataWriterCore_MultipleItemsInNavigationLinkContent");
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06001653 RID: 5715 RVA: 0x0004F8F3 File Offset: 0x0004DAF3
		internal static string ODataWriterCore_DeltaLinkNotSupportedOnExpandedFeed
		{
			get
			{
				return TextRes.GetString("ODataWriterCore_DeltaLinkNotSupportedOnExpandedFeed");
			}
		}

		// Token: 0x06001654 RID: 5716 RVA: 0x0004F900 File Offset: 0x0004DB00
		internal static string DuplicatePropertyNamesChecker_DuplicatePropertyNamesNotAllowed(object p0)
		{
			return TextRes.GetString("DuplicatePropertyNamesChecker_DuplicatePropertyNamesNotAllowed", new object[] { p0 });
		}

		// Token: 0x06001655 RID: 5717 RVA: 0x0004F924 File Offset: 0x0004DB24
		internal static string DuplicatePropertyNamesChecker_MultipleLinksForSingleton(object p0)
		{
			return TextRes.GetString("DuplicatePropertyNamesChecker_MultipleLinksForSingleton", new object[] { p0 });
		}

		// Token: 0x06001656 RID: 5718 RVA: 0x0004F948 File Offset: 0x0004DB48
		internal static string DuplicatePropertyNamesChecker_DuplicateAnnotationNotAllowed(object p0)
		{
			return TextRes.GetString("DuplicatePropertyNamesChecker_DuplicateAnnotationNotAllowed", new object[] { p0 });
		}

		// Token: 0x06001657 RID: 5719 RVA: 0x0004F96C File Offset: 0x0004DB6C
		internal static string DuplicatePropertyNamesChecker_DuplicateAnnotationForPropertyNotAllowed(object p0, object p1)
		{
			return TextRes.GetString("DuplicatePropertyNamesChecker_DuplicateAnnotationForPropertyNotAllowed", new object[] { p0, p1 });
		}

		// Token: 0x06001658 RID: 5720 RVA: 0x0004F994 File Offset: 0x0004DB94
		internal static string DuplicatePropertyNamesChecker_DuplicateAnnotationForInstanceAnnotationNotAllowed(object p0, object p1)
		{
			return TextRes.GetString("DuplicatePropertyNamesChecker_DuplicateAnnotationForInstanceAnnotationNotAllowed", new object[] { p0, p1 });
		}

		// Token: 0x06001659 RID: 5721 RVA: 0x0004F9BC File Offset: 0x0004DBBC
		internal static string DuplicatePropertyNamesChecker_PropertyAnnotationAfterTheProperty(object p0, object p1)
		{
			return TextRes.GetString("DuplicatePropertyNamesChecker_PropertyAnnotationAfterTheProperty", new object[] { p0, p1 });
		}

		// Token: 0x0600165A RID: 5722 RVA: 0x0004F9E4 File Offset: 0x0004DBE4
		internal static string AtomValueUtils_CannotConvertValueToAtomPrimitive(object p0)
		{
			return TextRes.GetString("AtomValueUtils_CannotConvertValueToAtomPrimitive", new object[] { p0 });
		}

		// Token: 0x0600165B RID: 5723 RVA: 0x0004FA08 File Offset: 0x0004DC08
		internal static string ODataJsonWriter_UnsupportedValueType(object p0)
		{
			return TextRes.GetString("ODataJsonWriter_UnsupportedValueType", new object[] { p0 });
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x0600165C RID: 5724 RVA: 0x0004FA2B File Offset: 0x0004DC2B
		internal static string ODataException_GeneralError
		{
			get
			{
				return TextRes.GetString("ODataException_GeneralError");
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x0600165D RID: 5725 RVA: 0x0004FA37 File Offset: 0x0004DC37
		internal static string ODataErrorException_GeneralError
		{
			get
			{
				return TextRes.GetString("ODataErrorException_GeneralError");
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x0600165E RID: 5726 RVA: 0x0004FA43 File Offset: 0x0004DC43
		internal static string ODataUriParserException_GeneralError
		{
			get
			{
				return TextRes.GetString("ODataUriParserException_GeneralError");
			}
		}

		// Token: 0x0600165F RID: 5727 RVA: 0x0004FA50 File Offset: 0x0004DC50
		internal static string ODataVersionChecker_MaxProtocolVersionExceeded(object p0, object p1)
		{
			return TextRes.GetString("ODataVersionChecker_MaxProtocolVersionExceeded", new object[] { p0, p1 });
		}

		// Token: 0x06001660 RID: 5728 RVA: 0x0004FA78 File Offset: 0x0004DC78
		internal static string ODataVersionChecker_PropertyNotSupportedForODataVersionGreaterThanX(object p0, object p1)
		{
			return TextRes.GetString("ODataVersionChecker_PropertyNotSupportedForODataVersionGreaterThanX", new object[] { p0, p1 });
		}

		// Token: 0x06001661 RID: 5729 RVA: 0x0004FAA0 File Offset: 0x0004DCA0
		internal static string ODataVersionChecker_ParameterPayloadNotSupported(object p0)
		{
			return TextRes.GetString("ODataVersionChecker_ParameterPayloadNotSupported", new object[] { p0 });
		}

		// Token: 0x06001662 RID: 5730 RVA: 0x0004FAC4 File Offset: 0x0004DCC4
		internal static string ODataVersionChecker_AssociationLinksNotSupported(object p0)
		{
			return TextRes.GetString("ODataVersionChecker_AssociationLinksNotSupported", new object[] { p0 });
		}

		// Token: 0x06001663 RID: 5731 RVA: 0x0004FAE8 File Offset: 0x0004DCE8
		internal static string ODataVersionChecker_InlineCountNotSupported(object p0)
		{
			return TextRes.GetString("ODataVersionChecker_InlineCountNotSupported", new object[] { p0 });
		}

		// Token: 0x06001664 RID: 5732 RVA: 0x0004FB0C File Offset: 0x0004DD0C
		internal static string ODataVersionChecker_NextLinkNotSupported(object p0)
		{
			return TextRes.GetString("ODataVersionChecker_NextLinkNotSupported", new object[] { p0 });
		}

		// Token: 0x06001665 RID: 5733 RVA: 0x0004FB30 File Offset: 0x0004DD30
		internal static string ODataVersionChecker_DeltaLinkNotSupported(object p0)
		{
			return TextRes.GetString("ODataVersionChecker_DeltaLinkNotSupported", new object[] { p0 });
		}

		// Token: 0x06001666 RID: 5734 RVA: 0x0004FB54 File Offset: 0x0004DD54
		internal static string ODataVersionChecker_CollectionPropertiesNotSupported(object p0, object p1)
		{
			return TextRes.GetString("ODataVersionChecker_CollectionPropertiesNotSupported", new object[] { p0, p1 });
		}

		// Token: 0x06001667 RID: 5735 RVA: 0x0004FB7C File Offset: 0x0004DD7C
		internal static string ODataVersionChecker_CollectionNotSupported(object p0)
		{
			return TextRes.GetString("ODataVersionChecker_CollectionNotSupported", new object[] { p0 });
		}

		// Token: 0x06001668 RID: 5736 RVA: 0x0004FBA0 File Offset: 0x0004DDA0
		internal static string ODataVersionChecker_StreamPropertiesNotSupported(object p0)
		{
			return TextRes.GetString("ODataVersionChecker_StreamPropertiesNotSupported", new object[] { p0 });
		}

		// Token: 0x06001669 RID: 5737 RVA: 0x0004FBC4 File Offset: 0x0004DDC4
		internal static string ODataVersionChecker_EpmVersionNotSupported(object p0, object p1, object p2)
		{
			return TextRes.GetString("ODataVersionChecker_EpmVersionNotSupported", new object[] { p0, p1, p2 });
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x0600166A RID: 5738 RVA: 0x0004FBEF File Offset: 0x0004DDEF
		internal static string ODataVersionChecker_ProtocolVersion3IsNotSupported
		{
			get
			{
				return TextRes.GetString("ODataVersionChecker_ProtocolVersion3IsNotSupported");
			}
		}

		// Token: 0x0600166B RID: 5739 RVA: 0x0004FBFC File Offset: 0x0004DDFC
		internal static string ODataVersionChecker_GeographyAndGeometryNotSupported(object p0)
		{
			return TextRes.GetString("ODataVersionChecker_GeographyAndGeometryNotSupported", new object[] { p0 });
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x0600166C RID: 5740 RVA: 0x0004FC1F File Offset: 0x0004DE1F
		internal static string ODataAtomCollectionWriter_CollectionNameMustNotBeNull
		{
			get
			{
				return TextRes.GetString("ODataAtomCollectionWriter_CollectionNameMustNotBeNull");
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x0600166D RID: 5741 RVA: 0x0004FC2B File Offset: 0x0004DE2B
		internal static string ODataAtomWriter_StartEntryXmlCustomizationCallbackReturnedSameInstance
		{
			get
			{
				return TextRes.GetString("ODataAtomWriter_StartEntryXmlCustomizationCallbackReturnedSameInstance");
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x0600166E RID: 5742 RVA: 0x0004FC37 File Offset: 0x0004DE37
		internal static string ODataAtomWriterMetadataUtils_AuthorMetadataMustNotContainNull
		{
			get
			{
				return TextRes.GetString("ODataAtomWriterMetadataUtils_AuthorMetadataMustNotContainNull");
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x0600166F RID: 5743 RVA: 0x0004FC43 File Offset: 0x0004DE43
		internal static string ODataAtomWriterMetadataUtils_CategoryMetadataMustNotContainNull
		{
			get
			{
				return TextRes.GetString("ODataAtomWriterMetadataUtils_CategoryMetadataMustNotContainNull");
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06001670 RID: 5744 RVA: 0x0004FC4F File Offset: 0x0004DE4F
		internal static string ODataAtomWriterMetadataUtils_ContributorMetadataMustNotContainNull
		{
			get
			{
				return TextRes.GetString("ODataAtomWriterMetadataUtils_ContributorMetadataMustNotContainNull");
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06001671 RID: 5745 RVA: 0x0004FC5B File Offset: 0x0004DE5B
		internal static string ODataAtomWriterMetadataUtils_LinkMetadataMustNotContainNull
		{
			get
			{
				return TextRes.GetString("ODataAtomWriterMetadataUtils_LinkMetadataMustNotContainNull");
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06001672 RID: 5746 RVA: 0x0004FC67 File Offset: 0x0004DE67
		internal static string ODataAtomWriterMetadataUtils_LinkMustSpecifyHref
		{
			get
			{
				return TextRes.GetString("ODataAtomWriterMetadataUtils_LinkMustSpecifyHref");
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06001673 RID: 5747 RVA: 0x0004FC73 File Offset: 0x0004DE73
		internal static string ODataAtomWriterMetadataUtils_CategoryMustSpecifyTerm
		{
			get
			{
				return TextRes.GetString("ODataAtomWriterMetadataUtils_CategoryMustSpecifyTerm");
			}
		}

		// Token: 0x06001674 RID: 5748 RVA: 0x0004FC80 File Offset: 0x0004DE80
		internal static string ODataAtomWriterMetadataUtils_LinkHrefsMustMatch(object p0, object p1)
		{
			return TextRes.GetString("ODataAtomWriterMetadataUtils_LinkHrefsMustMatch", new object[] { p0, p1 });
		}

		// Token: 0x06001675 RID: 5749 RVA: 0x0004FCA8 File Offset: 0x0004DEA8
		internal static string ODataAtomWriterMetadataUtils_LinkTitlesMustMatch(object p0, object p1)
		{
			return TextRes.GetString("ODataAtomWriterMetadataUtils_LinkTitlesMustMatch", new object[] { p0, p1 });
		}

		// Token: 0x06001676 RID: 5750 RVA: 0x0004FCD0 File Offset: 0x0004DED0
		internal static string ODataAtomWriterMetadataUtils_LinkRelationsMustMatch(object p0, object p1)
		{
			return TextRes.GetString("ODataAtomWriterMetadataUtils_LinkRelationsMustMatch", new object[] { p0, p1 });
		}

		// Token: 0x06001677 RID: 5751 RVA: 0x0004FCF8 File Offset: 0x0004DEF8
		internal static string ODataAtomWriterMetadataUtils_LinkMediaTypesMustMatch(object p0, object p1)
		{
			return TextRes.GetString("ODataAtomWriterMetadataUtils_LinkMediaTypesMustMatch", new object[] { p0, p1 });
		}

		// Token: 0x06001678 RID: 5752 RVA: 0x0004FD20 File Offset: 0x0004DF20
		internal static string ODataAtomWriterMetadataUtils_InvalidAnnotationValue(object p0, object p1)
		{
			return TextRes.GetString("ODataAtomWriterMetadataUtils_InvalidAnnotationValue", new object[] { p0, p1 });
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06001679 RID: 5753 RVA: 0x0004FD47 File Offset: 0x0004DF47
		internal static string ODataAtomWriterMetadataUtils_CategoriesHrefWithOtherValues
		{
			get
			{
				return TextRes.GetString("ODataAtomWriterMetadataUtils_CategoriesHrefWithOtherValues");
			}
		}

		// Token: 0x0600167A RID: 5754 RVA: 0x0004FD54 File Offset: 0x0004DF54
		internal static string ODataAtomWriterMetadataUtils_CategoryTermsMustMatch(object p0, object p1)
		{
			return TextRes.GetString("ODataAtomWriterMetadataUtils_CategoryTermsMustMatch", new object[] { p0, p1 });
		}

		// Token: 0x0600167B RID: 5755 RVA: 0x0004FD7C File Offset: 0x0004DF7C
		internal static string ODataAtomWriterMetadataUtils_CategorySchemesMustMatch(object p0, object p1)
		{
			return TextRes.GetString("ODataAtomWriterMetadataUtils_CategorySchemesMustMatch", new object[] { p0, p1 });
		}

		// Token: 0x0600167C RID: 5756 RVA: 0x0004FDA4 File Offset: 0x0004DFA4
		internal static string ODataAtomMetadataEpmMerge_TextKindConflict(object p0, object p1, object p2)
		{
			return TextRes.GetString("ODataAtomMetadataEpmMerge_TextKindConflict", new object[] { p0, p1, p2 });
		}

		// Token: 0x0600167D RID: 5757 RVA: 0x0004FDD0 File Offset: 0x0004DFD0
		internal static string ODataAtomMetadataEpmMerge_TextValueConflict(object p0, object p1, object p2)
		{
			return TextRes.GetString("ODataAtomMetadataEpmMerge_TextValueConflict", new object[] { p0, p1, p2 });
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x0600167E RID: 5758 RVA: 0x0004FDFB File Offset: 0x0004DFFB
		internal static string ODataMessageWriter_WriterAlreadyUsed
		{
			get
			{
				return TextRes.GetString("ODataMessageWriter_WriterAlreadyUsed");
			}
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x0004FE08 File Offset: 0x0004E008
		internal static string ODataMessageWriter_InvalidContentTypeForWritingRawValue(object p0)
		{
			return TextRes.GetString("ODataMessageWriter_InvalidContentTypeForWritingRawValue", new object[] { p0 });
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06001680 RID: 5760 RVA: 0x0004FE2B File Offset: 0x0004E02B
		internal static string ODataMessageWriter_EntityReferenceLinksInRequestNotAllowed
		{
			get
			{
				return TextRes.GetString("ODataMessageWriter_EntityReferenceLinksInRequestNotAllowed");
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06001681 RID: 5761 RVA: 0x0004FE37 File Offset: 0x0004E037
		internal static string ODataMessageWriter_ErrorPayloadInRequest
		{
			get
			{
				return TextRes.GetString("ODataMessageWriter_ErrorPayloadInRequest");
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06001682 RID: 5762 RVA: 0x0004FE43 File Offset: 0x0004E043
		internal static string ODataMessageWriter_ServiceDocumentInRequest
		{
			get
			{
				return TextRes.GetString("ODataMessageWriter_ServiceDocumentInRequest");
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06001683 RID: 5763 RVA: 0x0004FE4F File Offset: 0x0004E04F
		internal static string ODataMessageWriter_MetadataDocumentInRequest
		{
			get
			{
				return TextRes.GetString("ODataMessageWriter_MetadataDocumentInRequest");
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06001684 RID: 5764 RVA: 0x0004FE5B File Offset: 0x0004E05B
		internal static string ODataMessageWriter_CannotWriteNullInRawFormat
		{
			get
			{
				return TextRes.GetString("ODataMessageWriter_CannotWriteNullInRawFormat");
			}
		}

		// Token: 0x06001685 RID: 5765 RVA: 0x0004FE68 File Offset: 0x0004E068
		internal static string ODataMessageWriter_CannotSetHeadersWithInvalidPayloadKind(object p0)
		{
			return TextRes.GetString("ODataMessageWriter_CannotSetHeadersWithInvalidPayloadKind", new object[] { p0 });
		}

		// Token: 0x06001686 RID: 5766 RVA: 0x0004FE8C File Offset: 0x0004E08C
		internal static string ODataMessageWriter_IncompatiblePayloadKinds(object p0, object p1)
		{
			return TextRes.GetString("ODataMessageWriter_IncompatiblePayloadKinds", new object[] { p0, p1 });
		}

		// Token: 0x06001687 RID: 5767 RVA: 0x0004FEB4 File Offset: 0x0004E0B4
		internal static string ODataMessageWriter_CannotWriteStreamPropertyAsTopLevelProperty(object p0)
		{
			return TextRes.GetString("ODataMessageWriter_CannotWriteStreamPropertyAsTopLevelProperty", new object[] { p0 });
		}

		// Token: 0x06001688 RID: 5768 RVA: 0x0004FED8 File Offset: 0x0004E0D8
		internal static string ODataMessageWriter_InvalidPropertyOwningType(object p0, object p1)
		{
			return TextRes.GetString("ODataMessageWriter_InvalidPropertyOwningType", new object[] { p0, p1 });
		}

		// Token: 0x06001689 RID: 5769 RVA: 0x0004FF00 File Offset: 0x0004E100
		internal static string ODataMessageWriter_InvalidPropertyProducingFunctionImport(object p0)
		{
			return TextRes.GetString("ODataMessageWriter_InvalidPropertyProducingFunctionImport", new object[] { p0 });
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x0600168A RID: 5770 RVA: 0x0004FF23 File Offset: 0x0004E123
		internal static string ODataMessageWriter_WriteErrorAlreadyCalled
		{
			get
			{
				return TextRes.GetString("ODataMessageWriter_WriteErrorAlreadyCalled");
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x0600168B RID: 5771 RVA: 0x0004FF2F File Offset: 0x0004E12F
		internal static string ODataMessageWriter_CannotWriteInStreamErrorForRawValues
		{
			get
			{
				return TextRes.GetString("ODataMessageWriter_CannotWriteInStreamErrorForRawValues");
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x0600168C RID: 5772 RVA: 0x0004FF3B File Offset: 0x0004E13B
		internal static string ODataMessageWriter_CannotWriteMetadataWithoutModel
		{
			get
			{
				return TextRes.GetString("ODataMessageWriter_CannotWriteMetadataWithoutModel");
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x0600168D RID: 5773 RVA: 0x0004FF47 File Offset: 0x0004E147
		internal static string ODataMessageWriter_CannotSpecifyFunctionImportWithoutModel
		{
			get
			{
				return TextRes.GetString("ODataMessageWriter_CannotSpecifyFunctionImportWithoutModel");
			}
		}

		// Token: 0x0600168E RID: 5774 RVA: 0x0004FF54 File Offset: 0x0004E154
		internal static string ODataMessageWriter_EntityReferenceLinksWithSingletonNavPropNotAllowed(object p0)
		{
			return TextRes.GetString("ODataMessageWriter_EntityReferenceLinksWithSingletonNavPropNotAllowed", new object[] { p0 });
		}

		// Token: 0x0600168F RID: 5775 RVA: 0x0004FF78 File Offset: 0x0004E178
		internal static string ODataMessageWriter_JsonPaddingOnInvalidContentType(object p0)
		{
			return TextRes.GetString("ODataMessageWriter_JsonPaddingOnInvalidContentType", new object[] { p0 });
		}

		// Token: 0x06001690 RID: 5776 RVA: 0x0004FF9C File Offset: 0x0004E19C
		internal static string ODataMessageWriter_NonCollectionType(object p0)
		{
			return TextRes.GetString("ODataMessageWriter_NonCollectionType", new object[] { p0 });
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06001691 RID: 5777 RVA: 0x0004FFBF File Offset: 0x0004E1BF
		internal static string ODataMessageWriterSettings_MessageWriterSettingsXmlCustomizationCallbacksMustBeSpecifiedBoth
		{
			get
			{
				return TextRes.GetString("ODataMessageWriterSettings_MessageWriterSettingsXmlCustomizationCallbacksMustBeSpecifiedBoth");
			}
		}

		// Token: 0x06001692 RID: 5778 RVA: 0x0004FFCC File Offset: 0x0004E1CC
		internal static string ODataCollectionWriter_CannotCreateCollectionWriterForFormat(object p0)
		{
			return TextRes.GetString("ODataCollectionWriter_CannotCreateCollectionWriterForFormat", new object[] { p0 });
		}

		// Token: 0x06001693 RID: 5779 RVA: 0x0004FFF0 File Offset: 0x0004E1F0
		internal static string ODataCollectionWriterCore_InvalidTransitionFromStart(object p0, object p1)
		{
			return TextRes.GetString("ODataCollectionWriterCore_InvalidTransitionFromStart", new object[] { p0, p1 });
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x00050018 File Offset: 0x0004E218
		internal static string ODataCollectionWriterCore_InvalidTransitionFromCollection(object p0, object p1)
		{
			return TextRes.GetString("ODataCollectionWriterCore_InvalidTransitionFromCollection", new object[] { p0, p1 });
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x00050040 File Offset: 0x0004E240
		internal static string ODataCollectionWriterCore_InvalidTransitionFromItem(object p0, object p1)
		{
			return TextRes.GetString("ODataCollectionWriterCore_InvalidTransitionFromItem", new object[] { p0, p1 });
		}

		// Token: 0x06001696 RID: 5782 RVA: 0x00050068 File Offset: 0x0004E268
		internal static string ODataCollectionWriterCore_WriteEndCalledInInvalidState(object p0)
		{
			return TextRes.GetString("ODataCollectionWriterCore_WriteEndCalledInInvalidState", new object[] { p0 });
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06001697 RID: 5783 RVA: 0x0005008B File Offset: 0x0004E28B
		internal static string ODataCollectionWriterCore_SyncCallOnAsyncWriter
		{
			get
			{
				return TextRes.GetString("ODataCollectionWriterCore_SyncCallOnAsyncWriter");
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06001698 RID: 5784 RVA: 0x00050097 File Offset: 0x0004E297
		internal static string ODataCollectionWriterCore_AsyncCallOnSyncWriter
		{
			get
			{
				return TextRes.GetString("ODataCollectionWriterCore_AsyncCallOnSyncWriter");
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06001699 RID: 5785 RVA: 0x000500A3 File Offset: 0x0004E2A3
		internal static string ODataCollectionWriterCore_CollectionsMustNotHaveEmptyName
		{
			get
			{
				return TextRes.GetString("ODataCollectionWriterCore_CollectionsMustNotHaveEmptyName");
			}
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x000500B0 File Offset: 0x0004E2B0
		internal static string ODataCollectionWriterCore_CollectionNameDoesntMatchFunctionImportName(object p0, object p1)
		{
			return TextRes.GetString("ODataCollectionWriterCore_CollectionNameDoesntMatchFunctionImportName", new object[] { p0, p1 });
		}

		// Token: 0x0600169B RID: 5787 RVA: 0x000500D8 File Offset: 0x0004E2D8
		internal static string ODataCollectionWriterCore_NonCollectionType(object p0, object p1)
		{
			return TextRes.GetString("ODataCollectionWriterCore_NonCollectionType", new object[] { p0, p1 });
		}

		// Token: 0x0600169C RID: 5788 RVA: 0x00050100 File Offset: 0x0004E300
		internal static string ODataBatch_InvalidHttpMethodForQueryOperation(object p0)
		{
			return TextRes.GetString("ODataBatch_InvalidHttpMethodForQueryOperation", new object[] { p0 });
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x00050124 File Offset: 0x0004E324
		internal static string ODataBatch_InvalidHttpMethodForChangeSetRequest(object p0)
		{
			return TextRes.GetString("ODataBatch_InvalidHttpMethodForChangeSetRequest", new object[] { p0 });
		}

		// Token: 0x0600169E RID: 5790 RVA: 0x00050148 File Offset: 0x0004E348
		internal static string ODataBatchOperationHeaderDictionary_KeyNotFound(object p0)
		{
			return TextRes.GetString("ODataBatchOperationHeaderDictionary_KeyNotFound", new object[] { p0 });
		}

		// Token: 0x0600169F RID: 5791 RVA: 0x0005016C File Offset: 0x0004E36C
		internal static string ODataBatchOperationHeaderDictionary_DuplicateCaseInsensitiveKeys(object p0)
		{
			return TextRes.GetString("ODataBatchOperationHeaderDictionary_DuplicateCaseInsensitiveKeys", new object[] { p0 });
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x060016A0 RID: 5792 RVA: 0x0005018F File Offset: 0x0004E38F
		internal static string ODataParameterWriter_InStreamErrorNotSupported
		{
			get
			{
				return TextRes.GetString("ODataParameterWriter_InStreamErrorNotSupported");
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x060016A1 RID: 5793 RVA: 0x0005019B File Offset: 0x0004E39B
		internal static string ODataParameterWriter_CannotCreateParameterWriterOnResponseMessage
		{
			get
			{
				return TextRes.GetString("ODataParameterWriter_CannotCreateParameterWriterOnResponseMessage");
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x060016A2 RID: 5794 RVA: 0x000501A7 File Offset: 0x0004E3A7
		internal static string ODataParameterWriterCore_SyncCallOnAsyncWriter
		{
			get
			{
				return TextRes.GetString("ODataParameterWriterCore_SyncCallOnAsyncWriter");
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x060016A3 RID: 5795 RVA: 0x000501B3 File Offset: 0x0004E3B3
		internal static string ODataParameterWriterCore_AsyncCallOnSyncWriter
		{
			get
			{
				return TextRes.GetString("ODataParameterWriterCore_AsyncCallOnSyncWriter");
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x060016A4 RID: 5796 RVA: 0x000501BF File Offset: 0x0004E3BF
		internal static string ODataParameterWriterCore_CannotWriteStart
		{
			get
			{
				return TextRes.GetString("ODataParameterWriterCore_CannotWriteStart");
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x060016A5 RID: 5797 RVA: 0x000501CB File Offset: 0x0004E3CB
		internal static string ODataParameterWriterCore_CannotWriteParameter
		{
			get
			{
				return TextRes.GetString("ODataParameterWriterCore_CannotWriteParameter");
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x060016A6 RID: 5798 RVA: 0x000501D7 File Offset: 0x0004E3D7
		internal static string ODataParameterWriterCore_CannotWriteEnd
		{
			get
			{
				return TextRes.GetString("ODataParameterWriterCore_CannotWriteEnd");
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x060016A7 RID: 5799 RVA: 0x000501E3 File Offset: 0x0004E3E3
		internal static string ODataParameterWriterCore_CannotWriteInErrorOrCompletedState
		{
			get
			{
				return TextRes.GetString("ODataParameterWriterCore_CannotWriteInErrorOrCompletedState");
			}
		}

		// Token: 0x060016A8 RID: 5800 RVA: 0x000501F0 File Offset: 0x0004E3F0
		internal static string ODataParameterWriterCore_DuplicatedParameterNameNotAllowed(object p0)
		{
			return TextRes.GetString("ODataParameterWriterCore_DuplicatedParameterNameNotAllowed", new object[] { p0 });
		}

		// Token: 0x060016A9 RID: 5801 RVA: 0x00050214 File Offset: 0x0004E414
		internal static string ODataParameterWriterCore_CannotWriteValueOnNonValueTypeKind(object p0, object p1)
		{
			return TextRes.GetString("ODataParameterWriterCore_CannotWriteValueOnNonValueTypeKind", new object[] { p0, p1 });
		}

		// Token: 0x060016AA RID: 5802 RVA: 0x0005023C File Offset: 0x0004E43C
		internal static string ODataParameterWriterCore_CannotWriteValueOnNonSupportedValueType(object p0, object p1)
		{
			return TextRes.GetString("ODataParameterWriterCore_CannotWriteValueOnNonSupportedValueType", new object[] { p0, p1 });
		}

		// Token: 0x060016AB RID: 5803 RVA: 0x00050264 File Offset: 0x0004E464
		internal static string ODataParameterWriterCore_CannotCreateCollectionWriterOnNonCollectionTypeKind(object p0, object p1)
		{
			return TextRes.GetString("ODataParameterWriterCore_CannotCreateCollectionWriterOnNonCollectionTypeKind", new object[] { p0, p1 });
		}

		// Token: 0x060016AC RID: 5804 RVA: 0x0005028C File Offset: 0x0004E48C
		internal static string ODataParameterWriterCore_ParameterNameNotFoundInFunctionImport(object p0, object p1)
		{
			return TextRes.GetString("ODataParameterWriterCore_ParameterNameNotFoundInFunctionImport", new object[] { p0, p1 });
		}

		// Token: 0x060016AD RID: 5805 RVA: 0x000502B4 File Offset: 0x0004E4B4
		internal static string ODataParameterWriterCore_MissingParameterInParameterPayload(object p0, object p1)
		{
			return TextRes.GetString("ODataParameterWriterCore_MissingParameterInParameterPayload", new object[] { p0, p1 });
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x060016AE RID: 5806 RVA: 0x000502DB File Offset: 0x0004E4DB
		internal static string ODataBatchWriter_FlushOrFlushAsyncCalledInStreamRequestedState
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_FlushOrFlushAsyncCalledInStreamRequestedState");
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x060016AF RID: 5807 RVA: 0x000502E7 File Offset: 0x0004E4E7
		internal static string ODataBatchWriter_CannotCompleteBatchWithActiveChangeSet
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_CannotCompleteBatchWithActiveChangeSet");
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x060016B0 RID: 5808 RVA: 0x000502F3 File Offset: 0x0004E4F3
		internal static string ODataBatchWriter_CannotStartChangeSetWithActiveChangeSet
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_CannotStartChangeSetWithActiveChangeSet");
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x060016B1 RID: 5809 RVA: 0x000502FF File Offset: 0x0004E4FF
		internal static string ODataBatchWriter_CannotCompleteChangeSetWithoutActiveChangeSet
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_CannotCompleteChangeSetWithoutActiveChangeSet");
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x060016B2 RID: 5810 RVA: 0x0005030B File Offset: 0x0004E50B
		internal static string ODataBatchWriter_InvalidTransitionFromStart
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_InvalidTransitionFromStart");
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x060016B3 RID: 5811 RVA: 0x00050317 File Offset: 0x0004E517
		internal static string ODataBatchWriter_InvalidTransitionFromBatchStarted
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_InvalidTransitionFromBatchStarted");
			}
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x060016B4 RID: 5812 RVA: 0x00050323 File Offset: 0x0004E523
		internal static string ODataBatchWriter_InvalidTransitionFromChangeSetStarted
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_InvalidTransitionFromChangeSetStarted");
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x060016B5 RID: 5813 RVA: 0x0005032F File Offset: 0x0004E52F
		internal static string ODataBatchWriter_InvalidTransitionFromOperationCreated
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_InvalidTransitionFromOperationCreated");
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x060016B6 RID: 5814 RVA: 0x0005033B File Offset: 0x0004E53B
		internal static string ODataBatchWriter_InvalidTransitionFromOperationContentStreamRequested
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_InvalidTransitionFromOperationContentStreamRequested");
			}
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x060016B7 RID: 5815 RVA: 0x00050347 File Offset: 0x0004E547
		internal static string ODataBatchWriter_InvalidTransitionFromOperationContentStreamDisposed
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_InvalidTransitionFromOperationContentStreamDisposed");
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x060016B8 RID: 5816 RVA: 0x00050353 File Offset: 0x0004E553
		internal static string ODataBatchWriter_InvalidTransitionFromChangeSetCompleted
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_InvalidTransitionFromChangeSetCompleted");
			}
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x060016B9 RID: 5817 RVA: 0x0005035F File Offset: 0x0004E55F
		internal static string ODataBatchWriter_InvalidTransitionFromBatchCompleted
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_InvalidTransitionFromBatchCompleted");
			}
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x060016BA RID: 5818 RVA: 0x0005036B File Offset: 0x0004E56B
		internal static string ODataBatchWriter_CannotCreateRequestOperationWhenWritingResponse
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_CannotCreateRequestOperationWhenWritingResponse");
			}
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x060016BB RID: 5819 RVA: 0x00050377 File Offset: 0x0004E577
		internal static string ODataBatchWriter_CannotCreateResponseOperationWhenWritingRequest
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_CannotCreateResponseOperationWhenWritingRequest");
			}
		}

		// Token: 0x060016BC RID: 5820 RVA: 0x00050384 File Offset: 0x0004E584
		internal static string ODataBatchWriter_MaxBatchSizeExceeded(object p0)
		{
			return TextRes.GetString("ODataBatchWriter_MaxBatchSizeExceeded", new object[] { p0 });
		}

		// Token: 0x060016BD RID: 5821 RVA: 0x000503A8 File Offset: 0x0004E5A8
		internal static string ODataBatchWriter_MaxChangeSetSizeExceeded(object p0)
		{
			return TextRes.GetString("ODataBatchWriter_MaxChangeSetSizeExceeded", new object[] { p0 });
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x060016BE RID: 5822 RVA: 0x000503CB File Offset: 0x0004E5CB
		internal static string ODataBatchWriter_SyncCallOnAsyncWriter
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_SyncCallOnAsyncWriter");
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x060016BF RID: 5823 RVA: 0x000503D7 File Offset: 0x0004E5D7
		internal static string ODataBatchWriter_AsyncCallOnSyncWriter
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_AsyncCallOnSyncWriter");
			}
		}

		// Token: 0x060016C0 RID: 5824 RVA: 0x000503E4 File Offset: 0x0004E5E4
		internal static string ODataBatchWriter_DuplicateContentIDsNotAllowed(object p0)
		{
			return TextRes.GetString("ODataBatchWriter_DuplicateContentIDsNotAllowed", new object[] { p0 });
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x060016C1 RID: 5825 RVA: 0x00050407 File Offset: 0x0004E607
		internal static string ODataBatchWriter_CannotWriteInStreamErrorForBatch
		{
			get
			{
				return TextRes.GetString("ODataBatchWriter_CannotWriteInStreamErrorForBatch");
			}
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x00050414 File Offset: 0x0004E614
		internal static string ODataBatchUtils_RelativeUriUsedWithoutBaseUriSpecified(object p0)
		{
			return TextRes.GetString("ODataBatchUtils_RelativeUriUsedWithoutBaseUriSpecified", new object[] { p0 });
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x00050438 File Offset: 0x0004E638
		internal static string ODataBatchUtils_RelativeUriStartingWithDollarUsedWithoutBaseUriSpecified(object p0)
		{
			return TextRes.GetString("ODataBatchUtils_RelativeUriStartingWithDollarUsedWithoutBaseUriSpecified", new object[] { p0 });
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x060016C4 RID: 5828 RVA: 0x0005045B File Offset: 0x0004E65B
		internal static string ODataBatchOperationMessage_VerifyNotCompleted
		{
			get
			{
				return TextRes.GetString("ODataBatchOperationMessage_VerifyNotCompleted");
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x060016C5 RID: 5829 RVA: 0x00050467 File Offset: 0x0004E667
		internal static string ODataBatchOperationStream_Disposed
		{
			get
			{
				return TextRes.GetString("ODataBatchOperationStream_Disposed");
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x060016C6 RID: 5830 RVA: 0x00050473 File Offset: 0x0004E673
		internal static string ODataBatchReader_CannotCreateRequestOperationWhenReadingResponse
		{
			get
			{
				return TextRes.GetString("ODataBatchReader_CannotCreateRequestOperationWhenReadingResponse");
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x060016C7 RID: 5831 RVA: 0x0005047F File Offset: 0x0004E67F
		internal static string ODataBatchReader_CannotCreateResponseOperationWhenReadingRequest
		{
			get
			{
				return TextRes.GetString("ODataBatchReader_CannotCreateResponseOperationWhenReadingRequest");
			}
		}

		// Token: 0x060016C8 RID: 5832 RVA: 0x0005048C File Offset: 0x0004E68C
		internal static string ODataBatchReader_InvalidStateForCreateOperationRequestMessage(object p0)
		{
			return TextRes.GetString("ODataBatchReader_InvalidStateForCreateOperationRequestMessage", new object[] { p0 });
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x060016C9 RID: 5833 RVA: 0x000504AF File Offset: 0x0004E6AF
		internal static string ODataBatchReader_OperationRequestMessageAlreadyCreated
		{
			get
			{
				return TextRes.GetString("ODataBatchReader_OperationRequestMessageAlreadyCreated");
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x060016CA RID: 5834 RVA: 0x000504BB File Offset: 0x0004E6BB
		internal static string ODataBatchReader_OperationResponseMessageAlreadyCreated
		{
			get
			{
				return TextRes.GetString("ODataBatchReader_OperationResponseMessageAlreadyCreated");
			}
		}

		// Token: 0x060016CB RID: 5835 RVA: 0x000504C8 File Offset: 0x0004E6C8
		internal static string ODataBatchReader_InvalidStateForCreateOperationResponseMessage(object p0)
		{
			return TextRes.GetString("ODataBatchReader_InvalidStateForCreateOperationResponseMessage", new object[] { p0 });
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x060016CC RID: 5836 RVA: 0x000504EB File Offset: 0x0004E6EB
		internal static string ODataBatchReader_CannotUseReaderWhileOperationStreamActive
		{
			get
			{
				return TextRes.GetString("ODataBatchReader_CannotUseReaderWhileOperationStreamActive");
			}
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x060016CD RID: 5837 RVA: 0x000504F7 File Offset: 0x0004E6F7
		internal static string ODataBatchReader_SyncCallOnAsyncReader
		{
			get
			{
				return TextRes.GetString("ODataBatchReader_SyncCallOnAsyncReader");
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x060016CE RID: 5838 RVA: 0x00050503 File Offset: 0x0004E703
		internal static string ODataBatchReader_AsyncCallOnSyncReader
		{
			get
			{
				return TextRes.GetString("ODataBatchReader_AsyncCallOnSyncReader");
			}
		}

		// Token: 0x060016CF RID: 5839 RVA: 0x00050510 File Offset: 0x0004E710
		internal static string ODataBatchReader_ReadOrReadAsyncCalledInInvalidState(object p0)
		{
			return TextRes.GetString("ODataBatchReader_ReadOrReadAsyncCalledInInvalidState", new object[] { p0 });
		}

		// Token: 0x060016D0 RID: 5840 RVA: 0x00050534 File Offset: 0x0004E734
		internal static string ODataBatchReader_MaxBatchSizeExceeded(object p0)
		{
			return TextRes.GetString("ODataBatchReader_MaxBatchSizeExceeded", new object[] { p0 });
		}

		// Token: 0x060016D1 RID: 5841 RVA: 0x00050558 File Offset: 0x0004E758
		internal static string ODataBatchReader_MaxChangeSetSizeExceeded(object p0)
		{
			return TextRes.GetString("ODataBatchReader_MaxChangeSetSizeExceeded", new object[] { p0 });
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x060016D2 RID: 5842 RVA: 0x0005057B File Offset: 0x0004E77B
		internal static string ODataBatchReader_NoMessageWasCreatedForOperation
		{
			get
			{
				return TextRes.GetString("ODataBatchReader_NoMessageWasCreatedForOperation");
			}
		}

		// Token: 0x060016D3 RID: 5843 RVA: 0x00050588 File Offset: 0x0004E788
		internal static string ODataBatchReader_DuplicateContentIDsNotAllowed(object p0)
		{
			return TextRes.GetString("ODataBatchReader_DuplicateContentIDsNotAllowed", new object[] { p0 });
		}

		// Token: 0x060016D4 RID: 5844 RVA: 0x000505AC File Offset: 0x0004E7AC
		internal static string ODataBatchReaderStream_InvalidHeaderSpecified(object p0)
		{
			return TextRes.GetString("ODataBatchReaderStream_InvalidHeaderSpecified", new object[] { p0 });
		}

		// Token: 0x060016D5 RID: 5845 RVA: 0x000505D0 File Offset: 0x0004E7D0
		internal static string ODataBatchReaderStream_InvalidRequestLine(object p0)
		{
			return TextRes.GetString("ODataBatchReaderStream_InvalidRequestLine", new object[] { p0 });
		}

		// Token: 0x060016D6 RID: 5846 RVA: 0x000505F4 File Offset: 0x0004E7F4
		internal static string ODataBatchReaderStream_InvalidResponseLine(object p0)
		{
			return TextRes.GetString("ODataBatchReaderStream_InvalidResponseLine", new object[] { p0 });
		}

		// Token: 0x060016D7 RID: 5847 RVA: 0x00050618 File Offset: 0x0004E818
		internal static string ODataBatchReaderStream_InvalidHttpVersionSpecified(object p0, object p1)
		{
			return TextRes.GetString("ODataBatchReaderStream_InvalidHttpVersionSpecified", new object[] { p0, p1 });
		}

		// Token: 0x060016D8 RID: 5848 RVA: 0x00050640 File Offset: 0x0004E840
		internal static string ODataBatchReaderStream_NonIntegerHttpStatusCode(object p0)
		{
			return TextRes.GetString("ODataBatchReaderStream_NonIntegerHttpStatusCode", new object[] { p0 });
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x060016D9 RID: 5849 RVA: 0x00050663 File Offset: 0x0004E863
		internal static string ODataBatchReaderStream_MissingContentTypeHeader
		{
			get
			{
				return TextRes.GetString("ODataBatchReaderStream_MissingContentTypeHeader");
			}
		}

		// Token: 0x060016DA RID: 5850 RVA: 0x00050670 File Offset: 0x0004E870
		internal static string ODataBatchReaderStream_MissingOrInvalidContentEncodingHeader(object p0, object p1)
		{
			return TextRes.GetString("ODataBatchReaderStream_MissingOrInvalidContentEncodingHeader", new object[] { p0, p1 });
		}

		// Token: 0x060016DB RID: 5851 RVA: 0x00050698 File Offset: 0x0004E898
		internal static string ODataBatchReaderStream_InvalidContentTypeSpecified(object p0, object p1, object p2, object p3)
		{
			return TextRes.GetString("ODataBatchReaderStream_InvalidContentTypeSpecified", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x060016DC RID: 5852 RVA: 0x000506C8 File Offset: 0x0004E8C8
		internal static string ODataBatchReaderStream_InvalidContentLengthSpecified(object p0)
		{
			return TextRes.GetString("ODataBatchReaderStream_InvalidContentLengthSpecified", new object[] { p0 });
		}

		// Token: 0x060016DD RID: 5853 RVA: 0x000506EC File Offset: 0x0004E8EC
		internal static string ODataBatchReaderStream_DuplicateHeaderFound(object p0)
		{
			return TextRes.GetString("ODataBatchReaderStream_DuplicateHeaderFound", new object[] { p0 });
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x060016DE RID: 5854 RVA: 0x0005070F File Offset: 0x0004E90F
		internal static string ODataBatchReaderStream_NestedChangesetsAreNotSupported
		{
			get
			{
				return TextRes.GetString("ODataBatchReaderStream_NestedChangesetsAreNotSupported");
			}
		}

		// Token: 0x060016DF RID: 5855 RVA: 0x0005071C File Offset: 0x0004E91C
		internal static string ODataBatchReaderStream_MultiByteEncodingsNotSupported(object p0)
		{
			return TextRes.GetString("ODataBatchReaderStream_MultiByteEncodingsNotSupported", new object[] { p0 });
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x060016E0 RID: 5856 RVA: 0x0005073F File Offset: 0x0004E93F
		internal static string ODataBatchReaderStream_UnexpectedEndOfInput
		{
			get
			{
				return TextRes.GetString("ODataBatchReaderStream_UnexpectedEndOfInput");
			}
		}

		// Token: 0x060016E1 RID: 5857 RVA: 0x0005074C File Offset: 0x0004E94C
		internal static string ODataBatchReaderStreamBuffer_BoundaryLineSecurityLimitReached(object p0)
		{
			return TextRes.GetString("ODataBatchReaderStreamBuffer_BoundaryLineSecurityLimitReached", new object[] { p0 });
		}

		// Token: 0x060016E2 RID: 5858 RVA: 0x00050770 File Offset: 0x0004E970
		internal static string HttpUtils_MediaTypeUnspecified(object p0)
		{
			return TextRes.GetString("HttpUtils_MediaTypeUnspecified", new object[] { p0 });
		}

		// Token: 0x060016E3 RID: 5859 RVA: 0x00050794 File Offset: 0x0004E994
		internal static string HttpUtils_MediaTypeRequiresSlash(object p0)
		{
			return TextRes.GetString("HttpUtils_MediaTypeRequiresSlash", new object[] { p0 });
		}

		// Token: 0x060016E4 RID: 5860 RVA: 0x000507B8 File Offset: 0x0004E9B8
		internal static string HttpUtils_MediaTypeRequiresSubType(object p0)
		{
			return TextRes.GetString("HttpUtils_MediaTypeRequiresSubType", new object[] { p0 });
		}

		// Token: 0x060016E5 RID: 5861 RVA: 0x000507DC File Offset: 0x0004E9DC
		internal static string HttpUtils_MediaTypeMissingParameterValue(object p0)
		{
			return TextRes.GetString("HttpUtils_MediaTypeMissingParameterValue", new object[] { p0 });
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x060016E6 RID: 5862 RVA: 0x000507FF File Offset: 0x0004E9FF
		internal static string HttpUtils_MediaTypeMissingParameterName
		{
			get
			{
				return TextRes.GetString("HttpUtils_MediaTypeMissingParameterName");
			}
		}

		// Token: 0x060016E7 RID: 5863 RVA: 0x0005080C File Offset: 0x0004EA0C
		internal static string HttpUtils_EscapeCharWithoutQuotes(object p0, object p1, object p2, object p3)
		{
			return TextRes.GetString("HttpUtils_EscapeCharWithoutQuotes", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x060016E8 RID: 5864 RVA: 0x0005083C File Offset: 0x0004EA3C
		internal static string HttpUtils_EscapeCharAtEnd(object p0, object p1, object p2, object p3)
		{
			return TextRes.GetString("HttpUtils_EscapeCharAtEnd", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x060016E9 RID: 5865 RVA: 0x0005086C File Offset: 0x0004EA6C
		internal static string HttpUtils_ClosingQuoteNotFound(object p0, object p1, object p2)
		{
			return TextRes.GetString("HttpUtils_ClosingQuoteNotFound", new object[] { p0, p1, p2 });
		}

		// Token: 0x060016EA RID: 5866 RVA: 0x00050898 File Offset: 0x0004EA98
		internal static string HttpUtils_InvalidCharacterInQuotedParameterValue(object p0, object p1, object p2, object p3)
		{
			return TextRes.GetString("HttpUtils_InvalidCharacterInQuotedParameterValue", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x060016EB RID: 5867 RVA: 0x000508C7 File Offset: 0x0004EAC7
		internal static string HttpUtils_ContentTypeMissing
		{
			get
			{
				return TextRes.GetString("HttpUtils_ContentTypeMissing");
			}
		}

		// Token: 0x060016EC RID: 5868 RVA: 0x000508D4 File Offset: 0x0004EAD4
		internal static string HttpUtils_MediaTypeRequiresSemicolonBeforeParameter(object p0)
		{
			return TextRes.GetString("HttpUtils_MediaTypeRequiresSemicolonBeforeParameter", new object[] { p0 });
		}

		// Token: 0x060016ED RID: 5869 RVA: 0x000508F8 File Offset: 0x0004EAF8
		internal static string HttpUtils_InvalidQualityValueStartChar(object p0, object p1)
		{
			return TextRes.GetString("HttpUtils_InvalidQualityValueStartChar", new object[] { p0, p1 });
		}

		// Token: 0x060016EE RID: 5870 RVA: 0x00050920 File Offset: 0x0004EB20
		internal static string HttpUtils_InvalidQualityValue(object p0, object p1)
		{
			return TextRes.GetString("HttpUtils_InvalidQualityValue", new object[] { p0, p1 });
		}

		// Token: 0x060016EF RID: 5871 RVA: 0x00050948 File Offset: 0x0004EB48
		internal static string HttpUtils_CannotConvertCharToInt(object p0)
		{
			return TextRes.GetString("HttpUtils_CannotConvertCharToInt", new object[] { p0 });
		}

		// Token: 0x060016F0 RID: 5872 RVA: 0x0005096C File Offset: 0x0004EB6C
		internal static string HttpUtils_MissingSeparatorBetweenCharsets(object p0)
		{
			return TextRes.GetString("HttpUtils_MissingSeparatorBetweenCharsets", new object[] { p0 });
		}

		// Token: 0x060016F1 RID: 5873 RVA: 0x00050990 File Offset: 0x0004EB90
		internal static string HttpUtils_InvalidSeparatorBetweenCharsets(object p0)
		{
			return TextRes.GetString("HttpUtils_InvalidSeparatorBetweenCharsets", new object[] { p0 });
		}

		// Token: 0x060016F2 RID: 5874 RVA: 0x000509B4 File Offset: 0x0004EBB4
		internal static string HttpUtils_InvalidCharsetName(object p0)
		{
			return TextRes.GetString("HttpUtils_InvalidCharsetName", new object[] { p0 });
		}

		// Token: 0x060016F3 RID: 5875 RVA: 0x000509D8 File Offset: 0x0004EBD8
		internal static string HttpUtils_UnexpectedEndOfQValue(object p0)
		{
			return TextRes.GetString("HttpUtils_UnexpectedEndOfQValue", new object[] { p0 });
		}

		// Token: 0x060016F4 RID: 5876 RVA: 0x000509FC File Offset: 0x0004EBFC
		internal static string HttpUtils_ExpectedLiteralNotFoundInString(object p0, object p1, object p2)
		{
			return TextRes.GetString("HttpUtils_ExpectedLiteralNotFoundInString", new object[] { p0, p1, p2 });
		}

		// Token: 0x060016F5 RID: 5877 RVA: 0x00050A28 File Offset: 0x0004EC28
		internal static string HttpUtils_InvalidHttpMethodString(object p0)
		{
			return TextRes.GetString("HttpUtils_InvalidHttpMethodString", new object[] { p0 });
		}

		// Token: 0x060016F6 RID: 5878 RVA: 0x00050A4C File Offset: 0x0004EC4C
		internal static string HttpUtils_NoOrMoreThanOneContentTypeSpecified(object p0)
		{
			return TextRes.GetString("HttpUtils_NoOrMoreThanOneContentTypeSpecified", new object[] { p0 });
		}

		// Token: 0x060016F7 RID: 5879 RVA: 0x00050A70 File Offset: 0x0004EC70
		internal static string HttpHeaderValueLexer_UnrecognizedSeparator(object p0, object p1, object p2, object p3)
		{
			return TextRes.GetString("HttpHeaderValueLexer_UnrecognizedSeparator", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x060016F8 RID: 5880 RVA: 0x00050AA0 File Offset: 0x0004ECA0
		internal static string HttpHeaderValueLexer_TokenExpectedButFoundQuotedString(object p0, object p1, object p2)
		{
			return TextRes.GetString("HttpHeaderValueLexer_TokenExpectedButFoundQuotedString", new object[] { p0, p1, p2 });
		}

		// Token: 0x060016F9 RID: 5881 RVA: 0x00050ACC File Offset: 0x0004ECCC
		internal static string HttpHeaderValueLexer_FailedToReadTokenOrQuotedString(object p0, object p1, object p2)
		{
			return TextRes.GetString("HttpHeaderValueLexer_FailedToReadTokenOrQuotedString", new object[] { p0, p1, p2 });
		}

		// Token: 0x060016FA RID: 5882 RVA: 0x00050AF8 File Offset: 0x0004ECF8
		internal static string HttpHeaderValueLexer_InvalidSeparatorAfterQuotedString(object p0, object p1, object p2, object p3)
		{
			return TextRes.GetString("HttpHeaderValueLexer_InvalidSeparatorAfterQuotedString", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x060016FB RID: 5883 RVA: 0x00050B28 File Offset: 0x0004ED28
		internal static string HttpHeaderValueLexer_EndOfFileAfterSeparator(object p0, object p1, object p2, object p3)
		{
			return TextRes.GetString("HttpHeaderValueLexer_EndOfFileAfterSeparator", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x060016FC RID: 5884 RVA: 0x00050B58 File Offset: 0x0004ED58
		internal static string MediaType_EncodingNotSupported(object p0)
		{
			return TextRes.GetString("MediaType_EncodingNotSupported", new object[] { p0 });
		}

		// Token: 0x060016FD RID: 5885 RVA: 0x00050B7C File Offset: 0x0004ED7C
		internal static string MediaTypeUtils_DidNotFindMatchingMediaType(object p0, object p1)
		{
			return TextRes.GetString("MediaTypeUtils_DidNotFindMatchingMediaType", new object[] { p0, p1 });
		}

		// Token: 0x060016FE RID: 5886 RVA: 0x00050BA4 File Offset: 0x0004EDA4
		internal static string MediaTypeUtils_CannotDetermineFormatFromContentType(object p0, object p1)
		{
			return TextRes.GetString("MediaTypeUtils_CannotDetermineFormatFromContentType", new object[] { p0, p1 });
		}

		// Token: 0x060016FF RID: 5887 RVA: 0x00050BCC File Offset: 0x0004EDCC
		internal static string MediaTypeUtils_NoOrMoreThanOneContentTypeSpecified(object p0)
		{
			return TextRes.GetString("MediaTypeUtils_NoOrMoreThanOneContentTypeSpecified", new object[] { p0 });
		}

		// Token: 0x06001700 RID: 5888 RVA: 0x00050BF0 File Offset: 0x0004EDF0
		internal static string MediaTypeUtils_BoundaryMustBeSpecifiedForBatchPayloads(object p0, object p1)
		{
			return TextRes.GetString("MediaTypeUtils_BoundaryMustBeSpecifiedForBatchPayloads", new object[] { p0, p1 });
		}

		// Token: 0x06001701 RID: 5889 RVA: 0x00050C18 File Offset: 0x0004EE18
		internal static string EntityPropertyMapping_EpmAttribute(object p0)
		{
			return TextRes.GetString("EntityPropertyMapping_EpmAttribute", new object[] { p0 });
		}

		// Token: 0x06001702 RID: 5890 RVA: 0x00050C3C File Offset: 0x0004EE3C
		internal static string EntityPropertyMapping_InvalidTargetPath(object p0)
		{
			return TextRes.GetString("EntityPropertyMapping_InvalidTargetPath", new object[] { p0 });
		}

		// Token: 0x06001703 RID: 5891 RVA: 0x00050C60 File Offset: 0x0004EE60
		internal static string EntityPropertyMapping_TargetNamespaceUriNotValid(object p0)
		{
			return TextRes.GetString("EntityPropertyMapping_TargetNamespaceUriNotValid", new object[] { p0 });
		}

		// Token: 0x06001704 RID: 5892 RVA: 0x00050C84 File Offset: 0x0004EE84
		internal static string EpmSourceTree_InvalidSourcePath(object p0, object p1)
		{
			return TextRes.GetString("EpmSourceTree_InvalidSourcePath", new object[] { p0, p1 });
		}

		// Token: 0x06001705 RID: 5893 RVA: 0x00050CAC File Offset: 0x0004EEAC
		internal static string EpmSourceTree_EndsWithNonPrimitiveType(object p0)
		{
			return TextRes.GetString("EpmSourceTree_EndsWithNonPrimitiveType", new object[] { p0 });
		}

		// Token: 0x06001706 RID: 5894 RVA: 0x00050CD0 File Offset: 0x0004EED0
		internal static string EpmSourceTree_TraversalOfNonComplexType(object p0)
		{
			return TextRes.GetString("EpmSourceTree_TraversalOfNonComplexType", new object[] { p0 });
		}

		// Token: 0x06001707 RID: 5895 RVA: 0x00050CF4 File Offset: 0x0004EEF4
		internal static string EpmSourceTree_DuplicateEpmAttributesWithSameSourceName(object p0, object p1)
		{
			return TextRes.GetString("EpmSourceTree_DuplicateEpmAttributesWithSameSourceName", new object[] { p0, p1 });
		}

		// Token: 0x06001708 RID: 5896 RVA: 0x00050D1C File Offset: 0x0004EF1C
		internal static string EpmSourceTree_MissingPropertyOnType(object p0, object p1)
		{
			return TextRes.GetString("EpmSourceTree_MissingPropertyOnType", new object[] { p0, p1 });
		}

		// Token: 0x06001709 RID: 5897 RVA: 0x00050D44 File Offset: 0x0004EF44
		internal static string EpmSourceTree_MissingPropertyOnInstance(object p0, object p1)
		{
			return TextRes.GetString("EpmSourceTree_MissingPropertyOnInstance", new object[] { p0, p1 });
		}

		// Token: 0x0600170A RID: 5898 RVA: 0x00050D6C File Offset: 0x0004EF6C
		internal static string EpmSourceTree_StreamPropertyCannotBeMapped(object p0, object p1)
		{
			return TextRes.GetString("EpmSourceTree_StreamPropertyCannotBeMapped", new object[] { p0, p1 });
		}

		// Token: 0x0600170B RID: 5899 RVA: 0x00050D94 File Offset: 0x0004EF94
		internal static string EpmSourceTree_SpatialTypeCannotBeMapped(object p0, object p1)
		{
			return TextRes.GetString("EpmSourceTree_SpatialTypeCannotBeMapped", new object[] { p0, p1 });
		}

		// Token: 0x0600170C RID: 5900 RVA: 0x00050DBC File Offset: 0x0004EFBC
		internal static string EpmSourceTree_OpenPropertySpatialTypeCannotBeMapped(object p0, object p1)
		{
			return TextRes.GetString("EpmSourceTree_OpenPropertySpatialTypeCannotBeMapped", new object[] { p0, p1 });
		}

		// Token: 0x0600170D RID: 5901 RVA: 0x00050DE4 File Offset: 0x0004EFE4
		internal static string EpmSourceTree_OpenComplexPropertyCannotBeMapped(object p0, object p1)
		{
			return TextRes.GetString("EpmSourceTree_OpenComplexPropertyCannotBeMapped", new object[] { p0, p1 });
		}

		// Token: 0x0600170E RID: 5902 RVA: 0x00050E0C File Offset: 0x0004F00C
		internal static string EpmSourceTree_CollectionPropertyCannotBeMapped(object p0, object p1)
		{
			return TextRes.GetString("EpmSourceTree_CollectionPropertyCannotBeMapped", new object[] { p0, p1 });
		}

		// Token: 0x0600170F RID: 5903 RVA: 0x00050E34 File Offset: 0x0004F034
		internal static string EpmTargetTree_InvalidTargetPath_EmptySegment(object p0)
		{
			return TextRes.GetString("EpmTargetTree_InvalidTargetPath_EmptySegment", new object[] { p0 });
		}

		// Token: 0x06001710 RID: 5904 RVA: 0x00050E58 File Offset: 0x0004F058
		internal static string EpmTargetTree_InvalidTargetPath_MixedContent(object p0, object p1)
		{
			return TextRes.GetString("EpmTargetTree_InvalidTargetPath_MixedContent", new object[] { p0, p1 });
		}

		// Token: 0x06001711 RID: 5905 RVA: 0x00050E80 File Offset: 0x0004F080
		internal static string EpmTargetTree_AttributeInMiddle(object p0)
		{
			return TextRes.GetString("EpmTargetTree_AttributeInMiddle", new object[] { p0 });
		}

		// Token: 0x06001712 RID: 5906 RVA: 0x00050EA4 File Offset: 0x0004F0A4
		internal static string EpmTargetTree_DuplicateEpmAttributesWithSameTargetName(object p0, object p1, object p2, object p3)
		{
			return TextRes.GetString("EpmTargetTree_DuplicateEpmAttributesWithSameTargetName", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06001713 RID: 5907 RVA: 0x00050ED4 File Offset: 0x0004F0D4
		internal static string EpmSyndicationWriter_DateTimePropertyCanNotBeConverted(object p0)
		{
			return TextRes.GetString("EpmSyndicationWriter_DateTimePropertyCanNotBeConverted", new object[] { p0 });
		}

		// Token: 0x06001714 RID: 5908 RVA: 0x00050EF8 File Offset: 0x0004F0F8
		internal static string EpmSyndicationWriter_EmptyCollectionMappedToAuthor(object p0)
		{
			return TextRes.GetString("EpmSyndicationWriter_EmptyCollectionMappedToAuthor", new object[] { p0 });
		}

		// Token: 0x06001715 RID: 5909 RVA: 0x00050F1C File Offset: 0x0004F11C
		internal static string EpmSyndicationWriter_NullValueForAttributeTarget(object p0, object p1, object p2)
		{
			return TextRes.GetString("EpmSyndicationWriter_NullValueForAttributeTarget", new object[] { p0, p1, p2 });
		}

		// Token: 0x06001716 RID: 5910 RVA: 0x00050F48 File Offset: 0x0004F148
		internal static string EpmSyndicationWriter_InvalidLinkLengthValue(object p0)
		{
			return TextRes.GetString("EpmSyndicationWriter_InvalidLinkLengthValue", new object[] { p0 });
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x00050F6C File Offset: 0x0004F16C
		internal static string EpmSyndicationWriter_InvalidValueForLinkRelCriteriaAttribute(object p0, object p1, object p2)
		{
			return TextRes.GetString("EpmSyndicationWriter_InvalidValueForLinkRelCriteriaAttribute", new object[] { p0, p1, p2 });
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x00050F98 File Offset: 0x0004F198
		internal static string EpmSyndicationWriter_InvalidValueForCategorySchemeCriteriaAttribute(object p0, object p1, object p2)
		{
			return TextRes.GetString("EpmSyndicationWriter_InvalidValueForCategorySchemeCriteriaAttribute", new object[] { p0, p1, p2 });
		}

		// Token: 0x06001719 RID: 5913 RVA: 0x00050FC4 File Offset: 0x0004F1C4
		internal static string ExpressionLexer_ExpectedLiteralToken(object p0)
		{
			return TextRes.GetString("ExpressionLexer_ExpectedLiteralToken", new object[] { p0 });
		}

		// Token: 0x0600171A RID: 5914 RVA: 0x00050FE8 File Offset: 0x0004F1E8
		internal static string UriUtils_InvalidRelativeUriForEscaping(object p0, object p1)
		{
			return TextRes.GetString("UriUtils_InvalidRelativeUriForEscaping", new object[] { p0, p1 });
		}

		// Token: 0x0600171B RID: 5915 RVA: 0x00051010 File Offset: 0x0004F210
		internal static string ODataUriUtils_ConvertToUriLiteralUnsupportedType(object p0)
		{
			return TextRes.GetString("ODataUriUtils_ConvertToUriLiteralUnsupportedType", new object[] { p0 });
		}

		// Token: 0x0600171C RID: 5916 RVA: 0x00051034 File Offset: 0x0004F234
		internal static string ODataUriUtils_ConvertToUriLiteralUnsupportedFormat(object p0)
		{
			return TextRes.GetString("ODataUriUtils_ConvertToUriLiteralUnsupportedFormat", new object[] { p0 });
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x0600171D RID: 5917 RVA: 0x00051057 File Offset: 0x0004F257
		internal static string ODataUriUtils_ConvertFromUriLiteralTypeRefWithoutModel
		{
			get
			{
				return TextRes.GetString("ODataUriUtils_ConvertFromUriLiteralTypeRefWithoutModel");
			}
		}

		// Token: 0x0600171E RID: 5918 RVA: 0x00051064 File Offset: 0x0004F264
		internal static string ODataUriUtils_ConvertFromUriLiteralTypeVerificationFailure(object p0, object p1)
		{
			return TextRes.GetString("ODataUriUtils_ConvertFromUriLiteralTypeVerificationFailure", new object[] { p0, p1 });
		}

		// Token: 0x0600171F RID: 5919 RVA: 0x0005108C File Offset: 0x0004F28C
		internal static string ODataUriUtils_ConvertFromUriLiteralNullTypeVerificationFailure(object p0, object p1)
		{
			return TextRes.GetString("ODataUriUtils_ConvertFromUriLiteralNullTypeVerificationFailure", new object[] { p0, p1 });
		}

		// Token: 0x06001720 RID: 5920 RVA: 0x000510B4 File Offset: 0x0004F2B4
		internal static string ODataUriUtils_ConvertFromUriLiteralNullOnNonNullableType(object p0)
		{
			return TextRes.GetString("ODataUriUtils_ConvertFromUriLiteralNullOnNonNullableType", new object[] { p0 });
		}

		// Token: 0x06001721 RID: 5921 RVA: 0x000510D8 File Offset: 0x0004F2D8
		internal static string ODataUtils_CannotConvertValueToRawPrimitive(object p0)
		{
			return TextRes.GetString("ODataUtils_CannotConvertValueToRawPrimitive", new object[] { p0 });
		}

		// Token: 0x06001722 RID: 5922 RVA: 0x000510FC File Offset: 0x0004F2FC
		internal static string ODataUtils_DidNotFindDefaultMediaType(object p0)
		{
			return TextRes.GetString("ODataUtils_DidNotFindDefaultMediaType", new object[] { p0 });
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06001723 RID: 5923 RVA: 0x0005111F File Offset: 0x0004F31F
		internal static string ODataUtils_CannotSaveAnnotationsToBuiltInModel
		{
			get
			{
				return TextRes.GetString("ODataUtils_CannotSaveAnnotationsToBuiltInModel");
			}
		}

		// Token: 0x06001724 RID: 5924 RVA: 0x0005112C File Offset: 0x0004F32C
		internal static string ODataUtils_UnsupportedVersionHeader(object p0)
		{
			return TextRes.GetString("ODataUtils_UnsupportedVersionHeader", new object[] { p0 });
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06001725 RID: 5925 RVA: 0x0005114F File Offset: 0x0004F34F
		internal static string ODataUtils_UnsupportedVersionNumber
		{
			get
			{
				return TextRes.GetString("ODataUtils_UnsupportedVersionNumber");
			}
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06001726 RID: 5926 RVA: 0x0005115B File Offset: 0x0004F35B
		internal static string ODataUtils_NullValueForMimeTypeAnnotation
		{
			get
			{
				return TextRes.GetString("ODataUtils_NullValueForMimeTypeAnnotation");
			}
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06001727 RID: 5927 RVA: 0x00051167 File Offset: 0x0004F367
		internal static string ODataUtils_NullValueForHttpMethodAnnotation
		{
			get
			{
				return TextRes.GetString("ODataUtils_NullValueForHttpMethodAnnotation");
			}
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06001728 RID: 5928 RVA: 0x00051173 File Offset: 0x0004F373
		internal static string ODataUtils_IsAlwaysBindableAnnotationSetForANonBindableFunctionImport
		{
			get
			{
				return TextRes.GetString("ODataUtils_IsAlwaysBindableAnnotationSetForANonBindableFunctionImport");
			}
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06001729 RID: 5929 RVA: 0x0005117F File Offset: 0x0004F37F
		internal static string ODataUtils_UnexpectedIsAlwaysBindableAnnotationInANonBindableFunctionImport
		{
			get
			{
				return TextRes.GetString("ODataUtils_UnexpectedIsAlwaysBindableAnnotationInANonBindableFunctionImport");
			}
		}

		// Token: 0x0600172A RID: 5930 RVA: 0x0005118C File Offset: 0x0004F38C
		internal static string ReaderUtils_EnumerableModified(object p0)
		{
			return TextRes.GetString("ReaderUtils_EnumerableModified", new object[] { p0 });
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x000511B0 File Offset: 0x0004F3B0
		internal static string ReaderValidationUtils_NullValueForNonNullableType(object p0)
		{
			return TextRes.GetString("ReaderValidationUtils_NullValueForNonNullableType", new object[] { p0 });
		}

		// Token: 0x0600172C RID: 5932 RVA: 0x000511D4 File Offset: 0x0004F3D4
		internal static string ReaderValidationUtils_NullNamedValueForNonNullableType(object p0, object p1)
		{
			return TextRes.GetString("ReaderValidationUtils_NullNamedValueForNonNullableType", new object[] { p0, p1 });
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x0600172D RID: 5933 RVA: 0x000511FB File Offset: 0x0004F3FB
		internal static string ReaderValidationUtils_EntityReferenceLinkMissingUri
		{
			get
			{
				return TextRes.GetString("ReaderValidationUtils_EntityReferenceLinkMissingUri");
			}
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x0600172E RID: 5934 RVA: 0x00051207 File Offset: 0x0004F407
		internal static string ReaderValidationUtils_ValueWithoutType
		{
			get
			{
				return TextRes.GetString("ReaderValidationUtils_ValueWithoutType");
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x0600172F RID: 5935 RVA: 0x00051213 File Offset: 0x0004F413
		internal static string ReaderValidationUtils_EntryWithoutType
		{
			get
			{
				return TextRes.GetString("ReaderValidationUtils_EntryWithoutType");
			}
		}

		// Token: 0x06001730 RID: 5936 RVA: 0x00051220 File Offset: 0x0004F420
		internal static string ReaderValidationUtils_DerivedComplexTypesAreNotAllowed(object p0, object p1)
		{
			return TextRes.GetString("ReaderValidationUtils_DerivedComplexTypesAreNotAllowed", new object[] { p0, p1 });
		}

		// Token: 0x06001731 RID: 5937 RVA: 0x00051248 File Offset: 0x0004F448
		internal static string ReaderValidationUtils_CannotConvertPrimitiveValue(object p0)
		{
			return TextRes.GetString("ReaderValidationUtils_CannotConvertPrimitiveValue", new object[] { p0 });
		}

		// Token: 0x06001732 RID: 5938 RVA: 0x0005126C File Offset: 0x0004F46C
		internal static string ReaderValidationUtils_MessageReaderSettingsBaseUriMustBeNullOrAbsolute(object p0)
		{
			return TextRes.GetString("ReaderValidationUtils_MessageReaderSettingsBaseUriMustBeNullOrAbsolute", new object[] { p0 });
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06001733 RID: 5939 RVA: 0x0005128F File Offset: 0x0004F48F
		internal static string ReaderValidationUtils_UndeclaredPropertyBehaviorKindSpecifiedOnRequest
		{
			get
			{
				return TextRes.GetString("ReaderValidationUtils_UndeclaredPropertyBehaviorKindSpecifiedOnRequest");
			}
		}

		// Token: 0x06001734 RID: 5940 RVA: 0x0005129C File Offset: 0x0004F49C
		internal static string ReaderValidationUtils_UndeclaredPropertyBehaviorKindSpecifiedForOpenType(object p0, object p1)
		{
			return TextRes.GetString("ReaderValidationUtils_UndeclaredPropertyBehaviorKindSpecifiedForOpenType", new object[] { p0, p1 });
		}

		// Token: 0x06001735 RID: 5941 RVA: 0x000512C4 File Offset: 0x0004F4C4
		internal static string ReaderValidationUtils_MetadataUriValidationInvalidExpectedEntitySet(object p0, object p1, object p2)
		{
			return TextRes.GetString("ReaderValidationUtils_MetadataUriValidationInvalidExpectedEntitySet", new object[] { p0, p1, p2 });
		}

		// Token: 0x06001736 RID: 5942 RVA: 0x000512F0 File Offset: 0x0004F4F0
		internal static string ReaderValidationUtils_MetadataUriValidationInvalidExpectedEntityType(object p0, object p1, object p2)
		{
			return TextRes.GetString("ReaderValidationUtils_MetadataUriValidationInvalidExpectedEntityType", new object[] { p0, p1, p2 });
		}

		// Token: 0x06001737 RID: 5943 RVA: 0x0005131C File Offset: 0x0004F51C
		internal static string ReaderValidationUtils_MetadataUriValidationNonMatchingPropertyNames(object p0, object p1, object p2, object p3)
		{
			return TextRes.GetString("ReaderValidationUtils_MetadataUriValidationNonMatchingPropertyNames", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06001738 RID: 5944 RVA: 0x0005134C File Offset: 0x0004F54C
		internal static string ReaderValidationUtils_MetadataUriValidationNonMatchingDeclaringTypes(object p0, object p1, object p2, object p3)
		{
			return TextRes.GetString("ReaderValidationUtils_MetadataUriValidationNonMatchingDeclaringTypes", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06001739 RID: 5945 RVA: 0x0005137C File Offset: 0x0004F57C
		internal static string ReaderValidationUtils_MetadataUriValidationNonMatchingCollectionNames(object p0, object p1, object p2)
		{
			return TextRes.GetString("ReaderValidationUtils_MetadataUriValidationNonMatchingCollectionNames", new object[] { p0, p1, p2 });
		}

		// Token: 0x0600173A RID: 5946 RVA: 0x000513A8 File Offset: 0x0004F5A8
		internal static string ReaderValidationUtils_MetadataUriValidationNonMatchingCollectionItemTypes(object p0, object p1, object p2, object p3)
		{
			return TextRes.GetString("ReaderValidationUtils_MetadataUriValidationNonMatchingCollectionItemTypes", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x0600173B RID: 5947 RVA: 0x000513D8 File Offset: 0x0004F5D8
		internal static string ReaderValidationUtils_MetadataUriValidationPropertyWithExpectedFunctionImport(object p0, object p1, object p2, object p3)
		{
			return TextRes.GetString("ReaderValidationUtils_MetadataUriValidationPropertyWithExpectedFunctionImport", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x0600173C RID: 5948 RVA: 0x00051408 File Offset: 0x0004F608
		internal static string ReaderValidationUtils_MetadataUriValidationFunctionImportWithExpectedProperty(object p0, object p1, object p2, object p3)
		{
			return TextRes.GetString("ReaderValidationUtils_MetadataUriValidationFunctionImportWithExpectedProperty", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x0600173D RID: 5949 RVA: 0x00051438 File Offset: 0x0004F638
		internal static string ReaderValidationUtils_NonMatchingCollectionNames(object p0, object p1)
		{
			return TextRes.GetString("ReaderValidationUtils_NonMatchingCollectionNames", new object[] { p0, p1 });
		}

		// Token: 0x0600173E RID: 5950 RVA: 0x00051460 File Offset: 0x0004F660
		internal static string ReaderValidationUtils_NonMatchingPropertyNames(object p0, object p1)
		{
			return TextRes.GetString("ReaderValidationUtils_NonMatchingPropertyNames", new object[] { p0, p1 });
		}

		// Token: 0x0600173F RID: 5951 RVA: 0x00051488 File Offset: 0x0004F688
		internal static string ReaderValidationUtils_MetadataUriValidationNonMatchingPropertyDeclaringTypes(object p0, object p1, object p2, object p3)
		{
			return TextRes.GetString("ReaderValidationUtils_MetadataUriValidationNonMatchingPropertyDeclaringTypes", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06001740 RID: 5952 RVA: 0x000514B8 File Offset: 0x0004F6B8
		internal static string ReaderValidationUtils_MetadataUriValidationNonMatchingPropertyTypes(object p0, object p1, object p2, object p3, object p4)
		{
			return TextRes.GetString("ReaderValidationUtils_MetadataUriValidationNonMatchingPropertyTypes", new object[] { p0, p1, p2, p3, p4 });
		}

		// Token: 0x06001741 RID: 5953 RVA: 0x000514EC File Offset: 0x0004F6EC
		internal static string ReaderValidationUtils_MetadataUriValidationNonMatchingFunctionImportNames(object p0, object p1, object p2)
		{
			return TextRes.GetString("ReaderValidationUtils_MetadataUriValidationNonMatchingFunctionImportNames", new object[] { p0, p1, p2 });
		}

		// Token: 0x06001742 RID: 5954 RVA: 0x00051518 File Offset: 0x0004F718
		internal static string ReaderValidationUtils_MetadataUriValidationNonMatchingFunctionImportReturnTypes(object p0, object p1, object p2, object p3)
		{
			return TextRes.GetString("ReaderValidationUtils_MetadataUriValidationNonMatchingFunctionImportReturnTypes", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06001743 RID: 5955 RVA: 0x00051548 File Offset: 0x0004F748
		internal static string ReaderValidationUtils_TypeInMetadataUriDoesNotMatchExpectedType(object p0, object p1, object p2)
		{
			return TextRes.GetString("ReaderValidationUtils_TypeInMetadataUriDoesNotMatchExpectedType", new object[] { p0, p1, p2 });
		}

		// Token: 0x06001744 RID: 5956 RVA: 0x00051574 File Offset: 0x0004F774
		internal static string ReaderValidationUtils_MetadataUriDoesNotReferTypeAssignableToExpectedType(object p0, object p1, object p2)
		{
			return TextRes.GetString("ReaderValidationUtils_MetadataUriDoesNotReferTypeAssignableToExpectedType", new object[] { p0, p1, p2 });
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06001745 RID: 5957 RVA: 0x0005159F File Offset: 0x0004F79F
		internal static string ODataMessageReader_ReaderAlreadyUsed
		{
			get
			{
				return TextRes.GetString("ODataMessageReader_ReaderAlreadyUsed");
			}
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06001746 RID: 5958 RVA: 0x000515AB File Offset: 0x0004F7AB
		internal static string ODataMessageReader_ErrorPayloadInRequest
		{
			get
			{
				return TextRes.GetString("ODataMessageReader_ErrorPayloadInRequest");
			}
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06001747 RID: 5959 RVA: 0x000515B7 File Offset: 0x0004F7B7
		internal static string ODataMessageReader_ServiceDocumentInRequest
		{
			get
			{
				return TextRes.GetString("ODataMessageReader_ServiceDocumentInRequest");
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06001748 RID: 5960 RVA: 0x000515C3 File Offset: 0x0004F7C3
		internal static string ODataMessageReader_MetadataDocumentInRequest
		{
			get
			{
				return TextRes.GetString("ODataMessageReader_MetadataDocumentInRequest");
			}
		}

		// Token: 0x06001749 RID: 5961 RVA: 0x000515D0 File Offset: 0x0004F7D0
		internal static string ODataMessageReader_ExpectedTypeSpecifiedWithoutMetadata(object p0)
		{
			return TextRes.GetString("ODataMessageReader_ExpectedTypeSpecifiedWithoutMetadata", new object[] { p0 });
		}

		// Token: 0x0600174A RID: 5962 RVA: 0x000515F4 File Offset: 0x0004F7F4
		internal static string ODataMessageReader_EntitySetSpecifiedWithoutMetadata(object p0)
		{
			return TextRes.GetString("ODataMessageReader_EntitySetSpecifiedWithoutMetadata", new object[] { p0 });
		}

		// Token: 0x0600174B RID: 5963 RVA: 0x00051618 File Offset: 0x0004F818
		internal static string ODataMessageReader_FunctionImportSpecifiedWithoutMetadata(object p0)
		{
			return TextRes.GetString("ODataMessageReader_FunctionImportSpecifiedWithoutMetadata", new object[] { p0 });
		}

		// Token: 0x0600174C RID: 5964 RVA: 0x0005163C File Offset: 0x0004F83C
		internal static string ODataMessageReader_ProducingFunctionImportNonCollectionType(object p0, object p1)
		{
			return TextRes.GetString("ODataMessageReader_ProducingFunctionImportNonCollectionType", new object[] { p0, p1 });
		}

		// Token: 0x0600174D RID: 5965 RVA: 0x00051664 File Offset: 0x0004F864
		internal static string ODataMessageReader_ExpectedCollectionTypeWrongKind(object p0)
		{
			return TextRes.GetString("ODataMessageReader_ExpectedCollectionTypeWrongKind", new object[] { p0 });
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x0600174E RID: 5966 RVA: 0x00051687 File Offset: 0x0004F887
		internal static string ODataMessageReader_ExpectedPropertyTypeEntityCollectionKind
		{
			get
			{
				return TextRes.GetString("ODataMessageReader_ExpectedPropertyTypeEntityCollectionKind");
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x0600174F RID: 5967 RVA: 0x00051693 File Offset: 0x0004F893
		internal static string ODataMessageReader_ExpectedPropertyTypeEntityKind
		{
			get
			{
				return TextRes.GetString("ODataMessageReader_ExpectedPropertyTypeEntityKind");
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06001750 RID: 5968 RVA: 0x0005169F File Offset: 0x0004F89F
		internal static string ODataMessageReader_ExpectedPropertyTypeStream
		{
			get
			{
				return TextRes.GetString("ODataMessageReader_ExpectedPropertyTypeStream");
			}
		}

		// Token: 0x06001751 RID: 5969 RVA: 0x000516AC File Offset: 0x0004F8AC
		internal static string ODataMessageReader_ExpectedValueTypeWrongKind(object p0)
		{
			return TextRes.GetString("ODataMessageReader_ExpectedValueTypeWrongKind", new object[] { p0 });
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06001752 RID: 5970 RVA: 0x000516CF File Offset: 0x0004F8CF
		internal static string ODataMessageReader_NoneOrEmptyContentTypeHeader
		{
			get
			{
				return TextRes.GetString("ODataMessageReader_NoneOrEmptyContentTypeHeader");
			}
		}

		// Token: 0x06001753 RID: 5971 RVA: 0x000516DC File Offset: 0x0004F8DC
		internal static string ODataMessageReader_WildcardInContentType(object p0)
		{
			return TextRes.GetString("ODataMessageReader_WildcardInContentType", new object[] { p0 });
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06001754 RID: 5972 RVA: 0x000516FF File Offset: 0x0004F8FF
		internal static string ODataMessageReader_EntityReferenceLinksInRequestNotAllowed
		{
			get
			{
				return TextRes.GetString("ODataMessageReader_EntityReferenceLinksInRequestNotAllowed");
			}
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06001755 RID: 5973 RVA: 0x0005170B File Offset: 0x0004F90B
		internal static string ODataMessageReader_GetFormatCalledBeforeReadingStarted
		{
			get
			{
				return TextRes.GetString("ODataMessageReader_GetFormatCalledBeforeReadingStarted");
			}
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06001756 RID: 5974 RVA: 0x00051717 File Offset: 0x0004F917
		internal static string ODataMessageReader_DetectPayloadKindMultipleTimes
		{
			get
			{
				return TextRes.GetString("ODataMessageReader_DetectPayloadKindMultipleTimes");
			}
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06001757 RID: 5975 RVA: 0x00051723 File Offset: 0x0004F923
		internal static string ODataMessageReader_PayloadKindDetectionRunning
		{
			get
			{
				return TextRes.GetString("ODataMessageReader_PayloadKindDetectionRunning");
			}
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x06001758 RID: 5976 RVA: 0x0005172F File Offset: 0x0004F92F
		internal static string ODataMessageReader_PayloadKindDetectionInServerMode
		{
			get
			{
				return TextRes.GetString("ODataMessageReader_PayloadKindDetectionInServerMode");
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x06001759 RID: 5977 RVA: 0x0005173B File Offset: 0x0004F93B
		internal static string ODataMessageReader_ParameterPayloadInResponse
		{
			get
			{
				return TextRes.GetString("ODataMessageReader_ParameterPayloadInResponse");
			}
		}

		// Token: 0x0600175A RID: 5978 RVA: 0x00051748 File Offset: 0x0004F948
		internal static string ODataMessageReader_SingletonNavigationPropertyForEntityReferenceLinks(object p0, object p1)
		{
			return TextRes.GetString("ODataMessageReader_SingletonNavigationPropertyForEntityReferenceLinks", new object[] { p0, p1 });
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x0600175B RID: 5979 RVA: 0x0005176F File Offset: 0x0004F96F
		internal static string ODataMessage_MustNotModifyMessage
		{
			get
			{
				return TextRes.GetString("ODataMessage_MustNotModifyMessage");
			}
		}

		// Token: 0x0600175C RID: 5980 RVA: 0x0005177C File Offset: 0x0004F97C
		internal static string ODataMediaTypeUtils_BoundaryMustBeSpecifiedForBatchPayloads(object p0, object p1)
		{
			return TextRes.GetString("ODataMediaTypeUtils_BoundaryMustBeSpecifiedForBatchPayloads", new object[] { p0, p1 });
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x0600175D RID: 5981 RVA: 0x000517A3 File Offset: 0x0004F9A3
		internal static string ODataReaderCore_SyncCallOnAsyncReader
		{
			get
			{
				return TextRes.GetString("ODataReaderCore_SyncCallOnAsyncReader");
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x0600175E RID: 5982 RVA: 0x000517AF File Offset: 0x0004F9AF
		internal static string ODataReaderCore_AsyncCallOnSyncReader
		{
			get
			{
				return TextRes.GetString("ODataReaderCore_AsyncCallOnSyncReader");
			}
		}

		// Token: 0x0600175F RID: 5983 RVA: 0x000517BC File Offset: 0x0004F9BC
		internal static string ODataReaderCore_ReadOrReadAsyncCalledInInvalidState(object p0)
		{
			return TextRes.GetString("ODataReaderCore_ReadOrReadAsyncCalledInInvalidState", new object[] { p0 });
		}

		// Token: 0x06001760 RID: 5984 RVA: 0x000517E0 File Offset: 0x0004F9E0
		internal static string ODataReaderCore_NoReadCallsAllowed(object p0)
		{
			return TextRes.GetString("ODataReaderCore_NoReadCallsAllowed", new object[] { p0 });
		}

		// Token: 0x06001761 RID: 5985 RVA: 0x00051804 File Offset: 0x0004FA04
		internal static string ODataJsonReader_CannotReadEntriesOfFeed(object p0)
		{
			return TextRes.GetString("ODataJsonReader_CannotReadEntriesOfFeed", new object[] { p0 });
		}

		// Token: 0x06001762 RID: 5986 RVA: 0x00051828 File Offset: 0x0004FA28
		internal static string ODataJsonReader_CannotReadFeedStart(object p0)
		{
			return TextRes.GetString("ODataJsonReader_CannotReadFeedStart", new object[] { p0 });
		}

		// Token: 0x06001763 RID: 5987 RVA: 0x0005184C File Offset: 0x0004FA4C
		internal static string ODataJsonReader_CannotReadEntryStart(object p0)
		{
			return TextRes.GetString("ODataJsonReader_CannotReadEntryStart", new object[] { p0 });
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06001764 RID: 5988 RVA: 0x0005186F File Offset: 0x0004FA6F
		internal static string ODataJsonReader_ParsingWithoutMetadata
		{
			get
			{
				return TextRes.GetString("ODataJsonReader_ParsingWithoutMetadata");
			}
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06001765 RID: 5989 RVA: 0x0005187B File Offset: 0x0004FA7B
		internal static string ODataJsonReaderUtils_CannotConvertInt64OrDecimal
		{
			get
			{
				return TextRes.GetString("ODataJsonReaderUtils_CannotConvertInt64OrDecimal");
			}
		}

		// Token: 0x06001766 RID: 5990 RVA: 0x00051888 File Offset: 0x0004FA88
		internal static string ODataJsonReaderUtils_CannotConvertInt32(object p0)
		{
			return TextRes.GetString("ODataJsonReaderUtils_CannotConvertInt32", new object[] { p0 });
		}

		// Token: 0x06001767 RID: 5991 RVA: 0x000518AC File Offset: 0x0004FAAC
		internal static string ODataJsonReaderUtils_CannotConvertDouble(object p0)
		{
			return TextRes.GetString("ODataJsonReaderUtils_CannotConvertDouble", new object[] { p0 });
		}

		// Token: 0x06001768 RID: 5992 RVA: 0x000518D0 File Offset: 0x0004FAD0
		internal static string ODataJsonReaderUtils_CannotConvertBoolean(object p0)
		{
			return TextRes.GetString("ODataJsonReaderUtils_CannotConvertBoolean", new object[] { p0 });
		}

		// Token: 0x06001769 RID: 5993 RVA: 0x000518F4 File Offset: 0x0004FAF4
		internal static string ODataJsonReaderUtils_CannotConvertDateTime(object p0)
		{
			return TextRes.GetString("ODataJsonReaderUtils_CannotConvertDateTime", new object[] { p0 });
		}

		// Token: 0x0600176A RID: 5994 RVA: 0x00051918 File Offset: 0x0004FB18
		internal static string ODataJsonReaderUtils_CannotConvertDateTimeOffset(object p0)
		{
			return TextRes.GetString("ODataJsonReaderUtils_CannotConvertDateTimeOffset", new object[] { p0 });
		}

		// Token: 0x0600176B RID: 5995 RVA: 0x0005193C File Offset: 0x0004FB3C
		internal static string ODataJsonReaderUtils_MultipleMetadataPropertiesWithSameName(object p0)
		{
			return TextRes.GetString("ODataJsonReaderUtils_MultipleMetadataPropertiesWithSameName", new object[] { p0 });
		}

		// Token: 0x0600176C RID: 5996 RVA: 0x00051960 File Offset: 0x0004FB60
		internal static string ODataJsonReaderUtils_MultipleEntityReferenceLinksWrapperPropertiesWithSameName(object p0)
		{
			return TextRes.GetString("ODataJsonReaderUtils_MultipleEntityReferenceLinksWrapperPropertiesWithSameName", new object[] { p0 });
		}

		// Token: 0x0600176D RID: 5997 RVA: 0x00051984 File Offset: 0x0004FB84
		internal static string ODataJsonReaderUtils_MultipleErrorPropertiesWithSameName(object p0)
		{
			return TextRes.GetString("ODataJsonReaderUtils_MultipleErrorPropertiesWithSameName", new object[] { p0 });
		}

		// Token: 0x0600176E RID: 5998 RVA: 0x000519A8 File Offset: 0x0004FBA8
		internal static string ODataJsonReaderUtils_FeedPropertyWithNullValue(object p0)
		{
			return TextRes.GetString("ODataJsonReaderUtils_FeedPropertyWithNullValue", new object[] { p0 });
		}

		// Token: 0x0600176F RID: 5999 RVA: 0x000519CC File Offset: 0x0004FBCC
		internal static string ODataJsonReaderUtils_MediaResourcePropertyWithNullValue(object p0)
		{
			return TextRes.GetString("ODataJsonReaderUtils_MediaResourcePropertyWithNullValue", new object[] { p0 });
		}

		// Token: 0x06001770 RID: 6000 RVA: 0x000519F0 File Offset: 0x0004FBF0
		internal static string ODataJsonReaderUtils_EntityReferenceLinksInlineCountWithNullValue(object p0)
		{
			return TextRes.GetString("ODataJsonReaderUtils_EntityReferenceLinksInlineCountWithNullValue", new object[] { p0 });
		}

		// Token: 0x06001771 RID: 6001 RVA: 0x00051A14 File Offset: 0x0004FC14
		internal static string ODataJsonReaderUtils_EntityReferenceLinksPropertyWithNullValue(object p0)
		{
			return TextRes.GetString("ODataJsonReaderUtils_EntityReferenceLinksPropertyWithNullValue", new object[] { p0 });
		}

		// Token: 0x06001772 RID: 6002 RVA: 0x00051A38 File Offset: 0x0004FC38
		internal static string ODataJsonReaderUtils_MetadataPropertyWithNullValue(object p0)
		{
			return TextRes.GetString("ODataJsonReaderUtils_MetadataPropertyWithNullValue", new object[] { p0 });
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06001773 RID: 6003 RVA: 0x00051A5B File Offset: 0x0004FC5B
		internal static string ODataJsonDeserializer_DataWrapperPropertyNotFound
		{
			get
			{
				return TextRes.GetString("ODataJsonDeserializer_DataWrapperPropertyNotFound");
			}
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06001774 RID: 6004 RVA: 0x00051A67 File Offset: 0x0004FC67
		internal static string ODataJsonDeserializer_DataWrapperMultipleProperties
		{
			get
			{
				return TextRes.GetString("ODataJsonDeserializer_DataWrapperMultipleProperties");
			}
		}

		// Token: 0x06001775 RID: 6005 RVA: 0x00051A74 File Offset: 0x0004FC74
		internal static string ODataJsonDeserializer_RelativeUriUsedWithoutBaseUriSpecified(object p0)
		{
			return TextRes.GetString("ODataJsonDeserializer_RelativeUriUsedWithoutBaseUriSpecified", new object[] { p0 });
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06001776 RID: 6006 RVA: 0x00051A97 File Offset: 0x0004FC97
		internal static string ODataJsonCollectionDeserializer_MissingResultsPropertyForCollection
		{
			get
			{
				return TextRes.GetString("ODataJsonCollectionDeserializer_MissingResultsPropertyForCollection");
			}
		}

		// Token: 0x06001777 RID: 6007 RVA: 0x00051AA4 File Offset: 0x0004FCA4
		internal static string ODataJsonCollectionDeserializer_CannotReadCollectionContentStart(object p0)
		{
			return TextRes.GetString("ODataJsonCollectionDeserializer_CannotReadCollectionContentStart", new object[] { p0 });
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06001778 RID: 6008 RVA: 0x00051AC7 File Offset: 0x0004FCC7
		internal static string ODataJsonCollectionDeserializer_MultipleResultsPropertiesForCollection
		{
			get
			{
				return TextRes.GetString("ODataJsonCollectionDeserializer_MultipleResultsPropertiesForCollection");
			}
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06001779 RID: 6009 RVA: 0x00051AD3 File Offset: 0x0004FCD3
		internal static string ODataJsonEntityReferenceLinkDeserializer_ExpectedEntityReferenceLinksResultsPropertyNotFound
		{
			get
			{
				return TextRes.GetString("ODataJsonEntityReferenceLinkDeserializer_ExpectedEntityReferenceLinksResultsPropertyNotFound");
			}
		}

		// Token: 0x0600177A RID: 6010 RVA: 0x00051AE0 File Offset: 0x0004FCE0
		internal static string ODataJsonEntityReferenceLinkDeserializer_EntityReferenceLinkMustBeObjectValue(object p0)
		{
			return TextRes.GetString("ODataJsonEntityReferenceLinkDeserializer_EntityReferenceLinkMustBeObjectValue", new object[] { p0 });
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x0600177B RID: 6011 RVA: 0x00051B03 File Offset: 0x0004FD03
		internal static string ODataJsonEntityReferenceLinkDeserializer_MultipleUriPropertiesInEntityReferenceLink
		{
			get
			{
				return TextRes.GetString("ODataJsonEntityReferenceLinkDeserializer_MultipleUriPropertiesInEntityReferenceLink");
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x0600177C RID: 6012 RVA: 0x00051B0F File Offset: 0x0004FD0F
		internal static string ODataJsonEntityReferenceLinkDeserializer_EntityReferenceLinkUriCannotBeNull
		{
			get
			{
				return TextRes.GetString("ODataJsonEntityReferenceLinkDeserializer_EntityReferenceLinkUriCannotBeNull");
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x0600177D RID: 6013 RVA: 0x00051B1B File Offset: 0x0004FD1B
		internal static string ODataJsonEntryAndFeedDeserializer_ExpectedFeedResultsPropertyNotFound
		{
			get
			{
				return TextRes.GetString("ODataJsonEntryAndFeedDeserializer_ExpectedFeedResultsPropertyNotFound");
			}
		}

		// Token: 0x0600177E RID: 6014 RVA: 0x00051B28 File Offset: 0x0004FD28
		internal static string ODataJsonEntryAndFeedDeserializer_CannotReadFeedContentStart(object p0)
		{
			return TextRes.GetString("ODataJsonEntryAndFeedDeserializer_CannotReadFeedContentStart", new object[] { p0 });
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x0600177F RID: 6015 RVA: 0x00051B4B File Offset: 0x0004FD4B
		internal static string ODataJsonEntryAndFeedDeserializer_MultipleMetadataPropertiesInEntryValue
		{
			get
			{
				return TextRes.GetString("ODataJsonEntryAndFeedDeserializer_MultipleMetadataPropertiesInEntryValue");
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06001780 RID: 6016 RVA: 0x00051B57 File Offset: 0x0004FD57
		internal static string ODataJsonEntryAndFeedDeserializer_MultipleUriPropertiesInDeferredLink
		{
			get
			{
				return TextRes.GetString("ODataJsonEntryAndFeedDeserializer_MultipleUriPropertiesInDeferredLink");
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06001781 RID: 6017 RVA: 0x00051B63 File Offset: 0x0004FD63
		internal static string ODataJsonEntryAndFeedDeserializer_DeferredLinkUriCannotBeNull
		{
			get
			{
				return TextRes.GetString("ODataJsonEntryAndFeedDeserializer_DeferredLinkUriCannotBeNull");
			}
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06001782 RID: 6018 RVA: 0x00051B6F File Offset: 0x0004FD6F
		internal static string ODataJsonEntryAndFeedDeserializer_DeferredLinkMissingUri
		{
			get
			{
				return TextRes.GetString("ODataJsonEntryAndFeedDeserializer_DeferredLinkMissingUri");
			}
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06001783 RID: 6019 RVA: 0x00051B7B File Offset: 0x0004FD7B
		internal static string ODataJsonEntryAndFeedDeserializer_CannotReadNavigationPropertyValue
		{
			get
			{
				return TextRes.GetString("ODataJsonEntryAndFeedDeserializer_CannotReadNavigationPropertyValue");
			}
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06001784 RID: 6020 RVA: 0x00051B87 File Offset: 0x0004FD87
		internal static string ODataJsonEntryAndFeedDeserializer_MultipleFeedResultsPropertiesFound
		{
			get
			{
				return TextRes.GetString("ODataJsonEntryAndFeedDeserializer_MultipleFeedResultsPropertiesFound");
			}
		}

		// Token: 0x06001785 RID: 6021 RVA: 0x00051B94 File Offset: 0x0004FD94
		internal static string ODataJsonEntryAndFeedDeserializer_MultipleMetadataPropertiesForStreamProperty(object p0)
		{
			return TextRes.GetString("ODataJsonEntryAndFeedDeserializer_MultipleMetadataPropertiesForStreamProperty", new object[] { p0 });
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06001786 RID: 6022 RVA: 0x00051BB7 File Offset: 0x0004FDB7
		internal static string ODataJsonEntryAndFeedDeserializer_CannotParseStreamReference
		{
			get
			{
				return TextRes.GetString("ODataJsonEntryAndFeedDeserializer_CannotParseStreamReference");
			}
		}

		// Token: 0x06001787 RID: 6023 RVA: 0x00051BC4 File Offset: 0x0004FDC4
		internal static string ODataJsonEntryAndFeedDeserializer_PropertyInEntryMustHaveObjectValue(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonEntryAndFeedDeserializer_PropertyInEntryMustHaveObjectValue", new object[] { p0, p1 });
		}

		// Token: 0x06001788 RID: 6024 RVA: 0x00051BEC File Offset: 0x0004FDEC
		internal static string ODataJsonEntryAndFeedDeserializer_CannotReadSingletonNavigationPropertyValue(object p0)
		{
			return TextRes.GetString("ODataJsonEntryAndFeedDeserializer_CannotReadSingletonNavigationPropertyValue", new object[] { p0 });
		}

		// Token: 0x06001789 RID: 6025 RVA: 0x00051C10 File Offset: 0x0004FE10
		internal static string ODataJsonEntryAndFeedDeserializer_CannotReadCollectionNavigationPropertyValue(object p0)
		{
			return TextRes.GetString("ODataJsonEntryAndFeedDeserializer_CannotReadCollectionNavigationPropertyValue", new object[] { p0 });
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x0600178A RID: 6026 RVA: 0x00051C33 File Offset: 0x0004FE33
		internal static string ODataJsonEntryAndFeedDeserializer_StreamPropertyInRequest
		{
			get
			{
				return TextRes.GetString("ODataJsonEntryAndFeedDeserializer_StreamPropertyInRequest");
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x0600178B RID: 6027 RVA: 0x00051C3F File Offset: 0x0004FE3F
		internal static string ODataJsonLightEntryAndFeedSerializer_AnnotationGroupWithoutName
		{
			get
			{
				return TextRes.GetString("ODataJsonLightEntryAndFeedSerializer_AnnotationGroupWithoutName");
			}
		}

		// Token: 0x0600178C RID: 6028 RVA: 0x00051C4C File Offset: 0x0004FE4C
		internal static string ODataJsonLightEntryAndFeedSerializer_AnnotationGroupMemberWithoutName(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedSerializer_AnnotationGroupMemberWithoutName", new object[] { p0 });
		}

		// Token: 0x0600178D RID: 6029 RVA: 0x00051C70 File Offset: 0x0004FE70
		internal static string ODataJsonLightEntryAndFeedSerializer_AnnotationGroupMemberWithInvalidValue(object p0, object p1, object p2)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedSerializer_AnnotationGroupMemberWithInvalidValue", new object[] { p0, p1, p2 });
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x0600178E RID: 6030 RVA: 0x00051C9B File Offset: 0x0004FE9B
		internal static string ODataJsonLightEntryAndFeedSerializer_AnnotationGroupInRequest
		{
			get
			{
				return TextRes.GetString("ODataJsonLightEntryAndFeedSerializer_AnnotationGroupInRequest");
			}
		}

		// Token: 0x0600178F RID: 6031 RVA: 0x00051CA8 File Offset: 0x0004FEA8
		internal static string ODataJsonLightEntryAndFeedSerializer_AnnotationGroupMemberMustBeAnnotation(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedSerializer_AnnotationGroupMemberMustBeAnnotation", new object[] { p0, p1 });
		}

		// Token: 0x06001790 RID: 6032 RVA: 0x00051CD0 File Offset: 0x0004FED0
		internal static string ODataJsonLightEntryAndFeedSerializer_DuplicateAnnotationGroup(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedSerializer_DuplicateAnnotationGroup", new object[] { p0 });
		}

		// Token: 0x06001791 RID: 6033 RVA: 0x00051CF4 File Offset: 0x0004FEF4
		internal static string ODataJsonLightEntryAndFeedSerializer_ActionsAndFunctionsGroupMustSpecifyTarget(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedSerializer_ActionsAndFunctionsGroupMustSpecifyTarget", new object[] { p0 });
		}

		// Token: 0x06001792 RID: 6034 RVA: 0x00051D18 File Offset: 0x0004FF18
		internal static string ODataJsonLightEntryAndFeedSerializer_ActionsAndFunctionsGroupMustNotHaveDuplicateTarget(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedSerializer_ActionsAndFunctionsGroupMustNotHaveDuplicateTarget", new object[] { p0, p1 });
		}

		// Token: 0x06001793 RID: 6035 RVA: 0x00051D40 File Offset: 0x0004FF40
		internal static string ODataJsonErrorDeserializer_TopLevelErrorWithInvalidProperty(object p0)
		{
			return TextRes.GetString("ODataJsonErrorDeserializer_TopLevelErrorWithInvalidProperty", new object[] { p0 });
		}

		// Token: 0x06001794 RID: 6036 RVA: 0x00051D64 File Offset: 0x0004FF64
		internal static string ODataJsonErrorDeserializer_TopLevelErrorMessageValueWithInvalidProperty(object p0)
		{
			return TextRes.GetString("ODataJsonErrorDeserializer_TopLevelErrorMessageValueWithInvalidProperty", new object[] { p0 });
		}

		// Token: 0x06001795 RID: 6037 RVA: 0x00051D88 File Offset: 0x0004FF88
		internal static string ODataVerboseJsonErrorDeserializer_TopLevelErrorValueWithInvalidProperty(object p0)
		{
			return TextRes.GetString("ODataVerboseJsonErrorDeserializer_TopLevelErrorValueWithInvalidProperty", new object[] { p0 });
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06001796 RID: 6038 RVA: 0x00051DAB File Offset: 0x0004FFAB
		internal static string ODataJsonPropertyAndValueDeserializer_TopLevelPropertyWithoutMetadata
		{
			get
			{
				return TextRes.GetString("ODataJsonPropertyAndValueDeserializer_TopLevelPropertyWithoutMetadata");
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06001797 RID: 6039 RVA: 0x00051DB7 File Offset: 0x0004FFB7
		internal static string ODataJsonPropertyAndValueDeserializer_InvalidTopLevelPropertyPayload
		{
			get
			{
				return TextRes.GetString("ODataJsonPropertyAndValueDeserializer_InvalidTopLevelPropertyPayload");
			}
		}

		// Token: 0x06001798 RID: 6040 RVA: 0x00051DC4 File Offset: 0x0004FFC4
		internal static string ODataJsonPropertyAndValueDeserializer_CannotReadPropertyValue(object p0)
		{
			return TextRes.GetString("ODataJsonPropertyAndValueDeserializer_CannotReadPropertyValue", new object[] { p0 });
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06001799 RID: 6041 RVA: 0x00051DE7 File Offset: 0x0004FFE7
		internal static string ODataJsonPropertyAndValueDeserializer_MultipleMetadataPropertiesInComplexValue
		{
			get
			{
				return TextRes.GetString("ODataJsonPropertyAndValueDeserializer_MultipleMetadataPropertiesInComplexValue");
			}
		}

		// Token: 0x0600179A RID: 6042 RVA: 0x00051DF4 File Offset: 0x0004FFF4
		internal static string ODataJsonPropertyAndValueDeserializer_MultiplePropertiesInCollectionWrapper(object p0)
		{
			return TextRes.GetString("ODataJsonPropertyAndValueDeserializer_MultiplePropertiesInCollectionWrapper", new object[] { p0 });
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x0600179B RID: 6043 RVA: 0x00051E17 File Offset: 0x00050017
		internal static string ODataJsonPropertyAndValueDeserializer_CollectionWithoutResults
		{
			get
			{
				return TextRes.GetString("ODataJsonPropertyAndValueDeserializer_CollectionWithoutResults");
			}
		}

		// Token: 0x0600179C RID: 6044 RVA: 0x00051E24 File Offset: 0x00050024
		internal static string ODataJsonPropertyAndValueDeserializer_InvalidTypeName(object p0)
		{
			return TextRes.GetString("ODataJsonPropertyAndValueDeserializer_InvalidTypeName", new object[] { p0 });
		}

		// Token: 0x0600179D RID: 6045 RVA: 0x00051E48 File Offset: 0x00050048
		internal static string ODataJsonPropertyAndValueDeserializer_InvalidPrimitiveTypeName(object p0)
		{
			return TextRes.GetString("ODataJsonPropertyAndValueDeserializer_InvalidPrimitiveTypeName", new object[] { p0 });
		}

		// Token: 0x0600179E RID: 6046 RVA: 0x00051E6C File Offset: 0x0005006C
		internal static string ODataJsonPropertyAndValueDeserializer_MetadataPropertyMustHaveObjectValue(object p0)
		{
			return TextRes.GetString("ODataJsonPropertyAndValueDeserializer_MetadataPropertyMustHaveObjectValue", new object[] { p0 });
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x0600179F RID: 6047 RVA: 0x00051E8F File Offset: 0x0005008F
		internal static string ODataJsonServiceDocumentDeserializer_MultipleEntitySetsPropertiesForServiceDocument
		{
			get
			{
				return TextRes.GetString("ODataJsonServiceDocumentDeserializer_MultipleEntitySetsPropertiesForServiceDocument");
			}
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x060017A0 RID: 6048 RVA: 0x00051E9B File Offset: 0x0005009B
		internal static string ODataJsonServiceDocumentDeserializer_NoEntitySetsPropertyForServiceDocument
		{
			get
			{
				return TextRes.GetString("ODataJsonServiceDocumentDeserializer_NoEntitySetsPropertyForServiceDocument");
			}
		}

		// Token: 0x060017A1 RID: 6049 RVA: 0x00051EA8 File Offset: 0x000500A8
		internal static string ODataCollectionReaderCore_ReadOrReadAsyncCalledInInvalidState(object p0)
		{
			return TextRes.GetString("ODataCollectionReaderCore_ReadOrReadAsyncCalledInInvalidState", new object[] { p0 });
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x060017A2 RID: 6050 RVA: 0x00051ECB File Offset: 0x000500CB
		internal static string ODataCollectionReaderCore_SyncCallOnAsyncReader
		{
			get
			{
				return TextRes.GetString("ODataCollectionReaderCore_SyncCallOnAsyncReader");
			}
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x060017A3 RID: 6051 RVA: 0x00051ED7 File Offset: 0x000500D7
		internal static string ODataCollectionReaderCore_AsyncCallOnSyncReader
		{
			get
			{
				return TextRes.GetString("ODataCollectionReaderCore_AsyncCallOnSyncReader");
			}
		}

		// Token: 0x060017A4 RID: 6052 RVA: 0x00051EE4 File Offset: 0x000500E4
		internal static string ODataCollectionReaderCore_ExpectedItemTypeSetInInvalidState(object p0, object p1)
		{
			return TextRes.GetString("ODataCollectionReaderCore_ExpectedItemTypeSetInInvalidState", new object[] { p0, p1 });
		}

		// Token: 0x060017A5 RID: 6053 RVA: 0x00051F0C File Offset: 0x0005010C
		internal static string ODataParameterReaderCore_ReadOrReadAsyncCalledInInvalidState(object p0)
		{
			return TextRes.GetString("ODataParameterReaderCore_ReadOrReadAsyncCalledInInvalidState", new object[] { p0 });
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x060017A6 RID: 6054 RVA: 0x00051F2F File Offset: 0x0005012F
		internal static string ODataParameterReaderCore_SyncCallOnAsyncReader
		{
			get
			{
				return TextRes.GetString("ODataParameterReaderCore_SyncCallOnAsyncReader");
			}
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x060017A7 RID: 6055 RVA: 0x00051F3B File Offset: 0x0005013B
		internal static string ODataParameterReaderCore_AsyncCallOnSyncReader
		{
			get
			{
				return TextRes.GetString("ODataParameterReaderCore_AsyncCallOnSyncReader");
			}
		}

		// Token: 0x060017A8 RID: 6056 RVA: 0x00051F48 File Offset: 0x00050148
		internal static string ODataParameterReaderCore_SubReaderMustBeCreatedAndReadToCompletionBeforeTheNextReadOrReadAsyncCall(object p0, object p1)
		{
			return TextRes.GetString("ODataParameterReaderCore_SubReaderMustBeCreatedAndReadToCompletionBeforeTheNextReadOrReadAsyncCall", new object[] { p0, p1 });
		}

		// Token: 0x060017A9 RID: 6057 RVA: 0x00051F70 File Offset: 0x00050170
		internal static string ODataParameterReaderCore_SubReaderMustBeInCompletedStateBeforeTheNextReadOrReadAsyncCall(object p0, object p1)
		{
			return TextRes.GetString("ODataParameterReaderCore_SubReaderMustBeInCompletedStateBeforeTheNextReadOrReadAsyncCall", new object[] { p0, p1 });
		}

		// Token: 0x060017AA RID: 6058 RVA: 0x00051F98 File Offset: 0x00050198
		internal static string ODataParameterReaderCore_InvalidCreateReaderMethodCalledForState(object p0, object p1)
		{
			return TextRes.GetString("ODataParameterReaderCore_InvalidCreateReaderMethodCalledForState", new object[] { p0, p1 });
		}

		// Token: 0x060017AB RID: 6059 RVA: 0x00051FC0 File Offset: 0x000501C0
		internal static string ODataParameterReaderCore_CreateReaderAlreadyCalled(object p0, object p1)
		{
			return TextRes.GetString("ODataParameterReaderCore_CreateReaderAlreadyCalled", new object[] { p0, p1 });
		}

		// Token: 0x060017AC RID: 6060 RVA: 0x00051FE8 File Offset: 0x000501E8
		internal static string ODataParameterReaderCore_ParameterNameNotInMetadata(object p0, object p1)
		{
			return TextRes.GetString("ODataParameterReaderCore_ParameterNameNotInMetadata", new object[] { p0, p1 });
		}

		// Token: 0x060017AD RID: 6061 RVA: 0x00052010 File Offset: 0x00050210
		internal static string ODataParameterReaderCore_DuplicateParametersInPayload(object p0)
		{
			return TextRes.GetString("ODataParameterReaderCore_DuplicateParametersInPayload", new object[] { p0 });
		}

		// Token: 0x060017AE RID: 6062 RVA: 0x00052034 File Offset: 0x00050234
		internal static string ODataParameterReaderCore_ParametersMissingInPayload(object p0, object p1)
		{
			return TextRes.GetString("ODataParameterReaderCore_ParametersMissingInPayload", new object[] { p0, p1 });
		}

		// Token: 0x060017AF RID: 6063 RVA: 0x0005205C File Offset: 0x0005025C
		internal static string ODataJsonParameterReader_UnsupportedPrimitiveParameterType(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonParameterReader_UnsupportedPrimitiveParameterType", new object[] { p0, p1 });
		}

		// Token: 0x060017B0 RID: 6064 RVA: 0x00052084 File Offset: 0x00050284
		internal static string ODataJsonParameterReader_UnsupportedParameterTypeKind(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonParameterReader_UnsupportedParameterTypeKind", new object[] { p0, p1 });
		}

		// Token: 0x060017B1 RID: 6065 RVA: 0x000520AC File Offset: 0x000502AC
		internal static string ODataJsonParameterReader_NullCollectionExpected(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonParameterReader_NullCollectionExpected", new object[] { p0, p1 });
		}

		// Token: 0x060017B2 RID: 6066 RVA: 0x000520D4 File Offset: 0x000502D4
		internal static string ODataJsonInputContext_FunctionImportCannotBeNullForCreateParameterReader(object p0)
		{
			return TextRes.GetString("ODataJsonInputContext_FunctionImportCannotBeNullForCreateParameterReader", new object[] { p0 });
		}

		// Token: 0x060017B3 RID: 6067 RVA: 0x000520F8 File Offset: 0x000502F8
		internal static string ODataJsonCollectionReader_CannotReadWrappedCollectionStart(object p0)
		{
			return TextRes.GetString("ODataJsonCollectionReader_CannotReadWrappedCollectionStart", new object[] { p0 });
		}

		// Token: 0x060017B4 RID: 6068 RVA: 0x0005211C File Offset: 0x0005031C
		internal static string ODataJsonCollectionReader_CannotReadCollectionStart(object p0)
		{
			return TextRes.GetString("ODataJsonCollectionReader_CannotReadCollectionStart", new object[] { p0 });
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x060017B5 RID: 6069 RVA: 0x0005213F File Offset: 0x0005033F
		internal static string ODataJsonCollectionReader_ParsingWithoutMetadata
		{
			get
			{
				return TextRes.GetString("ODataJsonCollectionReader_ParsingWithoutMetadata");
			}
		}

		// Token: 0x060017B6 RID: 6070 RVA: 0x0005214C File Offset: 0x0005034C
		internal static string ValidationUtils_ActionsAndFunctionsMustSpecifyMetadata(object p0)
		{
			return TextRes.GetString("ValidationUtils_ActionsAndFunctionsMustSpecifyMetadata", new object[] { p0 });
		}

		// Token: 0x060017B7 RID: 6071 RVA: 0x00052170 File Offset: 0x00050370
		internal static string ValidationUtils_ActionsAndFunctionsMustSpecifyTarget(object p0)
		{
			return TextRes.GetString("ValidationUtils_ActionsAndFunctionsMustSpecifyTarget", new object[] { p0 });
		}

		// Token: 0x060017B8 RID: 6072 RVA: 0x00052194 File Offset: 0x00050394
		internal static string ValidationUtils_EnumerableContainsANullItem(object p0)
		{
			return TextRes.GetString("ValidationUtils_EnumerableContainsANullItem", new object[] { p0 });
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x060017B9 RID: 6073 RVA: 0x000521B7 File Offset: 0x000503B7
		internal static string ValidationUtils_AssociationLinkMustSpecifyName
		{
			get
			{
				return TextRes.GetString("ValidationUtils_AssociationLinkMustSpecifyName");
			}
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x060017BA RID: 6074 RVA: 0x000521C3 File Offset: 0x000503C3
		internal static string ValidationUtils_AssociationLinkMustSpecifyUrl
		{
			get
			{
				return TextRes.GetString("ValidationUtils_AssociationLinkMustSpecifyUrl");
			}
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x060017BB RID: 6075 RVA: 0x000521CF File Offset: 0x000503CF
		internal static string ValidationUtils_TypeNameMustNotBeEmpty
		{
			get
			{
				return TextRes.GetString("ValidationUtils_TypeNameMustNotBeEmpty");
			}
		}

		// Token: 0x060017BC RID: 6076 RVA: 0x000521DC File Offset: 0x000503DC
		internal static string ValidationUtils_PropertyDoesNotExistOnType(object p0, object p1)
		{
			return TextRes.GetString("ValidationUtils_PropertyDoesNotExistOnType", new object[] { p0, p1 });
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x060017BD RID: 6077 RVA: 0x00052203 File Offset: 0x00050403
		internal static string ValidationUtils_ResourceCollectionMustSpecifyUrl
		{
			get
			{
				return TextRes.GetString("ValidationUtils_ResourceCollectionMustSpecifyUrl");
			}
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x060017BE RID: 6078 RVA: 0x0005220F File Offset: 0x0005040F
		internal static string ValidationUtils_ResourceCollectionUrlMustNotBeNull
		{
			get
			{
				return TextRes.GetString("ValidationUtils_ResourceCollectionUrlMustNotBeNull");
			}
		}

		// Token: 0x060017BF RID: 6079 RVA: 0x0005221C File Offset: 0x0005041C
		internal static string ValidationUtils_NonPrimitiveTypeForPrimitiveValue(object p0)
		{
			return TextRes.GetString("ValidationUtils_NonPrimitiveTypeForPrimitiveValue", new object[] { p0 });
		}

		// Token: 0x060017C0 RID: 6080 RVA: 0x00052240 File Offset: 0x00050440
		internal static string ValidationUtils_UnsupportedPrimitiveType(object p0)
		{
			return TextRes.GetString("ValidationUtils_UnsupportedPrimitiveType", new object[] { p0 });
		}

		// Token: 0x060017C1 RID: 6081 RVA: 0x00052264 File Offset: 0x00050464
		internal static string ValidationUtils_IncompatiblePrimitiveItemType(object p0, object p1, object p2, object p3)
		{
			return TextRes.GetString("ValidationUtils_IncompatiblePrimitiveItemType", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x060017C2 RID: 6082 RVA: 0x00052293 File Offset: 0x00050493
		internal static string ValidationUtils_NonStreamingCollectionElementsMustNotBeNull
		{
			get
			{
				return TextRes.GetString("ValidationUtils_NonStreamingCollectionElementsMustNotBeNull");
			}
		}

		// Token: 0x060017C3 RID: 6083 RVA: 0x000522A0 File Offset: 0x000504A0
		internal static string ValidationUtils_InvalidCollectionTypeName(object p0)
		{
			return TextRes.GetString("ValidationUtils_InvalidCollectionTypeName", new object[] { p0 });
		}

		// Token: 0x060017C4 RID: 6084 RVA: 0x000522C4 File Offset: 0x000504C4
		internal static string ValidationUtils_UnrecognizedTypeName(object p0)
		{
			return TextRes.GetString("ValidationUtils_UnrecognizedTypeName", new object[] { p0 });
		}

		// Token: 0x060017C5 RID: 6085 RVA: 0x000522E8 File Offset: 0x000504E8
		internal static string ValidationUtils_IncorrectTypeKind(object p0, object p1, object p2)
		{
			return TextRes.GetString("ValidationUtils_IncorrectTypeKind", new object[] { p0, p1, p2 });
		}

		// Token: 0x060017C6 RID: 6086 RVA: 0x00052314 File Offset: 0x00050514
		internal static string ValidationUtils_IncorrectTypeKindNoTypeName(object p0, object p1)
		{
			return TextRes.GetString("ValidationUtils_IncorrectTypeKindNoTypeName", new object[] { p0, p1 });
		}

		// Token: 0x060017C7 RID: 6087 RVA: 0x0005233C File Offset: 0x0005053C
		internal static string ValidationUtils_IncorrectValueTypeKind(object p0, object p1)
		{
			return TextRes.GetString("ValidationUtils_IncorrectValueTypeKind", new object[] { p0, p1 });
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x060017C8 RID: 6088 RVA: 0x00052363 File Offset: 0x00050563
		internal static string ValidationUtils_LinkMustSpecifyName
		{
			get
			{
				return TextRes.GetString("ValidationUtils_LinkMustSpecifyName");
			}
		}

		// Token: 0x060017C9 RID: 6089 RVA: 0x00052370 File Offset: 0x00050570
		internal static string ValidationUtils_MismatchPropertyKindForStreamProperty(object p0)
		{
			return TextRes.GetString("ValidationUtils_MismatchPropertyKindForStreamProperty", new object[] { p0 });
		}

		// Token: 0x060017CA RID: 6090 RVA: 0x00052394 File Offset: 0x00050594
		internal static string ValidationUtils_InvalidEtagValue(object p0)
		{
			return TextRes.GetString("ValidationUtils_InvalidEtagValue", new object[] { p0 });
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x060017CB RID: 6091 RVA: 0x000523B7 File Offset: 0x000505B7
		internal static string ValidationUtils_NestedCollectionsAreNotSupported
		{
			get
			{
				return TextRes.GetString("ValidationUtils_NestedCollectionsAreNotSupported");
			}
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x060017CC RID: 6092 RVA: 0x000523C3 File Offset: 0x000505C3
		internal static string ValidationUtils_StreamReferenceValuesNotSupportedInCollections
		{
			get
			{
				return TextRes.GetString("ValidationUtils_StreamReferenceValuesNotSupportedInCollections");
			}
		}

		// Token: 0x060017CD RID: 6093 RVA: 0x000523D0 File Offset: 0x000505D0
		internal static string ValidationUtils_IncompatibleType(object p0, object p1)
		{
			return TextRes.GetString("ValidationUtils_IncompatibleType", new object[] { p0, p1 });
		}

		// Token: 0x060017CE RID: 6094 RVA: 0x000523F8 File Offset: 0x000505F8
		internal static string ValidationUtils_OpenCollectionProperty(object p0)
		{
			return TextRes.GetString("ValidationUtils_OpenCollectionProperty", new object[] { p0 });
		}

		// Token: 0x060017CF RID: 6095 RVA: 0x0005241C File Offset: 0x0005061C
		internal static string ValidationUtils_OpenStreamProperty(object p0)
		{
			return TextRes.GetString("ValidationUtils_OpenStreamProperty", new object[] { p0 });
		}

		// Token: 0x060017D0 RID: 6096 RVA: 0x00052440 File Offset: 0x00050640
		internal static string ValidationUtils_InvalidCollectionTypeReference(object p0)
		{
			return TextRes.GetString("ValidationUtils_InvalidCollectionTypeReference", new object[] { p0 });
		}

		// Token: 0x060017D1 RID: 6097 RVA: 0x00052464 File Offset: 0x00050664
		internal static string ValidationUtils_EntryWithMediaResourceAndNonMLEType(object p0)
		{
			return TextRes.GetString("ValidationUtils_EntryWithMediaResourceAndNonMLEType", new object[] { p0 });
		}

		// Token: 0x060017D2 RID: 6098 RVA: 0x00052488 File Offset: 0x00050688
		internal static string ValidationUtils_EntryWithoutMediaResourceAndMLEType(object p0)
		{
			return TextRes.GetString("ValidationUtils_EntryWithoutMediaResourceAndMLEType", new object[] { p0 });
		}

		// Token: 0x060017D3 RID: 6099 RVA: 0x000524AC File Offset: 0x000506AC
		internal static string ValidationUtils_EntryTypeNotAssignableToExpectedType(object p0, object p1)
		{
			return TextRes.GetString("ValidationUtils_EntryTypeNotAssignableToExpectedType", new object[] { p0, p1 });
		}

		// Token: 0x060017D4 RID: 6100 RVA: 0x000524D4 File Offset: 0x000506D4
		internal static string ValidationUtils_OpenNavigationProperty(object p0, object p1)
		{
			return TextRes.GetString("ValidationUtils_OpenNavigationProperty", new object[] { p0, p1 });
		}

		// Token: 0x060017D5 RID: 6101 RVA: 0x000524FC File Offset: 0x000506FC
		internal static string ValidationUtils_NavigationPropertyExpected(object p0, object p1, object p2)
		{
			return TextRes.GetString("ValidationUtils_NavigationPropertyExpected", new object[] { p0, p1, p2 });
		}

		// Token: 0x060017D6 RID: 6102 RVA: 0x00052528 File Offset: 0x00050728
		internal static string ValidationUtils_InvalidBatchBoundaryDelimiterLength(object p0, object p1)
		{
			return TextRes.GetString("ValidationUtils_InvalidBatchBoundaryDelimiterLength", new object[] { p0, p1 });
		}

		// Token: 0x060017D7 RID: 6103 RVA: 0x00052550 File Offset: 0x00050750
		internal static string ValidationUtils_RecursionDepthLimitReached(object p0)
		{
			return TextRes.GetString("ValidationUtils_RecursionDepthLimitReached", new object[] { p0 });
		}

		// Token: 0x060017D8 RID: 6104 RVA: 0x00052574 File Offset: 0x00050774
		internal static string ValidationUtils_MaxDepthOfNestedEntriesExceeded(object p0)
		{
			return TextRes.GetString("ValidationUtils_MaxDepthOfNestedEntriesExceeded", new object[] { p0 });
		}

		// Token: 0x060017D9 RID: 6105 RVA: 0x00052598 File Offset: 0x00050798
		internal static string ValidationUtils_NullCollectionItemForNonNullableType(object p0)
		{
			return TextRes.GetString("ValidationUtils_NullCollectionItemForNonNullableType", new object[] { p0 });
		}

		// Token: 0x060017DA RID: 6106 RVA: 0x000525BC File Offset: 0x000507BC
		internal static string ValidationUtils_PropertiesMustNotContainReservedChars(object p0, object p1)
		{
			return TextRes.GetString("ValidationUtils_PropertiesMustNotContainReservedChars", new object[] { p0, p1 });
		}

		// Token: 0x060017DB RID: 6107 RVA: 0x000525E4 File Offset: 0x000507E4
		internal static string ValidationUtils_MaxNumberOfEntityPropertyMappingsExceeded(object p0, object p1)
		{
			return TextRes.GetString("ValidationUtils_MaxNumberOfEntityPropertyMappingsExceeded", new object[] { p0, p1 });
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x060017DC RID: 6108 RVA: 0x0005260B File Offset: 0x0005080B
		internal static string ValidationUtils_WorkspaceCollectionsMustNotContainNullItem
		{
			get
			{
				return TextRes.GetString("ValidationUtils_WorkspaceCollectionsMustNotContainNullItem");
			}
		}

		// Token: 0x060017DD RID: 6109 RVA: 0x00052618 File Offset: 0x00050818
		internal static string ValidationUtils_InvalidMetadataReferenceProperty(object p0)
		{
			return TextRes.GetString("ValidationUtils_InvalidMetadataReferenceProperty", new object[] { p0 });
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x060017DE RID: 6110 RVA: 0x0005263B File Offset: 0x0005083B
		internal static string ODataAtomWriter_FeedsMustHaveNonEmptyId
		{
			get
			{
				return TextRes.GetString("ODataAtomWriter_FeedsMustHaveNonEmptyId");
			}
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x060017DF RID: 6111 RVA: 0x00052647 File Offset: 0x00050847
		internal static string WriterValidationUtils_PropertyMustNotBeNull
		{
			get
			{
				return TextRes.GetString("WriterValidationUtils_PropertyMustNotBeNull");
			}
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x060017E0 RID: 6112 RVA: 0x00052653 File Offset: 0x00050853
		internal static string WriterValidationUtils_PropertiesMustHaveNonEmptyName
		{
			get
			{
				return TextRes.GetString("WriterValidationUtils_PropertiesMustHaveNonEmptyName");
			}
		}

		// Token: 0x060017E1 RID: 6113 RVA: 0x00052660 File Offset: 0x00050860
		internal static string WriterValidationUtils_PropertyNameDoesntMatchFunctionImportName(object p0, object p1)
		{
			return TextRes.GetString("WriterValidationUtils_PropertyNameDoesntMatchFunctionImportName", new object[] { p0, p1 });
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x060017E2 RID: 6114 RVA: 0x00052687 File Offset: 0x00050887
		internal static string WriterValidationUtils_MissingTypeNameWithMetadata
		{
			get
			{
				return TextRes.GetString("WriterValidationUtils_MissingTypeNameWithMetadata");
			}
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x060017E3 RID: 6115 RVA: 0x00052693 File Offset: 0x00050893
		internal static string WriterValidationUtils_NextPageLinkInRequest
		{
			get
			{
				return TextRes.GetString("WriterValidationUtils_NextPageLinkInRequest");
			}
		}

		// Token: 0x060017E4 RID: 6116 RVA: 0x000526A0 File Offset: 0x000508A0
		internal static string WriterValidationUtils_ResourceCollectionMustHaveUniqueName(object p0)
		{
			return TextRes.GetString("WriterValidationUtils_ResourceCollectionMustHaveUniqueName", new object[] { p0 });
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x060017E5 RID: 6117 RVA: 0x000526C3 File Offset: 0x000508C3
		internal static string WriterValidationUtils_DefaultStreamWithContentTypeWithoutReadLink
		{
			get
			{
				return TextRes.GetString("WriterValidationUtils_DefaultStreamWithContentTypeWithoutReadLink");
			}
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x060017E6 RID: 6118 RVA: 0x000526CF File Offset: 0x000508CF
		internal static string WriterValidationUtils_DefaultStreamWithReadLinkWithoutContentType
		{
			get
			{
				return TextRes.GetString("WriterValidationUtils_DefaultStreamWithReadLinkWithoutContentType");
			}
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x060017E7 RID: 6119 RVA: 0x000526DB File Offset: 0x000508DB
		internal static string WriterValidationUtils_StreamReferenceValueMustHaveEditLinkOrReadLink
		{
			get
			{
				return TextRes.GetString("WriterValidationUtils_StreamReferenceValueMustHaveEditLinkOrReadLink");
			}
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x060017E8 RID: 6120 RVA: 0x000526E7 File Offset: 0x000508E7
		internal static string WriterValidationUtils_StreamReferenceValueMustHaveEditLinkToHaveETag
		{
			get
			{
				return TextRes.GetString("WriterValidationUtils_StreamReferenceValueMustHaveEditLinkToHaveETag");
			}
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x060017E9 RID: 6121 RVA: 0x000526F3 File Offset: 0x000508F3
		internal static string WriterValidationUtils_StreamReferenceValueEmptyContentType
		{
			get
			{
				return TextRes.GetString("WriterValidationUtils_StreamReferenceValueEmptyContentType");
			}
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x060017EA RID: 6122 RVA: 0x000526FF File Offset: 0x000508FF
		internal static string WriterValidationUtils_EntriesMustHaveNonEmptyId
		{
			get
			{
				return TextRes.GetString("WriterValidationUtils_EntriesMustHaveNonEmptyId");
			}
		}

		// Token: 0x060017EB RID: 6123 RVA: 0x0005270C File Offset: 0x0005090C
		internal static string WriterValidationUtils_MessageWriterSettingsBaseUriMustBeNullOrAbsolute(object p0)
		{
			return TextRes.GetString("WriterValidationUtils_MessageWriterSettingsBaseUriMustBeNullOrAbsolute", new object[] { p0 });
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x060017EC RID: 6124 RVA: 0x0005272F File Offset: 0x0005092F
		internal static string WriterValidationUtils_EntityReferenceLinkUrlMustNotBeNull
		{
			get
			{
				return TextRes.GetString("WriterValidationUtils_EntityReferenceLinkUrlMustNotBeNull");
			}
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x060017ED RID: 6125 RVA: 0x0005273B File Offset: 0x0005093B
		internal static string WriterValidationUtils_EntityReferenceLinksLinkMustNotBeNull
		{
			get
			{
				return TextRes.GetString("WriterValidationUtils_EntityReferenceLinksLinkMustNotBeNull");
			}
		}

		// Token: 0x060017EE RID: 6126 RVA: 0x00052748 File Offset: 0x00050948
		internal static string WriterValidationUtils_EntryTypeInExpandedLinkNotCompatibleWithNavigationPropertyType(object p0, object p1)
		{
			return TextRes.GetString("WriterValidationUtils_EntryTypeInExpandedLinkNotCompatibleWithNavigationPropertyType", new object[] { p0, p1 });
		}

		// Token: 0x060017EF RID: 6127 RVA: 0x00052770 File Offset: 0x00050970
		internal static string WriterValidationUtils_ExpandedLinkIsCollectionTrueWithEntryContent(object p0)
		{
			return TextRes.GetString("WriterValidationUtils_ExpandedLinkIsCollectionTrueWithEntryContent", new object[] { p0 });
		}

		// Token: 0x060017F0 RID: 6128 RVA: 0x00052794 File Offset: 0x00050994
		internal static string WriterValidationUtils_ExpandedLinkIsCollectionFalseWithFeedContent(object p0)
		{
			return TextRes.GetString("WriterValidationUtils_ExpandedLinkIsCollectionFalseWithFeedContent", new object[] { p0 });
		}

		// Token: 0x060017F1 RID: 6129 RVA: 0x000527B8 File Offset: 0x000509B8
		internal static string WriterValidationUtils_ExpandedLinkIsCollectionTrueWithEntryMetadata(object p0)
		{
			return TextRes.GetString("WriterValidationUtils_ExpandedLinkIsCollectionTrueWithEntryMetadata", new object[] { p0 });
		}

		// Token: 0x060017F2 RID: 6130 RVA: 0x000527DC File Offset: 0x000509DC
		internal static string WriterValidationUtils_ExpandedLinkIsCollectionFalseWithFeedMetadata(object p0)
		{
			return TextRes.GetString("WriterValidationUtils_ExpandedLinkIsCollectionFalseWithFeedMetadata", new object[] { p0 });
		}

		// Token: 0x060017F3 RID: 6131 RVA: 0x00052800 File Offset: 0x00050A00
		internal static string WriterValidationUtils_ExpandedLinkWithFeedPayloadAndEntryMetadata(object p0)
		{
			return TextRes.GetString("WriterValidationUtils_ExpandedLinkWithFeedPayloadAndEntryMetadata", new object[] { p0 });
		}

		// Token: 0x060017F4 RID: 6132 RVA: 0x00052824 File Offset: 0x00050A24
		internal static string WriterValidationUtils_ExpandedLinkWithEntryPayloadAndFeedMetadata(object p0)
		{
			return TextRes.GetString("WriterValidationUtils_ExpandedLinkWithEntryPayloadAndFeedMetadata", new object[] { p0 });
		}

		// Token: 0x060017F5 RID: 6133 RVA: 0x00052848 File Offset: 0x00050A48
		internal static string WriterValidationUtils_CollectionPropertiesMustNotHaveNullValue(object p0)
		{
			return TextRes.GetString("WriterValidationUtils_CollectionPropertiesMustNotHaveNullValue", new object[] { p0 });
		}

		// Token: 0x060017F6 RID: 6134 RVA: 0x0005286C File Offset: 0x00050A6C
		internal static string WriterValidationUtils_NonNullablePropertiesMustNotHaveNullValue(object p0, object p1)
		{
			return TextRes.GetString("WriterValidationUtils_NonNullablePropertiesMustNotHaveNullValue", new object[] { p0, p1 });
		}

		// Token: 0x060017F7 RID: 6135 RVA: 0x00052894 File Offset: 0x00050A94
		internal static string WriterValidationUtils_StreamPropertiesMustNotHaveNullValue(object p0)
		{
			return TextRes.GetString("WriterValidationUtils_StreamPropertiesMustNotHaveNullValue", new object[] { p0 });
		}

		// Token: 0x060017F8 RID: 6136 RVA: 0x000528B8 File Offset: 0x00050AB8
		internal static string WriterValidationUtils_OperationInRequest(object p0)
		{
			return TextRes.GetString("WriterValidationUtils_OperationInRequest", new object[] { p0 });
		}

		// Token: 0x060017F9 RID: 6137 RVA: 0x000528DC File Offset: 0x00050ADC
		internal static string WriterValidationUtils_AssociationLinkInRequest(object p0)
		{
			return TextRes.GetString("WriterValidationUtils_AssociationLinkInRequest", new object[] { p0 });
		}

		// Token: 0x060017FA RID: 6138 RVA: 0x00052900 File Offset: 0x00050B00
		internal static string WriterValidationUtils_StreamPropertyInRequest(object p0)
		{
			return TextRes.GetString("WriterValidationUtils_StreamPropertyInRequest", new object[] { p0 });
		}

		// Token: 0x060017FB RID: 6139 RVA: 0x00052924 File Offset: 0x00050B24
		internal static string WriterValidationUtils_MessageWriterSettingsMetadataDocumentUriMustBeNullOrAbsolute(object p0)
		{
			return TextRes.GetString("WriterValidationUtils_MessageWriterSettingsMetadataDocumentUriMustBeNullOrAbsolute", new object[] { p0 });
		}

		// Token: 0x060017FC RID: 6140 RVA: 0x00052948 File Offset: 0x00050B48
		internal static string WriterValidationUtils_NavigationLinkMustSpecifyUrl(object p0)
		{
			return TextRes.GetString("WriterValidationUtils_NavigationLinkMustSpecifyUrl", new object[] { p0 });
		}

		// Token: 0x060017FD RID: 6141 RVA: 0x0005296C File Offset: 0x00050B6C
		internal static string WriterValidationUtils_NavigationLinkMustSpecifyIsCollection(object p0)
		{
			return TextRes.GetString("WriterValidationUtils_NavigationLinkMustSpecifyIsCollection", new object[] { p0 });
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x060017FE RID: 6142 RVA: 0x0005298F File Offset: 0x00050B8F
		internal static string WriterValidationUtils_MessageWriterSettingsJsonPaddingOnRequestMessage
		{
			get
			{
				return TextRes.GetString("WriterValidationUtils_MessageWriterSettingsJsonPaddingOnRequestMessage");
			}
		}

		// Token: 0x060017FF RID: 6143 RVA: 0x0005299C File Offset: 0x00050B9C
		internal static string XmlReaderExtension_InvalidNodeInStringValue(object p0)
		{
			return TextRes.GetString("XmlReaderExtension_InvalidNodeInStringValue", new object[] { p0 });
		}

		// Token: 0x06001800 RID: 6144 RVA: 0x000529C0 File Offset: 0x00050BC0
		internal static string XmlReaderExtension_InvalidRootNode(object p0)
		{
			return TextRes.GetString("XmlReaderExtension_InvalidRootNode", new object[] { p0 });
		}

		// Token: 0x06001801 RID: 6145 RVA: 0x000529E4 File Offset: 0x00050BE4
		internal static string ODataAtomInputContext_NonEmptyElementWithNullAttribute(object p0)
		{
			return TextRes.GetString("ODataAtomInputContext_NonEmptyElementWithNullAttribute", new object[] { p0 });
		}

		// Token: 0x06001802 RID: 6146 RVA: 0x00052A08 File Offset: 0x00050C08
		internal static string ODataMetadataInputContext_ErrorReadingMetadata(object p0)
		{
			return TextRes.GetString("ODataMetadataInputContext_ErrorReadingMetadata", new object[] { p0 });
		}

		// Token: 0x06001803 RID: 6147 RVA: 0x00052A2C File Offset: 0x00050C2C
		internal static string ODataMetadataOutputContext_ErrorWritingMetadata(object p0)
		{
			return TextRes.GetString("ODataMetadataOutputContext_ErrorWritingMetadata", new object[] { p0 });
		}

		// Token: 0x06001804 RID: 6148 RVA: 0x00052A50 File Offset: 0x00050C50
		internal static string EpmExtensionMethods_InvalidKeepInContentOnType(object p0, object p1)
		{
			return TextRes.GetString("EpmExtensionMethods_InvalidKeepInContentOnType", new object[] { p0, p1 });
		}

		// Token: 0x06001805 RID: 6149 RVA: 0x00052A78 File Offset: 0x00050C78
		internal static string EpmExtensionMethods_InvalidKeepInContentOnProperty(object p0, object p1, object p2)
		{
			return TextRes.GetString("EpmExtensionMethods_InvalidKeepInContentOnProperty", new object[] { p0, p1, p2 });
		}

		// Token: 0x06001806 RID: 6150 RVA: 0x00052AA4 File Offset: 0x00050CA4
		internal static string EpmExtensionMethods_InvalidTargetTextContentKindOnType(object p0, object p1)
		{
			return TextRes.GetString("EpmExtensionMethods_InvalidTargetTextContentKindOnType", new object[] { p0, p1 });
		}

		// Token: 0x06001807 RID: 6151 RVA: 0x00052ACC File Offset: 0x00050CCC
		internal static string EpmExtensionMethods_InvalidTargetTextContentKindOnProperty(object p0, object p1, object p2)
		{
			return TextRes.GetString("EpmExtensionMethods_InvalidTargetTextContentKindOnProperty", new object[] { p0, p1, p2 });
		}

		// Token: 0x06001808 RID: 6152 RVA: 0x00052AF8 File Offset: 0x00050CF8
		internal static string EpmExtensionMethods_MissingAttributeOnType(object p0, object p1)
		{
			return TextRes.GetString("EpmExtensionMethods_MissingAttributeOnType", new object[] { p0, p1 });
		}

		// Token: 0x06001809 RID: 6153 RVA: 0x00052B20 File Offset: 0x00050D20
		internal static string EpmExtensionMethods_MissingAttributeOnProperty(object p0, object p1, object p2)
		{
			return TextRes.GetString("EpmExtensionMethods_MissingAttributeOnProperty", new object[] { p0, p1, p2 });
		}

		// Token: 0x0600180A RID: 6154 RVA: 0x00052B4C File Offset: 0x00050D4C
		internal static string EpmExtensionMethods_AttributeNotAllowedForCustomMappingOnType(object p0, object p1)
		{
			return TextRes.GetString("EpmExtensionMethods_AttributeNotAllowedForCustomMappingOnType", new object[] { p0, p1 });
		}

		// Token: 0x0600180B RID: 6155 RVA: 0x00052B74 File Offset: 0x00050D74
		internal static string EpmExtensionMethods_AttributeNotAllowedForCustomMappingOnProperty(object p0, object p1, object p2)
		{
			return TextRes.GetString("EpmExtensionMethods_AttributeNotAllowedForCustomMappingOnProperty", new object[] { p0, p1, p2 });
		}

		// Token: 0x0600180C RID: 6156 RVA: 0x00052BA0 File Offset: 0x00050DA0
		internal static string EpmExtensionMethods_AttributeNotAllowedForAtomPubMappingOnType(object p0, object p1)
		{
			return TextRes.GetString("EpmExtensionMethods_AttributeNotAllowedForAtomPubMappingOnType", new object[] { p0, p1 });
		}

		// Token: 0x0600180D RID: 6157 RVA: 0x00052BC8 File Offset: 0x00050DC8
		internal static string EpmExtensionMethods_AttributeNotAllowedForAtomPubMappingOnProperty(object p0, object p1, object p2)
		{
			return TextRes.GetString("EpmExtensionMethods_AttributeNotAllowedForAtomPubMappingOnProperty", new object[] { p0, p1, p2 });
		}

		// Token: 0x0600180E RID: 6158 RVA: 0x00052BF4 File Offset: 0x00050DF4
		internal static string EpmExtensionMethods_CannotConvertEdmAnnotationValue(object p0, object p1, object p2)
		{
			return TextRes.GetString("EpmExtensionMethods_CannotConvertEdmAnnotationValue", new object[] { p0, p1, p2 });
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x0600180F RID: 6159 RVA: 0x00052C1F File Offset: 0x00050E1F
		internal static string ODataAtomReader_MediaLinkEntryMismatch
		{
			get
			{
				return TextRes.GetString("ODataAtomReader_MediaLinkEntryMismatch");
			}
		}

		// Token: 0x06001810 RID: 6160 RVA: 0x00052C2C File Offset: 0x00050E2C
		internal static string ODataAtomReader_FeedNavigationLinkForResourceReferenceProperty(object p0)
		{
			return TextRes.GetString("ODataAtomReader_FeedNavigationLinkForResourceReferenceProperty", new object[] { p0 });
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06001811 RID: 6161 RVA: 0x00052C4F File Offset: 0x00050E4F
		internal static string ODataAtomReader_ExpandedFeedInEntryNavigationLink
		{
			get
			{
				return TextRes.GetString("ODataAtomReader_ExpandedFeedInEntryNavigationLink");
			}
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06001812 RID: 6162 RVA: 0x00052C5B File Offset: 0x00050E5B
		internal static string ODataAtomReader_ExpandedEntryInFeedNavigationLink
		{
			get
			{
				return TextRes.GetString("ODataAtomReader_ExpandedEntryInFeedNavigationLink");
			}
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06001813 RID: 6163 RVA: 0x00052C67 File Offset: 0x00050E67
		internal static string ODataAtomReader_DeferredEntryInFeedNavigationLink
		{
			get
			{
				return TextRes.GetString("ODataAtomReader_DeferredEntryInFeedNavigationLink");
			}
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06001814 RID: 6164 RVA: 0x00052C73 File Offset: 0x00050E73
		internal static string ODataAtomReader_EntryXmlCustomizationCallbackReturnedSameInstance
		{
			get
			{
				return TextRes.GetString("ODataAtomReader_EntryXmlCustomizationCallbackReturnedSameInstance");
			}
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06001815 RID: 6165 RVA: 0x00052C7F File Offset: 0x00050E7F
		internal static string ODataAtomReaderUtils_InvalidTypeName
		{
			get
			{
				return TextRes.GetString("ODataAtomReaderUtils_InvalidTypeName");
			}
		}

		// Token: 0x06001816 RID: 6166 RVA: 0x00052C8C File Offset: 0x00050E8C
		internal static string ODataAtomDeserializer_RelativeUriUsedWithoutBaseUriSpecified(object p0)
		{
			return TextRes.GetString("ODataAtomDeserializer_RelativeUriUsedWithoutBaseUriSpecified", new object[] { p0 });
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06001817 RID: 6167 RVA: 0x00052CAF File Offset: 0x00050EAF
		internal static string ODataAtomCollectionDeserializer_TypeOrNullAttributeNotAllowed
		{
			get
			{
				return TextRes.GetString("ODataAtomCollectionDeserializer_TypeOrNullAttributeNotAllowed");
			}
		}

		// Token: 0x06001818 RID: 6168 RVA: 0x00052CBC File Offset: 0x00050EBC
		internal static string ODataAtomCollectionDeserializer_WrongCollectionItemElementName(object p0, object p1)
		{
			return TextRes.GetString("ODataAtomCollectionDeserializer_WrongCollectionItemElementName", new object[] { p0, p1 });
		}

		// Token: 0x06001819 RID: 6169 RVA: 0x00052CE4 File Offset: 0x00050EE4
		internal static string ODataAtomCollectionDeserializer_TopLevelCollectionElementWrongNamespace(object p0, object p1)
		{
			return TextRes.GetString("ODataAtomCollectionDeserializer_TopLevelCollectionElementWrongNamespace", new object[] { p0, p1 });
		}

		// Token: 0x0600181A RID: 6170 RVA: 0x00052D0C File Offset: 0x00050F0C
		internal static string ODataAtomPropertyAndValueDeserializer_TopLevelPropertyElementWrongNamespace(object p0, object p1)
		{
			return TextRes.GetString("ODataAtomPropertyAndValueDeserializer_TopLevelPropertyElementWrongNamespace", new object[] { p0, p1 });
		}

		// Token: 0x0600181B RID: 6171 RVA: 0x00052D34 File Offset: 0x00050F34
		internal static string ODataAtomPropertyAndValueDeserializer_NonEmptyElementWithNullAttribute(object p0)
		{
			return TextRes.GetString("ODataAtomPropertyAndValueDeserializer_NonEmptyElementWithNullAttribute", new object[] { p0 });
		}

		// Token: 0x0600181C RID: 6172 RVA: 0x00052D58 File Offset: 0x00050F58
		internal static string ODataAtomPropertyAndValueDeserializer_InvalidCollectionElement(object p0, object p1)
		{
			return TextRes.GetString("ODataAtomPropertyAndValueDeserializer_InvalidCollectionElement", new object[] { p0, p1 });
		}

		// Token: 0x0600181D RID: 6173 RVA: 0x00052D80 File Offset: 0x00050F80
		internal static string ODataAtomPropertyAndValueDeserializer_NavigationPropertyInProperties(object p0, object p1)
		{
			return TextRes.GetString("ODataAtomPropertyAndValueDeserializer_NavigationPropertyInProperties", new object[] { p0, p1 });
		}

		// Token: 0x0600181E RID: 6174 RVA: 0x00052DA8 File Offset: 0x00050FA8
		internal static string ODataAtomPropertyAndValueSerializer_NullValueNotAllowedForInstanceAnnotation(object p0, object p1)
		{
			return TextRes.GetString("ODataAtomPropertyAndValueSerializer_NullValueNotAllowedForInstanceAnnotation", new object[] { p0, p1 });
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x0600181F RID: 6175 RVA: 0x00052DCF File Offset: 0x00050FCF
		internal static string EdmLibraryExtensions_CollectionItemCanBeOnlyPrimitiveOrComplex
		{
			get
			{
				return TextRes.GetString("EdmLibraryExtensions_CollectionItemCanBeOnlyPrimitiveOrComplex");
			}
		}

		// Token: 0x06001820 RID: 6176 RVA: 0x00052DDC File Offset: 0x00050FDC
		internal static string ODataAtomEntryAndFeedDeserializer_ElementExpected(object p0)
		{
			return TextRes.GetString("ODataAtomEntryAndFeedDeserializer_ElementExpected", new object[] { p0 });
		}

		// Token: 0x06001821 RID: 6177 RVA: 0x00052E00 File Offset: 0x00051000
		internal static string ODataAtomEntryAndFeedDeserializer_EntryElementWrongName(object p0, object p1)
		{
			return TextRes.GetString("ODataAtomEntryAndFeedDeserializer_EntryElementWrongName", new object[] { p0, p1 });
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06001822 RID: 6178 RVA: 0x00052E27 File Offset: 0x00051027
		internal static string ODataAtomEntryAndFeedDeserializer_ContentWithSourceLinkIsNotEmpty
		{
			get
			{
				return TextRes.GetString("ODataAtomEntryAndFeedDeserializer_ContentWithSourceLinkIsNotEmpty");
			}
		}

		// Token: 0x06001823 RID: 6179 RVA: 0x00052E34 File Offset: 0x00051034
		internal static string ODataAtomEntryAndFeedDeserializer_ContentWithWrongType(object p0)
		{
			return TextRes.GetString("ODataAtomEntryAndFeedDeserializer_ContentWithWrongType", new object[] { p0 });
		}

		// Token: 0x06001824 RID: 6180 RVA: 0x00052E58 File Offset: 0x00051058
		internal static string ODataAtomEntryAndFeedDeserializer_ContentWithInvalidNode(object p0)
		{
			return TextRes.GetString("ODataAtomEntryAndFeedDeserializer_ContentWithInvalidNode", new object[] { p0 });
		}

		// Token: 0x06001825 RID: 6181 RVA: 0x00052E7C File Offset: 0x0005107C
		internal static string ODataAtomEntryAndFeedDeserializer_FeedElementWrongName(object p0, object p1)
		{
			return TextRes.GetString("ODataAtomEntryAndFeedDeserializer_FeedElementWrongName", new object[] { p0, p1 });
		}

		// Token: 0x06001826 RID: 6182 RVA: 0x00052EA4 File Offset: 0x000510A4
		internal static string ODataAtomEntryAndFeedDeserializer_UnknownElementInInline(object p0)
		{
			return TextRes.GetString("ODataAtomEntryAndFeedDeserializer_UnknownElementInInline", new object[] { p0 });
		}

		// Token: 0x06001827 RID: 6183 RVA: 0x00052EC8 File Offset: 0x000510C8
		internal static string ODataAtomEntryAndFeedDeserializer_MultipleExpansionsInInline(object p0)
		{
			return TextRes.GetString("ODataAtomEntryAndFeedDeserializer_MultipleExpansionsInInline", new object[] { p0 });
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06001828 RID: 6184 RVA: 0x00052EEB File Offset: 0x000510EB
		internal static string ODataAtomEntryAndFeedDeserializer_MultipleInlineElementsInLink
		{
			get
			{
				return TextRes.GetString("ODataAtomEntryAndFeedDeserializer_MultipleInlineElementsInLink");
			}
		}

		// Token: 0x06001829 RID: 6185 RVA: 0x00052EF8 File Offset: 0x000510F8
		internal static string ODataAtomEntryAndFeedDeserializer_StreamPropertyWithMultipleEditLinks(object p0)
		{
			return TextRes.GetString("ODataAtomEntryAndFeedDeserializer_StreamPropertyWithMultipleEditLinks", new object[] { p0 });
		}

		// Token: 0x0600182A RID: 6186 RVA: 0x00052F1C File Offset: 0x0005111C
		internal static string ODataAtomEntryAndFeedDeserializer_StreamPropertyWithMultipleReadLinks(object p0)
		{
			return TextRes.GetString("ODataAtomEntryAndFeedDeserializer_StreamPropertyWithMultipleReadLinks", new object[] { p0 });
		}

		// Token: 0x0600182B RID: 6187 RVA: 0x00052F40 File Offset: 0x00051140
		internal static string ODataAtomEntryAndFeedDeserializer_StreamPropertyWithMultipleContentTypes(object p0)
		{
			return TextRes.GetString("ODataAtomEntryAndFeedDeserializer_StreamPropertyWithMultipleContentTypes", new object[] { p0 });
		}

		// Token: 0x0600182C RID: 6188 RVA: 0x00052F64 File Offset: 0x00051164
		internal static string ODataAtomEntryAndFeedDeserializer_StreamPropertyDuplicatePropertyName(object p0)
		{
			return TextRes.GetString("ODataAtomEntryAndFeedDeserializer_StreamPropertyDuplicatePropertyName", new object[] { p0 });
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x0600182D RID: 6189 RVA: 0x00052F87 File Offset: 0x00051187
		internal static string ODataAtomEntryAndFeedDeserializer_StreamPropertyWithEmptyName
		{
			get
			{
				return TextRes.GetString("ODataAtomEntryAndFeedDeserializer_StreamPropertyWithEmptyName");
			}
		}

		// Token: 0x0600182E RID: 6190 RVA: 0x00052F94 File Offset: 0x00051194
		internal static string ODataAtomEntryAndFeedDeserializer_OperationMissingMetadataAttribute(object p0)
		{
			return TextRes.GetString("ODataAtomEntryAndFeedDeserializer_OperationMissingMetadataAttribute", new object[] { p0 });
		}

		// Token: 0x0600182F RID: 6191 RVA: 0x00052FB8 File Offset: 0x000511B8
		internal static string ODataAtomEntryAndFeedDeserializer_OperationMissingTargetAttribute(object p0)
		{
			return TextRes.GetString("ODataAtomEntryAndFeedDeserializer_OperationMissingTargetAttribute", new object[] { p0 });
		}

		// Token: 0x06001830 RID: 6192 RVA: 0x00052FDC File Offset: 0x000511DC
		internal static string ODataAtomEntryAndFeedDeserializer_MultipleLinksInEntry(object p0)
		{
			return TextRes.GetString("ODataAtomEntryAndFeedDeserializer_MultipleLinksInEntry", new object[] { p0 });
		}

		// Token: 0x06001831 RID: 6193 RVA: 0x00053000 File Offset: 0x00051200
		internal static string ODataAtomEntryAndFeedDeserializer_MultipleLinksInFeed(object p0)
		{
			return TextRes.GetString("ODataAtomEntryAndFeedDeserializer_MultipleLinksInFeed", new object[] { p0 });
		}

		// Token: 0x06001832 RID: 6194 RVA: 0x00053024 File Offset: 0x00051224
		internal static string ODataAtomEntryAndFeedDeserializer_DuplicateElements(object p0, object p1)
		{
			return TextRes.GetString("ODataAtomEntryAndFeedDeserializer_DuplicateElements", new object[] { p0, p1 });
		}

		// Token: 0x06001833 RID: 6195 RVA: 0x0005304C File Offset: 0x0005124C
		internal static string ODataAtomEntryAndFeedDeserializer_InvalidTypeAttributeOnAssociationLink(object p0)
		{
			return TextRes.GetString("ODataAtomEntryAndFeedDeserializer_InvalidTypeAttributeOnAssociationLink", new object[] { p0 });
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06001834 RID: 6196 RVA: 0x0005306F File Offset: 0x0005126F
		internal static string ODataAtomEntryAndFeedDeserializer_EncounteredAnnotationInNestedFeed
		{
			get
			{
				return TextRes.GetString("ODataAtomEntryAndFeedDeserializer_EncounteredAnnotationInNestedFeed");
			}
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06001835 RID: 6197 RVA: 0x0005307B File Offset: 0x0005127B
		internal static string ODataAtomEntryAndFeedDeserializer_EncounteredDeltaLinkInNestedFeed
		{
			get
			{
				return TextRes.GetString("ODataAtomEntryAndFeedDeserializer_EncounteredDeltaLinkInNestedFeed");
			}
		}

		// Token: 0x06001836 RID: 6198 RVA: 0x00053088 File Offset: 0x00051288
		internal static string ODataAtomEntryAndFeedDeserializer_AnnotationWithNonDotTarget(object p0, object p1)
		{
			return TextRes.GetString("ODataAtomEntryAndFeedDeserializer_AnnotationWithNonDotTarget", new object[] { p0, p1 });
		}

		// Token: 0x06001837 RID: 6199 RVA: 0x000530B0 File Offset: 0x000512B0
		internal static string ODataAtomServiceDocumentDeserializer_ServiceDocumentRootElementWrongNameOrNamespace(object p0, object p1)
		{
			return TextRes.GetString("ODataAtomServiceDocumentDeserializer_ServiceDocumentRootElementWrongNameOrNamespace", new object[] { p0, p1 });
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06001838 RID: 6200 RVA: 0x000530D7 File Offset: 0x000512D7
		internal static string ODataAtomServiceDocumentDeserializer_MissingWorkspaceElement
		{
			get
			{
				return TextRes.GetString("ODataAtomServiceDocumentDeserializer_MissingWorkspaceElement");
			}
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06001839 RID: 6201 RVA: 0x000530E3 File Offset: 0x000512E3
		internal static string ODataAtomServiceDocumentDeserializer_MultipleWorkspaceElementsFound
		{
			get
			{
				return TextRes.GetString("ODataAtomServiceDocumentDeserializer_MultipleWorkspaceElementsFound");
			}
		}

		// Token: 0x0600183A RID: 6202 RVA: 0x000530F0 File Offset: 0x000512F0
		internal static string ODataAtomServiceDocumentDeserializer_UnexpectedElementInServiceDocument(object p0)
		{
			return TextRes.GetString("ODataAtomServiceDocumentDeserializer_UnexpectedElementInServiceDocument", new object[] { p0 });
		}

		// Token: 0x0600183B RID: 6203 RVA: 0x00053114 File Offset: 0x00051314
		internal static string ODataAtomServiceDocumentDeserializer_UnexpectedElementInWorkspace(object p0)
		{
			return TextRes.GetString("ODataAtomServiceDocumentDeserializer_UnexpectedElementInWorkspace", new object[] { p0 });
		}

		// Token: 0x0600183C RID: 6204 RVA: 0x00053138 File Offset: 0x00051338
		internal static string ODataAtomServiceDocumentDeserializer_UnexpectedElementInResourceCollection(object p0)
		{
			return TextRes.GetString("ODataAtomServiceDocumentDeserializer_UnexpectedElementInResourceCollection", new object[] { p0 });
		}

		// Token: 0x0600183D RID: 6205 RVA: 0x0005315C File Offset: 0x0005135C
		internal static string ODataAtomEntryMetadataDeserializer_InvalidTextConstructKind(object p0, object p1)
		{
			return TextRes.GetString("ODataAtomEntryMetadataDeserializer_InvalidTextConstructKind", new object[] { p0, p1 });
		}

		// Token: 0x0600183E RID: 6206 RVA: 0x00053184 File Offset: 0x00051384
		internal static string ODataAtomMetadataDeserializer_MultipleSingletonMetadataElements(object p0, object p1)
		{
			return TextRes.GetString("ODataAtomMetadataDeserializer_MultipleSingletonMetadataElements", new object[] { p0, p1 });
		}

		// Token: 0x0600183F RID: 6207 RVA: 0x000531AC File Offset: 0x000513AC
		internal static string ODataAtomErrorDeserializer_InvalidRootElement(object p0, object p1)
		{
			return TextRes.GetString("ODataAtomErrorDeserializer_InvalidRootElement", new object[] { p0, p1 });
		}

		// Token: 0x06001840 RID: 6208 RVA: 0x000531D4 File Offset: 0x000513D4
		internal static string ODataAtomErrorDeserializer_MultipleErrorElementsWithSameName(object p0)
		{
			return TextRes.GetString("ODataAtomErrorDeserializer_MultipleErrorElementsWithSameName", new object[] { p0 });
		}

		// Token: 0x06001841 RID: 6209 RVA: 0x000531F8 File Offset: 0x000513F8
		internal static string ODataAtomErrorDeserializer_MultipleInnerErrorElementsWithSameName(object p0)
		{
			return TextRes.GetString("ODataAtomErrorDeserializer_MultipleInnerErrorElementsWithSameName", new object[] { p0 });
		}

		// Token: 0x06001842 RID: 6210 RVA: 0x0005321C File Offset: 0x0005141C
		internal static string ODataAtomEntityReferenceLinkDeserializer_InvalidEntityReferenceLinkStartElement(object p0, object p1)
		{
			return TextRes.GetString("ODataAtomEntityReferenceLinkDeserializer_InvalidEntityReferenceLinkStartElement", new object[] { p0, p1 });
		}

		// Token: 0x06001843 RID: 6211 RVA: 0x00053244 File Offset: 0x00051444
		internal static string ODataAtomEntityReferenceLinkDeserializer_InvalidEntityReferenceLinksStartElement(object p0, object p1)
		{
			return TextRes.GetString("ODataAtomEntityReferenceLinkDeserializer_InvalidEntityReferenceLinksStartElement", new object[] { p0, p1 });
		}

		// Token: 0x06001844 RID: 6212 RVA: 0x0005326C File Offset: 0x0005146C
		internal static string ODataAtomEntityReferenceLinkDeserializer_MultipleEntityReferenceLinksElementsWithSameName(object p0, object p1)
		{
			return TextRes.GetString("ODataAtomEntityReferenceLinkDeserializer_MultipleEntityReferenceLinksElementsWithSameName", new object[] { p0, p1 });
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x00053294 File Offset: 0x00051494
		internal static string EpmReader_OpenComplexOrCollectionEpmProperty(object p0)
		{
			return TextRes.GetString("EpmReader_OpenComplexOrCollectionEpmProperty", new object[] { p0 });
		}

		// Token: 0x06001846 RID: 6214 RVA: 0x000532B8 File Offset: 0x000514B8
		internal static string EpmSyndicationReader_MultipleValuesForNonCollectionProperty(object p0, object p1, object p2)
		{
			return TextRes.GetString("EpmSyndicationReader_MultipleValuesForNonCollectionProperty", new object[] { p0, p1, p2 });
		}

		// Token: 0x06001847 RID: 6215 RVA: 0x000532E4 File Offset: 0x000514E4
		internal static string ODataAtomServiceDocumentMetadataDeserializer_InvalidFixedAttributeValue(object p0)
		{
			return TextRes.GetString("ODataAtomServiceDocumentMetadataDeserializer_InvalidFixedAttributeValue", new object[] { p0 });
		}

		// Token: 0x06001848 RID: 6216 RVA: 0x00053308 File Offset: 0x00051508
		internal static string ODataAtomServiceDocumentMetadataDeserializer_MultipleTitleElementsFound(object p0)
		{
			return TextRes.GetString("ODataAtomServiceDocumentMetadataDeserializer_MultipleTitleElementsFound", new object[] { p0 });
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06001849 RID: 6217 RVA: 0x0005332B File Offset: 0x0005152B
		internal static string ODataAtomServiceDocumentMetadataDeserializer_MultipleAcceptElementsFoundInCollection
		{
			get
			{
				return TextRes.GetString("ODataAtomServiceDocumentMetadataDeserializer_MultipleAcceptElementsFoundInCollection");
			}
		}

		// Token: 0x0600184A RID: 6218 RVA: 0x00053338 File Offset: 0x00051538
		internal static string ODataAtomServiceDocumentMetadataSerializer_ResourceCollectionNameAndTitleMismatch(object p0, object p1)
		{
			return TextRes.GetString("ODataAtomServiceDocumentMetadataSerializer_ResourceCollectionNameAndTitleMismatch", new object[] { p0, p1 });
		}

		// Token: 0x0600184B RID: 6219 RVA: 0x00053360 File Offset: 0x00051560
		internal static string CollectionWithoutExpectedTypeValidator_InvalidItemTypeKind(object p0)
		{
			return TextRes.GetString("CollectionWithoutExpectedTypeValidator_InvalidItemTypeKind", new object[] { p0 });
		}

		// Token: 0x0600184C RID: 6220 RVA: 0x00053384 File Offset: 0x00051584
		internal static string CollectionWithoutExpectedTypeValidator_IncompatibleItemTypeKind(object p0, object p1)
		{
			return TextRes.GetString("CollectionWithoutExpectedTypeValidator_IncompatibleItemTypeKind", new object[] { p0, p1 });
		}

		// Token: 0x0600184D RID: 6221 RVA: 0x000533AC File Offset: 0x000515AC
		internal static string CollectionWithoutExpectedTypeValidator_IncompatibleItemTypeName(object p0, object p1)
		{
			return TextRes.GetString("CollectionWithoutExpectedTypeValidator_IncompatibleItemTypeName", new object[] { p0, p1 });
		}

		// Token: 0x0600184E RID: 6222 RVA: 0x000533D4 File Offset: 0x000515D4
		internal static string FeedWithoutExpectedTypeValidator_IncompatibleTypes(object p0, object p1)
		{
			return TextRes.GetString("FeedWithoutExpectedTypeValidator_IncompatibleTypes", new object[] { p0, p1 });
		}

		// Token: 0x0600184F RID: 6223 RVA: 0x000533FC File Offset: 0x000515FC
		internal static string MessageStreamWrappingStream_ByteLimitExceeded(object p0, object p1)
		{
			return TextRes.GetString("MessageStreamWrappingStream_ByteLimitExceeded", new object[] { p0, p1 });
		}

		// Token: 0x06001850 RID: 6224 RVA: 0x00053424 File Offset: 0x00051624
		internal static string MetadataUtils_ResolveTypeName(object p0)
		{
			return TextRes.GetString("MetadataUtils_ResolveTypeName", new object[] { p0 });
		}

		// Token: 0x06001851 RID: 6225 RVA: 0x00053448 File Offset: 0x00051648
		internal static string EdmValueUtils_UnsupportedPrimitiveType(object p0)
		{
			return TextRes.GetString("EdmValueUtils_UnsupportedPrimitiveType", new object[] { p0 });
		}

		// Token: 0x06001852 RID: 6226 RVA: 0x0005346C File Offset: 0x0005166C
		internal static string EdmValueUtils_IncorrectPrimitiveTypeKind(object p0, object p1, object p2)
		{
			return TextRes.GetString("EdmValueUtils_IncorrectPrimitiveTypeKind", new object[] { p0, p1, p2 });
		}

		// Token: 0x06001853 RID: 6227 RVA: 0x00053498 File Offset: 0x00051698
		internal static string EdmValueUtils_IncorrectPrimitiveTypeKindNoTypeName(object p0, object p1)
		{
			return TextRes.GetString("EdmValueUtils_IncorrectPrimitiveTypeKindNoTypeName", new object[] { p0, p1 });
		}

		// Token: 0x06001854 RID: 6228 RVA: 0x000534C0 File Offset: 0x000516C0
		internal static string EdmValueUtils_CannotConvertTypeToClrValue(object p0)
		{
			return TextRes.GetString("EdmValueUtils_CannotConvertTypeToClrValue", new object[] { p0 });
		}

		// Token: 0x06001855 RID: 6229 RVA: 0x000534E4 File Offset: 0x000516E4
		internal static string ODataEdmStructuredValue_UndeclaredProperty(object p0, object p1)
		{
			return TextRes.GetString("ODataEdmStructuredValue_UndeclaredProperty", new object[] { p0, p1 });
		}

		// Token: 0x06001856 RID: 6230 RVA: 0x0005350C File Offset: 0x0005170C
		internal static string ODataModelAnnotationEvaluator_AmbiguousAnnotationTerm(object p0, object p1)
		{
			return TextRes.GetString("ODataModelAnnotationEvaluator_AmbiguousAnnotationTerm", new object[] { p0, p1 });
		}

		// Token: 0x06001857 RID: 6231 RVA: 0x00053534 File Offset: 0x00051734
		internal static string ODataModelAnnotationEvaluator_AmbiguousAnnotationTermWithQualifier(object p0, object p1, object p2)
		{
			return TextRes.GetString("ODataModelAnnotationEvaluator_AmbiguousAnnotationTermWithQualifier", new object[] { p0, p1, p2 });
		}

		// Token: 0x06001858 RID: 6232 RVA: 0x00053560 File Offset: 0x00051760
		internal static string ODataModelAnnotationEvaluator_AnnotationTermWithInvalidQualifier(object p0, object p1, object p2)
		{
			return TextRes.GetString("ODataModelAnnotationEvaluator_AnnotationTermWithInvalidQualifier", new object[] { p0, p1, p2 });
		}

		// Token: 0x06001859 RID: 6233 RVA: 0x0005358C File Offset: 0x0005178C
		internal static string ODataModelAnnotationEvaluator_AnnotationTermWithUnsupportedQualifier(object p0, object p1, object p2, object p3)
		{
			return TextRes.GetString("ODataModelAnnotationEvaluator_AnnotationTermWithUnsupportedQualifier", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x0600185A RID: 6234 RVA: 0x000535BC File Offset: 0x000517BC
		internal static string ODataMetadataBuilder_MissingEntitySetUri(object p0)
		{
			return TextRes.GetString("ODataMetadataBuilder_MissingEntitySetUri", new object[] { p0 });
		}

		// Token: 0x0600185B RID: 6235 RVA: 0x000535E0 File Offset: 0x000517E0
		internal static string ODataMetadataBuilder_MissingSegmentForEntitySetUriSuffix(object p0, object p1)
		{
			return TextRes.GetString("ODataMetadataBuilder_MissingSegmentForEntitySetUriSuffix", new object[] { p0, p1 });
		}

		// Token: 0x0600185C RID: 6236 RVA: 0x00053608 File Offset: 0x00051808
		internal static string ODataMetadataBuilder_MissingEntityInstanceUri(object p0)
		{
			return TextRes.GetString("ODataMetadataBuilder_MissingEntityInstanceUri", new object[] { p0 });
		}

		// Token: 0x0600185D RID: 6237 RVA: 0x0005362C File Offset: 0x0005182C
		internal static string ODataJsonLightInputContext_EntityTypeMustBeCompatibleWithEntitySetBaseType(object p0, object p1, object p2)
		{
			return TextRes.GetString("ODataJsonLightInputContext_EntityTypeMustBeCompatibleWithEntitySetBaseType", new object[] { p0, p1, p2 });
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x0600185E RID: 6238 RVA: 0x00053657 File Offset: 0x00051857
		internal static string ODataJsonLightInputContext_MetadataDocumentUriMissing
		{
			get
			{
				return TextRes.GetString("ODataJsonLightInputContext_MetadataDocumentUriMissing");
			}
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x0600185F RID: 6239 RVA: 0x00053663 File Offset: 0x00051863
		internal static string ODataJsonLightInputContext_PayloadKindDetectionForRequest
		{
			get
			{
				return TextRes.GetString("ODataJsonLightInputContext_PayloadKindDetectionForRequest");
			}
		}

		// Token: 0x06001860 RID: 6240 RVA: 0x00053670 File Offset: 0x00051870
		internal static string ODataJsonLightInputContext_FunctionImportCannotBeNullForCreateParameterReader(object p0)
		{
			return TextRes.GetString("ODataJsonLightInputContext_FunctionImportCannotBeNullForCreateParameterReader", new object[] { p0 });
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06001861 RID: 6241 RVA: 0x00053693 File Offset: 0x00051893
		internal static string ODataJsonLightInputContext_NoEntitySetForRequest
		{
			get
			{
				return TextRes.GetString("ODataJsonLightInputContext_NoEntitySetForRequest");
			}
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06001862 RID: 6242 RVA: 0x0005369F File Offset: 0x0005189F
		internal static string ODataJsonLightInputContext_FunctionImportOrItemTypeRequiredForCollectionReaderInRequests
		{
			get
			{
				return TextRes.GetString("ODataJsonLightInputContext_FunctionImportOrItemTypeRequiredForCollectionReaderInRequests");
			}
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06001863 RID: 6243 RVA: 0x000536AB File Offset: 0x000518AB
		internal static string ODataJsonLightInputContext_NavigationPropertyRequiredForReadEntityReferenceLinkInRequests
		{
			get
			{
				return TextRes.GetString("ODataJsonLightInputContext_NavigationPropertyRequiredForReadEntityReferenceLinkInRequests");
			}
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06001864 RID: 6244 RVA: 0x000536B7 File Offset: 0x000518B7
		internal static string ODataJsonLightInputContext_ModelRequiredForReading
		{
			get
			{
				return TextRes.GetString("ODataJsonLightInputContext_ModelRequiredForReading");
			}
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06001865 RID: 6245 RVA: 0x000536C3 File Offset: 0x000518C3
		internal static string ODataJsonLightInputContext_BaseUriMustBeNonNullAndAbsolute
		{
			get
			{
				return TextRes.GetString("ODataJsonLightInputContext_BaseUriMustBeNonNullAndAbsolute");
			}
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06001866 RID: 6246 RVA: 0x000536CF File Offset: 0x000518CF
		internal static string ODataJsonLightInputContext_ItemTypeRequiredForCollectionReaderInRequests
		{
			get
			{
				return TextRes.GetString("ODataJsonLightInputContext_ItemTypeRequiredForCollectionReaderInRequests");
			}
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06001867 RID: 6247 RVA: 0x000536DB File Offset: 0x000518DB
		internal static string ODataJsonLightInputContext_NoItemTypeSpecified
		{
			get
			{
				return TextRes.GetString("ODataJsonLightInputContext_NoItemTypeSpecified");
			}
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06001868 RID: 6248 RVA: 0x000536E7 File Offset: 0x000518E7
		internal static string ODataJsonLightDeserializer_MetadataLinkNotFoundAsFirstProperty
		{
			get
			{
				return TextRes.GetString("ODataJsonLightDeserializer_MetadataLinkNotFoundAsFirstProperty");
			}
		}

		// Token: 0x06001869 RID: 6249 RVA: 0x000536F4 File Offset: 0x000518F4
		internal static string ODataJsonLightDeserializer_RequiredPropertyNotFound(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightDeserializer_RequiredPropertyNotFound", new object[] { p0, p1 });
		}

		// Token: 0x0600186A RID: 6250 RVA: 0x0005371C File Offset: 0x0005191C
		internal static string ODataJsonLightDeserializer_OnlyODataTypeAnnotationCanTargetInstanceAnnotation(object p0, object p1, object p2)
		{
			return TextRes.GetString("ODataJsonLightDeserializer_OnlyODataTypeAnnotationCanTargetInstanceAnnotation", new object[] { p0, p1, p2 });
		}

		// Token: 0x0600186B RID: 6251 RVA: 0x00053748 File Offset: 0x00051948
		internal static string ODataJsonLightDeserializer_AnnotationTargetingInstanceAnnotationWithoutValue(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightDeserializer_AnnotationTargetingInstanceAnnotationWithoutValue", new object[] { p0, p1 });
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x0600186C RID: 6252 RVA: 0x0005376F File Offset: 0x0005196F
		internal static string ODataJsonLightWriter_EntityReferenceLinkAfterFeedInRequest
		{
			get
			{
				return TextRes.GetString("ODataJsonLightWriter_EntityReferenceLinkAfterFeedInRequest");
			}
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x0600186D RID: 6253 RVA: 0x0005377B File Offset: 0x0005197B
		internal static string ODataJsonLightWriter_InstanceAnnotationNotSupportedOnExpandedFeed
		{
			get
			{
				return TextRes.GetString("ODataJsonLightWriter_InstanceAnnotationNotSupportedOnExpandedFeed");
			}
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x0600186E RID: 6254 RVA: 0x00053787 File Offset: 0x00051987
		internal static string ODataJsonLightOutputContext_MetadataDocumentUriMissing
		{
			get
			{
				return TextRes.GetString("ODataJsonLightOutputContext_MetadataDocumentUriMissing");
			}
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x0600186F RID: 6255 RVA: 0x00053793 File Offset: 0x00051993
		internal static string ODataJsonLightPropertyAndValueSerializer_NoExpectedTypeOrTypeNameSpecifiedForComplexValueRequest
		{
			get
			{
				return TextRes.GetString("ODataJsonLightPropertyAndValueSerializer_NoExpectedTypeOrTypeNameSpecifiedForComplexValueRequest");
			}
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06001870 RID: 6256 RVA: 0x0005379F File Offset: 0x0005199F
		internal static string ODataJsonLightPropertyAndValueSerializer_NoExpectedTypeOrTypeNameSpecifiedForCollectionValueInRequest
		{
			get
			{
				return TextRes.GetString("ODataJsonLightPropertyAndValueSerializer_NoExpectedTypeOrTypeNameSpecifiedForCollectionValueInRequest");
			}
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06001871 RID: 6257 RVA: 0x000537AB File Offset: 0x000519AB
		internal static string ODataJsonLightServiceDocumentSerializer_ResourceCollectionMustSpecifyName
		{
			get
			{
				return TextRes.GetString("ODataJsonLightServiceDocumentSerializer_ResourceCollectionMustSpecifyName");
			}
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06001872 RID: 6258 RVA: 0x000537B7 File Offset: 0x000519B7
		internal static string ODataFeedAndEntryTypeContext_MetadataOrSerializationInfoMissing
		{
			get
			{
				return TextRes.GetString("ODataFeedAndEntryTypeContext_MetadataOrSerializationInfoMissing");
			}
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06001873 RID: 6259 RVA: 0x000537C3 File Offset: 0x000519C3
		internal static string ODataFeedAndEntryTypeContext_ODataEntryTypeNameMissing
		{
			get
			{
				return TextRes.GetString("ODataFeedAndEntryTypeContext_ODataEntryTypeNameMissing");
			}
		}

		// Token: 0x06001874 RID: 6260 RVA: 0x000537D0 File Offset: 0x000519D0
		internal static string ODataJsonLightMetadataUriBuilder_ValidateDerivedType(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightMetadataUriBuilder_ValidateDerivedType", new object[] { p0, p1 });
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06001875 RID: 6261 RVA: 0x000537F7 File Offset: 0x000519F7
		internal static string ODataJsonLightMetadataUriBuilder_TypeNameMissingForTopLevelCollectionWhenWritingResponsePayload
		{
			get
			{
				return TextRes.GetString("ODataJsonLightMetadataUriBuilder_TypeNameMissingForTopLevelCollectionWhenWritingResponsePayload");
			}
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06001876 RID: 6262 RVA: 0x00053803 File Offset: 0x00051A03
		internal static string ODataJsonLightMetadataUriBuilder_EntitySetOrNavigationPropertyMissingForTopLevelEntityReferenceLinkResponse
		{
			get
			{
				return TextRes.GetString("ODataJsonLightMetadataUriBuilder_EntitySetOrNavigationPropertyMissingForTopLevelEntityReferenceLinkResponse");
			}
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06001877 RID: 6263 RVA: 0x0005380F File Offset: 0x00051A0F
		internal static string ODataJsonLightMetadataUriBuilder_EntitySetOrNavigationPropertyMissingForTopLevelEntityReferenceLinksResponse
		{
			get
			{
				return TextRes.GetString("ODataJsonLightMetadataUriBuilder_EntitySetOrNavigationPropertyMissingForTopLevelEntityReferenceLinksResponse");
			}
		}

		// Token: 0x06001878 RID: 6264 RVA: 0x0005381C File Offset: 0x00051A1C
		internal static string ODataJsonLightPropertyAndValueDeserializer_UnexpectedAnnotationProperties(object p0)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_UnexpectedAnnotationProperties", new object[] { p0 });
		}

		// Token: 0x06001879 RID: 6265 RVA: 0x00053840 File Offset: 0x00051A40
		internal static string ODataJsonLightPropertyAndValueDeserializer_UnexpectedPropertyAnnotation(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_UnexpectedPropertyAnnotation", new object[] { p0, p1 });
		}

		// Token: 0x0600187A RID: 6266 RVA: 0x00053868 File Offset: 0x00051A68
		internal static string ODataJsonLightPropertyAndValueDeserializer_UnexpectedODataPropertyAnnotation(object p0)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_UnexpectedODataPropertyAnnotation", new object[] { p0 });
		}

		// Token: 0x0600187B RID: 6267 RVA: 0x0005388C File Offset: 0x00051A8C
		internal static string ODataJsonLightPropertyAndValueDeserializer_UnexpectedProperty(object p0)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_UnexpectedProperty", new object[] { p0 });
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x0600187C RID: 6268 RVA: 0x000538AF File Offset: 0x00051AAF
		internal static string ODataJsonLightPropertyAndValueDeserializer_InvalidTopLevelPropertyPayload
		{
			get
			{
				return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_InvalidTopLevelPropertyPayload");
			}
		}

		// Token: 0x0600187D RID: 6269 RVA: 0x000538BC File Offset: 0x00051ABC
		internal static string ODataJsonLightPropertyAndValueDeserializer_InvalidTopLevelPropertyName(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_InvalidTopLevelPropertyName", new object[] { p0, p1 });
		}

		// Token: 0x0600187E RID: 6270 RVA: 0x000538E4 File Offset: 0x00051AE4
		internal static string ODataJsonLightPropertyAndValueDeserializer_InvalidTypeName(object p0)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_InvalidTypeName", new object[] { p0 });
		}

		// Token: 0x0600187F RID: 6271 RVA: 0x00053908 File Offset: 0x00051B08
		internal static string ODataJsonLightPropertyAndValueDeserializer_InvalidPrimitiveTypeName(object p0)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_InvalidPrimitiveTypeName", new object[] { p0 });
		}

		// Token: 0x06001880 RID: 6272 RVA: 0x0005392C File Offset: 0x00051B2C
		internal static string ODataJsonLightPropertyAndValueDeserializer_TopLevelPropertyAnnotationWithoutProperty(object p0)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_TopLevelPropertyAnnotationWithoutProperty", new object[] { p0 });
		}

		// Token: 0x06001881 RID: 6273 RVA: 0x00053950 File Offset: 0x00051B50
		internal static string ODataJsonLightPropertyAndValueDeserializer_ComplexValuePropertyAnnotationWithoutProperty(object p0)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_ComplexValuePropertyAnnotationWithoutProperty", new object[] { p0 });
		}

		// Token: 0x06001882 RID: 6274 RVA: 0x00053974 File Offset: 0x00051B74
		internal static string ODataJsonLightPropertyAndValueDeserializer_ComplexValueWithPropertyTypeAnnotation(object p0)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_ComplexValueWithPropertyTypeAnnotation", new object[] { p0 });
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06001883 RID: 6275 RVA: 0x00053997 File Offset: 0x00051B97
		internal static string ODataJsonLightPropertyAndValueDeserializer_ComplexTypeAnnotationNotFirst
		{
			get
			{
				return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_ComplexTypeAnnotationNotFirst");
			}
		}

		// Token: 0x06001884 RID: 6276 RVA: 0x000539A4 File Offset: 0x00051BA4
		internal static string ODataJsonLightPropertyAndValueDeserializer_UnexpectedDataPropertyAnnotation(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_UnexpectedDataPropertyAnnotation", new object[] { p0, p1 });
		}

		// Token: 0x06001885 RID: 6277 RVA: 0x000539CC File Offset: 0x00051BCC
		internal static string ODataJsonLightPropertyAndValueDeserializer_TypePropertyAfterValueProperty(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_TypePropertyAfterValueProperty", new object[] { p0, p1 });
		}

		// Token: 0x06001886 RID: 6278 RVA: 0x000539F4 File Offset: 0x00051BF4
		internal static string ODataJsonLightPropertyAndValueDeserializer_ODataTypeAnnotationInPrimitiveValue(object p0)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_ODataTypeAnnotationInPrimitiveValue", new object[] { p0 });
		}

		// Token: 0x06001887 RID: 6279 RVA: 0x00053A18 File Offset: 0x00051C18
		internal static string ODataJsonLightPropertyAndValueDeserializer_TopLevelPropertyWithPrimitiveNullValue(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_TopLevelPropertyWithPrimitiveNullValue", new object[] { p0, p1 });
		}

		// Token: 0x06001888 RID: 6280 RVA: 0x00053A40 File Offset: 0x00051C40
		internal static string ODataJsonLightPropertyAndValueDeserializer_UnexpectedMetadataReferenceProperty(object p0)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_UnexpectedMetadataReferenceProperty", new object[] { p0 });
		}

		// Token: 0x06001889 RID: 6281 RVA: 0x00053A64 File Offset: 0x00051C64
		internal static string ODataJsonLightPropertyAndValueDeserializer_NoPropertyAndAnnotationAllowedInNullPayload(object p0)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_NoPropertyAndAnnotationAllowedInNullPayload", new object[] { p0 });
		}

		// Token: 0x0600188A RID: 6282 RVA: 0x00053A88 File Offset: 0x00051C88
		internal static string ODataJsonLightPropertyAndValueDeserializer_EdmNullInMetadataUriWithoutNullValueInPayload(object p0)
		{
			return TextRes.GetString("ODataJsonLightPropertyAndValueDeserializer_EdmNullInMetadataUriWithoutNullValueInPayload", new object[] { p0 });
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x0600188B RID: 6283 RVA: 0x00053AAB File Offset: 0x00051CAB
		internal static string ODataJsonReaderCoreUtils_CannotReadSpatialPropertyValue
		{
			get
			{
				return TextRes.GetString("ODataJsonReaderCoreUtils_CannotReadSpatialPropertyValue");
			}
		}

		// Token: 0x0600188C RID: 6284 RVA: 0x00053AB8 File Offset: 0x00051CB8
		internal static string ODataJsonLightReaderUtils_AnnotationWithNullValue(object p0)
		{
			return TextRes.GetString("ODataJsonLightReaderUtils_AnnotationWithNullValue", new object[] { p0 });
		}

		// Token: 0x0600188D RID: 6285 RVA: 0x00053ADC File Offset: 0x00051CDC
		internal static string ODataJsonLightReaderUtils_InvalidValueForODataNullAnnotation(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightReaderUtils_InvalidValueForODataNullAnnotation", new object[] { p0, p1 });
		}

		// Token: 0x0600188E RID: 6286 RVA: 0x00053B04 File Offset: 0x00051D04
		internal static string JsonLightInstanceAnnotationWriter_DuplicateAnnotationNameInCollection(object p0)
		{
			return TextRes.GetString("JsonLightInstanceAnnotationWriter_DuplicateAnnotationNameInCollection", new object[] { p0 });
		}

		// Token: 0x0600188F RID: 6287 RVA: 0x00053B28 File Offset: 0x00051D28
		internal static string ODataJsonLightMetadataUriParser_ServiceDocumentUriMustNotHaveFragment(object p0)
		{
			return TextRes.GetString("ODataJsonLightMetadataUriParser_ServiceDocumentUriMustNotHaveFragment", new object[] { p0 });
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06001890 RID: 6288 RVA: 0x00053B4B File Offset: 0x00051D4B
		internal static string ODataJsonLightMetadataUriParser_NullMetadataDocumentUri
		{
			get
			{
				return TextRes.GetString("ODataJsonLightMetadataUriParser_NullMetadataDocumentUri");
			}
		}

		// Token: 0x06001891 RID: 6289 RVA: 0x00053B58 File Offset: 0x00051D58
		internal static string ODataJsonLightMetadataUriParser_MetadataUriDoesNotMatchExpectedPayloadKind(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightMetadataUriParser_MetadataUriDoesNotMatchExpectedPayloadKind", new object[] { p0, p1 });
		}

		// Token: 0x06001892 RID: 6290 RVA: 0x00053B80 File Offset: 0x00051D80
		internal static string ODataJsonLightMetadataUriParser_InvalidEntitySetNameOrTypeName(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightMetadataUriParser_InvalidEntitySetNameOrTypeName", new object[] { p0, p1 });
		}

		// Token: 0x06001893 RID: 6291 RVA: 0x00053BA8 File Offset: 0x00051DA8
		internal static string ODataJsonLightMetadataUriParser_InvalidPropertyName(object p0, object p1, object p2)
		{
			return TextRes.GetString("ODataJsonLightMetadataUriParser_InvalidPropertyName", new object[] { p0, p1, p2 });
		}

		// Token: 0x06001894 RID: 6292 RVA: 0x00053BD4 File Offset: 0x00051DD4
		internal static string ODataJsonLightMetadataUriParser_InvalidEntityWithTypeCastUriSuffix(object p0, object p1, object p2)
		{
			return TextRes.GetString("ODataJsonLightMetadataUriParser_InvalidEntityWithTypeCastUriSuffix", new object[] { p0, p1, p2 });
		}

		// Token: 0x06001895 RID: 6293 RVA: 0x00053C00 File Offset: 0x00051E00
		internal static string ODataJsonLightMetadataUriParser_InvalidEntityTypeInTypeCast(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightMetadataUriParser_InvalidEntityTypeInTypeCast", new object[] { p0, p1 });
		}

		// Token: 0x06001896 RID: 6294 RVA: 0x00053C28 File Offset: 0x00051E28
		internal static string ODataJsonLightMetadataUriParser_IncompatibleEntityTypeInTypeCast(object p0, object p1, object p2, object p3)
		{
			return TextRes.GetString("ODataJsonLightMetadataUriParser_IncompatibleEntityTypeInTypeCast", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06001897 RID: 6295 RVA: 0x00053C58 File Offset: 0x00051E58
		internal static string ODataJsonLightMetadataUriParser_InvalidEntityReferenceLinkUriSuffix(object p0, object p1, object p2)
		{
			return TextRes.GetString("ODataJsonLightMetadataUriParser_InvalidEntityReferenceLinkUriSuffix", new object[] { p0, p1, p2 });
		}

		// Token: 0x06001898 RID: 6296 RVA: 0x00053C84 File Offset: 0x00051E84
		internal static string ODataJsonLightMetadataUriParser_InvalidPropertyForEntityReferenceLinkUri(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightMetadataUriParser_InvalidPropertyForEntityReferenceLinkUri", new object[] { p0, p1 });
		}

		// Token: 0x06001899 RID: 6297 RVA: 0x00053CAC File Offset: 0x00051EAC
		internal static string ODataJsonLightMetadataUriParser_InvalidSingletonNavPropertyForEntityReferenceLinkUri(object p0, object p1, object p2)
		{
			return TextRes.GetString("ODataJsonLightMetadataUriParser_InvalidSingletonNavPropertyForEntityReferenceLinkUri", new object[] { p0, p1, p2 });
		}

		// Token: 0x0600189A RID: 6298 RVA: 0x00053CD8 File Offset: 0x00051ED8
		internal static string ODataJsonLightMetadataUriParser_FragmentWithInvalidNumberOfParts(object p0, object p1, object p2)
		{
			return TextRes.GetString("ODataJsonLightMetadataUriParser_FragmentWithInvalidNumberOfParts", new object[] { p0, p1, p2 });
		}

		// Token: 0x0600189B RID: 6299 RVA: 0x00053D04 File Offset: 0x00051F04
		internal static string ODataJsonLightMetadataUriParser_InvalidEntitySetOrFunctionImportName(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightMetadataUriParser_InvalidEntitySetOrFunctionImportName", new object[] { p0, p1 });
		}

		// Token: 0x0600189C RID: 6300 RVA: 0x00053D2C File Offset: 0x00051F2C
		internal static string ODataJsonLightMetadataUriParser_InvalidPayloadKindWithSelectQueryOption(object p0)
		{
			return TextRes.GetString("ODataJsonLightMetadataUriParser_InvalidPayloadKindWithSelectQueryOption", new object[] { p0 });
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x0600189D RID: 6301 RVA: 0x00053D4F File Offset: 0x00051F4F
		internal static string ODataJsonLightMetadataUriParser_NoModel
		{
			get
			{
				return TextRes.GetString("ODataJsonLightMetadataUriParser_NoModel");
			}
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x0600189E RID: 6302 RVA: 0x00053D5B File Offset: 0x00051F5B
		internal static string ODataJsonLightMetadataUriParser_ModelResolverReturnedNull
		{
			get
			{
				return TextRes.GetString("ODataJsonLightMetadataUriParser_ModelResolverReturnedNull");
			}
		}

		// Token: 0x0600189F RID: 6303 RVA: 0x00053D68 File Offset: 0x00051F68
		internal static string ODataJsonLightMetadataUriParser_InvalidAssociationLink(object p0)
		{
			return TextRes.GetString("ODataJsonLightMetadataUriParser_InvalidAssociationLink", new object[] { p0 });
		}

		// Token: 0x060018A0 RID: 6304 RVA: 0x00053D8C File Offset: 0x00051F8C
		internal static string ODataJsonLightMetadataUriParser_InvalidEntitySetName(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightMetadataUriParser_InvalidEntitySetName", new object[] { p0, p1 });
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x060018A1 RID: 6305 RVA: 0x00053DB3 File Offset: 0x00051FB3
		internal static string ODataJsonLightEntryAndFeedDeserializer_EntryTypeAnnotationNotFirst
		{
			get
			{
				return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_EntryTypeAnnotationNotFirst");
			}
		}

		// Token: 0x060018A2 RID: 6306 RVA: 0x00053DC0 File Offset: 0x00051FC0
		internal static string ODataJsonLightEntryAndFeedDeserializer_EntryInstanceAnnotationPrecededByProperty(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_EntryInstanceAnnotationPrecededByProperty", new object[] { p0 });
		}

		// Token: 0x060018A3 RID: 6307 RVA: 0x00053DE4 File Offset: 0x00051FE4
		internal static string ODataJsonLightEntryAndFeedDeserializer_CannotReadFeedContentStart(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_CannotReadFeedContentStart", new object[] { p0 });
		}

		// Token: 0x060018A4 RID: 6308 RVA: 0x00053E08 File Offset: 0x00052008
		internal static string ODataJsonLightEntryAndFeedDeserializer_ExpectedFeedPropertyNotFound(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_ExpectedFeedPropertyNotFound", new object[] { p0 });
		}

		// Token: 0x060018A5 RID: 6309 RVA: 0x00053E2C File Offset: 0x0005202C
		internal static string ODataJsonLightEntryAndFeedDeserializer_InvalidNodeTypeForItemsInFeed(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_InvalidNodeTypeForItemsInFeed", new object[] { p0 });
		}

		// Token: 0x060018A6 RID: 6310 RVA: 0x00053E50 File Offset: 0x00052050
		internal static string ODataJsonLightEntryAndFeedDeserializer_InvalidPropertyAnnotationInTopLevelFeed(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_InvalidPropertyAnnotationInTopLevelFeed", new object[] { p0 });
		}

		// Token: 0x060018A7 RID: 6311 RVA: 0x00053E74 File Offset: 0x00052074
		internal static string ODataJsonLightEntryAndFeedDeserializer_InvalidPropertyInTopLevelFeed(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_InvalidPropertyInTopLevelFeed", new object[] { p0, p1 });
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x060018A8 RID: 6312 RVA: 0x00053E9B File Offset: 0x0005209B
		internal static string ODataJsonLightEntryAndFeedDeserializer_FeedPropertyAnnotationForTopLevelFeed
		{
			get
			{
				return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_FeedPropertyAnnotationForTopLevelFeed");
			}
		}

		// Token: 0x060018A9 RID: 6313 RVA: 0x00053EA8 File Offset: 0x000520A8
		internal static string ODataJsonLightEntryAndFeedDeserializer_PropertyWithoutValueWithWrongType(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_PropertyWithoutValueWithWrongType", new object[] { p0, p1 });
		}

		// Token: 0x060018AA RID: 6314 RVA: 0x00053ED0 File Offset: 0x000520D0
		internal static string ODataJsonLightEntryAndFeedDeserializer_OpenPropertyWithoutValue(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_OpenPropertyWithoutValue", new object[] { p0 });
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x060018AB RID: 6315 RVA: 0x00053EF3 File Offset: 0x000520F3
		internal static string ODataJsonLightEntryAndFeedDeserializer_StreamPropertyInRequest
		{
			get
			{
				return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_StreamPropertyInRequest");
			}
		}

		// Token: 0x060018AC RID: 6316 RVA: 0x00053F00 File Offset: 0x00052100
		internal static string ODataJsonLightEntryAndFeedDeserializer_UnexpectedStreamPropertyAnnotation(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_UnexpectedStreamPropertyAnnotation", new object[] { p0, p1 });
		}

		// Token: 0x060018AD RID: 6317 RVA: 0x00053F28 File Offset: 0x00052128
		internal static string ODataJsonLightEntryAndFeedDeserializer_StreamPropertyWithValue(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_StreamPropertyWithValue", new object[] { p0 });
		}

		// Token: 0x060018AE RID: 6318 RVA: 0x00053F4C File Offset: 0x0005214C
		internal static string ODataJsonLightEntryAndFeedDeserializer_UnexpectedDeferredLinkPropertyAnnotation(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_UnexpectedDeferredLinkPropertyAnnotation", new object[] { p0, p1 });
		}

		// Token: 0x060018AF RID: 6319 RVA: 0x00053F74 File Offset: 0x00052174
		internal static string ODataJsonLightEntryAndFeedDeserializer_CannotReadSingletonNavigationPropertyValue(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_CannotReadSingletonNavigationPropertyValue", new object[] { p0 });
		}

		// Token: 0x060018B0 RID: 6320 RVA: 0x00053F98 File Offset: 0x00052198
		internal static string ODataJsonLightEntryAndFeedDeserializer_CannotReadCollectionNavigationPropertyValue(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_CannotReadCollectionNavigationPropertyValue", new object[] { p0 });
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x060018B1 RID: 6321 RVA: 0x00053FBB File Offset: 0x000521BB
		internal static string ODataJsonLightEntryAndFeedDeserializer_CannotReadNavigationPropertyValue
		{
			get
			{
				return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_CannotReadNavigationPropertyValue");
			}
		}

		// Token: 0x060018B2 RID: 6322 RVA: 0x00053FC8 File Offset: 0x000521C8
		internal static string ODataJsonLightEntryAndFeedDeserializer_UnexpectedExpandedSingletonNavigationLinkPropertyAnnotation(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_UnexpectedExpandedSingletonNavigationLinkPropertyAnnotation", new object[] { p0, p1 });
		}

		// Token: 0x060018B3 RID: 6323 RVA: 0x00053FF0 File Offset: 0x000521F0
		internal static string ODataJsonLightEntryAndFeedDeserializer_UnexpectedExpandedCollectionNavigationLinkPropertyAnnotation(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_UnexpectedExpandedCollectionNavigationLinkPropertyAnnotation", new object[] { p0, p1 });
		}

		// Token: 0x060018B4 RID: 6324 RVA: 0x00054018 File Offset: 0x00052218
		internal static string ODataJsonLightEntryAndFeedDeserializer_DuplicateExpandedFeedAnnotation(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_DuplicateExpandedFeedAnnotation", new object[] { p0, p1 });
		}

		// Token: 0x060018B5 RID: 6325 RVA: 0x00054040 File Offset: 0x00052240
		internal static string ODataJsonLightEntryAndFeedDeserializer_UnexpectedPropertyAnnotationAfterExpandedFeed(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_UnexpectedPropertyAnnotationAfterExpandedFeed", new object[] { p0, p1 });
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x060018B6 RID: 6326 RVA: 0x00054067 File Offset: 0x00052267
		internal static string ODataJsonLightEntryAndFeedDeserializer_AnnotationGroupWithoutName
		{
			get
			{
				return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_AnnotationGroupWithoutName");
			}
		}

		// Token: 0x060018B7 RID: 6327 RVA: 0x00054074 File Offset: 0x00052274
		internal static string ODataJsonLightEntryAndFeedDeserializer_AnnotationGroupMemberWithoutName(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_AnnotationGroupMemberWithoutName", new object[] { p0 });
		}

		// Token: 0x060018B8 RID: 6328 RVA: 0x00054098 File Offset: 0x00052298
		internal static string ODataJsonLightEntryAndFeedDeserializer_AnnotationGroupMemberWithInvalidValue(object p0, object p1, object p2)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_AnnotationGroupMemberWithInvalidValue", new object[] { p0, p1, p2 });
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x060018B9 RID: 6329 RVA: 0x000540C3 File Offset: 0x000522C3
		internal static string ODataJsonLightEntryAndFeedDeserializer_AnnotationGroupInRequest
		{
			get
			{
				return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_AnnotationGroupInRequest");
			}
		}

		// Token: 0x060018BA RID: 6330 RVA: 0x000540D0 File Offset: 0x000522D0
		internal static string ODataJsonLightEntryAndFeedDeserializer_UnexpectedNavigationLinkInRequestPropertyAnnotation(object p0, object p1, object p2)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_UnexpectedNavigationLinkInRequestPropertyAnnotation", new object[] { p0, p1, p2 });
		}

		// Token: 0x060018BB RID: 6331 RVA: 0x000540FC File Offset: 0x000522FC
		internal static string ODataJsonLightEntryAndFeedDeserializer_ArrayValueForSingletonBindPropertyAnnotation(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_ArrayValueForSingletonBindPropertyAnnotation", new object[] { p0, p1 });
		}

		// Token: 0x060018BC RID: 6332 RVA: 0x00054124 File Offset: 0x00052324
		internal static string ODataJsonLightEntryAndFeedDeserializer_StringValueForCollectionBindPropertyAnnotation(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_StringValueForCollectionBindPropertyAnnotation", new object[] { p0, p1 });
		}

		// Token: 0x060018BD RID: 6333 RVA: 0x0005414C File Offset: 0x0005234C
		internal static string ODataJsonLightEntryAndFeedDeserializer_EmptyBindArray(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_EmptyBindArray", new object[] { p0 });
		}

		// Token: 0x060018BE RID: 6334 RVA: 0x00054170 File Offset: 0x00052370
		internal static string ODataJsonLightEntryAndFeedDeserializer_NavigationPropertyWithoutValueAndEntityReferenceLink(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_NavigationPropertyWithoutValueAndEntityReferenceLink", new object[] { p0, p1 });
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x00054198 File Offset: 0x00052398
		internal static string ODataJsonLightEntryAndFeedDeserializer_SingletonNavigationPropertyWithBindingAndValue(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_SingletonNavigationPropertyWithBindingAndValue", new object[] { p0, p1 });
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x000541C0 File Offset: 0x000523C0
		internal static string ODataJsonLightEntryAndFeedDeserializer_PropertyWithoutValueWithUnknownType(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_PropertyWithoutValueWithUnknownType", new object[] { p0 });
		}

		// Token: 0x060018C1 RID: 6337 RVA: 0x000541E4 File Offset: 0x000523E4
		internal static string ODataJsonLightEntryAndFeedDeserializer_FunctionImportIsNotActionOrFunction(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_FunctionImportIsNotActionOrFunction", new object[] { p0 });
		}

		// Token: 0x060018C2 RID: 6338 RVA: 0x00054208 File Offset: 0x00052408
		internal static string ODataJsonLightEntryAndFeedDeserializer_MultipleOptionalPropertiesInOperation(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_MultipleOptionalPropertiesInOperation", new object[] { p0, p1 });
		}

		// Token: 0x060018C3 RID: 6339 RVA: 0x00054230 File Offset: 0x00052430
		internal static string ODataJsonLightEntryAndFeedDeserializer_MultipleTargetPropertiesInOperation(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_MultipleTargetPropertiesInOperation", new object[] { p0 });
		}

		// Token: 0x060018C4 RID: 6340 RVA: 0x00054254 File Offset: 0x00052454
		internal static string ODataJsonLightEntryAndFeedDeserializer_OperationMissingTargetProperty(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_OperationMissingTargetProperty", new object[] { p0 });
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x060018C5 RID: 6341 RVA: 0x00054277 File Offset: 0x00052477
		internal static string ODataJsonLightEntryAndFeedDeserializer_MetadataReferencePropertyInRequest
		{
			get
			{
				return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_MetadataReferencePropertyInRequest");
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x060018C6 RID: 6342 RVA: 0x00054283 File Offset: 0x00052483
		internal static string ODataJsonLightEntryAndFeedDeserializer_EncounteredAnnotationGroupInUnexpectedPosition
		{
			get
			{
				return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_EncounteredAnnotationGroupInUnexpectedPosition");
			}
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x060018C7 RID: 6343 RVA: 0x0005428F File Offset: 0x0005248F
		internal static string ODataJsonLightEntryAndFeedDeserializer_EntryTypeAlreadySpecified
		{
			get
			{
				return TextRes.GetString("ODataJsonLightEntryAndFeedDeserializer_EntryTypeAlreadySpecified");
			}
		}

		// Token: 0x060018C8 RID: 6344 RVA: 0x0005429C File Offset: 0x0005249C
		internal static string ODataJsonLightValidationUtils_OperationPropertyCannotBeNull(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightValidationUtils_OperationPropertyCannotBeNull", new object[] { p0, p1 });
		}

		// Token: 0x060018C9 RID: 6345 RVA: 0x000542C4 File Offset: 0x000524C4
		internal static string ODataJsonLightValidationUtils_OpenMetadataReferencePropertyNotSupported(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightValidationUtils_OpenMetadataReferencePropertyNotSupported", new object[] { p0, p1 });
		}

		// Token: 0x060018CA RID: 6346 RVA: 0x000542EC File Offset: 0x000524EC
		internal static string ODataJsonLightDeserializer_RelativeUriUsedWithouODataMetadataAnnotation(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightDeserializer_RelativeUriUsedWithouODataMetadataAnnotation", new object[] { p0, p1 });
		}

		// Token: 0x060018CB RID: 6347 RVA: 0x00054314 File Offset: 0x00052514
		internal static string ODataJsonLightEntryMetadataContext_MetadataAnnotationMustBeInPayload(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntryMetadataContext_MetadataAnnotationMustBeInPayload", new object[] { p0 });
		}

		// Token: 0x060018CC RID: 6348 RVA: 0x00054338 File Offset: 0x00052538
		internal static string ODataJsonLightCollectionDeserializer_ExpectedCollectionPropertyNotFound(object p0)
		{
			return TextRes.GetString("ODataJsonLightCollectionDeserializer_ExpectedCollectionPropertyNotFound", new object[] { p0 });
		}

		// Token: 0x060018CD RID: 6349 RVA: 0x0005435C File Offset: 0x0005255C
		internal static string ODataJsonLightCollectionDeserializer_CannotReadCollectionContentStart(object p0)
		{
			return TextRes.GetString("ODataJsonLightCollectionDeserializer_CannotReadCollectionContentStart", new object[] { p0 });
		}

		// Token: 0x060018CE RID: 6350 RVA: 0x00054380 File Offset: 0x00052580
		internal static string ODataJsonLightCollectionDeserializer_CannotReadCollectionEnd(object p0)
		{
			return TextRes.GetString("ODataJsonLightCollectionDeserializer_CannotReadCollectionEnd", new object[] { p0 });
		}

		// Token: 0x060018CF RID: 6351 RVA: 0x000543A4 File Offset: 0x000525A4
		internal static string ODataJsonLightCollectionDeserializer_InvalidCollectionTypeName(object p0)
		{
			return TextRes.GetString("ODataJsonLightCollectionDeserializer_InvalidCollectionTypeName", new object[] { p0 });
		}

		// Token: 0x060018D0 RID: 6352 RVA: 0x000543C8 File Offset: 0x000525C8
		internal static string ODataJsonLightEntityReferenceLinkDeserializer_EntityReferenceLinkMustBeObjectValue(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntityReferenceLinkDeserializer_EntityReferenceLinkMustBeObjectValue", new object[] { p0 });
		}

		// Token: 0x060018D1 RID: 6353 RVA: 0x000543EC File Offset: 0x000525EC
		internal static string ODataJsonLightEntityReferenceLinkDeserializer_PropertyAnnotationForEntityReferenceLink(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntityReferenceLinkDeserializer_PropertyAnnotationForEntityReferenceLink", new object[] { p0 });
		}

		// Token: 0x060018D2 RID: 6354 RVA: 0x00054410 File Offset: 0x00052610
		internal static string ODataJsonLightEntityReferenceLinkDeserializer_InvalidAnnotationInEntityReferenceLink(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntityReferenceLinkDeserializer_InvalidAnnotationInEntityReferenceLink", new object[] { p0 });
		}

		// Token: 0x060018D3 RID: 6355 RVA: 0x00054434 File Offset: 0x00052634
		internal static string ODataJsonLightEntityReferenceLinkDeserializer_InvalidPropertyInEntityReferenceLink(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightEntityReferenceLinkDeserializer_InvalidPropertyInEntityReferenceLink", new object[] { p0, p1 });
		}

		// Token: 0x060018D4 RID: 6356 RVA: 0x0005445C File Offset: 0x0005265C
		internal static string ODataJsonLightEntityReferenceLinkDeserializer_MissingEntityReferenceLinkProperty(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntityReferenceLinkDeserializer_MissingEntityReferenceLinkProperty", new object[] { p0 });
		}

		// Token: 0x060018D5 RID: 6357 RVA: 0x00054480 File Offset: 0x00052680
		internal static string ODataJsonLightEntityReferenceLinkDeserializer_MultipleUriPropertiesInEntityReferenceLink(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntityReferenceLinkDeserializer_MultipleUriPropertiesInEntityReferenceLink", new object[] { p0 });
		}

		// Token: 0x060018D6 RID: 6358 RVA: 0x000544A4 File Offset: 0x000526A4
		internal static string ODataJsonLightEntityReferenceLinkDeserializer_EntityReferenceLinkUrlCannotBeNull(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntityReferenceLinkDeserializer_EntityReferenceLinkUrlCannotBeNull", new object[] { p0 });
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x060018D7 RID: 6359 RVA: 0x000544C7 File Offset: 0x000526C7
		internal static string ODataJsonLightEntityReferenceLinkDeserializer_PropertyAnnotationForEntityReferenceLinks
		{
			get
			{
				return TextRes.GetString("ODataJsonLightEntityReferenceLinkDeserializer_PropertyAnnotationForEntityReferenceLinks");
			}
		}

		// Token: 0x060018D8 RID: 6360 RVA: 0x000544D4 File Offset: 0x000526D4
		internal static string ODataJsonLightEntityReferenceLinkDeserializer_InvalidEntityReferenceLinksPropertyFound(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightEntityReferenceLinkDeserializer_InvalidEntityReferenceLinksPropertyFound", new object[] { p0, p1 });
		}

		// Token: 0x060018D9 RID: 6361 RVA: 0x000544FC File Offset: 0x000526FC
		internal static string ODataJsonLightEntityReferenceLinkDeserializer_InvalidPropertyAnnotationInEntityReferenceLinks(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntityReferenceLinkDeserializer_InvalidPropertyAnnotationInEntityReferenceLinks", new object[] { p0 });
		}

		// Token: 0x060018DA RID: 6362 RVA: 0x00054520 File Offset: 0x00052720
		internal static string ODataJsonLightEntityReferenceLinkDeserializer_ExpectedEntityReferenceLinksPropertyNotFound(object p0)
		{
			return TextRes.GetString("ODataJsonLightEntityReferenceLinkDeserializer_ExpectedEntityReferenceLinksPropertyNotFound", new object[] { p0 });
		}

		// Token: 0x060018DB RID: 6363 RVA: 0x00054544 File Offset: 0x00052744
		internal static string ODataJsonOperationsDeserializerUtils_OperationPropertyCannotBeNull(object p0, object p1, object p2)
		{
			return TextRes.GetString("ODataJsonOperationsDeserializerUtils_OperationPropertyCannotBeNull", new object[] { p0, p1, p2 });
		}

		// Token: 0x060018DC RID: 6364 RVA: 0x00054570 File Offset: 0x00052770
		internal static string ODataJsonOperationsDeserializerUtils_OperationsPropertyMustHaveObjectValue(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonOperationsDeserializerUtils_OperationsPropertyMustHaveObjectValue", new object[] { p0, p1 });
		}

		// Token: 0x060018DD RID: 6365 RVA: 0x00054598 File Offset: 0x00052798
		internal static string ODataJsonOperationsDeserializerUtils_RepeatMetadataValue(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonOperationsDeserializerUtils_RepeatMetadataValue", new object[] { p0, p1 });
		}

		// Token: 0x060018DE RID: 6366 RVA: 0x000545C0 File Offset: 0x000527C0
		internal static string ODataJsonOperationsDeserializerUtils_MetadataMustHaveArrayValue(object p0, object p1, object p2)
		{
			return TextRes.GetString("ODataJsonOperationsDeserializerUtils_MetadataMustHaveArrayValue", new object[] { p0, p1, p2 });
		}

		// Token: 0x060018DF RID: 6367 RVA: 0x000545EC File Offset: 0x000527EC
		internal static string ODataJsonOperationsDeserializerUtils_OperationMetadataArrayExpectedAnObject(object p0, object p1, object p2)
		{
			return TextRes.GetString("ODataJsonOperationsDeserializerUtils_OperationMetadataArrayExpectedAnObject", new object[] { p0, p1, p2 });
		}

		// Token: 0x060018E0 RID: 6368 RVA: 0x00054618 File Offset: 0x00052818
		internal static string ODataJsonOperationsDeserializerUtils_MultipleOptionalPropertiesInOperation(object p0, object p1, object p2)
		{
			return TextRes.GetString("ODataJsonOperationsDeserializerUtils_MultipleOptionalPropertiesInOperation", new object[] { p0, p1, p2 });
		}

		// Token: 0x060018E1 RID: 6369 RVA: 0x00054644 File Offset: 0x00052844
		internal static string ODataJsonOperationsDeserializerUtils_MultipleTargetPropertiesInOperation(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonOperationsDeserializerUtils_MultipleTargetPropertiesInOperation", new object[] { p0, p1 });
		}

		// Token: 0x060018E2 RID: 6370 RVA: 0x0005466C File Offset: 0x0005286C
		internal static string ODataJsonOperationsDeserializerUtils_OperationMissingTargetProperty(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonOperationsDeserializerUtils_OperationMissingTargetProperty", new object[] { p0, p1 });
		}

		// Token: 0x060018E3 RID: 6371 RVA: 0x00054694 File Offset: 0x00052894
		internal static string ODataJsonLightServiceDocumentDeserializer_DuplicatePropertiesInServiceDocument(object p0)
		{
			return TextRes.GetString("ODataJsonLightServiceDocumentDeserializer_DuplicatePropertiesInServiceDocument", new object[] { p0 });
		}

		// Token: 0x060018E4 RID: 6372 RVA: 0x000546B8 File Offset: 0x000528B8
		internal static string ODataJsonLightServiceDocumentDeserializer_DuplicatePropertiesInResourceCollection(object p0)
		{
			return TextRes.GetString("ODataJsonLightServiceDocumentDeserializer_DuplicatePropertiesInResourceCollection", new object[] { p0 });
		}

		// Token: 0x060018E5 RID: 6373 RVA: 0x000546DC File Offset: 0x000528DC
		internal static string ODataJsonLightServiceDocumentDeserializer_MissingValuePropertyInServiceDocument(object p0)
		{
			return TextRes.GetString("ODataJsonLightServiceDocumentDeserializer_MissingValuePropertyInServiceDocument", new object[] { p0 });
		}

		// Token: 0x060018E6 RID: 6374 RVA: 0x00054700 File Offset: 0x00052900
		internal static string ODataJsonLightServiceDocumentDeserializer_MissingRequiredPropertyInResourceCollection(object p0)
		{
			return TextRes.GetString("ODataJsonLightServiceDocumentDeserializer_MissingRequiredPropertyInResourceCollection", new object[] { p0 });
		}

		// Token: 0x060018E7 RID: 6375 RVA: 0x00054724 File Offset: 0x00052924
		internal static string ODataJsonLightServiceDocumentDeserializer_PropertyAnnotationInServiceDocument(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightServiceDocumentDeserializer_PropertyAnnotationInServiceDocument", new object[] { p0, p1 });
		}

		// Token: 0x060018E8 RID: 6376 RVA: 0x0005474C File Offset: 0x0005294C
		internal static string ODataJsonLightServiceDocumentDeserializer_InstanceAnnotationInServiceDocument(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightServiceDocumentDeserializer_InstanceAnnotationInServiceDocument", new object[] { p0, p1 });
		}

		// Token: 0x060018E9 RID: 6377 RVA: 0x00054774 File Offset: 0x00052974
		internal static string ODataJsonLightServiceDocumentDeserializer_PropertyAnnotationInResourceCollection(object p0)
		{
			return TextRes.GetString("ODataJsonLightServiceDocumentDeserializer_PropertyAnnotationInResourceCollection", new object[] { p0 });
		}

		// Token: 0x060018EA RID: 6378 RVA: 0x00054798 File Offset: 0x00052998
		internal static string ODataJsonLightServiceDocumentDeserializer_InstanceAnnotationInResourceCollection(object p0)
		{
			return TextRes.GetString("ODataJsonLightServiceDocumentDeserializer_InstanceAnnotationInResourceCollection", new object[] { p0 });
		}

		// Token: 0x060018EB RID: 6379 RVA: 0x000547BC File Offset: 0x000529BC
		internal static string ODataJsonLightServiceDocumentDeserializer_UnexpectedPropertyInResourceCollection(object p0, object p1, object p2)
		{
			return TextRes.GetString("ODataJsonLightServiceDocumentDeserializer_UnexpectedPropertyInResourceCollection", new object[] { p0, p1, p2 });
		}

		// Token: 0x060018EC RID: 6380 RVA: 0x000547E8 File Offset: 0x000529E8
		internal static string ODataJsonLightServiceDocumentDeserializer_UnexpectedPropertyInServiceDocument(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightServiceDocumentDeserializer_UnexpectedPropertyInServiceDocument", new object[] { p0, p1 });
		}

		// Token: 0x060018ED RID: 6381 RVA: 0x00054810 File Offset: 0x00052A10
		internal static string ODataJsonLightServiceDocumentDeserializer_PropertyAnnotationWithoutProperty(object p0)
		{
			return TextRes.GetString("ODataJsonLightServiceDocumentDeserializer_PropertyAnnotationWithoutProperty", new object[] { p0 });
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x060018EE RID: 6382 RVA: 0x00054833 File Offset: 0x00052A33
		internal static string ODataJsonLightParameterDeserializer_PropertyAnnotationForParameters
		{
			get
			{
				return TextRes.GetString("ODataJsonLightParameterDeserializer_PropertyAnnotationForParameters");
			}
		}

		// Token: 0x060018EF RID: 6383 RVA: 0x00054840 File Offset: 0x00052A40
		internal static string ODataJsonLightParameterDeserializer_PropertyAnnotationWithoutPropertyForParameters(object p0)
		{
			return TextRes.GetString("ODataJsonLightParameterDeserializer_PropertyAnnotationWithoutPropertyForParameters", new object[] { p0 });
		}

		// Token: 0x060018F0 RID: 6384 RVA: 0x00054864 File Offset: 0x00052A64
		internal static string ODataJsonLightParameterDeserializer_UnsupportedPrimitiveParameterType(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightParameterDeserializer_UnsupportedPrimitiveParameterType", new object[] { p0, p1 });
		}

		// Token: 0x060018F1 RID: 6385 RVA: 0x0005488C File Offset: 0x00052A8C
		internal static string ODataJsonLightParameterDeserializer_NullCollectionExpected(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightParameterDeserializer_NullCollectionExpected", new object[] { p0, p1 });
		}

		// Token: 0x060018F2 RID: 6386 RVA: 0x000548B4 File Offset: 0x00052AB4
		internal static string ODataJsonLightParameterDeserializer_UnsupportedParameterTypeKind(object p0, object p1)
		{
			return TextRes.GetString("ODataJsonLightParameterDeserializer_UnsupportedParameterTypeKind", new object[] { p0, p1 });
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x060018F3 RID: 6387 RVA: 0x000548DB File Offset: 0x00052ADB
		internal static string SelectedPropertiesNode_StarSegmentNotLastSegment
		{
			get
			{
				return TextRes.GetString("SelectedPropertiesNode_StarSegmentNotLastSegment");
			}
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x060018F4 RID: 6388 RVA: 0x000548E7 File Offset: 0x00052AE7
		internal static string SelectedPropertiesNode_StarSegmentAfterTypeSegment
		{
			get
			{
				return TextRes.GetString("SelectedPropertiesNode_StarSegmentAfterTypeSegment");
			}
		}

		// Token: 0x060018F5 RID: 6389 RVA: 0x000548F4 File Offset: 0x00052AF4
		internal static string ODataJsonLightErrorDeserializer_PropertyAnnotationNotAllowedInErrorPayload(object p0)
		{
			return TextRes.GetString("ODataJsonLightErrorDeserializer_PropertyAnnotationNotAllowedInErrorPayload", new object[] { p0 });
		}

		// Token: 0x060018F6 RID: 6390 RVA: 0x00054918 File Offset: 0x00052B18
		internal static string ODataJsonLightErrorDeserializer_InstanceAnnotationNotAllowedInErrorPayload(object p0)
		{
			return TextRes.GetString("ODataJsonLightErrorDeserializer_InstanceAnnotationNotAllowedInErrorPayload", new object[] { p0 });
		}

		// Token: 0x060018F7 RID: 6391 RVA: 0x0005493C File Offset: 0x00052B3C
		internal static string ODataJsonLightErrorDeserializer_PropertyAnnotationWithoutPropertyForError(object p0)
		{
			return TextRes.GetString("ODataJsonLightErrorDeserializer_PropertyAnnotationWithoutPropertyForError", new object[] { p0 });
		}

		// Token: 0x060018F8 RID: 6392 RVA: 0x00054960 File Offset: 0x00052B60
		internal static string ODataJsonLightErrorDeserializer_TopLevelErrorValueWithInvalidProperty(object p0)
		{
			return TextRes.GetString("ODataJsonLightErrorDeserializer_TopLevelErrorValueWithInvalidProperty", new object[] { p0 });
		}

		// Token: 0x060018F9 RID: 6393 RVA: 0x00054984 File Offset: 0x00052B84
		internal static string ODataConventionalUriBuilder_EntityTypeWithNoKeyProperties(object p0)
		{
			return TextRes.GetString("ODataConventionalUriBuilder_EntityTypeWithNoKeyProperties", new object[] { p0 });
		}

		// Token: 0x060018FA RID: 6394 RVA: 0x000549A8 File Offset: 0x00052BA8
		internal static string ODataConventionalUriBuilder_NullKeyValue(object p0, object p1)
		{
			return TextRes.GetString("ODataConventionalUriBuilder_NullKeyValue", new object[] { p0, p1 });
		}

		// Token: 0x060018FB RID: 6395 RVA: 0x000549D0 File Offset: 0x00052BD0
		internal static string ODataEntryMetadataContext_EntityTypeWithNoKeyProperties(object p0)
		{
			return TextRes.GetString("ODataEntryMetadataContext_EntityTypeWithNoKeyProperties", new object[] { p0 });
		}

		// Token: 0x060018FC RID: 6396 RVA: 0x000549F4 File Offset: 0x00052BF4
		internal static string ODataEntryMetadataContext_NullKeyValue(object p0, object p1)
		{
			return TextRes.GetString("ODataEntryMetadataContext_NullKeyValue", new object[] { p0, p1 });
		}

		// Token: 0x060018FD RID: 6397 RVA: 0x00054A1C File Offset: 0x00052C1C
		internal static string ODataEntryMetadataContext_KeyOrETagValuesMustBePrimitiveValues(object p0, object p1)
		{
			return TextRes.GetString("ODataEntryMetadataContext_KeyOrETagValuesMustBePrimitiveValues", new object[] { p0, p1 });
		}

		// Token: 0x060018FE RID: 6398 RVA: 0x00054A44 File Offset: 0x00052C44
		internal static string EdmValueUtils_NonPrimitiveValue(object p0, object p1)
		{
			return TextRes.GetString("EdmValueUtils_NonPrimitiveValue", new object[] { p0, p1 });
		}

		// Token: 0x060018FF RID: 6399 RVA: 0x00054A6C File Offset: 0x00052C6C
		internal static string EdmValueUtils_PropertyDoesntExist(object p0, object p1)
		{
			return TextRes.GetString("EdmValueUtils_PropertyDoesntExist", new object[] { p0, p1 });
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06001900 RID: 6400 RVA: 0x00054A93 File Offset: 0x00052C93
		internal static string JsonLightAnnotationGroupDeserializer_AnnotationGroupDeclarationWithoutName
		{
			get
			{
				return TextRes.GetString("JsonLightAnnotationGroupDeserializer_AnnotationGroupDeclarationWithoutName");
			}
		}

		// Token: 0x06001901 RID: 6401 RVA: 0x00054AA0 File Offset: 0x00052CA0
		internal static string JsonLightAnnotationGroupDeserializer_InvalidAnnotationFoundInsideAnnotationGroup(object p0)
		{
			return TextRes.GetString("JsonLightAnnotationGroupDeserializer_InvalidAnnotationFoundInsideAnnotationGroup", new object[] { p0 });
		}

		// Token: 0x06001902 RID: 6402 RVA: 0x00054AC4 File Offset: 0x00052CC4
		internal static string JsonLightAnnotationGroupDeserializer_InvalidAnnotationFoundInsideNamedAnnotationGroup(object p0, object p1)
		{
			return TextRes.GetString("JsonLightAnnotationGroupDeserializer_InvalidAnnotationFoundInsideNamedAnnotationGroup", new object[] { p0, p1 });
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x06001903 RID: 6403 RVA: 0x00054AEB File Offset: 0x00052CEB
		internal static string JsonLightAnnotationGroupDeserializer_EncounteredMultipleNameProperties
		{
			get
			{
				return TextRes.GetString("JsonLightAnnotationGroupDeserializer_EncounteredMultipleNameProperties");
			}
		}

		// Token: 0x06001904 RID: 6404 RVA: 0x00054AF8 File Offset: 0x00052CF8
		internal static string JsonLightAnnotationGroupDeserializer_UndefinedAnnotationGroupReference(object p0)
		{
			return TextRes.GetString("JsonLightAnnotationGroupDeserializer_UndefinedAnnotationGroupReference", new object[] { p0 });
		}

		// Token: 0x06001905 RID: 6405 RVA: 0x00054B1C File Offset: 0x00052D1C
		internal static string JsonLightAnnotationGroupDeserializer_MultipleAnnotationGroupsWithSameName(object p0)
		{
			return TextRes.GetString("JsonLightAnnotationGroupDeserializer_MultipleAnnotationGroupsWithSameName", new object[] { p0 });
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06001906 RID: 6406 RVA: 0x00054B3F File Offset: 0x00052D3F
		internal static string ODataPrimitiveValue_CannotCreateODataPrimitiveValueFromNull
		{
			get
			{
				return TextRes.GetString("ODataPrimitiveValue_CannotCreateODataPrimitiveValueFromNull");
			}
		}

		// Token: 0x06001907 RID: 6407 RVA: 0x00054B4C File Offset: 0x00052D4C
		internal static string ODataPrimitiveValue_CannotCreateODataPrimitiveValueFromUnsupportedValueType(object p0)
		{
			return TextRes.GetString("ODataPrimitiveValue_CannotCreateODataPrimitiveValueFromUnsupportedValueType", new object[] { p0 });
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06001908 RID: 6408 RVA: 0x00054B6F File Offset: 0x00052D6F
		internal static string ODataAnnotatable_InstanceAnnotationsOnlyOnODataError
		{
			get
			{
				return TextRes.GetString("ODataAnnotatable_InstanceAnnotationsOnlyOnODataError");
			}
		}

		// Token: 0x06001909 RID: 6409 RVA: 0x00054B7C File Offset: 0x00052D7C
		internal static string ODataInstanceAnnotation_NeedPeriodInName(object p0)
		{
			return TextRes.GetString("ODataInstanceAnnotation_NeedPeriodInName", new object[] { p0 });
		}

		// Token: 0x0600190A RID: 6410 RVA: 0x00054BA0 File Offset: 0x00052DA0
		internal static string ODataInstanceAnnotation_ReservedNamesNotAllowed(object p0, object p1)
		{
			return TextRes.GetString("ODataInstanceAnnotation_ReservedNamesNotAllowed", new object[] { p0, p1 });
		}

		// Token: 0x0600190B RID: 6411 RVA: 0x00054BC8 File Offset: 0x00052DC8
		internal static string ODataInstanceAnnotation_BadTermName(object p0)
		{
			return TextRes.GetString("ODataInstanceAnnotation_BadTermName", new object[] { p0 });
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x0600190C RID: 6412 RVA: 0x00054BEB File Offset: 0x00052DEB
		internal static string ODataInstanceAnnotation_ValueCannotBeODataStreamReferenceValue
		{
			get
			{
				return TextRes.GetString("ODataInstanceAnnotation_ValueCannotBeODataStreamReferenceValue");
			}
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x0600190D RID: 6413 RVA: 0x00054BF7 File Offset: 0x00052DF7
		internal static string ODataJsonLightValueSerializer_MissingTypeNameOnComplex
		{
			get
			{
				return TextRes.GetString("ODataJsonLightValueSerializer_MissingTypeNameOnComplex");
			}
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x0600190E RID: 6414 RVA: 0x00054C03 File Offset: 0x00052E03
		internal static string ODataJsonLightValueSerializer_MissingTypeNameOnCollection
		{
			get
			{
				return TextRes.GetString("ODataJsonLightValueSerializer_MissingTypeNameOnCollection");
			}
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x0600190F RID: 6415 RVA: 0x00054C0F File Offset: 0x00052E0F
		internal static string AtomInstanceAnnotation_MissingTermAttributeOnAnnotationElement
		{
			get
			{
				return TextRes.GetString("AtomInstanceAnnotation_MissingTermAttributeOnAnnotationElement");
			}
		}

		// Token: 0x06001910 RID: 6416 RVA: 0x00054C1C File Offset: 0x00052E1C
		internal static string AtomInstanceAnnotation_AttributeValueNotationUsedWithIncompatibleType(object p0, object p1)
		{
			return TextRes.GetString("AtomInstanceAnnotation_AttributeValueNotationUsedWithIncompatibleType", new object[] { p0, p1 });
		}

		// Token: 0x06001911 RID: 6417 RVA: 0x00054C44 File Offset: 0x00052E44
		internal static string AtomInstanceAnnotation_AttributeValueNotationUsedOnNonEmptyElement(object p0)
		{
			return TextRes.GetString("AtomInstanceAnnotation_AttributeValueNotationUsedOnNonEmptyElement", new object[] { p0 });
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06001912 RID: 6418 RVA: 0x00054C67 File Offset: 0x00052E67
		internal static string AtomInstanceAnnotation_MultipleAttributeValueNotationAttributes
		{
			get
			{
				return TextRes.GetString("AtomInstanceAnnotation_MultipleAttributeValueNotationAttributes");
			}
		}

		// Token: 0x06001913 RID: 6419 RVA: 0x00054C74 File Offset: 0x00052E74
		internal static string AnnotationFilterPattern_InvalidPatternMissingDot(object p0)
		{
			return TextRes.GetString("AnnotationFilterPattern_InvalidPatternMissingDot", new object[] { p0 });
		}

		// Token: 0x06001914 RID: 6420 RVA: 0x00054C98 File Offset: 0x00052E98
		internal static string AnnotationFilterPattern_InvalidPatternEmptySegment(object p0)
		{
			return TextRes.GetString("AnnotationFilterPattern_InvalidPatternEmptySegment", new object[] { p0 });
		}

		// Token: 0x06001915 RID: 6421 RVA: 0x00054CBC File Offset: 0x00052EBC
		internal static string AnnotationFilterPattern_InvalidPatternWildCardInSegment(object p0)
		{
			return TextRes.GetString("AnnotationFilterPattern_InvalidPatternWildCardInSegment", new object[] { p0 });
		}

		// Token: 0x06001916 RID: 6422 RVA: 0x00054CE0 File Offset: 0x00052EE0
		internal static string AnnotationFilterPattern_InvalidPatternWildCardMustBeInLastSegment(object p0)
		{
			return TextRes.GetString("AnnotationFilterPattern_InvalidPatternWildCardMustBeInLastSegment", new object[] { p0 });
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06001917 RID: 6423 RVA: 0x00054D03 File Offset: 0x00052F03
		internal static string JsonFullMetadataLevel_MissingEntitySet
		{
			get
			{
				return TextRes.GetString("JsonFullMetadataLevel_MissingEntitySet");
			}
		}

		// Token: 0x06001918 RID: 6424 RVA: 0x00054D10 File Offset: 0x00052F10
		internal static string ODataQueryUtils_DidNotFindServiceOperation(object p0)
		{
			return TextRes.GetString("ODataQueryUtils_DidNotFindServiceOperation", new object[] { p0 });
		}

		// Token: 0x06001919 RID: 6425 RVA: 0x00054D34 File Offset: 0x00052F34
		internal static string ODataQueryUtils_FoundMultipleServiceOperations(object p0)
		{
			return TextRes.GetString("ODataQueryUtils_FoundMultipleServiceOperations", new object[] { p0 });
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x0600191A RID: 6426 RVA: 0x00054D57 File Offset: 0x00052F57
		internal static string ODataQueryUtils_CannotSetMetadataAnnotationOnPrimitiveType
		{
			get
			{
				return TextRes.GetString("ODataQueryUtils_CannotSetMetadataAnnotationOnPrimitiveType");
			}
		}

		// Token: 0x0600191B RID: 6427 RVA: 0x00054D64 File Offset: 0x00052F64
		internal static string ODataQueryUtils_DidNotFindEntitySet(object p0)
		{
			return TextRes.GetString("ODataQueryUtils_DidNotFindEntitySet", new object[] { p0 });
		}

		// Token: 0x0600191C RID: 6428 RVA: 0x00054D88 File Offset: 0x00052F88
		internal static string BinaryOperatorQueryNode_InvalidOperandType(object p0, object p1)
		{
			return TextRes.GetString("BinaryOperatorQueryNode_InvalidOperandType", new object[] { p0, p1 });
		}

		// Token: 0x0600191D RID: 6429 RVA: 0x00054DB0 File Offset: 0x00052FB0
		internal static string BinaryOperatorQueryNode_OperandsMustHaveSameTypes(object p0, object p1)
		{
			return TextRes.GetString("BinaryOperatorQueryNode_OperandsMustHaveSameTypes", new object[] { p0, p1 });
		}

		// Token: 0x0600191E RID: 6430 RVA: 0x00054DD8 File Offset: 0x00052FD8
		internal static string SyntacticTree_UriMustBeAbsolute(object p0)
		{
			return TextRes.GetString("SyntacticTree_UriMustBeAbsolute", new object[] { p0 });
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x0600191F RID: 6431 RVA: 0x00054DFB File Offset: 0x00052FFB
		internal static string SyntacticTree_MaxDepthInvalid
		{
			get
			{
				return TextRes.GetString("SyntacticTree_MaxDepthInvalid");
			}
		}

		// Token: 0x06001920 RID: 6432 RVA: 0x00054E08 File Offset: 0x00053008
		internal static string SyntacticTree_InvalidSkipQueryOptionValue(object p0)
		{
			return TextRes.GetString("SyntacticTree_InvalidSkipQueryOptionValue", new object[] { p0 });
		}

		// Token: 0x06001921 RID: 6433 RVA: 0x00054E2C File Offset: 0x0005302C
		internal static string SyntacticTree_InvalidTopQueryOptionValue(object p0)
		{
			return TextRes.GetString("SyntacticTree_InvalidTopQueryOptionValue", new object[] { p0 });
		}

		// Token: 0x06001922 RID: 6434 RVA: 0x00054E50 File Offset: 0x00053050
		internal static string SyntacticTree_InvalidInlineCountQueryOptionValue(object p0, object p1)
		{
			return TextRes.GetString("SyntacticTree_InvalidInlineCountQueryOptionValue", new object[] { p0, p1 });
		}

		// Token: 0x06001923 RID: 6435 RVA: 0x00054E78 File Offset: 0x00053078
		internal static string QueryOptionUtils_QueryParameterMustBeSpecifiedOnce(object p0)
		{
			return TextRes.GetString("QueryOptionUtils_QueryParameterMustBeSpecifiedOnce", new object[] { p0 });
		}

		// Token: 0x06001924 RID: 6436 RVA: 0x00054E9C File Offset: 0x0005309C
		internal static string UriBuilder_NotSupportedClrLiteral(object p0)
		{
			return TextRes.GetString("UriBuilder_NotSupportedClrLiteral", new object[] { p0 });
		}

		// Token: 0x06001925 RID: 6437 RVA: 0x00054EC0 File Offset: 0x000530C0
		internal static string UriBuilder_NotSupportedQueryToken(object p0)
		{
			return TextRes.GetString("UriBuilder_NotSupportedQueryToken", new object[] { p0 });
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06001926 RID: 6438 RVA: 0x00054EE3 File Offset: 0x000530E3
		internal static string UriQueryExpressionParser_TooDeep
		{
			get
			{
				return TextRes.GetString("UriQueryExpressionParser_TooDeep");
			}
		}

		// Token: 0x06001927 RID: 6439 RVA: 0x00054EF0 File Offset: 0x000530F0
		internal static string UriQueryExpressionParser_ExpressionExpected(object p0, object p1)
		{
			return TextRes.GetString("UriQueryExpressionParser_ExpressionExpected", new object[] { p0, p1 });
		}

		// Token: 0x06001928 RID: 6440 RVA: 0x00054F18 File Offset: 0x00053118
		internal static string UriQueryExpressionParser_OpenParenExpected(object p0, object p1)
		{
			return TextRes.GetString("UriQueryExpressionParser_OpenParenExpected", new object[] { p0, p1 });
		}

		// Token: 0x06001929 RID: 6441 RVA: 0x00054F40 File Offset: 0x00053140
		internal static string UriQueryExpressionParser_CloseParenOrCommaExpected(object p0, object p1)
		{
			return TextRes.GetString("UriQueryExpressionParser_CloseParenOrCommaExpected", new object[] { p0, p1 });
		}

		// Token: 0x0600192A RID: 6442 RVA: 0x00054F68 File Offset: 0x00053168
		internal static string UriQueryExpressionParser_CloseParenOrOperatorExpected(object p0, object p1)
		{
			return TextRes.GetString("UriQueryExpressionParser_CloseParenOrOperatorExpected", new object[] { p0, p1 });
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x0600192B RID: 6443 RVA: 0x00054F8F File Offset: 0x0005318F
		internal static string UriQueryExpressionParser_RepeatedVisitor
		{
			get
			{
				return TextRes.GetString("UriQueryExpressionParser_RepeatedVisitor");
			}
		}

		// Token: 0x0600192C RID: 6444 RVA: 0x00054F9C File Offset: 0x0005319C
		internal static string UriQueryExpressionParser_CannotCreateStarTokenFromNonStar(object p0)
		{
			return TextRes.GetString("UriQueryExpressionParser_CannotCreateStarTokenFromNonStar", new object[] { p0 });
		}

		// Token: 0x0600192D RID: 6445 RVA: 0x00054FC0 File Offset: 0x000531C0
		internal static string UriQueryExpressionParser_RangeVariableAlreadyDeclared(object p0)
		{
			return TextRes.GetString("UriQueryExpressionParser_RangeVariableAlreadyDeclared", new object[] { p0 });
		}

		// Token: 0x0600192E RID: 6446 RVA: 0x00054FE4 File Offset: 0x000531E4
		internal static string UriQueryPathParser_RequestUriDoesNotHaveTheCorrectBaseUri(object p0, object p1)
		{
			return TextRes.GetString("UriQueryPathParser_RequestUriDoesNotHaveTheCorrectBaseUri", new object[] { p0, p1 });
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x0600192F RID: 6447 RVA: 0x0005500B File Offset: 0x0005320B
		internal static string UriQueryPathParser_SyntaxError
		{
			get
			{
				return TextRes.GetString("UriQueryPathParser_SyntaxError");
			}
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06001930 RID: 6448 RVA: 0x00055017 File Offset: 0x00053217
		internal static string UriQueryPathParser_TooManySegments
		{
			get
			{
				return TextRes.GetString("UriQueryPathParser_TooManySegments");
			}
		}

		// Token: 0x06001931 RID: 6449 RVA: 0x00055024 File Offset: 0x00053224
		internal static string UriQueryPathParser_InvalidKeyValueLiteral(object p0)
		{
			return TextRes.GetString("UriQueryPathParser_InvalidKeyValueLiteral", new object[] { p0 });
		}

		// Token: 0x06001932 RID: 6450 RVA: 0x00055048 File Offset: 0x00053248
		internal static string PropertyInfoTypeAnnotation_CannotFindProperty(object p0, object p1, object p2)
		{
			return TextRes.GetString("PropertyInfoTypeAnnotation_CannotFindProperty", new object[] { p0, p1, p2 });
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06001933 RID: 6451 RVA: 0x00055073 File Offset: 0x00053273
		internal static string SelectionItemBinder_NonNavigationPathToken
		{
			get
			{
				return TextRes.GetString("SelectionItemBinder_NonNavigationPathToken");
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06001934 RID: 6452 RVA: 0x0005507F File Offset: 0x0005327F
		internal static string SelectTreeNormalizer_NonPathProperty
		{
			get
			{
				return TextRes.GetString("SelectTreeNormalizer_NonPathProperty");
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06001935 RID: 6453 RVA: 0x0005508B File Offset: 0x0005328B
		internal static string ExpandItem_NonEntityNavProp
		{
			get
			{
				return TextRes.GetString("ExpandItem_NonEntityNavProp");
			}
		}

		// Token: 0x06001936 RID: 6454 RVA: 0x00055098 File Offset: 0x00053298
		internal static string MetadataBinder_UnsupportedQueryTokenKind(object p0)
		{
			return TextRes.GetString("MetadataBinder_UnsupportedQueryTokenKind", new object[] { p0 });
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06001937 RID: 6455 RVA: 0x000550BB File Offset: 0x000532BB
		internal static string MetadataBinder_UnsupportedExtensionToken
		{
			get
			{
				return TextRes.GetString("MetadataBinder_UnsupportedExtensionToken");
			}
		}

		// Token: 0x06001938 RID: 6456 RVA: 0x000550C8 File Offset: 0x000532C8
		internal static string MetadataBinder_RootSegmentResourceNotFound(object p0)
		{
			return TextRes.GetString("MetadataBinder_RootSegmentResourceNotFound", new object[] { p0 });
		}

		// Token: 0x06001939 RID: 6457 RVA: 0x000550EC File Offset: 0x000532EC
		internal static string MetadataBinder_KeyValueApplicableOnlyToEntityType(object p0)
		{
			return TextRes.GetString("MetadataBinder_KeyValueApplicableOnlyToEntityType", new object[] { p0 });
		}

		// Token: 0x0600193A RID: 6458 RVA: 0x00055110 File Offset: 0x00053310
		internal static string MetadataBinder_PropertyNotDeclared(object p0, object p1)
		{
			return TextRes.GetString("MetadataBinder_PropertyNotDeclared", new object[] { p0, p1 });
		}

		// Token: 0x0600193B RID: 6459 RVA: 0x00055138 File Offset: 0x00053338
		internal static string MetadataBinder_PropertyNotDeclaredOrNotKeyInKeyValue(object p0, object p1)
		{
			return TextRes.GetString("MetadataBinder_PropertyNotDeclaredOrNotKeyInKeyValue", new object[] { p0, p1 });
		}

		// Token: 0x0600193C RID: 6460 RVA: 0x00055160 File Offset: 0x00053360
		internal static string MetadataBinder_UnnamedKeyValueOnTypeWithMultipleKeyProperties(object p0)
		{
			return TextRes.GetString("MetadataBinder_UnnamedKeyValueOnTypeWithMultipleKeyProperties", new object[] { p0 });
		}

		// Token: 0x0600193D RID: 6461 RVA: 0x00055184 File Offset: 0x00053384
		internal static string MetadataBinder_DuplicitKeyPropertyInKeyValues(object p0)
		{
			return TextRes.GetString("MetadataBinder_DuplicitKeyPropertyInKeyValues", new object[] { p0 });
		}

		// Token: 0x0600193E RID: 6462 RVA: 0x000551A8 File Offset: 0x000533A8
		internal static string MetadataBinder_NotAllKeyPropertiesSpecifiedInKeyValues(object p0)
		{
			return TextRes.GetString("MetadataBinder_NotAllKeyPropertiesSpecifiedInKeyValues", new object[] { p0 });
		}

		// Token: 0x0600193F RID: 6463 RVA: 0x000551CC File Offset: 0x000533CC
		internal static string MetadataBinder_CannotConvertToType(object p0, object p1)
		{
			return TextRes.GetString("MetadataBinder_CannotConvertToType", new object[] { p0, p1 });
		}

		// Token: 0x06001940 RID: 6464 RVA: 0x000551F4 File Offset: 0x000533F4
		internal static string MetadataBinder_NonQueryableServiceOperationWithKeyLookup(object p0)
		{
			return TextRes.GetString("MetadataBinder_NonQueryableServiceOperationWithKeyLookup", new object[] { p0 });
		}

		// Token: 0x06001941 RID: 6465 RVA: 0x00055218 File Offset: 0x00053418
		internal static string MetadataBinder_QueryServiceOperationOfNonEntityType(object p0, object p1, object p2)
		{
			return TextRes.GetString("MetadataBinder_QueryServiceOperationOfNonEntityType", new object[] { p0, p1, p2 });
		}

		// Token: 0x06001942 RID: 6466 RVA: 0x00055244 File Offset: 0x00053444
		internal static string MetadataBinder_ServiceOperationParameterMissing(object p0, object p1)
		{
			return TextRes.GetString("MetadataBinder_ServiceOperationParameterMissing", new object[] { p0, p1 });
		}

		// Token: 0x06001943 RID: 6467 RVA: 0x0005526C File Offset: 0x0005346C
		internal static string MetadataBinder_ServiceOperationParameterInvalidType(object p0, object p1, object p2, object p3)
		{
			return TextRes.GetString("MetadataBinder_ServiceOperationParameterInvalidType", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06001944 RID: 6468 RVA: 0x0005529B File Offset: 0x0005349B
		internal static string MetadataBinder_FilterExpressionNotSingleValue
		{
			get
			{
				return TextRes.GetString("MetadataBinder_FilterExpressionNotSingleValue");
			}
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x06001945 RID: 6469 RVA: 0x000552A7 File Offset: 0x000534A7
		internal static string MetadataBinder_OrderByExpressionNotSingleValue
		{
			get
			{
				return TextRes.GetString("MetadataBinder_OrderByExpressionNotSingleValue");
			}
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x06001946 RID: 6470 RVA: 0x000552B3 File Offset: 0x000534B3
		internal static string MetadataBinder_PropertyAccessWithoutParentParameter
		{
			get
			{
				return TextRes.GetString("MetadataBinder_PropertyAccessWithoutParentParameter");
			}
		}

		// Token: 0x06001947 RID: 6471 RVA: 0x000552C0 File Offset: 0x000534C0
		internal static string MetadataBinder_MultiValuePropertyNotSupportedInExpression(object p0)
		{
			return TextRes.GetString("MetadataBinder_MultiValuePropertyNotSupportedInExpression", new object[] { p0 });
		}

		// Token: 0x06001948 RID: 6472 RVA: 0x000552E4 File Offset: 0x000534E4
		internal static string MetadataBinder_BinaryOperatorOperandNotSingleValue(object p0)
		{
			return TextRes.GetString("MetadataBinder_BinaryOperatorOperandNotSingleValue", new object[] { p0 });
		}

		// Token: 0x06001949 RID: 6473 RVA: 0x00055308 File Offset: 0x00053508
		internal static string MetadataBinder_UnaryOperatorOperandNotSingleValue(object p0)
		{
			return TextRes.GetString("MetadataBinder_UnaryOperatorOperandNotSingleValue", new object[] { p0 });
		}

		// Token: 0x0600194A RID: 6474 RVA: 0x0005532C File Offset: 0x0005352C
		internal static string MetadataBinder_PropertyAccessSourceNotSingleValue(object p0)
		{
			return TextRes.GetString("MetadataBinder_PropertyAccessSourceNotSingleValue", new object[] { p0 });
		}

		// Token: 0x0600194B RID: 6475 RVA: 0x00055350 File Offset: 0x00053550
		internal static string MetadataBinder_IncompatibleOperandsError(object p0, object p1, object p2)
		{
			return TextRes.GetString("MetadataBinder_IncompatibleOperandsError", new object[] { p0, p1, p2 });
		}

		// Token: 0x0600194C RID: 6476 RVA: 0x0005537C File Offset: 0x0005357C
		internal static string MetadataBinder_IncompatibleOperandError(object p0, object p1)
		{
			return TextRes.GetString("MetadataBinder_IncompatibleOperandError", new object[] { p0, p1 });
		}

		// Token: 0x0600194D RID: 6477 RVA: 0x000553A4 File Offset: 0x000535A4
		internal static string MetadataBinder_UnknownFunction(object p0)
		{
			return TextRes.GetString("MetadataBinder_UnknownFunction", new object[] { p0 });
		}

		// Token: 0x0600194E RID: 6478 RVA: 0x000553C8 File Offset: 0x000535C8
		internal static string MetadataBinder_FunctionArgumentNotSingleValue(object p0)
		{
			return TextRes.GetString("MetadataBinder_FunctionArgumentNotSingleValue", new object[] { p0 });
		}

		// Token: 0x0600194F RID: 6479 RVA: 0x000553EC File Offset: 0x000535EC
		internal static string MetadataBinder_NoApplicableFunctionFound(object p0, object p1)
		{
			return TextRes.GetString("MetadataBinder_NoApplicableFunctionFound", new object[] { p0, p1 });
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x06001950 RID: 6480 RVA: 0x00055413 File Offset: 0x00053613
		internal static string MetadataBinder_BuiltInFunctionSignatureWithoutAReturnType
		{
			get
			{
				return TextRes.GetString("MetadataBinder_BuiltInFunctionSignatureWithoutAReturnType");
			}
		}

		// Token: 0x06001951 RID: 6481 RVA: 0x00055420 File Offset: 0x00053620
		internal static string MetadataBinder_UnsupportedSystemQueryOption(object p0)
		{
			return TextRes.GetString("MetadataBinder_UnsupportedSystemQueryOption", new object[] { p0 });
		}

		// Token: 0x06001952 RID: 6482 RVA: 0x00055444 File Offset: 0x00053644
		internal static string MetadataBinder_BoundNodeCannotBeNull(object p0)
		{
			return TextRes.GetString("MetadataBinder_BoundNodeCannotBeNull", new object[] { p0 });
		}

		// Token: 0x06001953 RID: 6483 RVA: 0x00055468 File Offset: 0x00053668
		internal static string MetadataBinder_TopRequiresNonNegativeInteger(object p0)
		{
			return TextRes.GetString("MetadataBinder_TopRequiresNonNegativeInteger", new object[] { p0 });
		}

		// Token: 0x06001954 RID: 6484 RVA: 0x0005548C File Offset: 0x0005368C
		internal static string MetadataBinder_SkipRequiresNonNegativeInteger(object p0)
		{
			return TextRes.GetString("MetadataBinder_SkipRequiresNonNegativeInteger", new object[] { p0 });
		}

		// Token: 0x06001955 RID: 6485 RVA: 0x000554B0 File Offset: 0x000536B0
		internal static string MetadataBinder_ServiceOperationWithoutResultKind(object p0)
		{
			return TextRes.GetString("MetadataBinder_ServiceOperationWithoutResultKind", new object[] { p0 });
		}

		// Token: 0x06001956 RID: 6486 RVA: 0x000554D4 File Offset: 0x000536D4
		internal static string MetadataBinder_HierarchyNotFollowed(object p0, object p1)
		{
			return TextRes.GetString("MetadataBinder_HierarchyNotFollowed", new object[] { p0, p1 });
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x06001957 RID: 6487 RVA: 0x000554FB File Offset: 0x000536FB
		internal static string MetadataBinder_MustBeCalledOnRoot
		{
			get
			{
				return TextRes.GetString("MetadataBinder_MustBeCalledOnRoot");
			}
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06001958 RID: 6488 RVA: 0x00055507 File Offset: 0x00053707
		internal static string MetadataBinder_NoTypeSupported
		{
			get
			{
				return TextRes.GetString("MetadataBinder_NoTypeSupported");
			}
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06001959 RID: 6489 RVA: 0x00055513 File Offset: 0x00053713
		internal static string MetadataBinder_LambdaParentMustBeCollection
		{
			get
			{
				return TextRes.GetString("MetadataBinder_LambdaParentMustBeCollection");
			}
		}

		// Token: 0x0600195A RID: 6490 RVA: 0x00055520 File Offset: 0x00053720
		internal static string MetadataBinder_ParameterNotInScope(object p0)
		{
			return TextRes.GetString("MetadataBinder_ParameterNotInScope", new object[] { p0 });
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x0600195B RID: 6491 RVA: 0x00055543 File Offset: 0x00053743
		internal static string MetadataBinder_NullNavigationProperty
		{
			get
			{
				return TextRes.GetString("MetadataBinder_NullNavigationProperty");
			}
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x0600195C RID: 6492 RVA: 0x0005554F File Offset: 0x0005374F
		internal static string MetadataBinder_NavigationPropertyNotFollowingSingleEntityType
		{
			get
			{
				return TextRes.GetString("MetadataBinder_NavigationPropertyNotFollowingSingleEntityType");
			}
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x0600195D RID: 6493 RVA: 0x0005555B File Offset: 0x0005375B
		internal static string MetadataBinder_AnyAllExpressionNotSingleValue
		{
			get
			{
				return TextRes.GetString("MetadataBinder_AnyAllExpressionNotSingleValue");
			}
		}

		// Token: 0x0600195E RID: 6494 RVA: 0x00055568 File Offset: 0x00053768
		internal static string MetadataBinder_CastOrIsOfExpressionWithWrongNumberOfOperands(object p0)
		{
			return TextRes.GetString("MetadataBinder_CastOrIsOfExpressionWithWrongNumberOfOperands", new object[] { p0 });
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x0600195F RID: 6495 RVA: 0x0005558B File Offset: 0x0005378B
		internal static string MetadataBinder_CastOrIsOfFunctionWithoutATypeArgument
		{
			get
			{
				return TextRes.GetString("MetadataBinder_CastOrIsOfFunctionWithoutATypeArgument");
			}
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06001960 RID: 6496 RVA: 0x00055597 File Offset: 0x00053797
		internal static string MetadataBinder_CastOrIsOfCollectionsNotSupported
		{
			get
			{
				return TextRes.GetString("MetadataBinder_CastOrIsOfCollectionsNotSupported");
			}
		}

		// Token: 0x06001961 RID: 6497 RVA: 0x000555A4 File Offset: 0x000537A4
		internal static string MetadataBinder_SpatialLengthFunctionWithInvalidArgs(object p0)
		{
			return TextRes.GetString("MetadataBinder_SpatialLengthFunctionWithInvalidArgs", new object[] { p0 });
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06001962 RID: 6498 RVA: 0x000555C7 File Offset: 0x000537C7
		internal static string MetadataBinder_SpatialLengthFunctionWithoutASingleValueArg
		{
			get
			{
				return TextRes.GetString("MetadataBinder_SpatialLengthFunctionWithoutASingleValueArg");
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06001963 RID: 6499 RVA: 0x000555D3 File Offset: 0x000537D3
		internal static string MetadataBinder_SpatialLengthFunctionWithOutLineStringArg
		{
			get
			{
				return TextRes.GetString("MetadataBinder_SpatialLengthFunctionWithOutLineStringArg");
			}
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x000555E0 File Offset: 0x000537E0
		internal static string MetadataBinder_SpatialIntersectsFunctionWithInvalidArgs(object p0)
		{
			return TextRes.GetString("MetadataBinder_SpatialIntersectsFunctionWithInvalidArgs", new object[] { p0 });
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06001965 RID: 6501 RVA: 0x00055603 File Offset: 0x00053803
		internal static string MetadataBinder_SpatialIntersectsFunctionWithoutASingleValueArg
		{
			get
			{
				return TextRes.GetString("MetadataBinder_SpatialIntersectsFunctionWithoutASingleValueArg");
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06001966 RID: 6502 RVA: 0x0005560F File Offset: 0x0005380F
		internal static string MetadataBinder_SpatialIntersectsFunctionWithInvalidArgTypes
		{
			get
			{
				return TextRes.GetString("MetadataBinder_SpatialIntersectsFunctionWithInvalidArgTypes");
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06001967 RID: 6503 RVA: 0x0005561B File Offset: 0x0005381B
		internal static string MetadataBinder_NonValidTypeArgument
		{
			get
			{
				return TextRes.GetString("MetadataBinder_NonValidTypeArgument");
			}
		}

		// Token: 0x06001968 RID: 6504 RVA: 0x00055628 File Offset: 0x00053828
		internal static string MetadataBinder_OperatorNotSupportedInThisVersion(object p0)
		{
			return TextRes.GetString("MetadataBinder_OperatorNotSupportedInThisVersion", new object[] { p0 });
		}

		// Token: 0x06001969 RID: 6505 RVA: 0x0005564C File Offset: 0x0005384C
		internal static string MetadataBinder_KeywordNotSupportedInThisRelease(object p0)
		{
			return TextRes.GetString("MetadataBinder_KeywordNotSupportedInThisRelease", new object[] { p0 });
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x0600196A RID: 6506 RVA: 0x0005566F File Offset: 0x0005386F
		internal static string MetadataBinder_CollectionOpenPropertiesNotSupportedInThisRelease
		{
			get
			{
				return TextRes.GetString("MetadataBinder_CollectionOpenPropertiesNotSupportedInThisRelease");
			}
		}

		// Token: 0x0600196B RID: 6507 RVA: 0x0005567C File Offset: 0x0005387C
		internal static string MetadataBinder_IllegalSegmentType(object p0)
		{
			return TextRes.GetString("MetadataBinder_IllegalSegmentType", new object[] { p0 });
		}

		// Token: 0x0600196C RID: 6508 RVA: 0x000556A0 File Offset: 0x000538A0
		internal static string MetadataBinder_QueryOptionNotApplicable(object p0)
		{
			return TextRes.GetString("MetadataBinder_QueryOptionNotApplicable", new object[] { p0 });
		}

		// Token: 0x0600196D RID: 6509 RVA: 0x000556C4 File Offset: 0x000538C4
		internal static string FunctionCallBinder_CannotFindASuitableOverload(object p0, object p1)
		{
			return TextRes.GetString("FunctionCallBinder_CannotFindASuitableOverload", new object[] { p0, p1 });
		}

		// Token: 0x0600196E RID: 6510 RVA: 0x000556EC File Offset: 0x000538EC
		internal static string FunctionCallBinder_NonSingleValueParent(object p0)
		{
			return TextRes.GetString("FunctionCallBinder_NonSingleValueParent", new object[] { p0 });
		}

		// Token: 0x0600196F RID: 6511 RVA: 0x00055710 File Offset: 0x00053910
		internal static string FunctionCallBinder_FoundInvalidFunctionImports(object p0)
		{
			return TextRes.GetString("FunctionCallBinder_FoundInvalidFunctionImports", new object[] { p0 });
		}

		// Token: 0x06001970 RID: 6512 RVA: 0x00055734 File Offset: 0x00053934
		internal static string FunctionCallBinder_BuiltInFunctionMustHaveHaveNullParent(object p0)
		{
			return TextRes.GetString("FunctionCallBinder_BuiltInFunctionMustHaveHaveNullParent", new object[] { p0 });
		}

		// Token: 0x06001971 RID: 6513 RVA: 0x00055758 File Offset: 0x00053958
		internal static string FunctionCallBinder_CallingFunctionOnOpenProperty(object p0)
		{
			return TextRes.GetString("FunctionCallBinder_CallingFunctionOnOpenProperty", new object[] { p0 });
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x06001972 RID: 6514 RVA: 0x0005577B File Offset: 0x0005397B
		internal static string FunctionCallParser_DuplicateParameterName
		{
			get
			{
				return TextRes.GetString("FunctionCallParser_DuplicateParameterName");
			}
		}

		// Token: 0x06001973 RID: 6515 RVA: 0x00055788 File Offset: 0x00053988
		internal static string ODataUriParser_InvalidInlineCount(object p0)
		{
			return TextRes.GetString("ODataUriParser_InvalidInlineCount", new object[] { p0 });
		}

		// Token: 0x06001974 RID: 6516 RVA: 0x000557AC File Offset: 0x000539AC
		internal static string CastBinder_ChildTypeIsNotEntity(object p0)
		{
			return TextRes.GetString("CastBinder_ChildTypeIsNotEntity", new object[] { p0 });
		}

		// Token: 0x06001975 RID: 6517 RVA: 0x000557D0 File Offset: 0x000539D0
		internal static string BatchReferenceSegment_InvalidContentID(object p0)
		{
			return TextRes.GetString("BatchReferenceSegment_InvalidContentID", new object[] { p0 });
		}

		// Token: 0x06001976 RID: 6518 RVA: 0x000557F4 File Offset: 0x000539F4
		internal static string SelectExpandBinder_UnknownPropertyType(object p0)
		{
			return TextRes.GetString("SelectExpandBinder_UnknownPropertyType", new object[] { p0 });
		}

		// Token: 0x06001977 RID: 6519 RVA: 0x00055818 File Offset: 0x00053A18
		internal static string SelectExpandBinder_CantFindProperty(object p0)
		{
			return TextRes.GetString("SelectExpandBinder_CantFindProperty", new object[] { p0 });
		}

		// Token: 0x06001978 RID: 6520 RVA: 0x0005583C File Offset: 0x00053A3C
		internal static string SelectionItemBinder_NoExpandForSelectedProperty(object p0)
		{
			return TextRes.GetString("SelectionItemBinder_NoExpandForSelectedProperty", new object[] { p0 });
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06001979 RID: 6521 RVA: 0x0005585F File Offset: 0x00053A5F
		internal static string SelectionItemBinder_NonPathSelectToken
		{
			get
			{
				return TextRes.GetString("SelectionItemBinder_NonPathSelectToken");
			}
		}

		// Token: 0x0600197A RID: 6522 RVA: 0x0005586C File Offset: 0x00053A6C
		internal static string SelectionItemBinder_NonEntityTypeSegment(object p0)
		{
			return TextRes.GetString("SelectionItemBinder_NonEntityTypeSegment", new object[] { p0 });
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x00055890 File Offset: 0x00053A90
		internal static string SelectExpandPathBinder_FollowNonTypeSegment(object p0)
		{
			return TextRes.GetString("SelectExpandPathBinder_FollowNonTypeSegment", new object[] { p0 });
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x000558B4 File Offset: 0x00053AB4
		internal static string SelectPropertyVisitor_SystemTokenInSelect(object p0)
		{
			return TextRes.GetString("SelectPropertyVisitor_SystemTokenInSelect", new object[] { p0 });
		}

		// Token: 0x0600197D RID: 6525 RVA: 0x000558D8 File Offset: 0x00053AD8
		internal static string SelectPropertyVisitor_InvalidSegmentInSelectPath(object p0)
		{
			return TextRes.GetString("SelectPropertyVisitor_InvalidSegmentInSelectPath", new object[] { p0 });
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x0600197E RID: 6526 RVA: 0x000558FB File Offset: 0x00053AFB
		internal static string SelectPropertyVisitor_DisparateTypeSegmentsInSelectExpand
		{
			get
			{
				return TextRes.GetString("SelectPropertyVisitor_DisparateTypeSegmentsInSelectExpand");
			}
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x0600197F RID: 6527 RVA: 0x00055907 File Offset: 0x00053B07
		internal static string SelectExpandClause_CannotDeleteFromAllSelection
		{
			get
			{
				return TextRes.GetString("SelectExpandClause_CannotDeleteFromAllSelection");
			}
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06001980 RID: 6528 RVA: 0x00055913 File Offset: 0x00053B13
		internal static string SegmentFactory_LinksSegmentNotFollowedByNavProp
		{
			get
			{
				return TextRes.GetString("SegmentFactory_LinksSegmentNotFollowedByNavProp");
			}
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06001981 RID: 6529 RVA: 0x0005591F File Offset: 0x00053B1F
		internal static string ExpandItemBinder_TraversingANonNormalizedTree
		{
			get
			{
				return TextRes.GetString("ExpandItemBinder_TraversingANonNormalizedTree");
			}
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x0005592C File Offset: 0x00053B2C
		internal static string ExpandItemBinder_CannotFindType(object p0)
		{
			return TextRes.GetString("ExpandItemBinder_CannotFindType", new object[] { p0 });
		}

		// Token: 0x06001983 RID: 6531 RVA: 0x00055950 File Offset: 0x00053B50
		internal static string ExpandItemBinder_PropertyIsNotANavigationProperty(object p0, object p1)
		{
			return TextRes.GetString("ExpandItemBinder_PropertyIsNotANavigationProperty", new object[] { p0, p1 });
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06001984 RID: 6532 RVA: 0x00055977 File Offset: 0x00053B77
		internal static string ExpandItemBinder_TypeSegmentNotFollowedByPath
		{
			get
			{
				return TextRes.GetString("ExpandItemBinder_TypeSegmentNotFollowedByPath");
			}
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06001985 RID: 6533 RVA: 0x00055983 File Offset: 0x00053B83
		internal static string ExpandItemBinder_PathTooDeep
		{
			get
			{
				return TextRes.GetString("ExpandItemBinder_PathTooDeep");
			}
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06001986 RID: 6534 RVA: 0x0005598F File Offset: 0x00053B8F
		internal static string Nodes_CollectionNavigationNode_MustHaveSingleMultiplicity
		{
			get
			{
				return TextRes.GetString("Nodes_CollectionNavigationNode_MustHaveSingleMultiplicity");
			}
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x0005599C File Offset: 0x00053B9C
		internal static string Nodes_NonentityParameterQueryNodeWithEntityType(object p0)
		{
			return TextRes.GetString("Nodes_NonentityParameterQueryNodeWithEntityType", new object[] { p0 });
		}

		// Token: 0x06001988 RID: 6536 RVA: 0x000559C0 File Offset: 0x00053BC0
		internal static string Nodes_EntityCollectionServiceOperationRequiresEntityReturnType(object p0)
		{
			return TextRes.GetString("Nodes_EntityCollectionServiceOperationRequiresEntityReturnType", new object[] { p0 });
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06001989 RID: 6537 RVA: 0x000559E3 File Offset: 0x00053BE3
		internal static string Nodes_CollectionNavigationNode_MustHaveManyMultiplicity
		{
			get
			{
				return TextRes.GetString("Nodes_CollectionNavigationNode_MustHaveManyMultiplicity");
			}
		}

		// Token: 0x0600198A RID: 6538 RVA: 0x000559F0 File Offset: 0x00053BF0
		internal static string Nodes_PropertyAccessShouldBeNonEntityProperty(object p0)
		{
			return TextRes.GetString("Nodes_PropertyAccessShouldBeNonEntityProperty", new object[] { p0 });
		}

		// Token: 0x0600198B RID: 6539 RVA: 0x00055A14 File Offset: 0x00053C14
		internal static string Nodes_PropertyAccessTypeShouldNotBeCollection(object p0)
		{
			return TextRes.GetString("Nodes_PropertyAccessTypeShouldNotBeCollection", new object[] { p0 });
		}

		// Token: 0x0600198C RID: 6540 RVA: 0x00055A38 File Offset: 0x00053C38
		internal static string Nodes_PropertyAccessTypeMustBeCollection(object p0)
		{
			return TextRes.GetString("Nodes_PropertyAccessTypeMustBeCollection", new object[] { p0 });
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x0600198D RID: 6541 RVA: 0x00055A5B File Offset: 0x00053C5B
		internal static string Nodes_NonStaticEntitySetExpressionsAreNotSupportedInThisRelease
		{
			get
			{
				return TextRes.GetString("Nodes_NonStaticEntitySetExpressionsAreNotSupportedInThisRelease");
			}
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x0600198E RID: 6542 RVA: 0x00055A67 File Offset: 0x00053C67
		internal static string Nodes_CollectionFunctionCallNode_ItemTypeMustBePrimitiveOrComplex
		{
			get
			{
				return TextRes.GetString("Nodes_CollectionFunctionCallNode_ItemTypeMustBePrimitiveOrComplex");
			}
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x0600198F RID: 6543 RVA: 0x00055A73 File Offset: 0x00053C73
		internal static string Nodes_EntityCollectionFunctionCallNode_ItemTypeMustBeAnEntity
		{
			get
			{
				return TextRes.GetString("Nodes_EntityCollectionFunctionCallNode_ItemTypeMustBeAnEntity");
			}
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06001990 RID: 6544 RVA: 0x00055A7F File Offset: 0x00053C7F
		internal static string ExpandTreeNormalizer_CallAddTermsOnUnexpandedTerms
		{
			get
			{
				return TextRes.GetString("ExpandTreeNormalizer_CallAddTermsOnUnexpandedTerms");
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06001991 RID: 6545 RVA: 0x00055A8B File Offset: 0x00053C8B
		internal static string ExpandTreeNormalizer_NonPathInPropertyChain
		{
			get
			{
				return TextRes.GetString("ExpandTreeNormalizer_NonPathInPropertyChain");
			}
		}

		// Token: 0x06001992 RID: 6546 RVA: 0x00055A98 File Offset: 0x00053C98
		internal static string UriSelectParser_TermIsNotValid(object p0)
		{
			return TextRes.GetString("UriSelectParser_TermIsNotValid", new object[] { p0 });
		}

		// Token: 0x06001993 RID: 6547 RVA: 0x00055ABC File Offset: 0x00053CBC
		internal static string UriSelectParser_FunctionsAreNotAllowed(object p0)
		{
			return TextRes.GetString("UriSelectParser_FunctionsAreNotAllowed", new object[] { p0 });
		}

		// Token: 0x06001994 RID: 6548 RVA: 0x00055AE0 File Offset: 0x00053CE0
		internal static string UriSelectParser_InvalidTopOption(object p0)
		{
			return TextRes.GetString("UriSelectParser_InvalidTopOption", new object[] { p0 });
		}

		// Token: 0x06001995 RID: 6549 RVA: 0x00055B04 File Offset: 0x00053D04
		internal static string UriSelectParser_InvalidSkipOption(object p0)
		{
			return TextRes.GetString("UriSelectParser_InvalidSkipOption", new object[] { p0 });
		}

		// Token: 0x06001996 RID: 6550 RVA: 0x00055B28 File Offset: 0x00053D28
		internal static string UriSelectParser_SystemTokenInSelectExpand(object p0, object p1)
		{
			return TextRes.GetString("UriSelectParser_SystemTokenInSelectExpand", new object[] { p0, p1 });
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06001997 RID: 6551 RVA: 0x00055B4F File Offset: 0x00053D4F
		internal static string UriParser_NeedServiceRootForThisOverload
		{
			get
			{
				return TextRes.GetString("UriParser_NeedServiceRootForThisOverload");
			}
		}

		// Token: 0x06001998 RID: 6552 RVA: 0x00055B5C File Offset: 0x00053D5C
		internal static string UriParser_UriMustBeAbsolute(object p0)
		{
			return TextRes.GetString("UriParser_UriMustBeAbsolute", new object[] { p0 });
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06001999 RID: 6553 RVA: 0x00055B7F File Offset: 0x00053D7F
		internal static string UriParser_NegativeLimit
		{
			get
			{
				return TextRes.GetString("UriParser_NegativeLimit");
			}
		}

		// Token: 0x0600199A RID: 6554 RVA: 0x00055B8C File Offset: 0x00053D8C
		internal static string UriParser_ExpandCountExceeded(object p0, object p1)
		{
			return TextRes.GetString("UriParser_ExpandCountExceeded", new object[] { p0, p1 });
		}

		// Token: 0x0600199B RID: 6555 RVA: 0x00055BB4 File Offset: 0x00053DB4
		internal static string UriParser_ExpandDepthExceeded(object p0, object p1)
		{
			return TextRes.GetString("UriParser_ExpandDepthExceeded", new object[] { p0, p1 });
		}

		// Token: 0x0600199C RID: 6556 RVA: 0x00055BDC File Offset: 0x00053DDC
		internal static string PathParser_ServiceOperationWithoutResultKindAttribute(object p0)
		{
			return TextRes.GetString("PathParser_ServiceOperationWithoutResultKindAttribute", new object[] { p0 });
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x0600199D RID: 6557 RVA: 0x00055BFF File Offset: 0x00053DFF
		internal static string PathParser_FunctionsAreNotSupported
		{
			get
			{
				return TextRes.GetString("PathParser_FunctionsAreNotSupported");
			}
		}

		// Token: 0x0600199E RID: 6558 RVA: 0x00055C0C File Offset: 0x00053E0C
		internal static string PathParser_ServiceOperationsWithSameName(object p0)
		{
			return TextRes.GetString("PathParser_ServiceOperationsWithSameName", new object[] { p0 });
		}

		// Token: 0x0600199F RID: 6559 RVA: 0x00055C30 File Offset: 0x00053E30
		internal static string PathParser_LinksNotSupported(object p0)
		{
			return TextRes.GetString("PathParser_LinksNotSupported", new object[] { p0 });
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x060019A0 RID: 6560 RVA: 0x00055C53 File Offset: 0x00053E53
		internal static string PathParser_CannotUseValueOnCollection
		{
			get
			{
				return TextRes.GetString("PathParser_CannotUseValueOnCollection");
			}
		}

		// Token: 0x060019A1 RID: 6561 RVA: 0x00055C60 File Offset: 0x00053E60
		internal static string PathParser_TypeMustBeRelatedToSet(object p0, object p1, object p2)
		{
			return TextRes.GetString("PathParser_TypeMustBeRelatedToSet", new object[] { p0, p1, p2 });
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x060019A2 RID: 6562 RVA: 0x00055C8B File Offset: 0x00053E8B
		internal static string ODataFeed_MustNotContainBothNextPageLinkAndDeltaLink
		{
			get
			{
				return TextRes.GetString("ODataFeed_MustNotContainBothNextPageLinkAndDeltaLink");
			}
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x060019A3 RID: 6563 RVA: 0x00055C97 File Offset: 0x00053E97
		internal static string ODataExpandPath_OnlyLastSegmentMustBeNavigationProperty
		{
			get
			{
				return TextRes.GetString("ODataExpandPath_OnlyLastSegmentMustBeNavigationProperty");
			}
		}

		// Token: 0x060019A4 RID: 6564 RVA: 0x00055CA4 File Offset: 0x00053EA4
		internal static string ODataExpandPath_InvalidExpandPathSegment(object p0)
		{
			return TextRes.GetString("ODataExpandPath_InvalidExpandPathSegment", new object[] { p0 });
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x060019A5 RID: 6565 RVA: 0x00055CC7 File Offset: 0x00053EC7
		internal static string ODataSelectPath_CannotEndInTypeSegment
		{
			get
			{
				return TextRes.GetString("ODataSelectPath_CannotEndInTypeSegment");
			}
		}

		// Token: 0x060019A6 RID: 6566 RVA: 0x00055CD4 File Offset: 0x00053ED4
		internal static string ODataSelectPath_InvalidSelectPathSegmentType(object p0)
		{
			return TextRes.GetString("ODataSelectPath_InvalidSelectPathSegmentType", new object[] { p0 });
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x060019A7 RID: 6567 RVA: 0x00055CF7 File Offset: 0x00053EF7
		internal static string ODataSelectPath_OperationSegmentCanOnlyBeLastSegment
		{
			get
			{
				return TextRes.GetString("ODataSelectPath_OperationSegmentCanOnlyBeLastSegment");
			}
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x060019A8 RID: 6568 RVA: 0x00055D03 File Offset: 0x00053F03
		internal static string ODataSelectPath_NavPropSegmentCanOnlyBeLastSegment
		{
			get
			{
				return TextRes.GetString("ODataSelectPath_NavPropSegmentCanOnlyBeLastSegment");
			}
		}

		// Token: 0x060019A9 RID: 6569 RVA: 0x00055D10 File Offset: 0x00053F10
		internal static string RequestUriProcessor_EntitySetNotSpecified(object p0)
		{
			return TextRes.GetString("RequestUriProcessor_EntitySetNotSpecified", new object[] { p0 });
		}

		// Token: 0x060019AA RID: 6570 RVA: 0x00055D34 File Offset: 0x00053F34
		internal static string RequestUriProcessor_TargetEntitySetNotFound(object p0)
		{
			return TextRes.GetString("RequestUriProcessor_TargetEntitySetNotFound", new object[] { p0 });
		}

		// Token: 0x060019AB RID: 6571 RVA: 0x00055D58 File Offset: 0x00053F58
		internal static string RequestUriProcessor_FoundInvalidFunctionImport(object p0)
		{
			return TextRes.GetString("RequestUriProcessor_FoundInvalidFunctionImport", new object[] { p0 });
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x060019AC RID: 6572 RVA: 0x00055D7B File Offset: 0x00053F7B
		internal static string OperationSegment_ReturnTypeForMultipleOverloads
		{
			get
			{
				return TextRes.GetString("OperationSegment_ReturnTypeForMultipleOverloads");
			}
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x060019AD RID: 6573 RVA: 0x00055D87 File Offset: 0x00053F87
		internal static string OperationSegment_CannotReturnNull
		{
			get
			{
				return TextRes.GetString("OperationSegment_CannotReturnNull");
			}
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x060019AE RID: 6574 RVA: 0x00055D93 File Offset: 0x00053F93
		internal static string SingleValueFunctionCallNode_FunctionImportsWithLegacyConstructor
		{
			get
			{
				return TextRes.GetString("SingleValueFunctionCallNode_FunctionImportsWithLegacyConstructor");
			}
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x060019AF RID: 6575 RVA: 0x00055D9F File Offset: 0x00053F9F
		internal static string SingleEntityFunctionCallNode_CallFunctionImportsUsingLegacyConstructor
		{
			get
			{
				return TextRes.GetString("SingleEntityFunctionCallNode_CallFunctionImportsUsingLegacyConstructor");
			}
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x060019B0 RID: 6576 RVA: 0x00055DAB File Offset: 0x00053FAB
		internal static string SegmentArgumentParser_TryConvertValuesForNamedValues
		{
			get
			{
				return TextRes.GetString("SegmentArgumentParser_TryConvertValuesForNamedValues");
			}
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x060019B1 RID: 6577 RVA: 0x00055DB7 File Offset: 0x00053FB7
		internal static string SegmentArgumentParser_TryConvertValuesToNonPrimitive
		{
			get
			{
				return TextRes.GetString("SegmentArgumentParser_TryConvertValuesToNonPrimitive");
			}
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x060019B2 RID: 6578 RVA: 0x00055DC3 File Offset: 0x00053FC3
		internal static string SegmentArgumentParser_TryConvertValuesForPositionalValues
		{
			get
			{
				return TextRes.GetString("SegmentArgumentParser_TryConvertValuesForPositionalValues");
			}
		}

		// Token: 0x060019B3 RID: 6579 RVA: 0x00055DD0 File Offset: 0x00053FD0
		internal static string FunctionOverloadResolver_NoSingleMatchFound(object p0, object p1)
		{
			return TextRes.GetString("FunctionOverloadResolver_NoSingleMatchFound", new object[] { p0, p1 });
		}

		// Token: 0x060019B4 RID: 6580 RVA: 0x00055DF8 File Offset: 0x00053FF8
		internal static string FunctionOverloadResolver_MultipleActionOverloads(object p0)
		{
			return TextRes.GetString("FunctionOverloadResolver_MultipleActionOverloads", new object[] { p0 });
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x060019B5 RID: 6581 RVA: 0x00055E1B File Offset: 0x0005401B
		internal static string RequestUriProcessor_EmptySegmentInRequestUrl
		{
			get
			{
				return TextRes.GetString("RequestUriProcessor_EmptySegmentInRequestUrl");
			}
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x060019B6 RID: 6582 RVA: 0x00055E27 File Offset: 0x00054027
		internal static string RequestUriProcessor_SyntaxError
		{
			get
			{
				return TextRes.GetString("RequestUriProcessor_SyntaxError");
			}
		}

		// Token: 0x060019B7 RID: 6583 RVA: 0x00055E34 File Offset: 0x00054034
		internal static string RequestUriProcessor_CannotSpecifyAfterPostLinkSegment(object p0, object p1)
		{
			return TextRes.GetString("RequestUriProcessor_CannotSpecifyAfterPostLinkSegment", new object[] { p0, p1 });
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x060019B8 RID: 6584 RVA: 0x00055E5B File Offset: 0x0005405B
		internal static string RequestUriProcessor_CountOnRoot
		{
			get
			{
				return TextRes.GetString("RequestUriProcessor_CountOnRoot");
			}
		}

		// Token: 0x060019B9 RID: 6585 RVA: 0x00055E68 File Offset: 0x00054068
		internal static string RequestUriProcessor_MustBeLeafSegment(object p0)
		{
			return TextRes.GetString("RequestUriProcessor_MustBeLeafSegment", new object[] { p0 });
		}

		// Token: 0x060019BA RID: 6586 RVA: 0x00055E8C File Offset: 0x0005408C
		internal static string RequestUriProcessor_LinkSegmentMustBeFollowedByEntitySegment(object p0, object p1)
		{
			return TextRes.GetString("RequestUriProcessor_LinkSegmentMustBeFollowedByEntitySegment", new object[] { p0, p1 });
		}

		// Token: 0x060019BB RID: 6587 RVA: 0x00055EB4 File Offset: 0x000540B4
		internal static string RequestUriProcessor_MissingSegmentAfterLink(object p0)
		{
			return TextRes.GetString("RequestUriProcessor_MissingSegmentAfterLink", new object[] { p0 });
		}

		// Token: 0x060019BC RID: 6588 RVA: 0x00055ED8 File Offset: 0x000540D8
		internal static string RequestUriProcessor_CountNotSupported(object p0)
		{
			return TextRes.GetString("RequestUriProcessor_CountNotSupported", new object[] { p0 });
		}

		// Token: 0x060019BD RID: 6589 RVA: 0x00055EFC File Offset: 0x000540FC
		internal static string RequestUriProcessor_CannotQuerySingletons(object p0, object p1)
		{
			return TextRes.GetString("RequestUriProcessor_CannotQuerySingletons", new object[] { p0, p1 });
		}

		// Token: 0x060019BE RID: 6590 RVA: 0x00055F24 File Offset: 0x00054124
		internal static string RequestUriProcessor_CannotQueryCollections(object p0)
		{
			return TextRes.GetString("RequestUriProcessor_CannotQueryCollections", new object[] { p0 });
		}

		// Token: 0x060019BF RID: 6591 RVA: 0x00055F48 File Offset: 0x00054148
		internal static string RequestUriProcessor_SegmentDoesNotSupportKeyPredicates(object p0)
		{
			return TextRes.GetString("RequestUriProcessor_SegmentDoesNotSupportKeyPredicates", new object[] { p0 });
		}

		// Token: 0x060019C0 RID: 6592 RVA: 0x00055F6C File Offset: 0x0005416C
		internal static string RequestUriProcessor_ValueSegmentAfterScalarPropertySegment(object p0, object p1)
		{
			return TextRes.GetString("RequestUriProcessor_ValueSegmentAfterScalarPropertySegment", new object[] { p0, p1 });
		}

		// Token: 0x060019C1 RID: 6593 RVA: 0x00055F94 File Offset: 0x00054194
		internal static string RequestUriProcessor_InvalidTypeIdentifier_UnrelatedType(object p0, object p1)
		{
			return TextRes.GetString("RequestUriProcessor_InvalidTypeIdentifier_UnrelatedType", new object[] { p0, p1 });
		}

		// Token: 0x060019C2 RID: 6594 RVA: 0x00055FBC File Offset: 0x000541BC
		internal static string ResourceType_ComplexTypeCannotBeOpen(object p0)
		{
			return TextRes.GetString("ResourceType_ComplexTypeCannotBeOpen", new object[] { p0 });
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x060019C3 RID: 6595 RVA: 0x00055FDF File Offset: 0x000541DF
		internal static string BadRequest_ValuesCannotBeReturnedForSpatialTypes
		{
			get
			{
				return TextRes.GetString("BadRequest_ValuesCannotBeReturnedForSpatialTypes");
			}
		}

		// Token: 0x060019C4 RID: 6596 RVA: 0x00055FEC File Offset: 0x000541EC
		internal static string OpenNavigationPropertiesNotSupportedOnOpenTypes(object p0)
		{
			return TextRes.GetString("OpenNavigationPropertiesNotSupportedOnOpenTypes", new object[] { p0 });
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x060019C5 RID: 6597 RVA: 0x0005600F File Offset: 0x0005420F
		internal static string BadRequest_ResourceCanBeCrossReferencedOnlyForBindOperation
		{
			get
			{
				return TextRes.GetString("BadRequest_ResourceCanBeCrossReferencedOnlyForBindOperation");
			}
		}

		// Token: 0x060019C6 RID: 6598 RVA: 0x0005601C File Offset: 0x0005421C
		internal static string DataServiceConfiguration_ResponseVersionIsBiggerThanProtocolVersion(object p0, object p1)
		{
			return TextRes.GetString("DataServiceConfiguration_ResponseVersionIsBiggerThanProtocolVersion", new object[] { p0, p1 });
		}

		// Token: 0x060019C7 RID: 6599 RVA: 0x00056044 File Offset: 0x00054244
		internal static string BadRequest_KeyCountMismatch(object p0)
		{
			return TextRes.GetString("BadRequest_KeyCountMismatch", new object[] { p0 });
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x060019C8 RID: 6600 RVA: 0x00056067 File Offset: 0x00054267
		internal static string RequestUriProcessor_KeysMustBeNamed
		{
			get
			{
				return TextRes.GetString("RequestUriProcessor_KeysMustBeNamed");
			}
		}

		// Token: 0x060019C9 RID: 6601 RVA: 0x00056074 File Offset: 0x00054274
		internal static string RequestUriProcessor_ResourceNotFound(object p0)
		{
			return TextRes.GetString("RequestUriProcessor_ResourceNotFound", new object[] { p0 });
		}

		// Token: 0x060019CA RID: 6602 RVA: 0x00056098 File Offset: 0x00054298
		internal static string RequestUriProcessor_BatchedActionOnEntityCreatedInSameChangeset(object p0)
		{
			return TextRes.GetString("RequestUriProcessor_BatchedActionOnEntityCreatedInSameChangeset", new object[] { p0 });
		}

		// Token: 0x060019CB RID: 6603 RVA: 0x000560BC File Offset: 0x000542BC
		internal static string RequestUriProcessor_IEnumerableServiceOperationsCannotBeFurtherComposed(object p0)
		{
			return TextRes.GetString("RequestUriProcessor_IEnumerableServiceOperationsCannotBeFurtherComposed", new object[] { p0 });
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x060019CC RID: 6604 RVA: 0x000560DF File Offset: 0x000542DF
		internal static string RequestUriProcessor_Forbidden
		{
			get
			{
				return TextRes.GetString("RequestUriProcessor_Forbidden");
			}
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x060019CD RID: 6605 RVA: 0x000560EB File Offset: 0x000542EB
		internal static string RequestUriProcessor_OperationSegmentBoundToANonEntityType
		{
			get
			{
				return TextRes.GetString("RequestUriProcessor_OperationSegmentBoundToANonEntityType");
			}
		}

		// Token: 0x060019CE RID: 6606 RVA: 0x000560F8 File Offset: 0x000542F8
		internal static string General_InternalError(object p0)
		{
			return TextRes.GetString("General_InternalError", new object[] { p0 });
		}

		// Token: 0x060019CF RID: 6607 RVA: 0x0005611C File Offset: 0x0005431C
		internal static string ExceptionUtils_CheckIntegerNotNegative(object p0)
		{
			return TextRes.GetString("ExceptionUtils_CheckIntegerNotNegative", new object[] { p0 });
		}

		// Token: 0x060019D0 RID: 6608 RVA: 0x00056140 File Offset: 0x00054340
		internal static string ExceptionUtils_CheckIntegerPositive(object p0)
		{
			return TextRes.GetString("ExceptionUtils_CheckIntegerPositive", new object[] { p0 });
		}

		// Token: 0x060019D1 RID: 6609 RVA: 0x00056164 File Offset: 0x00054364
		internal static string ExceptionUtils_CheckLongPositive(object p0)
		{
			return TextRes.GetString("ExceptionUtils_CheckLongPositive", new object[] { p0 });
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x060019D2 RID: 6610 RVA: 0x00056187 File Offset: 0x00054387
		internal static string ExceptionUtils_ArgumentStringNullOrEmpty
		{
			get
			{
				return TextRes.GetString("ExceptionUtils_ArgumentStringNullOrEmpty");
			}
		}

		// Token: 0x060019D3 RID: 6611 RVA: 0x00056194 File Offset: 0x00054394
		internal static string ExpressionToken_IdentifierExpected(object p0)
		{
			return TextRes.GetString("ExpressionToken_IdentifierExpected", new object[] { p0 });
		}

		// Token: 0x060019D4 RID: 6612 RVA: 0x000561B8 File Offset: 0x000543B8
		internal static string ExpressionLexer_UnterminatedStringLiteral(object p0, object p1)
		{
			return TextRes.GetString("ExpressionLexer_UnterminatedStringLiteral", new object[] { p0, p1 });
		}

		// Token: 0x060019D5 RID: 6613 RVA: 0x000561E0 File Offset: 0x000543E0
		internal static string ExpressionLexer_InvalidCharacter(object p0, object p1, object p2)
		{
			return TextRes.GetString("ExpressionLexer_InvalidCharacter", new object[] { p0, p1, p2 });
		}

		// Token: 0x060019D6 RID: 6614 RVA: 0x0005620C File Offset: 0x0005440C
		internal static string ExpressionLexer_SyntaxError(object p0, object p1)
		{
			return TextRes.GetString("ExpressionLexer_SyntaxError", new object[] { p0, p1 });
		}

		// Token: 0x060019D7 RID: 6615 RVA: 0x00056234 File Offset: 0x00054434
		internal static string ExpressionLexer_UnterminatedLiteral(object p0, object p1)
		{
			return TextRes.GetString("ExpressionLexer_UnterminatedLiteral", new object[] { p0, p1 });
		}

		// Token: 0x060019D8 RID: 6616 RVA: 0x0005625C File Offset: 0x0005445C
		internal static string ExpressionLexer_DigitExpected(object p0, object p1)
		{
			return TextRes.GetString("ExpressionLexer_DigitExpected", new object[] { p0, p1 });
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x060019D9 RID: 6617 RVA: 0x00056283 File Offset: 0x00054483
		internal static string ExpressionLexer_UnbalancedBracketExpression
		{
			get
			{
				return TextRes.GetString("ExpressionLexer_UnbalancedBracketExpression");
			}
		}

		// Token: 0x060019DA RID: 6618 RVA: 0x00056290 File Offset: 0x00054490
		internal static string UriQueryExpressionParser_UnrecognizedLiteral(object p0, object p1, object p2, object p3)
		{
			return TextRes.GetString("UriQueryExpressionParser_UnrecognizedLiteral", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x060019DB RID: 6619 RVA: 0x000562C0 File Offset: 0x000544C0
		internal static string JsonReader_UnexpectedComma(object p0)
		{
			return TextRes.GetString("JsonReader_UnexpectedComma", new object[] { p0 });
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x060019DC RID: 6620 RVA: 0x000562E3 File Offset: 0x000544E3
		internal static string JsonReader_MultipleTopLevelValues
		{
			get
			{
				return TextRes.GetString("JsonReader_MultipleTopLevelValues");
			}
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x060019DD RID: 6621 RVA: 0x000562EF File Offset: 0x000544EF
		internal static string JsonReader_EndOfInputWithOpenScope
		{
			get
			{
				return TextRes.GetString("JsonReader_EndOfInputWithOpenScope");
			}
		}

		// Token: 0x060019DE RID: 6622 RVA: 0x000562FC File Offset: 0x000544FC
		internal static string JsonReader_UnexpectedToken(object p0)
		{
			return TextRes.GetString("JsonReader_UnexpectedToken", new object[] { p0 });
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x060019DF RID: 6623 RVA: 0x0005631F File Offset: 0x0005451F
		internal static string JsonReader_UnrecognizedToken
		{
			get
			{
				return TextRes.GetString("JsonReader_UnrecognizedToken");
			}
		}

		// Token: 0x060019E0 RID: 6624 RVA: 0x0005632C File Offset: 0x0005452C
		internal static string JsonReader_MissingColon(object p0)
		{
			return TextRes.GetString("JsonReader_MissingColon", new object[] { p0 });
		}

		// Token: 0x060019E1 RID: 6625 RVA: 0x00056350 File Offset: 0x00054550
		internal static string JsonReader_UnrecognizedEscapeSequence(object p0)
		{
			return TextRes.GetString("JsonReader_UnrecognizedEscapeSequence", new object[] { p0 });
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x060019E2 RID: 6626 RVA: 0x00056373 File Offset: 0x00054573
		internal static string JsonReader_UnexpectedEndOfString
		{
			get
			{
				return TextRes.GetString("JsonReader_UnexpectedEndOfString");
			}
		}

		// Token: 0x060019E3 RID: 6627 RVA: 0x00056380 File Offset: 0x00054580
		internal static string JsonReader_InvalidNumberFormat(object p0)
		{
			return TextRes.GetString("JsonReader_InvalidNumberFormat", new object[] { p0 });
		}

		// Token: 0x060019E4 RID: 6628 RVA: 0x000563A4 File Offset: 0x000545A4
		internal static string JsonReader_MissingComma(object p0)
		{
			return TextRes.GetString("JsonReader_MissingComma", new object[] { p0 });
		}

		// Token: 0x060019E5 RID: 6629 RVA: 0x000563C8 File Offset: 0x000545C8
		internal static string JsonReader_InvalidPropertyNameOrUnexpectedComma(object p0)
		{
			return TextRes.GetString("JsonReader_InvalidPropertyNameOrUnexpectedComma", new object[] { p0 });
		}

		// Token: 0x060019E6 RID: 6630 RVA: 0x000563EC File Offset: 0x000545EC
		internal static string JsonReaderExtensions_UnexpectedNodeDetected(object p0, object p1)
		{
			return TextRes.GetString("JsonReaderExtensions_UnexpectedNodeDetected", new object[] { p0, p1 });
		}

		// Token: 0x060019E7 RID: 6631 RVA: 0x00056414 File Offset: 0x00054614
		internal static string JsonReaderExtensions_CannotReadPropertyValueAsString(object p0, object p1)
		{
			return TextRes.GetString("JsonReaderExtensions_CannotReadPropertyValueAsString", new object[] { p0, p1 });
		}

		// Token: 0x060019E8 RID: 6632 RVA: 0x0005643C File Offset: 0x0005463C
		internal static string JsonReaderExtensions_CannotReadValueAsString(object p0)
		{
			return TextRes.GetString("JsonReaderExtensions_CannotReadValueAsString", new object[] { p0 });
		}

		// Token: 0x060019E9 RID: 6633 RVA: 0x00056460 File Offset: 0x00054660
		internal static string JsonReaderExtensions_CannotReadValueAsDouble(object p0)
		{
			return TextRes.GetString("JsonReaderExtensions_CannotReadValueAsDouble", new object[] { p0 });
		}
	}
}
