using System;
using System.CodeDom.Compiler;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x0200003D RID: 61
	[OriginalName("PowerBIReportSingle")]
	public class PowerBIReportSingle : DataServiceQuerySingle<PowerBIReport>
	{
		// Token: 0x06000291 RID: 657 RVA: 0x00006795 File Offset: 0x00004995
		public PowerBIReportSingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000679F File Offset: 0x0000499F
		public PowerBIReportSingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x06000293 RID: 659 RVA: 0x000067AA File Offset: 0x000049AA
		public PowerBIReportSingle(DataServiceQuerySingle<PowerBIReport> query)
			: base(query)
		{
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000294 RID: 660 RVA: 0x000067B3 File Offset: 0x000049B3
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

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000295 RID: 661 RVA: 0x000067F2 File Offset: 0x000049F2
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("DataModelParameters")]
		public DataServiceQuery<DataModelParameter> DataModelParameters
		{
			get
			{
				if (!base.IsComposable)
				{
					throw new NotSupportedException("The previous function is not composable.");
				}
				if (this._DataModelParameters == null)
				{
					this._DataModelParameters = base.Context.CreateQuery<DataModelParameter>(base.GetPath("DataModelParameters"));
				}
				return this._DataModelParameters;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000296 RID: 662 RVA: 0x00006831 File Offset: 0x00004A31
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

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000297 RID: 663 RVA: 0x00006870 File Offset: 0x00004A70
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("DataModelRoleAssignments")]
		public DataServiceQuery<DataModelRoleAssignment> DataModelRoleAssignments
		{
			get
			{
				if (!base.IsComposable)
				{
					throw new NotSupportedException("The previous function is not composable.");
				}
				if (this._DataModelRoleAssignments == null)
				{
					this._DataModelRoleAssignments = base.Context.CreateQuery<DataModelRoleAssignment>(base.GetPath("DataModelRoleAssignments"));
				}
				return this._DataModelRoleAssignments;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000298 RID: 664 RVA: 0x000068AF File Offset: 0x00004AAF
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("DataModelRoles")]
		public DataServiceQuery<DataModelRole> DataModelRoles
		{
			get
			{
				if (!base.IsComposable)
				{
					throw new NotSupportedException("The previous function is not composable.");
				}
				if (this._DataModelRoles == null)
				{
					this._DataModelRoles = base.Context.CreateQuery<DataModelRole>(base.GetPath("DataModelRoles"));
				}
				return this._DataModelRoles;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000299 RID: 665 RVA: 0x000068EE File Offset: 0x00004AEE
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

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600029A RID: 666 RVA: 0x0000692D File Offset: 0x00004B2D
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

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600029B RID: 667 RVA: 0x0000696C File Offset: 0x00004B6C
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

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600029C RID: 668 RVA: 0x000069AB File Offset: 0x00004BAB
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

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600029D RID: 669 RVA: 0x000069EA File Offset: 0x00004BEA
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

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600029E RID: 670 RVA: 0x00006A29 File Offset: 0x00004C29
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

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600029F RID: 671 RVA: 0x00006A68 File Offset: 0x00004C68
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

		// Token: 0x0400014E RID: 334
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<DataSource> _DataSources;

		// Token: 0x0400014F RID: 335
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<DataModelParameter> _DataModelParameters;

		// Token: 0x04000150 RID: 336
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<CacheRefreshPlan> _CacheRefreshPlans;

		// Token: 0x04000151 RID: 337
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<DataModelRoleAssignment> _DataModelRoleAssignments;

		// Token: 0x04000152 RID: 338
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<DataModelRole> _DataModelRoles;

		// Token: 0x04000153 RID: 339
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private FolderSingle _ParentFolder;

		// Token: 0x04000154 RID: 340
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<Property> _Properties;

		// Token: 0x04000155 RID: 341
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<Comment> _Comments;

		// Token: 0x04000156 RID: 342
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<AlertSubscription> _AlertSubscriptions;

		// Token: 0x04000157 RID: 343
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<AllowedAction> _AllowedActions;

		// Token: 0x04000158 RID: 344
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<ItemPolicy> _Policies;

		// Token: 0x04000159 RID: 345
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<CatalogItem> _DependentItems;
	}
}
