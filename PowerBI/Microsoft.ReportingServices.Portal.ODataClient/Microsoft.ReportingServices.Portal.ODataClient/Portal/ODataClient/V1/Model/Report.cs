using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000BE RID: 190
	[Key("Id")]
	[OriginalName("Report")]
	public class Report : CatalogItem
	{
		// Token: 0x06000838 RID: 2104 RVA: 0x000108E8 File Offset: 0x0000EAE8
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

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000839 RID: 2105 RVA: 0x00010946 File Offset: 0x0000EB46
		// (set) Token: 0x0600083A RID: 2106 RVA: 0x0001094E File Offset: 0x0000EB4E
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

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x0600083B RID: 2107 RVA: 0x00010962 File Offset: 0x0000EB62
		// (set) Token: 0x0600083C RID: 2108 RVA: 0x0001096A File Offset: 0x0000EB6A
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

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x0600083D RID: 2109 RVA: 0x0001097E File Offset: 0x0000EB7E
		// (set) Token: 0x0600083E RID: 2110 RVA: 0x00010986 File Offset: 0x0000EB86
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

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x0600083F RID: 2111 RVA: 0x0001099A File Offset: 0x0000EB9A
		// (set) Token: 0x06000840 RID: 2112 RVA: 0x000109A2 File Offset: 0x0000EBA2
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

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000841 RID: 2113 RVA: 0x000109B6 File Offset: 0x0000EBB6
		// (set) Token: 0x06000842 RID: 2114 RVA: 0x000109BE File Offset: 0x0000EBBE
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

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000843 RID: 2115 RVA: 0x000109D2 File Offset: 0x0000EBD2
		// (set) Token: 0x06000844 RID: 2116 RVA: 0x000109DA File Offset: 0x0000EBDA
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

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000845 RID: 2117 RVA: 0x000109EE File Offset: 0x0000EBEE
		// (set) Token: 0x06000846 RID: 2118 RVA: 0x000109F6 File Offset: 0x0000EBF6
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

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000847 RID: 2119 RVA: 0x00010A0A File Offset: 0x0000EC0A
		// (set) Token: 0x06000848 RID: 2120 RVA: 0x00010A12 File Offset: 0x0000EC12
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

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000849 RID: 2121 RVA: 0x00010A26 File Offset: 0x0000EC26
		// (set) Token: 0x0600084A RID: 2122 RVA: 0x00010A2E File Offset: 0x0000EC2E
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

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x0600084B RID: 2123 RVA: 0x00010A42 File Offset: 0x0000EC42
		// (set) Token: 0x0600084C RID: 2124 RVA: 0x00010A4A File Offset: 0x0000EC4A
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

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x0600084D RID: 2125 RVA: 0x00010A5E File Offset: 0x0000EC5E
		// (set) Token: 0x0600084E RID: 2126 RVA: 0x00010A66 File Offset: 0x0000EC66
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

		// Token: 0x0600084F RID: 2127 RVA: 0x00010A7C File Offset: 0x0000EC7C
		[OriginalName("GetHistoryOptions")]
		public DataServiceQuerySingle<ItemHistoryOptions> GetHistoryOptions()
		{
			Uri uri;
			base.Context.TryGetUri(this, out uri);
			return base.Context.CreateFunctionQuerySingle<ItemHistoryOptions>(string.Join("/", from s in uri.Segments.Skip(base.Context.BaseUri.Segments.Length)
				select s.Trim(new char[] { '/' })), "Model.GetHistoryOptions", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x00010AFC File Offset: 0x0000ECFC
		[OriginalName("GetReportHistorySnapshotsOptions")]
		public DataServiceQuerySingle<ReportHistorySnapshotsOptions> GetReportHistorySnapshotsOptions()
		{
			Uri uri;
			base.Context.TryGetUri(this, out uri);
			return base.Context.CreateFunctionQuerySingle<ReportHistorySnapshotsOptions>(string.Join("/", from s in uri.Segments.Skip(base.Context.BaseUri.Segments.Length)
				select s.Trim(new char[] { '/' })), "Model.GetReportHistorySnapshotsOptions", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x00010B7C File Offset: 0x0000ED7C
		[OriginalName("GetCacheOptions")]
		public DataServiceQuerySingle<CacheOptions> GetCacheOptions()
		{
			Uri uri;
			base.Context.TryGetUri(this, out uri);
			return base.Context.CreateFunctionQuerySingle<CacheOptions>(string.Join("/", from s in uri.Segments.Skip(base.Context.BaseUri.Segments.Length)
				select s.Trim(new char[] { '/' })), "Model.GetCacheOptions", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x00010BFC File Offset: 0x0000EDFC
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

		// Token: 0x06000853 RID: 2131 RVA: 0x00010C70 File Offset: 0x0000EE70
		[OriginalName("SetParameterProperties")]
		public DataServiceActionQuery SetParameterProperties(ICollection<ReportParameterDefinition> ParameterProperties)
		{
			EntityDescriptor entityDescriptor = base.Context.EntityTracker.TryGetEntityDescriptor(this);
			if (entityDescriptor == null)
			{
				throw new Exception("cannot find entity");
			}
			return new DataServiceActionQuery(base.Context, entityDescriptor.EditLink.OriginalString.Trim(new char[] { '/' }) + "/Model.SetParameterProperties", new BodyOperationParameter[]
			{
				new BodyOperationParameter("ParameterProperties", ParameterProperties)
			});
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x00010CE4 File Offset: 0x0000EEE4
		[OriginalName("SetReportHistorySnapshotsOptions")]
		public DataServiceActionQuery SetReportHistorySnapshotsOptions(ReportHistorySnapshotsOptions ReportHistorySnapshotsOptions)
		{
			EntityDescriptor entityDescriptor = base.Context.EntityTracker.TryGetEntityDescriptor(this);
			if (entityDescriptor == null)
			{
				throw new Exception("cannot find entity");
			}
			return new DataServiceActionQuery(base.Context, entityDescriptor.EditLink.OriginalString.Trim(new char[] { '/' }) + "/Model.SetReportHistorySnapshotsOptions", new BodyOperationParameter[]
			{
				new BodyOperationParameter("ReportHistorySnapshotsOptions", ReportHistorySnapshotsOptions)
			});
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x00010D58 File Offset: 0x0000EF58
		[OriginalName("CreateSnapshot")]
		public DataServiceActionQuerySingle<string> CreateSnapshot()
		{
			EntityDescriptor entityDescriptor = base.Context.EntityTracker.TryGetEntityDescriptor(this);
			if (entityDescriptor == null)
			{
				throw new Exception("cannot find entity");
			}
			return new DataServiceActionQuerySingle<string>(base.Context, entityDescriptor.EditLink.OriginalString.Trim(new char[] { '/' }) + "/Model.CreateSnapshot", Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x00010DBC File Offset: 0x0000EFBC
		[OriginalName("DeleteSnapshot")]
		public DataServiceActionQuerySingle<bool> DeleteSnapshot(string HistoryId)
		{
			EntityDescriptor entityDescriptor = base.Context.EntityTracker.TryGetEntityDescriptor(this);
			if (entityDescriptor == null)
			{
				throw new Exception("cannot find entity");
			}
			return new DataServiceActionQuerySingle<bool>(base.Context, entityDescriptor.EditLink.OriginalString.Trim(new char[] { '/' }) + "/Model.DeleteSnapshot", new BodyOperationParameter[]
			{
				new BodyOperationParameter("HistoryId", HistoryId)
			});
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x00010E30 File Offset: 0x0000F030
		[OriginalName("UpdateExecutionSnapshot")]
		public DataServiceActionQuerySingle<bool> UpdateExecutionSnapshot()
		{
			EntityDescriptor entityDescriptor = base.Context.EntityTracker.TryGetEntityDescriptor(this);
			if (entityDescriptor == null)
			{
				throw new Exception("cannot find entity");
			}
			return new DataServiceActionQuerySingle<bool>(base.Context, entityDescriptor.EditLink.OriginalString.Trim(new char[] { '/' }) + "/Model.UpdateExecutionSnapshot", Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x00010E94 File Offset: 0x0000F094
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

		// Token: 0x06000859 RID: 2137 RVA: 0x00010F08 File Offset: 0x0000F108
		[OriginalName("UpdateItemDataSources")]
		public DataServiceActionQuery UpdateItemDataSources(ICollection<DataSource> dataSources)
		{
			EntityDescriptor entityDescriptor = base.Context.EntityTracker.TryGetEntityDescriptor(this);
			if (entityDescriptor == null)
			{
				throw new Exception("cannot find entity");
			}
			return new DataServiceActionQuery(base.Context, entityDescriptor.EditLink.OriginalString.Trim(new char[] { '/' }) + "/Model.UpdateItemDataSources", new BodyOperationParameter[]
			{
				new BodyOperationParameter("dataSources", dataSources)
			});
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x00010F7C File Offset: 0x0000F17C
		[OriginalName("SetCacheOptions")]
		public DataServiceActionQuery SetCacheOptions(CacheOptions cacheOptions)
		{
			EntityDescriptor entityDescriptor = base.Context.EntityTracker.TryGetEntityDescriptor(this);
			if (entityDescriptor == null)
			{
				throw new Exception("cannot find entity");
			}
			return new DataServiceActionQuery(base.Context, entityDescriptor.EditLink.OriginalString.Trim(new char[] { '/' }) + "/Model.SetCacheOptions", new BodyOperationParameter[]
			{
				new BodyOperationParameter("cacheOptions", cacheOptions)
			});
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x00010FF0 File Offset: 0x0000F1F0
		[OriginalName("UpdateReportDataSets")]
		public DataServiceActionQuery UpdateReportDataSets(ICollection<DataSet> dataSets)
		{
			EntityDescriptor entityDescriptor = base.Context.EntityTracker.TryGetEntityDescriptor(this);
			if (entityDescriptor == null)
			{
				throw new Exception("cannot find entity");
			}
			return new DataServiceActionQuery(base.Context, entityDescriptor.EditLink.OriginalString.Trim(new char[] { '/' }) + "/Model.UpdateReportDataSets", new BodyOperationParameter[]
			{
				new BodyOperationParameter("dataSets", dataSets)
			});
		}

		// Token: 0x040003F5 RID: 1013
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _HasDataSources;

		// Token: 0x040003F6 RID: 1014
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _HasSharedDataSets;

		// Token: 0x040003F7 RID: 1015
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _HasParameters;

		// Token: 0x040003F8 RID: 1016
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<Subscription> _Subscriptions = new DataServiceCollection<Subscription>(null, TrackingMode.None);

		// Token: 0x040003F9 RID: 1017
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<CacheRefreshPlan> _CacheRefreshPlans = new DataServiceCollection<CacheRefreshPlan>(null, TrackingMode.None);

		// Token: 0x040003FA RID: 1018
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<DataSource> _DataSources = new DataServiceCollection<DataSource>(null, TrackingMode.None);

		// Token: 0x040003FB RID: 1019
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<DataSet> _SharedDataSets = new DataServiceCollection<DataSet>(null, TrackingMode.None);

		// Token: 0x040003FC RID: 1020
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private HistorySnapshotOptions _HistorySnapshotOptions;

		// Token: 0x040003FD RID: 1021
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<ReportHistorySnapshot> _ReportHistorySnapshots = new DataServiceCollection<ReportHistorySnapshot>(null, TrackingMode.None);

		// Token: 0x040003FE RID: 1022
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<HistorySnapshot> _HistorySnapshots = new DataServiceCollection<HistorySnapshot>(null, TrackingMode.None);

		// Token: 0x040003FF RID: 1023
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<ReportParameterDefinition> _ParameterDefinitions = new DataServiceCollection<ReportParameterDefinition>(null, TrackingMode.None);
	}
}
