using System;
using System.Data.Common;
using System.IO;
using System.Security;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Internal;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000178 RID: 376
	internal class DataSourceResolver : DataSourceResolverBase
	{
		// Token: 0x06000DC8 RID: 3528 RVA: 0x000325B4 File Offset: 0x000307B4
		public DataSourceResolver(string itemPath, CatalogItemContext itemContext, RSService service)
			: base(service, itemContext, itemPath)
		{
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06000DC9 RID: 3529 RVA: 0x000325C0 File Offset: 0x000307C0
		private string ItemPathExtension
		{
			get
			{
				if (this.m_itemPathExtension == null)
				{
					this.m_itemPathExtension = Path.GetExtension(this.m_itemPath);
					if (this.m_itemPathExtension == null)
					{
						throw new InvalidParameterException("Item");
					}
					this.m_itemPathExtension = this.m_itemPathExtension.ToUpperInvariant();
				}
				return this.m_itemPathExtension;
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06000DCA RID: 3530 RVA: 0x00032610 File Offset: 0x00030810
		private bool ItemPathExists
		{
			get
			{
				return !this.m_itemPath.Equals("/", StringComparison.Ordinal);
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06000DCB RID: 3531 RVA: 0x00032626 File Offset: 0x00030826
		private bool IsItemPathRdlx
		{
			get
			{
				return this.ItemPathExists && ".RDLX".Equals(this.ItemPathExtension, StringComparison.OrdinalIgnoreCase);
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06000DCC RID: 3532 RVA: 0x00032643 File Offset: 0x00030843
		private bool IsItemPathDataSource
		{
			get
			{
				return this.ItemPathExists && (string.Equals(".RSDS", this.ItemPathExtension, StringComparison.OrdinalIgnoreCase) || this.IsNonCatalogTypeDataSourceFileExtension);
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06000DCD RID: 3533 RVA: 0x0003266C File Offset: 0x0003086C
		private bool IsNonCatalogTypeDataSourceFileExtension
		{
			get
			{
				return string.Equals(".BISM", this.ItemPathExtension, StringComparison.OrdinalIgnoreCase) || string.Equals(".XLSX", this.ItemPathExtension, StringComparison.OrdinalIgnoreCase) || string.Equals(".XLSM", this.ItemPathExtension, StringComparison.OrdinalIgnoreCase) || string.Equals(".XLSB", this.ItemPathExtension, StringComparison.OrdinalIgnoreCase);
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06000DCE RID: 3534 RVA: 0x000326C5 File Offset: 0x000308C5
		private bool IsXLSXFileExtension
		{
			get
			{
				return string.Equals(".XLSX", this.ItemPathExtension, StringComparison.OrdinalIgnoreCase);
			}
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x000326D8 File Offset: 0x000308D8
		public override DataSourceInfo ResolveDataSource(ProgressiveCacheEntry entry, string dataSourceName, bool isDataSourcesPresent, bool cacheItem)
		{
			DataSourceInfo dataSourceInfo2;
			using (MonitoredScope.NewFormat("DataSourceResolver.ResolveDataSource[DataSource={0}]", dataSourceName))
			{
				bool flag;
				DataSourceInfo dataSourceInfo = this.LoadDataSourceInfoForItem(entry, dataSourceName, isDataSourcesPresent, out flag);
				RSTrace.CatalogTrace.Assert(dataSourceInfo != null, "Failed to load DataSourceInfo for item");
				dataSourceInfo.ThrowIfNotUsable(new ServerDataSourceSettings(Globals.Configuration.IsSurrogatePresent, Global.EnableIntegratedSecurity));
				this.ProcessDataSourceInfoForSecureStoreCredentials(dataSourceInfo);
				if (cacheItem && !flag)
				{
					if (this.IsItemPathDataSource && entry.IsPowerView)
					{
						entry.AddDataSourceInfo(this.m_itemPath, this.m_itemContext.ItemName, dataSourceInfo);
					}
					else
					{
						entry.AddDataSourceInfo(this.m_itemPath, dataSourceName, dataSourceInfo);
					}
				}
				dataSourceInfo2 = dataSourceInfo;
			}
			return dataSourceInfo2;
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x00032790 File Offset: 0x00030990
		public override string ResolveModel(DataSourceInfo dsInfo, ProgressiveCacheEntry entry, string modelMetadataVersion, ModelRetrieval modelRetrievalAdditionalInfo)
		{
			string text = (this.ItemPathExists ? this.m_itemPath : dsInfo.Name);
			string modelFromDataExtension;
			using (MonitoredScope.NewFormat("DataSourceResolver.ResolveModel[Item={0}]", text))
			{
				modelFromDataExtension = base.GetModelFromDataExtension(dsInfo, entry, text, modelMetadataVersion, modelRetrievalAdditionalInfo);
			}
			return modelFromDataExtension;
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x000327EC File Offset: 0x000309EC
		public void ProcessCachedDataSourceInfoForSecureStoreCredentials(ProgressiveCacheEntry cacheEntry)
		{
			foreach (object obj in cacheEntry.DataSources)
			{
				DataSourceInfo dataSourceInfo = (DataSourceInfo)obj;
				this.ProcessDataSourceInfoForSecureStoreCredentials(dataSourceInfo);
			}
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x00032848 File Offset: 0x00030A48
		public override void ProcessDataSourceInfoForSecureStoreCredentials(DataSourceInfo dsInfo)
		{
			if (dsInfo.CredentialsRetrieval == DataSourceInfo.CredentialsRetrievalOption.SecureStore && !dsInfo.IsCredentialSet)
			{
				try
				{
					this.FillInWindowsCredentialFromSecureStore(dsInfo);
					dsInfo.IsCredentialSet = true;
				}
				catch (ReportCatalogException ex)
				{
					dsInfo.SetDataSourceFaultContext(ex.Code, ex.Message);
				}
			}
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x0003289C File Offset: 0x00030A9C
		private void FillInWindowsCredentialFromSecureStore(DataSourceInfo dsInfo)
		{
			string contextUrl = ProcessingContext.ReqContext.ContextUrl;
			if (contextUrl == null)
			{
				throw new SecureStoreContextUrlNotSpecifiedException();
			}
			SecureString secureString;
			SecureString secureString2;
			this.m_service.ServiceHelper.GetWindowsCredentialFromSecureStore(dsInfo.SecureStoreLookup.LookupContext == SecureStoreLookup.LookupContextOptions.Unattended, this.m_service.UserContext, contextUrl, dsInfo.SecureStoreLookup.TargetApplicationId, out secureString, out secureString2);
			string text = new SecureStringWrapper(secureString).ToString();
			dsInfo.SetUserName(text, DataProtection.Instance);
			dsInfo.SetPassword(secureString2, DataProtection.Instance);
			dsInfo.WindowsCredentials = true;
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x00032924 File Offset: 0x00030B24
		public override DataSourceInfo LoadDataSourceInfoForItem(ProgressiveCacheEntry entry, string dataSourceName, bool isDataSourcesPresent)
		{
			bool flag;
			return this.LoadDataSourceInfoForItem(entry, dataSourceName, isDataSourcesPresent, out flag);
		}

		// Token: 0x06000DD5 RID: 3541 RVA: 0x0003293C File Offset: 0x00030B3C
		private DataSourceInfo LoadDataSourceInfoForItem(ProgressiveCacheEntry entry, string dataSourceName, bool isDataSourcesPresent, out bool isCachedDataSource)
		{
			isCachedDataSource = false;
			if (!this.ItemPathExists)
			{
				if (entry.DataSources == null)
				{
					throw new MissingParameterException("ItemPath");
				}
				if (string.IsNullOrEmpty(dataSourceName))
				{
					throw new MissingParameterException("DataSourceName");
				}
			}
			if (this.IsItemPathDataSource)
			{
				if (entry.IsPowerView)
				{
					if (!this.IsXLSXFileExtension)
					{
						dataSourceName = this.m_itemContext.ItemName;
					}
				}
				else
				{
					dataSourceName = string.Empty;
				}
			}
			else if ((this.IsItemPathRdlx || isDataSourcesPresent) && string.IsNullOrEmpty(dataSourceName))
			{
				throw new MissingParameterException("DataSourceName");
			}
			if (!string.IsNullOrEmpty(dataSourceName))
			{
				string dataSourceInfoKey = entry.GetDataSourceInfoKey(this.m_itemPath, dataSourceName);
				DataSourceInfo dataSourceInfo = entry.LoadDataSourceInfoFromCache(dataSourceInfoKey);
				if (dataSourceInfo != null)
				{
					isCachedDataSource = true;
					return dataSourceInfo;
				}
				if (isDataSourcesPresent)
				{
					throw new DataSourceNotFoundException(dataSourceName);
				}
				if (this.IsItemPathRdlx)
				{
					return this.ReadDataSourceInfoFromRdlx(dataSourceName);
				}
			}
			return this.LoadDataSourceInfoForItem();
		}

		// Token: 0x06000DD6 RID: 3542 RVA: 0x00032A10 File Offset: 0x00030C10
		internal virtual DataSourceInfo ReadDataSourceInfoFromRdlx(string dataSourceName)
		{
			return this.m_service.ExecuteNestedTransaction<DataSourceInfo>(delegate(RSService newService)
			{
				FullReportCatalogItem fullReportCatalogItem = (FullReportCatalogItem)newService.CatalogItemFactory.GetCatalogItem(this.m_itemContext, ItemType.RdlxReport, true);
				fullReportCatalogItem.ThrowIfNoAccess(ReportOperation.ExecuteAndView);
				DataSourceInfo byOriginalName = fullReportCatalogItem.DataSources.GetByOriginalName(dataSourceName);
				if (byOriginalName != null)
				{
					return byOriginalName;
				}
				throw new DataSourceNotFoundException(dataSourceName);
			});
		}

		// Token: 0x06000DD7 RID: 3543 RVA: 0x00032A48 File Offset: 0x00030C48
		internal virtual DataSourceInfo LoadDataSourceInfoForItem()
		{
			DataSourceInfo dataSourceInfo;
			if (this.IsNonCatalogTypeDataSourceFileExtension)
			{
				dataSourceInfo = this.CreateDataSourceInfoForFile();
			}
			else
			{
				dataSourceInfo = this.GetDataSourceInfoFromCatalog();
			}
			return dataSourceInfo;
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x00032A70 File Offset: 0x00030C70
		private DataSourceInfo CreateDataSourceInfoForFile()
		{
			DbConnectionStringBuilder dbConnectionStringBuilder = new DbConnectionStringBuilder(false);
			dbConnectionStringBuilder.Add("Data Source", this.m_itemContext.ItemPathAsString);
			return new DataSourceInfo("TemporaryDataSource", Global.DaxDataExtensionName, dbConnectionStringBuilder.ConnectionString, true, null, DataProtection.Instance);
		}

		// Token: 0x06000DD9 RID: 3545 RVA: 0x00032AB6 File Offset: 0x00030CB6
		private DataSourceInfo GetDataSourceInfoFromCatalog()
		{
			return this.m_service.ExecuteNestedTransaction<DataSourceInfo>(delegate(RSService newService)
			{
				CatalogItem catalogItem = newService.CatalogItemFactory.GetCatalogItem(this.m_itemContext, true);
				catalogItem.ThrowIfWrongItemType(new ItemType[] { ItemType.DataSource });
				catalogItem.ThrowIfNoAccess(CommonOperation.ReadProperties);
				DataSourceCatalogItem dataSourceCatalogItem = (DataSourceCatalogItem)catalogItem;
				DataSourceInfo dataSourceInfo = dataSourceCatalogItem.DataSourceInfo;
				dataSourceCatalogItem.ThrowIfNotGoodForRdlx(this.m_itemPath);
				return dataSourceInfo;
			});
		}

		// Token: 0x040005B1 RID: 1457
		private string m_itemPathExtension;

		// Token: 0x040005B2 RID: 1458
		private const string BISMFileExtension = ".BISM";

		// Token: 0x040005B3 RID: 1459
		private const string XLSXFileExtension = ".XLSX";

		// Token: 0x040005B4 RID: 1460
		private const string XLSMFileExtension = ".XLSM";

		// Token: 0x040005B5 RID: 1461
		private const string XLSBFileExtension = ".XLSB";
	}
}
