using System;
using System.Collections.Generic;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x02000134 RID: 308
	public static class ExtensionMethods
	{
		// Token: 0x06000C5E RID: 3166 RVA: 0x00017BA9 File Offset: 0x00015DA9
		public static SubscriptionSingle ByKey(this DataServiceQuery<Subscription> source, Dictionary<string, object> keys)
		{
			return new SubscriptionSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x00017BC8 File Offset: 0x00015DC8
		public static SubscriptionSingle ByKey(this DataServiceQuery<Subscription> source, Guid id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new SubscriptionSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x00017C09 File Offset: 0x00015E09
		public static CacheRefreshPlanSingle ByKey(this DataServiceQuery<CacheRefreshPlan> source, Dictionary<string, object> keys)
		{
			return new CacheRefreshPlanSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x00017C28 File Offset: 0x00015E28
		public static CacheRefreshPlanSingle ByKey(this DataServiceQuery<CacheRefreshPlan> source, Guid id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new CacheRefreshPlanSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x00017C69 File Offset: 0x00015E69
		public static CatalogItemSingle ByKey(this DataServiceQuery<CatalogItem> source, Dictionary<string, object> keys)
		{
			return new CatalogItemSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x00017C88 File Offset: 0x00015E88
		public static CatalogItemSingle ByKey(this DataServiceQuery<CatalogItem> source, Guid id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new CatalogItemSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x00017CC9 File Offset: 0x00015EC9
		public static ReportParameterDefinitionSingle ByKey(this DataServiceQuery<ReportParameterDefinition> source, Dictionary<string, object> keys)
		{
			return new ReportParameterDefinitionSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x00017CE8 File Offset: 0x00015EE8
		public static ReportParameterDefinitionSingle ByKey(this DataServiceQuery<ReportParameterDefinition> source, string name)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Name", name } };
			return new ReportParameterDefinitionSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x00017D24 File Offset: 0x00015F24
		public static ReportSingle ByKey(this DataServiceQuery<Report> source, Dictionary<string, object> keys)
		{
			return new ReportSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x00017D44 File Offset: 0x00015F44
		public static ReportSingle ByKey(this DataServiceQuery<Report> source, Guid id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new ReportSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x00017D88 File Offset: 0x00015F88
		public static ReportSingle CastToReport(this DataServiceQuerySingle<CatalogItem> source)
		{
			DataServiceQuerySingle<Report> dataServiceQuerySingle = source.CastTo<Report>();
			return new ReportSingle(source.Context, dataServiceQuerySingle.GetPath(null));
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x00017DAE File Offset: 0x00015FAE
		public static LinkedReportSingle ByKey(this DataServiceQuery<LinkedReport> source, Dictionary<string, object> keys)
		{
			return new LinkedReportSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x00017DD0 File Offset: 0x00015FD0
		public static LinkedReportSingle ByKey(this DataServiceQuery<LinkedReport> source, Guid id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new LinkedReportSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x00017E14 File Offset: 0x00016014
		public static LinkedReportSingle CastToLinkedReport(this DataServiceQuerySingle<CatalogItem> source)
		{
			DataServiceQuerySingle<LinkedReport> dataServiceQuerySingle = source.CastTo<LinkedReport>();
			return new LinkedReportSingle(source.Context, dataServiceQuerySingle.GetPath(null));
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x00017E3A File Offset: 0x0001603A
		public static DataSetSingle ByKey(this DataServiceQuery<DataSet> source, Dictionary<string, object> keys)
		{
			return new DataSetSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x00017E5C File Offset: 0x0001605C
		public static DataSetSingle ByKey(this DataServiceQuery<DataSet> source, Guid id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new DataSetSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x00017EA0 File Offset: 0x000160A0
		public static DataSetSingle CastToDataSet(this DataServiceQuerySingle<CatalogItem> source)
		{
			DataServiceQuerySingle<DataSet> dataServiceQuerySingle = source.CastTo<DataSet>();
			return new DataSetSingle(source.Context, dataServiceQuerySingle.GetPath(null));
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x00017EC6 File Offset: 0x000160C6
		public static DataSourceSingle ByKey(this DataServiceQuery<DataSource> source, Dictionary<string, object> keys)
		{
			return new DataSourceSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x00017EE8 File Offset: 0x000160E8
		public static DataSourceSingle ByKey(this DataServiceQuery<DataSource> source, Guid id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new DataSourceSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x00017F2C File Offset: 0x0001612C
		public static DataSourceSingle CastToDataSource(this DataServiceQuerySingle<CatalogItem> source)
		{
			DataServiceQuerySingle<DataSource> dataServiceQuerySingle = source.CastTo<DataSource>();
			return new DataSourceSingle(source.Context, dataServiceQuerySingle.GetPath(null));
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x00017F52 File Offset: 0x00016152
		public static CommentSingle ByKey(this DataServiceQuery<Comment> source, Dictionary<string, object> keys)
		{
			return new CommentSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x00017F74 File Offset: 0x00016174
		public static CommentSingle ByKey(this DataServiceQuery<Comment> source, long id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new CommentSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x00017FB5 File Offset: 0x000161B5
		public static ScheduleSingle ByKey(this DataServiceQuery<Schedule> source, Dictionary<string, object> keys)
		{
			return new ScheduleSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x00017FD4 File Offset: 0x000161D4
		public static ScheduleSingle ByKey(this DataServiceQuery<Schedule> source, Guid id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new ScheduleSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x00018015 File Offset: 0x00016215
		public static ReportServerInfoSingle ByKey(this DataServiceQuery<ReportServerInfo> source, Dictionary<string, object> keys)
		{
			return new ReportServerInfoSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x00018034 File Offset: 0x00016234
		public static ReportServerInfoSingle ByKey(this DataServiceQuery<ReportServerInfo> source, Guid id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new ReportServerInfoSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x00018075 File Offset: 0x00016275
		public static SystemResourceSingle ByKey(this DataServiceQuery<SystemResource> source, Dictionary<string, object> keys)
		{
			return new SystemResourceSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x00018094 File Offset: 0x00016294
		public static SystemResourceSingle ByKey(this DataServiceQuery<SystemResource> source, Guid id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new SystemResourceSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x000180D5 File Offset: 0x000162D5
		public static SystemResourceItemSingle ByKey(this DataServiceQuery<SystemResourceItem> source, Dictionary<string, object> keys)
		{
			return new SystemResourceItemSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x000180F4 File Offset: 0x000162F4
		public static SystemResourceItemSingle ByKey(this DataServiceQuery<SystemResourceItem> source, Guid id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new SystemResourceItemSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x00018135 File Offset: 0x00016335
		public static NotificationSingle ByKey(this DataServiceQuery<Notification> source, Dictionary<string, object> keys)
		{
			return new NotificationSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x00018154 File Offset: 0x00016354
		public static NotificationSingle ByKey(this DataServiceQuery<Notification> source, Guid id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new NotificationSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x00018195 File Offset: 0x00016395
		public static UserSingle ByKey(this DataServiceQuery<User> source, Dictionary<string, object> keys)
		{
			return new UserSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x000181B4 File Offset: 0x000163B4
		public static UserSingle ByKey(this DataServiceQuery<User> source, Guid id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new UserSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x000181F5 File Offset: 0x000163F5
		public static TelemetrySingle ByKey(this DataServiceQuery<Telemetry> source, Dictionary<string, object> keys)
		{
			return new TelemetrySingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x00018214 File Offset: 0x00016414
		public static TelemetrySingle ByKey(this DataServiceQuery<Telemetry> source, Guid id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new TelemetrySingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x00018255 File Offset: 0x00016455
		public static PowerBIUserInfoSingle ByKey(this DataServiceQuery<PowerBIUserInfo> source, Dictionary<string, object> keys)
		{
			return new PowerBIUserInfoSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x00018274 File Offset: 0x00016474
		public static PowerBIUserInfoSingle ByKey(this DataServiceQuery<PowerBIUserInfo> source, Guid id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new PowerBIUserInfoSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x000182B5 File Offset: 0x000164B5
		public static FolderSingle ByKey(this DataServiceQuery<Folder> source, Dictionary<string, object> keys)
		{
			return new FolderSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x000182D4 File Offset: 0x000164D4
		public static FolderSingle ByKey(this DataServiceQuery<Folder> source, Guid id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new FolderSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x00018318 File Offset: 0x00016518
		public static FolderSingle CastToFolder(this DataServiceQuerySingle<CatalogItem> source)
		{
			DataServiceQuerySingle<Folder> dataServiceQuerySingle = source.CastTo<Folder>();
			return new FolderSingle(source.Context, dataServiceQuerySingle.GetPath(null));
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x0001833E File Offset: 0x0001653E
		public static AlertSubscriptionSingle ByKey(this DataServiceQuery<AlertSubscription> source, Dictionary<string, object> keys)
		{
			return new AlertSubscriptionSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x00018360 File Offset: 0x00016560
		public static AlertSubscriptionSingle ByKey(this DataServiceQuery<AlertSubscription> source, long id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new AlertSubscriptionSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x000183A1 File Offset: 0x000165A1
		public static AllowedActionSingle ByKey(this DataServiceQuery<AllowedAction> source, Dictionary<string, object> keys)
		{
			return new AllowedActionSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x000183C0 File Offset: 0x000165C0
		public static AllowedActionSingle ByKey(this DataServiceQuery<AllowedAction> source, string action)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Action", action } };
			return new AllowedActionSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x000183FC File Offset: 0x000165FC
		public static ExcelWorkbookSingle ByKey(this DataServiceQuery<ExcelWorkbook> source, Dictionary<string, object> keys)
		{
			return new ExcelWorkbookSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x0001841C File Offset: 0x0001661C
		public static ExcelWorkbookSingle ByKey(this DataServiceQuery<ExcelWorkbook> source, Guid id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new ExcelWorkbookSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x00018460 File Offset: 0x00016660
		public static ExcelWorkbookSingle CastToExcelWorkbook(this DataServiceQuerySingle<CatalogItem> source)
		{
			DataServiceQuerySingle<ExcelWorkbook> dataServiceQuerySingle = source.CastTo<ExcelWorkbook>();
			return new ExcelWorkbookSingle(source.Context, dataServiceQuerySingle.GetPath(null));
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x00018486 File Offset: 0x00016686
		public static PowerBIReportSingle ByKey(this DataServiceQuery<PowerBIReport> source, Dictionary<string, object> keys)
		{
			return new PowerBIReportSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x000184A8 File Offset: 0x000166A8
		public static PowerBIReportSingle ByKey(this DataServiceQuery<PowerBIReport> source, Guid id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new PowerBIReportSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x000184EC File Offset: 0x000166EC
		public static PowerBIReportSingle CastToPowerBIReport(this DataServiceQuerySingle<CatalogItem> source)
		{
			DataServiceQuerySingle<PowerBIReport> dataServiceQuerySingle = source.CastTo<PowerBIReport>();
			return new PowerBIReportSingle(source.Context, dataServiceQuerySingle.GetPath(null));
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x00018512 File Offset: 0x00016712
		public static DataModelDataSourceSingle ByKey(this DataServiceQuery<DataModelDataSource> source, Dictionary<string, object> keys)
		{
			return new DataModelDataSourceSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x00018534 File Offset: 0x00016734
		public static DataModelDataSourceSingle ByKey(this DataServiceQuery<DataModelDataSource> source, Guid id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new DataModelDataSourceSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x00018575 File Offset: 0x00016775
		public static ResourceSingle ByKey(this DataServiceQuery<Resource> source, Dictionary<string, object> keys)
		{
			return new ResourceSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x00018594 File Offset: 0x00016794
		public static ResourceSingle ByKey(this DataServiceQuery<Resource> source, Guid id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new ResourceSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x000185D8 File Offset: 0x000167D8
		public static ResourceSingle CastToResource(this DataServiceQuerySingle<CatalogItem> source)
		{
			DataServiceQuerySingle<Resource> dataServiceQuerySingle = source.CastTo<Resource>();
			return new ResourceSingle(source.Context, dataServiceQuerySingle.GetPath(null));
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x000185FE File Offset: 0x000167FE
		public static ComponentSingle ByKey(this DataServiceQuery<Component> source, Dictionary<string, object> keys)
		{
			return new ComponentSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x00018620 File Offset: 0x00016820
		public static ComponentSingle ByKey(this DataServiceQuery<Component> source, Guid id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new ComponentSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x00018664 File Offset: 0x00016864
		public static ComponentSingle CastToComponent(this DataServiceQuerySingle<Resource> source)
		{
			DataServiceQuerySingle<Component> dataServiceQuerySingle = source.CastTo<Component>();
			return new ComponentSingle(source.Context, dataServiceQuerySingle.GetPath(null));
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x0001868C File Offset: 0x0001688C
		public static ComponentSingle CastToComponent(this DataServiceQuerySingle<CatalogItem> source)
		{
			DataServiceQuerySingle<Component> dataServiceQuerySingle = source.CastTo<Component>();
			return new ComponentSingle(source.Context, dataServiceQuerySingle.GetPath(null));
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x000186B2 File Offset: 0x000168B2
		public static KpiSingle ByKey(this DataServiceQuery<Kpi> source, Dictionary<string, object> keys)
		{
			return new KpiSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x000186D4 File Offset: 0x000168D4
		public static KpiSingle ByKey(this DataServiceQuery<Kpi> source, Guid id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new KpiSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x00018718 File Offset: 0x00016918
		public static KpiSingle CastToKpi(this DataServiceQuerySingle<CatalogItem> source)
		{
			DataServiceQuerySingle<Kpi> dataServiceQuerySingle = source.CastTo<Kpi>();
			return new KpiSingle(source.Context, dataServiceQuerySingle.GetPath(null));
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x0001873E File Offset: 0x0001693E
		public static HistorySnapshotOptionsSingle ByKey(this DataServiceQuery<HistorySnapshotOptions> source, Dictionary<string, object> keys)
		{
			return new HistorySnapshotOptionsSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x00018760 File Offset: 0x00016960
		public static HistorySnapshotOptionsSingle ByKey(this DataServiceQuery<HistorySnapshotOptions> source, Guid catalogItemId)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "CatalogItemId", catalogItemId } };
			return new HistorySnapshotOptionsSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x000187A1 File Offset: 0x000169A1
		public static ReportHistorySnapshotSingle ByKey(this DataServiceQuery<ReportHistorySnapshot> source, Dictionary<string, object> keys)
		{
			return new ReportHistorySnapshotSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x000187C0 File Offset: 0x000169C0
		public static ReportHistorySnapshotSingle ByKey(this DataServiceQuery<ReportHistorySnapshot> source, string historyId)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "HistoryId", historyId } };
			return new ReportHistorySnapshotSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x000187FC File Offset: 0x000169FC
		public static HistorySnapshotSingle ByKey(this DataServiceQuery<HistorySnapshot> source, Dictionary<string, object> keys)
		{
			return new HistorySnapshotSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x0001881C File Offset: 0x00016A1C
		public static HistorySnapshotSingle ByKey(this DataServiceQuery<HistorySnapshot> source, Guid id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new HistorySnapshotSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x0001885D File Offset: 0x00016A5D
		public static MobileReportSingle ByKey(this DataServiceQuery<MobileReport> source, Dictionary<string, object> keys)
		{
			return new MobileReportSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x0001887C File Offset: 0x00016A7C
		public static MobileReportSingle ByKey(this DataServiceQuery<MobileReport> source, Guid id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new MobileReportSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x000188C0 File Offset: 0x00016AC0
		public static MobileReportSingle CastToMobileReport(this DataServiceQuerySingle<CatalogItem> source)
		{
			DataServiceQuerySingle<MobileReport> dataServiceQuerySingle = source.CastTo<MobileReport>();
			return new MobileReportSingle(source.Context, dataServiceQuerySingle.GetPath(null));
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x000188E6 File Offset: 0x00016AE6
		public static ReportModelSingle ByKey(this DataServiceQuery<ReportModel> source, Dictionary<string, object> keys)
		{
			return new ReportModelSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x00018908 File Offset: 0x00016B08
		public static ReportModelSingle ByKey(this DataServiceQuery<ReportModel> source, Guid id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new ReportModelSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x0001894C File Offset: 0x00016B4C
		public static ReportModelSingle CastToReportModel(this DataServiceQuerySingle<DataSource> source)
		{
			DataServiceQuerySingle<ReportModel> dataServiceQuerySingle = source.CastTo<ReportModel>();
			return new ReportModelSingle(source.Context, dataServiceQuerySingle.GetPath(null));
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x00018974 File Offset: 0x00016B74
		public static ReportModelSingle CastToReportModel(this DataServiceQuerySingle<CatalogItem> source)
		{
			DataServiceQuerySingle<ReportModel> dataServiceQuerySingle = source.CastTo<ReportModel>();
			return new ReportModelSingle(source.Context, dataServiceQuerySingle.GetPath(null));
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x0001899A File Offset: 0x00016B9A
		public static SystemPolicySingle ByKey(this DataServiceQuery<SystemPolicy> source, Dictionary<string, object> keys)
		{
			return new SystemPolicySingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x000189BC File Offset: 0x00016BBC
		public static SystemPolicySingle ByKey(this DataServiceQuery<SystemPolicy> source, Guid id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new SystemPolicySingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x000189FD File Offset: 0x00016BFD
		public static SystemResourcePackageSingle ByKey(this DataServiceQuery<SystemResourcePackage> source, Dictionary<string, object> keys)
		{
			return new SystemResourcePackageSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x00018A1C File Offset: 0x00016C1C
		public static SystemResourcePackageSingle ByKey(this DataServiceQuery<SystemResourcePackage> source, Guid id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new SystemResourcePackageSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x00018A60 File Offset: 0x00016C60
		public static SystemResourcePackageSingle CastToSystemResourcePackage(this DataServiceQuerySingle<SystemResource> source)
		{
			DataServiceQuerySingle<SystemResourcePackage> dataServiceQuerySingle = source.CastTo<SystemResourcePackage>();
			return new SystemResourcePackageSingle(source.Context, dataServiceQuerySingle.GetPath(null));
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x00018A86 File Offset: 0x00016C86
		[OriginalName("GetDependentItems")]
		public static DataServiceQuery<CatalogItem> GetDependentItems(this DataServiceQuerySingle<CatalogItem> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuery<CatalogItem>("Model.GetDependentItems", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x00018AAC File Offset: 0x00016CAC
		[OriginalName("GetDependentItems")]
		public static DataServiceQuery<CatalogItem> GetDependentItems(this DataServiceQuerySingle<Report> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuery<CatalogItem>("Model.GetDependentItems", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x00018AD2 File Offset: 0x00016CD2
		[OriginalName("GetDependentItems")]
		public static DataServiceQuery<CatalogItem> GetDependentItems(this DataServiceQuerySingle<LinkedReport> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuery<CatalogItem>("Model.GetDependentItems", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x00018AF8 File Offset: 0x00016CF8
		[OriginalName("GetDependentItems")]
		public static DataServiceQuery<CatalogItem> GetDependentItems(this DataServiceQuerySingle<DataSet> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuery<CatalogItem>("Model.GetDependentItems", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x00018B1E File Offset: 0x00016D1E
		[OriginalName("GetDependentItems")]
		public static DataServiceQuery<CatalogItem> GetDependentItems(this DataServiceQuerySingle<DataSource> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuery<CatalogItem>("Model.GetDependentItems", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x00018B44 File Offset: 0x00016D44
		[OriginalName("GetDependentItems")]
		public static DataServiceQuery<CatalogItem> GetDependentItems(this DataServiceQuerySingle<Folder> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuery<CatalogItem>("Model.GetDependentItems", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x00018B6A File Offset: 0x00016D6A
		[OriginalName("GetDependentItems")]
		public static DataServiceQuery<CatalogItem> GetDependentItems(this DataServiceQuerySingle<ExcelWorkbook> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuery<CatalogItem>("Model.GetDependentItems", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x00018B90 File Offset: 0x00016D90
		[OriginalName("GetDependentItems")]
		public static DataServiceQuery<CatalogItem> GetDependentItems(this DataServiceQuerySingle<PowerBIReport> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuery<CatalogItem>("Model.GetDependentItems", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x00018BB6 File Offset: 0x00016DB6
		[OriginalName("GetDependentItems")]
		public static DataServiceQuery<CatalogItem> GetDependentItems(this DataServiceQuerySingle<Resource> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuery<CatalogItem>("Model.GetDependentItems", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x00018BDC File Offset: 0x00016DDC
		[OriginalName("GetDependentItems")]
		public static DataServiceQuery<CatalogItem> GetDependentItems(this DataServiceQuerySingle<Kpi> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuery<CatalogItem>("Model.GetDependentItems", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x00018C02 File Offset: 0x00016E02
		[OriginalName("GetDependentItems")]
		public static DataServiceQuery<CatalogItem> GetDependentItems(this DataServiceQuerySingle<MobileReport> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuery<CatalogItem>("Model.GetDependentItems", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x00018C28 File Offset: 0x00016E28
		[OriginalName("SearchItems")]
		public static DataServiceQuery<CatalogItem> SearchItems(this DataServiceQuerySingle<CatalogItem> source, string SearchText)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuery<CatalogItem>("Model.SearchItems", false, new UriOperationParameter[]
			{
				new UriOperationParameter("SearchText", SearchText)
			});
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x00018C5D File Offset: 0x00016E5D
		[OriginalName("SearchItems")]
		public static DataServiceQuery<CatalogItem> SearchItems(this DataServiceQuerySingle<Report> source, string SearchText)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuery<CatalogItem>("Model.SearchItems", false, new UriOperationParameter[]
			{
				new UriOperationParameter("SearchText", SearchText)
			});
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x00018C92 File Offset: 0x00016E92
		[OriginalName("SearchItems")]
		public static DataServiceQuery<CatalogItem> SearchItems(this DataServiceQuerySingle<LinkedReport> source, string SearchText)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuery<CatalogItem>("Model.SearchItems", false, new UriOperationParameter[]
			{
				new UriOperationParameter("SearchText", SearchText)
			});
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x00018CC7 File Offset: 0x00016EC7
		[OriginalName("SearchItems")]
		public static DataServiceQuery<CatalogItem> SearchItems(this DataServiceQuerySingle<DataSet> source, string SearchText)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuery<CatalogItem>("Model.SearchItems", false, new UriOperationParameter[]
			{
				new UriOperationParameter("SearchText", SearchText)
			});
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x00018CFC File Offset: 0x00016EFC
		[OriginalName("SearchItems")]
		public static DataServiceQuery<CatalogItem> SearchItems(this DataServiceQuerySingle<DataSource> source, string SearchText)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuery<CatalogItem>("Model.SearchItems", false, new UriOperationParameter[]
			{
				new UriOperationParameter("SearchText", SearchText)
			});
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x00018D31 File Offset: 0x00016F31
		[OriginalName("SearchItems")]
		public static DataServiceQuery<CatalogItem> SearchItems(this DataServiceQuerySingle<Folder> source, string SearchText)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuery<CatalogItem>("Model.SearchItems", false, new UriOperationParameter[]
			{
				new UriOperationParameter("SearchText", SearchText)
			});
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x00018D66 File Offset: 0x00016F66
		[OriginalName("SearchItems")]
		public static DataServiceQuery<CatalogItem> SearchItems(this DataServiceQuerySingle<ExcelWorkbook> source, string SearchText)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuery<CatalogItem>("Model.SearchItems", false, new UriOperationParameter[]
			{
				new UriOperationParameter("SearchText", SearchText)
			});
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x00018D9B File Offset: 0x00016F9B
		[OriginalName("SearchItems")]
		public static DataServiceQuery<CatalogItem> SearchItems(this DataServiceQuerySingle<PowerBIReport> source, string SearchText)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuery<CatalogItem>("Model.SearchItems", false, new UriOperationParameter[]
			{
				new UriOperationParameter("SearchText", SearchText)
			});
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x00018DD0 File Offset: 0x00016FD0
		[OriginalName("SearchItems")]
		public static DataServiceQuery<CatalogItem> SearchItems(this DataServiceQuerySingle<Resource> source, string SearchText)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuery<CatalogItem>("Model.SearchItems", false, new UriOperationParameter[]
			{
				new UriOperationParameter("SearchText", SearchText)
			});
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x00018E05 File Offset: 0x00017005
		[OriginalName("SearchItems")]
		public static DataServiceQuery<CatalogItem> SearchItems(this DataServiceQuerySingle<Kpi> source, string SearchText)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuery<CatalogItem>("Model.SearchItems", false, new UriOperationParameter[]
			{
				new UriOperationParameter("SearchText", SearchText)
			});
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x00018E3A File Offset: 0x0001703A
		[OriginalName("SearchItems")]
		public static DataServiceQuery<CatalogItem> SearchItems(this DataServiceQuerySingle<MobileReport> source, string SearchText)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuery<CatalogItem>("Model.SearchItems", false, new UriOperationParameter[]
			{
				new UriOperationParameter("SearchText", SearchText)
			});
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x00018E6F File Offset: 0x0001706F
		[OriginalName("GetRoles")]
		public static DataServiceQuery<Role> GetRoles(this DataServiceQuerySingle<CatalogItem> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuery<Role>("Model.GetRoles", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x00018E95 File Offset: 0x00017095
		[OriginalName("GetRoles")]
		public static DataServiceQuery<Role> GetRoles(this DataServiceQuerySingle<Report> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuery<Role>("Model.GetRoles", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x00018EBB File Offset: 0x000170BB
		[OriginalName("GetRoles")]
		public static DataServiceQuery<Role> GetRoles(this DataServiceQuerySingle<LinkedReport> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuery<Role>("Model.GetRoles", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x00018EE1 File Offset: 0x000170E1
		[OriginalName("GetRoles")]
		public static DataServiceQuery<Role> GetRoles(this DataServiceQuerySingle<DataSet> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuery<Role>("Model.GetRoles", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x00018F07 File Offset: 0x00017107
		[OriginalName("GetRoles")]
		public static DataServiceQuery<Role> GetRoles(this DataServiceQuerySingle<DataSource> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuery<Role>("Model.GetRoles", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x00018F2D File Offset: 0x0001712D
		[OriginalName("GetRoles")]
		public static DataServiceQuery<Role> GetRoles(this DataServiceQuerySingle<Folder> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuery<Role>("Model.GetRoles", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x00018F53 File Offset: 0x00017153
		[OriginalName("GetRoles")]
		public static DataServiceQuery<Role> GetRoles(this DataServiceQuerySingle<ExcelWorkbook> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuery<Role>("Model.GetRoles", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x00018F79 File Offset: 0x00017179
		[OriginalName("GetRoles")]
		public static DataServiceQuery<Role> GetRoles(this DataServiceQuerySingle<PowerBIReport> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuery<Role>("Model.GetRoles", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x00018F9F File Offset: 0x0001719F
		[OriginalName("GetRoles")]
		public static DataServiceQuery<Role> GetRoles(this DataServiceQuerySingle<Resource> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuery<Role>("Model.GetRoles", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x00018FC5 File Offset: 0x000171C5
		[OriginalName("GetRoles")]
		public static DataServiceQuery<Role> GetRoles(this DataServiceQuerySingle<Kpi> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuery<Role>("Model.GetRoles", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x00018FEB File Offset: 0x000171EB
		[OriginalName("GetRoles")]
		public static DataServiceQuery<Role> GetRoles(this DataServiceQuerySingle<MobileReport> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuery<Role>("Model.GetRoles", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x00019011 File Offset: 0x00017211
		[OriginalName("GetPolicies")]
		public static DataServiceQuery<ItemPolicy> GetPolicies(this DataServiceQuerySingle<CatalogItem> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuery<ItemPolicy>("Model.GetPolicies", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x00019037 File Offset: 0x00017237
		[OriginalName("GetPolicies")]
		public static DataServiceQuery<ItemPolicy> GetPolicies(this DataServiceQuerySingle<Report> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuery<ItemPolicy>("Model.GetPolicies", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x0001905D File Offset: 0x0001725D
		[OriginalName("GetPolicies")]
		public static DataServiceQuery<ItemPolicy> GetPolicies(this DataServiceQuerySingle<LinkedReport> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuery<ItemPolicy>("Model.GetPolicies", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x00019083 File Offset: 0x00017283
		[OriginalName("GetPolicies")]
		public static DataServiceQuery<ItemPolicy> GetPolicies(this DataServiceQuerySingle<DataSet> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuery<ItemPolicy>("Model.GetPolicies", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x000190A9 File Offset: 0x000172A9
		[OriginalName("GetPolicies")]
		public static DataServiceQuery<ItemPolicy> GetPolicies(this DataServiceQuerySingle<DataSource> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuery<ItemPolicy>("Model.GetPolicies", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x000190CF File Offset: 0x000172CF
		[OriginalName("GetPolicies")]
		public static DataServiceQuery<ItemPolicy> GetPolicies(this DataServiceQuerySingle<Folder> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuery<ItemPolicy>("Model.GetPolicies", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x000190F5 File Offset: 0x000172F5
		[OriginalName("GetPolicies")]
		public static DataServiceQuery<ItemPolicy> GetPolicies(this DataServiceQuerySingle<ExcelWorkbook> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuery<ItemPolicy>("Model.GetPolicies", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x0001911B File Offset: 0x0001731B
		[OriginalName("GetPolicies")]
		public static DataServiceQuery<ItemPolicy> GetPolicies(this DataServiceQuerySingle<PowerBIReport> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuery<ItemPolicy>("Model.GetPolicies", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x00019141 File Offset: 0x00017341
		[OriginalName("GetPolicies")]
		public static DataServiceQuery<ItemPolicy> GetPolicies(this DataServiceQuerySingle<Resource> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuery<ItemPolicy>("Model.GetPolicies", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x00019167 File Offset: 0x00017367
		[OriginalName("GetPolicies")]
		public static DataServiceQuery<ItemPolicy> GetPolicies(this DataServiceQuerySingle<Kpi> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuery<ItemPolicy>("Model.GetPolicies", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x0001918D File Offset: 0x0001738D
		[OriginalName("GetPolicies")]
		public static DataServiceQuery<ItemPolicy> GetPolicies(this DataServiceQuerySingle<MobileReport> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuery<ItemPolicy>("Model.GetPolicies", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x000191B3 File Offset: 0x000173B3
		[OriginalName("GetHistoryOptions")]
		public static DataServiceQuerySingle<ItemHistoryOptions> GetHistoryOptions(this DataServiceQuerySingle<Report> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuerySingle<ItemHistoryOptions>("Model.GetHistoryOptions", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x000191D9 File Offset: 0x000173D9
		[OriginalName("GetReportHistorySnapshotsOptions")]
		public static DataServiceQuerySingle<ReportHistorySnapshotsOptions> GetReportHistorySnapshotsOptions(this DataServiceQuerySingle<Report> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuerySingle<ReportHistorySnapshotsOptions>("Model.GetReportHistorySnapshotsOptions", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x000191FF File Offset: 0x000173FF
		[OriginalName("GetReportHistorySnapshotsOptions")]
		public static DataServiceQuerySingle<ReportHistorySnapshotsOptions> GetReportHistorySnapshotsOptions(this DataServiceQuerySingle<LinkedReport> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuerySingle<ReportHistorySnapshotsOptions>("Model.GetReportHistorySnapshotsOptions", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x00019225 File Offset: 0x00017425
		[OriginalName("GetSchema")]
		public static DataServiceQuerySingle<DataSetSchema> GetSchema(this DataServiceQuerySingle<DataSet> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuerySingle<DataSetSchema>("Model.GetSchema", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x0001924B File Offset: 0x0001744B
		[OriginalName("GetTable")]
		public static DataServiceQuerySingle<string> GetTable(this DataServiceQuerySingle<DataSet> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuerySingle<string>("Model.GetTable", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x00019271 File Offset: 0x00017471
		[OriginalName("GetTable")]
		public static DataServiceQuerySingle<string> GetTable(this DataServiceQuerySingle<DataSet> source, int maxRows)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuerySingle<string>("Model.GetTable", false, new UriOperationParameter[]
			{
				new UriOperationParameter("maxRows", maxRows)
			});
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x000192AB File Offset: 0x000174AB
		[OriginalName("GetCacheOptions")]
		public static DataServiceQuerySingle<CacheOptions> GetCacheOptions(this DataServiceQuerySingle<Report> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuerySingle<CacheOptions>("Model.GetCacheOptions", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x000192D1 File Offset: 0x000174D1
		[OriginalName("GetCacheOptions")]
		public static DataServiceQuerySingle<CacheOptions> GetCacheOptions(this DataServiceQuerySingle<LinkedReport> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuerySingle<CacheOptions>("Model.GetCacheOptions", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x000192F7 File Offset: 0x000174F7
		[OriginalName("GetCacheOptions")]
		public static DataServiceQuerySingle<CacheOptions> GetCacheOptions(this DataServiceQuerySingle<DataSet> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuerySingle<CacheOptions>("Model.GetCacheOptions", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x0001931D File Offset: 0x0001751D
		[OriginalName("DeliveryExtensions")]
		public static DataServiceQuery<Extension> DeliveryExtensions(this DataServiceQuerySingle<ReportServerInfo> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuery<Extension>("Model.DeliveryExtensions", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x00019343 File Offset: 0x00017543
		[OriginalName("DeliveryUIExtensions")]
		public static DataServiceQuery<Extension> DeliveryUIExtensions(this DataServiceQuerySingle<ReportServerInfo> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuery<Extension>("Model.DeliveryUIExtensions", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x00019369 File Offset: 0x00017569
		[OriginalName("DataExtensions")]
		public static DataServiceQuery<Extension> DataExtensions(this DataServiceQuerySingle<ReportServerInfo> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuery<Extension>("Model.DataExtensions", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x0001938F File Offset: 0x0001758F
		[OriginalName("RenderingExtensions")]
		public static DataServiceQuery<Extension> RenderingExtensions(this DataServiceQuerySingle<ReportServerInfo> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuery<Extension>("Model.RenderingExtensions", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x000193B5 File Offset: 0x000175B5
		[OriginalName("ExtensionParameters")]
		public static DataServiceQuery<ExtensionParameter> ExtensionParameters(this DataServiceQuerySingle<ReportServerInfo> source, string ExtensionName)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuery<ExtensionParameter>("Model.ExtensionParameters", false, new UriOperationParameter[]
			{
				new UriOperationParameter("ExtensionName", ExtensionName)
			});
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x000193EA File Offset: 0x000175EA
		[OriginalName("Policies")]
		public static DataServiceQuery<Policy> Policies(this DataServiceQuerySingle<ReportServerInfo> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuery<Policy>("Model.Policies", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x00019410 File Offset: 0x00017610
		[OriginalName("Roles")]
		public static DataServiceQuery<Role> Roles(this DataServiceQuerySingle<ReportServerInfo> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuery<Role>("Model.Roles", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x00019436 File Offset: 0x00017636
		[OriginalName("RestrictedSettings")]
		public static DataServiceQuery<Property> RestrictedSettings(this DataServiceQuerySingle<ReportServerInfo> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuery<Property>("Model.RestrictedSettings", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x0001945C File Offset: 0x0001765C
		[OriginalName("Settings")]
		public static DataServiceQuery<Property> Settings(this DataServiceQuerySingle<ReportServerInfo> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuery<Property>("Model.Settings", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x00019482 File Offset: 0x00017682
		[OriginalName("ServerProductInfo")]
		public static DataServiceQuery<Property> ServerProductInfo(this DataServiceQuerySingle<ReportServerInfo> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuery<Property>("Model.ServerProductInfo", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x000194A8 File Offset: 0x000176A8
		[OriginalName("SiteName")]
		public static DataServiceQuerySingle<string> SiteName(this DataServiceQuerySingle<ReportServerInfo> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuerySingle<string>("Model.SiteName", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x000194CE File Offset: 0x000176CE
		[OriginalName("GetWebAppUrl")]
		public static DataServiceQuerySingle<string> GetWebAppUrl(this DataServiceQuerySingle<ReportServerInfo> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuerySingle<string>("Model.GetWebAppUrl", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x000194F4 File Offset: 0x000176F4
		[OriginalName("GetVirtualDirectory")]
		public static DataServiceQuerySingle<string> GetVirtualDirectory(this DataServiceQuerySingle<ReportServerInfo> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuerySingle<string>("Model.GetVirtualDirectory", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x0001951A File Offset: 0x0001771A
		[OriginalName("Enable")]
		public static DataServiceActionQuery Enable(this DataServiceQuerySingle<Subscription> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuery(source.Context, source.AppendRequestUri("Model.Enable"), Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x0001954A File Offset: 0x0001774A
		[OriginalName("Disable")]
		public static DataServiceActionQuery Disable(this DataServiceQuerySingle<Subscription> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuery(source.Context, source.AppendRequestUri("Model.Disable"), Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x0001957A File Offset: 0x0001777A
		[OriginalName("AddToFavorites")]
		public static DataServiceActionQuerySingle<bool> AddToFavorites(this DataServiceQuerySingle<CatalogItem> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<bool>(source.Context, source.AppendRequestUri("Model.AddToFavorites"), Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x000195AA File Offset: 0x000177AA
		[OriginalName("AddToFavorites")]
		public static DataServiceActionQuerySingle<bool> AddToFavorites(this DataServiceQuerySingle<Report> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<bool>(source.Context, source.AppendRequestUri("Model.AddToFavorites"), Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x000195DA File Offset: 0x000177DA
		[OriginalName("AddToFavorites")]
		public static DataServiceActionQuerySingle<bool> AddToFavorites(this DataServiceQuerySingle<LinkedReport> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<bool>(source.Context, source.AppendRequestUri("Model.AddToFavorites"), Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x0001960A File Offset: 0x0001780A
		[OriginalName("AddToFavorites")]
		public static DataServiceActionQuerySingle<bool> AddToFavorites(this DataServiceQuerySingle<DataSet> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<bool>(source.Context, source.AppendRequestUri("Model.AddToFavorites"), Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x0001963A File Offset: 0x0001783A
		[OriginalName("AddToFavorites")]
		public static DataServiceActionQuerySingle<bool> AddToFavorites(this DataServiceQuerySingle<DataSource> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<bool>(source.Context, source.AppendRequestUri("Model.AddToFavorites"), Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x0001966A File Offset: 0x0001786A
		[OriginalName("AddToFavorites")]
		public static DataServiceActionQuerySingle<bool> AddToFavorites(this DataServiceQuerySingle<Folder> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<bool>(source.Context, source.AppendRequestUri("Model.AddToFavorites"), Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x0001969A File Offset: 0x0001789A
		[OriginalName("AddToFavorites")]
		public static DataServiceActionQuerySingle<bool> AddToFavorites(this DataServiceQuerySingle<ExcelWorkbook> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<bool>(source.Context, source.AppendRequestUri("Model.AddToFavorites"), Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x000196CA File Offset: 0x000178CA
		[OriginalName("AddToFavorites")]
		public static DataServiceActionQuerySingle<bool> AddToFavorites(this DataServiceQuerySingle<PowerBIReport> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<bool>(source.Context, source.AppendRequestUri("Model.AddToFavorites"), Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x000196FA File Offset: 0x000178FA
		[OriginalName("AddToFavorites")]
		public static DataServiceActionQuerySingle<bool> AddToFavorites(this DataServiceQuerySingle<Resource> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<bool>(source.Context, source.AppendRequestUri("Model.AddToFavorites"), Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x0001972A File Offset: 0x0001792A
		[OriginalName("AddToFavorites")]
		public static DataServiceActionQuerySingle<bool> AddToFavorites(this DataServiceQuerySingle<Kpi> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<bool>(source.Context, source.AppendRequestUri("Model.AddToFavorites"), Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x0001975A File Offset: 0x0001795A
		[OriginalName("AddToFavorites")]
		public static DataServiceActionQuerySingle<bool> AddToFavorites(this DataServiceQuerySingle<MobileReport> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<bool>(source.Context, source.AppendRequestUri("Model.AddToFavorites"), Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x0001978A File Offset: 0x0001798A
		[OriginalName("RemoveFromFavorites")]
		public static DataServiceActionQuerySingle<bool> RemoveFromFavorites(this DataServiceQuerySingle<CatalogItem> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<bool>(source.Context, source.AppendRequestUri("Model.RemoveFromFavorites"), Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x000197BA File Offset: 0x000179BA
		[OriginalName("RemoveFromFavorites")]
		public static DataServiceActionQuerySingle<bool> RemoveFromFavorites(this DataServiceQuerySingle<Report> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<bool>(source.Context, source.AppendRequestUri("Model.RemoveFromFavorites"), Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x000197EA File Offset: 0x000179EA
		[OriginalName("RemoveFromFavorites")]
		public static DataServiceActionQuerySingle<bool> RemoveFromFavorites(this DataServiceQuerySingle<LinkedReport> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<bool>(source.Context, source.AppendRequestUri("Model.RemoveFromFavorites"), Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x0001981A File Offset: 0x00017A1A
		[OriginalName("RemoveFromFavorites")]
		public static DataServiceActionQuerySingle<bool> RemoveFromFavorites(this DataServiceQuerySingle<DataSet> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<bool>(source.Context, source.AppendRequestUri("Model.RemoveFromFavorites"), Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x0001984A File Offset: 0x00017A4A
		[OriginalName("RemoveFromFavorites")]
		public static DataServiceActionQuerySingle<bool> RemoveFromFavorites(this DataServiceQuerySingle<DataSource> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<bool>(source.Context, source.AppendRequestUri("Model.RemoveFromFavorites"), Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x0001987A File Offset: 0x00017A7A
		[OriginalName("RemoveFromFavorites")]
		public static DataServiceActionQuerySingle<bool> RemoveFromFavorites(this DataServiceQuerySingle<Folder> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<bool>(source.Context, source.AppendRequestUri("Model.RemoveFromFavorites"), Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x000198AA File Offset: 0x00017AAA
		[OriginalName("RemoveFromFavorites")]
		public static DataServiceActionQuerySingle<bool> RemoveFromFavorites(this DataServiceQuerySingle<ExcelWorkbook> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<bool>(source.Context, source.AppendRequestUri("Model.RemoveFromFavorites"), Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x000198DA File Offset: 0x00017ADA
		[OriginalName("RemoveFromFavorites")]
		public static DataServiceActionQuerySingle<bool> RemoveFromFavorites(this DataServiceQuerySingle<PowerBIReport> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<bool>(source.Context, source.AppendRequestUri("Model.RemoveFromFavorites"), Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x0001990A File Offset: 0x00017B0A
		[OriginalName("RemoveFromFavorites")]
		public static DataServiceActionQuerySingle<bool> RemoveFromFavorites(this DataServiceQuerySingle<Resource> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<bool>(source.Context, source.AppendRequestUri("Model.RemoveFromFavorites"), Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x0001993A File Offset: 0x00017B3A
		[OriginalName("RemoveFromFavorites")]
		public static DataServiceActionQuerySingle<bool> RemoveFromFavorites(this DataServiceQuerySingle<Kpi> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<bool>(source.Context, source.AppendRequestUri("Model.RemoveFromFavorites"), Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x0001996A File Offset: 0x00017B6A
		[OriginalName("RemoveFromFavorites")]
		public static DataServiceActionQuerySingle<bool> RemoveFromFavorites(this DataServiceQuerySingle<MobileReport> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<bool>(source.Context, source.AppendRequestUri("Model.RemoveFromFavorites"), Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x0001999A File Offset: 0x00017B9A
		[OriginalName("GetProperties")]
		public static DataServiceActionQuery<Property> GetProperties(this DataServiceQuerySingle<CatalogItem> source, ICollection<Property> RequestedProperties)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuery<Property>(source.Context, source.AppendRequestUri("Model.GetProperties"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("RequestedProperties", RequestedProperties)
			});
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x000199D9 File Offset: 0x00017BD9
		[OriginalName("GetProperties")]
		public static DataServiceActionQuery<Property> GetProperties(this DataServiceQuerySingle<Report> source, ICollection<Property> RequestedProperties)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuery<Property>(source.Context, source.AppendRequestUri("Model.GetProperties"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("RequestedProperties", RequestedProperties)
			});
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x00019A18 File Offset: 0x00017C18
		[OriginalName("GetProperties")]
		public static DataServiceActionQuery<Property> GetProperties(this DataServiceQuerySingle<LinkedReport> source, ICollection<Property> RequestedProperties)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuery<Property>(source.Context, source.AppendRequestUri("Model.GetProperties"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("RequestedProperties", RequestedProperties)
			});
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x00019A57 File Offset: 0x00017C57
		[OriginalName("GetProperties")]
		public static DataServiceActionQuery<Property> GetProperties(this DataServiceQuerySingle<DataSet> source, ICollection<Property> RequestedProperties)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuery<Property>(source.Context, source.AppendRequestUri("Model.GetProperties"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("RequestedProperties", RequestedProperties)
			});
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x00019A96 File Offset: 0x00017C96
		[OriginalName("GetProperties")]
		public static DataServiceActionQuery<Property> GetProperties(this DataServiceQuerySingle<DataSource> source, ICollection<Property> RequestedProperties)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuery<Property>(source.Context, source.AppendRequestUri("Model.GetProperties"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("RequestedProperties", RequestedProperties)
			});
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x00019AD5 File Offset: 0x00017CD5
		[OriginalName("GetProperties")]
		public static DataServiceActionQuery<Property> GetProperties(this DataServiceQuerySingle<Folder> source, ICollection<Property> RequestedProperties)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuery<Property>(source.Context, source.AppendRequestUri("Model.GetProperties"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("RequestedProperties", RequestedProperties)
			});
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x00019B14 File Offset: 0x00017D14
		[OriginalName("GetProperties")]
		public static DataServiceActionQuery<Property> GetProperties(this DataServiceQuerySingle<ExcelWorkbook> source, ICollection<Property> RequestedProperties)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuery<Property>(source.Context, source.AppendRequestUri("Model.GetProperties"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("RequestedProperties", RequestedProperties)
			});
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x00019B53 File Offset: 0x00017D53
		[OriginalName("GetProperties")]
		public static DataServiceActionQuery<Property> GetProperties(this DataServiceQuerySingle<PowerBIReport> source, ICollection<Property> RequestedProperties)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuery<Property>(source.Context, source.AppendRequestUri("Model.GetProperties"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("RequestedProperties", RequestedProperties)
			});
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x00019B92 File Offset: 0x00017D92
		[OriginalName("GetProperties")]
		public static DataServiceActionQuery<Property> GetProperties(this DataServiceQuerySingle<Resource> source, ICollection<Property> RequestedProperties)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuery<Property>(source.Context, source.AppendRequestUri("Model.GetProperties"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("RequestedProperties", RequestedProperties)
			});
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x00019BD1 File Offset: 0x00017DD1
		[OriginalName("GetProperties")]
		public static DataServiceActionQuery<Property> GetProperties(this DataServiceQuerySingle<Kpi> source, ICollection<Property> RequestedProperties)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuery<Property>(source.Context, source.AppendRequestUri("Model.GetProperties"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("RequestedProperties", RequestedProperties)
			});
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x00019C10 File Offset: 0x00017E10
		[OriginalName("GetProperties")]
		public static DataServiceActionQuery<Property> GetProperties(this DataServiceQuerySingle<MobileReport> source, ICollection<Property> RequestedProperties)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuery<Property>(source.Context, source.AppendRequestUri("Model.GetProperties"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("RequestedProperties", RequestedProperties)
			});
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x00019C4F File Offset: 0x00017E4F
		[OriginalName("SetProperties")]
		public static DataServiceActionQuerySingle<bool> SetProperties(this DataServiceQuerySingle<CatalogItem> source, ICollection<Property> Properties)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<bool>(source.Context, source.AppendRequestUri("Model.SetProperties"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("Properties", Properties)
			});
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x00019C8E File Offset: 0x00017E8E
		[OriginalName("SetProperties")]
		public static DataServiceActionQuerySingle<bool> SetProperties(this DataServiceQuerySingle<Report> source, ICollection<Property> Properties)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<bool>(source.Context, source.AppendRequestUri("Model.SetProperties"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("Properties", Properties)
			});
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x00019CCD File Offset: 0x00017ECD
		[OriginalName("SetProperties")]
		public static DataServiceActionQuerySingle<bool> SetProperties(this DataServiceQuerySingle<LinkedReport> source, ICollection<Property> Properties)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<bool>(source.Context, source.AppendRequestUri("Model.SetProperties"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("Properties", Properties)
			});
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x00019D0C File Offset: 0x00017F0C
		[OriginalName("SetProperties")]
		public static DataServiceActionQuerySingle<bool> SetProperties(this DataServiceQuerySingle<DataSet> source, ICollection<Property> Properties)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<bool>(source.Context, source.AppendRequestUri("Model.SetProperties"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("Properties", Properties)
			});
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x00019D4B File Offset: 0x00017F4B
		[OriginalName("SetProperties")]
		public static DataServiceActionQuerySingle<bool> SetProperties(this DataServiceQuerySingle<DataSource> source, ICollection<Property> Properties)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<bool>(source.Context, source.AppendRequestUri("Model.SetProperties"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("Properties", Properties)
			});
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x00019D8A File Offset: 0x00017F8A
		[OriginalName("SetProperties")]
		public static DataServiceActionQuerySingle<bool> SetProperties(this DataServiceQuerySingle<Folder> source, ICollection<Property> Properties)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<bool>(source.Context, source.AppendRequestUri("Model.SetProperties"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("Properties", Properties)
			});
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x00019DC9 File Offset: 0x00017FC9
		[OriginalName("SetProperties")]
		public static DataServiceActionQuerySingle<bool> SetProperties(this DataServiceQuerySingle<ExcelWorkbook> source, ICollection<Property> Properties)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<bool>(source.Context, source.AppendRequestUri("Model.SetProperties"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("Properties", Properties)
			});
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x00019E08 File Offset: 0x00018008
		[OriginalName("SetProperties")]
		public static DataServiceActionQuerySingle<bool> SetProperties(this DataServiceQuerySingle<PowerBIReport> source, ICollection<Property> Properties)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<bool>(source.Context, source.AppendRequestUri("Model.SetProperties"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("Properties", Properties)
			});
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x00019E47 File Offset: 0x00018047
		[OriginalName("SetProperties")]
		public static DataServiceActionQuerySingle<bool> SetProperties(this DataServiceQuerySingle<Resource> source, ICollection<Property> Properties)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<bool>(source.Context, source.AppendRequestUri("Model.SetProperties"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("Properties", Properties)
			});
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x00019E86 File Offset: 0x00018086
		[OriginalName("SetProperties")]
		public static DataServiceActionQuerySingle<bool> SetProperties(this DataServiceQuerySingle<Kpi> source, ICollection<Property> Properties)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<bool>(source.Context, source.AppendRequestUri("Model.SetProperties"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("Properties", Properties)
			});
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x00019EC5 File Offset: 0x000180C5
		[OriginalName("SetProperties")]
		public static DataServiceActionQuerySingle<bool> SetProperties(this DataServiceQuerySingle<MobileReport> source, ICollection<Property> Properties)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<bool>(source.Context, source.AppendRequestUri("Model.SetProperties"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("Properties", Properties)
			});
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x00019F04 File Offset: 0x00018104
		[OriginalName("DeleteItems")]
		public static DataServiceActionQuerySingle<BulkOperationsResult> DeleteItems(this DataServiceQuery<CatalogItem> source, ICollection<string> CatalogItemPaths)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<BulkOperationsResult>(source.Context, source.AppendRequestUri("Model.DeleteItems"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("CatalogItemPaths", CatalogItemPaths)
			});
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x00019F43 File Offset: 0x00018143
		[OriginalName("DeleteItems")]
		public static DataServiceActionQuerySingle<BulkOperationsResult> DeleteItems(this DataServiceQuery<Report> source, ICollection<string> CatalogItemPaths)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<BulkOperationsResult>(source.Context, source.AppendRequestUri("Model.DeleteItems"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("CatalogItemPaths", CatalogItemPaths)
			});
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x00019F82 File Offset: 0x00018182
		[OriginalName("DeleteItems")]
		public static DataServiceActionQuerySingle<BulkOperationsResult> DeleteItems(this DataServiceQuery<LinkedReport> source, ICollection<string> CatalogItemPaths)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<BulkOperationsResult>(source.Context, source.AppendRequestUri("Model.DeleteItems"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("CatalogItemPaths", CatalogItemPaths)
			});
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x00019FC1 File Offset: 0x000181C1
		[OriginalName("DeleteItems")]
		public static DataServiceActionQuerySingle<BulkOperationsResult> DeleteItems(this DataServiceQuery<DataSet> source, ICollection<string> CatalogItemPaths)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<BulkOperationsResult>(source.Context, source.AppendRequestUri("Model.DeleteItems"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("CatalogItemPaths", CatalogItemPaths)
			});
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x0001A000 File Offset: 0x00018200
		[OriginalName("DeleteItems")]
		public static DataServiceActionQuerySingle<BulkOperationsResult> DeleteItems(this DataServiceQuery<DataSource> source, ICollection<string> CatalogItemPaths)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<BulkOperationsResult>(source.Context, source.AppendRequestUri("Model.DeleteItems"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("CatalogItemPaths", CatalogItemPaths)
			});
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x0001A03F File Offset: 0x0001823F
		[OriginalName("DeleteItems")]
		public static DataServiceActionQuerySingle<BulkOperationsResult> DeleteItems(this DataServiceQuery<Folder> source, ICollection<string> CatalogItemPaths)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<BulkOperationsResult>(source.Context, source.AppendRequestUri("Model.DeleteItems"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("CatalogItemPaths", CatalogItemPaths)
			});
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x0001A07E File Offset: 0x0001827E
		[OriginalName("DeleteItems")]
		public static DataServiceActionQuerySingle<BulkOperationsResult> DeleteItems(this DataServiceQuery<ExcelWorkbook> source, ICollection<string> CatalogItemPaths)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<BulkOperationsResult>(source.Context, source.AppendRequestUri("Model.DeleteItems"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("CatalogItemPaths", CatalogItemPaths)
			});
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x0001A0BD File Offset: 0x000182BD
		[OriginalName("DeleteItems")]
		public static DataServiceActionQuerySingle<BulkOperationsResult> DeleteItems(this DataServiceQuery<PowerBIReport> source, ICollection<string> CatalogItemPaths)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<BulkOperationsResult>(source.Context, source.AppendRequestUri("Model.DeleteItems"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("CatalogItemPaths", CatalogItemPaths)
			});
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x0001A0FC File Offset: 0x000182FC
		[OriginalName("DeleteItems")]
		public static DataServiceActionQuerySingle<BulkOperationsResult> DeleteItems(this DataServiceQuery<Resource> source, ICollection<string> CatalogItemPaths)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<BulkOperationsResult>(source.Context, source.AppendRequestUri("Model.DeleteItems"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("CatalogItemPaths", CatalogItemPaths)
			});
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x0001A13B File Offset: 0x0001833B
		[OriginalName("DeleteItems")]
		public static DataServiceActionQuerySingle<BulkOperationsResult> DeleteItems(this DataServiceQuery<Kpi> source, ICollection<string> CatalogItemPaths)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<BulkOperationsResult>(source.Context, source.AppendRequestUri("Model.DeleteItems"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("CatalogItemPaths", CatalogItemPaths)
			});
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x0001A17A File Offset: 0x0001837A
		[OriginalName("DeleteItems")]
		public static DataServiceActionQuerySingle<BulkOperationsResult> DeleteItems(this DataServiceQuery<MobileReport> source, ICollection<string> CatalogItemPaths)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<BulkOperationsResult>(source.Context, source.AppendRequestUri("Model.DeleteItems"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("CatalogItemPaths", CatalogItemPaths)
			});
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x0001A1BC File Offset: 0x000183BC
		[OriginalName("MoveItems")]
		public static DataServiceActionQuerySingle<BulkOperationsResult> MoveItems(this DataServiceQuery<CatalogItem> source, string TargetPath, ICollection<string> CatalogItemPaths)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<BulkOperationsResult>(source.Context, source.AppendRequestUri("Model.MoveItems"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("TargetPath", TargetPath),
				new BodyOperationParameter("CatalogItemPaths", CatalogItemPaths)
			});
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x0001A214 File Offset: 0x00018414
		[OriginalName("MoveItems")]
		public static DataServiceActionQuerySingle<BulkOperationsResult> MoveItems(this DataServiceQuery<Report> source, string TargetPath, ICollection<string> CatalogItemPaths)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<BulkOperationsResult>(source.Context, source.AppendRequestUri("Model.MoveItems"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("TargetPath", TargetPath),
				new BodyOperationParameter("CatalogItemPaths", CatalogItemPaths)
			});
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x0001A26C File Offset: 0x0001846C
		[OriginalName("MoveItems")]
		public static DataServiceActionQuerySingle<BulkOperationsResult> MoveItems(this DataServiceQuery<LinkedReport> source, string TargetPath, ICollection<string> CatalogItemPaths)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<BulkOperationsResult>(source.Context, source.AppendRequestUri("Model.MoveItems"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("TargetPath", TargetPath),
				new BodyOperationParameter("CatalogItemPaths", CatalogItemPaths)
			});
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x0001A2C4 File Offset: 0x000184C4
		[OriginalName("MoveItems")]
		public static DataServiceActionQuerySingle<BulkOperationsResult> MoveItems(this DataServiceQuery<DataSet> source, string TargetPath, ICollection<string> CatalogItemPaths)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<BulkOperationsResult>(source.Context, source.AppendRequestUri("Model.MoveItems"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("TargetPath", TargetPath),
				new BodyOperationParameter("CatalogItemPaths", CatalogItemPaths)
			});
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x0001A31C File Offset: 0x0001851C
		[OriginalName("MoveItems")]
		public static DataServiceActionQuerySingle<BulkOperationsResult> MoveItems(this DataServiceQuery<DataSource> source, string TargetPath, ICollection<string> CatalogItemPaths)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<BulkOperationsResult>(source.Context, source.AppendRequestUri("Model.MoveItems"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("TargetPath", TargetPath),
				new BodyOperationParameter("CatalogItemPaths", CatalogItemPaths)
			});
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x0001A374 File Offset: 0x00018574
		[OriginalName("MoveItems")]
		public static DataServiceActionQuerySingle<BulkOperationsResult> MoveItems(this DataServiceQuery<Folder> source, string TargetPath, ICollection<string> CatalogItemPaths)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<BulkOperationsResult>(source.Context, source.AppendRequestUri("Model.MoveItems"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("TargetPath", TargetPath),
				new BodyOperationParameter("CatalogItemPaths", CatalogItemPaths)
			});
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x0001A3CC File Offset: 0x000185CC
		[OriginalName("MoveItems")]
		public static DataServiceActionQuerySingle<BulkOperationsResult> MoveItems(this DataServiceQuery<ExcelWorkbook> source, string TargetPath, ICollection<string> CatalogItemPaths)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<BulkOperationsResult>(source.Context, source.AppendRequestUri("Model.MoveItems"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("TargetPath", TargetPath),
				new BodyOperationParameter("CatalogItemPaths", CatalogItemPaths)
			});
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x0001A424 File Offset: 0x00018624
		[OriginalName("MoveItems")]
		public static DataServiceActionQuerySingle<BulkOperationsResult> MoveItems(this DataServiceQuery<PowerBIReport> source, string TargetPath, ICollection<string> CatalogItemPaths)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<BulkOperationsResult>(source.Context, source.AppendRequestUri("Model.MoveItems"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("TargetPath", TargetPath),
				new BodyOperationParameter("CatalogItemPaths", CatalogItemPaths)
			});
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x0001A47C File Offset: 0x0001867C
		[OriginalName("MoveItems")]
		public static DataServiceActionQuerySingle<BulkOperationsResult> MoveItems(this DataServiceQuery<Resource> source, string TargetPath, ICollection<string> CatalogItemPaths)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<BulkOperationsResult>(source.Context, source.AppendRequestUri("Model.MoveItems"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("TargetPath", TargetPath),
				new BodyOperationParameter("CatalogItemPaths", CatalogItemPaths)
			});
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x0001A4D4 File Offset: 0x000186D4
		[OriginalName("MoveItems")]
		public static DataServiceActionQuerySingle<BulkOperationsResult> MoveItems(this DataServiceQuery<Kpi> source, string TargetPath, ICollection<string> CatalogItemPaths)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<BulkOperationsResult>(source.Context, source.AppendRequestUri("Model.MoveItems"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("TargetPath", TargetPath),
				new BodyOperationParameter("CatalogItemPaths", CatalogItemPaths)
			});
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x0001A52C File Offset: 0x0001872C
		[OriginalName("MoveItems")]
		public static DataServiceActionQuerySingle<BulkOperationsResult> MoveItems(this DataServiceQuery<MobileReport> source, string TargetPath, ICollection<string> CatalogItemPaths)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<BulkOperationsResult>(source.Context, source.AppendRequestUri("Model.MoveItems"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("TargetPath", TargetPath),
				new BodyOperationParameter("CatalogItemPaths", CatalogItemPaths)
			});
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x0001A584 File Offset: 0x00018784
		[OriginalName("SetPolicies")]
		public static DataServiceActionQuerySingle<bool> SetPolicies(this DataServiceQuerySingle<CatalogItem> source, ItemPolicy Policy)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<bool>(source.Context, source.AppendRequestUri("Model.SetPolicies"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("Policy", Policy)
			});
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x0001A5C3 File Offset: 0x000187C3
		[OriginalName("SetPolicies")]
		public static DataServiceActionQuerySingle<bool> SetPolicies(this DataServiceQuerySingle<Report> source, ItemPolicy Policy)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<bool>(source.Context, source.AppendRequestUri("Model.SetPolicies"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("Policy", Policy)
			});
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x0001A602 File Offset: 0x00018802
		[OriginalName("SetPolicies")]
		public static DataServiceActionQuerySingle<bool> SetPolicies(this DataServiceQuerySingle<LinkedReport> source, ItemPolicy Policy)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<bool>(source.Context, source.AppendRequestUri("Model.SetPolicies"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("Policy", Policy)
			});
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x0001A641 File Offset: 0x00018841
		[OriginalName("SetPolicies")]
		public static DataServiceActionQuerySingle<bool> SetPolicies(this DataServiceQuerySingle<DataSet> source, ItemPolicy Policy)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<bool>(source.Context, source.AppendRequestUri("Model.SetPolicies"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("Policy", Policy)
			});
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x0001A680 File Offset: 0x00018880
		[OriginalName("SetPolicies")]
		public static DataServiceActionQuerySingle<bool> SetPolicies(this DataServiceQuerySingle<DataSource> source, ItemPolicy Policy)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<bool>(source.Context, source.AppendRequestUri("Model.SetPolicies"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("Policy", Policy)
			});
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x0001A6BF File Offset: 0x000188BF
		[OriginalName("SetPolicies")]
		public static DataServiceActionQuerySingle<bool> SetPolicies(this DataServiceQuerySingle<Folder> source, ItemPolicy Policy)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<bool>(source.Context, source.AppendRequestUri("Model.SetPolicies"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("Policy", Policy)
			});
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x0001A6FE File Offset: 0x000188FE
		[OriginalName("SetPolicies")]
		public static DataServiceActionQuerySingle<bool> SetPolicies(this DataServiceQuerySingle<ExcelWorkbook> source, ItemPolicy Policy)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<bool>(source.Context, source.AppendRequestUri("Model.SetPolicies"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("Policy", Policy)
			});
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x0001A73D File Offset: 0x0001893D
		[OriginalName("SetPolicies")]
		public static DataServiceActionQuerySingle<bool> SetPolicies(this DataServiceQuerySingle<PowerBIReport> source, ItemPolicy Policy)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<bool>(source.Context, source.AppendRequestUri("Model.SetPolicies"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("Policy", Policy)
			});
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x0001A77C File Offset: 0x0001897C
		[OriginalName("SetPolicies")]
		public static DataServiceActionQuerySingle<bool> SetPolicies(this DataServiceQuerySingle<Resource> source, ItemPolicy Policy)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<bool>(source.Context, source.AppendRequestUri("Model.SetPolicies"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("Policy", Policy)
			});
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x0001A7BB File Offset: 0x000189BB
		[OriginalName("SetPolicies")]
		public static DataServiceActionQuerySingle<bool> SetPolicies(this DataServiceQuerySingle<Kpi> source, ItemPolicy Policy)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<bool>(source.Context, source.AppendRequestUri("Model.SetPolicies"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("Policy", Policy)
			});
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x0001A7FA File Offset: 0x000189FA
		[OriginalName("SetPolicies")]
		public static DataServiceActionQuerySingle<bool> SetPolicies(this DataServiceQuerySingle<MobileReport> source, ItemPolicy Policy)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<bool>(source.Context, source.AppendRequestUri("Model.SetPolicies"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("Policy", Policy)
			});
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x0001A839 File Offset: 0x00018A39
		[OriginalName("GetParameters")]
		public static DataServiceActionQuery<ReportParameterDefinition> GetParameters(this DataServiceQuerySingle<Report> source, ICollection<ParameterValue> ParameterValues)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuery<ReportParameterDefinition>(source.Context, source.AppendRequestUri("Model.GetParameters"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("ParameterValues", ParameterValues)
			});
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x0001A878 File Offset: 0x00018A78
		[OriginalName("SetParameterProperties")]
		public static DataServiceActionQuery SetParameterProperties(this DataServiceQuerySingle<Report> source, ICollection<ReportParameterDefinition> ParameterProperties)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuery(source.Context, source.AppendRequestUri("Model.SetParameterProperties"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("ParameterProperties", ParameterProperties)
			});
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x0001A8B7 File Offset: 0x00018AB7
		[OriginalName("SetReportHistorySnapshotsOptions")]
		public static DataServiceActionQuery SetReportHistorySnapshotsOptions(this DataServiceQuerySingle<Report> source, ReportHistorySnapshotsOptions ReportHistorySnapshotsOptions)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuery(source.Context, source.AppendRequestUri("Model.SetReportHistorySnapshotsOptions"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("ReportHistorySnapshotsOptions", ReportHistorySnapshotsOptions)
			});
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x0001A8F6 File Offset: 0x00018AF6
		[OriginalName("SetReportHistorySnapshotsOptions")]
		public static DataServiceActionQuery SetReportHistorySnapshotsOptions(this DataServiceQuerySingle<LinkedReport> source, ReportHistorySnapshotsOptions ReportHistorySnapshotsOptions)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuery(source.Context, source.AppendRequestUri("Model.SetReportHistorySnapshotsOptions"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("ReportHistorySnapshotsOptions", ReportHistorySnapshotsOptions)
			});
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x0001A935 File Offset: 0x00018B35
		[OriginalName("CreateSnapshot")]
		public static DataServiceActionQuerySingle<string> CreateSnapshot(this DataServiceQuerySingle<Report> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<string>(source.Context, source.AppendRequestUri("Model.CreateSnapshot"), Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x0001A965 File Offset: 0x00018B65
		[OriginalName("DeleteSnapshot")]
		public static DataServiceActionQuerySingle<bool> DeleteSnapshot(this DataServiceQuerySingle<Report> source, string HistoryId)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<bool>(source.Context, source.AppendRequestUri("Model.DeleteSnapshot"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("HistoryId", HistoryId)
			});
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x0001A9A4 File Offset: 0x00018BA4
		[OriginalName("UpdateExecutionSnapshot")]
		public static DataServiceActionQuerySingle<bool> UpdateExecutionSnapshot(this DataServiceQuerySingle<Report> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<bool>(source.Context, source.AppendRequestUri("Model.UpdateExecutionSnapshot"), Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x0001A9D4 File Offset: 0x00018BD4
		[OriginalName("CreateSnapshot")]
		public static DataServiceActionQuerySingle<string> CreateSnapshot(this DataServiceQuerySingle<LinkedReport> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<string>(source.Context, source.AppendRequestUri("Model.CreateSnapshot"), Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x0001AA04 File Offset: 0x00018C04
		[OriginalName("DeleteSnapshot")]
		public static DataServiceActionQuerySingle<bool> DeleteSnapshot(this DataServiceQuerySingle<LinkedReport> source, string HistoryId)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<bool>(source.Context, source.AppendRequestUri("Model.DeleteSnapshot"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("HistoryId", HistoryId)
			});
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x0001AA44 File Offset: 0x00018C44
		[OriginalName("GetData")]
		public static DataServiceActionQuerySingle<string> GetData(this DataServiceQuerySingle<DataSet> source, ICollection<DataSetParameter> Parameters, int? maxRows)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<string>(source.Context, source.AppendRequestUri("Model.GetData"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("Parameters", Parameters),
				new BodyOperationParameter("maxRows", maxRows)
			});
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x0001AAA4 File Offset: 0x00018CA4
		[OriginalName("GetKpiTrendsetData")]
		public static DataServiceActionQuerySingle<string> GetKpiTrendsetData(this DataServiceQuerySingle<DataSet> source, ICollection<DataSetParameter> Parameters, string columnName)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<string>(source.Context, source.AppendRequestUri("Model.GetKpiTrendsetData"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("Parameters", Parameters),
				new BodyOperationParameter("columnName", columnName)
			});
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x0001AAFC File Offset: 0x00018CFC
		[OriginalName("GetAggregatedValue")]
		public static DataServiceActionQuerySingle<string> GetAggregatedValue(this DataServiceQuerySingle<DataSet> source, ICollection<DataSetParameter> Parameters)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<string>(source.Context, source.AppendRequestUri("Model.GetAggregatedValue"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("Parameters", Parameters)
			});
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x0001AB3B File Offset: 0x00018D3B
		[OriginalName("CheckDataSourceConnection")]
		public static DataServiceActionQuerySingle<DataSourceCheckResult> CheckDataSourceConnection(this DataServiceQuerySingle<Report> source, string DataSourceName)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<DataSourceCheckResult>(source.Context, source.AppendRequestUri("Model.CheckDataSourceConnection"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("DataSourceName", DataSourceName)
			});
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x0001AB7A File Offset: 0x00018D7A
		[OriginalName("CheckConnection")]
		public static DataServiceActionQuerySingle<DataSourceCheckResult> CheckConnection(this DataServiceQuerySingle<DataSource> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<DataSourceCheckResult>(source.Context, source.AppendRequestUri("Model.CheckConnection"), Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x0001ABAA File Offset: 0x00018DAA
		[OriginalName("CheckConnection")]
		public static DataServiceActionQuerySingle<DataSourceCheckResult> CheckConnection(this DataServiceQuerySingle<ReportModel> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<DataSourceCheckResult>(source.Context, source.AppendRequestUri("Model.CheckConnection"), Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x0001ABDA File Offset: 0x00018DDA
		[OriginalName("CheckConnection")]
		public static DataServiceActionQuerySingle<DataSourceCheckResult> CheckConnection(this DataServiceQuery<DataSource> source, DataSource dataSource)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<DataSourceCheckResult>(source.Context, source.AppendRequestUri("Model.CheckConnection"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("dataSource", dataSource)
			});
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x0001AC19 File Offset: 0x00018E19
		[OriginalName("CheckConnection")]
		public static DataServiceActionQuerySingle<DataSourceCheckResult> CheckConnection(this DataServiceQuery<ReportModel> source, DataSource dataSource)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<DataSourceCheckResult>(source.Context, source.AppendRequestUri("Model.CheckConnection"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("dataSource", dataSource)
			});
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x0001AC58 File Offset: 0x00018E58
		[OriginalName("GetQueryFields")]
		public static DataServiceActionQuery<string> GetQueryFields(this DataServiceQuery<DataSource> source, DataSource dataSource, Query query, string subscriptionId)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuery<string>(source.Context, source.AppendRequestUri("Model.GetQueryFields"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("dataSource", dataSource),
				new BodyOperationParameter("query", query),
				new BodyOperationParameter("subscriptionId", subscriptionId)
			});
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x0001ACC0 File Offset: 0x00018EC0
		[OriginalName("GetQueryFields")]
		public static DataServiceActionQuery<string> GetQueryFields(this DataServiceQuery<ReportModel> source, DataSource dataSource, Query query, string subscriptionId)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuery<string>(source.Context, source.AppendRequestUri("Model.GetQueryFields"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("dataSource", dataSource),
				new BodyOperationParameter("query", query),
				new BodyOperationParameter("subscriptionId", subscriptionId)
			});
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x0001AD26 File Offset: 0x00018F26
		[OriginalName("UpdateItemDataSources")]
		public static DataServiceActionQuery UpdateItemDataSources(this DataServiceQuerySingle<Report> source, ICollection<DataSource> dataSources)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuery(source.Context, source.AppendRequestUri("Model.UpdateItemDataSources"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("dataSources", dataSources)
			});
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x0001AD65 File Offset: 0x00018F65
		[OriginalName("UpdateItemDataSources")]
		public static DataServiceActionQuery UpdateItemDataSources(this DataServiceQuerySingle<DataSet> source, ICollection<DataSource> dataSources)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuery(source.Context, source.AppendRequestUri("Model.UpdateItemDataSources"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("dataSources", dataSources)
			});
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x0001ADA4 File Offset: 0x00018FA4
		[OriginalName("SetCacheOptions")]
		public static DataServiceActionQuery SetCacheOptions(this DataServiceQuerySingle<Report> source, CacheOptions cacheOptions)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuery(source.Context, source.AppendRequestUri("Model.SetCacheOptions"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("cacheOptions", cacheOptions)
			});
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x0001ADE3 File Offset: 0x00018FE3
		[OriginalName("SetCacheOptions")]
		public static DataServiceActionQuery SetCacheOptions(this DataServiceQuerySingle<LinkedReport> source, CacheOptions cacheOptions)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuery(source.Context, source.AppendRequestUri("Model.SetCacheOptions"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("cacheOptions", cacheOptions)
			});
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x0001AE22 File Offset: 0x00019022
		[OriginalName("SetCacheOptions")]
		public static DataServiceActionQuery SetCacheOptions(this DataServiceQuerySingle<DataSet> source, CacheOptions cacheOptions)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuery(source.Context, source.AppendRequestUri("Model.SetCacheOptions"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("cacheOptions", cacheOptions)
			});
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x0001AE61 File Offset: 0x00019061
		[OriginalName("UpdateReportDataSets")]
		public static DataServiceActionQuery UpdateReportDataSets(this DataServiceQuerySingle<Report> source, ICollection<DataSet> dataSets)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuery(source.Context, source.AppendRequestUri("Model.UpdateReportDataSets"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("dataSets", dataSets)
			});
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x0001AEA0 File Offset: 0x000190A0
		[OriginalName("Pause")]
		public static DataServiceActionQuery Pause(this DataServiceQuerySingle<Schedule> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuery(source.Context, source.AppendRequestUri("Model.Pause"), Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x0001AED0 File Offset: 0x000190D0
		[OriginalName("Resume")]
		public static DataServiceActionQuery Resume(this DataServiceQuerySingle<Schedule> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuery(source.Context, source.AppendRequestUri("Model.Resume"), Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x0001AF00 File Offset: 0x00019100
		[OriginalName("Describe")]
		public static DataServiceActionQuery Describe(this DataServiceQuery<Schedule> source, Schedule schedule)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuery(source.Context, source.AppendRequestUri("Model.Describe"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("schedule", schedule)
			});
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x0001AF40 File Offset: 0x00019140
		[OriginalName("ValidateExtensionSettings")]
		public static DataServiceActionQuery<ExtensionParameter> ValidateExtensionSettings(this DataServiceQuerySingle<ReportServerInfo> source, ICollection<ParameterValue> ParameterValues, string ExtensionName)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuery<ExtensionParameter>(source.Context, source.AppendRequestUri("Model.ValidateExtensionSettings"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("ParameterValues", ParameterValues),
				new BodyOperationParameter("ExtensionName", ExtensionName)
			});
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x0001AF98 File Offset: 0x00019198
		[OriginalName("UpdateSettings")]
		public static DataServiceActionQuerySingle<bool> UpdateSettings(this DataServiceQuerySingle<ReportServerInfo> source, ICollection<Property> PropertyValues)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<bool>(source.Context, source.AppendRequestUri("Model.UpdateSettings"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("PropertyValues", PropertyValues)
			});
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x0001AFD7 File Offset: 0x000191D7
		[OriginalName("SetSystemPolicies")]
		public static DataServiceActionQuerySingle<bool> SetSystemPolicies(this DataServiceQuerySingle<ReportServerInfo> source, ItemPolicy Policy)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<bool>(source.Context, source.AppendRequestUri("Model.SetSystemPolicies"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("Policy", Policy)
			});
		}
	}
}
