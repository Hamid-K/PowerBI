using System;
using System.Globalization;
using System.Resources;
using System.Threading;

namespace Microsoft.OData.Client
{
	// Token: 0x020000ED RID: 237
	internal sealed class TextRes
	{
		// Token: 0x0600091B RID: 2331 RVA: 0x00024567 File Offset: 0x00022767
		internal TextRes()
		{
			this.resources = new ResourceManager("Microsoft.OData.Client", base.GetType().Assembly);
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x0002458C File Offset: 0x0002278C
		private static TextRes GetLoader()
		{
			if (TextRes.loader == null)
			{
				TextRes textRes = new TextRes();
				Interlocked.CompareExchange<TextRes>(ref TextRes.loader, textRes, null);
			}
			return TextRes.loader;
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x0600091D RID: 2333 RVA: 0x00003487 File Offset: 0x00001687
		private static CultureInfo Culture
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x0600091E RID: 2334 RVA: 0x000245B8 File Offset: 0x000227B8
		public static ResourceManager Resources
		{
			get
			{
				return TextRes.GetLoader().resources;
			}
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x000245C4 File Offset: 0x000227C4
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

		// Token: 0x06000920 RID: 2336 RVA: 0x00024644 File Offset: 0x00022844
		public static string GetString(string name)
		{
			TextRes textRes = TextRes.GetLoader();
			if (textRes == null)
			{
				return null;
			}
			return textRes.resources.GetString(name, TextRes.Culture);
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x0002466D File Offset: 0x0002286D
		public static string GetString(string name, out bool usedFallback)
		{
			usedFallback = false;
			return TextRes.GetString(name);
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x00024678 File Offset: 0x00022878
		public static object GetObject(string name)
		{
			TextRes textRes = TextRes.GetLoader();
			if (textRes == null)
			{
				return null;
			}
			return textRes.resources.GetObject(name, TextRes.Culture);
		}

		// Token: 0x040004E4 RID: 1252
		internal const string Batch_ExpectedContentType = "Batch_ExpectedContentType";

		// Token: 0x040004E5 RID: 1253
		internal const string Batch_ExpectedResponse = "Batch_ExpectedResponse";

		// Token: 0x040004E6 RID: 1254
		internal const string Batch_IncompleteResponseCount = "Batch_IncompleteResponseCount";

		// Token: 0x040004E7 RID: 1255
		internal const string Batch_UnexpectedContent = "Batch_UnexpectedContent";

		// Token: 0x040004E8 RID: 1256
		internal const string Context_BaseUri = "Context_BaseUri";

		// Token: 0x040004E9 RID: 1257
		internal const string Context_BaseUriRequired = "Context_BaseUriRequired";

		// Token: 0x040004EA RID: 1258
		internal const string Context_ResolveReturnedInvalidUri = "Context_ResolveReturnedInvalidUri";

		// Token: 0x040004EB RID: 1259
		internal const string Context_RequestUriIsRelativeBaseUriRequired = "Context_RequestUriIsRelativeBaseUriRequired";

		// Token: 0x040004EC RID: 1260
		internal const string Context_ResolveEntitySetOrBaseUriRequired = "Context_ResolveEntitySetOrBaseUriRequired";

		// Token: 0x040004ED RID: 1261
		internal const string Context_CannotConvertKey = "Context_CannotConvertKey";

		// Token: 0x040004EE RID: 1262
		internal const string Context_TrackingExpectsAbsoluteUri = "Context_TrackingExpectsAbsoluteUri";

		// Token: 0x040004EF RID: 1263
		internal const string Context_LocationHeaderExpectsAbsoluteUri = "Context_LocationHeaderExpectsAbsoluteUri";

		// Token: 0x040004F0 RID: 1264
		internal const string Context_LinkResourceInsertFailure = "Context_LinkResourceInsertFailure";

		// Token: 0x040004F1 RID: 1265
		internal const string Context_InternalError = "Context_InternalError";

		// Token: 0x040004F2 RID: 1266
		internal const string Context_BatchExecuteError = "Context_BatchExecuteError";

		// Token: 0x040004F3 RID: 1267
		internal const string Context_EntitySetName = "Context_EntitySetName";

		// Token: 0x040004F4 RID: 1268
		internal const string Context_BatchNotSupportedForNamedStreams = "Context_BatchNotSupportedForNamedStreams";

		// Token: 0x040004F5 RID: 1269
		internal const string Context_SetSaveStreamWithoutNamedStreamEditLink = "Context_SetSaveStreamWithoutNamedStreamEditLink";

		// Token: 0x040004F6 RID: 1270
		internal const string Content_EntityWithoutKey = "Content_EntityWithoutKey";

		// Token: 0x040004F7 RID: 1271
		internal const string Content_EntityIsNotEntityType = "Content_EntityIsNotEntityType";

		// Token: 0x040004F8 RID: 1272
		internal const string Context_EntityNotContained = "Context_EntityNotContained";

		// Token: 0x040004F9 RID: 1273
		internal const string Context_EntityAlreadyContained = "Context_EntityAlreadyContained";

		// Token: 0x040004FA RID: 1274
		internal const string Context_DifferentEntityAlreadyContained = "Context_DifferentEntityAlreadyContained";

		// Token: 0x040004FB RID: 1275
		internal const string Context_DidNotOriginateAsync = "Context_DidNotOriginateAsync";

		// Token: 0x040004FC RID: 1276
		internal const string Context_AsyncAlreadyDone = "Context_AsyncAlreadyDone";

		// Token: 0x040004FD RID: 1277
		internal const string Context_OperationCanceled = "Context_OperationCanceled";

		// Token: 0x040004FE RID: 1278
		internal const string Context_PropertyNotSupportedForMaxDataServiceVersionGreaterThanX = "Context_PropertyNotSupportedForMaxDataServiceVersionGreaterThanX";

		// Token: 0x040004FF RID: 1279
		internal const string Context_NoLoadWithInsertEnd = "Context_NoLoadWithInsertEnd";

		// Token: 0x04000500 RID: 1280
		internal const string Context_NoRelationWithInsertEnd = "Context_NoRelationWithInsertEnd";

		// Token: 0x04000501 RID: 1281
		internal const string Context_NoRelationWithDeleteEnd = "Context_NoRelationWithDeleteEnd";

		// Token: 0x04000502 RID: 1282
		internal const string Context_RelationAlreadyContained = "Context_RelationAlreadyContained";

		// Token: 0x04000503 RID: 1283
		internal const string Context_RelationNotRefOrCollection = "Context_RelationNotRefOrCollection";

		// Token: 0x04000504 RID: 1284
		internal const string Context_AddLinkCollectionOnly = "Context_AddLinkCollectionOnly";

		// Token: 0x04000505 RID: 1285
		internal const string Context_AddRelatedObjectCollectionOnly = "Context_AddRelatedObjectCollectionOnly";

		// Token: 0x04000506 RID: 1286
		internal const string Context_AddRelatedObjectSourceDeleted = "Context_AddRelatedObjectSourceDeleted";

		// Token: 0x04000507 RID: 1287
		internal const string Context_UpdateRelatedObjectNonCollectionOnly = "Context_UpdateRelatedObjectNonCollectionOnly";

		// Token: 0x04000508 RID: 1288
		internal const string Context_SetLinkReferenceOnly = "Context_SetLinkReferenceOnly";

		// Token: 0x04000509 RID: 1289
		internal const string Context_NoContentTypeForMediaLink = "Context_NoContentTypeForMediaLink";

		// Token: 0x0400050A RID: 1290
		internal const string Context_BatchNotSupportedForMediaLink = "Context_BatchNotSupportedForMediaLink";

		// Token: 0x0400050B RID: 1291
		internal const string Context_UnexpectedZeroRawRead = "Context_UnexpectedZeroRawRead";

		// Token: 0x0400050C RID: 1292
		internal const string Context_VersionNotSupported = "Context_VersionNotSupported";

		// Token: 0x0400050D RID: 1293
		internal const string Context_ResponseVersionIsBiggerThanProtocolVersion = "Context_ResponseVersionIsBiggerThanProtocolVersion";

		// Token: 0x0400050E RID: 1294
		internal const string Context_RequestVersionIsBiggerThanProtocolVersion = "Context_RequestVersionIsBiggerThanProtocolVersion";

		// Token: 0x0400050F RID: 1295
		internal const string Context_ChildResourceExists = "Context_ChildResourceExists";

		// Token: 0x04000510 RID: 1296
		internal const string Context_ContentTypeRequiredForNamedStream = "Context_ContentTypeRequiredForNamedStream";

		// Token: 0x04000511 RID: 1297
		internal const string Context_EntityNotMediaLinkEntry = "Context_EntityNotMediaLinkEntry";

		// Token: 0x04000512 RID: 1298
		internal const string Context_MLEWithoutSaveStream = "Context_MLEWithoutSaveStream";

		// Token: 0x04000513 RID: 1299
		internal const string Context_SetSaveStreamOnMediaEntryProperty = "Context_SetSaveStreamOnMediaEntryProperty";

		// Token: 0x04000514 RID: 1300
		internal const string Context_SetSaveStreamWithoutEditMediaLink = "Context_SetSaveStreamWithoutEditMediaLink";

		// Token: 0x04000515 RID: 1301
		internal const string Context_SetSaveStreamOnInvalidEntityState = "Context_SetSaveStreamOnInvalidEntityState";

		// Token: 0x04000516 RID: 1302
		internal const string Context_EntityDoesNotContainNamedStream = "Context_EntityDoesNotContainNamedStream";

		// Token: 0x04000517 RID: 1303
		internal const string Context_MissingSelfAndEditLinkForNamedStream = "Context_MissingSelfAndEditLinkForNamedStream";

		// Token: 0x04000518 RID: 1304
		internal const string Context_BothLocationAndIdMustBeSpecified = "Context_BothLocationAndIdMustBeSpecified";

		// Token: 0x04000519 RID: 1305
		internal const string Context_BodyOperationParametersNotAllowedWithGet = "Context_BodyOperationParametersNotAllowedWithGet";

		// Token: 0x0400051A RID: 1306
		internal const string Context_MissingOperationParameterName = "Context_MissingOperationParameterName";

		// Token: 0x0400051B RID: 1307
		internal const string Context_DuplicateUriOperationParameterName = "Context_DuplicateUriOperationParameterName";

		// Token: 0x0400051C RID: 1308
		internal const string Context_DuplicateBodyOperationParameterName = "Context_DuplicateBodyOperationParameterName";

		// Token: 0x0400051D RID: 1309
		internal const string Context_NullKeysAreNotSupported = "Context_NullKeysAreNotSupported";

		// Token: 0x0400051E RID: 1310
		internal const string Context_ExecuteExpectsGetOrPostOrDelete = "Context_ExecuteExpectsGetOrPostOrDelete";

		// Token: 0x0400051F RID: 1311
		internal const string Context_EndExecuteExpectedVoidResponse = "Context_EndExecuteExpectedVoidResponse";

		// Token: 0x04000520 RID: 1312
		internal const string Context_NullElementInOperationParameterArray = "Context_NullElementInOperationParameterArray";

		// Token: 0x04000521 RID: 1313
		internal const string Context_EntityMetadataBuilderIsRequired = "Context_EntityMetadataBuilderIsRequired";

		// Token: 0x04000522 RID: 1314
		internal const string Context_CannotChangeStateToAdded = "Context_CannotChangeStateToAdded";

		// Token: 0x04000523 RID: 1315
		internal const string Context_CannotChangeStateToModifiedIfNotUnchanged = "Context_CannotChangeStateToModifiedIfNotUnchanged";

		// Token: 0x04000524 RID: 1316
		internal const string Context_CannotChangeStateIfAdded = "Context_CannotChangeStateIfAdded";

		// Token: 0x04000525 RID: 1317
		internal const string Context_OnMessageCreatingReturningNull = "Context_OnMessageCreatingReturningNull";

		// Token: 0x04000526 RID: 1318
		internal const string Context_SendingRequest_InvalidWhenUsingOnMessageCreating = "Context_SendingRequest_InvalidWhenUsingOnMessageCreating";

		// Token: 0x04000527 RID: 1319
		internal const string Context_MustBeUsedWith = "Context_MustBeUsedWith";

		// Token: 0x04000528 RID: 1320
		internal const string DataServiceClientFormat_LoadServiceModelRequired = "DataServiceClientFormat_LoadServiceModelRequired";

		// Token: 0x04000529 RID: 1321
		internal const string DataServiceClientFormat_ValidServiceModelRequiredForJson = "DataServiceClientFormat_ValidServiceModelRequiredForJson";

		// Token: 0x0400052A RID: 1322
		internal const string Collection_NullCollectionReference = "Collection_NullCollectionReference";

		// Token: 0x0400052B RID: 1323
		internal const string ClientType_MissingOpenProperty = "ClientType_MissingOpenProperty";

		// Token: 0x0400052C RID: 1324
		internal const string Clienttype_MultipleOpenProperty = "Clienttype_MultipleOpenProperty";

		// Token: 0x0400052D RID: 1325
		internal const string ClientType_MissingProperty = "ClientType_MissingProperty";

		// Token: 0x0400052E RID: 1326
		internal const string ClientType_KeysMustBeSimpleTypes = "ClientType_KeysMustBeSimpleTypes";

		// Token: 0x0400052F RID: 1327
		internal const string ClientType_KeysOnDifferentDeclaredType = "ClientType_KeysOnDifferentDeclaredType";

		// Token: 0x04000530 RID: 1328
		internal const string ClientType_MissingMimeTypeProperty = "ClientType_MissingMimeTypeProperty";

		// Token: 0x04000531 RID: 1329
		internal const string ClientType_MissingMimeTypeDataProperty = "ClientType_MissingMimeTypeDataProperty";

		// Token: 0x04000532 RID: 1330
		internal const string ClientType_MissingMediaEntryProperty = "ClientType_MissingMediaEntryProperty";

		// Token: 0x04000533 RID: 1331
		internal const string ClientType_NoSettableFields = "ClientType_NoSettableFields";

		// Token: 0x04000534 RID: 1332
		internal const string ClientType_MultipleImplementationNotSupported = "ClientType_MultipleImplementationNotSupported";

		// Token: 0x04000535 RID: 1333
		internal const string ClientType_NullOpenProperties = "ClientType_NullOpenProperties";

		// Token: 0x04000536 RID: 1334
		internal const string ClientType_Ambiguous = "ClientType_Ambiguous";

		// Token: 0x04000537 RID: 1335
		internal const string ClientType_UnsupportedType = "ClientType_UnsupportedType";

		// Token: 0x04000538 RID: 1336
		internal const string ClientType_CollectionOfCollectionNotSupported = "ClientType_CollectionOfCollectionNotSupported";

		// Token: 0x04000539 RID: 1337
		internal const string ClientType_MultipleTypesWithSameName = "ClientType_MultipleTypesWithSameName";

		// Token: 0x0400053A RID: 1338
		internal const string WebUtil_TypeMismatchInCollection = "WebUtil_TypeMismatchInCollection";

		// Token: 0x0400053B RID: 1339
		internal const string WebUtil_TypeMismatchInNonPropertyCollection = "WebUtil_TypeMismatchInNonPropertyCollection";

		// Token: 0x0400053C RID: 1340
		internal const string ClientTypeCache_NonEntityTypeCannotContainEntityProperties = "ClientTypeCache_NonEntityTypeCannotContainEntityProperties";

		// Token: 0x0400053D RID: 1341
		internal const string DataServiceException_GeneralError = "DataServiceException_GeneralError";

		// Token: 0x0400053E RID: 1342
		internal const string Deserialize_GetEnumerator = "Deserialize_GetEnumerator";

		// Token: 0x0400053F RID: 1343
		internal const string Deserialize_Current = "Deserialize_Current";

		// Token: 0x04000540 RID: 1344
		internal const string Deserialize_MixedTextWithComment = "Deserialize_MixedTextWithComment";

		// Token: 0x04000541 RID: 1345
		internal const string Deserialize_ExpectingSimpleValue = "Deserialize_ExpectingSimpleValue";

		// Token: 0x04000542 RID: 1346
		internal const string Deserialize_MismatchAtomLinkLocalSimple = "Deserialize_MismatchAtomLinkLocalSimple";

		// Token: 0x04000543 RID: 1347
		internal const string Deserialize_MismatchAtomLinkFeedPropertyNotCollection = "Deserialize_MismatchAtomLinkFeedPropertyNotCollection";

		// Token: 0x04000544 RID: 1348
		internal const string Deserialize_MismatchAtomLinkEntryPropertyIsCollection = "Deserialize_MismatchAtomLinkEntryPropertyIsCollection";

		// Token: 0x04000545 RID: 1349
		internal const string Deserialize_NoLocationHeader = "Deserialize_NoLocationHeader";

		// Token: 0x04000546 RID: 1350
		internal const string Deserialize_ServerException = "Deserialize_ServerException";

		// Token: 0x04000547 RID: 1351
		internal const string Deserialize_MissingIdElement = "Deserialize_MissingIdElement";

		// Token: 0x04000548 RID: 1352
		internal const string Collection_NullCollectionNotSupported = "Collection_NullCollectionNotSupported";

		// Token: 0x04000549 RID: 1353
		internal const string Collection_NullNonPropertyCollectionNotSupported = "Collection_NullNonPropertyCollectionNotSupported";

		// Token: 0x0400054A RID: 1354
		internal const string Collection_NullCollectionItemsNotSupported = "Collection_NullCollectionItemsNotSupported";

		// Token: 0x0400054B RID: 1355
		internal const string Collection_CollectionTypesInCollectionOfPrimitiveTypesNotAllowed = "Collection_CollectionTypesInCollectionOfPrimitiveTypesNotAllowed";

		// Token: 0x0400054C RID: 1356
		internal const string Collection_PrimitiveTypesInCollectionOfComplexTypesNotAllowed = "Collection_PrimitiveTypesInCollectionOfComplexTypesNotAllowed";

		// Token: 0x0400054D RID: 1357
		internal const string EntityDescriptor_MissingSelfEditLink = "EntityDescriptor_MissingSelfEditLink";

		// Token: 0x0400054E RID: 1358
		internal const string HttpProcessUtility_ContentTypeMissing = "HttpProcessUtility_ContentTypeMissing";

		// Token: 0x0400054F RID: 1359
		internal const string HttpProcessUtility_MediaTypeMissingValue = "HttpProcessUtility_MediaTypeMissingValue";

		// Token: 0x04000550 RID: 1360
		internal const string HttpProcessUtility_MediaTypeRequiresSemicolonBeforeParameter = "HttpProcessUtility_MediaTypeRequiresSemicolonBeforeParameter";

		// Token: 0x04000551 RID: 1361
		internal const string HttpProcessUtility_MediaTypeRequiresSlash = "HttpProcessUtility_MediaTypeRequiresSlash";

		// Token: 0x04000552 RID: 1362
		internal const string HttpProcessUtility_MediaTypeRequiresSubType = "HttpProcessUtility_MediaTypeRequiresSubType";

		// Token: 0x04000553 RID: 1363
		internal const string HttpProcessUtility_MediaTypeUnspecified = "HttpProcessUtility_MediaTypeUnspecified";

		// Token: 0x04000554 RID: 1364
		internal const string HttpProcessUtility_EncodingNotSupported = "HttpProcessUtility_EncodingNotSupported";

		// Token: 0x04000555 RID: 1365
		internal const string HttpProcessUtility_EscapeCharWithoutQuotes = "HttpProcessUtility_EscapeCharWithoutQuotes";

		// Token: 0x04000556 RID: 1366
		internal const string HttpProcessUtility_EscapeCharAtEnd = "HttpProcessUtility_EscapeCharAtEnd";

		// Token: 0x04000557 RID: 1367
		internal const string HttpProcessUtility_ClosingQuoteNotFound = "HttpProcessUtility_ClosingQuoteNotFound";

		// Token: 0x04000558 RID: 1368
		internal const string MaterializeFromAtom_CountNotPresent = "MaterializeFromAtom_CountNotPresent";

		// Token: 0x04000559 RID: 1369
		internal const string MaterializeFromAtom_TopLevelLinkNotAvailable = "MaterializeFromAtom_TopLevelLinkNotAvailable";

		// Token: 0x0400055A RID: 1370
		internal const string MaterializeFromAtom_CollectionKeyNotPresentInLinkTable = "MaterializeFromAtom_CollectionKeyNotPresentInLinkTable";

		// Token: 0x0400055B RID: 1371
		internal const string MaterializeFromAtom_GetNestLinkForFlatCollection = "MaterializeFromAtom_GetNestLinkForFlatCollection";

		// Token: 0x0400055C RID: 1372
		internal const string ODataRequestMessage_GetStreamMethodNotSupported = "ODataRequestMessage_GetStreamMethodNotSupported";

		// Token: 0x0400055D RID: 1373
		internal const string Util_EmptyString = "Util_EmptyString";

		// Token: 0x0400055E RID: 1374
		internal const string Util_EmptyArray = "Util_EmptyArray";

		// Token: 0x0400055F RID: 1375
		internal const string Util_NullArrayElement = "Util_NullArrayElement";

		// Token: 0x04000560 RID: 1376
		internal const string ALinq_UnsupportedExpression = "ALinq_UnsupportedExpression";

		// Token: 0x04000561 RID: 1377
		internal const string ALinq_CouldNotConvert = "ALinq_CouldNotConvert";

		// Token: 0x04000562 RID: 1378
		internal const string ALinq_MethodNotSupported = "ALinq_MethodNotSupported";

		// Token: 0x04000563 RID: 1379
		internal const string ALinq_UnaryNotSupported = "ALinq_UnaryNotSupported";

		// Token: 0x04000564 RID: 1380
		internal const string ALinq_BinaryNotSupported = "ALinq_BinaryNotSupported";

		// Token: 0x04000565 RID: 1381
		internal const string ALinq_ConstantNotSupported = "ALinq_ConstantNotSupported";

		// Token: 0x04000566 RID: 1382
		internal const string ALinq_TypeBinaryNotSupported = "ALinq_TypeBinaryNotSupported";

		// Token: 0x04000567 RID: 1383
		internal const string ALinq_ConditionalNotSupported = "ALinq_ConditionalNotSupported";

		// Token: 0x04000568 RID: 1384
		internal const string ALinq_ParameterNotSupported = "ALinq_ParameterNotSupported";

		// Token: 0x04000569 RID: 1385
		internal const string ALinq_MemberAccessNotSupported = "ALinq_MemberAccessNotSupported";

		// Token: 0x0400056A RID: 1386
		internal const string ALinq_LambdaNotSupported = "ALinq_LambdaNotSupported";

		// Token: 0x0400056B RID: 1387
		internal const string ALinq_NewNotSupported = "ALinq_NewNotSupported";

		// Token: 0x0400056C RID: 1388
		internal const string ALinq_MemberInitNotSupported = "ALinq_MemberInitNotSupported";

		// Token: 0x0400056D RID: 1389
		internal const string ALinq_ListInitNotSupported = "ALinq_ListInitNotSupported";

		// Token: 0x0400056E RID: 1390
		internal const string ALinq_NewArrayNotSupported = "ALinq_NewArrayNotSupported";

		// Token: 0x0400056F RID: 1391
		internal const string ALinq_InvocationNotSupported = "ALinq_InvocationNotSupported";

		// Token: 0x04000570 RID: 1392
		internal const string ALinq_QueryOptionsOnlyAllowedOnLeafNodes = "ALinq_QueryOptionsOnlyAllowedOnLeafNodes";

		// Token: 0x04000571 RID: 1393
		internal const string ALinq_CantExpand = "ALinq_CantExpand";

		// Token: 0x04000572 RID: 1394
		internal const string ALinq_CantCastToUnsupportedPrimitive = "ALinq_CantCastToUnsupportedPrimitive";

		// Token: 0x04000573 RID: 1395
		internal const string ALinq_CantNavigateWithoutKeyPredicate = "ALinq_CantNavigateWithoutKeyPredicate";

		// Token: 0x04000574 RID: 1396
		internal const string ALinq_CanOnlyApplyOneKeyPredicate = "ALinq_CanOnlyApplyOneKeyPredicate";

		// Token: 0x04000575 RID: 1397
		internal const string ALinq_CantTranslateExpression = "ALinq_CantTranslateExpression";

		// Token: 0x04000576 RID: 1398
		internal const string ALinq_TranslationError = "ALinq_TranslationError";

		// Token: 0x04000577 RID: 1399
		internal const string ALinq_CantAddQueryOption = "ALinq_CantAddQueryOption";

		// Token: 0x04000578 RID: 1400
		internal const string ALinq_CantAddDuplicateQueryOption = "ALinq_CantAddDuplicateQueryOption";

		// Token: 0x04000579 RID: 1401
		internal const string ALinq_CantAddAstoriaQueryOption = "ALinq_CantAddAstoriaQueryOption";

		// Token: 0x0400057A RID: 1402
		internal const string ALinq_CantAddQueryOptionStartingWithDollarSign = "ALinq_CantAddQueryOptionStartingWithDollarSign";

		// Token: 0x0400057B RID: 1403
		internal const string ALinq_CantReferToPublicField = "ALinq_CantReferToPublicField";

		// Token: 0x0400057C RID: 1404
		internal const string ALinq_QueryOptionsOnlyAllowedOnSingletons = "ALinq_QueryOptionsOnlyAllowedOnSingletons";

		// Token: 0x0400057D RID: 1405
		internal const string ALinq_QueryOptionOutOfOrder = "ALinq_QueryOptionOutOfOrder";

		// Token: 0x0400057E RID: 1406
		internal const string ALinq_CannotAddCountOption = "ALinq_CannotAddCountOption";

		// Token: 0x0400057F RID: 1407
		internal const string ALinq_CannotAddCountOptionConflict = "ALinq_CannotAddCountOptionConflict";

		// Token: 0x04000580 RID: 1408
		internal const string ALinq_ProjectionOnlyAllowedOnLeafNodes = "ALinq_ProjectionOnlyAllowedOnLeafNodes";

		// Token: 0x04000581 RID: 1409
		internal const string ALinq_ProjectionCanOnlyHaveOneProjection = "ALinq_ProjectionCanOnlyHaveOneProjection";

		// Token: 0x04000582 RID: 1410
		internal const string ALinq_ProjectionMemberAssignmentMismatch = "ALinq_ProjectionMemberAssignmentMismatch";

		// Token: 0x04000583 RID: 1411
		internal const string ALinq_InvalidExpressionInNavigationPath = "ALinq_InvalidExpressionInNavigationPath";

		// Token: 0x04000584 RID: 1412
		internal const string ALinq_ExpressionNotSupportedInProjectionToEntity = "ALinq_ExpressionNotSupportedInProjectionToEntity";

		// Token: 0x04000585 RID: 1413
		internal const string ALinq_ExpressionNotSupportedInProjection = "ALinq_ExpressionNotSupportedInProjection";

		// Token: 0x04000586 RID: 1414
		internal const string ALinq_CannotConstructKnownEntityTypes = "ALinq_CannotConstructKnownEntityTypes";

		// Token: 0x04000587 RID: 1415
		internal const string ALinq_CannotCreateConstantEntity = "ALinq_CannotCreateConstantEntity";

		// Token: 0x04000588 RID: 1416
		internal const string ALinq_PropertyNamesMustMatchInProjections = "ALinq_PropertyNamesMustMatchInProjections";

		// Token: 0x04000589 RID: 1417
		internal const string ALinq_CanOnlyProjectTheLeaf = "ALinq_CanOnlyProjectTheLeaf";

		// Token: 0x0400058A RID: 1418
		internal const string ALinq_CannotProjectWithExplicitExpansion = "ALinq_CannotProjectWithExplicitExpansion";

		// Token: 0x0400058B RID: 1419
		internal const string ALinq_CollectionPropertyNotSupportedInOrderBy = "ALinq_CollectionPropertyNotSupportedInOrderBy";

		// Token: 0x0400058C RID: 1420
		internal const string ALinq_CollectionPropertyNotSupportedInWhere = "ALinq_CollectionPropertyNotSupportedInWhere";

		// Token: 0x0400058D RID: 1421
		internal const string ALinq_CollectionMemberAccessNotSupportedInNavigation = "ALinq_CollectionMemberAccessNotSupportedInNavigation";

		// Token: 0x0400058E RID: 1422
		internal const string ALinq_LinkPropertyNotSupportedInExpression = "ALinq_LinkPropertyNotSupportedInExpression";

		// Token: 0x0400058F RID: 1423
		internal const string ALinq_OfTypeArgumentNotAvailable = "ALinq_OfTypeArgumentNotAvailable";

		// Token: 0x04000590 RID: 1424
		internal const string ALinq_CannotUseTypeFiltersMultipleTimes = "ALinq_CannotUseTypeFiltersMultipleTimes";

		// Token: 0x04000591 RID: 1425
		internal const string ALinq_ExpressionCannotEndWithTypeAs = "ALinq_ExpressionCannotEndWithTypeAs";

		// Token: 0x04000592 RID: 1426
		internal const string ALinq_TypeAsNotSupportedForMaxDataServiceVersionLessThan3 = "ALinq_TypeAsNotSupportedForMaxDataServiceVersionLessThan3";

		// Token: 0x04000593 RID: 1427
		internal const string ALinq_TypeAsArgumentNotEntityType = "ALinq_TypeAsArgumentNotEntityType";

		// Token: 0x04000594 RID: 1428
		internal const string ALinq_InvalidSourceForAnyAll = "ALinq_InvalidSourceForAnyAll";

		// Token: 0x04000595 RID: 1429
		internal const string ALinq_AnyAllNotSupportedInOrderBy = "ALinq_AnyAllNotSupportedInOrderBy";

		// Token: 0x04000596 RID: 1430
		internal const string ALinq_FormatQueryOptionNotSupported = "ALinq_FormatQueryOptionNotSupported";

		// Token: 0x04000597 RID: 1431
		internal const string ALinq_IllegalSystemQueryOption = "ALinq_IllegalSystemQueryOption";

		// Token: 0x04000598 RID: 1432
		internal const string ALinq_IllegalPathStructure = "ALinq_IllegalPathStructure";

		// Token: 0x04000599 RID: 1433
		internal const string ALinq_TypeTokenWithNoTrailingNavProp = "ALinq_TypeTokenWithNoTrailingNavProp";

		// Token: 0x0400059A RID: 1434
		internal const string DSKAttribute_MustSpecifyAtleastOnePropertyName = "DSKAttribute_MustSpecifyAtleastOnePropertyName";

		// Token: 0x0400059B RID: 1435
		internal const string DataServiceCollection_LoadRequiresTargetCollectionObserved = "DataServiceCollection_LoadRequiresTargetCollectionObserved";

		// Token: 0x0400059C RID: 1436
		internal const string DataServiceCollection_CannotStopTrackingChildCollection = "DataServiceCollection_CannotStopTrackingChildCollection";

		// Token: 0x0400059D RID: 1437
		internal const string DataServiceCollection_OperationForTrackedOnly = "DataServiceCollection_OperationForTrackedOnly";

		// Token: 0x0400059E RID: 1438
		internal const string DataServiceCollection_CannotDetermineContextFromItems = "DataServiceCollection_CannotDetermineContextFromItems";

		// Token: 0x0400059F RID: 1439
		internal const string DataServiceCollection_InsertIntoTrackedButNotLoadedCollection = "DataServiceCollection_InsertIntoTrackedButNotLoadedCollection";

		// Token: 0x040005A0 RID: 1440
		internal const string DataServiceCollection_MultipleLoadAsyncOperationsAtTheSameTime = "DataServiceCollection_MultipleLoadAsyncOperationsAtTheSameTime";

		// Token: 0x040005A1 RID: 1441
		internal const string DataServiceCollection_LoadAsyncNoParamsWithoutParentEntity = "DataServiceCollection_LoadAsyncNoParamsWithoutParentEntity";

		// Token: 0x040005A2 RID: 1442
		internal const string DataServiceCollection_LoadAsyncRequiresDataServiceQuery = "DataServiceCollection_LoadAsyncRequiresDataServiceQuery";

		// Token: 0x040005A3 RID: 1443
		internal const string DataBinding_DataServiceCollectionArgumentMustHaveEntityType = "DataBinding_DataServiceCollectionArgumentMustHaveEntityType";

		// Token: 0x040005A4 RID: 1444
		internal const string DataBinding_CollectionPropertySetterValueHasObserver = "DataBinding_CollectionPropertySetterValueHasObserver";

		// Token: 0x040005A5 RID: 1445
		internal const string DataBinding_DataServiceCollectionChangedUnknownActionCollection = "DataBinding_DataServiceCollectionChangedUnknownActionCollection";

		// Token: 0x040005A6 RID: 1446
		internal const string DataBinding_CollectionChangedUnknownActionCollection = "DataBinding_CollectionChangedUnknownActionCollection";

		// Token: 0x040005A7 RID: 1447
		internal const string DataBinding_BindingOperation_DetachedSource = "DataBinding_BindingOperation_DetachedSource";

		// Token: 0x040005A8 RID: 1448
		internal const string DataBinding_BindingOperation_ArrayItemNull = "DataBinding_BindingOperation_ArrayItemNull";

		// Token: 0x040005A9 RID: 1449
		internal const string DataBinding_BindingOperation_ArrayItemNotEntity = "DataBinding_BindingOperation_ArrayItemNotEntity";

		// Token: 0x040005AA RID: 1450
		internal const string DataBinding_Util_UnknownEntitySetName = "DataBinding_Util_UnknownEntitySetName";

		// Token: 0x040005AB RID: 1451
		internal const string DataBinding_EntityAlreadyInCollection = "DataBinding_EntityAlreadyInCollection";

		// Token: 0x040005AC RID: 1452
		internal const string DataBinding_NotifyPropertyChangedNotImpl = "DataBinding_NotifyPropertyChangedNotImpl";

		// Token: 0x040005AD RID: 1453
		internal const string DataBinding_NotifyCollectionChangedNotImpl = "DataBinding_NotifyCollectionChangedNotImpl";

		// Token: 0x040005AE RID: 1454
		internal const string DataBinding_ComplexObjectAssociatedWithMultipleEntities = "DataBinding_ComplexObjectAssociatedWithMultipleEntities";

		// Token: 0x040005AF RID: 1455
		internal const string DataBinding_CollectionAssociatedWithMultipleEntities = "DataBinding_CollectionAssociatedWithMultipleEntities";

		// Token: 0x040005B0 RID: 1456
		internal const string AtomParser_SingleEntry_NoneFound = "AtomParser_SingleEntry_NoneFound";

		// Token: 0x040005B1 RID: 1457
		internal const string AtomParser_SingleEntry_MultipleFound = "AtomParser_SingleEntry_MultipleFound";

		// Token: 0x040005B2 RID: 1458
		internal const string AtomParser_SingleEntry_ExpectedFeedOrEntry = "AtomParser_SingleEntry_ExpectedFeedOrEntry";

		// Token: 0x040005B3 RID: 1459
		internal const string AtomMaterializer_CannotAssignNull = "AtomMaterializer_CannotAssignNull";

		// Token: 0x040005B4 RID: 1460
		internal const string AtomMaterializer_EntryIntoCollectionMismatch = "AtomMaterializer_EntryIntoCollectionMismatch";

		// Token: 0x040005B5 RID: 1461
		internal const string AtomMaterializer_EntryToAccessIsNull = "AtomMaterializer_EntryToAccessIsNull";

		// Token: 0x040005B6 RID: 1462
		internal const string AtomMaterializer_EntryToInitializeIsNull = "AtomMaterializer_EntryToInitializeIsNull";

		// Token: 0x040005B7 RID: 1463
		internal const string AtomMaterializer_ProjectEntityTypeMismatch = "AtomMaterializer_ProjectEntityTypeMismatch";

		// Token: 0x040005B8 RID: 1464
		internal const string AtomMaterializer_PropertyMissing = "AtomMaterializer_PropertyMissing";

		// Token: 0x040005B9 RID: 1465
		internal const string AtomMaterializer_PropertyNotExpectedEntry = "AtomMaterializer_PropertyNotExpectedEntry";

		// Token: 0x040005BA RID: 1466
		internal const string AtomMaterializer_DataServiceCollectionNotSupportedForNonEntities = "AtomMaterializer_DataServiceCollectionNotSupportedForNonEntities";

		// Token: 0x040005BB RID: 1467
		internal const string AtomMaterializer_NoParameterlessCtorForCollectionProperty = "AtomMaterializer_NoParameterlessCtorForCollectionProperty";

		// Token: 0x040005BC RID: 1468
		internal const string AtomMaterializer_InvalidCollectionItem = "AtomMaterializer_InvalidCollectionItem";

		// Token: 0x040005BD RID: 1469
		internal const string AtomMaterializer_InvalidEntityType = "AtomMaterializer_InvalidEntityType";

		// Token: 0x040005BE RID: 1470
		internal const string AtomMaterializer_InvalidNonEntityType = "AtomMaterializer_InvalidNonEntityType";

		// Token: 0x040005BF RID: 1471
		internal const string AtomMaterializer_CollectionExpectedCollection = "AtomMaterializer_CollectionExpectedCollection";

		// Token: 0x040005C0 RID: 1472
		internal const string AtomMaterializer_InvalidResponsePayload = "AtomMaterializer_InvalidResponsePayload";

		// Token: 0x040005C1 RID: 1473
		internal const string AtomMaterializer_InvalidContentTypeEncountered = "AtomMaterializer_InvalidContentTypeEncountered";

		// Token: 0x040005C2 RID: 1474
		internal const string AtomMaterializer_MaterializationTypeError = "AtomMaterializer_MaterializationTypeError";

		// Token: 0x040005C3 RID: 1475
		internal const string AtomMaterializer_ResetAfterEnumeratorCreationError = "AtomMaterializer_ResetAfterEnumeratorCreationError";

		// Token: 0x040005C4 RID: 1476
		internal const string AtomMaterializer_TypeShouldBeCollectionError = "AtomMaterializer_TypeShouldBeCollectionError";

		// Token: 0x040005C5 RID: 1477
		internal const string Serializer_LoopsNotAllowedInComplexTypes = "Serializer_LoopsNotAllowedInComplexTypes";

		// Token: 0x040005C6 RID: 1478
		internal const string Serializer_LoopsNotAllowedInNonPropertyComplexTypes = "Serializer_LoopsNotAllowedInNonPropertyComplexTypes";

		// Token: 0x040005C7 RID: 1479
		internal const string Serializer_InvalidCollectionParamterItemType = "Serializer_InvalidCollectionParamterItemType";

		// Token: 0x040005C8 RID: 1480
		internal const string Serializer_NullCollectionParamterItemValue = "Serializer_NullCollectionParamterItemValue";

		// Token: 0x040005C9 RID: 1481
		internal const string Serializer_InvalidParameterType = "Serializer_InvalidParameterType";

		// Token: 0x040005CA RID: 1482
		internal const string Serializer_UriDoesNotContainParameterAlias = "Serializer_UriDoesNotContainParameterAlias";

		// Token: 0x040005CB RID: 1483
		internal const string Serializer_InvalidEnumMemberValue = "Serializer_InvalidEnumMemberValue";

		// Token: 0x040005CC RID: 1484
		internal const string DataServiceQuery_EnumerationNotSupported = "DataServiceQuery_EnumerationNotSupported";

		// Token: 0x040005CD RID: 1485
		internal const string Context_SendingRequestEventArgsNotHttp = "Context_SendingRequestEventArgsNotHttp";

		// Token: 0x040005CE RID: 1486
		internal const string General_InternalError = "General_InternalError";

		// Token: 0x040005CF RID: 1487
		internal const string ODataMetadataBuilder_MissingEntitySetUri = "ODataMetadataBuilder_MissingEntitySetUri";

		// Token: 0x040005D0 RID: 1488
		internal const string ODataMetadataBuilder_MissingSegmentForEntitySetUriSuffix = "ODataMetadataBuilder_MissingSegmentForEntitySetUriSuffix";

		// Token: 0x040005D1 RID: 1489
		internal const string ODataMetadataBuilder_MissingEntityInstanceUri = "ODataMetadataBuilder_MissingEntityInstanceUri";

		// Token: 0x040005D2 RID: 1490
		internal const string EdmValueUtils_UnsupportedPrimitiveType = "EdmValueUtils_UnsupportedPrimitiveType";

		// Token: 0x040005D3 RID: 1491
		internal const string EdmValueUtils_IncorrectPrimitiveTypeKind = "EdmValueUtils_IncorrectPrimitiveTypeKind";

		// Token: 0x040005D4 RID: 1492
		internal const string EdmValueUtils_IncorrectPrimitiveTypeKindNoTypeName = "EdmValueUtils_IncorrectPrimitiveTypeKindNoTypeName";

		// Token: 0x040005D5 RID: 1493
		internal const string EdmValueUtils_CannotConvertTypeToClrValue = "EdmValueUtils_CannotConvertTypeToClrValue";

		// Token: 0x040005D6 RID: 1494
		internal const string ValueParser_InvalidDuration = "ValueParser_InvalidDuration";

		// Token: 0x040005D7 RID: 1495
		internal const string PlatformHelper_DateTimeOffsetMustContainTimeZone = "PlatformHelper_DateTimeOffsetMustContainTimeZone";

		// Token: 0x040005D8 RID: 1496
		internal const string DataServiceRequest_FailGetCount = "DataServiceRequest_FailGetCount";

		// Token: 0x040005D9 RID: 1497
		internal const string Context_ExecuteExpectedVoidResponse = "Context_ExecuteExpectedVoidResponse";

		// Token: 0x040005DA RID: 1498
		private static TextRes loader;

		// Token: 0x040005DB RID: 1499
		private ResourceManager resources;
	}
}
