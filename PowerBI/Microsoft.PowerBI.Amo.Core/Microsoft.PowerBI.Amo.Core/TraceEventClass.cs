using System;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices
{
	// Token: 0x020000C3 RID: 195
	[Guid("D9B6D3BB-7CFF-4142-8447-E099248E9938")]
	public enum TraceEventClass
	{
		// Token: 0x0400054E RID: 1358
		NotAvailable,
		// Token: 0x0400054F RID: 1359
		AuditLogin,
		// Token: 0x04000550 RID: 1360
		AuditLogout,
		// Token: 0x04000551 RID: 1361
		AuditServerStartsAndStops = 4,
		// Token: 0x04000552 RID: 1362
		AuditObjectPermission = 18,
		// Token: 0x04000553 RID: 1363
		AuditAdminOperations,
		// Token: 0x04000554 RID: 1364
		ProgressReportBegin = 5,
		// Token: 0x04000555 RID: 1365
		ProgressReportEnd,
		// Token: 0x04000556 RID: 1366
		ProgressReportCurrent,
		// Token: 0x04000557 RID: 1367
		ProgressReportError,
		// Token: 0x04000558 RID: 1368
		QueryBegin,
		// Token: 0x04000559 RID: 1369
		QueryEnd,
		// Token: 0x0400055A RID: 1370
		CommandBegin = 15,
		// Token: 0x0400055B RID: 1371
		CommandEnd,
		// Token: 0x0400055C RID: 1372
		Error,
		// Token: 0x0400055D RID: 1373
		ServerStateDiscoverBegin = 33,
		// Token: 0x0400055E RID: 1374
		ServerStateDiscoverData,
		// Token: 0x0400055F RID: 1375
		ServerStateDiscoverEnd,
		// Token: 0x04000560 RID: 1376
		DiscoverBegin,
		// Token: 0x04000561 RID: 1377
		DiscoverEnd = 38,
		// Token: 0x04000562 RID: 1378
		Notification,
		// Token: 0x04000563 RID: 1379
		UserDefined,
		// Token: 0x04000564 RID: 1380
		ExistingConnection,
		// Token: 0x04000565 RID: 1381
		ExistingSession,
		// Token: 0x04000566 RID: 1382
		SessionInitialize,
		// Token: 0x04000567 RID: 1383
		Deadlock = 50,
		// Token: 0x04000568 RID: 1384
		LockTimeout,
		// Token: 0x04000569 RID: 1385
		LockAcquired,
		// Token: 0x0400056A RID: 1386
		LockReleased,
		// Token: 0x0400056B RID: 1387
		LockWaiting,
		// Token: 0x0400056C RID: 1388
		QueryCubeBegin = 70,
		// Token: 0x0400056D RID: 1389
		QueryCubeEnd,
		// Token: 0x0400056E RID: 1390
		CalculateNonEmptyBegin,
		// Token: 0x0400056F RID: 1391
		CalculateNonEmptyCurrent,
		// Token: 0x04000570 RID: 1392
		CalculateNonEmptyEnd,
		// Token: 0x04000571 RID: 1393
		SerializeResultsBegin,
		// Token: 0x04000572 RID: 1394
		SerializeResultsCurrent,
		// Token: 0x04000573 RID: 1395
		SerializeResultsEnd,
		// Token: 0x04000574 RID: 1396
		ExecuteMdxScriptBegin,
		// Token: 0x04000575 RID: 1397
		ExecuteMdxScriptCurrent,
		// Token: 0x04000576 RID: 1398
		ExecuteMdxScriptError = 126,
		// Token: 0x04000577 RID: 1399
		ExecuteMdxScriptEnd = 80,
		// Token: 0x04000578 RID: 1400
		QueryDimension,
		// Token: 0x04000579 RID: 1401
		QuerySubcube = 11,
		// Token: 0x0400057A RID: 1402
		QuerySubcubeVerbose,
		// Token: 0x0400057B RID: 1403
		GetDataFromAggregation = 60,
		// Token: 0x0400057C RID: 1404
		GetDataFromCache,
		// Token: 0x0400057D RID: 1405
		VertiPaqSEQueryBegin = 82,
		// Token: 0x0400057E RID: 1406
		VertiPaqSEQueryEnd,
		// Token: 0x0400057F RID: 1407
		ResourceUsage,
		// Token: 0x04000580 RID: 1408
		VertiPaqSEQueryCacheMatch,
		// Token: 0x04000581 RID: 1409
		VertiPaqSEQueryCacheMiss,
		// Token: 0x04000582 RID: 1410
		DirectQueryBegin = 98,
		// Token: 0x04000583 RID: 1411
		DirectQueryEnd,
		// Token: 0x04000584 RID: 1412
		CalculationEvaluation = 110,
		// Token: 0x04000585 RID: 1413
		CalculationEvaluationDetailedInformation,
		// Token: 0x04000586 RID: 1414
		DAXQueryPlan,
		// Token: 0x04000587 RID: 1415
		DAXExtensionExecutionBegin = 120,
		// Token: 0x04000588 RID: 1416
		DAXExtensionExecutionEnd,
		// Token: 0x04000589 RID: 1417
		DAXExtensionTraceError,
		// Token: 0x0400058A RID: 1418
		DAXExtensionTraceInfo,
		// Token: 0x0400058B RID: 1419
		DAXExtensionTraceVerbose,
		// Token: 0x0400058C RID: 1420
		AggregateTableRewriteQuery = 131,
		// Token: 0x0400058D RID: 1421
		AggregateTableRewriteInfo,
		// Token: 0x0400058E RID: 1422
		DAXQueryShape,
		// Token: 0x0400058F RID: 1423
		DAXEvaluationLog = 135,
		// Token: 0x04000590 RID: 1424
		FileLoadBegin = 90,
		// Token: 0x04000591 RID: 1425
		FileLoadEnd,
		// Token: 0x04000592 RID: 1426
		FileSaveBegin,
		// Token: 0x04000593 RID: 1427
		FileSaveEnd,
		// Token: 0x04000594 RID: 1428
		PageOutBegin,
		// Token: 0x04000595 RID: 1429
		PageOutEnd,
		// Token: 0x04000596 RID: 1430
		PageInBegin,
		// Token: 0x04000597 RID: 1431
		PageInEnd,
		// Token: 0x04000598 RID: 1432
		WLGroupCPUThrottling = 113,
		// Token: 0x04000599 RID: 1433
		WLGroupExceedsMemoryLimit,
		// Token: 0x0400059A RID: 1434
		WLGroupExceedsProcessingLimit,
		// Token: 0x0400059B RID: 1435
		ExecuteSourceQuery = 130,
		// Token: 0x0400059C RID: 1436
		JobGraph = 134,
		// Token: 0x0400059D RID: 1437
		ExecutionMetrics = 136
	}
}
