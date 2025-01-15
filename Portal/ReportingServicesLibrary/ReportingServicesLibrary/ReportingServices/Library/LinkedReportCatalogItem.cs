using System;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000D3 RID: 211
	[CatalogItemType(ItemType.LinkedReport)]
	internal class LinkedReportCatalogItem : BaseReportCatalogItem
	{
		// Token: 0x06000934 RID: 2356 RVA: 0x00024683 File Offset: 0x00022883
		internal LinkedReportCatalogItem(RSService service)
			: base(service)
		{
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x00024B51 File Offset: 0x00022D51
		internal void Initialize(CatalogItemContext itemContext, Guid itemID, byte[] securityDescriptor, Guid linkID)
		{
			base.Initialize(itemContext, itemID, securityDescriptor);
			this.m_linkID = linkID;
			this.m_linkIDInit = true;
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x00024B6C File Offset: 0x00022D6C
		internal void LoadLink()
		{
			base.LoadDefinition();
			RSTrace.CatalogTrace.Assert(this.m_content == null && this.m_itemMetadata.MimeType == null);
			if (this.m_linkID != Guid.Empty)
			{
				this.m_linkPath = base.Service.Storage.GetPathById(this.m_linkID);
			}
			this.SetLinkInitFlag();
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x00004FD4 File Offset: 0x000031D4
		protected override void ContentLoadSecurityCheck()
		{
			base.ThrowIfNoAccess(ReportOperation.ReadProperties);
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x00024BD6 File Offset: 0x00022DD6
		internal override void LoadStoredAndDerivedProperties()
		{
			base.LoadProperties();
			if (this.m_linkID != Guid.Empty)
			{
				base.DeriveProperties();
			}
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x00024BF6 File Offset: 0x00022DF6
		internal void EnsureLinkID()
		{
			if (this.LinkID == Guid.Empty)
			{
				throw new InvalidReportLinkException();
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x0600093A RID: 2362 RVA: 0x00024C10 File Offset: 0x00022E10
		internal Guid LinkID
		{
			get
			{
				RSTrace.CatalogTrace.Assert(this.m_linkIDInit);
				return this.m_linkID;
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x0600093B RID: 2363 RVA: 0x00024C28 File Offset: 0x00022E28
		internal CatalogItemPath LinkPath
		{
			get
			{
				RSTrace.CatalogTrace.Assert(this.m_linkPathInit);
				return this.m_linkPath;
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x0600093C RID: 2364 RVA: 0x00024C40 File Offset: 0x00022E40
		// (set) Token: 0x0600093D RID: 2365 RVA: 0x00024C62 File Offset: 0x00022E62
		internal override DataSourceInfoCollection DataSources
		{
			get
			{
				if (this.m_dataSources == null)
				{
					this.m_dataSources = base.GetSyncedItemDataSources(this.LinkID);
				}
				return this.m_dataSources;
			}
			set
			{
				RSTrace.CatalogTrace.Assert(false, "Do not support set data source on linked report.");
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x0600093E RID: 2366 RVA: 0x00024C74 File Offset: 0x00022E74
		// (set) Token: 0x0600093F RID: 2367 RVA: 0x00024C96 File Offset: 0x00022E96
		internal override DataSetInfoCollection SharedDataSets
		{
			get
			{
				if (this.m_sharedDataSets == null)
				{
					this.m_sharedDataSets = base.GetSyncedItemSharedDataSets(this.LinkID);
				}
				return this.m_sharedDataSets;
			}
			set
			{
				RSTrace.CatalogTrace.Assert(false, "Do not support set shared data sets on linked report.");
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000940 RID: 2368 RVA: 0x00024CA8 File Offset: 0x00022EA8
		// (set) Token: 0x06000941 RID: 2369 RVA: 0x00024CB0 File Offset: 0x00022EB0
		internal ProfessionalReportCatalogItem SourceReport
		{
			get
			{
				return this.m_sourceReport;
			}
			set
			{
				this.m_sourceReport = value;
				this.m_linkID = this.m_sourceReport.ItemID;
				this.m_linkPath = this.m_sourceReport.ItemContext.CatalogItemPath;
				this.SetLinkInitFlag();
			}
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x00024CE8 File Offset: 0x00022EE8
		internal void SaveParametersAndLink()
		{
			if (base.ItemContext == null)
			{
				throw new InternalCatalogException("LinkedReportCatalogItem is not initialized on SaveParametersAndLink()");
			}
			if (this.LinkID != Guid.Empty)
			{
				base.Parameters = ParameterInfoCollection.Match(base.Parameters, this.m_sourceReport.Parameters);
			}
			else
			{
				base.Parameters = this.m_sourceReport.Parameters;
			}
			base.Service.Storage.SetObjectContent(base.ItemContext.CatalogItemPath, base.ThisItemType, null, Guid.Empty, base.ParametersXml, this.LinkID, null);
			base.AdjustModificationInfo();
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x00024D83 File Offset: 0x00022F83
		internal new void SaveParameters()
		{
			if (base.ItemID == Guid.Empty)
			{
				throw new InternalCatalogException("LinkedReportCatalogItem is not initialized on SaveParameters()");
			}
			base.Service.Storage.SetParametersById(base.ItemID, base.ParametersXml);
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x00024DC0 File Offset: 0x00022FC0
		private void SetLinkInitFlag()
		{
			this.m_linkIDInit = (this.m_linkPathInit = true);
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x00024DE0 File Offset: 0x00022FE0
		protected override UserProfileState GetReportUserProfileProperties()
		{
			if (this.LinkID == Guid.Empty)
			{
				throw new InvalidReportLinkException();
			}
			if (this.m_linkedPropertyResolver == null)
			{
				CatalogItemPath pathById = base.Service.Storage.GetPathById(this.LinkID);
				LinkedReportProperyResolver linkedReportProperyResolver = new LinkedReportProperyResolver(base.Service.CatalogToExternal(pathById), base.Service);
				linkedReportProperyResolver.Resolve();
				this.m_linkedPropertyResolver = linkedReportProperyResolver;
			}
			RSTrace.CatalogTrace.Assert(this.m_linkedPropertyResolver != null);
			return this.m_linkedPropertyResolver.DependsOnUser;
		}

		// Token: 0x0400045A RID: 1114
		private ProfessionalReportCatalogItem m_sourceReport;

		// Token: 0x0400045B RID: 1115
		private LinkedReportProperyResolver m_linkedPropertyResolver;
	}
}
