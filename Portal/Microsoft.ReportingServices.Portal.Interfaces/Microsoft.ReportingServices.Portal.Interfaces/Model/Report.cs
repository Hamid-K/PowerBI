using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Model
{
	// Token: 0x02000074 RID: 116
	public class Report : CatalogItem
	{
		// Token: 0x06000355 RID: 853 RVA: 0x00004007 File Offset: 0x00002207
		public Report()
			: base(CatalogItemType.Report)
		{
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000356 RID: 854 RVA: 0x00004010 File Offset: 0x00002210
		// (set) Token: 0x06000357 RID: 855 RVA: 0x00004018 File Offset: 0x00002218
		[ReadOnly(true)]
		public bool HasDataSources { get; set; }

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000358 RID: 856 RVA: 0x00004021 File Offset: 0x00002221
		// (set) Token: 0x06000359 RID: 857 RVA: 0x00004029 File Offset: 0x00002229
		[ReadOnly(true)]
		public bool HasSharedDataSets { get; set; }

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600035A RID: 858 RVA: 0x00004032 File Offset: 0x00002232
		// (set) Token: 0x0600035B RID: 859 RVA: 0x0000403A File Offset: 0x0000223A
		[ReadOnly(true)]
		public bool HasParameters { get; set; }

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x0600035C RID: 860 RVA: 0x00004044 File Offset: 0x00002244
		public IList<Subscription> Subscriptions
		{
			get
			{
				IList<Subscription> list;
				if ((list = this.subscriptions) == null)
				{
					list = (this.subscriptions = this.LoadSubscriptions());
				}
				return list;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x0600035D RID: 861 RVA: 0x0000406C File Offset: 0x0000226C
		public IList<CacheRefreshPlan> CacheRefreshPlans
		{
			get
			{
				IList<CacheRefreshPlan> list;
				if ((list = this._cacheRefreshPlans) == null)
				{
					list = (this._cacheRefreshPlans = this.LoadCacheRefreshPlans());
				}
				return list;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x0600035E RID: 862 RVA: 0x00004094 File Offset: 0x00002294
		public IList<DataSource> DataSources
		{
			get
			{
				IList<DataSource> list;
				if ((list = this.dataSources) == null)
				{
					list = (this.dataSources = this.LoadDataSources());
				}
				return list;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x0600035F RID: 863 RVA: 0x000040BC File Offset: 0x000022BC
		public IList<DataSet> SharedDataSets
		{
			get
			{
				IList<DataSet> list;
				if ((list = this.sharedDataSets) == null)
				{
					list = (this.sharedDataSets = this.LoadSharedDataSets());
				}
				return list;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000360 RID: 864 RVA: 0x000040E4 File Offset: 0x000022E4
		// (set) Token: 0x06000361 RID: 865 RVA: 0x0000410A File Offset: 0x0000230A
		public HistorySnapshotOptions HistorySnapshotOptions
		{
			get
			{
				HistorySnapshotOptions historySnapshotOptions;
				if ((historySnapshotOptions = this.historySnapshotOptions) == null)
				{
					historySnapshotOptions = (this.historySnapshotOptions = this.LoadHistorySnapshotOptions());
				}
				return historySnapshotOptions;
			}
			set
			{
				this.historySnapshotOptions = value;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000362 RID: 866 RVA: 0x00004114 File Offset: 0x00002314
		public IList<ReportHistorySnapshot> ReportHistorySnapshots
		{
			get
			{
				IList<ReportHistorySnapshot> list;
				if ((list = this.reportHistorySnapshots) == null)
				{
					list = (this.reportHistorySnapshots = this.LoadReportHistorySnapshots());
				}
				return list;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000363 RID: 867 RVA: 0x0000413C File Offset: 0x0000233C
		public IList<HistorySnapshot> HistorySnapshots
		{
			get
			{
				IList<HistorySnapshot> list;
				if ((list = this.historySnapshots) == null)
				{
					list = (this.historySnapshots = this.LoadHistorySnapshots());
				}
				return list;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000364 RID: 868 RVA: 0x00004164 File Offset: 0x00002364
		public IList<ReportParameterDefinition> ParameterDefinitions
		{
			get
			{
				IList<ReportParameterDefinition> list;
				if ((list = this.parameterDefinitions) == null)
				{
					list = (this.parameterDefinitions = this.LoadParameterDefinitions());
				}
				return list;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000365 RID: 869 RVA: 0x0000418C File Offset: 0x0000238C
		// (set) Token: 0x06000366 RID: 870 RVA: 0x000041B2 File Offset: 0x000023B2
		public CacheOptions CacheOptions
		{
			get
			{
				CacheOptions cacheOptions;
				if ((cacheOptions = this._cacheOptions) == null)
				{
					cacheOptions = (this._cacheOptions = this.LoadCacheOptions());
				}
				return cacheOptions;
			}
			set
			{
				this._cacheOptions = value;
			}
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00003EE3 File Offset: 0x000020E3
		protected virtual IList<ReportHistorySnapshot> LoadReportHistorySnapshots()
		{
			return new List<ReportHistorySnapshot>();
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00003EEA File Offset: 0x000020EA
		protected virtual IList<HistorySnapshot> LoadHistorySnapshots()
		{
			return new List<HistorySnapshot>();
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00003CBF File Offset: 0x00001EBF
		protected virtual IList<Subscription> LoadSubscriptions()
		{
			return new List<Subscription>();
		}

		// Token: 0x0600036A RID: 874 RVA: 0x000024B9 File Offset: 0x000006B9
		protected virtual IList<CacheRefreshPlan> LoadCacheRefreshPlans()
		{
			return new List<CacheRefreshPlan>();
		}

		// Token: 0x0600036B RID: 875 RVA: 0x000024B2 File Offset: 0x000006B2
		protected virtual IList<DataSource> LoadDataSources()
		{
			return new List<DataSource>();
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00003F7A File Offset: 0x0000217A
		protected virtual IList<DataSet> LoadSharedDataSets()
		{
			return new List<DataSet>();
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00003A5F File Offset: 0x00001C5F
		protected virtual IList<ReportParameterDefinition> LoadParameterDefinitions()
		{
			return new List<ReportParameterDefinition>();
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00003EF1 File Offset: 0x000020F1
		protected virtual HistorySnapshotOptions LoadHistorySnapshotOptions()
		{
			return new HistorySnapshotOptions();
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00003A66 File Offset: 0x00001C66
		protected virtual CacheOptions LoadCacheOptions()
		{
			return new CacheOptions();
		}

		// Token: 0x04000267 RID: 615
		private IList<Subscription> subscriptions;

		// Token: 0x04000268 RID: 616
		private IList<CacheRefreshPlan> _cacheRefreshPlans;

		// Token: 0x04000269 RID: 617
		private IList<DataSource> dataSources;

		// Token: 0x0400026A RID: 618
		private IList<DataSet> sharedDataSets;

		// Token: 0x0400026B RID: 619
		private IList<ReportHistorySnapshot> reportHistorySnapshots;

		// Token: 0x0400026C RID: 620
		private IList<HistorySnapshot> historySnapshots;

		// Token: 0x0400026D RID: 621
		private IList<ReportParameterDefinition> parameterDefinitions;

		// Token: 0x0400026E RID: 622
		private HistorySnapshotOptions historySnapshotOptions;

		// Token: 0x0400026F RID: 623
		private CacheOptions _cacheOptions;
	}
}
