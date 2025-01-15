using System;
using System.CodeDom.Compiler;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000038 RID: 56
	[Key("Id")]
	[EntitySet("LinkedReports")]
	[OriginalName("LinkedReport")]
	public class LinkedReport : CatalogItem
	{
		// Token: 0x06000257 RID: 599 RVA: 0x00005FF8 File Offset: 0x000041F8
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static LinkedReport CreateLinkedReport(Guid ID, CatalogItemType type, bool hidden, long size, DateTimeOffset modifiedDate, DateTimeOffset createdDate, bool isFavorite, bool hasParameters)
		{
			return new LinkedReport
			{
				Id = ID,
				Type = type,
				Hidden = hidden,
				Size = size,
				ModifiedDate = modifiedDate,
				CreatedDate = createdDate,
				IsFavorite = isFavorite,
				HasParameters = hasParameters
			};
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000258 RID: 600 RVA: 0x00006046 File Offset: 0x00004246
		// (set) Token: 0x06000259 RID: 601 RVA: 0x0000604E File Offset: 0x0000424E
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("HasParameters")]
		public bool HasParameters
		{
			get
			{
				return this._HasParameters;
			}
			set
			{
				this._HasParameters = value;
				this.OnPropertyChanged("HasParameters");
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600025A RID: 602 RVA: 0x00006062 File Offset: 0x00004262
		// (set) Token: 0x0600025B RID: 603 RVA: 0x0000606A File Offset: 0x0000426A
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Link")]
		public string Link
		{
			get
			{
				return this._Link;
			}
			set
			{
				this._Link = value;
				this.OnPropertyChanged("Link");
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600025C RID: 604 RVA: 0x0000607E File Offset: 0x0000427E
		// (set) Token: 0x0600025D RID: 605 RVA: 0x00006086 File Offset: 0x00004286
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Subscriptions")]
		public DataServiceCollection<Subscription> Subscriptions
		{
			get
			{
				return this._Subscriptions;
			}
			set
			{
				this._Subscriptions = value;
				this.OnPropertyChanged("Subscriptions");
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600025E RID: 606 RVA: 0x0000609A File Offset: 0x0000429A
		// (set) Token: 0x0600025F RID: 607 RVA: 0x000060A2 File Offset: 0x000042A2
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("CacheRefreshPlans")]
		public DataServiceCollection<CacheRefreshPlan> CacheRefreshPlans
		{
			get
			{
				return this._CacheRefreshPlans;
			}
			set
			{
				this._CacheRefreshPlans = value;
				this.OnPropertyChanged("CacheRefreshPlans");
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000260 RID: 608 RVA: 0x000060B6 File Offset: 0x000042B6
		// (set) Token: 0x06000261 RID: 609 RVA: 0x000060BE File Offset: 0x000042BE
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("HistorySnapshotOptions")]
		public HistorySnapshotOptions HistorySnapshotOptions
		{
			get
			{
				return this._HistorySnapshotOptions;
			}
			set
			{
				this._HistorySnapshotOptions = value;
				this.OnPropertyChanged("HistorySnapshotOptions");
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000262 RID: 610 RVA: 0x000060D2 File Offset: 0x000042D2
		// (set) Token: 0x06000263 RID: 611 RVA: 0x000060DA File Offset: 0x000042DA
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("ReportHistorySnapshots")]
		public DataServiceCollection<ReportHistorySnapshot> ReportHistorySnapshots
		{
			get
			{
				return this._ReportHistorySnapshots;
			}
			set
			{
				this._ReportHistorySnapshots = value;
				this.OnPropertyChanged("ReportHistorySnapshots");
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000264 RID: 612 RVA: 0x000060EE File Offset: 0x000042EE
		// (set) Token: 0x06000265 RID: 613 RVA: 0x000060F6 File Offset: 0x000042F6
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("HistorySnapshots")]
		public DataServiceCollection<HistorySnapshot> HistorySnapshots
		{
			get
			{
				return this._HistorySnapshots;
			}
			set
			{
				this._HistorySnapshots = value;
				this.OnPropertyChanged("HistorySnapshots");
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000266 RID: 614 RVA: 0x0000610A File Offset: 0x0000430A
		// (set) Token: 0x06000267 RID: 615 RVA: 0x00006112 File Offset: 0x00004312
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("ParameterDefinitions")]
		public DataServiceCollection<ReportParameterDefinition> ParameterDefinitions
		{
			get
			{
				return this._ParameterDefinitions;
			}
			set
			{
				this._ParameterDefinitions = value;
				this.OnPropertyChanged("ParameterDefinitions");
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000268 RID: 616 RVA: 0x00006126 File Offset: 0x00004326
		// (set) Token: 0x06000269 RID: 617 RVA: 0x0000612E File Offset: 0x0000432E
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("CacheOptions")]
		public CacheOptions CacheOptions
		{
			get
			{
				return this._CacheOptions;
			}
			set
			{
				this._CacheOptions = value;
				this.OnPropertyChanged("CacheOptions");
			}
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00006144 File Offset: 0x00004344
		[OriginalName("Upload")]
		public DataServiceActionQuerySingle<LinkedReport> Upload()
		{
			EntityDescriptor entityDescriptor = base.Context.EntityTracker.TryGetEntityDescriptor(this);
			if (entityDescriptor == null)
			{
				throw new Exception("cannot find entity");
			}
			return new DataServiceActionQuerySingle<LinkedReport>(base.Context, entityDescriptor.EditLink.OriginalString.Trim(new char[] { '/' }) + "/Model.Upload", Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x0600026B RID: 619 RVA: 0x000061A8 File Offset: 0x000043A8
		[OriginalName("UpdateCacheSnapshot")]
		public DataServiceActionQuerySingle<bool> UpdateCacheSnapshot()
		{
			EntityDescriptor entityDescriptor = base.Context.EntityTracker.TryGetEntityDescriptor(this);
			if (entityDescriptor == null)
			{
				throw new Exception("cannot find entity");
			}
			return new DataServiceActionQuerySingle<bool>(base.Context, entityDescriptor.EditLink.OriginalString.Trim(new char[] { '/' }) + "/Model.UpdateCacheSnapshot", Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000620C File Offset: 0x0000440C
		[OriginalName("Reparent")]
		public DataServiceActionQuerySingle<string> Reparent(string ParentPath)
		{
			EntityDescriptor entityDescriptor = base.Context.EntityTracker.TryGetEntityDescriptor(this);
			if (entityDescriptor == null)
			{
				throw new Exception("cannot find entity");
			}
			return new DataServiceActionQuerySingle<string>(base.Context, entityDescriptor.EditLink.OriginalString.Trim(new char[] { '/' }) + "/Model.Reparent", new BodyOperationParameter[]
			{
				new BodyOperationParameter("ParentPath", ParentPath)
			});
		}

		// Token: 0x04000136 RID: 310
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _HasParameters;

		// Token: 0x04000137 RID: 311
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Link;

		// Token: 0x04000138 RID: 312
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<Subscription> _Subscriptions = new DataServiceCollection<Subscription>(null, TrackingMode.None);

		// Token: 0x04000139 RID: 313
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<CacheRefreshPlan> _CacheRefreshPlans = new DataServiceCollection<CacheRefreshPlan>(null, TrackingMode.None);

		// Token: 0x0400013A RID: 314
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private HistorySnapshotOptions _HistorySnapshotOptions;

		// Token: 0x0400013B RID: 315
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<ReportHistorySnapshot> _ReportHistorySnapshots = new DataServiceCollection<ReportHistorySnapshot>(null, TrackingMode.None);

		// Token: 0x0400013C RID: 316
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<HistorySnapshot> _HistorySnapshots = new DataServiceCollection<HistorySnapshot>(null, TrackingMode.None);

		// Token: 0x0400013D RID: 317
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<ReportParameterDefinition> _ParameterDefinitions = new DataServiceCollection<ReportParameterDefinition>(null, TrackingMode.None);

		// Token: 0x0400013E RID: 318
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private CacheOptions _CacheOptions;
	}
}
