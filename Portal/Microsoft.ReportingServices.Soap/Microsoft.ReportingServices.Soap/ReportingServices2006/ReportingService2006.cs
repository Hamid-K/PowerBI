using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x0200010F RID: 271
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[WebServiceBinding(Name = "ReportingService2006Soap", Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices")]
	[XmlInclude(typeof(DataSourceDefinitionOrReference))]
	[XmlInclude(typeof(ExpirationDefinition))]
	[XmlInclude(typeof(RecurrencePattern))]
	[XmlInclude(typeof(ScheduleDefinitionOrReference))]
	public class ReportingService2006 : SoapHttpClientProtocol
	{
		// Token: 0x060008E0 RID: 2272 RVA: 0x0000E6D2 File Offset: 0x0000C8D2
		public ReportingService2006()
		{
			base.Url = "http://localhost/reportserver/reportservice2006.asmx";
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060008E1 RID: 2273 RVA: 0x0000E6E5 File Offset: 0x0000C8E5
		// (set) Token: 0x060008E2 RID: 2274 RVA: 0x0000E6ED File Offset: 0x0000C8ED
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

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060008E3 RID: 2275 RVA: 0x0000E6F6 File Offset: 0x0000C8F6
		// (set) Token: 0x060008E4 RID: 2276 RVA: 0x0000E6FE File Offset: 0x0000C8FE
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

		// Token: 0x1400007C RID: 124
		// (add) Token: 0x060008E5 RID: 2277 RVA: 0x0000E708 File Offset: 0x0000C908
		// (remove) Token: 0x060008E6 RID: 2278 RVA: 0x0000E740 File Offset: 0x0000C940
		public event GetUserModelCompletedEventHandler GetUserModelCompleted;

		// Token: 0x1400007D RID: 125
		// (add) Token: 0x060008E7 RID: 2279 RVA: 0x0000E778 File Offset: 0x0000C978
		// (remove) Token: 0x060008E8 RID: 2280 RVA: 0x0000E7B0 File Offset: 0x0000C9B0
		public event ListModelItemChildrenCompletedEventHandler ListModelItemChildrenCompleted;

		// Token: 0x1400007E RID: 126
		// (add) Token: 0x060008E9 RID: 2281 RVA: 0x0000E7E8 File Offset: 0x0000C9E8
		// (remove) Token: 0x060008EA RID: 2282 RVA: 0x0000E820 File Offset: 0x0000CA20
		public event GetModelItemPermissionsCompletedEventHandler GetModelItemPermissionsCompleted;

		// Token: 0x1400007F RID: 127
		// (add) Token: 0x060008EB RID: 2283 RVA: 0x0000E858 File Offset: 0x0000CA58
		// (remove) Token: 0x060008EC RID: 2284 RVA: 0x0000E890 File Offset: 0x0000CA90
		public event GetModelItemPoliciesCompletedEventHandler GetModelItemPoliciesCompleted;

		// Token: 0x14000080 RID: 128
		// (add) Token: 0x060008ED RID: 2285 RVA: 0x0000E8C8 File Offset: 0x0000CAC8
		// (remove) Token: 0x060008EE RID: 2286 RVA: 0x0000E900 File Offset: 0x0000CB00
		public event SetModelItemPoliciesCompletedEventHandler SetModelItemPoliciesCompleted;

		// Token: 0x14000081 RID: 129
		// (add) Token: 0x060008EF RID: 2287 RVA: 0x0000E938 File Offset: 0x0000CB38
		// (remove) Token: 0x060008F0 RID: 2288 RVA: 0x0000E970 File Offset: 0x0000CB70
		public event InheritModelItemParentSecurityCompletedEventHandler InheritModelItemParentSecurityCompleted;

		// Token: 0x14000082 RID: 130
		// (add) Token: 0x060008F1 RID: 2289 RVA: 0x0000E9A8 File Offset: 0x0000CBA8
		// (remove) Token: 0x060008F2 RID: 2290 RVA: 0x0000E9E0 File Offset: 0x0000CBE0
		public event RemoveAllModelItemPoliciesCompletedEventHandler RemoveAllModelItemPoliciesCompleted;

		// Token: 0x14000083 RID: 131
		// (add) Token: 0x060008F3 RID: 2291 RVA: 0x0000EA18 File Offset: 0x0000CC18
		// (remove) Token: 0x060008F4 RID: 2292 RVA: 0x0000EA50 File Offset: 0x0000CC50
		public event SetModelDrillthroughReportsCompletedEventHandler SetModelDrillthroughReportsCompleted;

		// Token: 0x14000084 RID: 132
		// (add) Token: 0x060008F5 RID: 2293 RVA: 0x0000EA88 File Offset: 0x0000CC88
		// (remove) Token: 0x060008F6 RID: 2294 RVA: 0x0000EAC0 File Offset: 0x0000CCC0
		public event ListModelDrillthroughReportsCompletedEventHandler ListModelDrillthroughReportsCompleted;

		// Token: 0x14000085 RID: 133
		// (add) Token: 0x060008F7 RID: 2295 RVA: 0x0000EAF8 File Offset: 0x0000CCF8
		// (remove) Token: 0x060008F8 RID: 2296 RVA: 0x0000EB30 File Offset: 0x0000CD30
		public event GenerateModelCompletedEventHandler GenerateModelCompleted;

		// Token: 0x14000086 RID: 134
		// (add) Token: 0x060008F9 RID: 2297 RVA: 0x0000EB68 File Offset: 0x0000CD68
		// (remove) Token: 0x060008FA RID: 2298 RVA: 0x0000EBA0 File Offset: 0x0000CDA0
		public event RegenerateModelCompletedEventHandler RegenerateModelCompleted;

		// Token: 0x14000087 RID: 135
		// (add) Token: 0x060008FB RID: 2299 RVA: 0x0000EBD8 File Offset: 0x0000CDD8
		// (remove) Token: 0x060008FC RID: 2300 RVA: 0x0000EC10 File Offset: 0x0000CE10
		public event GetReportServerConfigInfoCompletedEventHandler GetReportServerConfigInfoCompleted;

		// Token: 0x14000088 RID: 136
		// (add) Token: 0x060008FD RID: 2301 RVA: 0x0000EC48 File Offset: 0x0000CE48
		// (remove) Token: 0x060008FE RID: 2302 RVA: 0x0000EC80 File Offset: 0x0000CE80
		public event ListSecureMethodsCompletedEventHandler ListSecureMethodsCompleted;

		// Token: 0x14000089 RID: 137
		// (add) Token: 0x060008FF RID: 2303 RVA: 0x0000ECB8 File Offset: 0x0000CEB8
		// (remove) Token: 0x06000900 RID: 2304 RVA: 0x0000ECF0 File Offset: 0x0000CEF0
		public event GetSystemPropertiesCompletedEventHandler GetSystemPropertiesCompleted;

		// Token: 0x1400008A RID: 138
		// (add) Token: 0x06000901 RID: 2305 RVA: 0x0000ED28 File Offset: 0x0000CF28
		// (remove) Token: 0x06000902 RID: 2306 RVA: 0x0000ED60 File Offset: 0x0000CF60
		public event SetSystemPropertiesCompletedEventHandler SetSystemPropertiesCompleted;

		// Token: 0x1400008B RID: 139
		// (add) Token: 0x06000903 RID: 2307 RVA: 0x0000ED98 File Offset: 0x0000CF98
		// (remove) Token: 0x06000904 RID: 2308 RVA: 0x0000EDD0 File Offset: 0x0000CFD0
		public event DeleteItemCompletedEventHandler DeleteItemCompleted;

		// Token: 0x1400008C RID: 140
		// (add) Token: 0x06000905 RID: 2309 RVA: 0x0000EE08 File Offset: 0x0000D008
		// (remove) Token: 0x06000906 RID: 2310 RVA: 0x0000EE40 File Offset: 0x0000D040
		public event MoveItemCompletedEventHandler MoveItemCompleted;

		// Token: 0x1400008D RID: 141
		// (add) Token: 0x06000907 RID: 2311 RVA: 0x0000EE78 File Offset: 0x0000D078
		// (remove) Token: 0x06000908 RID: 2312 RVA: 0x0000EEB0 File Offset: 0x0000D0B0
		public event ListChildrenCompletedEventHandler ListChildrenCompleted;

		// Token: 0x1400008E RID: 142
		// (add) Token: 0x06000909 RID: 2313 RVA: 0x0000EEE8 File Offset: 0x0000D0E8
		// (remove) Token: 0x0600090A RID: 2314 RVA: 0x0000EF20 File Offset: 0x0000D120
		public event ListParentsCompletedEventHandler ListParentsCompleted;

		// Token: 0x1400008F RID: 143
		// (add) Token: 0x0600090B RID: 2315 RVA: 0x0000EF58 File Offset: 0x0000D158
		// (remove) Token: 0x0600090C RID: 2316 RVA: 0x0000EF90 File Offset: 0x0000D190
		public event ListDependentItemsCompletedEventHandler ListDependentItemsCompleted;

		// Token: 0x14000090 RID: 144
		// (add) Token: 0x0600090D RID: 2317 RVA: 0x0000EFC8 File Offset: 0x0000D1C8
		// (remove) Token: 0x0600090E RID: 2318 RVA: 0x0000F000 File Offset: 0x0000D200
		public event GetPropertiesCompletedEventHandler GetPropertiesCompleted;

		// Token: 0x14000091 RID: 145
		// (add) Token: 0x0600090F RID: 2319 RVA: 0x0000F038 File Offset: 0x0000D238
		// (remove) Token: 0x06000910 RID: 2320 RVA: 0x0000F070 File Offset: 0x0000D270
		public event SetPropertiesCompletedEventHandler SetPropertiesCompleted;

		// Token: 0x14000092 RID: 146
		// (add) Token: 0x06000911 RID: 2321 RVA: 0x0000F0A8 File Offset: 0x0000D2A8
		// (remove) Token: 0x06000912 RID: 2322 RVA: 0x0000F0E0 File Offset: 0x0000D2E0
		public event GetItemTypeCompletedEventHandler GetItemTypeCompleted;

		// Token: 0x14000093 RID: 147
		// (add) Token: 0x06000913 RID: 2323 RVA: 0x0000F118 File Offset: 0x0000D318
		// (remove) Token: 0x06000914 RID: 2324 RVA: 0x0000F150 File Offset: 0x0000D350
		public event CreateFolderCompletedEventHandler CreateFolderCompleted;

		// Token: 0x14000094 RID: 148
		// (add) Token: 0x06000915 RID: 2325 RVA: 0x0000F188 File Offset: 0x0000D388
		// (remove) Token: 0x06000916 RID: 2326 RVA: 0x0000F1C0 File Offset: 0x0000D3C0
		public event CreateReportCompletedEventHandler CreateReportCompleted;

		// Token: 0x14000095 RID: 149
		// (add) Token: 0x06000917 RID: 2327 RVA: 0x0000F1F8 File Offset: 0x0000D3F8
		// (remove) Token: 0x06000918 RID: 2328 RVA: 0x0000F230 File Offset: 0x0000D430
		public event GetReportDefinitionCompletedEventHandler GetReportDefinitionCompleted;

		// Token: 0x14000096 RID: 150
		// (add) Token: 0x06000919 RID: 2329 RVA: 0x0000F268 File Offset: 0x0000D468
		// (remove) Token: 0x0600091A RID: 2330 RVA: 0x0000F2A0 File Offset: 0x0000D4A0
		public event SetReportDefinitionCompletedEventHandler SetReportDefinitionCompleted;

		// Token: 0x14000097 RID: 151
		// (add) Token: 0x0600091B RID: 2331 RVA: 0x0000F2D8 File Offset: 0x0000D4D8
		// (remove) Token: 0x0600091C RID: 2332 RVA: 0x0000F310 File Offset: 0x0000D510
		public event CreateResourceCompletedEventHandler CreateResourceCompleted;

		// Token: 0x14000098 RID: 152
		// (add) Token: 0x0600091D RID: 2333 RVA: 0x0000F348 File Offset: 0x0000D548
		// (remove) Token: 0x0600091E RID: 2334 RVA: 0x0000F380 File Offset: 0x0000D580
		public event SetResourceContentsCompletedEventHandler SetResourceContentsCompleted;

		// Token: 0x14000099 RID: 153
		// (add) Token: 0x0600091F RID: 2335 RVA: 0x0000F3B8 File Offset: 0x0000D5B8
		// (remove) Token: 0x06000920 RID: 2336 RVA: 0x0000F3F0 File Offset: 0x0000D5F0
		public event GetResourceContentsCompletedEventHandler GetResourceContentsCompleted;

		// Token: 0x1400009A RID: 154
		// (add) Token: 0x06000921 RID: 2337 RVA: 0x0000F428 File Offset: 0x0000D628
		// (remove) Token: 0x06000922 RID: 2338 RVA: 0x0000F460 File Offset: 0x0000D660
		public event CreateReportEditSessionCompletedEventHandler CreateReportEditSessionCompleted;

		// Token: 0x1400009B RID: 155
		// (add) Token: 0x06000923 RID: 2339 RVA: 0x0000F498 File Offset: 0x0000D698
		// (remove) Token: 0x06000924 RID: 2340 RVA: 0x0000F4D0 File Offset: 0x0000D6D0
		public event GetReportParametersCompletedEventHandler GetReportParametersCompleted;

		// Token: 0x1400009C RID: 156
		// (add) Token: 0x06000925 RID: 2341 RVA: 0x0000F508 File Offset: 0x0000D708
		// (remove) Token: 0x06000926 RID: 2342 RVA: 0x0000F540 File Offset: 0x0000D740
		public event SetReportParametersCompletedEventHandler SetReportParametersCompleted;

		// Token: 0x1400009D RID: 157
		// (add) Token: 0x06000927 RID: 2343 RVA: 0x0000F578 File Offset: 0x0000D778
		// (remove) Token: 0x06000928 RID: 2344 RVA: 0x0000F5B0 File Offset: 0x0000D7B0
		public event SetExecutionOptionsCompletedEventHandler SetExecutionOptionsCompleted;

		// Token: 0x1400009E RID: 158
		// (add) Token: 0x06000929 RID: 2345 RVA: 0x0000F5E8 File Offset: 0x0000D7E8
		// (remove) Token: 0x0600092A RID: 2346 RVA: 0x0000F620 File Offset: 0x0000D820
		public event GetExecutionOptionsCompletedEventHandler GetExecutionOptionsCompleted;

		// Token: 0x1400009F RID: 159
		// (add) Token: 0x0600092B RID: 2347 RVA: 0x0000F658 File Offset: 0x0000D858
		// (remove) Token: 0x0600092C RID: 2348 RVA: 0x0000F690 File Offset: 0x0000D890
		public event SetCacheOptionsCompletedEventHandler SetCacheOptionsCompleted;

		// Token: 0x140000A0 RID: 160
		// (add) Token: 0x0600092D RID: 2349 RVA: 0x0000F6C8 File Offset: 0x0000D8C8
		// (remove) Token: 0x0600092E RID: 2350 RVA: 0x0000F700 File Offset: 0x0000D900
		public event GetCacheOptionsCompletedEventHandler GetCacheOptionsCompleted;

		// Token: 0x140000A1 RID: 161
		// (add) Token: 0x0600092F RID: 2351 RVA: 0x0000F738 File Offset: 0x0000D938
		// (remove) Token: 0x06000930 RID: 2352 RVA: 0x0000F770 File Offset: 0x0000D970
		public event UpdateReportExecutionSnapshotCompletedEventHandler UpdateReportExecutionSnapshotCompleted;

		// Token: 0x140000A2 RID: 162
		// (add) Token: 0x06000931 RID: 2353 RVA: 0x0000F7A8 File Offset: 0x0000D9A8
		// (remove) Token: 0x06000932 RID: 2354 RVA: 0x0000F7E0 File Offset: 0x0000D9E0
		public event FlushCacheCompletedEventHandler FlushCacheCompleted;

		// Token: 0x140000A3 RID: 163
		// (add) Token: 0x06000933 RID: 2355 RVA: 0x0000F818 File Offset: 0x0000DA18
		// (remove) Token: 0x06000934 RID: 2356 RVA: 0x0000F850 File Offset: 0x0000DA50
		public event ListJobsCompletedEventHandler ListJobsCompleted;

		// Token: 0x140000A4 RID: 164
		// (add) Token: 0x06000935 RID: 2357 RVA: 0x0000F888 File Offset: 0x0000DA88
		// (remove) Token: 0x06000936 RID: 2358 RVA: 0x0000F8C0 File Offset: 0x0000DAC0
		public event CancelJobCompletedEventHandler CancelJobCompleted;

		// Token: 0x140000A5 RID: 165
		// (add) Token: 0x06000937 RID: 2359 RVA: 0x0000F8F8 File Offset: 0x0000DAF8
		// (remove) Token: 0x06000938 RID: 2360 RVA: 0x0000F930 File Offset: 0x0000DB30
		public event CreateDataSourceCompletedEventHandler CreateDataSourceCompleted;

		// Token: 0x140000A6 RID: 166
		// (add) Token: 0x06000939 RID: 2361 RVA: 0x0000F968 File Offset: 0x0000DB68
		// (remove) Token: 0x0600093A RID: 2362 RVA: 0x0000F9A0 File Offset: 0x0000DBA0
		public event GetDataSourceContentsCompletedEventHandler GetDataSourceContentsCompleted;

		// Token: 0x140000A7 RID: 167
		// (add) Token: 0x0600093B RID: 2363 RVA: 0x0000F9D8 File Offset: 0x0000DBD8
		// (remove) Token: 0x0600093C RID: 2364 RVA: 0x0000FA10 File Offset: 0x0000DC10
		public event SetDataSourceContentsCompletedEventHandler SetDataSourceContentsCompleted;

		// Token: 0x140000A8 RID: 168
		// (add) Token: 0x0600093D RID: 2365 RVA: 0x0000FA48 File Offset: 0x0000DC48
		// (remove) Token: 0x0600093E RID: 2366 RVA: 0x0000FA80 File Offset: 0x0000DC80
		public event EnableDataSourceCompletedEventHandler EnableDataSourceCompleted;

		// Token: 0x140000A9 RID: 169
		// (add) Token: 0x0600093F RID: 2367 RVA: 0x0000FAB8 File Offset: 0x0000DCB8
		// (remove) Token: 0x06000940 RID: 2368 RVA: 0x0000FAF0 File Offset: 0x0000DCF0
		public event DisableDataSourceCompletedEventHandler DisableDataSourceCompleted;

		// Token: 0x140000AA RID: 170
		// (add) Token: 0x06000941 RID: 2369 RVA: 0x0000FB28 File Offset: 0x0000DD28
		// (remove) Token: 0x06000942 RID: 2370 RVA: 0x0000FB60 File Offset: 0x0000DD60
		public event SetItemDataSourcesCompletedEventHandler SetItemDataSourcesCompleted;

		// Token: 0x140000AB RID: 171
		// (add) Token: 0x06000943 RID: 2371 RVA: 0x0000FB98 File Offset: 0x0000DD98
		// (remove) Token: 0x06000944 RID: 2372 RVA: 0x0000FBD0 File Offset: 0x0000DDD0
		public event GetItemDataSourcesCompletedEventHandler GetItemDataSourcesCompleted;

		// Token: 0x140000AC RID: 172
		// (add) Token: 0x06000945 RID: 2373 RVA: 0x0000FC08 File Offset: 0x0000DE08
		// (remove) Token: 0x06000946 RID: 2374 RVA: 0x0000FC40 File Offset: 0x0000DE40
		public event GetItemDataSourcePromptsCompletedEventHandler GetItemDataSourcePromptsCompleted;

		// Token: 0x140000AD RID: 173
		// (add) Token: 0x06000947 RID: 2375 RVA: 0x0000FC78 File Offset: 0x0000DE78
		// (remove) Token: 0x06000948 RID: 2376 RVA: 0x0000FCB0 File Offset: 0x0000DEB0
		public event TestConnectForDataSourceDefinitionCompletedEventHandler TestConnectForDataSourceDefinitionCompleted;

		// Token: 0x140000AE RID: 174
		// (add) Token: 0x06000949 RID: 2377 RVA: 0x0000FCE8 File Offset: 0x0000DEE8
		// (remove) Token: 0x0600094A RID: 2378 RVA: 0x0000FD20 File Offset: 0x0000DF20
		public event TestConnectForItemDataSourceCompletedEventHandler TestConnectForItemDataSourceCompleted;

		// Token: 0x140000AF RID: 175
		// (add) Token: 0x0600094B RID: 2379 RVA: 0x0000FD58 File Offset: 0x0000DF58
		// (remove) Token: 0x0600094C RID: 2380 RVA: 0x0000FD90 File Offset: 0x0000DF90
		public event CreateReportHistorySnapshotCompletedEventHandler CreateReportHistorySnapshotCompleted;

		// Token: 0x140000B0 RID: 176
		// (add) Token: 0x0600094D RID: 2381 RVA: 0x0000FDC8 File Offset: 0x0000DFC8
		// (remove) Token: 0x0600094E RID: 2382 RVA: 0x0000FE00 File Offset: 0x0000E000
		public event SetReportHistoryOptionsCompletedEventHandler SetReportHistoryOptionsCompleted;

		// Token: 0x140000B1 RID: 177
		// (add) Token: 0x0600094F RID: 2383 RVA: 0x0000FE38 File Offset: 0x0000E038
		// (remove) Token: 0x06000950 RID: 2384 RVA: 0x0000FE70 File Offset: 0x0000E070
		public event GetReportHistoryOptionsCompletedEventHandler GetReportHistoryOptionsCompleted;

		// Token: 0x140000B2 RID: 178
		// (add) Token: 0x06000951 RID: 2385 RVA: 0x0000FEA8 File Offset: 0x0000E0A8
		// (remove) Token: 0x06000952 RID: 2386 RVA: 0x0000FEE0 File Offset: 0x0000E0E0
		public event SetReportHistoryLimitCompletedEventHandler SetReportHistoryLimitCompleted;

		// Token: 0x140000B3 RID: 179
		// (add) Token: 0x06000953 RID: 2387 RVA: 0x0000FF18 File Offset: 0x0000E118
		// (remove) Token: 0x06000954 RID: 2388 RVA: 0x0000FF50 File Offset: 0x0000E150
		public event GetReportHistoryLimitCompletedEventHandler GetReportHistoryLimitCompleted;

		// Token: 0x140000B4 RID: 180
		// (add) Token: 0x06000955 RID: 2389 RVA: 0x0000FF88 File Offset: 0x0000E188
		// (remove) Token: 0x06000956 RID: 2390 RVA: 0x0000FFC0 File Offset: 0x0000E1C0
		public event ListReportHistoryCompletedEventHandler ListReportHistoryCompleted;

		// Token: 0x140000B5 RID: 181
		// (add) Token: 0x06000957 RID: 2391 RVA: 0x0000FFF8 File Offset: 0x0000E1F8
		// (remove) Token: 0x06000958 RID: 2392 RVA: 0x00010030 File Offset: 0x0000E230
		public event DeleteReportHistorySnapshotCompletedEventHandler DeleteReportHistorySnapshotCompleted;

		// Token: 0x140000B6 RID: 182
		// (add) Token: 0x06000959 RID: 2393 RVA: 0x00010068 File Offset: 0x0000E268
		// (remove) Token: 0x0600095A RID: 2394 RVA: 0x000100A0 File Offset: 0x0000E2A0
		public event CreateScheduleCompletedEventHandler CreateScheduleCompleted;

		// Token: 0x140000B7 RID: 183
		// (add) Token: 0x0600095B RID: 2395 RVA: 0x000100D8 File Offset: 0x0000E2D8
		// (remove) Token: 0x0600095C RID: 2396 RVA: 0x00010110 File Offset: 0x0000E310
		public event DeleteScheduleCompletedEventHandler DeleteScheduleCompleted;

		// Token: 0x140000B8 RID: 184
		// (add) Token: 0x0600095D RID: 2397 RVA: 0x00010148 File Offset: 0x0000E348
		// (remove) Token: 0x0600095E RID: 2398 RVA: 0x00010180 File Offset: 0x0000E380
		public event SetSchedulePropertiesCompletedEventHandler SetSchedulePropertiesCompleted;

		// Token: 0x140000B9 RID: 185
		// (add) Token: 0x0600095F RID: 2399 RVA: 0x000101B8 File Offset: 0x0000E3B8
		// (remove) Token: 0x06000960 RID: 2400 RVA: 0x000101F0 File Offset: 0x0000E3F0
		public event GetSchedulePropertiesCompletedEventHandler GetSchedulePropertiesCompleted;

		// Token: 0x140000BA RID: 186
		// (add) Token: 0x06000961 RID: 2401 RVA: 0x00010228 File Offset: 0x0000E428
		// (remove) Token: 0x06000962 RID: 2402 RVA: 0x00010260 File Offset: 0x0000E460
		public event ListScheduledReportsCompletedEventHandler ListScheduledReportsCompleted;

		// Token: 0x140000BB RID: 187
		// (add) Token: 0x06000963 RID: 2403 RVA: 0x00010298 File Offset: 0x0000E498
		// (remove) Token: 0x06000964 RID: 2404 RVA: 0x000102D0 File Offset: 0x0000E4D0
		public event ListSchedulesCompletedEventHandler ListSchedulesCompleted;

		// Token: 0x140000BC RID: 188
		// (add) Token: 0x06000965 RID: 2405 RVA: 0x00010308 File Offset: 0x0000E508
		// (remove) Token: 0x06000966 RID: 2406 RVA: 0x00010340 File Offset: 0x0000E540
		public event PauseScheduleCompletedEventHandler PauseScheduleCompleted;

		// Token: 0x140000BD RID: 189
		// (add) Token: 0x06000967 RID: 2407 RVA: 0x00010378 File Offset: 0x0000E578
		// (remove) Token: 0x06000968 RID: 2408 RVA: 0x000103B0 File Offset: 0x0000E5B0
		public event ResumeScheduleCompletedEventHandler ResumeScheduleCompleted;

		// Token: 0x140000BE RID: 190
		// (add) Token: 0x06000969 RID: 2409 RVA: 0x000103E8 File Offset: 0x0000E5E8
		// (remove) Token: 0x0600096A RID: 2410 RVA: 0x00010420 File Offset: 0x0000E620
		public event CreateSubscriptionCompletedEventHandler CreateSubscriptionCompleted;

		// Token: 0x140000BF RID: 191
		// (add) Token: 0x0600096B RID: 2411 RVA: 0x00010458 File Offset: 0x0000E658
		// (remove) Token: 0x0600096C RID: 2412 RVA: 0x00010490 File Offset: 0x0000E690
		public event CreateDataDrivenSubscriptionCompletedEventHandler CreateDataDrivenSubscriptionCompleted;

		// Token: 0x140000C0 RID: 192
		// (add) Token: 0x0600096D RID: 2413 RVA: 0x000104C8 File Offset: 0x0000E6C8
		// (remove) Token: 0x0600096E RID: 2414 RVA: 0x00010500 File Offset: 0x0000E700
		public event SetSubscriptionPropertiesCompletedEventHandler SetSubscriptionPropertiesCompleted;

		// Token: 0x140000C1 RID: 193
		// (add) Token: 0x0600096F RID: 2415 RVA: 0x00010538 File Offset: 0x0000E738
		// (remove) Token: 0x06000970 RID: 2416 RVA: 0x00010570 File Offset: 0x0000E770
		public event SetDataDrivenSubscriptionPropertiesCompletedEventHandler SetDataDrivenSubscriptionPropertiesCompleted;

		// Token: 0x140000C2 RID: 194
		// (add) Token: 0x06000971 RID: 2417 RVA: 0x000105A8 File Offset: 0x0000E7A8
		// (remove) Token: 0x06000972 RID: 2418 RVA: 0x000105E0 File Offset: 0x0000E7E0
		public event GetSubscriptionPropertiesCompletedEventHandler GetSubscriptionPropertiesCompleted;

		// Token: 0x140000C3 RID: 195
		// (add) Token: 0x06000973 RID: 2419 RVA: 0x00010618 File Offset: 0x0000E818
		// (remove) Token: 0x06000974 RID: 2420 RVA: 0x00010650 File Offset: 0x0000E850
		public event GetDataDrivenSubscriptionPropertiesCompletedEventHandler GetDataDrivenSubscriptionPropertiesCompleted;

		// Token: 0x140000C4 RID: 196
		// (add) Token: 0x06000975 RID: 2421 RVA: 0x00010688 File Offset: 0x0000E888
		// (remove) Token: 0x06000976 RID: 2422 RVA: 0x000106C0 File Offset: 0x0000E8C0
		public event DeleteSubscriptionCompletedEventHandler DeleteSubscriptionCompleted;

		// Token: 0x140000C5 RID: 197
		// (add) Token: 0x06000977 RID: 2423 RVA: 0x000106F8 File Offset: 0x0000E8F8
		// (remove) Token: 0x06000978 RID: 2424 RVA: 0x00010730 File Offset: 0x0000E930
		public event PrepareQueryCompletedEventHandler PrepareQueryCompleted;

		// Token: 0x140000C6 RID: 198
		// (add) Token: 0x06000979 RID: 2425 RVA: 0x00010768 File Offset: 0x0000E968
		// (remove) Token: 0x0600097A RID: 2426 RVA: 0x000107A0 File Offset: 0x0000E9A0
		public event GetExtensionSettingsCompletedEventHandler GetExtensionSettingsCompleted;

		// Token: 0x140000C7 RID: 199
		// (add) Token: 0x0600097B RID: 2427 RVA: 0x000107D8 File Offset: 0x0000E9D8
		// (remove) Token: 0x0600097C RID: 2428 RVA: 0x00010810 File Offset: 0x0000EA10
		public event ValidateExtensionSettingsCompletedEventHandler ValidateExtensionSettingsCompleted;

		// Token: 0x140000C8 RID: 200
		// (add) Token: 0x0600097D RID: 2429 RVA: 0x00010848 File Offset: 0x0000EA48
		// (remove) Token: 0x0600097E RID: 2430 RVA: 0x00010880 File Offset: 0x0000EA80
		public event ListAllSubscriptionsCompletedEventHandler ListAllSubscriptionsCompleted;

		// Token: 0x140000C9 RID: 201
		// (add) Token: 0x0600097F RID: 2431 RVA: 0x000108B8 File Offset: 0x0000EAB8
		// (remove) Token: 0x06000980 RID: 2432 RVA: 0x000108F0 File Offset: 0x0000EAF0
		public event ListMySubscriptionsCompletedEventHandler ListMySubscriptionsCompleted;

		// Token: 0x140000CA RID: 202
		// (add) Token: 0x06000981 RID: 2433 RVA: 0x00010928 File Offset: 0x0000EB28
		// (remove) Token: 0x06000982 RID: 2434 RVA: 0x00010960 File Offset: 0x0000EB60
		public event ListReportSubscriptionsCompletedEventHandler ListReportSubscriptionsCompleted;

		// Token: 0x140000CB RID: 203
		// (add) Token: 0x06000983 RID: 2435 RVA: 0x00010998 File Offset: 0x0000EB98
		// (remove) Token: 0x06000984 RID: 2436 RVA: 0x000109D0 File Offset: 0x0000EBD0
		public event ListExtensionsCompletedEventHandler ListExtensionsCompleted;

		// Token: 0x140000CC RID: 204
		// (add) Token: 0x06000985 RID: 2437 RVA: 0x00010A08 File Offset: 0x0000EC08
		// (remove) Token: 0x06000986 RID: 2438 RVA: 0x00010A40 File Offset: 0x0000EC40
		public event ListEventsCompletedEventHandler ListEventsCompleted;

		// Token: 0x140000CD RID: 205
		// (add) Token: 0x06000987 RID: 2439 RVA: 0x00010A78 File Offset: 0x0000EC78
		// (remove) Token: 0x06000988 RID: 2440 RVA: 0x00010AB0 File Offset: 0x0000ECB0
		public event FireEventCompletedEventHandler FireEventCompleted;

		// Token: 0x140000CE RID: 206
		// (add) Token: 0x06000989 RID: 2441 RVA: 0x00010AE8 File Offset: 0x0000ECE8
		// (remove) Token: 0x0600098A RID: 2442 RVA: 0x00010B20 File Offset: 0x0000ED20
		public event ListTasksCompletedEventHandler ListTasksCompleted;

		// Token: 0x140000CF RID: 207
		// (add) Token: 0x0600098B RID: 2443 RVA: 0x00010B58 File Offset: 0x0000ED58
		// (remove) Token: 0x0600098C RID: 2444 RVA: 0x00010B90 File Offset: 0x0000ED90
		public event ListRolesCompletedEventHandler ListRolesCompleted;

		// Token: 0x140000D0 RID: 208
		// (add) Token: 0x0600098D RID: 2445 RVA: 0x00010BC8 File Offset: 0x0000EDC8
		// (remove) Token: 0x0600098E RID: 2446 RVA: 0x00010C00 File Offset: 0x0000EE00
		public event GetRolePropertiesCompletedEventHandler GetRolePropertiesCompleted;

		// Token: 0x140000D1 RID: 209
		// (add) Token: 0x0600098F RID: 2447 RVA: 0x00010C38 File Offset: 0x0000EE38
		// (remove) Token: 0x06000990 RID: 2448 RVA: 0x00010C70 File Offset: 0x0000EE70
		public event GetPoliciesCompletedEventHandler GetPoliciesCompleted;

		// Token: 0x140000D2 RID: 210
		// (add) Token: 0x06000991 RID: 2449 RVA: 0x00010CA8 File Offset: 0x0000EEA8
		// (remove) Token: 0x06000992 RID: 2450 RVA: 0x00010CE0 File Offset: 0x0000EEE0
		public event SetPoliciesCompletedEventHandler SetPoliciesCompleted;

		// Token: 0x140000D3 RID: 211
		// (add) Token: 0x06000993 RID: 2451 RVA: 0x00010D18 File Offset: 0x0000EF18
		// (remove) Token: 0x06000994 RID: 2452 RVA: 0x00010D50 File Offset: 0x0000EF50
		public event InheritParentSecurityCompletedEventHandler InheritParentSecurityCompleted;

		// Token: 0x140000D4 RID: 212
		// (add) Token: 0x06000995 RID: 2453 RVA: 0x00010D88 File Offset: 0x0000EF88
		// (remove) Token: 0x06000996 RID: 2454 RVA: 0x00010DC0 File Offset: 0x0000EFC0
		public event GetPermissionsCompletedEventHandler GetPermissionsCompleted;

		// Token: 0x140000D5 RID: 213
		// (add) Token: 0x06000997 RID: 2455 RVA: 0x00010DF8 File Offset: 0x0000EFF8
		// (remove) Token: 0x06000998 RID: 2456 RVA: 0x00010E30 File Offset: 0x0000F030
		public event CreateModelCompletedEventHandler CreateModelCompleted;

		// Token: 0x140000D6 RID: 214
		// (add) Token: 0x06000999 RID: 2457 RVA: 0x00010E68 File Offset: 0x0000F068
		// (remove) Token: 0x0600099A RID: 2458 RVA: 0x00010EA0 File Offset: 0x0000F0A0
		public event GetModelDefinitionCompletedEventHandler GetModelDefinitionCompleted;

		// Token: 0x140000D7 RID: 215
		// (add) Token: 0x0600099B RID: 2459 RVA: 0x00010ED8 File Offset: 0x0000F0D8
		// (remove) Token: 0x0600099C RID: 2460 RVA: 0x00010F10 File Offset: 0x0000F110
		public event SetModelDefinitionCompletedEventHandler SetModelDefinitionCompleted;

		// Token: 0x140000D8 RID: 216
		// (add) Token: 0x0600099D RID: 2461 RVA: 0x00010F48 File Offset: 0x0000F148
		// (remove) Token: 0x0600099E RID: 2462 RVA: 0x00010F80 File Offset: 0x0000F180
		public event ListModelPerspectivesCompletedEventHandler ListModelPerspectivesCompleted;

		// Token: 0x0600099F RID: 2463 RVA: 0x00010FB5 File Offset: 0x0000F1B5
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/GetUserModel", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Definition", DataType = "base64Binary")]
		public byte[] GetUserModel(string Model, string Perspective)
		{
			return (byte[])base.Invoke("GetUserModel", new object[] { Model, Perspective })[0];
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x00010FD7 File Offset: 0x0000F1D7
		public IAsyncResult BeginGetUserModel(string Model, string Perspective, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetUserModel", new object[] { Model, Perspective }, callback, asyncState);
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x00010FF5 File Offset: 0x0000F1F5
		public byte[] EndGetUserModel(IAsyncResult asyncResult)
		{
			return (byte[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x00011005 File Offset: 0x0000F205
		public void GetUserModelAsync(string Model, string Perspective)
		{
			this.GetUserModelAsync(Model, Perspective, null);
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x00011010 File Offset: 0x0000F210
		public void GetUserModelAsync(string Model, string Perspective, object userState)
		{
			if (this.GetUserModelOperationCompleted == null)
			{
				this.GetUserModelOperationCompleted = new SendOrPostCallback(this.OnGetUserModelOperationCompleted);
			}
			base.InvokeAsync("GetUserModel", new object[] { Model, Perspective }, this.GetUserModelOperationCompleted, userState);
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x0001104C File Offset: 0x0000F24C
		private void OnGetUserModelOperationCompleted(object arg)
		{
			if (this.GetUserModelCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetUserModelCompleted(this, new GetUserModelCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x00011091 File Offset: 0x0000F291
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/ListModelItemChildren", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("ModelItems")]
		public ModelItem[] ListModelItemChildren(string Model, string ModelItemID, bool Recursive)
		{
			return (ModelItem[])base.Invoke("ListModelItemChildren", new object[] { Model, ModelItemID, Recursive })[0];
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x000110BC File Offset: 0x0000F2BC
		public IAsyncResult BeginListModelItemChildren(string Model, string ModelItemID, bool Recursive, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListModelItemChildren", new object[] { Model, ModelItemID, Recursive }, callback, asyncState);
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x000110E4 File Offset: 0x0000F2E4
		public ModelItem[] EndListModelItemChildren(IAsyncResult asyncResult)
		{
			return (ModelItem[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x000110F4 File Offset: 0x0000F2F4
		public void ListModelItemChildrenAsync(string Model, string ModelItemID, bool Recursive)
		{
			this.ListModelItemChildrenAsync(Model, ModelItemID, Recursive, null);
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x00011100 File Offset: 0x0000F300
		public void ListModelItemChildrenAsync(string Model, string ModelItemID, bool Recursive, object userState)
		{
			if (this.ListModelItemChildrenOperationCompleted == null)
			{
				this.ListModelItemChildrenOperationCompleted = new SendOrPostCallback(this.OnListModelItemChildrenOperationCompleted);
			}
			base.InvokeAsync("ListModelItemChildren", new object[] { Model, ModelItemID, Recursive }, this.ListModelItemChildrenOperationCompleted, userState);
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x00011154 File Offset: 0x0000F354
		private void OnListModelItemChildrenOperationCompleted(object arg)
		{
			if (this.ListModelItemChildrenCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListModelItemChildrenCompleted(this, new ListModelItemChildrenCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x00011199 File Offset: 0x0000F399
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/GetModelItemPermissions", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Permissions")]
		public string[] GetModelItemPermissions(string Model, string ModelItemID)
		{
			return (string[])base.Invoke("GetModelItemPermissions", new object[] { Model, ModelItemID })[0];
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x000111BB File Offset: 0x0000F3BB
		public IAsyncResult BeginGetModelItemPermissions(string Model, string ModelItemID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetModelItemPermissions", new object[] { Model, ModelItemID }, callback, asyncState);
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x000111D9 File Offset: 0x0000F3D9
		public string[] EndGetModelItemPermissions(IAsyncResult asyncResult)
		{
			return (string[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x000111E9 File Offset: 0x0000F3E9
		public void GetModelItemPermissionsAsync(string Model, string ModelItemID)
		{
			this.GetModelItemPermissionsAsync(Model, ModelItemID, null);
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x000111F4 File Offset: 0x0000F3F4
		public void GetModelItemPermissionsAsync(string Model, string ModelItemID, object userState)
		{
			if (this.GetModelItemPermissionsOperationCompleted == null)
			{
				this.GetModelItemPermissionsOperationCompleted = new SendOrPostCallback(this.OnGetModelItemPermissionsOperationCompleted);
			}
			base.InvokeAsync("GetModelItemPermissions", new object[] { Model, ModelItemID }, this.GetModelItemPermissionsOperationCompleted, userState);
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x00011230 File Offset: 0x0000F430
		private void OnGetModelItemPermissionsOperationCompleted(object arg)
		{
			if (this.GetModelItemPermissionsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetModelItemPermissionsCompleted(this, new GetModelItemPermissionsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x00011278 File Offset: 0x0000F478
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/GetModelItemPolicies", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Policies")]
		public Policy[] GetModelItemPolicies(string Model, string ModelItemID, out bool InheritParent)
		{
			object[] array = base.Invoke("GetModelItemPolicies", new object[] { Model, ModelItemID });
			InheritParent = (bool)array[1];
			return (Policy[])array[0];
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x000112B1 File Offset: 0x0000F4B1
		public IAsyncResult BeginGetModelItemPolicies(string Model, string ModelItemID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetModelItemPolicies", new object[] { Model, ModelItemID }, callback, asyncState);
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x000112D0 File Offset: 0x0000F4D0
		public Policy[] EndGetModelItemPolicies(IAsyncResult asyncResult, out bool InheritParent)
		{
			object[] array = base.EndInvoke(asyncResult);
			InheritParent = (bool)array[1];
			return (Policy[])array[0];
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x000112F7 File Offset: 0x0000F4F7
		public void GetModelItemPoliciesAsync(string Model, string ModelItemID)
		{
			this.GetModelItemPoliciesAsync(Model, ModelItemID, null);
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x00011302 File Offset: 0x0000F502
		public void GetModelItemPoliciesAsync(string Model, string ModelItemID, object userState)
		{
			if (this.GetModelItemPoliciesOperationCompleted == null)
			{
				this.GetModelItemPoliciesOperationCompleted = new SendOrPostCallback(this.OnGetModelItemPoliciesOperationCompleted);
			}
			base.InvokeAsync("GetModelItemPolicies", new object[] { Model, ModelItemID }, this.GetModelItemPoliciesOperationCompleted, userState);
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x00011340 File Offset: 0x0000F540
		private void OnGetModelItemPoliciesOperationCompleted(object arg)
		{
			if (this.GetModelItemPoliciesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetModelItemPoliciesCompleted(this, new GetModelItemPoliciesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x00011385 File Offset: 0x0000F585
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/SetModelItemPolicies", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void SetModelItemPolicies(string Model, string ModelItemID, Policy[] Policies)
		{
			base.Invoke("SetModelItemPolicies", new object[] { Model, ModelItemID, Policies });
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x000113A5 File Offset: 0x0000F5A5
		public IAsyncResult BeginSetModelItemPolicies(string Model, string ModelItemID, Policy[] Policies, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetModelItemPolicies", new object[] { Model, ModelItemID, Policies }, callback, asyncState);
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x000113C8 File Offset: 0x0000F5C8
		public void EndSetModelItemPolicies(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x000113D2 File Offset: 0x0000F5D2
		public void SetModelItemPoliciesAsync(string Model, string ModelItemID, Policy[] Policies)
		{
			this.SetModelItemPoliciesAsync(Model, ModelItemID, Policies, null);
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x000113E0 File Offset: 0x0000F5E0
		public void SetModelItemPoliciesAsync(string Model, string ModelItemID, Policy[] Policies, object userState)
		{
			if (this.SetModelItemPoliciesOperationCompleted == null)
			{
				this.SetModelItemPoliciesOperationCompleted = new SendOrPostCallback(this.OnSetModelItemPoliciesOperationCompleted);
			}
			base.InvokeAsync("SetModelItemPolicies", new object[] { Model, ModelItemID, Policies }, this.SetModelItemPoliciesOperationCompleted, userState);
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x0001142C File Offset: 0x0000F62C
		private void OnSetModelItemPoliciesOperationCompleted(object arg)
		{
			if (this.SetModelItemPoliciesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetModelItemPoliciesCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x0001146B File Offset: 0x0000F66B
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/InheritModelItemParentSecurity", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void InheritModelItemParentSecurity(string Model, string ModelItemID)
		{
			base.Invoke("InheritModelItemParentSecurity", new object[] { Model, ModelItemID });
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x00011487 File Offset: 0x0000F687
		public IAsyncResult BeginInheritModelItemParentSecurity(string Model, string ModelItemID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("InheritModelItemParentSecurity", new object[] { Model, ModelItemID }, callback, asyncState);
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x000114A5 File Offset: 0x0000F6A5
		public void EndInheritModelItemParentSecurity(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x000114AF File Offset: 0x0000F6AF
		public void InheritModelItemParentSecurityAsync(string Model, string ModelItemID)
		{
			this.InheritModelItemParentSecurityAsync(Model, ModelItemID, null);
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x000114BA File Offset: 0x0000F6BA
		public void InheritModelItemParentSecurityAsync(string Model, string ModelItemID, object userState)
		{
			if (this.InheritModelItemParentSecurityOperationCompleted == null)
			{
				this.InheritModelItemParentSecurityOperationCompleted = new SendOrPostCallback(this.OnInheritModelItemParentSecurityOperationCompleted);
			}
			base.InvokeAsync("InheritModelItemParentSecurity", new object[] { Model, ModelItemID }, this.InheritModelItemParentSecurityOperationCompleted, userState);
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x000114F8 File Offset: 0x0000F6F8
		private void OnInheritModelItemParentSecurityOperationCompleted(object arg)
		{
			if (this.InheritModelItemParentSecurityCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.InheritModelItemParentSecurityCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x00011537 File Offset: 0x0000F737
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/RemoveAllModelItemPolicies", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void RemoveAllModelItemPolicies(string Model)
		{
			base.Invoke("RemoveAllModelItemPolicies", new object[] { Model });
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x0001154F File Offset: 0x0000F74F
		public IAsyncResult BeginRemoveAllModelItemPolicies(string Model, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("RemoveAllModelItemPolicies", new object[] { Model }, callback, asyncState);
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x00011568 File Offset: 0x0000F768
		public void EndRemoveAllModelItemPolicies(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x00011572 File Offset: 0x0000F772
		public void RemoveAllModelItemPoliciesAsync(string Model)
		{
			this.RemoveAllModelItemPoliciesAsync(Model, null);
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x0001157C File Offset: 0x0000F77C
		public void RemoveAllModelItemPoliciesAsync(string Model, object userState)
		{
			if (this.RemoveAllModelItemPoliciesOperationCompleted == null)
			{
				this.RemoveAllModelItemPoliciesOperationCompleted = new SendOrPostCallback(this.OnRemoveAllModelItemPoliciesOperationCompleted);
			}
			base.InvokeAsync("RemoveAllModelItemPolicies", new object[] { Model }, this.RemoveAllModelItemPoliciesOperationCompleted, userState);
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x000115B4 File Offset: 0x0000F7B4
		private void OnRemoveAllModelItemPoliciesOperationCompleted(object arg)
		{
			if (this.RemoveAllModelItemPoliciesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.RemoveAllModelItemPoliciesCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x000115F3 File Offset: 0x0000F7F3
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/SetModelDrillthroughReports", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void SetModelDrillthroughReports(string Model, string ModelItemID, ModelDrillthroughReport[] Reports)
		{
			base.Invoke("SetModelDrillthroughReports", new object[] { Model, ModelItemID, Reports });
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x00011613 File Offset: 0x0000F813
		public IAsyncResult BeginSetModelDrillthroughReports(string Model, string ModelItemID, ModelDrillthroughReport[] Reports, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetModelDrillthroughReports", new object[] { Model, ModelItemID, Reports }, callback, asyncState);
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x00011636 File Offset: 0x0000F836
		public void EndSetModelDrillthroughReports(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x00011640 File Offset: 0x0000F840
		public void SetModelDrillthroughReportsAsync(string Model, string ModelItemID, ModelDrillthroughReport[] Reports)
		{
			this.SetModelDrillthroughReportsAsync(Model, ModelItemID, Reports, null);
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x0001164C File Offset: 0x0000F84C
		public void SetModelDrillthroughReportsAsync(string Model, string ModelItemID, ModelDrillthroughReport[] Reports, object userState)
		{
			if (this.SetModelDrillthroughReportsOperationCompleted == null)
			{
				this.SetModelDrillthroughReportsOperationCompleted = new SendOrPostCallback(this.OnSetModelDrillthroughReportsOperationCompleted);
			}
			base.InvokeAsync("SetModelDrillthroughReports", new object[] { Model, ModelItemID, Reports }, this.SetModelDrillthroughReportsOperationCompleted, userState);
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x00011698 File Offset: 0x0000F898
		private void OnSetModelDrillthroughReportsOperationCompleted(object arg)
		{
			if (this.SetModelDrillthroughReportsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetModelDrillthroughReportsCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x000116D7 File Offset: 0x0000F8D7
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/ListModelDrillthroughReports", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Reports")]
		public ModelDrillthroughReport[] ListModelDrillthroughReports(string Model, string ModelItemID)
		{
			return (ModelDrillthroughReport[])base.Invoke("ListModelDrillthroughReports", new object[] { Model, ModelItemID })[0];
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x000116F9 File Offset: 0x0000F8F9
		public IAsyncResult BeginListModelDrillthroughReports(string Model, string ModelItemID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListModelDrillthroughReports", new object[] { Model, ModelItemID }, callback, asyncState);
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x00011717 File Offset: 0x0000F917
		public ModelDrillthroughReport[] EndListModelDrillthroughReports(IAsyncResult asyncResult)
		{
			return (ModelDrillthroughReport[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x00011727 File Offset: 0x0000F927
		public void ListModelDrillthroughReportsAsync(string Model, string ModelItemID)
		{
			this.ListModelDrillthroughReportsAsync(Model, ModelItemID, null);
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x00011732 File Offset: 0x0000F932
		public void ListModelDrillthroughReportsAsync(string Model, string ModelItemID, object userState)
		{
			if (this.ListModelDrillthroughReportsOperationCompleted == null)
			{
				this.ListModelDrillthroughReportsOperationCompleted = new SendOrPostCallback(this.OnListModelDrillthroughReportsOperationCompleted);
			}
			base.InvokeAsync("ListModelDrillthroughReports", new object[] { Model, ModelItemID }, this.ListModelDrillthroughReportsOperationCompleted, userState);
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x00011770 File Offset: 0x0000F970
		private void OnListModelDrillthroughReportsOperationCompleted(object arg)
		{
			if (this.ListModelDrillthroughReportsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListModelDrillthroughReportsCompleted(this, new ListModelDrillthroughReportsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x000117B8 File Offset: 0x0000F9B8
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/GenerateModel", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("ItemInfo")]
		public CatalogItem GenerateModel(string DataSource, string Model, string Parent, Property[] Properties, out Warning[] Warnings)
		{
			object[] array = base.Invoke("GenerateModel", new object[] { DataSource, Model, Parent, Properties });
			Warnings = (Warning[])array[1];
			return (CatalogItem)array[0];
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x000117FB File Offset: 0x0000F9FB
		public IAsyncResult BeginGenerateModel(string DataSource, string Model, string Parent, Property[] Properties, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GenerateModel", new object[] { DataSource, Model, Parent, Properties }, callback, asyncState);
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x00011824 File Offset: 0x0000FA24
		public CatalogItem EndGenerateModel(IAsyncResult asyncResult, out Warning[] Warnings)
		{
			object[] array = base.EndInvoke(asyncResult);
			Warnings = (Warning[])array[1];
			return (CatalogItem)array[0];
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x0001184B File Offset: 0x0000FA4B
		public void GenerateModelAsync(string DataSource, string Model, string Parent, Property[] Properties)
		{
			this.GenerateModelAsync(DataSource, Model, Parent, Properties, null);
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x0001185C File Offset: 0x0000FA5C
		public void GenerateModelAsync(string DataSource, string Model, string Parent, Property[] Properties, object userState)
		{
			if (this.GenerateModelOperationCompleted == null)
			{
				this.GenerateModelOperationCompleted = new SendOrPostCallback(this.OnGenerateModelOperationCompleted);
			}
			base.InvokeAsync("GenerateModel", new object[] { DataSource, Model, Parent, Properties }, this.GenerateModelOperationCompleted, userState);
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x000118B0 File Offset: 0x0000FAB0
		private void OnGenerateModelOperationCompleted(object arg)
		{
			if (this.GenerateModelCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GenerateModelCompleted(this, new GenerateModelCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x000118F5 File Offset: 0x0000FAF5
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/RegenerateModel", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Warnings")]
		public Warning[] RegenerateModel(string Model)
		{
			return (Warning[])base.Invoke("RegenerateModel", new object[] { Model })[0];
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x00011913 File Offset: 0x0000FB13
		public IAsyncResult BeginRegenerateModel(string Model, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("RegenerateModel", new object[] { Model }, callback, asyncState);
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x0001192C File Offset: 0x0000FB2C
		public Warning[] EndRegenerateModel(IAsyncResult asyncResult)
		{
			return (Warning[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x0001193C File Offset: 0x0000FB3C
		public void RegenerateModelAsync(string Model)
		{
			this.RegenerateModelAsync(Model, null);
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x00011946 File Offset: 0x0000FB46
		public void RegenerateModelAsync(string Model, object userState)
		{
			if (this.RegenerateModelOperationCompleted == null)
			{
				this.RegenerateModelOperationCompleted = new SendOrPostCallback(this.OnRegenerateModelOperationCompleted);
			}
			base.InvokeAsync("RegenerateModel", new object[] { Model }, this.RegenerateModelOperationCompleted, userState);
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x00011980 File Offset: 0x0000FB80
		private void OnRegenerateModelOperationCompleted(object arg)
		{
			if (this.RegenerateModelCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.RegenerateModelCompleted(this, new RegenerateModelCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x000119C5 File Offset: 0x0000FBC5
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/GetReportServerConfigInfo", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("ServerConfigInfos")]
		public ServerConfigInfo[] GetReportServerConfigInfo(bool ScaleOut)
		{
			return (ServerConfigInfo[])base.Invoke("GetReportServerConfigInfo", new object[] { ScaleOut })[0];
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x000119E8 File Offset: 0x0000FBE8
		public IAsyncResult BeginGetReportServerConfigInfo(bool ScaleOut, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetReportServerConfigInfo", new object[] { ScaleOut }, callback, asyncState);
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x00011A06 File Offset: 0x0000FC06
		public ServerConfigInfo[] EndGetReportServerConfigInfo(IAsyncResult asyncResult)
		{
			return (ServerConfigInfo[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x00011A16 File Offset: 0x0000FC16
		public void GetReportServerConfigInfoAsync(bool ScaleOut)
		{
			this.GetReportServerConfigInfoAsync(ScaleOut, null);
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x00011A20 File Offset: 0x0000FC20
		public void GetReportServerConfigInfoAsync(bool ScaleOut, object userState)
		{
			if (this.GetReportServerConfigInfoOperationCompleted == null)
			{
				this.GetReportServerConfigInfoOperationCompleted = new SendOrPostCallback(this.OnGetReportServerConfigInfoOperationCompleted);
			}
			base.InvokeAsync("GetReportServerConfigInfo", new object[] { ScaleOut }, this.GetReportServerConfigInfoOperationCompleted, userState);
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x00011A60 File Offset: 0x0000FC60
		private void OnGetReportServerConfigInfoOperationCompleted(object arg)
		{
			if (this.GetReportServerConfigInfoCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetReportServerConfigInfoCompleted(this, new GetReportServerConfigInfoCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x00011AA5 File Offset: 0x0000FCA5
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/ListSecureMethods", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public string[] ListSecureMethods()
		{
			return (string[])base.Invoke("ListSecureMethods", new object[0])[0];
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x00011ABF File Offset: 0x0000FCBF
		public IAsyncResult BeginListSecureMethods(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListSecureMethods", new object[0], callback, asyncState);
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x00011AD4 File Offset: 0x0000FCD4
		public string[] EndListSecureMethods(IAsyncResult asyncResult)
		{
			return (string[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x00011AE4 File Offset: 0x0000FCE4
		public void ListSecureMethodsAsync()
		{
			this.ListSecureMethodsAsync(null);
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x00011AED File Offset: 0x0000FCED
		public void ListSecureMethodsAsync(object userState)
		{
			if (this.ListSecureMethodsOperationCompleted == null)
			{
				this.ListSecureMethodsOperationCompleted = new SendOrPostCallback(this.OnListSecureMethodsOperationCompleted);
			}
			base.InvokeAsync("ListSecureMethods", new object[0], this.ListSecureMethodsOperationCompleted, userState);
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x00011B24 File Offset: 0x0000FD24
		private void OnListSecureMethodsOperationCompleted(object arg)
		{
			if (this.ListSecureMethodsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListSecureMethodsCompleted(this, new ListSecureMethodsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x00011B69 File Offset: 0x0000FD69
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/GetSystemProperties", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Values")]
		public Property[] GetSystemProperties(Property[] Properties)
		{
			return (Property[])base.Invoke("GetSystemProperties", new object[] { Properties })[0];
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x00011B87 File Offset: 0x0000FD87
		public IAsyncResult BeginGetSystemProperties(Property[] Properties, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetSystemProperties", new object[] { Properties }, callback, asyncState);
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x00011BA0 File Offset: 0x0000FDA0
		public Property[] EndGetSystemProperties(IAsyncResult asyncResult)
		{
			return (Property[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x00011BB0 File Offset: 0x0000FDB0
		public void GetSystemPropertiesAsync(Property[] Properties)
		{
			this.GetSystemPropertiesAsync(Properties, null);
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x00011BBA File Offset: 0x0000FDBA
		public void GetSystemPropertiesAsync(Property[] Properties, object userState)
		{
			if (this.GetSystemPropertiesOperationCompleted == null)
			{
				this.GetSystemPropertiesOperationCompleted = new SendOrPostCallback(this.OnGetSystemPropertiesOperationCompleted);
			}
			base.InvokeAsync("GetSystemProperties", new object[] { Properties }, this.GetSystemPropertiesOperationCompleted, userState);
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x00011BF4 File Offset: 0x0000FDF4
		private void OnGetSystemPropertiesOperationCompleted(object arg)
		{
			if (this.GetSystemPropertiesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetSystemPropertiesCompleted(this, new GetSystemPropertiesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x00011C39 File Offset: 0x0000FE39
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/SetSystemProperties", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void SetSystemProperties(Property[] Properties)
		{
			base.Invoke("SetSystemProperties", new object[] { Properties });
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x00011C51 File Offset: 0x0000FE51
		public IAsyncResult BeginSetSystemProperties(Property[] Properties, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetSystemProperties", new object[] { Properties }, callback, asyncState);
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x00011C6A File Offset: 0x0000FE6A
		public void EndSetSystemProperties(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x00011C74 File Offset: 0x0000FE74
		public void SetSystemPropertiesAsync(Property[] Properties)
		{
			this.SetSystemPropertiesAsync(Properties, null);
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x00011C7E File Offset: 0x0000FE7E
		public void SetSystemPropertiesAsync(Property[] Properties, object userState)
		{
			if (this.SetSystemPropertiesOperationCompleted == null)
			{
				this.SetSystemPropertiesOperationCompleted = new SendOrPostCallback(this.OnSetSystemPropertiesOperationCompleted);
			}
			base.InvokeAsync("SetSystemProperties", new object[] { Properties }, this.SetSystemPropertiesOperationCompleted, userState);
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x00011CB8 File Offset: 0x0000FEB8
		private void OnSetSystemPropertiesOperationCompleted(object arg)
		{
			if (this.SetSystemPropertiesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetSystemPropertiesCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x00011CF7 File Offset: 0x0000FEF7
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/DeleteItem", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void DeleteItem(string Item)
		{
			base.Invoke("DeleteItem", new object[] { Item });
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x00011D0F File Offset: 0x0000FF0F
		public IAsyncResult BeginDeleteItem(string Item, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("DeleteItem", new object[] { Item }, callback, asyncState);
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x00011D28 File Offset: 0x0000FF28
		public void EndDeleteItem(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x00011D32 File Offset: 0x0000FF32
		public void DeleteItemAsync(string Item)
		{
			this.DeleteItemAsync(Item, null);
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x00011D3C File Offset: 0x0000FF3C
		public void DeleteItemAsync(string Item, object userState)
		{
			if (this.DeleteItemOperationCompleted == null)
			{
				this.DeleteItemOperationCompleted = new SendOrPostCallback(this.OnDeleteItemOperationCompleted);
			}
			base.InvokeAsync("DeleteItem", new object[] { Item }, this.DeleteItemOperationCompleted, userState);
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x00011D74 File Offset: 0x0000FF74
		private void OnDeleteItemOperationCompleted(object arg)
		{
			if (this.DeleteItemCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.DeleteItemCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x00011DB3 File Offset: 0x0000FFB3
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/MoveItem", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void MoveItem(string Item, string Target)
		{
			base.Invoke("MoveItem", new object[] { Item, Target });
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x00011DCF File Offset: 0x0000FFCF
		public IAsyncResult BeginMoveItem(string Item, string Target, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("MoveItem", new object[] { Item, Target }, callback, asyncState);
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x00011DED File Offset: 0x0000FFED
		public void EndMoveItem(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x00011DF7 File Offset: 0x0000FFF7
		public void MoveItemAsync(string Item, string Target)
		{
			this.MoveItemAsync(Item, Target, null);
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x00011E02 File Offset: 0x00010002
		public void MoveItemAsync(string Item, string Target, object userState)
		{
			if (this.MoveItemOperationCompleted == null)
			{
				this.MoveItemOperationCompleted = new SendOrPostCallback(this.OnMoveItemOperationCompleted);
			}
			base.InvokeAsync("MoveItem", new object[] { Item, Target }, this.MoveItemOperationCompleted, userState);
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x00011E40 File Offset: 0x00010040
		private void OnMoveItemOperationCompleted(object arg)
		{
			if (this.MoveItemCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.MoveItemCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x00011E7F File Offset: 0x0001007F
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/ListChildren", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("CatalogItems")]
		public CatalogItem[] ListChildren(string Item)
		{
			return (CatalogItem[])base.Invoke("ListChildren", new object[] { Item })[0];
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x00011E9D File Offset: 0x0001009D
		public IAsyncResult BeginListChildren(string Item, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListChildren", new object[] { Item }, callback, asyncState);
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x00011EB6 File Offset: 0x000100B6
		public CatalogItem[] EndListChildren(IAsyncResult asyncResult)
		{
			return (CatalogItem[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x00011EC6 File Offset: 0x000100C6
		public void ListChildrenAsync(string Item)
		{
			this.ListChildrenAsync(Item, null);
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x00011ED0 File Offset: 0x000100D0
		public void ListChildrenAsync(string Item, object userState)
		{
			if (this.ListChildrenOperationCompleted == null)
			{
				this.ListChildrenOperationCompleted = new SendOrPostCallback(this.OnListChildrenOperationCompleted);
			}
			base.InvokeAsync("ListChildren", new object[] { Item }, this.ListChildrenOperationCompleted, userState);
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x00011F08 File Offset: 0x00010108
		private void OnListChildrenOperationCompleted(object arg)
		{
			if (this.ListChildrenCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListChildrenCompleted(this, new ListChildrenCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x00011F4D File Offset: 0x0001014D
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/ListParents", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public CatalogItem[] ListParents(string Item)
		{
			return (CatalogItem[])base.Invoke("ListParents", new object[] { Item })[0];
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x00011F6B File Offset: 0x0001016B
		public IAsyncResult BeginListParents(string Item, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListParents", new object[] { Item }, callback, asyncState);
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x00011F84 File Offset: 0x00010184
		public CatalogItem[] EndListParents(IAsyncResult asyncResult)
		{
			return (CatalogItem[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x00011F94 File Offset: 0x00010194
		public void ListParentsAsync(string Item)
		{
			this.ListParentsAsync(Item, null);
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x00011F9E File Offset: 0x0001019E
		public void ListParentsAsync(string Item, object userState)
		{
			if (this.ListParentsOperationCompleted == null)
			{
				this.ListParentsOperationCompleted = new SendOrPostCallback(this.OnListParentsOperationCompleted);
			}
			base.InvokeAsync("ListParents", new object[] { Item }, this.ListParentsOperationCompleted, userState);
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x00011FD8 File Offset: 0x000101D8
		private void OnListParentsOperationCompleted(object arg)
		{
			if (this.ListParentsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListParentsCompleted(this, new ListParentsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x0001201D File Offset: 0x0001021D
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/ListDependentItems", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("CatalogItems")]
		public CatalogItem[] ListDependentItems(string Item)
		{
			return (CatalogItem[])base.Invoke("ListDependentItems", new object[] { Item })[0];
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x0001203B File Offset: 0x0001023B
		public IAsyncResult BeginListDependentItems(string Item, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListDependentItems", new object[] { Item }, callback, asyncState);
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x00012054 File Offset: 0x00010254
		public CatalogItem[] EndListDependentItems(IAsyncResult asyncResult)
		{
			return (CatalogItem[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x00012064 File Offset: 0x00010264
		public void ListDependentItemsAsync(string Item)
		{
			this.ListDependentItemsAsync(Item, null);
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x0001206E File Offset: 0x0001026E
		public void ListDependentItemsAsync(string Item, object userState)
		{
			if (this.ListDependentItemsOperationCompleted == null)
			{
				this.ListDependentItemsOperationCompleted = new SendOrPostCallback(this.OnListDependentItemsOperationCompleted);
			}
			base.InvokeAsync("ListDependentItems", new object[] { Item }, this.ListDependentItemsOperationCompleted, userState);
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x000120A8 File Offset: 0x000102A8
		private void OnListDependentItemsOperationCompleted(object arg)
		{
			if (this.ListDependentItemsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListDependentItemsCompleted(this, new ListDependentItemsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x000120ED File Offset: 0x000102ED
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/GetProperties", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Values")]
		public Property[] GetProperties(string Item, Property[] Properties)
		{
			return (Property[])base.Invoke("GetProperties", new object[] { Item, Properties })[0];
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x0001210F File Offset: 0x0001030F
		public IAsyncResult BeginGetProperties(string Item, Property[] Properties, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetProperties", new object[] { Item, Properties }, callback, asyncState);
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x0001212D File Offset: 0x0001032D
		public Property[] EndGetProperties(IAsyncResult asyncResult)
		{
			return (Property[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x0001213D File Offset: 0x0001033D
		public void GetPropertiesAsync(string Item, Property[] Properties)
		{
			this.GetPropertiesAsync(Item, Properties, null);
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x00012148 File Offset: 0x00010348
		public void GetPropertiesAsync(string Item, Property[] Properties, object userState)
		{
			if (this.GetPropertiesOperationCompleted == null)
			{
				this.GetPropertiesOperationCompleted = new SendOrPostCallback(this.OnGetPropertiesOperationCompleted);
			}
			base.InvokeAsync("GetProperties", new object[] { Item, Properties }, this.GetPropertiesOperationCompleted, userState);
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x00012184 File Offset: 0x00010384
		private void OnGetPropertiesOperationCompleted(object arg)
		{
			if (this.GetPropertiesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetPropertiesCompleted(this, new GetPropertiesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x000121C9 File Offset: 0x000103C9
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/SetProperties", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void SetProperties(string Item, Property[] Properties)
		{
			base.Invoke("SetProperties", new object[] { Item, Properties });
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x000121E5 File Offset: 0x000103E5
		public IAsyncResult BeginSetProperties(string Item, Property[] Properties, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetProperties", new object[] { Item, Properties }, callback, asyncState);
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x00012203 File Offset: 0x00010403
		public void EndSetProperties(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x0001220D File Offset: 0x0001040D
		public void SetPropertiesAsync(string Item, Property[] Properties)
		{
			this.SetPropertiesAsync(Item, Properties, null);
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x00012218 File Offset: 0x00010418
		public void SetPropertiesAsync(string Item, Property[] Properties, object userState)
		{
			if (this.SetPropertiesOperationCompleted == null)
			{
				this.SetPropertiesOperationCompleted = new SendOrPostCallback(this.OnSetPropertiesOperationCompleted);
			}
			base.InvokeAsync("SetProperties", new object[] { Item, Properties }, this.SetPropertiesOperationCompleted, userState);
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x00012254 File Offset: 0x00010454
		private void OnSetPropertiesOperationCompleted(object arg)
		{
			if (this.SetPropertiesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetPropertiesCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x00012293 File Offset: 0x00010493
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/GetItemType", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Type")]
		public ItemTypeEnum GetItemType(string Item)
		{
			return (ItemTypeEnum)base.Invoke("GetItemType", new object[] { Item })[0];
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x000122B1 File Offset: 0x000104B1
		public IAsyncResult BeginGetItemType(string Item, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetItemType", new object[] { Item }, callback, asyncState);
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x000122CA File Offset: 0x000104CA
		public ItemTypeEnum EndGetItemType(IAsyncResult asyncResult)
		{
			return (ItemTypeEnum)base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x000122DA File Offset: 0x000104DA
		public void GetItemTypeAsync(string Item)
		{
			this.GetItemTypeAsync(Item, null);
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x000122E4 File Offset: 0x000104E4
		public void GetItemTypeAsync(string Item, object userState)
		{
			if (this.GetItemTypeOperationCompleted == null)
			{
				this.GetItemTypeOperationCompleted = new SendOrPostCallback(this.OnGetItemTypeOperationCompleted);
			}
			base.InvokeAsync("GetItemType", new object[] { Item }, this.GetItemTypeOperationCompleted, userState);
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x0001231C File Offset: 0x0001051C
		private void OnGetItemTypeOperationCompleted(object arg)
		{
			if (this.GetItemTypeCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetItemTypeCompleted(this, new GetItemTypeCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x00012361 File Offset: 0x00010561
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/CreateFolder", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("ItemInfo")]
		public CatalogItem CreateFolder(string Folder, string Parent)
		{
			return (CatalogItem)base.Invoke("CreateFolder", new object[] { Folder, Parent })[0];
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x00012383 File Offset: 0x00010583
		public IAsyncResult BeginCreateFolder(string Folder, string Parent, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateFolder", new object[] { Folder, Parent }, callback, asyncState);
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x000123A1 File Offset: 0x000105A1
		public CatalogItem EndCreateFolder(IAsyncResult asyncResult)
		{
			return (CatalogItem)base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x000123B1 File Offset: 0x000105B1
		public void CreateFolderAsync(string Folder, string Parent)
		{
			this.CreateFolderAsync(Folder, Parent, null);
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x000123BC File Offset: 0x000105BC
		public void CreateFolderAsync(string Folder, string Parent, object userState)
		{
			if (this.CreateFolderOperationCompleted == null)
			{
				this.CreateFolderOperationCompleted = new SendOrPostCallback(this.OnCreateFolderOperationCompleted);
			}
			base.InvokeAsync("CreateFolder", new object[] { Folder, Parent }, this.CreateFolderOperationCompleted, userState);
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x000123F8 File Offset: 0x000105F8
		private void OnCreateFolderOperationCompleted(object arg)
		{
			if (this.CreateFolderCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateFolderCompleted(this, new CreateFolderCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x00012440 File Offset: 0x00010640
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/CreateReport", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("ItemInfo")]
		public CatalogItem CreateReport(string Report, string Parent, bool Overwrite, [XmlElement(DataType = "base64Binary")] byte[] Definition, Property[] Properties, out Warning[] Warnings)
		{
			object[] array = base.Invoke("CreateReport", new object[] { Report, Parent, Overwrite, Definition, Properties });
			Warnings = (Warning[])array[1];
			return (CatalogItem)array[0];
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x0001248D File Offset: 0x0001068D
		public IAsyncResult BeginCreateReport(string Report, string Parent, bool Overwrite, byte[] Definition, Property[] Properties, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateReport", new object[] { Report, Parent, Overwrite, Definition, Properties }, callback, asyncState);
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x000124C0 File Offset: 0x000106C0
		public CatalogItem EndCreateReport(IAsyncResult asyncResult, out Warning[] Warnings)
		{
			object[] array = base.EndInvoke(asyncResult);
			Warnings = (Warning[])array[1];
			return (CatalogItem)array[0];
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x000124E7 File Offset: 0x000106E7
		public void CreateReportAsync(string Report, string Parent, bool Overwrite, byte[] Definition, Property[] Properties)
		{
			this.CreateReportAsync(Report, Parent, Overwrite, Definition, Properties, null);
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x000124F8 File Offset: 0x000106F8
		public void CreateReportAsync(string Report, string Parent, bool Overwrite, byte[] Definition, Property[] Properties, object userState)
		{
			if (this.CreateReportOperationCompleted == null)
			{
				this.CreateReportOperationCompleted = new SendOrPostCallback(this.OnCreateReportOperationCompleted);
			}
			base.InvokeAsync("CreateReport", new object[] { Report, Parent, Overwrite, Definition, Properties }, this.CreateReportOperationCompleted, userState);
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x00012554 File Offset: 0x00010754
		private void OnCreateReportOperationCompleted(object arg)
		{
			if (this.CreateReportCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateReportCompleted(this, new CreateReportCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x00012599 File Offset: 0x00010799
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/GetReportDefinition", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Definition", DataType = "base64Binary")]
		public byte[] GetReportDefinition(string Report)
		{
			return (byte[])base.Invoke("GetReportDefinition", new object[] { Report })[0];
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x000125B7 File Offset: 0x000107B7
		public IAsyncResult BeginGetReportDefinition(string Report, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetReportDefinition", new object[] { Report }, callback, asyncState);
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x000125D0 File Offset: 0x000107D0
		public byte[] EndGetReportDefinition(IAsyncResult asyncResult)
		{
			return (byte[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x000125E0 File Offset: 0x000107E0
		public void GetReportDefinitionAsync(string Report)
		{
			this.GetReportDefinitionAsync(Report, null);
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x000125EA File Offset: 0x000107EA
		public void GetReportDefinitionAsync(string Report, object userState)
		{
			if (this.GetReportDefinitionOperationCompleted == null)
			{
				this.GetReportDefinitionOperationCompleted = new SendOrPostCallback(this.OnGetReportDefinitionOperationCompleted);
			}
			base.InvokeAsync("GetReportDefinition", new object[] { Report }, this.GetReportDefinitionOperationCompleted, userState);
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x00012624 File Offset: 0x00010824
		private void OnGetReportDefinitionOperationCompleted(object arg)
		{
			if (this.GetReportDefinitionCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetReportDefinitionCompleted(this, new GetReportDefinitionCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x00012669 File Offset: 0x00010869
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/SetReportDefinition", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Warnings")]
		public Warning[] SetReportDefinition(string Report, [XmlElement(DataType = "base64Binary")] byte[] Definition)
		{
			return (Warning[])base.Invoke("SetReportDefinition", new object[] { Report, Definition })[0];
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x0001268B File Offset: 0x0001088B
		public IAsyncResult BeginSetReportDefinition(string Report, byte[] Definition, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetReportDefinition", new object[] { Report, Definition }, callback, asyncState);
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x000126A9 File Offset: 0x000108A9
		public Warning[] EndSetReportDefinition(IAsyncResult asyncResult)
		{
			return (Warning[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x000126B9 File Offset: 0x000108B9
		public void SetReportDefinitionAsync(string Report, byte[] Definition)
		{
			this.SetReportDefinitionAsync(Report, Definition, null);
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x000126C4 File Offset: 0x000108C4
		public void SetReportDefinitionAsync(string Report, byte[] Definition, object userState)
		{
			if (this.SetReportDefinitionOperationCompleted == null)
			{
				this.SetReportDefinitionOperationCompleted = new SendOrPostCallback(this.OnSetReportDefinitionOperationCompleted);
			}
			base.InvokeAsync("SetReportDefinition", new object[] { Report, Definition }, this.SetReportDefinitionOperationCompleted, userState);
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x00012700 File Offset: 0x00010900
		private void OnSetReportDefinitionOperationCompleted(object arg)
		{
			if (this.SetReportDefinitionCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetReportDefinitionCompleted(this, new SetReportDefinitionCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x00012745 File Offset: 0x00010945
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/CreateResource", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("ItemInfo")]
		public CatalogItem CreateResource(string Resource, string Parent, bool Overwrite, [XmlElement(DataType = "base64Binary")] byte[] Contents, string MimeType, Property[] Properties)
		{
			return (CatalogItem)base.Invoke("CreateResource", new object[] { Resource, Parent, Overwrite, Contents, MimeType, Properties })[0];
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x0001277F File Offset: 0x0001097F
		public IAsyncResult BeginCreateResource(string Resource, string Parent, bool Overwrite, byte[] Contents, string MimeType, Property[] Properties, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateResource", new object[] { Resource, Parent, Overwrite, Contents, MimeType, Properties }, callback, asyncState);
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x000127B6 File Offset: 0x000109B6
		public CatalogItem EndCreateResource(IAsyncResult asyncResult)
		{
			return (CatalogItem)base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x000127C6 File Offset: 0x000109C6
		public void CreateResourceAsync(string Resource, string Parent, bool Overwrite, byte[] Contents, string MimeType, Property[] Properties)
		{
			this.CreateResourceAsync(Resource, Parent, Overwrite, Contents, MimeType, Properties, null);
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x000127D8 File Offset: 0x000109D8
		public void CreateResourceAsync(string Resource, string Parent, bool Overwrite, byte[] Contents, string MimeType, Property[] Properties, object userState)
		{
			if (this.CreateResourceOperationCompleted == null)
			{
				this.CreateResourceOperationCompleted = new SendOrPostCallback(this.OnCreateResourceOperationCompleted);
			}
			base.InvokeAsync("CreateResource", new object[] { Resource, Parent, Overwrite, Contents, MimeType, Properties }, this.CreateResourceOperationCompleted, userState);
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x00012838 File Offset: 0x00010A38
		private void OnCreateResourceOperationCompleted(object arg)
		{
			if (this.CreateResourceCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateResourceCompleted(this, new CreateResourceCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x0001287D File Offset: 0x00010A7D
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/SetResourceContents", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void SetResourceContents(string Resource, [XmlElement(DataType = "base64Binary")] byte[] Contents, string MimeType)
		{
			base.Invoke("SetResourceContents", new object[] { Resource, Contents, MimeType });
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x0001289D File Offset: 0x00010A9D
		public IAsyncResult BeginSetResourceContents(string Resource, byte[] Contents, string MimeType, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetResourceContents", new object[] { Resource, Contents, MimeType }, callback, asyncState);
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x000128C0 File Offset: 0x00010AC0
		public void EndSetResourceContents(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x000128CA File Offset: 0x00010ACA
		public void SetResourceContentsAsync(string Resource, byte[] Contents, string MimeType)
		{
			this.SetResourceContentsAsync(Resource, Contents, MimeType, null);
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x000128D8 File Offset: 0x00010AD8
		public void SetResourceContentsAsync(string Resource, byte[] Contents, string MimeType, object userState)
		{
			if (this.SetResourceContentsOperationCompleted == null)
			{
				this.SetResourceContentsOperationCompleted = new SendOrPostCallback(this.OnSetResourceContentsOperationCompleted);
			}
			base.InvokeAsync("SetResourceContents", new object[] { Resource, Contents, MimeType }, this.SetResourceContentsOperationCompleted, userState);
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x00012924 File Offset: 0x00010B24
		private void OnSetResourceContentsOperationCompleted(object arg)
		{
			if (this.SetResourceContentsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetResourceContentsCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x00012964 File Offset: 0x00010B64
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/GetResourceContents", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Contents", DataType = "base64Binary")]
		public byte[] GetResourceContents(string Resource, out string MimeType)
		{
			object[] array = base.Invoke("GetResourceContents", new object[] { Resource });
			MimeType = (string)array[1];
			return (byte[])array[0];
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x00012999 File Offset: 0x00010B99
		public IAsyncResult BeginGetResourceContents(string Resource, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetResourceContents", new object[] { Resource }, callback, asyncState);
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x000129B4 File Offset: 0x00010BB4
		public byte[] EndGetResourceContents(IAsyncResult asyncResult, out string MimeType)
		{
			object[] array = base.EndInvoke(asyncResult);
			MimeType = (string)array[1];
			return (byte[])array[0];
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x000129DB File Offset: 0x00010BDB
		public void GetResourceContentsAsync(string Resource)
		{
			this.GetResourceContentsAsync(Resource, null);
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x000129E5 File Offset: 0x00010BE5
		public void GetResourceContentsAsync(string Resource, object userState)
		{
			if (this.GetResourceContentsOperationCompleted == null)
			{
				this.GetResourceContentsOperationCompleted = new SendOrPostCallback(this.OnGetResourceContentsOperationCompleted);
			}
			base.InvokeAsync("GetResourceContents", new object[] { Resource }, this.GetResourceContentsOperationCompleted, userState);
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x00012A20 File Offset: 0x00010C20
		private void OnGetResourceContentsOperationCompleted(object arg)
		{
			if (this.GetResourceContentsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetResourceContentsCompleted(this, new GetResourceContentsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x00012A65 File Offset: 0x00010C65
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/CreateReportEditSession", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("EditSessionID")]
		public string CreateReportEditSession(string Report, string Parent, [XmlElement(DataType = "base64Binary")] byte[] Definition)
		{
			return (string)base.Invoke("CreateReportEditSession", new object[] { Report, Parent, Definition })[0];
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x00012A8B File Offset: 0x00010C8B
		public IAsyncResult BeginCreateReportEditSession(string Report, string Parent, byte[] Definition, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateReportEditSession", new object[] { Report, Parent, Definition }, callback, asyncState);
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x00012AAE File Offset: 0x00010CAE
		public string EndCreateReportEditSession(IAsyncResult asyncResult)
		{
			return (string)base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x00012ABE File Offset: 0x00010CBE
		public void CreateReportEditSessionAsync(string Report, string Parent, byte[] Definition)
		{
			this.CreateReportEditSessionAsync(Report, Parent, Definition, null);
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x00012ACC File Offset: 0x00010CCC
		public void CreateReportEditSessionAsync(string Report, string Parent, byte[] Definition, object userState)
		{
			if (this.CreateReportEditSessionOperationCompleted == null)
			{
				this.CreateReportEditSessionOperationCompleted = new SendOrPostCallback(this.OnCreateReportEditSessionOperationCompleted);
			}
			base.InvokeAsync("CreateReportEditSession", new object[] { Report, Parent, Definition }, this.CreateReportEditSessionOperationCompleted, userState);
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x00012B18 File Offset: 0x00010D18
		private void OnCreateReportEditSessionOperationCompleted(object arg)
		{
			if (this.CreateReportEditSessionCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateReportEditSessionCompleted(this, new CreateReportEditSessionCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x00012B5D File Offset: 0x00010D5D
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/GetReportParameters", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Parameters")]
		public ReportParameter[] GetReportParameters(string Report, string HistoryID, ParameterValue[] Values, DataSourceCredentials[] Credentials)
		{
			return (ReportParameter[])base.Invoke("GetReportParameters", new object[] { Report, HistoryID, Values, Credentials })[0];
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x00012B88 File Offset: 0x00010D88
		public IAsyncResult BeginGetReportParameters(string Report, string HistoryID, ParameterValue[] Values, DataSourceCredentials[] Credentials, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetReportParameters", new object[] { Report, HistoryID, Values, Credentials }, callback, asyncState);
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x00012BB0 File Offset: 0x00010DB0
		public ReportParameter[] EndGetReportParameters(IAsyncResult asyncResult)
		{
			return (ReportParameter[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x00012BC0 File Offset: 0x00010DC0
		public void GetReportParametersAsync(string Report, string HistoryID, ParameterValue[] Values, DataSourceCredentials[] Credentials)
		{
			this.GetReportParametersAsync(Report, HistoryID, Values, Credentials, null);
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x00012BD0 File Offset: 0x00010DD0
		public void GetReportParametersAsync(string Report, string HistoryID, ParameterValue[] Values, DataSourceCredentials[] Credentials, object userState)
		{
			if (this.GetReportParametersOperationCompleted == null)
			{
				this.GetReportParametersOperationCompleted = new SendOrPostCallback(this.OnGetReportParametersOperationCompleted);
			}
			base.InvokeAsync("GetReportParameters", new object[] { Report, HistoryID, Values, Credentials }, this.GetReportParametersOperationCompleted, userState);
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x00012C24 File Offset: 0x00010E24
		private void OnGetReportParametersOperationCompleted(object arg)
		{
			if (this.GetReportParametersCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetReportParametersCompleted(this, new GetReportParametersCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x00012C69 File Offset: 0x00010E69
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/SetReportParameters", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void SetReportParameters(string Report, ReportParameter[] Parameters)
		{
			base.Invoke("SetReportParameters", new object[] { Report, Parameters });
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x00012C85 File Offset: 0x00010E85
		public IAsyncResult BeginSetReportParameters(string Report, ReportParameter[] Parameters, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetReportParameters", new object[] { Report, Parameters }, callback, asyncState);
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x00012CA3 File Offset: 0x00010EA3
		public void EndSetReportParameters(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x00012CAD File Offset: 0x00010EAD
		public void SetReportParametersAsync(string Report, ReportParameter[] Parameters)
		{
			this.SetReportParametersAsync(Report, Parameters, null);
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x00012CB8 File Offset: 0x00010EB8
		public void SetReportParametersAsync(string Report, ReportParameter[] Parameters, object userState)
		{
			if (this.SetReportParametersOperationCompleted == null)
			{
				this.SetReportParametersOperationCompleted = new SendOrPostCallback(this.OnSetReportParametersOperationCompleted);
			}
			base.InvokeAsync("SetReportParameters", new object[] { Report, Parameters }, this.SetReportParametersOperationCompleted, userState);
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x00012CF4 File Offset: 0x00010EF4
		private void OnSetReportParametersOperationCompleted(object arg)
		{
			if (this.SetReportParametersCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetReportParametersCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x00012D33 File Offset: 0x00010F33
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/SetExecutionOptions", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void SetExecutionOptions(string Report, ExecutionSettingEnum ExecutionSetting, [XmlElement("NoSchedule", typeof(NoSchedule))] [XmlElement("ScheduleDefinition", typeof(ScheduleDefinition))] [XmlElement("ScheduleReference", typeof(ScheduleReference))] ScheduleDefinitionOrReference Item)
		{
			base.Invoke("SetExecutionOptions", new object[] { Report, ExecutionSetting, Item });
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x00012D58 File Offset: 0x00010F58
		public IAsyncResult BeginSetExecutionOptions(string Report, ExecutionSettingEnum ExecutionSetting, ScheduleDefinitionOrReference Item, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetExecutionOptions", new object[] { Report, ExecutionSetting, Item }, callback, asyncState);
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x00012D80 File Offset: 0x00010F80
		public void EndSetExecutionOptions(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x00012D8A File Offset: 0x00010F8A
		public void SetExecutionOptionsAsync(string Report, ExecutionSettingEnum ExecutionSetting, ScheduleDefinitionOrReference Item)
		{
			this.SetExecutionOptionsAsync(Report, ExecutionSetting, Item, null);
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x00012D98 File Offset: 0x00010F98
		public void SetExecutionOptionsAsync(string Report, ExecutionSettingEnum ExecutionSetting, ScheduleDefinitionOrReference Item, object userState)
		{
			if (this.SetExecutionOptionsOperationCompleted == null)
			{
				this.SetExecutionOptionsOperationCompleted = new SendOrPostCallback(this.OnSetExecutionOptionsOperationCompleted);
			}
			base.InvokeAsync("SetExecutionOptions", new object[] { Report, ExecutionSetting, Item }, this.SetExecutionOptionsOperationCompleted, userState);
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x00012DEC File Offset: 0x00010FEC
		private void OnSetExecutionOptionsOperationCompleted(object arg)
		{
			if (this.SetExecutionOptionsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetExecutionOptionsCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x00012E2C File Offset: 0x0001102C
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/GetExecutionOptions", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("ExecutionSetting")]
		public ExecutionSettingEnum GetExecutionOptions(string Report, [XmlElement("NoSchedule", typeof(NoSchedule))] [XmlElement("ScheduleDefinition", typeof(ScheduleDefinition))] [XmlElement("ScheduleReference", typeof(ScheduleReference))] out ScheduleDefinitionOrReference Item)
		{
			object[] array = base.Invoke("GetExecutionOptions", new object[] { Report });
			Item = (ScheduleDefinitionOrReference)array[1];
			return (ExecutionSettingEnum)array[0];
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x00012E61 File Offset: 0x00011061
		public IAsyncResult BeginGetExecutionOptions(string Report, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetExecutionOptions", new object[] { Report }, callback, asyncState);
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x00012E7C File Offset: 0x0001107C
		public ExecutionSettingEnum EndGetExecutionOptions(IAsyncResult asyncResult, out ScheduleDefinitionOrReference Item)
		{
			object[] array = base.EndInvoke(asyncResult);
			Item = (ScheduleDefinitionOrReference)array[1];
			return (ExecutionSettingEnum)array[0];
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x00012EA3 File Offset: 0x000110A3
		public void GetExecutionOptionsAsync(string Report)
		{
			this.GetExecutionOptionsAsync(Report, null);
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x00012EAD File Offset: 0x000110AD
		public void GetExecutionOptionsAsync(string Report, object userState)
		{
			if (this.GetExecutionOptionsOperationCompleted == null)
			{
				this.GetExecutionOptionsOperationCompleted = new SendOrPostCallback(this.OnGetExecutionOptionsOperationCompleted);
			}
			base.InvokeAsync("GetExecutionOptions", new object[] { Report }, this.GetExecutionOptionsOperationCompleted, userState);
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x00012EE8 File Offset: 0x000110E8
		private void OnGetExecutionOptionsOperationCompleted(object arg)
		{
			if (this.GetExecutionOptionsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetExecutionOptionsCompleted(this, new GetExecutionOptionsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x00012F2D File Offset: 0x0001112D
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/SetCacheOptions", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void SetCacheOptions(string Report, bool CacheReport, [XmlElement("ScheduleExpiration", typeof(ScheduleExpiration))] [XmlElement("TimeExpiration", typeof(TimeExpiration))] ExpirationDefinition Item)
		{
			base.Invoke("SetCacheOptions", new object[] { Report, CacheReport, Item });
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x00012F52 File Offset: 0x00011152
		public IAsyncResult BeginSetCacheOptions(string Report, bool CacheReport, ExpirationDefinition Item, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetCacheOptions", new object[] { Report, CacheReport, Item }, callback, asyncState);
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x00012F7A File Offset: 0x0001117A
		public void EndSetCacheOptions(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x00012F84 File Offset: 0x00011184
		public void SetCacheOptionsAsync(string Report, bool CacheReport, ExpirationDefinition Item)
		{
			this.SetCacheOptionsAsync(Report, CacheReport, Item, null);
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x00012F90 File Offset: 0x00011190
		public void SetCacheOptionsAsync(string Report, bool CacheReport, ExpirationDefinition Item, object userState)
		{
			if (this.SetCacheOptionsOperationCompleted == null)
			{
				this.SetCacheOptionsOperationCompleted = new SendOrPostCallback(this.OnSetCacheOptionsOperationCompleted);
			}
			base.InvokeAsync("SetCacheOptions", new object[] { Report, CacheReport, Item }, this.SetCacheOptionsOperationCompleted, userState);
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x00012FE4 File Offset: 0x000111E4
		private void OnSetCacheOptionsOperationCompleted(object arg)
		{
			if (this.SetCacheOptionsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetCacheOptionsCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x00013024 File Offset: 0x00011224
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/GetCacheOptions", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("CacheReport")]
		public bool GetCacheOptions(string Report, [XmlElement("ScheduleExpiration", typeof(ScheduleExpiration))] [XmlElement("TimeExpiration", typeof(TimeExpiration))] out ExpirationDefinition Item)
		{
			object[] array = base.Invoke("GetCacheOptions", new object[] { Report });
			Item = (ExpirationDefinition)array[1];
			return (bool)array[0];
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x00013059 File Offset: 0x00011259
		public IAsyncResult BeginGetCacheOptions(string Report, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetCacheOptions", new object[] { Report }, callback, asyncState);
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x00013074 File Offset: 0x00011274
		public bool EndGetCacheOptions(IAsyncResult asyncResult, out ExpirationDefinition Item)
		{
			object[] array = base.EndInvoke(asyncResult);
			Item = (ExpirationDefinition)array[1];
			return (bool)array[0];
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x0001309B File Offset: 0x0001129B
		public void GetCacheOptionsAsync(string Report)
		{
			this.GetCacheOptionsAsync(Report, null);
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x000130A5 File Offset: 0x000112A5
		public void GetCacheOptionsAsync(string Report, object userState)
		{
			if (this.GetCacheOptionsOperationCompleted == null)
			{
				this.GetCacheOptionsOperationCompleted = new SendOrPostCallback(this.OnGetCacheOptionsOperationCompleted);
			}
			base.InvokeAsync("GetCacheOptions", new object[] { Report }, this.GetCacheOptionsOperationCompleted, userState);
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x000130E0 File Offset: 0x000112E0
		private void OnGetCacheOptionsOperationCompleted(object arg)
		{
			if (this.GetCacheOptionsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetCacheOptionsCompleted(this, new GetCacheOptionsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x00013125 File Offset: 0x00011325
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/UpdateReportExecutionSnapshot", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void UpdateReportExecutionSnapshot(string Report)
		{
			base.Invoke("UpdateReportExecutionSnapshot", new object[] { Report });
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x0001313D File Offset: 0x0001133D
		public IAsyncResult BeginUpdateReportExecutionSnapshot(string Report, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("UpdateReportExecutionSnapshot", new object[] { Report }, callback, asyncState);
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x00013156 File Offset: 0x00011356
		public void EndUpdateReportExecutionSnapshot(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x00013160 File Offset: 0x00011360
		public void UpdateReportExecutionSnapshotAsync(string Report)
		{
			this.UpdateReportExecutionSnapshotAsync(Report, null);
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x0001316A File Offset: 0x0001136A
		public void UpdateReportExecutionSnapshotAsync(string Report, object userState)
		{
			if (this.UpdateReportExecutionSnapshotOperationCompleted == null)
			{
				this.UpdateReportExecutionSnapshotOperationCompleted = new SendOrPostCallback(this.OnUpdateReportExecutionSnapshotOperationCompleted);
			}
			base.InvokeAsync("UpdateReportExecutionSnapshot", new object[] { Report }, this.UpdateReportExecutionSnapshotOperationCompleted, userState);
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x000131A4 File Offset: 0x000113A4
		private void OnUpdateReportExecutionSnapshotOperationCompleted(object arg)
		{
			if (this.UpdateReportExecutionSnapshotCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.UpdateReportExecutionSnapshotCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x000131E3 File Offset: 0x000113E3
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/FlushCache", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void FlushCache(string Report)
		{
			base.Invoke("FlushCache", new object[] { Report });
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x000131FB File Offset: 0x000113FB
		public IAsyncResult BeginFlushCache(string Report, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("FlushCache", new object[] { Report }, callback, asyncState);
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x00013214 File Offset: 0x00011414
		public void EndFlushCache(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x0001321E File Offset: 0x0001141E
		public void FlushCacheAsync(string Report)
		{
			this.FlushCacheAsync(Report, null);
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x00013228 File Offset: 0x00011428
		public void FlushCacheAsync(string Report, object userState)
		{
			if (this.FlushCacheOperationCompleted == null)
			{
				this.FlushCacheOperationCompleted = new SendOrPostCallback(this.OnFlushCacheOperationCompleted);
			}
			base.InvokeAsync("FlushCache", new object[] { Report }, this.FlushCacheOperationCompleted, userState);
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x00013260 File Offset: 0x00011460
		private void OnFlushCacheOperationCompleted(object arg)
		{
			if (this.FlushCacheCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.FlushCacheCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x0001329F File Offset: 0x0001149F
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/ListJobs", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Jobs")]
		public Job[] ListJobs()
		{
			return (Job[])base.Invoke("ListJobs", new object[0])[0];
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x000132B9 File Offset: 0x000114B9
		public IAsyncResult BeginListJobs(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListJobs", new object[0], callback, asyncState);
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x000132CE File Offset: 0x000114CE
		public Job[] EndListJobs(IAsyncResult asyncResult)
		{
			return (Job[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x000132DE File Offset: 0x000114DE
		public void ListJobsAsync()
		{
			this.ListJobsAsync(null);
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x000132E7 File Offset: 0x000114E7
		public void ListJobsAsync(object userState)
		{
			if (this.ListJobsOperationCompleted == null)
			{
				this.ListJobsOperationCompleted = new SendOrPostCallback(this.OnListJobsOperationCompleted);
			}
			base.InvokeAsync("ListJobs", new object[0], this.ListJobsOperationCompleted, userState);
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x0001331C File Offset: 0x0001151C
		private void OnListJobsOperationCompleted(object arg)
		{
			if (this.ListJobsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListJobsCompleted(this, new ListJobsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x00013361 File Offset: 0x00011561
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/CancelJob", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public bool CancelJob(string JobID)
		{
			return (bool)base.Invoke("CancelJob", new object[] { JobID })[0];
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x0001337F File Offset: 0x0001157F
		public IAsyncResult BeginCancelJob(string JobID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CancelJob", new object[] { JobID }, callback, asyncState);
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x00013398 File Offset: 0x00011598
		public bool EndCancelJob(IAsyncResult asyncResult)
		{
			return (bool)base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x000133A8 File Offset: 0x000115A8
		public void CancelJobAsync(string JobID)
		{
			this.CancelJobAsync(JobID, null);
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x000133B2 File Offset: 0x000115B2
		public void CancelJobAsync(string JobID, object userState)
		{
			if (this.CancelJobOperationCompleted == null)
			{
				this.CancelJobOperationCompleted = new SendOrPostCallback(this.OnCancelJobOperationCompleted);
			}
			base.InvokeAsync("CancelJob", new object[] { JobID }, this.CancelJobOperationCompleted, userState);
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x000133EC File Offset: 0x000115EC
		private void OnCancelJobOperationCompleted(object arg)
		{
			if (this.CancelJobCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CancelJobCompleted(this, new CancelJobCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x00013431 File Offset: 0x00011631
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/CreateDataSource", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("ItemInfo")]
		public CatalogItem CreateDataSource(string DataSource, string Parent, bool Overwrite, DataSourceDefinition Definition, Property[] Properties)
		{
			return (CatalogItem)base.Invoke("CreateDataSource", new object[] { DataSource, Parent, Overwrite, Definition, Properties })[0];
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x00013466 File Offset: 0x00011666
		public IAsyncResult BeginCreateDataSource(string DataSource, string Parent, bool Overwrite, DataSourceDefinition Definition, Property[] Properties, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateDataSource", new object[] { DataSource, Parent, Overwrite, Definition, Properties }, callback, asyncState);
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x00013498 File Offset: 0x00011698
		public CatalogItem EndCreateDataSource(IAsyncResult asyncResult)
		{
			return (CatalogItem)base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x000134A8 File Offset: 0x000116A8
		public void CreateDataSourceAsync(string DataSource, string Parent, bool Overwrite, DataSourceDefinition Definition, Property[] Properties)
		{
			this.CreateDataSourceAsync(DataSource, Parent, Overwrite, Definition, Properties, null);
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x000134B8 File Offset: 0x000116B8
		public void CreateDataSourceAsync(string DataSource, string Parent, bool Overwrite, DataSourceDefinition Definition, Property[] Properties, object userState)
		{
			if (this.CreateDataSourceOperationCompleted == null)
			{
				this.CreateDataSourceOperationCompleted = new SendOrPostCallback(this.OnCreateDataSourceOperationCompleted);
			}
			base.InvokeAsync("CreateDataSource", new object[] { DataSource, Parent, Overwrite, Definition, Properties }, this.CreateDataSourceOperationCompleted, userState);
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x00013514 File Offset: 0x00011714
		private void OnCreateDataSourceOperationCompleted(object arg)
		{
			if (this.CreateDataSourceCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateDataSourceCompleted(this, new CreateDataSourceCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x00013559 File Offset: 0x00011759
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/GetDataSourceContents", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Definition")]
		public DataSourceDefinition GetDataSourceContents(string DataSource)
		{
			return (DataSourceDefinition)base.Invoke("GetDataSourceContents", new object[] { DataSource })[0];
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x00013577 File Offset: 0x00011777
		public IAsyncResult BeginGetDataSourceContents(string DataSource, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetDataSourceContents", new object[] { DataSource }, callback, asyncState);
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x00013590 File Offset: 0x00011790
		public DataSourceDefinition EndGetDataSourceContents(IAsyncResult asyncResult)
		{
			return (DataSourceDefinition)base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x000135A0 File Offset: 0x000117A0
		public void GetDataSourceContentsAsync(string DataSource)
		{
			this.GetDataSourceContentsAsync(DataSource, null);
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x000135AA File Offset: 0x000117AA
		public void GetDataSourceContentsAsync(string DataSource, object userState)
		{
			if (this.GetDataSourceContentsOperationCompleted == null)
			{
				this.GetDataSourceContentsOperationCompleted = new SendOrPostCallback(this.OnGetDataSourceContentsOperationCompleted);
			}
			base.InvokeAsync("GetDataSourceContents", new object[] { DataSource }, this.GetDataSourceContentsOperationCompleted, userState);
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x000135E4 File Offset: 0x000117E4
		private void OnGetDataSourceContentsOperationCompleted(object arg)
		{
			if (this.GetDataSourceContentsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetDataSourceContentsCompleted(this, new GetDataSourceContentsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x00013629 File Offset: 0x00011829
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/SetDataSourceContents", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void SetDataSourceContents(string DataSource, DataSourceDefinition Definition)
		{
			base.Invoke("SetDataSourceContents", new object[] { DataSource, Definition });
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x00013645 File Offset: 0x00011845
		public IAsyncResult BeginSetDataSourceContents(string DataSource, DataSourceDefinition Definition, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetDataSourceContents", new object[] { DataSource, Definition }, callback, asyncState);
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x00013663 File Offset: 0x00011863
		public void EndSetDataSourceContents(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x0001366D File Offset: 0x0001186D
		public void SetDataSourceContentsAsync(string DataSource, DataSourceDefinition Definition)
		{
			this.SetDataSourceContentsAsync(DataSource, Definition, null);
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x00013678 File Offset: 0x00011878
		public void SetDataSourceContentsAsync(string DataSource, DataSourceDefinition Definition, object userState)
		{
			if (this.SetDataSourceContentsOperationCompleted == null)
			{
				this.SetDataSourceContentsOperationCompleted = new SendOrPostCallback(this.OnSetDataSourceContentsOperationCompleted);
			}
			base.InvokeAsync("SetDataSourceContents", new object[] { DataSource, Definition }, this.SetDataSourceContentsOperationCompleted, userState);
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x000136B4 File Offset: 0x000118B4
		private void OnSetDataSourceContentsOperationCompleted(object arg)
		{
			if (this.SetDataSourceContentsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetDataSourceContentsCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x000136F3 File Offset: 0x000118F3
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/EnableDataSource", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void EnableDataSource(string DataSource)
		{
			base.Invoke("EnableDataSource", new object[] { DataSource });
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x0001370B File Offset: 0x0001190B
		public IAsyncResult BeginEnableDataSource(string DataSource, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("EnableDataSource", new object[] { DataSource }, callback, asyncState);
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x00013724 File Offset: 0x00011924
		public void EndEnableDataSource(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x0001372E File Offset: 0x0001192E
		public void EnableDataSourceAsync(string DataSource)
		{
			this.EnableDataSourceAsync(DataSource, null);
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x00013738 File Offset: 0x00011938
		public void EnableDataSourceAsync(string DataSource, object userState)
		{
			if (this.EnableDataSourceOperationCompleted == null)
			{
				this.EnableDataSourceOperationCompleted = new SendOrPostCallback(this.OnEnableDataSourceOperationCompleted);
			}
			base.InvokeAsync("EnableDataSource", new object[] { DataSource }, this.EnableDataSourceOperationCompleted, userState);
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x00013770 File Offset: 0x00011970
		private void OnEnableDataSourceOperationCompleted(object arg)
		{
			if (this.EnableDataSourceCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.EnableDataSourceCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x000137AF File Offset: 0x000119AF
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/DisableDataSource", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void DisableDataSource(string DataSource)
		{
			base.Invoke("DisableDataSource", new object[] { DataSource });
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x000137C7 File Offset: 0x000119C7
		public IAsyncResult BeginDisableDataSource(string DataSource, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("DisableDataSource", new object[] { DataSource }, callback, asyncState);
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x000137E0 File Offset: 0x000119E0
		public void EndDisableDataSource(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x000137EA File Offset: 0x000119EA
		public void DisableDataSourceAsync(string DataSource)
		{
			this.DisableDataSourceAsync(DataSource, null);
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x000137F4 File Offset: 0x000119F4
		public void DisableDataSourceAsync(string DataSource, object userState)
		{
			if (this.DisableDataSourceOperationCompleted == null)
			{
				this.DisableDataSourceOperationCompleted = new SendOrPostCallback(this.OnDisableDataSourceOperationCompleted);
			}
			base.InvokeAsync("DisableDataSource", new object[] { DataSource }, this.DisableDataSourceOperationCompleted, userState);
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x0001382C File Offset: 0x00011A2C
		private void OnDisableDataSourceOperationCompleted(object arg)
		{
			if (this.DisableDataSourceCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.DisableDataSourceCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x0001386B File Offset: 0x00011A6B
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/SetItemDataSources", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void SetItemDataSources(string Item, DataSource[] DataSources)
		{
			base.Invoke("SetItemDataSources", new object[] { Item, DataSources });
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x00013887 File Offset: 0x00011A87
		public IAsyncResult BeginSetItemDataSources(string Item, DataSource[] DataSources, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetItemDataSources", new object[] { Item, DataSources }, callback, asyncState);
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x000138A5 File Offset: 0x00011AA5
		public void EndSetItemDataSources(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x000138AF File Offset: 0x00011AAF
		public void SetItemDataSourcesAsync(string Item, DataSource[] DataSources)
		{
			this.SetItemDataSourcesAsync(Item, DataSources, null);
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x000138BA File Offset: 0x00011ABA
		public void SetItemDataSourcesAsync(string Item, DataSource[] DataSources, object userState)
		{
			if (this.SetItemDataSourcesOperationCompleted == null)
			{
				this.SetItemDataSourcesOperationCompleted = new SendOrPostCallback(this.OnSetItemDataSourcesOperationCompleted);
			}
			base.InvokeAsync("SetItemDataSources", new object[] { Item, DataSources }, this.SetItemDataSourcesOperationCompleted, userState);
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x000138F8 File Offset: 0x00011AF8
		private void OnSetItemDataSourcesOperationCompleted(object arg)
		{
			if (this.SetItemDataSourcesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetItemDataSourcesCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x00013937 File Offset: 0x00011B37
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/GetItemDataSources", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("DataSources")]
		public DataSource[] GetItemDataSources(string Item)
		{
			return (DataSource[])base.Invoke("GetItemDataSources", new object[] { Item })[0];
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x00013955 File Offset: 0x00011B55
		public IAsyncResult BeginGetItemDataSources(string Item, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetItemDataSources", new object[] { Item }, callback, asyncState);
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x0001396E File Offset: 0x00011B6E
		public DataSource[] EndGetItemDataSources(IAsyncResult asyncResult)
		{
			return (DataSource[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x0001397E File Offset: 0x00011B7E
		public void GetItemDataSourcesAsync(string Item)
		{
			this.GetItemDataSourcesAsync(Item, null);
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x00013988 File Offset: 0x00011B88
		public void GetItemDataSourcesAsync(string Item, object userState)
		{
			if (this.GetItemDataSourcesOperationCompleted == null)
			{
				this.GetItemDataSourcesOperationCompleted = new SendOrPostCallback(this.OnGetItemDataSourcesOperationCompleted);
			}
			base.InvokeAsync("GetItemDataSources", new object[] { Item }, this.GetItemDataSourcesOperationCompleted, userState);
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x000139C0 File Offset: 0x00011BC0
		private void OnGetItemDataSourcesOperationCompleted(object arg)
		{
			if (this.GetItemDataSourcesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetItemDataSourcesCompleted(this, new GetItemDataSourcesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x00013A05 File Offset: 0x00011C05
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/GetItemDataSourcePrompts", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("DataSourcePrompts")]
		public DataSourcePrompt[] GetItemDataSourcePrompts(string Item)
		{
			return (DataSourcePrompt[])base.Invoke("GetItemDataSourcePrompts", new object[] { Item })[0];
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x00013A23 File Offset: 0x00011C23
		public IAsyncResult BeginGetItemDataSourcePrompts(string Item, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetItemDataSourcePrompts", new object[] { Item }, callback, asyncState);
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x00013A3C File Offset: 0x00011C3C
		public DataSourcePrompt[] EndGetItemDataSourcePrompts(IAsyncResult asyncResult)
		{
			return (DataSourcePrompt[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x00013A4C File Offset: 0x00011C4C
		public void GetItemDataSourcePromptsAsync(string Item)
		{
			this.GetItemDataSourcePromptsAsync(Item, null);
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x00013A56 File Offset: 0x00011C56
		public void GetItemDataSourcePromptsAsync(string Item, object userState)
		{
			if (this.GetItemDataSourcePromptsOperationCompleted == null)
			{
				this.GetItemDataSourcePromptsOperationCompleted = new SendOrPostCallback(this.OnGetItemDataSourcePromptsOperationCompleted);
			}
			base.InvokeAsync("GetItemDataSourcePrompts", new object[] { Item }, this.GetItemDataSourcePromptsOperationCompleted, userState);
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x00013A90 File Offset: 0x00011C90
		private void OnGetItemDataSourcePromptsOperationCompleted(object arg)
		{
			if (this.GetItemDataSourcePromptsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetItemDataSourcePromptsCompleted(this, new GetItemDataSourcePromptsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x00013AD8 File Offset: 0x00011CD8
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/TestConnectForDataSourceDefinition", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public bool TestConnectForDataSourceDefinition(DataSourceDefinition DataSourceDefinition, string UserName, string Password, out string ConnectError)
		{
			object[] array = base.Invoke("TestConnectForDataSourceDefinition", new object[] { DataSourceDefinition, UserName, Password });
			ConnectError = (string)array[1];
			return (bool)array[0];
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x00013B16 File Offset: 0x00011D16
		public IAsyncResult BeginTestConnectForDataSourceDefinition(DataSourceDefinition DataSourceDefinition, string UserName, string Password, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("TestConnectForDataSourceDefinition", new object[] { DataSourceDefinition, UserName, Password }, callback, asyncState);
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x00013B3C File Offset: 0x00011D3C
		public bool EndTestConnectForDataSourceDefinition(IAsyncResult asyncResult, out string ConnectError)
		{
			object[] array = base.EndInvoke(asyncResult);
			ConnectError = (string)array[1];
			return (bool)array[0];
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x00013B63 File Offset: 0x00011D63
		public void TestConnectForDataSourceDefinitionAsync(DataSourceDefinition DataSourceDefinition, string UserName, string Password)
		{
			this.TestConnectForDataSourceDefinitionAsync(DataSourceDefinition, UserName, Password, null);
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x00013B70 File Offset: 0x00011D70
		public void TestConnectForDataSourceDefinitionAsync(DataSourceDefinition DataSourceDefinition, string UserName, string Password, object userState)
		{
			if (this.TestConnectForDataSourceDefinitionOperationCompleted == null)
			{
				this.TestConnectForDataSourceDefinitionOperationCompleted = new SendOrPostCallback(this.OnTestConnectForDataSourceDefinitionOperationCompleted);
			}
			base.InvokeAsync("TestConnectForDataSourceDefinition", new object[] { DataSourceDefinition, UserName, Password }, this.TestConnectForDataSourceDefinitionOperationCompleted, userState);
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x00013BBC File Offset: 0x00011DBC
		private void OnTestConnectForDataSourceDefinitionOperationCompleted(object arg)
		{
			if (this.TestConnectForDataSourceDefinitionCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.TestConnectForDataSourceDefinitionCompleted(this, new TestConnectForDataSourceDefinitionCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x00013C04 File Offset: 0x00011E04
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/TestConnectForItemDataSource", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public bool TestConnectForItemDataSource(string Item, string DataSourceName, string UserName, string Password, out string ConnectError)
		{
			object[] array = base.Invoke("TestConnectForItemDataSource", new object[] { Item, DataSourceName, UserName, Password });
			ConnectError = (string)array[1];
			return (bool)array[0];
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x00013C47 File Offset: 0x00011E47
		public IAsyncResult BeginTestConnectForItemDataSource(string Item, string DataSourceName, string UserName, string Password, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("TestConnectForItemDataSource", new object[] { Item, DataSourceName, UserName, Password }, callback, asyncState);
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x00013C70 File Offset: 0x00011E70
		public bool EndTestConnectForItemDataSource(IAsyncResult asyncResult, out string ConnectError)
		{
			object[] array = base.EndInvoke(asyncResult);
			ConnectError = (string)array[1];
			return (bool)array[0];
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x00013C97 File Offset: 0x00011E97
		public void TestConnectForItemDataSourceAsync(string Item, string DataSourceName, string UserName, string Password)
		{
			this.TestConnectForItemDataSourceAsync(Item, DataSourceName, UserName, Password, null);
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x00013CA8 File Offset: 0x00011EA8
		public void TestConnectForItemDataSourceAsync(string Item, string DataSourceName, string UserName, string Password, object userState)
		{
			if (this.TestConnectForItemDataSourceOperationCompleted == null)
			{
				this.TestConnectForItemDataSourceOperationCompleted = new SendOrPostCallback(this.OnTestConnectForItemDataSourceOperationCompleted);
			}
			base.InvokeAsync("TestConnectForItemDataSource", new object[] { Item, DataSourceName, UserName, Password }, this.TestConnectForItemDataSourceOperationCompleted, userState);
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x00013CFC File Offset: 0x00011EFC
		private void OnTestConnectForItemDataSourceOperationCompleted(object arg)
		{
			if (this.TestConnectForItemDataSourceCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.TestConnectForItemDataSourceCompleted(this, new TestConnectForItemDataSourceCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x00013D44 File Offset: 0x00011F44
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/CreateReportHistorySnapshot", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("HistoryID")]
		public string CreateReportHistorySnapshot(string Report, out Warning[] Warnings)
		{
			object[] array = base.Invoke("CreateReportHistorySnapshot", new object[] { Report });
			Warnings = (Warning[])array[1];
			return (string)array[0];
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x00013D79 File Offset: 0x00011F79
		public IAsyncResult BeginCreateReportHistorySnapshot(string Report, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateReportHistorySnapshot", new object[] { Report }, callback, asyncState);
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x00013D94 File Offset: 0x00011F94
		public string EndCreateReportHistorySnapshot(IAsyncResult asyncResult, out Warning[] Warnings)
		{
			object[] array = base.EndInvoke(asyncResult);
			Warnings = (Warning[])array[1];
			return (string)array[0];
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x00013DBB File Offset: 0x00011FBB
		public void CreateReportHistorySnapshotAsync(string Report)
		{
			this.CreateReportHistorySnapshotAsync(Report, null);
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x00013DC5 File Offset: 0x00011FC5
		public void CreateReportHistorySnapshotAsync(string Report, object userState)
		{
			if (this.CreateReportHistorySnapshotOperationCompleted == null)
			{
				this.CreateReportHistorySnapshotOperationCompleted = new SendOrPostCallback(this.OnCreateReportHistorySnapshotOperationCompleted);
			}
			base.InvokeAsync("CreateReportHistorySnapshot", new object[] { Report }, this.CreateReportHistorySnapshotOperationCompleted, userState);
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x00013E00 File Offset: 0x00012000
		private void OnCreateReportHistorySnapshotOperationCompleted(object arg)
		{
			if (this.CreateReportHistorySnapshotCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateReportHistorySnapshotCompleted(this, new CreateReportHistorySnapshotCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x00013E45 File Offset: 0x00012045
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/SetReportHistoryOptions", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void SetReportHistoryOptions(string Report, bool EnableManualSnapshotCreation, bool KeepExecutionSnapshots, [XmlElement("NoSchedule", typeof(NoSchedule))] [XmlElement("ScheduleDefinition", typeof(ScheduleDefinition))] [XmlElement("ScheduleReference", typeof(ScheduleReference))] ScheduleDefinitionOrReference Item)
		{
			base.Invoke("SetReportHistoryOptions", new object[] { Report, EnableManualSnapshotCreation, KeepExecutionSnapshots, Item });
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x00013E74 File Offset: 0x00012074
		public IAsyncResult BeginSetReportHistoryOptions(string Report, bool EnableManualSnapshotCreation, bool KeepExecutionSnapshots, ScheduleDefinitionOrReference Item, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetReportHistoryOptions", new object[] { Report, EnableManualSnapshotCreation, KeepExecutionSnapshots, Item }, callback, asyncState);
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x00013EA6 File Offset: 0x000120A6
		public void EndSetReportHistoryOptions(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x00013EB0 File Offset: 0x000120B0
		public void SetReportHistoryOptionsAsync(string Report, bool EnableManualSnapshotCreation, bool KeepExecutionSnapshots, ScheduleDefinitionOrReference Item)
		{
			this.SetReportHistoryOptionsAsync(Report, EnableManualSnapshotCreation, KeepExecutionSnapshots, Item, null);
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x00013EC0 File Offset: 0x000120C0
		public void SetReportHistoryOptionsAsync(string Report, bool EnableManualSnapshotCreation, bool KeepExecutionSnapshots, ScheduleDefinitionOrReference Item, object userState)
		{
			if (this.SetReportHistoryOptionsOperationCompleted == null)
			{
				this.SetReportHistoryOptionsOperationCompleted = new SendOrPostCallback(this.OnSetReportHistoryOptionsOperationCompleted);
			}
			base.InvokeAsync("SetReportHistoryOptions", new object[] { Report, EnableManualSnapshotCreation, KeepExecutionSnapshots, Item }, this.SetReportHistoryOptionsOperationCompleted, userState);
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x00013F1C File Offset: 0x0001211C
		private void OnSetReportHistoryOptionsOperationCompleted(object arg)
		{
			if (this.SetReportHistoryOptionsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetReportHistoryOptionsCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x00013F5C File Offset: 0x0001215C
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/GetReportHistoryOptions", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("EnableManualSnapshotCreation")]
		public bool GetReportHistoryOptions(string Report, out bool KeepExecutionSnapshots, [XmlElement("NoSchedule", typeof(NoSchedule))] [XmlElement("ScheduleDefinition", typeof(ScheduleDefinition))] [XmlElement("ScheduleReference", typeof(ScheduleReference))] out ScheduleDefinitionOrReference Item)
		{
			object[] array = base.Invoke("GetReportHistoryOptions", new object[] { Report });
			KeepExecutionSnapshots = (bool)array[1];
			Item = (ScheduleDefinitionOrReference)array[2];
			return (bool)array[0];
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x00013F9B File Offset: 0x0001219B
		public IAsyncResult BeginGetReportHistoryOptions(string Report, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetReportHistoryOptions", new object[] { Report }, callback, asyncState);
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x00013FB4 File Offset: 0x000121B4
		public bool EndGetReportHistoryOptions(IAsyncResult asyncResult, out bool KeepExecutionSnapshots, out ScheduleDefinitionOrReference Item)
		{
			object[] array = base.EndInvoke(asyncResult);
			KeepExecutionSnapshots = (bool)array[1];
			Item = (ScheduleDefinitionOrReference)array[2];
			return (bool)array[0];
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x00013FE5 File Offset: 0x000121E5
		public void GetReportHistoryOptionsAsync(string Report)
		{
			this.GetReportHistoryOptionsAsync(Report, null);
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x00013FEF File Offset: 0x000121EF
		public void GetReportHistoryOptionsAsync(string Report, object userState)
		{
			if (this.GetReportHistoryOptionsOperationCompleted == null)
			{
				this.GetReportHistoryOptionsOperationCompleted = new SendOrPostCallback(this.OnGetReportHistoryOptionsOperationCompleted);
			}
			base.InvokeAsync("GetReportHistoryOptions", new object[] { Report }, this.GetReportHistoryOptionsOperationCompleted, userState);
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x00014028 File Offset: 0x00012228
		private void OnGetReportHistoryOptionsOperationCompleted(object arg)
		{
			if (this.GetReportHistoryOptionsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetReportHistoryOptionsCompleted(this, new GetReportHistoryOptionsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x0001406D File Offset: 0x0001226D
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/SetReportHistoryLimit", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void SetReportHistoryLimit(string Report, bool UseSystem, int HistoryLimit)
		{
			base.Invoke("SetReportHistoryLimit", new object[] { Report, UseSystem, HistoryLimit });
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x00014097 File Offset: 0x00012297
		public IAsyncResult BeginSetReportHistoryLimit(string Report, bool UseSystem, int HistoryLimit, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetReportHistoryLimit", new object[] { Report, UseSystem, HistoryLimit }, callback, asyncState);
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x000140C4 File Offset: 0x000122C4
		public void EndSetReportHistoryLimit(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x000140CE File Offset: 0x000122CE
		public void SetReportHistoryLimitAsync(string Report, bool UseSystem, int HistoryLimit)
		{
			this.SetReportHistoryLimitAsync(Report, UseSystem, HistoryLimit, null);
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x000140DC File Offset: 0x000122DC
		public void SetReportHistoryLimitAsync(string Report, bool UseSystem, int HistoryLimit, object userState)
		{
			if (this.SetReportHistoryLimitOperationCompleted == null)
			{
				this.SetReportHistoryLimitOperationCompleted = new SendOrPostCallback(this.OnSetReportHistoryLimitOperationCompleted);
			}
			base.InvokeAsync("SetReportHistoryLimit", new object[] { Report, UseSystem, HistoryLimit }, this.SetReportHistoryLimitOperationCompleted, userState);
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x00014134 File Offset: 0x00012334
		private void OnSetReportHistoryLimitOperationCompleted(object arg)
		{
			if (this.SetReportHistoryLimitCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetReportHistoryLimitCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x00014174 File Offset: 0x00012374
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/GetReportHistoryLimit", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("HistoryLimit")]
		public int GetReportHistoryLimit(string Report, out bool IsSystem, out int SystemLimit)
		{
			object[] array = base.Invoke("GetReportHistoryLimit", new object[] { Report });
			IsSystem = (bool)array[1];
			SystemLimit = (int)array[2];
			return (int)array[0];
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x000141B3 File Offset: 0x000123B3
		public IAsyncResult BeginGetReportHistoryLimit(string Report, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetReportHistoryLimit", new object[] { Report }, callback, asyncState);
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x000141CC File Offset: 0x000123CC
		public int EndGetReportHistoryLimit(IAsyncResult asyncResult, out bool IsSystem, out int SystemLimit)
		{
			object[] array = base.EndInvoke(asyncResult);
			IsSystem = (bool)array[1];
			SystemLimit = (int)array[2];
			return (int)array[0];
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x000141FD File Offset: 0x000123FD
		public void GetReportHistoryLimitAsync(string Report)
		{
			this.GetReportHistoryLimitAsync(Report, null);
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x00014207 File Offset: 0x00012407
		public void GetReportHistoryLimitAsync(string Report, object userState)
		{
			if (this.GetReportHistoryLimitOperationCompleted == null)
			{
				this.GetReportHistoryLimitOperationCompleted = new SendOrPostCallback(this.OnGetReportHistoryLimitOperationCompleted);
			}
			base.InvokeAsync("GetReportHistoryLimit", new object[] { Report }, this.GetReportHistoryLimitOperationCompleted, userState);
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x00014240 File Offset: 0x00012440
		private void OnGetReportHistoryLimitOperationCompleted(object arg)
		{
			if (this.GetReportHistoryLimitCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetReportHistoryLimitCompleted(this, new GetReportHistoryLimitCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x00014285 File Offset: 0x00012485
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/ListReportHistory", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("ReportHistory")]
		public ReportHistorySnapshot[] ListReportHistory(string Report)
		{
			return (ReportHistorySnapshot[])base.Invoke("ListReportHistory", new object[] { Report })[0];
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x000142A3 File Offset: 0x000124A3
		public IAsyncResult BeginListReportHistory(string Report, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListReportHistory", new object[] { Report }, callback, asyncState);
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x000142BC File Offset: 0x000124BC
		public ReportHistorySnapshot[] EndListReportHistory(IAsyncResult asyncResult)
		{
			return (ReportHistorySnapshot[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x000142CC File Offset: 0x000124CC
		public void ListReportHistoryAsync(string Report)
		{
			this.ListReportHistoryAsync(Report, null);
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x000142D6 File Offset: 0x000124D6
		public void ListReportHistoryAsync(string Report, object userState)
		{
			if (this.ListReportHistoryOperationCompleted == null)
			{
				this.ListReportHistoryOperationCompleted = new SendOrPostCallback(this.OnListReportHistoryOperationCompleted);
			}
			base.InvokeAsync("ListReportHistory", new object[] { Report }, this.ListReportHistoryOperationCompleted, userState);
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x00014310 File Offset: 0x00012510
		private void OnListReportHistoryOperationCompleted(object arg)
		{
			if (this.ListReportHistoryCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListReportHistoryCompleted(this, new ListReportHistoryCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x00014355 File Offset: 0x00012555
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/DeleteReportHistorySnapshot", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void DeleteReportHistorySnapshot(string Report, string HistoryID)
		{
			base.Invoke("DeleteReportHistorySnapshot", new object[] { Report, HistoryID });
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x00014371 File Offset: 0x00012571
		public IAsyncResult BeginDeleteReportHistorySnapshot(string Report, string HistoryID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("DeleteReportHistorySnapshot", new object[] { Report, HistoryID }, callback, asyncState);
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x0001438F File Offset: 0x0001258F
		public void EndDeleteReportHistorySnapshot(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x00014399 File Offset: 0x00012599
		public void DeleteReportHistorySnapshotAsync(string Report, string HistoryID)
		{
			this.DeleteReportHistorySnapshotAsync(Report, HistoryID, null);
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x000143A4 File Offset: 0x000125A4
		public void DeleteReportHistorySnapshotAsync(string Report, string HistoryID, object userState)
		{
			if (this.DeleteReportHistorySnapshotOperationCompleted == null)
			{
				this.DeleteReportHistorySnapshotOperationCompleted = new SendOrPostCallback(this.OnDeleteReportHistorySnapshotOperationCompleted);
			}
			base.InvokeAsync("DeleteReportHistorySnapshot", new object[] { Report, HistoryID }, this.DeleteReportHistorySnapshotOperationCompleted, userState);
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x000143E0 File Offset: 0x000125E0
		private void OnDeleteReportHistorySnapshotOperationCompleted(object arg)
		{
			if (this.DeleteReportHistorySnapshotCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.DeleteReportHistorySnapshotCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x0001441F File Offset: 0x0001261F
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/CreateSchedule", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("ScheduleID")]
		public string CreateSchedule(string Name, ScheduleDefinition ScheduleDefinition, string Site)
		{
			return (string)base.Invoke("CreateSchedule", new object[] { Name, ScheduleDefinition, Site })[0];
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x00014445 File Offset: 0x00012645
		public IAsyncResult BeginCreateSchedule(string Name, ScheduleDefinition ScheduleDefinition, string Site, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateSchedule", new object[] { Name, ScheduleDefinition, Site }, callback, asyncState);
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x00014468 File Offset: 0x00012668
		public string EndCreateSchedule(IAsyncResult asyncResult)
		{
			return (string)base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x00014478 File Offset: 0x00012678
		public void CreateScheduleAsync(string Name, ScheduleDefinition ScheduleDefinition, string Site)
		{
			this.CreateScheduleAsync(Name, ScheduleDefinition, Site, null);
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x00014484 File Offset: 0x00012684
		public void CreateScheduleAsync(string Name, ScheduleDefinition ScheduleDefinition, string Site, object userState)
		{
			if (this.CreateScheduleOperationCompleted == null)
			{
				this.CreateScheduleOperationCompleted = new SendOrPostCallback(this.OnCreateScheduleOperationCompleted);
			}
			base.InvokeAsync("CreateSchedule", new object[] { Name, ScheduleDefinition, Site }, this.CreateScheduleOperationCompleted, userState);
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x000144D0 File Offset: 0x000126D0
		private void OnCreateScheduleOperationCompleted(object arg)
		{
			if (this.CreateScheduleCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateScheduleCompleted(this, new CreateScheduleCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x00014515 File Offset: 0x00012715
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/DeleteSchedule", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void DeleteSchedule(string ScheduleID)
		{
			base.Invoke("DeleteSchedule", new object[] { ScheduleID });
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x0001452D File Offset: 0x0001272D
		public IAsyncResult BeginDeleteSchedule(string ScheduleID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("DeleteSchedule", new object[] { ScheduleID }, callback, asyncState);
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x00014546 File Offset: 0x00012746
		public void EndDeleteSchedule(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x00014550 File Offset: 0x00012750
		public void DeleteScheduleAsync(string ScheduleID)
		{
			this.DeleteScheduleAsync(ScheduleID, null);
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x0001455A File Offset: 0x0001275A
		public void DeleteScheduleAsync(string ScheduleID, object userState)
		{
			if (this.DeleteScheduleOperationCompleted == null)
			{
				this.DeleteScheduleOperationCompleted = new SendOrPostCallback(this.OnDeleteScheduleOperationCompleted);
			}
			base.InvokeAsync("DeleteSchedule", new object[] { ScheduleID }, this.DeleteScheduleOperationCompleted, userState);
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x00014594 File Offset: 0x00012794
		private void OnDeleteScheduleOperationCompleted(object arg)
		{
			if (this.DeleteScheduleCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.DeleteScheduleCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x000145D3 File Offset: 0x000127D3
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/SetScheduleProperties", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void SetScheduleProperties(string Name, string ScheduleID, ScheduleDefinition ScheduleDefinition)
		{
			base.Invoke("SetScheduleProperties", new object[] { Name, ScheduleID, ScheduleDefinition });
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x000145F3 File Offset: 0x000127F3
		public IAsyncResult BeginSetScheduleProperties(string Name, string ScheduleID, ScheduleDefinition ScheduleDefinition, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetScheduleProperties", new object[] { Name, ScheduleID, ScheduleDefinition }, callback, asyncState);
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x00014616 File Offset: 0x00012816
		public void EndSetScheduleProperties(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x00014620 File Offset: 0x00012820
		public void SetSchedulePropertiesAsync(string Name, string ScheduleID, ScheduleDefinition ScheduleDefinition)
		{
			this.SetSchedulePropertiesAsync(Name, ScheduleID, ScheduleDefinition, null);
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x0001462C File Offset: 0x0001282C
		public void SetSchedulePropertiesAsync(string Name, string ScheduleID, ScheduleDefinition ScheduleDefinition, object userState)
		{
			if (this.SetSchedulePropertiesOperationCompleted == null)
			{
				this.SetSchedulePropertiesOperationCompleted = new SendOrPostCallback(this.OnSetSchedulePropertiesOperationCompleted);
			}
			base.InvokeAsync("SetScheduleProperties", new object[] { Name, ScheduleID, ScheduleDefinition }, this.SetSchedulePropertiesOperationCompleted, userState);
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x00014678 File Offset: 0x00012878
		private void OnSetSchedulePropertiesOperationCompleted(object arg)
		{
			if (this.SetSchedulePropertiesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetSchedulePropertiesCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x000146B7 File Offset: 0x000128B7
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/GetScheduleProperties", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Schedule")]
		public Schedule GetScheduleProperties(string ScheduleID)
		{
			return (Schedule)base.Invoke("GetScheduleProperties", new object[] { ScheduleID })[0];
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x000146D5 File Offset: 0x000128D5
		public IAsyncResult BeginGetScheduleProperties(string ScheduleID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetScheduleProperties", new object[] { ScheduleID }, callback, asyncState);
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x000146EE File Offset: 0x000128EE
		public Schedule EndGetScheduleProperties(IAsyncResult asyncResult)
		{
			return (Schedule)base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x000146FE File Offset: 0x000128FE
		public void GetSchedulePropertiesAsync(string ScheduleID)
		{
			this.GetSchedulePropertiesAsync(ScheduleID, null);
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x00014708 File Offset: 0x00012908
		public void GetSchedulePropertiesAsync(string ScheduleID, object userState)
		{
			if (this.GetSchedulePropertiesOperationCompleted == null)
			{
				this.GetSchedulePropertiesOperationCompleted = new SendOrPostCallback(this.OnGetSchedulePropertiesOperationCompleted);
			}
			base.InvokeAsync("GetScheduleProperties", new object[] { ScheduleID }, this.GetSchedulePropertiesOperationCompleted, userState);
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x00014740 File Offset: 0x00012940
		private void OnGetSchedulePropertiesOperationCompleted(object arg)
		{
			if (this.GetSchedulePropertiesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetSchedulePropertiesCompleted(this, new GetSchedulePropertiesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x00014785 File Offset: 0x00012985
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/ListScheduledReports", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Reports")]
		public CatalogItem[] ListScheduledReports(string ScheduleID)
		{
			return (CatalogItem[])base.Invoke("ListScheduledReports", new object[] { ScheduleID })[0];
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x000147A3 File Offset: 0x000129A3
		public IAsyncResult BeginListScheduledReports(string ScheduleID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListScheduledReports", new object[] { ScheduleID }, callback, asyncState);
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x000147BC File Offset: 0x000129BC
		public CatalogItem[] EndListScheduledReports(IAsyncResult asyncResult)
		{
			return (CatalogItem[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x000147CC File Offset: 0x000129CC
		public void ListScheduledReportsAsync(string ScheduleID)
		{
			this.ListScheduledReportsAsync(ScheduleID, null);
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x000147D6 File Offset: 0x000129D6
		public void ListScheduledReportsAsync(string ScheduleID, object userState)
		{
			if (this.ListScheduledReportsOperationCompleted == null)
			{
				this.ListScheduledReportsOperationCompleted = new SendOrPostCallback(this.OnListScheduledReportsOperationCompleted);
			}
			base.InvokeAsync("ListScheduledReports", new object[] { ScheduleID }, this.ListScheduledReportsOperationCompleted, userState);
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x00014810 File Offset: 0x00012A10
		private void OnListScheduledReportsOperationCompleted(object arg)
		{
			if (this.ListScheduledReportsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListScheduledReportsCompleted(this, new ListScheduledReportsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x00014855 File Offset: 0x00012A55
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/ListSchedules", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Schedules")]
		public Schedule[] ListSchedules(string Site)
		{
			return (Schedule[])base.Invoke("ListSchedules", new object[] { Site })[0];
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x00014873 File Offset: 0x00012A73
		public IAsyncResult BeginListSchedules(string Site, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListSchedules", new object[] { Site }, callback, asyncState);
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x0001488C File Offset: 0x00012A8C
		public Schedule[] EndListSchedules(IAsyncResult asyncResult)
		{
			return (Schedule[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x0001489C File Offset: 0x00012A9C
		public void ListSchedulesAsync(string Site)
		{
			this.ListSchedulesAsync(Site, null);
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x000148A6 File Offset: 0x00012AA6
		public void ListSchedulesAsync(string Site, object userState)
		{
			if (this.ListSchedulesOperationCompleted == null)
			{
				this.ListSchedulesOperationCompleted = new SendOrPostCallback(this.OnListSchedulesOperationCompleted);
			}
			base.InvokeAsync("ListSchedules", new object[] { Site }, this.ListSchedulesOperationCompleted, userState);
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x000148E0 File Offset: 0x00012AE0
		private void OnListSchedulesOperationCompleted(object arg)
		{
			if (this.ListSchedulesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListSchedulesCompleted(this, new ListSchedulesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x00014925 File Offset: 0x00012B25
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/PauseSchedule", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void PauseSchedule(string ScheduleID)
		{
			base.Invoke("PauseSchedule", new object[] { ScheduleID });
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x0001493D File Offset: 0x00012B3D
		public IAsyncResult BeginPauseSchedule(string ScheduleID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("PauseSchedule", new object[] { ScheduleID }, callback, asyncState);
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x00014956 File Offset: 0x00012B56
		public void EndPauseSchedule(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x00014960 File Offset: 0x00012B60
		public void PauseScheduleAsync(string ScheduleID)
		{
			this.PauseScheduleAsync(ScheduleID, null);
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x0001496A File Offset: 0x00012B6A
		public void PauseScheduleAsync(string ScheduleID, object userState)
		{
			if (this.PauseScheduleOperationCompleted == null)
			{
				this.PauseScheduleOperationCompleted = new SendOrPostCallback(this.OnPauseScheduleOperationCompleted);
			}
			base.InvokeAsync("PauseSchedule", new object[] { ScheduleID }, this.PauseScheduleOperationCompleted, userState);
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x000149A4 File Offset: 0x00012BA4
		private void OnPauseScheduleOperationCompleted(object arg)
		{
			if (this.PauseScheduleCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.PauseScheduleCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x000149E3 File Offset: 0x00012BE3
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/ResumeSchedule", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void ResumeSchedule(string ScheduleID)
		{
			base.Invoke("ResumeSchedule", new object[] { ScheduleID });
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x000149FB File Offset: 0x00012BFB
		public IAsyncResult BeginResumeSchedule(string ScheduleID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ResumeSchedule", new object[] { ScheduleID }, callback, asyncState);
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x00014A14 File Offset: 0x00012C14
		public void EndResumeSchedule(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x00014A1E File Offset: 0x00012C1E
		public void ResumeScheduleAsync(string ScheduleID)
		{
			this.ResumeScheduleAsync(ScheduleID, null);
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x00014A28 File Offset: 0x00012C28
		public void ResumeScheduleAsync(string ScheduleID, object userState)
		{
			if (this.ResumeScheduleOperationCompleted == null)
			{
				this.ResumeScheduleOperationCompleted = new SendOrPostCallback(this.OnResumeScheduleOperationCompleted);
			}
			base.InvokeAsync("ResumeSchedule", new object[] { ScheduleID }, this.ResumeScheduleOperationCompleted, userState);
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x00014A60 File Offset: 0x00012C60
		private void OnResumeScheduleOperationCompleted(object arg)
		{
			if (this.ResumeScheduleCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ResumeScheduleCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x00014A9F File Offset: 0x00012C9F
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/CreateSubscription", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("SubscriptionID")]
		public string CreateSubscription(string Report, ExtensionSettings ExtensionSettings, string Description, string EventType, string MatchData, ParameterValue[] Parameters)
		{
			return (string)base.Invoke("CreateSubscription", new object[] { Report, ExtensionSettings, Description, EventType, MatchData, Parameters })[0];
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x00014AD4 File Offset: 0x00012CD4
		public IAsyncResult BeginCreateSubscription(string Report, ExtensionSettings ExtensionSettings, string Description, string EventType, string MatchData, ParameterValue[] Parameters, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateSubscription", new object[] { Report, ExtensionSettings, Description, EventType, MatchData, Parameters }, callback, asyncState);
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x00014B06 File Offset: 0x00012D06
		public string EndCreateSubscription(IAsyncResult asyncResult)
		{
			return (string)base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x00014B16 File Offset: 0x00012D16
		public void CreateSubscriptionAsync(string Report, ExtensionSettings ExtensionSettings, string Description, string EventType, string MatchData, ParameterValue[] Parameters)
		{
			this.CreateSubscriptionAsync(Report, ExtensionSettings, Description, EventType, MatchData, Parameters, null);
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x00014B28 File Offset: 0x00012D28
		public void CreateSubscriptionAsync(string Report, ExtensionSettings ExtensionSettings, string Description, string EventType, string MatchData, ParameterValue[] Parameters, object userState)
		{
			if (this.CreateSubscriptionOperationCompleted == null)
			{
				this.CreateSubscriptionOperationCompleted = new SendOrPostCallback(this.OnCreateSubscriptionOperationCompleted);
			}
			base.InvokeAsync("CreateSubscription", new object[] { Report, ExtensionSettings, Description, EventType, MatchData, Parameters }, this.CreateSubscriptionOperationCompleted, userState);
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x00014B84 File Offset: 0x00012D84
		private void OnCreateSubscriptionOperationCompleted(object arg)
		{
			if (this.CreateSubscriptionCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateSubscriptionCompleted(this, new CreateSubscriptionCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x00014BC9 File Offset: 0x00012DC9
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/CreateDataDrivenSubscription", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("DataDrivenSubscriptionID")]
		public string CreateDataDrivenSubscription(string Report, ExtensionSettings ExtensionSettings, DataRetrievalPlan DataRetrievalPlan, string Description, string EventType, string MatchData, ParameterValueOrFieldReference[] Parameters)
		{
			return (string)base.Invoke("CreateDataDrivenSubscription", new object[] { Report, ExtensionSettings, DataRetrievalPlan, Description, EventType, MatchData, Parameters })[0];
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x00014C03 File Offset: 0x00012E03
		public IAsyncResult BeginCreateDataDrivenSubscription(string Report, ExtensionSettings ExtensionSettings, DataRetrievalPlan DataRetrievalPlan, string Description, string EventType, string MatchData, ParameterValueOrFieldReference[] Parameters, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateDataDrivenSubscription", new object[] { Report, ExtensionSettings, DataRetrievalPlan, Description, EventType, MatchData, Parameters }, callback, asyncState);
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x00014C3A File Offset: 0x00012E3A
		public string EndCreateDataDrivenSubscription(IAsyncResult asyncResult)
		{
			return (string)base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x00014C4C File Offset: 0x00012E4C
		public void CreateDataDrivenSubscriptionAsync(string Report, ExtensionSettings ExtensionSettings, DataRetrievalPlan DataRetrievalPlan, string Description, string EventType, string MatchData, ParameterValueOrFieldReference[] Parameters)
		{
			this.CreateDataDrivenSubscriptionAsync(Report, ExtensionSettings, DataRetrievalPlan, Description, EventType, MatchData, Parameters, null);
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x00014C6C File Offset: 0x00012E6C
		public void CreateDataDrivenSubscriptionAsync(string Report, ExtensionSettings ExtensionSettings, DataRetrievalPlan DataRetrievalPlan, string Description, string EventType, string MatchData, ParameterValueOrFieldReference[] Parameters, object userState)
		{
			if (this.CreateDataDrivenSubscriptionOperationCompleted == null)
			{
				this.CreateDataDrivenSubscriptionOperationCompleted = new SendOrPostCallback(this.OnCreateDataDrivenSubscriptionOperationCompleted);
			}
			base.InvokeAsync("CreateDataDrivenSubscription", new object[] { Report, ExtensionSettings, DataRetrievalPlan, Description, EventType, MatchData, Parameters }, this.CreateDataDrivenSubscriptionOperationCompleted, userState);
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x00014CCC File Offset: 0x00012ECC
		private void OnCreateDataDrivenSubscriptionOperationCompleted(object arg)
		{
			if (this.CreateDataDrivenSubscriptionCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateDataDrivenSubscriptionCompleted(this, new CreateDataDrivenSubscriptionCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x00014D11 File Offset: 0x00012F11
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/SetSubscriptionProperties", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void SetSubscriptionProperties(string SubscriptionID, ExtensionSettings ExtensionSettings, string Description, string EventType, string MatchData, ParameterValue[] Parameters)
		{
			base.Invoke("SetSubscriptionProperties", new object[] { SubscriptionID, ExtensionSettings, Description, EventType, MatchData, Parameters });
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x00014D40 File Offset: 0x00012F40
		public IAsyncResult BeginSetSubscriptionProperties(string SubscriptionID, ExtensionSettings ExtensionSettings, string Description, string EventType, string MatchData, ParameterValue[] Parameters, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetSubscriptionProperties", new object[] { SubscriptionID, ExtensionSettings, Description, EventType, MatchData, Parameters }, callback, asyncState);
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x00014D72 File Offset: 0x00012F72
		public void EndSetSubscriptionProperties(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x00014D7C File Offset: 0x00012F7C
		public void SetSubscriptionPropertiesAsync(string SubscriptionID, ExtensionSettings ExtensionSettings, string Description, string EventType, string MatchData, ParameterValue[] Parameters)
		{
			this.SetSubscriptionPropertiesAsync(SubscriptionID, ExtensionSettings, Description, EventType, MatchData, Parameters, null);
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x00014D90 File Offset: 0x00012F90
		public void SetSubscriptionPropertiesAsync(string SubscriptionID, ExtensionSettings ExtensionSettings, string Description, string EventType, string MatchData, ParameterValue[] Parameters, object userState)
		{
			if (this.SetSubscriptionPropertiesOperationCompleted == null)
			{
				this.SetSubscriptionPropertiesOperationCompleted = new SendOrPostCallback(this.OnSetSubscriptionPropertiesOperationCompleted);
			}
			base.InvokeAsync("SetSubscriptionProperties", new object[] { SubscriptionID, ExtensionSettings, Description, EventType, MatchData, Parameters }, this.SetSubscriptionPropertiesOperationCompleted, userState);
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x00014DEC File Offset: 0x00012FEC
		private void OnSetSubscriptionPropertiesOperationCompleted(object arg)
		{
			if (this.SetSubscriptionPropertiesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetSubscriptionPropertiesCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x00014E2B File Offset: 0x0001302B
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/SetDataDrivenSubscriptionProperties", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void SetDataDrivenSubscriptionProperties(string DataDrivenSubscriptionID, ExtensionSettings ExtensionSettings, DataRetrievalPlan DataRetrievalPlan, string Description, string EventType, string MatchData, ParameterValueOrFieldReference[] Parameters)
		{
			base.Invoke("SetDataDrivenSubscriptionProperties", new object[] { DataDrivenSubscriptionID, ExtensionSettings, DataRetrievalPlan, Description, EventType, MatchData, Parameters });
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x00014E5F File Offset: 0x0001305F
		public IAsyncResult BeginSetDataDrivenSubscriptionProperties(string DataDrivenSubscriptionID, ExtensionSettings ExtensionSettings, DataRetrievalPlan DataRetrievalPlan, string Description, string EventType, string MatchData, ParameterValueOrFieldReference[] Parameters, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetDataDrivenSubscriptionProperties", new object[] { DataDrivenSubscriptionID, ExtensionSettings, DataRetrievalPlan, Description, EventType, MatchData, Parameters }, callback, asyncState);
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x00014E96 File Offset: 0x00013096
		public void EndSetDataDrivenSubscriptionProperties(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x00014EA0 File Offset: 0x000130A0
		public void SetDataDrivenSubscriptionPropertiesAsync(string DataDrivenSubscriptionID, ExtensionSettings ExtensionSettings, DataRetrievalPlan DataRetrievalPlan, string Description, string EventType, string MatchData, ParameterValueOrFieldReference[] Parameters)
		{
			this.SetDataDrivenSubscriptionPropertiesAsync(DataDrivenSubscriptionID, ExtensionSettings, DataRetrievalPlan, Description, EventType, MatchData, Parameters, null);
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x00014EC0 File Offset: 0x000130C0
		public void SetDataDrivenSubscriptionPropertiesAsync(string DataDrivenSubscriptionID, ExtensionSettings ExtensionSettings, DataRetrievalPlan DataRetrievalPlan, string Description, string EventType, string MatchData, ParameterValueOrFieldReference[] Parameters, object userState)
		{
			if (this.SetDataDrivenSubscriptionPropertiesOperationCompleted == null)
			{
				this.SetDataDrivenSubscriptionPropertiesOperationCompleted = new SendOrPostCallback(this.OnSetDataDrivenSubscriptionPropertiesOperationCompleted);
			}
			base.InvokeAsync("SetDataDrivenSubscriptionProperties", new object[] { DataDrivenSubscriptionID, ExtensionSettings, DataRetrievalPlan, Description, EventType, MatchData, Parameters }, this.SetDataDrivenSubscriptionPropertiesOperationCompleted, userState);
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x00014F20 File Offset: 0x00013120
		private void OnSetDataDrivenSubscriptionPropertiesOperationCompleted(object arg)
		{
			if (this.SetDataDrivenSubscriptionPropertiesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetDataDrivenSubscriptionPropertiesCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x00014F60 File Offset: 0x00013160
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/GetSubscriptionProperties", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
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

		// Token: 0x06000B44 RID: 2884 RVA: 0x00014FD6 File Offset: 0x000131D6
		public IAsyncResult BeginGetSubscriptionProperties(string SubscriptionID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetSubscriptionProperties", new object[] { SubscriptionID }, callback, asyncState);
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x00014FF0 File Offset: 0x000131F0
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

		// Token: 0x06000B46 RID: 2886 RVA: 0x00015058 File Offset: 0x00013258
		public void GetSubscriptionPropertiesAsync(string SubscriptionID)
		{
			this.GetSubscriptionPropertiesAsync(SubscriptionID, null);
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x00015062 File Offset: 0x00013262
		public void GetSubscriptionPropertiesAsync(string SubscriptionID, object userState)
		{
			if (this.GetSubscriptionPropertiesOperationCompleted == null)
			{
				this.GetSubscriptionPropertiesOperationCompleted = new SendOrPostCallback(this.OnGetSubscriptionPropertiesOperationCompleted);
			}
			base.InvokeAsync("GetSubscriptionProperties", new object[] { SubscriptionID }, this.GetSubscriptionPropertiesOperationCompleted, userState);
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x0001509C File Offset: 0x0001329C
		private void OnGetSubscriptionPropertiesOperationCompleted(object arg)
		{
			if (this.GetSubscriptionPropertiesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetSubscriptionPropertiesCompleted(this, new GetSubscriptionPropertiesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x000150E4 File Offset: 0x000132E4
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/GetDataDrivenSubscriptionProperties", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
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

		// Token: 0x06000B4A RID: 2890 RVA: 0x00015165 File Offset: 0x00013365
		public IAsyncResult BeginGetDataDrivenSubscriptionProperties(string DataDrivenSubscriptionID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetDataDrivenSubscriptionProperties", new object[] { DataDrivenSubscriptionID }, callback, asyncState);
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x00015180 File Offset: 0x00013380
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

		// Token: 0x06000B4C RID: 2892 RVA: 0x000151F3 File Offset: 0x000133F3
		public void GetDataDrivenSubscriptionPropertiesAsync(string DataDrivenSubscriptionID)
		{
			this.GetDataDrivenSubscriptionPropertiesAsync(DataDrivenSubscriptionID, null);
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x000151FD File Offset: 0x000133FD
		public void GetDataDrivenSubscriptionPropertiesAsync(string DataDrivenSubscriptionID, object userState)
		{
			if (this.GetDataDrivenSubscriptionPropertiesOperationCompleted == null)
			{
				this.GetDataDrivenSubscriptionPropertiesOperationCompleted = new SendOrPostCallback(this.OnGetDataDrivenSubscriptionPropertiesOperationCompleted);
			}
			base.InvokeAsync("GetDataDrivenSubscriptionProperties", new object[] { DataDrivenSubscriptionID }, this.GetDataDrivenSubscriptionPropertiesOperationCompleted, userState);
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x00015238 File Offset: 0x00013438
		private void OnGetDataDrivenSubscriptionPropertiesOperationCompleted(object arg)
		{
			if (this.GetDataDrivenSubscriptionPropertiesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetDataDrivenSubscriptionPropertiesCompleted(this, new GetDataDrivenSubscriptionPropertiesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x0001527D File Offset: 0x0001347D
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/DeleteSubscription", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void DeleteSubscription(string SubscriptionID)
		{
			base.Invoke("DeleteSubscription", new object[] { SubscriptionID });
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x00015295 File Offset: 0x00013495
		public IAsyncResult BeginDeleteSubscription(string SubscriptionID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("DeleteSubscription", new object[] { SubscriptionID }, callback, asyncState);
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x000152AE File Offset: 0x000134AE
		public void EndDeleteSubscription(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x000152B8 File Offset: 0x000134B8
		public void DeleteSubscriptionAsync(string SubscriptionID)
		{
			this.DeleteSubscriptionAsync(SubscriptionID, null);
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x000152C2 File Offset: 0x000134C2
		public void DeleteSubscriptionAsync(string SubscriptionID, object userState)
		{
			if (this.DeleteSubscriptionOperationCompleted == null)
			{
				this.DeleteSubscriptionOperationCompleted = new SendOrPostCallback(this.OnDeleteSubscriptionOperationCompleted);
			}
			base.InvokeAsync("DeleteSubscription", new object[] { SubscriptionID }, this.DeleteSubscriptionOperationCompleted, userState);
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x000152FC File Offset: 0x000134FC
		private void OnDeleteSubscriptionOperationCompleted(object arg)
		{
			if (this.DeleteSubscriptionCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.DeleteSubscriptionCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x0001533C File Offset: 0x0001353C
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/PrepareQuery", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("DataSettings")]
		public DataSetDefinition PrepareQuery(DataSource DataSource, DataSetDefinition DataSet, out bool Changed, out string[] ParameterNames)
		{
			object[] array = base.Invoke("PrepareQuery", new object[] { DataSource, DataSet });
			Changed = (bool)array[1];
			ParameterNames = (string[])array[2];
			return (DataSetDefinition)array[0];
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x00015380 File Offset: 0x00013580
		public IAsyncResult BeginPrepareQuery(DataSource DataSource, DataSetDefinition DataSet, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("PrepareQuery", new object[] { DataSource, DataSet }, callback, asyncState);
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x000153A0 File Offset: 0x000135A0
		public DataSetDefinition EndPrepareQuery(IAsyncResult asyncResult, out bool Changed, out string[] ParameterNames)
		{
			object[] array = base.EndInvoke(asyncResult);
			Changed = (bool)array[1];
			ParameterNames = (string[])array[2];
			return (DataSetDefinition)array[0];
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x000153D1 File Offset: 0x000135D1
		public void PrepareQueryAsync(DataSource DataSource, DataSetDefinition DataSet)
		{
			this.PrepareQueryAsync(DataSource, DataSet, null);
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x000153DC File Offset: 0x000135DC
		public void PrepareQueryAsync(DataSource DataSource, DataSetDefinition DataSet, object userState)
		{
			if (this.PrepareQueryOperationCompleted == null)
			{
				this.PrepareQueryOperationCompleted = new SendOrPostCallback(this.OnPrepareQueryOperationCompleted);
			}
			base.InvokeAsync("PrepareQuery", new object[] { DataSource, DataSet }, this.PrepareQueryOperationCompleted, userState);
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x00015418 File Offset: 0x00013618
		private void OnPrepareQueryOperationCompleted(object arg)
		{
			if (this.PrepareQueryCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.PrepareQueryCompleted(this, new PrepareQueryCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x0001545D File Offset: 0x0001365D
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/GetExtensionSettings", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("ExtensionParameters")]
		public ExtensionParameter[] GetExtensionSettings(string Extension)
		{
			return (ExtensionParameter[])base.Invoke("GetExtensionSettings", new object[] { Extension })[0];
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x0001547B File Offset: 0x0001367B
		public IAsyncResult BeginGetExtensionSettings(string Extension, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetExtensionSettings", new object[] { Extension }, callback, asyncState);
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x00015494 File Offset: 0x00013694
		public ExtensionParameter[] EndGetExtensionSettings(IAsyncResult asyncResult)
		{
			return (ExtensionParameter[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x000154A4 File Offset: 0x000136A4
		public void GetExtensionSettingsAsync(string Extension)
		{
			this.GetExtensionSettingsAsync(Extension, null);
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x000154AE File Offset: 0x000136AE
		public void GetExtensionSettingsAsync(string Extension, object userState)
		{
			if (this.GetExtensionSettingsOperationCompleted == null)
			{
				this.GetExtensionSettingsOperationCompleted = new SendOrPostCallback(this.OnGetExtensionSettingsOperationCompleted);
			}
			base.InvokeAsync("GetExtensionSettings", new object[] { Extension }, this.GetExtensionSettingsOperationCompleted, userState);
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x000154E8 File Offset: 0x000136E8
		private void OnGetExtensionSettingsOperationCompleted(object arg)
		{
			if (this.GetExtensionSettingsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetExtensionSettingsCompleted(this, new GetExtensionSettingsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x0001552D File Offset: 0x0001372D
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/ValidateExtensionSettings", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("ParameterErrors")]
		public ExtensionParameter[] ValidateExtensionSettings(string Extension, ParameterValueOrFieldReference[] ParameterValues, string Site)
		{
			return (ExtensionParameter[])base.Invoke("ValidateExtensionSettings", new object[] { Extension, ParameterValues, Site })[0];
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x00015553 File Offset: 0x00013753
		public IAsyncResult BeginValidateExtensionSettings(string Extension, ParameterValueOrFieldReference[] ParameterValues, string Site, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ValidateExtensionSettings", new object[] { Extension, ParameterValues, Site }, callback, asyncState);
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x00015576 File Offset: 0x00013776
		public ExtensionParameter[] EndValidateExtensionSettings(IAsyncResult asyncResult)
		{
			return (ExtensionParameter[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x00015586 File Offset: 0x00013786
		public void ValidateExtensionSettingsAsync(string Extension, ParameterValueOrFieldReference[] ParameterValues, string Site)
		{
			this.ValidateExtensionSettingsAsync(Extension, ParameterValues, Site, null);
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x00015594 File Offset: 0x00013794
		public void ValidateExtensionSettingsAsync(string Extension, ParameterValueOrFieldReference[] ParameterValues, string Site, object userState)
		{
			if (this.ValidateExtensionSettingsOperationCompleted == null)
			{
				this.ValidateExtensionSettingsOperationCompleted = new SendOrPostCallback(this.OnValidateExtensionSettingsOperationCompleted);
			}
			base.InvokeAsync("ValidateExtensionSettings", new object[] { Extension, ParameterValues, Site }, this.ValidateExtensionSettingsOperationCompleted, userState);
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x000155E0 File Offset: 0x000137E0
		private void OnValidateExtensionSettingsOperationCompleted(object arg)
		{
			if (this.ValidateExtensionSettingsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ValidateExtensionSettingsCompleted(this, new ValidateExtensionSettingsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x00015625 File Offset: 0x00013825
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/ListAllSubscriptions", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("SubscriptionItems")]
		public Subscription[] ListAllSubscriptions(string Site)
		{
			return (Subscription[])base.Invoke("ListAllSubscriptions", new object[] { Site })[0];
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x00015643 File Offset: 0x00013843
		public IAsyncResult BeginListAllSubscriptions(string Site, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListAllSubscriptions", new object[] { Site }, callback, asyncState);
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x0001565C File Offset: 0x0001385C
		public Subscription[] EndListAllSubscriptions(IAsyncResult asyncResult)
		{
			return (Subscription[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x0001566C File Offset: 0x0001386C
		public void ListAllSubscriptionsAsync(string Site)
		{
			this.ListAllSubscriptionsAsync(Site, null);
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x00015676 File Offset: 0x00013876
		public void ListAllSubscriptionsAsync(string Site, object userState)
		{
			if (this.ListAllSubscriptionsOperationCompleted == null)
			{
				this.ListAllSubscriptionsOperationCompleted = new SendOrPostCallback(this.OnListAllSubscriptionsOperationCompleted);
			}
			base.InvokeAsync("ListAllSubscriptions", new object[] { Site }, this.ListAllSubscriptionsOperationCompleted, userState);
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x000156B0 File Offset: 0x000138B0
		private void OnListAllSubscriptionsOperationCompleted(object arg)
		{
			if (this.ListAllSubscriptionsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListAllSubscriptionsCompleted(this, new ListAllSubscriptionsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x000156F5 File Offset: 0x000138F5
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/ListMySubscriptions", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("SubscriptionItems")]
		public Subscription[] ListMySubscriptions(string Site)
		{
			return (Subscription[])base.Invoke("ListMySubscriptions", new object[] { Site })[0];
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x00015713 File Offset: 0x00013913
		public IAsyncResult BeginListMySubscriptions(string Site, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListMySubscriptions", new object[] { Site }, callback, asyncState);
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x0001572C File Offset: 0x0001392C
		public Subscription[] EndListMySubscriptions(IAsyncResult asyncResult)
		{
			return (Subscription[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x0001573C File Offset: 0x0001393C
		public void ListMySubscriptionsAsync(string Site)
		{
			this.ListMySubscriptionsAsync(Site, null);
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x00015746 File Offset: 0x00013946
		public void ListMySubscriptionsAsync(string Site, object userState)
		{
			if (this.ListMySubscriptionsOperationCompleted == null)
			{
				this.ListMySubscriptionsOperationCompleted = new SendOrPostCallback(this.OnListMySubscriptionsOperationCompleted);
			}
			base.InvokeAsync("ListMySubscriptions", new object[] { Site }, this.ListMySubscriptionsOperationCompleted, userState);
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x00015780 File Offset: 0x00013980
		private void OnListMySubscriptionsOperationCompleted(object arg)
		{
			if (this.ListMySubscriptionsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListMySubscriptionsCompleted(this, new ListMySubscriptionsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x000157C5 File Offset: 0x000139C5
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/ListReportSubscriptions", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("SubscriptionItems")]
		public Subscription[] ListReportSubscriptions(string Report)
		{
			return (Subscription[])base.Invoke("ListReportSubscriptions", new object[] { Report })[0];
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x000157E3 File Offset: 0x000139E3
		public IAsyncResult BeginListReportSubscriptions(string Report, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListReportSubscriptions", new object[] { Report }, callback, asyncState);
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x000157FC File Offset: 0x000139FC
		public Subscription[] EndListReportSubscriptions(IAsyncResult asyncResult)
		{
			return (Subscription[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x0001580C File Offset: 0x00013A0C
		public void ListReportSubscriptionsAsync(string Report)
		{
			this.ListReportSubscriptionsAsync(Report, null);
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x00015816 File Offset: 0x00013A16
		public void ListReportSubscriptionsAsync(string Report, object userState)
		{
			if (this.ListReportSubscriptionsOperationCompleted == null)
			{
				this.ListReportSubscriptionsOperationCompleted = new SendOrPostCallback(this.OnListReportSubscriptionsOperationCompleted);
			}
			base.InvokeAsync("ListReportSubscriptions", new object[] { Report }, this.ListReportSubscriptionsOperationCompleted, userState);
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x00015850 File Offset: 0x00013A50
		private void OnListReportSubscriptionsOperationCompleted(object arg)
		{
			if (this.ListReportSubscriptionsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListReportSubscriptionsCompleted(this, new ListReportSubscriptionsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x00015895 File Offset: 0x00013A95
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/ListExtensions", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Extensions")]
		public Extension[] ListExtensions(ExtensionTypeEnum ExtensionType)
		{
			return (Extension[])base.Invoke("ListExtensions", new object[] { ExtensionType })[0];
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x000158B8 File Offset: 0x00013AB8
		public IAsyncResult BeginListExtensions(ExtensionTypeEnum ExtensionType, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListExtensions", new object[] { ExtensionType }, callback, asyncState);
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x000158D6 File Offset: 0x00013AD6
		public Extension[] EndListExtensions(IAsyncResult asyncResult)
		{
			return (Extension[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x000158E6 File Offset: 0x00013AE6
		public void ListExtensionsAsync(ExtensionTypeEnum ExtensionType)
		{
			this.ListExtensionsAsync(ExtensionType, null);
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x000158F0 File Offset: 0x00013AF0
		public void ListExtensionsAsync(ExtensionTypeEnum ExtensionType, object userState)
		{
			if (this.ListExtensionsOperationCompleted == null)
			{
				this.ListExtensionsOperationCompleted = new SendOrPostCallback(this.OnListExtensionsOperationCompleted);
			}
			base.InvokeAsync("ListExtensions", new object[] { ExtensionType }, this.ListExtensionsOperationCompleted, userState);
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x00015930 File Offset: 0x00013B30
		private void OnListExtensionsOperationCompleted(object arg)
		{
			if (this.ListExtensionsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListExtensionsCompleted(this, new ListExtensionsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x00015975 File Offset: 0x00013B75
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/ListEvents", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Events")]
		public Event[] ListEvents()
		{
			return (Event[])base.Invoke("ListEvents", new object[0])[0];
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x0001598F File Offset: 0x00013B8F
		public IAsyncResult BeginListEvents(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListEvents", new object[0], callback, asyncState);
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x000159A4 File Offset: 0x00013BA4
		public Event[] EndListEvents(IAsyncResult asyncResult)
		{
			return (Event[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x000159B4 File Offset: 0x00013BB4
		public void ListEventsAsync()
		{
			this.ListEventsAsync(null);
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x000159BD File Offset: 0x00013BBD
		public void ListEventsAsync(object userState)
		{
			if (this.ListEventsOperationCompleted == null)
			{
				this.ListEventsOperationCompleted = new SendOrPostCallback(this.OnListEventsOperationCompleted);
			}
			base.InvokeAsync("ListEvents", new object[0], this.ListEventsOperationCompleted, userState);
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x000159F4 File Offset: 0x00013BF4
		private void OnListEventsOperationCompleted(object arg)
		{
			if (this.ListEventsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListEventsCompleted(this, new ListEventsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x00015A39 File Offset: 0x00013C39
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/FireEvent", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void FireEvent(string EventType, string EventData, string Site)
		{
			base.Invoke("FireEvent", new object[] { EventType, EventData, Site });
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x00015A59 File Offset: 0x00013C59
		public IAsyncResult BeginFireEvent(string EventType, string EventData, string Site, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("FireEvent", new object[] { EventType, EventData, Site }, callback, asyncState);
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x00015A7C File Offset: 0x00013C7C
		public void EndFireEvent(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x00015A86 File Offset: 0x00013C86
		public void FireEventAsync(string EventType, string EventData, string Site)
		{
			this.FireEventAsync(EventType, EventData, Site, null);
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x00015A94 File Offset: 0x00013C94
		public void FireEventAsync(string EventType, string EventData, string Site, object userState)
		{
			if (this.FireEventOperationCompleted == null)
			{
				this.FireEventOperationCompleted = new SendOrPostCallback(this.OnFireEventOperationCompleted);
			}
			base.InvokeAsync("FireEvent", new object[] { EventType, EventData, Site }, this.FireEventOperationCompleted, userState);
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x00015AE0 File Offset: 0x00013CE0
		private void OnFireEventOperationCompleted(object arg)
		{
			if (this.FireEventCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.FireEventCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x00015B1F File Offset: 0x00013D1F
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/ListTasks", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Tasks")]
		public Task[] ListTasks(SecurityScopeEnum SecurityScope)
		{
			return (Task[])base.Invoke("ListTasks", new object[] { SecurityScope })[0];
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x00015B42 File Offset: 0x00013D42
		public IAsyncResult BeginListTasks(SecurityScopeEnum SecurityScope, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListTasks", new object[] { SecurityScope }, callback, asyncState);
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x00015B60 File Offset: 0x00013D60
		public Task[] EndListTasks(IAsyncResult asyncResult)
		{
			return (Task[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x00015B70 File Offset: 0x00013D70
		public void ListTasksAsync(SecurityScopeEnum SecurityScope)
		{
			this.ListTasksAsync(SecurityScope, null);
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x00015B7A File Offset: 0x00013D7A
		public void ListTasksAsync(SecurityScopeEnum SecurityScope, object userState)
		{
			if (this.ListTasksOperationCompleted == null)
			{
				this.ListTasksOperationCompleted = new SendOrPostCallback(this.OnListTasksOperationCompleted);
			}
			base.InvokeAsync("ListTasks", new object[] { SecurityScope }, this.ListTasksOperationCompleted, userState);
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x00015BB8 File Offset: 0x00013DB8
		private void OnListTasksOperationCompleted(object arg)
		{
			if (this.ListTasksCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListTasksCompleted(this, new ListTasksCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x00015BFD File Offset: 0x00013DFD
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/ListRoles", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Roles")]
		public Role[] ListRoles(SecurityScopeEnum SecurityScope, string Path)
		{
			return (Role[])base.Invoke("ListRoles", new object[] { SecurityScope, Path })[0];
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x00015C24 File Offset: 0x00013E24
		public IAsyncResult BeginListRoles(SecurityScopeEnum SecurityScope, string Path, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListRoles", new object[] { SecurityScope, Path }, callback, asyncState);
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x00015C47 File Offset: 0x00013E47
		public Role[] EndListRoles(IAsyncResult asyncResult)
		{
			return (Role[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x00015C57 File Offset: 0x00013E57
		public void ListRolesAsync(SecurityScopeEnum SecurityScope, string Path)
		{
			this.ListRolesAsync(SecurityScope, Path, null);
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x00015C64 File Offset: 0x00013E64
		public void ListRolesAsync(SecurityScopeEnum SecurityScope, string Path, object userState)
		{
			if (this.ListRolesOperationCompleted == null)
			{
				this.ListRolesOperationCompleted = new SendOrPostCallback(this.OnListRolesOperationCompleted);
			}
			base.InvokeAsync("ListRoles", new object[] { SecurityScope, Path }, this.ListRolesOperationCompleted, userState);
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x00015CB0 File Offset: 0x00013EB0
		private void OnListRolesOperationCompleted(object arg)
		{
			if (this.ListRolesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListRolesCompleted(this, new ListRolesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x00015CF8 File Offset: 0x00013EF8
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/GetRoleProperties", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Tasks")]
		public Task[] GetRoleProperties(string Name, string Site, out string Description)
		{
			object[] array = base.Invoke("GetRoleProperties", new object[] { Name, Site });
			Description = (string)array[1];
			return (Task[])array[0];
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x00015D31 File Offset: 0x00013F31
		public IAsyncResult BeginGetRoleProperties(string Name, string Site, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetRoleProperties", new object[] { Name, Site }, callback, asyncState);
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x00015D50 File Offset: 0x00013F50
		public Task[] EndGetRoleProperties(IAsyncResult asyncResult, out string Description)
		{
			object[] array = base.EndInvoke(asyncResult);
			Description = (string)array[1];
			return (Task[])array[0];
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x00015D77 File Offset: 0x00013F77
		public void GetRolePropertiesAsync(string Name, string Site)
		{
			this.GetRolePropertiesAsync(Name, Site, null);
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x00015D82 File Offset: 0x00013F82
		public void GetRolePropertiesAsync(string Name, string Site, object userState)
		{
			if (this.GetRolePropertiesOperationCompleted == null)
			{
				this.GetRolePropertiesOperationCompleted = new SendOrPostCallback(this.OnGetRolePropertiesOperationCompleted);
			}
			base.InvokeAsync("GetRoleProperties", new object[] { Name, Site }, this.GetRolePropertiesOperationCompleted, userState);
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x00015DC0 File Offset: 0x00013FC0
		private void OnGetRolePropertiesOperationCompleted(object arg)
		{
			if (this.GetRolePropertiesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetRolePropertiesCompleted(this, new GetRolePropertiesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x00015E08 File Offset: 0x00014008
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/GetPolicies", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Policies")]
		public Policy[] GetPolicies(string Item, out bool InheritParent)
		{
			object[] array = base.Invoke("GetPolicies", new object[] { Item });
			InheritParent = (bool)array[1];
			return (Policy[])array[0];
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x00015E3D File Offset: 0x0001403D
		public IAsyncResult BeginGetPolicies(string Item, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetPolicies", new object[] { Item }, callback, asyncState);
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x00015E58 File Offset: 0x00014058
		public Policy[] EndGetPolicies(IAsyncResult asyncResult, out bool InheritParent)
		{
			object[] array = base.EndInvoke(asyncResult);
			InheritParent = (bool)array[1];
			return (Policy[])array[0];
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x00015E7F File Offset: 0x0001407F
		public void GetPoliciesAsync(string Item)
		{
			this.GetPoliciesAsync(Item, null);
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x00015E89 File Offset: 0x00014089
		public void GetPoliciesAsync(string Item, object userState)
		{
			if (this.GetPoliciesOperationCompleted == null)
			{
				this.GetPoliciesOperationCompleted = new SendOrPostCallback(this.OnGetPoliciesOperationCompleted);
			}
			base.InvokeAsync("GetPolicies", new object[] { Item }, this.GetPoliciesOperationCompleted, userState);
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x00015EC4 File Offset: 0x000140C4
		private void OnGetPoliciesOperationCompleted(object arg)
		{
			if (this.GetPoliciesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetPoliciesCompleted(this, new GetPoliciesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x00015F09 File Offset: 0x00014109
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/SetPolicies", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void SetPolicies(string Item, Policy[] Policies)
		{
			base.Invoke("SetPolicies", new object[] { Item, Policies });
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x00015F25 File Offset: 0x00014125
		public IAsyncResult BeginSetPolicies(string Item, Policy[] Policies, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetPolicies", new object[] { Item, Policies }, callback, asyncState);
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x00015F43 File Offset: 0x00014143
		public void EndSetPolicies(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x00015F4D File Offset: 0x0001414D
		public void SetPoliciesAsync(string Item, Policy[] Policies)
		{
			this.SetPoliciesAsync(Item, Policies, null);
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x00015F58 File Offset: 0x00014158
		public void SetPoliciesAsync(string Item, Policy[] Policies, object userState)
		{
			if (this.SetPoliciesOperationCompleted == null)
			{
				this.SetPoliciesOperationCompleted = new SendOrPostCallback(this.OnSetPoliciesOperationCompleted);
			}
			base.InvokeAsync("SetPolicies", new object[] { Item, Policies }, this.SetPoliciesOperationCompleted, userState);
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x00015F94 File Offset: 0x00014194
		private void OnSetPoliciesOperationCompleted(object arg)
		{
			if (this.SetPoliciesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetPoliciesCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x00015FD3 File Offset: 0x000141D3
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/InheritParentSecurity", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void InheritParentSecurity(string Item)
		{
			base.Invoke("InheritParentSecurity", new object[] { Item });
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x00015FEB File Offset: 0x000141EB
		public IAsyncResult BeginInheritParentSecurity(string Item, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("InheritParentSecurity", new object[] { Item }, callback, asyncState);
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x00016004 File Offset: 0x00014204
		public void EndInheritParentSecurity(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x0001600E File Offset: 0x0001420E
		public void InheritParentSecurityAsync(string Item)
		{
			this.InheritParentSecurityAsync(Item, null);
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x00016018 File Offset: 0x00014218
		public void InheritParentSecurityAsync(string Item, object userState)
		{
			if (this.InheritParentSecurityOperationCompleted == null)
			{
				this.InheritParentSecurityOperationCompleted = new SendOrPostCallback(this.OnInheritParentSecurityOperationCompleted);
			}
			base.InvokeAsync("InheritParentSecurity", new object[] { Item }, this.InheritParentSecurityOperationCompleted, userState);
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x00016050 File Offset: 0x00014250
		private void OnInheritParentSecurityOperationCompleted(object arg)
		{
			if (this.InheritParentSecurityCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.InheritParentSecurityCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x0001608F File Offset: 0x0001428F
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/GetPermissions", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Permissions")]
		[return: XmlArrayItem("Operation")]
		public string[] GetPermissions(string Item)
		{
			return (string[])base.Invoke("GetPermissions", new object[] { Item })[0];
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x000160AD File Offset: 0x000142AD
		public IAsyncResult BeginGetPermissions(string Item, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetPermissions", new object[] { Item }, callback, asyncState);
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x000160C6 File Offset: 0x000142C6
		public string[] EndGetPermissions(IAsyncResult asyncResult)
		{
			return (string[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x000160D6 File Offset: 0x000142D6
		public void GetPermissionsAsync(string Item)
		{
			this.GetPermissionsAsync(Item, null);
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x000160E0 File Offset: 0x000142E0
		public void GetPermissionsAsync(string Item, object userState)
		{
			if (this.GetPermissionsOperationCompleted == null)
			{
				this.GetPermissionsOperationCompleted = new SendOrPostCallback(this.OnGetPermissionsOperationCompleted);
			}
			base.InvokeAsync("GetPermissions", new object[] { Item }, this.GetPermissionsOperationCompleted, userState);
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x00016118 File Offset: 0x00014318
		private void OnGetPermissionsOperationCompleted(object arg)
		{
			if (this.GetPermissionsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetPermissionsCompleted(this, new GetPermissionsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x00016160 File Offset: 0x00014360
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/CreateModel", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("ItemInfo")]
		public CatalogItem CreateModel(string Model, string Parent, [XmlElement(DataType = "base64Binary")] byte[] Definition, Property[] Properties, out Warning[] Warnings)
		{
			object[] array = base.Invoke("CreateModel", new object[] { Model, Parent, Definition, Properties });
			Warnings = (Warning[])array[1];
			return (CatalogItem)array[0];
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x000161A3 File Offset: 0x000143A3
		public IAsyncResult BeginCreateModel(string Model, string Parent, byte[] Definition, Property[] Properties, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateModel", new object[] { Model, Parent, Definition, Properties }, callback, asyncState);
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x000161CC File Offset: 0x000143CC
		public CatalogItem EndCreateModel(IAsyncResult asyncResult, out Warning[] Warnings)
		{
			object[] array = base.EndInvoke(asyncResult);
			Warnings = (Warning[])array[1];
			return (CatalogItem)array[0];
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x000161F3 File Offset: 0x000143F3
		public void CreateModelAsync(string Model, string Parent, byte[] Definition, Property[] Properties)
		{
			this.CreateModelAsync(Model, Parent, Definition, Properties, null);
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x00016204 File Offset: 0x00014404
		public void CreateModelAsync(string Model, string Parent, byte[] Definition, Property[] Properties, object userState)
		{
			if (this.CreateModelOperationCompleted == null)
			{
				this.CreateModelOperationCompleted = new SendOrPostCallback(this.OnCreateModelOperationCompleted);
			}
			base.InvokeAsync("CreateModel", new object[] { Model, Parent, Definition, Properties }, this.CreateModelOperationCompleted, userState);
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x00016258 File Offset: 0x00014458
		private void OnCreateModelOperationCompleted(object arg)
		{
			if (this.CreateModelCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateModelCompleted(this, new CreateModelCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x0001629D File Offset: 0x0001449D
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/GetModelDefinition", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Definition", DataType = "base64Binary")]
		public byte[] GetModelDefinition(string Model)
		{
			return (byte[])base.Invoke("GetModelDefinition", new object[] { Model })[0];
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x000162BB File Offset: 0x000144BB
		public IAsyncResult BeginGetModelDefinition(string Model, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetModelDefinition", new object[] { Model }, callback, asyncState);
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x000162D4 File Offset: 0x000144D4
		public byte[] EndGetModelDefinition(IAsyncResult asyncResult)
		{
			return (byte[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x000162E4 File Offset: 0x000144E4
		public void GetModelDefinitionAsync(string Model)
		{
			this.GetModelDefinitionAsync(Model, null);
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x000162EE File Offset: 0x000144EE
		public void GetModelDefinitionAsync(string Model, object userState)
		{
			if (this.GetModelDefinitionOperationCompleted == null)
			{
				this.GetModelDefinitionOperationCompleted = new SendOrPostCallback(this.OnGetModelDefinitionOperationCompleted);
			}
			base.InvokeAsync("GetModelDefinition", new object[] { Model }, this.GetModelDefinitionOperationCompleted, userState);
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x00016328 File Offset: 0x00014528
		private void OnGetModelDefinitionOperationCompleted(object arg)
		{
			if (this.GetModelDefinitionCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetModelDefinitionCompleted(this, new GetModelDefinitionCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x0001636D File Offset: 0x0001456D
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/SetModelDefinition", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Warnings")]
		public Warning[] SetModelDefinition(string Model, [XmlElement(DataType = "base64Binary")] byte[] Definition)
		{
			return (Warning[])base.Invoke("SetModelDefinition", new object[] { Model, Definition })[0];
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x0001638F File Offset: 0x0001458F
		public IAsyncResult BeginSetModelDefinition(string Model, byte[] Definition, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetModelDefinition", new object[] { Model, Definition }, callback, asyncState);
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x000163AD File Offset: 0x000145AD
		public Warning[] EndSetModelDefinition(IAsyncResult asyncResult)
		{
			return (Warning[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x000163BD File Offset: 0x000145BD
		public void SetModelDefinitionAsync(string Model, byte[] Definition)
		{
			this.SetModelDefinitionAsync(Model, Definition, null);
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x000163C8 File Offset: 0x000145C8
		public void SetModelDefinitionAsync(string Model, byte[] Definition, object userState)
		{
			if (this.SetModelDefinitionOperationCompleted == null)
			{
				this.SetModelDefinitionOperationCompleted = new SendOrPostCallback(this.OnSetModelDefinitionOperationCompleted);
			}
			base.InvokeAsync("SetModelDefinition", new object[] { Model, Definition }, this.SetModelDefinitionOperationCompleted, userState);
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x00016404 File Offset: 0x00014604
		private void OnSetModelDefinitionOperationCompleted(object arg)
		{
			if (this.SetModelDefinitionCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetModelDefinitionCompleted(this, new SetModelDefinitionCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x00016449 File Offset: 0x00014649
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/ListModelPerspectives", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("ModelCatalogItems")]
		public ModelCatalogItem[] ListModelPerspectives(string Path)
		{
			return (ModelCatalogItem[])base.Invoke("ListModelPerspectives", new object[] { Path })[0];
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x00016467 File Offset: 0x00014667
		public IAsyncResult BeginListModelPerspectives(string Path, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListModelPerspectives", new object[] { Path }, callback, asyncState);
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x00016480 File Offset: 0x00014680
		public ModelCatalogItem[] EndListModelPerspectives(IAsyncResult asyncResult)
		{
			return (ModelCatalogItem[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x00016490 File Offset: 0x00014690
		public void ListModelPerspectivesAsync(string Path)
		{
			this.ListModelPerspectivesAsync(Path, null);
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x0001649A File Offset: 0x0001469A
		public void ListModelPerspectivesAsync(string Path, object userState)
		{
			if (this.ListModelPerspectivesOperationCompleted == null)
			{
				this.ListModelPerspectivesOperationCompleted = new SendOrPostCallback(this.OnListModelPerspectivesOperationCompleted);
			}
			base.InvokeAsync("ListModelPerspectives", new object[] { Path }, this.ListModelPerspectivesOperationCompleted, userState);
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x000164D4 File Offset: 0x000146D4
		private void OnListModelPerspectivesOperationCompleted(object arg)
		{
			if (this.ListModelPerspectivesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListModelPerspectivesCompleted(this, new ListModelPerspectivesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x00016519 File Offset: 0x00014719
		public new void CancelAsync(object userState)
		{
			base.CancelAsync(userState);
		}

		// Token: 0x04000270 RID: 624
		private TrustedUserHeader trustedUserHeaderValueField;

		// Token: 0x04000271 RID: 625
		private ServerInfoHeader serverInfoHeaderValueField;

		// Token: 0x04000272 RID: 626
		private SendOrPostCallback GetUserModelOperationCompleted;

		// Token: 0x04000273 RID: 627
		private SendOrPostCallback ListModelItemChildrenOperationCompleted;

		// Token: 0x04000274 RID: 628
		private SendOrPostCallback GetModelItemPermissionsOperationCompleted;

		// Token: 0x04000275 RID: 629
		private SendOrPostCallback GetModelItemPoliciesOperationCompleted;

		// Token: 0x04000276 RID: 630
		private SendOrPostCallback SetModelItemPoliciesOperationCompleted;

		// Token: 0x04000277 RID: 631
		private SendOrPostCallback InheritModelItemParentSecurityOperationCompleted;

		// Token: 0x04000278 RID: 632
		private SendOrPostCallback RemoveAllModelItemPoliciesOperationCompleted;

		// Token: 0x04000279 RID: 633
		private SendOrPostCallback SetModelDrillthroughReportsOperationCompleted;

		// Token: 0x0400027A RID: 634
		private SendOrPostCallback ListModelDrillthroughReportsOperationCompleted;

		// Token: 0x0400027B RID: 635
		private SendOrPostCallback GenerateModelOperationCompleted;

		// Token: 0x0400027C RID: 636
		private SendOrPostCallback RegenerateModelOperationCompleted;

		// Token: 0x0400027D RID: 637
		private SendOrPostCallback GetReportServerConfigInfoOperationCompleted;

		// Token: 0x0400027E RID: 638
		private SendOrPostCallback ListSecureMethodsOperationCompleted;

		// Token: 0x0400027F RID: 639
		private SendOrPostCallback GetSystemPropertiesOperationCompleted;

		// Token: 0x04000280 RID: 640
		private SendOrPostCallback SetSystemPropertiesOperationCompleted;

		// Token: 0x04000281 RID: 641
		private SendOrPostCallback DeleteItemOperationCompleted;

		// Token: 0x04000282 RID: 642
		private SendOrPostCallback MoveItemOperationCompleted;

		// Token: 0x04000283 RID: 643
		private SendOrPostCallback ListChildrenOperationCompleted;

		// Token: 0x04000284 RID: 644
		private SendOrPostCallback ListParentsOperationCompleted;

		// Token: 0x04000285 RID: 645
		private SendOrPostCallback ListDependentItemsOperationCompleted;

		// Token: 0x04000286 RID: 646
		private SendOrPostCallback GetPropertiesOperationCompleted;

		// Token: 0x04000287 RID: 647
		private SendOrPostCallback SetPropertiesOperationCompleted;

		// Token: 0x04000288 RID: 648
		private SendOrPostCallback GetItemTypeOperationCompleted;

		// Token: 0x04000289 RID: 649
		private SendOrPostCallback CreateFolderOperationCompleted;

		// Token: 0x0400028A RID: 650
		private SendOrPostCallback CreateReportOperationCompleted;

		// Token: 0x0400028B RID: 651
		private SendOrPostCallback GetReportDefinitionOperationCompleted;

		// Token: 0x0400028C RID: 652
		private SendOrPostCallback SetReportDefinitionOperationCompleted;

		// Token: 0x0400028D RID: 653
		private SendOrPostCallback CreateResourceOperationCompleted;

		// Token: 0x0400028E RID: 654
		private SendOrPostCallback SetResourceContentsOperationCompleted;

		// Token: 0x0400028F RID: 655
		private SendOrPostCallback GetResourceContentsOperationCompleted;

		// Token: 0x04000290 RID: 656
		private SendOrPostCallback CreateReportEditSessionOperationCompleted;

		// Token: 0x04000291 RID: 657
		private SendOrPostCallback GetReportParametersOperationCompleted;

		// Token: 0x04000292 RID: 658
		private SendOrPostCallback SetReportParametersOperationCompleted;

		// Token: 0x04000293 RID: 659
		private SendOrPostCallback SetExecutionOptionsOperationCompleted;

		// Token: 0x04000294 RID: 660
		private SendOrPostCallback GetExecutionOptionsOperationCompleted;

		// Token: 0x04000295 RID: 661
		private SendOrPostCallback SetCacheOptionsOperationCompleted;

		// Token: 0x04000296 RID: 662
		private SendOrPostCallback GetCacheOptionsOperationCompleted;

		// Token: 0x04000297 RID: 663
		private SendOrPostCallback UpdateReportExecutionSnapshotOperationCompleted;

		// Token: 0x04000298 RID: 664
		private SendOrPostCallback FlushCacheOperationCompleted;

		// Token: 0x04000299 RID: 665
		private SendOrPostCallback ListJobsOperationCompleted;

		// Token: 0x0400029A RID: 666
		private SendOrPostCallback CancelJobOperationCompleted;

		// Token: 0x0400029B RID: 667
		private SendOrPostCallback CreateDataSourceOperationCompleted;

		// Token: 0x0400029C RID: 668
		private SendOrPostCallback GetDataSourceContentsOperationCompleted;

		// Token: 0x0400029D RID: 669
		private SendOrPostCallback SetDataSourceContentsOperationCompleted;

		// Token: 0x0400029E RID: 670
		private SendOrPostCallback EnableDataSourceOperationCompleted;

		// Token: 0x0400029F RID: 671
		private SendOrPostCallback DisableDataSourceOperationCompleted;

		// Token: 0x040002A0 RID: 672
		private SendOrPostCallback SetItemDataSourcesOperationCompleted;

		// Token: 0x040002A1 RID: 673
		private SendOrPostCallback GetItemDataSourcesOperationCompleted;

		// Token: 0x040002A2 RID: 674
		private SendOrPostCallback GetItemDataSourcePromptsOperationCompleted;

		// Token: 0x040002A3 RID: 675
		private SendOrPostCallback TestConnectForDataSourceDefinitionOperationCompleted;

		// Token: 0x040002A4 RID: 676
		private SendOrPostCallback TestConnectForItemDataSourceOperationCompleted;

		// Token: 0x040002A5 RID: 677
		private SendOrPostCallback CreateReportHistorySnapshotOperationCompleted;

		// Token: 0x040002A6 RID: 678
		private SendOrPostCallback SetReportHistoryOptionsOperationCompleted;

		// Token: 0x040002A7 RID: 679
		private SendOrPostCallback GetReportHistoryOptionsOperationCompleted;

		// Token: 0x040002A8 RID: 680
		private SendOrPostCallback SetReportHistoryLimitOperationCompleted;

		// Token: 0x040002A9 RID: 681
		private SendOrPostCallback GetReportHistoryLimitOperationCompleted;

		// Token: 0x040002AA RID: 682
		private SendOrPostCallback ListReportHistoryOperationCompleted;

		// Token: 0x040002AB RID: 683
		private SendOrPostCallback DeleteReportHistorySnapshotOperationCompleted;

		// Token: 0x040002AC RID: 684
		private SendOrPostCallback CreateScheduleOperationCompleted;

		// Token: 0x040002AD RID: 685
		private SendOrPostCallback DeleteScheduleOperationCompleted;

		// Token: 0x040002AE RID: 686
		private SendOrPostCallback SetSchedulePropertiesOperationCompleted;

		// Token: 0x040002AF RID: 687
		private SendOrPostCallback GetSchedulePropertiesOperationCompleted;

		// Token: 0x040002B0 RID: 688
		private SendOrPostCallback ListScheduledReportsOperationCompleted;

		// Token: 0x040002B1 RID: 689
		private SendOrPostCallback ListSchedulesOperationCompleted;

		// Token: 0x040002B2 RID: 690
		private SendOrPostCallback PauseScheduleOperationCompleted;

		// Token: 0x040002B3 RID: 691
		private SendOrPostCallback ResumeScheduleOperationCompleted;

		// Token: 0x040002B4 RID: 692
		private SendOrPostCallback CreateSubscriptionOperationCompleted;

		// Token: 0x040002B5 RID: 693
		private SendOrPostCallback CreateDataDrivenSubscriptionOperationCompleted;

		// Token: 0x040002B6 RID: 694
		private SendOrPostCallback SetSubscriptionPropertiesOperationCompleted;

		// Token: 0x040002B7 RID: 695
		private SendOrPostCallback SetDataDrivenSubscriptionPropertiesOperationCompleted;

		// Token: 0x040002B8 RID: 696
		private SendOrPostCallback GetSubscriptionPropertiesOperationCompleted;

		// Token: 0x040002B9 RID: 697
		private SendOrPostCallback GetDataDrivenSubscriptionPropertiesOperationCompleted;

		// Token: 0x040002BA RID: 698
		private SendOrPostCallback DeleteSubscriptionOperationCompleted;

		// Token: 0x040002BB RID: 699
		private SendOrPostCallback PrepareQueryOperationCompleted;

		// Token: 0x040002BC RID: 700
		private SendOrPostCallback GetExtensionSettingsOperationCompleted;

		// Token: 0x040002BD RID: 701
		private SendOrPostCallback ValidateExtensionSettingsOperationCompleted;

		// Token: 0x040002BE RID: 702
		private SendOrPostCallback ListAllSubscriptionsOperationCompleted;

		// Token: 0x040002BF RID: 703
		private SendOrPostCallback ListMySubscriptionsOperationCompleted;

		// Token: 0x040002C0 RID: 704
		private SendOrPostCallback ListReportSubscriptionsOperationCompleted;

		// Token: 0x040002C1 RID: 705
		private SendOrPostCallback ListExtensionsOperationCompleted;

		// Token: 0x040002C2 RID: 706
		private SendOrPostCallback ListEventsOperationCompleted;

		// Token: 0x040002C3 RID: 707
		private SendOrPostCallback FireEventOperationCompleted;

		// Token: 0x040002C4 RID: 708
		private SendOrPostCallback ListTasksOperationCompleted;

		// Token: 0x040002C5 RID: 709
		private SendOrPostCallback ListRolesOperationCompleted;

		// Token: 0x040002C6 RID: 710
		private SendOrPostCallback GetRolePropertiesOperationCompleted;

		// Token: 0x040002C7 RID: 711
		private SendOrPostCallback GetPoliciesOperationCompleted;

		// Token: 0x040002C8 RID: 712
		private SendOrPostCallback SetPoliciesOperationCompleted;

		// Token: 0x040002C9 RID: 713
		private SendOrPostCallback InheritParentSecurityOperationCompleted;

		// Token: 0x040002CA RID: 714
		private SendOrPostCallback GetPermissionsOperationCompleted;

		// Token: 0x040002CB RID: 715
		private SendOrPostCallback CreateModelOperationCompleted;

		// Token: 0x040002CC RID: 716
		private SendOrPostCallback GetModelDefinitionOperationCompleted;

		// Token: 0x040002CD RID: 717
		private SendOrPostCallback SetModelDefinitionOperationCompleted;

		// Token: 0x040002CE RID: 718
		private SendOrPostCallback ListModelPerspectivesOperationCompleted;
	}
}
