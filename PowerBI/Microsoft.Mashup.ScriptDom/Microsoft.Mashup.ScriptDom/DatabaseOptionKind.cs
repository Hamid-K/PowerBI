using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200007C RID: 124
	internal enum DatabaseOptionKind
	{
		// Token: 0x040002CC RID: 716
		Online,
		// Token: 0x040002CD RID: 717
		Offline,
		// Token: 0x040002CE RID: 718
		Emergency,
		// Token: 0x040002CF RID: 719
		SingleUser,
		// Token: 0x040002D0 RID: 720
		RestrictedUser,
		// Token: 0x040002D1 RID: 721
		MultiUser,
		// Token: 0x040002D2 RID: 722
		ReadOnly,
		// Token: 0x040002D3 RID: 723
		ReadWrite,
		// Token: 0x040002D4 RID: 724
		EnableBroker,
		// Token: 0x040002D5 RID: 725
		DisableBroker,
		// Token: 0x040002D6 RID: 726
		NewBroker,
		// Token: 0x040002D7 RID: 727
		ErrorBrokerConversations,
		// Token: 0x040002D8 RID: 728
		DBChaining,
		// Token: 0x040002D9 RID: 729
		Trustworthy,
		// Token: 0x040002DA RID: 730
		CursorCloseOnCommit,
		// Token: 0x040002DB RID: 731
		AutoClose,
		// Token: 0x040002DC RID: 732
		AutoCreateStatistics,
		// Token: 0x040002DD RID: 733
		AutoShrink,
		// Token: 0x040002DE RID: 734
		AutoUpdateStatistics,
		// Token: 0x040002DF RID: 735
		AutoUpdateStatisticsAsync,
		// Token: 0x040002E0 RID: 736
		AnsiNullDefault,
		// Token: 0x040002E1 RID: 737
		AnsiNulls,
		// Token: 0x040002E2 RID: 738
		AnsiPadding,
		// Token: 0x040002E3 RID: 739
		AnsiWarnings,
		// Token: 0x040002E4 RID: 740
		ArithAbort,
		// Token: 0x040002E5 RID: 741
		ConcatNullYieldsNull,
		// Token: 0x040002E6 RID: 742
		NumericRoundAbort,
		// Token: 0x040002E7 RID: 743
		QuotedIdentifier,
		// Token: 0x040002E8 RID: 744
		RecursiveTriggers,
		// Token: 0x040002E9 RID: 745
		TornPageDetection,
		// Token: 0x040002EA RID: 746
		DateCorrelationOptimization,
		// Token: 0x040002EB RID: 747
		AllowSnapshotIsolation,
		// Token: 0x040002EC RID: 748
		ReadCommittedSnapshot,
		// Token: 0x040002ED RID: 749
		Encryption,
		// Token: 0x040002EE RID: 750
		HonorBrokerPriority,
		// Token: 0x040002EF RID: 751
		VarDecimalStorageFormat,
		// Token: 0x040002F0 RID: 752
		SupplementalLogging,
		// Token: 0x040002F1 RID: 753
		CompatibilityLevel,
		// Token: 0x040002F2 RID: 754
		CursorDefault,
		// Token: 0x040002F3 RID: 755
		Recovery,
		// Token: 0x040002F4 RID: 756
		PageVerify,
		// Token: 0x040002F5 RID: 757
		Partner,
		// Token: 0x040002F6 RID: 758
		Witness,
		// Token: 0x040002F7 RID: 759
		Parameterization,
		// Token: 0x040002F8 RID: 760
		ChangeTracking,
		// Token: 0x040002F9 RID: 761
		DefaultLanguage,
		// Token: 0x040002FA RID: 762
		DefaultFullTextLanguage,
		// Token: 0x040002FB RID: 763
		NestedTriggers,
		// Token: 0x040002FC RID: 764
		TransformNoiseWords,
		// Token: 0x040002FD RID: 765
		TwoDigitYearCutoff,
		// Token: 0x040002FE RID: 766
		Containment,
		// Token: 0x040002FF RID: 767
		Hadr,
		// Token: 0x04000300 RID: 768
		FileStream,
		// Token: 0x04000301 RID: 769
		Edition,
		// Token: 0x04000302 RID: 770
		MaxSize,
		// Token: 0x04000303 RID: 771
		TargetRecoveryTime
	}
}
