using System;
using System.CodeDom.Compiler;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000037 RID: 55
	[OriginalName("LinkedReportSingle")]
	public class LinkedReportSingle : DataServiceQuerySingle<LinkedReport>
	{
		// Token: 0x06000246 RID: 582 RVA: 0x00005C66 File Offset: 0x00003E66
		public LinkedReportSingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00005C70 File Offset: 0x00003E70
		public LinkedReportSingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00005C7B File Offset: 0x00003E7B
		public LinkedReportSingle(DataServiceQuerySingle<LinkedReport> query)
			: base(query)
		{
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000249 RID: 585 RVA: 0x00005C84 File Offset: 0x00003E84
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

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600024A RID: 586 RVA: 0x00005CC3 File Offset: 0x00003EC3
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

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600024B RID: 587 RVA: 0x00005D02 File Offset: 0x00003F02
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

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600024C RID: 588 RVA: 0x00005D41 File Offset: 0x00003F41
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

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600024D RID: 589 RVA: 0x00005D80 File Offset: 0x00003F80
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

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600024E RID: 590 RVA: 0x00005DBF File Offset: 0x00003FBF
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

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600024F RID: 591 RVA: 0x00005DFE File Offset: 0x00003FFE
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

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000250 RID: 592 RVA: 0x00005E3D File Offset: 0x0000403D
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

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000251 RID: 593 RVA: 0x00005E7C File Offset: 0x0000407C
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

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000252 RID: 594 RVA: 0x00005EBB File Offset: 0x000040BB
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

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000253 RID: 595 RVA: 0x00005EFA File Offset: 0x000040FA
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

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000254 RID: 596 RVA: 0x00005F39 File Offset: 0x00004139
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

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000255 RID: 597 RVA: 0x00005F78 File Offset: 0x00004178
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

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000256 RID: 598 RVA: 0x00005FB7 File Offset: 0x000041B7
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

		// Token: 0x04000128 RID: 296
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<Subscription> _Subscriptions;

		// Token: 0x04000129 RID: 297
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<CacheRefreshPlan> _CacheRefreshPlans;

		// Token: 0x0400012A RID: 298
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private HistorySnapshotOptionsSingle _HistorySnapshotOptions;

		// Token: 0x0400012B RID: 299
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<ReportHistorySnapshot> _ReportHistorySnapshots;

		// Token: 0x0400012C RID: 300
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<HistorySnapshot> _HistorySnapshots;

		// Token: 0x0400012D RID: 301
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<ReportParameterDefinition> _ParameterDefinitions;

		// Token: 0x0400012E RID: 302
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private CacheOptionsSingle _CacheOptions;

		// Token: 0x0400012F RID: 303
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private FolderSingle _ParentFolder;

		// Token: 0x04000130 RID: 304
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<Property> _Properties;

		// Token: 0x04000131 RID: 305
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<Comment> _Comments;

		// Token: 0x04000132 RID: 306
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<AlertSubscription> _AlertSubscriptions;

		// Token: 0x04000133 RID: 307
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<AllowedAction> _AllowedActions;

		// Token: 0x04000134 RID: 308
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<ItemPolicy> _Policies;

		// Token: 0x04000135 RID: 309
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<CatalogItem> _DependentItems;
	}
}
