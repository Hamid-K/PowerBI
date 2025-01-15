using System;
using System.CodeDom.Compiler;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x0200002B RID: 43
	[OriginalName("ExcelWorkbookSingle")]
	public class ExcelWorkbookSingle : DataServiceQuerySingle<ExcelWorkbook>
	{
		// Token: 0x060001CC RID: 460 RVA: 0x00004DE1 File Offset: 0x00002FE1
		public ExcelWorkbookSingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00004DEB File Offset: 0x00002FEB
		public ExcelWorkbookSingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00004DF6 File Offset: 0x00002FF6
		public ExcelWorkbookSingle(DataServiceQuerySingle<ExcelWorkbook> query)
			: base(query)
		{
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060001CF RID: 463 RVA: 0x00004DFF File Offset: 0x00002FFF
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

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x00004E3E File Offset: 0x0000303E
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

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x00004E7D File Offset: 0x0000307D
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

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x00004EBC File Offset: 0x000030BC
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

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x00004EFB File Offset: 0x000030FB
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

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x00004F3A File Offset: 0x0000313A
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

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x00004F79 File Offset: 0x00003179
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

		// Token: 0x040000F2 RID: 242
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private FolderSingle _ParentFolder;

		// Token: 0x040000F3 RID: 243
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<Property> _Properties;

		// Token: 0x040000F4 RID: 244
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<Comment> _Comments;

		// Token: 0x040000F5 RID: 245
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<AlertSubscription> _AlertSubscriptions;

		// Token: 0x040000F6 RID: 246
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<AllowedAction> _AllowedActions;

		// Token: 0x040000F7 RID: 247
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<ItemPolicy> _Policies;

		// Token: 0x040000F8 RID: 248
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<CatalogItem> _DependentItems;
	}
}
