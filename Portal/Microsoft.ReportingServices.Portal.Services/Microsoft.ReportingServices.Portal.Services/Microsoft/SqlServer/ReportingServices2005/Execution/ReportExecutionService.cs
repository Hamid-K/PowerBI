using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005.Execution
{
	// Token: 0x02000074 RID: 116
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[WebServiceBinding(Name = "ReportExecutionServiceSoap", Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[XmlInclude(typeof(ParameterValueOrFieldReference))]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[ToolboxItem(false)]
	public class ReportExecutionService : SoapHttpClientProtocol
	{
		// Token: 0x06000406 RID: 1030 RVA: 0x00017E3B File Offset: 0x0001603B
		public ReportExecutionService()
		{
			base.Url = "http://localhost/ReportServer/ReportExecution2005.asmx";
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x00017E4E File Offset: 0x0001604E
		// (set) Token: 0x06000408 RID: 1032 RVA: 0x00017E56 File Offset: 0x00016056
		public TrustedUserHeader TrustedUserHeaderValue
		{
			get
			{
				return this.trustedUserHeaderValueField;
			}
			set
			{
				this.trustedUserHeaderValueField = value;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x00017E5F File Offset: 0x0001605F
		// (set) Token: 0x0600040A RID: 1034 RVA: 0x00017E67 File Offset: 0x00016067
		public ServerInfoHeader ServerInfoHeaderValue
		{
			get
			{
				return this.serverInfoHeaderValueField;
			}
			set
			{
				this.serverInfoHeaderValueField = value;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x00017E70 File Offset: 0x00016070
		// (set) Token: 0x0600040C RID: 1036 RVA: 0x00017E78 File Offset: 0x00016078
		public ExecutionHeader ExecutionHeaderValue
		{
			get
			{
				return this.executionHeaderValueField;
			}
			set
			{
				this.executionHeaderValueField = value;
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600040D RID: 1037 RVA: 0x00017E84 File Offset: 0x00016084
		// (remove) Token: 0x0600040E RID: 1038 RVA: 0x00017EBC File Offset: 0x000160BC
		public event ListSecureMethodsCompletedEventHandler ListSecureMethodsCompleted;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600040F RID: 1039 RVA: 0x00017EF4 File Offset: 0x000160F4
		// (remove) Token: 0x06000410 RID: 1040 RVA: 0x00017F2C File Offset: 0x0001612C
		public event LoadReportCompletedEventHandler LoadReportCompleted;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000411 RID: 1041 RVA: 0x00017F64 File Offset: 0x00016164
		// (remove) Token: 0x06000412 RID: 1042 RVA: 0x00017F9C File Offset: 0x0001619C
		public event LoadReport3CompletedEventHandler LoadReport3Completed;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000413 RID: 1043 RVA: 0x00017FD4 File Offset: 0x000161D4
		// (remove) Token: 0x06000414 RID: 1044 RVA: 0x0001800C File Offset: 0x0001620C
		public event LoadReport2CompletedEventHandler LoadReport2Completed;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000415 RID: 1045 RVA: 0x00018044 File Offset: 0x00016244
		// (remove) Token: 0x06000416 RID: 1046 RVA: 0x0001807C File Offset: 0x0001627C
		public event LoadReportDefinitionCompletedEventHandler LoadReportDefinitionCompleted;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000417 RID: 1047 RVA: 0x000180B4 File Offset: 0x000162B4
		// (remove) Token: 0x06000418 RID: 1048 RVA: 0x000180EC File Offset: 0x000162EC
		public event LoadReportDefinition2CompletedEventHandler LoadReportDefinition2Completed;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06000419 RID: 1049 RVA: 0x00018124 File Offset: 0x00016324
		// (remove) Token: 0x0600041A RID: 1050 RVA: 0x0001815C File Offset: 0x0001635C
		public event LoadReportDefinition3CompletedEventHandler LoadReportDefinition3Completed;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x0600041B RID: 1051 RVA: 0x00018194 File Offset: 0x00016394
		// (remove) Token: 0x0600041C RID: 1052 RVA: 0x000181CC File Offset: 0x000163CC
		public event SetExecutionCredentialsCompletedEventHandler SetExecutionCredentialsCompleted;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x0600041D RID: 1053 RVA: 0x00018204 File Offset: 0x00016404
		// (remove) Token: 0x0600041E RID: 1054 RVA: 0x0001823C File Offset: 0x0001643C
		public event SetExecutionCredentials2CompletedEventHandler SetExecutionCredentials2Completed;

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x0600041F RID: 1055 RVA: 0x00018274 File Offset: 0x00016474
		// (remove) Token: 0x06000420 RID: 1056 RVA: 0x000182AC File Offset: 0x000164AC
		public event SetExecutionCredentials3CompletedEventHandler SetExecutionCredentials3Completed;

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06000421 RID: 1057 RVA: 0x000182E4 File Offset: 0x000164E4
		// (remove) Token: 0x06000422 RID: 1058 RVA: 0x0001831C File Offset: 0x0001651C
		public event SetExecutionParametersCompletedEventHandler SetExecutionParametersCompleted;

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06000423 RID: 1059 RVA: 0x00018354 File Offset: 0x00016554
		// (remove) Token: 0x06000424 RID: 1060 RVA: 0x0001838C File Offset: 0x0001658C
		public event SetExecutionParameters2CompletedEventHandler SetExecutionParameters2Completed;

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06000425 RID: 1061 RVA: 0x000183C4 File Offset: 0x000165C4
		// (remove) Token: 0x06000426 RID: 1062 RVA: 0x000183FC File Offset: 0x000165FC
		public event SetExecutionParameters3CompletedEventHandler SetExecutionParameters3Completed;

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06000427 RID: 1063 RVA: 0x00018434 File Offset: 0x00016634
		// (remove) Token: 0x06000428 RID: 1064 RVA: 0x0001846C File Offset: 0x0001666C
		public event ResetExecutionCompletedEventHandler ResetExecutionCompleted;

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06000429 RID: 1065 RVA: 0x000184A4 File Offset: 0x000166A4
		// (remove) Token: 0x0600042A RID: 1066 RVA: 0x000184DC File Offset: 0x000166DC
		public event ResetExecution2CompletedEventHandler ResetExecution2Completed;

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x0600042B RID: 1067 RVA: 0x00018514 File Offset: 0x00016714
		// (remove) Token: 0x0600042C RID: 1068 RVA: 0x0001854C File Offset: 0x0001674C
		public event ResetExecution3CompletedEventHandler ResetExecution3Completed;

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x0600042D RID: 1069 RVA: 0x00018584 File Offset: 0x00016784
		// (remove) Token: 0x0600042E RID: 1070 RVA: 0x000185BC File Offset: 0x000167BC
		public event RenderCompletedEventHandler RenderCompleted;

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x0600042F RID: 1071 RVA: 0x000185F4 File Offset: 0x000167F4
		// (remove) Token: 0x06000430 RID: 1072 RVA: 0x0001862C File Offset: 0x0001682C
		public event Render2CompletedEventHandler Render2Completed;

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x06000431 RID: 1073 RVA: 0x00018664 File Offset: 0x00016864
		// (remove) Token: 0x06000432 RID: 1074 RVA: 0x0001869C File Offset: 0x0001689C
		public event DeliverReportItemCompletedEventHandler DeliverReportItemCompleted;

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x06000433 RID: 1075 RVA: 0x000186D4 File Offset: 0x000168D4
		// (remove) Token: 0x06000434 RID: 1076 RVA: 0x0001870C File Offset: 0x0001690C
		public event RenderStreamCompletedEventHandler RenderStreamCompleted;

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x06000435 RID: 1077 RVA: 0x00018744 File Offset: 0x00016944
		// (remove) Token: 0x06000436 RID: 1078 RVA: 0x0001877C File Offset: 0x0001697C
		public event GetExecutionInfoCompletedEventHandler GetExecutionInfoCompleted;

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x06000437 RID: 1079 RVA: 0x000187B4 File Offset: 0x000169B4
		// (remove) Token: 0x06000438 RID: 1080 RVA: 0x000187EC File Offset: 0x000169EC
		public event GetExecutionInfo2CompletedEventHandler GetExecutionInfo2Completed;

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x06000439 RID: 1081 RVA: 0x00018824 File Offset: 0x00016A24
		// (remove) Token: 0x0600043A RID: 1082 RVA: 0x0001885C File Offset: 0x00016A5C
		public event GetExecutionInfo3CompletedEventHandler GetExecutionInfo3Completed;

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x0600043B RID: 1083 RVA: 0x00018894 File Offset: 0x00016A94
		// (remove) Token: 0x0600043C RID: 1084 RVA: 0x000188CC File Offset: 0x00016ACC
		public event GetDocumentMapCompletedEventHandler GetDocumentMapCompleted;

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x0600043D RID: 1085 RVA: 0x00018904 File Offset: 0x00016B04
		// (remove) Token: 0x0600043E RID: 1086 RVA: 0x0001893C File Offset: 0x00016B3C
		public event LoadDrillthroughTargetCompletedEventHandler LoadDrillthroughTargetCompleted;

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x0600043F RID: 1087 RVA: 0x00018974 File Offset: 0x00016B74
		// (remove) Token: 0x06000440 RID: 1088 RVA: 0x000189AC File Offset: 0x00016BAC
		public event LoadDrillthroughTarget2CompletedEventHandler LoadDrillthroughTarget2Completed;

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x06000441 RID: 1089 RVA: 0x000189E4 File Offset: 0x00016BE4
		// (remove) Token: 0x06000442 RID: 1090 RVA: 0x00018A1C File Offset: 0x00016C1C
		public event LoadDrillthroughTarget3CompletedEventHandler LoadDrillthroughTarget3Completed;

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x06000443 RID: 1091 RVA: 0x00018A54 File Offset: 0x00016C54
		// (remove) Token: 0x06000444 RID: 1092 RVA: 0x00018A8C File Offset: 0x00016C8C
		public event ToggleItemCompletedEventHandler ToggleItemCompleted;

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x06000445 RID: 1093 RVA: 0x00018AC4 File Offset: 0x00016CC4
		// (remove) Token: 0x06000446 RID: 1094 RVA: 0x00018AFC File Offset: 0x00016CFC
		public event NavigateDocumentMapCompletedEventHandler NavigateDocumentMapCompleted;

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x06000447 RID: 1095 RVA: 0x00018B34 File Offset: 0x00016D34
		// (remove) Token: 0x06000448 RID: 1096 RVA: 0x00018B6C File Offset: 0x00016D6C
		public event NavigateBookmarkCompletedEventHandler NavigateBookmarkCompleted;

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x06000449 RID: 1097 RVA: 0x00018BA4 File Offset: 0x00016DA4
		// (remove) Token: 0x0600044A RID: 1098 RVA: 0x00018BDC File Offset: 0x00016DDC
		public event FindStringCompletedEventHandler FindStringCompleted;

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x0600044B RID: 1099 RVA: 0x00018C14 File Offset: 0x00016E14
		// (remove) Token: 0x0600044C RID: 1100 RVA: 0x00018C4C File Offset: 0x00016E4C
		public event SortCompletedEventHandler SortCompleted;

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x0600044D RID: 1101 RVA: 0x00018C84 File Offset: 0x00016E84
		// (remove) Token: 0x0600044E RID: 1102 RVA: 0x00018CBC File Offset: 0x00016EBC
		public event Sort2CompletedEventHandler Sort2Completed;

		// Token: 0x14000026 RID: 38
		// (add) Token: 0x0600044F RID: 1103 RVA: 0x00018CF4 File Offset: 0x00016EF4
		// (remove) Token: 0x06000450 RID: 1104 RVA: 0x00018D2C File Offset: 0x00016F2C
		public event Sort3CompletedEventHandler Sort3Completed;

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x06000451 RID: 1105 RVA: 0x00018D64 File Offset: 0x00016F64
		// (remove) Token: 0x06000452 RID: 1106 RVA: 0x00018D9C File Offset: 0x00016F9C
		public event GetRenderResourceCompletedEventHandler GetRenderResourceCompleted;

		// Token: 0x14000028 RID: 40
		// (add) Token: 0x06000453 RID: 1107 RVA: 0x00018DD4 File Offset: 0x00016FD4
		// (remove) Token: 0x06000454 RID: 1108 RVA: 0x00018E0C File Offset: 0x0001700C
		public event ListRenderingExtensionsCompletedEventHandler ListRenderingExtensionsCompleted;

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x06000455 RID: 1109 RVA: 0x00018E44 File Offset: 0x00017044
		// (remove) Token: 0x06000456 RID: 1110 RVA: 0x00018E7C File Offset: 0x0001707C
		public event LogonUserCompletedEventHandler LogonUserCompleted;

		// Token: 0x1400002A RID: 42
		// (add) Token: 0x06000457 RID: 1111 RVA: 0x00018EB4 File Offset: 0x000170B4
		// (remove) Token: 0x06000458 RID: 1112 RVA: 0x00018EEC File Offset: 0x000170EC
		public event LogoffCompletedEventHandler LogoffCompleted;

		// Token: 0x06000459 RID: 1113 RVA: 0x00018F21 File Offset: 0x00017121
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/ListSecureMethods", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public string[] ListSecureMethods()
		{
			return (string[])this.Invoke("ListSecureMethods", new object[0])[0];
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x00018F3B File Offset: 0x0001713B
		protected new object[] Invoke(string methodName, object[] parameters)
		{
			return base.Invoke(methodName, parameters);
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00018F45 File Offset: 0x00017145
		public IAsyncResult BeginListSecureMethods(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListSecureMethods", new object[0], callback, asyncState);
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x00018F5A File Offset: 0x0001715A
		public string[] EndListSecureMethods(IAsyncResult asyncResult)
		{
			return (string[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x00018F6A File Offset: 0x0001716A
		public void ListSecureMethodsAsync()
		{
			this.ListSecureMethodsAsync(null);
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x00018F73 File Offset: 0x00017173
		public void ListSecureMethodsAsync(object userState)
		{
			if (this.ListSecureMethodsOperationCompleted == null)
			{
				this.ListSecureMethodsOperationCompleted = new SendOrPostCallback(this.OnListSecureMethodsOperationCompleted);
			}
			base.InvokeAsync("ListSecureMethods", new object[0], this.ListSecureMethodsOperationCompleted, userState);
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00018FA8 File Offset: 0x000171A8
		private void OnListSecureMethodsOperationCompleted(object arg)
		{
			if (this.ListSecureMethodsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListSecureMethodsCompleted(this, new ListSecureMethodsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x00018FED File Offset: 0x000171ED
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExecutionHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/LoadReport", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("executionInfo")]
		public ExecutionInfo LoadReport(string Report, string HistoryID)
		{
			return (ExecutionInfo)this.Invoke("LoadReport", new object[] { Report, HistoryID })[0];
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0001900F File Offset: 0x0001720F
		public IAsyncResult BeginLoadReport(string Report, string HistoryID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("LoadReport", new object[] { Report, HistoryID }, callback, asyncState);
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0001902D File Offset: 0x0001722D
		public ExecutionInfo EndLoadReport(IAsyncResult asyncResult)
		{
			return (ExecutionInfo)base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0001903D File Offset: 0x0001723D
		public void LoadReportAsync(string Report, string HistoryID)
		{
			this.LoadReportAsync(Report, HistoryID, null);
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x00019048 File Offset: 0x00017248
		public void LoadReportAsync(string Report, string HistoryID, object userState)
		{
			if (this.LoadReportOperationCompleted == null)
			{
				this.LoadReportOperationCompleted = new SendOrPostCallback(this.OnLoadReportOperationCompleted);
			}
			base.InvokeAsync("LoadReport", new object[] { Report, HistoryID }, this.LoadReportOperationCompleted, userState);
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x00019084 File Offset: 0x00017284
		private void OnLoadReportOperationCompleted(object arg)
		{
			if (this.LoadReportCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.LoadReportCompleted(this, new LoadReportCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x000190C9 File Offset: 0x000172C9
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExecutionHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/LoadReport3", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("executionInfo")]
		public ExecutionInfo3 LoadReport3(string Report, string HistoryID)
		{
			return (ExecutionInfo3)this.Invoke("LoadReport3", new object[] { Report, HistoryID })[0];
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x000190EB File Offset: 0x000172EB
		public IAsyncResult BeginLoadReport3(string Report, string HistoryID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("LoadReport3", new object[] { Report, HistoryID }, callback, asyncState);
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00019109 File Offset: 0x00017309
		public ExecutionInfo3 EndLoadReport3(IAsyncResult asyncResult)
		{
			return (ExecutionInfo3)base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00019119 File Offset: 0x00017319
		public void LoadReport3Async(string Report, string HistoryID)
		{
			this.LoadReport3Async(Report, HistoryID, null);
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00019124 File Offset: 0x00017324
		public void LoadReport3Async(string Report, string HistoryID, object userState)
		{
			if (this.LoadReport3OperationCompleted == null)
			{
				this.LoadReport3OperationCompleted = new SendOrPostCallback(this.OnLoadReport3OperationCompleted);
			}
			base.InvokeAsync("LoadReport3", new object[] { Report, HistoryID }, this.LoadReport3OperationCompleted, userState);
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00019160 File Offset: 0x00017360
		private void OnLoadReport3OperationCompleted(object arg)
		{
			if (this.LoadReport3Completed != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.LoadReport3Completed(this, new LoadReport3CompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x000191A5 File Offset: 0x000173A5
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExecutionHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/LoadReport2", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("executionInfo")]
		public ExecutionInfo2 LoadReport2(string Report, string HistoryID)
		{
			return (ExecutionInfo2)this.Invoke("LoadReport2", new object[] { Report, HistoryID })[0];
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x000191C7 File Offset: 0x000173C7
		public IAsyncResult BeginLoadReport2(string Report, string HistoryID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("LoadReport2", new object[] { Report, HistoryID }, callback, asyncState);
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x000191E5 File Offset: 0x000173E5
		public ExecutionInfo2 EndLoadReport2(IAsyncResult asyncResult)
		{
			return (ExecutionInfo2)base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x000191F5 File Offset: 0x000173F5
		public void LoadReport2Async(string Report, string HistoryID)
		{
			this.LoadReport2Async(Report, HistoryID, null);
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x00019200 File Offset: 0x00017400
		public void LoadReport2Async(string Report, string HistoryID, object userState)
		{
			if (this.LoadReport2OperationCompleted == null)
			{
				this.LoadReport2OperationCompleted = new SendOrPostCallback(this.OnLoadReport2OperationCompleted);
			}
			base.InvokeAsync("LoadReport2", new object[] { Report, HistoryID }, this.LoadReport2OperationCompleted, userState);
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0001923C File Offset: 0x0001743C
		private void OnLoadReport2OperationCompleted(object arg)
		{
			if (this.LoadReport2Completed != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.LoadReport2Completed(this, new LoadReport2CompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00019284 File Offset: 0x00017484
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExecutionHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/LoadReportDefinition", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("executionInfo")]
		public ExecutionInfo LoadReportDefinition([XmlElement(DataType = "base64Binary")] byte[] Definition, out Warning[] warnings)
		{
			object[] array = this.Invoke("LoadReportDefinition", new object[] { Definition });
			warnings = (Warning[])array[1];
			return (ExecutionInfo)array[0];
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x000192B9 File Offset: 0x000174B9
		public IAsyncResult BeginLoadReportDefinition(byte[] Definition, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("LoadReportDefinition", new object[] { Definition }, callback, asyncState);
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x000192D4 File Offset: 0x000174D4
		public ExecutionInfo EndLoadReportDefinition(IAsyncResult asyncResult, out Warning[] warnings)
		{
			object[] array = base.EndInvoke(asyncResult);
			warnings = (Warning[])array[1];
			return (ExecutionInfo)array[0];
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x000192FB File Offset: 0x000174FB
		public void LoadReportDefinitionAsync(byte[] Definition)
		{
			this.LoadReportDefinitionAsync(Definition, null);
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00019305 File Offset: 0x00017505
		public void LoadReportDefinitionAsync(byte[] Definition, object userState)
		{
			if (this.LoadReportDefinitionOperationCompleted == null)
			{
				this.LoadReportDefinitionOperationCompleted = new SendOrPostCallback(this.OnLoadReportDefinitionOperationCompleted);
			}
			base.InvokeAsync("LoadReportDefinition", new object[] { Definition }, this.LoadReportDefinitionOperationCompleted, userState);
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00019340 File Offset: 0x00017540
		private void OnLoadReportDefinitionOperationCompleted(object arg)
		{
			if (this.LoadReportDefinitionCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.LoadReportDefinitionCompleted(this, new LoadReportDefinitionCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x00019388 File Offset: 0x00017588
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExecutionHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/LoadReportDefinition2", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("executionInfo")]
		public ExecutionInfo2 LoadReportDefinition2([XmlElement(DataType = "base64Binary")] byte[] Definition, out Warning[] warnings)
		{
			object[] array = this.Invoke("LoadReportDefinition2", new object[] { Definition });
			warnings = (Warning[])array[1];
			return (ExecutionInfo2)array[0];
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x000193BD File Offset: 0x000175BD
		public IAsyncResult BeginLoadReportDefinition2(byte[] Definition, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("LoadReportDefinition2", new object[] { Definition }, callback, asyncState);
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x000193D8 File Offset: 0x000175D8
		public ExecutionInfo2 EndLoadReportDefinition2(IAsyncResult asyncResult, out Warning[] warnings)
		{
			object[] array = base.EndInvoke(asyncResult);
			warnings = (Warning[])array[1];
			return (ExecutionInfo2)array[0];
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x000193FF File Offset: 0x000175FF
		public void LoadReportDefinition2Async(byte[] Definition)
		{
			this.LoadReportDefinition2Async(Definition, null);
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00019409 File Offset: 0x00017609
		public void LoadReportDefinition2Async(byte[] Definition, object userState)
		{
			if (this.LoadReportDefinition2OperationCompleted == null)
			{
				this.LoadReportDefinition2OperationCompleted = new SendOrPostCallback(this.OnLoadReportDefinition2OperationCompleted);
			}
			base.InvokeAsync("LoadReportDefinition2", new object[] { Definition }, this.LoadReportDefinition2OperationCompleted, userState);
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00019444 File Offset: 0x00017644
		private void OnLoadReportDefinition2OperationCompleted(object arg)
		{
			if (this.LoadReportDefinition2Completed != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.LoadReportDefinition2Completed(this, new LoadReportDefinition2CompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x0001948C File Offset: 0x0001768C
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExecutionHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/LoadReportDefinition3", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("executionInfo")]
		public ExecutionInfo3 LoadReportDefinition3([XmlElement(DataType = "base64Binary")] byte[] Definition, out Warning[] warnings)
		{
			object[] array = this.Invoke("LoadReportDefinition3", new object[] { Definition });
			warnings = (Warning[])array[1];
			return (ExecutionInfo3)array[0];
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x000194C1 File Offset: 0x000176C1
		public IAsyncResult BeginLoadReportDefinition3(byte[] Definition, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("LoadReportDefinition3", new object[] { Definition }, callback, asyncState);
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x000194DC File Offset: 0x000176DC
		public ExecutionInfo3 EndLoadReportDefinition3(IAsyncResult asyncResult, out Warning[] warnings)
		{
			object[] array = base.EndInvoke(asyncResult);
			warnings = (Warning[])array[1];
			return (ExecutionInfo3)array[0];
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00019503 File Offset: 0x00017703
		public void LoadReportDefinition3Async(byte[] Definition)
		{
			this.LoadReportDefinition3Async(Definition, null);
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x0001950D File Offset: 0x0001770D
		public void LoadReportDefinition3Async(byte[] Definition, object userState)
		{
			if (this.LoadReportDefinition3OperationCompleted == null)
			{
				this.LoadReportDefinition3OperationCompleted = new SendOrPostCallback(this.OnLoadReportDefinition3OperationCompleted);
			}
			base.InvokeAsync("LoadReportDefinition3", new object[] { Definition }, this.LoadReportDefinition3OperationCompleted, userState);
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00019548 File Offset: 0x00017748
		private void OnLoadReportDefinition3OperationCompleted(object arg)
		{
			if (this.LoadReportDefinition3Completed != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.LoadReportDefinition3Completed(this, new LoadReportDefinition3CompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0001958D File Offset: 0x0001778D
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExecutionHeaderValue")]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/SetExecutionCredentials", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("executionInfo")]
		public ExecutionInfo SetExecutionCredentials(DataSourceCredentials[] Credentials)
		{
			return (ExecutionInfo)this.Invoke("SetExecutionCredentials", new object[] { Credentials })[0];
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x000195AB File Offset: 0x000177AB
		public IAsyncResult BeginSetExecutionCredentials(DataSourceCredentials[] Credentials, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetExecutionCredentials", new object[] { Credentials }, callback, asyncState);
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0001902D File Offset: 0x0001722D
		public ExecutionInfo EndSetExecutionCredentials(IAsyncResult asyncResult)
		{
			return (ExecutionInfo)base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x000195C4 File Offset: 0x000177C4
		public void SetExecutionCredentialsAsync(DataSourceCredentials[] Credentials)
		{
			this.SetExecutionCredentialsAsync(Credentials, null);
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x000195CE File Offset: 0x000177CE
		public void SetExecutionCredentialsAsync(DataSourceCredentials[] Credentials, object userState)
		{
			if (this.SetExecutionCredentialsOperationCompleted == null)
			{
				this.SetExecutionCredentialsOperationCompleted = new SendOrPostCallback(this.OnSetExecutionCredentialsOperationCompleted);
			}
			base.InvokeAsync("SetExecutionCredentials", new object[] { Credentials }, this.SetExecutionCredentialsOperationCompleted, userState);
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x00019608 File Offset: 0x00017808
		private void OnSetExecutionCredentialsOperationCompleted(object arg)
		{
			if (this.SetExecutionCredentialsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetExecutionCredentialsCompleted(this, new SetExecutionCredentialsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0001964D File Offset: 0x0001784D
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExecutionHeaderValue")]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/SetExecutionCredentials2", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("executionInfo")]
		public ExecutionInfo2 SetExecutionCredentials2(DataSourceCredentials[] Credentials)
		{
			return (ExecutionInfo2)this.Invoke("SetExecutionCredentials2", new object[] { Credentials })[0];
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0001966B File Offset: 0x0001786B
		public IAsyncResult BeginSetExecutionCredentials2(DataSourceCredentials[] Credentials, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetExecutionCredentials2", new object[] { Credentials }, callback, asyncState);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x000191E5 File Offset: 0x000173E5
		public ExecutionInfo2 EndSetExecutionCredentials2(IAsyncResult asyncResult)
		{
			return (ExecutionInfo2)base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00019684 File Offset: 0x00017884
		public void SetExecutionCredentials2Async(DataSourceCredentials[] Credentials)
		{
			this.SetExecutionCredentials2Async(Credentials, null);
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0001968E File Offset: 0x0001788E
		public void SetExecutionCredentials2Async(DataSourceCredentials[] Credentials, object userState)
		{
			if (this.SetExecutionCredentials2OperationCompleted == null)
			{
				this.SetExecutionCredentials2OperationCompleted = new SendOrPostCallback(this.OnSetExecutionCredentials2OperationCompleted);
			}
			base.InvokeAsync("SetExecutionCredentials2", new object[] { Credentials }, this.SetExecutionCredentials2OperationCompleted, userState);
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x000196C8 File Offset: 0x000178C8
		private void OnSetExecutionCredentials2OperationCompleted(object arg)
		{
			if (this.SetExecutionCredentials2Completed != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetExecutionCredentials2Completed(this, new SetExecutionCredentials2CompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0001970D File Offset: 0x0001790D
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExecutionHeaderValue")]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/SetExecutionCredentials3", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("executionInfo")]
		public ExecutionInfo3 SetExecutionCredentials3(DataSourceCredentials[] Credentials)
		{
			return (ExecutionInfo3)this.Invoke("SetExecutionCredentials3", new object[] { Credentials })[0];
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0001972B File Offset: 0x0001792B
		public IAsyncResult BeginSetExecutionCredentials3(DataSourceCredentials[] Credentials, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetExecutionCredentials3", new object[] { Credentials }, callback, asyncState);
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00019109 File Offset: 0x00017309
		public ExecutionInfo3 EndSetExecutionCredentials3(IAsyncResult asyncResult)
		{
			return (ExecutionInfo3)base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00019744 File Offset: 0x00017944
		public void SetExecutionCredentials3Async(DataSourceCredentials[] Credentials)
		{
			this.SetExecutionCredentials3Async(Credentials, null);
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0001974E File Offset: 0x0001794E
		public void SetExecutionCredentials3Async(DataSourceCredentials[] Credentials, object userState)
		{
			if (this.SetExecutionCredentials3OperationCompleted == null)
			{
				this.SetExecutionCredentials3OperationCompleted = new SendOrPostCallback(this.OnSetExecutionCredentials3OperationCompleted);
			}
			base.InvokeAsync("SetExecutionCredentials3", new object[] { Credentials }, this.SetExecutionCredentials3OperationCompleted, userState);
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00019788 File Offset: 0x00017988
		private void OnSetExecutionCredentials3OperationCompleted(object arg)
		{
			if (this.SetExecutionCredentials3Completed != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetExecutionCredentials3Completed(this, new SetExecutionCredentials3CompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x000197CD File Offset: 0x000179CD
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExecutionHeaderValue")]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/SetExecutionParameters", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("executionInfo")]
		public ExecutionInfo SetExecutionParameters(ParameterValue[] Parameters, string ParameterLanguage)
		{
			return (ExecutionInfo)this.Invoke("SetExecutionParameters", new object[] { Parameters, ParameterLanguage })[0];
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x000197EF File Offset: 0x000179EF
		public IAsyncResult BeginSetExecutionParameters(ParameterValue[] Parameters, string ParameterLanguage, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetExecutionParameters", new object[] { Parameters, ParameterLanguage }, callback, asyncState);
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x0001902D File Offset: 0x0001722D
		public ExecutionInfo EndSetExecutionParameters(IAsyncResult asyncResult)
		{
			return (ExecutionInfo)base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0001980D File Offset: 0x00017A0D
		public void SetExecutionParametersAsync(ParameterValue[] Parameters, string ParameterLanguage)
		{
			this.SetExecutionParametersAsync(Parameters, ParameterLanguage, null);
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00019818 File Offset: 0x00017A18
		public void SetExecutionParametersAsync(ParameterValue[] Parameters, string ParameterLanguage, object userState)
		{
			if (this.SetExecutionParametersOperationCompleted == null)
			{
				this.SetExecutionParametersOperationCompleted = new SendOrPostCallback(this.OnSetExecutionParametersOperationCompleted);
			}
			base.InvokeAsync("SetExecutionParameters", new object[] { Parameters, ParameterLanguage }, this.SetExecutionParametersOperationCompleted, userState);
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00019854 File Offset: 0x00017A54
		private void OnSetExecutionParametersOperationCompleted(object arg)
		{
			if (this.SetExecutionParametersCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetExecutionParametersCompleted(this, new SetExecutionParametersCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00019899 File Offset: 0x00017A99
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExecutionHeaderValue")]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/SetExecutionParameters2", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("executionInfo")]
		public ExecutionInfo2 SetExecutionParameters2(ParameterValue[] Parameters, string ParameterLanguage)
		{
			return (ExecutionInfo2)this.Invoke("SetExecutionParameters2", new object[] { Parameters, ParameterLanguage })[0];
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x000198BB File Offset: 0x00017ABB
		public IAsyncResult BeginSetExecutionParameters2(ParameterValue[] Parameters, string ParameterLanguage, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetExecutionParameters2", new object[] { Parameters, ParameterLanguage }, callback, asyncState);
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x000191E5 File Offset: 0x000173E5
		public ExecutionInfo2 EndSetExecutionParameters2(IAsyncResult asyncResult)
		{
			return (ExecutionInfo2)base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x000198D9 File Offset: 0x00017AD9
		public void SetExecutionParameters2Async(ParameterValue[] Parameters, string ParameterLanguage)
		{
			this.SetExecutionParameters2Async(Parameters, ParameterLanguage, null);
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x000198E4 File Offset: 0x00017AE4
		public void SetExecutionParameters2Async(ParameterValue[] Parameters, string ParameterLanguage, object userState)
		{
			if (this.SetExecutionParameters2OperationCompleted == null)
			{
				this.SetExecutionParameters2OperationCompleted = new SendOrPostCallback(this.OnSetExecutionParameters2OperationCompleted);
			}
			base.InvokeAsync("SetExecutionParameters2", new object[] { Parameters, ParameterLanguage }, this.SetExecutionParameters2OperationCompleted, userState);
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00019920 File Offset: 0x00017B20
		private void OnSetExecutionParameters2OperationCompleted(object arg)
		{
			if (this.SetExecutionParameters2Completed != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetExecutionParameters2Completed(this, new SetExecutionParameters2CompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00019965 File Offset: 0x00017B65
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExecutionHeaderValue")]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/SetExecutionParameters3", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("executionInfo")]
		public ExecutionInfo3 SetExecutionParameters3(ParameterValue[] Parameters, string ParameterLanguage)
		{
			return (ExecutionInfo3)this.Invoke("SetExecutionParameters3", new object[] { Parameters, ParameterLanguage })[0];
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00019987 File Offset: 0x00017B87
		public IAsyncResult BeginSetExecutionParameters3(ParameterValue[] Parameters, string ParameterLanguage, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetExecutionParameters3", new object[] { Parameters, ParameterLanguage }, callback, asyncState);
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x00019109 File Offset: 0x00017309
		public ExecutionInfo3 EndSetExecutionParameters3(IAsyncResult asyncResult)
		{
			return (ExecutionInfo3)base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x000199A5 File Offset: 0x00017BA5
		public void SetExecutionParameters3Async(ParameterValue[] Parameters, string ParameterLanguage)
		{
			this.SetExecutionParameters3Async(Parameters, ParameterLanguage, null);
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x000199B0 File Offset: 0x00017BB0
		public void SetExecutionParameters3Async(ParameterValue[] Parameters, string ParameterLanguage, object userState)
		{
			if (this.SetExecutionParameters3OperationCompleted == null)
			{
				this.SetExecutionParameters3OperationCompleted = new SendOrPostCallback(this.OnSetExecutionParameters3OperationCompleted);
			}
			base.InvokeAsync("SetExecutionParameters3", new object[] { Parameters, ParameterLanguage }, this.SetExecutionParameters3OperationCompleted, userState);
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x000199EC File Offset: 0x00017BEC
		private void OnSetExecutionParameters3OperationCompleted(object arg)
		{
			if (this.SetExecutionParameters3Completed != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetExecutionParameters3Completed(this, new SetExecutionParameters3CompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00019A31 File Offset: 0x00017C31
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExecutionHeaderValue")]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/ResetExecution", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("executionInfo")]
		public ExecutionInfo ResetExecution()
		{
			return (ExecutionInfo)this.Invoke("ResetExecution", new object[0])[0];
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x00019A4B File Offset: 0x00017C4B
		public IAsyncResult BeginResetExecution(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ResetExecution", new object[0], callback, asyncState);
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x0001902D File Offset: 0x0001722D
		public ExecutionInfo EndResetExecution(IAsyncResult asyncResult)
		{
			return (ExecutionInfo)base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00019A60 File Offset: 0x00017C60
		public void ResetExecutionAsync()
		{
			this.ResetExecutionAsync(null);
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00019A69 File Offset: 0x00017C69
		public void ResetExecutionAsync(object userState)
		{
			if (this.ResetExecutionOperationCompleted == null)
			{
				this.ResetExecutionOperationCompleted = new SendOrPostCallback(this.OnResetExecutionOperationCompleted);
			}
			base.InvokeAsync("ResetExecution", new object[0], this.ResetExecutionOperationCompleted, userState);
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00019AA0 File Offset: 0x00017CA0
		private void OnResetExecutionOperationCompleted(object arg)
		{
			if (this.ResetExecutionCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ResetExecutionCompleted(this, new ResetExecutionCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x00019AE5 File Offset: 0x00017CE5
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExecutionHeaderValue")]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/ResetExecution2", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("executionInfo")]
		public ExecutionInfo2 ResetExecution2()
		{
			return (ExecutionInfo2)this.Invoke("ResetExecution2", new object[0])[0];
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x00019AFF File Offset: 0x00017CFF
		public IAsyncResult BeginResetExecution2(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ResetExecution2", new object[0], callback, asyncState);
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x000191E5 File Offset: 0x000173E5
		public ExecutionInfo2 EndResetExecution2(IAsyncResult asyncResult)
		{
			return (ExecutionInfo2)base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00019B14 File Offset: 0x00017D14
		public void ResetExecution2Async()
		{
			this.ResetExecution2Async(null);
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x00019B1D File Offset: 0x00017D1D
		public void ResetExecution2Async(object userState)
		{
			if (this.ResetExecution2OperationCompleted == null)
			{
				this.ResetExecution2OperationCompleted = new SendOrPostCallback(this.OnResetExecution2OperationCompleted);
			}
			base.InvokeAsync("ResetExecution2", new object[0], this.ResetExecution2OperationCompleted, userState);
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x00019B54 File Offset: 0x00017D54
		private void OnResetExecution2OperationCompleted(object arg)
		{
			if (this.ResetExecution2Completed != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ResetExecution2Completed(this, new ResetExecution2CompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x00019B99 File Offset: 0x00017D99
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExecutionHeaderValue")]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/ResetExecution3", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("executionInfo")]
		public ExecutionInfo3 ResetExecution3()
		{
			return (ExecutionInfo3)this.Invoke("ResetExecution3", new object[0])[0];
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00019BB3 File Offset: 0x00017DB3
		public IAsyncResult BeginResetExecution3(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ResetExecution3", new object[0], callback, asyncState);
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x00019109 File Offset: 0x00017309
		public ExecutionInfo3 EndResetExecution3(IAsyncResult asyncResult)
		{
			return (ExecutionInfo3)base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x00019BC8 File Offset: 0x00017DC8
		public void ResetExecution3Async()
		{
			this.ResetExecution3Async(null);
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x00019BD1 File Offset: 0x00017DD1
		public void ResetExecution3Async(object userState)
		{
			if (this.ResetExecution3OperationCompleted == null)
			{
				this.ResetExecution3OperationCompleted = new SendOrPostCallback(this.OnResetExecution3OperationCompleted);
			}
			base.InvokeAsync("ResetExecution3", new object[0], this.ResetExecution3OperationCompleted, userState);
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00019C08 File Offset: 0x00017E08
		private void OnResetExecution3OperationCompleted(object arg)
		{
			if (this.ResetExecution3Completed != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ResetExecution3Completed(this, new ResetExecution3CompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x00019C50 File Offset: 0x00017E50
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExecutionHeaderValue")]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/Render", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", DataType = "base64Binary")]
		public byte[] Render(string Format, string DeviceInfo, out string Extension, out string MimeType, out string Encoding, out Warning[] Warnings, out string[] StreamIds)
		{
			object[] array = this.Invoke("Render", new object[] { Format, DeviceInfo });
			Extension = (string)array[1];
			MimeType = (string)array[2];
			Encoding = (string)array[3];
			Warnings = (Warning[])array[4];
			StreamIds = (string[])array[5];
			return (byte[])array[0];
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00019CB5 File Offset: 0x00017EB5
		public IAsyncResult BeginRender(string Format, string DeviceInfo, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("Render", new object[] { Format, DeviceInfo }, callback, asyncState);
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x00019CD4 File Offset: 0x00017ED4
		public byte[] EndRender(IAsyncResult asyncResult, out string Extension, out string MimeType, out string Encoding, out Warning[] Warnings, out string[] StreamIds)
		{
			object[] array = base.EndInvoke(asyncResult);
			Extension = (string)array[1];
			MimeType = (string)array[2];
			Encoding = (string)array[3];
			Warnings = (Warning[])array[4];
			StreamIds = (string[])array[5];
			return (byte[])array[0];
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x00019D26 File Offset: 0x00017F26
		public void RenderAsync(string Format, string DeviceInfo)
		{
			this.RenderAsync(Format, DeviceInfo, null);
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00019D31 File Offset: 0x00017F31
		public void RenderAsync(string Format, string DeviceInfo, object userState)
		{
			if (this.RenderOperationCompleted == null)
			{
				this.RenderOperationCompleted = new SendOrPostCallback(this.OnRenderOperationCompleted);
			}
			base.InvokeAsync("Render", new object[] { Format, DeviceInfo }, this.RenderOperationCompleted, userState);
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00019D70 File Offset: 0x00017F70
		private void OnRenderOperationCompleted(object arg)
		{
			if (this.RenderCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.RenderCompleted(this, new RenderCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00019DB8 File Offset: 0x00017FB8
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExecutionHeaderValue")]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/Render2", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", DataType = "base64Binary")]
		public byte[] Render2(string Format, string DeviceInfo, PageCountMode PaginationMode, out string Extension, out string MimeType, out string Encoding, out Warning[] Warnings, out string[] StreamIds)
		{
			object[] array = this.Invoke("Render2", new object[] { Format, DeviceInfo, PaginationMode });
			Extension = (string)array[1];
			MimeType = (string)array[2];
			Encoding = (string)array[3];
			Warnings = (Warning[])array[4];
			StreamIds = (string[])array[5];
			return (byte[])array[0];
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00019E27 File Offset: 0x00018027
		public IAsyncResult BeginRender2(string Format, string DeviceInfo, PageCountMode PaginationMode, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("Render2", new object[] { Format, DeviceInfo, PaginationMode }, callback, asyncState);
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x00019E50 File Offset: 0x00018050
		public byte[] EndRender2(IAsyncResult asyncResult, out string Extension, out string MimeType, out string Encoding, out Warning[] Warnings, out string[] StreamIds)
		{
			object[] array = base.EndInvoke(asyncResult);
			Extension = (string)array[1];
			MimeType = (string)array[2];
			Encoding = (string)array[3];
			Warnings = (Warning[])array[4];
			StreamIds = (string[])array[5];
			return (byte[])array[0];
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x00019EA2 File Offset: 0x000180A2
		public void Render2Async(string Format, string DeviceInfo, PageCountMode PaginationMode)
		{
			this.Render2Async(Format, DeviceInfo, PaginationMode, null);
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00019EB0 File Offset: 0x000180B0
		public void Render2Async(string Format, string DeviceInfo, PageCountMode PaginationMode, object userState)
		{
			if (this.Render2OperationCompleted == null)
			{
				this.Render2OperationCompleted = new SendOrPostCallback(this.OnRender2OperationCompleted);
			}
			base.InvokeAsync("Render2", new object[] { Format, DeviceInfo, PaginationMode }, this.Render2OperationCompleted, userState);
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x00019F04 File Offset: 0x00018104
		private void OnRender2OperationCompleted(object arg)
		{
			if (this.Render2Completed != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.Render2Completed(this, new Render2CompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00019F49 File Offset: 0x00018149
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExecutionHeaderValue")]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/DeliverReportItem", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void DeliverReportItem(string Format, string DeviceInfo, ExtensionSettings ExtensionSettings, string Description, string EventType, string MatchData)
		{
			this.Invoke("DeliverReportItem", new object[] { Format, DeviceInfo, ExtensionSettings, Description, EventType, MatchData });
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x00019F78 File Offset: 0x00018178
		public IAsyncResult BeginDeliverReportItem(string Format, string DeviceInfo, ExtensionSettings ExtensionSettings, string Description, string EventType, string MatchData, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("DeliverReportItem", new object[] { Format, DeviceInfo, ExtensionSettings, Description, EventType, MatchData }, callback, asyncState);
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x00019FAA File Offset: 0x000181AA
		public void EndDeliverReportItem(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x00019FB4 File Offset: 0x000181B4
		public void DeliverReportItemAsync(string Format, string DeviceInfo, string Report, ExtensionSettings ExtensionSettings, string Description, string EventType, string MatchData)
		{
			this.DeliverReportItemAsync(Format, DeviceInfo, ExtensionSettings, Description, EventType, MatchData, null);
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00019FC8 File Offset: 0x000181C8
		public void DeliverReportItemAsync(string Format, string DeviceInfo, ExtensionSettings ExtensionSettings, string Description, string EventType, string MatchData, object userState)
		{
			if (this.DeliverReportItemOperationCompleted == null)
			{
				this.DeliverReportItemOperationCompleted = new SendOrPostCallback(this.OnDeliverReportItemOperationCompleted);
			}
			base.InvokeAsync("DeliverReportItem", new object[] { Format, DeviceInfo, ExtensionSettings, Description, EventType, MatchData }, this.DeliverReportItemOperationCompleted, userState);
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x0001A024 File Offset: 0x00018224
		private void OnDeliverReportItemOperationCompleted(object arg)
		{
			if (this.DeliverReportItemCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.DeliverReportItemCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x0001A064 File Offset: 0x00018264
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExecutionHeaderValue")]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/RenderStream", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", DataType = "base64Binary")]
		public byte[] RenderStream(string Format, string StreamID, string DeviceInfo, out string Encoding, out string MimeType)
		{
			object[] array = this.Invoke("RenderStream", new object[] { Format, StreamID, DeviceInfo });
			Encoding = (string)array[1];
			MimeType = (string)array[2];
			return (byte[])array[0];
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x0001A0AD File Offset: 0x000182AD
		public IAsyncResult BeginRenderStream(string Format, string StreamID, string DeviceInfo, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("RenderStream", new object[] { Format, StreamID, DeviceInfo }, callback, asyncState);
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x0001A0D0 File Offset: 0x000182D0
		public byte[] EndRenderStream(IAsyncResult asyncResult, out string Encoding, out string MimeType)
		{
			object[] array = base.EndInvoke(asyncResult);
			Encoding = (string)array[1];
			MimeType = (string)array[2];
			return (byte[])array[0];
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x0001A101 File Offset: 0x00018301
		public void RenderStreamAsync(string Format, string StreamID, string DeviceInfo)
		{
			this.RenderStreamAsync(Format, StreamID, DeviceInfo, null);
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x0001A110 File Offset: 0x00018310
		public void RenderStreamAsync(string Format, string StreamID, string DeviceInfo, object userState)
		{
			if (this.RenderStreamOperationCompleted == null)
			{
				this.RenderStreamOperationCompleted = new SendOrPostCallback(this.OnRenderStreamOperationCompleted);
			}
			base.InvokeAsync("RenderStream", new object[] { Format, StreamID, DeviceInfo }, this.RenderStreamOperationCompleted, userState);
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x0001A15C File Offset: 0x0001835C
		private void OnRenderStreamOperationCompleted(object arg)
		{
			if (this.RenderStreamCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.RenderStreamCompleted(this, new RenderStreamCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x0001A1A1 File Offset: 0x000183A1
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExecutionHeaderValue")]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/GetExecutionInfo", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("executionInfo")]
		public ExecutionInfo GetExecutionInfo()
		{
			return (ExecutionInfo)this.Invoke("GetExecutionInfo", new object[0])[0];
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x0001A1BB File Offset: 0x000183BB
		public IAsyncResult BeginGetExecutionInfo(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetExecutionInfo", new object[0], callback, asyncState);
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x0001902D File Offset: 0x0001722D
		public ExecutionInfo EndGetExecutionInfo(IAsyncResult asyncResult)
		{
			return (ExecutionInfo)base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x0001A1D0 File Offset: 0x000183D0
		public void GetExecutionInfoAsync()
		{
			this.GetExecutionInfoAsync(null);
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0001A1D9 File Offset: 0x000183D9
		public void GetExecutionInfoAsync(object userState)
		{
			if (this.GetExecutionInfoOperationCompleted == null)
			{
				this.GetExecutionInfoOperationCompleted = new SendOrPostCallback(this.OnGetExecutionInfoOperationCompleted);
			}
			base.InvokeAsync("GetExecutionInfo", new object[0], this.GetExecutionInfoOperationCompleted, userState);
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0001A210 File Offset: 0x00018410
		private void OnGetExecutionInfoOperationCompleted(object arg)
		{
			if (this.GetExecutionInfoCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetExecutionInfoCompleted(this, new GetExecutionInfoCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0001A255 File Offset: 0x00018455
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExecutionHeaderValue")]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/GetExecutionInfo2", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("executionInfo")]
		public ExecutionInfo2 GetExecutionInfo2()
		{
			return (ExecutionInfo2)this.Invoke("GetExecutionInfo2", new object[0])[0];
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0001A26F File Offset: 0x0001846F
		public IAsyncResult BeginGetExecutionInfo2(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetExecutionInfo2", new object[0], callback, asyncState);
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x000191E5 File Offset: 0x000173E5
		public ExecutionInfo2 EndGetExecutionInfo2(IAsyncResult asyncResult)
		{
			return (ExecutionInfo2)base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0001A284 File Offset: 0x00018484
		public void GetExecutionInfo2Async()
		{
			this.GetExecutionInfo2Async(null);
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0001A28D File Offset: 0x0001848D
		public void GetExecutionInfo2Async(object userState)
		{
			if (this.GetExecutionInfo2OperationCompleted == null)
			{
				this.GetExecutionInfo2OperationCompleted = new SendOrPostCallback(this.OnGetExecutionInfo2OperationCompleted);
			}
			base.InvokeAsync("GetExecutionInfo2", new object[0], this.GetExecutionInfo2OperationCompleted, userState);
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0001A2C4 File Offset: 0x000184C4
		private void OnGetExecutionInfo2OperationCompleted(object arg)
		{
			if (this.GetExecutionInfo2Completed != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetExecutionInfo2Completed(this, new GetExecutionInfo2CompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0001A309 File Offset: 0x00018509
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExecutionHeaderValue")]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/GetExecutionInfo3", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("executionInfo")]
		public ExecutionInfo3 GetExecutionInfo3()
		{
			return (ExecutionInfo3)this.Invoke("GetExecutionInfo3", new object[0])[0];
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0001A323 File Offset: 0x00018523
		public IAsyncResult BeginGetExecutionInfo3(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetExecutionInfo3", new object[0], callback, asyncState);
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00019109 File Offset: 0x00017309
		public ExecutionInfo3 EndGetExecutionInfo3(IAsyncResult asyncResult)
		{
			return (ExecutionInfo3)base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0001A338 File Offset: 0x00018538
		public void GetExecutionInfo3Async()
		{
			this.GetExecutionInfo3Async(null);
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0001A341 File Offset: 0x00018541
		public void GetExecutionInfo3Async(object userState)
		{
			if (this.GetExecutionInfo3OperationCompleted == null)
			{
				this.GetExecutionInfo3OperationCompleted = new SendOrPostCallback(this.OnGetExecutionInfo3OperationCompleted);
			}
			base.InvokeAsync("GetExecutionInfo3", new object[0], this.GetExecutionInfo3OperationCompleted, userState);
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x0001A378 File Offset: 0x00018578
		private void OnGetExecutionInfo3OperationCompleted(object arg)
		{
			if (this.GetExecutionInfo3Completed != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetExecutionInfo3Completed(this, new GetExecutionInfo3CompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x0001A3BD File Offset: 0x000185BD
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExecutionHeaderValue")]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/GetDocumentMap", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("result")]
		public DocumentMapNode GetDocumentMap()
		{
			return (DocumentMapNode)this.Invoke("GetDocumentMap", new object[0])[0];
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x0001A3D7 File Offset: 0x000185D7
		public IAsyncResult BeginGetDocumentMap(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetDocumentMap", new object[0], callback, asyncState);
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0001A3EC File Offset: 0x000185EC
		public DocumentMapNode EndGetDocumentMap(IAsyncResult asyncResult)
		{
			return (DocumentMapNode)base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0001A3FC File Offset: 0x000185FC
		public void GetDocumentMapAsync()
		{
			this.GetDocumentMapAsync(null);
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x0001A405 File Offset: 0x00018605
		public void GetDocumentMapAsync(object userState)
		{
			if (this.GetDocumentMapOperationCompleted == null)
			{
				this.GetDocumentMapOperationCompleted = new SendOrPostCallback(this.OnGetDocumentMapOperationCompleted);
			}
			base.InvokeAsync("GetDocumentMap", new object[0], this.GetDocumentMapOperationCompleted, userState);
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x0001A43C File Offset: 0x0001863C
		private void OnGetDocumentMapOperationCompleted(object arg)
		{
			if (this.GetDocumentMapCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetDocumentMapCompleted(this, new GetDocumentMapCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0001A481 File Offset: 0x00018681
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExecutionHeaderValue", Direction = SoapHeaderDirection.InOut)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/LoadDrillthroughTarget", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("ExecutionInfo")]
		public ExecutionInfo LoadDrillthroughTarget(string DrillthroughID)
		{
			return (ExecutionInfo)this.Invoke("LoadDrillthroughTarget", new object[] { DrillthroughID })[0];
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x0001A49F File Offset: 0x0001869F
		public IAsyncResult BeginLoadDrillthroughTarget(string DrillthroughID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("LoadDrillthroughTarget", new object[] { DrillthroughID }, callback, asyncState);
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0001902D File Offset: 0x0001722D
		public ExecutionInfo EndLoadDrillthroughTarget(IAsyncResult asyncResult)
		{
			return (ExecutionInfo)base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0001A4B8 File Offset: 0x000186B8
		public void LoadDrillthroughTargetAsync(string DrillthroughID)
		{
			this.LoadDrillthroughTargetAsync(DrillthroughID, null);
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0001A4C2 File Offset: 0x000186C2
		public void LoadDrillthroughTargetAsync(string DrillthroughID, object userState)
		{
			if (this.LoadDrillthroughTargetOperationCompleted == null)
			{
				this.LoadDrillthroughTargetOperationCompleted = new SendOrPostCallback(this.OnLoadDrillthroughTargetOperationCompleted);
			}
			base.InvokeAsync("LoadDrillthroughTarget", new object[] { DrillthroughID }, this.LoadDrillthroughTargetOperationCompleted, userState);
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0001A4FC File Offset: 0x000186FC
		private void OnLoadDrillthroughTargetOperationCompleted(object arg)
		{
			if (this.LoadDrillthroughTargetCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.LoadDrillthroughTargetCompleted(this, new LoadDrillthroughTargetCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x0001A541 File Offset: 0x00018741
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExecutionHeaderValue", Direction = SoapHeaderDirection.InOut)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/LoadDrillthroughTarget2", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("ExecutionInfo")]
		public ExecutionInfo2 LoadDrillthroughTarget2(string DrillthroughID)
		{
			return (ExecutionInfo2)this.Invoke("LoadDrillthroughTarget2", new object[] { DrillthroughID })[0];
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0001A55F File Offset: 0x0001875F
		public IAsyncResult BeginLoadDrillthroughTarget2(string DrillthroughID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("LoadDrillthroughTarget2", new object[] { DrillthroughID }, callback, asyncState);
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x000191E5 File Offset: 0x000173E5
		public ExecutionInfo2 EndLoadDrillthroughTarget2(IAsyncResult asyncResult)
		{
			return (ExecutionInfo2)base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0001A578 File Offset: 0x00018778
		public void LoadDrillthroughTarget2Async(string DrillthroughID)
		{
			this.LoadDrillthroughTarget2Async(DrillthroughID, null);
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0001A582 File Offset: 0x00018782
		public void LoadDrillthroughTarget2Async(string DrillthroughID, object userState)
		{
			if (this.LoadDrillthroughTarget2OperationCompleted == null)
			{
				this.LoadDrillthroughTarget2OperationCompleted = new SendOrPostCallback(this.OnLoadDrillthroughTarget2OperationCompleted);
			}
			base.InvokeAsync("LoadDrillthroughTarget2", new object[] { DrillthroughID }, this.LoadDrillthroughTarget2OperationCompleted, userState);
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0001A5BC File Offset: 0x000187BC
		private void OnLoadDrillthroughTarget2OperationCompleted(object arg)
		{
			if (this.LoadDrillthroughTarget2Completed != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.LoadDrillthroughTarget2Completed(this, new LoadDrillthroughTarget2CompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0001A601 File Offset: 0x00018801
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExecutionHeaderValue", Direction = SoapHeaderDirection.InOut)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/LoadDrillthroughTarget3", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("ExecutionInfo")]
		public ExecutionInfo3 LoadDrillthroughTarget3(string DrillthroughID)
		{
			return (ExecutionInfo3)this.Invoke("LoadDrillthroughTarget3", new object[] { DrillthroughID })[0];
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0001A61F File Offset: 0x0001881F
		public IAsyncResult BeginLoadDrillthroughTarget3(string DrillthroughID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("LoadDrillthroughTarget3", new object[] { DrillthroughID }, callback, asyncState);
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00019109 File Offset: 0x00017309
		public ExecutionInfo3 EndLoadDrillthroughTarget3(IAsyncResult asyncResult)
		{
			return (ExecutionInfo3)base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0001A638 File Offset: 0x00018838
		public void LoadDrillthroughTarget3Async(string DrillthroughID)
		{
			this.LoadDrillthroughTarget3Async(DrillthroughID, null);
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0001A642 File Offset: 0x00018842
		public void LoadDrillthroughTarget3Async(string DrillthroughID, object userState)
		{
			if (this.LoadDrillthroughTarget3OperationCompleted == null)
			{
				this.LoadDrillthroughTarget3OperationCompleted = new SendOrPostCallback(this.OnLoadDrillthroughTarget3OperationCompleted);
			}
			base.InvokeAsync("LoadDrillthroughTarget3", new object[] { DrillthroughID }, this.LoadDrillthroughTarget3OperationCompleted, userState);
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0001A67C File Offset: 0x0001887C
		private void OnLoadDrillthroughTarget3OperationCompleted(object arg)
		{
			if (this.LoadDrillthroughTarget3Completed != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.LoadDrillthroughTarget3Completed(this, new LoadDrillthroughTarget3CompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0001A6C1 File Offset: 0x000188C1
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExecutionHeaderValue")]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/ToggleItem", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Found")]
		public bool ToggleItem(string ToggleID)
		{
			return (bool)this.Invoke("ToggleItem", new object[] { ToggleID })[0];
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0001A6DF File Offset: 0x000188DF
		public IAsyncResult BeginToggleItem(string ToggleID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ToggleItem", new object[] { ToggleID }, callback, asyncState);
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0001A6F8 File Offset: 0x000188F8
		public bool EndToggleItem(IAsyncResult asyncResult)
		{
			return (bool)base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0001A708 File Offset: 0x00018908
		public void ToggleItemAsync(string ToggleID)
		{
			this.ToggleItemAsync(ToggleID, null);
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0001A712 File Offset: 0x00018912
		public void ToggleItemAsync(string ToggleID, object userState)
		{
			if (this.ToggleItemOperationCompleted == null)
			{
				this.ToggleItemOperationCompleted = new SendOrPostCallback(this.OnToggleItemOperationCompleted);
			}
			base.InvokeAsync("ToggleItem", new object[] { ToggleID }, this.ToggleItemOperationCompleted, userState);
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0001A74C File Offset: 0x0001894C
		private void OnToggleItemOperationCompleted(object arg)
		{
			if (this.ToggleItemCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ToggleItemCompleted(this, new ToggleItemCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0001A791 File Offset: 0x00018991
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExecutionHeaderValue")]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/NavigateDocumentMap", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("PageNumber")]
		public int NavigateDocumentMap(string DocMapID)
		{
			return (int)this.Invoke("NavigateDocumentMap", new object[] { DocMapID })[0];
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0001A7AF File Offset: 0x000189AF
		public IAsyncResult BeginNavigateDocumentMap(string DocMapID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("NavigateDocumentMap", new object[] { DocMapID }, callback, asyncState);
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0001A7C8 File Offset: 0x000189C8
		public int EndNavigateDocumentMap(IAsyncResult asyncResult)
		{
			return (int)base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0001A7D8 File Offset: 0x000189D8
		public void NavigateDocumentMapAsync(string DocMapID)
		{
			this.NavigateDocumentMapAsync(DocMapID, null);
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0001A7E2 File Offset: 0x000189E2
		public void NavigateDocumentMapAsync(string DocMapID, object userState)
		{
			if (this.NavigateDocumentMapOperationCompleted == null)
			{
				this.NavigateDocumentMapOperationCompleted = new SendOrPostCallback(this.OnNavigateDocumentMapOperationCompleted);
			}
			base.InvokeAsync("NavigateDocumentMap", new object[] { DocMapID }, this.NavigateDocumentMapOperationCompleted, userState);
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0001A81C File Offset: 0x00018A1C
		private void OnNavigateDocumentMapOperationCompleted(object arg)
		{
			if (this.NavigateDocumentMapCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.NavigateDocumentMapCompleted(this, new NavigateDocumentMapCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0001A864 File Offset: 0x00018A64
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExecutionHeaderValue")]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/NavigateBookmark", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("PageNumber")]
		public int NavigateBookmark(string BookmarkID, out string UniqueName)
		{
			object[] array = this.Invoke("NavigateBookmark", new object[] { BookmarkID });
			UniqueName = (string)array[1];
			return (int)array[0];
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0001A899 File Offset: 0x00018A99
		public IAsyncResult BeginNavigateBookmark(string BookmarkID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("NavigateBookmark", new object[] { BookmarkID }, callback, asyncState);
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0001A8B4 File Offset: 0x00018AB4
		public int EndNavigateBookmark(IAsyncResult asyncResult, out string UniqueName)
		{
			object[] array = base.EndInvoke(asyncResult);
			UniqueName = (string)array[1];
			return (int)array[0];
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0001A8DB File Offset: 0x00018ADB
		public void NavigateBookmarkAsync(string BookmarkID)
		{
			this.NavigateBookmarkAsync(BookmarkID, null);
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0001A8E5 File Offset: 0x00018AE5
		public void NavigateBookmarkAsync(string BookmarkID, object userState)
		{
			if (this.NavigateBookmarkOperationCompleted == null)
			{
				this.NavigateBookmarkOperationCompleted = new SendOrPostCallback(this.OnNavigateBookmarkOperationCompleted);
			}
			base.InvokeAsync("NavigateBookmark", new object[] { BookmarkID }, this.NavigateBookmarkOperationCompleted, userState);
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0001A920 File Offset: 0x00018B20
		private void OnNavigateBookmarkOperationCompleted(object arg)
		{
			if (this.NavigateBookmarkCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.NavigateBookmarkCompleted(this, new NavigateBookmarkCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0001A965 File Offset: 0x00018B65
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExecutionHeaderValue")]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/FindString", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("PageNumber")]
		public int FindString(int StartPage, int EndPage, string FindValue)
		{
			return (int)this.Invoke("FindString", new object[] { StartPage, EndPage, FindValue })[0];
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0001A995 File Offset: 0x00018B95
		public IAsyncResult BeginFindString(int StartPage, int EndPage, string FindValue, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("FindString", new object[] { StartPage, EndPage, FindValue }, callback, asyncState);
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0001A7C8 File Offset: 0x000189C8
		public int EndFindString(IAsyncResult asyncResult)
		{
			return (int)base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0001A9C2 File Offset: 0x00018BC2
		public void FindStringAsync(int StartPage, int EndPage, string FindValue)
		{
			this.FindStringAsync(StartPage, EndPage, FindValue, null);
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0001A9D0 File Offset: 0x00018BD0
		public void FindStringAsync(int StartPage, int EndPage, string FindValue, object userState)
		{
			if (this.FindStringOperationCompleted == null)
			{
				this.FindStringOperationCompleted = new SendOrPostCallback(this.OnFindStringOperationCompleted);
			}
			base.InvokeAsync("FindString", new object[] { StartPage, EndPage, FindValue }, this.FindStringOperationCompleted, userState);
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0001AA28 File Offset: 0x00018C28
		private void OnFindStringOperationCompleted(object arg)
		{
			if (this.FindStringCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.FindStringCompleted(this, new FindStringCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0001AA70 File Offset: 0x00018C70
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExecutionHeaderValue")]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/Sort", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("PageNumber")]
		public int Sort(string SortItem, SortDirectionEnum Direction, bool Clear, out string ReportItem, out int NumPages)
		{
			object[] array = this.Invoke("Sort", new object[] { SortItem, Direction, Clear });
			ReportItem = (string)array[1];
			NumPages = (int)array[2];
			return (int)array[0];
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0001AAC3 File Offset: 0x00018CC3
		public IAsyncResult BeginSort(string SortItem, SortDirectionEnum Direction, bool Clear, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("Sort", new object[] { SortItem, Direction, Clear }, callback, asyncState);
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0001AAF0 File Offset: 0x00018CF0
		public int EndSort(IAsyncResult asyncResult, out string ReportItem, out int NumPages)
		{
			object[] array = base.EndInvoke(asyncResult);
			ReportItem = (string)array[1];
			NumPages = (int)array[2];
			return (int)array[0];
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0001AB21 File Offset: 0x00018D21
		public void SortAsync(string SortItem, SortDirectionEnum Direction, bool Clear)
		{
			this.SortAsync(SortItem, Direction, Clear, null);
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0001AB30 File Offset: 0x00018D30
		public void SortAsync(string SortItem, SortDirectionEnum Direction, bool Clear, object userState)
		{
			if (this.SortOperationCompleted == null)
			{
				this.SortOperationCompleted = new SendOrPostCallback(this.OnSortOperationCompleted);
			}
			base.InvokeAsync("Sort", new object[] { SortItem, Direction, Clear }, this.SortOperationCompleted, userState);
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0001AB88 File Offset: 0x00018D88
		private void OnSortOperationCompleted(object arg)
		{
			if (this.SortCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SortCompleted(this, new SortCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0001ABD0 File Offset: 0x00018DD0
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExecutionHeaderValue")]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/Sort2", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("PageNumber")]
		public int Sort2(string SortItem, SortDirectionEnum Direction, bool Clear, PageCountMode PaginationMode, out string ReportItem, out ExecutionInfo2 ExecutionInfo)
		{
			object[] array = this.Invoke("Sort2", new object[] { SortItem, Direction, Clear, PaginationMode });
			ReportItem = (string)array[1];
			ExecutionInfo = (ExecutionInfo2)array[2];
			return (int)array[0];
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0001AC2D File Offset: 0x00018E2D
		public IAsyncResult BeginSort2(string SortItem, SortDirectionEnum Direction, bool Clear, PageCountMode PaginationMode, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("Sort2", new object[] { SortItem, Direction, Clear, PaginationMode }, callback, asyncState);
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0001AC64 File Offset: 0x00018E64
		public int EndSort2(IAsyncResult asyncResult, out string ReportItem, out ExecutionInfo2 ExecutionInfo)
		{
			object[] array = base.EndInvoke(asyncResult);
			ReportItem = (string)array[1];
			ExecutionInfo = (ExecutionInfo2)array[2];
			return (int)array[0];
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0001AC95 File Offset: 0x00018E95
		public void Sort2Async(string SortItem, SortDirectionEnum Direction, bool Clear, PageCountMode PaginationMode)
		{
			this.Sort2Async(SortItem, Direction, Clear, PaginationMode, null);
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0001ACA4 File Offset: 0x00018EA4
		public void Sort2Async(string SortItem, SortDirectionEnum Direction, bool Clear, PageCountMode PaginationMode, object userState)
		{
			if (this.Sort2OperationCompleted == null)
			{
				this.Sort2OperationCompleted = new SendOrPostCallback(this.OnSort2OperationCompleted);
			}
			base.InvokeAsync("Sort2", new object[] { SortItem, Direction, Clear, PaginationMode }, this.Sort2OperationCompleted, userState);
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0001AD04 File Offset: 0x00018F04
		private void OnSort2OperationCompleted(object arg)
		{
			if (this.Sort2Completed != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.Sort2Completed(this, new Sort2CompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0001AD4C File Offset: 0x00018F4C
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExecutionHeaderValue")]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/Sort3", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("PageNumber")]
		public int Sort3(string SortItem, SortDirectionEnum Direction, bool Clear, PageCountMode PaginationMode, out string ReportItem, out ExecutionInfo3 ExecutionInfo)
		{
			object[] array = this.Invoke("Sort3", new object[] { SortItem, Direction, Clear, PaginationMode });
			ReportItem = (string)array[1];
			ExecutionInfo = (ExecutionInfo3)array[2];
			return (int)array[0];
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x0001ADA9 File Offset: 0x00018FA9
		public IAsyncResult BeginSort3(string SortItem, SortDirectionEnum Direction, bool Clear, PageCountMode PaginationMode, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("Sort3", new object[] { SortItem, Direction, Clear, PaginationMode }, callback, asyncState);
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x0001ADE0 File Offset: 0x00018FE0
		public int EndSort3(IAsyncResult asyncResult, out string ReportItem, out ExecutionInfo3 ExecutionInfo)
		{
			object[] array = base.EndInvoke(asyncResult);
			ReportItem = (string)array[1];
			ExecutionInfo = (ExecutionInfo3)array[2];
			return (int)array[0];
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x0001AE11 File Offset: 0x00019011
		public void Sort3Async(string SortItem, SortDirectionEnum Direction, bool Clear, PageCountMode PaginationMode)
		{
			this.Sort3Async(SortItem, Direction, Clear, PaginationMode, null);
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x0001AE20 File Offset: 0x00019020
		public void Sort3Async(string SortItem, SortDirectionEnum Direction, bool Clear, PageCountMode PaginationMode, object userState)
		{
			if (this.Sort3OperationCompleted == null)
			{
				this.Sort3OperationCompleted = new SendOrPostCallback(this.OnSort3OperationCompleted);
			}
			base.InvokeAsync("Sort3", new object[] { SortItem, Direction, Clear, PaginationMode }, this.Sort3OperationCompleted, userState);
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x0001AE80 File Offset: 0x00019080
		private void OnSort3OperationCompleted(object arg)
		{
			if (this.Sort3Completed != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.Sort3Completed(this, new Sort3CompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0001AEC8 File Offset: 0x000190C8
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/GetRenderResource", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", DataType = "base64Binary")]
		public byte[] GetRenderResource(string Format, string DeviceInfo, out string MimeType)
		{
			object[] array = this.Invoke("GetRenderResource", new object[] { Format, DeviceInfo });
			MimeType = (string)array[1];
			return (byte[])array[0];
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0001AF01 File Offset: 0x00019101
		public IAsyncResult BeginGetRenderResource(string Format, string DeviceInfo, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetRenderResource", new object[] { Format, DeviceInfo }, callback, asyncState);
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0001AF20 File Offset: 0x00019120
		public byte[] EndGetRenderResource(IAsyncResult asyncResult, out string MimeType)
		{
			object[] array = base.EndInvoke(asyncResult);
			MimeType = (string)array[1];
			return (byte[])array[0];
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x0001AF47 File Offset: 0x00019147
		public void GetRenderResourceAsync(string Format, string DeviceInfo)
		{
			this.GetRenderResourceAsync(Format, DeviceInfo, null);
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x0001AF52 File Offset: 0x00019152
		public void GetRenderResourceAsync(string Format, string DeviceInfo, object userState)
		{
			if (this.GetRenderResourceOperationCompleted == null)
			{
				this.GetRenderResourceOperationCompleted = new SendOrPostCallback(this.OnGetRenderResourceOperationCompleted);
			}
			base.InvokeAsync("GetRenderResource", new object[] { Format, DeviceInfo }, this.GetRenderResourceOperationCompleted, userState);
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x0001AF90 File Offset: 0x00019190
		private void OnGetRenderResourceOperationCompleted(object arg)
		{
			if (this.GetRenderResourceCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetRenderResourceCompleted(this, new GetRenderResourceCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0001AFD5 File Offset: 0x000191D5
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/ListRenderingExtensions", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Extensions")]
		public Extension[] ListRenderingExtensions()
		{
			return (Extension[])this.Invoke("ListRenderingExtensions", new object[0])[0];
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x0001AFEF File Offset: 0x000191EF
		public IAsyncResult BeginListRenderingExtensions(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListRenderingExtensions", new object[0], callback, asyncState);
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0001B004 File Offset: 0x00019204
		public Extension[] EndListRenderingExtensions(IAsyncResult asyncResult)
		{
			return (Extension[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0001B014 File Offset: 0x00019214
		public void ListRenderingExtensionsAsync()
		{
			this.ListRenderingExtensionsAsync(null);
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0001B01D File Offset: 0x0001921D
		public void ListRenderingExtensionsAsync(object userState)
		{
			if (this.ListRenderingExtensionsOperationCompleted == null)
			{
				this.ListRenderingExtensionsOperationCompleted = new SendOrPostCallback(this.OnListRenderingExtensionsOperationCompleted);
			}
			base.InvokeAsync("ListRenderingExtensions", new object[0], this.ListRenderingExtensionsOperationCompleted, userState);
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x0001B054 File Offset: 0x00019254
		private void OnListRenderingExtensionsOperationCompleted(object arg)
		{
			if (this.ListRenderingExtensionsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListRenderingExtensionsCompleted(this, new ListRenderingExtensionsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0001B099 File Offset: 0x00019299
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/LogonUser", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void LogonUser(string userName, string password, string authority)
		{
			this.Invoke("LogonUser", new object[] { userName, password, authority });
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0001B0B9 File Offset: 0x000192B9
		public IAsyncResult BeginLogonUser(string userName, string password, string authority, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("LogonUser", new object[] { userName, password, authority }, callback, asyncState);
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x00019FAA File Offset: 0x000181AA
		public void EndLogonUser(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0001B0DC File Offset: 0x000192DC
		public void LogonUserAsync(string userName, string password, string authority)
		{
			this.LogonUserAsync(userName, password, authority, null);
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x0001B0E8 File Offset: 0x000192E8
		public void LogonUserAsync(string userName, string password, string authority, object userState)
		{
			if (this.LogonUserOperationCompleted == null)
			{
				this.LogonUserOperationCompleted = new SendOrPostCallback(this.OnLogonUserOperationCompleted);
			}
			base.InvokeAsync("LogonUser", new object[] { userName, password, authority }, this.LogonUserOperationCompleted, userState);
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x0001B134 File Offset: 0x00019334
		private void OnLogonUserOperationCompleted(object arg)
		{
			if (this.LogonUserCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.LogonUserCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0001B173 File Offset: 0x00019373
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/Logoff", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void Logoff()
		{
			this.Invoke("Logoff", new object[0]);
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x0001B187 File Offset: 0x00019387
		public IAsyncResult BeginLogoff(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("Logoff", new object[0], callback, asyncState);
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00019FAA File Offset: 0x000181AA
		public void EndLogoff(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x0001B19C File Offset: 0x0001939C
		public void LogoffAsync()
		{
			this.LogoffAsync(null);
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0001B1A5 File Offset: 0x000193A5
		public void LogoffAsync(object userState)
		{
			if (this.LogoffOperationCompleted == null)
			{
				this.LogoffOperationCompleted = new SendOrPostCallback(this.OnLogoffOperationCompleted);
			}
			base.InvokeAsync("Logoff", new object[0], this.LogoffOperationCompleted, userState);
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0001B1DC File Offset: 0x000193DC
		private void OnLogoffOperationCompleted(object arg)
		{
			if (this.LogoffCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.LogoffCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x0001B21B File Offset: 0x0001941B
		public new void CancelAsync(object userState)
		{
			base.CancelAsync(userState);
		}

		// Token: 0x04000108 RID: 264
		private TrustedUserHeader trustedUserHeaderValueField;

		// Token: 0x04000109 RID: 265
		private ServerInfoHeader serverInfoHeaderValueField;

		// Token: 0x0400010A RID: 266
		private SendOrPostCallback ListSecureMethodsOperationCompleted;

		// Token: 0x0400010B RID: 267
		private ExecutionHeader executionHeaderValueField;

		// Token: 0x0400010C RID: 268
		private SendOrPostCallback LoadReportOperationCompleted;

		// Token: 0x0400010D RID: 269
		private SendOrPostCallback LoadReport3OperationCompleted;

		// Token: 0x0400010E RID: 270
		private SendOrPostCallback LoadReport2OperationCompleted;

		// Token: 0x0400010F RID: 271
		private SendOrPostCallback LoadReportDefinitionOperationCompleted;

		// Token: 0x04000110 RID: 272
		private SendOrPostCallback LoadReportDefinition2OperationCompleted;

		// Token: 0x04000111 RID: 273
		private SendOrPostCallback LoadReportDefinition3OperationCompleted;

		// Token: 0x04000112 RID: 274
		private SendOrPostCallback SetExecutionCredentialsOperationCompleted;

		// Token: 0x04000113 RID: 275
		private SendOrPostCallback SetExecutionCredentials2OperationCompleted;

		// Token: 0x04000114 RID: 276
		private SendOrPostCallback SetExecutionCredentials3OperationCompleted;

		// Token: 0x04000115 RID: 277
		private SendOrPostCallback SetExecutionParametersOperationCompleted;

		// Token: 0x04000116 RID: 278
		private SendOrPostCallback SetExecutionParameters2OperationCompleted;

		// Token: 0x04000117 RID: 279
		private SendOrPostCallback SetExecutionParameters3OperationCompleted;

		// Token: 0x04000118 RID: 280
		private SendOrPostCallback ResetExecutionOperationCompleted;

		// Token: 0x04000119 RID: 281
		private SendOrPostCallback ResetExecution2OperationCompleted;

		// Token: 0x0400011A RID: 282
		private SendOrPostCallback ResetExecution3OperationCompleted;

		// Token: 0x0400011B RID: 283
		private SendOrPostCallback RenderOperationCompleted;

		// Token: 0x0400011C RID: 284
		private SendOrPostCallback Render2OperationCompleted;

		// Token: 0x0400011D RID: 285
		private SendOrPostCallback DeliverReportItemOperationCompleted;

		// Token: 0x0400011E RID: 286
		private SendOrPostCallback RenderStreamOperationCompleted;

		// Token: 0x0400011F RID: 287
		private SendOrPostCallback GetExecutionInfoOperationCompleted;

		// Token: 0x04000120 RID: 288
		private SendOrPostCallback GetExecutionInfo2OperationCompleted;

		// Token: 0x04000121 RID: 289
		private SendOrPostCallback GetExecutionInfo3OperationCompleted;

		// Token: 0x04000122 RID: 290
		private SendOrPostCallback GetDocumentMapOperationCompleted;

		// Token: 0x04000123 RID: 291
		private SendOrPostCallback LoadDrillthroughTargetOperationCompleted;

		// Token: 0x04000124 RID: 292
		private SendOrPostCallback LoadDrillthroughTarget2OperationCompleted;

		// Token: 0x04000125 RID: 293
		private SendOrPostCallback LoadDrillthroughTarget3OperationCompleted;

		// Token: 0x04000126 RID: 294
		private SendOrPostCallback ToggleItemOperationCompleted;

		// Token: 0x04000127 RID: 295
		private SendOrPostCallback NavigateDocumentMapOperationCompleted;

		// Token: 0x04000128 RID: 296
		private SendOrPostCallback NavigateBookmarkOperationCompleted;

		// Token: 0x04000129 RID: 297
		private SendOrPostCallback FindStringOperationCompleted;

		// Token: 0x0400012A RID: 298
		private SendOrPostCallback SortOperationCompleted;

		// Token: 0x0400012B RID: 299
		private SendOrPostCallback Sort2OperationCompleted;

		// Token: 0x0400012C RID: 300
		private SendOrPostCallback Sort3OperationCompleted;

		// Token: 0x0400012D RID: 301
		private SendOrPostCallback GetRenderResourceOperationCompleted;

		// Token: 0x0400012E RID: 302
		private SendOrPostCallback ListRenderingExtensionsOperationCompleted;

		// Token: 0x0400012F RID: 303
		private SendOrPostCallback LogonUserOperationCompleted;

		// Token: 0x04000130 RID: 304
		private SendOrPostCallback LogoffOperationCompleted;
	}
}
