using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200018C RID: 396
	internal class RenderEditAction : RenderEditActionBase, IPowerViewExecution
	{
		// Token: 0x06000EA4 RID: 3748 RVA: 0x00035940 File Offset: 0x00033B40
		public RenderEditAction(Stream inputStream, Stream outputStream, IList<string> responseFlags, IRenderEditSession session, RSService service, CatalogItemContext reportContext, string jobId)
			: base(inputStream, outputStream, responseFlags, session, service, reportContext, jobId)
		{
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x00035954 File Offset: 0x00033B54
		protected override bool InitializeAction()
		{
			this.m_service.WillDisconnectStorage();
			try
			{
				Security.SafeCheckExecuteReportDefinitionPermission(this.m_service, null, true);
			}
			catch (Exception ex)
			{
				this.m_service.AbortTransaction();
				if (ex is RSException)
				{
					throw;
				}
				throw new InternalCatalogException(ex, null);
			}
			finally
			{
				this.m_service.DisconnectStorage();
			}
			return base.InitializeAction();
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x000359C8 File Offset: 0x00033BC8
		protected override void ExecuteAction()
		{
			if (!base.ValidateSession(this.m_session))
			{
				return;
			}
			RenderEditSessionManager.ExecuteInRenderEditSession(this, this.m_session, this.m_jobId, this.m_service.UserName, this.OperationName);
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x000359FC File Offset: 0x00033BFC
		public void ProcessInput(bool foundSession, ProgressiveCacheEntry entry)
		{
			using (SurrogateContextFactory.CreateContext(out this.m_execType))
			{
				using (this.m_provider.EnterStorageContext(null))
				{
					string text = this.m_reader.ConsumeOptionalValue<string>("rdlxPath");
					bool flag = false;
					Stream stream = this.m_reader.ConsumeOptionalValue<Stream>("dataSources");
					if (stream != null)
					{
						flag = true;
						entry.PopulateCacheEntryWithDataSourceInfo(stream);
						new DataSourceResolver("/", this.m_reportContext, this.m_service).ProcessCachedDataSourceInfoForSecureStoreCredentials(entry);
					}
					if (!flag && entry.DataSources == null && text != null)
					{
						this.PopulateCacheEntryWithCatalogMetadata(entry, text);
					}
					Stream stream2;
					if (foundSession && entry.Report == null)
					{
						stream2 = this.m_reader.ConsumeRequiredValue<Stream>("rdlx");
					}
					else
					{
						stream2 = this.m_reader.ConsumeOptionalValue<Stream>("rdlx");
					}
					if (stream2 != null)
					{
						base.PublishReport(stream2, entry);
					}
				}
			}
			this.m_cacheEntry = entry;
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x00035B0C File Offset: 0x00033D0C
		public void RenderItem()
		{
			Stream stream = this.m_reader.ConsumeOptionalValue<Stream>("dsq");
			if (stream != null)
			{
				base.RenderReport(this.m_execType, this.m_cacheEntry, stream);
			}
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x000357CE File Offset: 0x000339CE
		public void WriteMessage(string component, string errorCode)
		{
			base.MessageWriter.WriteMessage(component, errorCode);
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x00035B40 File Offset: 0x00033D40
		public void WriteSessionId(string sessionId)
		{
			this.WriteMessage("sessionId", sessionId);
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x00035B50 File Offset: 0x00033D50
		private void PopulateCacheEntryWithCatalogMetadata(ProgressiveCacheEntry entry, string rdlxPath)
		{
			this.m_service.ExecuteNestedTransaction(delegate(RSService newService)
			{
				CatalogItemContext catalogItemContext = new CatalogItemContext(newService, rdlxPath, "rdlxPath");
				FullReportCatalogItem fullReportCatalogItem = (FullReportCatalogItem)newService.CatalogItemFactory.GetCatalogItem(catalogItemContext, ItemType.RdlxReport, true);
				fullReportCatalogItem.ThrowIfNoAccess(ReportOperation.ExecuteAndView);
				this.m_session.ItemPath = catalogItemContext.ItemPath;
				entry.DataSources = fullReportCatalogItem.DataSources;
			});
		}

		// Token: 0x04000607 RID: 1543
		private ReportProcessing.ExecutionType m_execType;

		// Token: 0x04000608 RID: 1544
		private ProgressiveCacheEntry m_cacheEntry;
	}
}
