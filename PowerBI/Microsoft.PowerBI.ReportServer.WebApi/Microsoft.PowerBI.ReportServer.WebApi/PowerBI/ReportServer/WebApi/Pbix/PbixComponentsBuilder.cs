using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.BIServer.HostingEnvironment.Contracts;
using Microsoft.PowerBI.ReportServer.AsServer.Artifacts;
using Microsoft.PowerBI.ReportServer.PbixLib.Parsing;
using Microsoft.ReportingServices.Portal.ODataClient.V2;

namespace Microsoft.PowerBI.ReportServer.WebApi.Pbix
{
	// Token: 0x02000017 RID: 23
	public class PbixComponentsBuilder : IPbixComponentsBuilder
	{
		// Token: 0x06000049 RID: 73 RVA: 0x0000303F File Offset: 0x0000123F
		public static IPbixComponentsBuilder GetInstance(IDataModelArtifactsProvider modelArtifactsProvider)
		{
			ContractExtensions.NotNull<IDataModelArtifactsProvider>(modelArtifactsProvider, "modelArtifactsProvider");
			return new PbixComponentsBuilder(modelArtifactsProvider);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003052 File Offset: 0x00001252
		internal PbixComponentsBuilder(IDataModelArtifactsProvider modelArtifactsProvider)
		{
			this._modelArtifactsProvider = modelArtifactsProvider;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003064 File Offset: 0x00001264
		public async Task<PbixComponents> BuildPbixComponentsFromPbixReportElements(PbixReportElements pbixReportElements, string requestId, string sessionId, bool getDataModelArtifacts = true)
		{
			bool flag = pbixReportElements.DataModel != null && pbixReportElements.DataModel.Length != 0;
			string modelVersion = pbixReportElements.ModelVersion;
			PbixComponents pbixComponents = new PbixComponents
			{
				ReportDocument = pbixReportElements.ReportDocument,
				ReportMobileState = pbixReportElements.ReportMobileState,
				Connections = pbixReportElements.ConnectionsSettings,
				CreatedFromVersion = pbixReportElements.AuthorVersion,
				DataModel = pbixReportElements.DataModel,
				StaticResources = pbixReportElements.StaticResources,
				HasEmbeddedModels = flag,
				CustomVisuals = pbixReportElements.CustomVisuals,
				HasCustomVisuals = (pbixReportElements.CustomVisuals != null && pbixReportElements.CustomVisuals.Count > 0),
				IsMobileOptimized = pbixReportElements.IsMobileOptimized(),
				DatabaseType = (flag ? DataModelDataSourceType.Unknown : DataModelDataSourceType.Live),
				HasDirectQuery = false,
				EmbeddedDataSources = this.GetInitialDataSourcesFromPbixReportElements(pbixReportElements),
				ModelVersion = modelVersion
			};
			if (getDataModelArtifacts)
			{
				DataModelArtifacts dataModelArtifacts = null;
				try
				{
					dataModelArtifacts = await this.GetArtifactsFromComponents(pbixComponents, requestId, sessionId);
					if (!string.IsNullOrEmpty((dataModelArtifacts != null) ? dataModelArtifacts.ModelVersion : null))
					{
						pbixComponents.ModelVersion = dataModelArtifacts.ModelVersion;
					}
					Logger.Info("BuildPbixComponentsFromPbixReportElements.GetArtifactsFromComponents: Finished getting artifacts from components. ModelVersion: {0}.", new object[] { pbixComponents.ModelVersion });
				}
				catch (Exception ex)
				{
					Logger.Error("BuildPbixComponentsFromPbixReportElements.GetArtifactsFromComponents: Failure in getting artifacts from components. ModelVersion: {0}. Exception {1}", new object[] { modelVersion, ex });
					throw new Exception("BuildPbixComponentsFromPbixReportElements.GetArtifactsFromComponents: Failure in getting artifacts from components. ModelVersion: " + modelVersion + ". Exception: " + ex.Message);
				}
				PbixComponentsBuilder.AppendEmbeddedDataSources(dataModelArtifacts, pbixComponents);
			}
			return pbixComponents;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000030CC File Offset: 0x000012CC
		internal static void AppendEmbeddedDataSources(DataModelArtifacts dataModelArtifacts, PbixComponents pbixComponents)
		{
			if (dataModelArtifacts == null)
			{
				return;
			}
			IReadOnlyList<PbixDataSource> embeddedDataSources = dataModelArtifacts.EmbeddedDataSources;
			pbixComponents.ModelRefreshAllowed = false;
			pbixComponents.HasDirectQuery = embeddedDataSources.Any((PbixDataSource x) => x.Type == AccessType.DirectQuery);
			IEnumerable<DataSource> enumerable = embeddedDataSources.Select((PbixDataSource p) => p.ToOdataV2DataSource());
			pbixComponents.EmbeddedDataSources = pbixComponents.EmbeddedDataSources.Union(enumerable).ToList<DataSource>();
			pbixComponents.DataModelRoles = dataModelArtifacts.DataModelRoles.Select((PbixModelRole p) => p.ToDataModelRole()).ToList<DataModelRole>();
			pbixComponents.DataModelParameters = dataModelArtifacts.DataModelParameters.Select((PbixModelParameter p) => p.ToDataModelParameter()).ToList<DataModelParameter>();
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000031C0 File Offset: 0x000013C0
		private IList<DataSource> GetInitialDataSourcesFromPbixReportElements(PbixReportElements pbixReportElements)
		{
			if (pbixReportElements == null || pbixReportElements.DataSources == null)
			{
				return new List<DataSource>();
			}
			return pbixReportElements.DataSources.Select((PbixDataSource p) => p.ToOdataV2DataSource()).ToList<DataSource>();
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003210 File Offset: 0x00001410
		private async Task<DataModelArtifacts> GetArtifactsFromComponents(PbixComponents pbixComponents, string requestId, string clientSessionId)
		{
			DataModelArtifacts dataModelArtifacts;
			if (!pbixComponents.HasEmbeddedModels)
			{
				dataModelArtifacts = null;
			}
			else
			{
				MemoryStream memoryStream = new MemoryStream(pbixComponents.DataModel);
				dataModelArtifacts = await this._modelArtifactsProvider.RetrieveArtifactsAsync(memoryStream, requestId, clientSessionId);
			}
			return dataModelArtifacts;
		}

		// Token: 0x04000043 RID: 67
		private readonly IDataModelArtifactsProvider _modelArtifactsProvider;
	}
}
