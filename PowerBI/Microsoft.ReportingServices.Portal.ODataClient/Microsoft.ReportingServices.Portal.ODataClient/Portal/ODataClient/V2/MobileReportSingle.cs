using System;
using System.CodeDom.Compiler;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000039 RID: 57
	[OriginalName("MobileReportSingle")]
	public class MobileReportSingle : DataServiceQuerySingle<MobileReport>
	{
		// Token: 0x0600026E RID: 622 RVA: 0x000062D4 File Offset: 0x000044D4
		public MobileReportSingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x0600026F RID: 623 RVA: 0x000062DE File Offset: 0x000044DE
		public MobileReportSingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x06000270 RID: 624 RVA: 0x000062E9 File Offset: 0x000044E9
		public MobileReportSingle(DataServiceQuerySingle<MobileReport> query)
			: base(query)
		{
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000271 RID: 625 RVA: 0x000062F2 File Offset: 0x000044F2
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

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000272 RID: 626 RVA: 0x00006331 File Offset: 0x00004531
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

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000273 RID: 627 RVA: 0x00006370 File Offset: 0x00004570
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

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000274 RID: 628 RVA: 0x000063AF File Offset: 0x000045AF
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

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000275 RID: 629 RVA: 0x000063EE File Offset: 0x000045EE
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

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000276 RID: 630 RVA: 0x0000642D File Offset: 0x0000462D
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

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000277 RID: 631 RVA: 0x0000646C File Offset: 0x0000466C
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

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000278 RID: 632 RVA: 0x000064AB File Offset: 0x000046AB
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

		// Token: 0x0400013F RID: 319
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<DataSet> _SharedDataSets;

		// Token: 0x04000140 RID: 320
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private FolderSingle _ParentFolder;

		// Token: 0x04000141 RID: 321
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<Property> _Properties;

		// Token: 0x04000142 RID: 322
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<Comment> _Comments;

		// Token: 0x04000143 RID: 323
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<AlertSubscription> _AlertSubscriptions;

		// Token: 0x04000144 RID: 324
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<AllowedAction> _AllowedActions;

		// Token: 0x04000145 RID: 325
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<ItemPolicy> _Policies;

		// Token: 0x04000146 RID: 326
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<CatalogItem> _DependentItems;
	}
}
