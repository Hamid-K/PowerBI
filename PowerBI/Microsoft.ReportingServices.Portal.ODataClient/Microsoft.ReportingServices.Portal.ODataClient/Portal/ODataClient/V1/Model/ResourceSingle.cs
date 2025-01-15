using System;
using System.CodeDom.Compiler;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000FA RID: 250
	[OriginalName("ResourceSingle")]
	public class ResourceSingle : DataServiceQuerySingle<Resource>
	{
		// Token: 0x06000B00 RID: 2816 RVA: 0x00015B45 File Offset: 0x00013D45
		public ResourceSingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x00015B4F File Offset: 0x00013D4F
		public ResourceSingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x00015B5A File Offset: 0x00013D5A
		public ResourceSingle(DataServiceQuerySingle<Resource> query)
			: base(query)
		{
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000B03 RID: 2819 RVA: 0x00015B63 File Offset: 0x00013D63
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

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000B04 RID: 2820 RVA: 0x00015BA2 File Offset: 0x00013DA2
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

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000B05 RID: 2821 RVA: 0x00015BE1 File Offset: 0x00013DE1
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

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000B06 RID: 2822 RVA: 0x00015C20 File Offset: 0x00013E20
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

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000B07 RID: 2823 RVA: 0x00015C5F File Offset: 0x00013E5F
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

		// Token: 0x04000507 RID: 1287
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private FolderSingle _ParentFolder;

		// Token: 0x04000508 RID: 1288
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<Comment> _Comments;

		// Token: 0x04000509 RID: 1289
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<AlertSubscription> _AlertSubscriptions;

		// Token: 0x0400050A RID: 1290
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<AllowedAction> _AllowedActions;

		// Token: 0x0400050B RID: 1291
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<CatalogItem> _DependentItems;
	}
}
