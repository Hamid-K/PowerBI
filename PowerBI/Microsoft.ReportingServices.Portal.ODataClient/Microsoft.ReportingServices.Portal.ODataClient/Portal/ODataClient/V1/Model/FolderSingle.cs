using System;
using System.CodeDom.Compiler;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000EE RID: 238
	[OriginalName("FolderSingle")]
	public class FolderSingle : DataServiceQuerySingle<Folder>
	{
		// Token: 0x06000AA6 RID: 2726 RVA: 0x00015125 File Offset: 0x00013325
		public FolderSingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x0001512F File Offset: 0x0001332F
		public FolderSingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x0001513A File Offset: 0x0001333A
		public FolderSingle(DataServiceQuerySingle<Folder> query)
			: base(query)
		{
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000AA9 RID: 2729 RVA: 0x00015143 File Offset: 0x00013343
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

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000AAA RID: 2730 RVA: 0x00015182 File Offset: 0x00013382
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

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000AAB RID: 2731 RVA: 0x000151C1 File Offset: 0x000133C1
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

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000AAC RID: 2732 RVA: 0x00015200 File Offset: 0x00013400
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

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000AAD RID: 2733 RVA: 0x0001523F File Offset: 0x0001343F
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

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000AAE RID: 2734 RVA: 0x0001527E File Offset: 0x0001347E
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

		// Token: 0x040004E1 RID: 1249
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<CatalogItem> _CatalogItems;

		// Token: 0x040004E2 RID: 1250
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private FolderSingle _ParentFolder;

		// Token: 0x040004E3 RID: 1251
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<Comment> _Comments;

		// Token: 0x040004E4 RID: 1252
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<AlertSubscription> _AlertSubscriptions;

		// Token: 0x040004E5 RID: 1253
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<AllowedAction> _AllowedActions;

		// Token: 0x040004E6 RID: 1254
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<CatalogItem> _DependentItems;
	}
}
