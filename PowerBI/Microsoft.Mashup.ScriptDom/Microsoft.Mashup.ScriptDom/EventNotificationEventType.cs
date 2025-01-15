using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020000B6 RID: 182
	internal enum EventNotificationEventType
	{
		// Token: 0x04000479 RID: 1145
		Unknown,
		// Token: 0x0400047A RID: 1146
		CreateTable = 21,
		// Token: 0x0400047B RID: 1147
		AlterTable,
		// Token: 0x0400047C RID: 1148
		DropTable,
		// Token: 0x0400047D RID: 1149
		CreateIndex,
		// Token: 0x0400047E RID: 1150
		AlterIndex,
		// Token: 0x0400047F RID: 1151
		DropIndex,
		// Token: 0x04000480 RID: 1152
		CreateStatistics,
		// Token: 0x04000481 RID: 1153
		UpdateStatistics,
		// Token: 0x04000482 RID: 1154
		DropStatistics,
		// Token: 0x04000483 RID: 1155
		CreateSynonym = 34,
		// Token: 0x04000484 RID: 1156
		DropSynonym = 36,
		// Token: 0x04000485 RID: 1157
		CreateView = 41,
		// Token: 0x04000486 RID: 1158
		AlterView,
		// Token: 0x04000487 RID: 1159
		DropView,
		// Token: 0x04000488 RID: 1160
		CreateProcedure = 51,
		// Token: 0x04000489 RID: 1161
		AlterProcedure,
		// Token: 0x0400048A RID: 1162
		DropProcedure,
		// Token: 0x0400048B RID: 1163
		CreateFunction = 61,
		// Token: 0x0400048C RID: 1164
		AlterFunction,
		// Token: 0x0400048D RID: 1165
		DropFunction,
		// Token: 0x0400048E RID: 1166
		CreateTrigger = 71,
		// Token: 0x0400048F RID: 1167
		AlterTrigger,
		// Token: 0x04000490 RID: 1168
		DropTrigger,
		// Token: 0x04000491 RID: 1169
		CreateEventNotification,
		// Token: 0x04000492 RID: 1170
		DropEventNotification = 76,
		// Token: 0x04000493 RID: 1171
		CreateType = 91,
		// Token: 0x04000494 RID: 1172
		DropType = 93,
		// Token: 0x04000495 RID: 1173
		CreateAssembly = 101,
		// Token: 0x04000496 RID: 1174
		AlterAssembly,
		// Token: 0x04000497 RID: 1175
		DropAssembly,
		// Token: 0x04000498 RID: 1176
		CreateUser = 131,
		// Token: 0x04000499 RID: 1177
		AlterUser,
		// Token: 0x0400049A RID: 1178
		DropUser,
		// Token: 0x0400049B RID: 1179
		CreateRole,
		// Token: 0x0400049C RID: 1180
		AlterRole,
		// Token: 0x0400049D RID: 1181
		DropRole,
		// Token: 0x0400049E RID: 1182
		CreateApplicationRole,
		// Token: 0x0400049F RID: 1183
		AlterApplicationRole,
		// Token: 0x040004A0 RID: 1184
		DropApplicationRole,
		// Token: 0x040004A1 RID: 1185
		CreateSchema = 141,
		// Token: 0x040004A2 RID: 1186
		AlterSchema,
		// Token: 0x040004A3 RID: 1187
		DropSchema,
		// Token: 0x040004A4 RID: 1188
		CreateLogin,
		// Token: 0x040004A5 RID: 1189
		AlterLogin,
		// Token: 0x040004A6 RID: 1190
		DropLogin,
		// Token: 0x040004A7 RID: 1191
		CreateMessageType = 151,
		// Token: 0x040004A8 RID: 1192
		AlterMessageType,
		// Token: 0x040004A9 RID: 1193
		DropMessageType,
		// Token: 0x040004AA RID: 1194
		CreateContract,
		// Token: 0x040004AB RID: 1195
		DropContract = 156,
		// Token: 0x040004AC RID: 1196
		CreateQueue,
		// Token: 0x040004AD RID: 1197
		AlterQueue,
		// Token: 0x040004AE RID: 1198
		DropQueue,
		// Token: 0x040004AF RID: 1199
		BrokerQueueDisabled,
		// Token: 0x040004B0 RID: 1200
		CreateService,
		// Token: 0x040004B1 RID: 1201
		AlterService,
		// Token: 0x040004B2 RID: 1202
		DropService,
		// Token: 0x040004B3 RID: 1203
		CreateRoute,
		// Token: 0x040004B4 RID: 1204
		AlterRoute,
		// Token: 0x040004B5 RID: 1205
		DropRoute,
		// Token: 0x040004B6 RID: 1206
		GrantServer,
		// Token: 0x040004B7 RID: 1207
		DenyServer,
		// Token: 0x040004B8 RID: 1208
		RevokeServer,
		// Token: 0x040004B9 RID: 1209
		GrantDatabase,
		// Token: 0x040004BA RID: 1210
		DenyDatabase,
		// Token: 0x040004BB RID: 1211
		RevokeDatabase,
		// Token: 0x040004BC RID: 1212
		QueueActivation,
		// Token: 0x040004BD RID: 1213
		CreateRemoteServiceBinding,
		// Token: 0x040004BE RID: 1214
		AlterRemoteServiceBinding,
		// Token: 0x040004BF RID: 1215
		DropRemoteServiceBinding,
		// Token: 0x040004C0 RID: 1216
		CreateXmlSchemaCollection,
		// Token: 0x040004C1 RID: 1217
		AlterXmlSchemaCollection,
		// Token: 0x040004C2 RID: 1218
		DropXmlSchemaCollection,
		// Token: 0x040004C3 RID: 1219
		CreateEndpoint = 181,
		// Token: 0x040004C4 RID: 1220
		AlterEndpoint,
		// Token: 0x040004C5 RID: 1221
		DropEndpoint,
		// Token: 0x040004C6 RID: 1222
		CreatePartitionFunction = 191,
		// Token: 0x040004C7 RID: 1223
		AlterPartitionFunction,
		// Token: 0x040004C8 RID: 1224
		DropPartitionFunction,
		// Token: 0x040004C9 RID: 1225
		CreatePartitionScheme,
		// Token: 0x040004CA RID: 1226
		AlterPartitionScheme,
		// Token: 0x040004CB RID: 1227
		DropPartitionScheme,
		// Token: 0x040004CC RID: 1228
		CreateCertificate,
		// Token: 0x040004CD RID: 1229
		AlterCertificate,
		// Token: 0x040004CE RID: 1230
		DropCertificate,
		// Token: 0x040004CF RID: 1231
		CreateDatabase = 201,
		// Token: 0x040004D0 RID: 1232
		AlterDatabase,
		// Token: 0x040004D1 RID: 1233
		DropDatabase,
		// Token: 0x040004D2 RID: 1234
		AlterAuthorizationServer,
		// Token: 0x040004D3 RID: 1235
		AlterAuthorizationDatabase,
		// Token: 0x040004D4 RID: 1236
		CreateXmlIndex,
		// Token: 0x040004D5 RID: 1237
		AddRoleMember,
		// Token: 0x040004D6 RID: 1238
		DropRoleMember,
		// Token: 0x040004D7 RID: 1239
		AddServerRoleMember,
		// Token: 0x040004D8 RID: 1240
		DropServerRoleMember,
		// Token: 0x040004D9 RID: 1241
		AlterExtendedProperty,
		// Token: 0x040004DA RID: 1242
		AlterFullTextCatalog,
		// Token: 0x040004DB RID: 1243
		AlterFullTextIndex,
		// Token: 0x040004DC RID: 1244
		AlterInstance,
		// Token: 0x040004DD RID: 1245
		AlterMessage,
		// Token: 0x040004DE RID: 1246
		AlterPlanGuide,
		// Token: 0x040004DF RID: 1247
		AlterRemoteServer,
		// Token: 0x040004E0 RID: 1248
		BindDefault,
		// Token: 0x040004E1 RID: 1249
		BindRule,
		// Token: 0x040004E2 RID: 1250
		CreateDefault,
		// Token: 0x040004E3 RID: 1251
		CreateExtendedProcedure,
		// Token: 0x040004E4 RID: 1252
		CreateExtendedProperty,
		// Token: 0x040004E5 RID: 1253
		CreateFullTextCatalog,
		// Token: 0x040004E6 RID: 1254
		CreateFullTextIndex,
		// Token: 0x040004E7 RID: 1255
		CreateLinkedServer,
		// Token: 0x040004E8 RID: 1256
		CreateLinkedServerLogin,
		// Token: 0x040004E9 RID: 1257
		CreateMessage,
		// Token: 0x040004EA RID: 1258
		CreatePlanGuide,
		// Token: 0x040004EB RID: 1259
		CreateRule,
		// Token: 0x040004EC RID: 1260
		CreateRemoteServer,
		// Token: 0x040004ED RID: 1261
		DropDefault,
		// Token: 0x040004EE RID: 1262
		DropExtendedProcedure,
		// Token: 0x040004EF RID: 1263
		DropExtendedProperty,
		// Token: 0x040004F0 RID: 1264
		DropFullTextCatalog,
		// Token: 0x040004F1 RID: 1265
		DropFullTextIndex,
		// Token: 0x040004F2 RID: 1266
		DropLinkedServerLogin,
		// Token: 0x040004F3 RID: 1267
		DropMessage,
		// Token: 0x040004F4 RID: 1268
		DropPlanGuide,
		// Token: 0x040004F5 RID: 1269
		DropRule,
		// Token: 0x040004F6 RID: 1270
		DropRemoteServer,
		// Token: 0x040004F7 RID: 1271
		Rename,
		// Token: 0x040004F8 RID: 1272
		UnbindDefault,
		// Token: 0x040004F9 RID: 1273
		UnbindRule,
		// Token: 0x040004FA RID: 1274
		CreateSymmetricKey,
		// Token: 0x040004FB RID: 1275
		AlterSymmetricKey,
		// Token: 0x040004FC RID: 1276
		DropSymmetricKey,
		// Token: 0x040004FD RID: 1277
		CreateAsymmetricKey,
		// Token: 0x040004FE RID: 1278
		AlterAsymmetricKey,
		// Token: 0x040004FF RID: 1279
		DropAsymmetricKey,
		// Token: 0x04000500 RID: 1280
		AlterServiceMasterKey = 251,
		// Token: 0x04000501 RID: 1281
		CreateMasterKey,
		// Token: 0x04000502 RID: 1282
		AlterMasterKey,
		// Token: 0x04000503 RID: 1283
		DropMasterKey,
		// Token: 0x04000504 RID: 1284
		AddSignatureSchemaObject,
		// Token: 0x04000505 RID: 1285
		DropSignatureSchemaObject,
		// Token: 0x04000506 RID: 1286
		AddSignature,
		// Token: 0x04000507 RID: 1287
		DropSignature,
		// Token: 0x04000508 RID: 1288
		CreateCredential,
		// Token: 0x04000509 RID: 1289
		AlterCredential,
		// Token: 0x0400050A RID: 1290
		DropCredential,
		// Token: 0x0400050B RID: 1291
		DropLinkedServer,
		// Token: 0x0400050C RID: 1292
		AlterLinkedServer,
		// Token: 0x0400050D RID: 1293
		CreateEventSession,
		// Token: 0x0400050E RID: 1294
		AlterEventSession,
		// Token: 0x0400050F RID: 1295
		DropEventSession,
		// Token: 0x04000510 RID: 1296
		CreateResourcePool,
		// Token: 0x04000511 RID: 1297
		AlterResourcePool,
		// Token: 0x04000512 RID: 1298
		DropResourcePool,
		// Token: 0x04000513 RID: 1299
		CreateWorkloadGroup,
		// Token: 0x04000514 RID: 1300
		AlterWorkloadGroup,
		// Token: 0x04000515 RID: 1301
		DropWorkloadGroup,
		// Token: 0x04000516 RID: 1302
		AlterResourceGovernorConfig,
		// Token: 0x04000517 RID: 1303
		CreateSpatialIndex,
		// Token: 0x04000518 RID: 1304
		CreateCryptographicProvider,
		// Token: 0x04000519 RID: 1305
		AlterCryptographicProvider,
		// Token: 0x0400051A RID: 1306
		DropCryptographicProvider,
		// Token: 0x0400051B RID: 1307
		CreateDatabaseEncryptionKey,
		// Token: 0x0400051C RID: 1308
		AlterDatabaseEncryptionKey,
		// Token: 0x0400051D RID: 1309
		DropDatabaseEncryptionKey,
		// Token: 0x0400051E RID: 1310
		CreateBrokerPriority,
		// Token: 0x0400051F RID: 1311
		AlterBrokerPriority,
		// Token: 0x04000520 RID: 1312
		DropBrokerPriority,
		// Token: 0x04000521 RID: 1313
		CreateServerAudit,
		// Token: 0x04000522 RID: 1314
		AlterServerAudit,
		// Token: 0x04000523 RID: 1315
		DropServerAudit,
		// Token: 0x04000524 RID: 1316
		CreateServerAuditSpecification,
		// Token: 0x04000525 RID: 1317
		AlterServerAuditSpecification,
		// Token: 0x04000526 RID: 1318
		DropServerAuditSpecification,
		// Token: 0x04000527 RID: 1319
		CreateDatabaseAuditSpecification,
		// Token: 0x04000528 RID: 1320
		AlterDatabaseAuditSpecification,
		// Token: 0x04000529 RID: 1321
		DropDatabaseAuditSpecification,
		// Token: 0x0400052A RID: 1322
		CreateFullTextStopList,
		// Token: 0x0400052B RID: 1323
		AlterFullTextStopList,
		// Token: 0x0400052C RID: 1324
		DropFullTextStopList,
		// Token: 0x0400052D RID: 1325
		AlterServerConfiguration,
		// Token: 0x0400052E RID: 1326
		CreateSearchPropertyList,
		// Token: 0x0400052F RID: 1327
		AlterSearchPropertyList,
		// Token: 0x04000530 RID: 1328
		DropSearchPropertyList,
		// Token: 0x04000531 RID: 1329
		CreateServerRole,
		// Token: 0x04000532 RID: 1330
		AlterServerRole,
		// Token: 0x04000533 RID: 1331
		DropServerRole,
		// Token: 0x04000534 RID: 1332
		CreateSequence,
		// Token: 0x04000535 RID: 1333
		AlterSequence,
		// Token: 0x04000536 RID: 1334
		DropSequence,
		// Token: 0x04000537 RID: 1335
		CreateAvailabilityGroup,
		// Token: 0x04000538 RID: 1336
		AlterAvailabilityGroup,
		// Token: 0x04000539 RID: 1337
		DropAvailabilityGroup,
		// Token: 0x0400053A RID: 1338
		AuditLogin = 1014,
		// Token: 0x0400053B RID: 1339
		AuditLogout,
		// Token: 0x0400053C RID: 1340
		AuditLoginFailed = 1020,
		// Token: 0x0400053D RID: 1341
		EventLog,
		// Token: 0x0400053E RID: 1342
		ErrorLog,
		// Token: 0x0400053F RID: 1343
		LockDeadlock = 1025,
		// Token: 0x04000540 RID: 1344
		Exception = 1033,
		// Token: 0x04000541 RID: 1345
		SpCacheMiss,
		// Token: 0x04000542 RID: 1346
		SpCacheInsert,
		// Token: 0x04000543 RID: 1347
		SpCacheRemove,
		// Token: 0x04000544 RID: 1348
		SpRecompile,
		// Token: 0x04000545 RID: 1349
		ObjectCreated = 1046,
		// Token: 0x04000546 RID: 1350
		ObjectDeleted,
		// Token: 0x04000547 RID: 1351
		HashWarning = 1055,
		// Token: 0x04000548 RID: 1352
		LockDeadlockChain = 1059,
		// Token: 0x04000549 RID: 1353
		LockEscalation,
		// Token: 0x0400054A RID: 1354
		OledbErrors,
		// Token: 0x0400054B RID: 1355
		ExecutionWarnings = 1067,
		// Token: 0x0400054C RID: 1356
		SortWarnings = 1069,
		// Token: 0x0400054D RID: 1357
		MissingColumnStatistics = 1079,
		// Token: 0x0400054E RID: 1358
		MissingJoinPredicate,
		// Token: 0x0400054F RID: 1359
		ServerMemoryChange,
		// Token: 0x04000550 RID: 1360
		UserConfigurable0,
		// Token: 0x04000551 RID: 1361
		UserConfigurable1,
		// Token: 0x04000552 RID: 1362
		UserConfigurable2,
		// Token: 0x04000553 RID: 1363
		UserConfigurable3,
		// Token: 0x04000554 RID: 1364
		UserConfigurable4,
		// Token: 0x04000555 RID: 1365
		UserConfigurable5,
		// Token: 0x04000556 RID: 1366
		UserConfigurable6,
		// Token: 0x04000557 RID: 1367
		UserConfigurable7,
		// Token: 0x04000558 RID: 1368
		UserConfigurable8,
		// Token: 0x04000559 RID: 1369
		UserConfigurable9,
		// Token: 0x0400055A RID: 1370
		DataFileAutoGrow,
		// Token: 0x0400055B RID: 1371
		LogFileAutoGrow,
		// Token: 0x0400055C RID: 1372
		DataFileAutoShrink,
		// Token: 0x0400055D RID: 1373
		LogFileAutoShrink,
		// Token: 0x0400055E RID: 1374
		AuditDatabaseScopeGdrEvent = 1102,
		// Token: 0x0400055F RID: 1375
		AuditSchemaObjectGdrEvent,
		// Token: 0x04000560 RID: 1376
		AuditAddLoginEvent,
		// Token: 0x04000561 RID: 1377
		AuditLoginGdrEvent,
		// Token: 0x04000562 RID: 1378
		AuditLoginChangePropertyEvent,
		// Token: 0x04000563 RID: 1379
		AuditLoginChangePasswordEvent,
		// Token: 0x04000564 RID: 1380
		AuditAddLoginToServerRoleEvent,
		// Token: 0x04000565 RID: 1381
		AuditAddDBUserEvent,
		// Token: 0x04000566 RID: 1382
		AuditAddMemberToDBRoleEvent,
		// Token: 0x04000567 RID: 1383
		AuditAddRoleEvent,
		// Token: 0x04000568 RID: 1384
		AuditAppRoleChangePasswordEvent,
		// Token: 0x04000569 RID: 1385
		AuditSchemaObjectAccessEvent = 1114,
		// Token: 0x0400056A RID: 1386
		AuditBackupRestoreEvent,
		// Token: 0x0400056B RID: 1387
		AuditDbccEvent,
		// Token: 0x0400056C RID: 1388
		AuditChangeAuditEvent,
		// Token: 0x0400056D RID: 1389
		OledbCallEvent = 1119,
		// Token: 0x0400056E RID: 1390
		OledbQueryInterfaceEvent,
		// Token: 0x0400056F RID: 1391
		OledbDataReadEvent,
		// Token: 0x04000570 RID: 1392
		ShowPlanXml,
		// Token: 0x04000571 RID: 1393
		DeprecationAnnouncement = 1125,
		// Token: 0x04000572 RID: 1394
		DeprecationFinalSupport,
		// Token: 0x04000573 RID: 1395
		ExchangeSpillEvent,
		// Token: 0x04000574 RID: 1396
		AuditDatabaseManagementEvent,
		// Token: 0x04000575 RID: 1397
		AuditDatabaseObjectManagementEvent,
		// Token: 0x04000576 RID: 1398
		AuditDatabasePrincipalManagementEvent,
		// Token: 0x04000577 RID: 1399
		AuditSchemaObjectManagementEvent,
		// Token: 0x04000578 RID: 1400
		AuditServerPrincipalImpersonationEvent,
		// Token: 0x04000579 RID: 1401
		AuditDatabasePrincipalImpersonationEvent,
		// Token: 0x0400057A RID: 1402
		AuditServerObjectTakeOwnershipEvent,
		// Token: 0x0400057B RID: 1403
		AuditDatabaseObjectTakeOwnershipEvent,
		// Token: 0x0400057C RID: 1404
		BlockedProcessReport = 1137,
		// Token: 0x0400057D RID: 1405
		ShowPlanXmlStatisticsProfile = 1146,
		// Token: 0x0400057E RID: 1406
		DeadlockGraph = 1148,
		// Token: 0x0400057F RID: 1407
		TraceFileClose = 1150,
		// Token: 0x04000580 RID: 1408
		AuditChangeDatabaseOwner = 1152,
		// Token: 0x04000581 RID: 1409
		AuditSchemaObjectTakeOwnershipEvent,
		// Token: 0x04000582 RID: 1410
		FtCrawlStarted = 1155,
		// Token: 0x04000583 RID: 1411
		FtCrawlStopped,
		// Token: 0x04000584 RID: 1412
		FtCrawlAborted,
		// Token: 0x04000585 RID: 1413
		UserErrorMessage = 1162,
		// Token: 0x04000586 RID: 1414
		ObjectAltered = 1164,
		// Token: 0x04000587 RID: 1415
		SqlStmtRecompile = 1166,
		// Token: 0x04000588 RID: 1416
		DatabaseMirroringStateChange,
		// Token: 0x04000589 RID: 1417
		ShowPlanXmlForQueryCompile,
		// Token: 0x0400058A RID: 1418
		ShowPlanAllForQueryCompile,
		// Token: 0x0400058B RID: 1419
		AuditServerScopeGdrEvent,
		// Token: 0x0400058C RID: 1420
		AuditServerObjectGdrEvent,
		// Token: 0x0400058D RID: 1421
		AuditDatabaseObjectGdrEvent,
		// Token: 0x0400058E RID: 1422
		AuditServerOperationEvent,
		// Token: 0x0400058F RID: 1423
		AuditServerAlterTraceEvent = 1175,
		// Token: 0x04000590 RID: 1424
		AuditServerObjectManagementEvent,
		// Token: 0x04000591 RID: 1425
		AuditServerPrincipalManagementEvent,
		// Token: 0x04000592 RID: 1426
		AuditDatabaseOperationEvent,
		// Token: 0x04000593 RID: 1427
		AuditDatabaseObjectAccessEvent = 1180,
		// Token: 0x04000594 RID: 1428
		OledbProviderInformation = 1194,
		// Token: 0x04000595 RID: 1429
		MountTape,
		// Token: 0x04000596 RID: 1430
		AssemblyLoad,
		// Token: 0x04000597 RID: 1431
		XQueryStaticType = 1198,
		// Token: 0x04000598 RID: 1432
		QnSubscription,
		// Token: 0x04000599 RID: 1433
		QnParameterTable,
		// Token: 0x0400059A RID: 1434
		QnTemplate,
		// Token: 0x0400059B RID: 1435
		QnDynamics,
		// Token: 0x0400059C RID: 1436
		BitmapWarning = 1212,
		// Token: 0x0400059D RID: 1437
		DatabaseSuspectDataPage,
		// Token: 0x0400059E RID: 1438
		CpuThresholdExceeded,
		// Token: 0x0400059F RID: 1439
		AuditFullText = 1235
	}
}
