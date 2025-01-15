using System;
using System.CodeDom.Compiler;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x02000109 RID: 265
	[OriginalName("MobileReportSingle")]
	public class MobileReportSingle : DataServiceQuerySingle<MobileReport>
	{
		// Token: 0x06000B74 RID: 2932 RVA: 0x000166D5 File Offset: 0x000148D5
		public MobileReportSingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x000166DF File Offset: 0x000148DF
		public MobileReportSingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x000166EA File Offset: 0x000148EA
		public MobileReportSingle(DataServiceQuerySingle<MobileReport> query)
			: base(query)
		{
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06000B77 RID: 2935 RVA: 0x000166F3 File Offset: 0x000148F3
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

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000B78 RID: 2936 RVA: 0x00016732 File Offset: 0x00014932
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

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06000B79 RID: 2937 RVA: 0x00016771 File Offset: 0x00014971
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

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06000B7A RID: 2938 RVA: 0x000167B0 File Offset: 0x000149B0
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

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000B7B RID: 2939 RVA: 0x000167EF File Offset: 0x000149EF
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

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000B7C RID: 2940 RVA: 0x0001682E File Offset: 0x00014A2E
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

		// Token: 0x04000535 RID: 1333
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<DataSet> _SharedDataSets;

		// Token: 0x04000536 RID: 1334
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private FolderSingle _ParentFolder;

		// Token: 0x04000537 RID: 1335
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<Comment> _Comments;

		// Token: 0x04000538 RID: 1336
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<AlertSubscription> _AlertSubscriptions;

		// Token: 0x04000539 RID: 1337
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<AllowedAction> _AllowedActions;

		// Token: 0x0400053A RID: 1338
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<CatalogItem> _DependentItems;
	}
}
