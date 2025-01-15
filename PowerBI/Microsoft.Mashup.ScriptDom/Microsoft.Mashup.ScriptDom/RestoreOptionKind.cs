using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000141 RID: 321
	[Serializable]
	internal enum RestoreOptionKind
	{
		// Token: 0x040011AD RID: 4525
		NoLog = 1,
		// Token: 0x040011AE RID: 4526
		Checksum,
		// Token: 0x040011AF RID: 4527
		NoChecksum,
		// Token: 0x040011B0 RID: 4528
		ContinueAfterError,
		// Token: 0x040011B1 RID: 4529
		StopOnError,
		// Token: 0x040011B2 RID: 4530
		Unload,
		// Token: 0x040011B3 RID: 4531
		NoUnload,
		// Token: 0x040011B4 RID: 4532
		Rewind,
		// Token: 0x040011B5 RID: 4533
		NoRewind,
		// Token: 0x040011B6 RID: 4534
		NoRecovery,
		// Token: 0x040011B7 RID: 4535
		Recovery,
		// Token: 0x040011B8 RID: 4536
		Replace,
		// Token: 0x040011B9 RID: 4537
		Restart,
		// Token: 0x040011BA RID: 4538
		Verbose,
		// Token: 0x040011BB RID: 4539
		LoadHistory,
		// Token: 0x040011BC RID: 4540
		DboOnly,
		// Token: 0x040011BD RID: 4541
		RestrictedUser,
		// Token: 0x040011BE RID: 4542
		Partial,
		// Token: 0x040011BF RID: 4543
		Snapshot,
		// Token: 0x040011C0 RID: 4544
		KeepReplication,
		// Token: 0x040011C1 RID: 4545
		Online,
		// Token: 0x040011C2 RID: 4546
		CommitDifferentialBase,
		// Token: 0x040011C3 RID: 4547
		SnapshotImport,
		// Token: 0x040011C4 RID: 4548
		EnableBroker,
		// Token: 0x040011C5 RID: 4549
		NewBroker,
		// Token: 0x040011C6 RID: 4550
		ErrorBrokerConversations,
		// Token: 0x040011C7 RID: 4551
		Stats,
		// Token: 0x040011C8 RID: 4552
		File,
		// Token: 0x040011C9 RID: 4553
		StopAt,
		// Token: 0x040011CA RID: 4554
		MediaName,
		// Token: 0x040011CB RID: 4555
		MediaPassword,
		// Token: 0x040011CC RID: 4556
		Password,
		// Token: 0x040011CD RID: 4557
		BlockSize,
		// Token: 0x040011CE RID: 4558
		BufferCount,
		// Token: 0x040011CF RID: 4559
		MaxTransferSize,
		// Token: 0x040011D0 RID: 4560
		Standby,
		// Token: 0x040011D1 RID: 4561
		EnhancedIntegrity,
		// Token: 0x040011D2 RID: 4562
		SnapshotRestorePhase,
		// Token: 0x040011D3 RID: 4563
		Move,
		// Token: 0x040011D4 RID: 4564
		Stop,
		// Token: 0x040011D5 RID: 4565
		FileStream = 50
	}
}
