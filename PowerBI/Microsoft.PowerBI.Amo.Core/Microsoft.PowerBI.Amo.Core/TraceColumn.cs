using System;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices
{
	// Token: 0x020000C2 RID: 194
	[Guid("46080753-2524-4faf-8BD1-EDFDA1085209")]
	public enum TraceColumn
	{
		// Token: 0x04000521 RID: 1313
		EventClass,
		// Token: 0x04000522 RID: 1314
		EventSubclass,
		// Token: 0x04000523 RID: 1315
		CurrentTime,
		// Token: 0x04000524 RID: 1316
		StartTime,
		// Token: 0x04000525 RID: 1317
		EndTime,
		// Token: 0x04000526 RID: 1318
		Duration,
		// Token: 0x04000527 RID: 1319
		CpuTime,
		// Token: 0x04000528 RID: 1320
		JobID,
		// Token: 0x04000529 RID: 1321
		SessionType,
		// Token: 0x0400052A RID: 1322
		ProgressTotal,
		// Token: 0x0400052B RID: 1323
		IntegerData,
		// Token: 0x0400052C RID: 1324
		ObjectID,
		// Token: 0x0400052D RID: 1325
		ObjectType,
		// Token: 0x0400052E RID: 1326
		ObjectName,
		// Token: 0x0400052F RID: 1327
		ObjectPath,
		// Token: 0x04000530 RID: 1328
		ObjectReference,
		// Token: 0x04000531 RID: 1329
		Severity = 22,
		// Token: 0x04000532 RID: 1330
		Success,
		// Token: 0x04000533 RID: 1331
		Error,
		// Token: 0x04000534 RID: 1332
		ConnectionID,
		// Token: 0x04000535 RID: 1333
		DatabaseName = 28,
		// Token: 0x04000536 RID: 1334
		NTUserName = 32,
		// Token: 0x04000537 RID: 1335
		NTDomainName,
		// Token: 0x04000538 RID: 1336
		ClientHostName = 35,
		// Token: 0x04000539 RID: 1337
		ClientProcessID,
		// Token: 0x0400053A RID: 1338
		ApplicationName,
		// Token: 0x0400053B RID: 1339
		SessionID = 39,
		// Token: 0x0400053C RID: 1340
		NTCanonicalUserName,
		// Token: 0x0400053D RID: 1341
		Spid,
		// Token: 0x0400053E RID: 1342
		TextData,
		// Token: 0x0400053F RID: 1343
		ServerName,
		// Token: 0x04000540 RID: 1344
		RequestParameters,
		// Token: 0x04000541 RID: 1345
		RequestProperties,
		// Token: 0x04000542 RID: 1346
		ActivityID,
		// Token: 0x04000543 RID: 1347
		RequestID,
		// Token: 0x04000544 RID: 1348
		CalculationExpression,
		// Token: 0x04000545 RID: 1349
		ErrorType,
		// Token: 0x04000546 RID: 1350
		FunctionName,
		// Token: 0x04000547 RID: 1351
		UserObjectID,
		// Token: 0x04000548 RID: 1352
		ApplicationContext,
		// Token: 0x04000549 RID: 1353
		[Obsolete("Deprecated!")]
		GraphCorrelationID,
		// Token: 0x0400054A RID: 1354
		DatabaseFriendlyName,
		// Token: 0x0400054B RID: 1355
		Identity,
		// Token: 0x0400054C RID: 1356
		Label
	}
}
