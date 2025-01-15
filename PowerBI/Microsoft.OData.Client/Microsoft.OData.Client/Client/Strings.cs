using System;

namespace Microsoft.OData.Client
{
	// Token: 0x020000EE RID: 238
	internal static class Strings
	{
		// Token: 0x06000924 RID: 2340 RVA: 0x000246A1 File Offset: 0x000228A1
		internal static string Batch_ExpectedContentType(object p0)
		{
			return TextRes.GetString("Batch_ExpectedContentType", new object[] { p0 });
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x000246B7 File Offset: 0x000228B7
		internal static string Batch_ExpectedResponse(object p0)
		{
			return TextRes.GetString("Batch_ExpectedResponse", new object[] { p0 });
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000926 RID: 2342 RVA: 0x000246CD File Offset: 0x000228CD
		internal static string Batch_IncompleteResponseCount
		{
			get
			{
				return TextRes.GetString("Batch_IncompleteResponseCount");
			}
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x000246D9 File Offset: 0x000228D9
		internal static string Batch_UnexpectedContent(object p0)
		{
			return TextRes.GetString("Batch_UnexpectedContent", new object[] { p0 });
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000928 RID: 2344 RVA: 0x000246EF File Offset: 0x000228EF
		internal static string Context_BaseUri
		{
			get
			{
				return TextRes.GetString("Context_BaseUri");
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000929 RID: 2345 RVA: 0x000246FB File Offset: 0x000228FB
		internal static string Context_BaseUriRequired
		{
			get
			{
				return TextRes.GetString("Context_BaseUriRequired");
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x00024707 File Offset: 0x00022907
		internal static string Context_ResolveReturnedInvalidUri
		{
			get
			{
				return TextRes.GetString("Context_ResolveReturnedInvalidUri");
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x0600092B RID: 2347 RVA: 0x00024713 File Offset: 0x00022913
		internal static string Context_RequestUriIsRelativeBaseUriRequired
		{
			get
			{
				return TextRes.GetString("Context_RequestUriIsRelativeBaseUriRequired");
			}
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x0002471F File Offset: 0x0002291F
		internal static string Context_ResolveEntitySetOrBaseUriRequired(object p0)
		{
			return TextRes.GetString("Context_ResolveEntitySetOrBaseUriRequired", new object[] { p0 });
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x00024735 File Offset: 0x00022935
		internal static string Context_CannotConvertKey(object p0)
		{
			return TextRes.GetString("Context_CannotConvertKey", new object[] { p0 });
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x0600092E RID: 2350 RVA: 0x0002474B File Offset: 0x0002294B
		internal static string Context_TrackingExpectsAbsoluteUri
		{
			get
			{
				return TextRes.GetString("Context_TrackingExpectsAbsoluteUri");
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x0600092F RID: 2351 RVA: 0x00024757 File Offset: 0x00022957
		internal static string Context_LocationHeaderExpectsAbsoluteUri
		{
			get
			{
				return TextRes.GetString("Context_LocationHeaderExpectsAbsoluteUri");
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000930 RID: 2352 RVA: 0x00024763 File Offset: 0x00022963
		internal static string Context_LinkResourceInsertFailure
		{
			get
			{
				return TextRes.GetString("Context_LinkResourceInsertFailure");
			}
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x0002476F File Offset: 0x0002296F
		internal static string Context_InternalError(object p0)
		{
			return TextRes.GetString("Context_InternalError", new object[] { p0 });
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000932 RID: 2354 RVA: 0x00024785 File Offset: 0x00022985
		internal static string Context_BatchExecuteError
		{
			get
			{
				return TextRes.GetString("Context_BatchExecuteError");
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000933 RID: 2355 RVA: 0x00024791 File Offset: 0x00022991
		internal static string Context_EntitySetName
		{
			get
			{
				return TextRes.GetString("Context_EntitySetName");
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000934 RID: 2356 RVA: 0x0002479D File Offset: 0x0002299D
		internal static string Context_BatchNotSupportedForNamedStreams
		{
			get
			{
				return TextRes.GetString("Context_BatchNotSupportedForNamedStreams");
			}
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x000247A9 File Offset: 0x000229A9
		internal static string Context_SetSaveStreamWithoutNamedStreamEditLink(object p0)
		{
			return TextRes.GetString("Context_SetSaveStreamWithoutNamedStreamEditLink", new object[] { p0 });
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000936 RID: 2358 RVA: 0x000247BF File Offset: 0x000229BF
		internal static string Content_EntityWithoutKey
		{
			get
			{
				return TextRes.GetString("Content_EntityWithoutKey");
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000937 RID: 2359 RVA: 0x000247CB File Offset: 0x000229CB
		internal static string Content_EntityIsNotEntityType
		{
			get
			{
				return TextRes.GetString("Content_EntityIsNotEntityType");
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000938 RID: 2360 RVA: 0x000247D7 File Offset: 0x000229D7
		internal static string Context_EntityNotContained
		{
			get
			{
				return TextRes.GetString("Context_EntityNotContained");
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000939 RID: 2361 RVA: 0x000247E3 File Offset: 0x000229E3
		internal static string Context_EntityAlreadyContained
		{
			get
			{
				return TextRes.GetString("Context_EntityAlreadyContained");
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x0600093A RID: 2362 RVA: 0x000247EF File Offset: 0x000229EF
		internal static string Context_DifferentEntityAlreadyContained
		{
			get
			{
				return TextRes.GetString("Context_DifferentEntityAlreadyContained");
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x0600093B RID: 2363 RVA: 0x000247FB File Offset: 0x000229FB
		internal static string Context_DidNotOriginateAsync
		{
			get
			{
				return TextRes.GetString("Context_DidNotOriginateAsync");
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x0600093C RID: 2364 RVA: 0x00024807 File Offset: 0x00022A07
		internal static string Context_AsyncAlreadyDone
		{
			get
			{
				return TextRes.GetString("Context_AsyncAlreadyDone");
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x0600093D RID: 2365 RVA: 0x00024813 File Offset: 0x00022A13
		internal static string Context_OperationCanceled
		{
			get
			{
				return TextRes.GetString("Context_OperationCanceled");
			}
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x0002481F File Offset: 0x00022A1F
		internal static string Context_PropertyNotSupportedForMaxDataServiceVersionGreaterThanX(object p0, object p1)
		{
			return TextRes.GetString("Context_PropertyNotSupportedForMaxDataServiceVersionGreaterThanX", new object[] { p0, p1 });
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x0600093F RID: 2367 RVA: 0x00024839 File Offset: 0x00022A39
		internal static string Context_NoLoadWithInsertEnd
		{
			get
			{
				return TextRes.GetString("Context_NoLoadWithInsertEnd");
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000940 RID: 2368 RVA: 0x00024845 File Offset: 0x00022A45
		internal static string Context_NoRelationWithInsertEnd
		{
			get
			{
				return TextRes.GetString("Context_NoRelationWithInsertEnd");
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000941 RID: 2369 RVA: 0x00024851 File Offset: 0x00022A51
		internal static string Context_NoRelationWithDeleteEnd
		{
			get
			{
				return TextRes.GetString("Context_NoRelationWithDeleteEnd");
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000942 RID: 2370 RVA: 0x0002485D File Offset: 0x00022A5D
		internal static string Context_RelationAlreadyContained
		{
			get
			{
				return TextRes.GetString("Context_RelationAlreadyContained");
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000943 RID: 2371 RVA: 0x00024869 File Offset: 0x00022A69
		internal static string Context_RelationNotRefOrCollection
		{
			get
			{
				return TextRes.GetString("Context_RelationNotRefOrCollection");
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000944 RID: 2372 RVA: 0x00024875 File Offset: 0x00022A75
		internal static string Context_AddLinkCollectionOnly
		{
			get
			{
				return TextRes.GetString("Context_AddLinkCollectionOnly");
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000945 RID: 2373 RVA: 0x00024881 File Offset: 0x00022A81
		internal static string Context_AddRelatedObjectCollectionOnly
		{
			get
			{
				return TextRes.GetString("Context_AddRelatedObjectCollectionOnly");
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000946 RID: 2374 RVA: 0x0002488D File Offset: 0x00022A8D
		internal static string Context_AddRelatedObjectSourceDeleted
		{
			get
			{
				return TextRes.GetString("Context_AddRelatedObjectSourceDeleted");
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000947 RID: 2375 RVA: 0x00024899 File Offset: 0x00022A99
		internal static string Context_UpdateRelatedObjectNonCollectionOnly
		{
			get
			{
				return TextRes.GetString("Context_UpdateRelatedObjectNonCollectionOnly");
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000948 RID: 2376 RVA: 0x000248A5 File Offset: 0x00022AA5
		internal static string Context_SetLinkReferenceOnly
		{
			get
			{
				return TextRes.GetString("Context_SetLinkReferenceOnly");
			}
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x000248B1 File Offset: 0x00022AB1
		internal static string Context_NoContentTypeForMediaLink(object p0, object p1)
		{
			return TextRes.GetString("Context_NoContentTypeForMediaLink", new object[] { p0, p1 });
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x0600094A RID: 2378 RVA: 0x000248CB File Offset: 0x00022ACB
		internal static string Context_BatchNotSupportedForMediaLink
		{
			get
			{
				return TextRes.GetString("Context_BatchNotSupportedForMediaLink");
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x0600094B RID: 2379 RVA: 0x000248D7 File Offset: 0x00022AD7
		internal static string Context_UnexpectedZeroRawRead
		{
			get
			{
				return TextRes.GetString("Context_UnexpectedZeroRawRead");
			}
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x000248E3 File Offset: 0x00022AE3
		internal static string Context_VersionNotSupported(object p0, object p1)
		{
			return TextRes.GetString("Context_VersionNotSupported", new object[] { p0, p1 });
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x000248FD File Offset: 0x00022AFD
		internal static string Context_ResponseVersionIsBiggerThanProtocolVersion(object p0, object p1)
		{
			return TextRes.GetString("Context_ResponseVersionIsBiggerThanProtocolVersion", new object[] { p0, p1 });
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x00024917 File Offset: 0x00022B17
		internal static string Context_RequestVersionIsBiggerThanProtocolVersion(object p0, object p1)
		{
			return TextRes.GetString("Context_RequestVersionIsBiggerThanProtocolVersion", new object[] { p0, p1 });
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x0600094F RID: 2383 RVA: 0x00024931 File Offset: 0x00022B31
		internal static string Context_ChildResourceExists
		{
			get
			{
				return TextRes.GetString("Context_ChildResourceExists");
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000950 RID: 2384 RVA: 0x0002493D File Offset: 0x00022B3D
		internal static string Context_ContentTypeRequiredForNamedStream
		{
			get
			{
				return TextRes.GetString("Context_ContentTypeRequiredForNamedStream");
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000951 RID: 2385 RVA: 0x00024949 File Offset: 0x00022B49
		internal static string Context_EntityNotMediaLinkEntry
		{
			get
			{
				return TextRes.GetString("Context_EntityNotMediaLinkEntry");
			}
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x00024955 File Offset: 0x00022B55
		internal static string Context_MLEWithoutSaveStream(object p0)
		{
			return TextRes.GetString("Context_MLEWithoutSaveStream", new object[] { p0 });
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x0002496B File Offset: 0x00022B6B
		internal static string Context_SetSaveStreamOnMediaEntryProperty(object p0)
		{
			return TextRes.GetString("Context_SetSaveStreamOnMediaEntryProperty", new object[] { p0 });
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000954 RID: 2388 RVA: 0x00024981 File Offset: 0x00022B81
		internal static string Context_SetSaveStreamWithoutEditMediaLink
		{
			get
			{
				return TextRes.GetString("Context_SetSaveStreamWithoutEditMediaLink");
			}
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x0002498D File Offset: 0x00022B8D
		internal static string Context_SetSaveStreamOnInvalidEntityState(object p0)
		{
			return TextRes.GetString("Context_SetSaveStreamOnInvalidEntityState", new object[] { p0 });
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x000249A3 File Offset: 0x00022BA3
		internal static string Context_EntityDoesNotContainNamedStream(object p0)
		{
			return TextRes.GetString("Context_EntityDoesNotContainNamedStream", new object[] { p0 });
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x000249B9 File Offset: 0x00022BB9
		internal static string Context_MissingSelfAndEditLinkForNamedStream(object p0)
		{
			return TextRes.GetString("Context_MissingSelfAndEditLinkForNamedStream", new object[] { p0 });
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000958 RID: 2392 RVA: 0x000249CF File Offset: 0x00022BCF
		internal static string Context_BothLocationAndIdMustBeSpecified
		{
			get
			{
				return TextRes.GetString("Context_BothLocationAndIdMustBeSpecified");
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000959 RID: 2393 RVA: 0x000249DB File Offset: 0x00022BDB
		internal static string Context_BodyOperationParametersNotAllowedWithGet
		{
			get
			{
				return TextRes.GetString("Context_BodyOperationParametersNotAllowedWithGet");
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x0600095A RID: 2394 RVA: 0x000249E7 File Offset: 0x00022BE7
		internal static string Context_MissingOperationParameterName
		{
			get
			{
				return TextRes.GetString("Context_MissingOperationParameterName");
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x0600095B RID: 2395 RVA: 0x000249F3 File Offset: 0x00022BF3
		internal static string Context_DuplicateUriOperationParameterName
		{
			get
			{
				return TextRes.GetString("Context_DuplicateUriOperationParameterName");
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x0600095C RID: 2396 RVA: 0x000249FF File Offset: 0x00022BFF
		internal static string Context_DuplicateBodyOperationParameterName
		{
			get
			{
				return TextRes.GetString("Context_DuplicateBodyOperationParameterName");
			}
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x00024A0B File Offset: 0x00022C0B
		internal static string Context_NullKeysAreNotSupported(object p0)
		{
			return TextRes.GetString("Context_NullKeysAreNotSupported", new object[] { p0 });
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x0600095E RID: 2398 RVA: 0x00024A21 File Offset: 0x00022C21
		internal static string Context_ExecuteExpectsGetOrPostOrDelete
		{
			get
			{
				return TextRes.GetString("Context_ExecuteExpectsGetOrPostOrDelete");
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x0600095F RID: 2399 RVA: 0x00024A2D File Offset: 0x00022C2D
		internal static string Context_EndExecuteExpectedVoidResponse
		{
			get
			{
				return TextRes.GetString("Context_EndExecuteExpectedVoidResponse");
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000960 RID: 2400 RVA: 0x00024A39 File Offset: 0x00022C39
		internal static string Context_NullElementInOperationParameterArray
		{
			get
			{
				return TextRes.GetString("Context_NullElementInOperationParameterArray");
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000961 RID: 2401 RVA: 0x00024A45 File Offset: 0x00022C45
		internal static string Context_EntityMetadataBuilderIsRequired
		{
			get
			{
				return TextRes.GetString("Context_EntityMetadataBuilderIsRequired");
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000962 RID: 2402 RVA: 0x00024A51 File Offset: 0x00022C51
		internal static string Context_CannotChangeStateToAdded
		{
			get
			{
				return TextRes.GetString("Context_CannotChangeStateToAdded");
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000963 RID: 2403 RVA: 0x00024A5D File Offset: 0x00022C5D
		internal static string Context_CannotChangeStateToModifiedIfNotUnchanged
		{
			get
			{
				return TextRes.GetString("Context_CannotChangeStateToModifiedIfNotUnchanged");
			}
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x00024A69 File Offset: 0x00022C69
		internal static string Context_CannotChangeStateIfAdded(object p0)
		{
			return TextRes.GetString("Context_CannotChangeStateIfAdded", new object[] { p0 });
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000965 RID: 2405 RVA: 0x00024A7F File Offset: 0x00022C7F
		internal static string Context_OnMessageCreatingReturningNull
		{
			get
			{
				return TextRes.GetString("Context_OnMessageCreatingReturningNull");
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000966 RID: 2406 RVA: 0x00024A8B File Offset: 0x00022C8B
		internal static string Context_SendingRequest_InvalidWhenUsingOnMessageCreating
		{
			get
			{
				return TextRes.GetString("Context_SendingRequest_InvalidWhenUsingOnMessageCreating");
			}
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x00024A97 File Offset: 0x00022C97
		internal static string Context_MustBeUsedWith(object p0, object p1)
		{
			return TextRes.GetString("Context_MustBeUsedWith", new object[] { p0, p1 });
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000968 RID: 2408 RVA: 0x00024AB1 File Offset: 0x00022CB1
		internal static string DataServiceClientFormat_LoadServiceModelRequired
		{
			get
			{
				return TextRes.GetString("DataServiceClientFormat_LoadServiceModelRequired");
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000969 RID: 2409 RVA: 0x00024ABD File Offset: 0x00022CBD
		internal static string DataServiceClientFormat_ValidServiceModelRequiredForJson
		{
			get
			{
				return TextRes.GetString("DataServiceClientFormat_ValidServiceModelRequiredForJson");
			}
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x00024AC9 File Offset: 0x00022CC9
		internal static string Collection_NullCollectionReference(object p0, object p1)
		{
			return TextRes.GetString("Collection_NullCollectionReference", new object[] { p0, p1 });
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x00024AE3 File Offset: 0x00022CE3
		internal static string ClientType_MissingOpenProperty(object p0, object p1)
		{
			return TextRes.GetString("ClientType_MissingOpenProperty", new object[] { p0, p1 });
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x00024AFD File Offset: 0x00022CFD
		internal static string Clienttype_MultipleOpenProperty(object p0)
		{
			return TextRes.GetString("Clienttype_MultipleOpenProperty", new object[] { p0 });
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x00024B13 File Offset: 0x00022D13
		internal static string ClientType_MissingProperty(object p0, object p1)
		{
			return TextRes.GetString("ClientType_MissingProperty", new object[] { p0, p1 });
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x00024B2D File Offset: 0x00022D2D
		internal static string ClientType_KeysMustBeSimpleTypes(object p0, object p1, object p2)
		{
			return TextRes.GetString("ClientType_KeysMustBeSimpleTypes", new object[] { p0, p1, p2 });
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x00024B4B File Offset: 0x00022D4B
		internal static string ClientType_KeysOnDifferentDeclaredType(object p0)
		{
			return TextRes.GetString("ClientType_KeysOnDifferentDeclaredType", new object[] { p0 });
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x00024B61 File Offset: 0x00022D61
		internal static string ClientType_MissingMimeTypeProperty(object p0, object p1)
		{
			return TextRes.GetString("ClientType_MissingMimeTypeProperty", new object[] { p0, p1 });
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x00024B7B File Offset: 0x00022D7B
		internal static string ClientType_MissingMimeTypeDataProperty(object p0, object p1)
		{
			return TextRes.GetString("ClientType_MissingMimeTypeDataProperty", new object[] { p0, p1 });
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x00024B95 File Offset: 0x00022D95
		internal static string ClientType_MissingMediaEntryProperty(object p0, object p1)
		{
			return TextRes.GetString("ClientType_MissingMediaEntryProperty", new object[] { p0, p1 });
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x00024BAF File Offset: 0x00022DAF
		internal static string ClientType_NoSettableFields(object p0)
		{
			return TextRes.GetString("ClientType_NoSettableFields", new object[] { p0 });
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000974 RID: 2420 RVA: 0x00024BC5 File Offset: 0x00022DC5
		internal static string ClientType_MultipleImplementationNotSupported
		{
			get
			{
				return TextRes.GetString("ClientType_MultipleImplementationNotSupported");
			}
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x00024BD1 File Offset: 0x00022DD1
		internal static string ClientType_NullOpenProperties(object p0)
		{
			return TextRes.GetString("ClientType_NullOpenProperties", new object[] { p0 });
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x00024BE7 File Offset: 0x00022DE7
		internal static string ClientType_Ambiguous(object p0, object p1)
		{
			return TextRes.GetString("ClientType_Ambiguous", new object[] { p0, p1 });
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x00024C01 File Offset: 0x00022E01
		internal static string ClientType_UnsupportedType(object p0)
		{
			return TextRes.GetString("ClientType_UnsupportedType", new object[] { p0 });
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000978 RID: 2424 RVA: 0x00024C17 File Offset: 0x00022E17
		internal static string ClientType_CollectionOfCollectionNotSupported
		{
			get
			{
				return TextRes.GetString("ClientType_CollectionOfCollectionNotSupported");
			}
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x00024C23 File Offset: 0x00022E23
		internal static string ClientType_MultipleTypesWithSameName(object p0)
		{
			return TextRes.GetString("ClientType_MultipleTypesWithSameName", new object[] { p0 });
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x00024C39 File Offset: 0x00022E39
		internal static string WebUtil_TypeMismatchInCollection(object p0)
		{
			return TextRes.GetString("WebUtil_TypeMismatchInCollection", new object[] { p0 });
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x00024C4F File Offset: 0x00022E4F
		internal static string WebUtil_TypeMismatchInNonPropertyCollection(object p0)
		{
			return TextRes.GetString("WebUtil_TypeMismatchInNonPropertyCollection", new object[] { p0 });
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x00024C65 File Offset: 0x00022E65
		internal static string ClientTypeCache_NonEntityTypeCannotContainEntityProperties(object p0, object p1)
		{
			return TextRes.GetString("ClientTypeCache_NonEntityTypeCannotContainEntityProperties", new object[] { p0, p1 });
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x0600097D RID: 2429 RVA: 0x00024C7F File Offset: 0x00022E7F
		internal static string DataServiceException_GeneralError
		{
			get
			{
				return TextRes.GetString("DataServiceException_GeneralError");
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x0600097E RID: 2430 RVA: 0x00024C8B File Offset: 0x00022E8B
		internal static string Deserialize_GetEnumerator
		{
			get
			{
				return TextRes.GetString("Deserialize_GetEnumerator");
			}
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x00024C97 File Offset: 0x00022E97
		internal static string Deserialize_Current(object p0, object p1)
		{
			return TextRes.GetString("Deserialize_Current", new object[] { p0, p1 });
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000980 RID: 2432 RVA: 0x00024CB1 File Offset: 0x00022EB1
		internal static string Deserialize_MixedTextWithComment
		{
			get
			{
				return TextRes.GetString("Deserialize_MixedTextWithComment");
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000981 RID: 2433 RVA: 0x00024CBD File Offset: 0x00022EBD
		internal static string Deserialize_ExpectingSimpleValue
		{
			get
			{
				return TextRes.GetString("Deserialize_ExpectingSimpleValue");
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000982 RID: 2434 RVA: 0x00024CC9 File Offset: 0x00022EC9
		internal static string Deserialize_MismatchAtomLinkLocalSimple
		{
			get
			{
				return TextRes.GetString("Deserialize_MismatchAtomLinkLocalSimple");
			}
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x00024CD5 File Offset: 0x00022ED5
		internal static string Deserialize_MismatchAtomLinkFeedPropertyNotCollection(object p0)
		{
			return TextRes.GetString("Deserialize_MismatchAtomLinkFeedPropertyNotCollection", new object[] { p0 });
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x00024CEB File Offset: 0x00022EEB
		internal static string Deserialize_MismatchAtomLinkEntryPropertyIsCollection(object p0)
		{
			return TextRes.GetString("Deserialize_MismatchAtomLinkEntryPropertyIsCollection", new object[] { p0 });
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000985 RID: 2437 RVA: 0x00024D01 File Offset: 0x00022F01
		internal static string Deserialize_NoLocationHeader
		{
			get
			{
				return TextRes.GetString("Deserialize_NoLocationHeader");
			}
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x00024D0D File Offset: 0x00022F0D
		internal static string Deserialize_ServerException(object p0)
		{
			return TextRes.GetString("Deserialize_ServerException", new object[] { p0 });
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000987 RID: 2439 RVA: 0x00024D23 File Offset: 0x00022F23
		internal static string Deserialize_MissingIdElement
		{
			get
			{
				return TextRes.GetString("Deserialize_MissingIdElement");
			}
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x00024D2F File Offset: 0x00022F2F
		internal static string Collection_NullCollectionNotSupported(object p0)
		{
			return TextRes.GetString("Collection_NullCollectionNotSupported", new object[] { p0 });
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x00024D45 File Offset: 0x00022F45
		internal static string Collection_NullNonPropertyCollectionNotSupported(object p0)
		{
			return TextRes.GetString("Collection_NullNonPropertyCollectionNotSupported", new object[] { p0 });
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x0600098A RID: 2442 RVA: 0x00024D5B File Offset: 0x00022F5B
		internal static string Collection_NullCollectionItemsNotSupported
		{
			get
			{
				return TextRes.GetString("Collection_NullCollectionItemsNotSupported");
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x0600098B RID: 2443 RVA: 0x00024D67 File Offset: 0x00022F67
		internal static string Collection_CollectionTypesInCollectionOfPrimitiveTypesNotAllowed
		{
			get
			{
				return TextRes.GetString("Collection_CollectionTypesInCollectionOfPrimitiveTypesNotAllowed");
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x0600098C RID: 2444 RVA: 0x00024D73 File Offset: 0x00022F73
		internal static string Collection_PrimitiveTypesInCollectionOfComplexTypesNotAllowed
		{
			get
			{
				return TextRes.GetString("Collection_PrimitiveTypesInCollectionOfComplexTypesNotAllowed");
			}
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x00024D7F File Offset: 0x00022F7F
		internal static string EntityDescriptor_MissingSelfEditLink(object p0)
		{
			return TextRes.GetString("EntityDescriptor_MissingSelfEditLink", new object[] { p0 });
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x0600098E RID: 2446 RVA: 0x00024D95 File Offset: 0x00022F95
		internal static string HttpProcessUtility_ContentTypeMissing
		{
			get
			{
				return TextRes.GetString("HttpProcessUtility_ContentTypeMissing");
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x0600098F RID: 2447 RVA: 0x00024DA1 File Offset: 0x00022FA1
		internal static string HttpProcessUtility_MediaTypeMissingValue
		{
			get
			{
				return TextRes.GetString("HttpProcessUtility_MediaTypeMissingValue");
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000990 RID: 2448 RVA: 0x00024DAD File Offset: 0x00022FAD
		internal static string HttpProcessUtility_MediaTypeRequiresSemicolonBeforeParameter
		{
			get
			{
				return TextRes.GetString("HttpProcessUtility_MediaTypeRequiresSemicolonBeforeParameter");
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000991 RID: 2449 RVA: 0x00024DB9 File Offset: 0x00022FB9
		internal static string HttpProcessUtility_MediaTypeRequiresSlash
		{
			get
			{
				return TextRes.GetString("HttpProcessUtility_MediaTypeRequiresSlash");
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000992 RID: 2450 RVA: 0x00024DC5 File Offset: 0x00022FC5
		internal static string HttpProcessUtility_MediaTypeRequiresSubType
		{
			get
			{
				return TextRes.GetString("HttpProcessUtility_MediaTypeRequiresSubType");
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000993 RID: 2451 RVA: 0x00024DD1 File Offset: 0x00022FD1
		internal static string HttpProcessUtility_MediaTypeUnspecified
		{
			get
			{
				return TextRes.GetString("HttpProcessUtility_MediaTypeUnspecified");
			}
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x00024DDD File Offset: 0x00022FDD
		internal static string HttpProcessUtility_EncodingNotSupported(object p0)
		{
			return TextRes.GetString("HttpProcessUtility_EncodingNotSupported", new object[] { p0 });
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x00024DF3 File Offset: 0x00022FF3
		internal static string HttpProcessUtility_EscapeCharWithoutQuotes(object p0)
		{
			return TextRes.GetString("HttpProcessUtility_EscapeCharWithoutQuotes", new object[] { p0 });
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x00024E09 File Offset: 0x00023009
		internal static string HttpProcessUtility_EscapeCharAtEnd(object p0)
		{
			return TextRes.GetString("HttpProcessUtility_EscapeCharAtEnd", new object[] { p0 });
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x00024E1F File Offset: 0x0002301F
		internal static string HttpProcessUtility_ClosingQuoteNotFound(object p0)
		{
			return TextRes.GetString("HttpProcessUtility_ClosingQuoteNotFound", new object[] { p0 });
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000998 RID: 2456 RVA: 0x00024E35 File Offset: 0x00023035
		internal static string MaterializeFromAtom_CountNotPresent
		{
			get
			{
				return TextRes.GetString("MaterializeFromAtom_CountNotPresent");
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000999 RID: 2457 RVA: 0x00024E41 File Offset: 0x00023041
		internal static string MaterializeFromAtom_TopLevelLinkNotAvailable
		{
			get
			{
				return TextRes.GetString("MaterializeFromAtom_TopLevelLinkNotAvailable");
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x0600099A RID: 2458 RVA: 0x00024E4D File Offset: 0x0002304D
		internal static string MaterializeFromAtom_CollectionKeyNotPresentInLinkTable
		{
			get
			{
				return TextRes.GetString("MaterializeFromAtom_CollectionKeyNotPresentInLinkTable");
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x0600099B RID: 2459 RVA: 0x00024E59 File Offset: 0x00023059
		internal static string MaterializeFromAtom_GetNestLinkForFlatCollection
		{
			get
			{
				return TextRes.GetString("MaterializeFromAtom_GetNestLinkForFlatCollection");
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x0600099C RID: 2460 RVA: 0x00024E65 File Offset: 0x00023065
		internal static string ODataRequestMessage_GetStreamMethodNotSupported
		{
			get
			{
				return TextRes.GetString("ODataRequestMessage_GetStreamMethodNotSupported");
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x0600099D RID: 2461 RVA: 0x00024E71 File Offset: 0x00023071
		internal static string Util_EmptyString
		{
			get
			{
				return TextRes.GetString("Util_EmptyString");
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x0600099E RID: 2462 RVA: 0x00024E7D File Offset: 0x0002307D
		internal static string Util_EmptyArray
		{
			get
			{
				return TextRes.GetString("Util_EmptyArray");
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x0600099F RID: 2463 RVA: 0x00024E89 File Offset: 0x00023089
		internal static string Util_NullArrayElement
		{
			get
			{
				return TextRes.GetString("Util_NullArrayElement");
			}
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x00024E95 File Offset: 0x00023095
		internal static string ALinq_UnsupportedExpression(object p0)
		{
			return TextRes.GetString("ALinq_UnsupportedExpression", new object[] { p0 });
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x00024EAB File Offset: 0x000230AB
		internal static string ALinq_CouldNotConvert(object p0)
		{
			return TextRes.GetString("ALinq_CouldNotConvert", new object[] { p0 });
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x00024EC1 File Offset: 0x000230C1
		internal static string ALinq_MethodNotSupported(object p0)
		{
			return TextRes.GetString("ALinq_MethodNotSupported", new object[] { p0 });
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x00024ED7 File Offset: 0x000230D7
		internal static string ALinq_UnaryNotSupported(object p0)
		{
			return TextRes.GetString("ALinq_UnaryNotSupported", new object[] { p0 });
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x00024EED File Offset: 0x000230ED
		internal static string ALinq_BinaryNotSupported(object p0)
		{
			return TextRes.GetString("ALinq_BinaryNotSupported", new object[] { p0 });
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x00024F03 File Offset: 0x00023103
		internal static string ALinq_ConstantNotSupported(object p0)
		{
			return TextRes.GetString("ALinq_ConstantNotSupported", new object[] { p0 });
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060009A6 RID: 2470 RVA: 0x00024F19 File Offset: 0x00023119
		internal static string ALinq_TypeBinaryNotSupported
		{
			get
			{
				return TextRes.GetString("ALinq_TypeBinaryNotSupported");
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060009A7 RID: 2471 RVA: 0x00024F25 File Offset: 0x00023125
		internal static string ALinq_ConditionalNotSupported
		{
			get
			{
				return TextRes.GetString("ALinq_ConditionalNotSupported");
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060009A8 RID: 2472 RVA: 0x00024F31 File Offset: 0x00023131
		internal static string ALinq_ParameterNotSupported
		{
			get
			{
				return TextRes.GetString("ALinq_ParameterNotSupported");
			}
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x00024F3D File Offset: 0x0002313D
		internal static string ALinq_MemberAccessNotSupported(object p0)
		{
			return TextRes.GetString("ALinq_MemberAccessNotSupported", new object[] { p0 });
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060009AA RID: 2474 RVA: 0x00024F53 File Offset: 0x00023153
		internal static string ALinq_LambdaNotSupported
		{
			get
			{
				return TextRes.GetString("ALinq_LambdaNotSupported");
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060009AB RID: 2475 RVA: 0x00024F5F File Offset: 0x0002315F
		internal static string ALinq_NewNotSupported
		{
			get
			{
				return TextRes.GetString("ALinq_NewNotSupported");
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060009AC RID: 2476 RVA: 0x00024F6B File Offset: 0x0002316B
		internal static string ALinq_MemberInitNotSupported
		{
			get
			{
				return TextRes.GetString("ALinq_MemberInitNotSupported");
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060009AD RID: 2477 RVA: 0x00024F77 File Offset: 0x00023177
		internal static string ALinq_ListInitNotSupported
		{
			get
			{
				return TextRes.GetString("ALinq_ListInitNotSupported");
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060009AE RID: 2478 RVA: 0x00024F83 File Offset: 0x00023183
		internal static string ALinq_NewArrayNotSupported
		{
			get
			{
				return TextRes.GetString("ALinq_NewArrayNotSupported");
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060009AF RID: 2479 RVA: 0x00024F8F File Offset: 0x0002318F
		internal static string ALinq_InvocationNotSupported
		{
			get
			{
				return TextRes.GetString("ALinq_InvocationNotSupported");
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060009B0 RID: 2480 RVA: 0x00024F9B File Offset: 0x0002319B
		internal static string ALinq_QueryOptionsOnlyAllowedOnLeafNodes
		{
			get
			{
				return TextRes.GetString("ALinq_QueryOptionsOnlyAllowedOnLeafNodes");
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060009B1 RID: 2481 RVA: 0x00024FA7 File Offset: 0x000231A7
		internal static string ALinq_CantExpand
		{
			get
			{
				return TextRes.GetString("ALinq_CantExpand");
			}
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x00024FB3 File Offset: 0x000231B3
		internal static string ALinq_CantCastToUnsupportedPrimitive(object p0)
		{
			return TextRes.GetString("ALinq_CantCastToUnsupportedPrimitive", new object[] { p0 });
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060009B3 RID: 2483 RVA: 0x00024FC9 File Offset: 0x000231C9
		internal static string ALinq_CantNavigateWithoutKeyPredicate
		{
			get
			{
				return TextRes.GetString("ALinq_CantNavigateWithoutKeyPredicate");
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060009B4 RID: 2484 RVA: 0x00024FD5 File Offset: 0x000231D5
		internal static string ALinq_CanOnlyApplyOneKeyPredicate
		{
			get
			{
				return TextRes.GetString("ALinq_CanOnlyApplyOneKeyPredicate");
			}
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x00024FE1 File Offset: 0x000231E1
		internal static string ALinq_CantTranslateExpression(object p0)
		{
			return TextRes.GetString("ALinq_CantTranslateExpression", new object[] { p0 });
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x00024FF7 File Offset: 0x000231F7
		internal static string ALinq_TranslationError(object p0)
		{
			return TextRes.GetString("ALinq_TranslationError", new object[] { p0 });
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060009B7 RID: 2487 RVA: 0x0002500D File Offset: 0x0002320D
		internal static string ALinq_CantAddQueryOption
		{
			get
			{
				return TextRes.GetString("ALinq_CantAddQueryOption");
			}
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x00025019 File Offset: 0x00023219
		internal static string ALinq_CantAddDuplicateQueryOption(object p0)
		{
			return TextRes.GetString("ALinq_CantAddDuplicateQueryOption", new object[] { p0 });
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x0002502F File Offset: 0x0002322F
		internal static string ALinq_CantAddAstoriaQueryOption(object p0)
		{
			return TextRes.GetString("ALinq_CantAddAstoriaQueryOption", new object[] { p0 });
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x00025045 File Offset: 0x00023245
		internal static string ALinq_CantAddQueryOptionStartingWithDollarSign(object p0)
		{
			return TextRes.GetString("ALinq_CantAddQueryOptionStartingWithDollarSign", new object[] { p0 });
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x0002505B File Offset: 0x0002325B
		internal static string ALinq_CantReferToPublicField(object p0)
		{
			return TextRes.GetString("ALinq_CantReferToPublicField", new object[] { p0 });
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060009BC RID: 2492 RVA: 0x00025071 File Offset: 0x00023271
		internal static string ALinq_QueryOptionsOnlyAllowedOnSingletons
		{
			get
			{
				return TextRes.GetString("ALinq_QueryOptionsOnlyAllowedOnSingletons");
			}
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x0002507D File Offset: 0x0002327D
		internal static string ALinq_QueryOptionOutOfOrder(object p0, object p1)
		{
			return TextRes.GetString("ALinq_QueryOptionOutOfOrder", new object[] { p0, p1 });
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060009BE RID: 2494 RVA: 0x00025097 File Offset: 0x00023297
		internal static string ALinq_CannotAddCountOption
		{
			get
			{
				return TextRes.GetString("ALinq_CannotAddCountOption");
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060009BF RID: 2495 RVA: 0x000250A3 File Offset: 0x000232A3
		internal static string ALinq_CannotAddCountOptionConflict
		{
			get
			{
				return TextRes.GetString("ALinq_CannotAddCountOptionConflict");
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060009C0 RID: 2496 RVA: 0x000250AF File Offset: 0x000232AF
		internal static string ALinq_ProjectionOnlyAllowedOnLeafNodes
		{
			get
			{
				return TextRes.GetString("ALinq_ProjectionOnlyAllowedOnLeafNodes");
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060009C1 RID: 2497 RVA: 0x000250BB File Offset: 0x000232BB
		internal static string ALinq_ProjectionCanOnlyHaveOneProjection
		{
			get
			{
				return TextRes.GetString("ALinq_ProjectionCanOnlyHaveOneProjection");
			}
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x000250C7 File Offset: 0x000232C7
		internal static string ALinq_ProjectionMemberAssignmentMismatch(object p0, object p1, object p2)
		{
			return TextRes.GetString("ALinq_ProjectionMemberAssignmentMismatch", new object[] { p0, p1, p2 });
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x000250E5 File Offset: 0x000232E5
		internal static string ALinq_InvalidExpressionInNavigationPath(object p0)
		{
			return TextRes.GetString("ALinq_InvalidExpressionInNavigationPath", new object[] { p0 });
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x000250FB File Offset: 0x000232FB
		internal static string ALinq_ExpressionNotSupportedInProjectionToEntity(object p0, object p1)
		{
			return TextRes.GetString("ALinq_ExpressionNotSupportedInProjectionToEntity", new object[] { p0, p1 });
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x00025115 File Offset: 0x00023315
		internal static string ALinq_ExpressionNotSupportedInProjection(object p0, object p1)
		{
			return TextRes.GetString("ALinq_ExpressionNotSupportedInProjection", new object[] { p0, p1 });
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x060009C6 RID: 2502 RVA: 0x0002512F File Offset: 0x0002332F
		internal static string ALinq_CannotConstructKnownEntityTypes
		{
			get
			{
				return TextRes.GetString("ALinq_CannotConstructKnownEntityTypes");
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x060009C7 RID: 2503 RVA: 0x0002513B File Offset: 0x0002333B
		internal static string ALinq_CannotCreateConstantEntity
		{
			get
			{
				return TextRes.GetString("ALinq_CannotCreateConstantEntity");
			}
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x00025147 File Offset: 0x00023347
		internal static string ALinq_PropertyNamesMustMatchInProjections(object p0, object p1)
		{
			return TextRes.GetString("ALinq_PropertyNamesMustMatchInProjections", new object[] { p0, p1 });
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x060009C9 RID: 2505 RVA: 0x00025161 File Offset: 0x00023361
		internal static string ALinq_CanOnlyProjectTheLeaf
		{
			get
			{
				return TextRes.GetString("ALinq_CanOnlyProjectTheLeaf");
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x060009CA RID: 2506 RVA: 0x0002516D File Offset: 0x0002336D
		internal static string ALinq_CannotProjectWithExplicitExpansion
		{
			get
			{
				return TextRes.GetString("ALinq_CannotProjectWithExplicitExpansion");
			}
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x00025179 File Offset: 0x00023379
		internal static string ALinq_CollectionPropertyNotSupportedInOrderBy(object p0)
		{
			return TextRes.GetString("ALinq_CollectionPropertyNotSupportedInOrderBy", new object[] { p0 });
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x0002518F File Offset: 0x0002338F
		internal static string ALinq_CollectionPropertyNotSupportedInWhere(object p0)
		{
			return TextRes.GetString("ALinq_CollectionPropertyNotSupportedInWhere", new object[] { p0 });
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x000251A5 File Offset: 0x000233A5
		internal static string ALinq_CollectionMemberAccessNotSupportedInNavigation(object p0)
		{
			return TextRes.GetString("ALinq_CollectionMemberAccessNotSupportedInNavigation", new object[] { p0 });
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x000251BB File Offset: 0x000233BB
		internal static string ALinq_LinkPropertyNotSupportedInExpression(object p0)
		{
			return TextRes.GetString("ALinq_LinkPropertyNotSupportedInExpression", new object[] { p0 });
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x060009CF RID: 2511 RVA: 0x000251D1 File Offset: 0x000233D1
		internal static string ALinq_OfTypeArgumentNotAvailable
		{
			get
			{
				return TextRes.GetString("ALinq_OfTypeArgumentNotAvailable");
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x060009D0 RID: 2512 RVA: 0x000251DD File Offset: 0x000233DD
		internal static string ALinq_CannotUseTypeFiltersMultipleTimes
		{
			get
			{
				return TextRes.GetString("ALinq_CannotUseTypeFiltersMultipleTimes");
			}
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x000251E9 File Offset: 0x000233E9
		internal static string ALinq_ExpressionCannotEndWithTypeAs(object p0, object p1)
		{
			return TextRes.GetString("ALinq_ExpressionCannotEndWithTypeAs", new object[] { p0, p1 });
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x060009D2 RID: 2514 RVA: 0x00025203 File Offset: 0x00023403
		internal static string ALinq_TypeAsNotSupportedForMaxDataServiceVersionLessThan3
		{
			get
			{
				return TextRes.GetString("ALinq_TypeAsNotSupportedForMaxDataServiceVersionLessThan3");
			}
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x0002520F File Offset: 0x0002340F
		internal static string ALinq_TypeAsArgumentNotEntityType(object p0)
		{
			return TextRes.GetString("ALinq_TypeAsArgumentNotEntityType", new object[] { p0 });
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x00025225 File Offset: 0x00023425
		internal static string ALinq_InvalidSourceForAnyAll(object p0)
		{
			return TextRes.GetString("ALinq_InvalidSourceForAnyAll", new object[] { p0 });
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x0002523B File Offset: 0x0002343B
		internal static string ALinq_AnyAllNotSupportedInOrderBy(object p0)
		{
			return TextRes.GetString("ALinq_AnyAllNotSupportedInOrderBy", new object[] { p0 });
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x060009D6 RID: 2518 RVA: 0x00025251 File Offset: 0x00023451
		internal static string ALinq_FormatQueryOptionNotSupported
		{
			get
			{
				return TextRes.GetString("ALinq_FormatQueryOptionNotSupported");
			}
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x0002525D File Offset: 0x0002345D
		internal static string ALinq_IllegalSystemQueryOption(object p0)
		{
			return TextRes.GetString("ALinq_IllegalSystemQueryOption", new object[] { p0 });
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x00025273 File Offset: 0x00023473
		internal static string ALinq_IllegalPathStructure(object p0)
		{
			return TextRes.GetString("ALinq_IllegalPathStructure", new object[] { p0 });
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x00025289 File Offset: 0x00023489
		internal static string ALinq_TypeTokenWithNoTrailingNavProp(object p0)
		{
			return TextRes.GetString("ALinq_TypeTokenWithNoTrailingNavProp", new object[] { p0 });
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x060009DA RID: 2522 RVA: 0x0002529F File Offset: 0x0002349F
		internal static string DSKAttribute_MustSpecifyAtleastOnePropertyName
		{
			get
			{
				return TextRes.GetString("DSKAttribute_MustSpecifyAtleastOnePropertyName");
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x060009DB RID: 2523 RVA: 0x000252AB File Offset: 0x000234AB
		internal static string DataServiceCollection_LoadRequiresTargetCollectionObserved
		{
			get
			{
				return TextRes.GetString("DataServiceCollection_LoadRequiresTargetCollectionObserved");
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x060009DC RID: 2524 RVA: 0x000252B7 File Offset: 0x000234B7
		internal static string DataServiceCollection_CannotStopTrackingChildCollection
		{
			get
			{
				return TextRes.GetString("DataServiceCollection_CannotStopTrackingChildCollection");
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x060009DD RID: 2525 RVA: 0x000252C3 File Offset: 0x000234C3
		internal static string DataServiceCollection_OperationForTrackedOnly
		{
			get
			{
				return TextRes.GetString("DataServiceCollection_OperationForTrackedOnly");
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x060009DE RID: 2526 RVA: 0x000252CF File Offset: 0x000234CF
		internal static string DataServiceCollection_CannotDetermineContextFromItems
		{
			get
			{
				return TextRes.GetString("DataServiceCollection_CannotDetermineContextFromItems");
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x060009DF RID: 2527 RVA: 0x000252DB File Offset: 0x000234DB
		internal static string DataServiceCollection_InsertIntoTrackedButNotLoadedCollection
		{
			get
			{
				return TextRes.GetString("DataServiceCollection_InsertIntoTrackedButNotLoadedCollection");
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x060009E0 RID: 2528 RVA: 0x000252E7 File Offset: 0x000234E7
		internal static string DataServiceCollection_MultipleLoadAsyncOperationsAtTheSameTime
		{
			get
			{
				return TextRes.GetString("DataServiceCollection_MultipleLoadAsyncOperationsAtTheSameTime");
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x060009E1 RID: 2529 RVA: 0x000252F3 File Offset: 0x000234F3
		internal static string DataServiceCollection_LoadAsyncNoParamsWithoutParentEntity
		{
			get
			{
				return TextRes.GetString("DataServiceCollection_LoadAsyncNoParamsWithoutParentEntity");
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x060009E2 RID: 2530 RVA: 0x000252FF File Offset: 0x000234FF
		internal static string DataServiceCollection_LoadAsyncRequiresDataServiceQuery
		{
			get
			{
				return TextRes.GetString("DataServiceCollection_LoadAsyncRequiresDataServiceQuery");
			}
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x0002530B File Offset: 0x0002350B
		internal static string DataBinding_DataServiceCollectionArgumentMustHaveEntityType(object p0)
		{
			return TextRes.GetString("DataBinding_DataServiceCollectionArgumentMustHaveEntityType", new object[] { p0 });
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x00025321 File Offset: 0x00023521
		internal static string DataBinding_CollectionPropertySetterValueHasObserver(object p0, object p1)
		{
			return TextRes.GetString("DataBinding_CollectionPropertySetterValueHasObserver", new object[] { p0, p1 });
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x0002533B File Offset: 0x0002353B
		internal static string DataBinding_DataServiceCollectionChangedUnknownActionCollection(object p0)
		{
			return TextRes.GetString("DataBinding_DataServiceCollectionChangedUnknownActionCollection", new object[] { p0 });
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x00025351 File Offset: 0x00023551
		internal static string DataBinding_CollectionChangedUnknownActionCollection(object p0, object p1)
		{
			return TextRes.GetString("DataBinding_CollectionChangedUnknownActionCollection", new object[] { p0, p1 });
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x060009E7 RID: 2535 RVA: 0x0002536B File Offset: 0x0002356B
		internal static string DataBinding_BindingOperation_DetachedSource
		{
			get
			{
				return TextRes.GetString("DataBinding_BindingOperation_DetachedSource");
			}
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x00025377 File Offset: 0x00023577
		internal static string DataBinding_BindingOperation_ArrayItemNull(object p0)
		{
			return TextRes.GetString("DataBinding_BindingOperation_ArrayItemNull", new object[] { p0 });
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x0002538D File Offset: 0x0002358D
		internal static string DataBinding_BindingOperation_ArrayItemNotEntity(object p0)
		{
			return TextRes.GetString("DataBinding_BindingOperation_ArrayItemNotEntity", new object[] { p0 });
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x000253A3 File Offset: 0x000235A3
		internal static string DataBinding_Util_UnknownEntitySetName(object p0)
		{
			return TextRes.GetString("DataBinding_Util_UnknownEntitySetName", new object[] { p0 });
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x000253B9 File Offset: 0x000235B9
		internal static string DataBinding_EntityAlreadyInCollection(object p0)
		{
			return TextRes.GetString("DataBinding_EntityAlreadyInCollection", new object[] { p0 });
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x000253CF File Offset: 0x000235CF
		internal static string DataBinding_NotifyPropertyChangedNotImpl(object p0)
		{
			return TextRes.GetString("DataBinding_NotifyPropertyChangedNotImpl", new object[] { p0 });
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x000253E5 File Offset: 0x000235E5
		internal static string DataBinding_NotifyCollectionChangedNotImpl(object p0)
		{
			return TextRes.GetString("DataBinding_NotifyCollectionChangedNotImpl", new object[] { p0 });
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x000253FB File Offset: 0x000235FB
		internal static string DataBinding_ComplexObjectAssociatedWithMultipleEntities(object p0)
		{
			return TextRes.GetString("DataBinding_ComplexObjectAssociatedWithMultipleEntities", new object[] { p0 });
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x00025411 File Offset: 0x00023611
		internal static string DataBinding_CollectionAssociatedWithMultipleEntities(object p0)
		{
			return TextRes.GetString("DataBinding_CollectionAssociatedWithMultipleEntities", new object[] { p0 });
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x060009F0 RID: 2544 RVA: 0x00025427 File Offset: 0x00023627
		internal static string AtomParser_SingleEntry_NoneFound
		{
			get
			{
				return TextRes.GetString("AtomParser_SingleEntry_NoneFound");
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x060009F1 RID: 2545 RVA: 0x00025433 File Offset: 0x00023633
		internal static string AtomParser_SingleEntry_MultipleFound
		{
			get
			{
				return TextRes.GetString("AtomParser_SingleEntry_MultipleFound");
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x060009F2 RID: 2546 RVA: 0x0002543F File Offset: 0x0002363F
		internal static string AtomParser_SingleEntry_ExpectedFeedOrEntry
		{
			get
			{
				return TextRes.GetString("AtomParser_SingleEntry_ExpectedFeedOrEntry");
			}
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x0002544B File Offset: 0x0002364B
		internal static string AtomMaterializer_CannotAssignNull(object p0, object p1)
		{
			return TextRes.GetString("AtomMaterializer_CannotAssignNull", new object[] { p0, p1 });
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x00025465 File Offset: 0x00023665
		internal static string AtomMaterializer_EntryIntoCollectionMismatch(object p0, object p1)
		{
			return TextRes.GetString("AtomMaterializer_EntryIntoCollectionMismatch", new object[] { p0, p1 });
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x0002547F File Offset: 0x0002367F
		internal static string AtomMaterializer_EntryToAccessIsNull(object p0)
		{
			return TextRes.GetString("AtomMaterializer_EntryToAccessIsNull", new object[] { p0 });
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x00025495 File Offset: 0x00023695
		internal static string AtomMaterializer_EntryToInitializeIsNull(object p0)
		{
			return TextRes.GetString("AtomMaterializer_EntryToInitializeIsNull", new object[] { p0 });
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x000254AB File Offset: 0x000236AB
		internal static string AtomMaterializer_ProjectEntityTypeMismatch(object p0, object p1, object p2)
		{
			return TextRes.GetString("AtomMaterializer_ProjectEntityTypeMismatch", new object[] { p0, p1, p2 });
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x000254C9 File Offset: 0x000236C9
		internal static string AtomMaterializer_PropertyMissing(object p0)
		{
			return TextRes.GetString("AtomMaterializer_PropertyMissing", new object[] { p0 });
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x000254DF File Offset: 0x000236DF
		internal static string AtomMaterializer_PropertyNotExpectedEntry(object p0)
		{
			return TextRes.GetString("AtomMaterializer_PropertyNotExpectedEntry", new object[] { p0 });
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x060009FA RID: 2554 RVA: 0x000254F5 File Offset: 0x000236F5
		internal static string AtomMaterializer_DataServiceCollectionNotSupportedForNonEntities
		{
			get
			{
				return TextRes.GetString("AtomMaterializer_DataServiceCollectionNotSupportedForNonEntities");
			}
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x00025501 File Offset: 0x00023701
		internal static string AtomMaterializer_NoParameterlessCtorForCollectionProperty(object p0, object p1)
		{
			return TextRes.GetString("AtomMaterializer_NoParameterlessCtorForCollectionProperty", new object[] { p0, p1 });
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x0002551B File Offset: 0x0002371B
		internal static string AtomMaterializer_InvalidCollectionItem(object p0)
		{
			return TextRes.GetString("AtomMaterializer_InvalidCollectionItem", new object[] { p0 });
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x00025531 File Offset: 0x00023731
		internal static string AtomMaterializer_InvalidEntityType(object p0)
		{
			return TextRes.GetString("AtomMaterializer_InvalidEntityType", new object[] { p0 });
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x00025547 File Offset: 0x00023747
		internal static string AtomMaterializer_InvalidNonEntityType(object p0)
		{
			return TextRes.GetString("AtomMaterializer_InvalidNonEntityType", new object[] { p0 });
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x0002555D File Offset: 0x0002375D
		internal static string AtomMaterializer_CollectionExpectedCollection(object p0)
		{
			return TextRes.GetString("AtomMaterializer_CollectionExpectedCollection", new object[] { p0 });
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x00025573 File Offset: 0x00023773
		internal static string AtomMaterializer_InvalidResponsePayload(object p0)
		{
			return TextRes.GetString("AtomMaterializer_InvalidResponsePayload", new object[] { p0 });
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x00025589 File Offset: 0x00023789
		internal static string AtomMaterializer_InvalidContentTypeEncountered(object p0)
		{
			return TextRes.GetString("AtomMaterializer_InvalidContentTypeEncountered", new object[] { p0 });
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x0002559F File Offset: 0x0002379F
		internal static string AtomMaterializer_MaterializationTypeError(object p0)
		{
			return TextRes.GetString("AtomMaterializer_MaterializationTypeError", new object[] { p0 });
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000A03 RID: 2563 RVA: 0x000255B5 File Offset: 0x000237B5
		internal static string AtomMaterializer_ResetAfterEnumeratorCreationError
		{
			get
			{
				return TextRes.GetString("AtomMaterializer_ResetAfterEnumeratorCreationError");
			}
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x000255C1 File Offset: 0x000237C1
		internal static string AtomMaterializer_TypeShouldBeCollectionError(object p0)
		{
			return TextRes.GetString("AtomMaterializer_TypeShouldBeCollectionError", new object[] { p0 });
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x000255D7 File Offset: 0x000237D7
		internal static string Serializer_LoopsNotAllowedInComplexTypes(object p0)
		{
			return TextRes.GetString("Serializer_LoopsNotAllowedInComplexTypes", new object[] { p0 });
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x000255ED File Offset: 0x000237ED
		internal static string Serializer_LoopsNotAllowedInNonPropertyComplexTypes(object p0)
		{
			return TextRes.GetString("Serializer_LoopsNotAllowedInNonPropertyComplexTypes", new object[] { p0 });
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x00025603 File Offset: 0x00023803
		internal static string Serializer_InvalidCollectionParamterItemType(object p0, object p1)
		{
			return TextRes.GetString("Serializer_InvalidCollectionParamterItemType", new object[] { p0, p1 });
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x0002561D File Offset: 0x0002381D
		internal static string Serializer_NullCollectionParamterItemValue(object p0)
		{
			return TextRes.GetString("Serializer_NullCollectionParamterItemValue", new object[] { p0 });
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x00025633 File Offset: 0x00023833
		internal static string Serializer_InvalidParameterType(object p0, object p1)
		{
			return TextRes.GetString("Serializer_InvalidParameterType", new object[] { p0, p1 });
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x0002564D File Offset: 0x0002384D
		internal static string Serializer_UriDoesNotContainParameterAlias(object p0)
		{
			return TextRes.GetString("Serializer_UriDoesNotContainParameterAlias", new object[] { p0 });
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x00025663 File Offset: 0x00023863
		internal static string Serializer_InvalidEnumMemberValue(object p0, object p1)
		{
			return TextRes.GetString("Serializer_InvalidEnumMemberValue", new object[] { p0, p1 });
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000A0C RID: 2572 RVA: 0x0002567D File Offset: 0x0002387D
		internal static string DataServiceQuery_EnumerationNotSupported
		{
			get
			{
				return TextRes.GetString("DataServiceQuery_EnumerationNotSupported");
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000A0D RID: 2573 RVA: 0x00025689 File Offset: 0x00023889
		internal static string Context_SendingRequestEventArgsNotHttp
		{
			get
			{
				return TextRes.GetString("Context_SendingRequestEventArgsNotHttp");
			}
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x00025695 File Offset: 0x00023895
		internal static string General_InternalError(object p0)
		{
			return TextRes.GetString("General_InternalError", new object[] { p0 });
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x000256AB File Offset: 0x000238AB
		internal static string ODataMetadataBuilder_MissingEntitySetUri(object p0)
		{
			return TextRes.GetString("ODataMetadataBuilder_MissingEntitySetUri", new object[] { p0 });
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x000256C1 File Offset: 0x000238C1
		internal static string ODataMetadataBuilder_MissingSegmentForEntitySetUriSuffix(object p0, object p1)
		{
			return TextRes.GetString("ODataMetadataBuilder_MissingSegmentForEntitySetUriSuffix", new object[] { p0, p1 });
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x000256DB File Offset: 0x000238DB
		internal static string ODataMetadataBuilder_MissingEntityInstanceUri(object p0)
		{
			return TextRes.GetString("ODataMetadataBuilder_MissingEntityInstanceUri", new object[] { p0 });
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x000256F1 File Offset: 0x000238F1
		internal static string EdmValueUtils_UnsupportedPrimitiveType(object p0)
		{
			return TextRes.GetString("EdmValueUtils_UnsupportedPrimitiveType", new object[] { p0 });
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x00025707 File Offset: 0x00023907
		internal static string EdmValueUtils_IncorrectPrimitiveTypeKind(object p0, object p1, object p2)
		{
			return TextRes.GetString("EdmValueUtils_IncorrectPrimitiveTypeKind", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x00025725 File Offset: 0x00023925
		internal static string EdmValueUtils_IncorrectPrimitiveTypeKindNoTypeName(object p0, object p1)
		{
			return TextRes.GetString("EdmValueUtils_IncorrectPrimitiveTypeKindNoTypeName", new object[] { p0, p1 });
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x0002573F File Offset: 0x0002393F
		internal static string EdmValueUtils_CannotConvertTypeToClrValue(object p0)
		{
			return TextRes.GetString("EdmValueUtils_CannotConvertTypeToClrValue", new object[] { p0 });
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x00025755 File Offset: 0x00023955
		internal static string ValueParser_InvalidDuration(object p0)
		{
			return TextRes.GetString("ValueParser_InvalidDuration", new object[] { p0 });
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x0002576B File Offset: 0x0002396B
		internal static string PlatformHelper_DateTimeOffsetMustContainTimeZone(object p0)
		{
			return TextRes.GetString("PlatformHelper_DateTimeOffsetMustContainTimeZone", new object[] { p0 });
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000A18 RID: 2584 RVA: 0x00025781 File Offset: 0x00023981
		internal static string DataServiceRequest_FailGetCount
		{
			get
			{
				return TextRes.GetString("DataServiceRequest_FailGetCount");
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000A19 RID: 2585 RVA: 0x0002578D File Offset: 0x0002398D
		internal static string Context_ExecuteExpectedVoidResponse
		{
			get
			{
				return TextRes.GetString("Context_ExecuteExpectedVoidResponse");
			}
		}
	}
}
