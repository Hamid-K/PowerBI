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

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000A8 RID: 168
	[OriginalName("Container")]
	public class Container : DataServiceContext
	{
		// Token: 0x060006EB RID: 1771 RVA: 0x0000E728 File Offset: 0x0000C928
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public Container(Uri serviceRoot)
			: base(serviceRoot, ODataProtocolVersion.V4)
		{
			base.ResolveName = new Func<Type, string>(this.ResolveNameFromType);
			base.ResolveType = new Func<string, Type>(this.ResolveTypeFromName);
			base.Format.LoadServiceModel = new Func<IEdmModel>(Container.GeneratedEdmModel.GetInstance);
			base.Format.UseJson();
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x0000E784 File Offset: 0x0000C984
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected Type ResolveTypeFromName(string typeName)
		{
			Type type = base.DefaultResolveType(typeName, "PowerBIIntegration", "Microsoft.ReportingServices.Portal.ODataClient.V1.PowerBIIntegration");
			if (type != null)
			{
				return type;
			}
			type = base.DefaultResolveType(typeName, "Model", "Microsoft.ReportingServices.Portal.ODataClient.V1.Model");
			if (type != null)
			{
				return type;
			}
			return null;
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x0000E7CC File Offset: 0x0000C9CC
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected string ResolveNameFromType(Type clientType)
		{
			OriginalNameAttribute originalNameAttribute = (OriginalNameAttribute)Utility.GetCustomAttributes(clientType, typeof(OriginalNameAttribute), true).SingleOrDefault<object>();
			if (clientType.Namespace.Equals("Microsoft.ReportingServices.Portal.ODataClient.V1.PowerBIIntegration", StringComparison.Ordinal))
			{
				if (originalNameAttribute != null)
				{
					return "PowerBIIntegration." + originalNameAttribute.OriginalName;
				}
				return "PowerBIIntegration." + clientType.Name;
			}
			else if (clientType.Namespace.Equals("Microsoft.ReportingServices.Portal.ODataClient.V1.Model", StringComparison.Ordinal))
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

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x060006EE RID: 1774 RVA: 0x0000E885 File Offset: 0x0000CA85
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

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x060006EF RID: 1775 RVA: 0x0000E8A6 File Offset: 0x0000CAA6
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("CacheRefreshPlan")]
		public DataServiceQuery<CacheRefreshPlan> CacheRefreshPlan
		{
			get
			{
				if (this._CacheRefreshPlan == null)
				{
					this._CacheRefreshPlan = base.CreateQuery<CacheRefreshPlan>("CacheRefreshPlan");
				}
				return this._CacheRefreshPlan;
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x060006F0 RID: 1776 RVA: 0x0000E8C7 File Offset: 0x0000CAC7
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

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x060006F1 RID: 1777 RVA: 0x0000E8E8 File Offset: 0x0000CAE8
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("ReportParameters")]
		public DataServiceQuery<ReportParameterDefinition> ReportParameters
		{
			get
			{
				if (this._ReportParameters == null)
				{
					this._ReportParameters = base.CreateQuery<ReportParameterDefinition>("ReportParameters");
				}
				return this._ReportParameters;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x060006F2 RID: 1778 RVA: 0x0000E909 File Offset: 0x0000CB09
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

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x060006F3 RID: 1779 RVA: 0x0000E92A File Offset: 0x0000CB2A
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

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x060006F4 RID: 1780 RVA: 0x0000E94B File Offset: 0x0000CB4B
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

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x060006F5 RID: 1781 RVA: 0x0000E96C File Offset: 0x0000CB6C
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

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x060006F6 RID: 1782 RVA: 0x0000E98D File Offset: 0x0000CB8D
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

		// Token: 0x060006F7 RID: 1783 RVA: 0x000025B8 File Offset: 0x000007B8
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public void AddToSubscriptions(Subscription subscription)
		{
			base.AddObject("Subscriptions", subscription);
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x0000E9AE File Offset: 0x0000CBAE
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public void AddToCacheRefreshPlan(CacheRefreshPlan cacheRefreshPlan)
		{
			base.AddObject("CacheRefreshPlan", cacheRefreshPlan);
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x000024BC File Offset: 0x000006BC
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public void AddToCatalogItems(CatalogItem catalogItem)
		{
			base.AddObject("CatalogItems", catalogItem);
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x0000E9BC File Offset: 0x0000CBBC
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public void AddToReportParameters(ReportParameterDefinition reportParameterDefinition)
		{
			base.AddObject("ReportParameters", reportParameterDefinition);
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x000024CA File Offset: 0x000006CA
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public void AddToComments(Comment comment)
		{
			base.AddObject("Comments", comment);
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x000025AA File Offset: 0x000007AA
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public void AddToSchedules(Schedule schedule)
		{
			base.AddObject("Schedules", schedule);
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x000025C6 File Offset: 0x000007C6
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public void AddToSystemResources(SystemResource systemResource)
		{
			base.AddObject("SystemResources", systemResource);
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x000025D4 File Offset: 0x000007D4
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public void AddToSystemResourceItems(SystemResourceItem systemResourceItem)
		{
			base.AddObject("SystemResourceItems", systemResourceItem);
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x00002564 File Offset: 0x00000764
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public void AddToNotifications(Notification notification)
		{
			base.AddObject("Notifications", notification);
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000700 RID: 1792 RVA: 0x0000E9CA File Offset: 0x0000CBCA
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("ReportServerInfo")]
		public ReportServerInfoSingle ReportServerInfo
		{
			get
			{
				if (this._ReportServerInfo == null)
				{
					this._ReportServerInfo = new ReportServerInfoSingle(this, "ReportServerInfo");
				}
				return this._ReportServerInfo;
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000701 RID: 1793 RVA: 0x0000E9EB File Offset: 0x0000CBEB
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

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000702 RID: 1794 RVA: 0x0000EA0C File Offset: 0x0000CC0C
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

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000703 RID: 1795 RVA: 0x0000EA2D File Offset: 0x0000CC2D
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

		// Token: 0x06000704 RID: 1796 RVA: 0x0000EA50 File Offset: 0x0000CC50
		[OriginalName("CatalogItemByPath")]
		public CatalogItemSingle CatalogItemByPath(string path)
		{
			return new CatalogItemSingle(base.CreateFunctionQuerySingle<CatalogItem>("", "CatalogItemByPath", false, new UriOperationParameter[]
			{
				new UriOperationParameter("path", path)
			}));
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x0000EA87 File Offset: 0x0000CC87
		[OriginalName("FavoriteItems")]
		public DataServiceQuery<CatalogItem> FavoriteItems()
		{
			return base.CreateFunctionQuery<CatalogItem>("", "FavoriteItems", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x0000EA9F File Offset: 0x0000CC9F
		[OriginalName("RestrictedFeatures")]
		public DataServiceQuery<string> RestrictedFeatures()
		{
			return base.CreateFunctionQuery<string>("", "RestrictedFeatures", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x0000EAB7 File Offset: 0x0000CCB7
		[OriginalName("ServiceState")]
		public DataServiceQuerySingle<ServiceState> ServiceState()
		{
			return base.CreateFunctionQuerySingle<ServiceState>("", "ServiceState", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x0000EAD0 File Offset: 0x0000CCD0
		[OriginalName("AllowedActions")]
		public DataServiceQuery<string> AllowedActions(string path)
		{
			return base.CreateFunctionQuery<string>("", "AllowedActions", false, new UriOperationParameter[]
			{
				new UriOperationParameter("path", path)
			});
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x0000EB04 File Offset: 0x0000CD04
		[OriginalName("SafeGetSystemResourceContent")]
		public DataServiceQuerySingle<byte[]> SafeGetSystemResourceContent(string type, string key)
		{
			return base.CreateFunctionQuerySingle<byte[]>("", "SafeGetSystemResourceContent", false, new UriOperationParameter[]
			{
				new UriOperationParameter("type", type),
				new UriOperationParameter("key", key)
			});
		}

		// Token: 0x04000367 RID: 871
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<Subscription> _Subscriptions;

		// Token: 0x04000368 RID: 872
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<CacheRefreshPlan> _CacheRefreshPlan;

		// Token: 0x04000369 RID: 873
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<CatalogItem> _CatalogItems;

		// Token: 0x0400036A RID: 874
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<ReportParameterDefinition> _ReportParameters;

		// Token: 0x0400036B RID: 875
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<Comment> _Comments;

		// Token: 0x0400036C RID: 876
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<Schedule> _Schedules;

		// Token: 0x0400036D RID: 877
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<SystemResource> _SystemResources;

		// Token: 0x0400036E RID: 878
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<SystemResourceItem> _SystemResourceItems;

		// Token: 0x0400036F RID: 879
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<Notification> _Notifications;

		// Token: 0x04000370 RID: 880
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ReportServerInfoSingle _ReportServerInfo;

		// Token: 0x04000371 RID: 881
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private UserSingle _Me;

		// Token: 0x04000372 RID: 882
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private TelemetrySingle _Telemetry;

		// Token: 0x04000373 RID: 883
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private PowerBIUserInfoSingle _PowerBIIntegration;

		// Token: 0x0200013A RID: 314
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private abstract class GeneratedEdmModel
		{
			// Token: 0x06000D70 RID: 3440 RVA: 0x0001B124 File Offset: 0x00019324
			[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
			public static IEdmModel GetInstance()
			{
				return Container.GeneratedEdmModel.ParsedModel;
			}

			// Token: 0x06000D71 RID: 3441 RVA: 0x0001B12C File Offset: 0x0001932C
			[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
			private static IEdmModel LoadModelFromString()
			{
				XmlReader xmlReader = Container.GeneratedEdmModel.CreateXmlReader("<edmx:Edmx Version=\"4.0\" xmlns:edmx=\"http://docs.oasis-open.org/odata/ns/edmx\">\r\n  <edmx:DataServices>\r\n    <Schema Namespace=\"Model\" xmlns=\"http://docs.oasis-open.org/odata/ns/edm\">\r\n      <ComplexType Name=\"KpiDataItem\" Abstract=\"true\">\r\n        <Property Name=\"Type\" Type=\"Model.KpiDataItemType\" Nullable=\"false\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"KpiSharedDataItem\" BaseType=\"Model.KpiDataItem\">\r\n        <Property Name=\"Id\" Type=\"Edm.Guid\" Nullable=\"false\" />\r\n        <Property Name=\"Path\" Type=\"Edm.String\" />\r\n        <Property Name=\"Parameters\" Type=\"Collection(Model.DataSetParameter)\" Nullable=\"false\" />\r\n        <Property Name=\"Aggregation\" Type=\"Model.KpiSharedDataItemAggregation\" Nullable=\"false\" />\r\n        <Property Name=\"Column\" Type=\"Edm.String\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"DataSetParameter\">\r\n        <Property Name=\"Name\" Type=\"Edm.String\" />\r\n        <Property Name=\"Value\" Type=\"Edm.String\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"KpiStaticDataItem\" BaseType=\"Model.KpiDataItem\">\r\n        <Property Name=\"Value\" Type=\"Edm.String\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"DrillthroughTarget\" Abstract=\"true\">\r\n        <Property Name=\"Type\" Type=\"Model.DrillthroughTargetType\" Nullable=\"false\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"CatalogItemDrillthroughTarget\" BaseType=\"Model.DrillthroughTarget\">\r\n        <Property Name=\"CatalogItemType\" Type=\"Model.CatalogItemType\" Nullable=\"false\" />\r\n        <Property Name=\"Path\" Type=\"Edm.String\" />\r\n        <Property Name=\"Id\" Type=\"Edm.Guid\" Nullable=\"false\" />\r\n        <Property Name=\"Parameters\" Type=\"Collection(Model.CatalogItemParameter)\" Nullable=\"false\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"CatalogItemParameter\">\r\n        <Property Name=\"Name\" Type=\"Edm.String\" />\r\n        <Property Name=\"Value\" Type=\"Edm.String\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"UrlDrillthroughTarget\" BaseType=\"Model.DrillthroughTarget\">\r\n        <Property Name=\"Url\" Type=\"Edm.String\" />\r\n        <Property Name=\"DirectNavigation\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n      </ComplexType>\r\n      <EntityType Name=\"Subscription\">\r\n        <Key>\r\n          <PropertyRef Name=\"Id\" />\r\n        </Key>\r\n        <Property Name=\"Id\" Type=\"Edm.Guid\" Nullable=\"false\" />\r\n        <Property Name=\"Owner\" Type=\"Edm.String\" />\r\n        <Property Name=\"IsDataDriven\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"Description\" Type=\"Edm.String\" />\r\n        <Property Name=\"Report\" Type=\"Edm.String\" />\r\n        <Property Name=\"IsActive\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"EventType\" Type=\"Edm.String\" />\r\n        <Property Name=\"Schedule\" Type=\"Model.ScheduleReference\" />\r\n        <Property Name=\"ScheduleDescription\" Type=\"Edm.String\" />\r\n        <Property Name=\"LastRunTime\" Type=\"Edm.DateTimeOffset\" />\r\n        <Property Name=\"LastStatus\" Type=\"Edm.String\" />\r\n        <Property Name=\"DataQuery\" Type=\"Model.Query\" />\r\n        <Property Name=\"ExtensionSettings\" Type=\"Model.ExtensionSettings\" />\r\n        <Property Name=\"DeliveryExtension\" Type=\"Edm.String\" />\r\n        <Property Name=\"LocalizedDeliveryExtensionName\" Type=\"Edm.String\" />\r\n        <Property Name=\"ModifiedBy\" Type=\"Edm.String\" />\r\n        <Property Name=\"ModifiedDate\" Type=\"Edm.DateTimeOffset\" />\r\n        <Property Name=\"ParameterValues\" Type=\"Collection(Model.ParameterValue)\" />\r\n        <NavigationProperty Name=\"DataSource\" Type=\"Model.DataSource\" />\r\n      </EntityType>\r\n      <EntityType Name=\"CacheRefreshPlan\">\r\n        <Key>\r\n          <PropertyRef Name=\"Id\" />\r\n        </Key>\r\n        <Property Name=\"Id\" Type=\"Edm.Guid\" Nullable=\"false\" />\r\n        <Property Name=\"Owner\" Type=\"Edm.String\" />\r\n        <Property Name=\"Description\" Type=\"Edm.String\" />\r\n        <Property Name=\"CatalogItemPath\" Type=\"Edm.String\" />\r\n        <Property Name=\"EventType\" Type=\"Edm.String\" />\r\n        <Property Name=\"Schedule\" Type=\"Model.ScheduleReference\" />\r\n        <Property Name=\"LastRunTime\" Type=\"Edm.DateTimeOffset\" />\r\n        <Property Name=\"LastStatus\" Type=\"Edm.String\" />\r\n        <Property Name=\"ModifiedBy\" Type=\"Edm.String\" />\r\n        <Property Name=\"ModifiedDate\" Type=\"Edm.DateTimeOffset\" />\r\n        <Property Name=\"ParameterValues\" Type=\"Collection(Model.ParameterValue)\" />\r\n      </EntityType>\r\n      <EntityType Name=\"CatalogItem\" Abstract=\"true\">\r\n        <Key>\r\n          <PropertyRef Name=\"Id\" />\r\n        </Key>\r\n        <Property Name=\"Id\" Type=\"Edm.Guid\" Nullable=\"false\" />\r\n        <Property Name=\"Name\" Type=\"Edm.String\" />\r\n        <Property Name=\"Description\" Type=\"Edm.String\" />\r\n        <Property Name=\"Path\" Type=\"Edm.String\" />\r\n        <Property Name=\"Type\" Type=\"Model.CatalogItemType\" Nullable=\"false\" />\r\n        <Property Name=\"Hidden\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"Size\" Type=\"Edm.Int64\" Nullable=\"false\" />\r\n        <Property Name=\"ModifiedBy\" Type=\"Edm.String\" />\r\n        <Property Name=\"ModifiedDate\" Type=\"Edm.DateTimeOffset\" Nullable=\"false\" />\r\n        <Property Name=\"CreatedBy\" Type=\"Edm.String\" />\r\n        <Property Name=\"CreatedDate\" Type=\"Edm.DateTimeOffset\" Nullable=\"false\" />\r\n        <Property Name=\"ParentFolderId\" Type=\"Edm.Guid\" />\r\n        <Property Name=\"ContentType\" Type=\"Edm.String\" />\r\n        <Property Name=\"Content\" Type=\"Edm.Binary\" />\r\n        <Property Name=\"Properties\" Type=\"Collection(Model.Property)\" />\r\n        <Property Name=\"IsFavorite\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"Policies\" Type=\"Collection(Model.ItemPolicy)\" />\r\n        <Property Name=\"Roles\" Type=\"Collection(Model.Role)\" />\r\n        <NavigationProperty Name=\"ParentFolder\" Type=\"Model.Folder\" />\r\n        <NavigationProperty Name=\"Comments\" Type=\"Collection(Model.Comment)\" />\r\n        <NavigationProperty Name=\"AlertSubscriptions\" Type=\"Collection(Model.AlertSubscription)\" />\r\n        <NavigationProperty Name=\"AllowedActions\" Type=\"Collection(Model.AllowedAction)\" />\r\n        <NavigationProperty Name=\"DependentItems\" Type=\"Collection(Model.CatalogItem)\" />\r\n      </EntityType>\r\n      <ComplexType Name=\"Property\">\r\n        <Property Name=\"Name\" Type=\"Edm.String\" />\r\n        <Property Name=\"Value\" Type=\"Edm.String\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"BulkOperationsResult\">\r\n        <Property Name=\"FailedOperations\" Type=\"Collection(Edm.String)\" />\r\n        <Property Name=\"HasErrors\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"Role\">\r\n        <Property Name=\"Name\" Type=\"Edm.String\" />\r\n        <Property Name=\"Description\" Type=\"Edm.String\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"ItemPolicy\">\r\n        <Property Name=\"Id\" Type=\"Edm.Guid\" Nullable=\"false\" />\r\n        <Property Name=\"InheritParentPolicy\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"Policies\" Type=\"Collection(Model.Policy)\" />\r\n      </ComplexType>\r\n      <EntityType Name=\"ReportParameterDefinition\">\r\n        <Key>\r\n          <PropertyRef Name=\"Name\" />\r\n        </Key>\r\n        <Property Name=\"Name\" Type=\"Edm.String\" Nullable=\"false\" />\r\n        <Property Name=\"ParameterType\" Type=\"Model.ReportParameterType\" Nullable=\"false\" />\r\n        <Property Name=\"ParameterVisibility\" Type=\"Model.ReportParameterVisibility\" Nullable=\"false\" />\r\n        <Property Name=\"ParameterState\" Type=\"Model.ReportParameterState\" Nullable=\"false\" />\r\n        <Property Name=\"ValidValues\" Type=\"Collection(Model.ValidValue)\" />\r\n        <Property Name=\"ValidValuesIsNull\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"Nullable\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"AllowBlank\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"MultiValue\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"Prompt\" Type=\"Edm.String\" />\r\n        <Property Name=\"PromptUser\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"QueryParameter\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"DefaultValuesQueryBased\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"ValidValuesQueryBased\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"Dependencies\" Type=\"Collection(Edm.String)\" />\r\n        <Property Name=\"DefaultValues\" Type=\"Collection(Edm.String)\" />\r\n        <Property Name=\"DefaultValuesIsNull\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"ErrorMessage\" Type=\"Edm.String\" />\r\n      </EntityType>\r\n      <EntityType Name=\"Report\" BaseType=\"Model.CatalogItem\">\r\n        <Property Name=\"HasDataSources\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"HasSharedDataSets\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"HasParameters\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <NavigationProperty Name=\"Subscriptions\" Type=\"Collection(Model.Subscription)\" />\r\n        <NavigationProperty Name=\"CacheRefreshPlans\" Type=\"Collection(Model.CacheRefreshPlan)\" />\r\n        <NavigationProperty Name=\"DataSources\" Type=\"Collection(Model.DataSource)\" />\r\n        <NavigationProperty Name=\"SharedDataSets\" Type=\"Collection(Model.DataSet)\" />\r\n        <NavigationProperty Name=\"HistorySnapshotOptions\" Type=\"Model.HistorySnapshotOptions\" />\r\n        <NavigationProperty Name=\"ReportHistorySnapshots\" Type=\"Collection(Model.ReportHistorySnapshot)\" />\r\n        <NavigationProperty Name=\"HistorySnapshots\" Type=\"Collection(Model.HistorySnapshot)\" />\r\n        <NavigationProperty Name=\"ParameterDefinitions\" Type=\"Collection(Model.ReportParameterDefinition)\" />\r\n      </EntityType>\r\n      <ComplexType Name=\"ParameterValue\">\r\n        <Property Name=\"Name\" Type=\"Edm.String\" />\r\n        <Property Name=\"Value\" Type=\"Edm.String\" />\r\n        <Property Name=\"IsValueFieldReference\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"ReportHistorySnapshotsOptions\">\r\n        <Property Name=\"ManualCreationEnabled\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"KeepExecutionSnapshots\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"UseDefaultSystemLimit\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"ScopedLimit\" Type=\"Edm.Int32\" Nullable=\"false\" />\r\n        <Property Name=\"SystemLimit\" Type=\"Edm.Int32\" Nullable=\"false\" />\r\n        <Property Name=\"Schedule\" Type=\"Model.ScheduleReference\" />\r\n      </ComplexType>\r\n      <EntityType Name=\"LinkedReport\" BaseType=\"Model.CatalogItem\">\r\n        <Property Name=\"HasParameters\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"Link\" Type=\"Edm.String\" />\r\n        <NavigationProperty Name=\"Subscriptions\" Type=\"Collection(Model.Subscription)\" />\r\n        <NavigationProperty Name=\"CacheRefreshPlans\" Type=\"Collection(Model.CacheRefreshPlan)\" />\r\n        <NavigationProperty Name=\"HistorySnapshotOptions\" Type=\"Model.HistorySnapshotOptions\" />\r\n        <NavigationProperty Name=\"ReportHistorySnapshots\" Type=\"Collection(Model.ReportHistorySnapshot)\" />\r\n        <NavigationProperty Name=\"HistorySnapshots\" Type=\"Collection(Model.HistorySnapshot)\" />\r\n        <NavigationProperty Name=\"ParameterDefinitions\" Type=\"Collection(Model.ReportParameterDefinition)\" />\r\n      </EntityType>\r\n      <ComplexType Name=\"ItemHistoryOptions\">\r\n        <Property Name=\"EnableManualSnapshotCreation\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"KeepExecutionSnapshots\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"Schedule\" Type=\"Model.ScheduleReference\" />\r\n      </ComplexType>\r\n      <EntityType Name=\"DataSet\" BaseType=\"Model.CatalogItem\">\r\n        <Property Name=\"HasParameters\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"QueryExecutionTimeOut\" Type=\"Edm.Int32\" Nullable=\"false\" />\r\n        <NavigationProperty Name=\"DataSources\" Type=\"Collection(Model.DataSource)\" />\r\n        <NavigationProperty Name=\"CacheRefreshPlans\" Type=\"Collection(Model.CacheRefreshPlan)\" />\r\n        <NavigationProperty Name=\"ParameterDefinitions\" Type=\"Collection(Model.ReportParameterDefinition)\" />\r\n      </EntityType>\r\n      <ComplexType Name=\"DataSetSchema\">\r\n        <Property Name=\"Name\" Type=\"Edm.String\" />\r\n        <Property Name=\"Fields\" Type=\"Collection(Model.DataSetField)\" />\r\n        <Property Name=\"Parameters\" Type=\"Collection(Model.DataSetParameterInfo)\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"DataSourceCheckResult\">\r\n        <Property Name=\"IsSuccessful\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"ErrorMessage\" Type=\"Edm.String\" />\r\n      </ComplexType>\r\n      <EntityType Name=\"DataSource\" BaseType=\"Model.CatalogItem\">\r\n        <Property Name=\"IsEnabled\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"ConnectionString\" Type=\"Edm.String\" />\r\n        <Property Name=\"DataSourceType\" Type=\"Edm.String\" />\r\n        <Property Name=\"IsOriginalConnectionStringExpressionBased\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"IsConnectionStringOverridden\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"CredentialRetrieval\" Type=\"Model.CredentialRetrievalType\" Nullable=\"false\" />\r\n        <Property Name=\"CredentialsByUser\" Type=\"Model.CredentialsSuppliedByUser\" />\r\n        <Property Name=\"CredentialsInServer\" Type=\"Model.CredentialsStoredInServer\" />\r\n        <Property Name=\"IsReference\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"DSIDNum\" Type=\"Edm.Int64\" />\r\n        <NavigationProperty Name=\"Subscriptions\" Type=\"Collection(Model.Subscription)\" />\r\n      </EntityType>\r\n      <ComplexType Name=\"Query\">\r\n        <Property Name=\"CommandText\" Type=\"Edm.String\" />\r\n        <Property Name=\"Timeout\" Type=\"Edm.Int32\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"CacheOptions\">\r\n        <Property Name=\"ExecutionType\" Type=\"Model.ItemExecutionType\" Nullable=\"false\" />\r\n        <Property Name=\"Expiration\" Type=\"Model.ExpirationReference\" />\r\n      </ComplexType>\r\n      <EntityType Name=\"Comment\">\r\n        <Key>\r\n          <PropertyRef Name=\"Id\" />\r\n        </Key>\r\n        <Property Name=\"Id\" Type=\"Edm.Int64\" Nullable=\"false\" />\r\n        <Property Name=\"ItemId\" Type=\"Edm.Guid\" Nullable=\"false\" />\r\n        <Property Name=\"UserName\" Type=\"Edm.String\" />\r\n        <Property Name=\"ThreadId\" Type=\"Edm.Int64\" />\r\n        <Property Name=\"AttachmentPath\" Type=\"Edm.String\" />\r\n        <Property Name=\"Text\" Type=\"Edm.String\" Nullable=\"false\" />\r\n        <Property Name=\"CreatedDate\" Type=\"Edm.DateTimeOffset\" Nullable=\"false\" />\r\n        <Property Name=\"ModifiedDate\" Type=\"Edm.DateTimeOffset\" />\r\n      </EntityType>\r\n      <EntityType Name=\"Schedule\">\r\n        <Key>\r\n          <PropertyRef Name=\"Id\" />\r\n        </Key>\r\n        <Property Name=\"Id\" Type=\"Edm.Guid\" Nullable=\"false\" />\r\n        <Property Name=\"Name\" Type=\"Edm.String\" />\r\n        <Property Name=\"Definition\" Type=\"Model.ScheduleDefinition\" />\r\n        <Property Name=\"Description\" Type=\"Edm.String\" />\r\n        <Property Name=\"Creator\" Type=\"Edm.String\" />\r\n        <Property Name=\"NextRunTime\" Type=\"Edm.DateTimeOffset\" Nullable=\"false\" />\r\n        <Property Name=\"NextRunTimeSpecified\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"LastRunTime\" Type=\"Edm.DateTimeOffset\" Nullable=\"false\" />\r\n        <Property Name=\"LastRunTimeSpecified\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"ReferencesPresent\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"State\" Type=\"Model.ScheduleStateEnum\" Nullable=\"false\" />\r\n      </EntityType>\r\n      <ComplexType Name=\"MinuteRecurrence\">\r\n        <Property Name=\"MinutesInterval\" Type=\"Edm.Int32\" Nullable=\"false\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"DailyRecurrence\">\r\n        <Property Name=\"DaysInterval\" Type=\"Edm.Int32\" Nullable=\"false\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"WeeklyRecurrence\">\r\n        <Property Name=\"WeeksInterval\" Type=\"Edm.Int32\" Nullable=\"false\" />\r\n        <Property Name=\"WeeksIntervalSpecified\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"DaysOfWeek\" Type=\"Model.DaysOfWeekSelector\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"MonthlyRecurrence\">\r\n        <Property Name=\"Days\" Type=\"Edm.String\" />\r\n        <Property Name=\"MonthsOfYear\" Type=\"Model.MonthsOfYearSelector\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"MonthlyDOWRecurrence\">\r\n        <Property Name=\"WhichWeek\" Type=\"Model.WeekNumberEnum\" Nullable=\"false\" />\r\n        <Property Name=\"WhichWeekSpecified\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"DaysOfWeek\" Type=\"Model.DaysOfWeekSelector\" />\r\n        <Property Name=\"MonthsOfYear\" Type=\"Model.MonthsOfYearSelector\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"ScheduleDefinition\">\r\n        <Property Name=\"StartDateTime\" Type=\"Edm.DateTimeOffset\" Nullable=\"false\" />\r\n        <Property Name=\"EndDate\" Type=\"Edm.DateTimeOffset\" Nullable=\"false\" />\r\n        <Property Name=\"EndDateSpecified\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"Recurrence\" Type=\"Model.ScheduleRecurrence\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"ScheduleReference\">\r\n        <Property Name=\"ScheduleID\" Type=\"Edm.String\" />\r\n        <Property Name=\"Definition\" Type=\"Model.ScheduleDefinition\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"ScheduleRecurrence\">\r\n        <Property Name=\"MinuteRecurrence\" Type=\"Model.MinuteRecurrence\" />\r\n        <Property Name=\"DailyRecurrence\" Type=\"Model.DailyRecurrence\" />\r\n        <Property Name=\"WeeklyRecurrence\" Type=\"Model.WeeklyRecurrence\" />\r\n        <Property Name=\"MonthlyRecurrence\" Type=\"Model.MonthlyRecurrence\" />\r\n        <Property Name=\"MonthlyDOWRecurrence\" Type=\"Model.MonthlyDOWRecurrence\" />\r\n      </ComplexType>\r\n      <EntityType Name=\"ReportServerInfo\">\r\n        <Key>\r\n          <PropertyRef Name=\"Id\" />\r\n        </Key>\r\n        <Property Name=\"Id\" Type=\"Edm.Guid\" Nullable=\"false\" />\r\n        <Property Name=\"ReportServerUrl\" Type=\"Edm.String\" />\r\n        <Property Name=\"VirtualDirectory\" Type=\"Edm.String\" />\r\n        <Property Name=\"WebAppUrl\" Type=\"Edm.String\" />\r\n        <Property Name=\"Roles\" Type=\"Collection(Model.Role)\" />\r\n        <NavigationProperty Name=\"Policies\" Type=\"Collection(Model.SystemPolicy)\" />\r\n      </EntityType>\r\n      <ComplexType Name=\"Extension\">\r\n        <Property Name=\"ExtensionType\" Type=\"Model.ExtensionType\" Nullable=\"false\" />\r\n        <Property Name=\"Name\" Type=\"Edm.String\" />\r\n        <Property Name=\"LocalizedName\" Type=\"Edm.String\" />\r\n        <Property Name=\"Visible\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"Parameters\" Type=\"Collection(Model.ExtensionParameter)\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"ExtensionParameter\">\r\n        <Property Name=\"Name\" Type=\"Edm.String\" />\r\n        <Property Name=\"DisplayName\" Type=\"Edm.String\" />\r\n        <Property Name=\"Required\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"ReadOnly\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"Value\" Type=\"Edm.String\" />\r\n        <Property Name=\"Error\" Type=\"Edm.String\" />\r\n        <Property Name=\"Encrypted\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"IsPassword\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"ValidValues\" Type=\"Collection(Model.ValidValue)\" />\r\n        <Property Name=\"ValidValuesIsNull\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"Policy\">\r\n        <Property Name=\"GroupUserName\" Type=\"Edm.String\" />\r\n        <Property Name=\"Roles\" Type=\"Collection(Model.Role)\" />\r\n      </ComplexType>\r\n      <EntityType Name=\"SystemResource\">\r\n        <Key>\r\n          <PropertyRef Name=\"Id\" />\r\n        </Key>\r\n        <Property Name=\"Id\" Type=\"Edm.Guid\" Nullable=\"false\" />\r\n        <Property Name=\"Type\" Type=\"Model.SystemResourceType\" Nullable=\"false\" />\r\n        <Property [...string is too long...]");
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

			// Token: 0x06000D72 RID: 3442 RVA: 0x0001B0C8 File Offset: 0x000192C8
			[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
			private static XmlReader CreateXmlReader(string edmxToParse)
			{
				return XmlReader.Create(new StringReader(edmxToParse));
			}

			// Token: 0x04000637 RID: 1591
			[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
			private static IEdmModel ParsedModel = Container.GeneratedEdmModel.LoadModelFromString();

			// Token: 0x04000638 RID: 1592
			[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
			private const string Edmx = "<edmx:Edmx Version=\"4.0\" xmlns:edmx=\"http://docs.oasis-open.org/odata/ns/edmx\">\r\n  <edmx:DataServices>\r\n    <Schema Namespace=\"Model\" xmlns=\"http://docs.oasis-open.org/odata/ns/edm\">\r\n      <ComplexType Name=\"KpiDataItem\" Abstract=\"true\">\r\n        <Property Name=\"Type\" Type=\"Model.KpiDataItemType\" Nullable=\"false\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"KpiSharedDataItem\" BaseType=\"Model.KpiDataItem\">\r\n        <Property Name=\"Id\" Type=\"Edm.Guid\" Nullable=\"false\" />\r\n        <Property Name=\"Path\" Type=\"Edm.String\" />\r\n        <Property Name=\"Parameters\" Type=\"Collection(Model.DataSetParameter)\" Nullable=\"false\" />\r\n        <Property Name=\"Aggregation\" Type=\"Model.KpiSharedDataItemAggregation\" Nullable=\"false\" />\r\n        <Property Name=\"Column\" Type=\"Edm.String\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"DataSetParameter\">\r\n        <Property Name=\"Name\" Type=\"Edm.String\" />\r\n        <Property Name=\"Value\" Type=\"Edm.String\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"KpiStaticDataItem\" BaseType=\"Model.KpiDataItem\">\r\n        <Property Name=\"Value\" Type=\"Edm.String\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"DrillthroughTarget\" Abstract=\"true\">\r\n        <Property Name=\"Type\" Type=\"Model.DrillthroughTargetType\" Nullable=\"false\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"CatalogItemDrillthroughTarget\" BaseType=\"Model.DrillthroughTarget\">\r\n        <Property Name=\"CatalogItemType\" Type=\"Model.CatalogItemType\" Nullable=\"false\" />\r\n        <Property Name=\"Path\" Type=\"Edm.String\" />\r\n        <Property Name=\"Id\" Type=\"Edm.Guid\" Nullable=\"false\" />\r\n        <Property Name=\"Parameters\" Type=\"Collection(Model.CatalogItemParameter)\" Nullable=\"false\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"CatalogItemParameter\">\r\n        <Property Name=\"Name\" Type=\"Edm.String\" />\r\n        <Property Name=\"Value\" Type=\"Edm.String\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"UrlDrillthroughTarget\" BaseType=\"Model.DrillthroughTarget\">\r\n        <Property Name=\"Url\" Type=\"Edm.String\" />\r\n        <Property Name=\"DirectNavigation\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n      </ComplexType>\r\n      <EntityType Name=\"Subscription\">\r\n        <Key>\r\n          <PropertyRef Name=\"Id\" />\r\n        </Key>\r\n        <Property Name=\"Id\" Type=\"Edm.Guid\" Nullable=\"false\" />\r\n        <Property Name=\"Owner\" Type=\"Edm.String\" />\r\n        <Property Name=\"IsDataDriven\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"Description\" Type=\"Edm.String\" />\r\n        <Property Name=\"Report\" Type=\"Edm.String\" />\r\n        <Property Name=\"IsActive\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"EventType\" Type=\"Edm.String\" />\r\n        <Property Name=\"Schedule\" Type=\"Model.ScheduleReference\" />\r\n        <Property Name=\"ScheduleDescription\" Type=\"Edm.String\" />\r\n        <Property Name=\"LastRunTime\" Type=\"Edm.DateTimeOffset\" />\r\n        <Property Name=\"LastStatus\" Type=\"Edm.String\" />\r\n        <Property Name=\"DataQuery\" Type=\"Model.Query\" />\r\n        <Property Name=\"ExtensionSettings\" Type=\"Model.ExtensionSettings\" />\r\n        <Property Name=\"DeliveryExtension\" Type=\"Edm.String\" />\r\n        <Property Name=\"LocalizedDeliveryExtensionName\" Type=\"Edm.String\" />\r\n        <Property Name=\"ModifiedBy\" Type=\"Edm.String\" />\r\n        <Property Name=\"ModifiedDate\" Type=\"Edm.DateTimeOffset\" />\r\n        <Property Name=\"ParameterValues\" Type=\"Collection(Model.ParameterValue)\" />\r\n        <NavigationProperty Name=\"DataSource\" Type=\"Model.DataSource\" />\r\n      </EntityType>\r\n      <EntityType Name=\"CacheRefreshPlan\">\r\n        <Key>\r\n          <PropertyRef Name=\"Id\" />\r\n        </Key>\r\n        <Property Name=\"Id\" Type=\"Edm.Guid\" Nullable=\"false\" />\r\n        <Property Name=\"Owner\" Type=\"Edm.String\" />\r\n        <Property Name=\"Description\" Type=\"Edm.String\" />\r\n        <Property Name=\"CatalogItemPath\" Type=\"Edm.String\" />\r\n        <Property Name=\"EventType\" Type=\"Edm.String\" />\r\n        <Property Name=\"Schedule\" Type=\"Model.ScheduleReference\" />\r\n        <Property Name=\"LastRunTime\" Type=\"Edm.DateTimeOffset\" />\r\n        <Property Name=\"LastStatus\" Type=\"Edm.String\" />\r\n        <Property Name=\"ModifiedBy\" Type=\"Edm.String\" />\r\n        <Property Name=\"ModifiedDate\" Type=\"Edm.DateTimeOffset\" />\r\n        <Property Name=\"ParameterValues\" Type=\"Collection(Model.ParameterValue)\" />\r\n      </EntityType>\r\n      <EntityType Name=\"CatalogItem\" Abstract=\"true\">\r\n        <Key>\r\n          <PropertyRef Name=\"Id\" />\r\n        </Key>\r\n        <Property Name=\"Id\" Type=\"Edm.Guid\" Nullable=\"false\" />\r\n        <Property Name=\"Name\" Type=\"Edm.String\" />\r\n        <Property Name=\"Description\" Type=\"Edm.String\" />\r\n        <Property Name=\"Path\" Type=\"Edm.String\" />\r\n        <Property Name=\"Type\" Type=\"Model.CatalogItemType\" Nullable=\"false\" />\r\n        <Property Name=\"Hidden\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"Size\" Type=\"Edm.Int64\" Nullable=\"false\" />\r\n        <Property Name=\"ModifiedBy\" Type=\"Edm.String\" />\r\n        <Property Name=\"ModifiedDate\" Type=\"Edm.DateTimeOffset\" Nullable=\"false\" />\r\n        <Property Name=\"CreatedBy\" Type=\"Edm.String\" />\r\n        <Property Name=\"CreatedDate\" Type=\"Edm.DateTimeOffset\" Nullable=\"false\" />\r\n        <Property Name=\"ParentFolderId\" Type=\"Edm.Guid\" />\r\n        <Property Name=\"ContentType\" Type=\"Edm.String\" />\r\n        <Property Name=\"Content\" Type=\"Edm.Binary\" />\r\n        <Property Name=\"Properties\" Type=\"Collection(Model.Property)\" />\r\n        <Property Name=\"IsFavorite\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"Policies\" Type=\"Collection(Model.ItemPolicy)\" />\r\n        <Property Name=\"Roles\" Type=\"Collection(Model.Role)\" />\r\n        <NavigationProperty Name=\"ParentFolder\" Type=\"Model.Folder\" />\r\n        <NavigationProperty Name=\"Comments\" Type=\"Collection(Model.Comment)\" />\r\n        <NavigationProperty Name=\"AlertSubscriptions\" Type=\"Collection(Model.AlertSubscription)\" />\r\n        <NavigationProperty Name=\"AllowedActions\" Type=\"Collection(Model.AllowedAction)\" />\r\n        <NavigationProperty Name=\"DependentItems\" Type=\"Collection(Model.CatalogItem)\" />\r\n      </EntityType>\r\n      <ComplexType Name=\"Property\">\r\n        <Property Name=\"Name\" Type=\"Edm.String\" />\r\n        <Property Name=\"Value\" Type=\"Edm.String\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"BulkOperationsResult\">\r\n        <Property Name=\"FailedOperations\" Type=\"Collection(Edm.String)\" />\r\n        <Property Name=\"HasErrors\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"Role\">\r\n        <Property Name=\"Name\" Type=\"Edm.String\" />\r\n        <Property Name=\"Description\" Type=\"Edm.String\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"ItemPolicy\">\r\n        <Property Name=\"Id\" Type=\"Edm.Guid\" Nullable=\"false\" />\r\n        <Property Name=\"InheritParentPolicy\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"Policies\" Type=\"Collection(Model.Policy)\" />\r\n      </ComplexType>\r\n      <EntityType Name=\"ReportParameterDefinition\">\r\n        <Key>\r\n          <PropertyRef Name=\"Name\" />\r\n        </Key>\r\n        <Property Name=\"Name\" Type=\"Edm.String\" Nullable=\"false\" />\r\n        <Property Name=\"ParameterType\" Type=\"Model.ReportParameterType\" Nullable=\"false\" />\r\n        <Property Name=\"ParameterVisibility\" Type=\"Model.ReportParameterVisibility\" Nullable=\"false\" />\r\n        <Property Name=\"ParameterState\" Type=\"Model.ReportParameterState\" Nullable=\"false\" />\r\n        <Property Name=\"ValidValues\" Type=\"Collection(Model.ValidValue)\" />\r\n        <Property Name=\"ValidValuesIsNull\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"Nullable\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"AllowBlank\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"MultiValue\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"Prompt\" Type=\"Edm.String\" />\r\n        <Property Name=\"PromptUser\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"QueryParameter\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"DefaultValuesQueryBased\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"ValidValuesQueryBased\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"Dependencies\" Type=\"Collection(Edm.String)\" />\r\n        <Property Name=\"DefaultValues\" Type=\"Collection(Edm.String)\" />\r\n        <Property Name=\"DefaultValuesIsNull\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"ErrorMessage\" Type=\"Edm.String\" />\r\n      </EntityType>\r\n      <EntityType Name=\"Report\" BaseType=\"Model.CatalogItem\">\r\n        <Property Name=\"HasDataSources\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"HasSharedDataSets\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"HasParameters\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <NavigationProperty Name=\"Subscriptions\" Type=\"Collection(Model.Subscription)\" />\r\n        <NavigationProperty Name=\"CacheRefreshPlans\" Type=\"Collection(Model.CacheRefreshPlan)\" />\r\n        <NavigationProperty Name=\"DataSources\" Type=\"Collection(Model.DataSource)\" />\r\n        <NavigationProperty Name=\"SharedDataSets\" Type=\"Collection(Model.DataSet)\" />\r\n        <NavigationProperty Name=\"HistorySnapshotOptions\" Type=\"Model.HistorySnapshotOptions\" />\r\n        <NavigationProperty Name=\"ReportHistorySnapshots\" Type=\"Collection(Model.ReportHistorySnapshot)\" />\r\n        <NavigationProperty Name=\"HistorySnapshots\" Type=\"Collection(Model.HistorySnapshot)\" />\r\n        <NavigationProperty Name=\"ParameterDefinitions\" Type=\"Collection(Model.ReportParameterDefinition)\" />\r\n      </EntityType>\r\n      <ComplexType Name=\"ParameterValue\">\r\n        <Property Name=\"Name\" Type=\"Edm.String\" />\r\n        <Property Name=\"Value\" Type=\"Edm.String\" />\r\n        <Property Name=\"IsValueFieldReference\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"ReportHistorySnapshotsOptions\">\r\n        <Property Name=\"ManualCreationEnabled\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"KeepExecutionSnapshots\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"UseDefaultSystemLimit\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"ScopedLimit\" Type=\"Edm.Int32\" Nullable=\"false\" />\r\n        <Property Name=\"SystemLimit\" Type=\"Edm.Int32\" Nullable=\"false\" />\r\n        <Property Name=\"Schedule\" Type=\"Model.ScheduleReference\" />\r\n      </ComplexType>\r\n      <EntityType Name=\"LinkedReport\" BaseType=\"Model.CatalogItem\">\r\n        <Property Name=\"HasParameters\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"Link\" Type=\"Edm.String\" />\r\n        <NavigationProperty Name=\"Subscriptions\" Type=\"Collection(Model.Subscription)\" />\r\n        <NavigationProperty Name=\"CacheRefreshPlans\" Type=\"Collection(Model.CacheRefreshPlan)\" />\r\n        <NavigationProperty Name=\"HistorySnapshotOptions\" Type=\"Model.HistorySnapshotOptions\" />\r\n        <NavigationProperty Name=\"ReportHistorySnapshots\" Type=\"Collection(Model.ReportHistorySnapshot)\" />\r\n        <NavigationProperty Name=\"HistorySnapshots\" Type=\"Collection(Model.HistorySnapshot)\" />\r\n        <NavigationProperty Name=\"ParameterDefinitions\" Type=\"Collection(Model.ReportParameterDefinition)\" />\r\n      </EntityType>\r\n      <ComplexType Name=\"ItemHistoryOptions\">\r\n        <Property Name=\"EnableManualSnapshotCreation\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"KeepExecutionSnapshots\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"Schedule\" Type=\"Model.ScheduleReference\" />\r\n      </ComplexType>\r\n      <EntityType Name=\"DataSet\" BaseType=\"Model.CatalogItem\">\r\n        <Property Name=\"HasParameters\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"QueryExecutionTimeOut\" Type=\"Edm.Int32\" Nullable=\"false\" />\r\n        <NavigationProperty Name=\"DataSources\" Type=\"Collection(Model.DataSource)\" />\r\n        <NavigationProperty Name=\"CacheRefreshPlans\" Type=\"Collection(Model.CacheRefreshPlan)\" />\r\n        <NavigationProperty Name=\"ParameterDefinitions\" Type=\"Collection(Model.ReportParameterDefinition)\" />\r\n      </EntityType>\r\n      <ComplexType Name=\"DataSetSchema\">\r\n        <Property Name=\"Name\" Type=\"Edm.String\" />\r\n        <Property Name=\"Fields\" Type=\"Collection(Model.DataSetField)\" />\r\n        <Property Name=\"Parameters\" Type=\"Collection(Model.DataSetParameterInfo)\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"DataSourceCheckResult\">\r\n        <Property Name=\"IsSuccessful\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"ErrorMessage\" Type=\"Edm.String\" />\r\n      </ComplexType>\r\n      <EntityType Name=\"DataSource\" BaseType=\"Model.CatalogItem\">\r\n        <Property Name=\"IsEnabled\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"ConnectionString\" Type=\"Edm.String\" />\r\n        <Property Name=\"DataSourceType\" Type=\"Edm.String\" />\r\n        <Property Name=\"IsOriginalConnectionStringExpressionBased\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"IsConnectionStringOverridden\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"CredentialRetrieval\" Type=\"Model.CredentialRetrievalType\" Nullable=\"false\" />\r\n        <Property Name=\"CredentialsByUser\" Type=\"Model.CredentialsSuppliedByUser\" />\r\n        <Property Name=\"CredentialsInServer\" Type=\"Model.CredentialsStoredInServer\" />\r\n        <Property Name=\"IsReference\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"DSIDNum\" Type=\"Edm.Int64\" />\r\n        <NavigationProperty Name=\"Subscriptions\" Type=\"Collection(Model.Subscription)\" />\r\n      </EntityType>\r\n      <ComplexType Name=\"Query\">\r\n        <Property Name=\"CommandText\" Type=\"Edm.String\" />\r\n        <Property Name=\"Timeout\" Type=\"Edm.Int32\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"CacheOptions\">\r\n        <Property Name=\"ExecutionType\" Type=\"Model.ItemExecutionType\" Nullable=\"false\" />\r\n        <Property Name=\"Expiration\" Type=\"Model.ExpirationReference\" />\r\n      </ComplexType>\r\n      <EntityType Name=\"Comment\">\r\n        <Key>\r\n          <PropertyRef Name=\"Id\" />\r\n        </Key>\r\n        <Property Name=\"Id\" Type=\"Edm.Int64\" Nullable=\"false\" />\r\n        <Property Name=\"ItemId\" Type=\"Edm.Guid\" Nullable=\"false\" />\r\n        <Property Name=\"UserName\" Type=\"Edm.String\" />\r\n        <Property Name=\"ThreadId\" Type=\"Edm.Int64\" />\r\n        <Property Name=\"AttachmentPath\" Type=\"Edm.String\" />\r\n        <Property Name=\"Text\" Type=\"Edm.String\" Nullable=\"false\" />\r\n        <Property Name=\"CreatedDate\" Type=\"Edm.DateTimeOffset\" Nullable=\"false\" />\r\n        <Property Name=\"ModifiedDate\" Type=\"Edm.DateTimeOffset\" />\r\n      </EntityType>\r\n      <EntityType Name=\"Schedule\">\r\n        <Key>\r\n          <PropertyRef Name=\"Id\" />\r\n        </Key>\r\n        <Property Name=\"Id\" Type=\"Edm.Guid\" Nullable=\"false\" />\r\n        <Property Name=\"Name\" Type=\"Edm.String\" />\r\n        <Property Name=\"Definition\" Type=\"Model.ScheduleDefinition\" />\r\n        <Property Name=\"Description\" Type=\"Edm.String\" />\r\n        <Property Name=\"Creator\" Type=\"Edm.String\" />\r\n        <Property Name=\"NextRunTime\" Type=\"Edm.DateTimeOffset\" Nullable=\"false\" />\r\n        <Property Name=\"NextRunTimeSpecified\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"LastRunTime\" Type=\"Edm.DateTimeOffset\" Nullable=\"false\" />\r\n        <Property Name=\"LastRunTimeSpecified\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"ReferencesPresent\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"State\" Type=\"Model.ScheduleStateEnum\" Nullable=\"false\" />\r\n      </EntityType>\r\n      <ComplexType Name=\"MinuteRecurrence\">\r\n        <Property Name=\"MinutesInterval\" Type=\"Edm.Int32\" Nullable=\"false\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"DailyRecurrence\">\r\n        <Property Name=\"DaysInterval\" Type=\"Edm.Int32\" Nullable=\"false\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"WeeklyRecurrence\">\r\n        <Property Name=\"WeeksInterval\" Type=\"Edm.Int32\" Nullable=\"false\" />\r\n        <Property Name=\"WeeksIntervalSpecified\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"DaysOfWeek\" Type=\"Model.DaysOfWeekSelector\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"MonthlyRecurrence\">\r\n        <Property Name=\"Days\" Type=\"Edm.String\" />\r\n        <Property Name=\"MonthsOfYear\" Type=\"Model.MonthsOfYearSelector\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"MonthlyDOWRecurrence\">\r\n        <Property Name=\"WhichWeek\" Type=\"Model.WeekNumberEnum\" Nullable=\"false\" />\r\n        <Property Name=\"WhichWeekSpecified\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"DaysOfWeek\" Type=\"Model.DaysOfWeekSelector\" />\r\n        <Property Name=\"MonthsOfYear\" Type=\"Model.MonthsOfYearSelector\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"ScheduleDefinition\">\r\n        <Property Name=\"StartDateTime\" Type=\"Edm.DateTimeOffset\" Nullable=\"false\" />\r\n        <Property Name=\"EndDate\" Type=\"Edm.DateTimeOffset\" Nullable=\"false\" />\r\n        <Property Name=\"EndDateSpecified\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"Recurrence\" Type=\"Model.ScheduleRecurrence\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"ScheduleReference\">\r\n        <Property Name=\"ScheduleID\" Type=\"Edm.String\" />\r\n        <Property Name=\"Definition\" Type=\"Model.ScheduleDefinition\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"ScheduleRecurrence\">\r\n        <Property Name=\"MinuteRecurrence\" Type=\"Model.MinuteRecurrence\" />\r\n        <Property Name=\"DailyRecurrence\" Type=\"Model.DailyRecurrence\" />\r\n        <Property Name=\"WeeklyRecurrence\" Type=\"Model.WeeklyRecurrence\" />\r\n        <Property Name=\"MonthlyRecurrence\" Type=\"Model.MonthlyRecurrence\" />\r\n        <Property Name=\"MonthlyDOWRecurrence\" Type=\"Model.MonthlyDOWRecurrence\" />\r\n      </ComplexType>\r\n      <EntityType Name=\"ReportServerInfo\">\r\n        <Key>\r\n          <PropertyRef Name=\"Id\" />\r\n        </Key>\r\n        <Property Name=\"Id\" Type=\"Edm.Guid\" Nullable=\"false\" />\r\n        <Property Name=\"ReportServerUrl\" Type=\"Edm.String\" />\r\n        <Property Name=\"VirtualDirectory\" Type=\"Edm.String\" />\r\n        <Property Name=\"WebAppUrl\" Type=\"Edm.String\" />\r\n        <Property Name=\"Roles\" Type=\"Collection(Model.Role)\" />\r\n        <NavigationProperty Name=\"Policies\" Type=\"Collection(Model.SystemPolicy)\" />\r\n      </EntityType>\r\n      <ComplexType Name=\"Extension\">\r\n        <Property Name=\"ExtensionType\" Type=\"Model.ExtensionType\" Nullable=\"false\" />\r\n        <Property Name=\"Name\" Type=\"Edm.String\" />\r\n        <Property Name=\"LocalizedName\" Type=\"Edm.String\" />\r\n        <Property Name=\"Visible\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"Parameters\" Type=\"Collection(Model.ExtensionParameter)\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"ExtensionParameter\">\r\n        <Property Name=\"Name\" Type=\"Edm.String\" />\r\n        <Property Name=\"DisplayName\" Type=\"Edm.String\" />\r\n        <Property Name=\"Required\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"ReadOnly\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"Value\" Type=\"Edm.String\" />\r\n        <Property Name=\"Error\" Type=\"Edm.String\" />\r\n        <Property Name=\"Encrypted\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"IsPassword\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n        <Property Name=\"ValidValues\" Type=\"Collection(Model.ValidValue)\" />\r\n        <Property Name=\"ValidValuesIsNull\" Type=\"Edm.Boolean\" Nullable=\"false\" />\r\n      </ComplexType>\r\n      <ComplexType Name=\"Policy\">\r\n        <Property Name=\"GroupUserName\" Type=\"Edm.String\" />\r\n        <Property Name=\"Roles\" Type=\"Collection(Model.Role)\" />\r\n      </ComplexType>\r\n      <EntityType Name=\"SystemResource\">\r\n        <Key>\r\n          <PropertyRef Name=\"Id\" />\r\n        </Key>\r\n        <Property Name=\"Id\" Type=\"Edm.Guid\" Nullable=\"false\" />\r\n        <Property Name=\"Type\" Type=\"Model.SystemResourceType\" Nullable=\"false\" />\r\n        <Property [...string is too long...]";
		}
	}
}
