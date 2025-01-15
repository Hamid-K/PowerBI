using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000C5 RID: 197
	[Key("Id")]
	[OriginalName("DataSet")]
	public class DataSet : CatalogItem
	{
		// Token: 0x060008B5 RID: 2229 RVA: 0x00011D3C File Offset: 0x0000FF3C
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

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x060008B6 RID: 2230 RVA: 0x00011D92 File Offset: 0x0000FF92
		// (set) Token: 0x060008B7 RID: 2231 RVA: 0x00011D9A File Offset: 0x0000FF9A
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

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x060008B8 RID: 2232 RVA: 0x00011DAE File Offset: 0x0000FFAE
		// (set) Token: 0x060008B9 RID: 2233 RVA: 0x00011DB6 File Offset: 0x0000FFB6
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

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x060008BA RID: 2234 RVA: 0x00011DCA File Offset: 0x0000FFCA
		// (set) Token: 0x060008BB RID: 2235 RVA: 0x00011DD2 File Offset: 0x0000FFD2
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

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x060008BC RID: 2236 RVA: 0x00011DE6 File Offset: 0x0000FFE6
		// (set) Token: 0x060008BD RID: 2237 RVA: 0x00011DEE File Offset: 0x0000FFEE
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

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x060008BE RID: 2238 RVA: 0x00011E02 File Offset: 0x00010002
		// (set) Token: 0x060008BF RID: 2239 RVA: 0x00011E0A File Offset: 0x0001000A
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

		// Token: 0x060008C0 RID: 2240 RVA: 0x00011E20 File Offset: 0x00010020
		[OriginalName("GetSchema")]
		public DataServiceQuerySingle<DataSetSchema> GetSchema()
		{
			Uri uri;
			base.Context.TryGetUri(this, out uri);
			return base.Context.CreateFunctionQuerySingle<DataSetSchema>(string.Join("/", from s in uri.Segments.Skip(base.Context.BaseUri.Segments.Length)
				select s.Trim(new char[] { '/' })), "Model.GetSchema", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x00011EA0 File Offset: 0x000100A0
		[OriginalName("GetTable")]
		public DataServiceQuerySingle<string> GetTable()
		{
			Uri uri;
			base.Context.TryGetUri(this, out uri);
			return base.Context.CreateFunctionQuerySingle<string>(string.Join("/", from s in uri.Segments.Skip(base.Context.BaseUri.Segments.Length)
				select s.Trim(new char[] { '/' })), "Model.GetTable", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x00011F20 File Offset: 0x00010120
		[OriginalName("GetTable")]
		public DataServiceQuerySingle<string> GetTable(int maxRows)
		{
			Uri uri;
			base.Context.TryGetUri(this, out uri);
			return base.Context.CreateFunctionQuerySingle<string>(string.Join("/", from s in uri.Segments.Skip(base.Context.BaseUri.Segments.Length)
				select s.Trim(new char[] { '/' })), "Model.GetTable", false, new UriOperationParameter[]
			{
				new UriOperationParameter("maxRows", maxRows)
			});
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x00011FB4 File Offset: 0x000101B4
		[OriginalName("GetCacheOptions")]
		public DataServiceQuerySingle<CacheOptions> GetCacheOptions()
		{
			Uri uri;
			base.Context.TryGetUri(this, out uri);
			return base.Context.CreateFunctionQuerySingle<CacheOptions>(string.Join("/", from s in uri.Segments.Skip(base.Context.BaseUri.Segments.Length)
				select s.Trim(new char[] { '/' })), "Model.GetCacheOptions", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x00012034 File Offset: 0x00010234
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

		// Token: 0x060008C5 RID: 2245 RVA: 0x000120B8 File Offset: 0x000102B8
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

		// Token: 0x060008C6 RID: 2246 RVA: 0x00012138 File Offset: 0x00010338
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

		// Token: 0x060008C7 RID: 2247 RVA: 0x000121AC File Offset: 0x000103AC
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

		// Token: 0x060008C8 RID: 2248 RVA: 0x00012220 File Offset: 0x00010420
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

		// Token: 0x0400042A RID: 1066
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _HasParameters;

		// Token: 0x0400042B RID: 1067
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private int _QueryExecutionTimeOut;

		// Token: 0x0400042C RID: 1068
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<DataSource> _DataSources = new DataServiceCollection<DataSource>(null, TrackingMode.None);

		// Token: 0x0400042D RID: 1069
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<CacheRefreshPlan> _CacheRefreshPlans = new DataServiceCollection<CacheRefreshPlan>(null, TrackingMode.None);

		// Token: 0x0400042E RID: 1070
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<ReportParameterDefinition> _ParameterDefinitions = new DataServiceCollection<ReportParameterDefinition>(null, TrackingMode.None);
	}
}
