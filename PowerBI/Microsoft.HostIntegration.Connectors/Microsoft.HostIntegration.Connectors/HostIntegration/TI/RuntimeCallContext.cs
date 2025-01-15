using System;
using System.Collections;
using System.Transactions;
using Microsoft.HostIntegration.Common;
using Microsoft.HostIntegration.EventLogging;
using Microsoft.HostIntegration.Tracing;

namespace Microsoft.HostIntegration.TI
{
	// Token: 0x02000745 RID: 1861
	public class RuntimeCallContext
	{
		// Token: 0x06003AB4 RID: 15028 RVA: 0x000C699A File Offset: 0x000C4B9A
		public RuntimeCallContext(EventLogContainer eventLogging, Hashtable specialOverrides)
		{
			this.InternalConstructor(eventLogging, null, specialOverrides);
		}

		// Token: 0x06003AB5 RID: 15029 RVA: 0x000C69AB File Offset: 0x000C4BAB
		public RuntimeCallContext(EventLogContainer eventLogging, BaseTracePoint tracePoint, Hashtable specialOverrides)
		{
			this.InternalConstructor(eventLogging, tracePoint, specialOverrides);
		}

		// Token: 0x06003AB6 RID: 15030 RVA: 0x000C69BC File Offset: 0x000C4BBC
		private void InternalConstructor(EventLogContainer eventLogging, BaseTracePoint tracePoint, Hashtable specialOverrides)
		{
			this.LEId = -1;
			this.HEId = -1;
			this.CodePage = 37;
			this.EnvTransport = -1;
			this.MethodIdentity = -1;
			this.IsForClient = true;
			this.DispId = -666;
			this.CurrentParameterIndex = -666;
			this.EventLogging = eventLogging;
			this.TracePoint = tracePoint;
			this.HostUserId = "";
			this.UsedPort = "";
			this.UsedIPAddress = "";
			this.UsedLocalLUName = "";
			this.UsedPartnerLUName = "";
			this.UsedProgramName = "";
			this.UsedTransactionId = "";
			this.overrides = specialOverrides;
			string text = this.GetOverrideValue("CallAccountingProcessor") as string;
			if (text != null)
			{
				Type type = Type.GetType(text);
				if (type != null)
				{
					RuntimeCallContext.callAccountingProcessor = Activator.CreateInstance(type) as ICallAccountingProcessor;
				}
			}
			string text2 = this.GetOverrideValue("CustomREDispenser") as string;
			if (text2 != null)
			{
				Type type2 = Type.GetType(text2);
				if (type2 != null)
				{
					RuntimeCallContext.customREDispenser = Activator.CreateInstance(type2) as ICustomREDispenser;
				}
			}
		}

		// Token: 0x17000D48 RID: 3400
		// (get) Token: 0x06003AB7 RID: 15031 RVA: 0x000C6AD4 File Offset: 0x000C4CD4
		// (set) Token: 0x06003AB8 RID: 15032 RVA: 0x000C6ADC File Offset: 0x000C4CDC
		public object HostFilesConnection { get; set; }

		// Token: 0x17000D49 RID: 3401
		// (get) Token: 0x06003AB9 RID: 15033 RVA: 0x000C6AE5 File Offset: 0x000C4CE5
		// (set) Token: 0x06003ABA RID: 15034 RVA: 0x000C6AED File Offset: 0x000C4CED
		public string UsedIPAddress { get; set; }

		// Token: 0x17000D4A RID: 3402
		// (get) Token: 0x06003ABB RID: 15035 RVA: 0x000C6AF6 File Offset: 0x000C4CF6
		// (set) Token: 0x06003ABC RID: 15036 RVA: 0x000C6AFE File Offset: 0x000C4CFE
		public string UsedPartnerLUName { get; set; }

		// Token: 0x17000D4B RID: 3403
		// (get) Token: 0x06003ABD RID: 15037 RVA: 0x000C6B07 File Offset: 0x000C4D07
		// (set) Token: 0x06003ABE RID: 15038 RVA: 0x000C6B0F File Offset: 0x000C4D0F
		public string UsedLocalLUName { get; set; }

		// Token: 0x17000D4C RID: 3404
		// (get) Token: 0x06003ABF RID: 15039 RVA: 0x000C6B18 File Offset: 0x000C4D18
		// (set) Token: 0x06003AC0 RID: 15040 RVA: 0x000C6B20 File Offset: 0x000C4D20
		public string UsedTransactionId { get; set; }

		// Token: 0x17000D4D RID: 3405
		// (get) Token: 0x06003AC1 RID: 15041 RVA: 0x000C6B29 File Offset: 0x000C4D29
		// (set) Token: 0x06003AC2 RID: 15042 RVA: 0x000C6B31 File Offset: 0x000C4D31
		public string UsedProgramName { get; set; }

		// Token: 0x17000D4E RID: 3406
		// (get) Token: 0x06003AC3 RID: 15043 RVA: 0x000C6B3A File Offset: 0x000C4D3A
		// (set) Token: 0x06003AC4 RID: 15044 RVA: 0x000C6B42 File Offset: 0x000C4D42
		public string UsedPort { get; set; }

		// Token: 0x17000D4F RID: 3407
		// (get) Token: 0x06003AC5 RID: 15045 RVA: 0x000C6B4B File Offset: 0x000C4D4B
		// (set) Token: 0x06003AC6 RID: 15046 RVA: 0x000C6B53 File Offset: 0x000C4D53
		public string HostUserId { get; set; }

		// Token: 0x17000D50 RID: 3408
		// (get) Token: 0x06003AC7 RID: 15047 RVA: 0x000C6B5C File Offset: 0x000C4D5C
		// (set) Token: 0x06003AC8 RID: 15048 RVA: 0x000C6B64 File Offset: 0x000C4D64
		public DateTime CallStartTime { get; set; }

		// Token: 0x17000D51 RID: 3409
		// (get) Token: 0x06003AC9 RID: 15049 RVA: 0x000C6B6D File Offset: 0x000C4D6D
		// (set) Token: 0x06003ACA RID: 15050 RVA: 0x000C6B75 File Offset: 0x000C4D75
		public DateTime CallStopTime { get; set; }

		// Token: 0x17000D52 RID: 3410
		// (get) Token: 0x06003ACB RID: 15051 RVA: 0x000C6B7E File Offset: 0x000C4D7E
		// (set) Token: 0x06003ACC RID: 15052 RVA: 0x000C6B86 File Offset: 0x000C4D86
		public DateTime HostCallHeaderStartTime { get; set; }

		// Token: 0x17000D53 RID: 3411
		// (get) Token: 0x06003ACD RID: 15053 RVA: 0x000C6B8F File Offset: 0x000C4D8F
		// (set) Token: 0x06003ACE RID: 15054 RVA: 0x000C6B97 File Offset: 0x000C4D97
		public DateTime HostCallHeaderStopTime { get; set; }

		// Token: 0x17000D54 RID: 3412
		// (get) Token: 0x06003ACF RID: 15055 RVA: 0x000C6BA0 File Offset: 0x000C4DA0
		// (set) Token: 0x06003AD0 RID: 15056 RVA: 0x000C6BA8 File Offset: 0x000C4DA8
		public DateTime HostCallStartTime { get; set; }

		// Token: 0x17000D55 RID: 3413
		// (get) Token: 0x06003AD1 RID: 15057 RVA: 0x000C6BB1 File Offset: 0x000C4DB1
		// (set) Token: 0x06003AD2 RID: 15058 RVA: 0x000C6BB9 File Offset: 0x000C4DB9
		public DateTime HostCallStopTime { get; set; }

		// Token: 0x17000D56 RID: 3414
		// (get) Token: 0x06003AD3 RID: 15059 RVA: 0x000C6BC2 File Offset: 0x000C4DC2
		// (set) Token: 0x06003AD4 RID: 15060 RVA: 0x000C6BCA File Offset: 0x000C4DCA
		public int TotalBytesSent { get; set; }

		// Token: 0x17000D57 RID: 3415
		// (get) Token: 0x06003AD5 RID: 15061 RVA: 0x000C6BD3 File Offset: 0x000C4DD3
		// (set) Token: 0x06003AD6 RID: 15062 RVA: 0x000C6BDB File Offset: 0x000C4DDB
		public int TotalBytesReceived { get; set; }

		// Token: 0x17000D58 RID: 3416
		// (get) Token: 0x06003AD7 RID: 15063 RVA: 0x000C6BE4 File Offset: 0x000C4DE4
		// (set) Token: 0x06003AD8 RID: 15064 RVA: 0x000C6BEC File Offset: 0x000C4DEC
		public EventLogContainer EventLogging { get; set; }

		// Token: 0x17000D59 RID: 3417
		// (get) Token: 0x06003AD9 RID: 15065 RVA: 0x000C6BF5 File Offset: 0x000C4DF5
		// (set) Token: 0x06003ADA RID: 15066 RVA: 0x000C6BFD File Offset: 0x000C4DFD
		public BaseTracePoint TracePoint { get; set; }

		// Token: 0x17000D5A RID: 3418
		// (get) Token: 0x06003ADB RID: 15067 RVA: 0x000C6C06 File Offset: 0x000C4E06
		// (set) Token: 0x06003ADC RID: 15068 RVA: 0x000C6C0E File Offset: 0x000C4E0E
		public TIConnectionTimer TIConnectionTimer { get; set; }

		// Token: 0x17000D5B RID: 3419
		// (get) Token: 0x06003ADD RID: 15069 RVA: 0x000C6C17 File Offset: 0x000C4E17
		// (set) Token: 0x06003ADE RID: 15070 RVA: 0x000C6C1F File Offset: 0x000C4E1F
		public CommonContextControlInformation ContextControlInformation { get; set; }

		// Token: 0x17000D5C RID: 3420
		// (get) Token: 0x06003ADF RID: 15071 RVA: 0x000C6C28 File Offset: 0x000C4E28
		// (set) Token: 0x06003AE0 RID: 15072 RVA: 0x000C6C30 File Offset: 0x000C4E30
		public object HIPProxy { get; set; }

		// Token: 0x17000D5D RID: 3421
		// (get) Token: 0x06003AE1 RID: 15073 RVA: 0x000C6C39 File Offset: 0x000C4E39
		// (set) Token: 0x06003AE2 RID: 15074 RVA: 0x000C6C41 File Offset: 0x000C4E41
		public object Invoker { get; set; }

		// Token: 0x17000D5E RID: 3422
		// (get) Token: 0x06003AE3 RID: 15075 RVA: 0x000C6C4A File Offset: 0x000C4E4A
		// (set) Token: 0x06003AE4 RID: 15076 RVA: 0x000C6C52 File Offset: 0x000C4E52
		public object ClientContext { get; set; }

		// Token: 0x17000D5F RID: 3423
		// (get) Token: 0x06003AE5 RID: 15077 RVA: 0x000C6C5B File Offset: 0x000C4E5B
		// (set) Token: 0x06003AE6 RID: 15078 RVA: 0x000C6C63 File Offset: 0x000C4E63
		public object ServerContext { get; set; }

		// Token: 0x17000D60 RID: 3424
		// (get) Token: 0x06003AE7 RID: 15079 RVA: 0x000C6C6C File Offset: 0x000C4E6C
		// (set) Token: 0x06003AE8 RID: 15080 RVA: 0x000C6C74 File Offset: 0x000C4E74
		public string ClientHostName { get; set; }

		// Token: 0x17000D61 RID: 3425
		// (get) Token: 0x06003AE9 RID: 15081 RVA: 0x000C6C7D File Offset: 0x000C4E7D
		// (set) Token: 0x06003AEA RID: 15082 RVA: 0x000C6C85 File Offset: 0x000C4E85
		public object LibraryReader { get; set; }

		// Token: 0x17000D62 RID: 3426
		// (get) Token: 0x06003AEB RID: 15083 RVA: 0x000C6C8E File Offset: 0x000C4E8E
		// (set) Token: 0x06003AEC RID: 15084 RVA: 0x000C6C96 File Offset: 0x000C4E96
		public object WipRuntimeAdmin { get; set; }

		// Token: 0x17000D63 RID: 3427
		// (get) Token: 0x06003AED RID: 15085 RVA: 0x000C6C9F File Offset: 0x000C4E9F
		// (set) Token: 0x06003AEE RID: 15086 RVA: 0x000C6CA7 File Offset: 0x000C4EA7
		public object PerformanceContainer { get; set; }

		// Token: 0x17000D64 RID: 3428
		// (get) Token: 0x06003AEF RID: 15087 RVA: 0x000C6CB0 File Offset: 0x000C4EB0
		// (set) Token: 0x06003AF0 RID: 15088 RVA: 0x000C6CB8 File Offset: 0x000C4EB8
		public object CommonPerformanceContainer { get; set; }

		// Token: 0x17000D65 RID: 3429
		// (get) Token: 0x06003AF1 RID: 15089 RVA: 0x000C6CC1 File Offset: 0x000C4EC1
		// (set) Token: 0x06003AF2 RID: 15090 RVA: 0x000C6CC9 File Offset: 0x000C4EC9
		public object AdminInterface { get; set; }

		// Token: 0x17000D66 RID: 3430
		// (get) Token: 0x06003AF3 RID: 15091 RVA: 0x000C6CD2 File Offset: 0x000C4ED2
		// (set) Token: 0x06003AF4 RID: 15092 RVA: 0x000C6CDA File Offset: 0x000C4EDA
		public IPrimitiveConverter IPrimitiveConverter { get; set; }

		// Token: 0x17000D67 RID: 3431
		// (get) Token: 0x06003AF5 RID: 15093 RVA: 0x000C6CE3 File Offset: 0x000C4EE3
		// (set) Token: 0x06003AF6 RID: 15094 RVA: 0x000C6CEB File Offset: 0x000C4EEB
		public IAggregateConverter IAggregateConverter { get; set; }

		// Token: 0x17000D68 RID: 3432
		// (get) Token: 0x06003AF7 RID: 15095 RVA: 0x000C6CF4 File Offset: 0x000C4EF4
		// (set) Token: 0x06003AF8 RID: 15096 RVA: 0x000C6CFC File Offset: 0x000C4EFC
		public ITransport ITransport { get; set; }

		// Token: 0x17000D69 RID: 3433
		// (get) Token: 0x06003AF9 RID: 15097 RVA: 0x000C6D05 File Offset: 0x000C4F05
		// (set) Token: 0x06003AFA RID: 15098 RVA: 0x000C6D0D File Offset: 0x000C4F0D
		public IAppintStateMachine IAppintStateMachine { get; set; }

		// Token: 0x17000D6A RID: 3434
		// (get) Token: 0x06003AFB RID: 15099 RVA: 0x000C6D16 File Offset: 0x000C4F16
		// (set) Token: 0x06003AFC RID: 15100 RVA: 0x000C6D1E File Offset: 0x000C4F1E
		public IHIPFlowControl IHIPFlowControl { get; set; }

		// Token: 0x17000D6B RID: 3435
		// (get) Token: 0x06003AFD RID: 15101 RVA: 0x000C6D27 File Offset: 0x000C4F27
		// (set) Token: 0x06003AFE RID: 15102 RVA: 0x000C6D2F File Offset: 0x000C4F2F
		public BufferManager BufferManager { get; set; }

		// Token: 0x17000D6C RID: 3436
		// (get) Token: 0x06003AFF RID: 15103 RVA: 0x000C6D38 File Offset: 0x000C4F38
		// (set) Token: 0x06003B00 RID: 15104 RVA: 0x000C6D40 File Offset: 0x000C4F40
		public BaseRemoteEnvironment RemoteEnvironment { get; set; }

		// Token: 0x17000D6D RID: 3437
		// (get) Token: 0x06003B01 RID: 15105 RVA: 0x000C6D49 File Offset: 0x000C4F49
		// (set) Token: 0x06003B02 RID: 15106 RVA: 0x000C6D51 File Offset: 0x000C4F51
		public RemoteEnvironmentClass RemoteEnvironmentClass { get; set; }

		// Token: 0x17000D6E RID: 3438
		// (get) Token: 0x06003B03 RID: 15107 RVA: 0x000C6D5A File Offset: 0x000C4F5A
		// (set) Token: 0x06003B04 RID: 15108 RVA: 0x000C6D62 File Offset: 0x000C4F62
		public int DispId { get; set; }

		// Token: 0x17000D6F RID: 3439
		// (get) Token: 0x06003B05 RID: 15109 RVA: 0x000C6D6B File Offset: 0x000C4F6B
		// (set) Token: 0x06003B06 RID: 15110 RVA: 0x000C6D73 File Offset: 0x000C4F73
		public int CodePage { get; set; }

		// Token: 0x17000D70 RID: 3440
		// (get) Token: 0x06003B07 RID: 15111 RVA: 0x000C6D7C File Offset: 0x000C4F7C
		// (set) Token: 0x06003B08 RID: 15112 RVA: 0x000C6D84 File Offset: 0x000C4F84
		public int HEId { get; set; }

		// Token: 0x17000D71 RID: 3441
		// (get) Token: 0x06003B09 RID: 15113 RVA: 0x000C6D8D File Offset: 0x000C4F8D
		// (set) Token: 0x06003B0A RID: 15114 RVA: 0x000C6D95 File Offset: 0x000C4F95
		public int LEId { get; set; }

		// Token: 0x17000D72 RID: 3442
		// (get) Token: 0x06003B0B RID: 15115 RVA: 0x000C6D9E File Offset: 0x000C4F9E
		// (set) Token: 0x06003B0C RID: 15116 RVA: 0x000C6DA6 File Offset: 0x000C4FA6
		public int EnvTransport { get; set; }

		// Token: 0x17000D73 RID: 3443
		// (get) Token: 0x06003B0D RID: 15117 RVA: 0x000C6DAF File Offset: 0x000C4FAF
		// (set) Token: 0x06003B0E RID: 15118 RVA: 0x000C6DB7 File Offset: 0x000C4FB7
		public int MethodIdentity { get; set; }

		// Token: 0x17000D74 RID: 3444
		// (get) Token: 0x06003B0F RID: 15119 RVA: 0x000C6DC0 File Offset: 0x000C4FC0
		// (set) Token: 0x06003B10 RID: 15120 RVA: 0x000C6DC8 File Offset: 0x000C4FC8
		public int WorkPlanMethodIdentity { get; set; }

		// Token: 0x17000D75 RID: 3445
		// (get) Token: 0x06003B11 RID: 15121 RVA: 0x000C6DD1 File Offset: 0x000C4FD1
		// (set) Token: 0x06003B12 RID: 15122 RVA: 0x000C6DD9 File Offset: 0x000C4FD9
		public int CurrentParameterIndex { get; set; }

		// Token: 0x17000D76 RID: 3446
		// (get) Token: 0x06003B13 RID: 15123 RVA: 0x000C6DE2 File Offset: 0x000C4FE2
		// (set) Token: 0x06003B14 RID: 15124 RVA: 0x000C6DEA File Offset: 0x000C4FEA
		public bool IsForClient { get; set; }

		// Token: 0x17000D77 RID: 3447
		// (get) Token: 0x06003B15 RID: 15125 RVA: 0x000C6DF3 File Offset: 0x000C4FF3
		// (set) Token: 0x06003B16 RID: 15126 RVA: 0x000C6DFB File Offset: 0x000C4FFB
		public bool IsContinuedCall { get; set; }

		// Token: 0x17000D78 RID: 3448
		// (get) Token: 0x06003B17 RID: 15127 RVA: 0x000C6E04 File Offset: 0x000C5004
		// (set) Token: 0x06003B18 RID: 15128 RVA: 0x000C6E0C File Offset: 0x000C500C
		public bool UserCompatibleErrorCode { get; set; }

		// Token: 0x17000D79 RID: 3449
		// (get) Token: 0x06003B19 RID: 15129 RVA: 0x000C6E15 File Offset: 0x000C5015
		// (set) Token: 0x06003B1A RID: 15130 RVA: 0x000C6E1D File Offset: 0x000C501D
		public bool IsParticipatingInTransaction { get; set; }

		// Token: 0x17000D7A RID: 3450
		// (get) Token: 0x06003B1B RID: 15131 RVA: 0x000C6E26 File Offset: 0x000C5026
		// (set) Token: 0x06003B1C RID: 15132 RVA: 0x000C6E2E File Offset: 0x000C502E
		public bool DidTransportFinish { get; set; }

		// Token: 0x17000D7B RID: 3451
		// (get) Token: 0x06003B1D RID: 15133 RVA: 0x000C6E37 File Offset: 0x000C5037
		// (set) Token: 0x06003B1E RID: 15134 RVA: 0x000C6E3F File Offset: 0x000C503F
		public object RecoveryCorrelator { get; set; }

		// Token: 0x17000D7C RID: 3452
		// (get) Token: 0x06003B1F RID: 15135 RVA: 0x000C6E48 File Offset: 0x000C5048
		// (set) Token: 0x06003B20 RID: 15136 RVA: 0x000C6E50 File Offset: 0x000C5050
		public Transaction CurrentTransaction { get; set; }

		// Token: 0x17000D7D RID: 3453
		// (get) Token: 0x06003B21 RID: 15137 RVA: 0x000C6E59 File Offset: 0x000C5059
		// (set) Token: 0x06003B22 RID: 15138 RVA: 0x000C6E61 File Offset: 0x000C5061
		public short MetaDataErrBlockReturnError { get; set; }

		// Token: 0x17000D7E RID: 3454
		// (get) Token: 0x06003B23 RID: 15139 RVA: 0x000C6E6A File Offset: 0x000C506A
		// (set) Token: 0x06003B24 RID: 15140 RVA: 0x000C6E72 File Offset: 0x000C5072
		public short MetaDataErrBlockErrorCode { get; set; }

		// Token: 0x17000D7F RID: 3455
		// (get) Token: 0x06003B25 RID: 15141 RVA: 0x000C6E7B File Offset: 0x000C507B
		// (set) Token: 0x06003B26 RID: 15142 RVA: 0x000C6E83 File Offset: 0x000C5083
		public string MetaDataErrBlockErrorMessage { get; set; }

		// Token: 0x17000D80 RID: 3456
		// (get) Token: 0x06003B27 RID: 15143 RVA: 0x000C6E8C File Offset: 0x000C508C
		public ICustomREDispenser ICustomREDispenser
		{
			get
			{
				return RuntimeCallContext.customREDispenser;
			}
		}

		// Token: 0x17000D81 RID: 3457
		// (get) Token: 0x06003B28 RID: 15144 RVA: 0x000C6E93 File Offset: 0x000C5093
		public ICallAccountingProcessor ICallAccountingProcessor
		{
			get
			{
				return RuntimeCallContext.callAccountingProcessor;
			}
		}

		// Token: 0x06003B29 RID: 15145 RVA: 0x000C6E9C File Offset: 0x000C509C
		public object GetOverrideValue(string OverrideName)
		{
			object obj = null;
			if (this.overrides != null)
			{
				obj = this.overrides[OverrideName];
			}
			return obj;
		}

		// Token: 0x04002349 RID: 9033
		private static ICallAccountingProcessor callAccountingProcessor;

		// Token: 0x0400234A RID: 9034
		private static ICustomREDispenser customREDispenser;

		// Token: 0x0400234B RID: 9035
		private Hashtable overrides;
	}
}
