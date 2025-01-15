using System;
using System.Globalization;
using System.Resources;

namespace Microsoft.HostIntegration.MqClient.StrictResources.Globals
{
	// Token: 0x02000BD3 RID: 3027
	internal class SR
	{
		// Token: 0x06005DF6 RID: 24054 RVA: 0x00002061 File Offset: 0x00000261
		private SR()
		{
		}

		// Token: 0x170016ED RID: 5869
		// (get) Token: 0x06005DF7 RID: 24055 RVA: 0x0017FD37 File Offset: 0x0017DF37
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (SR.resourceManager == null)
				{
					SR.resourceManager = new ResourceManager("Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR", typeof(SR).Assembly);
				}
				return SR.resourceManager;
			}
		}

		// Token: 0x170016EE RID: 5870
		// (get) Token: 0x06005DF8 RID: 24056 RVA: 0x0017FD63 File Offset: 0x0017DF63
		// (set) Token: 0x06005DF9 RID: 24057 RVA: 0x0017FD6A File Offset: 0x0017DF6A
		internal static CultureInfo Culture
		{
			get
			{
				return SR.resourceCulture;
			}
			set
			{
				SR.resourceCulture = value;
			}
		}

		// Token: 0x170016EF RID: 5871
		// (get) Token: 0x06005DFA RID: 24058 RVA: 0x0017FD72 File Offset: 0x0017DF72
		internal static string TcpConnectFailed
		{
			get
			{
				return SR.ResourceManager.GetString("TcpConnectFailed", SR.Culture);
			}
		}

		// Token: 0x170016F0 RID: 5872
		// (get) Token: 0x06005DFB RID: 24059 RVA: 0x0017FD88 File Offset: 0x0017DF88
		internal static string TcpSslConnectFailed
		{
			get
			{
				return SR.ResourceManager.GetString("TcpSslConnectFailed", SR.Culture);
			}
		}

		// Token: 0x170016F1 RID: 5873
		// (get) Token: 0x06005DFC RID: 24060 RVA: 0x0017FD9E File Offset: 0x0017DF9E
		internal static string TcpPoolingDifferentSsl
		{
			get
			{
				return SR.ResourceManager.GetString("TcpPoolingDifferentSsl", SR.Culture);
			}
		}

		// Token: 0x170016F2 RID: 5874
		// (get) Token: 0x06005DFD RID: 24061 RVA: 0x0017FDB4 File Offset: 0x0017DFB4
		internal static string QmConnectTcpConnectFailed
		{
			get
			{
				return SR.ResourceManager.GetString("QmConnectTcpConnectFailed", SR.Culture);
			}
		}

		// Token: 0x170016F3 RID: 5875
		// (get) Token: 0x06005DFE RID: 24062 RVA: 0x0017FDCA File Offset: 0x0017DFCA
		internal static string QmConnectTcpDisconnected
		{
			get
			{
				return SR.ResourceManager.GetString("QmConnectTcpDisconnected", SR.Culture);
			}
		}

		// Token: 0x170016F4 RID: 5876
		// (get) Token: 0x06005DFF RID: 24063 RVA: 0x0017FDE0 File Offset: 0x0017DFE0
		internal static string QmConnectInitialDataRejectedCcsid
		{
			get
			{
				return SR.ResourceManager.GetString("QmConnectInitialDataRejectedCcsid", SR.Culture);
			}
		}

		// Token: 0x170016F5 RID: 5877
		// (get) Token: 0x06005E00 RID: 24064 RVA: 0x0017FDF6 File Offset: 0x0017DFF6
		internal static string QmConnectInitialDataRejectedConversations0
		{
			get
			{
				return SR.ResourceManager.GetString("QmConnectInitialDataRejectedConversations0", SR.Culture);
			}
		}

		// Token: 0x170016F6 RID: 5878
		// (get) Token: 0x06005E01 RID: 24065 RVA: 0x0017FE0C File Offset: 0x0017E00C
		internal static string QmConnectInitialDataRejectedEncoding
		{
			get
			{
				return SR.ResourceManager.GetString("QmConnectInitialDataRejectedEncoding", SR.Culture);
			}
		}

		// Token: 0x170016F7 RID: 5879
		// (get) Token: 0x06005E02 RID: 24066 RVA: 0x0017FE22 File Offset: 0x0017E022
		internal static string QmConnectInitialDataRejectedErrorFlag2
		{
			get
			{
				return SR.ResourceManager.GetString("QmConnectInitialDataRejectedErrorFlag2", SR.Culture);
			}
		}

		// Token: 0x170016F8 RID: 5880
		// (get) Token: 0x06005E03 RID: 24067 RVA: 0x0017FE38 File Offset: 0x0017E038
		internal static string QmConnectInitialDataRejectedFap
		{
			get
			{
				return SR.ResourceManager.GetString("QmConnectInitialDataRejectedFap", SR.Culture);
			}
		}

		// Token: 0x170016F9 RID: 5881
		// (get) Token: 0x06005E04 RID: 24068 RVA: 0x0017FE4E File Offset: 0x0017E04E
		internal static string QmConnectInitialDataRejectedTransmissionSize
		{
			get
			{
				return SR.ResourceManager.GetString("QmConnectInitialDataRejectedTransmissionSize", SR.Culture);
			}
		}

		// Token: 0x170016FA RID: 5882
		// (get) Token: 0x06005E05 RID: 24069 RVA: 0x0017FE64 File Offset: 0x0017E064
		internal static string QmConnectUnexpected
		{
			get
			{
				return SR.ResourceManager.GetString("QmConnectUnexpected", SR.Culture);
			}
		}

		// Token: 0x170016FB RID: 5883
		// (get) Token: 0x06005E06 RID: 24070 RVA: 0x0017FE7A File Offset: 0x0017E07A
		internal static string QmConnectMqConnFailed
		{
			get
			{
				return SR.ResourceManager.GetString("QmConnectMqConnFailed", SR.Culture);
			}
		}

		// Token: 0x170016FC RID: 5884
		// (get) Token: 0x06005E07 RID: 24071 RVA: 0x0017FE90 File Offset: 0x0017E090
		internal static string QmConnectNoChannel
		{
			get
			{
				return SR.ResourceManager.GetString("QmConnectNoChannel", SR.Culture);
			}
		}

		// Token: 0x170016FD RID: 5885
		// (get) Token: 0x06005E08 RID: 24072 RVA: 0x0017FEA6 File Offset: 0x0017E0A6
		internal static string QmConnectCipherSpec
		{
			get
			{
				return SR.ResourceManager.GetString("QmConnectCipherSpec", SR.Culture);
			}
		}

		// Token: 0x170016FE RID: 5886
		// (get) Token: 0x06005E09 RID: 24073 RVA: 0x0017FEBC File Offset: 0x0017E0BC
		internal static string QmConnectSslExpected
		{
			get
			{
				return SR.ResourceManager.GetString("QmConnectSslExpected", SR.Culture);
			}
		}

		// Token: 0x170016FF RID: 5887
		// (get) Token: 0x06005E0A RID: 24074 RVA: 0x0017FED2 File Offset: 0x0017E0D2
		internal static string QmAsyncStatusFailedQuiescing
		{
			get
			{
				return SR.ResourceManager.GetString("QmAsyncStatusFailedQuiescing", SR.Culture);
			}
		}

		// Token: 0x17001700 RID: 5888
		// (get) Token: 0x06005E0B RID: 24075 RVA: 0x0017FEE8 File Offset: 0x0017E0E8
		internal static string QmAsyncStatusFailedHandle
		{
			get
			{
				return SR.ResourceManager.GetString("QmAsyncStatusFailedHandle", SR.Culture);
			}
		}

		// Token: 0x17001701 RID: 5889
		// (get) Token: 0x06005E0C RID: 24076 RVA: 0x0017FEFE File Offset: 0x0017E0FE
		internal static string QSendMaximumMessageSize
		{
			get
			{
				return SR.ResourceManager.GetString("QSendMaximumMessageSize", SR.Culture);
			}
		}

		// Token: 0x17001702 RID: 5890
		// (get) Token: 0x06005E0D RID: 24077 RVA: 0x0017FF14 File Offset: 0x0017E114
		internal static string QOpenFailedQmFailed
		{
			get
			{
				return SR.ResourceManager.GetString("QOpenFailedQmFailed", SR.Culture);
			}
		}

		// Token: 0x17001703 RID: 5891
		// (get) Token: 0x06005E0E RID: 24078 RVA: 0x0017FF2A File Offset: 0x0017E12A
		internal static string QOpenFailedQmQuiesced
		{
			get
			{
				return SR.ResourceManager.GetString("QOpenFailedQmQuiesced", SR.Culture);
			}
		}

		// Token: 0x17001704 RID: 5892
		// (get) Token: 0x06005E0F RID: 24079 RVA: 0x0017FF40 File Offset: 0x0017E140
		internal static string QOpenFailedTcpFailed
		{
			get
			{
				return SR.ResourceManager.GetString("QOpenFailedTcpFailed", SR.Culture);
			}
		}

		// Token: 0x17001705 RID: 5893
		// (get) Token: 0x06005E10 RID: 24080 RVA: 0x0017FF56 File Offset: 0x0017E156
		internal static string QOpenFailedFinalizing
		{
			get
			{
				return SR.ResourceManager.GetString("QOpenFailedFinalizing", SR.Culture);
			}
		}

		// Token: 0x17001706 RID: 5894
		// (get) Token: 0x06005E11 RID: 24081 RVA: 0x0017FF6C File Offset: 0x0017E16C
		internal static string QSendFailedUnknown
		{
			get
			{
				return SR.ResourceManager.GetString("QSendFailedUnknown", SR.Culture);
			}
		}

		// Token: 0x17001707 RID: 5895
		// (get) Token: 0x06005E12 RID: 24082 RVA: 0x0017FF82 File Offset: 0x0017E182
		internal static string QSendFailedQmFailed
		{
			get
			{
				return SR.ResourceManager.GetString("QSendFailedQmFailed", SR.Culture);
			}
		}

		// Token: 0x17001708 RID: 5896
		// (get) Token: 0x06005E13 RID: 24083 RVA: 0x0017FF98 File Offset: 0x0017E198
		internal static string QSendFailedQmQuiesced
		{
			get
			{
				return SR.ResourceManager.GetString("QSendFailedQmQuiesced", SR.Culture);
			}
		}

		// Token: 0x17001709 RID: 5897
		// (get) Token: 0x06005E14 RID: 24084 RVA: 0x0017FFAE File Offset: 0x0017E1AE
		internal static string QReceiveFailedUnknown
		{
			get
			{
				return SR.ResourceManager.GetString("QReceiveFailedUnknown", SR.Culture);
			}
		}

		// Token: 0x1700170A RID: 5898
		// (get) Token: 0x06005E15 RID: 24085 RVA: 0x0017FFC4 File Offset: 0x0017E1C4
		internal static string QReceiveFailedQmFailed
		{
			get
			{
				return SR.ResourceManager.GetString("QReceiveFailedQmFailed", SR.Culture);
			}
		}

		// Token: 0x1700170B RID: 5899
		// (get) Token: 0x06005E16 RID: 24086 RVA: 0x0017FFDA File Offset: 0x0017E1DA
		internal static string QReceiveFailedQmQuiesced
		{
			get
			{
				return SR.ResourceManager.GetString("QReceiveFailedQmQuiesced", SR.Culture);
			}
		}

		// Token: 0x1700170C RID: 5900
		// (get) Token: 0x06005E17 RID: 24087 RVA: 0x0017FFF0 File Offset: 0x0017E1F0
		internal static string XaEndClosing
		{
			get
			{
				return SR.ResourceManager.GetString("XaEndClosing", SR.Culture);
			}
		}

		// Token: 0x1700170D RID: 5901
		// (get) Token: 0x06005E18 RID: 24088 RVA: 0x00180006 File Offset: 0x0017E206
		internal static string XaEndDisconnecting
		{
			get
			{
				return SR.ResourceManager.GetString("XaEndDisconnecting", SR.Culture);
			}
		}

		// Token: 0x1700170E RID: 5902
		// (get) Token: 0x06005E19 RID: 24089 RVA: 0x0018001C File Offset: 0x0017E21C
		internal static string XaFinalizeClosing
		{
			get
			{
				return SR.ResourceManager.GetString("XaFinalizeClosing", SR.Culture);
			}
		}

		// Token: 0x1700170F RID: 5903
		// (get) Token: 0x06005E1A RID: 24090 RVA: 0x00180032 File Offset: 0x0017E232
		internal static string XaFinalizeDisconnecting
		{
			get
			{
				return SR.ResourceManager.GetString("XaFinalizeDisconnecting", SR.Culture);
			}
		}

		// Token: 0x17001710 RID: 5904
		// (get) Token: 0x06005E1B RID: 24091 RVA: 0x00180048 File Offset: 0x0017E248
		internal static string XaPrepareClosing
		{
			get
			{
				return SR.ResourceManager.GetString("XaPrepareClosing", SR.Culture);
			}
		}

		// Token: 0x17001711 RID: 5905
		// (get) Token: 0x06005E1C RID: 24092 RVA: 0x0018005E File Offset: 0x0017E25E
		internal static string XaPrepareDisconnecting
		{
			get
			{
				return SR.ResourceManager.GetString("XaPrepareDisconnecting", SR.Culture);
			}
		}

		// Token: 0x17001712 RID: 5906
		// (get) Token: 0x06005E1D RID: 24093 RVA: 0x00180074 File Offset: 0x0017E274
		internal static string XaStartClosing
		{
			get
			{
				return SR.ResourceManager.GetString("XaStartClosing", SR.Culture);
			}
		}

		// Token: 0x17001713 RID: 5907
		// (get) Token: 0x06005E1E RID: 24094 RVA: 0x0018008A File Offset: 0x0017E28A
		internal static string XaStartDisconnecting
		{
			get
			{
				return SR.ResourceManager.GetString("XaStartDisconnecting", SR.Culture);
			}
		}

		// Token: 0x17001714 RID: 5908
		// (get) Token: 0x06005E1F RID: 24095 RVA: 0x001800A0 File Offset: 0x0017E2A0
		internal static string XaRollback
		{
			get
			{
				return SR.ResourceManager.GetString("XaRollback", SR.Culture);
			}
		}

		// Token: 0x17001715 RID: 5909
		// (get) Token: 0x06005E20 RID: 24096 RVA: 0x001800B6 File Offset: 0x0017E2B6
		internal static string XaRollbackCommunicationFailure
		{
			get
			{
				return SR.ResourceManager.GetString("XaRollbackCommunicationFailure", SR.Culture);
			}
		}

		// Token: 0x17001716 RID: 5910
		// (get) Token: 0x06005E21 RID: 24097 RVA: 0x001800CC File Offset: 0x0017E2CC
		internal static string XaRollbackDeadlock
		{
			get
			{
				return SR.ResourceManager.GetString("XaRollbackDeadlock", SR.Culture);
			}
		}

		// Token: 0x17001717 RID: 5911
		// (get) Token: 0x06005E22 RID: 24098 RVA: 0x001800E2 File Offset: 0x0017E2E2
		internal static string XaRollbackIntegrity
		{
			get
			{
				return SR.ResourceManager.GetString("XaRollbackIntegrity", SR.Culture);
			}
		}

		// Token: 0x17001718 RID: 5912
		// (get) Token: 0x06005E23 RID: 24099 RVA: 0x001800F8 File Offset: 0x0017E2F8
		internal static string XaRollbackOther
		{
			get
			{
				return SR.ResourceManager.GetString("XaRollbackOther", SR.Culture);
			}
		}

		// Token: 0x17001719 RID: 5913
		// (get) Token: 0x06005E24 RID: 24100 RVA: 0x0018010E File Offset: 0x0017E30E
		internal static string XaRollbackProtocol
		{
			get
			{
				return SR.ResourceManager.GetString("XaRollbackProtocol", SR.Culture);
			}
		}

		// Token: 0x1700171A RID: 5914
		// (get) Token: 0x06005E25 RID: 24101 RVA: 0x00180124 File Offset: 0x0017E324
		internal static string XaRollbackTimeout
		{
			get
			{
				return SR.ResourceManager.GetString("XaRollbackTimeout", SR.Culture);
			}
		}

		// Token: 0x1700171B RID: 5915
		// (get) Token: 0x06005E26 RID: 24102 RVA: 0x0018013A File Offset: 0x0017E33A
		internal static string XaRollbackTransient
		{
			get
			{
				return SR.ResourceManager.GetString("XaRollbackTransient", SR.Culture);
			}
		}

		// Token: 0x1700171C RID: 5916
		// (get) Token: 0x06005E27 RID: 24103 RVA: 0x00180150 File Offset: 0x0017E350
		internal static string XaNoMigrate
		{
			get
			{
				return SR.ResourceManager.GetString("XaNoMigrate", SR.Culture);
			}
		}

		// Token: 0x1700171D RID: 5917
		// (get) Token: 0x06005E28 RID: 24104 RVA: 0x00180166 File Offset: 0x0017E366
		internal static string XaHeuristicHazard
		{
			get
			{
				return SR.ResourceManager.GetString("XaHeuristicHazard", SR.Culture);
			}
		}

		// Token: 0x1700171E RID: 5918
		// (get) Token: 0x06005E29 RID: 24105 RVA: 0x0018017C File Offset: 0x0017E37C
		internal static string XaHeuristicCommit
		{
			get
			{
				return SR.ResourceManager.GetString("XaHeuristicCommit", SR.Culture);
			}
		}

		// Token: 0x1700171F RID: 5919
		// (get) Token: 0x06005E2A RID: 24106 RVA: 0x00180192 File Offset: 0x0017E392
		internal static string XaHeuristicRollback
		{
			get
			{
				return SR.ResourceManager.GetString("XaHeuristicRollback", SR.Culture);
			}
		}

		// Token: 0x17001720 RID: 5920
		// (get) Token: 0x06005E2B RID: 24107 RVA: 0x001801A8 File Offset: 0x0017E3A8
		internal static string XaHeuristicMix
		{
			get
			{
				return SR.ResourceManager.GetString("XaHeuristicMix", SR.Culture);
			}
		}

		// Token: 0x17001721 RID: 5921
		// (get) Token: 0x06005E2C RID: 24108 RVA: 0x001801BE File Offset: 0x0017E3BE
		internal static string XaRetry
		{
			get
			{
				return SR.ResourceManager.GetString("XaRetry", SR.Culture);
			}
		}

		// Token: 0x17001722 RID: 5922
		// (get) Token: 0x06005E2D RID: 24109 RVA: 0x001801D4 File Offset: 0x0017E3D4
		internal static string XaReadOnly
		{
			get
			{
				return SR.ResourceManager.GetString("XaReadOnly", SR.Culture);
			}
		}

		// Token: 0x17001723 RID: 5923
		// (get) Token: 0x06005E2E RID: 24110 RVA: 0x001801EA File Offset: 0x0017E3EA
		internal static string XaOutstandingAsynchronousOperation
		{
			get
			{
				return SR.ResourceManager.GetString("XaOutstandingAsynchronousOperation", SR.Culture);
			}
		}

		// Token: 0x17001724 RID: 5924
		// (get) Token: 0x06005E2F RID: 24111 RVA: 0x00180200 File Offset: 0x0017E400
		internal static string XaResourceManagerError
		{
			get
			{
				return SR.ResourceManager.GetString("XaResourceManagerError", SR.Culture);
			}
		}

		// Token: 0x17001725 RID: 5925
		// (get) Token: 0x06005E30 RID: 24112 RVA: 0x00180216 File Offset: 0x0017E416
		internal static string XaInvalidXid
		{
			get
			{
				return SR.ResourceManager.GetString("XaInvalidXid", SR.Culture);
			}
		}

		// Token: 0x17001726 RID: 5926
		// (get) Token: 0x06005E31 RID: 24113 RVA: 0x0018022C File Offset: 0x0017E42C
		internal static string XaInvalidArguments
		{
			get
			{
				return SR.ResourceManager.GetString("XaInvalidArguments", SR.Culture);
			}
		}

		// Token: 0x17001727 RID: 5927
		// (get) Token: 0x06005E32 RID: 24114 RVA: 0x00180242 File Offset: 0x0017E442
		internal static string XaProtocol
		{
			get
			{
				return SR.ResourceManager.GetString("XaProtocol", SR.Culture);
			}
		}

		// Token: 0x17001728 RID: 5928
		// (get) Token: 0x06005E33 RID: 24115 RVA: 0x00180258 File Offset: 0x0017E458
		internal static string XaResourceManagerUnavailable
		{
			get
			{
				return SR.ResourceManager.GetString("XaResourceManagerUnavailable", SR.Culture);
			}
		}

		// Token: 0x17001729 RID: 5929
		// (get) Token: 0x06005E34 RID: 24116 RVA: 0x0018026E File Offset: 0x0017E46E
		internal static string XaDuplicateXid
		{
			get
			{
				return SR.ResourceManager.GetString("XaDuplicateXid", SR.Culture);
			}
		}

		// Token: 0x1700172A RID: 5930
		// (get) Token: 0x06005E35 RID: 24117 RVA: 0x00180284 File Offset: 0x0017E484
		internal static string XaWorkOutsideTransaction
		{
			get
			{
				return SR.ResourceManager.GetString("XaWorkOutsideTransaction", SR.Culture);
			}
		}

		// Token: 0x1700172B RID: 5931
		// (get) Token: 0x06005E36 RID: 24118 RVA: 0x0018029A File Offset: 0x0017E49A
		internal static string XaEnlistmentFailed
		{
			get
			{
				return SR.ResourceManager.GetString("XaEnlistmentFailed", SR.Culture);
			}
		}

		// Token: 0x1700172C RID: 5932
		// (get) Token: 0x06005E37 RID: 24119 RVA: 0x001802B0 File Offset: 0x0017E4B0
		internal static string StaleTransaction
		{
			get
			{
				return SR.ResourceManager.GetString("StaleTransaction", SR.Culture);
			}
		}

		// Token: 0x06005E38 RID: 24120 RVA: 0x001802C6 File Offset: 0x0017E4C6
		internal static string MqReturnCode(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("MqReturnCode", SR.Culture), param0);
		}

		// Token: 0x06005E39 RID: 24121 RVA: 0x001802E7 File Offset: 0x0017E4E7
		internal static string MaximumLength(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("MaximumLength", SR.Culture), param0);
		}

		// Token: 0x06005E3A RID: 24122 RVA: 0x00180308 File Offset: 0x0017E508
		internal static string ExactLength(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("ExactLength", SR.Culture), param0);
		}

		// Token: 0x06005E3B RID: 24123 RVA: 0x00180329 File Offset: 0x0017E529
		internal static string ValueRange(object param0, object param1)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("ValueRange", SR.Culture), param0, param1);
		}

		// Token: 0x04004FB5 RID: 20405
		private static ResourceManager resourceManager;

		// Token: 0x04004FB6 RID: 20406
		private static CultureInfo resourceCulture;
	}
}
