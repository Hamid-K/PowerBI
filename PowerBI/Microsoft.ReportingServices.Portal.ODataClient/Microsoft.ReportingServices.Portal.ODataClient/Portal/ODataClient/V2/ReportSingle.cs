using System;
using System.CodeDom.Compiler;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000043 RID: 67
	[OriginalName("ReportSingle")]
	public class ReportSingle : DataServiceQuerySingle<Report>
	{
		// Token: 0x060002CA RID: 714 RVA: 0x00006EE0 File Offset: 0x000050E0
		public ReportSingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00006EEA File Offset: 0x000050EA
		public ReportSingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00006EF5 File Offset: 0x000050F5
		public ReportSingle(DataServiceQuerySingle<Report> query)
			: base(query)
		{
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060002CD RID: 717 RVA: 0x00006EFE File Offset: 0x000050FE
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

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060002CE RID: 718 RVA: 0x00006F3D File Offset: 0x0000513D
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

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060002CF RID: 719 RVA: 0x00006F7C File Offset: 0x0000517C
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

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x00006FBB File Offset: 0x000051BB
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

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x00006FFA File Offset: 0x000051FA
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

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x00007039 File Offset: 0x00005239
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

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x00007078 File Offset: 0x00005278
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

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x000070B7 File Offset: 0x000052B7
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

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x000070F6 File Offset: 0x000052F6
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("CacheOptions")]
		public CacheOptionsSingle CacheOptions
		{
			get
			{
				if (!base.IsComposable)
				{
					throw new NotSupportedException("The previous function is not composable.");
				}
				if (this._CacheOptions == null)
				{
					this._CacheOptions = new CacheOptionsSingle(base.Context, base.GetPath("CacheOptions"));
				}
				return this._CacheOptions;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x00007135 File Offset: 0x00005335
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

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x00007174 File Offset: 0x00005374
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Properties")]
		public DataServiceQuery<Property> Properties
		{
			get
			{
				if (!base.IsComposable)
				{
					throw new NotSupportedException("The previous function is not composable.");
				}
				if (this._Properties == null)
				{
					this._Properties = base.Context.CreateQuery<Property>(base.GetPath("Properties"));
				}
				return this._Properties;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x000071B3 File Offset: 0x000053B3
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

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x000071F2 File Offset: 0x000053F2
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

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060002DA RID: 730 RVA: 0x00007231 File Offset: 0x00005431
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

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060002DB RID: 731 RVA: 0x00007270 File Offset: 0x00005470
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Policies")]
		public DataServiceQuery<ItemPolicy> Policies
		{
			get
			{
				if (!base.IsComposable)
				{
					throw new NotSupportedException("The previous function is not composable.");
				}
				if (this._Policies == null)
				{
					this._Policies = base.Context.CreateQuery<ItemPolicy>(base.GetPath("Policies"));
				}
				return this._Policies;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060002DC RID: 732 RVA: 0x000072AF File Offset: 0x000054AF
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

		// Token: 0x04000167 RID: 359
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<Subscription> _Subscriptions;

		// Token: 0x04000168 RID: 360
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<CacheRefreshPlan> _CacheRefreshPlans;

		// Token: 0x04000169 RID: 361
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<DataSource> _DataSources;

		// Token: 0x0400016A RID: 362
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<DataSet> _SharedDataSets;

		// Token: 0x0400016B RID: 363
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private HistorySnapshotOptionsSingle _HistorySnapshotOptions;

		// Token: 0x0400016C RID: 364
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<ReportHistorySnapshot> _ReportHistorySnapshots;

		// Token: 0x0400016D RID: 365
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<HistorySnapshot> _HistorySnapshots;

		// Token: 0x0400016E RID: 366
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<ReportParameterDefinition> _ParameterDefinitions;

		// Token: 0x0400016F RID: 367
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private CacheOptionsSingle _CacheOptions;

		// Token: 0x04000170 RID: 368
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private FolderSingle _ParentFolder;

		// Token: 0x04000171 RID: 369
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<Property> _Properties;

		// Token: 0x04000172 RID: 370
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<Comment> _Comments;

		// Token: 0x04000173 RID: 371
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<AlertSubscription> _AlertSubscriptions;

		// Token: 0x04000174 RID: 372
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<AllowedAction> _AllowedActions;

		// Token: 0x04000175 RID: 373
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<ItemPolicy> _Policies;

		// Token: 0x04000176 RID: 374
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<CatalogItem> _DependentItems;
	}
}
