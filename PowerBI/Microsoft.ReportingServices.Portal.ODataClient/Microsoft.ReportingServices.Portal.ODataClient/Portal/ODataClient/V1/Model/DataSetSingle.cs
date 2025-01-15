using System;
using System.CodeDom.Compiler;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000C4 RID: 196
	[OriginalName("DataSetSingle")]
	public class DataSetSingle : DataServiceQuerySingle<DataSet>
	{
		// Token: 0x060008AA RID: 2218 RVA: 0x00011B25 File Offset: 0x0000FD25
		public DataSetSingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x00011B2F File Offset: 0x0000FD2F
		public DataSetSingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x00011B3A File Offset: 0x0000FD3A
		public DataSetSingle(DataServiceQuerySingle<DataSet> query)
			: base(query)
		{
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x060008AD RID: 2221 RVA: 0x00011B43 File Offset: 0x0000FD43
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

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x060008AE RID: 2222 RVA: 0x00011B82 File Offset: 0x0000FD82
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

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x060008AF RID: 2223 RVA: 0x00011BC1 File Offset: 0x0000FDC1
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

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x060008B0 RID: 2224 RVA: 0x00011C00 File Offset: 0x0000FE00
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

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x060008B1 RID: 2225 RVA: 0x00011C3F File Offset: 0x0000FE3F
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

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x060008B2 RID: 2226 RVA: 0x00011C7E File Offset: 0x0000FE7E
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

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x060008B3 RID: 2227 RVA: 0x00011CBD File Offset: 0x0000FEBD
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

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x060008B4 RID: 2228 RVA: 0x00011CFC File Offset: 0x0000FEFC
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

		// Token: 0x04000422 RID: 1058
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<DataSource> _DataSources;

		// Token: 0x04000423 RID: 1059
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<CacheRefreshPlan> _CacheRefreshPlans;

		// Token: 0x04000424 RID: 1060
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<ReportParameterDefinition> _ParameterDefinitions;

		// Token: 0x04000425 RID: 1061
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private FolderSingle _ParentFolder;

		// Token: 0x04000426 RID: 1062
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<Comment> _Comments;

		// Token: 0x04000427 RID: 1063
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<AlertSubscription> _AlertSubscriptions;

		// Token: 0x04000428 RID: 1064
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<AllowedAction> _AllowedActions;

		// Token: 0x04000429 RID: 1065
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<CatalogItem> _DependentItems;
	}
}
