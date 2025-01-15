using System;
using System.Globalization;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000DA RID: 218
	[CatalogItemType(ItemType.DataSet)]
	internal class DataSetCatalogItem : BaseExecutableCatalogItem
	{
		// Token: 0x06000993 RID: 2451 RVA: 0x000229F9 File Offset: 0x00020BF9
		internal DataSetCatalogItem(RSService service)
			: base(service)
		{
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000994 RID: 2452 RVA: 0x00004FDD File Offset: 0x000031DD
		// (set) Token: 0x06000995 RID: 2453 RVA: 0x00004FE5 File Offset: 0x000031E5
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

		// Token: 0x17000316 RID: 790
		// (set) Token: 0x06000996 RID: 2454 RVA: 0x00024FD9 File Offset: 0x000231D9
		internal ServerSnapshot CompiledDefinition
		{
			set
			{
				this.m_compiledDefinition = value;
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000997 RID: 2455 RVA: 0x00025AD5 File Offset: 0x00023CD5
		// (set) Token: 0x06000998 RID: 2456 RVA: 0x000246E4 File Offset: 0x000228E4
		internal override DataSourceInfoCollection DataSources
		{
			get
			{
				if (this.m_dataSources == null)
				{
					this.m_dataSources = base.GetSyncedItemDataSources(this.m_itemID);
					RSTrace.CatalogTrace.Assert(this.m_dataSources.Count <= 1);
				}
				return this.m_dataSources;
			}
			set
			{
				this.m_dataSources = value;
			}
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x00025B12 File Offset: 0x00023D12
		protected override void ContentLoadSecurityCheck()
		{
			if (base.InternalUsePermissionForExecution)
			{
				base.ThrowIfNoAccess(ReportOperation.ExecuteAndView);
				return;
			}
			base.ThrowIfNoAccess(ReportOperation.ReadReportDefinition);
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x00025B2B File Offset: 0x00023D2B
		internal override void LoadStoredAndDerivedProperties()
		{
			base.LoadProperties();
			this.DeriveProperties();
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x00025B3C File Offset: 0x00023D3C
		protected void DeriveProperties()
		{
			base.Properties.FixupIfConnectionStringUserDependency(this.DataSources);
			base.Properties.HasUserProfileDependencies = base.Properties.QueryDependsOnUser.ToString(CultureInfo.InvariantCulture);
			base.Properties.HasDataSourceCredentials = DataSourceCatalogItem.GoodForUnattendedExecution(this.DataSources.GetTheOnlyDataSource()).ToString(CultureInfo.InvariantCulture);
			base.ItemMetadata.HasParameters = base.Parameters.Count > 0;
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x000247EC File Offset: 0x000229EC
		internal void SaveDataSources()
		{
			base.Service.Storage.DeleteDataSources(base.ItemID);
			base.Service.AddDataSources(base.ItemID, this.DataSources, base.ItemContext.ItemPath.EditSessionID);
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x00025BBF File Offset: 0x00023DBF
		internal void SaveDefinition()
		{
			base.Service.Storage.SetObjectContent(base.ItemContext.CatalogItemPath, ItemType.DataSet, this.m_content, this.m_compiledDefinition.SnapshotDataID, base.ParametersXml, Guid.Empty, null);
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x00025BFA File Offset: 0x00023DFA
		protected override void Update()
		{
			this.SaveDefinition();
			base.SaveProperties();
			this.SaveDataSources();
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x00025C0E File Offset: 0x00023E0E
		protected override void FinalizeCreation()
		{
			base.Service.AddDataSources(base.ItemID, this.DataSources, base.ItemContext.ItemPath.EditSessionID);
		}
	}
}
