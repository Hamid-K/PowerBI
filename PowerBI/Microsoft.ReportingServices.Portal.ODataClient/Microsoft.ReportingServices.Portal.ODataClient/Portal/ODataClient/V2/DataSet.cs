using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000023 RID: 35
	[Key("Id")]
	[EntitySet("DataSets")]
	[OriginalName("DataSet")]
	public class DataSet : CatalogItem
	{
		// Token: 0x0600016B RID: 363 RVA: 0x00004218 File Offset: 0x00002418
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static DataSet CreateDataSet(Guid ID, CatalogItemType type, bool hidden, long size, DateTimeOffset modifiedDate, DateTimeOffset createdDate, bool isFavorite, bool hasParameters, int queryExecutionTimeOut)
		{
			return new DataSet
			{
				Id = ID,
				Type = type,
				Hidden = hidden,
				Size = size,
				ModifiedDate = modifiedDate,
				CreatedDate = createdDate,
				IsFavorite = isFavorite,
				HasParameters = hasParameters,
				QueryExecutionTimeOut = queryExecutionTimeOut
			};
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600016C RID: 364 RVA: 0x0000426E File Offset: 0x0000246E
		// (set) Token: 0x0600016D RID: 365 RVA: 0x00004276 File Offset: 0x00002476
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

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600016E RID: 366 RVA: 0x0000428A File Offset: 0x0000248A
		// (set) Token: 0x0600016F RID: 367 RVA: 0x00004292 File Offset: 0x00002492
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("QueryExecutionTimeOut")]
		public int QueryExecutionTimeOut
		{
			get
			{
				return this._QueryExecutionTimeOut;
			}
			set
			{
				this._QueryExecutionTimeOut = value;
				this.OnPropertyChanged("QueryExecutionTimeOut");
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000170 RID: 368 RVA: 0x000042A6 File Offset: 0x000024A6
		// (set) Token: 0x06000171 RID: 369 RVA: 0x000042AE File Offset: 0x000024AE
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Data")]
		public DataServiceCollection<DataSetRow> Data
		{
			get
			{
				return this._Data;
			}
			set
			{
				this._Data = value;
				this.OnPropertyChanged("Data");
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000172 RID: 370 RVA: 0x000042C2 File Offset: 0x000024C2
		// (set) Token: 0x06000173 RID: 371 RVA: 0x000042CA File Offset: 0x000024CA
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

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000174 RID: 372 RVA: 0x000042DE File Offset: 0x000024DE
		// (set) Token: 0x06000175 RID: 373 RVA: 0x000042E6 File Offset: 0x000024E6
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

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000176 RID: 374 RVA: 0x000042FA File Offset: 0x000024FA
		// (set) Token: 0x06000177 RID: 375 RVA: 0x00004302 File Offset: 0x00002502
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

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000178 RID: 376 RVA: 0x00004316 File Offset: 0x00002516
		// (set) Token: 0x06000179 RID: 377 RVA: 0x0000431E File Offset: 0x0000251E
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

		// Token: 0x0600017A RID: 378 RVA: 0x00004334 File Offset: 0x00002534
		[OriginalName("GetSchema")]
		public DataServiceQuerySingle<DataSetSchema> GetSchema()
		{
			Uri uri;
			base.Context.TryGetUri(this, out uri);
			return base.Context.CreateFunctionQuerySingle<DataSetSchema>(string.Join("/", from s in uri.Segments.Skip(base.Context.BaseUri.Segments.Length)
				select s.Trim(new char[] { '/' })), "Model.GetSchema", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x0600017B RID: 379 RVA: 0x000043B4 File Offset: 0x000025B4
		[OriginalName("Upload")]
		public DataServiceActionQuerySingle<DataSet> Upload()
		{
			EntityDescriptor entityDescriptor = base.Context.EntityTracker.TryGetEntityDescriptor(this);
			if (entityDescriptor == null)
			{
				throw new Exception("cannot find entity");
			}
			return new DataServiceActionQuerySingle<DataSet>(base.Context, entityDescriptor.EditLink.OriginalString.Trim(new char[] { '/' }) + "/Model.Upload", Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00004418 File Offset: 0x00002618
		[OriginalName("GetData")]
		public DataServiceActionQuerySingle<string> GetData(ICollection<DataSetParameter> Parameters, int? maxRows)
		{
			EntityDescriptor entityDescriptor = base.Context.EntityTracker.TryGetEntityDescriptor(this);
			if (entityDescriptor == null)
			{
				throw new Exception("cannot find entity");
			}
			return new DataServiceActionQuerySingle<string>(base.Context, entityDescriptor.EditLink.OriginalString.Trim(new char[] { '/' }) + "/Model.GetData", new BodyOperationParameter[]
			{
				new BodyOperationParameter("Parameters", Parameters),
				new BodyOperationParameter("maxRows", maxRows)
			});
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000449C File Offset: 0x0000269C
		[OriginalName("GetKpiTrendsetData")]
		public DataServiceActionQuerySingle<string> GetKpiTrendsetData(ICollection<DataSetParameter> Parameters, string columnName)
		{
			EntityDescriptor entityDescriptor = base.Context.EntityTracker.TryGetEntityDescriptor(this);
			if (entityDescriptor == null)
			{
				throw new Exception("cannot find entity");
			}
			return new DataServiceActionQuerySingle<string>(base.Context, entityDescriptor.EditLink.OriginalString.Trim(new char[] { '/' }) + "/Model.GetKpiTrendsetData", new BodyOperationParameter[]
			{
				new BodyOperationParameter("Parameters", Parameters),
				new BodyOperationParameter("columnName", columnName)
			});
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000451C File Offset: 0x0000271C
		[OriginalName("GetAggregatedValue")]
		public DataServiceActionQuerySingle<string> GetAggregatedValue(ICollection<DataSetParameter> Parameters)
		{
			EntityDescriptor entityDescriptor = base.Context.EntityTracker.TryGetEntityDescriptor(this);
			if (entityDescriptor == null)
			{
				throw new Exception("cannot find entity");
			}
			return new DataServiceActionQuerySingle<string>(base.Context, entityDescriptor.EditLink.OriginalString.Trim(new char[] { '/' }) + "/Model.GetAggregatedValue", new BodyOperationParameter[]
			{
				new BodyOperationParameter("Parameters", Parameters)
			});
		}

		// Token: 0x040000CB RID: 203
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _HasParameters;

		// Token: 0x040000CC RID: 204
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private int _QueryExecutionTimeOut;

		// Token: 0x040000CD RID: 205
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<DataSetRow> _Data = new DataServiceCollection<DataSetRow>(null, TrackingMode.None);

		// Token: 0x040000CE RID: 206
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<DataSource> _DataSources = new DataServiceCollection<DataSource>(null, TrackingMode.None);

		// Token: 0x040000CF RID: 207
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<CacheRefreshPlan> _CacheRefreshPlans = new DataServiceCollection<CacheRefreshPlan>(null, TrackingMode.None);

		// Token: 0x040000D0 RID: 208
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<ReportParameterDefinition> _ParameterDefinitions = new DataServiceCollection<ReportParameterDefinition>(null, TrackingMode.None);

		// Token: 0x040000D1 RID: 209
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private CacheOptions _CacheOptions;
	}
}
