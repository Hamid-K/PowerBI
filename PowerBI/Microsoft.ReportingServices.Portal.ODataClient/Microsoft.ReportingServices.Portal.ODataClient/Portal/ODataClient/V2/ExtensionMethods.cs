using System;
using System.Collections.Generic;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x020000A6 RID: 166
	public static class ExtensionMethods
	{
		// Token: 0x0600062A RID: 1578 RVA: 0x0000BF25 File Offset: 0x0000A125
		public static AlertSubscriptionSingle ByKey(this DataServiceQuery<AlertSubscription> source, Dictionary<string, object> keys)
		{
			return new AlertSubscriptionSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x0000BF44 File Offset: 0x0000A144
		public static AlertSubscriptionSingle ByKey(this DataServiceQuery<AlertSubscription> source, long id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new AlertSubscriptionSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x0000BF85 File Offset: 0x0000A185
		public static CacheRefreshPlanSingle ByKey(this DataServiceQuery<CacheRefreshPlan> source, Dictionary<string, object> keys)
		{
			return new CacheRefreshPlanSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x0000BFA4 File Offset: 0x0000A1A4
		public static CacheRefreshPlanSingle ByKey(this DataServiceQuery<CacheRefreshPlan> source, Guid id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new CacheRefreshPlanSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x0000BFE5 File Offset: 0x0000A1E5
		public static SubscriptionHistorySingle ByKey(this DataServiceQuery<SubscriptionHistory> source, Dictionary<string, object> keys)
		{
			return new SubscriptionHistorySingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x0000C004 File Offset: 0x0000A204
		public static SubscriptionHistorySingle ByKey(this DataServiceQuery<SubscriptionHistory> source, int id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new SubscriptionHistorySingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x0000C045 File Offset: 0x0000A245
		public static PropertySingle ByKey(this DataServiceQuery<Property> source, Dictionary<string, object> keys)
		{
			return new PropertySingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x0000C064 File Offset: 0x0000A264
		public static PropertySingle ByKey(this DataServiceQuery<Property> source, string name)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Name", name } };
			return new PropertySingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x0000C0A0 File Offset: 0x0000A2A0
		public static HistorySnapshotOptionsSingle ByKey(this DataServiceQuery<HistorySnapshotOptions> source, Dictionary<string, object> keys)
		{
			return new HistorySnapshotOptionsSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x0000C0C0 File Offset: 0x0000A2C0
		public static HistorySnapshotOptionsSingle ByKey(this DataServiceQuery<HistorySnapshotOptions> source, Guid catalogItemId)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "CatalogItemId", catalogItemId } };
			return new HistorySnapshotOptionsSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x0000C101 File Offset: 0x0000A301
		public static CacheOptionsSingle ByKey(this DataServiceQuery<CacheOptions> source, Dictionary<string, object> keys)
		{
			return new CacheOptionsSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x0000C120 File Offset: 0x0000A320
		public static CacheOptionsSingle ByKey(this DataServiceQuery<CacheOptions> source, Guid id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new CacheOptionsSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0000C161 File Offset: 0x0000A361
		public static CatalogItemSingle ByKey(this DataServiceQuery<CatalogItem> source, Dictionary<string, object> keys)
		{
			return new CatalogItemSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x0000C180 File Offset: 0x0000A380
		public static CatalogItemSingle ByKey(this DataServiceQuery<CatalogItem> source, Guid id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new CatalogItemSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x0000C1C1 File Offset: 0x0000A3C1
		public static ItemPolicySingle ByKey(this DataServiceQuery<ItemPolicy> source, Dictionary<string, object> keys)
		{
			return new ItemPolicySingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x0000C1E0 File Offset: 0x0000A3E0
		public static ItemPolicySingle ByKey(this DataServiceQuery<ItemPolicy> source, Guid id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new ItemPolicySingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0000C221 File Offset: 0x0000A421
		public static CommentSingle ByKey(this DataServiceQuery<Comment> source, Dictionary<string, object> keys)
		{
			return new CommentSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x0000C240 File Offset: 0x0000A440
		public static CommentSingle ByKey(this DataServiceQuery<Comment> source, long id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new CommentSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x0000C281 File Offset: 0x0000A481
		public static DataSetSingle ByKey(this DataServiceQuery<DataSet> source, Dictionary<string, object> keys)
		{
			return new DataSetSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x0000C2A0 File Offset: 0x0000A4A0
		public static DataSetSingle ByKey(this DataServiceQuery<DataSet> source, Guid id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new DataSetSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0000C2E4 File Offset: 0x0000A4E4
		public static DataSetSingle CastToDataSet(this DataServiceQuerySingle<CatalogItem> source)
		{
			DataServiceQuerySingle<DataSet> dataServiceQuerySingle = source.CastTo<DataSet>();
			return new DataSetSingle(source.Context, dataServiceQuerySingle.GetPath(null));
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x0000C30A File Offset: 0x0000A50A
		public static DataSetRowSingle ByKey(this DataServiceQuery<DataSetRow> source, Dictionary<string, object> keys)
		{
			return new DataSetRowSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x0000C32C File Offset: 0x0000A52C
		public static DataSetRowSingle ByKey(this DataServiceQuery<DataSetRow> source, Guid id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new DataSetRowSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x0000C36D File Offset: 0x0000A56D
		public static DataSourceSingle ByKey(this DataServiceQuery<DataSource> source, Dictionary<string, object> keys)
		{
			return new DataSourceSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x0000C38C File Offset: 0x0000A58C
		public static DataSourceSingle ByKey(this DataServiceQuery<DataSource> source, Guid id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new DataSourceSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x0000C3D0 File Offset: 0x0000A5D0
		public static DataSourceSingle CastToDataSource(this DataServiceQuerySingle<CatalogItem> source)
		{
			DataServiceQuerySingle<DataSource> dataServiceQuerySingle = source.CastTo<DataSource>();
			return new DataSourceSingle(source.Context, dataServiceQuerySingle.GetPath(null));
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x0000C3F6 File Offset: 0x0000A5F6
		public static ExcelWorkbookSingle ByKey(this DataServiceQuery<ExcelWorkbook> source, Dictionary<string, object> keys)
		{
			return new ExcelWorkbookSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x0000C418 File Offset: 0x0000A618
		public static ExcelWorkbookSingle ByKey(this DataServiceQuery<ExcelWorkbook> source, Guid id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new ExcelWorkbookSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x0000C45C File Offset: 0x0000A65C
		public static ExcelWorkbookSingle CastToExcelWorkbook(this DataServiceQuerySingle<CatalogItem> source)
		{
			DataServiceQuerySingle<ExcelWorkbook> dataServiceQuerySingle = source.CastTo<ExcelWorkbook>();
			return new ExcelWorkbookSingle(source.Context, dataServiceQuerySingle.GetPath(null));
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0000C482 File Offset: 0x0000A682
		public static ExtensionSingle ByKey(this DataServiceQuery<Extension> source, Dictionary<string, object> keys)
		{
			return new ExtensionSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x0000C4A4 File Offset: 0x0000A6A4
		public static ExtensionSingle ByKey(this DataServiceQuery<Extension> source, string name)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Name", name } };
			return new ExtensionSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x0000C4E0 File Offset: 0x0000A6E0
		public static FavoriteItemSingle ByKey(this DataServiceQuery<FavoriteItem> source, Dictionary<string, object> keys)
		{
			return new FavoriteItemSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x0000C500 File Offset: 0x0000A700
		public static FavoriteItemSingle ByKey(this DataServiceQuery<FavoriteItem> source, Guid id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new FavoriteItemSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x0000C541 File Offset: 0x0000A741
		public static FolderSingle ByKey(this DataServiceQuery<Folder> source, Dictionary<string, object> keys)
		{
			return new FolderSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x0000C560 File Offset: 0x0000A760
		public static FolderSingle ByKey(this DataServiceQuery<Folder> source, Guid id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new FolderSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x0000C5A4 File Offset: 0x0000A7A4
		public static FolderSingle CastToFolder(this DataServiceQuerySingle<CatalogItem> source)
		{
			DataServiceQuerySingle<Folder> dataServiceQuerySingle = source.CastTo<Folder>();
			return new FolderSingle(source.Context, dataServiceQuerySingle.GetPath(null));
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x0000C5CA File Offset: 0x0000A7CA
		public static KpiSingle ByKey(this DataServiceQuery<Kpi> source, Dictionary<string, object> keys)
		{
			return new KpiSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x0000C5EC File Offset: 0x0000A7EC
		public static KpiSingle ByKey(this DataServiceQuery<Kpi> source, Guid id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new KpiSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x0000C630 File Offset: 0x0000A830
		public static KpiSingle CastToKpi(this DataServiceQuerySingle<CatalogItem> source)
		{
			DataServiceQuerySingle<Kpi> dataServiceQuerySingle = source.CastTo<Kpi>();
			return new KpiSingle(source.Context, dataServiceQuerySingle.GetPath(null));
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x0000C656 File Offset: 0x0000A856
		public static LinkedReportSingle ByKey(this DataServiceQuery<LinkedReport> source, Dictionary<string, object> keys)
		{
			return new LinkedReportSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x0000C678 File Offset: 0x0000A878
		public static LinkedReportSingle ByKey(this DataServiceQuery<LinkedReport> source, Guid id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new LinkedReportSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x0000C6BC File Offset: 0x0000A8BC
		public static LinkedReportSingle CastToLinkedReport(this DataServiceQuerySingle<CatalogItem> source)
		{
			DataServiceQuerySingle<LinkedReport> dataServiceQuerySingle = source.CastTo<LinkedReport>();
			return new LinkedReportSingle(source.Context, dataServiceQuerySingle.GetPath(null));
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x0000C6E2 File Offset: 0x0000A8E2
		public static MobileReportSingle ByKey(this DataServiceQuery<MobileReport> source, Dictionary<string, object> keys)
		{
			return new MobileReportSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x0000C704 File Offset: 0x0000A904
		public static MobileReportSingle ByKey(this DataServiceQuery<MobileReport> source, Guid id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new MobileReportSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x0000C748 File Offset: 0x0000A948
		public static MobileReportSingle CastToMobileReport(this DataServiceQuerySingle<CatalogItem> source)
		{
			DataServiceQuerySingle<MobileReport> dataServiceQuerySingle = source.CastTo<MobileReport>();
			return new MobileReportSingle(source.Context, dataServiceQuerySingle.GetPath(null));
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x0000C76E File Offset: 0x0000A96E
		public static NotificationSingle ByKey(this DataServiceQuery<Notification> source, Dictionary<string, object> keys)
		{
			return new NotificationSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x0000C790 File Offset: 0x0000A990
		public static NotificationSingle ByKey(this DataServiceQuery<Notification> source, Guid id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new NotificationSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x0000C7D1 File Offset: 0x0000A9D1
		public static PowerBIReportSingle ByKey(this DataServiceQuery<PowerBIReport> source, Dictionary<string, object> keys)
		{
			return new PowerBIReportSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x0000C7F0 File Offset: 0x0000A9F0
		public static PowerBIReportSingle ByKey(this DataServiceQuery<PowerBIReport> source, Guid id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new PowerBIReportSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x0000C834 File Offset: 0x0000AA34
		public static PowerBIReportSingle CastToPowerBIReport(this DataServiceQuerySingle<CatalogItem> source)
		{
			DataServiceQuerySingle<PowerBIReport> dataServiceQuerySingle = source.CastTo<PowerBIReport>();
			return new PowerBIReportSingle(source.Context, dataServiceQuerySingle.GetPath(null));
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x0000C85A File Offset: 0x0000AA5A
		public static DataModelRoleSingle ByKey(this DataServiceQuery<DataModelRole> source, Dictionary<string, object> keys)
		{
			return new DataModelRoleSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x0000C87C File Offset: 0x0000AA7C
		public static DataModelRoleSingle ByKey(this DataServiceQuery<DataModelRole> source, Guid modelRoleId)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "ModelRoleId", modelRoleId } };
			return new DataModelRoleSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x0000C8BD File Offset: 0x0000AABD
		public static DataModelRoleAssignmentSingle ByKey(this DataServiceQuery<DataModelRoleAssignment> source, Dictionary<string, object> keys)
		{
			return new DataModelRoleAssignmentSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x0000C8DC File Offset: 0x0000AADC
		public static DataModelRoleAssignmentSingle ByKey(this DataServiceQuery<DataModelRoleAssignment> source, string groupUserName)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "GroupUserName", groupUserName } };
			return new DataModelRoleAssignmentSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x0000C918 File Offset: 0x0000AB18
		public static ReportSingle ByKey(this DataServiceQuery<Report> source, Dictionary<string, object> keys)
		{
			return new ReportSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x0000C938 File Offset: 0x0000AB38
		public static ReportSingle ByKey(this DataServiceQuery<Report> source, Guid id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new ReportSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x0000C97C File Offset: 0x0000AB7C
		public static ReportSingle CastToReport(this DataServiceQuerySingle<CatalogItem> source)
		{
			DataServiceQuerySingle<Report> dataServiceQuerySingle = source.CastTo<Report>();
			return new ReportSingle(source.Context, dataServiceQuerySingle.GetPath(null));
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x0000C9A2 File Offset: 0x0000ABA2
		public static ReportParameterDefinitionSingle ByKey(this DataServiceQuery<ReportParameterDefinition> source, Dictionary<string, object> keys)
		{
			return new ReportParameterDefinitionSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x0000C9C4 File Offset: 0x0000ABC4
		public static ReportParameterDefinitionSingle ByKey(this DataServiceQuery<ReportParameterDefinition> source, string name)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Name", name } };
			return new ReportParameterDefinitionSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x0000CA00 File Offset: 0x0000AC00
		public static SystemSingle ByKey(this DataServiceQuery<Microsoft.ReportingServices.Portal.ODataClient.V2.System> source, Dictionary<string, object> keys)
		{
			return new SystemSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x0000CA20 File Offset: 0x0000AC20
		public static SystemSingle ByKey(this DataServiceQuery<Microsoft.ReportingServices.Portal.ODataClient.V2.System> source, Guid id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new SystemSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x0000CA61 File Offset: 0x0000AC61
		public static SystemPolicySingle ByKey(this DataServiceQuery<SystemPolicy> source, Dictionary<string, object> keys)
		{
			return new SystemPolicySingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x0000CA80 File Offset: 0x0000AC80
		public static SystemPolicySingle ByKey(this DataServiceQuery<SystemPolicy> source, Guid id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new SystemPolicySingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x0000CAC1 File Offset: 0x0000ACC1
		public static ResourceSingle ByKey(this DataServiceQuery<Resource> source, Dictionary<string, object> keys)
		{
			return new ResourceSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x0000CAE0 File Offset: 0x0000ACE0
		public static ResourceSingle ByKey(this DataServiceQuery<Resource> source, Guid id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new ResourceSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x0000CB24 File Offset: 0x0000AD24
		public static ResourceSingle CastToResource(this DataServiceQuerySingle<CatalogItem> source)
		{
			DataServiceQuerySingle<Resource> dataServiceQuerySingle = source.CastTo<Resource>();
			return new ResourceSingle(source.Context, dataServiceQuerySingle.GetPath(null));
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x0000CB4A File Offset: 0x0000AD4A
		public static ScheduleSingle ByKey(this DataServiceQuery<Schedule> source, Dictionary<string, object> keys)
		{
			return new ScheduleSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x0000CB6C File Offset: 0x0000AD6C
		public static ScheduleSingle ByKey(this DataServiceQuery<Schedule> source, Guid id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new ScheduleSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x0000CBAD File Offset: 0x0000ADAD
		public static SubscriptionSingle ByKey(this DataServiceQuery<Subscription> source, Dictionary<string, object> keys)
		{
			return new SubscriptionSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x0000CBCC File Offset: 0x0000ADCC
		public static SubscriptionSingle ByKey(this DataServiceQuery<Subscription> source, Guid id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new SubscriptionSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x0000CC0D File Offset: 0x0000AE0D
		public static SystemResourceSingle ByKey(this DataServiceQuery<SystemResource> source, Dictionary<string, object> keys)
		{
			return new SystemResourceSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x0000CC2C File Offset: 0x0000AE2C
		public static SystemResourceSingle ByKey(this DataServiceQuery<SystemResource> source, Guid id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new SystemResourceSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x0000CC6D File Offset: 0x0000AE6D
		public static SystemResourceItemSingle ByKey(this DataServiceQuery<SystemResourceItem> source, Dictionary<string, object> keys)
		{
			return new SystemResourceItemSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x0000CC8C File Offset: 0x0000AE8C
		public static SystemResourceItemSingle ByKey(this DataServiceQuery<SystemResourceItem> source, Guid id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new SystemResourceItemSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x0000CCCD File Offset: 0x0000AECD
		public static UserSettingsSingle ByKey(this DataServiceQuery<UserSettings> source, Dictionary<string, object> keys)
		{
			return new UserSettingsSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x0000CCEC File Offset: 0x0000AEEC
		public static UserSettingsSingle ByKey(this DataServiceQuery<UserSettings> source, Guid id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new UserSettingsSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x0000CD2D File Offset: 0x0000AF2D
		public static UserSingle ByKey(this DataServiceQuery<User> source, Dictionary<string, object> keys)
		{
			return new UserSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x0000CD4C File Offset: 0x0000AF4C
		public static UserSingle ByKey(this DataServiceQuery<User> source, Guid id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new UserSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x0000CD8D File Offset: 0x0000AF8D
		public static TelemetrySingle ByKey(this DataServiceQuery<Telemetry> source, Dictionary<string, object> keys)
		{
			return new TelemetrySingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x0000CDAC File Offset: 0x0000AFAC
		public static TelemetrySingle ByKey(this DataServiceQuery<Telemetry> source, Guid id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new TelemetrySingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x0000CDED File Offset: 0x0000AFED
		public static PowerBIUserInfoSingle ByKey(this DataServiceQuery<PowerBIUserInfo> source, Dictionary<string, object> keys)
		{
			return new PowerBIUserInfoSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x0000CE0C File Offset: 0x0000B00C
		public static PowerBIUserInfoSingle ByKey(this DataServiceQuery<PowerBIUserInfo> source, Guid id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new PowerBIUserInfoSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x0000CE4D File Offset: 0x0000B04D
		public static AllowedActionSingle ByKey(this DataServiceQuery<AllowedAction> source, Dictionary<string, object> keys)
		{
			return new AllowedActionSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0000CE6C File Offset: 0x0000B06C
		public static AllowedActionSingle ByKey(this DataServiceQuery<AllowedAction> source, string action)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Action", action } };
			return new AllowedActionSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x0000CEA8 File Offset: 0x0000B0A8
		public static ReportModelSingle ByKey(this DataServiceQuery<ReportModel> source, Dictionary<string, object> keys)
		{
			return new ReportModelSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x0000CEC8 File Offset: 0x0000B0C8
		public static ReportModelSingle ByKey(this DataServiceQuery<ReportModel> source, Guid id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new ReportModelSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0000CF0C File Offset: 0x0000B10C
		public static ReportModelSingle CastToReportModel(this DataServiceQuerySingle<DataSource> source)
		{
			DataServiceQuerySingle<ReportModel> dataServiceQuerySingle = source.CastTo<ReportModel>();
			return new ReportModelSingle(source.Context, dataServiceQuerySingle.GetPath(null));
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x0000CF34 File Offset: 0x0000B134
		public static ReportModelSingle CastToReportModel(this DataServiceQuerySingle<CatalogItem> source)
		{
			DataServiceQuerySingle<ReportModel> dataServiceQuerySingle = source.CastTo<ReportModel>();
			return new ReportModelSingle(source.Context, dataServiceQuerySingle.GetPath(null));
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x0000CF5A File Offset: 0x0000B15A
		public static DataModelParameterSingle ByKey(this DataServiceQuery<DataModelParameter> source, Dictionary<string, object> keys)
		{
			return new DataModelParameterSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0000CF7C File Offset: 0x0000B17C
		public static DataModelParameterSingle ByKey(this DataServiceQuery<DataModelParameter> source, string name)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Name", name } };
			return new DataModelParameterSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x0000CFB8 File Offset: 0x0000B1B8
		public static ReportHistorySnapshotSingle ByKey(this DataServiceQuery<ReportHistorySnapshot> source, Dictionary<string, object> keys)
		{
			return new ReportHistorySnapshotSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x0000CFD8 File Offset: 0x0000B1D8
		public static ReportHistorySnapshotSingle ByKey(this DataServiceQuery<ReportHistorySnapshot> source, string historyId)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "HistoryId", historyId } };
			return new ReportHistorySnapshotSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0000D014 File Offset: 0x0000B214
		public static HistorySnapshotSingle ByKey(this DataServiceQuery<HistorySnapshot> source, Dictionary<string, object> keys)
		{
			return new HistorySnapshotSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0000D034 File Offset: 0x0000B234
		public static HistorySnapshotSingle ByKey(this DataServiceQuery<HistorySnapshot> source, Guid id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new HistorySnapshotSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0000D075 File Offset: 0x0000B275
		public static ComponentSingle ByKey(this DataServiceQuery<Component> source, Dictionary<string, object> keys)
		{
			return new ComponentSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x0000D094 File Offset: 0x0000B294
		public static ComponentSingle ByKey(this DataServiceQuery<Component> source, Guid id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new ComponentSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0000D0D8 File Offset: 0x0000B2D8
		public static ComponentSingle CastToComponent(this DataServiceQuerySingle<Resource> source)
		{
			DataServiceQuerySingle<Component> dataServiceQuerySingle = source.CastTo<Component>();
			return new ComponentSingle(source.Context, dataServiceQuerySingle.GetPath(null));
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x0000D100 File Offset: 0x0000B300
		public static ComponentSingle CastToComponent(this DataServiceQuerySingle<CatalogItem> source)
		{
			DataServiceQuerySingle<Component> dataServiceQuerySingle = source.CastTo<Component>();
			return new ComponentSingle(source.Context, dataServiceQuerySingle.GetPath(null));
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x0000D126 File Offset: 0x0000B326
		public static DeliveryExtensionSingle ByKey(this DataServiceQuery<DeliveryExtension> source, Dictionary<string, object> keys)
		{
			return new DeliveryExtensionSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x0000D148 File Offset: 0x0000B348
		public static DeliveryExtensionSingle ByKey(this DataServiceQuery<DeliveryExtension> source, string name)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Name", name } };
			return new DeliveryExtensionSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x0000D184 File Offset: 0x0000B384
		public static DeliveryExtensionSingle CastToDeliveryExtension(this DataServiceQuerySingle<Extension> source)
		{
			DataServiceQuerySingle<DeliveryExtension> dataServiceQuerySingle = source.CastTo<DeliveryExtension>();
			return new DeliveryExtensionSingle(source.Context, dataServiceQuerySingle.GetPath(null));
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x0000D1AA File Offset: 0x0000B3AA
		public static SystemResourcePackageSingle ByKey(this DataServiceQuery<SystemResourcePackage> source, Dictionary<string, object> keys)
		{
			return new SystemResourcePackageSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, keys)));
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x0000D1CC File Offset: 0x0000B3CC
		public static SystemResourcePackageSingle ByKey(this DataServiceQuery<SystemResourcePackage> source, Guid id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Id", id } };
			return new SystemResourcePackageSingle(source.Context, source.GetKeyPath(Serializer.GetKeyString(source.Context, dictionary)));
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x0000D210 File Offset: 0x0000B410
		public static SystemResourcePackageSingle CastToSystemResourcePackage(this DataServiceQuerySingle<SystemResource> source)
		{
			DataServiceQuerySingle<SystemResourcePackage> dataServiceQuerySingle = source.CastTo<SystemResourcePackage>();
			return new SystemResourcePackageSingle(source.Context, dataServiceQuerySingle.GetPath(null));
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x0000D236 File Offset: 0x0000B436
		[OriginalName("AccessToken")]
		public static DataServiceQuerySingle<CatalogItemAccessToken> AccessToken(this DataServiceQuerySingle<CatalogItem> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuerySingle<CatalogItemAccessToken>("Model.AccessToken", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x0000D25C File Offset: 0x0000B45C
		[OriginalName("AccessToken")]
		public static DataServiceQuerySingle<CatalogItemAccessToken> AccessToken(this DataServiceQuerySingle<DataSet> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuerySingle<CatalogItemAccessToken>("Model.AccessToken", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x0000D282 File Offset: 0x0000B482
		[OriginalName("AccessToken")]
		public static DataServiceQuerySingle<CatalogItemAccessToken> AccessToken(this DataServiceQuerySingle<DataSource> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuerySingle<CatalogItemAccessToken>("Model.AccessToken", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x0000D2A8 File Offset: 0x0000B4A8
		[OriginalName("AccessToken")]
		public static DataServiceQuerySingle<CatalogItemAccessToken> AccessToken(this DataServiceQuerySingle<ExcelWorkbook> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuerySingle<CatalogItemAccessToken>("Model.AccessToken", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0000D2CE File Offset: 0x0000B4CE
		[OriginalName("AccessToken")]
		public static DataServiceQuerySingle<CatalogItemAccessToken> AccessToken(this DataServiceQuerySingle<Folder> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuerySingle<CatalogItemAccessToken>("Model.AccessToken", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x0000D2F4 File Offset: 0x0000B4F4
		[OriginalName("AccessToken")]
		public static DataServiceQuerySingle<CatalogItemAccessToken> AccessToken(this DataServiceQuerySingle<Kpi> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuerySingle<CatalogItemAccessToken>("Model.AccessToken", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x0000D31A File Offset: 0x0000B51A
		[OriginalName("AccessToken")]
		public static DataServiceQuerySingle<CatalogItemAccessToken> AccessToken(this DataServiceQuerySingle<LinkedReport> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuerySingle<CatalogItemAccessToken>("Model.AccessToken", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0000D340 File Offset: 0x0000B540
		[OriginalName("AccessToken")]
		public static DataServiceQuerySingle<CatalogItemAccessToken> AccessToken(this DataServiceQuerySingle<MobileReport> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuerySingle<CatalogItemAccessToken>("Model.AccessToken", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x0000D366 File Offset: 0x0000B566
		[OriginalName("AccessToken")]
		public static DataServiceQuerySingle<CatalogItemAccessToken> AccessToken(this DataServiceQuerySingle<PowerBIReport> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuerySingle<CatalogItemAccessToken>("Model.AccessToken", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x0000D38C File Offset: 0x0000B58C
		[OriginalName("AccessToken")]
		public static DataServiceQuerySingle<CatalogItemAccessToken> AccessToken(this DataServiceQuerySingle<Report> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuerySingle<CatalogItemAccessToken>("Model.AccessToken", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x0000D3B2 File Offset: 0x0000B5B2
		[OriginalName("AccessToken")]
		public static DataServiceQuerySingle<CatalogItemAccessToken> AccessToken(this DataServiceQuerySingle<Resource> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuerySingle<CatalogItemAccessToken>("Model.AccessToken", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x0000D3D8 File Offset: 0x0000B5D8
		[OriginalName("GetSchema")]
		public static DataServiceQuerySingle<DataSetSchema> GetSchema(this DataServiceQuerySingle<DataSet> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuerySingle<DataSetSchema>("Model.GetSchema", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x0000D3FE File Offset: 0x0000B5FE
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

		// Token: 0x0600069F RID: 1695 RVA: 0x0000D433 File Offset: 0x0000B633
		[OriginalName("IsEnabled")]
		public static DataServiceQuerySingle<bool> IsEnabled(this DataServiceQuerySingle<PowerBIUserInfo> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuerySingle<bool>("Model.IsEnabled", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x0000D459 File Offset: 0x0000B659
		[OriginalName("Execute")]
		public static DataServiceActionQuery Execute(this DataServiceQuerySingle<CacheRefreshPlan> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuery(source.Context, source.AppendRequestUri("Model.Execute"), Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x0000D489 File Offset: 0x0000B689
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

		// Token: 0x060006A2 RID: 1698 RVA: 0x0000D4C8 File Offset: 0x0000B6C8
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

		// Token: 0x060006A3 RID: 1699 RVA: 0x0000D507 File Offset: 0x0000B707
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

		// Token: 0x060006A4 RID: 1700 RVA: 0x0000D546 File Offset: 0x0000B746
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

		// Token: 0x060006A5 RID: 1701 RVA: 0x0000D585 File Offset: 0x0000B785
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

		// Token: 0x060006A6 RID: 1702 RVA: 0x0000D5C4 File Offset: 0x0000B7C4
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

		// Token: 0x060006A7 RID: 1703 RVA: 0x0000D603 File Offset: 0x0000B803
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

		// Token: 0x060006A8 RID: 1704 RVA: 0x0000D642 File Offset: 0x0000B842
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

		// Token: 0x060006A9 RID: 1705 RVA: 0x0000D681 File Offset: 0x0000B881
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

		// Token: 0x060006AA RID: 1706 RVA: 0x0000D6C0 File Offset: 0x0000B8C0
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

		// Token: 0x060006AB RID: 1707 RVA: 0x0000D6FF File Offset: 0x0000B8FF
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

		// Token: 0x060006AC RID: 1708 RVA: 0x0000D740 File Offset: 0x0000B940
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

		// Token: 0x060006AD RID: 1709 RVA: 0x0000D798 File Offset: 0x0000B998
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

		// Token: 0x060006AE RID: 1710 RVA: 0x0000D7F0 File Offset: 0x0000B9F0
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

		// Token: 0x060006AF RID: 1711 RVA: 0x0000D848 File Offset: 0x0000BA48
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

		// Token: 0x060006B0 RID: 1712 RVA: 0x0000D8A0 File Offset: 0x0000BAA0
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

		// Token: 0x060006B1 RID: 1713 RVA: 0x0000D8F8 File Offset: 0x0000BAF8
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

		// Token: 0x060006B2 RID: 1714 RVA: 0x0000D950 File Offset: 0x0000BB50
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

		// Token: 0x060006B3 RID: 1715 RVA: 0x0000D9A8 File Offset: 0x0000BBA8
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

		// Token: 0x060006B4 RID: 1716 RVA: 0x0000DA00 File Offset: 0x0000BC00
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

		// Token: 0x060006B5 RID: 1717 RVA: 0x0000DA58 File Offset: 0x0000BC58
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

		// Token: 0x060006B6 RID: 1718 RVA: 0x0000DAB0 File Offset: 0x0000BCB0
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

		// Token: 0x060006B7 RID: 1719 RVA: 0x0000DB08 File Offset: 0x0000BD08
		[OriginalName("GetContentTrusted")]
		public static DataServiceActionQuerySingle<string> GetContentTrusted(this DataServiceQuerySingle<CatalogItem> source, string TrustedProcessToken)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<string>(source.Context, source.AppendRequestUri("Model.GetContentTrusted"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("TrustedProcessToken", TrustedProcessToken)
			});
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x0000DB47 File Offset: 0x0000BD47
		[OriginalName("GetContentTrusted")]
		public static DataServiceActionQuerySingle<string> GetContentTrusted(this DataServiceQuerySingle<DataSet> source, string TrustedProcessToken)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<string>(source.Context, source.AppendRequestUri("Model.GetContentTrusted"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("TrustedProcessToken", TrustedProcessToken)
			});
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x0000DB86 File Offset: 0x0000BD86
		[OriginalName("GetContentTrusted")]
		public static DataServiceActionQuerySingle<string> GetContentTrusted(this DataServiceQuerySingle<DataSource> source, string TrustedProcessToken)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<string>(source.Context, source.AppendRequestUri("Model.GetContentTrusted"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("TrustedProcessToken", TrustedProcessToken)
			});
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x0000DBC5 File Offset: 0x0000BDC5
		[OriginalName("GetContentTrusted")]
		public static DataServiceActionQuerySingle<string> GetContentTrusted(this DataServiceQuerySingle<ExcelWorkbook> source, string TrustedProcessToken)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<string>(source.Context, source.AppendRequestUri("Model.GetContentTrusted"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("TrustedProcessToken", TrustedProcessToken)
			});
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x0000DC04 File Offset: 0x0000BE04
		[OriginalName("GetContentTrusted")]
		public static DataServiceActionQuerySingle<string> GetContentTrusted(this DataServiceQuerySingle<Folder> source, string TrustedProcessToken)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<string>(source.Context, source.AppendRequestUri("Model.GetContentTrusted"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("TrustedProcessToken", TrustedProcessToken)
			});
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x0000DC43 File Offset: 0x0000BE43
		[OriginalName("GetContentTrusted")]
		public static DataServiceActionQuerySingle<string> GetContentTrusted(this DataServiceQuerySingle<Kpi> source, string TrustedProcessToken)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<string>(source.Context, source.AppendRequestUri("Model.GetContentTrusted"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("TrustedProcessToken", TrustedProcessToken)
			});
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x0000DC82 File Offset: 0x0000BE82
		[OriginalName("GetContentTrusted")]
		public static DataServiceActionQuerySingle<string> GetContentTrusted(this DataServiceQuerySingle<LinkedReport> source, string TrustedProcessToken)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<string>(source.Context, source.AppendRequestUri("Model.GetContentTrusted"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("TrustedProcessToken", TrustedProcessToken)
			});
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x0000DCC1 File Offset: 0x0000BEC1
		[OriginalName("GetContentTrusted")]
		public static DataServiceActionQuerySingle<string> GetContentTrusted(this DataServiceQuerySingle<MobileReport> source, string TrustedProcessToken)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<string>(source.Context, source.AppendRequestUri("Model.GetContentTrusted"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("TrustedProcessToken", TrustedProcessToken)
			});
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x0000DD00 File Offset: 0x0000BF00
		[OriginalName("GetContentTrusted")]
		public static DataServiceActionQuerySingle<string> GetContentTrusted(this DataServiceQuerySingle<PowerBIReport> source, string TrustedProcessToken)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<string>(source.Context, source.AppendRequestUri("Model.GetContentTrusted"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("TrustedProcessToken", TrustedProcessToken)
			});
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x0000DD3F File Offset: 0x0000BF3F
		[OriginalName("GetContentTrusted")]
		public static DataServiceActionQuerySingle<string> GetContentTrusted(this DataServiceQuerySingle<Report> source, string TrustedProcessToken)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<string>(source.Context, source.AppendRequestUri("Model.GetContentTrusted"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("TrustedProcessToken", TrustedProcessToken)
			});
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x0000DD7E File Offset: 0x0000BF7E
		[OriginalName("GetContentTrusted")]
		public static DataServiceActionQuerySingle<string> GetContentTrusted(this DataServiceQuerySingle<Resource> source, string TrustedProcessToken)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<string>(source.Context, source.AppendRequestUri("Model.GetContentTrusted"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("TrustedProcessToken", TrustedProcessToken)
			});
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x0000DDBD File Offset: 0x0000BFBD
		[OriginalName("Upload")]
		public static DataServiceActionQuerySingle<DataSet> Upload(this DataServiceQuerySingle<DataSet> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<DataSet>(source.Context, source.AppendRequestUri("Model.Upload"), Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x0000DDF0 File Offset: 0x0000BFF0
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

		// Token: 0x060006C4 RID: 1732 RVA: 0x0000DE50 File Offset: 0x0000C050
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

		// Token: 0x060006C5 RID: 1733 RVA: 0x0000DEA8 File Offset: 0x0000C0A8
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

		// Token: 0x060006C6 RID: 1734 RVA: 0x0000DEE7 File Offset: 0x0000C0E7
		[OriginalName("Upload")]
		public static DataServiceActionQuerySingle<DataSource> Upload(this DataServiceQuerySingle<DataSource> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<DataSource>(source.Context, source.AppendRequestUri("Model.Upload"), Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0000DF17 File Offset: 0x0000C117
		[OriginalName("Upload")]
		public static DataServiceActionQuerySingle<DataSource> Upload(this DataServiceQuerySingle<ReportModel> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<DataSource>(source.Context, source.AppendRequestUri("Model.Upload"), Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x0000DF47 File Offset: 0x0000C147
		[OriginalName("CheckConnection")]
		public static DataServiceActionQuerySingle<DataSourceCheckResult> CheckConnection(this DataServiceQuerySingle<DataSource> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<DataSourceCheckResult>(source.Context, source.AppendRequestUri("Model.CheckConnection"), Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x0000DF77 File Offset: 0x0000C177
		[OriginalName("CheckConnection")]
		public static DataServiceActionQuerySingle<DataSourceCheckResult> CheckConnection(this DataServiceQuerySingle<ReportModel> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<DataSourceCheckResult>(source.Context, source.AppendRequestUri("Model.CheckConnection"), Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x0000DFA7 File Offset: 0x0000C1A7
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

		// Token: 0x060006CB RID: 1739 RVA: 0x0000DFE6 File Offset: 0x0000C1E6
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

		// Token: 0x060006CC RID: 1740 RVA: 0x0000E028 File Offset: 0x0000C228
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

		// Token: 0x060006CD RID: 1741 RVA: 0x0000E090 File Offset: 0x0000C290
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

		// Token: 0x060006CE RID: 1742 RVA: 0x0000E0F6 File Offset: 0x0000C2F6
		[OriginalName("Upload")]
		public static DataServiceActionQuerySingle<ExcelWorkbook> Upload(this DataServiceQuerySingle<ExcelWorkbook> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<ExcelWorkbook>(source.Context, source.AppendRequestUri("Model.Upload"), Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x0000E128 File Offset: 0x0000C328
		[OriginalName("ValidateExtensionSettings")]
		public static DataServiceActionQuery<ExtensionParameter> ValidateExtensionSettings(this DataServiceQuery<Extension> source, ICollection<ParameterValue> ParameterValues, string ExtensionName)
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

		// Token: 0x060006D0 RID: 1744 RVA: 0x0000E180 File Offset: 0x0000C380
		[OriginalName("ValidateExtensionSettings")]
		public static DataServiceActionQuery<ExtensionParameter> ValidateExtensionSettings(this DataServiceQuery<DeliveryExtension> source, ICollection<ParameterValue> ParameterValues, string ExtensionName)
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

		// Token: 0x060006D1 RID: 1745 RVA: 0x0000E1D8 File Offset: 0x0000C3D8
		[OriginalName("Upload")]
		public static DataServiceActionQuerySingle<Folder> Upload(this DataServiceQuerySingle<Folder> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<Folder>(source.Context, source.AppendRequestUri("Model.Upload"), Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x0000E208 File Offset: 0x0000C408
		[OriginalName("Upload")]
		public static DataServiceActionQuerySingle<Kpi> Upload(this DataServiceQuerySingle<Kpi> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<Kpi>(source.Context, source.AppendRequestUri("Model.Upload"), Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x0000E238 File Offset: 0x0000C438
		[OriginalName("Upload")]
		public static DataServiceActionQuerySingle<LinkedReport> Upload(this DataServiceQuerySingle<LinkedReport> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<LinkedReport>(source.Context, source.AppendRequestUri("Model.Upload"), Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x0000E268 File Offset: 0x0000C468
		[OriginalName("UpdateCacheSnapshot")]
		public static DataServiceActionQuerySingle<bool> UpdateCacheSnapshot(this DataServiceQuerySingle<LinkedReport> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<bool>(source.Context, source.AppendRequestUri("Model.UpdateCacheSnapshot"), Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x0000E298 File Offset: 0x0000C498
		[OriginalName("Reparent")]
		public static DataServiceActionQuerySingle<string> Reparent(this DataServiceQuerySingle<LinkedReport> source, string ParentPath)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<string>(source.Context, source.AppendRequestUri("Model.Reparent"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("ParentPath", ParentPath)
			});
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x0000E2D7 File Offset: 0x0000C4D7
		[OriginalName("Upload")]
		public static DataServiceActionQuerySingle<MobileReport> Upload(this DataServiceQuerySingle<MobileReport> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<MobileReport>(source.Context, source.AppendRequestUri("Model.Upload"), Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x0000E307 File Offset: 0x0000C507
		[OriginalName("UpdateReportDataSets")]
		public static DataServiceActionQuery UpdateReportDataSets(this DataServiceQuerySingle<MobileReport> source, ICollection<DataSet> DataSets)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuery(source.Context, source.AppendRequestUri("Model.UpdateReportDataSets"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("DataSets", DataSets)
			});
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x0000E346 File Offset: 0x0000C546
		[OriginalName("Upload")]
		public static DataServiceActionQuerySingle<PowerBIReport> Upload(this DataServiceQuerySingle<PowerBIReport> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<PowerBIReport>(source.Context, source.AppendRequestUri("Model.Upload"), Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x0000E376 File Offset: 0x0000C576
		[OriginalName("CheckDataSourceConnection")]
		public static DataServiceActionQuerySingle<DataSourceCheckResult> CheckDataSourceConnection(this DataServiceQuerySingle<PowerBIReport> source, string DataSourceName)
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

		// Token: 0x060006DA RID: 1754 RVA: 0x0000E3B5 File Offset: 0x0000C5B5
		[OriginalName("Upload")]
		public static DataServiceActionQuerySingle<Report> Upload(this DataServiceQuerySingle<Report> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<Report>(source.Context, source.AppendRequestUri("Model.Upload"), Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x0000E3E5 File Offset: 0x0000C5E5
		[OriginalName("UpdateCacheSnapshot")]
		public static DataServiceActionQuerySingle<bool> UpdateCacheSnapshot(this DataServiceQuerySingle<Report> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<bool>(source.Context, source.AppendRequestUri("Model.UpdateCacheSnapshot"), Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x0000E415 File Offset: 0x0000C615
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

		// Token: 0x060006DD RID: 1757 RVA: 0x0000E454 File Offset: 0x0000C654
		[OriginalName("UpdateReportDataSets")]
		public static DataServiceActionQuery UpdateReportDataSets(this DataServiceQuerySingle<Report> source, ICollection<DataSet> DataSets)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuery(source.Context, source.AppendRequestUri("Model.UpdateReportDataSets"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("DataSets", DataSets)
			});
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x0000E493 File Offset: 0x0000C693
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

		// Token: 0x060006DF RID: 1759 RVA: 0x0000E4D2 File Offset: 0x0000C6D2
		[OriginalName("ProcessParameters")]
		public static DataServiceActionQuery<ReportParameterDefinition> ProcessParameters(this DataServiceQuerySingle<Report> source, ICollection<ParameterValue> ParameterValues)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuery<ReportParameterDefinition>(source.Context, source.AppendRequestUri("Model.ProcessParameters"), new BodyOperationParameter[]
			{
				new BodyOperationParameter("ParameterValues", ParameterValues)
			});
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x0000E511 File Offset: 0x0000C711
		[OriginalName("Upload")]
		public static DataServiceActionQuerySingle<Resource> Upload(this DataServiceQuerySingle<Resource> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<Resource>(source.Context, source.AppendRequestUri("Model.Upload"), Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x0000E541 File Offset: 0x0000C741
		[OriginalName("Upload")]
		public static DataServiceActionQuerySingle<Resource> Upload(this DataServiceQuerySingle<Component> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuerySingle<Resource>(source.Context, source.AppendRequestUri("Model.Upload"), Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x0000E571 File Offset: 0x0000C771
		[OriginalName("Pause")]
		public static DataServiceActionQuery Pause(this DataServiceQuerySingle<Schedule> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuery(source.Context, source.AppendRequestUri("Model.Pause"), Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x0000E5A1 File Offset: 0x0000C7A1
		[OriginalName("Resume")]
		public static DataServiceActionQuery Resume(this DataServiceQuerySingle<Schedule> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuery(source.Context, source.AppendRequestUri("Model.Resume"), Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x0000E5D1 File Offset: 0x0000C7D1
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

		// Token: 0x060006E5 RID: 1765 RVA: 0x0000E610 File Offset: 0x0000C810
		[OriginalName("Enable")]
		public static DataServiceActionQuery Enable(this DataServiceQuerySingle<Subscription> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuery(source.Context, source.AppendRequestUri("Model.Enable"), Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x0000E640 File Offset: 0x0000C840
		[OriginalName("Disable")]
		public static DataServiceActionQuery Disable(this DataServiceQuerySingle<Subscription> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuery(source.Context, source.AppendRequestUri("Model.Disable"), Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x0000E670 File Offset: 0x0000C870
		[OriginalName("Execute")]
		public static DataServiceActionQuery Execute(this DataServiceQuerySingle<Subscription> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuery(source.Context, source.AppendRequestUri("Model.Execute"), Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x0000E6A0 File Offset: 0x0000C8A0
		[OriginalName("Logout")]
		public static DataServiceActionQuery Logout(this DataServiceQuerySingle<PowerBIUserInfo> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuery(source.Context, source.AppendRequestUri("Model.Logout"), Array.Empty<BodyOperationParameter>());
		}
	}
}
