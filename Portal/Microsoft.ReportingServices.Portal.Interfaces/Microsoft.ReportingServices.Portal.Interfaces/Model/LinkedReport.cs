using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.ReportingServices;
using Microsoft.ReportingServices.Portal.Interfaces;

namespace Model
{
	// Token: 0x02000070 RID: 112
	public class LinkedReport : CatalogItem
	{
		// Token: 0x0600032B RID: 811 RVA: 0x00003D7B File Offset: 0x00001F7B
		public LinkedReport()
			: base(CatalogItemType.LinkedReport)
		{
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600032C RID: 812 RVA: 0x00003D90 File Offset: 0x00001F90
		// (set) Token: 0x0600032D RID: 813 RVA: 0x00003D98 File Offset: 0x00001F98
		[ReadOnly(true)]
		public bool HasParameters { get; set; }

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x0600032E RID: 814 RVA: 0x00003DA1 File Offset: 0x00001FA1
		// (set) Token: 0x0600032F RID: 815 RVA: 0x00003DA9 File Offset: 0x00001FA9
		public string Link
		{
			get
			{
				return this._link;
			}
			set
			{
				this.ValidatePath(value);
				this._link = value;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000330 RID: 816 RVA: 0x00003DBC File Offset: 0x00001FBC
		public IList<Subscription> Subscriptions
		{
			get
			{
				IList<Subscription> list;
				if ((list = this._subscriptions) == null)
				{
					list = (this._subscriptions = this.LoadSubscriptions());
				}
				return list;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000331 RID: 817 RVA: 0x00003DE4 File Offset: 0x00001FE4
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

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000332 RID: 818 RVA: 0x00003E0C File Offset: 0x0000200C
		// (set) Token: 0x06000333 RID: 819 RVA: 0x00003E32 File Offset: 0x00002032
		public HistorySnapshotOptions HistorySnapshotOptions
		{
			get
			{
				HistorySnapshotOptions historySnapshotOptions;
				if ((historySnapshotOptions = this._historySnapshotOptions) == null)
				{
					historySnapshotOptions = (this._historySnapshotOptions = this.LoadHistorySnapshotOptions());
				}
				return historySnapshotOptions;
			}
			set
			{
				this._historySnapshotOptions = value;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000334 RID: 820 RVA: 0x00003E3C File Offset: 0x0000203C
		public IList<ReportHistorySnapshot> ReportHistorySnapshots
		{
			get
			{
				IList<ReportHistorySnapshot> list;
				if ((list = this._reportHistorySnapshots) == null)
				{
					list = (this._reportHistorySnapshots = this.LoadReportHistorySnapshots());
				}
				return list;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000335 RID: 821 RVA: 0x00003E64 File Offset: 0x00002064
		public IList<HistorySnapshot> HistorySnapshots
		{
			get
			{
				IList<HistorySnapshot> list;
				if ((list = this._historySnapshots) == null)
				{
					list = (this._historySnapshots = this.LoadHistorySnapshots());
				}
				return list;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000336 RID: 822 RVA: 0x00003E8C File Offset: 0x0000208C
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

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000337 RID: 823 RVA: 0x00003EB4 File Offset: 0x000020B4
		// (set) Token: 0x06000338 RID: 824 RVA: 0x00003EDA File Offset: 0x000020DA
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

		// Token: 0x06000339 RID: 825 RVA: 0x00003EE3 File Offset: 0x000020E3
		protected virtual IList<ReportHistorySnapshot> LoadReportHistorySnapshots()
		{
			return new List<ReportHistorySnapshot>();
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00003EEA File Offset: 0x000020EA
		protected virtual IList<HistorySnapshot> LoadHistorySnapshots()
		{
			return new List<HistorySnapshot>();
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00003EF1 File Offset: 0x000020F1
		protected virtual HistorySnapshotOptions LoadHistorySnapshotOptions()
		{
			return new HistorySnapshotOptions();
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00003CBF File Offset: 0x00001EBF
		protected virtual IList<Subscription> LoadSubscriptions()
		{
			return new List<Subscription>();
		}

		// Token: 0x0600033D RID: 829 RVA: 0x000024B9 File Offset: 0x000006B9
		protected virtual IList<CacheRefreshPlan> LoadCacheRefreshPlans()
		{
			return new List<CacheRefreshPlan>();
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00003A5F File Offset: 0x00001C5F
		protected virtual IList<ReportParameterDefinition> LoadParameterDefinitions()
		{
			return new List<ReportParameterDefinition>();
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00003EF8 File Offset: 0x000020F8
		private void ValidatePath(string linkPath)
		{
			if (linkPath != null && !linkPath.StartsWith("/"))
			{
				throw new DataValidationException(SR.ERROR_PathStart);
			}
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00003A66 File Offset: 0x00001C66
		protected virtual CacheOptions LoadCacheOptions()
		{
			return new CacheOptions();
		}

		// Token: 0x04000252 RID: 594
		private IList<Subscription> _subscriptions;

		// Token: 0x04000253 RID: 595
		private IList<ReportHistorySnapshot> _reportHistorySnapshots;

		// Token: 0x04000254 RID: 596
		private IList<HistorySnapshot> _historySnapshots;

		// Token: 0x04000255 RID: 597
		private IList<ReportParameterDefinition> parameterDefinitions;

		// Token: 0x04000256 RID: 598
		private IList<CacheRefreshPlan> _cacheRefreshPlans;

		// Token: 0x04000257 RID: 599
		private HistorySnapshotOptions _historySnapshotOptions;

		// Token: 0x04000258 RID: 600
		private CacheOptions _cacheOptions;

		// Token: 0x04000259 RID: 601
		private string _link = string.Empty;
	}
}
