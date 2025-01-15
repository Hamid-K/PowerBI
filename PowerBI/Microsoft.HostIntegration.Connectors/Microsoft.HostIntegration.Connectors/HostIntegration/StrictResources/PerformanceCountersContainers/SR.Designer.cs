using System;
using System.Globalization;
using System.Resources;

namespace Microsoft.HostIntegration.StrictResources.PerformanceCountersContainers
{
	// Token: 0x02000785 RID: 1925
	internal class SR
	{
		// Token: 0x06003DE5 RID: 15845 RVA: 0x00002061 File Offset: 0x00000261
		private SR()
		{
		}

		// Token: 0x17000E5A RID: 3674
		// (get) Token: 0x06003DE6 RID: 15846 RVA: 0x000D07E3 File Offset: 0x000CE9E3
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (SR.resourceManager == null)
				{
					SR.resourceManager = new ResourceManager("Microsoft.HostIntegration.StrictResources.PerformanceCountersContainers.SR", typeof(SR).Assembly);
				}
				return SR.resourceManager;
			}
		}

		// Token: 0x17000E5B RID: 3675
		// (get) Token: 0x06003DE7 RID: 15847 RVA: 0x000D080F File Offset: 0x000CEA0F
		// (set) Token: 0x06003DE8 RID: 15848 RVA: 0x000D0816 File Offset: 0x000CEA16
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

		// Token: 0x17000E5C RID: 3676
		// (get) Token: 0x06003DE9 RID: 15849 RVA: 0x000D081E File Offset: 0x000CEA1E
		internal static string WipPerformanceCounterCategory
		{
			get
			{
				return SR.ResourceManager.GetString("WipPerformanceCounterCategory", SR.Culture);
			}
		}

		// Token: 0x17000E5D RID: 3677
		// (get) Token: 0x06003DEA RID: 15850 RVA: 0x000D0834 File Offset: 0x000CEA34
		internal static string WipPerformanceCounterCategoryHelp
		{
			get
			{
				return SR.ResourceManager.GetString("WipPerformanceCounterCategoryHelp", SR.Culture);
			}
		}

		// Token: 0x17000E5E RID: 3678
		// (get) Token: 0x06003DEB RID: 15851 RVA: 0x000D084A File Offset: 0x000CEA4A
		internal static string WipAverageMethodCallTime
		{
			get
			{
				return SR.ResourceManager.GetString("WipAverageMethodCallTime", SR.Culture);
			}
		}

		// Token: 0x17000E5F RID: 3679
		// (get) Token: 0x06003DEC RID: 15852 RVA: 0x000D0860 File Offset: 0x000CEA60
		internal static string WipAverageHostResponseTime
		{
			get
			{
				return SR.ResourceManager.GetString("WipAverageHostResponseTime", SR.Culture);
			}
		}

		// Token: 0x17000E60 RID: 3680
		// (get) Token: 0x06003DED RID: 15853 RVA: 0x000D0876 File Offset: 0x000CEA76
		internal static string WipCumulativeCalls
		{
			get
			{
				return SR.ResourceManager.GetString("WipCumulativeCalls", SR.Culture);
			}
		}

		// Token: 0x17000E61 RID: 3681
		// (get) Token: 0x06003DEE RID: 15854 RVA: 0x000D088C File Offset: 0x000CEA8C
		internal static string WipCurrentlyExecutingCalls
		{
			get
			{
				return SR.ResourceManager.GetString("WipCurrentlyExecutingCalls", SR.Culture);
			}
		}

		// Token: 0x17000E62 RID: 3682
		// (get) Token: 0x06003DEF RID: 15855 RVA: 0x000D08A2 File Offset: 0x000CEAA2
		internal static string WipTotalCallsPerSecond
		{
			get
			{
				return SR.ResourceManager.GetString("WipTotalCallsPerSecond", SR.Culture);
			}
		}

		// Token: 0x17000E63 RID: 3683
		// (get) Token: 0x06003DF0 RID: 15856 RVA: 0x000D08B8 File Offset: 0x000CEAB8
		internal static string WipTotalErrorsPerSecond
		{
			get
			{
				return SR.ResourceManager.GetString("WipTotalErrorsPerSecond", SR.Culture);
			}
		}

		// Token: 0x17000E64 RID: 3684
		// (get) Token: 0x06003DF1 RID: 15857 RVA: 0x000D08CE File Offset: 0x000CEACE
		internal static string WipAverageMethodCallTimeHelp
		{
			get
			{
				return SR.ResourceManager.GetString("WipAverageMethodCallTimeHelp", SR.Culture);
			}
		}

		// Token: 0x17000E65 RID: 3685
		// (get) Token: 0x06003DF2 RID: 15858 RVA: 0x000D08E4 File Offset: 0x000CEAE4
		internal static string WipAverageHostResponseTimeHelp
		{
			get
			{
				return SR.ResourceManager.GetString("WipAverageHostResponseTimeHelp", SR.Culture);
			}
		}

		// Token: 0x17000E66 RID: 3686
		// (get) Token: 0x06003DF3 RID: 15859 RVA: 0x000D08FA File Offset: 0x000CEAFA
		internal static string WipCumulativeCallsHelp
		{
			get
			{
				return SR.ResourceManager.GetString("WipCumulativeCallsHelp", SR.Culture);
			}
		}

		// Token: 0x17000E67 RID: 3687
		// (get) Token: 0x06003DF4 RID: 15860 RVA: 0x000D0910 File Offset: 0x000CEB10
		internal static string WipCurrentlyExecutingCallsHelp
		{
			get
			{
				return SR.ResourceManager.GetString("WipCurrentlyExecutingCallsHelp", SR.Culture);
			}
		}

		// Token: 0x17000E68 RID: 3688
		// (get) Token: 0x06003DF5 RID: 15861 RVA: 0x000D0926 File Offset: 0x000CEB26
		internal static string WipTotalCallsPerSecondHelp
		{
			get
			{
				return SR.ResourceManager.GetString("WipTotalCallsPerSecondHelp", SR.Culture);
			}
		}

		// Token: 0x17000E69 RID: 3689
		// (get) Token: 0x06003DF6 RID: 15862 RVA: 0x000D093C File Offset: 0x000CEB3C
		internal static string WipTotalErrorsPerSecondHelp
		{
			get
			{
				return SR.ResourceManager.GetString("WipTotalErrorsPerSecondHelp", SR.Culture);
			}
		}

		// Token: 0x17000E6A RID: 3690
		// (get) Token: 0x06003DF7 RID: 15863 RVA: 0x000D0952 File Offset: 0x000CEB52
		internal static string HipPerformanceCounterCategory
		{
			get
			{
				return SR.ResourceManager.GetString("HipPerformanceCounterCategory", SR.Culture);
			}
		}

		// Token: 0x17000E6B RID: 3691
		// (get) Token: 0x06003DF8 RID: 15864 RVA: 0x000D0968 File Offset: 0x000CEB68
		internal static string HipPerformanceCounterCategoryHelp
		{
			get
			{
				return SR.ResourceManager.GetString("HipPerformanceCounterCategoryHelp", SR.Culture);
			}
		}

		// Token: 0x17000E6C RID: 3692
		// (get) Token: 0x06003DF9 RID: 15865 RVA: 0x000D097E File Offset: 0x000CEB7E
		internal static string HipAverageMethodCallTime
		{
			get
			{
				return SR.ResourceManager.GetString("HipAverageMethodCallTime", SR.Culture);
			}
		}

		// Token: 0x17000E6D RID: 3693
		// (get) Token: 0x06003DFA RID: 15866 RVA: 0x000D0994 File Offset: 0x000CEB94
		internal static string HipAverageHostResponseTime
		{
			get
			{
				return SR.ResourceManager.GetString("HipAverageHostResponseTime", SR.Culture);
			}
		}

		// Token: 0x17000E6E RID: 3694
		// (get) Token: 0x06003DFB RID: 15867 RVA: 0x000D09AA File Offset: 0x000CEBAA
		internal static string HipCumulativeCalls
		{
			get
			{
				return SR.ResourceManager.GetString("HipCumulativeCalls", SR.Culture);
			}
		}

		// Token: 0x17000E6F RID: 3695
		// (get) Token: 0x06003DFC RID: 15868 RVA: 0x000D09C0 File Offset: 0x000CEBC0
		internal static string HipCurrentlyExecutingCalls
		{
			get
			{
				return SR.ResourceManager.GetString("HipCurrentlyExecutingCalls", SR.Culture);
			}
		}

		// Token: 0x17000E70 RID: 3696
		// (get) Token: 0x06003DFD RID: 15869 RVA: 0x000D09D6 File Offset: 0x000CEBD6
		internal static string HipTotalCallsPerSecond
		{
			get
			{
				return SR.ResourceManager.GetString("HipTotalCallsPerSecond", SR.Culture);
			}
		}

		// Token: 0x17000E71 RID: 3697
		// (get) Token: 0x06003DFE RID: 15870 RVA: 0x000D09EC File Offset: 0x000CEBEC
		internal static string HipTotalErrorsPerSecond
		{
			get
			{
				return SR.ResourceManager.GetString("HipTotalErrorsPerSecond", SR.Culture);
			}
		}

		// Token: 0x17000E72 RID: 3698
		// (get) Token: 0x06003DFF RID: 15871 RVA: 0x000D0A02 File Offset: 0x000CEC02
		internal static string HipAverageMethodCallTimeHelp
		{
			get
			{
				return SR.ResourceManager.GetString("HipAverageMethodCallTimeHelp", SR.Culture);
			}
		}

		// Token: 0x17000E73 RID: 3699
		// (get) Token: 0x06003E00 RID: 15872 RVA: 0x000D0A18 File Offset: 0x000CEC18
		internal static string HipAverageHostResponseTimeHelp
		{
			get
			{
				return SR.ResourceManager.GetString("HipAverageHostResponseTimeHelp", SR.Culture);
			}
		}

		// Token: 0x17000E74 RID: 3700
		// (get) Token: 0x06003E01 RID: 15873 RVA: 0x000D0A2E File Offset: 0x000CEC2E
		internal static string HipCumulativeCallsHelp
		{
			get
			{
				return SR.ResourceManager.GetString("HipCumulativeCallsHelp", SR.Culture);
			}
		}

		// Token: 0x17000E75 RID: 3701
		// (get) Token: 0x06003E02 RID: 15874 RVA: 0x000D0A44 File Offset: 0x000CEC44
		internal static string HipCurrentlyExecutingCallsHelp
		{
			get
			{
				return SR.ResourceManager.GetString("HipCurrentlyExecutingCallsHelp", SR.Culture);
			}
		}

		// Token: 0x17000E76 RID: 3702
		// (get) Token: 0x06003E03 RID: 15875 RVA: 0x000D0A5A File Offset: 0x000CEC5A
		internal static string HipTotalCallsPerSecondHelp
		{
			get
			{
				return SR.ResourceManager.GetString("HipTotalCallsPerSecondHelp", SR.Culture);
			}
		}

		// Token: 0x17000E77 RID: 3703
		// (get) Token: 0x06003E04 RID: 15876 RVA: 0x000D0A70 File Offset: 0x000CEC70
		internal static string HipTotalErrorsPerSecondHelp
		{
			get
			{
				return SR.ResourceManager.GetString("HipTotalErrorsPerSecondHelp", SR.Culture);
			}
		}

		// Token: 0x17000E78 RID: 3704
		// (get) Token: 0x06003E05 RID: 15877 RVA: 0x000D0A86 File Offset: 0x000CEC86
		internal static string MqClientPerformanceCounterCategory
		{
			get
			{
				return SR.ResourceManager.GetString("MqClientPerformanceCounterCategory", SR.Culture);
			}
		}

		// Token: 0x17000E79 RID: 3705
		// (get) Token: 0x06003E06 RID: 15878 RVA: 0x000D0A9C File Offset: 0x000CEC9C
		internal static string MqClientPerformanceCounterCategoryHelp
		{
			get
			{
				return SR.ResourceManager.GetString("MqClientPerformanceCounterCategoryHelp", SR.Culture);
			}
		}

		// Token: 0x17000E7A RID: 3706
		// (get) Token: 0x06003E07 RID: 15879 RVA: 0x000D0AB2 File Offset: 0x000CECB2
		internal static string MqClientAveragePutTime
		{
			get
			{
				return SR.ResourceManager.GetString("MqClientAveragePutTime", SR.Culture);
			}
		}

		// Token: 0x17000E7B RID: 3707
		// (get) Token: 0x06003E08 RID: 15880 RVA: 0x000D0AC8 File Offset: 0x000CECC8
		internal static string MqClientAverageGetTime
		{
			get
			{
				return SR.ResourceManager.GetString("MqClientAverageGetTime", SR.Culture);
			}
		}

		// Token: 0x17000E7C RID: 3708
		// (get) Token: 0x06003E09 RID: 15881 RVA: 0x000D0ADE File Offset: 0x000CECDE
		internal static string MqClientCumulativePuts
		{
			get
			{
				return SR.ResourceManager.GetString("MqClientCumulativePuts", SR.Culture);
			}
		}

		// Token: 0x17000E7D RID: 3709
		// (get) Token: 0x06003E0A RID: 15882 RVA: 0x000D0AF4 File Offset: 0x000CECF4
		internal static string MqClientCumulativeGets
		{
			get
			{
				return SR.ResourceManager.GetString("MqClientCumulativeGets", SR.Culture);
			}
		}

		// Token: 0x17000E7E RID: 3710
		// (get) Token: 0x06003E0B RID: 15883 RVA: 0x000D0B0A File Offset: 0x000CED0A
		internal static string MqClientPutsPerSecond
		{
			get
			{
				return SR.ResourceManager.GetString("MqClientPutsPerSecond", SR.Culture);
			}
		}

		// Token: 0x17000E7F RID: 3711
		// (get) Token: 0x06003E0C RID: 15884 RVA: 0x000D0B20 File Offset: 0x000CED20
		internal static string MqClientGetsPerSecond
		{
			get
			{
				return SR.ResourceManager.GetString("MqClientGetsPerSecond", SR.Culture);
			}
		}

		// Token: 0x17000E80 RID: 3712
		// (get) Token: 0x06003E0D RID: 15885 RVA: 0x000D0B36 File Offset: 0x000CED36
		internal static string MqClientAverageTransactionalPutTime
		{
			get
			{
				return SR.ResourceManager.GetString("MqClientAverageTransactionalPutTime", SR.Culture);
			}
		}

		// Token: 0x17000E81 RID: 3713
		// (get) Token: 0x06003E0E RID: 15886 RVA: 0x000D0B4C File Offset: 0x000CED4C
		internal static string MqClientAverageTransactionalGetTime
		{
			get
			{
				return SR.ResourceManager.GetString("MqClientAverageTransactionalGetTime", SR.Culture);
			}
		}

		// Token: 0x17000E82 RID: 3714
		// (get) Token: 0x06003E0F RID: 15887 RVA: 0x000D0B62 File Offset: 0x000CED62
		internal static string MqClientCumulativeTransactionalPuts
		{
			get
			{
				return SR.ResourceManager.GetString("MqClientCumulativeTransactionalPuts", SR.Culture);
			}
		}

		// Token: 0x17000E83 RID: 3715
		// (get) Token: 0x06003E10 RID: 15888 RVA: 0x000D0B78 File Offset: 0x000CED78
		internal static string MqClientCumulativeTransactionalGets
		{
			get
			{
				return SR.ResourceManager.GetString("MqClientCumulativeTransactionalGets", SR.Culture);
			}
		}

		// Token: 0x17000E84 RID: 3716
		// (get) Token: 0x06003E11 RID: 15889 RVA: 0x000D0B8E File Offset: 0x000CED8E
		internal static string MqClientTransactionalPutsPerSecond
		{
			get
			{
				return SR.ResourceManager.GetString("MqClientTransactionalPutsPerSecond", SR.Culture);
			}
		}

		// Token: 0x17000E85 RID: 3717
		// (get) Token: 0x06003E12 RID: 15890 RVA: 0x000D0BA4 File Offset: 0x000CEDA4
		internal static string MqClientTransactionalGetsPerSecond
		{
			get
			{
				return SR.ResourceManager.GetString("MqClientTransactionalGetsPerSecond", SR.Culture);
			}
		}

		// Token: 0x17000E86 RID: 3718
		// (get) Token: 0x06003E13 RID: 15891 RVA: 0x000D0BBA File Offset: 0x000CEDBA
		internal static string MqClientAveragePutTimeHelp
		{
			get
			{
				return SR.ResourceManager.GetString("MqClientAveragePutTimeHelp", SR.Culture);
			}
		}

		// Token: 0x17000E87 RID: 3719
		// (get) Token: 0x06003E14 RID: 15892 RVA: 0x000D0BD0 File Offset: 0x000CEDD0
		internal static string MqClientAverageGetTimeHelp
		{
			get
			{
				return SR.ResourceManager.GetString("MqClientAverageGetTimeHelp", SR.Culture);
			}
		}

		// Token: 0x17000E88 RID: 3720
		// (get) Token: 0x06003E15 RID: 15893 RVA: 0x000D0BE6 File Offset: 0x000CEDE6
		internal static string MqClientCumulativePutsHelp
		{
			get
			{
				return SR.ResourceManager.GetString("MqClientCumulativePutsHelp", SR.Culture);
			}
		}

		// Token: 0x17000E89 RID: 3721
		// (get) Token: 0x06003E16 RID: 15894 RVA: 0x000D0BFC File Offset: 0x000CEDFC
		internal static string MqClientCumulativeGetsHelp
		{
			get
			{
				return SR.ResourceManager.GetString("MqClientCumulativeGetsHelp", SR.Culture);
			}
		}

		// Token: 0x17000E8A RID: 3722
		// (get) Token: 0x06003E17 RID: 15895 RVA: 0x000D0C12 File Offset: 0x000CEE12
		internal static string MqClientPutsPerSecondHelp
		{
			get
			{
				return SR.ResourceManager.GetString("MqClientPutsPerSecondHelp", SR.Culture);
			}
		}

		// Token: 0x17000E8B RID: 3723
		// (get) Token: 0x06003E18 RID: 15896 RVA: 0x000D0C28 File Offset: 0x000CEE28
		internal static string MqClientGetsPerSecondHelp
		{
			get
			{
				return SR.ResourceManager.GetString("MqClientGetsPerSecondHelp", SR.Culture);
			}
		}

		// Token: 0x17000E8C RID: 3724
		// (get) Token: 0x06003E19 RID: 15897 RVA: 0x000D0C3E File Offset: 0x000CEE3E
		internal static string MqClientAverageTransactionalPutTimeHelp
		{
			get
			{
				return SR.ResourceManager.GetString("MqClientAverageTransactionalPutTimeHelp", SR.Culture);
			}
		}

		// Token: 0x17000E8D RID: 3725
		// (get) Token: 0x06003E1A RID: 15898 RVA: 0x000D0C54 File Offset: 0x000CEE54
		internal static string MqClientAverageTransactionalGetTimeHelp
		{
			get
			{
				return SR.ResourceManager.GetString("MqClientAverageTransactionalGetTimeHelp", SR.Culture);
			}
		}

		// Token: 0x17000E8E RID: 3726
		// (get) Token: 0x06003E1B RID: 15899 RVA: 0x000D0C6A File Offset: 0x000CEE6A
		internal static string MqClientCumulativeTransactionalPutsHelp
		{
			get
			{
				return SR.ResourceManager.GetString("MqClientCumulativeTransactionalPutsHelp", SR.Culture);
			}
		}

		// Token: 0x17000E8F RID: 3727
		// (get) Token: 0x06003E1C RID: 15900 RVA: 0x000D0C80 File Offset: 0x000CEE80
		internal static string MqClientCumulativeTransactionalGetsHelp
		{
			get
			{
				return SR.ResourceManager.GetString("MqClientCumulativeTransactionalGetsHelp", SR.Culture);
			}
		}

		// Token: 0x17000E90 RID: 3728
		// (get) Token: 0x06003E1D RID: 15901 RVA: 0x000D0C96 File Offset: 0x000CEE96
		internal static string MqClientTransactionalPutsPerSecondHelp
		{
			get
			{
				return SR.ResourceManager.GetString("MqClientTransactionalPutsPerSecondHelp", SR.Culture);
			}
		}

		// Token: 0x17000E91 RID: 3729
		// (get) Token: 0x06003E1E RID: 15902 RVA: 0x000D0CAC File Offset: 0x000CEEAC
		internal static string MqClientTransactionalGetsPerSecondHelp
		{
			get
			{
				return SR.ResourceManager.GetString("MqClientTransactionalGetsPerSecondHelp", SR.Culture);
			}
		}

		// Token: 0x17000E92 RID: 3730
		// (get) Token: 0x06003E1F RID: 15903 RVA: 0x000D0CC2 File Offset: 0x000CEEC2
		internal static string DrdaAsPerformanceCounterCategory
		{
			get
			{
				return SR.ResourceManager.GetString("DrdaAsPerformanceCounterCategory", SR.Culture);
			}
		}

		// Token: 0x17000E93 RID: 3731
		// (get) Token: 0x06003E20 RID: 15904 RVA: 0x000D0CD8 File Offset: 0x000CEED8
		internal static string DrdaAsPerformanceCounterCategoryHelp
		{
			get
			{
				return SR.ResourceManager.GetString("DrdaAsPerformanceCounterCategoryHelp", SR.Culture);
			}
		}

		// Token: 0x17000E94 RID: 3732
		// (get) Token: 0x06003E21 RID: 15905 RVA: 0x000D0CEE File Offset: 0x000CEEEE
		internal static string DrdaAsActiveDrdaSessions
		{
			get
			{
				return SR.ResourceManager.GetString("DrdaAsActiveDrdaSessions", SR.Culture);
			}
		}

		// Token: 0x17000E95 RID: 3733
		// (get) Token: 0x06003E22 RID: 15906 RVA: 0x000D0D04 File Offset: 0x000CEF04
		internal static string DrdaAsActiveSqlConnections
		{
			get
			{
				return SR.ResourceManager.GetString("DrdaAsActiveSqlConnections", SR.Culture);
			}
		}

		// Token: 0x17000E96 RID: 3734
		// (get) Token: 0x06003E23 RID: 15907 RVA: 0x000D0D1A File Offset: 0x000CEF1A
		internal static string DrdaAsActiveTransactions
		{
			get
			{
				return SR.ResourceManager.GetString("DrdaAsActiveTransactions", SR.Culture);
			}
		}

		// Token: 0x17000E97 RID: 3735
		// (get) Token: 0x06003E24 RID: 15908 RVA: 0x000D0D30 File Offset: 0x000CEF30
		internal static string DrdaAsBytesReceivedPerSecond
		{
			get
			{
				return SR.ResourceManager.GetString("DrdaAsBytesReceivedPerSecond", SR.Culture);
			}
		}

		// Token: 0x17000E98 RID: 3736
		// (get) Token: 0x06003E25 RID: 15909 RVA: 0x000D0D46 File Offset: 0x000CEF46
		internal static string DrdaAsBytesSentPerSecond
		{
			get
			{
				return SR.ResourceManager.GetString("DrdaAsBytesSentPerSecond", SR.Culture);
			}
		}

		// Token: 0x17000E99 RID: 3737
		// (get) Token: 0x06003E26 RID: 15910 RVA: 0x000D0D5C File Offset: 0x000CEF5C
		internal static string DrdaAsTotalTransactions
		{
			get
			{
				return SR.ResourceManager.GetString("DrdaAsTotalTransactions", SR.Culture);
			}
		}

		// Token: 0x17000E9A RID: 3738
		// (get) Token: 0x06003E27 RID: 15911 RVA: 0x000D0D72 File Offset: 0x000CEF72
		internal static string DrdaAsTransactionsPerSecond
		{
			get
			{
				return SR.ResourceManager.GetString("DrdaAsTransactionsPerSecond", SR.Culture);
			}
		}

		// Token: 0x17000E9B RID: 3739
		// (get) Token: 0x06003E28 RID: 15912 RVA: 0x000D0D88 File Offset: 0x000CEF88
		internal static string DrdaAsTransactionCommitsPerSecond
		{
			get
			{
				return SR.ResourceManager.GetString("DrdaAsTransactionCommitsPerSecond", SR.Culture);
			}
		}

		// Token: 0x17000E9C RID: 3740
		// (get) Token: 0x06003E29 RID: 15913 RVA: 0x000D0D9E File Offset: 0x000CEF9E
		internal static string DrdaAsTransactionRollbacksPerSecond
		{
			get
			{
				return SR.ResourceManager.GetString("DrdaAsTransactionRollbacksPerSecond", SR.Culture);
			}
		}

		// Token: 0x17000E9D RID: 3741
		// (get) Token: 0x06003E2A RID: 15914 RVA: 0x000D0DB4 File Offset: 0x000CEFB4
		internal static string DrdaAsActiveDrdaSessionsHelp
		{
			get
			{
				return SR.ResourceManager.GetString("DrdaAsActiveDrdaSessionsHelp", SR.Culture);
			}
		}

		// Token: 0x17000E9E RID: 3742
		// (get) Token: 0x06003E2B RID: 15915 RVA: 0x000D0DCA File Offset: 0x000CEFCA
		internal static string DrdaAsActiveSqlConnectionsHelp
		{
			get
			{
				return SR.ResourceManager.GetString("DrdaAsActiveSqlConnectionsHelp", SR.Culture);
			}
		}

		// Token: 0x17000E9F RID: 3743
		// (get) Token: 0x06003E2C RID: 15916 RVA: 0x000D0DE0 File Offset: 0x000CEFE0
		internal static string DrdaAsActiveTransactionsHelp
		{
			get
			{
				return SR.ResourceManager.GetString("DrdaAsActiveTransactionsHelp", SR.Culture);
			}
		}

		// Token: 0x17000EA0 RID: 3744
		// (get) Token: 0x06003E2D RID: 15917 RVA: 0x000D0DF6 File Offset: 0x000CEFF6
		internal static string DrdaAsBytesReceivedPerSecondHelp
		{
			get
			{
				return SR.ResourceManager.GetString("DrdaAsBytesReceivedPerSecondHelp", SR.Culture);
			}
		}

		// Token: 0x17000EA1 RID: 3745
		// (get) Token: 0x06003E2E RID: 15918 RVA: 0x000D0E0C File Offset: 0x000CF00C
		internal static string DrdaAsBytesSentPerSecondHelp
		{
			get
			{
				return SR.ResourceManager.GetString("DrdaAsBytesSentPerSecondHelp", SR.Culture);
			}
		}

		// Token: 0x17000EA2 RID: 3746
		// (get) Token: 0x06003E2F RID: 15919 RVA: 0x000D0E22 File Offset: 0x000CF022
		internal static string DrdaAsTotalTransactionsHelp
		{
			get
			{
				return SR.ResourceManager.GetString("DrdaAsTotalTransactionsHelp", SR.Culture);
			}
		}

		// Token: 0x17000EA3 RID: 3747
		// (get) Token: 0x06003E30 RID: 15920 RVA: 0x000D0E38 File Offset: 0x000CF038
		internal static string DrdaAsTransactionsPerSecondHelp
		{
			get
			{
				return SR.ResourceManager.GetString("DrdaAsTransactionsPerSecondHelp", SR.Culture);
			}
		}

		// Token: 0x17000EA4 RID: 3748
		// (get) Token: 0x06003E31 RID: 15921 RVA: 0x000D0E4E File Offset: 0x000CF04E
		internal static string DrdaAsTransactionCommitsPerSecondHelp
		{
			get
			{
				return SR.ResourceManager.GetString("DrdaAsTransactionCommitsPerSecondHelp", SR.Culture);
			}
		}

		// Token: 0x17000EA5 RID: 3749
		// (get) Token: 0x06003E32 RID: 15922 RVA: 0x000D0E64 File Offset: 0x000CF064
		internal static string DrdaAsTransactionRollbacksPerSecondHelp
		{
			get
			{
				return SR.ResourceManager.GetString("DrdaAsTransactionRollbacksPerSecondHelp", SR.Culture);
			}
		}

		// Token: 0x17000EA6 RID: 3750
		// (get) Token: 0x06003E33 RID: 15923 RVA: 0x000D0E7A File Offset: 0x000CF07A
		internal static string DrdaArPerformanceCounterCategory
		{
			get
			{
				return SR.ResourceManager.GetString("DrdaArPerformanceCounterCategory", SR.Culture);
			}
		}

		// Token: 0x17000EA7 RID: 3751
		// (get) Token: 0x06003E34 RID: 15924 RVA: 0x000D0E90 File Offset: 0x000CF090
		internal static string DrdaArPerformanceCounterCategoryHelp
		{
			get
			{
				return SR.ResourceManager.GetString("DrdaArPerformanceCounterCategoryHelp", SR.Culture);
			}
		}

		// Token: 0x17000EA8 RID: 3752
		// (get) Token: 0x06003E35 RID: 15925 RVA: 0x000D0EA6 File Offset: 0x000CF0A6
		internal static string DrdaArActiveDrdaSessions
		{
			get
			{
				return SR.ResourceManager.GetString("DrdaArActiveDrdaSessions", SR.Culture);
			}
		}

		// Token: 0x17000EA9 RID: 3753
		// (get) Token: 0x06003E36 RID: 15926 RVA: 0x000D0EBC File Offset: 0x000CF0BC
		internal static string DrdaArOpenStatements
		{
			get
			{
				return SR.ResourceManager.GetString("DrdaArOpenStatements", SR.Culture);
			}
		}

		// Token: 0x17000EAA RID: 3754
		// (get) Token: 0x06003E37 RID: 15927 RVA: 0x000D0ED2 File Offset: 0x000CF0D2
		internal static string DrdaArAverageHostProcessingTime
		{
			get
			{
				return SR.ResourceManager.GetString("DrdaArAverageHostProcessingTime", SR.Culture);
			}
		}

		// Token: 0x17000EAB RID: 3755
		// (get) Token: 0x06003E38 RID: 15928 RVA: 0x000D0EE8 File Offset: 0x000CF0E8
		internal static string DrdaArBytesReceivedPerSecond
		{
			get
			{
				return SR.ResourceManager.GetString("DrdaArBytesReceivedPerSecond", SR.Culture);
			}
		}

		// Token: 0x17000EAC RID: 3756
		// (get) Token: 0x06003E39 RID: 15929 RVA: 0x000D0EFE File Offset: 0x000CF0FE
		internal static string DrdaArBytesSentPerSecond
		{
			get
			{
				return SR.ResourceManager.GetString("DrdaArBytesSentPerSecond", SR.Culture);
			}
		}

		// Token: 0x17000EAD RID: 3757
		// (get) Token: 0x06003E3A RID: 15930 RVA: 0x000D0F14 File Offset: 0x000CF114
		internal static string DrdaArTotalTransactions
		{
			get
			{
				return SR.ResourceManager.GetString("DrdaArTotalTransactions", SR.Culture);
			}
		}

		// Token: 0x17000EAE RID: 3758
		// (get) Token: 0x06003E3B RID: 15931 RVA: 0x000D0F2A File Offset: 0x000CF12A
		internal static string DrdaArTransactionsPerSecond
		{
			get
			{
				return SR.ResourceManager.GetString("DrdaArTransactionsPerSecond", SR.Culture);
			}
		}

		// Token: 0x17000EAF RID: 3759
		// (get) Token: 0x06003E3C RID: 15932 RVA: 0x000D0F40 File Offset: 0x000CF140
		internal static string DrdaArTransactionCommitsPerSecond
		{
			get
			{
				return SR.ResourceManager.GetString("DrdaArTransactionCommitsPerSecond", SR.Culture);
			}
		}

		// Token: 0x17000EB0 RID: 3760
		// (get) Token: 0x06003E3D RID: 15933 RVA: 0x000D0F56 File Offset: 0x000CF156
		internal static string DrdaArTransactionRollbacksPerSecond
		{
			get
			{
				return SR.ResourceManager.GetString("DrdaArTransactionRollbacksPerSecond", SR.Culture);
			}
		}

		// Token: 0x17000EB1 RID: 3761
		// (get) Token: 0x06003E3E RID: 15934 RVA: 0x000D0F6C File Offset: 0x000CF16C
		internal static string DrdaArActiveDrdaSessionsHelp
		{
			get
			{
				return SR.ResourceManager.GetString("DrdaArActiveDrdaSessionsHelp", SR.Culture);
			}
		}

		// Token: 0x17000EB2 RID: 3762
		// (get) Token: 0x06003E3F RID: 15935 RVA: 0x000D0F82 File Offset: 0x000CF182
		internal static string DrdaArOpenStatementsHelp
		{
			get
			{
				return SR.ResourceManager.GetString("DrdaArOpenStatementsHelp", SR.Culture);
			}
		}

		// Token: 0x17000EB3 RID: 3763
		// (get) Token: 0x06003E40 RID: 15936 RVA: 0x000D0F98 File Offset: 0x000CF198
		internal static string DrdaArAverageHostProcessingTimeHelp
		{
			get
			{
				return SR.ResourceManager.GetString("DrdaArAverageHostProcessingTimeHelp", SR.Culture);
			}
		}

		// Token: 0x17000EB4 RID: 3764
		// (get) Token: 0x06003E41 RID: 15937 RVA: 0x000D0FAE File Offset: 0x000CF1AE
		internal static string DrdaArBytesReceivedPerSecondHelp
		{
			get
			{
				return SR.ResourceManager.GetString("DrdaArBytesReceivedPerSecondHelp", SR.Culture);
			}
		}

		// Token: 0x17000EB5 RID: 3765
		// (get) Token: 0x06003E42 RID: 15938 RVA: 0x000D0FC4 File Offset: 0x000CF1C4
		internal static string DrdaArBytesSentPerSecondHelp
		{
			get
			{
				return SR.ResourceManager.GetString("DrdaArBytesSentPerSecondHelp", SR.Culture);
			}
		}

		// Token: 0x17000EB6 RID: 3766
		// (get) Token: 0x06003E43 RID: 15939 RVA: 0x000D0FDA File Offset: 0x000CF1DA
		internal static string DrdaArTotalTransactionsHelp
		{
			get
			{
				return SR.ResourceManager.GetString("DrdaArTotalTransactionsHelp", SR.Culture);
			}
		}

		// Token: 0x17000EB7 RID: 3767
		// (get) Token: 0x06003E44 RID: 15940 RVA: 0x000D0FF0 File Offset: 0x000CF1F0
		internal static string DrdaArTransactionsPerSecondHelp
		{
			get
			{
				return SR.ResourceManager.GetString("DrdaArTransactionsPerSecondHelp", SR.Culture);
			}
		}

		// Token: 0x17000EB8 RID: 3768
		// (get) Token: 0x06003E45 RID: 15941 RVA: 0x000D1006 File Offset: 0x000CF206
		internal static string DrdaArTransactionCommitsPerSecondHelp
		{
			get
			{
				return SR.ResourceManager.GetString("DrdaArTransactionCommitsPerSecondHelp", SR.Culture);
			}
		}

		// Token: 0x17000EB9 RID: 3769
		// (get) Token: 0x06003E46 RID: 15942 RVA: 0x000D101C File Offset: 0x000CF21C
		internal static string DrdaArTransactionRollbacksPerSecondHelp
		{
			get
			{
				return SR.ResourceManager.GetString("DrdaArTransactionRollbacksPerSecondHelp", SR.Culture);
			}
		}

		// Token: 0x040024E5 RID: 9445
		private static ResourceManager resourceManager;

		// Token: 0x040024E6 RID: 9446
		private static CultureInfo resourceCulture;
	}
}
