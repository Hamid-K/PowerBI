using System;
using System.Globalization;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000F6 RID: 246
	internal sealed class CreateDataSetAction : CreateItemAction<CreateDataSetActionParameters, DataSetCatalogItem>
	{
		// Token: 0x06000A2E RID: 2606 RVA: 0x000271E6 File Offset: 0x000253E6
		public CreateDataSetAction(RSService service)
			: base("CreateDataSetAction", service)
		{
			Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.SharedDataset);
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000A2F RID: 2607 RVA: 0x000053DC File Offset: 0x000035DC
		protected override bool IsUpdateSupported
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x00027208 File Offset: 0x00025408
		protected override void UpdateExistingItem(DataSetCatalogItem item)
		{
			SetDataSetDefinitionAction setDataSetDefinitionAction = base.Service.SetDataSetDefinitionAction;
			setDataSetDefinitionAction.ActionParameters.DataSetDefinition = base.ActionParameters.DataSetDefinition;
			setDataSetDefinitionAction.Update(item);
			base.ActionParameters.Warnings = setDataSetDefinitionAction.ActionParameters.Warnings;
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x00027254 File Offset: 0x00025454
		protected override void PrepareForNewItem(DataSetCatalogItem item)
		{
			if (base.ActionParameters.DataSetDefinition != null)
			{
				item.Content = base.ActionParameters.DataSetDefinition;
			}
			ReportSnapshot reportSnapshot;
			ParameterInfoCollection parameterInfoCollection;
			Warning[] array;
			DataSourceInfoCollection dataSourceInfoCollection;
			base.Service.CreateDataSetAction.ConvertToIntermediate(item.Content, item.Properties, item.ItemContext, item.CreationDate, out reportSnapshot, out parameterInfoCollection, out array, out dataSourceInfoCollection);
			base.ActionParameters.Warnings = array;
			item.CompiledDefinition = reportSnapshot;
			item.DataSources = dataSourceInfoCollection;
			item.Parameters = parameterInfoCollection;
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x000272D0 File Offset: 0x000254D0
		internal void ConvertToIntermediate(byte[] definition, ItemProperties properties, CatalogItemContext dataSetContext, DateTime currentDate, out ReportSnapshot intermediateSnapshot, out ParameterInfoCollection parameters, out Warning[] warnings, out DataSourceInfoCollection dataSources)
		{
			ReportProcessing processingEngine = Global.GetProcessingEngine();
			intermediateSnapshot = base.Service.AllocateNewReportSnapshot(true, null, currentDate, null, ReportProcessingFlags.OnDemandEngine);
			ReportProcessing.CheckSharedDataSource checkSharedDataSource = new ReportProcessing.CheckSharedDataSource(this.CheckDataSourcePublishingCallback);
			try
			{
				DataSetPublishingResult dataSetPublishingResult = null;
				using (ISnapshotTransaction snapshotTransaction = intermediateSnapshot.EnterTransactionContext())
				{
					PublishingContext publishingContext = new PublishingContext(dataSetContext, definition, intermediateSnapshot, null, false, checkSharedDataSource, processingEngine.Configuration);
					dataSetPublishingResult = processingEngine.CreateSharedDataSet(publishingContext);
					snapshotTransaction.Commit();
				}
				if (properties != null)
				{
					if (properties.Description == null)
					{
						properties.Description = dataSetPublishingResult.DataSetDefinition.Description;
					}
					properties.HasUserProfileQueryDependencies = dataSetPublishingResult.HasUserProfileQueryDependencies.ToString();
					if (properties.QueryExecutionTimeOut == null)
					{
						properties.QueryExecutionTimeOut = dataSetPublishingResult.TimeOut.ToString(CultureInfo.InvariantCulture);
					}
				}
				warnings = Warning.ProcessingMessagesToWarningArray(dataSetPublishingResult.Warnings);
				parameters = dataSetPublishingResult.DataSetDefinition.DataSetParameters;
				dataSources = new DataSourceInfoCollection();
				dataSources.Add(dataSetPublishingResult.DataSourceInfo);
			}
			catch (Exception)
			{
				intermediateSnapshot.DeleteSnapshotAndChunks();
				throw;
			}
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x000273F0 File Offset: 0x000255F0
		private DataSourceInfo CheckDataSourcePublishingCallback(string itemPath, out Guid catalogItemId)
		{
			byte[] array;
			ItemType itemType;
			DataSourceInfo dataSourceInfo = ProcessingPublishing.CheckDataSourcePublishingCallback(base.Service, itemPath, out catalogItemId, out array, out itemType);
			if (dataSourceInfo == null)
			{
				return null;
			}
			if (!base.Service.SecMgr.CheckAccess(itemType, array, CommonOperation.ReadProperties, base.Service.CatalogToExternal(itemPath)))
			{
				return null;
			}
			return dataSourceInfo;
		}
	}
}
