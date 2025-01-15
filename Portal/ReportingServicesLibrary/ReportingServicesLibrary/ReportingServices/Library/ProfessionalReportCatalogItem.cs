using System;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000D6 RID: 214
	[CatalogItemType(ItemType.Report)]
	internal class ProfessionalReportCatalogItem : FullReportCatalogItem, IEditSessionAware
	{
		// Token: 0x0600097B RID: 2427 RVA: 0x000258C8 File Offset: 0x00023AC8
		internal ProfessionalReportCatalogItem(RSService service)
			: base(service)
		{
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x000258D1 File Offset: 0x00023AD1
		internal override void LoadStoredAndDerivedProperties()
		{
			base.LoadProperties();
			base.DeriveProperties();
		}

		// Token: 0x1700030F RID: 783
		// (set) Token: 0x0600097D RID: 2429 RVA: 0x00024FD9 File Offset: 0x000231D9
		internal new ServerSnapshot CompiledDefinition
		{
			set
			{
				this.m_compiledDefinition = value;
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x0600097E RID: 2430 RVA: 0x000246ED File Offset: 0x000228ED
		// (set) Token: 0x0600097F RID: 2431 RVA: 0x0002470F File Offset: 0x0002290F
		internal override DataSetInfoCollection SharedDataSets
		{
			get
			{
				if (this.m_sharedDataSets == null)
				{
					this.m_sharedDataSets = base.GetSyncedItemSharedDataSets(this.m_itemID);
				}
				return this.m_sharedDataSets;
			}
			set
			{
				this.m_sharedDataSets = value;
			}
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x000258E0 File Offset: 0x00023AE0
		internal override void ThrowIfRdceNotSupported()
		{
			if (Microsoft.ReportingServices.Library.ExecutionOptions.IsSnapshotExecution(base.ExecuteOption) || Microsoft.ReportingServices.Library.ExecutionOptions.IsHistoryOnSchedule(base.ExecuteOption) || Microsoft.ReportingServices.Library.ExecutionOptions.ExecutionSnapshotsKept(base.ExecuteOption))
			{
				throw new RdceInvalidExecutionOptionException();
			}
			bool flag;
			ExpirationDefinition expirationDefinition;
			base.Service.ExecCacheDb.GetCacheOptions(this.m_itemContext.CatalogItemPath, out flag, out expirationDefinition);
			if (flag)
			{
				throw new RdceInvalidCacheOptionException();
			}
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x00025942 File Offset: 0x00023B42
		internal override void FlushDataCache()
		{
			base.Service.ExecCacheDb.FlushCacheById(base.ItemID);
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x0002595C File Offset: 0x00023B5C
		string IEditSessionAware.CreateEditSession()
		{
			RSTrace.CatalogTrace.Assert(this.m_compiledDefinition != null && this.m_compiledDefinition.SnapshotDataID != Guid.Empty, "invalid intermediateID");
			string text = UrlFriendlyUIDGenerator.Create();
			string text3;
			bool flag;
			string text2 = ItemProperties.PrepareForSaving(base.Properties, out text3, out flag);
			CatalogItemPath catalogItemPath = base.Service.ExternalToCatalogItemPath(this.m_itemContext.ItemPath);
			Guid guid = base.Service.Storage.CreateEditSession(catalogItemPath, text, this.m_itemContext.ItemName, this.Content, text3, this.m_compiledDefinition.SnapshotDataID, text2, base.ParametersXml, base.DataCacheHash);
			base.ItemID = guid;
			base.ItemContext.ItemPath.EditSessionID = text;
			base.SaveDataSources();
			base.SaveDataSets();
			return base.ItemContext.ItemPath.FullEditSessionIdentifier;
		}
	}
}
