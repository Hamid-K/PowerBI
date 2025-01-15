using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020000C5 RID: 197
	[Serializable]
	internal enum TableHintKind
	{
		// Token: 0x040005E6 RID: 1510
		None,
		// Token: 0x040005E7 RID: 1511
		FastFirstRow,
		// Token: 0x040005E8 RID: 1512
		HoldLock,
		// Token: 0x040005E9 RID: 1513
		NoLock,
		// Token: 0x040005EA RID: 1514
		PagLock,
		// Token: 0x040005EB RID: 1515
		ReadCommitted,
		// Token: 0x040005EC RID: 1516
		ReadPast,
		// Token: 0x040005ED RID: 1517
		ReadUncommitted,
		// Token: 0x040005EE RID: 1518
		RepeatableRead,
		// Token: 0x040005EF RID: 1519
		Rowlock,
		// Token: 0x040005F0 RID: 1520
		Serializable,
		// Token: 0x040005F1 RID: 1521
		TabLock,
		// Token: 0x040005F2 RID: 1522
		TabLockX,
		// Token: 0x040005F3 RID: 1523
		UpdLock,
		// Token: 0x040005F4 RID: 1524
		XLock,
		// Token: 0x040005F5 RID: 1525
		NoExpand,
		// Token: 0x040005F6 RID: 1526
		NoWait,
		// Token: 0x040005F7 RID: 1527
		ReadCommittedLock,
		// Token: 0x040005F8 RID: 1528
		KeepIdentity,
		// Token: 0x040005F9 RID: 1529
		KeepDefaults,
		// Token: 0x040005FA RID: 1530
		IgnoreConstraints,
		// Token: 0x040005FB RID: 1531
		IgnoreTriggers,
		// Token: 0x040005FC RID: 1532
		ForceSeek,
		// Token: 0x040005FD RID: 1533
		Index,
		// Token: 0x040005FE RID: 1534
		SpatialWindowMaxCells,
		// Token: 0x040005FF RID: 1535
		ForceScan
	}
}
