using System;
using System.Security;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000A0 RID: 160
	internal class RSServiceHelper
	{
		// Token: 0x06000774 RID: 1908 RVA: 0x0001F756 File Offset: 0x0001D956
		public RSServiceHelper()
		{
			this.m_service = null;
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x0001F765 File Offset: 0x0001D965
		public RSServiceHelper(RSService s)
		{
			this.m_service = s;
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x0001F774 File Offset: 0x0001D974
		internal virtual void EnsureAllowedToEditproperties(ItemType itemType, byte[] secDesc, ExternalItemPath path)
		{
			switch (itemType)
			{
			case ItemType.Folder:
				if (!this.m_service.SecMgr.CheckAccess(itemType, secDesc, FolderOperation.UpdateProperties, path))
				{
					throw new AccessDeniedException(this.UserName, ErrorCode.rsAccessDenied);
				}
				break;
			case ItemType.Report:
			case ItemType.LinkedReport:
			case ItemType.DataSet:
			case ItemType.PowerBIReport:
			case ItemType.ExcelWorkbook:
				if (!this.m_service.SecMgr.CheckAccess(itemType, secDesc, ReportOperation.UpdateProperties, path))
				{
					throw new AccessDeniedException(this.UserName, ErrorCode.rsAccessDenied);
				}
				break;
			case ItemType.Resource:
			case ItemType.Component:
			case ItemType.Kpi:
				if (!this.m_service.SecMgr.CheckAccess(itemType, secDesc, ResourceOperation.UpdateProperties, path))
				{
					throw new AccessDeniedException(this.UserName, ErrorCode.rsAccessDenied);
				}
				break;
			case ItemType.DataSource:
				if (!this.m_service.SecMgr.CheckAccess(itemType, secDesc, DatasourceOperation.UpdateProperties, path))
				{
					throw new AccessDeniedException(this.UserName, ErrorCode.rsAccessDenied);
				}
				break;
			case ItemType.Model:
				if (!this.m_service.SecMgr.CheckAccess(itemType, secDesc, ModelOperation.UpdateProperties, path))
				{
					throw new AccessDeniedException(this.UserName, ErrorCode.rsAccessDenied);
				}
				break;
			case ItemType.Site:
			case ItemType.RdlxReport:
			case ItemType.MobileReport:
				break;
			default:
				return;
			}
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x0001F87C File Offset: 0x0001DA7C
		internal virtual void EnsureAllowedAsSubitem(ItemType parentType, ItemType childType, byte[] secDesc, ExternalItemPath parent, string item)
		{
			if (parentType == ItemType.Folder)
			{
				switch (childType)
				{
				case ItemType.Folder:
					if (!this.m_service.SecMgr.CheckAccess(ItemType.Folder, secDesc, FolderOperation.CreateFolder, parent))
					{
						throw new AccessDeniedException(this.UserName, ErrorCode.rsAccessDenied);
					}
					break;
				case ItemType.Report:
				case ItemType.LinkedReport:
				case ItemType.DataSet:
					if (!this.m_service.SecMgr.CheckAccess(ItemType.Folder, secDesc, FolderOperation.CreateReport, parent))
					{
						throw new AccessDeniedException(this.UserName, ErrorCode.rsAccessDenied);
					}
					break;
				case ItemType.Resource:
				case ItemType.Component:
				case ItemType.Kpi:
				case ItemType.PowerBIReport:
				case ItemType.ExcelWorkbook:
					if (!this.m_service.SecMgr.CheckAccess(ItemType.Folder, secDesc, FolderOperation.CreateResource, parent))
					{
						throw new AccessDeniedException(this.UserName, ErrorCode.rsAccessDenied);
					}
					break;
				case ItemType.DataSource:
					if (!this.m_service.SecMgr.CheckAccess(ItemType.Folder, secDesc, FolderOperation.CreateDatasource, parent))
					{
						throw new AccessDeniedException(this.UserName, ErrorCode.rsAccessDenied);
					}
					break;
				case ItemType.Model:
					if (!this.m_service.SecMgr.CheckAccess(ItemType.Folder, secDesc, FolderOperation.CreateModel, parent))
					{
						throw new AccessDeniedException(this.UserName, ErrorCode.rsAccessDenied);
					}
					break;
				case ItemType.Site:
				case ItemType.RdlxReport:
				case ItemType.MobileReport:
					break;
				default:
					return;
				}
				return;
			}
			if (parentType <= ItemType.LinkedReport)
			{
				if (parentType != ItemType.Report)
				{
					if (parentType == ItemType.LinkedReport)
					{
						if (!this.m_service.SecMgr.CheckAccess(ItemType.LinkedReport, secDesc, ReportOperation.Comment, parent))
						{
							throw new AccessDeniedException(this.UserName, ErrorCode.rsAccessDenied);
						}
						return;
					}
				}
				else
				{
					if (!this.m_service.SecMgr.CheckAccess(ItemType.Report, secDesc, ReportOperation.Comment, parent))
					{
						throw new AccessDeniedException(this.UserName, ErrorCode.rsAccessDenied);
					}
					return;
				}
			}
			else if (parentType != ItemType.PowerBIReport)
			{
				if (parentType == ItemType.ExcelWorkbook)
				{
					if (!this.m_service.SecMgr.CheckAccess(ItemType.ExcelWorkbook, secDesc, ReportOperation.Comment, parent))
					{
						throw new AccessDeniedException(this.UserName, ErrorCode.rsAccessDenied);
					}
					return;
				}
			}
			else
			{
				if (!this.m_service.SecMgr.CheckAccess(ItemType.PowerBIReport, secDesc, ReportOperation.Comment, parent))
				{
					throw new AccessDeniedException(this.UserName, ErrorCode.rsAccessDenied);
				}
				return;
			}
			throw new WrongItemTypeException(parent.Value);
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x0001FA59 File Offset: 0x0001DC59
		internal virtual DBInterface GetStorageInternal()
		{
			return new DBInterface(this.Service.UserContext);
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x0001FA6B File Offset: 0x0001DC6B
		internal virtual ReportExecutionCacheDb GetExecutionCacheDbInternal()
		{
			return new ReportExecutionCacheDb(this.Service);
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x0001FA78 File Offset: 0x0001DC78
		internal virtual Security GetSecurityManager(UserContext userContext, bool m_checkAccess)
		{
			return new Security(userContext, m_checkAccess);
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x00005BEF File Offset: 0x00003DEF
		internal virtual bool SyncToRSCatalog(ExternalItemPath context, bool createOnly)
		{
			return false;
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x0001FA84 File Offset: 0x0001DC84
		internal bool SyncToRSCatalog(ExternalItemPath path)
		{
			bool flag;
			using (MonitoredScope.New("RSServerHelper.SyncToRSCatalog"))
			{
				flag = this.SyncToRSCatalog(path, false);
			}
			return flag;
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x0001FAC4 File Offset: 0x0001DCC4
		internal virtual DateTime PushDataSourceStateChange(DataSourceCatalogItem dsItem, bool enabled)
		{
			return DateTime.MinValue;
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x00005BF2 File Offset: 0x00003DF2
		internal virtual void AbortCreation(CatalogItem item)
		{
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x00005BF2 File Offset: 0x00003DF2
		internal virtual void AbortUpdate(CatalogItem item, byte[] originalContent)
		{
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x00005BF2 File Offset: 0x00003DF2
		internal virtual void EnsureSharePointServicesAccessible()
		{
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x00005BF2 File Offset: 0x00003DF2
		internal virtual void PopulateAdditionalToken(RSService service, string itemPath)
		{
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x0001FACB File Offset: 0x0001DCCB
		internal virtual Guid? GetSPSiteSubscriptionId(string wssUrl)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x00005BF2 File Offset: 0x00003DF2
		internal virtual void CheckItemType(ItemType expectedType, ExternalItemPath itemPath)
		{
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x00005BF2 File Offset: 0x00003DF2
		internal virtual void EnsureSecurityZone(string itemPath)
		{
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x00005C88 File Offset: 0x00003E88
		internal virtual string GetReportServerUrl(ExternalItemPath path)
		{
			return null;
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x00005C88 File Offset: 0x00003E88
		internal virtual ExternalItemPath GetSiteUrl(ExternalItemPath url, UserContext userContext)
		{
			return null;
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0001FAD2 File Offset: 0x0001DCD2
		internal virtual string GetPublicUrl(string url, bool noThrow)
		{
			return url;
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x0001FAD5 File Offset: 0x0001DCD5
		internal virtual bool GetExternalResourceFromSite(string resourceUrl, UserContext userContext, out byte[] contents, out string mimeType)
		{
			contents = null;
			mimeType = null;
			return false;
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x0001FACB File Offset: 0x0001DCCB
		internal virtual void GetWindowsCredentialFromSecureStore(bool isUnattended, UserContext userContext, string contextUrl, string appId, out SecureString userName, out SecureString password)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x0001FADF File Offset: 0x0001DCDF
		internal virtual string TranslateViewerUrl(Uri requestUrl)
		{
			return requestUrl.AbsoluteUri;
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x00005BF2 File Offset: 0x00003DF2
		internal virtual void EnsureSourceAndTargetOnSameSite(ExternalItemPath sourceUrl, ExternalItemPath targetUrl, UserContext userContext)
		{
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0001FAE7 File Offset: 0x0001DCE7
		internal virtual string UserEmail(ExternalItemPath path, string userName)
		{
			return this.Service.UserName;
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0001FAF4 File Offset: 0x0001DCF4
		internal virtual void RunElevated(RSServiceHelper.CodeToRunElevated code)
		{
			code();
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0001FAFC File Offset: 0x0001DCFC
		internal virtual MoveItemAction GetMoveItemActionInternal()
		{
			return new MoveItemAction(this.Service);
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x0001FB09 File Offset: 0x0001DD09
		internal virtual ListModelPerspectivesAction GetListModelPerspectivesActionInternal()
		{
			return new ListModelPerspectivesAction(this.Service);
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000790 RID: 1936 RVA: 0x0001FB16 File Offset: 0x0001DD16
		// (set) Token: 0x06000791 RID: 1937 RVA: 0x0001FB1E File Offset: 0x0001DD1E
		internal RSService Service
		{
			get
			{
				return this.m_service;
			}
			set
			{
				this.m_service = value;
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000792 RID: 1938 RVA: 0x0001FB27 File Offset: 0x0001DD27
		protected string UserName
		{
			get
			{
				return this.m_service.UserName;
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000793 RID: 1939 RVA: 0x0001FB34 File Offset: 0x0001DD34
		protected UserContext UserContext
		{
			get
			{
				return this.m_service.UserContext;
			}
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x0001FB41 File Offset: 0x0001DD41
		internal virtual RSPropertyProvider GetPropertyProviderInternal()
		{
			return new RSPropertyProvider();
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x00005BF2 File Offset: 0x00003DF2
		internal virtual void SetExternalRoot(string path)
		{
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x00005BF2 File Offset: 0x00003DF2
		internal virtual void SetExternalRoot(CatalogItemPath path, int zone)
		{
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x00005C88 File Offset: 0x00003E88
		internal virtual Uri GetExternalRoot()
		{
			return null;
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x00005BEF File Offset: 0x00003DEF
		internal virtual int GetExternalRootZone(ExternalItemPath path)
		{
			return 0;
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x0001FAD2 File Offset: 0x0001DCD2
		internal virtual string CatalogToExternal(string source, bool noThrow)
		{
			return source;
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x0001FAD2 File Offset: 0x0001DCD2
		internal virtual string CatalogToExternal(string source, int zone, bool noThrow)
		{
			return source;
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x0001FAD2 File Offset: 0x0001DCD2
		internal virtual string ExternalToCatalog(string source, bool noThrow)
		{
			return source;
		}

		// Token: 0x040003F4 RID: 1012
		private RSService m_service;

		// Token: 0x02000462 RID: 1122
		// (Invoke) Token: 0x0600234E RID: 9038
		internal delegate void CodeToRunElevated();
	}
}
