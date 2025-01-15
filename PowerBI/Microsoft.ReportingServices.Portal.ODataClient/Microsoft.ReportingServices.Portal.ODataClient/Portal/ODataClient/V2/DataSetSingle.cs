using System;
using System.CodeDom.Compiler;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000022 RID: 34
	[OriginalName("DataSetSingle")]
	public class DataSetSingle : DataServiceQuerySingle<DataSet>
	{
		// Token: 0x0600015C RID: 348 RVA: 0x00003F05 File Offset: 0x00002105
		public DataSetSingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00003F0F File Offset: 0x0000210F
		public DataSetSingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00003F1A File Offset: 0x0000211A
		public DataSetSingle(DataServiceQuerySingle<DataSet> query)
			: base(query)
		{
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00003F23 File Offset: 0x00002123
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Data")]
		public DataServiceQuery<DataSetRow> Data
		{
			get
			{
				if (!base.IsComposable)
				{
					throw new NotSupportedException("The previous function is not composable.");
				}
				if (this._Data == null)
				{
					this._Data = base.Context.CreateQuery<DataSetRow>(base.GetPath("Data"));
				}
				return this._Data;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00003F62 File Offset: 0x00002162
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

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00003FA1 File Offset: 0x000021A1
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

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00003FE0 File Offset: 0x000021E0
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

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000163 RID: 355 RVA: 0x0000401F File Offset: 0x0000221F
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

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000164 RID: 356 RVA: 0x0000405E File Offset: 0x0000225E
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

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000165 RID: 357 RVA: 0x0000409D File Offset: 0x0000229D
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

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000166 RID: 358 RVA: 0x000040DC File Offset: 0x000022DC
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

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000167 RID: 359 RVA: 0x0000411B File Offset: 0x0000231B
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

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000168 RID: 360 RVA: 0x0000415A File Offset: 0x0000235A
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

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00004199 File Offset: 0x00002399
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

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600016A RID: 362 RVA: 0x000041D8 File Offset: 0x000023D8
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

		// Token: 0x040000BF RID: 191
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<DataSetRow> _Data;

		// Token: 0x040000C0 RID: 192
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<DataSource> _DataSources;

		// Token: 0x040000C1 RID: 193
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<CacheRefreshPlan> _CacheRefreshPlans;

		// Token: 0x040000C2 RID: 194
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<ReportParameterDefinition> _ParameterDefinitions;

		// Token: 0x040000C3 RID: 195
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private CacheOptionsSingle _CacheOptions;

		// Token: 0x040000C4 RID: 196
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private FolderSingle _ParentFolder;

		// Token: 0x040000C5 RID: 197
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<Property> _Properties;

		// Token: 0x040000C6 RID: 198
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<Comment> _Comments;

		// Token: 0x040000C7 RID: 199
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<AlertSubscription> _AlertSubscriptions;

		// Token: 0x040000C8 RID: 200
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<AllowedAction> _AllowedActions;

		// Token: 0x040000C9 RID: 201
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<ItemPolicy> _Policies;

		// Token: 0x040000CA RID: 202
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<CatalogItem> _DependentItems;
	}
}
