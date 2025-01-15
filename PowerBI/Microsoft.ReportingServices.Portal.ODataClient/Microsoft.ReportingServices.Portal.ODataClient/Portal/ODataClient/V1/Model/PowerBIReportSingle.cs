using System;
using System.CodeDom.Compiler;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000F6 RID: 246
	[OriginalName("PowerBIReportSingle")]
	public class PowerBIReportSingle : DataServiceQuerySingle<PowerBIReport>
	{
		// Token: 0x06000AD5 RID: 2773 RVA: 0x000156B1 File Offset: 0x000138B1
		public PowerBIReportSingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x000156BB File Offset: 0x000138BB
		public PowerBIReportSingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x000156C6 File Offset: 0x000138C6
		public PowerBIReportSingle(DataServiceQuerySingle<PowerBIReport> query)
			: base(query)
		{
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000AD8 RID: 2776 RVA: 0x000156CF File Offset: 0x000138CF
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("DataSources")]
		public DataServiceQuery<DataSource> DataSources
		{
			get
			{
				if (!base.IsComposable)
				{
					throw new NotSupportedException("The previous function is not composable.");
				}
				if (this._DataSources == null)
				{
					this._DataSources = base.Context.CreateQuery<DataSource>(base.GetPath("DataSources"));
				}
				return this._DataSources;
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000AD9 RID: 2777 RVA: 0x0001570E File Offset: 0x0001390E
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("DataModelDataSources")]
		public DataServiceQuery<DataModelDataSource> DataModelDataSources
		{
			get
			{
				if (!base.IsComposable)
				{
					throw new NotSupportedException("The previous function is not composable.");
				}
				if (this._DataModelDataSources == null)
				{
					this._DataModelDataSources = base.Context.CreateQuery<DataModelDataSource>(base.GetPath("DataModelDataSources"));
				}
				return this._DataModelDataSources;
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000ADA RID: 2778 RVA: 0x0001574D File Offset: 0x0001394D
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("CacheRefreshPlans")]
		public DataServiceQuery<CacheRefreshPlan> CacheRefreshPlans
		{
			get
			{
				if (!base.IsComposable)
				{
					throw new NotSupportedException("The previous function is not composable.");
				}
				if (this._CacheRefreshPlans == null)
				{
					this._CacheRefreshPlans = base.Context.CreateQuery<CacheRefreshPlan>(base.GetPath("CacheRefreshPlans"));
				}
				return this._CacheRefreshPlans;
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000ADB RID: 2779 RVA: 0x0001578C File Offset: 0x0001398C
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("ParentFolder")]
		public FolderSingle ParentFolder
		{
			get
			{
				if (!base.IsComposable)
				{
					throw new NotSupportedException("The previous function is not composable.");
				}
				if (this._ParentFolder == null)
				{
					this._ParentFolder = new FolderSingle(base.Context, base.GetPath("ParentFolder"));
				}
				return this._ParentFolder;
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x000157CB File Offset: 0x000139CB
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Comments")]
		public DataServiceQuery<Comment> Comments
		{
			get
			{
				if (!base.IsComposable)
				{
					throw new NotSupportedException("The previous function is not composable.");
				}
				if (this._Comments == null)
				{
					this._Comments = base.Context.CreateQuery<Comment>(base.GetPath("Comments"));
				}
				return this._Comments;
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000ADD RID: 2781 RVA: 0x0001580A File Offset: 0x00013A0A
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("AlertSubscriptions")]
		public DataServiceQuery<AlertSubscription> AlertSubscriptions
		{
			get
			{
				if (!base.IsComposable)
				{
					throw new NotSupportedException("The previous function is not composable.");
				}
				if (this._AlertSubscriptions == null)
				{
					this._AlertSubscriptions = base.Context.CreateQuery<AlertSubscription>(base.GetPath("AlertSubscriptions"));
				}
				return this._AlertSubscriptions;
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000ADE RID: 2782 RVA: 0x00015849 File Offset: 0x00013A49
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("AllowedActions")]
		public DataServiceQuery<AllowedAction> AllowedActions
		{
			get
			{
				if (!base.IsComposable)
				{
					throw new NotSupportedException("The previous function is not composable.");
				}
				if (this._AllowedActions == null)
				{
					this._AllowedActions = base.Context.CreateQuery<AllowedAction>(base.GetPath("AllowedActions"));
				}
				return this._AllowedActions;
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000ADF RID: 2783 RVA: 0x00015888 File Offset: 0x00013A88
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("DependentItems")]
		public DataServiceQuery<CatalogItem> DependentItems
		{
			get
			{
				if (!base.IsComposable)
				{
					throw new NotSupportedException("The previous function is not composable.");
				}
				if (this._DependentItems == null)
				{
					this._DependentItems = base.Context.CreateQuery<CatalogItem>(base.GetPath("DependentItems"));
				}
				return this._DependentItems;
			}
		}

		// Token: 0x040004F3 RID: 1267
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<DataSource> _DataSources;

		// Token: 0x040004F4 RID: 1268
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<DataModelDataSource> _DataModelDataSources;

		// Token: 0x040004F5 RID: 1269
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<CacheRefreshPlan> _CacheRefreshPlans;

		// Token: 0x040004F6 RID: 1270
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private FolderSingle _ParentFolder;

		// Token: 0x040004F7 RID: 1271
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<Comment> _Comments;

		// Token: 0x040004F8 RID: 1272
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<AlertSubscription> _AlertSubscriptions;

		// Token: 0x040004F9 RID: 1273
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<AllowedAction> _AllowedActions;

		// Token: 0x040004FA RID: 1274
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<CatalogItem> _DependentItems;
	}
}
