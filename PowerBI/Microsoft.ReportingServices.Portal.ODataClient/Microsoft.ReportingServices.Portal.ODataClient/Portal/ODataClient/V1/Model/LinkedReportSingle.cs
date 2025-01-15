using System;
using System.CodeDom.Compiler;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000C1 RID: 193
	[OriginalName("LinkedReportSingle")]
	public class LinkedReportSingle : DataServiceQuerySingle<LinkedReport>
	{
		// Token: 0x06000879 RID: 2169 RVA: 0x00011319 File Offset: 0x0000F519
		public LinkedReportSingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x00011323 File Offset: 0x0000F523
		public LinkedReportSingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x0001132E File Offset: 0x0000F52E
		public LinkedReportSingle(DataServiceQuerySingle<LinkedReport> query)
			: base(query)
		{
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x0600087C RID: 2172 RVA: 0x00011337 File Offset: 0x0000F537
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Subscriptions")]
		public DataServiceQuery<Subscription> Subscriptions
		{
			get
			{
				if (!base.IsComposable)
				{
					throw new NotSupportedException("The previous function is not composable.");
				}
				if (this._Subscriptions == null)
				{
					this._Subscriptions = base.Context.CreateQuery<Subscription>(base.GetPath("Subscriptions"));
				}
				return this._Subscriptions;
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x0600087D RID: 2173 RVA: 0x00011376 File Offset: 0x0000F576
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

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x0600087E RID: 2174 RVA: 0x000113B5 File Offset: 0x0000F5B5
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("HistorySnapshotOptions")]
		public HistorySnapshotOptionsSingle HistorySnapshotOptions
		{
			get
			{
				if (!base.IsComposable)
				{
					throw new NotSupportedException("The previous function is not composable.");
				}
				if (this._HistorySnapshotOptions == null)
				{
					this._HistorySnapshotOptions = new HistorySnapshotOptionsSingle(base.Context, base.GetPath("HistorySnapshotOptions"));
				}
				return this._HistorySnapshotOptions;
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x0600087F RID: 2175 RVA: 0x000113F4 File Offset: 0x0000F5F4
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("ReportHistorySnapshots")]
		public DataServiceQuery<ReportHistorySnapshot> ReportHistorySnapshots
		{
			get
			{
				if (!base.IsComposable)
				{
					throw new NotSupportedException("The previous function is not composable.");
				}
				if (this._ReportHistorySnapshots == null)
				{
					this._ReportHistorySnapshots = base.Context.CreateQuery<ReportHistorySnapshot>(base.GetPath("ReportHistorySnapshots"));
				}
				return this._ReportHistorySnapshots;
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000880 RID: 2176 RVA: 0x00011433 File Offset: 0x0000F633
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("HistorySnapshots")]
		public DataServiceQuery<HistorySnapshot> HistorySnapshots
		{
			get
			{
				if (!base.IsComposable)
				{
					throw new NotSupportedException("The previous function is not composable.");
				}
				if (this._HistorySnapshots == null)
				{
					this._HistorySnapshots = base.Context.CreateQuery<HistorySnapshot>(base.GetPath("HistorySnapshots"));
				}
				return this._HistorySnapshots;
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000881 RID: 2177 RVA: 0x00011472 File Offset: 0x0000F672
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("ParameterDefinitions")]
		public DataServiceQuery<ReportParameterDefinition> ParameterDefinitions
		{
			get
			{
				if (!base.IsComposable)
				{
					throw new NotSupportedException("The previous function is not composable.");
				}
				if (this._ParameterDefinitions == null)
				{
					this._ParameterDefinitions = base.Context.CreateQuery<ReportParameterDefinition>(base.GetPath("ParameterDefinitions"));
				}
				return this._ParameterDefinitions;
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000882 RID: 2178 RVA: 0x000114B1 File Offset: 0x0000F6B1
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

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000883 RID: 2179 RVA: 0x000114F0 File Offset: 0x0000F6F0
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

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000884 RID: 2180 RVA: 0x0001152F File Offset: 0x0000F72F
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

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000885 RID: 2181 RVA: 0x0001156E File Offset: 0x0000F76E
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

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000886 RID: 2182 RVA: 0x000115AD File Offset: 0x0000F7AD
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

		// Token: 0x0400040B RID: 1035
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<Subscription> _Subscriptions;

		// Token: 0x0400040C RID: 1036
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<CacheRefreshPlan> _CacheRefreshPlans;

		// Token: 0x0400040D RID: 1037
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private HistorySnapshotOptionsSingle _HistorySnapshotOptions;

		// Token: 0x0400040E RID: 1038
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<ReportHistorySnapshot> _ReportHistorySnapshots;

		// Token: 0x0400040F RID: 1039
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<HistorySnapshot> _HistorySnapshots;

		// Token: 0x04000410 RID: 1040
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<ReportParameterDefinition> _ParameterDefinitions;

		// Token: 0x04000411 RID: 1041
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private FolderSingle _ParentFolder;

		// Token: 0x04000412 RID: 1042
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<Comment> _Comments;

		// Token: 0x04000413 RID: 1043
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<AlertSubscription> _AlertSubscriptions;

		// Token: 0x04000414 RID: 1044
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<AllowedAction> _AllowedActions;

		// Token: 0x04000415 RID: 1045
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<CatalogItem> _DependentItems;
	}
}
