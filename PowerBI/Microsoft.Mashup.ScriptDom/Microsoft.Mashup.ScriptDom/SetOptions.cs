using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200016E RID: 366
	[Flags]
	[Serializable]
	internal enum SetOptions
	{
		// Token: 0x040018D9 RID: 6361
		None = 0,
		// Token: 0x040018DA RID: 6362
		QuotedIdentifier = 1,
		// Token: 0x040018DB RID: 6363
		ConcatNullYieldsNull = 2,
		// Token: 0x040018DC RID: 6364
		CursorCloseOnCommit = 4,
		// Token: 0x040018DD RID: 6365
		ArithAbort = 8,
		// Token: 0x040018DE RID: 6366
		ArithIgnore = 16,
		// Token: 0x040018DF RID: 6367
		FmtOnly = 32,
		// Token: 0x040018E0 RID: 6368
		NoCount = 64,
		// Token: 0x040018E1 RID: 6369
		NoExec = 128,
		// Token: 0x040018E2 RID: 6370
		NumericRoundAbort = 256,
		// Token: 0x040018E3 RID: 6371
		ParseOnly = 512,
		// Token: 0x040018E4 RID: 6372
		AnsiDefaults = 1024,
		// Token: 0x040018E5 RID: 6373
		AnsiNullDfltOff = 2048,
		// Token: 0x040018E6 RID: 6374
		AnsiNullDfltOn = 4096,
		// Token: 0x040018E7 RID: 6375
		AnsiNulls = 8192,
		// Token: 0x040018E8 RID: 6376
		AnsiPadding = 16384,
		// Token: 0x040018E9 RID: 6377
		AnsiWarnings = 32768,
		// Token: 0x040018EA RID: 6378
		ForcePlan = 65536,
		// Token: 0x040018EB RID: 6379
		ShowPlanAll = 131072,
		// Token: 0x040018EC RID: 6380
		ShowPlanText = 262144,
		// Token: 0x040018ED RID: 6381
		ImplicitTransactions = 524288,
		// Token: 0x040018EE RID: 6382
		RemoteProcTransactions = 1048576,
		// Token: 0x040018EF RID: 6383
		XactAbort = 2097152,
		// Token: 0x040018F0 RID: 6384
		DisableDefCnstChk = 4194304,
		// Token: 0x040018F1 RID: 6385
		ShowPlanXml = 8388608,
		// Token: 0x040018F2 RID: 6386
		NoBrowsetable = 16777216
	}
}
