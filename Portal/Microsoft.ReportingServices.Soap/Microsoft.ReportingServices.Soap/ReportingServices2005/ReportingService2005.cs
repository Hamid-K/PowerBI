using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x020001F8 RID: 504
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[WebServiceBinding(Name = "ReportingService2005Soap", Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[XmlInclude(typeof(DataSourceDefinitionOrReference))]
	[XmlInclude(typeof(ExpirationDefinition))]
	[XmlInclude(typeof(RecurrencePattern))]
	[XmlInclude(typeof(ScheduleDefinitionOrReference))]
	public class ReportingService2005 : SoapHttpClientProtocol
	{
		// Token: 0x06001035 RID: 4149 RVA: 0x00018847 File Offset: 0x00016A47
		public ReportingService2005()
		{
			base.Url = "http://localhost/ReportServer/ReportService2005.asmx";
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06001036 RID: 4150 RVA: 0x0001885A File Offset: 0x00016A5A
		// (set) Token: 0x06001037 RID: 4151 RVA: 0x00018862 File Offset: 0x00016A62
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

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06001038 RID: 4152 RVA: 0x0001886B File Offset: 0x00016A6B
		// (set) Token: 0x06001039 RID: 4153 RVA: 0x00018873 File Offset: 0x00016A73
		public BatchHeader BatchHeaderValue
		{
			get
			{
				return this.batchHeaderValueField;
			}
			set
			{
				this.batchHeaderValueField = value;
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x0600103A RID: 4154 RVA: 0x0001887C File Offset: 0x00016A7C
		// (set) Token: 0x0600103B RID: 4155 RVA: 0x00018884 File Offset: 0x00016A84
		public ItemNamespaceHeader ItemNamespaceHeaderValue
		{
			get
			{
				return this.itemNamespaceHeaderValueField;
			}
			set
			{
				this.itemNamespaceHeaderValueField = value;
			}
		}

		// Token: 0x140000DC RID: 220
		// (add) Token: 0x0600103C RID: 4156 RVA: 0x00018890 File Offset: 0x00016A90
		// (remove) Token: 0x0600103D RID: 4157 RVA: 0x000188C8 File Offset: 0x00016AC8
		public event GetSystemPoliciesCompletedEventHandler GetSystemPoliciesCompleted;

		// Token: 0x140000DD RID: 221
		// (add) Token: 0x0600103E RID: 4158 RVA: 0x00018900 File Offset: 0x00016B00
		// (remove) Token: 0x0600103F RID: 4159 RVA: 0x00018938 File Offset: 0x00016B38
		public event SetSystemPoliciesCompletedEventHandler SetSystemPoliciesCompleted;

		// Token: 0x140000DE RID: 222
		// (add) Token: 0x06001040 RID: 4160 RVA: 0x00018970 File Offset: 0x00016B70
		// (remove) Token: 0x06001041 RID: 4161 RVA: 0x000189A8 File Offset: 0x00016BA8
		public event GetPoliciesCompletedEventHandler GetPoliciesCompleted;

		// Token: 0x140000DF RID: 223
		// (add) Token: 0x06001042 RID: 4162 RVA: 0x000189E0 File Offset: 0x00016BE0
		// (remove) Token: 0x06001043 RID: 4163 RVA: 0x00018A18 File Offset: 0x00016C18
		public event SetPoliciesCompletedEventHandler SetPoliciesCompleted;

		// Token: 0x140000E0 RID: 224
		// (add) Token: 0x06001044 RID: 4164 RVA: 0x00018A50 File Offset: 0x00016C50
		// (remove) Token: 0x06001045 RID: 4165 RVA: 0x00018A88 File Offset: 0x00016C88
		public event InheritParentSecurityCompletedEventHandler InheritParentSecurityCompleted;

		// Token: 0x140000E1 RID: 225
		// (add) Token: 0x06001046 RID: 4166 RVA: 0x00018AC0 File Offset: 0x00016CC0
		// (remove) Token: 0x06001047 RID: 4167 RVA: 0x00018AF8 File Offset: 0x00016CF8
		public event GetSystemPermissionsCompletedEventHandler GetSystemPermissionsCompleted;

		// Token: 0x140000E2 RID: 226
		// (add) Token: 0x06001048 RID: 4168 RVA: 0x00018B30 File Offset: 0x00016D30
		// (remove) Token: 0x06001049 RID: 4169 RVA: 0x00018B68 File Offset: 0x00016D68
		public event GetPermissionsCompletedEventHandler GetPermissionsCompleted;

		// Token: 0x140000E3 RID: 227
		// (add) Token: 0x0600104A RID: 4170 RVA: 0x00018BA0 File Offset: 0x00016DA0
		// (remove) Token: 0x0600104B RID: 4171 RVA: 0x00018BD8 File Offset: 0x00016DD8
		public event LogonUserCompletedEventHandler LogonUserCompleted;

		// Token: 0x140000E4 RID: 228
		// (add) Token: 0x0600104C RID: 4172 RVA: 0x00018C10 File Offset: 0x00016E10
		// (remove) Token: 0x0600104D RID: 4173 RVA: 0x00018C48 File Offset: 0x00016E48
		public event LogoffCompletedEventHandler LogoffCompleted;

		// Token: 0x140000E5 RID: 229
		// (add) Token: 0x0600104E RID: 4174 RVA: 0x00018C80 File Offset: 0x00016E80
		// (remove) Token: 0x0600104F RID: 4175 RVA: 0x00018CB8 File Offset: 0x00016EB8
		public event CreateModelCompletedEventHandler CreateModelCompleted;

		// Token: 0x140000E6 RID: 230
		// (add) Token: 0x06001050 RID: 4176 RVA: 0x00018CF0 File Offset: 0x00016EF0
		// (remove) Token: 0x06001051 RID: 4177 RVA: 0x00018D28 File Offset: 0x00016F28
		public event GetModelDefinitionCompletedEventHandler GetModelDefinitionCompleted;

		// Token: 0x140000E7 RID: 231
		// (add) Token: 0x06001052 RID: 4178 RVA: 0x00018D60 File Offset: 0x00016F60
		// (remove) Token: 0x06001053 RID: 4179 RVA: 0x00018D98 File Offset: 0x00016F98
		public event SetModelDefinitionCompletedEventHandler SetModelDefinitionCompleted;

		// Token: 0x140000E8 RID: 232
		// (add) Token: 0x06001054 RID: 4180 RVA: 0x00018DD0 File Offset: 0x00016FD0
		// (remove) Token: 0x06001055 RID: 4181 RVA: 0x00018E08 File Offset: 0x00017008
		public event ListModelPerspectivesCompletedEventHandler ListModelPerspectivesCompleted;

		// Token: 0x140000E9 RID: 233
		// (add) Token: 0x06001056 RID: 4182 RVA: 0x00018E40 File Offset: 0x00017040
		// (remove) Token: 0x06001057 RID: 4183 RVA: 0x00018E78 File Offset: 0x00017078
		public event GetUserModelCompletedEventHandler GetUserModelCompleted;

		// Token: 0x140000EA RID: 234
		// (add) Token: 0x06001058 RID: 4184 RVA: 0x00018EB0 File Offset: 0x000170B0
		// (remove) Token: 0x06001059 RID: 4185 RVA: 0x00018EE8 File Offset: 0x000170E8
		public event ListModelItemChildrenCompletedEventHandler ListModelItemChildrenCompleted;

		// Token: 0x140000EB RID: 235
		// (add) Token: 0x0600105A RID: 4186 RVA: 0x00018F20 File Offset: 0x00017120
		// (remove) Token: 0x0600105B RID: 4187 RVA: 0x00018F58 File Offset: 0x00017158
		public event GetModelItemPermissionsCompletedEventHandler GetModelItemPermissionsCompleted;

		// Token: 0x140000EC RID: 236
		// (add) Token: 0x0600105C RID: 4188 RVA: 0x00018F90 File Offset: 0x00017190
		// (remove) Token: 0x0600105D RID: 4189 RVA: 0x00018FC8 File Offset: 0x000171C8
		public event GetModelItemPoliciesCompletedEventHandler GetModelItemPoliciesCompleted;

		// Token: 0x140000ED RID: 237
		// (add) Token: 0x0600105E RID: 4190 RVA: 0x00019000 File Offset: 0x00017200
		// (remove) Token: 0x0600105F RID: 4191 RVA: 0x00019038 File Offset: 0x00017238
		public event SetModelItemPoliciesCompletedEventHandler SetModelItemPoliciesCompleted;

		// Token: 0x140000EE RID: 238
		// (add) Token: 0x06001060 RID: 4192 RVA: 0x00019070 File Offset: 0x00017270
		// (remove) Token: 0x06001061 RID: 4193 RVA: 0x000190A8 File Offset: 0x000172A8
		public event InheritModelItemParentSecurityCompletedEventHandler InheritModelItemParentSecurityCompleted;

		// Token: 0x140000EF RID: 239
		// (add) Token: 0x06001062 RID: 4194 RVA: 0x000190E0 File Offset: 0x000172E0
		// (remove) Token: 0x06001063 RID: 4195 RVA: 0x00019118 File Offset: 0x00017318
		public event RemoveAllModelItemPoliciesCompletedEventHandler RemoveAllModelItemPoliciesCompleted;

		// Token: 0x140000F0 RID: 240
		// (add) Token: 0x06001064 RID: 4196 RVA: 0x00019150 File Offset: 0x00017350
		// (remove) Token: 0x06001065 RID: 4197 RVA: 0x00019188 File Offset: 0x00017388
		public event SetModelDrillthroughReportsCompletedEventHandler SetModelDrillthroughReportsCompleted;

		// Token: 0x140000F1 RID: 241
		// (add) Token: 0x06001066 RID: 4198 RVA: 0x000191C0 File Offset: 0x000173C0
		// (remove) Token: 0x06001067 RID: 4199 RVA: 0x000191F8 File Offset: 0x000173F8
		public event ListModelDrillthroughReportsCompletedEventHandler ListModelDrillthroughReportsCompleted;

		// Token: 0x140000F2 RID: 242
		// (add) Token: 0x06001068 RID: 4200 RVA: 0x00019230 File Offset: 0x00017430
		// (remove) Token: 0x06001069 RID: 4201 RVA: 0x00019268 File Offset: 0x00017468
		public event GenerateModelCompletedEventHandler GenerateModelCompleted;

		// Token: 0x140000F3 RID: 243
		// (add) Token: 0x0600106A RID: 4202 RVA: 0x000192A0 File Offset: 0x000174A0
		// (remove) Token: 0x0600106B RID: 4203 RVA: 0x000192D8 File Offset: 0x000174D8
		public event RegenerateModelCompletedEventHandler RegenerateModelCompleted;

		// Token: 0x140000F4 RID: 244
		// (add) Token: 0x0600106C RID: 4204 RVA: 0x00019310 File Offset: 0x00017510
		// (remove) Token: 0x0600106D RID: 4205 RVA: 0x00019348 File Offset: 0x00017548
		public event ListSecureMethodsCompletedEventHandler ListSecureMethodsCompleted;

		// Token: 0x140000F5 RID: 245
		// (add) Token: 0x0600106E RID: 4206 RVA: 0x00019380 File Offset: 0x00017580
		// (remove) Token: 0x0600106F RID: 4207 RVA: 0x000193B8 File Offset: 0x000175B8
		public event CreateBatchCompletedEventHandler CreateBatchCompleted;

		// Token: 0x140000F6 RID: 246
		// (add) Token: 0x06001070 RID: 4208 RVA: 0x000193F0 File Offset: 0x000175F0
		// (remove) Token: 0x06001071 RID: 4209 RVA: 0x00019428 File Offset: 0x00017628
		public event CancelBatchCompletedEventHandler CancelBatchCompleted;

		// Token: 0x140000F7 RID: 247
		// (add) Token: 0x06001072 RID: 4210 RVA: 0x00019460 File Offset: 0x00017660
		// (remove) Token: 0x06001073 RID: 4211 RVA: 0x00019498 File Offset: 0x00017698
		public event ExecuteBatchCompletedEventHandler ExecuteBatchCompleted;

		// Token: 0x140000F8 RID: 248
		// (add) Token: 0x06001074 RID: 4212 RVA: 0x000194D0 File Offset: 0x000176D0
		// (remove) Token: 0x06001075 RID: 4213 RVA: 0x00019508 File Offset: 0x00017708
		public event GetSystemPropertiesCompletedEventHandler GetSystemPropertiesCompleted;

		// Token: 0x140000F9 RID: 249
		// (add) Token: 0x06001076 RID: 4214 RVA: 0x00019540 File Offset: 0x00017740
		// (remove) Token: 0x06001077 RID: 4215 RVA: 0x00019578 File Offset: 0x00017778
		public event SetSystemPropertiesCompletedEventHandler SetSystemPropertiesCompleted;

		// Token: 0x140000FA RID: 250
		// (add) Token: 0x06001078 RID: 4216 RVA: 0x000195B0 File Offset: 0x000177B0
		// (remove) Token: 0x06001079 RID: 4217 RVA: 0x000195E8 File Offset: 0x000177E8
		public event DeleteItemCompletedEventHandler DeleteItemCompleted;

		// Token: 0x140000FB RID: 251
		// (add) Token: 0x0600107A RID: 4218 RVA: 0x00019620 File Offset: 0x00017820
		// (remove) Token: 0x0600107B RID: 4219 RVA: 0x00019658 File Offset: 0x00017858
		public event MoveItemCompletedEventHandler MoveItemCompleted;

		// Token: 0x140000FC RID: 252
		// (add) Token: 0x0600107C RID: 4220 RVA: 0x00019690 File Offset: 0x00017890
		// (remove) Token: 0x0600107D RID: 4221 RVA: 0x000196C8 File Offset: 0x000178C8
		public event ListChildrenCompletedEventHandler ListChildrenCompleted;

		// Token: 0x140000FD RID: 253
		// (add) Token: 0x0600107E RID: 4222 RVA: 0x00019700 File Offset: 0x00017900
		// (remove) Token: 0x0600107F RID: 4223 RVA: 0x00019738 File Offset: 0x00017938
		public event ListDependentItemsCompletedEventHandler ListDependentItemsCompleted;

		// Token: 0x140000FE RID: 254
		// (add) Token: 0x06001080 RID: 4224 RVA: 0x00019770 File Offset: 0x00017970
		// (remove) Token: 0x06001081 RID: 4225 RVA: 0x000197A8 File Offset: 0x000179A8
		public event GetPropertiesCompletedEventHandler GetPropertiesCompleted;

		// Token: 0x140000FF RID: 255
		// (add) Token: 0x06001082 RID: 4226 RVA: 0x000197E0 File Offset: 0x000179E0
		// (remove) Token: 0x06001083 RID: 4227 RVA: 0x00019818 File Offset: 0x00017A18
		public event SetPropertiesCompletedEventHandler SetPropertiesCompleted;

		// Token: 0x14000100 RID: 256
		// (add) Token: 0x06001084 RID: 4228 RVA: 0x00019850 File Offset: 0x00017A50
		// (remove) Token: 0x06001085 RID: 4229 RVA: 0x00019888 File Offset: 0x00017A88
		public event GetItemTypeCompletedEventHandler GetItemTypeCompleted;

		// Token: 0x14000101 RID: 257
		// (add) Token: 0x06001086 RID: 4230 RVA: 0x000198C0 File Offset: 0x00017AC0
		// (remove) Token: 0x06001087 RID: 4231 RVA: 0x000198F8 File Offset: 0x00017AF8
		public event CreateFolderCompletedEventHandler CreateFolderCompleted;

		// Token: 0x14000102 RID: 258
		// (add) Token: 0x06001088 RID: 4232 RVA: 0x00019930 File Offset: 0x00017B30
		// (remove) Token: 0x06001089 RID: 4233 RVA: 0x00019968 File Offset: 0x00017B68
		public event CreateReportCompletedEventHandler CreateReportCompleted;

		// Token: 0x14000103 RID: 259
		// (add) Token: 0x0600108A RID: 4234 RVA: 0x000199A0 File Offset: 0x00017BA0
		// (remove) Token: 0x0600108B RID: 4235 RVA: 0x000199D8 File Offset: 0x00017BD8
		public event GetReportDefinitionCompletedEventHandler GetReportDefinitionCompleted;

		// Token: 0x14000104 RID: 260
		// (add) Token: 0x0600108C RID: 4236 RVA: 0x00019A10 File Offset: 0x00017C10
		// (remove) Token: 0x0600108D RID: 4237 RVA: 0x00019A48 File Offset: 0x00017C48
		public event SetReportDefinitionCompletedEventHandler SetReportDefinitionCompleted;

		// Token: 0x14000105 RID: 261
		// (add) Token: 0x0600108E RID: 4238 RVA: 0x00019A80 File Offset: 0x00017C80
		// (remove) Token: 0x0600108F RID: 4239 RVA: 0x00019AB8 File Offset: 0x00017CB8
		public event CreateResourceCompletedEventHandler CreateResourceCompleted;

		// Token: 0x14000106 RID: 262
		// (add) Token: 0x06001090 RID: 4240 RVA: 0x00019AF0 File Offset: 0x00017CF0
		// (remove) Token: 0x06001091 RID: 4241 RVA: 0x00019B28 File Offset: 0x00017D28
		public event SetResourceContentsCompletedEventHandler SetResourceContentsCompleted;

		// Token: 0x14000107 RID: 263
		// (add) Token: 0x06001092 RID: 4242 RVA: 0x00019B60 File Offset: 0x00017D60
		// (remove) Token: 0x06001093 RID: 4243 RVA: 0x00019B98 File Offset: 0x00017D98
		public event GetResourceContentsCompletedEventHandler GetResourceContentsCompleted;

		// Token: 0x14000108 RID: 264
		// (add) Token: 0x06001094 RID: 4244 RVA: 0x00019BD0 File Offset: 0x00017DD0
		// (remove) Token: 0x06001095 RID: 4245 RVA: 0x00019C08 File Offset: 0x00017E08
		public event CreateReportEditSessionCompletedEventHandler CreateReportEditSessionCompleted;

		// Token: 0x14000109 RID: 265
		// (add) Token: 0x06001096 RID: 4246 RVA: 0x00019C40 File Offset: 0x00017E40
		// (remove) Token: 0x06001097 RID: 4247 RVA: 0x00019C78 File Offset: 0x00017E78
		public event GetReportParametersCompletedEventHandler GetReportParametersCompleted;

		// Token: 0x1400010A RID: 266
		// (add) Token: 0x06001098 RID: 4248 RVA: 0x00019CB0 File Offset: 0x00017EB0
		// (remove) Token: 0x06001099 RID: 4249 RVA: 0x00019CE8 File Offset: 0x00017EE8
		public event SetReportParametersCompletedEventHandler SetReportParametersCompleted;

		// Token: 0x1400010B RID: 267
		// (add) Token: 0x0600109A RID: 4250 RVA: 0x00019D20 File Offset: 0x00017F20
		// (remove) Token: 0x0600109B RID: 4251 RVA: 0x00019D58 File Offset: 0x00017F58
		public event CreateLinkedReportCompletedEventHandler CreateLinkedReportCompleted;

		// Token: 0x1400010C RID: 268
		// (add) Token: 0x0600109C RID: 4252 RVA: 0x00019D90 File Offset: 0x00017F90
		// (remove) Token: 0x0600109D RID: 4253 RVA: 0x00019DC8 File Offset: 0x00017FC8
		public event GetReportLinkCompletedEventHandler GetReportLinkCompleted;

		// Token: 0x1400010D RID: 269
		// (add) Token: 0x0600109E RID: 4254 RVA: 0x00019E00 File Offset: 0x00018000
		// (remove) Token: 0x0600109F RID: 4255 RVA: 0x00019E38 File Offset: 0x00018038
		public event SetReportLinkCompletedEventHandler SetReportLinkCompleted;

		// Token: 0x1400010E RID: 270
		// (add) Token: 0x060010A0 RID: 4256 RVA: 0x00019E70 File Offset: 0x00018070
		// (remove) Token: 0x060010A1 RID: 4257 RVA: 0x00019EA8 File Offset: 0x000180A8
		public event GetRenderResourceCompletedEventHandler GetRenderResourceCompleted;

		// Token: 0x1400010F RID: 271
		// (add) Token: 0x060010A2 RID: 4258 RVA: 0x00019EE0 File Offset: 0x000180E0
		// (remove) Token: 0x060010A3 RID: 4259 RVA: 0x00019F18 File Offset: 0x00018118
		public event SetExecutionOptionsCompletedEventHandler SetExecutionOptionsCompleted;

		// Token: 0x14000110 RID: 272
		// (add) Token: 0x060010A4 RID: 4260 RVA: 0x00019F50 File Offset: 0x00018150
		// (remove) Token: 0x060010A5 RID: 4261 RVA: 0x00019F88 File Offset: 0x00018188
		public event GetExecutionOptionsCompletedEventHandler GetExecutionOptionsCompleted;

		// Token: 0x14000111 RID: 273
		// (add) Token: 0x060010A6 RID: 4262 RVA: 0x00019FC0 File Offset: 0x000181C0
		// (remove) Token: 0x060010A7 RID: 4263 RVA: 0x00019FF8 File Offset: 0x000181F8
		public event SetCacheOptionsCompletedEventHandler SetCacheOptionsCompleted;

		// Token: 0x14000112 RID: 274
		// (add) Token: 0x060010A8 RID: 4264 RVA: 0x0001A030 File Offset: 0x00018230
		// (remove) Token: 0x060010A9 RID: 4265 RVA: 0x0001A068 File Offset: 0x00018268
		public event GetCacheOptionsCompletedEventHandler GetCacheOptionsCompleted;

		// Token: 0x14000113 RID: 275
		// (add) Token: 0x060010AA RID: 4266 RVA: 0x0001A0A0 File Offset: 0x000182A0
		// (remove) Token: 0x060010AB RID: 4267 RVA: 0x0001A0D8 File Offset: 0x000182D8
		public event UpdateReportExecutionSnapshotCompletedEventHandler UpdateReportExecutionSnapshotCompleted;

		// Token: 0x14000114 RID: 276
		// (add) Token: 0x060010AC RID: 4268 RVA: 0x0001A110 File Offset: 0x00018310
		// (remove) Token: 0x060010AD RID: 4269 RVA: 0x0001A148 File Offset: 0x00018348
		public event FlushCacheCompletedEventHandler FlushCacheCompleted;

		// Token: 0x14000115 RID: 277
		// (add) Token: 0x060010AE RID: 4270 RVA: 0x0001A180 File Offset: 0x00018380
		// (remove) Token: 0x060010AF RID: 4271 RVA: 0x0001A1B8 File Offset: 0x000183B8
		public event ListJobsCompletedEventHandler ListJobsCompleted;

		// Token: 0x14000116 RID: 278
		// (add) Token: 0x060010B0 RID: 4272 RVA: 0x0001A1F0 File Offset: 0x000183F0
		// (remove) Token: 0x060010B1 RID: 4273 RVA: 0x0001A228 File Offset: 0x00018428
		public event CancelJobCompletedEventHandler CancelJobCompleted;

		// Token: 0x14000117 RID: 279
		// (add) Token: 0x060010B2 RID: 4274 RVA: 0x0001A260 File Offset: 0x00018460
		// (remove) Token: 0x060010B3 RID: 4275 RVA: 0x0001A298 File Offset: 0x00018498
		public event CreateDataSourceCompletedEventHandler CreateDataSourceCompleted;

		// Token: 0x14000118 RID: 280
		// (add) Token: 0x060010B4 RID: 4276 RVA: 0x0001A2D0 File Offset: 0x000184D0
		// (remove) Token: 0x060010B5 RID: 4277 RVA: 0x0001A308 File Offset: 0x00018508
		public event GetDataSourceContentsCompletedEventHandler GetDataSourceContentsCompleted;

		// Token: 0x14000119 RID: 281
		// (add) Token: 0x060010B6 RID: 4278 RVA: 0x0001A340 File Offset: 0x00018540
		// (remove) Token: 0x060010B7 RID: 4279 RVA: 0x0001A378 File Offset: 0x00018578
		public event SetDataSourceContentsCompletedEventHandler SetDataSourceContentsCompleted;

		// Token: 0x1400011A RID: 282
		// (add) Token: 0x060010B8 RID: 4280 RVA: 0x0001A3B0 File Offset: 0x000185B0
		// (remove) Token: 0x060010B9 RID: 4281 RVA: 0x0001A3E8 File Offset: 0x000185E8
		public event EnableDataSourceCompletedEventHandler EnableDataSourceCompleted;

		// Token: 0x1400011B RID: 283
		// (add) Token: 0x060010BA RID: 4282 RVA: 0x0001A420 File Offset: 0x00018620
		// (remove) Token: 0x060010BB RID: 4283 RVA: 0x0001A458 File Offset: 0x00018658
		public event DisableDataSourceCompletedEventHandler DisableDataSourceCompleted;

		// Token: 0x1400011C RID: 284
		// (add) Token: 0x060010BC RID: 4284 RVA: 0x0001A490 File Offset: 0x00018690
		// (remove) Token: 0x060010BD RID: 4285 RVA: 0x0001A4C8 File Offset: 0x000186C8
		public event SetItemDataSourcesCompletedEventHandler SetItemDataSourcesCompleted;

		// Token: 0x1400011D RID: 285
		// (add) Token: 0x060010BE RID: 4286 RVA: 0x0001A500 File Offset: 0x00018700
		// (remove) Token: 0x060010BF RID: 4287 RVA: 0x0001A538 File Offset: 0x00018738
		public event GetItemDataSourcesCompletedEventHandler GetItemDataSourcesCompleted;

		// Token: 0x1400011E RID: 286
		// (add) Token: 0x060010C0 RID: 4288 RVA: 0x0001A570 File Offset: 0x00018770
		// (remove) Token: 0x060010C1 RID: 4289 RVA: 0x0001A5A8 File Offset: 0x000187A8
		public event GetItemDataSourcePromptsCompletedEventHandler GetItemDataSourcePromptsCompleted;

		// Token: 0x1400011F RID: 287
		// (add) Token: 0x060010C2 RID: 4290 RVA: 0x0001A5E0 File Offset: 0x000187E0
		// (remove) Token: 0x060010C3 RID: 4291 RVA: 0x0001A618 File Offset: 0x00018818
		public event TestConnectForDataSourceDefinitionCompletedEventHandler TestConnectForDataSourceDefinitionCompleted;

		// Token: 0x14000120 RID: 288
		// (add) Token: 0x060010C4 RID: 4292 RVA: 0x0001A650 File Offset: 0x00018850
		// (remove) Token: 0x060010C5 RID: 4293 RVA: 0x0001A688 File Offset: 0x00018888
		public event TestConnectForItemDataSourceCompletedEventHandler TestConnectForItemDataSourceCompleted;

		// Token: 0x14000121 RID: 289
		// (add) Token: 0x060010C6 RID: 4294 RVA: 0x0001A6C0 File Offset: 0x000188C0
		// (remove) Token: 0x060010C7 RID: 4295 RVA: 0x0001A6F8 File Offset: 0x000188F8
		public event CreateReportHistorySnapshotCompletedEventHandler CreateReportHistorySnapshotCompleted;

		// Token: 0x14000122 RID: 290
		// (add) Token: 0x060010C8 RID: 4296 RVA: 0x0001A730 File Offset: 0x00018930
		// (remove) Token: 0x060010C9 RID: 4297 RVA: 0x0001A768 File Offset: 0x00018968
		public event SetReportHistoryOptionsCompletedEventHandler SetReportHistoryOptionsCompleted;

		// Token: 0x14000123 RID: 291
		// (add) Token: 0x060010CA RID: 4298 RVA: 0x0001A7A0 File Offset: 0x000189A0
		// (remove) Token: 0x060010CB RID: 4299 RVA: 0x0001A7D8 File Offset: 0x000189D8
		public event GetReportHistoryOptionsCompletedEventHandler GetReportHistoryOptionsCompleted;

		// Token: 0x14000124 RID: 292
		// (add) Token: 0x060010CC RID: 4300 RVA: 0x0001A810 File Offset: 0x00018A10
		// (remove) Token: 0x060010CD RID: 4301 RVA: 0x0001A848 File Offset: 0x00018A48
		public event SetReportHistoryLimitCompletedEventHandler SetReportHistoryLimitCompleted;

		// Token: 0x14000125 RID: 293
		// (add) Token: 0x060010CE RID: 4302 RVA: 0x0001A880 File Offset: 0x00018A80
		// (remove) Token: 0x060010CF RID: 4303 RVA: 0x0001A8B8 File Offset: 0x00018AB8
		public event GetReportHistoryLimitCompletedEventHandler GetReportHistoryLimitCompleted;

		// Token: 0x14000126 RID: 294
		// (add) Token: 0x060010D0 RID: 4304 RVA: 0x0001A8F0 File Offset: 0x00018AF0
		// (remove) Token: 0x060010D1 RID: 4305 RVA: 0x0001A928 File Offset: 0x00018B28
		public event ListReportHistoryCompletedEventHandler ListReportHistoryCompleted;

		// Token: 0x14000127 RID: 295
		// (add) Token: 0x060010D2 RID: 4306 RVA: 0x0001A960 File Offset: 0x00018B60
		// (remove) Token: 0x060010D3 RID: 4307 RVA: 0x0001A998 File Offset: 0x00018B98
		public event DeleteReportHistorySnapshotCompletedEventHandler DeleteReportHistorySnapshotCompleted;

		// Token: 0x14000128 RID: 296
		// (add) Token: 0x060010D4 RID: 4308 RVA: 0x0001A9D0 File Offset: 0x00018BD0
		// (remove) Token: 0x060010D5 RID: 4309 RVA: 0x0001AA08 File Offset: 0x00018C08
		public event FindItemsCompletedEventHandler FindItemsCompleted;

		// Token: 0x14000129 RID: 297
		// (add) Token: 0x060010D6 RID: 4310 RVA: 0x0001AA40 File Offset: 0x00018C40
		// (remove) Token: 0x060010D7 RID: 4311 RVA: 0x0001AA78 File Offset: 0x00018C78
		public event CreateScheduleCompletedEventHandler CreateScheduleCompleted;

		// Token: 0x1400012A RID: 298
		// (add) Token: 0x060010D8 RID: 4312 RVA: 0x0001AAB0 File Offset: 0x00018CB0
		// (remove) Token: 0x060010D9 RID: 4313 RVA: 0x0001AAE8 File Offset: 0x00018CE8
		public event DeleteScheduleCompletedEventHandler DeleteScheduleCompleted;

		// Token: 0x1400012B RID: 299
		// (add) Token: 0x060010DA RID: 4314 RVA: 0x0001AB20 File Offset: 0x00018D20
		// (remove) Token: 0x060010DB RID: 4315 RVA: 0x0001AB58 File Offset: 0x00018D58
		public event SetSchedulePropertiesCompletedEventHandler SetSchedulePropertiesCompleted;

		// Token: 0x1400012C RID: 300
		// (add) Token: 0x060010DC RID: 4316 RVA: 0x0001AB90 File Offset: 0x00018D90
		// (remove) Token: 0x060010DD RID: 4317 RVA: 0x0001ABC8 File Offset: 0x00018DC8
		public event GetSchedulePropertiesCompletedEventHandler GetSchedulePropertiesCompleted;

		// Token: 0x1400012D RID: 301
		// (add) Token: 0x060010DE RID: 4318 RVA: 0x0001AC00 File Offset: 0x00018E00
		// (remove) Token: 0x060010DF RID: 4319 RVA: 0x0001AC38 File Offset: 0x00018E38
		public event ListScheduledReportsCompletedEventHandler ListScheduledReportsCompleted;

		// Token: 0x1400012E RID: 302
		// (add) Token: 0x060010E0 RID: 4320 RVA: 0x0001AC70 File Offset: 0x00018E70
		// (remove) Token: 0x060010E1 RID: 4321 RVA: 0x0001ACA8 File Offset: 0x00018EA8
		public event ListSchedulesCompletedEventHandler ListSchedulesCompleted;

		// Token: 0x1400012F RID: 303
		// (add) Token: 0x060010E2 RID: 4322 RVA: 0x0001ACE0 File Offset: 0x00018EE0
		// (remove) Token: 0x060010E3 RID: 4323 RVA: 0x0001AD18 File Offset: 0x00018F18
		public event PauseScheduleCompletedEventHandler PauseScheduleCompleted;

		// Token: 0x14000130 RID: 304
		// (add) Token: 0x060010E4 RID: 4324 RVA: 0x0001AD50 File Offset: 0x00018F50
		// (remove) Token: 0x060010E5 RID: 4325 RVA: 0x0001AD88 File Offset: 0x00018F88
		public event ResumeScheduleCompletedEventHandler ResumeScheduleCompleted;

		// Token: 0x14000131 RID: 305
		// (add) Token: 0x060010E6 RID: 4326 RVA: 0x0001ADC0 File Offset: 0x00018FC0
		// (remove) Token: 0x060010E7 RID: 4327 RVA: 0x0001ADF8 File Offset: 0x00018FF8
		public event CreateSubscriptionCompletedEventHandler CreateSubscriptionCompleted;

		// Token: 0x14000132 RID: 306
		// (add) Token: 0x060010E8 RID: 4328 RVA: 0x0001AE30 File Offset: 0x00019030
		// (remove) Token: 0x060010E9 RID: 4329 RVA: 0x0001AE68 File Offset: 0x00019068
		public event CreateDataDrivenSubscriptionCompletedEventHandler CreateDataDrivenSubscriptionCompleted;

		// Token: 0x14000133 RID: 307
		// (add) Token: 0x060010EA RID: 4330 RVA: 0x0001AEA0 File Offset: 0x000190A0
		// (remove) Token: 0x060010EB RID: 4331 RVA: 0x0001AED8 File Offset: 0x000190D8
		public event SetSubscriptionPropertiesCompletedEventHandler SetSubscriptionPropertiesCompleted;

		// Token: 0x14000134 RID: 308
		// (add) Token: 0x060010EC RID: 4332 RVA: 0x0001AF10 File Offset: 0x00019110
		// (remove) Token: 0x060010ED RID: 4333 RVA: 0x0001AF48 File Offset: 0x00019148
		public event SetDataDrivenSubscriptionPropertiesCompletedEventHandler SetDataDrivenSubscriptionPropertiesCompleted;

		// Token: 0x14000135 RID: 309
		// (add) Token: 0x060010EE RID: 4334 RVA: 0x0001AF80 File Offset: 0x00019180
		// (remove) Token: 0x060010EF RID: 4335 RVA: 0x0001AFB8 File Offset: 0x000191B8
		public event GetSubscriptionPropertiesCompletedEventHandler GetSubscriptionPropertiesCompleted;

		// Token: 0x14000136 RID: 310
		// (add) Token: 0x060010F0 RID: 4336 RVA: 0x0001AFF0 File Offset: 0x000191F0
		// (remove) Token: 0x060010F1 RID: 4337 RVA: 0x0001B028 File Offset: 0x00019228
		public event GetDataDrivenSubscriptionPropertiesCompletedEventHandler GetDataDrivenSubscriptionPropertiesCompleted;

		// Token: 0x14000137 RID: 311
		// (add) Token: 0x060010F2 RID: 4338 RVA: 0x0001B060 File Offset: 0x00019260
		// (remove) Token: 0x060010F3 RID: 4339 RVA: 0x0001B098 File Offset: 0x00019298
		public event DeleteSubscriptionCompletedEventHandler DeleteSubscriptionCompleted;

		// Token: 0x14000138 RID: 312
		// (add) Token: 0x060010F4 RID: 4340 RVA: 0x0001B0D0 File Offset: 0x000192D0
		// (remove) Token: 0x060010F5 RID: 4341 RVA: 0x0001B108 File Offset: 0x00019308
		public event PrepareQueryCompletedEventHandler PrepareQueryCompleted;

		// Token: 0x14000139 RID: 313
		// (add) Token: 0x060010F6 RID: 4342 RVA: 0x0001B140 File Offset: 0x00019340
		// (remove) Token: 0x060010F7 RID: 4343 RVA: 0x0001B178 File Offset: 0x00019378
		public event GetExtensionSettingsCompletedEventHandler GetExtensionSettingsCompleted;

		// Token: 0x1400013A RID: 314
		// (add) Token: 0x060010F8 RID: 4344 RVA: 0x0001B1B0 File Offset: 0x000193B0
		// (remove) Token: 0x060010F9 RID: 4345 RVA: 0x0001B1E8 File Offset: 0x000193E8
		public event ValidateExtensionSettingsCompletedEventHandler ValidateExtensionSettingsCompleted;

		// Token: 0x1400013B RID: 315
		// (add) Token: 0x060010FA RID: 4346 RVA: 0x0001B220 File Offset: 0x00019420
		// (remove) Token: 0x060010FB RID: 4347 RVA: 0x0001B258 File Offset: 0x00019458
		public event ListSubscriptionsCompletedEventHandler ListSubscriptionsCompleted;

		// Token: 0x1400013C RID: 316
		// (add) Token: 0x060010FC RID: 4348 RVA: 0x0001B290 File Offset: 0x00019490
		// (remove) Token: 0x060010FD RID: 4349 RVA: 0x0001B2C8 File Offset: 0x000194C8
		public event ListSubscriptionsUsingDataSourceCompletedEventHandler ListSubscriptionsUsingDataSourceCompleted;

		// Token: 0x1400013D RID: 317
		// (add) Token: 0x060010FE RID: 4350 RVA: 0x0001B300 File Offset: 0x00019500
		// (remove) Token: 0x060010FF RID: 4351 RVA: 0x0001B338 File Offset: 0x00019538
		public event ListExtensionsCompletedEventHandler ListExtensionsCompleted;

		// Token: 0x1400013E RID: 318
		// (add) Token: 0x06001100 RID: 4352 RVA: 0x0001B370 File Offset: 0x00019570
		// (remove) Token: 0x06001101 RID: 4353 RVA: 0x0001B3A8 File Offset: 0x000195A8
		public event ListEventsCompletedEventHandler ListEventsCompleted;

		// Token: 0x1400013F RID: 319
		// (add) Token: 0x06001102 RID: 4354 RVA: 0x0001B3E0 File Offset: 0x000195E0
		// (remove) Token: 0x06001103 RID: 4355 RVA: 0x0001B418 File Offset: 0x00019618
		public event FireEventCompletedEventHandler FireEventCompleted;

		// Token: 0x14000140 RID: 320
		// (add) Token: 0x06001104 RID: 4356 RVA: 0x0001B450 File Offset: 0x00019650
		// (remove) Token: 0x06001105 RID: 4357 RVA: 0x0001B488 File Offset: 0x00019688
		public event ListTasksCompletedEventHandler ListTasksCompleted;

		// Token: 0x14000141 RID: 321
		// (add) Token: 0x06001106 RID: 4358 RVA: 0x0001B4C0 File Offset: 0x000196C0
		// (remove) Token: 0x06001107 RID: 4359 RVA: 0x0001B4F8 File Offset: 0x000196F8
		public event ListRolesCompletedEventHandler ListRolesCompleted;

		// Token: 0x14000142 RID: 322
		// (add) Token: 0x06001108 RID: 4360 RVA: 0x0001B530 File Offset: 0x00019730
		// (remove) Token: 0x06001109 RID: 4361 RVA: 0x0001B568 File Offset: 0x00019768
		public event CreateRoleCompletedEventHandler CreateRoleCompleted;

		// Token: 0x14000143 RID: 323
		// (add) Token: 0x0600110A RID: 4362 RVA: 0x0001B5A0 File Offset: 0x000197A0
		// (remove) Token: 0x0600110B RID: 4363 RVA: 0x0001B5D8 File Offset: 0x000197D8
		public event DeleteRoleCompletedEventHandler DeleteRoleCompleted;

		// Token: 0x14000144 RID: 324
		// (add) Token: 0x0600110C RID: 4364 RVA: 0x0001B610 File Offset: 0x00019810
		// (remove) Token: 0x0600110D RID: 4365 RVA: 0x0001B648 File Offset: 0x00019848
		public event GetRolePropertiesCompletedEventHandler GetRolePropertiesCompleted;

		// Token: 0x14000145 RID: 325
		// (add) Token: 0x0600110E RID: 4366 RVA: 0x0001B680 File Offset: 0x00019880
		// (remove) Token: 0x0600110F RID: 4367 RVA: 0x0001B6B8 File Offset: 0x000198B8
		public event SetRolePropertiesCompletedEventHandler SetRolePropertiesCompleted;

		// Token: 0x06001110 RID: 4368 RVA: 0x0001B6ED File Offset: 0x000198ED
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/GetSystemPolicies", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Policies")]
		public Policy[] GetSystemPolicies()
		{
			return (Policy[])base.Invoke("GetSystemPolicies", new object[0])[0];
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x0001B707 File Offset: 0x00019907
		public IAsyncResult BeginGetSystemPolicies(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetSystemPolicies", new object[0], callback, asyncState);
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x0001B71C File Offset: 0x0001991C
		public Policy[] EndGetSystemPolicies(IAsyncResult asyncResult)
		{
			return (Policy[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x0001B72C File Offset: 0x0001992C
		public void GetSystemPoliciesAsync()
		{
			this.GetSystemPoliciesAsync(null);
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x0001B735 File Offset: 0x00019935
		public void GetSystemPoliciesAsync(object userState)
		{
			if (this.GetSystemPoliciesOperationCompleted == null)
			{
				this.GetSystemPoliciesOperationCompleted = new SendOrPostCallback(this.OnGetSystemPoliciesOperationCompleted);
			}
			base.InvokeAsync("GetSystemPolicies", new object[0], this.GetSystemPoliciesOperationCompleted, userState);
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x0001B76C File Offset: 0x0001996C
		private void OnGetSystemPoliciesOperationCompleted(object arg)
		{
			if (this.GetSystemPoliciesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetSystemPoliciesCompleted(this, new GetSystemPoliciesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x0001B7B1 File Offset: 0x000199B1
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("BatchHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/SetSystemPolicies", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void SetSystemPolicies(Policy[] Policies)
		{
			base.Invoke("SetSystemPolicies", new object[] { Policies });
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x0001B7C9 File Offset: 0x000199C9
		public IAsyncResult BeginSetSystemPolicies(Policy[] Policies, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetSystemPolicies", new object[] { Policies }, callback, asyncState);
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x0001B7E2 File Offset: 0x000199E2
		public void EndSetSystemPolicies(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x0001B7EC File Offset: 0x000199EC
		public void SetSystemPoliciesAsync(Policy[] Policies)
		{
			this.SetSystemPoliciesAsync(Policies, null);
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x0001B7F6 File Offset: 0x000199F6
		public void SetSystemPoliciesAsync(Policy[] Policies, object userState)
		{
			if (this.SetSystemPoliciesOperationCompleted == null)
			{
				this.SetSystemPoliciesOperationCompleted = new SendOrPostCallback(this.OnSetSystemPoliciesOperationCompleted);
			}
			base.InvokeAsync("SetSystemPolicies", new object[] { Policies }, this.SetSystemPoliciesOperationCompleted, userState);
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x0001B830 File Offset: 0x00019A30
		private void OnSetSystemPoliciesOperationCompleted(object arg)
		{
			if (this.SetSystemPoliciesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetSystemPoliciesCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x0001B870 File Offset: 0x00019A70
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/GetPolicies", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Policies")]
		public Policy[] GetPolicies(string Item, out bool InheritParent)
		{
			object[] array = base.Invoke("GetPolicies", new object[] { Item });
			InheritParent = (bool)array[1];
			return (Policy[])array[0];
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x0001B8A5 File Offset: 0x00019AA5
		public IAsyncResult BeginGetPolicies(string Item, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetPolicies", new object[] { Item }, callback, asyncState);
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x0001B8C0 File Offset: 0x00019AC0
		public Policy[] EndGetPolicies(IAsyncResult asyncResult, out bool InheritParent)
		{
			object[] array = base.EndInvoke(asyncResult);
			InheritParent = (bool)array[1];
			return (Policy[])array[0];
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x0001B8E7 File Offset: 0x00019AE7
		public void GetPoliciesAsync(string Item)
		{
			this.GetPoliciesAsync(Item, null);
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x0001B8F1 File Offset: 0x00019AF1
		public void GetPoliciesAsync(string Item, object userState)
		{
			if (this.GetPoliciesOperationCompleted == null)
			{
				this.GetPoliciesOperationCompleted = new SendOrPostCallback(this.OnGetPoliciesOperationCompleted);
			}
			base.InvokeAsync("GetPolicies", new object[] { Item }, this.GetPoliciesOperationCompleted, userState);
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x0001B92C File Offset: 0x00019B2C
		private void OnGetPoliciesOperationCompleted(object arg)
		{
			if (this.GetPoliciesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetPoliciesCompleted(this, new GetPoliciesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x0001B971 File Offset: 0x00019B71
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("BatchHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/SetPolicies", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void SetPolicies(string Item, Policy[] Policies)
		{
			base.Invoke("SetPolicies", new object[] { Item, Policies });
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x0001B98D File Offset: 0x00019B8D
		public IAsyncResult BeginSetPolicies(string Item, Policy[] Policies, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetPolicies", new object[] { Item, Policies }, callback, asyncState);
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x0001B9AB File Offset: 0x00019BAB
		public void EndSetPolicies(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x0001B9B5 File Offset: 0x00019BB5
		public void SetPoliciesAsync(string Item, Policy[] Policies)
		{
			this.SetPoliciesAsync(Item, Policies, null);
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x0001B9C0 File Offset: 0x00019BC0
		public void SetPoliciesAsync(string Item, Policy[] Policies, object userState)
		{
			if (this.SetPoliciesOperationCompleted == null)
			{
				this.SetPoliciesOperationCompleted = new SendOrPostCallback(this.OnSetPoliciesOperationCompleted);
			}
			base.InvokeAsync("SetPolicies", new object[] { Item, Policies }, this.SetPoliciesOperationCompleted, userState);
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x0001B9FC File Offset: 0x00019BFC
		private void OnSetPoliciesOperationCompleted(object arg)
		{
			if (this.SetPoliciesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetPoliciesCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x0001BA3B File Offset: 0x00019C3B
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("BatchHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/InheritParentSecurity", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void InheritParentSecurity(string Item)
		{
			base.Invoke("InheritParentSecurity", new object[] { Item });
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x0001BA53 File Offset: 0x00019C53
		public IAsyncResult BeginInheritParentSecurity(string Item, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("InheritParentSecurity", new object[] { Item }, callback, asyncState);
		}

		// Token: 0x0600112A RID: 4394 RVA: 0x0001BA6C File Offset: 0x00019C6C
		public void EndInheritParentSecurity(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x0600112B RID: 4395 RVA: 0x0001BA76 File Offset: 0x00019C76
		public void InheritParentSecurityAsync(string Item)
		{
			this.InheritParentSecurityAsync(Item, null);
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x0001BA80 File Offset: 0x00019C80
		public void InheritParentSecurityAsync(string Item, object userState)
		{
			if (this.InheritParentSecurityOperationCompleted == null)
			{
				this.InheritParentSecurityOperationCompleted = new SendOrPostCallback(this.OnInheritParentSecurityOperationCompleted);
			}
			base.InvokeAsync("InheritParentSecurity", new object[] { Item }, this.InheritParentSecurityOperationCompleted, userState);
		}

		// Token: 0x0600112D RID: 4397 RVA: 0x0001BAB8 File Offset: 0x00019CB8
		private void OnInheritParentSecurityOperationCompleted(object arg)
		{
			if (this.InheritParentSecurityCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.InheritParentSecurityCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x0001BAF7 File Offset: 0x00019CF7
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/GetSystemPermissions", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Permissions")]
		[return: XmlArrayItem("Operation")]
		public string[] GetSystemPermissions()
		{
			return (string[])base.Invoke("GetSystemPermissions", new object[0])[0];
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x0001BB11 File Offset: 0x00019D11
		public IAsyncResult BeginGetSystemPermissions(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetSystemPermissions", new object[0], callback, asyncState);
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x0001BB26 File Offset: 0x00019D26
		public string[] EndGetSystemPermissions(IAsyncResult asyncResult)
		{
			return (string[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x0001BB36 File Offset: 0x00019D36
		public void GetSystemPermissionsAsync()
		{
			this.GetSystemPermissionsAsync(null);
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x0001BB3F File Offset: 0x00019D3F
		public void GetSystemPermissionsAsync(object userState)
		{
			if (this.GetSystemPermissionsOperationCompleted == null)
			{
				this.GetSystemPermissionsOperationCompleted = new SendOrPostCallback(this.OnGetSystemPermissionsOperationCompleted);
			}
			base.InvokeAsync("GetSystemPermissions", new object[0], this.GetSystemPermissionsOperationCompleted, userState);
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x0001BB74 File Offset: 0x00019D74
		private void OnGetSystemPermissionsOperationCompleted(object arg)
		{
			if (this.GetSystemPermissionsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetSystemPermissionsCompleted(this, new GetSystemPermissionsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x0001BBB9 File Offset: 0x00019DB9
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/GetPermissions", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Permissions")]
		[return: XmlArrayItem("Operation")]
		public string[] GetPermissions(string Item)
		{
			return (string[])base.Invoke("GetPermissions", new object[] { Item })[0];
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x0001BBD7 File Offset: 0x00019DD7
		public IAsyncResult BeginGetPermissions(string Item, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetPermissions", new object[] { Item }, callback, asyncState);
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x0001BBF0 File Offset: 0x00019DF0
		public string[] EndGetPermissions(IAsyncResult asyncResult)
		{
			return (string[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x0001BC00 File Offset: 0x00019E00
		public void GetPermissionsAsync(string Item)
		{
			this.GetPermissionsAsync(Item, null);
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x0001BC0A File Offset: 0x00019E0A
		public void GetPermissionsAsync(string Item, object userState)
		{
			if (this.GetPermissionsOperationCompleted == null)
			{
				this.GetPermissionsOperationCompleted = new SendOrPostCallback(this.OnGetPermissionsOperationCompleted);
			}
			base.InvokeAsync("GetPermissions", new object[] { Item }, this.GetPermissionsOperationCompleted, userState);
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x0001BC44 File Offset: 0x00019E44
		private void OnGetPermissionsOperationCompleted(object arg)
		{
			if (this.GetPermissionsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetPermissionsCompleted(this, new GetPermissionsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x0001BC89 File Offset: 0x00019E89
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/LogonUser", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void LogonUser(string userName, string password, string authority)
		{
			base.Invoke("LogonUser", new object[] { userName, password, authority });
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x0001BCA9 File Offset: 0x00019EA9
		public IAsyncResult BeginLogonUser(string userName, string password, string authority, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("LogonUser", new object[] { userName, password, authority }, callback, asyncState);
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x0001BCCC File Offset: 0x00019ECC
		public void EndLogonUser(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x0001BCD6 File Offset: 0x00019ED6
		public void LogonUserAsync(string userName, string password, string authority)
		{
			this.LogonUserAsync(userName, password, authority, null);
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x0001BCE4 File Offset: 0x00019EE4
		public void LogonUserAsync(string userName, string password, string authority, object userState)
		{
			if (this.LogonUserOperationCompleted == null)
			{
				this.LogonUserOperationCompleted = new SendOrPostCallback(this.OnLogonUserOperationCompleted);
			}
			base.InvokeAsync("LogonUser", new object[] { userName, password, authority }, this.LogonUserOperationCompleted, userState);
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x0001BD30 File Offset: 0x00019F30
		private void OnLogonUserOperationCompleted(object arg)
		{
			if (this.LogonUserCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.LogonUserCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x0001BD6F File Offset: 0x00019F6F
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/Logoff", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void Logoff()
		{
			base.Invoke("Logoff", new object[0]);
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x0001BD83 File Offset: 0x00019F83
		public IAsyncResult BeginLogoff(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("Logoff", new object[0], callback, asyncState);
		}

		// Token: 0x06001142 RID: 4418 RVA: 0x0001BD98 File Offset: 0x00019F98
		public void EndLogoff(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x0001BDA2 File Offset: 0x00019FA2
		public void LogoffAsync()
		{
			this.LogoffAsync(null);
		}

		// Token: 0x06001144 RID: 4420 RVA: 0x0001BDAB File Offset: 0x00019FAB
		public void LogoffAsync(object userState)
		{
			if (this.LogoffOperationCompleted == null)
			{
				this.LogoffOperationCompleted = new SendOrPostCallback(this.OnLogoffOperationCompleted);
			}
			base.InvokeAsync("Logoff", new object[0], this.LogoffOperationCompleted, userState);
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x0001BDE0 File Offset: 0x00019FE0
		private void OnLogoffOperationCompleted(object arg)
		{
			if (this.LogoffCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.LogoffCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x0001BE1F File Offset: 0x0001A01F
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("BatchHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/CreateModel", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Warnings")]
		public Warning[] CreateModel(string Model, string Parent, [XmlElement(DataType = "base64Binary")] byte[] Definition, Property[] Properties)
		{
			return (Warning[])base.Invoke("CreateModel", new object[] { Model, Parent, Definition, Properties })[0];
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x0001BE4A File Offset: 0x0001A04A
		public IAsyncResult BeginCreateModel(string Model, string Parent, byte[] Definition, Property[] Properties, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateModel", new object[] { Model, Parent, Definition, Properties }, callback, asyncState);
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x0001BE72 File Offset: 0x0001A072
		public Warning[] EndCreateModel(IAsyncResult asyncResult)
		{
			return (Warning[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x0001BE82 File Offset: 0x0001A082
		public void CreateModelAsync(string Model, string Parent, byte[] Definition, Property[] Properties)
		{
			this.CreateModelAsync(Model, Parent, Definition, Properties, null);
		}

		// Token: 0x0600114A RID: 4426 RVA: 0x0001BE90 File Offset: 0x0001A090
		public void CreateModelAsync(string Model, string Parent, byte[] Definition, Property[] Properties, object userState)
		{
			if (this.CreateModelOperationCompleted == null)
			{
				this.CreateModelOperationCompleted = new SendOrPostCallback(this.OnCreateModelOperationCompleted);
			}
			base.InvokeAsync("CreateModel", new object[] { Model, Parent, Definition, Properties }, this.CreateModelOperationCompleted, userState);
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x0001BEE4 File Offset: 0x0001A0E4
		private void OnCreateModelOperationCompleted(object arg)
		{
			if (this.CreateModelCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateModelCompleted(this, new CreateModelCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x0001BF29 File Offset: 0x0001A129
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/GetModelDefinition", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Definition", DataType = "base64Binary")]
		public byte[] GetModelDefinition(string Model)
		{
			return (byte[])base.Invoke("GetModelDefinition", new object[] { Model })[0];
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x0001BF47 File Offset: 0x0001A147
		public IAsyncResult BeginGetModelDefinition(string Model, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetModelDefinition", new object[] { Model }, callback, asyncState);
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x0001BF60 File Offset: 0x0001A160
		public byte[] EndGetModelDefinition(IAsyncResult asyncResult)
		{
			return (byte[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x0001BF70 File Offset: 0x0001A170
		public void GetModelDefinitionAsync(string Model)
		{
			this.GetModelDefinitionAsync(Model, null);
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x0001BF7A File Offset: 0x0001A17A
		public void GetModelDefinitionAsync(string Model, object userState)
		{
			if (this.GetModelDefinitionOperationCompleted == null)
			{
				this.GetModelDefinitionOperationCompleted = new SendOrPostCallback(this.OnGetModelDefinitionOperationCompleted);
			}
			base.InvokeAsync("GetModelDefinition", new object[] { Model }, this.GetModelDefinitionOperationCompleted, userState);
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x0001BFB4 File Offset: 0x0001A1B4
		private void OnGetModelDefinitionOperationCompleted(object arg)
		{
			if (this.GetModelDefinitionCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetModelDefinitionCompleted(this, new GetModelDefinitionCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001152 RID: 4434 RVA: 0x0001BFF9 File Offset: 0x0001A1F9
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("BatchHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/SetModelDefinition", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Warnings")]
		public Warning[] SetModelDefinition(string Model, [XmlElement(DataType = "base64Binary")] byte[] Definition)
		{
			return (Warning[])base.Invoke("SetModelDefinition", new object[] { Model, Definition })[0];
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x0001C01B File Offset: 0x0001A21B
		public IAsyncResult BeginSetModelDefinition(string Model, byte[] Definition, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetModelDefinition", new object[] { Model, Definition }, callback, asyncState);
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x0001C039 File Offset: 0x0001A239
		public Warning[] EndSetModelDefinition(IAsyncResult asyncResult)
		{
			return (Warning[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x0001C049 File Offset: 0x0001A249
		public void SetModelDefinitionAsync(string Model, byte[] Definition)
		{
			this.SetModelDefinitionAsync(Model, Definition, null);
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x0001C054 File Offset: 0x0001A254
		public void SetModelDefinitionAsync(string Model, byte[] Definition, object userState)
		{
			if (this.SetModelDefinitionOperationCompleted == null)
			{
				this.SetModelDefinitionOperationCompleted = new SendOrPostCallback(this.OnSetModelDefinitionOperationCompleted);
			}
			base.InvokeAsync("SetModelDefinition", new object[] { Model, Definition }, this.SetModelDefinitionOperationCompleted, userState);
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x0001C090 File Offset: 0x0001A290
		private void OnSetModelDefinitionOperationCompleted(object arg)
		{
			if (this.SetModelDefinitionCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetModelDefinitionCompleted(this, new SetModelDefinitionCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x0001C0D5 File Offset: 0x0001A2D5
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/ListModelPerspectives", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("ModelCatalogItems")]
		public ModelCatalogItem[] ListModelPerspectives(string Model)
		{
			return (ModelCatalogItem[])base.Invoke("ListModelPerspectives", new object[] { Model })[0];
		}

		// Token: 0x06001159 RID: 4441 RVA: 0x0001C0F3 File Offset: 0x0001A2F3
		public IAsyncResult BeginListModelPerspectives(string Model, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListModelPerspectives", new object[] { Model }, callback, asyncState);
		}

		// Token: 0x0600115A RID: 4442 RVA: 0x0001C10C File Offset: 0x0001A30C
		public ModelCatalogItem[] EndListModelPerspectives(IAsyncResult asyncResult)
		{
			return (ModelCatalogItem[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x0600115B RID: 4443 RVA: 0x0001C11C File Offset: 0x0001A31C
		public void ListModelPerspectivesAsync(string Model)
		{
			this.ListModelPerspectivesAsync(Model, null);
		}

		// Token: 0x0600115C RID: 4444 RVA: 0x0001C126 File Offset: 0x0001A326
		public void ListModelPerspectivesAsync(string Model, object userState)
		{
			if (this.ListModelPerspectivesOperationCompleted == null)
			{
				this.ListModelPerspectivesOperationCompleted = new SendOrPostCallback(this.OnListModelPerspectivesOperationCompleted);
			}
			base.InvokeAsync("ListModelPerspectives", new object[] { Model }, this.ListModelPerspectivesOperationCompleted, userState);
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x0001C160 File Offset: 0x0001A360
		private void OnListModelPerspectivesOperationCompleted(object arg)
		{
			if (this.ListModelPerspectivesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListModelPerspectivesCompleted(this, new ListModelPerspectivesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x0001C1A5 File Offset: 0x0001A3A5
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/GetUserModel", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Definition", DataType = "base64Binary")]
		public byte[] GetUserModel(string Model, string Perspective)
		{
			return (byte[])base.Invoke("GetUserModel", new object[] { Model, Perspective })[0];
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x0001C1C7 File Offset: 0x0001A3C7
		public IAsyncResult BeginGetUserModel(string Model, string Perspective, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetUserModel", new object[] { Model, Perspective }, callback, asyncState);
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x0001C1E5 File Offset: 0x0001A3E5
		public byte[] EndGetUserModel(IAsyncResult asyncResult)
		{
			return (byte[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06001161 RID: 4449 RVA: 0x0001C1F5 File Offset: 0x0001A3F5
		public void GetUserModelAsync(string Model, string Perspective)
		{
			this.GetUserModelAsync(Model, Perspective, null);
		}

		// Token: 0x06001162 RID: 4450 RVA: 0x0001C200 File Offset: 0x0001A400
		public void GetUserModelAsync(string Model, string Perspective, object userState)
		{
			if (this.GetUserModelOperationCompleted == null)
			{
				this.GetUserModelOperationCompleted = new SendOrPostCallback(this.OnGetUserModelOperationCompleted);
			}
			base.InvokeAsync("GetUserModel", new object[] { Model, Perspective }, this.GetUserModelOperationCompleted, userState);
		}

		// Token: 0x06001163 RID: 4451 RVA: 0x0001C23C File Offset: 0x0001A43C
		private void OnGetUserModelOperationCompleted(object arg)
		{
			if (this.GetUserModelCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetUserModelCompleted(this, new GetUserModelCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x0001C281 File Offset: 0x0001A481
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/ListModelItemChildren", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("ModelItems")]
		public ModelItem[] ListModelItemChildren(string Model, string ModelItemID, bool Recursive)
		{
			return (ModelItem[])base.Invoke("ListModelItemChildren", new object[] { Model, ModelItemID, Recursive })[0];
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x0001C2AC File Offset: 0x0001A4AC
		public IAsyncResult BeginListModelItemChildren(string Model, string ModelItemID, bool Recursive, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListModelItemChildren", new object[] { Model, ModelItemID, Recursive }, callback, asyncState);
		}

		// Token: 0x06001166 RID: 4454 RVA: 0x0001C2D4 File Offset: 0x0001A4D4
		public ModelItem[] EndListModelItemChildren(IAsyncResult asyncResult)
		{
			return (ModelItem[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x0001C2E4 File Offset: 0x0001A4E4
		public void ListModelItemChildrenAsync(string Model, string ModelItemID, bool Recursive)
		{
			this.ListModelItemChildrenAsync(Model, ModelItemID, Recursive, null);
		}

		// Token: 0x06001168 RID: 4456 RVA: 0x0001C2F0 File Offset: 0x0001A4F0
		public void ListModelItemChildrenAsync(string Model, string ModelItemID, bool Recursive, object userState)
		{
			if (this.ListModelItemChildrenOperationCompleted == null)
			{
				this.ListModelItemChildrenOperationCompleted = new SendOrPostCallback(this.OnListModelItemChildrenOperationCompleted);
			}
			base.InvokeAsync("ListModelItemChildren", new object[] { Model, ModelItemID, Recursive }, this.ListModelItemChildrenOperationCompleted, userState);
		}

		// Token: 0x06001169 RID: 4457 RVA: 0x0001C344 File Offset: 0x0001A544
		private void OnListModelItemChildrenOperationCompleted(object arg)
		{
			if (this.ListModelItemChildrenCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListModelItemChildrenCompleted(this, new ListModelItemChildrenCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600116A RID: 4458 RVA: 0x0001C389 File Offset: 0x0001A589
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/GetModelItemPermissions", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Permissions")]
		public string[] GetModelItemPermissions(string Model, string ModelItemID)
		{
			return (string[])base.Invoke("GetModelItemPermissions", new object[] { Model, ModelItemID })[0];
		}

		// Token: 0x0600116B RID: 4459 RVA: 0x0001C3AB File Offset: 0x0001A5AB
		public IAsyncResult BeginGetModelItemPermissions(string Model, string ModelItemID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetModelItemPermissions", new object[] { Model, ModelItemID }, callback, asyncState);
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x0001C3C9 File Offset: 0x0001A5C9
		public string[] EndGetModelItemPermissions(IAsyncResult asyncResult)
		{
			return (string[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x0600116D RID: 4461 RVA: 0x0001C3D9 File Offset: 0x0001A5D9
		public void GetModelItemPermissionsAsync(string Model, string ModelItemID)
		{
			this.GetModelItemPermissionsAsync(Model, ModelItemID, null);
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x0001C3E4 File Offset: 0x0001A5E4
		public void GetModelItemPermissionsAsync(string Model, string ModelItemID, object userState)
		{
			if (this.GetModelItemPermissionsOperationCompleted == null)
			{
				this.GetModelItemPermissionsOperationCompleted = new SendOrPostCallback(this.OnGetModelItemPermissionsOperationCompleted);
			}
			base.InvokeAsync("GetModelItemPermissions", new object[] { Model, ModelItemID }, this.GetModelItemPermissionsOperationCompleted, userState);
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x0001C420 File Offset: 0x0001A620
		private void OnGetModelItemPermissionsOperationCompleted(object arg)
		{
			if (this.GetModelItemPermissionsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetModelItemPermissionsCompleted(this, new GetModelItemPermissionsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x0001C468 File Offset: 0x0001A668
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/GetModelItemPolicies", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Policies")]
		public Policy[] GetModelItemPolicies(string Model, string ModelItemID, out bool InheritParent)
		{
			object[] array = base.Invoke("GetModelItemPolicies", new object[] { Model, ModelItemID });
			InheritParent = (bool)array[1];
			return (Policy[])array[0];
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x0001C4A1 File Offset: 0x0001A6A1
		public IAsyncResult BeginGetModelItemPolicies(string Model, string ModelItemID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetModelItemPolicies", new object[] { Model, ModelItemID }, callback, asyncState);
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x0001C4C0 File Offset: 0x0001A6C0
		public Policy[] EndGetModelItemPolicies(IAsyncResult asyncResult, out bool InheritParent)
		{
			object[] array = base.EndInvoke(asyncResult);
			InheritParent = (bool)array[1];
			return (Policy[])array[0];
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x0001C4E7 File Offset: 0x0001A6E7
		public void GetModelItemPoliciesAsync(string Model, string ModelItemID)
		{
			this.GetModelItemPoliciesAsync(Model, ModelItemID, null);
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x0001C4F2 File Offset: 0x0001A6F2
		public void GetModelItemPoliciesAsync(string Model, string ModelItemID, object userState)
		{
			if (this.GetModelItemPoliciesOperationCompleted == null)
			{
				this.GetModelItemPoliciesOperationCompleted = new SendOrPostCallback(this.OnGetModelItemPoliciesOperationCompleted);
			}
			base.InvokeAsync("GetModelItemPolicies", new object[] { Model, ModelItemID }, this.GetModelItemPoliciesOperationCompleted, userState);
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x0001C530 File Offset: 0x0001A730
		private void OnGetModelItemPoliciesOperationCompleted(object arg)
		{
			if (this.GetModelItemPoliciesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetModelItemPoliciesCompleted(this, new GetModelItemPoliciesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x0001C575 File Offset: 0x0001A775
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("BatchHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/SetModelItemPolicies", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void SetModelItemPolicies(string Model, string ModelItemID, Policy[] Policies)
		{
			base.Invoke("SetModelItemPolicies", new object[] { Model, ModelItemID, Policies });
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x0001C595 File Offset: 0x0001A795
		public IAsyncResult BeginSetModelItemPolicies(string Model, string ModelItemID, Policy[] Policies, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetModelItemPolicies", new object[] { Model, ModelItemID, Policies }, callback, asyncState);
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x0001C5B8 File Offset: 0x0001A7B8
		public void EndSetModelItemPolicies(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x0001C5C2 File Offset: 0x0001A7C2
		public void SetModelItemPoliciesAsync(string Model, string ModelItemID, Policy[] Policies)
		{
			this.SetModelItemPoliciesAsync(Model, ModelItemID, Policies, null);
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x0001C5D0 File Offset: 0x0001A7D0
		public void SetModelItemPoliciesAsync(string Model, string ModelItemID, Policy[] Policies, object userState)
		{
			if (this.SetModelItemPoliciesOperationCompleted == null)
			{
				this.SetModelItemPoliciesOperationCompleted = new SendOrPostCallback(this.OnSetModelItemPoliciesOperationCompleted);
			}
			base.InvokeAsync("SetModelItemPolicies", new object[] { Model, ModelItemID, Policies }, this.SetModelItemPoliciesOperationCompleted, userState);
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x0001C61C File Offset: 0x0001A81C
		private void OnSetModelItemPoliciesOperationCompleted(object arg)
		{
			if (this.SetModelItemPoliciesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetModelItemPoliciesCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x0001C65B File Offset: 0x0001A85B
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("BatchHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/InheritModelItemParentSecurity", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void InheritModelItemParentSecurity(string Model, string ModelItemID)
		{
			base.Invoke("InheritModelItemParentSecurity", new object[] { Model, ModelItemID });
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x0001C677 File Offset: 0x0001A877
		public IAsyncResult BeginInheritModelItemParentSecurity(string Model, string ModelItemID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("InheritModelItemParentSecurity", new object[] { Model, ModelItemID }, callback, asyncState);
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x0001C695 File Offset: 0x0001A895
		public void EndInheritModelItemParentSecurity(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x0001C69F File Offset: 0x0001A89F
		public void InheritModelItemParentSecurityAsync(string Model, string ModelItemID)
		{
			this.InheritModelItemParentSecurityAsync(Model, ModelItemID, null);
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x0001C6AA File Offset: 0x0001A8AA
		public void InheritModelItemParentSecurityAsync(string Model, string ModelItemID, object userState)
		{
			if (this.InheritModelItemParentSecurityOperationCompleted == null)
			{
				this.InheritModelItemParentSecurityOperationCompleted = new SendOrPostCallback(this.OnInheritModelItemParentSecurityOperationCompleted);
			}
			base.InvokeAsync("InheritModelItemParentSecurity", new object[] { Model, ModelItemID }, this.InheritModelItemParentSecurityOperationCompleted, userState);
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x0001C6E8 File Offset: 0x0001A8E8
		private void OnInheritModelItemParentSecurityOperationCompleted(object arg)
		{
			if (this.InheritModelItemParentSecurityCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.InheritModelItemParentSecurityCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x0001C727 File Offset: 0x0001A927
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("BatchHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/RemoveAllModelItemPolicies", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void RemoveAllModelItemPolicies(string Model)
		{
			base.Invoke("RemoveAllModelItemPolicies", new object[] { Model });
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x0001C73F File Offset: 0x0001A93F
		public IAsyncResult BeginRemoveAllModelItemPolicies(string Model, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("RemoveAllModelItemPolicies", new object[] { Model }, callback, asyncState);
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x0001C758 File Offset: 0x0001A958
		public void EndRemoveAllModelItemPolicies(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x0001C762 File Offset: 0x0001A962
		public void RemoveAllModelItemPoliciesAsync(string Model)
		{
			this.RemoveAllModelItemPoliciesAsync(Model, null);
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x0001C76C File Offset: 0x0001A96C
		public void RemoveAllModelItemPoliciesAsync(string Model, object userState)
		{
			if (this.RemoveAllModelItemPoliciesOperationCompleted == null)
			{
				this.RemoveAllModelItemPoliciesOperationCompleted = new SendOrPostCallback(this.OnRemoveAllModelItemPoliciesOperationCompleted);
			}
			base.InvokeAsync("RemoveAllModelItemPolicies", new object[] { Model }, this.RemoveAllModelItemPoliciesOperationCompleted, userState);
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x0001C7A4 File Offset: 0x0001A9A4
		private void OnRemoveAllModelItemPoliciesOperationCompleted(object arg)
		{
			if (this.RemoveAllModelItemPoliciesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.RemoveAllModelItemPoliciesCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x0001C7E3 File Offset: 0x0001A9E3
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("BatchHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/SetModelDrillthroughReports", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void SetModelDrillthroughReports(string Model, string ModelItemID, ModelDrillthroughReport[] Reports)
		{
			base.Invoke("SetModelDrillthroughReports", new object[] { Model, ModelItemID, Reports });
		}

		// Token: 0x06001189 RID: 4489 RVA: 0x0001C803 File Offset: 0x0001AA03
		public IAsyncResult BeginSetModelDrillthroughReports(string Model, string ModelItemID, ModelDrillthroughReport[] Reports, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetModelDrillthroughReports", new object[] { Model, ModelItemID, Reports }, callback, asyncState);
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x0001C826 File Offset: 0x0001AA26
		public void EndSetModelDrillthroughReports(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x0001C830 File Offset: 0x0001AA30
		public void SetModelDrillthroughReportsAsync(string Model, string ModelItemID, ModelDrillthroughReport[] Reports)
		{
			this.SetModelDrillthroughReportsAsync(Model, ModelItemID, Reports, null);
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x0001C83C File Offset: 0x0001AA3C
		public void SetModelDrillthroughReportsAsync(string Model, string ModelItemID, ModelDrillthroughReport[] Reports, object userState)
		{
			if (this.SetModelDrillthroughReportsOperationCompleted == null)
			{
				this.SetModelDrillthroughReportsOperationCompleted = new SendOrPostCallback(this.OnSetModelDrillthroughReportsOperationCompleted);
			}
			base.InvokeAsync("SetModelDrillthroughReports", new object[] { Model, ModelItemID, Reports }, this.SetModelDrillthroughReportsOperationCompleted, userState);
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x0001C888 File Offset: 0x0001AA88
		private void OnSetModelDrillthroughReportsOperationCompleted(object arg)
		{
			if (this.SetModelDrillthroughReportsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetModelDrillthroughReportsCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x0001C8C7 File Offset: 0x0001AAC7
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/ListModelDrillthroughReports", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Reports")]
		public ModelDrillthroughReport[] ListModelDrillthroughReports(string Model, string ModelItemID)
		{
			return (ModelDrillthroughReport[])base.Invoke("ListModelDrillthroughReports", new object[] { Model, ModelItemID })[0];
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x0001C8E9 File Offset: 0x0001AAE9
		public IAsyncResult BeginListModelDrillthroughReports(string Model, string ModelItemID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListModelDrillthroughReports", new object[] { Model, ModelItemID }, callback, asyncState);
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x0001C907 File Offset: 0x0001AB07
		public ModelDrillthroughReport[] EndListModelDrillthroughReports(IAsyncResult asyncResult)
		{
			return (ModelDrillthroughReport[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x0001C917 File Offset: 0x0001AB17
		public void ListModelDrillthroughReportsAsync(string Model, string ModelItemID)
		{
			this.ListModelDrillthroughReportsAsync(Model, ModelItemID, null);
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x0001C922 File Offset: 0x0001AB22
		public void ListModelDrillthroughReportsAsync(string Model, string ModelItemID, object userState)
		{
			if (this.ListModelDrillthroughReportsOperationCompleted == null)
			{
				this.ListModelDrillthroughReportsOperationCompleted = new SendOrPostCallback(this.OnListModelDrillthroughReportsOperationCompleted);
			}
			base.InvokeAsync("ListModelDrillthroughReports", new object[] { Model, ModelItemID }, this.ListModelDrillthroughReportsOperationCompleted, userState);
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x0001C960 File Offset: 0x0001AB60
		private void OnListModelDrillthroughReportsOperationCompleted(object arg)
		{
			if (this.ListModelDrillthroughReportsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListModelDrillthroughReportsCompleted(this, new ListModelDrillthroughReportsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x0001C9A5 File Offset: 0x0001ABA5
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("BatchHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/GenerateModel", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Warnings")]
		public Warning[] GenerateModel(string DataSource, string Model, string Parent, Property[] Properties)
		{
			return (Warning[])base.Invoke("GenerateModel", new object[] { DataSource, Model, Parent, Properties })[0];
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x0001C9D0 File Offset: 0x0001ABD0
		public IAsyncResult BeginGenerateModel(string DataSource, string Model, string Parent, Property[] Properties, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GenerateModel", new object[] { DataSource, Model, Parent, Properties }, callback, asyncState);
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x0001C9F8 File Offset: 0x0001ABF8
		public Warning[] EndGenerateModel(IAsyncResult asyncResult)
		{
			return (Warning[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x0001CA08 File Offset: 0x0001AC08
		public void GenerateModelAsync(string DataSource, string Model, string Parent, Property[] Properties)
		{
			this.GenerateModelAsync(DataSource, Model, Parent, Properties, null);
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x0001CA18 File Offset: 0x0001AC18
		public void GenerateModelAsync(string DataSource, string Model, string Parent, Property[] Properties, object userState)
		{
			if (this.GenerateModelOperationCompleted == null)
			{
				this.GenerateModelOperationCompleted = new SendOrPostCallback(this.OnGenerateModelOperationCompleted);
			}
			base.InvokeAsync("GenerateModel", new object[] { DataSource, Model, Parent, Properties }, this.GenerateModelOperationCompleted, userState);
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x0001CA6C File Offset: 0x0001AC6C
		private void OnGenerateModelOperationCompleted(object arg)
		{
			if (this.GenerateModelCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GenerateModelCompleted(this, new GenerateModelCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x0001CAB1 File Offset: 0x0001ACB1
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("BatchHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/RegenerateModel", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Warnings")]
		public Warning[] RegenerateModel(string Model)
		{
			return (Warning[])base.Invoke("RegenerateModel", new object[] { Model })[0];
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x0001CACF File Offset: 0x0001ACCF
		public IAsyncResult BeginRegenerateModel(string Model, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("RegenerateModel", new object[] { Model }, callback, asyncState);
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x0001CAE8 File Offset: 0x0001ACE8
		public Warning[] EndRegenerateModel(IAsyncResult asyncResult)
		{
			return (Warning[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x0001CAF8 File Offset: 0x0001ACF8
		public void RegenerateModelAsync(string Model)
		{
			this.RegenerateModelAsync(Model, null);
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x0001CB02 File Offset: 0x0001AD02
		public void RegenerateModelAsync(string Model, object userState)
		{
			if (this.RegenerateModelOperationCompleted == null)
			{
				this.RegenerateModelOperationCompleted = new SendOrPostCallback(this.OnRegenerateModelOperationCompleted);
			}
			base.InvokeAsync("RegenerateModel", new object[] { Model }, this.RegenerateModelOperationCompleted, userState);
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x0001CB3C File Offset: 0x0001AD3C
		private void OnRegenerateModelOperationCompleted(object arg)
		{
			if (this.RegenerateModelCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.RegenerateModelCompleted(this, new RegenerateModelCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060011A0 RID: 4512 RVA: 0x0001CB81 File Offset: 0x0001AD81
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/ListSecureMethods", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public string[] ListSecureMethods()
		{
			return (string[])base.Invoke("ListSecureMethods", new object[0])[0];
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x0001CB9B File Offset: 0x0001AD9B
		public IAsyncResult BeginListSecureMethods(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListSecureMethods", new object[0], callback, asyncState);
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x0001CBB0 File Offset: 0x0001ADB0
		public string[] EndListSecureMethods(IAsyncResult asyncResult)
		{
			return (string[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x0001CBC0 File Offset: 0x0001ADC0
		public void ListSecureMethodsAsync()
		{
			this.ListSecureMethodsAsync(null);
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x0001CBC9 File Offset: 0x0001ADC9
		public void ListSecureMethodsAsync(object userState)
		{
			if (this.ListSecureMethodsOperationCompleted == null)
			{
				this.ListSecureMethodsOperationCompleted = new SendOrPostCallback(this.OnListSecureMethodsOperationCompleted);
			}
			base.InvokeAsync("ListSecureMethods", new object[0], this.ListSecureMethodsOperationCompleted, userState);
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x0001CC00 File Offset: 0x0001AE00
		private void OnListSecureMethodsOperationCompleted(object arg)
		{
			if (this.ListSecureMethodsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListSecureMethodsCompleted(this, new ListSecureMethodsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x0001CC45 File Offset: 0x0001AE45
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/CreateBatch", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("BatchID")]
		public string CreateBatch()
		{
			return (string)base.Invoke("CreateBatch", new object[0])[0];
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x0001CC5F File Offset: 0x0001AE5F
		public IAsyncResult BeginCreateBatch(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateBatch", new object[0], callback, asyncState);
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x0001CC74 File Offset: 0x0001AE74
		public string EndCreateBatch(IAsyncResult asyncResult)
		{
			return (string)base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x0001CC84 File Offset: 0x0001AE84
		public void CreateBatchAsync()
		{
			this.CreateBatchAsync(null);
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x0001CC8D File Offset: 0x0001AE8D
		public void CreateBatchAsync(object userState)
		{
			if (this.CreateBatchOperationCompleted == null)
			{
				this.CreateBatchOperationCompleted = new SendOrPostCallback(this.OnCreateBatchOperationCompleted);
			}
			base.InvokeAsync("CreateBatch", new object[0], this.CreateBatchOperationCompleted, userState);
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x0001CCC4 File Offset: 0x0001AEC4
		private void OnCreateBatchOperationCompleted(object arg)
		{
			if (this.CreateBatchCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateBatchCompleted(this, new CreateBatchCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x0001CD09 File Offset: 0x0001AF09
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("BatchHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/CancelBatch", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void CancelBatch()
		{
			base.Invoke("CancelBatch", new object[0]);
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x0001CD1D File Offset: 0x0001AF1D
		public IAsyncResult BeginCancelBatch(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CancelBatch", new object[0], callback, asyncState);
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x0001CD32 File Offset: 0x0001AF32
		public void EndCancelBatch(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x0001CD3C File Offset: 0x0001AF3C
		public void CancelBatchAsync()
		{
			this.CancelBatchAsync(null);
		}

		// Token: 0x060011B0 RID: 4528 RVA: 0x0001CD45 File Offset: 0x0001AF45
		public void CancelBatchAsync(object userState)
		{
			if (this.CancelBatchOperationCompleted == null)
			{
				this.CancelBatchOperationCompleted = new SendOrPostCallback(this.OnCancelBatchOperationCompleted);
			}
			base.InvokeAsync("CancelBatch", new object[0], this.CancelBatchOperationCompleted, userState);
		}

		// Token: 0x060011B1 RID: 4529 RVA: 0x0001CD7C File Offset: 0x0001AF7C
		private void OnCancelBatchOperationCompleted(object arg)
		{
			if (this.CancelBatchCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CancelBatchCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x0001CDBB File Offset: 0x0001AFBB
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("BatchHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/ExecuteBatch", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void ExecuteBatch()
		{
			base.Invoke("ExecuteBatch", new object[0]);
		}

		// Token: 0x060011B3 RID: 4531 RVA: 0x0001CDCF File Offset: 0x0001AFCF
		public IAsyncResult BeginExecuteBatch(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ExecuteBatch", new object[0], callback, asyncState);
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x0001CDE4 File Offset: 0x0001AFE4
		public void EndExecuteBatch(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x0001CDEE File Offset: 0x0001AFEE
		public void ExecuteBatchAsync()
		{
			this.ExecuteBatchAsync(null);
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x0001CDF7 File Offset: 0x0001AFF7
		public void ExecuteBatchAsync(object userState)
		{
			if (this.ExecuteBatchOperationCompleted == null)
			{
				this.ExecuteBatchOperationCompleted = new SendOrPostCallback(this.OnExecuteBatchOperationCompleted);
			}
			base.InvokeAsync("ExecuteBatch", new object[0], this.ExecuteBatchOperationCompleted, userState);
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x0001CE2C File Offset: 0x0001B02C
		private void OnExecuteBatchOperationCompleted(object arg)
		{
			if (this.ExecuteBatchCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ExecuteBatchCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x0001CE6B File Offset: 0x0001B06B
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/GetSystemProperties", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Values")]
		public Property[] GetSystemProperties(Property[] Properties)
		{
			return (Property[])base.Invoke("GetSystemProperties", new object[] { Properties })[0];
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x0001CE89 File Offset: 0x0001B089
		public IAsyncResult BeginGetSystemProperties(Property[] Properties, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetSystemProperties", new object[] { Properties }, callback, asyncState);
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x0001CEA2 File Offset: 0x0001B0A2
		public Property[] EndGetSystemProperties(IAsyncResult asyncResult)
		{
			return (Property[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x0001CEB2 File Offset: 0x0001B0B2
		public void GetSystemPropertiesAsync(Property[] Properties)
		{
			this.GetSystemPropertiesAsync(Properties, null);
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x0001CEBC File Offset: 0x0001B0BC
		public void GetSystemPropertiesAsync(Property[] Properties, object userState)
		{
			if (this.GetSystemPropertiesOperationCompleted == null)
			{
				this.GetSystemPropertiesOperationCompleted = new SendOrPostCallback(this.OnGetSystemPropertiesOperationCompleted);
			}
			base.InvokeAsync("GetSystemProperties", new object[] { Properties }, this.GetSystemPropertiesOperationCompleted, userState);
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x0001CEF4 File Offset: 0x0001B0F4
		private void OnGetSystemPropertiesOperationCompleted(object arg)
		{
			if (this.GetSystemPropertiesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetSystemPropertiesCompleted(this, new GetSystemPropertiesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x0001CF39 File Offset: 0x0001B139
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/SetSystemProperties", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void SetSystemProperties(Property[] Properties)
		{
			base.Invoke("SetSystemProperties", new object[] { Properties });
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x0001CF51 File Offset: 0x0001B151
		public IAsyncResult BeginSetSystemProperties(Property[] Properties, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetSystemProperties", new object[] { Properties }, callback, asyncState);
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x0001CF6A File Offset: 0x0001B16A
		public void EndSetSystemProperties(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x0001CF74 File Offset: 0x0001B174
		public void SetSystemPropertiesAsync(Property[] Properties)
		{
			this.SetSystemPropertiesAsync(Properties, null);
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x0001CF7E File Offset: 0x0001B17E
		public void SetSystemPropertiesAsync(Property[] Properties, object userState)
		{
			if (this.SetSystemPropertiesOperationCompleted == null)
			{
				this.SetSystemPropertiesOperationCompleted = new SendOrPostCallback(this.OnSetSystemPropertiesOperationCompleted);
			}
			base.InvokeAsync("SetSystemProperties", new object[] { Properties }, this.SetSystemPropertiesOperationCompleted, userState);
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x0001CFB8 File Offset: 0x0001B1B8
		private void OnSetSystemPropertiesOperationCompleted(object arg)
		{
			if (this.SetSystemPropertiesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetSystemPropertiesCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x0001CFF7 File Offset: 0x0001B1F7
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("BatchHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/DeleteItem", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void DeleteItem(string Item)
		{
			base.Invoke("DeleteItem", new object[] { Item });
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x0001D00F File Offset: 0x0001B20F
		public IAsyncResult BeginDeleteItem(string Item, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("DeleteItem", new object[] { Item }, callback, asyncState);
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x0001D028 File Offset: 0x0001B228
		public void EndDeleteItem(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x0001D032 File Offset: 0x0001B232
		public void DeleteItemAsync(string Item)
		{
			this.DeleteItemAsync(Item, null);
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x0001D03C File Offset: 0x0001B23C
		public void DeleteItemAsync(string Item, object userState)
		{
			if (this.DeleteItemOperationCompleted == null)
			{
				this.DeleteItemOperationCompleted = new SendOrPostCallback(this.OnDeleteItemOperationCompleted);
			}
			base.InvokeAsync("DeleteItem", new object[] { Item }, this.DeleteItemOperationCompleted, userState);
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x0001D074 File Offset: 0x0001B274
		private void OnDeleteItemOperationCompleted(object arg)
		{
			if (this.DeleteItemCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.DeleteItemCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x0001D0B3 File Offset: 0x0001B2B3
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("BatchHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/MoveItem", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void MoveItem(string Item, string Target)
		{
			base.Invoke("MoveItem", new object[] { Item, Target });
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x0001D0CF File Offset: 0x0001B2CF
		public IAsyncResult BeginMoveItem(string Item, string Target, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("MoveItem", new object[] { Item, Target }, callback, asyncState);
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x0001D0ED File Offset: 0x0001B2ED
		public void EndMoveItem(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x0001D0F7 File Offset: 0x0001B2F7
		public void MoveItemAsync(string Item, string Target)
		{
			this.MoveItemAsync(Item, Target, null);
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x0001D102 File Offset: 0x0001B302
		public void MoveItemAsync(string Item, string Target, object userState)
		{
			if (this.MoveItemOperationCompleted == null)
			{
				this.MoveItemOperationCompleted = new SendOrPostCallback(this.OnMoveItemOperationCompleted);
			}
			base.InvokeAsync("MoveItem", new object[] { Item, Target }, this.MoveItemOperationCompleted, userState);
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x0001D140 File Offset: 0x0001B340
		private void OnMoveItemOperationCompleted(object arg)
		{
			if (this.MoveItemCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.MoveItemCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x0001D17F File Offset: 0x0001B37F
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/ListChildren", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("CatalogItems")]
		public CatalogItem[] ListChildren(string Item, bool Recursive)
		{
			return (CatalogItem[])base.Invoke("ListChildren", new object[] { Item, Recursive })[0];
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x0001D1A6 File Offset: 0x0001B3A6
		public IAsyncResult BeginListChildren(string Item, bool Recursive, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListChildren", new object[] { Item, Recursive }, callback, asyncState);
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x0001D1C9 File Offset: 0x0001B3C9
		public CatalogItem[] EndListChildren(IAsyncResult asyncResult)
		{
			return (CatalogItem[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x0001D1D9 File Offset: 0x0001B3D9
		public void ListChildrenAsync(string Item, bool Recursive)
		{
			this.ListChildrenAsync(Item, Recursive, null);
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x0001D1E4 File Offset: 0x0001B3E4
		public void ListChildrenAsync(string Item, bool Recursive, object userState)
		{
			if (this.ListChildrenOperationCompleted == null)
			{
				this.ListChildrenOperationCompleted = new SendOrPostCallback(this.OnListChildrenOperationCompleted);
			}
			base.InvokeAsync("ListChildren", new object[] { Item, Recursive }, this.ListChildrenOperationCompleted, userState);
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x0001D230 File Offset: 0x0001B430
		private void OnListChildrenOperationCompleted(object arg)
		{
			if (this.ListChildrenCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListChildrenCompleted(this, new ListChildrenCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x0001D275 File Offset: 0x0001B475
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/ListDependentItems", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("CatalogItems")]
		public CatalogItem[] ListDependentItems(string Item)
		{
			return (CatalogItem[])base.Invoke("ListDependentItems", new object[] { Item })[0];
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x0001D293 File Offset: 0x0001B493
		public IAsyncResult BeginListDependentItems(string Item, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListDependentItems", new object[] { Item }, callback, asyncState);
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x0001D2AC File Offset: 0x0001B4AC
		public CatalogItem[] EndListDependentItems(IAsyncResult asyncResult)
		{
			return (CatalogItem[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x0001D2BC File Offset: 0x0001B4BC
		public void ListDependentItemsAsync(string Item)
		{
			this.ListDependentItemsAsync(Item, null);
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x0001D2C6 File Offset: 0x0001B4C6
		public void ListDependentItemsAsync(string Item, object userState)
		{
			if (this.ListDependentItemsOperationCompleted == null)
			{
				this.ListDependentItemsOperationCompleted = new SendOrPostCallback(this.OnListDependentItemsOperationCompleted);
			}
			base.InvokeAsync("ListDependentItems", new object[] { Item }, this.ListDependentItemsOperationCompleted, userState);
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x0001D300 File Offset: 0x0001B500
		private void OnListDependentItemsOperationCompleted(object arg)
		{
			if (this.ListDependentItemsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListDependentItemsCompleted(this, new ListDependentItemsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x0001D345 File Offset: 0x0001B545
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ItemNamespaceHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/GetProperties", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Values")]
		public Property[] GetProperties(string Item, Property[] Properties)
		{
			return (Property[])base.Invoke("GetProperties", new object[] { Item, Properties })[0];
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x0001D367 File Offset: 0x0001B567
		public IAsyncResult BeginGetProperties(string Item, Property[] Properties, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetProperties", new object[] { Item, Properties }, callback, asyncState);
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x0001D385 File Offset: 0x0001B585
		public Property[] EndGetProperties(IAsyncResult asyncResult)
		{
			return (Property[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x0001D395 File Offset: 0x0001B595
		public void GetPropertiesAsync(string Item, Property[] Properties)
		{
			this.GetPropertiesAsync(Item, Properties, null);
		}

		// Token: 0x060011E0 RID: 4576 RVA: 0x0001D3A0 File Offset: 0x0001B5A0
		public void GetPropertiesAsync(string Item, Property[] Properties, object userState)
		{
			if (this.GetPropertiesOperationCompleted == null)
			{
				this.GetPropertiesOperationCompleted = new SendOrPostCallback(this.OnGetPropertiesOperationCompleted);
			}
			base.InvokeAsync("GetProperties", new object[] { Item, Properties }, this.GetPropertiesOperationCompleted, userState);
		}

		// Token: 0x060011E1 RID: 4577 RVA: 0x0001D3DC File Offset: 0x0001B5DC
		private void OnGetPropertiesOperationCompleted(object arg)
		{
			if (this.GetPropertiesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetPropertiesCompleted(this, new GetPropertiesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060011E2 RID: 4578 RVA: 0x0001D421 File Offset: 0x0001B621
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("BatchHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/SetProperties", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void SetProperties(string Item, Property[] Properties)
		{
			base.Invoke("SetProperties", new object[] { Item, Properties });
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x0001D43D File Offset: 0x0001B63D
		public IAsyncResult BeginSetProperties(string Item, Property[] Properties, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetProperties", new object[] { Item, Properties }, callback, asyncState);
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x0001D45B File Offset: 0x0001B65B
		public void EndSetProperties(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x060011E5 RID: 4581 RVA: 0x0001D465 File Offset: 0x0001B665
		public void SetPropertiesAsync(string Item, Property[] Properties)
		{
			this.SetPropertiesAsync(Item, Properties, null);
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x0001D470 File Offset: 0x0001B670
		public void SetPropertiesAsync(string Item, Property[] Properties, object userState)
		{
			if (this.SetPropertiesOperationCompleted == null)
			{
				this.SetPropertiesOperationCompleted = new SendOrPostCallback(this.OnSetPropertiesOperationCompleted);
			}
			base.InvokeAsync("SetProperties", new object[] { Item, Properties }, this.SetPropertiesOperationCompleted, userState);
		}

		// Token: 0x060011E7 RID: 4583 RVA: 0x0001D4AC File Offset: 0x0001B6AC
		private void OnSetPropertiesOperationCompleted(object arg)
		{
			if (this.SetPropertiesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetPropertiesCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060011E8 RID: 4584 RVA: 0x0001D4EB File Offset: 0x0001B6EB
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/GetItemType", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Type")]
		public ItemTypeEnum GetItemType(string Item)
		{
			return (ItemTypeEnum)base.Invoke("GetItemType", new object[] { Item })[0];
		}

		// Token: 0x060011E9 RID: 4585 RVA: 0x0001D509 File Offset: 0x0001B709
		public IAsyncResult BeginGetItemType(string Item, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetItemType", new object[] { Item }, callback, asyncState);
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x0001D522 File Offset: 0x0001B722
		public ItemTypeEnum EndGetItemType(IAsyncResult asyncResult)
		{
			return (ItemTypeEnum)base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x0001D532 File Offset: 0x0001B732
		public void GetItemTypeAsync(string Item)
		{
			this.GetItemTypeAsync(Item, null);
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x0001D53C File Offset: 0x0001B73C
		public void GetItemTypeAsync(string Item, object userState)
		{
			if (this.GetItemTypeOperationCompleted == null)
			{
				this.GetItemTypeOperationCompleted = new SendOrPostCallback(this.OnGetItemTypeOperationCompleted);
			}
			base.InvokeAsync("GetItemType", new object[] { Item }, this.GetItemTypeOperationCompleted, userState);
		}

		// Token: 0x060011ED RID: 4589 RVA: 0x0001D574 File Offset: 0x0001B774
		private void OnGetItemTypeOperationCompleted(object arg)
		{
			if (this.GetItemTypeCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetItemTypeCompleted(this, new GetItemTypeCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060011EE RID: 4590 RVA: 0x0001D5B9 File Offset: 0x0001B7B9
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("BatchHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/CreateFolder", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void CreateFolder(string Folder, string Parent, Property[] Properties)
		{
			base.Invoke("CreateFolder", new object[] { Folder, Parent, Properties });
		}

		// Token: 0x060011EF RID: 4591 RVA: 0x0001D5D9 File Offset: 0x0001B7D9
		public IAsyncResult BeginCreateFolder(string Folder, string Parent, Property[] Properties, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateFolder", new object[] { Folder, Parent, Properties }, callback, asyncState);
		}

		// Token: 0x060011F0 RID: 4592 RVA: 0x0001D5FC File Offset: 0x0001B7FC
		public void EndCreateFolder(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x060011F1 RID: 4593 RVA: 0x0001D606 File Offset: 0x0001B806
		public void CreateFolderAsync(string Folder, string Parent, Property[] Properties)
		{
			this.CreateFolderAsync(Folder, Parent, Properties, null);
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x0001D614 File Offset: 0x0001B814
		public void CreateFolderAsync(string Folder, string Parent, Property[] Properties, object userState)
		{
			if (this.CreateFolderOperationCompleted == null)
			{
				this.CreateFolderOperationCompleted = new SendOrPostCallback(this.OnCreateFolderOperationCompleted);
			}
			base.InvokeAsync("CreateFolder", new object[] { Folder, Parent, Properties }, this.CreateFolderOperationCompleted, userState);
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x0001D660 File Offset: 0x0001B860
		private void OnCreateFolderOperationCompleted(object arg)
		{
			if (this.CreateFolderCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateFolderCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060011F4 RID: 4596 RVA: 0x0001D69F File Offset: 0x0001B89F
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("BatchHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/CreateReport", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Warnings")]
		public Warning[] CreateReport(string Report, string Parent, bool Overwrite, [XmlElement(DataType = "base64Binary")] byte[] Definition, Property[] Properties)
		{
			return (Warning[])base.Invoke("CreateReport", new object[] { Report, Parent, Overwrite, Definition, Properties })[0];
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x0001D6D4 File Offset: 0x0001B8D4
		public IAsyncResult BeginCreateReport(string Report, string Parent, bool Overwrite, byte[] Definition, Property[] Properties, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateReport", new object[] { Report, Parent, Overwrite, Definition, Properties }, callback, asyncState);
		}

		// Token: 0x060011F6 RID: 4598 RVA: 0x0001D706 File Offset: 0x0001B906
		public Warning[] EndCreateReport(IAsyncResult asyncResult)
		{
			return (Warning[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x060011F7 RID: 4599 RVA: 0x0001D716 File Offset: 0x0001B916
		public void CreateReportAsync(string Report, string Parent, bool Overwrite, byte[] Definition, Property[] Properties)
		{
			this.CreateReportAsync(Report, Parent, Overwrite, Definition, Properties, null);
		}

		// Token: 0x060011F8 RID: 4600 RVA: 0x0001D728 File Offset: 0x0001B928
		public void CreateReportAsync(string Report, string Parent, bool Overwrite, byte[] Definition, Property[] Properties, object userState)
		{
			if (this.CreateReportOperationCompleted == null)
			{
				this.CreateReportOperationCompleted = new SendOrPostCallback(this.OnCreateReportOperationCompleted);
			}
			base.InvokeAsync("CreateReport", new object[] { Report, Parent, Overwrite, Definition, Properties }, this.CreateReportOperationCompleted, userState);
		}

		// Token: 0x060011F9 RID: 4601 RVA: 0x0001D784 File Offset: 0x0001B984
		private void OnCreateReportOperationCompleted(object arg)
		{
			if (this.CreateReportCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateReportCompleted(this, new CreateReportCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x0001D7C9 File Offset: 0x0001B9C9
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/GetReportDefinition", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Definition", DataType = "base64Binary")]
		public byte[] GetReportDefinition(string Report)
		{
			return (byte[])base.Invoke("GetReportDefinition", new object[] { Report })[0];
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x0001D7E7 File Offset: 0x0001B9E7
		public IAsyncResult BeginGetReportDefinition(string Report, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetReportDefinition", new object[] { Report }, callback, asyncState);
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x0001D800 File Offset: 0x0001BA00
		public byte[] EndGetReportDefinition(IAsyncResult asyncResult)
		{
			return (byte[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x0001D810 File Offset: 0x0001BA10
		public void GetReportDefinitionAsync(string Report)
		{
			this.GetReportDefinitionAsync(Report, null);
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x0001D81A File Offset: 0x0001BA1A
		public void GetReportDefinitionAsync(string Report, object userState)
		{
			if (this.GetReportDefinitionOperationCompleted == null)
			{
				this.GetReportDefinitionOperationCompleted = new SendOrPostCallback(this.OnGetReportDefinitionOperationCompleted);
			}
			base.InvokeAsync("GetReportDefinition", new object[] { Report }, this.GetReportDefinitionOperationCompleted, userState);
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x0001D854 File Offset: 0x0001BA54
		private void OnGetReportDefinitionOperationCompleted(object arg)
		{
			if (this.GetReportDefinitionCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetReportDefinitionCompleted(this, new GetReportDefinitionCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x0001D899 File Offset: 0x0001BA99
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("BatchHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/SetReportDefinition", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Warnings")]
		public Warning[] SetReportDefinition(string Report, [XmlElement(DataType = "base64Binary")] byte[] Definition)
		{
			return (Warning[])base.Invoke("SetReportDefinition", new object[] { Report, Definition })[0];
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x0001D8BB File Offset: 0x0001BABB
		public IAsyncResult BeginSetReportDefinition(string Report, byte[] Definition, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetReportDefinition", new object[] { Report, Definition }, callback, asyncState);
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x0001D8D9 File Offset: 0x0001BAD9
		public Warning[] EndSetReportDefinition(IAsyncResult asyncResult)
		{
			return (Warning[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x0001D8E9 File Offset: 0x0001BAE9
		public void SetReportDefinitionAsync(string Report, byte[] Definition)
		{
			this.SetReportDefinitionAsync(Report, Definition, null);
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x0001D8F4 File Offset: 0x0001BAF4
		public void SetReportDefinitionAsync(string Report, byte[] Definition, object userState)
		{
			if (this.SetReportDefinitionOperationCompleted == null)
			{
				this.SetReportDefinitionOperationCompleted = new SendOrPostCallback(this.OnSetReportDefinitionOperationCompleted);
			}
			base.InvokeAsync("SetReportDefinition", new object[] { Report, Definition }, this.SetReportDefinitionOperationCompleted, userState);
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x0001D930 File Offset: 0x0001BB30
		private void OnSetReportDefinitionOperationCompleted(object arg)
		{
			if (this.SetReportDefinitionCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetReportDefinitionCompleted(this, new SetReportDefinitionCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x0001D975 File Offset: 0x0001BB75
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("BatchHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/CreateResource", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void CreateResource(string Resource, string Parent, bool Overwrite, [XmlElement(DataType = "base64Binary")] byte[] Contents, string MimeType, Property[] Properties)
		{
			base.Invoke("CreateResource", new object[] { Resource, Parent, Overwrite, Contents, MimeType, Properties });
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x0001D9A9 File Offset: 0x0001BBA9
		public IAsyncResult BeginCreateResource(string Resource, string Parent, bool Overwrite, byte[] Contents, string MimeType, Property[] Properties, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateResource", new object[] { Resource, Parent, Overwrite, Contents, MimeType, Properties }, callback, asyncState);
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x0001D9E0 File Offset: 0x0001BBE0
		public void EndCreateResource(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x0001D9EA File Offset: 0x0001BBEA
		public void CreateResourceAsync(string Resource, string Parent, bool Overwrite, byte[] Contents, string MimeType, Property[] Properties)
		{
			this.CreateResourceAsync(Resource, Parent, Overwrite, Contents, MimeType, Properties, null);
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x0001D9FC File Offset: 0x0001BBFC
		public void CreateResourceAsync(string Resource, string Parent, bool Overwrite, byte[] Contents, string MimeType, Property[] Properties, object userState)
		{
			if (this.CreateResourceOperationCompleted == null)
			{
				this.CreateResourceOperationCompleted = new SendOrPostCallback(this.OnCreateResourceOperationCompleted);
			}
			base.InvokeAsync("CreateResource", new object[] { Resource, Parent, Overwrite, Contents, MimeType, Properties }, this.CreateResourceOperationCompleted, userState);
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x0001DA5C File Offset: 0x0001BC5C
		private void OnCreateResourceOperationCompleted(object arg)
		{
			if (this.CreateResourceCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateResourceCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x0001DA9B File Offset: 0x0001BC9B
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("BatchHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/SetResourceContents", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void SetResourceContents(string Resource, [XmlElement(DataType = "base64Binary")] byte[] Contents, string MimeType)
		{
			base.Invoke("SetResourceContents", new object[] { Resource, Contents, MimeType });
		}

		// Token: 0x0600120D RID: 4621 RVA: 0x0001DABB File Offset: 0x0001BCBB
		public IAsyncResult BeginSetResourceContents(string Resource, byte[] Contents, string MimeType, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetResourceContents", new object[] { Resource, Contents, MimeType }, callback, asyncState);
		}

		// Token: 0x0600120E RID: 4622 RVA: 0x0001DADE File Offset: 0x0001BCDE
		public void EndSetResourceContents(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x0001DAE8 File Offset: 0x0001BCE8
		public void SetResourceContentsAsync(string Resource, byte[] Contents, string MimeType)
		{
			this.SetResourceContentsAsync(Resource, Contents, MimeType, null);
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x0001DAF4 File Offset: 0x0001BCF4
		public void SetResourceContentsAsync(string Resource, byte[] Contents, string MimeType, object userState)
		{
			if (this.SetResourceContentsOperationCompleted == null)
			{
				this.SetResourceContentsOperationCompleted = new SendOrPostCallback(this.OnSetResourceContentsOperationCompleted);
			}
			base.InvokeAsync("SetResourceContents", new object[] { Resource, Contents, MimeType }, this.SetResourceContentsOperationCompleted, userState);
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x0001DB40 File Offset: 0x0001BD40
		private void OnSetResourceContentsOperationCompleted(object arg)
		{
			if (this.SetResourceContentsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetResourceContentsCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x0001DB80 File Offset: 0x0001BD80
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/GetResourceContents", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Contents", DataType = "base64Binary")]
		public byte[] GetResourceContents(string Resource, out string MimeType)
		{
			object[] array = base.Invoke("GetResourceContents", new object[] { Resource });
			MimeType = (string)array[1];
			return (byte[])array[0];
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x0001DBB5 File Offset: 0x0001BDB5
		public IAsyncResult BeginGetResourceContents(string Resource, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetResourceContents", new object[] { Resource }, callback, asyncState);
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x0001DBD0 File Offset: 0x0001BDD0
		public byte[] EndGetResourceContents(IAsyncResult asyncResult, out string MimeType)
		{
			object[] array = base.EndInvoke(asyncResult);
			MimeType = (string)array[1];
			return (byte[])array[0];
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x0001DBF7 File Offset: 0x0001BDF7
		public void GetResourceContentsAsync(string Resource)
		{
			this.GetResourceContentsAsync(Resource, null);
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x0001DC01 File Offset: 0x0001BE01
		public void GetResourceContentsAsync(string Resource, object userState)
		{
			if (this.GetResourceContentsOperationCompleted == null)
			{
				this.GetResourceContentsOperationCompleted = new SendOrPostCallback(this.OnGetResourceContentsOperationCompleted);
			}
			base.InvokeAsync("GetResourceContents", new object[] { Resource }, this.GetResourceContentsOperationCompleted, userState);
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x0001DC3C File Offset: 0x0001BE3C
		private void OnGetResourceContentsOperationCompleted(object arg)
		{
			if (this.GetResourceContentsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetResourceContentsCompleted(this, new GetResourceContentsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x0001DC81 File Offset: 0x0001BE81
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/CreateReportEditSession", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("EditSessionID")]
		public string CreateReportEditSession(string Report, string Parent, [XmlElement(DataType = "base64Binary")] byte[] Definition)
		{
			return (string)base.Invoke("CreateReportEditSession", new object[] { Report, Parent, Definition })[0];
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x0001DCA7 File Offset: 0x0001BEA7
		public IAsyncResult BeginCreateReportEditSession(string Report, string Parent, byte[] Definition, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateReportEditSession", new object[] { Report, Parent, Definition }, callback, asyncState);
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x0001DCCA File Offset: 0x0001BECA
		public string EndCreateReportEditSession(IAsyncResult asyncResult)
		{
			return (string)base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x0001DCDA File Offset: 0x0001BEDA
		public void CreateReportEditSessionAsync(string Report, string Parent, byte[] Definition)
		{
			this.CreateReportEditSessionAsync(Report, Parent, Definition, null);
		}

		// Token: 0x0600121C RID: 4636 RVA: 0x0001DCE8 File Offset: 0x0001BEE8
		public void CreateReportEditSessionAsync(string Report, string Parent, byte[] Definition, object userState)
		{
			if (this.CreateReportEditSessionOperationCompleted == null)
			{
				this.CreateReportEditSessionOperationCompleted = new SendOrPostCallback(this.OnCreateReportEditSessionOperationCompleted);
			}
			base.InvokeAsync("CreateReportEditSession", new object[] { Report, Parent, Definition }, this.CreateReportEditSessionOperationCompleted, userState);
		}

		// Token: 0x0600121D RID: 4637 RVA: 0x0001DD34 File Offset: 0x0001BF34
		private void OnCreateReportEditSessionOperationCompleted(object arg)
		{
			if (this.CreateReportEditSessionCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateReportEditSessionCompleted(this, new CreateReportEditSessionCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x0001DD79 File Offset: 0x0001BF79
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/GetReportParameters", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Parameters")]
		public ReportParameter[] GetReportParameters(string Report, string HistoryID, bool ForRendering, ParameterValue[] Values, DataSourceCredentials[] Credentials)
		{
			return (ReportParameter[])base.Invoke("GetReportParameters", new object[] { Report, HistoryID, ForRendering, Values, Credentials })[0];
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x0001DDAE File Offset: 0x0001BFAE
		public IAsyncResult BeginGetReportParameters(string Report, string HistoryID, bool ForRendering, ParameterValue[] Values, DataSourceCredentials[] Credentials, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetReportParameters", new object[] { Report, HistoryID, ForRendering, Values, Credentials }, callback, asyncState);
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x0001DDE0 File Offset: 0x0001BFE0
		public ReportParameter[] EndGetReportParameters(IAsyncResult asyncResult)
		{
			return (ReportParameter[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x0001DDF0 File Offset: 0x0001BFF0
		public void GetReportParametersAsync(string Report, string HistoryID, bool ForRendering, ParameterValue[] Values, DataSourceCredentials[] Credentials)
		{
			this.GetReportParametersAsync(Report, HistoryID, ForRendering, Values, Credentials, null);
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x0001DE00 File Offset: 0x0001C000
		public void GetReportParametersAsync(string Report, string HistoryID, bool ForRendering, ParameterValue[] Values, DataSourceCredentials[] Credentials, object userState)
		{
			if (this.GetReportParametersOperationCompleted == null)
			{
				this.GetReportParametersOperationCompleted = new SendOrPostCallback(this.OnGetReportParametersOperationCompleted);
			}
			base.InvokeAsync("GetReportParameters", new object[] { Report, HistoryID, ForRendering, Values, Credentials }, this.GetReportParametersOperationCompleted, userState);
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x0001DE5C File Offset: 0x0001C05C
		private void OnGetReportParametersOperationCompleted(object arg)
		{
			if (this.GetReportParametersCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetReportParametersCompleted(this, new GetReportParametersCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x0001DEA1 File Offset: 0x0001C0A1
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("BatchHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/SetReportParameters", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void SetReportParameters(string Report, ReportParameter[] Parameters)
		{
			base.Invoke("SetReportParameters", new object[] { Report, Parameters });
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x0001DEBD File Offset: 0x0001C0BD
		public IAsyncResult BeginSetReportParameters(string Report, ReportParameter[] Parameters, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetReportParameters", new object[] { Report, Parameters }, callback, asyncState);
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x0001DEDB File Offset: 0x0001C0DB
		public void EndSetReportParameters(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x0001DEE5 File Offset: 0x0001C0E5
		public void SetReportParametersAsync(string Report, ReportParameter[] Parameters)
		{
			this.SetReportParametersAsync(Report, Parameters, null);
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x0001DEF0 File Offset: 0x0001C0F0
		public void SetReportParametersAsync(string Report, ReportParameter[] Parameters, object userState)
		{
			if (this.SetReportParametersOperationCompleted == null)
			{
				this.SetReportParametersOperationCompleted = new SendOrPostCallback(this.OnSetReportParametersOperationCompleted);
			}
			base.InvokeAsync("SetReportParameters", new object[] { Report, Parameters }, this.SetReportParametersOperationCompleted, userState);
		}

		// Token: 0x06001229 RID: 4649 RVA: 0x0001DF2C File Offset: 0x0001C12C
		private void OnSetReportParametersOperationCompleted(object arg)
		{
			if (this.SetReportParametersCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetReportParametersCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x0001DF6B File Offset: 0x0001C16B
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("BatchHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/CreateLinkedReport", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void CreateLinkedReport(string Report, string Parent, string Link, Property[] Properties)
		{
			base.Invoke("CreateLinkedReport", new object[] { Report, Parent, Link, Properties });
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x0001DF90 File Offset: 0x0001C190
		public IAsyncResult BeginCreateLinkedReport(string Report, string Parent, string Link, Property[] Properties, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateLinkedReport", new object[] { Report, Parent, Link, Properties }, callback, asyncState);
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x0001DFB8 File Offset: 0x0001C1B8
		public void EndCreateLinkedReport(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x0001DFC2 File Offset: 0x0001C1C2
		public void CreateLinkedReportAsync(string Report, string Parent, string Link, Property[] Properties)
		{
			this.CreateLinkedReportAsync(Report, Parent, Link, Properties, null);
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x0001DFD0 File Offset: 0x0001C1D0
		public void CreateLinkedReportAsync(string Report, string Parent, string Link, Property[] Properties, object userState)
		{
			if (this.CreateLinkedReportOperationCompleted == null)
			{
				this.CreateLinkedReportOperationCompleted = new SendOrPostCallback(this.OnCreateLinkedReportOperationCompleted);
			}
			base.InvokeAsync("CreateLinkedReport", new object[] { Report, Parent, Link, Properties }, this.CreateLinkedReportOperationCompleted, userState);
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x0001E024 File Offset: 0x0001C224
		private void OnCreateLinkedReportOperationCompleted(object arg)
		{
			if (this.CreateLinkedReportCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateLinkedReportCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x0001E063 File Offset: 0x0001C263
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/GetReportLink", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Link")]
		public string GetReportLink(string Report)
		{
			return (string)base.Invoke("GetReportLink", new object[] { Report })[0];
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x0001E081 File Offset: 0x0001C281
		public IAsyncResult BeginGetReportLink(string Report, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetReportLink", new object[] { Report }, callback, asyncState);
		}

		// Token: 0x06001232 RID: 4658 RVA: 0x0001E09A File Offset: 0x0001C29A
		public string EndGetReportLink(IAsyncResult asyncResult)
		{
			return (string)base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x0001E0AA File Offset: 0x0001C2AA
		public void GetReportLinkAsync(string Report)
		{
			this.GetReportLinkAsync(Report, null);
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x0001E0B4 File Offset: 0x0001C2B4
		public void GetReportLinkAsync(string Report, object userState)
		{
			if (this.GetReportLinkOperationCompleted == null)
			{
				this.GetReportLinkOperationCompleted = new SendOrPostCallback(this.OnGetReportLinkOperationCompleted);
			}
			base.InvokeAsync("GetReportLink", new object[] { Report }, this.GetReportLinkOperationCompleted, userState);
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x0001E0EC File Offset: 0x0001C2EC
		private void OnGetReportLinkOperationCompleted(object arg)
		{
			if (this.GetReportLinkCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetReportLinkCompleted(this, new GetReportLinkCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x0001E131 File Offset: 0x0001C331
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("BatchHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/SetReportLink", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void SetReportLink(string Report, string Link)
		{
			base.Invoke("SetReportLink", new object[] { Report, Link });
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x0001E14D File Offset: 0x0001C34D
		public IAsyncResult BeginSetReportLink(string Report, string Link, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetReportLink", new object[] { Report, Link }, callback, asyncState);
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x0001E16B File Offset: 0x0001C36B
		public void EndSetReportLink(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x0001E175 File Offset: 0x0001C375
		public void SetReportLinkAsync(string Report, string Link)
		{
			this.SetReportLinkAsync(Report, Link, null);
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x0001E180 File Offset: 0x0001C380
		public void SetReportLinkAsync(string Report, string Link, object userState)
		{
			if (this.SetReportLinkOperationCompleted == null)
			{
				this.SetReportLinkOperationCompleted = new SendOrPostCallback(this.OnSetReportLinkOperationCompleted);
			}
			base.InvokeAsync("SetReportLink", new object[] { Report, Link }, this.SetReportLinkOperationCompleted, userState);
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x0001E1BC File Offset: 0x0001C3BC
		private void OnSetReportLinkOperationCompleted(object arg)
		{
			if (this.SetReportLinkCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetReportLinkCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x0001E1FC File Offset: 0x0001C3FC
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/GetRenderResource", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", DataType = "base64Binary")]
		public byte[] GetRenderResource(string Format, string DeviceInfo, out string MimeType)
		{
			object[] array = base.Invoke("GetRenderResource", new object[] { Format, DeviceInfo });
			MimeType = (string)array[1];
			return (byte[])array[0];
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x0001E235 File Offset: 0x0001C435
		public IAsyncResult BeginGetRenderResource(string Format, string DeviceInfo, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetRenderResource", new object[] { Format, DeviceInfo }, callback, asyncState);
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x0001E254 File Offset: 0x0001C454
		public byte[] EndGetRenderResource(IAsyncResult asyncResult, out string MimeType)
		{
			object[] array = base.EndInvoke(asyncResult);
			MimeType = (string)array[1];
			return (byte[])array[0];
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x0001E27B File Offset: 0x0001C47B
		public void GetRenderResourceAsync(string Format, string DeviceInfo)
		{
			this.GetRenderResourceAsync(Format, DeviceInfo, null);
		}

		// Token: 0x06001240 RID: 4672 RVA: 0x0001E286 File Offset: 0x0001C486
		public void GetRenderResourceAsync(string Format, string DeviceInfo, object userState)
		{
			if (this.GetRenderResourceOperationCompleted == null)
			{
				this.GetRenderResourceOperationCompleted = new SendOrPostCallback(this.OnGetRenderResourceOperationCompleted);
			}
			base.InvokeAsync("GetRenderResource", new object[] { Format, DeviceInfo }, this.GetRenderResourceOperationCompleted, userState);
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x0001E2C4 File Offset: 0x0001C4C4
		private void OnGetRenderResourceOperationCompleted(object arg)
		{
			if (this.GetRenderResourceCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetRenderResourceCompleted(this, new GetRenderResourceCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x0001E309 File Offset: 0x0001C509
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("BatchHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/SetExecutionOptions", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void SetExecutionOptions(string Report, ExecutionSettingEnum ExecutionSetting, [XmlElement("NoSchedule", typeof(NoSchedule))] [XmlElement("ScheduleDefinition", typeof(ScheduleDefinition))] [XmlElement("ScheduleReference", typeof(ScheduleReference))] ScheduleDefinitionOrReference Item)
		{
			base.Invoke("SetExecutionOptions", new object[] { Report, ExecutionSetting, Item });
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x0001E32E File Offset: 0x0001C52E
		public IAsyncResult BeginSetExecutionOptions(string Report, ExecutionSettingEnum ExecutionSetting, ScheduleDefinitionOrReference Item, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetExecutionOptions", new object[] { Report, ExecutionSetting, Item }, callback, asyncState);
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x0001E356 File Offset: 0x0001C556
		public void EndSetExecutionOptions(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x0001E360 File Offset: 0x0001C560
		public void SetExecutionOptionsAsync(string Report, ExecutionSettingEnum ExecutionSetting, ScheduleDefinitionOrReference Item)
		{
			this.SetExecutionOptionsAsync(Report, ExecutionSetting, Item, null);
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x0001E36C File Offset: 0x0001C56C
		public void SetExecutionOptionsAsync(string Report, ExecutionSettingEnum ExecutionSetting, ScheduleDefinitionOrReference Item, object userState)
		{
			if (this.SetExecutionOptionsOperationCompleted == null)
			{
				this.SetExecutionOptionsOperationCompleted = new SendOrPostCallback(this.OnSetExecutionOptionsOperationCompleted);
			}
			base.InvokeAsync("SetExecutionOptions", new object[] { Report, ExecutionSetting, Item }, this.SetExecutionOptionsOperationCompleted, userState);
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x0001E3C0 File Offset: 0x0001C5C0
		private void OnSetExecutionOptionsOperationCompleted(object arg)
		{
			if (this.SetExecutionOptionsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetExecutionOptionsCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x0001E400 File Offset: 0x0001C600
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/GetExecutionOptions", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("ExecutionSetting")]
		public ExecutionSettingEnum GetExecutionOptions(string Report, [XmlElement("NoSchedule", typeof(NoSchedule))] [XmlElement("ScheduleDefinition", typeof(ScheduleDefinition))] [XmlElement("ScheduleReference", typeof(ScheduleReference))] out ScheduleDefinitionOrReference Item)
		{
			object[] array = base.Invoke("GetExecutionOptions", new object[] { Report });
			Item = (ScheduleDefinitionOrReference)array[1];
			return (ExecutionSettingEnum)array[0];
		}

		// Token: 0x06001249 RID: 4681 RVA: 0x0001E435 File Offset: 0x0001C635
		public IAsyncResult BeginGetExecutionOptions(string Report, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetExecutionOptions", new object[] { Report }, callback, asyncState);
		}

		// Token: 0x0600124A RID: 4682 RVA: 0x0001E450 File Offset: 0x0001C650
		public ExecutionSettingEnum EndGetExecutionOptions(IAsyncResult asyncResult, out ScheduleDefinitionOrReference Item)
		{
			object[] array = base.EndInvoke(asyncResult);
			Item = (ScheduleDefinitionOrReference)array[1];
			return (ExecutionSettingEnum)array[0];
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x0001E477 File Offset: 0x0001C677
		public void GetExecutionOptionsAsync(string Report)
		{
			this.GetExecutionOptionsAsync(Report, null);
		}

		// Token: 0x0600124C RID: 4684 RVA: 0x0001E481 File Offset: 0x0001C681
		public void GetExecutionOptionsAsync(string Report, object userState)
		{
			if (this.GetExecutionOptionsOperationCompleted == null)
			{
				this.GetExecutionOptionsOperationCompleted = new SendOrPostCallback(this.OnGetExecutionOptionsOperationCompleted);
			}
			base.InvokeAsync("GetExecutionOptions", new object[] { Report }, this.GetExecutionOptionsOperationCompleted, userState);
		}

		// Token: 0x0600124D RID: 4685 RVA: 0x0001E4BC File Offset: 0x0001C6BC
		private void OnGetExecutionOptionsOperationCompleted(object arg)
		{
			if (this.GetExecutionOptionsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetExecutionOptionsCompleted(this, new GetExecutionOptionsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x0001E501 File Offset: 0x0001C701
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("BatchHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/SetCacheOptions", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void SetCacheOptions(string Report, bool CacheReport, [XmlElement("ScheduleExpiration", typeof(ScheduleExpiration))] [XmlElement("TimeExpiration", typeof(TimeExpiration))] ExpirationDefinition Item)
		{
			base.Invoke("SetCacheOptions", new object[] { Report, CacheReport, Item });
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x0001E526 File Offset: 0x0001C726
		public IAsyncResult BeginSetCacheOptions(string Report, bool CacheReport, ExpirationDefinition Item, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetCacheOptions", new object[] { Report, CacheReport, Item }, callback, asyncState);
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x0001E54E File Offset: 0x0001C74E
		public void EndSetCacheOptions(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06001251 RID: 4689 RVA: 0x0001E558 File Offset: 0x0001C758
		public void SetCacheOptionsAsync(string Report, bool CacheReport, ExpirationDefinition Item)
		{
			this.SetCacheOptionsAsync(Report, CacheReport, Item, null);
		}

		// Token: 0x06001252 RID: 4690 RVA: 0x0001E564 File Offset: 0x0001C764
		public void SetCacheOptionsAsync(string Report, bool CacheReport, ExpirationDefinition Item, object userState)
		{
			if (this.SetCacheOptionsOperationCompleted == null)
			{
				this.SetCacheOptionsOperationCompleted = new SendOrPostCallback(this.OnSetCacheOptionsOperationCompleted);
			}
			base.InvokeAsync("SetCacheOptions", new object[] { Report, CacheReport, Item }, this.SetCacheOptionsOperationCompleted, userState);
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x0001E5B8 File Offset: 0x0001C7B8
		private void OnSetCacheOptionsOperationCompleted(object arg)
		{
			if (this.SetCacheOptionsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetCacheOptionsCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x0001E5F8 File Offset: 0x0001C7F8
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/GetCacheOptions", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("CacheReport")]
		public bool GetCacheOptions(string Report, [XmlElement("ScheduleExpiration", typeof(ScheduleExpiration))] [XmlElement("TimeExpiration", typeof(TimeExpiration))] out ExpirationDefinition Item)
		{
			object[] array = base.Invoke("GetCacheOptions", new object[] { Report });
			Item = (ExpirationDefinition)array[1];
			return (bool)array[0];
		}

		// Token: 0x06001255 RID: 4693 RVA: 0x0001E62D File Offset: 0x0001C82D
		public IAsyncResult BeginGetCacheOptions(string Report, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetCacheOptions", new object[] { Report }, callback, asyncState);
		}

		// Token: 0x06001256 RID: 4694 RVA: 0x0001E648 File Offset: 0x0001C848
		public bool EndGetCacheOptions(IAsyncResult asyncResult, out ExpirationDefinition Item)
		{
			object[] array = base.EndInvoke(asyncResult);
			Item = (ExpirationDefinition)array[1];
			return (bool)array[0];
		}

		// Token: 0x06001257 RID: 4695 RVA: 0x0001E66F File Offset: 0x0001C86F
		public void GetCacheOptionsAsync(string Report)
		{
			this.GetCacheOptionsAsync(Report, null);
		}

		// Token: 0x06001258 RID: 4696 RVA: 0x0001E679 File Offset: 0x0001C879
		public void GetCacheOptionsAsync(string Report, object userState)
		{
			if (this.GetCacheOptionsOperationCompleted == null)
			{
				this.GetCacheOptionsOperationCompleted = new SendOrPostCallback(this.OnGetCacheOptionsOperationCompleted);
			}
			base.InvokeAsync("GetCacheOptions", new object[] { Report }, this.GetCacheOptionsOperationCompleted, userState);
		}

		// Token: 0x06001259 RID: 4697 RVA: 0x0001E6B4 File Offset: 0x0001C8B4
		private void OnGetCacheOptionsOperationCompleted(object arg)
		{
			if (this.GetCacheOptionsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetCacheOptionsCompleted(this, new GetCacheOptionsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600125A RID: 4698 RVA: 0x0001E6F9 File Offset: 0x0001C8F9
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("BatchHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/UpdateReportExecutionSnapshot", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void UpdateReportExecutionSnapshot(string Report)
		{
			base.Invoke("UpdateReportExecutionSnapshot", new object[] { Report });
		}

		// Token: 0x0600125B RID: 4699 RVA: 0x0001E711 File Offset: 0x0001C911
		public IAsyncResult BeginUpdateReportExecutionSnapshot(string Report, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("UpdateReportExecutionSnapshot", new object[] { Report }, callback, asyncState);
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x0001E72A File Offset: 0x0001C92A
		public void EndUpdateReportExecutionSnapshot(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x0001E734 File Offset: 0x0001C934
		public void UpdateReportExecutionSnapshotAsync(string Report)
		{
			this.UpdateReportExecutionSnapshotAsync(Report, null);
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x0001E73E File Offset: 0x0001C93E
		public void UpdateReportExecutionSnapshotAsync(string Report, object userState)
		{
			if (this.UpdateReportExecutionSnapshotOperationCompleted == null)
			{
				this.UpdateReportExecutionSnapshotOperationCompleted = new SendOrPostCallback(this.OnUpdateReportExecutionSnapshotOperationCompleted);
			}
			base.InvokeAsync("UpdateReportExecutionSnapshot", new object[] { Report }, this.UpdateReportExecutionSnapshotOperationCompleted, userState);
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x0001E778 File Offset: 0x0001C978
		private void OnUpdateReportExecutionSnapshotOperationCompleted(object arg)
		{
			if (this.UpdateReportExecutionSnapshotCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.UpdateReportExecutionSnapshotCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x0001E7B7 File Offset: 0x0001C9B7
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("BatchHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/FlushCache", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void FlushCache(string Report)
		{
			base.Invoke("FlushCache", new object[] { Report });
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x0001E7CF File Offset: 0x0001C9CF
		public IAsyncResult BeginFlushCache(string Report, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("FlushCache", new object[] { Report }, callback, asyncState);
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x0001E7E8 File Offset: 0x0001C9E8
		public void EndFlushCache(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x0001E7F2 File Offset: 0x0001C9F2
		public void FlushCacheAsync(string Report)
		{
			this.FlushCacheAsync(Report, null);
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x0001E7FC File Offset: 0x0001C9FC
		public void FlushCacheAsync(string Report, object userState)
		{
			if (this.FlushCacheOperationCompleted == null)
			{
				this.FlushCacheOperationCompleted = new SendOrPostCallback(this.OnFlushCacheOperationCompleted);
			}
			base.InvokeAsync("FlushCache", new object[] { Report }, this.FlushCacheOperationCompleted, userState);
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x0001E834 File Offset: 0x0001CA34
		private void OnFlushCacheOperationCompleted(object arg)
		{
			if (this.FlushCacheCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.FlushCacheCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x0001E873 File Offset: 0x0001CA73
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/ListJobs", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Jobs")]
		public Job[] ListJobs()
		{
			return (Job[])base.Invoke("ListJobs", new object[0])[0];
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x0001E88D File Offset: 0x0001CA8D
		public IAsyncResult BeginListJobs(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListJobs", new object[0], callback, asyncState);
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x0001E8A2 File Offset: 0x0001CAA2
		public Job[] EndListJobs(IAsyncResult asyncResult)
		{
			return (Job[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x0001E8B2 File Offset: 0x0001CAB2
		public void ListJobsAsync()
		{
			this.ListJobsAsync(null);
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x0001E8BB File Offset: 0x0001CABB
		public void ListJobsAsync(object userState)
		{
			if (this.ListJobsOperationCompleted == null)
			{
				this.ListJobsOperationCompleted = new SendOrPostCallback(this.OnListJobsOperationCompleted);
			}
			base.InvokeAsync("ListJobs", new object[0], this.ListJobsOperationCompleted, userState);
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x0001E8F0 File Offset: 0x0001CAF0
		private void OnListJobsOperationCompleted(object arg)
		{
			if (this.ListJobsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListJobsCompleted(this, new ListJobsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x0001E935 File Offset: 0x0001CB35
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/CancelJob", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public bool CancelJob(string JobID)
		{
			return (bool)base.Invoke("CancelJob", new object[] { JobID })[0];
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x0001E953 File Offset: 0x0001CB53
		public IAsyncResult BeginCancelJob(string JobID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CancelJob", new object[] { JobID }, callback, asyncState);
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x0001E96C File Offset: 0x0001CB6C
		public bool EndCancelJob(IAsyncResult asyncResult)
		{
			return (bool)base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x0001E97C File Offset: 0x0001CB7C
		public void CancelJobAsync(string JobID)
		{
			this.CancelJobAsync(JobID, null);
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x0001E986 File Offset: 0x0001CB86
		public void CancelJobAsync(string JobID, object userState)
		{
			if (this.CancelJobOperationCompleted == null)
			{
				this.CancelJobOperationCompleted = new SendOrPostCallback(this.OnCancelJobOperationCompleted);
			}
			base.InvokeAsync("CancelJob", new object[] { JobID }, this.CancelJobOperationCompleted, userState);
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x0001E9C0 File Offset: 0x0001CBC0
		private void OnCancelJobOperationCompleted(object arg)
		{
			if (this.CancelJobCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CancelJobCompleted(this, new CancelJobCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001272 RID: 4722 RVA: 0x0001EA05 File Offset: 0x0001CC05
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("BatchHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/CreateDataSource", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void CreateDataSource(string DataSource, string Parent, bool Overwrite, DataSourceDefinition Definition, Property[] Properties)
		{
			base.Invoke("CreateDataSource", new object[] { DataSource, Parent, Overwrite, Definition, Properties });
		}

		// Token: 0x06001273 RID: 4723 RVA: 0x0001EA34 File Offset: 0x0001CC34
		public IAsyncResult BeginCreateDataSource(string DataSource, string Parent, bool Overwrite, DataSourceDefinition Definition, Property[] Properties, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateDataSource", new object[] { DataSource, Parent, Overwrite, Definition, Properties }, callback, asyncState);
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x0001EA66 File Offset: 0x0001CC66
		public void EndCreateDataSource(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x0001EA70 File Offset: 0x0001CC70
		public void CreateDataSourceAsync(string DataSource, string Parent, bool Overwrite, DataSourceDefinition Definition, Property[] Properties)
		{
			this.CreateDataSourceAsync(DataSource, Parent, Overwrite, Definition, Properties, null);
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x0001EA80 File Offset: 0x0001CC80
		public void CreateDataSourceAsync(string DataSource, string Parent, bool Overwrite, DataSourceDefinition Definition, Property[] Properties, object userState)
		{
			if (this.CreateDataSourceOperationCompleted == null)
			{
				this.CreateDataSourceOperationCompleted = new SendOrPostCallback(this.OnCreateDataSourceOperationCompleted);
			}
			base.InvokeAsync("CreateDataSource", new object[] { DataSource, Parent, Overwrite, Definition, Properties }, this.CreateDataSourceOperationCompleted, userState);
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x0001EADC File Offset: 0x0001CCDC
		private void OnCreateDataSourceOperationCompleted(object arg)
		{
			if (this.CreateDataSourceCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateDataSourceCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x0001EB1B File Offset: 0x0001CD1B
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/GetDataSourceContents", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Definition")]
		public DataSourceDefinition GetDataSourceContents(string DataSource)
		{
			return (DataSourceDefinition)base.Invoke("GetDataSourceContents", new object[] { DataSource })[0];
		}

		// Token: 0x06001279 RID: 4729 RVA: 0x0001EB39 File Offset: 0x0001CD39
		public IAsyncResult BeginGetDataSourceContents(string DataSource, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetDataSourceContents", new object[] { DataSource }, callback, asyncState);
		}

		// Token: 0x0600127A RID: 4730 RVA: 0x0001EB52 File Offset: 0x0001CD52
		public DataSourceDefinition EndGetDataSourceContents(IAsyncResult asyncResult)
		{
			return (DataSourceDefinition)base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x0600127B RID: 4731 RVA: 0x0001EB62 File Offset: 0x0001CD62
		public void GetDataSourceContentsAsync(string DataSource)
		{
			this.GetDataSourceContentsAsync(DataSource, null);
		}

		// Token: 0x0600127C RID: 4732 RVA: 0x0001EB6C File Offset: 0x0001CD6C
		public void GetDataSourceContentsAsync(string DataSource, object userState)
		{
			if (this.GetDataSourceContentsOperationCompleted == null)
			{
				this.GetDataSourceContentsOperationCompleted = new SendOrPostCallback(this.OnGetDataSourceContentsOperationCompleted);
			}
			base.InvokeAsync("GetDataSourceContents", new object[] { DataSource }, this.GetDataSourceContentsOperationCompleted, userState);
		}

		// Token: 0x0600127D RID: 4733 RVA: 0x0001EBA4 File Offset: 0x0001CDA4
		private void OnGetDataSourceContentsOperationCompleted(object arg)
		{
			if (this.GetDataSourceContentsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetDataSourceContentsCompleted(this, new GetDataSourceContentsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600127E RID: 4734 RVA: 0x0001EBE9 File Offset: 0x0001CDE9
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("BatchHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/SetDataSourceContents", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void SetDataSourceContents(string DataSource, DataSourceDefinition Definition)
		{
			base.Invoke("SetDataSourceContents", new object[] { DataSource, Definition });
		}

		// Token: 0x0600127F RID: 4735 RVA: 0x0001EC05 File Offset: 0x0001CE05
		public IAsyncResult BeginSetDataSourceContents(string DataSource, DataSourceDefinition Definition, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetDataSourceContents", new object[] { DataSource, Definition }, callback, asyncState);
		}

		// Token: 0x06001280 RID: 4736 RVA: 0x0001EC23 File Offset: 0x0001CE23
		public void EndSetDataSourceContents(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06001281 RID: 4737 RVA: 0x0001EC2D File Offset: 0x0001CE2D
		public void SetDataSourceContentsAsync(string DataSource, DataSourceDefinition Definition)
		{
			this.SetDataSourceContentsAsync(DataSource, Definition, null);
		}

		// Token: 0x06001282 RID: 4738 RVA: 0x0001EC38 File Offset: 0x0001CE38
		public void SetDataSourceContentsAsync(string DataSource, DataSourceDefinition Definition, object userState)
		{
			if (this.SetDataSourceContentsOperationCompleted == null)
			{
				this.SetDataSourceContentsOperationCompleted = new SendOrPostCallback(this.OnSetDataSourceContentsOperationCompleted);
			}
			base.InvokeAsync("SetDataSourceContents", new object[] { DataSource, Definition }, this.SetDataSourceContentsOperationCompleted, userState);
		}

		// Token: 0x06001283 RID: 4739 RVA: 0x0001EC74 File Offset: 0x0001CE74
		private void OnSetDataSourceContentsOperationCompleted(object arg)
		{
			if (this.SetDataSourceContentsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetDataSourceContentsCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x0001ECB3 File Offset: 0x0001CEB3
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("BatchHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/EnableDataSource", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void EnableDataSource(string DataSource)
		{
			base.Invoke("EnableDataSource", new object[] { DataSource });
		}

		// Token: 0x06001285 RID: 4741 RVA: 0x0001ECCB File Offset: 0x0001CECB
		public IAsyncResult BeginEnableDataSource(string DataSource, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("EnableDataSource", new object[] { DataSource }, callback, asyncState);
		}

		// Token: 0x06001286 RID: 4742 RVA: 0x0001ECE4 File Offset: 0x0001CEE4
		public void EndEnableDataSource(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06001287 RID: 4743 RVA: 0x0001ECEE File Offset: 0x0001CEEE
		public void EnableDataSourceAsync(string DataSource)
		{
			this.EnableDataSourceAsync(DataSource, null);
		}

		// Token: 0x06001288 RID: 4744 RVA: 0x0001ECF8 File Offset: 0x0001CEF8
		public void EnableDataSourceAsync(string DataSource, object userState)
		{
			if (this.EnableDataSourceOperationCompleted == null)
			{
				this.EnableDataSourceOperationCompleted = new SendOrPostCallback(this.OnEnableDataSourceOperationCompleted);
			}
			base.InvokeAsync("EnableDataSource", new object[] { DataSource }, this.EnableDataSourceOperationCompleted, userState);
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x0001ED30 File Offset: 0x0001CF30
		private void OnEnableDataSourceOperationCompleted(object arg)
		{
			if (this.EnableDataSourceCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.EnableDataSourceCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x0001ED6F File Offset: 0x0001CF6F
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("BatchHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/DisableDataSource", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void DisableDataSource(string DataSource)
		{
			base.Invoke("DisableDataSource", new object[] { DataSource });
		}

		// Token: 0x0600128B RID: 4747 RVA: 0x0001ED87 File Offset: 0x0001CF87
		public IAsyncResult BeginDisableDataSource(string DataSource, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("DisableDataSource", new object[] { DataSource }, callback, asyncState);
		}

		// Token: 0x0600128C RID: 4748 RVA: 0x0001EDA0 File Offset: 0x0001CFA0
		public void EndDisableDataSource(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x0001EDAA File Offset: 0x0001CFAA
		public void DisableDataSourceAsync(string DataSource)
		{
			this.DisableDataSourceAsync(DataSource, null);
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x0001EDB4 File Offset: 0x0001CFB4
		public void DisableDataSourceAsync(string DataSource, object userState)
		{
			if (this.DisableDataSourceOperationCompleted == null)
			{
				this.DisableDataSourceOperationCompleted = new SendOrPostCallback(this.OnDisableDataSourceOperationCompleted);
			}
			base.InvokeAsync("DisableDataSource", new object[] { DataSource }, this.DisableDataSourceOperationCompleted, userState);
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x0001EDEC File Offset: 0x0001CFEC
		private void OnDisableDataSourceOperationCompleted(object arg)
		{
			if (this.DisableDataSourceCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.DisableDataSourceCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x0001EE2B File Offset: 0x0001D02B
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("BatchHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/SetItemDataSources", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void SetItemDataSources(string Item, DataSource[] DataSources)
		{
			base.Invoke("SetItemDataSources", new object[] { Item, DataSources });
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x0001EE47 File Offset: 0x0001D047
		public IAsyncResult BeginSetItemDataSources(string Item, DataSource[] DataSources, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetItemDataSources", new object[] { Item, DataSources }, callback, asyncState);
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x0001EE65 File Offset: 0x0001D065
		public void EndSetItemDataSources(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x0001EE6F File Offset: 0x0001D06F
		public void SetItemDataSourcesAsync(string Item, DataSource[] DataSources)
		{
			this.SetItemDataSourcesAsync(Item, DataSources, null);
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x0001EE7A File Offset: 0x0001D07A
		public void SetItemDataSourcesAsync(string Item, DataSource[] DataSources, object userState)
		{
			if (this.SetItemDataSourcesOperationCompleted == null)
			{
				this.SetItemDataSourcesOperationCompleted = new SendOrPostCallback(this.OnSetItemDataSourcesOperationCompleted);
			}
			base.InvokeAsync("SetItemDataSources", new object[] { Item, DataSources }, this.SetItemDataSourcesOperationCompleted, userState);
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x0001EEB8 File Offset: 0x0001D0B8
		private void OnSetItemDataSourcesOperationCompleted(object arg)
		{
			if (this.SetItemDataSourcesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetItemDataSourcesCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x0001EEF7 File Offset: 0x0001D0F7
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/GetItemDataSources", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("DataSources")]
		public DataSource[] GetItemDataSources(string Item)
		{
			return (DataSource[])base.Invoke("GetItemDataSources", new object[] { Item })[0];
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x0001EF15 File Offset: 0x0001D115
		public IAsyncResult BeginGetItemDataSources(string Item, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetItemDataSources", new object[] { Item }, callback, asyncState);
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x0001EF2E File Offset: 0x0001D12E
		public DataSource[] EndGetItemDataSources(IAsyncResult asyncResult)
		{
			return (DataSource[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x0001EF3E File Offset: 0x0001D13E
		public void GetItemDataSourcesAsync(string Item)
		{
			this.GetItemDataSourcesAsync(Item, null);
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x0001EF48 File Offset: 0x0001D148
		public void GetItemDataSourcesAsync(string Item, object userState)
		{
			if (this.GetItemDataSourcesOperationCompleted == null)
			{
				this.GetItemDataSourcesOperationCompleted = new SendOrPostCallback(this.OnGetItemDataSourcesOperationCompleted);
			}
			base.InvokeAsync("GetItemDataSources", new object[] { Item }, this.GetItemDataSourcesOperationCompleted, userState);
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x0001EF80 File Offset: 0x0001D180
		private void OnGetItemDataSourcesOperationCompleted(object arg)
		{
			if (this.GetItemDataSourcesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetItemDataSourcesCompleted(this, new GetItemDataSourcesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x0001EFC5 File Offset: 0x0001D1C5
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/GetItemDataSourcePrompts", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("DataSourcePrompts")]
		public DataSourcePrompt[] GetItemDataSourcePrompts(string Item)
		{
			return (DataSourcePrompt[])base.Invoke("GetItemDataSourcePrompts", new object[] { Item })[0];
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x0001EFE3 File Offset: 0x0001D1E3
		public IAsyncResult BeginGetItemDataSourcePrompts(string Item, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetItemDataSourcePrompts", new object[] { Item }, callback, asyncState);
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x0001EFFC File Offset: 0x0001D1FC
		public DataSourcePrompt[] EndGetItemDataSourcePrompts(IAsyncResult asyncResult)
		{
			return (DataSourcePrompt[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x0001F00C File Offset: 0x0001D20C
		public void GetItemDataSourcePromptsAsync(string Item)
		{
			this.GetItemDataSourcePromptsAsync(Item, null);
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x0001F016 File Offset: 0x0001D216
		public void GetItemDataSourcePromptsAsync(string Item, object userState)
		{
			if (this.GetItemDataSourcePromptsOperationCompleted == null)
			{
				this.GetItemDataSourcePromptsOperationCompleted = new SendOrPostCallback(this.OnGetItemDataSourcePromptsOperationCompleted);
			}
			base.InvokeAsync("GetItemDataSourcePrompts", new object[] { Item }, this.GetItemDataSourcePromptsOperationCompleted, userState);
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x0001F050 File Offset: 0x0001D250
		private void OnGetItemDataSourcePromptsOperationCompleted(object arg)
		{
			if (this.GetItemDataSourcePromptsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetItemDataSourcePromptsCompleted(this, new GetItemDataSourcePromptsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x0001F098 File Offset: 0x0001D298
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/TestConnectForDataSourceDefinition", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public bool TestConnectForDataSourceDefinition(DataSourceDefinition DataSourceDefinition, string UserName, string Password, out string ConnectError)
		{
			object[] array = base.Invoke("TestConnectForDataSourceDefinition", new object[] { DataSourceDefinition, UserName, Password });
			ConnectError = (string)array[1];
			return (bool)array[0];
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x0001F0D6 File Offset: 0x0001D2D6
		public IAsyncResult BeginTestConnectForDataSourceDefinition(DataSourceDefinition DataSourceDefinition, string UserName, string Password, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("TestConnectForDataSourceDefinition", new object[] { DataSourceDefinition, UserName, Password }, callback, asyncState);
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x0001F0FC File Offset: 0x0001D2FC
		public bool EndTestConnectForDataSourceDefinition(IAsyncResult asyncResult, out string ConnectError)
		{
			object[] array = base.EndInvoke(asyncResult);
			ConnectError = (string)array[1];
			return (bool)array[0];
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x0001F123 File Offset: 0x0001D323
		public void TestConnectForDataSourceDefinitionAsync(DataSourceDefinition DataSourceDefinition, string UserName, string Password)
		{
			this.TestConnectForDataSourceDefinitionAsync(DataSourceDefinition, UserName, Password, null);
		}

		// Token: 0x060012A6 RID: 4774 RVA: 0x0001F130 File Offset: 0x0001D330
		public void TestConnectForDataSourceDefinitionAsync(DataSourceDefinition DataSourceDefinition, string UserName, string Password, object userState)
		{
			if (this.TestConnectForDataSourceDefinitionOperationCompleted == null)
			{
				this.TestConnectForDataSourceDefinitionOperationCompleted = new SendOrPostCallback(this.OnTestConnectForDataSourceDefinitionOperationCompleted);
			}
			base.InvokeAsync("TestConnectForDataSourceDefinition", new object[] { DataSourceDefinition, UserName, Password }, this.TestConnectForDataSourceDefinitionOperationCompleted, userState);
		}

		// Token: 0x060012A7 RID: 4775 RVA: 0x0001F17C File Offset: 0x0001D37C
		private void OnTestConnectForDataSourceDefinitionOperationCompleted(object arg)
		{
			if (this.TestConnectForDataSourceDefinitionCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.TestConnectForDataSourceDefinitionCompleted(this, new TestConnectForDataSourceDefinitionCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x0001F1C4 File Offset: 0x0001D3C4
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/TestConnectForItemDataSource", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public bool TestConnectForItemDataSource(string Item, string DataSourceName, string UserName, string Password, out string ConnectError)
		{
			object[] array = base.Invoke("TestConnectForItemDataSource", new object[] { Item, DataSourceName, UserName, Password });
			ConnectError = (string)array[1];
			return (bool)array[0];
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x0001F207 File Offset: 0x0001D407
		public IAsyncResult BeginTestConnectForItemDataSource(string Item, string DataSourceName, string UserName, string Password, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("TestConnectForItemDataSource", new object[] { Item, DataSourceName, UserName, Password }, callback, asyncState);
		}

		// Token: 0x060012AA RID: 4778 RVA: 0x0001F230 File Offset: 0x0001D430
		public bool EndTestConnectForItemDataSource(IAsyncResult asyncResult, out string ConnectError)
		{
			object[] array = base.EndInvoke(asyncResult);
			ConnectError = (string)array[1];
			return (bool)array[0];
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x0001F257 File Offset: 0x0001D457
		public void TestConnectForItemDataSourceAsync(string Item, string DataSourceName, string UserName, string Password)
		{
			this.TestConnectForItemDataSourceAsync(Item, DataSourceName, UserName, Password, null);
		}

		// Token: 0x060012AC RID: 4780 RVA: 0x0001F268 File Offset: 0x0001D468
		public void TestConnectForItemDataSourceAsync(string Item, string DataSourceName, string UserName, string Password, object userState)
		{
			if (this.TestConnectForItemDataSourceOperationCompleted == null)
			{
				this.TestConnectForItemDataSourceOperationCompleted = new SendOrPostCallback(this.OnTestConnectForItemDataSourceOperationCompleted);
			}
			base.InvokeAsync("TestConnectForItemDataSource", new object[] { Item, DataSourceName, UserName, Password }, this.TestConnectForItemDataSourceOperationCompleted, userState);
		}

		// Token: 0x060012AD RID: 4781 RVA: 0x0001F2BC File Offset: 0x0001D4BC
		private void OnTestConnectForItemDataSourceOperationCompleted(object arg)
		{
			if (this.TestConnectForItemDataSourceCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.TestConnectForItemDataSourceCompleted(this, new TestConnectForItemDataSourceCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060012AE RID: 4782 RVA: 0x0001F304 File Offset: 0x0001D504
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("BatchHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/CreateReportHistorySnapshot", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("HistoryID")]
		public string CreateReportHistorySnapshot(string Report, out Warning[] Warnings)
		{
			object[] array = base.Invoke("CreateReportHistorySnapshot", new object[] { Report });
			Warnings = (Warning[])array[1];
			return (string)array[0];
		}

		// Token: 0x060012AF RID: 4783 RVA: 0x0001F339 File Offset: 0x0001D539
		public IAsyncResult BeginCreateReportHistorySnapshot(string Report, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateReportHistorySnapshot", new object[] { Report }, callback, asyncState);
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x0001F354 File Offset: 0x0001D554
		public string EndCreateReportHistorySnapshot(IAsyncResult asyncResult, out Warning[] Warnings)
		{
			object[] array = base.EndInvoke(asyncResult);
			Warnings = (Warning[])array[1];
			return (string)array[0];
		}

		// Token: 0x060012B1 RID: 4785 RVA: 0x0001F37B File Offset: 0x0001D57B
		public void CreateReportHistorySnapshotAsync(string Report)
		{
			this.CreateReportHistorySnapshotAsync(Report, null);
		}

		// Token: 0x060012B2 RID: 4786 RVA: 0x0001F385 File Offset: 0x0001D585
		public void CreateReportHistorySnapshotAsync(string Report, object userState)
		{
			if (this.CreateReportHistorySnapshotOperationCompleted == null)
			{
				this.CreateReportHistorySnapshotOperationCompleted = new SendOrPostCallback(this.OnCreateReportHistorySnapshotOperationCompleted);
			}
			base.InvokeAsync("CreateReportHistorySnapshot", new object[] { Report }, this.CreateReportHistorySnapshotOperationCompleted, userState);
		}

		// Token: 0x060012B3 RID: 4787 RVA: 0x0001F3C0 File Offset: 0x0001D5C0
		private void OnCreateReportHistorySnapshotOperationCompleted(object arg)
		{
			if (this.CreateReportHistorySnapshotCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateReportHistorySnapshotCompleted(this, new CreateReportHistorySnapshotCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060012B4 RID: 4788 RVA: 0x0001F405 File Offset: 0x0001D605
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("BatchHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/SetReportHistoryOptions", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void SetReportHistoryOptions(string Report, bool EnableManualSnapshotCreation, bool KeepExecutionSnapshots, [XmlElement("NoSchedule", typeof(NoSchedule))] [XmlElement("ScheduleDefinition", typeof(ScheduleDefinition))] [XmlElement("ScheduleReference", typeof(ScheduleReference))] ScheduleDefinitionOrReference Item)
		{
			base.Invoke("SetReportHistoryOptions", new object[] { Report, EnableManualSnapshotCreation, KeepExecutionSnapshots, Item });
		}

		// Token: 0x060012B5 RID: 4789 RVA: 0x0001F434 File Offset: 0x0001D634
		public IAsyncResult BeginSetReportHistoryOptions(string Report, bool EnableManualSnapshotCreation, bool KeepExecutionSnapshots, ScheduleDefinitionOrReference Item, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetReportHistoryOptions", new object[] { Report, EnableManualSnapshotCreation, KeepExecutionSnapshots, Item }, callback, asyncState);
		}

		// Token: 0x060012B6 RID: 4790 RVA: 0x0001F466 File Offset: 0x0001D666
		public void EndSetReportHistoryOptions(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x060012B7 RID: 4791 RVA: 0x0001F470 File Offset: 0x0001D670
		public void SetReportHistoryOptionsAsync(string Report, bool EnableManualSnapshotCreation, bool KeepExecutionSnapshots, ScheduleDefinitionOrReference Item)
		{
			this.SetReportHistoryOptionsAsync(Report, EnableManualSnapshotCreation, KeepExecutionSnapshots, Item, null);
		}

		// Token: 0x060012B8 RID: 4792 RVA: 0x0001F480 File Offset: 0x0001D680
		public void SetReportHistoryOptionsAsync(string Report, bool EnableManualSnapshotCreation, bool KeepExecutionSnapshots, ScheduleDefinitionOrReference Item, object userState)
		{
			if (this.SetReportHistoryOptionsOperationCompleted == null)
			{
				this.SetReportHistoryOptionsOperationCompleted = new SendOrPostCallback(this.OnSetReportHistoryOptionsOperationCompleted);
			}
			base.InvokeAsync("SetReportHistoryOptions", new object[] { Report, EnableManualSnapshotCreation, KeepExecutionSnapshots, Item }, this.SetReportHistoryOptionsOperationCompleted, userState);
		}

		// Token: 0x060012B9 RID: 4793 RVA: 0x0001F4DC File Offset: 0x0001D6DC
		private void OnSetReportHistoryOptionsOperationCompleted(object arg)
		{
			if (this.SetReportHistoryOptionsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetReportHistoryOptionsCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x0001F51C File Offset: 0x0001D71C
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/GetReportHistoryOptions", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("EnableManualSnapshotCreation")]
		public bool GetReportHistoryOptions(string Report, out bool KeepExecutionSnapshots, [XmlElement("NoSchedule", typeof(NoSchedule))] [XmlElement("ScheduleDefinition", typeof(ScheduleDefinition))] [XmlElement("ScheduleReference", typeof(ScheduleReference))] out ScheduleDefinitionOrReference Item)
		{
			object[] array = base.Invoke("GetReportHistoryOptions", new object[] { Report });
			KeepExecutionSnapshots = (bool)array[1];
			Item = (ScheduleDefinitionOrReference)array[2];
			return (bool)array[0];
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x0001F55B File Offset: 0x0001D75B
		public IAsyncResult BeginGetReportHistoryOptions(string Report, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetReportHistoryOptions", new object[] { Report }, callback, asyncState);
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x0001F574 File Offset: 0x0001D774
		public bool EndGetReportHistoryOptions(IAsyncResult asyncResult, out bool KeepExecutionSnapshots, out ScheduleDefinitionOrReference Item)
		{
			object[] array = base.EndInvoke(asyncResult);
			KeepExecutionSnapshots = (bool)array[1];
			Item = (ScheduleDefinitionOrReference)array[2];
			return (bool)array[0];
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x0001F5A5 File Offset: 0x0001D7A5
		public void GetReportHistoryOptionsAsync(string Report)
		{
			this.GetReportHistoryOptionsAsync(Report, null);
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x0001F5AF File Offset: 0x0001D7AF
		public void GetReportHistoryOptionsAsync(string Report, object userState)
		{
			if (this.GetReportHistoryOptionsOperationCompleted == null)
			{
				this.GetReportHistoryOptionsOperationCompleted = new SendOrPostCallback(this.OnGetReportHistoryOptionsOperationCompleted);
			}
			base.InvokeAsync("GetReportHistoryOptions", new object[] { Report }, this.GetReportHistoryOptionsOperationCompleted, userState);
		}

		// Token: 0x060012BF RID: 4799 RVA: 0x0001F5E8 File Offset: 0x0001D7E8
		private void OnGetReportHistoryOptionsOperationCompleted(object arg)
		{
			if (this.GetReportHistoryOptionsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetReportHistoryOptionsCompleted(this, new GetReportHistoryOptionsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x0001F62D File Offset: 0x0001D82D
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("BatchHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/SetReportHistoryLimit", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void SetReportHistoryLimit(string Report, bool UseSystem, int HistoryLimit)
		{
			base.Invoke("SetReportHistoryLimit", new object[] { Report, UseSystem, HistoryLimit });
		}

		// Token: 0x060012C1 RID: 4801 RVA: 0x0001F657 File Offset: 0x0001D857
		public IAsyncResult BeginSetReportHistoryLimit(string Report, bool UseSystem, int HistoryLimit, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetReportHistoryLimit", new object[] { Report, UseSystem, HistoryLimit }, callback, asyncState);
		}

		// Token: 0x060012C2 RID: 4802 RVA: 0x0001F684 File Offset: 0x0001D884
		public void EndSetReportHistoryLimit(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x060012C3 RID: 4803 RVA: 0x0001F68E File Offset: 0x0001D88E
		public void SetReportHistoryLimitAsync(string Report, bool UseSystem, int HistoryLimit)
		{
			this.SetReportHistoryLimitAsync(Report, UseSystem, HistoryLimit, null);
		}

		// Token: 0x060012C4 RID: 4804 RVA: 0x0001F69C File Offset: 0x0001D89C
		public void SetReportHistoryLimitAsync(string Report, bool UseSystem, int HistoryLimit, object userState)
		{
			if (this.SetReportHistoryLimitOperationCompleted == null)
			{
				this.SetReportHistoryLimitOperationCompleted = new SendOrPostCallback(this.OnSetReportHistoryLimitOperationCompleted);
			}
			base.InvokeAsync("SetReportHistoryLimit", new object[] { Report, UseSystem, HistoryLimit }, this.SetReportHistoryLimitOperationCompleted, userState);
		}

		// Token: 0x060012C5 RID: 4805 RVA: 0x0001F6F4 File Offset: 0x0001D8F4
		private void OnSetReportHistoryLimitOperationCompleted(object arg)
		{
			if (this.SetReportHistoryLimitCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetReportHistoryLimitCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060012C6 RID: 4806 RVA: 0x0001F734 File Offset: 0x0001D934
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/GetReportHistoryLimit", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("HistoryLimit")]
		public int GetReportHistoryLimit(string Report, out bool IsSystem, out int SystemLimit)
		{
			object[] array = base.Invoke("GetReportHistoryLimit", new object[] { Report });
			IsSystem = (bool)array[1];
			SystemLimit = (int)array[2];
			return (int)array[0];
		}

		// Token: 0x060012C7 RID: 4807 RVA: 0x0001F773 File Offset: 0x0001D973
		public IAsyncResult BeginGetReportHistoryLimit(string Report, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetReportHistoryLimit", new object[] { Report }, callback, asyncState);
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x0001F78C File Offset: 0x0001D98C
		public int EndGetReportHistoryLimit(IAsyncResult asyncResult, out bool IsSystem, out int SystemLimit)
		{
			object[] array = base.EndInvoke(asyncResult);
			IsSystem = (bool)array[1];
			SystemLimit = (int)array[2];
			return (int)array[0];
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x0001F7BD File Offset: 0x0001D9BD
		public void GetReportHistoryLimitAsync(string Report)
		{
			this.GetReportHistoryLimitAsync(Report, null);
		}

		// Token: 0x060012CA RID: 4810 RVA: 0x0001F7C7 File Offset: 0x0001D9C7
		public void GetReportHistoryLimitAsync(string Report, object userState)
		{
			if (this.GetReportHistoryLimitOperationCompleted == null)
			{
				this.GetReportHistoryLimitOperationCompleted = new SendOrPostCallback(this.OnGetReportHistoryLimitOperationCompleted);
			}
			base.InvokeAsync("GetReportHistoryLimit", new object[] { Report }, this.GetReportHistoryLimitOperationCompleted, userState);
		}

		// Token: 0x060012CB RID: 4811 RVA: 0x0001F800 File Offset: 0x0001DA00
		private void OnGetReportHistoryLimitOperationCompleted(object arg)
		{
			if (this.GetReportHistoryLimitCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetReportHistoryLimitCompleted(this, new GetReportHistoryLimitCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060012CC RID: 4812 RVA: 0x0001F845 File Offset: 0x0001DA45
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/ListReportHistory", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("ReportHistory")]
		public ReportHistorySnapshot[] ListReportHistory(string Report)
		{
			return (ReportHistorySnapshot[])base.Invoke("ListReportHistory", new object[] { Report })[0];
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x0001F863 File Offset: 0x0001DA63
		public IAsyncResult BeginListReportHistory(string Report, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListReportHistory", new object[] { Report }, callback, asyncState);
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x0001F87C File Offset: 0x0001DA7C
		public ReportHistorySnapshot[] EndListReportHistory(IAsyncResult asyncResult)
		{
			return (ReportHistorySnapshot[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x0001F88C File Offset: 0x0001DA8C
		public void ListReportHistoryAsync(string Report)
		{
			this.ListReportHistoryAsync(Report, null);
		}

		// Token: 0x060012D0 RID: 4816 RVA: 0x0001F896 File Offset: 0x0001DA96
		public void ListReportHistoryAsync(string Report, object userState)
		{
			if (this.ListReportHistoryOperationCompleted == null)
			{
				this.ListReportHistoryOperationCompleted = new SendOrPostCallback(this.OnListReportHistoryOperationCompleted);
			}
			base.InvokeAsync("ListReportHistory", new object[] { Report }, this.ListReportHistoryOperationCompleted, userState);
		}

		// Token: 0x060012D1 RID: 4817 RVA: 0x0001F8D0 File Offset: 0x0001DAD0
		private void OnListReportHistoryOperationCompleted(object arg)
		{
			if (this.ListReportHistoryCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListReportHistoryCompleted(this, new ListReportHistoryCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060012D2 RID: 4818 RVA: 0x0001F915 File Offset: 0x0001DB15
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("BatchHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/DeleteReportHistorySnapshot", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void DeleteReportHistorySnapshot(string Report, string HistoryID)
		{
			base.Invoke("DeleteReportHistorySnapshot", new object[] { Report, HistoryID });
		}

		// Token: 0x060012D3 RID: 4819 RVA: 0x0001F931 File Offset: 0x0001DB31
		public IAsyncResult BeginDeleteReportHistorySnapshot(string Report, string HistoryID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("DeleteReportHistorySnapshot", new object[] { Report, HistoryID }, callback, asyncState);
		}

		// Token: 0x060012D4 RID: 4820 RVA: 0x0001F94F File Offset: 0x0001DB4F
		public void EndDeleteReportHistorySnapshot(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x060012D5 RID: 4821 RVA: 0x0001F959 File Offset: 0x0001DB59
		public void DeleteReportHistorySnapshotAsync(string Report, string HistoryID)
		{
			this.DeleteReportHistorySnapshotAsync(Report, HistoryID, null);
		}

		// Token: 0x060012D6 RID: 4822 RVA: 0x0001F964 File Offset: 0x0001DB64
		public void DeleteReportHistorySnapshotAsync(string Report, string HistoryID, object userState)
		{
			if (this.DeleteReportHistorySnapshotOperationCompleted == null)
			{
				this.DeleteReportHistorySnapshotOperationCompleted = new SendOrPostCallback(this.OnDeleteReportHistorySnapshotOperationCompleted);
			}
			base.InvokeAsync("DeleteReportHistorySnapshot", new object[] { Report, HistoryID }, this.DeleteReportHistorySnapshotOperationCompleted, userState);
		}

		// Token: 0x060012D7 RID: 4823 RVA: 0x0001F9A0 File Offset: 0x0001DBA0
		private void OnDeleteReportHistorySnapshotOperationCompleted(object arg)
		{
			if (this.DeleteReportHistorySnapshotCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.DeleteReportHistorySnapshotCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060012D8 RID: 4824 RVA: 0x0001F9DF File Offset: 0x0001DBDF
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/FindItems", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Items")]
		public CatalogItem[] FindItems(string Folder, BooleanOperatorEnum BooleanOperator, SearchCondition[] Conditions)
		{
			return (CatalogItem[])base.Invoke("FindItems", new object[] { Folder, BooleanOperator, Conditions })[0];
		}

		// Token: 0x060012D9 RID: 4825 RVA: 0x0001FA0A File Offset: 0x0001DC0A
		public IAsyncResult BeginFindItems(string Folder, BooleanOperatorEnum BooleanOperator, SearchCondition[] Conditions, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("FindItems", new object[] { Folder, BooleanOperator, Conditions }, callback, asyncState);
		}

		// Token: 0x060012DA RID: 4826 RVA: 0x0001FA32 File Offset: 0x0001DC32
		public CatalogItem[] EndFindItems(IAsyncResult asyncResult)
		{
			return (CatalogItem[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x060012DB RID: 4827 RVA: 0x0001FA42 File Offset: 0x0001DC42
		public void FindItemsAsync(string Folder, BooleanOperatorEnum BooleanOperator, SearchCondition[] Conditions)
		{
			this.FindItemsAsync(Folder, BooleanOperator, Conditions, null);
		}

		// Token: 0x060012DC RID: 4828 RVA: 0x0001FA50 File Offset: 0x0001DC50
		public void FindItemsAsync(string Folder, BooleanOperatorEnum BooleanOperator, SearchCondition[] Conditions, object userState)
		{
			if (this.FindItemsOperationCompleted == null)
			{
				this.FindItemsOperationCompleted = new SendOrPostCallback(this.OnFindItemsOperationCompleted);
			}
			base.InvokeAsync("FindItems", new object[] { Folder, BooleanOperator, Conditions }, this.FindItemsOperationCompleted, userState);
		}

		// Token: 0x060012DD RID: 4829 RVA: 0x0001FAA4 File Offset: 0x0001DCA4
		private void OnFindItemsOperationCompleted(object arg)
		{
			if (this.FindItemsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.FindItemsCompleted(this, new FindItemsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060012DE RID: 4830 RVA: 0x0001FAE9 File Offset: 0x0001DCE9
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("BatchHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/CreateSchedule", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("ScheduleID")]
		public string CreateSchedule(string Name, ScheduleDefinition ScheduleDefinition)
		{
			return (string)base.Invoke("CreateSchedule", new object[] { Name, ScheduleDefinition })[0];
		}

		// Token: 0x060012DF RID: 4831 RVA: 0x0001FB0B File Offset: 0x0001DD0B
		public IAsyncResult BeginCreateSchedule(string Name, ScheduleDefinition ScheduleDefinition, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateSchedule", new object[] { Name, ScheduleDefinition }, callback, asyncState);
		}

		// Token: 0x060012E0 RID: 4832 RVA: 0x0001FB29 File Offset: 0x0001DD29
		public string EndCreateSchedule(IAsyncResult asyncResult)
		{
			return (string)base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x060012E1 RID: 4833 RVA: 0x0001FB39 File Offset: 0x0001DD39
		public void CreateScheduleAsync(string Name, ScheduleDefinition ScheduleDefinition)
		{
			this.CreateScheduleAsync(Name, ScheduleDefinition, null);
		}

		// Token: 0x060012E2 RID: 4834 RVA: 0x0001FB44 File Offset: 0x0001DD44
		public void CreateScheduleAsync(string Name, ScheduleDefinition ScheduleDefinition, object userState)
		{
			if (this.CreateScheduleOperationCompleted == null)
			{
				this.CreateScheduleOperationCompleted = new SendOrPostCallback(this.OnCreateScheduleOperationCompleted);
			}
			base.InvokeAsync("CreateSchedule", new object[] { Name, ScheduleDefinition }, this.CreateScheduleOperationCompleted, userState);
		}

		// Token: 0x060012E3 RID: 4835 RVA: 0x0001FB80 File Offset: 0x0001DD80
		private void OnCreateScheduleOperationCompleted(object arg)
		{
			if (this.CreateScheduleCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateScheduleCompleted(this, new CreateScheduleCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060012E4 RID: 4836 RVA: 0x0001FBC5 File Offset: 0x0001DDC5
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("BatchHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/DeleteSchedule", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void DeleteSchedule(string ScheduleID)
		{
			base.Invoke("DeleteSchedule", new object[] { ScheduleID });
		}

		// Token: 0x060012E5 RID: 4837 RVA: 0x0001FBDD File Offset: 0x0001DDDD
		public IAsyncResult BeginDeleteSchedule(string ScheduleID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("DeleteSchedule", new object[] { ScheduleID }, callback, asyncState);
		}

		// Token: 0x060012E6 RID: 4838 RVA: 0x0001FBF6 File Offset: 0x0001DDF6
		public void EndDeleteSchedule(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x0001FC00 File Offset: 0x0001DE00
		public void DeleteScheduleAsync(string ScheduleID)
		{
			this.DeleteScheduleAsync(ScheduleID, null);
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x0001FC0A File Offset: 0x0001DE0A
		public void DeleteScheduleAsync(string ScheduleID, object userState)
		{
			if (this.DeleteScheduleOperationCompleted == null)
			{
				this.DeleteScheduleOperationCompleted = new SendOrPostCallback(this.OnDeleteScheduleOperationCompleted);
			}
			base.InvokeAsync("DeleteSchedule", new object[] { ScheduleID }, this.DeleteScheduleOperationCompleted, userState);
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x0001FC44 File Offset: 0x0001DE44
		private void OnDeleteScheduleOperationCompleted(object arg)
		{
			if (this.DeleteScheduleCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.DeleteScheduleCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x0001FC83 File Offset: 0x0001DE83
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("BatchHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/SetScheduleProperties", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void SetScheduleProperties(string Name, string ScheduleID, ScheduleDefinition ScheduleDefinition)
		{
			base.Invoke("SetScheduleProperties", new object[] { Name, ScheduleID, ScheduleDefinition });
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x0001FCA3 File Offset: 0x0001DEA3
		public IAsyncResult BeginSetScheduleProperties(string Name, string ScheduleID, ScheduleDefinition ScheduleDefinition, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetScheduleProperties", new object[] { Name, ScheduleID, ScheduleDefinition }, callback, asyncState);
		}

		// Token: 0x060012EC RID: 4844 RVA: 0x0001FCC6 File Offset: 0x0001DEC6
		public void EndSetScheduleProperties(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x0001FCD0 File Offset: 0x0001DED0
		public void SetSchedulePropertiesAsync(string Name, string ScheduleID, ScheduleDefinition ScheduleDefinition)
		{
			this.SetSchedulePropertiesAsync(Name, ScheduleID, ScheduleDefinition, null);
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x0001FCDC File Offset: 0x0001DEDC
		public void SetSchedulePropertiesAsync(string Name, string ScheduleID, ScheduleDefinition ScheduleDefinition, object userState)
		{
			if (this.SetSchedulePropertiesOperationCompleted == null)
			{
				this.SetSchedulePropertiesOperationCompleted = new SendOrPostCallback(this.OnSetSchedulePropertiesOperationCompleted);
			}
			base.InvokeAsync("SetScheduleProperties", new object[] { Name, ScheduleID, ScheduleDefinition }, this.SetSchedulePropertiesOperationCompleted, userState);
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x0001FD28 File Offset: 0x0001DF28
		private void OnSetSchedulePropertiesOperationCompleted(object arg)
		{
			if (this.SetSchedulePropertiesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetSchedulePropertiesCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x0001FD67 File Offset: 0x0001DF67
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/GetScheduleProperties", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Schedule")]
		public Schedule GetScheduleProperties(string ScheduleID)
		{
			return (Schedule)base.Invoke("GetScheduleProperties", new object[] { ScheduleID })[0];
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x0001FD85 File Offset: 0x0001DF85
		public IAsyncResult BeginGetScheduleProperties(string ScheduleID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetScheduleProperties", new object[] { ScheduleID }, callback, asyncState);
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x0001FD9E File Offset: 0x0001DF9E
		public Schedule EndGetScheduleProperties(IAsyncResult asyncResult)
		{
			return (Schedule)base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x0001FDAE File Offset: 0x0001DFAE
		public void GetSchedulePropertiesAsync(string ScheduleID)
		{
			this.GetSchedulePropertiesAsync(ScheduleID, null);
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x0001FDB8 File Offset: 0x0001DFB8
		public void GetSchedulePropertiesAsync(string ScheduleID, object userState)
		{
			if (this.GetSchedulePropertiesOperationCompleted == null)
			{
				this.GetSchedulePropertiesOperationCompleted = new SendOrPostCallback(this.OnGetSchedulePropertiesOperationCompleted);
			}
			base.InvokeAsync("GetScheduleProperties", new object[] { ScheduleID }, this.GetSchedulePropertiesOperationCompleted, userState);
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x0001FDF0 File Offset: 0x0001DFF0
		private void OnGetSchedulePropertiesOperationCompleted(object arg)
		{
			if (this.GetSchedulePropertiesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetSchedulePropertiesCompleted(this, new GetSchedulePropertiesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x0001FE35 File Offset: 0x0001E035
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/ListScheduledReports", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Reports")]
		public CatalogItem[] ListScheduledReports(string ScheduleID)
		{
			return (CatalogItem[])base.Invoke("ListScheduledReports", new object[] { ScheduleID })[0];
		}

		// Token: 0x060012F7 RID: 4855 RVA: 0x0001FE53 File Offset: 0x0001E053
		public IAsyncResult BeginListScheduledReports(string ScheduleID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListScheduledReports", new object[] { ScheduleID }, callback, asyncState);
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x0001FE6C File Offset: 0x0001E06C
		public CatalogItem[] EndListScheduledReports(IAsyncResult asyncResult)
		{
			return (CatalogItem[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x0001FE7C File Offset: 0x0001E07C
		public void ListScheduledReportsAsync(string ScheduleID)
		{
			this.ListScheduledReportsAsync(ScheduleID, null);
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x0001FE86 File Offset: 0x0001E086
		public void ListScheduledReportsAsync(string ScheduleID, object userState)
		{
			if (this.ListScheduledReportsOperationCompleted == null)
			{
				this.ListScheduledReportsOperationCompleted = new SendOrPostCallback(this.OnListScheduledReportsOperationCompleted);
			}
			base.InvokeAsync("ListScheduledReports", new object[] { ScheduleID }, this.ListScheduledReportsOperationCompleted, userState);
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x0001FEC0 File Offset: 0x0001E0C0
		private void OnListScheduledReportsOperationCompleted(object arg)
		{
			if (this.ListScheduledReportsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListScheduledReportsCompleted(this, new ListScheduledReportsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x0001FF05 File Offset: 0x0001E105
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/ListSchedules", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Schedules")]
		public Schedule[] ListSchedules()
		{
			return (Schedule[])base.Invoke("ListSchedules", new object[0])[0];
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x0001FF1F File Offset: 0x0001E11F
		public IAsyncResult BeginListSchedules(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListSchedules", new object[0], callback, asyncState);
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x0001FF34 File Offset: 0x0001E134
		public Schedule[] EndListSchedules(IAsyncResult asyncResult)
		{
			return (Schedule[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x0001FF44 File Offset: 0x0001E144
		public void ListSchedulesAsync()
		{
			this.ListSchedulesAsync(null);
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x0001FF4D File Offset: 0x0001E14D
		public void ListSchedulesAsync(object userState)
		{
			if (this.ListSchedulesOperationCompleted == null)
			{
				this.ListSchedulesOperationCompleted = new SendOrPostCallback(this.OnListSchedulesOperationCompleted);
			}
			base.InvokeAsync("ListSchedules", new object[0], this.ListSchedulesOperationCompleted, userState);
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x0001FF84 File Offset: 0x0001E184
		private void OnListSchedulesOperationCompleted(object arg)
		{
			if (this.ListSchedulesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListSchedulesCompleted(this, new ListSchedulesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x0001FFC9 File Offset: 0x0001E1C9
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("BatchHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/PauseSchedule", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void PauseSchedule(string ScheduleID)
		{
			base.Invoke("PauseSchedule", new object[] { ScheduleID });
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x0001FFE1 File Offset: 0x0001E1E1
		public IAsyncResult BeginPauseSchedule(string ScheduleID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("PauseSchedule", new object[] { ScheduleID }, callback, asyncState);
		}

		// Token: 0x06001304 RID: 4868 RVA: 0x0001FFFA File Offset: 0x0001E1FA
		public void EndPauseSchedule(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06001305 RID: 4869 RVA: 0x00020004 File Offset: 0x0001E204
		public void PauseScheduleAsync(string ScheduleID)
		{
			this.PauseScheduleAsync(ScheduleID, null);
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x0002000E File Offset: 0x0001E20E
		public void PauseScheduleAsync(string ScheduleID, object userState)
		{
			if (this.PauseScheduleOperationCompleted == null)
			{
				this.PauseScheduleOperationCompleted = new SendOrPostCallback(this.OnPauseScheduleOperationCompleted);
			}
			base.InvokeAsync("PauseSchedule", new object[] { ScheduleID }, this.PauseScheduleOperationCompleted, userState);
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x00020048 File Offset: 0x0001E248
		private void OnPauseScheduleOperationCompleted(object arg)
		{
			if (this.PauseScheduleCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.PauseScheduleCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x00020087 File Offset: 0x0001E287
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("BatchHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/ResumeSchedule", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void ResumeSchedule(string ScheduleID)
		{
			base.Invoke("ResumeSchedule", new object[] { ScheduleID });
		}

		// Token: 0x06001309 RID: 4873 RVA: 0x0002009F File Offset: 0x0001E29F
		public IAsyncResult BeginResumeSchedule(string ScheduleID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ResumeSchedule", new object[] { ScheduleID }, callback, asyncState);
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x000200B8 File Offset: 0x0001E2B8
		public void EndResumeSchedule(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x0600130B RID: 4875 RVA: 0x000200C2 File Offset: 0x0001E2C2
		public void ResumeScheduleAsync(string ScheduleID)
		{
			this.ResumeScheduleAsync(ScheduleID, null);
		}

		// Token: 0x0600130C RID: 4876 RVA: 0x000200CC File Offset: 0x0001E2CC
		public void ResumeScheduleAsync(string ScheduleID, object userState)
		{
			if (this.ResumeScheduleOperationCompleted == null)
			{
				this.ResumeScheduleOperationCompleted = new SendOrPostCallback(this.OnResumeScheduleOperationCompleted);
			}
			base.InvokeAsync("ResumeSchedule", new object[] { ScheduleID }, this.ResumeScheduleOperationCompleted, userState);
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x00020104 File Offset: 0x0001E304
		private void OnResumeScheduleOperationCompleted(object arg)
		{
			if (this.ResumeScheduleCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ResumeScheduleCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600130E RID: 4878 RVA: 0x00020143 File Offset: 0x0001E343
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("BatchHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/CreateSubscription", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("SubscriptionID")]
		public string CreateSubscription(string Report, ExtensionSettings ExtensionSettings, string Description, string EventType, string MatchData, ParameterValue[] Parameters)
		{
			return (string)base.Invoke("CreateSubscription", new object[] { Report, ExtensionSettings, Description, EventType, MatchData, Parameters })[0];
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x00020178 File Offset: 0x0001E378
		public IAsyncResult BeginCreateSubscription(string Report, ExtensionSettings ExtensionSettings, string Description, string EventType, string MatchData, ParameterValue[] Parameters, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateSubscription", new object[] { Report, ExtensionSettings, Description, EventType, MatchData, Parameters }, callback, asyncState);
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x000201AA File Offset: 0x0001E3AA
		public string EndCreateSubscription(IAsyncResult asyncResult)
		{
			return (string)base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x000201BA File Offset: 0x0001E3BA
		public void CreateSubscriptionAsync(string Report, ExtensionSettings ExtensionSettings, string Description, string EventType, string MatchData, ParameterValue[] Parameters)
		{
			this.CreateSubscriptionAsync(Report, ExtensionSettings, Description, EventType, MatchData, Parameters, null);
		}

		// Token: 0x06001312 RID: 4882 RVA: 0x000201CC File Offset: 0x0001E3CC
		public void CreateSubscriptionAsync(string Report, ExtensionSettings ExtensionSettings, string Description, string EventType, string MatchData, ParameterValue[] Parameters, object userState)
		{
			if (this.CreateSubscriptionOperationCompleted == null)
			{
				this.CreateSubscriptionOperationCompleted = new SendOrPostCallback(this.OnCreateSubscriptionOperationCompleted);
			}
			base.InvokeAsync("CreateSubscription", new object[] { Report, ExtensionSettings, Description, EventType, MatchData, Parameters }, this.CreateSubscriptionOperationCompleted, userState);
		}

		// Token: 0x06001313 RID: 4883 RVA: 0x00020228 File Offset: 0x0001E428
		private void OnCreateSubscriptionOperationCompleted(object arg)
		{
			if (this.CreateSubscriptionCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateSubscriptionCompleted(this, new CreateSubscriptionCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001314 RID: 4884 RVA: 0x0002026D File Offset: 0x0001E46D
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("BatchHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/CreateDataDrivenSubscription", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("SubscriptionID")]
		public string CreateDataDrivenSubscription(string Report, ExtensionSettings ExtensionSettings, DataRetrievalPlan DataRetrievalPlan, string Description, string EventType, string MatchData, ParameterValueOrFieldReference[] Parameters)
		{
			return (string)base.Invoke("CreateDataDrivenSubscription", new object[] { Report, ExtensionSettings, DataRetrievalPlan, Description, EventType, MatchData, Parameters })[0];
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x000202A7 File Offset: 0x0001E4A7
		public IAsyncResult BeginCreateDataDrivenSubscription(string Report, ExtensionSettings ExtensionSettings, DataRetrievalPlan DataRetrievalPlan, string Description, string EventType, string MatchData, ParameterValueOrFieldReference[] Parameters, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateDataDrivenSubscription", new object[] { Report, ExtensionSettings, DataRetrievalPlan, Description, EventType, MatchData, Parameters }, callback, asyncState);
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x000202DE File Offset: 0x0001E4DE
		public string EndCreateDataDrivenSubscription(IAsyncResult asyncResult)
		{
			return (string)base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x000202F0 File Offset: 0x0001E4F0
		public void CreateDataDrivenSubscriptionAsync(string Report, ExtensionSettings ExtensionSettings, DataRetrievalPlan DataRetrievalPlan, string Description, string EventType, string MatchData, ParameterValueOrFieldReference[] Parameters)
		{
			this.CreateDataDrivenSubscriptionAsync(Report, ExtensionSettings, DataRetrievalPlan, Description, EventType, MatchData, Parameters, null);
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x00020310 File Offset: 0x0001E510
		public void CreateDataDrivenSubscriptionAsync(string Report, ExtensionSettings ExtensionSettings, DataRetrievalPlan DataRetrievalPlan, string Description, string EventType, string MatchData, ParameterValueOrFieldReference[] Parameters, object userState)
		{
			if (this.CreateDataDrivenSubscriptionOperationCompleted == null)
			{
				this.CreateDataDrivenSubscriptionOperationCompleted = new SendOrPostCallback(this.OnCreateDataDrivenSubscriptionOperationCompleted);
			}
			base.InvokeAsync("CreateDataDrivenSubscription", new object[] { Report, ExtensionSettings, DataRetrievalPlan, Description, EventType, MatchData, Parameters }, this.CreateDataDrivenSubscriptionOperationCompleted, userState);
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x00020370 File Offset: 0x0001E570
		private void OnCreateDataDrivenSubscriptionOperationCompleted(object arg)
		{
			if (this.CreateDataDrivenSubscriptionCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateDataDrivenSubscriptionCompleted(this, new CreateDataDrivenSubscriptionCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x000203B5 File Offset: 0x0001E5B5
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("BatchHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/SetSubscriptionProperties", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void SetSubscriptionProperties(string SubscriptionID, ExtensionSettings ExtensionSettings, string Description, string EventType, string MatchData, ParameterValue[] Parameters)
		{
			base.Invoke("SetSubscriptionProperties", new object[] { SubscriptionID, ExtensionSettings, Description, EventType, MatchData, Parameters });
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x000203E4 File Offset: 0x0001E5E4
		public IAsyncResult BeginSetSubscriptionProperties(string SubscriptionID, ExtensionSettings ExtensionSettings, string Description, string EventType, string MatchData, ParameterValue[] Parameters, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetSubscriptionProperties", new object[] { SubscriptionID, ExtensionSettings, Description, EventType, MatchData, Parameters }, callback, asyncState);
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x00020416 File Offset: 0x0001E616
		public void EndSetSubscriptionProperties(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x00020420 File Offset: 0x0001E620
		public void SetSubscriptionPropertiesAsync(string SubscriptionID, ExtensionSettings ExtensionSettings, string Description, string EventType, string MatchData, ParameterValue[] Parameters)
		{
			this.SetSubscriptionPropertiesAsync(SubscriptionID, ExtensionSettings, Description, EventType, MatchData, Parameters, null);
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x00020434 File Offset: 0x0001E634
		public void SetSubscriptionPropertiesAsync(string SubscriptionID, ExtensionSettings ExtensionSettings, string Description, string EventType, string MatchData, ParameterValue[] Parameters, object userState)
		{
			if (this.SetSubscriptionPropertiesOperationCompleted == null)
			{
				this.SetSubscriptionPropertiesOperationCompleted = new SendOrPostCallback(this.OnSetSubscriptionPropertiesOperationCompleted);
			}
			base.InvokeAsync("SetSubscriptionProperties", new object[] { SubscriptionID, ExtensionSettings, Description, EventType, MatchData, Parameters }, this.SetSubscriptionPropertiesOperationCompleted, userState);
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x00020490 File Offset: 0x0001E690
		private void OnSetSubscriptionPropertiesOperationCompleted(object arg)
		{
			if (this.SetSubscriptionPropertiesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetSubscriptionPropertiesCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x000204CF File Offset: 0x0001E6CF
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("BatchHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/SetDataDrivenSubscriptionProperties", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void SetDataDrivenSubscriptionProperties(string DataDrivenSubscriptionID, ExtensionSettings ExtensionSettings, DataRetrievalPlan DataRetrievalPlan, string Description, string EventType, string MatchData, ParameterValueOrFieldReference[] Parameters)
		{
			base.Invoke("SetDataDrivenSubscriptionProperties", new object[] { DataDrivenSubscriptionID, ExtensionSettings, DataRetrievalPlan, Description, EventType, MatchData, Parameters });
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x00020503 File Offset: 0x0001E703
		public IAsyncResult BeginSetDataDrivenSubscriptionProperties(string DataDrivenSubscriptionID, ExtensionSettings ExtensionSettings, DataRetrievalPlan DataRetrievalPlan, string Description, string EventType, string MatchData, ParameterValueOrFieldReference[] Parameters, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetDataDrivenSubscriptionProperties", new object[] { DataDrivenSubscriptionID, ExtensionSettings, DataRetrievalPlan, Description, EventType, MatchData, Parameters }, callback, asyncState);
		}

		// Token: 0x06001322 RID: 4898 RVA: 0x0002053A File Offset: 0x0001E73A
		public void EndSetDataDrivenSubscriptionProperties(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06001323 RID: 4899 RVA: 0x00020544 File Offset: 0x0001E744
		public void SetDataDrivenSubscriptionPropertiesAsync(string DataDrivenSubscriptionID, ExtensionSettings ExtensionSettings, DataRetrievalPlan DataRetrievalPlan, string Description, string EventType, string MatchData, ParameterValueOrFieldReference[] Parameters)
		{
			this.SetDataDrivenSubscriptionPropertiesAsync(DataDrivenSubscriptionID, ExtensionSettings, DataRetrievalPlan, Description, EventType, MatchData, Parameters, null);
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x00020564 File Offset: 0x0001E764
		public void SetDataDrivenSubscriptionPropertiesAsync(string DataDrivenSubscriptionID, ExtensionSettings ExtensionSettings, DataRetrievalPlan DataRetrievalPlan, string Description, string EventType, string MatchData, ParameterValueOrFieldReference[] Parameters, object userState)
		{
			if (this.SetDataDrivenSubscriptionPropertiesOperationCompleted == null)
			{
				this.SetDataDrivenSubscriptionPropertiesOperationCompleted = new SendOrPostCallback(this.OnSetDataDrivenSubscriptionPropertiesOperationCompleted);
			}
			base.InvokeAsync("SetDataDrivenSubscriptionProperties", new object[] { DataDrivenSubscriptionID, ExtensionSettings, DataRetrievalPlan, Description, EventType, MatchData, Parameters }, this.SetDataDrivenSubscriptionPropertiesOperationCompleted, userState);
		}

		// Token: 0x06001325 RID: 4901 RVA: 0x000205C4 File Offset: 0x0001E7C4
		private void OnSetDataDrivenSubscriptionPropertiesOperationCompleted(object arg)
		{
			if (this.SetDataDrivenSubscriptionPropertiesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetDataDrivenSubscriptionPropertiesCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x00020604 File Offset: 0x0001E804
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/GetSubscriptionProperties", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Owner")]
		public string GetSubscriptionProperties(string SubscriptionID, out ExtensionSettings ExtensionSettings, out string Description, out ActiveState Active, out string Status, out string EventType, out string MatchData, out ParameterValue[] Parameters)
		{
			object[] array = base.Invoke("GetSubscriptionProperties", new object[] { SubscriptionID });
			ExtensionSettings = (ExtensionSettings)array[1];
			Description = (string)array[2];
			Active = (ActiveState)array[3];
			Status = (string)array[4];
			EventType = (string)array[5];
			MatchData = (string)array[6];
			Parameters = (ParameterValue[])array[7];
			return (string)array[0];
		}

		// Token: 0x06001327 RID: 4903 RVA: 0x0002067A File Offset: 0x0001E87A
		public IAsyncResult BeginGetSubscriptionProperties(string SubscriptionID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetSubscriptionProperties", new object[] { SubscriptionID }, callback, asyncState);
		}

		// Token: 0x06001328 RID: 4904 RVA: 0x00020694 File Offset: 0x0001E894
		public string EndGetSubscriptionProperties(IAsyncResult asyncResult, out ExtensionSettings ExtensionSettings, out string Description, out ActiveState Active, out string Status, out string EventType, out string MatchData, out ParameterValue[] Parameters)
		{
			object[] array = base.EndInvoke(asyncResult);
			ExtensionSettings = (ExtensionSettings)array[1];
			Description = (string)array[2];
			Active = (ActiveState)array[3];
			Status = (string)array[4];
			EventType = (string)array[5];
			MatchData = (string)array[6];
			Parameters = (ParameterValue[])array[7];
			return (string)array[0];
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x000206FC File Offset: 0x0001E8FC
		public void GetSubscriptionPropertiesAsync(string SubscriptionID)
		{
			this.GetSubscriptionPropertiesAsync(SubscriptionID, null);
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x00020706 File Offset: 0x0001E906
		public void GetSubscriptionPropertiesAsync(string SubscriptionID, object userState)
		{
			if (this.GetSubscriptionPropertiesOperationCompleted == null)
			{
				this.GetSubscriptionPropertiesOperationCompleted = new SendOrPostCallback(this.OnGetSubscriptionPropertiesOperationCompleted);
			}
			base.InvokeAsync("GetSubscriptionProperties", new object[] { SubscriptionID }, this.GetSubscriptionPropertiesOperationCompleted, userState);
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x00020740 File Offset: 0x0001E940
		private void OnGetSubscriptionPropertiesOperationCompleted(object arg)
		{
			if (this.GetSubscriptionPropertiesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetSubscriptionPropertiesCompleted(this, new GetSubscriptionPropertiesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600132C RID: 4908 RVA: 0x00020788 File Offset: 0x0001E988
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/GetDataDrivenSubscriptionProperties", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Owner")]
		public string GetDataDrivenSubscriptionProperties(string DataDrivenSubscriptionID, out ExtensionSettings ExtensionSettings, out DataRetrievalPlan DataRetrievalPlan, out string Description, out ActiveState Active, out string Status, out string EventType, out string MatchData, out ParameterValueOrFieldReference[] Parameters)
		{
			object[] array = base.Invoke("GetDataDrivenSubscriptionProperties", new object[] { DataDrivenSubscriptionID });
			ExtensionSettings = (ExtensionSettings)array[1];
			DataRetrievalPlan = (DataRetrievalPlan)array[2];
			Description = (string)array[3];
			Active = (ActiveState)array[4];
			Status = (string)array[5];
			EventType = (string)array[6];
			MatchData = (string)array[7];
			Parameters = (ParameterValueOrFieldReference[])array[8];
			return (string)array[0];
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x00020809 File Offset: 0x0001EA09
		public IAsyncResult BeginGetDataDrivenSubscriptionProperties(string DataDrivenSubscriptionID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetDataDrivenSubscriptionProperties", new object[] { DataDrivenSubscriptionID }, callback, asyncState);
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x00020824 File Offset: 0x0001EA24
		public string EndGetDataDrivenSubscriptionProperties(IAsyncResult asyncResult, out ExtensionSettings ExtensionSettings, out DataRetrievalPlan DataRetrievalPlan, out string Description, out ActiveState Active, out string Status, out string EventType, out string MatchData, out ParameterValueOrFieldReference[] Parameters)
		{
			object[] array = base.EndInvoke(asyncResult);
			ExtensionSettings = (ExtensionSettings)array[1];
			DataRetrievalPlan = (DataRetrievalPlan)array[2];
			Description = (string)array[3];
			Active = (ActiveState)array[4];
			Status = (string)array[5];
			EventType = (string)array[6];
			MatchData = (string)array[7];
			Parameters = (ParameterValueOrFieldReference[])array[8];
			return (string)array[0];
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x00020897 File Offset: 0x0001EA97
		public void GetDataDrivenSubscriptionPropertiesAsync(string DataDrivenSubscriptionID)
		{
			this.GetDataDrivenSubscriptionPropertiesAsync(DataDrivenSubscriptionID, null);
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x000208A1 File Offset: 0x0001EAA1
		public void GetDataDrivenSubscriptionPropertiesAsync(string DataDrivenSubscriptionID, object userState)
		{
			if (this.GetDataDrivenSubscriptionPropertiesOperationCompleted == null)
			{
				this.GetDataDrivenSubscriptionPropertiesOperationCompleted = new SendOrPostCallback(this.OnGetDataDrivenSubscriptionPropertiesOperationCompleted);
			}
			base.InvokeAsync("GetDataDrivenSubscriptionProperties", new object[] { DataDrivenSubscriptionID }, this.GetDataDrivenSubscriptionPropertiesOperationCompleted, userState);
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x000208DC File Offset: 0x0001EADC
		private void OnGetDataDrivenSubscriptionPropertiesOperationCompleted(object arg)
		{
			if (this.GetDataDrivenSubscriptionPropertiesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetDataDrivenSubscriptionPropertiesCompleted(this, new GetDataDrivenSubscriptionPropertiesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x00020921 File Offset: 0x0001EB21
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("BatchHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/DeleteSubscription", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void DeleteSubscription(string SubscriptionID)
		{
			base.Invoke("DeleteSubscription", new object[] { SubscriptionID });
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x00020939 File Offset: 0x0001EB39
		public IAsyncResult BeginDeleteSubscription(string SubscriptionID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("DeleteSubscription", new object[] { SubscriptionID }, callback, asyncState);
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x00020952 File Offset: 0x0001EB52
		public void EndDeleteSubscription(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x0002095C File Offset: 0x0001EB5C
		public void DeleteSubscriptionAsync(string SubscriptionID)
		{
			this.DeleteSubscriptionAsync(SubscriptionID, null);
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x00020966 File Offset: 0x0001EB66
		public void DeleteSubscriptionAsync(string SubscriptionID, object userState)
		{
			if (this.DeleteSubscriptionOperationCompleted == null)
			{
				this.DeleteSubscriptionOperationCompleted = new SendOrPostCallback(this.OnDeleteSubscriptionOperationCompleted);
			}
			base.InvokeAsync("DeleteSubscription", new object[] { SubscriptionID }, this.DeleteSubscriptionOperationCompleted, userState);
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x000209A0 File Offset: 0x0001EBA0
		private void OnDeleteSubscriptionOperationCompleted(object arg)
		{
			if (this.DeleteSubscriptionCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.DeleteSubscriptionCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x000209E0 File Offset: 0x0001EBE0
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("BatchHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/PrepareQuery", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("DataSettings")]
		public DataSetDefinition PrepareQuery(DataSource DataSource, DataSetDefinition DataSet, out bool Changed, out string[] ParameterNames)
		{
			object[] array = base.Invoke("PrepareQuery", new object[] { DataSource, DataSet });
			Changed = (bool)array[1];
			ParameterNames = (string[])array[2];
			return (DataSetDefinition)array[0];
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x00020A24 File Offset: 0x0001EC24
		public IAsyncResult BeginPrepareQuery(DataSource DataSource, DataSetDefinition DataSet, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("PrepareQuery", new object[] { DataSource, DataSet }, callback, asyncState);
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x00020A44 File Offset: 0x0001EC44
		public DataSetDefinition EndPrepareQuery(IAsyncResult asyncResult, out bool Changed, out string[] ParameterNames)
		{
			object[] array = base.EndInvoke(asyncResult);
			Changed = (bool)array[1];
			ParameterNames = (string[])array[2];
			return (DataSetDefinition)array[0];
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x00020A75 File Offset: 0x0001EC75
		public void PrepareQueryAsync(DataSource DataSource, DataSetDefinition DataSet)
		{
			this.PrepareQueryAsync(DataSource, DataSet, null);
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x00020A80 File Offset: 0x0001EC80
		public void PrepareQueryAsync(DataSource DataSource, DataSetDefinition DataSet, object userState)
		{
			if (this.PrepareQueryOperationCompleted == null)
			{
				this.PrepareQueryOperationCompleted = new SendOrPostCallback(this.OnPrepareQueryOperationCompleted);
			}
			base.InvokeAsync("PrepareQuery", new object[] { DataSource, DataSet }, this.PrepareQueryOperationCompleted, userState);
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x00020ABC File Offset: 0x0001ECBC
		private void OnPrepareQueryOperationCompleted(object arg)
		{
			if (this.PrepareQueryCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.PrepareQueryCompleted(this, new PrepareQueryCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x00020B01 File Offset: 0x0001ED01
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/GetExtensionSettings", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("ExtensionParameters")]
		public ExtensionParameter[] GetExtensionSettings(string Extension)
		{
			return (ExtensionParameter[])base.Invoke("GetExtensionSettings", new object[] { Extension })[0];
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x00020B1F File Offset: 0x0001ED1F
		public IAsyncResult BeginGetExtensionSettings(string Extension, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetExtensionSettings", new object[] { Extension }, callback, asyncState);
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x00020B38 File Offset: 0x0001ED38
		public ExtensionParameter[] EndGetExtensionSettings(IAsyncResult asyncResult)
		{
			return (ExtensionParameter[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x00020B48 File Offset: 0x0001ED48
		public void GetExtensionSettingsAsync(string Extension)
		{
			this.GetExtensionSettingsAsync(Extension, null);
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x00020B52 File Offset: 0x0001ED52
		public void GetExtensionSettingsAsync(string Extension, object userState)
		{
			if (this.GetExtensionSettingsOperationCompleted == null)
			{
				this.GetExtensionSettingsOperationCompleted = new SendOrPostCallback(this.OnGetExtensionSettingsOperationCompleted);
			}
			base.InvokeAsync("GetExtensionSettings", new object[] { Extension }, this.GetExtensionSettingsOperationCompleted, userState);
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x00020B8C File Offset: 0x0001ED8C
		private void OnGetExtensionSettingsOperationCompleted(object arg)
		{
			if (this.GetExtensionSettingsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetExtensionSettingsCompleted(this, new GetExtensionSettingsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x00020BD1 File Offset: 0x0001EDD1
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/ValidateExtensionSettings", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("ParameterErrors")]
		public ExtensionParameter[] ValidateExtensionSettings(string Extension, ParameterValueOrFieldReference[] ParameterValues)
		{
			return (ExtensionParameter[])base.Invoke("ValidateExtensionSettings", new object[] { Extension, ParameterValues })[0];
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x00020BF3 File Offset: 0x0001EDF3
		public IAsyncResult BeginValidateExtensionSettings(string Extension, ParameterValueOrFieldReference[] ParameterValues, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ValidateExtensionSettings", new object[] { Extension, ParameterValues }, callback, asyncState);
		}

		// Token: 0x06001346 RID: 4934 RVA: 0x00020C11 File Offset: 0x0001EE11
		public ExtensionParameter[] EndValidateExtensionSettings(IAsyncResult asyncResult)
		{
			return (ExtensionParameter[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06001347 RID: 4935 RVA: 0x00020C21 File Offset: 0x0001EE21
		public void ValidateExtensionSettingsAsync(string Extension, ParameterValueOrFieldReference[] ParameterValues)
		{
			this.ValidateExtensionSettingsAsync(Extension, ParameterValues, null);
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x00020C2C File Offset: 0x0001EE2C
		public void ValidateExtensionSettingsAsync(string Extension, ParameterValueOrFieldReference[] ParameterValues, object userState)
		{
			if (this.ValidateExtensionSettingsOperationCompleted == null)
			{
				this.ValidateExtensionSettingsOperationCompleted = new SendOrPostCallback(this.OnValidateExtensionSettingsOperationCompleted);
			}
			base.InvokeAsync("ValidateExtensionSettings", new object[] { Extension, ParameterValues }, this.ValidateExtensionSettingsOperationCompleted, userState);
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x00020C68 File Offset: 0x0001EE68
		private void OnValidateExtensionSettingsOperationCompleted(object arg)
		{
			if (this.ValidateExtensionSettingsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ValidateExtensionSettingsCompleted(this, new ValidateExtensionSettingsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600134A RID: 4938 RVA: 0x00020CAD File Offset: 0x0001EEAD
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/ListSubscriptions", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("SubscriptionItems")]
		public Subscription[] ListSubscriptions(string Report, string Owner)
		{
			return (Subscription[])base.Invoke("ListSubscriptions", new object[] { Report, Owner })[0];
		}

		// Token: 0x0600134B RID: 4939 RVA: 0x00020CCF File Offset: 0x0001EECF
		public IAsyncResult BeginListSubscriptions(string Report, string Owner, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListSubscriptions", new object[] { Report, Owner }, callback, asyncState);
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x00020CED File Offset: 0x0001EEED
		public Subscription[] EndListSubscriptions(IAsyncResult asyncResult)
		{
			return (Subscription[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x00020CFD File Offset: 0x0001EEFD
		public void ListSubscriptionsAsync(string Report, string Owner)
		{
			this.ListSubscriptionsAsync(Report, Owner, null);
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x00020D08 File Offset: 0x0001EF08
		public void ListSubscriptionsAsync(string Report, string Owner, object userState)
		{
			if (this.ListSubscriptionsOperationCompleted == null)
			{
				this.ListSubscriptionsOperationCompleted = new SendOrPostCallback(this.OnListSubscriptionsOperationCompleted);
			}
			base.InvokeAsync("ListSubscriptions", new object[] { Report, Owner }, this.ListSubscriptionsOperationCompleted, userState);
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x00020D44 File Offset: 0x0001EF44
		private void OnListSubscriptionsOperationCompleted(object arg)
		{
			if (this.ListSubscriptionsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListSubscriptionsCompleted(this, new ListSubscriptionsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x00020D89 File Offset: 0x0001EF89
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/ListSubscriptionsUsingDataSource", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("SubscriptionItems")]
		public Subscription[] ListSubscriptionsUsingDataSource(string DataSource)
		{
			return (Subscription[])base.Invoke("ListSubscriptionsUsingDataSource", new object[] { DataSource })[0];
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x00020DA7 File Offset: 0x0001EFA7
		public IAsyncResult BeginListSubscriptionsUsingDataSource(string DataSource, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListSubscriptionsUsingDataSource", new object[] { DataSource }, callback, asyncState);
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x00020DC0 File Offset: 0x0001EFC0
		public Subscription[] EndListSubscriptionsUsingDataSource(IAsyncResult asyncResult)
		{
			return (Subscription[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x00020DD0 File Offset: 0x0001EFD0
		public void ListSubscriptionsUsingDataSourceAsync(string DataSource)
		{
			this.ListSubscriptionsUsingDataSourceAsync(DataSource, null);
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x00020DDA File Offset: 0x0001EFDA
		public void ListSubscriptionsUsingDataSourceAsync(string DataSource, object userState)
		{
			if (this.ListSubscriptionsUsingDataSourceOperationCompleted == null)
			{
				this.ListSubscriptionsUsingDataSourceOperationCompleted = new SendOrPostCallback(this.OnListSubscriptionsUsingDataSourceOperationCompleted);
			}
			base.InvokeAsync("ListSubscriptionsUsingDataSource", new object[] { DataSource }, this.ListSubscriptionsUsingDataSourceOperationCompleted, userState);
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x00020E14 File Offset: 0x0001F014
		private void OnListSubscriptionsUsingDataSourceOperationCompleted(object arg)
		{
			if (this.ListSubscriptionsUsingDataSourceCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListSubscriptionsUsingDataSourceCompleted(this, new ListSubscriptionsUsingDataSourceCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x00020E59 File Offset: 0x0001F059
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/ListExtensions", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Extensions")]
		public Extension[] ListExtensions(ExtensionTypeEnum ExtensionType)
		{
			return (Extension[])base.Invoke("ListExtensions", new object[] { ExtensionType })[0];
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x00020E7C File Offset: 0x0001F07C
		public IAsyncResult BeginListExtensions(ExtensionTypeEnum ExtensionType, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListExtensions", new object[] { ExtensionType }, callback, asyncState);
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x00020E9A File Offset: 0x0001F09A
		public Extension[] EndListExtensions(IAsyncResult asyncResult)
		{
			return (Extension[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x00020EAA File Offset: 0x0001F0AA
		public void ListExtensionsAsync(ExtensionTypeEnum ExtensionType)
		{
			this.ListExtensionsAsync(ExtensionType, null);
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x00020EB4 File Offset: 0x0001F0B4
		public void ListExtensionsAsync(ExtensionTypeEnum ExtensionType, object userState)
		{
			if (this.ListExtensionsOperationCompleted == null)
			{
				this.ListExtensionsOperationCompleted = new SendOrPostCallback(this.OnListExtensionsOperationCompleted);
			}
			base.InvokeAsync("ListExtensions", new object[] { ExtensionType }, this.ListExtensionsOperationCompleted, userState);
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x00020EF4 File Offset: 0x0001F0F4
		private void OnListExtensionsOperationCompleted(object arg)
		{
			if (this.ListExtensionsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListExtensionsCompleted(this, new ListExtensionsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x00020F39 File Offset: 0x0001F139
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/ListEvents", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Events")]
		public Event[] ListEvents()
		{
			return (Event[])base.Invoke("ListEvents", new object[0])[0];
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x00020F53 File Offset: 0x0001F153
		public IAsyncResult BeginListEvents(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListEvents", new object[0], callback, asyncState);
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x00020F68 File Offset: 0x0001F168
		public Event[] EndListEvents(IAsyncResult asyncResult)
		{
			return (Event[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x00020F78 File Offset: 0x0001F178
		public void ListEventsAsync()
		{
			this.ListEventsAsync(null);
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x00020F81 File Offset: 0x0001F181
		public void ListEventsAsync(object userState)
		{
			if (this.ListEventsOperationCompleted == null)
			{
				this.ListEventsOperationCompleted = new SendOrPostCallback(this.OnListEventsOperationCompleted);
			}
			base.InvokeAsync("ListEvents", new object[0], this.ListEventsOperationCompleted, userState);
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x00020FB8 File Offset: 0x0001F1B8
		private void OnListEventsOperationCompleted(object arg)
		{
			if (this.ListEventsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListEventsCompleted(this, new ListEventsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x00020FFD File Offset: 0x0001F1FD
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("BatchHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/FireEvent", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void FireEvent(string EventType, string EventData)
		{
			base.Invoke("FireEvent", new object[] { EventType, EventData });
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x00021019 File Offset: 0x0001F219
		public IAsyncResult BeginFireEvent(string EventType, string EventData, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("FireEvent", new object[] { EventType, EventData }, callback, asyncState);
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x00021037 File Offset: 0x0001F237
		public void EndFireEvent(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x00021041 File Offset: 0x0001F241
		public void FireEventAsync(string EventType, string EventData)
		{
			this.FireEventAsync(EventType, EventData, null);
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x0002104C File Offset: 0x0001F24C
		public void FireEventAsync(string EventType, string EventData, object userState)
		{
			if (this.FireEventOperationCompleted == null)
			{
				this.FireEventOperationCompleted = new SendOrPostCallback(this.OnFireEventOperationCompleted);
			}
			base.InvokeAsync("FireEvent", new object[] { EventType, EventData }, this.FireEventOperationCompleted, userState);
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x00021088 File Offset: 0x0001F288
		private void OnFireEventOperationCompleted(object arg)
		{
			if (this.FireEventCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.FireEventCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001368 RID: 4968 RVA: 0x000210C7 File Offset: 0x0001F2C7
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/ListTasks", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Tasks")]
		public Task[] ListTasks(SecurityScopeEnum SecurityScope)
		{
			return (Task[])base.Invoke("ListTasks", new object[] { SecurityScope })[0];
		}

		// Token: 0x06001369 RID: 4969 RVA: 0x000210EA File Offset: 0x0001F2EA
		public IAsyncResult BeginListTasks(SecurityScopeEnum SecurityScope, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListTasks", new object[] { SecurityScope }, callback, asyncState);
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x00021108 File Offset: 0x0001F308
		public Task[] EndListTasks(IAsyncResult asyncResult)
		{
			return (Task[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x00021118 File Offset: 0x0001F318
		public void ListTasksAsync(SecurityScopeEnum SecurityScope)
		{
			this.ListTasksAsync(SecurityScope, null);
		}

		// Token: 0x0600136C RID: 4972 RVA: 0x00021122 File Offset: 0x0001F322
		public void ListTasksAsync(SecurityScopeEnum SecurityScope, object userState)
		{
			if (this.ListTasksOperationCompleted == null)
			{
				this.ListTasksOperationCompleted = new SendOrPostCallback(this.OnListTasksOperationCompleted);
			}
			base.InvokeAsync("ListTasks", new object[] { SecurityScope }, this.ListTasksOperationCompleted, userState);
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x00021160 File Offset: 0x0001F360
		private void OnListTasksOperationCompleted(object arg)
		{
			if (this.ListTasksCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListTasksCompleted(this, new ListTasksCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x000211A5 File Offset: 0x0001F3A5
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/ListRoles", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Roles")]
		public Role[] ListRoles(SecurityScopeEnum SecurityScope)
		{
			return (Role[])base.Invoke("ListRoles", new object[] { SecurityScope })[0];
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x000211C8 File Offset: 0x0001F3C8
		public IAsyncResult BeginListRoles(SecurityScopeEnum SecurityScope, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListRoles", new object[] { SecurityScope }, callback, asyncState);
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x000211E6 File Offset: 0x0001F3E6
		public Role[] EndListRoles(IAsyncResult asyncResult)
		{
			return (Role[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x000211F6 File Offset: 0x0001F3F6
		public void ListRolesAsync(SecurityScopeEnum SecurityScope)
		{
			this.ListRolesAsync(SecurityScope, null);
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x00021200 File Offset: 0x0001F400
		public void ListRolesAsync(SecurityScopeEnum SecurityScope, object userState)
		{
			if (this.ListRolesOperationCompleted == null)
			{
				this.ListRolesOperationCompleted = new SendOrPostCallback(this.OnListRolesOperationCompleted);
			}
			base.InvokeAsync("ListRoles", new object[] { SecurityScope }, this.ListRolesOperationCompleted, userState);
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x00021240 File Offset: 0x0001F440
		private void OnListRolesOperationCompleted(object arg)
		{
			if (this.ListRolesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListRolesCompleted(this, new ListRolesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x00021285 File Offset: 0x0001F485
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("BatchHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/CreateRole", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void CreateRole(string Name, string Description, Task[] Tasks)
		{
			base.Invoke("CreateRole", new object[] { Name, Description, Tasks });
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x000212A5 File Offset: 0x0001F4A5
		public IAsyncResult BeginCreateRole(string Name, string Description, Task[] Tasks, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateRole", new object[] { Name, Description, Tasks }, callback, asyncState);
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x000212C8 File Offset: 0x0001F4C8
		public void EndCreateRole(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x000212D2 File Offset: 0x0001F4D2
		public void CreateRoleAsync(string Name, string Description, Task[] Tasks)
		{
			this.CreateRoleAsync(Name, Description, Tasks, null);
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x000212E0 File Offset: 0x0001F4E0
		public void CreateRoleAsync(string Name, string Description, Task[] Tasks, object userState)
		{
			if (this.CreateRoleOperationCompleted == null)
			{
				this.CreateRoleOperationCompleted = new SendOrPostCallback(this.OnCreateRoleOperationCompleted);
			}
			base.InvokeAsync("CreateRole", new object[] { Name, Description, Tasks }, this.CreateRoleOperationCompleted, userState);
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x0002132C File Offset: 0x0001F52C
		private void OnCreateRoleOperationCompleted(object arg)
		{
			if (this.CreateRoleCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateRoleCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x0002136B File Offset: 0x0001F56B
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("BatchHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/DeleteRole", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void DeleteRole(string Name)
		{
			base.Invoke("DeleteRole", new object[] { Name });
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x00021383 File Offset: 0x0001F583
		public IAsyncResult BeginDeleteRole(string Name, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("DeleteRole", new object[] { Name }, callback, asyncState);
		}

		// Token: 0x0600137C RID: 4988 RVA: 0x0002139C File Offset: 0x0001F59C
		public void EndDeleteRole(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x000213A6 File Offset: 0x0001F5A6
		public void DeleteRoleAsync(string Name)
		{
			this.DeleteRoleAsync(Name, null);
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x000213B0 File Offset: 0x0001F5B0
		public void DeleteRoleAsync(string Name, object userState)
		{
			if (this.DeleteRoleOperationCompleted == null)
			{
				this.DeleteRoleOperationCompleted = new SendOrPostCallback(this.OnDeleteRoleOperationCompleted);
			}
			base.InvokeAsync("DeleteRole", new object[] { Name }, this.DeleteRoleOperationCompleted, userState);
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x000213E8 File Offset: 0x0001F5E8
		private void OnDeleteRoleOperationCompleted(object arg)
		{
			if (this.DeleteRoleCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.DeleteRoleCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x00021428 File Offset: 0x0001F628
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/GetRoleProperties", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Tasks")]
		public Task[] GetRoleProperties(string Name, out string Description)
		{
			object[] array = base.Invoke("GetRoleProperties", new object[] { Name });
			Description = (string)array[1];
			return (Task[])array[0];
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x0002145D File Offset: 0x0001F65D
		public IAsyncResult BeginGetRoleProperties(string Name, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetRoleProperties", new object[] { Name }, callback, asyncState);
		}

		// Token: 0x06001382 RID: 4994 RVA: 0x00021478 File Offset: 0x0001F678
		public Task[] EndGetRoleProperties(IAsyncResult asyncResult, out string Description)
		{
			object[] array = base.EndInvoke(asyncResult);
			Description = (string)array[1];
			return (Task[])array[0];
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x0002149F File Offset: 0x0001F69F
		public void GetRolePropertiesAsync(string Name)
		{
			this.GetRolePropertiesAsync(Name, null);
		}

		// Token: 0x06001384 RID: 4996 RVA: 0x000214A9 File Offset: 0x0001F6A9
		public void GetRolePropertiesAsync(string Name, object userState)
		{
			if (this.GetRolePropertiesOperationCompleted == null)
			{
				this.GetRolePropertiesOperationCompleted = new SendOrPostCallback(this.OnGetRolePropertiesOperationCompleted);
			}
			base.InvokeAsync("GetRoleProperties", new object[] { Name }, this.GetRolePropertiesOperationCompleted, userState);
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x000214E4 File Offset: 0x0001F6E4
		private void OnGetRolePropertiesOperationCompleted(object arg)
		{
			if (this.GetRolePropertiesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetRolePropertiesCompleted(this, new GetRolePropertiesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x00021529 File Offset: 0x0001F729
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("BatchHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/SetRoleProperties", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void SetRoleProperties(string Name, string Description, Task[] Tasks)
		{
			base.Invoke("SetRoleProperties", new object[] { Name, Description, Tasks });
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x00021549 File Offset: 0x0001F749
		public IAsyncResult BeginSetRoleProperties(string Name, string Description, Task[] Tasks, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetRoleProperties", new object[] { Name, Description, Tasks }, callback, asyncState);
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x0002156C File Offset: 0x0001F76C
		public void EndSetRoleProperties(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x00021576 File Offset: 0x0001F776
		public void SetRolePropertiesAsync(string Name, string Description, Task[] Tasks)
		{
			this.SetRolePropertiesAsync(Name, Description, Tasks, null);
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x00021584 File Offset: 0x0001F784
		public void SetRolePropertiesAsync(string Name, string Description, Task[] Tasks, object userState)
		{
			if (this.SetRolePropertiesOperationCompleted == null)
			{
				this.SetRolePropertiesOperationCompleted = new SendOrPostCallback(this.OnSetRolePropertiesOperationCompleted);
			}
			base.InvokeAsync("SetRoleProperties", new object[] { Name, Description, Tasks }, this.SetRolePropertiesOperationCompleted, userState);
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x000215D0 File Offset: 0x0001F7D0
		private void OnSetRolePropertiesOperationCompleted(object arg)
		{
			if (this.SetRolePropertiesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetRolePropertiesCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x0002160F File Offset: 0x0001F80F
		public new void CancelAsync(object userState)
		{
			base.CancelAsync(userState);
		}

		// Token: 0x040004BD RID: 1213
		private ServerInfoHeader serverInfoHeaderValueField;

		// Token: 0x040004BE RID: 1214
		private SendOrPostCallback GetSystemPoliciesOperationCompleted;

		// Token: 0x040004BF RID: 1215
		private BatchHeader batchHeaderValueField;

		// Token: 0x040004C0 RID: 1216
		private SendOrPostCallback SetSystemPoliciesOperationCompleted;

		// Token: 0x040004C1 RID: 1217
		private SendOrPostCallback GetPoliciesOperationCompleted;

		// Token: 0x040004C2 RID: 1218
		private SendOrPostCallback SetPoliciesOperationCompleted;

		// Token: 0x040004C3 RID: 1219
		private SendOrPostCallback InheritParentSecurityOperationCompleted;

		// Token: 0x040004C4 RID: 1220
		private SendOrPostCallback GetSystemPermissionsOperationCompleted;

		// Token: 0x040004C5 RID: 1221
		private SendOrPostCallback GetPermissionsOperationCompleted;

		// Token: 0x040004C6 RID: 1222
		private SendOrPostCallback LogonUserOperationCompleted;

		// Token: 0x040004C7 RID: 1223
		private SendOrPostCallback LogoffOperationCompleted;

		// Token: 0x040004C8 RID: 1224
		private SendOrPostCallback CreateModelOperationCompleted;

		// Token: 0x040004C9 RID: 1225
		private SendOrPostCallback GetModelDefinitionOperationCompleted;

		// Token: 0x040004CA RID: 1226
		private SendOrPostCallback SetModelDefinitionOperationCompleted;

		// Token: 0x040004CB RID: 1227
		private SendOrPostCallback ListModelPerspectivesOperationCompleted;

		// Token: 0x040004CC RID: 1228
		private SendOrPostCallback GetUserModelOperationCompleted;

		// Token: 0x040004CD RID: 1229
		private SendOrPostCallback ListModelItemChildrenOperationCompleted;

		// Token: 0x040004CE RID: 1230
		private SendOrPostCallback GetModelItemPermissionsOperationCompleted;

		// Token: 0x040004CF RID: 1231
		private SendOrPostCallback GetModelItemPoliciesOperationCompleted;

		// Token: 0x040004D0 RID: 1232
		private SendOrPostCallback SetModelItemPoliciesOperationCompleted;

		// Token: 0x040004D1 RID: 1233
		private SendOrPostCallback InheritModelItemParentSecurityOperationCompleted;

		// Token: 0x040004D2 RID: 1234
		private SendOrPostCallback RemoveAllModelItemPoliciesOperationCompleted;

		// Token: 0x040004D3 RID: 1235
		private SendOrPostCallback SetModelDrillthroughReportsOperationCompleted;

		// Token: 0x040004D4 RID: 1236
		private SendOrPostCallback ListModelDrillthroughReportsOperationCompleted;

		// Token: 0x040004D5 RID: 1237
		private SendOrPostCallback GenerateModelOperationCompleted;

		// Token: 0x040004D6 RID: 1238
		private SendOrPostCallback RegenerateModelOperationCompleted;

		// Token: 0x040004D7 RID: 1239
		private SendOrPostCallback ListSecureMethodsOperationCompleted;

		// Token: 0x040004D8 RID: 1240
		private SendOrPostCallback CreateBatchOperationCompleted;

		// Token: 0x040004D9 RID: 1241
		private SendOrPostCallback CancelBatchOperationCompleted;

		// Token: 0x040004DA RID: 1242
		private SendOrPostCallback ExecuteBatchOperationCompleted;

		// Token: 0x040004DB RID: 1243
		private SendOrPostCallback GetSystemPropertiesOperationCompleted;

		// Token: 0x040004DC RID: 1244
		private SendOrPostCallback SetSystemPropertiesOperationCompleted;

		// Token: 0x040004DD RID: 1245
		private SendOrPostCallback DeleteItemOperationCompleted;

		// Token: 0x040004DE RID: 1246
		private SendOrPostCallback MoveItemOperationCompleted;

		// Token: 0x040004DF RID: 1247
		private SendOrPostCallback ListChildrenOperationCompleted;

		// Token: 0x040004E0 RID: 1248
		private SendOrPostCallback ListDependentItemsOperationCompleted;

		// Token: 0x040004E1 RID: 1249
		private ItemNamespaceHeader itemNamespaceHeaderValueField;

		// Token: 0x040004E2 RID: 1250
		private SendOrPostCallback GetPropertiesOperationCompleted;

		// Token: 0x040004E3 RID: 1251
		private SendOrPostCallback SetPropertiesOperationCompleted;

		// Token: 0x040004E4 RID: 1252
		private SendOrPostCallback GetItemTypeOperationCompleted;

		// Token: 0x040004E5 RID: 1253
		private SendOrPostCallback CreateFolderOperationCompleted;

		// Token: 0x040004E6 RID: 1254
		private SendOrPostCallback CreateReportOperationCompleted;

		// Token: 0x040004E7 RID: 1255
		private SendOrPostCallback GetReportDefinitionOperationCompleted;

		// Token: 0x040004E8 RID: 1256
		private SendOrPostCallback SetReportDefinitionOperationCompleted;

		// Token: 0x040004E9 RID: 1257
		private SendOrPostCallback CreateResourceOperationCompleted;

		// Token: 0x040004EA RID: 1258
		private SendOrPostCallback SetResourceContentsOperationCompleted;

		// Token: 0x040004EB RID: 1259
		private SendOrPostCallback GetResourceContentsOperationCompleted;

		// Token: 0x040004EC RID: 1260
		private SendOrPostCallback CreateReportEditSessionOperationCompleted;

		// Token: 0x040004ED RID: 1261
		private SendOrPostCallback GetReportParametersOperationCompleted;

		// Token: 0x040004EE RID: 1262
		private SendOrPostCallback SetReportParametersOperationCompleted;

		// Token: 0x040004EF RID: 1263
		private SendOrPostCallback CreateLinkedReportOperationCompleted;

		// Token: 0x040004F0 RID: 1264
		private SendOrPostCallback GetReportLinkOperationCompleted;

		// Token: 0x040004F1 RID: 1265
		private SendOrPostCallback SetReportLinkOperationCompleted;

		// Token: 0x040004F2 RID: 1266
		private SendOrPostCallback GetRenderResourceOperationCompleted;

		// Token: 0x040004F3 RID: 1267
		private SendOrPostCallback SetExecutionOptionsOperationCompleted;

		// Token: 0x040004F4 RID: 1268
		private SendOrPostCallback GetExecutionOptionsOperationCompleted;

		// Token: 0x040004F5 RID: 1269
		private SendOrPostCallback SetCacheOptionsOperationCompleted;

		// Token: 0x040004F6 RID: 1270
		private SendOrPostCallback GetCacheOptionsOperationCompleted;

		// Token: 0x040004F7 RID: 1271
		private SendOrPostCallback UpdateReportExecutionSnapshotOperationCompleted;

		// Token: 0x040004F8 RID: 1272
		private SendOrPostCallback FlushCacheOperationCompleted;

		// Token: 0x040004F9 RID: 1273
		private SendOrPostCallback ListJobsOperationCompleted;

		// Token: 0x040004FA RID: 1274
		private SendOrPostCallback CancelJobOperationCompleted;

		// Token: 0x040004FB RID: 1275
		private SendOrPostCallback CreateDataSourceOperationCompleted;

		// Token: 0x040004FC RID: 1276
		private SendOrPostCallback GetDataSourceContentsOperationCompleted;

		// Token: 0x040004FD RID: 1277
		private SendOrPostCallback SetDataSourceContentsOperationCompleted;

		// Token: 0x040004FE RID: 1278
		private SendOrPostCallback EnableDataSourceOperationCompleted;

		// Token: 0x040004FF RID: 1279
		private SendOrPostCallback DisableDataSourceOperationCompleted;

		// Token: 0x04000500 RID: 1280
		private SendOrPostCallback SetItemDataSourcesOperationCompleted;

		// Token: 0x04000501 RID: 1281
		private SendOrPostCallback GetItemDataSourcesOperationCompleted;

		// Token: 0x04000502 RID: 1282
		private SendOrPostCallback GetItemDataSourcePromptsOperationCompleted;

		// Token: 0x04000503 RID: 1283
		private SendOrPostCallback TestConnectForDataSourceDefinitionOperationCompleted;

		// Token: 0x04000504 RID: 1284
		private SendOrPostCallback TestConnectForItemDataSourceOperationCompleted;

		// Token: 0x04000505 RID: 1285
		private SendOrPostCallback CreateReportHistorySnapshotOperationCompleted;

		// Token: 0x04000506 RID: 1286
		private SendOrPostCallback SetReportHistoryOptionsOperationCompleted;

		// Token: 0x04000507 RID: 1287
		private SendOrPostCallback GetReportHistoryOptionsOperationCompleted;

		// Token: 0x04000508 RID: 1288
		private SendOrPostCallback SetReportHistoryLimitOperationCompleted;

		// Token: 0x04000509 RID: 1289
		private SendOrPostCallback GetReportHistoryLimitOperationCompleted;

		// Token: 0x0400050A RID: 1290
		private SendOrPostCallback ListReportHistoryOperationCompleted;

		// Token: 0x0400050B RID: 1291
		private SendOrPostCallback DeleteReportHistorySnapshotOperationCompleted;

		// Token: 0x0400050C RID: 1292
		private SendOrPostCallback FindItemsOperationCompleted;

		// Token: 0x0400050D RID: 1293
		private SendOrPostCallback CreateScheduleOperationCompleted;

		// Token: 0x0400050E RID: 1294
		private SendOrPostCallback DeleteScheduleOperationCompleted;

		// Token: 0x0400050F RID: 1295
		private SendOrPostCallback SetSchedulePropertiesOperationCompleted;

		// Token: 0x04000510 RID: 1296
		private SendOrPostCallback GetSchedulePropertiesOperationCompleted;

		// Token: 0x04000511 RID: 1297
		private SendOrPostCallback ListScheduledReportsOperationCompleted;

		// Token: 0x04000512 RID: 1298
		private SendOrPostCallback ListSchedulesOperationCompleted;

		// Token: 0x04000513 RID: 1299
		private SendOrPostCallback PauseScheduleOperationCompleted;

		// Token: 0x04000514 RID: 1300
		private SendOrPostCallback ResumeScheduleOperationCompleted;

		// Token: 0x04000515 RID: 1301
		private SendOrPostCallback CreateSubscriptionOperationCompleted;

		// Token: 0x04000516 RID: 1302
		private SendOrPostCallback CreateDataDrivenSubscriptionOperationCompleted;

		// Token: 0x04000517 RID: 1303
		private SendOrPostCallback SetSubscriptionPropertiesOperationCompleted;

		// Token: 0x04000518 RID: 1304
		private SendOrPostCallback SetDataDrivenSubscriptionPropertiesOperationCompleted;

		// Token: 0x04000519 RID: 1305
		private SendOrPostCallback GetSubscriptionPropertiesOperationCompleted;

		// Token: 0x0400051A RID: 1306
		private SendOrPostCallback GetDataDrivenSubscriptionPropertiesOperationCompleted;

		// Token: 0x0400051B RID: 1307
		private SendOrPostCallback DeleteSubscriptionOperationCompleted;

		// Token: 0x0400051C RID: 1308
		private SendOrPostCallback PrepareQueryOperationCompleted;

		// Token: 0x0400051D RID: 1309
		private SendOrPostCallback GetExtensionSettingsOperationCompleted;

		// Token: 0x0400051E RID: 1310
		private SendOrPostCallback ValidateExtensionSettingsOperationCompleted;

		// Token: 0x0400051F RID: 1311
		private SendOrPostCallback ListSubscriptionsOperationCompleted;

		// Token: 0x04000520 RID: 1312
		private SendOrPostCallback ListSubscriptionsUsingDataSourceOperationCompleted;

		// Token: 0x04000521 RID: 1313
		private SendOrPostCallback ListExtensionsOperationCompleted;

		// Token: 0x04000522 RID: 1314
		private SendOrPostCallback ListEventsOperationCompleted;

		// Token: 0x04000523 RID: 1315
		private SendOrPostCallback FireEventOperationCompleted;

		// Token: 0x04000524 RID: 1316
		private SendOrPostCallback ListTasksOperationCompleted;

		// Token: 0x04000525 RID: 1317
		private SendOrPostCallback ListRolesOperationCompleted;

		// Token: 0x04000526 RID: 1318
		private SendOrPostCallback CreateRoleOperationCompleted;

		// Token: 0x04000527 RID: 1319
		private SendOrPostCallback DeleteRoleOperationCompleted;

		// Token: 0x04000528 RID: 1320
		private SendOrPostCallback GetRolePropertiesOperationCompleted;

		// Token: 0x04000529 RID: 1321
		private SendOrPostCallback SetRolePropertiesOperationCompleted;
	}
}
