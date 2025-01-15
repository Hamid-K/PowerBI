using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Internal;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000186 RID: 390
	internal sealed class GetReportAndModelsAction : ModelActionBase
	{
		// Token: 0x06000E40 RID: 3648 RVA: 0x0003435C File Offset: 0x0003255C
		public GetReportAndModelsAction(string itemPath, string modelMetadataVersion, bool omitModels, Stream outputStream, IList<string> responseFlags, RSService service, CatalogItemContext itemContext)
			: base(itemPath, itemContext, outputStream, responseFlags, service)
		{
			this.m_omitModels = omitModels;
			this.m_modelMetaDataVersion = modelMetadataVersion;
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x000053DC File Offset: 0x000035DC
		protected override bool InitializeAction()
		{
			return true;
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x00005BF2 File Offset: 0x00003DF2
		protected override void CleanupForException()
		{
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x00005BF2 File Offset: 0x00003DF2
		protected override void FinalCleanup(ErrorCode status)
		{
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x0003437B File Offset: 0x0003257B
		private void GenerateAndWriteSession()
		{
			this.m_session = RenderEditRequest.CreateAndGenerate(this.m_service.UserName);
			base.MessageWriter.WriteMessage("sessionId", this.m_session.SessionId);
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x000343B0 File Offset: 0x000325B0
		private void PutSessionInCache()
		{
			ProgressiveCacheEntry progressiveCacheEntry = ProgressiveExecutionCacheManager.CreateCacheEntry(this.m_session.UserName, this.m_connectionPool);
			ProgressiveExecutionCacheManager.PutCacheEntry(this.m_session, progressiveCacheEntry);
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x000343E0 File Offset: 0x000325E0
		protected override void InternalExecute()
		{
			CatalogItem catalogItem = this.m_service.CatalogItemFactory.GetCatalogItem(this.m_itemContext, true);
			catalogItem.ThrowIfWrongItemType(new ItemType[] { ItemType.RdlxReport });
			FullReportCatalogItem fullReportCatalogItem = (FullReportCatalogItem)catalogItem;
			fullReportCatalogItem.ThrowIfNoAccess(ReportOperation.ReadReportDefinition);
			fullReportCatalogItem.ThrowIfNoAccess(ReportOperation.ReadDatasource);
			fullReportCatalogItem.ThrowIfNoAccess(ReportOperation.ReadProperties);
			fullReportCatalogItem.LoadDefinition();
			DataSourceInfoCollection dataSources = fullReportCatalogItem.DataSources;
			if (!this.m_omitModels && dataSources.Count != 1)
			{
				throw new InvalidDataSourceCountException(this.m_itemPath);
			}
			this.GenerateAndWriteSession();
			byte[] content = fullReportCatalogItem.Content;
			using (Stream stream = base.MessageWriter.CreateWritableStream("rdlx"))
			{
				stream.Write(content, 0, content.Length);
			}
			string text = null;
			if (!this.m_omitModels)
			{
				DataSourceInfo theOnlyDataSource = dataSources.GetTheOnlyDataSource();
				DataSourceResolverHelper.ValidateDataSource(theOnlyDataSource);
				DataSourceCatalogItem.ThrowIfNotGoodForRdlx(theOnlyDataSource, theOnlyDataSource.Name);
				text = this.GetModelFromDataExtension(theOnlyDataSource, theOnlyDataSource.Name);
			}
			GetItemDataSourcesAction.FixDataSourceReferences(dataSources, this.m_service, this.m_itemContext, catalogItem);
			this.WriteDataSourcesToOutput(dataSources);
			if (!this.m_omitModels)
			{
				this.WriteModelToOutput(text);
			}
			catalogItem.LoadProperties();
			Dictionary<string, object> dictionary = new Dictionary<string, object>(1);
			dictionary.Add("wasReportLastModifiedByCurrentUser", string.Equals(this.m_service.UserName, catalogItem.Properties.ModifiedBy, StringComparison.OrdinalIgnoreCase));
			base.MessageWriter.WriteMessage("additionalInformation", dictionary);
			this.PutSessionInCache();
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x00034558 File Offset: 0x00032758
		private void WriteDataSourcesToOutput(DataSourceInfoCollection dataSourceInfos)
		{
			DataSourceDefinition2Collection dataSourceDefinition2Collection = new DataSourceDefinition2Collection(dataSourceInfos.Count);
			foreach (object obj in dataSourceInfos)
			{
				DataSourceInfo dataSourceInfo = (DataSourceInfo)obj;
				if (this.m_omitModels)
				{
					DataSourceResolverHelper.ValidateDataSource(dataSourceInfo);
				}
				DataSourceReferenceOrDefinition dataSourceReferenceOrDefinition = new DataSourceReferenceOrDefinition();
				if (dataSourceInfo.IsReference)
				{
					dataSourceReferenceOrDefinition.Reference = dataSourceInfo.DataSourceReference;
				}
				else
				{
					dataSourceReferenceOrDefinition.Definition = new DataSourceDefinition2
					{
						ConnectString = this.GetConnectionString(dataSourceInfo, DataProtection.Instance),
						CredentialRetrieval = DataSourceResolverHelper.ConvertToCredentialsRetrievalType(dataSourceInfo.CredentialsRetrieval),
						WindowsCredentials = dataSourceInfo.WindowsCredentials,
						ImpersonateUser = dataSourceInfo.ImpersonateUser
					};
				}
				dataSourceDefinition2Collection.Add(dataSourceInfo.OriginalName, dataSourceReferenceOrDefinition);
			}
			using (Stream stream = base.MessageWriter.CreateWritableStream("dataSources"))
			{
				using (XmlWriter xmlWriter = XmlWriter.Create(stream))
				{
					dataSourceDefinition2Collection.WriteXml(xmlWriter);
				}
			}
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x00034694 File Offset: 0x00032894
		private string GetModelFromDataExtension(DataSourceInfo dataSourceInfo, string itemName)
		{
			this.m_connectionPool = new DbConnectionPool(new global::System.Action(ServerDataExtensionConnection.OnConnectionCloseAction));
			ModelRetrieval modelRetrieval = new ModelRetrieval("GetReportAndModelsAction");
			return this.m_dataSourceResolver.GetModelFromDataExtension(dataSourceInfo, this.m_connectionPool, itemName, this.m_modelMetaDataVersion, modelRetrieval);
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x000346DD File Offset: 0x000328DD
		private string GetConnectionString(DataSourceInfo dataSourceInfo, IDataProtection dataProtection)
		{
			if (dataSourceInfo.UseOriginalConnectionString)
			{
				return dataSourceInfo.GetOriginalConnectionString(dataProtection);
			}
			return dataSourceInfo.GetConnectionString(dataProtection);
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06000E4A RID: 3658 RVA: 0x000346F6 File Offset: 0x000328F6
		protected override string OperationName
		{
			get
			{
				return "GetReportAndModels";
			}
		}

		// Token: 0x040005DF RID: 1503
		private readonly string m_modelMetaDataVersion;

		// Token: 0x040005E0 RID: 1504
		private IRenderEditSession m_session;

		// Token: 0x040005E1 RID: 1505
		private IDbConnectionPool m_connectionPool;

		// Token: 0x040005E2 RID: 1506
		private bool m_omitModels;
	}
}
