using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Model
{
	// Token: 0x0200006A RID: 106
	public class DataSet : CatalogItem
	{
		// Token: 0x060002E9 RID: 745 RVA: 0x00003957 File Offset: 0x00001B57
		public DataSet()
			: base(CatalogItemType.DataSet)
		{
			this.QueryExecutionTimeOut = 0;
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060002EA RID: 746 RVA: 0x00003967 File Offset: 0x00001B67
		// (set) Token: 0x060002EB RID: 747 RVA: 0x0000396F File Offset: 0x00001B6F
		[ReadOnly(true)]
		public bool HasParameters { get; set; }

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060002EC RID: 748 RVA: 0x00003978 File Offset: 0x00001B78
		// (set) Token: 0x060002ED RID: 749 RVA: 0x0000399E File Offset: 0x00001B9E
		public IList<DataSetRow> Data
		{
			get
			{
				IList<DataSetRow> list;
				if ((list = this._data) == null)
				{
					list = (this._data = this.LoadDataSetData());
				}
				return list;
			}
			set
			{
				this._data = value;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060002EE RID: 750 RVA: 0x000039A8 File Offset: 0x00001BA8
		public IList<DataSource> DataSources
		{
			get
			{
				IList<DataSource> list;
				if ((list = this._dataSources) == null)
				{
					list = (this._dataSources = this.LoadDataSources());
				}
				return list;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060002EF RID: 751 RVA: 0x000039D0 File Offset: 0x00001BD0
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

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x000039F6 File Offset: 0x00001BF6
		// (set) Token: 0x060002F1 RID: 753 RVA: 0x000039FE File Offset: 0x00001BFE
		public int QueryExecutionTimeOut { get; set; }

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x00003A08 File Offset: 0x00001C08
		public IList<ReportParameterDefinition> ParameterDefinitions
		{
			get
			{
				IList<ReportParameterDefinition> list;
				if ((list = this._parameterDefinitions) == null)
				{
					list = (this._parameterDefinitions = this.LoadParameterDefinitions());
				}
				return list;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x00003A30 File Offset: 0x00001C30
		// (set) Token: 0x060002F4 RID: 756 RVA: 0x00003A56 File Offset: 0x00001C56
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

		// Token: 0x060002F5 RID: 757 RVA: 0x000024B2 File Offset: 0x000006B2
		protected virtual IList<DataSource> LoadDataSources()
		{
			return new List<DataSource>();
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x000024B9 File Offset: 0x000006B9
		protected virtual IList<CacheRefreshPlan> LoadCacheRefreshPlans()
		{
			return new List<CacheRefreshPlan>();
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00003A5F File Offset: 0x00001C5F
		protected virtual IList<ReportParameterDefinition> LoadParameterDefinitions()
		{
			return new List<ReportParameterDefinition>();
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00003A66 File Offset: 0x00001C66
		protected virtual CacheOptions LoadCacheOptions()
		{
			return new CacheOptions();
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x00003A6D File Offset: 0x00001C6D
		protected virtual IList<DataSetRow> LoadDataSetData()
		{
			return new List<DataSetRow>();
		}

		// Token: 0x04000228 RID: 552
		private IList<DataSource> _dataSources;

		// Token: 0x04000229 RID: 553
		private IList<CacheRefreshPlan> _cacheRefreshPlans;

		// Token: 0x0400022A RID: 554
		private IList<ReportParameterDefinition> _parameterDefinitions;

		// Token: 0x0400022B RID: 555
		private CacheOptions _cacheOptions;

		// Token: 0x0400022C RID: 556
		private IList<DataSetRow> _data;
	}
}
