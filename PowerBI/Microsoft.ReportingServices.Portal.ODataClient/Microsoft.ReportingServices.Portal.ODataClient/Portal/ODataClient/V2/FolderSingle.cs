using System;
using System.CodeDom.Compiler;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000033 RID: 51
	[OriginalName("FolderSingle")]
	public class FolderSingle : DataServiceQuerySingle<Folder>
	{
		// Token: 0x0600021C RID: 540 RVA: 0x000055B9 File Offset: 0x000037B9
		public FolderSingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x0600021D RID: 541 RVA: 0x000055C3 File Offset: 0x000037C3
		public FolderSingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x0600021E RID: 542 RVA: 0x000055CE File Offset: 0x000037CE
		public FolderSingle(DataServiceQuerySingle<Folder> query)
			: base(query)
		{
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600021F RID: 543 RVA: 0x000055D7 File Offset: 0x000037D7
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("CatalogItems")]
		public DataServiceQuery<CatalogItem> CatalogItems
		{
			get
			{
				if (!base.IsComposable)
				{
					throw new NotSupportedException("The previous function is not composable.");
				}
				if (this._CatalogItems == null)
				{
					this._CatalogItems = base.Context.CreateQuery<CatalogItem>(base.GetPath("CatalogItems"));
				}
				return this._CatalogItems;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000220 RID: 544 RVA: 0x00005616 File Offset: 0x00003816
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

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000221 RID: 545 RVA: 0x00005655 File Offset: 0x00003855
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

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000222 RID: 546 RVA: 0x00005694 File Offset: 0x00003894
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

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000223 RID: 547 RVA: 0x000056D3 File Offset: 0x000038D3
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

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000224 RID: 548 RVA: 0x00005712 File Offset: 0x00003912
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

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000225 RID: 549 RVA: 0x00005751 File Offset: 0x00003951
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

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000226 RID: 550 RVA: 0x00005790 File Offset: 0x00003990
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

		// Token: 0x04000112 RID: 274
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<CatalogItem> _CatalogItems;

		// Token: 0x04000113 RID: 275
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private FolderSingle _ParentFolder;

		// Token: 0x04000114 RID: 276
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<Property> _Properties;

		// Token: 0x04000115 RID: 277
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<Comment> _Comments;

		// Token: 0x04000116 RID: 278
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<AlertSubscription> _AlertSubscriptions;

		// Token: 0x04000117 RID: 279
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<AllowedAction> _AllowedActions;

		// Token: 0x04000118 RID: 280
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<ItemPolicy> _Policies;

		// Token: 0x04000119 RID: 281
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<CatalogItem> _DependentItems;
	}
}
