using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000D2 RID: 210
	[CatalogItemType(ItemType.Unknown)]
	internal abstract class FullReportCatalogItem : BaseReportCatalogItem
	{
		// Token: 0x0600091F RID: 2335 RVA: 0x00024683 File Offset: 0x00022883
		internal FullReportCatalogItem(RSService service)
			: base(service)
		{
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x0002468C File Offset: 0x0002288C
		protected override void ContentLoadSecurityCheck()
		{
			RSTrace.CatalogTrace.Assert(this.m_linkID == Guid.Empty && this.m_itemMetadata.MimeType == null);
			base.ThrowIfNoAccess(ReportOperation.ReadReportDefinition);
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000921 RID: 2337 RVA: 0x000246C2 File Offset: 0x000228C2
		// (set) Token: 0x06000922 RID: 2338 RVA: 0x000246E4 File Offset: 0x000228E4
		internal override DataSourceInfoCollection DataSources
		{
			get
			{
				if (this.m_dataSources == null)
				{
					this.m_dataSources = base.GetSyncedItemDataSources(this.m_itemID);
				}
				return this.m_dataSources;
			}
			set
			{
				this.m_dataSources = value;
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000923 RID: 2339 RVA: 0x000246ED File Offset: 0x000228ED
		// (set) Token: 0x06000924 RID: 2340 RVA: 0x0002470F File Offset: 0x0002290F
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

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000925 RID: 2341 RVA: 0x00004FDD File Offset: 0x000031DD
		// (set) Token: 0x06000926 RID: 2342 RVA: 0x00004FE5 File Offset: 0x000031E5
		internal override byte[] Content
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_content;
			}
			[DebuggerStepThrough]
			set
			{
				this.m_content = value;
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000927 RID: 2343 RVA: 0x00024718 File Offset: 0x00022918
		internal int ExecuteOption
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_executionOptions;
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000928 RID: 2344 RVA: 0x00005BEF File Offset: 0x00003DEF
		protected virtual bool IsRdlx
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000929 RID: 2345
		internal abstract void FlushDataCache();

		// Token: 0x0600092A RID: 2346 RVA: 0x00024720 File Offset: 0x00022920
		internal Warning[] PrepareNewReport(byte[] definition, bool usePermanentSnapshot)
		{
			this.m_content = definition;
			ReportSnapshot reportSnapshot;
			ParameterInfoCollection parameterInfoCollection;
			Warning[] array;
			DataSourceInfoCollection dataSourceInfoCollection;
			DataSetInfoCollection dataSetInfoCollection;
			PageProperties pageProperties;
			byte[] array2;
			base.Service.ConvertToIntermediate(this.Content, usePermanentSnapshot, base.Properties, base.ItemContext, base.CreationDate, true, ReportProcessingFlags.NotSet, false, this.IsRdlx, out reportSnapshot, out parameterInfoCollection, out array, out dataSourceInfoCollection, out dataSetInfoCollection, out pageProperties, out array2);
			this.m_compiledDefinition = reportSnapshot;
			this.m_dataSources = dataSourceInfoCollection;
			this.m_sharedDataSets = dataSetInfoCollection;
			base.Parameters = parameterInfoCollection;
			this.m_dataCacheHash = array2;
			ItemProperties itemProperties = new ItemProperties(new ReportPageProperties(base.Service, base.ItemContext.ItemPath, pageProperties.PageHeight, pageProperties.PageWidth, pageProperties.TopMargin, pageProperties.BottomMargin, pageProperties.LeftMargin, pageProperties.RightMargin).InitializeNewPropertyArray(), ItemType.Report);
			itemProperties.EnsurePropertiesWritable();
			base.CombineProperties(itemProperties);
			return array;
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x000247EC File Offset: 0x000229EC
		internal void SaveDataSources()
		{
			base.Service.Storage.DeleteDataSources(base.ItemID);
			base.Service.AddDataSources(base.ItemID, this.DataSources, base.ItemContext.ItemPath.EditSessionID);
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x0002482C File Offset: 0x00022A2C
		internal void SaveReportDefinition()
		{
			byte[] array = null;
			if (base.ItemContext.ItemPath.IsEditSession)
			{
				array = this.DataCacheHash;
			}
			base.Service.Storage.SetObjectContent(base.ItemContext.CatalogItemPath, ItemType.Report, this.m_content, this.m_compiledDefinition.SnapshotDataID, base.ParametersXml, Guid.Empty, null, array);
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x0002488E File Offset: 0x00022A8E
		internal void SaveDataSets()
		{
			base.Service.Storage.DeleteDataSets(base.ItemID);
			base.Service.AddDataSets(base.ItemID, this.SharedDataSets, base.ItemContext.ItemPath.EditSessionID);
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x000248CD File Offset: 0x00022ACD
		protected override void Update()
		{
			this.SaveReportDefinition();
			base.SaveProperties();
			this.SaveDataSources();
			this.SaveDataSets();
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x000248E8 File Offset: 0x00022AE8
		protected override void FinalizeCreation()
		{
			base.Service.AddDataSources(base.ItemID, this.DataSources, base.ItemContext.ItemPath.EditSessionID);
			base.Service.AddDataSets(base.ItemID, this.SharedDataSets, base.ItemContext.ItemPath.EditSessionID);
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x00024944 File Offset: 0x00022B44
		internal Warning[] UpdateDefinition(byte[] definition)
		{
			this.m_content = definition;
			base.LoadProperties();
			base.ThrowIfNoAccess(ReportOperation.UpdateReportDefinition);
			bool isEditSession = base.ItemContext.ItemPath.IsEditSession;
			DataSourceInfoCollection dataSources = this.DataSources;
			DataSetInfoCollection sharedDataSets = this.SharedDataSets;
			ReportSnapshot reportSnapshot;
			ParameterInfoCollection parameterInfoCollection;
			Warning[] array;
			DataSourceInfoCollection dataSourceInfoCollection;
			DataSetInfoCollection dataSetInfoCollection;
			PageProperties pageProperties;
			byte[] array2;
			base.Service.ConvertToIntermediate(this.Content, !isEditSession, base.Properties, base.ItemContext, base.ModificationDate, true, true, dataSources, sharedDataSets, ReportProcessingFlags.NotSet, false, this.IsRdlx, out reportSnapshot, out parameterInfoCollection, out array, out dataSourceInfoCollection, out dataSetInfoCollection, out pageProperties, out array2);
			bool flag;
			if (isEditSession)
			{
				base.Parameters = parameterInfoCollection;
				flag = false;
			}
			else
			{
				base.Parameters = ParameterInfoCollection.Match(base.Parameters, parameterInfoCollection, out flag);
			}
			this.m_compiledDefinition = reportSnapshot;
			this.DataCacheHash = array2;
			ItemProperties itemProperties = new ItemProperties(new ReportPageProperties(base.Service, base.ItemContext.ItemPath, pageProperties.PageHeight, pageProperties.PageWidth, pageProperties.TopMargin, pageProperties.BottomMargin, pageProperties.LeftMargin, pageProperties.RightMargin).InitializeNewPropertyArray(), ItemType.Report);
			itemProperties.EnsurePropertiesWritable();
			base.CombineProperties(itemProperties);
			this.DataSources = dataSources.CombineOnSetDefinition(dataSourceInfoCollection);
			this.SharedDataSets = sharedDataSets.CombineOnSetDefinition(dataSetInfoCollection);
			if (flag)
			{
				List<LinkedReportCatalogItem> linkedReports = base.Service.Storage.GetLinkedReports(base.Service, base.ItemID);
				this.UpdateLinkedReports(linkedReports);
			}
			this.Save(isEditSession);
			return array;
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x00024AA8 File Offset: 0x00022CA8
		private void UpdateLinkedReports(List<LinkedReportCatalogItem> linkedReports)
		{
			if (linkedReports == null)
			{
				return;
			}
			foreach (LinkedReportCatalogItem linkedReportCatalogItem in linkedReports)
			{
				linkedReportCatalogItem.Parameters = ParameterInfoCollection.Match(linkedReportCatalogItem.Parameters, base.Parameters);
				linkedReportCatalogItem.SaveParameters();
			}
			if (RSTrace.CatalogTrace.TraceVerbose)
			{
				RSTrace.CatalogTrace.Trace(TraceLevel.Verbose, "Updated parameters for {0} linked reports", new object[] { linkedReports.Count });
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000932 RID: 2354 RVA: 0x00024B40 File Offset: 0x00022D40
		// (set) Token: 0x06000933 RID: 2355 RVA: 0x00024B48 File Offset: 0x00022D48
		internal byte[] DataCacheHash
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_dataCacheHash;
			}
			[DebuggerStepThrough]
			set
			{
				this.m_dataCacheHash = value;
			}
		}

		// Token: 0x04000459 RID: 1113
		private byte[] m_dataCacheHash;
	}
}
