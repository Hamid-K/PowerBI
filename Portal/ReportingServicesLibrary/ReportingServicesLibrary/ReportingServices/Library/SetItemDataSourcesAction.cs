using System;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Library.Soap2005;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000EE RID: 238
	internal sealed class SetItemDataSourcesAction : RSSoapAction<SetItemDataSourcesActionParameters>
	{
		// Token: 0x06000A01 RID: 2561 RVA: 0x000267DD File Offset: 0x000249DD
		internal SetItemDataSourcesAction(RSService service)
			: base("SetItemDataSourcesAction", service)
		{
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x000267EC File Offset: 0x000249EC
		protected override void AddActionToBatch()
		{
			string text = DataSource.ThisArrayToXml(base.ActionParameters.ItemDataSources);
			base.Service.Storage.ConnectionManager.VerifyConnection(true);
			byte[] array = CatalogEncryption.Instance.Encrypt(text, null);
			base.Service.Storage.AddBatchRecord(base.BatchID, base.Service.UserName, CatalogCommand.SetItemDataSources, base.ActionParameters.ItemPath, "DataSources", null, null, null, null, false, array, null);
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x00026868 File Offset: 0x00024A68
		internal override void PerformActionInBatch(CallParameters parameters)
		{
			DataSource[] array = DataSource.XmlToThisArray(CatalogEncryption.Instance.DecryptToString(Encryption.CurrentVersion, parameters.Content, null), true);
			base.ActionParameters.ItemPath = parameters.Item;
			base.ActionParameters.ItemDataSources = array;
			this.PerformActionNow();
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x000268B8 File Offset: 0x00024AB8
		internal override void PerformActionNow()
		{
			CatalogItemContext catalogItemContext = base.Service.ConstructItemContext(base.ActionParameters.ItemPath, true, "Item");
			CatalogItem catalogItem = base.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext, true);
			if (catalogItem.ThisItemType == ItemType.Report || catalogItem.ThisItemType == ItemType.RdlxReport)
			{
				this.SetReportDataSources(catalogItem, base.ActionParameters.ItemDataSources, base.ActionParameters.IgnoreSecCheck);
				return;
			}
			if (catalogItem.ThisItemType == ItemType.Model)
			{
				this.SetModelDataSource(catalogItem, base.ActionParameters.ItemDataSources, base.ActionParameters.IgnoreSecCheck);
				return;
			}
			if (catalogItem.ThisItemType == ItemType.DataSet)
			{
				this.SetDataSetDataSource(catalogItem, base.ActionParameters.ItemDataSources, base.ActionParameters.IgnoreSecCheck);
				return;
			}
			throw new WrongItemTypeException(catalogItemContext.OriginalItemPath.Value);
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x00026984 File Offset: 0x00024B84
		private void SetReportDataSources(CatalogItem item, DataSource[] dataSources, bool ignoreSecCheck)
		{
			FullReportCatalogItem fullReportCatalogItem = item as FullReportCatalogItem;
			if (!ignoreSecCheck)
			{
				fullReportCatalogItem.ThrowIfNoAccess(ReportOperation.UpdateDatasource);
			}
			DataSourceInfoCollection dataSources2 = fullReportCatalogItem.DataSources;
			DataSourceInfoCollection dataSourceInfoCollection = DataSource.ThisArrayToDataSourceInfoCollection(dataSources);
			this.ResolveNewDataSources(dataSourceInfoCollection, false);
			RSTrace.CatalogTrace.Assert(fullReportCatalogItem.ExecuteOption != 0);
			SetItemDataSourcesAction.VerifyDataSources(dataSourceInfoCollection, fullReportCatalogItem);
			DataSourceInfoCollection dataSourceInfoCollection2 = dataSources2.CombineOnSetDataSources(dataSourceInfoCollection);
			if (fullReportCatalogItem.ItemContext.ItemPath.IsEditSession)
			{
				fullReportCatalogItem.FlushDataCache();
			}
			fullReportCatalogItem.DataSources = dataSourceInfoCollection2;
			fullReportCatalogItem.SaveDataSources();
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x00026A00 File Offset: 0x00024C00
		private void SetModelDataSource(CatalogItem item, DataSource[] dataSources, bool ignoreSecCheck)
		{
			ModelCatalogItem modelCatalogItem = item as ModelCatalogItem;
			if (!ignoreSecCheck)
			{
				modelCatalogItem.ThrowIfNoAccess(ModelOperation.UpdateDatasource);
			}
			DataSourceInfoCollection dataSourceInfoCollection = DataSource.ThisArrayToDataSourceInfoCollection(dataSources);
			if (dataSourceInfoCollection.Count != 1)
			{
				throw new InvalidParameterException("DataSources");
			}
			DataSourceInfoCollection dataSources2 = modelCatalogItem.DataSources;
			this.ResolveNewDataSources(dataSourceInfoCollection, true);
			DataSourceInfo theOnlyDataSource = dataSourceInfoCollection.GetTheOnlyDataSource();
			if (!theOnlyDataSource.IsReference)
			{
				throw new InvalidElementException("Item");
			}
			if (!theOnlyDataSource.IsModel)
			{
				throw new InternalCatalogException("SetModelDataSource: dataSource.IsModel must be true.");
			}
			DataSourceInfoCollection dataSourceInfoCollection2 = dataSources2.CombineOnSetDataSources(dataSourceInfoCollection);
			modelCatalogItem.DataSources = dataSourceInfoCollection2;
			modelCatalogItem.SaveDataSources();
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x00026A88 File Offset: 0x00024C88
		private void SetDataSetDataSource(CatalogItem item, DataSource[] dataSources, bool ignoreSecCheck)
		{
			DataSetCatalogItem dataSetCatalogItem = item as DataSetCatalogItem;
			if (!ignoreSecCheck)
			{
				dataSetCatalogItem.ThrowIfNoAccess(ReportOperation.UpdateDatasource);
			}
			DataSourceInfoCollection dataSourceInfoCollection = DataSource.ThisArrayToDataSourceInfoCollection(dataSources);
			if (dataSourceInfoCollection.Count != 1)
			{
				throw new InvalidParameterException("DataSources");
			}
			DataSourceInfoCollection dataSources2 = dataSetCatalogItem.DataSources;
			this.ResolveNewDataSources(dataSourceInfoCollection, false);
			if (!dataSourceInfoCollection.GetTheOnlyDataSource().IsReference)
			{
				throw new InvalidElementException("Item");
			}
			DataSourceInfoCollection dataSourceInfoCollection2 = dataSources2.CombineOnSetDataSources(dataSourceInfoCollection);
			dataSetCatalogItem.DataSources = dataSourceInfoCollection2;
			dataSetCatalogItem.SaveDataSources();
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x00026AFC File Offset: 0x00024CFC
		private void ResolveNewDataSources(DataSourceInfoCollection newDataSources, bool forModel)
		{
			foreach (object obj in newDataSources)
			{
				DataSourceInfo dataSourceInfo = (DataSourceInfo)obj;
				if (dataSourceInfo.ReferenceByPath)
				{
					CatalogItemContext catalogItemContext = new CatalogItemContext(base.Service, dataSourceInfo.DataSourceReference, DataSourceInfo.GetDataSourceReferenceXmlTag());
					CatalogItem catalogItem = base.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext, true);
					if (catalogItem.ThisItemType != ItemType.DataSource && (forModel || catalogItem.ThisItemType != ItemType.Model))
					{
						throw new WrongItemTypeException(catalogItemContext.ItemPath.Value);
					}
					catalogItem.ThrowIfNoAccess(CommonOperation.ReadProperties);
					DataSourceInfoCollection dataSources = base.Service.Storage.GetDataSources(catalogItem.ItemID);
					dataSourceInfo.CopyFrom(dataSources.GetTheOnlyDataSource(), catalogItemContext.ItemPath.Value, catalogItem.ItemID, forModel);
				}
			}
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x00026BEC File Offset: 0x00024DEC
		internal static void VerifyDataSources(DataSourceInfoCollection dataSources, FullReportCatalogItem item)
		{
			bool flag = item.ThisItemType == ItemType.RdlxReport;
			int executeOption = item.ExecuteOption;
			foreach (object obj in dataSources)
			{
				DataSourceInfo dataSourceInfo = (DataSourceInfo)obj;
				if ((ExecutionOptions.IsSnapshotExecution(executeOption) || ExecutionOptions.IsHistoryOnSchedule(executeOption)) && !DataSourceCatalogItem.GoodForUnattendedExecution(dataSourceInfo))
				{
					throw new OperationPreventsUnattendedExecutionException();
				}
				if ((!dataSourceInfo.IsModel || dataSourceInfo.ReferenceIsValid) && Globals.Configuration.Extensions.Data[dataSourceInfo.Extension] == null)
				{
					throw new DataExtensionNotFoundException(dataSourceInfo.Extension);
				}
				if (flag)
				{
					DataSourceCatalogItem.ThrowIfNotGoodForRdlx(dataSourceInfo, item.ItemContext.OriginalItemPath.Value);
				}
			}
		}
	}
}
