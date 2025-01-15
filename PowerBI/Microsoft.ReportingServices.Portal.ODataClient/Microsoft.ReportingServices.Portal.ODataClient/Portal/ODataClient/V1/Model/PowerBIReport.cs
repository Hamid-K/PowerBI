using System;
using System.CodeDom.Compiler;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000F7 RID: 247
	[Key("Id")]
	[OriginalName("PowerBIReport")]
	public class PowerBIReport : CatalogItem
	{
		// Token: 0x06000AE0 RID: 2784 RVA: 0x000158C8 File Offset: 0x00013AC8
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static PowerBIReport CreatePowerBIReport(Guid ID, CatalogItemType type, bool hidden, long size, DateTimeOffset modifiedDate, DateTimeOffset createdDate, bool isFavorite, bool hasDataSources)
		{
			return new PowerBIReport
			{
				Id = ID,
				Type = type,
				Hidden = hidden,
				Size = size,
				ModifiedDate = modifiedDate,
				CreatedDate = createdDate,
				IsFavorite = isFavorite,
				HasDataSources = hasDataSources
			};
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000AE1 RID: 2785 RVA: 0x00015916 File Offset: 0x00013B16
		// (set) Token: 0x06000AE2 RID: 2786 RVA: 0x0001591E File Offset: 0x00013B1E
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

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000AE3 RID: 2787 RVA: 0x00015932 File Offset: 0x00013B32
		// (set) Token: 0x06000AE4 RID: 2788 RVA: 0x0001593A File Offset: 0x00013B3A
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

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000AE5 RID: 2789 RVA: 0x0001594E File Offset: 0x00013B4E
		// (set) Token: 0x06000AE6 RID: 2790 RVA: 0x00015956 File Offset: 0x00013B56
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("DataModelDataSources")]
		public DataServiceCollection<DataModelDataSource> DataModelDataSources
		{
			get
			{
				return this._DataModelDataSources;
			}
			set
			{
				this._DataModelDataSources = value;
				this.OnPropertyChanged("DataModelDataSources");
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000AE7 RID: 2791 RVA: 0x0001596A File Offset: 0x00013B6A
		// (set) Token: 0x06000AE8 RID: 2792 RVA: 0x00015972 File Offset: 0x00013B72
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

		// Token: 0x040004FB RID: 1275
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _HasDataSources;

		// Token: 0x040004FC RID: 1276
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<DataSource> _DataSources = new DataServiceCollection<DataSource>(null, TrackingMode.None);

		// Token: 0x040004FD RID: 1277
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<DataModelDataSource> _DataModelDataSources = new DataServiceCollection<DataModelDataSource>(null, TrackingMode.None);

		// Token: 0x040004FE RID: 1278
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<CacheRefreshPlan> _CacheRefreshPlans = new DataServiceCollection<CacheRefreshPlan>(null, TrackingMode.None);
	}
}
