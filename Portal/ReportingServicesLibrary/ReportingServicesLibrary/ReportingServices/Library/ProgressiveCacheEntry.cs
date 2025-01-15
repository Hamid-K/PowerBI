using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Xml;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Caching;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.ProgressiveReport;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200018A RID: 394
	internal class ProgressiveCacheEntry : IDisposable
	{
		// Token: 0x06000E5F RID: 3679 RVA: 0x00034C74 File Offset: 0x00032E74
		internal ProgressiveCacheEntry(string userName)
		{
			RSTrace.CatalogTrace.Assert(userName != null, "ReportCacheEntry.ctor: userName != null");
			this.m_userName = userName;
			this.m_concurrencyEnabledJobIds = Hashtable.Synchronized(new Hashtable());
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x00034CB1 File Offset: 0x00032EB1
		internal ProgressiveCacheEntry(string userName, PowerViewSessionType sessionType)
			: this(userName, sessionType, null)
		{
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x00034CBC File Offset: 0x00032EBC
		internal ProgressiveCacheEntry(string userName, PowerViewSessionType sessionType, IDataSourceRewriter dataSourceRewriter)
			: this(userName)
		{
			this.m_sessionType = sessionType;
			this.m_dataSourceRewriter = dataSourceRewriter;
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x00034CD3 File Offset: 0x00032ED3
		internal static bool IsVrmOrDataShape(PowerViewSessionType sessionType)
		{
			return sessionType == PowerViewSessionType.DataShape || sessionType == PowerViewSessionType.Vrm;
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06000E63 RID: 3683 RVA: 0x00034CDF File Offset: 0x00032EDF
		internal bool IsPowerView
		{
			get
			{
				return ProgressiveCacheEntry.IsVrmOrDataShape(this.m_sessionType);
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06000E64 RID: 3684 RVA: 0x00034CEC File Offset: 0x00032EEC
		// (set) Token: 0x06000E65 RID: 3685 RVA: 0x00034CF4 File Offset: 0x00032EF4
		internal long PowerBIModelId { get; set; }

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06000E66 RID: 3686 RVA: 0x00034CFD File Offset: 0x00032EFD
		// (set) Token: 0x06000E67 RID: 3687 RVA: 0x00034D05 File Offset: 0x00032F05
		internal ProgressiveReport Report
		{
			get
			{
				return this.m_report;
			}
			set
			{
				this.m_report = value;
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06000E68 RID: 3688 RVA: 0x00034D0E File Offset: 0x00032F0E
		// (set) Token: 0x06000E69 RID: 3689 RVA: 0x00034D16 File Offset: 0x00032F16
		internal string DataSourceName { get; set; }

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06000E6A RID: 3690 RVA: 0x00034D1F File Offset: 0x00032F1F
		// (set) Token: 0x06000E6B RID: 3691 RVA: 0x00034D27 File Offset: 0x00032F27
		internal DataSourceInfoCollection DataSources
		{
			get
			{
				return this.m_dataSources;
			}
			set
			{
				this.m_dataSources = value;
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06000E6C RID: 3692 RVA: 0x00034D30 File Offset: 0x00032F30
		// (set) Token: 0x06000E6D RID: 3693 RVA: 0x00034D38 File Offset: 0x00032F38
		internal DataSourceInfo InternalDataSourceInfo
		{
			get
			{
				return this.m_internalDataSourceInfo;
			}
			set
			{
				this.m_internalDataSourceInfo = value;
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06000E6E RID: 3694 RVA: 0x00034D41 File Offset: 0x00032F41
		// (set) Token: 0x06000E6F RID: 3695 RVA: 0x00034D4B File Offset: 0x00032F4B
		internal ICacheItemVersion CachedVersion
		{
			get
			{
				return this.m_cachedItemVersion;
			}
			set
			{
				this.m_cachedItemVersion = value;
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06000E70 RID: 3696 RVA: 0x00034D56 File Offset: 0x00032F56
		internal string UserName
		{
			get
			{
				return this.m_userName;
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06000E71 RID: 3697 RVA: 0x00034D5E File Offset: 0x00032F5E
		// (set) Token: 0x06000E72 RID: 3698 RVA: 0x00034D66 File Offset: 0x00032F66
		internal IDbConnectionPool ConnectionPool
		{
			get
			{
				return this.m_connectionPool;
			}
			set
			{
				RSTrace.CatalogTrace.Assert(this.m_connectionPool == null, "Cannot set ConnectionPool twice");
				RSTrace.CatalogTrace.Assert(value != null, "Cannot set ConnectionPool to null");
				this.m_connectionPool = value;
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06000E73 RID: 3699 RVA: 0x00034D9A File Offset: 0x00032F9A
		// (set) Token: 0x06000E74 RID: 3700 RVA: 0x00034DA2 File Offset: 0x00032FA2
		internal string ClientActivityId { get; set; }

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06000E75 RID: 3701 RVA: 0x00034DAC File Offset: 0x00032FAC
		internal IList<string> RunningJobs
		{
			get
			{
				IList<string> list = new List<string>();
				string currentRenderEditJobId = this.m_currentRenderEditJobId;
				if (currentRenderEditJobId != null)
				{
					list.Add(currentRenderEditJobId);
				}
				object syncRoot = this.m_concurrencyEnabledJobIds.SyncRoot;
				lock (syncRoot)
				{
					foreach (object obj in this.m_concurrencyEnabledJobIds.Keys)
					{
						string text = (string)obj;
						list.Add(text);
					}
				}
				return list;
			}
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x00034E58 File Offset: 0x00033058
		internal virtual bool Validate(string sessionId, string userName, string operationName)
		{
			if (this.UserName != userName)
			{
				RSTrace.CatalogTrace.Trace(TraceLevel.Error, "{0}: session {1} requested by {2} belongs to {3}", new object[] { operationName, sessionId, userName, this.UserName });
				return false;
			}
			return true;
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x00034E96 File Offset: 0x00033096
		private bool CanPerformRenderEditOnSession(string renderEditJobId)
		{
			bool flag = Interlocked.CompareExchange<string>(ref this.m_currentRenderEditJobId, renderEditJobId, null) == null;
			if (flag)
			{
				this.ConcurrentProgressiveActionStarted();
			}
			return flag;
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x00034EB1 File Offset: 0x000330B1
		private void RenderEditRequestComplete(string renderEditJobId)
		{
			if (Interlocked.CompareExchange<string>(ref this.m_currentRenderEditJobId, null, renderEditJobId) == renderEditJobId)
			{
				this.ConcurrentProgressiveActionCompleted();
			}
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x00034ECE File Offset: 0x000330CE
		internal void ConcurrentProgressiveActionStarted()
		{
			Interlocked.Increment(ref this.m_concurrentRequestCounter);
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x00034EDC File Offset: 0x000330DC
		internal void ConcurrentProgressiveActionCompleted()
		{
			Interlocked.Decrement(ref this.m_concurrentRequestCounter);
			this.TryDelayDisposeConnectionPool();
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x00034EF0 File Offset: 0x000330F0
		internal void ConcurrentNativeActionStarted()
		{
			Interlocked.Increment(ref this.m_concurrentNativeRequests);
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x00034EFE File Offset: 0x000330FE
		internal void ConcurrentNativeActionEnded()
		{
			Interlocked.Decrement(ref this.m_concurrentNativeRequests);
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x00034F0C File Offset: 0x0003310C
		private void TryDelayDisposeConnectionPool()
		{
			if (this.m_disposeConnectionPoolOnComplete && this.CanDisposeConnectionPool())
			{
				object delayDisposeSync = this.m_delayDisposeSync;
				lock (delayDisposeSync)
				{
					this.DisposeConnectionPool();
				}
			}
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x00034F5C File Offset: 0x0003315C
		internal bool AddJob(string jobId, bool isRenderEdit)
		{
			if (isRenderEdit)
			{
				return this.CanPerformRenderEditOnSession(jobId);
			}
			this.m_concurrencyEnabledJobIds.Add(jobId, null);
			return true;
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x00034F77 File Offset: 0x00033177
		internal void RemoveJob(string jobId, bool isRenderEdit)
		{
			if (isRenderEdit)
			{
				this.RenderEditRequestComplete(jobId);
				return;
			}
			this.m_concurrencyEnabledJobIds.Remove(jobId);
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x00034F90 File Offset: 0x00033190
		public void Dispose()
		{
			if (this.m_connectionPool != null && this.CanDisposeConnectionPool())
			{
				this.DisposeConnectionPool();
				return;
			}
			RSTrace.CacheTracer.Trace(TraceLevel.Verbose, "CacheEntry - Cleanup: Cannot immediately dispose connection pool because of in-flight RenderEdit/ExecuteQueries request on this session; cleanup happens after all in-flight requests finished");
			this.m_disposeConnectionPoolOnComplete = true;
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x00034FC0 File Offset: 0x000331C0
		private bool CanDisposeConnectionPool()
		{
			return this.m_concurrentRequestCounter == 0;
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x00034FCB File Offset: 0x000331CB
		private bool CanDisposeNativeSession()
		{
			return this.m_concurrentNativeRequests == 0;
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x00034FD8 File Offset: 0x000331D8
		private void DisposeConnectionPool()
		{
			IDisposable disposable = this.m_connectionPool as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
			this.m_connectionPool = null;
			this.m_disposeConnectionPoolOnComplete = false;
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x00035008 File Offset: 0x00033208
		internal void AddDataSourceInfo(string itemPath, string dataSourceName, DataSourceInfo dsInfo)
		{
			if (this.DataSources == null)
			{
				this.DataSources = new DataSourceInfoCollection();
			}
			string dataSourceInfoKey = this.GetDataSourceInfoKey(itemPath, dataSourceName);
			this.DataSources.AddOrUpdate(dataSourceInfoKey, dsInfo);
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x0003503E File Offset: 0x0003323E
		internal string GetDataSourceInfoKey(string itemPath, string dsName)
		{
			if (this.IsPowerView)
			{
				if (itemPath == null)
				{
					itemPath = string.Empty;
				}
				return string.Format(CultureInfo.InvariantCulture, "{0}|{1}", RenderEditRequest.EncodeCacheKeyValue(itemPath.ToUpperInvariant()), RenderEditRequest.EncodeCacheKeyValue(dsName));
			}
			return dsName;
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x00035074 File Offset: 0x00033274
		internal DataSourceInfo LoadDataSourceInfoFromCache(string key)
		{
			DataSourceInfo dataSourceInfo = null;
			if (!string.IsNullOrEmpty(key) && this.DataSources != null)
			{
				dataSourceInfo = this.DataSources.GetDataSourceFromKey(key);
			}
			return dataSourceInfo;
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x000350A1 File Offset: 0x000332A1
		internal string GetTenantName()
		{
			if (this.InternalDataSourceInfo == null)
			{
				return string.Empty;
			}
			return this.InternalDataSourceInfo.TenantName;
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x000350BC File Offset: 0x000332BC
		public void PopulateCacheEntryWithDataSourceInfo(string itemPath, string content)
		{
			using (StringReader stringReader = new StringReader(content))
			{
				using (XmlReader xmlReader = XmlReader.Create(stringReader, XmlUtil.ApplyDtdDosDefense(new XmlReaderSettings
				{
					CloseInput = true
				})))
				{
					this.PopulateCacheEntryWithDataSourceInfo(itemPath, xmlReader);
				}
			}
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x00035124 File Offset: 0x00033324
		public void PopulateCacheEntryWithDataSourceInfo(Stream dsStream)
		{
			using (XmlReader xmlReader = XmlReader.Create(dsStream, XmlUtil.ApplyDtdDosDefense(new XmlReaderSettings
			{
				CloseInput = true
			})))
			{
				this.PopulateCacheEntryWithDataSourceInfo(null, xmlReader);
			}
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x00035170 File Offset: 0x00033370
		public void PopulateCacheEntryWithPowerBIOnPremDataSourceInfo(IModelDataSourceResolver dataSourceResolver, string itemPath, DataSourceDefinition2Collection dsdefs)
		{
			this.PopulateCacheEntryWithDataSourceInfo(dataSourceResolver, itemPath, this.DataSources, dsdefs, true);
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x00035184 File Offset: 0x00033384
		private void PopulateCacheEntryWithDataSourceInfo(string itemPath, XmlReader reader)
		{
			DataSourceDefinition2Collection dataSourceDefinition2Collection = new DataSourceDefinition2Collection();
			try
			{
				dataSourceDefinition2Collection.ReadXml(reader);
			}
			catch (XmlException ex)
			{
				throw new MalformedXmlException(ex);
			}
			if (dataSourceDefinition2Collection.Count == 0)
			{
				throw new MissingElementException("DataSource");
			}
			this.PopulateCacheEntryWithDataSourceInfo(null, itemPath, this.DataSources, dataSourceDefinition2Collection, false);
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x000351DC File Offset: 0x000333DC
		private void PopulateCacheEntryWithDataSourceInfo(IModelDataSourceResolver dataSourceResolver, string itemPath, DataSourceInfoCollection cachedDataSources, DataSourceDefinition2Collection dataSourceDefinitions, bool powerbiOnPremDatasources)
		{
			if (dataSourceResolver == null && powerbiOnPremDatasources)
			{
				throw new ArgumentException("ModelDataSourceResolver need to provided for PowerBI datasources", "dataSourceResolver");
			}
			foreach (KeyValuePair<string, DataSourceReferenceOrDefinition> keyValuePair in dataSourceDefinitions)
			{
				DataSourceInfo dataSourceInfo;
				if (keyValuePair.Value.IsReference)
				{
					dataSourceInfo = new DataSourceInfo(keyValuePair.Key, keyValuePair.Value.Reference, Guid.Empty);
				}
				else
				{
					dataSourceInfo = new DataSourceInfo(keyValuePair.Key, keyValuePair.Key);
					DataSourceDefinition2 definition = keyValuePair.Value.Definition;
					dataSourceInfo.Extension = Global.DaxDataExtensionName;
					if (definition.IsFullyFormedExternalConnectionString)
					{
						RSTrace.CacheTracer.Trace(TraceLevel.Info, "Fully formed external connection string, Model Id={0}", new object[] { this.PowerBIModelId });
						dataSourceInfo.IsExternalDataSource = true;
						dataSourceInfo.IsFullyFormedExternalDataSource = true;
						dataSourceResolver.ResolveOnPremiseModelConnectionString(dataSourceInfo, definition.ConnectString, definition.OnPremiseModelIdentityClaims, DataProtection.Instance);
					}
					else if (powerbiOnPremDatasources || definition.IsOnPremiseModel)
					{
						RSTrace.CacheTracer.Assert(this.PowerBIModelId == 0L || this.PowerBIModelId == definition.PowerBIModelId, "Only a single model is supported");
						RSTrace.CacheTracer.Trace(TraceLevel.Info, "Populate datasource, Id={0}", new object[] { this.PowerBIModelId });
						dataSourceInfo.IsExternalDataSource = true;
						dataSourceInfo.IsFullyFormedExternalDataSource = false;
						dataSourceResolver.ResolveOnPremiseModelConnectionString(dataSourceInfo, definition.ConnectString, definition.OnPremiseModelIdentityClaims, DataProtection.Instance);
					}
					else
					{
						this.RewriteDataSourceDefinition(keyValuePair.Key, definition);
						dataSourceInfo.SetConnectionString(definition.ConnectString, DataProtection.Instance);
					}
					dataSourceInfo.CredentialsRetrieval = DataSourceResolverHelper.ConvertToDataSourceInfoCredentials(definition.CredentialRetrieval);
					dataSourceInfo.WindowsCredentials = definition.WindowsCredentials;
					dataSourceInfo.ImpersonateUser = definition.ImpersonateUser;
					dataSourceInfo.ModelPerspectiveName = definition.PerspectiveName;
					dataSourceInfo.ModelLastUpdatedTime = definition.PowerBIModelLastUpdatedTime;
					dataSourceInfo.IsMultiDimensional = definition.IsMultiDimensional;
					if (definition.SecureStoreLookup != null)
					{
						throw new SecureStoreUnsupportedSharePointVersionException();
					}
					dataSourceInfo.ValidateDefinition(false);
					DataSourceUtility.ThrowIfNotGoodForRdlx(dataSourceInfo, dataSourceInfo.Name);
					this.PowerBIModelId = definition.PowerBIModelId;
				}
				Guid guid;
				if (cachedDataSources != null && cachedDataSources.TryGetCachedDataSourceId(dataSourceInfo.OriginalName, out guid))
				{
					dataSourceInfo.ID = guid;
				}
				dataSourceInfo.ReferenceIsValid = true;
				dataSourceInfo.Enabled = true;
				DataSourceResolverHelper.ValidateDataSource(dataSourceInfo);
				this.AddDataSourceInfo(itemPath, dataSourceInfo.Name, dataSourceInfo);
			}
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x00035474 File Offset: 0x00033674
		private void RewriteDataSourceDefinition(string dataSourceName, DataSourceDefinition2 definition)
		{
			if (this.m_dataSourceRewriter != null && this.m_dataSourceRewriter.NeedsRewrite(dataSourceName))
			{
				definition.ConnectString = this.m_dataSourceRewriter.RewriteConnectionString(dataSourceName, definition);
			}
		}

		// Token: 0x040005F2 RID: 1522
		private readonly PowerViewSessionType m_sessionType;

		// Token: 0x040005F3 RID: 1523
		private readonly IDataSourceRewriter m_dataSourceRewriter;

		// Token: 0x040005F4 RID: 1524
		private ProgressiveReport m_report;

		// Token: 0x040005F5 RID: 1525
		private DataSourceInfoCollection m_dataSources;

		// Token: 0x040005F6 RID: 1526
		private volatile ICacheItemVersion m_cachedItemVersion;

		// Token: 0x040005F7 RID: 1527
		private readonly string m_userName;

		// Token: 0x040005F8 RID: 1528
		private IDbConnectionPool m_connectionPool;

		// Token: 0x040005F9 RID: 1529
		private DataSourceInfo m_internalDataSourceInfo;

		// Token: 0x040005FA RID: 1530
		private string m_currentRenderEditJobId;

		// Token: 0x040005FB RID: 1531
		private readonly Hashtable m_concurrencyEnabledJobIds;

		// Token: 0x040005FC RID: 1532
		private bool m_disposeConnectionPoolOnComplete;

		// Token: 0x040005FD RID: 1533
		private int m_concurrentRequestCounter;

		// Token: 0x040005FE RID: 1534
		private object m_delayDisposeSync = new object();

		// Token: 0x040005FF RID: 1535
		private int m_concurrentNativeRequests;
	}
}
