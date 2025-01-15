using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.OData.Client;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Csdl;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000005 RID: 5
	[OriginalName("Container")]
	public class Container : DataServiceContext
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public Container(Uri serviceRoot)
			: base(serviceRoot, ODataProtocolVersion.V4)
		{
			base.ResolveName = new Func<Type, string>(this.ResolveNameFromType);
			base.ResolveType = new Func<string, Type>(this.ResolveTypeFromName);
			base.Format.LoadServiceModel = new Func<IEdmModel>(Container.GeneratedEdmModel.GetInstance);
			base.Format.UseJson();
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020AC File Offset: 0x000002AC
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected Type ResolveTypeFromName(string typeName)
		{
			Type type = base.DefaultResolveType(typeName, "Model", "Microsoft.ReportingServices.Portal.ODataClient.V2");
			if (type != null)
			{
				return type;
			}
			return null;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020D8 File Offset: 0x000002D8
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected string ResolveNameFromType(Type clientType)
		{
			OriginalNameAttribute originalNameAttribute = (OriginalNameAttribute)Utility.GetCustomAttributes(clientType, typeof(OriginalNameAttribute), true).SingleOrDefault<object>();
			if (clientType.Namespace.Equals("Microsoft.ReportingServices.Portal.ODataClient.V2", StringComparison.Ordinal))
			{
				if (originalNameAttribute != null)
				{
					return "Model." + originalNameAttribute.OriginalName;
				}
				return "Model." + clientType.Name;
			}
			else
			{
				if (originalNameAttribute != null)
				{
					return clientType.Namespace + "." + originalNameAttribute.OriginalName;
				}
				return clientType.FullName;
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002159 File Offset: 0x00000359
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("AlertSubscriptions")]
		public DataServiceQuery<AlertSubscription> AlertSubscriptions
		{
			get
			{
				if (this._AlertSubscriptions == null)
				{
					this._AlertSubscriptions = base.CreateQuery<AlertSubscription>("AlertSubscriptions");
				}
				return this._AlertSubscriptions;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x0000217A File Offset: 0x0000037A
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("CacheRefreshPlans")]
		public DataServiceQuery<CacheRefreshPlan> CacheRefreshPlans
		{
			get
			{
				if (this._CacheRefreshPlans == null)
				{
					this._CacheRefreshPlans = base.CreateQuery<CacheRefreshPlan>("CacheRefreshPlans");
				}
				return this._CacheRefreshPlans;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x0000219B File Offset: 0x0000039B
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("CacheRefreshPlanHistory")]
		public DataServiceQuery<SubscriptionHistory> CacheRefreshPlanHistory
		{
			get
			{
				if (this._CacheRefreshPlanHistory == null)
				{
					this._CacheRefreshPlanHistory = base.CreateQuery<SubscriptionHistory>("CacheRefreshPlanHistory");
				}
				return this._CacheRefreshPlanHistory;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000021BC File Offset: 0x000003BC
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("CatalogItems")]
		public DataServiceQuery<CatalogItem> CatalogItems
		{
			get
			{
				if (this._CatalogItems == null)
				{
					this._CatalogItems = base.CreateQuery<CatalogItem>("CatalogItems");
				}
				return this._CatalogItems;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000021DD File Offset: 0x000003DD
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Comments")]
		public DataServiceQuery<Comment> Comments
		{
			get
			{
				if (this._Comments == null)
				{
					this._Comments = base.CreateQuery<Comment>("Comments");
				}
				return this._Comments;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000021FE File Offset: 0x000003FE
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("DataSets")]
		public DataServiceQuery<DataSet> DataSets
		{
			get
			{
				if (this._DataSets == null)
				{
					this._DataSets = base.CreateQuery<DataSet>("DataSets");
				}
				return this._DataSets;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000A RID: 10 RVA: 0x0000221F File Offset: 0x0000041F
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("DataSetData")]
		public DataServiceQuery<DataSetRow> DataSetData
		{
			get
			{
				if (this._DataSetData == null)
				{
					this._DataSetData = base.CreateQuery<DataSetRow>("DataSetData");
				}
				return this._DataSetData;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002240 File Offset: 0x00000440
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("DataSources")]
		public DataServiceQuery<DataSource> DataSources
		{
			get
			{
				if (this._DataSources == null)
				{
					this._DataSources = base.CreateQuery<DataSource>("DataSources");
				}
				return this._DataSources;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002261 File Offset: 0x00000461
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("ExcelWorkbooks")]
		public DataServiceQuery<ExcelWorkbook> ExcelWorkbooks
		{
			get
			{
				if (this._ExcelWorkbooks == null)
				{
					this._ExcelWorkbooks = base.CreateQuery<ExcelWorkbook>("ExcelWorkbooks");
				}
				return this._ExcelWorkbooks;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002282 File Offset: 0x00000482
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Extensions")]
		public DataServiceQuery<Extension> Extensions
		{
			get
			{
				if (this._Extensions == null)
				{
					this._Extensions = base.CreateQuery<Extension>("Extensions");
				}
				return this._Extensions;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000022A3 File Offset: 0x000004A3
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("FavoriteItems")]
		public DataServiceQuery<FavoriteItem> FavoriteItems
		{
			get
			{
				if (this._FavoriteItems == null)
				{
					this._FavoriteItems = base.CreateQuery<FavoriteItem>("FavoriteItems");
				}
				return this._FavoriteItems;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000022C4 File Offset: 0x000004C4
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Folders")]
		public DataServiceQuery<Folder> Folders
		{
			get
			{
				if (this._Folders == null)
				{
					this._Folders = base.CreateQuery<Folder>("Folders");
				}
				return this._Folders;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000022E5 File Offset: 0x000004E5
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Kpis")]
		public DataServiceQuery<Kpi> Kpis
		{
			get
			{
				if (this._Kpis == null)
				{
					this._Kpis = base.CreateQuery<Kpi>("Kpis");
				}
				return this._Kpis;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002306 File Offset: 0x00000506
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("LinkedReports")]
		public DataServiceQuery<LinkedReport> LinkedReports
		{
			get
			{
				if (this._LinkedReports == null)
				{
					this._LinkedReports = base.CreateQuery<LinkedReport>("LinkedReports");
				}
				return this._LinkedReports;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002327 File Offset: 0x00000527
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("MobileReports")]
		public DataServiceQuery<MobileReport> MobileReports
		{
			get
			{
				if (this._MobileReports == null)
				{
					this._MobileReports = base.CreateQuery<MobileReport>("MobileReports");
				}
				return this._MobileReports;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002348 File Offset: 0x00000548
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Notifications")]
		public DataServiceQuery<Notification> Notifications
		{
			get
			{
				if (this._Notifications == null)
				{
					this._Notifications = base.CreateQuery<Notification>("Notifications");
				}
				return this._Notifications;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002369 File Offset: 0x00000569
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("PowerBIReports")]
		public DataServiceQuery<PowerBIReport> PowerBIReports
		{
			get
			{
				if (this._PowerBIReports == null)
				{
					this._PowerBIReports = base.CreateQuery<PowerBIReport>("PowerBIReports");
				}
				return this._PowerBIReports;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000015 RID: 21 RVA: 0x0000238A File Offset: 0x0000058A
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Reports")]
		public DataServiceQuery<Report> Reports
		{
			get
			{
				if (this._Reports == null)
				{
					this._Reports = base.CreateQuery<Report>("Reports");
				}
				return this._Reports;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000023AB File Offset: 0x000005AB
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("ParameterDefinitions")]
		public DataServiceQuery<ReportParameterDefinition> ParameterDefinitions
		{
			get
			{
				if (this._ParameterDefinitions == null)
				{
					this._ParameterDefinitions = base.CreateQuery<ReportParameterDefinition>("ParameterDefinitions");
				}
				return this._ParameterDefinitions;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000023CC File Offset: 0x000005CC
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Resources")]
		public DataServiceQuery<Resource> Resources
		{
			get
			{
				if (this._Resources == null)
				{
					this._Resources = base.CreateQuery<Resource>("Resources");
				}
				return this._Resources;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000023ED File Offset: 0x000005ED
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Schedules")]
		public DataServiceQuery<Schedule> Schedules
		{
			get
			{
				if (this._Schedules == null)
				{
					this._Schedules = base.CreateQuery<Schedule>("Schedules");
				}
				return this._Schedules;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000019 RID: 25 RVA: 0x0000240E File Offset: 0x0000060E
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Subscriptions")]
		public DataServiceQuery<Subscription> Subscriptions
		{
			get
			{
				if (this._Subscriptions == null)
				{
					this._Subscriptions = base.CreateQuery<Subscription>("Subscriptions");
				}
				return this._Subscriptions;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600001A RID: 26 RVA: 0x0000242F File Offset: 0x0000062F
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("SystemResources")]
		public DataServiceQuery<SystemResource> SystemResources
		{
			get
			{
				if (this._SystemResources == null)
				{
					this._SystemResources = base.CreateQuery<SystemResource>("SystemResources");
				}
				return this._SystemResources;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002450 File Offset: 0x00000650
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("SystemResourceItems")]
		public DataServiceQuery<SystemResourceItem> SystemResourceItems
		{
			get
			{
				if (this._SystemResourceItems == null)
				{
					this._SystemResourceItems = base.CreateQuery<SystemResourceItem>("SystemResourceItems");
				}
				return this._SystemResourceItems;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002471 File Offset: 0x00000671
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("UserSettings")]
		public DataServiceQuery<UserSettings> UserSettings
		{
			get
			{
				if (this._UserSettings == null)
				{
					this._UserSettings = base.CreateQuery<UserSettings>("UserSettings");
				}
				return this._UserSettings;
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002492 File Offset: 0x00000692
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public void AddToAlertSubscriptions(AlertSubscription alertSubscription)
		{
			base.AddObject("AlertSubscriptions", alertSubscription);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000024A0 File Offset: 0x000006A0
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public void AddToCacheRefreshPlans(CacheRefreshPlan cacheRefreshPlan)
		{
			base.AddObject("CacheRefreshPlans", cacheRefreshPlan);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000024AE File Offset: 0x000006AE
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public void AddToCacheRefreshPlanHistory(SubscriptionHistory subscriptionHistory)
		{
			base.AddObject("CacheRefreshPlanHistory", subscriptionHistory);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000024BC File Offset: 0x000006BC
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public void AddToCatalogItems(CatalogItem catalogItem)
		{
			base.AddObject("CatalogItems", catalogItem);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000024CA File Offset: 0x000006CA
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public void AddToComments(Comment comment)
		{
			base.AddObject("Comments", comment);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000024D8 File Offset: 0x000006D8
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public void AddToDataSets(DataSet dataSet)
		{
			base.AddObject("DataSets", dataSet);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000024E6 File Offset: 0x000006E6
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public void AddToDataSetData(DataSetRow dataSetRow)
		{
			base.AddObject("DataSetData", dataSetRow);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000024F4 File Offset: 0x000006F4
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public void AddToDataSources(DataSource dataSource)
		{
			base.AddObject("DataSources", dataSource);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002502 File Offset: 0x00000702
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public void AddToExcelWorkbooks(ExcelWorkbook excelWorkbook)
		{
			base.AddObject("ExcelWorkbooks", excelWorkbook);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002510 File Offset: 0x00000710
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public void AddToExtensions(Extension extension)
		{
			base.AddObject("Extensions", extension);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000251E File Offset: 0x0000071E
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public void AddToFavoriteItems(FavoriteItem favoriteItem)
		{
			base.AddObject("FavoriteItems", favoriteItem);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000252C File Offset: 0x0000072C
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public void AddToFolders(Folder folder)
		{
			base.AddObject("Folders", folder);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000253A File Offset: 0x0000073A
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public void AddToKpis(Kpi kpi)
		{
			base.AddObject("Kpis", kpi);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002548 File Offset: 0x00000748
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public void AddToLinkedReports(LinkedReport linkedReport)
		{
			base.AddObject("LinkedReports", linkedReport);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002556 File Offset: 0x00000756
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public void AddToMobileReports(MobileReport mobileReport)
		{
			base.AddObject("MobileReports", mobileReport);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002564 File Offset: 0x00000764
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public void AddToNotifications(Notification notification)
		{
			base.AddObject("Notifications", notification);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002572 File Offset: 0x00000772
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public void AddToPowerBIReports(PowerBIReport powerBIReport)
		{
			base.AddObject("PowerBIReports", powerBIReport);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002580 File Offset: 0x00000780
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public void AddToReports(Report report)
		{
			base.AddObject("Reports", report);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000258E File Offset: 0x0000078E
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public void AddToParameterDefinitions(ReportParameterDefinition reportParameterDefinition)
		{
			base.AddObject("ParameterDefinitions", reportParameterDefinition);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000259C File Offset: 0x0000079C
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public void AddToResources(Resource resource)
		{
			base.AddObject("Resources", resource);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000025AA File Offset: 0x000007AA
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public void AddToSchedules(Schedule schedule)
		{
			base.AddObject("Schedules", schedule);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000025B8 File Offset: 0x000007B8
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public void AddToSubscriptions(Subscription subscription)
		{
			base.AddObject("Subscriptions", subscription);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000025C6 File Offset: 0x000007C6
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public void AddToSystemResources(SystemResource systemResource)
		{
			base.AddObject("SystemResources", systemResource);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000025D4 File Offset: 0x000007D4
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public void AddToSystemResourceItems(SystemResourceItem systemResourceItem)
		{
			base.AddObject("SystemResourceItems", systemResourceItem);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000025E2 File Offset: 0x000007E2
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public void AddToUserSettings(UserSettings userSettings)
		{
			base.AddObject("UserSettings", userSettings);
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000036 RID: 54 RVA: 0x000025F0 File Offset: 0x000007F0
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("System")]
		public SystemSingle System
		{
			get
			{
				if (this._System == null)
				{
					this._System = new SystemSingle(this, "System");
				}
				return this._System;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002611 File Offset: 0x00000811
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Me")]
		public UserSingle Me
		{
			get
			{
				if (this._Me == null)
				{
					this._Me = new UserSingle(this, "Me");
				}
				return this._Me;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002632 File Offset: 0x00000832
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Telemetry")]
		public TelemetrySingle Telemetry
		{
			get
			{
				if (this._Telemetry == null)
				{
					this._Telemetry = new TelemetrySingle(this, "Telemetry");
				}
				return this._Telemetry;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002653 File Offset: 0x00000853
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("PowerBIIntegration")]
		public PowerBIUserInfoSingle PowerBIIntegration
		{
			get
			{
				if (this._PowerBIIntegration == null)
				{
					this._PowerBIIntegration = new PowerBIUserInfoSingle(this, "PowerBIIntegration");
				}
				return this._PowerBIIntegration;
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002674 File Offset: 0x00000874
		[OriginalName("ServiceState")]
		public DataServiceQuerySingle<ServiceState> ServiceState()
		{
			return base.CreateFunctionQuerySingle<ServiceState>("", "ServiceState", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000268C File Offset: 0x0000088C
		[OriginalName("AllowedActions")]
		public DataServiceQuery<string> AllowedActions(string path)
		{
			return base.CreateFunctionQuery<string>("", "AllowedActions", false, new UriOperationParameter[]
			{
				new UriOperationParameter("path", path)
			});
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000026C0 File Offset: 0x000008C0
		[OriginalName("SafeGetSystemResourceContent")]
		public DataServiceQuerySingle<byte[]> SafeGetSystemResourceContent(string type, string key)
		{
			return base.CreateFunctionQuerySingle<byte[]>("", "SafeGetSystemResourceContent", false, new UriOperationParameter[]
			{
				new UriOperationParameter("type", type),
				new UriOperationParameter("key", key)
			});
		}

		// Token: 0x04000035 RID: 53
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<AlertSubscription> _AlertSubscriptions;

		// Token: 0x04000036 RID: 54
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<CacheRefreshPlan> _CacheRefreshPlans;

		// Token: 0x04000037 RID: 55
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<SubscriptionHistory> _CacheRefreshPlanHistory;

		// Token: 0x04000038 RID: 56
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<CatalogItem> _CatalogItems;

		// Token: 0x04000039 RID: 57
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<Comment> _Comments;

		// Token: 0x0400003A RID: 58
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<DataSet> _DataSets;

		// Token: 0x0400003B RID: 59
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<DataSetRow> _DataSetData;

		// Token: 0x0400003C RID: 60
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<DataSource> _DataSources;

		// Token: 0x0400003D RID: 61
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<ExcelWorkbook> _ExcelWorkbooks;

		// Token: 0x0400003E RID: 62
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<Extension> _Extensions;

		// Token: 0x0400003F RID: 63
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<FavoriteItem> _FavoriteItems;

		// Token: 0x04000040 RID: 64
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<Folder> _Folders;

		// Token: 0x04000041 RID: 65
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<Kpi> _Kpis;

		// Token: 0x04000042 RID: 66
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<LinkedReport> _LinkedReports;

		// Token: 0x04000043 RID: 67
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<MobileReport> _MobileReports;

		// Token: 0x04000044 RID: 68
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<Notification> _Notifications;

		// Token: 0x04000045 RID: 69
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<PowerBIReport> _PowerBIReports;

		// Token: 0x04000046 RID: 70
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<Report> _Reports;

		// Token: 0x04000047 RID: 71
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<ReportParameterDefinition> _ParameterDefinitions;

		// Token: 0x04000048 RID: 72
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<Resource> _Resources;

		// Token: 0x04000049 RID: 73
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<Schedule> _Schedules;

		// Token: 0x0400004A RID: 74
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<Subscription> _Subscriptions;

		// Token: 0x0400004B RID: 75
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<SystemResource> _SystemResources;

		// Token: 0x0400004C RID: 76
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<SystemResourceItem> _SystemResourceItems;

		// Token: 0x0400004D RID: 77
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<UserSettings> _UserSettings;

		// Token: 0x0400004E RID: 78
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private SystemSingle _System;

		// Token: 0x0400004F RID: 79
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private UserSingle _Me;

		// Token: 0x04000050 RID: 80
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private TelemetrySingle _Telemetry;

		// Token: 0x04000051 RID: 81
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private PowerBIUserInfoSingle _PowerBIIntegration;

		// Token: 0x02000135 RID: 309
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private abstract class GeneratedEdmModel
		{
			// Token: 0x06000D5F RID: 3423 RVA: 0x0001B016 File Offset: 0x00019216
			[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
			public static IEdmModel GetInstance()
			{
				return Container.GeneratedEdmModel.ParsedModel;
			}

			// Token: 0x06000D60 RID: 3424 RVA: 0x0001B020 File Offset: 0x00019220
			[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
			private static IEdmModel LoadModelFromString()
			{
				XmlReader xmlReader = Container.GeneratedEdmModel.CreateXmlReader("<edmx:Edmx Version=\"4.0\" xmlns:edmx=\"http://docs.oasis-open.org/odata/ns/edmx\">\r\n  <edmx:DataServices>\r\n    <Schema Namespace=\"Model\" xmlns=\"http://docs.oasis-open.org/odata/ns/edm\">\r\n      <ComplexType Name=\"KpiDataItem\" Abstract=\"true\">\r\n        <Property Name=\"Type\" Type=\"Model.KpiDataItemType\" Nullable=\"false\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"KpiSharedDataItem\" BaseType=\"Model.KpiDataItem\">\r\n        <Property Name=\"Id\" Type=\"Edm.Guid\" Nullable=\"false\" />\r\n        <Property Name=\"Path\" Type=\"Edm.String\" />\r\n        <Property Name=\"Parameters\" Type=\"Collection(Model.DataSetParameter)\" Nullable=\"false\" />\r\n        <Property Name=\"Aggregation\" Type=\"Model.KpiSharedDataItemAggregation\" Nullable=\"false\" />\r\n        <Property Name=\"Column\" Type=\"Edm.String\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"DataSetParameter\">\r\n        <Property Name=\"Name\" Type=\"Edm.String\" />\r\n        <Property Name=\"Value\" Type=\"Edm.String\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"KpiStaticDataItem\" BaseType=\"Model.KpiDataItem\">\r\n        <Property Name=\"Value\" Type=\"Edm.String\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"DrillthroughTarget\" Abstract=\"true\">\r\n        <Property Name=\"Type\" Type=\"Model.DrillthroughTargetType\" Nullable=\"false\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"CatalogItemDrillthroughTarget\" BaseType=\"Model.DrillthroughTarget\">\r\n        <Property Name=\"CatalogItemType\" Type=\"Model.CatalogItemType\" Nullable=\"false\" />\r\n        <Property Name=\"Path\" Type=\"Edm.String\" />\r\n        <Property Name=\"Id\" Type=\"Edm.Guid\" Nullable=\"false\" />\r\n        <Property Name=\"Parameters\" Type=\"Collection(Model.CatalogItemParameter)\" Nullable=\"false\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"CatalogItemParameter\">\r\n        <Property Name=\"Name\" Type=\"Edm.String\" />\r\n        <Property Name=\"Value\" Type=\"Edm.String\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"UrlDrillthroughTarget\" BaseType=\"Model.DrillthroughTarget\">\r\n        <Property Name=\"Url\" Type=\"Edm.String\" />\r\n        <Property Name=\"DirectNavigation\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n      </ComplexType>\r\n      <EntityType Name=\"AlertSubscription\">\r\n        <Key>\r\n          <PropertyRef Name=\"Id\" />\r\n        </Key>\r\n        <Property Name=\"Id\" Type=\"Edm.Int64\" Nullable=\"false\" />\r\n        <Property Name=\"ItemId\" Type=\"Edm.Guid\" Nullable=\"false\" />\r\n        <Property Name=\"AlertType\" Type=\"Edm.String\" Nullable=\"false\" />\r\n      </EntityType>\r\n      <EntityType Name=\"CacheRefreshPlan\">\r\n        <Key>\r\n          <PropertyRef Name=\"Id\" />\r\n        </Key>\r\n        <Property Name=\"Id\" Type=\"Edm.Guid\" Nullable=\"false\" />\r\n        <Property Name=\"Owner\" Type=\"Edm.String\" />\r\n        <Property Name=\"Description\" Type=\"Edm.String\" />\r\n        <Property Name=\"CatalogItemPath\" Type=\"Edm.String\" />\r\n        <Property Name=\"EventType\" Type=\"Edm.String\" />\r\n        <Property Name=\"Schedule\" Type=\"Model.ScheduleReference\" />\r\n        <Property Name=\"ScheduleDescription\" Type=\"Edm.String\" />\r\n        <Property Name=\"LastRunTime\" Type=\"Edm.DateTimeOffset\" />\r\n        <Property Name=\"LastStatus\" Type=\"Edm.String\" />\r\n        <Property Name=\"ModifiedBy\" Type=\"Edm.String\" />\r\n        <Property Name=\"ModifiedDate\" Type=\"Edm.DateTimeOffset\" />\r\n        <Property Name=\"ParameterValues\" Type=\"Collection(Model.ParameterValue)\" />\r\n        <NavigationProperty Name=\"History\" Type=\"Collection(Model.SubscriptionHistory)\" />\r\n      </EntityType>\r\n      <EntityType Name=\"SubscriptionHistory\">\r\n        <Key>\r\n          <PropertyRef Name=\"Id\" />\r\n        </Key>\r\n        <Property Name=\"Id\" Type=\"Edm.Int32\" Nullable=\"false\" />\r\n        <Property Name=\"SubscriptionID\" Type=\"Edm.Guid\" Nullable=\"false\" />\r\n        <Property Name=\"Type\" Type=\"Model.SubscriptionExecutionType\" Nullable=\"false\" />\r\n        <Property Name=\"StartTime\" Type=\"Edm.DateTimeOffset\" Nullable=\"false\" />\r\n        <Property Name=\"EndTime\" Type=\"Edm.DateTimeOffset\" Nullable=\"false\" />\r\n        <Property Name=\"Status\" Type=\"Model.SubscriptionStatus\" Nullable=\"false\" />\r\n        <Property Name=\"Message\" Type=\"Edm.String\" />\r\n        <Property Name=\"Details\" Type=\"Edm.String\" />\r\n      </EntityType>\r\n      <EntityType Name=\"Property\">\r\n        <Key>\r\n          <PropertyRef Name=\"Name\" />\r\n        </Key>\r\n        <Property Name=\"Name\" Type=\"Edm.String\" Nullable=\"false\" />\r\n        <Property Name=\"Value\" Type=\"Edm.String\" />\r\n      </EntityType>\r\n      <EntityType Name=\"HistorySnapshotOptions\">\r\n        <Key>\r\n          <PropertyRef Name=\"CatalogItemId\" />\r\n        </Key>\r\n        <Property Name=\"CatalogItemId\" Type=\"Edm.Guid\" Nullable=\"false\" />\r\n        <Property Name=\"HistorySnapshotsOptions\" Type=\"Model.ReportHistorySnapshotsOptions\" />\r\n      </EntityType>\r\n      <EntityType Name=\"CacheOptions\">\r\n        <Key>\r\n          <PropertyRef Name=\"Id\" />\r\n        </Key>\r\n        <Property Name=\"Id\" Type=\"Edm.Guid\" Nullable=\"false\" />\r\n        <Property Name=\"ExecutionType\" Type=\"Model.ItemExecutionType\" Nullable=\"false\" />\r\n        <Property Name=\"Expiration\" Type=\"Model.ExpirationReference\" />\r\n      </EntityType>\r\n      <EntityType Name=\"CatalogItem\" Abstract=\"true\">\r\n        <Key>\r\n          <PropertyRef Name=\"Id\" />\r\n        </Key>\r\n        <Property Name=\"Id\" Type=\"Edm.Guid\" Nullable=\"false\" />\r\n        <Property Name=\"Name\" Type=\"Edm.String\" />\r\n        <Property Name=\"Description\" Type=\"Edm.String\" />\r\n        <Property Name=\"Path\" Type=\"Edm.String\" />\r\n        <Property Name=\"Type\" Type=\"Model.CatalogItemType\" Nullable=\"false\" />\r\n        <Property Name=\"Hidden\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"Size\" Type=\"Edm.Int64\" Nullable=\"false\" />\r\n        <Property Name=\"ModifiedBy\" Type=\"Edm.String\" />\r\n        <Property Name=\"ModifiedDate\" Type=\"Edm.DateTimeOffset\" Nullable=\"false\" />\r\n        <Property Name=\"CreatedBy\" Type=\"Edm.String\" />\r\n        <Property Name=\"CreatedDate\" Type=\"Edm.DateTimeOffset\" Nullable=\"false\" />\r\n        <Property Name=\"ParentFolderId\" Type=\"Edm.Guid\" />\r\n        <Property Name=\"IsFavorite\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"Roles\" Type=\"Collection(Model.Role)\" />\r\n        <Property Name=\"ContentType\" Type=\"Edm.String\" />\r\n        <Property Name=\"Content\" Type=\"Edm.Binary\" />\r\n        <NavigationProperty Name=\"ParentFolder\" Type=\"Model.Folder\" />\r\n        <NavigationProperty Name=\"Properties\" Type=\"Collection(Model.Property)\" />\r\n        <NavigationProperty Name=\"Comments\" Type=\"Collection(Model.Comment)\" />\r\n        <NavigationProperty Name=\"AlertSubscriptions\" Type=\"Collection(Model.AlertSubscription)\" />\r\n        <NavigationProperty Name=\"AllowedActions\" Type=\"Collection(Model.AllowedAction)\" />\r\n        <NavigationProperty Name=\"Policies\" Type=\"Collection(Model.ItemPolicy)\" />\r\n        <NavigationProperty Name=\"DependentItems\" Type=\"Collection(Model.CatalogItem)\" />\r\n        <Annotation Term=\"OData.Community.Keys.V1.AlternateKeys\">\r\n          <Collection>\r\n            <Record Type=\"OData.Community.Keys.V1.AlternateKey\">\r\n              <PropertyValue Property=\"Key\">\r\n                <Collection>\r\n                  <Record Type=\"OData.Community.Keys.V1.PropertyRef\">\r\n                    <PropertyValue Property=\"Alias\" String=\"Path\" />\r\n                    <PropertyValue Property=\"Name\" PropertyPath=\"Path\" />\r\n                  </Record>\r\n                </Collection>\r\n              </PropertyValue>\r\n            </Record>\r\n          </Collection>\r\n        </Annotation>\r\n      </EntityType>\r\n      <ComplexType Name=\"BulkOperationsResult\">\r\n        <Property Name=\"FailedOperations\" Type=\"Collection(Edm.String)\" />\r\n        <Property Name=\"HasErrors\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"CatalogItemAccessToken\">\r\n        <Property Name=\"Token\" Type=\"Edm.Binary\" />\r\n      </ComplexType>\r\n      <EntityType Name=\"ItemPolicy\">\r\n        <Key>\r\n          <PropertyRef Name=\"Id\" />\r\n        </Key>\r\n        <Property Name=\"Id\" Type=\"Edm.Guid\" Nullable=\"false\" />\r\n        <Property Name=\"InheritParentPolicy\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"Policies\" Type=\"Collection(Model.Policy)\" />\r\n      </EntityType>\r\n      <EntityType Name=\"Comment\">\r\n        <Key>\r\n          <PropertyRef Name=\"Id\" />\r\n        </Key>\r\n        <Property Name=\"Id\" Type=\"Edm.Int64\" Nullable=\"false\" />\r\n        <Property Name=\"ItemId\" Type=\"Edm.Guid\" Nullable=\"false\" />\r\n        <Property Name=\"UserName\" Type=\"Edm.String\" />\r\n        <Property Name=\"ThreadId\" Type=\"Edm.Int64\" />\r\n        <Property Name=\"AttachmentPath\" Type=\"Edm.String\" />\r\n        <Property Name=\"Text\" Type=\"Edm.String\" Nullable=\"false\" />\r\n        <Property Name=\"CreatedDate\" Type=\"Edm.DateTimeOffset\" Nullable=\"false\" />\r\n        <Property Name=\"ModifiedDate\" Type=\"Edm.DateTimeOffset\" />\r\n      </EntityType>\r\n      <EntityType Name=\"DataSet\" BaseType=\"Model.CatalogItem\">\r\n        <Property Name=\"HasParameters\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"QueryExecutionTimeOut\" Type=\"Edm.Int32\" Nullable=\"false\" />\r\n        <NavigationProperty Name=\"Data\" Type=\"Collection(Model.DataSetRow)\" />\r\n        <NavigationProperty Name=\"DataSources\" Type=\"Collection(Model.DataSource)\" />\r\n        <NavigationProperty Name=\"CacheRefreshPlans\" Type=\"Collection(Model.CacheRefreshPlan)\" />\r\n        <NavigationProperty Name=\"ParameterDefinitions\" Type=\"Collection(Model.ReportParameterDefinition)\" />\r\n        <NavigationProperty Name=\"CacheOptions\" Type=\"Model.CacheOptions\" />\r\n      </EntityType>\r\n      <ComplexType Name=\"DataSetSchema\">\r\n        <Property Name=\"Name\" Type=\"Edm.String\" />\r\n        <Property Name=\"Fields\" Type=\"Collection(Model.DataSetField)\" />\r\n        <Property Name=\"Parameters\" Type=\"Collection(Model.DataSetParameterInfo)\" />\r\n      </ComplexType>\r\n      <EntityType Name=\"DataSetRow\" OpenType=\"true\">\r\n        <Key>\r\n          <PropertyRef Name=\"Id\" />\r\n        </Key>\r\n        <Property Name=\"Id\" Type=\"Edm.Guid\" Nullable=\"false\" />\r\n      </EntityType>\r\n      <EntityType Name=\"DataSource\" BaseType=\"Model.CatalogItem\">\r\n        <Property Name=\"IsEnabled\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"ConnectionString\" Type=\"Edm.String\" />\r\n        <Property Name=\"DataSourceType\" Type=\"Edm.String\" />\r\n        <Property Name=\"IsOriginalConnectionStringExpressionBased\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"IsConnectionStringOverridden\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"CredentialRetrieval\" Type=\"Model.CredentialRetrievalType\" Nullable=\"false\" />\r\n        <Property Name=\"CredentialsByUser\" Type=\"Model.CredentialsSuppliedByUser\" />\r\n        <Property Name=\"CredentialsInServer\" Type=\"Model.CredentialsStoredInServer\" />\r\n        <Property Name=\"IsReference\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"DataSourceSubType\" Type=\"Edm.String\" />\r\n        <Property Name=\"DataModelDataSource\" Type=\"Model.DataModelDataSource\" />\r\n        <NavigationProperty Name=\"Subscriptions\" Type=\"Collection(Model.Subscription)\" />\r\n      </EntityType>\r\n      <ComplexType Name=\"DataSourceCheckResult\">\r\n        <Property Name=\"IsSuccessful\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"ErrorMessage\" Type=\"Edm.String\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"Query\">\r\n        <Property Name=\"CommandText\" Type=\"Edm.String\" />\r\n        <Property Name=\"Timeout\" Type=\"Edm.Int32\" />\r\n      </ComplexType>\r\n      <EntityType Name=\"ExcelWorkbook\" BaseType=\"Model.CatalogItem\" />\r\n      <EntityType Name=\"Extension\">\r\n        <Key>\r\n          <PropertyRef Name=\"Name\" />\r\n        </Key>\r\n        <Property Name=\"ExtensionType\" Type=\"Model.ExtensionType\" Nullable=\"false\" />\r\n        <Property Name=\"Name\" Type=\"Edm.String\" Nullable=\"false\" />\r\n        <Property Name=\"LocalizedName\" Type=\"Edm.String\" />\r\n        <Property Name=\"Visible\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"Parameters\" Type=\"Collection(Model.ExtensionParameter)\" />\r\n      </EntityType>\r\n      <ComplexType Name=\"ExtensionParameter\">\r\n        <Property Name=\"Name\" Type=\"Edm.String\" />\r\n        <Property Name=\"DisplayName\" Type=\"Edm.String\" />\r\n        <Property Name=\"Required\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"ReadOnly\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"Value\" Type=\"Edm.String\" />\r\n        <Property Name=\"Error\" Type=\"Edm.String\" />\r\n        <Property Name=\"Encrypted\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"IsPassword\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"ValidValues\" Type=\"Collection(Model.ValidValue)\" />\r\n        <Property Name=\"ValidValuesIsNull\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"ParameterValue\">\r\n        <Property Name=\"Name\" Type=\"Edm.String\" />\r\n        <Property Name=\"Value\" Type=\"Edm.String\" />\r\n        <Property Name=\"IsValueFieldReference\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n      </ComplexType>\r\n      <EntityType Name=\"FavoriteItem\">\r\n        <Key>\r\n          <PropertyRef Name=\"Id\" />\r\n        </Key>\r\n        <Property Name=\"Id\" Type=\"Edm.Guid\" Nullable=\"false\" />\r\n        <NavigationProperty Name=\"Item\" Type=\"Model.CatalogItem\" />\r\n      </EntityType>\r\n      <EntityType Name=\"Folder\" BaseType=\"Model.CatalogItem\">\r\n        <NavigationProperty Name=\"CatalogItems\" Type=\"Collection(Model.CatalogItem)\" />\r\n      </EntityType>\r\n      <EntityType Name=\"Kpi\" BaseType=\"Model.CatalogItem\">\r\n        <Property Name=\"ValueFormat\" Type=\"Model.KpiValueFormat\" Nullable=\"false\" />\r\n        <Property Name=\"Visualization\" Type=\"Model.KpiVisualization\" Nullable=\"false\" />\r\n        <Property Name=\"DrillthroughTarget\" Type=\"Model.DrillthroughTarget\" />\r\n        <Property Name=\"Currency\" Type=\"Edm.String\" />\r\n        <Property Name=\"Values\" Type=\"Model.KpiValues\" />\r\n        <Property Name=\"Data\" Type=\"Model.KpiData\" />\r\n      </EntityType>\r\n      <EntityType Name=\"LinkedReport\" BaseType=\"Model.CatalogItem\">\r\n        <Property Name=\"HasParameters\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"Link\" Type=\"Edm.String\" />\r\n        <NavigationProperty Name=\"Subscriptions\" Type=\"Collection(Model.Subscription)\" />\r\n        <NavigationProperty Name=\"CacheRefreshPlans\" Type=\"Collection(Model.CacheRefreshPlan)\" />\r\n        <NavigationProperty Name=\"HistorySnapshotOptions\" Type=\"Model.HistorySnapshotOptions\" />\r\n        <NavigationProperty Name=\"ReportHistorySnapshots\" Type=\"Collection(Model.ReportHistorySnapshot)\" />\r\n        <NavigationProperty Name=\"HistorySnapshots\" Type=\"Collection(Model.HistorySnapshot)\" />\r\n        <NavigationProperty Name=\"ParameterDefinitions\" Type=\"Collection(Model.ReportParameterDefinition)\" />\r\n        <NavigationProperty Name=\"CacheOptions\" Type=\"Model.CacheOptions\" />\r\n      </EntityType>\r\n      <EntityType Name=\"MobileReport\" BaseType=\"Model.CatalogItem\">\r\n        <Property Name=\"AllowCaching\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"Manifest\" Type=\"Model.MobileReportManifest\" />\r\n        <Property Name=\"HasSharedDataSets\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <NavigationProperty Name=\"SharedDataSets\" Type=\"Collection(Model.DataSet)\" />\r\n      </EntityType>\r\n      <EntityType Name=\"Notification\">\r\n        <Key>\r\n          <PropertyRef Name=\"Id\" />\r\n        </Key>\r\n        <Property Name=\"Id\" Type=\"Edm.Guid\" Nullable=\"false\" />\r\n        <Property Name=\"IssueType\" Type=\"Model.IssueType\" Nullable=\"false\" />\r\n      </EntityType>\r\n      <EntityType Name=\"PowerBIReport\" BaseType=\"Model.CatalogItem\">\r\n        <Property Name=\"HasDataSources\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <NavigationProperty Name=\"DataSources\" Type=\"Collection(Model.DataSource)\" />\r\n        <NavigationProperty Name=\"DataModelParameters\" Type=\"Collection(Model.DataModelParameter)\" />\r\n        <NavigationProperty Name=\"CacheRefreshPlans\" Type=\"Collection(Model.CacheRefreshPlan)\" />\r\n        <NavigationProperty Name=\"DataModelRoleAssignments\" Type=\"Collection(Model.DataModelRoleAssignment)\" />\r\n        <NavigationProperty Name=\"DataModelRoles\" Type=\"Collection(Model.DataModelRole)\" />\r\n      </EntityType>\r\n      <EntityType Name=\"DataModelRole\">\r\n        <Key>\r\n          <PropertyRef Name=\"ModelRoleId\" />\r\n        </Key>\r\n        <Property Name=\"ModelRoleId\" Type=\"Edm.Guid\" Nullable=\"false\" />\r\n        <Property Name=\"ModelRoleName\" Type=\"Edm.String\" />\r\n      </EntityType>\r\n      <EntityType Name=\"DataModelRoleAssignment\">\r\n        <Key>\r\n          <PropertyRef Name=\"GroupUserName\" />\r\n        </Key>\r\n        <Property Name=\"GroupUserName\" Type=\"Edm.String\" Nullable=\"false\" />\r\n        <Property Name=\"DataModelRoles\" Type=\"Collection(Edm.Guid)\" Nullable=\"false\" />\r\n      </EntityType>\r\n      <EntityType Name=\"Report\" BaseType=\"Model.CatalogItem\">\r\n        <Property Name=\"HasDataSources\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"HasSharedDataSets\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"HasParameters\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <NavigationProperty Name=\"Subscriptions\" Type=\"Collection(Model.Subscription)\" />\r\n        <NavigationProperty Name=\"CacheRefreshPlans\" Type=\"Collection(Model.CacheRefreshPlan)\" />\r\n        <NavigationProperty Name=\"DataSources\" Type=\"Collection(Model.DataSource)\" />\r\n        <NavigationProperty Name=\"SharedDataSets\" Type=\"Collection(Model.DataSet)\" />\r\n        <NavigationProperty Name=\"HistorySnapshotOptions\" Type=\"Model.HistorySnapshotOptions\" />\r\n        <NavigationProperty Name=\"ReportHistorySnapshots\" Type=\"Collection(Model.ReportHistorySnapshot)\" />\r\n        <NavigationProperty Name=\"HistorySnapshots\" Type=\"Collection(Model.HistorySnapshot)\" />\r\n        <NavigationProperty Name=\"ParameterDefinitions\" Type=\"Collection(Model.ReportParameterDefinition)\" />\r\n        <NavigationProperty Name=\"CacheOptions\" Type=\"Model.CacheOptions\" />\r\n      </EntityType>\r\n      <EntityType Name=\"ReportParameterDefinition\">\r\n        <Key>\r\n          <PropertyRef Name=\"Name\" />\r\n        </Key>\r\n        <Property Name=\"Name\" Type=\"Edm.String\" Nullable=\"false\" />\r\n        <Property Name=\"ParameterType\" Type=\"Model.ReportParameterType\" Nullable=\"false\" />\r\n        <Property Name=\"ParameterVisibility\" Type=\"Model.ReportParameterVisibility\" Nullable=\"false\" />\r\n        <Property Name=\"ParameterState\" Type=\"Model.ReportParameterState\" Nullable=\"false\" />\r\n        <Property Name=\"ValidValues\" Type=\"Collection(Model.ValidValue)\" />\r\n        <Property Name=\"ValidValuesIsNull\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"Nullable\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"AllowBlank\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"MultiValue\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"Prompt\" Type=\"Edm.String\" />\r\n        <Property Name=\"PromptUser\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"QueryParameter\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"DefaultValuesQueryBased\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"ValidValuesQueryBased\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"Dependencies\" Type=\"Collection(Edm.String)\" />\r\n        <Property Name=\"DefaultValues\" Type=\"Collection(Edm.String)\" />\r\n        <Property Name=\"DefaultValuesIsNull\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"ErrorMessage\" Type=\"Edm.String\" />\r\n      </EntityType>\r\n      <EntityType Name=\"System\">\r\n        <Key>\r\n          <PropertyRef Name=\"Id\" />\r\n        </Key>\r\n        <Property Name=\"Id\" Type=\"Edm.Guid\" Nullable=\"false\" />\r\n        <Property Name=\"ReportServerAbsoluteUrl\" Type=\"Edm.String\" />\r\n        <Property Name=\"ReportServerRelativeUrl\" Type=\"Edm.String\" />\r\n        <Property Name=\"WebPort[...string is too long...]");
				IEdmModel edmModel2;
				try
				{
					IEdmModel edmModel;
					IEnumerable<EdmError> enumerable;
					if (!CsdlReader.TryParse(xmlReader, true, out edmModel, out enumerable))
					{
						StringBuilder stringBuilder = new StringBuilder();
						foreach (EdmError edmError in enumerable)
						{
							stringBuilder.Append(edmError.ErrorMessage);
							stringBuilder.Append("; ");
						}
						throw new InvalidOperationException(stringBuilder.ToString());
					}
					edmModel2 = edmModel;
				}
				finally
				{
					((IDisposable)xmlReader).Dispose();
				}
				return edmModel2;
			}

			// Token: 0x06000D61 RID: 3425 RVA: 0x0001B0C8 File Offset: 0x000192C8
			[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
			private static XmlReader CreateXmlReader(string edmxToParse)
			{
				return XmlReader.Create(new StringReader(edmxToParse));
			}

			// Token: 0x0400062D RID: 1581
			[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
			private static IEdmModel ParsedModel = Container.GeneratedEdmModel.LoadModelFromString();

			// Token: 0x0400062E RID: 1582
			[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
			private const string Edmx = "<edmx:Edmx Version=\"4.0\" xmlns:edmx=\"http://docs.oasis-open.org/odata/ns/edmx\">\r\n  <edmx:DataServices>\r\n    <Schema Namespace=\"Model\" xmlns=\"http://docs.oasis-open.org/odata/ns/edm\">\r\n      <ComplexType Name=\"KpiDataItem\" Abstract=\"true\">\r\n        <Property Name=\"Type\" Type=\"Model.KpiDataItemType\" Nullable=\"false\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"KpiSharedDataItem\" BaseType=\"Model.KpiDataItem\">\r\n        <Property Name=\"Id\" Type=\"Edm.Guid\" Nullable=\"false\" />\r\n        <Property Name=\"Path\" Type=\"Edm.String\" />\r\n        <Property Name=\"Parameters\" Type=\"Collection(Model.DataSetParameter)\" Nullable=\"false\" />\r\n        <Property Name=\"Aggregation\" Type=\"Model.KpiSharedDataItemAggregation\" Nullable=\"false\" />\r\n        <Property Name=\"Column\" Type=\"Edm.String\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"DataSetParameter\">\r\n        <Property Name=\"Name\" Type=\"Edm.String\" />\r\n        <Property Name=\"Value\" Type=\"Edm.String\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"KpiStaticDataItem\" BaseType=\"Model.KpiDataItem\">\r\n        <Property Name=\"Value\" Type=\"Edm.String\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"DrillthroughTarget\" Abstract=\"true\">\r\n        <Property Name=\"Type\" Type=\"Model.DrillthroughTargetType\" Nullable=\"false\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"CatalogItemDrillthroughTarget\" BaseType=\"Model.DrillthroughTarget\">\r\n        <Property Name=\"CatalogItemType\" Type=\"Model.CatalogItemType\" Nullable=\"false\" />\r\n        <Property Name=\"Path\" Type=\"Edm.String\" />\r\n        <Property Name=\"Id\" Type=\"Edm.Guid\" Nullable=\"false\" />\r\n        <Property Name=\"Parameters\" Type=\"Collection(Model.CatalogItemParameter)\" Nullable=\"false\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"CatalogItemParameter\">\r\n        <Property Name=\"Name\" Type=\"Edm.String\" />\r\n        <Property Name=\"Value\" Type=\"Edm.String\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"UrlDrillthroughTarget\" BaseType=\"Model.DrillthroughTarget\">\r\n        <Property Name=\"Url\" Type=\"Edm.String\" />\r\n        <Property Name=\"DirectNavigation\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n      </ComplexType>\r\n      <EntityType Name=\"AlertSubscription\">\r\n        <Key>\r\n          <PropertyRef Name=\"Id\" />\r\n        </Key>\r\n        <Property Name=\"Id\" Type=\"Edm.Int64\" Nullable=\"false\" />\r\n        <Property Name=\"ItemId\" Type=\"Edm.Guid\" Nullable=\"false\" />\r\n        <Property Name=\"AlertType\" Type=\"Edm.String\" Nullable=\"false\" />\r\n      </EntityType>\r\n      <EntityType Name=\"CacheRefreshPlan\">\r\n        <Key>\r\n          <PropertyRef Name=\"Id\" />\r\n        </Key>\r\n        <Property Name=\"Id\" Type=\"Edm.Guid\" Nullable=\"false\" />\r\n        <Property Name=\"Owner\" Type=\"Edm.String\" />\r\n        <Property Name=\"Description\" Type=\"Edm.String\" />\r\n        <Property Name=\"CatalogItemPath\" Type=\"Edm.String\" />\r\n        <Property Name=\"EventType\" Type=\"Edm.String\" />\r\n        <Property Name=\"Schedule\" Type=\"Model.ScheduleReference\" />\r\n        <Property Name=\"ScheduleDescription\" Type=\"Edm.String\" />\r\n        <Property Name=\"LastRunTime\" Type=\"Edm.DateTimeOffset\" />\r\n        <Property Name=\"LastStatus\" Type=\"Edm.String\" />\r\n        <Property Name=\"ModifiedBy\" Type=\"Edm.String\" />\r\n        <Property Name=\"ModifiedDate\" Type=\"Edm.DateTimeOffset\" />\r\n        <Property Name=\"ParameterValues\" Type=\"Collection(Model.ParameterValue)\" />\r\n        <NavigationProperty Name=\"History\" Type=\"Collection(Model.SubscriptionHistory)\" />\r\n      </EntityType>\r\n      <EntityType Name=\"SubscriptionHistory\">\r\n        <Key>\r\n          <PropertyRef Name=\"Id\" />\r\n        </Key>\r\n        <Property Name=\"Id\" Type=\"Edm.Int32\" Nullable=\"false\" />\r\n        <Property Name=\"SubscriptionID\" Type=\"Edm.Guid\" Nullable=\"false\" />\r\n        <Property Name=\"Type\" Type=\"Model.SubscriptionExecutionType\" Nullable=\"false\" />\r\n        <Property Name=\"StartTime\" Type=\"Edm.DateTimeOffset\" Nullable=\"false\" />\r\n        <Property Name=\"EndTime\" Type=\"Edm.DateTimeOffset\" Nullable=\"false\" />\r\n        <Property Name=\"Status\" Type=\"Model.SubscriptionStatus\" Nullable=\"false\" />\r\n        <Property Name=\"Message\" Type=\"Edm.String\" />\r\n        <Property Name=\"Details\" Type=\"Edm.String\" />\r\n      </EntityType>\r\n      <EntityType Name=\"Property\">\r\n        <Key>\r\n          <PropertyRef Name=\"Name\" />\r\n        </Key>\r\n        <Property Name=\"Name\" Type=\"Edm.String\" Nullable=\"false\" />\r\n        <Property Name=\"Value\" Type=\"Edm.String\" />\r\n      </EntityType>\r\n      <EntityType Name=\"HistorySnapshotOptions\">\r\n        <Key>\r\n          <PropertyRef Name=\"CatalogItemId\" />\r\n        </Key>\r\n        <Property Name=\"CatalogItemId\" Type=\"Edm.Guid\" Nullable=\"false\" />\r\n        <Property Name=\"HistorySnapshotsOptions\" Type=\"Model.ReportHistorySnapshotsOptions\" />\r\n      </EntityType>\r\n      <EntityType Name=\"CacheOptions\">\r\n        <Key>\r\n          <PropertyRef Name=\"Id\" />\r\n        </Key>\r\n        <Property Name=\"Id\" Type=\"Edm.Guid\" Nullable=\"false\" />\r\n        <Property Name=\"ExecutionType\" Type=\"Model.ItemExecutionType\" Nullable=\"false\" />\r\n        <Property Name=\"Expiration\" Type=\"Model.ExpirationReference\" />\r\n      </EntityType>\r\n      <EntityType Name=\"CatalogItem\" Abstract=\"true\">\r\n        <Key>\r\n          <PropertyRef Name=\"Id\" />\r\n        </Key>\r\n        <Property Name=\"Id\" Type=\"Edm.Guid\" Nullable=\"false\" />\r\n        <Property Name=\"Name\" Type=\"Edm.String\" />\r\n        <Property Name=\"Description\" Type=\"Edm.String\" />\r\n        <Property Name=\"Path\" Type=\"Edm.String\" />\r\n        <Property Name=\"Type\" Type=\"Model.CatalogItemType\" Nullable=\"false\" />\r\n        <Property Name=\"Hidden\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"Size\" Type=\"Edm.Int64\" Nullable=\"false\" />\r\n        <Property Name=\"ModifiedBy\" Type=\"Edm.String\" />\r\n        <Property Name=\"ModifiedDate\" Type=\"Edm.DateTimeOffset\" Nullable=\"false\" />\r\n        <Property Name=\"CreatedBy\" Type=\"Edm.String\" />\r\n        <Property Name=\"CreatedDate\" Type=\"Edm.DateTimeOffset\" Nullable=\"false\" />\r\n        <Property Name=\"ParentFolderId\" Type=\"Edm.Guid\" />\r\n        <Property Name=\"IsFavorite\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"Roles\" Type=\"Collection(Model.Role)\" />\r\n        <Property Name=\"ContentType\" Type=\"Edm.String\" />\r\n        <Property Name=\"Content\" Type=\"Edm.Binary\" />\r\n        <NavigationProperty Name=\"ParentFolder\" Type=\"Model.Folder\" />\r\n        <NavigationProperty Name=\"Properties\" Type=\"Collection(Model.Property)\" />\r\n        <NavigationProperty Name=\"Comments\" Type=\"Collection(Model.Comment)\" />\r\n        <NavigationProperty Name=\"AlertSubscriptions\" Type=\"Collection(Model.AlertSubscription)\" />\r\n        <NavigationProperty Name=\"AllowedActions\" Type=\"Collection(Model.AllowedAction)\" />\r\n        <NavigationProperty Name=\"Policies\" Type=\"Collection(Model.ItemPolicy)\" />\r\n        <NavigationProperty Name=\"DependentItems\" Type=\"Collection(Model.CatalogItem)\" />\r\n        <Annotation Term=\"OData.Community.Keys.V1.AlternateKeys\">\r\n          <Collection>\r\n            <Record Type=\"OData.Community.Keys.V1.AlternateKey\">\r\n              <PropertyValue Property=\"Key\">\r\n                <Collection>\r\n                  <Record Type=\"OData.Community.Keys.V1.PropertyRef\">\r\n                    <PropertyValue Property=\"Alias\" String=\"Path\" />\r\n                    <PropertyValue Property=\"Name\" PropertyPath=\"Path\" />\r\n                  </Record>\r\n                </Collection>\r\n              </PropertyValue>\r\n            </Record>\r\n          </Collection>\r\n        </Annotation>\r\n      </EntityType>\r\n      <ComplexType Name=\"BulkOperationsResult\">\r\n        <Property Name=\"FailedOperations\" Type=\"Collection(Edm.String)\" />\r\n        <Property Name=\"HasErrors\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"CatalogItemAccessToken\">\r\n        <Property Name=\"Token\" Type=\"Edm.Binary\" />\r\n      </ComplexType>\r\n      <EntityType Name=\"ItemPolicy\">\r\n        <Key>\r\n          <PropertyRef Name=\"Id\" />\r\n        </Key>\r\n        <Property Name=\"Id\" Type=\"Edm.Guid\" Nullable=\"false\" />\r\n        <Property Name=\"InheritParentPolicy\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"Policies\" Type=\"Collection(Model.Policy)\" />\r\n      </EntityType>\r\n      <EntityType Name=\"Comment\">\r\n        <Key>\r\n          <PropertyRef Name=\"Id\" />\r\n        </Key>\r\n        <Property Name=\"Id\" Type=\"Edm.Int64\" Nullable=\"false\" />\r\n        <Property Name=\"ItemId\" Type=\"Edm.Guid\" Nullable=\"false\" />\r\n        <Property Name=\"UserName\" Type=\"Edm.String\" />\r\n        <Property Name=\"ThreadId\" Type=\"Edm.Int64\" />\r\n        <Property Name=\"AttachmentPath\" Type=\"Edm.String\" />\r\n        <Property Name=\"Text\" Type=\"Edm.String\" Nullable=\"false\" />\r\n        <Property Name=\"CreatedDate\" Type=\"Edm.DateTimeOffset\" Nullable=\"false\" />\r\n        <Property Name=\"ModifiedDate\" Type=\"Edm.DateTimeOffset\" />\r\n      </EntityType>\r\n      <EntityType Name=\"DataSet\" BaseType=\"Model.CatalogItem\">\r\n        <Property Name=\"HasParameters\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"QueryExecutionTimeOut\" Type=\"Edm.Int32\" Nullable=\"false\" />\r\n        <NavigationProperty Name=\"Data\" Type=\"Collection(Model.DataSetRow)\" />\r\n        <NavigationProperty Name=\"DataSources\" Type=\"Collection(Model.DataSource)\" />\r\n        <NavigationProperty Name=\"CacheRefreshPlans\" Type=\"Collection(Model.CacheRefreshPlan)\" />\r\n        <NavigationProperty Name=\"ParameterDefinitions\" Type=\"Collection(Model.ReportParameterDefinition)\" />\r\n        <NavigationProperty Name=\"CacheOptions\" Type=\"Model.CacheOptions\" />\r\n      </EntityType>\r\n      <ComplexType Name=\"DataSetSchema\">\r\n        <Property Name=\"Name\" Type=\"Edm.String\" />\r\n        <Property Name=\"Fields\" Type=\"Collection(Model.DataSetField)\" />\r\n        <Property Name=\"Parameters\" Type=\"Collection(Model.DataSetParameterInfo)\" />\r\n      </ComplexType>\r\n      <EntityType Name=\"DataSetRow\" OpenType=\"true\">\r\n        <Key>\r\n          <PropertyRef Name=\"Id\" />\r\n        </Key>\r\n        <Property Name=\"Id\" Type=\"Edm.Guid\" Nullable=\"false\" />\r\n      </EntityType>\r\n      <EntityType Name=\"DataSource\" BaseType=\"Model.CatalogItem\">\r\n        <Property Name=\"IsEnabled\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"ConnectionString\" Type=\"Edm.String\" />\r\n        <Property Name=\"DataSourceType\" Type=\"Edm.String\" />\r\n        <Property Name=\"IsOriginalConnectionStringExpressionBased\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"IsConnectionStringOverridden\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"CredentialRetrieval\" Type=\"Model.CredentialRetrievalType\" Nullable=\"false\" />\r\n        <Property Name=\"CredentialsByUser\" Type=\"Model.CredentialsSuppliedByUser\" />\r\n        <Property Name=\"CredentialsInServer\" Type=\"Model.CredentialsStoredInServer\" />\r\n        <Property Name=\"IsReference\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"DataSourceSubType\" Type=\"Edm.String\" />\r\n        <Property Name=\"DataModelDataSource\" Type=\"Model.DataModelDataSource\" />\r\n        <NavigationProperty Name=\"Subscriptions\" Type=\"Collection(Model.Subscription)\" />\r\n      </EntityType>\r\n      <ComplexType Name=\"DataSourceCheckResult\">\r\n        <Property Name=\"IsSuccessful\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"ErrorMessage\" Type=\"Edm.String\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"Query\">\r\n        <Property Name=\"CommandText\" Type=\"Edm.String\" />\r\n        <Property Name=\"Timeout\" Type=\"Edm.Int32\" />\r\n      </ComplexType>\r\n      <EntityType Name=\"ExcelWorkbook\" BaseType=\"Model.CatalogItem\" />\r\n      <EntityType Name=\"Extension\">\r\n        <Key>\r\n          <PropertyRef Name=\"Name\" />\r\n        </Key>\r\n        <Property Name=\"ExtensionType\" Type=\"Model.ExtensionType\" Nullable=\"false\" />\r\n        <Property Name=\"Name\" Type=\"Edm.String\" Nullable=\"false\" />\r\n        <Property Name=\"LocalizedName\" Type=\"Edm.String\" />\r\n        <Property Name=\"Visible\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"Parameters\" Type=\"Collection(Model.ExtensionParameter)\" />\r\n      </EntityType>\r\n      <ComplexType Name=\"ExtensionParameter\">\r\n        <Property Name=\"Name\" Type=\"Edm.String\" />\r\n        <Property Name=\"DisplayName\" Type=\"Edm.String\" />\r\n        <Property Name=\"Required\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"ReadOnly\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"Value\" Type=\"Edm.String\" />\r\n        <Property Name=\"Error\" Type=\"Edm.String\" />\r\n        <Property Name=\"Encrypted\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"IsPassword\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"ValidValues\" Type=\"Collection(Model.ValidValue)\" />\r\n        <Property Name=\"ValidValuesIsNull\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"ParameterValue\">\r\n        <Property Name=\"Name\" Type=\"Edm.String\" />\r\n        <Property Name=\"Value\" Type=\"Edm.String\" />\r\n        <Property Name=\"IsValueFieldReference\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n      </ComplexType>\r\n      <EntityType Name=\"FavoriteItem\">\r\n        <Key>\r\n          <PropertyRef Name=\"Id\" />\r\n        </Key>\r\n        <Property Name=\"Id\" Type=\"Edm.Guid\" Nullable=\"false\" />\r\n        <NavigationProperty Name=\"Item\" Type=\"Model.CatalogItem\" />\r\n      </EntityType>\r\n      <EntityType Name=\"Folder\" BaseType=\"Model.CatalogItem\">\r\n        <NavigationProperty Name=\"CatalogItems\" Type=\"Collection(Model.CatalogItem)\" />\r\n      </EntityType>\r\n      <EntityType Name=\"Kpi\" BaseType=\"Model.CatalogItem\">\r\n        <Property Name=\"ValueFormat\" Type=\"Model.KpiValueFormat\" Nullable=\"false\" />\r\n        <Property Name=\"Visualization\" Type=\"Model.KpiVisualization\" Nullable=\"false\" />\r\n        <Property Name=\"DrillthroughTarget\" Type=\"Model.DrillthroughTarget\" />\r\n        <Property Name=\"Currency\" Type=\"Edm.String\" />\r\n        <Property Name=\"Values\" Type=\"Model.KpiValues\" />\r\n        <Property Name=\"Data\" Type=\"Model.KpiData\" />\r\n      </EntityType>\r\n      <EntityType Name=\"LinkedReport\" BaseType=\"Model.CatalogItem\">\r\n        <Property Name=\"HasParameters\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"Link\" Type=\"Edm.String\" />\r\n        <NavigationProperty Name=\"Subscriptions\" Type=\"Collection(Model.Subscription)\" />\r\n        <NavigationProperty Name=\"CacheRefreshPlans\" Type=\"Collection(Model.CacheRefreshPlan)\" />\r\n        <NavigationProperty Name=\"HistorySnapshotOptions\" Type=\"Model.HistorySnapshotOptions\" />\r\n        <NavigationProperty Name=\"ReportHistorySnapshots\" Type=\"Collection(Model.ReportHistorySnapshot)\" />\r\n        <NavigationProperty Name=\"HistorySnapshots\" Type=\"Collection(Model.HistorySnapshot)\" />\r\n        <NavigationProperty Name=\"ParameterDefinitions\" Type=\"Collection(Model.ReportParameterDefinition)\" />\r\n        <NavigationProperty Name=\"CacheOptions\" Type=\"Model.CacheOptions\" />\r\n      </EntityType>\r\n      <EntityType Name=\"MobileReport\" BaseType=\"Model.CatalogItem\">\r\n        <Property Name=\"AllowCaching\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"Manifest\" Type=\"Model.MobileReportManifest\" />\r\n        <Property Name=\"HasSharedDataSets\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <NavigationProperty Name=\"SharedDataSets\" Type=\"Collection(Model.DataSet)\" />\r\n      </EntityType>\r\n      <EntityType Name=\"Notification\">\r\n        <Key>\r\n          <PropertyRef Name=\"Id\" />\r\n        </Key>\r\n        <Property Name=\"Id\" Type=\"Edm.Guid\" Nullable=\"false\" />\r\n        <Property Name=\"IssueType\" Type=\"Model.IssueType\" Nullable=\"false\" />\r\n      </EntityType>\r\n      <EntityType Name=\"PowerBIReport\" BaseType=\"Model.CatalogItem\">\r\n        <Property Name=\"HasDataSources\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <NavigationProperty Name=\"DataSources\" Type=\"Collection(Model.DataSource)\" />\r\n        <NavigationProperty Name=\"DataModelParameters\" Type=\"Collection(Model.DataModelParameter)\" />\r\n        <NavigationProperty Name=\"CacheRefreshPlans\" Type=\"Collection(Model.CacheRefreshPlan)\" />\r\n        <NavigationProperty Name=\"DataModelRoleAssignments\" Type=\"Collection(Model.DataModelRoleAssignment)\" />\r\n        <NavigationProperty Name=\"DataModelRoles\" Type=\"Collection(Model.DataModelRole)\" />\r\n      </EntityType>\r\n      <EntityType Name=\"DataModelRole\">\r\n        <Key>\r\n          <PropertyRef Name=\"ModelRoleId\" />\r\n        </Key>\r\n        <Property Name=\"ModelRoleId\" Type=\"Edm.Guid\" Nullable=\"false\" />\r\n        <Property Name=\"ModelRoleName\" Type=\"Edm.String\" />\r\n      </EntityType>\r\n      <EntityType Name=\"DataModelRoleAssignment\">\r\n        <Key>\r\n          <PropertyRef Name=\"GroupUserName\" />\r\n        </Key>\r\n        <Property Name=\"GroupUserName\" Type=\"Edm.String\" Nullable=\"false\" />\r\n        <Property Name=\"DataModelRoles\" Type=\"Collection(Edm.Guid)\" Nullable=\"false\" />\r\n      </EntityType>\r\n      <EntityType Name=\"Report\" BaseType=\"Model.CatalogItem\">\r\n        <Property Name=\"HasDataSources\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"HasSharedDataSets\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"HasParameters\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <NavigationProperty Name=\"Subscriptions\" Type=\"Collection(Model.Subscription)\" />\r\n        <NavigationProperty Name=\"CacheRefreshPlans\" Type=\"Collection(Model.CacheRefreshPlan)\" />\r\n        <NavigationProperty Name=\"DataSources\" Type=\"Collection(Model.DataSource)\" />\r\n        <NavigationProperty Name=\"SharedDataSets\" Type=\"Collection(Model.DataSet)\" />\r\n        <NavigationProperty Name=\"HistorySnapshotOptions\" Type=\"Model.HistorySnapshotOptions\" />\r\n        <NavigationProperty Name=\"ReportHistorySnapshots\" Type=\"Collection(Model.ReportHistorySnapshot)\" />\r\n        <NavigationProperty Name=\"HistorySnapshots\" Type=\"Collection(Model.HistorySnapshot)\" />\r\n        <NavigationProperty Name=\"ParameterDefinitions\" Type=\"Collection(Model.ReportParameterDefinition)\" />\r\n        <NavigationProperty Name=\"CacheOptions\" Type=\"Model.CacheOptions\" />\r\n      </EntityType>\r\n      <EntityType Name=\"ReportParameterDefinition\">\r\n        <Key>\r\n          <PropertyRef Name=\"Name\" />\r\n        </Key>\r\n        <Property Name=\"Name\" Type=\"Edm.String\" Nullable=\"false\" />\r\n        <Property Name=\"ParameterType\" Type=\"Model.ReportParameterType\" Nullable=\"false\" />\r\n        <Property Name=\"ParameterVisibility\" Type=\"Model.ReportParameterVisibility\" Nullable=\"false\" />\r\n        <Property Name=\"ParameterState\" Type=\"Model.ReportParameterState\" Nullable=\"false\" />\r\n        <Property Name=\"ValidValues\" Type=\"Collection(Model.ValidValue)\" />\r\n        <Property Name=\"ValidValuesIsNull\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"Nullable\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"AllowBlank\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"MultiValue\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"Prompt\" Type=\"Edm.String\" />\r\n        <Property Name=\"PromptUser\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"QueryParameter\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"DefaultValuesQueryBased\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"ValidValuesQueryBased\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"Dependencies\" Type=\"Collection(Edm.String)\" />\r\n        <Property Name=\"DefaultValues\" Type=\"Collection(Edm.String)\" />\r\n        <Property Name=\"DefaultValuesIsNull\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"ErrorMessage\" Type=\"Edm.String\" />\r\n      </EntityType>\r\n      <EntityType Name=\"System\">\r\n        <Key>\r\n          <PropertyRef Name=\"Id\" />\r\n        </Key>\r\n        <Property Name=\"Id\" Type=\"Edm.Guid\" Nullable=\"false\" />\r\n        <Property Name=\"ReportServerAbsoluteUrl\" Type=\"Edm.String\" />\r\n        <Property Name=\"ReportServerRelativeUrl\" Type=\"Edm.String\" />\r\n        <Property Name=\"WebPort[...string is too long...]";
		}
	}
}
