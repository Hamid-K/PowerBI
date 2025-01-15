using System;

namespace Microsoft.HostIntegration.MqClient.ProgrammableCommandFormat
{
	// Token: 0x02000B5F RID: 2911
	public enum Verb
	{
		// Token: 0x04004870 RID: 18544
		None,
		// Token: 0x04004871 RID: 18545
		ChangeQueueManager,
		// Token: 0x04004872 RID: 18546
		InquireQueueManager,
		// Token: 0x04004873 RID: 18547
		ChangeProcess,
		// Token: 0x04004874 RID: 18548
		CopyProcess,
		// Token: 0x04004875 RID: 18549
		CreateProcess,
		// Token: 0x04004876 RID: 18550
		DeleteProcess,
		// Token: 0x04004877 RID: 18551
		InquireProcess,
		// Token: 0x04004878 RID: 18552
		ChangeQueue,
		// Token: 0x04004879 RID: 18553
		ClearQueue,
		// Token: 0x0400487A RID: 18554
		CopyQueue,
		// Token: 0x0400487B RID: 18555
		CreateQueue,
		// Token: 0x0400487C RID: 18556
		DeleteQueue,
		// Token: 0x0400487D RID: 18557
		InquireQueue,
		// Token: 0x0400487E RID: 18558
		RefreshQueueManager = 16,
		// Token: 0x0400487F RID: 18559
		ResetQueueStatistics,
		// Token: 0x04004880 RID: 18560
		InquireQueueNames,
		// Token: 0x04004881 RID: 18561
		InquireProcessNames,
		// Token: 0x04004882 RID: 18562
		InquireChannelnames,
		// Token: 0x04004883 RID: 18563
		ChangeChannel,
		// Token: 0x04004884 RID: 18564
		CopyChannel,
		// Token: 0x04004885 RID: 18565
		CreateChannel,
		// Token: 0x04004886 RID: 18566
		DeleteChannel,
		// Token: 0x04004887 RID: 18567
		InquireChannel,
		// Token: 0x04004888 RID: 18568
		PingChannel,
		// Token: 0x04004889 RID: 18569
		ResetChannel,
		// Token: 0x0400488A RID: 18570
		StartChannel,
		// Token: 0x0400488B RID: 18571
		StopChannel,
		// Token: 0x0400488C RID: 18572
		StartChannelInitialization,
		// Token: 0x0400488D RID: 18573
		StartChannelListener,
		// Token: 0x0400488E RID: 18574
		ChangeNamelist,
		// Token: 0x0400488F RID: 18575
		CopyNamelist,
		// Token: 0x04004890 RID: 18576
		CreateNamelist,
		// Token: 0x04004891 RID: 18577
		DeleteNamelist,
		// Token: 0x04004892 RID: 18578
		InquireNamelist,
		// Token: 0x04004893 RID: 18579
		InquireNamelistNames,
		// Token: 0x04004894 RID: 18580
		Escape,
		// Token: 0x04004895 RID: 18581
		ResolveChannel,
		// Token: 0x04004896 RID: 18582
		PingQueueManager,
		// Token: 0x04004897 RID: 18583
		InquireQueueStatus,
		// Token: 0x04004898 RID: 18584
		InquireChannelStatus,
		// Token: 0x04004899 RID: 18585
		ConfigurationEvent,
		// Token: 0x0400489A RID: 18586
		QueueManagerEvent,
		// Token: 0x0400489B RID: 18587
		PerfomanceEvent,
		// Token: 0x0400489C RID: 18588
		ChannelEvent,
		// Token: 0x0400489D RID: 18589
		DeletePublication = 60,
		// Token: 0x0400489E RID: 18590
		DeregisterPublisher,
		// Token: 0x0400489F RID: 18591
		DeregisterSubscriber,
		// Token: 0x040048A0 RID: 18592
		Publish,
		// Token: 0x040048A1 RID: 18593
		RegisterPublisher,
		// Token: 0x040048A2 RID: 18594
		RegisterSubscriber,
		// Token: 0x040048A3 RID: 18595
		RequestUpdate,
		// Token: 0x040048A4 RID: 18596
		BrokerInternal,
		// Token: 0x040048A5 RID: 18597
		ActivityMessage = 69,
		// Token: 0x040048A6 RID: 18598
		InquireClusterQueueManager,
		// Token: 0x040048A7 RID: 18599
		ResumeQueueManagerCluster,
		// Token: 0x040048A8 RID: 18600
		SuspendQueueManagerCluster,
		// Token: 0x040048A9 RID: 18601
		RefreshCluster,
		// Token: 0x040048AA RID: 18602
		ResetCluster,
		// Token: 0x040048AB RID: 18603
		TraceRoute,
		// Token: 0x040048AC RID: 18604
		RefreshSecurity = 78,
		// Token: 0x040048AD RID: 18605
		ChangeAuthorizationInformation,
		// Token: 0x040048AE RID: 18606
		CopyAuthorizationInformation,
		// Token: 0x040048AF RID: 18607
		CreateAuthorizationInformation,
		// Token: 0x040048B0 RID: 18608
		DeleteAuthorizationInformation,
		// Token: 0x040048B1 RID: 18609
		InquireAuthorizationInformation,
		// Token: 0x040048B2 RID: 18610
		InquireAuthorizationInformationNames,
		// Token: 0x040048B3 RID: 18611
		InquireConnection,
		// Token: 0x040048B4 RID: 18612
		StopConnection,
		// Token: 0x040048B5 RID: 18613
		InquireAuthorizationRecords,
		// Token: 0x040048B6 RID: 18614
		InquireEntityAuthorization,
		// Token: 0x040048B7 RID: 18615
		DeleteAuthorizationRecord,
		// Token: 0x040048B8 RID: 18616
		SetAuthorizationRecord,
		// Token: 0x040048B9 RID: 18617
		LoggerEvent,
		// Token: 0x040048BA RID: 18618
		ResetQueueManager,
		// Token: 0x040048BB RID: 18619
		ChangeListener,
		// Token: 0x040048BC RID: 18620
		CopyListener,
		// Token: 0x040048BD RID: 18621
		CreateListener,
		// Token: 0x040048BE RID: 18622
		DeleteListener,
		// Token: 0x040048BF RID: 18623
		InquireListener,
		// Token: 0x040048C0 RID: 18624
		InquireListenerStatus,
		// Token: 0x040048C1 RID: 18625
		CommandEvent,
		// Token: 0x040048C2 RID: 18626
		ChangeSecurity,
		// Token: 0x040048C3 RID: 18627
		ChangeCfStructure,
		// Token: 0x040048C4 RID: 18628
		ChangeStgClass,
		// Token: 0x040048C5 RID: 18629
		ChangeTrace,
		// Token: 0x040048C6 RID: 18630
		ArchiveLog,
		// Token: 0x040048C7 RID: 18631
		BackupCfStructure,
		// Token: 0x040048C8 RID: 18632
		CreateBufferPool,
		// Token: 0x040048C9 RID: 18633
		CreatePageSet,
		// Token: 0x040048CA RID: 18634
		CreateCfStructure,
		// Token: 0x040048CB RID: 18635
		CreateStgClass,
		// Token: 0x040048CC RID: 18636
		CopyCfStructure,
		// Token: 0x040048CD RID: 18637
		CopyStgClass,
		// Token: 0x040048CE RID: 18638
		DeleteCfStructure,
		// Token: 0x040048CF RID: 18639
		DeleteStgClass,
		// Token: 0x040048D0 RID: 18640
		InquireArchive,
		// Token: 0x040048D1 RID: 18641
		InquireCfStructure,
		// Token: 0x040048D2 RID: 18642
		InquireCfStructureStatus,
		// Token: 0x040048D3 RID: 18643
		InquireCommandServer,
		// Token: 0x040048D4 RID: 18644
		InquireChannelInitialization,
		// Token: 0x040048D5 RID: 18645
		InquireQsg,
		// Token: 0x040048D6 RID: 18646
		InquireLog,
		// Token: 0x040048D7 RID: 18647
		InquireSecurity,
		// Token: 0x040048D8 RID: 18648
		InquireStgClass,
		// Token: 0x040048D9 RID: 18649
		InquireSystem,
		// Token: 0x040048DA RID: 18650
		InquireThread,
		// Token: 0x040048DB RID: 18651
		InquireTrace,
		// Token: 0x040048DC RID: 18652
		InquireUsage,
		// Token: 0x040048DD RID: 18653
		MoveQueue,
		// Token: 0x040048DE RID: 18654
		RecoverBsds,
		// Token: 0x040048DF RID: 18655
		RecoverCfStructure,
		// Token: 0x040048E0 RID: 18656
		ResetTPipe,
		// Token: 0x040048E1 RID: 18657
		ResolveInDoubt,
		// Token: 0x040048E2 RID: 18658
		ResumeQueueManager,
		// Token: 0x040048E3 RID: 18659
		ReverifySecurity,
		// Token: 0x040048E4 RID: 18660
		SetArchive,
		// Token: 0x040048E5 RID: 18661
		SetLog = 136,
		// Token: 0x040048E6 RID: 18662
		SetSystem,
		// Token: 0x040048E7 RID: 18663
		StartCommandServer,
		// Token: 0x040048E8 RID: 18664
		StartQueueManager,
		// Token: 0x040048E9 RID: 18665
		StartTrace,
		// Token: 0x040048EA RID: 18666
		StopChannelInitialization,
		// Token: 0x040048EB RID: 18667
		StopChannelListener,
		// Token: 0x040048EC RID: 18668
		StopCommandServer,
		// Token: 0x040048ED RID: 18669
		StopQueueManager,
		// Token: 0x040048EE RID: 18670
		StopTrace,
		// Token: 0x040048EF RID: 18671
		SuspendQueueManager,
		// Token: 0x040048F0 RID: 18672
		InquireCfStructureNames,
		// Token: 0x040048F1 RID: 18673
		InquireStgClassNames,
		// Token: 0x040048F2 RID: 18674
		ChangeService,
		// Token: 0x040048F3 RID: 18675
		CopyService,
		// Token: 0x040048F4 RID: 18676
		CreateService,
		// Token: 0x040048F5 RID: 18677
		DeleteService,
		// Token: 0x040048F6 RID: 18678
		InquireService,
		// Token: 0x040048F7 RID: 18679
		InquireServiceStatus,
		// Token: 0x040048F8 RID: 18680
		StartService,
		// Token: 0x040048F9 RID: 18681
		StopService,
		// Token: 0x040048FA RID: 18682
		DeleteBufferPool,
		// Token: 0x040048FB RID: 18683
		DeletePageSet,
		// Token: 0x040048FC RID: 18684
		ChangeBufferPool,
		// Token: 0x040048FD RID: 18685
		ChangePageSet,
		// Token: 0x040048FE RID: 18686
		InquireQueueManagerStatus,
		// Token: 0x040048FF RID: 18687
		CreateLog,
		// Token: 0x04004900 RID: 18688
		StatisticsMqi = 164,
		// Token: 0x04004901 RID: 18689
		StatisticsQueue,
		// Token: 0x04004902 RID: 18690
		StatisticsChannel,
		// Token: 0x04004903 RID: 18691
		AccountingMqi,
		// Token: 0x04004904 RID: 18692
		AccountingQueue,
		// Token: 0x04004905 RID: 18693
		InquireAuthorizationService,
		// Token: 0x04004906 RID: 18694
		ChangeTopic,
		// Token: 0x04004907 RID: 18695
		CopyTopic,
		// Token: 0x04004908 RID: 18696
		CreateTopic,
		// Token: 0x04004909 RID: 18697
		DeleteTopic,
		// Token: 0x0400490A RID: 18698
		InquireTopic,
		// Token: 0x0400490B RID: 18699
		InquireTopicNames,
		// Token: 0x0400490C RID: 18700
		InquireSubscription,
		// Token: 0x0400490D RID: 18701
		CreateSubscription,
		// Token: 0x0400490E RID: 18702
		ChangeSubscription,
		// Token: 0x0400490F RID: 18703
		DeleteSubscription,
		// Token: 0x04004910 RID: 18704
		CopySubscription = 181,
		// Token: 0x04004911 RID: 18705
		InquireSubStatus,
		// Token: 0x04004912 RID: 18706
		InquireTopicStatus,
		// Token: 0x04004913 RID: 18707
		ClearTopicString,
		// Token: 0x04004914 RID: 18708
		InquirePubSubStatus,
		// Token: 0x04004915 RID: 18709
		PurgeChannel = 195
	}
}
