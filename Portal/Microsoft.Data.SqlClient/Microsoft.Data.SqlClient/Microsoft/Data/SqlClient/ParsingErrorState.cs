using System;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000A7 RID: 167
	internal enum ParsingErrorState
	{
		// Token: 0x0400051B RID: 1307
		Undefined,
		// Token: 0x0400051C RID: 1308
		FedAuthInfoLengthTooShortForCountOfInfoIds,
		// Token: 0x0400051D RID: 1309
		FedAuthInfoLengthTooShortForData,
		// Token: 0x0400051E RID: 1310
		FedAuthInfoFailedToReadCountOfInfoIds,
		// Token: 0x0400051F RID: 1311
		FedAuthInfoFailedToReadTokenStream,
		// Token: 0x04000520 RID: 1312
		FedAuthInfoInvalidOffset,
		// Token: 0x04000521 RID: 1313
		FedAuthInfoFailedToReadData,
		// Token: 0x04000522 RID: 1314
		FedAuthInfoDataNotUnicode,
		// Token: 0x04000523 RID: 1315
		FedAuthInfoDoesNotContainStsurlAndSpn,
		// Token: 0x04000524 RID: 1316
		FedAuthInfoNotReceived,
		// Token: 0x04000525 RID: 1317
		FedAuthNotAcknowledged,
		// Token: 0x04000526 RID: 1318
		FedAuthFeatureAckContainsExtraData,
		// Token: 0x04000527 RID: 1319
		FedAuthFeatureAckUnknownLibraryType,
		// Token: 0x04000528 RID: 1320
		UnrequestedFeatureAckReceived,
		// Token: 0x04000529 RID: 1321
		UnknownFeatureAck,
		// Token: 0x0400052A RID: 1322
		InvalidTdsTokenReceived,
		// Token: 0x0400052B RID: 1323
		SessionStateLengthTooShort,
		// Token: 0x0400052C RID: 1324
		SessionStateInvalidStatus,
		// Token: 0x0400052D RID: 1325
		CorruptedTdsStream,
		// Token: 0x0400052E RID: 1326
		ProcessSniPacketFailed,
		// Token: 0x0400052F RID: 1327
		FedAuthRequiredPreLoginResponseInvalidValue,
		// Token: 0x04000530 RID: 1328
		TceUnknownVersion,
		// Token: 0x04000531 RID: 1329
		TceInvalidVersion,
		// Token: 0x04000532 RID: 1330
		TceInvalidOrdinalIntoCipherInfoTable,
		// Token: 0x04000533 RID: 1331
		DataClassificationInvalidVersion,
		// Token: 0x04000534 RID: 1332
		DataClassificationNotExpected,
		// Token: 0x04000535 RID: 1333
		DataClassificationInvalidLabelIndex,
		// Token: 0x04000536 RID: 1334
		DataClassificationInvalidInformationTypeIndex
	}
}
