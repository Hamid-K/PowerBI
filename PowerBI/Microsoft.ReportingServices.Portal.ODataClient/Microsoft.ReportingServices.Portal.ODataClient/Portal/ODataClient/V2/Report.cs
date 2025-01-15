using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000044 RID: 68
	[Key("Id")]
	[EntitySet("Reports")]
	[OriginalName("Report")]
	public class Report : CatalogItem
	{
		// Token: 0x060002DD RID: 733 RVA: 0x000072F0 File Offset: 0x000054F0
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static Report CreateReport(Guid ID, CatalogItemType type, bool hidden, long size, DateTimeOffset modifiedDate, DateTimeOffset createdDate, bool isFavorite, bool hasDataSources, bool hasSharedDataSets, bool hasParameters)
		{
			return new Report
			{
				Id = ID,
				Type = type,
				Hidden = hidden,
				Size = size,
				ModifiedDate = modifiedDate,
				CreatedDate = createdDate,
				IsFavorite = isFavorite,
				HasDataSources = hasDataSources,
				HasSharedDataSets = hasSharedDataSets,
				HasParameters = hasParameters
			};
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060002DE RID: 734 RVA: 0x0000734E File Offset: 0x0000554E
		// (set) Token: 0x060002DF RID: 735 RVA: 0x00007356 File Offset: 0x00005556
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("HasDataSources")]
		public bool HasDataSources
		{
			get
			{
				return this._HasDataSources;
			}
			set
			{
				this._HasDataSources = value;
				this.OnPropertyChanged("HasDataSources");
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x0000736A File Offset: 0x0000556A
		// (set) Token: 0x060002E1 RID: 737 RVA: 0x00007372 File Offset: 0x00005572
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("HasSharedDataSets")]
		public bool HasSharedDataSets
		{
			get
			{
				return this._HasSharedDataSets;
			}
			set
			{
				this._HasSharedDataSets = value;
				this.OnPropertyChanged("HasSharedDataSets");
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x00007386 File Offset: 0x00005586
		// (set) Token: 0x060002E3 RID: 739 RVA: 0x0000738E File Offset: 0x0000558E
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

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x000073A2 File Offset: 0x000055A2
		// (set) Token: 0x060002E5 RID: 741 RVA: 0x000073AA File Offset: 0x000055AA
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

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x000073BE File Offset: 0x000055BE
		// (set) Token: 0x060002E7 RID: 743 RVA: 0x000073C6 File Offset: 0x000055C6
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

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x000073DA File Offset: 0x000055DA
		// (set) Token: 0x060002E9 RID: 745 RVA: 0x000073E2 File Offset: 0x000055E2
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("DataSources")]
		public DataServiceCollection<DataSource> DataSources
		{
			get
			{
				return this._DataSources;
			}
			set
			{
				this._DataSources = value;
				this.OnPropertyChanged("DataSources");
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060002EA RID: 746 RVA: 0x000073F6 File Offset: 0x000055F6
		// (set) Token: 0x060002EB RID: 747 RVA: 0x000073FE File Offset: 0x000055FE
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("SharedDataSets")]
		public DataServiceCollection<DataSet> SharedDataSets
		{
			get
			{
				return this._SharedDataSets;
			}
			set
			{
				this._SharedDataSets = value;
				this.OnPropertyChanged("SharedDataSets");
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060002EC RID: 748 RVA: 0x00007412 File Offset: 0x00005612
		// (set) Token: 0x060002ED RID: 749 RVA: 0x0000741A File Offset: 0x0000561A
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

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0000742E File Offset: 0x0000562E
		// (set) Token: 0x060002EF RID: 751 RVA: 0x00007436 File Offset: 0x00005636
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

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x0000744A File Offset: 0x0000564A
		// (set) Token: 0x060002F1 RID: 753 RVA: 0x00007452 File Offset: 0x00005652
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

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x00007466 File Offset: 0x00005666
		// (set) Token: 0x060002F3 RID: 755 RVA: 0x0000746E File Offset: 0x0000566E
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

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x00007482 File Offset: 0x00005682
		// (set) Token: 0x060002F5 RID: 757 RVA: 0x0000748A File Offset: 0x0000568A
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

		// Token: 0x060002F6 RID: 758 RVA: 0x000074A0 File Offset: 0x000056A0
		[OriginalName("Upload")]
		public DataServiceActionQuerySingle<Report> Upload()
		{
			EntityDescriptor entityDescriptor = base.Context.EntityTracker.TryGetEntityDescriptor(this);
			if (entityDescriptor == null)
			{
				throw new Exception("cannot find entity");
			}
			return new DataServiceActionQuerySingle<Report>(base.Context, entityDescriptor.EditLink.OriginalString.Trim(new char[] { '/' }) + "/Model.Upload", Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00007504 File Offset: 0x00005704
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

		// Token: 0x060002F8 RID: 760 RVA: 0x00007568 File Offset: 0x00005768
		[OriginalName("CheckDataSourceConnection")]
		public DataServiceActionQuerySingle<DataSourceCheckResult> CheckDataSourceConnection(string DataSourceName)
		{
			EntityDescriptor entityDescriptor = base.Context.EntityTracker.TryGetEntityDescriptor(this);
			if (entityDescriptor == null)
			{
				throw new Exception("cannot find entity");
			}
			return new DataServiceActionQuerySingle<DataSourceCheckResult>(base.Context, entityDescriptor.EditLink.OriginalString.Trim(new char[] { '/' }) + "/Model.CheckDataSourceConnection", new BodyOperationParameter[]
			{
				new BodyOperationParameter("DataSourceName", DataSourceName)
			});
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x000075DC File Offset: 0x000057DC
		[OriginalName("UpdateReportDataSets")]
		public DataServiceActionQuery UpdateReportDataSets(ICollection<DataSet> DataSets)
		{
			EntityDescriptor entityDescriptor = base.Context.EntityTracker.TryGetEntityDescriptor(this);
			if (entityDescriptor == null)
			{
				throw new Exception("cannot find entity");
			}
			return new DataServiceActionQuery(base.Context, entityDescriptor.EditLink.OriginalString.Trim(new char[] { '/' }) + "/Model.UpdateReportDataSets", new BodyOperationParameter[]
			{
				new BodyOperationParameter("DataSets", DataSets)
			});
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00007650 File Offset: 0x00005850
		[OriginalName("GetParameters")]
		public DataServiceActionQuery<ReportParameterDefinition> GetParameters(ICollection<ParameterValue> ParameterValues)
		{
			EntityDescriptor entityDescriptor = base.Context.EntityTracker.TryGetEntityDescriptor(this);
			if (entityDescriptor == null)
			{
				throw new Exception("cannot find entity");
			}
			return new DataServiceActionQuery<ReportParameterDefinition>(base.Context, entityDescriptor.EditLink.OriginalString.Trim(new char[] { '/' }) + "/Model.GetParameters", new BodyOperationParameter[]
			{
				new BodyOperationParameter("ParameterValues", ParameterValues)
			});
		}

		// Token: 0x060002FB RID: 763 RVA: 0x000076C4 File Offset: 0x000058C4
		[OriginalName("ProcessParameters")]
		public DataServiceActionQuery<ReportParameterDefinition> ProcessParameters(ICollection<ParameterValue> ParameterValues)
		{
			EntityDescriptor entityDescriptor = base.Context.EntityTracker.TryGetEntityDescriptor(this);
			if (entityDescriptor == null)
			{
				throw new Exception("cannot find entity");
			}
			return new DataServiceActionQuery<ReportParameterDefinition>(base.Context, entityDescriptor.EditLink.OriginalString.Trim(new char[] { '/' }) + "/Model.ProcessParameters", new BodyOperationParameter[]
			{
				new BodyOperationParameter("ParameterValues", ParameterValues)
			});
		}

		// Token: 0x04000177 RID: 375
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _HasDataSources;

		// Token: 0x04000178 RID: 376
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _HasSharedDataSets;

		// Token: 0x04000179 RID: 377
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _HasParameters;

		// Token: 0x0400017A RID: 378
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<Subscription> _Subscriptions = new DataServiceCollection<Subscription>(null, TrackingMode.None);

		// Token: 0x0400017B RID: 379
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<CacheRefreshPlan> _CacheRefreshPlans = new DataServiceCollection<CacheRefreshPlan>(null, TrackingMode.None);

		// Token: 0x0400017C RID: 380
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<DataSource> _DataSources = new DataServiceCollection<DataSource>(null, TrackingMode.None);

		// Token: 0x0400017D RID: 381
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<DataSet> _SharedDataSets = new DataServiceCollection<DataSet>(null, TrackingMode.None);

		// Token: 0x0400017E RID: 382
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private HistorySnapshotOptions _HistorySnapshotOptions;

		// Token: 0x0400017F RID: 383
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<ReportHistorySnapshot> _ReportHistorySnapshots = new DataServiceCollection<ReportHistorySnapshot>(null, TrackingMode.None);

		// Token: 0x04000180 RID: 384
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<HistorySnapshot> _HistorySnapshots = new DataServiceCollection<HistorySnapshot>(null, TrackingMode.None);

		// Token: 0x04000181 RID: 385
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<ReportParameterDefinition> _ParameterDefinitions = new DataServiceCollection<ReportParameterDefinition>(null, TrackingMode.None);

		// Token: 0x04000182 RID: 386
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private CacheOptions _CacheOptions;
	}
}
