using System;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;
using Microsoft.ReportingServices.Exceptions;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000C9 RID: 201
	internal class ErrorStringsWrapper
	{
		// Token: 0x060002DC RID: 732 RVA: 0x00005AA3 File Offset: 0x00003CA3
		protected ErrorStringsWrapper()
		{
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060002DD RID: 733 RVA: 0x00005AAB File Offset: 0x00003CAB
		// (set) Token: 0x060002DE RID: 734 RVA: 0x00005AB2 File Offset: 0x00003CB2
		public static CultureInfo Culture
		{
			get
			{
				return ErrorStringsWrapper.Keys.Culture;
			}
			set
			{
				ErrorStringsWrapper.Keys.Culture = value;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060002DF RID: 735 RVA: 0x00005ABA File Offset: 0x00003CBA
		public static string InvalidKeyValue
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("InvalidKeyValue");
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x00005AC6 File Offset: 0x00003CC6
		public static string EmptyExtensionName
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("EmptyExtensionName");
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x00005AD2 File Offset: 0x00003CD2
		public static string UIServerLoopback
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("UIServerLoopback");
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x00005ADE File Offset: 0x00003CDE
		public static string DataSourceConnectionErrorNotVisible
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("DataSourceConnectionErrorNotVisible");
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x00005AEA File Offset: 0x00003CEA
		public static string UserNameUnknown
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("UserNameUnknown");
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x00005AF6 File Offset: 0x00003CF6
		public static string rsInvalidParameterCombination
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsInvalidParameterCombination");
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x00005B02 File Offset: 0x00003D02
		public static string rsProcessingError
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsProcessingError");
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x00005B0E File Offset: 0x00003D0E
		public static string rsStreamNotFound
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsStreamNotFound");
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x00005B1A File Offset: 0x00003D1A
		public static string rsMissingSessionId
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsMissingSessionId");
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x00005B26 File Offset: 0x00003D26
		public static string rsQueryExecutionNotAllowed
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsQueryExecutionNotAllowed");
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x00005B32 File Offset: 0x00003D32
		public static string rsReportNotReady
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsReportNotReady");
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060002EA RID: 746 RVA: 0x00005B3E File Offset: 0x00003D3E
		public static string rsReportSnapshotEnabled
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsReportSnapshotEnabled");
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060002EB RID: 747 RVA: 0x00005B4A File Offset: 0x00003D4A
		public static string rsReportSnapshotNotEnabled
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsReportSnapshotNotEnabled");
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060002EC RID: 748 RVA: 0x00005B56 File Offset: 0x00003D56
		public static string rsOperationPreventsUnattendedExecution
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsOperationPreventsUnattendedExecution");
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060002ED RID: 749 RVA: 0x00005B62 File Offset: 0x00003D62
		public static string rsInvalidReportLink
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsInvalidReportLink");
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060002EE RID: 750 RVA: 0x00005B6E File Offset: 0x00003D6E
		public static string rsSubreportFromSnapshot
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsSubreportFromSnapshot");
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060002EF RID: 751 RVA: 0x00005B7A File Offset: 0x00003D7A
		public static string rsQueryTimeout
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsQueryTimeout");
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x00005B86 File Offset: 0x00003D86
		public static string rsSchedulerNotResponding
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsSchedulerNotResponding");
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x00005B92 File Offset: 0x00003D92
		public static string rsSchedulerNotRespondingPreventsPinning
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsSchedulerNotRespondingPreventsPinning");
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x00005B9E File Offset: 0x00003D9E
		public static string rsScheduleDateTimeRangeException
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsScheduleDateTimeRangeException");
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x00005BAA File Offset: 0x00003DAA
		public static string rsUserCannotOwnSubscription
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsUserCannotOwnSubscription");
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x00005BB6 File Offset: 0x00003DB6
		public static string rsCannotActivateSubscription
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsCannotActivateSubscription");
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x00005BC2 File Offset: 0x00003DC2
		public static string rsDeliveryExtensionNotFound
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsDeliveryExtensionNotFound");
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x00005BCE File Offset: 0x00003DCE
		public static string rsDeliverError
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsDeliverError");
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x00005BDA File Offset: 0x00003DDA
		public static string rsCannotPrepareQuery
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsCannotPrepareQuery");
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x00005BE6 File Offset: 0x00003DE6
		public static string rsMixedTasks
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsMixedTasks");
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x00005BF2 File Offset: 0x00003DF2
		public static string rsEmptyRole
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsEmptyRole");
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060002FA RID: 762 RVA: 0x00005BFE File Offset: 0x00003DFE
		public static string rsCannotDeleteRootPolicy
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsCannotDeleteRootPolicy");
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060002FB RID: 763 RVA: 0x00005C0A File Offset: 0x00003E0A
		public static string rsSecureConnectionRequired
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsSecureConnectionRequired");
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060002FC RID: 764 RVA: 0x00005C16 File Offset: 0x00003E16
		public static string rsModelRootPolicyRequired
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsModelRootPolicyRequired");
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060002FD RID: 765 RVA: 0x00005C22 File Offset: 0x00003E22
		public static string rsModelIDMismatch
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsModelIDMismatch");
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060002FE RID: 766 RVA: 0x00005C2E File Offset: 0x00003E2E
		public static string rsModelNotGenerated
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsModelNotGenerated");
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060002FF RID: 767 RVA: 0x00005C3A File Offset: 0x00003E3A
		public static string rsModelGenerationNotSupported
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsModelGenerationNotSupported");
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000300 RID: 768 RVA: 0x00005C46 File Offset: 0x00003E46
		public static string rsModelGenerationError
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsModelGenerationError");
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000301 RID: 769 RVA: 0x00005C52 File Offset: 0x00003E52
		public static string rsReportServerDatabaseUnavailable
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsReportServerDatabaseUnavailable");
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000302 RID: 770 RVA: 0x00005C5E File Offset: 0x00003E5E
		public static string rsReportServerDatabaseError
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsReportServerDatabaseError");
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000303 RID: 771 RVA: 0x00005C6A File Offset: 0x00003E6A
		public static string rsEvaluationCopyExpired
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsEvaluationCopyExpired");
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000304 RID: 772 RVA: 0x00005C76 File Offset: 0x00003E76
		public static string rsServerBusy
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsServerBusy");
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000305 RID: 773 RVA: 0x00005C82 File Offset: 0x00003E82
		public static string rsReportServerDisabled
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsReportServerDisabled");
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000306 RID: 774 RVA: 0x00005C8E File Offset: 0x00003E8E
		public static string rsKeyStateNotValid
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsKeyStateNotValid");
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000307 RID: 775 RVA: 0x00005C9A File Offset: 0x00003E9A
		public static string rsReportServerNotActivated
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsReportServerNotActivated");
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000308 RID: 776 RVA: 0x00005CA6 File Offset: 0x00003EA6
		public static string rsAccessDeniedToSecureData
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsAccessDeniedToSecureData");
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000309 RID: 777 RVA: 0x00005CB2 File Offset: 0x00003EB2
		public static string rsCannotValidateEncryptedData
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsCannotValidateEncryptedData");
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x0600030A RID: 778 RVA: 0x00005CBE File Offset: 0x00003EBE
		public static string rsRemotePublicKeyUnavailable
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsRemotePublicKeyUnavailable");
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x0600030B RID: 779 RVA: 0x00005CCA File Offset: 0x00003ECA
		public static string rsFailedToExportSymmetricKey
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsFailedToExportSymmetricKey");
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x0600030C RID: 780 RVA: 0x00005CD6 File Offset: 0x00003ED6
		public static string rsBackupKeyPasswordInvalid
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsBackupKeyPasswordInvalid");
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x0600030D RID: 781 RVA: 0x00005CE2 File Offset: 0x00003EE2
		public static string rsInternalResourceNotSpecifiedError
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsInternalResourceNotSpecifiedError");
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x0600030E RID: 782 RVA: 0x00005CEE File Offset: 0x00003EEE
		public static string SkuNonSqlDataSources
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("SkuNonSqlDataSources");
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600030F RID: 783 RVA: 0x00005CFA File Offset: 0x00003EFA
		public static string SkuOtherSkuDatasources
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("SkuOtherSkuDatasources");
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000310 RID: 784 RVA: 0x00005D06 File Offset: 0x00003F06
		public static string SkuRemoteDataSources
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("SkuRemoteDataSources");
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000311 RID: 785 RVA: 0x00005D12 File Offset: 0x00003F12
		public static string SkuCaching
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("SkuCaching");
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000312 RID: 786 RVA: 0x00005D1E File Offset: 0x00003F1E
		public static string SkuExecutionSnapshots
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("SkuExecutionSnapshots");
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000313 RID: 787 RVA: 0x00005D2A File Offset: 0x00003F2A
		public static string SkuHistory
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("SkuHistory");
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000314 RID: 788 RVA: 0x00005D36 File Offset: 0x00003F36
		public static string SkuDelivery
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("SkuDelivery");
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000315 RID: 789 RVA: 0x00005D42 File Offset: 0x00003F42
		public static string SkuScheduling
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("SkuScheduling");
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000316 RID: 790 RVA: 0x00005D4E File Offset: 0x00003F4E
		public static string SkuExtensibility
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("SkuExtensibility");
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000317 RID: 791 RVA: 0x00005D5A File Offset: 0x00003F5A
		public static string SkuCustomAuth
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("SkuCustomAuth");
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000318 RID: 792 RVA: 0x00005D66 File Offset: 0x00003F66
		public static string SkuSharepoint
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("SkuSharepoint");
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000319 RID: 793 RVA: 0x00005D72 File Offset: 0x00003F72
		public static string SkuScaleOut
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("SkuScaleOut");
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x0600031A RID: 794 RVA: 0x00005D7E File Offset: 0x00003F7E
		public static string SkuSubscriptions
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("SkuSubscriptions");
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600031B RID: 795 RVA: 0x00005D8A File Offset: 0x00003F8A
		public static string SkuDataDrivenSubscriptions
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("SkuDataDrivenSubscriptions");
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x0600031C RID: 796 RVA: 0x00005D96 File Offset: 0x00003F96
		public static string SkuCustomRolesSecurity
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("SkuCustomRolesSecurity");
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600031D RID: 797 RVA: 0x00005DA2 File Offset: 0x00003FA2
		public static string SkuReportBuilder
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("SkuReportBuilder");
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x0600031E RID: 798 RVA: 0x00005DAE File Offset: 0x00003FAE
		public static string SkuModelItemSecurity
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("SkuModelItemSecurity");
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x0600031F RID: 799 RVA: 0x00005DBA File Offset: 0x00003FBA
		public static string SkuDynamicDrillthrough
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("SkuDynamicDrillthrough");
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000320 RID: 800 RVA: 0x00005DC6 File Offset: 0x00003FC6
		public static string SkuNoCpuThrottling
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("SkuNoCpuThrottling");
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000321 RID: 801 RVA: 0x00005DD2 File Offset: 0x00003FD2
		public static string SkuNoMemoryThrottling
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("SkuNoMemoryThrottling");
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000322 RID: 802 RVA: 0x00005DDE File Offset: 0x00003FDE
		public static string SkuEventGeneration
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("SkuEventGeneration");
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000323 RID: 803 RVA: 0x00005DEA File Offset: 0x00003FEA
		public static string SkuComponentLibrary
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("SkuComponentLibrary");
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000324 RID: 804 RVA: 0x00005DF6 File Offset: 0x00003FF6
		public static string SkuSharedDataset
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("SkuSharedDataset");
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000325 RID: 805 RVA: 0x00005E02 File Offset: 0x00004002
		public static string SkuDataAlerting
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("SkuDataAlerting");
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000326 RID: 806 RVA: 0x00005E0E File Offset: 0x0000400E
		public static string SkuCrescent
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("SkuCrescent");
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000327 RID: 807 RVA: 0x00005E1A File Offset: 0x0000401A
		public static string SkuKpiItems
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("SkuKpiItems");
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000328 RID: 808 RVA: 0x00005E26 File Offset: 0x00004026
		public static string SkuCommentAlerting
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("SkuCommentAlerting");
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000329 RID: 809 RVA: 0x00005E32 File Offset: 0x00004032
		public static string rsSharePointError
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsSharePointError");
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x0600032A RID: 810 RVA: 0x00005E3E File Offset: 0x0000403E
		public static string rsSharePointContentDBAccessError
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsSharePointContentDBAccessError");
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x0600032B RID: 811 RVA: 0x00005E4A File Offset: 0x0000404A
		public static string rsODCVersionNotSupported
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsODCVersionNotSupported");
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x0600032C RID: 812 RVA: 0x00005E56 File Offset: 0x00004056
		public static string rsOperationNotSupportedSharePointMode
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsOperationNotSupportedSharePointMode");
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600032D RID: 813 RVA: 0x00005E62 File Offset: 0x00004062
		public static string rsOperationNotSupportedNativeMode
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsOperationNotSupportedNativeMode");
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x0600032E RID: 814 RVA: 0x00005E6E File Offset: 0x0000406E
		public static string rsContainerNotSupported
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsContainerNotSupported");
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x0600032F RID: 815 RVA: 0x00005E7A File Offset: 0x0000407A
		public static string rsPropertyDisabled
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsPropertyDisabled");
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000330 RID: 816 RVA: 0x00005E86 File Offset: 0x00004086
		public static string rsPropertyDisabledNativeMode
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsPropertyDisabledNativeMode");
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000331 RID: 817 RVA: 0x00005E92 File Offset: 0x00004092
		public static string rsInvalidRSDSSchema
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsInvalidRSDSSchema");
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000332 RID: 818 RVA: 0x00005E9E File Offset: 0x0000409E
		public static string rsSecurityZoneNotSupported
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsSecurityZoneNotSupported");
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000333 RID: 819 RVA: 0x00005EAA File Offset: 0x000040AA
		public static string rsFileSize
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsFileSize");
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000334 RID: 820 RVA: 0x00005EB6 File Offset: 0x000040B6
		public static string rsFileSizeNotSupported
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsFileSizeNotSupported");
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000335 RID: 821 RVA: 0x00005EC2 File Offset: 0x000040C2
		public static string rsRdceInvalidRdlError
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsRdceInvalidRdlError");
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000336 RID: 822 RVA: 0x00005ECE File Offset: 0x000040CE
		public static string rsRdceInvalidConfigurationError
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsRdceInvalidConfigurationError");
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000337 RID: 823 RVA: 0x00005EDA File Offset: 0x000040DA
		public static string rsRdceInvalidExecutionOptionError
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsRdceInvalidExecutionOptionError");
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000338 RID: 824 RVA: 0x00005EE6 File Offset: 0x000040E6
		public static string rsRdceInvalidCacheOptionError
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsRdceInvalidCacheOptionError");
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000339 RID: 825 RVA: 0x00005EF2 File Offset: 0x000040F2
		public static string rsRdceWrappedException
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsRdceWrappedException");
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x0600033A RID: 826 RVA: 0x00005EFE File Offset: 0x000040FE
		public static string rsRdceMismatchRdlVersion
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsRdceMismatchRdlVersion");
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x0600033B RID: 827 RVA: 0x00005F0A File Offset: 0x0000410A
		public static string rsInvalidOperation
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsInvalidOperation");
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x0600033C RID: 828 RVA: 0x00005F16 File Offset: 0x00004116
		public static string rsAuthorizationExtensionError
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsAuthorizationExtensionError");
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x0600033D RID: 829 RVA: 0x00005F22 File Offset: 0x00004122
		public static string rsDataCacheMismatch
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsDataCacheMismatch");
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600033E RID: 830 RVA: 0x00005F2E File Offset: 0x0000412E
		public static string rsSoapExtensionInvalidPreambleError
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsSoapExtensionInvalidPreambleError");
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x0600033F RID: 831 RVA: 0x00005F3A File Offset: 0x0000413A
		public static string rsRequestThroughHttpRedirectorNotSupportedError
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsRequestThroughHttpRedirectorNotSupportedError");
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000340 RID: 832 RVA: 0x00005F46 File Offset: 0x00004146
		public static string rsUnhandledHttpApplicationError
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsUnhandledHttpApplicationError");
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000341 RID: 833 RVA: 0x00005F52 File Offset: 0x00004152
		public static string rsInvalidCatalogRecord
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsInvalidCatalogRecord");
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000342 RID: 834 RVA: 0x00005F5E File Offset: 0x0000415E
		public static string rsClaimsToWindowsTokenError
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsClaimsToWindowsTokenError");
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000343 RID: 835 RVA: 0x00005F6A File Offset: 0x0000416A
		public static string rsClaimsToWindowsTokenLoginTypeError
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsClaimsToWindowsTokenLoginTypeError");
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000344 RID: 836 RVA: 0x00005F76 File Offset: 0x00004176
		public static string GetExternalImagesInvalidNamespace
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("GetExternalImagesInvalidNamespace");
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000345 RID: 837 RVA: 0x00005F82 File Offset: 0x00004182
		public static string GetExternalImagesInvalidSyntax
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("GetExternalImagesInvalidSyntax");
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000346 RID: 838 RVA: 0x00005F8E File Offset: 0x0000418E
		public static string rsSecureStoreContextUrlNotSpecified
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsSecureStoreContextUrlNotSpecified");
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000347 RID: 839 RVA: 0x00005F9A File Offset: 0x0000419A
		public static string rsSecureStoreInvalidLookupContext
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsSecureStoreInvalidLookupContext");
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000348 RID: 840 RVA: 0x00005FA6 File Offset: 0x000041A6
		public static string rsSecureStoreUnsupportedSharePointVersion
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsSecureStoreUnsupportedSharePointVersion");
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000349 RID: 841 RVA: 0x00005FB2 File Offset: 0x000041B2
		public static string ProductNameSSRS
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("ProductNameSSRS");
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x0600034A RID: 842 RVA: 0x00005FBE File Offset: 0x000041BE
		public static string ProductNamePBIRS
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("ProductNamePBIRS");
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x0600034B RID: 843 RVA: 0x00005FCA File Offset: 0x000041CA
		public static string rsInvalidXml
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsInvalidXml");
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x0600034C RID: 844 RVA: 0x00005FD6 File Offset: 0x000041D6
		public static string rsSnapshotVersionMismatch
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsSnapshotVersionMismatch");
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x0600034D RID: 845 RVA: 0x00005FE2 File Offset: 0x000041E2
		public static string rsInvalidDataSourceCredentialSetting
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsInvalidDataSourceCredentialSetting");
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x0600034E RID: 846 RVA: 0x00005FEE File Offset: 0x000041EE
		public static string rsDatasourceCredentialsNoLongerValid
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsDatasourceCredentialsNoLongerValid");
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x0600034F RID: 847 RVA: 0x00005FFA File Offset: 0x000041FA
		public static string rsInvalidDataSourceCredentialSettingForITokenDataExtension
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsInvalidDataSourceCredentialSettingForITokenDataExtension");
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000350 RID: 848 RVA: 0x00006006 File Offset: 0x00004206
		public static string rsWindowsIntegratedSecurityDisabled
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsWindowsIntegratedSecurityDisabled");
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000351 RID: 849 RVA: 0x00006012 File Offset: 0x00004212
		public static string internalDataSourceNotFound
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("internalDataSourceNotFound");
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000352 RID: 850 RVA: 0x0000601E File Offset: 0x0000421E
		public static string rsDataSourceDisabled
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsDataSourceDisabled");
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000353 RID: 851 RVA: 0x0000602A File Offset: 0x0000422A
		public static string rsModelRetrievalCanceled
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsModelRetrievalCanceled");
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000354 RID: 852 RVA: 0x00006036 File Offset: 0x00004236
		public static string rsInternalError
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsInternalError");
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000355 RID: 853 RVA: 0x00006042 File Offset: 0x00004242
		public static string rsStreamOperationFailed
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsStreamOperationFailed");
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000356 RID: 854 RVA: 0x0000604E File Offset: 0x0000424E
		public static string rsNotSupported
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsNotSupported");
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000357 RID: 855 RVA: 0x0000605A File Offset: 0x0000425A
		public static string rsNotEnabled
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsNotEnabled");
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000358 RID: 856 RVA: 0x00006066 File Offset: 0x00004266
		public static string rsReportServerDatabaseLogonFailed
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsReportServerDatabaseLogonFailed");
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000359 RID: 857 RVA: 0x00006072 File Offset: 0x00004272
		public static string rsReportTimeoutExpired
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsReportTimeoutExpired");
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x0600035A RID: 858 RVA: 0x0000607E File Offset: 0x0000427E
		public static string rsJobWasCanceled
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsJobWasCanceled");
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x0600035B RID: 859 RVA: 0x0000608A File Offset: 0x0000428A
		public static string rsLogonFailed
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsLogonFailed");
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x0600035C RID: 860 RVA: 0x00006096 File Offset: 0x00004296
		public static string rsEncryptedDataUnavailable
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsEncryptedDataUnavailable");
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x0600035D RID: 861 RVA: 0x000060A2 File Offset: 0x000042A2
		public static string rsErrorNotVisibleToRemoteBrowsers
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsErrorNotVisibleToRemoteBrowsers");
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x0600035E RID: 862 RVA: 0x000060AE File Offset: 0x000042AE
		public static string rsFileExtensionRequired
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsFileExtensionRequired");
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x0600035F RID: 863 RVA: 0x000060BA File Offset: 0x000042BA
		public static string rsComponentPublishingError
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsComponentPublishingError");
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000360 RID: 864 RVA: 0x000060C6 File Offset: 0x000042C6
		public static string LogClientTraceEventsInvalidSyntax
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("LogClientTraceEventsInvalidSyntax");
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000361 RID: 865 RVA: 0x000060D2 File Offset: 0x000042D2
		public static string LogClientTraceEventsInvalidNamespace
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("LogClientTraceEventsInvalidNamespace");
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000362 RID: 866 RVA: 0x000060DE File Offset: 0x000042DE
		public static string rsVersionMismatch
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsVersionMismatch");
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000363 RID: 867 RVA: 0x000060EA File Offset: 0x000042EA
		public static string rsClosingRegisteredStreamException
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsClosingRegisteredStreamException");
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000364 RID: 868 RVA: 0x000060F6 File Offset: 0x000042F6
		public static string rsReportSerializationError
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsReportSerializationError");
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000365 RID: 869 RVA: 0x00006102 File Offset: 0x00004302
		public static string MultipleSessionCatalogItemsNotSupported
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("MultipleSessionCatalogItemsNotSupported");
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000366 RID: 870 RVA: 0x0000610E File Offset: 0x0000430E
		public static string rsRequestEncodingFormatException
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsRequestEncodingFormatException");
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000367 RID: 871 RVA: 0x0000611A File Offset: 0x0000431A
		public static string rsOnPremConnectionBuilderUnknownError
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsOnPremConnectionBuilderUnknownError");
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000368 RID: 872 RVA: 0x00006126 File Offset: 0x00004326
		public static string rsOnPremConnectionBuilderConnectionStringMissing
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsOnPremConnectionBuilderConnectionStringMissing");
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000369 RID: 873 RVA: 0x00006132 File Offset: 0x00004332
		public static string rsOnPremConnectionBuilderMissingEffectiveUsername
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsOnPremConnectionBuilderMissingEffectiveUsername");
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x0600036A RID: 874 RVA: 0x0000613E File Offset: 0x0000433E
		public static string rsSystemResourcePackageMetadataNotFound
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsSystemResourcePackageMetadataNotFound");
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x0600036B RID: 875 RVA: 0x0000614A File Offset: 0x0000434A
		public static string rsSystemResourcePackageMetadataInvalid
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsSystemResourcePackageMetadataInvalid");
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x0600036C RID: 876 RVA: 0x00006156 File Offset: 0x00004356
		public static string rsSystemResourcePackageValidationFailed
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsSystemResourcePackageValidationFailed");
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x0600036D RID: 877 RVA: 0x00006162 File Offset: 0x00004362
		public static string rsAuthorizationTokenInvalidOrExpired
		{
			get
			{
				return ErrorStringsWrapper.Keys.GetString("rsAuthorizationTokenInvalidOrExpired");
			}
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000616E File Offset: 0x0000436E
		public static string InvalidConfigElement(string element)
		{
			return ErrorStringsWrapper.Keys.GetString("InvalidConfigElement", element);
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0000617B File Offset: 0x0000437B
		public static string CouldNotFindElement(string element)
		{
			return ErrorStringsWrapper.Keys.GetString("CouldNotFindElement", element);
		}

		// Token: 0x06000370 RID: 880 RVA: 0x00006188 File Offset: 0x00004388
		public static string DuplicateConfigElement(string element)
		{
			return ErrorStringsWrapper.Keys.GetString("DuplicateConfigElement", element);
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00006195 File Offset: 0x00004395
		public static string SameExtensionName(string name)
		{
			return ErrorStringsWrapper.Keys.GetString("SameExtensionName", name);
		}

		// Token: 0x06000372 RID: 882 RVA: 0x000061A2 File Offset: 0x000043A2
		public static string SameEventType(string type)
		{
			return ErrorStringsWrapper.Keys.GetString("SameEventType", type);
		}

		// Token: 0x06000373 RID: 883 RVA: 0x000061AF File Offset: 0x000043AF
		public static string NoEventForEventProcessor(string name)
		{
			return ErrorStringsWrapper.Keys.GetString("NoEventForEventProcessor", name);
		}

		// Token: 0x06000374 RID: 884 RVA: 0x000061BC File Offset: 0x000043BC
		public static string rsEventExtensionNotFoundException(string @event)
		{
			return ErrorStringsWrapper.Keys.GetString("rsEventExtensionNotFoundException", @event);
		}

		// Token: 0x06000375 RID: 885 RVA: 0x000061C9 File Offset: 0x000043C9
		public static string rsEventMaxRetryExceededException(string @event)
		{
			return ErrorStringsWrapper.Keys.GetString("rsEventMaxRetryExceededException", @event);
		}

		// Token: 0x06000376 RID: 886 RVA: 0x000061D6 File Offset: 0x000043D6
		public static string rsMissingRequiredPropertyForItemType(string propertyName)
		{
			return ErrorStringsWrapper.Keys.GetString("rsMissingRequiredPropertyForItemType", propertyName);
		}

		// Token: 0x06000377 RID: 887 RVA: 0x000061E3 File Offset: 0x000043E3
		public static string rsParameterTypeMismatch(string parameterName)
		{
			return ErrorStringsWrapper.Keys.GetString("rsParameterTypeMismatch", parameterName);
		}

		// Token: 0x06000378 RID: 888 RVA: 0x000061F0 File Offset: 0x000043F0
		public static string rsStoredParameterNotFound(string StoredParameterID)
		{
			return ErrorStringsWrapper.Keys.GetString("rsStoredParameterNotFound", StoredParameterID);
		}

		// Token: 0x06000379 RID: 889 RVA: 0x000061FD File Offset: 0x000043FD
		public static string rsItemAlreadyExists(string itemPath)
		{
			return ErrorStringsWrapper.Keys.GetString("rsItemAlreadyExists", itemPath);
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0000620A File Offset: 0x0000440A
		public static string rsInvalidMove(string itemPath, string targetPath)
		{
			return ErrorStringsWrapper.Keys.GetString("rsInvalidMove", itemPath, targetPath);
		}

		// Token: 0x0600037B RID: 891 RVA: 0x00006218 File Offset: 0x00004418
		public static string rsInvalidDestination(string sourcePath, string targetPath)
		{
			return ErrorStringsWrapper.Keys.GetString("rsInvalidDestination", sourcePath, targetPath);
		}

		// Token: 0x0600037C RID: 892 RVA: 0x00006226 File Offset: 0x00004426
		public static string rsReservedItem(string itemPath)
		{
			return ErrorStringsWrapper.Keys.GetString("rsReservedItem", itemPath);
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00006233 File Offset: 0x00004433
		public static string rsReadOnlyProperty(string property)
		{
			return ErrorStringsWrapper.Keys.GetString("rsReadOnlyProperty", property);
		}

		// Token: 0x0600037E RID: 894 RVA: 0x00006240 File Offset: 0x00004440
		public static string rsExecutionNotFound(string executionID)
		{
			return ErrorStringsWrapper.Keys.GetString("rsExecutionNotFound", executionID);
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0000624D File Offset: 0x0000444D
		public static string rsSPSiteNotFound(string id)
		{
			return ErrorStringsWrapper.Keys.GetString("rsSPSiteNotFound", id);
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000625A File Offset: 0x0000445A
		public static string rsCachingNotEnabled(string item)
		{
			return ErrorStringsWrapper.Keys.GetString("rsCachingNotEnabled", item);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x00006267 File Offset: 0x00004467
		public static string rsInvalidSearchOperator(string operation)
		{
			return ErrorStringsWrapper.Keys.GetString("rsInvalidSearchOperator", operation);
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00006274 File Offset: 0x00004474
		public static string rsReportHistoryNotFound(string reportPath, string snapshotId)
		{
			return ErrorStringsWrapper.Keys.GetString("rsReportHistoryNotFound", reportPath, snapshotId);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00006282 File Offset: 0x00004482
		public static string rsHasUserProfileDependencies(string reportName)
		{
			return ErrorStringsWrapper.Keys.GetString("rsHasUserProfileDependencies", reportName);
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000628F File Offset: 0x0000448F
		public static string rsScheduleNotFound(string name)
		{
			return ErrorStringsWrapper.Keys.GetString("rsScheduleNotFound", name);
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000629C File Offset: 0x0000449C
		public static string rsScheduleAlreadyExists(string name)
		{
			return ErrorStringsWrapper.Keys.GetString("rsScheduleAlreadyExists", name);
		}

		// Token: 0x06000386 RID: 902 RVA: 0x000062A9 File Offset: 0x000044A9
		public static string rsSharePoitScheduleAlreadyExists(string name, string path)
		{
			return ErrorStringsWrapper.Keys.GetString("rsSharePoitScheduleAlreadyExists", name, path);
		}

		// Token: 0x06000387 RID: 903 RVA: 0x000062B7 File Offset: 0x000044B7
		public static string rsInvalidSqlAgentJob(string taskName)
		{
			return ErrorStringsWrapper.Keys.GetString("rsInvalidSqlAgentJob", taskName);
		}

		// Token: 0x06000388 RID: 904 RVA: 0x000062C4 File Offset: 0x000044C4
		public static string rsSubscriptionNotFound(string name)
		{
			return ErrorStringsWrapper.Keys.GetString("rsSubscriptionNotFound", name);
		}

		// Token: 0x06000389 RID: 905 RVA: 0x000062D1 File Offset: 0x000044D1
		public static string rsCacheRefreshPlanNotFound(string name)
		{
			return ErrorStringsWrapper.Keys.GetString("rsCacheRefreshPlanNotFound", name);
		}

		// Token: 0x0600038A RID: 906 RVA: 0x000062DE File Offset: 0x000044DE
		public static string rsInvalidExtensionParameter(string reason)
		{
			return ErrorStringsWrapper.Keys.GetString("rsInvalidExtensionParameter", reason);
		}

		// Token: 0x0600038B RID: 907 RVA: 0x000062EB File Offset: 0x000044EB
		public static string rsInvalidSubscription(string ID)
		{
			return ErrorStringsWrapper.Keys.GetString("rsInvalidSubscription", ID);
		}

		// Token: 0x0600038C RID: 908 RVA: 0x000062F8 File Offset: 0x000044F8
		public static string rsPBIServiceUnavailable(string correlationId)
		{
			return ErrorStringsWrapper.Keys.GetString("rsPBIServiceUnavailable", correlationId);
		}

		// Token: 0x0600038D RID: 909 RVA: 0x00006305 File Offset: 0x00004505
		public static string rsUnknownEventType(string eventType)
		{
			return ErrorStringsWrapper.Keys.GetString("rsUnknownEventType", eventType);
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00006312 File Offset: 0x00004512
		public static string rsCannotSubscribeToEvent(string eventType)
		{
			return ErrorStringsWrapper.Keys.GetString("rsCannotSubscribeToEvent", eventType);
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000631F File Offset: 0x0000451F
		public static string rsReservedRole(string roleName)
		{
			return ErrorStringsWrapper.Keys.GetString("rsReservedRole", roleName);
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000632C File Offset: 0x0000452C
		public static string rsTaskNotFound(string taskName)
		{
			return ErrorStringsWrapper.Keys.GetString("rsTaskNotFound", taskName);
		}

		// Token: 0x06000391 RID: 913 RVA: 0x00006339 File Offset: 0x00004539
		public static string rsInheritedPolicy(string itemPath)
		{
			return ErrorStringsWrapper.Keys.GetString("rsInheritedPolicy", itemPath);
		}

		// Token: 0x06000392 RID: 914 RVA: 0x00006346 File Offset: 0x00004546
		public static string rsInheritedPolicyModelItem(string itemPath, string modelItemID)
		{
			return ErrorStringsWrapper.Keys.GetString("rsInheritedPolicyModelItem", itemPath, modelItemID);
		}

		// Token: 0x06000393 RID: 915 RVA: 0x00006354 File Offset: 0x00004554
		public static string rsInvalidPolicyDefinition(string principalName)
		{
			return ErrorStringsWrapper.Keys.GetString("rsInvalidPolicyDefinition", principalName);
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00006361 File Offset: 0x00004561
		public static string rsRoleAlreadyExists(string roleName)
		{
			return ErrorStringsWrapper.Keys.GetString("rsRoleAlreadyExists", roleName);
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000636E File Offset: 0x0000456E
		public static string rsRoleNotFound(string roleName)
		{
			return ErrorStringsWrapper.Keys.GetString("rsRoleNotFound", roleName);
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000637B File Offset: 0x0000457B
		public static string rsAccessDenied(string userName)
		{
			return ErrorStringsWrapper.Keys.GetString("rsAccessDenied", userName);
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00006388 File Offset: 0x00004588
		public static string rsAssemblyNotPermissioned(string assemblyName)
		{
			return ErrorStringsWrapper.Keys.GetString("rsAssemblyNotPermissioned", assemblyName);
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00006395 File Offset: 0x00004595
		public static string rsBatchNotFound(string batchId)
		{
			return ErrorStringsWrapper.Keys.GetString("rsBatchNotFound", batchId);
		}

		// Token: 0x06000399 RID: 921 RVA: 0x000063A2 File Offset: 0x000045A2
		public static string rsModelItemNotFound(string modelPath, string modelItemID)
		{
			return ErrorStringsWrapper.Keys.GetString("rsModelItemNotFound", modelPath, modelItemID);
		}

		// Token: 0x0600039A RID: 922 RVA: 0x000063B0 File Offset: 0x000045B0
		public static string rsInvalidReportServerDatabase(string storedVersion, string expectedVersion)
		{
			return ErrorStringsWrapper.Keys.GetString("rsInvalidReportServerDatabase", storedVersion, expectedVersion);
		}

		// Token: 0x0600039B RID: 923 RVA: 0x000063BE File Offset: 0x000045BE
		public static string rsSharePointObjectModelNotInstalled(string error)
		{
			return ErrorStringsWrapper.Keys.GetString("rsSharePointObjectModelNotInstalled", error);
		}

		// Token: 0x0600039C RID: 924 RVA: 0x000063CB File Offset: 0x000045CB
		public static string rsSemanticQueryExtensionNotFound(string extension)
		{
			return ErrorStringsWrapper.Keys.GetString("rsSemanticQueryExtensionNotFound", extension);
		}

		// Token: 0x0600039D RID: 925 RVA: 0x000063D8 File Offset: 0x000045D8
		public static string rsFailedToDecryptConfigInformation(string configElement)
		{
			return ErrorStringsWrapper.Keys.GetString("rsFailedToDecryptConfigInformation", configElement);
		}

		// Token: 0x0600039E RID: 926 RVA: 0x000063E5 File Offset: 0x000045E5
		public static string rsReportServerKeyContainerError(string accountName)
		{
			return ErrorStringsWrapper.Keys.GetString("rsReportServerKeyContainerError", accountName);
		}

		// Token: 0x0600039F RID: 927 RVA: 0x000063F2 File Offset: 0x000045F2
		public static string rsReportServerServiceUnavailable(string serviceName)
		{
			return ErrorStringsWrapper.Keys.GetString("rsReportServerServiceUnavailable", serviceName);
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x000063FF File Offset: 0x000045FF
		public static string rsInvalidModelDrillthroughReport(string reportName)
		{
			return ErrorStringsWrapper.Keys.GetString("rsInvalidModelDrillthroughReport", reportName);
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000640C File Offset: 0x0000460C
		public static string rsErrorOpeningConnection(string dataSourceName)
		{
			return ErrorStringsWrapper.Keys.GetString("rsErrorOpeningConnection", dataSourceName);
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x00006419 File Offset: 0x00004619
		public static string rsAppDomainManagerError(string appDomain)
		{
			return ErrorStringsWrapper.Keys.GetString("rsAppDomainManagerError", appDomain);
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00006426 File Offset: 0x00004626
		public static string rsHttpRuntimeError(string appDomain)
		{
			return ErrorStringsWrapper.Keys.GetString("rsHttpRuntimeError", appDomain);
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00006433 File Offset: 0x00004633
		public static string rsHttpRuntimeInternalError(string appDomain)
		{
			return ErrorStringsWrapper.Keys.GetString("rsHttpRuntimeInternalError", appDomain);
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00006440 File Offset: 0x00004640
		public static string rsHttpRuntimeClientDisconnectionError(string appdomain, string hr)
		{
			return ErrorStringsWrapper.Keys.GetString("rsHttpRuntimeClientDisconnectionError", appdomain, hr);
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000644E File Offset: 0x0000464E
		public static string rsReportBuilderFileTransmissionError(string fileName)
		{
			return ErrorStringsWrapper.Keys.GetString("rsReportBuilderFileTransmissionError", fileName);
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000645B File Offset: 0x0000465B
		public static string rsInternalResourceNotFoundError(string imageId)
		{
			return ErrorStringsWrapper.Keys.GetString("rsInternalResourceNotFoundError", imageId);
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00006468 File Offset: 0x00004668
		public static string rsRestrictedItem(string itemPath)
		{
			return ErrorStringsWrapper.Keys.GetString("rsRestrictedItem", itemPath);
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x00006475 File Offset: 0x00004675
		public static string rsStoredCredentialsOutOfSync(string path)
		{
			return ErrorStringsWrapper.Keys.GetString("rsStoredCredentialsOutOfSync", path);
		}

		// Token: 0x060003AA RID: 938 RVA: 0x00006482 File Offset: 0x00004682
		public static string rsUnsupportedParameterForMode(string mode, string parameterName)
		{
			return ErrorStringsWrapper.Keys.GetString("rsUnsupportedParameterForMode", mode, parameterName);
		}

		// Token: 0x060003AB RID: 939 RVA: 0x00006490 File Offset: 0x00004690
		public static string rsAuthenticationExtensionError(string message)
		{
			return ErrorStringsWrapper.Keys.GetString("rsAuthenticationExtensionError", message);
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000649D File Offset: 0x0000469D
		public static string rsRdceExtraElementError(string nodeName)
		{
			return ErrorStringsWrapper.Keys.GetString("rsRdceExtraElementError", nodeName);
		}

		// Token: 0x060003AD RID: 941 RVA: 0x000064AA File Offset: 0x000046AA
		public static string rsRdceMismatchError(string rdceSet, string rdceConfigured)
		{
			return ErrorStringsWrapper.Keys.GetString("rsRdceMismatchError", rdceSet, rdceConfigured);
		}

		// Token: 0x060003AE RID: 942 RVA: 0x000064B8 File Offset: 0x000046B8
		public static string rsRdceInvalidItemTypeError(string type)
		{
			return ErrorStringsWrapper.Keys.GetString("rsRdceInvalidItemTypeError", type);
		}

		// Token: 0x060003AF RID: 943 RVA: 0x000064C5 File Offset: 0x000046C5
		public static string rsSoapExtensionInvalidPreambleLengthError(string length)
		{
			return ErrorStringsWrapper.Keys.GetString("rsSoapExtensionInvalidPreambleLengthError", length);
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x000064D2 File Offset: 0x000046D2
		public static string rsUrlRemapError(string url)
		{
			return ErrorStringsWrapper.Keys.GetString("rsUrlRemapError", url);
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x000064DF File Offset: 0x000046DF
		public static string rsUnknownFeedColumnType(string column)
		{
			return ErrorStringsWrapper.Keys.GetString("rsUnknownFeedColumnType", column);
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x000064EC File Offset: 0x000046EC
		public static string rsFeedValueOutOfRange(string column)
		{
			return ErrorStringsWrapper.Keys.GetString("rsFeedValueOutOfRange", column);
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x000064F9 File Offset: 0x000046F9
		public static string rsMissingFeedColumnException(string column)
		{
			return ErrorStringsWrapper.Keys.GetString("rsMissingFeedColumnException", column);
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00006506 File Offset: 0x00004706
		public static string rFeedColumnTypeMismatchException(string column)
		{
			return ErrorStringsWrapper.Keys.GetString("rFeedColumnTypeMismatchException", column);
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00006513 File Offset: 0x00004713
		public static string rsSecureStoreCannotRetrieveCredentials(string innerMessage)
		{
			return ErrorStringsWrapper.Keys.GetString("rsSecureStoreCannotRetrieveCredentials", innerMessage);
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00006520 File Offset: 0x00004720
		public static string rsSecureStoreMissingCredentialFields(string appId)
		{
			return ErrorStringsWrapper.Keys.GetString("rsSecureStoreMissingCredentialFields", appId);
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000652D File Offset: 0x0000472D
		public static string rsSecureStoreAmbiguousCredentialFields(string appId)
		{
			return ErrorStringsWrapper.Keys.GetString("rsSecureStoreAmbiguousCredentialFields", appId);
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0000653A File Offset: 0x0000473A
		public static string rsSecureStoreUnsupportedCredentialField(string appId)
		{
			return ErrorStringsWrapper.Keys.GetString("rsSecureStoreUnsupportedCredentialField", appId);
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00006547 File Offset: 0x00004747
		public static string ProductNameSSRSAndVersion(string version)
		{
			return ErrorStringsWrapper.Keys.GetString("ProductNameSSRSAndVersion", version);
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00006554 File Offset: 0x00004754
		public static string ProductNamePBIRSAndVersion(string version)
		{
			return ErrorStringsWrapper.Keys.GetString("ProductNamePBIRSAndVersion", version);
		}

		// Token: 0x060003BB RID: 955 RVA: 0x00006561 File Offset: 0x00004761
		public static string rsMissingParameter(string parameterName)
		{
			return ErrorStringsWrapper.Keys.GetString("rsMissingParameter", parameterName);
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0000656E File Offset: 0x0000476E
		public static string rsInvalidParameter(string parameterName)
		{
			return ErrorStringsWrapper.Keys.GetString("rsInvalidParameter", parameterName);
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0000657B File Offset: 0x0000477B
		public static string rsInvalidElement(string elementName)
		{
			return ErrorStringsWrapper.Keys.GetString("rsInvalidElement", elementName);
		}

		// Token: 0x060003BE RID: 958 RVA: 0x00006588 File Offset: 0x00004788
		public static string rsUnrecognizedXmlElement(string elementName)
		{
			return ErrorStringsWrapper.Keys.GetString("rsUnrecognizedXmlElement", elementName);
		}

		// Token: 0x060003BF RID: 959 RVA: 0x00006595 File Offset: 0x00004795
		public static string rsMissingElement(string elementName)
		{
			return ErrorStringsWrapper.Keys.GetString("rsMissingElement", elementName);
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x000065A2 File Offset: 0x000047A2
		public static string rsElementTypeMismatch(string name)
		{
			return ErrorStringsWrapper.Keys.GetString("rsElementTypeMismatch", name);
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x000065AF File Offset: 0x000047AF
		public static string rsInvalidElementCombination(string firstElementName, string secondElementName)
		{
			return ErrorStringsWrapper.Keys.GetString("rsInvalidElementCombination", firstElementName, secondElementName);
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x000065BD File Offset: 0x000047BD
		public static string rsInvalidMultipleElementCombination(string firstElementName, string secondElementName, string thirdElement)
		{
			return ErrorStringsWrapper.Keys.GetString("rsInvalidMultipleElementCombination", firstElementName, secondElementName, thirdElement);
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x000065CC File Offset: 0x000047CC
		public static string rsMalformedXml(string error)
		{
			return ErrorStringsWrapper.Keys.GetString("rsMalformedXml", error);
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x000065D9 File Offset: 0x000047D9
		public static string rsInvalidItemPath(string itemPath, int maxLength)
		{
			return ErrorStringsWrapper.Keys.GetString("rsInvalidItemPath", itemPath, maxLength);
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x000065EC File Offset: 0x000047EC
		public static string rsItemPathLengthExceeded(string itemPath, int maxLength)
		{
			return ErrorStringsWrapper.Keys.GetString("rsItemPathLengthExceeded", itemPath, maxLength);
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x000065FF File Offset: 0x000047FF
		public static string rsInvalidItemName(string itemName, int maxLength)
		{
			return ErrorStringsWrapper.Keys.GetString("rsInvalidItemName", itemName, maxLength);
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x00006612 File Offset: 0x00004812
		public static string rsItemNotFound(string itemPath)
		{
			return ErrorStringsWrapper.Keys.GetString("rsItemNotFound", itemPath);
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000661F File Offset: 0x0000481F
		public static string rsItemContentInvalid(string itemPath)
		{
			return ErrorStringsWrapper.Keys.GetString("rsItemContentInvalid", itemPath);
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000662C File Offset: 0x0000482C
		public static string rsMaxCountComments()
		{
			return ErrorStringsWrapper.Keys.GetString("rsMaxCountComments");
		}

		// Token: 0x060003CA RID: 970 RVA: 0x00006638 File Offset: 0x00004838
		public static string rsWrongItemType(string itemPath)
		{
			return ErrorStringsWrapper.Keys.GetString("rsWrongItemType", itemPath);
		}

		// Token: 0x060003CB RID: 971 RVA: 0x00006645 File Offset: 0x00004845
		public static string rsReadOnlyReportParameter(string parameterName)
		{
			return ErrorStringsWrapper.Keys.GetString("rsReadOnlyReportParameter", parameterName);
		}

		// Token: 0x060003CC RID: 972 RVA: 0x00006652 File Offset: 0x00004852
		public static string rsReadOnlyDataSetParameter(string parameterName)
		{
			return ErrorStringsWrapper.Keys.GetString("rsReadOnlyDataSetParameter", parameterName);
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000665F File Offset: 0x0000485F
		public static string rsUnknownReportParameter(string parameterName)
		{
			return ErrorStringsWrapper.Keys.GetString("rsUnknownReportParameter", parameterName);
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0000666C File Offset: 0x0000486C
		public static string rsUnknownDataSetParameter(string parameterName)
		{
			return ErrorStringsWrapper.Keys.GetString("rsUnknownDataSetParameter", parameterName);
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00006679 File Offset: 0x00004879
		public static string rsReportParameterValueNotSet(string parameterName)
		{
			return ErrorStringsWrapper.Keys.GetString("rsReportParameterValueNotSet", parameterName);
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x00006686 File Offset: 0x00004886
		public static string rsDataSetParameterValueNotSet(string parameterName)
		{
			return ErrorStringsWrapper.Keys.GetString("rsDataSetParameterValueNotSet", parameterName);
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00006693 File Offset: 0x00004893
		public static string rsReportParameterTypeMismatch(string parameterName)
		{
			return ErrorStringsWrapper.Keys.GetString("rsReportParameterTypeMismatch", parameterName);
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x000066A0 File Offset: 0x000048A0
		public static string rsInvalidReportParameter(string parameterName)
		{
			return ErrorStringsWrapper.Keys.GetString("rsInvalidReportParameter", parameterName);
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x000066AD File Offset: 0x000048AD
		public static string rsDataSourceNotFound(string dataSource)
		{
			return ErrorStringsWrapper.Keys.GetString("rsDataSourceNotFound", dataSource);
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x000066BA File Offset: 0x000048BA
		public static string rsDataSourceNoPromptException(string dataSource)
		{
			return ErrorStringsWrapper.Keys.GetString("rsDataSourceNoPromptException", dataSource);
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x000066C7 File Offset: 0x000048C7
		public static string cannotBuildExternalConnectionString(string dataSource)
		{
			return ErrorStringsWrapper.Keys.GetString("cannotBuildExternalConnectionString", dataSource);
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x000066D4 File Offset: 0x000048D4
		public static string rsInvalidDataSourceReference(string dataSourceName)
		{
			return ErrorStringsWrapper.Keys.GetString("rsInvalidDataSourceReference", dataSourceName);
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x000066E1 File Offset: 0x000048E1
		public static string rsInvalidDataSetReference(string dataSetName)
		{
			return ErrorStringsWrapper.Keys.GetString("rsInvalidDataSetReference", dataSetName);
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x000066EE File Offset: 0x000048EE
		public static string rsInvalidDataSourceType(string dataSourcePath)
		{
			return ErrorStringsWrapper.Keys.GetString("rsInvalidDataSourceType", dataSourcePath);
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x000066FB File Offset: 0x000048FB
		public static string rsInvalidDataSourceCount(string reportPath)
		{
			return ErrorStringsWrapper.Keys.GetString("rsInvalidDataSourceCount", reportPath);
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00006708 File Offset: 0x00004908
		public static string rsCannotRetrieveModel(string itemPath)
		{
			return ErrorStringsWrapper.Keys.GetString("rsCannotRetrieveModel", itemPath);
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00006715 File Offset: 0x00004915
		public static string rsExecuteQueriesFailure(string dataSourcePath)
		{
			return ErrorStringsWrapper.Keys.GetString("rsExecuteQueriesFailure", dataSourcePath);
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00006722 File Offset: 0x00004922
		public static string rsDataSetExecutionError(string dataSetPath)
		{
			return ErrorStringsWrapper.Keys.GetString("rsDataSetExecutionError", dataSetPath);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0000672F File Offset: 0x0000492F
		public static string rsUnknownUserName(string userName)
		{
			return ErrorStringsWrapper.Keys.GetString("rsUnknownUserName", userName);
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000673C File Offset: 0x0000493C
		public static string rsDataExtensionNotFound(string extension)
		{
			return ErrorStringsWrapper.Keys.GetString("rsDataExtensionNotFound", extension);
		}

		// Token: 0x060003DF RID: 991 RVA: 0x00006749 File Offset: 0x00004949
		public static string rsOperationNotSupported(string operation)
		{
			return ErrorStringsWrapper.Keys.GetString("rsOperationNotSupported", operation);
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00006756 File Offset: 0x00004956
		public static string rsServerConfigurationError(string additionalMsg)
		{
			return ErrorStringsWrapper.Keys.GetString("rsServerConfigurationError", additionalMsg);
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00006763 File Offset: 0x00004963
		public static string rsWinAuthz(string methodName, string errorCode, string userName)
		{
			return ErrorStringsWrapper.Keys.GetString("rsWinAuthz", methodName, errorCode, userName);
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00006772 File Offset: 0x00004972
		public static string rsWinAuthz5(string methodName, string userName)
		{
			return ErrorStringsWrapper.Keys.GetString("rsWinAuthz5", methodName, userName);
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00006780 File Offset: 0x00004980
		public static string rsWinAuthz1355(string methodName, string userName)
		{
			return ErrorStringsWrapper.Keys.GetString("rsWinAuthz1355", methodName, userName);
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0000678E File Offset: 0x0000498E
		public static string rsEventLogSourceNotFound(string source)
		{
			return ErrorStringsWrapper.Keys.GetString("rsEventLogSourceNotFound", source);
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0000679B File Offset: 0x0000499B
		public static string rsFileExtensionViolation(string target, string source)
		{
			return ErrorStringsWrapper.Keys.GetString("rsFileExtensionViolation", target, source);
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x000067A9 File Offset: 0x000049A9
		public static string rsDataSetNotFound(string dataSet)
		{
			return ErrorStringsWrapper.Keys.GetString("rsDataSetNotFound", dataSet);
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x000067B6 File Offset: 0x000049B6
		public static string rsInvalidProgressiveFormatError(string command)
		{
			return ErrorStringsWrapper.Keys.GetString("rsInvalidProgressiveFormatError", command);
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x000067C3 File Offset: 0x000049C3
		public static string rsProgressiveFormatElementMissingError(string key)
		{
			return ErrorStringsWrapper.Keys.GetString("rsProgressiveFormatElementMissingError", key);
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x000067D0 File Offset: 0x000049D0
		public static string rsProgressiveMessageWriteError(string command)
		{
			return ErrorStringsWrapper.Keys.GetString("rsProgressiveMessageWriteError", command);
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x000067DD File Offset: 0x000049DD
		public static string rsProgressiveMessageWriteElementError(string key, string command)
		{
			return ErrorStringsWrapper.Keys.GetString("rsProgressiveMessageWriteElementError", key, command);
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x000067EB File Offset: 0x000049EB
		public static string rsInvalidSessionId(string sessionId)
		{
			return ErrorStringsWrapper.Keys.GetString("rsInvalidSessionId", sessionId);
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x000067F8 File Offset: 0x000049F8
		public static string rsInvalidConcurrentRenderEditSessionRequest(string sessionId)
		{
			return ErrorStringsWrapper.Keys.GetString("rsInvalidConcurrentRenderEditSessionRequest", sessionId);
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00006805 File Offset: 0x00004A05
		public static string rsSessionNotFound(string sessionId, string userName)
		{
			return ErrorStringsWrapper.Keys.GetString("rsSessionNotFound", sessionId, userName);
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00006813 File Offset: 0x00004A13
		public static string rsInvalidSessionCatalogItems(string details)
		{
			return ErrorStringsWrapper.Keys.GetString("rsInvalidSessionCatalogItems", details);
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00006820 File Offset: 0x00004A20
		public static string rsApiVersionDiscontinued(string serverVersion, string clientVersion)
		{
			return ErrorStringsWrapper.Keys.GetString("rsApiVersionDiscontinued", serverVersion, clientVersion);
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0000682E File Offset: 0x00004A2E
		public static string rsApiVersionNotRecognized(string serverVersion, string clientVersion)
		{
			return ErrorStringsWrapper.Keys.GetString("rsApiVersionNotRecognized", serverVersion, clientVersion);
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0000683C File Offset: 0x00004A3C
		public static string rsCertificateMissingOrInvalid(string certificateId)
		{
			return ErrorStringsWrapper.Keys.GetString("rsCertificateMissingOrInvalid", certificateId);
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00006849 File Offset: 0x00004A49
		public static string rsResolutionFailureException(string databaseFullName)
		{
			return ErrorStringsWrapper.Keys.GetString("rsResolutionFailureException", databaseFullName);
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00006856 File Offset: 0x00004A56
		public static string rsReportServerStorageSingleRefreshConnectionExpected(string modelId, string actualCount)
		{
			return ErrorStringsWrapper.Keys.GetString("rsReportServerStorageSingleRefreshConnectionExpected", modelId, actualCount);
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00006864 File Offset: 0x00004A64
		public static string rsReportServerStorageRefreshConnectionNotValidated(string modelId, string refreshConnectionId)
		{
			return ErrorStringsWrapper.Keys.GetString("rsReportServerStorageRefreshConnectionNotValidated", modelId, refreshConnectionId);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00006872 File Offset: 0x00004A72
		public static string rsIdentityClaimsMissingOrInvalid(string identityClaims)
		{
			return ErrorStringsWrapper.Keys.GetString("rsIdentityClaimsMissingOrInvalid", identityClaims);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0000687F File Offset: 0x00004A7F
		public static string rsSystemResourcePackageReferencedItemMissing(string itemName)
		{
			return ErrorStringsWrapper.Keys.GetString("rsSystemResourcePackageReferencedItemMissing", itemName);
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0000688C File Offset: 0x00004A8C
		public static string rsSystemResourcePackageRequiredItemMissing(string itemName)
		{
			return ErrorStringsWrapper.Keys.GetString("rsSystemResourcePackageRequiredItemMissing", itemName);
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00006899 File Offset: 0x00004A99
		public static string rsSystemResourcePackageItemContentTypeMismatch(string itemName, string contentType)
		{
			return ErrorStringsWrapper.Keys.GetString("rsSystemResourcePackageItemContentTypeMismatch", itemName, contentType);
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x000068A7 File Offset: 0x00004AA7
		public static string rsSystemResourcePackageItemExtensionMismatch(string itemName, string extension)
		{
			return ErrorStringsWrapper.Keys.GetString("rsSystemResourcePackageItemExtensionMismatch", itemName, extension);
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x000068B5 File Offset: 0x00004AB5
		public static string rsSystemResourcePackageWrongType(string typeName)
		{
			return ErrorStringsWrapper.Keys.GetString("rsSystemResourcePackageWrongType", typeName);
		}

		// Token: 0x020000D7 RID: 215
		[CompilerGenerated]
		public class Keys
		{
			// Token: 0x060004AD RID: 1197 RVA: 0x0000861B File Offset: 0x0000681B
			private Keys()
			{
			}

			// Token: 0x17000209 RID: 521
			// (get) Token: 0x060004AE RID: 1198 RVA: 0x00008623 File Offset: 0x00006823
			// (set) Token: 0x060004AF RID: 1199 RVA: 0x0000862A File Offset: 0x0000682A
			public static CultureInfo Culture
			{
				get
				{
					return ErrorStringsWrapper.Keys._culture;
				}
				set
				{
					ErrorStringsWrapper.Keys._culture = value;
				}
			}

			// Token: 0x060004B0 RID: 1200 RVA: 0x00008632 File Offset: 0x00006832
			public static string GetString(string key)
			{
				return ErrorStringsWrapper.Keys.resourceManager.GetString(key, ErrorStringsWrapper.Keys._culture);
			}

			// Token: 0x060004B1 RID: 1201 RVA: 0x00008644 File Offset: 0x00006844
			public static string GetString(string key, object arg0)
			{
				return string.Format(CultureInfo.CurrentCulture, ErrorStringsWrapper.Keys.resourceManager.GetString(key, ErrorStringsWrapper.Keys._culture), arg0);
			}

			// Token: 0x060004B2 RID: 1202 RVA: 0x00008661 File Offset: 0x00006861
			public static string GetString(string key, object arg0, object arg1)
			{
				return string.Format(CultureInfo.CurrentCulture, ErrorStringsWrapper.Keys.resourceManager.GetString(key, ErrorStringsWrapper.Keys._culture), arg0, arg1);
			}

			// Token: 0x060004B3 RID: 1203 RVA: 0x0000867F File Offset: 0x0000687F
			public static string GetString(string key, object arg0, object arg1, object arg2)
			{
				return string.Format(CultureInfo.CurrentCulture, ErrorStringsWrapper.Keys.resourceManager.GetString(key, ErrorStringsWrapper.Keys._culture), arg0, arg1, arg2);
			}

			// Token: 0x04000087 RID: 135
			private static ResourceManager resourceManager = ErrorStrings.ResourceManager;

			// Token: 0x04000088 RID: 136
			private static CultureInfo _culture = null;

			// Token: 0x04000089 RID: 137
			public const string InvalidKeyValue = "InvalidKeyValue";

			// Token: 0x0400008A RID: 138
			public const string InvalidConfigElement = "InvalidConfigElement";

			// Token: 0x0400008B RID: 139
			public const string CouldNotFindElement = "CouldNotFindElement";

			// Token: 0x0400008C RID: 140
			public const string DuplicateConfigElement = "DuplicateConfigElement";

			// Token: 0x0400008D RID: 141
			public const string EmptyExtensionName = "EmptyExtensionName";

			// Token: 0x0400008E RID: 142
			public const string SameExtensionName = "SameExtensionName";

			// Token: 0x0400008F RID: 143
			public const string SameEventType = "SameEventType";

			// Token: 0x04000090 RID: 144
			public const string NoEventForEventProcessor = "NoEventForEventProcessor";

			// Token: 0x04000091 RID: 145
			public const string rsEventExtensionNotFoundException = "rsEventExtensionNotFoundException";

			// Token: 0x04000092 RID: 146
			public const string rsEventMaxRetryExceededException = "rsEventMaxRetryExceededException";

			// Token: 0x04000093 RID: 147
			public const string UIServerLoopback = "UIServerLoopback";

			// Token: 0x04000094 RID: 148
			public const string DataSourceConnectionErrorNotVisible = "DataSourceConnectionErrorNotVisible";

			// Token: 0x04000095 RID: 149
			public const string UserNameUnknown = "UserNameUnknown";

			// Token: 0x04000096 RID: 150
			public const string rsMissingRequiredPropertyForItemType = "rsMissingRequiredPropertyForItemType";

			// Token: 0x04000097 RID: 151
			public const string rsParameterTypeMismatch = "rsParameterTypeMismatch";

			// Token: 0x04000098 RID: 152
			public const string rsInvalidParameterCombination = "rsInvalidParameterCombination";

			// Token: 0x04000099 RID: 153
			public const string rsStoredParameterNotFound = "rsStoredParameterNotFound";

			// Token: 0x0400009A RID: 154
			public const string rsItemAlreadyExists = "rsItemAlreadyExists";

			// Token: 0x0400009B RID: 155
			public const string rsInvalidMove = "rsInvalidMove";

			// Token: 0x0400009C RID: 156
			public const string rsInvalidDestination = "rsInvalidDestination";

			// Token: 0x0400009D RID: 157
			public const string rsReservedItem = "rsReservedItem";

			// Token: 0x0400009E RID: 158
			public const string rsProcessingError = "rsProcessingError";

			// Token: 0x0400009F RID: 159
			public const string rsReadOnlyProperty = "rsReadOnlyProperty";

			// Token: 0x040000A0 RID: 160
			public const string rsStreamNotFound = "rsStreamNotFound";

			// Token: 0x040000A1 RID: 161
			public const string rsMissingSessionId = "rsMissingSessionId";

			// Token: 0x040000A2 RID: 162
			public const string rsExecutionNotFound = "rsExecutionNotFound";

			// Token: 0x040000A3 RID: 163
			public const string rsQueryExecutionNotAllowed = "rsQueryExecutionNotAllowed";

			// Token: 0x040000A4 RID: 164
			public const string rsReportNotReady = "rsReportNotReady";

			// Token: 0x040000A5 RID: 165
			public const string rsReportSnapshotEnabled = "rsReportSnapshotEnabled";

			// Token: 0x040000A6 RID: 166
			public const string rsReportSnapshotNotEnabled = "rsReportSnapshotNotEnabled";

			// Token: 0x040000A7 RID: 167
			public const string rsOperationPreventsUnattendedExecution = "rsOperationPreventsUnattendedExecution";

			// Token: 0x040000A8 RID: 168
			public const string rsInvalidReportLink = "rsInvalidReportLink";

			// Token: 0x040000A9 RID: 169
			public const string rsSubreportFromSnapshot = "rsSubreportFromSnapshot";

			// Token: 0x040000AA RID: 170
			public const string rsSPSiteNotFound = "rsSPSiteNotFound";

			// Token: 0x040000AB RID: 171
			public const string rsCachingNotEnabled = "rsCachingNotEnabled";

			// Token: 0x040000AC RID: 172
			public const string rsInvalidSearchOperator = "rsInvalidSearchOperator";

			// Token: 0x040000AD RID: 173
			public const string rsQueryTimeout = "rsQueryTimeout";

			// Token: 0x040000AE RID: 174
			public const string rsReportHistoryNotFound = "rsReportHistoryNotFound";

			// Token: 0x040000AF RID: 175
			public const string rsSchedulerNotResponding = "rsSchedulerNotResponding";

			// Token: 0x040000B0 RID: 176
			public const string rsSchedulerNotRespondingPreventsPinning = "rsSchedulerNotRespondingPreventsPinning";

			// Token: 0x040000B1 RID: 177
			public const string rsHasUserProfileDependencies = "rsHasUserProfileDependencies";

			// Token: 0x040000B2 RID: 178
			public const string rsScheduleNotFound = "rsScheduleNotFound";

			// Token: 0x040000B3 RID: 179
			public const string rsScheduleAlreadyExists = "rsScheduleAlreadyExists";

			// Token: 0x040000B4 RID: 180
			public const string rsSharePoitScheduleAlreadyExists = "rsSharePoitScheduleAlreadyExists";

			// Token: 0x040000B5 RID: 181
			public const string rsScheduleDateTimeRangeException = "rsScheduleDateTimeRangeException";

			// Token: 0x040000B6 RID: 182
			public const string rsInvalidSqlAgentJob = "rsInvalidSqlAgentJob";

			// Token: 0x040000B7 RID: 183
			public const string rsUserCannotOwnSubscription = "rsUserCannotOwnSubscription";

			// Token: 0x040000B8 RID: 184
			public const string rsCannotActivateSubscription = "rsCannotActivateSubscription";

			// Token: 0x040000B9 RID: 185
			public const string rsSubscriptionNotFound = "rsSubscriptionNotFound";

			// Token: 0x040000BA RID: 186
			public const string rsCacheRefreshPlanNotFound = "rsCacheRefreshPlanNotFound";

			// Token: 0x040000BB RID: 187
			public const string rsDeliveryExtensionNotFound = "rsDeliveryExtensionNotFound";

			// Token: 0x040000BC RID: 188
			public const string rsDeliverError = "rsDeliverError";

			// Token: 0x040000BD RID: 189
			public const string rsCannotPrepareQuery = "rsCannotPrepareQuery";

			// Token: 0x040000BE RID: 190
			public const string rsInvalidExtensionParameter = "rsInvalidExtensionParameter";

			// Token: 0x040000BF RID: 191
			public const string rsInvalidSubscription = "rsInvalidSubscription";

			// Token: 0x040000C0 RID: 192
			public const string rsPBIServiceUnavailable = "rsPBIServiceUnavailable";

			// Token: 0x040000C1 RID: 193
			public const string rsUnknownEventType = "rsUnknownEventType";

			// Token: 0x040000C2 RID: 194
			public const string rsCannotSubscribeToEvent = "rsCannotSubscribeToEvent";

			// Token: 0x040000C3 RID: 195
			public const string rsReservedRole = "rsReservedRole";

			// Token: 0x040000C4 RID: 196
			public const string rsTaskNotFound = "rsTaskNotFound";

			// Token: 0x040000C5 RID: 197
			public const string rsMixedTasks = "rsMixedTasks";

			// Token: 0x040000C6 RID: 198
			public const string rsEmptyRole = "rsEmptyRole";

			// Token: 0x040000C7 RID: 199
			public const string rsInheritedPolicy = "rsInheritedPolicy";

			// Token: 0x040000C8 RID: 200
			public const string rsInheritedPolicyModelItem = "rsInheritedPolicyModelItem";

			// Token: 0x040000C9 RID: 201
			public const string rsInvalidPolicyDefinition = "rsInvalidPolicyDefinition";

			// Token: 0x040000CA RID: 202
			public const string rsRoleAlreadyExists = "rsRoleAlreadyExists";

			// Token: 0x040000CB RID: 203
			public const string rsRoleNotFound = "rsRoleNotFound";

			// Token: 0x040000CC RID: 204
			public const string rsCannotDeleteRootPolicy = "rsCannotDeleteRootPolicy";

			// Token: 0x040000CD RID: 205
			public const string rsAccessDenied = "rsAccessDenied";

			// Token: 0x040000CE RID: 206
			public const string rsSecureConnectionRequired = "rsSecureConnectionRequired";

			// Token: 0x040000CF RID: 207
			public const string rsAssemblyNotPermissioned = "rsAssemblyNotPermissioned";

			// Token: 0x040000D0 RID: 208
			public const string rsBatchNotFound = "rsBatchNotFound";

			// Token: 0x040000D1 RID: 209
			public const string rsModelItemNotFound = "rsModelItemNotFound";

			// Token: 0x040000D2 RID: 210
			public const string rsModelRootPolicyRequired = "rsModelRootPolicyRequired";

			// Token: 0x040000D3 RID: 211
			public const string rsModelIDMismatch = "rsModelIDMismatch";

			// Token: 0x040000D4 RID: 212
			public const string rsModelNotGenerated = "rsModelNotGenerated";

			// Token: 0x040000D5 RID: 213
			public const string rsModelGenerationNotSupported = "rsModelGenerationNotSupported";

			// Token: 0x040000D6 RID: 214
			public const string rsModelGenerationError = "rsModelGenerationError";

			// Token: 0x040000D7 RID: 215
			public const string rsInvalidReportServerDatabase = "rsInvalidReportServerDatabase";

			// Token: 0x040000D8 RID: 216
			public const string rsSharePointObjectModelNotInstalled = "rsSharePointObjectModelNotInstalled";

			// Token: 0x040000D9 RID: 217
			public const string rsReportServerDatabaseUnavailable = "rsReportServerDatabaseUnavailable";

			// Token: 0x040000DA RID: 218
			public const string rsReportServerDatabaseError = "rsReportServerDatabaseError";

			// Token: 0x040000DB RID: 219
			public const string rsSemanticQueryExtensionNotFound = "rsSemanticQueryExtensionNotFound";

			// Token: 0x040000DC RID: 220
			public const string rsEvaluationCopyExpired = "rsEvaluationCopyExpired";

			// Token: 0x040000DD RID: 221
			public const string rsServerBusy = "rsServerBusy";

			// Token: 0x040000DE RID: 222
			public const string rsFailedToDecryptConfigInformation = "rsFailedToDecryptConfigInformation";

			// Token: 0x040000DF RID: 223
			public const string rsReportServerDisabled = "rsReportServerDisabled";

			// Token: 0x040000E0 RID: 224
			public const string rsReportServerKeyContainerError = "rsReportServerKeyContainerError";

			// Token: 0x040000E1 RID: 225
			public const string rsKeyStateNotValid = "rsKeyStateNotValid";

			// Token: 0x040000E2 RID: 226
			public const string rsReportServerNotActivated = "rsReportServerNotActivated";

			// Token: 0x040000E3 RID: 227
			public const string rsReportServerServiceUnavailable = "rsReportServerServiceUnavailable";

			// Token: 0x040000E4 RID: 228
			public const string rsAccessDeniedToSecureData = "rsAccessDeniedToSecureData";

			// Token: 0x040000E5 RID: 229
			public const string rsInvalidModelDrillthroughReport = "rsInvalidModelDrillthroughReport";

			// Token: 0x040000E6 RID: 230
			public const string rsCannotValidateEncryptedData = "rsCannotValidateEncryptedData";

			// Token: 0x040000E7 RID: 231
			public const string rsRemotePublicKeyUnavailable = "rsRemotePublicKeyUnavailable";

			// Token: 0x040000E8 RID: 232
			public const string rsFailedToExportSymmetricKey = "rsFailedToExportSymmetricKey";

			// Token: 0x040000E9 RID: 233
			public const string rsBackupKeyPasswordInvalid = "rsBackupKeyPasswordInvalid";

			// Token: 0x040000EA RID: 234
			public const string rsErrorOpeningConnection = "rsErrorOpeningConnection";

			// Token: 0x040000EB RID: 235
			public const string rsAppDomainManagerError = "rsAppDomainManagerError";

			// Token: 0x040000EC RID: 236
			public const string rsHttpRuntimeError = "rsHttpRuntimeError";

			// Token: 0x040000ED RID: 237
			public const string rsHttpRuntimeInternalError = "rsHttpRuntimeInternalError";

			// Token: 0x040000EE RID: 238
			public const string rsHttpRuntimeClientDisconnectionError = "rsHttpRuntimeClientDisconnectionError";

			// Token: 0x040000EF RID: 239
			public const string rsReportBuilderFileTransmissionError = "rsReportBuilderFileTransmissionError";

			// Token: 0x040000F0 RID: 240
			public const string rsInternalResourceNotSpecifiedError = "rsInternalResourceNotSpecifiedError";

			// Token: 0x040000F1 RID: 241
			public const string rsInternalResourceNotFoundError = "rsInternalResourceNotFoundError";

			// Token: 0x040000F2 RID: 242
			public const string SkuNonSqlDataSources = "SkuNonSqlDataSources";

			// Token: 0x040000F3 RID: 243
			public const string SkuOtherSkuDatasources = "SkuOtherSkuDatasources";

			// Token: 0x040000F4 RID: 244
			public const string SkuRemoteDataSources = "SkuRemoteDataSources";

			// Token: 0x040000F5 RID: 245
			public const string SkuCaching = "SkuCaching";

			// Token: 0x040000F6 RID: 246
			public const string SkuExecutionSnapshots = "SkuExecutionSnapshots";

			// Token: 0x040000F7 RID: 247
			public const string SkuHistory = "SkuHistory";

			// Token: 0x040000F8 RID: 248
			public const string SkuDelivery = "SkuDelivery";

			// Token: 0x040000F9 RID: 249
			public const string SkuScheduling = "SkuScheduling";

			// Token: 0x040000FA RID: 250
			public const string SkuExtensibility = "SkuExtensibility";

			// Token: 0x040000FB RID: 251
			public const string SkuCustomAuth = "SkuCustomAuth";

			// Token: 0x040000FC RID: 252
			public const string SkuSharepoint = "SkuSharepoint";

			// Token: 0x040000FD RID: 253
			public const string SkuScaleOut = "SkuScaleOut";

			// Token: 0x040000FE RID: 254
			public const string SkuSubscriptions = "SkuSubscriptions";

			// Token: 0x040000FF RID: 255
			public const string SkuDataDrivenSubscriptions = "SkuDataDrivenSubscriptions";

			// Token: 0x04000100 RID: 256
			public const string SkuCustomRolesSecurity = "SkuCustomRolesSecurity";

			// Token: 0x04000101 RID: 257
			public const string SkuReportBuilder = "SkuReportBuilder";

			// Token: 0x04000102 RID: 258
			public const string SkuModelItemSecurity = "SkuModelItemSecurity";

			// Token: 0x04000103 RID: 259
			public const string SkuDynamicDrillthrough = "SkuDynamicDrillthrough";

			// Token: 0x04000104 RID: 260
			public const string SkuNoCpuThrottling = "SkuNoCpuThrottling";

			// Token: 0x04000105 RID: 261
			public const string SkuNoMemoryThrottling = "SkuNoMemoryThrottling";

			// Token: 0x04000106 RID: 262
			public const string SkuEventGeneration = "SkuEventGeneration";

			// Token: 0x04000107 RID: 263
			public const string SkuComponentLibrary = "SkuComponentLibrary";

			// Token: 0x04000108 RID: 264
			public const string SkuSharedDataset = "SkuSharedDataset";

			// Token: 0x04000109 RID: 265
			public const string SkuDataAlerting = "SkuDataAlerting";

			// Token: 0x0400010A RID: 266
			public const string SkuCrescent = "SkuCrescent";

			// Token: 0x0400010B RID: 267
			public const string SkuKpiItems = "SkuKpiItems";

			// Token: 0x0400010C RID: 268
			public const string SkuCommentAlerting = "SkuCommentAlerting";

			// Token: 0x0400010D RID: 269
			public const string rsRestrictedItem = "rsRestrictedItem";

			// Token: 0x0400010E RID: 270
			public const string rsSharePointError = "rsSharePointError";

			// Token: 0x0400010F RID: 271
			public const string rsSharePointContentDBAccessError = "rsSharePointContentDBAccessError";

			// Token: 0x04000110 RID: 272
			public const string rsStoredCredentialsOutOfSync = "rsStoredCredentialsOutOfSync";

			// Token: 0x04000111 RID: 273
			public const string rsODCVersionNotSupported = "rsODCVersionNotSupported";

			// Token: 0x04000112 RID: 274
			public const string rsOperationNotSupportedSharePointMode = "rsOperationNotSupportedSharePointMode";

			// Token: 0x04000113 RID: 275
			public const string rsOperationNotSupportedNativeMode = "rsOperationNotSupportedNativeMode";

			// Token: 0x04000114 RID: 276
			public const string rsUnsupportedParameterForMode = "rsUnsupportedParameterForMode";

			// Token: 0x04000115 RID: 277
			public const string rsContainerNotSupported = "rsContainerNotSupported";

			// Token: 0x04000116 RID: 278
			public const string rsPropertyDisabled = "rsPropertyDisabled";

			// Token: 0x04000117 RID: 279
			public const string rsPropertyDisabledNativeMode = "rsPropertyDisabledNativeMode";

			// Token: 0x04000118 RID: 280
			public const string rsInvalidRSDSSchema = "rsInvalidRSDSSchema";

			// Token: 0x04000119 RID: 281
			public const string rsSecurityZoneNotSupported = "rsSecurityZoneNotSupported";

			// Token: 0x0400011A RID: 282
			public const string rsAuthenticationExtensionError = "rsAuthenticationExtensionError";

			// Token: 0x0400011B RID: 283
			public const string rsFileSize = "rsFileSize";

			// Token: 0x0400011C RID: 284
			public const string rsFileSizeNotSupported = "rsFileSizeNotSupported";

			// Token: 0x0400011D RID: 285
			public const string rsRdceExtraElementError = "rsRdceExtraElementError";

			// Token: 0x0400011E RID: 286
			public const string rsRdceMismatchError = "rsRdceMismatchError";

			// Token: 0x0400011F RID: 287
			public const string rsRdceInvalidRdlError = "rsRdceInvalidRdlError";

			// Token: 0x04000120 RID: 288
			public const string rsRdceInvalidConfigurationError = "rsRdceInvalidConfigurationError";

			// Token: 0x04000121 RID: 289
			public const string rsRdceInvalidItemTypeError = "rsRdceInvalidItemTypeError";

			// Token: 0x04000122 RID: 290
			public const string rsRdceInvalidExecutionOptionError = "rsRdceInvalidExecutionOptionError";

			// Token: 0x04000123 RID: 291
			public const string rsRdceInvalidCacheOptionError = "rsRdceInvalidCacheOptionError";

			// Token: 0x04000124 RID: 292
			public const string rsRdceWrappedException = "rsRdceWrappedException";

			// Token: 0x04000125 RID: 293
			public const string rsRdceMismatchRdlVersion = "rsRdceMismatchRdlVersion";

			// Token: 0x04000126 RID: 294
			public const string rsInvalidOperation = "rsInvalidOperation";

			// Token: 0x04000127 RID: 295
			public const string rsAuthorizationExtensionError = "rsAuthorizationExtensionError";

			// Token: 0x04000128 RID: 296
			public const string rsDataCacheMismatch = "rsDataCacheMismatch";

			// Token: 0x04000129 RID: 297
			public const string rsSoapExtensionInvalidPreambleLengthError = "rsSoapExtensionInvalidPreambleLengthError";

			// Token: 0x0400012A RID: 298
			public const string rsSoapExtensionInvalidPreambleError = "rsSoapExtensionInvalidPreambleError";

			// Token: 0x0400012B RID: 299
			public const string rsUrlRemapError = "rsUrlRemapError";

			// Token: 0x0400012C RID: 300
			public const string rsRequestThroughHttpRedirectorNotSupportedError = "rsRequestThroughHttpRedirectorNotSupportedError";

			// Token: 0x0400012D RID: 301
			public const string rsUnhandledHttpApplicationError = "rsUnhandledHttpApplicationError";

			// Token: 0x0400012E RID: 302
			public const string rsInvalidCatalogRecord = "rsInvalidCatalogRecord";

			// Token: 0x0400012F RID: 303
			public const string rsUnknownFeedColumnType = "rsUnknownFeedColumnType";

			// Token: 0x04000130 RID: 304
			public const string rsFeedValueOutOfRange = "rsFeedValueOutOfRange";

			// Token: 0x04000131 RID: 305
			public const string rsMissingFeedColumnException = "rsMissingFeedColumnException";

			// Token: 0x04000132 RID: 306
			public const string rFeedColumnTypeMismatchException = "rFeedColumnTypeMismatchException";

			// Token: 0x04000133 RID: 307
			public const string rsClaimsToWindowsTokenError = "rsClaimsToWindowsTokenError";

			// Token: 0x04000134 RID: 308
			public const string rsClaimsToWindowsTokenLoginTypeError = "rsClaimsToWindowsTokenLoginTypeError";

			// Token: 0x04000135 RID: 309
			public const string GetExternalImagesInvalidNamespace = "GetExternalImagesInvalidNamespace";

			// Token: 0x04000136 RID: 310
			public const string GetExternalImagesInvalidSyntax = "GetExternalImagesInvalidSyntax";

			// Token: 0x04000137 RID: 311
			public const string rsSecureStoreContextUrlNotSpecified = "rsSecureStoreContextUrlNotSpecified";

			// Token: 0x04000138 RID: 312
			public const string rsSecureStoreInvalidLookupContext = "rsSecureStoreInvalidLookupContext";

			// Token: 0x04000139 RID: 313
			public const string rsSecureStoreCannotRetrieveCredentials = "rsSecureStoreCannotRetrieveCredentials";

			// Token: 0x0400013A RID: 314
			public const string rsSecureStoreMissingCredentialFields = "rsSecureStoreMissingCredentialFields";

			// Token: 0x0400013B RID: 315
			public const string rsSecureStoreAmbiguousCredentialFields = "rsSecureStoreAmbiguousCredentialFields";

			// Token: 0x0400013C RID: 316
			public const string rsSecureStoreUnsupportedCredentialField = "rsSecureStoreUnsupportedCredentialField";

			// Token: 0x0400013D RID: 317
			public const string rsSecureStoreUnsupportedSharePointVersion = "rsSecureStoreUnsupportedSharePointVersion";

			// Token: 0x0400013E RID: 318
			public const string ProductNameSSRS = "ProductNameSSRS";

			// Token: 0x0400013F RID: 319
			public const string ProductNamePBIRS = "ProductNamePBIRS";

			// Token: 0x04000140 RID: 320
			public const string ProductNameSSRSAndVersion = "ProductNameSSRSAndVersion";

			// Token: 0x04000141 RID: 321
			public const string ProductNamePBIRSAndVersion = "ProductNamePBIRSAndVersion";

			// Token: 0x04000142 RID: 322
			public const string rsMissingParameter = "rsMissingParameter";

			// Token: 0x04000143 RID: 323
			public const string rsInvalidParameter = "rsInvalidParameter";

			// Token: 0x04000144 RID: 324
			public const string rsInvalidElement = "rsInvalidElement";

			// Token: 0x04000145 RID: 325
			public const string rsInvalidXml = "rsInvalidXml";

			// Token: 0x04000146 RID: 326
			public const string rsUnrecognizedXmlElement = "rsUnrecognizedXmlElement";

			// Token: 0x04000147 RID: 327
			public const string rsMissingElement = "rsMissingElement";

			// Token: 0x04000148 RID: 328
			public const string rsElementTypeMismatch = "rsElementTypeMismatch";

			// Token: 0x04000149 RID: 329
			public const string rsInvalidElementCombination = "rsInvalidElementCombination";

			// Token: 0x0400014A RID: 330
			public const string rsInvalidMultipleElementCombination = "rsInvalidMultipleElementCombination";

			// Token: 0x0400014B RID: 331
			public const string rsMalformedXml = "rsMalformedXml";

			// Token: 0x0400014C RID: 332
			public const string rsInvalidItemPath = "rsInvalidItemPath";

			// Token: 0x0400014D RID: 333
			public const string rsItemPathLengthExceeded = "rsItemPathLengthExceeded";

			// Token: 0x0400014E RID: 334
			public const string rsInvalidItemName = "rsInvalidItemName";

			// Token: 0x0400014F RID: 335
			public const string rsItemNotFound = "rsItemNotFound";

			// Token: 0x04000150 RID: 336
			public const string rsItemContentInvalid = "rsItemContentInvalid";

			// Token: 0x04000151 RID: 337
			public const string rsMaxCountComments = "rsMaxCountComments";

			// Token: 0x04000152 RID: 338
			public const string rsWrongItemType = "rsWrongItemType";

			// Token: 0x04000153 RID: 339
			public const string rsSnapshotVersionMismatch = "rsSnapshotVersionMismatch";

			// Token: 0x04000154 RID: 340
			public const string rsReadOnlyReportParameter = "rsReadOnlyReportParameter";

			// Token: 0x04000155 RID: 341
			public const string rsReadOnlyDataSetParameter = "rsReadOnlyDataSetParameter";

			// Token: 0x04000156 RID: 342
			public const string rsUnknownReportParameter = "rsUnknownReportParameter";

			// Token: 0x04000157 RID: 343
			public const string rsUnknownDataSetParameter = "rsUnknownDataSetParameter";

			// Token: 0x04000158 RID: 344
			public const string rsReportParameterValueNotSet = "rsReportParameterValueNotSet";

			// Token: 0x04000159 RID: 345
			public const string rsDataSetParameterValueNotSet = "rsDataSetParameterValueNotSet";

			// Token: 0x0400015A RID: 346
			public const string rsReportParameterTypeMismatch = "rsReportParameterTypeMismatch";

			// Token: 0x0400015B RID: 347
			public const string rsInvalidReportParameter = "rsInvalidReportParameter";

			// Token: 0x0400015C RID: 348
			public const string rsDataSourceNotFound = "rsDataSourceNotFound";

			// Token: 0x0400015D RID: 349
			public const string rsDataSourceNoPromptException = "rsDataSourceNoPromptException";

			// Token: 0x0400015E RID: 350
			public const string rsInvalidDataSourceCredentialSetting = "rsInvalidDataSourceCredentialSetting";

			// Token: 0x0400015F RID: 351
			public const string rsDatasourceCredentialsNoLongerValid = "rsDatasourceCredentialsNoLongerValid";

			// Token: 0x04000160 RID: 352
			public const string rsInvalidDataSourceCredentialSettingForITokenDataExtension = "rsInvalidDataSourceCredentialSettingForITokenDataExtension";

			// Token: 0x04000161 RID: 353
			public const string rsWindowsIntegratedSecurityDisabled = "rsWindowsIntegratedSecurityDisabled";

			// Token: 0x04000162 RID: 354
			public const string internalDataSourceNotFound = "internalDataSourceNotFound";

			// Token: 0x04000163 RID: 355
			public const string cannotBuildExternalConnectionString = "cannotBuildExternalConnectionString";

			// Token: 0x04000164 RID: 356
			public const string rsDataSourceDisabled = "rsDataSourceDisabled";

			// Token: 0x04000165 RID: 357
			public const string rsInvalidDataSourceReference = "rsInvalidDataSourceReference";

			// Token: 0x04000166 RID: 358
			public const string rsInvalidDataSetReference = "rsInvalidDataSetReference";

			// Token: 0x04000167 RID: 359
			public const string rsInvalidDataSourceType = "rsInvalidDataSourceType";

			// Token: 0x04000168 RID: 360
			public const string rsInvalidDataSourceCount = "rsInvalidDataSourceCount";

			// Token: 0x04000169 RID: 361
			public const string rsCannotRetrieveModel = "rsCannotRetrieveModel";

			// Token: 0x0400016A RID: 362
			public const string rsModelRetrievalCanceled = "rsModelRetrievalCanceled";

			// Token: 0x0400016B RID: 363
			public const string rsExecuteQueriesFailure = "rsExecuteQueriesFailure";

			// Token: 0x0400016C RID: 364
			public const string rsDataSetExecutionError = "rsDataSetExecutionError";

			// Token: 0x0400016D RID: 365
			public const string rsUnknownUserName = "rsUnknownUserName";

			// Token: 0x0400016E RID: 366
			public const string rsInternalError = "rsInternalError";

			// Token: 0x0400016F RID: 367
			public const string rsStreamOperationFailed = "rsStreamOperationFailed";

			// Token: 0x04000170 RID: 368
			public const string rsNotSupported = "rsNotSupported";

			// Token: 0x04000171 RID: 369
			public const string rsNotEnabled = "rsNotEnabled";

			// Token: 0x04000172 RID: 370
			public const string rsReportServerDatabaseLogonFailed = "rsReportServerDatabaseLogonFailed";

			// Token: 0x04000173 RID: 371
			public const string rsDataExtensionNotFound = "rsDataExtensionNotFound";

			// Token: 0x04000174 RID: 372
			public const string rsReportTimeoutExpired = "rsReportTimeoutExpired";

			// Token: 0x04000175 RID: 373
			public const string rsJobWasCanceled = "rsJobWasCanceled";

			// Token: 0x04000176 RID: 374
			public const string rsOperationNotSupported = "rsOperationNotSupported";

			// Token: 0x04000177 RID: 375
			public const string rsServerConfigurationError = "rsServerConfigurationError";

			// Token: 0x04000178 RID: 376
			public const string rsWinAuthz = "rsWinAuthz";

			// Token: 0x04000179 RID: 377
			public const string rsWinAuthz5 = "rsWinAuthz5";

			// Token: 0x0400017A RID: 378
			public const string rsWinAuthz1355 = "rsWinAuthz1355";

			// Token: 0x0400017B RID: 379
			public const string rsEventLogSourceNotFound = "rsEventLogSourceNotFound";

			// Token: 0x0400017C RID: 380
			public const string rsLogonFailed = "rsLogonFailed";

			// Token: 0x0400017D RID: 381
			public const string rsEncryptedDataUnavailable = "rsEncryptedDataUnavailable";

			// Token: 0x0400017E RID: 382
			public const string rsErrorNotVisibleToRemoteBrowsers = "rsErrorNotVisibleToRemoteBrowsers";

			// Token: 0x0400017F RID: 383
			public const string rsFileExtensionRequired = "rsFileExtensionRequired";

			// Token: 0x04000180 RID: 384
			public const string rsFileExtensionViolation = "rsFileExtensionViolation";

			// Token: 0x04000181 RID: 385
			public const string rsDataSetNotFound = "rsDataSetNotFound";

			// Token: 0x04000182 RID: 386
			public const string rsComponentPublishingError = "rsComponentPublishingError";

			// Token: 0x04000183 RID: 387
			public const string rsInvalidProgressiveFormatError = "rsInvalidProgressiveFormatError";

			// Token: 0x04000184 RID: 388
			public const string rsProgressiveFormatElementMissingError = "rsProgressiveFormatElementMissingError";

			// Token: 0x04000185 RID: 389
			public const string rsProgressiveMessageWriteError = "rsProgressiveMessageWriteError";

			// Token: 0x04000186 RID: 390
			public const string rsProgressiveMessageWriteElementError = "rsProgressiveMessageWriteElementError";

			// Token: 0x04000187 RID: 391
			public const string LogClientTraceEventsInvalidSyntax = "LogClientTraceEventsInvalidSyntax";

			// Token: 0x04000188 RID: 392
			public const string LogClientTraceEventsInvalidNamespace = "LogClientTraceEventsInvalidNamespace";

			// Token: 0x04000189 RID: 393
			public const string rsVersionMismatch = "rsVersionMismatch";

			// Token: 0x0400018A RID: 394
			public const string rsClosingRegisteredStreamException = "rsClosingRegisteredStreamException";

			// Token: 0x0400018B RID: 395
			public const string rsInvalidSessionId = "rsInvalidSessionId";

			// Token: 0x0400018C RID: 396
			public const string rsInvalidConcurrentRenderEditSessionRequest = "rsInvalidConcurrentRenderEditSessionRequest";

			// Token: 0x0400018D RID: 397
			public const string rsSessionNotFound = "rsSessionNotFound";

			// Token: 0x0400018E RID: 398
			public const string rsReportSerializationError = "rsReportSerializationError";

			// Token: 0x0400018F RID: 399
			public const string rsInvalidSessionCatalogItems = "rsInvalidSessionCatalogItems";

			// Token: 0x04000190 RID: 400
			public const string MultipleSessionCatalogItemsNotSupported = "MultipleSessionCatalogItemsNotSupported";

			// Token: 0x04000191 RID: 401
			public const string rsApiVersionDiscontinued = "rsApiVersionDiscontinued";

			// Token: 0x04000192 RID: 402
			public const string rsApiVersionNotRecognized = "rsApiVersionNotRecognized";

			// Token: 0x04000193 RID: 403
			public const string rsRequestEncodingFormatException = "rsRequestEncodingFormatException";

			// Token: 0x04000194 RID: 404
			public const string rsCertificateMissingOrInvalid = "rsCertificateMissingOrInvalid";

			// Token: 0x04000195 RID: 405
			public const string rsResolutionFailureException = "rsResolutionFailureException";

			// Token: 0x04000196 RID: 406
			public const string rsReportServerStorageSingleRefreshConnectionExpected = "rsReportServerStorageSingleRefreshConnectionExpected";

			// Token: 0x04000197 RID: 407
			public const string rsReportServerStorageRefreshConnectionNotValidated = "rsReportServerStorageRefreshConnectionNotValidated";

			// Token: 0x04000198 RID: 408
			public const string rsOnPremConnectionBuilderUnknownError = "rsOnPremConnectionBuilderUnknownError";

			// Token: 0x04000199 RID: 409
			public const string rsOnPremConnectionBuilderConnectionStringMissing = "rsOnPremConnectionBuilderConnectionStringMissing";

			// Token: 0x0400019A RID: 410
			public const string rsOnPremConnectionBuilderMissingEffectiveUsername = "rsOnPremConnectionBuilderMissingEffectiveUsername";

			// Token: 0x0400019B RID: 411
			public const string rsIdentityClaimsMissingOrInvalid = "rsIdentityClaimsMissingOrInvalid";

			// Token: 0x0400019C RID: 412
			public const string rsSystemResourcePackageMetadataNotFound = "rsSystemResourcePackageMetadataNotFound";

			// Token: 0x0400019D RID: 413
			public const string rsSystemResourcePackageMetadataInvalid = "rsSystemResourcePackageMetadataInvalid";

			// Token: 0x0400019E RID: 414
			public const string rsSystemResourcePackageReferencedItemMissing = "rsSystemResourcePackageReferencedItemMissing";

			// Token: 0x0400019F RID: 415
			public const string rsSystemResourcePackageRequiredItemMissing = "rsSystemResourcePackageRequiredItemMissing";

			// Token: 0x040001A0 RID: 416
			public const string rsSystemResourcePackageItemContentTypeMismatch = "rsSystemResourcePackageItemContentTypeMismatch";

			// Token: 0x040001A1 RID: 417
			public const string rsSystemResourcePackageItemExtensionMismatch = "rsSystemResourcePackageItemExtensionMismatch";

			// Token: 0x040001A2 RID: 418
			public const string rsSystemResourcePackageValidationFailed = "rsSystemResourcePackageValidationFailed";

			// Token: 0x040001A3 RID: 419
			public const string rsSystemResourcePackageWrongType = "rsSystemResourcePackageWrongType";

			// Token: 0x040001A4 RID: 420
			public const string rsAuthorizationTokenInvalidOrExpired = "rsAuthorizationTokenInvalidOrExpired";
		}
	}
}
