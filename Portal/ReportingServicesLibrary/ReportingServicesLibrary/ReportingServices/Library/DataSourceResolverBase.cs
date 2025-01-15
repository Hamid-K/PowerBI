using System;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.DataProcessing;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Internal;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200017B RID: 379
	internal abstract class DataSourceResolverBase : IModelDataSourceResolver
	{
		// Token: 0x06000DDF RID: 3551 RVA: 0x00032B3C File Offset: 0x00030D3C
		protected DataSourceResolverBase(RSService service, CatalogItemContext itemContext, string itemPath)
		{
			this.m_service = service;
			this.m_itemContext = itemContext;
			this.m_itemPath = itemPath;
			this.m_cancelationHelper = new DataSourceResolverCancelationHelper<IModelDataExtension>(delegate(IModelDataExtension mde)
			{
				mde.CancelModelMetadataRetrieval();
			});
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x00032B8E File Offset: 0x00030D8E
		public virtual DataSourceInfo RebuildAndResolveDataSource(ProgressiveCacheEntry entry, string dataSourceName, bool isDataSourcesPresent, bool cacheItem, RlsUserInfo rlsUserInfo)
		{
			return this.ResolveDataSource(entry, dataSourceName, isDataSourcesPresent, cacheItem);
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x00005BF2 File Offset: 0x00003DF2
		public virtual void RebuildInternalDataSource(ProgressiveCacheEntry entry, RlsUserInfo rlsUserInfo)
		{
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x00005BF2 File Offset: 0x00003DF2
		public virtual void ResolveOnPremiseModelConnectionString(DataSourceInfo dsInfo, string connectionString, string identityClaims, IDataProtection dataProtection)
		{
		}

		// Token: 0x06000DE3 RID: 3555
		public abstract DataSourceInfo ResolveDataSource(ProgressiveCacheEntry entry, string dataSourceName, bool isDataSourcesPresent, bool cacheItem);

		// Token: 0x06000DE4 RID: 3556
		public abstract string ResolveModel(DataSourceInfo dsInfo, ProgressiveCacheEntry entry, string modelMetadataVersion, ModelRetrieval modelRetrievalAdditionalInfo);

		// Token: 0x06000DE5 RID: 3557
		public abstract DataSourceInfo LoadDataSourceInfoForItem(ProgressiveCacheEntry entry, string dataSourceName, bool isDataSourcesPresent);

		// Token: 0x06000DE6 RID: 3558
		public abstract void ProcessDataSourceInfoForSecureStoreCredentials(DataSourceInfo dsInfo);

		// Token: 0x06000DE7 RID: 3559 RVA: 0x00032B9C File Offset: 0x00030D9C
		protected string GetModelFromDataExtension(DataSourceInfo dsInfo, ProgressiveCacheEntry entry, string dataSourceName, string modelMetadataVersion, ModelRetrieval modelRetrievalAdditionalInfo)
		{
			IDbConnectionPool dbConnectionPool = ProgressiveExecutionCacheManager.LoadConnectionPool(entry);
			return this.GetModelFromDataExtension(dsInfo, dbConnectionPool, dataSourceName, modelMetadataVersion, modelRetrievalAdditionalInfo);
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x00032BC0 File Offset: 0x00030DC0
		public string GetModelFromDataExtension(DataSourceInfo dsInfo, IDbConnectionPool connectionPool, string dataSourceName, string modelMetadataVersion, ModelRetrieval modelRetrievalAdditionalInfo)
		{
			bool flag = false;
			string text;
			try
			{
				this.m_cancelationHelper.ThrowIfCanceled(ModelRetrievalAbortedException.CancelationTrigger.ModelResolutionBeforeConnectionOpen);
				long elapsedTimeMs = modelRetrievalAdditionalInfo.ElapsedTimeMs;
				ServerDataExtensionConnectionWrapper serverDataExtensionConnectionWrapper = null;
				try
				{
					serverDataExtensionConnectionWrapper = ProgressiveDataExtensionConnection.Open(this.m_service, this.m_itemContext, dsInfo, connectionPool);
					IModelDataExtension modelDataExtension = (IModelDataExtension)serverDataExtensionConnectionWrapper.Connection;
					flag = true;
					modelRetrievalAdditionalInfo.SetConnectionOpenTime(elapsedTimeMs, modelRetrievalAdditionalInfo.ElapsedTimeMs);
					this.m_cancelationHelper.SetCancelationTarget(modelDataExtension);
					this.m_cancelationHelper.ThrowIfCanceled(ModelRetrievalAbortedException.CancelationTrigger.ModelResolutionAfterConnectionOpen);
					long elapsedTimeMs2 = modelRetrievalAdditionalInfo.ElapsedTimeMs;
					string modelMetadata = modelDataExtension.GetModelMetadata(dsInfo.ModelPerspectiveName, modelMetadataVersion);
					modelRetrievalAdditionalInfo.SetModelRetrievalTime(elapsedTimeMs2, modelRetrievalAdditionalInfo.ElapsedTimeMs);
					this.m_cancelationHelper.ResetAndThrowIfCanceled(ModelRetrievalAbortedException.CancelationTrigger.ModelResolutionDuringModelResolution);
					text = modelMetadata;
				}
				finally
				{
					modelRetrievalAdditionalInfo.IsConnectionFromPool = (DataSourceResolverBase.IsConnectionFromPool(serverDataExtensionConnectionWrapper) ? NullableBool.True : NullableBool.False);
					if (serverDataExtensionConnectionWrapper != null)
					{
						serverDataExtensionConnectionWrapper.Dispose();
					}
				}
			}
			catch (RSException ex)
			{
				this.m_cancelationHelper.ResetAndThrowIfCanceled(flag ? ModelRetrievalAbortedException.CancelationTrigger.ModelResolutionDuringModelResolutionRSException : ModelRetrievalAbortedException.CancelationTrigger.ModelResolutionDuringConnectionOpenRSException);
				modelRetrievalAdditionalInfo.Error = string.Concat(new object[]
				{
					typeof(CannotRetrieveModelException).Name,
					"(",
					ex.Code,
					")"
				});
				if (CannotRetrieveModelException.IsCannotRetrieveModelErrorCode(ex.Code))
				{
					throw new CannotRetrieveModelException(ex.Code, dataSourceName, ex);
				}
				throw new CannotRetrieveModelException(dataSourceName, ex);
			}
			catch (Exception ex2)
			{
				this.m_cancelationHelper.ResetAndThrowIfCanceled(flag ? ModelRetrievalAbortedException.CancelationTrigger.ModelResolutionDuringModelResolutionException : ModelRetrievalAbortedException.CancelationTrigger.ModelResolutionDuringConnectionOpenException);
				modelRetrievalAdditionalInfo.Error = typeof(CannotRetrieveModelException).Name;
				throw new CannotRetrieveModelException(dataSourceName, ex2);
			}
			return text;
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x00032D64 File Offset: 0x00030F64
		private static bool IsConnectionFromPool(ServerDataExtensionConnectionWrapper connectionWrapper)
		{
			if (connectionWrapper != null)
			{
				IDbPoolableConnection dbPoolableConnection = connectionWrapper.Connection as IDbPoolableConnection;
				if (dbPoolableConnection != null)
				{
					return dbPoolableConnection.IsFromPool;
				}
			}
			return false;
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x00032D8B File Offset: 0x00030F8B
		public void CancelModelRetrieval()
		{
			this.m_cancelationHelper.Cancel();
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x00032D98 File Offset: 0x00030F98
		public virtual bool TryResolveInternalDataSource(ProgressiveCacheEntry entry, out DataSourceInfo dsInfo)
		{
			dsInfo = null;
			string text;
			if (this.TryGetInternalDataSourceName(out text))
			{
				dsInfo = this.LoadDataSourceInfoForItem(entry, text, true);
				RSTrace.CatalogTrace.Assert(dsInfo != null, "Failed to load DataSourceInfo for item");
				dsInfo.ThrowIfNotUsable(new ServerDataSourceSettings(Globals.Configuration.IsSurrogatePresent, Global.EnableIntegratedSecurity));
				return true;
			}
			return false;
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x00032DEF File Offset: 0x00030FEF
		private bool TryGetInternalDataSourceName(out string dataSourceName)
		{
			dataSourceName = null;
			if (Microsoft.ReportingServices.Diagnostics.ProcessingContext.ReqContext.HasInternalDataSource)
			{
				dataSourceName = Microsoft.ReportingServices.Diagnostics.ProcessingContext.ReqContext.InternalDataSource.DataSourceName;
				return true;
			}
			return false;
		}

		// Token: 0x040005B7 RID: 1463
		protected readonly RSService m_service;

		// Token: 0x040005B8 RID: 1464
		protected readonly CatalogItemContext m_itemContext;

		// Token: 0x040005B9 RID: 1465
		protected readonly string m_itemPath;

		// Token: 0x040005BA RID: 1466
		private DataSourceResolverCancelationHelper<IModelDataExtension> m_cancelationHelper;
	}
}
