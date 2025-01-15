using System;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200008F RID: 143
	internal class ExecuteStandaloneDataSet : StandaloneExecutionBase<DataSetCatalogItem>
	{
		// Token: 0x060005F1 RID: 1521 RVA: 0x00018589 File Offset: 0x00016789
		internal ExecuteStandaloneDataSet(DataSetCatalogItem item)
			: base(item)
		{
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x00018594 File Offset: 0x00016794
		protected override void CallProcessing()
		{
			ParameterInfoCollection combinedRequestParameters = base.Item.GetCombinedRequestParameters();
			DateTime executionDateNotTruncatedLocalTime = base.ExecutionDateNotTruncatedLocalTime;
			RuntimeDataSourceInfoCollection runtimeDataSourceInfoCollection = new RuntimeDataSourceInfoCollection();
			foreach (object obj in base.Item.DataSources)
			{
				DataSourceInfo dataSourceInfo = (DataSourceInfo)obj;
				runtimeDataSourceInfoCollection.Add(dataSourceInfo, base.Item.ItemContext);
			}
			ItemType itemType;
			ReportSnapshot reportSnapshot;
			Guid guid;
			string text;
			string text2;
			byte[] array;
			Guid guid2;
			int num;
			ReportSnapshot reportSnapshot2;
			string text3;
			string text4;
			Guid guid3;
			base.Service.Storage.GetCompiledDefinition(base.Item.ItemContext.CatalogItemPath, out itemType, out reportSnapshot, out guid, out text, out text2, out array, out guid2, out num, out reportSnapshot2, out text3, out text4, out guid3);
			ReportProcessing processingEngine = Global.GetProcessingEngine();
			ReportProcessing.ExecutionType executionType;
			using (SurrogateContextFactory.CreateContext(out executionType))
			{
				using (reportSnapshot.EnterTransactionContext())
				{
					DataSetContext dataSetContext = new DataSetContext("SharedDataSet", null, true, null, base.Item.ItemContext, runtimeDataSourceInfoCollection, base.UserName, executionDateNotTruncatedLocalTime, combinedRequestParameters, null, executionType, Localization.ClientPrimaryCulture, UserProfileState.None, base.Item.Properties.DependsOnUser, new ServerDataExtensionConnection(base.Service.HowToCreateDataExtensionInstance, base.Service.UserContext, executionType, new ServerAdditionalToken(base.Service, base.Item.ItemContext)), new CreateAndRegisterStream(base.Service.StreamManager.GetNewStream), ReportRuntimeSetup.GetDefault(), ServerJobContext.ConstructJobContext(Microsoft.ReportingServices.Diagnostics.ProcessingContext.JobContext), DataProtection.Instance);
					DataSetDefinition dataSetDefinition = new DataSetDefinition(ReadOnlyChunkFactory.FromSnapshot(reportSnapshot));
					processingEngine.ProcessDataSetParameters(dataSetContext, dataSetDefinition);
					Microsoft.ReportingServices.Diagnostics.ProcessingContext.JobContext.ExecutionInfo.Parameters = dataSetContext.Parameters.ToUrl(false);
					base.TargetSnapshot = ReportSnapshot.Create(false, reportSnapshot.ProcessingFlags);
					base.TargetSnapshot.WriteNewSnapshotToDB(combinedRequestParameters, executionDateNotTruncatedLocalTime, base.Item.Description);
					using (ISnapshotTransaction snapshotTransaction2 = base.TargetSnapshot.EnterTransactionContext())
					{
						dataSetContext.CreateChunkFactory = base.TargetSnapshot;
						processingEngine.ProcessSharedDataSet(dataSetContext, dataSetDefinition);
						snapshotTransaction2.Commit();
					}
				}
			}
			base.Service.Storage.Commit();
		}
	}
}
