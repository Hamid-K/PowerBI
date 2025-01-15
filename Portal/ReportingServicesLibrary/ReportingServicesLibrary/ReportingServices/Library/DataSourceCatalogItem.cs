using System;
using System.Globalization;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000CF RID: 207
	[CatalogItemType(ItemType.DataSource)]
	internal class DataSourceCatalogItem : CatalogItem
	{
		// Token: 0x06000901 RID: 2305 RVA: 0x00004F8E File Offset: 0x0000318E
		internal DataSourceCatalogItem(RSService service)
			: base(service)
		{
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x00023E80 File Offset: 0x00022080
		internal override void LoadStoredAndDerivedProperties()
		{
			base.LoadProperties();
			DataSourceInfo theOnlyDataSource = base.Service.Storage.GetDataSources(base.ItemID).GetTheOnlyDataSource();
			bool flag = theOnlyDataSource.GoodForLiveExecution(Globals.Configuration.IsSurrogatePresent) && Globals.Configuration.Extensions.ModelGeneration[theOnlyDataSource.Extension] != null;
			base.Properties.CanGenerateModel = flag.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x00023EF9 File Offset: 0x000220F9
		internal void ThrowIfNoAccess(DatasourceOperation operation)
		{
			if (!base.Service.SecMgr.CheckAccess(base.ThisItemType, base.SecurityDescriptor, operation, base.ItemContext.ItemPath))
			{
				throw new AccessDeniedException(base.Service.UserName, ErrorCode.rsAccessDenied);
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000904 RID: 2308 RVA: 0x00023F38 File Offset: 0x00022138
		// (set) Token: 0x06000905 RID: 2309 RVA: 0x00023F40 File Offset: 0x00022140
		internal bool Enabled
		{
			get
			{
				return this.m_enabled;
			}
			set
			{
				this.m_enabled = value;
				this.m_enabledRetrieved = true;
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000906 RID: 2310 RVA: 0x00023F50 File Offset: 0x00022150
		// (set) Token: 0x06000907 RID: 2311 RVA: 0x00023F59 File Offset: 0x00022159
		internal DataSourceInfo DataSourceInfo
		{
			get
			{
				return this.GetDataSourceInfo(true);
			}
			set
			{
				this.m_dataSourceInfo = value;
			}
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x00023F64 File Offset: 0x00022164
		internal DataSourceInfo GetDataSourceInfo(bool throwIfNotExist)
		{
			if (this.m_dataSourceInfo == null)
			{
				DataSourceInfoCollection dataSources = base.Service.Storage.GetDataSources(base.ItemID);
				if (!throwIfNotExist && dataSources.Count == 0)
				{
					return null;
				}
				this.m_dataSourceInfo = dataSources.GetTheOnlyDataSource();
				this.m_dataSourceInfo.Name = base.ItemContext.ItemName;
			}
			return this.m_dataSourceInfo;
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000909 RID: 2313 RVA: 0x00004FDD File Offset: 0x000031DD
		// (set) Token: 0x0600090A RID: 2314 RVA: 0x00004FE5 File Offset: 0x000031E5
		internal override byte[] Content
		{
			get
			{
				return this.m_content;
			}
			set
			{
				this.m_content = value;
			}
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x00023FC5 File Offset: 0x000221C5
		protected override void ContentLoadSecurityCheck()
		{
			this.ThrowIfNoAccess(DatasourceOperation.ReadContent);
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x00023FCE File Offset: 0x000221CE
		protected override void Update()
		{
			base.Service.Storage.DeleteDataSources(this.m_itemID);
			this.FinalizeCreation();
			base.AdjustModificationInfo();
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x00023FF4 File Offset: 0x000221F4
		protected override void FinalizeCreation()
		{
			Guid guid;
			ItemType itemType;
			byte[] array;
			this.SaveDataSourceInfo(out guid, out itemType, out array);
			Global.m_Tracer.Assert(guid == Guid.Empty, "CreateDataSource - link returned");
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x00024028 File Offset: 0x00022228
		internal void SaveEnabled()
		{
			if (!this.m_enabledRetrieved)
			{
				throw new InternalCatalogException("DataSourceCatalogItem is not initialized in SaveEnabled()");
			}
			DateTime dateTime = base.Service.ServiceHelper.PushDataSourceStateChange(this, this.Enabled);
			base.Service.Storage.ChangeStateOfDataSource(base.ItemID, this.Enabled);
			if (dateTime != DateTime.MinValue)
			{
				base.Service.Storage.SetLastModified(base.ItemContext.CatalogItemPath, base.Service.UserName, dateTime);
			}
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x000240B0 File Offset: 0x000222B0
		private void SaveDataSourceInfo(out Guid linkID, out ItemType linkType, out byte[] linkSecDesc)
		{
			Global.m_Tracer.Assert(this.m_dataSourceInfo != null, "DataSourceInfo is null when trying to save it");
			base.Service.Storage.AddDataSource(this.m_itemID, Guid.Empty, this.m_dataSourceInfo, base.Service, null, out linkID, out linkType, out linkSecDesc);
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x00024100 File Offset: 0x00022300
		internal void ThrowIfNotGoodForRdlx(string nameOrPath)
		{
			DataSourceInfo dataSourceInfo = this.DataSourceInfo;
			Global.m_Tracer.Assert(dataSourceInfo != null, "DataSourceInfo is null in ThrowIfNotGoodForRdlx.");
			DataSourceCatalogItem.ThrowIfNotGoodForRdlx(dataSourceInfo, nameOrPath);
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x0002412E File Offset: 0x0002232E
		internal static void ThrowIfNotGoodForRdlx(DataSourceInfo info, string nameOrPath)
		{
			DataSourceUtility.ThrowIfNotGoodForRdlx(info, nameOrPath);
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x00024137 File Offset: 0x00022337
		public static bool GoodForUnattendedExecution(DataSourceInfo dataSource)
		{
			return DataSourceUtility.GoodForUnattendedExecution(dataSource);
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x0002413F File Offset: 0x0002233F
		public static bool GoodForExecutionUnderServiceAccount(DataSourceInfo dataSource)
		{
			return DataSourceUtility.GoodForExecutionUnderServiceAccount(dataSource);
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x00024147 File Offset: 0x00022347
		public static bool GoodForUnattendedSurrogateExecution(DataSourceInfo dataSource)
		{
			return DataSourceUtility.GoodForUnattendedSurrogateExecution(dataSource);
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x0002414F File Offset: 0x0002234F
		public static bool GoodForUnattendedExecution(RuntimeDataSourceInfoCollection dataSources)
		{
			return DataSourceUtility.GoodForUnattendedExecution(dataSources);
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x00024157 File Offset: 0x00022357
		public static bool ExtensionImplementsInterface(DataSourceInfo dataSource, Type interfaceType)
		{
			return DataSourceUtility.ExtensionImplementsInterface(dataSource, interfaceType);
		}

		// Token: 0x04000453 RID: 1107
		private bool m_enabled;

		// Token: 0x04000454 RID: 1108
		private bool m_enabledRetrieved;

		// Token: 0x04000455 RID: 1109
		private DataSourceInfo m_dataSourceInfo;
	}
}
