using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000D5 RID: 213
	[CatalogItemType(ItemType.PowerBIReport)]
	internal class PowerBIReportCatalogItem : CatalogItem
	{
		// Token: 0x06000968 RID: 2408 RVA: 0x00004F8E File Offset: 0x0000318E
		internal PowerBIReportCatalogItem(RSService service)
			: base(service)
		{
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x0002543C File Offset: 0x0002363C
		internal void LoadContent()
		{
			base.LoadDefinition();
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x00025444 File Offset: 0x00023644
		protected override void ContentLoadSecurityCheck()
		{
			base.ThrowIfNoAccess(base.InternalUsePermissionForExecution ? ReportOperation.ExecuteAndView : ReportOperation.ReadReportDefinition);
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x0600096B RID: 2411 RVA: 0x00004FDD File Offset: 0x000031DD
		// (set) Token: 0x0600096C RID: 2412 RVA: 0x00004FE5 File Offset: 0x000031E5
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

		// Token: 0x0600096D RID: 2413 RVA: 0x00025458 File Offset: 0x00023658
		public void SetPreShreddedReadStreams(Stream unshreddedPbix, Stream pbix, Stream model)
		{
			if (unshreddedPbix == null && this.Content != null && this.Content.Length != 0)
			{
				unshreddedPbix = new MemoryStream(this.Content);
			}
			this._unshreddedPbix = unshreddedPbix;
			this._pbix = pbix ?? this._unshreddedPbix;
			this._model = model;
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x0600096E RID: 2414 RVA: 0x000254A5 File Offset: 0x000236A5
		// (set) Token: 0x0600096F RID: 2415 RVA: 0x000254AD File Offset: 0x000236AD
		public string DataModelParameters { get; set; }

		// Token: 0x06000970 RID: 2416 RVA: 0x000254B8 File Offset: 0x000236B8
		protected override void Update()
		{
			PowerBIReportCatalogItem.ExtendedContentData extendedContentData = this.WriteExtendedContentStream(ExtendedContentType.CatalogItem, this._unshreddedPbix);
			PowerBIReportCatalogItem.ExtendedContentData extendedContentData2 = this.WriteExtendedContentStream(ExtendedContentType.PowerBIReportDefinition, this._pbix);
			PowerBIReportCatalogItem.ExtendedContentData extendedContentData3 = this.WriteExtendedContentStream(ExtendedContentType.DataModel, this._model);
			this.UpdateContentLength(extendedContentData);
			this.UpdateDataModelParametersById(this.DataModelParameters);
			this.FinishExtendedContentStream(extendedContentData);
			this.FinishExtendedContentStream(extendedContentData2);
			this.FinishExtendedContentStream(extendedContentData3);
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x00025518 File Offset: 0x00023718
		internal override void Create()
		{
			if (base.ItemID != Guid.Empty)
			{
				throw new ItemAlreadyExistsException(this.m_itemContext.ItemPath.Value);
			}
			try
			{
				this.ProtectProperties();
				PowerBIReportCatalogItem.ExtendedContentData extendedContentData = this.WriteExtendedContentStream(ExtendedContentType.CatalogItem, this._unshreddedPbix);
				PowerBIReportCatalogItem.ExtendedContentData extendedContentData2 = this.WriteExtendedContentStream(ExtendedContentType.PowerBIReportDefinition, this._pbix);
				PowerBIReportCatalogItem.ExtendedContentData extendedContentData3 = this.WriteExtendedContentStream(ExtendedContentType.DataModel, this._model);
				base.ItemID = base.Service.Storage.CreateObject(Guid.Empty, this.m_itemContext.ItemName, this.m_itemContext.CatalogItemPath, base.Parent.ItemContext.ItemPath, base.Parent.ItemID, base.ThisItemType, this.m_content, (this.m_compiledDefinition == null) ? Guid.Empty : this.m_compiledDefinition.SnapshotDataID, this.m_linkID, ItemPathBase.SafeValue(this.m_linkPath), base.Properties, this.m_parametersXml, base.CreatedBy, base.CreationDate, base.ModificationDate, this.m_itemMetadata.MimeType, this.m_itemMetadata.SubType, this.m_itemMetadata.ComponentID);
				this.FinalizeCreation();
				this.UnprotectProperties();
				this.UpdateContentLength(extendedContentData);
				this.UpdateDataModelParametersById(this.DataModelParameters);
				this.FinishExtendedContentStream(extendedContentData);
				this.FinishExtendedContentStream(extendedContentData2);
				this.FinishExtendedContentStream(extendedContentData3);
			}
			catch (Exception)
			{
				try
				{
					base.Service.ServiceHelper.AbortCreation(this);
				}
				catch (Exception ex)
				{
					if (RSTrace.CatalogTrace.TraceWarning)
					{
						RSTrace.CatalogTrace.Trace(TraceLevel.Warning, string.Format(CultureInfo.InvariantCulture, "Fail to cleanup when there is error creating catalog item '{0}' to external storage. Exception is {1}.", base.ItemContext.ItemPath, ex.Message));
					}
				}
				throw;
			}
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x000256FC File Offset: 0x000238FC
		private PowerBIReportCatalogItem.ExtendedContentData WriteExtendedContentStream(ExtendedContentType extendedContentType, Stream stream)
		{
			if (stream != null)
			{
				PowerBIReportCatalogItem.ExtendedContentData extendedContentData = new PowerBIReportCatalogItem.ExtendedContentData();
				extendedContentData.Id = base.Service.Storage.CreateExtendedCatalogContent(extendedContentType, stream, out extendedContentData.Length);
				return extendedContentData;
			}
			return null;
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x00025733 File Offset: 0x00023933
		private long UpdateExtendedContentStream(ExtendedContentType extendedContentType, Stream stream)
		{
			if (stream != null)
			{
				return base.Service.Storage.WriteExtendedCatalogContent(base.ItemID, extendedContentType, stream, new DateTime?(base.ModificationDate));
			}
			return 0L;
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x0002575E File Offset: 0x0002395E
		private void UpdateContentLength(PowerBIReportCatalogItem.ExtendedContentData modelInfo)
		{
			if (modelInfo == null)
			{
				throw new ItemNotFoundException(base.Service.CatalogToExternal(this.m_itemContext.CatalogItemPath).Value);
			}
			this.UpdateContentLength(modelInfo.Length);
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x00025790 File Offset: 0x00023990
		private void UpdateContentLength(long length)
		{
			base.Service.Storage.WriteContentSize(base.ItemID, length);
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x000257A9 File Offset: 0x000239A9
		private void UpdateDataModelParametersById(string parameters)
		{
			if (parameters != null && parameters.Length > 0)
			{
				base.Service.Storage.WriteDataModelParameters(base.ItemID, parameters);
			}
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x000257CE File Offset: 0x000239CE
		private void FinishExtendedContentStream(PowerBIReportCatalogItem.ExtendedContentData modelInfo)
		{
			if (modelInfo != null)
			{
				base.Service.Storage.FinalizeNewExtendedCatalogContent(modelInfo.Id, base.ItemID);
			}
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x000257F0 File Offset: 0x000239F0
		internal override void Save(bool preventCreate)
		{
			if (base.ItemID == Guid.Empty)
			{
				throw new ItemNotFoundException(this.m_itemContext.ItemPath.Value);
			}
			long num = this.UpdateExtendedContentStream(ExtendedContentType.CatalogItem, this._unshreddedPbix);
			this.UpdateExtendedContentStream(ExtendedContentType.PowerBIReportDefinition, this._pbix);
			this.UpdateExtendedContentStream(ExtendedContentType.DataModel, this._model);
			base.Service.Storage.SetObjectContent(base.ItemContext.CatalogItemPath, ItemType.PowerBIReport, this.Content, Guid.Empty, null, Guid.Empty, null);
			base.AdjustModificationInfo();
			this.UpdateContentLength(num);
			this.UpdateDataModelParametersById(this.DataModelParameters);
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x00025897 File Offset: 0x00023A97
		internal override void LoadStoredAndDerivedProperties()
		{
			base.LoadProperties();
			this.DeriveProperties();
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x000258A5 File Offset: 0x00023AA5
		protected void DeriveProperties()
		{
			base.ItemMetadata.HasDataSources = base.Service.Storage.HasDataModelDataSources(this.m_itemID);
		}

		// Token: 0x04000462 RID: 1122
		private Stream _unshreddedPbix;

		// Token: 0x04000463 RID: 1123
		private Stream _pbix;

		// Token: 0x04000464 RID: 1124
		private Stream _model;

		// Token: 0x02000464 RID: 1124
		internal class ExtendedContentData
		{
			// Token: 0x04000FB6 RID: 4022
			internal long Id;

			// Token: 0x04000FB7 RID: 4023
			internal long Length;
		}
	}
}
