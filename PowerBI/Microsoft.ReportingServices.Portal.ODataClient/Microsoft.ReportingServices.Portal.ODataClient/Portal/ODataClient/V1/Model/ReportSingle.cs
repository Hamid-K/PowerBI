using System;
using System.CodeDom.Compiler;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000BD RID: 189
	[OriginalName("ReportSingle")]
	public class ReportSingle : DataServiceQuerySingle<Report>
	{
		// Token: 0x06000828 RID: 2088 RVA: 0x00010596 File Offset: 0x0000E796
		public ReportSingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x000105A0 File Offset: 0x0000E7A0
		public ReportSingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x000105AB File Offset: 0x0000E7AB
		public ReportSingle(DataServiceQuerySingle<Report> query)
			: base(query)
		{
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x0600082B RID: 2091 RVA: 0x000105B4 File Offset: 0x0000E7B4
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

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x0600082C RID: 2092 RVA: 0x000105F3 File Offset: 0x0000E7F3
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

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x0600082D RID: 2093 RVA: 0x00010632 File Offset: 0x0000E832
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

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x0600082E RID: 2094 RVA: 0x00010671 File Offset: 0x0000E871
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("SharedDataSets")]
		public DataServiceQuery<DataSet> SharedDataSets
		{
			get
			{
				if (!base.IsComposable)
				{
					throw new NotSupportedException("The previous function is not composable.");
				}
				if (this._SharedDataSets == null)
				{
					this._SharedDataSets = base.Context.CreateQuery<DataSet>(base.GetPath("SharedDataSets"));
				}
				return this._SharedDataSets;
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x0600082F RID: 2095 RVA: 0x000106B0 File Offset: 0x0000E8B0
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

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000830 RID: 2096 RVA: 0x000106EF File Offset: 0x0000E8EF
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

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000831 RID: 2097 RVA: 0x0001072E File Offset: 0x0000E92E
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

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000832 RID: 2098 RVA: 0x0001076D File Offset: 0x0000E96D
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

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000833 RID: 2099 RVA: 0x000107AC File Offset: 0x0000E9AC
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

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000834 RID: 2100 RVA: 0x000107EB File Offset: 0x0000E9EB
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

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000835 RID: 2101 RVA: 0x0001082A File Offset: 0x0000EA2A
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

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000836 RID: 2102 RVA: 0x00010869 File Offset: 0x0000EA69
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

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000837 RID: 2103 RVA: 0x000108A8 File Offset: 0x0000EAA8
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

		// Token: 0x040003E8 RID: 1000
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<Subscription> _Subscriptions;

		// Token: 0x040003E9 RID: 1001
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<CacheRefreshPlan> _CacheRefreshPlans;

		// Token: 0x040003EA RID: 1002
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<DataSource> _DataSources;

		// Token: 0x040003EB RID: 1003
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<DataSet> _SharedDataSets;

		// Token: 0x040003EC RID: 1004
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private HistorySnapshotOptionsSingle _HistorySnapshotOptions;

		// Token: 0x040003ED RID: 1005
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<ReportHistorySnapshot> _ReportHistorySnapshots;

		// Token: 0x040003EE RID: 1006
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<HistorySnapshot> _HistorySnapshots;

		// Token: 0x040003EF RID: 1007
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<ReportParameterDefinition> _ParameterDefinitions;

		// Token: 0x040003F0 RID: 1008
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private FolderSingle _ParentFolder;

		// Token: 0x040003F1 RID: 1009
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<Comment> _Comments;

		// Token: 0x040003F2 RID: 1010
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<AlertSubscription> _AlertSubscriptions;

		// Token: 0x040003F3 RID: 1011
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<AllowedAction> _AllowedActions;

		// Token: 0x040003F4 RID: 1012
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<CatalogItem> _DependentItems;
	}
}
