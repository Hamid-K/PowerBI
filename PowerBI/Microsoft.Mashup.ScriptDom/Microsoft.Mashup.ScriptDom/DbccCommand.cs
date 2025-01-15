using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020000EA RID: 234
	internal enum DbccCommand
	{
		// Token: 0x04000A53 RID: 2643
		None,
		// Token: 0x04000A54 RID: 2644
		ActiveCursors,
		// Token: 0x04000A55 RID: 2645
		AddExtendedProc,
		// Token: 0x04000A56 RID: 2646
		AddInstance,
		// Token: 0x04000A57 RID: 2647
		AuditEvent,
		// Token: 0x04000A58 RID: 2648
		AutoPilot,
		// Token: 0x04000A59 RID: 2649
		Buffer,
		// Token: 0x04000A5A RID: 2650
		Bytes,
		// Token: 0x04000A5B RID: 2651
		CacheProfile,
		// Token: 0x04000A5C RID: 2652
		CacheStats,
		// Token: 0x04000A5D RID: 2653
		CallFullText,
		// Token: 0x04000A5E RID: 2654
		CheckAlloc,
		// Token: 0x04000A5F RID: 2655
		CheckCatalog,
		// Token: 0x04000A60 RID: 2656
		CheckConstraints,
		// Token: 0x04000A61 RID: 2657
		CheckDB,
		// Token: 0x04000A62 RID: 2658
		CheckFileGroup,
		// Token: 0x04000A63 RID: 2659
		CheckIdent,
		// Token: 0x04000A64 RID: 2660
		CheckPrimaryFile,
		// Token: 0x04000A65 RID: 2661
		CheckTable,
		// Token: 0x04000A66 RID: 2662
		CleanTable,
		// Token: 0x04000A67 RID: 2663
		ClearSpaceCaches,
		// Token: 0x04000A68 RID: 2664
		CollectStats,
		// Token: 0x04000A69 RID: 2665
		ConcurrencyViolation,
		// Token: 0x04000A6A RID: 2666
		CursorStats,
		// Token: 0x04000A6B RID: 2667
		DBRecover,
		// Token: 0x04000A6C RID: 2668
		DBReindex,
		// Token: 0x04000A6D RID: 2669
		DBReindexAll,
		// Token: 0x04000A6E RID: 2670
		DBRepair,
		// Token: 0x04000A6F RID: 2671
		DebugBreak,
		// Token: 0x04000A70 RID: 2672
		DeleteInstance,
		// Token: 0x04000A71 RID: 2673
		DetachDB,
		// Token: 0x04000A72 RID: 2674
		DropCleanBuffers,
		// Token: 0x04000A73 RID: 2675
		DropExtendedProc,
		// Token: 0x04000A74 RID: 2676
		DumpConfig,
		// Token: 0x04000A75 RID: 2677
		DumpDBInfo,
		// Token: 0x04000A76 RID: 2678
		DumpDBTable,
		// Token: 0x04000A77 RID: 2679
		DumpLock,
		// Token: 0x04000A78 RID: 2680
		DumpLog,
		// Token: 0x04000A79 RID: 2681
		DumpPage,
		// Token: 0x04000A7A RID: 2682
		DumpResource,
		// Token: 0x04000A7B RID: 2683
		DumpTrigger,
		// Token: 0x04000A7C RID: 2684
		ErrorLog,
		// Token: 0x04000A7D RID: 2685
		ExtentInfo,
		// Token: 0x04000A7E RID: 2686
		FileHeader,
		// Token: 0x04000A7F RID: 2687
		FixAllocation,
		// Token: 0x04000A80 RID: 2688
		Flush,
		// Token: 0x04000A81 RID: 2689
		FlushProcInDB,
		// Token: 0x04000A82 RID: 2690
		ForceGhostCleanup,
		// Token: 0x04000A83 RID: 2691
		Free,
		// Token: 0x04000A84 RID: 2692
		FreeProcCache,
		// Token: 0x04000A85 RID: 2693
		FreeSessionCache,
		// Token: 0x04000A86 RID: 2694
		FreeSystemCache,
		// Token: 0x04000A87 RID: 2695
		FreezeIO,
		// Token: 0x04000A88 RID: 2696
		Help,
		// Token: 0x04000A89 RID: 2697
		IcecapQuery,
		// Token: 0x04000A8A RID: 2698
		IncrementInstance,
		// Token: 0x04000A8B RID: 2699
		Ind,
		// Token: 0x04000A8C RID: 2700
		IndexDefrag,
		// Token: 0x04000A8D RID: 2701
		InputBuffer,
		// Token: 0x04000A8E RID: 2702
		InvalidateTextptr,
		// Token: 0x04000A8F RID: 2703
		InvalidateTextptrObjid,
		// Token: 0x04000A90 RID: 2704
		Latch,
		// Token: 0x04000A91 RID: 2705
		LogInfo,
		// Token: 0x04000A92 RID: 2706
		MapAllocUnit,
		// Token: 0x04000A93 RID: 2707
		MemObjList,
		// Token: 0x04000A94 RID: 2708
		MemoryMap,
		// Token: 0x04000A95 RID: 2709
		MemoryStatus,
		// Token: 0x04000A96 RID: 2710
		Metadata,
		// Token: 0x04000A97 RID: 2711
		MovePage,
		// Token: 0x04000A98 RID: 2712
		NoTextptr,
		// Token: 0x04000A99 RID: 2713
		OpenTran,
		// Token: 0x04000A9A RID: 2714
		OptimizerWhatIf,
		// Token: 0x04000A9B RID: 2715
		OutputBuffer,
		// Token: 0x04000A9C RID: 2716
		PerfMonStats,
		// Token: 0x04000A9D RID: 2717
		PersistStackHash,
		// Token: 0x04000A9E RID: 2718
		PinTable,
		// Token: 0x04000A9F RID: 2719
		ProcCache,
		// Token: 0x04000AA0 RID: 2720
		PrtiPage,
		// Token: 0x04000AA1 RID: 2721
		ReadPage,
		// Token: 0x04000AA2 RID: 2722
		RenameColumn,
		// Token: 0x04000AA3 RID: 2723
		RuleOff,
		// Token: 0x04000AA4 RID: 2724
		RuleOn,
		// Token: 0x04000AA5 RID: 2725
		SeMetadata,
		// Token: 0x04000AA6 RID: 2726
		SetCpuWeight,
		// Token: 0x04000AA7 RID: 2727
		SetInstance,
		// Token: 0x04000AA8 RID: 2728
		SetIOWeight,
		// Token: 0x04000AA9 RID: 2729
		ShowStatistics,
		// Token: 0x04000AAA RID: 2730
		ShowContig,
		// Token: 0x04000AAB RID: 2731
		ShowDBAffinity,
		// Token: 0x04000AAC RID: 2732
		ShowFileStats,
		// Token: 0x04000AAD RID: 2733
		ShowOffRules,
		// Token: 0x04000AAE RID: 2734
		ShowOnRules,
		// Token: 0x04000AAF RID: 2735
		ShowTableAffinity,
		// Token: 0x04000AB0 RID: 2736
		ShowText,
		// Token: 0x04000AB1 RID: 2737
		ShowWeights,
		// Token: 0x04000AB2 RID: 2738
		ShrinkDatabase,
		// Token: 0x04000AB3 RID: 2739
		ShrinkFile,
		// Token: 0x04000AB4 RID: 2740
		SqlMgrStats,
		// Token: 0x04000AB5 RID: 2741
		SqlPerf,
		// Token: 0x04000AB6 RID: 2742
		StackDump,
		// Token: 0x04000AB7 RID: 2743
		Tec,
		// Token: 0x04000AB8 RID: 2744
		ThawIO,
		// Token: 0x04000AB9 RID: 2745
		TraceOff,
		// Token: 0x04000ABA RID: 2746
		TraceOn,
		// Token: 0x04000ABB RID: 2747
		TraceStatus,
		// Token: 0x04000ABC RID: 2748
		UnpinTable,
		// Token: 0x04000ABD RID: 2749
		UpdateUsage,
		// Token: 0x04000ABE RID: 2750
		UsePlan,
		// Token: 0x04000ABF RID: 2751
		UserOptions,
		// Token: 0x04000AC0 RID: 2752
		WritePage
	}
}
